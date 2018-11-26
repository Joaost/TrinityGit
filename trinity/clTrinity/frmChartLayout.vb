Imports System.Windows.Forms

Public Class frmChartLayout

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmChartLayout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbColorScheme.Items.Clear()
        For i As Integer = 1 To TrinitySettings.ColorSchemeCount
            cmbColorScheme.Items.Add(TrinitySettings.ColorScheme(i))
        Next
        cmbColorScheme.SelectedIndex = TrinitySettings.DefaultColorScheme
    End Sub

    Private Sub grdSeries_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSeries.CellContentClick
        If e.ColumnIndex = 1 Then
            Dim gfx As Bitmap
            Dim clr As Color
            Dim dlgColor As New ColorDialog

            gfx = grdSeries.Rows(e.RowIndex).Cells(1).Value
            clr = gfx.GetPixel(6, 6)
            dlgColor.Color = clr
            If dlgColor.ShowDialog = Windows.Forms.DialogResult.OK Then
                clr = dlgColor.Color
                For x As Integer = 0 To 9
                    For y As Integer = 0 To 9
                        If x = 0 OrElse y = 0 OrElse x = 9 OrElse y = 9 Then
                            gfx.SetPixel(x, y, Color.Black)
                        Else
                            gfx.SetPixel(x, y, clr)
                        End If
                    Next
                Next
                With DirectCast(grdSeries.Rows(e.RowIndex).Cells(1), Windows.Forms.DataGridViewImageCell)
                    .ValueIsIcon = False
                    .Value = gfx
                End With
            End If
        End If
    End Sub

    Private Sub grdSeries_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grdSeries.RowsAdded

    End Sub

    Private Sub cmdApplyScheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApplyScheme.Click
        For i As Integer = 0 To grdSeries.Rows.Count - 1
            Dim gfx As Bitmap
            Dim clr As Color
            gfx = grdSeries.Rows(i).Cells(1).Value
            clr = ConvertIntToARGB(TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Diagram" & i + 1))
            For x As Integer = 0 To 9
                For y As Integer = 0 To 9
                    If x = 0 OrElse y = 0 OrElse x = 9 OrElse y = 9 Then
                        gfx.SetPixel(x, y, Color.Black)
                    Else
                        gfx.SetPixel(x, y, clr)
                    End If
                Next
            Next
            With DirectCast(grdSeries.Rows(i).Cells(1), Windows.Forms.DataGridViewImageCell)
                .ValueIsIcon = False
                .Value = gfx
            End With
        Next
        Me.Tag = TrinitySettings.SchemeFont(cmbColorScheme.SelectedIndex + 1)
        grdSeries.Invalidate()
    End Sub

    Function ConvertIntToARGB(ByVal Int As Integer) As Drawing.Color
        'makes a RGB value out of the windows color type
        Dim HexVal As String = Hex(Int)

        While HexVal.Length < 6
            HexVal = "0" & HexVal
        End While
        Return Drawing.Color.FromArgb(Val("&H" & HexVal.Substring(4, 2)), Val("&H" & HexVal.Substring(2, 2)), Val("&H" & HexVal.Substring(0, 2)))
    End Function

End Class
