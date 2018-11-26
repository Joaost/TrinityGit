Namespace ExcelReadTemplates
    Public Class cMutableRuleList
        Inherits cRuleList

        Public Overloads Function Add(Rule As cRule) As cRule
            Return MyBase.Add(Rule)
        End Function

        Public Overloads Sub Remove(Rule As cRule)
            MyBase.Remove(Rule)
        End Sub

        Public Sub New(Template As ITemplate)
            MyBase.New(Template)
        End Sub
    End Class
End Namespace