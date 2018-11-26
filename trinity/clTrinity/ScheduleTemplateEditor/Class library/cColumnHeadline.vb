Public Class cColumnHeadline

    Public Property ColumnName As String = ""
    Public Property Headlines As New List(Of String)
    Public Property Required As Boolean = False

    Public Property ValidationResult As cSearchResult

    Private _template As cScheduleTemplate

    Public Sub New(Template As cScheduleTemplate, Optional Name As String = "")
        ColumnName = Name
        _template = Template
    End Sub

    Public Overrides Function ToString() As String
        Return ColumnName
    End Function

    Public Sub Validate(Schedule As cSchedule, Rows As cRuleList)
        Dim _sheet As cScheduleSheet = Schedule.Sheets(_template.UseSheet(Schedule))
        ValidationResult = New cSearchResult With {.Succeeded = False}
        For Each _row In Rows
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

    Function GetXML() As XElement
        If Headlines.Count = 0 Then Return Nothing
        Return <<%= String.Concat(ColumnName.ToLower, "column") %> required=<%= Required %>><%= From _headline In Headlines Select <headline value=<%= _headline %>/> %></>
    End Function

    Friend Function GetAsXML() As XElement
        Return GetXML
    End Function

End Class
