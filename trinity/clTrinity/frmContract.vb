Imports System.Windows.Forms
Imports System.Drawing

Public Class frmContract
    'a contract is a altered pricelist. Its simply a copy of the Channel structure with the negotiated prices

    'Dim TmpContract As Trinity.cContract
    'Dim TmpCampaign As Trinity.cKampanj

    Dim IndexNames() As String = {"Net CPP", "Gross CPP", "TRP", "", "Fixed CPP"}
    Dim ShowInNames() As String = {"Both", "Allocate", "Booking"}


    Private Sub frmContract_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        Me.Dispose()
    End Sub

    Private Sub frmContract_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim TmpChan As Trinity.cChannel
        'Dim TmpBT As Trinity.cBookingType
        'Dim TmpCost As Trinity.cCost
        'Dim i As Integer

        'TmpContract = New Trinity.cContract(Campaign)
        'For Each TmpChan In TmpContract.Channels
        '    For Each TmpBT In TmpChan.BookingTypes
        '        TmpBT.ReadPricelist()
        '        For Each TmpTarget As Trinity.cPricelistTarget In Campaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Pricelist.Targets
        '            Try
        '                TmpBT.Pricelist.Targets.Remove(TmpTarget.TargetName)
        '            Catch ex As Exception

        '            End Try
        '            With TmpBT.Pricelist.Targets.Add(TmpTarget.TargetName, TmpTarget.Target, , , TmpTarget.CalcCPP)
        '                .Discount = TmpTarget.Discount
        '                .IsEntered = TmpTarget.IsEntered
        '                .StandardTarget = TmpTarget.StandardTarget
        '                For Each TmpIndex As Trinity.cPricelistPeriod In TmpTarget.PricelistPeriods
        '                    .PricelistPeriods.Add(TmpIndex.Name, TmpIndex.ID)
        '                    .PricelistPeriods(TmpIndex.ID).FromDate = TmpIndex.FromDate
        '                    .PricelistPeriods(TmpIndex.ID).ToDate = TmpIndex.ToDate
        '                    .PricelistPeriods(TmpIndex.ID).TargetNat = TmpIndex.TargetNat
        '                    .PricelistPeriods(TmpIndex.ID).TargetUni = TmpIndex.TargetUni
        '                    .PricelistPeriods(TmpIndex.ID).PriceIsCPP = TmpIndex.PriceIsCPP
        '                    .PricelistPeriods(TmpIndex.ID).Price(TmpIndex.PriceIsCPP) = TmpIndex.Price(TmpIndex.PriceIsCPP)
        '                    For i = 0 To Campaign.DaypartCount - 1
        '                        .PricelistPeriods(TmpIndex.ID).Price(TmpIndex.PriceIsCPP, i) = TmpIndex.Price(TmpIndex.PriceIsCPP, i)
        '                    Next
        '                Next
        '            End With
        '        Next
        '    Next
        'Next

        'TmpCampaign = New Trinity.cKampanj
        'TmpCampaign.Area = Campaign.Area
        'TmpCampaign.AreaLog = Campaign.AreaLog

        'TmpCampaign.CreateChannels()
        'For Each TmpChan In TmpCampaign.Channels
        '    For Each TmpBT In TmpChan.BookingTypes
        '        TmpBT.ReadPricelist()
        '        If Not Campaign.Channels(TmpChan.ChannelName) Is Nothing AndAlso Not Campaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name) Is Nothing Then
        '            For Each TmpTarget As Trinity.cPricelistTarget In Campaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Pricelist.Targets
        '                Try
        '                    TmpBT.Pricelist.Targets.Remove(TmpTarget.TargetName)
        '                Catch ex As Exception

        '                End Try
        '                With TmpBT.Pricelist.Targets.Add(TmpTarget.TargetName, TmpTarget.Target, , , TmpTarget.CalcCPP)
        '                    .Discount = TmpTarget.Discount
        '                    .IsEntered = TmpTarget.IsEntered
        '                    .StandardTarget = TmpTarget.StandardTarget
        '                    For Each TmpIndex As Trinity.cPricelistPeriod In TmpTarget.PricelistPeriods
        '                        .PricelistPeriods.Add(TmpIndex.Name, TmpIndex.ID)
        '                        .PricelistPeriods(TmpIndex.ID).FromDate = TmpIndex.FromDate
        '                        .PricelistPeriods(TmpIndex.ID).ToDate = TmpIndex.ToDate
        '                        .PricelistPeriods(TmpIndex.ID).PriceIsCPP = TmpIndex.PriceIsCPP
        '                        .PricelistPeriods(TmpIndex.ID).Price(TmpIndex.PriceIsCPP) = TmpIndex.Price(TmpIndex.PriceIsCPP)
        '                        .PricelistPeriods(TmpIndex.ID).TargetNat = TmpIndex.TargetNat
        '                        .PricelistPeriods(TmpIndex.ID).TargetUni = TmpIndex.TargetUni
        '                        .PricelistPeriods(TmpIndex.ID).Price(TmpIndex.PriceIsCPP) = TmpIndex.Price(TmpIndex.PriceIsCPP)
        '                        For i = 0 To Campaign.DaypartCount - 1
        '                            .PricelistPeriods(TmpIndex.ID).Price(TmpIndex.PriceIsCPP, i) = TmpIndex.Price(TmpIndex.PriceIsCPP, i)
        '                        Next
        '                    Next
        '                End With
        '            Next
        '        End If
        '    Next
        'Next




        'Dim TmpCol As System.Windows.Forms.DataGridViewTextBoxColumn
        'For i = 0 To Campaign.DaypartCount - 1
        '    TmpCol = New System.Windows.Forms.DataGridViewTextBoxColumn
        '    TmpCol.Name = "col" & Campaign.DaypartName(i)
        '    TmpCol.HeaderText = Campaign.DaypartName(i)
        '    TmpCol.Width = 50
        '    TmpCol.Resizable = DataGridViewTriState.False
        '    TmpCol.DefaultCellStyle.Format = "##,##0%"
        '    grdTargets.Columns.Add(TmpCol)
        'Next

        'cmbIndexChannel.Items.Clear()
        'For Each TmpChan In TmpContract.Channels
        '    For Each TmpBT In TmpChan.BookingTypes
        '        If TmpBT.Pricelist.Targets.Count > 0 Then
        '            cmbIndexChannel.Items.Add(TmpBT)
        '        End If
        '    Next
        'Next

        'cmbIndexChannel.SelectedIndex = 0

        Dim TypeArray() As String = {"Fixed", "Percent", "Per Unit", "On Discount"}

        Dim TmpCost As Trinity.cCost
        grdCosts.Rows.Clear()
        For Each TmpCost In Campaign.Contract.Costs
            grdCosts.Rows.Add()
            grdCosts.Rows(grdCosts.Rows.Count - 1).Tag = TmpCost
        Next
        txtName.Text = Campaign.Contract.Name
        dtFrom.Value = Campaign.Contract.FromDate
        dtTo.Value = Campaign.Contract.ToDate

        'For Each TmpChan As Trinity.cChannel In Campaign.Channels
        '    cmbChannels.Items.Add(TmpChan)
        'Next

        cmbChannels.Items.Clear()
        For Each TmpChan As Trinity.cContractChannel In Campaign.Contract.Channels
            If TmpChan.BookingTypes(1) IsNot Nothing AndAlso TmpChan.BookingTypes(1).Count > 0 Then
                cmbChannels.Items.Add(TmpChan)
            End If
        Next

        grdLevels.Rows.Clear()

        txtNotes.Text = Campaign.Contract.Description

        'dtFrom_ValueChanged(New Object, New EventArgs)

        'Dim TmpCost As Trinity.cCost

        'grdCosts.Rows.Clear()
        'For Each TmpCost In Campaign.Contract.Costs
        '    grdCosts.Rows.Add()
        '    grdCosts.Rows(grdCosts.Rows.Count - 1).Tag = TmpCost
        'Next
        PopulateClientCombo()
        If Campaign.Area <> "DK" Then
            grdAddedValues.Columns(3).Visible = False
        End If
        If Campaign.Contract.restriced Then
            chkContractRestriction.Checked = True
        Else
            chkContractRestriction.Checked = False
        End If

        colMarathonID.Visible = TrinitySettings.MarathonEnabled

        cmdSave.Visible = TrinitySettings.SaveCampaignsAsFiles
        cmdSaveToDB.Visible = Not TrinitySettings.SaveCampaignsAsFiles
    End Sub

    Public Sub PopulateClientCombo()
        'selcts all clients and put the names into the combo box

        cmbClient.Items.Clear()
        cmbClient.DisplayMember = "Name"

        Dim clients As DataTable = DBReader.getAllClients()
        For Each dr As DataRow In clients.Rows
            Dim TmpItem As New Client
            TmpItem.name = dr.Item("name") 'rd!name
            TmpItem.id = dr.Item("id") 'rd!id
            ' Added by JOKO
            ' Important contraint since Norway dont have that value and will then return null and it will break down.
<<<<<<< HEAD
            If TrinitySettings.DefaultArea <> "NO" And dr.ItemArray.Length > 2 Then
                If Not IsDBNull(dr.Item("restricted")) Then
                    TmpItem.restricted = dr.Item("restricted") 'rd!Restricted 
=======
            If TrinitySettings.DefaultArea <> "NO" Then
                If dr.ItemArray.Length > 2 Then
                    If Not IsDBNull(dr.Item("restricted")) Then
                        TmpItem.restricted = dr.Item("restricted") 'rd!Restricted 
                    End If
>>>>>>> f647b3c9ae32fce817ae49eeff9ed37047413848
                End If
            End If
                cmbClient.Items.Add(TmpItem)
            If TmpItem.id = Campaign.ClientID Then
                cmbClient.Text = TmpItem.name
            End If
        Next

    End Sub
    Private Sub cmdAddTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddTarget.Click
        If cmbBookingtypes.SelectedIndex = -1 Then Exit Sub

        Dim TmpTarget As Trinity.cContractTarget

        Dim TmpBT As Trinity.cContractBookingtype = DirectCast(cmbBookingtypes.SelectedItem, Trinity.cContractBookingtype).ParentChannel.BookingTypes(cmbLevel.SelectedIndex + 1)(DirectCast(cmbBookingtypes.SelectedItem, Trinity.cContractBookingtype).Name)

        Dim Chan As String
        Dim BT As String
        Chan = TmpBT.ParentChannel.ChannelName
        BT = TmpBT.Name


        'get the first unused target
        Dim i As Integer
        For i = 1 To Campaign.Channels(Chan).BookingTypes(BT).Pricelist.Targets.Count
            If TmpBT.ContractTargets(Campaign.Channels(Chan).BookingTypes(BT).Pricelist.Targets(i).TargetName) Is Nothing Then
                Exit For
            End If
        Next

        If i > Campaign.Channels(Chan).BookingTypes(BT).Pricelist.Targets.Count Then
            MsgBox("You have no available Targets not in use", MsgBoxStyle.Information, "Error")
            Exit Sub
        End If

        TmpTarget = TmpBT.ParentChannel.BookingTypes(cmbLevel.SelectedIndex + 1)(TmpBT.Name).ContractTargets.Add(Campaign.Channels(Chan).BookingTypes(BT).Pricelist.Targets(i).TargetName)

        'set the basic attributes
        TmpTarget.AdEdgeTargetName = Campaign.Channels(Chan).BookingTypes(BT).Pricelist.Targets(i).Target.TargetName
        TmpTarget.CalcCPP = Campaign.Channels(Chan).BookingTypes(BT).Pricelist.Targets(i).CalcCPP
        TmpTarget.Bookingtype = TmpBT
        TmpTarget.Indexes = New Trinity.cIndexes(Campaign, TmpTarget)

        grdTargets.Rows.Add()
        grdTargets.Rows(grdTargets.Rows.Count - 1).Tag = TmpTarget
        cmbIndexTarget.Items.Add(TmpTarget)

    End Sub

    Private Sub dtFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtFrom.ValueChanged
        'Dim d As Long
        'Dim i As Long
        'Dim TmpChannel As Trinity.cChannel
        'Dim TmpBT As Trinity.cBookingType
        'Dim TmpWeek As Trinity.cWeek

        Dim _saveToDate As Date = Campaign.Contract.ToDate
        dtTo.MinDate = dtFrom.Value
        If _saveToDate > dtTo.MinDate Then
            dtTo.Value = _saveToDate
        End If
        Campaign.Contract.FromDate = dtFrom.Value

        For Each ch As Trinity.cContractChannel In Campaign.Contract.Channels
            For Each bt As Trinity.cContractBookingtype In ch.BookingTypes(1)
                bt.ActiveFromDate = dtFrom.Value
            Next
        Next

        cmbChannels_SelectedIndexChanged(New Object, New System.EventArgs)

        'For Each TmpChannel In Campaign.Contract.Channels
        '    For Each TmpBT In TmpChannel.BookingTypes
        '        TmpBT.Weeks = New Trinity.cWeeks(Campaign)
        '        For d = Campaign.Contract.FromDate.ToOADate To Campaign.Contract.ToDate.ToOADate Step 7
        '            i = DatePart(DateInterval.WeekOfYear, Date.FromOADate(d), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
        '            If Trinity.Helper.MondayOfWeek(DatePart(DateInterval.Year, Date.FromOADate(d), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays).ToString, DatePart(DateInterval.WeekOfYear, Date.FromOADate(d), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays).ToString).ToOADate > d Then
        '                TmpWeek = TmpBT.Weeks.Add(DatePart(DateInterval.Year, Date.FromOADate(d), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) - 1 & " " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(d), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays).ToString)
        '            Else
        '                TmpWeek = TmpBT.Weeks.Add(DatePart(DateInterval.Year, Date.FromOADate(d), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays).ToString & " " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(d), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays).ToString)
        '            End If
        '            TmpWeek.StartDate = Trinity.Helper.MondayOfWeek(Year(Campaign.Contract.FromDate), i).ToOADate
        '            TmpWeek.EndDate = Trinity.Helper.MondayOfWeek(Year(Campaign.Contract.FromDate), i).ToOADate + 6
        '            If Campaign.Contract.FromDate.ToOADate > TmpWeek.StartDate Then
        '                TmpWeek.StartDate = Campaign.Contract.FromDate.ToOADate
        '            End If
        '            If Campaign.Contract.ToDate.ToOADate < TmpWeek.EndDate Then
        '                TmpWeek.EndDate = Campaign.Contract.ToDate.ToOADate
        '            End If
        '        Next
        '    Next
        'Next
        'dtOverview.MaxDate = dtTo.Value
        'dtOverview.MinDate = dtFrom.Value

    End Sub

    Private Sub dtTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtTo.ValueChanged
        Campaign.Contract.ToDate = dtTo.Value
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        Campaign.Contract.Name = txtName.Text
    End Sub

    Private Sub grdCosts_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdCosts.CellValueNeeded
        'the CellValueNedded is a update sub, it triggers on a number of events (mouse over etc)
        Dim TmpCost As Trinity.cCost = grdCosts.Rows(e.RowIndex).Tag
        Dim TypeArray() As String = {"Fixed", "Percent", "Per Unit", "On Discount"}

        If TmpCost Is Nothing Then Exit Sub

        If e.ColumnIndex = 0 Then
            e.Value = TmpCost.CostName
        ElseIf e.ColumnIndex = 1 Then
            e.Value = TypeArray(TmpCost.CostType)
        ElseIf e.ColumnIndex = 2 Then
            If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent OrElse TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeOnDiscount Then
                e.Value = Format(TmpCost.Amount, "P2")
            Else
                e.Value = Format(TmpCost.Amount, "##,##0 kr")
            End If
        ElseIf e.ColumnIndex = 3 Then
            Dim TmpCell As DataGridViewComboBoxCell = grdCosts.Rows(e.RowIndex).Cells("colCostOn")
            If grdCosts.Rows(e.RowIndex).Cells(1).Value = "Fixed" AndAlso (TmpCell.Items.Count = 0 OrElse TmpCell.Items(0) <> "-") Then
                TmpCell.Items.Clear()
                TmpCell.Items.Add("-")
            ElseIf grdCosts.Rows(e.RowIndex).Cells(1).Value = "Percent" AndAlso (TmpCell.Items.Count = 0 OrElse TmpCell.Items(0) <> "Media Net") Then
                TmpCell.Items.Clear()
                TmpCell.Items.Add("Media Net")
                TmpCell.Items.Add("Net")
                TmpCell.Items.Add("Net Net")
                TmpCell.Items.Add("Ratecard")
            ElseIf grdCosts.Rows(e.RowIndex).Cells(1).Value = "Per Unit" AndAlso (TmpCell.Items.Count = 0 OrElse TmpCell.Items(0) <> "Spots") Then
                TmpCell.Items.Clear()
                TmpCell.Items.Add("Spots")
                TmpCell.Items.Add("Buy TRP")
                TmpCell.Items.Add("Main TRP")
            ElseIf grdCosts.Rows(e.RowIndex).Cells(1).Value = "On Discount" AndAlso (TmpCell.Items.Count = 0 OrElse TmpCell.Items(0) <> "All") Then
                TmpCell.Items.Clear()
                TmpCell.Items.Add("All")
                For Each TmpChan As Trinity.cContractChannel In Campaign.Contract.Channels
                    TmpCell.Items.Add(TmpChan.Shortname)
                Next
            End If
            e.Value = TmpCost.CostOnText
        ElseIf e.ColumnIndex = 4 Then
            e.Value = TmpCost.MarathonID
        End If
    End Sub

    Private Sub grdCosts_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdCosts.CellValuePushed
        'The CellValuePushed event it triggered when the user alters the table
        Dim TmpCost As Trinity.cCost = grdCosts.Rows(e.RowIndex).Tag
        If TmpCost Is Nothing Then Exit Sub

        If e.ColumnIndex = 0 Then
            TmpCost.CostName = e.Value
        ElseIf e.ColumnIndex = 1 Then
            If e.Value = "Fixed" Then
                TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeFixed
            ElseIf e.Value = "Percent" Then
                TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent
            ElseIf e.Value = "Per Unit" Then
                TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePerUnit
            ElseIf e.Value = "On Discount" Then
                TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeOnDiscount
            End If
        ElseIf e.ColumnIndex = 2 Then
            If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent OrElse TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeOnDiscount Then
                TmpCost.Amount = e.Value.ToString.Replace("%", "") / 100
            Else
                TmpCost.Amount = e.Value
            End If
        ElseIf e.ColumnIndex = 3 Then
            If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeFixed Then
                TmpCost.CountCostOn = 0
            ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                If e.Value = "Media Net" Then
                    TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnMediaNet
                ElseIf e.Value = "Net Net" Then
                    TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNetNet
                ElseIf e.Value = "Net" Then
                    TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNet
                ElseIf e.Value = "Ratecard" Then
                    TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnRatecard
                End If
            ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePerUnit Then
                If e.Value = "Spots" Then
                    TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnSpots
                ElseIf e.Value = "Buy TRP" Then
                    TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnBuyingTRP
                ElseIf e.Value = "Main TRP" Then
                    TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnMainTRP
                End If
            ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeOnDiscount Then
                If e.Value = "All" Then
                    TmpCost.CountCostOn = Nothing
                Else
                    TmpCost.CountCostOn = Campaign.Contract.Channels(LongName(e.Value))
                End If
            End If
        ElseIf e.ColumnIndex = 4 Then
            TmpCost.MarathonID = e.Value
        End If
    End Sub

    'Private Sub grdCosts_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCosts.CellValueChanged
    '    If e.RowIndex > -1 And e.RowIndex < grdCosts.Rows.Count Then
    '        If e.ColumnIndex = 0 Then
    '            Dim TmpCell As DataGridViewTextBoxCell = grdCosts.Rows(e.RowIndex).Cells(e.ColumnIndex)
    '            Campaign.Contract.Costs(e.RowIndex + 1).CostName = TmpCell.Value
    '        ElseIf e.ColumnIndex = 1 Then
    '            Dim TmpCell As DataGridViewComboBoxCell = grdCosts.Rows(e.RowIndex).Cells("colCostOn")
    '            TmpCell.Items.Clear()
    '            If grdCosts.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Fixed" Then
    '                TmpCell.Items.Add("-")
    '                Campaign.Contract.Costs(e.RowIndex + 1).CostType = Trinity.cCost.CostTypeEnum.CostTypeFixed
    '            ElseIf grdCosts.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Percent" Then
    '                TmpCell.Items.Add("Media Net")
    '                TmpCell.Items.Add("Net")
    '                TmpCell.Items.Add("Net Net")
    '                Campaign.Contract.Costs(e.RowIndex + 1).CostType = Trinity.cCost.CostTypeEnum.CostTypePercent
    '            ElseIf grdCosts.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Per Unit" Then
    '                TmpCell.Items.Add("Spots")
    '                TmpCell.Items.Add("Buy TRP")
    '                TmpCell.Items.Add("Main TRP")
    '                Campaign.Contract.Costs(e.RowIndex + 1).CostType = Trinity.cCost.CostTypeEnum.CostTypePerUnit
    '            End If
    '            TmpCell.Value = TmpCell.Items(0).ToString
    '        ElseIf e.ColumnIndex = 2 Then
    '            Dim TmpCell As DataGridViewTextBoxCell = grdCosts.Rows(e.RowIndex).Cells(e.ColumnIndex)
    '            Campaign.Contract.Costs(e.RowIndex + 1).Amount = TmpCell.Value
    '        ElseIf e.ColumnIndex = 3 Then
    '            Dim TmpCell As DataGridViewComboBoxCell = grdCosts.Rows(e.RowIndex).Cells(e.ColumnIndex)
    '            If grdCosts.Rows(e.RowIndex).Cells("colType").Value = "Fixed" Then
    '                Campaign.Contract.Costs(e.RowIndex + 1).CountCostOn = 0
    '            ElseIf grdCosts.Rows(e.RowIndex).Cells("colType").Value = "Percent" Then
    '                If TmpCell.Value = "Media Net" Then
    '                    Campaign.Contract.Costs(e.RowIndex + 1).CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnMediaNet
    '                ElseIf TmpCell.Value = "Net" Then
    '                    Campaign.Contract.Costs(e.RowIndex + 1).CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNet
    '                ElseIf TmpCell.Value = "Net Net" Then
    '                    Campaign.Contract.Costs(e.RowIndex + 1).CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNetNet
    '                End If
    '            ElseIf grdCosts.Rows(e.RowIndex).Cells("colType").Value = "Per Unit" Then
    '                If TmpCell.Value = "Spots" Then
    '                    Campaign.Contract.Costs(e.RowIndex + 1).CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnSpots
    '                ElseIf TmpCell.Value = "Buy TRP" Then
    '                    Campaign.Contract.Costs(e.RowIndex + 1).CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnBuyingTRP
    '                ElseIf TmpCell.Value = "Main TRP" Then
    '                    Campaign.Contract.Costs(e.RowIndex + 1).CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnMainTRP
    '                End If
    '            End If
    '        ElseIf e.ColumnIndex = 4 Then
    '            Dim TmpCell As DataGridViewTextBoxCell = grdCosts.Rows(e.RowIndex).Cells(e.ColumnIndex)
    '            Campaign.Contract.Costs(e.RowIndex + 1).MarathonID = TmpCell.Value
    '        End If
    '    End If
    '    Saved = False
    'End Sub

    Private Sub cmbBookingtypes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBookingtypes.SelectedIndexChanged
        cmbLevel.Items.Clear()
        For i As Integer = 1 To DirectCast(cmbBookingtypes.SelectedItem, Trinity.cContractBookingtype).ParentChannel.LevelCount
            cmbLevel.Items.Add("Level " & i)
        Next
        cmbLevel.SelectedIndex = 0
        txtMaxDiscount.Text = DirectCast(cmbBookingtypes.SelectedItem, Trinity.cContractBookingtype).MaxDiscount * 100
        chkRatecard.Checked = Not DirectCast(cmbBookingtypes.SelectedItem, Trinity.cContractBookingtype).RatecardCPPIsGross

        'add the daypart columns to the targetgrid
        grdTargets.Rows.Clear()
        While grdTargets.ColumnCount > 3
            grdTargets.Columns.RemoveAt(3)
        End While
        Dim TmpBT As Trinity.cBookingType = Campaign.Channels(DirectCast(cmbBookingtypes.SelectedItem, Trinity.cContractBookingtype).ParentChannel.ChannelName).BookingTypes(DirectCast(cmbBookingtypes.SelectedItem, Trinity.cContractBookingtype).Name)
        For i As Integer = 0 To TmpBT.Dayparts.Count - 1
            If grdTargets.Columns.Contains("col" & TmpBT.Dayparts(i).Name) Then
                grdTargets.Columns.Remove("col" & TmpBT.Dayparts(i).Name)
            End If
            grdTargets.Columns.Add("col" & TmpBT.Dayparts(i).Name, TmpBT.Dayparts(i).Name)
            grdTargets.Columns(grdTargets.Columns.Count - 1).Width = 65
        Next

        cmbLevel_SelectedIndexChanged(cmbLevel, New EventArgs())

    End Sub

    Private Sub grdTargets_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTargets.CellEnter
        Dim TmpCTarget As Trinity.cContractTarget = grdTargets.Rows(e.RowIndex).Tag
        If TmpCTarget Is Nothing Then Exit Sub

        Dim TmpTarget As Trinity.cPricelistTarget
        Dim Chan As String
        Dim BT As String

        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        Chan = TmpBT.ParentChannel.ChannelName
        BT = TmpBT.Name

        If e.ColumnIndex = 0 And e.RowIndex > -1 Then
            Dim cellTarget As DataGridViewComboBoxCell = grdTargets.Rows(e.RowIndex).Cells(0)
            cellTarget.Items.Clear()
            For Each TmpTarget In Campaign.Channels(Chan).BookingTypes(BT).Pricelist.Targets
                If Not TmpCTarget.Bookingtype.ContractTargets.Contains(TmpTarget.TargetName) OrElse TmpCTarget.TargetName = TmpTarget.TargetName Then
                    cellTarget.Items.Add(TmpTarget.TargetName)
                End If
            Next
        End If
    End Sub

    Private Sub cmdRemoveTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelTarget.Click
        If grdTargets.SelectedRows.Count = 0 Then Exit Sub

        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag
        Dim TmpCTarget As Trinity.cContractTarget = grdTargets.SelectedRows(0).Tag

        TmpBT.ContractTargets.Remove(TmpCTarget.TargetName)

        grdTargets.Rows.Remove(grdTargets.SelectedRows(0))

        cmbLevel_SelectedIndexChanged(New Object, New EventArgs)

    End Sub

    Private Sub cmdAddIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddIndex.Click
        If cmbBookingtypes.SelectedIndex = -1 Then Exit Sub

        Dim ID As String

        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        If cmbIndexTarget.SelectedIndex = 0 Then
            ID = TmpBT.Indexes.Add("").ID

            TmpBT.Indexes(ID).FromDate = Campaign.Contract.FromDate
            TmpBT.Indexes(ID).ToDate = Campaign.Contract.ToDate
            TmpBT.Indexes(ID).Index = 100
            grdIndexes.Rows.Add()
            grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = TmpBT.Indexes(ID)
        Else
            Dim TmpTarget As Trinity.cContractTarget = cmbIndexTarget.SelectedItem
            ID = TmpTarget.Indexes.Add("").ID

            TmpTarget.Indexes(ID).FromDate = Campaign.Contract.FromDate
            TmpTarget.Indexes(ID).ToDate = Campaign.Contract.ToDate
            TmpTarget.Indexes(ID).Index = 100
            grdIndexes.Rows.Add()
            grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = TmpTarget.Indexes(ID)
        End If


    End Sub

    Private Sub grdFilmIndex_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdFilmIndex.CellValueNeeded
        If e.RowIndex = -1 Or e.ColumnIndex = -1 Then Exit Sub

        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        If e.ColumnIndex = 0 Then
            e.Value = grdFilmIndex.Rows(e.RowIndex).Cells(0).Tag
        Else
            e.Value = grdFilmIndex.Rows(e.RowIndex).Cells(1).Tag
        End If
    End Sub

    Private Sub grdFilmIndex_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdFilmIndex.CellValuePushed
        If e.RowIndex = -1 Or e.ColumnIndex = -1 Then Exit Sub

        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        If e.ColumnIndex = 0 Then
            grdFilmIndex.Rows(e.RowIndex).Tag = e.Value
            grdFilmIndex.Rows(e.RowIndex).Cells(0).Tag = e.Value
            TmpBT.FilmIndex(grdFilmIndex.Rows(e.RowIndex).Tag) = 0
            TmpBT.FilmIndex(e.Value) = grdFilmIndex.Rows(e.RowIndex).Cells(1).Value

        Else
            grdFilmIndex.Rows(e.RowIndex).Cells(1).Tag = e.Value
            TmpBT.FilmIndex(grdFilmIndex.Rows(e.RowIndex).Tag) = e.Value
        End If

        grdFilmIndex.Invalidate()
    End Sub

    Private Sub OnlyOneMarkedGrid(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdTargets.GotFocus, grdAddedValues.GotFocus, grdIndexes.GotFocus, grdFilmIndex.GotFocus
        If sender Is Nothing Then
            grdAddedValues.ClearSelection()
            grdIndexes.ClearSelection()
            grdTargets.ClearSelection()
            grdFilmIndex.ClearSelection()
        Else
            Dim grid As Windows.Forms.DataGridView
            grid = sender

            Select Case grid.Name
                Case Is = "grdTargets"
                    grdAddedValues.ClearSelection()
                    grdIndexes.ClearSelection()
                    grdFilmIndex.ClearSelection()
                Case Is = "grdIndexes"
                    grdAddedValues.ClearSelection()
                    grdTargets.ClearSelection()
                    grdFilmIndex.ClearSelection()
                Case Is = "grdAddedValues"
                    grdIndexes.ClearSelection()
                    grdTargets.ClearSelection()
                    grdFilmIndex.ClearSelection()
                Case Is = "grdFilmIndex"
                    grdAddedValues.ClearSelection()
                    grdIndexes.ClearSelection()
                    grdTargets.ClearSelection()
            End Select
        End If
    End Sub

    Private Sub cmdRemoveIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveIndex.Click
        If cmbBookingtypes.SelectedIndex = -1 Then Exit Sub
        If grdIndexes.SelectedRows.Count = 0 Then Exit Sub

        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag
        Dim TmpIndex As Trinity.cIndex

        For Each row As DataGridViewRow In grdIndexes.SelectedRows
            TmpIndex = row.Tag
            If cmbIndexTarget.SelectedIndex = 0 Then
                TmpBT.Indexes.Remove(TmpIndex.ID)
            Else
                DirectCast(cmbIndexTarget.SelectedItem, Trinity.cContractTarget).Indexes.Remove(TmpIndex.ID)
            End If
            grdIndexes.Rows.Remove(row)
        Next
    End Sub

    Private Sub cmdWizard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWizard.Click
        mnuWizard.Show(cmdWizard, New System.Drawing.Point(0, cmdWizard.Height))
    End Sub

    Private Sub cmdAddAV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddAV.Click
        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        Dim AV As Trinity.cAddedValue

        AV = TmpBT.AddedValues.Add("")

        AV.IndexNet = 100
        AV.IndexGross = 100

        grdAddedValues.Rows.Add()
        grdAddedValues.Rows(grdAddedValues.Rows.Count - 1).Tag = AV

    End Sub

    Private Sub mnuWeekOnFixedCPP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuWeekOnFixedCPP.Click
        Dim d As Long
        Dim TmpIndex As Trinity.cIndex
        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        For d = Campaign.Contract.FromDate.ToOADate To Campaign.Contract.ToDate.ToOADate Step 7
            If cmbIndexTarget.SelectedIndex = 0 Then
                TmpIndex = TmpBT.Indexes.Add("Week " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(d), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays))
                TmpIndex.FixedCPP = 0
            Else
                Dim TmpTarget As Trinity.cContractTarget = cmbIndexTarget.SelectedItem
                TmpIndex = TmpTarget.Indexes.Add("Week " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(d), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays))
                TmpIndex.FixedCPP = 0
                If TmpTarget.IsEntered = Trinity.cContractTarget.EnteredEnum.eCPP Then TmpIndex.FixedCPP = TmpTarget.EnteredValue
            End If
            TmpIndex.FromDate = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 1)
            TmpIndex.ToDate = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 7)
            TmpIndex.Index = 100
            TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eFixedCPP
        Next
        cmbIndexTarget_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub mnuMonthOnFixedCPP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMonthOnFixedCPP.Click
        Dim d As Long
        Dim Months() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}

        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        Dim TmpIndex As Trinity.cIndex
        For d = Campaign.Contract.FromDate.ToOADate To Campaign.Contract.ToDate.ToOADate Step 31
            If cmbIndexTarget.SelectedIndex = 0 Then
                TmpIndex = TmpBT.Indexes.Add(Months(Month(Date.FromOADate(d)) - 1))
                TmpIndex.FixedCPP = 0
            Else
                Dim TmpTarget As Trinity.cContractTarget = cmbIndexTarget.SelectedItem
                TmpIndex = TmpTarget.Indexes.Add(Months(Month(Date.FromOADate(d)) - 1))
                TmpIndex.FixedCPP = 0
                If TmpTarget.IsEntered = Trinity.cContractTarget.EnteredEnum.eCPP Then TmpIndex.FixedCPP = TmpTarget.EnteredValue
            End If
            TmpIndex.FromDate = Date.FromOADate(d - DatePart(DateInterval.Day, Date.FromOADate(d)) + 1)
            TmpIndex.ToDate = CDate(Year(Date.FromOADate(d)) & "-" & Month(Date.FromOADate(d)) & "-" & DatePart(DateInterval.Day, Date.FromOADate(d + 31 - DatePart(DateInterval.Day, Date.FromOADate(d + 31)))))
            TmpIndex.Index = 100
            TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eFixedCPP
            grdIndexes.Rows.Add()
            grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = TmpIndex
        Next
        cmbIndexTarget_SelectedIndexChanged(New Object, New EventArgs)

    End Sub

    Private Sub mnuWeekOnNetCPP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuWeekOnNetCPP.Click
        Dim d As Long
        Dim TmpIndex As Trinity.cIndex
        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        For d = Campaign.Contract.FromDate.ToOADate To Campaign.Contract.ToDate.ToOADate Step 7
            TmpIndex = TmpBT.Indexes.Add("Week " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(d), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays))
            TmpIndex.FromDate = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 1)
            TmpIndex.ToDate = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 7)
            TmpIndex.Index = 100
            TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP
        Next
        cmbIndexTarget_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub mnuWeekOnGrossCPP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuWeekOnGrossCPP.Click
        Dim d As Long
        Dim TmpIndex As Trinity.cIndex
        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        For d = Campaign.Contract.FromDate.ToOADate To Campaign.Contract.ToDate.ToOADate Step 7
            TmpIndex = TmpBT.Indexes.Add("Week " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(d), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays))
            TmpIndex.FromDate = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 1)
            TmpIndex.ToDate = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 7)
            TmpIndex.Index = 100
            TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP
        Next
        cmbIndexTarget_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub mnuWeekOnTRP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuWeekOnTRP.Click
        Dim d As Long
        Dim TmpIndex As Trinity.cIndex
        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        For d = Campaign.Contract.FromDate.ToOADate To Campaign.Contract.ToDate.ToOADate Step 7
            TmpIndex = TmpBT.Indexes.Add("Week " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(d), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays))
            TmpIndex.FromDate = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 1)
            TmpIndex.ToDate = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 7)
            TmpIndex.Index = 100
            TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eTRP
        Next
        cmbIndexTarget_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub mnuMonthOnGrossCPP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuMonthOnGrossCPP.Click
        Dim d As Long
        Dim Months() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}

        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        Dim TmpIndex As Trinity.cIndex
        For d = Campaign.Contract.FromDate.ToOADate To Campaign.Contract.ToDate.ToOADate Step 31
            TmpIndex = TmpBT.Indexes.Add(Months(Month(Date.FromOADate(d)) - 1))
            TmpIndex.FromDate = Date.FromOADate(d - DatePart(DateInterval.Day, Date.FromOADate(d)) + 1)
            TmpIndex.ToDate = CDate(Year(Date.FromOADate(d)) & "-" & Month(Date.FromOADate(d)) & "-" & DatePart(DateInterval.Day, Date.FromOADate(d + 31 - DatePart(DateInterval.Day, Date.FromOADate(d + 31)))))
            TmpIndex.Index = 100
            TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP
            grdIndexes.Rows.Add()
            grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = TmpIndex
        Next
        cmbIndexTarget_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub mnuMonthOnTRP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuMonthOnTRP.Click
        Dim d As Long
        Dim Months() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}

        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        Dim TmpIndex As Trinity.cIndex
        For d = Campaign.Contract.FromDate.ToOADate To Campaign.Contract.ToDate.ToOADate Step 31
            TmpIndex = TmpBT.Indexes.Add(Months(Month(Date.FromOADate(d)) - 1))
            TmpIndex.FromDate = Date.FromOADate(d - DatePart(DateInterval.Day, Date.FromOADate(d)) + 1)
            TmpIndex.ToDate = CDate(Year(Date.FromOADate(d)) & "-" & Month(Date.FromOADate(d)) & "-" & DatePart(DateInterval.Day, Date.FromOADate(d + 31 - DatePart(DateInterval.Day, Date.FromOADate(d + 31)))))
            TmpIndex.Index = 100
            TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eTRP
            grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = TmpIndex
        Next
        cmbIndexTarget_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub mnuMonthOnNetCPP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMonthOnNetCPP.Click
        Dim d As Long
        Dim Months() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}

        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        Dim TmpIndex As Trinity.cIndex
        For d = Campaign.Contract.FromDate.ToOADate To Campaign.Contract.ToDate.ToOADate Step 31
            TmpIndex = TmpBT.Indexes.Add(Months(Month(Date.FromOADate(d)) - 1))
            TmpIndex.FromDate = Date.FromOADate(d - DatePart(DateInterval.Day, Date.FromOADate(d)) + 1)
            TmpIndex.ToDate = CDate(Year(Date.FromOADate(d)) & "-" & Month(Date.FromOADate(d)) & "-" & DatePart(DateInterval.Day, Date.FromOADate(d + 31 - DatePart(DateInterval.Day, Date.FromOADate(d + 31)))))
            TmpIndex.Index = 100
            TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP
            grdIndexes.Rows.Add()
            grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = TmpIndex
        Next
        cmbIndexTarget_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub txtNotes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNotes.TextChanged
        'updates the saved string whenever a change is made
        Campaign.Contract.Description = txtNotes.Text
    End Sub

    Private Sub SaveContract()
        Dim FileName As String = ""

        If Campaign.Contract.Path = "" Then
            dlgContract.OverwritePrompt = True
            If dlgContract.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
            FileName = dlgContract.FileName
        Else
            If My.Computer.FileSystem.FileExists(Campaign.Contract.Path) Then
                FileName = Campaign.Contract.Path
            Else
                Windows.Forms.MessageBox.Show("The contract you are saving has been moved." + vbCrLf + "Please specify the new location.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
                dlgContract.OverwritePrompt = True
                If dlgContract.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
                FileName = dlgContract.FileName
            End If
        End If

        If Campaign.Contract.Save(FileName) = "FALSE" Then
            MsgBox("Contract was NOT saved.", vbInformation, "T R I N I T Y")
        Else
            MsgBox("Contract was saved.", vbInformation, "T R I N I T Y")
        End If
    End Sub

    Private Sub SaveContractAs()
        Dim FileName As String = ""
        Dim fileDialog As Windows.Forms.SaveFileDialog = New Windows.Forms.SaveFileDialog
        fileDialog.CheckPathExists = False
        fileDialog.FileName = "*.tct"
        fileDialog.Filter = "Trinity contracts (*.tct)|*.tct"
        fileDialog.Title = "Save contract as..."
        fileDialog.ShowDialog()
        FileName = fileDialog.FileName

        If Campaign.Contract.Save(FileName) = "FALSE" Then
            MsgBox("Contract was NOT saved.", vbInformation, "T R I N I T Y")
        Else
            MsgBox("Contract was saved.", vbInformation, "T R I N I T Y")
        End If
    End Sub
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Dim mnuSaveOrSaveAs As New ContextMenuStrip
        Dim mnuSave As New ToolStripMenuItem("Save")
        Dim mnuSaveAs As New ToolStripMenuItem("Save As")
        AddHandler mnuSave.Click, AddressOf SaveContract
        AddHandler mnuSaveAs.Click, AddressOf SaveContractAs
        mnuSaveOrSaveAs.Items.Add(mnuSave)
        mnuSaveOrSaveAs.Items.Add(mnuSaveAs)
        mnuSaveOrSaveAs.Show(MousePosition)



    End Sub

    Private Sub cmdRemoveAV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveAV.Click
        If grdAddedValues.SelectedRows.Count = 0 Then Exit Sub

        'deletes added values
        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag
        Dim TmpAV As Trinity.cAddedValue
        For Each row As DataGridViewRow In grdAddedValues.SelectedRows
            TmpAV = row.Tag
            TmpBT.AddedValues.Remove(TmpAV.ID)
            grdAddedValues.Rows.Remove(row)
        Next
    End Sub

    Private Sub cmdDeleteCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteCost.Click
        If grdCosts.SelectedRows.Count = 0 Then
            System.Windows.Forms.MessageBox.Show("No cost selected.", "TRINITY", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        For Each TmpRow As DataGridViewRow In grdCosts.SelectedRows
            Campaign.Contract.Costs.Remove(TmpRow.Tag.ID)
            grdCosts.Rows.Remove(TmpRow)
        Next
    End Sub

    Private Sub cmdAddCost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddCost.Click
        Dim TmpCost As Trinity.cCost
        TmpCost = Campaign.Contract.Costs.Add("", 0, 0, 0, 0)
        grdCosts.Rows.Add()
        grdCosts.Rows(grdCosts.Rows.Count - 1).Tag = TmpCost
    End Sub

    Private Sub tpCombinations_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpCombinations.Enter
        grdCombos.Rows.Clear()
        For Each TmpCombo As Trinity.cCombination In Campaign.Contract.Combinations
            With grdCombos.Rows(grdCombos.Rows.Add)
                .Tag = TmpCombo
            End With
        Next
        grpCombo.Visible = False

    End Sub

    Private Sub grdCombos_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdCombos.CellValueNeeded
        Dim TmpCombo As Trinity.cCombination = grdCombos.Rows(e.RowIndex).Tag
        If e.ColumnIndex = 0 Then
            e.Value = TmpCombo.Name
        Else
            Dim TmpStr As String = ""
            For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                TmpStr &= TmpCC.ToString & ","
            Next
            TmpStr = TmpStr.TrimEnd(",")
            e.Value = TmpStr
        End If
    End Sub

    Private Sub grdCombos_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCombos.RowEnter
        If grdCombos.Rows(e.RowIndex).Tag Is Nothing Then Exit Sub
        Dim TmpCombo As Trinity.cCombination = Campaign.Contract.Combinations(grdCombos.Rows(e.RowIndex).Tag.ID)
        If TmpCombo Is Nothing Then Exit Sub
        grpCombo.Tag = TmpCombo
        grdCombo.Rows.Clear()
        For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
            With grdCombo.Rows(grdCombo.Rows.Add)
                .Tag = TmpCC
            End With
        Next
        txtComboName.Text = TmpCombo.Name
        If TmpCombo.CombinationOn = Trinity.cCombination.CombinationOnEnum.coBudget Then
            optBudget.Checked = True
        Else
            optTRP.Checked = True
        End If
        grpCombo.Visible = True
        chkShowAsOne.Checked = TmpCombo.ShowAsOne
    End Sub

    Private Sub txtComboName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtComboName.TextChanged
        DirectCast(grpCombo.Tag, Trinity.cCombination).Name = txtComboName.Text
        grdCombos.Invalidate()
    End Sub

    Private Sub optBudget_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optBudget.CheckedChanged
        If DirectCast(grpCombo.Tag, Trinity.cCombination) Is Nothing Then Exit Sub
        DirectCast(grpCombo.Tag, Trinity.cCombination).CombinationOn = -(optTRP.Checked)
    End Sub

    Private Sub optTRP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTRP.CheckedChanged
        If DirectCast(grpCombo.Tag, Trinity.cCombination) Is Nothing Then Exit Sub
        DirectCast(grpCombo.Tag, Trinity.cCombination).CombinationOn = -(optTRP.Checked)
    End Sub


    Private Sub cmdAddChannelToCombo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddChannelToCombo.Click
        With DirectCast(grpCombo.Tag, Trinity.cCombination)
            Dim FirstChannel As Trinity.cContractBookingtype = Nothing
            For Each TmpChan As Trinity.cContractChannel In Campaign.Contract.Channels
                For Each TmpBT As Trinity.cContractBookingtype In TmpChan.BookingTypes(1)
                    FirstChannel = TmpBT
                    For Each TmpCombo As Trinity.cCombination In Campaign.Contract.Combinations
                        For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                            If TmpCC.ToString = TmpBT.ToString Then
                                FirstChannel = Nothing
                            End If
                        Next
                    Next
                    If Not FirstChannel Is Nothing Then
                        Exit For
                    End If
                Next
                If Not FirstChannel Is Nothing Then
                    Exit For
                End If
            Next
            grdCombo.Rows(grdCombo.Rows.Add).Tag = .Relations.Add(FirstChannel, 0)
            grdCombos.Invalidate()
        End With
    End Sub

    Private Sub grdCombo_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCombo.CellEnter
        Dim NextChannel As Trinity.cContractBookingtype
        Dim MyCC As Trinity.cCombinationChannel = grdCombo.Rows(e.RowIndex).Tag
        If e.ColumnIndex = 0 And e.RowIndex > -1 Then
            Dim cellTarget As ExtendedComboBoxCell = grdCombo.Rows(e.RowIndex).Cells(0)
            cellTarget.Items.Clear()
            For Each TmpChan As Trinity.cContractChannel In Campaign.Contract.Channels
                For Each TmpBT As Trinity.cContractBookingtype In TmpChan.BookingTypes(1)
                    NextChannel = TmpBT
                    For Each TmpCombo As Trinity.cCombination In Campaign.Contract.Combinations
                        For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                            If TmpCC.ToString = TmpBT.ToString And TmpCC IsNot MyCC Then
                                NextChannel = Nothing
                            End If
                        Next
                    Next
                    If Not NextChannel Is Nothing Then
                        cellTarget.Items.Add(TmpBT)
                    End If
                Next
            Next
        End If

    End Sub

    Private Sub cmdAddCombo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddCombo.Click
        With grdCombos.Rows(grdCombos.Rows.Add)
            .Tag = Campaign.Contract.Combinations.Add
        End With
    End Sub

    Private Sub grdCombo_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdCombo.CellValueNeeded
        Dim TmpCC As Trinity.cCombinationChannel = grdCombo.Rows(e.RowIndex).Tag
        If e.ColumnIndex = 0 Then
            e.Value = Campaign.Contract.Channels(TmpCC.ChannelName).BookingTypes(1)(TmpCC.BookingTypeName)
        Else
            e.Value = TmpCC.Relation
        End If
        grdCombos.Invalidate()
    End Sub

    Private Sub grdCombo_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdCombo.CellValuePushed
        Dim TmpCC As Trinity.cCombinationChannel = grdCombo.Rows(e.RowIndex).Tag
        If e.ColumnIndex = 0 Then
            TmpCC.ChannelName = DirectCast(e.Value, Trinity.cContractBookingtype).ParentChannel.ChannelName
            TmpCC.BookingTypeName = DirectCast(e.Value, Trinity.cContractBookingtype).Name
        Else
            TmpCC.Relation = e.Value
        End If
        grdCombos.Invalidate()
    End Sub

    Private Sub cmdDeleteCombo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteCombo.Click
        For Each TmpRow As DataGridViewRow In grdCombos.SelectedRows
            Dim TmpCombo As Trinity.cCombination = TmpRow.Tag
            Campaign.Contract.Combinations.Remove(TmpCombo)
            grdCombos.Rows.Remove(TmpRow)
        Next
        grpCombo.Visible = False
    End Sub

    '   Changed 2019-02-20  In order to remove Channels in Contract while creating new combinations//JOOS
    '   From: .Relations.Remove(TmpCC)
    '   To: .Relations.RemoveChannelfromContractCombo(TmpCC)
    Private Sub cmdDeleteChannelFromCombo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteChannelFromCombo.Click
        With DirectCast(grpCombo.Tag, Trinity.cCombination)
            For Each TmpRow As DataGridViewRow In grdCombo.SelectedRows
                Dim TmpCC As Trinity.cCombinationChannel = TmpRow.Tag
                .Relations.RemoveChannelFromContractCombo(TmpCC)
                grdCombo.Rows.Remove(TmpRow)
            Next
        End With
        grdCombos.Invalidate()
    End Sub

    Private Sub cmdCopyIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopyIndex.Click
        Dim mnuCopy As New ContextMenu
        Dim BT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        For Each TmpChan As Trinity.cContractChannel In Campaign.Contract.Channels
            If TmpChan.LevelCount >= cmbLevel.SelectedIndex + 1 Then
                For Each TmpBT As Trinity.cContractBookingtype In TmpChan.BookingTypes(cmbLevel.SelectedIndex + 1)
                    If TmpBT.ToString <> BT.ToString Then
                        Dim added As Boolean = False
                        If TmpBT.Indexes.Count > 0 Then
                            mnuCopy.MenuItems.Add(TmpBT.ToString, AddressOf CopyIndex).Tag = TmpBT
                            added = True
                        End If
                        If Not added Then
                            For Each TmpTarget As Trinity.cContractTarget In TmpBT.ContractTargets
                                If TmpTarget.Indexes.Count > 0 Then
                                    mnuCopy.MenuItems.Add(TmpBT.ToString, AddressOf CopyIndex).Tag = TmpBT
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                Next
            End If
        Next
        mnuCopy.Show(cmdCopyIndex, New System.Drawing.Point(0, cmdCopyIndex.Height))
    End Sub

    Private Sub CopyIndex(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim TmpAddBT As Trinity.cContractBookingtype = sender.tag
        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag
        Dim frm As New frmThreeChoices("An index with this name already exists. What would you like to do?")

        For Each TmpIndex As Trinity.cIndex In TmpAddBT.Indexes

            Dim recievingBT As Trinity.cContractBookingtype = TmpBT
            For Each tmpindex2 As Trinity.cIndex In recievingBT.Indexes
                If recievingBT.Indexes(tmpindex2.ID).Name = TmpIndex.Name Then
                    If Not frm.chkDoForAll.Checked AndAlso recievingBT.Indexes(tmpindex2.ID).Name = TmpIndex.Name Then
                        frm.lblItem.Text = TmpIndex.Name
                        frm.ShowDialog()
                    End If
                End If
            Next

            If Not frm.Result = Windows.Forms.DialogResult.Cancel Then
                With TmpBT
                    Dim _id As String = CreateGUID()
                    If frm.Result = Windows.Forms.DialogResult.OK Then
                        _id = .Indexes(.Indexes.IndexForName(TmpIndex.Name)).ID
                        .Indexes.Remove(.Indexes.IndexForName(TmpIndex.Name))
                    End If
                    With .Indexes.Add(TmpIndex.Name)
                        .ID = _id
                        .FromDate = TmpIndex.FromDate
                        .ToDate = TmpIndex.ToDate
                        .IndexOn = TmpIndex.IndexOn
                        .FixedCPP = TmpIndex.FixedCPP
                        If TmpIndex.Enhancements.Count > 0 Then
                            For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
                                With .Enhancements.Add()
                                    .Name = TmpEnh.Name
                                    .Amount = TmpEnh.Amount
                                End With
                            Next
                            '.Enhancements.SpecificFactor = TmpIndex.Enhancements.SpecificFactor
                        Else
                            .Index = TmpIndex.Index
                        End If
                    End With
                End With
            End If
        Next
        For Each TmpTarget As Trinity.cContractTarget In TmpAddBT.ContractTargets
            If TmpBT.ContractTargets(TmpTarget.TargetName) IsNot Nothing Then
                For Each TmpIndex As Trinity.cIndex In TmpTarget.Indexes

                    Dim recievingTarget As Trinity.cContractTarget = TmpBT.ContractTargets(TmpTarget.TargetName)
                    For Each tmpindex2 As Trinity.cIndex In recievingTarget.Indexes
                        If recievingTarget.Indexes(tmpindex2.ID).Name = TmpIndex.Name Then
                            If Not frm.chkDoForAll.Checked AndAlso recievingTarget.Indexes(tmpindex2.ID).Name = TmpIndex.Name Then
                                frm.lblItem.Text = TmpIndex.Name
                                frm.ShowDialog()
                            End If
                        End If
                    Next

                    If Not frm.Result = Windows.Forms.DialogResult.Cancel Then
                        With TmpBT.ContractTargets(TmpTarget.TargetName)
                            Dim _id As String = CreateGUID()
                            If frm.Result = Windows.Forms.DialogResult.OK Then
                                _id = .Indexes(.Indexes.IndexForName(TmpIndex.Name)).ID
                                .Indexes.Remove(.Indexes.IndexForName(TmpIndex.Name))
                            End If
                            With .Indexes.Add(TmpIndex.Name)
                                .ID = _id
                                .FromDate = TmpIndex.FromDate
                                .ToDate = TmpIndex.ToDate
                                .IndexOn = TmpIndex.IndexOn
                                .FixedCPP = TmpIndex.FixedCPP
                                If TmpIndex.Enhancements.Count > 0 Then
                                    For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
                                        With .Enhancements.Add()
                                            .Name = TmpEnh.Name
                                            .Amount = TmpEnh.Amount
                                        End With
                                    Next
                                    '.Enhancements.SpecificFactor = TmpIndex.Enhancements.SpecificFactor
                                Else
                                    .Index = TmpIndex.Index
                                End If
                            End With
                        End With
                    End If
                Next
            End If
        Next
        cmbLevel_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub CopyFilmIndex(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim TmpAddBT As Trinity.cContractBookingtype = sender.tag
        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        For i As Integer = 1 To 300
            If TmpAddBT.FilmIndex(i) <> 0 Then
                TmpBT.FilmIndex(i) = TmpAddBT.FilmIndex(i)
            End If
        Next
        cmbLevel_SelectedIndexChanged(sender, e)

    End Sub

    Private Sub CopyTarget(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim TmpAddBT As Trinity.cContractBookingtype = sender.tag
        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag


        For Each TmpTarget As Trinity.cContractTarget In TmpAddBT.ContractTargets
            If Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Pricelist.Targets(TmpTarget.TargetName) IsNot Nothing Then
                If TmpBT.ContractTargets(TmpTarget.TargetName) Is Nothing Then
                    With TmpBT.ContractTargets.Add(TmpTarget.TargetName)
                        .AdEdgeTargetName = TmpTarget.AdEdgeTargetName
                        .Bookingtype = TmpBT
                        .CalcCPP = TmpTarget.CalcCPP

                        For i As Integer = 0 To Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Dayparts.Count - 1
                            .DefaultDaypart(i) = TmpTarget.DefaultDaypart(i)
                        Next

                        .EnteredValue = TmpTarget.EnteredValue
                        .TargetType = TmpTarget.TargetType
                        .IsContractTarget = TmpTarget.IsContractTarget
                        .IsEntered = TmpTarget.IsEntered
                        .Indexes = New Trinity.cIndexes(Campaign, TmpTarget)

                        For Each plp As Trinity.cPricelistPeriod In TmpTarget.PricelistPeriods
                            With .PricelistPeriods.Add(plp.Name)
                                .FromDate = plp.FromDate
                                .ToDate = plp.ToDate
                                .TargetNat = plp.TargetNat
                                .TargetUni = plp.TargetUni
                                .PriceIsCPP = plp.PriceIsCPP

                                For i As Integer = 0 To Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Dayparts.Count - 1
                                    .Price(.PriceIsCPP, i) = plp.Price(.PriceIsCPP, i)
                                Next
                            End With
                        Next

                    End With
                Else
                    With TmpBT.ContractTargets(TmpTarget.TargetName)
                        .AdEdgeTargetName = TmpTarget.AdEdgeTargetName
                        .Bookingtype = TmpBT
                        .CalcCPP = TmpTarget.CalcCPP

                        For i As Integer = 0 To Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Dayparts.Count - 1
                            .DefaultDaypart(i) = TmpTarget.DefaultDaypart(i)
                        Next

                        .EnteredValue = TmpTarget.EnteredValue
                        .TargetType = TmpTarget.TargetType
                        .IsContractTarget = TmpTarget.IsContractTarget
                        .IsEntered = TmpTarget.IsEntered
                        .Indexes = New Trinity.cIndexes(Campaign, TmpTarget)

                        For Each plp As Trinity.cPricelistPeriod In TmpTarget.PricelistPeriods
                            With .PricelistPeriods.Add(plp.Name)
                                .FromDate = plp.FromDate
                                .ToDate = plp.ToDate
                                .TargetNat = plp.TargetNat
                                .TargetUni = plp.TargetUni
                                .PriceIsCPP = plp.PriceIsCPP

                                For i As Integer = 0 To Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Dayparts.Count - 1
                                    .Price(.PriceIsCPP, i) = plp.Price(.PriceIsCPP, i)
                                Next
                            End With
                        Next

                    End With
                End If

            End If
        Next
        cmbLevel_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub cmdCopyAV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopyAV.Click
        Dim mnuCopy As New ContextMenu
        Dim BT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        For Each TmpChan As Trinity.cContractChannel In Campaign.Contract.Channels
            For Each TmpBT As Trinity.cContractBookingtype In TmpChan.BookingTypes(cmbLevel.SelectedIndex + 1)
                If TmpBT.AddedValues.Count > 0 And TmpBT.ToString IsNot BT.ToString Then
                    mnuCopy.MenuItems.Add(TmpBT.ToString, AddressOf CopyAV).Tag = TmpBT
                End If
            Next
        Next
        mnuCopy.Show(cmdCopyAV, New System.Drawing.Point(0, cmdCopyAV.Height))
    End Sub

    Sub CopyAV(ByVal sender As Object, ByVal e As EventArgs)
        Dim TmpAddBT As Trinity.cContractBookingtype = sender.tag
        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        For Each TmpAv As Trinity.cAddedValue In TmpAddBT.AddedValues
            With TmpBT
                With .AddedValues.Add(TmpAv.Name)
                    .IndexGross = TmpAv.IndexGross
                    .IndexNet = TmpAv.IndexNet
                    .ShowIn = TmpAv.ShowIn
                End With
            End With
        Next
        cmbLevel_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub chkShowAsOne_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowAsOne.CheckedChanged
        Dim TmpCombo As Trinity.cCombination = grpCombo.Tag
        If TmpCombo Is Nothing Then Exit Sub

        TmpCombo.ShowAsOne = chkShowAsOne.Checked

        'Deprecated: ShowMe now derives from ShowAsOne. See Bookingtype.ShowMe
        ''set that the BT on this channel should (not) be shown
        'For Each tmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
        '    tmpCC.Bookingtype.ShowMe = Not chkShowAsOne.Checked
        'Next
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub cmbChannels_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbChannels.SelectedIndexChanged
        If cmbChannels.SelectedIndex = -1 Then Exit Sub
        grdLevels.Rows.Clear()
        Dim TmpChan As Trinity.cContractChannel = cmbChannels.SelectedItem
        TmpChan.Agencycommission = Campaign.Contract.Channels(TmpChan.ChannelName).Agencycommission
        txtCommission.Text = TmpChan.Agencycommission * 100
        Dim j As Integer
        For i As Integer = 1 To TmpChan.LevelCount
            j = grdLevels.Rows.Add
            grdLevels.Rows(j).Tag = TmpChan.BookingTypes(i)
        Next
        grdLevels.Invalidate()

        grdBT.Rows.Clear()
        For Each TmpBT As Trinity.cContractBookingtype In TmpChan.BookingTypes(1)
            j = grdBT.Rows.Add()
            grdBT.Rows(j).Tag = TmpBT
            If Not TmpBT.IsContractBookingtype Then
                grdBT.Rows(j).ReadOnly = True
            End If
        Next
    End Sub

    Private Sub grdLevels_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLevels.CellEnter
        If e.ColumnIndex = 3 Then
            Dim TmpChan As Trinity.cContractChannel = cmbChannels.SelectedItem
            For Each TmpBT As Trinity.cContractBookingtype In TmpChan.BookingTypes(e.RowIndex + 1)
                If Not TmpBT.Active Then
                    TmpBT.ActiveFromDate = Date.Now
                Else
                    Exit Sub
                End If
            Next
            grdLevels.Invalidate()
        End If
    End Sub

    Private Sub cmdAddLevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddLevel.Click
        If cmbChannels.SelectedIndex = -1 Then Exit Sub

        Dim TmpChan As Trinity.cContractChannel = cmbChannels.SelectedItem

        TmpChan.AddLevel()

        Dim i As Integer = grdLevels.Rows.Add()
        grdLevels.Rows(i).Tag = TmpChan.BookingTypes(TmpChan.LevelCount)
    End Sub

    Private Sub cmdDelLevel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelLevel.Click
        If grdLevels.SelectedRows.Count = 0 Or grdLevels.CurrentRow.Index = 0 Then
            Exit Sub
        End If

        Dim TmpChan As Trinity.cContractChannel = cmbChannels.SelectedItem

        grdLevels.Rows.Remove(grdLevels.SelectedRows(0))
        TmpChan.RemoveLevel(grdLevels.SelectedRows(0).Index + 1)

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedTab Is tpChannelSetup Then
            cmbBookingtypes.Items.Clear()
            For Each TmpChan As Trinity.cContractChannel In Campaign.Contract.Channels
                For Each TmpBT As Trinity.cContractBookingtype In TmpChan.BookingTypes(1)
                    cmbBookingtypes.Items.Add(TmpBT)
                Next
            Next

            cmbBookingtypes.SelectedIndex = 0
            cmbLevel.SelectedIndex = 0
        End If
    End Sub

    Private Sub cmbLevel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLevel.SelectedIndexChanged
        'List all the targets in the Bookingtype
        grdTargets.Rows.Clear()
        grdAddedValues.Rows.Clear()

        Dim TmpBT As Trinity.cContractBookingtype = DirectCast(cmbBookingtypes.SelectedItem, Trinity.cContractBookingtype).ParentChannel.BookingTypes(cmbLevel.SelectedIndex + 1)(DirectCast(cmbBookingtypes.SelectedItem, Trinity.cContractBookingtype).Name)

        'We store the current BT in the tag och the list
        cmbBookingtypes.Tag = TmpBT

        cmbIndexTarget.Items.Clear()
        cmbIndexTarget.Items.Add("All")
        For Each TmpTarget As Trinity.cContractTarget In TmpBT.ContractTargets
            grdTargets.Rows.Add()
            grdTargets.Rows(grdTargets.Rows.Count - 1).Tag = TmpTarget
            cmbIndexTarget.Items.Add(TmpTarget)
        Next
        cmbIndexTarget.SelectedIndex = 0

        For Each TmpAV As Trinity.cAddedValue In TmpBT.AddedValues
            grdAddedValues.Rows.Add()
            grdAddedValues.Rows(grdAddedValues.Rows.Count - 1).Tag = TmpAV
        Next

        grdFilmIndex.Rows.Clear()
        For i As Integer = 0 To 500
            If TmpBT.FilmIndex(i) > 0 Then
                grdFilmIndex.Rows.Add()
                grdFilmIndex.Rows(grdFilmIndex.Rows.Count - 1).Tag = i.ToString
                grdFilmIndex.Rows(grdFilmIndex.Rows.Count - 1).Cells(0).Tag = i.ToString
                grdFilmIndex.Rows(grdFilmIndex.Rows.Count - 1).Cells(1).Tag = TmpBT.FilmIndex(i)
            End If
        Next

    End Sub

    Private Sub grdTargets_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdTargets.CellValueNeeded
        If e.RowIndex = -1 OrElse e.ColumnIndex = -1 Then Exit Sub

        Dim TmpTarget As Trinity.cContractTarget = grdTargets.Rows(e.RowIndex).Tag

        Select Case e.ColumnIndex
            Case Is = 0
                e.Value = TmpTarget.TargetName
            Case Is = 1
                If TmpTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eCPT Then
                    e.Value = "CPT"
                ElseIf TmpTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eCPP Then
                    e.Value = "CPP"
                ElseIf TmpTarget.IsEntered = Trinity.cContractTarget.EnteredEnum.eDiscount Then
                    e.Value = "Discount"
                Else
                    e.Value = "Enhancement"
                End If
            Case Is = 2
                If TmpTarget.IsEntered = Trinity.cContractTarget.EnteredEnum.eDiscount Then
                    e.Value = Format(TmpTarget.EnteredValue, "0.0%")
                Else
                    e.Value = Format(TmpTarget.EnteredValue, "0.0")
                End If
            Case Is > 2
                e.Value = TmpTarget.DefaultDaypart(e.ColumnIndex - 3)
        End Select
    End Sub

    Private Sub grdTargets_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdTargets.CellValuePushed
        If e.RowIndex = -1 OrElse e.ColumnIndex = -1 Then Exit Sub

        Dim TmpTarget As Trinity.cContractTarget = grdTargets.Rows(e.RowIndex).Tag

        Select Case e.ColumnIndex
            Case Is = 0
                TmpTarget.TargetName = e.Value
                cmbIndexTarget.Items(e.RowIndex + 1) = TmpTarget
                'cmbIndexTarget.Invalidate()
            Case Is = 1
                If e.Value = "CPT" Then
                    TmpTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eCPT
                ElseIf e.Value = "CPP" Then
                    TmpTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eCPP
                ElseIf e.Value = "Discount" Then
                    TmpTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eDiscount
                Else
                    TmpTarget.IsEntered = Trinity.cContractTarget.EnteredEnum.eEnhancement
                End If
            Case Is = 2
                Dim numberCandidate As Double
                Try
                    numberCandidate = CDbl(e.Value)
                Catch ex As Exception
                    Exit Sub
                End Try

                If TmpTarget.IsEntered = Trinity.cContractTarget.EnteredEnum.eDiscount Then
                    TmpTarget.EnteredValue = numberCandidate / 100
                Else
                    TmpTarget.EnteredValue = numberCandidate
                End If
            Case Is > 2
                Dim numberCandidate As Double
                Try
                    numberCandidate = CDbl(e.Value)
                Catch ex As Exception

                End Try
                TmpTarget.DefaultDaypart(e.ColumnIndex - 3) = numberCandidate
        End Select
    End Sub

    Private Sub cmdLevelUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLevelUp.Click
        If grdLevels.SelectedRows.Count = 0 Then Exit Sub
        If grdLevels.SelectedRows(0).Index = 0 Then Exit Sub

        Dim TmpBTs As Trinity.cContractBookingtypes
        Dim TmpHolder As Trinity.cContractBookingtypes
        Dim TmpChan As Trinity.cContractChannel = cmbChannels.SelectedItem
        Dim i As Integer = grdLevels.SelectedRows(0).Index + 1

        'get the chosen collection
        TmpBTs = TmpChan.BookingTypes(i)

        'get the collection that holds the old spot
        TmpHolder = TmpChan.BookingTypes(i - 1)

        'set them on eachothers place
        TmpChan.BookingTypes(i - 1) = TmpBTs
        TmpChan.BookingTypes(i) = TmpHolder

        cmbChannels_SelectedIndexChanged(New Object, New System.EventArgs)

        grdLevels.ClearSelection()
        grdLevels.Rows(i - 2).Selected = True
    End Sub

    Private Sub cmdLevelDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLevelDown.Click
        If grdLevels.SelectedRows.Count = 0 Then Exit Sub
        If grdLevels.SelectedRows(0).Index = grdLevels.Rows.Count - 1 Then Exit Sub

        Dim TmpBTs As Trinity.cContractBookingtypes
        Dim TmpHolder As Trinity.cContractBookingtypes
        Dim TmpChan As Trinity.cContractChannel = cmbChannels.SelectedItem
        Dim i As Integer = grdLevels.SelectedRows(0).Index + 1

        'get the chosen collection
        TmpBTs = TmpChan.BookingTypes(i)

        'get the collection that holds the old spot
        TmpHolder = TmpChan.BookingTypes(i + 1)

        'set them on eachothers place
        TmpChan.BookingTypes(i + 1) = TmpBTs
        TmpChan.BookingTypes(i) = TmpHolder

        cmbChannels_SelectedIndexChanged(New Object, New System.EventArgs)

        grdLevels.ClearSelection()
        grdLevels.Rows(i).Selected = True
    End Sub

    Private Sub grdIndexes_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdIndexes.CellValueNeeded
        If e.ColumnIndex = -1 OrElse e.RowIndex = -1 Then Exit Sub

        Dim TmpIndex As Trinity.cIndex = grdIndexes.Rows(e.RowIndex).Tag

        Select Case e.ColumnIndex
            Case Is = 0
                e.Value = TmpIndex.Name
            Case Is = 1
                e.Value = IndexNames(TmpIndex.IndexOn)
            Case Is = 2
                e.Value = TmpIndex.FromDate
            Case Is = 3
                e.Value = TmpIndex.ToDate
            Case Is = 4
                If TmpIndex.IndexOn <> Trinity.cIndex.IndexOnEnum.eFixedCPP Then
                    e.Value = TmpIndex.Index
                Else
                    e.Value = TmpIndex.FixedCPP
                End If
        End Select
    End Sub

    Private Sub grdIndexes_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdIndexes.CellValuePushed
        Dim TmpIndex As Trinity.cIndex = grdIndexes.Rows(e.RowIndex).Tag

        Select Case e.ColumnIndex
            Case Is = 0
                TmpIndex.Name = e.Value
            Case Is = 1
                If e.Value = "Net CPP" Then
                    TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP
                ElseIf e.Value = "Gross CPP" Then
                    TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP
                ElseIf e.Value = "TRP" Then
                    TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eTRP
                ElseIf e.Value = "Fixed CPP" Then
                    TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eFixedCPP
                End If
            Case Is = 2
                TmpIndex.FromDate = e.Value
            Case Is = 3
                TmpIndex.ToDate = e.Value
            Case Is = 4
                If TmpIndex.IndexOn <> Trinity.cIndex.IndexOnEnum.eFixedCPP Then
                    TmpIndex.Index = e.Value
                Else
                    TmpIndex.FixedCPP = e.Value
                End If
        End Select
    End Sub

    Private Sub grdTargets_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grdTargets.RowsAdded
        Dim TmpCTarget As Trinity.cContractTarget = grdTargets.Rows(e.RowIndex).Tag

        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag
        Dim Chan As String
        Dim BT As String
        Chan = TmpBT.ParentChannel.ChannelName
        BT = TmpBT.Name
        Dim TmpTarget As Trinity.cPricelistTarget

        If TmpCTarget Is Nothing Then
            Dim target As Trinity.cPricelistTarget
            Dim cellTarget As DataGridViewComboBoxCell = grdTargets.Rows(grdTargets.Rows.Count - 1).Cells(0)
            cellTarget.Items.Clear()
            If Campaign.Channels(Chan) IsNot Nothing AndAlso Campaign.Channels(Chan).BookingTypes(BT) IsNot Nothing Then
                For Each target In Campaign.Channels(Chan).BookingTypes(BT).Pricelist.Targets
                    cellTarget.Items.Add(target.TargetName)
                Next
            End If
            For Each _target As Trinity.cContractTarget In Campaign.Contract.Channels(Chan).BookingTypes(cmbLevel.SelectedIndex + 1)(BT).ContractTargets
                cellTarget.Items.Add(_target.TargetName)
            Next

        Else
            Dim cellTarget As DataGridViewComboBoxCell = grdTargets.Rows(e.RowIndex).Cells(0)
            cellTarget.Items.Clear()
            For Each TmpTarget In Campaign.Channels(Chan).BookingTypes(BT).Pricelist.Targets
                If Not TmpCTarget.Bookingtype.ContractTargets.Contains(TmpTarget.TargetName) OrElse TmpCTarget.TargetName = TmpTarget.TargetName Then
                    cellTarget.Items.Add(TmpTarget.TargetName)
                End If
            Next
        End If
    End Sub

    Private Sub grdAddedValues_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdAddedValues.CellValueNeeded
        If e.ColumnIndex = -1 OrElse e.RowIndex = -1 Then Exit Sub

        Dim AV As Trinity.cAddedValue = grdAddedValues.Rows(e.RowIndex).Tag
        Select Case e.ColumnIndex
            Case Is = 0
                e.Value = AV.Name
            Case Is = 1
                e.Value = AV.IndexGross
            Case Is = 2
                e.Value = AV.IndexNet
            Case Is = 3
                e.Value = ShowInNames(AV.ShowIn)
        End Select
    End Sub

    Private Sub grdAddedValues_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdAddedValues.CellValuePushed
        If e.ColumnIndex = -1 OrElse e.RowIndex = -1 Then Exit Sub

        Dim AV As Trinity.cAddedValue = grdAddedValues.Rows(e.RowIndex).Tag
        Select Case e.ColumnIndex
            Case Is = 0
                AV.Name = e.Value
            Case Is = 1
                AV.IndexGross = e.Value
            Case Is = 2
                AV.IndexNet = e.Value
            Case Is = 3
                Select Case grdAddedValues.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                    Case "Both"
                        AV.ShowIn = Trinity.cAddedValue.ShowInEnum.siBoth
                    Case "Allocate"
                        AV.ShowIn = Trinity.cAddedValue.ShowInEnum.siAllocate
                    Case "Booking"
                        AV.ShowIn = Trinity.cAddedValue.ShowInEnum.siBooking
                End Select
        End Select
    End Sub

    Private Sub cmdAddFilmIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddFilmIndex.Click
        grdFilmIndex.Rows.Add()
    End Sub

    Private Sub cmdDelFilmIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelFilmIndex.Click
        If grdFilmIndex.SelectedRows.Count = 0 Then Exit Sub

        Dim TmpBTs As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        TmpBTs.FilmIndex(grdFilmIndex.SelectedRows(0).Tag) = 0
        grdFilmIndex.Rows.Remove(grdFilmIndex.SelectedRows(0))
    End Sub

    Private Sub AddTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddTarget.Click
        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.SelectedItem
        Dim frm As New frmContractTarget(TmpBT)
        If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim TmpTarget As Trinity.cContractTarget
            'we need to add the BT on all Levels
            'Level one is already set
            For i As Integer = 1 To TmpBT.ParentChannel.LevelCount
                For z As Integer = 1 To frm.TmpBT.ContractTargets.Count
                    TmpTarget = frm.TmpBT.ContractTargets(z)
                    If TmpTarget.IsContractTarget Then
                        If TmpBT.ParentChannel.BookingTypes(i)(TmpBT.Name).ContractTargets.Contains(TmpTarget.TargetName) Then
                            TmpBT.ParentChannel.BookingTypes(i)(TmpBT.Name).ContractTargets.Remove(TmpTarget.TargetName)
                        End If

                        Dim TmpNewTarget As Trinity.cContractTarget = TmpBT.ParentChannel.BookingTypes(i)(TmpBT.Name).ContractTargets.Add(TmpTarget.TargetName)
                        TmpNewTarget.CalcCPP = TmpTarget.CalcCPP
                        TmpNewTarget.IsContractTarget = True
                        TmpNewTarget.TargetType = TmpTarget.TargetType
                        For Each TmpPeriod As Trinity.cPricelistPeriod In TmpTarget.PricelistPeriods
                            Dim period As Trinity.cPricelistPeriod = TmpNewTarget.PricelistPeriods.Add(TmpPeriod.Name)
                            period.FromDate = TmpPeriod.FromDate
                            period.ToDate = TmpPeriod.ToDate
                            period.PriceIsCPP = TmpPeriod.PriceIsCPP
                            period.TargetNat = TmpPeriod.TargetNat
                            period.TargetUni = TmpPeriod.TargetUni
                            For j As Integer = 0 To Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Dayparts.Count - 1
                                period.Price(j) = TmpPeriod.Price(j)
                            Next
                        Next

                        If Not Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Pricelist.Targets.Contains(TmpTarget.TargetName) Then
                            Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name).Pricelist.Targets.Add(TmpTarget.TargetName, Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(TmpBT.Name))
                        End If
                    End If
                Next
            Next
        End If
        cmbLevel_SelectedIndexChanged(New Object, New System.EventArgs)
    End Sub

    Private Sub grdLevels_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdLevels.CellFormatting
        Select Case e.ColumnIndex
            Case 1

        End Select
    End Sub

    Private Sub grdLevels_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdLevels.CellValueNeeded
        If e.RowIndex = -1 OrElse e.ColumnIndex = -1 Then Exit Sub

        Dim TmpBTs As Trinity.cContractBookingtypes = grdLevels.Rows(e.RowIndex).Tag

        Select Case e.ColumnIndex
            Case Is = 0
                e.Value = "Level " & (e.RowIndex + 1).ToString

            Case Is = 1
                e.Value = TmpBTs(1).NegotiatedVolume

            Case Is = 2
                e.Value = TmpBTs(1).Active

            Case Is = 3
                If TmpBTs(1).ActiveFromDate.Year = 1 Then
                    e.Value = Nothing
                Else
                    e.Value = TmpBTs(1).ActiveFromDate
                End If

        End Select
    End Sub

    Private Sub grdLevels_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdLevels.CellValuePushed
        If e.RowIndex = -1 OrElse e.ColumnIndex = -1 Then Exit Sub
        Dim TmpBTs As Trinity.cContractBookingtypes = grdLevels.Rows(e.RowIndex).Tag
        Dim levelforchannel As Integer = TmpBTs.ParentChannel.ActiveContractLevel
        Dim rowlevel As Integer = e.RowIndex
        Select Case e.ColumnIndex
            Case Is = 1
                For Each TmpBT As Trinity.cContractBookingtype In TmpBTs
                    TmpBT.NegotiatedVolume = e.Value
                Next
            Case Is = 2

                'If the user wanted to set a checkbox to false, it has to be the highest level contract
                If e.Value = True And TmpBTs.ParentChannel.ActiveContractLevel < e.RowIndex Then
                    MsgBox("Activate the lower level contracts in order first.", MsgBoxStyle.Information, "Invalid operation")
                    Exit Sub
                Else
                    If (e.Value = False And TmpBTs.ParentChannel.ActiveContractLevel > e.RowIndex + 1) Or e.RowIndex = 0 Then
                        MsgBox("This level is locked. Either it is the first level, or you need to deactivate higher levels first.", MsgBoxStyle.Information, "Invalid operation")
                        Exit Sub
                    End If

                End If

                For Each TmpBT As Trinity.cContractBookingtype In TmpBTs
                    TmpBT.Active = e.Value
                    TmpBT.ActiveFromDate = Date.Now
                Next

                Dim j As Integer = grdLevels.Rows.Count - 1

                If e.Value = False Then
                    TmpBTs.ParentChannel.ActiveContractLevel -= 1
                Else
                    TmpBTs.ParentChannel.ActiveContractLevel += 1
                End If

                'While Not j = 0
                '    If TmpBTs(1).Active = True Then
                '        TmpBTs.ParentChannel.ActiveContractLevel = e.RowIndex + 1
                '        Exit While
                '    End If
                '    j -= 1
                'End While

            Case Is = 3
                If Not TmpBTs(1).Active Then
                    If MsgBox("This Level is not active, do you wish to activate it?", MsgBoxStyle.YesNo, "Invalid operation") = MsgBoxResult.Yes Then
                        TmpBTs.ParentChannel.ActiveContractLevel = e.RowIndex + 1
                        For Each TmpBT As Trinity.cContractBookingtype In TmpBTs
                            TmpBT.Active = True
                            TmpBT.ActiveFromDate = e.Value
                        Next
                        'grdLevels.Invalidate()
                        Exit Sub
                    Else
                        Exit Sub
                    End If
                    Exit Sub
                End If

                For Each TmpBT As Trinity.cContractBookingtype In TmpBTs
                    TmpBT.ActiveFromDate = e.Value
                Next
        End Select

        grdLevels.Invalidate()
    End Sub

    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        TabControl1.SelectedIndex = 0

        For Each TmpChan As Trinity.cChannel In Campaign.Channels

            If Campaign.Contract.Channels(TmpChan.ChannelName) Is Nothing Then
                With Campaign.Contract.Channels.Add(TmpChan.ChannelName, "")
                    .AddLevel()
                    .ActiveContractLevel = 1
                    .ListNumber = TmpChan.ListNumber
                    .Shortname = TmpChan.Shortname
                End With
            Else
                With Campaign.Contract.Channels(TmpChan.ChannelName)
                    .ListNumber = TmpChan.ListNumber
                    .Shortname = TmpChan.Shortname
                End With
            End If
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                If Campaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(1)(TmpBT.Name) Is Nothing Then
                    For i As Integer = 1 To Campaign.Contract.Channels(TmpChan.ChannelName).LevelCount
                        With Campaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(i).Add(TmpBT.Name)
                            .ShortName = TmpBT.Shortname
                            .IsRBS = TmpBT.IsRBS
                            .IsSpecific = TmpBT.IsSpecific
                            .PrintDayparts = TmpBT.PrintDayparts
                            .PrintBookingCode = TmpBT.PrintBookingCode
                            .ParentChannel = Campaign.Contract.Channels(TmpChan.ChannelName)
                            .Indexes = New Trinity.cIndexes(Campaign, Campaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(1)(TmpBT.Name))
                        End With
                    Next
                Else
                    For i As Integer = 1 To Campaign.Contract.Channels(TmpChan.ChannelName).LevelCount
                        With Campaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(i)(TmpBT.Name)
                            .ShortName = TmpBT.Shortname
                            .IsRBS = TmpBT.IsRBS
                            .IsSpecific = TmpBT.IsSpecific
                            .PrintDayparts = TmpBT.PrintDayparts
                            .PrintBookingCode = TmpBT.PrintBookingCode
                        End With
                    Next
                End If
                For i As Integer = 1 To Campaign.Contract.Channels(TmpChan.ChannelName).LevelCount
                    For Each pt As Trinity.cContractTarget In Campaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(i)(TmpBT.Name).ContractTargets
                        If TmpBT.Pricelist.Targets(pt.TargetName) IsNot Nothing Then
                            pt.CalcCPP = TmpBT.Pricelist.Targets(pt.TargetName).CalcCPP
                            pt.AdEdgeTargetName = TmpBT.Pricelist.Targets(pt.TargetName).Target.TargetName
                        End If
                    Next
                Next
            Next
        Next

        frmContract_Load(New Object, New System.EventArgs)
    End Sub

    Private Sub cmdEnhancements_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmEnhancements.grdEnhancements.Rows.Clear()
        If frmEnhancements.ShowDialog <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        Dim TmpIndex As Trinity.cIndex = TmpBT.Indexes.Add("")
        TmpIndex.FromDate = Date.FromOADate(Campaign.StartDate)
        TmpIndex.ToDate = Date.FromOADate(Campaign.EndDate)
        TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eTRP

        For Each TmpRow As DataGridViewRow In frmEnhancements.grdEnhancements.Rows
            With TmpIndex.Enhancements.Add()
                .Amount = TmpRow.Cells(1).Value
                .Name = TmpRow.Cells(0).Value
            End With
            'TmpIndex.Enhancements.SpecificFactor = frmEnhancements.txtSpecFactor.Text / 100
        Next

        cmbLevel_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub cmdEditEnhancement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditEnhancement.Click
        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        If grdIndexes.SelectedRows.Count = 0 Then Exit Sub

        Dim TmpIndex As Trinity.cIndex = grdIndexes.SelectedRows.Item(0).Tag
        frmEnhancements.grdEnhancements.Rows.Clear()
        For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
            With frmEnhancements.grdEnhancements.Rows(frmEnhancements.grdEnhancements.Rows.Add())
                .Cells(0).Value = TmpEnh.Name
                .Cells(1).Value = TmpEnh.Amount
                .Tag = TmpEnh.ID
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
                If TmpRow.Tag <> "" Then
                    .ID = TmpRow.Tag
                End If
            End With
        Next
        'TmpIndex.Enhancements.SpecificFactor = frmEnhancements.txtSpecFactor.Text / 100

        cmbLevel_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub TabPage2_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpChannelSetup.Enter

        cmdEditEnhancement.Visible = Campaign.Area = "DK"
        lblMaxDiscount.Visible = (Campaign.Area = "DK")
        txtMaxDiscount.Visible = (Campaign.Area = "DK")

    End Sub

    Private Sub cmdAddBT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddBT.Click

        Dim TmpChan As Trinity.cContractChannel = cmbChannels.SelectedItem
        If TmpChan Is Nothing Then Exit Sub
        Dim TmpBT As Trinity.cContractBookingtype = TmpChan.BookingTypes(1).Add("Bookingtype")
        TmpBT.IsContractBookingtype = True
        TmpBT.Indexes = New Trinity.cIndexes(Campaign, TmpBT)

        Dim j As Integer
        For j = 2 To TmpChan.LevelCount
            With TmpChan.BookingTypes(j).Add("Bookingtype")
                .IsContractBookingtype = True
                .Indexes = New Trinity.cIndexes(Campaign, TmpChan.BookingTypes(j)("Bookingtype"))
            End With
        Next

        j = grdBT.Rows.Add()
        grdBT.Rows(j).Tag = TmpBT

        Campaign.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes.Add("Bookingtype")
    End Sub

    Private Sub grdBT_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdBT.CellValueNeeded
        Dim tmpBT As Trinity.cContractBookingtype = grdBT.Rows(e.RowIndex).Tag
        If tmpBT Is Nothing Then Exit Sub

        Select Case e.ColumnIndex
            Case Is = 0
                e.Value = tmpBT.Name
            Case Is = 1
                e.Value = tmpBT.ShortName
            Case Is = 2
                e.Value = tmpBT.IsRBS
            Case Is = 3
                e.Value = tmpBT.IsSpecific
            Case Is = 4
                e.Value = tmpBT.PrintDayparts
            Case Is = 5
                e.Value = tmpBT.PrintBookingCode
        End Select
    End Sub

    Private Sub grdBT_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdBT.CellValuePushed
        Dim tmpBT As Trinity.cContractBookingtype = grdBT.Rows(e.RowIndex).Tag
        If tmpBT Is Nothing Then Exit Sub

        If Not tmpBT.IsContractBookingtype Then
            MsgBox("you can not change this bookingtype", MsgBoxStyle.Information, "")
            grdBT.Invalidate()
            Exit Sub
        End If

        Select Case e.ColumnIndex
            Case Is = 0
                'change the BT in the campaign collection aswell
                Campaign.Channels(tmpBT.ParentChannel.ChannelName).BookingTypes(tmpBT.Name).Name = e.Value
                tmpBT.Name = e.Value
            Case Is = 1
                tmpBT.ShortName = e.Value
            Case Is = 2
                tmpBT.IsRBS = e.Value
            Case Is = 3
                tmpBT.IsSpecific = e.Value
            Case Is = 4
                tmpBT.PrintDayparts = e.Value
            Case Is = 5
                tmpBT.PrintBookingCode = e.Value
        End Select

    End Sub

    Private Sub cmdDeleteBT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteBT.Click
        Dim TmpChan As Trinity.cContractChannel = cmbChannels.SelectedItem
        If TmpChan Is Nothing Then Exit Sub

        If grdBT.SelectedRows.Count = 0 Then Exit Sub

        Dim TmpBT As Trinity.cContractBookingtype = grdBT.SelectedRows(0).Tag

        Dim j As Integer
        For j = 1 To TmpChan.LevelCount
            TmpChan.BookingTypes(j).Remove(TmpBT.Name)
        Next

        grdBT.Rows.Remove(grdBT.SelectedRows(0))
        grdBT.Invalidate()
    End Sub

    Private Sub cmdCopyTargets_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopyTargets.Click
        Dim mnuCopy As New ContextMenu
        Dim BT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        For Each TmpChan As Trinity.cContractChannel In Campaign.Contract.Channels
            For Each TmpBT As Trinity.cContractBookingtype In TmpChan.BookingTypes(cmbLevel.SelectedIndex + 1)
                If TmpBT.ContractTargets.Count > 0 AndAlso Not (TmpBT.ToString = BT.ToString) Then
                    mnuCopy.MenuItems.Add(TmpBT.ToString, AddressOf CopyTarget).Tag = TmpBT
                End If
            Next
        Next
        mnuCopy.Show(cmdCopyTargets, New System.Drawing.Point(0, cmdCopyIndex.Height))
    End Sub



    Private Sub tpChannelSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tpChannelSetup.Click

    End Sub

    Private Sub txtMaxDiscount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMaxDiscount.TextChanged
        Dim TmpBT As Trinity.cContractBookingtype = DirectCast(cmbBookingtypes.SelectedItem, Trinity.cContractBookingtype).ParentChannel.BookingTypes(cmbLevel.SelectedIndex + 1)(DirectCast(cmbBookingtypes.SelectedItem, Trinity.cContractBookingtype).Name)
        TmpBT.MaxDiscount = txtMaxDiscount.Text / 100

    End Sub

    Private Sub txtCommission_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCommission.KeyUp

        If Not IsNumeric(txtCommission.Text) Then
            txtCommission.BackColor = Color.Red
        Else
            If CInt(txtCommission.Text) >= 0 And CInt(txtCommission.Text) <= 100 Then
                txtCommission.BackColor = Color.White
                Dim TmpChan As Trinity.cContractChannel = cmbChannels.SelectedItem
                TmpChan.Agencycommission = CSng(txtCommission.Text) / 100
            Else
                txtCommission.BackColor = Color.Red
            End If
        End If


    End Sub

    Private Sub txtCommission_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCommission.TextChanged
        'If Not IsNumeric(txtCommission.Text) Then
        '    txtCommission.BackColor = Color.Red
        'Else
        '    If CInt(txtCommission.Text) >= 0 And CInt(txtCommission.Text) <= 100 Then
        '        txtCommission.BackColor = Color.White
        '    End If
        'End If
    End Sub

    Private Sub cmdCopyFilmIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopyFilmIndex.Click
        Dim mnuCopy As New ContextMenu
        Dim BT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        For Each TmpChan As Trinity.cContractChannel In Campaign.Contract.Channels
            For Each TmpBT As Trinity.cContractBookingtype In TmpChan.BookingTypes(cmbLevel.SelectedIndex + 1)
                If TmpBT.ContractTargets.Count > 0 AndAlso Not (TmpBT.ToString = BT.ToString) Then
                    mnuCopy.MenuItems.Add(TmpBT.ToString, AddressOf CopyFilmIndex).Tag = TmpBT
                End If
            Next
        Next
        mnuCopy.Show(cmdCopyFilmIndex, New System.Drawing.Point(0, cmdCopyIndex.Height))
    End Sub

    Private Sub cmdAddCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddCopy.Click
        If cmbBookingtypes.SelectedIndex = -1 Then Exit Sub
        If grdIndexes.Rows.Count = 0 Then
            cmdAddIndex_Click(sender, e)
            Exit Sub
        End If
        Dim LastIndex As Trinity.cIndex = grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag
        Dim ID As String

        Dim TmpBT As Trinity.cContractBookingtype = cmbBookingtypes.Tag

        If cmbIndexTarget.SelectedIndex = 0 Then
            ID = TmpBT.Indexes.Add(LastIndex.Name).ID

            TmpBT.Indexes(ID).FromDate = LastIndex.FromDate
            TmpBT.Indexes(ID).ToDate = LastIndex.ToDate
            TmpBT.Indexes(ID).Index = LastIndex.Index

            For Each TmpEnh As Trinity.cEnhancement In LastIndex.Enhancements
                With TmpBT.Indexes(ID).Enhancements.Add()
                    .Amount = TmpEnh.Amount
                    .Name = TmpEnh.Name
                End With
            Next

            grdIndexes.Rows.Add()
            grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = TmpBT.Indexes(ID)
        Else
            Dim TmpTarget As Trinity.cContractTarget = cmbIndexTarget.SelectedItem
            ID = TmpTarget.Indexes.Add(LastIndex.Name).ID

            TmpTarget.Indexes(ID).FromDate = LastIndex.FromDate
            TmpTarget.Indexes(ID).ToDate = LastIndex.ToDate
            TmpTarget.Indexes(ID).Index = LastIndex.Index

            For Each TmpEnh As Trinity.cEnhancement In LastIndex.Enhancements
                With TmpTarget.Indexes(ID).Enhancements.Add()
                    .Amount = TmpEnh.Amount
                    .Name = TmpEnh.Name
                End With
            Next

            grdIndexes.Rows.Add()
            grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = TmpTarget.Indexes(ID)
        End If
    End Sub

    Private Sub tpGeneralSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tpGeneralSetup.Click

    End Sub

    Private Sub cmbIndexTarget_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbIndexTarget.SelectedIndexChanged
        Dim TmpBT As Trinity.cContractBookingtype = DirectCast(cmbBookingtypes.SelectedItem, Trinity.cContractBookingtype).ParentChannel.BookingTypes(cmbLevel.SelectedIndex + 1)(DirectCast(cmbBookingtypes.SelectedItem, Trinity.cContractBookingtype).Name)
        grdIndexes.Rows.Clear()
        If cmbIndexTarget.SelectedIndex = 0 Then
            Dim _fixedCPP As Boolean = False
            colOn.Items.Clear()
            colOn.Items.AddRange({"Net CPP", "Gross CPP", "TRP"})
            For Each TmpIndex As Trinity.cIndex In TmpBT.Indexes
                If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eFixedCPP Then
                    TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP
                    TmpIndex.Name &= "*"
                    _fixedCPP = True
                End If
                grdIndexes.Rows.Add()
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = TmpIndex
            Next
            If _fixedCPP Then
                Windows.Forms.MessageBox.Show("There were indexes set as 'Fixed CPP', which is illegal for indexes on 'All' targets." & vbCrLf & "Only indexes added to a specific target can have 'Fixed CPP'" & vbCrLf & vbCrLf & "These indexes have been changed to 'Net CPP' and are marked with '*'", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            colOn.Items.Clear()
            colOn.Items.AddRange({"Net CPP", "Gross CPP", "TRP", "Fixed CPP"})
            Dim TmpTarget As Trinity.cContractTarget = cmbIndexTarget.SelectedItem
            For Each TmpIndex As Trinity.cIndex In TmpTarget.Indexes
                grdIndexes.Rows.Add()
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = TmpIndex
            Next
        End If
    End Sub

    Private Sub chkRatecard_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRatecard.CheckedChanged
        DirectCast(cmbBookingtypes.SelectedItem, Trinity.cContractBookingtype).RatecardCPPIsGross = Not chkRatecard.Checked
    End Sub

    Private Sub cmdSaveToDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveToDB.Click
        If Campaign.ContractID > 0 AndAlso Campaign.Contract.Name <> DBReader.getContractName(Campaign.ContractID) Then
            Dim _res As Windows.Forms.DialogResult = Windows.Forms.MessageBox.Show("You have changed the name of the contract. Do you want to save it as a new contract?" & vbNewLine & vbNewLine & "Clicking 'No' will overwrite and change the name of the current contract.", "T R I N I T Y", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If _res = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            ElseIf _res = Windows.Forms.DialogResult.Yes Then
                Campaign.ContractID = 0
            End If
        End If

        Campaign.Contract.Save("", True, True)
        MessageBox.Show("Saved contract " & Campaign.Contract.Name, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click

    End Sub

    Private Sub grdBT_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdBT.CellContentClick

    End Sub

    Private Sub grdBT_SelectionChanged(sender As Object, e As System.EventArgs) Handles grdBT.SelectionChanged
        cmdDeleteBT.Enabled = False
        If grdBT.SelectedRows.Count = 1 Then
            If grdBT.SelectedRows(0).Tag IsNot Nothing Then
                Dim tmpBT As Trinity.cContractBookingtype = grdBT.SelectedRows(0).Tag
                If Campaign.Channels(tmpBT.ParentChannel.ChannelName) Is Nothing OrElse Campaign.Channels(tmpBT.ParentChannel.ChannelName).BookingTypes(tmpBT.Name) Is Nothing Then
                    cmdDeleteBT.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub grdLevels_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLevels.CellContentClick

    End Sub

    Private Sub grdCosts_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdCosts.CellContentClick

    End Sub

    Private Sub chkContractRestriction_CheckedChanged(sender As Object, e As EventArgs) Handles chkContractRestriction.CheckedChanged
        If chkContractRestriction.Checked Then
            Campaign.Contract.restriced = True
        Else
            Campaign.Contract.restriced = False
        End If
    End Sub

    Private Sub cmbClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbClient.SelectedIndexChanged
        Dim contractClientID = ""
        Dim contractClientName = ""
        contractClientID = DirectCast(cmbClient.SelectedItem, Client).id
        contractClientName = DBReader.getClient(contractClientID)

        Campaign.Contract.client = DirectCast(cmbClient.SelectedItem, Client).id

    End Sub
End Class

'Private Sub cmbOverviewChannel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
'    Dim Chan As String
'    Dim BT As String
'    Dim TmpTarget As Trinity.cPricelistTarget
'    Dim TmpBT As Trinity.cBookingType = cmbOverviewChannel.SelectedItem

'    Chan = TmpBT.ParentChannel.ChannelName
'    BT = TmpBT.Name

'    cmbOverviewTarget.Items.Clear()
'    For Each TmpTarget In Campaign.Contract.Channels(Chan).BookingTypes(BT).Pricelist.Targets
'        cmbOverviewTarget.Items.Add(TmpTarget.TargetName)
'    Next
'    If cmbOverviewTarget.Items.Count > 0 Then
'        cmbOverviewTarget.SelectedIndex = 0
'    End If
'End Sub

'Private Sub cmbOverviewTarget_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
'    Dim Chan As String
'    Dim BT As String
'    Dim i As Integer
'    Dim TmpBT As Trinity.cBookingType = cmbOverviewChannel.SelectedItem

'    Chan = TmpBT.ParentChannel.ChannelName
'    BT = TmpBT.name

'    Campaign.Contract.Channels(Chan).BookingTypes(BT).BuyingTarget = Campaign.Contract.Channels(Chan).BookingTypes(BT).Pricelist.Targets(cmbOverviewTarget.Text)
'    For i = 0 To Campaign.DaypartCount
'        Campaign.Contract.Channels(Chan).BookingTypes(BT).DaypartSplit(i) = Campaign.Contract.Channels(Chan).BookingTypes(BT).Pricelist.Targets(cmbOverviewTarget.Text).DefaultDaypart(i)
'    Next
'    Campaign.Contract.Channels(Chan).BookingTypes(BT).BuyingTarget.Discount = Campaign.Contract.Channels(Chan).BookingTypes(BT).Pricelist.Targets(cmbOverviewTarget.Text).Discount
'    dtOverview_ValueChanged(New Object, New EventArgs)
'    'cmbPeriod_Click()
'End Sub

'Private Sub dtOverview_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
'    Dim Chan As String
'    Dim BT As String
'    Dim Target As String
'    Dim r As Integer
'    Dim TmpIndex As Trinity.cIndex
'    Dim TmpRTF As String = ""
'    Dim TmpBT As Trinity.cBookingType = cmbOverviewChannel.SelectedItem

'    Chan = TmpBT.ParentChannel.ChannelName
'    BT = TmpBT.Name
'    Target = cmbOverviewTarget.Text
'    If Target = "" Then Exit Sub

'    rtxOverview.Clear()
'    TmpRTF = "{\rtf1\ansi\ansicpg1252\deff0\deflang1053{\fonttbl{\f0\fswiss\fcharset0 Segoe UI;}}"
'    TmpRTF = TmpRTF & "\pard\b\f0\fs15 Gross CPP 30""\b0\tab\tab\tab " & Format(Campaign.Contract.Channels(Chan).BookingTypes(BT).Pricelist.Targets(Target).CPP, "##,##0.0") & "\par\i"
'    'r = 1
'    For Each TmpIndex In Campaign.Contract.Channels(Chan).BookingTypes(BT).Pricelist.Targets(Target).Indexes
'        If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
'            If (TmpIndex.FromDate <= dtOverview.Value) And (TmpIndex.ToDate >= dtOverview.Value) Then
'                TmpRTF = TmpRTF & "\pard\tab\tab " & TmpIndex.Name & "\tab "
'                If TmpIndex.Name.Length < 10 Then
'                    TmpRTF = TmpRTF & "\tab "
'                End If
'                TmpRTF = TmpRTF & Format(TmpIndex.Index, "0.0") & "\par"
'            End If
'        End If
'    Next
'    For Each TmpIndex In Campaign.Contract.Channels(Chan).BookingTypes(BT).Indexes
'        If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
'            If (TmpIndex.FromDate <= dtOverview.Value) And (TmpIndex.ToDate >= dtOverview.Value) Then

'                TmpRTF = TmpRTF & "\pard\tab\tab " & TmpIndex.Name & "\tab "
'                If TmpIndex.Name.Length < 10 Then
'                    TmpRTF = TmpRTF & "\tab "
'                End If
'                TmpRTF = TmpRTF & Format(TmpIndex.Index, "0.0") & "\par"
'            End If
'        End If
'    Next

'    TmpRTF = TmpRTF & "\pard\tab\tab\tab\tab---------------\par"
'    TmpRTF = TmpRTF & "\pard\tab\tab\tab\tab\i0\b " & Format(Campaign.Contract.Channels(Chan).BookingTypes(BT).Pricelist.Targets(Target).CPP * (Campaign.Contract.Channels(Chan).BookingTypes(BT).Pricelist.Targets(Target).Indexes.GetIndexForDate(dtOverview.Value, Trinity.cIndex.IndexOnEnum.eGrossCPP) / 100) * (Campaign.Contract.Channels(Chan).BookingTypes(BT).Indexes.GetIndexForDate(dtOverview.Value, Trinity.cIndex.IndexOnEnum.eGrossCPP) / 100), "#,#0.0") & "\par\par "
'    If Campaign.Contract.Channels(Chan).BookingTypes(BT).Pricelist.Targets(Target).IsEntered = Trinity.cPricelistTarget.EnteredEnum.eDiscount Then
'        TmpRTF = TmpRTF & "\pard Discount "
'        TmpRTF = TmpRTF & "\tab\tab\tab\tab " & Format(Campaign.Contract.Channels(Chan).BookingTypes(BT).Pricelist.Targets(Target).Discount, "0.0%") & "\par "
'    ElseIf Campaign.Contract.Channels(Chan).BookingTypes(BT).Pricelist.Targets(Target).IsEntered = Trinity.cPricelistTarget.EnteredEnum.eCPT Then
'        TmpRTF = TmpRTF & "\pard CPT "
'        TmpRTF = TmpRTF & "\tab\tab\tab " & Format(Campaign.Contract.Channels(Chan).BookingTypes(BT).Pricelist.Targets(Target).NetCPT, "##,##0.0 kr") & "\par "
'    ElseIf Campaign.Contract.Channels(Chan).BookingTypes(BT).Pricelist.Targets(Target).IsEntered = Trinity.cPricelistTarget.EnteredEnum.eCPP Then
'        TmpRTF = TmpRTF & "\pard CPP "
'        TmpRTF = TmpRTF & "\tab\tab\tab " & Format(Campaign.Contract.Channels(Chan).BookingTypes(BT).Pricelist.Targets(Target).NetCPP, "##,##0.0 kr") & "\par "
'    End If
'    For Each TmpIndex In Campaign.Contract.Channels(Chan).BookingTypes(BT).Indexes
'        If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP Then
'            If (TmpIndex.FromDate <= dtOverview.Value) And (TmpIndex.ToDate >= dtOverview.Value) Then
'                TmpRTF = TmpRTF & "\pard\i\tab\tab " & TmpIndex.Name
'                TmpRTF = TmpRTF & "\tab " & Format(TmpIndex.Index, "0.0") & "\par "
'            End If
'        End If
'    Next
'    For Each TmpIndex In Campaign.Contract.Channels(Chan).BookingTypes(BT).Pricelist.Targets(Target).Indexes
'        If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eTRP Then
'            If (TmpIndex.FromDate <= dtOverview.Value) And (TmpIndex.ToDate >= dtOverview.Value) Then

'                TmpRTF = TmpRTF & "\pard\i\tab\tab " & TmpIndex.Name
'                TmpRTF = TmpRTF & "\tab (" & Format(100 / (TmpIndex.Index / 100), "0.0") & ")\par "
'                r = r + 1
'            End If
'        End If
'    Next
'    TmpRTF = TmpRTF & "\i0\b\pard\tab\tab\tab\tab---------------\par"
'    If Campaign.Contract.Channels(Chan).BookingTypes(BT).Weeks.Count > 0 Then
'        Try
'            TmpRTF = TmpRTF & "\pard\tab\tab\tab\tab\i0\b " & Format(Campaign.Contract.Channels(Chan).BookingTypes(BT).BuyingTarget.NetCPP * (Campaign.Contract.Channels(Chan).BookingTypes(BT).Indexes.GetIndexForDate(dtOverview.Value, Trinity.cIndex.IndexOnEnum.eNetCPP) / 100) * Campaign.Contract.Channels(Chan).BookingTypes(BT).GetWeek(dtOverview.Value).GrossIndex, "##,##0 kr")
'        Catch

'        End Try

'    End If
'    'grdOverview.Rows = r + 1
'    rtxOverview.Clear()
'    rtxOverview.Rtf = TmpRTF '"\bGross CPP 30""\b0" & Chr(8) & Format(Campaign.Contract.Channels(Chan).BookingTypes(BT).Pricelist.Targets(Target).CPP, "##,##0.0")

'End Sub,

'Private Sub picOverview_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
'    Dim Gfx As Graphics
'    Dim ScaleLeft As Integer
'    Dim ScaleRight As Integer
'    Dim ScaleHeight As Single
'    Dim ScaleWidth As Single
'    Dim ScaleTop As Single
'    Dim ScaleBottom As Single
'    Dim ScaleStepsX As Single
'    Dim ScaleStepsY As Single
'    Dim Chan As String
'    Dim BT As String
'    Dim i As Integer
'    Dim max As Single
'    Dim Mth As Integer
'    Dim d As Date
'    Dim DaysInMonth As Integer
'    Dim Idx As Single
'    Dim ExtraIndex As Single
'    Dim CPP As Single
'    Dim TmpBT As Trinity.cBookingType = cmbOverviewChannel.SelectedItem

'    Chan = TmpBT.ParentChannel.ChannelName
'    BT = TmpBT.Name

'    Gfx = picOverview.CreateGraphics

'    Gfx.Clear(Color.White)
'    ScaleLeft = (picOverview.Width / 20)
'    ScaleRight = (picOverview.Width - ScaleLeft)
'    ScaleTop = (picOverview.Height / 20)
'    ScaleBottom = (picOverview.Height - ScaleTop)

'    Dim Font = New Font("Small fonts", 6)
'    If cmbPeriod.SelectedIndex = 0 Then
'        ScaleWidth = Campaign.Contract.Channels(Chan).BookingTypes(BT).Weeks.Count + 4
'        For i = 1 To Campaign.Contract.Channels(Chan).BookingTypes(BT).Weeks.Count
'            If Campaign.Contract.Channels(Chan).BookingTypes(BT).Weeks(i).NetCPP30 > max Then
'                max = Campaign.Contract.Channels(Chan).BookingTypes(BT).Weeks(i).NetCPP30
'            End If
'        Next
'        max = max + 1000
'        ScaleHeight = max
'        ScaleStepsX = (picOverview.Width - ScaleLeft * 2) / ScaleWidth
'        ScaleStepsY = (picOverview.Height - ScaleTop * 2) / ScaleHeight
'        For i = 1000 To max - 1000 Step 1000
'            Gfx.DrawLine(Pens.DarkGray, ScaleLeft, ScaleBottom - i * ScaleStepsY, ScaleRight, ScaleBottom - i * ScaleStepsY)
'            Gfx.DrawString(Str(i), Font, Brushes.Black, ScaleLeft - 4 - Gfx.MeasureString(Str(i), Font).Width, ScaleBottom - i * ScaleStepsY - Gfx.MeasureString(Str(i), Font).Height / 2)
'        Next
'        For i = 2 To Campaign.Contract.Channels(Chan).BookingTypes(BT).Weeks.Count + 1
'            Dim Rect As New Rectangle(ScaleLeft + (ScaleStepsX * i), ScaleBottom - Campaign.Contract.Channels(Chan).BookingTypes(BT).Weeks(i - 1).NetCPP30 * ScaleStepsY, 10, Int(Campaign.Contract.Channels(Chan).BookingTypes(BT).Weeks(i - 1).NetCPP30 * ScaleStepsY))
'            'Dim Rect As New Rectangle(ScaleLeft + (ScaleStepsX * i), ScaleBottom, 10, -Campaign.Contract.Channels(Chan).BookingTypes(BT).Weeks(i - 1).NetCPP30 * ScaleStepsY)
'            Gfx.FillRectangle(Brushes.Red, Rect)
'            Gfx.DrawRectangle(Pens.Black, Rect)
'        Next
'    Else
'        ScaleWidth = Campaign.Contract.ToDate.Month - Campaign.Contract.FromDate.Month + 12 * (Campaign.Contract.ToDate.Year - Campaign.Contract.FromDate.Year) + 3
'        For i = 1 To Campaign.Contract.Channels(Chan).BookingTypes(BT).Weeks.Count
'            If Campaign.Contract.Channels(Chan).BookingTypes(BT).Weeks(i).NetCPP30 > max Then
'                max = Campaign.Contract.Channels(Chan).BookingTypes(BT).Weeks(i).NetCPP30
'            End If
'        Next
'        max = max + 1000
'        ScaleHeight = max
'        ScaleStepsX = (picOverview.Width - ScaleLeft * 2) / ScaleWidth
'        ScaleStepsY = (picOverview.Height - ScaleTop * 2) / ScaleHeight
'        For i = 1000 To max - 1000 Step 1000
'            Gfx.DrawLine(Pens.DarkGray, ScaleLeft, ScaleBottom - i * ScaleStepsY, ScaleRight, ScaleBottom - i * ScaleStepsY)
'            Gfx.DrawString(Str(i), Font, Brushes.Black, ScaleLeft - 4 - Gfx.MeasureString(Str(i), Font).Width, ScaleBottom - i * ScaleStepsY - Gfx.MeasureString(Str(i), Font).Height / 2)
'        Next
'        Mth = Campaign.Contract.FromDate.Month
'        i = 1
'        d = Campaign.Contract.FromDate
'        DaysInMonth = Date.DaysInMonth(d.Year, d.Month) - d.Day + 1
'        While d < Campaign.Contract.ToDate
'            CPP = 0
'            While d.Month = Mth And d <= Campaign.Contract.ToDate
'                Idx = ((Campaign.Channels(Chan).BookingTypes(BT).Indexes.GetIndexForDate(d, Trinity.cIndex.IndexOnEnum.eNetCPP) * (Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Indexes.GetIndexForDate(d, Trinity.cIndex.IndexOnEnum.eNetCPP))) / (DaysInMonth))
'                ExtraIndex = ((Campaign.Channels(Chan).BookingTypes(BT).Indexes.GetIndexForDate(d, Trinity.cIndex.IndexOnEnum.eTRP) * (Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Indexes.GetIndexForDate(d, Trinity.cIndex.IndexOnEnum.eTRP))) / (DaysInMonth))
'                If ExtraIndex <> 0 Then
'                    Idx = Format((Idx / 10000) * (10000 / ExtraIndex), "0.000000")
'                Else
'                    Idx = Format((Idx / 10000), "0.000000")
'                End If
'                If Idx = 0 Then
'                    Idx = 1
'                End If
'                If Not Campaign.Contract.Channels(Chan).BookingTypes(BT).GetWeek(d) Is Nothing Then
'                    CPP = CPP + ((Campaign.Contract.Channels(Chan).BookingTypes(BT).GetWeek(d).NetCPP30 * Idx) / DaysInMonth)
'                End If
'                d = d.AddDays(1)
'            End While

'            Dim Rect As New Rectangle(ScaleLeft + (ScaleStepsX * i), ScaleBottom - CPP * ScaleStepsY, ScaleStepsX, Int(CPP * ScaleStepsY))
'            'Dim Rect As New Rectangle(ScaleLeft + (ScaleStepsX * i), ScaleBottom, 10, -Campaign.Contract.Channels(Chan).BookingTypes(BT).Weeks(i - 1).NetCPP30 * ScaleStepsY)
'            Gfx.FillRectangle(Brushes.Red, Rect)
'            Gfx.DrawRectangle(Pens.Black, Rect)
'            Gfx.DrawString(Str(Mth), Font, Brushes.Black, ScaleLeft + (ScaleStepsX * i) - Gfx.MeasureString(Str(i), Font).Width / 2 + ScaleStepsX / 2, ScaleBottom + 2)

'            Mth = d.Month
'            DaysInMonth = Date.DaysInMonth(d.Year, d.Month)
'            i = i + 1
'        End While


'        'For i = 2 To Campaign.Contract.Channels(Chan).BookingTypes(BT).Weeks.Count + 1
'        '    Dim Rect As New Rectangle(ScaleLeft + (ScaleStepsX * i), ScaleBottom - Campaign.Contract.Channels(Chan).BookingTypes(BT).Weeks(i - 1).NetCPP30 * ScaleStepsY, 10, Int(Campaign.Contract.Channels(Chan).BookingTypes(BT).Weeks(i - 1).NetCPP30 * ScaleStepsY))
'        '    'Dim Rect As New Rectangle(ScaleLeft + (ScaleStepsX * i), ScaleBottom, 10, -Campaign.Contract.Channels(Chan).BookingTypes(BT).Weeks(i - 1).NetCPP30 * ScaleStepsY)
'        '    Gfx.FillRectangle(Brushes.Red, Rect)
'        '    Gfx.DrawRectangle(Pens.Black, Rect)
'        'Next
'    End If
'    Gfx.DrawLine(Pens.Black, ScaleLeft, ScaleTop, ScaleLeft, ScaleBottom)
'    Gfx.DrawLine(Pens.Black, ScaleLeft, ScaleBottom, ScaleRight, ScaleBottom)

'    Gfx.Dispose()
'End Sub