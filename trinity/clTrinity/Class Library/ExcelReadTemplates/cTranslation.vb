Imports System.Xml.Linq

Namespace ExcelReadTemplates

    Public Class cTranslation

        Public Property From As String = ""
        Public Property [To] As String = ""
        Public Property Exact As Boolean = True

        Public Sub Parse(xml As XElement)
            [From] = xml.@from
            [To] = xml.@to
            If xml.@exact IsNot Nothing Then Exact = xml.@exact
        End Sub

        Public Function GetXML() As XElement
            Return <translation from=<%= [From] %> to=<%= [To] %> exact=<%= Exact %>></translation>
        End Function

        Friend Function GetAsXml() As XElement
            Return GetXML()
        End Function
    End Class

End Namespace

