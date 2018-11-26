Imports System.Xml.Linq
Imports ExcelReadTemplates

Namespace ScheduleTemplates

    Public Class cSpotlistRules

        Public StartRules As cRuleList
        Public EndRules As cRuleList
        Public SkipRules As cMutableRuleList
        Public BudgetRules As cMutableRuleList
        Public LabelRules As cMutableRuleList

        Private _template As cScheduleTemplate

        Sub New(Template As cScheduleTemplate)
            StartRules = New cRuleList(Template)
            StartRules.Add(New cRule(Template))
            EndRules = New cRuleList(Template)
            EndRules.Add(New cRule(Template))
            SkipRules = New cMutableRuleList(Template)
            SkipRules.Add(New cRule(Template))
            BudgetRules = New cMutableRuleList(Template)
            BudgetRules.Add(New cRule(Template))
            LabelRules = New cMutableRuleList(Template)
            LabelRules.Add(New cRule(Template))
            _template = Template
        End Sub

        Function GetXML() As XElement
            Dim _xml = <spotlist>
                           <%= If(StartRules(0).Searches.Count > 0, <start><%= From _search In StartRules(0).Searches Select _search.GetAsXML() %></start>, Nothing) %>
                           <end>
                               <%= From _search In EndRules(0).Searches Select _search.GetAsXML %>
                           </end>
                           <%= If(SkipRules(0).Searches.Count > 0, <skip><%= From _search In SkipRules(0).Searches Select _search.GetAsXML() %></skip>, Nothing) %>
                           <%= If(BudgetRules(0).Searches.Count > 0, <netbudget><%= From _search In BudgetRules(0).Searches Select _search.GetAsXML() %></netbudget>, Nothing) %>
                           <%= If(LabelRules(0).Searches.Count > 0, <label><%= From _search In LabelRules(0).Searches Select _search.GetAsXML() %></label>, Nothing) %>
                       </spotlist>

            Return _xml
        End Function

        Sub Parse(xml As XElement)
            StartRules(0).Searches.Clear()
            If xml.<start> IsNot Nothing Then
                For Each _searchNode As XElement In xml.<start>.Elements
                    Dim _search As New cSearch(StartRules(0), _template)
                    _search.Parse(_searchNode)
                Next
            End If
            EndRules(0).Searches.Clear()
            For Each _searchNode As XElement In xml.<end>.Elements
                Dim _search As New cSearch(EndRules(0), _template)
                _search.Parse(_searchNode)
            Next
            SkipRules(0).Searches.Clear()
            If xml.<skip> IsNot Nothing Then
                For Each _searchNode As XElement In xml.<skip>.Elements
                    Dim _search As New cSearch(SkipRules(0), _template)
                    _search.Parse(_searchNode)
                Next
            End If
            BudgetRules(0).Searches.Clear()
            If xml.<netbudget> IsNot Nothing Then
                For Each _searchNode As XElement In xml.<netbudget>.Elements
                    Dim _search As New cSearch(BudgetRules(0), _template)
                    _search.Parse(_searchNode)
                Next
            End If
            LabelRules(0).Searches.Clear()
            If xml.<label> IsNot Nothing Then
                For Each _searchNode As XElement In xml.<label>.Elements
                    Dim _search As New cSearch(LabelRules(0), _template)
                    _search.Parse(_searchNode)
                Next
            End If
        End Sub

        Sub Validate(Schedule As cDocument)
            StartRules(0).Validate(Schedule)
            EndRules(0).Validate(Schedule)
            SkipRules(0).Validate(Schedule)
            BudgetRules(0).Validate(Schedule)
        End Sub
    End Class
End Namespace