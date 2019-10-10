Imports System.Windows.Forms
Imports System.Drawing
Imports System.Linq
Imports System.IO

Public Class frmSetup
    'the setup form sets the basic parameters for a campaign such as the targeted group, films beeing used
    'what channels we use etc. All this information can be altered later but most of the information is
    'vital in order to take additional steps through the campaign planning.
    Private SkipIt As Boolean = False

    Dim txtBudgetAllowed As Boolean = True   'a marker for allowed chars in txtBudget
    Dim changeText As Boolean = False
    Dim strPricelistUpdate As String = ""
    Dim Flip As Boolean = False

    Dim planners As Xml.XmlNode
    Dim buyers As Xml.XmlNode

    Dim comboOptionModifyingManually As Boolean = False


    Private _activeCampaign As Trinity.cKampanj

    Private ReadOnly Property ActiveCampaign() As Trinity.cKampanj
        Get
            If _activeCampaign Is Nothing Then
                _activeCampaign = Campaign
            End If
            Return _activeCampaign
        End Get
    End Property

    Private Class CBItem

        Private _restricted As Boolean
        Private _text As String
        Public Property Text() As String
            Get
                Return _text
            End Get
            Set(ByVal value As String)
                _text = value
            End Set
        End Property
        Public Property restricted() As Boolean
            Get
                Return _restricted
            End Get
            Set(ByVal value As Boolean)
                _restricted = restricted
            End Set
        End Property

        Private _tag As Object
        Public Property Tag() As Object
            Get
                Return _tag
            End Get
            Set(ByVal value As Object)
                _tag = value
            End Set
        End Property
    End Class


    Public Sub PopulateClientCombo()
        'selcts all clients and put the names into the combo box

        cmbClient.Items.Clear()
        cmbClient.DisplayMember = "Text"

        Dim clients As DataTable = DBReader.getAllClients()
        For Each dr As DataRow In clients.Rows
            Dim TmpItem As New CBItem
            TmpItem.Text = dr.Item("name") 'rd!name
            TmpItem.Tag = dr.Item("id") 'rd!id
            If Not IsDBNull(dr.Item("restricted")) Then
                TmpItem.restricted = dr.Item("restricted") 'rd!Restricted 
            End If
            cmbClient.Items.Add(TmpItem)
            If TmpItem.Tag = Campaign.ClientID Then
                cmbClient.Text = TmpItem.Text
            End If
        Next
    End Sub


    Sub PopulateProductCombo()
        'selcts all products matching the client ID and put the names into the combo box and the ID into a hidden tag
        cmbProduct.Items.Clear()
        If cmbClient.SelectedItem Is Nothing Then
            Exit Sub
        End If
        Dim tempClient = DirectCast(cmbClient.SelectedItem, CBItem)
        If tempClient.restricted Then
            lblRestrictedClientBool.Visible = True
            lblRestrictedClientBool.Text = "Restricted"
        Else
            lblRestrictedClientBool.Visible = False
        End If
        ActiveCampaign.ClientID = DirectCast(cmbClient.SelectedItem, CBItem).Tag
        cmbProduct.Items.Clear()
        cmbProduct.DisplayMember = "Text"

        Dim products As DataTable = DBReader.getAllProducts(DirectCast(cmbClient.SelectedItem, CBItem).Tag)
        For Each dr As DataRow In products.Rows
            Dim TmpItem As New CBItem
            TmpItem.Text = dr.Item("name")
            TmpItem.Tag = dr.Item("id")
            cmbProduct.Items.Add(TmpItem)
            If TmpItem.Tag = Campaign.ProductID Then
                cmbProduct.Text = TmpItem.Text
            End If
        Next
    End Sub

    Private Sub cmdAddCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddCost.Click
        'adds a cost (either a one time cost or a variable cost based on %) to the campaign
        ActiveCampaign.Costs.Add("", 0, 0, 0, 0)
        'ActiveCampaign.Costs.Add("Spotkontroll", Trinity.cCost.CostTypeEnum.CostTypePerUnit, 50, Trinity.cCost.CostOnUnitEnum.CostOnSpots, 206)
        frmSetup_Activated(Me, New System.EventArgs)
        'the saved campaign is no longer up to date
        Saved = False
    End Sub

    Private Sub frmSetup_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'the procedure updates all labels and combo boxes
        Dim TmpCost As Trinity.cCost
        Dim TypeArray() As String = {"Fixed", "Percent", "Per Unit", "On Discount"}
        Dim i As Integer

        tpChannels.Enabled = False
        tpFilms.Enabled = False
        tpCombinations.Enabled = False
        'cmdChannelsNext.Enabled = False
        tpIndex.Enabled = False
        cmdFilmsNext.Enabled = False

        grdCosts.Rows.Clear()
        For Each TmpCost In ActiveCampaign.Costs
            grdCosts.Rows.Add()
            grdCosts.Rows(grdCosts.Rows.Count - 1).Tag = TmpCost
        Next


        If ActiveCampaign.Channels(1).BookingTypes(1).Weeks Is Nothing OrElse ActiveCampaign.Channels(1).BookingTypes(1).Weeks.Count = 0 Then
            'if we have no dates for the campaign we should only be able to access the first tab
            lblPeriod.Text = "No period selected"
            lblPeriod.ForeColor = Color.Red
            cmdGeneralNext.Enabled = False
            tpChannels.Enabled = False
            cmdGeneralNext.Enabled = False
        ElseIf Not ActiveCampaign.Channels(1).BookingTypes(1).Weeks(1).Films Is Nothing AndAlso ActiveCampaign.Channels(1).BookingTypes(1).Weeks(1).Films.Count > 0 Then
            'if we have films added we can eneble all tabs
            lblPeriod.Text = Format(Date.FromOADate(ActiveCampaign.StartDate), "Short Date") & " - " & Format(Date.FromOADate(ActiveCampaign.EndDate), "Short Date")
            lblPeriod.ForeColor = Color.Green
            tpChannels.Enabled = True
            cmdGeneralNext.Enabled = True
            tpFilms.Enabled = True
            tpCombinations.Enabled = True
            tpFilms.Enabled = True
            tpIndex.Enabled = True
        Else
            Dim hasChannels As Boolean = False
            For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels
                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        'if true we have atleast one channel added to the campaign
                        hasChannels = True
                        Exit For
                    End If
                Next
                If hasChannels Then Exit For
            Next

            If hasChannels Then
                'we enable film and combinations tab
                lblPeriod.Text = Format(Date.FromOADate(ActiveCampaign.StartDate), "Short Date") & " - " & Format(Date.FromOADate(ActiveCampaign.EndDate), "Short Date")
                lblPeriod.ForeColor = Color.Green

                tpChannels.Enabled = True
                cmdGeneralNext.Enabled = True
                tpFilms.Enabled = True
                tpCombinations.Enabled = True

            Else
                'We only enable channels tab
                lblPeriod.Text = Format(Date.FromOADate(ActiveCampaign.StartDate), "Short Date") & " - " & Format(Date.FromOADate(ActiveCampaign.EndDate), "Short Date")
                lblPeriod.ForeColor = Color.Green

                tpChannels.Enabled = True
                cmdGeneralNext.Enabled = True

            End If
        End If

        ''if the information on the first tab is not complete, you cant goto the next tab
        'If ActiveCampaign.Channels(1).BookingTypes(1).Weeks.Count = 0 Then
        '    lblPeriod.Text = "No period selected"
        '    lblPeriod.ForeColor = Color.Red
        '    cmdGeneralNext.Enabled = False
        '    tpChannels.Enabled = False
        '    cmdGeneralNext.Enabled = False
        'Else 'If the campaign info is complete we enables the next button and the channel tab
        '    lblPeriod.Text = Format(Date.FromOADate(ActiveCampaign.StartDate), "Short Date") & " - " & Format(Date.FromOADate(ActiveCampaign.EndDate), "Short Date")
        '    lblPeriod.ForeColor = Color.Green
        '    'cmdGeneralNext.Enabled = True 'dubbel kod?
        '    tpChannels.Enabled = True
        '    cmdGeneralNext.Enabled = True


        'End If

        'sets the target text according to the campaign
        txtMain.Text = ActiveCampaign.MainTarget.TargetName
        txtSec.Text = ActiveCampaign.SecondaryTarget.TargetName
        txtThird.Text = ActiveCampaign.ThirdTarget.TargetName

        'Clears all items in combo boxes
        cmbMainUni.Items.Clear()
        cmbSecondUni.Items.Clear()
        cmbThirdUni.Items.Clear()

        'repopulates the target combo boxes 
        For i = 0 To ActiveCampaign.Universes.Count - 1
            cmbMainUni.Items.Add(ActiveCampaign.Universes(i))
            cmbSecondUni.Items.Add(ActiveCampaign.Universes(i))
            cmbThirdUni.Items.Add(ActiveCampaign.Universes(i))
        Next
        If ActiveCampaign.MainTarget.Universe = "" AndAlso ActiveCampaign.Universes.Count > 0 Then
            cmbMainUni.SelectedItem = ActiveCampaign.Universes(0)
        Else
            cmbMainUni.Text = ActiveCampaign.Universes(ActiveCampaign.MainTarget.Universe)
        End If
        If ActiveCampaign.SecondaryTarget.Universe = "" AndAlso ActiveCampaign.Universes.Count > 0 Then
            cmbSecondUni.SelectedItem = ActiveCampaign.Universes(0)
        Else
            cmbSecondUni.Text = ActiveCampaign.Universes(ActiveCampaign.SecondaryTarget.Universe)
        End If
        If ActiveCampaign.ThirdTarget.Universe = "" AndAlso ActiveCampaign.Universes.Count > 0 Then
            cmbThirdUni.SelectedItem = ActiveCampaign.Universes(0)
        Else
            cmbThirdUni.Text = ActiveCampaign.Universes(ActiveCampaign.ThirdTarget.Universe)
        End If

        'displays the contract name if there is one present
        If ActiveCampaign.Contract Is Nothing Then
            lblContract.Text = "<None>"
        Else
            lblContract.Text = ActiveCampaign.Contract.Name
        End If

        'sets labels according to the campaign. Do not use ActiveCampaign, since Lab setup should also show main campaign values
        txtName.Text = Campaign.Name
        txtBudget.Text = Campaign.BudgetTotalCTC
        cmbPlanner.Text = Campaign.Planner
        cmbBuyer.Text = Campaign.Buyer

        'sets the picture on the area buttion depending on what country is selected
        cmdCountry.Image = frmMain.ilsBig.Images(ActiveCampaign.Area)
        lblArea.Text = TrinitySettings.AreaName(ActiveCampaign.Area)

        If ActiveCampaign.Contract IsNot Nothing AndAlso ActiveCampaign.Contract.Name IsNot Nothing Then
            Me.Text = "Setup [Contract: " & ActiveCampaign.Contract.Name & "]"
        End If
        'enables the autosave timer
        frmMain.tmrAutosave.Enabled = True
    End Sub

    'Private Sub grdCosts_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCosts.CellValueChanged
    '    If e.RowIndex > -1 And e.RowIndex < grdCosts.Rows.Count Then
    '        If e.ColumnIndex = 0 Then
    '            Dim TmpCell As DataGridViewTextBoxCell = grdCosts.Rows(e.RowIndex).Cells(e.ColumnIndex)
    '            ActiveCampaign.Costs(e.RowIndex + 1).CostName = TmpCell.Value
    '        ElseIf e.ColumnIndex = 1 Then
    '            Dim TmpCell As DataGridViewComboBoxCell = grdCosts.Rows(e.RowIndex).Cells("colCostOn")
    '            TmpCell.Items.Clear()
    '            If grdCosts.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Fixed" Then
    '                TmpCell.Items.Add("-")
    '                ActiveCampaign.Costs(e.RowIndex + 1).CostType = Trinity.cCost.CostTypeEnum.CostTypeFixed
    '            ElseIf grdCosts.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Percent" Then
    '                TmpCell.Items.Add("Media Net")
    '                TmpCell.Items.Add("Net")
    '                TmpCell.Items.Add("Net Net")
    '                ActiveCampaign.Costs(e.RowIndex + 1).CostType = Trinity.cCost.CostTypeEnum.CostTypePercent
    '            ElseIf grdCosts.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Per Unit" Then
    '                TmpCell.Items.Add("Spots")
    '                TmpCell.Items.Add("Buy TRP")
    '                TmpCell.Items.Add("Main TRP")
    '                ActiveCampaign.Costs(e.RowIndex + 1).CostType = Trinity.cCost.CostTypeEnum.CostTypePerUnit
    '            End If
    '            TmpCell.Value = TmpCell.Items(0).ToString
    '        ElseIf e.ColumnIndex = 2 Then
    '            Dim TmpCell As DataGridViewTextBoxCell = grdCosts.Rows(e.RowIndex).Cells(e.ColumnIndex)
    '            ActiveCampaign.Costs(e.RowIndex + 1).Amount = TmpCell.Value
    '        ElseIf e.ColumnIndex = 3 Then
    '            Dim TmpCell As DataGridViewComboBoxCell = grdCosts.Rows(e.RowIndex).Cells(e.ColumnIndex)
    '            If grdCosts.Rows(e.RowIndex).Cells("colType").Value = "Fixed" Then
    '                ActiveCampaign.Costs(e.RowIndex + 1).CountCostOn = 0
    '            ElseIf grdCosts.Rows(e.RowIndex).Cells("colType").Value = "Percent" Then
    '                If TmpCell.Value = "Media Net" Then
    '                    ActiveCampaign.Costs(e.RowIndex + 1).CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnMediaNet
    '                ElseIf TmpCell.Value = "Net" Then
    '                    ActiveCampaign.Costs(e.RowIndex + 1).CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNet
    '                ElseIf TmpCell.Value = "Net Net" Then
    '                    ActiveCampaign.Costs(e.RowIndex + 1).CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNetNet
    '                End If
    '            ElseIf grdCosts.Rows(e.RowIndex).Cells("colType").Value = "Per Unit" Then
    '                If TmpCell.Value = "Spots" Then
    '                    ActiveCampaign.Costs(e.RowIndex + 1).CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnSpots
    '                ElseIf TmpCell.Value = "Buy TRP" Then
    '                    ActiveCampaign.Costs(e.RowIndex + 1).CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnBuyingTRP
    '                ElseIf TmpCell.Value = "Main TRP" Then
    '                    ActiveCampaign.Costs(e.RowIndex + 1).CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnMainTRP
    '                End If
    '            End If
    '        ElseIf e.ColumnIndex = 4 Then
    '            Dim TmpCell As DataGridViewTextBoxCell = grdCosts.Rows(e.RowIndex).Cells(e.ColumnIndex)
    '            ActiveCampaign.Costs(e.RowIndex + 1).MarathonID = TmpCell.Value
    '        End If
    '    End If
    '    Saved = False
    'End Sub

    Private Sub cmdCountry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCountry.Click
        'this procedure/button selects what country settings is supposed to use
        'mnuArea.ImageList = 
        Dim i As Integer
        Dim TmpMnu As System.Windows.Forms.ToolStripMenuItem
        Saved = False

        'clears the availaable countrys
        mnuArea.Items.Clear()
        mnuArea.ShowCheckMargin = True
        mnuArea.ImageList = frmMain.ilsSmall
        'adds the available countrys in the list
        For i = 1 To TrinitySettings.AreaCount
            TmpMnu = mnuArea.Items.Add(TrinitySettings.AreaName(i), Nothing, New EventHandler(AddressOf mnuAreaItem_Click))
            TmpMnu.Checked = (ActiveCampaign.Area = TrinitySettings.Area(i))
            TmpMnu.ImageKey = TrinitySettings.Area(i)
            TmpMnu.Name = TrinitySettings.Area(i)
            TmpMnu.Tag = TrinitySettings.AreaLog(i)
            'AddItem(, , , , frmMain.ilsIcons.ItemIndex(Settings.Area(i)) - 1, True, , Settings.Area(i))
        Next
        mnuArea.Show(cmdCountry, New System.Drawing.Point(0, cmdCountry.Height))
    End Sub

    Private Sub mnuAreaItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim TmpIni As New Trinity.clsIni

        'if there are settings made, they will be reset when you change country. The code below makes a confirmation
        If ActiveCampaign.Channels(1).BookingTypes(1).Weeks.Count > 0 Then
            If Windows.Forms.MessageBox.Show("This will reset your period settings." & vbCrLf & vbCrLf & "Continue?", "T R I N I T Y", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        'makes a waiting icon of the cursor
        Me.Cursor = Cursors.WaitCursor
        'ActiveCampaign.UniStr = ""
        ActiveCampaign.TargetCollection.Clear()
        'ActiveCampaign.UniverseCollection.Clear()
        ActiveCampaign.Adedge.clearTargetSelection()

        ActiveCampaign.Area = DirectCast(sender, ToolStripMenuItem).Name
        TmpIni.Create(TrinitySettings.ActiveDataPath & ActiveCampaign.Area & "\Area.ini")
        ActiveCampaign.AreaLog = DirectCast(sender, ToolStripMenuItem).Tag

        'removes the channels in the campaign
        For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels
            ActiveCampaign.Channels.Remove(TmpChan.ChannelName)
        Next
        LongName.Clear()
        LongBT.Clear()

        'read the available channels from XML-files
        ActiveCampaign.CreateChannels()

        Trinity.Helper.WriteToLogFile("Set Basic Targets")
        Dim OldAllAdults As String = ActiveCampaign.AllAdults
        ActiveCampaign.AllAdults = TrinitySettings.AllAdults

        'Make sure all targets are properly calculated with advantedge
        If ActiveCampaign.MainTarget.TargetName = OldAllAdults Then
            txtMain.Text = ActiveCampaign.AllAdults
            txtMain_KeyUp(New Object, New KeyEventArgs(Keys.None))
        End If
        If ActiveCampaign.SecondaryTarget.TargetName = OldAllAdults Then
            txtSec.Text = ActiveCampaign.AllAdults
            txtSec_KeyUp(New Object, New KeyEventArgs(Keys.None))
        End If
        If ActiveCampaign.ThirdTarget.TargetName = OldAllAdults Then
            txtThird.Text = ActiveCampaign.AllAdults
            txtThird_KeyUp(New Object, New KeyEventArgs(Keys.None))
        End If
        ActiveCampaign.MainTarget.TargetName = ActiveCampaign.MainTarget.TargetName
        ActiveCampaign.SecondaryTarget.TargetName = ActiveCampaign.SecondaryTarget.TargetName
        ActiveCampaign.ThirdTarget.TargetName = ActiveCampaign.ThirdTarget.TargetName

        For i As Integer = 1 To ActiveCampaign.Channels.Count
            LongName.Add(ActiveCampaign.Channels(i).Shortname, ActiveCampaign.Channels(i).ChannelName)
        Next
        'For i As Integer = 1 To ActiveCampaign.Channels(1).BookingTypes.Count
        '    LongBT.Add(ActiveCampaign.Channels(1).BookingTypes(i).Shortname, ActiveCampaign.Channels(1).BookingTypes(i).Name)
        'Next

        For Each chan As Trinity.cChannel In ActiveCampaign.Channels
            For Each BT As Trinity.cBookingType In chan.BookingTypes
                If Not LongBT.ContainsValue(BT.Name) Then
                    LongBT.Add(BT.Shortname, BT.Name)
                End If
            Next
        Next

        Trinity.Helper.WriteToLogFile("Read Pricelists")
        For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                TmpBT.ReadPricelist()
            Next
        Next
        UpdateChannelGrid()
        cmdCountry.Image = frmMain.ilsBig.Images(ActiveCampaign.Area)
        lblArea.Text = DirectCast(sender, ToolStripMenuItem).Text

        'the campaign is not saved
        Saved = False
        'the cursor is reset to normal
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmdContract_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdContract.Click
        'this button enables the user to add or edit contracts
        mnuEditContract.Enabled = Not (ActiveCampaign.Contract Is Nothing)
        mnuContract.Show(cmdContract, New System.Drawing.Point(0, cmdContract.Height))

    End Sub

    Private Sub txtMain_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMain.KeyUp
        'if the main target text field changes we need to update labels and buttons
        If txtMain.Text = ActiveCampaign.MainTarget.TargetName Then Exit Sub
        Try
            ActiveCampaign.MainTarget.TargetType = Trinity.cTarget.TargetTypeEnum.trgMnemonicTarget
            ActiveCampaign.MainTarget.TargetName = UCase(txtMain.Text)
        Catch ex As Exception

        End Try

        'If ActiveCampaign.MainTarget.UniSize = 0 Then
        '    Try
        '        ActiveCampaign.MainTarget.TargetType = Trinity.cTarget.TargetTypeEnum.trgUserTarget
        '        ActiveCampaign.MainTarget.TargetName = UCase(txtMain.Text)
        '    Catch ex As Exception

        '    End Try
        'End If
        lblMainSize.Text = Format(ActiveCampaign.MainTarget.UniSize * 1000, "##,##0")
        'makes the label red if the value is invalid
        If ActiveCampaign.MainTarget.UniSize = 0 Then
            lblMainTarget.ForeColor = Color.Red
        Else
            lblMainTarget.ForeColor = Color.Black
        End If
        Saved = False
        If ActiveCampaign.MainTarget.TargetType > 0 Then
            txtMain.ForeColor = Color.Gray
        Else
            txtMain.ForeColor = Color.Black
        End If
        Saved = False
    End Sub

    Private Sub txtSec_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSec.KeyUp
        'if the second target text field changes we need to update labels and buttons
        If txtSec.Text = ActiveCampaign.SecondaryTarget.TargetName Then Exit Sub
        ActiveCampaign.SecondaryTarget.TargetType = Trinity.cTarget.TargetTypeEnum.trgMnemonicTarget
        ActiveCampaign.SecondaryTarget.TargetName = UCase(txtSec.Text)
        lblSecondSize.Text = Format(ActiveCampaign.SecondaryTarget.UniSize * 1000, "##,##0")
        'makes the label red if the value is invalid
        If ActiveCampaign.SecondaryTarget.UniSize = 0 Then
            lblSecondTarget.ForeColor = Color.Red
        Else
            lblSecondTarget.ForeColor = Color.Black
        End If
        Saved = False
        If ActiveCampaign.SecondaryTarget.TargetType > 0 Then
            txtSec.ForeColor = Color.Gray
        Else
            txtSec.ForeColor = Color.Black
        End If
        Saved = False
    End Sub

    Private Sub txtThird_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtThird.KeyUp
        'if the third target text field changes we need to update labels and buttons
        If txtThird.Text = ActiveCampaign.ThirdTarget.TargetName Then Exit Sub
        ActiveCampaign.ThirdTarget.TargetType = Trinity.cTarget.TargetTypeEnum.trgMnemonicTarget
        ActiveCampaign.ThirdTarget.TargetName = UCase(txtThird.Text)
        lblThirdSize.Text = Format(ActiveCampaign.ThirdTarget.UniSize * 1000, "##,##0")
        'makes the label red if the value is invalid
        If ActiveCampaign.MainTarget.UniSize = 0 Then
            lblThirdTarget.ForeColor = Color.Red
        Else
            lblThirdTarget.ForeColor = Color.Black
        End If
        Saved = False
        If ActiveCampaign.ThirdTarget.TargetType > 0 Then
            txtThird.ForeColor = Color.Gray
        Else
            txtThird.ForeColor = Color.Black
        End If
        Saved = False
    End Sub

    'Textrules for the Campaign Name form (txtName)'
    Private Sub txtName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        'added the charcode for the signs "(,),/"'
        e.Handled = Not (Char.IsLetterOrDigit(e.KeyChar) Or e.KeyChar = ChrW(32) Or Char.IsControl(e.KeyChar) Or e.KeyChar = ChrW(45) Or e.KeyChar = ChrW(40) Or e.KeyChar = ChrW(41) Or e.KeyChar = ChrW(47))

    End Sub

    Private Sub txtName_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        'if the campaign name changes we need to update the window namn and campaign name

        ActiveCampaign.Name = txtName.Text
        'the campaign is not saved
        Saved = False
        If ActiveCampaign.Name <> "" Then
            frmMain.Text = "T R I N I T Y   4.0  -  " & ActiveCampaign.Name
        Else
            frmMain.Text = "T R I N I T Y   4.0"
        End If
    End Sub

    Private Function formatStringNumeric(ByVal s As String) As String
        Try
            formatStringNumeric = ""
            For Each c As Char In s
                If Char.IsNumber(c) Then
                    formatStringNumeric = formatStringNumeric + c
                End If
            Next
            'obs denna function fixar endast 10 siffror
            formatStringNumeric = Format(CInt(formatStringNumeric), "#,#0")
            Return formatStringNumeric
        Catch
            Return 0
        End Try
    End Function

    Private Sub txtFilmLength_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFilmLength.KeyDown

    End Sub

    Private Sub txtBudget_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBudget.KeyPress, txtFilmLength.KeyPress
        If (Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57) AndAlso Asc(e.KeyChar) <> 44 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 13 Then
            e.Handled = True
            changeText = False
        Else
            changeText = True
        End If
    End Sub

    Private Sub txtBudget_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBudget.KeyUp
        If txtBudget.Text = "" Then Exit Sub
        If changeText = True Then
            If e.KeyCode = Keys.Return Then Exit Sub 'added because the error messege cant be closed with return otherwize

            Dim s As String = txtBudget.Text
            Dim oldPos As Integer = txtBudget.SelectionStart
            Dim ln As Integer = Len(txtBudget.Text)
            If (s \ 3 > 0) And (s Mod 3 > 0) Then
                If (oldPos = 4) Or (oldPos = 8) Or (oldPos = 12) Then 'this limits the textbox to 15 numbers to maintain correct formatting
                    oldPos += 1
                End If
                txtBudget.Text = formatStringNumeric(txtBudget.Text)
                txtBudget.Select(oldPos, 0)
            Else
                txtBudget.Text = formatStringNumeric(txtBudget.Text)
                If ln < Len(txtBudget.Text) Then
                    txtBudget.Select(oldPos + 1, 0)
                Else
                    txtBudget.Select(oldPos, 0)
                End If
            End If

            If txtBudget.Text <> "" Then
                ActiveCampaign.BudgetTotalCTC = CInt(txtBudget.Text)
            Else
                ActiveCampaign.BudgetTotalCTC = 0
            End If
            Saved = False
        End If
        changeText = False
    End Sub

    Private Sub cmdPeriod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPeriod.Click
        'opens a scheduler to let the user set the campaign period
        Dim _period = New frmPeriod(ActiveCampaign)
        _period.ShowDialog()
        frmSetup_Activated(New Object, New System.EventArgs)
    End Sub


    Private Sub cmdAddClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddClient.Click
        'deletes any previous add clients window, the opens a add client window for the user
        frmAddClient.Dispose()
        frmAddClient.Tag = ""
        frmAddClient.ShowDialog()
        'if there was a client added then the combo box need to be updated aswell
        If frmAddClient.txtName.Text <> "" Then
            PopulateClientCombo()
            cmbClient.Text = frmAddClient.txtName.Text
        End If
    End Sub

    Private Sub mnuNewContract_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuNewContract.Click
        'opens a "new contract" dialog and adds a contract to the campaign if a contract is set up.
        'Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType

        ActiveCampaign.Contract = Nothing
        If TrinitySettings.DefaultContractPath = "" Then
            ActiveCampaign.Contract = New Trinity.cContract(Campaign, True)
        Else
            Try
                ActiveCampaign.LoadDefaultContract()
            Catch
                ActiveCampaign.Contract = New Trinity.cContract(Campaign, True)
            End Try
        End If

        ActiveCampaign.Contract.Name = "New contract"
        ActiveCampaign.ContractID = 0

        'For Each TmpChan In ActiveCampaign.Contract.Channels
        '    For Each TmpBT In TmpChan.BookingTypes
        '        If Not ActiveCampaign.Channels(TmpChan.ChannelName) Is Nothing AndAlso Not ActiveCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name) Is Nothing Then
        '            For Each TmpAV As Trinity.cAddedValue In ActiveCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).AddedValues
        '                With TmpBT.AddedValues.Add(TmpAV.Name)
        '                    .IndexGross = TmpAV.IndexGross
        '                    .IndexNet = TmpAV.IndexNet
        '                End With
        '            Next
        '        End If
        '        If Not ActiveCampaign.Channels(TmpChan.ChannelName) Is Nothing AndAlso Not ActiveCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name) Is Nothing Then
        '            TmpBT.Indexes = ActiveCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Indexes
        '        End If
        '    Next
        'Next

        'update all information after the new contract is set
        'ActiveCampaign.Contract.Costs = ActiveCampaign.Costs
        'ActiveCampaign.Contract.Combinations.Clear()
        'For Each TmpCombo As Trinity.cCombination In ActiveCampaign.Combinations
        '    With ActiveCampaign.Contract.Combinations.Add
        '        .CombinationOn = TmpCombo.CombinationOn
        '        .ID = TmpCombo.ID
        '        .Name = TmpCombo.Name
        '        .ShowAsOne = TmpCombo.ShowAsOne
        '        For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
        '            TmpCC.Bookingtype = ActiveCampaign.Channels(TmpCC.Bookingtype.ParentChannel.ChannelName).BookingTypes(TmpCC.Bookingtype.Name)
        '            .Relations.Add(ActiveCampaign.Channels(TmpCC.Bookingtype.ParentChannel.ChannelName).BookingTypes(TmpCC.Bookingtype.Name), TmpCC.Relation)
        '            If .ShowAsOne Then
        '                TmpCC.Bookingtype.ShowMe = False
        '            Else
        '                TmpCC.Bookingtype.ShowMe = True
        '            End If
        '        Next
        '    End With
        'Next

        'opens the contract window
        frmContract.ShowDialog()

        Dim Channels = From Chans In ActiveCampaign.Channels Select Chans

        'After frmContract is closed, transfer everything in the contract back into the campaign
        _progress = New frmProgress()
        _progress.Show()
        AddHandler ActiveCampaign.Contract.ApplyingContract, AddressOf ApplyingContract
        ActiveCampaign.Contract.ApplyToCampaign()
        _progress.Hide()
        _progress.Dispose()

        lblContract.Text = ActiveCampaign.Contract.Name

        frmSetup_Activated(New Object, New EventArgs)
        lblContract.Text = ActiveCampaign.Contract.Name
    End Sub

    Private Sub OpenContractFromDB()

        If frmSelectContract.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ActiveCampaign.Contract = New Trinity.cContract(Campaign)
            ActiveCampaign.Contract.Load("", True, DBReader.getContract(frmSelectContract.grdContracts.SelectedRows(0).Tag!id).OuterXml.ToString)
            ActiveCampaign.ContractID = frmSelectContract.grdContracts.SelectedRows(0).Tag!id

            ActiveCampaign.Contract.ApplyToCampaign()

            lblContract.Text = ActiveCampaign.Contract.Name
        End If
        frmSetup_Activated(New Object, New EventArgs)

    End Sub

    '    Public Sub ApplyContract()

    '        Dim TmpBT As Trinity.cBookingType

    '        For Each TmpC As Trinity.cContractChannel In ActiveCampaign.Contract.Channels

    '            For Each TmpCBT As Trinity.cContractBookingtype In TmpC.BookingTypes(TmpC.ActiveContractLevel)

    '                If ActiveCampaign.Channels(TmpC.ChannelName) Is Nothing Then 'The channel is not in the campaign but exists in the contract
    '                    ActiveCampaign.Channels.Add(TmpC.ChannelName, "", TrinitySettings.DefaultArea)
    '                    ActiveCampaign.Channels(TmpC.ChannelName).readDefaultBookingTypes()

    '                End If

    '                TmpBT = ActiveCampaign.Channels(TmpC.ChannelName).BookingTypes(TmpCBT.Name)

    '                If TmpBT Is Nothing Then

    '                    'Hannes added
    '                    Dim CampaignFilms As New List(Of Trinity.cFilm)

    '                    If ActiveCampaign.Channels(1).BookingTypes(1).Weeks.Count > 0 AndAlso ActiveCampaign.Channels(1).BookingTypes(1).Weeks(1).Films.Count > 0 Then
    '                        For Each f As Trinity.cFilm In ActiveCampaign.Channels(1).BookingTypes(1).Weeks(1).Films
    '                            CampaignFilms.Add(f)
    '                        Next
    '                    End If

    '                    'if it is a BT added in the contract we add it
    '                    If TmpCBT.IsContractBookingtype Then
    '                        TmpBT = ActiveCampaign.Channels(TmpC.ChannelName).BookingTypes.Add(TmpCBT.Name)
    '                        TmpBT.Shortname = TmpCBT.ShortName
    '                        TmpBT.PrintDayparts = TmpCBT.PrintDayparts
    '                        TmpBT.PrintBookingCode = TmpCBT.PrintBookingCode
    '                        TmpBT.IsRBS = TmpCBT.IsRBS
    '                        TmpBT.IsSpecific = TmpCBT.IsSpecific

    '                        If ActiveCampaign.Channels(1).BookingTypes(1).Weeks.Count = 0 Then
    '                            For Each w As Trinity.cWeek In ActiveCampaign.Channels(2).BookingTypes(1).Weeks
    '                                With TmpBT.Weeks.Add(w.Name)
    '                                    .Bookingtype = TmpBT
    '                                    .EndDate = w.EndDate
    '                                    .StartDate = w.StartDate
    '                                End With
    '                            Next
    '                        Else
    '                            For Each w As Trinity.cWeek In ActiveCampaign.Channels(1).BookingTypes(1).Weeks
    '                                With TmpBT.Weeks.Add(w.Name)
    '                                    .Bookingtype = TmpBT
    '                                    .EndDate = w.EndDate
    '                                    .StartDate = w.StartDate
    '                                    'Hannes added
    '                                    For Each film As Trinity.cFilm In CampaignFilms
    '                                        .Films.Add(film.FilmString)
    '                                        .Films(film.FilmString).Filmcode = film.Filmcode
    '                                        .Films(film.FilmString).Name = film.Name
    '                                    Next
    '                                End With
    '                            Next
    '                        End If

    '                    Else
    '                        'if the bookingtype is missing we ask the user about it
    '                        'OPTIONS AVAILABLE
    '                        'Skip reading in the bookingtype
    '                        'Rename the bookingtype (add all the options to another BT since it has been renamed)

    '                        'create a list of available bookingtypes
    '                        Dim list As New List(Of String)
    '                        Dim found As Boolean = False
    '                        For Each TmpBT In ActiveCampaign.Channels(TmpC.ChannelName).BookingTypes
    '                            For Each TmpCBT2 As Trinity.cContractBookingtype In TmpC.BookingTypes(1)
    '                                If TmpBT.Name = TmpCBT2.Name Then
    '                                    found = True
    '                                End If
    '                            Next
    '                            If Not found Then
    '                                list.Add(TmpBT.Name)
    '                            End If
    '                        Next
    '                        If list.Count = 0 Then
    '                            GoTo End_BookingType 'we skip this BT
    '                        Else
    '                            Dim intResult As Integer
    '                            With New OptionsPicker(list)
    '                                .Text = "Bookingtype error"
    '                                .lbl.Text = TmpCBT.ToString & " does not exist in your ActiveCampaign."
    '                                .ShowDialog()
    '                                intResult = .DialogResult
    '                            End With

    '                            If intResult = Windows.Forms.DialogResult.No Then
    '                                GoTo End_BookingType 'we skip this BT
    '                            Else
    '                                TmpBT = ActiveCampaign.Channels(TmpC.ChannelName).BookingTypes(list(intResult - 1))
    '                            End If
    '                        End If
    '                    End If
    '                End If

    '                TmpBT.ParentChannel.AgencyCommission = TmpC.Agencycommission
    '                TmpBT.RatecardCPPIsGross = TmpCBT.RatecardCPPIsGross
    '                TmpBT.MaxDiscount = TmpCBT.MaxDiscount
    '                For i As Integer = 1 To 500
    '                    TmpBT.FilmIndex(i) = TmpCBT.FilmIndex(i)
    '                Next

    '                For Each TmpIndex As Trinity.cIndex In TmpCBT.Indexes
    '                    If Not TmpBT.Indexes(TmpIndex.ID) Is Nothing Then
    '                        TmpBT.Indexes.Remove(TmpIndex.ID)
    '                    End If
    '                    With TmpBT.Indexes.Add(TmpIndex.Name, TmpIndex.ID)
    '                        .FromDate = TmpIndex.FromDate
    '                        .ToDate = TmpIndex.ToDate
    '                        .IndexOn = TmpIndex.IndexOn
    '                        .Index = TmpIndex.Index
    '                        .UseThis = TrinitySettings.DefaultUseThis
    '                        For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
    '                            With .Enhancements.Add
    '                                .Amount = TmpEnh.Amount
    '                                .ID = TmpEnh.ID
    '                                .Name = TmpEnh.Name
    '                                .UseThis = TrinitySettings.DefaultUseThis
    '                            End With
    '                        Next
    '                    End With
    '                Next

    '                For Each TmpAV As Trinity.cAddedValue In TmpCBT.AddedValues
    '                    If Not TmpBT.AddedValues(TmpAV.ID) Is Nothing Then
    '                        TmpBT.AddedValues.Remove(TmpAV.ID)
    '                    End If
    '                    With TmpBT.AddedValues.Add(TmpAV.Name, TmpAV.ID)
    '                        .IndexGross = TmpAV.IndexGross
    '                        .IndexNet = TmpAV.IndexNet
    '                    End With
    '                Next

    '                Dim TmpTarget As Trinity.cPricelistTarget
    '                For Each TmpCTarget As Trinity.cContractTarget In TmpCBT.ContractTargets
    '                    'orelse is for oldcontract structure
    '                    If TmpCTarget.IsContractTarget OrElse TmpBT.Pricelist.Targets(TmpCTarget.TargetName) Is Nothing Then

    '                        TmpTarget = TmpBT.Pricelist.Targets.Add(TmpCTarget.TargetName, TmpBT)

    '                        TmpTarget.CalcCPP = TmpCTarget.CalcCPP
    '                        TmpTarget.Target.TargetType = TmpCTarget.TargetType
    '                        TmpTarget.Target.TargetName = TmpCTarget.AdEdgeTargetName
    '                        TmpTarget.Bookingtype = TmpBT
    '                        For Each TmpPeriod As Trinity.cPricelistPeriod In TmpCTarget.PricelistPeriods
    '                            Dim period As Trinity.cPricelistPeriod = TmpTarget.PricelistPeriods.Add(TmpPeriod.Name)
    '                            period.FromDate = TmpPeriod.FromDate
    '                            period.ToDate = TmpPeriod.ToDate
    '                            period.PriceIsCPP = TmpPeriod.PriceIsCPP
    '                            period.TargetNat = TmpPeriod.TargetNat
    '                            period.TargetUni = TmpPeriod.TargetUni
    '                            For j As Integer = 0 To TmpBT.Dayparts.Count - 1
    '                                period.Price(j) = TmpPeriod.Price(j)
    '                            Next
    '                        Next
    '                    Else
    '                        TmpTarget = TmpBT.Pricelist.Targets(TmpCTarget.TargetName)
    '                    End If
    '                    Dim TmpCPP As Single = TmpBT.BuyingTarget.NetCPP
    '                    Dim Added As Boolean = False
    '                    For Each TmpIndex As Trinity.cIndex In TmpCTarget.Indexes
    '                        Dim UseThis As Boolean = False
    '                        Dim SaveEnh As New Dictionary(Of String, Trinity.cEnhancement)
    '                        If Not TmpTarget.Indexes(TmpIndex.ID) Is Nothing Then
    '                            UseThis = TmpTarget.Indexes(TmpIndex.ID).UseThis
    '                            For Each TmpEnh As Trinity.cEnhancement In TmpTarget.Indexes(TmpIndex.ID).Enhancements
    '                                SaveEnh.Add(TmpEnh.ID, TmpEnh)
    '                            Next
    '                            TmpTarget.Indexes.Remove(TmpIndex.ID)
    '                        End If
    '                        If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eFixedCPP Then UseThis = True
    '                        'Add all the indexes from the contract into the campaign again.. not desirable 
    '                        With TmpTarget.Indexes.Add(TmpIndex.Name, TmpIndex.ID)
    '                            .FromDate = TmpIndex.FromDate
    '                            .ToDate = TmpIndex.ToDate
    '                            .IndexOn = TmpIndex.IndexOn
    '                            .Index = TmpIndex.Index
    '                            .FixedCPP = TmpIndex.FixedCPP
    '                            .SystemGenerated = False

    '                            If TmpBT.BuyingTarget.TargetName = TmpTarget.TargetName Then
    '                                If .IndexOn = Trinity.cIndex.IndexOnEnum.eFixedCPP Then
    '                                    If (.FromDate.ToOADate <= Campaign.EndDate AndAlso .ToDate.ToOADate >= Campaign.StartDate) Then
    '                                        If Not Added Then
    '                                            TmpCPP = .FixedCPP
    '                                            TmpCTarget.EnteredValue = TmpCPP
    '                                            Added = True
    '                                        End If
    '                                        .IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP
    '                                        .Index = (.FixedCPP / TmpCPP) * 100
    '                                        .UseThis = True
    '                                    End If
    '                                End If
    '                            End If

    '                            For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
    '                                If Not TmpBT.Indexes(TmpIndex.ID) Is Nothing AndAlso Not TmpBT.Indexes(TmpIndex.ID).Enhancements(TmpEnh.ID) Is Nothing Then
    '                                    TmpBT.Indexes(TmpIndex.ID).Enhancements.Remove(TmpEnh.ID)
    '                                End If
    '                                With .Enhancements.Add
    '                                    .Amount = TmpEnh.Amount
    '                                    .ID = TmpEnh.ID
    '                                    .Name = TmpEnh.Name
    '                                    .UseThis = TrinitySettings.DefaultUseThis
    '                                End With
    '                            Next
    '                            For Each TmpEnh As Trinity.cEnhancement In SaveEnh.Values
    '                                If TmpBT.Indexes(TmpIndex.ID).Enhancements(TmpEnh.ID) IsNot Nothing Then
    '                                    TmpBT.Indexes(TmpIndex.ID).Enhancements(TmpEnh.ID).UseThis = TmpEnh.UseThis
    '                                Else
    '                                    With TmpBT.Indexes(TmpIndex.ID).Enhancements.Add
    '                                        .ID = TmpEnh.ID
    '                                        .Name = TmpEnh.Name
    '                                        .Amount = TmpEnh.Amount
    '                                        .UseThis = TmpEnh.UseThis
    '                                    End With
    '                                End If
    '                            Next
    '                            .UseThis = UseThis
    '                        End With
    '                    Next
    '                    'copy the daypart split
    '                    For i As Integer = 0 To TmpBT.Dayparts.Count - 1
    '                        TmpTarget.DefaultDayPart(i) = TmpCTarget.DefaultDaypart(i)
    '                    Next

    '                    'copy the CPP/CPT/Discount entered
    '                    TmpTarget.IsEntered = TmpCTarget.IsEntered
    '                    Select Case TmpCTarget.IsEntered
    '                        Case Is = Trinity.cContractTarget.EnteredEnum.eCPP
    '                            TmpTarget.NetCPP = TmpCTarget.EnteredValue
    '                        Case Is = Trinity.cContractTarget.EnteredEnum.eCPT
    '                            TmpTarget.NetCPT = TmpCTarget.EnteredValue
    '                        Case Is = Trinity.cContractTarget.EnteredEnum.eDiscount
    '                            TmpTarget.Discount = TmpCTarget.EnteredValue
    '                    End Select
    '                Next

    '                'set the buyingtarget again so it will be updated
    '                If Not TmpBT.BuyingTarget.TargetName Is Nothing Then
    '                    TmpBT.BuyingTarget = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName)
    '                    Select Case TmpBT.BuyingTarget.IsEntered
    '                        Case Trinity.cPricelistTarget.EnteredEnum.eCPP
    '                            TmpBT.BuyingTarget.NetCPP = TmpBT.BuyingTarget.NetCPP
    '                        Case Trinity.cPricelistTarget.EnteredEnum.eCPT
    '                            TmpBT.BuyingTarget.NetCPT = TmpBT.BuyingTarget.NetCPT
    '                        Case Trinity.cPricelistTarget.EnteredEnum.eDiscount
    '                            TmpBT.BuyingTarget.Discount = TmpBT.BuyingTarget.Discount
    '                    End Select
    '                End If
    'End_BookingType:
    '            Next
    '        Next

    '        'For Each TmpChan In ActiveCampaign.Channels
    '        '    For Each TmpBT In TmpChan.BookingTypes
    '        '        If Not ActiveCampaign.Contract.Channels(TmpChan.ChannelName) Is Nothing AndAlso Not ActiveCampaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name) Is Nothing Then
    '        '            If ActiveCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Pricelist.Targets.Count > 0 Then
    '        '                For Each TmpAV As Trinity.cAddedValue In ActiveCampaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(ActiveCampaign.Contract.Channels(TmpChan.ChannelName).ActiveContractLevel)(TmpBT.Name).AddedValues
    '        '                    If Not TmpBT.AddedValues(TmpAV.ID) Is Nothing Then
    '        '                        TmpBT.AddedValues.Remove(TmpAV.ID)
    '        '                    End If
    '        '                    With TmpBT.AddedValues.Add(TmpAV.Name, TmpAV.ID)
    '        '                        .IndexGross = TmpAV.IndexGross
    '        '                        .IndexNet = TmpAV.IndexNet
    '        '                    End With
    '        '                Next
    '        '                TmpBT.Pricelist = ActiveCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Pricelist
    '        '                TmpBT.BookIt = False
    '        '            End If
    '        '            TmpBT.Indexes = ActiveCampaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(ActiveCampaign.Contract.Channels(TmpChan.ChannelName).ActiveContractLevel)(TmpBT.Name).Indexes
    '        '        End If
    '        '    Next
    '        'Next
    '        'update all labels and other information boxes about the new changes
    '        If ActiveCampaign.Costs.Count = 0 OrElse ActiveCampaign.Channels(1).BookingTypes(1).Weeks.Count = 0 Then
    '            ActiveCampaign.Costs = ActiveCampaign.Contract.Costs
    '        End If

    '        If ActiveCampaign.Contract.Combinations.Count > 0 Then

    '            ActiveCampaign.Combinations.Clear()
    '            For Each TmpCombo As Trinity.cCombination In ActiveCampaign.Contract.Combinations
    '                With ActiveCampaign.Combinations.Add
    '                    .CombinationOn = TmpCombo.CombinationOn
    '                    .ID = TmpCombo.ID
    '                    .Name = TmpCombo.Name
    '                    .ShowAsOne = TmpCombo.ShowAsOne
    '                    For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
    '                        TmpCC.Bookingtype = ActiveCampaign.Channels(TmpCC.Bookingtype.ParentChannel.ChannelName).BookingTypes(TmpCC.Bookingtype.Name)
    '                        .Relations.Add(ActiveCampaign.Channels(TmpCC.Bookingtype.ParentChannel.ChannelName).BookingTypes(TmpCC.Bookingtype.Name), TmpCC.Relation)
    '                        'Deprecated: ShowMe now derives from ShowAsOne. See Bookingtype.ShowMe
    '                        '
    '                        'If .ShowAsOne Then
    '                        '    TmpCC.Bookingtype.ShowMe = False
    '                        'Else
    '                        '    TmpCC.Bookingtype.ShowMe = True
    '                        'End If
    '                    Next
    '                End With
    '            Next
    '        End If

    '    End Sub

    Private Sub OpenContractFromFile()
        'a contract can be for several campaigns, this code loads a saved contract
        Dim dlg As New System.Windows.Forms.OpenFileDialog
        'Dim TmpChan As Trinity.cChannel


        dlg.CheckFileExists = True
        dlg.DefaultExt = "*.tct"
        dlg.FileName = "*.tct"
        dlg.Filter = "Trinity contracts|*.tct"
        dlg.Multiselect = False
        dlg.Title = "Open Trinity Contract..."
        'if you dont click OK then we exit the sub
        If dlg.ShowDialog(Me) <> Windows.Forms.DialogResult.OK Then Exit Sub
        If ActiveCampaign.Contract IsNot Nothing Then
            If Windows.Forms.MessageBox.Show("Applying a new contract on a campaign that already has a contract loaded will reset all 'Use'-checkboxes in the 'Indexes / Added Values' tab.", "T R I N I T Y", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
            For Each _chan As Trinity.cContractChannel In ActiveCampaign.Contract.Channels
                For Each _bt As Trinity.cContractBookingtype In _chan.BookingTypes(_chan.ActiveContractLevel)
                    For Each _index As Trinity.cIndex In _bt.Indexes
                        If ActiveCampaign.Channels(_chan.ChannelName) IsNot Nothing AndAlso ActiveCampaign.Channels(_chan.ChannelName).BookingTypes(_bt.Name) IsNot Nothing Then
                            If ActiveCampaign.Channels(_chan.ChannelName).BookingTypes(_bt.Name).Indexes.Exists(_index.ID) Then
                                ActiveCampaign.Channels(_chan.ChannelName).BookingTypes(_bt.Name).Indexes.Remove(_index.ID)
                            End If
                        End If
                    Next
                    For Each _target As Trinity.cContractTarget In _bt.ContractTargets
                        For Each _index As Trinity.cIndex In _target.Indexes
                            If ActiveCampaign.Channels(_chan.ChannelName) IsNot Nothing AndAlso ActiveCampaign.Channels(_chan.ChannelName).BookingTypes(_bt.Name) IsNot Nothing AndAlso ActiveCampaign.Channels(_chan.ChannelName).BookingTypes(_bt.Name).Pricelist.Targets(_target.TargetName) IsNot Nothing Then
                                If ActiveCampaign.Channels(_chan.ChannelName).BookingTypes(_bt.Name).Pricelist.Targets(_target.TargetName).Indexes.Exists(_index.ID) Then
                                    ActiveCampaign.Channels(_chan.ChannelName).BookingTypes(_bt.Name).Pricelist.Targets(_target.TargetName).Indexes.Remove(_index.ID)
                                End If
                                If ActiveCampaign.Channels(_chan.ChannelName).BookingTypes(_bt.Name).Indexes.Exists(_index.ID) Then
                                    ActiveCampaign.Channels(_chan.ChannelName).BookingTypes(_bt.Name).Indexes.Remove(_index.ID)
                                End If
                            End If
                        Next
                    Next
                Next
            Next
        End If
        ActiveCampaign.Contract = New Trinity.cContract(Campaign)
        ActiveCampaign.Contract.Load(dlg.FileName)

        _progress = New frmProgress()
        _progress.Show()
        AddHandler ActiveCampaign.Contract.ApplyingContract, AddressOf ApplyingContract
        ActiveCampaign.Contract.ApplyToCampaign()
        _progress.Hide()
        _progress.Dispose()

        lblContract.Text = ActiveCampaign.Contract.Name
        ActiveCampaign.Contract.Path = dlg.FileName

        frmSetup_Activated(New Object, New EventArgs)
    End Sub

    Private Sub mnuEditContract_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditContract.Click

        If ActiveCampaign.IsStripped AndAlso Windows.Forms.MessageBox.Show("Unused channels has been removed from this campaign." & vbCrLf & vbCrLf & "Channels must be reloaded for Contracts to be edited." & vbCrLf & "To remove unused channels again, click 'Apply' in the 'Index / Added values'-tab", "T R I N I T Y", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
            ActiveCampaign.ReloadDeletedChannels()
            LongName.Clear()
            For i As Integer = 1 To Campaign.Channels.Count
                LongName.Add(Campaign.Channels(i).Shortname, Campaign.Channels(i).ChannelName)
            Next
        ElseIf ActiveCampaign.IsStripped Then
            Exit Sub
        End If

        'Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType

        'opens the contract window
        Dim frm As New frmContract

        'Add all channels and bookingtypes not currently in contract
        For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels
            If ActiveCampaign.Contract.Channels(TmpChan.ChannelName) Is Nothing Then
                With ActiveCampaign.Contract.Channels.Add(TmpChan.ChannelName, "")
                    .AddLevel()
                    .ActiveContractLevel = 1
                    .ListNumber = TmpChan.ListNumber
                    .Shortname = TmpChan.Shortname
                End With
            Else
                With ActiveCampaign.Contract.Channels(TmpChan.ChannelName)
                    .ListNumber = TmpChan.ListNumber
                    .Shortname = TmpChan.Shortname
                    .Agencycommission = TmpChan.AgencyCommission
                End With
            End If
            For Each TmpBT In TmpChan.BookingTypes
                '
                If ActiveCampaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(1)(TmpBT.Name) Is Nothing Then
                    For i As Integer = 1 To ActiveCampaign.Contract.Channels(TmpChan.ChannelName).LevelCount
                        With ActiveCampaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(i).Add(TmpBT.Name)
                            .ShortName = TmpBT.Shortname
                            .IsRBS = TmpBT.IsRBS
                            .IsSpecific = TmpBT.IsSpecific
                            .PrintDayparts = TmpBT.PrintDayparts
                            .PrintBookingCode = TmpBT.PrintBookingCode
                            .ParentChannel = ActiveCampaign.Contract.Channels(TmpChan.ChannelName)
                            .Indexes = New Trinity.cIndexes(Campaign, ActiveCampaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(1)(TmpBT.Name))
                            .MaxDiscount = TmpBT.MaxDiscount
                        End With
                    Next
                Else
                    'For each level that exists for this channel in the contract, set the shortname and other parameters
                    'according to the campaign channels
                    For i As Integer = 1 To ActiveCampaign.Contract.Channels(TmpChan.ChannelName).LevelCount
                        With ActiveCampaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(i)(TmpBT.Name)
                            'This next if block might be stupid .. but why use i anyway
                            If Not ActiveCampaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(i)(TmpBT.Name) Is Nothing Then
                                .ShortName = TmpBT.Shortname
                                .IsRBS = TmpBT.IsRBS
                                .IsSpecific = TmpBT.IsSpecific
                                .PrintDayparts = TmpBT.PrintDayparts
                                .PrintBookingCode = TmpBT.PrintBookingCode
                            End If
                        End With
                    Next
                End If
                For i As Integer = 1 To ActiveCampaign.Contract.Channels(TmpChan.ChannelName).LevelCount
                    If Not ActiveCampaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(i)(TmpBT.Name) Is Nothing Then
                        For Each pt As Trinity.cContractTarget In ActiveCampaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(i)(TmpBT.Name).ContractTargets
                            If Not TmpBT.Pricelist.Targets(pt.TargetName) Is Nothing Then
                                pt.CalcCPP = TmpBT.Pricelist.Targets(pt.TargetName).CalcCPP
                                pt.AdEdgeTargetName = TmpBT.Pricelist.Targets(pt.TargetName).Target.TargetName
                            End If
                        Next
                    End If
                Next
            Next
        Next



        frm.ShowDialog()

        lblContract.Text = ActiveCampaign.Contract.Name

        _progress = New frmProgress()
        _progress.Show()
        AddHandler ActiveCampaign.Contract.ApplyingContract, AddressOf ApplyingContract
        ActiveCampaign.Contract.ApplyToCampaign()
        _progress.Hide()
        _progress.Dispose()

        frmSetup_Activated(New Object, New EventArgs)
    End Sub

    Dim _progress As frmProgress
    Sub ApplyingContract(sender As Object, e As Trinity.cContract.ContractEventArgs)
        Dim _bt As Trinity.cContractBookingtype = e.Bookingtype

        _progress.MaxValue = 100
        _progress.Progress = e.Progress
        _progress.Status = String.Format("Applying contract to {0}", _bt.ToString)

    End Sub

    Private Sub frmSetup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'depending on Marathon settings we display costs differently
        If TrinitySettings.MarathonEnabled Then
            grdCosts.Columns.Clear()
            grdCosts.Columns.Add("colCost", "Cost")
            grdCosts.Columns(0).Width = 95
            Dim col As New DataGridViewComboBoxColumn
            col.Name = "colType"
            col.HeaderText = "Type"
            col.Items.Add("Fixed")
            col.Items.Add("Percent")
            col.Items.Add("Per Unit")
            col.Items.Add("On Discount")
            grdCosts.Columns.Add(col)
            grdCosts.Columns(1).Width = 95
            grdCosts.Columns.Add("colAmount", "Amount")
            grdCosts.Columns(2).Width = 95
            col = New DataGridViewComboBoxColumn
            col.Name = "colCostOn"
            col.HeaderText = "On"
            col.Items.Add("Media Net")
            col.Items.Add("Net")
            col.Items.Add("Net Net")
            col.Items.Add("Ratecard")
            grdCosts.Columns.Add(col)
            grdCosts.Columns(3).Width = 95
            grdCosts.Columns.Add("colMarathonID", "Marathon ID")
            grdCosts.Columns(4).Width = 95
        Else
            grdCosts.Columns.Clear()
            grdCosts.Columns.Add("colCost", "Cost")
            grdCosts.Columns(0).Width = 95
            Dim col As New DataGridViewComboBoxColumn
            col.Name = "colType"
            col.HeaderText = "Type"
            col.Items.Add("Fixed")
            col.Items.Add("Percent")
            col.Items.Add("Per Unit")
            col.Items.Add("On Discount")
            grdCosts.Columns.Add(col)
            grdCosts.Columns(1).Width = 95
            grdCosts.Columns.Add("colAmount", "Amount")
            grdCosts.Columns(2).Width = 95
            col = New DataGridViewComboBoxColumn
            col.Name = "colCostOn"
            col.HeaderText = "On"
            col.Items.Add("Media Net")
            col.Items.Add("Net")
            col.Items.Add("Net Net")
            col.Items.Add("Ratecard")
            grdCosts.Columns.Add(col)
            grdCosts.Columns(3).Width = 95
        End If
        If TrinitySettings.AdtooxEnabled Then
            cmdSaveToAdtoox.Visible = True
            cmdFindInAdtoox.Visible = True
            cmdPlayMovie.Visible = True
        Else
            cmdSaveToAdtoox.Visible = False
            cmdFindInAdtoox.Visible = False
            cmdPlayMovie.Visible = False
        End If

        'populates the client combo box
        PopulateClientCombo()


        'get the planners and buyers
        cmbBuyer.Items.Clear()
        cmbPlanner.Items.Clear()

        Dim People As List(Of Trinity.cPerson) = DBReader.getAllPeople
        'Dim People As List(Of Trinity.cPerson) = Nothing

        If Not People Is Nothing AndAlso People.Count > 0 AndAlso Not TrinitySettings.SaveCampaignsAsFiles Then
            cmbBuyer.ValueMember = "id"
            cmbPlanner.ValueMember = "id"
            cmbBuyer.DisplayMember = "name"
            cmbPlanner.DisplayMember = "name"
            For Each tmpPerson As Trinity.cPerson In People
                If tmpPerson.statusActive <> False Then
                    cmbPlanner.Items.Add(tmpPerson)
                    cmbBuyer.Items.Add(tmpPerson)
                End if
            Next
        Else
            If planners Is Nothing Or buyers Is Nothing Then
                If System.IO.File.Exists(Trinity.Helper.Pathify(TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork)) & "people.xml") Then
                    'this code gets all planners and buyers from a XML file
                    'note that not all locations h ave a XML file, some still have the old people.lst file
                    Dim xmldoc As New Xml.XmlDocument
                    xmldoc.Load(Trinity.Helper.Pathify(TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork)) & "people.xml")

                    planners = xmldoc.GetElementsByTagName("planners").Item(0)
                    buyers = xmldoc.GetElementsByTagName("buyers").Item(0)

                    Dim xmlTmp As Xml.XmlElement
                    For Each xmlTmp In planners.ChildNodes
                        cmbPlanner.Items.Add(xmlTmp.GetAttribute("name"))
                    Next


                    For Each xmlTmp In buyers.ChildNodes
                        cmbBuyer.Items.Add(xmlTmp.GetAttribute("name"))
                    Next

                    xmldoc = Nothing
                Else
                    'read the planners and buyers from the people.lst file
                    Using sr As System.IO.StreamReader = New System.IO.StreamReader(Trinity.Helper.Pathify(TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork)) & "people.lst")
                        Dim line As String
                        Do
                            line = sr.ReadLine()
                            If Not line Is Nothing Then
                                cmbBuyer.Items.Add(line)
                                cmbPlanner.Items.Add(line)
                            End If
                        Loop Until line Is Nothing
                        sr.Close()
                    End Using
                End If
            End If
        End If
        Dim check As New Trinity.cPricelistCheck(ActiveCampaign, Me)
        Dim Thread1 As New System.Threading.Thread(AddressOf check.checkPricelists)
        Thread1.IsBackground = True
        Thread1.Start()
        colDP.HeaderCell.Style.WrapMode = DataGridViewTriState.False
    End Sub

    Public Sub setPricelistLabel(ByVal Errors As String)
        strPricelistUpdate = Errors

        If Errors = "" Then
            lblOldPricelist.Visible = False
        Else
            lblOldPricelist.Visible = True
        End If
        lblOldPricelist.Text = "The pricelists of this campaign are not all up to date"
    End Sub

    Private Sub cmbClient_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClient.SelectedIndexChanged
        Saved = False
        PopulateProductCombo()
    End Sub

    Private Sub cmbProduct_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProduct.SelectedIndexChanged
        Saved = False
        If DirectCast(cmbProduct.SelectedItem, CBItem).Tag <> ActiveCampaign.ProductID Then
            Dim product As DataTable = DBReader.getAllFromProducts(DirectCast(cmbProduct.SelectedItem, CBItem).Tag)
            Dim TmpAdedgeBrands() = product.Rows(0)("AdedgeBrands").ToString.Split("|")
            ActiveCampaign.AdEdgeProducts.Clear()
            For i As Integer = TmpAdedgeBrands.GetLowerBound(0) To TmpAdedgeBrands.GetUpperBound(0)
                If Not TmpAdedgeBrands(i) = "" Then
                    ActiveCampaign.AdEdgeProducts.Add(TmpAdedgeBrands(i))
                End If
            Next
        End If
        ActiveCampaign.ProductID = DirectCast(cmbProduct.SelectedItem, CBItem).Tag
    End Sub

    Private Sub cmdEditClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditClient.Click
        'opens the add client window with the "edit" tag, this means the name will be edited from the datebase
        frmAddClient.Tag = "EDIT"
        frmAddClient.ShowDialog()
        'repopulate the combo box after the changes
        PopulateClientCombo()
    End Sub

    Private Sub cmdAddProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddProduct.Click
        'make sure no add product windows are running, then open a new add product window for the user to fill
        frmAddProduct.Dispose()
        frmAddProduct.Tag = ""
        frmAddProduct.ShowDialog()
        'if a new product was registerd we need to repoulate the combo box
        If frmAddProduct.txtName.Text <> "" Then
            PopulateProductCombo()
            cmbProduct.Text = frmAddProduct.txtName.Text
        End If
    End Sub

    Private Sub cmdEditProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditProduct.Click
        'mke sure a product is selected
        If cmbClient.SelectedItem Is Nothing Then
            Exit Sub
        End If
        'opens a add product form with the tag "edit", this mean the name will be changed  not created
        frmAddProduct.Tag = "EDIT"
        frmAddProduct.ShowDialog()
        'update the combo box
        PopulateProductCombo()
    End Sub

    Public Sub UpdateChannelGrid()
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim i As Integer

        'Only show Max column in Denmark
        colMax.Visible = (ActiveCampaign.Area = "DK")

        'clear the grid and disable films
        grdChannels.Rows.Clear()
        tpFilms.Enabled = False
        tpCombinations.Enabled = False
        'disables the "next" button
        cmdChannelsNext.Enabled = False
        For Each TmpChan In ActiveCampaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                'if a booking occurs
                If TmpBT.BookIt Then
                    grdChannels.Rows.Add() 'add a new line to the grid and add the channel info
                    DirectCast(grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(0), ExtendedComboBoxCell).Items.clear()
                    For Each TmpChan2 As Trinity.cChannel In ActiveCampaign.Channels
                        For Each TmpBT2 As Trinity.cBookingType In TmpChan2.BookingTypes

                            If TmpBT2.Pricelist.Targets.Count > 0 Then
                                'adds all channels to TmpBT
                                DirectCast(grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(0), ExtendedComboBoxCell).Items.Add(TmpBT2)
                                If ActiveCampaign.Contract IsNot Nothing Then
                                    If ActiveCampaign.Contract.Channels(TmpBT2.ParentChannel.ChannelName) Is Nothing OrElse ActiveCampaign.Contract.Channels(TmpBT2.ParentChannel.ChannelName).BookingTypes(ActiveCampaign.Contract.Channels(TmpBT2.ParentChannel.ChannelName).ActiveContractLevel)(TmpBT2.Name) Is Nothing OrElse ActiveCampaign.Contract.Channels(TmpBT2.ParentChannel.ChannelName).BookingTypes(ActiveCampaign.Contract.Channels(TmpBT2.ParentChannel.ChannelName).ActiveContractLevel)(TmpBT2.Name).ContractTargets.Count = 0 Then
                                        DirectCast(grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(0), ExtendedComboBoxCell).SetItemStyle(TmpBT2, New StyleableComboboxStyle With {.ForeColor = Color.LightGray})
                                    End If
                                End If
                            End If

                        Next
                    Next

                    grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(0).Value = TmpBT


                    Try
                        grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(1).Value = TmpBT.BuyingTarget.TargetName
                        'alters the cell depending on if CPT or CPP is used
                        If cmbCPT.Text = "CPT" Then
                            grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(2).Value = TmpBT.BuyingTarget.NetCPT
                        ElseIf cmbCPT.Text = "CPP" Then
                            grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(2).Value = TmpBT.BuyingTarget.NetCPP
                        ElseIf cmbCPT.Text = "Enhancement" Then
                            grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(2).Value = TmpBT.BuyingTarget.Enhancement
                        Else
                            grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(2).Value = TmpBT.BuyingTarget.Discount
                            If TmpBT.BuyingTarget.Discount > TmpBT.MaxDiscount Then
                                grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(2).Style.BackColor = Color.Red
                            End If
                        End If
                        grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(3).Value = TmpBT.MaxDiscount
                    Catch ex As Exception
                        MessageBox.Show("The buying target for " & TmpBT.ToString & " couldn't be found")
                    End Try


                    Try
                        Dim _dpSplit As String = ""
                        For i = 0 To TmpBT.Dayparts.Count - 1
                            _dpSplit &= Format(TmpBT.Dayparts(i).Share / 100, "P0") & " / "
                            If Not grdChannels.Columns.Contains("colDP" & i) Then
                                Dim _col As New DataGridViewTextBoxColumn
                                _col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                                _col.Name = "colDP" & i
                                _col.Visible = False
                                _col.DefaultCellStyle = styleInVisible
                                grdChannels.Columns.Add(_col)
                            End If
                        Next
                        grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(4).Value = _dpSplit.Trim.TrimEnd("/").Trim
                    Catch ex As Exception
                        MessageBox.Show("The booking type " & TmpBT.ToString & " has no dayparts.")
                    End Try

                    'enables films
                    tpFilms.Enabled = True
                    tpCombinations.Enabled = True
                    'you can now change tabs
                    cmdChannelsNext.Enabled = True
                End If
            Next
        Next
    End Sub

    Public Sub UpdateChannelInfoGrid(Optional ByVal Redraw As Boolean = False)
        Dim i As Integer
        Dim j As Integer
        Dim TmpBT As Trinity.cBookingType

        colInfoChannel.Width = 75
        grdChannelInfo.Rows.Clear()
        'add the number of rows equal to the channels
        If grdChannels.Rows.Count > 0 Then grdChannelInfo.Rows.Add(grdChannels.Rows.Count)

        If Redraw Then 'if redraw is selected we redraw the entire structure
            While grdChannelInfo.Columns.Count > 3
                grdChannelInfo.Columns.Remove(grdChannelInfo.Columns(3))
            End While
            lblGrossCPP.Left = grdChannelInfo.GetColumnDisplayRectangle(2, False).Right
            Dim colTotal As New DataGridViewTextBoxColumn
            colTotal.HeaderText = "Total"
            colTotal.Width = 50
            colTotal.DefaultCellStyle.Format = "N0"
            grdChannelInfo.Columns.Add(colTotal)

            'TODO: **DAYPART** New solution for the case below
            'Dim colDP As DataGridViewTextBoxColumn
            'colDP = Nothing
            'For i = 0 To ActiveCampaign.Dayparts.Count - 1
            '    colDP = New DataGridViewTextBoxColumn
            '    colDP.HeaderText = ActiveCampaign.Dayparts(i).Name
            '    colDP.DefaultCellStyle.Format = "N0"
            '    colDP.Width = 50
            '    grdChannelInfo.Columns.Add(colDP)
            '    lblGrossCPP.Width = lblGrossCPP.Width + grdChannelInfo.GetColumnDisplayRectangle(colDP.Index, False).Width
            'Next
            Dim colDP As New DataGridViewTextBoxColumn
            colDP.Name = "colInfoDP"
            colDP.HeaderText = "CPP per daypart"
            colDP.ReadOnly = True
            colDP.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            colDP.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            colDP.DefaultCellStyle.Format = "N0"
            grdChannelInfo.Columns.Add(colDP)
            lblGrossCPP.Width = colTotal.Width + colDP.Width

            Dim colNat As New DataGridViewTextBoxColumn
            colNat.HeaderText = "Nat"
            colNat.Width = 50
            colNat.DefaultCellStyle.Format = "N0"
            grdChannelInfo.Columns.Add(colNat)

            Dim colChn As New DataGridViewTextBoxColumn
            colChn.HeaderText = "Chn"
            colChn.Width = 50
            colChn.DefaultCellStyle.Format = "N0"
            grdChannelInfo.Columns.Add(colChn)

            lblNetCPP.Left = grdChannelInfo.GetColumnDisplayRectangle(colNat.Index, False).Left
            lblNetCPP.Width = grdChannelInfo.GetColumnDisplayRectangle(colNat.Index, False).Width + grdChannelInfo.GetColumnDisplayRectangle(colChn.Index, False).Width
        End If
        'repopulates the grid info 
        For i = 0 To grdChannelInfo.Rows.Count - 1
            Dim TmpCell As ExtendedComboBoxCell = grdChannels.Rows(i).Cells(0)
            If Not TmpCell.Value Is Nothing Then
                TmpBT = TmpCell.Value
                ' Define variables to check if CPP is different in different weeks
                Dim BreakPoint As Boolean = False
                Dim Initialcpp As Single
                Dim Idx As Decimal = 1
                For Each TmpIndex As Trinity.cIndex In TmpBT.Indexes
                    If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
                        If (TmpIndex.FromDate.ToOADate <= TmpBT.Weeks(1).StartDate And TmpIndex.ToDate.ToOADate >= TmpBT.Weeks(1).StartDate) Or (TmpIndex.FromDate.ToOADate <= TmpBT.Weeks(1).EndDate And TmpIndex.ToDate.ToOADate >= TmpBT.Weeks(1).EndDate) Then
                            Idx = Idx * (TmpIndex.Index / 100)
                        End If
                    End If
                Next
                If TmpBT.Weeks(1).SpotIndex(True) > 0 Then
                    Idx *= TmpBT.Weeks(1).SpotIndex(True) / 100
                End If
                Initialcpp = TmpBT.Weeks(1).GrossCPP / Idx / (TmpBT.Weeks(1).AddedValueIndexGross)
                'If i = 3 Then Stop
                For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                    Idx = 1
                    For Each TmpIndex As Trinity.cIndex In TmpBT.Indexes
                        If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
                            If (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate) Or (TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate And TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then
                                Idx = Idx * (TmpIndex.Index / 100)
                            End If
                        End If
                    Next
                    If TmpWeek.SpotIndex(True) > 0 Then
                        Idx *= TmpWeek.SpotIndex(True) / 100
                    End If
                    If Format(TmpWeek.GrossCPP / Idx, "N2") <> Format(Initialcpp, "N2") Then
                        BreakPoint = True
                        Exit For
                    End If
                Next
                'Adds the channel name
                grdChannelInfo.Rows(i).Cells(0).Value = TmpBT.ParentChannel.Shortname & " " & TmpBT.Shortname
                'adds the national universe size for the targeted group 
                grdChannelInfo.Rows(i).Cells(1).Value = TmpBT.BuyingTarget.getUniSizeNat(0)
                ''adds the channel universe size for the targeted group 
                grdChannelInfo.Rows(i).Cells(2).Value = TmpBT.BuyingTarget.getUniSizeUni(0)
                If grdChannelInfo.Columns.Count > 3 Then
                    'adds the Total Gross CPP
                    grdChannelInfo.Rows(i).Cells(3).Value = Initialcpp
                    If BreakPoint Then
                        grdChannelInfo.Rows(i).Cells(3).Style.ForeColor = Color.Blue
                    Else
                        grdChannelInfo.Rows(i).Cells(3).Style.ForeColor = Color.Black
                    End If

                    If TmpBT.BuyingTarget.CalcCPP Then
                        Dim _dpPrices As String = ""
                        'if we have daypart prices we need to calculate the price from the daypart split
                        For j = 0 To TmpBT.Dayparts.Count - 1
                            'Adds the daypart CPP (day,prime,night)
                            Dim cppSum As Double = 0
                            For z As Integer = ActiveCampaign.StartDate To ActiveCampaign.EndDate
                                cppSum += TmpBT.BuyingTarget.GetCPPForDate(z, j)
                            Next
                            _dpPrices &= Format(cppSum / (ActiveCampaign.EndDate - ActiveCampaign.StartDate + 1), "N0") & " / "
                        Next
                        If BreakPoint Then
                            grdChannelInfo.Rows(i).Cells(4).Style.ForeColor = Color.Blue
                        Else
                            grdChannelInfo.Rows(i).Cells(4).Style.ForeColor = Color.Black
                        End If
                        grdChannelInfo.Rows(i).Cells(4).Value = _dpPrices.Trim.TrimEnd("/").Trim
                    Else
                        'if we have a all day price it is the same no matter what daypart
                        For j = 0 To ActiveCampaign.Dayparts.Count - 1
                            grdChannelInfo.Rows(i).Cells(4).Value = grdChannelInfo.Rows(i).Cells(3).Value
                        Next
                    End If

                    If TmpBT.Weeks(1).Index > 0 Then
                        grdChannelInfo.Rows(i).Cells(6).Value = TmpBT.Weeks(1).NetCPP30 / TmpBT.Weeks(1).Index
                        grdChannelInfo.Rows(i).Cells(5).Value = TmpBT.Weeks(1).NetCPP30 / TmpBT.Weeks(1).Index / TmpBT.BuyingTarget.UniIndex(ActiveCampaign.StartDate)
                    Else
                        grdChannelInfo.Rows(i).Cells(6).Value = 0
                        grdChannelInfo.Rows(i).Cells(5).Value = 0
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub grdChannels_CellContextMenuStripNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventArgs) Handles grdChannels.CellContextMenuStripNeeded

    End Sub

    Private Sub grdChannels_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdChannels.CellEndEdit
        Saved = False
    End Sub

    Public Delegate Sub ChangeCellDelegate(ByVal e As DataGridViewCellEventArgs)

    Private _lastRow As Integer
    Private styleInVisible As New DataGridViewCellStyle With {.BackColor = SystemColors.AppWorkspace, .ForeColor = SystemColors.AppWorkspace}
    Private styleVisible As New DataGridViewCellStyle With {.BackColor = Color.White, .ForeColor = Color.Black}
    Private Sub grdChannels_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdChannels.CellEnter
        If e.ColumnIndex = 4 Or (e.ColumnIndex > 4 And e.RowIndex <> _lastRow) Then
            Dim TmpBT As Trinity.cBookingType = grdChannels.Rows(e.RowIndex).Cells(0).Value
            If TmpBT Is Nothing Then Exit Sub

            If TmpBT.Dayparts.Count = 0 Then
                TmpBT.Dayparts = Campaign.Dayparts
            End If
            If grdChannels.Columns("colDP0") IsNot Nothing Then


                For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                    Dim _col As DataGridViewTextBoxColumn = grdChannels.Columns("colDP" & i)
                    For Each _row As DataGridViewRow In grdChannels.Rows
                        If _row IsNot grdChannels.Rows(e.RowIndex) Then
                            _row.Cells(_col.Name).Style = styleInVisible
                        End If
                    Next
                    _col.HeaderText = TmpBT.Dayparts(i).Name
                    grdChannels.Rows(e.RowIndex).Cells(_col.Name).Value = Format(TmpBT.Dayparts(i).Share / 100, "P0")
                    grdChannels.Rows(e.RowIndex).Cells(_col.Name).Style = styleVisible
                    Me.BeginInvoke(Sub()
                                       _col.Visible = True
                                   End Sub)
                Next
                grdChannels.Columns("colDP0").Selected = True
                grdChannels.BeginInvoke(New ChangeCellDelegate(AddressOf ChangeCell), e)
                If e.RowIndex <> _lastRow Then
                    For c As Integer = 5 + TmpBT.Dayparts.Count To grdChannels.ColumnCount - 1
                        Me.BeginInvoke(Sub(_c)
                                           grdChannels.Columns(_c).Visible = False
                                       End Sub, c)
                    Next
                End If
            Else
                Exit Sub
            End If
        ElseIf e.ColumnIndex > 4 Then
            grdChannels.Columns(4).Visible = False
        Else
            grdChannels.Columns(4).Visible = True
            For c As Integer = 5 To grdChannels.ColumnCount - 1
                Me.BeginInvoke(Sub(_c)
                                   grdChannels.Columns(_c).Visible = False
                               End Sub, c)
            Next
        End If
    End Sub

    Sub ChangeCell(ByVal e As DataGridViewCellEventArgs)
        grdChannels.CurrentCell = grdChannels.Rows(e.RowIndex).Cells("colDP0")
    End Sub

    Private Sub grdChannels_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdChannels.CellLeave
        _lastRow = e.RowIndex
    End Sub

    Private Sub grdChannels_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdChannels.CellValueChanged
        Dim TmpTarget As Trinity.cPricelistTarget
        Dim TmpBT As Trinity.cBookingType = Nothing
        Dim i As Integer
        Dim Total As Single



        If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then
            Exit Sub
        End If
        'If e.ColumnIndex > 3 Then
        '    If Format(grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value, "0%") <> grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue Then
        '        grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Val(grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString.TrimEnd("%")) / 100
        '    End If
        'End If
        If grdChannels.Rows(e.RowIndex).Cells.Count = 3 Then Exit Sub
        If e.ColumnIndex = 0 Then
            Dim TmpCell As ExtendedComboBoxCell = grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex)

            TmpBT = TmpCell.Value
            If Not grdChannels.Rows(e.RowIndex).Tag Is Nothing Then
                Try
                    DirectCast(grdChannels.Rows(e.RowIndex).Tag, Trinity.cBookingType).BookIt = False
                Catch ex As Exception

                    ' If it fails, ask the user to remove the spots
                    If MessageBox.Show("There are spots attached to the previously chosen bookingtype. By changing, any attached spots will be removed from the campaign!" & vbNewLine & "Do you want to continue?", "T R I N I T Y", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.Yes Then

                        ' Remove all spots and try again
                        DirectCast(grdChannels.Rows(e.RowIndex).Tag, Trinity.cBookingType).RemoveAllSpots()
                        DirectCast(grdChannels.Rows(e.RowIndex).Tag, Trinity.cBookingType).BookIt = False
                    Else

                        UpdateChannelGrid()
                        UpdateChannelInfoGrid()
                        Exit Sub

                    End If
                End Try

            End If
            TmpBT.BookIt = True
            grdChannels.Rows(e.RowIndex).Tag = TmpBT

            TmpCell = grdChannels.Rows(e.RowIndex).Cells(colBuyingTarget.Index)
            TmpCell.Items.Clear()

            For Each TmpTarget In (From _targ As Trinity.cPricelistTarget In TmpBT.Pricelist.Targets Select _targ)
                Dim _target As Trinity.cPricelistTarget = TmpTarget
                If (From _period As Trinity.cPricelistPeriod In _target.PricelistPeriods Select _period Where _period.FromDate.ToOADate <= _activeCampaign.EndDate AndAlso _period.ToDate.ToOADate >= _activeCampaign.StartDate).Count > 0 Then
                    TmpCell.Items.Add(TmpTarget.TargetName)
                    If ActiveCampaign.Contract IsNot Nothing AndAlso ActiveCampaign.Contract.Channels(TmpBT.ParentChannel.ChannelName) IsNot Nothing AndAlso ActiveCampaign.Contract.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(ActiveCampaign.Contract.Channels(TmpBT.ParentChannel.ChannelName).ActiveContractLevel)(TmpBT.Name) IsNot Nothing AndAlso Not ActiveCampaign.Contract.Channels(TmpBT.ParentChannel.ChannelName).BookingTypes(ActiveCampaign.Contract.Channels(TmpBT.ParentChannel.ChannelName).ActiveContractLevel)(TmpBT.Name).ContractTargets.Contains(TmpTarget.TargetName) Then
                        DirectCast(TmpCell, ExtendedComboBoxCell).SetItemStyle(TmpTarget.TargetName, New StyleableComboboxStyle With {.ForeColor = Color.LightGray})
                    End If
                End If
            Next


        ElseIf e.ColumnIndex = 1 Then
            Dim TmpCell As ExtendedComboBoxCell = grdChannels.Rows(e.RowIndex).Cells(0)
            TmpBT = TmpCell.Value
            If Not grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "" Then
                If Not (TmpBT.BuyingTarget.TargetName = TmpBT.Pricelist.Targets(grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value).TargetName) Then
                    TmpBT.BuyingTarget = TmpBT.Pricelist.Targets(grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                    Dim _sum As Integer = 0
                    For i = 0 To TmpBT.Dayparts.Count - 1
                        _sum += TmpBT.BuyingTarget.DefaultDayPart(i)
                    Next
                    If _sum = 100 Then
                        For i = 0 To TmpBT.Dayparts.Count - 1
                            TmpBT.Dayparts(i).Share = TmpBT.BuyingTarget.DefaultDayPart(i)
                        Next
                    Else
                        For i = 0 To TmpBT.Dayparts.Count - 1
                            TmpBT.Dayparts(i).Share = TmpBT.DefaultDaypart(i)
                        Next
                    End If
                End If
                Dim Added As Boolean = False
                Dim TmpCPP As Single = TmpBT.BuyingTarget.NetCPP
                For Each TmpIndex As Trinity.cIndex In TmpBT.BuyingTarget.Indexes
                    If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eFixedCPP Then
                        If (TmpIndex.FromDate.ToOADate <= Campaign.EndDate AndAlso TmpIndex.ToDate.ToOADate >= Campaign.StartDate) Then
                            If Not Added Then
                                TmpCPP = TmpIndex.FixedCPP
                                TmpBT.BuyingTarget.NetCPP = TmpCPP
                                Added = True
                            End If
                            TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP
                            TmpIndex.Index = (TmpIndex.FixedCPP / TmpCPP) * 100
                            TmpIndex.UseThis = True
                        End If
                    End If
                Next
                Dim _dpSplit As String = ""
                Dim _dpTotBuyTarget As Byte = 0
                Dim _dpTotBookingType As Byte = 0
                Dim _dpTotPricelist As Byte = 0
                For i = 0 To TmpBT.Dayparts.Count - 1
                    _dpTotBuyTarget += TmpBT.BuyingTarget.DefaultDayPart(i)
                    _dpTotBookingType += TmpBT.Dayparts(i).Share
                    _dpTotPricelist += TmpBT.Pricelist.Targets(grdChannels.Rows(e.RowIndex).Cells(1).Value).DefaultDaypart(i)
                Next

                For i = 0 To TmpBT.Dayparts.Count - 1
                    'try to get the targets daypartsplit, if not available use the default on the Booking Type

                    If _dpTotBookingType = 100 Then
                        _dpSplit &= Format(TmpBT.Dayparts(i).Share / 100, "P0") & " / "
                    ElseIf _dpTotBuyTarget = 100 Then
                        _dpSplit &= Format(TmpBT.BuyingTarget.DefaultDayPart(i) / 100, "P0") & " / "
                        TmpBT.Dayparts(i).Share = TmpBT.BuyingTarget.DefaultDayPart(i)
                    Else
                        _dpSplit &= Format(TmpBT.Pricelist.Targets(grdChannels.Rows(e.RowIndex).Cells(1).Value).DefaultDaypart(i) / 100, "P0") & " / "
                        TmpBT.Dayparts(i).Share = TmpBT.Pricelist.Targets(grdChannels.Rows(e.RowIndex).Cells(1).Value).DefaultDaypart(i)
                    End If
                    If Not grdChannels.Columns.Contains("colDP" & i) Then
                        Dim _col As New DataGridViewTextBoxColumn
                        _col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                        _col.Visible = False
                        _col.Name = "colDP" & i
                        _col.DefaultCellStyle = styleInVisible
                        grdChannels.Columns.Add(_col)
                    End If
                Next

                grdChannels.Rows(e.RowIndex).Cells(4).Value = _dpSplit.Trim.TrimEnd("/").Trim
                Dim bolErrorPrice As Boolean = False
                For z As Integer = ActiveCampaign.StartDate To ActiveCampaign.EndDate
                    If TmpBT.BuyingTarget.CalcCPP Then
                        For x As Integer = 0 To TmpBT.Dayparts.Count - 1
                            If Not TmpBT.BuyingTarget.hasCPPForDate(z, x) Then
                                bolErrorPrice = True
                            End If
                        Next
                    Else
                        If Not TmpBT.BuyingTarget.hasCPPForDate(z) Then
                            bolErrorPrice = True
                        End If
                    End If
                Next

                If bolErrorPrice Then
                    MsgBox("The target " & TmpBT.BuyingTarget.TargetName & " in " & TmpBT.ParentChannel.ChannelName & " does not have a pricelist covering the entire campaign period. This might cause errors", MsgBoxStyle.Critical, "No Pricelist")
                End If
            End If
            SkipIt = True
            If cmbCPT.Text = "CPT" Then
                grdChannels.Rows(e.RowIndex).Cells(2).Value = TmpBT.BuyingTarget.NetCPT
            ElseIf cmbCPT.Text = "CPP" Then
                grdChannels.Rows(e.RowIndex).Cells(2).Value = TmpBT.BuyingTarget.NetCPP
            ElseIf cmbCPT.Text = "Discount" Then

                grdChannels.Rows(e.RowIndex).Cells(2).Value = (TmpBT.BuyingTarget.Discount)
            ElseIf cmbCPT.Text = "Enhancement" Then
                grdChannels.Rows(e.RowIndex).Cells(2).Value = TmpBT.BuyingTarget.Enhancement
            End If
            grdChannels.Rows(e.RowIndex).Cells(3).Value = TmpBT.MaxDiscount
            SkipIt = False
            tpFilms.Enabled = True
            tpCombinations.Enabled = True
            cmdChannelsNext.Enabled = True
            tabSetup.Refresh()
        ElseIf e.ColumnIndex = 2 Then

            ' Make the string more tolerant and then see if its proper
            Dim tmpStr As String = grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            Dim result As Decimal = 0

            ' Strip away expected changes
            tmpStr = tmpStr.Replace("%", "").Replace("kr", "").Replace(" ", "").Replace(".", ",")

            ' See make sure that after the value stripped, its now valid to use
            If (Not Decimal.TryParse(tmpStr, result)) Then
                grdChannels.Invalidate()
                Exit Sub
            End If
            'The column where we write a value for CPP, Discount or CPT
            If Not grdChannels.EditingControl Is Nothing Then
                If grdChannels.EditingControl.Visible = True AndAlso Not SkipIt Then
                    If cmbCPT.Text = "CPT" Then
                        Dim TmpCell As ExtendedComboBoxCell = grdChannels.Rows(e.RowIndex).Cells(0)
                        TmpBT = TmpCell.Value
                        If TmpBT.BuyingTarget.TargetName Is Nothing Then
                            If grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value <> "" Then
                                Windows.Forms.MessageBox.Show("You have not chosen a Buying target.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                            grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                            Exit Sub
                        End If
                        TmpBT.BuyingTarget.NetCPT = result
                        TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).NetCPT = result
                    ElseIf cmbCPT.Text = "CPP" Then
                        Dim TmpCell As ExtendedComboBoxCell = grdChannels.Rows(e.RowIndex).Cells(0)
                        TmpBT = TmpCell.Value
                        If TmpBT.BuyingTarget.TargetName Is Nothing Then
                            If grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value <> "" Then
                                Windows.Forms.MessageBox.Show("You have not chosen a Buying target.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                            grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                            Exit Sub
                        End If
                        TmpBT.BuyingTarget.NetCPP = result
                        TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).NetCPP = result
                    ElseIf cmbCPT.Text = "Discount" Then
                        Dim TmpCell As ExtendedComboBoxCell = grdChannels.Rows(e.RowIndex).Cells(0)
                        TmpBT = TmpCell.Value
                        If TmpBT.BuyingTarget.TargetName Is Nothing Then
                            If grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value <> "" Then
                                Windows.Forms.MessageBox.Show("You have not chosen a Buying target.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                            grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                            Exit Sub
                        End If
                        grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = result.ToString()
                        TmpBT.BuyingTarget.Discount = result / 100
                        TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Discount = result / 100

                    Else
                        Dim TmpCell As ExtendedComboBoxCell = grdChannels.Rows(e.RowIndex).Cells(0)
                        TmpBT = TmpCell.Value
                        If TmpBT.BuyingTarget.TargetName Is Nothing Then
                            If grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value <> "" Then
                                Windows.Forms.MessageBox.Show("You have not chosen a Buying target.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                            grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                            Exit Sub
                        End If
                        TmpBT.BuyingTarget.Enhancement = result
                        TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Enhancement = result
                    End If
                    TmpBT.BuyingTarget.IsEntered = cmbCPT.SelectedIndex

                    If TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Discount > TmpBT.MaxDiscount Then
                        grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Red
                    End If

                End If
            End If
        ElseIf e.ColumnIndex = 3 Then
            Dim TmpCell As ExtendedComboBoxCell = grdChannels.Rows(e.RowIndex).Cells(0)
            TmpBT = TmpCell.Value
            If Not grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString = "" Then
                TmpBT.MaxDiscount = grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).FormattedValue.ToString.Trim("%") / 100
            End If
        ElseIf e.ColumnIndex > 4 Then
            Dim TmpCell As ExtendedComboBoxCell = grdChannels.Rows(e.RowIndex).Cells(0)

            Dim tmpStr As String = grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString.Trim("%")
            Dim result As Single = 0

            ' If the input is invaldi even after removeing expected characters, exit the sub as its invalid
            If (Not Single.TryParse(tmpStr, result)) Then
                Exit Sub
            End If


            TmpBT = TmpCell.Value
            TmpBT.Dayparts(e.ColumnIndex - 5).Share = grdChannels.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString.Trim("%")
            If TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eDiscount Then
                TmpBT.BuyingTarget.Discount = TmpBT.BuyingTarget.Discount
            End If
            SkipIt = True
            If cmbCPT.Text = "Discount" Then
                grdChannels.Rows(e.RowIndex).Cells(2).Value = (TmpBT.BuyingTarget.Discount)
            End If
            Dim _dpSplit As String = ""
            For i = 0 To TmpBT.Dayparts.Count - 1
                _dpSplit &= TmpBT.Dayparts(i).Share & "% / "
            Next
            grdChannels.Rows(e.RowIndex).Cells(4).Value = _dpSplit.Trim.TrimEnd("/").Trim
            SkipIt = False
        End If
        If Not TmpBT Is Nothing Then
            TmpBT.InvalidateCPPs()
            TmpBT.InvalidateTotalTRP()
            TmpBT.InvalidateActualNetValue()
        End If
        If (Not grdChannels.Rows(e.RowIndex).Cells(4).Value Is Nothing) AndAlso grdChannels.Rows(e.RowIndex).Cells(4).Value.ToString <> "" Then
            TmpBT = grdChannels.Rows(e.RowIndex).Cells(0).Value
            If TmpBT IsNot Nothing Then
                For i = 0 To TmpBT.Dayparts.Count - 1
                    Total = Total + Int(TmpBT.Dayparts(i).Share)
                Next
                If Total <> 100 Then
                    grdChannels.Rows(e.RowIndex).Cells(4).Style.BackColor = Color.Red
                Else
                    grdChannels.Rows(e.RowIndex).Cells(4).Style.BackColor = Color.White
                End If
                UpdateChannelInfoGrid()
            End If
        End If
    End Sub

    Private Sub cmdAddChannel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddChannel.Click
        If ActiveCampaign.IsStripped AndAlso Windows.Forms.MessageBox.Show("Unused channels has been removed from this campaign." & vbCrLf & vbCrLf & "Do you want to reload all channels now?", "T R I N I T Y", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            ActiveCampaign.ReloadDeletedChannels()
            LongName.Clear()
            For i As Integer = 1 To Campaign.Channels.Count
                LongName.Add(Campaign.Channels(i).Shortname, Campaign.Channels(i).ChannelName)
            Next
        End If
        Saved = False
        grdChannels.Rows.Add()
        DirectCast(grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(0), ExtendedComboBoxCell).Items.clear()

        For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                If Not TmpBT.BookIt AndAlso TmpBT.Pricelist.Targets.Count > 0 Then
                    'adds all channels
                    DirectCast(grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(0), ExtendedComboBoxCell).Items.add(TmpBT)
                End If
            Next
        Next
        grdChannelInfo.Rows.Add()
    End Sub

    Private Sub cmbCPT_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCPT.SelectedIndexChanged
        If cmbCPT.Text = "Discount" Then
            colCPT.DefaultCellStyle.Format = "P"
        Else
            colCPT.DefaultCellStyle.Format = "N"
        End If

        UpdateChannelGrid()
        UpdateChannelInfoGrid()

    End Sub

    Private Sub tabSetup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabSetup.SelectedIndexChanged
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType

        'TODO: **Dayparts** Ändra nedan så att det lirar med nya daypartklassen
        If tabSetup.SelectedTab Is tpChannels Then
            'For i As Integer = 0 To ActiveCampaign.Dayparts.Count - 1
            '    If grdChannels.Columns.Contains("col" & ActiveCampaign.Dayparts(i).Name) Then
            '        grdChannels.Columns.Remove("col" & ActiveCampaign.Dayparts(i).Name)
            '    End If
            'Next

            'Dim TmpCol As System.Windows.Forms.DataGridViewTextBoxColumn
            'For i As Integer = 0 To ActiveCampaign.Dayparts.Count - 1
            '    TmpCol = New System.Windows.Forms.DataGridViewTextBoxColumn
            '    TmpCol.Name = "col" & ActiveCampaign.Dayparts(i).Name
            '    TmpCol.HeaderText = ActiveCampaign.Dayparts(i).Name
            '    TmpCol.Width = 50
            '    TmpCol.Resizable = DataGridViewTriState.False
            '    TmpCol.DefaultCellStyle.Format = "##,##0%"
            '    grdChannels.Columns.Add(TmpCol)
            'Next

            cmbCPT.Text = TrinitySettings.DefaultCPTDropdown

            UpdateChannelInfoGrid(True)
            grdChannelInfo.ClearSelection()
            grdChannels.ClearSelection()
        ElseIf tabSetup.SelectedTab Is tpFilms Then
            chkFilmIdxAsDiscount.Checked = ActiveCampaign.FilmindexAsDiscount

            UpdateFilmGrid()
            grdFilms.ClearSelection()
            grpFilm.Visible = False
        ElseIf tabSetup.SelectedTab Is tpIndex Then
            chkMultiply.Checked = ActiveCampaign.MultiplyAddedValues
            cmbIndexChannel.Items.Clear()
            For Each TmpChan In ActiveCampaign.Channels
                For Each TmpBT In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        cmbIndexChannel.Items.Add(TmpBT)
                    End If
                Next
            Next
            grdIndexes.ClearSelection()
            grdAddedValues.ClearSelection()
        End If
        If cmbIndexChannel.Items.Count > 0 Then
            cmbIndexChannel.SelectedIndex = 0
        End If
    End Sub

    Private Sub tabSetup_SelectedIndexChanging(ByVal sender As Object, ByVal e As TabSelectionChangingArgs) Handles tabSetup.SelectedIndexChanging
        If Not tabSetup.TabPages(e.NewTabIndex).Enabled Then
            e.Cancel = True
        End If
    End Sub

    Sub UpdateFilmGrid()
        Dim TmpFilm As Trinity.cFilm
        Dim tmpchan As Trinity.cChannel
        Dim tmpBT As Trinity.cBookingType

        grdFilms.Rows.Clear()
        tpIndex.Enabled = False
        cmdFilmsNext.Enabled = False
        If ActiveCampaign.Channels(1).BookingTypes(1).Weeks.Count > 0 Then
            For Each TmpFilm In ActiveCampaign.Channels(1).BookingTypes(1).Weeks(1).Films
                grdFilms.Rows.Add()
                grdFilms.Rows(grdFilms.Rows.Count - 1).Cells(0).Value = TmpFilm.Name
                grdFilms.Rows(grdFilms.Rows.Count - 1).Cells(1).Value = TmpFilm.Description
                grdFilms.Rows(grdFilms.Rows.Count - 1).Cells(2).Value = TmpFilm.FilmString
                grdFilms.Rows(grdFilms.Rows.Count - 1).Cells(3).Value = TmpFilm.FilmLength
                tpIndex.Enabled = True
                cmdFilmsNext.Enabled = True
                'tpIndex.Refresh()
                tabSetup.Refresh()
            Next

            '    Dim TmpWeek As Trinity.cWeek
            '    For Each tmpchan In ActiveCampaign.Channels
            '        For Each tmpBT In tmpchan.BookingTypes
            '            For Each TmpWeek In tmpBT.Weeks
            '                For Each TmpFilm In TmpWeek.Films
            '                    'if you add a new film its automatically autoindexed
            '                    If Not TmpFilm.FilmLength = 30 AndAlso TmpFilm.Index = 100 Then
            '                        TmpFilm.Index = ActiveCampaign.Channels(1).BookingTypes(1).FilmIndex(TmpFilm.FilmLength)
            '                        TmpFilm.GrossIndex = ActiveCampaign.Channels(1).BookingTypes(1).FilmIndex(TmpFilm.FilmLength)
            '                    End If
            '                Next
            '            Next
            '        Next
            '    Next
        End If
    End Sub

    Private Sub cmdAddFilm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddFilm.Click
        Saved = False
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpWeek As Trinity.cWeek

        For Each TmpChan In ActiveCampaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                For Each TmpWeek In TmpBT.Weeks

                    Dim tmpIndex As Integer = 1
                    While (TmpWeek.Films.Item("Film " & tmpIndex.ToString()) IsNot Nothing)
                        'Increase index counter
                        tmpIndex += 1
                    End While

                    ' Add the film with the first free index
                    TmpWeek.Films.Add("Film " & tmpIndex).FilmLength = 30
                    TmpWeek.Films("Film " & tmpIndex).Index = 100

                Next
            Next
        Next
        UpdateFilmGrid()
    End Sub


    Private Sub cmdRemoveChannel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveChannel.Click
        Saved = False
        If grdChannels.SelectedRows.Count = 0 Then Exit Sub
        Dim TmpBT As Trinity.cBookingType

        Dim TmpCell As ExtendedComboBoxCell = grdChannels.SelectedRows.Item(0).Cells(0)
        TmpBT = TmpCell.Value
        If Not TmpCell.Value Is Nothing Then
            If Not TmpBT.Combination Is Nothing Then 'Added by JK
                For Each TmpCombo As Trinity.cCombination In ActiveCampaign.Combinations
                    For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                        If TmpCC.Bookingtype Is TmpBT Then
                            Windows.Forms.MessageBox.Show("You can not remove a channel that is a part of a Combination.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                    Next
                Next

                Try
                    TmpBT.BookIt = False
                Catch ex As Exception

                    ' If an exception is raised, its because there are spots attached to the booking type
                    If MessageBox.Show("There are spots attached to this booking type. Removing the booking type will also remove any spots attahed to it. Do you want to continue", "T R I N I T Y", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.Yes Then
                        ' Remove them and try again
                        TmpBT.RemoveAllSpots()
                        TmpBT.BookIt = False
                        UpdateChannelGrid()
                        UpdateChannelInfoGrid()
                    End If
                End Try
            Else
                TmpBT.BookIt = False
                grdChannels.Rows.Remove(grdChannels.SelectedRows.Item(0))
            End If
        Else
            grdChannels.Rows.Remove(grdChannels.SelectedRows.Item(0))
        End If

        UpdateChannelInfoGrid()
    End Sub

    Private Sub grdFilms_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdFilms.CellContentClick

    End Sub

    Private Sub grdFilms_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdFilms.CellEnter
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType

        If e.RowIndex < 0 Then
            grpFilm.Visible = False
            Exit Sub
        End If
        If grdFilms.SelectedRows.Count = 0 Then Exit Sub
        txtFilmName.Text = ActiveCampaign.Channels(1).BookingTypes(1).Weeks(1).Films(grdFilms.Rows(e.RowIndex).Cells(0).Value).Name
        txtFilmName.Tag = txtFilmName.Text
        txtFilmDescription.Text = ActiveCampaign.Channels(1).BookingTypes(1).Weeks(1).Films(grdFilms.Rows(e.RowIndex).Cells(0).Value).Description
        txtFilmLength.Text = ActiveCampaign.Channels(1).BookingTypes(1).Weeks(1).Films(grdFilms.Rows(e.RowIndex).Cells(0).Value).FilmLength
        txtFilmLength.Tag = txtFilmLength.Text
        grdFilmDetails.Rows.Clear()
        For Each TmpChan In ActiveCampaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                If TmpBT.BookIt Then
                    grdFilmDetails.Rows.Add()
                    grdFilmDetails.Rows(grdFilmDetails.Rows.Count - 1).HeaderCell.Value = TmpChan.Shortname & " " & TmpBT.Shortname
                    grdFilmDetails.Rows(grdFilmDetails.Rows.Count - 1).Tag = TmpBT
                    'grdFilmDetails.Rows(grdFilmDetails.Rows.Count - 1).Cells(0).Value = TmpBT.Weeks(1).Films(txtFilmName.Text).Filmcode
                    'grdFilmDetails.Rows(grdFilmDetails.Rows.Count - 1).Cells(1).Value = TmpBT.Weeks(1).Films(txtFilmName.Text).Index
                End If
            Next
        Next
        grpFilm.Visible = True
    End Sub

    Private Sub txtFilmName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilmName.LostFocus, txtFilmName.Leave
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpWeek As Trinity.cWeek
        Dim Film As String

        If grdFilms.SelectedRows.Count = 0 Then Exit Sub
        Film = grdFilms.SelectedRows.Item(0).Cells(0).Value

        Try
            For Each TmpChan In ActiveCampaign.Channels
                For Each TmpBT In TmpChan.BookingTypes
                    For Each TmpWeek In TmpBT.Weeks
                        'Hannes added
                        If Not TmpWeek.Films(Film) Is Nothing Then TmpWeek.Films(Film).Name = txtFilmName.Text
                    Next
                Next
            Next
            grdFilms.SelectedRows.Item(0).Cells(0).Value = txtFilmName.Text
        Catch ex As Exception
            If ex.TargetSite.Name = "Raise" Then
                System.Windows.Forms.MessageBox.Show("Two films can not share the same name.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtFilmName.Text = Film
                For Each TmpChan In ActiveCampaign.Channels
                    For Each TmpBT In TmpChan.BookingTypes
                        For Each TmpWeek In TmpBT.Weeks
                            TmpWeek.Films(Film).Name = txtFilmName.Text
                        Next
                    Next
                Next
                grdFilms.SelectedRows.Item(0).Cells(0).Value = txtFilmName.Text
            Else
                Throw ex
            End If
        End Try
    End Sub

    Private Sub txtFilmDescription_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFilmDescription.TextChanged
        Saved = False
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpWeek As Trinity.cWeek
        Dim Film As String

        If grdFilms.SelectedRows.Count = 0 Then Exit Sub
        Film = grdFilms.SelectedRows.Item(0).Cells(0).Value

        For Each TmpChan In ActiveCampaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                For Each TmpWeek In TmpBT.Weeks
                    If Not TmpWeek.Films(Film) Is Nothing Then TmpWeek.Films(Film).Description = txtFilmDescription.Text
                Next
            Next
        Next
        grdFilms.SelectedRows.Item(0).Cells(1).Value = txtFilmDescription.Text

    End Sub

    Private Sub txtFilmLength_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFilmLength.Leave

        '' First make sure that a change ahs actually taken place, if not, dont do anything
        'If txtFilmLength.Tag = txtFilmLength.Text Then
        '    Exit Sub
        'End If

        '' If Not txtFilmLength.Tag = txtFilmLength.Text Then
        'Dim TmpChan As Trinity.cChannel
        'Dim TmpBT As Trinity.cBookingType
        'Dim TmpWeek As Trinity.cWeek
        'Dim i As Integer
        'Dim Chan As String
        'Dim BT As String
        'Dim Film As String

        ' ''check if there are specifics spots and films, if so we give a warning
        'If ActiveCampaign.BookedSpots.Count > 0 Then
        '    For Each row As Windows.Forms.DataGridViewRow In grdFilmDetails.Rows
        '        If DirectCast(row.Tag, Trinity.cBookingType).IsSpecific Then
        '            If MsgBox("Changing the film length will affect prices on booked spots. Continue?", MsgBoxStyle.OkCancel, "Warning!") = MsgBoxResult.Cancel Then
        '                txtFilmLength.Text = txtFilmLength.Tag
        '                Exit Sub
        '            End If
        '            Exit For
        '        End If
        '    Next
        'End If


        'txtFilmLength.Tag = txtFilmLength.Text

        'If grdFilms.SelectedRows.Count = 0 Then Exit Sub
        'Film = grdFilms.SelectedRows.Item(0).Cells(0).Value

        'For Each TmpChan In ActiveCampaign.Channels
        '    For Each TmpBT In TmpChan.BookingTypes
        '        For Each TmpWeek In TmpBT.Weeks
        '            Try
        '                TmpWeek.Films(Film).FilmLength = Val(txtFilmLength.Text)
        '            Catch

        '            End Try
        '        Next
        '    Next
        'Next
        'grdFilms.SelectedRows.Item(0).Cells(3).Value = txtFilmLength.Text

        'If chkFilmAutoIndex.Checked AndAlso Val(txtFilmLength.Text) > 0 Then

        '    'change the index on the spots
        '    For Each spot As Trinity.cBookedSpot In ActiveCampaign.BookedSpots
        '        If spot.Film.Name = txtFilmName.Text Then
        '            spot.setIndexOnFilm(ActiveCampaign.Channels(1).BookingTypes(1).FilmIndex(txtFilmLength.Text))
        '        End If
        '    Next

        '    For i = 0 To grdFilmDetails.Rows.Count - 1
        '        Chan = DirectCast(grdFilmDetails.Rows(i).Tag, Trinity.cBookingType).ParentChannel.ChannelName
        '        BT = DirectCast(grdFilmDetails.Rows(i).Tag, Trinity.cBookingType).Name
        '        For Each TmpWeek In ActiveCampaign.Channels(Chan).BookingTypes(BT).Weeks
        '            If ActiveCampaign.Contract IsNot Nothing AndAlso ActiveCampaign.Contract.Channels(Chan).BookingTypes(ActiveCampaign.Contract.Channels(Chan).ActiveContractLevel)(BT) IsNot Nothing Then
        '                If ActiveCampaign.Contract.Channels(Chan).BookingTypes(ActiveCampaign.Contract.Channels(Chan).ActiveContractLevel)(BT).FilmIndex(txtFilmLength.Text) <> 0 Then
        '                    TmpWeek.Films(txtFilmName.Text).Index = ActiveCampaign.Contract.Channels(Chan).BookingTypes(ActiveCampaign.Contract.Channels(Chan).ActiveContractLevel)(BT).FilmIndex(txtFilmLength.Text)
        '                    TmpWeek.Films(txtFilmName.Text).GrossIndex = ActiveCampaign.Contract.Channels(Chan).BookingTypes(ActiveCampaign.Contract.Channels(Chan).ActiveContractLevel)(BT).FilmIndex(txtFilmLength.Text)
        '                End If
        '            Else
        '                If ActiveCampaign.Channels(Chan).BookingTypes(BT).FilmIndex(txtFilmLength.Text) <> 0 Then
        '                    TmpWeek.Films(txtFilmName.Text).Index = ActiveCampaign.Channels(Chan).BookingTypes(BT).FilmIndex(txtFilmLength.Text)
        '                    TmpWeek.Films(txtFilmName.Text).GrossIndex = ActiveCampaign.Channels(Chan).BookingTypes(BT).FilmIndex(txtFilmLength.Text)
        '                End If
        '            End If
        '        Next
        '    Next
        'End If
        grdFilmDetails.Invalidate()
        '  End If
    End Sub

    Private Sub txtFilmLength_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFilmLength.KeyUp

        tmrKeyPressTimer.Enabled = False
        tmrKeyPressTimer.Enabled = True


    End Sub

    Private Sub grdFilmDetails_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdFilmDetails.CellEndEdit
        Saved = False

        If chkAutoFilmCode.Checked Then
            If e.ColumnIndex = 0 Then
                For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels

                    For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                        If TmpBT.Weeks(1).Films(txtFilmName.Text) Is Nothing Then
                            For Each w As Trinity.cWeek In TmpBT.Weeks
                                With w.Films.Add(txtFilmName.Text)
                                    .Filmcode = ""
                                End With
                            Next
                        End If
                        If TmpBT.BookIt AndAlso TmpBT.Weeks(1).Films(txtFilmName.Text).Filmcode = "" AndAlso Not TmpBT.ToString = grdFilmDetails.Rows(e.RowIndex).HeaderCell.Value Then
                            For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                                'If TmpWeek.Films(txtFilmName.Text).Filmcode = "" OrElse TmpChan.Shortname & " " & TmpBT.Shortname = grdFilmDetails.Rows(e.RowIndex).HeaderCell.Value Then
                                TmpWeek.Films(txtFilmName.Text).Filmcode = grdFilmDetails.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                                'End If
                            Next
                        End If


                        'gamla copy koden
                        'If TmpBT.BookIt AndAlso TmpBT.ToString = grdFilmDetails.Rows(e.RowIndex).HeaderCell.Value Then
                        '    For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                        '        If TmpWeek.Films(txtFilmName.Text).Filmcode = "" OrElse TmpChan.Shortname & " " & TmpBT.Shortname = grdFilmDetails.Rows(e.RowIndex).HeaderCell.Value Then
                        '            TmpWeek.Films(txtFilmName.Text).Filmcode = grdFilmDetails.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                        '        End If
                        '    Next
                        'End If
                    Next
                Next
            End If
        Else
            If e.ColumnIndex = 0 Then

                Dim TmpBT As Trinity.cBookingType = DirectCast(grdFilmDetails.Rows(e.RowIndex).Tag, Trinity.cBookingType)
                If DirectCast(grdFilmDetails.Rows(e.RowIndex).Tag, Trinity.cBookingType).Weeks(1).Films(txtFilmName.Text) Is Nothing Then
                    For Each w As Trinity.cWeek In TmpBT.Weeks
                        With w.Films.Add(txtFilmName.Text)
                            .Filmcode = ""
                        End With
                    Next
                End If
                If TmpBT.BookIt AndAlso TmpBT.Weeks(1).Films(txtFilmName.Text).Filmcode = "" AndAlso Not TmpBT.ToString = grdFilmDetails.Rows(e.RowIndex).HeaderCell.Value Then
                    For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                        'If TmpWeek.Films(txtFilmName.Text).Filmcode = "" OrElse TmpChan.Shortname & " " & TmpBT.Shortname = grdFilmDetails.Rows(e.RowIndex).HeaderCell.Value Then
                        TmpWeek.Films(txtFilmName.Text).Filmcode = grdFilmDetails.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                        'End If
                    Next
                End If


                'gamla copy koden
                'If TmpBT.BookIt AndAlso TmpBT.ToString = grdFilmDetails.Rows(e.RowIndex).HeaderCell.Value Then
                '    For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                '        If TmpWeek.Films(txtFilmName.Text).Filmcode = "" OrElse TmpChan.Shortname & " " & TmpBT.Shortname = grdFilmDetails.Rows(e.RowIndex).HeaderCell.Value Then
                '            TmpWeek.Films(txtFilmName.Text).Filmcode = grdFilmDetails.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                '        End If
                '    Next
                'End If

            End If
        End If
        grdFilmDetails.Invalidate()
    End Sub

    Private Sub cmdRemoveFilm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveFilm.Click
        Saved = False
        Dim Film As String
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpWeek As Trinity.cWeek

        If grdFilms.SelectedRows.Count = 0 Then Exit Sub
        Film = grdFilms.SelectedRows.Item(0).Cells(0).Value

        'Added If bSpot.Film is not nothing /JK
        Dim BookedSpots As List(Of Trinity.cBookedSpot) = (From bSpot As Trinity.cBookedSpot In Campaign.BookedSpots Select bSpot Where bSpot.Film IsNot Nothing AndAlso bSpot.Film.Name = Film).ToList
        If BookedSpots.Count > 0 Then
            Dim messageText As String = "You have booked the following spots using the film you are about to remove. Removing this film will remove your booked spots. Continue?" & _
            vbNewLine & "Note that you can avoid this message if you instead of removing the spot simply change its details below."
            For Each bSpot As Trinity.cBookedSpot In BookedSpots
                messageText &= bSpot.AirDate.ToShortDateString & " " & bSpot.Bookingtype.ToString & " " & bSpot.ProgAfter & vbNewLine
            Next
            If MessageBox.Show(messageText, "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            For Each bSpot As Trinity.cBookedSpot In BookedSpots
                Campaign.BookedSpots.Remove(bSpot.ID)
            Next
        End If

        For Each TmpChan In ActiveCampaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                For Each TmpWeek In TmpBT.Weeks
                    TmpWeek.Films.Remove(Film)
                Next
            Next
        Next
        grpFilm.Visible = False
        UpdateFilmGrid()
    End Sub

    Private Sub grdAddedValues_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdAddedValues.CellEndEdit
        Saved = False
        If e.ColumnIndex > 0 And e.ColumnIndex < 3 Then
            If grdAddedValues.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 0 Then
                MsgBox("Indexes amy not be 0. The index will be automatically set to 1.", MsgBoxStyle.Information, "Bad index")
                grdAddedValues.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 1
            End If
        End If

    End Sub

    Private Sub grdAddedValues_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdAddedValues.CellValueChanged
        Dim ID As String
        Dim Chan As String
        Dim BT As String

        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub


        Dim combo As Trinity.cCombination

        Dim comboID As String = Nothing

        Chan = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        BT = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).Name
        ID = grdAddedValues.Rows(e.RowIndex).Tag

        For Each combo In ActiveCampaign.Combinations
            If combo.ShowAsOne Then
                For Each cc As Trinity.cCombinationChannel In combo.Relations
                    If cc.Bookingtype.Shortname = BT AndAlso cc.Bookingtype.ParentChannel.ChannelName = Chan Then
                        comboID = combo.ID
                    End If
                Next
            End If
        Next


        If comboID Is Nothing Then
            If e.ColumnIndex = 0 Then
                ActiveCampaign.Channels(Chan).BookingTypes(BT).AddedValues(ID).Name = grdAddedValues.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            ElseIf e.ColumnIndex = 1 Then
                Try
                    ActiveCampaign.Channels(Chan).BookingTypes(BT).AddedValues(ID).IndexGross = grdAddedValues.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                Catch ex As Exception

                End Try

            ElseIf e.ColumnIndex = 2 Then
                Try
                    ActiveCampaign.Channels(Chan).BookingTypes(BT).AddedValues(ID).IndexNet = grdAddedValues.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                Catch ex As Exception

                End Try

            ElseIf e.ColumnIndex = 3 Then
                Select Case grdAddedValues.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                    Case "Both"
                        ActiveCampaign.Channels(Chan).BookingTypes(BT).AddedValues(ID).ShowIn = Trinity.cAddedValue.ShowInEnum.siBoth
                    Case "Allocate"
                        ActiveCampaign.Channels(Chan).BookingTypes(BT).AddedValues(ID).ShowIn = Trinity.cAddedValue.ShowInEnum.siAllocate
                    Case "Booking"
                        ActiveCampaign.Channels(Chan).BookingTypes(BT).AddedValues(ID).ShowIn = Trinity.cAddedValue.ShowInEnum.siBooking
                End Select
            ElseIf e.ColumnIndex = 4 Then
                ActiveCampaign.Channels(Chan).BookingTypes(BT).AddedValues(ID).UseThis = grdAddedValues.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            End If
        Else
            combo = ActiveCampaign.Combinations(comboID)
            For Each cc As Trinity.cCombinationChannel In combo.Relations
                Try
                    If e.ColumnIndex = 0 Then
                        cc.Bookingtype.AddedValues(ID).Name = grdAddedValues.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                    ElseIf e.ColumnIndex = 1 Then
                        cc.Bookingtype.AddedValues(ID).IndexGross = grdAddedValues.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                    ElseIf e.ColumnIndex = 2 Then
                        cc.Bookingtype.AddedValues(ID).IndexNet = grdAddedValues.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                    ElseIf e.ColumnIndex = 3 Then
                        Select Case grdAddedValues.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                            Case "Both"
                                cc.Bookingtype.AddedValues(ID).ShowIn = Trinity.cAddedValue.ShowInEnum.siBoth
                            Case "Allocate"
                                cc.Bookingtype.AddedValues(ID).ShowIn = Trinity.cAddedValue.ShowInEnum.siAllocate
                            Case "Booking"
                                cc.Bookingtype.AddedValues(ID).ShowIn = Trinity.cAddedValue.ShowInEnum.siBooking
                        End Select
                    ElseIf e.ColumnIndex = 4 Then
                        cc.Bookingtype.AddedValues(ID).UseThis = grdAddedValues.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                    End If
                Catch ex As Exception
                    If Not cc.Bookingtype.AddedValues.Contains(ID) Then
                        With cc.Bookingtype.AddedValues.Add(ActiveCampaign.Channels(Chan).BookingTypes(BT).AddedValues(ID).Name)
                            .ID = ActiveCampaign.Channels(Chan).BookingTypes(BT).AddedValues(ID).ID
                            .IndexGross = ActiveCampaign.Channels(Chan).BookingTypes(BT).AddedValues(ID).IndexGross
                            .IndexNet = ActiveCampaign.Channels(Chan).BookingTypes(BT).AddedValues(ID).IndexNet
                            .ShowIn = ActiveCampaign.Channels(Chan).BookingTypes(BT).AddedValues(ID).ShowIn
                            .UseThis = ActiveCampaign.Channels(Chan).BookingTypes(BT).AddedValues(ID).UseThis
                        End With
                    Else
                        Throw ex
                    End If
                End Try
            Next
        End If
    End Sub

    Private Sub cmdAddAV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddAV.Click
        Saved = False
        Dim ID As String = ""
        Dim TmpID As String = ""
        Dim Chan As String
        Dim BT As String
        Dim combo As Trinity.cCombination

        Dim comboID As String = Nothing

        Chan = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        BT = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).Name

        For Each combo In ActiveCampaign.Combinations
            If combo.ShowAsOne Then
                For Each cc As Trinity.cCombinationChannel In combo.Relations
                    If cc.Bookingtype.Shortname = BT AndAlso cc.Bookingtype.ParentChannel.ChannelName = Chan Then
                        comboID = combo.ID
                        Exit For
                    End If
                Next
            End If
        Next

        If comboID Is Nothing Then
            ID = ActiveCampaign.Channels(Chan).BookingTypes(BT).AddedValues.Add("").ID.ToString

            ActiveCampaign.Channels(Chan).BookingTypes(BT).AddedValues(ID).IndexNet = 100
            ActiveCampaign.Channels(Chan).BookingTypes(BT).AddedValues(ID).IndexGross = 100
        Else
            combo = ActiveCampaign.Combinations(comboID)
            For Each cc As Trinity.cCombinationChannel In combo.Relations
                If ID Is "" Then
                    ID = cc.Bookingtype.AddedValues.Add("").ID.ToString
                Else
                    cc.Bookingtype.AddedValues.Add("", ID).ID.ToString()
                End If

                cc.Bookingtype.AddedValues(ID).IndexNet = 100
                cc.Bookingtype.AddedValues(ID).IndexGross = 100
            Next
        End If

        cmbIndexChannel_SelectedIndexChanged(New Object, New EventArgs)

    End Sub

    Private Sub grdIndexes_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdIndexes.CellEndEdit

        If e.ColumnIndex = 4 Then
            If grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 0 Then
                MsgBox("Indexes may not be 0. The index will be automatically set to 1.", MsgBoxStyle.Information, "Bad index")
                grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 1
            End If
        End If
    End Sub

    Private Sub grdIndexes_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdIndexes.CellFormatting
        If grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly Then
            e.CellStyle.ForeColor = Color.LightGray
        End If
    End Sub

    Private Sub grdIndexes_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles grdIndexes.CellValidating

    End Sub

    Private Sub grdIndexes_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdIndexes.CellValueChanged
        Dim Chan As String
        Dim BT As String
        Dim ID As String
        Dim combo As Trinity.cCombination
        Dim comboID As String = Nothing

        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        Chan = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        BT = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).Name
        If grdIndexes.Rows(e.RowIndex).Tag.GetType Is GetType(Trinity.cIndex) Then
            If e.ColumnIndex = 5 Then
                DirectCast(grdIndexes.Rows(e.RowIndex).Tag, Trinity.cIndex).UseThis = grdIndexes.Rows(e.RowIndex).Cells(5).Value
            End If
            Exit Sub
        Else
            ID = grdIndexes.Rows(e.RowIndex).Tag
        End If

        'Remove ' to activate Combo-sync

        For Each combo In ActiveCampaign.Combinations
            If combo.IncludesBookingtype(ActiveCampaign.Channels(Chan).BookingTypes(BT)) Then
                comboID = combo.ID
            End If
        Next

        If comboID Is Nothing Then
            If e.ColumnIndex = 0 Then
                ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes(ID).Name = grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            ElseIf e.ColumnIndex = 1 Then
                If grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Net CPP" Then
                    ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes(ID).IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP
                ElseIf grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Gross CPP" Then
                    ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes(ID).IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP
                ElseIf grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "TRP" Then
                    ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes(ID).IndexOn = Trinity.cIndex.IndexOnEnum.eTRP
                ElseIf grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Min. CPP" Then
                    ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes(ID).IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPPAfterMaxDiscount
                End If
            ElseIf e.ColumnIndex = 2 Then
                ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes(ID).FromDate = grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            ElseIf e.ColumnIndex = 3 Then
                ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes(ID).ToDate = grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            ElseIf e.ColumnIndex = 4 Then
                ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes(ID).Index = grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            ElseIf e.ColumnIndex = 5 Then
                ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes(ID).UseThis = grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            End If
            ActiveCampaign.Channels(Chan).BookingTypes(BT).InvalidateCPPs()
            ActiveCampaign.Channels(Chan).BookingTypes(BT).InvalidateTotalTRP()
            ActiveCampaign.Channels(Chan).BookingTypes(BT).InvalidateActualNetValue()
        Else
            combo = ActiveCampaign.Combinations(comboID)
            For Each cc As Trinity.cCombinationChannel In combo.Relations
                If cc.Bookingtype.Indexes.Exists(ID) Then
                    If e.ColumnIndex = 0 Then
                        cc.Bookingtype.Indexes(ID).Name = grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                    ElseIf e.ColumnIndex = 1 Then
                        If grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Net CPP" Then
                            cc.Bookingtype.Indexes(ID).IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP
                        ElseIf grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Gross CPP" Then
                            cc.Bookingtype.Indexes(ID).IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP
                        ElseIf grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "TRP" Then
                            cc.Bookingtype.Indexes(ID).IndexOn = Trinity.cIndex.IndexOnEnum.eTRP
                        ElseIf grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "Min. CPP" Then
                            cc.Bookingtype.Indexes(ID).IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPPAfterMaxDiscount
                        End If
                    ElseIf e.ColumnIndex = 2 Then
                        cc.Bookingtype.Indexes(ID).FromDate = grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                    ElseIf e.ColumnIndex = 3 Then
                        cc.Bookingtype.Indexes(ID).ToDate = grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                    ElseIf e.ColumnIndex = 4 Then
                        cc.Bookingtype.Indexes(ID).Index = grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                    ElseIf e.ColumnIndex = 5 Then
                        cc.Bookingtype.Indexes(ID).UseThis = grdIndexes.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                    End If
                End If
                cc.Bookingtype.InvalidateCPPs()
                cc.Bookingtype.InvalidateTotalTRP()
                cc.Bookingtype.InvalidateActualNetValue()
            Next
        End If
    End Sub

    Private Sub cmdAddIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddIndex.Click
        Saved = False
        Dim Chan As String
        Dim BT As String
        Dim ID As String = ""
        Dim combo As Trinity.cCombination

        Dim comboID As String = Nothing

        Chan = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        BT = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).Name

        For Each combo In ActiveCampaign.Combinations
            If combo.ShowAsOne Then
                For Each cc As Trinity.cCombinationChannel In combo.Relations
                    If cc.Bookingtype.Shortname = BT AndAlso cc.Bookingtype.ParentChannel.ChannelName = Chan Then
                        comboID = combo.ID

                        If MsgBox("This channel has a combination. Changes will apply on all channels in the Combination.", MsgBoxStyle.OkCancel, "") = MsgBoxResult.Cancel Then
                            Exit Sub
                        Else
                            Exit For
                        End If
                    End If
                Next
            End If
        Next

        If comboID Is Nothing Then
            ID = ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes.Add("").ID

            ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes(ID).FromDate = Date.FromOADate(ActiveCampaign.StartDate)
            ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes(ID).ToDate = Date.FromOADate(ActiveCampaign.EndDate)

        Else
            combo = ActiveCampaign.Combinations(comboID)
            For Each cc As Trinity.cCombinationChannel In combo.Relations
                If ID Is "" Then
                    ID = cc.Bookingtype.Indexes.Add("").ID.ToString
                Else
                    cc.Bookingtype.Indexes.Add("", ID)
                End If

                cc.Bookingtype.Indexes(ID).FromDate = Date.FromOADate(ActiveCampaign.StartDate)
                cc.Bookingtype.Indexes(ID).ToDate = Date.FromOADate(ActiveCampaign.EndDate)
            Next
        End If

        cmbIndexChannel_SelectedIndexChanged(New Object, New EventArgs)
        grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = ID
    End Sub

    Private Sub cmdRemoveIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveIndex.Click
        Saved = False
        Dim Chan As String
        Dim BT As String


        Chan = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        BT = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).Name

        For Each Row As DataGridViewRow In grdIndexes.SelectedRows
            If Row.Tag.GetType Is GetType(Trinity.cIndex) Then
                Windows.Forms.MessageBox.Show("Choose Settings->Edit pricelist to delete indexes from the pricelist.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes.Remove(Row.Tag)
            End If
        Next
        'ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes.Remove(grdIndexes.SelectedRows(0).Tag)

        cmbIndexChannel_SelectedIndexChanged(New Object, New EventArgs)

    End Sub

    Private Sub cmbIndexChannel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbIndexChannel.SelectedIndexChanged
        Dim i As Integer
        Dim IndexNames() As String = {"Net CPP", "Gross CPP", "TRP", "Min. CPP"}
        Dim ShowInNames() As String = {"Both", "Allocate", "Booking"}
        Dim TmpAV As Trinity.cAddedValue
        Dim TmpBT As Trinity.cBookingType = cmbIndexChannel.SelectedItem

        grdIndexes.Rows.Clear()
        colUse.Visible = (ActiveCampaign.Contract IsNot Nothing)
        For Each _index As Trinity.cIndex In TmpBT.BuyingTarget.Indexes
            If _index.FromDate.ToOADate <= ActiveCampaign.EndDate AndAlso _index.ToDate.ToOADate >= ActiveCampaign.StartDate Then
                grdIndexes.Rows.Add()
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = _index
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(0).Value = _index.Name
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(1).Value = IndexNames(_index.IndexOn)
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(2).Value = Format(_index.FromDate, "Short Date")
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(3).Value = Format(_index.ToDate, "Short Date")
                If _index.Enhancements.Count = 0 Then
                    grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(4).Value = _index.Index
                    grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(4).Style.ForeColor = Color.Black
                Else
                    grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(4).Value = Format(_index.Enhancements.FactoredIndex, "N1")
                    grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(4).Style.ForeColor = Color.Blue
                End If
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(5).Value = _index.UseThis
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(0).ReadOnly = True
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(1).ReadOnly = True
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(2).ReadOnly = True
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(3).ReadOnly = True
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(4).ReadOnly = True
                colUse.Visible = True
            End If
        Next

        For i = 1 To TmpBT.Indexes.Count
            If Not TmpBT.Indexes(i).SystemGenerated Then
                grdIndexes.Rows.Add()
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = TmpBT.Indexes(i).ID
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(0).Value = TmpBT.Indexes(i).Name
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(1).Value = IndexNames(TmpBT.Indexes(i).IndexOn)
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(2).Value = Format(TmpBT.Indexes(i).FromDate, "Short Date")
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(3).Value = Format(TmpBT.Indexes(i).ToDate, "Short Date")
                If TmpBT.Indexes(i).Enhancements.Count = 0 Then
                    grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(4).Value = TmpBT.Indexes(i).Index
                    grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(4).Style.ForeColor = Color.Black
                Else
                    grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(4).Value = Format(TmpBT.Indexes(i).Enhancements.FactoredIndex, "N1")
                    grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(4).Style.ForeColor = Color.Blue
                End If
                grdIndexes.Rows(grdIndexes.Rows.Count - 1).Cells(5).Value = TmpBT.Indexes(i).UseThis
            Else

            End If
        Next

        grdAddedValues.Rows.Clear()
        For Each TmpAV In TmpBT.AddedValues
            grdAddedValues.Rows.Add()
            grdAddedValues.Rows(grdAddedValues.Rows.Count - 1).Tag = TmpAV.ID
            grdAddedValues.Rows(grdAddedValues.Rows.Count - 1).Cells(0).Value = TmpAV.Name
            grdAddedValues.Rows(grdAddedValues.Rows.Count - 1).Cells(1).Value = TmpAV.IndexGross
            grdAddedValues.Rows(grdAddedValues.Rows.Count - 1).Cells(2).Value = TmpAV.IndexNet
            grdAddedValues.Rows(grdAddedValues.Rows.Count - 1).Cells(3).Value = ShowInNames(TmpAV.ShowIn)
            grdAddedValues.Rows(grdAddedValues.Rows.Count - 1).Cells(4).Value = TmpAV.UseThis
        Next
        If ActiveCampaign.Area <> "DK" Then
            grdAddedValues.Columns(3).Visible = False
        End If
        If ActiveCampaign.Contract Is Nothing Then
            grdAddedValues.Columns(4).Visible = False
        End If
        lblSeasonal.Visible = Not TmpBT.IsSeasonal
    End Sub

    Private Sub cmdGeneralNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGeneralNext.Click
        'activates the channel tab
        tabSetup.SelectTab(tpChannels)
    End Sub

    Private Sub cmdChannelsNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChannelsNext.Click
        'activates the Film tab
        tabSetup.SelectTab(tpCombinations)
    End Sub

    Private Sub cmdApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApply.Click
        'all setup information is set and we can enable all funcion buttons
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType

        frmMain.cmdAllocate.Enabled = True
        frmMain.cmdMonitor.Enabled = True
        frmMain.cmdSpots.Enabled = True
        frmMain.cmdLab.Enabled = True
        frmMain.cmdPivot.Enabled = True
        frmMain.cmdInfo.Enabled = True
        frmMain.cmdBudget.Enabled = True
        frmMain.cmdNotes.Enabled = True
        frmMain.cmdDelivery.Enabled = True
        For Each TmpChan In ActiveCampaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                If (TmpBT.IsSpecific OrElse TmpBT.IsPremium) AndAlso TmpBT.BookIt Then
                    frmMain.cmdBooking.Enabled = True
                End If
            Next
        Next
        If Not ActiveCampaign.IsStripped AndAlso Windows.Forms.MessageBox.Show("To save memory and increase perfomance, Trinity can remove" & vbCrLf &
                                         "channels and bookingtypes that are not used on this campaign." & vbCrLf &
                                         "Channels can be re-added at any time, either by adding a" & vbCrLf &
                                         "row in the channels section of Setup or by choosing 'Load" & vbCrLf &
                                         "Default channels' in Settings->Channel Options." & vbCrLf &
                                         vbCrLf &
                                         "Do you want to remove unused channels now?", "T R I N I T Y", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Campaign.DeleteUnusedChannels()
        End If
        'closes the setup window
        Me.Dispose()
    End Sub

    Private Sub cmdFilmsNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFilmsNext.Click
        'activates the index tab
        tabSetup.SelectTab(tpIndex)
    End Sub


    Private Sub cmdRemoveAV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveAV.Click
        Saved = False
        Dim Chan As String
        Dim BT As String
        Dim combo As Trinity.cCombination

        Dim comboID As String = Nothing

        Chan = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        BT = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).Name

        For Each AVRow As DataGridViewRow In grdAddedValues.SelectedRows

            Dim AV As String = AVRow.Tag

            'Dim AV As String = grdAddedValues.SelectedRows(0).Tag
            Dim AVtmp As Trinity.cAddedValue
            Dim remove As Boolean = True

            For Each tmpspot As Trinity.cBookedSpot In ActiveCampaign.BookedSpots
                If tmpspot.Channel.ChannelName = Chan Then
                    Try
                        AVtmp = tmpspot.AddedValues(AV)
                    Catch
                        AVtmp = Nothing
                    End Try
                    If Not AVtmp Is Nothing Then
                        If remove Then
                            If MsgBox("The added value about to be deleted is currently in use. Do you wish to remove the added values where it is used?", MsgBoxStyle.YesNo, "Remove added values on spots?") = MsgBoxResult.Yes Then
                                remove = False
                                tmpspot.AddedValues.Remove(AV)
                            End If
                        Else
                            tmpspot.AddedValues.Remove(AV)
                        End If
                    End If
                End If
            Next

            Dim AVID As String
            For Each tmpspot As Trinity.cPlannedSpot In ActiveCampaign.PlannedSpots
                If tmpspot.Channel.ChannelName = Chan Then
                    Try
                        AVID = tmpspot.AddedValue.ID
                    Catch ex As Exception
                        AVID = ""
                    End Try
                    If AV = AVID Then
                        If remove Then
                            If MsgBox("The added value about to be deleted is currently in use. Do you wish to remove the added values where it is used?", MsgBoxStyle.YesNo, "Remove added values on spots?") = MsgBoxResult.Yes Then
                                remove = False
                                tmpspot.AddedValue = Nothing
                            End If
                        Else
                            tmpspot.AddedValue = Nothing
                        End If
                    End If
                End If
            Next

            For Each combo In ActiveCampaign.Combinations
                If combo.ShowAsOne Then
                    For Each cc As Trinity.cCombinationChannel In combo.Relations
                        If cc.Bookingtype.Shortname = BT AndAlso cc.Bookingtype.ParentChannel.ChannelName = Chan Then
                            comboID = combo.ID
                            Exit For
                        End If
                    Next
                End If
            Next

            If comboID Is Nothing Then
                ActiveCampaign.Channels(Chan).BookingTypes(BT).AddedValues.Remove(AVRow.Tag)
            Else
                combo = ActiveCampaign.Combinations(comboID)
                For Each cc As Trinity.cCombinationChannel In combo.Relations
                    cc.Bookingtype.AddedValues.Remove(AVRow.Tag)
                Next
            End If
        Next
        cmbIndexChannel_SelectedIndexChanged(New Object, New EventArgs)

    End Sub

    Private Sub grdCosts_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCosts.CellEndEdit
        Saved = False
    End Sub

    Private Sub grdCosts_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdCosts.CellValueNeeded
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
                TmpCell.Items.Add("Media Net") 'Changed from Media net
                TmpCell.Items.Add("Net")    'Changed from Net
                TmpCell.Items.Add("Net Net")
                TmpCell.Items.Add("Ratecard")
            ElseIf grdCosts.Rows(e.RowIndex).Cells(1).Value = "Per Unit" AndAlso (TmpCell.Items.Count = 0 OrElse TmpCell.Items(0) <> "Spots") Then
                TmpCell.Items.Clear()
                TmpCell.Items.Add("Spots")
                TmpCell.Items.Add("Buy TRP")
                TmpCell.Items.Add("Main TRP")
                TmpCell.Items.Add("Weeks")
            ElseIf grdCosts.Rows(e.RowIndex).Cells(1).Value = "On Discount" AndAlso (TmpCell.Items.Count = 0 OrElse TmpCell.Items(0) <> "All") Then
                TmpCell.Items.Clear()
                TmpCell.Items.Add("All")
                For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels
                    TmpCell.Items.Add(TmpChan.Shortname)
                Next
            End If
            e.Value = TmpCost.CostOnText
        ElseIf e.ColumnIndex = 4 Then
            If TrinitySettings.MarathonEnabled Then
                e.Value = TmpCost.MarathonID
            End If
        End If

    End Sub


    Private Sub grdCosts_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdCosts.CellValuePushed
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

            Dim numberCandidate As Single
            'Try
            '    numberCandidate = CType(e.Value, Single)
            'Catch ex As Exception

            '    Exit Sub
            'End Try

            ' Added new method to try add a proper value, or restore the old one if its not valid
            ' Has some tolerance, eg you can write % or "kr" or use a dot instead of a comma
            Dim tmpStr As String = e.Value.ToString()
            tmpStr = tmpStr.Replace("%", "").Replace(".", ",").Replace(" ", "").Replace("kr", "")

            If (Not Single.TryParse(tmpStr, numberCandidate)) Then
                Exit Sub
            End If


            If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent OrElse TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeOnDiscount Then
                TmpCost.Amount = numberCandidate / 100
            Else
                TmpCost.Amount = numberCandidate
            End If
        ElseIf e.ColumnIndex = 3 Then
            If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeFixed Then
                TmpCost.CountCostOn = 0
            ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                If e.Value = "Media Net" Then
                    If Campaign.Client = "Telia" Then
                        TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNetNet
                    Else
                        TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnMediaNet
                    End If
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
                ElseIf e.Value = "Weeks" Then
                    TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnWeeks
                End If
            ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeOnDiscount Then
                If e.Value = "All" Then
                    TmpCost.CountCostOn = Nothing
                Else
                    TmpCost.CountCostOn = ActiveCampaign.Channels(LongName(e.Value))
                End If
            End If
        ElseIf e.ColumnIndex = 4 Then
            If TrinitySettings.MarathonEnabled Then
                TmpCost.MarathonID = e.Value
            End If
        End If
    End Sub

    Private Sub cmbMainUni_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMainUni.SelectedIndexChanged
        ActiveCampaign.MainTarget.Universe = ActiveCampaign.Universes.GetKey(sender.selectedindex)
        lblMainSize.Text = Format(ActiveCampaign.MainTarget.UniSize * 1000, "##,##0")
        If ActiveCampaign.MainTarget.UniSize = 0 Then
            lblMainTarget.ForeColor = Color.Red
        Else
            lblMainTarget.ForeColor = Color.Black
        End If
    End Sub

    Private Sub cmbSecondUni_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSecondUni.SelectedIndexChanged
        ActiveCampaign.SecondaryTarget.Universe = ActiveCampaign.Universes.GetKey(sender.selectedindex)
        lblSecondSize.Text = Format(ActiveCampaign.SecondaryTarget.UniSize * 1000, "##,##0")
        If ActiveCampaign.SecondaryTarget.UniSize = 0 Then
            lblSecondTarget.ForeColor = Color.Red
        Else
            lblSecondTarget.ForeColor = Color.Black
        End If
    End Sub

    Private Sub cmbThirdUni_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbThirdUni.SelectedIndexChanged
        ActiveCampaign.ThirdTarget.Universe = ActiveCampaign.Universes.GetKey(sender.selectedindex)
        lblThirdSize.Text = Format(ActiveCampaign.ThirdTarget.UniSize * 1000, "##,##0")
        If ActiveCampaign.ThirdTarget.UniSize = 0 Then
            lblThirdTarget.ForeColor = Color.Red
        Else
            lblThirdTarget.ForeColor = Color.Black
        End If
    End Sub

    Private Sub cmdDeleteCost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteCost.Click
        If grdCosts.SelectedRows.Count = 0 Then
            System.Windows.Forms.MessageBox.Show("No cost selected.", "TRINITY", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        For Each TmpRow As DataGridViewRow In grdCosts.SelectedRows
            ActiveCampaign.Costs.Remove(TmpRow.Tag.ID)
            grdCosts.Rows.Remove(TmpRow)
        Next
    End Sub

    Private Sub cmbBuyer_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBuyer.TextChanged
        ActiveCampaign.Buyer = cmbBuyer.Text

        If Not buyers Is Nothing Then
            Dim xmlE As Xml.XmlElement
            xmlE = buyers.SelectNodes("//buyer[@name='" & cmbBuyer.Text & "']").Item(0)
            If xmlE Is Nothing Then Exit Sub
            ActiveCampaign.BuyerEmail = xmlE.GetAttribute("email")
            ActiveCampaign.BuyerPhone = xmlE.GetAttribute("phone")
        End If

    End Sub

    Private Sub cmbPlanner_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPlanner.TextChanged
        ActiveCampaign.Planner = cmbPlanner.Text

        If Not planners Is Nothing Then
            Dim xmlE As Xml.XmlElement
            xmlE = planners.SelectNodes("//planner[@name='" & cmbPlanner.Text & "']").Item(0)
            If xmlE Is Nothing Then Exit Sub
            ActiveCampaign.PlannerEmail = xmlE.GetAttribute("email")
            ActiveCampaign.PlannerPhone = xmlE.GetAttribute("phone")
        End If
    End Sub

    Private Sub txtBudget_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBudget.TextChanged
        Saved = False
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim EmptyFilmcode As Boolean = True

        'if it exists ask if to replace
        If DBReader.FilmExist(txtFilmName.Text, ActiveCampaign.ProductID) Then
            If MessageBox.Show("There is already a film called '" & txtFilmName.Text & "' in the database." & vbCrLf & vbCrLf & "Are you sure you want to replace it?", "T R I N I T Y", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If
        End If

        Me.Cursor = Cursors.WaitCursor

        For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                If TmpBT.BookIt Then
                    If TmpBT.Weeks(1).Films(txtFilmName.Text).Filmcode <> "" Then
                        DBReader.addOrUpdateFilm(txtFilmName.Text, ActiveCampaign.ProductID, TmpBT.Weeks(1).Films(txtFilmName.Text).Filmcode, TmpChan.ChannelName, txtFilmDescription.Text, txtFilmLength.Text, TmpBT.Weeks(1).Films(txtFilmName.Text).Index)
                        EmptyFilmcode = False
                    End If
                End If
            Next
        Next
        Me.Cursor = Cursors.Default

        If EmptyFilmcode Then
            Windows.Forms.MessageBox.Show("You need to specify a filmcode in at least one channel to save it.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        'Dim rd As Odbc.OdbcDataReader
        'Dim com As New Odbc.OdbcCommand("SELECT * FROM Films WHERE Name='" & txtFilmName.Text & "' AND Product=" & ActiveCampaign.ProductID, DBConn)

        'rd = com.ExecuteReader
        'If rd.HasRows Then
        '    If MessageBox.Show("There is already a film called '" & txtFilmName.Text & "' in the database." & vbCrLf & vbCrLf & "Are you sure you want to replace it?", "T R I N I T Y", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
        '        Exit Sub
        '    End If
        'End If
        'Me.Cursor = Cursors.WaitCursor
        'rd.Close()
        'For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels
        '    For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
        '        If TmpBT.BookIt Then
        '            If TmpBT.Weeks(1).Films(txtFilmName.Text).Filmcode <> "" Then
        '                com = New Odbc.OdbcCommand("SELECT * FROM Films WHERE Name='" & txtFilmName.Text & "' AND Product=" & ActiveCampaign.ProductID & " AND Channel='" & TmpChan.ChannelName & "'", DBConn)
        '                rd = com.ExecuteReader
        '                If rd.HasRows Then
        '                    com.CommandText = "UPDATE Films SET Filmcode='" & TmpBT.Weeks(1).Films(txtFilmName.Text).Filmcode & "',Name='" & txtFilmName.Text & "',Description='" & txtFilmDescription.Text & "',Length=" & txtFilmLength.Text & ",Channel='" & TmpChan.ChannelName & "',Product=" & ActiveCampaign.ProductID & ",Index=" & TmpBT.Weeks(1).Films(txtFilmName.Text).Index.ToString.Replace(",", ".") & " WHERE Name='" & txtFilmName.Text & "' and Channel='" & TmpChan.ChannelName & "'"
        '                Else
        '                    com.CommandText = "INSERT INTO Films (Filmcode,Name,Description,Length,Channel,Product,Index) VALUES ('" & TmpBT.Weeks(1).Films(txtFilmName.Text).Filmcode & "','" & txtFilmName.Text & "','" & txtFilmDescription.Text & "'," & txtFilmLength.Text & ",'" & TmpChan.ChannelName & "'," & ActiveCampaign.ProductID & "," & TmpBT.Weeks(1).Films(txtFilmName.Text).Index.ToString.Replace(",", ".") & ")"
        '                End If
        '                rd.Close()
        '                com.ExecuteScalar()
        '            End If
        '        End If
        '    Next
        'Next
        'Me.Cursor = Cursors.Default
    End Sub

    Private Sub grdFilmDetails_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdFilmDetails.CellValueNeeded
        If e.RowIndex < 0 Then Exit Sub
        Try
            Dim TmpBT As Trinity.cBookingType = grdFilmDetails.Rows(e.RowIndex).Tag
            If e.ColumnIndex = 0 Then
                e.Value = TmpBT.Weeks(1).Films(txtFilmName.Text).Filmcode
                grdFilmDetails.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = e.Value
            ElseIf e.ColumnIndex = 1 Then
                e.Value = TmpBT.Weeks(1).Films(txtFilmName.Text).GrossIndex
            ElseIf e.ColumnIndex = 2 Then
                If TmpBT.Weeks(1).Films(txtFilmName.Text).Index = 0 Then
                    TmpBT.Weeks(1).Films(txtFilmName.Text).Index = Campaign.Channels.DefaultFilmIndex(txtFilmLength.Text)
                End If
                e.Value = TmpBT.Weeks(1).Films(txtFilmName.Text).Index
            End If
        Catch 'Try/catch is to solve a problem with Cellvalue needed, it runs before the lost focus event wich causes problem if you change film name and then activates this sub
            If String.Equals(txtFilmName.Text, ActiveCampaign.Channels(1).BookingTypes(1).Weeks(1).Films(grdFilms.SelectedRows(0).Cells(0).Value).Name) Then
                Dim s As New Object
                Dim ex As New System.EventArgs
                txtFilmName_LostFocus(s, ex)
            End If
        End Try
    End Sub


    Private Sub grdFilmDetails_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdFilmDetails.CellValuePushed
        Dim TmpWeek As Trinity.cWeek
        If e.RowIndex < 0 Then Exit Sub
        Dim TmpBT As Trinity.cBookingType = grdFilmDetails.Rows(e.RowIndex).Tag

        If e.ColumnIndex = 0 Then
            Dim bolAskIfReplace As Boolean = False

            If e.Value <> UCase(e.Value) Then
                e.Value = UCase(e.Value)
            End If

            Dim oldFilmCode As String = grdFilmDetails.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag
            Dim newFilmCode As String
            If Not e.Value Is Nothing Then
                newFilmCode = e.Value.ToString.Trim
            Else
                newFilmCode = ""
            End If

            For Each TmpWeek In TmpBT.Weeks
                If bolAskIfReplace Then
                    TmpWeek.Films(txtFilmName.Text).Filmcode = newFilmCode
                Else
                    For Each spot As Trinity.cActualSpot In ActiveCampaign.ActualSpots
                        If spot.Filmcode = oldFilmCode AndAlso spot.Bookingtype Is TmpBT Then

                            'we ask if we want to replace them
                            If MsgBox("This filmcode is in use, do you wish to change filmcode and delete the Actualspots with this filmcode?", MsgBoxStyle.YesNo, "Change Filmcode") = MsgBoxResult.Yes Then
                                'if yes we change filmcode on all the spots connected with the filmcode
                                Dim list As New List(Of String)
                                For Each tmpspot1 As Trinity.cActualSpot In ActiveCampaign.ActualSpots
                                    If tmpspot1.Filmcode = oldFilmCode Then
                                        list.Add(tmpspot1.ID)
                                    End If
                                Next
                                For j As Integer = 0 To list.Count - 1
                                    ActiveCampaign.ActualSpots.Remove(list(j))
                                Next
                                ActiveCampaign.UpdatedTo = 0
                                ActiveCampaign.GetNewActualSpots()
                                ActiveCampaign.CalculateSpots()
                                bolAskIfReplace = True
                                Exit For
                            Else
                                Exit Sub
                            End If
                        End If
                    Next
                    For Each spot As Trinity.cPlannedSpot In ActiveCampaign.PlannedSpots
                        If spot.Filmcode = oldFilmCode AndAlso spot.Bookingtype Is TmpBT Then
                            spot.Filmcode = newFilmCode
                        End If
                    Next
                    For Each spot As Trinity.cBookedSpot In ActiveCampaign.BookedSpots
                        If spot.Filmcode = oldFilmCode AndAlso spot.Bookingtype Is TmpBT Then
                            spot.Filmcode = newFilmCode
                        End If
                    Next

                    TmpWeek.Films(txtFilmName.Text).Filmcode = newFilmCode
                End If
            Next
        ElseIf e.ColumnIndex = 1 Then
            For Each TmpWeek In TmpBT.Weeks
                TmpWeek.Films(txtFilmName.Text).GrossIndex = e.Value
            Next
        ElseIf e.ColumnIndex = 2 Then
            If IsNumeric(e.Value) AndAlso (e.Value >= 1 And e.Value <= 500) Then
                For Each TmpWeek In TmpBT.Weeks
                    TmpWeek.Films(txtFilmName.Text).Index = e.Value
                Next
                If e.Value < 5 Then
                    lblIndexWarning.Visible = True
                Else
                    lblIndexWarning.Visible = False
                End If
            End If
        End If

    End Sub

    Private Sub cmdFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        frmFindFilmInLibrary.ShowDialog()
        Saved = False
        UpdateFilmGrid()
    End Sub

    Private Sub grdChannels_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdChannels.GotFocus
        grdChannelInfo.ClearSelection()
    End Sub

    Private Sub grdChannelInfo_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdChannelInfo.CellDoubleClick
        Dim TmpCell As ExtendedComboBoxCell = grdChannels.Rows(e.RowIndex).Cells(0)
        Dim TmpBT As Trinity.cBookingType = Nothing
        Dim DP As Integer
        If Not TmpCell.Value Is Nothing Then
            TmpBT = TmpCell.Value
        End If
        If TmpCell Is Nothing OrElse DP < -1 OrElse DP > TmpBT.Dayparts.Count - 1 Then
            Exit Sub
        End If

        Dim frmDetails As New frmDetails
        frmDetails.grdDetails.Rows.Clear()
        frmDetails.grdDetails.Rows.Add(CInt(TmpBT.Weeks.Count) + 2)
        frmDetails.grdDetails.ForeColor = Color.Black
        frmDetails.grdDetails.Rows(1).Cells(0).Value = "Week"
        frmDetails.grdDetails.Rows(1).Cells(1).Value = "Gross CPP"
        frmDetails.grdDetails.Rows(1).Cells(0).Style.ForeColor = Color.Blue
        frmDetails.grdDetails.Rows(1).Cells(1).Style.ForeColor = Color.Blue

        If e.ColumnIndex = 3 Then
            frmDetails.grdDetails.Rows(0).Cells(0).Value = "Total"
            frmDetails.grdDetails.Rows(0).Cells(0).Style.ForeColor = Color.Blue
            For i As Integer = 1 To TmpBT.Weeks.Count
                Dim Idx As Single = 0
                For d As Long = TmpBT.Weeks(i).StartDate To TmpBT.Weeks(i).EndDate
                    Idx += TmpBT.BuyingTarget.GetCPPForDate(d)
                Next
                Idx /= TmpBT.Weeks(i).EndDate - TmpBT.Weeks(i).StartDate + 1
                frmDetails.grdDetails.Rows(i + 1).Cells(0).Value = TmpBT.Weeks(i).Name
                If TmpBT.Weeks(i).SpotIndex > 0 Then
                    frmDetails.grdDetails.Rows(i + 1).Cells(1).Value = Format(TmpBT.Weeks(i).GrossCPP / (TmpBT.Weeks(i).SpotIndex / 100), "C0")
                Else
                    frmDetails.grdDetails.Rows(i + 1).Cells(1).Value = Format(TmpBT.Weeks(i).GrossCPP, "C0")
                End If
            Next
        ElseIf e.ColumnIndex = 4 Then
            frmDetails.grdDetails.Rows(0).Cells(0).Value = "Gross CPP"
            frmDetails.grdDetails.Rows(0).Cells(0).Style.ForeColor = Color.Blue
            While frmDetails.grdDetails.ColumnCount > 1
                frmDetails.grdDetails.Columns.RemoveAt(1)
            End While
            For Each _dp As Trinity.cDaypart In TmpBT.Dayparts
                With frmDetails.grdDetails.Columns(frmDetails.grdDetails.Columns.Add("colDP" & _dp.Name, ""))
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    frmDetails.grdDetails.Rows(1).Cells(.Index).Value = _dp.Name
                    frmDetails.grdDetails.Rows(1).Cells(.Index).Style.ForeColor = Color.Blue
                End With
            Next
            For i As Integer = 1 To TmpBT.Weeks.Count
                Dim Idx As Single = 0
                For d As Long = TmpBT.Weeks(i).StartDate To TmpBT.Weeks(i).EndDate
                    Idx += TmpBT.BuyingTarget.GetCPPForDate(d, DP)
                Next
                Idx /= TmpBT.Weeks(i).EndDate - TmpBT.Weeks(i).StartDate + 1
                frmDetails.grdDetails.Rows(i + 1).Cells(0).Value = TmpBT.Weeks(i).Name
                For DP = 0 To TmpBT.Dayparts.Count - 1
                    frmDetails.grdDetails.Rows(i + 1).Cells(1 + DP).Value = Format(TmpBT.BuyingTarget.GetCPPForDate(TmpBT.Weeks(i).StartDate, DP), "C0")
                Next
            Next
        End If
        frmDetails.ShowDialog()

    End Sub

    Private Sub grdChannelInfo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdChannelInfo.GotFocus
        grdChannels.ClearSelection()
    End Sub

    Private Sub cmbBuyer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBuyer.SelectedIndexChanged
        Saved = False
        If cmbBuyer.SelectedItem IsNot Nothing AndAlso cmbBuyer.SelectedItem.GetType.FullName = "clTrinity.Trinity.cPerson" Then
            Campaign.BuyerID = cmbBuyer.SelectedItem.id
        End If
    End Sub

    Private Sub cmbPlanner_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPlanner.SelectedIndexChanged
        Saved = False
        If cmbPlanner.SelectedItem IsNot Nothing AndAlso cmbPlanner.SelectedItem.GetType.FullName = "clTrinity.Trinity.cPerson" Then
            Campaign.PlannerID = cmbPlanner.SelectedItem.id
        End If
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        Saved = False
    End Sub

    Private Sub txtMain_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMain.TextChanged

    End Sub

    Private Sub txtSec_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSec.TextChanged

    End Sub

    Private Sub txtThird_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtThird.TextChanged

    End Sub

    Private Sub txtFilmName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFilmName.TextChanged
        Saved = False
    End Sub

    Private Sub txtFilmLength_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Saved = False
    End Sub

    Private Sub cmdCalculateDayparts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCalculateDayparts.Click
        mnuCalculateDaypart.Show(cmdCalculateDayparts, New System.Drawing.Point(0, cmdCalculateDayparts.Height))
    End Sub

    Sub mnuCalculateDayparts(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuLastWeek.Click, mnuLastYear.Click
        For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                If TmpBT.BookIt Then
                    Dim TotDP As Integer = 0
                    For dp As Integer = 0 To TmpBT.Dayparts.Count - 1
                        TotDP += TmpBT.Dayparts(dp).Share
                    Next
                    If TotDP <> 100 Then
                        CalculateNDDaypart(TmpBT, sender.tag)
                    End If
                End If
            Next
        Next
        UpdateChannelGrid()
        UpdateChannelInfoGrid()
    End Sub

    Private Enum RefPeriod
        RefPeriodLastYear = 1
        RefPeriodLastWeek = 0
    End Enum

    Private Sub CalculateNDDaypart(ByVal BT As Trinity.cBookingType, ByVal ReferencePeriod As RefPeriod)
        Dim TmpAdedge As New ConnectWrapper.Breaks
        Dim b As Long
        Dim TRPDaypart()
        Dim BreakCount As Long
        ReDim TRPDaypart(0 To Trinity.cKampanj.MAX_DAYPARTS)
        Dim TotTRP As Single = 0
        Dim TmpDay As Date

        TmpAdedge.setArea(ActiveCampaign.Area)
        If ReferencePeriod = RefPeriod.RefPeriodLastWeek Then
            TmpAdedge.setPeriod("-1fw")
        Else
            TmpDay = Date.FromOADate(ActiveCampaign.StartDate).AddYears(-1)
            TmpAdedge.setPeriod(Format(TmpDay, "ddMMyy") & "-" & Format(TmpDay.AddDays(6), "ddMMyy"))
        End If
        TmpAdedge.setChannelsArea(BT.ParentChannel.AdEdgeNames, ActiveCampaign.Area)
        Trinity.Helper.AddTarget(TmpAdedge, BT.BuyingTarget.Target, True)
        TmpAdedge.setTargetMnemonic(BT.BuyingTarget.Target.TargetName, True)
        'TmpAdedge.setUniverseUserDefined(ActiveCampaign.UniStr)
        Trinity.Helper.AddTimeShift(TmpAdedge)
        TmpAdedge.clearList()
        BreakCount = TmpAdedge.Run
        For b = 0 To BreakCount - 1
            Dim u As Integer
            u = ActiveCampaign.TimeShift
            TRPDaypart(BT.Dayparts.GetDaypartIndexForMam(TmpAdedge.getAttrib(Connect.eAttribs.aFromTime, b) \ 60)) += TmpAdedge.getUnit(Connect.eUnits.uTRP, b, , u, 0)
            TotTRP += TmpAdedge.getUnit(Connect.eUnits.uTRP, b, , u, 0)
        Next
        Dim _total As Single = 0
        For dp As Integer = 0 To BT.Dayparts.Count - 1
            If Not TotTRP = 0 Then
                BT.Dayparts(dp).Share = Math.Round((TRPDaypart(dp) / TotTRP) * 100, 0, MidpointRounding.ToEven)
            Else
                BT.Dayparts(dp).Share = 0
            End If
            _total += BT.Dayparts(dp).Share
        Next
        If _total <> 100 Then
            BT.Dayparts(BT.Dayparts.Count - 1).Share += 100 - _total
        End If
    End Sub

    Private Function CalculateNDCombo(ByVal BT As Trinity.cBookingType, ByVal ReferencePeriod As RefPeriod) As Single
        Dim TmpAdedge As New ConnectWrapper.Breaks
        Dim b As Long
        Dim TRPDaypart()
        Dim BreakCount As Long
        ReDim TRPDaypart(0 To 5)
        Dim TotTRP As Single = 0
        Dim TmpDay As Date

        TmpAdedge.setArea(ActiveCampaign.Area)
        If ReferencePeriod = RefPeriod.RefPeriodLastWeek Then
            TmpAdedge.setPeriod("-1fw")
        Else
            TmpDay = Date.FromOADate(ActiveCampaign.StartDate).AddYears(-1)
            TmpAdedge.setPeriod(Format(TmpDay, "ddMMyy") & "-" & Format(TmpDay.AddDays(6), "ddMMyy"))
        End If
        TmpAdedge.setChannelsArea(BT.ParentChannel.AdEdgeNames, ActiveCampaign.Area)
        'TmpAdedge.setTargetMnemonic(BT.BuyingTarget.Target.TargetName, True)
        Trinity.Helper.AddTarget(TmpAdedge, ActiveCampaign.MainTarget)
        'TmpAdedge.setUniverseUserDefined(ActiveCampaign.UniStr)
        Trinity.Helper.AddTimeShift(TmpAdedge)
        TmpAdedge.clearList()
        BreakCount = TmpAdedge.Run
        For b = 0 To BreakCount - 1
            Dim u As Integer
            u = ActiveCampaign.TimeShift
            TRPDaypart(BT.Dayparts.GetDaypartIndexForMam(TmpAdedge.getAttrib(Connect.eAttribs.aFromTime, b) \ 60)) += TmpAdedge.getUnit(Connect.eUnits.uTRP, b, , u, 0)
        Next
        For d As Integer = 0 To BT.Dayparts.Count - 1
            TotTRP += (BT.Dayparts(d).Share / 100) * TRPDaypart(d)
        Next
        Return TotTRP
    End Function

    Private Sub chkFilmIdxAsDiscount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilmIdxAsDiscount.CheckedChanged
        ActiveCampaign.FilmindexAsDiscount = chkFilmIdxAsDiscount.Checked
        grdFilmDetails.Columns("colGrossFilmIndex").Visible = ActiveCampaign.FilmindexAsDiscount
    End Sub

    Private Sub lblMainTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblMainTarget.Click
        If frmFindTarget.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ActiveCampaign.MainTarget.TargetType = Trinity.cTarget.TargetTypeEnum.trgUserTarget
            ActiveCampaign.MainTarget.TargetGroup = frmFindTarget.tvwTargets.SelectedNode.Tag
            ActiveCampaign.MainTarget.TargetName = frmFindTarget.tvwTargets.SelectedNode.Text
            txtMain.Text = ActiveCampaign.MainTarget.TargetName
            lblMainSize.Text = Format(ActiveCampaign.MainTarget.UniSize * 1000, "##,##0")
            'makes the label red if the value is invalid
            If ActiveCampaign.MainTarget.UniSize = 0 Then
                lblMainTarget.ForeColor = Color.Red
            Else
                lblMainTarget.ForeColor = Color.Black
            End If
            txtMain.ForeColor = Color.Gray
            Saved = False
        End If
    End Sub

    Private Sub lblSecondTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblSecondTarget.Click
        If frmFindTarget.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ActiveCampaign.SecondaryTarget.TargetType = Trinity.cTarget.TargetTypeEnum.trgUserTarget
            ActiveCampaign.SecondaryTarget.TargetGroup = frmFindTarget.tvwTargets.SelectedNode.Tag
            ActiveCampaign.SecondaryTarget.TargetName = frmFindTarget.tvwTargets.SelectedNode.Text
            txtSec.Text = ActiveCampaign.SecondaryTarget.TargetName
            lblSecondSize.Text = Format(ActiveCampaign.SecondaryTarget.UniSize * 1000, "##,##0")
            'makes the label red if the value is invalid
            If ActiveCampaign.SecondaryTarget.UniSize = 0 Then
                lblSecondTarget.ForeColor = Color.Red
            Else
                lblSecondTarget.ForeColor = Color.Black
            End If
            Saved = False
            txtSec.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub lblThirdTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblThirdTarget.Click
        If frmFindTarget.ShowDialog() = Windows.Forms.DialogResult.OK Then
            ActiveCampaign.ThirdTarget.TargetType = Trinity.cTarget.TargetTypeEnum.trgUserTarget
            ActiveCampaign.ThirdTarget.TargetGroup = frmFindTarget.tvwTargets.SelectedNode.Tag
            ActiveCampaign.ThirdTarget.TargetName = frmFindTarget.tvwTargets.SelectedNode.Text
            txtThird.Text = ActiveCampaign.ThirdTarget.TargetName
            lblThirdSize.Text = Format(ActiveCampaign.ThirdTarget.UniSize * 1000, "##,##0")
            'makes the label red if the value is invalid
            If ActiveCampaign.ThirdTarget.UniSize = 0 Then
                lblThirdTarget.ForeColor = Color.Red
            Else
                lblThirdTarget.ForeColor = Color.Black
            End If
            txtThird.ForeColor = Color.Gray
            Saved = False
        End If
    End Sub

    Private Shadows Sub MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblMainTarget.MouseHover, lblSecondTarget.MouseHover, lblThirdTarget.MouseHover
        DirectCast(sender, Windows.Forms.Label).BorderStyle = BorderStyle.Fixed3D
    End Sub

    Private Shadows Sub MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblMainTarget.MouseLeave, lblSecondTarget.MouseLeave, lblThirdTarget.MouseLeave
        DirectCast(sender, Windows.Forms.Label).BorderStyle = BorderStyle.None
    End Sub


    Private Sub cmdEnhancements_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmEnhancements.grdEnhancements.Rows.Clear()
        If frmEnhancements.ShowDialog <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        Dim Chan As String = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        Dim BT As String = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).Name

        Dim TmpIndex As Trinity.cIndex = ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes.Add("")
        TmpIndex.FromDate = Date.FromOADate(ActiveCampaign.StartDate)
        TmpIndex.ToDate = Date.FromOADate(ActiveCampaign.EndDate)
        TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eTRP

        For Each TmpRow As DataGridViewRow In frmEnhancements.grdEnhancements.Rows
            With TmpIndex.Enhancements.Add()
                .Amount = TmpRow.Cells(1).Value
                .Name = TmpRow.Cells(0).Value
            End With
            'TmpIndex.Enhancements.SpecificFactor = frmEnhancements.txtSpecFactor.Text / 100
        Next
        cmbIndexChannel_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub grdIndexes_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdIndexes.RowEnter
        If grdIndexes.SelectedRows.Count = 0 Then Exit Sub
        Dim Chan As String '= Microsoft.VisualBasic.Left(cmbIndexChannel.Text, InStr(cmbIndexChannel.Text, " ") - 1)
        Dim BT As String '= Mid(cmbIndexChannel.Text, InStr(cmbIndexChannel.Text, " ") + 1)
        Chan = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        BT = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).Name

        If grdIndexes.Rows(e.RowIndex).Tag.GetType Is GetType(Trinity.cIndex) Then Exit Sub
        Dim TmpIndex As Trinity.cIndex = ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes(grdIndexes.SelectedRows.Item(0).Tag)
        'UNCOMMENT BELOW IF THE BUTTON IS TO BE USED
        'cmdEditEnhancement.Enabled = TmpIndex.Enhancements.Count > 0
    End Sub

    Private Sub cmdEditEnhancement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditEnhancement.Click
        Dim Chan As String = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        Dim BT As String = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).Name

        If grdIndexes.SelectedRows.Count = 0 Then Exit Sub

        If grdIndexes.SelectedRows.Item(0).Tag.GetType Is GetType(Trinity.cIndex) Then
            Windows.Forms.MessageBox.Show("Can not edit enhancements on a pricelist index.", "T RI N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim TmpIndex As Trinity.cIndex = ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes(grdIndexes.SelectedRows.Item(0).Tag)
        frmEnhancements.grdEnhancements.Rows.Clear()
        For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
            With frmEnhancements.grdEnhancements.Rows(frmEnhancements.grdEnhancements.Rows.Add())
                .Cells(0).Value = TmpEnh.Name
                .Cells(1).Value = TmpEnh.Amount
                .Cells(2).Value = TmpEnh.UseThis
                .Tag = TmpEnh.ID
            End With
        Next
        If ActiveCampaign.Contract Is Nothing Then
            frmEnhancements.grdEnhancements.Columns(2).Visible = False
        Else
            frmEnhancements.grdEnhancements.Columns(2).Visible = True
        End If
        'frmEnhancements.txtSpecFactor.Text = TmpIndex.Enhancements.SpecificFactor * 100
        If frmEnhancements.ShowDialog() <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        TmpIndex.Enhancements.Clear()

        For Each TmpRow As DataGridViewRow In frmEnhancements.grdEnhancements.Rows
            With TmpIndex.Enhancements.Add()
                .Amount = TmpRow.Cells(1).Value
                .Name = TmpRow.Cells(0).Value
                .UseThis = TmpRow.Cells(2).Value
                If TmpRow.Tag <> "" Then
                    .ID = TmpRow.Tag
                End If
            End With
        Next

        'Check if any value has been changed
        Dim _noChange As Boolean = True
        For Each TmpRow As DataGridViewRow In frmEnhancements.grdEnhancements.Rows
            If TmpRow.Tag Is Nothing Then
                _noChange = False
                Exit For
            Else
                If ActiveCampaign.Contract IsNot Nothing Then
                    With ActiveCampaign.Contract.Channels(Chan).BookingTypes(1).Item(BT).Indexes(TmpIndex.ID).Enhancements.Item(TmpRow.Tag)
                        If .Amount <> TmpRow.Cells(1).Value OrElse _
                           .Name <> TmpRow.Cells(0).Value Then
                            _noChange = False
                            Exit For
                        End If
                    End With
                End If
            End If
        Next

        If Not _noChange AndAlso ActiveCampaign.Contract IsNot Nothing Then
            If MsgBox("Reflect this change in the contract?", MsgBoxStyle.YesNo, "Contract exists") = MsgBoxResult.Yes Then
                For Each TmpRow As DataGridViewRow In frmEnhancements.grdEnhancements.Rows
                    If TmpRow.Tag Is Nothing Then
                        TmpRow.Tag = Guid.NewGuid.ToString
                        With ActiveCampaign.Contract.Channels(Chan).BookingTypes(1).Item(BT).Indexes(TmpIndex.ID).Enhancements.Add
                            .Amount = TmpRow.Cells(1).Value
                            .Name = TmpRow.Cells(0).Value
                            .UseThis = TmpRow.Cells(2).Value
                            If TmpRow.Tag <> "" Then
                                .ID = TmpRow.Tag
                            End If
                        End With
                    Else
                        With ActiveCampaign.Contract.Channels(Chan).BookingTypes(1).Item(BT).Indexes(TmpIndex.ID).Enhancements.Item(TmpRow.Tag)
                            .Amount = TmpRow.Cells(1).Value
                            .Name = TmpRow.Cells(0).Value
                            .UseThis = TmpRow.Cells(2).Value
                            If TmpRow.Tag <> "" Then
                                .ID = TmpRow.Tag
                            End If
                        End With
                    End If


                Next
            End If
        End If
        'TmpIndex.Enhancements.SpecificFactor = frmEnhancements.txtSpecFactor.Text / 100
        cmbIndexChannel_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub lblOldPricelist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblOldPricelist.Click
        MsgBox(strPricelistUpdate, MsgBoxStyle.Information, "")
    End Sub

    Sub mouse_enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblOldPricelist.MouseEnter
        Me.Cursor = Windows.Forms.Cursors.Hand
    End Sub

    Sub mouse_leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblOldPricelist.MouseLeave
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub tpIndex_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpIndex.Enter
        'cmdEnhancements.Visible = ActiveCampaign.Area = "DK"
        cmdEditEnhancement.Visible = ActiveCampaign.Area = "DK"
        If ActiveCampaign.Area = "DK" Then
            'cmdCopyIndex.Top = 75
        Else
            'cmdCopyIndex.Top = 131
            If colOn.Items.Contains("Min. CPP") Then
                colOn.Items.Remove("Min. CPP")
            End If
        End If
    End Sub

    Private Sub tpCombinations_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpCombinations.Enter
        grdCombos.Rows.Clear()
        For Each TmpCombo As Trinity.cCombination In ActiveCampaign.Combinations
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
                TmpStr &= TmpCC.Bookingtype.ToString & ","
            Next
            TmpStr = TmpStr.TrimEnd(",")
            e.Value = TmpStr
        End If
    End Sub

    Private Sub grdCombos_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCombos.RowEnter
        Dim TmpCombo As Trinity.cCombination = grdCombos.Rows(e.RowIndex).Tag
        If TmpCombo Is Nothing Then Exit Sub
        grpCombo.Tag = TmpCombo
        grdCombo.Rows.Clear()
        For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
            With grdCombo.Rows(grdCombo.Rows.Add)
                .Tag = TmpCC
            End With
        Next
        txtComboName.Text = TmpCombo.Name
        chkShowAsOne.Checked = TmpCombo.ShowAsOne
        chkPrintAsOne.Checked = TmpCombo.PrintAsOne
        If TmpCombo.CombinationOn = Trinity.cCombination.CombinationOnEnum.coBudget Then
            optBudget.Checked = True
        Else
            optTRP.Checked = True
        End If
        grpCombo.Visible = True
        txtMarathonIDCombo.Text = TmpCombo.MarathonIDCombination
        If TmpCombo.sendAsOneUnitTOMarathon
            chkSendAsUnitMarathon.Checked = TmpCombo.sendAsOneUnitTOMarathon
        Else
            chkSendAsUnitMarathon.Checked = TmpCombo.sendAsOneUnitTOMarathon
        End If
    End Sub

    Private Sub txtComboName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtComboName.TextChanged
        DirectCast(grpCombo.Tag, Trinity.cCombination).Name = txtComboName.Text
        grdCombos.Invalidate()
    End Sub

    Private Function RecountCombination(ByVal tmpCombination As Trinity.cCombination, Optional ByVal invert As Boolean = False) As Boolean
        Dim previousValuesExist = False

        'invert is set to true when calling form the mouse events since the checked change is not done before the call
        If optBudget.Checked <> invert Then
            If tmpCombination.Relations.count > 0 Then
                Dim budgetForWeek(tmpCombination.Relations(1).Bookingtype.Weeks.Count) As Decimal
                Dim tmpCounter = 0
                For Each tmpChan As Trinity.cCombinationChannel In tmpCombination.Relations
                    tmpCounter = 0
                    For Each tmpWeek As Trinity.cWeek In tmpChan.Bookingtype.Weeks
                        budgetForWeek(tmpCounter) += tmpWeek.NetBudget
                        tmpCounter += 1
                        If tmpWeek.NetBudget > 0 Then
                            previousValuesExist = True
                        End If
                    Next
                Next


                If previousValuesExist Then
                    If MessageBox.Show("You've previously entered values for TRPs or budgets for this campaign when it was a combination on TRPs. Recalculate?", "T R I N I T Y", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Cancel Then Return False
                End If

                For Each tmpChan As Trinity.cCombinationChannel In tmpCombination.Relations
                    tmpCounter = 0
                    For Each tmpWeek As Trinity.cWeek In tmpChan.Bookingtype.Weeks
                        tmpWeek.TRPControl = False
                        tmpWeek.NetBudget = tmpChan.Percent * budgetForWeek(tmpCounter)
                        tmpCounter += 1
                    Next
                Next
            End If
        ElseIf optTRP.Checked <> invert Then
            If tmpCombination.Relations.count > 0 Then
                Dim TRPForWeek(tmpCombination.Relations(1).Bookingtype.Weeks.Count) As Decimal

                Dim tmpCounter = 0
                For Each tmpChan As Trinity.cCombinationChannel In tmpCombination.Relations
                    tmpCounter = 0
                    For Each tmpWeek As Trinity.cWeek In tmpChan.Bookingtype.Weeks
                        TRPForWeek(tmpCounter) += tmpWeek.TRPBuyingTarget
                        tmpCounter += 1
                        If tmpWeek.TRPBuyingTarget > 0 Then previousValuesExist = True
                        If tmpWeek.NetBudget > 0 Then

                        End If
                    Next
                Next

                If previousValuesExist Then
                    If MessageBox.Show("You've previously entered values for TRPs or budgets for this campaign when it was a combination on budget. Recalculate?", "T R I N I T Y", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Cancel Then Return False
                End If

                For Each tmpChan As Trinity.cCombinationChannel In tmpCombination.Relations
                    tmpCounter = 0
                    For Each tmpWeek As Trinity.cWeek In tmpChan.Bookingtype.Weeks
                        tmpWeek.TRPControl = True
                        tmpWeek.TRPBuyingTarget = tmpChan.Percent * TRPForWeek(tmpCounter)
                        tmpCounter += 1
                    Next
                Next
            End If
        End If

        Return True
    End Function

    Private Sub optBudget_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles optBudget.MouseDown, optTRP.MouseDown
        If _activeCampaign.Combinations.Count = 0 Then Exit Sub

        Dim tmpCombination As Trinity.cCombination = _activeCampaign.Combinations(DirectCast(grpCombo.Tag, Trinity.cCombination).ID)
        If tmpCombination Is Nothing Then Exit Sub

        If RecountCombination(tmpCombination, True) Then
            optBudget.Checked = optTRP.Checked
            optTRP.Checked = Not optBudget.Checked

            DirectCast(grpCombo.Tag, Trinity.cCombination).CombinationOn = -(optTRP.Checked)
        End If
    End Sub

    Private Sub cmdAddChannelToCombo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddChannelToCombo.Click
        With DirectCast(grpCombo.Tag, Trinity.cCombination)
            Dim FirstChannel As Trinity.cBookingType = Nothing
            For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels
                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        FirstChannel = TmpBT
                        For Each TmpCombo As Trinity.cCombination In ActiveCampaign.Combinations
                            For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                                If TmpCC.Bookingtype Is TmpBT Then
                                    FirstChannel = Nothing
                                End If
                            Next
                        Next
                        If Not FirstChannel Is Nothing Then
                            Exit For
                        End If
                    End If
                Next
                If Not FirstChannel Is Nothing Then
                    Exit For
                End If
            Next
            If FirstChannel IsNot Nothing Then
                grdCombo.Rows(grdCombo.Rows.Add).Tag = .Relations.Add(FirstChannel, 0)
                grdCombos.Invalidate()
            End If
        End With
    End Sub

    Private Sub grdCombo_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCombo.CellEnter
        Dim NextChannel As Trinity.cBookingType
        Dim MyCC As Trinity.cCombinationChannel = grdCombo.Rows(e.RowIndex).Tag
        If e.ColumnIndex = 0 And e.RowIndex > -1 Then
            Dim cellTarget As ExtendedComboBoxCell = grdCombo.Rows(e.RowIndex).Cells(0)
            cellTarget.Items.Clear()
            For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels
                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        NextChannel = TmpBT
                        For Each TmpCombo As Trinity.cCombination In ActiveCampaign.Combinations
                            For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                                If TmpCC.Bookingtype Is TmpBT And TmpCC IsNot MyCC Then
                                    NextChannel = Nothing
                                End If
                            Next
                        Next
                        If Not NextChannel Is Nothing Then
                            cellTarget.Items.Add(TmpBT)
                        End If
                    End If
                Next
            Next
        End If

    End Sub

    Private Sub cmdAddCombo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddCombo.Click
        With grdCombos.Rows(grdCombos.Rows.Add)
            .Tag = ActiveCampaign.Combinations.Add
        End With
    End Sub

    Private Sub grdCombo_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdCombo.CellValueNeeded
        Dim TmpCC As Trinity.cCombinationChannel = grdCombo.Rows(e.RowIndex).Tag
        If e.ColumnIndex = 0 Then
            e.Value = TmpCC.Bookingtype
        Else
            e.Value = TmpCC.Relation
        End If
        grdCombos.Invalidate()
    End Sub

    Private Sub grdCombo_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdCombo.CellValuePushed
        Dim TmpCC As Trinity.cCombinationChannel = grdCombo.Rows(e.RowIndex).Tag

        If e.ColumnIndex = 0 Then
            TmpCC.Bookingtype = e.Value
        Else
            Dim oldValue As Single = TmpCC.Relation
            TmpCC.Relation = e.Value
            If Not RecountCombination(grdCombos.SelectedRows(0).Tag) Then
                TmpCC.Relation = oldValue
            End If
        End If
        grdCombos.Invalidate()


    End Sub

    Private Sub cmdCombinationsNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCombinationsNext.Click
        tabSetup.SelectTab(tpFilms)
    End Sub

    Private Sub cmdDeleteCombo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteCombo.Click
        For Each TmpRow As DataGridViewRow In grdCombos.SelectedRows
            Dim TmpCombo As Trinity.cCombination = TmpRow.Tag
            For Each tmpcc As Trinity.cCombinationChannel In TmpCombo.Relations
                tmpcc.Bookingtype.Combination = Nothing
                tmpcc.Bookingtype = Nothing
            Next

            ActiveCampaign.Combinations.Remove(TmpCombo)
            grdCombos.Rows.Remove(TmpRow)
        Next
        grpCombo.Visible = False
    End Sub

    Private Sub cmdDeleteChannelFromCombo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteChannelFromCombo.Click
        With DirectCast(grpCombo.Tag, Trinity.cCombination)
            For Each TmpRow As DataGridViewRow In grdCombo.SelectedRows
                Dim TmpCC As Trinity.cCombinationChannel = TmpRow.Tag
                .Relations.Remove(TmpCC)
                grdCombo.Rows.Remove(TmpRow)
            Next
        End With
        grdCombos.Invalidate()
    End Sub

    Private Sub cmdCopyIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopyIndex.Click
        Dim mnuCopy As New ContextMenu
        For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                If TmpBT.BookIt And TmpBT IsNot cmbIndexChannel.SelectedItem Then
                    mnuCopy.MenuItems.Add(TmpBT.ToString, AddressOf CopyIndex).Tag = TmpBT
                End If
            Next
        Next
        mnuCopy.Show(cmdCopyIndex, New System.Drawing.Point(0, cmdCopyIndex.Height))
    End Sub

    Private Sub CopyIndex(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim frm As New frmThreeChoices("An index with this name already exists. What would you like to do?")

        Dim TmpBT As Trinity.cBookingType = sender.tag

        For Each TmpIndex As Trinity.cIndex In TmpBT.Indexes

            Dim recievingBT As Trinity.cBookingType = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType)
            For Each tmpindex2 As Trinity.cIndex In recievingBT.Indexes
                If Not frm.chkDoForAll.Checked AndAlso recievingBT.Indexes(tmpindex2.ID).Name = TmpIndex.Name Then
                    frm.lblItem.Text = TmpIndex.Name
                    frm.ShowDialog()
                End If
            Next

            If Not frm.Result = Windows.Forms.DialogResult.Cancel Then
                With recievingBT
                    If frm.Result = Windows.Forms.DialogResult.OK Then
                        .Indexes.Remove(.Indexes.IndexForName(TmpIndex.Name))
                    End If
                    With .Indexes.Add(TmpIndex.Name)
                        .FromDate = TmpIndex.FromDate
                        .ToDate = TmpIndex.ToDate
                        .IndexOn = TmpIndex.IndexOn
                        If TmpIndex.Enhancements.Count > 0 Then
                            For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
                                With .Enhancements.Add()
                                    .Name = TmpEnh.Name
                                    .Amount = TmpEnh.Amount
                                    .UseThis = TmpEnh.UseThis
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
        cmbIndexChannel_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub cmdCopyAV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopyAV.Click
        Dim mnuCopy As New ContextMenu
        For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                If TmpBT.BookIt And TmpBT IsNot cmbIndexChannel.SelectedItem Then
                    mnuCopy.MenuItems.Add(TmpBT.ToString, AddressOf CopyAV).Tag = TmpBT
                End If
            Next
        Next
        mnuCopy.Show(cmdCopyAV, New System.Drawing.Point(0, cmdCopyAV.Height))
    End Sub

    Sub CopyAV(ByVal sender As Object, ByVal e As EventArgs)
        Dim TmpBT As Trinity.cBookingType = sender.tag

        For Each TmpAv As Trinity.cAddedValue In TmpBT.AddedValues
            With DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType)
                With .AddedValues.Add(TmpAv.Name)
                    .IndexGross = TmpAv.IndexGross
                    .IndexNet = TmpAv.IndexNet
                    .ShowIn = TmpAv.ShowIn
                    .UseThis = TmpAv.UseThis
                End With
            End With
        Next
        cmbIndexChannel_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Private Sub chkShowAsOne_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowAsOne.CheckedChanged
        Dim TmpCombo As Trinity.cCombination = grpCombo.Tag
        If TmpCombo Is Nothing Then Exit Sub

        TmpCombo.ShowAsOne = chkShowAsOne.Checked
        If chkShowAsOne.Checked Then
            chkPrintAsOne.Checked = True
        End If
        'Deprecated: ShowMe now derives from ShowAsOne. See Bookingtype.ShowMe
        ''set that the BT on this channel should (not) be shown
        'For Each tmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
        '    tmpCC.Bookingtype.ShowMe = Not chkShowAsOne.Checked
        'Next

    End Sub

    Private Sub cmdComboND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdComboND.Click
        Dim _comb As Trinity.cCombination = grpCombo.Tag
        If _comb Is Nothing Then Exit Sub

        mnuUseActualTRPs.Enabled = (ActiveCampaign.ActualSpots.Count > 0)
        mnuCalculcateComboND.Show(cmdComboND, New System.Drawing.Point(0, cmdComboND.Height))
    End Sub

    Private Sub mnuLastWeekND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLastWeekND.Click
        frmProgress.Status = "Calculating natural delivery..."
        frmProgress.Show()
        frmProgress.Progress = 0
        For Each TmpRow As DataGridViewRow In grdCombo.Rows
            frmProgress.Progress += (1 / (grdCombo.Rows.Count)) * 100
            Dim TmpCC As Trinity.cCombinationChannel = TmpRow.Tag
            TmpCC.Relation = Math.Round(CalculateNDCombo(TmpCC.Bookingtype, RefPeriod.RefPeriodLastWeek), 0)
        Next
        frmProgress.Hide()
        grdCombo.Invalidate()
    End Sub

    Private Sub mnuSamePeriodND_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSamePeriodND.Click
        frmProgress.Status = "Calculating natural delivery..."
        frmProgress.Show()
        frmProgress.Progress = 0
        For Each TmpRow As DataGridViewRow In grdCombo.Rows
            'IF a combination contains 6 BTs the percentage for each BT will be 17 which will be 102, which is too high for the frmProgress dimension. So if the total is too high we substract from each BTs percentage.
            Dim tmpProg As Integer = (1 / (grdCombo.Rows.Count)) * 100
            If ((tmpProg * grdCombo.Rows.Count) > 100) Then
                tmpProg = tmpProg - 1
            End If
            frmProgress.Progress += tmpProg
            Dim TmpCC As Trinity.cCombinationChannel = TmpRow.Tag
            TmpCC.Relation = Math.Round(CalculateNDCombo(TmpCC.Bookingtype, RefPeriod.RefPeriodLastYear), 0)
        Next
        frmProgress.Hide()
        grdCombo.Invalidate()
    End Sub

    Private Sub chkMultiply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMultiply.Click
        ActiveCampaign.MultiplyAddedValues = chkMultiply.Checked
    End Sub


    Private Sub cmdQuickCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuickCopy.Click
        Dim _frmAdd As New frmQuickCopy
        With _frmAdd
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim _bt As Trinity.cBookingType
                If .cmbBookingType.SelectedIndex = .cmbBookingType.Items.Count - 1 Then
                    Dim _chan As Trinity.cChannel = .cmbChannel.SelectedItem
                    _chan.BookingTypes.Add(.txtName.Text, False).Shortname = .txtShortname.Text
                    _bt = _chan.BookingTypes(.txtName.Text)
                    _bt.ReadDefaultDayparts()
                    For Each _week As Trinity.cWeek In ActiveCampaign.Channels(1).BookingTypes(1).Weeks
                        With _bt.Weeks.Add(_week.Name)
                            .StartDate = _week.StartDate
                            .EndDate = _week.EndDate
                            For Each _film As Trinity.cFilm In _week.Films
                                With .Films.Add(_film.Name)
                                    .FilmLength = _film.FilmLength
                                    .Index = _bt.FilmIndex(.FilmLength)
                                    .Filmcode = _film.Filmcode
                                    .GrossIndex = _bt.FilmIndex(.FilmLength)
                                End With
                            Next
                        End With
                    Next
                Else
                    _bt = .cmbBookingType.SelectedItem
                End If
                _bt.IsRBS = .btnRBS.Checked
                _bt.IsSpecific = .btnSpecifics.Checked
                _bt.IsCompensation = .chkIsCompensation.Checked
                _bt.IsPremium = .chkIsPremium.Checked
                _bt.IsSponsorship = .chkIsSponsorship.Checked
                If .optPricelist.Checked Then
                    _bt.PricelistName = .cmbPricelist.SelectedItem
                    _bt.ReadPricelist()
                Else
                    _bt.Pricelist.Targets.Add(.txtTargetName.Text, _bt, DirectCast(.txtAdedgeTarget.Tag, Trinity.cPricelistTarget).Target, .grdPrice.Rows(0).Cells(0).Value, .grdPrice.Rows(0).Cells(0).Value, True)
                    _bt.Pricelist.Targets(1).StandardTarget = False
                    Dim _id As String = CreateGUID()
                    _bt.Pricelist.Targets(1).PricelistPeriods.Add("", _id)
                    _bt.Pricelist.Targets(1).PricelistPeriods(_id).FromDate = Date.FromOADate(ActiveCampaign.StartDate)
                    _bt.Pricelist.Targets(1).PricelistPeriods(_id).ToDate = Date.FromOADate(ActiveCampaign.EndDate)
                    _bt.Pricelist.Targets(1).PricelistPeriods(_id).PriceIsCPP = True
                    _bt.Pricelist.Targets(1).PricelistPeriods(_id).TargetNat = .grdPrice.Rows(0).Cells(0).Value
                    _bt.Pricelist.Targets(1).PricelistPeriods(_id).TargetUni = .grdPrice.Rows(0).Cells(0).Value
                    For _dp As Integer = 1 To .grdPrice.ColumnCount - 1
                        _bt.Pricelist.Targets(1).PricelistPeriods(_id).Price(True, _dp - 1) = .grdPrice.Rows(0).Cells(_dp).Tag
                    Next
                End If
                UpdateChannelGrid()

                cmdAddChannel_Click(sender, e)

                grdChannels.Rows(grdChannels.RowCount - 1).Cells(0).Value = _bt

                If .optNewPrice.Checked Then
                    grdChannels.Rows(grdChannels.RowCount - 1).Cells(1).Value = .txtTargetName.Text
                End If
            End If
        End With
    End Sub


    Private Sub cmdFindInAdtoox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFindInAdtoox.Click

        Cursor = Windows.Forms.Cursors.WaitCursor

        Dim AdtooxClient As Single

        Dim table As DataTable = DBReader.getAllFromProducts(ActiveCampaign.ProductID)
        If table.Rows.Count = 1 Then
            If Not IsDBNull(table.Rows(0)!AdtooxBrandID) Then
                AdtooxClient = table.Rows(0)!AdtooxBrandID
            End If
        Else
            MessageBox.Show("You need to enter the Adtoox ID for the product in Setup for this to work.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Cursor = Cursors.Default
            Exit Sub
        End If

        frmAdtooxFilmLibrary.ShowDialog()
        'Add films to the campaign here
        For Each film As Trinity.cAdTooxFilmcode In frmAdtooxFilmLibrary.SelectedFilms
            Dim TmpChan As Trinity.cChannel
            Dim TmpBT As Trinity.cBookingType
            Dim TmpWeek As Trinity.cWeek

            For Each TmpChan In ActiveCampaign.Channels
                For Each TmpBT In TmpChan.BookingTypes
                    For Each TmpWeek In TmpBT.Weeks
                        TmpWeek.Films.Add(film.CopyCode).FilmLength = film.Length
                        TmpWeek.Films(film.CopyCode).Index = TmpBT.FilmIndex(CInt(film.Length))
                        TmpWeek.Films(film.CopyCode).Filmcode = film.CopyCode
                        TmpWeek.Films(film.CopyCode).Description = film.Title
                    Next
                Next
            Next
            UpdateFilmGrid()
        Next

        Cursor = Cursors.Default

    End Sub


    Private Sub cmdSaveToAdtoox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveToAdtoox.Click
        Dim frmAdtoox As New frmAdtooxWindow(ActiveCampaign.Channels(1).BookingTypes(1).Weeks(1).Films(txtFilmName.Text))

        'frmAdtoox.ShowDialog()
        AddHandler frmAdtoox.PageLoaded, AddressOf ShowAdTooxWindow
        AddHandler frmAdtoox.LoginFailed, AddressOf AdTooxLoginFailed
    End Sub

    Sub ShowAdTooxWindow(ByVal sender As Object, ByVal e As EventArgs)
        If DirectCast(sender, frmAdtooxWindow).ShowDialog = Windows.Forms.DialogResult.OK Then
            For Each TmpChan As Trinity.cChannel In ActiveCampaign.Channels
                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                    For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                        TmpWeek.Films(txtFilmName.Text).Filmcode = DirectCast(sender, frmAdtooxWindow).ReturnValue
                    Next
                Next
            Next
        End If
        grdFilmDetails.Invalidate()
    End Sub

    Sub AdTooxLoginFailed(ByVal sender As Object, ByVal e As EventArgs)
        Windows.Forms.MessageBox.Show("The Adtoox username or password is incorrect!" & vbCrLf & vbCrLf & "Correct this in Settings->Preferences, Adtoox tab.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        DirectCast(sender, frmAdtooxWindow).Dispose()
    End Sub

    Private Sub cmdPlayMovie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPlayMovie.Click
        If grdFilms.SelectedRows.Count > 0 Then
            Cursor = Cursors.WaitCursor
            Dim TmpBT As Trinity.cBookingType = ActiveCampaign.Channels(1).BookingTypes(1)
            Dim Filmcode As String = TmpBT.Weeks(1).Films(txtFilmName.Text).Filmcode
            Dim url As String
            If ActiveCampaign.AdToox Is Nothing Then ActiveCampaign.AdToox = New Trinity.cAdtoox

            Try
                url = ActiveCampaign.AdToox.getSingleFilmCodeInfo(Filmcode).GetLinkToPlayVideo
            Catch
                MessageBox.Show("According to Adtoox, you do not have permission to play this film." & vbNewLine _
                                & "Have you entered your username and password in the Adtoox section of Setup?", "A D T O O X", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Cursor = Cursors.Default
                Exit Sub
            End Try

            Dim WC As System.Net.WebRequest
            Dim WR As System.Net.WebResponse

            WC = System.Net.WebRequest.Create(url)
            WR = WC.GetResponse

            Dim streamResponse As IO.Stream = WR.GetResponseStream()

            Dim SReader As New IO.StreamReader(streamResponse)
            Dim TmpString As String

            TmpString = SReader.ReadToEnd

            SReader.Close()
            streamResponse.Close()
            WR.Close()

            Dim func As String = TmpString.Substring(TmpString.IndexOf("writePlayer('"), TmpString.IndexOf(");", TmpString.IndexOf("writePlayer('")) - TmpString.IndexOf("writePlayer('"))
            Dim flvMovie As String = func.Substring(func.IndexOf("'"), func.IndexOf("'", func.IndexOf("'") + 1) - func.IndexOf("'")).TrimStart("'")
            func = func.Substring(func.IndexOf(flvMovie) + flvMovie.Length + 2).Trim.TrimStart("'")
            Dim previewPicture As String = func.Substring(0, func.IndexOf("'"))
            func = func.Substring(previewPicture.Length).Trim.TrimStart("'").Trim.Trim(",")
            Dim type As Integer = 1
            Dim copycode As String = func.Substring(func.IndexOf(",")).Trim.TrimStart(",").Trim.Trim("""")
            Dim flashvars As String = "type=flv&file=" & flvMovie & "&image=" & previewPicture
            Dim frmFlash As New frmFlashPlayer("https://ecexpress.adtoox.com/ec/flvplayer.swf", flashvars, 519, 312)
            frmFlash.Text = Filmcode
            frmFlash.Show()
            Cursor = Cursors.Default
        End If
    End Sub

    Private Sub grdFilmDetails_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdFilmDetails.CellContentClick

    End Sub


    Private Sub cmdAddCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddCopy.Click

        If grdIndexes.Rows.Count = 0 Then
            cmdAddIndex_Click(sender, e)
            Exit Sub
        End If

        Saved = False
        Dim Chan As String
        Dim BT As String
        Dim ID As String = ""
        Dim combo As Trinity.cCombination

        Dim comboID As String = Nothing

        Chan = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        BT = DirectCast(cmbIndexChannel.SelectedItem, Trinity.cBookingType).Name

        For Each combo In ActiveCampaign.Combinations
            If combo.ShowAsOne Then
                For Each cc As Trinity.cCombinationChannel In combo.Relations
                    If cc.Bookingtype.Shortname = BT AndAlso cc.Bookingtype.ParentChannel.ChannelName = Chan Then
                        comboID = combo.ID

                        If MsgBox("This channel has a combination. Changes will apply on all channels in the Combination.", MsgBoxStyle.OkCancel, "") = MsgBoxResult.Cancel Then
                            Exit Sub
                        Else
                            Exit For
                        End If
                    End If
                Next
            End If
        Next
        Dim LastIndex As Trinity.cIndex = ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes(grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag)

        If comboID Is Nothing Then
            ID = ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes.Add(LastIndex.Name).ID

            ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes(ID).FromDate = LastIndex.FromDate
            ActiveCampaign.Channels(Chan).BookingTypes(BT).Indexes(ID).ToDate = LastIndex.ToDate

        Else
            combo = ActiveCampaign.Combinations(comboID)
            For Each cc As Trinity.cCombinationChannel In combo.Relations
                If ID Is "" Then
                    ID = cc.Bookingtype.Indexes.Add(LastIndex.Name).ID.ToString
                Else
                    cc.Bookingtype.Indexes.Add(LastIndex.Name, ID)
                End If

                cc.Bookingtype.Indexes(ID).FromDate = LastIndex.FromDate
                cc.Bookingtype.Indexes(ID).ToDate = LastIndex.ToDate
            Next
        End If

        cmbIndexChannel_SelectedIndexChanged(New Object, New EventArgs)
        grdIndexes.Rows(grdIndexes.Rows.Count - 1).Tag = ID
    End Sub

    Private Sub grdChannels_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdChannels.CellContentClick

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _activeCampaign = Campaign

    End Sub

    Public Sub New(ByVal Camp As Trinity.cKampanj)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _activeCampaign = Camp

    End Sub

    Private Sub lblContract_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblContract.MouseHover
        'If Campaign.Contract IsNot Nothing AndAlso Campaign.Contract.Name IsNot Nothing Then
        '    Dim TT As New ToolTip
        '    TT.Show(Campaign.Contract.Name, Me, MousePosition.)
        'End If
    End Sub

    Private Sub tmrKeyPressTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrKeyPressTimer.Tick
        Saved = False
        tmrKeyPressTimer.Enabled = False
        'if its a one digit only we skip this (all lengths should be 2 digits, just in case we catch changes i nthe leave event)
        If txtFilmLength.Text.Length < 1 Then Exit Sub

        'if it hasnt changed we dont do anything
        If txtFilmLength.Text = txtFilmLength.Tag Then Exit Sub

        If changeText = True Then
            Dim TmpChan As Trinity.cChannel
            Dim TmpBT As Trinity.cBookingType
            Dim TmpWeek As Trinity.cWeek
            Dim Film As String

            ''check if there are specifics spots and films, if so we give a warning
            If ActiveCampaign.BookedSpots.Count > 0 Then
                For Each row As Windows.Forms.DataGridViewRow In grdFilmDetails.Rows
                    If DirectCast(row.Tag, Trinity.cBookingType).IsSpecific Then
                        If MsgBox("Changing the film length will affect prices on booked spots. Continue?", MsgBoxStyle.OkCancel, "Warning!") = MsgBoxResult.Cancel Then
                            txtFilmLength.Text = txtFilmLength.Tag
                            Exit Sub
                        End If
                        Exit For
                    End If
                Next
            End If


            txtFilmLength.Tag = txtFilmLength.Text


            If grdFilms.SelectedRows.Count = 0 Then Exit Sub
            Film = grdFilms.SelectedRows.Item(0).Cells(0).Value

            For Each TmpChan In ActiveCampaign.Channels
                For Each TmpBT In TmpChan.BookingTypes
                    For Each TmpWeek In TmpBT.Weeks
                        Try
                            TmpWeek.Films(Film).FilmLength = Val(txtFilmLength.Text)
                        Catch

                        End Try
                    Next
                Next
            Next
            grdFilms.SelectedRows.Item(0).Cells(3).Value = txtFilmLength.Text

            If chkFilmAutoIndex.Checked AndAlso Val(txtFilmLength.Text) > 0 Then

                'change the index on the booked spots
                For Each spot As Trinity.cBookedSpot In ActiveCampaign.BookedSpots
                    If spot.Film.Name = txtFilmName.Text Then
                        spot.setIndexOnFilm(ActiveCampaign.Channels(1).BookingTypes(1).FilmIndex(txtFilmLength.Text))
                    End If
                Next

                For Each TmpChan In ActiveCampaign.Channels
                    For Each TmpBT In TmpChan.BookingTypes
                        For Each TmpWeek In TmpBT.Weeks
                            'If there is a contract, and the contract contains this channel
                            'and the booking type at the active level is not nothing
                            If ActiveCampaign.Contract IsNot Nothing AndAlso ActiveCampaign.Contract.Channels(TmpChan.ChannelName) IsNot Nothing AndAlso ActiveCampaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(ActiveCampaign.Contract.Channels(TmpChan.ChannelName).ActiveContractLevel)(TmpBT.Name) IsNot Nothing AndAlso ActiveCampaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(ActiveCampaign.Contract.Channels(TmpChan.ChannelName).ActiveContractLevel) IsNot Nothing Then
                                'Then if the film index for this film length is specified in the contract for this booking type, set it according to what it is
                                If ActiveCampaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(ActiveCampaign.Contract.Channels(TmpChan.ChannelName).ActiveContractLevel)(TmpBT.Name).FilmIndex(txtFilmLength.Text) <> 0 Then
                                    TmpWeek.Films(txtFilmName.Text).Index = ActiveCampaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(ActiveCampaign.Contract.Channels(TmpChan.ChannelName).ActiveContractLevel)(TmpBT.Name).FilmIndex(txtFilmLength.Text)
                                    TmpWeek.Films(txtFilmName.Text).GrossIndex = ActiveCampaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(ActiveCampaign.Contract.Channels(TmpChan.ChannelName).ActiveContractLevel)(TmpBT.Name).FilmIndex(txtFilmLength.Text)
                                ElseIf ActiveCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).FilmIndex(txtFilmLength.Text) <> 0 Then
                                    TmpWeek.Films(txtFilmName.Text).Index = ActiveCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).FilmIndex(txtFilmLength.Text)
                                    TmpWeek.Films(txtFilmName.Text).GrossIndex = ActiveCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).FilmIndex(txtFilmLength.Text)
                                ElseIf ActiveCampaign.Channels.DefaultFilmIndex(txtFilmLength.Text) > 0 Then
                                    TmpWeek.Films(txtFilmName.Text).Index = ActiveCampaign.Channels.DefaultFilmIndex(txtFilmLength.Text)
                                    TmpWeek.Films(txtFilmName.Text).GrossIndex = ActiveCampaign.Channels.DefaultFilmIndex(txtFilmLength.Text)
                                Else
                                    TmpWeek.Films(txtFilmName.Text).Index = ActiveCampaign.Channels.DefaultFilmIndex(txtFilmLength.Text)
                                    TmpWeek.Films(txtFilmName.Text).GrossIndex = ActiveCampaign.Channels.DefaultFilmIndex(txtFilmLength.Text)
                                End If
                            Else
                                'If there is no contract or this booking type does not have a particular index, set it according to what the campaign says it should be
                                If ActiveCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).FilmIndex(txtFilmLength.Text) <> 0 Then
                                    TmpWeek.Films(txtFilmName.Text).Index = ActiveCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).FilmIndex(txtFilmLength.Text)
                                    TmpWeek.Films(txtFilmName.Text).GrossIndex = ActiveCampaign.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).FilmIndex(txtFilmLength.Text)
                                Else
                                    TmpWeek.Films(txtFilmName.Text).Index = ActiveCampaign.Channels.DefaultFilmIndex(txtFilmLength.Text)
                                    TmpWeek.Films(txtFilmName.Text).GrossIndex = ActiveCampaign.Channels.DefaultFilmIndex(txtFilmLength.Text)
                                End If
                            End If
                        Next
                    Next
                Next

            End If
            'We don't update grdFilmDetails directly, but the underlying data, so an invalidate is enough
            grdFilmDetails.Invalidate()

        End If
        changeText = False

        ' tmrKeyPressTimer.Enabled = False
    End Sub

    Private Sub mnuUseActualTRPs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUseActualTRPs.Click
        Dim _comb As Trinity.cCombination = grpCombo.Tag
        Dim _total As Single = 0
        For Each _row As DataGridViewRow In grdCombo.Rows
            Dim TmpCC As Trinity.cCombinationChannel = _row.Tag
            For Each _week As Trinity.cWeek In TmpCC.Bookingtype.Weeks
                Select Case _comb.CombinationOn
                    Case Trinity.cCombination.CombinationOnEnum.coBudget
                        _total += _week.NetBudget
                    Case Trinity.cCombination.CombinationOnEnum.coTRP
                        _total += _week.TRPBuyingTarget
                End Select
            Next
        Next
        Dim _totTRPs As Single = 0
        For Each _row As DataGridViewRow In grdCombo.Rows
            Dim TmpCC As Trinity.cCombinationChannel = _row.Tag
            Dim TRPs As Single = (From _spot As Trinity.cActualSpot In ActiveCampaign.ActualSpots Select _spot Where _spot.Bookingtype Is TmpCC.Bookingtype).Sum(Function(_s) _s.Rating30(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget))
            TmpCC.Relation = TRPs
            _totTRPs += TRPs
        Next
        For Each _row As DataGridViewRow In grdCombo.Rows
            Dim TmpCC As Trinity.cCombinationChannel = _row.Tag
            Dim _totalBudget As Decimal = TmpCC.Bookingtype.PlannedNetBudget
            Dim _totalTRP As Decimal = TmpCC.Bookingtype.PlannedTRP30(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
            For Each _week As Trinity.cWeek In TmpCC.Bookingtype.Weeks
                Select Case _comb.CombinationOn
                    Case Trinity.cCombination.CombinationOnEnum.coBudget
                        If _totalBudget > 0 AndAlso _totTRPs > 0 Then
                            _week.NetBudget = _total * (TmpCC.Relation / _totTRPs) * (_week.NetBudget / _totalBudget)
                        Else
                            _week.NetBudget = 0
                        End If
                    Case Trinity.cCombination.CombinationOnEnum.coTRP
                        If _totTRPs > 0 AndAlso _totalTRP > 0 Then
                            _week.TRPBuyingTarget = _total * (TmpCC.Relation / _totTRPs) * (_week.TRPBuyingTarget / _totalTRP)
                        Else
                            _week.TRPBuyingTarget = 0
                        End If
                End Select
            Next
        Next
        grdCombo.Invalidate()

    End Sub

    Private Sub cmdQuickAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuickAdd.Click

        'Campaign.ChannelBundles.Read()

        Dim tmpMenu As New ContextMenuStrip()
        'Dim tmpTargets As New ContextMenuStrip

        Dim targetList As List(Of String) = Nothing
        If Campaign.ChannelBundles IsNot Nothing Then
            For Each Bundle As KeyValuePair(Of String, List(Of Trinity.cPricelistTarget)) In Campaign.ChannelBundles
                Dim tmpMenuItem As New ToolStripMenuItem
                tmpMenuItem.Text = Bundle.Key

                'If targetList Is Nothing Then
                '    targetList = New List(Of String)
                '    For Each tmpBT As Trinity.cBookingType In Bundle.Value
                '        targetList.AddRange(tmpBT.Pricelist.Targets.TargetNameList)
                '    Next
                '    targetList = (From tg As String In targetList Where tg.Count = Bundle.Value.Count Select tg).ToList
                'End If
                tmpMenuItem.Tag = DirectCast(Bundle.Value, List(Of Trinity.cPricelistTarget))
                AddHandler tmpMenuItem.Click, AddressOf QuickAdd
                tmpMenu.Items.Add(tmpMenuItem)

            Next
        End If
        tmpMenu.Items.Add("-")
        tmpMenu.Items.Add("Edit packages", Nothing, AddressOf EditPackages)
        tmpMenu.Show(cmdQuickAdd, New System.Drawing.Point(0, cmdQuickAdd.Height))
    End Sub

    Sub EditPackages(ByVal sender As Object, ByVal e As EventArgs)
        frmManageBundles.ShowDialog()
    End Sub

    Private Sub QuickAdd(ByVal sender As Object, ByVal e As EventArgs)

        Dim targetList As List(Of Trinity.cPricelistTarget) = sender.tag

        'For i As Integer = 1 To BTList.Count
        '    DirectCast(grdChannels.Rows(grdChannels.Rows.Count - 1).Cells , ExtendedComboBoxCell).Items.add(Campaign.Channels(BTList .key).BookingTypes(BTList .Value))
        'Next
        For Each tmpTarget As Trinity.cPricelistTarget In targetList

            If Not tmpTarget.Bookingtype.BookIt Then
                grdChannels.Rows.Add()
                DirectCast(grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(0), ExtendedComboBoxCell).Items.add(tmpTarget.Bookingtype)
                DirectCast(grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(0), ExtendedComboBoxCell).Value = tmpTarget.Bookingtype
                tmpTarget.Bookingtype.BookIt = True
                DirectCast(grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(1), ExtendedComboBoxCell).Value = tmpTarget.TargetName
            End If

        Next
    End Sub

    Private Sub mnuOpenContract_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOpenContract.Click
        If TrinitySettings.SaveCampaignsAsFiles Then
            OpenContractFromFile()
        Else
            OpenContractFromDB()
        End If
    End Sub

    Private Sub tpChannels_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpChannels.Enter

        If Campaign.Contract IsNot Nothing AndAlso Campaign.Contract.ChannelPackage IsNot Nothing Then

            Dim targetList As List(Of Trinity.cPricelistTarget) = Campaign.ChannelBundles.GetBundle(Campaign.Contract.ChannelPackage)

            If targetList IsNot Nothing Then
                For Each tmpTarget As Trinity.cPricelistTarget In targetList
                    If Not tmpTarget.Bookingtype.BookIt Then
                        grdChannels.Rows.Add()
                        DirectCast(grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(0), ExtendedComboBoxCell).Items.add(tmpTarget.Bookingtype)
                        DirectCast(grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(0), ExtendedComboBoxCell).Value = tmpTarget.Bookingtype
                        DirectCast(grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(1), ExtendedComboBoxCell
                            ).Value = tmpTarget.TargetName
                        tmpTarget.Bookingtype.BookIt = True
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub grdCombo_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCombo.CellContentClick

    End Sub

    Private Sub chkPrintAsOne_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkPrintAsOne.CheckedChanged
        Dim TmpCombo As Trinity.cCombination = grpCombo.Tag
        If TmpCombo Is Nothing Then Exit Sub

        TmpCombo.PrintAsOne = chkPrintAsOne.Checked
    End Sub
    

    Private Sub cmdAddChannelWizard_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddChannelWizard.Click
        If ActiveCampaign.IsStripped AndAlso Windows.Forms.MessageBox.Show("Unused channels has been removed from this campaign." & vbCrLf & vbCrLf & "Do you want to reload all channels now?", "T R I N I T Y", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            ActiveCampaign.ReloadDeletedChannels()
            LongName.Clear()
            For i As Integer = 1 To Campaign.Channels.Count
                LongName.Add(Campaign.Channels(i).Shortname, Campaign.Channels(i).ChannelName)
            Next
        End If
        Dim _frm = New frmAddChannelWizard(ActiveCampaign)
        _frm.ShowDialog()
        UpdateChannelGrid()
        UpdateChannelInfoGrid(True)
    End Sub

    Private Sub disableContract(sender As Object, e As EventArgs) Handles mnuNoContract.Click

    End Sub

    Private Sub mnuFF36810MTVCC_Click(sender As Object, e As EventArgs) Handles mnuFF36810MTVCC.Click
        Dim tmpListOfCombinationChannels As New List(Of String)
        For Each TmpRow As DataGridViewRow In grdCombo.Rows            
            Dim TmpCC As Trinity.cCombinationChannel = TmpRow.Tag
            tmpListOfCombinationChannels.Add(TmpCC.ChannelName)
        Next

        Dim FFAll As Boolean = False
        'FF 3-6-8-10-MTV-CC
        If tmpListOfCombinationChannels.Contains("TV3") And tmpListOfCombinationChannels.Contains("TV6") And tmpListOfCombinationChannels.Contains("TV8") And tmpListOfCombinationChannels.Contains("TV10") And tmpListOfCombinationChannels.Contains("MTV") And tmpListOfCombinationChannels.Contains("Comedy Central")
            FFAll = True   
        End If

        If FFAll
            Dim xmldoc As New XmlDataDocument()
            Dim xmlnode As XmlNodeList
            Dim listTargets As new List(Of Object)
            Dim str As String = ""
            Dim fs As New FileStream(TrinitySettings.ActiveDataPath & "3-6-8-10-MTV-CC.xml", FileMode.Open, FileAccess.Read)
            xmldoc.Load(fs)
            xmlnode = xmldoc.GetElementsByTagName("Target")
            For i As Integer = 0 To xmlnode.Count - 1
                Dim newtarget As New Object
                Dim testAttri = xmlnode(i).SelectSingleNode("//Target")
                Dim targetName = xmlnode(i).ChildNodes.Item(0).InnerText
                Dim split As Integer
                For x As Integer = 0 To xmlnode(i).ChildNodes.Count - 1
                    split = Integer.TryParse(xmlnode(i).ChildNodes.Item(x).InnerText, split)
                Next

                'newtarget.TargetName = 
            Next
        End If
        'TmpCC.Relation = Math.Round(CalculateNDCombo(TmpCC.Bookingtype, RefPeriod.RefPeriodLastWeek), 0)
    End Sub

    Private Sub mnuCalculcateComboND_Opening(sender As Object, e As CancelEventArgs) Handles mnuCalculcateComboND.Opening
        If TrinitySettings.DefaultArea = "SE"
            UseMTGChannelSplitToolStripMenuItem.Visible = True
        End If
    End Sub

    Private Sub txtMarathonIDCombo_TextChanged(sender As Object, e As EventArgs) Handles txtMarathonIDCombo.TextChanged
        Dim selectedCombo As Trinity.cCombination
        For each row As DataGridViewrow In grdcombos.Rows
            If row.Selected = True
                selectedCombo = row.Tag()
            End If
        Next
        If selectedCombo isnot Nothing
            selectedCombo.MarathonIDCombination = txtMarathonIDCombo.Text
        End If
    End Sub

    Private Sub txtMarathonIDCombo_Enter(sender As Object, e As EventArgs) Handles txtMarathonIDCombo.Enter
        
        Dim selectedCombo As Trinity.cCombination
        For each row As DataGridViewrow In grdcombos.Rows
            If row.Selected = True
                selectedCombo = row.Tag()
            End If
        Next
        If selectedCombo isnot Nothing
            selectedCombo.MarathonIDCombination = txtMarathonIDCombo.Text
        End If
    End Sub

    Private Sub chkSendAsUnitMarathon_CheckedChanged(sender As Object, e As EventArgs) Handles chkSendAsUnitMarathon.CheckedChanged
        Dim selectedCombo As Trinity.cCombination
        For each row As DataGridViewrow In grdcombos.Rows
            If row.Selected = True
                selectedCombo = row.Tag()
            End If
        Next
        If selectedCombo isnot Nothing
            If chkSendAsUnitMarathon.Checked
                selectedCombo.sendAsOneUnitTOMarathon = True
            Else                
                selectedCombo.sendAsOneUnitTOMarathon = False
            End If
        End If
    End Sub

    Private Sub txtName_Validating(sender As Object, e As CancelEventArgs) Handles txtName.Validating

    End Sub

    Private Sub txtComboName_Validated(sender As Object, e As EventArgs) Handles txtComboName.Validated

    End Sub

    Private Sub tpGeneral_Click(sender As Object, e As EventArgs) Handles tpGeneral.Click

    End Sub

    Private Sub grdIndexes_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdIndexes.CellContentClick

    End Sub
End Class

