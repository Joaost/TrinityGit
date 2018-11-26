Public Class cStaffScheduleShift
    Public AssignedStaff As New Collection
    Public ReserveStaff As New List(Of cStaff)
    Public DatabaseID As Integer = -1
    Public Shift As cShift

    Private _day As cStaffScheduleDay
    Private _main As cEvent

    Public Sub New(ByVal Main As cEvent, ByVal Day As cStaffScheduleDay)
        _main = Main
        _day = Day
    End Sub

    Public ReadOnly Property Day() As cStaffScheduleDay
        Get
            Return _day
        End Get
    End Property

    Public ReadOnly Property RootEvent() As cEvent
        Get
            Return _main
        End Get
    End Property

    Friend Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim XMLNode As Xml.XmlElement = XMLDoc.CreateElement("Shift")

        XMLNode.SetAttribute("ShiftID", Shift.ID)
        XMLNode.SetAttribute("DatabaseID", DatabaseID)

        Dim StaffNode As Xml.XmlElement = XMLDoc.CreateElement("AssignedStaff")
        For Each TmpStaff As cStaff In AssignedStaff
            Dim Node As Xml.XmlElement = XMLDoc.CreateElement("Staff")
            Node.SetAttribute("DatabaseID", TmpStaff.DatabaseID)
            StaffNode.AppendChild(Node)
        Next
        XMLNode.AppendChild(StaffNode)

        Return XMLNode
    End Function

    Friend Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        Shift = _day.Day.Template.Shifts(Node.GetAttribute("ShiftID"))
        DatabaseID = Node.GetAttribute("DatabaseID")

        Dim StaffNode As Xml.XmlElement = Node.GetElementsByTagName("AssignedStaff").Item(0).FirstChild
        While Not StaffNode Is Nothing
            Dim TmpStaff As cStaff = Database.GetSingleStaff(StaffNode.GetAttribute("DatabaseID"))
            StaffNode = StaffNode.NextSibling
            AssignedStaff.Add(TmpStaff, "DB" & TmpStaff.DatabaseID)
        End While
    End Sub
End Class
