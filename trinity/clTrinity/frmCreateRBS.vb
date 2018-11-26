Imports System.Globalization.CultureInfo

Public Class frmCreateRBS

    Private Sub frmCreateRBS_Layout(ByVal sender As Object, ByVal e As System.Windows.Forms.LayoutEventArgs) Handles Me.Layout
        'Stop
    End Sub

    Private Sub frmCreateRBS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Sets today as default date in the date boxes
        dtFrom.Value = Date.FromOADate(Campaign.StartDate)
        dtTo.Value = Date.FromOADate(Campaign.EndDate)
        dtTo_ValueChanged(New Object, New System.EventArgs)
        getFilms()
        grdFilmsplit.ClearSelection()
        getChannels()
        grdDaypartSplit.Rows.Clear()
        grdDaypartSplit.Rows.Add()
        grdDaypartSplit.Rows(0).Cells(0).Value = "25"
        grdDaypartSplit.Rows(0).Cells(1).Value = "65"
        grdDaypartSplit.Rows(0).Cells(2).Value = "10"
    End Sub

    Private Sub getFilms()
        Dim tmpfilm As Trinity.cFilm
        grdFilmsplit.Rows.Clear()
        If Campaign.Channels(1).BookingTypes(1).Weeks.Count > 0 Then
            For Each tmpfilm In Campaign.Channels(1).BookingTypes(1).Weeks(1).Films
                grdFilmsplit.Rows.Add()
                grdFilmsplit.Rows(grdFilmsplit.Rows.Count - 1).HeaderCell.Value = tmpfilm.Name
                grdFilmsplit.RowHeadersWidth = 65
            Next
        End If
        grdFilmsplit.RowHeadersWidth = 100
    End Sub

    Private Sub getChannels()
        Dim tmpChan As Trinity.cChannel
        Dim tmpBT As Trinity.cBookingType

        cmbFilmChannel.Items.Clear()
        For Each tmpChan In Campaign.Channels
            For Each tmpBT In tmpChan.BookingTypes
                If tmpBT.BookIt = True And tmpBT.Name.Substring(0, 3) = "RBS" Then
                    cmbFilmChannel.Items.Add(tmpBT)
                End If
            Next
        Next
        If cmbFilmChannel.Items.Count > 0 Then
            cmbFilmChannel.SelectedIndex = 0
        End If

    End Sub

    Private Sub dtTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtTo.ValueChanged
        Dim d As Date
        Dim l As Long
        Dim i As Integer
        Dim StartDate As Date
        Dim EndDate As Date
        Dim a As Integer

        On Error GoTo dtNormalTo_Change_Error

        i = 0
        'clear and repopulate the week grid
        grdFilmsplit.Columns.Clear()
        For l = dtFrom.Value.ToOADate To Trinity.Helper.MondayOfWeek(Year(dtTo.Value), DatePart(DateInterval.WeekOfYear, dtTo.Value, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)).ToOADate + 6 Step 7
            grdFilmsplit.Columns.Add(i, i + CurrentCulture.DateTimeFormat.Calendar.GetWeekOfYear(dtFrom.Value.Date, CurrentCulture.DateTimeFormat.CalendarWeekRule, CurrentCulture.DateTimeFormat.FirstDayOfWeek))
            grdFilmsplit.Columns(i).Width = 45
            i += 1
        Next

        getFilms()

        On Error GoTo 0
        Exit Sub

dtNormalTo_Change_Error:

        If IsIDE() Then
            a = MsgBox("Error:" & Chr(10) & Chr(10) & "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description & Chr(10) & Chr(10) & "Vill du utföra en felsökning?", vbYesNo, "TRINITY")
            If a = vbNo Then Exit Sub
            Stop
            Resume Next
        End If
        MsgBox("Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description & " in dtTo_Change.", vbCritical, "Error")

    End Sub

    Private Sub dtFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtFrom.ValueChanged
        Dim d As Date
        Dim i As Integer
        Dim l As Long
        Dim StartDate As Date
        Dim EndDate As Date
        Dim a As Integer

        On Error GoTo dtNormalFrom_Change_Error

        dtTo.MinDate = dtFrom.Value

        i = 0

        'clear the grid and repopulate it according to the newly set date
        grdFilmsplit.Columns.Clear()
        For l = dtFrom.Value.ToOADate To dtTo.Value.ToOADate Step 7
            grdFilmsplit.Columns.Add(i, i + CurrentCulture.DateTimeFormat.Calendar.GetWeekOfYear(dtFrom.Value.Date, CurrentCulture.DateTimeFormat.CalendarWeekRule, CurrentCulture.DateTimeFormat.FirstDayOfWeek))
            grdFilmsplit.Columns(i).Width = 45
            i = i + 1
        Next

        getFilms()

        On Error GoTo 0
        Exit Sub

dtNormalFrom_Change_Error:

        If IsIDE() Then
            a = MsgBox("Error:" & Chr(10) & Chr(10) & "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description & Chr(10) & Chr(10) & "Vill du utföra en felsökning?", vbYesNo, "TRINITY")
            If a = vbNo Then Exit Sub
            Stop
            Resume Next
        End If
        MsgBox("Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description & " in dtNormalFrom_Change.", vbCritical, "Error")
    End Sub

    Private Sub grdFilmsplit_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdFilmsplit.CellFormatting
        If e.Value = Nothing Then
            e.Value = Format(0, "P0")
        Else
            Dim tmp As String = e.Value
            e.Value = Format(tmp.Trim("%") / 100, "P0")
        End If

    End Sub

    Private Sub grdDaypartsplit_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdDaypartSplit.CellFormatting
        If e.Value = Nothing Then
            e.Value = Format(0, "P0")
        Else
            Dim tmp As String = e.Value
            e.Value = Format(tmp.Trim("%") / 100, "P0")
        End If

    End Sub



    Sub ColorFilmGrid()
        'sets colors on the film grid depending on whether the sum is correct or not (adds to 100%)
        Dim i As Integer
        Dim j As Integer
        Dim Tot As Single
        For i = 0 To grdFilmsplit.Columns.Count - 1
            Tot = 0
            For j = 0 To grdFilmsplit.Rows.Count - 1
                Tot = Tot + grdFilmsplit.Rows(j).Cells(i).Value
            Next
            If Tot <> 100 Then 'if the films dont sum up make them red
                For j = 0 To grdFilmsplit.Rows.Count - 1
                    grdFilmsplit.Rows(j).Cells(i).Style.BackColor = Drawing.Color.Red
                Next
            Else
                For j = 0 To grdFilmsplit.Rows.Count - 1
                    grdFilmsplit.Rows(j).Cells(i).Style.BackColor = Drawing.Color.White
                Next
            End If
        Next

    End Sub



    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Saved = False

        'some input validation
        If txtTRP.Text = "" OrElse txtTRP.Text = 0 Then
            MsgBox("You must enter a TRP to be added.", MsgBoxStyle.Critical, "Bad input")
            Exit Sub
        End If

        Dim tc As Integer
        Dim tr As Integer
        For tr = 0 To grdFilmsplit.Rows.Count - 1
            For tc = 0 To grdFilmsplit.Columns.Count - 1 '
                If grdFilmsplit.Rows(tr).Cells(tc).Style.BackColor = Drawing.Color.Red Then
                    MsgBox("The sum of the film split must be 100% on all weeks.", MsgBoxStyle.Critical, "Bad input")
                    Exit Sub
                End If
            Next
        Next
        If grdDaypartSplit.Rows(0).Cells(0).Style.BackColor = Drawing.Color.Red Then
            MsgBox("The sum of the daypart split must be 100%.", MsgBoxStyle.Critical, "Bad input")
            Exit Sub
        End If

        If grdDaypartSplit.Rows(0).Cells(1).Style.BackColor = Drawing.Color.Red Then
            MsgBox("The sum of the daypart split must be 100%.", MsgBoxStyle.Critical, "Bad input")
            Exit Sub
        End If
        If grdDaypartSplit.Rows(0).Cells(2).Style.BackColor = Drawing.Color.Red Then
            MsgBox("The sum of the daypart split must be 100%.", MsgBoxStyle.Critical, "Bad input")
            Exit Sub
        End If

        Try
            Dim TRP As Double
            Dim days As Integer = dtTo.Value.DayOfYear - dtFrom.Value.DayOfYear + 1
            Dim dTRP As Double = txtTRP.Text / days

            Dim i As Integer
            Dim j As Integer
            Dim z As Integer
            Dim tmpDate As Date = dtFrom.Value.Date
            Dim w As Integer 'week number
            Dim tmpBT As Trinity.cBookingType = cmbFilmChannel.SelectedItem

            For dp As Integer = 0 To tmpBT.Dayparts.Count - 1
                For j = 0 To days - 1
                    TRP = 0
                    'get the "current" week so we can check the column in the film split grid
                    w = CurrentCulture.DateTimeFormat.Calendar.GetWeekOfYear(tmpDate, CurrentCulture.DateTimeFormat.CalendarWeekRule, CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                    'check the column in the ffilm split grid
                    For z = 0 To grdFilmsplit.Columns.Count - 1
                        If grdFilmsplit.Columns(z).HeaderText = w Then
                            w = z
                            Exit For
                        End If
                    Next
                    'get the film split value and create the dummy spot
                    For i = 0 To grdFilmsplit.Rows.Count - 1
                        If grdFilmsplit.Rows(0).Cells(0).Value <> 0% Then
                            TRP = grdFilmsplit.Rows(i).Cells(w).Value / 100
                            TRP *= ((grdDaypartSplit.Rows(0).Cells(0).Value / 100) * dTRP)
                            'add spot if it has TRPs
                            If TRP <> 0 Then
                                Dim tmpSpot As Trinity.cPlannedSpot
                                tmpSpot = Campaign.PlannedSpots.Add
                                Dim tmpchan As Trinity.cChannel
                                Dim tmpbtCheck As Trinity.cBookingType
                                For Each tmpchan In Campaign.Channels
                                    For Each tmpbtCheck In tmpchan.BookingTypes
                                        If tmpBT.ToString = tmpbtCheck.ToString Then
                                            tmpSpot.Channel = tmpchan
                                        End If
                                    Next
                                Next

                                tmpSpot.MyRating = TRP
                                tmpSpot.Programme = "RBS - CREATED SPOT"
                                tmpSpot.Bookingtype = cmbFilmChannel.SelectedItem
                                tmpSpot.Filmcode = grdFilmsplit.Rows(i).HeaderCell.Value
                                tmpSpot.SpotLength = 15
                                tmpSpot.Week = tmpSpot.Bookingtype.GetWeek(tmpDate)
                                tmpSpot.MaM = CInt(tmpBT.Dayparts(dp).StartMaM + 2)
                                tmpSpot.AirDate = tmpDate.ToOADate
                                If radBT.Checked Then
                                    tmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = TRP
                                Else
                                    tmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = TRP
                                End If
                            End If
                        End If
                    Next
                    'count day up one step
                    tmpDate = tmpDate.AddDays(1)
                Next
            Next

            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch
            MsgBox("Some input are not correct.", MsgBoxStyle.Critical, "Bad input")
        End Try
    End Sub

    Private Sub cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub txtTRP_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTRP.KeyPress
        If (Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57) And Asc(e.KeyChar) <> 44 And Asc(e.KeyChar) <> 8 Then
            e.Handled = True
        End If
    End Sub

    Private Sub grdDaypartSplit_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdDaypartSplit.CellValueChanged
        Dim Tot As Single
        Dim tmpStr As String
        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        Try
            Tot = 0
            tmpStr = grdDaypartSplit.Rows(0).Cells(0).Value
            Tot = Tot + tmpStr.Trim("%")
            tmpStr = grdDaypartSplit.Rows(0).Cells(1).Value
            Tot = Tot + tmpStr.Trim("%")
            tmpStr = grdDaypartSplit.Rows(0).Cells(2).Value
            Tot = Tot + tmpStr.Trim("%")

            If Tot <> 100 Then 'if the films dont sum up make them red
                grdDaypartSplit.Rows(0).Cells(0).Style.BackColor = Drawing.Color.Red
                grdDaypartSplit.Rows(0).Cells(1).Style.BackColor = Drawing.Color.Red
                grdDaypartSplit.Rows(0).Cells(2).Style.BackColor = Drawing.Color.Red
            Else
                grdDaypartSplit.Rows(0).Cells(0).Style.BackColor = Drawing.Color.White
                grdDaypartSplit.Rows(0).Cells(1).Style.BackColor = Drawing.Color.White
                grdDaypartSplit.Rows(0).Cells(2).Style.BackColor = Drawing.Color.White
            End If
        Catch
            grdDaypartSplit.Rows(0).Cells(0).Style.BackColor = Drawing.Color.Red
            grdDaypartSplit.Rows(0).Cells(1).Style.BackColor = Drawing.Color.Red
            grdDaypartSplit.Rows(0).Cells(2).Style.BackColor = Drawing.Color.Red
        End Try
    End Sub

    Private Sub grdFilmsplit_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdFilmsplit.CellValueChanged
        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        ColorFilmGrid()
    End Sub

    Private Sub selectedgridFilm(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdFilmsplit.GotFocus
        grdDaypartSplit.ClearSelection()
    End Sub

    Private Sub selectedgridDaypart(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDaypartSplit.GotFocus
        grdFilmsplit.ClearSelection()
    End Sub

    Private Sub frmCreateRBS_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        grdFilmsplit.Size = grdFilmsplit.PreferredSize
        grdFilmsplit.Width = grdFilmsplit.GetColumnDisplayRectangle(grdFilmsplit.Columns.Count - 1, False).Right + 1
        grdFilmsplit.Height = grdFilmsplit.GetRowDisplayRectangle(grdFilmsplit.Rows.Count - 1, False).Bottom + 1
        grdDaypartSplit.Height = grdDaypartSplit.GetRowDisplayRectangle(0, False).Bottom + 1
        Me.Size = Me.PreferredSize
        Me.Width = grdFilmsplit.Right + 15
        Me.Height = grdFilmsplit.Bottom + 50 + OK.Height
    End Sub
End Class



