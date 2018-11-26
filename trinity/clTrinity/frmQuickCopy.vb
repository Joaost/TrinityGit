Imports System.Windows.Forms

Public Class frmQuickCopy

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmQuickCopy_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        cmbChannel.Items.Clear()
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            cmbChannel.Items.Add(TmpChan)
        Next
        If cmbChannel.Items.Count > 0 Then cmbChannel.SelectedIndex = 0
        txtName.Text = ""
        txtShortname.Text = ""
        UpdatePricelistCombo()
    End Sub

    Private Sub cmbChannel_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbChannel.SelectedIndexChanged
        cmbBookingType.Items.Clear()
        cmbBookingType.DisplayMember = "Name"
        With DirectCast(cmbChannel.SelectedItem, Trinity.cChannel)
            For Each _bt As Trinity.cBookingType In .BookingTypes
                If _bt.Pricelist.Targets.Count = 0 OrElse (From _target As Trinity.cPricelistTarget In _bt.Pricelist.Targets From _price As Trinity.cPricelistPeriod In _target.PricelistPeriods Where _price.FromDate.ToOADate < Campaign.EndDate AndAlso _price.ToDate.ToOADate > Campaign.EndDate).Count = 0 Then
                    cmbBookingType.Items.Add(_bt)
                End If
            Next
        End With
        Dim _newBT As New Trinity.cBookingType(Campaign) With {.Name = "<Add new>"}
        _newBT.ParentChannel = cmbChannel.SelectedItem
        _newBT.ReadDefaultDayparts()
        cmbBookingType.Items.Add(_newBT)
        cmbBookingType.SelectedIndex = 0
    End Sub

    Private Sub frmQuickCopy_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub cmbBookingType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbBookingType.SelectedIndexChanged
        pnlAdd.Visible = cmbBookingType.SelectedIndex = cmbBookingType.Items.Count - 1        
        If optNewPrice.Checked AndAlso grdPrice.Tag IsNot cmbBookingType.SelectedItem Then
            UpdatePriceGrid()
            grdPrice.Tag = cmbBookingType.SelectedItem
        End If
        Dim _bt As Trinity.cBookingType = cmbBookingType.SelectedItem
        btnSpecifics.Checked = _bt.IsSpecific
        chkIsCompensation.Checked = _bt.IsCompensation
        chkIsPremium.Checked = _bt.IsPremium
        chkIsSponsorship.Checked = _bt.IsSponsorship
    End Sub

    Sub UpdatePricelistCombo()
        cmbPricelist.Items.Clear()
        For Each _file As String In IO.Directory.GetFiles(IO.Path.Combine(TrinitySettings.ActiveDataPath, Campaign.Area & "\Pricelists\"))
            Dim _name As String = IO.Path.GetFileNameWithoutExtension(_file)
            cmbPricelist.Items.Add(_name)
        Next
    End Sub

    Sub UpdatePriceGrid()
        Dim _bt As Trinity.cBookingType = cmbBookingType.SelectedItem
        grdPrice.Rows.Clear()
        While grdPrice.ColumnCount > 1
            grdPrice.Columns.RemoveAt(1)
        End While
        For Each _dp As Trinity.cDaypart In _bt.Dayparts
            grdPrice.Columns.Add(New DataGridViewTextBoxColumn With {.HeaderText = "CPP (" & _dp.Name & ")", .Width = 70})
            grdPrice.Columns(grdPrice.ColumnCount - 1).HeaderCell.Style.Font = New System.Drawing.Font(Me.Font.FontFamily, 8)
            grdPrice.Columns(grdPrice.ColumnCount - 1).HeaderCell.Style.WrapMode = DataGridViewTriState.False
        Next
        grdPrice.Rows.Add()
    End Sub

    Private Sub optNewPrice_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optNewPrice.CheckedChanged
        grpNew.Enabled = optNewPrice.Checked

        If grpNew.Enabled Then
            txtAdedgeTarget.Tag = New Trinity.cPricelistTarget(Campaign, cmbBookingType.SelectedItem)
            UpdatePriceGrid()
        End If
    End Sub

    Private Sub grdPrice_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdPrice.CellFormatting
        If Not String.IsNullOrEmpty(e.Value) Then
            Single.TryParse(e.Value, e.Value)
            e.Value = Format(e.Value, "N0")
            e.FormattingApplied = True
        End If
    End Sub

    Private Sub grdPrice_CellValueNeeded(sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdPrice.CellValueNeeded
        e.Value = grdPrice.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag
    End Sub

    Private Sub grdPrice_CellValuePushed(sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdPrice.CellValuePushed
        grdPrice.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = e.Value
    End Sub

    Private Shadows Sub MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblTarget.MouseHover
        DirectCast(sender, Windows.Forms.Label).BorderStyle = Windows.Forms.BorderStyle.Fixed3D
    End Sub

    Private Shadows Sub MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblTarget.MouseLeave
        DirectCast(sender, Windows.Forms.Label).BorderStyle = Windows.Forms.BorderStyle.None
    End Sub

    Private Sub lblTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTarget.Click
        Dim TmpTarget As Trinity.cPricelistTarget = txtAdedgeTarget.Tag
        If frmFindTarget.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TmpTarget.Target.TargetType = Trinity.cTarget.TargetTypeEnum.trgUserTarget
            TmpTarget.Target.TargetGroup = frmFindTarget.tvwTargets.SelectedNode.Tag
            TmpTarget.Target.TargetName = frmFindTarget.tvwTargets.SelectedNode.Text
            txtAdedgeTarget.Text = TmpTarget.Target.TargetName
            'makes the label red if the value is invalid
            txtAdedgeTarget.ForeColor = Color.Gray
            Saved = False
        End If
    End Sub

    Private Sub txtAdedgeTarget_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdedgeTarget.TextChanged
        Saved = False
        Dim TmpTarget As Trinity.cPricelistTarget = txtAdedgeTarget.Tag
        If TmpTarget Is Nothing Then Exit Sub
        If TmpTarget.Target.TargetType > 0 Then
            txtAdedgeTarget.ForeColor = Color.Gray
        Else
            txtAdedgeTarget.ForeColor = Color.Black
        End If
        grdPrice.Rows(0).Cells(0).Value = TmpTarget.Target.UniSize
    End Sub

    Private Sub txtAdedgeTarget_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAdedgeTarget.KeyUp
        Dim TmpTarget As Trinity.cPricelistTarget = txtAdedgeTarget.Tag
        If txtAdedgeTarget.Text = TmpTarget.Target.TargetName Then Exit Sub
        Try
            TmpTarget.Target.TargetType = Trinity.cTarget.TargetTypeEnum.trgMnemonicTarget
            TmpTarget.Target.TargetName = UCase(txtAdedgeTarget.Text)
            grdPrice.Rows(0).Cells(0).Value = TmpTarget.Target.UniSize
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRBS_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles btnRBS.CheckedChanged

    End Sub
End Class
