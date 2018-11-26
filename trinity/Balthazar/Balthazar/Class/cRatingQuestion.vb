Imports System
Imports System.Xml

Public Class cRatingQuestion
    Public Text As String
    Public MeaningOf5 As String
    Public MeaningOf1 As String
    Public DatabaseID As Integer = -1

    Friend Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim TmpNode As Xml.XmlElement = XMLDoc.CreateElement("Question")
        TmpNode.SetAttribute("Text", Text)
        TmpNode.SetAttribute("MeaningOf5", MeaningOf5)
        TmpNode.SetAttribute("MeaningOf1", MeaningOf1)
        TmpNode.SetAttribute("DatabaseID", DatabaseID)
        Return TmpNode
    End Function

    Friend Function CreatePreviewHTML() As String
        Dim HTML As String = "<tr><td>" & Text & "</td><td>" & MeaningOf5 & "</td>"
        For i As Integer = 5 To 1 Step -1
            HTML &= "<td><input type='radio' style='box' value='rating" & i & "'>&nbsp;" & i & "</td>"
        Next
        HTML &= "</td><td>" & MeaningOf1 & "</td>"
        Return HTML
    End Function

    Friend Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        Text = Node.GetAttribute("Text")
        MeaningOf5 = Node.GetAttribute("MeaningOf5")
        MeaningOf1 = Node.GetAttribute("MeaningOf1")
        DatabaseID = Node.GetAttribute("DatabaseID")
    End Sub

    Public Sub New()

    End Sub

    Public Sub New(ByVal Node As Xml.XmlElement)
        CreateFromXML(Node)
    End Sub
End Class
