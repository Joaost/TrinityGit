Public Class cScheduleTemplate

    Public Property IdentificationRules As New cMutableRuleList(Me)
    Public Property TargetRules As New cRuleList(Me)
    Public Property ContractRules As New cRuleList(Me)
    Public Property ChannelRules As New cRuleList(Me)
    Public Property HeadlineRowRules As New cMutableRuleList(Me)
    Public Property ColumnHeadlines As New Dictionary(Of String, cColumnHeadline)
    Public Property PossibleSheetNames As New List(Of String)

    Public Property UseFirstSheetIfNotFound As Boolean = True

    Public Property Name As String = "New template"

    Public Enum ValidationResult
        Success
        SheetNotFound
    End Enum

    Public Sub New()
        Me.New(True)

    End Sub

    Public Sub New(AutoCreate As Boolean)
        MyBase.New()
        IdentificationRules.Add(New cRule(Me))
        ContractRules.Add(New cRule(Me))
        TargetRules.Add(New cRule(Me))
        ChannelRules.Add(New cRule(Me))
        HeadlineRowRules.Add(New cRule(Me) With {.Name = "Row"})

        ColumnHeadlines.Add("channel", New cColumnHeadline(Me, "Channel"))
        ColumnHeadlines.Add("date", New cColumnHeadline(Me, "Date"))
        ColumnHeadlines.Add("time", New cColumnHeadline(Me, "Time"))
        ColumnHeadlines.Add("program", New cColumnHeadline(Me, "Program"))
        ColumnHeadlines.Add("progbefore", New cColumnHeadline(Me, "ProgBefore"))
        ColumnHeadlines.Add("progafter", New cColumnHeadline(Me, "ProgAfter"))
        ColumnHeadlines.Add("filmcode", New cColumnHeadline(Me, "Filmcode"))
        ColumnHeadlines.Add("length", New cColumnHeadline(Me, "Length"))
        ColumnHeadlines.Add("rating", New cColumnHeadline(Me, "Rating"))
        ColumnHeadlines.Add("netprice", New cColumnHeadline(Me, "NetPrice"))
        ColumnHeadlines.Add("grossprice", New cColumnHeadline(Me, "GrossPrice"))
        ColumnHeadlines.Add("remark", New cColumnHeadline(Me, "Remark"))
        ColumnHeadlines.Add("addedvalue", New cColumnHeadline(Me, "AddedValue"))
        ColumnHeadlines.Add("bookingtype", New cColumnHeadline(Me, "Bookingtype"))

        If AutoCreate Then
            Dim _check As New cSearch(IdentificationRules(0), Me)
        End If
    End Sub

    Public Overrides Function ToString() As String
        Return Name
    End Function

    Function GetXML() As XElement
        Dim _xmlTemplate = <template name=<%= Name %>>
                               <sheets>
                                   <%= From _sheet As String In PossibleSheetNames Select <sheet name=<%= _sheet %>/> %>
                               </sheets>
                               <identify>
                                   <%= From _rule As cRule In IdentificationRules Select _rule.GetXML("rule") %>
                               </identify>
                               <target required=<%= TargetRules.IsRequired %>>
                                   <%= From _search As cSearch In TargetRules(0).Searches.Where(Function(s) s.ParentSearch Is Nothing) Select _search.GetAsXML %>
                                   <%= If(TargetRules.IsRequired, Nothing, <fallback><%= TargetRules.Fallback %></fallback>) %>
                               </target>
                               <contractno required=<%= ContractRules.IsRequired %>>
                                   <%= From _search As cSearch In ContractRules(0).Searches.Where(Function(s) s.ParentSearch Is Nothing) Select _search.GetAsXML %>
                                   <%= If(ContractRules.IsRequired, Nothing, <fallback><%= ContractRules.Fallback %></fallback>) %>
                               </contractno>
                               <channel required=<%= ChannelRules.IsRequired %>>
                                   <%= From _search As cSearch In ChannelRules(0).Searches.Where(Function(s) s.ParentSearch Is Nothing) Select _search.GetAsXML %>
                                   <%= If(ChannelRules.IsRequired, Nothing, <fallback><%= ChannelRules.Fallback %></fallback>) %>
                               </channel>
                               <columns>
                                   <%= From _rule As cRule In HeadlineRowRules Select _rule.GetXML("row") %>
                                   <%= From _column As cColumnHeadline In ColumnHeadlines.Values Select _column.GetAsXML %>
                               </columns>
                               <spotlist/>
                           </template>
        Return _xmlTemplate
    End Function

    Sub Parse(xml As XDocument)

        Name = xml.<template>.@name

        IdentificationRules = New cMutableRuleList(Me)
        HeadlineRowRules = New cMutableRuleList(Me)
        PossibleSheetNames = New List(Of String)
        For Each _sheetNode As XElement In xml.<template>.<sheets>...<sheet>
            PossibleSheetNames.Add(_sheetNode.@name)
        Next

        For Each _ruleNode As XElement In xml.<template>.<identify>...<rule>
            Dim _rule As New cRule(Me)
            _rule.Parse(_ruleNode)
            IdentificationRules.Add(_rule)
        Next

        TargetRules(0).Searches.Clear()
        If xml.<template>.<target>.@required IsNot Nothing Then TargetRules.IsRequired = xml.<template>.<target>.@required
        If xml.<template>.<target>.<fallback> IsNot Nothing Then TargetRules.Fallback = xml.<template>.<target>.<fallback>.Value
        For Each _searchNode As XElement In xml.<template>.<target>.Elements.Where(Function(n) n.Name <> "fallback")
            Dim _search As New cSearch(TargetRules(0), Me)
            _search.Parse(_searchNode)
        Next

        ContractRules(0).Searches.Clear()
        If xml.<template>.<contractno>.@required IsNot Nothing Then ContractRules.IsRequired = xml.<template>.<contractno>.@required
        If xml.<template>.<contractno>.<fallback> IsNot Nothing Then ContractRules.Fallback = xml.<template>.<contractno>.<fallback>.Value
        For Each _searchNode As XElement In xml.<template>.<contractno>.Elements.Where(Function(n) n.Name <> "fallback")
            Dim _search As New cSearch(ContractRules(0), Me)
            _search.Parse(_searchNode)
        Next

        ChannelRules(0).Searches.Clear()
        If xml.<template>.<channel>.@required IsNot Nothing Then ContractRules.IsRequired = xml.<template>.<channel>.@required
        For Each _searchNode As XElement In xml.<template>.<channel>.Elements.Where(Function(n) n.Name <> "fallback")
            Dim _search As New cSearch(ChannelRules(0), Me)
            _search.Parse(_searchNode)
        Next

        For Each _ruleNode As XElement In xml.<template>.<columns>...<row>
            Dim _rule As New cRule(Me)
            _rule.Name = "Row"
            _rule.Parse(_ruleNode)
            HeadlineRowRules.Add(_rule)
        Next
        For Each _columnNode As XElement In xml.<template>.<columns>.Elements
            If ColumnHeadlines.ContainsKey(_columnNode.Name.LocalName.Replace("column", "")) Then
                For Each _headlineNode In _columnNode...<headline>
                    ColumnHeadlines(_columnNode.Name.LocalName.Replace("column", "")).Headlines.Add(_headlineNode.@value)
                    ColumnHeadlines(_columnNode.Name.LocalName.Replace("column", "")).Required = _columnNode.@required
                Next
            End If
        Next
    End Sub

    Function Validate(Schedule As cSchedule) As ValidationResult
        Dim _validate As ValidationResult = ValidationResult.Success
        If Schedule Is Nothing Then
            _validate = False
            Return _validate
        End If
        If UseSheet(Schedule) = "" Then
            _validate = ValidationResult.SheetNotFound
        End If

        IdentificationRules.Validate(Schedule)

        TargetRules.Validate(Schedule)

        ContractRules.Validate(Schedule)

        ChannelRules.Validate(Schedule)
        If ChannelRules(0).Searches.Count = 0 Then
            ChannelRules.ValidationResult.Succeeded = True
        End If

        HeadlineRowRules.Validate(Schedule)

        For Each _headline As cColumnHeadline In ColumnHeadlines.Values
            _headline.Validate(Schedule, HeadlineRowRules)
            If _headline.Required AndAlso Not _headline.ValidationResult.Succeeded Then
                HeadlineRowRules.ValidationResult.Succeeded = False
            End If
        Next

        Return _validate
    End Function

    Function UseSheet(Schedule As cSchedule) As String
        Dim _useSheet As String = ""
        For Each _sheet In PossibleSheetNames
            If Schedule.Sheets.ContainsKey(_sheet) Then
                _useSheet = _sheet
            End If
        Next
        If _useSheet = "" Then
            If UseFirstSheetIfNotFound Then
                _useSheet = Schedule.Sheets.Keys(0)
            End If
        End If
        Return _useSheet
    End Function

End Class
