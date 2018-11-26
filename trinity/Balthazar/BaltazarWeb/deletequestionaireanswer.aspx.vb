Public Partial Class deletequestionaireanswer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub cmdNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNo.Click
        Response.Redirect("~/Default.aspx")
    End Sub

    Protected Sub cmdYes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdYes.Click
        Master.Database.DeleteQuestionAnswer(Request.QueryString("answerid"))
        Response.Redirect("~/Default.aspx")
    End Sub
End Class