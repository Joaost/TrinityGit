Imports System.Windows.Forms

Public Class frmExtranet

    Private Class cDocument

        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Private _data() As Byte
        Public Property Data() As Byte()
            Get
                Return _data
            End Get
            Set(ByVal value() As Byte)
                _data = value
            End Set
        End Property

        Private _mimeType As String
        Public Property MIMEType() As String
            Get
                Return _mimeType
            End Get
            Set(ByVal value As String)
                _mimeType = value
            End Set
        End Property

    End Class

    Private UserList As New SortedList(Of String, DataRow)

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If Campaign.PlannedTotCTC > Campaign.BudgetTotalCTC Then
            If Windows.Forms.MessageBox.Show("The allocated budget in this campaign exceeds the Total TV Budget." & vbCrLf & vbCrLf & "Proceed?", "T R I N I T Y", MessageBoxButtons.YesNo, MessageBoxIcon.Information) <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If
        End If
        Using Conn As New SqlClient.SqlConnection("Data Source=" & TrinitySettings.ExtranetDatabaseServer & ";Initial Catalog=" & TrinitySettings.ExtranetDatabase & ";User ID=" & TrinitySettings.ExtranetDBUser & ";pwd=" & TrinitySettings.ExtranetDBPwd)
            Dim CampStr As String = Campaign.SaveCampaign(DoNotSaveToFile:=True, SkipHistory:=True, SkipLab:=True)
            If Campaign.ExtranetDatabaseID = 0 Then
                CampStr = CampStr.Replace("'", "''")
                Dim SQL As String = "INSERT INTO campaign (name,ctc,startdate,enddate,campaign,product) VALUES ('" & Campaign.Name & "'," & Campaign.BudgetTotalCTC & ",'" & Date.FromOADate(Campaign.StartDate) & "','" & Date.FromOADate(Campaign.EndDate) & "','" & CampStr & "','" & Campaign.Product & "');SELECT @@identity"
                Conn.Open()
                Dim Com As New SqlClient.SqlCommand(SQL, Conn)
                Dim ID As Integer = Com.ExecuteScalar
                Com.Parameters.AddWithValue("@cid", ID)
                For Each TmpStr As String In lstAuthorizers.Items
                    Com.CommandText = "INSERT INTO usercampaigns (userid,campaignid,canauthorize) VALUES (@uid,@cid,@ca)"
                    Com.Parameters.Clear()
                    Com.Parameters.AddWithValue("@cid", ID)
                    Com.Parameters.AddWithValue("@uid", UserList(TmpStr)!id)
                    If IsDBNull(UserList(TmpStr)!canauthorize) Then
                        Com.Parameters.AddWithValue("@ca", False)
                    Else
                        Com.Parameters.AddWithValue("@ca", UserList(TmpStr)!canauthorize)
                    End If
                    Com.ExecuteNonQuery()
                Next
                For Each item As ListViewItem In lvwDocuments.Items
                    Com.CommandText = "INSERT INTO docs (campaignid,name,mimetype,data) VALUES (@cid,@name,@mime,@data)"
                    Com.Parameters.Clear()
                    Com.Parameters.AddWithValue("@cid", ID)
                    Com.Parameters.AddWithValue("@name", item.Text)
                    Com.Parameters.AddWithValue("@mime", item.Tag.mimetype)
                    Com.Parameters.AddWithValue("@data", item.Tag.data)
                    Com.ExecuteNonQuery()
                Next
            Else
                Conn.Open()
                Dim SQL As String = "SELECT ctc,authorized FROM campaign WHERE id=" & Campaign.ExtranetDatabaseID
                Dim Com As New SqlClient.SqlCommand(SQL, Conn)
                Dim rd As SqlClient.SqlDataReader = Com.ExecuteReader
                rd.Read()
                If rd!authorized Then
                    If Windows.Forms.MessageBox.Show("This campaign has already been authorized." & vbCrLf & "Uploading it to extranet will set the status to not authorized." & vbCrLf & vbCrLf & "Proceed?", "T R I N I T Y", MessageBoxButtons.YesNo, MessageBoxIcon.Information) <> Windows.Forms.DialogResult.Yes Then
                        Conn.Close()
                        Exit Sub
                    End If
                End If
                rd.Close()

                SQL = "UPDATE campaign SET name=@name,ctc=@ctc,startdate=@sd,enddate=@ed,campaign=@camp,product=@prod,authorized='false' WHERE id=@cid"
                Com.CommandText = SQL
                Com.Parameters.AddWithValue("@cid", Campaign.ExtranetDatabaseID)
                Com.Parameters.AddWithValue("@name", Campaign.Name)
                Com.Parameters.AddWithValue("@ctc", Campaign.BudgetTotalCTC)
                Com.Parameters.AddWithValue("@sd", Date.FromOADate(Campaign.StartDate))
                Com.Parameters.AddWithValue("@ed", Date.FromOADate(Campaign.EndDate))
                Com.Parameters.AddWithValue("@prod", Campaign.Product)
                Com.Parameters.AddWithValue("@camp", CampStr)
                Com.ExecuteNonQuery()

                Com.CommandText = "DELETE FROM usercampaigns WHERE campaignid=@cid"
                Com.ExecuteNonQuery()

                For Each TmpStr As String In lstAuthorizers.Items
                    Com.CommandText = "INSERT INTO usercampaigns (userid,campaignid,canauthorize) VALUES (@uid,@cid,@ca)"
                    Com.Parameters.Clear()
                    Com.Parameters.AddWithValue("@cid", Campaign.ExtranetDatabaseID)
                    Com.Parameters.AddWithValue("@uid", UserList(TmpStr)!id)
                    If IsDBNull(UserList(TmpStr)!canauthorize) OrElse Not UserList(TmpStr)!canauthorize Then
                        Com.Parameters.AddWithValue("@ca", False)
                    Else
                        Com.Parameters.AddWithValue("@ca", True)
                    End If
                    Com.ExecuteNonQuery()
                Next

                Com.CommandText = "DELETE FROM docs WHERE campaignid=@cid"
                Com.ExecuteNonQuery()

                For Each item As ListViewItem In lvwDocuments.Items
                    Com.CommandText = "INSERT INTO docs (campaignid,name,mimetype,data) VALUES (@cid,@name,@mime,@data)"
                    Com.Parameters.Clear()
                    Com.Parameters.AddWithValue("@cid", Campaign.ExtranetDatabaseID)
                    Com.Parameters.AddWithValue("@name", item.Text)
                    Com.Parameters.AddWithValue("@mime", item.Tag.mimetype)
                    Com.Parameters.AddWithValue("@data", item.Tag.data)
                    Com.ExecuteNonQuery()
                Next

            End If
            Conn.Close()
        End Using
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmExtranet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Using Conn As New SqlClient.SqlConnection("Data Source=" & TrinitySettings.ExtranetDatabaseServer & ";Initial Catalog=" & TrinitySettings.ExtranetDatabase & ";User ID=" & TrinitySettings.ExtranetDBUser & ";pwd=" & TrinitySettings.ExtranetDBPwd)
            Conn.Open()
            Dim com As New SqlClient.SqlCommand("SELECT *,lastname+', '+firstname as fullname,(SELECT canauthorize FROM usercampaigns WHERE userid=users.id and campaignid=" & Campaign.ExtranetDatabaseID & ") as canauthorize,(SELECT id FROM usercampaigns WHERE userid=users.id and campaignid=" & Campaign.ExtranetDatabaseID & ") as canaccess FROM users WHERE trinityclientid=" & Campaign.ClientID, Conn)
            Dim rd As SqlClient.SqlDataReader = com.ExecuteReader
            Dim dt As New DataTable
            dt.Load(rd)
            dt.Columns("canaccess").ReadOnly = False
            dt.Columns("canauthorize").ReadOnly = False

            UserList.Clear()
            For Each TmpRow As DataRow In dt.Rows
                UserList.Add(TmpRow!fullname, TmpRow)
            Next

            lstUsers.Items.Clear()
            lstAuthorizers.Items.Clear()
            For Each TmpString As String In UserList.Keys
                Dim NewIndex As Integer = lstUsers.Items.Add(TmpString)
                If Not IsDBNull(UserList(TmpString)!canaccess) Then
                    UserList(TmpString)!canaccess = True
                    UserList(TmpString).AcceptChanges()
                    lstUsers.SetItemCheckState(NewIndex, CheckState.Checked)
                    lstAuthorizers.Items.Add(lstUsers.Items(NewIndex))
                Else
                    UserList(TmpString)!canaccess = False
                    UserList(TmpString).AcceptChanges()
                End If
                If Not IsDBNull(UserList(TmpString)!canauthorize) AndAlso UserList(TmpString)!canauthorize Then
                    lstAuthorizers.SetItemChecked(lstAuthorizers.FindString(UserList(TmpString)!fullname), True)
                End If
            Next

            lvwDocuments.Items.Clear()
            com.CommandText = "SELECT * FROM docs WHERE campaignid=" & Campaign.ExtranetDatabaseID
            rd = com.ExecuteReader
            While rd.Read
                Dim TmpDoc As New cDocument
                TmpDoc.Name = rd!Name
                TmpDoc.MIMEType = rd!Mimetype
                TmpDoc.Data = rd!Data
                With lvwDocuments.Items.Add(TmpDoc.Name)
                    .Tag = TmpDoc
                    .ImageKey = TmpDoc.MIMEType
                End With
            End While
            Conn.Close()

        End Using

    End Sub

    Private Sub lstUsers_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles lstUsers.ItemCheck
        If e.CurrentValue = CheckState.Checked AndAlso e.NewValue = CheckState.Unchecked AndAlso UserList(lstUsers.Items(e.Index))!canaccess = 1 Then
            lstAuthorizers.Items.Remove(lstUsers.Items(e.Index))
            UserList(lstUsers.Items(e.Index))!canaccess = False
            UserList(lstUsers.Items(e.Index))!canauthorize = False
        ElseIf e.CurrentValue = CheckState.Unchecked AndAlso e.NewValue = CheckState.Checked AndAlso Not UserList(lstUsers.Items(e.Index))!canaccess = 1 Then
            lstAuthorizers.Items.Add(lstUsers.Items(e.Index))
            UserList(lstUsers.Items(e.Index))!canaccess = True
        End If
    End Sub

    Private Sub cmdAddDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddDoc.Click
        Dim dlgOpen As New OpenFileDialog
        Dim MIMEType As New Hashtable

        MIMEType.Add("PDF", "application/pdf")
        MIMEType.Add("XLS", "application/msexcel")
        MIMEType.Add("DOC", "application/msexcel")
        MIMEType.Add("PPT", "application/mspowerpoint")
        MIMEType.Add("TXT", "text/plain")

        dlgOpen.Filter = "All files|*.*"
        If dlgOpen.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim Doc As New cDocument
            Doc.Name = dlgOpen.FileName.Substring(dlgOpen.FileName.LastIndexOf("\") + 1)
            If MIMEType.ContainsKey(dlgOpen.FileName.Substring(dlgOpen.FileName.LastIndexOf(".") + 1).ToUpper) Then
                Doc.MIMEType = MIMEType(dlgOpen.FileName.Substring(dlgOpen.FileName.LastIndexOf(".") + 1).ToUpper)
            Else
                Doc.MIMEType = "Unknown"
            End If
            Doc.Data = My.Computer.FileSystem.ReadAllBytes(dlgOpen.FileName)
            With lvwDocuments.Items.Add(Doc.Name)
                .ImageKey = Doc.MIMEType
                .Tag = Doc
            End With
        End If
    End Sub

    Private Sub cmdRemoveDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveDoc.Click
        For Each doc As ListViewItem In lvwDocuments.SelectedItems
            doc.Remove()
        Next
    End Sub
End Class
