Public Partial Class questionairesummary
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Public Function Divide(ByVal Tal1 As Object, ByVal Tal2 As Object) As Single
        If IsDBNull(Tal1) OrElse IsDBNull(Tal2) OrElse Tal2 = 0 Then
            Return 0
        Else
            Return Tal1 / Tal2
        End If
    End Function

    Sub UpdateTable()
        Dim _table As Table = upnQuestionaire.FindControl("Summary")
        _table.Rows.Clear()
        upnQuestionaire.Update()
        Dim xmlSummary As XmlDocument = Master.Database.GetQuestionaireSummary(Request.QueryString("id"))
        Dim _answers As Object = Master.Database.GetQuestionaireAnswers(Request.QueryString("qid"))

        Dim _sums As New Dictionary(Of String, Single)
        Dim _averages As New Dictionary(Of String, Single)
        Dim _shares As New Dictionary(Of String, Dictionary(Of Integer, Single))

        For Each _element As XmlElement In xmlSummary.SelectSingleNode("questionairesummary").ChildNodes
            Select Case _element.Name
                Case "sum"
                    _sums.Add(_element.GetAttribute("field"), 0)
                Case "average"
                    _averages.Add(_element.GetAttribute("field"), 0)
                Case "share"
                    Dim _elements As New Dictionary(Of Integer, Single)
                    For Each _subElement As XmlElement In _element.ChildNodes
                        _elements.Add(_subElement.GetAttribute("value"), 0)
                    Next
                    _shares.Add(_element.GetAttribute("field"), _elements)
            End Select
        Next

        For Each _answer As XmlDocument In _answers
            For i As Integer = 0 To _sums.Count - 1
                Dim _node As XmlElement = _answer.SelectSingleNode("//*[@name='" & _sums.Keys(i) & "']")
                If _node IsNot Nothing Then
                    _sums(_sums.Keys(i)) += _node.GetAttribute("answer")
                End If
            Next
            For i As Integer = 0 To _averages.Count - 1
                Dim _node As XmlElement = _answer.SelectSingleNode("//*[@name='" & _averages.Keys(i) & "']")
                If _node IsNot Nothing Then
                    _averages(_averages.Keys(i)) += _node.GetAttribute("answer")
                End If
            Next
            For i As Integer = 0 To _shares.Count - 1
                Dim _node As XmlElement = _answer.SelectSingleNode("//*[@name='" & _shares.Keys(i) & "']")
                If _node IsNot Nothing Then
                    _shares(_shares.Keys(i))(_node.GetAttribute("answer")) += 1
                End If
            Next
        Next
        For i As Integer = 0 To _averages.Count - 1
            _averages(_averages.Keys(i)) /= _answers.Count
        Next

        For Each _element As XmlElement In xmlSummary.SelectSingleNode("questionairesummary").ChildNodes
            Dim _row As New TableRow
            _row.Font.Bold = False
            _row.Font.Size = FontUnit.Parse("11px")
            Select Case _element.Name
                Case "output"
                    Select Case _element.GetAttribute("type")
                        Case "headline"
                            _row.Font.Bold = True
                            _row.Font.Size = FontUnit.Parse("14px")
                            _row.Cells.Add(New TableCell With {.Text = _element.GetAttribute("value"), .Wrap = False, .ColumnSpan = 2})
                        Case "text"
                            Dim _cell As TableCell = New TableCell With {.Text = _element.GetAttribute("value"), .Wrap = False, .ColumnSpan = 2}
                            If _element.GetAttribute("bold") = "true" Then
                                _row.Font.Bold = True
                            End If
                            _row.Cells.Add(_cell)
                    End Select
                Case "sum"
                    Dim _headlineCell As New TableCell
                    Dim _valueCell As New TableCell

                    _headlineCell.Text = _element.GetAttribute("label")
                    _row.Cells.Add(_headlineCell)

                    _valueCell.Text = Format(_sums(_element.GetAttribute("field")), "N1")
                    _row.Cells.Add(_valueCell)

                Case "average"
                    Dim _headlineCell As New TableCell
                    Dim _valueCell As New TableCell

                    _headlineCell.Text = _element.GetAttribute("label")
                    _row.Cells.Add(_headlineCell)

                    _valueCell.Text = Format(_averages(_element.GetAttribute("field")), "N1")
                    _row.Cells.Add(_valueCell)

                Case "share"
                    Dim _headlineCell As New TableCell
                    Dim _valueCell As New TableCell

                    _headlineCell.Text = _element.GetAttribute("label")
                    _headlineCell.VerticalAlign = VerticalAlign.Top

                    _row.Cells.Add(_headlineCell)

                    Dim _valueTable As New Table
                    _valueTable.Width = Unit.Percentage(100)
                    For Each _subElement As XmlElement In _element.ChildNodes
                        Dim _r As New TableRow
                        '_r.Font.Italic = True
                        _r.Cells.Add(New TableCell With {.Text = _subElement.GetAttribute("text"), .Width = Unit.Percentage(100), .Wrap = False})
                        _r.Cells.Add(New TableCell With {.Text = Format(_shares(_element.GetAttribute("field"))(_subElement.GetAttribute("value")) / _answers.count, "P1"), .Wrap = False})
                        _valueTable.Rows.Add(_r)
                    Next

                    _valueCell.Controls.Add(_valueTable)
                    _row.Cells.Add(_valueCell)
            End Select
            _table.Rows.Add(_row)
        Next
    End Sub

    Private Sub questionairesummary_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        UpdateTable()
    End Sub
End Class