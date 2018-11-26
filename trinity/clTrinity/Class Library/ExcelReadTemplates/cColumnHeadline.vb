Imports System.Xml.Linq

Namespace ExcelReadTemplates
    Public Class cColumnHeadline

        Public Property ColumnName As String = ""
        Public Property Headlines As New List(Of String)
        Public Property Required As Boolean = False
        Public Property TrimStart As Integer = 0
        Public Property TrimUntilLast As String = ""
        Public Property TrimUntilFirst As String = ""

        Public Property ValidationResult As cSearchResult

        Private _template As ITemplate

        Public Sub New(Template As ITemplate, Name As String)
            ColumnName = Name
            _template = Template
        End Sub

        Public Overrides Function ToString() As String
            Return ColumnName
        End Function

        Public Sub Validate(Schedule As cDocument, Rows As cRuleList)
            Dim _sheet As cTemplateSheet = Schedule.Sheets(_template.UseSheet(Schedule))
            ValidationResult = New cSearchResult With {.Succeeded = False}
            For Each _row As cRule In Rows
                If _row.ValidationResult Is Nothing Then _row.Validate(Schedule)
                If _row.ValidationResult.Succeeded Then
                    For _c As Integer = 1 To _sheet.Columns
                        Dim _toUpperHeadlines As List(Of String) = Headlines.Select(Function(s) If(s Is Nothing, "", s.ToUpper)).ToList()
                        If _toUpperHeadlines.Contains(_sheet.Cells(_row.ValidationResult.Row, _c).ToUpper) Then
                            ValidationResult.Succeeded = True
                            ValidationResult.Row = _row.ValidationResult.Row
                            ValidationResult.Column = _c
                            ValidationResult.Result = _sheet.Cells(_row.ValidationResult.Row, _c)
                            Return
                        End If
                    Next
                End If
            Next
        End Sub

        Public Sub Validate(Schedule As cDocument, Row As Integer)
            Dim _sheet As cTemplateSheet = Schedule.Sheets(_template.UseSheet(Schedule))
            ValidationResult = New cSearchResult With {.Succeeded = False}
            For _c As Integer = 1 To _sheet.Columns
                Dim _toUpperHeadlines As List(Of String) = Headlines.Select(Function(s) If(s Is Nothing, "", s.ToUpper)).ToList()
                If _toUpperHeadlines.Contains(_sheet.Cells(Row, _c).ToString.ToUpper) Then
                    ValidationResult.Succeeded = True
                    ValidationResult.Row = Row
                    ValidationResult.Column = _c
                    ValidationResult.Result = _sheet.Cells(Row, _c)
                    Return
                End If
            Next
        End Sub

        Function GetXML() As XElement
            If Headlines.Count = 0 Then Return Nothing
            Return <<%= String.Concat(ColumnName.ToLower, "column") %> trimstart=<%= TrimStart %> required=<%= Required %>><%= From _headline In Headlines Select <headline value=<%= _headline %>/> %></>
        End Function

        Friend Function GetAsXML() As XElement
            Return GetXML
        End Function

    End Class
End Namespace