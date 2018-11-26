Public Partial Class booking
    Inherits System.Web.UI.Page

    Private Sub booking_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Stop
    End Sub

    Private Sub addWeekNumberColumn(ByVal Day As DateTime)

        Dim curMonth As DateTime = Day
        curMonth = Convert.ToDateTime(curMonth.Year.ToString() + "-" + curMonth.Month.ToString() + "-01")
        Dim jscript As String = "<script type='text/javascript'> addWkColumn('" + calDates.ClientID + "', " + getISOWeek(curMonth).ToString() + ");</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "AddWeeknumbers", jscript)

    End Sub

    Private Function getISOWeek(ByVal day As DateTime)
        Dim TmpInt As Integer
        TmpInt = System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(day, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)
        If day.DayOfWeek = DayOfWeek.Monday Then TmpInt -= 1
        Return TmpInt
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            'add our popup onclick attribute to the desired element on the page
            lnkFindStore.Attributes.Add("onclick", "window.open('findstore.aspx',null,'height=400, width=500, status=no, resizable=no, scrollbars=no, toolbar=no,location=no, menubar=no, dependent=yes');")

            addWeekNumberColumn(Now)
            hdnDate.Value = Now
            Session("Dates") = Nothing
            cmbCompany.Items.Clear()
            cmbCompany.Items.Add(New ListItem("MEC Access väljer", 0))
            cmbCompany.DataTextField = "FirstName"
            cmbCompany.DataValueField = "ID"
            'If Master.Database.GetProviders(Request.QueryString("id")) Is Nothing Then
            '    pnlProvider.Visible = False
            '    pnlSalesman.Visible = False
            '    pnlNoAccess.Visible = True
            '    Exit Sub
            'End If
            cmbCompany.DataSource = Master.Database.GetProviders(Request.QueryString("id"))
            cmbCompany.DataBind()
            cmbCompany.Items.Add(New ListItem("Annat...", -1))

            If Request.QueryString("bookingid") IsNot Nothing Then
                Dim TmpRow As DataRow = Master.Database.GetBooking(Request.QueryString("bookingid"))
                If Master.Database.GetLoggedInUserInfo.Type = cUserInfo.UserTypeEnum.Provider Then
                    If TmpRow!chosenprovider <> Master.Database.GetLoggedInUserInfo.ID Then
                        pnlProvider.Visible = False
                        pnlSalesman.Visible = False
                        pnlNoAccess.Visible = True
                        Exit Sub
                    End If
                ElseIf Master.Database.GetLoggedInUserInfo.Type = cUserInfo.UserTypeEnum.HeadOfSales Then
                    If TmpRow!ClientID <> Master.Database.GetLoggedInUserInfo.clientID Then
                        pnlProvider.Visible = False
                        pnlSalesman.Visible = False
                        pnlNoAccess.Visible = True
                        Exit Sub
                    End If
                ElseIf TmpRow Is Nothing OrElse TmpRow!StaffID <> Master.Database.GetLoggedInUserInfo.ID Then
                    pnlProvider.Visible = False
                    pnlSalesman.Visible = False
                    pnlNoAccess.Visible = True
                    Exit Sub
                End If

                Dim Confirmed As Boolean = TmpRow!confirmed = 1
                txtStore.Text = GetDBString(TmpRow!Store)
                txtAddress.Text = GetDBString(TmpRow!Address)
                txtCity.Text = GetDBString(TmpRow!city)
                txtCompany.Text = GetDBString(TmpRow!otherprovider)
                txtContact.Text = GetDBString(TmpRow!Contact)
                txtPhoneNr.Text = GetDBString(TmpRow!phone)
                txtPlacement.Text = GetDBString(TmpRow!Placement)
                txtWishlist.Text = GetDBString(TmpRow!Comments)
                chkKitchen.Checked = GetDBInt(TmpRow!storehaskitchen)
                cmbCompany.SelectedValue = GetDBInt(TmpRow!providerid)
                cmbCompany_SelectedIndexChanged(New Object, New EventArgs)

                lblStore.Text = GetDBString(TmpRow!Store)
                lblAddress.Text = GetDBString(TmpRow!Address)
                lblCity.Text = GetDBString(TmpRow!city)
                lblContact.Text = GetDBString(TmpRow!Contact)
                lblPhoneNr.Text = GetDBString(TmpRow!phone)
                lblPlacement.Text = GetDBString(TmpRow!Placement)
                lblWishlist.Text = GetDBString(TmpRow!Comments)
                lblProvider.Text = GetDBString(TmpRow!chosenprovidername)
                txtStaffName.Text = GetDBString(TmpRow!chosenproviderstaffname)
                lblStaffName.Text = GetDBString(TmpRow!chosenproviderstaffname)
                If Not GetDBInt(TmpRow!StoreHasKitchen) Then
                    lblKitchen.Text = "-"
                End If
                If TmpRow!confirmedbyprovider Then
                    pnlButtons.Visible = False
                    lblBack.Visible = True
                End If
                If Master.Database.GetLoggedInUserInfo.Type <> cUserInfo.UserTypeEnum.Provider Then
                    txtStaffName.Visible = False
                    lblStaffName.Visible = True
                    cmdChangeStaffName.Visible = False
                Else
                    cmdChangeStaffName.Visible = True
                    txtStaffName.Visible = True
                    lblStaffName.Visible = False
                End If

                'If Master.Database.GetLoggedInUserInfo.Type = cUserInfo.UserTypeEnum.Provider Then
                lblSalesPerson.Text = TmpRow!Salesman
                lblCompany.Text = TmpRow!Client
                lblMobile.Text = TmpRow!MobilePhone
                'End If
                Session("Dates") = Master.Database.GetBookingDates(Request.QueryString("bookingid"))
                Dim TmpDT As DataTable = Master.Database.GetBookingProducts(Request.QueryString("bookingid"))
                cmbProducts.SelectedValue = TmpDT.Rows.Count
                Dim i As Integer = 1
                For Each TmpRow In TmpDT.Rows
                    DirectCast(tblProducts.FindControl("txtProduct" & i), TextBox).Text = TmpRow!Product
                    i += 1
                Next
                cmbProducts_SelectedIndexChanged(New Object, New EventArgs)
                TmpDT = Master.Database.GetBookingCollaborations(Request.QueryString("bookingid"))
                If TmpDT.Rows.Count > 0 Then
                    chkCollaboration.Checked = True
                    lblCollaboration.Text = "Ja"
                Else
                    lblCollaboration.Text = "Nej"
                End If
                chkCollaboration_CheckedChanged(New Object, New EventArgs)
                cmbCollaborations.SelectedValue = TmpDT.Rows.Count
                i = 1
                For Each TmpRow In TmpDT.Rows
                    DirectCast(tblCollaboration.FindControl("rowCollaboration" & i).FindControl("txtPartnerCompany" & i), TextBox).Text = GetDBString(TmpRow!Company)
                    DirectCast(tblCollaboration.FindControl("rowCollaboration" & i).FindControl("txtPartnerProduct" & i), TextBox).Text = GetDBString(TmpRow!Product)
                    DirectCast(tblCollaboration.FindControl("rowCollaboration" & i).FindControl("txtPartnerInvoiceShare" & i), TextBox).Text = GetDBString(TmpRow!shareofinvoice)
                    DirectCast(tblCollaboration.FindControl("rowCollaboration" & i).FindControl("txtPartnerReference" & i), TextBox).Text = GetDBString(TmpRow!Reference)
                    DirectCast(tblCollaboration.FindControl("rowCollaboration" & i).FindControl("txtPartnerPhoneNr" & i), TextBox).Text = GetDBString(TmpRow!PhoneNr)
                    DirectCast(tblCollaboration.FindControl("rowCollaboration" & i).FindControl("txtPartnerInvoiceAddress" & i), TextBox).Text = GetDBString(TmpRow!Address)
                    DirectCast(tblCollaboration.FindControl("rowCollaboration" & i).FindControl("txtPartnerZipCode" & i), TextBox).Text = GetDBString(TmpRow!ZipCode)

                    DirectCast(tblProviderCollaborations.FindControl("rowProviderCollaboration" & i).FindControl("lblPartnerCompany" & i), Label).Text = GetDBString(TmpRow!Company)
                    DirectCast(tblProviderCollaborations.FindControl("rowProviderCollaboration" & i).FindControl("lblPartnerProduct" & i), Label).Text = GetDBString(TmpRow!Product)
                    DirectCast(tblProviderCollaborations.FindControl("rowProviderCollaboration" & i).FindControl("lblPartnerInvoiceShare" & i), Label).Text = GetDBString(TmpRow!shareofinvoice)
                    DirectCast(tblProviderCollaborations.FindControl("rowProviderCollaboration" & i).FindControl("lblPartnerReference" & i), Label).Text = GetDBString(TmpRow!Reference)
                    DirectCast(tblProviderCollaborations.FindControl("rowProviderCollaboration" & i).FindControl("lblPartnerPhoneNr" & i), Label).Text = GetDBString(TmpRow!PhoneNr)
                    DirectCast(tblProviderCollaborations.FindControl("rowProviderCollaboration" & i).FindControl("lblPartnerInvoiceAddress" & i), Label).Text = GetDBString(TmpRow!Address)
                    DirectCast(tblProviderCollaborations.FindControl("rowProviderCollaboration" & i).FindControl("lblPartnerZipCode" & i), Label).Text = GetDBString(TmpRow!ZipCode)
                    DirectCast(tblProviderCollaborations.FindControl("rowProviderCollaboration" & i), TableRow).Visible = True
                    i += 1

                Next
                cmbCollaborations_SelectedIndexChanged(New Object, New EventArgs)
                cmdSend.Text = "Skicka ändring"

                grdProducts.DataSource = Master.Database.GetBookingProducts(Request.QueryString("bookingid"))
                grdProducts.DataBind()

                Dim TmpDocs As DataTable = Master.Database.GetDocuments(Request.QueryString("id"))
                If TmpDocs.Rows.Count > 0 Then
                    Dim TmpInfo As cUserInfo = Master.Database.GetLoggedInUserInfo
                    For i = 0 To TmpDocs.Rows.Count - 1
                        If (TmpDocs.Rows(i)!ShowProviders AndAlso TmpInfo.Type = cUserInfo.UserTypeEnum.Provider) OrElse (TmpDocs.Rows(i)!ShowStaff AndAlso TmpInfo.Type = cUserInfo.UserTypeEnum.Staff) OrElse (TmpDocs.Rows(i)!ShowSalesmen AndAlso TmpInfo.Type = cUserInfo.UserTypeEnum.Salesman) Then
                            rowInstructions.Visible = True
                            Dim TmpImage As String = "Pics/doc.png"
                            If UCase(TmpDocs.Rows(i)!DocType) = ".PDF" Then
                                TmpImage = "Pics/pdf.png"
                            ElseIf UCase(TmpDocs.Rows(i)!DocType) = ".DOC" Then
                                TmpImage = "Pics/word.png"
                            End If
                            cellInstructions.Text = "<a href='fetchdoc.aspx?id=" & TmpDocs.Rows(i)!id & "' target='_new' style='color: #009999'><img src='" & TmpImage & "' border='0' />" & TmpDocs.Rows(i)!Name & "</a>"
                            If i < TmpDocs.Rows.Count - 1 Then
                                cellInstructions.Text &= "<br />"
                            End If
                        End If
                    Next
                End If
                If (Master.Database.GetLoggedInUserInfo.Type = cUserInfo.UserTypeEnum.HeadOfSales OrElse Master.Database.GetLoggedInUserInfo.Type = cUserInfo.UserTypeEnum.Salesman) AndAlso Not Confirmed Then
                    pnlSalesman.Visible = True
                    pnlProvider.Visible = False
                ElseIf Master.Database.GetLoggedInUserInfo.Type = cUserInfo.UserTypeEnum.Provider Then
                    pnlSalesman.Visible = False
                    pnlProvider.Visible = True
                Else
                    pnlSalesman.Visible = False
                    pnlProvider.Visible = True
                    pnlButtons.Visible = False
                    lblBack.Visible = True
                End If
            Else
                If Master.Database.GetLoggedInUserInfo.Type <> cUserInfo.UserTypeEnum.Salesman Then
                    pnlNoAccess.Visible = True
                    pnlSalesman.Visible = False
                    pnlProvider.Visible = False
                    Exit Sub
                End If
                txtProduct1.Text = Master.Database.GetEvent(Request.QueryString("id"))!Product
            End If
        End If
        UpdateDatesList()
    End Sub

    Private Function GetDBString(ByVal Str As Object) As String
        If IsDBNull(Str) Then Return ""
        Return Str
    End Function

    Private Function GetDBDate(ByVal [Date] As Object) As Date
        If IsDBNull([Date]) Then Return New Date(1900, 1, 1)
        Return [Date]
    End Function

    Private Function GetDBInt(ByVal Val As Object) As Integer
        If IsDBNull(Val) Then Return -1
        Return Val
    End Function

    Private Sub lblAddDates_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblAddDates.Click
        'lblAddDates.Visible = Not lblAddDates.Visible
        pnlAddDates.Visible = Not pnlAddDates.Visible
        If pnlAddDates.Visible AndAlso calDates.VisibleDate.ToOADate = 0 Then
            hdnDate.Value = Now
        End If
        addWeekNumberColumn(hdnDate.Value)
    End Sub

    Private Sub cmdAddDates_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddDates.Click
        'Dates.Rows.Add(CDate(txtDates.Text))
        Dates.Rows.Add(Dates.Rows.Count + 1, calDates.SelectedDate, 0)
        UpdateDatesList()
    End Sub

    Sub UpdateDatesList()
        Dim DV As New DataView(Dates())
        DV.Sort = "Date"
        grdDates.DataSource = DV
        grdDates.DataBind()
        If grdDates.Rows.Count > 0 Then
            lblNoDates.Visible = False
        Else
            lblNoDates.Visible = True
        End If
        txtDateCount.Text = grdDates.Rows.Count
        grdProviderDates.DataSource = Master.Database.GetBookingDates(Request.QueryString("bookingid"))
        grdProviderDates.DataBind()
    End Sub

    Private Function Dates() As DataTable
        If Session("Dates") Is Nothing Then
            Dim TmpDT As New DataTable
            TmpDT.Columns.Add("ID", GetType(Integer))
            TmpDT.Columns.Add("Date", GetType(Date))
            TmpDT.Columns.Add("Time", GetType(Integer))
            Dim Key() As DataColumn = {TmpDT.Columns("ID")}
            TmpDT.PrimaryKey = Key
            Session("Dates") = TmpDT
        End If
        Return Session("Dates")
    End Function

    Private Sub chkCollaboration_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCollaboration.CheckedChanged
        tblCollaboration.Visible = chkCollaboration.Checked
    End Sub

    Private Sub cmbCollaborations_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCollaborations.SelectedIndexChanged
        For i As Integer = 1 To cmbCollaborations.SelectedIndex + 1
            tblCollaboration.Rows(i).Visible = True
        Next
        For i As Integer = cmbCollaborations.SelectedIndex + 2 To 3
            tblCollaboration.Rows(i).Visible = False
        Next

    End Sub

    Private Sub cmbCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCompany.SelectedIndexChanged
        txtCompany.Visible = (cmbCompany.SelectedValue = -1)
    End Sub

    Private Sub cmbProducts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProducts.SelectedIndexChanged
        For i As Integer = 0 To cmbProducts.SelectedIndex
            tblProducts.Rows(i).Visible = True
        Next
        For i As Integer = cmbProducts.SelectedIndex + 1 To 2
            tblProducts.Rows(i).Visible = False
        Next
    End Sub

    Private Sub calDates_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles calDates.DayRender
        If Not Master.Database.DayAvailable(e.Day.Date, Request.QueryString("id")) Then
            e.Cell.Text = ""
        End If
    End Sub

    Sub DeleteDate(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dates.Rows.Remove(Dates.Rows.Find(e.CommandArgument))
        UpdateDatesList()
    End Sub

    Private Sub cmdSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSend.Click
        If IsValid Then
            Dim BID As Integer
            For Each TmpRow As DataRow In Dates.Rows
                If TmpRow!Time = 0 Then
                    lblStatus.Text = "Du måste ange en tid på alla demonstrationer."
                    Exit Sub
                End If
            Next

            If Request.QueryString("bookingid") Is Nothing Then
                BID = Master.Database.SaveBooking(Request.QueryString("id"), txtStore.Text, txtAddress.Text, txtCity.Text, txtPhoneNr.Text, txtContact.Text, txtPlacement.Text, txtWishlist.Text, chkKitchen.Checked, cmbCompany.SelectedValue, txtCompany.Text)
            Else
                BID = Request.QueryString("bookingid")
                BID = Master.Database.SaveBooking(Request.QueryString("id"), txtStore.Text, txtAddress.Text, txtCity.Text, txtPhoneNr.Text, txtContact.Text, txtPlacement.Text, txtWishlist.Text, chkKitchen.Checked, cmbCompany.SelectedValue, txtCompany.Text, BID)
                Master.Database.ClearBookingDates(BID)
                Master.Database.ClearBookingProducts(BID)
                Master.Database.ClearCollaborations(BID)
            End If
            For Each TmpRow As DataRow In Dates.Rows
                Master.Database.AddBookingDate(BID, TmpRow!Date, TmpRow!Time)
            Next
            For i As Integer = 1 To cmbProducts.SelectedValue
                Master.Database.AddBookingProduct(BID, Request.Form("ctl00$Main$txtProduct" & i))
            Next
            If chkCollaboration.Checked Then
                For i As Integer = 1 To cmbCollaborations.SelectedValue
                    Master.Database.AddCollaboration(BID, Request.Form("ctl00$Main$txtPartnerCompany" & i), Request.Form("ctl00$Main$txtPartnerProduct" & i), Request.Form("ctl00$Main$txtPartnerInvoiceShare" & i), Request.Form("ctl00$Main$txtPartnerReference" & i), Request.Form("ctl00$Main$txtPartnerInvoiceAddress" & i), Request.Form("ctl00$Main$txtPartnerPhoneNr" & i), Request.Form("ctl00$Main$txtPartnerZipCode" & i))
                Next
            End If
            'Skicka mail
            Response.Redirect("~\default.aspx")
        End If
    End Sub

    Private Sub booking_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        For Each TmpRow As GridViewRow In grdDates.Rows
            If TmpRow.RowType = DataControlRowType.DataRow Then
                If Not Request.Form("optTime" & TmpRow.RowIndex + 1) Is Nothing Then
                    Dates.Rows(TmpRow.RowIndex).Item("Time") = Request.Form("optTime" & TmpRow.RowIndex + 1)
                End If
            End If
        Next
        txtDateCount.Attributes.Add("style", "visibility:hidden")
    End Sub

    Protected Sub cmdAccept_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdAccept.Click
        Master.Database.SetBookingStatus(Request.QueryString("bookingid"), True, txtStaffName.Text)
        Response.Redirect("~\default.aspx")
    End Sub

    Protected Sub cmdReject_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdReject.Click
        Master.Database.SetBookingStatus(Request.QueryString("bookingid"), False, txtStaffName.Text)
        Response.Redirect("~\default.aspx")
    End Sub

    Private Sub calDates_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calDates.SelectionChanged
        addWeekNumberColumn(hdnDate.Value)
    End Sub

    Private Sub calDates_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles calDates.VisibleMonthChanged
        addWeekNumberColumn(e.NewDate)
        hdnDate.Value = e.NewDate
    End Sub

    Private Sub cmdChangeStaffName_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdChangeStaffName.Click
        Master.Database.SetBookingStatus(Request.QueryString("bookingid"), True, txtStaffName.Text)
        Response.Redirect("~\default.aspx")
    End Sub

    Private Sub lnkFindStore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkFindStore.Click

    End Sub


    Private Sub valDates_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles valDates.ServerValidate
        If lblNoDates.Visible Then
            args.IsValid = False
            lblNoDates.ForeColor = Drawing.Color.Red
        Else
            args.IsValid = True
        End If
    End Sub
End Class