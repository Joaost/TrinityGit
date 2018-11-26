Imports System
Imports System.Xml

Public Class cCommentQuestion
    Public Text As String
    Public MaxComments As Integer = 5
    Public DatabaseID As Integer = -1

    Friend Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim TmpNode As Xml.XmlElement = XMLDoc.CreateElement("Question")
        TmpNode.SetAttribute("Text", Text)
        TmpNode.SetAttribute("MaxComments", MaxComments)
        TmpNode.SetAttribute("DatabaseID", DatabaseID)
        Return TmpNode
    End Function

    Friend Function CreatePreviewHTML() As String
        Dim HTML As String = "<table width=100% border=0><tr><td class='headline'>" & Text & "</td></tr>"
        For i As Integer = 1 To MaxComments
            HTML &= "<tr><td>" & i & ".&nbsp;<input type='text' class='box' style='width: 97%;'></td></tr>"
        Next
        Return HTML
    End Function

    Friend Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        Text = Node.GetAttribute("Text")
        MaxComments = Node.GetAttribute("MaxComments")
        DatabaseID = Node.GetAttribute("DatabaseID")
    End Sub

    Public Sub New()

    End Sub

    Public Sub New(ByVal Node As Xml.XmlElement)
        CreateFromXML(Node)
    End Sub

End Class
