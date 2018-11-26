Imports System
Imports System.Xml

Public Class cDayTemplate
    Public ID As String = Guid.NewGuid.ToString
    Public Name As String
    Public Description As String
    Public Shifts As New Dictionary(Of String, cShift)
    Public ForeColor As Drawing.Color = Helper.GetRandomColor
    Private _main As cEvent

    Friend Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim TmpNode As Xml.XmlElement
        TmpNode = XMLDoc.CreateElement("DayTemplate")
        TmpNode.SetAttribute("ID", ID)
        TmpNode.SetAttribute("Name", Name)
        TmpNode.SetAttribute("Description", Description)
        TmpNode.SetAttribute("Color", ForeColor.ToArgb)
        Dim TmpShiftsNode As Xml.XmlElement = XMLDoc.CreateElement("Shifts")
        For Each TmpShift As cShift In Shifts.Values
            TmpShiftsNode.AppendChild(TmpShift.CreateXML(XMLDoc))
        Next
        TmpNode.AppendChild(TmpShiftsNode)
        Return TmpNode
    End Function

    Friend Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        ID = Node.GetAttribute("ID")
        Name = Node.GetAttribute("Name")
        Description = Node.GetAttribute("Description")
        ForeColor = Drawing.Color.FromArgb(Node.GetAttribute("Color"))
        Dim XmlNode As Xml.XmlElement = Node.GetElementsByTagName("Shifts").Item(0).FirstChild
        While Not XmlNode Is Nothing
            Dim TmpShift As cShift = New cShift(_main, XmlNode)
            Shifts.Add(TmpShift.ID, TmpShift)
            XmlNode = XmlNode.NextSibling
        End While
    End Sub

    Public Sub New(ByVal main As cEvent)
        _main = main
    End Sub

    Public Overrides Function ToString() As String
        Return "Day Template: " & Name
    End Function

    Public Sub New(ByVal main As cEvent, ByVal CreateFromNode As Xml.XmlElement)
        _main = main
        CreateFromXML(CreateFromNode)
    End Sub
End Class
