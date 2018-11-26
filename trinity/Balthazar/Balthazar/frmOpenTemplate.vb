Public Class frmOpenTemplate

    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        If lstQuestionaires.SelectedItem Is Nothing Then
            Windows.Forms.MessageBox.Show("You need to choose a template.", "BALTHAZAR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

End Class