Public Class frmEditClients

    'List of restricted clients from DB
    Dim _restrictedClients As DataTable
    Public Sub populateClients()
        'Sets up the table
        'Using _clients As DataTable = New DataTable
        '    Using _conn As New SqlClient.SqlConnection(_connectionString)
        '        _conn.Open()
        '        Using com As New SqlClient.SqlCommand("SELECT * FROM Clients", _conn)
        '            Using rd As SqlClient.SqlDataReader = com.ExecuteReader
        '                _clients.Load(rd)
        '                rd.Close()
        '                _conn.Close()
        '                Return _clients
        '            End Using
        '        End Using
        '    End Using
        'End Using
    End Sub
    Public Sub PopulateClientList()
        _restrictedClients = DBReader.getAllClients()
        grdClients.Rows.Clear()
        grdClients.Rows.Add(_restrictedClients.Rows)
    End Sub
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        tmrKeypress.Enabled = False
        tmrKeypress.Enabled = True

    End Sub
    Private Sub TmrKeypress_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrKeypress.Tick
        If txtSearch.TextLength <> 0 Then
            PopulateClientList()
            tmrKeypress.Enabled = False
            Exit Sub
        Else
            PopulateClientList()
        End If
        tmrKeypress.Enabled = False
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        txtSearch.TabStop = True
        txtSearch.TabIndex = 1
        txtSearch.Focus()
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        'list of Rejected clients'
        Dim _list As New List(Of String)
        For Each _row As Windows.Forms.DataGridView In grdClients.Rows
            ' If _row.Visible = False AndAlso _restrictedClients(_row.Index).id > 0 Then
            '''''''Remove Restricteed Client
            'ElseIf _row.Visible Then
            '''''''_list.Add(_restrictedClient(_row.Index).id)
            ' End If
        Next
        ' DBReader.addClients(_list)
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class