Imports System.Xml.Linq
Imports System.Globalization

Namespace ScheduleTemplates
    Public Class cScheduleTemplate
        Implements ITemplate

        Public Property IdentificationRules As New cMutableRuleList(Me)
        Public Property TargetRules As New cRuleList(Me)
        Public Property ContractRules As New cRuleList(Me)
        Public Property ChannelRules As New cRuleList(Me)
        Public Property HeadlineRowRules As New cMutableRuleList(Me)
        Public Property ColumnHeadlines As New Dictionary(Of String, cColumnHeadline)
        Public Property PossibleSheetNames As New List(Of String)
        Public Property SpotlistRules As New cSpotlistRules(Me)
        Public Property EndRules As New cRuleList(Me)
        Public Property Translations As New List(Of cTranslation)
        Public Property DateFormats As New List(Of String)
        Public Property Multi As Boolean = False
        Public Property NewChunkOnlyOnLabel As Boolean = False

        Public Property UseFirstSheetIfNotFound As Boolean = True

        Public Property Name As String = "New template"

        Private _lastValidatedSchedule As cDocument

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
            EndRules.Add(New cRule(Me))

            ColumnHeadlines.Add("channel", New cColumnHeadline(Me, "Channel"))
            ColumnHeadlines.Add("date", New cColumnHeadline(Me, "Date"))
            ColumnHeadlines.Add("year", New cColumnHeadline(Me, "Year"))
            ColumnHeadlines.Add("month", New cColumnHeadline(Me, "Month"))
            ColumnHeadlines.Add("day", New cColumnHeadline(Me, "Day"))
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

        Function GetXML() As XElement Implements ITemplate.GetXML
            Dim _xmlTemplate = <template name=<%= Name %> multi=<%= Multi %> newchunkonlyonlable=<%= NewChunkOnlyOnLabel %>>
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
                                   <%= SpotlistRules.GetXML() %>
                                   <end>
                                       <%= From _rule As cRule In EndRules Select _rule.GetXML("rule") %>
                                   </end>
                                   <translations>
                                       <%= From _trans As cTranslation In Translations Select _trans.GetAsXml() %>
                                   </translations>
                                   <dateformats>
                                       <%= From _format As String In DateFormats Select <format string=<%= _format %>></format> %>
                                   </dateformats>
                               </template>
            Return _xmlTemplate
        End Function

        Sub Parse(xml As XDocument) Implements ITemplate.Parse

            Name = xml.<template>.@name
            Boolean.TryParse(xml.<template>.@multi, Multi)
            Boolean.TryParse(xml.<template>.@newchunkonlyonlabel, NewChunkOnlyOnLabel)

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
            If xml.<template>.<channel>.@required IsNot Nothing Then ChannelRules.IsRequired = xml.<template>.<channel>.@required
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
                    For Each _headlineNode As XElement In _columnNode...<headline>
                        ColumnHeadlines(_columnNode.Name.LocalName.Replace("column", "")).Headlines.Add(_headlineNode.@value)
                        ColumnHeadlines(_columnNode.Name.LocalName.Replace("column", "")).Required = _columnNode.@required
                        If _columnNode.@trimstart IsNot Nothing Then
                            ColumnHeadlines(_columnNode.Name.LocalName.Replace("column", "")).TrimStart = _columnNode.@trimstart
                        End If
                        If _columnNode.@trimuntillast IsNot Nothing Then
                            ColumnHeadlines(_columnNode.Name.LocalName.Replace("column", "")).TrimUntilLast = _columnNode.@trimuntillast
                        End If
                        If _columnNode.@trimuntilfirst IsNot Nothing Then
                            ColumnHeadlines(_columnNode.Name.LocalName.Replace("column", "")).TrimUntilFirst = _columnNode.@trimuntilfirst
                        End If
                    Next
                End If
            Next
            SpotlistRules = New cSpotlistRules(Me)
            SpotlistRules.Parse(xml.<template>.<spotlist>(0))

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
            DateFormats.Clear()
            If xml.<template>.<dateformats> IsNot Nothing Then
                For Each _formatNode As XElement In xml.<template>.<dateformats>.Elements
                    DateFormats.Add(_formatNode.@string)
                Next
            End If
        End Sub

        Function Validate(Schedule As cDocument, Optional BreakOnFail As Boolean = False) As ITemplate.ValidationResult Implements ITemplate.Validate
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

            TargetRules.Validate(Schedule)
            If TargetRules.IsRequired AndAlso Not TargetRules.ValidationResult.Succeeded Then
                _validate = ITemplate.ValidationResult.TargetNotFound
                If BreakOnFail Then Return _validate
            End If

            ContractRules.Validate(Schedule)
            If ContractRules.IsRequired AndAlso Not ContractRules.ValidationResult.Succeeded Then
                _validate = ITemplate.ValidationResult.ContractNoNotFound
                If BreakOnFail Then Return _validate
            End If

            'Need to be analyzed and optimized. '
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

            SpotlistRules.Validate(Schedule)

            EndRules.Validate(Schedule)

            _lastValidatedSchedule = Schedule

            Return _validate
        End Function

        Function GetContractNumber() As String
            Return ContractRules().ValidationResult.Result
        End Function

    
        Function GetChannels() As List(Of String)
            Dim _list As New List(Of String)
            If ChannelRules.ValidationResult.Succeeded Then
                _list.Add(ChannelRules.ValidationResult.Result)
            Else
                For Each _chan As String In (From _c In GetSpots() Select _c.Channel)
                    _list.Add(_chan)
                Next
            End If
            Return _list
        End Function

        Function GetSpots() As List(Of cChunkInfo)
            Dim _startRow As Integer = 0
            Dim _retVal As New List(Of cChunkInfo)

            HeadlineRowRules.Validate(_lastValidatedSchedule)

            For Each _headline As cColumnHeadline In ColumnHeadlines.Values
                _headline.Validate(_lastValidatedSchedule, HeadlineRowRules)
                If _headline.Required AndAlso Not _headline.ValidationResult.Succeeded Then
                    HeadlineRowRules.ValidationResult.Succeeded = False
                End If
            Next

            If SpotlistRules.StartRules.ValidationResult IsNot Nothing AndAlso SpotlistRules.StartRules.ValidationResult.Succeeded Then
                _startRow = SpotlistRules.StartRules.ValidationResult.Result
            Else
                _startRow = HeadlineRowRules.ValidationResult.Row + 1
            End If
            Dim _chunks As List(Of cChunkInfo) = GetChunks(_startRow, True)
            While _chunks IsNot Nothing AndAlso _chunks.Count > 0
                For Each _chunk As cChunkInfo In _chunks
                    Dim _c = _chunk
                    If NewChunkOnlyOnLabel AndAlso _chunk.Label = "" AndAlso _retVal.Where(Function(c) c.Channel = _c.Channel).Count > 0 Then
                        _retVal.Last(Function(c) c.Channel = _c.Channel).Spots.AddRange(_chunk.Spots)
                    Else
                        _retVal.Add(_chunk)
                    End If

                Next
                _chunks = GetChunks(_startRow)
            End While
            Return _retVal
        End Function

        Public Function GetBudget(StartRow As Integer, Optional Channel As String = "") As Single
            For Each _search As cSearch In SpotlistRules.BudgetRules(0).Searches.Where(Function(s) s.ParentSearch Is Nothing)
                Dim _oldValue As String = _search.SearchValue
                If Channel <> "" Then _search.SearchValue = _search.SearchValue.Replace("%channel%", Channel)
                Dim _res = _search.Execute(_lastValidatedSchedule, New cSearchResult() With {.Succeeded = True, .Row = StartRow, .Column = 1})
                If _res.Succeeded Then
                    _search.SearchValue = _oldValue
                    Return _res.Result.Replace(" ", "").Replace(Chr(160), "").Replace("kr", "")
                End If
                _search.SearchValue = _oldValue
            Next
            Return 0
        End Function

        Dim _lastFoundRow As Integer
        Public Function GetLabel(StartRow As Integer, Optional Channel As String = "") As String
            For Each _search As cSearch In SpotlistRules.LabelRules(0).Searches.Where(Function(s) s.ParentSearch Is Nothing)
                Dim _oldValue As String = _search.SearchValue
                If Channel <> "" Then _search.SearchValue = _search.SearchValue.Replace("%channel%", Channel)
                If _search.Execute(_lastValidatedSchedule, New cSearchResult() With {.Succeeded = True, .Row = StartRow, .Column = 1}).Succeeded Then
                    Dim _res As cSearchResult = _search.Execute(_lastValidatedSchedule, New cSearchResult() With {.Succeeded = True, .Row = StartRow, .Column = 1})
                    If _res.Row <> _lastFoundRow Then
                        _lastFoundRow = _res.Row
                        Return _res.Result
                    End If
                End If
                _search.SearchValue = _oldValue
            Next
            Return ""
        End Function

        Private Function GetChunks(ByRef StartRow As Integer, Optional NoMultiSearch As Boolean = False) As List(Of cChunkInfo)
            Dim _startRow As Integer = StartRow

            If Not NoMultiSearch AndAlso Multi Then
                For Each _rule As cRule In HeadlineRowRules
                    For Each _search As cSearch In _rule.Searches.Where(Function(s) s.ParentSearch Is Nothing)
                        If _search.Execute(_lastValidatedSchedule, New cSearchResult() With {.Succeeded = True, .Row = StartRow, .Column = 1}).Succeeded Then
                            For Each _headline As cColumnHeadline In ColumnHeadlines.Values
                                _headline.Validate(_lastValidatedSchedule, _search.Result.Row)
                                If _headline.Required AndAlso Not _headline.ValidationResult.Succeeded Then
                                    HeadlineRowRules.ValidationResult.Succeeded = False
                                End If
                            Next
                            StartRow = _search.Result.Row + 1
                            _startRow = StartRow
                            Exit For
                        End If
                    Next
                Next
            End If

            Dim _list As List(Of cScheduleSpot) = GetChunkGrossList(StartRow)
            Dim _chunks As New List(Of cChunkInfo)
            'Divide into chunks

            For Each _c As Object In (From _s In _list Group _s By Key = _s.Channel Into Group Select New With {.Channel = Key, .List = Group})
                Dim _chunk As New cChunkInfo
                _chunk.Channel = _c.Channel
                _chunk.Spots = DirectCast(_c.List, ScheduleTemplates.cScheduleSpot()).ToList
                If _chunk.Spots.Sum(Function(s) s.NetPrice) = 0 Then
                    _chunk.Budget = GetBudget(_startRow - 1, _chunk.Channel)
                Else
                    _chunk.Budget = _chunk.Spots.Sum(Function(s) s.NetPrice)
                End If
                _chunk.Label = GetLabel(_startRow - 1)
                _chunks.Add(_chunk)
            Next
            _startRow += 1
            Return _chunks
        End Function

        Private Function GetChunkGrossList(ByRef StartRow As Integer) As List(Of cScheduleSpot)
            Dim _list As New List(Of cScheduleSpot)
            Dim _ended As Boolean = False
            Dim _row As Integer = StartRow
            Dim _sheet As cTemplateSheet = _lastValidatedSchedule.Sheets(UseSheet(_lastValidatedSchedule))
            Dim _en As New System.Globalization.CultureInfo("en-US")
            Do
                Dim _skip As Boolean = False
                For Each _search As cSearch In SpotlistRules.SkipRules(0).Searches.Where(Function(s) s.ParentSearch Is Nothing)
                    If _search.Execute(_lastValidatedSchedule, New cSearchResult() With {.Succeeded = True, .Row = _row, .Column = 1}).Succeeded Then
                        _skip = True
                        Exit For
                    End If
                Next
                For Each _search As cSearch In SpotlistRules.EndRules(0).Searches.Where(Function(s) s.ParentSearch Is Nothing)
                    If _search.Execute(_lastValidatedSchedule, New cSearchResult() With {.Succeeded = True, .Row = _row, .Column = 1}).Succeeded Then
                        _ended = True
                        Exit For
                    End If
                Next

                ' Find out if we have reached the end of the excel sheet
                If (StartRow > _sheet.Sheet.UsedRange.Row + _sheet.Sheet.UsedRange.Rows.Count) Then
                    _ended = True
                End If

                If Not _skip AndAlso Not _ended Then
                    Dim _spot As New cScheduleSpot
                    Try
                        _spot.Channel = GetColumnValue(_sheet, _row, "Channel", IIf(ChannelRules.ValidationResult.Succeeded, Translate(ChannelRules.ValidationResult.Result), ""))
                        _spot.Date = ParseDate(GetColumnValue(_sheet, _row, "Date", Nothing))
                        If _spot.Date = Nothing Then
                            Dim _year As Integer = GetColumnValue(_sheet, _row, "YEAR", 0)
                            Dim _month As Integer = GetColumnValue(_sheet, _row, "MONTH", 0)
                            Dim _day As Integer = GetColumnValue(_sheet, _row, "DAY", 0)
                            Try
                                Dim _date As New Date(_year, _month, _day)
                                _spot.Date = _date
                            Catch

                            End Try
                        End If
                        _spot.MaM = ParseTime(GetColumnValue(_sheet, _row, "Time", "00:00"))
                        If Not Integer.TryParse(GetColumnValue(_sheet, _row, "Length", 0), _spot.Length) Then
                            Dim _date As Date
                            If Date.TryParse(GetColumnValue(_sheet, _row, "Length", 0), _date) Then
                                _spot.Length = _date.Second
                            Else
                                Try
                                    _date = Date.FromOADate(GetColumnValue(_sheet, _row, "Length", 0))
                                    _spot.Length = _date.Second
                                Catch ex As Exception
                                    _spot.Length = 0
                                End Try
                            End If
                        End If
                        _spot.Filmcode = GetColumnValue(_sheet, _row, "Filmcode", IIf(_spot.Length > 0, _spot.Length, ""))

                        Decimal.TryParse(GetColumnValue(_sheet, _row, "GrossPrice", 0).ToString.Replace(" ", ""), System.Globalization.NumberStyles.Any, _en, _spot.GrossPrice)
                        Decimal.TryParse(GetColumnValue(_sheet, _row, "NetPrice", 0).ToString.Replace(" ", ""), System.Globalization.NumberStyles.Any, _en, _spot.NetPrice)
                        _spot.AddedValue = GetColumnValue(_sheet, _row, "AddedValue", "")
                        _spot.ProgAfter = GetColumnValue(_sheet, _row, "ProgAfter", "")
                        _spot.ProgBefore = GetColumnValue(_sheet, _row, "ProgBefore", "")
                        _spot.Program = GetColumnValue(_sheet, _row, "Program", _spot.ProgAfter)
                        _spot.Rating = GetToStringColumnValue(_sheet, _row, "Rating", 0)
                        _spot.Remark = GetColumnValue(_sheet, _row, "Remark", "")
                        _list.Add(_spot)
                    Catch

                    End Try
                End If
                _row += 1
                For Each _search As cSearch In SpotlistRules.EndRules(0).Searches.Where(Function(s) s.ParentSearch Is Nothing)
                    If _search.Execute(_lastValidatedSchedule, New cSearchResult() With {.Succeeded = True, .Row = _row, .Column = 1}).Succeeded Then
                        _ended = True
                        Exit For
                    End If
                Next
            Loop Until _row > _sheet.Rows OrElse _ended
            StartRow = _row
            Return _list
        End Function

        Private Function ParseDate(DateString As Object) As Date
            Dim _date As Date

            If DateString Is Nothing Then Return _date
            If DateString.GetType Is GetType(Date) Then Return DateString

            'Take away weekdays
            DateString = DateString.Replace("mon", "").Replace("tue", "").Replace("wed", "").Replace("thu", "").Replace("fri", "").Replace("sat", "").Replace("sun", "").Trim
            DateString = DateString.Replace("mån", "").Replace("tis", "").Replace("ons", "").Replace("tor", "").Replace("fre", "").Replace("lör", "").Replace("sön", "").Trim
            DateString = DateString.Replace("må", "").Replace("ti", "").Replace("on", "").Replace("to", "").Replace("fr", "").Replace("lö", "").Replace("sö", "").Trim

            For Each _format As String In DateFormats
                For Each _culture As CultureInfo In {New CultureInfo("sv-SE"), CultureInfo.InvariantCulture, CultureInfo.CurrentCulture}
                    If DateTime.TryParseExact(DateString, _format, _culture, DateTimeStyles.None, _date) Then
                        Return _date
                    End If
                Next
            Next
            If DateTime.TryParse(DateString, _date) Then
                Return _date
            End If
            Throw New Exception("Could not parse date: " & DateString)
        End Function

        Private Function ParseTime(Time As Object) As Integer
            If IsNumeric(Time) Then
                If Time < 2 Then
                    Return Time * 24 * 60
                End If
                Time = Time.ToString
            End If
            If Time.IndexOf(":") > 0 Then
                Dim tmpTimeValue = Time.Substring(0, Time.IndexOf(":")) * 60 + Time.Substring(Time.IndexOf(":") + 1, 2)
                If tmptimevalue > 0 And tmpTimeValue < 120
                    Return 1440 + Time.Substring(0, Time.IndexOf(":")) * 60 + Time.Substring(Time.IndexOf(":") + 1, 2)
                Else
                    Return Time.Substring(0, Time.IndexOf(":")) * 60 + Time.Substring(Time.IndexOf(":") + 1, 2)
                End If
            Else
                Return Time.Substring(0, 2) * 60 + Time.Substring(Time.Length - 2)
            End If
        End Function

        Private Function GetColumnValue(Sheet As cTemplateSheet, Row As Integer, ColumnName As String, DefaultValue As Object) As Object
            If ColumnHeadlines.ContainsKey(ColumnName.ToLower.Trim) Then
                If ColumnHeadlines(ColumnName.ToLower.Trim).ValidationResult.Succeeded Then
                    Dim _val = Sheet.Cells(Row, ColumnHeadlines(ColumnName.ToLower.Trim).ValidationResult.Column)
                    _val = Translate(_val)
                    If _val.GetType Is GetType(String) Then
                        If _val IsNot Nothing AndAlso _val.ToString.Length > ColumnHeadlines(ColumnName.ToLower.Trim).TrimStart Then
                            _val = _val.ToString.Substring(ColumnHeadlines(ColumnName.ToLower.Trim).TrimStart)
                        End If
                        If ColumnHeadlines(ColumnName.ToLower.Trim).TrimUntilFirst <> "" AndAlso _val.ToString.IndexOf(ColumnHeadlines(ColumnName.ToLower.Trim).TrimUntilFirst) >= 0 Then
                            _val = _val.ToString.Substring(_val.ToString.IndexOf(ColumnHeadlines(ColumnName.ToLower.Trim).TrimUntilFirst) + 1)
                        End If
                        If ColumnHeadlines(ColumnName.ToLower.Trim).TrimUntilLast <> "" AndAlso _val.ToString.IndexOf(ColumnHeadlines(ColumnName.ToLower.Trim).TrimUntilLast) >= 0 Then
                            _val = _val.ToString.Substring(_val.ToString.LastIndexOf(ColumnHeadlines(ColumnName.ToLower.Trim).TrimUntilLast) + 1)
                        End If
                    End If
                    Return _val
                End If
            End If
            Return DefaultValue
        End Function

        Private Function GetToStringColumnValue(ByVal Sheet As cTemplateSheet, ByVal Row As Integer, ByVal ColumnName As String, ByVal DefaultValue As Object) As Object
            If ColumnHeadlines.ContainsKey(ColumnName.ToLower.Trim) Then
                If ColumnHeadlines(ColumnName.ToLower.Trim).ValidationResult.Succeeded Then
                    Dim _val = Sheet.Cells(Row, ColumnHeadlines(ColumnName.ToLower.Trim).ValidationResult.Column)
                    Return _val.ToString()
                End If
            End If
            Return DefaultValue
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
            If _useSheet = "" Then
                If UseFirstSheetIfNotFound Then
                    _useSheet = Schedule.Sheets.Keys(0)
                End If
            End If
            Return _useSheet
        End Function

        Private Function _sheet() As Object
            Throw New NotImplementedException
        End Function

    End Class
End Namespace