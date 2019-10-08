Public Class frmClientResctriction

    Public Sub populateCLients()
        Try
            Dim clients As DataTable = DBReader.getAllClients()
            'cmbClient.Items.Clear()
            'cmbClient.DisplayMember = "Text"
            For Each dr As DataRow In clients.Rows
                'Dim TmpItem As New CBItem
                'TmpItem.Text = dr.Item("name") 'rd!name
                'TmpItem.Tag = dr.Item("id") 'rd!id
                'cmbClient.Items.Add(TmpItem)
                'If TmpItem.Tag = Campaign.ClientID Then
                'cmbClient.Text = TmpItem.Text
                End If
            Next
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("There was an error while populating the Client dropdown list:" & vbCrLf & vbCrLf & "'" & ex.Message & "'")
        End Try
    End Sub

End Class