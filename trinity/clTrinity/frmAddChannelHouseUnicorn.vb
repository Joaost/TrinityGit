Public Class frmAddChannelHouseUnicorn
    Private Sub cmdOk_Click(sender As Object, e As EventArgs) Handles cmdOk.Click
        If txtInputChannelName.Text = "" Then

            If Windows.Forms.MessageBox.Show("Channel house name must have a name. Please type in a name.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Warning) = Windows.Forms.DialogResult.OK Then
                Me.Show()
            End If
        Else
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub
End Class