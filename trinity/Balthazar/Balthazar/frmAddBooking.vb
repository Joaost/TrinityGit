Imports System.Windows.Forms

Public Class frmAddBooking

    Private Structure BookingDate
        Public [Date] As Date
        Public Time As Integer

        Public Overrides Function ToString() As String
            Return Format([Date], "Short Date") & " (" & TimeString(Time) & ")"
        End Function

        Public ReadOnly Property TimeString(ByVal Time As Integer)
            Get
                If Weekday([Date], FirstDayOfWeek.Monday) > 5 Then
                    Select Case Time
                        Case 1
                            Return "10-15"
                        Case 2
                            Return "11-16"
                    End Select
                Else
                    Select Case Time
                        Case 1
                            Return "10-18"
                        Case 2
                            Return "11-19"
                    End Select
                End If
                Return ""
            End Get
        End Property
    End Structure

    Private Structure Collaboration
        Public Company As String
        Public Product As String
        Public InvoiceShare As Byte
        Public Reference As String
        Public PhoneNo As String
        Public InvoiceAddress As String
        Public ZipCode As String

        Public Overrides Function ToString() As String
            Return Product
        End Function
    End Structure

    Public AddedBookingID As Integer

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim ProviderID As Integer
        If cmbRequestedProvider.SelectedItem IsNot Nothing Then
            ProviderID = DirectCast(cmbRequestedProvider.SelectedItem, cStaff).DatabaseID
        Else
            ProviderID = 0
        End If
        Dim BookingID As Integer = Database.SaveBooking(MyEvent.DatabaseID, DirectCast(cmbSalesman.SelectedItem, cStaff).DatabaseID, txtStore.Text, txtAddress.Text, txtCity.Text, txtStorePhone.Text, txtStoreContact.Text, txtStorePlacement.Text, txtOtherRequests.Text, chkUseStoreKitchen.Checked, ProviderID, cmbRequestedProvider.Text)

        For Each TmpDate As BookingDate In lstDates.Items
            Database.AddBookingDate(BookingID, TmpDate.Date, TmpDate.Time)
        Next

        For Each TmpProduct As String In lstProducts.Items
            Database.AddBookingProduct(BookingID, TmpProduct)
        Next

        For Each TmpColl As Collaboration In lstCollaborators.Items
            Database.AddCollaboration(BookingID, TmpColl.Company, TmpColl.Product, TmpColl.InvoiceShare, TmpColl.Reference, TmpColl.InvoiceAddress, TmpColl.PhoneNo, TmpColl.ZipCode)
        Next

        AddedBookingID = BookingID
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmdAddDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddDate.Click
        pnlCalendar.Visible = Not pnlCalendar.Visible
    End Sub

    Private Sub cmdAddIt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddIt.Click
        Dim TmpBDate As BookingDate
        TmpBDate.Date = calDates.SelectionRange.Start
        TmpBDate.Time = 1 + (Math.Abs(CInt(optTime2.Checked)) * 1)
        lstDates.Items.Add(TmpBDate)
        calDates.Focus()
    End Sub

    Private Sub frmAddBooking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbSalesman.Items.Clear()
        For Each TmpSalesman As cChosenSalesPerson In MyEvent.InStore.ChosenSalespersons
            cmbSalesman.Items.Add(TmpSalesman.Staff)
        Next
        cmbRequestedProvider.Items.Clear()
        For Each TmpProvider As cStaff In MyEvent.InStore.ChosenProviders
            cmbRequestedProvider.Items.Add(TmpProvider)
        Next
        cmbSalesman.SelectedIndex = 0
        calDates.MinDate = MyEvent.InStore.StartDate
        calDates.MaxDate = MyEvent.InStore.EndDate
        For Each TmpDate As Date In MyEvent.InStore.ExcludedDates
            calDates.AddBoldedDate(TmpDate)
        Next
        lstProducts.Items.Clear()
        lstProducts.Items.Add(MyEvent.Product.Name)
        lstDates.Items.Clear()
    End Sub

    Private Sub calDates_DateChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles calDates.DateChanged
        Dim TmpBDate As BookingDate
        TmpBDate.Date = e.Start
        optTime1.Text = TmpBDate.TimeString(1)
        optTime2.Text = TmpBDate.TimeString(2)
    End Sub

    Private Sub cmdRemoveDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveDate.Click
        If lstDates.SelectedIndex > -1 Then
            lstDates.Items.RemoveAt(lstDates.SelectedIndex)
        End If
    End Sub

    Private Sub cmdAddProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddProduct.Click
        Dim TmpProduct As String = InputBox("Name of product to demonstrate:", "BALTHAZAR", "")
        If TmpProduct <> "" Then
            lstProducts.Items.Add(TmpProduct)
        End If
    End Sub

    Private Sub cmdRemoveProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveProduct.Click
        If lstProducts.SelectedIndex > -1 Then
            lstProducts.Items.RemoveAt(lstProducts.SelectedIndex)
        End If
    End Sub

    Private Sub cmdAddCollaborator_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddCollaborator.Click
        If frmAddCollaboration.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim TmpColl As New Collaboration
            TmpColl.Company = frmAddCollaboration.txtCompany.Text
            TmpColl.Product = frmAddCollaboration.txtProduct.Text
            TmpColl.InvoiceShare = frmAddCollaboration.txtInvoiceShare.Text
            TmpColl.Reference = frmAddCollaboration.txtReference.Text
            TmpColl.InvoiceAddress = frmAddCollaboration.txtInvoiceAddress.Text
            TmpColl.PhoneNo = frmAddCollaboration.txtPhoneNumber.Text
            TmpColl.ZipCode = frmAddCollaboration.txtZipCode.Text
            lstCollaborators.Items.Add(TmpColl)
        End If
    End Sub

    Private Sub cmdRemoveCollaborator_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveCollaborator.Click
        If lstCollaborators.SelectedIndex > -1 Then
            lstCollaborators.Items.RemoveAt(lstCollaborators.SelectedIndex)
        End If
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        If frmFindStore.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim TmpStore As cStore = Database.GetStores.ToDictionary(Function(s) s.DatabaseID)(frmFindStore.grdStores.SelectedRows(0).Tag)
            txtStore.Text = TmpStore.Name
            txtAddress.Text = TmpStore.Address
            txtStorePhone.Text = TmpStore.PhoneNo
            txtCity.Text = TmpStore.City
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Database.SaveStore(New cStore With {.Name = txtStore.Text, .City = txtCity.Text, .Address = txtAddress.Text, .PhoneNo = txtStorePhone.Text})
        Windows.Forms.MessageBox.Show("Store was saved in database.", "Balthazar", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class
