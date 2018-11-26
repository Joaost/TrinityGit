Public Class frmInStore
    Implements IBookingEventConsumer

#Region "frmAllBookings Mirrored"

    Private BookingList As Dictionary(Of Integer, cBooking)
    Private NewBookings As New Dictionary(Of Integer, cBooking)

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BookingList = Database.GetBookingsForEvent(MyEvent.DatabaseID)
        UpdateList()
    End Sub

    Sub GABProgress(ByVal p As Integer)
        frmProgress.Progress = p
    End Sub

    Sub UpdateList()
        Dim TmpList As New List(Of String)
        Dim ProviderList As List(Of cStaff) = Database.GetStaffList(cStaff.UserTypeEnum.Provider).Values.ToList
        grdBookings.Rows.Clear()
        frmProgress.Show()

        Dim ListOfBookings As List(Of cBooking) = (From Booking As cBooking In BookingList.Values Select Booking).ToList
        Dim emptyDate As Boolean = False
        For Each tmpBooking As cBooking In ListOfBookings
            Try
                Dim tmpDate As Date = tmpBooking.Dates.Item(0).Date
            Catch
                emptyDate = True
            End Try
        Next

        Dim AllBookings As List(Of cBooking)

        If emptyDate Then
            MessageBox.Show("A booking has empty dates. Unable to sort the list in order of date.")
            AllBookings = (From Booking As cBooking In BookingList.Values Select Booking Where (Not chkNotInvoiced.Checked OrElse Booking.Invoiced) AndAlso (Not chkNotConfirmed.Checked OrElse Booking.Status = cBooking.BookingStatusEnum.bsPending)).ToList
        Else
            AllBookings = (From Booking As cBooking In BookingList.Values Select Booking Where (Not chkNotInvoiced.Checked OrElse Booking.Invoiced) AndAlso (Not chkNotConfirmed.Checked OrElse Booking.Status = cBooking.BookingStatusEnum.bsPending) Order By Booking.Dates.Item(0).Date).ToList
        End If


        TmpList = (From booking As cBooking In BookingList.Values Select booking.Responsible Distinct Order By Responsible).ToList
        'TmpList = (From booking As cBooking In BookingList.Values Select booking.Responsible Distinct Order By Responsible).ToList
        Dim count As Integer = 1
        grdBookings.SuspendLayout()
        Try
            AllBookings.Sort()
        Catch

        End Try
        For Each TmpBooking As cBooking In AllBookings
            frmProgress.Progress = (count / AllBookings.Count) * 100
            'If cmbResponsible.SelectedIndex = 0 OrElse TmpBooking.Responsible = cmbResponsible.Text Then
            'If Not chkNotConfirmed.Checked OrElse TmpBooking.Status = cBooking.BookingStatusEnum.bsPending Then
            'If Not chkNotInvoiced.Checked OrElse Not TmpBooking.Invoiced Then
            With grdBookings.Rows(grdBookings.Rows.Add)
                .Tag = TmpBooking.DatabaseID
                DirectCast(.Cells("colProvider"), ExtendedComboBoxCell).ComboBox.DropDownStyle = ComboBoxStyle.DropDown
                DirectCast(.Cells("colProvider"), ExtendedComboBoxCell).Items.Clear()
                DirectCast(.Cells("colProvider"), ExtendedComboBoxCell).ComboBox.DisplayMember = "FirstName"
                For Each TmpProvider As cStaff In ProviderList
                    DirectCast(.Cells("colProvider"), ExtendedComboBoxCell).Items.Add(TmpProvider)
                Next
            End With
            'End If
            'End If
            'End If
            count += 1
        Next
        frmProgress.Hide()
        grdBookings.ResumeLayout()
        Database.RegisterBookingEventConsumer(Me)
    End Sub

    Private Sub grdBookings_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdBookings.CellDoubleClick
        If e.RowIndex < 0 Then Exit Sub
        Dim TmpBooking As cBooking = BookingList(grdBookings.Rows(e.RowIndex).Tag)
        Dim r As Integer = 0

        With frmDetails.grdDetails
            .Rows.Clear()
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            .Rows.Add("Event", TmpBooking.EventName)
            .Rows.Add("Client", TmpBooking.Client.Name)
            With .Rows(.Rows.Add)
                .Cells(0).Value = "Sales person:"
                .Cells(1).Value = TmpBooking.Salesperson.Firstname & " " & TmpBooking.Salesperson.LastName
            End With
            With .Rows(.Rows.Add)
                .Cells(0).Value = "Mobile Phone:"
                .Cells(1).Value = TmpBooking.Salesperson.MobilePhone
            End With
            r = .Rows.Count
            For Each TmpDT As cBooking.DateTime In TmpBooking.Dates
                With .Rows(.Rows.Add)
                    .Cells(1).Value = TmpDT.Date & "  kl." & TmpDT.TimeString
                End With
            Next
            .Rows(r).Cells(0).Value = "Dates:"
            r = .Rows.Count
            For Each TmpString As String In TmpBooking.Products
                .Rows.Add("", TmpString)
            Next
            If TmpBooking.Products.Count = 0 Then .Rows.Add("", "-")
            .Rows(r).Cells(0).Value = "Product(s):"
            With .Rows(.Rows.Add)
                .Cells(0).Value = "Store:"
                .Cells(1).Value = TmpBooking.Store
            End With
            .Rows.Add("Address:", TmpBooking.Address)
            .Rows.Add("City:", TmpBooking.City)
            .Rows.Add("Store Phone:", TmpBooking.PhoneNr)
            .Rows.Add("Contact:", TmpBooking.Contact)
            .Rows.Add("Placement:", TmpBooking.Placement)
            .Rows.Add("Comments:", TmpBooking.Comments)

            With .Rows(.Rows.Add)
                .Cells(0).Value = "Provider:"
                .Cells(1).Value = TmpBooking.ProviderName
            End With
            With .Rows(.Rows.Add)
                .Cells(0).Value = "Invoiced:"
                If TmpBooking.Invoiced Then
                    .Cells(1).Value = "Yes"
                Else
                    .Cells(1).Value = "No"
                End If
            End With
            .Height = .Rows(0).Height * .Rows.Count + 3
            Dim Width As Integer = 0
            For i As Integer = 0 To .Columns.Count - 1
                Width += .Columns(i).Width
            Next
            .Width = Width + 3
        End With
        frmDetails.ShowDialog()
    End Sub

    Private Sub grdBookings_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdBookings.CellEnter
        If grdBookings.Rows(e.RowIndex).Tag Is Nothing Then Exit Sub
        If e.ColumnIndex = 0 Then
            Dim TmpBooking As cBooking = BookingList(grdBookings.Rows(e.RowIndex).Tag)
            If TmpBooking Is Nothing Then Exit Sub
            If TmpBooking.Dates.Count = 0 Then Exit Sub
            If TmpBooking.Dates(TmpBooking.Dates.Count - 1).Date.ToOADate - TmpBooking.Dates(0).Date.ToOADate > TmpBooking.Dates.Count - 1 Then
                lblDates.Text = ""
                For Each TmpDT As cBooking.DateTime In TmpBooking.Dates
                    lblDates.Text &= Format(TmpDT.Date, "Short date")
                    If TmpDT.Time = 0 Then
                        If TmpDT.Date.DayOfWeek = DayOfWeek.Saturday OrElse TmpDT.Date.DayOfWeek = DayOfWeek.Sunday Then
                            lblDates.Text &= " 10-15" & vbCrLf
                        Else
                            lblDates.Text &= " 10-18" & vbCrLf
                        End If
                    Else
                        If TmpDT.Date.DayOfWeek = DayOfWeek.Saturday OrElse TmpDT.Date.DayOfWeek = DayOfWeek.Sunday Then
                            lblDates.Text &= " 11-19" & vbCrLf
                        Else
                            lblDates.Text &= " 11-16" & vbCrLf
                        End If
                    End If
                Next
                lblDates.Top = grdBookings.GetRowDisplayRectangle(e.RowIndex, True).Top + Me.Top + 15
                lblDates.Visible = True
            Else
                lblDates.Visible = False
            End If
        Else
            lblDates.Visible = False
        End If

    End Sub

    Private Sub grdBookings_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdBookings.CellFormatting
        If Not BookingList.ContainsKey(grdBookings.Rows(e.RowIndex).Tag) Then
            Exit Sub
        End If
        Dim TmpBooking As cBooking = BookingList(grdBookings.Rows(e.RowIndex).Tag)

        Select Case grdBookings.Columns(e.ColumnIndex).HeaderText
            Case "Dates"
                If TmpBooking.Dates.Count = 0 Then Exit Sub
                If TmpBooking.Dates(TmpBooking.Dates.Count - 1).Date.ToOADate - TmpBooking.Dates(0).Date.ToOADate > TmpBooking.Dates.Count - 1 Then
                    e.CellStyle.ForeColor = Color.Blue
                End If
            Case "Provider"
                If TmpBooking.ProviderConfirmed = cBooking.BookingStatusEnum.bsConfirmed Then
                    e.CellStyle.ForeColor = Color.Green
                    grdBookings.Rows(e.RowIndex).Cells(e.ColumnIndex).ReadOnly = True
                ElseIf TmpBooking.ProviderConfirmed = cBooking.BookingStatusEnum.bsPending Then
                    e.CellStyle.ForeColor = Color.Blue
                Else
                    e.CellStyle.ForeColor = Color.Red
                End If
        End Select
    End Sub

    Private Sub grdBookings_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdBookings.CellValueNeeded
        If Not BookingList.ContainsKey(grdBookings.Rows(e.RowIndex).Tag) Then
            Exit Sub
        End If
        Dim TmpBooking As cBooking = BookingList(grdBookings.Rows(e.RowIndex).Tag)

        Select Case grdBookings.Columns(e.ColumnIndex).HeaderText
            Case "Dates"
                If TmpBooking.Dates.Count = 0 Then Exit Sub
                e.Value = Format(TmpBooking.Dates(0).Date, "yyyyMMdd") & "-" & Format(TmpBooking.Dates(TmpBooking.Dates.Count - 1).Date, "yyyyMMdd")
            Case "Days"
                e.Value = TmpBooking.Dates.Count
            Case "Sales person"
                e.Value = TmpBooking.Salesperson.Firstname & " " & TmpBooking.Salesperson.LastName
            Case "Store"
                e.Value = TmpBooking.Store
            Case "City"
                e.Value = TmpBooking.City
            Case "Req.Provider"
                e.Value = TmpBooking.RequestedProvider
            Case "Provider"
                e.Value = TmpBooking.ProviderName
            Case "Confirmed"
                Select Case TmpBooking.Status
                    Case cBooking.BookingStatusEnum.bsConfirmed
                        e.Value = "Confirmed"
                    Case cBooking.BookingStatusEnum.bsRejected
                        e.Value = "Rejected"
                    Case cBooking.BookingStatusEnum.bsPending
                        e.Value = ""
                End Select
            Case "Invoiced"
                e.Value = TmpBooking.Invoiced
            Case "Event"
                e.Value = TmpBooking.EventName
            Case "Client"
                e.Value = TmpBooking.Client.Name
            Case "Q-days"
                e.Value = TmpBooking.AnsweredQuestionaireDays
        End Select
    End Sub

    Private Sub grdBookings_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdBookings.CellValuePushed
        If Not BookingList.ContainsKey(grdBookings.Rows(e.RowIndex).Tag) Then
            Exit Sub
        End If
        Dim TmpBooking As cBooking = BookingList(grdBookings.Rows(e.RowIndex).Tag)

        Select Case grdBookings.Columns(e.ColumnIndex).HeaderText
            Case "Provider"
                If DirectCast(grdBookings.Rows(e.RowIndex).Cells(e.ColumnIndex), ExtendedComboBoxCell).ComboBox.SelectedItem IsNot Nothing Then
                    TmpBooking.Provider = DirectCast(grdBookings.Rows(e.RowIndex).Cells(e.ColumnIndex), ExtendedComboBoxCell).ComboBox.SelectedItem
                    Database.SetProvider(TmpBooking.DatabaseID, TmpBooking.Provider)
                Else
                    TmpBooking.Provider = Nothing
                    TmpBooking.ProviderName = DirectCast(grdBookings.Rows(e.RowIndex).Cells(e.ColumnIndex), ExtendedComboBoxCell).ComboBox.Text
                    Database.SetProvider(TmpBooking.DatabaseID, Nothing, TmpBooking.ProviderName)
                End If
            Case "Confirmed"
                Select Case e.Value
                    Case "Confirmed"
                        TmpBooking.Status = cBooking.BookingStatusEnum.bsConfirmed
                    Case "Rejected"
                        TmpBooking.Status = cBooking.BookingStatusEnum.bsRejected
                        TmpBooking.RejectionComment = InputBox("Comment:", "BALTHAZAR", TmpBooking.RejectionComment)
                    Case ""
                        TmpBooking.Status = cBooking.BookingStatusEnum.bsPending
                End Select
                Database.SetBookingStatus(TmpBooking.DatabaseID, TmpBooking.Status)
                Database.SetRejectionComment(TmpBooking.DatabaseID, TmpBooking.RejectionComment)
            Case "Invoiced"
                TmpBooking.Invoiced = e.Value
                Database.SetBookingIsInvoiced(TmpBooking.DatabaseID, e.Value)
        End Select
    End Sub

    Private Sub chkNotConfirmed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNotConfirmed.CheckedChanged
        UpdateList()
    End Sub

    Private Sub chkNotInvoiced_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNotInvoiced.CheckedChanged
        UpdateList()
    End Sub

    Private Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click
        Dim Excel As Object = CreateObject("excel.application")
        Dim WB As Object = Excel.Workbooks.Add

        Excel.screenupdating = False
        With WB.sheets(1)
            For c As Integer = 0 To grdBookings.Columns.Count - 1
                .cells(1, c + 1).value = grdBookings.Columns(c).HeaderText
                For r As Integer = 0 To grdBookings.RowCount - 1
                    .cells(r + 2, c + 1).value = grdBookings.Rows(r).Cells(c).Value.ToString
                Next
            Next
        End With
        Excel.screenupdating = True
        Excel.visible = True
    End Sub

    Public Sub BookingsUpdated(ByRef e As IBookingEventConsumer.BookingsUpdatedEventArgs) Implements IBookingEventConsumer.BookingsUpdated
        If e.ListOfUpdatedEntries.Count > 0 Then
            For Each TmpBooking As cBooking In (From Booking As cBooking In e.ListOfUpdatedEntries.Values Select Booking Where Not BookingList.ContainsKey(Booking.DatabaseID)).ToList
                NewBookings.Add(TmpBooking.DatabaseID, TmpBooking)
            Next
            BookingList = e.ListOfAllEntries
            For Each TmpRow As DataGridViewRow In grdBookings.Rows
                If e.ListOfUpdatedEntries.ContainsKey(TmpRow.Tag) Then
                    grdBookings.InvalidateRow(TmpRow.Index)
                End If
            Next
        End If
        'grdBookings.Invalidate()
    End Sub

    Private Sub grdBookings_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdBookings.RowEnter
        BookingList = Database.GetBookingsForEvent(MyEvent.DatabaseID)
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        If MessageBox.Show("Deleting bookings will remove them permanently from the database." & vbCrLf & vbCrLf & _
                            "If a booking has been confirmed by a provider, make sure they" & vbCrLf & _
                            "are properly notified after booking is deleted." & vbCrLf & vbCrLf & _
                            "Are you sure you want to delete these bookings?", "BALTHAZAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            For Each TmpRow As DataGridViewRow In grdBookings.SelectedRows
                Try
                    Database.DeleteBooking(TmpRow.Tag)
                    BookingList.Remove(TmpRow.Tag)
                    grdBookings.Rows.Remove(TmpRow)
                Catch ex As Exception
                    Windows.Forms.MessageBox.Show(ex.Message, "BALTHAZAR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next
        End If
    End Sub

    Private Sub tmrCheckForChanges_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrCheckForChanges.Tick
        tmrCheckForChanges.Enabled = False
        BookingList = Database.GetBookingsForEvent(MyEvent.DatabaseID)
        If NewBookings.Count > 0 Then
            Dim ProviderList As List(Of cStaff) = Database.GetStaffList(cStaff.UserTypeEnum.Provider).Values.ToList
            While NewBookings.Values.Count > 0
                Dim TmpBooking As cBooking = NewBookings.Values.ToList(0)
                With grdBookings.Rows(grdBookings.Rows.Add)
                    .Tag = TmpBooking.DatabaseID
                    DirectCast(.Cells("colProvider"), ExtendedComboBoxCell).ComboBox.DropDownStyle = ComboBoxStyle.DropDown
                    DirectCast(.Cells("colProvider"), ExtendedComboBoxCell).Items.Clear()
                    DirectCast(.Cells("colProvider"), ExtendedComboBoxCell).ComboBox.DisplayMember = "FirstName"
                    For Each TmpProvider As cStaff In ProviderList
                        DirectCast(.Cells("colProvider"), ExtendedComboBoxCell).Items.Add(TmpProvider)
                    Next
                End With
                NewBookings.Remove(TmpBooking.DatabaseID)
            End While
        End If
        tmrCheckForChanges.Enabled = True
    End Sub
#End Region


    Private Sub cmdWizard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWizard.Click
        Dim TmpMnu As New Windows.Forms.ContextMenuStrip
        TmpMnu.Items.Add("Exclude all sundays", Nothing, AddressOf ExcludeSundays)
        TmpMnu.Items.Add("Exclude holidays", Nothing, AddressOf ExcludeHolidays).Enabled = False
        TmpMnu.Show(cmdWizard, New Point(0, cmdWizard.Height))
    End Sub

    Sub ExcludeSundays(ByVal sender As Object, ByVal e As EventArgs)
        For d As Long = dtFrom.Value.ToOADate To dtTo.Value.ToOADate
            If Weekday(Date.FromOADate(d)) = 1 Then
                lstExcludedDates.Items.Add(Date.FromOADate(d))
                MyEvent.InStore.ExcludedDates.Add(Date.FromOADate(d))
            End If
        Next
    End Sub

    Sub ExcludeHolidays(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub cmdAddIt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddIt.Click
        lstExcludedDates.Items.Add(calExcluded.SelectionRange.Start)
        MyEvent.InStore.ExcludedDates.Add(calExcluded.SelectionRange.Start)
        calExcluded.Focus()
    End Sub

    Private Sub cmdAddDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddDate.Click
        pnlCalendar.Visible = Not pnlCalendar.Visible
    End Sub

    Private Sub frmInStore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Click
        Me.Focus()
    End Sub

    Private Sub dtFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtFrom.ValueChanged
        MyEvent.InStore.StartDate = dtFrom.Value
    End Sub

    Private Sub dtTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtTo.ValueChanged
        MyEvent.InStore.EndDate = dtTo.Value
    End Sub

    Private Sub txtMaxBookings_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMaxBookings.KeyUp
        If txtMaxBookings.Text = "" Then Exit Sub
        MyEvent.InStore.MaxBookingsPerDay = txtMaxBookings.Text
    End Sub

    Private Sub txtMaxBookings_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMaxBookings.TextChanged
    End Sub

    Private Sub frmInStore_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not MyEvent.StartDate = Nothing Then dtFrom.Value = MyEvent.InStore.StartDate
        If Not MyEvent.EndDate = Nothing Then dtTo.Value = MyEvent.InStore.EndDate
        txtMaxBookings.Text = MyEvent.InStore.MaxBookingsPerDay

        lstSalespersons.Items.Clear()
        For Each TmpStaff As cStaff In Database.GetStaffList(cStaff.UserTypeEnum.Salesman, MyEvent.Client.ID).Values
            lstSalespersons.Items.Add(TmpStaff)
        Next

        lstExcludedDates.Items.Clear()
        For Each TmpDate As Date In MyEvent.InStore.ExcludedDates
            lstExcludedDates.Items.Add(TmpDate)
        Next

        grdChosenSP.Rows.Clear()
        For Each TmpStaff As cChosenSalesPerson In MyEvent.InStore.ChosenSalespersons
            If TmpStaff.Staff IsNot Nothing Then grdChosenSP.Rows(grdChosenSP.Rows.Add).Tag = TmpStaff
        Next

        lstProviders.Items.Clear()
        For Each TmpStaff As cStaff In Database.GetStaffList(cStaff.UserTypeEnum.Provider).Values
            lstProviders.Items.Add(TmpStaff)
        Next

        lstChosenProviders.Items.Clear()
        For Each TmpStaff As cStaff In MyEvent.InStore.ChosenProviders
            If Not TmpStaff Is Nothing Then
                lstChosenProviders.Items.Add(TmpStaff)
            End If
        Next

        cmbDemoInstruction.Items.Clear()
        For Each TmpDoc As cDocument In MyEvent.Documents.Values
            cmbDemoInstruction.Items.Add(TmpDoc)
        Next
        cmbDemoInstruction.SelectedItem = MyEvent.InStore.DemoInstructions
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        For Each TmpStaff As cStaff In lstSalespersons.SelectedItems
            Dim TmpSP As New cChosenSalesPerson
            TmpSP.Staff = TmpStaff
            TmpSP.MaxDays = 0
            grdChosenSP.Rows(grdChosenSP.Rows.Add).Tag = TmpSP
            MyEvent.InStore.ChosenSalespersons.Add(TmpSP)
        Next
    End Sub

    Private Sub cmdRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
        For Each TmpRow As DataGridViewRow In grdChosenSP.SelectedRows
            MyEvent.InStore.ChosenSalespersons.Remove(TmpRow.Tag)
            grdChosenSP.Rows.Remove(TmpRow)
        Next
    End Sub

    Private Sub cmdRemoveSalesperson_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveSalesperson.Click
        lstSalespersons.Items.Remove(lstSalespersons.SelectedItems)
    End Sub

    Private Sub cmdAddSalesperson_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddSalesperson.Click
        If frmAddSalesperson.ShowDialog = Windows.Forms.DialogResult.OK Then
            lstSalespersons.Items.Add(frmAddSalesperson.Tag)
        End If
    End Sub

    Private Sub cmdPublish_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPublish.Click
        If Windows.Forms.MessageBox.Show("This will save your campaign." & vbCrLf & vbCrLf & "Proceed?", "Balthazar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        MyEvent.Save()
        Database.SaveInStoreToDB(MyEvent.InStore)
        Windows.Forms.MessageBox.Show("The campaign was successfully published to the web.", "BALTHAZAR", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub cmdChooseProvider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChooseProvider.Click
        For Each TmpStaff As cStaff In lstProviders.SelectedItems
            lstChosenProviders.Items.Add(TmpStaff)
            MyEvent.InStore.ChosenProviders.Add(TmpStaff)
        Next
    End Sub

    Private Sub cmdRemoveProvider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveProvider.Click
        MyEvent.InStore.ChosenProviders.Remove(lstChosenProviders.SelectedItem)
        lstChosenProviders.Items.Remove(lstChosenProviders.SelectedItem)
    End Sub

    Private Sub tpBookings_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles tpBookings.Enter
        'grdBookings.Rows.Clear()
        'For Each TmpBooking As cBooking In Database.GetBookingsForEvent(MyEvent.DatabaseID).Values
        '    With grdBookings.Rows(grdBookings.Rows.Add)
        '        .Tag = TmpBooking
        '        DirectCast(.Cells("colProvider"), ExtendedComboBoxCell).ComboBox.DropDownStyle = ComboBoxStyle.DropDown
        '        DirectCast(.Cells("colProvider"), ExtendedComboBoxCell).Items.Clear()
        '        DirectCast(.Cells("colProvider"), ExtendedComboBoxCell).ComboBox.DisplayMember = "FirstName"
        '        For Each TmpProvider As cStaff In MyEvent.InStore.ChosenProviders
        '            DirectCast(.Cells("colProvider"), ExtendedComboBoxCell).Items.Add(TmpProvider)
        '        Next
        '    End With
        '    TmpBooking.Status = MyEvent.BookingData(TmpBooking.DatabaseID).Status
        '    TmpBooking.Provider = MyEvent.BookingData(TmpBooking.DatabaseID).Provider
        '    TmpBooking.ProviderName = MyEvent.BookingData(TmpBooking.DatabaseID).ProviderName
        'Next
    End Sub


    Private Sub cmdPublishBookings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Windows.Forms.MessageBox.Show("This will save your campaign." & vbCrLf & vbCrLf & "Proceed?", "BALTHAZAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        MyEvent.Save()
        Database.SaveBookingData(MyEvent.BookingData)
    End Sub

    Private Sub grdChosenSP_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdChosenSP.CellValueNeeded
        Dim TmpSP As cChosenSalesPerson = grdChosenSP.Rows(e.RowIndex).Tag

        If Not TmpSP.Staff Is Nothing Then
            Select Case grdChosenSP.Columns(e.ColumnIndex).HeaderText
                Case "Sales person"
                    e.Value = TmpSP.Staff.LastName & ", " & TmpSP.Staff.Firstname
                Case "Max days"
                    e.Value = TmpSP.MaxDays
            End Select
        End If

    End Sub

    Private Sub grdChosenSP_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdChosenSP.CellValuePushed
        Dim TmpSP As cChosenSalesPerson = grdChosenSP.Rows(e.RowIndex).Tag

        Select Case grdChosenSP.Columns(e.ColumnIndex).HeaderText
            Case "Max days"
                TmpSP.MaxDays = e.Value
        End Select
    End Sub

    Private Sub cmdAddProvider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddProvider.Click
        If frmAddProvider.ShowDialog = Windows.Forms.DialogResult.OK Then
            lstProviders.Items.Add(frmAddProvider.Tag)
        End If
    End Sub

    Private Sub cmbDemoInstruction_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDemoInstruction.SelectedIndexChanged
        MyEvent.InStore.DemoInstructions = cmbDemoInstruction.SelectedItem
    End Sub

    Private Sub cmdDeleteCampaign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteCampaign.Click
        If MessageBox.Show("Are you sure you want to remove this campaign?", "D E L E T E", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.OK Then
            Database.RemoveCampaign(MyEvent)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteProvider.Click
        If MessageBox.Show("Are you sure you want to remove this provider?", "D E L E T E", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.OK Then
            Database.RemoveSingleStaff(lstProviders.SelectedItem.databaseid)
            lstProviders.Items.Clear()
            For Each TmpStaff As cStaff In Database.GetStaffList(cStaff.UserTypeEnum.Provider).Values
                lstProviders.Items.Add(TmpStaff)
            Next
        End If
    End Sub

    Private Sub cmdAddBooking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddBooking.Click
        If frmAddBooking.ShowDialog() = Windows.Forms.DialogResult.OK Then

        End If
    End Sub
End Class