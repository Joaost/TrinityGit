Imports System.Windows.Forms

Public Class frmEnhancements

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdAddIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddIndex.Click
        With grdEnhancements.Rows(grdEnhancements.Rows.Add())
            .Cells(1).Value = 0
            .Cells(2).Value = True
        End With
    End Sub

    Private Sub cmdRemoveIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveIndex.Click
        grdEnhancements.Rows.Remove(grdEnhancements.SelectedRows.Item(0))
    End Sub

    Private Sub grdEnhancements_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdEnhancements.CellContentClick

    End Sub

    Private Sub grdEnhancements_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdEnhancements.KeyUp
        If e.Control AndAlso e.KeyCode = Keys.V Then
            Try
                Dim _rows() As String = Clipboard.GetText.Split(vbCrLf)
                For Each _row As String In _rows
                    Dim _cols() As String = _row.Split(vbTab)
                    If _cols(0) <> "Name" Then
                        'Stop
                        With grdEnhancements.Rows(grdEnhancements.Rows.Add())
                            .Cells(0).Value = _cols(0).Trim(Chr(10))
                            .Cells(1).Value = _cols(1).Trim(Chr(10))
                            .Cells(2).Value = True
                        End With
                    End If
                Next
            Catch ex As Exception
                Windows.Forms.MessageBox.Show("Error:" & vbCrLf & vbCrLf & ex.Message, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
End Class
