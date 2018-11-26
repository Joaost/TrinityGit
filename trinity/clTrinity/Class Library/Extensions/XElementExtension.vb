Imports System.Xml.Linq
Imports System.Runtime.CompilerServices

Namespace Trinity
    Module XElementExtensions

        <Extension()> Public Function ToXElement(xml As XmlElement) As XElement
            Dim _element As XElement = Nothing
            If xml IsNot Nothing Then
                _element = XElement.Parse(xml.OuterXml)
            End If
            Return _element
        End Function

        <Extension()> Public Function ToXMLElement(xml As XElement) As XmlElement
            Dim _XMLDoc As New XmlDocument
            Dim reader As XmlReader = xml.CreateReader
            _XMLDoc.Load(reader)
            Return _XMLDoc.FirstChild
        End Function
    End Module
End Namespace
