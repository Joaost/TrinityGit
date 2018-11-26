Public Class frmSelectContract

    Private Sub frmSelectContract_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

    End Sub

    Private Sub grdContracts_CellContentDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdContracts.CellContentDoubleClick
        cmdOpen_Click(New Object, New EventArgs)
    End Sub

    Private Sub grdContracts_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdContracts.CellClick
        If e.RowIndex < 0 Then Exit Sub
        If Not grdContracts.Columns(e.ColumnIndex).HeaderText = "Delete" Then
            grdContracts.Rows(e.RowIndex).Selected = True
        Else
            If Windows.Forms.MessageBox.Show("Are you sure you want to delete this contract?", "Delete contract?", Windows.Forms.MessageBoxButtons.OKCancel, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
                DBReader.setContractAsDeleted(grdContracts.SelectedRows(0).Tag!id)
                populateContract()

            End If
        End If
    End Sub

    Private Sub grdContracts_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdContracts.CellValueNeeded
        Select Case grdContracts.Columns(e.ColumnIndex).HeaderText
            Case Is = "Name"
                e.Value = grdContracts.Rows(e.RowIndex).Tag!name
            Case Is = "Start"
                e.Value = grdContracts.Rows(e.RowIndex).Tag!startdate
            Case Is = "End"
                e.Value = grdContracts.Rows(e.RowIndex).Tag!enddate
            Case Is = "Last saved"
                e.Value = grdContracts.Rows(e.RowIndex).Tag!lastsavedon
            Case Is = "Version"
                e.Value = grdContracts.Rows(e.RowIndex).Tag!version
            Case Is = "Delete"
                'e.Value = grdContracts.Rows(e.RowIndex).Tag!delete
            Case Else
                e.Value = ""
        End Select
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        If grdContracts.SelectedRows.Count = 0 Then Exit Sub
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub grdContracts_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdContracts.CellContentClick

    End Sub

    Private Sub cmdImport_Click(sender As System.Object, e As System.EventArgs) Handles cmdImport.Click

        Dim _contract As New Trinity.cContract(Campaign)
        

    End Sub
    Private Sub populateContract(Optional byval searchCriteria As String = "")

        grdContracts.Rows.Clear()

        For Each row As DataRow In DBReader.getContracts.Rows
            Dim values As DataRow = row
            If values.Item(1).ToString().ToUpper().Contains(searchCriteria.ToUpper)
                Dim newRowNumber As Integer = grdContracts.Rows.Add()
                grdContracts.Rows(newRowNumber).Tag = row
            End If

        Next

    End Sub

    Private Sub txtSearchBox_TextChanged(sender As Object, e As EventArgs) Handles txtSearchBox.TextChanged
        populateContract(txtSearchBox.Text)
    End Sub

    'Added a textfield in the searcharea to make it more user friendly
    'joos
    Private Sub frmSelectContract_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtSearchBox.Text = "Search..."
        populateContract()
    End Sub
    Private Sub btnCopyPasteContract_Click(sender As Object, e As EventArgs) Handles btnCopyPasteContract.Click

        'Get the ID from the chosen contract 
        Dim chosenContractID = grdContracts.SelectedRows(0).Tag!id

        Dim XMLContractString = DBReader.getContract(chosenContractID).OuterXml.ToString()
        Dim xmlDoc As New XmlDocument

        xmlDoc.LoadXml(XMLContractString)

        Dim XMLContract As Xml.XmlElement = xmlDoc.GetElementsByTagName("Contract").Item(0)

        Dim newNameFrm As New frmNewContractName
        newNameFrm.txtNewContractName.Text = XMLContract.GetAttribute("Name")
        newNameFrm.ShowDialog()

        If newNameFrm.DialogResult = Windows.Forms.DialogResult.OK Then

            Dim _contract As New Trinity.cContract(Campaign)

            _contract.Load("", True, XMLContractString)

            _contract.Name = newNameFrm.txtNewContractName.Text
            'xmlDoc. = newNameFrm.txtNewContractName.Text

            XMLContract.SetAttribute("Name", newNameFrm.txtNewContractName.Text)

            _contract.MainObject.ID = 0
            _contract.MainObject.ContractID = 0

            '_contract.Name = newNameFrm.txtNewContractName.Text
            If DBReader.saveContract(_contract, xmlDoc) Then
                Windows.Forms.MessageBox.Show("New contract with name: " & _contract.Name & " has been saved to the database", "New conctract saved", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            End If
        End If
    End Sub
    Private Sub txtSearchBox_Leave(sender As Object, e As EventArgs) Handles txtSearchBox.Leave
        If txtSearchBox.Text = "" Then
            txtSearchBox.Text = "Search..."
        End If
    End Sub
    Private Sub txtSearchBox_Enter(sender As Object, e As EventArgs) Handles txtSearchBox.Enter
        If txtSearchBox.Text = "Search..." Then
            txtSearchBox.Text = ""
        Else

        End If
    End Sub
End Class