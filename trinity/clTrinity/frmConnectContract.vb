Public Class frmConnectContract

    Private Sub CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optImport.CheckedChanged, optConnect.CheckedChanged, optDisconnected.CheckedChanged
        cmbContract.Visible = optConnect.Checked
    End Sub

    Private Sub cmdOk_Click(sender As System.Object, e As System.EventArgs) Handles cmdOk.Click
        If optConnect.Checked Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
        ElseIf optImport.Checked Then
            Me.DialogResult = Windows.Forms.DialogResult.Yes
        Else
            Me.DialogResult = Windows.Forms.DialogResult.No
        End If
    End Sub

    Private Sub frmConnectContract_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        cmbContract.DataSource = Nothing
        cmbContract.Items.Clear()
        cmbContract.DisplayMember = "Name"
        cmbContract.ValueMember = "ID"
        cmbContract.DataSource = DBReader.getContracts()
        For Each _cnt As DataRowView In cmbContract.Items
            If Not IsDBNull(_cnt!originallocation) AndAlso _cnt!originallocation = Campaign.Contract.Path Then
                cmbContract.SelectedItem = _cnt
            End If
        Next
        If cmbContract.SelectedIndex = -1 AndAlso cmbContract.Items.Count > 0 Then cmbContract.SelectedIndex = 0
    End Sub

End Class