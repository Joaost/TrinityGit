Imports System.Xml
Imports System.IO

Public Class frmXML
    Dim _XMLDoc As New XmlDocument
    Dim _XML As XmlElement
    Dim _xmlFile As New XmlDocument
    Dim bolSetMarker As Boolean = False

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

    Private Sub frmXML_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Populate the cmbFile with all XML files
        Dim s As New trinityAdmin.FileSearch("G:\TV\Trinity Data 4.0\", "*.xml")
        s.Search()

        Dim fi As FileInfo
        Dim strTmp As String

        For i As Integer = 0 To s.Files.Count - 1
            fi = s.Files.Item(i)
            strTmp = fi.FullName
            cmbFile.Items.Add(strTmp)
        Next

    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvNodes.AfterSelect
        'when a selection is changed
        TextBox1.Text = tvNodes.SelectedNode.FullPath
        grdAttrib.Rows.Clear()
        Dim strTag As String


        'check if its marked for deletion or not
        If tvNodes.SelectedNode.Tag = "DEL" Then
            lblDELETE.Visible = True
        Else
            lblDELETE.Visible = False
        End If

        If e.Node.Text = "#text" Then
            cmdAdd.Enabled = False
            txtHuge.Visible = True
            grdAttrib.Visible = False
            chkIfFound.Visible = False
            txtHuge.Text = e.Node.Tag
            strTag = e.Node.Parent.Tag
        Else
            cmdAdd.Enabled = True
            txtHuge.Visible = False
            grdAttrib.Visible = True
            chkIfFound.Visible = True
            strTag = e.Node.Tag
        End If

        grdAttrib.RowHeadersVisible = False
        Dim strTmp As String
        Dim i As Integer

        While Not strTag Is Nothing
            If strTag.IndexOf(";") > -1 Then
                strTmp = strTag.Substring(0, strTag.IndexOf(";"))
                strTag = strTag.Substring(strTag.IndexOf(";") + 1)
            Else
                strTmp = strTag
                strTag = Nothing
            End If
            If Not strTmp Is Nothing AndAlso Not strTmp = "" Then
                i = grdAttrib.Rows.Add()
                grdAttrib.Rows(i).Cells(0).Value = strTmp.Substring(0, strTmp.IndexOf("~"))
                grdAttrib.Rows(i).Cells(0).Tag = strTmp.Substring(0, strTmp.IndexOf("~"))
                grdAttrib.Rows(i).Cells(1).Value = strTmp.Substring(strTmp.IndexOf("~") + 1)
                grdAttrib.Rows(i).Cells(1).Tag = strTmp.Substring(strTmp.IndexOf("~") + 1)
            End If
        End While
    End Sub

    Private Sub cmbFile_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFile.SelectedIndexChanged
        'load the document
        _xmlFile.Load(cmbFile.Items.Item(cmbFile.SelectedIndex).ToString.Trim)

        ' Add the root node's children to the TreeView.
        tvNodes.Nodes.Clear()
        Dim new_node As TreeNode = tvNodes.Nodes.Add(_xmlFile.DocumentElement.Name)

        If _xmlFile.DocumentElement.Attributes.Count > 0 Then
            Dim strTmp As String = ""
            For Each attrib As XmlAttribute In _XMLDoc.DocumentElement.Attributes
                strTmp = strTmp + ";" + attrib.Name + "~" + attrib.Value
            Next
            new_node.Tag = strTmp
        End If

        AddTreeViewChildNodes(new_node.Nodes, _xmlFile.DocumentElement)
    End Sub

    ' Add the children of this XML node 
    ' to this child nodes collection.
    Private Sub AddTreeViewChildNodes(ByVal parent_nodes As TreeNodeCollection, ByVal xml_node As XmlNode)
        For Each child_node As XmlNode In xml_node.ChildNodes
            ' Make the new TreeView node.
            Dim new_node As TreeNode = parent_nodes.Add(child_node.Name)

            If child_node.Name = "#text" Then
                new_node.Tag = child_node.Value
                new_node.EnsureVisible()
                Exit Sub
            End If

            If child_node.Attributes.Count > 0 Then
                Dim strTmp As String = ""
                For Each attrib As XmlAttribute In child_node.Attributes
                    strTmp = strTmp + ";" + attrib.Name + "~" + attrib.Value
                Next
                new_node.Tag = strTmp
            End If

            ' Recursively make this node's descendants.
            AddTreeViewChildNodes(new_node.Nodes, child_node)

            ' If this is a leaf node, make sure it's visible.
            If new_node.Nodes.Count = 0 Then new_node.EnsureVisible()
        Next child_node
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Dim frm1 As New frmInput
        Dim frm2 As New frmInput

        frm1.Text = "Attribute Name"
        If frm1.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        frm2.Text = "Value for " + frm1.TextBox1.Text
        If frm2.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Dim i As Integer = grdAttrib.Rows.Add()
        grdAttrib.Rows(i).Cells(0).Value = frm1.TextBox1.Text
        grdAttrib.Rows(i).Cells(1).Value = frm2.TextBox1.Text

    End Sub

    Private Sub cmdAddRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddRow.Click
        Dim frm1 As New frmInput
        frm1.Text = "Node name"
        If frm1.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub

        Dim node As Xml.XmlElement = _XMLDoc.CreateElement("Change")
        node.SetAttribute("Type", "AddRow")
        node.SetAttribute("Name", frm1.TextBox1.Text)
        Dim j As Integer

        'get some compairing attributes
        Dim strTag As String = tvNodes.SelectedNode.Tag
        Dim strTmp As String
        'if this node has no attributes we simply go back up in the hierarchy
        If Not strTag = "" Then
            While Not strTag Is Nothing
                If strTag.IndexOf(";") > -1 Then
                    strTmp = strTag.Substring(0, strTag.IndexOf(";"))
                    strTag = strTag.Substring(strTag.IndexOf(";") + 1)
                Else
                    strTmp = strTag
                    strTag = Nothing
                End If
                If Not strTmp Is Nothing AndAlso Not strTmp = "" Then
                    If j = 3 Then Exit While
                    j += 1
                    Dim xmlTmp As Xml.XmlElement = _XMLDoc.CreateElement("Attribute")
                    xmlTmp.SetAttribute(strTmp.Substring(0, strTmp.IndexOf("~")), strTmp.Substring(strTmp.IndexOf("~") + 1))
                    node.AppendChild(xmlTmp)
                End If
            End While
        End If


        'get the parent nodes from the tree
        Dim parent As TreeNode = tvNodes.SelectedNode.Parent
        Dim pathTmp As Xml.XmlElement

        j = 0
        'get the rest of the path
        While Not parent Is Nothing
            pathTmp = _XMLDoc.CreateElement(parent.Text)

            strTag = parent.Tag
            While Not strTag Is Nothing AndAlso Not strTag = ""
                If strTag.IndexOf(";") > -1 Then
                    strTmp = strTag.Substring(0, strTag.IndexOf(";"))
                    strTag = strTag.Substring(strTag.IndexOf(";") + 1)
                Else
                    strTmp = strTag
                    strTag = Nothing
                End If
                If Not strTmp Is Nothing AndAlso Not strTmp = "" Then
                    If j = 3 Then Exit While
                    j += 1
                    pathTmp.SetAttribute(strTmp.Substring(0, strTmp.IndexOf("~")), strTmp.Substring(strTmp.IndexOf("~") + 1))
                End If
            End While

            pathTmp.AppendChild(node)
            node = pathTmp
            Try
                parent = parent.Parent
            Catch
                parent = Nothing
            End Try
            j = 0
        End While

        _XML.AppendChild(node)


        'add the node too the treeveiw aswell
        tvNodes.SelectedNode.Nodes.Add(frm1.TextBox1.Text)
        tvNodes.SelectedNode.Nodes.Item(frm1.TextBox1.Text).Tag = "NEW"

    End Sub

    Private Sub cmdDeleteRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteRow.Click
        Dim node As Xml.XmlElement = _XMLDoc.CreateElement("Change")
        node.SetAttribute("Type", "DeleteRow")
        node.SetAttribute("Name", tvNodes.SelectedNode.Text)
        Dim j As Integer

        'get some compairing attributes
        Dim strTag As String = tvNodes.SelectedNode.Tag
        Dim strTmp As String
        'if this node has no attributes we simply go back up in the hierarchy
        If Not strTag = "" Then
            While Not strTag Is Nothing
                If strTag.IndexOf(";") > -1 Then
                    strTmp = strTag.Substring(0, strTag.IndexOf(";"))
                    strTag = strTag.Substring(strTag.IndexOf(";") + 1)
                Else
                    strTmp = strTag
                    strTag = Nothing
                End If
                If Not strTmp Is Nothing AndAlso Not strTmp = "" Then
                    If j = 3 Then Exit While
                    j += 1
                    Dim xmlTmp As Xml.XmlElement = _XMLDoc.CreateElement("Attribute")
                    xmlTmp.SetAttribute(strTmp.Substring(0, strTmp.IndexOf("~")), strTmp.Substring(strTmp.IndexOf("~") + 1))
                    node.AppendChild(xmlTmp)
                End If
            End While
        End If

        'get the parent nodes from the tree
        Dim parent As TreeNode = tvNodes.SelectedNode.Parent
        Dim pathTmp As Xml.XmlElement

        j = 0
        'get the rest of the path
        While Not parent Is Nothing
            pathTmp = _XMLDoc.CreateElement(parent.Text)

            strTag = parent.Tag
            While Not strTag Is Nothing AndAlso Not strTag = ""
                If strTag.IndexOf(";") > -1 Then
                    strTmp = strTag.Substring(0, strTag.IndexOf(";"))
                    strTag = strTag.Substring(strTag.IndexOf(";") + 1)
                Else
                    strTmp = strTag
                    strTag = Nothing
                End If
                If Not strTmp Is Nothing AndAlso Not strTmp = "" Then
                    If j = 3 Then Exit While
                    j += 1
                    pathTmp.SetAttribute(strTmp.Substring(0, strTmp.IndexOf("~")), strTmp.Substring(strTmp.IndexOf("~") + 1))
                End If
            End While

            pathTmp.AppendChild(node)
            node = pathTmp
            Try
                parent = parent.Parent
            Catch
                parent = Nothing
            End Try
            j = 0
        End While

        _XML.AppendChild(node)


        'edits the treeview
        tvNodes.SelectedNode.Tag = "DEL"

    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        If txtHuge.Visible = True Then
            Dim node As Xml.XmlElement = _XMLDoc.CreateElement("Change")
            node.SetAttribute("Type", "Text")
            Try
                node.SetAttribute("Name", grdAttrib.Rows(0).Cells(0).Tag)
                node.SetAttribute("Value", grdAttrib.Rows(0).Cells(1).Tag)
            Catch
                'no adds here
            End Try
            node.InnerText = txtHuge.Text

            Dim parent As TreeNode = tvNodes.SelectedNode.Parent
            Dim pathTmp As Xml.XmlElement
            Dim j As Integer = 0
            'get the rest of the path
            While Not parent Is Nothing
                pathTmp = _XMLDoc.CreateElement(parent.Text)

                pathTmp.AppendChild(node)
                node = pathTmp
                Try
                    parent = parent.Parent
                Catch
                    parent = Nothing
                End Try
                j = 0
            End While

            Dim FileNode As Xml.XmlElement = _XMLDoc.CreateElement("XML")
            FileNode.SetAttribute("FileName", cmbFile.SelectedItem.ToString.Substring(cmbFile.SelectedItem.ToString.IndexOf("Trinity Data 4.0") + 17))

            FileNode.AppendChild(node)

            'add the nodes we have made to the form xml 
            _XML.AppendChild(FileNode)

        Else 'end for Text changes start of attribute changes
            Dim i As Integer
            Dim j As Integer
            Dim AttribNode As Xml.XmlElement
            Dim nodeA As Xml.XmlElement
            Dim bolPath As Boolean

            For i = 0 To grdAttrib.Rows.Count - 1
                AttribNode = Nothing
                nodeA = Nothing
                For j = 0 To grdAttrib.Columns.Count - 1
                    bolPath = False 'reset
                    If grdAttrib.Rows(i).Tag = "DEL" Then
                        AttribNode = _XMLDoc.CreateElement("Change")
                        AttribNode.SetAttribute("Type", "Delete")
                        AttribNode.SetAttribute("Name", grdAttrib.Rows(i).Cells(0).Tag)
                        AttribNode.SetAttribute("Value", grdAttrib.Rows(i).Cells(1).Tag)

                        bolPath = True
                    ElseIf grdAttrib.Rows(i).Tag = "NEW" Then
                        AttribNode = _XMLDoc.CreateElement("Change")
                        nodeA.SetAttribute("Type", "Add")
                        nodeA.SetAttribute("Name", grdAttrib.Rows(i).Cells(0).Value)
                        nodeA.SetAttribute("Value", grdAttrib.Rows(i).Cells(1).Value)

                        bolPath = True
                    Else
                        If Not grdAttrib.Rows(i).Cells(j).Value = grdAttrib.Rows(i).Cells(j).Tag Then
                            'save the changes
                            AttribNode = _XMLDoc.CreateElement("Change")

                            Dim x As Integer

                            If j = 0 Then 'a attribute name
                                'when a attribute name is changed we delete the old and make a new
                                AttribNode.SetAttribute("Type", "Delete")

                                'get key attributes if available
                                'key attributes are colored red
                                For x = 0 To grdAttrib.Rows.Count - 1
                                    If grdAttrib.Rows(x).Cells(0).Style.BackColor = Color.Red Then
                                        AttribNode.SetAttribute("Name", grdAttrib.Rows(x).Cells(0).Tag)
                                        AttribNode.SetAttribute("Value", grdAttrib.Rows(x).Cells(1).Tag)
                                        Exit For
                                    End If
                                Next

                                Dim node2 As Xml.XmlElement = _XMLDoc.CreateElement("Attribute")

                                node2.SetAttribute("Name", grdAttrib.Rows(i).Cells(0).Tag)
                                node2.SetAttribute("Value", grdAttrib.Rows(i).Cells(1).Value)

                                AttribNode.AppendChild(node2)

                                'now we add a new attribute
                                nodeA = _XMLDoc.CreateElement("Change")

                                nodeA.SetAttribute("Type", "Add")
                                'get key attributes if available
                                'key attributes are colored red
                                For x = 0 To grdAttrib.Rows.Count - 1
                                    If grdAttrib.Rows(x).Cells(0).Style.BackColor = Color.Red Then
                                        nodeA.SetAttribute("Name", grdAttrib.Rows(x).Cells(0).Value)
                                        nodeA.SetAttribute("Value", grdAttrib.Rows(x).Cells(1).Value)
                                        Exit For
                                    End If
                                Next

                            Else ' a attribute value
                                AttribNode.SetAttribute("Type", "Edit")

                                'get key attributes if available
                                'key attributes are colored red
                                For x = 0 To grdAttrib.Rows.Count - 1
                                    If grdAttrib.Rows(x).Cells(0).Style.BackColor = Color.Red Then
                                        AttribNode.SetAttribute("Name", grdAttrib.Rows(x).Cells(0).Tag)
                                        AttribNode.SetAttribute("Value", grdAttrib.Rows(x).Cells(1).Tag)
                                        Exit For
                                    End If
                                Next

                                Dim node2 As Xml.XmlElement = _XMLDoc.CreateElement("Attribute")
                                node2.SetAttribute("Name", grdAttrib.Rows(i).Cells(0).Value)
                                node2.SetAttribute("From", grdAttrib.Rows(i).Cells(1).Tag)
                                node2.SetAttribute("To", grdAttrib.Rows(i).Cells(1).Value)
                                If chkIfFound.Checked Then
                                    node2.SetAttribute("AddIfNotFound", "True")
                                Else
                                    node2.SetAttribute("AddIfNotFound", "False")
                                End If
                                AttribNode.AppendChild(node2)
                            End If

                            bolPath = True
                        End If
                    End If
                    If bolPath Then 'same code for getting the paths
                        'saves the path

                        'used this code for getting attributes but it was replaced by key attributes
                        'see code above
                        ''get some compairing attributes if available
                        'Dim strTag As String = tvNodes.SelectedNode.Tag
                        'Dim strTmp As String
                        ''if this node has no attributes we simply go back up in the hierarchy
                        'If Not strTag = "" Then
                        '    While Not strTag Is Nothing
                        '        If strTag.IndexOf(";") > -1 Then
                        '            strTmp = strTag.Substring(0, strTag.IndexOf(";"))
                        '            strTag = strTag.Substring(strTag.IndexOf(";") + 1)
                        '        Else
                        '            strTmp = strTag
                        '            strTag = Nothing
                        '        End If
                        '        If Not strTmp Is Nothing AndAlso Not strTmp = "" Then
                        '            If j = 3 Then Exit While
                        '            j += 1
                        '            Dim xmlTmp As Xml.XmlElement = _XMLDoc.CreateElement("Attribute")
                        '            xmlTmp.SetAttribute(strTmp.Substring(0, strTmp.IndexOf("~")), strTmp.Substring(strTmp.IndexOf("~") + 1))
                        '            AttribNode.AppendChild(xmlTmp)
                        '        End If
                        '    End While
                        'End If

                        'get the parent nodes from the tree
                        Dim parent As TreeNode = tvNodes.SelectedNode.Parent
                        Dim pathTmp As Xml.XmlElement
                        Dim strTag As String
                        Dim strTmp As String

                        j = 0
                        'get the rest of the path
                        While Not parent Is Nothing
                            pathTmp = _XMLDoc.CreateElement(parent.Text)

                            strTag = parent.Tag
                            While Not strTag Is Nothing AndAlso Not strTag = ""
                                If strTag.IndexOf(";") > -1 Then
                                    strTmp = strTag.Substring(0, strTag.IndexOf(";"))
                                    strTag = strTag.Substring(strTag.IndexOf(";") + 1)
                                Else
                                    strTmp = strTag
                                    strTag = Nothing
                                End If
                                If Not strTmp Is Nothing AndAlso Not strTmp = "" Then
                                    If j = 3 Then Exit While
                                    j += 1
                                    pathTmp.SetAttribute(strTmp.Substring(0, strTmp.IndexOf("~")), strTmp.Substring(strTmp.IndexOf("~") + 1))
                                End If
                            End While

                            'if we have 2 attributes we add both (only applyes when we change a attribute name)
                            If Not nodeA Is Nothing Then
                                pathTmp.AppendChild(nodeA)
                                nodeA = Nothing
                            End If
                            pathTmp.AppendChild(AttribNode)
                            AttribNode = pathTmp
                            Try
                                parent = parent.Parent
                            Catch
                                parent = Nothing
                            End Try
                            j = 0
                        End While

                        Dim FileNode As Xml.XmlElement = _XMLDoc.CreateElement("XML")
                        FileNode.SetAttribute("FileName", cmbFile.SelectedItem.ToString.Substring(cmbFile.SelectedItem.ToString.IndexOf("Trinity Data 4.0") + 17))

                        FileNode.AppendChild(AttribNode)
                        _XML.AppendChild(FileNode)

                        'if we changed the attribute name we have already saved the value
                        If j = 0 Then Exit For
                    End If
                Next
            Next
        End If

    End Sub

    Private Sub cmdDelAttrib_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelAttrib.Click
        grdAttrib.Rows(grdAttrib.SelectedCells(0).RowIndex).Tag = "DEL"
        For i As Integer = 0 To grdAttrib.Columns.Count - 1
            grdAttrib.Rows(grdAttrib.SelectedCells(0).RowIndex).Cells(i).Style.BackColor = Color.Red
        Next
    End Sub

    Private Sub cmdSetMarker_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetMarker.Click
        bolSetMarker = True
    End Sub

    Private Sub txtHuge_TextChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdAttrib.CellClick
        If bolSetMarker Then
            If grdAttrib.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.Red Then
                grdAttrib.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.White
                grdAttrib.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.White
            Else
                grdAttrib.Rows(e.RowIndex).Cells(0).Style.BackColor = Color.Red
                grdAttrib.Rows(e.RowIndex).Cells(1).Style.BackColor = Color.Red
            End If
        End If

        bolSetMarker = False
    End Sub
End Class