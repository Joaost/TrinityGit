Public Class frmEditClients


    Public Sub populateClites()
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
End Class