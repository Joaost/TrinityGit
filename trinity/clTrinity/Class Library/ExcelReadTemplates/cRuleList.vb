
Namespace ExcelReadTemplates
    Public Class cRuleList
        Implements IEnumerable(Of cRule)

        Public Property ValidationResult As cSearchResult
        Public Property IsRequired As Boolean = True
        Public Property Fallback As String = ""

        Private _template As ITemplate

        Private _rules As New List(Of cRule)

        Friend Function Add(Rule As cRule) As cRule
            _rules.Add(Rule)
            Return Rule
        End Function

        Friend Sub Remove(Rule As cRule)
            _rules.Remove(Rule)
        End Sub

        Default ReadOnly Property Item(Index As Integer) As cRule
            Get
                Return _rules(Index)
            End Get
        End Property

        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of cRule) Implements System.Collections.Generic.IEnumerable(Of cRule).GetEnumerator
            Return _rules.GetEnumerator()
        End Function

        Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return _rules.GetEnumerator()
        End Function

        Sub Validate(Schedule As cDocument)
            ValidationResult = New cSearchResult With {.Succeeded = False}
            For Each _rule As cRule In _rules
                _rule.Validate(Schedule) 'break 20180328'
                If _rule.ValidationResult.Succeeded Then
                    ValidationResult.Succeeded = True
                    ValidationResult.Row = _rule.ValidationResult.Row
                    ValidationResult.Column = _rule.ValidationResult.Column
                    ValidationResult.Result = _rule.ValidationResult.Result
                    Return
                End If 'break 20180328'
            Next
            If Not IsRequired Then
                ValidationResult.Succeeded = True
                ValidationResult.Row = 0
                ValidationResult.Column = 0
            ElseIf IsRequired Then
                ValidationResult.Succeeded = False
            End If

        End Sub

        Public Sub New(Template As ITemplate)
            _template = Template
        End Sub
    End Class
End Namespace