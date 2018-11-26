Public Class frmBudget

    Private Sub frmBudget_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.SuspendLayout()
        grdStaff.Rows.Clear()
        grdPlanning.Rows.Clear()
        grdMaterial.Rows.Clear()
        grdLogistics.Rows.Clear()
        For Each TmpCost As cCost In MyEvent.Budget.PlanningCosts
            Dim NewRow As Integer = grdPlanning.Rows.Add
            grdPlanning.Rows(NewRow).Tag = TmpCost
        Next
        For Each TmpCost As cCost In MyEvent.Budget.StaffCosts
            Dim NewRow As Integer = grdStaff.Rows.Add
            grdStaff.Rows(NewRow).Tag = TmpCost
        Next
        For Each TmpCost As cCost In MyEvent.Budget.MaterialCosts
            Dim NewRow As Integer = grdMaterial.Rows.Add
            grdMaterial.Rows(NewRow).Tag = TmpCost
        Next
        For Each TmpCost As cCost In MyEvent.Budget.LogisticsCosts
            Dim NewRow As Integer = grdLogistics.Rows.Add
            grdLogistics.Rows(NewRow).Tag = TmpCost
        Next
        Me.ResumeLayout()
    End Sub

    Private Sub grdStaff_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdStaff.CellFormatting
        Dim TmpCat As cStaffCategory = grdStaff.Rows(e.RowIndex).Tag
        Select Case grdStaff.Columns(e.ColumnIndex).Name
            Case "colStaffPrice"
                e.Value = Format(e.Value, "C0")
            Case "colStaffCTC"
                e.Value = Format(e.Value, "C0")
            Case "colStaffACHour"
                e.Value = Format(e.Value, "C0")
            Case "colStaffActualPrice"
                e.Value = Format(e.Value, "C0")
            Case "colStaffProfit"
                e.Value = Format(e.Value, "C0")
            Case "colStaffCount"
                If TmpCat.Quantity <> TmpCat.getQuantityFromRoles Then
                    e.CellStyle.ForeColor = Color.Blue
                Else
                    e.CellStyle.ForeColor = Color.Black
                End If
            Case "colStaffDays"
                If TmpCat.Days <> TmpCat.getDaysFromLocations Then
                    e.CellStyle.ForeColor = Color.Blue
                Else
                    e.CellStyle.ForeColor = Color.Black
                End If
        End Select
    End Sub

    Private Sub grdStaff_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdStaff.CellValueNeeded
        Dim TmpCat As cStaffCategory = grdStaff.Rows(e.RowIndex).Tag
        If TmpCat Is Nothing Then Exit Sub
        Select Case grdStaff.Columns(e.ColumnIndex).Name
            Case "colStaffName"
                e.Value = TmpCat.Name
            Case "colStaffDescription"
                e.Value = TmpCat.Description
            Case "colStaffCount"
                e.Value = TmpCat.Quantity
            Case "colStaffDays"
                e.Value = TmpCat.Days
            Case "colStaffHours"
                e.Value = TmpCat.HoursPerDay
            Case "colStaffPrice"
                e.Value = TmpCat.CostPerHourCTC
            Case "colStaffCTC"
                e.Value = TmpCat.CTC
            Case "colStaffACHour"
                e.Value = TmpCat.CostPerHourActual
            Case "colStaffActualPrice"
                e.Value = TmpCat.ActualCost
            Case "colStaffProfit"
                e.Value = TmpCat.CTC - TmpCat.ActualCost
        End Select
    End Sub

    Private Sub grdStaff_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdStaff.CellValuePushed
        Dim TmpCat As cStaffCategory = grdStaff.Rows(e.RowIndex).Tag
        Select Case grdStaff.Columns(e.ColumnIndex).Name
            Case "colStaffHours"
                TmpCat.HoursPerDay = e.Value
            Case "colStaffDays"
                TmpCat.Days = e.Value
            Case "colStaffPrice"
                TmpCat.CostPerHourCTC = e.Value
            Case "colStaffACHour"
                TmpCat.CostPerHourActual = e.Value
            Case "colStaffCount"
                TmpCat.Quantity = e.Value
            Case "colStaffName"
                TmpCat.Name = e.Value
            Case "colStaffDescription"
                TmpCat.Description = e.Value
        End Select
        grdStaff.InvalidateRow(e.RowIndex)
    End Sub

    Private Sub cmdAddStaff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddStaff.Click
        Dim TmpCat As cStaffCategory = MyEvent.Budget.StaffCosts.Add("")
        Dim NewRow As Integer = grdStaff.Rows.Add
        grdStaff.Rows(NewRow).Tag = TmpCat
    End Sub

    Private Sub cmdRemoveStaff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveStaff.Click
        For Each TmpRow As DataGridViewRow In grdStaff.SelectedRows
            MyEvent.Budget.StaffCosts.Remove(DirectCast(TmpRow.Tag, cStaffCategory).ID)
            grdStaff.Rows.Remove(TmpRow)
        Next
    End Sub

    Sub InvalidatedGrid(ByVal sender As Object, ByVal e As InvalidateEventArgs) Handles grdStaff.Invalidated, grdPlanning.Invalidated, grdLogistics.Invalidated, grdMaterial.Invalidated
        'Stop
        lblCTC.Text = Format(MyEvent.CTC, "C0")
        lblActualCost.Text = Format(MyEvent.ActualCost, "C0")
        lblProfit.Text = Format(MyEvent.CTC - MyEvent.ActualCost, "C0")
        If MyEvent.ActualCost > 0 Then
            lblPercent.Text = Format((MyEvent.CTC - MyEvent.ActualCost) / MyEvent.ActualCost, "P0")
            If (MyEvent.CTC - MyEvent.ActualCost) / MyEvent.ActualCost < 0.3 Then
                lblPercent.ForeColor = Color.Red
            Else
                lblPercent.ForeColor = Color.Green
            End If
        Else
            lblPercent.Text = Format(1, "P0")
            lblPercent.ForeColor = Color.Green
        End If
    End Sub

    Private Sub lblCTC_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCTC.SizeChanged
        lblCTC.Left = grpTotal.Width - lblCTC.Width - 15
    End Sub

    Private Sub lblActualCost_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblActualCost.SizeChanged
        lblActualCost.Left = grpTotal.Width - lblActualCost.Width - 15
    End Sub

    Private Sub lblProfit_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblProfit.SizeChanged
        lblProfit.Left = grpTotal.Width - lblProfit.Width - 15
    End Sub


    Private Sub grdMaterial_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdMaterial.CellFormatting
        Select Case grdMaterial.Columns(e.ColumnIndex).Name
            Case "colMaterialCTC"
                e.Value = Format(e.Value, "C0")
            Case "colMaterialActualPrice"
                e.Value = Format(e.Value, "C0")
            Case "colMaterialProfit"
                e.Value = Format(e.Value, "C0")
        End Select
    End Sub

    Private Sub grdMaterial_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdMaterial.CellValueNeeded
        Dim TmpCost As cCost = grdMaterial.Rows(e.RowIndex).Tag
        If TmpCost Is Nothing Then Exit Sub
        Select Case grdMaterial.Columns(e.ColumnIndex).Name
            Case "colMaterialName"
                e.Value = TmpCost.Name
            Case "colMaterialDescription"
                e.Value = TmpCost.Description
            Case "colMaterialCTC"
                e.Value = TmpCost.CTC
            Case "colMaterialActualPrice"
                e.Value = TmpCost.ActualCost
            Case "colMaterialProfit"
                e.Value = TmpCost.Profit
        End Select
    End Sub

    Private Sub grdMaterial_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdMaterial.CellValuePushed
        Dim TmpCost As cCost = grdMaterial.Rows(e.RowIndex).Tag
        Select Case grdMaterial.Columns(e.ColumnIndex).Name
            Case "colMaterialName"
                TmpCost.Name = e.Value
            Case "colMaterialDescription"
                TmpCost.Description = e.Value
            Case "colMaterialCTC"
                TmpCost.CTC = e.Value
            Case "colMaterialActualPrice"
                TmpCost.ActualCost = e.Value
        End Select
        grdMaterial.InvalidateRow(e.RowIndex)
    End Sub

    Private Sub cmdAddMaterial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddMaterial.Click
        Dim TmpCost As New cCost
        MyEvent.Budget.MaterialCosts.Add(TmpCost)
        Dim NewRow As Integer = grdMaterial.Rows.Add
        grdMaterial.Rows(NewRow).Tag = TmpCost
    End Sub

    Private Sub cmdRemoveMaterial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveMaterial.Click
        For Each TmpRow As DataGridViewRow In grdMaterial.SelectedRows
            MyEvent.Budget.MaterialCosts.Remove(DirectCast(TmpRow.Tag, cCost))
            grdMaterial.Rows.Remove(TmpRow)
        Next
    End Sub


    Private Sub grdLogistics_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdLogistics.CellFormatting
        Select Case grdLogistics.Columns(e.ColumnIndex).Name
            Case "colLogCTC"
                e.Value = Format(e.Value, "C0")
            Case "colLogActualPrice"
                e.Value = Format(e.Value, "C0")
            Case "colLogProfit"
                e.Value = Format(e.Value, "C0")
        End Select
    End Sub

    Private Sub grdLogistics_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdLogistics.CellValueNeeded
        Dim TmpCost As cCost = grdLogistics.Rows(e.RowIndex).Tag
        If TmpCost Is Nothing Then Exit Sub
        Select Case grdLogistics.Columns(e.ColumnIndex).Name
            Case "colLogName"
                e.Value = TmpCost.Name
            Case "colLogDescription"
                e.Value = TmpCost.Description
            Case "colLogCTC"
                e.Value = TmpCost.CTC
            Case "colLogActualPrice"
                e.Value = TmpCost.ActualCost
            Case "colLogProfit"
                e.Value = TmpCost.Profit
        End Select
    End Sub

    Private Sub grdLogistics_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdLogistics.CellValuePushed
        Dim TmpCost As cCost = grdLogistics.Rows(e.RowIndex).Tag
        Select Case grdLogistics.Columns(e.ColumnIndex).Name
            Case "colLogName"
                TmpCost.Name = e.Value
            Case "colLogDescription"
                TmpCost.Description = e.Value
            Case "colLogCTC"
                TmpCost.CTC = e.Value
            Case "colLogActualPrice"
                TmpCost.ActualCost = e.Value
        End Select
        grdLogistics.InvalidateRow(e.RowIndex)
    End Sub

    Private Sub cmdAddLogistics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddLog.Click
        Dim TmpCost As New cCost
        MyEvent.Budget.LogisticsCosts.Add(TmpCost)
        Dim NewRow As Integer = grdLogistics.Rows.Add
        grdLogistics.Rows(NewRow).Tag = TmpCost
    End Sub

    Private Sub cmdRemoveLogistics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveLog.Click
        For Each TmpRow As DataGridViewRow In grdLogistics.SelectedRows
            MyEvent.Budget.LogisticsCosts.Remove(DirectCast(TmpRow.Tag, cCost))
            grdLogistics.Rows.Remove(TmpRow)
        Next
    End Sub

    Private Sub grdPlanning_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdPlanning.CellFormatting
        Select Case grdPlanning.Columns(e.ColumnIndex).Name
            Case "colPlanningPrice"
                e.Value = Format(e.Value, "C0")
            Case "colPlanningCTC"
                e.Value = Format(e.Value, "C0")
            Case "colPlanningActualCost"
                e.Value = Format(e.Value, "C0")
            Case "colPlanningProfit"
                e.Value = Format(e.Value, "C0")
        End Select
    End Sub

    Private Sub grdPlanning_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdPlanning.CellValueNeeded
        Dim TmpCost As cHourCost = grdPlanning.Rows(e.RowIndex).Tag
        If TmpCost Is Nothing Then Exit Sub
        Select Case grdPlanning.Columns(e.ColumnIndex).Name
            Case "colPlanningName"
                e.Value = TmpCost.Name
            Case "colPlanningDescription"
                e.Value = TmpCost.Description
            Case "colPlanningCTC"
                e.Value = TmpCost.CTC
            Case "colPlanningHours"
                e.Value = TmpCost.Hours
            Case "colPlanningPrice"
                e.Value = TmpCost.CostPerHourCTC
            Case "colPlanningActualCost"
                e.Value = TmpCost.ActualCost
            Case "colPlanningProfit"
                e.Value = TmpCost.Profit
        End Select
    End Sub

    Private Sub grdPlanning_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdPlanning.CellValuePushed
        Dim TmpCost As cHourCost = grdPlanning.Rows(e.RowIndex).Tag
        Select Case grdPlanning.Columns(e.ColumnIndex).Name
            Case "colPlanningName"
                TmpCost.Name = e.Value
            Case "colPlanningDescription"
                TmpCost.Description = e.Value
            Case "colPlanningHours"
                TmpCost.Hours = e.Value
            Case "colPlanningActualCost"
                TmpCost.ActualCost = e.Value
            Case "colPlanningPrice"
                TmpCost.CostPerHourCTC = e.Value
        End Select
        grdPlanning.InvalidateRow(e.RowIndex)
    End Sub

    Private Sub cmdAddPlanning_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddPlanning.Click
        Dim TmpCost As New cHourCost
        MyEvent.Budget.PlanningCosts.Add(TmpCost)
        Dim NewRow As Integer = grdPlanning.Rows.Add
        grdPlanning.Rows(NewRow).Tag = TmpCost
    End Sub

    Private Sub cmdRemovePlanning_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemovePlanning.Click
        For Each TmpRow As DataGridViewRow In grdPlanning.SelectedRows
            MyEvent.Budget.PlanningCosts.Remove(DirectCast(TmpRow.Tag, cHourCost))
            grdPlanning.Rows.Remove(TmpRow)
        Next
    End Sub

    Private Sub lblPercent_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblPercent.Resize
        lblPercent.Left = lblCTC.Right - lblPercent.Width
    End Sub

    Private Sub frmBudget_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class