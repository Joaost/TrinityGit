Public Class cSearch

    Private _template As cScheduleTemplate

    Private _result As cSearchResult
    Public Property Result() As cSearchResult
        Get
            If _result IsNot Nothing AndAlso _parentSearch IsNot Nothing AndAlso Not _parentSearch.Result Is Nothing AndAlso Not _parentSearch.Result.Succeeded Then
                _result.Succeeded = False
            End If
            Return _result
        End Get
        Set(ByVal value As cSearchResult)
            _result = value
        End Set
    End Property


    Public Enum RuleTypeEnum
        Check
        Find
        [Step]
    End Enum

    Public Enum SearchTypeEnum
        ValueIs
        ValueIsNot
        Contains
        ContainsNot
    End Enum

    Private _parentSearch As cSearch
    Public Property ParentSearch() As cSearch
        Get
            Return _parentSearch
        End Get
        Set(ByVal value As cSearch)
            If _parentSearch IsNot Nothing Then
                _parentSearch.ChildSearches.Remove(Me)
            End If
            _parentSearch = value
            If _parentSearch IsNot Nothing Then
                _parentSearch.ChildSearches.Add(Me)
            End If
        End Set
    End Property

    Private _childSearches As New List(Of cSearch)
    Public ReadOnly Property ChildSearches() As List(Of cSearch)
        Get
            Return _childSearches
        End Get
    End Property

    Private _column As String = 1
    Public Property Column As String
        Get
            Return _column
        End Get
        Set(value As String)
            If ValidateRowColumnValue(value) Then
                _column = value
            End If
        End Set
    End Property

    Private _row As String = 1
    Public Property Row As String
        Get
            Return _row
        End Get
        Set(value As String)
            If ValidateRowColumnValue(value) Then
                _row = value
            End If
        End Set
    End Property

    Private _toColumn As String = 1
    Public Property ToColumn As String
        Get
            Return _toColumn
        End Get
        Set(value As String)
            If ValidateRowColumnValue(value) Then
                _toColumn = value
            End If
        End Set
    End Property

    Private _toRow As String = 1
    Public Property ToRow As String
        Get
            Return _toRow
        End Get
        Set(value As String)
            If ValidateRowColumnValue(value) Then
                _toRow = value
            End If
        End Set
    End Property

    Public Property RuleType As RuleTypeEnum = RuleTypeEnum.Check

    Public Property SearchType As SearchTypeEnum = SearchTypeEnum.ValueIs

    Public Property SearchValue As String = ""

    Private _rule As cRule
    Public Property Rule As cRule
        Get
            Return _rule
        End Get
        Set(value As cRule)
            If _rule IsNot Nothing Then
                _rule.Searches.Remove(Me)
            End If
            _rule = value
            If _rule IsNot Nothing Then
                _rule.Searches.Add(Me)
            End If
        End Set
    End Property

    Friend Function ValidateRowColumnValue(Value As String) As Boolean
        If Integer.TryParse(Value, New Integer) Then
            Return True
        End If
        Return False
    End Function

    Friend Function TypeToString()
        Select Case _SearchType
            Case SearchTypeEnum.ValueIs
                Return "is"
            Case SearchTypeEnum.ValueIsNot
                Return "is not"
            Case SearchTypeEnum.Contains
                Return "contains"
            Case SearchTypeEnum.ContainsNot
                Return "does not contain"
            Case Else
                Return ""
        End Select
    End Function

    Public Overrides Function ToString() As String
        Dim _sb As New System.Text.StringBuilder

        Select Case _RuleType
            Case RuleTypeEnum.Check
                _sb.Append("Check that the cell in ")
                _sb.Append(CoordinateToString(Row, Column))
                _sb.Append(" " & TypeToString() & " '" & SearchValue & "'")
                Return _sb.ToString
            Case RuleTypeEnum.Find
                _sb.Append("Find a cell that " & TypeToString() & " '" & SearchValue & "' between " & CoordinateToString(Row, Column) & " and " & CoordinateToString(ToRow, ToColumn))
                Return _sb.ToString
            Case RuleTypeEnum.Step
                Return "Step " & Column & " columns, " & Row & " rows"
            Case Else
                Return ""
        End Select
    End Function

    Function CoordinateToString(Row As String, Column As String) As String
        Dim _sb As New System.Text.StringBuilder
        If Column.StartsWith("+") OrElse Column.StartsWith("-") Then
            If Integer.Parse(Column) = 0 Then
                _sb.Append("the same column")
            Else
                If Integer.Parse(Column) > 1 Then
                    _sb.Append(Integer.Parse(Column) & " columns")
                Else
                    _sb.Append("the column")
                End If
                If Column.StartsWith("+") Then
                    _sb.Append(" to the right")
                Else
                    _sb.Append(" to the left")
                End If
            End If
        Else
            _sb.Append("column " & Integer.Parse(Column))
        End If
        _sb.Append(" on ")
        If Row.StartsWith("+") OrElse Row.StartsWith("-") Then
            If Integer.Parse(Row) = 0 Then
                _sb.Append("the same row")
            Else
                If Integer.Parse(Row) > 1 Then
                    _sb.Append(Integer.Parse(Row) & " rows")
                Else
                    _sb.Append("the row")
                End If
                If Row.StartsWith("+") Then
                    _sb.Append(" below")
                Else
                    _sb.Append(" above")
                End If
            End If
        Else
            _sb.Append("row " & Integer.Parse(Row))
        End If
        Return _sb.ToString
    End Function

    Public Sub New(Rule As cRule, Template As cScheduleTemplate, Optional ParentSearch As cSearch = Nothing)
        Me.Rule = Rule
        If ParentSearch IsNot Nothing Then
            Me.ParentSearch = ParentSearch
            Me.Rule.Searches.Remove(Me)
        End If
        _template = Template
    End Sub

    Function GetXML() As XElement
        Dim _searchTypeString As String = ""
        Select Case SearchType
            Case SearchTypeEnum.ValueIs
                _searchTypeString = "valueis"
            Case SearchTypeEnum.ValueIsNot
                _searchTypeString = "valueisnot"
            Case SearchTypeEnum.Contains
                _searchTypeString = "contains"
            Case SearchTypeEnum.ContainsNot
                _searchTypeString = "notcontains"
        End Select
        Dim _node As XElement = Nothing
        Select Case RuleType
            Case RuleTypeEnum.Find
                _node = <find <%= _searchTypeString %>=<%= SearchValue %> fromcol=<%= Column %> fromrow=<%= Row %> tocol=<%= ToColumn %> torow=<%= ToRow %>><%= From _search As cSearch In ChildSearches Select _search.GetAsXML() %></find>
            Case RuleTypeEnum.Check
                _node = <check <%= _searchTypeString %>=<%= SearchValue %> col=<%= Column %> row=<%= Row %>><%= From _search As cSearch In ChildSearches Select _search.GetAsXML() %></check>
            Case RuleTypeEnum.Step
                _node = <step col=<%= Column %> row=<%= Row %>><%= From _search As cSearch In ChildSearches Select _search.GetAsXML() %></step>
        End Select
        Return _node
    End Function

    Friend Function GetAsXML() As XElement
        Return GetXML()
    End Function

    Sub Parse(xml As XElement)
        For Each _searchNode As XElement In xml.Elements
            Dim _search As New cSearch(Rule, _template, Me)
            _search.Parse(_searchNode)
        Next
        Select Case xml.Name
            Case "find"
                RuleType = RuleTypeEnum.Find
                Column = xml.@fromcol
                Row = xml.@fromrow
                ToColumn = xml.@tocol
                ToRow = xml.@torow                
            Case "check"
                RuleType = RuleTypeEnum.Check
                Column = xml.@col
                Row = xml.@row
            Case "step"
                RuleType = RuleTypeEnum.Step
                Column = xml.@col
                Row = xml.@row
        End Select
        If xml.Attribute("valueis") IsNot Nothing Then
            SearchType = SearchTypeEnum.ValueIs
            SearchValue = xml.Attribute("valueis")
        ElseIf xml.Attribute("valueisnot") IsNot Nothing Then
            SearchType = SearchTypeEnum.ValueIsNot
            SearchValue = xml.Attribute("valueisnot")
        ElseIf xml.Attribute("contains") IsNot Nothing Then
            SearchType = SearchTypeEnum.Contains
            SearchValue = xml.Attribute("contains")
        ElseIf xml.Attribute("notcontains") IsNot Nothing Then
            SearchType = SearchTypeEnum.ContainsNot
            SearchValue = xml.Attribute("notcontains")
        End If
    End Sub

    Function Execute(Schedule As cSchedule, Optional PreviousSearchResult As cSearchResult = Nothing) As cSearchResult
        Dim _result As New cSearchResult With {.Succeeded = False}
        If _template.UseSheet(Schedule) = "" Then
            Return _result
        End If
        Dim _sheet As cScheduleSheet = Schedule.Sheets(_template.UseSheet(Schedule))
        Dim _column As Integer
        Dim _row As Integer
        _column = Integer.Parse(Column)
        _row = Integer.Parse(Row)
        If PreviousSearchResult IsNot Nothing Then
            If Column.StartsWith("+") OrElse Column.StartsWith("-") OrElse RuleType = RuleTypeEnum.Step Then
                _column += PreviousSearchResult.Column
            End If
            If Row.StartsWith("+") OrElse Row.StartsWith("-") OrElse RuleType = RuleTypeEnum.Step Then
                _row += PreviousSearchResult.Row
            End If
        End If
        If Not (_sheet.Rows < _row OrElse _sheet.Columns < _column) Then
            Select Case _RuleType
                Case RuleTypeEnum.Check
                    _result = GetResultForCell(Schedule, _sheet, _row, _column)
                    _result.Result = _sheet.Cells(_row, _column)
                Case RuleTypeEnum.Find
                    Dim _toColumn As Integer
                    Dim _toRow As Integer
                    _toColumn = Integer.Parse(ToColumn)
                    _toRow = Integer.Parse(ToRow)
                    If PreviousSearchResult IsNot Nothing Then
                        If ToColumn.StartsWith("+") OrElse ToColumn.StartsWith("-") Then
                            _toColumn += PreviousSearchResult.Column
                        End If
                        If ToRow.StartsWith("+") OrElse ToRow.StartsWith("-") Then
                            _toRow += PreviousSearchResult.Row
                        End If
                    End If
                    For _r As Integer = _row To _toRow
                        For _c As Integer = _column To _toColumn
                            _result = GetResultForCell(Schedule, _sheet, _r, _c)
                            If _result.Succeeded Then
                                Return _result
                            End If
                        Next
                    Next
                Case RuleTypeEnum.Step
                    _result.Row = _row
                    _result.Column = _column
                    _result.Succeeded = True
                    _result.Result = _sheet.Cells(_row, _column)
                    Me.Result = _result
                    Return _result
            End Select
        End If
        Return _result
    End Function

    Private Function GetResultForCell(Schedule As cSchedule, Sheet As cScheduleSheet, Row As Integer, Column As Integer) As cSearchResult
        _result = New cSearchResult With {.Succeeded = False}
        If Sheet.Columns < Column OrElse Sheet.Rows < Row OrElse Column < 1 OrElse Row < 1 Then Return _result
        Select Case SearchType
            Case SearchTypeEnum.ValueIs
                If Sheet.Cells(Row, Column).ToUpper = SearchValue.ToUpper Then
                    _result.Row = Row
                    _result.Column = Column
                    _result.Succeeded = True
                    _result.Result = Sheet.Cells(Row, Column)
                    If ChildSearches.Count > 0 Then
                        For Each _search As cSearch In ChildSearches
                            Dim _subResult As cSearchResult = _search.Execute(Schedule, _result)
                            If _subResult.Succeeded Then
                                Return _subResult
                            End If
                        Next
                        Return New cSearchResult With {.Succeeded = False}
                    End If
                End If
            Case SearchTypeEnum.ValueIsNot
                If Not Sheet.Cells(Row, Column).ToUpper = SearchValue.ToUpper Then
                    _result.Row = Row
                    _result.Column = Column
                    _result.Succeeded = True
                    _result.Result = Sheet.Cells(Row, Column)
                    If ChildSearches.Count > 0 Then
                        For Each _search As cSearch In ChildSearches
                            Dim _subResult As cSearchResult = _search.Execute(Schedule, _result)
                            If _subResult.Succeeded Then
                                Return _subResult
                            End If
                        Next
                        Return New cSearchResult With {.Succeeded = False}
                    End If

                End If

            Case SearchTypeEnum.Contains
                If Sheet.Cells(Row, Column).ToUpper.Contains(SearchValue.ToUpper) Then
                    _result.Row = Row
                    _result.Column = Column
                    _result.Succeeded = True
                    _result.Result = Sheet.Cells(Row, Column)
                    If ChildSearches.Count > 0 Then
                        For Each _search As cSearch In ChildSearches
                            Dim _subResult As cSearchResult = _search.Execute(Schedule, _result)
                            If _subResult.Succeeded Then
                                Return _subResult
                            End If
                        Next
                        Return New cSearchResult With {.Succeeded = False}
                    End If
                End If
            Case SearchTypeEnum.ContainsNot
                If Not Sheet.Cells(Row, Column).ToUpper.Contains(SearchValue.ToUpper) Then
                    _result.Row = Row
                    _result.Column = Column
                    _result.Succeeded = True
                    _result.Result = Sheet.Cells(Row, Column)
                    If ChildSearches.Count > 0 Then
                        For Each _search As cSearch In ChildSearches
                            Dim _subResult As cSearchResult = _search.Execute(Schedule, _result)
                            If _subResult.Succeeded Then
                                Return _subResult
                            End If
                        Next
                        Return New cSearchResult With {.Succeeded = False}
                    End If
                End If
        End Select
        Return _result
    End Function

    Function Clone(Rule As cRule, Template As cScheduleTemplate, Optional ParentSearch As cSearch = Nothing) As cSearch
        Dim _search As New cSearch(Rule, Template, ParentSearch)
        _search.Column = Column
        _search.Row = Row
        _search.ToColumn = ToColumn
        _search.ToRow = ToRow
        _search.SearchType = SearchType
        _search.SearchValue = SearchValue
        _search.RuleType = RuleType

        For Each _child As cSearch In ChildSearches
            _child.Clone(Rule, Template, _search)
        Next
        Return _search
    End Function
End Class
