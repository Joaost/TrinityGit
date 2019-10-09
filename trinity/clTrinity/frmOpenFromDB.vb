Imports System.Windows.Forms

Public Class frmOpenFromDB

    Dim Clientlist As List(Of Client) = Nothing
    Dim Productlist As List(Of Product) = Nothing
    Dim CampaignList As List(Of CampaignEssentials)
    'Dim BTT As Trinity.cBalloonToolTip

    Dim _orderVar As String = "startdate"
    Dim _ascDesc As String = "DESC"
    Dim _userAccessMC As String = "mc_access"
    Dim _userAccessMS As String = "ms_access"


    Private _doNotLoadCampaign As Boolean = False
    Public Property DoNotLoadCampaign() As Boolean
        Get
            Return _doNotLoadCampaign
        End Get
        Set(ByVal value As Boolean)
            _doNotLoadCampaign = value
        End Set
    End Property

    Private _campaignID As Integer
    Public Property CampaignID() As Integer
        Get
            Return _campaignID
        End Get
        Set(ByVal value As Integer)
            _campaignID = value
        End Set
    End Property


    Private Function GetMonth(ByVal Month As String) As Integer
        Select Case Month
            Case Is = "January"
                Return 1
            Case Is = "February"
                Return 2
            Case Is = "March"
                Return 3
            Case Is = "April"
                Return 4
            Case Is = "May"
                Return 5
            Case Is = "June"
                Return 6
            Case Is = "July"
                Return 7
            Case Is = "August"
                Return 8
            Case Is = "September"
                Return 9
            Case Is = "October"
                Return 10
            Case Is = "November"
                Return 11
            Case Is = "December"
                Return 12
            Case Is = "All"
                Return 0
            Case Else
                Return Nothing
        End Select
    End Function


    Private Sub PopulateClients()

        If Clientlist Is Nothing Then
            Dim dt As DataTable = DBReader.getAllClients()
            Clientlist = New List(Of Client)
            For Each item As Object In dt.Rows
                Dim tmpClient As New Client
                tmpClient.id = item!id
                tmpClient.name = item!name
                If Not IsDBNull(item("restricted")) Then
                    tmpClient.restricted = item("restricted") 'rd!Restricted 
                End If
                Clientlist.Add(tmpClient)
            Next
            Clientlist.Sort(New ClientComparer)



        End If

        cmbClient.Items.Clear()

        cmbClient.Items.Add("All")

        Dim Year As Object = cmbYear.SelectedItem
        Dim Month As Object = cmbMonth.SelectedItem

        For Each tmpClient As Client In Clientlist
            cmbClient.Items.Add(tmpClient)
        Next

        cmbClient.SelectedIndex = 0

        If cmbClient.Items.Count > 0 Then
            If cmbClient.SelectedItem.GetType.FullName = "System.String" Then
                PopulateProducts(-1)
            Else
                PopulateProducts(cmbClient.SelectedItem.id)
            End If
        End If




    End Sub

    Private Sub PopulateProducts(ByVal clientID As Integer)

        If Not cmbClient.Items.Count > 0 Then Exit Sub

        cmbProduct.Items.Clear()
        cmbProduct.Items.Add("All")

        Dim dt As DataTable = DBReader.getAllProducts(clientID)
        Productlist = New List(Of Product)
        For Each item As Object In dt.Rows
            Dim tmpProduct As New Product
            tmpProduct.id = item!ID
            tmpProduct.name = item!Name
            Productlist.Add(tmpProduct)
        Next

        Productlist.Sort(New CampaignNameComparer)

        For Each tmpProduct As Product In Productlist
            cmbProduct.Items.Add(tmpProduct)
        Next

        cmbProduct.SelectedIndex = 0

        'PopulateList()

    End Sub

    Public Sub PopulateList()

        'lstCampaigns.Items.Clear()
        grdCampaigns.Rows.Clear()

        For Each tmpcmp As CampaignEssentials In DBReader.GetCampaigns(BuildSQL)
            'lstCampaigns.Items.Add(tmpcmp)
            If grdCampaigns.Columns.Count > 0 Then
                Dim newRow As Integer = grdCampaigns.Rows.Add
                grdCampaigns.Rows(newRow).Tag = tmpcmp
            End If
        Next

        lblCount.Text = grdCampaigns.Rows.Count.ToString
    End Sub

    Public Sub PopulatePeople()

        cmbBy.ValueMember = "id"
        cmbBy.DisplayMember = "name"
        cmbBy.Items.Add("All")
        Dim selectedIndex As Integer = 0
        For Each tmpPerson As Trinity.cPerson In DBReader.getAllPeople
            If tmpPerson.Name = TrinitySettings.UserName Then
                selectedIndex = cmbBy.Items.Add(tmpPerson)
            Else
                cmbBy.Items.Add(tmpPerson)
            End If
        Next

        cmbBy.SelectedIndex = selectedIndex

    End Sub

    Public Sub PopulateRecent()
        grdRecent.Rows.Clear()
        'Dim _campIDs As String
        If TrinitySettings.getLastCampaigns().Where(Function(s As String) Val(s) > 0).Count > 0 Then
            '_campIDs = String.Join(",", TrinitySettings.getLastCampaigns().Where(Function(s As String) Val(s) > 0).ToArray)
            For i As Integer = 0 To TrinitySettings.getLastCampaigns.Count() - 1
                For Each tmpCmp As CampaignEssentials In DBReader.GetCampaigns(BuildRecentSQL)
                    If TrinitySettings.getLastCampaigns(i) <> "" Then

                        If (tmpCmp.id.ToString() = TrinitySettings.getLastCampaigns(i)) Then
                            Dim newRow As Integer = grdRecent.Rows.Add
                            grdRecent.Rows(newRow).Tag = tmpCmp
                        End If
                    End If
                Next
            Next
        End If
    End Sub
    Public Function BuildRecentSQL() As String
        Dim _campIDs As String
        _campIDs = String.Join(",", TrinitySettings.getLastCampaigns().Where(Function(s As String) Val(s) > 0).ToArray)

        Dim SQLString As String = Nothing
        SQLString &= "SELECT campaigns.id,campaigns.name,YEAR(startdate) as year,MONTH(startdate) as month,startdate,contractid, enddate,client,status,product,p.name as planner,ps.name as buyer,lastopened,lastsaved,originallocation,originalfilechangeddate,campaignid,lockedby from campaigns join people p on campaigns.planner=p.id join people ps on campaigns.buyer=ps.id WHERE deletedon < '2001-01-01' AND campaigns.id in (" & _campIDs & ") order by lastopened"

        Return SQLString '& " ORDER BY " & _orderVar & " " & _ascDesc
    End Function
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        PopulateClients()

        cmbClient.DisplayMember = "name"
        cmbClient.ValueMember = "id"
        cmbProduct.DisplayMember = "name"
        cmbProduct.ValueMember = "id"
        cmbYear.Items.AddRange(New Object() {"All", 2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019})
        cmbMonth.Items.AddRange(New Object() {"All", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"})
        cmbStatus.Items.AddRange(New Object() {"All", "Exclude Cancelled", "Planned", "Running", "Finished", "Cancelled"})
        cmbStatus.SelectedItem = "Exclude Cancelled"
        cmbYear.SelectedItem = Now.Year
        cmbMonth.SelectedItem = "All"

        btnMultiChangeCampaigns.Visible = TrinitySettings.Developer

        PopulatePeople()

        'grdCampaigns.AutoSizeColumnsMode = Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        'grdRecent.AutoSizeColumnsMode = Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill

        'grdRecent.Columns.Add("colstartdate", "Start")
        'grdRecent.Columns.Add("colenddate", "End")
        'grdRecent.Columns.Add("colname", "Name")
        'grdRecent.Columns.Add("colstatus", "Status")

        'grdRecent.Columns("colstartdate").FillWeight = 30
        'grdRecent.Columns("colenddate").FillWeight = 30
        'grdRecent.Columns("colname").FillWeight = 50
        'grdRecent.Columns("colstatus").FillWeight = 15


        'grdCampaigns.Columns.Add("colstartdate", "Start")
        'grdCampaigns.Columns.Add("colenddate", "End")
        'grdCampaigns.Columns.Add("colname", "Name")
        'grdCampaigns.Columns.Add("colstatus", "Status")

        'Dim imageColumn As New Windows.Forms.DataGridViewImageColumn
        'imageColumn.Name = "coldelete"
        'imageColumn.HeaderText = "Delete"
        'imageColumn.Image = My.Resources.delete2
        'grdCampaigns.Columns.Add(imageColumn)

        'grdCampaigns.Columns("colstartdate").FillWeight = 20
        'grdCampaigns.Columns("colenddate").FillWeight = 20
        'grdCampaigns.Columns("colname").FillWeight = 50
        'grdCampaigns.Columns("colstatus").FillWeight = 15
        'grdCampaigns.Columns("coldelete").FillWeight = 10
        'grdRecent.Columns("coldelete").FillWeight = 10

        PopulateRecent()
        OpenAsDefault()

    End Sub

    Private Sub cmbClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbClient.SelectedIndexChanged
        If cmbClient.SelectedItem.GetType.FullName = "System.String" Then
            PopulateProducts(-1)
        Else
            PopulateProducts(cmbClient.SelectedItem.id)
        End If
    End Sub

    Private Sub cmbProduct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProduct.SelectedIndexChanged
        PopulateList()
    End Sub

    Public Function BuildSQL() As String
        Dim SQLString As String = Nothing
        Dim NeedsAnd As Boolean = False
        Dim EmptyException As String = ""

        'SQLString &= "SELECT id,name,startdate,enddate,client,status,product,contractid,planner,buyer,lastopened,lastsaved,originallocation,originalfilechangeddate,campaignid from campaigns WHERE deletedon < '2001-01-01' AND "
        Dim DBUserAccess As List(Of dbUA) = Campaign.GetCampaignAreas
        SQLString &= "SELECT campaigns.id,campaigns.name,startdate,enddate,client,status,product,contractid,lastopened,lastsaved,originallocation,originalfilechangeddate,campaignid, p.name as planner, ps.name as buyer,pl.name as lockedby from campaigns left join people as p on campaigns.planner = p.id left join people as ps on campaigns.buyer = ps.id left join people as pl on campaigns.lockedby=pl.id WHERE deletedon < '2001-01-01' AND "


        If cmbYear.SelectedItem IsNot Nothing AndAlso cmbYear.SelectedItem.GetType.FullName <> "System.String" Then
            SQLString &= "YEAR(startdate) = " & cmbYear.SelectedItem
            NeedsAnd = True
        Else

        End If

        If cmbMonth.SelectedItem IsNot Nothing AndAlso cmbMonth.SelectedItem <> "All" Then
            If NeedsAnd Then
                SQLString &= " AND (MONTH(startdate) = " & GetMonth(cmbMonth.SelectedItem) & " OR MONTH(enddate)=" & GetMonth(cmbMonth.SelectedItem) & ")"
            Else
                SQLString &= " (MONTH(startdate) = " & GetMonth(cmbMonth.SelectedItem) & " OR MONTH(enddate)=" & GetMonth(cmbMonth.SelectedItem) & ")"
            End If
            NeedsAnd = True
        Else

        End If

        If cmbClient.SelectedItem.GetType.FullName <> "System.String" Then
            If NeedsAnd Then
                SQLString &= " AND client=" & cmbClient.SelectedItem.id
            Else
                SQLString &= "client=" & cmbClient.SelectedItem.id
            End If
            NeedsAnd = True
        Else

        End If

        If cmbProduct.SelectedItem.GetType.FullName <> "System.String" Then
            If NeedsAnd Then
                SQLString &= " AND product=" & cmbProduct.SelectedItem.id
            Else
                SQLString &= "product=" & cmbProduct.SelectedItem.id
            End If
            NeedsAnd = True
        Else

        End If

        If cmbStatus.SelectedItem IsNot Nothing AndAlso cmbStatus.SelectedItem <> "All" AndAlso cmbStatus.SelectedItem <> "Exclude Cancelled" Then
            If NeedsAnd Then
                SQLString &= " AND status='" & cmbStatus.SelectedItem & "'"
            Else
                SQLString &= "status='" & cmbStatus.SelectedItem & "'"
            End If
            NeedsAnd = True
        ElseIf cmbStatus.SelectedItem = "Exclude Cancelled" Then
            If NeedsAnd Then
                SQLString &= " AND "
            End If
            SQLString &= "status<>'Cancelled'"
            NeedsAnd = True
        End If

        If cmbBy.SelectedItem IsNot Nothing AndAlso cmbBy.SelectedItem.GetType.FullName = "clTrinity.Trinity.cPerson" Then
            If NeedsAnd Then
                SQLString &= " AND (campaigns.planner=" & cmbBy.SelectedItem.id & " OR campaigns.buyer=" & cmbBy.SelectedItem.id & ")"
            Else
                SQLString &= "(campaigns.planner=" & cmbBy.SelectedItem.id & " OR campaigns.buyer=" & cmbBy.SelectedItem.id & ")"
            End If
            NeedsAnd = True
        Else

        End If

        If NeedsAnd Then
            SQLString &= " AND campaigns.name LIKE '%" & txtSearch.Text & "%'"
        Else
            SQLString &= " campaigns.name LIKE '%" & txtSearch.Text & "%'"
        End If

        If Strings.Right(SQLString, 4) = "AND " Then
            Return Strings.Left(SQLString, SQLString.LastIndexOf("AND ")) & " ORDER BY startdate DESC,client, product"
        End If

        Dim tmpCount As Integer = 0
        Dim UserAccessComplete As Boolean = False
        For i As Integer = 0 To DBUserAccess.Count - 1
            If DBUserAccess(i).dbValue Then
                UserAccessComplete = True
                tmpCount = tmpCount + 1
            End If
        Next
        If tmpCount > 1 Then
            NeedsAnd = True
        Else
            NeedsAnd = False
        End If

        'If DBUserAccess.Count > 0 And UserAccessComplete Then
        '    SQLString &= " AND "
        '    Dim pointerIndex = 1
        '    For Each tmpObj As dbUA In DBUserAccess
        '        If tmpObj.dbValue Then
        '            If NeedsAnd Then
        '                SQLString &= "(campaigns.useraccess LIKE '%" & tmpObj.dbName & "%' OR "
        '                NeedsAnd = False
        '            Else
        '                If DBUserAccess.Count <> pointerIndex Then
        '                    SQLString &= "campaigns.useraccess LIKE '%" & tmpObj.dbName & "%'"
        '                Else
        '                    SQLString &= "campaigns.useraccess LIKE '%" & tmpObj.dbName & "%' OR "
        '                End If
        '            End If
        '        End If
        '        pointerIndex = pointerIndex + 1
        '    Next
        '    If tmpCount > 1 Then
        '        SQLString &= ")"
        '    End If
        'Else
        '    SQLString &= " AND campaigns.useraccess = ''"

        'End If

        Return SQLString & " ORDER BY " & _orderVar & " " & _ascDesc


    End Function

    Private Sub txtSearch_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyUp

    End Sub

    Private Sub cmbYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbYear.SelectedIndexChanged
        'PopulateClients()
        PopulateList()
    End Sub

    Private Sub cmbMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMonth.SelectedIndexChanged
        PopulateList()

    End Sub

    Private Sub cmbBy_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBy.SelectedIndexChanged
        PopulateList()
        'BuildSQL()
    End Sub

    Private Sub cmbStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbStatus.SelectedIndexChanged
        PopulateList()
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        tmrKeypress.Enabled = False
        tmrKeypress.Enabled = True
    End Sub

    Private Sub tmrKeypress_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrKeypress.Tick
        If txtSearch.TextLength <> 0 Then
            PopulateList()
            tmrKeypress.Enabled = False
            Exit Sub
        Else
            PopulateList()
        End If

        tmrKeypress.Enabled = False
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        txtSearch.TabStop = True
        txtSearch.TabIndex = 1
        txtSearch.Focus()
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub grdCampaigns_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCampaigns.CellClick
        If e.RowIndex < 0 Then Exit Sub
        If Not grdCampaigns.Columns(e.ColumnIndex).HeaderText = "Delete" Then
            grdCampaigns.Rows(e.RowIndex).Selected = True
        Else
            If Windows.Forms.MessageBox.Show("Are you sure you want to delete the campaign " & grdCampaigns.Rows(e.RowIndex).Tag.name & "?", "Delete?", Windows.Forms.MessageBoxButtons.OKCancel, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
                DBReader.setAsDeleted(grdCampaigns.Rows(e.RowIndex).Tag.id)
                PopulateList()
                PopulateRecent()
            End If
        End If

    End Sub
    Private Sub grdCampaigns_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles grdCampaigns.KeyDown
        FocusOnTxtSeach()
        If e.KeyCode = Keys.Enter Then
            btnOpen_Click(sender, e)
        End If
    End Sub

    Private Sub grdCampaigns_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCampaigns.CellMouseEnter
        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        'Dim tmpCmp As clTrinity.CampaignEssentials = grdCampaigns.Rows(e.RowIndex).Tag
        'BTT = New Trinity.cBalloonToolTip
        'BTT.Style = Trinity.cBalloonToolTip.ttStyleEnum.TTBalloon
        'BTT.VisibleTime = 1000
        'BTT.Title = "Campaign info"
        'BTT.TipText = "Test"
        'BTT.CreateToolTip(Me.Handle)
        'BTT.Show(MousePosition.X, MousePosition.Y)
    End Sub

    Private Sub grdCampaigns_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCampaigns.CellMouseLeave
        'If BTT IsNot Nothing Then
        '    BTT.Destroy()
        '    BTT = Nothing
        'End If
    End Sub

    Private Sub grdCampaigns_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdCampaigns.CellValueNeeded
        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        Select Case grdCampaigns.Rows(e.RowIndex).Cells(e.ColumnIndex).OwningColumn.HeaderText
            Case Is = "Start"
                e.Value = Format(grdCampaigns.Rows(e.RowIndex).Tag.startdate, "yyyy-MM-dd")
            Case Is = "End"
                e.Value = Format(grdCampaigns.Rows(e.RowIndex).Tag.enddate, "yyyy-MM-dd")
            Case Is = "Name"
                e.Value = grdCampaigns.Rows(e.RowIndex).Tag.name
            Case Is = "Status"
                e.Value = grdCampaigns.Rows(e.RowIndex).Tag.status
            Case Is = "Saved"
                e.Value = grdCampaigns.Rows(e.RowIndex).Tag.lastsaved
            Case Is = "Planner"
                e.Value = grdCampaigns.Rows(e.RowIndex).Tag.planner
            Case Is = "Buyer"
                e.Value = grdCampaigns.Rows(e.RowIndex).Tag.buyer
            Case Is = "Locked"
                e.Value = grdCampaigns.Rows(e.RowIndex).Tag.lockedby
            Case Else
                'e.Value = ""
        End Select
    End Sub

    Private Sub grdRecent_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRecent.CellClick
        FocusOnTxtSeach()
        If e.RowIndex < 0 Then Exit Sub
        grdRecent.Rows(e.RowIndex).Selected = True
    End Sub

    Private Sub grdRecent_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdRecent.CellValueNeeded
        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        Select Case grdRecent.Rows(e.RowIndex).Cells(e.ColumnIndex).OwningColumn.HeaderText
            Case Is = "Start"
                e.Value = Format(grdRecent.Rows(e.RowIndex).Tag.startdate, "yyyy-MM-dd")
            Case Is = "End"
                e.Value = Format(grdRecent.Rows(e.RowIndex).Tag.enddate, "yyyy-MM-dd")
            Case Is = "Name"
                e.Value = grdRecent.Rows(e.RowIndex).Tag.name
            Case Is = "Status"
                e.Value = grdRecent.Rows(e.RowIndex).Tag.status
            Case Is = "Saved"
                e.Value = grdRecent.Rows(e.RowIndex).Tag.lastsaved
            Case Is = "Planner"
                e.Value = grdRecent.Rows(e.RowIndex).Tag.planner
            Case Is = "Buyer"
                e.Value = grdRecent.Rows(e.RowIndex).Tag.buyer
            Case Else
                e.Value = ""
        End Select
    End Sub

    Private Function OpenCampaign(ByVal ID As Integer, Optional ByVal ContractID As Integer = 0) As Boolean

        'Dim CampaignID As Long

        Try
            Campaign.DatabaseID = ID
            Campaign.ContractID = ContractID
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("No campaign selected", "Info", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            Exit Function
        End Try
        Dim _campXML As String = ""
        Dim _readOnly As Boolean = False

        Try
            _campXML = DBReader.GetCampaign(Campaign.DatabaseID)
            Me.Cursor = Windows.Forms.Cursors.WaitCursor
        Catch ex As ReadOnlyException
            Dim dlgLocked As New frmLockedCampaign(ex.Message)
            Select Case dlgLocked.ShowDialog
                Case Windows.Forms.DialogResult.OK
                    _campXML = DBReader.GetCampaign(Campaign.DatabaseID, True)
                    _readOnly = True
                Case Windows.Forms.DialogResult.Cancel
                    Exit Function
                Case Windows.Forms.DialogResult.Ignore
                    If Windows.Forms.MessageBox.Show("Unlocking a campaign will make the user currently using" & vbCrLf & "it unable to save." & vbCrLf & vbCrLf & "Are you sure you want to unlock?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        DBReader.unlockCampaign(Campaign.DatabaseID)
                        _campXML = DBReader.GetCampaign(Campaign.DatabaseID, False)
                    Else
                        Return False
                    End If
            End Select
            Me.Cursor = Windows.Forms.Cursors.WaitCursor
        Catch ex As Exception
            Throw ex
        End Try
        If _campXML <> "" Then
            Campaign.LoadCampaign("", True, _campXML)
            Campaign.ReadOnly = _readOnly
        Else
            Return False
        End If
        TrinitySettings.setLastCampaign = Campaign.DatabaseID
        Me.Cursor = Windows.Forms.Cursors.Default
        Return True
    End Function

    Private Sub frmOpenFromDB_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        PopulateRecent()
        PopulateList()
        grdRecent.ClearSelection()
        grdCampaigns.ClearSelection()
    End Sub

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        DBReader.closeConnection()
        If grdCampaigns.SelectedRows.Count > 0 Then
            _campaignID = grdCampaigns.SelectedRows(0).Tag.ID
            If Not _doNotLoadCampaign Then
                'If campaign contains client that is restricted check with XML
                Dim returnClient As List(Of Client) = Campaign.checkIfCampaignHasRescritions(TrinitySettings.UserName, _campaignID)
                If returnClient(0).restricted Then
                    If Campaign.checkIfUserIsValid(returnClient(0).name) Then
                        Windows.Forms.MessageBox.Show("Campaign client contains restriction but user is correct.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                        Campaign = New Trinity.cKampanj(TrinitySettings.ErrorChecking)
                        TrinitySettings.MainObject = Campaign
                        Trinity.Helper.MainObject = Campaign
                        If Not OpenCampaign(_campaignID, grdCampaigns.SelectedRows(0).Tag.ContractID) Then
                            Exit Sub
                        End If
                    Else
                        Windows.Forms.MessageBox.Show("Campaign client contains restriction but user is incorrect", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                    End If
                Else
                    'If not go a head as usual
                    Campaign = New Trinity.cKampanj(TrinitySettings.ErrorChecking)
                    TrinitySettings.MainObject = Campaign
                    Trinity.Helper.MainObject = Campaign
                    If Not OpenCampaign(_campaignID, grdCampaigns.SelectedRows(0).Tag.ContractID) Then
                        Exit Sub
                    End If
                End If

            End If
        ElseIf grdRecent.SelectedRows.Count > 0 Then
            _campaignID = grdRecent.SelectedRows(0).Tag.ID
            If Not _doNotLoadCampaign Then
                Campaign = New Trinity.cKampanj(TrinitySettings.ErrorChecking)
                TrinitySettings.MainObject = Campaign
                Trinity.Helper.MainObject = Campaign
                OpenCampaign(_campaignID)
            End If
        Else
            Windows.Forms.MessageBox.Show("No campaign was selected.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            Exit Sub
        End If

        If chkDefault.Checked = True Then
            SetAsDefault()
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK

    End Sub

    Private Sub grdRecent_CellContentClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles grdRecent.CellContentClick, grdRecent.GotFocus
        FocusOnTxtSeach()
        grdCampaigns.ClearSelection()
    End Sub

    Private Sub FocusOnTxtSeach()
        txtSearch.TabStop = True
        txtSearch.TabIndex = 1
        txtSearch.Focus()
    End Sub

    Private Sub grdCampaigns_CellContentClick(ByVal sender As System.Object, ByVal e As EventArgs) Handles grdCampaigns.CellContentClick, grdCampaigns.GotFocus
        FocusOnTxtSeach()
        grdRecent.ClearSelection()
    End Sub

    Private Sub cmdImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImport.Click
        Dim _import As New frmImportCampaigns
        _import.ShowDialog()
    End Sub

    Private Sub frmOpenFromDB_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        FocusOnTxtSeach()
    End Sub

    Private Sub grdCampaigns_CellMouseDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdCampaigns.CellMouseDoubleClick, grdRecent.CellMouseDoubleClick
        If e.RowIndex < 0 Then Exit Sub
        btnOpen_Click(sender, New EventArgs)
    End Sub

    Private Sub grdCampaigns_ColumnHeaderMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdCampaigns.ColumnHeaderMouseClick

        If e.Button = Windows.Forms.MouseButtons.Left Then

            Dim _reverse As Boolean = False
            Select Case grdCampaigns.Columns(e.ColumnIndex).HeaderText
                Case "Start"
                    If _orderVar = "startdate" Then _reverse = True
                    _orderVar = "startdate"
                Case "End"
                    If _orderVar = "enddate" Then _reverse = True
                    _orderVar = "enddate"
                Case "Name"
                    If _orderVar = "name" Then _reverse = True
                    _orderVar = "name"
                Case "Status"
                    If _orderVar = "status" Then _reverse = True
                    _orderVar = "status"
                Case "Saved"
                    If _orderVar = "lastsaved" Then _reverse = True
                    _orderVar = "lastsaved"
                Case "Planner"
                    If _orderVar = "planner" Then _reverse = True
                    _orderVar = "planner"
                Case "Buyer"
                    If _orderVar = "buyer" Then _reverse = True
                    _orderVar = "buyer"
                Case "Locked"
                    If _orderVar = "lockedby" Then _reverse = True
                    _orderVar = "lockedby"
                Case Else
                    'e.Value = ""
            End Select
            If _reverse Then _ascDesc = IIf(_ascDesc = "ASC", "DESC", "ASC")
            PopulateList()
        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then

            Dim columnheader As Windows.Forms.DataGridViewColumn

            Dim mnuFilter As New Windows.Forms.ContextMenu

            mnuFilter.MenuItems.Clear()


            For Each columnheader In grdCampaigns.Columns
                If columnheader.HeaderText IsNot "Delete" Then
                    mnuFilter.MenuItems.Add(columnheader.HeaderText, AddressOf campaignfilter_Click).Checked = columnheader.Visible
                End If
            Next

            Dim pos As Point = Me.PointToClient(Cursor.Position)
            mnuFilter.Show(Me, pos, LeftRightAlignment.Right)

        End If
    End Sub

    Private Sub campaignfilter_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If sender.text = "Start" Then
            If grdCampaigns.Columns.Item("colStart").Visible = True Then
                grdCampaigns.Columns.Item("colStart").Visible = False
            Else
                grdCampaigns.Columns.Item("colStart").Visible = True
            End If
        ElseIf sender.text = "End" Then
            If grdCampaigns.Columns.Item("colEnd").Visible = True Then
                grdCampaigns.Columns.Item("colEnd").Visible = False
            Else
                grdCampaigns.Columns.Item("colEnd").Visible = True
            End If
        ElseIf sender.text = "Name" Then
            If grdCampaigns.Columns.Item("colName").Visible = True Then
                grdCampaigns.Columns.Item("colName").Visible = False
            Else
                grdCampaigns.Columns.Item("colName").Visible = True
            End If
        ElseIf sender.text = "Status" Then
            If grdCampaigns.Columns.Item("colStatus").Visible = True Then
                grdCampaigns.Columns.Item("colStatus").Visible = False
            Else
                grdCampaigns.Columns.Item("colStatus").Visible = True
            End If
        ElseIf sender.text = "Planner" Then
            If grdCampaigns.Columns.Item("colPlanner").Visible = True Then
                grdCampaigns.Columns.Item("colPlanner").Visible = False
            Else
                grdCampaigns.Columns.Item("colPlanner").Visible = True
            End If
        ElseIf sender.text = "Buyer" Then
            If grdCampaigns.Columns.Item("colBuyer").Visible = True Then
                grdCampaigns.Columns.Item("colBuyer").Visible = False
            Else
                grdCampaigns.Columns.Item("colBuyer").Visible = True
            End If
        ElseIf sender.text = "Saved" Then
            If grdCampaigns.Columns.Item("colSaved").Visible = True Then
                grdCampaigns.Columns.Item("colSaved").Visible = False
            Else
                grdCampaigns.Columns.Item("colSaved").Visible = True
            End If
        ElseIf sender.text = "Locked" Then
            If grdCampaigns.Columns.Item("colLocked").Visible = True Then
                grdCampaigns.Columns.Item("colLocked").Visible = False
            Else
                grdCampaigns.Columns.Item("colLocked").Visible = True
            End If
        End If
    End Sub

    Private Sub grdRecent_ColumnHeaderMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdRecent.ColumnHeaderMouseClick
        If e.Button = Windows.Forms.MouseButtons.Left Then

            Dim _reverse As Boolean = False
            Select Case grdRecent.Columns(e.ColumnIndex).HeaderText
                Case "Start"
                    If _orderVar = "startdate" Then _reverse = True
                    _orderVar = "startdate"
                Case "End"
                    If _orderVar = "enddate" Then _reverse = True
                    _orderVar = "enddate"
                Case "Name"
                    If _orderVar = "name" Then _reverse = True
                    _orderVar = "name"
                Case "Status"
                    If _orderVar = "status" Then _reverse = True
                    _orderVar = "status"
                Case "Last saved"
                    If _orderVar = "lastsaved" Then _reverse = True
                    _orderVar = "lastsaved"
                Case "Planner"
                    If _orderVar = "planner" Then _reverse = True
                    _orderVar = "planner"
                Case "Buyer"
                    If _orderVar = "buyer" Then _reverse = True
                    _orderVar = "buyer"
                Case Else
                    'e.Value = ""
            End Select
            If _reverse Then _ascDesc = IIf(_ascDesc = "ASC", "DESC", "ASC")
            PopulateRecent()
        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then

            Dim columnheader As Windows.Forms.DataGridViewColumn

            Dim mnuFilter As New Windows.Forms.ContextMenu

            mnuFilter.MenuItems.Clear()

            For Each columnheader In grdRecent.Columns
                If columnheader.HeaderText IsNot "Delete" Then
                    mnuFilter.MenuItems.Add(columnheader.HeaderText, AddressOf recentcampaignfilter_Click).Checked = columnheader.Visible
                End If
            Next

            Dim pos As Point = Me.PointToClient(Cursor.Position)
            mnuFilter.Show(Me, pos, LeftRightAlignment.Right)

        End If

    End Sub

    Private Sub recentcampaignfilter_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If sender.text = "Start" Then
            If grdRecent.Columns.Item("colrecentstartdate").Visible = True Then
                grdRecent.Columns.Item("colrecentstartdate").Visible = False
            Else
                grdRecent.Columns.Item("colrecentstartdate").Visible = True
            End If
        ElseIf sender.text = "End" Then
            If grdRecent.Columns.Item("colrecentenddate").Visible = True Then
                grdRecent.Columns.Item("colrecentenddate").Visible = False
            Else
                grdRecent.Columns.Item("colrecentenddate").Visible = True
            End If
        ElseIf sender.text = "Name" Then
            If grdRecent.Columns.Item("colrecentenddate").Visible = True Then
                grdRecent.Columns.Item("colrecentenddate").Visible = False
            Else
                grdRecent.Columns.Item("colrecentenddate").Visible = True
            End If
        ElseIf sender.text = "Status" Then
            If grdRecent.Columns.Item("colrecentstatus").Visible = True Then
                grdRecent.Columns.Item("colrecentstatus").Visible = False
            Else
                grdRecent.Columns.Item("colrecentstatus").Visible = True
            End If
        ElseIf sender.text = "Planner" Then
            If grdRecent.Columns.Item("colrecentplanner").Visible = True Then
                grdRecent.Columns.Item("colrecentplanner").Visible = False
            Else
                grdRecent.Columns.Item("colrecentplanner").Visible = True
            End If
        ElseIf sender.text = "Buyer" Then
            If grdRecent.Columns.Item("colrecentbuyer").Visible = True Then
                grdRecent.Columns.Item("colrecentbuyer").Visible = False
            Else
                grdRecent.Columns.Item("colrecentbuyer").Visible = True
            End If
        ElseIf sender.text = "Last saved" Then
            If grdRecent.Columns.Item("colRecentSaved").Visible = True Then
                grdRecent.Columns.Item("colRecentSaved").Visible = False
            Else
                grdRecent.Columns.Item("colRecentSaved").Visible = True
            End If
        End If

    End Sub

    Private Sub SetAsDefault()

        Dim i As Integer
        Dim col As Windows.Forms.DataGridViewColumn
        Dim visCol As Integer

        For Each col2 As Windows.Forms.DataGridViewColumn In grdCampaigns.Columns
            If col2.Visible = True Then
                visCol = visCol + 1
            End If
        Next

        TrinitySettings.PrintOpenDBColumnCount = visCol
        For Each col In grdCampaigns.Columns
            If col.Visible = True Then
                TrinitySettings.PrintOpenDBColumn(i) = col.HeaderText()
                i = i + 1
            End If
        Next
    End Sub
    Private Sub OpenAsDefault()

        Dim i As Windows.Forms.DataGridViewColumn

        If TrinitySettings.PrintOpenDBColumnCount > 0 Then
            For c As Integer = 0 To TrinitySettings.PrintOpenDBColumnCount

                For Each col As Windows.Forms.DataGridViewColumn In grdCampaigns.Columns
                    If grdCampaigns.Columns.Item("col" & TrinitySettings.PrintOpenDBColumn(c)) IsNot Nothing Then
                        If TrinitySettings.PrintOpenDBColumn(c).ToString = "Delete" Then
                            Exit Sub
                        Else
                            grdCampaigns.Columns.Item("col" & TrinitySettings.PrintOpenDBColumn(c)).Visible = True
                        End If

                    End If
                Next
            Next
        Else
            grdCampaigns.Columns.Item(0).Visible = True
            grdCampaigns.Columns.Item(1).Visible = True
            grdCampaigns.Columns.Item(2).Visible = True
            grdCampaigns.Columns.Item(3).Visible = True
            grdCampaigns.Columns.Item(8).Visible = True
        End If

    End Sub

    Private Sub btnMultiChangeCampaigns_Click(sender As System.Object, e As System.EventArgs) Handles btnMultiChangeCampaigns.Click
        Dim multiChange As New frmMultiChangeCampaigns
        multiChange.Show()
    End Sub
End Class




''' <summary>
''' Describes a client in terms of a client ID and name
''' </summary>
''' <remarks></remarks>
Public Class Client
    Private _id As Integer
    Private _name As String
    Private _restricted As Boolean

    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property
    Public Property restricted() As Boolean
        Get
            Return _restricted
        End Get
        Set(ByVal value As Boolean)
            _restricted = value
        End Set
    End Property

    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
End Class

''' <summary>
''' Describes a product in terms of a product ID and name
''' </summary>
''' <remarks></remarks>
Class Product
    Private _id As Integer
    Private _name As String

    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
End Class

Class ClientComparer
    Implements IComparer(Of Client)


    Public Function Compare(ByVal x As Client, ByVal y As Client) As Integer Implements System.Collections.Generic.IComparer(Of Client).Compare
        Return x.name.CompareTo(y.name)

    End Function
End Class

Class CampaignNameComparer
    Implements IComparer(Of Product)


    Public Function Compare(ByVal x As Product, ByVal y As Product) As Integer Implements System.Collections.Generic.IComparer(Of Product).Compare
        Return x.name.CompareTo(y.name)

    End Function
End Class

Class CampaignDateComparer
    Implements IComparer(Of CampaignEssentials)


    Public Function Compare(ByVal x As CampaignEssentials, ByVal y As CampaignEssentials) As Integer Implements System.Collections.Generic.IComparer(Of CampaignEssentials).Compare
        Return x.lastopened.CompareTo(y.lastopened)

    End Function
End Class