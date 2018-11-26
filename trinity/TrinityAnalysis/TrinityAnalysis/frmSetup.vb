Public Class frmSetup


    Dim _analysis As New cAnalysis
    Dim _db As New cDBReaderSQL
    Dim _values As New cSearchValues
    Dim _c As New cCriteria



    Private _activeAnalysis As TrinityAnalysis.cAnalysis
    Private ReadOnly Property ActiveAnalysis() As TrinityAnalysis.cAnalysis
        Get
            If _activeAnalysis Is Nothing Then
                _activeAnalysis = Analysis
            End If
            Return _activeAnalysis
        End Get
    End Property

    Private Sub cmdCreateAnalysis_Click(sender As System.Object, e As System.EventArgs) Handles cmdCreateAnalysis.Click

    End Sub


    Private Sub cmdPeriod_Click(sender As System.Object, e As System.EventArgs) Handles cmdPeriod.Click
        Dim _period As New frmPeriod
        _period.ShowDialog()
    End Sub

    Private Sub cmdAddValue_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddValue.Click

        grdSearchValues.Rows.Add()
        DirectCast(grdSearchValues.Rows(grdSearchValues.Rows.Count - 1).Cells(1), ExtendedComboBoxCell).Items.clear()

        For Each p As System.Reflection.PropertyInfo In _values.GetType().GetProperties()
            DirectCast(grdSearchValues.Rows(grdSearchValues.Rows.Count - 1).Cells(0), ExtendedComboBoxCell).Items.add(p.Name)
        Next

    End Sub

    Private Sub cmdDeleteValue_Click(sender As System.Object, e As System.EventArgs) Handles cmdDeleteValue.Click

        For Each tmpRow As Windows.Forms.DataGridViewRow In grdSearchValues.SelectedRows
            grdSearchValues.Rows.Remove(grdSearchValues.Rows(tmpRow.Index))
        Next

    End Sub

    Private Sub grdSearchValues_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSearchValues.CellValueChanged


        If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then
            Exit Sub
        End If


        If e.ColumnIndex = 0 Then
            Dim tmpCell As ExtendedComboBoxCell = grdSearchValues.Rows(e.RowIndex).Cells(e.ColumnIndex)
            For Each row As DataGridViewRow In grdSearchValues.SelectedRows


                DirectCast(grdSearchValues.Rows(grdSearchValues.Rows.Count - 1).Cells(1), ExtendedComboBoxCell).Items.clear()

                If grdSearchValues.Rows.Count >= 1 AndAlso grdSearchValues(e.ColumnIndex, e.RowIndex).Value = "Channel" Then
                    For Each p As System.Reflection.PropertyInfo In _c.GetType().GetProperties()
                    Next
                ElseIf (grdSearchValues.Rows.Count >= 1 AndAlso grdSearchValues(e.ColumnIndex, e.RowIndex).Value = "TotalBudgetCTC") Then
                    For Each p As System.Reflection.PropertyInfo In _c.GetType().GetProperties()
                        DirectCast(grdSearchValues.Rows(grdSearchValues.Rows.Count - 1).Cells(1), ExtendedComboBoxCell).Items.add(p.Name)
                    Next
                ElseIf (grdSearchValues.Rows.Count >= 1 AndAlso grdSearchValues(e.ColumnIndex, e.RowIndex).Value = "Client") Then
                    PopulateClientList()
                ElseIf (grdSearchValues.Rows.Count >= 1 AndAlso grdSearchValues(e.ColumnIndex, e.RowIndex).Value = "Compensation") Then
                    For Each p As System.Reflection.PropertyInfo In _c.GetType().GetProperties()
                        DirectCast(grdSearchValues.Rows(grdSearchValues.Rows.Count - 1).Cells(1), ExtendedComboBoxCell).Items.add(p.Name)
                    Next
                End If
            Next
        End If
    End Sub


    Sub PopulateClientList()

        Dim clients As DataTable = _db.getAllClients()
        For Each dr As DataRow In clients.Rows
            Dim s As String = dr.Item("name")
            DirectCast(grdSearchValues.Rows(grdSearchValues.Rows.Count - 1).Cells(1), ExtendedComboBoxCell).Items.add(s)
        Next
    End Sub

    Public Sub cmdAddChannel_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddChannel.Click


        grdChannels.Rows.Add()

        For Each TmpChan As TrinityAnalysis.cChannel In ActiveAnalysis.Channels

            DirectCast(grdChannels.Rows(grdChannels.Rows.Count - 1).Cells(0), ExtendedComboBoxCell).Items.add(TmpChan)
        Next

    End Sub

    Public Sub importChannels()

        Dim dialog As New OpenFileDialog
        dialog.ShowDialog()

        Dim xml As String = dialog.FileName

        _analysis.CreateChannels(xml)


        grdChannels.Rows.Add()

    End Sub

End Class
