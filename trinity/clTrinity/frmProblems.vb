Public Class frmProblems

    Private Delegate Sub ProblemsChanged(ByVal List As List(Of Trinity.cProblem))

    Dim _problemList As List(Of Trinity.cProblem)
    Private Sub frmProblems_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

    End Sub

    Private Sub frmProblems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BindList(Campaign.Problems)
        AddHandler Campaign.AllProblemsFound, AddressOf NewProblemList
    End Sub

    Public Sub NewProblemList(ByVal Problems As List(Of Trinity.cProblem))
        Dim _problems As List(Of Trinity.cProblem) = Problems.Where(Function(p) chkShowAll.Checked OrElse p.IsVisible).ToList
        Dim _problemsNotInOldList As List(Of Trinity.cProblem) = _problems.Where(Function(p) Not _problemList.Contains(p)).ToList
        '_problemsNotInOldList = _problemsNotInOldList.Where(Function(p) chkShowAll.Checked OrElse p.IsVisible).ToList()
        Dim _problemsNotInNewList As List(Of Trinity.cProblem) = _problemList.Where(Function(p) Not _problems.Contains(p)).ToList
        '_problemsNotInNewList = _problemsNotInNewList.Where(Function(p) chkShowAll.Checked OrElse p.IsVisible).ToList()
        If _problemsNotInOldList.Count > 0 OrElse _problemsNotInNewList.Count > 0 Then
            BindList(Problems)
        End If
    End Sub

    Sub BindList(ByVal List As List(Of Trinity.cProblem))
        If Not Me.InvokeRequired Then
            grdProblems.Rows.Clear()
            If List Is Nothing Then Exit Sub
            _problemList = List.ToList.Where(Function(p) chkShowAll.Checked OrElse p.IsVisible).ToList
            If List.Count > _problemList.Count Then
                lblHidden.Text = "There are " & List.Count - _problemList.Count & " hidden problems. Check 'Show all problems' to show them."
            Else
                lblHidden.Text = "There are 0 hidden problems"
            End If
            If grdProblems.ColumnCount > 0 Then
                For Each _problem As Trinity.cProblem In _problemList
                    If _problem.AutoFix Then
                        _problem.FixMe()
                    Else
                        grdProblems.Rows.Add()
                    End If

                Next
                grdProblems.Invalidate()
            End If
        Else
            Me.Invoke(New ProblemsChanged(AddressOf BindList), List)
        End If
    End Sub

    Private Sub grdProblems_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdProblems.CellValueNeeded
        If e.RowIndex > _problemList.Count - 1 Then Exit Sub
        Dim _problem As Trinity.cProblem = _problemList(e.RowIndex)

        Select Case grdProblems.Columns(e.ColumnIndex).Name
            Case "colSeverity"
                Select Case _problem.Severity
                    Case Trinity.cProblem.ProblemSeverityEnum.Warning
                        e.Value = lstImages.Images("Warning")
                    Case Trinity.cProblem.ProblemSeverityEnum.Error
                        e.Value = lstImages.Images("Error")
                    Case Trinity.cProblem.ProblemSeverityEnum.Message
                        e.Value = lstImages.Images("Message")
                End Select
            Case "colProblem"
                e.Value = _problem.Message
            Case "colSource"
                e.Value = _problem.Source
                ' Case "colFixit"
                ' Stop
        End Select

    End Sub

    Private Sub grdProblems_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdProblems.CellContentClick
        Dim _problem As Trinity.cProblem = _problemList(e.RowIndex)
        If grdProblems.Columns(e.ColumnIndex).Name = "colHelp" Then
            Dim frmHelp As New frmHelp(_problem.HelpText, _problem)
            frmHelp.ShowDialog()
        ElseIf grdProblems.Columns(e.ColumnIndex).Name = "colFixit" Then
            If _problem.AutoFixable Then _problem.FixMe()
            BindList(Campaign.Problems)
        End If
    End Sub

    Private Sub grdProblems_CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles grdProblems.CellPainting
        If e.ColumnIndex = -1 OrElse e.RowIndex = -1 Then
            Return
        End If
        If grdProblems.Columns(e.ColumnIndex).Name = "colHelp" And e.RowIndex >= 0 Then
            Dim ico As Image = lstImages.Images("Help")
            e.Paint(e.CellBounds, Windows.Forms.DataGridViewPaintParts.All)
            e.Graphics.DrawImage(ico, e.CellBounds.Left + 2, e.CellBounds.Top + 2)
            e.Handled = True
        End If
        If grdProblems.Columns(e.ColumnIndex).Name = "colFixit" Then
            If e.RowIndex >= 0 And _problemList(e.RowIndex).AutoFixable Then
                Dim ico As Image = lstImages.Images("Greenpin")
                e.Paint(e.CellBounds, Windows.Forms.DataGridViewPaintParts.All)
                e.Graphics.DrawImage(ico, e.CellBounds.Left + 2, e.CellBounds.Top + 2)
                e.Handled = True
            Else
                Dim ico As Image = lstImages.Images("Redpin")
                e.Paint(e.CellBounds, Windows.Forms.DataGridViewPaintParts.All)
                e.Graphics.DrawImage(ico, e.CellBounds.Left + 2, e.CellBounds.Top + 2)
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub chkShowAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowAll.CheckedChanged
        BindList(Campaign.Problems)
    End Sub

    Private Sub cmdSolveSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSolveSelected.Click
        For Each _row As Windows.Forms.DataGridViewRow In grdProblems.SelectedRows
            Dim _problem As Trinity.cProblem = _problemList(_row.Index)
            If _problem.AutoFixable Then _problem.FixMe()
        Next
    End Sub
End Class