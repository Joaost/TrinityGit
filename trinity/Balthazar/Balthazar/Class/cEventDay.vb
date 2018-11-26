Imports System
Imports System.Xml

Public Class cEventDay
    Public DayDate As Date
    Public ID As String = Guid.NewGuid.ToString
    'Public Shifts As New Dictionary(Of String, cShift)
    Private _template As cDayTemplate
    Private _main As cEvent

    Public Property Template() As cDayTemplate
        Get
            Return _template
        End Get
        Set(ByVal value As cDayTemplate)
            _template = value
        End Set
    End Property

    Friend Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim TmpNode As Xml.XmlElement
        TmpNode = XMLDoc.CreateElement("Day")
        TmpNode.SetAttribute("Date", DayDate)
        TmpNode.SetAttribute("ID", ID)
        'Dim TmpShiftsNode As Xml.XmlElement = XMLDoc.CreateElement("Shifts")
        'For Each TmpShift As cShift In Shifts.Values
        '    TmpShiftsNode.AppendChild(TmpShift.CreateXML(XMLDoc))
        'Next
        'TmpNode.AppendChild(TmpShiftsNode)
        If _template IsNot Nothing Then
            TmpNode.SetAttribute("TemplateID", _template.ID)
        End If
        Return TmpNode
    End Function

    Friend Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        DayDate = Node.GetAttribute("Date")
        If Not Node.GetAttribute("TemplateID") = "" Then
            _template = _main.DayTemplates(Node.GetAttribute("TemplateID"))
        End If
        ID = Node.GetAttribute("ID")
        'Dim XmlNode As Xml.XmlElement = Node.GetElementsByTagName("Shifts").Item(0).FirstChild
        'While Not XmlNode Is Nothing
        '    Dim TmpShift As cShift = New cShift(_main, XmlNode)
        '    Shifts.Add(TmpShift.ID, TmpShift)
        '    XmlNode = XmlNode.NextSibling
        'End While
    End Sub

    Public Sub CreateFromTemplate(ByVal Template As cDayTemplate, ByVal OnDate As Date)
        DayDate = OnDate
        'Shifts.Clear()
        'For Each TmpShift As cShift In Template.Shifts
        '    Dim TmpDoc As New Xml.XmlDocument
        '    Dim NewShift As cShift = New cShift(_main, TmpShift.CreateXML(TmpDoc))
        '    Shifts.Add(NewShift.ID, NewShift)
        '    'For Each TmpRole As cRole In TmpShift.Roles
        '    '    Dim NewRole As New cRole
        '    '    NewRole.ID = TmpRole.ID
        '    '    NewRole.Name = TmpRole.Name
        '    '    NewRole.Description = TmpRole.Description
        '    '    NewRole.Category = TmpRole.Category
        '    '    NewRole.Quantity = TmpRole.Quantity
        '    '    Shifts(TmpShift.ID).Roles.Add(NewRole)
        '    'Next
        'Next
        _template = Template
    End Sub

    Public Sub New(ByVal Main As cEvent)
        _main = Main
    End Sub

    Public Sub New(ByVal Main As cEvent, ByVal CreateFromNode As Xml.XmlElement)
        _main = Main
        CreateFromXML(CreateFromNode)
    End Sub

End Class
