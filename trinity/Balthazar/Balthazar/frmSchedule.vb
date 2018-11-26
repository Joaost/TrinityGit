Imports Domino

Public Class frmSchedule

    Private Sub frmSchedule_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If tabSchedule.SelectedTab Is tpLocations Then
            grdLocations.Rows.Clear()
            For Each TmpLoc As cLocation In MyEvent.Locations
                grdLocations.Rows(grdLocations.Rows.Add()).Tag = TmpLoc
            Next

            grdRoles.Rows.Clear()
            For Each TmpRole As cRole In MyEvent.Roles
                grdRoles.Rows(grdRoles.Rows.Add()).Tag = TmpRole
            Next
        ElseIf tabSchedule.SelectedTab Is tpDayTemplates Then
            grdDays.Rows.Clear()
            For Each TmpDay As cDayTemplate In MyEvent.DayTemplates
                With grdDays.Rows(grdDays.Rows.Add)
                    .Tag = TmpDay
                End With
            Next
        ElseIf tabSchedule.SelectedTab Is tpSchedule Then
            Me.SuspendLayout()
            imgColors.Images.Clear()
            lvwTemplates.Items.Clear()
            For Each TmpTemplate As cDayTemplate In MyEvent.DayTemplates
                Dim TmpBMP As New Bitmap(16, 16)
                For x As Integer = 0 To 15
                    For y As Integer = 0 To 15
                        TmpBMP.SetPixel(x, y, TmpTemplate.ForeColor)
                    Next
                Next
                imgColors.Images.Add(TmpTemplate.ID, TmpBMP)
                lvwTemplates.Items.Add(TmpTemplate.ID, TmpTemplate.Name, TmpTemplate.ID)
                lvwTemplates.Items(TmpTemplate.ID).Tag = TmpTemplate
            Next
            pnlLocations.Visible = False
            pnlLocations.Controls.Clear()
            pnlLocations.RowCount = MyEvent.Locations.Count
            pnlLocations.RowStyles.Clear()
            pnlLocations.ColumnCount = 2
            Dim r As Integer = 0
            For Each TmpLocation As cLocation In MyEvent.Locations
                Dim TmpLabel As New Label
                Dim TmpStyle As New RowStyle(SizeType.AutoSize)
                pnlLocations.RowStyles.Add(TmpStyle)
                pnlLocations.Controls.Add(TmpLabel, 0, r)
                TmpLabel.Text = TmpLocation.Name
                TmpLabel.TextAlign = ContentAlignment.MiddleLeft
                TmpLabel.Visible = True
                Dim TmpPanel As New Panel
                pnlLocations.Controls.Add(TmpPanel, 1, r)
                TmpPanel.AutoSize = True
                Dim LastRight = 0
                Dim TmpLabels(TmpLocation.Days.Count) As Control
                Dim d As Integer = 0
                For Each TmpDay As cEventDay In TmpLocation.Days
                    Dim TmpDayLabel As New Label
                    TmpDayLabel.Width = 16
                    TmpDayLabel.Height = 16
                    TmpDayLabel.BorderStyle = BorderStyle.FixedSingle
                    If TmpDay.Template Is Nothing Then
                        TmpDayLabel.BackColor = Color.White
                    Else
                        TmpDayLabel.BackColor = TmpDay.Template.ForeColor
                    End If
                    TmpDayLabel.Left = LastRight + 3
                    TmpDayLabel.Tag = TmpDay
                    AddHandler TmpDayLabel.Click, AddressOf ClickDay
                    ttpSchedule.SetToolTip(TmpDayLabel, Format(TmpDay.DayDate, "Short date"))
                    LastRight = TmpDayLabel.Right
                    TmpDayLabel.Visible = True
                    'TmpPanel.Controls.Add(TmpDayLabel)
                    TmpLabels(d) = TmpDayLabel
                    d += 1
                Next
                TmpPanel.Controls.AddRange(TmpLabels)
                TmpPanel.Visible = True
                r += 1
            Next
            pnlLocations.Visible = True

            grdHeadcount.Rows.Clear()
            For Each TmpRole As cRole In MyEvent.Roles
                grdHeadcount.Rows(grdHeadcount.Rows.Add()).Tag = TmpRole
            Next
            Me.ResumeLayout()
        End If
    End Sub

    Sub ClickDay(ByVal sender As Object, ByVal e As EventArgs)
        If lvwTemplates.SelectedItems.Count = 0 Then Exit Sub

        Dim Label As Label = DirectCast(sender, Label)
        Dim TmpDay As cEventDay = DirectCast(Label.Tag, cEventDay)
        Dim TmpTemplate As cDayTemplate = DirectCast(lvwTemplates.SelectedItems(0).Tag, cDayTemplate)

        TmpDay.Template = TmpTemplate
        TmpDay.CreateFromTemplate(TmpTemplate, TmpDay.DayDate)
        Label.BackColor = TmpTemplate.ForeColor
        grdHeadcount.Invalidate()
    End Sub

    Private Sub frmSchedule_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub


    Private Sub cmdAddLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddLocation.Click
        With grdLocations.Rows(grdLocations.Rows.Add)
            .Tag = MyEvent.Locations.Add
            DirectCast(.Tag, cLocation).Days.CreateFromDates(Now, Now, False)
        End With
        'MyEvent.StaffCategories.Invalidate()
    End Sub

    Private Sub cmdRemoveLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveLocation.Click
        If Windows.Forms.MessageBox.Show("Are you sure you want to delete the selected item(s)?", "BALTHAZAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
            For Each TmpRow As DataGridViewRow In grdLocations.SelectedRows
                MyEvent.Locations.Remove(TmpRow.Tag.id)
                grdLocations.Rows.Remove(TmpRow)
            Next
        End If
        'MyEvent.StaffCategories.Invalidate()
    End Sub

    Private Sub cmdAddRole_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddRole.Click
        With grdRoles.Rows(grdRoles.Rows.Add)
            .Tag = MyEvent.Roles.Add("")
        End With
    End Sub

    Private Sub cmdRemoveRole_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveRole.Click
        For Each TmpRow As DataGridViewRow In grdRoles.SelectedRows
            MyEvent.Roles.Remove(TmpRow.Tag.id)
            grdRoles.Rows.Remove(TmpRow)
        Next
    End Sub

    Private Sub grdLocations_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdLocations.CellValueNeeded
        Dim TmpLoc As cLocation = grdLocations.Rows(e.RowIndex).Tag
        Select Case grdLocations.Columns(e.ColumnIndex).Name
            Case "colLocation"
                e.Value = TmpLoc.Name
            Case "colFrom"
                e.Value = TmpLoc.FromDate
            Case "colTo"
                e.Value = TmpLoc.ToDate
        End Select
    End Sub

    Private Sub grdLocations_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdLocations.CellValuePushed
        Dim TmpLoc As cLocation = grdLocations.Rows(e.RowIndex).Tag
        Select Case grdLocations.Columns(e.ColumnIndex).Name
            Case "colLocation"
                TmpLoc.Name = e.Value
            Case "colFrom"
                TmpLoc.Days.CreateFromDates(e.Value, TmpLoc.ToDate, True)
                'MyEvent.StaffCategories.Invalidate()
            Case "colTo"
                TmpLoc.Days.CreateFromDates(TmpLoc.FromDate, e.Value, True)
                'MyEvent.StaffCategories.Invalidate()
        End Select
    End Sub

    Private Sub grdRoles_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdRoles.CellFormatting
        Dim TmpRole As cRole = grdRoles.Rows(e.RowIndex).Tag
        Select Case grdRoles.Columns(e.ColumnIndex).Name
            Case "colQuantity"
                If TmpRole.Category IsNot Nothing AndAlso TmpRole.Category.Quantity <> TmpRole.Category.getQuantityFromRoles Then
                    e.CellStyle.ForeColor = Color.Blue
                Else
                    e.CellStyle.ForeColor = Color.Black
                End If
        End Select

    End Sub

    Private Sub grdRoles_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdRoles.CellValueNeeded
        Dim TmpRole As cRole = grdRoles.Rows(e.RowIndex).Tag
        Select Case grdRoles.Columns(e.ColumnIndex).Name
            Case "colRoleName"
                e.Value = TmpRole.Name
            Case "colDescription"
                e.Value = TmpRole.Description
            Case "colCategory"
                If TmpRole.Category Is Nothing Then
                    e.Value = ""
                Else
                    e.Value = TmpRole.Category.Name
                End If
            Case "colRoleMinAge"
                e.Value = TmpRole.MinAge
            Case "colRoleMaxAge"
                e.Value = TmpRole.MaxAge
            Case "colRoleGender"
                Select Case TmpRole.Gender
                    Case cRole.GenderEnum.Both
                        e.Value = "Both"
                    Case cRole.GenderEnum.Female
                        e.Value = "Female"
                    Case cRole.GenderEnum.Male
                        e.Value = "Male"
                End Select
            Case "colDriver"
                Dim TmpArray() As String = {"-", "B", "C"}
                e.Value = TmpArray(TmpRole.Driver)
            Case "colRolePerDiem"
                e.Value = TmpRole.PerDiem
        End Select
    End Sub

    Private Sub grdRoles_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdRoles.CellValuePushed
        Dim TmpRole As cRole = grdRoles.Rows(e.RowIndex).Tag
        Select Case grdRoles.Columns(e.ColumnIndex).Name
            Case "colRoleName"
                TmpRole.Name = e.Value
            Case "colDescription"
                TmpRole.Description = e.Value
            Case "colCategory"
                For Each TmpCat As cStaffCategory In MyEvent.StaffCategories
                    If TmpCat.Name = e.Value Then
                        TmpRole.Category = TmpCat
                        Exit For
                    End If
                Next
                'MyEvent.StaffCategories.Invalidate()
            Case "colRoleMinAge"
                TmpRole.MinAge = e.Value
            Case "colRoleMaxAge"
                TmpRole.MaxAge = e.Value
            Case "colRoleGender"
                Select Case e.Value
                    Case "Both"
                        TmpRole.Gender = cRole.GenderEnum.Both
                    Case "Female"
                        TmpRole.Gender = cRole.GenderEnum.Female
                    Case "Male"
                        TmpRole.Gender = cRole.GenderEnum.Male
                End Select
            Case "colDriver"
                Select Case e.Value
                    Case "-"
                        TmpRole.Driver = cRole.DriverEnum.driverNone
                    Case "B"
                        TmpRole.Driver = cRole.DriverEnum.driverB
                    Case "C"
                        TmpRole.Driver = cRole.DriverEnum.driverC
                End Select
            Case "colRolePerDiem"
                TmpRole.PerDiem = e.Value
        End Select
        'For Each TmpLoc As cLocation In MyEvent.Locations
        '    For Each TmpDay As cEventDay In TmpLoc.Days
        '        If TmpDay.Template IsNot Nothing Then
        '            For Each TmpShift As cShift In TmpDay.Template.Shifts
        '                For Each Role As cRole In TmpShift.Roles
        '                    Dim XMLDoc As New Xml.XmlDocument
        '                    Dim Quantity As Integer = Role.Quantity
        '                    Role.CreateFromXML(TmpRole.CreateXML(XMLDoc))
        '                    Role.Quantity = Quantity
        '                Next
        '            Next
        '        End If
        '    Next
        'Next

        For Each TmpDay As cDayTemplate In MyEvent.DayTemplates
            For Each TmpShift As cShift In TmpDay.Shifts.Values
                If TmpShift.Roles.Contains(TmpRole.ID) Then
                    Dim XMLDoc As New Xml.XmlDocument
                    Dim Quantity As Integer = TmpShift.Roles(TmpRole.ID).Quantity
                    TmpShift.Roles(TmpRole.ID).CreateFromXML(TmpRole.CreateXML(XMLDoc))
                    TmpShift.Roles(TmpRole.ID).Quantity = Quantity
                End If
            Next
        Next
    End Sub

    Private Sub grdLocations_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdLocations.CellContentClick

    End Sub

    Private Sub grdRoles_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRoles.CellContentClick

    End Sub

    Private Sub grdRoles_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRoles.RowEnter
        With grdRoles.Rows(e.RowIndex)
            With CType(.Cells("colCategory"), Balthazar.ExtendedComboBoxCell)
                .ComboBox.DropDownStyle = ComboBoxStyle.DropDownList
                .Items.clear()
                For Each TmpCat As cStaffCategory In MyEvent.StaffCategories
                    .Items.add(TmpCat.Name)
                Next
            End With
        End With
    End Sub

    Private Sub grdShifts_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdShifts.CellValueNeeded
        Dim TmpShift As cShift = grdShifts.Rows(e.RowIndex).Tag

        Select Case grdShifts.Columns(e.ColumnIndex).HeaderText
            Case "Name"
                e.Value = TmpShift.Name
            Case "Description"
                e.Value = TmpShift.Description
            Case "Starts"
                e.Value = TmpShift.StartTime
            Case "Ends"
                e.Value = TmpShift.EndTime
            Case "Heads"
                Dim TmpCount As Integer = 0
                For Each TmpRole As cRole In TmpShift.Roles
                    TmpCount += TmpRole.Quantity
                Next
                e.Value = TmpCount
        End Select
    End Sub

    Private Sub grdShifts_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdShifts.CellValuePushed
        Dim TmpShift As cShift = grdShifts.Rows(e.RowIndex).Tag

        Select Case grdShifts.Columns(e.ColumnIndex).HeaderText
            Case "Name"
                TmpShift.Name = e.Value
            Case "Description"
                TmpShift.Description = e.Value
            Case "Starts"
                TmpShift.StartTime = e.Value
            Case "Ends"
                TmpShift.EndTime = e.Value
        End Select
    End Sub

    Private Sub grdDays_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdDays.CellValueNeeded
        Dim TmpDay As cDayTemplate = grdDays.Rows(e.RowIndex).Tag

        Select Case grdDays.Columns(e.ColumnIndex).Name
            Case "colDayName"
                e.Value = TmpDay.Name
            Case "colDayDescription"
                e.Value = TmpDay.Description
        End Select
    End Sub

    Private Sub grdDays_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdDays.CellValuePushed
        Dim TmpDay As cDayTemplate = grdDays.Rows(e.RowIndex).Tag

        Select Case grdDays.Columns(e.ColumnIndex).Name
            Case "colDayName"
                TmpDay.Name = e.Value
            Case "colDayDescription"
                TmpDay.Description = e.Value
        End Select
    End Sub

    Private Sub grdDays_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdDays.RowEnter
        If grdDays.Rows.Count = 0 OrElse grdDays.Rows(e.RowIndex).Tag Is Nothing Then
            grpShifts.Visible = False
            Exit Sub
        End If
        grpShifts.Visible = True
        grdShifts.Rows.Clear()
        For Each TmpShift As cShift In DirectCast(grdDays.Rows(e.RowIndex).Tag, cDayTemplate).Shifts.Values
            grdShifts.Rows(grdShifts.Rows.Add).Tag = TmpShift
        Next
        grdDays.Tag = grdDays.Rows(e.RowIndex).Tag
        grdShifts_RowEnter(New Object, New DataGridViewCellEventArgs(0, 0))
    End Sub

    Private Sub cmdAddDayTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddDayTemplate.Click
        Dim TmpDay As cDayTemplate = MyEvent.DayTemplates.Add
        With grdDays.Rows(grdDays.Rows.Add)
            .Tag = TmpDay
        End With
    End Sub

    Private Sub cmdAddShift_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddShift.Click
        Dim TmpShift As New cShift(MyEvent)
        DirectCast(grdDays.Tag, cDayTemplate).Shifts.Add(TmpShift.ID, TmpShift)
        With grdShifts.Rows(grdShifts.Rows.Add)
            .Tag = TmpShift
        End With
    End Sub

    Private Sub tpLocations_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpLocations.Enter
        frmSchedule_Activated(sender, e)
    End Sub

    Private Sub tpDayTemplates_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpDayTemplates.Enter
        frmSchedule_Activated(sender, e)
        If grdDays.SelectedRows.Count = 0 Then
            grpShifts.Visible = False
        Else
            grdDays_RowEnter(New Object, New DataGridViewCellEventArgs(0, 0))
        End If
    End Sub

    Private Sub tpSchedule_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpSchedule.Enter
        frmSchedule_Activated(sender, e)
    End Sub

    Private Sub grdShifts_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdShifts.RowEnter
        If grdShifts.Rows.Count = 0 Then
            grpShiftRoles.Visible = False
            Exit Sub
        End If
        Dim TmpShift As cShift = grdShifts.Rows(e.RowIndex).Tag
        If TmpShift Is Nothing Then Exit Sub

        grdShiftRoles.Rows.Clear()
        For Each TmpRole As cRole In TmpShift.Roles
            grdShiftRoles.Rows(grdShiftRoles.Rows.Add).Tag = TmpRole
        Next
        grpShiftRoles.Visible = True

    End Sub

    Private Sub grdShiftRoles_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdShiftRoles.CellValueNeeded
        Dim TmpRole As cRole = grdShiftRoles.Rows(e.RowIndex).Tag

        Select Case grdShiftRoles.Columns(e.ColumnIndex).HeaderText
            Case "Name"
                e.Value = TmpRole.Name
            Case "Quantity"
                e.Value = TmpRole.Quantity
        End Select
    End Sub

    Private Sub grdShiftRoles_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdShiftRoles.CellValuePushed
        Dim TmpRole As cRole = grdShiftRoles.Rows(e.RowIndex).Tag '

        TmpRole.Quantity = e.Value

        grdShifts.Invalidate()

    End Sub

    Private Sub grdHeadcount_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdHeadcount.CellValueNeeded
        Const OB_TILL횳G As Single = 15

        Dim TmpRole As cRole = grdHeadcount.Rows(e.RowIndex).Tag

        Select Case grdHeadcount.Columns(e.ColumnIndex).HeaderText
            Case "Name"
                e.Value = TmpRole.Name
            Case "Hours"
                Dim TmpCount As Single = 0
                For Each TmpShift As cShift In MyEvent.ShiftList
                    For Each Role As cRole In TmpShift.Roles
                        If TmpRole.ID = Role.ID Then
                            TmpCount += Role.Quantity * (TmpShift.Length / 60)
                        End If
                    Next
                Next
                e.Value = TmpCount
            Case "Shifts"
                Dim TmpCount As Single = 0
                For Each TmpShift As cShift In MyEvent.ShiftList
                    For Each Role As cRole In TmpShift.Roles
                        If TmpRole.ID = Role.ID Then
                            TmpCount += Role.Quantity
                        End If
                    Next
                Next
                e.Value = TmpCount
            Case "CTC"
                Dim TmpCost As Decimal = 0
                For Each TmpLoc As cLocation In MyEvent.Locations
                    For Each TmpDay As cEventDay In TmpLoc.Days
                        If TmpDay.Template IsNot Nothing Then
                            For Each TmpShift As cShift In TmpDay.Template.Shifts.Values
                                For Each Role As cRole In TmpShift.Roles
                                    If TmpRole.ID = Role.ID Then
                                        TmpCost += Role.PerDiem * Role.Quantity
                                        If Weekday(TmpDay.DayDate, FirstDayOfWeek.Monday) > 5 Then
                                            TmpCost += ((Role.CTCPerHour + (OB_TILL횳G * Role.Quantity))) * (TmpShift.Length / 60)
                                        Else
                                            For m As Integer = Helper.Time2Mam(TmpShift.StartTime) To Helper.Time2Mam(TmpShift.EndTime) - 1
                                                If m < 7 * 60 OrElse m > 20 * 60 Then
                                                    TmpCost += ((Role.CTCPerHour + (OB_TILL횳G * Role.Quantity)) / 60)
                                                Else
                                                    TmpCost += Role.CTCPerHour / 60
                                                End If
                                            Next
                                        End If
                                    End If
                                Next
                            Next
                        End If
                    Next
                Next
                e.Value = TmpCost
        End Select
    End Sub

    Private Sub cmdCopyToAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopyToAll.Click
        If grdShifts.SelectedRows.Count = 0 Then Exit Sub
        Dim MyShift As cShift = grdShifts.SelectedRows(0).Tag
        For Each TmpShift As cShift In DirectCast(grdDays.Tag, cDayTemplate).Shifts.Values
            For Each TmpRole As cRole In MyShift.Roles
                TmpShift.Roles(TmpRole.ID).Quantity = TmpRole.Quantity
            Next
        Next
        grdShifts.Invalidate()
    End Sub

    Private Sub cmdRemoveShift_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveShift.Click
        For Each TmpRow As DataGridViewRow In grdShifts.SelectedRows
            DirectCast(grdDays.Tag, cDayTemplate).Shifts.Remove(TmpRow.Tag)
            grdShifts.Rows.Remove(TmpRow)
        Next
        grdShifts_RowEnter(New Object, New DataGridViewCellEventArgs(0, 0))
    End Sub

    Private Sub cmdRemoveDayTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveDayTemplate.Click
        For Each TmpRow As DataGridViewRow In grdDays.SelectedRows
            MyEvent.DayTemplates.Remove(DirectCast(TmpRow.Tag, cDayTemplate).ID)
            grdDays.Rows.Remove(TmpRow)
        Next
        grdDays_RowEnter(New Object, New DataGridViewCellEventArgs(0, 0))
    End Sub

    Private Sub lblPercent_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblPercent.Resize
        lblPercent.Left = grpTotal.Width - lblPercent.Width - 15
    End Sub

    Private Sub tpBudget_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpBudget.Enter
        Me.SuspendLayout()
        grdStaff.Rows.Clear()
        grdPlanning.Rows.Clear()
        grdMaterial.Rows.Clear()
        grdLogistics.Rows.Clear()
        For Each TmpCost As cCost In MyEvent.Budget.PlanningCosts
            Dim NewRow As Integer = grdPlanning.Rows.Add
            grdPlanning.Rows(NewRow).Tag = TmpCost
        Next
        For Each TmpCost As cStaffCategory In MyEvent.Budget.StaffCosts
            Dim NewRow As Integer = grdStaff.Rows.Add
            grdStaff.Rows(NewRow).Tag = TmpCost
        Next
        For Each TmpCost As cCost In MyEvent.Budget.MaterialCosts
            Dim NewRow As Integer = grdMaterial.Rows.Add
            grdMaterial.Rows(NewRow).Tag = TmpCost
        Next
        For Each TmpCost As cCost In MyEvent.Budget.LogisticsCosts
            Dim NewRow As Integer = grdLogistics.Rows.Add
            grdLogistics.Rows(NewRow).Tag = TmpCost
        Next
        Me.ResumeLayout()
    End Sub

    Private Sub grdStaff_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdStaff.CellFormatting
        Dim TmpCat As cStaffCategory = grdStaff.Rows(e.RowIndex).Tag
        Select Case grdStaff.Columns(e.ColumnIndex).Name
            Case "colStaffPrice"
                e.Value = Format(e.Value, "C0")
            Case "colStaffCTC"
                e.Value = Format(e.Value, "C0")
            Case "colStaffACHour"
                e.Value = Format(e.Value, "C0")
            Case "colStaffActualPrice"
                e.Value = Format(e.Value, "C0")
            Case "colStaffProfit"
                e.Value = Format(e.Value, "C0")
            Case "colStaffCount"
                If TmpCat.Quantity <> TmpCat.getQuantityFromRoles Then
                    e.CellStyle.ForeColor = Color.Blue
                Else
                    e.CellStyle.ForeColor = Color.Black
                End If
            Case "colStaffDays"
                If TmpCat.Days <> TmpCat.getDaysFromLocations Then
                    e.CellStyle.ForeColor = Color.Blue
                Else
                    e.CellStyle.ForeColor = Color.Black
                End If
        End Select
    End Sub

    Private Sub grdStaff_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdStaff.CellValueNeeded
        Const OB_TILL횳G = 15
        Dim TmpCat As cStaffCategory = grdStaff.Rows(e.RowIndex).Tag
        If TmpCat Is Nothing Then Exit Sub
        Select Case grdStaff.Columns(e.ColumnIndex).Name
            Case "colStaffName"
                e.Value = TmpCat.Name
            Case "colStaffDescription"
                e.Value = TmpCat.Description
            Case "colStaffCount"
            Case "colStaffDays"
                e.Value = TmpCat.Days
            Case "colStaffHours"
                Dim TmpCount As Integer = 0
                For Each TmpLoc As cLocation In MyEvent.Locations
                    For Each TmpDay As cEventDay In TmpLoc.Days
                        If TmpDay.Template IsNot Nothing Then
                            For Each TmpShift As cShift In TmpDay.Template.Shifts.Values
                                For Each TmpRole As cRole In TmpShift.Roles
                                    If TmpRole.Category IsNot Nothing AndAlso TmpRole.Category.ID = TmpCat.ID Then
                                        TmpCount += TmpRole.Quantity * (TmpShift.Length / 60)
                                    End If
                                Next
                            Next
                        End If
                    Next
                Next
                e.Value = TmpCount
            Case "colStaffPrice"
                e.Value = TmpCat.CostPerHourCTC
            Case "colStaffCTC"
                Dim TmpCost As Decimal = 0
                For Each TmpLoc As cLocation In MyEvent.Locations
                    For Each TmpDay As cEventDay In TmpLoc.Days
                        If TmpDay.Template IsNot Nothing Then
                            For Each TmpShift As cShift In TmpDay.Template.Shifts.Values
                                For Each TmpRole As cRole In TmpShift.Roles
                                    If TmpRole.Category IsNot Nothing AndAlso TmpRole.Category.ID = TmpCat.ID Then
                                        TmpCost += TmpRole.PerDiem * TmpRole.Quantity
                                        If Weekday(TmpDay.DayDate, FirstDayOfWeek.Monday) > 5 Then
                                            TmpCost += ((TmpRole.CTCPerHour + (OB_TILL횳G * TmpRole.Quantity))) * (TmpShift.Length / 60)
                                        Else
                                            For m As Integer = Helper.Time2Mam(TmpShift.StartTime) To Helper.Time2Mam(TmpShift.EndTime) - 1
                                                If m < 7 * 60 OrElse m > 20 * 60 Then
                                                    TmpCost += ((TmpRole.CTCPerHour + (OB_TILL횳G * TmpRole.Quantity)) / 60)
                                                Else
                                                    TmpCost += TmpRole.CTCPerHour / 60
                                                End If
                                            Next
                                        End If
                                    End If
                                Next
                            Next
                        End If
                    Next
                Next
                e.Value = TmpCost
            Case "colStaffACHour"
                e.Value = TmpCat.CostPerHourActual
            Case "colStaffActualPrice"
                Dim TmpCost As Decimal = 0
                For Each TmpLoc As cLocation In MyEvent.Locations
                    For Each TmpDay As cEventDay In TmpLoc.Days
                        If TmpDay.Template IsNot Nothing Then
                            For Each TmpShift As cShift In TmpDay.Template.Shifts.Values
                                For Each TmpRole As cRole In TmpShift.Roles
                                    If TmpRole.Category IsNot Nothing AndAlso TmpRole.Category.ID = TmpCat.ID Then
                                        If TmpRole.Quantity > 0 Then
                                            TmpCost += TmpRole.PerDiem
                                        End If
                                        If Weekday(TmpDay.DayDate, FirstDayOfWeek.Monday) > 5 Then
                                            TmpCost += ((TmpRole.ActualCost + (OB_TILL횳G * TmpRole.Quantity)))
                                        Else
                                            For m As Integer = Helper.Time2Mam(TmpShift.StartTime) To Helper.Time2Mam(TmpShift.EndTime) - 1
                                                If m < 7 * 60 OrElse m > 20 * 60 Then
                                                    TmpCost += ((TmpRole.ActualCost + (OB_TILL횳G * TmpRole.Quantity)) / 60)
                                                Else
                                                    TmpCost += TmpRole.ActualCost / 60
                                                End If
                                            Next
                                        End If
                                    End If
                                Next
                            Next
                        End If
                    Next
                Next
                e.Value = TmpCost
            Case "colStaffProfit"
                e.Value = TmpCat.CTC - TmpCat.ActualCost
        End Select
    End Sub

    Private Sub grdStaff_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdStaff.CellValuePushed
        Dim TmpCat As cStaffCategory = grdStaff.Rows(e.RowIndex).Tag
        Select Case grdStaff.Columns(e.ColumnIndex).Name
            Case "colStaffHours"
                TmpCat.HoursPerDay = e.Value
            Case "colStaffDays"
                TmpCat.Days = e.Value
            Case "colStaffPrice"
                TmpCat.CostPerHourCTC = e.Value
            Case "colStaffACHour"
                TmpCat.CostPerHourActual = e.Value
            Case "colStaffCount"
                TmpCat.Quantity = e.Value
            Case "colStaffName"
                TmpCat.Name = e.Value
            Case "colStaffDescription"
                TmpCat.Description = e.Value
        End Select
        grdStaff.InvalidateRow(e.RowIndex)
    End Sub

    Sub InvalidatedGrid(ByVal sender As Object, ByVal e As InvalidateEventArgs) Handles grdStaff.Invalidated, grdPlanning.Invalidated, grdLogistics.Invalidated, grdMaterial.Invalidated
        'Stop
        lblCTC.Text = Format(MyEvent.CTC, "C0")
        lblActualCost.Text = Format(MyEvent.ActualCost, "C0")
        lblProfit.Text = Format(MyEvent.CTC - MyEvent.ActualCost, "C0")
        If MyEvent.ActualCost > 0 Then
            lblPercent.Text = Format((MyEvent.CTC - MyEvent.ActualCost) / MyEvent.ActualCost, "P0")
            If (MyEvent.CTC - MyEvent.ActualCost) / MyEvent.ActualCost < 0.3 Then
                lblPercent.ForeColor = Color.Red
            Else
                lblPercent.ForeColor = Color.Green
            End If
        Else
            lblPercent.Text = Format(1, "P0")
            lblPercent.ForeColor = Color.Green
        End If
    End Sub

    Private Sub lblCTC_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblCTC.SizeChanged
        lblCTC.Left = grpTotal.Width - lblCTC.Width - 15
    End Sub

    Private Sub lblActualCost_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblActualCost.SizeChanged
        lblActualCost.Left = grpTotal.Width - lblActualCost.Width - 15
    End Sub

    Private Sub lblProfit_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblProfit.SizeChanged
        lblProfit.Left = grpTotal.Width - lblProfit.Width - 15
    End Sub


    Private Sub grdMaterial_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdMaterial.CellFormatting
        Select Case grdMaterial.Columns(e.ColumnIndex).Name
            Case "colMaterialCTC"
                e.Value = Format(e.Value, "C0")
            Case "colMaterialActualPrice"
                e.Value = Format(e.Value, "C0")
            Case "colMaterialProfit"
                e.Value = Format(e.Value, "C0")
        End Select
    End Sub

    Private Sub grdMaterial_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdMaterial.CellValueNeeded
        Dim TmpCost As cCost = grdMaterial.Rows(e.RowIndex).Tag
        If TmpCost Is Nothing Then Exit Sub
        Select Case grdMaterial.Columns(e.ColumnIndex).Name
            Case "colMaterialName"
                e.Value = TmpCost.Name
            Case "colMaterialDescription"
                e.Value = TmpCost.Description
            Case "colMaterialCTC"
                e.Value = TmpCost.CTC
            Case "colMaterialActualPrice"
                e.Value = TmpCost.ActualCost
            Case "colMaterialProfit"
                e.Value = TmpCost.Profit
        End Select
    End Sub

    Private Sub grdMaterial_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdMaterial.CellValuePushed
        Dim TmpCost As cCost = grdMaterial.Rows(e.RowIndex).Tag
        Select Case grdMaterial.Columns(e.ColumnIndex).Name
            Case "colMaterialName"
                TmpCost.Name = e.Value
            Case "colMaterialDescription"
                TmpCost.Description = e.Value
            Case "colMaterialCTC"
                TmpCost.CTC = e.Value
            Case "colMaterialActualPrice"
                TmpCost.ActualCost = e.Value
        End Select
        grdMaterial.InvalidateRow(e.RowIndex)
    End Sub

    Private Sub cmdAddMaterial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddMaterial.Click
        Dim TmpCost As New cCost
        MyEvent.Budget.MaterialCosts.Add(TmpCost)
        Dim NewRow As Integer = grdMaterial.Rows.Add
        grdMaterial.Rows(NewRow).Tag = TmpCost
    End Sub

    Private Sub cmdRemoveMaterial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveMaterial.Click
        For Each TmpRow As DataGridViewRow In grdMaterial.SelectedRows
            MyEvent.Budget.MaterialCosts.Remove(DirectCast(TmpRow.Tag, cCost))
            grdMaterial.Rows.Remove(TmpRow)
        Next
    End Sub


    Private Sub grdLogistics_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdLogistics.CellFormatting
        Select Case grdLogistics.Columns(e.ColumnIndex).Name
            Case "colLogCTC"
                e.Value = Format(e.Value, "C0")
            Case "colLogActualPrice"
                e.Value = Format(e.Value, "C0")
            Case "colLogProfit"
                e.Value = Format(e.Value, "C0")
        End Select
    End Sub

    Private Sub grdLogistics_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdLogistics.CellValueNeeded
        Dim TmpCost As cCost = grdLogistics.Rows(e.RowIndex).Tag
        If TmpCost Is Nothing Then Exit Sub
        Select Case grdLogistics.Columns(e.ColumnIndex).Name
            Case "colLogName"
                e.Value = TmpCost.Name
            Case "colLogDescription"
                e.Value = TmpCost.Description
            Case "colLogCTC"
                e.Value = TmpCost.CTC
            Case "colLogActualPrice"
                e.Value = TmpCost.ActualCost
            Case "colLogProfit"
                e.Value = TmpCost.Profit
        End Select
    End Sub

    Private Sub grdLogistics_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdLogistics.CellValuePushed
        Dim TmpCost As cCost = grdLogistics.Rows(e.RowIndex).Tag
        Select Case grdLogistics.Columns(e.ColumnIndex).Name
            Case "colLogName"
                TmpCost.Name = e.Value
            Case "colLogDescription"
                TmpCost.Description = e.Value
            Case "colLogCTC"
                TmpCost.CTC = e.Value
            Case "colLogActualPrice"
                TmpCost.ActualCost = e.Value
        End Select
        grdLogistics.InvalidateRow(e.RowIndex)
    End Sub

    Private Sub cmdAddLogistics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddLog.Click
        Dim TmpCost As New cCost
        MyEvent.Budget.LogisticsCosts.Add(TmpCost)
        Dim NewRow As Integer = grdLogistics.Rows.Add
        grdLogistics.Rows(NewRow).Tag = TmpCost
    End Sub

    Private Sub cmdRemoveLogistics_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveLog.Click
        For Each TmpRow As DataGridViewRow In grdLogistics.SelectedRows
            MyEvent.Budget.LogisticsCosts.Remove(DirectCast(TmpRow.Tag, cCost))
            grdLogistics.Rows.Remove(TmpRow)
        Next
    End Sub

    Private Sub grdPlanning_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdPlanning.CellFormatting
        Select Case grdPlanning.Columns(e.ColumnIndex).Name
            Case "colPlanningPrice"
                e.Value = Format(e.Value, "C0")
            Case "colPlanningCTC"
                e.Value = Format(e.Value, "C0")
            Case "colPlanningActualCost"
                e.Value = Format(e.Value, "C0")
            Case "colPlanningProfit"
                e.Value = Format(e.Value, "C0")
        End Select
    End Sub

    Private Sub grdPlanning_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdPlanning.CellValueNeeded
        Dim TmpCost As cHourCost = grdPlanning.Rows(e.RowIndex).Tag
        If TmpCost Is Nothing Then Exit Sub
        Select Case grdPlanning.Columns(e.ColumnIndex).Name
            Case "colPlanningName"
                e.Value = TmpCost.Name
            Case "colPlanningDescription"
                e.Value = TmpCost.Description
            Case "colPlanningCTC"
                e.Value = TmpCost.CTC
            Case "colPlanningHours"
                e.Value = TmpCost.Hours
            Case "colPlanningPrice"
                e.Value = TmpCost.CostPerHourCTC
            Case "colPlanningActualCost"
                e.Value = TmpCost.ActualCost
            Case "colPlanningProfit"
                e.Value = TmpCost.Profit
        End Select
    End Sub

    Private Sub grdPlanning_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdPlanning.CellValuePushed
        Dim TmpCost As cHourCost = grdPlanning.Rows(e.RowIndex).Tag
        Select Case grdPlanning.Columns(e.ColumnIndex).Name
            Case "colPlanningName"
                TmpCost.Name = e.Value
            Case "colPlanningDescription"
                TmpCost.Description = e.Value
            Case "colPlanningHours"
                TmpCost.Hours = e.Value
            Case "colPlanningActualCost"
                TmpCost.ActualCost = e.Value
            Case "colPlanningPrice"
                TmpCost.CostPerHourCTC = e.Value
        End Select
        grdPlanning.InvalidateRow(e.RowIndex)
    End Sub

    Private Sub cmdAddPlanning_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddPlanning.Click
        Dim TmpCost As New cHourCost
        MyEvent.Budget.PlanningCosts.Add(TmpCost)
        Dim NewRow As Integer = grdPlanning.Rows.Add
        grdPlanning.Rows(NewRow).Tag = TmpCost
    End Sub

    Private Sub cmdRemovePlanning_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemovePlanning.Click
        For Each TmpRow As DataGridViewRow In grdPlanning.SelectedRows
            MyEvent.Budget.PlanningCosts.Remove(DirectCast(TmpRow.Tag, cHourCost))
            grdPlanning.Rows.Remove(TmpRow)
        Next
    End Sub

    Private Sub tpBudget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tpBudget.Click

    End Sub

    Private Sub grdShifts_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdShifts.CellContentClick

    End Sub

    Private Sub grpDates_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpDates.Enter
        grdImportantDates.Rows.Clear()
        For Each TmpDate As cImportantDate In MyEvent.ImportantDates
            With grdImportantDates.Rows(grdImportantDates.Rows.Add)
                .Tag = TmpDate
            End With
        Next
    End Sub

    Private Sub grdImportantDates_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdImportantDates.CellBeginEdit
        Dim TmpDate As cImportantDate = grdImportantDates.Rows(e.RowIndex).Tag

    End Sub

    Private Sub grdImportantDates_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdImportantDates.CellFormatting
        Dim TmpDate As cImportantDate = grdImportantDates.Rows(e.RowIndex).Tag
        Select Case grdImportantDates.Columns(e.ColumnIndex).HeaderText
            Case "Remind me"
                e.Value = e.Value & " day(s) before"
        End Select

    End Sub

    Private Sub grdImportantDates_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdImportantDates.CellValueNeeded
        Dim TmpDate As cImportantDate = grdImportantDates.Rows(e.RowIndex).Tag
        Select Case grdImportantDates.Columns(e.ColumnIndex).HeaderText
            Case "Date"
                e.Value = TmpDate.Date
            Case "Name"
                e.Value = TmpDate.Name
            Case "Description"
                e.Value = TmpDate.Description
            Case "Remind me"
                e.Value = TmpDate.RemindMe
        End Select
    End Sub

    Private Sub grdImportantDates_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdImportantDates.CellValuePushed
        Dim TmpDate As cImportantDate = grdImportantDates.Rows(e.RowIndex).Tag
        Select Case grdImportantDates.Columns(e.ColumnIndex).HeaderText
            Case "Date"
                TmpDate.Date = e.Value
            Case "Name"
                TmpDate.Name = e.Value
            Case "Description"
                TmpDate.Description = e.Value
            Case "Remind me"
                TmpDate.RemindMe = e.Value
        End Select
    End Sub

    Private Sub cmdAddImportantDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddImportantDate.Click
        With grdImportantDates.Rows(grdImportantDates.Rows.Add)
            Dim TmpDate As New cImportantDate
            TmpDate.Date = Now
            .Tag = TmpDate
            MyEvent.ImportantDates.Add(TmpDate)
        End With
        grdImportantDates.Invalidate()
    End Sub

    Private Sub cmdRemoveImportantDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveImportantDate.Click

    End Sub

    Private Sub cmdExportNotes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExportNotes.Click
        For Each TmpRow As DataGridViewRow In grdImportantDates.Rows
            Dim TmpDate As cImportantDate = TmpRow.Tag
            CreateNotesCalendarEntry(TmpDate)
        Next
    End Sub

    Sub CreateNotesCalendarEntry(ByVal item As cImportantDate)
        createReminder(item.Date, item.Description, item.Name, item.RemindMe * (60 * 24))
    End Sub

    'Sub CreateNotesCalendarEntry(ByVal item As cImportantDate)
    '    Dim NS As New Domino.NotesSession
    '    Dim NotesDB As Domino.NotesDatabase
    '    Dim Doc As Domino.NotesDocument
    '    If Not (NS Is Nothing) Then
    '        NS.Initialize("Nadia2008")
    '        NotesDB = NS.GetDatabase(BalthazarSettings.NotesServer, "mail\" & BalthazarSettings.NotesUser & ".nsf", False)
    '        If Not (NotesDB Is Nothing) Then
    '            Doc = NotesDB.CreateDocument
    '            Doc.ReplaceItemValue("Subject", item.Name)
    '            Doc.ReplaceItemValue("Body", item.Description)
    '            Doc.ReplaceItemValue("Notes", CreateNotesValue(""))
    '            Doc.ReplaceItemValue("StartDateTime", CreateNotesValue(item.Date))
    '            Doc.ReplaceItemValue("EndDateTime", CreateNotesValue(item.Date))
    '            Doc.ReplaceItemValue("CalendarDateTime", CreateNotesValue(item.Date))
    '            Doc.ReplaceItemValue("$NoPurge", CreateNotesValue(item.Date))
    '            Doc.ReplaceItemValue("Form", CreateNotesValue("Appointment"))
    '            Doc.ReplaceItemValue("AppointmentType", CreateNotesValue("4"))
    '            Doc.ReplaceItemValue("MeetingType", CreateNotesValue("1"))
    '            Doc.ReplaceItemValue("Chair", CreateNotesValue(NS.UserName))
    '            Doc.ReplaceItemValue("From", CreateNotesValue(NS.UserName))
    '            Doc.ReplaceItemValue("Principal", CreateNotesValue(NS.UserName))
    '            Doc.ReplaceItemValue("_ViewIcon", CreateNotesValue("10"))
    '            Doc.ReplaceItemValue("$HFFlags", CreateNotesValue("1"))
    '            Doc.ReplaceItemValue("Form", CreateNotesValue("Appointment"))
    '            Doc.ReplaceItemValue("$ExpandGroups", CreateNotesValue("3"))
    '            Doc.ReplaceItemValue("Logo", CreateNotesValue("StdNotesLtr30"))
    '            Doc.ReplaceItemValue("Alarms", CreateNotesValue("1"))
    '            If Int(item.RemindMe.ToOADate) <> Int(item.Date.ToOADate) Then
    '                Doc.ReplaceItemValue("$AlarmUnit", CreateNotesValue("D"))
    '                'Doc.ReplaceItemValue("$AlarmMemoOptions", "")
    '                Doc.ReplaceItemValue("$AlarmDescription", item.Name)
    '                'Doc.ReplaceItemValue("$Alarm", "1")
    '                Doc.ReplaceItemValue("$AlarmOffset", "-60")
    '            End If
    '            'Doc = NotesDB.CreateDocument
    '            'Doc.ReplaceItemValue("$PublicAccess", "1")
    '            'Doc.ReplaceItemValue("Body", "")
    '            'Doc.ReplaceItemValue("Notes", "")
    '            'Doc.ReplaceItemValue("Chair", NS.UserName)
    '            'Doc.ReplaceItemValue("Principal", NS.UserName)
    '            'Doc.ReplaceItemValue("$AltPrincipal", NS.UserName)
    '            'Dim Exclude() As String = {"D", "S"}
    '            'Doc.ReplaceItemValue("ExcludeFromView", Exclude)
    '            'Doc.ReplaceItemValue("SequenceNum", 1)
    '            'Doc.ReplaceItemValue("UpdateSeq", 1)
    '            'Doc.ReplaceItemValue("$CSVersion", 1)
    '            'Doc.ReplaceItemValue("SMTPKeepNotesItems", 1)
    '            'Dim CSWISL() As String = {"$S:1", "$L:1", "$B:1", "$R:1", "$E:1"}
    '            'Doc.ReplaceItemValue("$CSWISL", CSWISL)
    '            'Doc.ReplaceItemValue("WebDateTimeInit", 1)
    '            'Doc.ReplaceItemValue("OrgTable", "C0")
    '            'Doc.ReplaceItemValue("SMTPKeepNotesItems", 1)
    '            'Doc.ReplaceItemValue("$AlarmUnit", "M")
    '            'Doc.ReplaceItemValue("$AlarmMemoOptions", "")
    '            'Doc.ReplaceItemValue("$Alarm", "1")
    '            'Doc.ReplaceItemValue("$AlarmOffset", "0")
    '            'Doc.ReplaceItemValue("STARTDATETIME", item.date
    '            'Doc.ReplaceItemValue("EndDateTime", item.date
    '            'Doc.ReplaceItemValue("CalendarDateTime", item.date
    '            'Doc.ReplaceItemValue("$NoPurge", item.date
    '            'Doc.ReplaceItemValue("_ViewIcon", "10")
    '            'Doc.ReplaceItemValue("$HFFlags", "1")
    '            'Doc.ReplaceItemValue("Form", "Appointment")
    '            'Doc.ReplaceItemValue("$ExpandGroups", "3")
    '            'Doc.ReplaceItemValue("Logo", "StdNotesLtr30")
    '            'Doc.ReplaceItemValue("Saveoptions", "")
    '            'Doc.ReplaceItemValue("MailOptions", "")
    '            'Doc.ReplaceItemValue("Sign", "0")
    '            'Doc.ReplaceItemValue("Encrypt", "0")
    '            'Doc.ReplaceItemValue("From", NS.UserName)
    '            'Doc.ReplaceItemValue("$FromPreferredLanguage", "sv")
    '            'Doc.ReplaceItemValue("ApptUNID", "CB9D4AA60AECF27FC12574020035F06C")
    '            'Doc.ReplaceItemValue("OnlinePlace", "")
    '            'Doc.ReplaceItemValue("$LangChair", "")
    '            'Doc.ReplaceItemValue("AltChair", NS.UserName)
    '            'Doc.ReplaceItemValue("AppointmentType", "4")
    '            'Doc.ReplaceItemValue("Alarms", "1")
    '            'Doc.ReplaceItemValue("OrgConfidential", "")
    '            'Doc.ReplaceItemValue("Subject", "Johan testar notes")
    '            'Doc.ReplaceItemValue("StartTime", item.date
    '            'Doc.ReplaceItemValue("EndTime", item.date
    '            'Doc.ReplaceItemValue("Repeats", "")
    '            'Doc.ReplaceItemValue("OrganizerInclude", "")
    '            'Doc.ReplaceItemValue("Location", "")
    '            'Doc.ReplaceItemValue("RoomToReserve", "")
    '            'Doc.ReplaceItemValue("Resources", "")
    '            'Doc.ReplaceItemValue("OnlineMeeting", "")
    '            'Doc.ReplaceItemValue("MeetingType", "1")
    '            'Doc.ReplaceItemValue("Presenters", "")
    '            'Doc.ReplaceItemValue("OnlinePlaceToReserve", "")
    '            'Doc.ReplaceItemValue("AudioVideoFlags", "")
    '            'Doc.ReplaceItemValue("SendAttachments", "")
    '            'Doc.ReplaceItemValue("WhiteBoardContent", "")
    '            'Doc.ReplaceItemValue("Categories", "")
    '            'Doc.ReplaceItemValue("SchedulerSwitcher", "1")
    '            'Doc.ReplaceItemValue("$BorderColor", "D2DCDC")
    '            'Dim Watched() As String = {"$S", "$L", "$B", "$R", "$E"}
    '            'Doc.ReplaceItemValue("$WatchedItems", Watched)
    '            'Doc.ReplaceItemValue("StartDate", item.date
    '            'Doc.ReplaceItemValue("EndDate", item.date

    '            'For i As Integer = 0 To UBound(NotesDB.AllDocuments.GetNthDocument(13121).Items)
    '            '    Dim Itm As Domino.NotesItem = NotesDB.AllDocuments.GetNthDocument(13121).Items(i)
    '            '    If Itm.Values.Length > 1 Then Stop
    '            '    If Itm.Values.GetType.Name = "Object[]" Then
    '            '        Debug.Print(i & ": " & Itm.Name & " = " & Itm.Values(0).ToString)
    '            '    ElseIf Itm.Values.GetType.Name = "String" Then
    '            '        Debug.Print(i & ": " & Itm.Name & " = " & Itm.Values)
    '            '    Else
    '            '        Stop
    '            '    End If
    '            'Next
    '            Doc.Save(True, False)

    '        End If
    '    End If
    'End Sub

    Function CreateNotesValue(ByVal Value As String)
        Dim TmpString() As String = {Value}
        Return TmpString
    End Function

    Function CreateNotesValue(ByVal Value As Date)
        Dim TmpDTs() As Date = {Value}
        Return TmpDTs
    End Function

    Function CreateNotesValue(ByVal Value() As String)
        Return Value
    End Function

    Sub createReminder(ByVal dateTime As Date, ByVal popUpStr As String, ByVal subjectStr As String, Optional ByVal MinutesBefore As Integer = 0)
        If LotusNotes Is Nothing Then
            If frmNotesPassword.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If
            LotusNotes = New cNotes
            LotusNotes.NotesPassword = frmNotesPassword.txtPassword.Text
        End If
        LotusNotes.CreateCalendarReminder(dateTime, popUpStr, subjectStr, MinutesBefore)
    End Sub

    Private Sub grdImportantDates_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdImportantDates.EditingControlShowing
        If InStr(e.Control.Text, "day(s) before") > 0 Then
            e.Control.Text = e.Control.Text.Substring(0, InStr(e.Control.Text, "day(s) before") - 1).Trim
        End If
    End Sub

    Private Sub tpDates_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpDates.Enter
        grdImportantDates.Rows.Clear()
        For Each TmpDate As cImportantDate In MyEvent.ImportantDates
            grdImportantDates.Rows(grdImportantDates.Rows.Add).Tag = TmpDate
        Next
    End Sub
End Class