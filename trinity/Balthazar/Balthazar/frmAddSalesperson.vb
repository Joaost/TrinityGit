Public Class frmAddSalesperson

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Dim TmpStaff As cStaff = Database.GetSingleStaff(Database.AddStaff())
        TmpStaff.FirstName = txtFirstName.Text
        TmpStaff.LastName = txtLastName.Text
        TmpStaff.Username = txtLogin.Text
        TmpStaff.Email = txtEmail.Text
        TmpStaff.Password = txtPassword.Text
        TmpStaff.Client = MyEvent.Client
        TmpStaff.Type = cStaff.UserTypeEnum.Salesman
        If chkNotify.Checked Then
            'Send mail
        End If
        Database.SaveStaff(TmpStaff)
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Tag = TmpStaff
    End Sub

    Private Sub cmdGeneratePassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGeneratePassword.Click
        txtPassword.Text = Helper.CreateRandomPassword
    End Sub

    Private Sub frmAddSalesperson_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbRole.SelectedIndex = 0
    End Sub
End Class