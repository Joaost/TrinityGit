Public Class frmNew

    Private Sub frmNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lvwEvents.Groups("grpTemplates").Items.Clear()
        For Each TmpEvent As cDBReader.EventStruct In Database.EventTemplates
            Dim TmpItem As New ListViewItem
            TmpItem.Tag = TmpEvent
            TmpItem.Text = TmpEvent.Name
            TmpItem.ImageKey = "template"
            lvwEvents.Items.Add(TmpItem)
            TmpItem.Group = lvwEvents.Groups("grpTemplates")
        Next
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub lvwEvents_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvwEvents.DoubleClick
        cmdOk_Click(sender, e)
    End Sub

End Class