Imports Microsoft
Imports System
Imports System.Xml

Public Class cTemplate
    Public Name As String
    Public Description As String
    Public Text As String

    Friend Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim XmlNode As Xml.XmlElement = XMLDoc.CreateElement("Template")
        XmlNode.SetAttribute("Name", Name)
        XmlNode.SetAttribute("Description", Description)
        XmlNode.InnerText = Text
        Return XmlNode
    End Function

    Friend Sub CreateFromXML(ByVal Node As XmlElement)
        Name = Node.GetAttribute("Name")
        Description = Node.GetAttribute("Description")
        Text = Node.InnerText
    End Sub

    Public Overrides Function ToString() As String
        Return "Template: " & Name
    End Function

    Public Sub New()

    End Sub

    Public Sub New(ByVal CreateFromNode As Xml.XmlElement)
        CreateFromXML(CreateFromNode)
    End Sub
End Class
