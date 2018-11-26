Public Class frmDefineDayparts
    'Kaaaaa'
    'Kap kuuuu'
    Private Sub frmDefineDayparts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbChannel.Items.Clear()
        cmbChannel.Items.Add("Campaign")
        For Each _chan As Trinity.cChannel In Campaign.Channels
            For Each _bt As Trinity.cBookingType In _chan.BookingTypes
                cmbChannel.Items.Add(_bt)
            Next
        Next
        cmbChannel.SelectedIndex = 0
        cmdSaveToFile.Visible = TrinitySettings.Developer
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        If cmbChannel.SelectedIndex = 0 Then
            Campaign.Dayparts.Add(New Trinity.cDaypart)
        Else
            DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Dayparts.Add(New Trinity.cDaypart)
        End If
        grdDayparts.Rows.Add()
    End Sub

    Private Sub grdDayparts_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdDayparts.CellValueNeeded
        Dim Dayparts As Trinity.cDayparts
        If cmbChannel.SelectedIndex = 0 Then
            Dayparts = Campaign.Dayparts
        Else
            Dayparts = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Dayparts
        End If
        If e.RowIndex > Dayparts.Count - 1 Then Exit Sub
        Select Case e.ColumnIndex
            Case 0
                e.Value = Dayparts(e.RowIndex).Name
            Case 1
                e.Value = Trinity.Helper.Mam2Tid(Dayparts(e.RowIndex).StartMaM)
            Case 2
                If Dayparts(e.RowIndex).EndMaM > 24 * 60 - 1 Then
                    e.Value = Trinity.Helper.Mam2Tid(Dayparts(e.RowIndex).EndMaM - 24 * 60)
                Else
                    e.Value = Trinity.Helper.Mam2Tid(Dayparts(e.RowIndex).EndMaM)
                End If
            Case 3
                e.Value = Dayparts(e.RowIndex).IsPrime
        End Select
    End Sub

    Private Sub grdDayparts_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdDayparts.CellValuePushed
        Dim Dayparts As Trinity.cDayparts
        If cmbChannel.SelectedIndex = 0 Then
            Dayparts = Campaign.Dayparts
        Else
            Dayparts = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Dayparts
        End If
        Select Case e.ColumnIndex
            Case 0
                Dayparts(e.RowIndex).Name = e.Value
            Case 1, 2
                Dim h As Integer
                Dim m As Integer
                Dim TmpStr As String = e.Value
                If TmpStr.IndexOf(":") > -1 Then
                    h = TmpStr.Substring(0, TmpStr.IndexOf(":"))
                    m = TmpStr.Substring(TmpStr.IndexOf(":") + 1)
                Else
                    While TmpStr.Length < 4
                        TmpStr = "0" & TmpStr
                    End While
                    h = TmpStr.Substring(0, TmpStr.Length - 2)
                    m = TmpStr.Substring(TmpStr.Length - 2)
                End If
                Select Case e.ColumnIndex
                    Case 1
                        Dayparts(e.RowIndex).StartMaM = h * 60 + m
                    Case 2
                        If h * 60 + m < Dayparts(e.RowIndex).StartMaM Then
                            h = h + 24
                        End If
                        Dayparts(e.RowIndex).EndMaM = h * 60 + m
                End Select
            Case 3
                Dayparts(e.RowIndex).IsPrime = e.Value
        End Select
    End Sub

    Private Sub cmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub cmdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelete.Click
        Dim Dayparts As Trinity.cDayparts
        If cmbChannel.SelectedIndex = 0 Then
            Dayparts = Campaign.Dayparts
        Else
            Dayparts = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Dayparts
        End If
        For Each TmpRow As Windows.Forms.DataGridViewRow In grdDayparts.SelectedRows
            Dayparts.RemoveAt(TmpRow.Index)
            grdDayparts.Rows.Remove(TmpRow)
        Next
    End Sub

    Private Sub cmdSaveToFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveToFile.Click
        If DBReader.isLocal Then
            MsgBox("You are using a Local database and all changes you make will be lost when you connect back to the network.", MsgBoxStyle.Information, "FYI")
        End If

        If System.Windows.Forms.MessageBox.Show("This will overwrite the default dayparts that are saved" & vbCrLf & "on your system." & vbCrLf & vbCrLf & "Are you sure you want to proceed?" & vbCrLf & vbCrLf & "(To save the changes to this campaign only, click the 'Ok' button)", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question, Windows.Forms.MessageBoxDefaultButton.Button2) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        If InputBox("This function is protected with a password." & vbCrLf & "Please enter the password required:", "T R I N I T Y") <> "orange" Then
            System.Windows.Forms.MessageBox.Show("Wrong password!", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            Exit Sub
        End If
        'Dim Ini As New Trinity.clsIni

        'Ini.Create(Trinity.Helper.Pathify(TrinitySettings.DataPath) & Campaign.Area & "\Area.ini")
        'For i As Integer = 0 To Campaign.Dayparts.Count - 1
        '    Ini.Text("Dayparts", "Daypart" & i + 1) = Campaign.Dayparts(i).Name
        '    Ini.Data("Dayparts", "StartTime" & i + 1) = Campaign.Dayparts(i).StartMaM
        '    Ini.Data("Dayparts", "EndTime" & i + 1) = Campaign.Dayparts(i).EndMaM
        'Next
        Dim Node As XmlNode
        Dim XmlRoot As Xml.XmlElement

        Dim XMLDoc As New Xml.XmlDocument
        XMLDoc.PreserveWhitespace = True
        Node = XMLDoc.CreateProcessingInstruction("xml", "version='1.0'")
        XMLDoc.AppendChild(Node)

        Node = XMLDoc.CreateComment("Trinity daypart definition file.")
        XMLDoc.AppendChild(Node)
        Node = XMLDoc.CreateComment("Saved by user " & TrinitySettings.UserName & " at " & Now)
        XMLDoc.AppendChild(Node)

        XmlRoot = XMLDoc.CreateElement("Dayparts")

        Dim XMLDayparts As Xml.XmlElement

        XMLDayparts = XMLDoc.CreateElement("Campaign")
        For Each _dp As Trinity.cDaypart In Campaign.Dayparts
            Dim XMLDaypart As Xml.XmlElement = XMLDoc.CreateElement("Daypart")
            XMLDaypart.SetAttribute("Name", _dp.Name)
            XMLDaypart.SetAttribute("StartMaM", _dp.StartMaM)
            XMLDaypart.SetAttribute("EndMaM", _dp.EndMaM)
            XMLDaypart.SetAttribute("IsPrime", _dp.IsPrime)
            XMLDayparts.AppendChild(XMLDaypart)
        Next
        XmlRoot.AppendChild(XMLDayparts)

        For Each _chan As Trinity.cChannel In Campaign.Channels
            Dim XMLChannel As Xml.XmlElement = XMLDoc.CreateElement("Channel")
            XMLChannel.SetAttribute("Name", _chan.ChannelName)
            XmlRoot.AppendChild(XMLChannel)
            For Each _bt As Trinity.cBookingType In _chan.BookingTypes
                Dim XMLBookingtype As Xml.XmlElement = XMLDoc.CreateElement("Bookingtype")
                XMLBookingtype.SetAttribute("Name", _bt.Name)
                XMLChannel.AppendChild(XMLBookingtype)
                For Each _dp As Trinity.cDaypart In _bt.Dayparts
                    Dim XMLDaypart As Xml.XmlElement = XMLDoc.CreateElement("Daypart")
                    XMLDaypart.SetAttribute("Name", _dp.Name)
                    XMLDaypart.SetAttribute("StartMaM", _dp.StartMaM)
                    XMLDaypart.SetAttribute("EndMaM", _dp.EndMaM)
                    XMLDaypart.SetAttribute("IsPrime", _dp.IsPrime)
                    XMLBookingtype.AppendChild(XMLDaypart)
                Next
            Next
        Next
        XMLDoc.AppendChild(XmlRoot)
        XMLDoc.Save(TrinitySettings.ActiveDataPath & Campaign.Area & "\Dayparts.xml")
    End Sub

    Private Sub cmdUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUp.Click
        If grdDayparts.SelectedRows(0).Index = 0 Then Exit Sub
        Dim Dayparts As Trinity.cDayparts
        If cmbChannel.SelectedIndex = 0 Then
            Dayparts = Campaign.Dayparts
        Else
            Dayparts = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Dayparts
        End If
        Dayparts.SwitchPosition(Dayparts(grdDayparts.SelectedRows(0).Index), Dayparts(grdDayparts.SelectedRows(0).Index - 1))
        grdDayparts.Invalidate()
        grdDayparts.CurrentCell = grdDayparts.Rows(grdDayparts.SelectedRows(0).Index - 1).Cells(0)
    End Sub

    Private Sub cmdDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDown.Click
        If grdDayparts.SelectedRows(0).Index = grdDayparts.Rows.Count - 1 Then Exit Sub
        Dim Dayparts As Trinity.cDayparts
        If cmbChannel.SelectedIndex = 0 Then
            Dayparts = Campaign.Dayparts
        Else
            Dayparts = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Dayparts
        End If
        Dayparts.SwitchPosition(Dayparts(grdDayparts.SelectedRows(0).Index), Dayparts(grdDayparts.SelectedRows(0).Index + 1))
        grdDayparts.Invalidate()
        grdDayparts.CurrentCell = grdDayparts.Rows(grdDayparts.SelectedRows(0).Index + 1).Cells(0)
    End Sub

    Private Sub cmbChannel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbChannel.SelectedIndexChanged
        grdDayparts.Rows.Clear()
        If cmbChannel.SelectedIndex = 0 Then
            grdDayparts.Rows.Add(Campaign.Dayparts.Count)
        Else
            Dim TmpBT As Trinity.cBookingType = cmbChannel.SelectedItem
            If TmpBT.Dayparts.Count > 0 Then
                grdDayparts.Rows.Add(TmpBT.Dayparts.Count)
            End If
        End If

    End Sub

    Private Sub grdDayparts_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdDayparts.CellContentClick

    End Sub

    Private Sub CopyDaypart(ByVal sender As Object, ByVal e As EventArgs)
        DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Dayparts.Clear()
        For Each tmpDP As Trinity.cDaypart In DirectCast(sender.tag.dayparts, Trinity.cDayparts)
            DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Dayparts.Add(tmpDP)
        Next
    End Sub
    Private Sub cmdCopyDaypart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopyDaypart.Click

       
        Dim mnu As New Windows.Forms.ContextMenuStrip
        For Each tmpChan As Trinity.cChannel In Campaign.Channels
            For Each tmpBT As Trinity.cBookingType In tmpChan.BookingTypes
                Dim mnuItem As New Windows.Forms.ToolStripMenuItem
                mnuItem.Text = tmpBT.ToString
                mnuItem.Tag = tmpBT
                AddHandler mnuItem.Click, AddressOf CopyDaypart
                mnu.Items.Add(mnuItem)
            Next
        Next
        mnu.Show(MousePosition)
    End Sub
End Class