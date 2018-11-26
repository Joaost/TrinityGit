Public Class cStaffScheduleDay
    Public Shifts As cStaffScheduleShifts
    Public Day As cEventDay

    Private _role As cStaffScheduleRole
    Private _main As cEvent

    Public Sub New(ByVal Main As cEvent, ByVal Role As cStaffScheduleRole)
        _main = Main
        _role = Role
        Shifts = New cStaffScheduleShifts(Me)
    End Sub

    Public ReadOnly Property Role() As cStaffScheduleRole
        Get
            Return _role
        End Get
    End Property

    Friend Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim XMLNode As Xml.XmlElement = XMLDoc.CreateElement("Day")

        XMLNode.SetAttribute("DayDate", Day.DayDate)
        Dim XMLShifts As Xml.XmlElement = XMLDoc.CreateElement("Shifts")
        XMLNode.AppendChild(XMLShifts)
        For Each TmpShift As cStaffScheduleShift In Shifts
            XMLShifts.AppendChild(TmpShift.CreateXML(XMLDoc))
        Next
        Return XMLNode
    End Function

    Friend Sub CreateFromXML(ByVal Node As Xml.XmlElement)

        Day = _role.Location.Location.Days(CDate(Node.GetAttribute("DayDate")))

        Shifts.Clear()
        Dim XmlNode As Xml.XmlElement = Node.GetElementsByTagName("Shifts").Item(0).FirstChild
        While Not XmlNode Is Nothing
            Dim TmpShift As New cStaffScheduleShift(_main, Me)
            TmpShift.CreateFromXML(XmlNode)
            Shifts.Add(TmpShift)
            XmlNode = XmlNode.NextSibling
        End While
    End Sub
End Class
