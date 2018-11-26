Public Class frmContractTarget

    Public TmpBT As Trinity.cContractBookingtype

    Private Sub txtAdedgeTarget_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAdedgeTarget.KeyUp
        If txtAdedgeTarget.Text = cmbTarget.SelectedItem.TargetName Then Exit Sub
        Try
            cmbTarget.SelectedItem.TargetType = Trinity.cTarget.TargetTypeEnum.trgMnemonicTarget
            cmbTarget.SelectedItem.AdEdgeTargetName = UCase(txtAdedgeTarget.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtAdedgeTarget_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdedgeTarget.TextChanged
        Saved = False
        If cmbTarget.SelectedItem Is Nothing Then Exit Sub
        If cmbTarget.SelectedItem.TargetType > 0 Then
            txtAdedgeTarget.ForeColor = Color.Gray
        Else
            txtAdedgeTarget.ForeColor = Color.Black
        End If
    End Sub

    Private Sub grdPricelist_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdPricelist.CellValueNeeded
        'if a variable is changed the grid also need to be changed
        Dim TmpPeriod As Trinity.cPricelistPeriod = grdPricelist.Rows(e.RowIndex).Tag
        If TmpPeriod Is Nothing Then Exit Sub
        If e.ColumnIndex = 0 Then
            If TmpPeriod.FromDate.ToOADate = 0 Then
                TmpPeriod.FromDate = New Date(Date.Now.Year, 1, 1)
            End If
            e.Value = TmpPeriod.FromDate
        ElseIf e.ColumnIndex = 1 Then
            If TmpPeriod.ToDate.ToOADate = 0 Then
                TmpPeriod.ToDate = New Date(Date.Now.Year, 12, 31)
            End If
            e.Value = TmpPeriod.ToDate
        ElseIf grdPricelist.Columns(e.ColumnIndex).Name.StartsWith("colCPP") Then
            If rdbCPP.Checked Then
                e.Value = Format((TmpPeriod.Price(True, grdPricelist.Columns(e.ColumnIndex).Tag)), "N1")
            Else
                e.Value = Format((TmpPeriod.Price(False, grdPricelist.Columns(e.ColumnIndex).Tag)), "N1")
            End If
        ElseIf grdPricelist.Columns(e.ColumnIndex).Name = "colUni" Then
            e.Value = TmpPeriod.TargetUni
        ElseIf grdPricelist.Columns(e.ColumnIndex).Name = "colNatUni" Then
            e.Value = TmpPeriod.TargetNat

            If rdbCPP.Checked = grdPricelist.Rows(e.RowIndex).Tag.priceiscpp Then
                For i As Integer = 0 To grdPricelist.Columns.Count - 1
                    grdPricelist.Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.Black
                Next
            Else
                For i As Integer = 0 To grdPricelist.Columns.Count - 1
                    grdPricelist.Rows(e.RowIndex).Cells(i).Style.ForeColor = Color.Blue
                Next
            End If
        End If
    End Sub

    Private Sub grdPricelist_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdPricelist.CellValuePushed
        'stored values entered in the grid in local variables
        Dim TmpPeriod As Trinity.cPricelistPeriod = grdPricelist.Rows(e.RowIndex).Tag
        If e.ColumnIndex = 0 Then
            TmpPeriod.FromDate = e.Value
        ElseIf e.ColumnIndex = 1 Then
            TmpPeriod.ToDate = e.Value
        ElseIf grdPricelist.Columns(e.ColumnIndex).Name.StartsWith("colCPP") Then
            If rdbCPP.Checked Then
                TmpPeriod.Price(True, grdPricelist.Columns(e.ColumnIndex).Tag) = e.Value
            Else
                TmpPeriod.Price(False, grdPricelist.Columns(e.ColumnIndex).Tag) = e.Value
            End If
        ElseIf grdPricelist.Columns(e.ColumnIndex).Name = "colUni" Then
            TmpPeriod.TargetUni = e.Value
        ElseIf grdPricelist.Columns(e.ColumnIndex).Name = "colNatUni" Then
            TmpPeriod.TargetNat = e.Value
        End If
    End Sub
    
    Private Sub chkCalcCPP_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCalcCPP.CheckedChanged
        cmbTarget.SelectedItem.CalcCPP = chkCalcCPP.Checked

        'change the number of columns displayed depending on true or false
        Dim i As Integer
        If Not chkCalcCPP.Checked Then
            'we want to hide the daypart columns and show the CPP column
            grdPricelist.Columns("colCPP").Visible = True
            For i = 5 To grdPricelist.Columns.Count - 1
                grdPricelist.Columns(i).Visible = False
            Next
        Else
            'we want to show the daypart columns and hide the CPP column
            grdPricelist.Columns("colCPP").Visible = False
            For i = 5 To grdPricelist.Columns.Count - 1
                grdPricelist.Columns(i).Visible = True
            Next
        End If
    End Sub

    Private Sub rdbCPP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCPP.CheckedChanged
        'when checked is changed we need to change the displayed changes
        Dim cols As Integer = grdPricelist.Columns.Count - 1
        Dim i As Integer
        If grdPricelist.Columns.Count < 3 Then Exit Sub
        If rdbCPP.Checked = True Then
            grdPricelist.Columns(4).HeaderText = "CPP"
            For i = 5 To cols
                grdPricelist.Columns(i).HeaderText = "CPP" & grdPricelist.Columns(i).HeaderText.Substring(3, grdPricelist.Columns(i).HeaderText.Length - 3)
            Next
        Else
            grdPricelist.Columns(4).HeaderText = "CPT"
            For i = 5 To cols
                grdPricelist.Columns(i).HeaderText = "CPT" & grdPricelist.Columns(i).HeaderText.Substring(3, grdPricelist.Columns(i).HeaderText.Length - 3)
            Next
        End If
        grdPricelist.Invalidate()
    End Sub

    Public Sub New(ByVal btc As Trinity.cContractBookingtype)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        TmpBT = btc
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Dim TmpPeriod As Trinity.cPricelistPeriod = DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods.Add("Pricelist period")
        TmpPeriod.FromDate = Now
        TmpPeriod.ToDate = Now

        grdPricelist.Rows.Add()
        grdPricelist.Rows(grdPricelist.Rows.Count - 1).Tag = TmpPeriod
    End Sub

    Private Sub cmdCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopy.Click
        Dim mnuCopy As New Windows.Forms.ContextMenuStrip
        For Each TmpChan As Trinity.cContractChannel In Campaign.Contract.Channels
            For Each TmpBT As Trinity.cContractBookingtype In TmpChan.BookingTypes(1)
                For Each TmpTarget As Trinity.cContractTarget In TmpBT.ContractTargets
                    If TmpTarget.PricelistPeriods.Count > 0 Then
                        With mnuCopy.Items.Add(TmpBT.ToString & " " & TmpTarget.TargetName, Nothing, AddressOf CopyTarget)
                            '.Tag = TmpBT.ToString & "," & TmpTarget.TargetName
                            .Tag = TmpTarget
                        End With
                    End If
                Next
            Next
        Next
        mnuCopy.Show(cmdCopy, 0, cmdCopy.Height)
    End Sub

    Private Sub CopyTarget(ByVal sender As Object, ByVal e As System.EventArgs)
        If Windows.Forms.MessageBox.Show("This will replace the pricelist for " & cmbTarget.Text & "." & vbCrLf & "Are you sure you want to proceed?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then Exit Sub
        Dim TmpTarget As Trinity.cContractTarget
        TmpTarget = DirectCast(sender.tag, Trinity.cContractTarget)
        Dim TmpT As Trinity.cContractTarget = cmbTarget.SelectedItem

        If TmpT Is Nothing Then
            If Not TmpBT.ContractTargets.Contains(sender.tag.targetname) Then TmpBT.ContractTargets.Add(sender.tag.targetname)
            TmpT = TmpBT.ContractTargets(sender.tag.targetname)
        End If
        With TmpBT.ContractTargets(TmpT.TargetName)
            .CalcCPP = TmpTarget.CalcCPP
            For i As Integer = 0 To Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Dayparts.Count - 1
                .DefaultDaypart(i) = TmpTarget.DefaultDaypart(i)
            Next
            .IsEntered = TmpTarget.IsEntered
            .IsContractTarget = True
            For Each TmpPeriod As Trinity.cPricelistPeriod In TmpBT.ContractTargets(TmpT.TargetName).PricelistPeriods
                TmpBT.ContractTargets(TmpT.TargetName).PricelistPeriods.Remove(TmpPeriod.ID)
            Next
            For Each TmpPeriod As Trinity.cPricelistPeriod In TmpTarget.PricelistPeriods

                Dim NewIndex As Trinity.cPricelistPeriod = .PricelistPeriods.Add(TmpPeriod.Name)
                NewIndex.FromDate = TmpPeriod.FromDate
                NewIndex.PriceIsCPP = TmpPeriod.PriceIsCPP
                NewIndex.Price(TmpPeriod.PriceIsCPP) = TmpPeriod.Price(TmpPeriod.PriceIsCPP)
                For i As Integer = 0 To Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Dayparts.Count - 1
                    NewIndex.Price(TmpPeriod.PriceIsCPP, i) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, i)
                Next
                NewIndex.TargetNat = TmpPeriod.TargetNat
                NewIndex.TargetUni = TmpPeriod.TargetUni
                NewIndex.ToDate = TmpPeriod.ToDate
            Next
        End With
        cmbTarget.Items.Add(TmpT)
        cmbTarget.SelectedItem = TmpT
    End Sub

    Private Sub cmdRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
        For Each TmpRow As Windows.Forms.DataGridViewRow In grdPricelist.SelectedRows
            Dim TmpPeriod As Trinity.cPricelistPeriod = grdPricelist.Rows(TmpRow.Index).Tag
            DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods.Remove(TmpPeriod.ID)
            grdPricelist.Rows.Remove(grdPricelist.Rows(TmpRow.Index))
        Next
    End Sub

    Private Sub cmdWizard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWizard.Click
        mnuWizard.Show(cmdWizard, New System.Drawing.Point(0, cmdWizard.Height))
    End Sub

    Private Sub mnuWeekOnCPT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuWeekOnCPT.Click

        Dim d As Long
        Dim ID As String

        For d = Trinity.Helper.MondayOfWeek(Date.Now.Year, 1).ToOADate To Trinity.Helper.MondayOfWeek(Date.Now.Year, 52).AddDays(6).ToOADate Step 7
            ID = DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods.Add(DatePart(DateInterval.WeekOfYear, Date.FromOADate(d), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)).ID
            If Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 1).Year < CInt(Date.Now.Year) Then
                DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods(ID).FromDate = New Date(Date.Now.Year, 1, 1)
            Else
                DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods(ID).FromDate = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 1)
            End If
            DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods(ID).ToDate = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 7)
            DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods(ID).PriceIsCPP = False
        Next

        'update the view
        RepaintGrid()
    End Sub

    Private Sub mnuWeekOnCPP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuWeekOnCPP.Click
        Dim d As Long
        Dim ID As String

        For d = Trinity.Helper.MondayOfWeek(Date.Now.Year, 1).ToOADate To Trinity.Helper.MondayOfWeek(Date.Now.Year, 52).AddDays(6).ToOADate Step 7
            ID = DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods.Add(DatePart(DateInterval.WeekOfYear, Date.FromOADate(d), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)).ID
            If Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 1).Year < CInt(Date.Now.Year) Then
                DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods(ID).FromDate = New Date(Date.Now.Year, 1, 1)
            Else
                DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods(ID).FromDate = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 1)
            End If
            DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods(ID).ToDate = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 7)
            DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods(ID).PriceIsCPP = True
        Next

        'update the view
        RepaintGrid()
    End Sub

    Private Sub mnuMonthOnCPP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuMonthOnCPP.Click
        Dim ID As String

        For i As Integer = 1 To 12
            ID = DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods.Add(i).ID
            DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods(ID).FromDate = New Date(Date.Now.Year, i, 1)
            DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods(ID).ToDate = Date.FromOADate(DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods(ID).FromDate.AddMonths(1).ToOADate - 1)
            DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods(ID).PriceIsCPP = True
        Next
        RepaintGrid()
    End Sub

    Private Sub mnuMonthOnCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMonthOnCPT.Click
        Dim ID As String

        For i As Integer = 1 To 12
            ID = DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods.Add(i).ID
            DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods(ID).FromDate = New Date(Date.Now.Year, i, 1)
            DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods(ID).ToDate = Date.FromOADate(DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods(ID).FromDate.AddMonths(1).ToOADate - 1)
            DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods(ID).PriceIsCPP = False
        Next

        RepaintGrid()
    End Sub

    Private Sub RepaintGrid()
        grdPricelist.Rows.Clear()
        For Each TmpPeriod As Trinity.cPricelistPeriod In DirectCast(cmbTarget.SelectedItem, Trinity.cContractTarget).PricelistPeriods
            grdPricelist.Rows.Add()
            grdPricelist.Rows(grdPricelist.Rows.Count - 1).Tag = TmpPeriod
        Next
    End Sub

    Private Sub frmContractTarget_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbTarget.Items.Clear()
        For Each TmpTarget As Trinity.cContractTarget In TmpBT.ContractTargets
            If TmpTarget.IsContractTarget Then
                cmbTarget.Items.Add(TmpTarget)
            End If
        Next

        While grdPricelist.Columns.Count > 5
            grdPricelist.Columns.RemoveAt(5)
        End While

        grdPricelist.Columns("colCPP").Tag = "-1"

        Dim TmpCol As System.Windows.Forms.DataGridViewTextBoxColumn

        For i As Integer = 0 To Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Dayparts.Count - 1
            TmpCol = New System.Windows.Forms.DataGridViewTextBoxColumn
            TmpCol.Name = "colCPP" & Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Dayparts(i).Name
            TmpCol.HeaderText = "CPP " & Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Dayparts(i).Name
            TmpCol.Tag = i
            TmpCol.Visible = False
            grdPricelist.Columns.Add(TmpCol)
        Next

    End Sub

    Private Sub cmbTarget_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTarget.SelectedIndexChanged
        'we update all boxes and grid
        Dim TmpTarget As Trinity.cContractTarget = cmbTarget.SelectedItem
        txtAdedgeTarget.Text = TmpTarget.AdEdgeTargetName

        grdPricelist.Rows.Clear()
        For Each TmpPeriod As Trinity.cPricelistPeriod In TmpTarget.PricelistPeriods
            grdPricelist.Rows.Add()
            grdPricelist.Rows(grdPricelist.Rows.Count - 1).Tag = TmpPeriod
        Next

        If TmpTarget.CalcCPP Then
            chkCalcCPP.Checked = True
        Else
            chkCalcCPP.Checked = False
        End If

    End Sub

    Private Sub cmdAddTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddTarget.Click
        Saved = False
        Dim TmpStr As String = InputBox("Name of target:", "T R I N I T Y", Nothing)
        If TmpStr Is Nothing Or TmpStr = "" Then Exit Sub

        Dim TmpTarget As Trinity.cContractTarget = TmpBT.ContractTargets.Add(TmpStr)
        TmpTarget.IsContractTarget = True
        cmbTarget.Items.Add(TmpTarget)
        cmbTarget.SelectedItem = TmpTarget

        Dim adedge As String = ""
        If IsNumeric(TmpStr.Substring(0, 1)) Then
            If TmpStr.Substring(1, 1) = "+" OrElse TmpStr.Substring(2, 1) = "+" Then
                adedge = TmpStr
            ElseIf adedge = "" AndAlso TmpStr.Substring(1, 1) = "-" OrElse TmpStr.Substring(2, 1) = "-" Then
                adedge = TmpStr
            End If
        Else
            Dim tmpSubStr As String = Trim(TmpStr.Substring(1))
            Select Case TmpStr.Substring(0, 1).ToUpper
                Case "A"
                    If IsNumeric(tmpSubStr.Substring(0, 1)) Then
                        If tmpSubStr.Substring(1, 1) = "+" OrElse tmpSubStr.Substring(2, 1) = "+" Then
                            adedge = tmpSubStr
                        ElseIf adedge = "" AndAlso tmpSubStr.Substring(1, 1) = "-" OrElse tmpSubStr.Substring(2, 1) = "-" Then
                            adedge = tmpSubStr
                        End If
                    End If
                Case "P"
                    If IsNumeric(tmpSubStr.Substring(0, 1)) Then
                        If tmpSubStr.Substring(1, 1) = "+" OrElse tmpSubStr.Substring(2, 1) = "+" Then
                            adedge = tmpSubStr
                        ElseIf adedge = "" AndAlso tmpSubStr.Substring(1, 1) = "-" OrElse tmpSubStr.Substring(2, 1) = "-" Then
                            adedge = tmpSubStr
                        End If
                    End If
                Case "M"
                    If IsNumeric(tmpSubStr.Substring(0, 1)) Then
                        If tmpSubStr.Substring(1, 1) = "+" OrElse tmpSubStr.Substring(2, 1) = "+" Then
                            adedge = "M" + tmpSubStr
                        ElseIf adedge = "" AndAlso tmpSubStr.Substring(1, 1) = "-" OrElse tmpSubStr.Substring(2, 1) = "-" Then
                            adedge = "M" + tmpSubStr
                        End If
                    End If
                Case "K"
                    If IsNumeric(tmpSubStr.Substring(0, 1)) Then
                        If tmpSubStr.Substring(1, 1) = "+" OrElse tmpSubStr.Substring(2, 1) = "+" Then
                            adedge = "W" + tmpSubStr
                        ElseIf adedge = "" AndAlso tmpSubStr.Substring(1, 1) = "-" OrElse tmpSubStr.Substring(2, 1) = "-" Then
                            adedge = "W" + tmpSubStr
                        End If
                    End If
                Case "W"
                    If IsNumeric(tmpSubStr.Substring(0, 1)) Then
                        If tmpSubStr.Substring(1, 1) = "+" OrElse tmpSubStr.Substring(2, 1) = "+" Then
                            adedge = "W" + tmpSubStr
                        ElseIf adedge = "" AndAlso tmpSubStr.Substring(1, 1) = "-" OrElse tmpSubStr.Substring(2, 1) = "-" Then
                            adedge = "W" + tmpSubStr
                        End If
                    End If
            End Select
        End If

        If adedge <> "" Then
            Dim test As New ConnectWrapper.Brands
            If test.setTargetMnemonic(adedge, False) = 1 Then
                TmpTarget.AdEdgeTargetName = adedge
                txtAdedgeTarget.Text = adedge
            End If
        End If
    End Sub

    Private Sub cmdDeleteTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteTarget.Click
        TmpBT.ContractTargets.Remove(cmbTarget.SelectedItem.TargetName)
    End Sub

    Private Sub cmdCalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCalculate.Click
        Dim TmpTarget As Trinity.cContractTarget = cmbTarget.SelectedItem

        If TmpTarget.AdEdgeTargetName Is Nothing Then Exit Sub

        Dim ValueTarget As New Trinity.cTarget(Campaign)
        ValueTarget.TargetName = TmpTarget.AdEdgeTargetName

        'TmpTarget.Target.NoUniverseSize = False
        Dim size As Single = ValueTarget.UniSizeTot
        Dim size2 As Single = ValueTarget.UniSize
        For Each TmpRow As Windows.Forms.DataGridViewRow In grdPricelist.SelectedRows
            TmpRow.Cells("colNatUni").Value = size
            TmpRow.Cells("colUni").Value = size2
        Next
    End Sub

    Private Sub SetPriceType(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tmpPeriod As Trinity.cPricelistPeriod
        Dim row As System.Windows.Forms.DataGridViewRow

        If sender.tag = "CPP" Then
            For Each row In grdPricelist.SelectedRows
                tmpPeriod = row.Tag
                tmpPeriod.PriceIsCPP = True
            Next
        Else
            For Each row In grdPricelist.SelectedRows
                tmpPeriod = row.Tag
                tmpPeriod.PriceIsCPP = False
            Next
        End If

        grdPricelist.Invalidate()
    End Sub

    Private Sub cmdSetPriceType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetPriceType.Click
        Dim mnuSet As New Windows.Forms.ContextMenuStrip

        With mnuSet.Items.Add("CPP", Nothing, AddressOf SetPriceType)
            .Tag = "CPP"
        End With
        With mnuSet.Items.Add("CPT", Nothing, AddressOf SetPriceType)
            .Tag = "CPT"
        End With

        mnuSet.Show(cmdSetPriceType, New System.Drawing.Point(0, cmdSetPriceType.Height))
    End Sub
End Class