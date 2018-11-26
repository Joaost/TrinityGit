Imports System.Xml.Linq

Namespace ExcelReadTemplates
    Public Class cRule

        Public Property Searches As New List(Of cSearch)

        Public Property Name As String = "Rule"
        Public Property Required As Boolean = False

        Private _template As ITemplate
        Public ReadOnly Property Template() As ITemplate
            Get
                Return _template
            End Get
        End Property


        Private _validationResult As cSearchResult
        Public ReadOnly Property ValidationResult As cSearchResult
            Get
                Return _validationResult
            End Get
        End Property

        Function GetXML(Optional NodeName As String = "rule") As XElement
            Dim _xmlTemplate = <<%= NodeName %>>
                                   <%= From _search As cSearch In Searches.Where(Function(s) s.ParentSearch Is Nothing) Select _search.GetAsXML %>
                               </>
            Return _xmlTemplate
        End Function

        Friend Function GetAsXML(Optional NodeName As String = "rule") As XElement
            Return GetXML()
        End Function

        Sub Parse(xml As XElement)
            For Each _searchNode As XElement In xml.Elements
                Dim _search As New cSearch(Me, Template)
                _search.Parse(_searchNode)
            Next
        End Sub

        Function Clone(Template As ITemplate) As cRule
            Dim _rule As New cRule(Template)
            For Each _search As cSearch In Searches.Where(Function(s) s.ParentSearch Is Nothing)
                _search.Clone(_rule, Template)
            Next
            Return _rule
        End Function

        Sub Validate(Schedule As cDocument)
            _validationResult = New cSearchResult With {.Succeeded = False}
            For Each _search As cSearch In Searches
                _search.Result = New cSearchResult With {.Succeeded = False}
            Next
            For Each _search As cSearch In Searches.Where(Function(s) s.ParentSearch Is Nothing)
                Dim _result As cSearchResult = _search.Execute(Schedule)
                If _result.Succeeded Then
                    _validationResult = _result
                End If
            Next
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function

        Public Sub New(Template As ITemplate)
            _template = Template
        End Sub
    End Class
End Namespace