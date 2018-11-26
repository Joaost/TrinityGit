Public Class frmDetails

    Private Sub grdDetails_CellStyleContentChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellStyleContentChangedEventArgs) Handles grdDetails.CellStyleContentChanged
        e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor
    End Sub

    Private Sub frmDetails_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Height = grdDetails.Rows(0).Height * grdDetails.Rows.Count + grdDetails.Top * 2 + 30
    End Sub

    Private Sub cmdExcel_Click(sender As System.Object, e As System.EventArgs) Handles cmdExcel.Click

    End Sub
End Class