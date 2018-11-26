Public Partial Class workschedule
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            grdSchedule.DataSource = Master.Database.GetConfirmedShifts(Request.QueryString("id"))
            grdSchedule.DataBind()
        End If
    End Sub

End Class