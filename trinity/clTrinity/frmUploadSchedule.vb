Imports System.Net
Imports System.IO


Public Class frmUploadSchedule

    Dim _breaks As New SortedDictionary(Of Object, Object)
    Dim _failedbreaks As New SortedDictionary(Of Object, Object)
    Dim _progs As New SortedDictionary(Of Object, Object)
    Dim _startDate As Date
    Dim _endDate As Date
    Dim _area As String = ""
    Dim browser As New System.Windows.Forms.WebBrowser


    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        If ofdOpen.ShowDialog = Windows.Forms.DialogResult.OK Then
            Select Case IO.Path.GetExtension(ofdOpen.FileName).ToUpper
                Case ".XML"
                    OpenXMLSchedule(ofdOpen.FileName)
                Case ".XLS", ".XLSX", ".TXT"
                    OpenExcelSchedule(ofdOpen.FileName)
            End Select
        End If
    End Sub

    Sub OpenXMLSchedule(ByVal Filename As String)
        Dim XMLDoc As New Xml.XmlDocument
        Try
            XMLDoc.Load(ofdOpen.FileName)
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("Could not open file." & vbCrLf & vbCrLf & "Reason: '" & ex.Message & "'", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            Exit Sub
        End Try

        If XMLDoc.GetElementsByTagName("programplan").Count > 0 Then
            ReadTV2DKSchedule(XMLDoc)
        ElseIf XMLDoc.GetElementsByTagName("tv").Count > 0 Then
            ReadTV4SESchedule(XMLDoc)
        Else
            Windows.Forms.MessageBox.Show("Unknown format. Could not import.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        End If
    End Sub

    Sub OpenExcelSchedule(ByVal Filename As String)
        Dim Excel As CultureSafeExcel.Application
        Dim WB As CultureSafeExcel.Workbook
        Try
            Excel = New CultureSafeExcel.Application(True)

            WB = Excel.OpenWorkbook(Filename)
            If Not WB.sheets(1).cells(1, 3).value Is Nothing AndAlso WB.Sheets(1).Range("C1").Value = "KorrigeretTitel" Then
                ReadSBSSchedule(WB)
            ElseIf (Not WB.Sheets(1).cells(1, 3).value Is Nothing AndAlso WB.sheets(1).range("C1").value.ToString.ToUpper.Contains("KANAL 5")) Or _
                        (Not WB.Sheets(1).cells(1, 2).value Is Nothing AndAlso WB.sheets(1).range("B1").value.ToString.ToUpper.Contains("KANAL 5")) Then
                ReadK5SESchedule(WB)
            ElseIf Campaign.Area = "NO" AndAlso WB.sheets(1).cells(1, 2) IsNot Nothing AndAlso WB.sheets(1).cells(1, 2).value = "Dato" Then
                ReadTV2NOSchedule(WB)
            Else
                Dim channelChoice As New frmMultipleChoice((From chan In Campaign.Channels Select chan Where chan.hasspecifics = True).ToArray, "Pick channel:")
                channelChoice.ShowDialog()
                If Not channelChoice.Result Is Nothing Then
                    Select Case DirectCast(channelChoice.Result, Trinity.cChannel).ChannelName
                        Case "TV3"
                            ReadMTGSESchedule(WB, "TV3")
                        Case "TV6"
                            ReadMTGSESchedule(WB, "TV6")
                        Case "TV8"
                            ReadMTGSESchedule(WB, "TV8")
                        Case Else : Windows.Forms.MessageBox.Show("Unknown format. Could not import.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                    End Select
                End If
            End If
            WB.Close()
            Excel.Quit()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("Runtime error:" & vbCrLf & ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            If Excel IsNot Nothing Then
                If WB IsNot Nothing Then
                    WB.close()
                End If
                Excel.Quit()
            End If
        End Try
    End Sub

    Sub ReadSBSSchedule(ByVal WB As Object)
        grdSchedule.Columns.Clear()
        _breaks.Clear()
        _progs.Clear()

        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Date", .ReadOnly = True, .Name = "colDate", .Width = 65})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Time", .ReadOnly = True, .Name = "colTime", .Width = 45})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Dur", .ReadOnly = True, .Name = "colDur", .Width = 45})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Channel", .ReadOnly = True, .Name = "colChan", .AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Programme", .ReadOnly = True, .Name = "colProg", .AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Est.Rating", .ReadOnly = True, .Name = "colEst"})

        _startDate = Date.FromOADate(0)
        _endDate = Date.FromOADate(0)
        With WB.Sheets(1)
            Dim _row As Integer = 2

            While .Cells(_row, 1).value IsNot Nothing
                Dim _date As Date = Date.FromOADate(CDate(.cells(_row, 1).Value).ToOADate + .cells(_row, 2).value)
                If _date < _startDate OrElse _startDate.ToOADate = 0 Then
                    _startDate = Date.FromOADate(Int(_date.ToOADate))
                End If
                If _date > _endDate Then
                    _endDate = Date.FromOADate(Int(_date.ToOADate))
                End If
                Dim _time As String = .cells(_row, 2).text
                Dim _name As String = .cells(_row, 3).value
                Dim _est As Single = .cells(_row, 5).value
                Dim _chan As String = .cells(_row, 6).value
                While _breaks.ContainsKey(_date)
                    _date = _date.AddSeconds(1)
                End While
                _breaks.Add(_date, New With {.Date = _date, .Time = _time, .Name = _name, .Est = _est, .Channel = _chan})
                _row += 1
            End While
            Dim _lastTime As Date = Nothing
            For Each _kv As KeyValuePair(Of Object, Object) In _breaks
                Dim TmpRow As Windows.Forms.DataGridViewRow = grdSchedule.Rows(grdSchedule.Rows.Add)
                TmpRow.Cells("colDate").Value = Format(_kv.Value.Date, "Short date")
                TmpRow.Cells("colTime").Value = _kv.Value.Time
                If _lastTime <> Nothing Then
                    grdSchedule.Rows(grdSchedule.Rows.Count - 2).Cells("colDur").Value = Hour(Date.FromOADate(_kv.Value.Date.ToOADate - _lastTime.ToOADate)) * 60 + Minute(Date.FromOADate(_kv.Value.Date.ToOADate - _lastTime.ToOADate))
                End If
                _lastTime = _kv.Value.Date
                TmpRow.Cells("colChan").Value = _kv.Value.Channel
                TmpRow.Cells("colProg").Value = _kv.Value.Name
                TmpRow.Cells("colEst").Value = _kv.Value.Est
                TmpRow.Tag = _kv.Key
            Next
        End With
        cmdImportToDB.Enabled = True
        AddHandler cmdImportToDB.Click, AddressOf ImportSBSSchedule
    End Sub

    Private Sub ImportSBSSchedule(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim _channels As New List(Of String)
        For Each _row As Windows.Forms.DataGridViewRow In grdSchedule.Rows
            If Not _channels.Contains(_row.Cells("colChan").Value) Then
                _channels.Add(_row.Cells("colChan").Value)
            End If
        Next
        For Each _channel As String In _channels
            If DBReader.QUERY("SELECT count(id) FROM events WHERE channel='" & _channel & "' and [date]>=" & _startDate.ToOADate & " and [date]<=" & _endDate.ToOADate) > 0 Then
                If Windows.Forms.MessageBox.Show("There are events during this period on " & _channel & "." & vbCrLf & vbCrLf & "Are you sure you want to replace them?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
                DBReader.QUERY("DELETE FROM events WHERE channel='" & _channel & "' and [date]>=" & _startDate.ToOADate & " and [date]<=" & _endDate.ToOADate)
            End If
        Next
        Dim frmProg As New frmProgress
        frmProg.Text = "Saving to database..."
        frmProg.Show()
        Dim r As Long
        For Each _row As Windows.Forms.DataGridViewRow In grdSchedule.Rows
            frmProg.Progress = (r / grdSchedule.Rows.Count) * 100
            Dim _id As String = CreateGUID()
            Dim _chan As String = _row.Cells("colChan").Value
            Dim _date As Long = Int(CDate(_row.Tag).ToOADate)
            Dim _time As String = _row.Cells("colTime").Value.ToString.Replace(":", "")
            Dim _mam As Long = Trinity.Helper.Tid2Mam(_time)
            Dim _dur As Integer = _row.Cells("colDur").Value
            Dim _name As String = _row.Cells("colProg").Value.ToString.Replace("'", "''")
            Dim _est As String
            If _row.Cells("colEst").Value IsNot Nothing Then
                _est = _row.Cells("colEst").Value.ToString.Replace(",", ".")
            Else
                _est = "0"
            End If

            'SQL = "INSERT INTO events (id,Channel,[Date],[Time],StartMam,Duration,[Name],ChanEst,[Type],EstimationPeriod,Area,EstimationTarget,Addition,UseCPP,Comment) VALUES " & _
            '                                     "('" & _id & "','" & _chan & "'," & _date & ",'" & _time & "'," & _mam & "," & _dur & ",'" & _name & "'," & _est & ",0,'-4fw','DK','" & _target & "'," & _addition & ",1,'" & _comment & "')"
            InsertInDB(_chan, _date, _time, _mam, _dur, _name, _est, 0, 0, 0, False, False, "-4fw", "", True, 0, "", "DK", "")
            r += 1
        Next
        frmProg.Close()
        RemoveHandler cmdImportToDB.Click, AddressOf ImportSBSSchedule
    End Sub

    Private Sub ReadTV2DKSchedule(ByVal XMLDoc As Xml.XmlDocument)
        _area = "DK"
        _breaks.Clear()
        _progs.Clear()

        grdSchedule.Columns.Clear()

        Dim _schedule As Xml.XmlElement = XMLDoc.GetElementsByTagName("udsendelser")(0)
        Dim _channel As Trinity.cChannel
        Select Case _schedule.GetAttribute("kanalId")
            Case 1
                _channel = Campaign.Channels("TV 2")
            Case 2
                _channel = Campaign.Channels("TV 2 Zulu")
            Case Else
                Windows.Forms.MessageBox.Show("Unknown Channel ID." & vbCrLf & "Could not read file.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                Exit Sub
        End Select
        _startDate = CDate(_schedule.GetAttribute("periodeStart"))
        _endDate = CDate(_schedule.GetAttribute("periodeSlut"))

        For Each _event As Xml.XmlElement In _schedule.ChildNodes
            Dim _date As Date = _event.GetElementsByTagName("tidspunkt")(0).InnerText
            Select Case _event.GetAttribute("udsendelseType")
                Case "B", "T"
                    Dim _info As Xml.XmlElement = _event.GetElementsByTagName("blokInfo")(0)
                    Dim _target As String
                    Select Case _info.GetElementsByTagName("maalgruppeKode")(0).InnerText
                        Case "AL"
                            _target = "12+"
                        Case "P4"
                            _target = "P 4-11"
                    End Select
                    Dim _est As Single
                    If _info.GetElementsByTagName("forventetTrp")(0).InnerText <> "" Then
                        _est = _info.GetElementsByTagName("forventetTrp")(0).InnerText.Replace(".", ",")
                    Else
                        _est = 0
                    End If
                    _breaks.Add(_date, New With {.Type = _event.GetAttribute("udsendelseType"), .Under18 = CInt(_info.GetElementsByTagName("under18")(0).InnerText), .Addition = CInt(_info.GetElementsByTagName("eftsp")(0).InnerText), .Target = _target, .Est = _est})
                Case "P"
                    Dim _info As Xml.XmlElement = _event.GetElementsByTagName("programInfo")(0)
                    _progs.Add(_date, New With {.Programme = _info.GetElementsByTagName("titel")(0).InnerText})
            End Select
        Next
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Date", .ReadOnly = True, .Name = "colDate", .Width = 65})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Time", .ReadOnly = True, .Name = "colTime", .Width = 45})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Dur", .ReadOnly = True, .Name = "colDur", .Width = 45})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Channel", .ReadOnly = True, .Name = "colChan", .AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Programme", .ReadOnly = True, .Name = "colProg", .AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Restriction", .ReadOnly = True, .Name = "colRestriction"})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Addition", .ReadOnly = True, .Name = "colAdd"})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Target", .ReadOnly = True, .Name = "colTarget"})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Est.Rating", .ReadOnly = True, .Name = "colEst"})

        Dim _lastTime As Date = Nothing
        For Each kv As KeyValuePair(Of Object, Object) In _progs
            With grdSchedule.Rows(grdSchedule.Rows.Add)
                .Cells("colDate").Value = Format(kv.Key, "Short date")
                .Cells("colTime").Value = Format(kv.Key, "HH:mm")
                .Cells("colChan").Value = _channel.ChannelName
                If _lastTime <> Nothing Then
                    grdSchedule.Rows(grdSchedule.Rows.Count - 2).Cells("colDur").Value = Hour(Date.FromOADate(kv.Key.ToOADate - _lastTime.ToOADate)) * 60 + Minute(Date.FromOADate(kv.Key.ToOADate - _lastTime.ToOADate))
                End If
                _lastTime = kv.Key
                .Cells("colProg").Value = kv.Value.Programme
                If _breaks.ContainsKey(kv.Key) Then
                    Dim _rest As String = ""
                    Select Case _breaks(kv.Key).Under18
                        Case 0
                            _rest = "No restriction"
                        Case 1
                            _rest = "No alcohol"
                        Case 2
                            _rest = "No alcohol, OTC or additives"
                    End Select
                    .Cells("colRestriction").Value = _rest
                    .Cells("colAdd").Value = _breaks(kv.Key).addition
                    .Cells("colTarget").Value = _breaks(kv.Key).Target
                    .Cells("colEst").Value = _breaks(kv.Key).Est
                End If
                .Tag = kv.Key
            End With
        Next
        cmdImportToDB.Enabled = True
        AddHandler cmdImportToDB.Click, AddressOf ImportTV2DKSchedule
    End Sub

    Private Sub ReadTV2NOSchedule(ByVal WB As Object)
        _area = "NO"
        _breaks.Clear()
        _failedbreaks.Clear()

        grdSchedule.Columns.Clear()

        Dim _row As Integer = 2
        With WB.Sheets(1)

            Dim _channel As String
            Dim _date As Date
            Dim _realDate As Date
            Dim _time As Integer
            Dim _est As Single
            Dim _prog As String

            While .Cells(_row, 1) IsNot Nothing AndAlso Not .cells(_row, 1).value = ""

                _channel = .cells(_row, 1).value
                _date = CDate(.cells(_row, 2).value)
                _realDate = CDate(.cells(_row, 2).value & " " & .cells(_row, 3).text & ":00")
                _time = Trinity.Helper.Tid2Mam(.cells(_row, 3).text)
                _est = .cells(_row, 4).value
                _prog = .cells(_row, 5).value

                If _time < 6 * 60 Then
                    _time += 24 * 60
                    _realDate = _realDate.AddDays(1)
                End If
                If Campaign.Channels(_channel) Is Nothing Then
                    _channel = _channel.Replace("TV2", "TV 2")
                    If Campaign.Channels(_channel) Is Nothing Then
                        Windows.Forms.MessageBox.Show("Could not find channel '" & _channel & "'")
                        Exit Sub
                    End If
                End If

                If Not _breaks.ContainsKey(_realDate) Then
                    _breaks.Add(_realDate, New With {.AirDate = _date, .Channel = _channel, .MaM = _time, .Est = _est, .Programme = _prog})
                    _row += 1
                Else
                    _failedbreaks.Add(_realDate, New With {.AirDate = _date, .Channel = _channel, .MaM = _time, .Est = _est, .Programme = _prog})
                    _row += 1
                End If

            End While
        End With

        If _failedbreaks.Count > 0 Then
            Dim errormessage As String = "The following lines were duplicates or had other problems: " & vbNewLine
            For Each break As Object In _failedbreaks.Values
                errormessage += break.airdate & " - " & break.programme & vbNewLine
            Next
            Windows.Forms.MessageBox.Show(errormessage)
        End If


        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Date", .ReadOnly = True, .Name = "colDate", .Width = 65})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Time", .ReadOnly = True, .Name = "colTime", .Width = 45})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Dur", .ReadOnly = True, .Name = "colDur", .Width = 45})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Channel", .ReadOnly = True, .Name = "colChan", .AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Programme", .ReadOnly = True, .Name = "colProg", .AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Est.Rating", .ReadOnly = True, .Name = "colEst"})

        Dim _lastTime As Date = Nothing
        For Each kv As KeyValuePair(Of Object, Object) In _breaks
            With grdSchedule.Rows(grdSchedule.Rows.Add)
                .Cells("colDate").Value = Format(kv.Value.AirDate, "Short date")
                .Cells("colTime").Value = Trinity.Helper.Mam2Tid(kv.Value.MaM)
                .Cells("colChan").Value = kv.Value.Channel
                If _lastTime <> Nothing Then
                    grdSchedule.Rows(grdSchedule.Rows.Count - 2).Cells("colDur").Value = Hour(Date.FromOADate(kv.Key.ToOADate - _lastTime.ToOADate)) * 60 + Minute(Date.FromOADate(kv.Key.ToOADate - _lastTime.ToOADate))
                End If
                _lastTime = kv.Key
                .Cells("colProg").Value = kv.Value.Programme
                .Cells("colEst").Value = kv.Value.Est
                .Tag = kv.Value
            End With
        Next
        cmdImportToDB.Enabled = True
        AddHandler cmdImportToDB.Click, AddressOf ImportTV2NOSchedule
    End Sub


    Private Sub frmUploadSchedule_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    Private Sub ImportTV2DKSchedule(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim _channels As New List(Of String)
        For Each _row As Windows.Forms.DataGridViewRow In grdSchedule.Rows
            If Not _channels.Contains(_row.Cells("colChan").Value) Then
                _channels.Add(_row.Cells("colChan").Value)
            End If
        Next
        For Each _channel As String In _channels
            If DBReader.QUERY("SELECT count(id) FROM events WHERE channel='" & _channel & "' and [date]>=" & _startDate.ToOADate & " and [date]<=" & _endDate.ToOADate) > 0 Then
                If Windows.Forms.MessageBox.Show("There are events during this period on " & _channel & "." & vbCrLf & vbCrLf & "Are you sure you want to replace them?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
            End If
            DBReader.QUERY("DELETE FROM events WHERE channel='" & _channel & "' and [date]>=" & _startDate.ToOADate & " and [date]<=" & _endDate.ToOADate)
        Next
        Dim frmProg As New frmProgress
        frmProg.Text = "Saving to database..."
        frmProg.Show()
        Dim r As Long
        For Each _row As Windows.Forms.DataGridViewRow In grdSchedule.Rows
            frmProg.Progress = (r / grdSchedule.Rows.Count) * 100
            Dim _id As String = CreateGUID()
            Dim _chan As String = _row.Cells("colChan").Value
            Dim _date As Long = Int(CDate(_row.Tag).ToOADate)
            Dim _time As String = _row.Cells("colTime").Value
            Dim _mam As Long = Trinity.Helper.Tid2Mam(_time)
            Dim _dur As Integer = _row.Cells("colDur").Value
            Dim _name As String = _row.Cells("colProg").Value.ToString.Replace("'", "''")
            Dim _est As String
            If _row.Cells("colEst").Value IsNot Nothing Then
                _est = _row.Cells("colEst").Value.ToString.Replace(",", ".")
            Else
                _est = "0"
            End If
            Dim _target As String = _row.Cells("colTarget").Value
            Dim _addition As Integer = _row.Cells("colAdd").Value
            Dim _comment As String = _row.Cells("colRestriction").Value

            'SQL = "INSERT INTO events (id,Channel,[Date],[Time],StartMam,Duration,[Name],ChanEst,[Type],EstimationPeriod,Area,EstimationTarget,Addition,UseCPP,Comment) VALUES " & _
            '                                     "('" & _id & "','" & _chan & "'," & _date & ",'" & _time & "'," & _mam & "," & _dur & ",'" & _name & "'," & _est & ",0,'-4fw','DK','" & _target & "'," & _addition & ",1,'" & _comment & "')"
            InsertInDB(_chan, _date, _time, _mam, _dur, _name, _est, 0, 0, 0, False, False, "-4fw", "", True, _addition, _target, "DK", _comment)
            r += 1
        Next
        frmProg.Close()
        grdSchedule.Columns.Clear()
        cmdImportToDB.Enabled = False
        RemoveHandler cmdImportToDB.Click, AddressOf ImportTV2DKSchedule
    End Sub

    Private Sub ImportTV2NOSchedule(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim _channels As New List(Of String)
        For Each _row As Windows.Forms.DataGridViewRow In grdSchedule.Rows
            If Not _channels.Contains(_row.Cells("colChan").Value) Then
                _channels.Add(_row.Cells("colChan").Value)
            End If
        Next
        For Each _channel As String In _channels
            If DBReader.QUERY("SELECT count(id) FROM events WHERE channel='" & _channel & "' and [date]>=" & _startDate.ToOADate & " and [date]<=" & _endDate.ToOADate) > 0 Then
                If Windows.Forms.MessageBox.Show("There are events during this period on " & _channel & "." & vbCrLf & vbCrLf & "Are you sure you want to replace them?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
            End If
            DBReader.QUERY("DELETE FROM events WHERE channel='" & _channel & "' and [date]>=" & _startDate.ToOADate & " and [date]<=" & _endDate.ToOADate)
        Next
        Dim frmProg As New frmProgress
        frmProg.Text = "Saving to database..."
        frmProg.Show()
        Dim r As Long
        For Each _row As Windows.Forms.DataGridViewRow In grdSchedule.Rows
            frmProg.Progress = (r / grdSchedule.Rows.Count) * 100
            Dim _id As String = CreateGUID()
            Dim _chan As String = _row.Cells("colChan").Value
            Dim _date As Long = Int(CDate(_row.Tag.AirDate).ToOADate)
            Dim _time As String = _row.Cells("colTime").Value
            Dim _mam As Long = Trinity.Helper.Tid2Mam(_time)
            Dim _dur As Integer = _row.Cells("colDur").Value
            Dim _name As String = _row.Cells("colProg").Value.ToString.Replace("'", "''")
            Dim _est As String
            If _row.Cells("colEst").Value IsNot Nothing Then
                _est = _row.Cells("colEst").Value.ToString.Replace(",", ".")
            Else
                _est = "0"
            End If

            'SQL = "INSERT INTO events (id,Channel,[Date],[Time],StartMam,Duration,[Name],ChanEst,[Type],EstimationPeriod,Area,EstimationTarget,Addition,UseCPP,Comment) VALUES " & _
            '                                     "('" & _id & "','" & _chan & "'," & _date & ",'" & _time & "'," & _mam & "," & _dur & ",'" & _name & "'," & _est & ",0,'-4fw','DK','" & _target & "'," & _addition & ",1,'" & _comment & "')"
            InsertInDB(_chan, _date, _time, _mam, _dur, _name, _est, 0, 0, 0, False, False, "-4fw", "", True, 0, "12+", "NO", "")
            r += 1
        Next
        frmProg.Close()
        grdSchedule.Columns.Clear()
        cmdImportToDB.Enabled = False
        RemoveHandler cmdImportToDB.Click, AddressOf ImportTV2DKSchedule
    End Sub

    Private Sub ReadTV4SESchedule(ByVal XMLDoc As Xml.XmlDocument)

        grdSchedule.Rows.Clear()
        grdSchedule.Columns.Clear()

        Dim breakList As New Collection
        Dim breakList_errors As New Collection


        Dim rootNode As Xml.XmlElement = XMLDoc.GetElementsByTagName("tv")(0)
        Dim titleNode As Xml.XmlElement
        Dim estimateNode As Xml.XmlElement

        Dim lastBreak As Planned_Break = Nothing

        For Each programme As XmlElement In rootNode.ChildNodes

            Try

                Dim Break As New Planned_Break


                Dim tmpDateString As String = Strings.Left(programme.GetAttribute("start"), 8)
                Break.AirDate = DateSerial(tmpDateString.Substring(0, 4), tmpDateString.Substring(0, 6).Substring(4, 2), tmpDateString.Substring(6, 2))
                Dim Hours As Integer = CInt(programme.GetAttribute("start").ToString.Substring(8, 4).Substring(0, 2))
                Dim Minutes As Integer = CInt(programme.GetAttribute("start").ToString.Substring(8, 4).Substring(2, 2))
                Break.MaM = Hours * 60 + Minutes
                ' If Break.AirDate.Month = 6 Then Stop
                Break.AirDate = Break.AirDate.AddMinutes(Hours * 60 + Minutes)

                If Break.MaM < 360 Then
                    Break.AirDate = Break.AirDate.AddDays(-1)
                    Break.MaM += 1440
                End If
                Break.Channel = programme.GetAttribute("channel")
                Break.Sequence_number = programme.GetAttribute("sequence_no")
                Break.ID_number = programme.GetAttribute("id_no")
                Break.Is_Regional = programme.GetAttribute("is_regional")
                If Not lastBreak Is Nothing Then
                    Break.Duration = Math.Abs(DateDiff(DateInterval.Minute, lastBreak.AirDate, Break.AirDate))
                Else
                    _startDate = Break.AirDate
                    Break.Duration = 0
                End If



                titleNode = programme.GetElementsByTagName("title")(0)

                Break.Title = titleNode.InnerXml
                Break.Language = titleNode.GetAttribute("lang")

                estimateNode = programme.GetElementsByTagName("estimate")(0)

                Break.TargetGroup = "A12-59" ' estimateNode.GetAttribute("targetgroup")
                Break.Est_rating = estimateNode.GetElementsByTagName("rating")(0).InnerXml 'programme.GetAttribute("rating")
                Break.Price = estimateNode.GetElementsByTagName("price")(0).InnerXml 'programme.GetAttribute("price")
                Break.CPT = estimateNode.GetElementsByTagName("cpt")(0).InnerXml '   programme.GetAttribute("CPT")
                Break.CPP = estimateNode.GetElementsByTagName("cpt")(0).InnerXml  'programme.GetAttribute("CPP")
                Break.Dirr = estimateNode.GetElementsByTagName("dirr")(0).InnerXml 'programme.GetAttribute("dirr")

                breakList.Add(Break)
                lastBreak = Break

            Catch ex As Exception
                breakList_errors.Add(breakList)
            End Try

        Next

        _endDate = lastBreak.AirDate

        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Date", .ReadOnly = True, .Name = "colDate", .Width = 65})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Time", .ReadOnly = True, .Name = "colTime", .Width = 45})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Dur", .ReadOnly = True, .Name = "colDur", .Width = 45})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Channel", .ReadOnly = True, .Name = "colChan", .AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Programme", .ReadOnly = True, .Name = "colProg", .AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Target", .ReadOnly = True, .Name = "colTarget"})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Price", .ReadOnly = True, .Name = "colPrice"})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Est.Rating", .ReadOnly = True, .Name = "colEst"})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Local", .ReadOnly = True, .Name = "colIsLocal"})



        For Each break As Planned_Break In breakList

            With grdSchedule.Rows(grdSchedule.Rows.Add)
                .Tag = break
                .Cells("colDate").Value = Format(break.AirDate, "Short date")
                .Cells("colTime").Value = Format(break.AirDate, "HH:mm")
                .Cells("colDur").Value = break.Duration
                .Cells("colChan").Value = break.Channel
                .Cells("colProg").Value = break.Title
                .Cells("colTarget").Value = break.TargetGroup
                .Cells("colPrice").Value = break.Price
                .Cells("colEst").Value = break.Est_rating
                Select Case break.Is_Regional
                    Case True : .Cells("colIsLocal").Value = "Lokal"
                    Case False : .Cells("colIsLocal").Value = ""
                End Select
            End With
        Next
        cmdImportToDB.Enabled = True
        AddHandler cmdImportToDB.Click, AddressOf ImportTV4SESchedule
    End Sub

    Private Sub ImportTV4SESchedule(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim _channels As New List(Of String)
        For Each _row As Windows.Forms.DataGridViewRow In grdSchedule.Rows
            If Not _channels.Contains(_row.Cells("colChan").Value) Then
                _channels.Add(_row.Cells("colChan").Value)
            End If
        Next
        For Each _channel As String In _channels
            Dim breakCount As Integer = DBReader.QUERY("SELECT count(id) FROM events WHERE channel='" & _channel & "' and [date]>=" & Int(_startDate.ToOADate) & " and [date]<=" & Int(_endDate.ToOADate))
            If breakCount > 0 Then
                If Windows.Forms.MessageBox.Show("There " & breakCount & " breaks in the database already during this period on " & _channel & "." & vbCrLf & vbCrLf & "Are you sure you want to replace them?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
            End If
            DBReader.QUERY("DELETE FROM events WHERE channel='" & _channel & "' and [date]>=" & Int(_startDate.ToOADate) & " and [date]<=" & Int(_endDate.ToOADate))
        Next
        Dim frmProg As New frmProgress
        frmProg.Text = "Saving to database..."
        frmProg.Show()
        Dim r As Long
        For Each _row As Windows.Forms.DataGridViewRow In grdSchedule.Rows
            frmProg.Progress = (r / grdSchedule.Rows.Count) * 100
            Dim _id As String = CreateGUID()
            Dim _chan As String = _row.Cells("colChan").Value
            Dim _date As Long = Int(DirectCast(_row.Tag, Planned_Break).AirDate.ToOADate)
            Dim _time As String = _row.Cells("colTime").Value.ToString.Replace(":", "")
            Dim _mam As Long = DirectCast(_row.Tag, Planned_Break).MaM
            Dim _dur As Integer = _row.Cells("colDur").Value
            Dim _name As String = _row.Cells("colProg").Value.ToString.Replace("'", "''")
            'If _name.Contains("Bonnier") Then Stop
            Dim _est As String = _row.Cells("colEst").Value
            Dim _price As String = _row.Cells("colPrice").Value
            Dim _local As Boolean = _row.Cells("colIsLocal").Value = "Lokal"
            If _row.Cells("colEst").Value IsNot Nothing Then
                '_est = _row.Cells("colEst").Value.ToString.Replace(".", ",")
            Else
                '_est = "0"
            End If
            Dim _target As String = _row.Cells("colTarget").Value

            InsertInDB(_chan, _date, _time, _mam, _dur, _name, _est, _price, 0, 0, _local, 0, "-4fw", "", False, 0, _target, "SE", "")
            r += 1
        Next
        frmProg.Close()
        grdSchedule.Columns.Clear()
        cmdImportToDB.Enabled = False
        RemoveHandler cmdImportToDB.Click, AddressOf ImportTV4SESchedule
    End Sub

    Sub InsertInDB(ByVal Channel As String, ByVal [Date] As String, ByVal Time As String, ByVal StartMam As Long, ByVal Duration As Long, ByVal Name As String, ByVal ChanEst As String, ByVal Price As Decimal, ByVal Type As Integer, ByVal SubType As Integer, ByVal IsLocal As Boolean, ByVal IsRB As Boolean, ByVal EstimationPeriod As String, ByVal DateString As String, ByVal UseCPP As Boolean, ByVal Addition As Single, ByVal EstimationTarget As String, ByVal Area As String, ByVal Comment As String)
        Dim ID As String = CreateGUID()
        Dim SQL As String = "INSERT INTO events ([ID],[Channel],[Date],[Time],[StartMam],[Duration],[Name],[ChanEst],[Price],[Type],[SubType],[IsLocal],[IsRB],[EstimationPeriod],[DateString],[UseCPP],[Addition],[EstimationTarget],[Area],[Comment]) VALUES " & _
            "('" & [ID] & "','" & [Channel] & "'," & [Date] & ",'" & [Time] & "'," & [StartMam] & "," & [Duration] & ",'" & [Name] & "'," & [ChanEst] & "," & [Price] & "," & [Type] & "," & [SubType] & "," & CInt(IsLocal) & "," & CInt([IsRB]) & ",'" & [EstimationPeriod] & "','" & [DateString] & "'," & CInt([UseCPP]) & "," & [Addition] & ",'" & [EstimationTarget] & "','" & [Area] & "','" & [Comment] & "')"
        DBReader.QUERY(SQL)
    End Sub

    Private Sub ReadK5SESchedule(ByVal WB As Microsoft.Office.Interop.Excel.Workbook)

        grdSchedule.Rows.Clear()
        grdSchedule.Columns.Clear()

        Dim breakList As New Collection
        Dim breakList_errors As New Collection

        Dim lastBreak As Planned_Break = Nothing

        With WB.Sheets(1)

            Dim Break As New Planned_Break
            Dim currentRow As Integer = 1

            While .cells(currentRow, 1).text.ToString.ToUpper <> "DATUM" AndAlso .cells(currentRow, 1).text.ToString.ToUpper <> "DATE"
                currentRow += 1
            End While

            currentRow += 1

            Dim progress As New frmProgress
            progress.Show()
            Dim sheet As Microsoft.Office.Interop.Excel.Worksheet = WB.Sheets(1)
            frmProgress.MaxValue = sheet.UsedRange.Rows.Count

            While .cells(currentRow, 1).text <> ""

                progress.Progress += 1

                Break = New Planned_Break

                Dim tmpDateString As String = .cells(currentRow, 1).value
                Dim time As Double
                Break.AirDate = CDate(tmpDateString)
                'Dim Hours As Integer = CInt(Strings.Left(.cells(currentRow, 2).value.ToString, 2))
                'Dim Minutes As Integer = CInt(Strings.Right(.cells(currentRow, 2).value.ToString, 2))
                time = .cells(currentRow, 2).value
                Break.MaM = DateTime.FromOADate(time).Hour * 60 + DateTime.FromOADate(time).Minute
                Break.AirDate = Break.AirDate.AddMinutes(Break.MaM)

                If Break.MaM < 120 Then Break.MaM += 1440
                If Break.MaM >= 120 And Break.MaM < 360 Then
                    Break.AirDate = Break.AirDate.AddDays(-1)
                    Break.MaM += 1440
                End If
                Break.Channel = "Kanal5"
                Break.Sequence_number = ""
                Break.ID_number = ""
                Break.Is_Regional = False
                If Not lastBreak Is Nothing Then
                    Break.Duration = Math.Abs(DateDiff(DateInterval.Minute, lastBreak.AirDate, Break.AirDate))
                Else
                    _startDate = Break.AirDate
                    Break.Duration = 0
                End If

                Break.Title = .cells(currentRow, 3).value
                Break.Language = "SE"


                Break.TargetGroup = "A15-44" ' estimateNode.GetAttribute("targetgroup")
                Break.Est_rating = .cells(currentRow, 6).value
                Break.Price = .cells(currentRow, 7).value
                Break.CPT = ""
                Break.CPP = ""
                Break.Dirr = ""
                Break.Remark = .cells(currentRow, 4).value

                breakList.Add(Break)
                lastBreak = Break
                currentRow += 1
            End While
            progress.Close()
            progress = Nothing
        End With

        _endDate = lastBreak.AirDate

        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Date", .ReadOnly = True, .Name = "colDate", .Width = 65})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Time", .ReadOnly = True, .Name = "colTime", .Width = 45})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Dur", .ReadOnly = True, .Name = "colDur", .Width = 45})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Channel", .ReadOnly = True, .Name = "colChan", .AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Programme", .ReadOnly = True, .Name = "colProg", .AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Target", .ReadOnly = True, .Name = "colTarget"})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Price", .ReadOnly = True, .Name = "colPrice"})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Est.Rating", .ReadOnly = True, .Name = "colEst"})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Remark", .ReadOnly = True, .Name = "colRemark"})



        For Each break As Planned_Break In breakList

            With grdSchedule.Rows(grdSchedule.Rows.Add)
                .Tag = break
                .Cells("colDate").Value = Format(break.AirDate, "Short date")
                .Cells("colTime").Value = Format(break.AirDate, "HH:mm")
                .Cells("colDur").Value = break.Duration
                .Cells("colChan").Value = break.Channel
                .Cells("colProg").Value = break.Title
                .Cells("colTarget").Value = break.TargetGroup
                .Cells("colPrice").Value = break.Price
                .Cells("colEst").Value = break.Est_rating
                .Cells("colRemark").Value = break.Remark
            End With
        Next
        cmdImportToDB.Enabled = True
        AddHandler cmdImportToDB.Click, AddressOf ImportK5SESchedule
    End Sub

    Private Sub ImportK5SESchedule(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim _channels As New List(Of String)
        For Each _row As Windows.Forms.DataGridViewRow In grdSchedule.Rows
            If Not _channels.Contains(_row.Cells("colChan").Value) Then
                _channels.Add(_row.Cells("colChan").Value)
            End If
        Next
        For Each _channel As String In _channels
            Dim breakCount As Integer = DBReader.QUERY("SELECT count(id) FROM events WHERE channel='" & _channel & "' and [date]>=" & Int(_startDate.ToOADate) & " and [date]<=" & Int(_endDate.ToOADate))
            If breakCount > 0 Then
                If Windows.Forms.MessageBox.Show("There " & breakCount & " breaks in the database already during this period on " & _channel & "." & vbCrLf & vbCrLf & "Are you sure you want to replace them?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
            End If
            DBReader.QUERY("DELETE FROM events WHERE channel='" & _channel & "' and [date]>=" & Int(_startDate.ToOADate) & " and [date]<=" & Int(_endDate.ToOADate))
        Next
        Dim frmProg As New frmProgress
        frmProg.Text = "Saving to database..."
        frmProg.Show()
        Dim r As Long
        For Each _row As Windows.Forms.DataGridViewRow In grdSchedule.Rows
            frmProg.Progress = (r / grdSchedule.Rows.Count) * 100
            Dim _id As String = CreateGUID()
            Dim _chan As String = _row.Cells("colChan").Value
            Dim _date As Long = Int(DirectCast(_row.Tag, Planned_Break).AirDate.ToOADate)
            Dim _mam As Long = DirectCast(_row.Tag, Planned_Break).MaM
            Dim _time As String = Trinity.Helper.Mam2Tid(_mam)
            Dim _dur As Integer = _row.Cells("colDur").Value
            Dim _name As String = _row.Cells("colProg").Value.ToString.Replace("'", "''")
            'If _name.Contains("Bonnier") Then Stop
            'Dim est As String = _row.Cells("colEst").Value.ToString.Replace(",", ".")
            Dim _est As String = _row.Cells("colEst").Value.ToString.Replace(",", ".")
            Dim _price As String = _row.Cells("colPrice").Value
            Dim _local As Boolean = False
            If _row.Cells("colEst").Value IsNot Nothing Then
                '_est = _row.Cells("colEst").Value.ToString.Replace(".", ",")
            Else
                '_est = "0"
            End If
            Dim _target As String = _row.Cells("colTarget").Value
            Dim _remark As String = DirectCast(_row.Tag, Planned_Break).Remark '.Cells("colRemark").Value
            InsertInDB(_chan, _date, _time, _mam, _dur, _name, _est, _price, 0, 0, _local, 0, "-4fw", "", False, 0, _target, "SE", _remark)
            r += 1
        Next
        frmProg.Close()
        grdSchedule.Columns.Clear()
        cmdImportToDB.Enabled = False
        RemoveHandler cmdImportToDB.Click, AddressOf ImportK5SESchedule
    End Sub

    Private Sub ReadMTGSESchedule(ByVal WB As Microsoft.Office.Interop.Excel.Workbook, ByVal Channel As String)

        grdSchedule.Rows.Clear()
        grdSchedule.Columns.Clear()

        Dim breakList As New Collection
        Dim breakList_errors As New Collection

        Dim lastBreak As Planned_Break = Nothing

        With WB.Sheets(1)

            Dim Break As New Planned_Break
            Dim currentRow As Integer = 1

            While .cells(currentRow, 1).text <> ""

                Break = New Planned_Break

                Dim tmpDateString As String = .cells(currentRow, 1).value

                Break.AirDate = CDate(tmpDateString)
                Dim Time As Double = .cells(currentRow, 2).value
                Dim dt As DateTime = DateTime.FromOADate(Time)
                Dim Hours As Integer = dt.Hour
                Dim Minutes As Integer = dt.Minute
                Break.MaM = Hours * 60 + Minutes
                Break.AirDate = Break.AirDate.AddMinutes(Hours * 60 + Minutes)

                If Break.MaM < 6 * 60 Then
                    Break.MaM += 1440
                    Break.AirDate = Break.AirDate.AddDays(1)
                End If

                Break.Channel = Channel
                Break.Sequence_number = ""
                Break.ID_number = ""
                Break.Is_Regional = False
                If Not lastBreak Is Nothing Then
                    lastBreak.Duration = Math.Abs(DateDiff(DateInterval.Minute, lastBreak.AirDate, Break.AirDate))
                Else
                    _startDate = Break.AirDate
                    Break.Duration = 0
                End If

                Break.Title = .cells(currentRow, 3).value
                Break.Language = "SE"


                Break.TargetGroup = "A15-44" ' estimateNode.GetAttribute("targetgroup")
                'Break.Est_rating = .cells(currentRow, 6).value
                Break.Price = .cells(currentRow, 6).value
                Break.CPT = ""
                Break.CPP = ""
                Break.Dirr = ""
                Break.Remark = .cells(currentRow, 7).value

                breakList.Add(Break)
                lastBreak = Break
                currentRow += 1
            End While
        End With

        _endDate = lastBreak.AirDate

        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Date", .ReadOnly = True, .Name = "colDate", .Width = 65})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Time", .ReadOnly = True, .Name = "colTime", .Width = 45})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Dur", .ReadOnly = True, .Name = "colDur", .Width = 45})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Channel", .ReadOnly = True, .Name = "colChan", .AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Programme", .ReadOnly = True, .Name = "colProg", .AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Price", .ReadOnly = True, .Name = "colPrice"})
        grdSchedule.Columns.Add(New Windows.Forms.DataGridViewTextBoxColumn With {.HeaderText = "Remark", .ReadOnly = True, .Name = "colRemark"})

        For Each break As Planned_Break In breakList

            With grdSchedule.Rows(grdSchedule.Rows.Add)
                .Tag = break
                .Cells("colDate").Value = Format(break.AirDate, "yyyy-MM-dd")
                .Cells("colTime").Value = Format(break.AirDate, "HH:mm")
                .Cells("colDur").Value = break.Duration
                .Cells("colChan").Value = break.Channel
                .Cells("colProg").Value = break.Title
                .Cells("colPrice").Value = break.Price
                .Cells("colRemark").Value = break.Remark
            End With
        Next
        cmdImportToDB.Enabled = True
        AddHandler cmdImportToDB.Click, AddressOf ImportMTGSESchedule
    End Sub

    Private Sub ImportMTGSESchedule(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim _channels As New List(Of String)
        For Each _row As Windows.Forms.DataGridViewRow In grdSchedule.Rows
            If Not _channels.Contains(_row.Cells("colChan").Value) Then
                _channels.Add(_row.Cells("colChan").Value)
            End If
        Next
        For Each _channel As String In _channels
            Dim breakCount As Integer = DBReader.QUERY("SELECT count(id) FROM events WHERE channel='" & _channel & "' and [date]>=" & Int(_startDate.ToOADate) & " and [date]<=" & Int(_endDate.ToOADate))
            If breakCount > 0 Then
                If Windows.Forms.MessageBox.Show("There " & breakCount & " breaks in the database already during this period on " & _channel & "." & vbCrLf & vbCrLf & "Are you sure you want to replace them?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
            End If
            DBReader.QUERY("DELETE FROM events WHERE channel='" & _channel & "' and [date]>=" & Int(_startDate.ToOADate) & " and [date]<=" & Int(_endDate.ToOADate))
        Next
        Dim frmProg As New frmProgress
        frmProg.Text = "Saving to database..."
        frmProg.Show()
        Dim r As Long
        For Each _row As Windows.Forms.DataGridViewRow In grdSchedule.Rows
            frmProg.Progress = (r / grdSchedule.Rows.Count) * 100
            Dim _id As String = CreateGUID()
            Dim _chan As String = _row.Cells("colChan").Value
            Dim _date As Long
            If DirectCast(_row.Tag, Planned_Break).MaM >= 24 * 60 Then
                _date = Int(DirectCast(_row.Tag, Planned_Break).AirDate.AddDays(-1).ToOADate)
            Else
                _date = Int(DirectCast(_row.Tag, Planned_Break).AirDate.ToOADate)
            End If
            Dim _time As String = _row.Cells("colTime").Value.ToString.Replace(":", "")
            Dim _mam As Long = DirectCast(_row.Tag, Planned_Break).MaM
            Dim _dur As Integer = _row.Cells("colDur").Value
            Dim _name As String = _row.Cells("colProg").Value.ToString.Replace("'", "''")
            'If _name.Contains("Bonnier") Then Stop
            Dim _price As String = _row.Cells("colPrice").Value
            Dim _local As Boolean = False

            Dim _remark As String = DirectCast(_row.Tag, Planned_Break).Remark '.Cells("colRemark").Value
            InsertInDB(_chan, _date, _time, _mam, _dur, _name, 0, _price, 0, 0, _local, 0, "-4fw", "", False, 0, "''", "SE", _remark)
            r += 1
        Next
        frmProg.Close()
        grdSchedule.Columns.Clear()
        cmdImportToDB.Enabled = False
        RemoveHandler cmdImportToDB.Click, AddressOf ImportMTGSESchedule
    End Sub

    Private Sub EnterCredentialsAndGo(ByVal sender As Object, ByVal e As Windows.Forms.WebBrowserDocumentCompletedEventArgs)

        Dim doc As mshtml.HTMLDocument = sender.Document.DomDocument

        'Dim browser As New System.Windows.Forms.WebBrowser
        'browser.Show()

        doc.getElementById("TxtUserName").value = "hannes.falth@mecglobal.com"
        doc.getElementById("TxtPwd").value = "w0rmsign"

        doc.Login.submit()

    End Sub

    Private Sub cmdSynchAllTV4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSynchAllTV4.Click


        browser = New System.Windows.Forms.WebBrowser
        browser.ScriptErrorsSuppressed = True

        Windows.Forms.MessageBox.Show(browser.Version.ToString)

        AddHandler browser.DocumentCompleted, AddressOf EnterCredentialsAndGo

        Dim startDateString As String = Format(tmPckStart.Value, "yyyy-MM-dd")
        Dim endDateString As String = Format(tmPckEnd.Value, "yyyy-MM-dd")

        Dim URL As String = "http://online.tv4.se/Release/XMLChart.aspx?channel=1&startdate=" & startDateString & "&enddate=" & endDateString

        browser.Navigate(URL)


        



        



        'Dim request As HttpWebRequest = WebRequest.Create(URL)
        'Dim response As HttpWebResponse = request.GetResponse()
        'Dim reader As StreamReader = New StreamReader(response.GetResponseStream())
        'Dim xmlstring As String = reader.ReadToEnd



        'responseXML.LoadXml(reader.ReadToEnd)

        'TV4

        'TV4+
        'http://online.tv4.se/Release/XMLChart.aspx?channel=12&startdate=2011-01-31&enddate=2011-02-27
    End Sub
End Class



Public Class Planned_Break

    Public AirDate As Date
    Public Time As Date
    Public Title As String
    Public Channel As String
    Public Duration As String
    Public Sequence_number As String
    Public ID_number As String
    Public Is_Regional As Boolean
    Public Language As String
    Public TargetGroup As String
    Public Est_rating As String
    Public Price As String
    Public CPT As String
    Public CPP As String
    Public Dirr As String
    Public MaM As Integer
    Public Remark As String

End Class