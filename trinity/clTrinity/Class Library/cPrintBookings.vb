Imports clTrinity.CultureSafeExcel

Public Class cPrintBookings
    Implements IDisposable

    Public Enum LogoAlignmentEnum
        Left
        Center
        Right
    End Enum

    Dim _excel As Application
    Dim _wb As Workbook
    Dim _sheet As Worksheet

    Dim _langIni As New IniFile()

    Private _bookingType As Trinity.cBookingType
    Private _combination As Trinity.cCombination

    Private _lastRow As Integer

    Private _topRow As Integer

    Property HeadlineColor As Color
    Property PanelColor As Color
    Property PanelTextColor As Color

    Property LogoPath As String = ""
    Property LogoAlignment As LogoAlignmentEnum = LogoAlignmentEnum.Right
    Property FontName As String = ""
    Property SkipWeeks As New List(Of String)

    Property CustomOrderPlacer As String
    Property CustomBillingAddress As String


    'Property BookingComment As String

    ReadOnly Property BookingDisclaimer As String
        Get
            Select Case Campaign.Area
                Case Is = "SE"
                    Return "Vi ber er ange ovanstående ordernummer vid all korrespondens angående denna order. " & _
                "Detta ordernummer måste uppges på er faktura. Fakturor utan ordernummer kommer att skickas " & _
                "tillbaka utan att betalas."
                Case Is = "NO"
                    Return "Vi ber dere angi ovenstående ordrenummer ved all korrespondanse angående denne ordren." & _
                "Ordrenummeret må oppgis på faktura. Fakturaer uten ordrenummer vil returneres uten å betales."
                Case Else
                    Return ""
            End Select
        End Get
    End Property

    Private Enum TypeEnum
        RBS
        Specifics
    End Enum

    Private _type As TypeEnum

    Public Sub New(Language As String)
        _excel = New Application(False)
        _langIni.Create(Language)
    End Sub

    Sub CreateBooking(Bookingtype As Trinity.cBookingType, Contact As String)

        _bookingType = Bookingtype
        _combination = Nothing

        PrepareEnvironment()
        Try
            If (Bookingtype.IsSpecific AndAlso Not Bookingtype.IsRBS) OrElse (Bookingtype.IsPremium AndAlso Not Bookingtype.IsRBS) Then
                CreateSpecificBooking(Contact)
            End If
            If Bookingtype.IsRBS Then
                CreateRBSBooking(Contact)
            End If
        Catch ex As Exception
            ResetEnvironment()
            Throw ex
        End Try
        ResetEnvironment()
        _excel.Visible = True
    End Sub

    Sub CreateBooking(Combination As Trinity.cCombination, Contact As String)

        _bookingType = Combination.Relations(1).Bookingtype
        _combination = Combination

        PrepareEnvironment()

        CreateRBSBooking(Combination, Contact)

        ResetEnvironment()

        '_excel.Visible = True

        CleanupPage()
        _excel.Visible = True

    End Sub

    Protected Sub CreateSpecificBooking(Contact As String)
        If Not CheckFilms() Then
            Dim _filmList As String = String.Join(vbCrLf, (From _spot As Trinity.cBookedSpot In _bookingType.Main.BookedSpots() Select Filmcode = _spot.Filmcode).Distinct().Where(Function(fk) _bookingType.Weeks(1).Films(fk) Is Nothing).ToArray())
            Throw New Exception("Could not create booking for '" & _bookingType.ToString & "':" & vbCrLf & vbCrLf & "Spots are booked on the following filmcodes, that are not a a part of the campaign:" & vbCrLf & _filmList)
            Exit Sub
        End If

        CreateWorkbook()

        _type = TypeEnum.Specifics

        PrintHeadline(1, 2, _bookingType.ParentChannel.ChannelName & " " & _bookingType.Name & " " & _langIni.Text("Booking", "Booking") & " - " & _bookingType.Main.Name)

        PrintHeader(Contact)

        PrintColumnHeadlines()

        PrintSpots()

        InsertLogo(2, _sheet.Range("N:N").Column)

        CleanupPage()
        
        _excel.Visible = True
    End Sub

    Sub CreateWorkbook()
        _wb = _excel.AddWorkbook
        _sheet = _wb.Sheets(1)

        SetColors()
        PreparePage()
    End Sub

    Protected Sub CreateRBSBooking(Contact As String)
        CreateWorkbook()

        _type = TypeEnum.RBS

        PrintHeadline(3, 1, _bookingType.ParentChannel.ChannelName & " - " & IIf(_langIni.Text("Booking", _bookingType.Name) = "", _bookingType.Name, _langIni.Text("Booking", _bookingType.Name)) & " " & _langIni.Text("Booking", "Booking"))

        PrintHeader(Contact)

        PrintCustomFields()

        PrintDayparts()

        PrintFilms()

        With _sheet

            PrintWeeks()
            PrintIndexes()
            PrintComments()
            PrintOther()
            PrintPremiums()

            'Create border around booking
            CreatePanel(.Range("A1:L" & _topRow + 13 + _premiumRows), True, False, 0)

        End With
        InsertLogo(1, 13, True)
        CleanupPage()
        _excel.Visible = True
    End Sub

    Sub CreateRBSBooking(Combination As Trinity.cCombination, Contact As String)

        _combination = Combination

        CreateWorkbook()

        _type = TypeEnum.RBS

        PrintHeadline(3, 1, Combination.Name & " - " & _langIni.Text("Booking", "Booking"))

        PrintHeader(Contact)

        PrintCustomFields()

        PrintDayparts()

        PrintFilms()

        With _sheet

            PrintWeeks()
            PrintChannels(Combination)
            PrintIndexes()
            PrintComments()
            PrintOther()
            PrintPremiums()

            'Create border around booking
            CreatePanel(.Range("A1:L" & _topRow + 13 + _premiumRows), True, False, 0)

        End With

        CleanupPage()
        _excel.Visible = True
        InsertLogo(1, 13, True)
    End Sub

    Private _colCPP As Integer
    Private _colBudget As Integer
    Private _totBudget As Single = 0
    Private _firstRow As Integer

    Sub PrintWeeks()
        If _topRow < 30 Then _topRow = 30

        If TrinitySettings.PrintIndexes Then
            _colCPP = 10
            _colBudget = 12
        Else
            _colCPP = 9
            _colBudget = 11
        End If

        'if we are supposed to print indexes we do so

        PrintWeekHeadlines()

        With _sheet
            Dim _headlineRange As String
            If TrinitySettings.PrintIndexes Then
                .Cells(_topRow + 1, 9).Value = _langIni.Text("Booking", "Index") & "*"
                _headlineRange = "A" & _topRow & ":L" & _topRow + 1
            Else
                _headlineRange = "A" & _topRow & ":K" & _topRow + 1
            End If
            With CreatePanel(.Range(_headlineRange), True, True, -4108)
                .Font.Bold = True
            End With

            _topRow = _topRow + 1

            Dim _rowColors(0 To 1) As Integer
            Dim _color As Integer = 0
            Dim _row As Integer = _topRow + 1


            _rowColors(0) = 48
            _rowColors(1) = 15
            For _w As Integer = 1 To _bookingType.Weeks.Count
                If Not SkipWeeks.Contains(_bookingType.Weeks(_w).Name) Then
                    _firstRow = _row
                    PrintWeekRow(_bookingType.Weeks(_w), _row, _rowColors(_color))
                    _color = 1 + (_color = 1)
                End If
            Next
            PrintWeekSums(_row)
        End With
    End Sub

    Sub PrintWeekHeadlines()
        With _sheet
            .Cells(_topRow, 1).Value = _langIni.Text("Booking", "Week")
            .Cells(_topRow, 2).Value = _langIni.Text("Booking", "CampaignPeriod")
            .Range("B" & _topRow & ":C" & _topRow).Merge()
            .Range("B" & _topRow & ":C" & _topRow).HorizontalAlignment = -4108
            .Range("B" & _topRow + 1 & ":C" & _topRow + 1).Merge()
            .Cells(_topRow, 4).Value = _langIni.Text("Booking", "TRPShare")
            .Range("D" & _topRow & ":E" & _topRow).Merge()
            .Cells(_topRow, 6).Value = _langIni.Text("Booking", "FilmSplit")
            .Range("F" & _topRow & ":H" & _topRow).Merge()
            .Range("F" & _topRow & ":H" & _topRow).HorizontalAlignment = -4108
            .Cells(_topRow, _colCPP).Value = _langIni.Text("Booking", "CPP")
            .Cells(_topRow, _colBudget).Value = _langIni.Text("Booking", "Budget")
            .Cells(_topRow, _colBudget - 1).Value = _langIni.Text("Booking", "Budget")
            .Cells(_topRow + 1, 8).Value = _langIni.Text("Booking", "TRP")
            .Cells(_topRow + 1, _colBudget - 1).Value = _langIni.Text("Booking", "Filmcode")
            .Cells(_topRow + 1, _colBudget).Value = _langIni.Text("Booking", "Week")
            .Cells(_topRow + 1, 4).Value = "%"
            .Cells(_topRow + 1, 5).Value = _langIni.Text("Booking", "TRP")
            .Cells(_topRow + 1, 6).Value = _langIni.Text("Booking", "Filmcode")
            .Cells(_topRow + 1, 7).Value = "%"
            .Cells(_topRow + 1, 8).Value = _langIni.Text("Booking", "TRP")
        End With
    End Sub

    Sub PrintWeekRow(Week As Trinity.cWeek, ByRef Row As Integer, Color As Integer)
        With _sheet
            .Cells(Row, 1).Value = Trim(Week.Name)
            .Cells(Row, 2).Value = Trinity.Helper.FormatShortDateForBooking(Date.FromOADate(Week.StartDate)) & " - " & Trinity.Helper.FormatShortDateForBooking(Date.FromOADate(Week.EndDate))
            .Range("B" & Row & ":C" & Row).Merge()
            .Range("B" & Row & ":C" & Row).HorizontalAlignment = -4108
            .Cells(Row, 4).Numberformat = "0%"

            Dim _trp As Single
            Dim _index As Single
            Dim _cpp As Single
            Dim _netBudget As Double

            If _combination Is Nothing Then
                _trp = Week.TRPBuyingTarget
                _index = Week.Index

                _netBudget = Week.NetBudget
            Else
                For Each _cc As Trinity.cCombinationChannel In _combination.Relations
                    If Not _cc.Bookingtype.IsPremium Then
                        _trp += _cc.Bookingtype.Weeks(Week.Name).TRPBuyingTarget
                        _index += _cc.Bookingtype.Weeks(Week.Name).Index() * _cc.Percent(True)
                        _cpp += _cc.Bookingtype.Weeks(Week.Name).NetCPP * _cc.Percent(True)
                        If Not _cc.Bookingtype.IsCompensation Then
                            _netBudget += _cc.Bookingtype.Weeks(Week.Name).NetBudget
                        End If
                    End If
                Next
            End If

            .Cells(Row, 5).Value = _trp
            .Cells(Row, 5).Numberformat = "0.0"
            If TrinitySettings.PrintIndexes Then
                .Cells(Row, 9).Value = _index * 100
                .Cells(Row, 9).Numberformat = "0"
            End If
            .Cells(Row, _colBudget).Numberformat = "#,##0 $"
            .Cells(Row, _colBudget).Value = _netBudget
            _totBudget += _netBudget
            For _f As Integer = 1 To Week.Films.Count
                Dim _share As Double = 0
                If _combination Is Nothing Then
                    _share = (Week.Films(_f).Share() / 100)
                    _cpp = Week.NetCPP30 * (Week.Films(_f).Index / 100)
                    _netBudget = Week.NetBudget * (Week.Films(_f).Share(Trinity.cFilm.FilmShareEnum.fseBudget) / 100)
                Else
                    _cpp = 0
                    _netBudget = 0
                    For Each _cc As Trinity.cCombinationChannel In _combination.Relations
                        If Not _cc.Bookingtype.IsPremium Then
                            _share += (_cc.Bookingtype.Weeks(Week.Name).Films(_f).Share / 100) * _cc.Percent(True)
                            _cpp += _cc.Bookingtype.Weeks(Week.Name).NetCPP30 * (_cc.Bookingtype.Weeks(Week.Name).Films(_f).Index / 100) * _cc.Percent(True)
                            If Not _cc.Bookingtype.IsCompensation Then
                                _netBudget += _cc.Bookingtype.Weeks(Week.Name).NetBudget * (_cc.Bookingtype.Weeks(Week.Name).Films(_f).Share(Trinity.cFilm.FilmShareEnum.fseBudget) / 100)
                            End If
                        End If
                    Next
                End If
                If _share > 0 Then
                    .Cells(Row, _colCPP).Numberformat = "#,#0.0"
                    .Cells(Row, _colCPP).Value = _cpp
                    .Cells(Row, _colBudget - 1).Numberformat = "#,##0 $"
                    .Cells(Row, _colBudget - 1).Value = _netBudget
                    If Week.Films(_f).Filmcode.TrimEnd(",") <> "" Then
                        .Cells(Row, 6).Value = Week.Films(_f).Filmcode.TrimEnd(",")
                    Else
                        .Cells(Row, 6).Value = Week.Films(_f).Name
                    End If
                    .Cells(Row, 7).Numberformat = "0%"
                    .Cells(Row, 7).Value = _share
                    .Cells(Row, 8).Numberformat = "#,#0.0"
                    .Cells(Row, 8).Value = _share * _trp
                    If TrinitySettings.PrintIndexes Then
                        .Range("A" & Row & ":L" & Row).Interior.ColorIndex = Color
                    Else
                        .Range("A" & Row & ":K" & Row).Interior.ColorIndex = Color
                    End If
                    Row += 1
                End If
            Next

            While Row - 1 < _firstRow
                Row += 1
            End While
            .Range("A" & _firstRow & ":A" & Row - 1).Merge()
            .Range("B" & _firstRow & ":C" & Row - 1).Merge()
            .Range("D" & _firstRow & ":D" & Row - 1).Merge()
            .Range("E" & _firstRow & ":E" & Row - 1).Merge()
            If TrinitySettings.PrintIndexes() Then
                .Range("L" & _firstRow & ":L" & Row - 1).Merge()
                .Range("I" & _firstRow & ":I" & Row - 1).Merge()
            Else
                .Range("K" & _firstRow & ":K" & Row - 1).Merge()
            End If
        End With
    End Sub

    Sub PrintWeekSums(ByRef Row As Integer)
        With _sheet
            For _rr As Integer = _topRow + 1 To Row - 1
                .Cells(_rr, 4).Formula = "=" & .Cells(_rr, 5).Address & "/" & .Cells(Row, 5).Address
            Next
            If TrinitySettings.PrintIndexes Then
                With CreatePanel(IIf(TrinitySettings.PrintIndexes, .Range("A" & _topRow + 1 & ":L" & Row), .Range("A" & _topRow + 1 & ":K" & Row)), True, False, -4108)
                    For _x As Integer = 11 To 12
                        With .Borders(_x)
                            .LineStyle = 1
                        End With
                    Next
                End With
            End If
            .Cells(Row, 1).Value = _langIni.Text("Booking", "Total")
            .Cells(Row, 4).Value = "100%"
            .Cells(Row, 5).Formula = "=SUM(" & .Cells(_topRow + 1, 5).Address & ":" & .Cells(Row - 1, 5).Address
            .Cells(Row, _colBudget).Numberformat = "#,##0 $"
            .Cells(Row, _colBudget).Formula = "=SUM(" & .Cells(_topRow + 1, _colBudget).Address & ":" & .Cells(Row - 1, _colBudget).Address
            .Columns(11).AutoFit()
            .Range("B" & Row & ":C" & Row).Merge()
            .Range("B" & Row & ":C" & Row).HorizontalAlignment = -4108
            .Range("F" & Row & ":H" & Row).Merge()
            .Range("F" & Row & ":H" & Row).HorizontalAlignment = -4108
            _topRow = Row + 3
        End With
    End Sub

    Sub PrintIndexes()
        If TrinitySettings.PrintIndexes AndAlso _bookingType.Indexes.Count > 0 Then
            With _sheet
                .Cells(_topRow, 1).Value = "*" & _langIni.Text("Booking", "Indexes")
                .Cells(_topRow + 1, 1).Value = _langIni.Text("Booking", "Name")
                .Cells(_topRow + 1, 2).Value = _langIni.Text("Spotlist", "Period")
                .Cells(_topRow + 1, 4).Value = _langIni.Text("Booking", "Index")
                With CreatePanel(.Range("A" & _topRow & ":L" & _topRow + 1), 2, True)
                    .Font.Bold = True
                End With
                _topRow += 1
                For Each _index As Trinity.cIndex In _bookingType.Indexes
                    If (_index.ToDate.ToOADate >= Campaign.StartDate And _index.ToDate.ToOADate <= Campaign.EndDate) Or (_index.FromDate.ToOADate >= Campaign.StartDate And _index.FromDate.ToOADate <= Campaign.EndDate) Or (_index.FromDate.ToOADate <= Campaign.StartDate And _index.ToDate.ToOADate >= Campaign.EndDate) Then
                        _topRow += 1
                        .Cells(_topRow, 1).Value = _index.Name
                        .Range("B" & _topRow & ":C" & _topRow).Merge()
                        .Cells(_topRow, 2).Value = Trinity.Helper.FormatShortDateForBooking(_index.FromDate) & " - " & Trinity.Helper.FormatShortDateForBooking(_index.ToDate)
                        .Cells(_topRow, 4).Numberformat = "##,##0.0"
                        .Cells(_topRow, 4).HorizontalAlignment = -4131
                        .Cells(_topRow, 4).Value = _index.Index
                    End If
                Next
                _topRow += 3
            End With
        End If
    End Sub

    Sub PrintChannels(Combination As Trinity.cCombination)
        With _sheet
            .Cells(_topRow, 1).Value = _langIni.Text("Booking", "Channels")
            With CreatePanel(.Range("A" & _topRow & ":L" & _topRow), 2, True)
                .Font.Bold = True
            End With
            For Each _chan As Trinity.cCombinationChannel In Combination.Relations
                _topRow += 1
                .Cells(_topRow, 1).Value = _chan.ChannelName
                If _chan.Relation > 0 Then
                    .Cells(_topRow, 2).Numberformat = "0%"
                    .Cells(_topRow, 2).Value = _chan.Percent
                End If
            Next
            _topRow += 2
        End With
    End Sub

    Sub PrintComments()
        With _sheet
            .Cells(_topRow, 1).Value = _langIni.Text("Booking", "Comments")
            .Range("A" & _topRow + 1 & ":L" & _topRow + 5).Merge()
            .Range("A" & _topRow + 1 & ":L" & _topRow + 5).VerticalAlignment = -4160
            .Range("A" & _topRow + 1 & ":L" & _topRow + 5).WrapText = True
            .Cells(_topRow + 1, 1).Value = _bookingType.Comments
            With CreatePanel(.Range("A" & _topRow & ":L" & _topRow), 2, True)
                .Font.Bold = True
            End With

        End With
    End Sub

    Sub PrintOther()
        With _sheet
            .Cells(_topRow + 6, 1).Value = _langIni.Text("Booking", "Other")
            With CreatePanel(.Range("A" & _topRow + 6 & ":L" & _topRow + 6), 2, True)
                .Font.Bold = True
            End With
        End With
    End Sub

    Private _premiumRows As Integer = 0
    Sub PrintPremiums()
        If _combination IsNot Nothing Then
            For Each _cc As Trinity.cCombinationChannel In _combination.Relations
                If _cc.Bookingtype.IsPremium Then
                    AddPremium(_cc.Bookingtype, (From _tmpCC As Trinity.cCombinationChannel In _combination.Relations Select _tmpCC Where _tmpCC.Bookingtype.IsPremium).Count > 1)
                End If
            Next
        Else
            If _bookingType.IsPremium OrElse (_bookingType.IsRBS AndAlso _bookingType.IsSpecific) Then
                AddPremium(_bookingType, False)
            End If
        End If
    End Sub

    Private Sub AddPremium(Bookingtype As Trinity.cBookingType, IncludeChannelColumn As Boolean)
        With _sheet
            If _premiumRows = 0 Then
                .Cells(_topRow + 13 + _premiumRows, 1).Value = _langIni.Text("Booking", "Premiums")
                With CreatePanel(.Range("A" & _topRow + 13 + _premiumRows & ":L" & _topRow + 13 + _premiumRows), 2, True)
                    .Font.Bold = True
                End With
                _premiumRows += 1
                .Cells(_topRow + 13 + _premiumRows, 1).Value = _langIni.Text("Booking", "Date")
                .Cells(_topRow + 13 + _premiumRows, 2).Value = _langIni.Text("Booking", "Time")
                If IncludeChannelColumn Then
                    .Cells(_topRow + 13 + _premiumRows, 4).Value = _langIni.Text("Booking", "Channel")
                    .Cells(_topRow + 13 + _premiumRows, 4).Value = _langIni.Text("Booking", "Programme")
                Else
                    .Cells(_topRow + 13 + _premiumRows, 3).Value = _langIni.Text("Booking", "Programme")
                End If

                .Rows(_topRow + 13 + _premiumRows).Font.Bold = True
                _premiumRows += 1
            End If
            For Each TmpSpot As Trinity.cBookedSpot In From _spot As Trinity.cBookedSpot In Bookingtype.Main.BookedSpots Select _spot Where _spot.Bookingtype Is Bookingtype
                .Cells(_topRow + 13 + _premiumRows, 1).Value = Format(TmpSpot.AirDate, "Short date")
                .Cells(_topRow + 13 + _premiumRows, 2).Value = Trinity.Helper.Mam2Tid(TmpSpot.MaM)
                If IncludeChannelColumn Then
                    .Cells(_topRow + 13 + _premiumRows, 3).Value = TmpSpot.Channel.ChannelName
                    .Cells(_topRow + 13 + _premiumRows, 4).Value = TmpSpot.Programme
                Else
                    .Cells(_topRow + 13 + _premiumRows, 3).Value = TmpSpot.Programme
                End If
                .Rows(_topRow + 13 + _premiumRows).HorizontalAlignment = -4131
                _premiumRows += 1
            Next
        End With
    End Sub

    Sub PrintCustomFields()
        With _sheet
            If CustomOrderPlacer <> "" Then
                Dim addressSplit As String() = Strings.Split(CustomOrderPlacer, ",")
                .Cells(7, 7).Value = "Booked on behalf of:"
                .Cells(7, 7).Font.Bold = True
                Dim addressRow As Integer = 0
                For Each rowstring As String In addressSplit
                    .Cells(7 + addressRow, 8).Value = rowstring
                    addressRow += 1
                Next
                .Range("H7:H10").Font.Color = HeadlineColor.ToArgb
            End If
            If CustomBillingAddress <> "" Then
                Dim addressSplit As String() = Strings.Split(CustomBillingAddress, ",")
                .Cells(11, 7).Value = "Please bill:"
                .Cells(11, 7).Font.Bold = True
                Dim addressRow As Integer = 0
                For Each rowstring As String In addressSplit
                    .Cells(11 + addressRow, 8).Value = rowstring
                    addressRow += 1
                Next
                .Range("H11:H14").Font.Color = HeadlineColor.ToArgb
            End If
        End With
    End Sub

    Protected Sub PrintHeadline(Row As Integer, Column As Integer, Headline As String)
        With _sheet.Cells(Row, Column)
            .Value = Headline
            .Font.Bold = True
            .Font.Size = 14
            .Font.Color = HeadlineColor.ToArgb
        End With
    End Sub

    Protected Sub PrintHeader(Contact As String)
        With _sheet
            If _type = TypeEnum.Specifics Then
                .Cells(3, 2).Value = _langIni.Text("Booking", "To")
                .Cells(3, 3).Value = Contact
                .Cells(4, 2).Value = _langIni.Text("Booking", "From")
                .Cells(4, 3).Value = TrinitySettings.UserName
                .Cells(5, 3).Value = TrinitySettings.UserEmail
                .Cells(6, 3).Value = TrinitySettings.UserPhoneNr
                .Cells(8, 2).Value = _langIni.Text("Booking", "Advertiser")
                .Cells(8, 3).Value = Campaign.Client
                .Cells(9, 2).Value = _langIni.Text("Booking", "Product")
                .Cells(9, 3).Value = Campaign.Product
                .Cells(10, 2).Value = _langIni.Text("Booking", "CampaignPeriod")
                .Cells(10, 3).Value = Trinity.Helper.FormatDateForBooking(Date.FromOADate(Campaign.StartDate)) & " - " & Trinity.Helper.FormatDateForBooking(Date.FromOADate(Campaign.EndDate))
                .Range("B3:B10").Font.Bold = True
                CreatePanel(.Range("B3:E10"), True, True)

                If _bookingType.PrintBookingCode Then
                    CreatePanel(.Range("B12:C14"), True, True)
                    .Cells(12, 2).Value = _langIni.Text("Booking", "Bookingcode")
                Else
                    CreatePanel(.Range("B13:C14"), True, True)
                End If

                .Cells(13, 2).Value = _langIni.Text("Booking", "OrderNumber")
                .Cells(13, 3).Value = _bookingType.OrderNumber
                .Cells(14, 2).Value = _langIni.Text("Booking", "ContractNumber")
                .Cells(14, 3).Value = _bookingType.ContractNumber

                .Range("B12:B14").Font.Bold = True

                CreatePanel(.Range("B16:C17"), True, True)
                .Range("B16:B17").Font.Bold = True

                .Cells(16, 2).Value = _langIni.Text("Booking", "Gross")
                .Cells(16, 3).Value = "=I23"
                .Cells(16, 3).Numberformat = "#,##0 $"
                .Cells(17, 2).Value = _langIni.Text("Booking", "Net")
                .Cells(17, 3).Value = "=K23"
                .Cells(17, 3).Numberformat = "#,##0 $"

                CreatePanel(.Range("H8:M17"), True, True)

                With .Range("H9:M17")
                    .Merge()
                    .WrapText = True
                    .HorizontalAlignment = -4131
                    .VerticalAlignment = -4160
                End With
                .Cells(8, 8).Value = _langIni.Text("Booking", "Comments")
                .Cells(9, 8).Value = _bookingType.Comments & Chr(10) & BookingDisclaimer
                .Cells(8, 8).Font.Bold = True

            Else

                If _combination Is Nothing Then
                    .Cells(5, 9).Value = _bookingType.ParentChannel.ChannelName
                Else
                    .Cells(5, 9).Value = _combination.Name
                End If
                .Cells(5, 1).Value = _langIni.Text("Booking", "To")
                .Cells(5, 3).Value = Contact
                .Cells(7, 1).Value = _langIni.Text("Booking", "From")
                .Cells(7, 3).Value = TrinitySettings.UserName
                .Cells(8, 3).Value = TrinitySettings.UserEmail
                .Cells(9, 3).Value = TrinitySettings.UserPhoneNr
                .Cells(11, 1).Value = _langIni.Text("Booking", "Advertiser")
                .Cells(11, 3).Value = _bookingType.Main.Client
                .Cells(13, 1).Value = _langIni.Text("Booking", "Product")
                .Cells(13, 3).Value = _bookingType.Main.Product
                .Range("A5:A19").Font.Bold = True
                Dim _periodStr As String
                If DatePart(DateInterval.WeekOfYear, Date.FromOADate(_bookingType.Main.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) = DatePart(DateInterval.WeekOfYear, Date.FromOADate(_bookingType.Main.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) Then
                    _periodStr = _langIni.Text("Spotlist", "Week") & " " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(_bookingType.Main.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & ", " & Year(Date.FromOADate(_bookingType.Main.StartDate))
                Else
                    _periodStr = _langIni.Text("Spotlist", "Week") & " " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(_bookingType.Main.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & "-" & DatePart(DateInterval.WeekOfYear, Date.FromOADate(_bookingType.Main.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & ", " & Year(Date.FromOADate(_bookingType.Main.StartDate))
                End If
                .Cells(15, 1).Value = _langIni.Text("Booking", "CampaignPeriod")
                .Cells(15, 3).Value = _periodStr
                .Cells(17, 1).Value = _langIni.Text("Booking", "BookingPeriod")
                .Cells(17, 7).Value = _langIni.Text("Booking", "Ordernumber")
                .Cells(16, 7).Value = "Channel reference:"
                .Cells(16, 9).Value = _bookingType.ContractNumber
                .Cells(16, 7).Font.Bold = True
                .Cells(17, 7).Font.Bold = True

                'Create disclaimer
                If BookingDisclaimer <> "" Then
                    With .Range("G18:K21")
                        .Value = BookingDisclaimer
                        .Merge()
                        .Font.Size = 8
                        .WrapText = True
                    End With
                End If

                .Cells(17, 9).Value = _bookingType.OrderNumber

                'Create panel for channel reference/order number
                CreatePanel(.Range("G16:I17"), 2, True)

                'if Bookingcode is wanted we print the markers for it
                If _bookingType.PrintBookingCode Then
                    CreatePanel(.Range("G15:I15"), True, True)
                    .Cells(15, 7).Value = _langIni.Text("Booking", "Bookingcode")
                    .Cells(15, 7).Font.Bold = True
                End If

                .Cells(19, 1).Value = _langIni.Text("Booking", "Target")
                .Cells(19, 3).Value = GetNiceTarget() & " (" & Format(_bookingType.BuyingTarget.getUniSizeUni(_bookingType.Weeks(CInt(_bookingType.Weeks.Count)).StartDate) * 1000, "N0") & ")"

                .Cells(21, 3).Value = _langIni.Text("Booking", "CPP30")
                .Cells(21, 4).Value = _langIni.Text("Booking", "CPT")
                .Cells(22, 1).Value = _langIni.Text("Booking", "Gross")

                Dim _idx As Decimal = 1

                For Each TmpIndex As Trinity.cIndex In _bookingType.Indexes
                    If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
                        If (TmpIndex.FromDate.ToOADate <= _bookingType.Weeks(1).StartDate And TmpIndex.ToDate.ToOADate >= _bookingType.Weeks(1).StartDate) Or (TmpIndex.FromDate.ToOADate <= _bookingType.Weeks(1).EndDate And TmpIndex.ToDate.ToOADate >= _bookingType.Weeks(1).EndDate) Then
                            _idx *= (TmpIndex.Index / 100)
                        End If
                    End If
                Next
                If _bookingType.Weeks(1).SpotIndex(True) > 0 Then
                    _idx *= _bookingType.Weeks(1).SpotIndex(True) / 100
                End If

                Dim _grossCPP As Double = 0
                Dim _grossCPT As Double = 0
                Dim _discount As Double = 0
                Dim _netCPT30 As Double = 0
                Dim _netCPP30 As Double = 0

                If _combination Is Nothing Then
                    _grossCPP = _bookingType.BuyingTarget.GetCPPForDate(_bookingType.Main.StartDate)
                    _grossCPT = (_bookingType.BuyingTarget.GetCPPForDate(_bookingType.Main.StartDate) / _bookingType.BuyingTarget.getUniSizeUni(0)) * 100
                    _netCPP30 = _bookingType.BuyingTarget.NetCPP
                    _netCPT30 = (_bookingType.BuyingTarget.NetCPP / _bookingType.BuyingTarget.getUniSizeUni(0)) * 100
                    _discount = _bookingType.BuyingTarget.Discount
                Else
                    For Each _cc As Trinity.cCombinationChannel In _combination.Relations
                        Dim _totalGrossCPP As Single = 0
                        If _cc.Bookingtype.BuyingTarget.CalcCPP Then
                            For _dp As Integer = 0 To _cc.Bookingtype.Dayparts.Count - 1
                                _grossCPP += _cc.Bookingtype.BuyingTarget.GetCPPForDate(_bookingType.Main.StartDate, _dp) * (_cc.Bookingtype.Dayparts(_dp).Share / 100) * _cc.Percent(True)
                                _totalGrossCPP += _cc.Bookingtype.BuyingTarget.GetCPPForDate(_bookingType.Main.StartDate, _dp) * (_cc.Bookingtype.Dayparts(_dp).Share / 100)
                            Next
                        Else
                            _grossCPP += _cc.Bookingtype.BuyingTarget.GetCPPForDate(_bookingType.Main.StartDate) * _cc.Percent(True)
                            _totalGrossCPP += _cc.Bookingtype.BuyingTarget.GetCPPForDate(_bookingType.Main.StartDate)
                        End If
                        _grossCPT += (_totalGrossCPP / _cc.Bookingtype.BuyingTarget.getUniSizeUni(0)) * 100 * _cc.Percent(True)
                        _discount += _cc.Bookingtype.BuyingTarget.Discount * _cc.Percent(True)
                        _netCPT30 += (_cc.Bookingtype.BuyingTarget.NetCPP / _cc.Bookingtype.BuyingTarget.getUniSizeUni(0)) * 100 * _cc.Percent(True)
                        _netCPP30 += _cc.Bookingtype.BuyingTarget.NetCPP * _cc.Percent(True)
                    Next
                End If
                .Cells(22, 3).Value = Format(_grossCPP, "0.0")
                .Cells(23, 1).Value = _langIni.Text("Booking", "Discount")
                .Cells(23, 3).Value = _discount
                .Cells(23, 4).Value = _discount
                .Cells(22, 4).Value = _grossCPT
                .Cells(24, 1).Value = _langIni.Text("Booking", "Net")
                .Cells(24, 3).Value = _netCPP30
                .Cells(24, 4).Value = _netCPT30
                .Cells(21, 4).Font.Color = HeadlineColor.ToArgb
                .Cells(22, 4).Numberformat = "##,##0.0"
                .Cells(24, 4).Numberformat = "##,##0.0"
                .Cells(22, 3).Numberformat = "##,##0.0"
                .Cells(24, 3).Numberformat = "##,##0.0"
                .Cells(21, 3).Font.Bold = True
                .Cells(21, 4).Font.Bold = True
                .Cells(22, 1).Font.Bold = True
                .Cells(23, 1).Font.Bold = True
                .Cells(24, 1).Font.Bold = True
                .Cells(23, 3).Numberformat = "0.0%"
                .Cells(23, 4).Numberformat = "0.0%"
                .Range("C21:D24").HorizontalAlignment = -4108
            End If
        End With
    End Sub

    Sub PrintDayparts()
        If _bookingType.PrintDayparts Then
            With _sheet
                With CreatePanel(.Range("B26:" & Chr(65 + _bookingType.Dayparts.Count) & "26"), 2, True, -4108)
                    .Merge()
                    .Font.Bold = True
                End With

                CreatePanel(.Range("B26:" & Chr(65 + _bookingType.Dayparts.Count) & "28"), 2, False, -4108)

                .Cells(26, 2).Value = _langIni.Text("Booking", "DaypartSplit")
                For _dp As Integer = 0 To _bookingType.Dayparts.Count - 1
                    If _langIni.Text("Booking", _bookingType.Dayparts(_dp).Name) <> "" Then
                        .Cells(27, 2 + _dp).Value = _langIni.Text("Booking", _bookingType.Dayparts(_dp).Name)
                    Else
                        .Cells(27, 2 + _dp).Value = _bookingType.Dayparts(_dp).Name
                    End If
                Next
                For _dp As Integer = 2 To _bookingType.Dayparts.Count() + 1
                    .Cells(28, _dp).Value = _bookingType.Dayparts(_dp - 2).Share / 100
                    .Cells(28, _dp).Numberformat = "0%"
                    .Cells(28, _dp).Font.Color = HeadlineColor.ToArgb
                Next
            End With
        End If
    End Sub


    Function GetNiceTarget() As String
        Dim _targ As String = _bookingType.BuyingTarget.TargetName
        Dim _gender As String

        Select Case _targ.Substring(0, 1)
            Case "A" : _gender = _langIni.Text("Targets", "All") & " "
            Case "K" : _gender = _langIni.Text("Targets", "Women") & " "
            Case "M" : _gender = _langIni.Text("Targets", "Men") & " "
            Case "W" : _gender = _langIni.Text("Targets", "Women") & " "
            Case "0" To "9" : _gender = _langIni.Text("Targets", "All") & " "
            Case Else : _gender = ""
        End Select
        Return _gender & " " & _targ.Trim("A", "K", "M", "W").Trim()
    End Function

    Private Sub PrintColumnHeadlines()
        If _type = TypeEnum.Specifics Then
            With _sheet
                'The column headings. It is now 12 columns wide
                With CreatePanel(.Range("B20:M20"), True, True, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter)
                    .Font.Bold = True
                End With

                'This is where we should print the columns in the order of their column number in TrinitySettings
                'But we don't at the moment
                .Cells(20, 2).Value = _langIni.Text("Booking", "Date")
                .Cells(20, 3).Value = _langIni.Text("Booking", "Time")
                .Cells(20, 4).Value = _langIni.Text("Booking", "Week")
                .Cells(20, 5).Value = _langIni.Text("Booking", "Programme")
                .Cells(20, 6).Value = _langIni.Text("Booking", "Spotlength")
                .Cells(20, 7).Value = _langIni.Text("Booking", "Filmcode")
                .Cells(20, 8).Value = _langIni.Text("Booking", "Estimate")
                .Cells(20, 9).Value = _langIni.Text("Booking", "Gross")
                .Cells(20, 10).Value = _langIni.Text("Booking", "Discount")
                .Cells(20, 11).Value = _langIni.Text("Booking", "Net")
                .Cells(20, 12).Value = _langIni.Text("Booking", "Placement")
                .Cells(20, 13).Value = _langIni.Text("Booking", "Extra")

                With CreatePanel(.Range("B23:M23"), True, True, Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter)
                    .Font.Bold = True
                End With

                'Add sums
                .Cells(23, 2).Value = _langIni.Text("Booking", "Total")
                .Cells(23, 8).Value = "=SUM(H21:H22)"
                .Cells(23, 9).Value = "=SUM(I21:I22)"
                .Cells(23, 9).Numberformat = "$#,##0_);($#,##0)"
                .Cells(23, 10).Value = "=1-(K23/I23)"
                .Cells(23, 10).Numberformat = "0.00%"
                .Cells(23, 11).Value = "=SUM(K21:K22)"
                .Cells(23, 11).Numberformat = "$#,##0_);($#,##0)"
            End With
        Else

        End If
    End Sub

    Sub CleanupPage()
        With _sheet
            If _type = TypeEnum.Specifics Then
                .Columns(1).ColumnWidth = 3
                .Columns(2).ColumnWidth = 14.5
                .Columns(3).ColumnWidth = 12
                '.Columns(5).ColumnWidth = 22.29
                .Columns("E:E").EntireColumn.AutoFit()
                .Columns("G:G").EntireColumn.AutoFit()

                If _bookingType.ContractNumber = "" Then
                    .Rows(14).entirerow.delete()
                End If
                With .PageSetup
                    .Orientation = 2
                    .PrintArea = "$A$1:$M$" & _lastRow
                    .PrintTitleRows = "$19:$19"
                End With
            Else
                .Columns(1).ColumnWidth = 8
                .Columns(2).ColumnWidth = 9.57
                .Columns(3).ColumnWidth = 13.57
                .Columns(4).ColumnWidth = 9.29
                .Columns(5).ColumnWidth = 8.71
                .Range("F:K").ColumnWidth = 8.86
                .Range("L:L").ColumnWidth = 3.43

                With .Range("A3:L3")
                    .Merge()
                    .HorizontalAlignment = -4108
                    .Font.Bold = True
                    .Font.Size = 16
                    .Font.Color = HeadlineColor.ToArgb
                End With

                .Range("C7:C21").Font.Color = HeadlineColor.ToArgb
                .Cells(21, 5).Font.Color = HeadlineColor.ToArgb

                .Columns(7).AutoFit()
                .Columns(12).AutoFit()

                .Range("A" & _topRow + 13 + _premiumRows & ":L" & _topRow + 13 + _premiumRows).Merge()
                .Cells(_topRow + 13 + _premiumRows, 1).Interior.Color = 0
                .Cells(_topRow + 13 + _premiumRows, 1).Font.Color = RGB(255, 255, 255)
                .Cells(_topRow + 13 + _premiumRows, 1).Font.Italic = True
                .Cells(_topRow + 13 + _premiumRows, 1).Font.Bold = True
                .Cells(_topRow + 13 + _premiumRows, 1).Numberformat = "yyyy-mm-dd"
                .Cells(_topRow + 13 + _premiumRows, 1).HorizontalAlignment = -4108
                .Cells(_topRow + 13 + _premiumRows, 1).Value = Now
            End If
            'Remove page breaks
            _excel.Windows(_wb.Name).View = 2
            While .VPageBreaks.Count > 0
                .VPageBreaks(1).DragOff(Direction:=-4161, RegionIndex:=1)
            End While
            _excel.Windows(_wb.Name).View = 1
            _excel.Visible = True
        End With

    End Sub
    Sub PrintFilms()
        With _sheet
            With CreatePanel(.Range("G23:J23"), 2, True, -4108)
                .Font.Bold = True
            End With

            .Cells(23, 7).Value = _langIni.Text("Booking", "Filmcode")
            .Cells(23, 8).Value = _langIni.Text("Booking", "Spotlength")
            .Cells(23, 9).Value = _langIni.Text("Booking", "Share")
            .Cells(23, 10).Value = _langIni.Text("Booking", "TRP")
            .Cells(23, 7).HorizontalAlignment = -4108
            .Cells(23, 8).HorizontalAlignment = -4108
            .Cells(23, 9).HorizontalAlignment = -4108
            .Cells(23, 10).HorizontalAlignment = -4108

            Dim _filmTRP(_bookingType.Weeks(1).Films.Count)
            Dim _totTRP As Single = 0
            For _w As Integer = 1 To _bookingType.Weeks.Count
                If Not SkipWeeks.Contains(_bookingType.Weeks(_w).Name) Then
                    For _f As Integer = 1 To _bookingType.Weeks(_w).Films.Count
                        If _combination Is Nothing Then
                            _filmTRP(_f) += (_bookingType.Weeks(_w).TRPBuyingTarget * (_bookingType.Weeks(_w).Films(_f).Share / 100))
                            _totTRP += (_bookingType.Weeks(_w).TRPBuyingTarget * (_bookingType.Weeks(_w).Films(_f).Share / 100))
                        Else
                            For Each _cc As Trinity.cCombinationChannel In _combination.Relations
                                If Not _cc.Bookingtype.IsPremium Then
                                    _filmTRP(_f) += _cc.Bookingtype.Weeks(_w).TRPBuyingTarget * (_cc.Bookingtype.Weeks(_w).Films(_f).Share / 100)
                                    _totTRP += _cc.Bookingtype.Weeks(_w).TRPBuyingTarget * (_cc.Bookingtype.Weeks(_w).Films(_f).Share / 100)
                                End If
                            Next
                        End If
                    Next
                End If
            Next
            Dim _r As Integer = 1
            For _f As Integer = 1 To _bookingType.Weeks(1).Films.Count
                If _filmTRP(_f) > 0 Then
                    .Cells(24 + _r, 6).Value = _r
                    .Cells(24 + _r, 6).Font.Color = HeadlineColor.ToArgb
                    .Cells(24 + _r, 6).HorizontalAlignment = -4152
                    .Cells(24 + _r, 7).Numberformat = "@"
                    If _bookingType.Weeks(1).Films(_f).Filmcode <> "" Then
                        .Cells(24 + _r, 7).Value = _bookingType.Weeks(1).Films(_f).Filmcode
                    Else
                        .Cells(24 + _r, 7).Value = _bookingType.Weeks(1).Films(_f).Name
                    End If
                    .Cells(24 + _r, 7).Font.Color = HeadlineColor.ToArgb
                    .Cells(24 + _r, 7).Font.Bold = True
                    .Cells(24 + _r, 8).Value = _bookingType.Weeks(1).Films(_f).FilmLength
                    .Cells(24 + _r, 8).Font.Color = HeadlineColor.ToArgb
                    If _totTRP > 0 Then
                        .Cells(24 + _r, 9).Value = _filmTRP(_f) / _totTRP
                    Else
                        .Cells(24 + _r, 9).Value = 0
                    End If
                    .Cells(24 + _r, 9).Numberformat = "0%"
                    .Cells(24 + _r, 9).Font.Color = HeadlineColor.ToArgb
                    .Cells(24 + _r, 10).Value = _filmTRP(_f)
                    .Cells(24 + _r, 10).Numberformat = "##,##0.0"
                    .Cells(24 + _r, 10).Font.Color = HeadlineColor.ToArgb
                    _r += 1
                End If
            Next
            CreatePanel(.Range("G23:J" & 24 + _r), 2, False, -4108)
            _topRow = 26 + _r
        End With
    End Sub

    Sub InsertLogo(TopRow As Integer, RightColumn As Integer, Optional ScaleRow As Boolean = False)
        If LogoPath <> "" AndAlso My.Computer.FileSystem.FileExists(LogoPath) Then
            With _sheet
                .InsertPicture(LogoPath)
                Dim _scale As Single = 180 / .Pictures(1).Width
                _scale = 1
                If _scale < 1 Then
                    .Pictures(1).ScaleWidth(_scale, 0, 0)
                    .Pictures(1).ScaleHeight(_scale, 0, 0)
                End If
                If ScaleRow Then
                    .Rows(TopRow).RowHeight = .Pictures(1).Height + 10
                    .Pictures(1).Top = 10
                Else
                    .Pictures(1).Top = .Rows(TopRow).Top
                End If

                If _bookingType.IsRBS Then
                    '.Rows(1).RowHeight = .pictures(1).Height + 10
                    Select Case LogoAlignment
                        Case LogoAlignmentEnum.Center
                            .Pictures(1).Left = .Columns(RightColumn).Left / 2 - .Pictures(1).Width / 2
                        Case LogoAlignmentEnum.Right
                            .Pictures(1).Left = .Columns(RightColumn).Left - .Pictures(1).Width - 10
                        Case LogoAlignmentEnum.Left
                            .Pictures(1).Left = 10
                    End Select
                Else
                    .Pictures(1).Left = .Columns(RightColumn).Left - .Pictures(1).Width - 10
                End If
            End With
        End If
    End Sub

    Sub PrintSpots()
        Dim _row As Integer = 22
        With _sheet
            For Each _s As Trinity.cBookedSpot In (From _spot As Trinity.cBookedSpot In _bookingType.Main.BookedSpots Select _spot Where _spot.Bookingtype Is _bookingType AndAlso (Not _spot.week Is Nothing AndAlso Not SkipWeeks.Contains(_spot.week.Name)))
                .Rows(_row).EntireRow.Insert()
                .Cells(_row, 2).Value = Trinity.Helper.FormatDateForBooking(_s.AirDate)
                .Cells(_row, 3).Value = Trinity.Helper.Mam2Tid(_s.MaM)
                .Cells(_row, 4).Value = DatePart(DateInterval.WeekOfYear, _s.AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                .Cells(_row, 5).Value = _s.Programme
                .Cells(_row, 6).Value = _s.Film.FilmLength
                If _s.Filmcode <> "" Then
                    .Cells(_row, 7).Value = _s.Filmcode
                Else
                    .Cells(_row, 7).Value = _s.Film.Name
                End If
                .Cells(_row, 8).Value = _s.ChannelEstimate
                .Cells(_row, 9).Numberformat = "#,##0 $"
                .Cells(_row, 9).Value = _s.GrossPrice * _s.AddedValueIndex(False)
                .Cells(_row, 10).Value = _s.Bookingtype.BuyingTarget.Discount
                .Cells(_row, 10).Numberformat = "0.00%"
                .Cells(_row, 11).Value = (_s.NetPrice * _s.AddedValueIndex)
                .Cells(_row, 11).Numberformat = "#,##0 $"

                If _s.AddedValues.Count > 0 Then
                    .Cells(_row, 12).Value = String.Join("/", _s.AddedValues.Values.Select(Function(av As Trinity.cAddedValue) av.Name).ToArray)
                    .Cells(_row, 13).Value = _s.AddedValueIndex - 1
                End If
                .Cells(_row, 13).Numberformat = "0.00%"
                .Rows(_row).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                _row += 1
            Next
            .Rows(_row).EntireRow.Delete()
            .Rows(21).EntireRow.Delete()
            .ClearSortFields()
            .AddSortField(.Range("B20:B" & _row - 2), Excel.XlSortOn.xlSortOnValues, Excel.XlSortOrder.xlAscending, Excel.XlSortDataOption.xlSortNormal)
            .AddSortField(.Range("C20:C" & _row - 2), Excel.XlSortOn.xlSortOnValues, Excel.XlSortOrder.xlAscending, Excel.XlSortDataOption.xlSortNormal)
            .Sort(.Range("B20:M" & _row), True, False, 1, Excel.XlSortMethod.xlPinYin)
            .Range("B20:M" & _row).Sort(Key1:=.Range("B21"), Order1:=1, Key2:=.Range("C21"), Order2:=1, Header:=0, OrderCustom:=1, MatchCase:=False, Orientation:=1, DataOption1:=0, DataOption2:=0)
            _row += 1
        End With
        _lastRow = _row
    End Sub

    Protected Function CreatePanel(ByRef Area As Range, BorderWeight As Integer, Panel As Boolean, Optional HorizontalAlignment As Integer = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft) As Range
        With Area
            If Panel Then
                .Interior.Color = PanelColor.ToArgb
                .Font.Color = PanelTextColor.ToArgb
            End If
            If BorderWeight <> 0 Then
                For _x As Integer = 7 To 10
                    With .Borders(_x)
                        .LineStyle = 1
                        .Weight = BorderWeight
                        .ColorIndex = -4105
                    End With
                Next
            End If
            If HorizontalAlignment <> 0 Then .HorizontalAlignment = HorizontalAlignment
        End With
        Return Area
    End Function

    Protected Function CreatePanel(ByRef Area As Range, ByVal Borders As Boolean, ByVal Panel As Boolean, Optional ByVal HorizontalAlignment As Integer = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft) As Range
        If Borders Then
            Return CreatePanel(Area, -4138, Panel, HorizontalAlignment)
        Else
            Return CreatePanel(Area, 0, Panel, HorizontalAlignment)
        End If
    End Function

    Protected Function CheckFilms() As Boolean
        Return (From _spot As Trinity.cBookedSpot In _bookingType.Main.BookedSpots() Select _spot Where _spot.Bookingtype Is _bookingType AndAlso _spot.Film Is Nothing).Count = 0
    End Function

    Protected Sub SetColors()
        _wb.Colors(17) = HeadlineColor.ToArgb
        _wb.Colors(18) = PanelColor.ToArgb
        _wb.Colors(19) = PanelTextColor.ToArgb
    End Sub

    Protected Sub PreparePage()
        If FontName <> "" Then _sheet.AllCells.Font.Name = FontName
        _sheet.AllCells.Font.Size = 9
        _sheet.AllCells.Interior.Color = RGB(255, 255, 255)
    End Sub

    Protected Sub PrepareEnvironment()
        _topRow = 0
        _premiumRows = 0
        If _excel IsNot Nothing Then
            _excel.ScreenUpdating = False
            _excel.DisplayAlerts = False
        End If
    End Sub

    Protected Sub ResetEnvironment()
        If _excel IsNot Nothing Then
            _excel.ScreenUpdating = True
            _excel.DisplayAlerts = True
        End If
    End Sub


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).

            End If
            _excel.Quit()
            _excel = Nothing
            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
