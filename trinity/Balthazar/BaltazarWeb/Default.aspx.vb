Imports System.DBNull

Partial Public Class _Default
    Inherits System.Web.UI.Page
    Public BookedDaysColors() As Drawing.Color = {Drawing.Color.Black, Drawing.Color.Red}

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Master.Database.GetLoggedInUserInfo Is Nothing Then
            FormsAuthentication.RedirectToLoginPage()
            Exit Sub
        End If
        If Not IsPostBack Then
            Select Case Master.Database.GetLoggedInUserInfo.Type

                Case cUserInfo.UserTypeEnum.Staff
                    lstQuestionaires.DataSource = Master.Database.Questionaires
                    lstQuestionaires.DataBind()
                    lstAvailableJobs.DataSource = Master.Database.GetAvailableJobs
                    lstAvailableJobs.DataBind()
                    lstCurrentJobs.DataSource = Master.Database.CurrentJobs
                    lstCurrentJobs.DataBind()
                    lstAnsweredQuestionaires.DataSource = Master.Database.Questionaires(True)
                    lstAnsweredQuestionaires.DataBind()

                    pnlStaff.Visible = True
                    pnlSalesman.Visible = False
                    pnlProvider.Visible = False
                    pnlHeadOfSales.Visible = False
                Case cUserInfo.UserTypeEnum.Salesman
                    If Master.Database.GetLoggedInUserInfo.CanCreateBookings Then
                        lstCampaigns.DataSource = Master.Database.GetAvailableJobs
                        lstCampaigns.DataBind()
                        grdPendingBookings.DataSource = Master.Database.GetBookings(cDBReader.BookingStatusEnum.bsPending)
                        grdPendingBookings.DataBind()
                        grdRejectedBookings.DataSource = Master.Database.GetBookings(cDBReader.BookingStatusEnum.bsRejected)
                        grdRejectedBookings.DataBind()
                    End If
                    grdConfirmedBookings.DataSource = Master.Database.GetBookings(cDBReader.BookingStatusEnum.bsConfirmed)
                    grdConfirmedBookings.DataBind()
                    grdSalesmanQuestionaires.DataSource = Master.Database.Questionaires(True)
                    grdSalesmanQuestionaires.DataBind()

                    pnlStaff.Visible = False
                    pnlSalesman.Visible = True
                    pnlProvider.Visible = False
                    pnlHeadOfSales.Visible = False
                    pnlBooking.Visible = Master.Database.GetLoggedInUserInfo.CanCreateBookings

                Case cUserInfo.UserTypeEnum.Provider
                    grdOfferedBookings.DataSource = Master.Database.GetAvailableBookings
                    grdOfferedBookings.DataBind()
                    grdAcceptedBookings.DataSource = Master.Database.CurrentBookings
                    grdAcceptedBookings.DataBind()
                    Dim Questionaires As ICollection = Master.Database.Questionaires(True)
                    grdAnsweredProviderQuestionaires.DataSource = From Questionaire In Questionaires Select Questionaire Where Questionaire!Answered
                    grdAnsweredProviderQuestionaires.DataBind()
                    grdNotAnsweredProviderQuestionaires.DataSource = From Questionaire In Questionaires Select Questionaire Where Not Questionaire!Answered And Questionaire!AnswerDate < Now
                    grdNotAnsweredProviderQuestionaires.DataBind()

                    pnlStaff.Visible = False
                    pnlSalesman.Visible = False
                    pnlProvider.Visible = True
                    pnlHeadOfSales.Visible = False
                Case cUserInfo.UserTypeEnum.HeadOfSales
                    grdHOSConfirmed.Columns(4).Visible = True
                    grdHOSConfirmed.DataSource = Master.Database.GetBookings(cDBReader.BookingStatusEnum.bsConfirmed)
                    grdHOSConfirmed.DataBind()
                    grdHOSQuestionaires.DataSource = Master.Database.Questionaires(True)
                    grdHOSQuestionaires.DataBind()
                    grdQuestionaireSummary.DataSource = Master.Database.QuestionaireSummaries
                    grdQuestionaireSummary.DataBind()

                    pnlStaff.Visible = False
                    pnlSalesman.Visible = False
                    pnlProvider.Visible = False
                    pnlHeadOfSales.Visible = True
            End Select
        End If
        If lstQuestionaires.Items.Count > 0 Then
            lstQuestionaires.Visible = True
            lblQuestionaires.Visible = False
        Else
            lstQuestionaires.Visible = False
            lblQuestionaires.Visible = True
        End If
        If lstAvailableJobs.Items.Count > 0 Then
            lstAvailableJobs.Visible = True
            lblAvailableJobs.Visible = False
        Else
            lstAvailableJobs.Visible = False
            lblAvailableJobs.Visible = True
        End If
        If lstCurrentJobs.Items.Count > 0 Then
            lstCurrentJobs.Visible = True
            lblCurrentJobs.Visible = False
        Else
            lstCurrentJobs.Visible = False
            lblCurrentJobs.Visible = True
        End If
        If lstAnsweredQuestionaires.Items.Count > 0 Then
            lstAnsweredQuestionaires.Visible = True
            lblAnsweredQuestionaires.Visible = False
        Else
            lstAnsweredQuestionaires.Visible = False
            lblAnsweredQuestionaires.Visible = True
        End If

        If lstCampaigns.Items.Count > 0 Then
            lstCampaigns.Visible = True
            lblNoCampaigns.Visible = False
        Else
            lstCampaigns.Visible = False
            lblNoCampaigns.Visible = True
        End If
        If grdPendingBookings.Rows.Count > 0 Then
            grdPendingBookings.Visible = True
            lblNoPendingBookings.Visible = False
        Else
            grdPendingBookings.Visible = False
            lblNoPendingBookings.Visible = True
        End If
        If grdConfirmedBookings.Rows.Count > 0 Then
            grdConfirmedBookings.Visible = True
            lblNoConfirmedBookings.Visible = False
        Else
            grdConfirmedBookings.Visible = False
            lblNoConfirmedBookings.Visible = True
        End If
        If grdRejectedBookings.Rows.Count > 0 Then
            grdRejectedBookings.Visible = True
            lblNoRejectedBookings.Visible = False
        Else
            grdRejectedBookings.Visible = False
            lblNoRejectedBookings.Visible = True
        End If
        If grdOfferedBookings.Rows.Count > 0 Then
            grdOfferedBookings.Visible = True
            lblNoOfferedBookings.Visible = False
        Else
            grdOfferedBookings.Visible = False
            lblNoOfferedBookings.Visible = True
        End If
        If grdAcceptedBookings.Rows.Count > 0 Then
            grdAcceptedBookings.Visible = True
            lblNoAcceptedBookings.Visible = False
        Else
            grdAcceptedBookings.Visible = False
            lblNoAcceptedBookings.Visible = True
        End If
        If grdAnsweredProviderQuestionaires.Rows.Count > 0 Then
            grdAnsweredProviderQuestionaires.Visible = True
            lblNoAnsweredProviderQuestionaires.Visible = False
        Else
            grdAnsweredProviderQuestionaires.Visible = False
            lblNoAnsweredProviderQuestionaires.Visible = True
        End If
        If grdNotAnsweredProviderQuestionaires.Rows.Count > 0 Then
            grdNotAnsweredProviderQuestionaires.Visible = True
            lblNoNotAnsweredProviderQuestionaires.Visible = False
        Else
            grdNotAnsweredProviderQuestionaires.Visible = False
            lblNoNotAnsweredProviderQuestionaires.Visible = True
        End If
        If grdSalesmanQuestionaires.Rows.Count > 0 Then
            grdSalesmanQuestionaires.Visible = True
            lblNoSalesmanQuestionaires.Visible = False
        Else
            grdSalesmanQuestionaires.Visible = False
            lblNoSalesmanQuestionaires.Visible = True
        End If
        If grdHOSConfirmed.Rows.Count > 0 Then
            grdHOSConfirmed.Visible = True
            lblHOSNoComfirmed.Visible = False
        Else
            grdHOSConfirmed.Visible = False
            lblHOSNoComfirmed.Visible = True
        End If
        If grdHOSQuestionaires.Rows.Count > 0 Then
            grdHOSQuestionaires.Visible = True
            lblHOSNoQuestionaires.Visible = False
        Else
            grdHOSQuestionaires.Visible = False
            lblHOSNoQuestionaires.Visible = True
        End If
        If grdQuestionaireSummary.Rows.Count > 0 Then
            grdQuestionaireSummary.Visible = True
            lblNoQuestionaireSummary.Visible = False
        Else
            grdQuestionaireSummary.Visible = False
            lblNoQuestionaireSummary.Visible = True
        End If
    End Sub

    Protected Sub lnkMail_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkMail.Click
        pnlMailForm.Visible = True
    End Sub

    Protected Sub cmdSendMail_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSendMail.Click
        Dim Mail As New System.Net.Mail.SmtpClient
        Mail.Host = "instore.mecaccess.se"
        Try
            Mail.Send(txtMailFrom.Text, "johan.hogfeldt@mecglobal.com", "Balthazarfråga", txtMailBody.Text)
        Catch ex As Exception
            lblAlertMessage.Text = "Kunde inte skicka mailet. Var vänlig använd ditt vanliga mailprogram och maila till ann.hoglund@mecglobal.com eller karin.branmark@mecglobal.com.<br />Felmeddelande: " & ex.Message
            Exit Sub
        End Try
        lblAlertMessage.Text = "Mailet skickades."
    End Sub

    Function CheckNull(ByVal value As Object) As Integer
        If IsDBNull(value) Then
            Return 0
        Else
            Return value
        End If
    End Function
End Class