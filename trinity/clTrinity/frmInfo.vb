Imports System.Drawing

Public Class frmInfo
    Private Class CBItem

        Private _text As String
        Public Property Text() As String
            Get
                Return _text
            End Get
            Set(ByVal value As String)
                _text = value
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
    Private Sub picPin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picPin.Click
        'pins the window
        TrinitySettings.ShowInfoInWindow = False
        frmMain.pnlInfo.Visible = True
        Me.Dispose()
    End Sub

    Private Sub frmInfo_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        Dim i As Integer

        'clears and populates the target options (text fields and comboboxes)
        cmbMainUni.Items.Clear()
        cmbSecondUni.Items.Clear()
        cmbThirdUni.Items.Clear()

        For i = 0 To Campaign.Universes.Count - 1
            cmbMainUni.Items.Add(Campaign.Universes(i))
            cmbSecondUni.Items.Add(Campaign.Universes(i))
            cmbThirdUni.Items.Add(Campaign.Universes(i))
        Next
        If Campaign.MainTarget.Universe = "" Then
            cmbMainUni.SelectedItem = Campaign.Universes(0)
        Else
            cmbMainUni.Text = Campaign.Universes(Campaign.MainTarget.Universe)
        End If
        If Campaign.SecondaryTarget.Universe = "" Then
            cmbSecondUni.SelectedItem = Campaign.Universes(0)
        Else
            cmbSecondUni.Text = Campaign.Universes(Campaign.SecondaryTarget.Universe)
        End If
        If Campaign.ThirdTarget.Universe = "" Then
            cmbThirdUni.SelectedItem = Campaign.Universes(0)
        Else
            cmbThirdUni.Text = Campaign.Universes(Campaign.ThirdTarget.Universe)
        End If


        'updates the text fields with the campaign targets and campaign name
        txtMain.Text = Campaign.MainTarget.TargetName
        txtSec.Text = Campaign.SecondaryTarget.TargetName
        txtThird.Text = Campaign.ThirdTarget.TargetName
        txtName.Text = Campaign.Name

        'check when it was last updated and sets labels
        If Campaign.UpdatedTo >= Campaign.StartDate Then
            lblUpdatedTo.Text = "Updated to: " & Format(Date.FromOADate(Campaign.UpdatedTo), "Short date")
        Else
            lblUpdatedTo.Text = "Updated to: -"
        End If
        If Not Campaign.RootCampaign Is Nothing Then
            For Each ctrl As Windows.Forms.Control In pnlInfo.Controls
                If Not ctrl.Name = "picPin" Then
                    ctrl.Enabled = False
                Else
                    ctrl.Enabled = True
                End If
            Next
        Else
            For Each ctrl As Windows.Forms.Control In Me.Controls
                ctrl.Enabled = True
            Next
        End If

        'sets the Frequency Focus combo box
        cmbFF.SelectedIndex = Campaign.FrequencyFocus
    End Sub

    Private Sub frmInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim i As Integer

        'populates the Client combo box
        PopulateClientCombo()

        'sets the Frequency Focus combo box
        cmbFF.SelectedIndex = Campaign.FrequencyFocus

        If cmbTarget.SelectedIndex = -1 Then
            cmbTarget.SelectedIndex = 0
        End If

        'enables writing in the grid and then clears and repopulate it
        grdReach.ReadOnly = False
        grdReach.Rows.Clear()
        grdReach.Rows.Add(10)
        For i = 1 To 10
            '            grdReach.Rows(i).Cells(0).Value = Format(Campaign.ReachTargets(i, "Nat"), "0.0")
            grdReach.AutoResizeRow(i - 1, Windows.Forms.DataGridViewAutoSizeRowMode.AllCells)
        Next
        For i = 1 To 10
            Dim lblFreq As New System.Windows.Forms.Label
            lblFreq.Text = i & "+"
            lblFreq.AutoSize = True
            lblFreq.Top = grdReach.GetRowDisplayRectangle(i - 1, True).Top + grdReach.Top
            lblFreq.Height = grdReach.GetRowDisplayRectangle(i - 1, True).Height
            lblFreq.TextAlign = Drawing.ContentAlignment.MiddleLeft
            lblFreq.Left = 6
            pnlInfo.Controls.Add(lblFreq)
            grdReach.Rows(i - 1).Cells(0).ReadOnly = False
            grdReach.Rows(i - 1).Cells(1).ReadOnly = True
        Next
    End Sub

    Private Sub txtMain_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMain.KeyUp
        'sets the "is saved" status to flase when main target is altered
        Saved = False
    End Sub

    Private Sub txtSec_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSec.KeyUp
        'sets the "is saved" status to flase when second target is altered
        Saved = False
    End Sub

    Private Sub txtThird_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtThird.KeyUp
        'sets the "is saved" status to flase when third target is altered
        Saved = False
    End Sub

    Private Sub txtName_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        'if campaign name is altered the campaign is in "not saved" status, and updates are applyed to the campaign and the main window 
        Campaign.Name = txtName.Text
        Saved = False
        If Campaign.Name <> "" Then
            frmMain.Text = "T R I N I T Y   4.0  -  " & Campaign.Name
        Else
            frmMain.Text = "T R I N I T Y   4.0"
        End If
    End Sub

    Private Sub cmbClient_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClient.SelectedIndexChanged
        Saved = False
        'populates the product combo box depending on what client is selected
        PopulateProductCombo()
    End Sub

    Private Sub cmbProduct_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProduct.SelectedIndexChanged
        Saved = False
        'sets product ID depending on what client and product is selected
        Campaign.ProductID = DirectCast(cmbProduct.SelectedItem, CBItem).Tag
    End Sub

    Private Sub cmdEditClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditClient.Click
        Saved = False
        'enables the dialog for client editing
        frmAddClient.Tag = "EDIT" 'the form will check for the tag
        frmAddClient.ShowDialog() ' shows the add cliend dialog
        'repopulates the combo box after the changes
        PopulateClientCombo()
    End Sub

    Private Sub cmdAddProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddProduct.Click
        Saved = False
        'enables the add product dialog
        frmAddProduct.Dispose()
        frmAddProduct.Tag = "" 'empty the tag (tag is used for editing)
        frmAddProduct.ShowDialog() 'shows the add product dialog
        If frmAddProduct.txtName.Text <> "" Then
            'repopulate the product combo box
            PopulateProductCombo()
        End If
    End Sub

    Private Sub cmdEditProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditProduct.Click
        Saved = False
        'enables the edit product dialog (same as add product)
        If cmbClient.SelectedItem Is Nothing Then
            Exit Sub
        End If
        frmAddProduct.Tag = "EDIT" 'the form will check for the tag
        frmAddProduct.ShowDialog() ' shows the add product form
        'repopulate the product combo box after the changes
        PopulateProductCombo()
    End Sub

    Sub PopulateClientCombo()
        'populates the combobox from the database
        Dim clients As DataTable = DBReader.getAllClients()

        'Dim com As New Odbc.OdbcCommand("SELECT * FROM Clients", DBConn)
        'Dim rd As Odbc.OdbcDataReader = com.ExecuteReader

        'clear the combo box
        cmbClient.Items.Clear()
        cmbClient.DisplayMember = "Text"

        For Each dr As DataRow In clients.Rows
            Dim TmpItem As New CBItem 'CBItem is a class defined it the beginning of this file
            TmpItem.Text = dr("name")
            TmpItem.Tag = dr("id")
            cmbClient.Items.Add(TmpItem)
            If dr("id") = Campaign.ClientID Then
                cmbClient.Text = dr("name")
            End If
        Next

        'While rd.Read
        '    Dim TmpItem As New CBItem 'CBItem is a class defined it the beginning of this file
        '    TmpItem.Text = rd!name
        '    TmpItem.Tag = rd!id
        '    cmbClient.Items.Add(TmpItem)
        '    If rd!ID = Campaign.ClientID Then
        '        cmbClient.Text = rd!Name
        '    End If
        'End While
        'rd.Close()
    End Sub


    Sub PopulateProductCombo()
        'populates the combobox from the database
        cmbProduct.Items.Clear()
        If cmbClient.SelectedItem Is Nothing Then
            Exit Sub
        End If

        Campaign.ClientID = DirectCast(cmbClient.SelectedItem, CBItem).Tag
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

        'Dim com As New Odbc.OdbcCommand("SELECT id,Name FROM Products WHERE ClientID=" & DirectCast(cmbClient.SelectedItem, CBItem).Tag, DBConn)
        'Dim rd As Odbc.OdbcDataReader = com.ExecuteReader

        'Campaign.ClientID = DirectCast(cmbClient.SelectedItem, CBItem).Tag
        'cmbProduct.Items.Clear()
        'cmbProduct.DisplayMember = "Text"
        'While rd.Read
        '    Dim TmpItem As New CBItem
        '    TmpItem.Text = rd!Name
        '    TmpItem.Tag = rd!id
        '    cmbProduct.Items.Add(TmpItem)
        '    If rd!ID = Campaign.ProductID Then
        '        cmbProduct.Text = rd!Name
        '    End If
        'End While
    End Sub

    Private Sub cmdCalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCalculate.Click
        Saved = False
        Dim mnuCalculate As New System.Windows.Forms.ContextMenuStrip

        mnuCalculate.Items.Add("Use last weeks of data", Nothing, AddressOf mnuUseLastWeeks_Click)
        mnuCalculate.Items.Add("Use same period last year", Nothing, AddressOf mnuUseSamePeriod_Click)
        mnuCalculate.Items.Add("Create custom period", Nothing, AddressOf mnuUseCustomPeriod_Click)
        mnuCalculate.Show(cmdCalculate, 0, cmdCalculate.Height)

    End Sub

    Private Sub mnuUseLastWeeks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If Windows.Forms.MessageBox.Show("Använd gamla?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        '    mnuUseLastWeeks_Click_oldKarma(sender, e)
        '    Exit Sub
        'End If
        Dim Periodstr As String = ""
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim IsUsed As Boolean
        Dim i As Integer
        Dim TmpStr As String
        Dim UseSponsorship As Boolean = False
        Dim UseCommercial As Boolean = False

        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        'Periodstr = "-" & Trim(Campaign.Channels(1).BookingTypes(1).Weeks.Count) & "fw"

        Dim Karma As New Trinity.cKarma(Campaign)
        Dim TmpDate As Long = Campaign.EndDate
        Dim DateDiff As Long

        While TmpDate >= Karma.KarmaAdedge.getDataRangeTo(Connect.eDataType.mSpot)
            TmpDate = TmpDate - 1
        End While
        DateDiff = Campaign.EndDate - TmpDate

        For Each TmpWeek As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
            Periodstr &= Format(Date.FromOADate(TmpWeek.StartDate - DateDiff), "ddMMyy") & "-" & Format(Date.FromOADate(TmpWeek.EndDate - DateDiff), "ddMMyy") & ","
        Next
        Karma.ReferencePeriod = Periodstr

        For Each TmpChan In Campaign.Channels
            IsUsed = False
            For Each TmpBT In TmpChan.BookingTypes
                If TmpBT.BookIt Then
                    IsUsed = True
                End If
                If TmpBT.IsSponsorship Then
                    UseSponsorship = True
                ElseIf TmpBT.BookIt Then
                    UseCommercial = True
                End If
            Next
            If IsUsed Then
                Karma.Channels.Add(TmpChan.ChannelName)
                Karma.Weeks = TmpChan.BookingTypes(1).Weeks.Count
            End If
        Next
        frmProgress.Status = "Fetching spots..."
        frmProgress.Progress = 0
        frmProgress.Show()
        AddHandler Karma.PopulateProgress, AddressOf Progress
        Karma.Populate(UseSponsorship, UseCommercial)

        TmpStr = Campaign.Name
        If TmpStr Is Nothing Then TmpStr = ""
        Karma.Campaigns.Add(TmpStr, Campaign)
        frmProgress.Status = "Calculating reach for " & TmpStr
        frmProgress.Progress = 0
        frmProgress.Show()
        AddHandler Karma.Campaigns(TmpStr).Progress, AddressOf Progress
        Karma.Campaigns(TmpStr).Run()
        frmProgress.Hide()
        For i = 1 To 10
            Campaign.ReachGoal(i) = Karma.Campaigns.Item(TmpStr).Reach(0, i)
            Campaign.ReachGoal(i, Trinity.cKampanj.ReachTargetEnum.rteSecondTarget) = Karma.Campaigns.Item(TmpStr).Reach(0, i, Trinity.cKampanj.ReachTargetEnum.rteSecondTarget)
        Next

        'Too Time consuming

        'Dim t As Single = Timer
        'For Each TmpWeek As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
        '    Dim TRPSum As Single = 0
        '    For Each TmpChan In Campaign.Channels
        '        For Each TmpBT In TmpChan.BookingTypes
        '            TRPSum += TmpBT.Weeks(TmpWeek.Name).TRP
        '        Next
        '    Next
        '    Campaign.EstimatedWeeklyReach(TmpWeek.Name) = Karma.Campaigns(TmpStr).GetReachAtTRP(TRPSum, 1)
        'Next
        't = Timer - t
        frmInfo_Activated(New Object, New EventArgs)
        Me.Cursor = Windows.Forms.Cursors.Default
        grdReach.Invalidate()
    End Sub

    'Private Sub mnuUseLastWeeks_Click_oldKarma(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim Periodstr As String = ""
    '    Dim TmpChan As Trinity.cChannel
    '    Dim TmpBT As Trinity.cBookingType
    '    Dim IsUsed As Boolean
    '    Dim i As Integer
    '    Dim TmpStr As String
    '    Dim UseSponsorship As Boolean = False

    '    Me.Cursor = Windows.Forms.Cursors.WaitCursor
    '    'Periodstr = "-" & Trim(Campaign.Channels(1).BookingTypes(1).Weeks.Count) & "fw"

    '    Dim Karma As New Trinity._cKarma
    '    Dim TmpDate As Long = Campaign.EndDate
    '    Dim DateDiff As Long

    '    While TmpDate >= Karma.KarmaAdedge.getDataRangeTo(Connect.eDataType.mSpot)
    '        TmpDate = TmpDate - 1
    '    End While
    '    DateDiff = Campaign.EndDate - TmpDate

    '    For Each TmpWeek As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
    '        Periodstr &= Format(Date.FromOADate(TmpWeek.StartDate - DateDiff), "ddMMyy") & "-" & Format(Date.FromOADate(TmpWeek.EndDate - DateDiff), "ddMMyy") & ","
    '    Next
    '    Karma.ReferencePeriod = Periodstr

    '    For Each TmpChan In Campaign.Channels
    '        IsUsed = False
    '        For Each TmpBT In TmpChan.BookingTypes
    '            If TmpBT.BookIt Then
    '                IsUsed = True
    '            End If
    '            If TmpBT.IsSponsorship Then
    '                UseSponsorship = True
    '            End If
    '        Next
    '        If IsUsed Then
    '            Karma.Channels.Add(TmpChan.ChannelName)
    '            For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
    '                With Karma.Channels(TmpChan.ChannelName).Weeks.Add()
    '                    For dp As Integer = 0 To Campaign.Dayparts.Count - 1
    '                        .Dayparts.Add()
    '                    Next
    '                End With
    '            Next
    '            'Karma.Weeks = TmpChan.BookingTypes(1).Weeks.Count
    '        End If
    '    Next
    '    frmProgress.Status = "Fetching spots..."
    '    frmProgress.Progress = 0
    '    frmProgress.Show()
    '    AddHandler Karma.PopulateProgress, AddressOf Progress
    '    Karma.Populate(UseSponsorship)

    '    TmpStr = Campaign.Name
    '    If TmpStr Is Nothing Then TmpStr = ""
    '    Karma.Campaigns.Add(TmpStr, Campaign)
    '    frmProgress.Status = "Calculating reach for " & TmpStr
    '    frmProgress.Progress = 0
    '    frmProgress.Show()
    '    AddHandler Karma.Campaigns(TmpStr).Progress, AddressOf Progress
    '    Karma.Campaigns(TmpStr).Run()
    '    frmProgress.Hide()
    '    For i = 1 To 10
    '        Campaign.ReachGoal(i) = Karma.Campaigns.Item(TmpStr).Reach(0, i)
    '        Campaign.ReachGoal(i, Trinity.cKampanj.ReachTargetEnum.rteSecondTarget) = Karma.Campaigns.Item(TmpStr).Reach(0, i, Trinity.cKampanj.ReachTargetEnum.rteSecondTarget)
    '    Next

    '    'Too Time consuming

    '    'Dim t As Single = Timer
    '    'For Each TmpWeek As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
    '    '    Dim TRPSum As Single = 0
    '    '    For Each TmpChan In Campaign.Channels
    '    '        For Each TmpBT In TmpChan.BookingTypes
    '    '            TRPSum += TmpBT.Weeks(TmpWeek.Name).TRP
    '    '        Next
    '    '    Next
    '    '    Campaign.EstimatedWeeklyReach(TmpWeek.Name) = Karma.Campaigns(TmpStr).GetReachAtTRP(TRPSum, 1)
    '    'Next
    '    't = Timer - t
    '    frmInfo_Activated(New Object, New EventArgs)
    '    Me.Cursor = Windows.Forms.Cursors.Default
    '    grdReach.Invalidate()
    'End Sub

    Sub Progress(ByVal p As Single)
        frmProgress.Progress = p
    End Sub

    Private Sub mnuUseSamePeriod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Periodstr As String = ""
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim IsUsed As Boolean
        Dim i As Integer
        Dim TmpStr As String
        Dim UseSponsorship As Boolean = False
        Dim UseCommercial As Boolean = False

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        Dim Karma As New Trinity.cKarma(Campaign)
        Dim TmpDate As Long = Date.FromOADate(Campaign.EndDate).AddYears(-1).ToOADate
        Dim DateDiff As Long

        While Weekday(Date.FromOADate(TmpDate), FirstDayOfWeek.Monday) <> Weekday(Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday)
            TmpDate = TmpDate + 1
        End While
        DateDiff = Campaign.EndDate - TmpDate

        For Each TmpWeek As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
            Periodstr &= Format(Date.FromOADate(TmpWeek.StartDate - DateDiff), "ddMMyy") & "-" & Format(Date.FromOADate(TmpWeek.EndDate - DateDiff), "ddMMyy") & ","
        Next
        Karma.ReferencePeriod = Periodstr

        For Each TmpChan In Campaign.Channels
            IsUsed = False
            For Each TmpBT In TmpChan.BookingTypes                
                If TmpBT.BookIt Then
                    IsUsed = True
                End If
                If TmpBT.IsSponsorship Then
                    UseSponsorship = True
                ElseIf TmpBT.BookIt Then
                    UseCommercial = True
                End If
            Next
            If IsUsed Then
                Karma.Channels.Add(TmpChan.ChannelName)
                Karma.Weeks = TmpChan.BookingTypes(1).Weeks.Count
            End If
        Next
        frmProgress.Status = "Fetching spots..."
        frmProgress.Progress = 0
        frmProgress.Show()
        AddHandler Karma.PopulateProgress, AddressOf Progress
        Karma.Populate(UseSponsorship, UseCommercial)

        TmpStr = Campaign.Name
        Karma.Campaigns.Add(TmpStr, Campaign)
        frmProgress.Status = "Calculating reach for " & TmpStr
        frmProgress.Progress = 0
        frmProgress.Show()
        AddHandler Karma.Campaigns(TmpStr).Progress, AddressOf Progress
        Karma.Campaigns(TmpStr).Run()
        frmProgress.Hide()

        For i = 1 To 10
            Campaign.ReachGoal(i) = Karma.Campaigns.Item(TmpStr).Reach(0, i)
            Campaign.ReachGoal(i, Trinity.cKampanj.ReachTargetEnum.rteSecondTarget) = Karma.Campaigns.Item(TmpStr).Reach(0, i, Trinity.cKampanj.ReachTargetEnum.rteSecondTarget)
        Next

        frmInfo_Activated(New Object, New EventArgs)
        Me.Cursor = Windows.Forms.Cursors.Default
        grdReach.Invalidate()
    End Sub

    Private Sub mnuUseCustomPeriod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Periodstr As String
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim IsUsed As Boolean
        Dim i As Integer
        Dim TmpStr As String
        Dim UseSponsorship As Boolean = False
        Dim UseCommercial As Boolean = False

        frmDates.ShowDialog()
        If frmDates.DialogResult = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        Periodstr = Format(frmDates.dateFrom.Value, "ddMMyy") & "-" & Format(frmDates.dateTo.Value, "ddMMyy")

        Dim Karma As New Trinity.cKarma(Campaign)
        Karma.ReferencePeriod = Periodstr

        For Each TmpChan In Campaign.Channels
            IsUsed = False
            For Each TmpBT In TmpChan.BookingTypes
                If TmpBT.BookIt Then
                    IsUsed = True
                End If
                If TmpBT.IsSponsorship Then
                    UseSponsorship = True
                ElseIf TmpBT.BookIt Then
                    UseCommercial = True
                End If
            Next
            If IsUsed Then
                Karma.Channels.Add(TmpChan.ChannelName)
                Karma.Weeks = TmpChan.BookingTypes(1).Weeks.Count
            End If
        Next
        frmProgress.Status = "Fetching spots..."
        frmProgress.Progress = 0
        frmProgress.Show()
        AddHandler Karma.PopulateProgress, AddressOf Progress
        Karma.Populate(UseSponsorship, UseCommercial)

        TmpStr = Campaign.Name
        If TmpStr Is Nothing Then TmpStr = ""
        Karma.Campaigns.Add(TmpStr, Campaign)
        frmProgress.Status = "Calculating reach for " & TmpStr
        frmProgress.Progress = 0
        frmProgress.Show()
        AddHandler Karma.Campaigns(TmpStr).Progress, AddressOf Progress
        Karma.Campaigns(TmpStr).Run()
        frmProgress.Hide()

        For i = 1 To 10
            Campaign.ReachGoal(i) = Karma.Campaigns.Item(TmpStr).Reach(0, i)
            Campaign.ReachGoal(i, Trinity.cKampanj.ReachTargetEnum.rteSecondTarget) = Karma.Campaigns.Item(TmpStr).Reach(0, i, Trinity.cKampanj.ReachTargetEnum.rteSecondTarget)
        Next

        frmInfo_Activated(New Object, New EventArgs)
        Me.Cursor = Windows.Forms.Cursors.Default
        grdReach.Invalidate()
    End Sub

    Private Sub txtMain_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMain.TextChanged
        Saved = False
        Try
            Campaign.MainTarget.TargetName = txtMain.Text
        Catch ex As Exception

        End Try
        lblMainTarget.Text = Format(Campaign.MainTarget.UniSize * 1000, "##,##0")
        If Campaign.MainTarget.UniSize = 0 Then
            lblMainTarget.ForeColor = Drawing.Color.Red
        Else
            lblMainTarget.ForeColor = Drawing.Color.Black
        End If
    End Sub

    Private Sub txtSec_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSec.TextChanged
        Saved = False
        Campaign.SecondaryTarget.TargetName = txtSec.Text
        lblSecondTarget.Text = Format(Campaign.SecondaryTarget.UniSize * 1000, "##,##0")
        If Campaign.SecondaryTarget.UniSize = 0 Then
            lblSecondTarget.ForeColor = Drawing.Color.Red
        Else
            lblSecondTarget.ForeColor = Drawing.Color.Black
        End If
    End Sub

    Private Sub txtThird_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtThird.TextChanged
        Saved = False
        Campaign.ThirdTarget.TargetName = txtThird.Text
        lblThirdTarget.Text = Format(Campaign.ThirdTarget.UniSize * 1000, "##,##0")
        If Campaign.MainTarget.UniSize = 0 Then
            lblThirdTarget.ForeColor = Drawing.Color.Red
        Else
            lblThirdTarget.ForeColor = Drawing.Color.Black
        End If
    End Sub

    Private Sub grdReach_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdReach.CellValueNeeded
        If e.ColumnIndex = 0 Then
            e.Value = Format(Campaign.ReachGoal(e.RowIndex + 1, cmbTarget.SelectedIndex), "N1")
        Else
            e.Value = Format(Campaign.ReachActual(e.RowIndex + 1, cmbTarget.SelectedIndex), "N1")
            Debug.Print(cmbTarget.Items.Count)
        End If
    End Sub

    Private Sub grdReach_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdReach.CellValuePushed
        If e.ColumnIndex = 0 Then
            Campaign.ReachGoal(e.RowIndex + 1, cmbTarget.SelectedIndex) = e.Value
        End If
    End Sub

    Private Sub cmbMainUni_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMainUni.SelectedIndexChanged
        Campaign.MainTarget.Universe = Campaign.Universes.GetKey(sender.selectedindex)
        lblMainTarget.Text = Format(Campaign.MainTarget.UniSize * 1000, "##,##0")
        If Campaign.MainTarget.UniSize = 0 Then
            lblMainTarget.ForeColor = Color.Red
        Else
            lblMainTarget.ForeColor = Color.Black
        End If
    End Sub

    Private Sub cmbSecondUni_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSecondUni.SelectedIndexChanged
        Campaign.SecondaryTarget.Universe = Campaign.Universes.GetKey(sender.selectedindex)
        lblSecondTarget.Text = Format(Campaign.SecondaryTarget.UniSize * 1000, "##,##0")
        If Campaign.SecondaryTarget.UniSize = 0 Then
            lblSecondTarget.ForeColor = Color.Red
        Else
            lblSecondTarget.ForeColor = Color.Black
        End If
    End Sub

    Private Sub cmbThirdUni_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbThirdUni.SelectedIndexChanged
        Campaign.ThirdTarget.Universe = Campaign.Universes.GetKey(sender.selectedindex)
        lblThirdTarget.Text = Format(Campaign.ThirdTarget.UniSize * 1000, "##,##0")
        If Campaign.ThirdTarget.UniSize = 0 Then
            lblThirdTarget.ForeColor = Color.Red
        Else
            lblThirdTarget.ForeColor = Color.Black
        End If
    End Sub

    Private Sub pnlInfo_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlInfo.Paint

    End Sub

    Public Overloads Sub Update()
        MyBase.Update()
        frmInfo_Activated(New Object, New EventArgs)
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        Saved = False
    End Sub

    Private Sub cmbFF_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFF.SelectedIndexChanged
        Campaign.FrequencyFocus = cmbFF.SelectedIndex
    End Sub

    Private Sub cmdAddClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddClient.Click
        Saved = False
    End Sub

    Private Sub cmbTarget_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTarget.SelectedIndexChanged
        grdReach.Invalidate()
    End Sub
End Class