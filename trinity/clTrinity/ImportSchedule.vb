Imports Microsoft.VisualBasic.Compatibility
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports clTrinity.ScheduleTemplates.cScheduleTemplate


Module ImportSchedules
    Dim DateOutOfPeriod As New Hashtable
    Dim frmImport As frmImport ' New frmImport
    Dim ViasatColumns As New Collection
    Dim ViasatBudgets As New Collection

    Private Function findColumnHeadersRowNumber(ByVal WB as CultureSafeExcel.Workbook, ByVal sheetNumber As Integer, ByVal startRow As Integer, ByVal valueColumn As Integer, ByVal searchStrings As List(Of String)) As Integer
        'search the sheet for one of the specified values in the list and returns the row number
        'return value -1 is error

        If searchStrings.Count = 0 Then Return -1
        If WB Is Nothing Then Return -1

        Dim returnValue = -1
        Try
            'search the first 50 rows from the start row
            For i As Integer = startRow To (startRow + 50)
                If searchStrings.Contains(WB.Sheets(sheetNumber).Cells(i, valueColumn).value) Then
                    If returnValue = -1 Then
                        returnValue = i
                    Else
                        'found two values, let the user pick the right
                        returnValue = -1
                        Exit For
                    End If
                End If
            Next

            If returnValue = -1 Then
                'let the user pick the row we are looking for
                Dim f As New frmPickExcelColumnHeader()
                f.populateFormFromExcel(WB, "", startRow)

                If f.ShowDialog = DialogResult.OK Then
                    returnValue = f.pointPicked.Y
                End If
            End If
        Catch ex As Exception
            Dim s As String = ex.ToString()
        End Try

        Return returnValue
    End Function

    Private Function findColumnHeader(ByVal WB as CultureSafeExcel.Workbook, ByVal sheetNumber As Integer, ByVal name As String, ByVal row As Integer, ByVal searchHeaders As List(Of String)) As Point
        If searchHeaders.Count = 0 Then Return New Point(-1, -1)
        If WB Is Nothing Then Return New Point(-1, -1)

        Dim returnValue As New Point(-1, row)

        Try
            'search the first 100 columns
            For i As Integer = 1 To 50
                If Not WB.Sheets(sheetNumber).Cells(row, i).value Is Nothing Then
                    If searchHeaders.Contains(WB.Sheets(sheetNumber).Cells(row, i).value) Then
                        If returnValue.X = -1 Then
                            returnValue.X = i
                        Else
                            'found two values, let the user pick the right
                            returnValue.X = -1
                            Exit For
                        End If
                    End If
                End If
            Next

            If returnValue.X = -1 Then
                'let the user pick the column
                Dim f As New frmPickExcelColumnHeader()
                f.populateFormFromExcel(WB, name, 1)

                If f.ShowDialog = DialogResult.OK Then
                    returnValue = f.pointPicked
                End If
            End If
        Catch ex As Exception
            Dim s As String = ex.ToString()
        End Try

        Return returnValue
    End Function

    Private Sub ImportDiscoverySchedule(ByVal WB as CultureSafeExcel.Workbook)
        Dim Row As Integer
        'Dim TargetRow As Integer
        Dim Target As String
        Dim Gender As String
        Dim AgeStr As String
        Dim MinAge As String
        Dim MaxAge As String
        Dim SkippedSpot As Boolean = False
        Dim r As Integer
        Dim d As String
        Dim c As Integer

        Dim TmpDate As Date
        Dim StartDate As Date
        Dim EndDate As Date

        Dim ChanCol As Integer
        Dim DateCol As Integer
        Dim FilmCol As Integer
        Dim ProgCol As Integer
        Dim TimeCol As Integer
        Dim DurCol As Integer

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim MaM As Integer
        Dim fk As String

        'Discovery
        Dim argList As New List(Of String)

        Dim _channel As String

        Row = 1
        With WB.Sheets(1)
            If .range("B3").value.ToString.StartsWith("Discovery") Then
                _channel = "Discovery"
            Else
                _channel = "TLC"
            End If
            Campaign.Channels(_channel).IsVisible = True

            Target = Trim(.range("B6").value)
            Gender = Target.Substring(0, 1)
            AgeStr = Trim(Mid(Target, 2))
            If InStr(AgeStr, "-") > 1 Then
                MinAge = AgeStr.Substring(0, InStr(AgeStr, "-") - 1)
                MaxAge = Mid(AgeStr, InStr(AgeStr, "-") + 1)
                Campaign.Channels(_channel).MainTarget.TargetName = Gender & MinAge & "-" & MaxAge
            ElseIf InStr(AgeStr, ".") > 1 Then
                MinAge = AgeStr.Substring(0, InStr(AgeStr, ".") - 1)
                MaxAge = Mid(AgeStr, InStr(AgeStr, ".") + 1)
                Campaign.Channels(_channel).MainTarget.TargetName = Gender & MinAge & "-" & MaxAge
            Else
                Campaign.Channels(_channel).MainTarget.TargetName = Target
            End If
            Select Case Gender
                Case "W" : Gender = "W"
                Case "M" : Gender = "M"
                Case "A" : Gender = ""
                Case Else : Gender = ""
            End Select

            'Campaign.Channels(_channel).MainTarget.TargetName = Gender & MinAge & "-" & MaxAge
            Campaign.Channels(_channel).MainTarget.Universe = Campaign.Channels(_channel).BuyingUniverse

            While .cells(Row, 1).value Is Nothing OrElse .Cells(Row, 1).value.ToString.Substring(0, 5) <> "Start"
                Row = Row + 1
            End While
            r = Row
            If .Cells(r, 2).value.ToString.Contains("-") Then
                'Dim TmpDateSplitIntoParts As String() = Strings.Split(.Cells(r, 2).value.ToString, "-")
                'd = DateSerial(CInt(TmpDateSplitIntoParts(0)), CInt(TmpDateSplitIntoParts(1)), CInt(TmpDateSplitIntoParts(2)))
                'd = CDate(.Cells(r, 2).value)
                d = .Cells(r, 2).value
            Else
                'Dim TmpDateSplitIntoParts As String() = Strings.Split(.Cells(r, 2).value.ToString, "-")
                'd = DateSerial(CInt(TmpDateSplitIntoParts(0)), CInt(TmpDateSplitIntoParts(1)), CInt(TmpDateSplitIntoParts(2)))
                'd = CDate(.Cells(r, 2).value.ToString.Substring(6) & "-" & .Cells(r, 2).value.ToString.Substring(3, 2) & .Cells(r, 2).value.ToString.Substring(0, 2))
                d = .Cells(r, 2).value
            End If
            Try
                StartDate = CDate(d)
            Catch ex As Exception
                StartDate = Date.FromOADate(Campaign.StartDate)
            End Try

            While .Cells(r, 1).value.ToString.Substring(0, 4) <> "Slut"
                r = r + 1
            End While
            If .Cells(r, 2).value.ToString.Contains("-") Then
                'Dim TmpDateSplitIntoParts As String() = Strings.Split(.Cells(r, 2).value.ToString, "-")
                'd = DateSerial(CInt(TmpDateSplitIntoParts(0)), CInt(TmpDateSplitIntoParts(1)), CInt(TmpDateSplitIntoParts(2)))
                'd = CDate(.Cells(r, 2).value)
                d = .Cells(r, 2).value
            Else
                'Dim TmpDateSplitIntoParts As String() = Strings.Split(.Cells(r, 2).value.ToString, "-")
                'd = DateSerial(CInt(TmpDateSplitIntoParts(0)), CInt(TmpDateSplitIntoParts(1)), CInt(TmpDateSplitIntoParts(2)))
                'd = CDate(.Cells(r, 2).value.ToString.Substring(6) & "-" & .Cells(r, 2).value.ToString.Substring(3, 2) & .Cells(r, 2).value.ToString.Substring(0, 2))

                d = .Cells(r, 2).value
            End If
            Try
                EndDate = CDate(d)
            Catch ex As Exception
                EndDate = Date.FromOADate(Campaign.EndDate)
            End Try

            Dim frmImport As New frmImport(Campaign.Channels(_channel))
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate
            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - Discovery"
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name
            Dim tmpNetBudget As Single
            'It looks as if spaces are removed twice, but the first space is actually chr(160) that is sometimes used as divider in Excel, not the normal space chr(32) 
            tmpNetBudget = .Range("B10").value.ToString.Replace(" ", "").Replace(" ", "").ToString.Replace(",", ".")
            frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Idx = frmImport.txtIndex.Text
            BT = frmImport.cmbBookingType.Text

            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = _channel Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedSpot Is Nothing Then
                                        TmpSpot.MatchedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            If frmImport.optReplaceBudget.Checked Then
                Campaign.Channels(_channel).BookingTypes(BT).ConfirmedNetBudget = CDec(tmpNetBudget)
            Else
                Campaign.Channels(_channel).BookingTypes(BT).ConfirmedNetBudget += CDec(tmpNetBudget)
            End If
            Campaign.Channels(_channel).BookingTypes(BT).ContractNumber = .Range("B1").value

            argList.Clear()
            argList.Add("Date")
            argList.Add("Datum")
            argList.Add("Dato")
            argList.Add("Channel")

            Row = findColumnHeadersRowNumber(WB, 1, 1, 1, argList)
            'Row = 1
            'While .cells(Row, 1).value Is Nothing OrElse (Not .cells(Row, 1).value.ToString.Contains("Date") AndAlso .cells(Row, 1).value.ToString <> "Datum" AndAlso .cells(Row, 1).value.ToString <> "Dato" AndAlso .cells(Row, 1).value.ToString <> "Channel")
            '    Row = Row + 1
            'End While


            c = 1
            While .cells(Row, c).value <> "Channel" And c < 20
                c += 1
            End While
            ChanCol = c

            c = 1

            argList.Clear()
            argList.Add("Date")
            argList.Add("Datum")
            argList.Add("Scheduled Date")
            DateCol = findColumnHeader(WB, 1, "Date", Row, argList).X 'Borde kanske kolla att Y = Row?

            'While .cells(Row, c).value <> "Date" And .cells(Row, c).value <> "Scheduled Date" And c < 20
            '    c += 1
            'End While
            'DateCol = c

            argList.Clear()

            argList.Add("Time")
            argList.Add("Tid")
            argList.Add("Scheduled Time")
            TimeCol = findColumnHeader(WB, 1, "Time", Row, argList).X 'Borde kanske kolla att Y = Row?

            'c = 1
            'While .cells(Row, c).value <> "Time" And .cells(Row, c).value <> "Scheduled Time" And c < 20
            '    c += 1
            'End While
            'TimeCol = c

            argList.Clear()
            argList.Add("Program")
            argList.Add("Programme")
            argList.Add("Programe")
            argList.Add("Programme/Category")
            ProgCol = findColumnHeader(WB, 1, "Program", Row, argList).X 'Borde kanske kolla att Y = Row?

            'c = 1
            'While .cells(Row, c).value <> "Programme" And .cells(Row, c).value <> "Programme/Category" And c < 20
            '    c += 1
            'End While
            'ProgCol = c


            argList.Clear()
            argList.Add("Sec.")
            argList.Add("Sek")
            argList.Add("Duration")
            DurCol = findColumnHeader(WB, 1, "Duration", Row, argList).X 'Borde kanske kolla att Y = Row?

            'c = 1
            'While .cells(Row, c).value <> "Sec." And c < 20
            '    c += 1
            'End While
            'DurCol = c

            argList.Clear()
            argList.Add("AC-Nielsen")
            argList.Add("Filmcode")
            argList.Add("Filmkode")
            argList.Add("Filmkod")
            argList.Add("Spot")
            argList.Add("SPOT")

            FilmCol = findColumnHeader(WB, 1, "Filmcode", Row, argList).X 'Borde kanske kolla att Y = Row?

            'c = 1
            'While .cells(Row, c).value IsNot Nothing AndAlso .cells(Row, c).value <> "AC-Nielsen" And .cells(Row, c).value <> "Filmkode" And c < 20
            '    c += 1
            'End While
            'FilmCol = c
            If c = 20 Then
                c = 1
                While .cells(Row, c).value IsNot Nothing AndAlso Not .cells(Row, c).value.ToString.Contains("Kanale") And c < 20
                    c += 1
                End While
                ChanCol = c
            End If
            Row = Row + 1

            'Dates on spots are either in the format YYYY-MM-DD or DD-MM-YYYY
            'Now we only handle the case where the date is a proper date in Excel
            While Not .Cells(Row, 1).value Is Nothing AndAlso .Cells(Row, 1).value.ToString <> ""

                Try
                    TmpDate = CDate(.cells(Row, DateCol).value)
                Catch ex As Exception
                    If Not .cells(Row, DateCol).value = Nothing AndAlso .cells(Row, DateCol).value.ToString.Contains("/") Then 'Date is like dd/mm/yyyy
                        Dim splitDate() As String = Strings.Split(.cells(Row, DateCol).value.ToString, "/")
                        TmpDate = DateSerial(CInt(Strings.Left(splitDate(2), 4)), CInt(splitDate(1)), CInt(splitDate(0)))
                        If Not (TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value) Then
                            'Lets check if Discovery have put the day and the month in a different order and see if the resulting date is in the campaign
                            Dim TmpDate2 As Date
                            Dim splitDateOtherMonthDayYearOrder As String() = Strings.Split(.cells(Row, 1).value.ToString, "/")
                            If Not splitDateOtherMonthDayYearOrder(2).ToString.Length > 2 Then
                                TmpDate2 = DateSerial(CInt(splitDateOtherMonthDayYearOrder(2)), CInt(splitDateOtherMonthDayYearOrder(0)), CInt(splitDateOtherMonthDayYearOrder(1)))
                                If TmpDate2 >= frmImport.dtFrom.Value And TmpDate2 <= frmImport.dtTo.Value Then
                                    'If we are here, then yes, this date is in the campaign if the order of the day and month are reversed
                                    TmpDate = TmpDate2
                                End If
                            Else
                                TmpDate = DateSerial(CInt(Left(splitDateOtherMonthDayYearOrder(2), 4)), CInt(splitDateOtherMonthDayYearOrder(0)), CInt(splitDateOtherMonthDayYearOrder(1)))
                            End If
                        End If
                    Else 'Date is like yyyy-mm-dd which we can make a CDate out of
                        TmpDate = .Cells(Row, DateCol).value
                    End If
                End Try


                If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                    TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                    If ChanCol < 20 AndAlso .cells(Row, ChanCol).value IsNot Nothing AndAlso .cells(Row, ChanCol).value.GetType.FullName = "System.String" AndAlso .cells(Row, ChanCol).value.ToString.Contains("TLC") Then '
                        TmpSpot.Channel = Campaign.Channels("TLC")
                    Else
                        TmpSpot.Channel = Campaign.Channels(_channel)
                    End If
                    TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                    TmpSpot.AirDate = TmpDate.ToOADate
                    .Columns(2).AutoFit()
                    If .cells(Row, DateCol).value.GetType.FullName = "System.Double" Or .cells(Row, DateCol).value.GetType.FullName = "System.DateTime" Then
                        Try
                            Dim Day As Date = Date.FromOADate(.cells(Row, TimeCol).value)
                            Dim Hrs As Integer = Day.Hour
                            Dim Mins As Integer = Day.Minute
                            MaM = Hrs * 60 + Mins
                        Catch
                            Dim Hrs As Integer = Int(Strings.Left(.cells(Row, TimeCol).value, 2))
                            Dim Mins As Integer = Int(Strings.Right(Strings.Left(.cells(Row, TimeCol).value, 5), 2))
                            MaM = Hrs * 60 + Mins
                        End Try
                    Else
                        Dim t2 As String = .cells(Row, TimeCol).value
                        If Not t2 Is Nothing Then
                            If t2.Length = 8 Then
                                Dim Hrs As Integer = CInt(Strings.Left(t2, 2))
                                Dim Mins As Integer = CInt(Strings.Right(Strings.Left(t2, 5), 2))
                                'If Hrs >= 24 Then Hrs = Hrs - 24
                                MaM = Hrs * 60 + Mins
                            End If
                            't = Format(Val(.Cells(Row, 2).Text), "0000")
                            'MaM = 60 * Val(t.Substring(0, 2)) + Val(Mid(t, 3))
                        End If
                    End If
                    'If MaM < 120 Then MaM = MaM + 1440
                    TmpSpot.MaM = MaM
                    TmpSpot.Programme = .Cells(Row, ProgCol).value
                    'set the film code
                    fk = .Cells(Row, FilmCol).value
                    ' If fk Is Nothing OrElse fk = "" Then
                    'fk = .range("B12").Text
                    ' End If
                    If fk Is Nothing OrElse fk = "" Then
                        fk = .Cells(Row, DurCol).value 'use the spot length
                    Else
                        fk = Strings.Trim(fk)
                    End If

                    TmpSpot.Filmcode = Strings.Trim(fk)
                    'TmpSpot.Remark = .Cells(Row, 6).value
                    TmpSpot.SpotLength = .Cells(Row, DurCol).value
                    TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                    If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                        DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                        Campaign.PlannedSpots.Remove(TmpSpot.ID)
                    End If
                    If Not TmpSpot.Week Is Nothing Then

                        Trinity.Helper.SetFilmForSpot(TmpSpot)


                        TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                        TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = 0
                        TmpSpot.MyRating = 0
                        TmpSpot.PriceNet = 0
                        TmpSpot.PriceGross = 0
                    Else
                        'Stop
                    End If
                End If
                Row = Row + 1
            End While
        End With
        If DateOutOfPeriod.Count > 0 Then
            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
            For i As Integer = 0 To DateOutOfPeriod.Count
                Msg += Format(Date.FromOADate(DateOutOfPeriod(i)), "Short date") & vbCrLf
            Next
            Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        'On Error GoTo 0
        Exit Sub

cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
    End Sub

    Private Sub ImportTV4PlusSchedule(ByVal WB As CultureSafeExcel.Workbook)
        Dim Row As Integer
        Dim TargetRow As Integer
        Dim Target As String
        Dim Gender As String
        Dim AgeStr As String
        Dim MinAge As String
        Dim MaxAge As String
        Dim SkippedSpot As Boolean = False
        Dim r As Integer
        Dim d As String
        Dim c As Integer
        Dim t As String

        Dim showEvaluateSpecifics As Boolean = False

        Dim TmpDate As Date
        Dim TmpYear As String
        Dim TmpMonth As String
        Dim TmpDay As String
        Dim StartDate As Date
        Dim EndDate As Date
        Dim StartRow As Integer
        Dim Stat As String

        Dim ChanCol As Integer
        Dim DateCol As Integer
        Dim FilmCol As Integer
        Dim ProgCol As Integer
        Dim TimeCol As Integer
        Dim RatingCol As Integer
        Dim LengthCol As Integer
        Dim BreakTypeCol As Integer
        Dim RemarkCol As Integer
        Dim PriceCol As Integer
        Dim PlacCol As Integer
        Dim GrossCol As Integer
        Dim NetCol As Integer
        Dim DurCol As Integer

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpFilm As Trinity.cFilm
        Dim MaM As Integer
        Dim UI As Single
        Dim fk As String
        Dim Chan As String

        Dim a As Integer
        Dim en As Long
        Dim ed As String
        Dim emptyRow As Integer = 0

        Dim intDateCol As Integer


        'TV4+ bekräftelse

        Dim rowCounter As Integer = 1
        Dim columnCounter As Integer = 1
        Dim emptyRows As Integer = 0

        With WB.Sheets(1)

            While .cells(rowCounter, columnCounter).Text <> "Filmkod" And .cells(rowCounter, columnCounter).Text <> "Program Id" And rowCounter < 200
                rowCounter += 1
            End While
            While .cells(rowCounter, columnCounter).Text <> "Sändn.dat" And .cells(rowCounter, columnCounter).Text <> "Sändningsdatum" And columnCounter < 30
                columnCounter += 1
            End While
            rowCounter += 1

            If .cells(rowCounter, columnCounter).value.ToString.Length = 10 Then
                d = CDate(.cells(rowCounter, columnCounter).value)
            Else
                Try
                    d = CDate(Left(.cells(rowCounter + 1, columnCounter).value, 4) & "-" & Mid(.cells(rowCounter + 1, columnCounter).value, 5, 2) & "-" & Right(.cells(rowCounter + 1, columnCounter).value, 2))


                Catch ex As Exception
                    d = Date.FromOADate(CDbl(.cells(rowCounter + 1, columnCounter).value))
                End Try

            End If
            StartDate = d

            rowCounter += 1

            While emptyRows < 100

                If Not .cells(rowCounter, columnCounter).value Is Nothing Then
                    If .cells(rowCounter, columnCounter).value.ToString.Length = 10 AndAlso .cells(rowCounter, columnCounter).value.ToString.Contains("-") Then
                        Try
                            d = CDate(.Cells(rowCounter, columnCounter).value)
                        Catch
                            d = Date.FromOADate(.Cells(rowCounter, columnCounter).value)
                        End Try
                        'Else
                        '   d = CDate(Left(.cells(rowCounter, columnCounter).value, 4) & "-" & Mid(.cells(rowCounter, columnCounter).value, 5, 2) & "-" & Right(.cells(rowCounter, columnCounter).value, 2))
                    End If
                Else
                    emptyRows += 1
                End If
                If d > EndDate Then EndDate = d
                EndDate = d
                rowCounter += 1
            End While

        End With

        Row = 1

read_Spots:

        With WB.Sheets(1)

            Campaign.Channels("TV4+").IsVisible = True

            Campaign.Channels("TV4+").MainTarget.TargetName = "12-59"
            Campaign.Channels("TV4+").MainTarget.Universe = ""

            Dim chunkBefore As Integer = Row + 1

            Dim RBS As Boolean = False
            Dim Spec As Boolean = False
            Dim LastMinute As Boolean = False

            While .Cells(Row, 1).value <> "Filmkod" AndAlso Not InStr(.Cells(Row, 1).value, "Program") > 0

                'if it is a RBS 
                If .Cells(Row, 1).value = "RBS" Then RBS = True

                'if it is a Specific
                If .Cells(Row, 1).value = "Specific" Then Spec = True

                'if it is a Specific
                If .Cells(Row, 1).value = "LastMinute" Or .cells(Row, 1).value = "Last Minute" Then LastMinute = True

                'if we cant find the "filmkod" it only contains the budget
                If RBS AndAlso Row > 100 Then
                    If chunkBefore < 20 Then
                        Row = 2
                    Else
                        Row = chunkBefore
                    End If
                    GoTo import_RBS_Budget
                End If
                Row = Row + 1
            End While

            Row = Row + 1
            r = Row

            'we get the number for the date column
            c = 1
            While InStr(.Cells(Row - 1, c).value, "dat") < 1 And c < 200
                c = c + 1
            End While
            intDateCol = c


            If .cells(r, intDateCol).value.ToString.Length = 10 Then
                d = CDate(.Cells(r, intDateCol).value)
            Else
                Try
                    d = CDate(Left(.cells(r, intDateCol).value, 4) & "-" & Mid(.cells(r, intDateCol).value, 5, 2) & "-" & Right(.cells(r, intDateCol).value, 2))
                Catch ex As Exception
                    d = Date.FromOADate(.cells(r, intDateCol).value)
                End Try

            End If
            StartDate = d

            'if there is no filmcode use the duration instead
            If .cells(r, 1).value Is Nothing Then
                While Not .cells(r, 2).value Is Nothing
                    r = r + 1
                End While
            Else
                While Not .cells(r, 1).value Is Nothing AndAlso InStr(.Cells(r, 1).value.ToString, "Total") < 1
                    r = r + 1
                End While
            End If

            r = r - 1
            If .cells(r, intDateCol).value.ToString.Length = 10 Then
                d = CDate(.Cells(r, intDateCol).value)
            Else
                Try
                    d = CDate(Left(.cells(r, intDateCol).value, 4) & "-" & Mid(.cells(r, intDateCol).value, 5, 2) & "-" & Right(.cells(r, intDateCol).value, 2))
                Catch ex As Exception
                    d = Date.FromOADate(.cells(r, intDateCol).value)
                End Try

            End If
            EndDate = d


            Dim frmImport As New frmImport(Campaign.Channels("TV4+"))
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate

            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - TV4+"
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name
            If RBS Then frmImport.Label6.Tag = "RBS"
            If Spec Then frmImport.Label6.Tag = "Spec"
            Dim tmpNetBudget As Single = 0
            r = 1
            While .cells(r, 1).Value <> "SUMMA"
                'If .cells(r, 1).Value = "Pris" Then
                'tmpNetBudget += .cells(r, 2).Value.ToString.Trim
                'End If
                r += 1
            End While
            While Not .cells(r + 1, 1).value Is Nothing
                If .cells(r, 1).value.ToString.ToUpper.IndexOf("SPEC") > -1 And Spec Then tmpNetBudget = CSng(Trinity.Helper.RemoveNonNumbersFromString(.cells(r, 3).value.ToString))
                If .cells(r, 1).value.ToString.ToUpper.IndexOf("RBS") > -1 And RBS Then tmpNetBudget = CSng(Trinity.Helper.RemoveNonNumbersFromString(.cells(r, 3).value.ToString))
                If .cells(r, 1).value.ToString.ToUpper.IndexOf("LAST") > -1 And LastMinute Then tmpNetBudget = CSng(Trinity.Helper.RemoveNonNumbersFromString(.cells(r, 3).value.ToString))
                'If .cells(r, 1).value.ToString.ToUpper.IndexOf("SPEC") > -1 And Spec Then tmpNetBudget = .cells(r, 2).value.ToString.Trim
                r += 1
            End While
            frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Idx = frmImport.txtIndex.Text
            BT = frmImport.cmbBookingType.Text

            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = "TV4+" Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedSpot Is Nothing Then
                                        TmpSpot.MatchedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If

            If frmImport.chkEvaluate.Checked Then
                showEvaluateSpecifics = True
            End If

            If frmImport.optReplaceBudget.Checked Then
                Campaign.Channels("TV4+").BookingTypes(BT).ConfirmedNetBudget = CDec(tmpNetBudget)
            Else
                Campaign.Channels("TV4+").BookingTypes(BT).ConfirmedNetBudget += CDec(tmpNetBudget)
            End If
            Campaign.Channels("TV4+").BookingTypes(BT).ContractNumber = .Range("B3").value
            c = 1
            While .Cells(Row - 1, c).value <> "Estimat" And c < 200
                c = c + 1
            End While
            RatingCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "BB/BP" And c < 200
                c = c + 1
            End While
            PlacCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Brutto" And c < 200
                c = c + 1
            End While
            GrossCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Netto" And c < 200
                c = c + 1
            End While
            NetCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Anm" And c < 200
                c = c + 1
            End While
            RemarkCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Spotlängd" And .Cells(Row - 1, c).value <> "Sek" And c < 200
                c = c + 1
            End While
            LengthCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Filmkod" And c < 200
                c = c + 1
            End While
            FilmCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Tid o Program" And c < 200
                c = c + 1
            End While
            ProgCol = c

            'remake the numeric columns to numerics, not strings
            If NetCol = 13 Then
                .Columns("K:M").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                .Columns("K:M").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                .AllCells.Replace(What:=",", Replacement:=",", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
            Else
                .Columns("J:L").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                .Columns("J:L").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                .AllCells.Replace(What:=",", Replacement:=",", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
            End If


            While emptyRow < 6
                'read all the spots

                Dim l As Integer
                Dim fRow As Integer
                'if there is no filmcode use the duration instead
                If .cells(Row, FilmCol).value Is Nothing Then
                    fRow = FilmCol + 1
                    l = 1
                Else
                    fRow = FilmCol
                    l = 5
                End If
                While Not .Cells(Row, fRow).value Is Nothing
                    If InStr(.Cells(Row, 1).value, "Total") = 1 Then
                        Exit While
                    End If
                    If .cells(Row, intDateCol).value.ToString.Length = 10 Then
                        d = (.Cells(Row, intDateCol).value)
                    Else
                        Try
                            d = (Left(.cells(Row, intDateCol).value, 4) & "-" & Mid(.cells(Row, intDateCol).value, 5, 2) & "-" & Right(.cells(Row, intDateCol).value, 2))
                        Catch
                            d = Date.FromOADate(.cells(Row, intDateCol).value)
                        End Try
                    End If
                    Try
                        TmpDate = CDate(d)
                    Catch ex As Exception
                        d = Date.FromOADate(.cells(Row, intDateCol).value)
                        TmpDate = CDate(d)
                    End Try

                    If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                        TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                        TmpSpot.Channel = Campaign.Channels("TV4+")
                        TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                        TmpSpot.AirDate = TmpDate.ToOADate
                        .Columns(2).AutoFit()
                        t = .Cells(Row, ProgCol).Text.ToString.Substring(0, 5)
                        MaM = 60 * Val(t.Substring(0, 2)) + Val(Mid(t, 4))
                        If MaM < 120 Then MaM = MaM + 1440
                        TmpSpot.MaM = MaM
                        TmpSpot.Programme = Mid(.Cells(Row, ProgCol).value, 7)
                        TmpSpot.SpotLength = .Cells(Row, LengthCol).value
                        If fRow <> FilmCol Then
                            fk = TmpSpot.SpotLength
                            If Not fk.Contains("s") Then
                                fk = fk & "s"
                            End If
                        Else
                            fk = .Cells(Row, FilmCol).value
                            If fk.Substring(0, 2) = "AA" Then fk = Mid(fk, 3)
                        End If
                        TmpSpot.Filmcode = fk
                        TmpSpot.Remark = .Cells(Row, RemarkCol).value
                        If TmpSpot.Filmcode Is Nothing Or TmpSpot.Filmcode = "" Then
                            TmpSpot.Filmcode = TmpSpot.SpotLength
                        End If
                        If Date.FromOADate(TmpSpot.AirDate).Year = 1899 Then Stop
                        TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                        If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                            DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                            Campaign.PlannedSpots.Remove(TmpSpot.ID)
                        End If
                        If Not TmpSpot.Week Is Nothing Then

                            Trinity.Helper.SetFilmForSpot(TmpSpot)

                            TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = .Cells(Row, RatingCol).Value
                            'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = .Cells(Row, RatingCol).Value
                            TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = .Cells(Row, RatingCol).Value
                            'If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                            '    UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                            'Else
                            '    UI = 0
                            'End If
                            'If UI > 0 Then
                            '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = TmpSpot.MyRating / UI
                            'Else
                            '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                            'End If
                            TmpSpot.PriceNet = .Cells(Row, NetCol).value
                            TmpSpot.PriceGross = .Cells(Row, GrossCol).value
                        End If
                    End If
                    Row = Row + 1
                    Debug.Print(Row)
                End While
                'finished reading all the spots

                'read the next lines and check for another chunk of spots
                Dim z As Integer
                Dim tmpStr As String
                For z = 0 To 60
                    Row += 1
                    tmpStr = .Cells(Row, 1).value
                    If tmpStr Is Nothing Then
                        emptyRow += 1
                        If emptyRow = 10 Then Exit While
                    Else
                        If tmpStr.ToUpper = "SUMMA" Then
                            Exit While
                        End If
                        'if we find more spots in another chunk (different headline) we redo the process
                        If Spec Then
                            If tmpStr = "RBS" Then
                                GoTo read_spots
                            End If
                        End If

                        If LastMinute Then
                            If tmpStr = "RBS" Then
                                GoTo read_spots
                            End If
                        End If

                        If RBS Then
                            If tmpStr = "Specific" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "RBS" Then
                                GoTo read_Spots
                            End If
                        End If
                        emptyRow = 0
                    End If
                    If Not tmpStr Is Nothing AndAlso tmpStr.ToUpper = "FILMKOD" Then
                        Row += 1
                        Exit For
                    End If
                Next

            End While

        End With

        If showEvaluateSpecifics Then
            frmEvaluateSpecifics.MdiParent = frmMain
            frmEvaluateSpecifics.Show()
        End If

        If DateOutOfPeriod.Count > 0 Then
            'Stop
            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
            For i As Integer = 0 To DateOutOfPeriod.Count - 1
                Msg += Format(Date.FromOADate(DateOutOfPeriod(i)), "Short date") & vbCrLf
            Next
            Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Exit Sub

import_RBS_Budget:

        Dim intPris As Integer
        Dim strPris As String
        Dim sum As Double = 0
        Dim bolGoToRead As Boolean = False

        With WB.Sheets(1)

read_pris:

            While .Cells(Row - 1, 1).value <> "Pris" And Row < 200
                If .Cells(Row - 1, 1).value = "Filmkod" Then
                    'if we have a budget we need to add it before reading spots
                    If sum = 0 Then
                        bolGoToRead = True
                        GoTo show_budget
                    End If
                    GoTo read_spots
                End If

                Row += 1
            End While
            If Row < 200 Then
                strPris = .Cells(Row - 1, 2).value
                If IsNumeric(strPris.Trim) Then
                    intPris = CInt(strPris.Trim)
                    sum += intPris
                End If
                Row += 2
                GoTo read_pris
            End If
        End With

show_budget:
        'put up the dialog of if to replace the budget or add to the budget
        Dim frmImportRBS As New frmImport(Campaign.Channels("TV4+"))
        frmImportRBS.dtFrom.Enabled = False
        frmImportRBS.dtTo.Enabled = False
        frmImportRBS.txtIndex.Text = 100
        frmImportRBS.Text = "Import Schedule - TV4+"
        frmImportRBS.lblPath.Tag = WB.Path
        frmImportRBS.lblPath.Text = WB.Name
        frmImportRBS.Label6.Tag = "RBS"
        frmImportRBS.chkReplace.Enabled = False
        frmImportRBS.lblConfirmationBudget.Text = Format(CDec(sum), "N0")
        If frmImportRBS.ShowDialog = Windows.Forms.DialogResult.Cancel Then GoTo cmdImportSchedule_Click_Error
        BT = frmImportRBS.cmbBookingType.Text

        If frmImportRBS.optReplaceBudget.Checked Then
            Campaign.Channels("TV4+").BookingTypes(BT).ConfirmedNetBudget = CDec(sum)
        Else
            Campaign.Channels("TV4+").BookingTypes(BT).ConfirmedNetBudget += CDec(sum)
        End If
        sum = 0
        'if we where directed here before going back to reading spots
        If bolGoToRead Then
            bolGoToRead = False
            GoTo read_spots
        End If

        Exit Sub

cmdImportSchedule_Click_Error:
        Debug.Print("Error reading TV4+ schedule on row " & Row)
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
    End Sub

    Private Sub ImportTV4Schedule(ByVal WB As CultureSafeExcel.Workbook)
        Dim Row As Integer
        Dim TargetRow As Integer
        Dim Target As String
        Dim Gender As String
        Dim AgeStr As String
        Dim MinAge As String
        Dim MaxAge As String
        Dim SkippedSpot As Boolean = False
        Dim r As Integer
        Dim d As String
        Dim c As Integer
        Dim t As String

        Dim showEvaluateSpecifics As Boolean = False
        Dim multipleSpecificsSections As Boolean = False

        Dim TmpDate As Date
        Dim TmpYear As String
        Dim TmpMonth As String
        Dim TmpDay As String
        Dim StartDate As Date
        Dim EndDate As Date
        Dim StartRow As Integer
        Dim Stat As String

        Dim ChanCol As Integer
        Dim DateCol As Integer
        Dim FilmCol As Integer
        Dim ProgCol As Integer
        Dim TimeCol As Integer
        Dim RatingCol As Integer
        Dim LengthCol As Integer
        Dim BreakTypeCol As Integer
        Dim RemarkCol As Integer
        Dim PriceCol As Integer
        Dim PlacCol As Integer
        Dim GrossCol As Integer
        Dim NetCol As Integer
        Dim DurCol As Integer

        ' New Columns for the summary row Net and Gross fields
        Dim SumNetCol As Integer
        Dim SumGrossCol As Integer

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpFilm As Trinity.cFilm
        Dim MaM As Integer
        Dim UI As Single
        Dim fk As String
        Dim Chan As String
        Dim PlacDict As New Dictionary(Of String, Trinity.cAddedValue)

        Dim intDateCol As Integer

        'TV4 bekräftelse

        Row = 1
        Dim Bookingtypes As Integer = 1
read_spots:

        '  Bookingtypes += 1
        'If Bookingtypes = 4 Then
        'MessageBox.Show("")
        ' End If

        With WB.Sheets(1)
            Campaign.Channels("TV4").IsVisible = True

            Campaign.Channels("TV4").MainTarget.TargetName = "12-59"
            Campaign.Channels("TV4").MainTarget.Universe = ""


            Dim chunkBefore As Integer = Row + 1
            Dim RBS As Boolean = False
            Dim Spec As Boolean = False
            Dim LastMinute As Boolean = False
            Dim FirstMinute As Boolean = False

            'This will find the first booking type on this TV4 confirmation, set a boolean to indicate which it is and then be ready to read the spots on the first line
            'after the name of the booking type

            Try


                While .Cells(Row, 1).value <> "Filmkod" AndAlso Not InStr(.Cells(Row, 1).value, "Program") > 0 AndAlso Row < 1000

                    'if it is a RBS 
                    If .Cells(Row, 1).value = "RBS" Then RBS = True

                    'if it is a Specific
                    If .Cells(Row, 1).value = "Specific" Then Spec = True

                    'if it is a last minute
                    If .Cells(Row, 1).value = "LastMinute" Then LastMinute = True

                    If .cells(Row, 1).value = "First Minute" Or .cells(Row, 1).value = "FirstMinute" Then FirstMinute = True


                    'If RBS AndAlso Row > 100 Then
                    '    If chunkBefore < 20 Then
                    '        Row = 2
                    '    Else
                    '        Row = chunkBefore
                    '    End If
                    '    GoTo import_RBS_Budget
                    'End If

                    Row = Row + 1
                End While
            Catch ex As Exception
                'Stop
            End Try

            If Row < 1000 Then
                Row = Row + 1
                r = Row

                'we get the number for the date column (which column after the name of the first booking type contains "dat"
                c = 1
                While InStr(.Cells(Row - 1, c).value, "dat") < 1 And c < 200
                    c = c + 1
                End While
                intDateCol = c
                'OK got the date column, but is it the same column for the RBS spots too?


                If .cells(r, intDateCol).value.ToString.Length = 10 Then
                    d = CDate(.Cells(r, intDateCol).value)
                ElseIf .cells(r, intDateCol).value.ToString.Length = 8 Then
                    Dim year As Integer = CInt(Left(.Cells(r, intDateCol).value.ToString, 4))
                    Dim month As Integer = CInt(Right(Left(.Cells(r, intDateCol).value.ToString, 6), 2))
                    Dim day As Integer = CInt(Right(.Cells(r, intDateCol).value.ToString, 2))
                    d = DateAndTime.DateSerial(year, month, day)
                ElseIf Date.FromOADate(.cells(r, intDateCol).value).Year > 2008 And Date.FromOADate(.cells(r, intDateCol).value).Year < 2012 Then
                    d = Date.FromOADate(.cells(r, intDateCol).value)
                Else
                    d = Nothing
                    'd = CDate(.cells(r, intDateCol).value) 'Comment this line and uncomment the next to change back to Johans version
                    'd = CDate(Left(.cells(r, intDateCol).value, 4) & "-" & Mid(.cells(r, intDateCol).value, 5, 2) & "-" & Right(.cells(r, intDateCol).value, 2))
                End If
                'The start date of the spotlist is set to the date of the first item in the spotlist
                'Could cause an error if other spots in this list are earlier or later?
                StartDate = d
                While .cells(r, 1).value Is Nothing OrElse .Cells(r, 1).value.ToString.Length < 5 OrElse .Cells(r, 1).value.ToString.Substring(0, 5) <> "Total"
                    r = r + 1
                End While
                r = r - 1
                'sometimes its a empty row after the last spot, sometimes not
                If .cells(r, intDateCol).value Is Nothing Then
                    If .cells(r - 1, intDateCol).value.ToString.Length = 10 Then
                        d = CDate(.Cells(r - 1, intDateCol).value)
                    ElseIf .cells(r - 1, intDateCol).value.ToString.Length = 8 Then
                        Dim year As Integer = CInt(Left(.Cells(r - 1, intDateCol).value.ToString, 4))
                        Dim month As Integer = CInt(Right(Left(.Cells(r - 1, intDateCol).value.ToString, 6), 2))
                        Dim day As Integer = CInt(Right(.Cells(r - 1, intDateCol).value.ToString, 2))
                        d = DateAndTime.DateSerial(year, month, day)
                    ElseIf Date.FromOADate(.cells(r - 1, intDateCol).value).Year > 2008 And Date.FromOADate(.cells(r - 1, intDateCol).value).Year < 2012 Then
                        d = Date.FromOADate(.cells(r - 1, intDateCol).value)
                    Else
                        'd = CDate(.cells(r - 1, intDateCol).value) 'Comment this line and uncomment the next to change back to Johans version
                        'd = CDate(Left(.cells(r - 1, intDateCol).value, 4) & "-" & Mid(.cells(r - 1, intDateCol).value, 5, 2) & "-" & Right(.cells(r - 1, intDateCol).value, 2))
                    End If
                Else
                    If .cells(r, intDateCol).value.ToString.Length = 10 Then
                        d = CDate(.Cells(r, intDateCol).value)
                    ElseIf .cells(r, intDateCol).value.ToString.Length = 8 Then
                        Dim year As Integer = CInt(Left(.Cells(r, intDateCol).value.ToString, 4))
                        Dim month As Integer = CInt(Right(Left(.Cells(r, intDateCol).value.ToString, 6), 2))
                        Dim day As Integer = CInt(Right(.Cells(r, intDateCol).value.ToString, 2))
                        d = DateAndTime.DateSerial(year, month, day)
                    ElseIf Date.FromOADate(.cells(r - 1, intDateCol).value).Year > 2008 And Date.FromOADate(.cells(r - 1, intDateCol).value).Year < 2012 Then
                        d = Date.FromOADate(.cells(r - 1, intDateCol).value)
                    Else
                        'd = CDate(.cells(r, intDateCol).value) 'Comment this line and uncomment the next to change back to Johans version
                        'd = CDate(Left(.cells(r, intDateCol).value, 4) & "-" & Mid(.cells(r, intDateCol).value, 5, 2) & "-" & Right(.cells(r, intDateCol).value, 2))
                    End If
                End If


                EndDate = d

                If Not .cells(6, 5).value Is Nothing AndAlso .cells(6, 5).value.ToString.Contains("start") Then
                    If Not .cells(6, 6).value Is Nothing Then
                        If Not .cells(6, 6).value.ToString.Length = 10 AndAlso (Date.FromOADate(.cells(6, 6).value).Year > 2008 And Date.FromOADate(.cells(6, 6).value).Year < 2012) Then
                            StartDate = Date.FromOADate(.cells(6, 6).value)
                        Else
                            StartDate = CDate(.cells(6, 6).value)
                        End If
                    End If
                End If

                If Not .cells(7, 5).value Is Nothing AndAlso .cells(7, 5).value.ToString.Contains("slut") Then
                    If Not .cells(7, 6).value Is Nothing Then
                        If Not .cells(7, 6).value.ToString.Length = 10 AndAlso (Date.FromOADate(.cells(7, 6).value).Year > 2008 And Date.FromOADate(.cells(7, 6).value).Year < 2012) Then
                            EndDate = Date.FromOADate(.cells(7, 6).value)
                        Else
                            EndDate = CDate(.cells(7, 6).value)
                        End If
                    End If
                End If

                c = 1
                While .Cells(Row - 1, c).value <> "Estimat" And c < 200
                    c = c + 1
                End While
                RatingCol = c
                c = 1
                While .Cells(Row - 1, c).value <> "BB/BP" And c < 200
                    c = c + 1
                End While
                PlacCol = c
                c = 1
                While .Cells(Row - 1, c).value <> "Brutto" And c < 200
                    c = c + 1
                End While
                GrossCol = c
                c = 1
                While .Cells(Row - 1, c).value <> "Netto" And c < 200
                    c = c + 1
                End While
                NetCol = c
                c = 1
                While .Cells(Row - 1, c).value <> "Anm" And c < 200
                    c = c + 1
                End While
                RemarkCol = c
                c = 1
                'different words are used in Spec and RBS
                While .Cells(Row - 1, c).value <> "Spotlängd" AndAlso .Cells(Row - 1, c).value <> "Sek" And c < 200
                    c = c + 1
                End While
                LengthCol = c
                c = 1
                While .Cells(Row - 1, c).value <> "Filmkod" And c < 200
                    c = c + 1
                End While
                FilmCol = c
                c = 1
                While .Cells(Row - 1, c).value <> "Tid o Program" And c < 200
                    c = c + 1
                End While
                ProgCol = c


                'remake the numeric columns to numerics, not strings
                'Search and replace noncharacters ""
                If NetCol = 13 Then
                    .Columns("K:M").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                    .Columns("K:M").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                    .AllCells.Replace(What:=",", Replacement:=",", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                Else
                    .Columns("J:L").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                    .Columns("J:L").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                    .AllCells.Replace(What:=",", Replacement:=",", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                End If
            End If

            Dim frmImport As New frmImport(Campaign.Channels("TV4"))
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate
            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - TV4"
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name
            If multipleSpecificsSections Then
                frmImport.chkReplace.Checked = False
                frmImport.optAddBudget.Checked = True
            End If

            If RBS Then
                frmImport.Label6.Tag = "RBS"
            ElseIf Spec Then
                frmImport.Label6.Tag = "Spec"
            ElseIf LastMinute Then
                frmImport.Label6.Tag = "Last Minute"
            ElseIf FirstMinute Then
                frmImport.Label6.Tag = "First Minute"
            Else
                frmImport.Label6.Tag = "RBS"
            End If


            Dim tmpNetBudget As Single

            'Look at the line below the last spot, in the net cost column
            If .cells(r + 1, NetCol).value Is Nothing OrElse Row = 1000 Then
                'if we cant find the budget here we need to look an the summary at the end of the booking
                Dim rs As Integer = r
                Dim summary As Boolean
                SumNetCol = 1
                While rs < 700
                    If Not .Cells(rs, 1).value Is Nothing Then
                        If .Cells(rs, 1).value.ToString.ToUpper = "SUMMA" OrElse .Cells(rs, 1).value.ToString.ToUpper = "OPTIONSTYP" Then
                            summary = True
                            While (.Cells(rs, SumNetCol).Value Is Nothing OrElse .Cells(rs, SumNetCol).Value.ToString.ToUpper <> "NETTO") AndAlso SumNetCol < 200
                                SumNetCol += 1
                            End While
                            SumGrossCol = SumNetCol - 1
                        End If
                        If summary Then
                            If .Cells(rs, 1).value.ToString.ToUpper = "RBS" Then
                                Exit While
                            End If
                        End If
                    End If
                    rs += 1
                End While
                If Not .Cells(rs, SumNetCol).value Is Nothing Then
                    Dim str As String = .cells(rs, SumNetCol).value.ToString.Replace(" ", "").ToString.Replace(",", ".").Replace(" ", "")
                    tmpNetBudget = CSng(str)
                Else
                    tmpNetBudget = 0
                End If

            Else
                tmpNetBudget = .cells(r + 1, NetCol).value.ToString.Replace(" ", "").ToString.Replace(",", ".")
            End If
            'OK now we have the total budget, for the first booking type at least

            frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            'GoTo cmdImportSchedule_Click_Error

            If frmImport.chkEvaluate.Checked Then
                showEvaluateSpecifics = True
            End If

            Idx = frmImport.txtIndex.Text 'These are variables taken from the form the user is shown
            BT = frmImport.cmbBookingType.Text

            'Delete confirmed spots from TV4 that have the appropriate date and booking type
            'Excellent place for a LINQ query
            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = "TV4" Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedBookedSpot Is Nothing Then
                                        TmpSpot.MatchedBookedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            'And now replace the confirmed budget the campaign has, if any
            If frmImport.optReplaceBudget.Checked Then
                Campaign.Channels("TV4").BookingTypes(BT).ConfirmedNetBudget = CDec(tmpNetBudget)
            Else
                Campaign.Channels("TV4").BookingTypes(BT).ConfirmedNetBudget += CDec(tmpNetBudget)
            End If
            Campaign.Channels("TV4").BookingTypes(BT).ContractNumber = .Range("B3").value

            Dim emptyRow As Integer

            While emptyRow < 9
                'read all the spots

                Dim l As Integer
                Dim fRow As Integer

                If .cells(Row, FilmCol).value Is Nothing Then
                    fRow = FilmCol + 1
                    l = 1
                Else
                    fRow = FilmCol
                    l = 5
                End If
                'Read lines while the column containing film codes still contains something
                While Not .Cells(Row, 3).value Is Nothing
                    If Not .Cells(Row, 1).value Is Nothing AndAlso InStr(.Cells(Row, 1).value, "Total") = 1 Then
                        Exit While
                    End If

                    ' While Not .Cells(Row, 3).value Is Nothing 'OrElse .Cells(Row, 1).value.ToString.Substring(0, 5) <> "Total"
                    If .cells(Row, intDateCol).value.ToString.Length = 10 Then
                        d = .Cells(Row, intDateCol).value
                    ElseIf Date.FromOADate(.cells(r, intDateCol).value).Year > 2008 And Date.FromOADate(.cells(r, intDateCol).value).Year < 2012 Then
                        d = Date.FromOADate(CInt(.cells(Row, intDateCol).value))
                    Else
                        Dim year As Integer = CInt(Left(.Cells(Row, intDateCol).value.ToString, 4))
                        Dim month As Integer = CInt(Right(Left(.Cells(Row, intDateCol).value.ToString, 6), 2))
                        Dim day As Integer = CInt(Right(.Cells(Row, intDateCol).value.ToString, 2))
                        d = DateAndTime.DateSerial(year, month, day)
                        'd = .cells(Row, intDateCol).value 'Comment this line and uncomment the next to change back to Johans version
                        'd = Left(.cells(Row, intDateCol).value, 4) & "-" & Mid(.cells(Row, intDateCol).value, 5, 2) & "-" & Right(.cells(Row, intDateCol).value, 2)
                    End If

                    TmpDate = CDate(d) 'Assign the date of this spot ..
                    If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                        TmpSpot = Campaign.PlannedSpots.Add(CreateGUID) '... but only consider adding it to the campaign if its date is within the campaign start and end
                        TmpSpot.Channel = Campaign.Channels("TV4") 'Set the channel for the spot
                        TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT) 'Set the booking type for the spot
                        TmpSpot.AirDate = TmpDate.ToOADate 'Set the date for the spot
                        .Columns(2).AutoFit()
                        t = .Cells(Row, ProgCol).Text.ToString.Substring(0, 5) 'Make the time nice and figure out the MaM of the spot
                        MaM = 60 * Val(t.Substring(0, 2)) + Val(Mid(t, 4))
                        If MaM < 120 Then MaM = MaM + 1440 'If it's 01:59 or less, add a whole day
                        TmpSpot.MaM = MaM 'Assign the MaM
                        TmpSpot.Programme = Mid(.Cells(Row, ProgCol).value, 7) 'Assign the program name
                        If TmpSpot.Programme = "" Then TmpSpot.Programme = "- No program information -"
                        fk = .Cells(Row, fRow).value 'fk means filmcode - assign it ...
                        If fk Is Nothing Then
                            fk = "" ' .. or not, if TV4 do not give that information
                        Else
                            If fk.Substring(0, 2) = "AA" OrElse fk.Substring(0, 2) = "BB" Then fk = Mid(fk, 3) 'Remove AA or BB from the filmcode
                        End If
                        TmpSpot.Filmcode = fk
                        TmpSpot.Remark = .Cells(Row, PlacCol).value 'Assign a remark, if any
                        If Not TmpSpot.Remark Is Nothing AndAlso Not PlacDict.ContainsKey(TmpSpot.Remark) Then
                            Dim frmGetAV As New frmChooseAddedValue(TmpSpot.Bookingtype, TmpSpot.Remark)
                            If frmGetAV.ShowDialog = DialogResult.OK Then
                                If frmGetAV.optSetAsOld.Checked Then
                                    If frmGetAV.cmbAV.SelectedIndex > 0 Then
                                        TmpSpot.AddedValue = TmpSpot.Bookingtype.AddedValues(frmGetAV.cmbAV.SelectedIndex)
                                    End If
                                Else
                                    With TmpSpot.Bookingtype.AddedValues.Add(frmGetAV.txtName.Text)
                                        .IndexGross = frmGetAV.txtGrossIdx.Text
                                        .IndexNet = frmGetAV.txtNetIdx.Text
                                        TmpSpot.AddedValue = TmpSpot.Bookingtype.AddedValues(.ID)
                                    End With
                                End If
                                PlacDict.Add(TmpSpot.Remark, TmpSpot.AddedValue)
                            End If
                        ElseIf Not TmpSpot.Remark Is Nothing Then
                            TmpSpot.AddedValue = PlacDict(TmpSpot.Remark)
                        End If
                        TmpSpot.SpotLength = .Cells(Row, LengthCol).value 'Just add the film length
                        If TmpSpot.Filmcode = "" Then
                            TmpSpot.Filmcode = TmpSpot.SpotLength & "s" 'If there is a length but no code, make something like "10s"
                        End If
                        TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate)) 'Set the week

                        If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then 'Add this date to collection of dates found outside campaign
                            DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                            Campaign.PlannedSpots.Remove(TmpSpot.ID) 'And remove it .. but it hasnt been added yet
                        End If
                        If Not TmpSpot.Week Is Nothing Then

                            Trinity.Helper.SetFilmForSpot(TmpSpot) 'Set the filmcode for this spot, based on ones found in the list of spots the user entered
                            TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = .Cells(Row, RatingCol).Value 'Whatever the channel estimated the spot to rate
                            'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = .Cells(Row, RatingCol).Value
                            TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = .Cells(Row, RatingCol).Value 'My estimate is set to their estimate
                            If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                'Set UI to the index towards the main target. But why? UI isnt used yet. Well let's see =)
                                UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                            Else
                                UI = 0
                            End If
                            TmpSpot.PriceNet = 0
                            TmpSpot.PriceGross = 0
                            If Not .cells(Row, NetCol).value Is Nothing Then TmpSpot.PriceNet = CDec(Trinity.Helper.RemoveNonNumbersFromString(.Cells(Row, NetCol).value.ToString))
                            If Not .cells(Row, NetCol).value Is Nothing Then TmpSpot.PriceGross = CDec(Trinity.Helper.RemoveNonNumbersFromString(.Cells(Row, GrossCol).value.ToString))
                        End If
                    End If
                    Row = Row + 1
                End While

                'OK we have now read all the spots for the first booking type and added them to the campaign
                'Row is now the same as the budget line of the first booking type

                'read the next lines and check for another chunk of spots
                Dim z As Integer
                Dim tmpStr As String = ""
                For z = 0 To 200
                    Row += 1
                    tmpStr = .Cells(Row, 1).value
                    If tmpStr Is Nothing Then
                        emptyRow += 1
                        If emptyRow = 30 Then
                            Exit While
                        End If

                    Else
                        If tmpStr.ToUpper = "SUMMA" OrElse tmpStr.ToUpper = "OPTIONSTYP" Then
                            Exit While
                        End If
                        'if we find more spots in another chunk (different headline) we redo the process, starting from the beginning
                        If Spec Then
                            If tmpStr = "RBS" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "LastMinute" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "First Minute" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "Specific" Then
                                multipleSpecificsSections = True
                                GoTo read_spots
                            End If
                        End If
                        If RBS Then
                            If tmpStr = "Specific" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "LastMinute" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "First Minute" Then
                                GoTo read_spots
                            End If
                        End If
                        If LastMinute Then
                            If tmpStr = "RBS" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "Specific" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "First Minute" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "LastMinute" Then
                                multipleSpecificsSections = True
                                GoTo read_spots
                            End If
                        End If
                        If FirstMinute Then
                            If tmpStr = "RBS" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "Specific" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "LastMinute" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "First Minute" Then
                                GoTo read_spots
                            End If
                        End If
                        emptyRow = 0
                    End If
                    If Not tmpStr Is Nothing AndAlso tmpStr.ToUpper = "FILMKOD" Then
                        Row += 1
                        Exit For
                    End If
                Next

            End While

            If showEvaluateSpecifics Then
                frmEvaluateSpecifics.MdiParent = frmMain
                frmEvaluateSpecifics.Show()
            End If
        End With
        If DateOutOfPeriod.Count > 0 Then
            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
            For i As Integer = 0 To DateOutOfPeriod.Count
                Msg += Format(Date.FromOADate(DateOutOfPeriod(i)), "Short date") & vbCrLf
            Next
            'Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        ' On Error GoTo 0
        Exit Sub


import_RBS_Budget:

        Dim intPris As Integer
        Dim strPris As String
        Dim sum As Double = 0
        Dim bolGoToRead As Boolean = False

        With WB.Sheets(1)

read_pris:

            While .Cells(Row - 1, 1).value <> "Pris" And Row < 400
                If .Cells(Row - 1, 1).value = "Filmkod" Then
                    'if we have a budget we need to add it before reading spots
                    If sum = 0 Then
                        bolGoToRead = True
                        GoTo show_budget
                    End If
                    GoTo read_spots
                End If

                Row += 1
            End While
            If Row < 400 Then
                strPris = .Cells(Row - 1, 2).value
                If IsNumeric(strPris.Trim) Then
                    intPris = CInt(strPris.Trim)
                    sum += intPris
                End If
                Row += 2
                GoTo read_pris
            End If
        End With

show_budget:
        'put up the dialog of if to replace the budget or add to the budget
        Dim frmImportRBS As New frmImport(Campaign.Channels("TV4"))
        frmImportRBS.dtFrom.Enabled = False
        frmImportRBS.dtTo.Enabled = False
        frmImportRBS.txtIndex.Text = 100
        frmImportRBS.Text = "Import Schedule - TV4"
        frmImportRBS.lblPath.Tag = WB.Path
        frmImportRBS.lblPath.Text = WB.Name
        frmImportRBS.Label6.Tag = "RBS"
        frmImportRBS.chkReplace.Enabled = False
        frmImportRBS.lblConfirmationBudget.Text = Format(CDec(sum), "N0")
        If frmImportRBS.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub ' GoTo cmdImportSchedule_Click_Error
        BT = frmImportRBS.cmbBookingType.Text

        If frmImportRBS.optReplaceBudget.Checked Then
            Campaign.Channels("TV4").BookingTypes(BT).ConfirmedNetBudget = CDec(sum)
        Else
            Campaign.Channels("TV4").BookingTypes(BT).ConfirmedNetBudget += CDec(sum)
        End If
        sum = 0
        'if we where directed here before going back to reading spots
        If bolGoToRead Then
            bolGoToRead = False
            GoTo read_spots
        End If

        Exit Sub

        'cmdImportSchedule_Click_Error:
        ''        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        '        Err.Raise(Err.Number)
    End Sub

    Private Sub ImportCanalDigitalSchedule(ByVal WB As CultureSafeExcel.Workbook)
        Dim Row As Integer
        Dim TargetRow As Integer
        Dim Target As String
        Dim Gender As String
        Dim AgeStr As String
        Dim MinAge As String
        Dim MaxAge As String
        Dim SkippedSpot As Boolean = False
        Dim r As Integer
        Dim d As String
        Dim c As Integer
        Dim t As String

        Dim showEvaluateSpecifics As Boolean = False
        Dim multipleSpecificsSections As Boolean = False

        Dim TmpDate As Date
        Dim TmpYear As String
        Dim TmpMonth As String
        Dim TmpDay As String
        Dim StartDate As Date
        Dim EndDate As Date
        Dim StartRow As Integer
        Dim Stat As String

        Dim ChanCol As Integer
        Dim DateCol As Integer
        Dim FilmCol As Integer
        Dim ProgCol As Integer
        Dim TimeCol As Integer
        Dim RatingCol As Integer
        Dim LengthCol As Integer
        Dim BreakTypeCol As Integer
        Dim RemarkCol As Integer
        Dim PriceCol As Integer
        Dim PlacCol As Integer
        Dim GrossCol As Integer
        Dim NetCol As Integer
        Dim DurCol As Integer

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpFilm As Trinity.cFilm
        Dim MaM As Integer
        Dim UI As Single
        Dim fk As String
        Dim Chan As String
        Dim PlacDict As New Dictionary(Of String, Trinity.cAddedValue)

        Dim intDateCol As Integer



        Row = 1
        Dim Bookingtypes As Integer = 1
read_spots:


        With WB.Sheets(1)
            Campaign.Channels("Canal Digital").IsVisible = True

            Campaign.Channels("Canal Digital").MainTarget.TargetName = "3+"
            Campaign.Channels("Canal Digital").MainTarget.Universe = ""


            Dim chunkBefore As Integer = Row + 1
            Dim RBS As Boolean = False
            Dim Spec As Boolean = False
            Dim LastMinute As Boolean = False
            Dim FirstMinute As Boolean = False

            'This will find the first booking type on this TV4 confirmation, set a boolean to indicate which it is and then be ready to read the spots on the first line
            'after the name of the booking type
            While .Cells(Row, 1).value <> "Filmkod" AndAlso Not InStr(.Cells(Row, 1).value, "Program") > 0 AndAlso Row < 1000

                'if it is a RBS 
                If .Cells(Row, 1).value = "RBS" Then RBS = True

                'if it is a Specific
                If .Cells(Row, 1).value = "Specific" Then Spec = True

                'if it is a last minute
                If .Cells(Row, 1).value = "LastMinute" Then LastMinute = True

                If .cells(Row, 1).value = "First Minute" Then FirstMinute = True

                'if we cant find the "filmkod" it only contains the budget
                If RBS Then
                End If

                'If RBS AndAlso Row > 100 Then
                '    If chunkBefore < 20 Then
                '        Row = 2
                '    Else
                '        Row = chunkBefore
                '    End If
                '    GoTo import_RBS_Budget
                'End If

                Row = Row + 1
            End While


            Row = Row + 1
            r = Row

            'we get the number for the date column (which column after the name of the first booking type contains "dat"
            c = 1
            While InStr(.Cells(Row - 1, c).value, "dat") < 1 And c < 200
                c = c + 1
            End While
            intDateCol = c
            'OK got the date column, but is it the same column for the RBS spots too?


            If .cells(r, intDateCol).value.ToString.Length = 10 Then
                d = CDate(.Cells(r, intDateCol).value)
            ElseIf .cells(r, intDateCol).value.ToString.Length = 8 Then
                Dim year As Integer = CInt(Left(.Cells(r, intDateCol).value.ToString, 4))
                Dim month As Integer = CInt(Right(Left(.Cells(r, intDateCol).value.ToString, 6), 2))
                Dim day As Integer = CInt(Right(.Cells(r, intDateCol).value.ToString, 2))
                d = DateAndTime.DateSerial(year, month, day)
            ElseIf Date.FromOADate(.cells(r, intDateCol).value).Year > 2008 And Date.FromOADate(.cells(r, intDateCol).value).Year < 2012 Then
                d = Date.FromOADate(.cells(r, intDateCol).value)
            Else
                d = Nothing
                'd = CDate(.cells(r, intDateCol).value) 'Comment this line and uncomment the next to change back to Johans version
                'd = CDate(Left(.cells(r, intDateCol).value, 4) & "-" & Mid(.cells(r, intDateCol).value, 5, 2) & "-" & Right(.cells(r, intDateCol).value, 2))
            End If
            'The start date of the spotlist is set to the date of the first item in the spotlist
            'Could cause an error if other spots in this list are earlier or later?
            StartDate = d
            While .cells(r, 1).value Is Nothing OrElse .Cells(r, 1).value.ToString.Length < 5 OrElse .Cells(r, 1).value.ToString.Substring(0, 5) <> "Total"
                r = r + 1
            End While
            r = r - 1
            'sometimes its a empty row after the last spot, sometimes not
            If .cells(r, intDateCol).value Is Nothing Then
                If .cells(r - 1, intDateCol).value.ToString.Length = 10 Then
                    d = CDate(.Cells(r - 1, intDateCol).value)
                ElseIf .cells(r - 1, intDateCol).value.ToString.Length = 8 Then
                    Dim year As Integer = CInt(Left(.Cells(r - 1, intDateCol).value.ToString, 4))
                    Dim month As Integer = CInt(Right(Left(.Cells(r - 1, intDateCol).value.ToString, 6), 2))
                    Dim day As Integer = CInt(Right(.Cells(r - 1, intDateCol).value.ToString, 2))
                    d = DateAndTime.DateSerial(year, month, day)
                ElseIf Date.FromOADate(.cells(r - 1, intDateCol).value).Year > 2008 And Date.FromOADate(.cells(r - 1, intDateCol).value).Year < 2012 Then
                    d = Date.FromOADate(.cells(r - 1, intDateCol).value)
                Else
                    'd = CDate(.cells(r - 1, intDateCol).value) 'Comment this line and uncomment the next to change back to Johans version
                    'd = CDate(Left(.cells(r - 1, intDateCol).value, 4) & "-" & Mid(.cells(r - 1, intDateCol).value, 5, 2) & "-" & Right(.cells(r - 1, intDateCol).value, 2))
                End If
            Else
                If .cells(r, intDateCol).value.ToString.Length = 10 Then
                    d = CDate(.Cells(r, intDateCol).value)
                ElseIf .cells(r, intDateCol).value.ToString.Length = 8 Then
                    Dim year As Integer = CInt(Left(.Cells(r, intDateCol).value.ToString, 4))
                    Dim month As Integer = CInt(Right(Left(.Cells(r, intDateCol).value.ToString, 6), 2))
                    Dim day As Integer = CInt(Right(.Cells(r, intDateCol).value.ToString, 2))
                    d = DateAndTime.DateSerial(year, month, day)
                ElseIf Date.FromOADate(.cells(r - 1, intDateCol).value).Year > 2008 And Date.FromOADate(.cells(r - 1, intDateCol).value).Year < 2012 Then
                    d = Date.FromOADate(.cells(r - 1, intDateCol).value)
                Else
                    'd = CDate(.cells(r, intDateCol).value) 'Comment this line and uncomment the next to change back to Johans version
                    'd = CDate(Left(.cells(r, intDateCol).value, 4) & "-" & Mid(.cells(r, intDateCol).value, 5, 2) & "-" & Right(.cells(r, intDateCol).value, 2))
                End If
            End If


            EndDate = d

            If Not .cells(6, 5).value Is Nothing AndAlso .cells(6, 5).value.ToString.Contains("start") Then
                If Not .cells(6, 6).value Is Nothing Then
                    If Not .cells(6, 6).value.ToString.Length = 10 AndAlso (Date.FromOADate(.cells(6, 6).value).Year > 2008 And Date.FromOADate(.cells(6, 6).value).Year < 2012) Then
                        StartDate = Date.FromOADate(.cells(6, 6).value)
                    Else
                        StartDate = CDate(.cells(6, 6).value)
                    End If
                End If
            End If

            If Not .cells(7, 5).value Is Nothing AndAlso .cells(7, 5).value.ToString.Contains("slut") Then
                If Not .cells(7, 6).value Is Nothing Then
                    If Not .cells(7, 6).value.ToString.Length = 10 AndAlso (Date.FromOADate(.cells(7, 6).value).Year > 2008 And Date.FromOADate(.cells(7, 6).value).Year < 2012) Then
                        EndDate = Date.FromOADate(.cells(7, 6).value)
                    Else
                        EndDate = CDate(.cells(7, 6).value)
                    End If
                End If
            End If

            c = 1
            While .Cells(Row - 1, c).value <> "Estimat" And c < 200
                c = c + 1
            End While
            RatingCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "BB/BP" And c < 200
                c = c + 1
            End While
            PlacCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Brutto" And c < 200
                c = c + 1
            End While
            GrossCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Netto" And c < 200
                c = c + 1
            End While
            NetCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Anm" And c < 200
                c = c + 1
            End While
            RemarkCol = c
            c = 1
            'different words are used in Spec and RBS
            While .Cells(Row - 1, c).value <> "Spotlängd" AndAlso .Cells(Row - 1, c).value <> "Sek" And c < 200
                c = c + 1
            End While
            LengthCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Filmkod" And c < 200
                c = c + 1
            End While
            FilmCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Tid o Program" And c < 200
                c = c + 1
            End While
            ProgCol = c


            'remake the numeric columns to numerics, not strings
            'Search and replace noncharacters ""
            If NetCol = 13 Then
                .Columns("K:M").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                .Columns("K:M").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                .AllCells.Replace(What:=",", Replacement:=",", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
            Else
                .Columns("J:L").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                .Columns("J:L").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                .AllCells.Replace(What:=",", Replacement:=",", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
            End If


            Dim frmImport As New frmImport(Campaign.Channels("Canal Digital"))
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate
            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - Canal Digital"
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name
            If multipleSpecificsSections Then
                frmImport.chkReplace.Checked = False
                frmImport.optAddBudget.Checked = True
            End If

            If RBS Then
                frmImport.Label6.Tag = "RBS"
            ElseIf Spec Then
                frmImport.Label6.Tag = "Spec"
            ElseIf LastMinute Then
                frmImport.Label6.Tag = "Last Minute"
            ElseIf FirstMinute Then
                frmImport.Label6.Tag = "First Minute"
            Else
                frmImport.Label6.Tag = "RBS"
            End If


            Dim tmpNetBudget As Single

            'Look at the line below the last spot, in the net cost column
            If .cells(r + 1, NetCol).value Is Nothing Then
                'if we cant find the budget here we need to look an the summary at the end of the booking
                Dim rs As Integer = r
                Dim summary As Boolean
                While rs < 600
                    If Not .Cells(rs, 1).value Is Nothing Then
                        If .Cells(rs, 1).value.ToString.ToUpper = "SUMMA" Then
                            summary = True
                        End If
                        If summary Then
                            If .Cells(rs, 1).value.ToString.ToUpper = "RBS" Then
                                Exit While
                            End If
                        End If
                    End If
                    rs += 1
                End While
                If Not .Cells(rs, 3).value Is Nothing Then
                    Dim str As String = .cells(rs, 3).value.ToString.Replace(" ", "").ToString.Replace(",", ".").Replace(" ", "")
                    tmpNetBudget = CSng(str)
                Else
                    tmpNetBudget = 0
                End If

            Else
                tmpNetBudget = .cells(r + 1, NetCol).value.ToString.Replace(" ", "").ToString.Replace(",", ".")
            End If
            'OK now we have the total budget, for the first booking type at least

            frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If

            If frmImport.chkEvaluate.Checked Then
                showEvaluateSpecifics = True
            End If

            Idx = frmImport.txtIndex.Text 'These are variables taken from the form the user is shown
            BT = frmImport.cmbBookingType.Text

            'Delete confirmed spots from TV4 that have the appropriate date and booking type
            'Excellent place for a LINQ query
            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = "Canal Digital" Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedBookedSpot Is Nothing Then
                                        TmpSpot.MatchedBookedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            'And now replace the confirmed budget the campaign has, if any
            If frmImport.optReplaceBudget.Checked Then
                Campaign.Channels("Canal Digital").BookingTypes(BT).ConfirmedNetBudget = CDec(tmpNetBudget)
            Else
                Campaign.Channels("Canal Digital").BookingTypes(BT).ConfirmedNetBudget += CDec(tmpNetBudget)
            End If
            Campaign.Channels("Canal Digital").BookingTypes(BT).ContractNumber = .Range("B3").value

            Dim emptyRow As Integer

            While emptyRow < 9
                'read all the spots

                Dim l As Integer
                Dim fRow As Integer

                If .cells(Row, FilmCol).value Is Nothing Then
                    fRow = FilmCol + 1
                    l = 1
                Else
                    fRow = FilmCol
                    l = 5
                End If
                'Read lines while the column containing film codes still contains something
                While Not .Cells(Row, 3).value Is Nothing
                    If Not .Cells(Row, 1).value Is Nothing AndAlso InStr(.Cells(Row, 1).value, "Total") = 1 Then
                        Exit While
                    End If

                    ' While Not .Cells(Row, 3).value Is Nothing 'OrElse .Cells(Row, 1).value.ToString.Substring(0, 5) <> "Total"
                    If .cells(Row, intDateCol).value.ToString.Length = 10 Then
                        d = .Cells(Row, intDateCol).value
                    ElseIf Date.FromOADate(.cells(r, intDateCol).value).Year > 2008 And Date.FromOADate(.cells(r, intDateCol).value).Year < 2012 Then
                        d = Date.FromOADate(.cells(r, intDateCol).value)
                    Else
                        Dim year As Integer = CInt(Left(.Cells(Row, intDateCol).value.ToString, 4))
                        Dim month As Integer = CInt(Right(Left(.Cells(Row, intDateCol).value.ToString, 6), 2))
                        Dim day As Integer = CInt(Right(.Cells(Row, intDateCol).value.ToString, 2))
                        d = DateAndTime.DateSerial(year, month, day)
                        'd = .cells(Row, intDateCol).value 'Comment this line and uncomment the next to change back to Johans version
                        'd = Left(.cells(Row, intDateCol).value, 4) & "-" & Mid(.cells(Row, intDateCol).value, 5, 2) & "-" & Right(.cells(Row, intDateCol).value, 2)
                    End If

                    TmpDate = CDate(d) 'Assign the date of this spot ..
                    If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                        TmpSpot = Campaign.PlannedSpots.Add(CreateGUID) '... but only consider adding it to the campaign if its date is within the campaign start and end
                        TmpSpot.Channel = Campaign.Channels("Canal Digital") 'Set the channel for the spot
                        TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT) 'Set the booking type for the spot
                        TmpSpot.AirDate = TmpDate.ToOADate 'Set the date for the spot
                        .Columns(2).AutoFit()
                        t = .Cells(Row, ProgCol).Text.ToString.Substring(0, 5) 'Make the time nice and figure out the MaM of the spot
                        MaM = 60 * Val(t.Substring(0, 2)) + Val(Mid(t, 4))
                        If MaM < 120 Then MaM = MaM + 1440 'If it's 01:59 or less, add a whole day
                        TmpSpot.MaM = MaM 'Assign the MaM
                        TmpSpot.Programme = Mid(.Cells(Row, ProgCol).value, 7) 'Assign the program name
                        fk = .Cells(Row, fRow).value 'fk means filmcode - assign it ...
                        If fk Is Nothing Then
                            fk = "" ' .. or not, if TV4 do not give that information
                        Else
                            If fk.Substring(0, 2) = "AA" OrElse fk.Substring(0, 2) = "BB" Then fk = Mid(fk, 3) 'Remove AA or BB from the filmcode
                        End If
                        TmpSpot.Filmcode = fk
                        TmpSpot.Remark = .Cells(Row, PlacCol).value 'Assign a remark, if any
                        If Not TmpSpot.Remark Is Nothing AndAlso Not PlacDict.ContainsKey(TmpSpot.Remark) Then
                            Dim frmGetAV As New frmChooseAddedValue(TmpSpot.Bookingtype, TmpSpot.Remark)
                            If frmGetAV.ShowDialog = DialogResult.OK Then
                                If frmGetAV.optSetAsOld.Checked Then
                                    If frmGetAV.cmbAV.SelectedIndex > 0 Then
                                        TmpSpot.AddedValue = TmpSpot.Bookingtype.AddedValues(frmGetAV.cmbAV.SelectedIndex)
                                    End If
                                Else
                                    With TmpSpot.Bookingtype.AddedValues.Add(frmGetAV.txtName.Text)
                                        .IndexGross = frmGetAV.txtGrossIdx.Text
                                        .IndexNet = frmGetAV.txtNetIdx.Text
                                        TmpSpot.AddedValue = TmpSpot.Bookingtype.AddedValues(.ID)
                                    End With
                                End If
                                PlacDict.Add(TmpSpot.Remark, TmpSpot.AddedValue)
                            End If
                        ElseIf Not TmpSpot.Remark Is Nothing Then
                            TmpSpot.AddedValue = PlacDict(TmpSpot.Remark)
                        End If
                        TmpSpot.SpotLength = .Cells(Row, LengthCol).value 'Just add the film length
                        If TmpSpot.Filmcode = "" Then
                            TmpSpot.Filmcode = TmpSpot.SpotLength & "s" 'If there is a length but no code, make something like "10s"
                        End If
                        TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate)) 'Set the week

                        If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then 'Add this date to collection of dates found outside campaign
                            DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                            Campaign.PlannedSpots.Remove(TmpSpot.ID) 'And remove it .. but it hasnt been added yet
                        End If
                        If Not TmpSpot.Week Is Nothing Then

                            Trinity.Helper.SetFilmForSpot(TmpSpot) 'Set the filmcode for this spot, based on ones found in the list of spots the user entered
                            TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = .Cells(Row, RatingCol).Value 'Whatever the channel estimated the spot to rate
                            'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = .Cells(Row, RatingCol).Value
                            TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = .Cells(Row, RatingCol).Value 'My estimate is set to their estimate
                            If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                'Set UI to the index towards the main target. But why? UI isnt used yet. Well let's see =)
                                UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                            Else
                                UI = 0
                            End If
                            TmpSpot.PriceNet = 0
                            TmpSpot.PriceGross = 0
                            If Not .cells(Row, NetCol).value Is Nothing Then TmpSpot.PriceNet = CDec(Trinity.Helper.RemoveNonNumbersFromString(.Cells(Row, NetCol).value.ToString))
                            If Not .cells(Row, NetCol).value Is Nothing Then TmpSpot.PriceGross = CDec(Trinity.Helper.RemoveNonNumbersFromString(.Cells(Row, GrossCol).value.ToString))
                        End If
                    End If
                    Row = Row + 1
                End While

                'OK we have now read all the spots for the first booking type and added them to the campaign
                'Row is now the same as the budget line of the first booking type

                'read the next lines and check for another chunk of spots
                Dim z As Integer
                Dim tmpStr As String = ""
                For z = 0 To 70
                    Row += 1
                    tmpStr = .Cells(Row, 1).value
                    If tmpStr Is Nothing Then
                        emptyRow += 1
                        If emptyRow = 30 Then
                            Exit While
                        End If

                    Else
                        If tmpStr.ToUpper = "SUMMA" Then
                            Exit While
                        End If
                        'if we find more spots in another chunk (different headline) we redo the process, starting from the beginning
                        If Spec Then
                            If tmpStr = "RBS" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "LastMinute" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "First Minute" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "Specific" Then
                                multipleSpecificsSections = True

                                GoTo read_spots
                            End If
                        End If
                        If RBS Then
                            If tmpStr = "Specific" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "LastMinute" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "First Minute" Then
                                GoTo read_spots
                            End If
                        End If
                        If LastMinute Then
                            If tmpStr = "RBS" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "Specific" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "First Minute" Then
                                GoTo read_spots
                            End If
                        End If
                        If FirstMinute Then
                            If tmpStr = "RBS" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "Specific" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "LastMinute" Then
                                GoTo read_spots
                            End If
                        End If
                        emptyRow = 0
                    End If
                    If Not tmpStr Is Nothing AndAlso tmpStr.ToUpper = "FILMKOD" Then
                        Row += 1
                        Exit For
                    End If
                Next

            End While

            If showEvaluateSpecifics Then
                frmEvaluateSpecifics.MdiParent = frmMain
                frmEvaluateSpecifics.Show()
            End If
        End With
        If DateOutOfPeriod.Count > 0 Then
            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
            For i As Integer = 0 To DateOutOfPeriod.Count
                Msg += Format(Date.FromOADate(DateOutOfPeriod(i)), "Short date") & vbCrLf
            Next
            'Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        On Error GoTo 0
        Exit Sub


import_RBS_Budget:

        Dim intPris As Integer
        Dim strPris As String
        Dim sum As Double = 0
        Dim bolGoToRead As Boolean = False

        With WB.Sheets(1)

read_pris:

            While .Cells(Row - 1, 1).value <> "Pris" And Row < 400
                If .Cells(Row - 1, 1).value = "Filmkod" Then
                    'if we have a budget we need to add it before reading spots
                    If sum = 0 Then
                        bolGoToRead = True
                        GoTo show_budget
                    End If
                    GoTo read_spots
                End If

                Row += 1
            End While
            If Row < 400 Then
                strPris = .Cells(Row - 1, 2).value
                If IsNumeric(strPris.Trim) Then
                    intPris = CInt(strPris.Trim)
                    sum += intPris
                End If
                Row += 2
                GoTo read_pris
            End If
        End With

show_budget:
        'put up the dialog of if to replace the budget or add to the budget
        Dim frmImportRBS As New frmImport(Campaign.Channels("Canal Digital"))
        frmImportRBS.dtFrom.Enabled = False
        frmImportRBS.dtTo.Enabled = False
        frmImportRBS.txtIndex.Text = 100
        frmImportRBS.Text = "Import Schedule - Canal Digital"
        frmImportRBS.lblPath.Tag = WB.Path
        frmImportRBS.lblPath.Text = WB.Name
        frmImportRBS.Label6.Tag = "RBS"
        frmImportRBS.chkReplace.Enabled = False
        frmImportRBS.lblConfirmationBudget.Text = Format(CDec(sum), "N0")
        If frmImportRBS.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            CleanUp()
            Exit Sub
        End If
        BT = frmImportRBS.cmbBookingType.Text

        If frmImportRBS.optReplaceBudget.Checked Then
            Campaign.Channels("Canal Digital").BookingTypes(BT).ConfirmedNetBudget = CDec(sum)
        Else
            Campaign.Channels("Canal Digital").BookingTypes(BT).ConfirmedNetBudget += CDec(sum)
        End If
        sum = 0
        'if we where directed here before going back to reading spots
        If bolGoToRead Then
            bolGoToRead = False
            GoTo read_spots
        End If

        Exit Sub

cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
    End Sub

    Private Sub ImportPlainTV4Schedule(ByVal WB As CultureSafeExcel.Workbook)
        Dim Row As Integer
        Dim TargetRow As Integer
        Dim Target As String
        Dim Gender As String
        Dim AgeStr As String
        Dim MinAge As String
        Dim MaxAge As String
        Dim SkippedSpot As Boolean = False
        Dim r As Integer
        Dim d As String
        Dim c As Integer
        Dim t As String

        Dim showEvaluateSpecifics As Boolean = False

        Dim TmpDate As Date
        Dim TmpYear As String
        Dim TmpMonth As String
        Dim TmpDay As String
        Dim StartDate As Date
        Dim EndDate As Date
        Dim StartRow As Integer
        Dim Stat As String

        Dim ChanCol As Integer
        Dim DateCol As Integer
        Dim FilmCol As Integer
        Dim ProgCol As Integer
        Dim TimeCol As Integer
        Dim RatingCol As Integer
        Dim LengthCol As Integer
        Dim BreakTypeCol As Integer
        Dim RemarkCol As Integer
        Dim PriceCol As Integer
        Dim PlacCol As Integer
        Dim GrossCol As Integer
        Dim NetCol As Integer
        Dim DurCol As Integer

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpFilm As Trinity.cFilm
        Dim MaM As Integer
        Dim UI As Single
        Dim fk As String
        Dim Chan As String
        Dim PlacDict As New Dictionary(Of String, Trinity.cAddedValue)

        Dim intDateCol As Integer

        'TV4 bekräftelse

        Row = 1
        Dim Bookingtypes As Integer = 1
read_spots:

        Bookingtypes += 1

        With WB.Sheets(1)
            Campaign.Channels("TV4").IsVisible = True

            Campaign.Channels("TV4").MainTarget.TargetName = "12-59"
            Campaign.Channels("TV4").MainTarget.Universe = ""


            Dim chunkBefore As Integer = Row + 1
            Dim RBS As Boolean = False
            Dim Spec As Boolean = False
            Dim LastMinute As Boolean = False
            'This will find the first booking type on this TV4 confirmation, set a boolean to indicate which it is and then be ready to read the spots on the first line
            'after the name of the booking type
            While .Cells(Row, 1).value <> "Filmkod" AndAlso Not InStr(.Cells(Row, 1).value, "Program") > 0

                'if it is a RBS 
                If .Cells(Row, 1).value = "RBS" Then RBS = True

                'if it is a Specific
                If .Cells(Row, 1).value = "Specific" Then Spec = True

                'if it is a last minute
                If .Cells(Row, 1).value = "LastMinute" Then LastMinute = True

                'if we cant find the "filmkod" it only contains the budget
                If RBS AndAlso Row > 100 Then
                    If chunkBefore < 20 Then
                        Row = 2
                    Else
                        Row = chunkBefore
                    End If
                    GoTo import_RBS_Budget
                End If
                Row = Row + 1
            End While


            Row = Row + 1
            r = Row

            'we get the number for the date column (which column after the name of the first booking type contains "dat"
            c = 1
            While InStr(.Cells(Row - 1, c).value, "dat") < 1 And c < 200
                c = c + 1
            End While
            intDateCol = c
            'OK got the date column, but is it the same column for the RBS spots too?

            If .cells(r, intDateCol).value.ToString.Length = 10 Then
                d = CDate(.Cells(r, intDateCol).value)
            Else
                Dim year As Integer = CInt(Left(.Cells(r, intDateCol).value.ToString, 4))
                Dim month As Integer = CInt(Right(Left(.Cells(r, intDateCol).value.ToString, 6), 2))
                Dim day As Integer = CInt(Right(.Cells(r, intDateCol).value.ToString, 2))
                d = DateAndTime.DateSerial(year, month, day)
                'd = CDate(.cells(r, intDateCol).value) 'Comment this line and uncomment the next to change back to Johans version
                'd = CDate(Left(.cells(r, intDateCol).value, 4) & "-" & Mid(.cells(r, intDateCol).value, 5, 2) & "-" & Right(.cells(r, intDateCol).value, 2))
            End If
            'The start date of the spotlist is set to the date of the first item in the spotlist
            'Could cause an error if other spots in this list are earlier or later?
            StartDate = d
            While .cells(r, 1).value Is Nothing OrElse .Cells(r, 1).value.ToString.Length < 5 OrElse .Cells(r, 1).value.ToString.Substring(0, 5) <> "Total"
                r = r + 1
            End While
            r = r - 1
            'sometimes its a empty row after the last spot, sometimes not
            If .cells(r, intDateCol).value Is Nothing Then
                If .cells(r - 1, intDateCol).value.ToString.Length = 10 Then
                    d = CDate(.Cells(r - 1, intDateCol).value)
                Else
                    Dim year As Integer = CInt(Left(.Cells(r - 1, intDateCol).value.ToString, 4))
                    Dim month As Integer = CInt(Right(Left(.Cells(r - 1, intDateCol).value.ToString, 6), 2))
                    Dim day As Integer = CInt(Right(.Cells(r - 1, intDateCol).value.ToString, 2))
                    d = DateAndTime.DateSerial(year, month, day)
                    'd = CDate(.cells(r - 1, intDateCol).value) 'Comment this line and uncomment the next to change back to Johans version
                    'd = CDate(Left(.cells(r - 1, intDateCol).value, 4) & "-" & Mid(.cells(r - 1, intDateCol).value, 5, 2) & "-" & Right(.cells(r - 1, intDateCol).value, 2))
                End If
            Else
                If .cells(r, intDateCol).value.ToString.Length = 10 Then
                    d = CDate(.Cells(r, intDateCol).value)
                Else
                    Dim year As Integer = CInt(Left(.Cells(r, intDateCol).value.ToString, 4))
                    Dim month As Integer = CInt(Right(Left(.Cells(r, intDateCol).value.ToString, 6), 2))
                    Dim day As Integer = CInt(Right(.Cells(r, intDateCol).value.ToString, 2))
                    d = DateAndTime.DateSerial(year, month, day)
                    'd = CDate(.cells(r, intDateCol).value) 'Comment this line and uncomment the next to change back to Johans version
                    'd = CDate(Left(.cells(r, intDateCol).value, 4) & "-" & Mid(.cells(r, intDateCol).value, 5, 2) & "-" & Right(.cells(r, intDateCol).value, 2))
                End If
            End If


            EndDate = d

            c = 1
            While .Cells(Row - 1, c).value <> "Estimat" And c < 200
                c = c + 1
            End While
            RatingCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "BB/BP" And c < 200
                c = c + 1
            End While
            PlacCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Brutto" And c < 200
                c = c + 1
            End While
            GrossCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Netto" And c < 200
                c = c + 1
            End While
            NetCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Anm" And c < 200
                c = c + 1
            End While
            RemarkCol = c
            c = 1
            'different words are used in Spec and RBS
            While .Cells(Row - 1, c).value <> "Spotlängd" AndAlso .Cells(Row - 1, c).value <> "Sek" And c < 200
                c = c + 1
            End While
            LengthCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Filmkod" And c < 200
                c = c + 1
            End While
            FilmCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Tid o Program" And c < 200
                c = c + 1
            End While
            ProgCol = c


            'remake the numeric columns to numerics, not strings
            'Search and replace noncharacters ""
            If NetCol = 13 Then
                .Columns("K:M").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                .Columns("K:M").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                .AllCells.Replace(What:=",", Replacement:=",", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
            Else
                .Columns("J:L").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                .Columns("J:L").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                .AllCells.Replace(What:=",", Replacement:=",", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
            End If


            Dim frmImport As New frmImport(Campaign.Channels("TV4"))
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate
            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - TV4"
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name

            If RBS Then frmImport.Label6.Tag = "RBS"
            If Spec Then frmImport.Label6.Tag = "Spec"
            If LastMinute Then frmImport.Label6.Tag = "Last Minute"

            Dim tmpNetBudget As Single

            'Look at the line below the last spot, in the net cost column
            If .cells(r + 1, NetCol).value Is Nothing Then
                'if we cant find the budget here we need to look an the summary at the end of the booking
                Dim rs As Integer = r
                Dim summary As Boolean
                While rs < 500
                    If Not .Cells(rs, 1).value Is Nothing Then
                        If .Cells(rs, 1).value.ToString.ToUpper = "SUMMA" Then
                            summary = True
                        End If
                        If summary Then
                            If .Cells(rs, 1).value.ToString.ToUpper = "RBS" Then
                                Exit While
                            End If
                        End If
                    End If
                    rs += 1
                End While
                If Not .Cells(rs, 3).value Is Nothing Then
                    Dim str As String = .cells(rs, 3).value.ToString.Replace(" ", "").ToString.Replace(",", ".").Replace(" ", "")
                    tmpNetBudget = CSng(str)
                Else
                    tmpNetBudget = 0
                End If

            Else
                tmpNetBudget = .cells(r + 1, NetCol).value.ToString.Replace(" ", "").ToString.Replace(",", ".")
            End If
            'OK now we have the total budget, for the first booking type at least

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")
            frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If

            If frmImport.chkEvaluate.Checked Then
                showEvaluateSpecifics = True
            End If

            Idx = frmImport.txtIndex.Text 'These are variables taken from the form the user is shown
            BT = frmImport.cmbBookingType.Text

            'Delete confirmed spots from TV4 that have the appropriate date and booking type
            'Excellent place for a LINQ query
            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = "TV4" Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedBookedSpot Is Nothing Then
                                        TmpSpot.MatchedBookedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            'And now replace the confirmed budget the campaign has, if any
            If frmImport.optReplaceBudget.Checked Then
                Campaign.Channels("TV4").BookingTypes(BT).ConfirmedNetBudget = CDec(tmpNetBudget)
            Else
                Campaign.Channels("TV4").BookingTypes(BT).ConfirmedNetBudget += CDec(tmpNetBudget)
            End If
            Campaign.Channels("TV4").BookingTypes(BT).ContractNumber = .Range("B3").value

            Dim emptyRow As Integer

            While emptyRow < 6
                'read all the spots

                Dim l As Integer
                Dim fRow As Integer

                If .cells(Row, FilmCol).value Is Nothing Then
                    fRow = FilmCol + 1
                    l = 1
                Else
                    fRow = FilmCol
                    l = 5
                End If
                'Read lines while the column containing film codes still contains something
                While Not .Cells(Row, 3).value Is Nothing
                    If Not .Cells(Row, 1).value Is Nothing AndAlso InStr(.Cells(Row, 1).value, "Total") = 1 Then
                        Exit While
                    End If

                    ' While Not .Cells(Row, 3).value Is Nothing 'OrElse .Cells(Row, 1).value.ToString.Substring(0, 5) <> "Total"
                    If .cells(Row, intDateCol).value.ToString.Length = 10 Then
                        d = .Cells(Row, intDateCol).value
                    Else
                        Dim year As Integer = CInt(Left(.Cells(Row, intDateCol).value.ToString, 4))
                        Dim month As Integer = CInt(Right(Left(.Cells(Row, intDateCol).value.ToString, 6), 2))
                        Dim day As Integer = CInt(Right(.Cells(Row, intDateCol).value.ToString, 2))
                        d = DateAndTime.DateSerial(year, month, day)
                        'd = .cells(Row, intDateCol).value 'Comment this line and uncomment the next to change back to Johans version
                        'd = Left(.cells(Row, intDateCol).value, 4) & "-" & Mid(.cells(Row, intDateCol).value, 5, 2) & "-" & Right(.cells(Row, intDateCol).value, 2)
                    End If

                    TmpDate = CDate(d) 'Assign the date of this spot ..
                    If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                        TmpSpot = Campaign.PlannedSpots.Add(CreateGUID) '... but only consider adding it to the campaign if its date is within the campaign start and end
                        TmpSpot.Channel = Campaign.Channels("TV4") 'Set the channel for the spot
                        TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT) 'Set the booking type for the spot
                        TmpSpot.AirDate = TmpDate.ToOADate 'Set the date for the spot
                        .Columns(2).AutoFit()
                        t = .Cells(Row, ProgCol).Text.ToString.Substring(0, 5) 'Make the time nice and figure out the MaM of the spot
                        MaM = 60 * Val(t.Substring(0, 2)) + Val(Mid(t, 4))
                        If MaM < 120 Then MaM = MaM + 1440 'If it's 01:59 or less, add a whole day
                        TmpSpot.MaM = MaM 'Assign the MaM
                        TmpSpot.Programme = Mid(.Cells(Row, ProgCol).value, 7) 'Assign the program name
                        fk = .Cells(Row, fRow).value 'fk means filmcode - assign it ...
                        If fk Is Nothing Then
                            fk = "" ' .. or not, if TV4 do not give that information
                        Else
                            If fk.Substring(0, 2) = "AA" OrElse fk.Substring(0, 2) = "BB" Then fk = Mid(fk, 3) 'Remove AA or BB from the filmcode
                        End If
                        TmpSpot.Filmcode = fk
                        TmpSpot.Remark = .Cells(Row, PlacCol).value 'Assign a remark, if any
                        If Not TmpSpot.Remark Is Nothing AndAlso Not PlacDict.ContainsKey(TmpSpot.Remark) Then
                            Dim frmGetAV As New frmChooseAddedValue(TmpSpot.Bookingtype, TmpSpot.Remark)
                            If frmGetAV.ShowDialog = DialogResult.OK Then
                                If frmGetAV.optSetAsOld.Checked Then
                                    If frmGetAV.cmbAV.SelectedIndex > 0 Then
                                        TmpSpot.AddedValue = TmpSpot.Bookingtype.AddedValues(frmGetAV.cmbAV.SelectedIndex)
                                    End If
                                Else
                                    With TmpSpot.Bookingtype.AddedValues.Add(frmGetAV.txtName.Text)
                                        .IndexGross = frmGetAV.txtGrossIdx.Text
                                        .IndexNet = frmGetAV.txtNetIdx.Text
                                        TmpSpot.AddedValue = TmpSpot.Bookingtype.AddedValues(.ID)
                                    End With
                                End If
                                PlacDict.Add(TmpSpot.Remark, TmpSpot.AddedValue)
                            End If
                        ElseIf Not TmpSpot.Remark Is Nothing Then
                            TmpSpot.AddedValue = PlacDict(TmpSpot.Remark)
                        End If
                        TmpSpot.SpotLength = .Cells(Row, LengthCol).value 'Just add the film length
                        If TmpSpot.Filmcode = "" Then
                            TmpSpot.Filmcode = TmpSpot.SpotLength & "s" 'If there is a length but no code, make something like "10s"
                        End If
                        TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate)) 'Set the week

                        If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then 'Add this date to collection of dates found outside campaign
                            DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                            Campaign.PlannedSpots.Remove(TmpSpot.ID) 'And remove it .. but it hasnt been added yet
                        End If
                        If Not TmpSpot.Week Is Nothing Then

                            Trinity.Helper.SetFilmForSpot(TmpSpot) 'Set the filmcode for this spot, based on ones found in the list of spots the user entered
                            TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = .Cells(Row, RatingCol).Value 'Whatever the channel estimated the spot to rate
                            'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = .Cells(Row, RatingCol).Value
                            TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = .Cells(Row, RatingCol).Value 'My estimate is set to their estimate
                            If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                'Set UI to the index towards the main target. But why? UI isnt used yet. Well let's see =)
                                UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                            Else
                                UI = 0
                            End If

                            TmpSpot.PriceNet = .Cells(Row, NetCol).value
                            TmpSpot.PriceGross = .Cells(Row, GrossCol).value
                        End If
                    End If
                    Row = Row + 1
                End While

                'OK we have now read all the spots for the first booking type and added them to the campaign
                'Row is now the same as the budget line of the first booking type

                'read the next lines and check for another chunk of spots
                Dim z As Integer
                Dim tmpStr As String
                For z = 0 To 30
                    Row += 1
                    tmpStr = .Cells(Row, 1).value
                    If tmpStr Is Nothing Then
                        emptyRow += 1
                        If emptyRow = 5 Then Exit While
                    Else
                        If tmpStr.ToUpper = "SUMMA" Then
                            Exit While
                        End If
                        'if we find more spots in another chunk (different headline) we redo the process, starting from the beginning
                        If Spec Then
                            If tmpStr = "RBS" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "LastMinute" Then
                                GoTo read_spots
                            End If
                        End If
                        If RBS Then
                            If tmpStr = "Specific" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "LastMinute" Then
                                GoTo read_spots
                            End If
                        End If
                        If LastMinute Then
                            If tmpStr = "RBS" Then
                                GoTo read_spots
                            End If
                            If tmpStr = "Specific" Then
                                GoTo read_spots
                            End If
                        End If
                        emptyRow = 0
                    End If
                    If Not tmpStr Is Nothing AndAlso tmpStr.ToUpper = "FILMKOD" Then
                        Row += 1
                        Exit For
                    End If
                Next

            End While

            If showEvaluateSpecifics Then
                frmEvaluateSpecifics.MdiParent = frmMain
                frmEvaluateSpecifics.Show()
            End If
        End With
        If DateOutOfPeriod.Count > 0 Then
            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
            For i As Integer = 0 To DateOutOfPeriod.Count
                Msg += Format(Date.FromOADate(DateOutOfPeriod(i)), "Short date") & vbCrLf
            Next
            Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        On Error GoTo 0
        Exit Sub


import_RBS_Budget:

        Dim intPris As Integer
        Dim strPris As String
        Dim sum As Double = 0
        Dim bolGoToRead As Boolean = False

        With WB.Sheets(1)

read_pris:

            While .Cells(Row - 1, 1).value <> "Pris" And Row < 400
                If .Cells(Row - 1, 1).value = "Filmkod" Then
                    'if we have a budget we need to add it before reading spots
                    If sum = 0 Then
                        bolGoToRead = True
                        GoTo show_budget
                    End If
                    GoTo read_spots
                End If

                Row += 1
            End While
            If Row < 400 Then
                strPris = .Cells(Row - 1, 2).value
                If IsNumeric(strPris.Trim) Then
                    intPris = CInt(strPris.Trim)
                    sum += intPris
                End If
                Row += 2
                GoTo read_pris
            End If
        End With

show_budget:
        'put up the dialog of if to replace the budget or add to the budget
        Dim frmImportRBS As New frmImport(Campaign.Channels("TV4"))
        frmImportRBS.dtFrom.Enabled = False
        frmImportRBS.dtTo.Enabled = False
        frmImportRBS.txtIndex.Text = 100
        frmImportRBS.Text = "Import Schedule - TV4"
        frmImportRBS.lblPath.Tag = WB.Path
        frmImportRBS.lblPath.Text = WB.Name
        frmImportRBS.Label6.Tag = "RBS"
        frmImportRBS.chkReplace.Enabled = False
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")
        frmImportRBS.lblConfirmationBudget.Text = Format(CDec(sum), "N0")
        If frmImportRBS.ShowDialog = Windows.Forms.DialogResult.Cancel Then GoTo cmdImportSchedule_Click_Error
        BT = frmImportRBS.cmbBookingType.Text

        If frmImportRBS.optReplaceBudget.Checked Then
            Campaign.Channels("TV4").BookingTypes(BT).ConfirmedNetBudget = CDec(sum)
        Else
            Campaign.Channels("TV4").BookingTypes(BT).ConfirmedNetBudget += CDec(sum)
        End If
        sum = 0
        'if we where directed here before going back to reading spots
        If bolGoToRead Then
            bolGoToRead = False
            GoTo read_spots
        End If

        Exit Sub

cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
    End Sub

    Private Sub ImportKanal5Schedule(ByVal WB As CultureSafeExcel.Workbook)

        Dim Row As Integer
        Dim TargetRow As Integer
        Dim Target As String
        Dim Gender As String
        Dim AgeStr As String
        Dim MinAge As String
        Dim MaxAge As String
        Dim SkippedSpot As Boolean = False
        Dim r As Integer
        Dim d As String
        Dim c As Integer
        Dim t As String

        Dim TmpDate As Date
        Dim TmpYear As String
        Dim TmpMonth As String
        Dim TmpDay As String
        Dim StartDate As Date
        Dim EndDate As Date
        Dim StartRow As Integer
        Dim Stat As String

        Dim ChanCol As Integer
        Dim DateCol As Integer
        Dim FilmCol As Integer
        Dim ProgCol As Integer
        Dim TimeCol As Integer
        Dim RatingCol As Integer
        Dim LengthCol As Integer
        Dim BreakTypeCol As Integer
        Dim RemarkCol As Integer
        Dim PriceCol As Integer
        Dim PlacCol As Integer
        Dim GrossCol As Integer
        Dim NetCol As Integer
        Dim DurCol As Integer

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpFilm As Trinity.cFilm
        Dim MaM As Integer
        Dim UI As Single
        Dim fk As String
        Dim Chan As String = "Kanal5"
        Dim PlacDict As New Dictionary(Of String, Trinity.cAddedValue)
        Dim foundLastMinute As Boolean = False

        Dim IsRegional As Boolean = False

        'Kanal5 bekräftelse

        Row = 1
        While WB.Sheets(1).cells(Row, 1).formula <> "Kanal"
            Row += 1
        End While
        While Not WB.Sheets(1).cells(Row, 1).value Is Nothing
            If Not WB.Sheets(1).cells(Row, 1).value Is Nothing AndAlso WB.Sheets(1).cells(Row, 1).value.ToString.ToUpper.Contains("STO") Then
                Chan = "Kanal 5 Sthlm"
                Exit While
            End If
            Row += 1
        End While

Read_spots:
        Dim frmImport As New frmImport(Campaign.Channels(Chan))
        Row = 1
        With WB.Sheets(1)
            'there are 2 DIFFERENT " "!!!
            .Columns("H:I").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
            .Columns("H:I").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
            .AllCells.Replace(What:=",", Replacement:=",", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)

            Campaign.Channels(Chan).IsVisible = True
            TargetRow = 1
            Dim targetCol = 6
            While .Cells(TargetRow, 5).value <> "Målgrupp:"
                TargetRow = TargetRow + 1
                If TargetRow > 30 Then
                    Exit While
                End If
            End While
            If TargetRow > 29 Then
                TargetRow = 1
                While .Cells(TargetRow, 6).value <> "Målgrupp:"
                    TargetRow = TargetRow + 1
                    If TargetRow > 30 Then
                        Exit While
                    End If
                End While
                targetCol = 7
            End If
            On Error Resume Next
            Target = .Cells(TargetRow, targetCol).Text
            If Target = "" Then
                targetCol = 8
                Target = .Cells(TargetRow, targetCol).Text
            End If

            Gender = Target.Substring(0, 1)
            AgeStr = Mid(Target, Len(Target) - 4)
            MinAge = AgeStr.Substring(0, InStr(AgeStr, "-") - 1)
            MaxAge = Mid(AgeStr, InStr(AgeStr, "-") + 1)
            On Error GoTo cmdImportSchedule_Click_Error
            Select Case Gender
                Case "W" : Gender = "W"
                Case "M" : Gender = "M"
                Case "A" : Gender = ""
            End Select
            Campaign.Channels(Chan).MainTarget.TargetName = Gender & MinAge & "-" & MaxAge
            Campaign.Channels(Chan).MainTarget.Universe = Campaign.Channels(Chan).BuyingUniverse

            Dim Datec As Integer = 1
            While .Cells(Row, Datec).formula <> "Datum"
                Row = Row + 1
                If Row > 40 Then Exit While
            End While
            If Row > 40 Then
                Row = 1
                Datec = 2
                While .Cells(Row, Datec).formula <> "Datum"
                    Row = Row + 1
                    If Row > 50 Then Exit While
                End While
            End If


            'If .cells(Row, 1).value <> "Kanal" And .cells(Row, 1).value <> "Datum" Then 
            Row = Row + 1

            r = Row
            d = .Cells(r, Datec).value
            If Asc(d.Substring(0, 1)) < 48 Or Asc(d.Substring(0, 1)) > 57 Then
                d = Mid(d, 4)
            End If
            TmpYear = Mid(d, Len(d) - 3)
            TmpDay = d.Substring(0, 2)
            TmpMonth = Mid(d, 4, 2)
            StartDate = CDate(TmpYear & "-" & TmpMonth & "-" & TmpDay)
            While .Cells(r, 1).value <> "Total"
                r = r + 1
            End While
            r = r - 1
            d = .Cells(r, Datec).value
            If Asc(d.Substring(0, 1)) < 48 Or Asc(d.Substring(0, 1)) > 57 Then
                d = Mid(d, 4)
            End If
            TmpYear = Mid(d, Len(d) - 3)
            TmpDay = d.Substring(0, 2)
            TmpMonth = Mid(d, 4, 2)
            EndDate = CDate(TmpYear & "-" & TmpMonth & "-" & TmpDay)
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate

            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - " & Chan
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name

            Dim tmpNetBudget As Single
            Dim budgetcol As Integer = 5
            Dim z As Integer = 1
            While .Cells(z, 5).value <> "Budget:"
                z = z + 1
                If z > 30 Then Exit While
            End While
            If z > 30 Then
                z = 1
                budgetcol = 6
                While .Cells(z, 6).value <> "Budget:"
                    z = z + 1
                    If z > 30 Then Exit While
                End While
            End If
            'Dom har haft lite olika ihopdragna celler så vi kollar vart budgeten ligger

            tmpNetBudget = 0 'sätter default ifall vi inte hittar budgeten
            If z < 30 Then
                'Hämar ut vilken kolumn själva siffrorna är
                While budgetcol < 15
                    If Not .cells(z, budgetcol).value Is Nothing Then
                        If Double.TryParse(.cells(z, budgetcol).value.ToString.Replace(" ", "").ToString.Replace(",", "."), tmpNetBudget) Then
                            Exit While
                        End If
                    End If
                    budgetcol += 1
                End While
            End If

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")
            frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Idx = frmImport.txtIndex.Text
            BT = frmImport.cmbBookingType.Text

            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = Chan Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedSpot Is Nothing Then
                                        TmpSpot.MatchedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            If frmImport.optReplaceBudget.Checked Then
                Campaign.Channels(Chan).BookingTypes(BT).ConfirmedNetBudget = CDec(tmpNetBudget)
            Else
                Campaign.Channels(Chan).BookingTypes(BT).ConfirmedNetBudget += CDec(tmpNetBudget)
            End If
            Campaign.Channels(Chan).BookingTypes(BT).ContractNumber = .Range("G8").value

            c = 1
            While .Cells(Row - 1, c).value <> "TRP" And c < 200
                c = c + 1
            End While
            RatingCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Pos" And c < 200 And .cells(Row - 1, c).value <> "Kommentar"
                c = c + 1
            End While
            If c = 200 Then c = -1
            RemarkCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Tid" And c < 200
                c = c + 1
            End While
            Dim tidCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Program" And c < 200
                c = c + 1
            End While
            Dim programmeCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Pris" And c < 200
                c = c + 1
            End While
            PriceCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Längd" And c < 200
                c = c + 1
            End While
            LengthCol = c

            c = 1
            While .Cells(Row - 1, c).value <> "Filmkod" And c < 200
                c = c + 1
            End While
            FilmCol = c

            While .Cells(Row, 1).value <> "Total"

                d = .Cells(Row, Datec).value
                If Asc(d.Substring(0, 1)) < 48 Or Asc(d.Substring(0, 1)) > 57 Then
                    d = Mid(d, 4)
                End If
                TmpYear = Mid(d, Len(d) - 3)
                TmpDay = d.Substring(0, 2)
                TmpMonth = Mid(d, 4, 2)
                TmpDate = TmpYear & "-" & TmpMonth & "-" & TmpDay
                If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                    TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                    TmpSpot.Channel = Campaign.Channels(Chan)

                    If RemarkCol > 0 AndAlso Not .cells(Row, RemarkCol).value Is Nothing AndAlso .cells(Row, RemarkCol).value.ToString.ToUpper = "LM" Then
                        TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes("Last Minute")
                    Else
                        TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                    End If

                    TmpSpot.AirDate = TmpDate.ToOADate
                    TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                    If TmpSpot.Week Is Nothing Then
                        SkippedSpot = True
                        Campaign.PlannedSpots.Remove(TmpSpot.ID)
                    Else
                        .Columns(tidCol).AutoFit()
                        t = .Cells(Row, tidCol).Text
                        MaM = 60 * Val(t.Substring(0, 2)) + Val(Mid(t, Len(t) - 1))
                        If MaM < 120 Then MaM = MaM + 1440
                        TmpSpot.MaM = MaM
                        TmpSpot.Programme = .Cells(Row, programmeCol).value
                        If RemarkCol > 0 Then TmpSpot.Remark = .Cells(Row, RemarkCol).value
                        If Not TmpSpot.Remark Is Nothing AndAlso Not PlacDict.ContainsKey(TmpSpot.Remark) Then
                            Dim frmGetAV As New frmChooseAddedValue(TmpSpot.Bookingtype, TmpSpot.Remark)
                            If frmGetAV.ShowDialog = DialogResult.OK Then
                                If frmGetAV.optSetAsOld.Checked Then
                                    If frmGetAV.cmbAV.SelectedIndex > 0 Then
                                        TmpSpot.AddedValue = TmpSpot.Bookingtype.AddedValues(frmGetAV.cmbAV.SelectedIndex)
                                    End If
                                Else
                                    With TmpSpot.Bookingtype.AddedValues.Add(frmGetAV.txtName.Text)
                                        .IndexGross = frmGetAV.txtGrossIdx.Text
                                        .IndexNet = frmGetAV.txtNetIdx.Text
                                        TmpSpot.AddedValue = TmpSpot.Bookingtype.AddedValues(.ID)
                                    End With
                                End If
                                PlacDict.Add(TmpSpot.Remark, TmpSpot.AddedValue)
                            End If
                        ElseIf Not TmpSpot.Remark Is Nothing Then
                            TmpSpot.AddedValue = PlacDict(TmpSpot.Remark)
                        End If
                        If TypeOf WB.Sheets(1).cells(Row, LengthCol).value Is String Then
                            Dim SplitSpotlength() As String = Strings.Split(WB.Sheets(1).Cells(Row, LengthCol).value.ToString, ":")
                            Dim Spotlength As Byte = CByte(SplitSpotlength(SplitSpotlength.Length - 1)) + 60 * CByte(SplitSpotlength(SplitSpotlength.Length - 2))

                            TmpSpot.SpotLength = Spotlength
                        Else
                            TmpSpot.SpotLength = Format(Date.FromOADate(WB.Sheets(1).Cells(Row, LengthCol).value), "ss")
                        End If

                        TmpSpot.Filmcode = .Cells(Row, FilmCol).value
                        If TmpSpot.Filmcode Is Nothing Or TmpSpot.Filmcode = "" Then
                            TmpSpot.Filmcode = TmpSpot.SpotLength & "s"
                        End If
                        If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                            DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                            Campaign.PlannedSpots.Remove(TmpSpot.ID)
                        End If
                        If Not TmpSpot.Week Is Nothing Then

                            Trinity.Helper.SetFilmForSpot(TmpSpot)

                            TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = .Cells(Row, RatingCol).Value
                            TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = .Cells(Row, RatingCol).Value * TmpSpot.Channel.MainTarget.UniIndex(Trinity.cTarget.EnumUni.uniMainCmp) * (Idx / 100) * (TmpSpot.Bookingtype.IndexMainTarget / 100)
                            If Not .Cells(Row, PriceCol).value Is Nothing Then
                                TmpSpot.PriceNet = .Cells(Row, PriceCol).value.ToString.Replace(" ", "").Replace(",", ".")
                            Else
                                TmpSpot.PriceNet = 0
                            End If

                        End If
                    End If
                End If
                Row = Row + 1
            End While

            If frmImport.chkEvaluate.Checked Then
                If Campaign.Channels(Chan).BookingTypes(BT).IsSpecific Then
                    frmEvaluateSpecifics.MdiParent = frmMain
                    frmEvaluateSpecifics.Show()
                End If
                If Campaign.Channels(Chan).BookingTypes(BT).IsRBS Then
                    frmEvaluateRBS.MdiParent = frmMain
                    frmEvaluateRBS.Show()
                End If
            End If
        End With
        If DateOutOfPeriod.Count > 0 Then
            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
            For i As Integer = 0 To DateOutOfPeriod.Count
                Msg += Format(Date.FromOADate(DateOutOfPeriod(i)), "Short date") & vbCrLf
            Next
            Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        On Error GoTo 0
        Exit Sub

cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)

    End Sub

    Private Function ViasatGetChannelList(ByVal WB As CultureSafeExcel.Workbook) As List(Of String)

        Dim RowCounter As Integer = 3
        Dim ChannelList As New List(Of String)

        With WB.Sheets(1)

            While RowCounter < 100 'and not .cells(RowCounter, 2).value Is Nothing 
                If .cells(RowCounter, 2).value IsNot Nothing AndAlso .cells(RowCounter, 2).value.GetType.FullName = "System.String" Then
                    Select Case .cells(RowCounter, 2).value
                        Case "Kanal" : ChannelList.Add(.cells(RowCounter, 3).value)
                        Case "Channel" : ChannelList.Add(.cells(RowCounter, 3).value)
                        Case "Station" : ChannelList.Add(.cells(RowCounter, 3).value)
                    End Select
                End If
                RowCounter += 1
            End While
        End With

        If ChannelList.Contains("CPT") Then ChannelList.Remove("CPT")
        Return ChannelList

    End Function

    Private Sub ViasatGetBudgets(ByVal WB As CultureSafeExcel.Workbook, ByVal Channel As String)

        Dim RowCounter As Integer = 3
        Dim Budget As Double = 0

        With WB.Sheets(1)

            While Not .cells(RowCounter, 2).value Is Nothing
                If .cells(RowCounter, 2).value.ToString.Contains("Budget") Then
                    If .cells(RowCounter, 2).value.ToString.Contains(Channel) Then
                        Try
                            Budget = Replace(.cells(RowCounter, 3).value, ",", ".")
                            ViasatBudgets.Add(Budget, ViasatMatchToCampaignChannel(Channel))
                        Catch
                        End Try
                    Else
                        Budget = Replace(.cells(RowCounter, 3).value, ",", ".")
                        Dim RowCounter2 As Integer = RowCounter
                        While Not .cells(RowCounter2, 2).Text.ToString.Contains("By Channel") And RowCounter2 < 1500
                            RowCounter2 += 1
                        End While
                        While .cells(RowCounter2, 2).Text <> ""
                            If .cells(RowCounter2, 2).value.ToString.Contains(Channel) Then
                                Dim Percent As Double = Replace(.cells(RowCounter2, 5).value, ",", ".")
                                Try
                                    ViasatBudgets.Add(Budget * Percent, ViasatMatchToCampaignChannel(Channel))
                                Catch
                                End Try
                            End If
                            RowCounter2 += 1
                        End While
                    End If
                End If
                RowCounter += 1
            End While
        End With

    End Sub

    Private Function ViasatGetTarget(ByVal WB As CultureSafeExcel.Workbook) As String

        Dim RowCounter As Integer = 3
        Dim Gender As String
        Dim AgeStr As String
        Dim MinAge As String
        Dim MaxAge As String

        Dim Target As String

        With WB.Sheets(1)

            While Trim(.Cells(RowCounter, 2).value) <> "Målgrupp" And RowCounter < 500 And Trim(.Cells(RowCounter, 2).value) <> "Audience"
                RowCounter += 1
            End While

            If RowCounter = 500 Then
                Return "3+"
            Else

                Target = .Cells(RowCounter, 3).value
                Target = Target.Replace(" ", "")

                If IsNumeric(Target.Substring(0, 1)) Then
                    Gender = "A"
                    AgeStr = Target
                Else
                    If Target.Substring(0, 3) = "ALL" Then
                        Gender = Target.Substring(0, 1)
                        AgeStr = Target.Substring(3)
                    Else
                        Gender = Target.Substring(0, 1)
                        AgeStr = Target.Substring(1)
                    End If


                End If

                If Not InStr(AgeStr, "-") Then
                    MinAge = AgeStr
                    MaxAge = AgeStr
                End If

                If InStr(AgeStr, "-") > 1 Then
                    MinAge = AgeStr.Substring(0, InStr(AgeStr, "-") - 1)
                    MaxAge = Mid(AgeStr, InStr(AgeStr, "-") + 1)
                ElseIf Not InStr(AgeStr, "-") Then
                    MinAge = AgeStr
                    MaxAge = AgeStr
                Else
                    MinAge = AgeStr.Substring(0, InStr(AgeStr, ".") - 1)
                    MaxAge = Mid(AgeStr, InStr(AgeStr, ".") + 1)
                End If
                Select Case Gender
                    Case "FEM" : Gender = "W"
                    Case "MEN" : Gender = "M"
                    Case Else : Gender = ""

                End Select
                If InStr(AgeStr, "-") Then
                    Target = Gender & Trim(MinAge) & "-" & Trim(MaxAge)
                    'Campaign.Channels(Stat).MainTarget.TargetName = Gender & Trim(MinAge) & "-" & Trim(MaxAge)
                    'Campaign.Channels(Stat).MainTarget.Universe = Campaign.Channels(Stat).BuyingUniverse
                Else
                    Target = Gender & Trim(MinAge) & "+"
                    'Campaign.Channels(Stat).MainTarget.TargetName = Gender & Trim(MinAge) & "+"
                    'Campaign.Channels(Stat).MainTarget.Universe = Campaign.Channels(Stat).BuyingUniverse
                End If
            End If
        End With

        Return Target

    End Function

    Private Function ViasatMatchToCampaignChannel(ByVal Channel As String) As String

        Select Case Channel

            Case Is = "MA"
                Return "TV3REG Mitt"
            Case Is = "SS"
                Return "TV3REG Sthlm"
            Case Is = "SE"
                Return "TV3REG Öst"
            Case Is = "Norr"
                Return "TV3REG Norr"
            Case Is = "Mitt"
                Return "MT"
            Case Is = "Öst"
                Return "Ö"
            Case Is = "Väst"
                Return "V"
            Case Is = "Syd"
                Return "S"
            Case Is = "Sthlm"
                Return "SM"
            Case Is = "Stockholm"
                Return "TV3REG Sthlm"
            Case Is = "SS"
                Return "TV3REG Sthlm"
            Case Is = "SE"
                Return "TV3REG Öst"
            Case Is = "Mitt"
                Return "TV3REG Mitt"
            Case Is = "MA"
                Return "TV3REG Mitt"
            Case Is = "Öst"
                Return "TV3REG Öst"
            Case Is = "Väst"
                Return "TV3REG Väst"
            Case Is = "Syd"
                Return "TV3REG Syd"
            Case Is = "Norr"
                Return "TV3REG Norr"
            Case Is = "3 Plus Denmark"
                Return "3+"
            Case Is = "Viasat 4 Norway"
                Return "Viasat 4"
            Case Is = "TV3 Sweden"
                Return "TV3"
            Case Is = "TV6 Sweden"
                Return "TV6"
            Case Is = "TV8 Sweden"
                Return "TV8"
            Case Is = "S8"
                Return "TV8"
            Case Is = "TV8"
                Return "TV8"
            Case Is = "TV3 NORWAY"
                Return "TV3"
            Case Is = "V4 NORWAY"
                Return "Viasat 4"
            Case Is = "Viasat 4 Norway"
                Return "Viasat 4"
            Case Is = "TV3 Norway"
                Return "TV3"
            Case Is = "TV3 Puls"
                Return "TV3 PULS"
            Case Is = "TV3 Denmark"
                Return "TV3"
            Case Is = "TV10 Sweden"
                Return "TV10"
            Case Else
                Return Nothing

        End Select

        Return Nothing

    End Function

    Private Sub ViasatGetColumns(ByVal WB As CultureSafeExcel.Workbook)

        Dim rowCounter As Integer = 3
        Dim columnCounter As Integer = 2

        With WB.Sheets(1)

            While .cells(rowCounter, 2).Text <> "Week" AndAlso .cells(rowCounter, 2).Text <> "Vecka" AndAlso .cells(rowCounter, 2).Text <> "Week No" AndAlso .cells(rowCounter, 2).Text <> "Week Number"
                rowCounter += 1
            End While

            While Not .cells(rowCounter, columnCounter).value Is Nothing

                Try
                    Select Case Trim(.Cells(rowCounter, columnCounter).value)
                        Case "Kanal" : ViasatColumns.Add(columnCounter, "ChanCol")
                        Case "Datum" : ViasatColumns.Add(columnCounter, "DateCol")
                        Case "Dataum" : ViasatColumns.Add(columnCounter, "DateCol")
                        Case "Filmkod" : ViasatColumns.Add(columnCounter, "FilmCol")
                        Case "Program" : ViasatColumns.Add(columnCounter, "ProgCol")
                        Case "Tid" : ViasatColumns.Add(columnCounter, "TimeCol")
                        Case "TRPs" : ViasatColumns.Add(columnCounter, "RatingCol")
                        Case "längd" : ViasatColumns.Add(columnCounter, "LengthCol")
                        Case "Spot längd" : ViasatColumns.Add(columnCounter, "LengthCol")
                        Case "Length" : ViasatColumns.Add(columnCounter, "LengthCol")
                        Case "Break Typ" : ViasatColumns.Add(columnCounter, "BreakTypeCol")
                        Case "Break Posn." : ViasatColumns.Add(columnCounter, "RemarkCol")
                        Case "Channel" : ViasatColumns.Add(columnCounter, "ChanCol")
                        Case "Date" : ViasatColumns.Add(columnCounter, "DateCol")
                        Case "Industry Code" : ViasatColumns.Add(columnCounter, "FilmCol")
                        Case "Program" : ViasatColumns.Add(columnCounter, "ProgCol")
                        Case "Programme" : ViasatColumns.Add(columnCounter, "ProgCol")
                        Case "Time" : ViasatColumns.Add(columnCounter, "TimeCol")
                        Case "TRPs" : ViasatColumns.Add(columnCounter, "RatingCol")
                        Case "Spot Length" : ViasatColumns.Add(columnCounter, "LengthCol")
                        Case "Pris Net" : ViasatColumns.Add(columnCounter, "PriceCol")
                        Case "Scheduled Date" : ViasatColumns.Add(columnCounter, "DateCol")
                        Case "Scheduled Time" : ViasatColumns.Add(columnCounter, "TimeCol")
                        Case "Break Sales Area" : ViasatColumns.Add(columnCounter, "ChanCol")
                        Case "Program/Category" : ViasatColumns.Add(columnCounter, "ProgCol")
                        Case "Ratings" : ViasatColumns.Add(columnCounter, "RatingCol")
                    End Select
                Catch
                End Try
                columnCounter += 1
            End While

        End With
    End Sub

    Private Function ViasatGetStartDate(ByVal WB As CultureSafeExcel.Workbook) As Date

        Dim rowCounter As Integer = 9
        Dim d, tmpYear, tmpDay, TmpMonth As String
        Dim StartDate As Date

        With WB.Sheets(1)


            While Not .cells(rowCounter, 2).value Is Nothing AndAlso Not .cells(rowCounter, 2).value.ToString.ToUpper.Contains("START")
                rowCounter += 1
            End While

            d = .Cells(rowCounter, 3).value
            tmpYear = Mid(d, Len(d) - 3)
            tmpDay = d.Substring(0, 2)
            TmpMonth = Mid(d, 4, 2)
            StartDate = CDate(tmpYear & "-" & TmpMonth & "-" & tmpDay)

        End With

        Return StartDate

    End Function

    Private Function ViasatGetEndDate(ByVal WB As CultureSafeExcel.Workbook) As Date

        Dim rowCounter As Integer = 9
        Dim d, tmpYear, tmpDay, TmpMonth As String
        Dim EndDate As Date

        With WB.Sheets(1)

            While Not .cells(rowCounter, 2).value Is Nothing AndAlso Not .cells(rowCounter, 2).value.ToString.ToUpper.Contains("SLUT") AndAlso Not .cells(rowCounter, 2).value.ToString.ToUpper.Contains("END")
                rowCounter += 1
            End While
            d = .Cells(rowCounter, 3).value
            tmpYear = Mid(d, Len(d) - 3)
            tmpDay = d.Substring(0, 2)
            TmpMonth = Mid(d, 4, 2)
            EndDate = CDate(tmpYear & "-" & TmpMonth & "-" & tmpDay)

        End With

        Return EndDate

    End Function

    Private Sub ViasatShowImportWindow(ByVal WB As CultureSafeExcel.Workbook, ByVal Chan As String)

        Dim Idx, BT As String

        frmImport = New frmImport(Campaign.Channels(Chan))
        frmImport.dtFrom.Value = ViasatGetStartDate(WB)
        frmImport.dtTo.Value = ViasatGetEndDate(WB)

        frmImport.txtIndex.Text = 100

        frmImport.Text = "Import Schedule - " & Chan
        frmImport.lblPath.Tag = WB.Path
        frmImport.lblPath.Text = WB.Name
        frmImport.Tag = Chan

        Try
            frmImport.lblConfirmationBudget.Text = Format(ViasatBudgets(Chan), "N0")
        Catch ex As Exception
            frmImport.lblConfirmationBudget.Text = ""
            Debug.Print("No budget was found on this break list")
        End Try

        frmImport.ShowDialog()

        Idx = frmImport.txtIndex.Text
        BT = frmImport.cmbBookingType.Text
        If frmImport.chkReplace.Checked Then
            For Each TmpSpot As Trinity.cPlannedSpot In Campaign.PlannedSpots
                If TmpSpot.Channel.ChannelName = Chan Then
                    If TmpSpot.Bookingtype.Name = BT Then
                        If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                            If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                If Not TmpSpot.MatchedSpot Is Nothing Then
                                    TmpSpot.MatchedSpot.MatchedSpot = Nothing
                                End If
                                Campaign.PlannedSpots.Remove(TmpSpot.ID)
                            End If
                        End If
                    End If
                End If
            Next
        End If

        Try
            If frmImport.optReplaceBudget.Checked Then
                Campaign.Channels(Chan).BookingTypes(BT).ConfirmedNetBudget = ViasatBudgets(Chan)
            Else
                Campaign.Channels(Chan).BookingTypes(BT).ConfirmedNetBudget += ViasatBudgets(Chan)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ViasatReadSpots(ByVal WB As CultureSafeExcel.Workbook, ByVal StartDate As Date, ByVal EndDate As Date)

        Dim rowCounter As Integer = 3
        Dim d, TmpYear, TmpDay, TmpMonth, Chan, t, fk As String
        Dim LastChannel As String = "Not Set"
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpDate As Date
        Dim SkippedSpot, Remarks, RatingCol As Boolean
        Dim MaM As Integer
        Dim PlacDict As New Dictionary(Of String, Trinity.cAddedValue)
        Dim UI As Double
        Dim Shown As New Collection

        With WB.Sheets(1)

            While .cells(rowCounter, 2).Text <> "Week" AndAlso .cells(rowCounter, 2).Text <> "Vecka" AndAlso .cells(rowCounter, 2).Text <> "Week No" AndAlso .cells(rowCounter, 2).Text <> "Week Number"
                rowCounter += 1
            End While
            rowCounter += 1

            Try
                If ViasatColumns("RemarkCol") > 0 Then
                    Debug.Print("Has remarks column")
                    Remarks = True
                End If
            Catch
                Debug.Print("Does NOT have a remarks column")
                Remarks = False
            End Try
            Try
                If ViasatColumns("RatingCol") > 0 Then
                    Debug.Print("Has ratings column")
                    RatingCol = True
                End If
            Catch
                Debug.Print("Does NOT have a ratings column")
                RatingCol = False
            End Try

            Try
                While .Cells(rowCounter, 2).Text <> "" AndAlso .Cells(rowCounter, 2).Text <> "Totals:" AndAlso rowCounter < 10000

                    Chan = ViasatMatchToCampaignChannel(.cells(rowCounter, ViasatColumns("ChanCol")).Text)
                    If LastChannel <> Chan And Not Shown.Contains(Chan) Then
                        Shown.Add(True, Chan)
                        ViasatShowImportWindow(WB, Chan)
                    End If
                    LastChannel = Chan


                    If .Cells(rowCounter, 2).value.ToString <> "" Then

                        d = .Cells(rowCounter, ViasatColumns("DateCol")).value
                        TmpYear = Mid(d, Len(d) - 3)
                        TmpDay = d.Substring(0, 2)
                        TmpMonth = Mid(d, 4, 2)
                        TmpDate = TmpYear & "-" & TmpMonth & "-" & TmpDay


                        If TmpDate >= StartDate And TmpDate <= EndDate Then
                            If TmpDate >= Date.FromOADate(Campaign.StartDate) And TmpDate <= Date.FromOADate(Campaign.EndDate) Then
                                TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                                TmpSpot.Channel = Campaign.Channels(Chan)
                                TmpSpot.AirDate = TmpDate.ToOADate
                                TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(frmImport.cmbBookingType.Text)
                                TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                                If TmpSpot.Week Is Nothing Then
                                    SkippedSpot = True
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                Else
                                    t = .Cells(rowCounter, ViasatColumns("TimeCol")).Text
                                    MaM = 60 * Val(t.Substring(0, 2)) + Val(Mid(t, InStr(t, ":") + 1))
                                    If MaM < 120 Then MaM = MaM + 1440
                                    TmpSpot.MaM = MaM
                                    TmpSpot.Programme = .Cells(rowCounter, ViasatColumns("ProgCol")).value
                                    If ViasatColumns.Contains("FilmCol") Then
                                        fk = .Cells(rowCounter, ViasatColumns("FilmCol")).value
                                    Else
                                        fk = ""
                                    End If

                                    If fk <> "" Then
                                        If Mid(fk, Len(fk) - 1) = "-S" Then
                                            fk = fk.Substring(0, Len(fk) - 2)
                                        ElseIf Mid(fk, Len(fk) - 3) = "-S/2" Then
                                            fk = fk.Substring(0, Len(fk) - 4)
                                        End If
                                    End If
                                    TmpSpot.Filmcode = fk
                                    TmpSpot.SpotLength = .Cells(rowCounter, ViasatColumns("LengthCol")).value

                                    If fk Is Nothing Or fk = "" Then
                                        TmpSpot.Filmcode = TmpSpot.SpotLength
                                    End If
                                    If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                                        DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                                        Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                    End If
                                    If Not TmpSpot.Week Is Nothing Then

                                        Trinity.Helper.SetFilmForSpot(TmpSpot)

                                        'TmpSpot.Film = TmpSpot.Week.Films(TmpSpot.Filmcode)

                                        If Remarks Then TmpSpot.Remark = .Cells(rowCounter, ViasatColumns("RemarkCol")).value

                                        If Not TmpSpot.Remark Is Nothing AndAlso Not PlacDict.ContainsKey(TmpSpot.Remark) Then
                                            Dim frmGetAV As New frmChooseAddedValue(TmpSpot.Bookingtype, TmpSpot.Remark)
                                            If frmGetAV.ShowDialog = DialogResult.OK Then
                                                If frmGetAV.optSetAsOld.Checked Then
                                                    If frmGetAV.cmbAV.SelectedIndex > 0 Then
                                                        TmpSpot.AddedValue = TmpSpot.Bookingtype.AddedValues(frmGetAV.cmbAV.SelectedIndex)
                                                    End If
                                                Else
                                                    With TmpSpot.Bookingtype.AddedValues.Add(frmGetAV.txtName.Text)
                                                        .IndexGross = frmGetAV.txtGrossIdx.Text
                                                        .IndexNet = frmGetAV.txtNetIdx.Text
                                                        TmpSpot.AddedValue = TmpSpot.Bookingtype.AddedValues(.ID)
                                                    End With
                                                End If
                                                PlacDict.Add(TmpSpot.Remark, TmpSpot.AddedValue)
                                            End If
                                        ElseIf Not TmpSpot.Remark Is Nothing Then
                                            TmpSpot.AddedValue = PlacDict(TmpSpot.Remark)
                                        End If

                                        If RatingCol Then
                                            TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = .Cells(rowCounter, ViasatColumns("RatingCol")).Value
                                            TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = .Cells(rowCounter, ViasatColumns("RatingCol")).Value * TmpSpot.Channel.MainTarget.UniIndex(Trinity.cTarget.EnumUni.uniMainCmp) * (CDec(frmImport.txtIndex.Text) / 100) * (TmpSpot.Bookingtype.IndexMainTarget / 100)
                                            UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                                            If UI > 0 Then
                                                TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = TmpSpot.MyRating / UI
                                            Else
                                                TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                                            End If
                                        End If

                                    End If
                                End If
                            Else
                                DateOutOfPeriod.Add(DateOutOfPeriod.Count, d)
                            End If
                        End If
                        rowCounter += 1
                    End If

                End While
            Catch ex As Exception
                MessageBox.Show("Failed on row " & rowCounter)
                rowCounter += 1
            End Try

        End With
        If DateOutOfPeriod.Count > 0 Then
            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
            For i As Integer = 0 To DateOutOfPeriod.Count
                Msg += Format(DateOutOfPeriod(i), "Short date") & vbCrLf
            Next
            Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub ImportViasatSchedule(ByVal WB As CultureSafeExcel.Workbook)

        'frmImport = New frmImport

        Dim rowCounter As Integer = 0
        Dim useAllAdults As Boolean = False
        Dim targetGroup As String
        Dim ChannelList As List(Of String) = ViasatGetChannelList(WB)
        Dim BudgetList As New Collection
        ViasatColumns = New Collection
        ViasatBudgets = New Collection

        For Each tmpChannel As String In ChannelList
            ViasatGetBudgets(WB, tmpChannel)
        Next

        targetGroup = ViasatGetTarget(WB)
        ViasatGetColumns(WB)
        ViasatReadSpots(WB, ViasatGetStartDate(WB), ViasatGetEndDate(WB))

        If frmImport.chkEvaluate.Checked Then
            frmEvaluateSpecifics.MdiParent = frmMain
            frmEvaluateSpecifics.Show()
        End If

    End Sub

    Private Sub ImportTV4FaktaSchedule(ByVal WB As CultureSafeExcel.Workbook)
        Dim Row As Integer
        Dim TargetRow As Integer
        Dim Target As String
        Dim Gender As String
        Dim AgeStr As String
        Dim MinAge As String
        Dim MaxAge As String
        Dim SkippedSpot As Boolean = False
        Dim r As Integer
        Dim d As String
        Dim c As Integer
        Dim t As String

        Dim TmpDate As Date
        Dim TmpYear As String
        Dim TmpMonth As String
        Dim TmpDay As String
        Dim StartDate As Date
        Dim EndDate As Date
        Dim StartRow As Integer
        Dim Stat As String

        Dim ChanCol As Integer
        Dim DateCol As Integer
        Dim FilmCol As Integer
        Dim ProgCol As Integer
        Dim TimeCol As Integer
        Dim RatingCol As Integer
        Dim LengthCol As Integer
        Dim BreakTypeCol As Integer
        Dim RemarkCol As Integer
        Dim PriceCol As Integer
        Dim PlacCol As Integer
        Dim GrossCol As Integer
        Dim NetCol As Integer
        Dim DurCol As Integer

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpFilm As Trinity.cFilm
        Dim MaM As Integer
        Dim UI As Single
        Dim fk As String
        Dim Chan As String

        'TV4 Fakta

        Row = 1
        With WB.Sheets(1)
            Campaign.Channels("TV4 Fakta").IsVisible = True

            Campaign.Channels("TV4 Fakta").MainTarget.TargetName = Campaign.Channels("TV4 Fakta").BookingTypes("RBS").BuyingTarget.Target.TargetName
            Campaign.Channels("TV4 Fakta").MainTarget.Universe = Campaign.Channels("TV4 Fakta").BuyingUniverse

            r = 3
            d = CDate(.Cells(r, 3).value)
            StartDate = d
            While Not .Cells(r, 1).value Is Nothing AndAlso .Cells(r, 1).value.ToString <> ""
                r = r + 1
            End While
            While .Cells(r, 3).value Is Nothing OrElse .Cells(r, 3).value.ToString = ""
                r = r - 1
            End While
            d = CDate(.Cells(r, 3).value)
            EndDate = d
            Dim frmImport As New frmImport(Campaign.Channels("TV4 Fakta"))
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate

            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - TV4 Fakta"
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name
            Dim tmpNetBudget As Single
            tmpNetBudget = 0
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")
            frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Idx = frmImport.txtIndex.Text
            BT = frmImport.cmbBookingType.Text

            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = "TV4 Fakta" Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedSpot Is Nothing Then
                                        TmpSpot.MatchedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            Campaign.Channels("TV4 Fakta").BookingTypes(BT).ConfirmedNetBudget = 0
            Campaign.Channels("TV4 Fakta").BookingTypes(BT).ContractNumber = ""
            c = 1
            Row = 3
            While Not .Cells(Row, 1).value Is Nothing AndAlso .Cells(Row, 1).value.ToString <> ""
                d = .Cells(Row, 3).value
                If Not d Is Nothing AndAlso Not d = "" Then
                    TmpDate = CDate(d)
                End If
                If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                    TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                    TmpSpot.Channel = Campaign.Channels("TV4 Fakta")
                    TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                    TmpSpot.AirDate = TmpDate.ToOADate
                    t = .Cells(Row, 4).Text
                    Dim temp As Integer
                    Dim temp2 As Integer
                    MaM = 60 * Val(t.Substring(0, 2)) + Val(Mid(t, 4, 2))
                    If MaM < 120 Then MaM = MaM + 1440
                    TmpSpot.MaM = MaM
                    TmpSpot.Programme = .Cells(Row, 5).value
                    fk = .Cells(Row, 1).value
                    If fk.Substring(0, 2) = "AA" Or fk.Substring(0, 2) = "BB" Then
                        fk = Mid(fk, 3)
                    End If
                    TmpSpot.Filmcode = fk
                    TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                    If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                        DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                        Campaign.PlannedSpots.Remove(TmpSpot.ID)
                    End If
                    If Not TmpSpot.Week Is Nothing Then

                        Trinity.Helper.SetFilmForSpot(TmpSpot)

                        TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                        'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = 0
                        TmpSpot.MyRating = 0
                        'If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                        '    UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                        'Else
                        '    UI = 0
                        'End If
                        'If UI > 0 Then
                        '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = TmpSpot.MyRating / UI
                        'Else
                        '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                        'End If
                        TmpSpot.PriceNet = 0
                        TmpSpot.PriceGross = 0
                    End If
                End If
                Row = Row + 1
            End While
        End With


        On Error GoTo 0
        Exit Sub

cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
    End Sub

    Sub ImportTV4Nisch(ByVal WB As CultureSafeExcel.Workbook)
        Dim Row As Integer
        Dim TargetRow As Integer
        Dim Target As String
        Dim Gender As String
        Dim AgeStr As String
        Dim MinAge As String
        Dim MaxAge As String
        Dim SkippedSpot As Boolean = False
        Dim r As Integer
        Dim d As String
        Dim c As Integer
        Dim t As String

        Dim TmpDate As Date
        Dim TmpYear As String
        Dim TmpMonth As String
        Dim TmpDay As String
        Dim StartDate As Date
        Dim EndDate As Date
        Dim StartRow As Integer
        Dim Stat As String

        Dim ChanCol As Integer
        Dim DateCol As Integer
        Dim FilmCol As Integer
        Dim ProgCol As Integer
        Dim TimeCol As Integer
        Dim RatingCol As Integer
        Dim LengthCol As Integer
        Dim BreakTypeCol As Integer
        Dim RemarkCol As Integer
        Dim PriceCol As Integer
        Dim PlacCol As Integer
        Dim GrossCol As Integer
        Dim NetCol As Integer
        Dim DurCol As Integer

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpFilm As Trinity.cFilm
        Dim MaM As Integer
        Dim UI As Single
        Dim fk As String
        Dim Chan As String

        Dim a As Integer
        Dim en As Long
        Dim ed As String
        Dim emptyRow As Integer = 0

        Dim intDateCol As Integer

        Dim RBSBudget As Double = 0
        Dim SpecificsBudget As Double = 0


        Row = 3

read_Spots:

        With WB.Sheets(1)

            If .range("A2").value.ToString.Contains("TV400") OrElse .range("A2").value.ToString.Contains("TV11") Then
                If Campaign.Channels("TV400") Is Nothing Then
                    Stat = "TV11"
                Else
                    Stat = "TV400"
                End If
            ElseIf .range("A2").value.ToString.Contains("TV4 Sport") Then
                Stat = "TV4 Sport"
            ElseIf .range("A2").value.ToString.Contains("TV4 Fakta") Then
                Stat = "TV4 Fakta"
            ElseIf .range("A2").value.ToString.Contains("Sjuan") OrElse .range("A2").value.ToString.Contains("TV4+") Then
                If Campaign.Channels("TV4+") Is Nothing Then
                    Stat = "Sjuan"
                Else
                    Stat = "TV4+"
                End If
            Else
                Stat = "TV4+"
            End If

            Campaign.Channels(Stat).IsVisible = True

            Campaign.Channels(Stat).MainTarget.TargetName = "12-59"
            Campaign.Channels(Stat).MainTarget.Universe = ""

            Dim chunkBefore As Integer = Row + 1

            Dim RBS As Boolean = False
            Dim Spec As Boolean = False
            While .Cells(Row, 1).value <> "Filmkod" AndAlso Not InStr(.Cells(Row, 1).value, "Program") > 0 AndAlso Not InStr(.Cells(Row, 1).value, "datu", Microsoft.VisualBasic.CompareMethod.Text) > 0 AndAlso Not InStr(.cells(Row, 1).value, "Id") > 0

                'if it is a RBS 
                If .Cells(Row, 1).value = "RBS" Then RBS = True

                'if it is a Specific
                If .Cells(Row, 1).value = "Specific" Then Spec = True

                'if we cant find the "filmkod" it only contains the budget
                'If RBS AndAlso Row > 100 Then
                '    If chunkBefore < 20 Then
                '        Row = 2
                '    Else
                '        Row = chunkBefore
                '    End If
                '    GoTo import_RBS_Budget
                'End If
                Row = Row + 1
            End While

            Row = Row + 1
            r = Row

            If Not RBS AndAlso Not Spec Then
                RBS = True
            End If

            'we get the number for the date column
            c = 1
            While InStr(.Cells(Row - 1, c).value, "dat", Microsoft.VisualBasic.CompareMethod.Text) < 1 And c < 200
                c = c + 1
            End While
            intDateCol = c

            If Not .cells(6, 5).value Is Nothing AndAlso .cells(6, 5).value.ToString.Contains("start") Then
                If .cells(6, 6).value.ToString.Length = 10 Then
                    d = CDate(.Cells(6, 6).value)
                Else
                    d = CDate(Left(.cells(6, 6).value, 4) & "-" & Mid(.cells(6, 6).value, 5, 2) & "-" & Right(.cells(6, 6).value, 2))
                End If
            Else
                If .cells(r, intDateCol).value.ToString.Length = 10 Then
                    d = CDate(.Cells(r, intDateCol).value)
                Else
                    d = CDate(Left(.cells(r, intDateCol).value, 4) & "-" & Mid(.cells(r, intDateCol).value, 5, 2) & "-" & Right(.cells(r, intDateCol).value, 2))
                End If

            End If

            Dim firstRowOfSpots As Integer = Row
            Dim emptyRowCounter As Integer = 0
            Dim rowCounter As Integer = Row
            While emptyRowCounter < 100
                If .cells(rowCounter, intDateCol).value Is Nothing Then
                    emptyRowCounter += 1
                Else
                    If .cells(rowCounter, intDateCol).value.ToString.Length = 10 AndAlso .cells(rowCounter, intDateCol).value.ToString.Contains("-") Then
                        emptyRowCounter = 0
                        If CDate(.cells(rowCounter, intDateCol).value) < d Then
                            d = CDate(.cells(rowCounter, intDateCol).value)
                        End If
                        ' ElseIf .cells(rowCounter, intDateCol).value.GetType.FullName = "System.Double" Then


                    End If

                End If
                rowCounter += 1
            End While

            StartDate = d




            'if there is no filmcode use the duration instead
            If .cells(r, 1).value Is Nothing Then
                While Not .cells(r, 2).value Is Nothing
                    r = r + 1
                End While
            Else
                While Not .cells(r, 1).value Is Nothing AndAlso InStr(.Cells(r, 1).value.ToString, "Total") < 1
                    r = r + 1
                End While
            End If

            If Not .cells(7, 5).value Is Nothing AndAlso .cells(7, 5).value.ToString.Contains("slut") Then
                If .cells(7, 6).value.ToString.Length = 10 Then
                    d = CDate(.Cells(7, 6).value)
                Else
                    d = CDate(Left(.cells(7, 6).value, 4) & "-" & Mid(.cells(7, 6).value, 5, 2) & "-" & Right(.cells(7, 6).value, 2))
                End If
            Else
                r = r - 1
                If .cells(r, intDateCol).value IsNot Nothing AndAlso .cells(r, intDateCol).value.ToString.Length = 10 Then
                    d = CDate(.Cells(r, intDateCol).value)
                Else
                    Dim emptyRows As Integer = 0
                    While emptyRows < 20
                        If .cells(r, intDateCol).value Is Nothing Then
                            emptyRows += 1
                            r += 1
                        Else
                            emptyRows = 0
                            'The word Annonsör has 8 letters as well and might appear there
                            If .cells(r, intDateCol).value.ToString.Length = 8 AndAlso .cells(r, intDateCol).value.ToString.Contains("-") AndAlso .cells(r, intDateCol).value <> "Annonsör" Then
                                If d < CDate(Left(.cells(r, intDateCol).value, 4) & "-" & Mid(.cells(r, intDateCol).value, 5, 2) & "-" & Right(.cells(r, intDateCol).value, 2)) Then
                                    d = CDate(Left(.cells(r, intDateCol).value, 4) & "-" & Mid(.cells(r, intDateCol).value, 5, 2) & "-" & Right(.cells(r, intDateCol).value, 2))
                                End If
                            End If
                            r += 1
                        End If

                    End While


                End If

            End If



            emptyRowCounter = 0
            rowCounter = firstRowOfSpots

            While emptyRowCounter < 100
                If .cells(rowCounter, intDateCol).value Is Nothing Then
                    emptyRowCounter += 1
                Else
                    If .cells(rowCounter, intDateCol).value.ToString.Length = 10 Then
                        emptyRowCounter = 0
                        If .cells(rowCounter, intDateCol).value.ToString.Contains("-") Then
                            If CDate(.cells(rowCounter, intDateCol).value) > d Then
                                d = CDate(.cells(rowCounter, intDateCol).value)
                            End If
                        End If
                    End If

                End If
                rowCounter += 1
            End While

            EndDate = d

            Dim frmImport As New frmImport(Campaign.Channels(Stat))
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate

            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - " & Stat
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name
            If RBS Then frmImport.Label6.Tag = "RBS"
            If Spec Then frmImport.Label6.Tag = "Spec"
            Dim tmpNetBudget As Single = 0
            r = 1

            Dim noBudget As Boolean = False
            While (.cells(r, 1).Value <> "SUMMA" AndAlso .cells(r, 1).value <> "Optionstyp") AndAlso r < 5000
                r += 1
            End While
            If r < 5000 Then
                c = 1
                While Not ((.cells(r, c).value Is Nothing OrElse .cells(r, c).value = "") AndAlso (.cells(r, c + 1).value Is Nothing OrElse .cells(r, c + 1).value = ""))
                    If .cells(r, c).value = "Netto" Then NetCol = c
                    c += 1
                End While
            End If
            Dim found As Boolean = False
            While Not found AndAlso r < 5000
                If .cells(r, 1).Value = "RBS" And RBS Then
                    tmpNetBudget += CDbl(Trinity.Helper.RemoveNonNumbersFromString(.cells(r, NetCol).value))
                    found = True
                ElseIf .cells(r, 1).value = "Specific" And Spec Then
                    tmpNetBudget += CDbl(Trinity.Helper.RemoveNonNumbersFromString(.cells(r, NetCol).value))
                    found = True
                End If
                r += 1
            End While

            If r = 5000 Then noBudget = True

            r -= 1
            'While .cells(r, 1).value <> "SUMMA"
            'r += 1
            'End While
            If noBudget Then
                tmpNetBudget = 0
            Else
                While Not .cells(r, 1).value Is Nothing
                    If Spec AndAlso .cells(r, 1).value = "Specific" Then tmpNetBudget = CDbl(Trinity.Helper.RemoveNonNumbersFromString(.cells(r, NetCol).value))
                    If RBS AndAlso .cells(r, 1).value = "RBS" Then tmpNetBudget = CDbl(Trinity.Helper.RemoveNonNumbersFromString(.cells(r, NetCol).value))
                    r += 1
                End While
            End If

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")
            frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Idx = frmImport.txtIndex.Text
            BT = frmImport.cmbBookingType.Text

            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = Stat Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedSpot Is Nothing Then
                                        TmpSpot.MatchedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            If frmImport.optReplaceBudget.Checked Then
                Campaign.Channels(Stat).BookingTypes(BT).ConfirmedNetBudget = CDec(tmpNetBudget)
            Else
                Campaign.Channels(Stat).BookingTypes(BT).ConfirmedNetBudget += CDec(tmpNetBudget)
            End If
            Campaign.Channels(Stat).BookingTypes(BT).ContractNumber = .Range("B3").value
            c = 1
            While .Cells(Row - 1, c).value <> "Estimat" And c < 200
                c = c + 1
            End While
            RatingCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "BB/BP" And c < 200
                c = c + 1
            End While
            PlacCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Brutto" And c < 200
                c = c + 1
            End While
            GrossCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Netto" And c < 200
                c = c + 1
            End While
            NetCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Anm" And c < 200
                c = c + 1
            End While
            RemarkCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Spotlängd" And .Cells(Row - 1, c).value <> "Sek" And c < 200
                c = c + 1
            End While
            LengthCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Filmkod" And c < 200
                c = c + 1
            End While
            FilmCol = c
            c = 1
            While InStr(.Cells(Row - 1, c).value, "tid", Microsoft.VisualBasic.CompareMethod.Text) = 0 And c < 200
                c = c + 1
            End While
            ProgCol = c

            'remake the numeric columns to numerics, not strings
            If NetCol = 13 Then
                .Columns("K:M").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                .Columns("K:M").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                .AllCells.Replace(What:=",", Replacement:=",", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
            Else
                .Columns("J:L").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                .Columns("J:L").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
                .AllCells.Replace(What:=",", Replacement:=",", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
            End If


            While emptyRow < 6
                'read all the spots

                Dim l As Integer
                Dim fRow As Integer
                'if there is no filmcode use the duration instead

                If FilmCol = 200 Then
                    MsgBox("There are no filmcodes or spot duration present. Trinity will use a default film.", MsgBoxStyle.Information)
                    fRow = 1
                Else
                    If .cells(Row, FilmCol).value Is Nothing Then
                        fRow = FilmCol + 1
                        l = 1
                    Else
                        fRow = FilmCol
                        l = 5
                    End If
                End If

                While Not .Cells(Row, fRow).value Is Nothing Or Not .cells(Row, fRow + 1).value Is Nothing
                    'If .cells(Row, FilmCol).value Is Nothing Then
                    '    fRow = FilmCol + 1
                    '    l = 1
                    'Else
                    '    fRow = FilmCol
                    '    l = 5
                    'End If
                    If InStr(.Cells(Row, 1).value, "Total") = 1 Then
                        Exit While
                    End If
                    If .cells(Row, intDateCol).value.ToString.Length = 10 Then
                        d = (.Cells(Row, intDateCol).value)
                    ElseIf .cells(Row, intDateCol).value.GetType.FullName = "System.Double" Then
                        d = Date.FromOADate(.cells(Row, intDateCol).value).ToString
                    Else
                        d = (Left(.cells(Row, intDateCol).value, 4) & "-" & Mid(.cells(Row, intDateCol).value, 5, 2) & "-" & Right(.cells(Row, intDateCol).value, 2))
                    End If

                    TmpDate = CDate(d)
                    If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                        TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                        TmpSpot.Channel = Campaign.Channels(Stat)
                        TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                        TmpSpot.AirDate = TmpDate.ToOADate
                        .Columns(2).AutoFit()
                        t = .Cells(Row, ProgCol).Text.ToString.Substring(0, 5)
                        MaM = 60 * Val(t.Substring(0, 2)) + Val(Mid(t, 4))
                        If MaM < 120 Then MaM = MaM + 1440
                        TmpSpot.MaM = MaM
                        TmpSpot.Programme = Mid(.Cells(Row, ProgCol).value, 7)
                        TmpSpot.SpotLength = .Cells(Row, LengthCol).value

                        If LengthCol = FilmCol Then
                            TmpSpot.SpotLength = TmpSpot.Channel.BookingTypes(BT).Weeks(1).Films(1).FilmLength
                            fk = TmpSpot.Channel.BookingTypes(BT).Weeks(1).Films(1).Filmcode
                        Else
                            If fRow <> FilmCol Then
                                fk = TmpSpot.SpotLength
                                If Not fk.Contains("s") Then
                                    fk = fk & "s"
                                End If
                            Else
                                fk = .Cells(Row, FilmCol).value
                                If fk.Substring(0, 2) = "AA" Then fk = Mid(fk, 3)
                            End If
                        End If

                        TmpSpot.Filmcode = fk
                        TmpSpot.Remark = .Cells(Row, RemarkCol).value
                        If TmpSpot.Filmcode Is Nothing Or TmpSpot.Filmcode = "" Then
                            TmpSpot.Filmcode = TmpSpot.SpotLength
                        End If
                        TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                        If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                            DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                            Campaign.PlannedSpots.Remove(TmpSpot.ID)
                        End If
                        If Not TmpSpot.Week Is Nothing Then

                            Trinity.Helper.SetFilmForSpot(TmpSpot)

                            TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = .Cells(Row, RatingCol).Value
                            TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = .Cells(Row, RatingCol).Value
                            TmpSpot.PriceNet = .Cells(Row, NetCol).value
                            TmpSpot.PriceGross = .Cells(Row, GrossCol).value
                        End If
                    Else
                        Dim Foo As Integer = 0
                    End If
                    Row = Row + 1
                End While
                'finished reading all the spots

                'read the next lines and check for another chunk of spots
                Dim z As Integer = 0
                Dim tmpStr As String
                For z = 0 To 150
                    Row += 1
                    tmpStr = .Cells(Row, 1).value
                    If tmpStr Is Nothing Then
                        emptyRow += 1
                        If emptyRow = 15 Then
                            Exit While
                        End If

                    Else 'There is something in this row, now look what it is
                        If tmpStr.ToUpper = "SUMMA" OrElse tmpStr.ToUpper = "OPTIONSTYP" Then
                            Exit While
                        End If
                        'if we find more spots in another chunk (different headline) we redo the process
                        If Spec Then
                            If tmpStr = "RBS" Then
                                GoTo read_spots
                            End If
                        End If
                        If RBS Then
                            If tmpStr = "Specific" Then 'Or tmpStr = "RBS" Then
                                GoTo read_spots
                            End If
                        End If
                        emptyRow = 0
                    End If
                    If Not tmpStr Is Nothing AndAlso tmpStr.ToUpper = "FILMKOD" Then
                        Row += 1
                        Exit For
                    End If
                Next

            End While

        End With
        If DateOutOfPeriod.Count > 0 Then
            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
            For i As Integer = 0 To DateOutOfPeriod.Count
                Msg += Format(Date.FromOADate(DateOutOfPeriod(i)), "Short date") & vbCrLf
            Next
            Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        On Error GoTo 0
        Exit Sub

import_RBS_Budget:

        Dim intPris As Integer
        Dim strPris As String
        Dim sum As Double = 0
        Dim bolGoToRead As Boolean = False

        With WB.Sheets(1)

read_pris:

            While .Cells(Row - 1, 1).value <> "Pris" And Row < 200
                If .Cells(Row - 1, 1).value = "Filmkod" Then
                    'if we have a budget we need to add it before reading spots
                    If sum = 0 Then
                        bolGoToRead = True
                        GoTo show_budget
                    End If
                    GoTo read_spots
                End If

                Row += 1
            End While
            If Row < 200 Then
                strPris = .Cells(Row - 1, 2).value
                If IsNumeric(strPris.Trim) Then
                    intPris = CInt(strPris.Trim)
                    sum += intPris
                End If
                Row += 2
                GoTo read_pris
            End If
        End With

show_budget:
        'put up the dialog of if to replace the budget or add to the budget
        Dim frmImportRBS As New frmImport(Campaign.Channels(Stat))
        frmImportRBS.dtFrom.Enabled = False
        frmImportRBS.dtTo.Enabled = False
        frmImportRBS.txtIndex.Text = 100
        frmImportRBS.Text = "Import Schedule - TV4+"
        frmImportRBS.lblPath.Tag = WB.Path
        frmImportRBS.lblPath.Text = WB.Name
        frmImportRBS.Label6.Tag = "RBS"
        frmImportRBS.chkReplace.Enabled = False
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")
        frmImportRBS.lblConfirmationBudget.Text = Format(CDec(sum), "N0")
        If frmImportRBS.ShowDialog = Windows.Forms.DialogResult.Cancel Then GoTo cmdImportSchedule_Click_Error
        BT = frmImportRBS.cmbBookingType.Text

        If frmImportRBS.optReplaceBudget.Checked Then
            Campaign.Channels(Stat).BookingTypes(BT).ConfirmedNetBudget = CDec(sum)
        Else
            Campaign.Channels(Stat).BookingTypes(BT).ConfirmedNetBudget += CDec(sum)
        End If
        sum = 0
        'if we where directed here before going back to reading spots
        If bolGoToRead Then
            bolGoToRead = False
            GoTo read_spots
        End If

        Exit Sub

cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
    End Sub

    Private Sub ImportTV4SportSchedule(ByVal WB As CultureSafeExcel.Workbook)
        Dim Row As Integer
        Dim TargetRow As Integer
        Dim Target As String
        Dim Gender As String
        Dim AgeStr As String
        Dim MinAge As String
        Dim MaxAge As String
        Dim SkippedSpot As Boolean = False
        Dim r As Integer
        Dim d As String
        Dim c As Integer
        Dim t As String

        Dim TmpDate As Date
        Dim TmpYear As String
        Dim TmpMonth As String
        Dim TmpDay As String
        Dim StartDate As Date
        Dim EndDate As Date
        Dim StartRow As Integer
        Dim Stat As String

        Dim ChanCol As Integer
        Dim DateCol As Integer
        Dim FilmCol As Integer
        Dim ProgCol As Integer
        Dim TimeCol As Integer
        Dim RatingCol As Integer
        Dim LengthCol As Integer
        Dim BreakTypeCol As Integer
        Dim RemarkCol As Integer
        Dim PriceCol As Integer
        Dim PlacCol As Integer
        Dim GrossCol As Integer
        Dim NetCol As Integer
        Dim DurCol As Integer

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpFilm As Trinity.cFilm
        Dim MaM As Integer
        Dim UI As Single
        Dim fk As String
        Dim Chan As String

        'TV4 Sport

        Row = 1
        With WB.Sheets(1)
            Campaign.Channels("TV4 Sport").IsVisible = True

            Campaign.Channels("TV4 Sport").MainTarget.TargetName = Campaign.Channels("TV4 Sport").BookingTypes("RBS").BuyingTarget.Target.TargetName
            Campaign.Channels("TV4 Sport").MainTarget.Universe = Campaign.Channels("TV4 Sport").BuyingUniverse

            r = 3
            d = CDate(.Cells(r, 3).value)
            StartDate = d
            While Not .Cells(r, 1).value Is Nothing AndAlso .Cells(r, 1).value.ToString <> ""
                r = r + 1
            End While
            While .Cells(r, 3).value Is Nothing OrElse .Cells(r, 3).value.ToString = ""
                r = r - 1
            End While
            d = CDate(.Cells(r, 3).value)
            EndDate = d
            Dim frmImport As New frmImport(Campaign.Channels("TV4 Sport"))
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate

            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - TV4 Sport"
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name
            Dim tmpNetBudget As Single
            tmpNetBudget = 0
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")
            frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Idx = frmImport.txtIndex.Text
            BT = frmImport.cmbBookingType.Text

            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = "TV4 Sport" Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedSpot Is Nothing Then
                                        TmpSpot.MatchedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            Campaign.Channels("TV4 Sport").BookingTypes(BT).ConfirmedNetBudget = 0
            Campaign.Channels("TV4 Sport").BookingTypes(BT).ContractNumber = ""
            c = 1
            Row = 3
            While Not .Cells(Row, 1).value Is Nothing AndAlso .Cells(Row, 1).value.ToString <> ""
                d = .Cells(Row, 3).value
                If Not d Is Nothing AndAlso Not d = "" Then
                    TmpDate = CDate(d)
                End If
                If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                    TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                    TmpSpot.Channel = Campaign.Channels("TV4 Sport")
                    TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                    TmpSpot.AirDate = TmpDate.ToOADate
                    t = .Cells(Row, 4).Text
                    Dim temp As Integer
                    Dim temp2 As Integer
                    MaM = 60 * Val(t.Substring(0, 2)) + Val(Mid(t, 4, 2))
                    If MaM < 120 Then MaM = MaM + 1440
                    TmpSpot.MaM = MaM
                    TmpSpot.Programme = .Cells(Row, 5).value
                    fk = .Cells(Row, 1).value
                    If fk.Substring(0, 2) = "AA" Or fk.Substring(0, 2) = "BB" Then
                        fk = Mid(fk, 3)
                    End If
                    TmpSpot.Filmcode = fk
                    TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                    If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                        DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                        Campaign.PlannedSpots.Remove(TmpSpot.ID)
                    End If
                    If Not TmpSpot.Week Is Nothing Then

                        Trinity.Helper.SetFilmForSpot(TmpSpot)

                        TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                        'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = 0
                        TmpSpot.MyRating = 0
                        'If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                        '    UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                        'Else
                        '    UI = 0
                        'End If
                        'If UI > 0 Then
                        '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = TmpSpot.MyRating / UI
                        'Else
                        '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                        'End If
                        TmpSpot.PriceNet = 0
                        TmpSpot.PriceGross = 0
                    End If
                End If
                Row = Row + 1
            End While
        End With


        On Error GoTo 0
        Exit Sub

cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
    End Sub

    Private Sub ImportTV400Schedule(ByVal WB As CultureSafeExcel.Workbook)
        Dim Row As Integer
        Dim TargetRow As Integer
        Dim Target As String
        Dim Gender As String
        Dim AgeStr As String
        Dim MinAge As String
        Dim MaxAge As String
        Dim SkippedSpot As Boolean = False
        Dim r As Integer
        Dim d As String
        Dim c As Integer
        Dim t As String

        Dim TmpDate As Date
        Dim TmpYear As String
        Dim TmpMonth As String
        Dim TmpDay As String
        Dim StartDate As Date
        Dim EndDate As Date
        Dim StartRow As Integer
        Dim Stat As String

        Dim ChanCol As Integer
        Dim DateCol As Integer
        Dim FilmCol As Integer
        Dim ProgCol As Integer
        Dim TimeCol As Integer
        Dim RatingCol As Integer
        Dim LengthCol As Integer
        Dim BreakTypeCol As Integer
        Dim RemarkCol As Integer
        Dim PriceCol As Integer
        Dim PlacCol As Integer
        Dim GrossCol As Integer
        Dim NetCol As Integer
        Dim DurCol As Integer

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpFilm As Trinity.cFilm
        Dim MaM As Integer
        Dim UI As Single
        Dim fk As String
        Dim Chan As String

        'TV400 Bekräftelse

        Row = 1
        With WB.Sheets(1)
            Campaign.Channels("TV400").IsVisible = True

            Campaign.Channels("TV400").MainTarget.TargetName = Campaign.Channels("TV400").BookingTypes("RBS").BuyingTarget.Target.TargetName
            Campaign.Channels("TV400").MainTarget.Universe = Campaign.Channels("TV400").BuyingUniverse

            r = 3
            d = CDate(.Cells(r, 3).value)
            StartDate = d
            While Not .Cells(r, 1).value Is Nothing AndAlso .Cells(r, 1).value.ToString <> ""
                r = r + 1
            End While
            While .Cells(r, 3).value Is Nothing OrElse .Cells(r, 3).value.ToString = ""
                r = r - 1
            End While
            d = CDate(.Cells(r, 3).value)
            EndDate = d
            Dim frmImport As New frmImport(Campaign.Channels("TV400"))
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate

            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - TV400"
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name
            Dim tmpNetBudget As Single
            tmpNetBudget = 0
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")
            frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Idx = frmImport.txtIndex.Text
            BT = frmImport.cmbBookingType.Text

            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = "TV400" Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedSpot Is Nothing Then
                                        TmpSpot.MatchedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If

            Campaign.Channels("TV400").BookingTypes(BT).ConfirmedNetBudget = 0
            Campaign.Channels("TV400").BookingTypes(BT).ContractNumber = ""
            c = 1
            Row = 3
            While Not .Cells(Row, 1).value Is Nothing AndAlso .Cells(Row, 1).value.ToString <> ""
                d = .Cells(Row, 3).value
                If Not d Is Nothing AndAlso Not d = "" Then
                    TmpDate = CDate(d)
                End If
                If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                    TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                    TmpSpot.Channel = Campaign.Channels("TV400")
                    TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                    TmpSpot.AirDate = TmpDate.ToOADate
                    t = .Cells(Row, 4).Text
                    Dim temp As Integer
                    Dim temp2 As Integer
                    MaM = 60 * Val(t.Substring(0, 2)) + Val(Mid(t, 4, 2))
                    If MaM < 120 Then MaM = MaM + 1440
                    TmpSpot.MaM = MaM
                    TmpSpot.Programme = .Cells(Row, 5).value
                    fk = .Cells(Row, 1).value
                    If fk.Substring(0, 2) = "AA" Or fk.Substring(0, 2) = "BB" Then
                        fk = Mid(fk, 3)
                    End If
                    TmpSpot.Filmcode = fk
                    TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                    If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                        DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                        Campaign.PlannedSpots.Remove(TmpSpot.ID)
                    End If
                    If Not TmpSpot.Week Is Nothing Then

                        Trinity.Helper.SetFilmForSpot(TmpSpot)

                        TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                        'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = 0
                        TmpSpot.MyRating = 0
                        'If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                        '    UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                        'Else
                        '    UI = 0
                        'End If
                        'If UI > 0 Then
                        '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = TmpSpot.MyRating / UI
                        'Else
                        '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                        'End If
                        TmpSpot.PriceNet = 0
                        TmpSpot.PriceGross = 0
                    End If
                End If
                Row = Row + 1
            End While
        End With


        On Error GoTo 0
        Exit Sub

cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
    End Sub

    Private Sub ImportMTVSchedule(ByVal WB As CultureSafeExcel.Workbook)

        Dim Row As Integer
        Dim TargetRow As Integer
        Dim Target As String
        Dim Gender As String
        Dim AgeStr As String
        Dim MinAge As String
        Dim MaxAge As String
        Dim SkippedSpot As Boolean = False
        Dim r As Integer
        Dim d As String
        Dim c As Integer
        Dim t As String

        Dim TmpDate As Date
        Dim TmpYear As String
        Dim TmpMonth As String
        Dim TmpDay As String
        Dim StartDate As Date
        Dim EndDate As Date
        Dim StartRow As Integer
        Dim Stat As String

        Dim ChanCol As Integer
        Dim DateCol As Integer
        Dim FilmCol As Integer
        Dim ProgCol As Integer
        Dim TimeCol As Integer
        Dim RatingCol As Integer
        Dim LengthCol As Integer
        Dim BreakTypeCol As Integer
        Dim RemarkCol As Integer
        Dim PriceCol As Integer
        Dim PlacCol As Integer
        Dim GrossCol As Integer
        Dim NetCol As Integer
        Dim DurCol As Integer

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpFilm As Trinity.cFilm
        Dim MaM As Integer
        Dim UI As Single
        Dim fk As String
        Dim Chan As String
        Dim UseChannel As String = "Not Set"
        Dim ControlizerSheet As String = "Not Set"
        'MTV

        frmChooseMTVNickComedy.ShowDialog()
        If frmChooseMTVNickComedy.optMTV.Checked = True Then UseChannel = "MTV"
        If frmChooseMTVNickComedy.optCC.Checked = True Then UseChannel = "Comedy Central"
        If frmChooseMTVNickComedy.optNick.Checked = True Then UseChannel = "Nickelodeon"

        'If (WB.Sheets("Sales Order").cells(4, 1).value.ToString.ToUpper.Contains("NICKELODEON")) Then
        '    UseChannel = "Nickelodeon"
        'ElseIf (WB.Sheets("Sales Order").cells(4, 1).value.ToString.ToUpper.Contains("COMEDY")) Then
        '    UseChannel = "Comedy Central"
        'Else : UseChannel = "MTV"
        'End If


        Row = 1




        With WB.Sheets(1)

            Dim campaignInfo As New Dictionary(Of String, Object)
            campaignInfo.Add("Startdate", Now())
            campaignInfo.Add("Enddate", Now())
            campaignInfo.Add("Budget", 0)

            For tmpRow As Integer = 1 To 20
                If .cells(tmpRow, 1).value IsNot Nothing AndAlso .cells(tmpRow, 1).value.ToString.ToUpper.Contains("START") Then
                    campaignInfo("Startdate") = .cells(tmpRow, 2).value
                ElseIf .cells(tmpRow, 1).value IsNot Nothing AndAlso .cells(tmpRow, 1).value.ToString.ToUpper.Contains("END") Then
                    campaignInfo("Enddate") = .cells(tmpRow, 2).value
                ElseIf .cells(tmpRow, 1).value IsNot Nothing AndAlso .cells(tmpRow, 1).value.ToString.ToUpper.Contains("BUDGET") Then
                    campaignInfo("Budget") = .cells(tmpRow, 2).value
                End If

            Next
            Dim frmImport As New frmImport(Campaign.Channels(UseChannel))
            frmImport.dtFrom.Value = campaignInfo("Startdate")
            frmImport.dtTo.Value = campaignInfo("Enddate")

            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - " & UseChannel
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name
            Dim tmpNetBudget As Single
            Dim x As Integer
            Dim y As Integer
            Dim tmpStr As String


            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")
            frmImport.lblConfirmationBudget.Text = Format(CDec(campaignInfo("Budget")), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Idx = frmImport.txtIndex.Text
            BT = frmImport.cmbBookingType.Text

            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = UseChannel Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedSpot Is Nothing Then
                                        TmpSpot.MatchedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            If frmImport.optReplaceBudget.Checked Then
                Campaign.Channels(UseChannel).BookingTypes(BT).ConfirmedNetBudget = campaignInfo("Budget")
            Else
                Campaign.Channels(UseChannel).BookingTypes(BT).ConfirmedNetBudget += campaignInfo("Budget")
            End If
            'Campaign.Channels(UseChannel).BookingTypes(BT).ContractNumber = .Range("H13").value
            While .cells(Row, 1).value <> "Date" And .cells(Row, 1).value <> "Week"
                Row = Row + 1
            End While
            c = 1
            While .cells(Row, c).value <> "Date"
                c = c + 1
            End While
            DateCol = c
            c = 1
            While .cells(Row, c).value <> "Time"
                c = c + 1
            End While
            TimeCol = c
            c = 1
            While InStr(.cells(Row, c).value, "Program") = 0
                c = c + 1
            End While
            ProgCol = c
            c = 1
            While .cells(Row, c).value <> "Duration" And .cells(Row, c).value <> "Length"
                c = c + 1
            End While
            DurCol = c
            c = 1
            While InStr(.cells(Row, c).value, "Film") = 0
                c = c + 1
            End While
            FilmCol = c
            Row = Row + 1


            While Not .Cells(Row, DateCol).value Is Nothing AndAlso Not .Cells(Row + 1, DateCol).value Is Nothing
                If Not .cells(Row, DateCol).value Is Nothing Then
                    d = .Cells(Row, DateCol).value
                    TmpDate = CDate(d)
                    If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                        TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                        TmpSpot.Channel = Campaign.Channels(UseChannel)
                        TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                        TmpSpot.AirDate = TmpDate.ToOADate
                        t = .Cells(Row, TimeCol).Text
                        t = t.Substring(0, 5)
                        MaM = 60 * Val(t.Substring(0, 2)) + Val(Mid(t, 4))
                        If MaM < 120 Then MaM = MaM + 1440
                        TmpSpot.MaM = MaM
                        TmpSpot.Programme = .Cells(Row, ProgCol).value
                        fk = .Cells(Row, FilmCol).value
                        If InStr(fk, " ") > 0 Then
                            fk = fk.Substring(0, InStr(fk, " ") - 1)
                        End If
                        If fk Is Nothing Or fk = "" Then
                            TmpSpot.Filmcode = .Cells(Row, DurCol).value & "s"
                        Else
                            TmpSpot.Filmcode = fk
                        End If
                        TmpSpot.SpotLength = .Cells(Row, DurCol).value
                        TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                        If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                            DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                            Campaign.PlannedSpots.Remove(TmpSpot.ID)
                        End If
                        If Not TmpSpot.Week Is Nothing Then

                            Trinity.Helper.SetFilmForSpot(TmpSpot)


                            TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                            'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = 0
                            TmpSpot.MyRating = 0
                            'If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                            '    UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                            'Else
                            '    UI = 0
                            'End If
                            'If UI > 0 Then
                            '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = TmpSpot.MyRating / UI
                            'Else
                            '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                            'End If
                            TmpSpot.PriceNet = 0
                            TmpSpot.PriceGross = 0
                        End If
                    End If
                    Row = Row + 1
                End If
            End While
        End With

        If DateOutOfPeriod.Count > 0 Then
            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
            For i As Integer = 0 To DateOutOfPeriod.Count
                Msg += Format(Date.FromOADate(DateOutOfPeriod(i)), "Short date") & vbCrLf
            Next
            Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        On Error GoTo 0
        Exit Sub

cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
    End Sub

    Private Sub ImportJetixSchedule(ByVal WB As CultureSafeExcel.Workbook)
        Dim Row As Integer
        Dim TargetRow As Integer
        Dim Target As String
        Dim MinAge As String
        Dim MaxAge As String
        Dim SkippedSpot As Boolean = False
        Dim r As Integer
        Dim d As String
        Dim c As Integer
        Dim t As String

        Dim TmpDate As Date
        Dim TmpYear As String
        Dim TmpMonth As String
        Dim TmpDay As String
        Dim StartDate As Date
        Dim EndDate As Date
        Dim StartRow As Integer
        Dim Stat As String

        Dim ChanCol As Integer
        Dim DateCol As Integer
        Dim FilmCol As Integer
        Dim ProgCol As Integer
        Dim TimeCol As Integer
        Dim RatingCol As Integer
        Dim LengthCol As Integer
        Dim BreakTypeCol As Integer
        Dim RemarkCol As Integer
        Dim PriceCol As Integer
        Dim PlacCol As Integer
        Dim GrossCol As Integer
        Dim NetCol As Integer
        Dim DurCol As Integer

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpFilm As Trinity.cFilm
        Dim MaM As Integer
        Dim UI As Single
        Dim fk As String
        Dim Chan As String
        Dim newFormat As Boolean = False

        'Jetix

        Row = 1
        With WB.Sheets(1)
            Campaign.Channels("Jetix").IsVisible = True

            If Not .range("O14").value Is Nothing Then
                Target = Trim(.range("O14").value).Substring(Trim(.range("O14").value).Length - 7).Replace(" ", "")
            Else
                Target = .range("A14").value.ToString
                For Each letter As Char In "Kabcdefghijklmnopqrstuvxyz "
                    Target = Target.Replace(letter, "")
                Next
                newFormat = True
            End If

            If InStr(Target, "-") > 1 Then
                MinAge = Target.Substring(0, InStr(Target, "-") - 1)
                MaxAge = Mid(Target, InStr(Target, "-") + 1)
            Else
                MinAge = Target.Substring(0, InStr(Target, ".") - 1)
                MaxAge = Mid(Target, InStr(Target, ".") + 1)
            End If

            Campaign.Channels("Jetix").MainTarget.TargetName = MinAge & "-" & MaxAge
            Campaign.Channels("Jetix").MainTarget.Universe = Campaign.Channels("jetix").BuyingUniverse

            If Not newFormat Then
                d = .range("N13").value
            Else
                d = .range("K13").value
            End If
            'check what date format is used
            Dim int1 As Integer = CInt(d.Substring(0, d.IndexOf("/")).Replace("/", ""))
            Dim int2 As Integer = CInt(d.Substring(d.IndexOf("/") + 1, 2).Replace("/", ""))
            Dim bolDate As Boolean = False
            If IsNumeric(Right(d, 4)) Then
                If int1 > 12 Then
                    bolDate = False
                ElseIf int2 > 12 Then
                    bolDate = True
                Else
                    bolDate = True
                End If
            Else
                bolDate = True
            End If

            If bolDate Then
                StartDate = d
                d = .range("V13").value.ToString.Substring(0, 10)
                EndDate = d
            Else
                If newFormat Then
                    Dim date1() As String = Strings.Split(Strings.Left(.range("K13").value, 10), "/")
                    Dim date2() As String = Strings.Split(Strings.Right(.range("K13").value, 10), "/")
                    StartDate = DateSerial(CInt(date1(2)), CInt(date1(1)), CInt(date1(0)))
                    EndDate = DateSerial(CInt(date2(2)), CInt(date2(1)), CInt(date2(0)))
                Else
                    Dim s As String
                    Dim s2 As String
                    Dim s3 As String
                    s = Right(d, 4)
                    s2 = d.Substring(0, d.IndexOf("/"))
                    s3 = d.Substring(d.IndexOf("/") + 1, d.LastIndexOf("/") - d.IndexOf("/") - 1)
                    d = s3 & "/" & s2 & "/" & s
                    StartDate = d
                    d = .range("V13").value.ToString.Substring(0, 10)
                    s = Right(d, 4)
                    s2 = d.Substring(0, d.IndexOf("/"))
                    s3 = d.Substring(d.IndexOf("/") + 1, d.LastIndexOf("/") - d.IndexOf("/") - 1)
                    d = s3 & "/" & s2 & "/" & s
                    EndDate = d
                End If
            End If

            Dim frmImport As New frmImport(Campaign.Channels("Jetix"))
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate

            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - Jetix"
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name
            Dim tmpNetBudget As Single
            If newFormat Then
                tmpNetBudget = .range("C15").value
            Else
                tmpNetBudget = .range("E16").value
            End If

            'If .range("G20").value = "Campaign Budget" Then
            '    If .range("H20").value.ToString.Trim.EndsWith("SEK", StringComparison.InvariantCultureIgnoreCase) Then
            '        .range("H20").value = .range("H20").value.ToString.Trim.Substring(0, .range("H20").value.ToString.Trim.Length - 3)
            '    End If
            '    tmpNetBudget = .Range("H20").value.ToString.Replace(" ", "").Replace(" ", "").Replace(",", ".")
            'Else
            '    If .range("G20").value.ToString.Trim.EndsWith("SEK", StringComparison.InvariantCultureIgnoreCase) Then
            '        .range("G20").value = .range("G20").value.ToString.Trim.Substring(0, .range("G20").value.ToString.Trim.Length - 3)
            '    End If
            '    tmpNetBudget = .Range("G20").value.ToString.Replace(" ", "").Replace(" ", "").Replace(",", ".")
            'End If
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")
            frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Idx = frmImport.txtIndex.Text
            BT = frmImport.cmbBookingType.Text

            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = "Jetix" Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedSpot Is Nothing Then
                                        TmpSpot.MatchedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            If frmImport.optReplaceBudget.Checked Then
                Campaign.Channels("Jetix").BookingTypes(BT).ConfirmedNetBudget = CDec(tmpNetBudget)
            Else
                Campaign.Channels("Jetix").BookingTypes(BT).ConfirmedNetBudget += CDec(tmpNetBudget)
            End If
            If newFormat Then
                Campaign.Channels("Jetix").BookingTypes(BT).ContractNumber = .Range("K9").value
            Else
                Campaign.Channels("Jetix").BookingTypes(BT).ContractNumber = .Range("V9").value
            End If

            If newFormat Then
                Row = 17
            Else
                Row = 19
            End If

            c = 1
            While .cells(Row, c).value <> "Date" 'AndAlso .cells(Row - 1, c).value <> "Date" AndAlso .cells(Row + 1, c).value <> "Date"
                c = c + 1
            End While
            DateCol = c
            c = 1
            While .cells(Row, c).value <> "Time" 'AndAlso .cells(Row - 1, c).value <> "Time" AndAlso .cells(Row + 1, c).value <> "Time"
                c = c + 1
            End While
            TimeCol = c
            c = 1
            While .cells(Row, c).value <> "Prog after" 'AndAlso .cells(Row - 1, c).value.ToString <> "Prog after" AndAlso .cells(Row + 1, c).value.ToString <> "Prog after"
                c = c + 1
            End While
            If newFormat Then
                ProgCol = c
            Else
                ProgCol = c - 1
            End If
            c = 1
            While .cells(Row, c).value <> "Sec" 'AndAlso .cells(Row - 1, c).value <> "Sec" AndAlso .cells(Row + 1, c).value <> "Sec"
                c = c + 1
            End While
            DurCol = c
            c = 1
            While .cells(Row, c).value <> "Description" 'AndAlso .cells(Row - 1, c).value <> "Description" AndAlso .cells(Row + 1, c).value <> "Description"
                c = c + 1
            End While
            FilmCol = c
            If newFormat Then
                Row = 18
            Else
                Row = 21
            End If


            While Not .Cells(Row, 2).value Is Nothing
                If newFormat Then
                    TmpDate = DateSerial(CInt(Strings.Split(.cells(Row, DateCol).value, "/")(2)), CInt(Strings.Split(.cells(Row, DateCol).value, "/")(1)), CInt(Strings.Split(.cells(Row, DateCol).value, "/")(0)))
                Else
                    d = .Cells(Row, DateCol).value
                    If Year(StartDate) <> Year(EndDate) Then
                        d = Year(StartDate) & "-" & Right(d, 2) & "-" & Left(d, 2)
                        If CDate(d) < StartDate OrElse CDate(d) > EndDate Then
                            d = Year(EndDate) & "-" & Right(d, 2) & "-" & Left(d, 2)
                        End If
                    Else
                        d = Year(StartDate) & "-" & Right(d, 2) & "-" & Left(d, 2)
                    End If
                    TmpDate = CDate(d)
                End If
                If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                    TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                    TmpSpot.Channel = Campaign.Channels("Jetix")
                    TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                    TmpSpot.AirDate = TmpDate.ToOADate
                    t = .Cells(Row, TimeCol).Text
                    t = t.Substring(0, 5)
                    MaM = 60 * Val(t.Substring(0, 2)) + Val(Mid(t, 4))
                    If MaM < 120 Then MaM = MaM + 1440
                    TmpSpot.MaM = MaM
                    TmpSpot.Programme = .Cells(Row, ProgCol).value
                    fk = .Cells(Row, FilmCol).value
                    If InStr(fk, " ") > 0 Then
                        fk = fk.Substring(0, InStr(fk, " ") - 1)
                    End If
                    If fk Is Nothing Or fk = "" Then
                        TmpSpot.Filmcode = .Cells(Row, DurCol).value & "s"
                    Else
                        TmpSpot.Filmcode = fk
                    End If
                    TmpSpot.SpotLength = .Cells(Row, DurCol).value
                    TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                    If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                        DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                        Campaign.PlannedSpots.Remove(TmpSpot.ID)
                    End If
                    If Not TmpSpot.Week Is Nothing Then

                        Trinity.Helper.SetFilmForSpot(TmpSpot)

                        TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                        'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = 0
                        TmpSpot.MyRating = 0
                        'If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                        '    UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                        'Else
                        '    UI = 0
                        'End If
                        'If UI > 0 Then
                        '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = TmpSpot.MyRating / UI
                        'Else
                        '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                        'End If
                        TmpSpot.PriceNet = 0
                        TmpSpot.PriceGross = 0
                    End If
                End If
                Row = Row + 1
            End While
        End With
        If DateOutOfPeriod.Count > 0 Then
            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
            For i As Integer = 0 To DateOutOfPeriod.Count
                Msg += Format(Date.FromOADate(DateOutOfPeriod(i)), "Short date") & vbCrLf
            Next
            Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        On Error GoTo 0
        Exit Sub

cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
    End Sub

    Private Sub ImportNationalGeographicSchedule(ByVal WB As CultureSafeExcel.Workbook)

        Dim Row As Integer
        Dim TargetRow As Integer
        Dim Target As String
        Dim Gender As String
        Dim AgeStr As String
        Dim MinAge As String
        Dim MaxAge As String
        Dim SkippedSpot As Boolean = False
        Dim r As Integer
        Dim d As String
        Dim c As Integer
        Dim t As String

        Dim TmpDate As Date
        Dim TmpYear As String
        Dim TmpMonth As String
        Dim TmpDay As String
        Dim StartDate As Date
        Dim EndDate As Date
        Dim StartRow As Integer
        Dim Stat As String

        Dim ChanCol As Integer
        Dim DateCol As Integer
        Dim FilmCol As Integer
        Dim ProgCol As Integer
        Dim TimeCol As Integer
        Dim RatingCol As Integer
        Dim LengthCol As Integer
        Dim BreakTypeCol As Integer
        Dim RemarkCol As Integer
        Dim PriceCol As Integer
        Dim PlacCol As Integer
        Dim GrossCol As Integer
        Dim NetCol As Integer
        Dim DurCol As Integer

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpFilm As Trinity.cFilm
        Dim MaM As Integer
        Dim UI As Single
        Dim fk As String
        Dim Chan As String
        Dim UseChannel As String = "National Geographic"
        'Dim StartRow As Integer = 0
        Row = 2

        While WB.Sheets(1).cells(Row, 1).value <> "YEAR"
            Row += 1
        End While

        Dim Column As Integer = 1

        While (WB.Sheets(1).cells(Row, Column).value Is Nothing OrElse WB.Sheets(1).cells(Row, Column).value.ToString <> "LENGTH") And Column < 100
            Column += 1
        End While
        DurCol = Column

        Row += 1
        StartRow = Row

        StartDate = DateSerial(CInt(WB.Sheets(1).cells(Row, 1).value), CInt(WB.Sheets(1).cells(Row, 2).value), CInt(WB.Sheets(1).cells(Row, 3).value))

        While (Not WB.Sheets(1).cells(Row, 1).value Is Nothing)
            EndDate = DateSerial(CInt(WB.Sheets(1).cells(Row, 1).value), CInt(WB.Sheets(1).cells(Row, 2).value), CInt(WB.Sheets(1).cells(Row, 3).value))
            Row += 1
        End While

        Row = StartRow

        With WB.Sheets(1)
            Campaign.Channels(UseChannel).IsVisible = True

            Dim frmImport As New frmImport(Campaign.Channels(UseChannel))
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate

            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - " & UseChannel
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")
            frmImport.lblConfirmationBudget.Visible = False
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Idx = frmImport.txtIndex.Text
            BT = frmImport.cmbBookingType.Text

            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = UseChannel Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedSpot Is Nothing Then
                                        TmpSpot.MatchedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If

            While Not .Cells(Row, 1).value Is Nothing AndAlso Not .Cells(Row + 1, 1).value Is Nothing
                If Not .cells(Row, 1).value Is Nothing Then
                    TmpDate = DateSerial(CInt(.cells(Row, 1).value), CInt(.cells(Row, 2).value), CInt(.cells(Row, 3).value))
                    If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                        TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                        TmpSpot.Channel = Campaign.Channels(UseChannel)
                        TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                        TmpSpot.AirDate = TmpDate.ToOADate
                        t = .Cells(Row, 4).Text
                        t = t.Substring(0, 5)
                        MaM = 60 * Val(t.Substring(0, 2)) + Val(Mid(t, 4))
                        If MaM < 120 Then MaM = MaM + 1440
                        TmpSpot.MaM = MaM
                        TmpSpot.Programme = .Cells(Row, 5).value
                        fk = .Cells(Row, 6).value
                        If InStr(fk, " ") > 0 Then
                            fk = fk.Substring(0, InStr(fk, " ") - 1)
                        End If
                        If fk Is Nothing Or fk = "" Then
                            TmpSpot.Filmcode = .Cells(Row, DurCol).value & "s"
                        Else
                            TmpSpot.Filmcode = fk
                        End If
                        TmpSpot.SpotLength = .Cells(Row, 7).value
                        TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                        If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                            DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                            Campaign.PlannedSpots.Remove(TmpSpot.ID)
                        End If
                        If Not TmpSpot.Week Is Nothing Then

                            Trinity.Helper.SetFilmForSpot(TmpSpot)


                            TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                            'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = 0
                            TmpSpot.MyRating = 0
                            'If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                            '    UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                            'Else
                            '    UI = 0
                            'End If
                            'If UI > 0 Then
                            '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = TmpSpot.MyRating / UI
                            'Else
                            '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                            'End If
                            TmpSpot.PriceNet = 0
                            TmpSpot.PriceGross = 0
                        End If
                    End If
                    Row = Row + 1
                End If
            End While
        End With

        If DateOutOfPeriod.Count > 0 Then
            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
            For i As Integer = 0 To DateOutOfPeriod.Count
                Msg += Format(Date.FromOADate(DateOutOfPeriod(i)), "Short date") & vbCrLf
            Next
            Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        On Error GoTo 0
        Exit Sub

cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
    End Sub

    Private Sub ImportGenericSchedule(ByVal WB As CultureSafeExcel.Workbook, ByVal Channel As Trinity.cChannel, ByVal StartRow As Integer, ByVal DateCol As Integer, ByVal TimeCol As Integer, Optional ByVal ProgCol As Integer = -1, Optional ByVal FilmCol As Integer = -1, Optional ByVal DurationCol As Integer = -1, Optional ByVal GrossCol As Integer = -1, Optional ByVal NetCol As Integer = -1)
        Dim Row As Integer
        'Dim TargetRow As Integer
        'Dim Target As String
        'Dim Gender As String
        'Dim AgeStr As String
        'Dim MinAge As String
        'Dim MaxAge As String
        Dim SkippedSpot As Boolean = False
        Dim r As Integer
        Dim d As String
        'Dim c As Integer
        Dim t As String

        Dim TmpDate As Date
        'Dim TmpYear As String
        'Dim TmpMonth As String
        'Dim TmpDay As String
        Dim StartDate As Date
        Dim EndDate As Date
        'Dim Stat As String

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        'Dim TmpFilm As Trinity.cFilm
        Dim MaM As Integer
        'Dim UI As Single
        Dim fk As String
        'Dim Chan As String

        With WB.Sheets(1)

            Channel.IsVisible = True

            r = StartRow

            d = .Cells(r, DateCol).value
            If .Cells(r, DateCol).value.GetType.Name = "Double" Then
                StartDate = Date.FromOADate(d)
            Else
                StartDate = CDate(d)
            End If
            If .cells(r + 1, DateCol).value Is Nothing Then
                EndDate = StartDate
            Else
                While Not .Cells(r, 1).value Is Nothing
                    r = r + 1
                End While
                If .Cells(r - 1, DateCol).value.GetType.Name = "Double" Then
                    d = Date.FromOADate(.Cells(r - 1, DateCol).value)
                Else
                    Try
                        d = CDate(.Cells(r - 1, DateCol).value)
                    Catch
                        Dim p = Strings.Split(.cells(r - 1, DateCol).value, "/")
                        d = DateSerial(p(2), p(1), p(0))
                    End Try
                End If
                EndDate = d
            End If
            Dim frmImport As New frmImport(Channel)
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate

            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - " & Channel.ChannelName
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name
            frmImport.grpBudget.Enabled = False
            Dim tmpNetBudget As Single
            tmpNetBudget = 0
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")
            frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Idx = frmImport.txtIndex.Text
            BT = frmImport.cmbBookingType.Text
            Channel.MainTarget.TargetName = Channel.BookingTypes(BT).BuyingTarget.Target.TargetName
            Channel.MainTarget.Universe = Channel.BookingTypes(BT).BuyingTarget.Target.Universe
            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = Channel.ChannelName Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedSpot Is Nothing Then
                                        TmpSpot.MatchedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            Row = StartRow
            While Not .Cells(Row, 4).value Is Nothing AndAlso .Cells(Row, 4).value.ToString <> ""
                If .cells(Row, DateCol).value.ToString.Length <= 10 Then
                    Dim SplitDate() As String = Strings.Split(.cells(Row, DateCol).value, "/")
                    TmpDate = DateSerial(CInt(SplitDate(2)), CInt(SplitDate(1)), CInt(SplitDate(0)))
                Else
                    d = .Cells(Row, DateCol).value
                    If .Cells(Row, DateCol).value.GetType.Name = "Double" Then
                        TmpDate = Date.FromOADate(d)
                    Else
                        Try
                            d = CDate(.Cells(Row, DateCol).value)
                        Catch
                            Dim p = Strings.Split(.cells(Row, DateCol).value, "/")
                            d = DateSerial(p(2), p(1), p(0))
                        End Try
                        TmpDate = d
                    End If
                End If
                If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                    TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                    TmpSpot.Channel = Channel
                    TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                    TmpSpot.AirDate = TmpDate.ToOADate
                    If TimeCol = ProgCol Then
                        t = Left(.cells(Row, TimeCol).Text, InStr(.cells(Row, TimeCol).Text, " ") - 1)
                    Else
                        t = .Cells(Row, TimeCol).Text
                    End If
                    MaM = 60 * Val(t.Substring(0, 2)) + Val(Right(t, 2))
                    If MaM < 120 Then MaM = MaM + 1440
                    TmpSpot.MaM = MaM
                    If ProgCol > -1 Then
                        If TimeCol = ProgCol Then
                            TmpSpot.Programme = Mid(.cells(Row, ProgCol).value, InStr(.cells(Row, ProgCol).value, " ") + 1)
                        Else
                            TmpSpot.Programme = .Cells(Row, ProgCol).value
                        End If
                    End If
                    If FilmCol > -1 Then
                        fk = .cells(Row, FilmCol).value
                    Else
                        fk = ""
                    End If
                    If fk Is Nothing Then fk = .cells(Row, DurationCol).value
                    TmpSpot.Filmcode = fk
                    If DurationCol > -1 Then
                        TmpSpot.SpotLength = .cells(Row, DurationCol).value
                    End If
                    TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                    If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                        DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                        Campaign.PlannedSpots.Remove(TmpSpot.ID)
                    End If
                    If Not TmpSpot.Week Is Nothing Then

                        Trinity.Helper.SetFilmForSpot(TmpSpot)

                        If Not TmpSpot.Film Is Nothing AndAlso TmpSpot.SpotLength = 0 Then
                            TmpSpot.SpotLength = TmpSpot.Film.FilmLength
                        End If
                        TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                        'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = 0
                        TmpSpot.MyRating = 0
                        'If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                        '    UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                        'Else
                        '    UI = 0
                        'End If
                        'If UI > 0 Then
                        '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = TmpSpot.MyRating / UI
                        'Else
                        '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                        'End If
                        If NetCol < 0 Then
                            TmpSpot.PriceNet = 0
                        Else
                            TmpSpot.PriceNet = CDec(Strings.Replace(.cells(Row, NetCol).value.ToString, " ", ""))
                        End If
                        If GrossCol < 0 Then
                            TmpSpot.PriceGross = 0
                        Else
                            TmpSpot.PriceGross = CDec(Strings.Replace(.cells(Row, GrossCol).value.ToString, " ", ""))
                        End If
                    End If
                End If
                Row = Row + 1
            End While
        End With
    End Sub

    'Reversed order of Channel and WB parameter for functions to have different signatures
    Private Sub ImportGenericSchedule(ByVal Channel As Trinity.cChannel, ByVal WB As CultureSafeExcel.Workbook, ByVal StartRow As Integer, ByVal YearCol As Integer, ByVal MonthCol As Integer, ByVal DayCol As Integer, ByVal TimeCol As Integer, Optional ByVal ProgCol As Integer = -1, Optional ByVal FilmCol As Integer = -1, Optional ByVal DurationCol As Integer = -1)
        Dim Row As Integer
        Dim TargetRow As Integer
        Dim Target As String
        Dim Gender As String
        Dim AgeStr As String
        Dim MinAge As String
        Dim MaxAge As String
        Dim SkippedSpot As Boolean = False
        Dim r As Integer
        Dim d As String
        Dim c As Integer
        Dim t As String

        Dim TmpDate As Date
        Dim TmpYear As String
        Dim TmpMonth As String
        Dim TmpDay As String
        Dim StartDate As Date
        Dim EndDate As Date
        Dim Stat As String

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpFilm As Trinity.cFilm
        Dim MaM As Integer
        Dim UI As Single
        Dim fk As String
        Dim Chan As String

        With WB.Sheets(1)

            Channel.IsVisible = True

            r = StartRow

            d = CDate(.Cells(r, YearCol).value & "-" & .Cells(r, MonthCol).value & "-" & .Cells(r, DayCol).value)
            StartDate = d
            While Not .Cells(r, 1).value Is Nothing
                r += 1
            End While
            r -= 1
            d = CDate(.Cells(r, YearCol).value & "-" & .Cells(r, MonthCol).value & "-" & .Cells(r, DayCol).value)
            EndDate = d
            Dim frmImport As New frmImport(Channel)
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate

            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - " & Channel.ChannelName
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name
            frmImport.grpBudget.Enabled = False
            Dim tmpNetBudget As Single
            tmpNetBudget = 0
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")
            frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Idx = frmImport.txtIndex.Text
            BT = frmImport.cmbBookingType.Text
            Channel.MainTarget.TargetName = Channel.BookingTypes(BT).BuyingTarget.Target.TargetName
            Channel.MainTarget.Universe = Channel.BookingTypes(BT).BuyingTarget.Target.Universe
            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = Channel.ChannelName Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedSpot Is Nothing Then
                                        TmpSpot.MatchedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            Row = StartRow
            While Not .Cells(Row, 1).value Is Nothing AndAlso .Cells(Row, 1).value.ToString <> ""
                d = CDate(.Cells(Row, YearCol).value & "-" & .Cells(Row, MonthCol).value & "-" & .Cells(Row, DayCol).value)
                TmpDate = d
                If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                    TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                    TmpSpot.Channel = Channel
                    TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                    TmpSpot.AirDate = TmpDate.ToOADate
                    If TimeCol = ProgCol Then
                        t = Left(.cells(Row, TimeCol).Text, InStr(.cells(Row, TimeCol).Text, " ") - 1)
                    Else
                        t = Left(.Cells(Row, TimeCol).Text, 5)
                    End If
                    MaM = 60 * Val(t.Substring(0, 2)) + Val(Right(t, 2))

                    If MaM < 120 Then MaM = MaM + 1440
                    TmpSpot.MaM = MaM
                    If ProgCol > -1 Then
                        If TimeCol = ProgCol Then
                            TmpSpot.Programme = Mid(.cells(Row, ProgCol).value, InStr(.cells(Row, ProgCol).value, " ") + 1)
                        Else
                            TmpSpot.Programme = .Cells(Row, ProgCol).value
                        End If
                    End If
                    If FilmCol > -1 Then
                        fk = .cells(Row, FilmCol).value
                    Else
                        fk = ""
                    End If
                    TmpSpot.Filmcode = fk
                    If DurationCol > -1 Then
                        TmpSpot.SpotLength = .cells(Row, DurationCol).value
                    End If
                    TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                    If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                        DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                        Campaign.PlannedSpots.Remove(TmpSpot.ID)
                    End If
                    If Not TmpSpot.Week Is Nothing Then

                        Trinity.Helper.SetFilmForSpot(TmpSpot)

                        If Not TmpSpot.Film Is Nothing AndAlso TmpSpot.SpotLength = 0 Then
                            TmpSpot.SpotLength = TmpSpot.Film.FilmLength
                        End If
                        TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                        'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = 0
                        TmpSpot.MyRating = 0
                        'If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                        '    UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                        'Else
                        '    UI = 0
                        'End If
                        'If UI > 0 Then
                        '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = TmpSpot.MyRating / UI
                        'Else
                        '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                        'End If
                        TmpSpot.PriceNet = 0
                        TmpSpot.PriceGross = 0
                    End If
                End If
                Row = Row + 1
            End While
        End With
        On Error GoTo 0
        Exit Sub

cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
    End Sub

    Private Sub ImportNewEurosportSchedule(ByVal WB As CultureSafeExcel.Workbook)
        Dim intRow As Integer = 5

        'holders for the column number
        Dim intDateCol As Integer
        Dim intFilmCol As Integer
        Dim intProgCol As Integer
        Dim intTimeCol As Integer
        Dim intDurCol As Integer

        Dim _Spot As Trinity.cPlannedSpot
        Dim strBT As String


        Dim intMaM As Integer

        Dim dateStart As Date
        Dim dateEnd As Date
        Dim dateTmp As Date

        Dim intStartRow As Integer

        Dim c As Integer

        With WB.Sheets(2)

            'find the row where the head of the spot list is
            While .Cells(intRow, 2).value <> "Advertiser"
                intRow = intRow + 1
            End While
            intStartRow = intRow + 1

            'find the date column
            c = 1
            While .Cells(intRow, c).value <> "Day" And c < 200
                c = c + 1
            End While
            intDateCol = c
            c = 1

            'find the time column
            While .Cells(intRow, c).value <> "Break Time" And c < 200
                c = c + 1
            End While
            intTimeCol = c
            c = 1

            'find the program column
            While .Cells(intRow, c).value <> "Program" And c < 200
                c = c + 1
            End While
            intProgCol = c
            c = 1

            'find the filmcode
            While .Cells(intRow, c).value <> "Tape ID" And c < 200
                c = c + 1
            End While
            intFilmCol = c
            c = 1

            'find the column for the lenght
            While .Cells(intRow, c).value <> "Length" And c < 200
                c = c + 1
            End While
            intDurCol = c
            c = 1

            'get the dates for the list
            intRow += 1

            dateStart = .Cells(intRow, intDateCol).value
            While Not .Cells(intRow, intDurCol).value Is Nothing
                dateEnd = .Cells(intRow, intDateCol).value
                intRow += 1
            End While


            Dim frmImport As New frmImport(Campaign.Channels("Eurosport"))
            frmImport.dtFrom.Value = dateStart
            frmImport.dtTo.Value = dateEnd

            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - Eurosport"
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name

            frmImport.grpBudget.Enabled = False
            Dim tmpNetBudget As Single
            tmpNetBudget = 0
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")
            frmImport.lblConfirmationBudget.Text = 0
            frmImport.Label6.Tag = "RBS"
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            'Idx = frmImport.txtIndex.Text
            strBT = frmImport.cmbBookingType.Text
            Campaign.Channels("Eurosport").MainTarget.TargetName = Campaign.Channels("Eurosport").BookingTypes(strBT).BuyingTarget.Target.TargetName
            Campaign.Channels("Eurosport").MainTarget.Universe = Campaign.Channels("Eurosport").BookingTypes(strBT).BuyingTarget.Target.Universe
            If frmImport.chkReplace.Checked Then
                For Each _Spot In Campaign.PlannedSpots
                    If _Spot.Channel.ChannelName = "Eurosport" Then
                        If _Spot.Bookingtype.Name = strBT Then
                            If _Spot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If _Spot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not _Spot.MatchedSpot Is Nothing Then
                                        _Spot.MatchedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(_Spot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If

            intRow = intStartRow
            While Not .Cells(intRow, 2).value Is Nothing AndAlso .Cells(intRow, 2).value.ToString <> ""
                'get the spot date
                dateTmp = .Cells(intRow, intDateCol).value

                'we only read spots inside te date interval
                If dateTmp >= frmImport.dtFrom.Value And dateTmp <= frmImport.dtTo.Value Then
                    _Spot = Campaign.PlannedSpots.Add(CreateGUID)
                    _Spot.Channel = Campaign.Channels("Eurosport")
                    _Spot.Bookingtype = _Spot.Channel.BookingTypes(strBT)
                    _Spot.AirDate = dateTmp.ToOADate
                    c = .Cells(intRow, intTimeCol).Text.ToString.Replace(":", "")
                    intMaM = 60 * Val(c.ToString.Substring(0, 2)) + Val(Mid(c, 4))
                    If intMaM < 120 Then intMaM = intMaM + 1440
                    _Spot.MaM = intMaM
                    _Spot.Programme = .Cells(intRow, intProgCol).value
                    _Spot.Filmcode = .Cells(intRow, intFilmCol).value
                    _Spot.SpotLength = .Cells(intRow, intDurCol).value
                    If _Spot.Filmcode Is Nothing OrElse _Spot.Filmcode = "" Then
                        _Spot.Filmcode = .Cells(intRow, intDurCol).value
                    End If
                    _Spot.Week = _Spot.Bookingtype.GetWeek(Date.FromOADate(_Spot.AirDate))
                    If _Spot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(_Spot.AirDate) Then
                        DateOutOfPeriod.Add(DateOutOfPeriod.Count, _Spot.AirDate)
                        Campaign.PlannedSpots.Remove(_Spot.ID)
                    End If
                    If Not _Spot.Week Is Nothing Then
                        Trinity.Helper.SetFilmForSpot(_Spot)
                        _Spot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                        _Spot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = 0
                        _Spot.MyRating = 0
                        _Spot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                        _Spot.PriceNet = 0
                        _Spot.PriceGross = 0
                    End If
                End If
                intRow = intRow + 1
            End While
        End With

Quit:
        Exit Sub

cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)

    End Sub

    Private Sub ImportEurosportSchedule(ByVal WB As CultureSafeExcel.Workbook, Optional ByVal spotListVersion As Integer = 1)
        Dim Row As Integer
        Dim TargetRow As Integer
        Dim Target As String
        Dim Gender As String
        Dim AgeStr As String
        Dim MinAge As String
        Dim MaxAge As String
        Dim SkippedSpot As Boolean = False
        Dim r As Integer
        Dim d As String
        Dim c As Integer
        Dim t As String

        Dim TmpDate As Date
        Dim TmpYear As String
        Dim TmpMonth As String
        Dim TmpDay As String
        Dim StartDate As Date
        Dim EndDate As Date
        Dim StartRow As Integer
        Dim Stat As String

        Dim ChanCol As Integer
        Dim DateCol As Integer
        Dim FilmCol As Integer
        Dim ProgCol As Integer
        Dim TimeCol As Integer
        Dim RatingCol As Integer
        Dim LengthCol As Integer
        Dim BreakTypeCol As Integer
        Dim RemarkCol As Integer
        Dim PriceCol As Integer
        Dim PlacCol As Integer
        Dim GrossCol As Integer
        Dim NetCol As Integer
        Dim DurCol As Integer

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpFilm As Trinity.cFilm
        Dim MaM As Integer
        Dim UI As Single
        Dim fk As String
        Dim Chan As String
        Dim tmpFilmCode As String = "Not Set"

        'Eurosport

        With WB.Sheets(1)

            For colCount As Integer = 1 To 15
                If Not .cells(10, colCount).value Is Nothing Then
                    Select Case .cells(10, colCount).value.ToString.ToUpper
                        Case "DATE" : DateCol = colCount
                        Case "BREAK TIME" : TimeCol = colCount
                        Case "PROGRAM" : ProgCol = colCount
                        Case "FILMNO" : FilmCol = colCount
                        Case "FILMCODE" : FilmCol = colCount
                        Case "BOOKED" : FilmCol = colCount
                    End Select
                End If
            Next

            If FilmCol = 0 Then
                tmpFilmCode = Campaign.Channels("Eurosport").BookingTypes(1).Weeks(1).Films(1).Filmcode
                If MessageBox.Show("This schedule contains no filmcodes. Press cancel to stop, or OK to import this schedule and set all films to " & Campaign.Channels("Eurosport").BookingTypes(1).Weeks(1).Films(1).Filmcode & " - " & Campaign.Channels("Eurosport").BookingTypes(1).Weeks(1).Films(1).Description, "T R I N I T Y", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) = DialogResult.Cancel Then Exit Sub
            End If

            Campaign.Channels("Eurosport").IsVisible = True
            Row = 11

            r = Row

            Select Case spotListVersion
                Case 1
                    d = CDate(.Cells(r, 7).value)
                    StartDate = d
                    While Not .Cells(r, 2).value Is Nothing
                        r = r + 1
                    End While
                    If WB.Sheets(1).Cells(r - 1, 7).value.GetType.Name = "Double" Then
                        d = Date.FromOADate(.Cells(r - 1, 7).value)
                    Else
                        d = CDate(.Cells(r - 1, 7).value)
                    End If
                    EndDate = d
                    Dim frmImport As New frmImport(Campaign.Channels("Eurosport"))
                    frmImport.dtFrom.Value = StartDate
                    frmImport.dtTo.Value = EndDate

                    frmImport.txtIndex.Text = 100
                    frmImport.Text = "Import Schedule - Eurosport"
                    frmImport.lblPath.Tag = WB.Path
                    frmImport.lblPath.Text = WB.Name
                    frmImport.grpBudget.Enabled = False
                    Dim tmpNetBudget As Single
                    tmpNetBudget = 0
                    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")
                    frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
                    If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                        CleanUp()
                        Exit Sub
                    End If
                    Idx = frmImport.txtIndex.Text
                    BT = frmImport.cmbBookingType.Text
                    Campaign.Channels("Eurosport").MainTarget.TargetName = Campaign.Channels("Eurosport").BookingTypes(BT).BuyingTarget.Target.TargetName
                    Campaign.Channels("Eurosport").MainTarget.Universe = Campaign.Channels("Eurosport").BookingTypes(BT).BuyingTarget.Target.Universe
                    If frmImport.chkReplace.Checked Then
                        For Each TmpSpot In Campaign.PlannedSpots
                            If TmpSpot.Channel.ChannelName = "Eurosport" Then
                                If TmpSpot.Bookingtype.Name = BT Then
                                    If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                        If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                            If Not TmpSpot.MatchedSpot Is Nothing Then
                                                TmpSpot.MatchedSpot.MatchedSpot = Nothing
                                            End If
                                            Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    End If
                    While Not .Cells(Row, 2).value Is Nothing AndAlso .Cells(Row, 2).value.ToString <> ""
                        d = .Cells(Row, 7).value
                        If .Cells(Row, 7).value.GetType.Name = "Double" Then
                            TmpDate = Date.FromOADate(d)
                        Else
                            TmpDate = CDate(d)
                        End If
                        If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                            TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                            TmpSpot.Channel = Campaign.Channels("Eurosport")
                            TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                            TmpSpot.AirDate = TmpDate.ToOADate
                            t = .Cells(Row, 8).Text
                            MaM = 60 * Val(t.Substring(0, 2)) + Val(Mid(t, 4))
                            If MaM < 120 Then MaM = MaM + 1440
                            TmpSpot.MaM = MaM
                            TmpSpot.Programme = .Cells(Row, 10).value
                            fk = .Cells(Row, 14).value
                            TmpSpot.Filmcode = fk
                            TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                            If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                                DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                                Campaign.PlannedSpots.Remove(TmpSpot.ID)
                            End If
                            If Not TmpSpot.Week Is Nothing Then

                                Trinity.Helper.SetFilmForSpot(TmpSpot)

                                TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                                'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = 0
                                TmpSpot.MyRating = 0
                                'If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                '    UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                                'Else
                                '    UI = 0
                                'End If
                                'If UI > 0 Then
                                '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = TmpSpot.MyRating / UI
                                'Else
                                '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                                'End If
                                TmpSpot.PriceNet = 0
                                TmpSpot.PriceGross = 0
                            End If
                        End If
                        Row = Row + 1
                    End While
                Case 2
                    d = CDate(.Cells(r, DateCol).value)
                    StartDate = d
                    While Not .Cells(r, DateCol).value Is Nothing
                        r = r + 1
                    End While
                    If WB.Sheets(1).Cells(r - 1, DateCol).value.GetType.Name = "Double" Then
                        d = Date.FromOADate(.Cells(r - 1, 7).value)
                    Else
                        d = CDate(.Cells(r - 1, DateCol).value)
                    End If
                    EndDate = d
                    Dim frmImport As New frmImport(Campaign.Channels("Eurosport"))
                    frmImport.dtFrom.Value = StartDate
                    frmImport.dtTo.Value = EndDate

                    frmImport.txtIndex.Text = 100
                    frmImport.Text = "Import Schedule - Eurosport"
                    frmImport.lblPath.Tag = WB.Path
                    frmImport.lblPath.Text = WB.Name
                    frmImport.grpBudget.Enabled = False
                    Dim tmpNetBudget As Single
                    tmpNetBudget = 0
                    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")
                    frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
                    If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                        CleanUp()
                        Exit Sub
                    End If
                    GoTo cmdImportSchedule_Click_Error
                    Idx = frmImport.txtIndex.Text
                    BT = frmImport.cmbBookingType.Text
                    Campaign.Channels("Eurosport").MainTarget.TargetName = Campaign.Channels("Eurosport").BookingTypes(BT).BuyingTarget.Target.TargetName
                    Campaign.Channels("Eurosport").MainTarget.Universe = Campaign.Channels("Eurosport").BookingTypes(BT).BuyingTarget.Target.Universe
                    If frmImport.chkReplace.Checked Then
                        For Each TmpSpot In Campaign.PlannedSpots
                            If TmpSpot.Channel.ChannelName = "Eurosport" Then
                                If TmpSpot.Bookingtype.Name = BT Then
                                    If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                        If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                            If Not TmpSpot.MatchedSpot Is Nothing Then
                                                TmpSpot.MatchedSpot.MatchedSpot = Nothing
                                            End If
                                            Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    End If
                    While Not .Cells(Row, DateCol).value Is Nothing AndAlso .Cells(Row, 3).value.ToString <> ""
                        d = .Cells(Row, DateCol).value
                        If .Cells(Row, DateCol).value.GetType.Name = "Double" Then
                            TmpDate = Date.FromOADate(d)
                        Else
                            TmpDate = CDate(d)
                        End If
                        If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                            TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                            TmpSpot.Channel = Campaign.Channels("Eurosport")
                            TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                            TmpSpot.AirDate = TmpDate.ToOADate
                            t = .Cells(Row, TimeCol).Text
                            MaM = 60 * Val(t.Substring(0, 2)) + Val(Mid(t, 4))
                            If MaM < 120 Then MaM = MaM + 1440
                            TmpSpot.MaM = MaM
                            TmpSpot.Programme = .Cells(Row, ProgCol).value
                            If FilmCol = 0 Then
                                fk = tmpFilmCode
                            Else
                                fk = .Cells(Row, FilmCol).value
                            End If
                            'If fk Is Nothing Then Stop
                            'fk = "UNKNOWN"
                            TmpSpot.Filmcode = fk
                            TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                            If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                                DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                                Campaign.PlannedSpots.Remove(TmpSpot.ID)
                            End If
                            If Not TmpSpot.Week Is Nothing Then

                                Trinity.Helper.SetFilmForSpot(TmpSpot)

                                TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                                'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = 0
                                TmpSpot.MyRating = 0
                                'If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                '    UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                                'Else
                                '    UI = 0
                                'End If
                                'If UI > 0 Then
                                '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = TmpSpot.MyRating / UI
                                'Else
                                '    TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                                'End If
                                TmpSpot.PriceNet = 0
                                TmpSpot.PriceGross = 0
                            End If
                        End If
                        Row = Row + 1
                    End While
            End Select

        End With
        On Error GoTo 0
        Exit Sub

cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
    End Sub

    Private Sub ImportTV2SportDenmarkSchedule(ByVal WB As CultureSafeExcel.Workbook)

        'Dim TargetString As String
        Dim StartDate As Date
        Dim EndDate As Date
        Dim tmpSpot As Trinity.cPlannedSpot
        Dim NetBudget As Decimal

        'Dim Row As Integer = 1

        Trinity.Helper.WriteToLogFile("ImportSchedule: Start reading TV2 Sport Denmark")

        'With WB.Sheets(1)

        '    Dim SplitDate() As String
        '    SplitDate = Strings.Split(.cells(4, 2).value.ToString, "-")
        '    StartDate = DateSerial(CInt(SplitDate(2)), CInt(SplitDate(1)), CInt(SplitDate(0)))
        '    SplitDate = Strings.Split(.cells(5, 2).value.ToString, "-")
        '    EndDate = DateSerial(CInt(SplitDate(2)), CInt(SplitDate(1)), CInt(SplitDate(0)))
        '    TargetString = .cells(8, 2).value
        '    NetBudget = .cells(21, 2).value
        'End With

        With WB.Sheets("Visningsoversigt")

            Dim row As Integer = 1
            Dim column As Integer = 1
            Dim dateCol As Integer = 0
            While .cells(row, 1).value <> "Channel"
                row += 1
            End While
            While .cells(row, column).value <> "Date"
                column += 1
            End While

            row += 1

            Dim SplitDate() As String

            If .cells(row, column).value.ToString.Length <= 10 Then
                SplitDate = Strings.Split(.cells(row, column).value.ToString, "-")
                StartDate = DateSerial(CInt(SplitDate(2)), CInt(SplitDate(1)), CInt(SplitDate(0)))
            Else
                StartDate = CDate(.cells(row, column).value)
            End If

            While Not .cells(row, column).value Is Nothing
                row += 1
            End While

            row -= 1

            If .cells(row, column).value.ToString.Length <= 10 Then
                SplitDate = Strings.Split(.cells(row, column).value.ToString, "-")
                EndDate = DateSerial(CInt(SplitDate(2)), CInt(SplitDate(1)), CInt(SplitDate(0)))
            Else
                EndDate = CDate(.cells(row, column).value)
            End If


            Dim frmImport As New frmImport(Campaign.Channels("TV2 Sport"))
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate

            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - TV2 Sport"
            'frmImport.lblPath.Tag = OriginalWordDocPath
            frmImport.lblPath.Text = ""
            Dim tmpNetBudget As Single

            Trinity.Helper.WriteToLogFile("ImportSchedule: Read budget")
            ' tmpNetBudget = .Range("D2").value.ToString.Replace(" ", "").ToString.Replace(" ", "").ToString.Replace(".", "").ToString.Replace(",", ".").Trim
            Trinity.Helper.WriteToLogFile("ImportSchedule: Set label")
            frmImport.lblConfirmationBudget.Text = Format(CDec(NetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Dim Idx As Single = frmImport.txtIndex.Text
            Dim BT As String = frmImport.cmbBookingType.Text

            If frmImport.chkReplace.Checked Then
                Trinity.Helper.WriteToLogFile("ImportSchedule: Remove old spots")
                For Each tmpSpot In Campaign.PlannedSpots
                    If tmpSpot.Channel.ChannelName = "TV2 Sport" Then
                        If tmpSpot.Bookingtype.Name = BT Then
                            If tmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If tmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not tmpSpot.MatchedBookedSpot Is Nothing Then
                                        tmpSpot.MatchedBookedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(tmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If

            Trinity.Helper.WriteToLogFile("ImportSchedule: Set budget")
            If frmImport.optReplaceBudget.Checked Then
                Campaign.Channels("TV2 Sport").BookingTypes(BT).ConfirmedNetBudget = CDec(tmpNetBudget)
            Else
                Campaign.Channels("TV2 Sport").BookingTypes(BT).ConfirmedNetBudget += CDec(tmpNetBudget)
            End If

            row = 1
            While Not .cells(row, 1).value = "Channel"
                row += 1
                If row > 100 Then Exit Sub
            End While

            row += 1

            While .Cells(row, 1).value <> Nothing

                Trinity.Helper.WriteToLogFile("ImportSchedule: Read spot on row " & row)

                'Dim SplitDate() As String
                'Dim DateStr As String
                Dim TmpDate As Date
                Dim TmpTime As String
                Dim MaM As Integer
                Dim fk As String

                ' SplitDate = Strings.Split(.cells(Row, 2).value, "-")
                ' TmpDate = DateSerial(CInt(SplitDate(2)), CInt(SplitDate(1)), CInt(SplitDate(0)))
                TmpDate = CDate(.cells(row, 2).value)
                If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                    tmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                    Trinity.Helper.WriteToLogFile("ImportSchedule: Set channel")
                    tmpSpot.Channel = Campaign.Channels("TV2 Sport")
                    Trinity.Helper.WriteToLogFile("ImportSchedule: Set Bookingtype")
                    tmpSpot.Bookingtype = tmpSpot.Channel.BookingTypes(BT)
                    Trinity.Helper.WriteToLogFile("ImportSchedule: Set AirDate")
                    tmpSpot.AirDate = TmpDate.ToOADate
                    Trinity.Helper.WriteToLogFile("ImportSchedule: Set Time")
                    .Columns(2).AutoFit()
                    TmpTime = .Cells(row, 3).Text
                    MaM = 60 * Val(TmpTime.Substring(0, 2)) + TmpTime.Substring(3, 2)
                    If MaM < 120 Then MaM = MaM + 1440
                    tmpSpot.MaM = MaM
                    Trinity.Helper.WriteToLogFile("ImportSchedule: Get Prog")
                    tmpSpot.Programme = .Cells(row, 6).value
                    tmpSpot.ProgAfter = .Cells(row, 6).value
                    tmpSpot.ProgBefore = .Cells(row, 6).value

                    Trinity.Helper.WriteToLogFile("ImportSchedule: Get film")
                    fk = .Cells(row, 5).value
                    tmpSpot.Filmcode = fk
                    tmpSpot.SpotLength = .Cells(row, 4).value

                    If fk Is Nothing Or fk = "" Then
                        tmpSpot.Filmcode = tmpSpot.SpotLength
                    End If

                    tmpSpot.Week = tmpSpot.Bookingtype.GetWeek(Date.FromOADate(tmpSpot.AirDate))

                    If Not tmpSpot.Week Is Nothing Then

                        Trinity.Helper.SetFilmForSpot(tmpSpot)

                    End If

                    Trinity.Helper.WriteToLogFile("ImportSchedule: Set Week")
                    tmpSpot.Week = tmpSpot.Bookingtype.GetWeek(Date.FromOADate(tmpSpot.AirDate))

                    If tmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(tmpSpot.AirDate) Then
                        Trinity.Helper.WriteToLogFile("ImportSchedule: Spot not in period!!")
                        DateOutOfPeriod.Add(DateOutOfPeriod.Count, tmpSpot.AirDate)
                        Campaign.PlannedSpots.Remove(tmpSpot.ID)
                    End If

                End If

                row = row + 1
            End While

        End With
    End Sub

    Private Sub ImportTVNorgeSchedule(ByVal WB As CultureSafeExcel.Workbook, ByVal OriginalWordDocPath As String)

        Dim TargStr As String
        Dim DateStr As String

        Dim StartDate As Date
        Dim EndDate As Date

        Dim TmpDate As Date
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpTime As String
        Dim MaM As Integer
        Dim fk As String
        Dim UI As Single

        On Error GoTo cmdImportSchedule_Click_Error

        Trinity.Helper.WriteToLogFile("ImportSchedule: Start reading TVNorge")


        With WB.Sheets(1)
            Trinity.Helper.WriteToLogFile("ImportSchedule: Find columns row")
            Dim Row As Integer = 1
            Dim ColumnsRow As Integer
            Dim c As Integer = 1
            While Trim(.Cells(Row, c).value) <> "CHAN"
                Row += 1
            End While
            Trinity.Helper.WriteToLogFile("ImportSchedule: Columns row is " & Row)
            ColumnsRow = Row

            Dim Chans As New List(Of String)
            While .Cells(Row, 1).Text <> "DAILY SEGMENT TOTALS"
                If .cells(Row, 1).Text IsNot Nothing AndAlso .cells(Row, 1).Text <> "CHAN" AndAlso .cells(Row, 1).Text <> "" Then
                    If Not Chans.Contains(.cells(Row, 1).Text) Then Chans.Add(.cells(Row, 1).Text)
                End If
                Row += 1
            End While

            For Each TmpChan As String In Chans
                Dim Stat As String = ""
                Select Case TmpChan
                    Case "TVN"
                        Stat = "TVNorge"
                    Case "FEM"
                        Stat = "FEM"
                    Case "MAX"
                        Stat = "MAX"
                    Case Else
                        Stat = ""
                End Select
                If Campaign.Channels(Stat) Is Nothing Then
                    Windows.Forms.MessageBox.Show("Unknown channel " & Stat, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    If Not Stat = "" Then
                        Campaign.Channels(Stat).IsVisible = True

                        TargStr = .range("D6").value
                        Trinity.Helper.WriteToLogFile("ImportSchedule: Found Target " & TargStr)
                        DateStr = .range("D1").value
                        Trinity.Helper.WriteToLogFile("ImportSchedule: Found Date " & DateStr)

                        If TargStr.StartsWith("PERSONER") Then
                            Campaign.Channels(Stat).MainTarget.TargetName = TargStr.Substring(7)
                        ElseIf TargStr.StartsWith("KVINNER") Then
                            Campaign.Channels(Stat).MainTarget.TargetName = TargStr.Substring(7)
                        ElseIf TargStr.StartsWith("MENN") Then
                            Campaign.Channels(Stat).MainTarget.TargetName = TargStr.Substring(7)
                        End If
                        Trinity.Helper.WriteToLogFile("ImportSchedule: Parse target")
                        Campaign.Channels(Stat).MainTarget.Universe = Campaign.Channels(Stat).BuyingUniverse

                        Trinity.Helper.WriteToLogFile("ImportSchedule: Set Start- and end-date")
                        StartDate = CDate(DateStr.Substring(6, 4) & "-" & DateStr.Substring(3, 2) & "-" & DateStr.Substring(0, 2))
                        EndDate = CDate(DateStr.Substring(21, 4) & "-" & DateStr.Substring(18, 2) & "-" & DateStr.Substring(15, 2))

                        Trinity.Helper.WriteToLogFile("ImportSchedule: Show Import window")
                        Dim frmImport As New frmImport(Campaign.Channels(Stat))
                        frmImport.dtFrom.Value = StartDate
                        frmImport.dtTo.Value = EndDate

                        frmImport.txtIndex.Text = 100
                        frmImport.Text = "Import Schedule - " & Stat
                        frmImport.lblPath.Tag = OriginalWordDocPath
                        frmImport.lblPath.Text = ""
                        Dim tmpNetBudget As Single
                        'Trinity.Helper.WriteToLogFile("ImportSchedule: Set culture to Sweden")
                        'System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")

                        Trinity.Helper.WriteToLogFile("ImportSchedule: Set culture to English")
                        Trinity.Helper.WriteToLogFile("ImportSchedule: Read budget")
                        tmpNetBudget = .Range("D2").value.ToString.Replace(" ", "").ToString.Replace(" ", "").ToString.Replace(".", "").ToString.Replace(",", ".").Trim
                        Trinity.Helper.WriteToLogFile("ImportSchedule: Set label")
                        frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
                        If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then GoTo SkipRead
                        Dim Idx As Single = frmImport.txtIndex.Text
                        Dim BT As String = frmImport.cmbBookingType.Text

                        If frmImport.chkReplace.Checked Then
                            Trinity.Helper.WriteToLogFile("ImportSchedule: Remove old spots")
                            For Each TmpSpot In Campaign.PlannedSpots
                                If TmpSpot.Channel.ChannelName = Stat Then
                                    If TmpSpot.Bookingtype.Name = BT Then
                                        If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                            If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                                If Not TmpSpot.MatchedBookedSpot Is Nothing Then
                                                    TmpSpot.MatchedBookedSpot.MatchedSpot = Nothing
                                                End If
                                                Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                        End If
                        Trinity.Helper.WriteToLogFile("ImportSchedule: Set budget")
                        If frmImport.optReplaceBudget.Checked Then
                            Campaign.Channels(Stat).BookingTypes(BT).ConfirmedNetBudget = CDec(tmpNetBudget)
                        Else
                            Campaign.Channels(Stat).BookingTypes(BT).ConfirmedNetBudget += CDec(tmpNetBudget)
                        End If
                        Trinity.Helper.WriteToLogFile("ImportSchedule: Get contract number")
                        Campaign.Channels(Stat).BookingTypes(BT).ContractNumber = .Range("B1").value

                        Row = ColumnsRow
                        c = 1
                        While Trim(.Cells(Row, c).value) <> "TVR" And c < 200
                            c = c + 1
                        End While
                        Dim RatingCol As Integer = c
                        Trinity.Helper.WriteToLogFile("ImportSchedule: RatingCol is " & c)

                        c = 1
                        While Trim(.Cells(Row, c).value) <> "COST" And c < 200
                            c = c + 1
                        End While
                        Dim NetCol As Integer = c
                        Trinity.Helper.WriteToLogFile("ImportSchedule: NetCol is " & c)

                        c = 1
                        While Trim(.Cells(Row, c).value) <> "STATUS" And c < 200
                            c = c + 1
                        End While
                        Dim RemarkCol As Integer = c
                        Trinity.Helper.WriteToLogFile("ImportSchedule: RemarkCol is " & c)

                        c = 1
                        While Trim(.Cells(Row, c).value) <> "LEN" And c < 200
                            c = c + 1
                        End While
                        Dim LengthCol As Integer = c
                        Trinity.Helper.WriteToLogFile("ImportSchedule: LengthCol is " & c)

                        c = 1
                        While Trim(.Cells(Row, c).value) <> "COPY NUMBER" And c < 200
                            c = c + 1
                        End While
                        Dim FilmCol As Integer = c
                        Trinity.Helper.WriteToLogFile("ImportSchedule: FilmCol is " & c)

                        c = 1
                        While Trim(.Cells(Row, c).value) <> "PROGRAMME BEFORE" And c < 200
                            c = c + 1
                        End While
                        Dim ProgBeforeCol As Integer = c
                        Trinity.Helper.WriteToLogFile("ImportSchedule: ProgBeforeCol is " & c)

                        c = 1
                        While Trim(.Cells(Row, c).value) <> "PROGRAMME AFTER" And c < 200
                            c = c + 1
                        End While
                        Dim ProgAfterCol As Integer = c
                        Trinity.Helper.WriteToLogFile("ImportSchedule: ProgAfterCol is " & c)

                        c = 1
                        While Trim(.Cells(Row, c).value) <> "TX/DATE" And c < 200
                            c = c + 1
                        End While
                        Dim DateCol As Integer = c
                        Trinity.Helper.WriteToLogFile("ImportSchedule: DateCol is " & c)

                        c = 1
                        While Trim(.Cells(Row, c).value) <> "TX/TIME" And c < 200
                            c = c + 1
                        End While
                        Dim TimeCol As Integer = c
                        Trinity.Helper.WriteToLogFile("ImportSchedule: TimeCol is " & c)

                        Trinity.Helper.WriteToLogFile("ImportSchedule: Start reading spots")
                        While .Cells(Row, 1).value <> "DAILY SEGMENT TOTALS"
                            If .cells(Row, 1).Text = TmpChan Then
                                Trinity.Helper.WriteToLogFile("ImportSchedule: Read spot on row " & Row)
                                DateStr = .cells(Row, DateCol).Text
                                TmpDate = CDate(DateStr.Substring(6, 4) & "-" & DateStr.Substring(3, 2) & "-" & DateStr.Substring(0, 2))
                                If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                                    TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                                    Trinity.Helper.WriteToLogFile("ImportSchedule: Set channel")
                                    TmpSpot.Channel = Campaign.Channels(Stat)
                                    Trinity.Helper.WriteToLogFile("ImportSchedule: Set Bookingtype")
                                    TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                                    Trinity.Helper.WriteToLogFile("ImportSchedule: Set AirDate")
                                    TmpSpot.AirDate = TmpDate.ToOADate
                                    Trinity.Helper.WriteToLogFile("ImportSchedule: Set Time")
                                    .Columns(2).AutoFit()
                                    TmpTime = .Cells(Row, TimeCol).Text
                                    MaM = 60 * Val(TmpTime.Substring(0, 2)) + TmpTime.Substring(3, 2)
                                    If MaM < 120 Then MaM = MaM + 1440
                                    TmpSpot.MaM = MaM
                                    Trinity.Helper.WriteToLogFile("ImportSchedule: Get Prog")
                                    TmpSpot.Programme = .Cells(Row, ProgAfterCol).value
                                    TmpSpot.ProgAfter = .Cells(Row, ProgAfterCol).value
                                    TmpSpot.ProgBefore = .Cells(Row, ProgBeforeCol).value

                                    Trinity.Helper.WriteToLogFile("ImportSchedule: Get film")
                                    fk = .Cells(Row, FilmCol).value
                                    TmpSpot.Filmcode = fk
                                    TmpSpot.Remark = .Cells(Row, RemarkCol).value
                                    TmpSpot.SpotLength = .Cells(Row, LengthCol).value

                                    If fk Is Nothing Or fk = "" OrElse fk.ToUpper = "IKKE ALLOKERT" Then
                                        TmpSpot.Filmcode = TmpSpot.SpotLength
                                    End If
                                    Trinity.Helper.WriteToLogFile("ImportSchedule: Set Week")
                                    TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))

                                    If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                                        Trinity.Helper.WriteToLogFile("ImportSchedule: Spot not in period!!")
                                        DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                                        Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                    End If
                                    If Not TmpSpot.Week Is Nothing Then
                                        Trinity.Helper.WriteToLogFile("ImportSchedule: Week is found")

                                        Trinity.Helper.SetFilmForSpot(TmpSpot)

                                        TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = .Cells(Row, RatingCol).Value
                                        'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = .Cells(Row, RatingCol).Value
                                        If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                            UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                                        Else
                                            UI = 0
                                        End If
                                        Trinity.Helper.WriteToLogFile("ImportSchedule: Get Rating")
                                        TmpSpot.MyRating = .Cells(Row, RatingCol).Value * UI

                                        Trinity.Helper.WriteToLogFile("ImportSchedule: Get Budget")
                                        TmpSpot.PriceNet = CSng(.Cells(Row, NetCol).value.ToString.Trim.Replace(".", "").Replace(",", "."))
                                    End If
                                End If
                            End If
                            Row = Row + 1
                        End While
                        If frmImport.chkEvaluate.Checked Then
                            frmEvaluateSpecifics.MdiParent = frmMain
                            frmEvaluateSpecifics.Show()
                        End If
                    End If
                End If
SkipRead:
            Next
        End With
        If DateOutOfPeriod.Count > 0 Then
            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
            For i As Integer = 0 To DateOutOfPeriod.Count
                Msg += Format(Date.FromOADate(DateOutOfPeriod(i)), "Short date") & vbCrLf
            Next
            Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        On Error GoTo 0
        Exit Sub

cmdImportSchedule_Click_Error:
        Trinity.Helper.WriteToLogFile("ImportSchedule: ERROR (" & Err.Number & "): " & Err.Description)
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
        Resume
    End Sub

    Private Sub ImportTV2Schedule(ByVal WB As CultureSafeExcel.Workbook, Optional ByVal Sisterchannel As String = "TV2")

        Dim TargStr As String
        Dim DateStr As String

        Dim StartDate As Date
        Dim EndDate As Date

        Dim TmpDate As Date
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpTime As String
        Dim MaM As Integer
        Dim fk As String
        Dim UI As Single
        Dim FilmList As New Hashtable

        On Error GoTo cmdImportSchedule_Click_Error

        Campaign.Channels(Sisterchannel).IsVisible = True
        With WB.Sheets(1)
            Campaign.Channels(Sisterchannel).MainTarget.TargetName = "12+"
            Campaign.Channels(Sisterchannel).MainTarget.Universe = Campaign.Channels(Sisterchannel).BuyingUniverse

            DateStr = .range("F7").Text
            If DateStr.IndexOf("-") = 4 Then
                StartDate = CDate(DateStr)
            Else
                StartDate = CDate(DateStr.Substring(6, 4) & "-" & DateStr.Substring(3, 2) & "-" & DateStr.Substring(0, 2))
            End If

            DateStr = .range("F8").Text
            If DateStr.IndexOf("-") = 4 Then
                EndDate = CDate(DateStr)
            Else
                EndDate = CDate(DateStr.Substring(6, 4) & "-" & DateStr.Substring(3, 2) & "-" & DateStr.Substring(0, 2))
            End If

            Dim frmImport As New frmImport(Campaign.Channels(Sisterchannel))
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate

            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - " & Sisterchannel
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name
            Dim tmpNetBudget As Single
            tmpNetBudget = .Range("F6").value.ToString.Replace(" ", "").ToString.Replace(",", ".")
            frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Dim Idx As Single = frmImport.txtIndex.Text
            Dim BT As String = frmImport.cmbBookingType.Text

            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = Sisterchannel Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedBookedSpot Is Nothing Then
                                        TmpSpot.MatchedBookedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            If frmImport.optReplaceBudget.Checked Then
                Campaign.Channels(Sisterchannel).BookingTypes(BT).ConfirmedNetBudget = CDec(tmpNetBudget)
            Else
                Campaign.Channels(Sisterchannel).BookingTypes(BT).ConfirmedNetBudget += CDec(tmpNetBudget)
            End If
            Campaign.Channels(Sisterchannel).BookingTypes(BT).ContractNumber = ""

            Dim Row As Integer = 18
            While Not .cells(Row, 1).value Is Nothing
                FilmList.Add(.cells(Row, 1).Text.ToString.Substring(0, 1), .cells(Row, 2).Text)
                Row += 1
            End While

            Dim c As Integer = 1
            While Trim(.Cells(Row, c).value) <> "Dato"
                Row += 1
            End While
            c = 1
            While Trim(.Cells(Row, c).value) <> "Rating" And c < 200
                c = c + 1
            End While
            Dim RatingCol As Integer = c
            c = 1
            While Trim(.Cells(Row, c).value) <> "Pris" And c < 200
                c = c + 1
            End While
            Dim NetCol As Integer = c
            c = 1
            While Trim(.Cells(Row, c).value) <> "Lengde" And c < 200
                c = c + 1
            End While
            Dim LengthCol As Integer = c
            c = 1
            While Trim(.Cells(Row, c).value) <> "Film" And c < 200
                c = c + 1
            End While
            Dim FilmCol As Integer = c
            c = 1
            While Trim(.Cells(Row, c).value) <> "Program før" And c < 200
                c = c + 1
            End While
            Dim ProgBeforeCol As Integer = c
            c = 1
            While Trim(.Cells(Row, c).value) <> "Program etter" And c < 200
                c = c + 1
            End While
            Dim ProgAfterCol As Integer = c
            c = 1
            While Trim(.Cells(Row, c).value) <> "Dato" And c < 200
                c = c + 1
            End While
            Dim DateCol As Integer = c
            c = 1
            While Trim(.Cells(Row, c).value) <> "Tid" And c < 200
                c = c + 1
            End While
            Dim TimeCol As Integer = c
            Row += 2
            While Not ((.Cells(Row, 1).value Is Nothing OrElse .cells(Row, 1).value = "") AndAlso (.Cells(Row - 1, 1).value Is Nothing OrElse .cells(Row - 1, 1).value = "") AndAlso (.Cells(Row + 1, 1).value Is Nothing OrElse .cells(Row + 1, 1).value = ""))
                If Not .cells(Row, 1).value Is Nothing AndAlso Not .cells(Row, 1).value = "" Then
                    DateStr = .cells(Row, DateCol).Text
                    TmpDate = CDate("20" & DateStr.Substring(6, 2) & "-" & DateStr.Substring(3, 2) & "-" & DateStr.Substring(0, 2))
                    If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                        TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                        TmpSpot.Channel = Campaign.Channels(Sisterchannel)
                        TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                        TmpSpot.AirDate = TmpDate.ToOADate
                        TmpTime = Trim(.Cells(Row, TimeCol).Text)
                        MaM = 60 * Val(TmpTime.Substring(0, 2)) + TmpTime.Substring(3, 2)
                        If MaM < 120 Then MaM = MaM + 1440
                        TmpSpot.MaM = MaM
                        TmpSpot.Programme = .Cells(Row, ProgAfterCol).value
                        TmpSpot.ProgAfter = .Cells(Row, ProgAfterCol).value
                        TmpSpot.ProgBefore = .Cells(Row, ProgBeforeCol).value
                        TmpSpot.SpotLength = .Cells(Row, LengthCol).value

                        fk = .Cells(Row, FilmCol).value
                        TmpSpot.Filmcode = FilmList(fk)

                        'We can not save ikke allokert since more that one filmcode can have that status.
                        If TmpSpot.Filmcode.ToUpper = "IKKE ALLOKERT" Then
                            TmpSpot.Filmcode = .Cells(Row, FilmCol).value
                        End If


                        TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))

                        If TmpSpot.Week Is Nothing Then
                            If Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                                DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                            End If
                            Campaign.PlannedSpots.Remove(TmpSpot.ID)
                        Else

                            Trinity.Helper.SetFilmForSpot(TmpSpot)

                            TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = .Cells(Row, RatingCol).Value
                            'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = .Cells(Row, RatingCol).Value
                            If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                            Else
                                UI = 0
                            End If
                            TmpSpot.MyRating = .Cells(Row, RatingCol).Value * UI

                            TmpSpot.PriceNet = .Cells(Row, NetCol).value
                        End If
                    End If
                End If
                Row = Row + 1
            End While
            If frmImport.chkEvaluate.Checked Then
                frmEvaluateSpecifics.MdiParent = frmMain
                frmEvaluateSpecifics.Show()
            End If
        End With
        If DateOutOfPeriod.Count > 0 Then
            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
            For i As Integer = 0 To DateOutOfPeriod.Count
                Msg += Format(Date.FromOADate(DateOutOfPeriod(i)), "Short date") & vbCrLf
            Next
            Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        On Error GoTo 0
        Exit Sub

cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
        Resume
    End Sub

    Private Sub ImportTV2DKSchedule(ByVal WB As CultureSafeExcel.Workbook)

        Dim TargStr As String
        Dim DateStr As String

        Dim StartDate As Date
        Dim EndDate As Date

        Dim TmpDate As Date
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpTime As String
        Dim MaM As Integer
        Dim fk As String
        Dim UI As Single
        Dim IsSpecific As Boolean = False
        Dim TopRow As Integer

        On Error GoTo cmdImportSchedule_Click_Error

        With WB.Sheets(1)
            Dim row As Integer = 1
            While .cells(row, 5).value Is Nothing OrElse .cells(row, 5).value = ""
                row += 1
            End While
            If row = 2 Then IsSpecific = True
            Dim Stat As String = .cells(row, 5).value
            If Campaign.Channels(Stat) Is Nothing Then
                If Stat.IndexOf("TV2") > -1 Then
                    Stat = Stat.Replace("TV2", "TV 2")
                ElseIf Stat.IndexOf("TV 2") > -1 Then
                    Stat = Stat.Replace("TV 2", "TV2")
                End If
            End If
            Campaign.Channels(Stat).IsVisible = True
            Campaign.Channels(Stat).MainTarget.TargetName = "12+"
            Campaign.Channels(Stat).MainTarget.Universe = Campaign.Channels(Stat).BuyingUniverse

            Dim tmpNetBudget As Single = 0
            row = 1
            If IsSpecific Then
                While .cells(row, 1).Text <> "Kampagnelinier"
                    row += 1
                End While
            Else
                While .cells(row, 1).Text <> "Uge"
                    row += 1
                End While
            End If

            Dim LengthCol As Integer
            Dim FilmCol As Integer
            Dim ProgBeforeCol As Integer
            Dim ProgAfterCol As Integer
            Dim DateCol As Integer
            Dim NetCol As Integer

            If Not IsSpecific Then
                Dim c As Integer = 1
                While Trim(.Cells(row, c).value) <> "SpotLen" And c < 200
                    c = c + 1
                End While
                LengthCol = c
                c = 1
                While Trim(.Cells(row, c).value) <> "Titel" And c < 200
                    c = c + 1
                End While
                FilmCol = c
                c = 1
                While Trim(.Cells(row, c).value) <> "ProgramFoer" And c < 200
                    c = c + 1
                End While
                ProgBeforeCol = c
                c = 1
                While Trim(.Cells(row, c).value) <> "ProgramEfter" And c < 200
                    c = c + 1
                End While
                ProgAfterCol = c
                c = 1
                While Trim(.Cells(row, c).value) <> "Sendt" And c < 200
                    c = c + 1
                End While
                DateCol = c
                c = 1
            Else
                LengthCol = 7
                FilmCol = 15
                ProgBeforeCol = 13
                ProgAfterCol = 14
                DateCol = 3
                NetCol = 11
            End If
            TopRow = row
            row += 1

            DateStr = Date.FromOADate(Convert.ToSingle(.Cells(row, DateCol).Formula, New System.Globalization.CultureInfo("en-US")))
            DateStr = CDate(DateStr).Year & "-" & Format(CDate(DateStr).Month, "00") & "-" & Format(CDate(DateStr).Day, "00")
            StartDate = CDate(DateStr)

            While Val(.cells(row, 1).value) <> 0
                tmpNetBudget += Convert.ToSingle(.Cells(row, 11).Value, New System.Globalization.CultureInfo("en-US"))
                row += 1
            End While
            row -= 1
            DateStr = Date.FromOADate(Convert.ToSingle(.Cells(row, DateCol).Formula, New System.Globalization.CultureInfo("en-US")))
            DateStr = CDate(DateStr).Year & "-" & Format(CDate(DateStr).Month, "00") & "-" & Format(CDate(DateStr).Day, "00")
            EndDate = CDate(DateStr)

            Dim frmImport As New frmImport(Campaign.Channels(Stat))
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate

            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - " & Stat
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name
            If Not IsSpecific Then tmpNetBudget = Convert.ToSingle(.Range("G4").Value, New System.Globalization.CultureInfo("en-US"))
            frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
            Dim Idx As Single = frmImport.txtIndex.Text
            Dim BT As String = frmImport.cmbBookingType.Text

            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = Stat Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedBookedSpot Is Nothing Then
                                        TmpSpot.MatchedBookedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If

            If frmImport.optReplaceBudget.Checked Then
                Campaign.Channels(Stat).BookingTypes(BT).ConfirmedNetBudget = CDec(tmpNetBudget)
            Else
                Campaign.Channels(Stat).BookingTypes(BT).ConfirmedNetBudget += CDec(tmpNetBudget)
            End If
            Campaign.Channels(Stat).BookingTypes(BT).ContractNumber = ""

            row = TopRow + 1
            While Not ((.Cells(row, 1).value Is Nothing OrElse .cells(row, 1).Text = "") OrElse (Not IsSpecific AndAlso .Cells(row, 1).Text.ToString.Substring(0, 3) = "---") OrElse (IsSpecific AndAlso .Cells(row, 1).Text = "Spotkoder"))
                DateStr = Date.FromOADate(Convert.ToSingle(.Cells(row, DateCol).Formula, New System.Globalization.CultureInfo("en-US")))
                DateStr = CDate(DateStr).Year & "-" & Format(CDate(DateStr).Month, "00") & "-" & Format(CDate(DateStr).Day, "00")
                TmpDate = CDate(DateStr)
                If (TmpDate >= frmImport.dtFrom.Value AndAlso TmpDate <= frmImport.dtTo.Value) AndAlso (Not IsSpecific OrElse .cells(row, 2).Text = "B") Then
                    TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                    TmpSpot.Channel = Campaign.Channels(Stat)
                    TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                    TmpSpot.AirDate = TmpDate.ToOADate
                    Dim _date As Date = Date.FromOADate(Convert.ToSingle(.Cells(row, DateCol).Formula, New System.Globalization.CultureInfo("en-US")))
                    TmpTime = Format(_date.Hour, "00") & ":" & Format(_date.Minute, "00")
                    MaM = 60 * Val(TmpTime.Substring(0, 2)) + TmpTime.Substring(3, 2)
                    If MaM < 360 Then
                        MaM = MaM + 1440
                        TmpDate = TmpDate.AddDays(-1)
                        TmpSpot.AirDate = TmpDate.ToOADate
                    End If

                    TmpSpot.MaM = MaM
                    TmpSpot.Programme = .Cells(row, ProgAfterCol).value
                    TmpSpot.ProgAfter = .Cells(row, ProgAfterCol).value
                    TmpSpot.ProgBefore = .Cells(row, ProgBeforeCol).value
                    TmpSpot.SpotLength = .Cells(row, LengthCol).value

                    fk = .Cells(row, FilmCol).value
                    If fk Is Nothing Then fk = ""
                    TmpSpot.Filmcode = fk

                    TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))

                    If TmpSpot.Week Is Nothing Then
                        If Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                            DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                        End If
                        Campaign.PlannedSpots.Remove(TmpSpot.ID)
                    Else

                        Trinity.Helper.SetFilmForSpot(TmpSpot)

                        TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                        'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = .Cells(Row, RatingCol).Value
                        If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                            UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                        Else
                            UI = 0
                        End If
                        TmpSpot.MyRating = 0

                        If IsSpecific Then TmpSpot.PriceNet = .cells(row, NetCol).value.ToString.Replace(",", ".")

                    End If
                End If
                row = row + 1
            End While
            '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
            If frmImport.chkEvaluate.Checked Then
                frmEvaluateSpecifics.MdiParent = frmMain
                frmEvaluateSpecifics.Show()
            End If
        End With
        If DateOutOfPeriod.Count > 0 Then
            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
            For i As Integer = 0 To DateOutOfPeriod.Count
                Msg += Format(Date.FromOADate(DateOutOfPeriod(i)), "Short date") & vbCrLf
            Next
            Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        On Error GoTo 0
        Exit Sub

cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
        Resume
    End Sub

    Private Sub ImportTV2ZebraSchedule(ByVal WB As CultureSafeExcel.Workbook)

        Dim TargStr As String
        Dim DateStr As String

        Dim StartDate As Date
        Dim EndDate As Date

        Dim TmpDate As Date
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpTime As String
        Dim MaM As Integer
        Dim fk As String
        Dim UI As Single
        Dim FilmList As New Hashtable
        Dim Chan As String

        On Error GoTo cmdImportSchedule_Click_Error

        Campaign.Channels("TV2 Zebra").IsVisible = True
        With WB.Sheets(1)

            DateStr = .range("F7").Text
            If DateStr.IndexOf("-") = 4 Then
                StartDate = CDate(DateStr)
            Else
                StartDate = CDate(DateStr.Substring(6, 4) & "-" & DateStr.Substring(3, 2) & "-" & DateStr.Substring(0, 2))
            End If

            DateStr = .range("F8").Text
            If DateStr.IndexOf("-") = 4 Then
                EndDate = CDate(DateStr)
            Else
                EndDate = CDate(DateStr.Substring(6, 4) & "-" & DateStr.Substring(3, 2) & "-" & DateStr.Substring(0, 2))
            End If

            Dim frmImport As New frmImport(Campaign.Channels("TV2 Zebra"))
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate

            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - TV2 Zebra"
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name
            Dim tmpNetBudget As Single
            tmpNetBudget = .Range("F6").value.ToString.Replace(" ", "").ToString.Replace(",", ".")
            frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Dim Idx As Single = frmImport.txtIndex.Text
            Dim BT As String = frmImport.cmbBookingType.Text

            If Campaign.Channels("TV2 Zebra").BookingTypes(BT).BookIt Then
                Chan = "TV2 Zebra"
                Campaign.Channels("TV2 Zebra").MainTarget.TargetName = "12+"
            Else
                Chan = "TV2"
            End If
            Campaign.Channels("TV2 Zebra").MainTarget.Universe = Campaign.Channels("TV2 Zebra").BuyingUniverse

            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = "TV2 Zebra" OrElse (TmpSpot.Channel.ChannelName = "TV2" AndAlso TmpSpot.Remark = "TV2 Zebra") Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedBookedSpot Is Nothing Then
                                        TmpSpot.MatchedBookedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            If frmImport.optReplaceBudget.Checked Then
                Campaign.Channels(Chan).BookingTypes(BT).ConfirmedNetBudget = CDec(tmpNetBudget)
            Else
                Campaign.Channels(Chan).BookingTypes(BT).ConfirmedNetBudget += CDec(tmpNetBudget)
            End If

            Dim Row As Integer = 18
            While Not .cells(Row, 1).value Is Nothing
                FilmList.Add(.cells(Row, 1).Text.ToString.Substring(0, 1), .cells(Row, 2).Text)
                Row += 1
            End While

            Dim c As Integer = 1
            While Trim(.Cells(Row, c).value) <> "Dato"
                Row += 1
            End While
            c = 1
            While Trim(.Cells(Row, c).value) <> "Rating" And c < 200
                c = c + 1
            End While
            Dim RatingCol As Integer = c
            c = 1
            While Trim(.Cells(Row, c).value) <> "Pris" And c < 200
                c = c + 1
            End While
            Dim NetCol As Integer = c
            c = 1
            While Trim(.Cells(Row, c).value) <> "Lengde" And c < 200
                c = c + 1
            End While
            Dim LengthCol As Integer = c
            c = 1
            While Trim(.Cells(Row, c).value) <> "Film" And c < 200
                c = c + 1
            End While
            Dim FilmCol As Integer = c
            c = 1
            While Trim(.Cells(Row, c).value) <> "Program før" And c < 200
                c = c + 1
            End While
            Dim ProgBeforeCol As Integer = c
            c = 1
            While Trim(.Cells(Row, c).value) <> "Program etter" And c < 200
                c = c + 1
            End While
            Dim ProgAfterCol As Integer = c
            c = 1
            While Trim(.Cells(Row, c).value) <> "Dato" And c < 200
                c = c + 1
            End While
            Dim DateCol As Integer = c
            c = 1
            While Trim(.Cells(Row, c).value) <> "Tid" And c < 200
                c = c + 1
            End While
            Dim TimeCol As Integer = c
            Row += 2
            While Not ((.Cells(Row, 1).value Is Nothing OrElse .cells(Row, 1).value = "") AndAlso (.Cells(Row - 1, 1).value Is Nothing OrElse .cells(Row - 1, 1).value = "") AndAlso (.Cells(Row + 1, 1).value Is Nothing OrElse .cells(Row + 1, 1).value = ""))
                If Not .cells(Row, 1).value Is Nothing AndAlso Not .cells(Row, 1).value = "" Then
                    DateStr = .cells(Row, DateCol).Text
                    TmpDate = CDate("20" & DateStr.Substring(6, 2) & "-" & DateStr.Substring(3, 2) & "-" & DateStr.Substring(0, 2))
                    If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                        TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                        TmpSpot.Channel = Campaign.Channels(Chan)
                        TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                        TmpSpot.AirDate = TmpDate.ToOADate
                        TmpTime = Trim(.Cells(Row, TimeCol).Text)
                        MaM = 60 * Val(TmpTime.Substring(0, 2)) + TmpTime.Substring(3, 2)
                        If MaM < 120 Then MaM = MaM + 1440
                        TmpSpot.MaM = MaM
                        TmpSpot.Programme = .Cells(Row, ProgAfterCol).value
                        TmpSpot.ProgAfter = .Cells(Row, ProgAfterCol).value
                        TmpSpot.ProgBefore = .Cells(Row, ProgBeforeCol).value
                        TmpSpot.SpotLength = .Cells(Row, LengthCol).value

                        fk = .Cells(Row, FilmCol).value
                        TmpSpot.Filmcode = FilmList(fk)

                        'We can not save ikke allokert since more that one filmcode can have that status.
                        If TmpSpot.Filmcode.ToUpper = "IKKE ALLOKERT" Then
                            TmpSpot.Filmcode = .Cells(Row, FilmCol).value
                        End If

                        If Chan = "TV2" Then
                            TmpSpot.Remark = "TV2 Zebra"
                        End If

                        TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))

                        If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                            DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                            Campaign.PlannedSpots.Remove(TmpSpot.ID)
                        End If
                        If Not TmpSpot.Week Is Nothing Then

                            Trinity.Helper.SetFilmForSpot(TmpSpot)

                            TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = .Cells(Row, RatingCol).Value
                            'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = .Cells(Row, RatingCol).Value
                            If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                                UI = (TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                            Else
                                UI = 0
                            End If
                            TmpSpot.MyRating = .Cells(Row, RatingCol).Value * UI

                            TmpSpot.PriceNet = .Cells(Row, NetCol).value
                        End If
                    End If
                End If
                Row = Row + 1
            End While
            If frmImport.chkEvaluate.Checked Then
                frmEvaluateSpecifics.MdiParent = frmMain
                frmEvaluateSpecifics.Show()
            End If
        End With
        If DateOutOfPeriod.Count > 0 Then
            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
            For i As Integer = 0 To DateOutOfPeriod.Count
                Msg += Format(Date.FromOADate(DateOutOfPeriod(i)), "Short date") & vbCrLf
            Next
            Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        On Error GoTo 0
        Exit Sub

cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
        Resume
    End Sub

    Private Sub ImportTheVoice(ByVal WB As CultureSafeExcel.Workbook)
        Dim StartDate As Date
        Dim EndDate As Date
        Dim spot As Trinity.cPlannedSpot
        Dim budget As Integer
        Dim TmpTime As String
        Dim MaM As String

        With WB.Sheets(1)

            Dim col As Integer = 6
            Dim row As Integer
            Dim i As Integer = 0
            Dim j As Integer = 0

            Dim tmpStr As String
            For i = 0 To 3
                row = 5
                For j = 0 To 10
                    tmpStr = .cells(row, col).value
                    If Not tmpStr Is Nothing Then
                        'get start date
                        If tmpStr.ToUpper = "START DATE" Then
                            StartDate = .cells(row, col + 2).value
                        End If
                        'get end date
                        If tmpStr.ToUpper = "END DATE" Then
                            EndDate = .cells(row, col + 2).value
                        End If
                        'get budget
                        If tmpStr.ToUpper = "BUDGET" Then
                            Dim b As String = ""
                            tmpStr = .cells(row, col + 2).value
                            For Each c As Char In tmpStr
                                If Char.IsNumber(c) Then
                                    b = b & c
                                End If
                            Next
                            budget = CInt(b)
                        End If
                    End If
                    row += 1
                Next
                col += 1
            Next
            If StartDate.ToOADate = 0 Then
                StartDate = Date.FromOADate(Campaign.StartDate)
            End If
            If EndDate.ToOADate = 0 Then
                EndDate = Date.FromOADate(Campaign.endDate)
            End If
            Dim frmImport As New frmImport(Campaign.Channels("The Voice"))
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate

            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - The Voice"
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name
            frmImport.lblConfirmationBudget.Text = Format(budget, "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Dim Idx As Single = frmImport.txtIndex.Text
            Dim BT As String = frmImport.cmbBookingType.Text

            'add the budget
            If frmImport.optReplaceBudget.Checked Then
                Campaign.Channels("The Voice").BookingTypes(BT).ConfirmedNetBudget = CDec(budget)
            Else
                Campaign.Channels("The Voice").BookingTypes(BT).ConfirmedNetBudget += CDec(budget)
            End If

            Dim tmpDate As Date
            Dim d As String
            Dim IterateCounter As Integer = 0
            row = 1
row_iteration:
            While .cells(row, 1).value Is Nothing
                row += 1
            End While
            If Not .cells(row, 1).value.ToString.LastIndexOf("-") > 0 Then
                row += 1
                GoTo row_iteration
            End If

new_Spotchunk:
            d = .Cells(row, 1).value

            Dim s As String
            Dim s2 As String
            Dim s3 As String
            s = Right(d, 2)
            s2 = d.Substring(0, d.IndexOf("-"))
            s3 = d.Substring(d.IndexOf("-") + 1, d.LastIndexOf("-") - d.IndexOf("-") - 1)
            d = s3 & "-" & s2 & "-" & s
            tmpDate = d


            'tmpDate now holds the date for all spots until a new date is found
            row += 1

            While IterateCounter < 5
                'if the row is empty we increase the counter, if not we read the spot
                If .cells(row, 1).value Is Nothing Then
                    IterateCounter += 1
                Else
                    IterateCounter = 0
                    d = .cells(row, 1).value
                    If d.LastIndexOf("-") > 0 Then
                        'we have a date
                        GoTo new_Spotchunk
                    End If

                    If Not .cells(row, 4).value Is Nothing Then
                        spot = Campaign.PlannedSpots.Add(CreateGUID)
                        spot.Channel = Campaign.Channels("The Voice")
                        spot.Bookingtype = spot.Channel.BookingTypes(BT)
                        spot.AirDate = tmpDate.ToOADate
                        TmpTime = Trim(d)
                        MaM = 60 * Val(TmpTime.Substring(0, 2)) + TmpTime.Substring(3, 2)
                        If MaM < 120 Then MaM = MaM + 1440
                        spot.MaM = MaM
                        d = .Cells(row, 4).value
                        'deletes the "Programme:" part if it exist
                        If d.Length > 9 AndAlso d.Substring(0, 9).ToUpper = "PROGRAMME" Then
                            d = d.Substring(d.IndexOf(" "), (d.Length - d.IndexOf(" "))).Trim
                        End If
                        spot.Programme = d
                        spot.Filmcode = "No_film"

                        spot.Week = spot.Bookingtype.GetWeek(Date.FromOADate(spot.AirDate))

                        If spot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(spot.AirDate) Then
                            DateOutOfPeriod.Add(DateOutOfPeriod.Count, spot.AirDate)
                            Campaign.PlannedSpots.Remove(spot.ID)
                        End If
                        If Not spot.Week Is Nothing Then
                            Trinity.Helper.SetFilmForSpot(spot)
                            spot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                            spot.MyRating = 0
                            spot.PriceNet = 0
                        End If
                    End If
                End If
                row += 1
            End While




        End With
        Exit Sub

cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
        Resume

    End Sub

    Private Sub ImportKanal9(ByVal WB As CultureSafeExcel.Workbook)
        Dim Row As Integer
        Dim TargetRow As Integer
        Dim Target As String
        Dim Gender As String
        Dim AgeStr As String
        Dim MinAge As String
        Dim MaxAge As String
        Dim SkippedSpot As Boolean = False
        Dim r As Integer
        Dim d As String
        Dim c As Integer
        Dim t As String

        Dim TmpDate As Date
        Dim TmpYear As String
        Dim TmpMonth As String
        Dim TmpDay As String
        Dim StartDate As Date
        Dim EndDate As Date
        Dim StartRow As Integer
        Dim Stat As String

        Dim ChanCol As Integer
        Dim DateCol As Integer
        Dim FilmCol As Integer
        Dim ProgCol As Integer
        Dim TimeCol As Integer
        Dim RatingCol As Integer
        Dim LengthCol As Integer
        Dim BreakTypeCol As Integer
        Dim RemarkCol As Integer
        Dim PriceCol As Integer
        Dim PlacCol As Integer
        Dim GrossCol As Integer
        Dim NetCol As Integer
        Dim DurCol As Integer

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpFilm As Trinity.cFilm
        Dim MaM As Integer
        Dim UI As Single
        Dim fk As String
        Dim Chan As String

        'Kanal9 bekräftelse

        'Since users have defined this channel themselfs there are 2 versions of channel names, Kanal9 and Kanal 9
        'we need to find out which.
        Dim channel As String = ""
        For Each ch As Trinity.cChannel In Campaign.Channels
            channel = ch.ChannelName
            If InStr(channel, "Kanal 9") OrElse InStr(channel, "Kanal9") Then
                'now channel contains the correct channel name
                Exit For
            End If
        Next

        Row = 1
        With WB.Sheets(1)
            'there are 2 DIFFERENT " "!!!
            .Columns("H:I").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
            .Columns("H:I").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
            .AllCells.Replace(What:=",", Replacement:=",", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)

            Campaign.Channels(channel).IsVisible = True
            TargetRow = 1
            Dim targetCol = 6

            'we search an area for "Målgrupp"
            Dim n As Integer
            Dim m As Integer
            Dim targetFound As Boolean = False

            For m = 10 To 15
                If .Cells(m, 6).value = "Målgrupp:" Then
                    TargetRow = m
                    targetCol = 6
                    targetFound = True
                End If
            Next

            If Not targetFound Then
                For n = 5 To 7
                    For m = 20 To 22
                        If .Cells(m, n).value = "Målgrupp:" Then
                            TargetRow = m
                            targetCol = n
                        End If
                    Next
                Next
            End If
            targetCol += 1
            'we get if there is a empty column between the headers and the figures
            While .Cells(TargetRow, targetCol).value Is Nothing
                targetCol += 1
            End While


            On Error Resume Next
            Target = .Cells(TargetRow, targetCol).Text
            Gender = Target.Substring(0, 1)
            AgeStr = Mid(Target, Len(Target) - 4)
            MinAge = AgeStr.Substring(0, InStr(AgeStr, "-") - 1)
            MaxAge = Mid(AgeStr, InStr(AgeStr, "-") + 1)
            On Error GoTo cmdImportSchedule_Click_Error
            Select Case Gender
                Case "W" : Gender = "W"
                Case "M" : Gender = "M"
                Case "A" : Gender = ""
            End Select
            Campaign.Channels(channel).MainTarget.TargetName = Gender & MinAge & "-" & MaxAge
            Campaign.Channels(channel).MainTarget.Universe = Campaign.Channels(channel).BuyingUniverse

            Dim Datec As Integer = 1
            While .Cells(Row, Datec).formula <> "Datum" And .Cells(Row, Datec).formula <> "Kanal"
                Row = Row + 1
                If Row > 100 Then Exit While
            End While
            If .cells(Row, Datec).value = "Kanal" Or Row > 40 Then
                Row = 1
                Datec = 2
                While .Cells(Row, Datec).formula <> "Datum" And .Cells(Row, Datec).formula <> "Kanal"
                    Row = Row + 1
                    If Row > 100 Then Exit While
                End While
            End If


            Row = Row + 1
            r = Row
            d = .Cells(r, Datec).value
            If Asc(d.Substring(0, 1)) < 48 Or Asc(d.Substring(0, 1)) > 57 Then
                d = Mid(d, 4)
            End If
            TmpYear = Mid(d, Len(d) - 3)
            TmpDay = d.Substring(0, 2)
            TmpMonth = Mid(d, 4, 2)
            StartDate = CDate(TmpYear & "-" & TmpMonth & "-" & TmpDay)
            While .Cells(r, 1).value <> "Total"
                r = r + 1
            End While
            r = r - 1
            d = .Cells(r, Datec).value
            If Asc(d.Substring(0, 1)) < 48 Or Asc(d.Substring(0, 1)) > 57 Then
                d = Mid(d, 4)
            End If
            TmpYear = Mid(d, Len(d) - 3)
            TmpDay = d.Substring(0, 2)
            TmpMonth = Mid(d, 4, 2)
            EndDate = CDate(TmpYear & "-" & TmpMonth & "-" & TmpDay)

            Dim frmImport As New frmImport(Campaign.Channels(channel))
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate
            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - Kanal9"
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name

            Dim tmpNetBudget As Single
            Dim budgetcol As Integer = 5
            Dim z As Integer = 1
            While .Cells(z, 5).value <> "Budget:"
                z = z + 1
                If z > 30 Then Exit While
            End While
            If z > 30 Then
                z = 1
                budgetcol = 6
                While .Cells(z, 6).value <> "Budget:"
                    z = z + 1
                    If z > 30 Then Exit While
                End While
            End If
            'Dom har haft lite olika ihopdragna celler så vi kollar vart budgeten ligger

            tmpNetBudget = 0 'sätter default ifall vi inte hittar budgeten
            If z < 30 Then
                'Hämar ut vilken kolumn själva siffrorna är
                While budgetcol < 15
                    If Not .cells(z, budgetcol).value Is Nothing Then
                        If Double.TryParse(.cells(z, budgetcol).value.ToString.Replace(" ", "").ToString.Replace(",", "."), tmpNetBudget) Then
                            Exit While
                        End If
                    End If
                    budgetcol += 1
                End While
            End If

            frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Idx = frmImport.txtIndex.Text
            BT = frmImport.cmbBookingType.Text

            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = channel Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedSpot Is Nothing Then
                                        TmpSpot.MatchedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            If frmImport.optReplaceBudget.Checked Then
                Campaign.Channels(channel).BookingTypes(BT).ConfirmedNetBudget = CDec(tmpNetBudget)
            Else
                Campaign.Channels(channel).BookingTypes(BT).ConfirmedNetBudget += CDec(tmpNetBudget)
            End If
            Campaign.Channels(channel).BookingTypes(BT).ContractNumber = .Range("G8").value
            c = 1
            While .Cells(Row - 1, c).value <> "TRP" And c < 200
                c = c + 1
            End While
            RatingCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Pos" And c < 200
                c = c + 1
            End While
            RemarkCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Tid" And c < 200
                c = c + 1
            End While
            Dim tidCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Program" And c < 200
                c = c + 1
            End While
            Dim programmeCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Pris" And c < 200
                c = c + 1
            End While
            PriceCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Längd" And c < 200
                c = c + 1
            End While
            LengthCol = c

            c = 1
            While .Cells(Row - 1, c).value <> "Filmkod" And c < 200
                c = c + 1
            End While
            FilmCol = c

            While .Cells(Row, 1).value <> "Total"
                d = .Cells(Row, Datec).value

                If Not IsNumeric(d.First) Then
                    d = d.Substring(3)
                End If

                'If Asc(d.Substring(0, 1)) < 48 Or Asc(d.Substring(0, 1)) > 57 Then
                'd = Mid(d, 4)
                'End If
                TmpYear = d.Substring(6, 4)
                TmpDay = d.Substring(0, 2)
                TmpMonth = d.Substring(3, 2)
                'TmpYear = Mid(d, Len(d) - 3)
                'TmpDay = d.Substring(0, 2)
                'TmpMonth = Mid(d, 4, 2)
                TmpDate = TmpYear & "-" & TmpMonth & "-" & TmpDay
                If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                    TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                    TmpSpot.Channel = Campaign.Channels(channel)
                    TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                    TmpSpot.AirDate = TmpDate.ToOADate
                    TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                    If TmpSpot.Week Is Nothing Then
                        SkippedSpot = True
                        Campaign.PlannedSpots.Remove(TmpSpot.ID)
                    Else
                        .Columns(tidCol).AutoFit()
                        t = .Cells(Row, tidCol).Text
                        MaM = 60 * Val(t.Substring(0, 2)) + Val(Mid(t, Len(t) - 1))
                        If MaM < 120 Then MaM = MaM + 1440
                        TmpSpot.MaM = MaM
                        TmpSpot.Programme = .Cells(Row, programmeCol).value
                        TmpSpot.Remark = .Cells(Row, RemarkCol).value
                        TmpSpot.SpotLength = Format(Date.FromOADate(WB.Sheets(1).Cells(Row, LengthCol).value), "ss")
                        TmpSpot.Filmcode = .Cells(Row, FilmCol).value
                        If TmpSpot.Filmcode Is Nothing Or TmpSpot.Filmcode = "" Then
                            TmpSpot.Filmcode = TmpSpot.SpotLength & "s"
                        End If
                        If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                            DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                            Campaign.PlannedSpots.Remove(TmpSpot.ID)
                        End If
                        If Not TmpSpot.Week Is Nothing Then

                            Trinity.Helper.SetFilmForSpot(TmpSpot)

                            TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = .Cells(Row, RatingCol).Value
                            TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = .Cells(Row, RatingCol).Value * TmpSpot.Channel.MainTarget.UniIndex(Trinity.cTarget.EnumUni.uniMainCmp) * (Idx / 100) * (TmpSpot.Bookingtype.IndexMainTarget / 100)
                            If Not .Cells(Row, PriceCol).value Is Nothing Then
                                TmpSpot.PriceNet = .Cells(Row, PriceCol).value.ToString.Replace(" ", "").Replace(",", ".")
                            Else
                                TmpSpot.PriceNet = 0
                            End If

                        End If
                    End If
                End If
                Row = Row + 1
            End While
            If frmImport.chkEvaluate.Checked Then
                frmEvaluateSpecifics.MdiParent = frmMain
                frmEvaluateSpecifics.Show()
            End If
        End With
        If DateOutOfPeriod.Count > 0 Then
            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
            For i As Integer = 0 To DateOutOfPeriod.Count
                Msg += Format(Date.FromOADate(DateOutOfPeriod(i)), "Short date") & vbCrLf
            Next
            Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        On Error GoTo 0
        Exit Sub




cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
        Resume
    End Sub

    Private Sub ImportCanalPlus(ByVal WB As CultureSafeExcel.Workbook)
        Dim Row As Integer
        Dim TargetRow As Integer
        Dim Target As String
        Dim Gender As String
        Dim AgeStr As String
        Dim MinAge As String
        Dim MaxAge As String
        Dim SkippedSpot As Boolean = False
        Dim r As Integer
        Dim d As String
        Dim c As Integer
        Dim t As String

        Dim TmpDate As Date
        Dim TmpYear As String
        Dim TmpMonth As String
        Dim TmpDay As String
        Dim StartDate As Date
        Dim EndDate As Date
        Dim StartRow As Integer
        Dim Stat As String

        Dim ChanCol As Integer
        Dim DateCol As Integer
        Dim FilmCol As Integer
        Dim ProgCol As Integer
        Dim TimeCol As Integer
        Dim RatingCol As Integer
        Dim LengthCol As Integer
        Dim BreakTypeCol As Integer
        Dim RemarkCol As Integer
        Dim PriceCol As Integer
        Dim PlacCol As Integer
        Dim GrossCol As Integer
        Dim NetCol As Integer
        Dim DurCol As Integer

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim TmpFilm As Trinity.cFilm
        Dim MaM As Integer
        Dim UI As Single
        Dim fk As String
        Dim Chan As String


        Row = 1
        With WB.Sheets(1)
            'there are 2 DIFFERENT " "!!!
            .Columns("H:I").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
            .Columns("H:I").Replace(What:=" ", Replacement:="", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)
            .AllCells.Replace(What:=",", Replacement:=",", LookAt:=2, SearchOrder:=1, MatchCase:=False, SearchFormat:=False, ReplaceFormat:=False)

            Campaign.Channels("Canal+").IsVisible = True
            TargetRow = 1
            Dim targetCol = 6

            'we search an area for "Målgrupp"
            Dim n As Integer
            Dim m As Integer
            For n = 5 To 7
                For m = 20 To 22
                    If .Cells(m, n).value = "Målgrupp:" Then
                        TargetRow = m
                        targetCol = n
                    End If
                Next
            Next
            targetCol += 1
            'we get if there is a empty column between the headers and the figures
            While .Cells(TargetRow, targetCol).value Is Nothing
                targetCol += 1
            End While


            On Error Resume Next
            Target = .Cells(TargetRow, targetCol).Text
            Gender = Target.Substring(0, 1)
            AgeStr = Mid(Target, Len(Target) - 4)
            MinAge = AgeStr.Substring(0, InStr(AgeStr, "-") - 1)
            MaxAge = Mid(AgeStr, InStr(AgeStr, "-") + 1)
            On Error GoTo cmdImportSchedule_Click_Error
            Select Case Gender
                Case "W" : Gender = "W"
                Case "M" : Gender = "M"
                Case "A" : Gender = ""
            End Select

            Campaign.Channels("Canal+").MainTarget.TargetName = Gender & MinAge & "-" & MaxAge
            Campaign.Channels("Canal+").MainTarget.Universe = Campaign.Channels("Canal+").BuyingUniverse

            Dim Datec As Integer = 1
            While .Cells(Row, Datec).formula <> "Datum"
                Row = Row + 1
                If Row > 40 Then Exit While
            End While
            If Row > 40 Then
                Row = 1
                Datec = 2
                While .Cells(Row, Datec).formula <> "Datum"
                    Row = Row + 1
                    If Row > 40 Then Exit While
                End While
            End If


            Row = Row + 1
            r = Row
            d = .Cells(r, Datec).value
            If Asc(d.Substring(0, 1)) < 48 Or Asc(d.Substring(0, 1)) > 57 Then
                d = Mid(d, 4)
            End If
            TmpYear = Mid(d, Len(d) - 3)
            TmpDay = d.Substring(0, 2)
            TmpMonth = Mid(d, 4, 2)
            StartDate = CDate(TmpYear & "-" & TmpMonth & "-" & TmpDay)
            While .Cells(r, 1).value <> "Total"
                r = r + 1
            End While
            r = r - 1
            d = .Cells(r, Datec).value
            If Asc(d.Substring(0, 1)) < 48 Or Asc(d.Substring(0, 1)) > 57 Then
                d = Mid(d, 4)
            End If
            TmpYear = Mid(d, Len(d) - 3)
            TmpDay = d.Substring(0, 2)
            TmpMonth = Mid(d, 4, 2)
            EndDate = CDate(TmpYear & "-" & TmpMonth & "-" & TmpDay)

            Dim frmImport As New frmImport(Campaign.Channels("Canal+"))
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate
            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - Kanal9"
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name

            Dim tmpNetBudget As Single
            Dim budgetcol As Integer = 6
            Dim z As Integer = 1
            While .Cells(z, 5).value <> "Budget:"
                z = z + 1
                If z > 30 Then Exit While
            End While
            If z > 30 Then
                z = 1
                budgetcol = 8
                While .Cells(z, 6).value <> "Budget:"
                    z = z + 1
                    If z > 30 Then Exit While
                End While
            End If

            tmpNetBudget = .cells(z, budgetcol).value.ToString.Replace(" ", "").ToString.Replace(",", ".")
            frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Idx = frmImport.txtIndex.Text
            BT = frmImport.cmbBookingType.Text

            If frmImport.chkReplace.Checked Then
                For Each TmpSpot In Campaign.PlannedSpots
                    If TmpSpot.Channel.ChannelName = "Canal+" Then
                        If TmpSpot.Bookingtype.Name = BT Then
                            If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                    If Not TmpSpot.MatchedSpot Is Nothing Then
                                        TmpSpot.MatchedSpot.MatchedSpot = Nothing
                                    End If
                                    Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            If frmImport.optReplaceBudget.Checked Then
                Campaign.Channels("Canal+").BookingTypes(BT).ConfirmedNetBudget = CDec(tmpNetBudget)
            Else
                Campaign.Channels("Canal+").BookingTypes(BT).ConfirmedNetBudget += CDec(tmpNetBudget)
            End If
            Campaign.Channels("Canal+").BookingTypes(BT).ContractNumber = .Range("G8").value
            c = 1
            While .Cells(Row - 1, c).value <> "TRP" And c < 200
                c = c + 1
            End While
            RatingCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Pos" And c < 200
                c = c + 1
            End While
            RemarkCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Tid" And c < 200
                c = c + 1
            End While
            Dim tidCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Program" And c < 200
                c = c + 1
            End While
            Dim programmeCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Pris" And c < 200
                c = c + 1
            End While
            PriceCol = c
            c = 1
            While .Cells(Row - 1, c).value <> "Längd" And c < 200
                c = c + 1
            End While
            LengthCol = c
            While .Cells(Row, 1).Text <> "Total"
                d = .Cells(Row, Datec).value
                If Asc(d.Substring(0, 1)) < 48 Or Asc(d.Substring(0, 1)) > 57 Then
                    d = Mid(d, 4)
                End If
                TmpYear = Mid(d, Len(d) - 3)
                TmpMonth = d.Substring(0, 2)
                TmpDay = Mid(d, 4, 2)
                TmpDate = CDate(d)
                If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                    TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                    TmpSpot.Channel = Campaign.Channels("Canal+")
                    TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                    TmpSpot.AirDate = TmpDate.ToOADate
                    TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                    If TmpSpot.Week Is Nothing Then
                        SkippedSpot = True
                        Campaign.PlannedSpots.Remove(TmpSpot.ID)
                    Else
                        .Columns(tidCol).AutoFit()
                        t = .Cells(Row, tidCol).Text
                        MaM = 60 * Val(t.Substring(0, 2)) + Val(Mid(t, Len(t) - 1))
                        If MaM < 120 Then MaM = MaM + 1440
                        TmpSpot.MaM = MaM
                        TmpSpot.Programme = .Cells(Row, programmeCol).value
                        TmpSpot.Remark = .Cells(Row, RemarkCol).value
                        TmpSpot.SpotLength = Format(Date.FromOADate(WB.Sheets(1).Cells(Row, LengthCol).value), "ss")
                        TmpSpot.Filmcode = .Cells(Row, 8).value
                        If TmpSpot.Filmcode Is Nothing Or TmpSpot.Filmcode = "" Then
                            TmpSpot.Filmcode = TmpSpot.SpotLength & "s"
                        End If
                        If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                            DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                            Campaign.PlannedSpots.Remove(TmpSpot.ID)
                        End If
                        If Not TmpSpot.Week Is Nothing Then

                            Trinity.Helper.SetFilmForSpot(TmpSpot)

                            TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = .Cells(Row, RatingCol).Value
                            TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = .Cells(Row, RatingCol).Value * TmpSpot.Channel.MainTarget.UniIndex(Trinity.cTarget.EnumUni.uniMainCmp) * (Idx / 100) * (TmpSpot.Bookingtype.IndexMainTarget / 100)
                            If Not .Cells(Row, PriceCol).value Is Nothing Then
                                TmpSpot.PriceNet = .Cells(Row, PriceCol).value.ToString.Replace(" ", "").Replace(",", ".")
                            Else
                                TmpSpot.PriceNet = 0
                            End If

                        End If
                    End If
                End If
                Row = Row + 1
            End While
            If frmImport.chkEvaluate.Checked Then
                frmEvaluateSpecifics.MdiParent = frmMain
                frmEvaluateSpecifics.Show()
            End If
        End With
        If DateOutOfPeriod.Count > 0 Then
            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
            For i As Integer = 0 To DateOutOfPeriod.Count
                Msg += Format(Date.FromOADate(DateOutOfPeriod(i)), "Short date") & vbCrLf
            Next
            Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        On Error GoTo 0
        Exit Sub




cmdImportSchedule_Click_Error:
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
        Resume
    End Sub

    Private Sub ImportTV4Lokal(ByVal WB As CultureSafeExcel.Workbook)
        Dim Row As Integer
        Dim c As Integer = 1
        Dim d As String
        Dim StartDate As Date
        Dim EndDate As Date
        Dim firstSpot As Integer
        Dim Idx As Integer
        Dim BT As String

        'columns number
        Dim colDate As Integer
        Dim colTime As Integer
        Dim colProgramme As Integer
        Dim colFilmcode As Integer
        Dim colLength As Integer
        Dim colTRP As Integer
        Dim colChannel As Integer

        Row = 1
        With WB.Sheets(1)
            'fint the row where the head of the spot list is
            While .Cells(Row, 1).value <> "Kanal" And Not (.cells(Row, 1).value IsNot Nothing AndAlso .cells(Row, 1).value.ToString.ToUpper.Contains("STATION"))
                Row = Row + 1
            End While
            firstSpot = Row + 1

            'find the date column
            While .Cells(Row, c).value <> "Datum" And c < 200
                c = c + 1
            End While
            colDate = c
            c = 1

            'find the time column
            While .Cells(Row, c).value <> "Tid" And c < 200
                c = c + 1
            End While
            colTime = c
            c = 1

            'find the program column
            While .Cells(Row, c).value <> "Program" And c < 200
                c = c + 1
            End While
            colProgramme = c
            c = 1

            'find the filmcode
            While .Cells(Row, c).value <> "Kunds filmkod" And c < 200
                c = c + 1
            End While
            colFilmcode = c
            c = 1

            'find the column for the lenght
            While .Cells(Row, c).value <> "Längd" And c < 200
                c = c + 1
            End While
            colLength = c
            c = 1

            'find the ratings column
            While .Cells(Row, c).value <> "TRP" And c < 200
                c = c + 1
            End While
            colTRP = c

            c = 1

            'find the ratings column
            While .Cells(Row, c).value <> "Kanal" And c < 200
                c = c + 1
            End While
            colChannel = c

            Row += 1

            'get the start date
            d = .Cells(Row, colDate).value
            StartDate = CDate(d)
            While Not .Cells(Row, colDate).value Is Nothing
                Row += 1
            End While

            'get the end date
            d = .Cells(Row - 1, colDate).value
            EndDate = CDate(d)


            'show the import dialog

            Dim frmImport As New frmImport(Campaign.Channels("TV4"))
            frmImport.Tag = "TV4Lokal"
            frmImport.dtFrom.Value = StartDate
            frmImport.dtTo.Value = EndDate
            frmImport.txtIndex.Text = 100
            frmImport.Text = "Import Schedule - TV4 Lokal"
            frmImport.lblPath.Tag = WB.Path
            frmImport.lblPath.Text = WB.Name
            If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                CleanUp()
                Exit Sub
            End If
            Idx = frmImport.txtIndex.Text
            BT = frmImport.cmbBookingType.Text

            Row = firstSpot
            'read the spots



            Dim TmpDate As Date
            Dim TmpSpot As Trinity.cPlannedSpot
            Dim skippedspot As Boolean
            Dim found As Boolean
            Dim strChannel = ""
            Dim MaM As Integer
            Dim filmMasterList As New Dictionary(Of String, String)

            While Not .Cells(Row, 1).value Is Nothing

                d = .Cells(Row, colDate).value
                TmpDate = CDate(d)
                If TmpDate >= frmImport.dtFrom.Value And TmpDate <= frmImport.dtTo.Value Then
                    'there are 3 different ways of displaying the channel name.
                    'the *word* is what we need to get from teh string
                    'Göteborg / *Göteborg*
                    'Halland / *Halmstad*-Falkenberg
                    'Öst / S3 (*Motala*-Kisa)
                    found = False
                    d = .Cells(Row, 1).value
                    'remove all before the / in the string
                    d = d.Substring(d.LastIndexOf("/") + 1, d.Length - d.LastIndexOf("/") - 1).Trim
                    'if theres a brackett in the string we know its a type 3, if a - its type 2
                    If InStr(d, "(") Then
                        'get whats in the bracketts
                        d = d.Substring(d.LastIndexOf("(") + 1, d.Length - d.LastIndexOf("(") - 2)
                        'gets what before the -
                        If InStr(d, "-") Then
                            d = d.Substring(0, d.IndexOf("-"))
                        End If
                    ElseIf InStr(d, "-") Then
                        'gets what before the -
                        d = d.Substring(0, d.IndexOf("-"))
                    End If

                    'match the channel name from the spot list to the channel name we have
                    If .cells(Row, colChannel).value = "TV4" Then
                        Select Case d

                            Case Is = "Norrbotten"
                                strChannel = "TV4 Norrb"
                            Case Is = "Skellefteå"
                                strChannel = "TV4 Skellefteå"
                            Case Is = "Umeå"
                                strChannel = "TV4 Umeå"
                            Case Is = "Sundsvall"
                                strChannel = "TV4 Sunds"
                            Case Is = "Örnskölsvik"
                                strChannel = "TV4 Örnsk"
                            Case Is = "Örnskölsdvik"
                                strChannel = "TV4 Örnsk"
                            Case Is = "Jämtland"
                                strChannel = "TV4 Jämt"
                            Case Is = "Gävle"
                                strChannel = "TV4 Gävle"
                            Case Is = "Dalarna"
                                strChannel = "TV4 Dala"
                            Case Is = "Värmland"
                                strChannel = "TV4 Värm"
                            Case Is = "Väst"
                                strChannel = "TV4 Väst"
                            Case Is = "Skaraborg"
                                strChannel = "TV4 Skara"
                            Case Is = "Göteborg"
                                strChannel = "TV4 Göteb"
                            Case Is = "Borås"
                                strChannel = "TV4 Borås"
                            Case Is = "Uppland"
                                strChannel = "TV4 Uppl"
                            Case Is = "Västerås"
                                strChannel = "TV4 Västerås"
                            Case Is = "Örebro"
                                strChannel = "TV4 Örebro"
                            Case Is = "Norrköping"
                                strChannel = "TV4 Norrk"
                            Case Is = "Motala"
                                strChannel = "TV4 Motala"
                            Case Is = "Stockholm"
                                strChannel = "TV4 Sthlm"
                            Case Is = "Jönköping"
                                strChannel = "TV4 Jönk"
                            Case Is = "Nässjö"
                                strChannel = "TV4 Nässjö"
                            Case Is = "Värnamo"
                                strChannel = "TV4 Värn"
                            Case Is = "Hamlstad"
                                strChannel = "TV4 Halm"
                            Case Is = "Varberg"
                                strChannel = "TV4 Varberg"
                            Case Is = "Kronoberg"
                                strChannel = "TV4 Krono"
                            Case Is = "Blekinge"
                                strChannel = "TV4 Blek"
                            Case Is = "Kalmar"
                                strChannel = "TV4 Kalmar"
                            Case Is = "Västervik"
                                strChannel = "TV4 Västvk"
                            Case Is = "Malmö"
                                strChannel = "TV4 Malmö"
                            Case Is = "Skåne"
                                strChannel = "TV4 Malmö"
                            Case Is = "Helsingborg"
                                strChannel = "TV4 Hlsngb"
                        End Select
                    Else
                        Select Case d
                            Case Is = "Göteborg"
                                strChannel = "TV4+ Göteborg"
                            Case Is = "Stockholm"
                                strChannel = "TV4+ Stockholm"
                            Case Else
                                strChannel = "TV4+ " & d
                        End Select
                    End If
                    If strChannel = "" Then
                        MsgBox(d & " is not a known channel")
                        Exit Sub
                    End If

                    TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                    TmpSpot.Channel = Campaign.Channels(strChannel)
                    If TmpSpot.Channel Is Nothing Then Stop
                    If TmpSpot.Channel.ChannelName.Contains("TV4+") Then
                        TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes("Lokal")
                    Else
                        TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                    End If




                    TmpSpot.AirDate = TmpDate.ToOADate
                    TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))

                    If TmpSpot.Week Is Nothing Then
                        skippedspot = True
                        Campaign.PlannedSpots.Remove(TmpSpot.ID)
                    Else
                        .Columns(colTime).AutoFit()
                        d = .Cells(Row, colTime).Text
                        MaM = 60 * Val(d.Substring(0, 2)) + Val(Mid(d, Len(d) - 1))
                        If MaM < 120 Then MaM = MaM + 1440
                        TmpSpot.MaM = MaM
                        TmpSpot.Programme = .Cells(Row, colProgramme).value
                        TmpSpot.SpotLength = .Cells(Row, colLength).value
                        d = .Cells(Row, colFilmcode).value
                        If d Is Nothing Or d = "" Then
                            TmpSpot.Filmcode = TmpSpot.SpotLength & "s"
                        Else
                            TmpSpot.Filmcode = d
                        End If
                        If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                            DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                            Campaign.PlannedSpots.Remove(TmpSpot.ID)
                        End If
                        If Not TmpSpot.Week Is Nothing Then

                            Trinity.Helper.SetFilmForSpot(TmpSpot)

                            TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = .Cells(Row, colTRP).Value
                            TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = .Cells(Row, colTRP).Value * TmpSpot.Channel.MainTarget.UniIndex(Trinity.cTarget.EnumUni.uniMainCmp) * (Idx / 100) * (TmpSpot.Bookingtype.IndexMainTarget / 100)
                            TmpSpot.PriceNet = 0


                        End If
                    End If
                End If
                Row = Row + 1
            End While


        End With

        Exit Sub
cmdImportSchedule_Click_Error:
        MessageBox.Show(Row.ToString)

    End Sub

    Sub checkChannelExists(ByVal channel As Trinity.cChannel)
        For Each tmpchan As Trinity.cChannel In Campaign.Channels

        Next
    End Sub
    Public sub importConfirmationFromSpotlight

    End sub
    Public Sub ImportSchedule()

        ' Stores the amount of spots found in the spotlist, so the user can be warned if its empty
        Dim _spotCount As Integer = 0

        Dim showEvaluateSpecifics As Boolean = False
        Dim dlgOpen As New System.Windows.Forms.OpenFileDialog

        dlgOpen.CheckFileExists = True
        dlgOpen.DefaultExt = ""
        dlgOpen.FileName = ""
        dlgOpen.Filter = "All readable formats|*.xls;*.doc;*.docx;*.xlsx;*.rtf|Excel workbook|*.xls|Word document|*.doc;*.rtf"
        dlgOpen.Multiselect = True
        dlgOpen.Title = "Open channel schedule"
        dlgOpen.InitialDirectory = TrinitySettings.ChannelSchedules

        If dlgOpen.ShowDialog = DialogResult.Cancel Then Exit Sub

        For Each _file As String In dlgOpen.FileNames
            Dim _schedule As New ExcelReadTemplates.cDocument
            _schedule.Load(_file)

            Dim _foundTemplate As Boolean = False
            For Each _path As String In TrinitySettings.ScheduleTemplateList
                Dim _template As New ScheduleTemplates.cScheduleTemplate(False)
                Dim _xml As Xml.Linq.XDocument = Xml.Linq.XDocument.Load(_path)

                _template.Parse(_xml)
                Dim _res As ITemplate.ValidationResult
                If Not _foundTemplate Then _res = _template.Validate(_schedule, True)
                If Not _foundTemplate AndAlso _res = ITemplate.ValidationResult.Success Then
                    _foundTemplate = True

                    If (_template.GetSpots().Count = 0) Then
                        MessageBox.Show("This spotlist is empty!", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If

                    For Each _chunk As ScheduleTemplates.cChunkInfo In _template.GetSpots

                        DateOutOfPeriod.Clear()
                        Dim _chan As Trinity.cChannel = Campaign.Channels(_chunk.Channel)
                        If _chan Is Nothing Then
                            Dim _message As String = String.Format("The channel '{0}' was found in the schedule, but not the campaign." & vbCrLf & "Please choose a channel to import spots to:", _chunk.Channel)
                            If String.IsNullOrEmpty(_chunk.Channel) Then
                                _message = "This schedule did not specify a channel, please provide one:" & vbCrLf & IO.Path.GetFileName(_file)
                            End If
                            Dim _frm As New frmMultipleChoice((From _c As Trinity.cChannel In Campaign.Channels Select _c.ChannelName).ToArray, _message)
                            If _frm.ShowDialog = DialogResult.OK Then
                                _chan = Campaign.Channels(_frm.Result)
                            End If
                        End If

                        If _chan IsNot Nothing Then
                            Dim frmImport As New frmImport(_chan)

                            Try
                                If _chunk.Spots.Count > 0
                                    frmImport.dtFrom.Value = _chunk.Spots.Min(Function(s) s.Date)
                                    frmImport.dtTo.Value = _chunk.Spots.Max(Function(s) s.Date)

                                    frmImport.txtIndex.Text = 100
                                    frmImport.Text = "Import Schedule - " & _chan.ChannelName
                                    frmImport.lblPath.Tag = IO.Path.GetDirectoryName(_file)
                                    frmImport.lblPath.Text = IO.Path.GetFileName(_file)
                                    frmImport.lblConfirmationBudget.Text = Format(CDec(_chunk.Budget), "N0")
                                    frmImport.lblLabel.Text = _chunk.Label
                                    frmImport.lblAmountOfSpots.Text = _chunk.Spots.Count().ToString
                                    If _chunk.Label <> "" Then
                                        frmImport.cmbBookingType.SelectedItem = _chunk.Label
                                    Else
                                        If frmImport.cmbBookingType.Items.Count > 1
                                            frmImport.cmbBookingType.SelectedIndex = 0
                                        End If
                                    End If
                                    If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                                        CleanUp()
                                        Exit Sub
                                    End If
                                    Dim _Idx As Single = frmImport.txtIndex.Text
                                    Dim _bt As Trinity.cBookingType = _chan.BookingTypes(frmImport.cmbBookingType.Text)

                                    If _template.ContractRules.ValidationResult.Succeeded Then
                                        _bt.ContractNumber = _template.ContractRules.ValidationResult.Result
                                    End If

                                    If frmImport.chkReplace.Checked Then
                                        Dim _spots As List(Of Trinity.cPlannedSpot) = (From _spot As Trinity.cPlannedSpot In Campaign.PlannedSpots Where _spot.Bookingtype Is _bt AndAlso _spot.AirDate >= frmImport.dtFrom.Value.ToOADate AndAlso _spot.AirDate <= frmImport.dtTo.Value.ToOADate Select _spot).ToList()
                                        For Each _spot As Trinity.cPlannedSpot In _spots
                                            Campaign.PlannedSpots.Remove(_spot.ID)
                                        Next
                                    End If

                                    'Added by JK
                                    If frmImport.chkEvaluate.Checked Then
                                        showEvaluateSpecifics = True
                                    End If

                                    If frmImport.optReplaceBudget.Checked Then
                                        _bt.ConfirmedNetBudget = _chunk.Budget
                                    Else
                                        _bt.ConfirmedNetBudget += _chunk.Budget
                                    End If

                                    Dim _skippedSpot As Boolean = False
                                    Dim _placDict As New Dictionary(Of String, Trinity.cAddedValue)

                                    For Each _spot As ScheduleTemplates.cScheduleSpot In _chunk.Spots
                                        If _spot.Date >= frmImport.dtFrom.Value AndAlso _spot.Date <= frmImport.dtTo.Value Then
                                            If _spot.Date >= Date.FromOADate(Campaign.StartDate) AndAlso _spot.Date <= Date.FromOADate(Campaign.EndDate) Then
                                                Dim _newSpot As Trinity.cPlannedSpot = Campaign.PlannedSpots.Add(CreateGUID)
                                                _newSpot.Channel = _chan
                                                _newSpot.AirDate = _spot.Date.ToOADate
                                                _newSpot.Bookingtype = _bt
                                                _newSpot.Week = _newSpot.Bookingtype.GetWeek(Date.FromOADate(_newSpot.AirDate))
                                                If _newSpot.Week Is Nothing Then
                                                    _skippedSpot = True
                                                    Campaign.PlannedSpots.Remove(_newSpot.ID)
                                                Else
                                                    _newSpot.MaM = _spot.MaM
                                                    _newSpot.Programme = _spot.Program
                                                    Dim _fk As String = _spot.Filmcode

                                                    If _fk <> "" AndAlso _fk.Length > 1 Then
                                                        If Mid(_fk, Len(_fk) - 1) = "-S" Then
                                                            _fk = _fk.Substring(0, Len(_fk) - 2)
                                                            'ElseIf Mid(_fk, Len(_fk) - 3) = "-S/2" Then
                                                            '    _fk = _fk.Substring(0, Len(_fk) - 4)
                                                        End If
                                                    End If
                                                    _newSpot.ProgBefore = _spot.ProgBefore
                                                    _newSpot.ProgAfter = _spot.ProgAfter
                                                    _newSpot.Filmcode = _fk
                                                    _newSpot.SpotLength = _spot.Length
                                                    _newSpot.PriceGross = _spot.GrossPrice
                                                    _newSpot.PriceNet = _spot.NetPrice

                                                    If _fk Is Nothing Or _fk = "" Then
                                                        _newSpot.Filmcode = _spot.Length
                                                    End If
                                                    If _newSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(_newSpot.AirDate) Then
                                                        DateOutOfPeriod.Add(DateOutOfPeriod.Count, _newSpot.AirDate)
                                                        Campaign.PlannedSpots.Remove(_newSpot.ID)
                                                    End If
                                                    If Not _newSpot.Week Is Nothing Then

                                                        Trinity.Helper.SetFilmForSpot(_newSpot)

                                                        If _newSpot.Bookingtype.AddedValues.FindByName(_spot.AddedValue) IsNot Nothing AndAlso Not _placDict.ContainsKey(_spot.AddedValue) Then
                                                            _placDict.Add(_spot.AddedValue, _newSpot.Bookingtype.AddedValues.FindByName(_spot.AddedValue))
                                                        End If
                                                        If Not _spot.AddedValue Is Nothing AndAlso Not _spot.AddedValue = "" AndAlso Not _placDict.ContainsKey(_spot.AddedValue) Then
                                                            Dim frmGetAV As New frmChooseAddedValue(_bt, _spot.AddedValue)
                                                            If frmGetAV.ShowDialog = DialogResult.OK Then
                                                                If frmGetAV.optSetAsOld.Checked Then
                                                                    If frmGetAV.cmbAV.SelectedIndex > 0 Then
                                                                        _newSpot.AddedValue = _newSpot.Bookingtype.AddedValues(frmGetAV.cmbAV.SelectedIndex)
                                                                    End If
                                                                Else
                                                                    With _newSpot.Bookingtype.AddedValues.Add(frmGetAV.txtName.Text)
                                                                        .IndexGross = frmGetAV.txtGrossIdx.Text
                                                                        .IndexNet = frmGetAV.txtNetIdx.Text
                                                                        _newSpot.AddedValue = _newSpot.Bookingtype.AddedValues(.ID)
                                                                    End With
                                                                End If
                                                                _placDict.Add(_spot.AddedValue, _newSpot.AddedValue)
                                                            End If
                                                        ElseIf Not _spot.AddedValue Is Nothing AndAlso Not _spot.AddedValue = "" Then
                                                            _newSpot.AddedValue = _placDict(_spot.AddedValue)
                                                        End If

                                                        If _spot.Rating > 0 Then
                                                            _newSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = _spot.Rating
                                                            _newSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = _spot.Rating * _newSpot.Channel.MainTarget.UniIndex(Trinity.cTarget.EnumUni.uniMainCmp) * (CDec(frmImport.txtIndex.Text) / 100) * (_newSpot.Bookingtype.IndexMainTarget / 100)
                                                            Dim _ui As Single = (_newSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) / _newSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget))
                                                            If _ui > 0 Then
                                                                _newSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = _newSpot.MyRating / _ui
                                                            Else
                                                                _newSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            Else
                                                If Not DateOutOfPeriod.Contains(_spot.Date) Then
                                                    DateOutOfPeriod.Add(DateOutOfPeriod.Count, _spot.Date)
                                                End If
                                            End If
                                        End If
                                    Next
                                Else 
                                    Windows.Forms.MessageBox.Show("The spotlist for " & _chan.ChannelName & " contains no spots. no spots were loaded" & vbNewLine, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                ' End here
                            Catch e As Exception
                                Windows.Forms.MessageBox.Show("An error occured when reading the spotlist, no spots were loaded!" & vbNewLine & vbNewLine & e.Message, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End Try
                        End If
                        If DateOutOfPeriod.Count > 0 Then
                            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
                            For i As Integer = 0 To DateOutOfPeriod.Count
                                Msg += Format(DateOutOfPeriod(i), "Short date") & vbCrLf
                            Next
                            Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Next
                End If
            Next
            If Not _foundTemplate Then
                Windows.Forms.MessageBox.Show("Could not find any template that could be used to read '" & IO.Path.GetFileName(_file) & "'", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Next
        'Added by JK
        If showEvaluateSpecifics Then
            frmEvaluateSpecifics.MdiParent = frmMain
            frmEvaluateSpecifics.Show()
        End If
    End Sub

    Sub CleanUp()

    End Sub

    Public Sub ImportSchedule_old()
        'this is the start procedue that is initially called

        Dim Excel As CultureSafeExcel.Application
        Dim WB As CultureSafeExcel.Workbook

        Dim row As Integer

        Dim a As Integer
        Dim en As Long
        Dim ed As String
        On Error GoTo cmdImportSchedule_Click_Error
        frmMain.tmrAutosave.Enabled = False
        Trinity.Helper.WriteToLogFile("Start of frmSpots/cmdImportSchedule_Click")

        Dim dlgOpen As New System.Windows.Forms.OpenFileDialog

        dlgOpen.CheckFileExists = True
        dlgOpen.DefaultExt = ""
        dlgOpen.FileName = ""
        dlgOpen.Filter = "All readable formats|*.xls;*.doc;*.docx;*.xlsx;*.rtf|Excel workbook|*.xls|Word document|*.doc;*.rtf"
        dlgOpen.Multiselect = True
        dlgOpen.Title = "Open channel schedule"
        dlgOpen.InitialDirectory = TrinitySettings.ChannelSchedules

        If dlgOpen.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
            Exit Sub
        End If
        Trinity.Helper.WriteToLogFile("ImportSchedule: Start Excel")
        Excel = New CultureSafeExcel.Application(True)

        'Excel = CreateObject("CultureSafeExcel")
        Excel.DisplayAlerts = False
        Excel.ScreenUpdating = False
        For Each Filename As String In dlgOpen.FileNames
            If Not (UCase(Filename).EndsWith("XLS") Or UCase(Filename).EndsWith("XLSX")) Then
                Trinity.Helper.WriteToLogFile("ImportSchedule: Found a Word-document")
                Dim Word As Microsoft.Office.Interop.Word.Application
                Trinity.Helper.WriteToLogFile("ImportSchedule: Start Word")
                Word = New Microsoft.Office.Interop.Word.Application 'CreateObject("word.application")
                With Word.Documents.Open(Filename, , True, False)
                    WB = Excel.AddWorkbook
                    Word.Selection.WholeStory()
                    Trinity.Helper.WriteToLogFile("ImportSchedule: Copy document")
                    Word.Selection.Copy()
                    'Line below needed because of curious bug with Excel not being ready for the Paste operation
                    On Error Resume Next
                    Trinity.Helper.WriteToLogFile("ImportSchedule: Paste in Excel")
                    WB.Sheets(1).Paste()
                    While Err.Number > 0
                        On Error Resume Next
                        WB.Sheets(1).Paste()
                    End While
                    On Error GoTo cmdImportSchedule_Click_Error
                    Trinity.Helper.WriteToLogFile("ImportSchedule: Close document and quit word")
                    .Close()
                    Word.Quit()
                End With
            Else
                WB = Excel.OpenWorkbook(Filename)
            End If

            Trinity.Helper.ClearFilmList()

            DateOutOfPeriod.Clear()

            Dim cellContents As New List(Of String)

            For r As Integer = 1 To 20
                For c As Integer = 1 To 20
                    If Not WB.Sheets(1).Cells(r, c).Value Is Nothing Then cellContents.Add(WB.Sheets(1).Cells(r, c).Value.ToString)
                Next
            Next

            '   If (InStr(WB.Sheets(1).Range("B3").value, "Campaign Information") > 0 Or InStr(WB.Sheets(1).Range("B3").value, "Kampanj Information") > 0) And InStr(WB.Sheets(1).Range("C30").value, "TV8") Then
            'MsgBox("Sorry but Trinity dont support TV8 at present", MsgBoxStyle.Information, "Error reading file")

            If cellContents.Contains("TV3 NORWAY") Or cellContents.Contains("V4 NORWAY") Or InStr(WB.Sheets(1).Range("B3").Value, "Campaign Information") > 0 Or InStr(WB.Sheets(1).Range("B3").Value, "Kampanj Information") > 0 Then

                ImportViasatSchedule(WB)

            ElseIf Not WB.Sheets(1).Range("H1").Value Is Nothing AndAlso WB.Sheets(1).Range("H1").Value.ToString.IndexOf("Jetix") > -1 Or Not WB.Sheets(1).Range("D1").Value Is Nothing AndAlso WB.Sheets(1).Range("D1").Value.ToString.IndexOf("Jetix") > -1 Then

                ImportJetixSchedule(WB)

            ElseIf Not WB.Sheets(1).Range("B11").Value Is Nothing AndAlso InStr(WB.Sheets(1).Range("B11").Value.ToString.ToUpper, "EUROSPORT") > 0 Then

                ImportEurosportSchedule(WB)

            ElseIf Not WB.Sheets(1).Range("H2").Value Is Nothing AndAlso WB.Sheets(1).Range("H2").Value.ToString.ToUpper.Contains("LOCSEARCH") Then

                ImportEurosportSchedule(WB, 2)

            ElseIf Not WB.Sheets(1).Range("A2").Value Is Nothing AndAlso WB.Sheets(1).Range("A2").Value.GetType.Name = "String" AndAlso InStr(WB.Sheets(1).Range("A2").Value, "TV4 Plus") Then

                ImportTV4Nisch(WB)
                'ImportTV4PlusSchedule(WB)


            ElseIf Not WB.Sheets(1).Range("A1").Value Is Nothing AndAlso WB.Sheets(1).Range("A1").Value.GetType.Name = "String" AndAlso InStr(WB.Sheets(1).Range("A1").Value, "TV4 Plus") OrElse WB.Sheets(1).Range("A2").Text.ToString.Contains("Sjuan") Then

                ImportTV4Nisch(WB)
                ' ImportTV4PlusSchedule(WB)

            ElseIf WB.Sheets(1).Range("A2").Text.ToString.Contains("TV4 Sport") OrElse WB.Sheets(1).Range("A2").Text.ToString.Contains("TV400") OrElse WB.Sheets(1).Range("A2").Text.ToString.Contains("TV4 Fakta") OrElse WB.Sheets(1).Range("A2").Text.ToString.Contains("TV11") Then

                ImportTV4Nisch(WB)


            ElseIf Not WB.Sheets(1).Range("A2").Value Is Nothing AndAlso WB.Sheets(1).Range("A2").Value.GetType.Name = "String" AndAlso InStr(WB.Sheets(1).Range("A2").Value, "TV4") Then

                ImportTV4Schedule(WB)

                '    ElseIf Not WB.Sheets(1).range("G2").value Is Nothing AndAlso WB.Sheets(1).range("G2").value.GetType.Name = "String" AndAlso WB.Sheets(1).Range("G2").value.ToString.Contains("TV4") Then

                '   ImportPlainTV4Schedule(WB)

            ElseIf cellContents.Contains("Discovery Channel") OrElse (Not WB.Sheets(1).Range("B3").Value Is Nothing AndAlso WB.Sheets(1).Range("B3").Value.GetType.Name = "String" AndAlso (WB.Sheets(1).Range("B3").Value.ToString.StartsWith("Discovery") Or WB.Sheets(1).Range("B3").Value.ToString.StartsWith("TLC"))) Then

                ImportDiscoverySchedule(WB)

            ElseIf Not WB.Sheets(1).Range("A1").Value Is Nothing AndAlso WB.Sheets(1).Range("A1").Value.GetType.Name = "String" AndAlso (WB.Sheets(1).Range("A1").Value.ToString.ToUpper.Contains("JAAR") Or WB.Sheets(1).Range("A1").Value.ToString.ToUpper.Contains("GEO")) Then

                ImportNationalGeographicSchedule(WB)

            ElseIf WB.Sheets(1).Range("B3").Value IsNot Nothing AndAlso WB.Sheets(1).Range("B3").Value.GetType.Name = "String" AndAlso WB.Sheets(1).Range("B3").Value.ToString.ToUpper = "FAKTA" Then

                ImportTV4FaktaSchedule(WB)

            ElseIf Not WB.Sheets(1).Range("B3").Value Is Nothing AndAlso WB.Sheets(1).Range("B3").Value.GetType.Name = "String" AndAlso WB.Sheets(1).Range("B3").Value = "TV4 Sport" Then

                ImportTV4SportSchedule(WB)

            ElseIf cellContents.Contains("COMEDY CENTRAL SWEDEN") Then

                ImportMTVSchedule(WB)

            ElseIf cellContents.Contains("NICKELODEON SWEDEN") Then

                ImportMTVSchedule(WB)

            ElseIf cellContents.Contains("MTV SWEDEN") OrElse cellContents.Contains("MTV NORWAY") Then

                ImportMTVSchedule(WB)

            ElseIf Not WB.Sheets(1).Cells(2, 2).Value Is Nothing AndAlso WB.Sheets(1).Cells(2, 2).Value.GetType.Name = "String" AndAlso WB.Sheets(1).Cells(2, 2).Value = "Feed" Then

                ImportMTVSchedule(WB)

            ElseIf Not WB.Sheets(1).Cells(1, 2).Value Is Nothing AndAlso WB.Sheets(1).Cells(1, 2).Value.GetType.Name = "String" AndAlso WB.Sheets(1).Cells(1, 2).Value.ToString.ToUpper.Contains("CONTROLIZER") Then

                ImportMTVSchedule(WB)

                'ElseIf Not WB.Sheets(1).cells(2, 2).value Is Nothing AndAlso WB.Sheets(1).cells(2, 2).value.GetType.Name = "String" AndAlso WB.Sheets(1).cells(2, 2).value = "Feed" Then

                '    ImportNickelodeonSchedule(WB)

            ElseIf (Not WB.Sheets(1).Range("E4").Value Is Nothing AndAlso WB.Sheets(1).Range("E4").Value.ToString.Length > 3 AndAlso WB.Sheets(1).Range("E4").Value.ToString.Substring(0, 4) = "TV 2") OrElse (Not WB.Sheets(1).Range("E2").Value Is Nothing AndAlso WB.Sheets(1).Range("E2").Value.ToString.Length > 2 AndAlso WB.Sheets(1).Range("E2").Value.ToString.Substring(0, 3) = "TV2") Then

                Trinity.Helper.WriteToLogFile("ImportSchedule: Found a TV2 Denmark Schedule")
                ImportTV2DKSchedule(WB)

            ElseIf Not WB.Sheets(1).Cells(1, 1).Value Is Nothing AndAlso WB.Sheets(1).Cells(1, 1).Value.GetType.Name = "String" AndAlso WB.Sheets(1).Cells(1, 1).Value.Contains("Contract") Then

                Trinity.Helper.WriteToLogFile("ImportSchedule: Found a TVNorge Schedule")
                ImportTVNorgeSchedule(WB, dlgOpen.FileName)

            ElseIf Not WB.Sheets(1).Cells(1, 1).Value Is Nothing AndAlso WB.Sheets(1).Cells(1, 1).Value.GetType.Name = "String" AndAlso WB.Sheets(1).Cells(1, 1).Value = "Visningsplaner" Then

                Dim TV2orZebra As New frmMultipleChoice({"TV2", "TV2 Zebra", "Nyhetskanalen", "Bliss"}, "Choose a channel")
                TV2orZebra.ShowDialog()
                If TV2orZebra.Result = "TV2" Then
                    ImportTV2Schedule(WB)
                ElseIf TV2orZebra.Result = "TV2 Zebra" Then
                    ImportTV2ZebraSchedule(WB)
                Else
                    ImportTV2Schedule(WB, TV2orZebra.Result)
                End If
                'If WB.Sheets(1).range("E23").value = "Standard" OrElse WB.Sheets(1).range("E24").value = "Standard" OrElse WB.Sheets(1).range("B6").value.ToString.Substring(0, 3).ToUpper = "ZEB" Then
                '    ImportTV2ZebraSchedule(WB)
                'Else
                '    ImportTV2Schedule(WB)
                'End If

            ElseIf Not WB.Sheets(1).Range("B3").Value Is Nothing AndAlso WB.Sheets(1).Range("B3").Value.GetType.Name = "String" AndAlso WB.Sheets(1).Range("B3").Value = "TV400" Then

                ImportTV400Schedule(WB)

                '    'Kanal5 and Kanal9 
                'ElseIf Not WB.Sheets(1).range("A1").value Is Nothing AndAlso WB.Sheets(1).range("A1").value.GetType.Name = "String" AndAlso ((InStr(WB.Sheets(1).Range("A1").value, "Kanal 5") > 0 Or InStr(WB.Sheets(1).Range("A1").value, "Kanal 9") > 0) Or (InStr(WB.Sheets(1).Range("A1").value, "Kanal5") > 0 Or InStr(WB.Sheets(1).Range("A1").value, "Kanal9"))) Then
                '    row = 1
                '    Dim found As Boolean = False
                '    row = 5
                '    While row < 50
                '        If Not WB.Sheets(1).cells(row, 1).value Is Nothing Then
                '            If InStr(WB.Sheets(1).cells(row, 1).value.ToString.ToUpper, "KANAL5") OrElse InStr(WB.Sheets(1).cells(row, 1).value.ToString.ToUpper, "KANAL 5") Then
                '                ImportKanal5Schedule(WB)
                '                found = True
                '                Exit While
                '            End If
                '            If InStr(WB.Sheets(1).cells(row, 1).value.ToString.ToUpper, "KANAL9") OrElse InStr(WB.Sheets(1).cells(row, 1).value.ToString.ToUpper, "KANAL 9") Then
                '                ImportKanal9(WB)
                '                found = True
                '                Exit While
                '            End If
                '        End If
                '        row += 1
                '    End While
                '    If Not found Then ImportKanal5Schedule(WB)

                'Kanal5
            ElseIf Not WB.Sheets(1).Range("I12").Value Is Nothing AndAlso WB.Sheets(1).Range("I12").Value.GetType.Name = "String" AndAlso ((InStr(WB.Sheets(1).Range("I12").Value, "Kanal 5") > 0) Or (InStr(WB.Sheets(1).Range("I12").Value, "Kanal5") > 0)) Then

                ImportKanal5Schedule(WB)

                'Kanal9
            ElseIf Not WB.Sheets(1).Range("I12").Value Is Nothing AndAlso WB.Sheets(1).Range("I12").Value.GetType.Name = "String" AndAlso ((InStr(WB.Sheets(1).Range("I12").Value, "Kanal 9") > 0) Or (InStr(WB.Sheets(1).Range("I12").Value, "Kanal9") > 0)) Then

                ImportKanal9(WB)

                'Canal+
            ElseIf Not WB.Sheets(1).Range("A1").Value Is Nothing AndAlso WB.Sheets(1).Range("A1").Value.GetType.Name = "String" AndAlso ((InStr(WB.Sheets(1).Range("A1").Value, "Canal+") > 0) Or (InStr(WB.Sheets(1).Range("A1").Value, "Canal +") > 0)) Then

                ImportCanalPlus(WB)

                'Tv4 Lokal
            ElseIf WB.Sheets(1).Range("A3").Value = "Annonsör:" Or (WB.Sheets(1).Range("A1").Value IsNot Nothing AndAlso WB.Sheets(1).Range("A1").Value.ToString.Contains("Visningsplan")) Then

                ImportTV4Lokal(WB)

            ElseIf WB.Sheets(1).Range("B2").Text.ToString.StartsWith("SBS") Then

                ImportSBSDenmarkSchedule(WB)

            ElseIf Not WB.Sheets(1).Range("A1").Value Is Nothing AndAlso InStr(WB.Sheets(1).Range("A1").Value.ToString.ToUpper, "TELECAST") Then

                ImportNewEurosportSchedule(WB)

            ElseIf Not WB.Sheets(1).Range("A1").Value Is Nothing AndAlso WB.Sheets(1).Range("A1").Value.ToString.ToUpper = "SCANDINAVIA" And Campaign.Area = "DK" Then

                ImportGenericSchedule(Campaign.Channels("National Geographic"), WB, 4, 1, 2, 3, 4, 5, , 6)

            ElseIf WB.Sheets(1).Cells(8, 1).Value IsNot Nothing AndAlso Not WB.Sheets(1).Cells(8, 1).Value.GetType.FullName = "System.Double" AndAlso WB.Sheets(1).Cells(8, 1).Value = "TV 2 Sport" Then 'WB.Sheets.Count > 1 AndAlso WB.Sheets(2).Name = "Visningsoversigt" Then

                ImportTV2SportDenmarkSchedule(WB)

            ElseIf WB.Sheets(1).Name.ToString.Contains("Digital") Then

                ImportCanalDigitalSchedule(WB)

            ElseIf cellContents.Contains("Disney XD Scandi") Then

                ImportGenericSchedule(WB, Campaign.Channels("Disney XD"), 18, 2, 3, 10, -1, 6, -1, -1)

            Else
                'Other channels without good signatures

                Trinity.Helper.WriteToLogFile("ImportSchedule: No schedule found. Attempting to load a general format")

                'Eurosport
                row = 1
                While WB.Sheets(1).Cells(row, 1).Value Is Nothing And row < 200
                    row += 1
                End While
                If Not WB.Sheets(1).Cells(row, 1).Value Is Nothing AndAlso WB.Sheets(1).Cells(row, 1).Value.ToString.Length > 9 AndAlso WB.Sheets(1).Cells(row, 1).Value.ToString.Substring(0, 9) = "LOC AVAIL" Then

                    ImportEurosportSchedule(WB)

                End If

                'The Voice
                row = 1
                While (WB.Sheets(1).Cells(row, 2).Value Is Nothing OrElse WB.Sheets(1).Cells(row, 1).Value Is Nothing) And row < 200
                    row += 1
                End While
                If WB.Sheets(1).Cells(row, 2).Value.GetType.Name = "String" AndAlso WB.Sheets(1).Cells(row, 2).Value.ToString.Length > 5 AndAlso WB.Sheets(1).Cells(row, 2).Value.ToString.ToUpper.Contains("VOICE") Then

                    ImportTheVoice(WB)

                End If

                'TV4 spec in odd format

                row = 1
                While WB.Sheets(1).Cells(row, 1).Value Is Nothing
                    row += 1
                    If row > 1000 Then Exit While
                End While
                If Not WB.Sheets(1).Cells(row, 1).Value Is Nothing AndAlso WB.Sheets(1).Cells(row, 1).Value = "Filmkod" Then

                    ImportGenericSchedule(WB, Campaign.Channels("TV4"), 2, 3, 4, 4, 1, 2, 11, 12)

                End If

                If Filename.ToString.ToUpper.Contains("CARTOON") Then

                    ImportGenericSchedule(WB, Campaign.Channels("Cartoon"), 2, 6, 7, 4, 5, -1, -1, -1)

                End If

                If Filename.ToString.ToUpper.Contains("EUROSPORT") Then

                    ImportGenericSchedule(WB, Campaign.Channels("Eurosport"), 1, 1, 6, 2, 8, -1, -1, -1)

                End If

                If Filename.ToString.ToUpper.Contains("GEO") Then

                    ImportNationalGeographicSchedule(WB)

                End If

                If WB.SheetCount > 1 Then
                    If Not WB.Sheets(2).Cells(1, 1).Value Is Nothing Then
                        If WB.Sheets(2).Cells(1, 1).Value.ToString.ToUpper.Contains("TELECAST") Then
                            ImportMTVSchedule(WB)
                        End If
                    End If

                End If

            End If

            WB.Close()
        Next
        Excel.DisplayAlerts = True
        Excel.ScreenUpdating = True
        Excel.Quit()
        Excel = Nothing
        frmMain.tmrAutosave.Enabled = True

        On Error GoTo 0
        Exit Sub

cmdImportSchedule_Click_Error:

        en = Err.Number
        ed = Err.Description
        If en = 32755 Then Exit Sub
        If IsIDE() Then
            a = MsgBox("Error:" & Chr(10) & Chr(10) & "Runtime Error '" & en & "':" & Chr(10) & Chr(10) & ed & Chr(10) & Chr(10) & "Vill du utföra en felsökning?", vbYesNo, "TRINITY")
            If a = vbNo Then Exit Sub
            ' Stop
            Resume
        End If
        '        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI

        MsgBox("Runtime Error '" & en & "':" & Chr(10) & Chr(10) & ed & " in cmdImportSchedule_Click.", vbCritical, "Error")
        Excel.Quit()
        Trinity.Helper.WriteToLogFile("ERROR IN frmSpots/cmdImportSchedule_Click!")
    End Sub

    Private Sub ImportSBSDenmarkSchedule(ByVal WB as CultureSafeExcel.Workbook)
        Dim Row As Integer
        Dim r As Integer
        Dim StartRow As Integer
        Dim d As String
        Dim c As Integer
        Dim ChanCol As Integer
        Dim DateCol As Integer
        Dim TimeCol As Integer
        Dim FilmCol As Integer
        Dim ProgBeforeCol As Integer
        Dim ProgAfterCol As Integer
        Dim LengthCol As Integer
        Dim t As String

        Dim TmpDate As Date
        Dim StartDate As Date
        Dim EndDate As Date

        Dim Idx As Single
        Dim BT As String
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim MaM As Integer
        Dim fk As String
        Dim Channel As Trinity.cChannel
        Dim Gender As String
        Dim AgeStr As String
        Dim MinAge As Integer
        Dim MaxAge As Integer

        Dim Channels As New List(Of String)

        With WB.Sheets(1)

            Row = 1
            While .cells(Row, 2).value <> "CH"
                Row += 1
            End While
            Row += 1
            StartRow = Row
            c = 2
            While .cells(StartRow - 1, c).value <> ""
                Select Case .cells(StartRow - 1, c).value
                    Case "CH"
                        ChanCol = c
                    Case "TITLE"
                        FilmCol = c
                    Case "TX/DATE"
                        DateCol = c
                    Case "PROGRAMME BEFORE"
                        ProgBeforeCol = c
                    Case "PROGRAMME AFTER"
                        ProgAfterCol = c
                    Case "TX/TIME"
                        TimeCol = c
                    Case "LEN"
                        LengthCol = c
                End Select
                c += 1
            End While
            While .cells(Row, 2).value <> ""
                If Not Channels.Contains(.cells(Row, 2).value) Then
                    Channels.Add(.cells(Row, 2).value)
                    Select Case .cells(Row, 2).value
                        Case "Kanal 4"
                            Channels.Add("K4")
                        Case "Kanal 5"
                            Channels.Add("K5")
                        Case "6´eren"
                            Channels.Add("6er")
                    End Select
                End If
                Row += 1
            End While
            Dim Target As String = .range("G11").Text
            If InStr(Target, " ") > 0 Then
                Gender = Target.Substring(0, 1)
                AgeStr = Mid(Target, 3)
            Else
                Gender = Target.Substring(0, 1)
                AgeStr = Mid(Target, 2)
            End If
            If InStr(AgeStr, "-") > 1 Then
                MinAge = AgeStr.Substring(0, InStr(AgeStr, "-") - 1)
                MaxAge = Mid(AgeStr, InStr(AgeStr, "-") + 1)
            Else
                MinAge = AgeStr.Substring(0, InStr(AgeStr, ".") - 1)
                MaxAge = Mid(AgeStr, InStr(AgeStr, ".") + 1)
            End If
            Select Case Gender
                Case "K" : Gender = "W"
                Case "M" : Gender = "M"
                Case Else : Gender = ""
            End Select
            For Each TmpChan As String In Channels

                If Not LongName.ContainsKey(TmpChan) Then
                    'Windows.Forms.MessageBox.Show("Channel " & TmpChan & " was not recognized and will be skipped.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    Channel = Campaign.Channels(LongName(TmpChan))
                    Channel.IsVisible = True

                    r = StartRow

                    Channel.MainTarget.TargetName = Gender & Trim(MinAge) & "-" & Trim(MaxAge)

                    d = .Cells(r, DateCol).value
                    If .Cells(r, DateCol).value.GetType.Name = "Double" Then
                        StartDate = Date.FromOADate(d)
                    Else
                        StartDate = CDate(d)
                    End If
                    While Not .Cells(r, 2).value Is Nothing
                        r = r + 1
                    End While
                    If .Cells(r - 1, DateCol).value.GetType.Name = "Double" Then
                        d = Date.FromOADate(.Cells(r - 1, DateCol).value)
                    Else
                        d = CDate(.Cells(r - 1, DateCol).value)
                    End If
                    EndDate = d
                    Dim frmImport As New frmImport(Channel)
                    frmImport.dtFrom.Value = StartDate
                    frmImport.dtTo.Value = EndDate

                    frmImport.txtIndex.Text = 100
                    frmImport.Text = "Import Schedule - " & Channel.ChannelName
                    frmImport.lblPath.Tag = WB.Path
                    frmImport.lblPath.Text = WB.Name
                    Dim tmpNetBudget As Single
                    tmpNetBudget = .range("G7").value
                    frmImport.lblConfirmationBudget.Text = Format(CDec(tmpNetBudget), "N0")


                    If frmImport.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                        CleanUp()
                        Exit Sub
                    End If

                    Idx = frmImport.txtIndex.Text
                    BT = frmImport.cmbBookingType.Text
                    Channel.BookingTypes(BT).ContractNumber = .range("D6").value
                    Channel.MainTarget.TargetName = Channel.BookingTypes(BT).BuyingTarget.Target.TargetName
                    Channel.MainTarget.Universe = Channel.BookingTypes(BT).BuyingTarget.Target.Universe
                    If frmImport.chkReplace.Checked Then
                        For Each TmpSpot In Campaign.PlannedSpots
                            If TmpSpot.Channel.ChannelName = Channel.ChannelName Then
                                If TmpSpot.Bookingtype.Name = BT Then
                                    If TmpSpot.AirDate >= frmImport.dtFrom.Value.ToOADate Then
                                        If TmpSpot.AirDate <= frmImport.dtTo.Value.ToOADate Then
                                            If Not TmpSpot.MatchedSpot Is Nothing Then
                                                TmpSpot.MatchedSpot.MatchedSpot = Nothing
                                            End If
                                            Campaign.PlannedSpots.Remove(TmpSpot.ID)
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    End If
                    Row = StartRow
                    While Not .Cells(Row, ChanCol).value Is Nothing AndAlso .Cells(Row, ChanCol).value.ToString <> ""
                        d = .Cells(Row, DateCol).value
                        If .Cells(Row, DateCol).value.GetType.Name = "Double" Then
                            TmpDate = Date.FromOADate(d)
                        Else
                            TmpDate = CDate(d)
                        End If
                        If .cells(Row, ChanCol).value = "6´eren" Then .cells(Row, ChanCol).value = "6eren"
                        If TmpDate >= frmImport.dtFrom.Value AndAlso TmpDate <= frmImport.dtTo.Value AndAlso Channel.ChannelName = .cells(Row, ChanCol).value Then
                            TmpSpot = Campaign.PlannedSpots.Add(CreateGUID)
                            TmpSpot.Channel = Channel
                            TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(BT)
                            TmpSpot.AirDate = TmpDate.ToOADate
                            t = .Cells(Row, TimeCol).Text
                            MaM = 60 * Val(t.Substring(0, 2)) + Val(t.Substring(3, 2))
                            If MaM < 120 Then MaM = MaM + 1440
                            TmpSpot.MaM = MaM
                            If ProgAfterCol > -1 Then
                                TmpSpot.Programme = .Cells(Row, ProgAfterCol).value
                                TmpSpot.ProgAfter = .Cells(Row, ProgAfterCol).value
                            End If
                            If ProgBeforeCol > -1 Then
                                TmpSpot.Programme = .Cells(Row, ProgBeforeCol).value
                            End If
                            If FilmCol > -1 Then
                                fk = .cells(Row, FilmCol).value
                            Else
                                fk = ""
                            End If
                            TmpSpot.Filmcode = fk
                            TmpSpot.SpotLength = .cells(Row, LengthCol).value
                            TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                            If TmpSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(TmpSpot.AirDate) Then
                                DateOutOfPeriod.Add(DateOutOfPeriod.Count, TmpSpot.AirDate)
                                Campaign.PlannedSpots.Remove(TmpSpot.ID)
                            End If
                            If Not TmpSpot.Week Is Nothing Then

                                Trinity.Helper.SetFilmForSpot(TmpSpot)

                                If Not TmpSpot.Film Is Nothing AndAlso TmpSpot.SpotLength = 0 Then
                                    TmpSpot.SpotLength = TmpSpot.Film.FilmLength
                                End If
                                TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                                'TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = 0
                                TmpSpot.MyRating = 0
                                'TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0
                                TmpSpot.PriceNet = 0
                                TmpSpot.PriceGross = 0
                            End If
                        End If
                        Row = Row + 1
                    End While
                End If
            Next
        End With
        If DateOutOfPeriod.Count > 0 Then
            Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
            For i As Integer = 0 To DateOutOfPeriod.Count
                Msg += Format(Date.FromOADate(DateOutOfPeriod(i)), "Short date") & vbCrLf
            Next
            Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        On Error GoTo 0
        Exit Sub

cmdImportSchedule_Click_Error:
'        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Err.Raise(Err.Number)
    End Sub

    Function readValue(ByVal searchString As String, ByVal intStartCol As Integer, ByVal intStartRow As Integer, Optional ByVal offSet As Long = 1, Optional ByVal offSetValue As Long = 3, Optional ByVal vertical As Boolean = True) As String
        'this function requires the WB variable to be declared
        'it does not handle negative values for columns and rows

        ' This function reads a value form the Excel file.
        ' the searchString value is the string you want to search for, the headline to the value.
        ' Note that the strings is always compared in upper case. IT IS NOT CASE SENSITIVE!
        ' startLocation is the initial cell (in (x,y) format)
        ' The offSet is how many cells from the startLocation we will search, a offSet = 2 and a initial cell(3,3) will search the cells 1,1 to 5,5 starting from the initial cell and working outwards
        ' The offSetValue is how many cells we are gonna search to get a value after we have gotten the searched for cell.
        ' the vertical value if whether we want a value that is to the right of the searched cell (True) or a cell that is beneath (False)
        ' we have 2 error codes (return strings), "#0001#" is searchString not found and "#0002#" is no value found after the searchedString

        'get the individual values for the X and Y
        Dim intStartY As Integer = intStartCol
        Dim intStartX As Integer = intStartRow

        'declarations of a few variables
        Dim x As Integer
        Dim y As Integer
        Dim z As Integer

        'make the string in uppercase
        searchString = searchString.ToUpper

        'ta bort denna OBS********************
        '*****
        Dim WB As New Object

        With WB.Sheets(1)
            'read the spot of cell and go to read_value if its the correct
            If Not .Cells(intStartX, intStartY).value Is Nothing AndAlso .Cells(intStartX, intStartY).value.ToString.ToUpper = searchString Then
                GoTo get_value
            End If

            'we loop the offset figure to extend the search outwards from the initial target cell
            For z = 1 To offSet

                'reads a row stright down from the offset top left corner
                For x = (intStartX - z) To (intStartX + z)
                    'if we find the value we go to read_value after we have saved the cell
                    If Not .Cells(x, intStartY - z).value Is Nothing AndAlso .Cells(x, intStartY - z).value.ToString.ToUpper = searchString Then
                        intStartX = x
                        intStartY = intStartY - z
                        GoTo get_value
                    End If
                Next
                'remove the last addition
                x -= 1

                'read the row from left to right
                For y = (intStartY - z + 1) To (intStartY + z)
                    'if we find the value we go to read_value after we have saved the cell
                    If Not .Cells(x, y).value Is Nothing AndAlso .Cells(x, y).value.ToString.ToUpper = searchString Then
                        intStartX = x
                        intStartY = y
                        GoTo get_value
                    End If
                Next
                'remove the last addition
                y -= 1

                'we read the right column
                For x = (intStartX - z) To (intStartX + z - 1)
                    'if we find the value we go to read_value after we have saved the cell
                    If Not .Cells(x, y).value Is Nothing AndAlso .Cells(x, y).value.ToString.ToUpper = searchString Then
                        intStartX = x
                        intStartY = y
                        GoTo get_value
                    End If
                Next
                'remove the last addition
                x = intStartX - z

                'read the top row 

                For y = (intStartY - z + 1) To (intStartY + z - 1)
                    'if we find the value we go to read_value after we have saved the cell
                    If Not .Cells(x, y).value Is Nothing AndAlso .Cells(x, y).value.ToString.ToUpper = searchString Then
                        intStartX = x
                        intStartY = y
                        GoTo get_value
                    End If
                Next

            Next
            'if we have not found a match we send back a error
            Return "#0001#"

get_value:
            'we continue here if we have found a match in the first part

            If vertical Then
                'we search up to x number of cells to the right of the value we found
                For y = intStartY + 1 To intStartY + 1 + offSetValue
                    If Not .Cells(intStartX, y).value Is Nothing AndAlso Not .Cells(intStartX, y).value.ToString = "" Then
                        Return .Cells(intStartX, y).value
                    End If
                Next
            Else
                'we search up to x number of cells beneath the value we found
                For x = intStartX + 1 To intStartX + 1 + offSetValue
                    If Not .Cells(x, intStartY).value Is Nothing AndAlso Not .Cells(x, intStartY).value.ToString = "" Then
                        Return .Cells(x, intStartY).value
                    End If
                Next
            End If
        End With

        'if we have not found a cell with a value but found the match string
        Return "#0002#"
    End Function
End Module
