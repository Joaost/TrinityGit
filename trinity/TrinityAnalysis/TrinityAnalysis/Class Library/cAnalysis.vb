Imports System.Xml

Public Class cAnalysis

    Private mvarChannels As New cChannels(Me)

    Public ReadOnly Property Channels() As cChannels
        Get
            Channels = mvarChannels
        End Get
    End Property

    Public Sub CreateChannels(ByVal xml As String)

        Dim XMLDoc As Xml.XmlDocument = New Xml.XmlDocument
        Dim XMLTmpNode As Xml.XmlElement
        Dim XMLChannels As Xml.XmlElement

        Dim tmpChannel As cChannel

        XMLDoc.Load(xml)

        XMLChannels = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("Channels")

        XMLTmpNode = XMLChannels.ChildNodes.Item(0)

        mvarChannels = New cChannels(Me)

        mvarChannels.MainObject = Me

        While Not XMLTmpNode Is Nothing

            tmpChannel = mvarChannels.Add(XMLTmpNode.GetAttribute("Name"), "Channels.xml")

            XMLTmpNode = XMLTmpNode.NextSibling
        End While
    End Sub
End Class
