Public Class frmEditClients

    'List of restricted clients from DB
    Dim _restrictedClients As DataTable
    Dim _clients As DataTable = DBReader.getAllClients()

    'Public Sub populateClients()
    '    PopulateClientList()

    '    'Sets up the table
    '    Using _clients As DataTable = New DataTable
    '        Using _conn As New SqlClient.SqlConnection(_connectionString)
    '            _conn.Open()
    '            Using com As New SqlClient.SqlCommand("SELECT * FROM Clients", _conn)
    '                Using rd As SqlClient.SqlDataReader = com.ExecuteReader
    '                    _clients.Load(rd)
    '                    rd.Close()
    '                    _conn.Close()
    '                    Return _clients
    '                End Using
    '            End Using
    '        End Using
    '    End Using
    'End Sub
    Public Sub PopulateClientList()
        grdClients.Rows.Clear()

        _restrictedClients = DBReader.getAllClients()

        grdClients.Rows.Add(_restrictedClients.Rows)

    End Sub
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        'tmrKeypress.Enabled = False
        'tmrKeypress.Enabled = True

    End Sub
    Private Sub TmrKeypress_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrKeypress.Tick
        'If txtSearch.TextLength <> 0 Then
        '    PopulateClientList()
        '    tmrKeypress.Enabled = False
        '    Exit Sub
        'Else
        '    PopulateClientList()
        'End If
        'tmrKeypress.Enabled = False
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        'txtSearch.TabStop = True
        'txtSearch.TabIndex = 1
        'txtSearch.Focus()
        'Me.DialogResult = Windows.Forms.DialogResult.Cancel
        'Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        'list of Rejected clients'
        'Dim _list As New List(Of String)
        'For Each _row As Windows.Forms.DataGridView In grdClients.Rows
        '    ' If _row.Visible = False AndAlso _restrictedClients(_row.Index).id > 0 Then
        '    '''''''Remove Restricteed Client
        '    'ElseIf _row.Visible Then
        '    '''''''_list.Add(_restrictedClient(_row.Index).id)
        '    ' End If
        'Next
        '' DBReader.addClients(_list)
        'Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
    Private Sub getAllClients()
        Dim clients As DataTable = DBReader.getAllClients()
        For Each dr As DataRow In clients.Rows
            Dim TmpItem As New CBItem
            TmpItem.Text = dr.Item("name") 'rd!name
            TmpItem.Tag = dr.Item("id") 'rd!id
            ' Added by JOKO
            ' Important contraint since Norway dont have that value and will then return null and it will break down.
            If TrinitySettings.DefaultArea <> "NO" Then
                If Not IsDBNull(dr.Item("restricted")) Then
                    TmpItem.restricted = dr.Item("restricted") 'rd!Restricted 
                End If
            End If
            grdClients.Rows.Add(TmpItem)
            'cmbClient.Items.Add(TmpItem)
        Next
    End Sub
    Private Sub frmClients_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        '_clients = DBReader.getAllClients
        'grdClients.Rows.Clear()

        'For Each dr As DataRow In _clients.Rows
        '    Dim tmpItem As New CBItem
        '    tmpItem.Text = dr.Item("Name") 'rd!name
        '    tmpItem.Tag = dr.Item("Id") 'dr!Id
        '    If Not IsDBNull(dr.Item("restricted")) Then
        '        tmpItem.restricted = dr.Item("restricted") 'rd!restricted
        '    End If
        '    grdClients.Rows.Add(tmpItem)
        '    If tmpItem.Tag = Campaign.ClientID Then
        '        ''
        '    End If

        'Next
    End Sub

    Private Class CBItem

        Private _restricted As Boolean
        Private _text As String
        Public Property Text() As String
            Get
                Return _text
            End Get
            Set(ByVal value As String)
                _text = value
            End Set
        End Property
        Public Property restricted() As Boolean
            Get
                Return _restricted
            End Get
            Set(ByVal value As Boolean)
                _restricted = restricted
            End Set
        End Property

        Private _tag As Object
        Public Property Tag() As Object
            Get
                Return _tag
            End Get
            Set(ByVal value As Object)
                _tag = value
            End Set
        End Property
    End Class

    Private Sub grdClients_CellValueNeeded(sender As Object, e As Windows.Forms.DataGridViewCellValueEventArgs) Handles grdClients.CellValueNeeded

        getAllClients()
    End Sub

    Private Sub grdClients_CellValuePushed(sender As Object, e As Windows.Forms.DataGridViewCellValueEventArgs) Handles grdClients.CellValuePushed
        getAllClients()
    End Sub
End Class