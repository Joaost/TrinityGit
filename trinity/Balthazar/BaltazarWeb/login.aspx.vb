Public Partial Class login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("code") <> "" Then
                txtVerifyCode.Text = Request.QueryString("code")
                txtVerifyUsername.Text = Request.QueryString("username")
                lblVerifyAccount_Click(sender, e)
            Else
                'If Request.Cookies("Username") IsNot Nothing Then
                '    txtUsername.Text = Request.Cookies("Username").Value
                '    txtPassword.Text = Request.Cookies("Password").Value
                'End If
                lblLogin_Click(sender, e)
            End If
            Dim browser As HttpBrowserCapabilities = Request.Browser

            If browser.Browser <> "Internet Explorer" OrElse browser.Version < 5 Then
                pnlWrongBrowser.Visible = True
                lblBrowser.Text = browser.Browser
                lblBrowserVersion.Text = browser.Version
            End If
        End If
    End Sub

    'Protected Sub LoginUser_Authenticate(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles LoginUser.Authenticate
    '    If Master.Database.ConnectionState = ConnectionState.Closed Then
    '        Master.Database.Connect()
    '    End If
    '    Dim StaffID As Integer = Master.Database.Login(LoginUser.UserName, LoginUser.Password)
    '    If StaffID > 0 Then e.Authenticated = True
    'End Sub


    Private Sub lblLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblLogin.Click

        lblLogin.Font.Bold = True
        lblCreateAccount.Font.Bold = False
        lblVerifyAccount.Font.Bold = False
        lblRecoverPassword.Font.Bold = False

        tblLogin.Visible = True
        tblCreateAccount.Visible = False
        tblVerifyAccount.Visible = False
        tblRecoverPassword.Visible = False

        HideErrors()

    End Sub


    Private Sub lblVerifyAccount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblVerifyAccount.Click

        lblLogin.Font.Bold = False
        lblCreateAccount.Font.Bold = False
        lblVerifyAccount.Font.Bold = True
        lblRecoverPassword.Font.Bold = False

        tblLogin.Visible = False
        tblCreateAccount.Visible = False
        tblVerifyAccount.Visible = True
        tblRecoverPassword.Visible = False

        HideErrors()

    End Sub


    Private Sub lblCreateAccount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCreateAccount.Click

        lblLogin.Font.Bold = False
        lblCreateAccount.Font.Bold = True
        lblVerifyAccount.Font.Bold = False
        lblRecoverPassword.Font.Bold = False

        tblLogin.Visible = False
        tblCreateAccount.Visible = True
        tblVerifyAccount.Visible = False
        tblRecoverPassword.Visible = False

        HideErrors()

    End Sub


    Private Sub lblRecoverPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblRecoverPassword.Click

        lblLogin.Font.Bold = False
        lblCreateAccount.Font.Bold = False
        lblVerifyAccount.Font.Bold = False
        lblRecoverPassword.Font.Bold = True

        tblLogin.Visible = False
        tblCreateAccount.Visible = False
        tblVerifyAccount.Visible = False
        tblRecoverPassword.Visible = True

        HideErrors()

    End Sub

    Private Sub HideErrors()
        rowCreateUserError.Visible = False
        rowLoginError.Visible = False
        rowVerifyError.Visible = False
    End Sub

    Private Sub cmdLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
        If Master.Database.ConnectionState = ConnectionState.Closed Then
            Master.Database.Connect()
        End If
        Dim StaffID As Integer = Master.Database.Login(txtLogin.Text, txtPassword.Text)
        If StaffID > 0 Then
            FormsAuthentication.SetAuthCookie(txtUsername.Text, False)
            'If chkRememberMe.Checked Then
            '    Response.Cookies("Username").Value = txtLogin.Text
            '    Response.Cookies("Username").Expires = Now.AddYears(1)
            '    Response.Cookies("Password").Value = txtPassword.Text
            '    Response.Cookies("Password").Expires = Now.AddYears(1)
            'End If
            If Request.QueryString("ReturnUrl") IsNot Nothing Then
                Response.Redirect(Request.QueryString("ReturnUrl"))
            Else
                Response.Redirect("~/Default.aspx")
            End If
        ElseIf StaffID = 0 Then
            lblLoginError.Text = "Felaktigt lösenord!"
            rowLoginError.Visible = True
        ElseIf StaffID = -2 Then
            lblLoginError.Text = "Felaktigt användarnamn!"
            rowLoginError.Visible = True
        ElseIf StaffID = -1 Then
            lblLoginError.Text = "Kontot ej verifierat."
            rowLoginError.Visible = True
        End If
    End Sub

    Private Sub cmdCreateAccount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCreateAccount.Click
        If Master.Database.UsernameExists(txtUsername.Text.Trim) Then
            lblCreateUserError.Text = "Användarnamnet finns redan."
            rowCreateUserError.Visible = True
            Exit Sub
        Else
            Dim TmpCode As String = Master.Database.CreateUser(txtUsername.Text.Trim, txtEmail.Text)
            If TmpCode = "" Then
                lblCreateUserError.Text = "Kunde inte skapa konto."
                rowCreateUserError.Visible = True
            Else
                Dim Body As String
                Dim TmpURL As String = "http://instore.mecaccess.com/login.aspx?username=" & txtUsername.Text & "&code=" & TmpCode
                Body = "Hej,<br /><br />Ditt konto hos MEC Access har skapats. Klicka på länken nedan eller klistra in den i din browser för att verifiera din mailadress. Din verifieringskod är " & TmpCode & ".<br /><br><a href='" & TmpURL & "'>" & TmpURL & "</a><br /><br>Mvh<br /><br />MEC Access"
                Master.Database.SendMail(txtEmail.Text, "", "Verifiera ditt konto", Body)
                lblCreateUserError.Text = "Ett verifieringsmail har skickats till " & txtEmail.Text
                rowCreateUserError.Visible = True
            End If
        End If
    End Sub

    Private Sub cmdVerify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVerify.Click
        If txtWantedPassword.Text.Trim <> txtRepeatPassword.Text.Trim Then
            lblVerifyError.Text = "Lösenorden stämmer inte."
            rowVerifyError.Visible = True
            Exit Sub
        ElseIf txtWantedPassword.Text.Trim.Length < 4 Then
            lblVerifyError.Text = "Lösenordet måste vara minst 4 tecken."
            rowVerifyError.Visible = True
            Exit Sub
        End If
        Dim TmpVerify As Integer = Master.Database.VerifyAccount(txtVerifyUsername.Text.Trim, txtVerifyCode.Text.Trim, txtWantedPassword.Text.Trim)
        If TmpVerify = 0 Then
            lblVerifyError.Text = "Felaktig kod."
            rowVerifyError.Visible = True
            Exit Sub
        ElseIf TmpVerify = -1 Then
            lblVerifyError.Text = "Kontot är redan aktiverat."
            rowVerifyError.Visible = True
            Exit Sub
        Else
            Master.Database.Login(txtVerifyUsername.Text, txtWantedPassword.Text)
            FormsAuthentication.SetAuthCookie(txtVerifyUsername.Text, False)
            If Request.QueryString("ReturnUrl") IsNot Nothing Then
                Response.Redirect(Request.QueryString("ReturnUrl"))
            Else
                Response.Redirect("~/Default.aspx")
            End If
        End If
    End Sub

    Private Sub cmdRecover_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRecover.Click
        Master.Database.RecoverPassword(txtRecoverUsername.Text)
    End Sub
End Class