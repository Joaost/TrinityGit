Public Class TextReader
    Shared Event Progress(p As Integer, Message As String)

    Shared Function Read(File As String) As List(Of Break)
        Dim _prevBreak As Break = Nothing
        Dim _list As New List(Of Break)
        Dim BreakItIntoBreaks As Boolean = False
        Using _rd As New Microsoft.VisualBasic.FileIO.TextFieldParser(File)
            _rd.TextFieldType = FileIO.FieldType.Delimited
            _rd.SetDelimiters(vbTab)
            While Not _rd.EndOfData
                Dim _row As String() = _rd.ReadFields
                Dim _break As New Break(_prevBreak)
                If Date.TryParse(_row(0), _break.Date) Then

                    _break.Channel = IO.Path.GetFileName(File).Substring(0, IO.Path.GetFileName(File).IndexOf("-"))
                    ''_break.Channel = IO.Path.

                    'DNS customization if DNS are sending time where they include seconds.
                    _break.Time = _row(1).Replace(":", "")
                    'If _break.Time > 4
                    '    _break.Time = Left(_break.Time, 4)
                    '    _break.Programme = _row(2)
                    '    If _row(5).ToString <> ""
                    '        Single.TryParse(_row(5), _break.Price)
                    '        BreakItIntoBreaks = true
                    '    End If
                    'End if

                    If _row.Length = 3 Then
                        _break.Programme = _row(2)
                        'TLC mod
                        _break.Channel = "TLC"
                        BreakItIntoBreaks = True
                    ElseIf _row.Length = 7 And _break.Channel.Contains("TV") Then
                        _break.Programme = _row(2)
                        'Viasat
                        Single.TryParse(_row(5), _break.Price)
                        BreakItIntoBreaks = True

                    ElseIf _row.Length = 7 And _break.Channel.Contains("Kanal") Then
                        'SBS
                        Single.TryParse(_row(6), _break.Price)
                        Single.TryParse(_row(4), _break.ChanEst)
                        _break.Programme = _row(2)
                        '-------------------------------------------------------------
                        ' If the programe should read textfiles bigger than 7 columns - Add statement with a higher row.Length
                        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                        ' Following code was removed 2018-03-29:
                        ''ElseIf _row.Length = 8 Then
                        'ProSieben
                        ''Single.TryParse(_row(5), _break.ChanEst)
                        ''Single.TryParse(_row(6), _break.Price)
                        ''ElseIf _row.Length = 11 Then
                        ''   Single.TryParse(_row(5), _break.ChanEst)
                        ''   Single.TryParse(_row(6), _break.Price)
                        '-------------------------------------------------------------
                    End If


                    _prevBreak = _break
                    _list.Add(_break)

                End If
            End While
            _rd.Close()
        End Using
        If BreakItIntoBreaks Then
            _list = BreakIntoBreaks(_list)
        End If
        Return _list
    End Function

    Private Shared Function BreakIntoBreaks(Breaks As List(Of Break)) As List(Of Break)
        Dim _newList As New List(Of Break)

        For Each _b In Breaks
            Dim _price As Decimal = _b.Price
            Dim _est As Decimal = _b.ChanEst
            Dim _prog As String = _b.Programme
            Dim _nextBreak As Break = Nothing

            Dim index As Integer = Breaks.IndexOf(_b)
            index = index + 1
            If index >= Breaks.Count Then
                Return _newList
            End If
            _b._nextBreak = Breaks.Item(index)

            _b.Programme = _prog + " 1"
            _newList.Add(_b)
            If _b.Duration <= 30 Then
                Dim _newBreak As New Break(_b) With {.ChanEst = _est, .Channel = _b.Channel, .Date = _b.Date, .IsLocal = _b.IsLocal, .IsRB = _b.IsRB, .Price = _price, .Programme = _prog + " 2"}
                _newBreak.SetMaM(_b.MaM + 15)
                _newList.Add(_newBreak)
            Else
                Dim _i As Integer = 2
                For _m As Integer = _b.MaM + 15 To _b.MaM + _b.Duration - 1 Step 20
                    Dim _newBreak As New Break(_b) With {.ChanEst = _est, .Channel = _b.Channel, .Date = _b.Date, .IsLocal = _b.IsLocal, .IsRB = _b.IsRB, .Price = _price, .Programme = _prog & " " & _i}
                    _newBreak.SetMaM(_m)
                    _newList.Add(_newBreak)
                    _i += 1
                Next
            End If
        Next
        Return _newList
    End Function

End Class
