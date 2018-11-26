Public Class frmOpen

    Private Sub frmOpen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lstEvents.Items.Clear()
        lstEvents.DisplayMember = "Name"
        For Each TmpEvent As cDBReader.EventStruct In Database.Events
            lstEvents.Items.Add(TmpEvent)
        Next
        lstEvents.DisplayMember = "Name"
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        If lstEvents.SelectedItem Is Nothing Then Exit Sub
        MyEvent = Database.GetEvent(lstEvents.SelectedItem.ID)
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class