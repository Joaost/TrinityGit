Public Class frmFile

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        Dim dlgFile As New OpenFileDialog
        dlgFile.ShowDialog()
        lblFile.Text = dlgFile.FileName
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Tag = lblFile.Text
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub rdb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbProg.CheckedChanged, rdbNone.CheckedChanged, rdbSetup.CheckedChanged, rdbVersion.CheckedChanged
        If rdbProg.Checked Then
            chkNew.Enabled = False
            chkOverwrite.Checked = True
            chkOverwrite.Enabled = False
        ElseIf rdbNone.Checked Then
            chkNew.Enabled = True
            chkOverwrite.Enabled = True
        ElseIf rdbVersion.Checked Then
            chkNew.Enabled = False
            chkOverwrite.Checked = True
            chkOverwrite.Enabled = False
        ElseIf rdbSetup.Checked Then
            chkNew.Enabled = False
            chkOverwrite.Checked = True
            chkOverwrite.Enabled = False
        End If
    End Sub
End Class