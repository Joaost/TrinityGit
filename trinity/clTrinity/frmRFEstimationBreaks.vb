Public Class frmRFEstimationBreaks

    Dim _refPeriod As Trinity.cReferencePeriod

    Dim _solus As New Dictionary(Of String, Dictionary(Of Integer, Single))

    Private Sub frmRFEstimationBreaks_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For i As Integer = 1 To 5
            With grdBreaks.Columns(grdBreaks.Columns.Add("colSolus" & i, "Solus " & i & "+"))
                .Width = 50
            End With
        Next
        grdBreaks.Columns("colDate").Width = 70
        grdBreaks.Columns("colTime").Width = 45
        grdBreaks.Columns("colTRP").Width = 45

        grdBreaks.Rows.Add(_refPeriod.Spots.Count)
    End Sub

    Private Sub grdBreaks_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdBreaks.CellValueNeeded
        Dim _spot As Trinity.cSpot = _refPeriod.Spots(e.RowIndex + 1)

        If Not _solus.ContainsKey(_spot.ID) Then
            _solus.Add(_spot.ID, New Dictionary(Of Integer, Single))
            Dim ID As String
            ID = _spot.ID
            For f As Integer = 1 To 5
                Dim ReachBefore As Single
                Dim ReachAfter As Single

                _refPeriod.Calculate()
                ReachBefore = _refPeriod.Reach(f)
                _refPeriod.Spots.Remove(ID)
                _refPeriod.Calculate()
                ReachAfter = _refPeriod.Reach(f)
                _refPeriod.Spots.Add(_spot.AirDate, _spot.MaM, _spot.Channel, _spot.ID)
                _refPeriod.Calculate()
                _solus(ID).Add(f, ReachBefore - ReachAfter)
            Next
        End If

        Select Case grdBreaks.Columns(e.ColumnIndex).Name
            Case "colDate"
                e.Value = Format(_spot.AirDate, "Short date")
            Case "colTime"
                e.Value = Trinity.Helper.Mam2Tid(_spot.MaM)
            Case "colTRP"
                e.Value = Format(_spot.TRP, "N1")
            Case "colProgramme"
                e.Value = _spot.Programme
            Case Else
                Dim f As Integer = e.ColumnIndex - 3
                e.Value = Format(_solus(_spot.ID)(f), "N1")
        End Select
    End Sub

    Public Sub New(ByVal ReferencePeriod As Trinity.cReferencePeriod)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        _refPeriod = ReferencePeriod

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class