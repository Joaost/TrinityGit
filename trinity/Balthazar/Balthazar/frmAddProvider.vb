Public Class frmAddProvider
    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Dim TmpStaff As cStaff = Database.GetSingleStaff(Database.AddStaff())
        TmpStaff.FirstName = txtFirstName.Text
        TmpStaff.Username = txtLogin.Text
        TmpStaff.Email = txtEmail.Text
        TmpStaff.Password = txtPassword.Text
        TmpStaff.Type = cStaff.UserTypeEnum.Provider
        If chkNotify.Checked Then
            Dim TmpBody As String
            TmpBody = "Ett nytt konto har skapats på http://instore.mecaccess.se. Med hjälp av kontot kan ni logga in och hantera demonstrationer för MEC Access kunder.<br />Logga in med följande uppgifter:<br />Username: " & txtLogin.Text & "<br />Password: " & txtPassword.Text & "<br /><br />Du kan ändra lösenord när du loggat in.<br /><br />Mvh<br />MEC Access"
            Helper.SendMail(txtFirstName.Text, txtEmail.Text, BalthazarSettings.UserName, "johan.hogfeldt@mecglobal.com", "New account", TmpBody)
        End If
        Database.SaveStaff(TmpStaff)
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Tag = TmpStaff
    End Sub

    Private Sub cmdGeneratePassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGeneratePassword.Click
        txtPassword.Text = Helper.CreateRandomPassword
    End Sub

    Private Sub txtEmail_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged

    End Sub
End Class