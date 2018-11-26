Imports System.IO
Imports System.Windows.Forms

Public Class frmSpotControlImport

    Dim chan As New Hashtable(15)
    Dim strFileName As String

    Public Function returnName() As String
        Return strFileName
    End Function

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        On Error GoTo error_handler

        'getting the file via a dialogue
        Dim dlgFile As New OpenFileDialog
        If dlgFile.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        txtFile.Text = dlgFile.FileName

        'grdSpotControl.Rows.Clear()
        'grdAdvantEdge.Rows.Clear()

        'two variables holding start end end date of the posts in the newly created file
        Dim startDate As String = ""
        Dim endDate As String = ""

        'declaring a FileStream to open the file named txt with access mode of reading
        Dim fsRead As New FileStream(dlgFile.FileName, FileMode.Open, FileAccess.Read)

        'creating a new StreamReader and passing the filestream object fsRead as argument
        Dim sr As New StreamReader(fsRead)

        'set the pointer in the beginning of the file
        sr.BaseStream.Seek(0, SeekOrigin.Begin)

        'a container for the row
        Dim strLine As String

        Dim strTemp As String
        Dim i As Integer

        Dim FileName As String = Guid.NewGuid.ToString + ".txt"

        'declaring a FileStream and creating a text document file named file with access mode of writing
        Dim fsWrite As New FileStream(My.Application.Info.DirectoryPath + "\" + FileName, FileMode.Create, FileAccess.Write)

        'creating a new StreamWriter and passing the filestream object fsWrite as argument
        Dim sw As New StreamWriter(fsWrite)

        'temporary containers for the information we want from each line
        Dim strComment As String = ""
        'Dim strChannel As String = ""
        Dim strYear As String = ""
        Dim strMonth As String = ""
        Dim strDate As String = ""
        Dim strDuration As String = ""
        Dim strHours As String = ""
        Dim strMinutes As String = ""
        Dim strSeconds As String = ""
        Dim strProgramme As String = ""
        Dim strAdvertiser As String = ""
        Dim dDate As Date
        Dim MaM As Integer
        Dim SaM As Integer
        Dim program As String
        Dim j As Integer = 0
        Dim Errors As New List(Of String)

        'peek method of StreamReader object tells how much more data is left in the file
        While sr.Peek() > -1
            'read a new line
            strLine = sr.ReadLine

            'last row mightn ot be entire empty
            If strLine.Length < 10 Then Exit While

            'set trough it until we are at the commetns line
            strTemp = Mid(strLine, InStr(strLine, Chr(9)) + 1)
            For i = 0 To 12
                strTemp = Mid(strTemp, InStr(strTemp, Chr(9)) + 1)
            Next
            strTemp = strTemp.Substring(0, strTemp.IndexOf(Chr(9)))

            'if there are a comment we save the entire break
            'this means we need to back track until when the spot number is 1
            If Not strTemp = "" Then

                i = grdSpotControl.Rows.Add

                'read a new line
                j = 0

                'step though the line (tabs) and save the information we want
                strAdvertiser = Mid(strLine, 1, InStr(strLine, Chr(9)) - 1).Replace("""", "").Trim
                strTemp = Mid(strLine, InStr(strLine, Chr(9)) + 1)
                For j = 0 To 12
                    strTemp = Mid(strTemp, InStr(strTemp, Chr(9)) + 1)
                    Select Case j
                        Case Is = 1 'channel name
                            grdSpotControl.Rows(i).Cells("colChannel").Value = chan(strTemp.Substring(0, strTemp.IndexOf(Chr(9))).Trim.Replace("""", ""))
                        Case Is = 2 'year
                            strYear = strTemp.Substring(0, strTemp.IndexOf(Chr(9))).Trim.Replace("""", "")
                        Case Is = 3 'month
                            strMonth = strTemp.Substring(0, strTemp.IndexOf(Chr(9))).Trim.Replace("""", "")
                        Case Is = 4 'date
                            strDate = strTemp.Substring(0, strTemp.IndexOf(Chr(9))).Trim.Replace("""", "")
                        Case Is = 5 ' duration
                            grdSpotControl.Rows(i).Cells("colDuration").Value = strTemp.Substring(0, strTemp.IndexOf(Chr(9))).Trim.Replace("""", "")
                        Case Is = 6 ' programe
                            strProgramme = strTemp.Substring(0, strTemp.IndexOf(Chr(9))).Trim.Replace("""", "")
                            grdSpotControl.Rows(i).Cells("colProgram").Value = strProgramme.Substring(strProgramme.IndexOf(" "), strProgramme.Length - strProgramme.IndexOf(" ")).Trim
                        Case Is = 7 'Hour
                            strHours = strTemp.Substring(0, strTemp.IndexOf(Chr(9))).Trim.Replace("""", "")
                        Case Is = 8 'Minutes
                            strMinutes = strTemp.Substring(0, strTemp.IndexOf(Chr(9))).Trim.Replace("""", "")
                        Case Is = 9 ' Seconds
                            strSeconds = strTemp.Substring(0, strTemp.IndexOf(Chr(9))).Trim.Replace("""", "")
                        Case Is = 11 ' Spot number #/#
                            grdSpotControl.Rows(i).Cells("colSpot").Value = strTemp.Substring(0, strTemp.IndexOf(Chr(9))).Trim.Replace("""", "")
                        Case Is = 12 ' spot comment
                            grdSpotControl.Rows(i).Cells("colReason").Value = strTemp.Substring(0, strTemp.IndexOf(Chr(9))).Trim.Replace("""", "")
                    End Select
                Next

                dDate = New Date(CInt(strYear), CInt(strMonth), CInt(strDate))

                If CInt(strHours) >= 26 Then
                    SaM = (((CInt(strHours) - 24) * 60) + CInt(strMinutes)) * 60
                    dDate = dDate.AddDays(1)
                    strHours = CInt(strHours) - 24
                Else
                    SaM = ((CInt(strHours) * 60) + CInt(strMinutes)) * 60
                End If


                SaM += CInt(strSeconds)

                grdSpotControl.Rows(i).Cells("colDate").Value = Format(dDate.Date, "ddMMyy")
                grdSpotControl.Rows(i).Cells("colDate").Tag = dDate.ToOADate
                grdSpotControl.Rows(i).Cells("colTime").Value = strHours & ":" & strMinutes
                grdSpotControl.Rows(i).Cells("colTime").Tag = SaM
                grdSpotControl.Rows(i).Cells("colAdvertiser").Value = strAdvertiser


                If startDate = "" Then
                    'if startdate is empty we add this date to it (only happens fist time
                    'step trough it until we are at the comments line
                    Dim s As String = ""
                    strTemp = Mid(strLine, InStr(strLine, Chr(9)) + 1)
                    For i = 0 To 4
                        strTemp = Mid(strTemp, InStr(strTemp, Chr(9)) + 1)
                        Select Case i
                            Case Is = 2
                                s = strTemp.Substring(0, strTemp.IndexOf(Chr(9))).Trim.Replace("""", "")
                            Case Is = 3
                                s &= "-" & Format(CInt(strTemp.Substring(0, strTemp.IndexOf(Chr(9))).Trim.Replace("""", "")), "00")
                            Case Is = 4
                                s &= "-" & Format(CInt(strTemp.Substring(0, strTemp.IndexOf(Chr(9))).Trim.Replace("""", "")), "00")
                        End Select
                    Next
                    startDate = s
                End If

                'we always save the latest date, if it is the last we will add it to the text file
                Dim s2 As String = ""
                strTemp = Mid(strLine, InStr(strLine, Chr(9)) + 1)
                For i = 0 To 4
                    strTemp = Mid(strTemp, InStr(strTemp, Chr(9)) + 1)
                    Select Case i
                        Case Is = 2
                            s2 = strTemp.Substring(0, strTemp.IndexOf(Chr(9))).Trim.Replace("""", "")
                        Case Is = 3
                            s2 &= "-" & Format(CInt(strTemp.Substring(0, strTemp.IndexOf(Chr(9))).Trim.Replace("""", "")), "00")
                        Case Is = 4
                            s2 &= "-" & Format(CInt(strTemp.Substring(0, strTemp.IndexOf(Chr(9))).Trim.Replace("""", "")), "00")
                    End Select
                Next
                endDate = s2
            End If
        End While
        If startDate <> "" Then
            If txtFromDate.Text = "" OrElse CDate(txtFromDate.Text) > CDate(startDate) Then
                txtFromDate.Text = startDate
            End If
            If txtToDate.Text = "" OrElse CDate(txtToDate.Text) < CDate(endDate) Then
                txtToDate.Text = endDate
            End If

            'closing the file reader
            sr.Close()

            'closing the file writer
            sw.Close()
            fsWrite.Close()
            fsWrite.Dispose()

            'declare the wrapper for the Advantage dll
            Dim Adedge As ConnectWrapper.Brands = New ConnectWrapper.Brands

            Dim filmcode As String
            Dim spotCountFile As Integer
            Dim ActualSpotCountFile As Integer

            Dim found As Boolean

            i = 0

            Dim spot As Integer


            'setup advantege so we can look up the spots we are looking for
            Adedge.setPeriod(Format(CDate(txtFromDate.Text), "ddMMyy") & "-" & Format(CDate(txtToDate.Text).AddDays(+1), "ddMMyy"))
            Adedge.setArea("SE") 'reads the country from the ini file
            Adedge.setTargetMnemonic("3+", False)
            Adedge.setChannelsArea("TV3 se,TV4,Kan 5,TV6 se, Kanal 9", "SE")
            Adedge.setBrandType("COMMERCIAL")

            Dim spotcount As Integer = Adedge.Run(True, False, 0, False)
            Adedge.sort("channel(asc),date(asc),fromtime(asc)")
            If spotcount = 0 Then
                MsgBox(" No Adedge spots where found")
                Exit Sub
            End If

            Dim nr As Integer = 0
            Dim bolRegional As Boolean = False

            For z As Integer = 0 To grdSpotControl.Rows.Count - 1

                nr += 1
                j = 0
                found = False
                bolRegional = False

reapeat:
                'step trought the Advantege list until we are at the right channel
                While Not Adedge.getAttrib(Connect.eAttribs.aChannel, j).ToString.ToUpper = grdSpotControl.Rows(z).Cells("colChannel").Value.ToString.ToUpper
                    j += 1
                    If j = spotcount - 1 Then
                        For Each cell As DataGridViewCell In grdSpotControl.Rows(z).Cells
                            cell.Style.ForeColor = Color.Red
                        Next
                        GoTo skip_it
                    End If
                End While

                'step trought the Advantege list until we are at the right date
                While Not Adedge.getAttrib(Connect.eAttribs.aDate, j) = grdSpotControl.Rows(z).Cells("colDate").Tag
                    j += 1

                    If j = spotcount - 1 Then
                        For Each cell As DataGridViewCell In grdSpotControl.Rows(z).Cells
                            cell.Style.ForeColor = Color.Red
                        Next
                        GoTo skip_it
                    End If
                End While

                'step trought the Advantege list until we are at the roughly right time
                While Adedge.getAttrib(Connect.eAttribs.aFromTime, j + 1) < CInt(grdSpotControl.Rows(z).Cells("colTime").Tag) - 180
                    j += 1
                    If j = spotcount - 1 Then
                        For Each cell As DataGridViewCell In grdSpotControl.Rows(z).Cells
                            cell.Style.ForeColor = Color.Red
                        Next
                        GoTo skip_it
                    End If
                End While

                j += 1

                'get the max number of spots in the break and the number for the specific spot
                ActualSpotCountFile = grdSpotControl.Rows(z).Cells("colSpot").Value.ToString.Substring(0, grdSpotControl.Rows(z).Cells("colSpot").Value.ToString.IndexOf("/"))
                spotCountFile = grdSpotControl.Rows(z).Cells("colSpot").Value.ToString.Substring(grdSpotControl.Rows(z).Cells("colSpot").Value.ToString.IndexOf("/") + 1)

                'we now check the values if the match the ones we are looking for
                While Adedge.getAttrib(Connect.eAttribs.aFromTime, j) < grdSpotControl.Rows(z).Cells("colTime").Tag + 180

                    'get the spot count in the break
                    spot = Adedge.getAttrib(Connect.eAttribs.aBrandSpotCount, j)

                    'check if the break contains a regional spot
                    If Adedge.getAttrib(Connect.eAttribs.aBrandFilmCode, j - Adedge.getAttrib(Connect.eAttribs.aBrandSpotInBreak, j) + 1) = "REGIONAL" Then
                        ''regional in beginning
                        bolRegional = True

go_Through_All:
                        For i = 0 To (Adedge.getAttrib(Connect.eAttribs.aBrandSpotCount, j) - Adedge.getAttrib(Connect.eAttribs.aBrandSpotInBreak, j))
                            If matchStrings(grdSpotControl.Rows(z).Cells("colProgram").Value.ToString.ToUpper, Adedge.getAttrib(Connect.eAttribs.aBrandProgAfter, j + i).ToString.ToUpper) > 0.6 Then
                                'we have a match
                                If found Then
                                    If Math.Abs((grdSpotControl.Rows(z).Cells("colTime").Tag - Adedge.getAttrib(Connect.eAttribs.aFromTime, j + i))) < Math.Abs((grdSpotControl.Rows(z).Cells("colTime").Tag - Adedge.getAttrib(Connect.eAttribs.aFromTime, spot))) Then
                                        spot = j + i
                                    End If
                                End If
                                found = True

                            End If
                        Next

                        If found Then
                            j = spot
                            Exit While
                        End If

                    ElseIf Adedge.getAttrib(Connect.eAttribs.aBrandFilmCode, j + (Adedge.getAttrib(Connect.eAttribs.aBrandSpotCount, j) - Adedge.getAttrib(Connect.eAttribs.aBrandSpotInBreak, j))) = "REGIONAL" Then
                        ''regional in the end
                        bolRegional = True

                        If ActualSpotCountFile < spot Then
                            GoTo check_beginning
                        Else
                            'if not we need to go though the entire break
                            GoTo go_Through_All
                        End If
                    Else
                        'no regional spots
check_beginning:
                        'set us to the rigth spot count
                        j += (ActualSpotCountFile - Adedge.getAttrib(Connect.eAttribs.aBrandSpotInBreak, j))

                        'check if it has the spotcount that we are looking for
                        If spot = spotCountFile Then

                            'check if the programme matches
                            If Adedge.getAttrib(Connect.eAttribs.aBrandSpotInBreak, j) = ActualSpotCountFile Then
                                If Not Adedge.getAttrib(Connect.eAttribs.aBrandProgAfter, j) Is Nothing Then
                                    If matchStrings(grdSpotControl.Rows(z).Cells("colProgram").Value.ToString.ToUpper, Adedge.getAttrib(Connect.eAttribs.aBrandProgAfter, j).ToString.ToUpper) > 0.6 Then
                                        'we have a match
                                        found = True
                                        Exit While
                                    End If
                                End If
                            End If
                        End If
                    End If

                    'increase J with the break length
                    j += (Adedge.getAttrib(Connect.eAttribs.aBrandSpotCount, j) - ActualSpotCountFile + 1)

                    If j > spotcount - 2 Then
                        For Each cell As DataGridViewCell In grdSpotControl.Rows(z).Cells
                            cell.Style.ForeColor = Color.Red
                        Next
                        GoTo skip_it
                    End If

                End While

skip_it:
                i = grdAdvantEdge.Rows.Add
                grdAdvantEdge.Rows(i).Tag = grdSpotControl.Rows(z).Index
                grdSpotControl.Rows(z).Tag = i

                If found Then
                    grdAdvantEdge.Rows(i).Cells("col_FilmCode").Value = Adedge.getAttrib(Connect.eAttribs.aBrandFilmCode, j)
                    grdAdvantEdge.Rows(i).Cells("col_Prolgram").Value = Adedge.getAttrib(Connect.eAttribs.aBrandProgAfter, j)
                    grdAdvantEdge.Rows(i).Cells("col_Date").Value = Format(Date.FromOADate(Adedge.getAttrib(Connect.eAttribs.aDate, j)), "ddMMyy")
                    grdAdvantEdge.Rows(i).Cells("col_Date").Tag = Adedge.getAttrib(Connect.eAttribs.aDate, j)
                    grdAdvantEdge.Rows(i).Cells("col_Time").Value = Mam2Tid(Adedge.getAttrib(Connect.eAttribs.aFromTime, j) \ 60)
                    grdAdvantEdge.Rows(i).Cells("col_Time").Tag = Adedge.getAttrib(Connect.eAttribs.aFromTime, j) \ 60
                    grdAdvantEdge.Rows(i).Cells("col_Channel").Value = Adedge.getAttrib(Connect.eAttribs.aChannel, j)
                    grdAdvantEdge.Rows(i).Cells("col_Advertiser").Value = Adedge.getAttrib(Connect.eAttribs.aBrandAdvertiser, j)
                    grdAdvantEdge.Rows(i).Cells("col_Duration").Value = Adedge.getAttrib(Connect.eAttribs.aDuration, j)
                    grdAdvantEdge.Rows(i).Cells("col_Spot").Value = Adedge.getAttrib(Connect.eAttribs.aBrandSpotInBreak, j) & "/" & Adedge.getAttrib(Connect.eAttribs.aBrandSpotCount, j)

                    If bolRegional Then
                        For Each cell As DataGridViewCell In grdSpotControl.Rows(z).Cells
                            cell.Style.ForeColor = Color.DarkGoldenrod
                        Next
                    End If

                Else
                    For Each cell As DataGridViewCell In grdSpotControl.Rows(z).Cells
                        cell.Style.ForeColor = Color.Red
                    Next
                End If
            Next
        Else
            MessageBox.Show("There are no errors to import in this file.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        Dim TmpMsg As String = "The file was imported with " & Errors.Count & " errors."
        For Each TmpStr As String In Errors
            TmpMsg &= vbCrLf & TmpStr
        Next
        MessageBox.Show(TmpMsg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Exit Sub

error_handler:
        MsgBox("Failed")
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim dialog As New SaveFileDialog

        dialog.DefaultExt = "*.mtf"
        dialog.Title = "Save as..."
        dialog.FileName = "*.mtf"
        dialog.Filter = "Meta file|*.mtf"

        If dialog.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub

        Dim fsWrite As FileStream = New FileStream(dialog.FileName, FileMode.Create, FileAccess.Write)
        Dim sw As StreamWriter = New StreamWriter(fsWrite)

        For Each row As DataGridViewRow In grdAdvantEdge.Rows
            If Not row.Cells("col_FilmCode").Value = "REGIONAL" Then
                sw.WriteLine("INSERT INTO spotcontrol (ID,Channel,date,MaM,program,filmcode,remark) VALUES ('" & CreateGUID() & "','" & row.Cells("col_Channel").Value & "'," & row.Cells("col_Date").Tag & "," & row.Cells("col_Time").Tag & ",'" & row.Cells("col_Prolgram").Value & "','" & row.Cells("col_FilmCode").Value & "','" & grdSpotControl.Rows(row.Index).Cells("colReason").Value & "')")
                DBReader.QUERY("INSERT INTO spotcontrol (ID,Channel,date,MaM,program,filmcode,remark) VALUES ('" & CreateGUID() & "','" & row.Cells("col_Channel").Value & "'," & row.Cells("col_Date").Tag & "," & row.Cells("col_Time").Tag & ",'" & row.Cells("col_Prolgram").Value & "','" & row.Cells("col_FilmCode").Value & "','" & grdSpotControl.Rows(row.Index).Cells("colReason").Value & "')")
            End If
        Next


        'closing the file writer
        sw.Close()
        fsWrite.Close()
        fsWrite.Dispose()

        strFileName = dialog.FileName

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Public Function matchStrings(ByVal s1 As String, ByVal s2 As String) As Single
        'this function matches two stings by their sub strings and return a value between 0 and 1 where 1 is a 100% match
        ' note that the function dont see any difference between "you can go home" and "can you home is"
        ' Words shorter that 3 letters are not taken into account and neither the order of the words in the string.

        Dim tryWord As String
        Dim tryLength As Integer
        Dim Words As String()
        Dim maxWord As String
        Dim maxLength As Integer
        Dim i As Integer
        Dim countable As Integer
        Dim retur As Double

        'first we search for s1 in s2
        Words = s1.Split(" ")

        For Each maxWord In Words
            maxLength = maxWord.Length
            tryLength = maxLength
            While tryLength > 2
                For i = 0 To maxLength - tryLength
                    tryWord = maxWord.Substring(maxLength - tryLength, tryLength)
                    If InStr(s2, tryWord) > 0 Then
                        Exit While
                    End If
                Next
                tryLength -= 1
            End While

            'we skip words shorter than 3 letters
            If Not tryLength < 3 Then
                retur += tryLength / maxLength
                countable += 1
            End If
        Next

        If countable = 0 Then
            Return 0
        Else
            Return retur / countable
        End If

    End Function

    Public Function removeSpecials(ByVal s As String) As String
        Dim ca As Char() = s.ToLower.ToCharArray
        Dim retur As String = ""
        Dim ASCII As Integer

        For Each c As Char In ca
            ASCII = Convert.ToByte(c.ToString.ToLower)
            If (ASCII > 96 AndAlso ASCII < 123) OrElse (ASCII > 223 AndAlso ASCII < 247) Then
                retur = retur & c.ToString
            End If
        Next

        Return retur
    End Function

    Shared Function Mam2Tid(ByVal MaM As Integer) As String
        Dim h As Integer
        Dim m As Integer
        Dim Tmpstr As String

        h = MaM \ 60
        m = MaM Mod 60

        Tmpstr = Trim(h) & ":" & Format(m, "00")
        While Len(Tmpstr) < 5
            Tmpstr = "0" & Tmpstr
        End While
        Mam2Tid = Tmpstr
    End Function

    Private Sub grdAdvantEdge_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAdvantEdge.SelectionChanged
        If grdAdvantEdge.SelectedRows.Count = 0 Then Exit Sub
        If grdSpotControl.Rows.Count = 0 Then Exit Sub
        If grdAdvantEdge.Rows.Count = 0 Then Exit Sub
        If grdAdvantEdge.SelectedRows(0).Index = grdSpotControl.SelectedRows(0).Index Then Exit Sub
        grdSpotControl.ClearSelection()
        grdSpotControl.Rows(grdAdvantEdge.SelectedRows(0).Index).Selected = True
    End Sub

    Private Sub grdSpotControl_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSpotControl.SelectionChanged
        If grdSpotControl.SelectedRows.Count = 0 Then Exit Sub
        If grdSpotControl.Rows.Count = 0 Then Exit Sub
        If grdAdvantEdge.Rows.Count = 0 Then Exit Sub
        If grdSpotControl.SelectedRows(0).Index = grdAdvantEdge.SelectedRows(0).Index Then Exit Sub
        grdAdvantEdge.ClearSelection()
        grdAdvantEdge.Rows(grdSpotControl.SelectedRows(0).Index).Selected = True
    End Sub

    Private Sub grdAdvantEdge_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles grdAdvantEdge.Scroll
        If grdAdvantEdge.FirstDisplayedScrollingRowIndex > grdSpotControl.Rows.Count Then Exit Sub
        grdSpotControl.FirstDisplayedScrollingRowIndex = grdAdvantEdge.FirstDisplayedScrollingRowIndex
    End Sub

    Private Sub grdSpotControl_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles grdSpotControl.Scroll
        If grdSpotControl.FirstDisplayedScrollingRowIndex > grdAdvantEdge.Rows.Count Then Exit Sub
        grdAdvantEdge.FirstDisplayedScrollingRowIndex = grdSpotControl.FirstDisplayedScrollingRowIndex
    End Sub

    Private Sub cmdMatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMatch.Click
        'declare the wrapper for the Advantage dll
        Dim Adedge As ConnectWrapper.Brands = New ConnectWrapper.Brands

        Dim row As DataGridViewRow = grdSpotControl.SelectedRows(0)

        'setup advantege so we can look up the spots we are looking for
        Adedge.setPeriod(Format(Date.FromOADate(row.Cells("colDate").Tag).AddDays(-1), "ddMMyy") & "-" & Format(Date.FromOADate(row.Cells("colDate").Tag).AddDays(+1), "ddMMyy"))
        Adedge.setArea(Campaign.Area) 'reads the country from the ini file
        Adedge.setTargetMnemonic("3+", False)
        Adedge.setChannelsArea(row.Cells("colChannel").Value, Campaign.Area)
        Adedge.setBrandType("COMMERCIAL")

        Dim spotcount As Integer = Adedge.Run(True, False, 0, False)
        Adedge.sort("date(asc),fromtime(asc)")
        If spotcount = 0 Then
            MsgBox(" No Adedge spots where found")
            Exit Sub
        End If

        Dim frm As New frmSpotControlMatch

        'populate the tow DGW that holds the spot we search for
        frm.grdSpotControl.Rows.Add()
        frm.grdSpotControl.Rows(0).Cells("colChannel").Value = row.Cells("colChannel").Value
        frm.grdSpotControl.Rows(0).Cells("colDuration").Value = row.Cells("colDuration").Value
        frm.grdSpotControl.Rows(0).Cells("colProgram").Value = row.Cells("colProgram").Value
        frm.grdSpotControl.Rows(0).Cells("colSpot").Value = row.Cells("colSpot").Value
        frm.grdSpotControl.Rows(0).Cells("colReason").Value = row.Cells("colReason").Value
        frm.grdSpotControl.Rows(0).Cells("colDate").Value = row.Cells("colDate").Value
        frm.grdSpotControl.Rows(0).Cells("colTime").Value = row.Cells("colTime").Value



        Dim j As Integer
        Dim i As Integer
        For j = 0 To spotcount - 1
            i = frm.grdAdvantEdge.Rows.Add()
            frm.grdAdvantEdge.Rows(i).Cells("col_FilmCode").Value = Adedge.getAttrib(Connect.eAttribs.aBrandFilmCode, j)
            frm.grdAdvantEdge.Rows(i).Cells("col_Prolgram").Value = Adedge.getAttrib(Connect.eAttribs.aBrandProgAfter, j)
            frm.grdAdvantEdge.Rows(i).Cells("col_Date").Value = Format(Date.FromOADate(Adedge.getAttrib(Connect.eAttribs.aDate, j)).Date, "ddMMyy")
            frm.grdAdvantEdge.Rows(i).Cells("col_Date").Tag = Adedge.getAttrib(Connect.eAttribs.aDate, j)
            frm.grdAdvantEdge.Rows(i).Cells("col_Time").Value = Mam2Tid(Adedge.getAttrib(Connect.eAttribs.aFromTime, j) \ 60)
            frm.grdAdvantEdge.Rows(i).Cells("col_Time").Tag = Adedge.getAttrib(Connect.eAttribs.aFromTime, j) \ 60
            frm.grdAdvantEdge.Rows(i).Cells("col_Channel").Value = Adedge.getAttrib(Connect.eAttribs.aChannel, j)
            frm.grdAdvantEdge.Rows(i).Cells("col_Duration").Value = Adedge.getAttrib(Connect.eAttribs.aDuration, j)
            frm.grdAdvantEdge.Rows(i).Cells("colAdvertiser").Value = Adedge.getAttrib(Connect.eAttribs.aBrandAdvertiser, j)
            frm.grdAdvantEdge.Rows(i).Cells("col_Spot").Value = Adedge.getAttrib(Connect.eAttribs.aBrandSpotInBreak, j) & "/" & Adedge.getAttrib(Connect.eAttribs.aBrandSpotCount, j)
        Next

        If frm.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub

        'color it blue
        For Each cell As DataGridViewCell In row.Cells
            cell.Style.ForeColor = Color.Blue
        Next

        Dim rowReturn As DataGridViewRow = frm.grdAdvantEdge.SelectedRows(0)
        row = grdAdvantEdge.SelectedRows(0)

        row.Cells("col_Channel").Value = rowReturn.Cells("col_Channel").Value
        row.Cells("col_Duration").Value = rowReturn.Cells("col_Duration").Value
        row.Cells("col_Prolgram").Value = rowReturn.Cells("col_Prolgram").Value
        row.Cells("col_Spot").Value = rowReturn.Cells("col_Spot").Value
        row.Cells("col_Date").Value = rowReturn.Cells("col_Date").Value
        row.Cells("col_Time").Value = rowReturn.Cells("col_Time").Value
        row.Cells("col_Date").Tag = rowReturn.Cells("col_Date").Tag
        row.Cells("col_Advertiser").Value = rowReturn.Cells("colAdvertiser").Value
        row.Cells("col_Time").Tag = rowReturn.Cells("col_Time").Tag
        row.Cells("col_FilmCode").Value = rowReturn.Cells("col_FilmCode").Value



    End Sub

    Private Sub frmSpotControl_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        chan.Clear()
        chan.Add("TV 3", "TV3 se")
        chan.Add("TV 4", "TV4")
        chan.Add("TV 5", "Kan 5")
        chan.Add("TV 6", "TV6 se")
        chan.Add("Kanal 9", "Kanal 9")
        chan.Add("Z TV", "TV6 se") 'OBS DENNA
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        grdSpotControl.Rows.Clear()
        grdAdvantEdge.Rows.Clear()

    End Sub
End Class