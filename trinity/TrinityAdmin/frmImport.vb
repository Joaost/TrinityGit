Imports System.IO

Public Class frmImport

    Dim MinDate As Date
    Dim MaxDate As Date
    Dim _xml As Xml.XmlElement
    Dim _xmlDoc As Xml.XmlDocument

    Dim strUpload As New List(Of String)

    Public Property XmlDoc()
        Get
            Return _xmlDoc
        End Get
        Set(ByVal value)
            _xmlDoc = value
            _xml = _xmlDoc.CreateElement("TEMP")
        End Set
    End Property

    Public ReadOnly Property Xml()
        Get
            Return _xml
        End Get
    End Property

    Public Property uploadList()
        Get
            Return strUpload
        End Get
        Set(ByVal value)
            strUpload = uploadList
        End Set
    End Property


    Private Sub cmdFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFile.Click
        Dim TmpString As String
        Dim TmpProg As String
        Dim c As Integer
        Dim Duration As Integer
        Dim x As Integer
        Dim Datum As String
        Dim Tid As String
        Dim Prog As String
        Dim Est As Single
        Dim Pris As Long
        Dim Flags As String
        Dim ProgTo As Integer
        Dim ToHour As Integer
        Dim ToMin As Integer
        Dim TmpEst As String
        Dim idx As Integer

        Dim dlgFile As New OpenFileDialog

        dlgFile.FileName = "*.txt"
        dlgFile.Filter = "Text-files|*.txt,*.csv|All files|*.*"

        If dlgFile.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        grdSchedule.Rows.Clear()
        If campaign.Area = "DK" Then
            Using sr As IO.StreamReader = New IO.StreamReader(dlgFile.FileName, System.Text.Encoding.UTF7)
                MinDate = Now.AddYears(4)
                TmpString = sr.ReadLine()
                If cmbChannel.SelectedItem = "TV 2" Then
                    Do
                        Flags = ""
                        Datum = TmpString.Substring(0, 8)
                        Dim d As Date = New Date(Datum.Substring(0, 4), Datum.Substring(4, 2), Datum.Substring(6, 2))
                        If d < MinDate Then
                            MinDate = d
                        End If
                        If d > MaxDate Then
                            MaxDate = d
                        End If
                        TmpString = Mid(TmpString, 10)
                        TmpString = Trim(TmpString)
                        Tid = TmpString.Substring(0, InStr(TmpString, vbTab) - 1)
                        If Tid.Length = 4 Then
                            Tid = "0" & Tid
                        End If
                        TmpString = Mid(TmpString, InStr(TmpString, vbTab) + 1)
                        Duration = Val(TmpString.Substring(0, TmpString.IndexOf(vbTab)))
                        TmpString = Mid(TmpString, InStr(TmpString, vbTab) + 1)
                        If InStr(TmpString, vbTab) > 0 Then
                            TmpProg = TmpString.Substring(0, TmpString.IndexOf(vbTab))
                            While InStr(TmpProg, "'") > 0
                                Mid(TmpProg, InStr(TmpProg, "'")) = "´"
                            End While
                            Prog = TmpProg
                            TmpString = Mid(TmpString, InStr(TmpString, vbTab) + 1)
                            TmpEst = TmpString.Substring(0, TmpString.IndexOf(vbTab))
                            TmpEst = TmpEst.Replace("""", "")
                            If InStr(TmpEst, ".") > 0 Then
                                Mid(TmpEst, InStr(TmpEst, "."), 1) = ","
                            End If
                            If TmpEst = "" Then TmpEst = "0"
                            If TmpEst.EndsWith("TRP") Then
                                Flags = "4-11"
                                TmpEst = TmpEst.Substring(0, TmpEst.Length - 4)
                            Else
                                Flags = "12+"
                            End If
                            Est = CSng(TmpEst)
                            TmpString = Mid(TmpString, InStr(TmpString, vbTab) + 1)
                            If InStr(TmpString, vbTab) = 1 Then TmpString = Mid(TmpString, 2)
                            If InStr(TmpString, vbTab) > 0 Then
                                Pris = TmpString.Substring(0, TmpString.IndexOf(vbTab))
                                TmpString = Mid(TmpString, InStr(TmpString, vbTab) + 1)
                            Else
                                Pris = Val(TmpString)
                            End If
                        Else
                            Prog = TmpString
                        End If


                        idx = grdSchedule.Rows.Add()
                        grdSchedule.Rows(idx).Cells(0).Value = Datum
                        grdSchedule.Rows(idx).Cells(1).Value = Tid
                        grdSchedule.Rows(idx).Cells(2).Value = Duration
                        grdSchedule.Rows(idx).Cells(3).Value = Prog
                        grdSchedule.Rows(idx).Cells(4).Value = Est
                        grdSchedule.Rows(idx).Cells(5).Value = Pris
                        grdSchedule.Rows(idx).Cells(6).Value = Flags

                        'Duration = Duration - 20
                        'If Tid.Length = 5 Then
                        '    Tid = Tid.Substring(0, 2) & Tid.Substring(3, 2)
                        'End If
                        'Tid = Tid + 20
                        ToHour = ProgTo \ 100
                        ToMin = ProgTo Mod 100
                        TmpString = sr.ReadLine()

                    Loop Until TmpString Is Nothing
                Else
                    Do
                        Flags = ""
                        Datum = TmpString.Substring(0, 8)
                        Dim d As Date = New Date(Datum.Substring(0, 4), Datum.Substring(4, 2), Datum.Substring(6, 2))
                        If d < MinDate Then
                            MinDate = d
                        End If
                        If d > MaxDate Then
                            MaxDate = d
                        End If
                        TmpString = Mid(TmpString, 10)
                        TmpString = Trim(TmpString)
                        Tid = TmpString.Substring(0, InStr(TmpString, vbTab) - 1)
                        If Tid.Length = 4 Then
                            Tid = "0" & Tid
                        End If
                        TmpString = Mid(TmpString, InStr(TmpString, vbTab) + 1)
                        Duration = Val(TmpString.Substring(0, TmpString.IndexOf(vbTab)))
                        TmpString = Mid(TmpString, InStr(TmpString, vbTab) + 1)
                        If InStr(TmpString, vbTab) > 0 Then
                            TmpProg = TmpString.Substring(0, TmpString.IndexOf(vbTab))
                            While InStr(TmpProg, "'") > 0
                                Mid(TmpProg, InStr(TmpProg, "'")) = "´"
                            End While
                            Prog = TmpProg
                            TmpString = Mid(TmpString, InStr(TmpString, vbTab) + 1)
                            TmpEst = TmpString.Substring(0, TmpString.IndexOf(vbTab))
                            TmpEst = TmpEst.Replace("""", "")
                            If InStr(TmpEst, ".") > 0 Then
                                Mid(TmpEst, InStr(TmpEst, "."), 1) = ","
                            End If
                            If TmpEst = "" Then TmpEst = "0"
                            Est = CSng(TmpEst)
                            TmpString = Mid(TmpString, InStr(TmpString, vbTab) + 1)
                            If InStr(TmpString, vbTab) = 1 Then TmpString = Mid(TmpString, 2)
                            Flags = TmpString
                            Pris = 0
                        Else
                            Prog = TmpString
                        End If


                        idx = grdSchedule.Rows.Add()
                        grdSchedule.Rows(idx).Cells(0).Value = Datum
                        grdSchedule.Rows(idx).Cells(1).Value = Tid
                        grdSchedule.Rows(idx).Cells(2).Value = Duration
                        grdSchedule.Rows(idx).Cells(3).Value = Prog
                        grdSchedule.Rows(idx).Cells(4).Value = Est
                        grdSchedule.Rows(idx).Cells(5).Value = Pris
                        grdSchedule.Rows(idx).Cells(6).Value = Flags


                        TmpString = sr.ReadLine()

                    Loop Until TmpString Is Nothing
                End If
            End Using
        Else
            Using sr As IO.StreamReader = New IO.StreamReader(dlgFile.FileName, System.Text.Encoding.UTF7)
                MinDate = Now.AddYears(4)
                TmpString = sr.ReadLine()
                Dim Counter As Integer = 0
                Do
                    Counter += 1 'counter for easier break on failing lines

                    Flags = ""

                    Datum = TmpString.Substring(0, TmpString.IndexOf(Chr(9)))

                    Dim d As Date
                    If Datum.Length = 10 Then
                        d = New Date(Datum.Substring(0, 4), Datum.Substring(5, 2), Datum.Substring(8, 2))
                    Else
                        d = New Date(Datum.Substring(0, 4), Datum.Substring(4, 2), Datum.Substring(6, 2))
                    End If

                    If d < MinDate Then
                        MinDate = d
                    End If
                    If d > MaxDate Then
                        MaxDate = d
                    End If
                    TmpString = Mid(TmpString, TmpString.IndexOf(Chr(9)) + 2)
                    TmpString = Trim(TmpString)
                    Tid = TmpString.Substring(0, TmpString.IndexOf(Chr(9)))
                    If Tid.Length = 3 Then
                        Tid = "0" & Tid
                    End If
                    TmpString = Mid(TmpString, TmpString.IndexOf(Chr(9)) + 2)
                    Dim TmpDurStr As String = TmpString.Substring(0, TmpString.IndexOf(Chr(9)))
                    If TmpDurStr = "#VALUE!" Or TmpDurStr = "000" Then
                        Duration = -1
                    Else
                        Duration = Val(TmpDurStr)
                    End If
                    If Not Duration = 0 Then
                        TmpString = Mid(TmpString, InStr(TmpString, Chr(9)) + 1)
                    End If
                    If InStr(TmpString, Chr(9)) > 0 Then
                        TmpProg = TmpString.Substring(0, TmpString.IndexOf(Chr(9)))
                        While InStr(TmpProg, "'") > 0
                            Mid(TmpProg, InStr(TmpProg, "'")) = "´"
                        End While
                        Prog = TmpProg
                        TmpString = Mid(TmpString, InStr(TmpString, Chr(9)) + 1)
                        TmpEst = TmpString.Substring(0, TmpString.IndexOf(Chr(9)))
                        If InStr(TmpEst, ".") > 0 Then
                            Mid(TmpEst, InStr(TmpEst, "."), 1) = "."
                        End If
                        If TmpEst = "" Then TmpEst = "0"
                        Est = CSng(TmpEst)
                        TmpString = Mid(TmpString, InStr(TmpString, Chr(9)) + 1)
                        If InStr(TmpString, Chr(9)) = 1 Then TmpString = Mid(TmpString, 2)
                        If InStr(TmpString, Chr(9)) > 0 Then
                            If TmpString.Substring(0, TmpString.IndexOf(Chr(9))) = "" Or TmpString.Contains("Special") Then
                                Pris = 0
                            Else
                                Pris = (TmpString.Substring(0, TmpString.IndexOf(Chr(9)))).Trim("""")
                            End If
                            TmpString = Mid(TmpString, InStr(TmpString, Chr(9)) + 1)
                        Else
                            Pris = Val(TmpString)
                        End If
                    Else
                        Prog = TmpString
                    End If
                    While InStr(Prog, "'") > 0
                        Mid(Prog, InStr(Prog, "'"), 1) = "´"
                    End While
                    If TmpString = "X" Then
                        Flags = Flags + "L"
                    End If
                    If cmbChannel.Text <> "TV4" And cmbChannel.Text <> "Kanal5" And cmbChannel.Text <> "TV4+" Then
                        c = 1
                        While Duration > 25
                            idx = grdSchedule.Rows.Add()
                            grdSchedule.Rows(idx).Cells(0).Value = Datum
                            grdSchedule.Rows(idx).Cells(1).Value = Tid
                            grdSchedule.Rows(idx).Cells(3).Value = Prog & " " & c
                            grdSchedule.Rows(idx).Cells(4).Value = Est
                            grdSchedule.Rows(idx).Cells(5).Value = Pris
                            grdSchedule.Rows(idx).Cells(6).Value = Flags
                            ProgTo = Tid
                            ToHour = ProgTo \ 100
                            ToMin = ProgTo Mod 100
                            If Duration < 61 Then
                                grdSchedule.Rows(idx).Cells(2).Value = 20
                                Duration = Duration - 20
                                ToMin = ToMin + 20
                            Else
                                If c = 1 Then
                                    grdSchedule.Rows(idx).Cells(2).Value = 15
                                    Duration = Duration - 15
                                    ToMin = ToMin + 15
                                Else
                                    grdSchedule.Rows(idx).Cells(2).Value = 30
                                    Duration = Duration - 30
                                    ToMin = ToMin + 30
                                End If
                            End If
                            While ToMin >= 60
                                ToMin = ToMin - 60
                                ToHour = ToHour + 1
                            End While
                            Tid = ToHour * 100 + ToMin
                            c = c + 1
                            x = x + 1
                        End While
                        If Duration > 20 Then
                            idx = grdSchedule.Rows.Add()
                            grdSchedule.Rows(idx).Cells(0).Value = Datum
                            grdSchedule.Rows(idx).Cells(1).Value = Tid
                            grdSchedule.Rows(idx).Cells(3).Value = Prog & " " & c
                            grdSchedule.Rows(idx).Cells(4).Value = Est
                            grdSchedule.Rows(idx).Cells(5).Value = Pris
                            grdSchedule.Rows(idx).Cells(6).Value = Flags
                            ProgTo = Tid
                            ToHour = ProgTo \ 100
                            ToMin = ProgTo Mod 100
                            If Duration < 61 Then
                                grdSchedule.Rows(idx).Cells(2).Value = 15
                                Duration = Duration - 15
                                ToMin = ToMin + 15
                            Else
                                grdSchedule.Rows(idx).Cells(2).Value = 30
                                Duration = Duration - 30
                                ToMin = ToMin + 30
                            End If
                            While ToMin >= 60
                                ToMin = ToMin - 60
                                ToHour = ToHour + 1
                            End While
                            Tid = ToHour * 100 + ToMin
                            c = c + 1
                            x = x + 1
                        End If
                        idx = grdSchedule.Rows.Add()
                        grdSchedule.Rows(idx).Cells(0).Value = Datum
                        grdSchedule.Rows(idx).Cells(1).Value = Tid
                        grdSchedule.Rows(idx).Cells(2).Value = Duration
                        grdSchedule.Rows(idx).Cells(3).Value = Prog & " " & c
                        grdSchedule.Rows(idx).Cells(4).Value = Est
                        grdSchedule.Rows(idx).Cells(5).Value = Pris
                        grdSchedule.Rows(idx).Cells(6).Value = Flags
                        Duration = Duration - 20
                        If Tid.Length = 5 Then
                            Tid = Tid.Substring(0, 2) & Tid.Substring(3, 2)
                        End If
                        Tid = Tid + 20
                        ToHour = ProgTo \ 100
                        ToMin = ProgTo Mod 100
                        '        Stop
                    Else
                        idx = grdSchedule.Rows.Add()
                        grdSchedule.Rows(idx).Cells(0).Value = Datum
                        grdSchedule.Rows(idx).Cells(1).Value = Tid
                        grdSchedule.Rows(idx).Cells(2).Value = Duration
                        grdSchedule.Rows(idx).Cells(3).Value = Prog
                        grdSchedule.Rows(idx).Cells(4).Value = Est
                        grdSchedule.Rows(idx).Cells(5).Value = Pris
                        grdSchedule.Rows(idx).Cells(6).Value = Flags
                    End If
                    TmpString = sr.ReadLine()
                Loop Until TmpString Is Nothing
                sr.Close()
            End Using
        End If

        lblFile.Text = dlgFile.FileName
        txtFrom.Text = MinDate
        txtTo.Text = MaxDate
    End Sub

    Private Sub cmdRead_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRead.Click
        Dim r As Integer

        DBReader.QUERY("DELETE FROM events WHERE Type=0 AND channel='" & cmbChannel.Text & "' AND date>=" & CDate(txtFrom.Text).ToOADate & " AND Date<=" & CDate(txtTo.Text).ToOADate)
        For r = 0 To grdSchedule.Rows.Count - 1
            Me.Text = "Saving... " & Format(r / (grdSchedule.RowCount - 1), "P0")
            My.Application.DoEvents()
            Dim datum As String = grdSchedule.Rows(r).Cells(0).Value
            Dim d As Date = New Date(datum.Substring(0, 4), datum.Substring(4, 2), datum.Substring(6, 2))
            Dim MaM As String
            Dim Time As String = grdSchedule.Rows(r).Cells(1).Value
            If Time.Length = 3 Then
                Time = "0" & Time
            End If
            MaM = Tid2Mam(Time)

            Dim strProgram As String = grdSchedule.Rows(r).Cells(3).Value
            If strProgram.Length > 50 Then
                strProgram = strProgram.Substring(0, 47) & "~" & strProgram.Substring(strProgram.Length - 1)
            End If

            If campaign.Area = "DK" Then
                DBReader.QUERY("INSERT INTO events (id,channel,[Date],[time],Startmam,Duration,Name,ChanEst,UseCPP,Addition,EstimationTarget) VALUES ('" & CreateGUID() & "','" & cmbChannel.Text & "'," & d.ToOADate & ",'" & Time & "'," & MaM & "," & Val(grdSchedule.Rows(r).Cells(2).Value) & ",'" & strProgram & "','" & _
                        grdSchedule.Rows(r).Cells(4).Value & "',True," & grdSchedule.Rows(r).Cells(5).Value & ",'" & grdSchedule.Rows(r).Cells(6).Value & "')")
            Else
                DBReader.QUERY("INSERT INTO events (id,channel,[Date],[time],Startmam,Duration,Name,ChanEst,Price,IsLocal) VALUES ('" & CreateGUID() & "','" & cmbChannel.Text & "'," & d.ToOADate & ",'" & Time & "'," & MaM & "," & Val(grdSchedule.Rows(r).Cells(2).Value) & ",'" & strProgram & "','" & _
                        grdSchedule.Rows(r).Cells(4).Value & "'," & grdSchedule.Rows(r).Cells(5).Value & "," & CSng(InStr(grdSchedule.Rows(r).Cells(6).Value, "L") > 0) & ")")
            End If
        Next
        
        MsgBox("Ready!", vbInformation)
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtFrom.Text = VB6.Format(Now, "yyyy-mm-dd")
        txtTo.Text = VB6.Format(Now, "yyyy-mm-dd")

        Dim TmpChan As Trinity.cChannel

        For Each TmpChan In campaign.Channels
            cmbChannel.Items.Add(TmpChan.ChannelName)
        Next
        cmbChannel.SelectedIndex = 0

        grdSchedule.Columns.Add("Date", "Date")
        grdSchedule.Columns.Add("Time", "Time")
        grdSchedule.Columns.Add("Dur", "Dur")
        grdSchedule.Columns.Add("Program", "Program")
        grdSchedule.Columns.Add("Est", "Est")
        grdSchedule.Columns.Add("Price", "Price")
        grdSchedule.Columns.Add("Flags", "Flags")

        grdSchedule.Columns(1).Width = 100
        grdSchedule.Columns(2).Width = 100
        grdSchedule.Columns(3).Width = 500
        grdSchedule.Columns(4).Width = 100
        grdSchedule.Columns(5).Width = 100
        grdSchedule.Columns(6).Width = 100
        grdSchedule.Columns(0).Width = 100
    End Sub

    Function ConvertDate(ByVal d As String) As Date
        ConvertDate = CDate(d.Substring(0, 4) & "-" & Mid(d, 5, 2) & "-" & d.Substring(d.Length - 2, 2))
    End Function

    Function Tid2Mam(ByVal Tid As String) As String
        Dim m As Integer
        Dim h As Integer
        m = Tid.Substring(Tid.Length - 2, 2)
        h = Tid.Substring(0, 2)
        Tid2Mam = h * 60 + m
    End Function


    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim i As Integer
        Dim j As Short
        Dim SQL As String
        Dim TmpCols As String
        Dim TmpData As String
        Dim Tmpstr As String
        Dim dialog As New SaveFileDialog

        dialog.DefaultExt = "*.mtf"
        dialog.Title = "Save as..."
        dialog.FileName = "*.mtf"
        dialog.Filter = "Meta file|*.mtf"

        If dialog.ShowDialog() <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        FileOpen(1, dialog.FileName, OpenMode.Output)

        TmpCols = "(" & grdSchedule.Columns(0).HeaderText
        For j = 1 To grdSchedule.Columns.Count - 1
            TmpCols = TmpCols & ",[" & grdSchedule.Columns(j).HeaderText & "]"
        Next
        TmpCols = TmpCols & ")"


        'index 0 is all
        If cmbChannel.SelectedIndex = 0 Then
            PrintLine(1, TAB, "DELETE FROM events WHERE Type=0 AND [Date]>=" & CLng(CDate(txtFrom.Text).ToOADate) & " AND [Date]<=" & CLng(CDate(txtTo.Text).ToOADate))
        Else
            PrintLine(1, TAB, "DELETE FROM events WHERE Type=0 AND [Date]>=" & CLng(CDate(txtFrom.Text).ToOADate) & " AND [Date]<=" & CLng(CDate(txtTo.Text).ToOADate) & " AND Channel='" & cmbChannel.Text & "'")
        End If
        For i = 2 To grdSchedule.Rows.Count - 1
            TmpData = "("
            For j = 0 To grdSchedule.Columns.Count - 1
                If "EstimationPeriod" = grdSchedule.Columns(j).HeaderText Then
                    Try
                        Tmpstr = grdSchedule.Rows(i).Cells(j).Value
                    Catch
                        Tmpstr = "-4fw"
                    End Try
                ElseIf "EstimationTarget" = grdSchedule.Columns(j).HeaderText Then
                    Try
                        Tmpstr = grdSchedule.Rows(i).Cells(j).Value
                    Catch
                        Tmpstr = ""
                    End Try
                Else
                    Try
                        Tmpstr = grdSchedule.Rows(i).Cells(j).Value
                    Catch
                        Tmpstr = "0"
                    End Try
                End If

                While InStr(Tmpstr, "'")
                    Mid(Tmpstr, InStr(Tmpstr, "'"), 1) = "´"
                End While
                While InStr(Tmpstr, ".")
                    Mid(Tmpstr, InStr(Tmpstr, "."), 1) = ","
                End While
                If Tmpstr = "False" Then Tmpstr = "0"
                If Tmpstr = "True" Then Tmpstr = "-1"
                Try
                    If Trim(CStr(Val(Tmpstr))) = Trim(Tmpstr) Then
                        TmpData = TmpData & Tmpstr & ","
                    Else
                        If "Price" = grdSchedule.Columns(j).HeaderText Then
                            If chkSQL.Checked Then
                                'need to cast in into a money datatype
                                TmpData = TmpData & "CAST('" & CDbl(Tmpstr) & "' as money),"
                            Else
                                TmpData = TmpData & "'" & Tmpstr & "',"
                            End If
                        Else
                            TmpData = TmpData & "'" & Tmpstr & "',"
                        End If
                    End If
                Catch
                    TmpData = TmpData & "'" & Tmpstr & "',"
                End Try
            Next
            TmpData = TmpData.Substring(0, Len(TmpData) - 1) & ")"
            SQL = "INSERT INTO events " & TmpCols & " VALUES " & TmpData
            PrintLine(1, SQL)
        Next
        FileClose()
        MsgBox("OK")

        strUpload.Add(dialog.FileName.ToString)
        Dim xe As Xml.XmlElement = _xmlDoc.CreateElement("CopyFile")
        xe.SetAttribute("FileName", "Meta/" & dialog.FileName.Substring(dialog.FileName.LastIndexOf("\") + 1))
        xe.SetAttribute("OverWrite", "True")
        xe.SetAttribute("Tag", "META")
        _xml.AppendChild(xe)
        MsgBox("File was successfully created and added to update list")
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class