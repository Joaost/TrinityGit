Public Class frmEditClients

    'List of restricted clients from DB
    Dim _restrictedClientsDBTable As DataTable
    Dim clientList As New List(Of Client)


    Public Sub PopulateClientList()
        grdClients.Rows.Clear()

        _restrictedClientsDBTable = DBReader.getAllClients()

    End Sub
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        getAllClients()
        fillGrid()

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
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        For Each cl As Client In clientList
            Dim tempRestricted As Integer = 0
            If cl.restricted Then
                tempRestricted = 1
            End If
            DBReader.updateClient(cl.name, cl.id, tempRestricted)
        Next
        'Saving the entire list of clients with their properties
        'Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
    Private Sub getAllClients()
        PopulateClientList()
        If grdClients.Rows.Count < 1 Then
            For Each dr As DataRow In _restrictedClientsDBTable.Rows
                Dim TmpItem As New Client
                TmpItem.name = dr.Item("name") 'rd!name
                TmpItem.id = dr.Item("id") 'rd!id
                ' Added by JOKO
                ' Important contraint since Norway dont have that value and will then return null and it will break down.
                If TrinitySettings.DefaultArea <> "NO" Then
                    If Not IsDBNull(dr.Item("restricted")) Then
                        TmpItem.restricted = dr.Item("restricted") 'rd!Restricted 
                    End If
                End If
                clientList.Add(TmpItem)
                'grdClients.Rows.Add(TmpItem)
                'cmbClient.Items.Add(TmpItem)
            Next
        End If
    End Sub
    Private Sub frmClients_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
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
        If clientList.Count > 0 Then
            Dim x As Client = clientList(e.RowIndex)
            If x Is Nothing Then Exit Sub
            Select Case e.ColumnIndex
                Case colId.Index
                    e.Value = x.id
                Case colName.Index
                    e.Value = x.name
                Case colRestriction.Index
                    e.Value = x.restricted
            End Select
        End If

    End Sub

    Sub fillGrid()
        If grdClients.RowCount < 1 Then
            grdClients.Rows.Clear()
            For Each cl As Client In clientList
                Dim newRow As Integer = grdClients.Rows.Add
                grdClients.Rows(newRow).Tag = cl
            Next
        End If
    End Sub

    Private Sub grdClients_CellEnter(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles grdClients.CellEnter
        'If e.ColumnIndex = 2 Then

        '    Dim tmpClient As Client = grdClients.Rows(e.RowIndex).Tag
        '    For Each cl As Client In clientList
        '        If cl.id = tmpClient.id Then
        '            Dim Checked As Boolean = CType(grdClients.Rows(e.RowIndex).Cells(e.ColumnIndex).Value, Boolean)
        '            If Checked Then
        '                cl.restricted = True
        '            Else
        '                cl.restricted = False
        '            End If
        '            'cl.restricted = tmpClient.restricted
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub grdClients_CellValueChanged(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles grdClients.CellValueChanged
        'If e.ColumnIndex = 2 Then
        '    If grdClients.Rows.Count > 0 Then
        '        Dim tmpClient As Client = grdClients.Rows(e.RowIndex).Tag
        '        For Each cl As Client In clientList
        '            If cl.id = tmpClient.id Then
        '                cl.restricted = grdClients.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
        '                'cl.restricted = tmpClient.restricted
        '            End If
        '        Next
        '    End If
        'End If
    End Sub

    Private Sub grdClients_CellLeave(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles grdClients.CellLeave
        'If e.ColumnIndex = 2 Then
        '    If grdClients.Rows.Count > 0 Then
        '        Dim tmpClient As Client = grdClients.Rows(e.RowIndex).Tag
        '        For Each cl As Client In clientList
        '            If cl.id = tmpClient.id Then
        '                cl.restricted = e.Value
        '                'cl.restricted = tmpClient.restricted
        '            End If
        '        Next
        '    End If
        'End If
    End Sub

    Private Sub grdClients_CellValuePushed(ByVal sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdClients.CellValuePushed
        If e.ColumnIndex = 2 Then
            If grdClients.Rows.Count > 0 Then
                Dim tmpClient As Client = grdClients.Rows(e.RowIndex).Tag
                For Each cl As Client In clientList
                    If cl.id = tmpClient.id Then
                        cl.restricted = e.Value
                        'cl.restricted = tmpClient.restricted
                    End If
                Next
            End If
        End If
    End Sub
End Class