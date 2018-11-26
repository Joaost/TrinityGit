Public Class cStaffScheduleRole
    Public Days As New Collection
    Public AvailableForStaff As New Collection
    Public Role As cRole
    Public DatabaseID As Integer = -1
    Private _loc As cStaffScheduleLocation
    Private _main As cEvent

    Public Sub New(ByVal Main As cEvent, ByVal Location As cStaffScheduleLocation)
        _main = Main
        _loc = Location
    End Sub

    Public ReadOnly Property AcceptedByStaff() As List(Of cStaff)
        Get

        End Get
    End Property

    Public ReadOnly Property Location() As cStaffScheduleLocation
        Get
            Return _loc
        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return Role.Name
        End Get
    End Property

    Friend Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim XMLNode As Xml.XmlElement = XMLDoc.CreateElement("Role")

        XMLNode.SetAttribute("Role", Role.ID)
        XMLNode.SetAttribute("DatabaseID", DatabaseID)

        Dim XMLDays As Xml.XmlElement = XMLDoc.CreateElement("Days")
        XMLNode.AppendChild(XMLDays)
        For Each TmpDay As cStaffScheduleDay In Days
            XMLDays.AppendChild(TmpDay.CreateXML(XMLDoc))
        Next

        Dim XMLStaff As Xml.XmlElement = XMLDoc.CreateElement("AvailableForStaff")
        XMLNode.AppendChild(XMLStaff)
        For Each TmpStaff As cStaff In AvailableForStaff
            XMLStaff.AppendChild(TmpStaff.CreateXML(XMLDoc))
        Next

        Return XMLNode
    End Function

    Friend Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        Role = _main.Roles(Node.GetAttribute("Role"))
        DatabaseID = Node.GetAttribute("DatabaseID")

        Days.Clear()
        Dim XmlNode As Xml.XmlElement = Node.GetElementsByTagName("Days").Item(0).FirstChild
        While Not XmlNode Is Nothing
            Dim TmpDay As New cStaffScheduleDay(_main, Me)
            TmpDay.CreateFromXML(XmlNode)
            Days.Add(TmpDay, TmpDay.Day.DayDate)
            XmlNode = XmlNode.NextSibling
        End While

        XmlNode = Node.GetElementsByTagName("AvailableForStaff").Item(0).FirstChild
        While Not XmlNode Is Nothing
            Dim TmpStaff As cStaff = Database.GetSingleStaff(XmlNode.GetAttribute("DatabaseID"))
            AvailableForStaff.Add(TmpStaff, "DB" & TmpStaff.DatabaseID)
            XmlNode = XmlNode.NextSibling
        End While

    End Sub
End Class
