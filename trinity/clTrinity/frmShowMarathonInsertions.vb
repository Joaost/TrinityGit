Public Class frmShowMarathonInsertions

    Private Sub frmShowMarathonInsertions_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        grdInsertions.Rows.Clear()

        Dim orderNumber As New Dictionary(Of String, Integer)
        '    Dim orderNumber As New Dictionadry(Of String, Integer)

        For Each order As XmlElement In Campaign.MarathonInsertions

            If orderNumber.ContainsKey(order.GetAttribute("OrderNumber")) Then
                grdInsertions.Rows(CInt(orderNumber(order.GetAttribute("OrderNumber")))).Cells(3).Value += CDbl(order.GetAttribute("Net"))
            Else
                grdInsertions.Rows.Add()
                orderNumber.Add(order.GetAttribute("OrderNumber"), grdInsertions.Rows.Count - 1)
                grdInsertions.Rows(grdInsertions.Rows.Count - 1).Cells(0).Value = order.GetAttribute("OrderNumber")
                grdInsertions.Rows(grdInsertions.Rows.Count - 1).Cells(1).Value = order.GetAttribute("Channel")
                grdInsertions.Rows(grdInsertions.Rows.Count - 1).Cells(2).Value = order.GetAttribute("BookingType")
                grdInsertions.Rows(grdInsertions.Rows.Count - 1).Cells(3).Value = order.GetAttribute("Net")
            End If
        Next
        If grdInsertions.RowCount = 0 Then
            Windows.Forms.MessageBox.Show("Nothing has been exported to Marathon.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            Me.Close()
        End If
    End Sub

    Private Sub frmShowMarathonInsertions_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        grdInsertions.Anchor = Windows.Forms.AnchorStyles.None
        grdInsertions.Anchor = Windows.Forms.AnchorStyles.Left
        grdInsertions.Anchor = Windows.Forms.AnchorStyles.Top
        grdInsertions.Size = grdInsertions.PreferredSize
        grdInsertions.Height = grdInsertions.GetRowDisplayRectangle(grdInsertions.Rows.Count - 1, False).Bottom + 1
        grdInsertions.Width = grdInsertions.GetColumnDisplayRectangle(grdInsertions.Columns.Count - 1, False).Right + 1
        Me.Height = grdInsertions.Height '+ StatusStrip1.Height + 27
        Me.Width = grdInsertions.Width
    End Sub
End Class