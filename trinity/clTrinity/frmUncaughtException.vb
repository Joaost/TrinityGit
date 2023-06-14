Public Class frmUncaughtException

    Private _exception As Exception

    Private Sub cmdContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdContinue.Click
        My.Computer.Clipboard.SetText(_exception.StackTrace)
        Me.DialogResult = vbCancel
    End Sub

    Private Sub cmdQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuit.Click
        Me.DialogResult = vbOK
    End Sub

    Private Sub cmdSendErrorReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSendErrorReport.Click
        My.Computer.Clipboard.SetText(_exception.StackTrace)
        frmErrorReport.Show(_exception)
    End Sub

    Public Sub New(ByVal e As Exception)

        _exception = e

        ' This call is required by the designer.
        InitializeComponent()

        lblError.Text = e.Message
        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class