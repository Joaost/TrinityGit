Imports System.Windows.Forms
Public Class frmInputDialog

    'Static context
    Public Overloads Shared Function Show(ByVal strMessage As String, ByVal strTitle As String, ByVal strDefaultValue As String) As inputDialogResult
        Dim tmpInputDialog As frmInputDialog = New frmInputDialog(strMessage, strTitle, strDefaultValue)

        Dim tmpDialogResult As DialogResult = tmpInputDialog.ShowDialog()

        If tmpDialogResult = Windows.Forms.DialogResult.OK Then
            Return New inputDialogResult(Windows.Forms.DialogResult.OK, tmpInputDialog.txbInput.Text)
        Else
            Return New inputDialogResult(Windows.Forms.DialogResult.Cancel, tmpInputDialog.strDefault)
        End If
    End Function

    'Instanced context
    Dim strDefault As String
    Private Sub New(ByVal strMessage As String, ByVal strTitle As String, ByVal strDefaultValue As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.txbInput.Focus()
        Me.strDefault = strDefaultValue
        Me.Text = strTitle
        Me.lblMessage.Text = strMessage
        Me.txbInput.Text = strDefaultValue

    End Sub

    Private Sub btnOK_Click(sender As System.Object, e As System.EventArgs) Handles btnOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
End Class

Public Class inputDialogResult

    'Shared and empty dialog result
    Public Shared ReadOnly Empty As inputDialogResult = New inputDialogResult(Windows.Forms.DialogResult.Cancel, "")

    Public ReadOnly DialogResult As DialogResult
    Public ReadOnly strResult As String

    Sub New(ByVal dialogResult As DialogResult, ByVal strResult As String)
        Me.DialogResult = dialogResult
        Me.strResult = strResult
    End Sub
End Class