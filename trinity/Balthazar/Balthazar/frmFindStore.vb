Imports System.Windows.Forms

Public Class frmFindStore

    Dim Stores As Dictionary(Of Integer, cStore)

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If grdStores.SelectedRows.Count > 1 Then
            Windows.Forms.MessageBox.Show("You can only select one store.", "Baltazar", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        ElseIf grdStores.SelectedRows.Count = 0 Then
            Windows.Forms.MessageBox.Show("You must select a store.", "Baltazar", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Sub UpdateList()
        Stores = Database.GetStores.ToDictionary(Function(s) s.DatabaseID)
        grdStores.Rows.Clear()
        For Each TmpStore As cStore In From Store As cStore In Stores.Values Select Store Where SearchFilter(Store, txtSearch.Text)
            With grdStores.Rows(grdStores.Rows.Add)
                .Tag = TmpStore.DatabaseID
            End With
        Next
    End Sub

    Private Sub frmFindStore_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UpdateList()
    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        UpdateList()
    End Sub

    Private Function SearchFilter(ByVal Store As cStore, ByVal SearchString As String) As Boolean
        If SearchString = "" Then Return True
        For Each TmpString As String In SearchString.ToUpper.Split(",")
            Dim WordExists As Boolean = False
            If Store.Address.ToUpper.Contains(TmpString.Trim) Then
                WordExists = True
            ElseIf Store.City.ToUpper.Contains(TmpString.Trim) Then
                WordExists = True
            ElseIf Store.Name.ToUpper.Contains(TmpString.Trim) Then
                WordExists = True
            End If
            If Not WordExists Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub grdStores_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdStores.CellValueNeeded
        Dim TmpStore As cStore = Stores(grdStores.Rows(e.RowIndex).Tag)
        Select Case grdStores.Columns(e.ColumnIndex).HeaderText
            Case "Store"
                e.Value = TmpStore.Name
            Case "City"
                e.Value = TmpStore.City
            Case "Address"
                e.Value = TmpStore.Address
        End Select
    End Sub

    Private Sub grdStores_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdStores.CellValuePushed

    End Sub

    Private Sub cmdDeleteStore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteStore.Click
        If Windows.Forms.MessageBox.Show("This will permanently delete all selected stores from the database." & vbCrLf & vbCrLf & "Are you sure you want to proceed?", "Balthazar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            For Each TmpRow As DataGridViewRow In grdStores.Rows
                Database.DeleteStore(TmpRow.Tag)
                grdStores.Rows.Remove(TmpRow)
            Next
        End If
    End Sub
End Class
