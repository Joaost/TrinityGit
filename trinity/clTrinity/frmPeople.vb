Public Class frmPeople

    Dim _people As List(Of Trinity.cPerson)

    Private Sub frmPeople_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        _people = DBReader.getAllPeople
        grdPeople.Rows.Clear()
        grdPeople.Rows.Add(_people.Count)
    End Sub

    Private Sub grdPeople_CellValueNeeded(sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdPeople.CellValueNeeded
        Dim _p As Trinity.cPerson = _people(e.RowIndex)

        Select Case e.ColumnIndex
            Case 0
                e.Value = _p.Name
            Case 1
                e.Value = _p.Phone
            Case 2
                e.Value = _p.Email
            Case 3
                e.Value = _p.statusActive
        End Select
    End Sub

    Private Sub grdPeople_CellValuePushed(sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdPeople.CellValuePushed
        Dim _p As Trinity.cPerson = _people(e.RowIndex)

        Select Case e.ColumnIndex
            Case 0
                _p.Name = e.Value
            Case 1
                _p.Phone = e.Value
            Case 2
                _p.Email = e.Value
            Case 3
                _p.statusActive = e.Value
        End Select
    End Sub

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Dim _list As New List(Of Trinity.cPerson)
        For Each _row As Windows.Forms.DataGridViewRow In grdPeople.Rows
            If _row.Visible = False AndAlso _people(_row.Index).id > 0 Then
                DBReader.removePerson(_people(_row.Index).id)
            ElseIf _row.Visible Then
                _list.Add(_people(_row.Index))
            End If
        Next
        DBReader.addPeople(_list)
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cmdAddPeople_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddPeople.Click
        _people.Add(New Trinity.cPerson)
        grdPeople.Rows.Add()
    End Sub

    Private Sub cmdDeletePeople_Click(sender As System.Object, e As System.EventArgs) Handles cmdDeletePeople.Click
        For Each _row As Windows.Forms.DataGridViewRow In grdPeople.SelectedRows
            _row.Visible = False
        Next
    End Sub
End Class