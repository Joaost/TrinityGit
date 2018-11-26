Partial Public Class login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblLogin_Click(sender, e)
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
        lblRecoverPassword.Font.Bold = False

        tblLogin.Visible = True
        tblRecoverPassword.Visible = False

        HideErrors()

    End Sub

    Private Sub lblRecoverPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblRecoverPassword.Click

        lblLogin.Font.Bold = False
        lblRecoverPassword.Font.Bold = True

        tblLogin.Visible = False
        tblRecoverPassword.Visible = True

        HideErrors()

    End Sub

    Private Sub HideErrors()
        rowLoginError.Visible = False
    End Sub

    Private Sub cmdLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLogin.Click
        Dim UserID As Integer = Master.Database.Login(txtLogin.Text, txtPassword.Text)
        If UserID > 0 Then
            FormsAuthentication.SetAuthCookie(txtLogin.Text, False)
            If Request.QueryString("ReturnUrl") IsNot Nothing Then
                Response.Redirect(Request.QueryString("ReturnUrl"))
            Else
                Response.Redirect("~/Default.aspx")
            End If
        Else
            lblLoginError.Text = "Invalid username or password!"
            rowLoginError.Visible = True
        End If
    End Sub

    Private Sub cmdRecover_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRecover.Click
        Master.Database.RecoverPassword(txtRecoverUsername.Text)
    End Sub
End Class