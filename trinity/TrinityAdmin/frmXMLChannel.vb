Imports System.Xml
Imports system.io

Public Class frmXMLChannel
    Dim _XMLDoc As New XmlDocument
    Dim _XMLDocChan As New XmlDocument
    Dim _XML As XmlElement
    Dim _XMLDocAdr As New XmlDocument
    Dim _XMLAdr As XmlElement

    Dim newChannel As Boolean = False

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

    Private Sub cmbFile_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFile.SelectedIndexChanged
        'load the document

        _XMLDocChan.Load(cmbFile.Items.Item(cmbFile.SelectedIndex).ToString.Trim)
        cmbChannel.Items.Clear()


        Dim XMLChannels As Xml.XmlElement
        Dim XMLTmpNode As Xml.XmlElement

        XMLChannels = _XMLDocChan.GetElementsByTagName("Data").Item(0).SelectSingleNode("Channels")

        If XMLChannels Is Nothing Then
            MsgBox("That file is no channel file", MsgBoxStyle.Information, "Error")
            Exit Sub
        End If

        XMLTmpNode = XMLChannels.ChildNodes.Item(0)

        While Not xmlTmpNode Is Nothing
            cmbChannel.Items.Add(XMLTmpNode.GetAttribute("Name"))
            XMLTmpNode = XMLTmpNode.NextSibling
        End While

        If _XML Is Nothing Then
            _XML = _XMLDoc.CreateElement("TEMP")
        End If

    End Sub

    Private Sub cmbChannel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbChannel.SelectedIndexChanged
        If newChannel Then
            txtName.Text = cmbChannel.Items.Item(cmbChannel.SelectedIndex).ToString.Trim
            txtName.Tag = txtName.Text 'the name is save first on new channels
            Exit Sub
        End If


        Dim XMLChannels As Xml.XmlElement
        Dim XMLTmpNode As Xml.XmlElement
        Dim XMLTmpNode2 As Xml.XmlElement
        Dim XMLBookingTypes As Xml.XmlElement

        lblDELETE.Visible = False
        newChannel = False

        XMLChannels = _XMLDocChan.GetElementsByTagName("Data").Item(0).SelectSingleNode("Channels")

        XMLTmpNode = XMLChannels.ChildNodes.Item(0)

        While Not XMLTmpNode Is Nothing
            If XMLTmpNode.GetAttribute("Name") = cmbChannel.Items.Item(cmbChannel.SelectedIndex).ToString.Trim Then
                txtName.Text = XMLTmpNode.GetAttribute("Name")
                txtName.Tag = txtName.Text
                txtShortName.Text = XMLTmpNode.GetAttribute("Shortname")
                txtShortName.Tag = txtShortName.Text
                txtListNr.Text = XMLTmpNode.GetAttribute("ListNumber")
                txtListNr.Tag = txtListNr.Text
                txtAdedge.Text = XMLTmpNode.GetAttribute("AdEdgeNames")
                txtAdedge.Tag = txtAdedge.Text
                txtCommission.Text = XMLTmpNode.GetAttribute("AgencyCommission") * 100
                txtCommission.Tag = XMLTmpNode.GetAttribute("AgencyCommission")
                txtConnected.Text = XMLTmpNode.GetAttribute("ConnectedChannel")
                txtConnected.Tag = txtConnected.Text
                txtMarathon.Text = XMLTmpNode.GetAttribute("Marathon")
                txtMarathon.Tag = txtMarathon.Text
                txtBuyingUni.Text = XMLTmpNode.GetAttribute("BuyingUniverse")
                txtBuyingUni.Tag = txtBuyingUni.Text
                txtLogo.Text = XMLTmpNode.GetAttribute("Logo")
                txtLogo.Tag = txtLogo.Text

                grdBT.Rows.Clear()

                Dim i As Integer
                XMLBookingTypes = XMLTmpNode.SelectSingleNode("Bookingtypes")
                If XMLBookingTypes Is Nothing Then
                    XMLBookingTypes = _XMLDocChan.GetElementsByTagName("Data").Item(0).SelectSingleNode("Bookingtypes")
                End If
                XMLTmpNode2 = XMLBookingTypes.ChildNodes.Item(0)
                While Not XMLTmpNode2 Is Nothing
                    i = grdBT.Rows.Add()
                    grdBT.Rows(i).Cells(0).Value = XMLTmpNode2.GetAttribute("Name")
                    grdBT.Rows(i).Cells(0).Tag = grdBT.Rows(i).Cells(0).Value
                    grdBT.Rows(i).Cells(1).Value = XMLTmpNode2.GetAttribute("Shortname")
                    grdBT.Rows(i).Cells(1).Tag = grdBT.Rows(i).Cells(1).Value
                    grdBT.Rows(i).Cells(2).Value = XMLTmpNode2.GetAttribute("IsRBS")
                    grdBT.Rows(i).Cells(2).Tag = grdBT.Rows(i).Cells(2).Value
                    grdBT.Rows(i).Cells(3).Value = XMLTmpNode2.GetAttribute("IsSpecific")
                    grdBT.Rows(i).Cells(3).Tag = grdBT.Rows(i).Cells(3).Value
                    grdBT.Rows(i).Cells(4).Value = XMLTmpNode2.GetAttribute("PrintDayparts")
                    grdBT.Rows(i).Cells(4).Tag = grdBT.Rows(i).Cells(4).Value
                    grdBT.Rows(i).Cells(5).Value = XMLTmpNode2.GetAttribute("PrintBookingCode")
                    grdBT.Rows(i).Cells(5).Tag = grdBT.Rows(i).Cells(5).Value

                    XMLTmpNode2 = XMLTmpNode2.NextSibling
                End While

                'gets the other info from the other XML file if it exist
                If My.Computer.FileSystem.FileExists(cmbFile.SelectedItem.ToString.Substring(0, cmbFile.SelectedItem.ToString.LastIndexOf("\") + 1) & "Channel info\" & txtName.Text & ".xml") Then
                    Dim strFile As String = cmbFile.Items.Item(cmbFile.SelectedIndex).ToString.Trim
                    _XMLDocAdr.Load(strFile.Substring(0, strFile.LastIndexOf("\")) & "\Channel info\" & txtName.Text & ".xml")
                    txtDelivery.Text = _XMLDocAdr.GetElementsByTagName("Address").Item(0).InnerText
                    txtDelivery.Tag = txtDelivery.Text
                    txtVHS.Text = _XMLDocAdr.GetElementsByTagName("Address").Item(0).InnerText
                    txtVHS.Tag = txtVHS.Text

                    If _XMLAdr Is Nothing Then
                        _XMLAdr = _XMLDocAdr.CreateElement("TEMP")
                    End If
                End If
            End If

            XMLTmpNode = XMLTmpNode.NextSibling
        End While
    End Sub

    Private Sub frmXMLChannel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Populate the cmbFile with all XML files
        Dim s As New trinityAdmin.FileSearch("G:\TV\Trinity Data 4.0\", "*.xml", )
        s.Search()

        Dim fi As FileInfo
        Dim strTmp As String

        For i As Integer = 0 To s.Files.Count - 1
            fi = s.Files.Item(i)
            strTmp = fi.FullName
            If Not strTmp.Contains("Pricelist") Then
                cmbFile.Items.Add(strTmp)
            End If
        Next
    End Sub

    Private Sub cmdAddChannel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddChannel.Click

        Dim frm As New frmInput
        frm.Text = "Enter channel name:"
        frm.ShowDialog()

        If frm.DialogResult = Windows.Forms.DialogResult.Cancel Then Exit Sub

        newChannel = True
        cmbChannel.Items.Add(frm.TextBox1.Text)

    End Sub

    Private Sub cmdDeleteChannel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteChannel.Click
        lblDELETE.Visible = True
    End Sub

    Private Sub cmdAddBT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddBT.Click
        Dim i As Integer = grdBT.Rows.Add()
        grdBT.Rows(i).Tag = "NEW"
    End Sub

    Private Sub cmdDeleteBT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteBT.Click
        grdBT.SelectedRows(0).Tag = "DEL"
        Dim i As Integer
        For i = 0 To 5
            grdBT.SelectedRows(0).Cells(i).Style.BackColor = Color.Red
        Next
        grdBT.ClearSelection()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim xmlTmp As Xml.XmlElement
        Dim node As Xml.XmlElement
        Dim xmlB As Xml.XmlElement
        Dim xmlB2 As Xml.XmlNode
        Dim path As Xml.XmlElement
        Dim parent As Xml.XmlElement
        Dim xmlAT As Xml.XmlAttribute
        Dim PathTmp As Xml.XmlElement
        Dim xmlList As Xml.XmlElement = _XMLDoc.CreateElement("TMP")
        Dim j As Integer
        Dim i As Integer
        Dim z As Integer = 0

        'if we are to delete the channel
        If lblDELETE.Visible = True Then
            node = _XMLDoc.CreateElement("Change")
            node.SetAttribute("Type", "DeleteRow")
            node.SetAttribute("Name", "Name")
            node.SetAttribute("Value", txtName.Tag)

            z += 1
            'we get the path to the channel
            xmlB2 = _XMLDocChan.SelectSingleNode("//Channel[@Name='" + txtName.Tag + "']")

            xmlB = _XMLDoc.CreateElement(xmlB2.Name)
            i = 0
            For Each xm As Xml.XmlAttribute In xmlB2.Attributes
                If i = 1 Then Exit For
                i += 1
                xmlB.SetAttribute(xm.Name, xm.Value)
            Next

            xmlB.AppendChild(node)
            path = xmlB

            Try
                parent = xmlB2.ParentNode
            Catch
                parent = Nothing
            End Try

            j = 0
            'get the rest of the path
            While Not parent Is Nothing
                PathTmp = _XMLDoc.CreateElement(parent.Name)
                For Each xmlAT In parent.Attributes
                    If j = 1 Then Exit For 'only need a certain number of attributes to know our path
                    j += 1
                    PathTmp.SetAttribute(xmlAT.Name, xmlAT.Value)
                Next
                PathTmp.AppendChild(path)
                path = PathTmp
                Try
                    parent = parent.ParentNode
                Catch
                    parent = Nothing
                End Try
                j = 0
            End While
            'finnished getting the path

            Dim FileNode As Xml.XmlElement = _XMLDoc.CreateElement("XML")
            FileNode.SetAttribute("FileName", cmbFile.SelectedItem.ToString.Substring(cmbFile.SelectedItem.ToString.IndexOf("Trinity Data 4.0") + 17))

            FileNode.AppendChild(path)
            _XML.AppendChild(FileNode)

            MsgBox("Save succesfull", MsgBoxStyle.Information)
            Exit Sub
        End If


        'if we have made a new channel we need to save the channel first
        If newChannel Then
            node = _XMLDoc.CreateElement("Change")
            node.SetAttribute("Type", "AddRow")
            node.SetAttribute("Name", "Channel")

            xmlB = _XMLDoc.CreateElement("Attribute")
            xmlB.SetAttribute("Name", "Name")
            xmlB.SetAttribute("Value", txtName.Tag)

            node.AppendChild(xmlB)

            xmlB2 = _XMLDoc.CreateElement("Channels")
            xmlB2.AppendChild(node)
            path = _XMLDoc.CreateElement("Data")
            path.AppendChild(xmlB2)

            Dim FileNode As Xml.XmlElement = _XMLDoc.CreateElement("XML")
            FileNode.SetAttribute("FileName", cmbFile.SelectedItem.ToString.Substring(cmbFile.SelectedItem.ToString.IndexOf("Trinity Data 4.0") + 17))

            FileNode.AppendChild(path)
            _XML.AppendChild(FileNode)

        End If


        'get the changes
        'set the "where" node
        node = _XMLDoc.CreateElement("Change")
        node.SetAttribute("Type", "Edit")
        node.SetAttribute("Name", "Name")
        node.SetAttribute("Value", txtName.Tag)

        If Not txtName.Tag = txtName.Text Then
            xmlTmp = _XMLDoc.CreateElement("Attribute")
            xmlTmp.SetAttribute("Name", "Name")
            xmlTmp.SetAttribute("From", txtName.Tag)
            xmlTmp.SetAttribute("To", txtName.Text)
            xmlTmp.SetAttribute("AddIfNotFound", chkAddIfNotFound.Checked)
            node.AppendChild(xmlTmp)
        End If
        If Not txtAdedge.Tag = txtAdedge.Text Then
            xmlTmp = _XMLDoc.CreateElement("Attribute")
            xmlTmp.SetAttribute("Name", "AdEdgeNames")
            xmlTmp.SetAttribute("From", txtAdedge.Tag)
            xmlTmp.SetAttribute("To", txtAdedge.Text)
            xmlTmp.SetAttribute("AddIfNotFound", chkAddIfNotFound.Checked)
            node.AppendChild(xmlTmp)
        End If
        If Not txtMarathon.Tag = txtMarathon.Text Then
            xmlTmp = _XMLDoc.CreateElement("Attribute")
            xmlTmp.SetAttribute("Name", "Marathon")
            xmlTmp.SetAttribute("From", txtMarathon.Tag)
            xmlTmp.SetAttribute("To", txtMarathon.Text)
            xmlTmp.SetAttribute("AddIfNotFound", chkAddIfNotFound.Checked)
            node.AppendChild(xmlTmp)
        End If
        If Not txtLogo.Tag = txtLogo.Text Then
            xmlTmp = _XMLDoc.CreateElement("Attribute")
            xmlTmp.SetAttribute("Name", "Logo")
            xmlTmp.SetAttribute("From", txtLogo.Tag)
            xmlTmp.SetAttribute("To", txtLogo.Text)
            xmlTmp.SetAttribute("AddIfNotFound", chkAddIfNotFound.Checked)
            node.AppendChild(xmlTmp)
        End If
        If Not txtShortName.Tag = txtShortName.Text Then
            xmlTmp = _XMLDoc.CreateElement("Attribute")
            xmlTmp.SetAttribute("Name", "Shortname")
            xmlTmp.SetAttribute("From", txtShortName.Tag)
            xmlTmp.SetAttribute("To", txtShortName.Text)
            xmlTmp.SetAttribute("AddIfNotFound", chkAddIfNotFound.Checked)
            node.AppendChild(xmlTmp)
        End If
        If Not txtListNr.Tag = txtListNr.Text Then
            xmlTmp = _XMLDoc.CreateElement("Attribute")
            xmlTmp.SetAttribute("Name", "ListNumber")
            xmlTmp.SetAttribute("From", txtListNr.Tag)
            xmlTmp.SetAttribute("To", txtListNr.Text)
            xmlTmp.SetAttribute("AddIfNotFound", chkAddIfNotFound.Checked)
            node.AppendChild(xmlTmp)
        End If
        If Not txtConnected.Tag = txtConnected.Text Then
            xmlTmp = _XMLDoc.CreateElement("Attribute")
            xmlTmp.SetAttribute("Name", "ConnectedChannel")
            xmlTmp.SetAttribute("From", txtConnected.Tag)
            xmlTmp.SetAttribute("To", txtConnected.Text)
            xmlTmp.SetAttribute("AddIfNotFound", chkAddIfNotFound.Checked)
            node.AppendChild(xmlTmp)
        End If
        If Not CDbl(txtCommission.Tag) = (CDbl(txtCommission.Text) / 100) Then
            xmlTmp = _XMLDoc.CreateElement("Attribute")
            xmlTmp.SetAttribute("Name", "AgencyCommission")
            xmlTmp.SetAttribute("From", txtCommission.Tag)
            xmlTmp.SetAttribute("To", CInt(txtCommission.Text) / 100)
            xmlTmp.SetAttribute("AddIfNotFound", chkAddIfNotFound.Checked)
            node.AppendChild(xmlTmp)
        End If
        If Not txtBuyingUni.Tag = txtBuyingUni.Text Then
            xmlTmp = _XMLDoc.CreateElement("Attribute")
            xmlTmp.SetAttribute("Name", "BuyingUniverse")
            xmlTmp.SetAttribute("From", txtBuyingUni.Tag)
            xmlTmp.SetAttribute("To", txtBuyingUni.Text)
            xmlTmp.SetAttribute("AddIfNotFound", chkAddIfNotFound.Checked)
            node.AppendChild(xmlTmp)
        End If

        'if one of the attributes where changed we save them
        If Not node.InnerXml = "" Then
            xmlList.AppendChild(node)
        End If 'end if the channel attributes


        'get the grid values aswell 
        'note: they are save with a seperate path aswell as the adress changes
        i = 0
        For i = 0 To grdBT.Rows.Count - 1
            Dim btNode As Xml.XmlElement
            btNode = Nothing

            'check for a new row or a Row that is to be deleted
            If grdBT.Rows(i).Tag = "NEW" Then
                btNode = _XMLDoc.CreateElement("Change")
                btNode.SetAttribute("Type", "AddRow")
                btNode.SetAttribute("Name", "Bookingtype")
                xmlTmp = _XMLDoc.CreateElement("Attribute")
                xmlTmp.SetAttribute("Name", "Name")
                xmlTmp.SetAttribute("Value", grdBT.Rows(i).Cells(0).Value)
                btNode.AppendChild(xmlTmp)
                xmlTmp = _XMLDoc.CreateElement("Attribute")
                xmlTmp.SetAttribute("Name", "Shortname")
                xmlTmp.SetAttribute("Value", grdBT.Rows(i).Cells(1).Value)
                btNode.AppendChild(xmlTmp)
                xmlTmp = _XMLDoc.CreateElement("Attribute")
                xmlTmp.SetAttribute("Name", "IsRBS")
                xmlTmp.SetAttribute("Value", grdBT.Rows(i).Cells(2).Value)
                btNode.AppendChild(xmlTmp)
                xmlTmp = _XMLDoc.CreateElement("Attribute")
                xmlTmp.SetAttribute("Name", "IsSpecific")
                xmlTmp.SetAttribute("Value", grdBT.Rows(i).Cells(3).Value)
                btNode.AppendChild(xmlTmp)
                xmlTmp = _XMLDoc.CreateElement("Attribute")
                xmlTmp.SetAttribute("Name", "PrintDayparts")
                xmlTmp.SetAttribute("Value", grdBT.Rows(i).Cells(4).Value)
                btNode.AppendChild(xmlTmp)
                xmlTmp = _XMLDoc.CreateElement("Attribute")
                xmlTmp.SetAttribute("Name", "PrintBookingCode")
                xmlTmp.SetAttribute("Value", grdBT.Rows(i).Cells(5).Value)
                btNode.AppendChild(xmlTmp)
            ElseIf grdBT.Rows(i).Tag = "DEL" Then
                btNode = _XMLDoc.CreateElement("Change")
                btNode.SetAttribute("Type", "DeleteRow")
                btNode.SetAttribute("Name", "Bookingtype")
                xmlTmp = _XMLDoc.CreateElement("Attribute")
                xmlTmp.SetAttribute("Name", "Name")
                xmlTmp.SetAttribute("Value", grdBT.Rows(i).Cells(0).Value)
                btNode.AppendChild(xmlTmp)
            Else
                'if not NEW or DEL step through all cells
                For j = 0 To grdBT.Columns.Count - 1
                    If Not grdBT.Rows(i).Cells(j).Value = grdBT.Rows(i).Cells(j).Tag Then
                        If btNode Is Nothing Then
                            btNode = _XMLDoc.CreateElement("Change")
                            btNode.SetAttribute("Type", "Edit")
                            btNode.SetAttribute("Name", "Name")
                            btNode.SetAttribute("Value", grdBT.Rows(i).Cells(0).Value)
                        End If
                        xmlTmp = _XMLDoc.CreateElement("Attribute")
                        Select Case j
                            Case Is = 0
                                xmlTmp.SetAttribute("Name", "Name")
                            Case Is = 1
                                xmlTmp.SetAttribute("Name", "Shortname")
                            Case Is = 2
                                xmlTmp.SetAttribute("Name", "IsRBS")
                            Case Is = 3
                                xmlTmp.SetAttribute("Name", "IsSpecific")
                            Case Is = 4
                                xmlTmp.SetAttribute("Name", "PrintDayparts")
                            Case Is = 5
                                xmlTmp.SetAttribute("Name", "PrintBookingCode")
                        End Select
                        xmlTmp.SetAttribute("From", grdBT.Rows(i).Cells(j).Tag)
                        xmlTmp.SetAttribute("To", grdBT.Rows(i).Cells(j).Value)
                        xmlTmp.SetAttribute("AddIfNotFound", chkAddIfNotFound.Checked)

                        btNode.AppendChild(xmlTmp)
                    End If
                Next
            End If

            'if we have a change we add it 
            If Not btNode Is Nothing Then
                xmlTmp = Nothing
                xmlTmp = _XMLDoc.CreateElement("Bookingtypes")
                xmlTmp.AppendChild(btNode)
                xmlList.AppendChild(xmlTmp)
            End If
        Next

        xmlTmp = xmlList.FirstChild
        z = 0
        While Not xmlTmp Is Nothing
            'if we have made a new channel we need to save the channel first
            If newChannel Then
                'this is method is not as good at the one in the else part
                'but the else code only works with existing channels
                node = _XMLDoc.CreateElement("Change")
                node.SetAttribute("Type", "Edit")
                node.SetAttribute("Name", "Name")
                node.SetAttribute("Value", txtName.Tag)

                xmlB = _XMLDoc.CreateElement("Attribute")
                xmlB.SetAttribute("Name", "Name")
                xmlB.SetAttribute("Value", txtName.Tag)

                node.AppendChild(xmlB)

                xmlB2 = _XMLDoc.CreateElement("Channels")
                xmlB2.AppendChild(node)
                path = _XMLDoc.CreateElement("Data")
                path.AppendChild(xmlB2)

                Dim FileNode As Xml.XmlElement = _XMLDoc.CreateElement("XML")
                FileNode.SetAttribute("FileName", cmbFile.SelectedItem.ToString.Substring(cmbFile.SelectedItem.ToString.IndexOf("Trinity Data 4.0") + 17))

                FileNode.AppendChild(path)
                _XML.AppendChild(FileNode)
            Else
                z += 1
                'we get the path to the channel
                xmlB2 = _XMLDocChan.SelectSingleNode("//Channel[@Name='" + txtName.Tag + "']")

                'if its not a pure channnel change we need to add 3 channel attributes
                If Not xmlTmp.ChildNodes(0).Name = "Attribute" Then
                    xmlB = _XMLDoc.CreateElement(xmlB2.Name)
                    i = 0
                    For Each xm As Xml.XmlAttribute In xmlB2.Attributes
                        If i = 1 Then Exit For
                        i += 1
                        xmlB.SetAttribute(xm.Name, xm.Value)
                    Next

                    xmlB.AppendChild(xmlTmp)
                    path = xmlB
                Else
                    xmlB2 = xmlB2.ParentNode

                    xmlB = _XMLDoc.CreateElement(xmlB2.Name)

                    'if its the channel node we use only the name (it is uniqe and cant be changed
                    Dim ch As String = ""
                    i = 0
                    For Each xm As Xml.XmlAttribute In xmlB2.Attributes
                        If i = 1 Then Exit For 'we use the first attribute (the name)
                        i += 1
                        xmlB.SetAttribute(xm.Name, xm.Value)
                    Next

                    xmlB.AppendChild(xmlTmp)
                    path = xmlB
                End If

                Try
                    parent = xmlB2.ParentNode
                Catch
                    parent = Nothing
                End Try

                j = 0
                'get the rest of the path
                While Not parent Is Nothing
                    PathTmp = _XMLDoc.CreateElement(parent.Name)
                    For Each xmlAT In parent.Attributes
                        If j = 1 Then Exit For 'only need a certain number of attributes to know our path
                        j += 1
                        PathTmp.SetAttribute(xmlAT.Name, xmlAT.Value)
                    Next
                    PathTmp.AppendChild(path)
                    path = PathTmp
                    Try
                        parent = parent.ParentNode
                    Catch
                        parent = Nothing
                    End Try
                    j = 0
                End While
                'finnished getting the path

                Dim FileNode As Xml.XmlElement = _XMLDoc.CreateElement("XML")
                FileNode.SetAttribute("FileName", cmbFile.SelectedItem.ToString.Substring(cmbFile.SelectedItem.ToString.IndexOf("Trinity Data 4.0") + 17))

                FileNode.AppendChild(path)
                _XML.AppendChild(FileNode)
            End If
            xmlTmp = xmlList.ChildNodes.Item(0)
        End While

        'save adress changes

        If Not txtDelivery.Tag = txtDelivery.Text Then
            node = _XMLDoc.CreateElement("Change")
            node.SetAttribute("Type", "Text")

            node.InnerText = txtDelivery.Text

            xmlB = _XMLDoc.CreateElement("Address")
            xmlB.AppendChild(node)

            node = _XMLDoc.CreateElement("data")
            node.AppendChild(xmlB)

            Dim FileNode As Xml.XmlElement = _XMLDoc.CreateElement("XML")
            Dim strfile As String = cmbFile.SelectedItem
            strfile = strfile.Substring(strfile.IndexOf("Trinity Data 4.0") + 17)
            FileNode.SetAttribute("FileName", strfile.Substring(0, strfile.LastIndexOf("\")) & "\Channel info\" & txtName.Text & ".xml")
            FileNode.AppendChild(node)
            _XML.AppendChild(FileNode)
        End If
        If Not txtVHS.Tag = txtVHS.Text Then
            node = _XMLDoc.CreateElement("Change")
            node.SetAttribute("Type", "Text")

            node.InnerText = txtDelivery.Text

            xmlB = _XMLDoc.CreateElement("VHSAddress")
            xmlB.AppendChild(node)

            node = _XMLDoc.CreateElement("data")
            node.AppendChild(xmlB)

            Dim FileNode As Xml.XmlElement = _XMLDoc.CreateElement("XML")
            Dim strfile As String = cmbFile.SelectedItem
            strfile = strfile.Substring(strfile.IndexOf("Trinity Data 4.0") + 17)
            FileNode.SetAttribute("FileName", strfile.Substring(0, strfile.LastIndexOf("\")) & "\Channel info\" & txtName.Text & ".xml")
            FileNode.AppendChild(node)
            _XML.AppendChild(FileNode)
        End If

        MsgBox("Save succesfull", MsgBoxStyle.Information)
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Label15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label15.Click

    End Sub
End Class