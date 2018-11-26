Public Class frmChooseOther

    Public Property Status() As String
        Get
            Return lblStatus.Text
        End Get
        Set(ByVal value As String)
            lblStatus.Text = value
        End Set
    End Property

    Private Sub cmdReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReplace.Click
        Me.DialogResult = Windows.Forms.DialogResult.Retry
    End Sub

    Private Sub cmdRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRename.Click
        Me.DialogResult = Windows.Forms.DialogResult.Ignore
    End Sub

    Private Sub cmdSkip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSkip.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
End Class