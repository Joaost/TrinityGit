Public Class frmAllBookings
    Implements IBookingEventConsumer

    Private BookingList As Dictionary(Of Integer, cBooking)
    Private NewBookings As New Dictionary(Of Integer, cBooking)

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmbResponsible.Items.Clear()
        BookingList = Database.GetAllBookings.ToDictionary(Function(b) b.DatabaseID)
        UpdateList()
        cmbResponsible.Tag = "NOUPDATE"
        cmbResponsible.SelectedIndex = 0
        cmbResponsible.Tag = ""
    End Sub

    Sub GABProgress(ByVal p As Integer)
        frmProgress.Progress = p
    End Sub

    Sub UpdateList()
        Dim TmpList As New List(Of String)
        Dim ProviderList As List(Of cStaff) = Database.GetStaffList(cStaff.UserTypeEnum.Provider).Values.ToList
        grdBookings.Rows.Clear()
        frmProgress.Show()
        Dim AllBookings As List(Of cBooking) = (From Booking As cBooking In BookingList.Values Select Booking Where (Not chkNotInvoiced.Checked OrElse Booking.Invoiced) AndAlso (cmbResponsible.SelectedIndex <= 0 OrElse cmbResponsible.SelectedItem = Booking.Responsible) AndAlso (Not chkNotConfirmed.Checked OrElse Booking.Status = cBooking.BookingStatusEnum.bsPending)).ToList
        TmpList = (From booking As cBooking In BookingList.Values Select booking.Responsible Distinct Order By Responsible).ToList
        Dim count As Integer = 1
        grdBookings.SuspendLayout()
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
        If cmbResponsible.Items.Count = 0 Then '
            cmbResponsible.Items.Add("All")
            For Each TmpString As String In TmpList
                cmbResponsible.Items.Add(TmpString)
            Next
        End If
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

    Private Sub cmbResponsible_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbResponsible.SelectedIndexChanged
        If Not cmbResponsible.Tag = "NOUPDATE" Then UpdateList()
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

    Private Sub grdBookings_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdBookings.CellContentClick

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
        BookingList = Database.GetAllBookings.ToDictionary(Function(b) b.DatabaseID)
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
        BookingList = Database.GetAllBookings.ToDictionary(Function(b) b.DatabaseID)
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
End Class