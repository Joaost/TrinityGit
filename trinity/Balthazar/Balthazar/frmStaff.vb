Public Class frmStaff

    Public CurrentStaff As cStaff
    Private StaffList As Dictionary(Of Integer, cStaff)
    Private NoPicture As Bitmap

    Private Sub tpCatalog_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpCatalog.Enter
        lvwStaff.Clear()
        For Each TmpStaff As cStaff In Database.GetStaffList.Values
            Dim TmpItem As New ListViewItem
            TmpItem.Tag = TmpStaff
            TmpItem.Text = TmpStaff.LastName & ", " & TmpStaff.Firstname
            TmpItem.ImageIndex = TmpStaff.Gender - 1
            lvwStaff.Items.Add(TmpItem)
        Next
    End Sub

    Private Sub lvwStaff_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles lvwStaff.ItemSelectionChanged

    End Sub

    Private Sub lvwStaff_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvwStaff.SelectedIndexChanged
        If lvwStaff.SelectedItems.Count = 0 Then
            grpUser.Visible = False
            Exit Sub
        End If
        CurrentStaff = lvwStaff.SelectedItems(0).Tag

        txtFirstName.Text = CurrentStaff.FirstName
        txtLastName.Text = CurrentStaff.LastName
        dtBirthday.Value = Date.FromOADate(CurrentStaff.Birthday.ToOADate)
        txtAge.Text = CurrentStaff.Age
        lstDriver.Tag = "UPDATING"
        lblDriver.Text = ""
        For i As Integer = 0 To lstDriver.Items.Count - 1
            lstDriver.SetItemChecked(i, False)
        Next
        If CurrentStaff.Driver And cStaff.DriverEnum.driverB Then
            lblDriver.Text &= ",B"
            lstDriver.SetItemChecked(0, True)
        End If
        If CurrentStaff.Driver And cStaff.DriverEnum.driverC Then
            lblDriver.Text &= ",C"
            lstDriver.SetItemChecked(1, True)
        End If
        If CurrentStaff.Driver And cStaff.DriverEnum.driverD Then
            lblDriver.Text &= ",D"
            lstDriver.SetItemChecked(2, True)
        End If
        If CurrentStaff.Driver And cStaff.DriverEnum.driverE Then
            lblDriver.Text &= ",E"
            lstDriver.SetItemChecked(3, True)
        End If
        If lblDriver.Text = "" Then
            lblDriver.Text = "None"
        ElseIf lblDriver.Text = ",B,C,D,E" Then
            lblDriver.Text = "All"
        Else
            lblDriver.Text = lblDriver.Text.Substring(1)
        End If
        lstDriver.Tag = ""
        Select Case CurrentStaff.Gender
            Case cStaff.GenderEnum.Female
                optFemale.Checked = True
                optMale.Checked = False
            Case cStaff.GenderEnum.Male
                optFemale.Checked = False
                optMale.Checked = True
        End Select
        txtEmail.Text = CurrentStaff.Email
        txtAddress1.Text = CurrentStaff.Adress1
        txtAddress2.Text = CurrentStaff.Adress2
        txtZipCode.Text = CurrentStaff.ZipCode
        txtCity.Text = CurrentStaff.ZipArea
        txtHomePhone.Text = CurrentStaff.HomePhone
        txtWorkPhone.Text = CurrentStaff.WorkPhone
        txtMobilePhone.Text = CurrentStaff.MobilePhone
        txtBank.Text = CurrentStaff.Bank
        txtClearing.Text = CurrentStaff.ClearingNo
        txtAccount.Text = CurrentStaff.AccountNo
        txtExternalInfo.Text = CurrentStaff.ExternalInfo
        txtInternalInfo.Text = CurrentStaff.InternalInfo
        txtUserName.Text = CurrentStaff.Username
        txtPassword.Text = CurrentStaff.Password
        If Not CurrentStaff.Picture Is Nothing Then
            picPicture.Image = CurrentStaff.Picture
        Else
            picPicture.Image = NoPicture
        End If

        lstAvailableForCategories.Items.Clear()
        Dim TmpListChosen As List(Of Integer) = Database.GetSelectedCategoriesForStaff(CurrentStaff.DatabaseID)
        For Each TmpCategory As cStaffCategory In Database.StaffCategories
            lstAvailableForCategories.Items.Add(TmpCategory, TmpListChosen.Contains(TmpCategory.DatabaseID))
        Next

        grdCV.Rows.Clear()
        For Each TmpEntry As cCVEntry In Database.GetCVForStaff(CurrentStaff.DatabaseID)
            grdCV.Rows(grdCV.Rows.Add).Tag = TmpEntry
        Next

        Me.SuspendLayout()
        grpUser.Visible = True
        Me.ResumeLayout()
    End Sub

    Sub UpdateLabels()
        For Each TmpItem As ListViewItem In lvwStaff.Items
            Dim TmpStaff As cStaff = TmpItem.Tag
            If TmpStaff.SavedToDB Then
                TmpItem.Text = TmpStaff.LastName & ", " & TmpStaff.FirstName
            Else
                TmpItem.Text = TmpStaff.LastName & ", " & TmpStaff.FirstName & "*"
            End If
            TmpItem.ImageIndex = TmpStaff.Gender - 1
        Next
    End Sub

    Private Sub txtFirstName_KeyUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFirstName.KeyUp
        CurrentStaff.FirstName = sender.text
        grpUser.Text = CurrentStaff.LastName & ", " & CurrentStaff.FirstName
        txtUserName.Text = LCase(txtFirstName.Text)
        If txtLastName.Text.Length > 0 Then
            txtUserName.Text &= LCase(txtLastName.Text.Substring(0, 1))
        End If
        CurrentStaff.SavedToDB = False : UpdateLabels()
    End Sub

    Private Sub txtLastName_KeyUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLastName.KeyUp
        CurrentStaff.LastName = sender.text
        grpUser.Text = CurrentStaff.LastName & ", " & CurrentStaff.FirstName
        txtUserName.Text = LCase(txtFirstName.Text)
        If txtLastName.Text.Length > 0 Then
            txtUserName.Text &= LCase(txtLastName.Text.Substring(0, 1))
        End If
        CurrentStaff.SavedToDB = False : UpdateLabels()
    End Sub

    Private Sub txtAge_KeyUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAge.KeyUp
        'CurrentStaff.Age = sender.text
    End Sub

    Private Sub optFemale_optMale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFemale.CheckedChanged, optMale.CheckedChanged
    End Sub

    Private Sub txtAddress1_KeyUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAddress1.KeyUp
        CurrentStaff.Adress1 = sender.text
        CurrentStaff.SavedToDB = False : UpdateLabels()
    End Sub

    Private Sub txtHomePhone_KeyUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHomePhone.KeyUp
        CurrentStaff.HomePhone = sender.text
        CurrentStaff.SavedToDB = False : UpdateLabels()
    End Sub

    Private Sub txtAddress2_KeyUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAddress2.KeyUp
        CurrentStaff.Adress2 = sender.text
        CurrentStaff.SavedToDB = False : UpdateLabels()
    End Sub

    Private Sub txtWorkPhone_KeyUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWorkPhone.KeyUp
        CurrentStaff.WorkPhone = sender.text
        CurrentStaff.SavedToDB = False : UpdateLabels()
    End Sub

    Private Sub txtZipCode_KeyUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtZipCode.KeyUp
        CurrentStaff.ZipCode = sender.text
        CurrentStaff.SavedToDB = False : UpdateLabels()
    End Sub

    Private Sub txtCity_KeyUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCity.KeyUp
        CurrentStaff.ZipArea = sender.text
        CurrentStaff.SavedToDB = False : UpdateLabels()
    End Sub

    Private Sub txtMobilePhone_KeyUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMobilePhone.KeyUp
        CurrentStaff.MobilePhone = sender.text
        CurrentStaff.SavedToDB = False : UpdateLabels()
    End Sub

    Private Sub txtBank_KeyUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBank.KeyUp
        CurrentStaff.Bank = sender.text
        CurrentStaff.SavedToDB = False : UpdateLabels()
    End Sub

    Private Sub txtClearing_KeyUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClearing.KeyUp
        CurrentStaff.ClearingNo = sender.text
        CurrentStaff.SavedToDB = False : UpdateLabels()
    End Sub

    Private Sub txtAccount_KeyUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAccount.KeyUp
        CurrentStaff.AccountNo = sender.text
        CurrentStaff.SavedToDB = False : UpdateLabels()
    End Sub

    Private Sub txtInternalInfo_KeyUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInternalInfo.KeyUp
        CurrentStaff.InternalInfo = sender.text
        CurrentStaff.SavedToDB = False : UpdateLabels()
    End Sub

    Private Sub txtExternalInfo_KeyUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtExternalInfo.KeyUp
        CurrentStaff.ExternalInfo = sender.text
        CurrentStaff.SavedToDB = False : UpdateLabels()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If Not Database.SaveStaff(CurrentStaff) Then
            Stop
        End If
        UpdateLabels()
    End Sub

    Sub CreateAllowedShifts()
        tvwAllowedShifts.Nodes.Clear()
        If cmbAllowed.SelectedIndex = 0 Then
            For Each TmpLoc As cStaffScheduleLocation In MyEvent.Schedule.Locations
                For Each TmpRole As cStaffScheduleRole In TmpLoc.Roles
                    Dim TmpItem As New TreeNode
                    TmpItem.ImageKey = "Folder"
                    TmpItem.Text = TmpRole.Name
                    TmpItem.Tag = TmpRole
                    TmpItem.Name = "R" & TmpRole.Role.ID
                    If Not tvwAllowedShifts.Nodes.ContainsKey("R" & TmpRole.Role.ID) Then
                        tvwAllowedShifts.Nodes.Add(TmpItem)
                    End If
                    Dim TmpLocItem As New TreeNode
                    TmpLocItem.ImageKey = "Folder"
                    TmpLocItem.Text = TmpLoc.Name
                    TmpLocItem.Tag = TmpLoc
                    TmpLocItem.Name = "L" & TmpLoc.Location.ID
                    tvwAllowedShifts.Nodes("R" & TmpRole.Role.ID).Nodes.Add(TmpLocItem)
                    TmpItem.ExpandAll()
                    For Each TmpStaff As cStaff In TmpRole.AvailableForStaff
                        Dim TmpStaffItem As New TreeNode
                        TmpStaffItem.Text = TmpStaff.LastName & ", " & TmpStaff.FirstName
                        TmpStaffItem.ImageIndex = TmpStaff.Gender - 1
                        TmpStaffItem.SelectedImageIndex = TmpStaff.Gender - 1
                        TmpStaffItem.Tag = TmpStaff
                        TmpStaffItem.Name = "S" & TmpStaff.DatabaseID
                        TmpLocItem.Nodes.Add(TmpStaffItem)
                    Next
                Next
            Next
        Else
            For Each TmpLoc As cStaffScheduleLocation In MyEvent.Schedule.Locations
                Dim TmpLocItem As New TreeNode
                TmpLocItem.ImageKey = "Folder"
                TmpLocItem.Text = TmpLoc.Name
                TmpLocItem.Tag = TmpLoc
                TmpLocItem.Name = "L" & TmpLoc.Location.ID
                tvwAllowedShifts.Nodes.Add(TmpLocItem)
                For Each TmpRole As cStaffScheduleRole In TmpLoc.Roles
                    Dim TmpItem As New TreeNode
                    TmpItem.ImageKey = "Folder"
                    TmpItem.Text = TmpRole.Name
                    TmpItem.Tag = TmpRole
                    TmpItem.Name = "R" & TmpRole.Role.ID
                    TmpLocItem.Nodes.Add(TmpItem)
                    For Each TmpStaff As cStaff In TmpRole.AvailableForStaff
                        Dim TmpStaffItem As New TreeNode
                        TmpStaffItem.Text = TmpStaff.LastName & ", " & TmpStaff.FirstName
                        TmpStaffItem.ImageIndex = TmpStaff.Gender - 1
                        TmpStaffItem.SelectedImageIndex = TmpStaff.Gender - 1
                        TmpStaffItem.Tag = TmpStaff
                        TmpStaffItem.Name = "S" & TmpStaff.DatabaseID
                        TmpItem.Nodes.Add(TmpStaffItem)
                    Next
                Next
                TmpLocItem.ExpandAll()
            Next
        End If
    End Sub

    Private Sub tabAssign_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabAssign.Enter


        cmbAllowed.SelectedIndex = 0
        CreateAllowedShifts()

        lvwAvailable.Items.Clear()
        StaffList = Database.GetStaffList
        For Each TmpStaff As cStaff In StaffList.Values
            Dim TmpDriverString As String = ""
            Dim TmpItem As New ListViewItem

            If TmpStaff.Driver And cStaff.DriverEnum.driverB Then
                TmpDriverString &= ",B"
            End If
            If TmpStaff.Driver And cStaff.DriverEnum.driverC Then
                TmpDriverString &= ",C"
            End If
            If TmpStaff.Driver And cStaff.DriverEnum.driverD Then
                TmpDriverString &= ",D"
            End If
            If TmpStaff.Driver And cStaff.DriverEnum.driverE Then
                TmpDriverString &= ",E"
            End If
            If TmpDriverString = "" Then
                TmpDriverString = "-"
            Else
                TmpDriverString = TmpDriverString.Substring(1)
            End If
            TmpItem.Text = TmpStaff.LastName & ", " & TmpStaff.Firstname
            TmpItem.ImageIndex = TmpStaff.Gender - 1
            TmpItem.StateImageIndex = TmpStaff.Gender - 1
            TmpItem.SubItems.Add(TmpStaff.Age)
            TmpItem.SubItems.Add(TmpDriverString)
            TmpItem.Tag = TmpStaff
            TmpItem.Name = "S" & TmpStaff.DatabaseID
            lvwAvailable.Items.Add(TmpItem)
        Next
        lvwAvailable.Columns(0).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
        lvwAvailable.Columns(1).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
        lvwAvailable.Columns(2).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)

        cmbFilterRole.Items.Clear()
        For Each TmpRole As cRole In MyEvent.Roles
            cmbFilterRole.Items.Add(TmpRole)
        Next
        If cmbFilterRole.Items.Count > 0 Then cmbFilterRole.SelectedIndex = 0
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Dim TmpNode As TreeNode = tvwAllowedShifts.SelectedNode
        For Each TmpStaffItem As ListViewItem In lvwAvailable.SelectedItems
            If TmpNode.Tag.GetType Is GetType(cStaffScheduleLocation) Then
                If TmpNode.Parent Is Nothing Then
                    For Each TmpRole As cStaffScheduleRole In DirectCast(TmpNode.Tag, cStaffScheduleLocation).Roles
                        If Not TmpRole.AvailableForStaff.Contains("DB" & TmpStaffItem.Tag.DatabaseID) Then
                            TmpRole.AvailableForStaff.Add(TmpStaffItem.Tag, "DB" & TmpStaffItem.Tag.DatabaseID)
                        End If
                    Next
                Else
                    Dim TmpRole As cStaffScheduleRole = TmpNode.Parent.Tag
                    DirectCast(TmpNode.Tag, cStaffScheduleLocation).Roles(TmpRole.Role.ID).AvailableForStaff.Add(TmpStaffItem.Tag, "DB" & TmpStaffItem.Tag.DatabaseID)
                End If
            Else
                For Each TmpLocation As cStaffScheduleLocation In MyEvent.Schedule.Locations
                    For Each TmpRole As cStaffScheduleRole In TmpLocation.Roles
                        If TmpRole.Role Is DirectCast(TmpNode.Tag, cStaffScheduleRole).Role Then
                            If Not TmpRole.AvailableForStaff.Contains("DB" & TmpStaffItem.Tag.DatabaseID) Then
                                TmpRole.AvailableForStaff.Add(TmpStaffItem.Tag, "DB" & TmpStaffItem.Tag.DatabaseID)
                            End If
                        End If
                    Next
                Next
            End If
        Next
        CreateAllowedShifts()
    End Sub

    Private Sub cmdRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
        Dim TmpNode As TreeNode = tvwAllowedShifts.SelectedNode
        If TmpNode.Tag.GetType Is GetType(cStaff) Then
            Dim TmpParentNode As TreeNode = TmpNode.Parent
            If TmpParentNode.Tag.GetType IsNot GetType(cStaffScheduleRole) Then
                DirectCast(TmpParentNode.Tag, cStaffScheduleLocation).Roles(TmpParentNode.Parent.Tag.Role.ID).AvailableForStaff.Remove("DB" & DirectCast(TmpNode.Tag, cStaff).DatabaseID)
            Else
                DirectCast(TmpParentNode.Tag, cStaffScheduleRole).AvailableForStaff.Remove("DB" & DirectCast(TmpNode.Tag, cStaff).DatabaseID)
            End If
        End If
        CreateAllowedShifts()
    End Sub

    Private Sub txtUserName_KeyUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUserName.KeyUp
        CurrentStaff.Username = sender.text
        CurrentStaff.SavedToDB = False : UpdateLabels()
    End Sub

    Private Sub txtPassword_KeyUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassword.KeyUp
        CurrentStaff.Password = sender.text
        CurrentStaff.SavedToDB = False : UpdateLabels()
    End Sub

    Private Sub txtEmail_KeyUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.KeyUp
        CurrentStaff.Email = sender.text
        CurrentStaff.SavedToDB = False : UpdateLabels()
    End Sub

    Private Sub tpCatalog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tpCatalog.Click

    End Sub

    Private Sub cmdAddStaff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddStaff.Click
        Dim TmpStaff As New cStaff
        Dim TmpItem As New ListViewItem
        TmpStaff.DatabaseID = Database.AddStaff
        TmpItem.Tag = TmpStaff
        TmpItem.Text = TmpStaff.LastName & ", " & TmpStaff.FirstName
        TmpItem.ImageIndex = TmpStaff.Gender - 1
        lvwStaff.Items.Add(TmpItem)
        lvwStaff.SelectedItems.Clear()
        TmpItem.Selected = True
    End Sub

    Private Sub dtBirthday_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtBirthday.ValueChanged
        If CurrentStaff.Birthday <> dtBirthday.Value Then
            CurrentStaff.SavedToDB = False : UpdateLabels()
        End If
        CurrentStaff.Birthday = sender.value
        txtAge.Text = CurrentStaff.Age
    End Sub

    Private Sub cmbAllowed_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAllowed.SelectedIndexChanged
        CreateAllowedShifts()
    End Sub

    Private Sub cmdRemoveFromAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveFromAll.Click
        Dim TmpNode As TreeNode = tvwAllowedShifts.SelectedNode
        If TmpNode.Tag.GetType Is GetType(cStaff) Then
            For Each TmpLoc As cStaffScheduleLocation In MyEvent.Schedule.Locations
                For Each TmpRole As cStaffScheduleRole In TmpLoc.Roles
                    If TmpRole.AvailableForStaff.Contains("DB" & DirectCast(TmpNode.Tag, cStaff).DatabaseID) Then
                        TmpRole.AvailableForStaff.Remove("DB" & DirectCast(TmpNode.Tag, cStaff).DatabaseID)
                    End If
                Next
            Next
        End If
        CreateAllowedShifts()
    End Sub

    Private Sub tabAssign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabAssign.Click

    End Sub

    Private Sub chkFilterRole_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFilterRole.CheckedChanged
        FilterList()
    End Sub

    Private Sub cmbFilterRole_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFilterRole.SelectedIndexChanged
        FilterList()
    End Sub

    Sub FilterList()
        Dim TmpRole As cRole = cmbFilterRole.SelectedItem
        Dim Driver() As String = {"-", "B", "C"}
        lvwAvailable.Items.Clear()
        If chkFilterRole.Checked Then
            For Each TmpStaff As cStaff In StaffList.Values
                If Not (TmpStaff.Age < TmpRole.MinAge OrElse TmpStaff.Age > TmpRole.MaxAge OrElse TmpStaff.Driver < TmpRole.Driver OrElse Not (TmpStaff.Gender And TmpRole.Gender) > 0) Then
                    Dim TmpItem As New ListViewItem
                    TmpItem.Text = TmpStaff.LastName & ", " & TmpStaff.Firstname
                    TmpItem.ImageIndex = TmpStaff.Gender - 1
                    TmpItem.StateImageIndex = TmpStaff.Gender - 1
                    TmpItem.SubItems.Add(TmpStaff.Age)
                    TmpItem.SubItems.Add(Driver(TmpStaff.Driver))
                    TmpItem.Tag = TmpStaff
                    TmpItem.Name = "S" & TmpStaff.DatabaseID
                    lvwAvailable.Items.Add(TmpItem)
                End If
            Next
        Else
            For Each TmpStaff As cStaff In StaffList.Values
                Dim TmpItem As New ListViewItem
                TmpItem.Text = TmpStaff.LastName & ", " & TmpStaff.Firstname
                TmpItem.ImageIndex = TmpStaff.Gender - 1
                TmpItem.StateImageIndex = TmpStaff.Gender - 1
                TmpItem.SubItems.Add(TmpStaff.Age)
                TmpItem.SubItems.Add(Driver(TmpStaff.Driver))
                TmpItem.Tag = TmpStaff
                TmpItem.Name = "S" & TmpStaff.DatabaseID
                lvwAvailable.Items.Add(TmpItem)
            Next
        End If
    End Sub

    Private Sub cmdPublishAllowed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPublishAllowed.Click
        If MessageBox.Show("This will save your Event." & vbCrLf & vbCrLf & "Continue?", "BALTHAZAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
        MyEvent.Save()
        Database.SaveScheduleToDB(MyEvent.Schedule)
    End Sub

    Private Sub tpDetails_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpDetails.Enter
        grdSchedule.Rows.Clear()
        For Each TmpLoc As cStaffScheduleLocation In MyEvent.Schedule.Locations
            For Each TmpRole As cStaffScheduleRole In TmpLoc.Roles
                For Each TmpDay As cStaffScheduleDay In TmpRole.Days
                    For Each TmpShift As cStaffScheduleShift In TmpDay.Shifts
                        If TmpShift.Shift.Roles(TmpRole.Role.ID).Quantity > 0 Then
                            grdSchedule.Rows(grdSchedule.Rows.Add).Tag = TmpShift
                        End If
                    Next
                Next
            Next
        Next
    End Sub

    Private stlNotInDB As DataGridViewCellStyle

    Private Sub grdSchedule_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdSchedule.CellFormatting
        Dim TmpShift As cStaffScheduleShift = grdSchedule.Rows(e.RowIndex).Tag
        If stlNotInDB Is Nothing Then
            stlNotInDB = New DataGridViewCellStyle(e.CellStyle)
            stlNotInDB.ForeColor = Color.Gray
        End If
        If TmpShift.DatabaseID = -1 Then
            e.CellStyle = stlNotInDB
        End If
        Select Case grdSchedule.Columns(e.ColumnIndex).HeaderText
            Case "Qty"
                If TmpShift.AssignedStaff.Count <> TmpShift.Shift.Roles(TmpShift.Day.Role.Role.ID).Quantity Then
                    e.CellStyle.ForeColor = Color.Red
                End If
        End Select
    End Sub

    Private Sub grdSchedule_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSchedule.CellValueNeeded
        Dim TmpShift As cStaffScheduleShift = grdSchedule.Rows(e.RowIndex).Tag

        Select Case grdSchedule.Columns(e.ColumnIndex).HeaderText
            Case "Date"
                e.Value = Format(TmpShift.Day.Day.DayDate, "Short date")
            Case "From"
                e.Value = TmpShift.Shift.StartTime
            Case "To"
                e.Value = TmpShift.Shift.EndTime
            Case "Role"
                e.Value = TmpShift.Day.Role.Name
            Case "Location"
                e.Value = TmpShift.Day.Role.Location.Name
            Case "Qty"
                e.Value = TmpShift.Shift.Roles(TmpShift.Day.Role.Role.ID).Quantity
        End Select

    End Sub

    Private Sub grdStaff_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdStaff.CellFormatting
        Dim TmpStaff As cStaff = grdStaff.Rows(e.RowIndex).Tag
        Select Case grdStaff.Columns(e.ColumnIndex).HeaderText
            Case ""
                e.Value = imgIcons.Images(TmpStaff.Gender - 1)
        End Select
    End Sub


    Private Sub grdStaff_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdStaff.CellValueNeeded
        Dim TmpStaff As cStaff = grdStaff.Rows(e.RowIndex).Tag

        Select Case grdStaff.Columns(e.ColumnIndex).HeaderText
            Case "Name"
                e.Value = TmpStaff.FirstName & " " & TmpStaff.LastName
            Case "Age"
                e.Value = TmpStaff.Age
        End Select
    End Sub

    Private Sub grdSchedule_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSchedule.RowEnter
        If e.RowIndex <= 0 Then Exit Sub
        Dim TmpShift As cStaffScheduleShift = grdSchedule.Rows(e.RowIndex).Tag
        If TmpShift Is Nothing Then Exit Sub
        grdStaff.Rows.Clear()
        For Each TmpStaff As cStaff In Database.GetAvailableStaffForShiftList(TmpShift.DatabaseID)
            grdStaff.Rows(grdStaff.Rows.Add).Tag = TmpStaff
        Next
        grdChosen.Rows.Clear()
        For Each TmpStaff As cStaff In TmpShift.AssignedStaff
            grdChosen.Rows(grdChosen.Rows.Add).Tag = TmpStaff
        Next
    End Sub

    Private Sub cmdChoose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChoose.Click

        If grdSchedule.SelectedRows.Count = 0 Then Exit Sub

        Dim TmpShift As cStaffScheduleShift = grdSchedule.SelectedRows(0).Tag

        For Each TmpRow As DataGridViewRow In grdStaff.SelectedRows
            If Not TmpShift.AssignedStaff.Contains("DB" & TmpRow.Tag.DatabaseID) Then
                TmpShift.AssignedStaff.Add(TmpRow.Tag, "DB" & TmpRow.Tag.DatabaseID)
            End If
        Next
        grdChosen.Rows.Clear()
        For Each TmpStaff As cStaff In TmpShift.AssignedStaff
            grdChosen.Rows(grdChosen.Rows.Add).Tag = TmpStaff
        Next
    End Sub

    Private Sub cmdUnChoose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnChoose.Click
        If grdSchedule.SelectedRows.Count = 0 Then Exit Sub

        Dim TmpShift As cStaffScheduleShift = grdSchedule.SelectedRows(0).Tag

        For Each TmpRow As DataGridViewRow In grdChosen.SelectedRows
            TmpShift.AssignedStaff.Remove("DB" & TmpRow.Tag.DatabaseID)
            grdChosen.Rows.Remove(TmpRow)
        Next
    End Sub

    Private Sub grdChosen_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdChosen.CellValueNeeded
        Dim TmpStaff As cStaff = grdChosen.Rows(e.RowIndex).Tag

        Select Case grdChosen.Columns(e.ColumnIndex).HeaderText
            Case "Name"
                e.Value = TmpStaff.FirstName & " " & TmpStaff.LastName
            Case "Age"
                e.Value = TmpStaff.Age
        End Select
    End Sub

    Private Sub grdChosen_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdChosen.CellFormatting
        Dim TmpStaff As cStaff = grdChosen.Rows(e.RowIndex).Tag
        Select Case grdChosen.Columns(e.ColumnIndex).HeaderText
            Case ""
                e.Value = imgIcons.Images(TmpStaff.Gender - 1)
        End Select
    End Sub

    Private Sub cmdWizard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWizard.Click
        frmOptimize.ShowDialog()
        grdSchedule.Rows.Clear()
        For Each TmpLoc As cStaffScheduleLocation In MyEvent.Schedule.Locations
            For Each TmpRole As cStaffScheduleRole In TmpLoc.Roles
                For Each TmpDay As cStaffScheduleDay In TmpRole.Days
                    For Each TmpShift As cStaffScheduleShift In TmpDay.Shifts
                        If TmpShift.Shift.Roles(TmpRole.Role.ID).Quantity > 0 Then
                            grdSchedule.Rows(grdSchedule.Rows.Add).Tag = TmpShift
                        End If
                    Next
                Next
            Next
        Next
        grdSchedule_RowEnter(New Object, New DataGridViewCellEventArgs(0, 0))
    End Sub

    Private Sub picPicture_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles picPicture.DoubleClick
        Dim dlgOpen As New OpenFileDialog
        dlgOpen.Filter = "All supported formats|*.jpg;*.png;*.gif;*.bmp|JPEG|*.jpg|PNG|*.png|CompuServe GIF|*.gif|Bitmap|*.bmp"
        If dlgOpen.ShowDialog = Windows.Forms.DialogResult.OK Then
            picPicture.Load(dlgOpen.FileName)
            CurrentStaff.Picture = New Bitmap(dlgOpen.FileName)
            CurrentStaff.SavedToDB = False : UpdateLabels()
        End If
    End Sub

    Private Sub cmdPublishChosen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPublishChosen.Click
        If MessageBox.Show("This will save your Event." & vbCrLf & vbCrLf & "Continue?", "BALTHAZAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
        MyEvent.Save()
        Database.SaveConfirmedToDB(MyEvent.Schedule)
    End Sub

    Private Sub optFemale_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles optFemale.MouseUp, optMale.MouseUp
        CurrentStaff.Gender = Math.Abs(CInt(optMale.Checked)) + 1
        CurrentStaff.SavedToDB = False : UpdateLabels()
    End Sub

    Private Sub frmStaff_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            Dim AllSaved As Boolean = True
            For Each TmpItem As ListViewItem In lvwStaff.Items
                If Not DirectCast(TmpItem.Tag, cStaff).SavedToDB Then
                    AllSaved = False
                    Exit For
                End If
            Next
            If Not AllSaved Then
                Dim res As DialogResult = Windows.Forms.MessageBox.Show("Do you want to save changes made in the Staff catalog?" & vbCrLf, "BALTHAZAR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If res = Windows.Forms.DialogResult.Yes Then
                    For Each TmpItem As ListViewItem In lvwStaff.Items
                        Database.SaveStaff(DirectCast(TmpItem.Tag, cStaff))
                    Next
                ElseIf res = Windows.Forms.DialogResult.Cancel Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            End If
        End If
    End Sub

    Private Sub frmStaff_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        NoPicture = New Bitmap(picPicture.Width, picPicture.Height)
        Using gfx As Graphics = Graphics.FromImage(NoPicture)
            Using fnt = New Font("Arial", 12, FontStyle.Bold)
                gfx.Clear(Color.White)
                gfx.DrawString("No Picture", fnt, Brushes.DarkGray, NoPicture.Width / 2 - gfx.MeasureString("No Picture", fnt).Width / 2, NoPicture.Height / 2 - gfx.MeasureString("No Picture", fnt).Height / 2)
            End Using
        End Using
    End Sub

    Private Sub txtInfo_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtInternalInfo.KeyUp

    End Sub

    Private Sub lstDriver_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstDriver.Click

    End Sub

    Private Sub lstDriver_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles lstDriver.ItemCheck
        If lstDriver.Tag = "UPDATING" Then Exit Sub
        Select Case e.Index
            Case 0
                CurrentStaff.Driver -= Math.Abs(cStaff.DriverEnum.driverB * e.CurrentValue)
                CurrentStaff.Driver += Math.Abs(cStaff.DriverEnum.driverB * e.NewValue)
            Case 1
                CurrentStaff.Driver -= Math.Abs(cStaff.DriverEnum.driverC * e.CurrentValue)
                CurrentStaff.Driver += Math.Abs(cStaff.DriverEnum.driverC * e.NewValue)
            Case 2
                CurrentStaff.Driver -= Math.Abs(cStaff.DriverEnum.driverD * e.CurrentValue)
                CurrentStaff.Driver += Math.Abs(cStaff.DriverEnum.driverD * e.NewValue)
            Case 3
                CurrentStaff.Driver -= Math.Abs(cStaff.DriverEnum.driverE * e.CurrentValue)
                CurrentStaff.Driver += Math.Abs(cStaff.DriverEnum.driverE * e.NewValue)
        End Select
        CurrentStaff.SavedToDB = False : UpdateLabels()

        lstDriver.Tag = "UPDATING"
        lblDriver.Text = ""
        If CurrentStaff.Driver And cStaff.DriverEnum.driverB Then
            lblDriver.Text &= ",B"
        End If
        If CurrentStaff.Driver And cStaff.DriverEnum.driverC Then
            lblDriver.Text &= ",C"
        End If
        If CurrentStaff.Driver And cStaff.DriverEnum.driverD Then
            lblDriver.Text &= ",D"
        End If
        If CurrentStaff.Driver And cStaff.DriverEnum.driverE Then
            lblDriver.Text &= ",E"
        End If
        If lblDriver.Text = "" Then
            lblDriver.Text = "None"
        ElseIf lblDriver.Text = ",B,C,D,E" Then
            lblDriver.Text = "All"
        Else
            lblDriver.Text = lblDriver.Text.Substring(1)
        End If
        lstDriver.Tag = ""
    End Sub

    Private Sub lstDriver_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstDriver.Leave
        lstDriver.Visible = False
    End Sub

    Private Sub lstDriver_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstDriver.LostFocus
        lstDriver.Visible = False
    End Sub

    Private Sub lstDriver_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstDriver.SelectedIndexChanged
        'Stop
    End Sub

    Private Sub cmdDriver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDriver.Click
        lstDriver.Visible = Not lstDriver.Visible
        If lstDriver.Visible Then lstDriver.Select()
    End Sub

    Private Sub grdCV_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdCV.CellValueNeeded
        Dim TmpCVEntry As cCVEntry = grdCV.Rows(e.RowIndex).Tag
        If TmpCVEntry Is Nothing Then Exit Sub
        Select Case grdCV.Columns(e.ColumnIndex).HeaderText
            Case "Event"
                e.Value = TmpCVEntry.EventName
            Case "Role"
                e.Value = TmpCVEntry.EventRole
            Case "Category"
                e.Value = TmpCVEntry.RoleCategory
            Case "Responsible"
                e.Value = TmpCVEntry.ResponsiblePerson
            Case "Hours"
                e.Value = Format(TmpCVEntry.WorkedMinutes / 60, "N1")
        End Select
    End Sub

    Private Sub cmdRemoveLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteStaff.Click
        Dim TmpStaff As cStaff = lvwStaff.SelectedItems(0).Tag

        Database.DeleteStaff(TmpStaff.DatabaseID)
        lvwStaff.Items.Remove(lvwStaff.SelectedItems(0))

    End Sub
End Class