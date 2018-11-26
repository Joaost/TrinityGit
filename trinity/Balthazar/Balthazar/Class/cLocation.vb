Imports System
Imports System.Xml

Public Class cLocation
    Public ID As String = Guid.NewGuid.ToString
    Public Name As String
    Private _days As cEventDays

    Private _main As cEvent

    Public ReadOnly Property Days() As cEventDays
        Get
            Return _days
        End Get
    End Property

    Public ReadOnly Property FromDate() As Date
        Get
            If Not _days.Count = 0 Then
                Return _days(0).DayDate
            Else
                Return Date.FromOADate(0)
            End If
        End Get
    End Property

    Public ReadOnly Property ToDate() As Date
        Get
            If Not _days.Count = 0 Then
                Return _days(_days.Count - 1).DayDate
            Else
                Return Date.FromOADate(0)
            End If
        End Get
    End Property

    Public Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim XMLNode As Xml.XmlElement = XMLDoc.CreateElement("Location")
        XMLNode.SetAttribute("ID", ID)
        XMLNode.SetAttribute("Name", Name)
        XMLNode.AppendChild(_days.CreateXML(XMLDoc))
        Return XMLNode
    End Function

    Public Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        ID = Node.GetAttribute("ID")
        Name = Node.GetAttribute("Name")
        _days.CreateFromXML(Node.GetElementsByTagName("Days").Item(0))
    End Sub

    Public Sub New(ByVal main As cEvent, ByVal CreateFromNode As Xml.XmlElement)
        _main = main
        _days = New cEventDays(main)
        CreateFromXML(CreateFromNode)
    End Sub

    Public Sub New(ByVal main As cEvent)
        _main = main
        _days = New cEventDays(main)
    End Sub
End Class
