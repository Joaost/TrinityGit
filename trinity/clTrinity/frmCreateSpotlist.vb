Public Class frmCreateSpotlist

    Private Sub frmCreateSpotlist_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim TmpRow As Windows.Forms.DataGridViewRow
        Dim TmpCol As Windows.Forms.DataGridViewColumn

        DataGridView1.Columns.Clear()
        DataGridView1.Rows.Clear()

        For Each TmpRow In frmSpots.grdConfirmed.Rows



        Next

    End Sub
End Class