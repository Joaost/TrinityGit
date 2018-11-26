Imports System.Xml
Imports System.IO

Public Class frmXMLPricelist
    Dim _XMLDoc As New XmlDocument
    Dim XMLRoot As Xml.XmlNodeList
    Dim _XML As XmlElement
    Dim cpp(10) As Integer
    Dim xmldocFile As New Xml.XmlDocument
    Dim Targets(60) As String

    Public ReadOnly Property Xml()
        Get
            Return _XML
        End Get
    End Property

    Public Property XmlDoc()
        Get
            Return _xmlDoc
        End Get
        Set(ByVal value)
            _xmlDoc = value
            _xml = _xmlDoc.CreateElement("TEMP")
        End Set
    End Property

    Private Sub frmXMLPricelist_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Populate the cmbFile with all XML files
        Dim s As New trinityAdmin.FileSearch("G:\TV\Trinity Data 4.0\", "*.xml", )
        s.Search()

        Dim fi As FileInfo
        Dim strTmp As String

        For i As Integer = 0 To s.Files.Count - 1
            fi = s.Files.Item(i)
            strTmp = fi.FullName
            If strTmp.Contains("Pricelist") Then
                cmbFile.Items.Add(strTmp)
            End If
        Next

    End Sub

    Private Sub cmbFile_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFile.SelectedIndexChanged
        'load the document
        xmldocFile.Load(cmbFile.Items.Item(cmbFile.SelectedIndex).ToString.Trim)

        Dim XMLBT As Xml.XmlElement

        XMLBT = xmldocFile.SelectSingleNode("Pricelist")
        XMLRoot = XMLBT.ChildNodes

        If XMLRoot Is Nothing Then
            MsgBox("That file is no pricelist file", MsgBoxStyle.Information, "Error")
            Exit Sub
        End If

        cmbBT.Items.Clear()
        For Each XMLBT In XMLRoot
            cmbBT.Items.Add(XMLBT.GetAttribute("Name"))
        Next

        If _XML Is Nothing Then
            _XML = _XMLDoc.CreateElement("TEMP")
        End If
    End Sub

    Private Sub cmbBT_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBT.SelectedIndexChanged
        'note that names are according to the XML in the files not what they contains
        Dim XMLBT As Xml.XmlElement
        Dim XMLTarget As Xml.XmlElement
        Dim XMLPrice As Xml.XmlElement

        cmbTarget.Items.Clear()
        For Each XMLBT In XMLRoot
            If XMLBT.GetAttribute("Name") = cmbBT.Items.Item(cmbBT.SelectedIndex).ToString.Trim Then
                For Each XMLTarget In XMLBT.ChildNodes
                    For Each XMLPrice In XMLTarget.ChildNodes
                        'read the whole Target and add it to the combo
                        cmbTarget.Items.Add(XMLPrice.GetAttribute("Target"))
                        Targets(cmbTarget.Items.Count - 1) = XMLPrice.GetAttribute("Target")
                    Next
                Next
            End If
        Next

        grbPricelist.Enabled = True
    End Sub

    Private Sub cmbTarget_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTarget.SelectedIndexChanged
        Dim XMLBT As Xml.XmlElement
        Dim XMLTarget As Xml.XmlElement
        Dim XMLPrice As Xml.XmlElement
        Dim XMLIndex As Xml.XmlElement

        Dim i As Integer
        Dim j As Integer
        Dim z As Integer

        'clear all items
        grdPricelist.Rows.Clear()
        txtAdedgeTarget.Text = ""
        txtAdedgeTarget.Tag = ""
        txtChnUni.Text = ""
        txtChnUni.Tag = ""
        txtNatUni.Text = ""
        txtNatUni.Tag = ""
        chkCalcCPP.Checked = False
        chkCalcCPP.Tag = Nothing
        chkStandard.Checked = False
        chkStandard.Tag = Nothing

        While grdPricelist.Columns.Count > 4
            grdPricelist.Columns.RemoveAt(4)
        End While

        For Each XMLBT In XMLRoot
            If XMLBT.GetAttribute("Name") = cmbBT.Items.Item(cmbBT.SelectedIndex).ToString.Trim Then
                For Each XMLTarget In XMLBT.ChildNodes
                    For Each XMLPrice In XMLTarget.ChildNodes
                        If XMLPrice.GetAttribute("Target") = cmbTarget.SelectedItem Then
                            txtAdedgeTarget.Text = XMLPrice.GetAttribute("AdedgeTarget")
                            txtAdedgeTarget.Tag = txtAdedgeTarget.Text
                            txtChnUni.Text = XMLPrice.GetAttribute("TargetUni")
                            txtChnUni.Tag = txtChnUni.Text
                            txtNatUni.Text = XMLPrice.GetAttribute("TargetNat")
                            txtNatUni.Tag = txtNatUni.Text
                            chkCalcCPP.Checked = XMLPrice.GetAttribute("CalcCPP")
                            chkCalcCPP.Tag = chkCalcCPP.Checked
                            chkStandard.Checked = XMLPrice.GetAttribute("StandardTarget")
                            chkStandard.Tag = chkStandard.Checked
                            Try
                                j = 1
                                cpp(0) = XMLPrice.GetAttribute("CPP")
                                While j < 10
                                    cpp(j) = XMLPrice.GetAttribute("CPP_DP" & j.ToString)
                                    j += 1
                                End While
                            Catch ex As Exception
                                'nothing
                            End Try
                            j = j - 1 ' J is now the number of dayparts

                            For i = 1 To j
                                Dim TmpCol As New System.Windows.Forms.DataGridViewTextBoxColumn
                                TmpCol.Name = "colCPP" & i
                                TmpCol.HeaderText = "CPP DP" & i
                                TmpCol.Tag = i
                                grdPricelist.Columns.Add(TmpCol)
                            Next

                            grdPricelist.Rows.Clear()
                            For Each XMLIndex In XMLPrice.ChildNodes
                                i = grdPricelist.Rows.Add()
                                grdPricelist.Rows(i).Cells(0).Value = XMLIndex.GetAttribute("Name")
                                grdPricelist.Rows(i).Cells(0).Tag = grdPricelist.Rows(i).Cells(0).Value
                                grdPricelist.Rows(i).Cells(1).Value = XMLIndex.GetAttribute("FromDate")
                                grdPricelist.Rows(i).Cells(1).Tag = grdPricelist.Rows(i).Cells(1).Value
                                grdPricelist.Rows(i).Cells(2).Value = XMLIndex.GetAttribute("ToDate")
                                grdPricelist.Rows(i).Cells(2).Tag = grdPricelist.Rows(i).Cells(2).Value
                                'get all dayparts
                                z = 0
                                grdPricelist.Rows(i).Cells(3).Value = Format(XMLIndex.GetAttribute("Index") * cpp(z) / 100, "N1")
                                grdPricelist.Rows(i).Cells(3).Tag = grdPricelist.Rows(i).Cells(3).Value
                                For z = 0 To j - 1
                                    grdPricelist.Rows(i).Cells(z + 4).Value = Format(XMLIndex.GetAttribute("IndexDP" & z.ToString) * cpp(z + 1) / 100, "N1")
                                    grdPricelist.Rows(i).Cells(z + 4).Tag = grdPricelist.Rows(i).Cells(z + 4).Value
                                Next
                            Next
                            GoTo _End
                        End If
                    Next
                Next
            End If
        Next
_End:
        If Targets(cmbTarget.SelectedIndex) = "DEL" Then
            lblDELETE.Visible = True
        Else
            lblDELETE.Visible = False
        End If
    End Sub

    Private Sub cmdRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
        Dim i As Integer
        grdPricelist.Rows(grdPricelist.SelectedCells(0).RowIndex).Tag = "DEL"
        For i = 0 To grdPricelist.Columns.Count - 1
            grdPricelist.Rows(grdPricelist.SelectedCells(0).RowIndex).Cells(i).Style.BackColor = Color.Red
        Next
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Dim i As Integer
        If grdPricelist.Rows.Count = 0 Then
            Dim frm As New frmInput
            frm.Text = "Set number of day parts"
            frm.ShowDialog()
            If frm.DialogResult = Windows.Forms.DialogResult.Cancel Then Exit Sub
            For i = 1 To CInt(frm.TextBox1.Text)
                grdPricelist.Columns.Add("daypart " & i.ToString, "daypart " & i.ToString)
            Next
        End If

        i = grdPricelist.Rows.Add
        grdPricelist.Rows(i).Tag = "NEW"
        grdPricelist.Rows(i).Cells(1).Value = Date.Now
        grdPricelist.Rows(i).Cells(2).Value = Date.Now
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        'save the changes made
        Dim xmlTmp As Xml.XmlElement
        Dim xmlB As Xml.XmlElement
        Dim node As Xml.XmlElement

        Dim z As Integer
        Dim i As Integer
        Dim j As Integer

        'if the cpp array is empty we need to fill it before we save hte prices
        If cpp(0) = 0 And cpp(1) = 0 Then
            z = 3
            While z < grdPricelist.Columns.Count
                cpp(z - 3) = grdPricelist.Rows(0).Cells(z).Value
                z += 1
            End While

        End If

        'check if theres as new or deleted Target
        If Targets(cmbTarget.SelectedIndex) = "DEL" Then
            'set the "where" node
            node = _XMLDoc.CreateElement("Change")
            node.SetAttribute("Type", "DeleteRow")
            node.SetAttribute("Name", "Target")
            node.SetAttribute("Value", cmbTarget.SelectedItem)

            xmlB = _XMLDoc.CreateElement("Targets")
            xmlB.AppendChild(node)
            xmlTmp = _XMLDoc.CreateElement("Price")
            xmlTmp.SetAttribute("Name", cmbBT.SelectedItem)
            xmlTmp.AppendChild(xmlB)
            xmlB = _XMLDoc.CreateElement("Pricelist")
            xmlB.AppendChild(xmlTmp)

            Dim FileNode As Xml.XmlElement = _XMLDoc.CreateElement("XML")
            FileNode.SetAttribute("FileName", cmbFile.SelectedItem.ToString.Substring(cmbFile.SelectedItem.ToString.IndexOf("Trinity Data 4.0") + 17))

            FileNode.AppendChild(xmlB)
            _XML.AppendChild(FileNode)

            Exit Sub
        Else
            'if its a new Target or a old
            If Targets(cmbTarget.SelectedIndex) = "NEW" Then
                'set the "where" node
                node = _XMLDoc.CreateElement("Change")
                node.SetAttribute("Type", "AddRow")
                node.SetAttribute("Name", "Price")

                xmlTmp = _XMLDoc.CreateElement("Attribute")
                xmlTmp.SetAttribute("Name", "Target")
                xmlTmp.SetAttribute("Value", cmbTarget.SelectedItem)
                node.AppendChild(xmlTmp)

                'we need to save the daypart index 100 to the target aswell
                xmlTmp = _XMLDoc.CreateElement("Attribute")
                xmlTmp.SetAttribute("Name", "CPP")
                xmlTmp.SetAttribute("Value", cpp(0))
                node.AppendChild(xmlTmp)
                z = 4
                While z < grdPricelist.Columns.Count
                    xmlTmp = _XMLDoc.CreateElement("Attribute")
                    xmlTmp.SetAttribute("Name", "CPP_DP" & CInt(z - 4).ToString)
                    xmlTmp.SetAttribute("Value", cpp(z - 3))
                    node.AppendChild(xmlTmp)
                    z += 1
                End While

                'if its a new Target the .tag will be empty so all will be saved
                If Not txtAdedgeTarget.Tag = txtAdedgeTarget.Text Then
                    xmlTmp = _XMLDoc.CreateElement("Attribute")
                    xmlTmp.SetAttribute("Name", "AdedgeTarget")
                    xmlTmp.SetAttribute("Value", txtAdedgeTarget.Text)
                    node.AppendChild(xmlTmp)
                End If
                If Not txtNatUni.Tag = txtNatUni.Text Then
                    xmlTmp = _XMLDoc.CreateElement("Attribute")
                    xmlTmp.SetAttribute("Name", "TargetNat")
                    xmlTmp.SetAttribute("Value", txtNatUni.Text)
                    node.AppendChild(xmlTmp)
                End If
                If Not txtChnUni.Tag = txtChnUni.Text Then
                    xmlTmp = _XMLDoc.CreateElement("Attribute")
                    xmlTmp.SetAttribute("Name", "TargetUni")
                    xmlTmp.SetAttribute("Value", txtChnUni.Text)
                    node.AppendChild(xmlTmp)
                End If
                If Not txtIdx100CPP.Tag = txtIdx100CPP.Text Then
                    'vad gör denna?
                End If
                If Not chkCalcCPP.Checked = chkCalcCPP.Tag Then
                    xmlTmp = _XMLDoc.CreateElement("Attribute")
                    xmlTmp.SetAttribute("Name", "CalcCPP")
                    xmlTmp.SetAttribute("Value", chkCalcCPP.Checked)
                    node.AppendChild(xmlTmp)
                End If
                If Not chkStandard.Checked = chkStandard.Tag Then
                    xmlTmp = _XMLDoc.CreateElement("Attribute")
                    xmlTmp.SetAttribute("Name", "StandardTarget")
                    xmlTmp.SetAttribute("Value", chkStandard.Checked)
                    node.AppendChild(xmlTmp)
                End If
            Else
                'check for edits EDIT
                'set the "where" node
                node = _XMLDoc.CreateElement("Change")
                node.SetAttribute("Name", "Target")
                node.SetAttribute("Type", "Edit")
                node.SetAttribute("Value", cmbTarget.SelectedItem)


                'if its a new Target the .tag will be empty so all will be saved
                If Not txtAdedgeTarget.Tag = txtAdedgeTarget.Text Then
                    xmlTmp = _XMLDoc.CreateElement("Attribute")
                    xmlTmp.SetAttribute("Name", "AdedgeTarget")
                    xmlTmp.SetAttribute("From", txtAdedgeTarget.Tag)
                    xmlTmp.SetAttribute("To", txtAdedgeTarget.Text)
                    xmlTmp.SetAttribute("AddIfNotFound", chkAddIfNotFound.Checked)
                    node.AppendChild(xmlTmp)
                End If
                If Not txtNatUni.Tag = txtNatUni.Text Then
                    xmlTmp = _XMLDoc.CreateElement("Attribute")
                    xmlTmp.SetAttribute("Name", "TargetNat")
                    xmlTmp.SetAttribute("From", txtNatUni.Tag)
                    xmlTmp.SetAttribute("To", txtNatUni.Text)
                    xmlTmp.SetAttribute("AddIfNotFound", chkAddIfNotFound.Checked)
                    node.AppendChild(xmlTmp)
                End If
                If Not txtChnUni.Tag = txtChnUni.Text Then
                    xmlTmp = _XMLDoc.CreateElement("Attribute")
                    xmlTmp.SetAttribute("Name", "TargetUni")
                    xmlTmp.SetAttribute("From", txtChnUni.Tag)
                    xmlTmp.SetAttribute("To", txtChnUni.Text)
                    xmlTmp.SetAttribute("AddIfNotFound", chkAddIfNotFound.Checked)
                    node.AppendChild(xmlTmp)
                End If
                If Not txtIdx100CPP.Tag = txtIdx100CPP.Text Then
                    'vad gör denna?
                End If
                If Not chkCalcCPP.Checked = chkCalcCPP.Tag Then
                    xmlTmp = _XMLDoc.CreateElement("Attribute")
                    xmlTmp.SetAttribute("Name", "CalcCPP")
                    xmlTmp.SetAttribute("From", chkCalcCPP.Tag)
                    xmlTmp.SetAttribute("To", chkCalcCPP.Checked)
                    xmlTmp.SetAttribute("AddIfNotFound", chkAddIfNotFound.Checked)
                    node.AppendChild(xmlTmp)
                End If
                If Not chkStandard.Checked = chkStandard.Tag Then
                    xmlTmp = _XMLDoc.CreateElement("Attribute")
                    xmlTmp.SetAttribute("Name", "StandardTarget")
                    xmlTmp.SetAttribute("From", chkStandard.Tag)
                    xmlTmp.SetAttribute("To", chkStandard.Checked)
                    xmlTmp.SetAttribute("AddIfNotFound", chkAddIfNotFound.Checked)
                    node.AppendChild(xmlTmp)
                End If
            End If

            'if one of the attributes where changed we save them
            If Not node.InnerXml = "" Then
                xmlB = _XMLDoc.CreateElement("Targets")
                xmlB.AppendChild(node)
                xmlTmp = _XMLDoc.CreateElement("Price")
                xmlTmp.SetAttribute("Name", cmbBT.SelectedItem)
                xmlTmp.AppendChild(xmlB)
                xmlB = _XMLDoc.CreateElement("Pricelist")
                xmlB.AppendChild(xmlTmp)

                Dim FileNode As Xml.XmlElement = _XMLDoc.CreateElement("XML")
                FileNode.SetAttribute("FileName", cmbFile.SelectedItem.ToString.Substring(cmbFile.SelectedItem.ToString.IndexOf("Trinity Data 4.0") + 17))

                FileNode.AppendChild(xmlB)
                _XML.AppendChild(FileNode)
            End If 'end if the channel attributes

            Dim priceNode As Xml.XmlElement
            For i = 0 To grdPricelist.Rows.Count - 1
                priceNode = Nothing

                'check for a new row or a Row that is to be deleted
                If grdPricelist.Rows(i).Tag = "NEW" Then
                    priceNode = _XMLDoc.CreateElement("Change")
                    priceNode.SetAttribute("Type", "AddRow")
                    priceNode.SetAttribute("Name", "Index")
                    xmlTmp = _XMLDoc.CreateElement("Attribute")
                    xmlTmp.SetAttribute("Name", "Name")
                    xmlTmp.SetAttribute("Value", grdPricelist.Rows(i).Cells(0).Value)
                    priceNode.AppendChild(xmlTmp)
                    xmlTmp = _XMLDoc.CreateElement("Attribute")
                    xmlTmp.SetAttribute("Name", "FromDate")
                    xmlTmp.SetAttribute("Value", grdPricelist.Rows(i).Cells(1).Value.ToString.Substring(0, 10))
                    priceNode.AppendChild(xmlTmp)
                    xmlTmp = _XMLDoc.CreateElement("Attribute")
                    xmlTmp.SetAttribute("Name", "ToDate")
                    xmlTmp.SetAttribute("Value", grdPricelist.Rows(i).Cells(2).Value.ToString.Substring(0, 10))
                    priceNode.AppendChild(xmlTmp)
                    xmlTmp = _XMLDoc.CreateElement("Attribute")
                    xmlTmp.SetAttribute("Name", "Index")
                    xmlTmp.SetAttribute("Value", grdPricelist.Rows(i).Cells(3).Value / cpp(0) * 100)
                    priceNode.AppendChild(xmlTmp)
                    For j = 0 To grdPricelist.Columns.Count - 5
                        xmlTmp = _XMLDoc.CreateElement("Attribute")
                        xmlTmp.SetAttribute("Name", "IndexDP" + j.ToString)
                        If grdPricelist.Rows(i).Cells(j + 4).Value = 0 Then
                            xmlTmp.SetAttribute("Value", "0")
                        Else
                            xmlTmp.SetAttribute("Value", grdPricelist.Rows(i).Cells(j + 4).Value / cpp(j + 1) * 100)
                        End If
                        priceNode.AppendChild(xmlTmp)
                    Next
                ElseIf grdPricelist.Rows(i).Tag = "DEL" Then
                    priceNode = _XMLDoc.CreateElement("Change")
                    priceNode.SetAttribute("Type", "DeleteRow")
                    priceNode.SetAttribute("Name", "Index")
                    xmlTmp = _XMLDoc.CreateElement("Attribute")
                    xmlTmp.SetAttribute("Name", "Name")
                    xmlTmp.SetAttribute("Value", grdPricelist.Rows(i).Cells(0).Value)
                    priceNode.AppendChild(xmlTmp)
                Else
                    'check each cell for changes
                    'if not NEW or DEL step through all cells
                    priceNode = _XMLDoc.CreateElement("Change")
                    priceNode.SetAttribute("Type", "Edit")
                    priceNode.SetAttribute("Name", "Name")
                    priceNode.SetAttribute("Value", grdPricelist.Rows(i).Cells(0).Value)

                    For j = 0 To grdPricelist.Columns.Count - 1
                        If Not grdPricelist.Rows(i).Cells(j).Value = grdPricelist.Rows(i).Cells(j).Tag Then
                            xmlTmp = _XMLDoc.CreateElement("Attribute")
                            Select Case j
                                Case Is = 3
                                    xmlTmp.SetAttribute("Name", "Index")
                                Case Is = 4
                                    xmlTmp.SetAttribute("Name", "IndexDP0")
                                Case Is = 5
                                    xmlTmp.SetAttribute("Name", "IndexDP1")
                                Case Is = 6
                                    xmlTmp.SetAttribute("Name", "IndexDP2")
                                Case Is = 1
                                    xmlTmp.SetAttribute("Name", "FromDate")
                                Case Is = 2
                                    xmlTmp.SetAttribute("Name", "ToDate")
                            End Select
                            If j > 2 Then
                                xmlTmp.SetAttribute("From", CDbl(grdPricelist.Rows(i).Cells(j).Tag))
                                xmlTmp.SetAttribute("To", CDbl(grdPricelist.Rows(i).Cells(j).Value))
                            Else
                                xmlTmp.SetAttribute("From", grdPricelist.Rows(i).Cells(j).Tag)
                                xmlTmp.SetAttribute("To", grdPricelist.Rows(i).Cells(j).Value)
                            End If

                            xmlTmp.SetAttribute("AddIfNotFound", chkAddIfNotFound.Checked)
                            priceNode.AppendChild(xmlTmp)
                        End If
                    Next
                End If

                'if one of the attributes where changed we save them
                If Not priceNode.InnerXml = "" Then
                    xmlTmp = _XMLDoc.CreateElement("Price")
                    xmlTmp.SetAttribute("Target", cmbTarget.SelectedItem)
                    xmlTmp.AppendChild(priceNode)
                    xmlB = _XMLDoc.CreateElement("Targets")
                    xmlB.AppendChild(xmlTmp)
                    xmlTmp = _XMLDoc.CreateElement("Price")
                    xmlTmp.SetAttribute("Name", cmbBT.SelectedItem)
                    xmlTmp.AppendChild(xmlB)
                    xmlB = _XMLDoc.CreateElement("Pricelist")
                    xmlB.AppendChild(xmlTmp)

                    Dim FileNode As Xml.XmlElement = _XMLDoc.CreateElement("XML")
                    FileNode.SetAttribute("FileName", cmbFile.SelectedItem.ToString.Substring(cmbFile.SelectedItem.ToString.IndexOf("Trinity Data 4.0") + 17))

                    FileNode.AppendChild(xmlB)
                    _XML.AppendChild(FileNode)
                End If 'end if the channel attributes





            Next
        End If



        MsgBox("All changes where saved")
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub cmdCalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCalculate.Click
        
    End Sub

    Private Sub cmdWizard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdWizard.Click
        mnuWizard.Show(cmdWizard, New System.Drawing.Point(0, cmdWizard.Height))
    End Sub

    Private Sub cmdCopyTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopyTarget.Click
        Dim mnuCopyTarget As New Windows.Forms.ContextMenuStrip
        
        For Each TmpTarget As String In cmbTarget.Items
            If TmpTarget <> cmbTarget.SelectedItem Then
                With mnuCopyTarget.Items.Add(TmpTarget, Nothing, AddressOf CopyPricelistTarget)
                    .Tag = TmpTarget
                End With
            End If
        Next
        mnuCopyTarget.Show(cmdCopyTarget, 0, cmdCopy.Height)
    End Sub

    Sub CopyPricelistTarget(ByVal sender As Object, ByVal e As EventArgs)
        'If Windows.Forms.MessageBox.Show("This will replace the pricelist for " & cmbTarget.Text & "." & vbCrLf & "Are you sure you want to proceed?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then Exit Sub
        'read the target from the file


    End Sub

    Private Sub mnuWeekOn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CreateWeeklyToolStripMenuItem.Click
        Dim d As Long
        For d = Trinity.Helper.MondayOfWeek(Year(Now), 1).ToOADate To Trinity.Helper.MondayOfWeek(Year(Now), 52).AddDays(6).ToOADate Step 7
            Dim i As Integer = grdPricelist.Rows.Add
            grdPricelist.Rows(i).Tag = "NEW"
            grdPricelist.Rows(i).Cells(0).Value = Date.FromOADate(d).ToString.Substring(0, 10)
            grdPricelist.Rows(i).Cells(1).Value = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 1).ToString.Substring(0, 10)
            grdPricelist.Rows(i).Cells(2).Value = Date.FromOADate(d - Weekday(Date.FromOADate(d), vbMonday) + 7).ToString.Substring(0, 10)
        Next

    End Sub

    Private Sub mnuMonth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuMonth.Click
        Dim d As Long
        For d = Trinity.Helper.MondayOfWeek(Year(Now), 1).ToOADate To Trinity.Helper.MondayOfWeek(Year(Now), 52).AddDays(6).ToOADate Step 31
            Dim i As Integer = grdPricelist.Rows.Add
            grdPricelist.Rows(i).Tag = "NEW"
            grdPricelist.Rows(i).Cells(0).Value = Date.FromOADate(d).ToString.Substring(0, 10)
            grdPricelist.Rows(i).Cells(1).Value = Date.FromOADate(d - DatePart(DateInterval.Day, Date.FromOADate(d)) + 1).ToString.Substring(0, 10)
            grdPricelist.Rows(i).Cells(2).Value = CDate(Year(Date.FromOADate(d)) & "-" & Month(Date.FromOADate(d)) & "-" & DatePart(DateInterval.Day, Date.FromOADate(d + 31 - DatePart(DateInterval.Day, Date.FromOADate(d + 31))))).ToString.Substring(0, 10)
        Next

    End Sub

    Private Sub cmdAddTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddTarget.Click
        'get the name of the new Target
        Dim TmpStr As String = InputBox("Name of target:", "T R I N I T Y", Nothing)
        If TmpStr Is Nothing Or TmpStr = "" Then Exit Sub

        cmbTarget.Items.Add(TmpStr)
        Targets(cmbTarget.Items.Count - 1) = "NEW"
    End Sub

    Private Sub cmdDeleteTarget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteTarget.Click
        Targets(cmbTarget.SelectedIndex) = "DEL"
        lblDELETE.Visible = True
    End Sub

    Private Sub cmdCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopy.Click

    End Sub

    Private Sub grdPricelist_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPricelist.CellEndEdit
        If e.ColumnIndex > 2 AndAlso chkCalcCPP.Checked Then
            Try
                Dim i As Integer = grdPricelist.Columns.Count - 1

                Dim sum As Integer = 0
                While i > 3
                    sum += grdPricelist.Rows(e.RowIndex).Cells(i).Value
                    i -= 1
                End While
                grdPricelist.Rows(e.RowIndex).Cells(3).Value = sum / (grdPricelist.Columns.Count - 4)
            Catch

            End Try
        End If
    End Sub
End Class