Imports System
Imports System.Windows.Forms

Public Class frmOtherMediaperiod

    Private Enum RowDataEnum
        NewRow = 0
        OldRow = 1
        EditedRow = 2
        RemovedRow = 3
    End Enum

    Private _mediaType As Trinity.cOtherMediaType

    Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub cmdOk_Click(sender As System.Object, e As System.EventArgs) Handles cmdOk.Click
        Dim _nameList As New List(Of String)
        For Each _row As DataGridViewRow In grdWeeks.Rows
            If _row.Visible Then
                If _nameList.Contains(_row.Cells(0).Value) Then
                    MessageBox.Show("All weeks must be assigned a unique name.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                Else
                    _nameList.Add(_row.Cells(0).Value)
                End If
            End If
        Next

        If _mediaType.WeekTemplates.Count = 0 Then
            For Each _row As DataGridViewRow In grdWeeks.Rows
                If _row.Visible Then
                    _mediaType.WeekTemplates.Add(_row.Cells(0).Value, CDate(_row.Cells(1).Value), CDate(_row.Cells(2).Value))
                End If
            Next
        Else
            For _r As Integer = 0 To grdWeeks.Rows.Count - 1
                Dim _row As DataGridViewRow = grdWeeks.Rows(_r)
                If _row.Tag = RowDataEnum.NewRow Then
                    If _row.Visible Then
                        _mediaType.WeekTemplates.Add(_row.Cells(0).Value, CDate(_row.Cells(1).Value), CDate(_row.Cells(2).Value))
                    End If
                ElseIf _row.Tag = RowDataEnum.EditedRow Then
                    If _row.Visible <> 0 Then
                        'TmpBT.Weeks(grdWeeks.Rows(i).Cells(3).Value).Name = grdWeeks.Rows(i).Cells(0).Value
                        'TmpBT.Weeks(grdWeeks.Rows(i).Cells(0).Value).StartDate = CDate(grdWeeks.Rows(i).Cells(1).Value).ToOADate
                        'TmpBT.Weeks(grdWeeks.Rows(i).Cells(0).Value).EndDate = CDate(grdWeeks.Rows(i).Cells(2).Value).ToOADate
                        _mediaType.WeekTemplates(_r).Name = _row.Cells(0).Value
                        _mediaType.WeekTemplates(_r).StartDate = CDate(_row.Cells(1).Value)
                        _mediaType.WeekTemplates(_r).EndDate = CDate(_row.Cells(2).Value)
                    End If
                ElseIf _row.Tag = RowDataEnum.RemovedRow Then
                    _mediaType.WeekTemplates.Remove(_row.Cells(3).Value)
                End If
            Next
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cmdRemove_Click(sender As System.Object, e As System.EventArgs) Handles cmdRemove.Click
        For Each Row As DataGridViewRow In grdWeeks.SelectedRows
            If Row.Tag = RowDataEnum.NewRow Then
                grdWeeks.Rows.Remove(Row)
            Else
                Row.Visible = False
                Row.Tag = RowDataEnum.RemovedRow
            End If
        Next
    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        'Adds the selected interval to the grid
        If txtTitle.Text = "" Then
            Windows.Forms.MessageBox.Show("The week needs to have a name.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtTitle.Focus()
            Exit Sub
        End If
        grdWeeks.Rows.Add()
        grdWeeks.Rows(grdWeeks.Rows.Count - 1).Cells(0).Value = txtTitle.Text
        grdWeeks.Rows(grdWeeks.Rows.Count - 1).Cells(1).Value = Format(dtAdvancedFrom.Value, "yyyy-MM-dd")
        grdWeeks.Rows(grdWeeks.Rows.Count - 1).Cells(2).Value = Format(dtAdvancedTo.Value, "yyyy-MM-dd")
        grdWeeks.Rows(grdWeeks.Rows.Count - 1).Tag = RowDataEnum.NewRow
        grdWeeks.Rows(grdWeeks.Rows.Count - 1).Cells(3).Value = txtTitle.Text
        'verify the grid
        VerifyDate()
    End Sub

    Private Sub cmdAddWeek_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddWeek.Click
        dtAdvancedTo.Value = dtAdvancedTo.Value.AddDays(7)
        dtAdvancedFrom.Value = dtAdvancedFrom.Value.AddDays(7)
        dtAdvancedFrom_ValueChanged(New Object, New EventArgs)
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

    Sub VerifyDate()
        Dim i As Integer
        Dim j As Integer

        For i = 0 To grdWeeks.Rows.Count - 1
            grdWeeks.Rows(i).Cells(1).Style.ForeColor = Drawing.Color.Black
            grdWeeks.Rows(i).Cells(2).Style.ForeColor = Drawing.Color.Black
        Next

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
                        If _mediaType.FixedWeeks Then
                            If Weekday(grdWeeks.Rows(i).Cells(1).Value, FirstDayOfWeek.Monday) <> _mediaType.FixedWeekStartWeekday Then
                                grdWeeks.Rows(i).Cells(1).Style.ForeColor = Drawing.Color.Red
                            End If
                            If Weekday(grdWeeks.Rows(i).Cells(2).Value, FirstDayOfWeek.Monday) <> _mediaType.FixedWeekEndWeekday Then
                                grdWeeks.Rows(i).Cells(2).Style.ForeColor = Drawing.Color.Red
                            End If
                        End If
                    End If
                End If
            Next
        Next
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

    Public Sub New(MediaType As Trinity.cOtherMediaType)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _mediaType = MediaType
        grdWeeks.Rows.Clear()
        For i As Integer = 1 To _mediaType.WeekTemplates.Count
            grdWeeks.Rows.Add()
            grdWeeks.Rows(i - 1).Cells(0).Value = _mediaType.WeekTemplates(i - 1).Name
            grdWeeks.Rows(i - 1).Cells(1).Value = Format(_mediaType.WeekTemplates(i - 1).StartDate, "Short Date")
            grdWeeks.Rows(i - 1).Cells(2).Value = Format(_mediaType.WeekTemplates(i - 1).EndDate, "Short Date")
            grdWeeks.Rows(i - 1).Cells(3).Value = _mediaType.WeekTemplates(i - 1).Name
            grdWeeks.Rows(i - 1).Tag = RowDataEnum.OldRow
        Next
    End Sub
End Class