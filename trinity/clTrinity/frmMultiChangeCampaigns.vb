Imports System.Windows.Forms


Public Class frmMultiChangeCampaigns

    Dim Clientlist As List(Of Client) = Nothing
    Dim Productlist As List(Of Product) = Nothing
    Dim RegularDB As Boolean = True

    Dim _orderVar As String = "startdate"
    Dim _ascDesc As String = "DESC"


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

    Public Function BuildBackupDBSQL() As String
        Dim SQLString As String = Nothing
        Dim NeedsAnd As Boolean = False

        SQLString = "SELECT [backup].id,originalid,[backup].name,status,lastsaved,p.name as planner,pb.name as buyer FROM [backup] left join people as p on [backup].planner = p.id left join people as pb on [backup].buyer = pb.id WHERE deletedon < '2001-01-01' AND "

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
        End If

        'If cmbStatus.SelectedItem IsNot Nothing AndAlso cmbStatus.SelectedItem <> "All" AndAlso cmbStatus.SelectedItem <> "Exclude Cancelled" Then
        '    If NeedsAnd Then
        '        SQLString &= " AND status='" & cmbStatus.SelectedItem & "'"
        '    Else
        '        SQLString &= "status='" & cmbStatus.SelectedItem & "'"
        '    End If
        '    NeedsAnd = True
        'ElseIf cmbStatus.SelectedItem = "Exclude Cancelled" Then
        '    If NeedsAnd Then
        '        SQLString &= " AND "
        '    End If
        '    SQLString &= "status<>'Cancelled'"
        '    NeedsAnd = True
        'End If

        'If cmbBy.SelectedItem IsNot Nothing AndAlso cmbBy.SelectedItem.GetType.FullName = "clTrinity.Trinity.cPerson" Then
        '    If NeedsAnd Then
        '        SQLString &= " AND [backup].planner=" & cmbBy.SelectedItem.id & " OR [backup].buyer=" & cmbBy.SelectedItem.id & ")"
        '    Else
        '        SQLString &= "[backup].planner=" & cmbBy.SelectedItem.id & " OR [backup].buyer=" & cmbBy.SelectedItem.id & ")"
        '    End If
        '    NeedsAnd = True
        'Else

        'End If

        'If NeedsAnd Then
        '    SQLString &= " AND [backup].name LIKE '%" & txtSearch.Text & "%'"
        'Else
        '    SQLString &= " [backup].name LIKE '%" & txtSearch.Text & "%'"
        'End If

        If Strings.Right(SQLString, 4) = "AND " Then
            Return Strings.Left(SQLString, SQLString.LastIndexOf("AND ")) & " ORDER BY startdate,client, product"
        End If


        Return SQLString & " ORDER BY " & _orderVar & " " & _ascDesc

    End Function

    Public Function BuildSQL() As String
        Dim SQLString As String = Nothing
        Dim NeedsAnd As Boolean = False

        'SQLString &= "SELECT id,name,startdate,enddate,client,status,product,contractid,planner,buyer,lastopened,lastsaved,originallocation,originalfilechangeddate,campaignid from campaigns WHERE deletedon < '2001-01-01' AND "

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

        'If cmbClient.SelectedItem.GetType.FullName <> "System.String" Then
        '    If NeedsAnd Then
        '        SQLString &= " AND client=" & cmbClient.SelectedItem.id
        '    Else
        '        SQLString &= "client=" & cmbClient.SelectedItem.id
        '    End If
        '    NeedsAnd = True
        'End If

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


        Return SQLString & " ORDER BY " & _orderVar & " " & _ascDesc

    End Function

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        cmbClient.DisplayMember = "name"
        cmbClient.ValueMember = "id"
        cmbYear.Items.AddRange(New Object() {"All", 2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017})
        cmbMonth.Items.AddRange(New Object() {"All", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"})
        cmbStatus.Items.AddRange(New Object() {"All", "Exclude Cancelled", "Planned", "Running", "Finished", "Cancelled"})
        cmbStatus.SelectedItem = "Exclude Cancelled"
        cmbYear.SelectedItem = Now.Year
        cmbMonth.SelectedItem = "All"

        PopulateList()
        PopulatePeople()
        PopulateBuyer()
        PopulatePlanner()
        PopulateClients()
        changeDB()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub changeDB()

        If RegularDB = True Then
            cmbChangeDB.SelectedItem = "RegularDB"
            PopulateList()
        Else
            cmbChangeDB.SelectedItem = "BackupDB"
            PopulateListFromBackupDB()
        End If
    End Sub
    Public Sub PopulateList()

        grdCampaigns.Rows.Clear()

        For Each tmpcmp As CampaignEssentials In DBReader.GetCampaigns(BuildSQL)
            'lstCampaigns.Items.Add(tmpcmp)
            If grdCampaigns.Columns.Count > 0 Then
                Dim newRow As Integer = grdCampaigns.Rows.Add
                grdCampaigns.Rows(newRow).Tag = tmpcmp
            End If
        Next

        lblCount.Text = grdCampaigns.Rows.Count.ToString
        RegularDB = True
        btnExtractNewCampaign.Enabled = False
    End Sub
    Public Sub PopulateListFromBackupDB()

        grdCampaigns.Rows.Clear()


        For Each tmpcmp As CampaignEssentials In DBReader.GetBackUpCampaigns(BuildBackupDBSQL)
            'lstCampaigns.Items.Add(tmpcmp)
            If grdCampaigns.Columns.Count > 0 Then
                Dim newRow As Integer = grdCampaigns.Rows.Add
                grdCampaigns.Rows(newRow).Tag = tmpcmp
            End If
        Next
        lblCount.Text = grdCampaigns.Rows.Count.ToString
        RegularDB = False
        btnExtractNewCampaign.Enabled = True
    End Sub

    Private Sub grdCampaigns_CellValueNeeded(sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdCampaigns.CellValueNeeded
        Dim tmpCamp As CampaignEssentials = grdCampaigns.Rows(e.RowIndex).Tag
        If tmpCamp Is Nothing Then Exit Sub

        Select Case grdCampaigns.Rows(e.RowIndex).Cells(e.ColumnIndex).OwningColumn.HeaderText
            Case Is = "ID"
                e.Value = tmpCamp.id
            Case Is = "Name"
                e.Value = tmpCamp.name
            Case Is = "Status"
                e.Value = tmpCamp.status
                If grdCampaigns.Rows(e.RowIndex).Tag.status = "Planned" Then
                    e.Value = "Planned"
                ElseIf grdCampaigns.Rows(e.RowIndex).Tag.status = "Running" Then
                    e.Value = "Running"
                ElseIf grdCampaigns.Rows(e.RowIndex).Tag.status = "Finished" Then
                    e.Value = "Finished"
                ElseIf grdCampaigns.Rows(e.RowIndex).Tag.status = "Cancelled" Then
                    e.Value = "Cancelled"
                End If
            'Case Is = "Planner"
            '    If colBuyer.Items.Contains(tmpCamp.buyer) Then
            '        e.Value = tmpCamp.buyer
            '    Else
            '        e.Value = ""
            '    End If
            'Case Is = "Buyer"
            '    If colPlanner.Items.Contains(tmpCamp.planner) Then
            '        e.Value = tmpCamp.planner
            '    Else
            '        e.Value = ""
            '    End If
            Case Is = "Last saved"
                e.Value = tmpCamp.lastsaved
        End Select
    End Sub

    Private Sub grdCampaigns_CellValuePushed(sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdCampaigns.CellValuePushed

        Dim tmpCamp As CampaignEssentials = grdCampaigns.Rows(e.RowIndex).Tag
        Dim TmpCol As Windows.Forms.DataGridViewColumn = grdCampaigns.Columns(e.ColumnIndex)

        Select Case TmpCol.HeaderText
            Case "Status"
                Dim tmpStatus = e.Value
                If Not DBReader.updateCampaignStatus(grdCampaigns.Rows(e.RowIndex).Tag.id, tmpStatus) Then
                    MessageBox.Show("Error while updating status on " + grdCampaigns.Rows(e.RowIndex).Tag.Name, "T R I N I T Y")
                    grdCampaigns.Rows(e.RowIndex).DefaultCellStyle.ForeColor = System.Drawing.Color.Red
                    btnReloadCampaigns.Enabled = False
                Else
                    grdCampaigns.Columns(e.ColumnIndex).DefaultCellStyle.SelectionBackColor = Color.Green
                    btnReloadCampaigns.Enabled = True
                End If
            Case "Buyer"
                Dim tmpBuyer = e.Value
                If Not DBReader.updateCampaignBuyer(grdCampaigns.Rows(e.RowIndex).Tag.id, tmpBuyer) Then
                    MessageBox.Show("Error while updating buyer on " + grdCampaigns.Rows(e.RowIndex).Tag.Name, "T R I N I T Y")
                    grdCampaigns.Rows(e.RowIndex).DefaultCellStyle.BackColor = System.Drawing.Color.Red
                    btnReloadCampaigns.Enabled = False
                Else
                    grdCampaigns.Columns(e.ColumnIndex).DefaultCellStyle.BackColor = Color.Green
                    btnReloadCampaigns.Enabled = True
                End If
            Case "Planner"
                Dim tmpPlanner = e.Value
                If Not DBReader.updateCampaignPlanner(grdCampaigns.Rows(e.RowIndex).Tag.id, tmpPlanner) Then
                    MessageBox.Show("Error while updating buyer on " + grdCampaigns.Rows(e.RowIndex).Tag.Name, "T R I N I T Y")
                    btnReloadCampaigns.Enabled = False
                Else
                    grdCampaigns.Columns(e.ColumnIndex).DefaultCellStyle.ForeColor = Color.Green
                    btnReloadCampaigns.Enabled = True
                End If
        End Select
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
    Private Sub PopulateClients()

        If Clientlist Is Nothing Then
            Dim dt As DataTable = DBReader.getAllClients()
            Clientlist = New List(Of Client)
            For Each item As Object In dt.Rows
                Dim tmpClient As New Client
                tmpClient.id = item!id
                tmpClient.name = item!name
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

        PopulateList()

    End Sub


    Public Sub PopulateBuyer()

        'colBuyer.ValueMember = "id"
        'colBuyer.DisplayMember = "name"
        'Dim selectedIndex As Integer = 0
        'For Each tmpPerson As Trinity.cPerson In DBReader.getAllPeople
        '    If tmpPerson.Name = TrinitySettings.UserName Then
        '        selectedIndex = colBuyer.Items.Add(tmpPerson.Name)
        '    Else
        '        colBuyer.Items.Add(tmpPerson.Name)
        '    End If
        'Next
        'cmbBy.SelectedIndex = selectedIndex
    End Sub
    Public Sub PopulatePlanner()

        'colPlanner.ValueMember = "id"
        'colPlanner.DisplayMember = "name"
        'Dim selectedIndex As Integer = 0
        'For Each tmpPerson As Trinity.cPerson In DBReader.getAllPeople
        '    If tmpPerson.Name = TrinitySettings.UserName Then
        '        selectedIndex = colPlanner.Items.Add(tmpPerson.Name)
        '    Else
        '        colPlanner.Items.Add(tmpPerson.Name)
        '    End If
        'Next
        'cmbBy.SelectedIndex = selectedIndex
    End Sub

    Private Sub txtSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSearch.TextChanged
        tmrKeypress.Enabled = False
        tmrKeypress.Enabled = True
    End Sub

    Private Sub tmrKeypress_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrKeypress.Tick
        If txtSearch.TextLength <> 0 Then
            changeDB()
            tmrKeypress.Enabled = False
            Exit Sub
        Else
            changeDB()
        End If

        tmrKeypress.Enabled = False
    End Sub

    Private Sub cmbBy_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbBy.SelectedIndexChanged
        changeDB()
    End Sub

    Private Sub cmbStatus_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbStatus.SelectedIndexChanged
        changeDB()
    End Sub

    Private Sub cmbMonth_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbMonth.SelectedIndexChanged
        changeDB()
    End Sub

    Private Sub cmbYear_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbYear.SelectedIndexChanged
        changeDB()
    End Sub

    Private Sub btnApplyToCampaign_Click(sender As System.Object, e As System.EventArgs)
        changeDB()
    End Sub

    Private Sub cmbClient_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbClient.SelectedIndexChanged
        If cmbClient.SelectedItem.GetType.FullName = "System.String" Then
            PopulateProducts(-1)
        Else
            PopulateProducts(cmbClient.SelectedItem.id)
        End If
    End Sub

    Private Sub btnReloadCampaigns_Click(sender As System.Object, e As System.EventArgs) Handles btnReloadCampaigns.Click
        changeDB()
        btnReloadCampaigns.Enabled = False
    End Sub

    Private Sub cmbChangeDB_SelectedValueChanged(sender As System.Object, e As System.EventArgs) Handles cmbChangeDB.SelectedValueChanged
        If cmbChangeDB.SelectedItem = "RegularDB" Then
            PopulateList()
        Else
            PopulateListFromBackupDB()
        End If
    End Sub
    Private Sub btnExtractNewCampaign_Click(sender As System.Object, e As System.EventArgs) Handles btnExtractNewCampaign.Click

        If RegularDB Then Exit Sub

        Dim tmpCampaignID As Integer = 0
        Dim tmpCampaignName As String = ""
        For Each cell As DataGridViewCell In grdCampaigns.SelectedCells
            tmpCampaignID = cell.Value
        Next

        Try
            If DBReader.RecoverCampaignFromBackup(tmpCampaignID) Then
                MessageBox.Show("The campaign has been added to the regular database", "T R I N I T Y")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub grdCampaigns_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCampaigns.CellContentClick
        If Not RegularDB Then
            btnExtractNewCampaign.Enabled = True
        Else
            btnExtractNewCampaign.Enabled = False
        End If
    End Sub
End Class