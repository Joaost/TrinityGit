Imports Microsoft.Office.Interop
Imports System.IO
Imports System.Collections
Imports System.Data

Public Class frmPremiums
    Implements System.Collections.IEnumerable

    Dim mCol As New Collection

    Public DayDictionary As New Dictionary(Of String, Integer)

    'Just takes an integer 1-7 and returns a 3-character string for the day of week
    Public Function getday(ByVal dayno As Integer) As String
        Select Case dayno
            Case 1
                Return "Man"
            Case 2
                Return "Tir"
            Case 3
                Return "Ons"
            Case 4
                Return "Tor"
            Case 5
                Return "Fre"
            Case 6
                Return "Lør"
            Case 7
                Return "Søn"
        End Select
    End Function


    Dim oldCI As System.Globalization.CultureInfo


    Public Sub add(ByVal o As Object)
        mCol.Add(o)
    End Sub

    'Takes the DataGridView grdViewHoldsPremiums and creates a list of proper spots as a collection of EnhancementSpot
    Public Function UnfoldPremiumSpots(ByVal foldedCollection As DataGridView) As Collection

        Dim _mCol As New Collection


        For Each spotInfo As DataGridViewRow In foldedCollection.Rows

            Dim spotToInsert As EnhancementSpot

            Dim Weeks() As String
            Dim Days As New List(Of String)

            If Strings.Split(spotInfo.Cells(2).Value.ToString, ",").Length = 1 Then
                Dim tmp(0) As String
                tmp(0) = spotInfo.Cells(2).Value
                Weeks = tmp
            Else
                Weeks = Strings.Split(spotInfo.Cells(2).Value.ToString, ",")
            End If

            'It goes through columns 4 to 10 in the GridView because thats where the days are. If a value in one of those cells is true, it checks which days it represents
            'and adds that day to this spotlist
            For i As Integer = 4 To 10
                If spotInfo.Cells(i).Value = True Then
                    Days.Add(getday(i - 3))
                End If
            Next

            Dim spotsgeneratedbyrow As Integer = Weeks.Length * Days.Count


            For Each a As String In Weeks

                For Each d As String In Days

                    spotToInsert = New EnhancementSpot
                    spotToInsert.channel = spotInfo.Cells(0).Value
                    spotToInsert.title = spotInfo.Cells(1).Value

                    spotToInsert.week = a
                    spotToInsert.airdate = GetDateFromWeekAndDay(a, d)
                    'MessageBox.Show(spotToInsert.airdate.ToString)
                    spotToInsert.StartMam = CType(spotInfo.Cells(3).Value, Date).Minute + CType(spotInfo.Cells(3).Value, Date).Hour * 60
                    spotToInsert.day = d
                    spotToInsert.time = CType(spotInfo.Cells(3).Value, Date).ToShortTimeString
                    spotToInsert.channel = spotInfo.Cells(0).Value
                    spotToInsert.grossPrice = spotInfo.Cells(11).Value
                    'spotToInsert.grossPriceFirst = spotInfo.grossPriceFirst
                    'spotToInsert.grossPriceSecondOrLast = spotInfo.grossPriceSecondOrLast
                    'spotToInsert.grossPriceThird = spotInfo.grossPriceThird
                    spotToInsert.comment = spotInfo.Cells(12).Value
                    _mCol.Add(spotToInsert)
                Next

            Next
        Next
        Return _mCol

    End Function

    Function MakeWeekRange(ByVal weekRange As List(Of String)) As List(Of String)
        Dim largest As Integer = CType(weekRange(0), Integer)
        Dim smallest As Integer = CType(weekRange(0), Integer)
        Dim range As New List(Of String)

        For Each i As String In weekRange
            If CType(i, Integer) > largest Then
                largest = i
            End If
            If CType(i, Integer) < smallest Then
                smallest = i
            End If
        Next

        For i As Integer = smallest To largest
            range.Add(CType(i, String))
        Next
        Return range

    End Function

    Function MakeDayRange(ByVal dayRange As List(Of String)) As List(Of String)
        ' Fungerar inte om programmet går till exempel sön -> tisdag just nu

        Dim range As New List(Of String)

        Dim firstDay As String = Strings.Left(dayRange(0), 3)
        Dim lastDay As String = firstDay

        For Each i As String In dayRange
            If DayDictionary(Strings.Left(i, 3)) > DayDictionary(firstDay) Then
                lastDay = Strings.Left(i, 3)
            End If
        Next

        For i As Integer = DayDictionary(firstDay) + 1 To DayDictionary(lastDay) + 1
            range.Add(getday(i))
        Next

        Return range

    End Function

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return mCol.GetEnumerator()
    End Function

    Public Function GetDateFromWeekAndDay(ByVal WeekNumber As String, ByVal WeekDay As String, Optional ByVal Year As Integer = 2008) As Date

        Dim firstDayOfFirstWeekOfYear As New Date

        'DayDictionary.Add(0, "Man")
        'DayDictionary.Add(1, "Tir")
        'DayDictionary.Add(2, "Ons")
        'DayDictionary.Add(3, "Tor")
        'DayDictionary.Add(4, "Fre")
        'DayDictionary.Add(5, "Lør")
        'DayDictionary.Add(6, "Søn")

        firstDayOfFirstWeekOfYear = DateSerial(Year, 1, 1)

        Dim correct As Integer = DateAndTime.Weekday(firstDayOfFirstWeekOfYear)

        Dim properDate As New Date

        Dim Week As Integer = CType(WeekNumber, Integer)
        Dim whateva As String = Strings.Left(WeekDay, 3)
        Dim Day As Integer = DayDictionary(Strings.Left(WeekDay, 3)) + 1
        Dim DaysToAdd As Double = Week * 7 + Day - correct + 1


        properDate = firstDayOfFirstWeekOfYear.AddDays(DaysToAdd)

        Return properDate

    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '      For Each tmpRow As DataGridViewRow In DataGridView1.SelectedRows
        'DataGridView1.Rows.Remove(tmpRow)
        '   Next
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportPremiumsFile.Click

        Dim tmpSpot As New EnhancementSpot 'Will hold a row of data to be put into the datagridview

        Dim openDlg = New OpenFileDialog 'Pick which file containing enhancement spots to open

        Dim xlApp As Excel.Application 'Define as a new Excel application object
        Dim xlWorkBook As Excel.Workbook 'Define as a new Excel workbook object
        Dim xlWorkSheet As Excel.Worksheet 'Define as a new Excel worksheet object

        openDlg.Showdialog() 'Pick the file
        oldCI = System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")

        xlApp = New Excel.ApplicationClass 'Point xlApp to a new application object
        xlWorkBook = xlApp.Workbooks.Open(openDlg.Filename) 'Make this application open the workbook we picked
        xlWorkSheet = xlWorkBook.Worksheets("sheet1") 'Set the active worksheet to the first one

        'Now find the first row where spot info is contained

        Dim firstRow As Integer = 0

        Dim currentRow As Integer = 3

        While tmpSpot.title <> "Program"
            firstRow += 1
            If Not IsNothing(xlWorkSheet.Cells(firstRow, 1).Value) Then
                tmpSpot.title = xlWorkSheet.Cells(firstRow, 1).Value.ToString
            End If
        End While

        firstRow += 1

        Dim lastRow As Integer = firstRow

        While tmpSpot.title <> Nothing
            lastRow += 1
            If Not IsNothing(xlWorkSheet.Cells(lastRow, 1).Value) Then
                tmpSpot.title = xlWorkSheet.Cells(lastRow, 1).Value.ToString
            Else
                tmpSpot.title = Nothing
            End If

        End While

        lastRow -= 1

        For index As Integer = firstRow To lastRow
            With xlWorkSheet
                tmpSpot = New EnhancementSpot
                tmpSpot.title = .Cells(index, 1).Value.ToString '   .Range("A4").Value

                For Each i As String In Split(.Cells(index, 2).Value.ToString, "-")
                    tmpSpot.weeks.Add(i)
                Next

                tmpSpot.weeks = MakeWeekRange(tmpSpot.weeks)

                For Each i As String In Split(.Cells(index, 3).Value.ToString, "-")
                    tmpSpot.days.Add(i)
                Next

                tmpSpot.days = MakeDayRange(tmpSpot.days)

                tmpSpot.time = CType(Strings.Right(.Cells(index, 4).Value.ToString, 5), Date).ToShortTimeString
                tmpSpot.channel = .Cells(index, 5).Value.ToString
                tmpSpot.grossPrice = .Cells(index, 6).Value
                tmpSpot.grossPriceFirst = .Cells(index, 7).Value
                tmpSpot.grossPriceSecondOrLast = .Cells(index, 8).Value
                tmpSpot.grossPriceThird = .Cells(index, 9).Value
                tmpSpot.comment = .Cells(index, 10).Value.ToString
            End With
            mCol.Add(tmpSpot)
            tmpSpot = Nothing
        Next

        UpdatePremiumsGridView(mCol)

        oldCI = System.Threading.Thread.CurrentThread.CurrentCulture

        xlWorkBook.Close()
        xlApp.Quit()

    End Sub

    Public Sub insertIntoDB(ByVal spotsToInsert As Collection)

        For Each spot As EnhancementSpot In spotsToInsert
            Dim query As String = "INSERT INTO events (id,channel,[Date],[time],Startmam,Duration,Name,ChanEst,UseCPP,Addition,EstimationTarget,Price,EstimationPeriod,Comment,Area,Type) VALUES ('" _
                           & CreateGUID() _
                           & "','" _
                           & spot.channel _
                           & "'," _
                           & spot.airdate.ToOADate _
                           & ",'" _
                           & spot.time.ToString _
                           & "'," _
                           & spot.StartMam _
                           & "," _
                           & spot.Duration _
                           & ",'" _
                           & Replace(spot.title, "'", "''") _
                           & "','" _
                           & 0 _
                           & "',0," _
                           & 0 _
                           & "," _
                           & 0 _
                           & "," _
                           & spot.grossPrice _
                           & ",'" _
                           & spot.EstimationPeriod _
                           & "','" _
                           & spot.comment _
                           & "','" _
                           & campaign.Area _
                           & "'," _
                           & 1 _
                           & ")"
            '       MessageBox.Show(query)
            DBReader.QUERY(query)
        Next

    End Sub

    Private Sub frmPremiums_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'check what DB is supposed to be used
        TrinitySettings = New Trinity.cSettings(Trinity.Helper.GetSpecialFolder(Trinity.Helper.CSIDLEnum.UserProfile) & "Trinity 4.0")

        campaign = New Trinity.cKampanj
        TrinitySettings.MainObject = campaign
        campaign.CreateChannels()

        Dim s As String
        s = TrinitySettings.DataBase(Trinity.cSettings.SettingsLocationEnum.locNetwork)
        If s.Substring(s.Length - 3, 3).ToUpper = "MDB" Then
            DBReader = New Trinity.cDBReaderAccess
        ElseIf TrinitySettings.DataBase(Trinity.cSettings.SettingsLocationEnum.locNetwork).ToUpper = "SQL" Then
            DBReader = New Trinity.cDBReaderSQL
        Else
            Stop 'no other types available
        End If

        'connect the handler to the database
        DBReader.Connect(Trinity.cDBReader.ConnectionPlace.Network)

        If Not DBReader.alive Then
            MsgBox("Could not connect to database", MsgBoxStyle.Critical, "Error connecting")
            Exit Sub
        End If

        For Each tmpChan As Trinity.cChannel In campaign.Channels
            Channel.Items.Add(tmpChan.ChannelName)
        Next


        'Remove this line when rolling out
        Channel.Items.Add("TV3+")

        DayDictionary.Add("Man", 0)
        DayDictionary.Add("Tir", 1)
        DayDictionary.Add("Ons", 2)
        DayDictionary.Add("Tor", 3)
        DayDictionary.Add("Fre", 4)
        DayDictionary.Add("Lør", 5)
        DayDictionary.Add("Søn", 6)

    End Sub

    Public Sub UpdatePremiumsGridView(ByVal mCol As Collection)

        Dim spotRow As DataGridViewRow

        For Each spot As EnhancementSpot In mCol
            spotRow = grdViewHoldsPremiums.Rows(grdViewHoldsPremiums.Rows.Add())
            spotRow.Tag = spot

        Next

    End Sub

    'This handles the delete button on a row being clicked on
    Private Sub grdViewHoldsPremiums_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdViewHoldsPremiums.CellClick
        If e.ColumnIndex = 13 Then
            grdViewHoldsPremiums.Rows.Remove(grdViewHoldsPremiums.CurrentRow)
        End If
    End Sub

    'This function does what it should right now. Gets its spot data from the Tag property of the row itself.
    Private Sub grdViewHoldsPremiums_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdViewHoldsPremiums.CellValueNeeded

        Dim spot As EnhancementSpot
        spot = grdViewHoldsPremiums.Rows(e.RowIndex).Tag

        If spot Is Nothing Then
            Exit Sub
        End If

        Dim s As String = Nothing

        Select Case e.ColumnIndex
            Case 0
                e.Value = spot.channel
            Case 1
                e.Value = spot.title
            Case 2
                e.Value = String.Join(",", spot.weeks.ToArray)
            Case 3
                e.Value = spot.time.ToString("HH:mm")
            Case 4
                If spot.days.Count > 0 Then
                    For Each Day As String In MakeDayRange(spot.days)
                        If Day = "Man" Then
                            e.Value = True
                        End If
                    Next
                End If
            Case 5
                If spot.days.Count > 0 Then
                    For Each Day As String In MakeDayRange(spot.days)
                        If Day = "Tir" Then
                            e.Value = True
                        End If
                    Next
                End If
            Case 6
                If spot.days.Count > 0 Then
                    For Each Day As String In MakeDayRange(spot.days)
                        If Day = "Ons" Then
                            e.Value = True
                        End If
                    Next
                End If
            Case 7
                If spot.days.Count > 0 Then
                    For Each Day As String In MakeDayRange(spot.days)
                        If Day = "Tor" Then
                            e.Value = True
                        End If
                    Next
                End If
            Case 8
                If spot.days.Count > 0 Then
                    For Each Day As String In MakeDayRange(spot.days)
                        If Day = "Fre" Then
                            e.Value = True
                        End If
                    Next
                End If
            Case 9
                If spot.days.Count > 0 Then
                    For Each Day As String In MakeDayRange(spot.days)
                        If Day = "Lør" Then
                            e.Value = True
                        End If
                    Next
                End If
            Case 10
                If spot.days.Count > 0 Then
                    For Each Day As String In MakeDayRange(spot.days)
                        If Day = "Søn" Then
                            e.Value = True
                        End If
                    Next
                End If
            Case 11
                e.Value = spot.grossPrice
            Case 12
                e.Value = spot.comment
        End Select

    End Sub

    'Should make a new spot, change what was pushed and then set the Tag of the row again
    Private Sub grdViewHoldsPremiums_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdViewHoldsPremiums.CellValuePushed


        Dim spot As New EnhancementSpot

        If grdViewHoldsPremiums.Rows(e.RowIndex).Tag Is Nothing Then
            grdViewHoldsPremiums.Rows(e.RowIndex).Tag = spot
        End If
        spot = grdViewHoldsPremiums.Rows(e.RowIndex).Tag
        'If spot Is Nothing Then
        'Exit Sub
        'End If

        Select Case e.ColumnIndex
            Case 0
                spot.channel = e.Value
            Case 1
                spot.title = e.Value
            Case 2
                spot.weeks.Clear()
                For Each s As String In Strings.Split(e.Value.ToString, ",")
                    spot.weeks.Add(s)
                Next
            Case 3
                spot.time = e.Value
            Case 4
                If True Then
                    spot.days.Add("Man")
                Else
                    spot.days.Remove("Man")
                End If
            Case 5
                If True Then
                    spot.days.Add("Tir")
                Else
                    spot.days.Remove("Tir")
                End If
            Case 6
                If True Then
                    spot.days.Add("Ons")
                Else
                    spot.days.Remove("Ons")
                End If
            Case 7
                If True Then
                    spot.days.Add("Tor")
                Else
                    spot.days.Remove("Tor")
                End If
            Case 8
                If True Then
                    spot.days.Add("Fre")
                Else
                    spot.days.Remove("Fre")
                End If
            Case 9
                If True Then
                    spot.days.Add("Lør")
                Else
                    spot.days.Remove("Lør")
                End If
            Case 10
                If True Then
                    spot.days.Add("Søn")
                Else
                    spot.days.Remove("Søn")
                End If
            Case 11
                spot.grossPrice = e.Value
            Case 12
                spot.comment = e.Value
                '    spot.time = e.Value
                'Case 5
                '    spot.channel = e.Value
                'Case 6
                '    spot.grossPrice = e.Value
                'Case 7
                '    spot.comment = e.Value
        End Select

        grdViewHoldsPremiums.Rows(e.RowIndex).Tag = spot

    End Sub

    Private Sub btnExportToDatabase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToDatabase.Click

        'Unfoldpremiumspots takes the information residing in the datagridview grdViewHoldsPremiums and turns it into a proper list of spots in the form of a collection of Enhancementspot
        insertIntoDB(UnfoldPremiumSpots(grdViewHoldsPremiums))
        MessageBox.Show("Spots now bookable in Bookings")

    End Sub

    Private Sub btnAddRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow.Click
        ' Dim newSpot As New EnhancementSpot("Edit")
        Dim newRow As New DataGridViewRow
        grdViewHoldsPremiums.Rows.Add()
        'newRow.Tag = newSpot
        'grdViewHoldsPremiums.Rows.Add(newRow)
        'grdViewHoldsPremiums.Rows.Item(grdViewHoldsPremiums.Rows.Count).Tag = newSpot
        'mCol.Add(newSpot)

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        'Me = Nothing
        Me.Close()
    End Sub
End Class

Public Class EnhancementSpot

    Public ID As Guid
    Public StartMam As Integer
    Public Duration As Integer
    Public EstimationPeriod As String

    Public week As Integer
    Public day As String
    Public airdate As Date
    

    Public title As String '1
    Public weeks As New List(Of String) '2
    Public days As New List(Of String) '3
    Public time As Date '4
    Public channel As String '5
    Public grossPrice As Decimal '6
    Public grossPriceFirst As Decimal '7
    Public grossPriceSecondOrLast As Decimal '8
    Public grossPriceThird As Decimal '9
    Public comment As String '10

    Public Sub New()
        'Things we just assume rather than read from the file
        ID = New Guid()
        Duration = 30
        EstimationPeriod = "-4fw"
    End Sub

    Public Sub New(ByVal init As String)
        'Things we just assume rather than read from the file
        ID = New Guid()
        Duration = 30
        EstimationPeriod = "-4fw"
        title = init
    End Sub

End Class
