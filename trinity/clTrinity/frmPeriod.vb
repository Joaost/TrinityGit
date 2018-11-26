Imports System
Imports System.Windows.Forms

Public Class frmPeriod

    Private Enum RowDataEnum
        NewRow = 0
        OldRow = 1
        EditedRow = 2
        RemovedRow = 3
    End Enum

    Private _activeCampaign As Trinity.cKampanj

    Private ReadOnly Property ActiveCampaign() As Trinity.cKampanj
        Get
            If _activeCampaign Is Nothing Then
                _activeCampaign = Campaign
            End If
            Return _activeCampaign
        End Get
    End Property

    Public Sub New(ByVal Camp As Trinity.cKampanj)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _activeCampaign = Camp

    End Sub

    Private Sub dtNormalTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtNormalTo.ValueChanged
        Dim d As Date
        Dim l As Long
        Dim i As Integer
        Dim StartDate As Date
        Dim EndDate As Date
        Dim a As Integer

        On Error GoTo dtNormalTo_Change_Error

        i = 0
        'clear and repopulate the week grid
        grdWeeks.Rows.Clear()
        Dim toYear As Integer = Year(dtNormalTo.Value)
        Dim toWeekOfYear = DatePart(DateInterval.WeekOfYear, dtNormalTo.Value, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)

        For l = dtNormalFrom.Value.ToOADate To Trinity.Helper.MondayOfWeek(dtNormalTo.Value).ToOADate + 6 Step 7
            d = Date.FromOADate(l)
            grdWeeks.Rows.Add()
            grdWeeks.Rows(i).Cells(0).Value = DatePart(DateInterval.WeekOfYear, d, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays).ToString
            grdWeeks.Rows(i).Cells(3).Value = DatePart(DateInterval.WeekOfYear, d, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays).ToString
            StartDate = d
            While Weekday(StartDate, vbMonday) > 1 And StartDate.ToOADate > Int(dtNormalFrom.Value.ToOADate)
                StartDate = StartDate.AddDays(-1)
            End While
            EndDate = Trinity.Helper.MondayOfWeek(Year(StartDate), DatePart(DateInterval.WeekOfYear, d, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays))
            While Weekday(EndDate, vbMonday) < 7 And EndDate.ToOADate < Int(dtNormalTo.Value.ToOADate)
                EndDate = EndDate.AddDays(1)
            End While
            grdWeeks.Rows(i).Cells(1).Value = StartDate.ToShortDateString
            grdWeeks.Rows(i).Cells(2).Value = EndDate.ToShortDateString
            grdWeeks.Rows(i).Tag = RowDataEnum.NewRow
            i = i + 1
        Next

        On Error GoTo 0
        Exit Sub

dtNormalTo_Change_Error:

        If IsIDE() Then
            a = MsgBox("Error:" & Chr(10) & Chr(10) & "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description & Chr(10) & Chr(10) & "Vill du utföra en felsökning?", vbYesNo, "TRINITY")
            If a = vbNo Then Exit Sub
            Stop
            Resume Next
        End If
        MsgBox("Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description & " in dtNormalTo_Change.", vbCritical, "Error")

    End Sub

    Private Sub frmPeriod_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim i As Integer

        'Sets today as default date in the date boxes
        dtNormalFrom.Value = Now
        dtNormalTo.Value = Now
        dtNormalTo_ValueChanged(New Object, New System.EventArgs)
        dtAdvancedFrom.Value = Now
        dtAdvancedTo.Value = Now

        'if there are a campaign which is altered (it already has set weeks) the old information is loaded into the 
        'table and the advanced option is set as default
        If ActiveCampaign.Channels(1).BookingTypes(1).Weeks.Count > 0 Then
            optAdvanced.Checked = True
            dtNormalFrom.Value = Date.FromOADate(ActiveCampaign.StartDate)
            dtNormalTo.Value = Date.FromOADate(ActiveCampaign.EndDate)
            grdWeeks.Rows.Clear()
            For i = 1 To ActiveCampaign.Channels(1).BookingTypes(1).Weeks.Count
                grdWeeks.Rows.Add()
                grdWeeks.Rows(i - 1).Cells(0).Value = ActiveCampaign.Channels(1).BookingTypes(1).Weeks(i).Name
                grdWeeks.Rows(i - 1).Cells(1).Value = Format(Date.FromOADate(ActiveCampaign.Channels(1).BookingTypes(1).Weeks(i).StartDate), "Short Date")
                grdWeeks.Rows(i - 1).Cells(2).Value = Format(Date.FromOADate(ActiveCampaign.Channels(1).BookingTypes(1).Weeks(i).EndDate), "Short Date")
                grdWeeks.Rows(i - 1).Cells(3).Value = ActiveCampaign.Channels(1).BookingTypes(1).Weeks(i).Name
                grdWeeks.Rows(i - 1).Tag = RowDataEnum.OldRow
            Next
            optNormal.Enabled = False
        Else ' if no prior dates is set the normal mode is set aas default
            optNormal.Checked = True
        End If

        'sets the width of the grid
        'grdWeeks.Columns(0).Width = 40
        'grdWeeks.Columns(1).Width = 80
        'grdWeeks.Columns(2).Width = 80
        grdWeeks.Columns(3).Width = 0
    End Sub

    Private Sub dtNormalFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtNormalFrom.ValueChanged
        Dim d As Date
        Dim i As Integer
        Dim l As Long
        Dim StartDate As Date
        Dim EndDate As Date
        Dim a As Integer

        On Error GoTo dtNormalFrom_Change_Error

        dtNormalTo.MinDate = dtNormalFrom.Value

        i = 0

        'clear the grid and repopulate it according to the newly set date
        grdWeeks.Rows.Clear()
        For l = dtNormalFrom.Value.ToOADate To dtNormalTo.Value.ToOADate Step 7
            d = Date.FromOADate(l)
            grdWeeks.Rows.Add()
            grdWeeks.Rows(i).Cells(0).Value = DatePart(DateInterval.WeekOfYear, d, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
            grdWeeks.Rows(i).Cells(3).Value = DatePart(DateInterval.WeekOfYear, d, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
            StartDate = d
            While Weekday(StartDate, vbMonday) > 1 And StartDate > dtNormalFrom.Value
                StartDate = StartDate.AddDays(-1)
            End While
            EndDate = d
            While Weekday(EndDate, vbMonday) < 7 And EndDate < dtNormalTo.Value
                EndDate = EndDate.AddDays(1)
            End While
            grdWeeks.Rows(i).Cells(1).Value = StartDate.ToShortDateString
            grdWeeks.Rows(i).Cells(2).Value = EndDate.ToShortDateString
            grdWeeks.Rows(i).Tag = RowDataEnum.NewRow
            i = i + 1
        Next

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

    Private Sub optNormal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optNormal.CheckedChanged
        frmAdvanced.Enabled = Not optNormal.Checked
        frmNormal.Enabled = optNormal.Checked
    End Sub

    Private Sub optAdvanced_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAdvanced.CheckedChanged
        frmAdvanced.Enabled = Not optNormal.Checked
        frmNormal.Enabled = optNormal.Checked
    End Sub


    Private Sub dtAdvancedFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtAdvancedFrom.ValueChanged
        'if the new date differs from other set date the textbox will display the date range
        dtAdvancedTo.MinDate = dtAdvancedFrom.Value
        If DatePart(DateInterval.WeekOfYear, dtAdvancedFrom.Value, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) <> DatePart(DateInterval.WeekOfYear, dtAdvancedTo.Value, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) Then
            txtTitle.Text = DatePart(DateInterval.WeekOfYear, dtAdvancedFrom.Value, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & "-" & DatePart(DateInterval.WeekOfYear, dtAdvancedTo.Value, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
        Else
            txtTitle.Text = DatePart(DateInterval.WeekOfYear, dtAdvancedFrom.Value, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
        End If
    End Sub

    Private Sub dtAdvancedTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtAdvancedTo.ValueChanged
        'if the new date differs from other set date the textbox will display the date range
        If DatePart(DateInterval.WeekOfYear, dtAdvancedFrom.Value, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) <> DatePart(DateInterval.WeekOfYear, dtAdvancedTo.Value, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) Then
            txtTitle.Text = DatePart(DateInterval.WeekOfYear, dtAdvancedFrom.Value, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & "-" & DatePart(DateInterval.WeekOfYear, dtAdvancedTo.Value, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
        Else
            txtTitle.Text = DatePart(DateInterval.WeekOfYear, dtAdvancedFrom.Value, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
        End If
    End Sub

    Private Sub cmdAddWeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddWeek.Click
        'Moves the interval one week forward so week 20-25 will become 21-26
        dtAdvancedTo.Value = dtAdvancedTo.Value.AddDays(7)
        dtAdvancedFrom.Value = dtAdvancedFrom.Value.AddDays(7)
        dtAdvancedFrom_ValueChanged(New Object, New EventArgs)
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Saved = False
        'Adds the selected interval to the grid
        grdWeeks.Rows.Add()
        grdWeeks.Rows(grdWeeks.Rows.Count - 1).Cells(0).Value = txtTitle.Text
        grdWeeks.Rows(grdWeeks.Rows.Count - 1).Cells(1).Value = Format(dtAdvancedFrom.Value, "yyyy-MM-dd")
        grdWeeks.Rows(grdWeeks.Rows.Count - 1).Cells(2).Value = Format(dtAdvancedTo.Value, "yyyy-MM-dd")
        grdWeeks.Rows(grdWeeks.Rows.Count - 1).Tag = RowDataEnum.NewRow
        grdWeeks.Rows(grdWeeks.Rows.Count - 1).Cells(3).Value = txtTitle.Text
        'verify the grid
        VerifyDate()
    End Sub

    Sub VerifyDate()
        Dim i As Integer
        Dim j As Integer

        'set all font color to black
        For i = 0 To grdWeeks.Rows.Count - 1
            grdWeeks.Rows(i).Cells(1).Style.ForeColor = Drawing.Color.Black
            grdWeeks.Rows(i).Cells(2).Style.ForeColor = Drawing.Color.Black
        Next

        '???????????????????????????????????????????????????????????????????????????????????? 
        For i = 0 To grdWeeks.Rows.Count - 1
            For j = 0 To grdWeeks.Rows.Count - 1
                If i <> j Then
                    If Not (grdWeeks.Rows(i).Visible = False Or grdWeeks.Rows(j).Visible = False) Then
                        If grdWeeks.Rows(i).Cells(1).Value <= grdWeeks.Rows(j).Cells(2).Value And _
                        grdWeeks.Rows(i).Cells(1).Value >= grdWeeks.Rows(j).Cells(1).Value And _
                        (grdWeeks.Rows(i).Visible = True And grdWeeks.Rows(j).Visible = True) Then
                            grdWeeks.Rows(i).Cells(1).Style.ForeColor = Drawing.Color.Red
                            grdWeeks.Rows(j).Cells(2).Style.ForeColor = Drawing.Color.Red
                        End If
                    End If
                End If
            Next
        Next
    End Sub

    Private Sub grdWeeks_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdWeeks.CellEndEdit
        Saved = False
    End Sub

    Private Sub grdWeeks_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdWeeks.CellValueChanged
        If e.RowIndex < 0 Then Exit Sub 'if its a negative value we won't continue
        If e.ColumnIndex = 0 Then
            If grdWeeks.Rows(e.RowIndex).Tag <> RowDataEnum.NewRow Then grdWeeks.Rows(e.RowIndex).Tag = RowDataEnum.EditedRow
        Else
            If grdWeeks.Rows(e.RowIndex).Tag <> RowDataEnum.NewRow Then grdWeeks.Rows(e.RowIndex).Tag = RowDataEnum.EditedRow
            VerifyDate()
        End If
    End Sub


    Private Sub cmdRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
        Saved = False
        For Each Row As DataGridViewRow In grdWeeks.SelectedRows
            If Row.Tag = RowDataEnum.NewRow Then
                grdWeeks.Rows.Remove(Row)
            Else
                Row.Visible = False
                Row.Tag = RowDataEnum.RemovedRow
            End If
        Next
    End Sub


    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Dim i As Integer
        Dim TmpChannel As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpWeek As Trinity.cWeek
        Dim TmpFilm As Trinity.cFilm
        Dim TmpFilm2 As Trinity.cFilm
        Dim TmpColl As New Collection
        Saved = False
        For i = 0 To grdWeeks.Rows.Count - 1
            If grdWeeks.Rows(i).Visible Then
                If TmpColl.Contains(grdWeeks.Rows(i).Cells(0).Value) Then
                    MessageBox.Show("All weeks must be assigned a unique name.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                Else
                    TmpColl.Add(grdWeeks.Rows(i).Cells(0).Value, grdWeeks.Rows(i).Cells(0).Value)
                End If
            End If
        Next

        'if no prior bookings we simple add the weeks to the campaign
        If ActiveCampaign.Channels(1).BookingTypes(1).Weeks.Count = 0 Then
            For i = 0 To grdWeeks.Rows.Count - 1
                If grdWeeks.Rows(i).Visible Then
                    For Each TmpChannel In ActiveCampaign.Channels
                        For Each TmpBT In TmpChannel.BookingTypes
                            TmpWeek = TmpBT.Weeks.Add(grdWeeks.Rows(i).Cells(0).Value)
                            TmpWeek.StartDate = CDate(grdWeeks.Rows(i).Cells(1).Value).ToOADate
                            TmpWeek.EndDate = CDate(grdWeeks.Rows(i).Cells(2).Value).ToOADate
                        Next
                    Next
                End If
            Next
        Else ' If we have prior bookings we need to add the newly added weeks to the existing
            '        For i = ActiveCampaign.Channels(1).BookingTypes(1).Weeks.Count + 1 To grdWeeks.Rows - 1
            For i = 0 To grdWeeks.Rows.Count - 1
                If grdWeeks.Rows(i).Tag = RowDataEnum.NewRow Then
                    If grdWeeks.Rows(i).Visible Then
                        For Each TmpChannel In ActiveCampaign.Channels
                            For Each TmpBT In TmpChannel.BookingTypes
                                Dim TmpFilms As Trinity.cFilms = TmpBT.Weeks(1).Films
                                TmpWeek = TmpBT.Weeks.Add(grdWeeks.Rows(i).Cells(0).Value)
                                TmpWeek.StartDate = CDate(grdWeeks.Rows(i).Cells(1).Value).ToOADate
                                TmpWeek.EndDate = CDate(grdWeeks.Rows(i).Cells(2).Value).ToOADate
                                For Each TmpFilm In TmpFilms
                                    TmpWeek.Films.Add(TmpFilm.Name).Clone(TmpFilm)
                                Next
                            Next
                        Next
                    End If
                ElseIf grdWeeks.Rows(i).Tag = RowDataEnum.EditedRow Then
                    If grdWeeks.Rows(i).Visible <> 0 Then
                        For Each TmpChannel In ActiveCampaign.Channels
                            For Each TmpBT In TmpChannel.BookingTypes
                                'TmpBT.Weeks(grdWeeks.Rows(i).Cells(3).Value).Name = grdWeeks.Rows(i).Cells(0).Value
                                'TmpBT.Weeks(grdWeeks.Rows(i).Cells(0).Value).StartDate = CDate(grdWeeks.Rows(i).Cells(1).Value).ToOADate
                                'TmpBT.Weeks(grdWeeks.Rows(i).Cells(0).Value).EndDate = CDate(grdWeeks.Rows(i).Cells(2).Value).ToOADate
                                TmpBT.Weeks(i + 1).Name = grdWeeks.Rows(i).Cells(0).Value
                                TmpBT.Weeks(i + 1).StartDate = CDate(grdWeeks.Rows(i).Cells(1).Value).ToOADate
                                TmpBT.Weeks(i + 1).EndDate = CDate(grdWeeks.Rows(i).Cells(2).Value).ToOADate
                            Next
                        Next
                    End If
                ElseIf grdWeeks.Rows(i).Tag = RowDataEnum.RemovedRow Then

                    For Each TmpChannel In ActiveCampaign.Channels
                        For Each TmpBT In TmpChannel.BookingTypes
                            TmpBT.Weeks.Remove(grdWeeks.Rows(i).Cells(3).Value)
                        Next
                    Next
                End If
            Next
        End If

        'Use this commented section if a week needs removing for some reason
        'For Each TmpChannel In ActiveCampaign.Channels
        '    For Each TmpBT In TmpChannel.BookingTypes
        '        TmpBT.Weeks.Remove("3")
        '    Next
        'Next
        'destroy the form
        Me.Dispose()
    End Sub
End Class