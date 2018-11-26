Public Partial Class _default1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            grdPendingCampaigns.DataSource = Master.Database.Campaigns(False)
            grdPendingCampaigns.DataBind()
            grdAuthorizedCampaigns.DataSource = Master.Database.Campaigns(True)
            grdAuthorizedCampaigns.DataBind()

            If grdPendingCampaigns.Rows.Count > 0 Then
                grdPendingCampaigns.Visible = True
                lblNoPendingCampaigns.Visible = False
            Else
                grdPendingCampaigns.Visible = False
                lblNoPendingCampaigns.Visible = True
            End If
            If grdAuthorizedCampaigns.Rows.Count > 0 Then
                grdAuthorizedCampaigns.Visible = True
                lblNoAuthorizedCampaigns.Visible = False
            Else
                grdAuthorizedCampaigns.Visible = False
                lblNoAuthorizedCampaigns.Visible = True
            End If

        End If
    End Sub

End Class