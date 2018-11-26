Public Class XmlReader

    Shared Event Progress(p As Integer, Message As String)


    Shared Function Read(File As String) As List(Of Break)
        Dim _doc As XDocument

        If File.StartsWith("http") Then
            _doc = GetFromUrl(File)
        Else
            _doc = XDocument.Load(File)
        End If

        Dim _prevBreak As Break = Nothing
        Dim _list As New List(Of Break)

        Dim _rows As Integer = _doc.<tv>...<programme>.Count
        Dim _row As Integer = 0
        For Each _prog In _doc.<tv>...<programme>
            Dim _break As New Break(_prevBreak)
            Dim _start As String = _prog.@broadcast_date
            Dim _time As String = _prog.@broadcast_time

            _break.Channel = IIf(_prog.@channel = "TV4 Plus", "TV4+", _prog.@channel)
            '_break.Date = CDate(_start.Substring(0, 4) & "-" & _start.Substring(4, 2) & "-" & _start.Substring(6, 2))
            _break.Date = _start
            _break.Time = _time
            _break.Programme = _prog.<title>.Value
            _break.IsLocal = _prog.@is_regional
            _break.ChanEst = _prog.<estimate>.<rating>.Value.ToString.Replace(".", ",")
            _break.Price = _prog.<estimate>.<price>.Value

            _list.Add(_break)
            _prevBreak = _break
            _row += 1
            RaiseEvent Progress((_row / _rows) * 100, "Reading " & _break.Channel)
        Next
        _rows = _doc.<programplan>.<udsendelser>...<udsendelse>.Count
        _row = 0
        For Each _b In _doc.<programplan>.<udsendelser>...<udsendelse>
            Dim _break As New Break(_prevBreak)
            Dim _start As String = _b.<tidspunkt>.Value

            If _b.@udsendelseType = "B" OrElse _b.@udsendelseType = "T" Then
                _break.Channel = IIf(_b.Parent.@kanalId = 1, "TV 2", "TV 2 Zulu")
                _break.Date = CDate(_start).Date
                _break.Time = Format(CDate(_start).Hour, "00") & Format(CDate(_start).Minute, "00")
                Dim _prog As XElement = _b.NextNode
                While _prog IsNot Nothing AndAlso _prog.@udsendelseType <> "P"
                    _prog = _prog.NextNode
                End While
                If _prog IsNot Nothing Then
                    _break.Programme = _prog.<programInfo>.<titel>.Value
                End If
                _break.Addition = _b.<blokInfo>.<eftsp>.Value
                _break.EstimationTarget = _b.<blokInfo>.<maalgruppeKode>.Value
                _break.ChanEst = _b.<blokInfo>.<forventetTRP>.Value
                _break.UseCPP = True
                _break.Area = "DK"
                _list.Add(_break)
                _prevBreak = _break
            End If
            _row += 1
            RaiseEvent Progress((_row / _rows) * 100, "Reading " & _break.Channel)
        Next
        Return _list
    End Function

    Private Shared Function GetFromUrl(url As String) As XDocument
        Login()

    End Function

    Private Shared Sub Login()

    End Sub
End Class
