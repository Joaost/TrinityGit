Imports System.ComponentModel.Composition
Imports System.ComponentModel.Composition.Hosting
Imports System.Reflection
Imports TrinityPlugin
Imports System.ServiceModel
Imports System.Xml.Serialization
Imports System.Windows.Forms
Imports clTrinity

Public Class frmImport

    Dim tv4Booking As TV4Online.SpotlightApiV23.xsd.Booking
    Dim tv4Confirmation As TV4Online.SpotlightApiV23.xsd.Confirmation
    Dim _camp As Object

    Public Sub New(ByVal tv4channel As TV4Online.SpotlightApiV23.xsd.Booking, ByVal confirmation As TV4Online.SpotlightApiV23.xsd.Confirmation, ByVal trinityChannel As Object, ByVal camp As Object)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        For Each TmpBT In trinityChannel.BookingTypes
            If TmpBT.BookIt Then
                cmbBookingType.Items.Add(TmpBT.Name)
            End If
        Next
        tv4Booking = tv4channel
        _camp = camp
        tv4Confirmation = confirmation
    End Sub

    Private Sub cmdOk_Click(sender As Object, e As EventArgs) Handles cmdOk.Click
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub cmbBookingType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBookingType.SelectedIndexChanged
        lblCurrentBudget.Text = Format(_camp.Channels(tv4Booking.Channel).BookingTypes(cmbBookingType.Text).ConfirmedNetBudget, "N0")  & " " & "kr"
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click        
    End Sub
    
    Private Sub btnIgnore_Click(sender As Object, e As EventArgs) Handles btnIgnore.Click
        Me.DialogResult = DialogResult.Ignore
    End Sub

    Private Sub lblChannelName_Click_1(sender As Object, e As EventArgs) Handles lblChannelName.Click
        
        
        Dim frmConfPreview As New frmPreviewConfirmation(tv4Confirmation)
        frmConfPreview.Text = tv4Confirmation.Channel & " " & tv4Confirmation.Versions.Max().Name
        frmConfPreview.ShowDialog()
    End Sub
End Class