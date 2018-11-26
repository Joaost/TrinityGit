Public Class frmExport

    Private Sub cmdExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExport.Click
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

        dialog.ShowDialog()


        FileOpen(1, dialog.FileName, OpenMode.Output)

        TmpCols = "(" & grdProgs.Columns(0).HeaderText
        For j = 1 To grdProgs.Columns.Count - 1
            TmpCols = TmpCols & ",[" & grdProgs.Columns(j).HeaderText & "]"
        Next
        TmpCols = TmpCols & ")"

        'DBConn.BeginTrans()
        If cmbChan.SelectedIndex = 0 Then
            PrintLine(1, TAB, "DELETE FROM events WHERE [Date]>=" & CLng(CDate(txtFrom.Text).ToOADate) & " AND [Date]<=" & CLng(CDate(txtTo.Text).ToOADate))
            'DBReader.QUERY("DELETE FROM events WHERE [Date]>=" & CLng(CDate(txtFrom.Text).ToOADate) & " AND [Date]<=" & CLng(CDate(txtTo.Text).ToOADate))
        Else
            PrintLine(1, TAB, "DELETE FROM events WHERE [Date]>=" & CLng(CDate(txtFrom.Text).ToOADate) & " AND [Date]<=" & CLng(CDate(txtTo.Text).ToOADate) & " AND Channel='" & cmbChan.Text & "'")
            'DBReader.QUERY("DELETE FROM events WHERE [Date]>=" & CLng(CDate(txtFrom.Text).ToOADate) & " AND [Date]<=" & CLng(CDate(txtTo.Text).ToOADate) & " AND Channel='" & cmbChan.Text & "'")
        End If
        For i = 2 To grdProgs.Rows.Count - 1
            TmpData = "("
            For j = 0 To grdProgs.Columns.Count - 1
                If "EstimationPeriod" = grdProgs.Columns(j).HeaderText Then
                    Try
                        Tmpstr = grdProgs.Rows(i).Cells(j).Value
                    Catch
                        Tmpstr = "-4fw"
                    End Try
                ElseIf "EstimationTarget" = grdProgs.Columns(j).HeaderText Then
                    Try
                        Tmpstr = grdProgs.Rows(i).Cells(j).Value
                    Catch
                        Tmpstr = ""
                    End Try
                Else
                    Try
                        Tmpstr = grdProgs.Rows(i).Cells(j).Value
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
                        If "Price" = grdProgs.Columns(j).HeaderText Then
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
            'DBReader.QUERY(SQL)
            PrintLine(1, SQL)
        Next
        FileClose()
        MsgBox("OK")
    End Sub

    Private Sub cmdImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdImport.Click
        Dim DT As DataTable

        If cmbChan.SelectedIndex = 0 Then
            DT = DBReader.getAllEvents(CDate(txtFrom.Text).ToOADate, CDate(txtTo.Text).ToOADate)
        Else
            'DT = DBReader.getEvents(CDate(txtFrom.Text).ToOADate, CDate(txtTo.Text).ToOADate, cmbChan.Text)
            Dim lst As New List(Of KeyValuePair(Of Date, Date))
            Dim kvp As New KeyValuePair(Of Date, Date)(CDate(txtFrom.Text), CDate(txtTo.Text))
            lst.Add(kvp)
            DT = DBReader.getEvents(lst, cmbChan.Text)
        End If
        grdProgs.DataSource = DT
    End Sub

    Private Sub Form_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtFrom.Text = VB6.Format(Now, "yyyy-mm-dd")
        txtTo.Text = VB6.Format(Now, "yyyy-mm-dd")
        cmbChan.Items.Clear()
        cmbChan.Items.Add("All")
        For Each TmpChan As Trinity.cChannel In campaign.Channels
            cmbChan.Items.Add(TmpChan.ChannelName)
        Next
        cmbChan.DisplayMember = "ChannelName"
        cmbChan.SelectedIndex = 0
    End Sub
End Class