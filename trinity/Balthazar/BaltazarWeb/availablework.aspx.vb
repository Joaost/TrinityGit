Public Partial Class availablework
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            grdSchedule.DataSource = Master.Database.GetAvailableShifts(Request.QueryString("id"))
            grdSchedule.DataBind()
        End If
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If Not Page.IsValid Then
            Exit Sub
        End If
        For Each TmpRow As GridViewRow In grdSchedule.Rows
            Master.Database.UpdateAvailableShift(DirectCast(TmpRow.FindControl("lblID"), Label).Text, DirectCast(TmpRow.FindControl("chkChecked"), CheckBox).Checked)
        Next
        Response.Redirect("~/Default.aspx")

    End Sub
End Class