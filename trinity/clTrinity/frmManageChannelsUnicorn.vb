Imports System
Imports System.IO
Imports System.Xml.XmlDocument
Imports System.Xml
Imports System.Windows.Forms
Public Class frmManageChannelsUnicorn

    Public listOfXmls As New List(Of XmlDocument)
    Public listOfFiles As New List(Of FileInfo)
    Private Sub frmManageChannelsUnicorn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Read all xml files which contains UNICORN
        importAllXmls()


    End Sub

    Public Sub importAllXmls()

        Dim datapath As String = TrinitySettings.DataPath

        ' Make a reference to a directory.
        Dim di As New DirectoryInfo(datapath)
        ' Get a reference to each file in that directory.
        Dim fiArr As FileInfo() = di.GetFiles("*.xml")
        ' Display the names of the files.
        Dim fri As FileInfo
        For Each fri In fiArr
            If fri.Name.Contains("Unicorn") Then
                listOfFiles.Add(fri)
            End If
        Next fri

        ' Read files as file info and add to list of xmls
        For Each xmlFile As FileInfo In listOfFiles
            ' Create xml document
            Dim doc As New XmlDocument
            ' load xml
            doc.Load(xmlFile.Directory.ToString() + "\" + xmlFile.Name)
            listOfXmls.Add(doc)
        Next
        updateTreeView()
    End Sub
    Private Sub updateTreeView()
        tvwChannelHouse.Nodes.Clear()
        'tvwChannelHouse.Controls.Clear()
        ' Populate channelhouse to treeview
        Dim newRow As Integer = 0
        For Each xmlChannelHouse As XmlDocument In listOfXmls
            ' Retreieve and populate name for each channelhouse
            Dim chName As String = ""
            Dim root As XmlElement = xmlChannelHouse.DocumentElement
            ' Xml node from xml doc
            Dim xmlnode As XmlNode
            xmlnode = xmlChannelHouse.ChildNodes(1)
            ' Adds the channelhouse name (supposed to)
            tvwChannelHouse.Nodes.Add(New TreeNode(xmlChannelHouse.DocumentElement.GetAttribute("ChannelHouse")))
            ' Create a new treenode for each channelhouse
            Dim tNode As New TreeNode
            ' Add treenode to a new row
            tNode = tvwChannelHouse.Nodes(newRow)
            ' Populate children to node function
            AddNode(xmlnode, tNode)
            ' Increase newrow for each channelhouse
            newRow = newRow + 1

        Next
    End Sub

    Private Sub AddNode(ByVal inXmlNode As XmlNode, ByVal inTreeNode As TreeNode)
        Dim xNode As XmlNode
        Dim tNode As TreeNode
        Dim nodeList As XmlNodeList
        Dim i As Integer
        ' Check if the node has children
        If inXmlNode.HasChildNodes Then
            nodeList = inXmlNode.ChildNodes
            ' Iterates through the childrens. If not then go to else statement
            For i = 0 To nodeList.Count - 1
                xNode = inXmlNode.ChildNodes(i)
                inTreeNode.Nodes.Add(New TreeNode(xNode.Name))
                tNode = inTreeNode.Nodes(i)
                AddNode(xNode, tNode)
            Next
        Else
            inTreeNode.Text = inXmlNode.Attributes("Name").Value
        End If
    End Sub


    Private Sub tvwChannelHouse_MouseClick(sender As Object, e As MouseEventArgs) Handles tvwChannelHouse.MouseClick
        Dim channelHouseClicked As TreeNode = tvwChannelHouse.SelectedNode
        If channelHouseClicked Is Nothing Then
            ' Disable controls if selected node has no value.
            btnAddChannel.Enabled = False
            btnRemoveChannel.Enabled = False
        Else
            ' Enable controls if node has a value.
            btnAddChannel.Enabled = True
            btnRemoveChannel.Enabled = True

        End If
    End Sub

    Private Sub tvwChannelHouse_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tvwChannelHouse.NodeMouseClick
        Dim node As TreeNode = e.Node
        If node.LastNode Is Nothing Then
            txtCHName.Enabled = True
            txtCHName.Text = e.Node.Text
            btnSave.Enabled = True
        Else
            txtCHName.Enabled = False
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        ' Bool when finding selected parentnode
        Dim foundParentNode As Boolean = False
        ' Xml res 
        Dim parentNodeFound As XmlDocument
        Dim selectedNodeName As String
        Dim updatedParent As String
        Dim resUpdatedValue As String = txtCHName.Text

        ' Get the selected node to eventually match.
        If tvwChannelHouse.SelectedNode IsNot Nothing Then
            selectedNodeName = tvwChannelHouse.SelectedNode.Parent.Parent.Text
            updatedParent = tvwChannelHouse.SelectedNode.Text
            For Each resXml As XmlDocument In listOfXmls
                If resXml.DocumentElement.GetAttribute("ChannelHouse") = selectedNodeName Then
                    foundParentNode = True
                    parentNodeFound = resXml
                    Exit For
                End If
            Next
            ' Call the update funciton for specific node.
            Dim resUpdatedXmlDOC As XmlDocument = updaateXmlNode(parentNodeFound, updatedParent, resUpdatedValue)
            Dim datapathURL As String = resUpdatedXmlDOC.BaseURI
            datapathURL = datapathURL.Replace("file:", "")

            resUpdatedXmlDOC.Save(datapathURL)
            lblUpdateInfo.Text = "Successfully updated channel house " + selectedNodeName + " and " + updatedParent + " to " + resUpdatedValue


            ' Upadating treeview
            updateTreeView()

        End If
    End Sub
    Private Function updaateXmlNode(ByVal selectedNodeXML As XmlDocument, ByVal nodeName As String, ByVal updatedValue As String) As XmlDocument

        Dim nodeList As XmlNodeList
        Dim i As Integer
        ' Check if the node has children
        If selectedNodeXML.HasChildNodes Then
            nodeList = selectedNodeXML.ChildNodes(1).ChildNodes(0).ChildNodes
            ' Iterates through the childrens. If not then go to else statement
            For i = 0 To nodeList.Count - 1
                For a As Integer = 0 To nodeList(i).Attributes.Count - 1
                    If nodeList(i).Attributes(a).Value = nodeName Then
                        nodeList(i).Attributes(a).Value = updatedValue
                        Return selectedNodeXML
                    End If
                Next
            Next
        Else
            'inTreeNode.Text = inXmlNode.Attributes("Name").Value
        End If
    End Function

    Private Sub btnAddChannel_Click(sender As Object, e As EventArgs) Handles btnAddChannel.Click

        ' Bool when finding selected parentnode
        Dim foundParentNode As Boolean = False
        ' Xml res 
        Dim parentNodeFound As XmlDocument
        If tvwChannelHouse.SelectedNode.Parent Is Nothing Then
            Windows.Forms.MessageBox.Show("To add a channel to a channel house you have to select the specific 'Channel' below the channel with a mouse click.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Question)
            Exit Sub
        Else
            For Each resXml As XmlDocument In listOfXmls
                If resXml.DocumentElement.GetAttribute("ChannelHouse") = tvwChannelHouse.SelectedNode.Parent.Text Then
                    foundParentNode = True
                    parentNodeFound = resXml
                    Exit For
                End If
            Next
        End If

        ' Get the selected node to eventually match.
        If tvwChannelHouse.SelectedNode IsNot Nothing Then

            'Create new windows form to let the user enter the new channel name
            Dim str As String
            Dim frm As New frmAddChannelUnicorn()
            frm.ShowDialog()

            If frm.DialogResult = Windows.Forms.DialogResult.OK Then
                str = frm.txtInputChannelName.Text
            Else
                Exit Sub
            End If

            ' Call the adding funciton for specific node.
            Dim resUpdatedXmlDOC As XmlDocument = addChannelToExistingChannelHouse(parentNodeFound, str)
            If resUpdatedXmlDOC IsNot Nothing Then

                Dim datapathURL As String = resUpdatedXmlDOC.BaseURI

                datapathURL = datapathURL.Replace("file:", "")
                ' Saving update to file.
                resUpdatedXmlDOC.Save(datapathURL)
                If saveXML(resUpdatedXmlDOC, datapathURL) Then
                    lblUpdateInfo.Text = "Successfully added new channel:" + parentNodeFound.Value
                End If
                ' Upadating treeview
                updateTreeView()
                End If
            End If
    End Sub
    Private Function saveXML(ByVal xmlDoc As XmlDocument, ByVal dataPath As String)
        Try
            xmlDoc.Save(dataPath)
            Return True
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("Something went wrong when adding channel to a channel house." + ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Warning)
        End Try
        Return False
    End Function
    Public Function addChannelToExistingChannelHouse(ByVal selectedNodeXML As XmlDocument, ByVal tempChannelName As String)
        ' Create a new xml document to append to the existing xml document
        Dim x As New Xml.XmlDocument()
        ' Add a xml node to the new xml document
        Dim parNode As Xml.XmlNode
        Dim xmlAttribute As XmlElement

        Try

            x = selectedNodeXML
            ' Where the place the new channel to correct childnode
            parNode = x.SelectSingleNode("/Data/Channels")
            ' Create an element for the channel
            xmlAttribute = x.CreateElement("Channel")
            ' Set the attribute nam eand value
            xmlAttribute.SetAttribute("Name", tempChannelName)
            ' Append to xml childnode
            parNode.AppendChild(xmlAttribute)

            Return x
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("Something went wrong when adding channel to a channel house." + ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Warning)
        End Try

    End Function

    Private Sub btnRemoveChannel_Click(sender As Object, e As EventArgs) Handles btnRemoveChannel.Click
        Dim selectedNode As TreeNode = tvwChannelHouse.SelectedNode
        Dim xmlDoc As XmlDocument
        ' Statement to check if channel node is selected
        If selectedNode IsNot Nothing Then
            If tvwChannelHouse.SelectedNode.Nodes.Count = 0 Then
                Dim parentNode As TreeNode = selectedNode.Parent.Parent
                If Windows.Forms.DialogResult.Yes = Windows.Forms.MessageBox.Show("Are you sure you want to remove this channel?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Warning) Then

                    For Each xmls As XmlDocument In listOfXmls
                        If xmls.DocumentElement.GetAttribute("ChannelHouse") = selectedNode.Parent.Parent.Text Then
                            Dim childeNodes As XmlNodeList = xmls.ChildNodes(1).ChildNodes(0).ChildNodes
                            Dim removedChannel As XmlNode
                            ' Append the correct xml document
                            xmlDoc = xmls
                            ' Append nodelist to iterate through
                            Dim tempChannels As XmlNodeList = xmlDoc.ChildNodes(1).ChildNodes(0).ChildNodes
                            For Each tempchannel As XmlNode In tempChannels
                                If tempchannel.Attributes(0).Value = selectedNode.Text Then
                                    removedChannel = tempchannel
                                End If
                            Next
                            xmlDoc.ChildNodes(1).ChildNodes(0).RemoveChild(removedChannel)
                            Dim datapathURL As String = xmls.BaseURI
                            datapathURL = datapathURL.Replace("file:", "")
                            If saveXML(xmls, datapathURL) Then
                                lblUpdateInfo.Text = "Successfully removed channel:" + selectedNode.Text
                                updateTreeView()
                            End If
                            Exit For
                        End If
                    Next
                End If
            Else
                Windows.Forms.MessageBox.Show("If you want to remove channel you have to select with the mouse click on a specific channel.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            End If
        Else
            Windows.Forms.MessageBox.Show("If you want to remove channel you have to select with the mouse click on a specific channel.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        End If
    End Sub
End Class