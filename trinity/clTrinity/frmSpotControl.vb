Public Class frmSpotControl

    Private Sub frmSpotControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        grdSpots.Rows.Clear()
        For Each TmpSpot As Trinity.cActualSpot In Campaign.ActualSpots
            If TmpSpot.SpotControlRemark <> "" Then
                With grdSpots.Rows(grdSpots.Rows.Add)
                    .Tag = TmpSpot
                End With
            End If
        Next
        With DirectCast(grdSpots.Columns("colStatus").CellTemplate, Windows.Forms.DataGridViewComboBoxCell)
            .Items.Clear()
            .Items.Add("Remind me later")
            .Items.Add("OK")
        End With
    End Sub

    Private Sub grdSpots_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSpots.CellValueNeeded
        Dim TmpSpot As Trinity.cActualSpot = grdSpots.Rows(e.RowIndex).Tag
        If TmpSpot Is Nothing Then Exit Sub
        Select Case grdSpots.Columns(e.ColumnIndex).Name
            Case "colDate"
                e.Value = Format(Date.FromOADate(TmpSpot.AirDate), "yyyy-MM-dd")
            Case "colTime"
                e.Value = Trinity.Helper.Mam2Tid(TmpSpot.MaM)
            Case "colChannel"
                e.Value = TmpSpot.Channel.Shortname
            Case "colProg"
                e.Value = TmpSpot.Programme
            Case "colFilmcode"
                e.Value = TmpSpot.Filmcode
            Case "colActual"
                e.Value = Format(TmpSpot.Rating, "N1")
            Case "colRemark"
                e.Value = TmpSpot.SpotControlRemark
            Case "colStatus"
                e.Value = DirectCast(grdSpots.Rows(e.RowIndex).Cells(e.ColumnIndex), Windows.Forms.DataGridViewComboBoxCell).Items.Item(TmpSpot.SpotControlStatus - 1)
        End Select
    End Sub

    Private Sub grdSpots_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSpots.CellValuePushed
        Dim TmpSpot As Trinity.cActualSpot = grdSpots.Rows(e.RowIndex).Tag
        If TmpSpot Is Nothing Then Exit Sub
        Select Case grdSpots.Columns(e.ColumnIndex).Name
            Case "colStatus"
                TmpSpot.SpotControlStatus = DirectCast(grdSpots.Rows(e.RowIndex).Cells(e.ColumnIndex), Windows.Forms.DataGridViewComboBoxCell).Items.IndexOf(e.Value) + 1
        End Select
    End Sub
End Class