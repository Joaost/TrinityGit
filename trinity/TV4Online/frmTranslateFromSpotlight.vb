Imports System.ServiceModel
Imports System.Windows.Forms

Public Class frmTranslateFromSpotlight

    Private _trinityValues As List(Of String)
    Private _spotlightValues As List(Of String)

    Private Sub frmTranslateFromSpotlight_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        For Each _row As System.Windows.Forms.DataGridViewRow In grdTranslate.Rows
            TV4OnlinePlugin.InternalApplication.SetUserPreference("SpotlightTrinity", _row.Cells(0).Value, _row.Cells(1).Value)
        Next
    End Sub

    Public Sub New(SpValues As List(Of String), TrinityValues As List(Of String))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _trinityValues = TrinityValues
        _spotlightValues = SpValues

        With DirectCast(colTrinity, DataGridViewComboBoxColumn)
            .Items.AddRange(_trinityValues.ToArray())
        End With

        grdTranslate.Rows.Add(_spotlightValues.Count)


    End Sub

    Private Sub grdTranslate_CellErrorTextNeeded(sender As Object, e As System.Windows.Forms.DataGridViewCellErrorTextNeededEventArgs) Handles grdTranslate.CellErrorTextNeeded

    End Sub

    Private Sub grdTranslate_CellValueNeeded(sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdTranslate.CellValueNeeded
        Select Case e.ColumnIndex
            Case colSpotlight.Index
                e.Value = _spotlightValues(e.RowIndex)
            Case colTrinity.Index
                e.Value = grdTranslate.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag                
        End Select
    End Sub

    Private Sub grdTranslate_CellValuePushed(sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdTranslate.CellValuePushed
        If e.ColumnIndex = colTrinity.Index Then
            grdTranslate.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = e.Value
        End If
    End Sub

    Private Sub cmdOk_Click(sender As System.Object, e As System.EventArgs) Handles cmdOk.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class