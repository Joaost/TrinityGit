Public Class cQuestionAndAnswer
    Public ID As String = Guid.NewGuid.ToString
    Public Question As String
    Public Answer As String

    Friend Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim XMLNode As Xml.XmlElement = XMLDoc.CreateElement("QA")

        XMLNode.SetAttribute("Question", Question)
        XMLNode.SetAttribute("Answer", Answer)

        Return XMLNode
    End Function

    Friend Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        Question = Node.GetAttribute("Question")
        Answer = Node.GetAttribute("Answer")
    End Sub

    Public Sub New()

    End Sub

    Public Sub New(ByVal XMLNode As Xml.XmlElement)
        CreateFromXML(XMLNode)
    End Sub
End Class
