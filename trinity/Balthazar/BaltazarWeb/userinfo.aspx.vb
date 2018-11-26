Public Partial Class userinfo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim TmpRow As DataRow = Master.Database.GetUserInfo
        If Not IsPostBack Then
            txtFirstName.Text = GetDBString(TmpRow!FirstName)
            txtLastName.Text = GetDBString(TmpRow!Lastname)
            txtBirthday.Text = Format(GetDBDate(TmpRow!Birthday), "Short date")
            cmbGender.SelectedValue = GetDBInt(TmpRow!gender)
            lstDriver.Items(0).Selected = TmpRow!DriverB
            lstDriver.Items(1).Selected = TmpRow!DriverC
            lstDriver.Items(2).Selected = TmpRow!DriverD
            lstDriver.Items(3).Selected = TmpRow!DriverE
            txtEmail.Text = GetDBString(TmpRow!Email)
            txtAddress1.Text = GetDBString(TmpRow!Address1)
            txtAddress2.Text = GetDBString(TmpRow!Address2)
            txtZipCode.Text = GetDBString(TmpRow!ZipCode)
            txtZipArea.Text = GetDBString(TmpRow!ZipArea)
            txtWorkPhone.Text = GetDBString(TmpRow!WorkPhone)
            txtHomePhone.Text = GetDBString(TmpRow!HomePhone)
            txtMobilePhone.Text = GetDBString(TmpRow!MobilePhone)
            txtBank.Text = GetDBString(TmpRow!Bank)
            txtClearingNo.Text = GetDBString(TmpRow!ClearingNo)
            txtAccountNo.Text = GetDBString(TmpRow!AccountNo)
            txtInfo.Text = GetDBString(TmpRow!Info)
            imgUser.ImageUrl = "~\fetchimage.aspx?id=" & TmpRow!ID

            lstCategories.DataSource = Master.Database.GetCategories
            lstCategories.DataTextField = "Name"
            lstCategories.DataValueField = "ID"
            lstCategories.DataBind()
            For Each TmpRowView As DataRowView In Master.Database.GetSelectedCategoriesForLoggedInUser
                lstCategories.Items.FindByValue(TmpRowView!CategoryID.ToString).Selected = True
            Next
        End If
        If Master.Database.GetLoggedInUserInfo.Type = cUserInfo.UserTypeEnum.Salesman OrElse Master.Database.GetLoggedInUserInfo.Type = cUserInfo.UserTypeEnum.HeadOfSales Then
            rowBirthday.Visible = False
            rowDriver.Visible = False
            rowGender.Visible = False
            rowHomephone.Visible = False
            rowInfo.Visible = False
            rowTasks1.Visible = False
            rowTasks2.Visible = False
            cellPicture.Visible = False
        ElseIf Master.Database.GetLoggedInUserInfo.Type = cUserInfo.UserTypeEnum.Provider Then
            rowBirthday.Visible = False
            rowLastname.Visible = False
            rowDriver.Visible = False
            rowGender.Visible = False
            rowHomephone.Visible = False
            rowInfo.Visible = False
            rowTasks1.Visible = False
            rowTasks2.Visible = False
            cellPicture.Visible = False
            cellFirstName.Text = "Namn"
        End If
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

    Protected Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If Not Page.IsValid Then
            Exit Sub
        End If
        Master.Database.SaveUserInfo(txtFirstName.Text, txtLastName.Text, CDate(txtBirthday.Text), cmbGender.SelectedValue, lstDriver.Items(0).Selected, lstDriver.Items(1).Selected, lstDriver.Items(2).Selected, lstDriver.Items(3).Selected, txtEmail.Text, txtAddress1.Text, txtAddress2.Text, txtZipCode.Text, txtZipArea.Text, txtHomePhone.Text, txtWorkPhone.Text, txtMobilePhone.Text, txtBank.Text, txtClearingNo.Text, txtAccountNo.Text, txtInfo.Text)
        Dim TmpList As New List(Of Integer)
        For Each TmpItem As ListItem In lstCategories.Items
            If TmpItem.Selected Then
                TmpList.Add(TmpItem.Value)
            End If
        Next
        Master.Database.SaveSelectedCategoriesForLoggedInUser(TmpList)
        If txtNewPassword.Text <> "" Then
            If txtNewPassword.Text = txtConfirmPassword.Text Then
                If txtNewPassword.Text.Trim.Length < 4 Then
                    lblPasswordError.Text = "Lösenordet måste innehålla minst 4 tecken."
                    rowPasswordError.Visible = True
                Else
                    Master.Database.ChangePassword(txtNewPassword.Text)
                    lblPasswordError.Text = "Lösenordet sparades."
                    rowPasswordError.Visible = True
                End If
            Else
                lblPasswordError.Text = "Lösenorden stämmer inte överens."
                rowPasswordError.Visible = True
            End If
        Else
            rowPasswordError.Visible = False
        End If
    End Sub

    Private Sub lblChangePassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblChangePassword.Click
        rowChangePassword.Visible = True
    End Sub

    Private Sub lblUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblUpload.Click
        pnlUpload.Visible = Not pnlUpload.Visible
    End Sub

    Private Sub cmdUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpload.Click
        If uplImage.PostedFile Is Nothing OrElse uplImage.PostedFile.ContentLength = 0 Then

            lblStatus.Text = "Ingen fil specificerad."

            Exit Sub

        Else

            Dim TmpByte As Byte()
            Dim fileName As String = uplImage.PostedFile.FileName
            Dim ext As String = fileName.Substring(fileName.LastIndexOf("."))
            ext = ext.ToLower

            If ext = ".jpg" Then

            ElseIf ext = ".bmp" Then

            ElseIf ext = ".gif" Then

            ElseIf ext = "jpg" Then

            ElseIf ext = "bmp" Then

            ElseIf ext = "gif" Then

            Else

                lblStatus.Text = "Endast bmp, gif och jpeg är tillåtet."

                Exit Sub

            End If

            Dim intLength As Integer = Convert.ToInt32(uplImage.PostedFile.InputStream.Length)
            ReDim TmpByte(intLength)

            uplImage.PostedFile.InputStream.Read(TmpByte, 0, intLength)

            If DirectCast(Session("Database"), cDBReader).UploadImage(TmpByte) Then
                lblStatus.Text = "Bilden laddades upp."
            Else
                lblStatus.Text = "Ett fel inträffade. Var vänlig försök igen."
            End If

        End If
    End Sub
End Class