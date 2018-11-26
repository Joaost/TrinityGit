Imports System.Windows.Forms

Public Class frmLockedCampaign

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReadOnly.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmLockedCampaign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public Sub New(ByVal Message As String)

        ' This call is required by the designer.
        InitializeComponent()

        lblMessage.Text = Message

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnlock.Click
        Me.DialogResult = Windows.Forms.DialogResult.Ignore
        Me.Close()
    End Sub
End Class
