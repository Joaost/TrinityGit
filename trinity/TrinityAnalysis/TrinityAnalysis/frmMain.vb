Imports System.Data

Public Class frmMain

    Private _db As New cDBReaderSQL
    Private _xml As New cXml
    Private _piv As New frmPivot
    Private _setup As New frmSetup

    Private Sub CloseApplication()
        For Each Form As Windows.Forms.Form In Me.MdiChildren
            Form.Dispose()
        Next
        Me.Dispose()
        End
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        CloseApplication()
    End Sub

    Private Sub cmdSetup_Click(sender As System.Object, e As System.EventArgs) Handles cmdSetup.Click
        frmSetup.MdiParent = Me
        frmSetup.Show()

    End Sub

    Private Sub cmdPivot_Click(sender As System.Object, e As System.EventArgs) Handles cmdPivot.Click
        frmPivot.MdiParent = Me
        frmPivot.Show()
    End Sub

    Private Sub OpenToolStripButton_Click(sender As System.Object, e As System.EventArgs) Handles OpenToolStripButton.Click

        _setup.importChannels()

    End Sub

    Private Sub OpenAnalysis(ByVal xml As String)
    End Sub
End Class
