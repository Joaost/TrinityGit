Public Class frmEditWebUsers

    Dim UserListByID As New Dictionary(Of Integer, cStaff)

    Private Sub grdProviders_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdProviders.CellValueNeeded
        Dim TmpUser As cStaff = UserListByID(grdProviders.Rows(e.RowIndex).Tag)
        Select Case grdProviders.Columns(e.ColumnIndex).Name
            Case "colName"
                e.Value = TmpUser.Firstname
            Case "colProvEmail"
                e.Value = TmpUser.Email
            Case "colProvLogin"
                e.Value = TmpUser.Username
            Case "colProvPassword"
                e.Value = TmpUser.Password
        End Select
    End Sub

    Private Sub grdProviders_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdProviders.CellValuePushed
        Dim TmpUser As cStaff = UserListByID(grdProviders.Rows(e.RowIndex).Tag)
        Select Case grdProviders.Columns(e.ColumnIndex).Name
            Case "colName"
                TmpUser.Firstname = e.Value
            Case "colProvEmail"
                TmpUser.Email = e.Value
            Case "colProvLogin"
                TmpUser.Username = e.Value
            Case "colProvPassword"
                TmpUser.Password = e.Value
        End Select
        grdProviders.Rows(e.RowIndex).Cells(0).Tag = "CHANGED"
    End Sub

    Private Sub grdUsers_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdUsers.CellValueNeeded
        Dim TmpUser As cStaff = UserListByID(grdUsers.Rows(e.RowIndex).Tag)
        Select Case grdUsers.Columns(e.ColumnIndex).Name
            Case "colFirstName"
                e.Value = TmpUser.Firstname
            Case "colLastName"
                e.Value = TmpUser.LastName
            Case "colEmail"
                e.Value = TmpUser.Email
            Case "colLogin"
                e.Value = TmpUser.Username
            Case "colPassword"
                e.Value = TmpUser.Password
            Case "colRole"
                Select Case TmpUser.Type
                    Case cStaff.UserTypeEnum.Salesman
                        e.Value = "Salesman"
                    Case cStaff.UserTypeEnum.HeadOfSales
                        e.Value = "Head of sales"
                    Case cStaff.UserTypeEnum.Provider
                        e.Value = "Provider"
                End Select
            Case "colClient"
                Dim ClientName As String = ""
                If TmpUser.Client IsNot Nothing Then
                    ClientName = TmpUser.Client.Name
                End If
                e.Value = ClientName
        End Select
    End Sub

    Private Sub grdUsers_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdUsers.CellValuePushed
        Dim TmpUser As cStaff = UserListByID(grdUsers.Rows(e.RowIndex).Tag)
        Select Case grdUsers.Columns(e.ColumnIndex).Name
            Case "colFirstName"
                TmpUser.Firstname = e.Value
            Case "colLastName"
                TmpUser.LastName = e.Value
            Case "colEmail"
                TmpUser.Email = e.Value
            Case "colLogin"
                TmpUser.Username = e.Value
            Case "colPassword"
                TmpUser.Password = e.Value
            Case "colRole"
                Select Case e.Value
                    Case "Salesman"
                        TmpUser.Type = cStaff.UserTypeEnum.Salesman
                    Case "Head of sales"
                        TmpUser.Type = cStaff.UserTypeEnum.HeadOfSales
                End Select
        End Select
        grdUsers.Rows(e.RowIndex).Cells(0).Tag = "CHANGED"
    End Sub

    Private Sub frmEditWebUsers_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        For Each TmpRow As DataGridViewRow In grdUsers.Rows
            If TmpRow.Cells(0).Tag = "CHANGED" Then
                Dim res As DialogResult = Windows.Forms.MessageBox.Show("You have changed user info. Do you want to save your changes?", "BALTHAZAR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If res = Windows.Forms.DialogResult.Yes Then
                    cmdSave_Click(sender, New EventArgs)
                ElseIf res = Windows.Forms.DialogResult.No Then
                    e.Cancel = False
                ElseIf res = Windows.Forms.DialogResult.Cancel Then
                    e.Cancel = True
                End If
                Exit For
            End If
        Next
        For Each TmpRow As DataGridViewRow In grdProviders.Rows
            If TmpRow.Cells(0).Tag = "CHANGED" Then
                Dim res As DialogResult = Windows.Forms.MessageBox.Show("You have changed user info. Do you want to save your changes?", "BALTHAZAR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If res = Windows.Forms.DialogResult.Yes Then
                    cmdSave_Click(sender, New EventArgs)
                ElseIf res = Windows.Forms.DialogResult.No Then
                    e.Cancel = False
                ElseIf res = Windows.Forms.DialogResult.Cancel Then
                    e.Cancel = True
                End If
                Exit For
            End If
        Next
    End Sub

    Private Sub frmEditWebUsers_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim UserListByClient As New SortedDictionary(Of String, cStaff)
        UserListByID.Clear()
        For Each TmpUser As cStaff In Database.GetStaffList(cStaff.UserTypeEnum.Salesman).Values
            Dim ClientName As String = ""
            If TmpUser.Client IsNot Nothing Then
                ClientName = TmpUser.Client.Name
            End If
            UserListByClient.Add(ClientName & TmpUser.LastName & TmpUser.Firstname & TmpUser.Username, TmpUser)
            UserListByID.Add(TmpUser.DatabaseID, TmpUser)
        Next
        For Each TmpUser As cStaff In Database.GetStaffList(cStaff.UserTypeEnum.HeadOfSales).Values
            Dim ClientName As String = ""
            If TmpUser.Client IsNot Nothing Then
                ClientName = TmpUser.Client.Name
            End If
            UserListByClient.Add(TmpUser.Client.Name & TmpUser.LastName & TmpUser.Firstname & TmpUser.Username, TmpUser)
            UserListByID.Add(TmpUser.DatabaseID, TmpUser)
        Next
        For Each TmpUser As cStaff In Database.GetStaffList(cStaff.UserTypeEnum.Provider).Values
            UserListByClient.Add(TmpUser.Firstname & TmpUser.Username & TmpUser.DatabaseID, TmpUser)
            UserListByID.Add(TmpUser.DatabaseID, TmpUser)
        Next
        For Each TmpUser As cStaff In UserListByClient.Values
            If TmpUser.Type = cStaff.UserTypeEnum.Provider Then
                With grdProviders.Rows(grdProviders.Rows.Add)
                    .Tag = TmpUser.DatabaseID
                End With
            Else
                With grdUsers.Rows(grdUsers.Rows.Add)
                    .Tag = TmpUser.DatabaseID
                    DirectCast(.Cells("colRole"), ExtendedComboBoxCell).Items.Clear()
                    DirectCast(.Cells("colRole"), ExtendedComboBoxCell).Items.Add("Salesman")
                    DirectCast(.Cells("colRole"), ExtendedComboBoxCell).Items.Add("Head of sales")
                    DirectCast(.Cells("colRole"), ExtendedComboBoxCell).Items.Add("Provider")
                    DirectCast(.Cells("colRole"), ExtendedComboBoxCell).DropDownStyle = ComboBoxStyle.DropDownList
                End With
            End If
        Next
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        For Each TmpRow As DataGridViewRow In grdUsers.Rows
            If TmpRow.Cells(0).Tag = "CHANGED" Then
                Database.SaveStaff(UserListByID(TmpRow.Tag))
                TmpRow.Cells(0).Tag = ""
            End If
        Next
        For Each TmpRow As DataGridViewRow In grdProviders.Rows
            If TmpRow.Cells(0).Tag = "CHANGED" Then
                Database.SaveStaff(UserListByID(TmpRow.Tag))
                TmpRow.Cells(0).Tag = ""
            End If
        Next

    End Sub

    Private Sub cmdDeleteUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteUser.Click
        If MessageBox.Show("Deleting a user will permanently remove it from the database." & vbCrLf & vbCrLf & "Are you sure you want to delete selected users?", "BALTHAZAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            For Each TmpRow As DataGridViewRow In grdUsers.SelectedRows
                Try
                    Database.DeleteStaff(TmpRow.Tag)
                    grdUsers.Rows.Remove(TmpRow)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next
        End If
    End Sub

    Private Sub cmdRemoveProvider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveProvider.Click
        If MessageBox.Show("Deleting a provider will permanently remove it from the database." & vbCrLf & vbCrLf & "Are you sure you want to delete selected users?", "BALTHAZAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            For Each TmpRow As DataGridViewRow In grdProviders.SelectedRows
                Try
                    Database.DeleteStaff(TmpRow.Tag)
                    grdProviders.Rows.Remove(TmpRow)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next
        End If
    End Sub
End Class