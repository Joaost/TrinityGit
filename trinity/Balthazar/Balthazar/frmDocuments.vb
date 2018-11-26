Public Class frmDocuments


    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Dim dlgOpen As New Windows.Forms.OpenFileDialog
        dlgOpen.Filter = "All files|*.*"
        If dlgOpen.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim TmpDoc As New cDocument
            TmpDoc.Data = My.Computer.FileSystem.ReadAllBytes(dlgOpen.FileName)
            TmpDoc.DocType = My.Computer.FileSystem.GetFileInfo(dlgOpen.FileName).Extension
            TmpDoc.FileName = My.Computer.FileSystem.GetFileInfo(dlgOpen.FileName).Name
            grdDocuments.Rows(grdDocuments.Rows.Add).Tag = TmpDoc
            MyEvent.Documents.Add(TmpDoc.ID, TmpDoc)
        End If
    End Sub


    Private Sub grdDocuments_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdDocuments.CellValueNeeded
        Dim TmpDoc As cDocument = grdDocuments.Rows(e.RowIndex).Tag
        Select Case e.ColumnIndex
            Case 0
                e.Value = TmpDoc.Name
            Case 1
                e.Value = TmpDoc.Description
            Case 2
                e.Value = TmpDoc.Filename
        End Select
    End Sub

    Private Sub grdDocuments_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdDocuments.CellValuePushed
        Dim TmpDoc As cDocument = grdDocuments.Rows(e.RowIndex).Tag
        Select Case e.ColumnIndex
            Case 0
                TmpDoc.Name = e.Value
            Case 1
                TmpDoc.Description = e.Value
        End Select

    End Sub

    Private Sub cmdView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdView.Click
        Dim TmpDoc As cDocument = grdDocuments.SelectedRows(0).Tag
        Dim Filename As String = My.Computer.FileSystem.GetTempFileName & TmpDoc.DocType
        My.Computer.FileSystem.WriteAllBytes(Filename, TmpDoc.Data, False)
        System.Diagnostics.Process.Start(Filename)
    End Sub

    Private Sub frmDocuments_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave
        frmInStore.cmbDemoInstruction.Items.Clear()
        For Each TmpDoc As cDocument In MyEvent.Documents.Values
            frmInStore.cmbDemoInstruction.Items.Add(TmpDoc)
        Next
        frmInStore.cmbDemoInstruction.SelectedItem = MyEvent.InStore.DemoInstructions
    End Sub

    Private Sub frmDocuments_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        grdDocuments.Rows.Clear()
        For Each TmpDoc As cDocument In MyEvent.Documents.Values
            grdDocuments.Rows(grdDocuments.Rows.Add).Tag = TmpDoc
        Next
    End Sub

    Private Sub cmdReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReplace.Click
        Dim TmpDoc As cDocument = grdDocuments.SelectedRows(0).Tag
        Dim dlgOpen As New Windows.Forms.OpenFileDialog
        dlgOpen.Filter = "All files|*.*"
        If dlgOpen.ShowDialog = Windows.Forms.DialogResult.OK Then
            TmpDoc.Data = My.Computer.FileSystem.ReadAllBytes(dlgOpen.FileName)
            TmpDoc.DocType = My.Computer.FileSystem.GetFileInfo(dlgOpen.FileName).Extension
            TmpDoc.FileName = My.Computer.FileSystem.GetFileInfo(dlgOpen.FileName).Name
        End If
        grdDocuments.Invalidate()
    End Sub

    Private Sub cmdRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
        For Each TmpRow As DataGridViewRow In grdDocuments.SelectedRows
            Dim TmpDoc As cDocument = TmpRow.Tag
            MyEvent.Documents.Remove(TmpDoc.ID)
            grdDocuments.Rows.Remove(TmpRow)
        Next
    End Sub
End Class