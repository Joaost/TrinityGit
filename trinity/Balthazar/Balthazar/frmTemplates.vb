Public Class frmTemplates

    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub frmTemplates_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim XMLDoc As New Xml.XmlDocument

        lvwTemplates.Items.Clear()
        For Each File As String In My.Computer.FileSystem.GetFiles(BalthazarSettings.DataFolder & "Templates\")
            XMLDoc.Load(File)
            Dim XMLTemplate As Xml.XmlElement = XMLDoc.GetElementsByTagName("Template").Item(0)
            With lvwTemplates.Items.Add(XMLTemplate.GetAttribute("Name"))
                .Tag = XMLDoc.OuterXml
            End With
        Next
    End Sub
End Class