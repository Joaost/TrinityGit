Imports System
Imports System.Xml

Public Class cCampaign
    Public Background As String
    Public What As String
    Public Purpose As String
    Public How As String
    Public WhenIsIt As String

    Friend Function CreateXML(ByVal XMLParentNode As Xml.XmlElement, ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        XMLParentNode.SetAttribute("Background", Background)
        XMLParentNode.SetAttribute("What", What)
        XMLParentNode.SetAttribute("Purpose", Purpose)
        XMLParentNode.SetAttribute("How", How)
        XMLParentNode.SetAttribute("When", WhenIsIt)
        Return XMLParentNode
    End Function

    Friend Sub CreateFromXML(ByVal XMLNode As Xml.XmlElement)
        Background = XMLNode.GetAttribute("Background")
        What = XMLNode.GetAttribute("What")
        Purpose = XMLNode.GetAttribute("Purpose")
        How = XMLNode.GetAttribute("How")
        WhenIsIt = XMLNode.GetAttribute("When")
    End Sub
End Class
