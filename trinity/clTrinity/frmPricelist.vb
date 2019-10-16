Imports System.Windows.Forms
Imports System.Drawing
Imports System.Xml.Linq
Imports System.Threading

Public Class frmPricelist

    Private _channels As Trinity.cChannels
    Private intCol As Integer = -1
    Private intRow As Integer = -1

    Private Sub frmPricelist_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        cmdSaveAs.Visible = TrinitySettings.Developer OrElse TrinitySettings.TrustedUser

        If TrinitySettings.UserEmail = "joakim.koch@groupm.com"
            cmdImport.Visible = TrinitySettings.Developer
            cmdCompress.Visible = TrinitySettings.Developer
            cmdUpdateAllUniverses.Visible = TrinitySettings.Developer
            cmdFixNorway.Visible = TrinitySettings.Developer
            cmbCopyToNextYear.Visible = TrinitySettings.Developer
        End If
        chkStandard.Enabled = TrinitySettings.Developer
        cmdSave.Visible = TrinitySettings.Developer OrElse TrinitySettings.TrustedUser
    End Sub

    Private Sub frmPricelist_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        cmbBookingType.Items.Clear()

        For Each TmpChan As Trinity.cChannel In _channels

            cmbChannel.Items.Add(TmpChan.ChannelName)

        Next
        UpdatePricelistCombo()
        If cmbChannel.Items.Count > 0 Then
            cmbChannel.SelectedIndex = 0
        End If

        If Campaign.Area = "DK" Then
            cmdEnhancements.Visible = True
            cmdEditEnhancement.Visible = True
        End If



    End Sub

    Sub UpdatePricelistCombo()
        cmbPricelist.Items.Clear()
        If IO.Directory.Exists(IO.Path.Combine(TrinitySettings.SharedDataPath, Campaign.Area & "/Pricelists")) Then
            For Each _file As String In IO.Directory.GetFiles(IO.Path.Combine(TrinitySettings.SharedDataPath, Campaign.Area & "/Pricelists"))
                Dim _name As String = IO.Path.GetFileNameWithoutExtension(_file)
                cmbPricelist.Items.Add(_name)
            Next
        End If
    End Sub

    Private Sub cmbChannel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBookingType.SelectedIndexChanged
        Dim TmpBT As Trinity.cBookingType = cmbBookingType.SelectedItem

        If TmpBT Is Nothing Then Exit Sub

        UpdateGrid()

        txtAdedgeTarget.Text = ""
        chkCalcCPP.Checked = False
        chkStandard.Checked = False
        txtAdedgeTarget.Enabled = False
        chkCalcCPP.Enabled = False
        chkStandard.Enabled = False
        grdPricelist.Rows.Clear()
        grdIndexes.Rows.Clear()
        If cmbTarget.Items.Count > 0 Then
            cmbTarget.SelectedIndex = 0
        End If

        If (Not TmpBT.IsUserEditable) And (Not TrinitySettings.Developer) Then
            'protected mode
            cmdAdd.Enabled = False
            cmdAddIndex.Enabled = False
            cmdAddTarget.Enabled = False
            cmdCalculate.Enabled = False
            cmdCopy.Enabled = False
            cmdCopyIndex.Enabled = False
            cmdCopyTarget.Enabled = False
            cmdDeleteTarget.Enabled = False
            cmdDelIndex.Enabled = False
            cmdEditEnhancement.Enabled = False
            cmdIndexWizard.Enabled = False
            cmdRemove.Enabled = False
            'cmdSaveAll.Visible = False
            'cmdSaveToFile.Enabled = False
            'cmdSaveAllChannels.Visible = False
            'cmdSaveAllChannels.Enabled = False
            cmdSetPriceType.Enabled = False
            cmdWizard.Enabled = False
            'cmdUpdateAllUniverses.Visible = False

            chkCalcCPP.Enabled = False
            chkStandard.Enabled = False

            txtAdedgeTarget.Enabled = False
            txtMaxRatings.Enabled = False

            grdPricelist.ReadOnly = True
            grdIndexes.ReadOnly = True

        Else
            'unprotected mode
            cmdAdd.Enabled = True
            cmdAddIndex.Enabled = True
            cmdAddTarget.Enabled = True
            cmdCalculate.Enabled = True
            cmdCopy.Enabled = True
            cmdCopyIndex.Enabled = True
            cmdCopyTarget.Enabled = True
            cmdDeleteTarget.Enabled = True
            cmdDelIndex.Enabled = True
            cmdEditEnhancement.Enabled = True
            cmdIndexWizard.Enabled = True
            cmdRemove.Enabled = True
            'cmdSaveAll.Visible = True
            'cmdSaveToFile.Enabled = True
            cmdSetPriceType.Enabled = True
            'cmdSaveAllChannels.Visible = True
            'cmdSaveAllChannels.Enabled = True
            cmdWizard.Enabled = True

            chkCalcCPP.Enabled = True
            chkStandard.Enabled = TrinitySettings.Developer

            txtAdedgeTarget.Enabled = True
            txtMaxRatings.Enabled = True

            grdPricelist.ReadOnly = False
            grdIndexes.ReadOnly = False

        End If

        cmbPricelist.SelectedItem = TmpBT.PricelistName
        If cmbPricelist.SelectedItem <> TmpBT.PricelistName Then
            cmbPricelist.SelectedIndex = -1
        End If
        GroupBox1.Enabled = True
    End Sub

    Sub UpdateGrid()
        Dim TmpBT As Trinity.cBookingType = cmbBookingType.SelectedItem

        While grdPricelist.Columns.Count > 5
            grdPricelist.Columns.RemoveAt(5)
        End While

        grdPricelist.Columns("colCPP").Tag = "-1"

        Dim TmpCol As System.Windows.Forms.DataGridViewTextBoxColumn
        For i As Integer = 0 To TmpBT.Dayparts.Count - 1
            TmpCol = New System.Windows.Forms.DataGridViewTextBoxColumn
            TmpCol.Name = "colCPP" & TmpBT.Dayparts(i).Name
            TmpCol.HeaderText = "CPP " & TmpBT.Dayparts(i).Name
            TmpCol.Tag = i
            grdPricelist.Columns.Add(TmpCol)
        Next

        cmbTarget.DisplayMember = "TargetName"
        cmbTarget.Items.Clear()
        For Each TmpTarget As Trinity.cPricelistTarget In TmpBT.Pricelist.Targets
            Dim style As New StyleableComboboxStyle
            If Not TmpTarget.IsUserEditable Then
                style.FontStyle = FontStyle.Bold
            End If
            cmbTarget.Items.Add(TmpTarget)
            cmbTarget.SetItemStyle(TmpTarget, style)
        Next
    End Sub


    Private Sub cmbTarget_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTarget.SelectedIndexChanged
        If cmbTarget.SelectedIndex = -1 Then Exit Sub
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem
        txtAdedgeTarget.Text = TmpTarget.Target.TargetName
        txtMaxRatings.Text = TmpTarget.MaxRatings
        If TmpTarget.Target.TargetType <> Trinity.cTarget.TargetTypeEnum.trgMnemonicTarget Then
            txtAdedgeTarget.ForeColor = Color.Gray
        Else
            txtAdedgeTarget.ForeColor = Color.Black
        End If
        chkCalcCPP.Checked = TmpTarget.CalcCPP
        chkStandard.Checked = TmpTarget.StandardTarget

        Dim TmpList As New SortedList(Of Date, Trinity.cPricelistPeriod)
        For Each TmpPeriod As Trinity.cPricelistPeriod In TmpTarget.PricelistPeriods
            Dim TmpKey As Date = TmpPeriod.FromDate
            While TmpList.ContainsKey(TmpKey)
                TmpKey = TmpKey.AddMilliseconds(1)
            End While
            TmpList.Add(TmpKey, TmpPeriod)
        Next

        grdIndexes.Rows.Clear()
        For Each Idx As Trinity.cIndex In TmpTarget.Indexes
            grdIndexes.Rows.Add()
            grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = Idx
        Next

        grdPricelist.Rows.Clear()
        For Each TmpPeriod As Trinity.cPricelistPeriod In TmpList.Values
            grdPricelist.Rows.Add()
            grdPricelist.Rows(grdPricelist.Rows.Count - 1).Tag = TmpPeriod
        Next

        'txtAdedgeTarget.Enabled = True
        'chkCalcCPP.Enabled = True
        'chkStandard.Enabled = True

        Dim strOldYear As String = cmbYear.SelectedItem
        If strOldYear Is Nothing Then
            strOldYear = "- All years -"
        End If
        cmbYear.Items.Clear()
        cmbYear.Items.Add("- All years -")

        Dim HighestYear As Integer
        For z As Integer = 0 To grdPricelist.Rows.Count - 1
            If Not cmbYear.Items.Contains(grdPricelist.Rows(z).Cells("colFrom").Value.year) Then
                cmbYear.Items.Add(grdPricelist.Rows(z).Cells("colFrom").Value.year)
                If grdPricelist.Rows(z).Cells("colFrom").Value.year > HighestYear Then
                    HighestYear = grdPricelist.Rows(z).Cells("colFrom").Value.year
                End If
            End If
        Next

        'add next year if not available
        Dim d As Date = Date.Now()
        If Not cmbYear.Items.Contains(d.AddYears(1).Year) Then
            cmbYear.Items.Add(d.AddYears(1).Year)
        End If
        If Not cmbYear.Items.Contains(HighestYear + 1) Then
            cmbYear.Items.Add(HighestYear + 1)
        End If

        Try
            If strOldYear = "" Then
                cmbYear.SelectedItem = d.Year
                cmbYear_SelectedIndexChanged(New Object, New System.EventArgs)
            Else
                cmbYear.Text = strOldYear
                cmbYear_SelectedIndexChanged(New Object, New System.EventArgs)
            End If
        Catch ex As Exception

        End Try

        chkCalcCPP_CheckedChanged(New Object, New System.EventArgs)

        If (Not TmpTarget.IsUserEditable) And (Not TrinitySettings.Developer) Then
            'protected mode
            cmdAdd.Enabled = False
            cmdAddIndex.Enabled = False
            cmdAddTarget.Enabled = False
            cmdCalculate.Enabled = False
            cmdCopy.Enabled = False
            cmdCopyIndex.Enabled = False
            cmdCopyTarget.Enabled = False
            cmdDeleteTarget.Enabled = False
            cmdDelIndex.Enabled = False
            cmdEditEnhancement.Enabled = False
            cmdIndexWizard.Enabled = False
            cmdRemove.Enabled = False
            'cmdSaveAll.Visible = False
            'cmdSaveToFile.Enabled = False
            'cmdSaveAllChannels.Visible = False
            'cmdSaveAllChannels.Enabled = False
            cmdSetPriceType.Enabled = False
            cmdWizard.Enabled = False
            'cmdUpdateAllUniverses.Visible = False

            chkCalcCPP.Enabled = False
            chkStandard.Enabled = False

            'txtAdedgeTarget.Enabled = False
            txtMaxRatings.Enabled = False

            grdPricelist.ReadOnly = True
            grdIndexes.ReadOnly = True

        Else
            'unprotected mode
            cmdAdd.Enabled = True
            cmdAddIndex.Enabled = True
            cmdAddTarget.Enabled = True
            cmdCalculate.Enabled = True
            cmdCopy.Enabled = True
            cmdCopyIndex.Enabled = True
            cmdCopyTarget.Enabled = True
            cmdDeleteTarget.Enabled = True
            cmdDelIndex.Enabled = True
            cmdEditEnhancement.Enabled = True
            cmdIndexWizard.Enabled = True
            cmdRemove.Enabled = True
            'cmdSaveAll.Visible = True
            'cmdSaveToFile.Enabled = True
            cmdSetPriceType.Enabled = True
            'cmdSaveAllChannels.Visible = True
            'cmdSaveAllChannels.Enabled = True
            cmdWizard.Enabled = True

            chkCalcCPP.Enabled = True
            chkStandard.Enabled = TrinitySettings.Developer

            txtAdedgeTarget.Enabled = True
            txtMaxRatings.Enabled = True

            grdPricelist.ReadOnly = False
            grdIndexes.ReadOnly = False

        End If

    End Sub

    Private Sub grdPricelist_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPricelist.CellEndEdit
        Saved = False
    End Sub

    Private Sub copyToEntireColumn(ByVal sender As Object, ByVal e As System.EventArgs)
        Stop
    End Sub

    Private Sub grdPricelist_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdPricelist.CellMouseClick, grdPricelist.CellMouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            intCol = e.ColumnIndex
            intRow = e.RowIndex
        End If
    End Sub

    Private Sub grdPricelist_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdPricelist.CellValueNeeded
        'if a variable is changed the grid also need to be changed
        Dim TmpPeriod As Trinity.cPricelistPeriod = grdPricelist.Rows(e.RowIndex).Tag
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem
        If TmpPeriod Is Nothing Or TmpTarget Is Nothing Then Exit Sub
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
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem

        If TmpTarget.IsUserEditable Then
            Try
                If e.ColumnIndex = 0 Then
                    TmpPeriod.FromDate = e.Value
                ElseIf e.ColumnIndex = 1 Then
                    TmpPeriod.ToDate = e.Value
                ElseIf grdPricelist.Columns(e.ColumnIndex).Name.StartsWith("colCPP") Then
                    If rdbCPP.Checked Then
                        TmpPeriod.Price(True, grdPricelist.Columns(e.ColumnIndex).Tag) = e.Value
                        TmpPeriod.PriceIsCPP = True
                    Else
                        TmpPeriod.Price(False, grdPricelist.Columns(e.ColumnIndex).Tag) = e.Value
                        TmpPeriod.PriceIsCPP = False

                    End If
                ElseIf grdPricelist.Columns(e.ColumnIndex).Name = "colUni" Then
                    TmpPeriod.TargetUni = e.Value
                ElseIf grdPricelist.Columns(e.ColumnIndex).Name = "colNatUni" Then
                    TmpPeriod.TargetNat = e.Value
                End If
            Catch ex As Exception
                Exit Sub
            End Try
        Else
            MessageBox.Show("Information in the currently selected pricelist cannot be changed - it is standard.")
        End If
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click

        Dim failList As New List(Of String)

        For Each TmpChan As Trinity.cChannel In _channels
            Try
                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        If TmpBT.BuyingTarget IsNot Nothing Then
                            If Not TmpBT.BuyingTarget.TargetName Is Nothing AndAlso Not TmpBT.BuyingTarget.TargetName = "" Then
                                If TmpBT.Pricelist.Targets IsNot Nothing Then

                                    'If TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName) Is Nothing Then
                                    '    TmpBT.BuyingTarget.TargetName = TmpBT.BuyingTarget.TargetName.Substring(0, 1) & " " & TmpBT.BuyingTarget.TargetName.Substring(1)
                                    'End If

                                    If Not TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName) Is Nothing Then
                                        Dim SaveIsEntered As Trinity.cPricelistTarget.EnteredEnum = TmpBT.BuyingTarget.IsEntered
                                        Select Case TmpBT.BuyingTarget.IsEntered
                                            Case Trinity.cPricelistTarget.EnteredEnum.eCPP
                                                TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).NetCPP = TmpBT.BuyingTarget.NetCPP
                                            Case Trinity.cPricelistTarget.EnteredEnum.eCPT
                                                TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).NetCPT = TmpBT.BuyingTarget.NetCPT
                                            Case Trinity.cPricelistTarget.EnteredEnum.eDiscount
                                                TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Discount = TmpBT.BuyingTarget.Discount
                                        End Select
                                        For Each _index As Trinity.cIndex In (From _idx As Trinity.cIndex In TmpBT.BuyingTarget.Indexes Where Not _idx.UseThis Select _idx)
                                            If TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName) IsNot Nothing AndAlso TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Indexes(_index.ID) IsNot Nothing Then
                                                TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Indexes(_index.ID).UseThis = False
                                                TmpBT.BuyingTarget.Indexes(_index.ID).UseThis = False
                                            End If
                                        Next
                                        TmpBT.BuyingTarget = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName)
                                        TmpBT.BuyingTarget.IsEntered = SaveIsEntered
                                        Select Case TmpBT.BuyingTarget.IsEntered
                                            Case Trinity.cPricelistTarget.EnteredEnum.eCPP
                                                TmpBT.BuyingTarget.NetCPP = TmpBT.BuyingTarget.NetCPP
                                            Case Trinity.cPricelistTarget.EnteredEnum.eCPT
                                                TmpBT.BuyingTarget.NetCPT = TmpBT.BuyingTarget.NetCPT
                                            Case Trinity.cPricelistTarget.EnteredEnum.eDiscount
                                                TmpBT.BuyingTarget.Discount = TmpBT.BuyingTarget.Discount
                                        End Select

                                    Else
                                        failList.Add("The pricelist for " & TmpBT.ToString & " does not contain the target " & TmpBT.BuyingTarget.TargetName & ". Could not update it")
                                    End If
                                End If
                            Else
                                failList.Add("The pricelist for " & TmpBT.ToString & " has no buyingtarget.")
                            End If
                        Else
                            failList.Add(TmpBT.ToString & " has no buying target.")
                        End If

                    End If
                Next
            Catch ex As Exception
                failList.Add(TmpChan.ChannelName & " has no booking types.")
            End Try
        Next

        If failList.Count > 0 Then
            Dim errMsg As String = "The following problems occurred while applying the pricelist to the campaign: "
            For Each item As String In failList
                errMsg &= item & vbNewLine
            Next
            MessageBox.Show(errMsg)
        End If
        If Campaign.Contract IsNot Nothing Then
            If MessageBox.Show("Your campaign is connected to a contract, would you like to apply contract on campaign again?", "T R I N I T Y", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.yes Then
                Campaign.Contract.ApplyToCampaign()
            End If 
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub


    Private Sub cmdAddTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddTarget.Click
        Saved = False
        Dim TmpBT As Trinity.cBookingType = cmbBookingType.SelectedItem
        If Not TmpBT.IsUserEditable Then
            Windows.Forms.MessageBox.Show("The booking type " & TmpBT.ToString & " is protected. Targets cannot be added or removed from it.")
            Exit Sub
        End If
        Dim TmpStr As String = InputBox("Name of target:", "T R I N I T Y", Nothing)
        If TmpStr Is Nothing Or TmpStr = "" Then Exit Sub

        cmbTarget.Items.Add(TmpBT.Pricelist.Targets.Add(TmpStr, TmpBT))
        TmpBT.Pricelist.Targets(TmpStr).Target.Universe = TmpBT.ParentChannel.BuyingUniverse
        cmbTarget.SelectedItem = TmpBT.Pricelist.Targets(TmpStr)

        Dim tmpPeriod As Trinity.cPricelistPeriod = TmpBT.Pricelist.Targets(TmpStr).PricelistPeriods.Add("All Year", Guid.NewGuid.ToString)
        tmpPeriod.FromDate = DateAdd(DateInterval.Day, 1 + Now.DayOfYear * -1, Now)
        tmpPeriod.ToDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Year, 1, tmpPeriod.FromDate))



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
                txtAdedgeTarget.Text = adedge

                Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem
                TmpTarget.Target.TargetType = Trinity.cTarget.TargetTypeEnum.trgMnemonicTarget
                TmpTarget.Target.TargetName = UCase(adedge)
                TmpTarget.Target.NoUniverseSize = False
                tmpPeriod.TargetNat = TmpTarget.Target.UniSizeTot
                tmpPeriod.TargetUni = TmpTarget.Target.UniSize
            End If
        End If

        cmbTarget_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    'Private Sub cmdSaveToFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    If DBReader.isLocal Then
    '        MsgBox("You are using a Local database and all changes you make will be lost when you connect back to the network.", MsgBoxStyle.Information, "FYI")
    '    End If

    '    If System.Windows.Forms.MessageBox.Show("This will overwrite the default pricelists that are saved" & vbCrLf & "on your system." & vbCrLf & vbCrLf & "Are you sure you want to proceed?" & vbCrLf & vbCrLf & "(To save the changes to this campaign only, click the 'Apply' button)", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question, Windows.Forms.MessageBoxDefaultButton.Button2) <> Windows.Forms.DialogResult.Yes Then
    '        Exit Sub
    '    End If
    '    If InputBox("This function is protected with a password." & vbCrLf & "Please enter the password required:", "T R I N I T Y") <> "orange2010" Then
    '        System.Windows.Forms.MessageBox.Show("Wrong password!", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
    '        Exit Sub
    '    End If
    '    Dim TmpBT As Trinity.cBookingType = cmbBookingType.SelectedItem
    '    If TmpBT.IsUserEditable Then
    '        Try
    '            Dim oldFileName As String = TrinitySettings.ActiveDataPath & Campaign.Area & "\Pricelists\" & TmpBT.ParentChannel.ChannelName & ".xml"
    '            Dim newDirectory As String = TrinitySettings.ActiveDataPath & Campaign.Area & "\Pricelists\" & Now.ToShortDateString & "\"
    '            Dim newFileName As String = newDirectory & Now.ToLongTimeString.Replace(":", "-") & " " & TmpBT.ParentChannel.ChannelName & ".xml"
    '            System.IO.Directory.CreateDirectory(newDirectory)
    '            System.IO.File.Copy(oldFileName, newFileName)
    '        Catch ex As Exception
    '            MessageBox.Show("Could not save backup")
    '        End Try

    '        TmpBT.Pricelist.Save(TrinitySettings.ActiveDataPath & Campaign.Area & "\Pricelists\", (TrinitySettings.ConnectionStringCommon <> ""))
    '    Else
    '        MessageBox.Show("This booking type is protected - it is standard. Changes will apply to your campaign only.")
    '    End If
    '    '        TmpBT.Pricelist.Save(TrinitySettings.ActiveDataPath & Campaign.Area & "\Pricelists\")
    '    Windows.Forms.MessageBox.Show("The pricelist was saved.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
    'End Sub

    Private Sub cmdDeleteTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteTarget.Click
        Saved = False
        Dim TmpBT As Trinity.cBookingType = cmbBookingType.SelectedItem

        TmpBT.Pricelist.Targets.Remove(cmbTarget.Text)
        cmbTarget.Items.RemoveAt(cmbTarget.SelectedIndex)
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Saved = False
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem
        Dim TmpPeriod As Trinity.cPricelistPeriod = TmpTarget.PricelistPeriods.Add("Pricelist period")

        'if we have visible pricerows we start the new row where the last ended
        Dim setDate As Boolean = False
        Dim i As Integer = grdPricelist.Rows.Count - 1
        While i > -1
            If grdPricelist.Rows(i).Visible Then
                TmpPeriod.FromDate = DirectCast(grdPricelist.Rows(i).Tag, Trinity.cPricelistPeriod).ToDate.AddDays(1)
                TmpPeriod.ToDate = TmpPeriod.FromDate
                TmpPeriod.PriceIsCPP = rdbCPP.Checked
                setDate = True
                Exit While
            End If
            i -= 1
        End While

        'if no visible rows...
        If Not setDate Then
            If IsNumeric(cmbYear.SelectedItem) Then
                TmpPeriod.FromDate = New Date(cmbYear.SelectedItem, 1, 1)
                TmpPeriod.ToDate = New Date(cmbYear.SelectedItem, 12, 31)
            Else
                TmpPeriod.FromDate = Now
                TmpPeriod.ToDate = Now
            End If
            TmpPeriod.PriceIsCPP = rdbCPP.Checked
        End If

        grdPricelist.Rows.Add()
        grdPricelist.Rows(grdPricelist.Rows.Count - 1).Tag = TmpPeriod
    End Sub

    Private Sub cmdRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
        Saved = False

        For Each TmpRow As Windows.Forms.DataGridViewRow In grdPricelist.SelectedRows
            If TmpRow.Visible Then
                Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem
                Dim TmpPeriod As Trinity.cPricelistPeriod = grdPricelist.Rows(TmpRow.Index).Tag
                TmpTarget.PricelistPeriods.Remove(TmpPeriod.ID)
                grdPricelist.Rows.Remove(grdPricelist.Rows(TmpRow.Index))
            End If
        Next

    End Sub

    Private Sub mnuWeekOnCPT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuWeekOnCPT.Click
        If Not IsNumeric(cmbYear.SelectedItem) Then
            MsgBox("The value " & cmbYear.SelectedItem & " is not a valid year", MsgBoxStyle.Exclamation, "Error")
            Exit Sub
        End If

        Dim d As Long
        Dim ID As String

        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem

        For d = Trinity.Helper.MondayOfWeek(cmbYear.SelectedItem, 1).ToOADate To Trinity.Helper.MondayOfWeek(cmbYear.SelectedItem, 52).AddDays(6).ToOADate Step 7
            ID = TmpTarget.PricelistPeriods.Add(DatePart(DateInterval.WeekOfYear, Date.FromOADate(d), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)).ID
            If Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 1).Year < CInt(cmbYear.SelectedItem) Then
                TmpTarget.PricelistPeriods(ID).FromDate = New Date(cmbYear.SelectedItem, 1, 1)
            Else
                TmpTarget.PricelistPeriods(ID).FromDate = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 1)
            End If
            TmpTarget.PricelistPeriods(ID).ToDate = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 7)
            TmpTarget.PricelistPeriods(ID).PriceIsCPP = False
        Next

        'update the view
        cmbTarget_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub mnuWeekOnCPP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuWeekOnCPP.Click
        If Not IsNumeric(cmbYear.SelectedItem) Then
            MsgBox("The value " & cmbYear.SelectedItem & " is not a valid year", MsgBoxStyle.Exclamation, "Error")
            Exit Sub
        End If

        Dim d As Long
        Dim ID As String

        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem

        For d = Trinity.Helper.MondayOfWeek(cmbYear.SelectedItem, 1).ToOADate To Trinity.Helper.MondayOfWeek(cmbYear.SelectedItem, 52).AddDays(6).ToOADate Step 7
            ID = TmpTarget.PricelistPeriods.Add(DatePart(DateInterval.WeekOfYear, Date.FromOADate(d), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)).ID
            If Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 1).Year < CInt(cmbYear.SelectedItem) Then
                TmpTarget.PricelistPeriods(ID).FromDate = New Date(cmbYear.SelectedItem, 1, 1)
            Else
                TmpTarget.PricelistPeriods(ID).FromDate = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 1)
            End If
            TmpTarget.PricelistPeriods(ID).ToDate = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 7)
            TmpTarget.PricelistPeriods(ID).PriceIsCPP = True
        Next

        'update the view
        cmbTarget_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub mnuMonthOnCPP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuMonthOnCPP.Click
        If Not IsNumeric(cmbYear.SelectedItem) Then
            MsgBox("The value " & cmbYear.SelectedItem & " is not a valid year", MsgBoxStyle.Exclamation, "Error")
            Exit Sub
        End If

        Dim ID As String
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem

        For i As Integer = 1 To 12
            ID = TmpTarget.PricelistPeriods.Add(i).ID
            TmpTarget.PricelistPeriods(ID).FromDate = New Date(cmbYear.SelectedItem, i, 1)
            TmpTarget.PricelistPeriods(ID).ToDate = Date.FromOADate(TmpTarget.PricelistPeriods(ID).FromDate.AddMonths(1).ToOADate - 1)
            TmpTarget.PricelistPeriods(ID).PriceIsCPP = True
        Next
        cmbTarget_SelectedIndexChanged(New Object, New EventArgs)
    End Sub


    Public Sub New(ByVal Channels As Trinity.cChannels)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _channels = Channels



    End Sub

    Private Sub cmdWizard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWizard.Click
        mnuWizard.Tag = ""
        Saved = False
        mnuWizard.Show(cmdWizard, New System.Drawing.Point(0, cmdWizard.Height))
    End Sub


    Sub CopyPricelist(ByVal sender As Object, ByVal e As EventArgs)
        Dim TmpBT As Trinity.cBookingType = cmbBookingType.SelectedItem
        If Windows.Forms.MessageBox.Show("This will replace the pricelist for " & cmbBookingType.Text & "." & vbCrLf & "Are you sure you want to proceed?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then Exit Sub
        TmpBT.Pricelist.Targets.Clear()

        Dim choices() As String = {"Pricelist + indexes", "Price list only", "Indexes only"}
        Dim choice As New frmMultipleChoice(choices, "Options")
        choice.ShowDialog()
        Dim hmm As Object = choice.Result()

        For Each TmpTarget As Trinity.cPricelistTarget In DirectCast(sender.tag, Trinity.cBookingType).Pricelist.Targets

           ' If TmpBT.ParentChannel.ChannelName = "TV3" Or TmpBT.ParentChannel.ChannelName = "TV6" Or TmpBT.ParentChannel.ChannelName = "TV8" Then
            '    If Not TmpTarget.TargetName.Substring(1, 1) = " " Then TmpTarget.TargetName = TmpTarget.TargetName.Substring(0, 1) & " " & TmpTarget.TargetName.Substring(1)
          '  End If


            With TmpBT.Pricelist.Targets.Add(TmpTarget.TargetName, TmpBT, TmpTarget.Target)

                .Bookingtype = TmpBT
                If hmm <> "Price list only" Then
                    For Each tmpIndex As Trinity.cIndex In TmpTarget.Indexes

                        With .Indexes.Add(tmpIndex.Name, tmpIndex.ID)
                            .FromDate = tmpIndex.FromDate
                            .ToDate = tmpIndex.ToDate
                            .Index = tmpIndex.Index
                            .IndexOn = tmpIndex.IndexOn
                            .Enhancements = tmpIndex.Enhancements
                            .UseThis = tmpIndex.UseThis
                        End With

                    Next
                End If
                .CalcCPP = TmpTarget.CalcCPP
                'For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                '    .DefaultDaypart(i) = TmpTarget.DefaultDaypart(i)
                'Next
                .NetCPP = TmpTarget.NetCPP
                .NetCPT = TmpTarget.NetCPT
                .Discount = TmpTarget.Discount
                .IsEntered = TmpTarget.IsEntered
                .CalcCPP = TmpTarget.CalcCPP

                For Each TmpPeriod As Trinity.cPricelistPeriod In TmpTarget.PricelistPeriods
                    Dim NewPeriod As Trinity.cPricelistPeriod = .PricelistPeriods.Add(TmpPeriod.Name)
                    NewPeriod.FromDate = TmpPeriod.FromDate
                    NewPeriod.PriceIsCPP = TmpPeriod.PriceIsCPP
                    NewPeriod.Price(TmpPeriod.PriceIsCPP) = TmpPeriod.Price(TmpPeriod.PriceIsCPP)
                    For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                        NewPeriod.Price(TmpPeriod.PriceIsCPP, i) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, i)
                    Next
                    NewPeriod.TargetNat = TmpPeriod.TargetNat
                    NewPeriod.TargetUni = TmpPeriod.TargetUni
                    NewPeriod.ToDate = TmpPeriod.ToDate
                Next
                .StandardTarget = TmpTarget.StandardTarget
            End With
        Next

        cmbChannel_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Sub CopyPricelistTarget(ByVal sender As Object, ByVal e As EventArgs)
        If Windows.Forms.MessageBox.Show("This will replace the pricelist for " & cmbTarget.Text & "." & vbCrLf & "Are you sure you want to proceed?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then Exit Sub
        Dim TmpBT As Trinity.cBookingType = cmbBookingType.SelectedItem
        Dim TmpTarget As Trinity.cPricelistTarget
        TmpTarget = DirectCast(sender.tag, Trinity.cPricelistTarget)
        Dim TmpT As Trinity.cPricelistTarget = cmbTarget.SelectedItem

        With TmpBT.Pricelist.Targets(TmpT.TargetName)
            .CalcCPP = TmpTarget.CalcCPP
            'For i As Integer = 0 To TmpBT.Dayparts.Count - 1
            '    .DefaultDaypart(i) = TmpTarget.DefaultDaypart(i)
            'Next
            .NetCPP = TmpTarget.NetCPP
            .NetCPT = TmpTarget.NetCPT
            .Discount = TmpTarget.Discount
            .IsEntered = TmpTarget.IsEntered

            'For Each TmpIndex As Trinity.cPricelistPeriod In TmpBT.Pricelist.Targets(TmpT.TargetName).PricelistPeriods
            '    If cmbYear.SelectedItem = "- All years -" OrElse (TmpIndex.FromDate.Year = CInt(cmbYear.SelectedItem) AndAlso TmpIndex.ToDate.Year = CInt(cmbYear.SelectedItem)) Then
            '        TmpBT.Pricelist.Targets(TmpT.TargetName).PricelistPeriods.Remove(TmpIndex.ID)
            '    End If
            'Next

            'For Each TmpIndex As Trinity.cPricelistPeriod In TmpTarget.PricelistPeriods
            '    If cmbYear.SelectedItem = "- All years -" OrElse (TmpIndex.FromDate.Year = CInt(cmbYear.SelectedItem) OrElse TmpIndex.ToDate.Year = CInt(cmbYear.SelectedItem)) Then
            '        Dim NewIndex As Trinity.cPricelistPeriod = .PricelistPeriods.Add(TmpIndex.Name)
            '        NewIndex.FromDate = TmpIndex.FromDate
            '        NewIndex.PriceIsCPP = TmpIndex.PriceIsCPP
            '        NewIndex.Price(TmpIndex.PriceIsCPP) = TmpIndex.Price(TmpIndex.PriceIsCPP)
            '        For i As Integer = 0 To TmpBT.Dayparts.Count - 1
            '            NewIndex.Price(TmpIndex.PriceIsCPP, i) = TmpIndex.Price(TmpIndex.PriceIsCPP, i)
            '        Next
            '        NewIndex.TargetNat = TmpIndex.TargetNat
            '        NewIndex.TargetUni = TmpIndex.TargetUni
            '        NewIndex.ToDate = TmpIndex.ToDate
            '    End If
            'Next
            .StandardTarget = TmpTarget.StandardTarget
        End With
        cmbChannel_SelectedIndexChanged(New Object, New EventArgs)
        cmbTarget.SelectedItem = TmpT
    End Sub


    Dim _progress As frmProgress
    Private Sub UpdateProgressBar(ByVal sender As Object, ByVal e As Trinity.PriceListEventArgs)

        If Me._progress Is Nothing Then Exit Sub

        ' Update the bar once step
        _progress.Progress = e.Progress
        Dim _tmpName As String = ""
        If (e.Target IsNot Nothing) Then
            _tmpName = e.Target.TargetName
        End If
        _progress.Status = String.Format("Updating pricelist " + e.BookingType.ToString)
        _progress.OverallStatus = String.Format("Updating channel " + e.BookingType.ParentChannel.ChannelName)

    End Sub

    Private Sub cmdUpdateChosenPricelist_Click(sender As Object, e As EventArgs) Handles cmdUpdateChosenPricelist.Click
        Dim chosenPricelist As String = cmbChannel.SelectedItem

        For i As Integer = 0 To Campaign.Channels.Count

        Next

        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            If chosenPricelist = TmpChan.ChannelName Then
                Me._progress = New frmProgress
                Me._progress.Show()
                Me._progress.BarType = cProgressBarType.SingleBar


                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                    Dim TmpNewBT As New Trinity.cBookingType(Campaign)
                    TmpNewBT.ParentChannel = TmpBT.ParentChannel
                    TmpNewBT.Name = TmpBT.Name
                    TmpNewBT.Dayparts = TmpBT.Dayparts
                    TmpNewBT.PricelistName = TmpBT.PricelistName

                    ' Set max Values
                    ' Me._progress.MaxValue = TmpNewBT.GetPricelistCount()
                    'AddHandler TmpNewBT.OnPriceListUpdate, AddressOf Me.UpdateProgressBar
                    '_progress.OverallMaxValue += 1
                    _progress.Progress += 1
                    _progress.Status = "Updating pricelist for " & TmpBT.ToString
                    TmpNewBT.ReadPricelist()

                    For Each TmpTarget As Trinity.cPricelistTarget In TmpBT.Pricelist.Targets
                        Dim TmpTarget2 As Trinity.cPricelistTarget = TmpNewBT.Pricelist.Targets(TmpTarget.TargetName)

                        If Not TmpTarget2 Is Nothing Then
                            TmpTarget.Target.NoUniverseSize = True
                            TmpTarget.Target.TargetName = TmpTarget2.Target.TargetName
                            TmpTarget.Target.TargetGroup = TmpTarget2.Target.TargetGroup
                            TmpTarget.Target.TargetType = TmpTarget2.Target.TargetType
                            TmpTarget.Target.NoUniverseSize = False
                            TmpTarget.CalcCPP = TmpTarget2.CalcCPP
                            TmpTarget.StandardTarget = TmpTarget2.StandardTarget
                            Select Case TmpTarget.IsEntered
                                Case Trinity.cPricelistTarget.EnteredEnum.eCPP
                                    TmpTarget.NetCPP = TmpTarget.NetCPP
                                Case Trinity.cPricelistTarget.EnteredEnum.eCPT
                                    TmpTarget.NetCPT = TmpTarget.NetCPT
                                Case Trinity.cPricelistTarget.EnteredEnum.eDiscount
                                    TmpTarget.Discount = TmpTarget.Discount
                            End Select
                        End If

                        TmpTarget.PricelistPeriods.Clear()
                        TmpTarget.Indexes.Clear()
                        If Not TmpTarget2 Is Nothing Then
                            For Each TmpPeriod As Trinity.cPricelistPeriod In TmpTarget2.PricelistPeriods
                                With TmpTarget.PricelistPeriods.Add(TmpPeriod.Name, TmpPeriod.ID)
                                    .FromDate = TmpPeriod.FromDate
                                    .ToDate = TmpPeriod.ToDate
                                    .PriceIsCPP = TmpPeriod.PriceIsCPP
                                    If TmpTarget2.CalcCPP Then
                                        For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                                            .Price(TmpPeriod.PriceIsCPP, i) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, i)
                                        Next
                                    Else
                                        .Price(TmpPeriod.PriceIsCPP) = TmpPeriod.Price(TmpPeriod.PriceIsCPP)
                                    End If
                                    .TargetNat = TmpPeriod.TargetNat
                                    .TargetUni = TmpPeriod.TargetUni
                                End With
                            Next
                            For Each TmpIndex As Trinity.cIndex In TmpTarget2.Indexes
                                With TmpTarget.Indexes.Add(TmpIndex.Name)
                                    .FromDate = TmpIndex.FromDate
                                    .ToDate = TmpIndex.ToDate
                                    .ID = TmpIndex.ID
                                    .IndexOn = TmpIndex.IndexOn
                                    .Index = TmpIndex.Index
                                    .UseThis = True
                                End With
                            Next
                        End If
                    Next

                    For Each TmpTarget2 As Trinity.cPricelistTarget In TmpNewBT.Pricelist.Targets
                        Try
                            If Not TmpBT.Pricelist.Targets.Contains(TmpTarget2.TargetName) Then
                                Dim TmpTarget As Trinity.cPricelistTarget = TmpBT.Pricelist.Targets.Add(TmpTarget2.TargetName, TmpBT)
                                If Not TmpTarget2 Is Nothing Then
                                    'TmpTarget.CPP = TmpTarget2.CPP
                                    'TmpTarget.UniSize = TmpTarget2.UniSize
                                    'TmpTarget.UniSizeNat = TmpTarget2.UniSizeNat
                                    TmpTarget.Bookingtype = TmpBT
                                    TmpTarget.Target.NoUniverseSize = True
                                    TmpTarget.Target.TargetName = TmpTarget2.Target.TargetName
                                    TmpTarget.Target.TargetGroup = TmpTarget2.Target.TargetGroup
                                    TmpTarget.Target.TargetType = TmpTarget2.Target.TargetType
                                    TmpTarget.Target.NoUniverseSize = False
                                    TmpTarget.CalcCPP = TmpTarget2.CalcCPP
                                    TmpTarget.StandardTarget = TmpTarget2.StandardTarget
                                    Select Case TmpTarget.IsEntered
                                        Case Trinity.cPricelistTarget.EnteredEnum.eCPP
                                            TmpTarget.NetCPP = TmpTarget.NetCPP
                                        Case Trinity.cPricelistTarget.EnteredEnum.eCPT
                                            TmpTarget.NetCPT = TmpTarget.NetCPT
                                        Case Trinity.cPricelistTarget.EnteredEnum.eDiscount
                                            TmpTarget.Discount = TmpTarget.Discount
                                    End Select
                                End If
                                TmpTarget.PricelistPeriods.Clear()
                                If Not TmpTarget2 Is Nothing Then
                                    For Each TmpPeriod As Trinity.cPricelistPeriod In TmpTarget2.PricelistPeriods
                                        With TmpTarget.PricelistPeriods.Add(TmpPeriod.Name, TmpPeriod.ID)

                                            .FromDate = TmpPeriod.FromDate
                                            .ToDate = TmpPeriod.ToDate
                                            .PriceIsCPP = TmpPeriod.PriceIsCPP
                                            If TmpTarget2.CalcCPP Then
                                                For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                                                    .Price(TmpPeriod.PriceIsCPP, i) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, i)
                                                Next
                                            Else
                                                .Price(TmpPeriod.PriceIsCPP) = TmpPeriod.Price(TmpPeriod.PriceIsCPP)
                                            End If
                                            .TargetNat = TmpPeriod.TargetNat
                                            .TargetUni = TmpPeriod.TargetUni
                                        End With
                                    Next
                                    For Each TmpIndex As Trinity.cIndex In TmpTarget2.Indexes
                                        With TmpTarget.Indexes.Add(TmpIndex.Name)
                                            .FromDate = TmpIndex.FromDate
                                            .ToDate = TmpIndex.ToDate
                                            .ID = TmpIndex.ID
                                            .IndexOn = TmpIndex.IndexOn
                                            .Index = TmpIndex.Index
                                            .UseThis = True
                                        End With
                                    Next
                                End If
                            End If
                        Catch ex As Exception
                            ' Stop
                        End Try
                    Next

                    If Not TmpBT.BuyingTarget Is Nothing AndAlso Not TmpBT.BuyingTarget.TargetName Is Nothing AndAlso TmpBT.BuyingTarget.TargetName <> "" Then
                        TmpBT.BuyingTarget.Target.NoUniverseSize = True
                        TmpBT.BuyingTarget.Target.TargetName = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Target.TargetName
                        TmpBT.BuyingTarget.Target.TargetGroup = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Target.TargetGroup
                        TmpBT.BuyingTarget.Target.TargetType = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Target.TargetType
                        TmpBT.BuyingTarget.Target.NoUniverseSize = False
                        TmpBT.BuyingTarget.CalcCPP = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).CalcCPP
                        TmpBT.BuyingTarget.StandardTarget = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).StandardTarget

                        Select Case TmpBT.BuyingTarget.IsEntered
                            Case Trinity.cPricelistTarget.EnteredEnum.eCPP
                                TmpBT.BuyingTarget.NetCPP = TmpBT.BuyingTarget.NetCPP
                            Case Trinity.cPricelistTarget.EnteredEnum.eCPT
                                TmpBT.BuyingTarget.NetCPT = TmpBT.BuyingTarget.NetCPT
                            Case Trinity.cPricelistTarget.EnteredEnum.eDiscount
                                TmpBT.BuyingTarget.Discount = TmpBT.BuyingTarget.Discount
                        End Select
                        If Not TmpBT.BuyingTarget Is TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName) Then
                            TmpBT.BuyingTarget.PricelistPeriods.Clear()
                            For Each TmpPeriod As Trinity.cPricelistPeriod In TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).PricelistPeriods
                                With TmpBT.BuyingTarget.PricelistPeriods.Add(TmpPeriod.Name, TmpPeriod.ID)
                                    .FromDate = TmpPeriod.FromDate
                                    .ToDate = TmpPeriod.ToDate
                                    .PriceIsCPP = TmpPeriod.PriceIsCPP
                                    If TmpBT.BuyingTarget.CalcCPP Then
                                        For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                                            .Price(TmpPeriod.PriceIsCPP, i) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, i)
                                        Next
                                    Else
                                        .Price(TmpPeriod.PriceIsCPP) = TmpPeriod.Price(TmpPeriod.PriceIsCPP)
                                    End If
                                    .TargetNat = TmpPeriod.TargetNat
                                    .TargetUni = TmpPeriod.TargetUni
                                End With
                            Next
                            Dim _notUsedIndexes As New List(Of Trinity.cIndex)
                            For Each _index As Trinity.cIndex In (From _idx As Trinity.cIndex In TmpBT.BuyingTarget.Indexes Where Not _idx.UseThis Select _idx)
                                _notUsedIndexes.Add(_index)
                            Next
                            TmpBT.BuyingTarget.Indexes.Clear()
                            For Each TmpIndex As Trinity.cIndex In TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Indexes
                                With TmpBT.BuyingTarget.Indexes.Add(TmpIndex.Name)
                                    .FromDate = TmpIndex.FromDate
                                    .ToDate = TmpIndex.ToDate
                                    .ID = TmpIndex.ID
                                    .IndexOn = TmpIndex.IndexOn
                                    .Index = TmpIndex.Index
                                    .UseThis = True
                                End With
                            Next
                            For Each _index As Trinity.cIndex In _notUsedIndexes
                                If TmpBT.BuyingTarget.Indexes(_index.ID) IsNot Nothing Then
                                    TmpBT.BuyingTarget.Indexes(_index.ID).UseThis = False
                                End If
                            Next
                        End If
                    End If

                    'RemoveHandler TmpNewBT.OnPriceListUpdate, AddressOf Me.UpdateProgressBar
                    'Me._progress.OverallAddOffset(TmpNewBT.GetPricelistCount())
                Next
                Me.Cursor = Windows.Forms.Cursors.Default
                cmbChannel_SelectedIndexChanged(New Object, New EventArgs)

                Me._progress.Hide()
                Me._progress.Dispose()



            End If
        Next

    End Sub
    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        Saved = False
        If MessageBox.Show("Your pricelist will be updated from the server. All changes will be lost." & vbCrLf & vbCrLf & "Are you sure you want to continue?", "T R I N I T Y", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        ' Sum the total amount of pricelist changes there are
        Dim totalCount As Integer = 0
        For Each TmpChan As Trinity.cChannel In _channels

            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                totalCount += 1
            Next
        Next

        Me._progress = New frmProgress
        Me._progress.MaxValue = totalCount
        Me._progress.Show()
        Me._progress.BarType = cProgressBarType.SingleBar

        For Each TmpChan As Trinity.cChannel In _channels

            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                Dim TmpNewBT As New Trinity.cBookingType(Campaign)
                TmpNewBT.ParentChannel = TmpBT.ParentChannel
                TmpNewBT.Name = TmpBT.Name
                TmpNewBT.Dayparts = TmpBT.Dayparts
                TmpNewBT.PricelistName = TmpBT.PricelistName

                ' Set max Values
                ' Me._progress.MaxValue = TmpNewBT.GetPricelistCount()
                'AddHandler TmpNewBT.OnPriceListUpdate, AddressOf Me.UpdateProgressBar
                '_progress.OverallMaxValue += 1
                _progress.Progress += 1
                _progress.Status = "Updating pricelist for " & TmpBT.ToString
                TmpNewBT.ReadPricelist()

                For Each TmpTarget As Trinity.cPricelistTarget In TmpBT.Pricelist.Targets
                    Dim TmpTarget2 As Trinity.cPricelistTarget = TmpNewBT.Pricelist.Targets(TmpTarget.TargetName)

                    If Not TmpTarget2 Is Nothing Then
                        TmpTarget.Target.NoUniverseSize = True
                        TmpTarget.Target.TargetName = TmpTarget2.Target.TargetName
                        TmpTarget.Target.TargetGroup = TmpTarget2.Target.TargetGroup
                        TmpTarget.Target.TargetType = TmpTarget2.Target.TargetType
                        TmpTarget.Target.NoUniverseSize = False
                        TmpTarget.CalcCPP = TmpTarget2.CalcCPP
                        TmpTarget.StandardTarget = TmpTarget2.StandardTarget
                        Select Case TmpTarget.IsEntered
                            Case Trinity.cPricelistTarget.EnteredEnum.eCPP
                                TmpTarget.NetCPP = TmpTarget.NetCPP
                            Case Trinity.cPricelistTarget.EnteredEnum.eCPT
                                TmpTarget.NetCPT = TmpTarget.NetCPT
                            Case Trinity.cPricelistTarget.EnteredEnum.eDiscount
                                TmpTarget.Discount = TmpTarget.Discount
                        End Select
                    End If

                    TmpTarget.PricelistPeriods.Clear()
                    TmpTarget.Indexes.Clear()
                    If Not TmpTarget2 Is Nothing Then
                        For Each TmpPeriod As Trinity.cPricelistPeriod In TmpTarget2.PricelistPeriods
                            With TmpTarget.PricelistPeriods.Add(TmpPeriod.Name, TmpPeriod.ID)
                                .FromDate = TmpPeriod.FromDate
                                .ToDate = TmpPeriod.ToDate
                                .PriceIsCPP = TmpPeriod.PriceIsCPP
                                If TmpTarget2.CalcCPP Then
                                    For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                                        .Price(TmpPeriod.PriceIsCPP, i) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, i)
                                    Next
                                Else
                                    .Price(TmpPeriod.PriceIsCPP) = TmpPeriod.Price(TmpPeriod.PriceIsCPP)
                                End If
                                .TargetNat = TmpPeriod.TargetNat
                                .TargetUni = TmpPeriod.TargetUni
                            End With
                        Next
                        For Each TmpIndex As Trinity.cIndex In TmpTarget2.Indexes
                            With TmpTarget.Indexes.Add(TmpIndex.Name)
                                .FromDate = TmpIndex.FromDate
                                .ToDate = TmpIndex.ToDate
                                .ID = TmpIndex.ID
                                .IndexOn = TmpIndex.IndexOn
                                .Index = TmpIndex.Index
                                .UseThis = True
                            End With
                        Next
                    End If
                Next

                For Each TmpTarget2 As Trinity.cPricelistTarget In TmpNewBT.Pricelist.Targets
                    Try
                        If Not TmpBT.Pricelist.Targets.Contains(TmpTarget2.TargetName) Then
                            Dim TmpTarget As Trinity.cPricelistTarget = TmpBT.Pricelist.Targets.Add(TmpTarget2.TargetName, TmpBT)
                            If Not TmpTarget2 Is Nothing Then
                                'TmpTarget.CPP = TmpTarget2.CPP
                                'TmpTarget.UniSize = TmpTarget2.UniSize
                                'TmpTarget.UniSizeNat = TmpTarget2.UniSizeNat
                                TmpTarget.Bookingtype = TmpBT
                                TmpTarget.Target.NoUniverseSize = True
                                TmpTarget.Target.TargetName = TmpTarget2.Target.TargetName
                                TmpTarget.Target.TargetGroup = TmpTarget2.Target.TargetGroup
                                TmpTarget.Target.TargetType = TmpTarget2.Target.TargetType
                                TmpTarget.Target.NoUniverseSize = False
                                TmpTarget.CalcCPP = TmpTarget2.CalcCPP
                                TmpTarget.StandardTarget = TmpTarget2.StandardTarget
                                Select Case TmpTarget.IsEntered
                                    Case Trinity.cPricelistTarget.EnteredEnum.eCPP
                                        TmpTarget.NetCPP = TmpTarget.NetCPP
                                    Case Trinity.cPricelistTarget.EnteredEnum.eCPT
                                        TmpTarget.NetCPT = TmpTarget.NetCPT
                                    Case Trinity.cPricelistTarget.EnteredEnum.eDiscount
                                        TmpTarget.Discount = TmpTarget.Discount
                                End Select
                            End If
                            TmpTarget.PricelistPeriods.Clear()
                            If Not TmpTarget2 Is Nothing Then
                                For Each TmpPeriod As Trinity.cPricelistPeriod In TmpTarget2.PricelistPeriods
                                    With TmpTarget.PricelistPeriods.Add(TmpPeriod.Name, TmpPeriod.ID)

                                        .FromDate = TmpPeriod.FromDate
                                        .ToDate = TmpPeriod.ToDate
                                        .PriceIsCPP = TmpPeriod.PriceIsCPP
                                        If TmpTarget2.CalcCPP Then
                                            For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                                                .Price(TmpPeriod.PriceIsCPP, i) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, i)
                                            Next
                                        Else
                                            .Price(TmpPeriod.PriceIsCPP) = TmpPeriod.Price(TmpPeriod.PriceIsCPP)
                                        End If
                                        .TargetNat = TmpPeriod.TargetNat
                                        .TargetUni = TmpPeriod.TargetUni
                                    End With
                                Next
                                For Each TmpIndex As Trinity.cIndex In TmpTarget2.Indexes
                                    With TmpTarget.Indexes.Add(TmpIndex.Name)
                                        .FromDate = TmpIndex.FromDate
                                        .ToDate = TmpIndex.ToDate
                                        .ID = TmpIndex.ID
                                        .IndexOn = TmpIndex.IndexOn
                                        .Index = TmpIndex.Index
                                        .UseThis = True
                                    End With
                                Next
                            End If
                        End If
                    Catch ex As Exception
                        ' Stop
                    End Try
                Next

                If Not TmpBT.BuyingTarget Is Nothing AndAlso Not TmpBT.BuyingTarget.TargetName Is Nothing AndAlso TmpBT.BuyingTarget.TargetName <> "" Then
                    TmpBT.BuyingTarget.Target.NoUniverseSize = True
                    TmpBT.BuyingTarget.Target.TargetName = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Target.TargetName
                    TmpBT.BuyingTarget.Target.TargetGroup = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Target.TargetGroup
                    TmpBT.BuyingTarget.Target.TargetType = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Target.TargetType
                    TmpBT.BuyingTarget.Target.NoUniverseSize = False
                    TmpBT.BuyingTarget.CalcCPP = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).CalcCPP
                    TmpBT.BuyingTarget.StandardTarget = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).StandardTarget

                    Select Case TmpBT.BuyingTarget.IsEntered
                        Case Trinity.cPricelistTarget.EnteredEnum.eCPP
                            TmpBT.BuyingTarget.NetCPP = TmpBT.BuyingTarget.NetCPP
                        Case Trinity.cPricelistTarget.EnteredEnum.eCPT
                            TmpBT.BuyingTarget.NetCPT = TmpBT.BuyingTarget.NetCPT
                        Case Trinity.cPricelistTarget.EnteredEnum.eDiscount
                            TmpBT.BuyingTarget.Discount = TmpBT.BuyingTarget.Discount
                    End Select
                    If Not TmpBT.BuyingTarget Is TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName) Then
                        TmpBT.BuyingTarget.PricelistPeriods.Clear()
                        For Each TmpPeriod As Trinity.cPricelistPeriod In TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).PricelistPeriods
                            With TmpBT.BuyingTarget.PricelistPeriods.Add(TmpPeriod.Name, TmpPeriod.ID)
                                .FromDate = TmpPeriod.FromDate
                                .ToDate = TmpPeriod.ToDate
                                .PriceIsCPP = TmpPeriod.PriceIsCPP
                                If TmpBT.BuyingTarget.CalcCPP Then
                                    For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                                        .Price(TmpPeriod.PriceIsCPP, i) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, i)
                                    Next
                                Else
                                    .Price(TmpPeriod.PriceIsCPP) = TmpPeriod.Price(TmpPeriod.PriceIsCPP)
                                End If
                                .TargetNat = TmpPeriod.TargetNat
                                .TargetUni = TmpPeriod.TargetUni
                            End With
                        Next
                        Dim _notUsedIndexes As New List(Of Trinity.cIndex)
                        For Each _index As Trinity.cIndex In (From _idx As Trinity.cIndex In TmpBT.BuyingTarget.Indexes Where Not _idx.UseThis Select _idx)
                            _notUsedIndexes.Add(_index)
                        Next
                        TmpBT.BuyingTarget.Indexes.Clear()
                        For Each TmpIndex As Trinity.cIndex In TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Indexes
                            If not TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Indexes.Exists(TmpIndex.ID)
                                With TmpBT.BuyingTarget.Indexes.Add(TmpIndex.Name)
                                    .FromDate = TmpIndex.FromDate
                                    .ToDate = TmpIndex.ToDate
                                    .ID = TmpIndex.ID
                                    .IndexOn = TmpIndex.IndexOn
                                    .Index = TmpIndex.Index
                                    .UseThis = True
                                End With
                            End If
                        Next
                        For Each _index As Trinity.cIndex In _notUsedIndexes
                            If TmpBT.BuyingTarget.Indexes(_index.ID) IsNot Nothing Then
                                TmpBT.BuyingTarget.Indexes(_index.ID).UseThis = False
                            End If
                        Next
                    End If
                End If

                'RemoveHandler TmpNewBT.OnPriceListUpdate, AddressOf Me.UpdateProgressBar
                'Me._progress.OverallAddOffset(TmpNewBT.GetPricelistCount())
            Next
        Next
        Me.Cursor = Windows.Forms.Cursors.Default
        cmbChannel_SelectedIndexChanged(New Object, New EventArgs)

        Me._progress.Hide()
        Me._progress.Dispose()

    End Sub


    Private Sub txtAdedgeTarget_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAdedgeTarget.KeyPress

    End Sub

    Private Sub txtAdedgeTarget_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAdedgeTarget.KeyUp
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem
        If txtAdedgeTarget.Text = TmpTarget.Target.TargetName Then Exit Sub
        Try
            TmpTarget.Target.TargetType = Trinity.cTarget.TargetTypeEnum.trgMnemonicTarget
            TmpTarget.Target.TargetName = UCase(txtAdedgeTarget.Text)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtAdedgeTarget_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdedgeTarget.TextChanged
        Saved = False
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem
        If TmpTarget Is Nothing Then Exit Sub
        If TmpTarget.Target.TargetType > 0 Then
            txtAdedgeTarget.ForeColor = Color.Gray
        Else
            txtAdedgeTarget.ForeColor = Color.Black
        End If
    End Sub


    Private Sub cmdCalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCalculate.Click
        Saved = False
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem
        TmpTarget.Target.NoUniverseSize = False
        Dim size As Single = TmpTarget.Target.UniSizeTot
        Dim size2 As Single = TmpTarget.Target.UniSize
        For Each TmpRow As Windows.Forms.DataGridViewRow In grdPricelist.SelectedRows
            If TmpRow.Visible = True Then
                TmpRow.Cells("colNatUni").Value = size
                TmpRow.Cells("colUni").Value = size2
            End If
        Next
    End Sub

    Private Sub chkCalcCPP_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCalcCPP.CheckedChanged
        Saved = False
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem
        If TmpTarget Is Nothing Then Exit Sub
        TmpTarget.CalcCPP = chkCalcCPP.Checked

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

    Private Sub chkStandard_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkStandard.CheckedChanged
        Saved = False
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem
        If TmpTarget Is Nothing Then Exit Sub
        TmpTarget.StandardTarget = chkStandard.Checked
    End Sub

    Private Sub cmdCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopy.Click
        Saved = False
        Dim mnuCopy As New Windows.Forms.ContextMenuStrip
        For Each TmpChan As Trinity.cChannel In _channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                If TmpBT.Pricelist.Targets.Count > 0 Then
                    With mnuCopy.Items.Add(TmpBT.ToString, Nothing, AddressOf CopyPricelist)
                        .Tag = TmpBT
                    End With
                End If
            Next
        Next
        mnuCopy.Show(cmdCopy, 0, cmdCopy.Height)
    End Sub

    Private Sub cmdCopyTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopyTarget.Click
        Saved = False
        Dim mnuCopyTarget As New Windows.Forms.ContextMenuStrip
        Dim TmpBT As Trinity.cBookingType = cmbBookingType.SelectedItem
        Dim TmpTargetChosen As Trinity.cPricelistTarget = cmbTarget.SelectedItem
        For Each TmpTarget As Trinity.cPricelistTarget In TmpBT.Pricelist.Targets
            If TmpTarget.TargetName <> TmpTargetChosen.TargetName Then
                With mnuCopyTarget.Items.Add(TmpTarget.TargetName, Nothing, AddressOf CopyPricelistTarget)
                    .Tag = TmpTarget
                End With
            End If
        Next
        mnuCopyTarget.Show(cmdCopyTarget, 0, cmdCopy.Height)
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

    Private Sub lblTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTarget.Click
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem
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

    Private Shadows Sub MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblTarget.MouseHover
        DirectCast(sender, Windows.Forms.Label).BorderStyle = Windows.Forms.BorderStyle.Fixed3D
    End Sub

    Private Shadows Sub MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblTarget.MouseLeave
        DirectCast(sender, Windows.Forms.Label).BorderStyle = Windows.Forms.BorderStyle.None
    End Sub

    Private Sub cmbYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbYear.SelectedIndexChanged
        For z As Integer = 0 To grdPricelist.Rows.Count - 1
            If cmbYear.SelectedItem.ToString = "- All years -" Then
                grdPricelist.Rows(z).Visible = True
            Else
                If DirectCast(grdPricelist.Rows(z).Cells("colFrom").Value, Date).Year.ToString = cmbYear.SelectedItem Then
                    grdPricelist.Rows(z).Visible = True
                Else
                    If DirectCast(grdPricelist.Rows(z).Cells("colFrom").Value, Date).Year.ToString < cmbYear.SelectedItem AndAlso DirectCast(grdPricelist.Rows(z).Cells("colTo").Value, Date).Year.ToString >= cmbYear.SelectedItem Then
                        grdPricelist.Rows(z).Visible = True
                    Else
                        grdPricelist.Rows(z).Visible = False
                    End If
                End If
            End If
        Next

        For z As Integer = 0 To grdIndexes.Rows.Count - 1
            If cmbYear.SelectedItem.ToString = "- All years -" Then
                grdIndexes.Rows(z).Visible = True
            Else
                If DirectCast(grdIndexes.Rows(z).Cells("CalendarColumn3").Value, Date).Year.ToString = cmbYear.SelectedItem Then
                    grdIndexes.Rows(z).Visible = True
                Else
                    If DirectCast(grdIndexes.Rows(z).Cells("CalendarColumn3").Value, Date).Year.ToString < cmbYear.SelectedItem AndAlso DirectCast(grdIndexes.Rows(z).Cells("CalendarColumn4").Value, Date).Year.ToString >= cmbYear.SelectedItem Then
                        grdIndexes.Rows(z).Visible = True
                    Else
                        grdIndexes.Rows(z).Visible = False
                    End If
                End If
            End If
        Next
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

    Private Sub mnuMonthOnCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMonthOnCPT.Click
        If Not IsNumeric(cmbYear.SelectedItem) Then
            MsgBox("The value " & cmbYear.SelectedItem & " is not a valid year", MsgBoxStyle.Exclamation, "Error")
            Exit Sub
        End If
        Dim ID As String
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem

        For i As Integer = 1 To 12
            ID = TmpTarget.PricelistPeriods.Add(i).ID
            TmpTarget.PricelistPeriods(ID).FromDate = New Date(cmbYear.SelectedItem, i, 1)
            TmpTarget.PricelistPeriods(ID).ToDate = Date.FromOADate(TmpTarget.PricelistPeriods(ID).FromDate.AddMonths(1).ToOADate - 1)
            TmpTarget.PricelistPeriods(ID).PriceIsCPP = False
        Next
        cmbTarget_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub mnuItemCopyColumn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuItemCopyColumn.Click
        For Each row As System.Windows.Forms.DataGridViewRow In grdPricelist.Rows
            If Not row.Index = intRow Then
                row.Cells(intCol).Value = grdPricelist.Rows(intRow).Cells(intCol).Value
            End If
        Next
    End Sub

    Private Sub cmdAddIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddIndex.Click
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem
        If TmpTarget Is Nothing Then Exit Sub

        Dim idx As Trinity.cIndex = TmpTarget.Indexes.Add("")
        If Campaign.StartDate = 0 Then
            idx.FromDate = Date.Now
            idx.ToDate = Date.Now
        Else
            idx.FromDate = Date.FromOADate(Campaign.StartDate)
            idx.ToDate = Date.FromOADate(Campaign.EndDate)
        End If


        Dim i As Integer = grdIndexes.Rows.Add
        grdIndexes.Rows(i).Tag = idx
    End Sub

    Private Sub cmdDelIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelIndex.Click
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem
        If TmpTarget Is Nothing Then Exit Sub

        For Each indexRow As DataGridViewRow In grdIndexes.SelectedRows
            TmpTarget.Indexes.Remove(indexRow.Tag.ID)
            grdIndexes.Rows.Remove(indexRow)
        Next
        'TmpTarget.Indexes.Remove(grdIndexes.SelectedRows(0).Tag.ID)
        'grdIndexes.Rows.Remove(grdIndexes.SelectedRows(0))
    End Sub

    Private Sub grdIndexes_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdIndexes.CellValueNeeded
        Dim TmpIdx As Trinity.cIndex = grdIndexes.Rows(e.RowIndex).Tag
        If TmpIdx Is Nothing Then Exit Sub


        Select Case e.ColumnIndex
            Case Is = 0
                e.Value = TmpIdx.Name
            Case Is = 1
                If TmpIdx.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
                    e.Value = "Gross CPP"
                ElseIf TmpIdx.IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP Then
                    e.Value = "Net CPP"
                ElseIf TmpIdx.IndexOn = Trinity.cIndex.IndexOnEnum.eTRP Then
                    e.Value = "TRP"
                End If
            Case Is = 2
                e.Value = TmpIdx.FromDate
            Case Is = 3
                e.Value = TmpIdx.ToDate
            Case Is = 4
                e.Value = TmpIdx.Index
        End Select

    End Sub

    Private Sub grdIndexes_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdIndexes.CellValuePushed
        Dim TmpIdx As Trinity.cIndex = grdIndexes.Rows(e.RowIndex).Tag
        If TmpIdx Is Nothing Then Exit Sub

        If DirectCast(cmbTarget.SelectedItem, Trinity.cPricelistTarget).IsUserEditable Then

            Select Case e.ColumnIndex
                Case Is = 0
                    TmpIdx.Name = e.Value
                Case Is = 1
                    If e.Value = "Gross CPP" Then
                        TmpIdx.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP
                    ElseIf e.Value = "Net CPP" Then
                        TmpIdx.IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP
                    ElseIf e.Value = "TRP" Then
                        TmpIdx.IndexOn = Trinity.cIndex.IndexOnEnum.eTRP
                    End If
                Case Is = 2
                    TmpIdx.FromDate = e.Value
                Case Is = 3
                    TmpIdx.ToDate = e.Value
                Case Is = 4
                    TmpIdx.Index = e.Value
            End Select
        Else
            MessageBox.Show("This target is standard and it's not possible to edit its indices.")
        End If
    End Sub

    Private Sub cmdEnhancements_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEnhancements.Click
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem
        If TmpTarget Is Nothing Then Exit Sub

        frmEnhancements.grdEnhancements.Rows.Clear()
        If frmEnhancements.ShowDialog <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        Dim TmpIndex As Trinity.cIndex = TmpTarget.Indexes.Add("")
        If Campaign.StartDate = 0 Then
            TmpIndex.FromDate = Date.Now
            TmpIndex.ToDate = Date.Now
        Else
            TmpIndex.FromDate = Date.FromOADate(Campaign.StartDate)
            TmpIndex.ToDate = Date.FromOADate(Campaign.EndDate)
        End If

        TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eTRP

        For Each TmpRow As DataGridViewRow In frmEnhancements.grdEnhancements.Rows
            With TmpIndex.Enhancements.Add()
                .Amount = TmpRow.Cells(1).Value
                .Name = TmpRow.Cells(0).Value
            End With
            'TmpIndex.Enhancements.SpecificFactor = frmEnhancements.txtSpecFactor.Text / 100
        Next

        Dim i As Integer = grdIndexes.Rows.Add
        grdIndexes.Rows(i).Tag = TmpIndex
    End Sub

    Private Sub cmdEditEnhancement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditEnhancement.Click
        Dim TmpIndex As Trinity.cIndex = grdIndexes.SelectedRows.Item(0).Tag
        If TmpIndex Is Nothing Then Exit Sub

        frmEnhancements.grdEnhancements.Rows.Clear()
        For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
            With frmEnhancements.grdEnhancements.Rows(frmEnhancements.grdEnhancements.Rows.Add())
                .Cells(0).Value = TmpEnh.Name
                .Cells(1).Value = TmpEnh.Amount
            End With
        Next
        frmEnhancements.grdEnhancements.Columns(2).Visible = False
        'frmEnhancements.txtSpecFactor.Text = TmpIndex.Enhancements.SpecificFactor * 100
        If frmEnhancements.ShowDialog() <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If
        TmpIndex.Enhancements.Clear()
        For Each TmpRow As DataGridViewRow In frmEnhancements.grdEnhancements.Rows
            With TmpIndex.Enhancements.Add()
                .Amount = TmpRow.Cells(1).Value
                .Name = TmpRow.Cells(0).Value
            End With
        Next
        'TmpIndex.Enhancements.SpecificFactor = frmEnhancements.txtSpecFactor.Text / 100
        grdIndexes.Invalidate()
    End Sub

    Private Sub cmdCopyIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopyIndex.Click
        Dim mnuCopy As New ContextMenuStrip
        Dim bolChannel As Boolean
        Dim bolBT As Boolean
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            bolChannel = False
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                bolBT = False
                For Each TmpTarget As Trinity.cPricelistTarget In TmpBT.Pricelist.Targets
                    If TmpTarget.Indexes.Count > 0 Then
                        If Not bolChannel Then
                            With mnuCopy.Items.Add(TmpChan.ChannelName)
                                .Name = TmpChan.ChannelName
                            End With
                            bolChannel = True
                        End If
                        If Not bolBT Then
                            With DirectCast(mnuCopy.Items(TmpChan.ChannelName), ToolStripMenuItem).DropDownItems.Add(TmpBT.Name)
                                .Name = TmpBT.Name
                            End With
                            bolBT = True
                        End If
                        DirectCast(DirectCast(mnuCopy.Items(TmpChan.ChannelName), ToolStripMenuItem).DropDownItems(TmpBT.Name), ToolStripMenuItem).DropDownItems.Add(TmpTarget.TargetName, Nothing, AddressOf CopyIndex).Tag = TmpTarget
                    End If
                Next
            Next
        Next
        mnuCopy.Show(cmdCopyIndex, New System.Drawing.Point(0, cmdCopyIndex.Height))
    End Sub

    Private Sub CopyIndex(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim TmpPT As Trinity.cPricelistTarget = sender.tag
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem
        If TmpTarget Is Nothing Then Exit Sub
        If TmpPT Is Nothing Then Exit Sub

        Dim TmpIdx As Trinity.cIndex
        Dim i As Integer

        For Each TmpIndex As Trinity.cIndex In TmpPT.Indexes
            TmpIdx = TmpTarget.Indexes.Add(TmpIndex.Name)
            TmpIdx.FromDate = TmpIndex.FromDate
            TmpIdx.ToDate = TmpIndex.ToDate
            TmpIdx.IndexOn = TmpIndex.IndexOn
            If TmpIndex.Enhancements.Count > 0 Then
                For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
                    With TmpIdx.Enhancements.Add()
                        .Name = TmpEnh.Name
                        .Amount = TmpEnh.Amount
                    End With
                Next
                'TmpIdx.Enhancements.SpecificFactor = TmpIndex.Enhancements.SpecificFactor
            Else
                TmpIdx.Index = TmpIndex.Index
            End If

            i = grdIndexes.Rows.Add()
            grdIndexes.Rows(i).Tag = TmpIdx
        Next
    End Sub

    Private Sub cmdIndexWizard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdIndexWizard.Click
        Saved = False
        mnuWizard.Tag = "INDEX"
        mnuWizard.Show(cmdWizard, New System.Drawing.Point(0, cmdWizard.Height))
    End Sub

    Private Sub mnuWizard_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles mnuWizard.Opening
        If mnuWizard.Tag = "" Then
            mnuMonthOnCPP.Visible = True
            mnuMonthOnCPT.Visible = True
            mnuWeekOnCPP.Visible = True
            mnuWeekOnCPT.Visible = True
        Else
            mnuMonthOnCPP.Visible = False
            mnuMonthOnCPT.Visible = False
            mnuWeekOnCPP.Visible = False
            mnuWeekOnCPT.Visible = False
        End If
    End Sub

    Private Sub CreateWeeklyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateWeeklyToolStripMenuItem.Click
        If mnuWizard.Tag = "INDEX" Then
            If Not IsNumeric(cmbYear.SelectedItem) Then
                MsgBox("The value " & cmbYear.SelectedItem & " is not a valid year", MsgBoxStyle.Exclamation, "Error")
                Exit Sub
            End If

            Dim d As Long
            Dim ID As String

            Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem

            For d = Trinity.Helper.MondayOfWeek(cmbYear.SelectedItem, 1).ToOADate To Trinity.Helper.MondayOfWeek(cmbYear.SelectedItem, 52).AddDays(6).ToOADate Step 7
                ID = TmpTarget.Indexes.Add(DatePart(DateInterval.WeekOfYear, Date.FromOADate(d), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)).ID
                If Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 1).Year < CInt(cmbYear.SelectedItem) Then
                    TmpTarget.Indexes(ID).FromDate = New Date(cmbYear.SelectedItem, 1, 1)
                Else
                    TmpTarget.Indexes(ID).FromDate = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 1)
                End If
                TmpTarget.Indexes(ID).ToDate = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 7)
                TmpTarget.Indexes(ID).IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP
                TmpTarget.Indexes(ID).Index = 100
            Next

            'update the view
            cmbTarget_SelectedIndexChanged(New Object, New EventArgs)
        End If
    End Sub

    Private Sub mnuMonth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMonth.Click
        If mnuWizard.Tag = "INDEX" Then
            If Not IsNumeric(cmbYear.SelectedItem) Then
                MsgBox("The value " & cmbYear.SelectedItem & " is not a valid year", MsgBoxStyle.Exclamation, "Error")
                Exit Sub
            End If

            Dim ID As String

            Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem

            For i As Integer = 1 To 12
                ID = TmpTarget.Indexes.Add(i).ID
                TmpTarget.Indexes(ID).FromDate = New Date(cmbYear.SelectedItem, i, 1)
                TmpTarget.Indexes(ID).ToDate = Date.FromOADate(TmpTarget.Indexes(ID).FromDate.AddMonths(1).ToOADate - 1)
                TmpTarget.Indexes(ID).IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP
                TmpTarget.Indexes(ID).Index = 100
            Next

            'update the view
            cmbTarget_SelectedIndexChanged(New Object, New EventArgs)
        End If
    End Sub

    'Private Sub cmdSaveAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If DBReader.isLocal Then
    '        MsgBox("You are using a Local database and all changes you make will be lost when you connect back to the network.", MsgBoxStyle.Information, "FYI")
    '    End If

    '    If System.Windows.Forms.MessageBox.Show("This will overwrite the default pricelists that are saved" & vbCrLf & "on your system." & vbCrLf & vbCrLf & "Are you sure you wnat to proceed?" & vbCrLf & vbCrLf & "(To save the changes to this campaign only, click the 'Apply' button)", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question, Windows.Forms.MessageBoxDefaultButton.Button2) <> Windows.Forms.DialogResult.Yes Then
    '        Exit Sub
    '    End If
    '    If InputBox("This function is protected with a password." & vbCrLf & "Please enter the password required:", "T R I N I T Y") <> "orange2010" Then
    '        System.Windows.Forms.MessageBox.Show("Wrong password!", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
    '        Exit Sub
    '    End If

    '    Dim failList As New List(Of String)
    '    For Each TmpBT As Trinity.cBookingType In cmbBookingType.Items
    '        ' If TmpBT.IsUserEditable Then

    '        Dim oldFileName As String = TrinitySettings.ActiveDataPath & Campaign.Area & "\Pricelists\" & TmpBT.ParentChannel.ChannelName & ".xml"
    '        Dim newDirectory As String = TrinitySettings.ActiveDataPath & Campaign.Area & "\Pricelists\" & Now.ToShortDateString & "\"
    '        Dim newFileName As String = newDirectory & Now.ToLongTimeString.Replace(":", "-") & " " & TmpBT.ParentChannel.ChannelName & ".xml"
    '        System.IO.Directory.CreateDirectory(newDirectory)
    '        If IO.File.Exists(oldFileName) Then System.IO.File.Copy(oldFileName, newFileName)

    '        TmpBT.Pricelist.Save(IO.Path.Combine(TrinitySettings.ActiveDataPath & Campaign.Area & "\Pricelists\", TmpBT.ParentChannel.ChannelName & ".xml"), (TrinitySettings.ConnectionStringCommon <> ""))
    '        ' Else
    '        '  failList.Add(TmpBT.ToString & " ")
    '        ' End If
    '    Next
    '    Windows.Forms.MessageBox.Show("All booking types were saved for this channel except the protected booking types " & String.Join(",", failList.ToArray), "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
    'End Sub


    Private Sub SameDateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SameDateToolStripMenuItem.Click
        Dim TmpBT As Trinity.cBookingType = cmbBookingType.SelectedItem
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem
        If mnuWizard.Tag = "" Then
            For Each TmpRow As Windows.Forms.DataGridViewRow In grdPricelist.SelectedRows
                Dim TmpPeriod As Trinity.cPricelistPeriod = TmpRow.Tag
                With TmpTarget.PricelistPeriods.Add(TmpPeriod.Name)
                    .TargetNat = TmpPeriod.TargetNat
                    .TargetUni = TmpPeriod.TargetUni
                    .FromDate = TmpPeriod.FromDate.AddYears(1)
                    .ToDate = TmpPeriod.ToDate.AddYears(1)
                    .PriceIsCPP = TmpPeriod.PriceIsCPP
                    .Price(TmpPeriod.PriceIsCPP) = TmpPeriod.Price(TmpPeriod.PriceIsCPP)
                    For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                        .Price(TmpPeriod.PriceIsCPP, i) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, i)
                    Next
                    grdPricelist.Rows.Add()
                    grdPricelist.Rows(grdPricelist.Rows.Count - 1).Tag = TmpTarget.PricelistPeriods(.ID)
                End With
            Next
        Else
            For Each TmpRow As Windows.Forms.DataGridViewRow In grdIndexes.SelectedRows
                Dim TmpIndex As Trinity.cIndex = TmpRow.Tag
                With TmpTarget.Indexes.Add(TmpIndex.Name)
                    If .Enhancements.Count > 0 Then
                        For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
                            With .Enhancements.Add
                                .Amount = TmpEnh.Amount
                                .Name = TmpEnh.Name
                            End With
                        Next
                    End If
                    For d As Integer = 0 To TmpBT.Dayparts.Count - 1
                        .Index(d) = TmpIndex.Index(d)
                    Next
                    .IndexOn = TmpIndex.IndexOn
                    .SystemGenerated = TmpIndex.SystemGenerated
                    .FromDate = TmpIndex.FromDate.AddYears(1)
                    .ToDate = TmpIndex.ToDate.AddYears(1)
                    grdIndexes.Rows.Add()
                    grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = TmpTarget.Indexes(.ID)
                End With
            Next
        End If
    End Sub

    Private Sub SameDayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SameDayToolStripMenuItem.Click
        Dim TmpBT As Trinity.cBookingType = cmbBookingType.SelectedItem
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem
        If mnuWizard.Tag = "" Then
            For Each TmpRow As Windows.Forms.DataGridViewRow In grdPricelist.SelectedRows
                Dim TmpPeriod As Trinity.cPricelistPeriod = TmpRow.Tag
                With TmpTarget.PricelistPeriods.Add(TmpPeriod.Name)
                    .TargetNat = TmpPeriod.TargetNat
                    .TargetUni = TmpPeriod.TargetUni
                    Dim TmpDate As Long = TmpPeriod.FromDate.AddYears(1).ToOADate
                    Dim DateDiff As Long

                    While Weekday(Date.FromOADate(TmpDate), FirstDayOfWeek.Monday) <> Weekday(TmpPeriod.FromDate, FirstDayOfWeek.Monday)
                        TmpDate = TmpDate - 1
                    End While
                    DateDiff = TmpDate - TmpPeriod.FromDate.ToOADate

                    .FromDate = TmpPeriod.FromDate.AddDays(DateDiff)
                    .ToDate = TmpPeriod.ToDate.AddDays(DateDiff)
                    .PriceIsCPP = TmpPeriod.PriceIsCPP
                    .Price(TmpPeriod.PriceIsCPP) = TmpPeriod.Price(TmpPeriod.PriceIsCPP)
                    For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                        .Price(TmpPeriod.PriceIsCPP, i) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, i)
                    Next
                    grdPricelist.Rows.Add()
                    grdPricelist.Rows(grdPricelist.Rows.Count - 1).Tag = TmpTarget.PricelistPeriods(.ID)
                End With
            Next
        Else
            For Each TmpRow As Windows.Forms.DataGridViewRow In grdIndexes.SelectedRows
                Dim TmpIndex As Trinity.cIndex = TmpRow.Tag
                With TmpTarget.Indexes.Add(TmpIndex.Name)
                    If .Enhancements.Count > 0 Then
                        For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
                            With .Enhancements.Add
                                .Amount = TmpEnh.Amount
                                .Name = TmpEnh.Name
                            End With
                        Next
                    End If
                    For d As Integer = 0 To TmpBT.Dayparts.Count - 1
                        .Index(d) = TmpIndex.Index(d)
                    Next
                    .IndexOn = TmpIndex.IndexOn
                    .SystemGenerated = TmpIndex.SystemGenerated
                    Dim TmpDate As Long = TmpIndex.FromDate.AddYears(1).ToOADate
                    Dim DateDiff As Long

                    While Weekday(Date.FromOADate(TmpDate), FirstDayOfWeek.Monday) <> Weekday(TmpIndex.FromDate, FirstDayOfWeek.Monday)
                        TmpDate = TmpDate - 1
                    End While
                    DateDiff = TmpDate - TmpIndex.FromDate.ToOADate

                    .FromDate = TmpIndex.FromDate.AddDays(DateDiff)
                    .ToDate = TmpIndex.ToDate.AddDays(DateDiff)
                    grdIndexes.Rows.Add()
                    grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = TmpTarget.Indexes(.ID)
                End With
            Next
        End If
    End Sub

    Private Sub AllTargetsSameDateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllTargetsSameDateToolStripMenuItem.Click
        Dim TmpBT As Trinity.cBookingType = cmbBookingType.SelectedItem
        If mnuWizard.Tag = "" Then
            For Each TmpTarget As Trinity.cPricelistTarget In cmbTarget.Items
                For Each TmpRow As Windows.Forms.DataGridViewRow In grdPricelist.SelectedRows
                    Dim TmpSourcePeriod As Trinity.cPricelistPeriod = TmpRow.Tag
                    Dim TmpPeriod As Trinity.cPricelistPeriod = Nothing
                    For Each TmpPer As Trinity.cPricelistPeriod In TmpTarget.PricelistPeriods
                        If TmpPer.FromDate = TmpSourcePeriod.FromDate AndAlso TmpPer.ToDate = TmpSourcePeriod.ToDate Then
                            TmpPeriod = TmpPer
                            Exit For
                        End If
                    Next
                    If Not TmpPeriod Is Nothing Then
                        With TmpTarget.PricelistPeriods.Add(TmpPeriod.Name)
                            .TargetNat = TmpPeriod.TargetNat
                            .TargetUni = TmpPeriod.TargetUni
                            .FromDate = TmpPeriod.FromDate.AddYears(1)
                            .ToDate = TmpPeriod.ToDate.AddYears(1)
                            .PriceIsCPP = TmpPeriod.PriceIsCPP
                            .Price(TmpPeriod.PriceIsCPP) = TmpPeriod.Price(TmpPeriod.PriceIsCPP)
                            For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                                .Price(TmpPeriod.PriceIsCPP, i) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, i)
                            Next
                            If TmpTarget Is cmbTarget.SelectedItem Then
                                grdPricelist.Rows.Add()
                                grdPricelist.Rows(grdPricelist.Rows.Count - 1).Tag = TmpTarget.PricelistPeriods(.ID)
                            End If
                        End With
                    End If
                Next
            Next
        Else
            For Each TmpTarget As Trinity.cPricelistTarget In cmbTarget.Items
                For Each TmpRow As Windows.Forms.DataGridViewRow In grdIndexes.SelectedRows
                    Dim TmpSourceIndex As Trinity.cIndex = TmpRow.Tag
                    Dim TmpIndex As Trinity.cIndex = Nothing
                    For Each TmpInd As Trinity.cIndex In TmpTarget.Indexes
                        If TmpInd.FromDate = TmpSourceIndex.FromDate AndAlso TmpInd.ToDate = TmpSourceIndex.ToDate Then
                            TmpIndex = TmpInd
                            Exit For
                        End If
                    Next
                    If Not TmpIndex Is Nothing Then
                        With TmpTarget.Indexes.Add(TmpIndex.Name)
                            If .Enhancements.Count > 0 Then
                                For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
                                    With .Enhancements.Add
                                        .Amount = TmpEnh.Amount
                                        .Name = TmpEnh.Name
                                    End With
                                Next
                            End If
                            For d As Integer = 0 To TmpBT.Dayparts.Count - 1
                                .Index(d) = TmpIndex.Index(d)
                            Next
                            .IndexOn = TmpIndex.IndexOn
                            .SystemGenerated = TmpIndex.SystemGenerated
                            .FromDate = TmpIndex.FromDate.AddYears(1)
                            .ToDate = TmpIndex.ToDate.AddYears(1)
                            If TmpTarget Is cmbTarget.SelectedItem Then
                                grdIndexes.Rows.Add()
                                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = TmpTarget.Indexes(.ID)
                            End If
                        End With
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub AllTargetsSameDayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllTargetsSameDayToolStripMenuItem.Click
        'Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem

        Dim valueDictionary As New Dictionary(Of String, Single)
        Dim valueDictionary2 As New Dictionary(Of String, Single)
        Dim TmpBT As Trinity.cBookingType = cmbBookingType.SelectedItem

        If mnuWizard.Tag = "" Then
            For Each TmpTarget As Trinity.cPricelistTarget In cmbTarget.Items
                For Each TmpRow As Windows.Forms.DataGridViewRow In grdPricelist.SelectedRows
                    Dim TmpSourcePeriod As Trinity.cPricelistPeriod = TmpRow.Tag
                    Dim TmpPeriod As Trinity.cPricelistPeriod = Nothing
                    For Each TmpPer As Trinity.cPricelistPeriod In TmpTarget.PricelistPeriods
                        If TmpPer.FromDate = TmpSourcePeriod.FromDate AndAlso TmpPer.ToDate = TmpSourcePeriod.ToDate Then
                            TmpPeriod = TmpPer
                            Exit For
                        End If
                    Next
                    If Not TmpPeriod Is Nothing Then
                        With TmpTarget.PricelistPeriods.Add(TmpPeriod.Name)

                            Try
                                .TargetNat = valueDictionary(TmpTarget.TargetName)
                                .TargetUni = valueDictionary(TmpTarget.TargetName)
                            Catch
                                valueDictionary.Add(TmpTarget.TargetName, TmpTarget.Target.UniSizeTot)
                                valueDictionary2.Add(TmpTarget.TargetName, TmpTarget.Target.UniSize)
                                .TargetNat = valueDictionary(TmpTarget.TargetName)
                                .TargetUni = valueDictionary2(TmpTarget.TargetName)
                            End Try

                            '.TargetNat = TmpPeriod.TargetNat
                            '.TargetUni = TmpPeriod.TargetUni
                            Dim TmpDate As Long = TmpPeriod.FromDate.AddYears(1).ToOADate
                            Dim DateDiff As Long

                            While Weekday(Date.FromOADate(TmpDate), FirstDayOfWeek.Monday) <> Weekday(TmpPeriod.FromDate, FirstDayOfWeek.Monday)
                                TmpDate = TmpDate - 1
                            End While
                            DateDiff = TmpDate - TmpPeriod.FromDate.ToOADate

                            .FromDate = TmpPeriod.FromDate.AddDays(DateDiff)
                            .ToDate = TmpPeriod.ToDate.AddDays(DateDiff)
                            .PriceIsCPP = TmpPeriod.PriceIsCPP
                            .Price(TmpPeriod.PriceIsCPP) = TmpPeriod.Price(TmpPeriod.PriceIsCPP)
                            For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                                .Price(TmpPeriod.PriceIsCPP, i) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, i)
                            Next
                            If TmpTarget Is cmbTarget.SelectedItem Then
                                grdPricelist.Rows.Add()
                                grdPricelist.Rows(grdPricelist.Rows.Count - 1).Tag = TmpTarget.PricelistPeriods(.ID)
                            End If
                        End With
                    End If
                Next
            Next
        Else
            For Each TmpTarget As Trinity.cPricelistTarget In cmbTarget.Items
                For Each TmpRow As Windows.Forms.DataGridViewRow In grdIndexes.SelectedRows
                    Dim TmpSourceIndex As Trinity.cIndex = TmpRow.Tag
                    Dim TmpIndex As Trinity.cIndex = Nothing
                    For Each TmpInd As Trinity.cIndex In TmpTarget.Indexes
                        If TmpInd.FromDate = TmpSourceIndex.FromDate AndAlso TmpInd.ToDate = TmpSourceIndex.ToDate Then
                            TmpIndex = TmpInd
                            Exit For
                        End If
                    Next
                    If Not TmpIndex Is Nothing Then
                        With TmpTarget.Indexes.Add(TmpIndex.Name)
                            If .Enhancements.Count > 0 Then
                                For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
                                    With .Enhancements.Add
                                        .Amount = TmpEnh.Amount
                                        .Name = TmpEnh.Name
                                    End With
                                Next
                            End If
                            For d As Integer = 0 To TmpBT.Dayparts.Count - 1
                                .Index(d) = TmpIndex.Index(d)
                            Next
                            .IndexOn = TmpIndex.IndexOn
                            .SystemGenerated = TmpIndex.SystemGenerated
                            Dim TmpDate As Long = TmpIndex.FromDate.AddYears(1).ToOADate
                            Dim DateDiff As Long

                            While Weekday(Date.FromOADate(TmpDate), FirstDayOfWeek.Monday) <> Weekday(TmpIndex.FromDate, FirstDayOfWeek.Monday)
                                TmpDate = TmpDate - 1
                            End While
                            DateDiff = TmpDate - TmpIndex.FromDate.ToOADate

                            .FromDate = TmpIndex.FromDate.AddDays(DateDiff)
                            .ToDate = TmpIndex.ToDate.AddDays(DateDiff)
                            If TmpTarget Is cmbTarget.SelectedItem Then
                                grdIndexes.Rows.Add()
                                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = TmpTarget.Indexes(.ID)
                            End If
                        End With
                    End If
                Next
            Next
        End If

    End Sub

    Private Sub cmdUpdateAllUniverses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdateAllUniverses.Click

        'Create a date form but dont show the 'to' compartment of it
        Dim pickDate As New frmDates(False, "Update periods with end date after:", False)
        Dim progressBar As New frmProgress

        'Dim targetsAndUniverses As New KeyValuePair(Of String, Single)
        Dim valueDictionary As New Dictionary(Of String, Single)
        Dim valueDictionary2 As New Dictionary(Of String, Single)
        Dim Multiple As Single = 100 / Campaign.Channels.Count ' Campaign.Channels.Count / 100
        Dim ChannelCount As Single = 0
        Dim UpdatedCounter As Single = 0

        'pickDate.Text = "Update periods with end date after:"
        'pickDate.dateTo.Visible = False
        'If cancel is pressed the sub will exit
        If pickDate.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        pickDate.Close()
        progressBar.Show()

        For Each Channel As Trinity.cChannel In Campaign.Channels
            For Each BookingType As Trinity.cBookingType In Channel.BookingTypes
                For Each Target As Trinity.cPricelistTarget In BookingType.Pricelist.Targets
                    For Each PriceListPeriod As Trinity.cPricelistPeriod In Target.PricelistPeriods
                        If PriceListPeriod.ToDate >= pickDate.dateFrom.Value Then 'AndAlso pickDate.dateTo.Value >= PriceListPeriod.ToDate Then
                            Dim _targetName As String = Target.Target.TargetName
                            If TrinitySettings.DefaultArea = "NO"
                                
                                _targetName = "12+"
                                Target.Target.TargetName = Target.TargetName.Replace("A", "").Replace("K", "W")
                            Else

                            End If
                            If Not valueDictionary.ContainsKey(_targetName) Then
                                valueDictionary.Add(_targetName, Target.Target.UniSizeTot)
                                valueDictionary2.Add(_targetName, Target.Target.UniSize)
                            End If
                            PriceListPeriod.TargetNat = valueDictionary(_targetName)
                            PriceListPeriod.TargetUni = valueDictionary2(_targetName)
                            UpdatedCounter += 1
                        End If
                    Next
                Next
            Next
            ChannelCount += 1
            progressBar.Progress = (ChannelCount * Multiple)
            progressBar.Text = "Updated " & UpdatedCounter & " target groups"
        Next

        progressBar.Close()
        cmbBookingType.Items.Clear()
        For Each TmpChan As Trinity.cChannel In _channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                cmbBookingType.Items.Add(TmpBT)
            Next
        Next
        If cmbBookingType.Items.Count > 0 Then
            cmbBookingType.SelectedIndex = 0
        End If
        While grdPricelist.Columns.Count > 5
            grdPricelist.Columns.RemoveAt(5)
        End While

        grdPricelist.Columns("colCPP").Tag = "-1"

        'TODO: **DAYPART** Solve below as in Load

        'Dim TmpCol As System.Windows.Forms.DataGridViewTextBoxColumn

        'For i As Integer = 0 To Campaign.DaypartCount - 1
        '    TmpCol = New System.Windows.Forms.DataGridViewTextBoxColumn
        '    TmpCol.Name = "colCPP" & Campaign.DaypartName(i)
        '    TmpCol.HeaderText = "CPP " & Campaign.DaypartName(i)
        '    TmpCol.Tag = i
        '    grdPricelist.Columns.Add(TmpCol)
        'Next

        If Campaign.Area = "DK" Then
            cmdEnhancements.Visible = True
            cmdEditEnhancement.Visible = True
        End If
    End Sub

    Private Sub txtMaxRatings_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMaxRatings.TextChanged
        Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem
        Try
            TmpTarget.MaxRatings = CSng(txtMaxRatings.Text)
        Catch
            txtMaxRatings.Text = ""
        End Try
    End Sub

    Private Sub cmdCopyToNextYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If cmbYear.SelectedItem.ToString = "- All years -" Then
            MessageBox.Show("Please pick a specific year", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If MessageBox.Show("This will copy the pricelists from the year " & cmbYear.SelectedItem & " to the year " & CInt(cmbYear.SelectedItem) + 1 & vbNewLine & _
                        "Only do this if you're sure that this is what you want to do!" & vbNewLine & _
                        "All channels and all booking types will be extended. Universe sizes on the newly created" & vbNewLine & _
                        "price list periods will be updated to the current ones. The prices will stay the same.", "T R I N I T Y", MessageBoxButtons.OKCancel _
                        , MessageBoxIcon.Information) = Windows.Forms.DialogResult.Cancel Then Exit Sub

        Dim valueDictionary As New Dictionary(Of String, Single)
        Dim valueDictionary2 As New Dictionary(Of String, Single)

        'If mnuWizard.Tag = "" Then

        For Each ch As Trinity.cChannel In Campaign.Channels
            For Each bt As Trinity.cBookingType In ch.BookingTypes
                For Each tg As Trinity.cPricelistTarget In bt.Pricelist.Targets
                    Dim _deleteList As List(Of Trinity.cPricelistPeriod) = (From _period As Trinity.cPricelistPeriod In tg.PricelistPeriods Select _period Where _period.FromDate >= CDate(CInt(cmbYear.Text) + 1 & "-01-01")).ToList
                    For Each _period As Trinity.cPricelistPeriod In _deleteList
                        tg.PricelistPeriods.Remove(_period.ID)
                    Next

                    Dim _deleteList2 As List(Of Trinity.cIndex) = (From _ix As Trinity.cIndex In tg.Indexes Select _ix Where _ix.FromDate >= CDate(CInt(cmbYear.Text) + 1 & "-01-01")).ToList
                    For Each _period As Trinity.cIndex In _deleteList2
                        tg.Indexes.Remove(_period.ID)
                    Next

                Next
            Next
        Next

        For Each tmpChannel As Trinity.cChannel In Campaign.Channels
            For Each tmpBT As Trinity.cBookingType In tmpChannel.BookingTypes
                For Each tmpTarget As Trinity.cPricelistTarget In tmpBT.Pricelist.Targets
                    Dim _addPeriod As New List(Of Trinity.cPricelistPeriod)
                    For Each tmpPeriod As Trinity.cPricelistPeriod In tmpTarget.PricelistPeriods
                        If tmpPeriod.FromDate.Year = cmbYear.SelectedItem OrElse tmpPeriod.ToDate.Year = cmbYear.SelectedItem Then

                            _addPeriod.Add(tmpPeriod)

                        End If
                    Next
                    For Each _period As Trinity.cPricelistPeriod In _addPeriod
                        With tmpTarget.PricelistPeriods.Add(_period.Name)

                            If Not valueDictionary.ContainsKey(tmpTarget.TargetName) Then
                                valueDictionary.Add(tmpTarget.TargetName, tmpTarget.Target.UniSizeTot)
                                valueDictionary2.Add(tmpTarget.TargetName, tmpTarget.Target.UniSize)
                            End If
                            .TargetNat = valueDictionary(tmpTarget.TargetName)
                            .TargetUni = valueDictionary2(tmpTarget.TargetName)

                            .FromDate = _period.FromDate.AddYears(1).AddDays(-1)
                            .ToDate = _period.ToDate.AddYears(1).AddDays(-1)

                            .FromDate = FindNewDate(_period, _period.FromDate)
                            .ToDate = FindNewDate(_period, _period.ToDate)

                            .PriceIsCPP = _period.PriceIsCPP
                            .Price(_period.PriceIsCPP) = _period.Price(_period.PriceIsCPP)
                            For i As Integer = 0 To tmpBT.Dayparts.Count - 1
                                .Price(_period.PriceIsCPP, i) = _period.Price(_period.PriceIsCPP, i)
                            Next
                        End With
                    Next
                Next
            Next
        Next



        For Each tmpChannel As Trinity.cChannel In Campaign.Channels
            For Each tmpBT As Trinity.cBookingType In tmpChannel.BookingTypes

                For Each tmpTarget As Trinity.cPricelistTarget In tmpBT.Pricelist.Targets
                    For Each tmpIndex As Trinity.cIndex In tmpTarget.Indexes
                        If tmpIndex.FromDate.Year = cmbYear.SelectedItem Or tmpIndex.ToDate.Year = cmbYear.SelectedItem Then

                            Dim TmpSourceIndex As Trinity.cIndex = tmpIndex
                            Dim TmpIndex2 As Trinity.cIndex = Nothing
                            For Each TmpInd As Trinity.cIndex In tmpTarget.Indexes
                                If TmpInd.FromDate = TmpSourceIndex.FromDate AndAlso TmpInd.ToDate = TmpSourceIndex.ToDate Then
                                    TmpIndex2 = TmpInd
                                    Exit For
                                End If
                            Next

                            If Not TmpIndex2 Is Nothing Then
                                With tmpTarget.Indexes.Add(tmpIndex.Name)
                                    If .Enhancements.Count > 0 Then
                                        For Each TmpEnh As Trinity.cEnhancement In tmpIndex.Enhancements
                                            With .Enhancements.Add
                                                .Amount = TmpEnh.Amount
                                                .Name = TmpEnh.Name
                                            End With
                                        Next
                                    End If
                                    For d As Integer = 0 To tmpBT.Dayparts.Count - 1
                                        .Index(d) = tmpIndex.Index(d)
                                    Next
                                    .IndexOn = tmpIndex.IndexOn

                                    .SystemGenerated = tmpIndex.SystemGenerated
                                    .FromDate = tmpIndex.FromDate.AddYears(1).AddDays(-1)
                                    .ToDate = tmpIndex.ToDate.AddYears(1).AddDays(-1)
                                    'If tmpTarget Is cmbTarget.SelectedItem Then
                                    '    grdIndexes.Rows.Add()
                                    '    grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = tmpTarget.Indexes(.ID)
                                    'End If
                                End With
                            End If

                        End If
                    Next

                Next

            Next
        Next

        cmbChannel_SelectedIndexChanged(sender, e)
        cmbYear.SelectedIndex += 1

    End Sub

    Function FindNewDate(ByVal SourcePeriod As Trinity.cPricelistPeriod, ByVal SourceDate As Date) As Date
        Dim _newDate As Date = SourceDate.AddYears(1)
        If SourcePeriod.FromDate.Day > 1 AndAlso SourcePeriod.ToDate.Day < Date.DaysInMonth(SourcePeriod.ToDate.Year, SourcePeriod.ToDate.Month) Then
            While _newDate.DayOfWeek <> SourceDate.DayOfWeek
                _newDate = _newDate.AddDays(-1)
            End While
        End If
        Return _newDate.AddSeconds(-_newDate.TimeOfDay.TotalSeconds)
    End Function

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbChannel.SelectedIndexChanged
        If cmbChannel.SelectedItem = "" Then Exit Sub
        cmbBookingType.Items.Clear()
        For Each TmpBT As Trinity.cBookingType In _channels(cmbChannel.SelectedItem).BookingTypes
            Dim style As New StyleableComboboxStyle
            If Not TmpBT.IsUserEditable Then
                style.FontStyle = FontStyle.Bold
            End If
            cmbBookingType.Items.Add(TmpBT)
            cmbBookingType.SetItemStyle(TmpBT, style)
            cmbBookingType.SelectedIndex = 0
        Next

        'GroupBox1.Enabled = False
    End Sub

    'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

    '    If DBReader.isLocal Then
    '        MsgBox("You are using a Local database and all changes you make will be lost when you connect back to the network.", MsgBoxStyle.Information, "FYI")
    '    End If

    '    If System.Windows.Forms.MessageBox.Show("This will overwrite the default pricelists that are saved" & vbCrLf & "on your system." & vbCrLf & vbCrLf & "Are you sure you want to proceed?" & vbCrLf & vbCrLf & "(To save the changes to this campaign only, click the 'Apply' button)", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question, Windows.Forms.MessageBoxDefaultButton.Button2) <> Windows.Forms.DialogResult.Yes Then
    '        Exit Sub
    '    End If

    '    If InputBox("This function is protected with a password." & vbCrLf & "Please enter the password required:", "T R I N I T Y") <> "orange2010" Then
    '        System.Windows.Forms.MessageBox.Show("Wrong password!", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
    '        Exit Sub
    '    End If

    '    For Each Chan As Trinity.cChannel In Campaign.Channels
    '        ' If Chan.IsUserEditable Then

    '        Dim oldFileName As String = TrinitySettings.ActiveDataPath & Campaign.Area & "\Pricelists\" & Chan.ChannelName & ".xml"
    '        Dim newDirectory As String = TrinitySettings.ActiveDataPath & Campaign.Area & "\Pricelists\" & Now.ToShortDateString & "\"
    '        Dim newFileName As String = newDirectory & Now.ToLongTimeString.Replace(":", "-") & " " & Chan.ChannelName & ".xml"
    '        System.IO.Directory.CreateDirectory(newDirectory)
    '        System.IO.File.Copy(oldFileName, newFileName)

    '        For Each TmpBT As Trinity.cBookingType In Chan.BookingTypes
    '            'If TmpBT.IsUserEditable Then



    '            TmpBT.Pricelist.Save(TrinitySettings.ActiveDataPath & Campaign.Area & "\Pricelists\" & Chan.ChannelName & ".xml", (TrinitySettings.ConnectionStringCommon <> ""))
    '            '  End If
    '        Next
    '        ' End If
    '    Next
    '    Windows.Forms.MessageBox.Show("All pricelists were saved.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
    'End Sub

    Private Sub grdPricelist_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdPricelist.CellFormatting
        'Dim TmpPeriod As Trinity.cPricelistPeriod = grdPricelist.Rows(e.RowIndex).Tag
        'Dim TmpTarget As Trinity.cPricelistTarget = cmbTarget.SelectedItem

        'If TmpTarget Is Nothing Then Exit Sub

        'If TmpTarget.IsUserEditable Then
        '    e.CellStyle = cellStyleEditable
        'Else
        '    e.CellStyle = cellstyleNotEditable
        'End If
    End Sub

    Private Sub cmbPricelist_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbPricelist.SelectedIndexChanged
        If (cmbPricelist.SelectedItem <> DirectCast(cmbBookingType.SelectedItem, Trinity.cBookingType).PricelistName AndAlso (DirectCast(cmbBookingType.SelectedItem, Trinity.cBookingType).PricelistName = "" OrElse Windows.Forms.MessageBox.Show("This will change the pricelist associated with this bookingtype." & vbNewLine & vbNewLine & "Are you sure you want to proceed?", "T R I N I T Y", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes)) Then
            DirectCast(cmbBookingType.SelectedItem, Trinity.cBookingType).PricelistName = cmbPricelist.SelectedItem
            DirectCast(cmbBookingType.SelectedItem, Trinity.cBookingType).ReadPricelist()
            UpdateGrid()
            If cmbTarget.Items.Count > 0 Then
                cmbTarget.SelectedIndex = 0
            End If
        Else
            If DirectCast(cmbBookingType.SelectedItem, Trinity.cBookingType).PricelistName <> "" Then
                cmbPricelist.SelectedItem = DirectCast(cmbBookingType.SelectedItem, Trinity.cBookingType).PricelistName
            Else
                cmbPricelist.SelectedIndex = -1
            End If
        End If
    End Sub

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Dim _file As String
        Dim _bt As Trinity.cBookingType = cmbBookingType.SelectedItem

        If Windows.Forms.MessageBox.Show("This will overwrite the current server settings, affecting all future campaigns." & vbNewLine & "To save to this campaign only, click 'Apply'." & vbNewLine & vbNewLine & "Are you sure you want to proceed?", "T R I N I T Y", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        If _bt.ParentChannel.fileName = "" Then
            _file = TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork) & Campaign.Area & "\Channels.xml"
        Else
            _file = TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork) & Campaign.Area & "\" & _bt.ParentChannel.fileName
        End If
        Dim _xml As XElement = XElement.Load(_file)

        Debug.Print(_xml.<Data>.<Channels>...<Channel>.Where(Function(c) c.@Name = _bt.ParentChannel.ChannelName).Count)
        If _xml.<Channels>...<Channel>.Where(Function(c) c.@Name = _bt.ParentChannel.ChannelName).<Bookingtypes>...<Bookingtype>.Where(Function(bt) bt.@Name = _bt.Name).Count > 0 Then
            _xml.<Channels>...<Channel>.Where(Function(c) c.@Name = _bt.ParentChannel.ChannelName).<Bookingtypes>...<Bookingtype>.Where(Function(bt) bt.@Name = _bt.Name).First.@Pricelist = cmbPricelist.SelectedItem
        End If

        _xml.Save(_file)
    End Sub

    Private Sub cmdSaveAs_Click(sender As System.Object, e As System.EventArgs) Handles cmdSaveAs.Click
        Dim TmpBT As Trinity.cBookingType = cmbBookingType.SelectedItem

        If TmpBT.PricelistName = "" Then
            Dim _dlg As New Windows.Forms.SaveFileDialog

            _dlg.FileName = "*.xml"
            _dlg.Filter = "Pricelists|*.xml"

            If _dlg.ShowDialog = Windows.Forms.DialogResult.OK Then

                If TrinitySettings.Developer Then
                    'Dim _file As String = TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork) & Campaign.Area & "/Pricelists/" & IO.Path.GetFileName(_dlg.FileName)
                    'TmpBT.Pricelist.Save(_file, False, True)
                Else
                    If Windows.Forms.MessageBox.Show("You are not an administrator. Only non-standard targets will be saved, and only in the database for your company.", "T R I N I T Y", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Cancel Then
                        Exit Sub
                    End If
                End If

                Dim _file As String = TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork) & Campaign.Area & "/Pricelists/" & IO.Path.GetFileName(_dlg.FileName)
                TmpBT.Pricelist.Save(_file, False, False)

            End If
        Else
            TmpBT.Pricelist.Save(IO.Path.Combine(IO.Path.Combine(TrinitySettings.SharedDataPath, Campaign.Area), "Pricelists\" & TmpBT.PricelistName & ".xml"), False, True)
            Dim _file As String = TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork) & Campaign.Area & "/Pricelists/" & TmpBT.PricelistName & ".xml"
            TmpBT.Pricelist.Save(_file, False, False)
        End If
    End Sub

    Private Sub cmdImport_Click(sender As System.Object, e As System.EventArgs) Handles cmdImport.Click
        Dim _dlg As New OpenFileDialog()
        _dlg.Filter = "Excel workbooks|*.xls;*.xlsx;*.xlsm"
        If _dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim _doc As New cDocument
            _doc.Load(_dlg.FileName)

            Dim _sheets() As String = _doc.Sheets.Keys.ToArray

            Dim _frm As New frmMultipleChoice(_sheets, "Which sheet contains the pricelist?")
            _frm.ShowDialog()
            Dim _sheet As String = _frm.Result
            Dim _xml As XElement = <pricelists>
                                       <template name="Kanal5" pricetype="CPT" dayparts="2">
                                           <sheets required="true" ask="true">
                                               <sheet name=<%= _sheet %>></sheet>
                                           </sheets>
                                           <identify>
                                               <rule>
                                                   <find fromcol="1" fromrow="1" tocol="10" torow="5" contains="Kanal 5"></find>
                                               </rule>
                                           </identify>
                                           <columns>
                                               <row>
                                                   <find fromcol="1" fromrow="1" tocol="1" torow="15" contains="Målgrupp"></find>
                                               </row>
                                               <targetcolumn>
                                                   <headline value="Målgrupp"></headline>
                                               </targetcolumn>
                                               <universecolumn>
                                                   <headline value="Tot univ."></headline>
                                               </universecolumn>
                                               <pricecolumn>
                                                   <headline value="CPM"></headline>
                                               </pricecolumn>
                                           </columns>
                                           <targetlist>
                                               <end>
                                                   <find valueis="Veckor" fromrow="+0" fromcol="1" torow="+0" tocol="3"></find>
                                               </end>
                                               <skip>
                                                   <check valueis="" row="+0" col="5"></check>
                                                   <check valueis="PT" row="+0" col="5"></check>
                                               </skip>
                                           </targetlist>
                                       </template>
                                       <template name="Kanal9" pricetype="CPT" dayparts="2">
                                           <sheets required="true" ask="true">
                                               <sheet name=<%= _sheet %>></sheet>
                                           </sheets>
                                           <identify>
                                               <rule>
                                                   <find fromcol="1" fromrow="1" tocol="10" torow="5" contains="Kanal 9"></find>
                                               </rule>
                                           </identify>
                                           <columns>
                                               <row>
                                                   <find fromcol="1" fromrow="1" tocol="1" torow="15" contains="Målgrupp"></find>
                                               </row>
                                               <targetcolumn>
                                                   <headline value="Målgrupp"></headline>
                                               </targetcolumn>
                                               <universecolumn>
                                                   <headline value="Tot univ."></headline>
                                               </universecolumn>
                                               <pricecolumn>
                                                   <headline value="CPM"></headline>
                                               </pricecolumn>
                                           </columns>
                                           <targetlist>
                                               <end>
                                                   <check valueis="Veckor" row="+0" col="3"></check>
                                               </end>
                                               <skip>
                                                   <check valueis="" row="+0" col="5"></check>
                                                   <check valueis="PT" row="+0" col="5"></check>
                                               </skip>
                                           </targetlist>
                                       </template>
                                       <template name="MTG" pricetype="CPT" dayparts="2">
                                           <sheets required="true">
                                               <sheet name=<%= _sheet %>></sheet>
                                           </sheets>
                                           <identify>
                                               <rule>
                                                   <find fromcol="1" fromrow="1" tocol="5" torow="10" contains="gäller"></find>
                                               </rule>
                                           </identify>
                                           <columns>
                                               <row>
                                                   <find fromcol="1" fromrow="1" tocol="5" torow="15" contains="Målgrupp"></find>
                                               </row>
                                               <targetcolumn>
                                                   <headline value="Målgrupp"></headline>
                                               </targetcolumn>
                                               <universecolumn>
                                                   <headline value="000´s"></headline>
                                               </universecolumn>
                                               <pricecolumn>
                                                   <headline value="CPT/OP"></headline>
                                               </pricecolumn>
                                           </columns>
                                           <targetlist>
                                               <end>
                                                   <check contains="Off Prime" row="+0" col="2"></check>
                                               </end>
                                               <skip>
                                                   <check valueis="" row="+0" col="2"></check>
                                               </skip>
                                           </targetlist>
                                       </template>

                                   </pricelists>


            Dim _xdoc As XDocument = XDocument.Load(_xml.CreateReader())
            Dim _bt As Trinity.cBookingType = cmbBookingType.SelectedItem

            'Add dialog to input dates
            Dim _startDate As Date = CDate("2012-01-01")
            Dim _endDate As Date = CDate("2012-12-31")

            For Each _tmpl As XElement In _xdoc.<pricelists>...<template>
                Dim _tmpXdoc As XDocument = XDocument.Load(_tmpl.CreateReader())
                Dim _template As New PricelistTemplates.cPricelistTemplate()
                _template.Parse(_tmpXdoc)
                If _template.Validate(_doc, True) = ITemplate.ValidationResult.Success Then

                    Dim _removeList = (From _t As Trinity.cPricelistTarget In _bt.Pricelist.Targets From _p As Trinity.cPricelistPeriod In _t.PricelistPeriods Where _p.FromDate = _startDate AndAlso _p.ToDate = _endDate Select New With {.Target = _t.TargetName, .Period = _p.ID}).ToList
                    For Each _pp As Object In _removeList
                        DirectCast(_bt.Pricelist.Targets(_pp.Target), Trinity.cPricelistTarget).PricelistPeriods.Remove(_pp.Period)
                    Next
                    For Each _price As PricelistTemplates.cPrice In _template.GetPrices()
                        Dim _targetName As String = _price.TargetName.ToString.Replace(" år", "")
                        If _targetName <> "" Then
                            If _bt.Pricelist.Targets(_targetName) Is Nothing AndAlso IsNumeric(_targetName.Substring(0, 1)) Then _targetName = "A" & _targetName
                            If _bt.Pricelist.Targets(_targetName) Is Nothing Then
                                With _bt.Pricelist.Targets.Add(_targetName, _bt)
                                    .CalcCPP = _template.Dayparts > 1
                                    .StandardTarget = True
                                    .Target.TargetName = _targetName.Replace(" ", "").Replace("A", "").Replace("K", "W")
                                End With
                            End If
                            With _bt.Pricelist.Targets(_targetName).PricelistPeriods.Add("2012")
                                .PriceIsCPP = (_template.PriceType = PricelistTemplates.cPricelistTemplate.PriceTypeEnum.CPP)
                                .FromDate = _startDate
                                .ToDate = _endDate
                                .TargetNat = _price.UniSize
                                .TargetUni = _price.UniSize                                
                                For _dp As Integer = 0 To _template.Dayparts - 1
                                    .Price(.PriceIsCPP, _dp) = _price.Price(_dp)
                                Next
                                For _dp As Integer = _template.Dayparts To _bt.Dayparts.Count - 1
                                    .Price(.PriceIsCPP, _dp) = _price.Price(0)
                                Next
                            End With
                        End If
                    Next
                    Exit For
                End If
            Next
        End If
    End Sub

    'Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
    '    For Each _bt As Trinity.cBookingType In cmbBookingType.Items
    '        For Each _t As Trinity.cPricelistTarget In _bt.Pricelist.Targets
    '            _t.StandardTarget = True
    '        Next
    '    Next
    'End Sub

    Private Sub cmdRefreshPricelist_Click(sender As System.Object, e As System.EventArgs) Handles cmdRefreshPricelist.Click
        DirectCast(cmbBookingType.SelectedItem, Trinity.cBookingType).ReadPricelist()
        UpdateGrid()
        If cmbTarget.Items.Count > 0 Then
            cmbTarget.SelectedIndex = 0
        End If
    End Sub

    Private Sub grdPricelist_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdPricelist.KeyUp
        If e.Control AndAlso e.KeyCode = Keys.V Then
            Dim _rows() As String = Clipboard.GetText().Split(vbNewLine)
            Dim _r As Integer = grdPricelist.SelectedRows(0).Index
            For Each _row As String In _rows
                Dim _cells() As String = _row.Split(vbTab)
                Dim _c As Integer = 0
                For Each _cell As String In _cells
                    If _cell.Trim <> "" Then
                        While Not grdPricelist.Columns(_c).Visible
                            _c += 1
                        End While
                        grdPricelist.Rows(_r).Cells(_c).Value = _cell

                    End If
                    _c += 1
                Next
                _r += 1
            Next

        End If
    End Sub

    Private Sub grdIndexes_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdIndexes.KeyUp
        If e.Control AndAlso e.KeyCode = Keys.V Then
            Dim _rows() As String = Clipboard.GetText().Split(vbNewLine)
            Dim _r As Integer = grdIndexes.SelectedRows(0).Index
            For Each _row As String In _rows
                Dim _cells() As String = _row.Split(vbTab)
                Dim _c As Integer = 0
                For Each _cell As String In _cells
                    If _cell.Trim <> "" Then
                        While Not grdIndexes.Columns(_c).Visible
                            _c += 1
                        End While
                        grdIndexes.Rows(_r).Cells(_c).Value = _cell

                    End If
                    _c += 1
                Next
                _r += 1
            Next

        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        Dim _chan As Trinity.cChannel = Campaign.Channels(cmbChannel.SelectedItem)
        For Each _bt As Trinity.cBookingType In _chan.BookingTypes
            For Each _target As Trinity.cPricelistTarget In _bt.Pricelist.Targets
                For Each _period As Trinity.cPricelistPeriod In _target.PricelistPeriods
                    _period.FromDate = _period.FromDate.AddDays((_period.ToDate.ToOADate - _period.FromDate.ToOADate) / 2)
                    _period.FromDate = _period.FromDate.AddDays(-(_period.FromDate.Day - 1))
                    _period.ToDate = _period.FromDate.AddMonths(1).AddDays(-1)
                Next
                Dim _removeList As New List(Of String)
                For _p As Integer = 0 To _target.PricelistPeriods.Count - 1
                    Dim _t As Trinity.cPricelistTarget = _target
                    Dim p As Integer = _p
                    Dim _periods As List(Of Trinity.cPricelistPeriod) = (From _per As Trinity.cPricelistPeriod In _t.PricelistPeriods Where _per.FromDate = _t.PricelistPeriods(p).FromDate AndAlso _per.ToDate = _t.PricelistPeriods(p).ToDate Select _per).ToList
                    While _periods.Count > 1
                        _removeList.Add(_periods(1).ID)
                        _periods.RemoveAt(1)
                    End While
                Next
                For Each _id As String In _removeList
                    _target.PricelistPeriods.Remove(_id)
                Next
            Next
        Next

    End Sub

    Private Function DaysInMonth(Year As Integer, Month As Integer) As Integer
        Dim _date As New Date(Year, Month, 1)
        _date = _date.AddMonths(1).AddDays(-1)
        Return _date.Day
    End Function

    Private Sub cmdFixNorway_Click(sender As System.Object, e As System.EventArgs) Handles cmdFixNorway.Click
        For Each _chan As Trinity.cChannel In Campaign.Channels
            For Each _bt As Trinity.cBookingType In _chan.BookingTypes
                For Each _target As Trinity.cPricelistTarget In _bt.Pricelist.Targets
                    _target.Target.TargetName = _target.TargetName.Replace("A", "").Replace("K", "W").Replace("P", "")
                Next
            Next
        Next
    End Sub

    Private Sub cmdCompress_Click(sender As System.Object, e As System.EventArgs) Handles cmdCompress.Click
        
        Dim frmreadChSplit As New frmReadChannelSplit
        frmreadChSplit.ShowDialog()

        'Dim _message As New System.Text.StringBuilder("Performed these compressions:" & vbNewLine)
        'For Each _channel As Trinity.cChannel In Campaign.Channels
        '    For Each _bt As Trinity.cBookingType In _channel.BookingTypes
        '        Dim _removedIndexes As Integer = 0
        '        Dim _removedPeriods As Integer = 0
        '        For Each _target As Trinity.cPricelistTarget In _bt.Pricelist.Targets
        '            If _target.Indexes.Count > 0 Then
        '                Dim _lastIndex As Trinity.cIndex = Nothing
        '                Dim _removeList As New List(Of Trinity.cIndex)
        '                For Each _index As Trinity.cIndex In (From _i As Trinity.cIndex In _target.Indexes Select _i Order By _i.FromDate)
        '                    If _lastIndex IsNot Nothing Then
        '                        Dim _removeIt As Boolean = False
        '                        If _lastIndex.ToDate.AddDays(1) = _index.FromDate AndAlso _lastIndex.Index = _index.Index Then
        '                            _index.FromDate = _lastIndex.FromDate
        '                            _index.Name = _lastIndex.Name & "+" & _index.Name
        '                            _removeIt = True
        '                        ElseIf _lastIndex.Index = 100 Then
        '                            _removeIt = True
        '                        End If
        '                        If _removeIt Then
        '                            _removeList.Add(_lastIndex)
        '                        End If
        '                    End If
        '                    _lastIndex = _index
        '                Next
        '                _removedIndexes += _removeList.Count
        '                For Each _index As Trinity.cIndex In _removeList
        '                    _target.Indexes.Remove(_index.ID)
        '                Next
        '            End If
        '            If _target.PricelistPeriods.Count > 0 Then
        '                Dim _lastPeriod As Trinity.cPricelistPeriod = Nothing
        '                Dim _removeList As New List(Of Trinity.cPricelistPeriod)
        '                For Each _period As Trinity.cPricelistPeriod In (From _p As Trinity.cPricelistPeriod In _target.PricelistPeriods Select _p Order By _p.FromDate)
        '                    If _lastPeriod IsNot Nothing Then
        '                        Dim _removeIt As Boolean = False
        '                        If _lastPeriod.ToDate.AddDays(1) = _period.FromDate AndAlso SamePrice(_lastPeriod, _period, _target.CalcCPP) Then
        '                            _period.FromDate = _lastPeriod.FromDate
        '                            _period.Name = _lastPeriod.Name & "+" & _period.Name
        '                            _removeIt = True
        '                        End If
        '                        If _removeIt Then
        '                            _removeList.Add(_lastPeriod)
        '                        End If
        '                    End If
        '                    _lastPeriod = _period
        '                Next
        '                _removedPeriods += _removeList.Count
        '                For Each _period As Trinity.cPricelistPeriod In _removeList
        '                    _target.PricelistPeriods.Remove(_period.ID)
        '                Next
        '            End If
        '        Next
        '        If _removedIndexes > 0 OrElse _removedPeriods > 0 Then
        '            _message.AppendLine(String.Format("{0} - Removed {1} indexes and {2} periods", _bt.ToString, _removedIndexes, _removedPeriods))
        '        End If
        '    Next
        'Next
        'Windows.Forms.MessageBox.Show(_message.ToString, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Function SamePrice(period1 As Trinity.cPricelistPeriod, period2 As Trinity.cPricelistPeriod, UseDayparts As Boolean) As Boolean
        Dim TmpBT As Trinity.cBookingType = cmbBookingType.SelectedItem
        If UseDayparts Then
            For _dp As Integer = 0 To TmpBT.Dayparts.Count - 1
                If period1.Price(True, _dp) <> period2.Price(True, _dp) Then
                    Return False
                End If
            Next
        Else
            If period1.Price(True) <> period2.Price(True) Then
                Return False
            End If
        End If
        Return True
    End Function

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles cmbCopyToNextYear.Click
        If cmbYear.SelectedItem.ToString = "- All years -" Then
            MessageBox.Show("Please pick a specific year", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If MessageBox.Show("This will copy the pricelists from the year " & cmbYear.SelectedItem & " to the year " & CInt(cmbYear.SelectedItem) + 1 & vbNewLine & _
                        "Only do this if you're sure that this is what you want to do!" & vbNewLine & _
                        "All channels and all booking types will be extended. Universe sizes on the newly created" & vbNewLine & _
                        "price list periods will be updated to the current ones. The prices will stay the same.", "T R I N I T Y", MessageBoxButtons.OKCancel _
                        , MessageBoxIcon.Information) = Windows.Forms.DialogResult.Cancel Then Exit Sub

        Dim valueDictionary As New Dictionary(Of String, Single)
        Dim valueDictionary2 As New Dictionary(Of String, Single)

        'If mnuWizard.Tag = "" Then

        For Each ch As Trinity.cChannel In Campaign.Channels
            For Each bt As Trinity.cBookingType In ch.BookingTypes
                For Each tg As Trinity.cPricelistTarget In bt.Pricelist.Targets
                    Dim _deleteList As List(Of Trinity.cPricelistPeriod) = (From _period As Trinity.cPricelistPeriod In tg.PricelistPeriods Select _period Where _period.FromDate >= CDate(CInt(cmbYear.Text) + 1 & "-01-01")).ToList
                    For Each _period As Trinity.cPricelistPeriod In _deleteList
                        tg.PricelistPeriods.Remove(_period.ID)
                    Next

                    Dim _deleteList2 As List(Of Trinity.cIndex) = (From _ix As Trinity.cIndex In tg.Indexes Select _ix Where _ix.FromDate >= CDate(CInt(cmbYear.Text) + 1 & "-01-01")).ToList
                    For Each _period As Trinity.cIndex In _deleteList2
                        tg.Indexes.Remove(_period.ID)
                    Next

                Next
            Next
        Next

        For Each tmpChannel As Trinity.cChannel In Campaign.Channels
            For Each tmpBT As Trinity.cBookingType In tmpChannel.BookingTypes
                For Each tmpTarget As Trinity.cPricelistTarget In tmpBT.Pricelist.Targets
                    Dim _addPeriod As New List(Of Trinity.cPricelistPeriod)
                    For Each tmpPeriod As Trinity.cPricelistPeriod In tmpTarget.PricelistPeriods
                        If tmpPeriod.FromDate.Year = cmbYear.SelectedItem OrElse tmpPeriod.ToDate.Year = cmbYear.SelectedItem Then

                            _addPeriod.Add(tmpPeriod)

                        End If
                    Next
                    For Each _period As Trinity.cPricelistPeriod In _addPeriod
                        With tmpTarget.PricelistPeriods.Add(_period.Name)

                            If Not valueDictionary.ContainsKey(tmpTarget.TargetName) Then
                                If TrinitySettings.DefaultArea = "NO" Then
                                    valueDictionary.Add(tmpTarget.TargetName, "4450")
                                    valueDictionary2.Add(tmpTarget.TargetName, "4450")
                                Else
                                    valueDictionary.Add(tmpTarget.TargetName, tmpTarget.Target.UniSizeTot)
                                    valueDictionary2.Add(tmpTarget.TargetName, tmpTarget.Target.UniSize)
                                End If
                            End If
                            .TargetNat = valueDictionary(tmpTarget.TargetName)
                            .TargetUni = valueDictionary2(tmpTarget.TargetName)

                            .FromDate = _period.FromDate.AddYears(1).AddDays(-1)
                            .ToDate = _period.ToDate.AddYears(1).AddDays(-1)

                            .FromDate = FindNewDate(_period, _period.FromDate)
                            .ToDate = FindNewDate(_period, _period.ToDate)

                            .PriceIsCPP = _period.PriceIsCPP
                            .Price(_period.PriceIsCPP) = _period.Price(_period.PriceIsCPP)
                            For i As Integer = 0 To tmpBT.Dayparts.Count - 1
                                .Price(_period.PriceIsCPP, i) = _period.Price(_period.PriceIsCPP, i)
                            Next
                        End With
                    Next
                Next
            Next
        Next



        For Each tmpChannel As Trinity.cChannel In Campaign.Channels
            For Each tmpBT As Trinity.cBookingType In tmpChannel.BookingTypes

                For Each tmpTarget As Trinity.cPricelistTarget In tmpBT.Pricelist.Targets
                    For Each tmpIndex As Trinity.cIndex In tmpTarget.Indexes
                        If tmpIndex.FromDate.Year = cmbYear.SelectedItem Or tmpIndex.ToDate.Year = cmbYear.SelectedItem Then

                            Dim TmpSourceIndex As Trinity.cIndex = tmpIndex
                            Dim TmpIndex2 As Trinity.cIndex = Nothing
                            For Each TmpInd As Trinity.cIndex In tmpTarget.Indexes
                                If TmpInd.FromDate = TmpSourceIndex.FromDate AndAlso TmpInd.ToDate = TmpSourceIndex.ToDate Then
                                    TmpIndex2 = TmpInd
                                    Exit For
                                End If
                            Next

                            If Not TmpIndex2 Is Nothing Then
                                With tmpTarget.Indexes.Add(tmpIndex.Name)
                                    If .Enhancements.Count > 0 Then
                                        For Each TmpEnh As Trinity.cEnhancement In tmpIndex.Enhancements
                                            With .Enhancements.Add
                                                .Amount = TmpEnh.Amount
                                                .Name = TmpEnh.Name
                                            End With
                                        Next
                                    End If
                                    For d As Integer = 0 To tmpBT.Dayparts.Count - 1
                                        .Index(d) = tmpIndex.Index(d)
                                    Next
                                    .IndexOn = tmpIndex.IndexOn

                                    .SystemGenerated = tmpIndex.SystemGenerated
                                    .FromDate = tmpIndex.FromDate.AddYears(1).AddDays(-1)
                                    .ToDate = tmpIndex.ToDate.AddYears(1).AddDays(-1)
                                    'If tmpTarget Is cmbTarget.SelectedItem Then
                                    '    grdIndexes.Rows.Add()
                                    '    grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = tmpTarget.Indexes(.ID)
                                    'End If
                                End With
                            End If

                        End If
                    Next

                Next

            Next
        Next
        cmbChannel_SelectedIndexChanged(sender, e)
        cmbYear.SelectedIndex += 1
    End Sub

    Private Sub grdPricelist_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdPricelist.CellContentClick

    End Sub
End Class