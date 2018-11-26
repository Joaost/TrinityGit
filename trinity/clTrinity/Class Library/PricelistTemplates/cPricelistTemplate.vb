Imports System.Xml.Linq

Namespace PricelistTemplates
    Public Class cPricelistTemplate
        Implements ITemplate

        Public Property IdentificationRules As New cMutableRuleList(Me)
        Public Property HeadlineRowRules As New cMutableRuleList(Me)
        Public Property ColumnHeadlines As New Dictionary(Of String, cColumnHeadline)
        Public Property PossibleSheetNames As New List(Of String)
        Public Property TargetlistRules As New cTargetlistRules(Me)
        Public Property EndRules As New cRuleList(Me)
        Public Property Translations As New List(Of cTranslation)

        Public Property Name As String = "New template"
        Public Property PriceType As PriceTypeEnum
        Public Property Dayparts As Integer

        Public Enum PriceTypeEnum
            CPT
            CPP
        End Enum

        Private _lastValidatedSchedule As cDocument

        Public Sub New()
            MyBase.New()
            IdentificationRules.Add(New cRule(Me))
            HeadlineRowRules.Add(New cRule(Me) With {.Name = "Row"})
            EndRules.Add(New cRule(Me))

            ColumnHeadlines.Add("target", New cColumnHeadline(Me, "Target"))
            ColumnHeadlines.Add("universe", New cColumnHeadline(Me, "Universe"))
            ColumnHeadlines.Add("price", New cColumnHeadline(Me, "Price"))

        End Sub

        Function GetXML() As XElement Implements ITemplate.GetXML
            Dim _xmlTemplate = <template name=<%= Name %>>
                                   <sheets>
                                       <%= From _sheet As String In PossibleSheetNames Select <sheet name=<%= _sheet %>/> %>
                                   </sheets>
                                   <identify>
                                       <%= From _rule As cRule In IdentificationRules Select _rule.GetXML("rule") %>
                                   </identify>
                                   <columns>
                                       <%= From _rule As cRule In HeadlineRowRules Select _rule.GetXML("row") %>
                                       <%= From _column As cColumnHeadline In ColumnHeadlines.Values Select _column.GetAsXML %>
                                   </columns>
                                   <%= TargetlistRules.GetXML() %>
                                   <end>
                                       <%= From _rule As cRule In EndRules Select _rule.GetXML("rule") %>
                                   </end>
                                   <translations>
                                       <%= From _trans As cTranslation In Translations Select _trans.GetAsXml() %>
                                   </translations>
                               </template>
            Return _xmlTemplate
        End Function

        Sub Parse(xml As XDocument) Implements ITemplate.Parse

            Name = xml.<template>.@name
            Dayparts = xml.<template>.@dayparts
            PriceType = IIf(xml.<template>.@pricetype = "CPT", PriceTypeEnum.CPT, PriceTypeEnum.CPP)

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

            For Each _ruleNode As XElement In xml.<template>.<columns>...<row>
                Dim _rule As New cRule(Me)
                _rule.Name = "Row"
                _rule.Parse(_ruleNode)
                HeadlineRowRules.Add(_rule)
            Next
            For Each _columnNode As XElement In xml.<template>.<columns>.Elements
                If ColumnHeadlines.ContainsKey(_columnNode.Name.LocalName.Replace("column", "")) Then
                    For Each _headlineNode As XElement In _columnNode...<headline>
                        ColumnHeadlines(_columnNode.Name.LocalName.Replace("column", "")).Headlines.Add(_headlineNode.@value)
                    Next
                End If
            Next
            TargetlistRules = New cTargetlistRules(Me)
            TargetlistRules.Parse(xml.<template>.<targetlist>(0))

            EndRules(0).Searches.Clear()
            If xml.<template>.<end> IsNot Nothing Then
                For Each _searchNode As XElement In xml.<template>.<end>.Elements
                    Dim _search As New cSearch(EndRules(0), Me)
                    _search.Parse(_searchNode)
                Next
            End If
            If xml.<template>.<translations> IsNot Nothing Then
                For Each _transNode As XElement In xml.<template>.<translations>.Elements
                    Dim _trans As New cTranslation
                    _trans.Parse(_transNode)
                    Translations.Add(_trans)
                Next
            End If
        End Sub

        Public Function Validate(Schedule As ExcelReadTemplates.cDocument, Optional BreakOnFail As Boolean = False) As ExcelReadTemplates.ITemplate.ValidationResult Implements ExcelReadTemplates.ITemplate.Validate
            Dim _validate As ITemplate.ValidationResult = ITemplate.ValidationResult.Success
            If Schedule Is Nothing Then
                _validate = False
                Return _validate
            End If
            If UseSheet(Schedule) = "" Then
                _validate = ITemplate.ValidationResult.SheetNotFound
                If BreakOnFail Then Return _validate
            End If

            IdentificationRules.Validate(Schedule)
            If Not IdentificationRules.ValidationResult.Succeeded Then
                _validate = ITemplate.ValidationResult.IdentificationFailed
                If BreakOnFail Then Return _validate
            End If

            HeadlineRowRules.Validate(Schedule)

            For Each _headline As cColumnHeadline In ColumnHeadlines.Values
                _headline.Validate(Schedule, HeadlineRowRules)
                If _headline.Required AndAlso Not _headline.ValidationResult.Succeeded Then
                    HeadlineRowRules.ValidationResult.Succeeded = False
                End If
            Next

            TargetlistRules.Validate(Schedule)

            EndRules.Validate(Schedule)

            _lastValidatedSchedule = Schedule

            Return _validate
        End Function

        Public Function Translate([string] As Object) As Object
            If [string] Is Nothing Then Return Nothing
            For Each _trans As cTranslation In Translations
                If _trans.Exact Then
                    If [string] = _trans.From Then
                        [string] = _trans.To
                    End If
                Else
                    If [string].ToString.Contains(_trans.From) Then
                        [string] = [string].ToString.Replace(_trans.From, _trans.To)
                    End If
                End If
            Next
            Return [string]
        End Function

        Function UseSheet(Schedule As cDocument) As String Implements ITemplate.UseSheet
            Dim _useSheet As String = ""
            For Each _sheet As String In PossibleSheetNames
                If Schedule.Sheets.ContainsKey(_sheet) Then
                    _useSheet = _sheet
                End If
            Next
            Return _useSheet
        End Function

        Function GetPrices() As List(Of cPrice)
            Dim _startRow As Integer = 1
            For Each _rule As cRule In HeadlineRowRules
                For Each _search As cSearch In _rule.Searches.Where(Function(s) s.ParentSearch Is Nothing)
                    If _search.Execute(_lastValidatedSchedule, New cSearchResult() With {.Succeeded = True, .Row = _startRow, .Column = 1}).Succeeded Then
                        For Each _headline As cColumnHeadline In ColumnHeadlines.Values
                            _headline.Validate(_lastValidatedSchedule, _search.Result.Row)
                            If _headline.Required AndAlso Not _headline.ValidationResult.Succeeded Then
                                HeadlineRowRules.ValidationResult.Succeeded = False
                            End If
                        Next
                        _startRow = _search.Result.Row + 1
                        Exit For
                    End If
                Next
            Next
            Dim _list As New List(Of cPrice)
            Dim _ended As Boolean = False
            Dim _row As Integer = _startRow
            Dim _sheet As cTemplateSheet = _lastValidatedSchedule.Sheets(UseSheet(_lastValidatedSchedule))
            Dim _en As New System.Globalization.CultureInfo("en-US")
            Do
                Dim _skip As Boolean = False
                For Each _search As cSearch In TargetlistRules.SkipRules(0).Searches.Where(Function(s) s.ParentSearch Is Nothing)
                    If _search.Execute(_lastValidatedSchedule, New cSearchResult() With {.Succeeded = True, .Row = _row, .Column = 1}).Succeeded Then
                        _skip = True
                        Exit For
                    End If
                Next
                For Each _search As cSearch In TargetlistRules.EndRules(0).Searches.Where(Function(s) s.ParentSearch Is Nothing)
                    If _search.Execute(_lastValidatedSchedule, New cSearchResult() With {.Succeeded = True, .Row = _row, .Column = 1}).Succeeded Then
                        _ended = True
                        Exit For
                    End If
                Next
                If Not _skip AndAlso Not _ended Then
                    Dim _price As New cPrice
                    Try
                        _price.TargetName = GetColumnValue(_sheet, _row, "Target", "")
                        For _dp As Integer = 0 To Dayparts - 1
                            _price.Price(_dp) = GetColumnValue(_sheet, _row, "Price", 0, _dp)
                        Next
                        _price.UniSize = GetColumnValue(_sheet, _row, "Universe", 0)
                        _list.Add(_price)
                    Catch

                    End Try
                End If
                _row += 1
                For Each _search As cSearch In TargetlistRules.EndRules(0).Searches.Where(Function(s) s.ParentSearch Is Nothing)
                    If _search.Execute(_lastValidatedSchedule, New cSearchResult() With {.Succeeded = True, .Row = _row, .Column = 1}).Succeeded Then
                        _ended = True
                        Exit For
                    End If
                Next
            Loop Until _row > _sheet.Rows OrElse _ended
            Return _list
        End Function

        Private Function GetColumnValue(Sheet As cTemplateSheet, Row As Integer, ColumnName As String, DefaultValue As Object, Optional Modifier As Integer = 0) As Object
            If ColumnHeadlines.ContainsKey(ColumnName.ToLower) Then
                If ColumnHeadlines(ColumnName.ToLower).ValidationResult.Succeeded Then
                    Dim _val = Sheet.Cells(Row, ColumnHeadlines(ColumnName.ToLower).ValidationResult.Column + Modifier)
                    _val = Translate(_val)
                    Return _val
                End If
            End If
            Return DefaultValue
        End Function
    End Class
End Namespace
