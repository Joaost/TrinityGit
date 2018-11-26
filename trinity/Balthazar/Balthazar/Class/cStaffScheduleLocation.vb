Public Class cStaffScheduleLocation
    Public Roles As New Collection
    Public Location As cLocation
    Public DatabaseID As Integer = -1

    Private _main As cEvent
    Private _schedule As cStaffSchedule

    Public Sub New(ByVal Main As cEvent, ByVal Schedule As cStaffSchedule)
        _main = Main
        _schedule = Schedule
    End Sub

    Public ReadOnly Property Schedule() As cStaffSchedule
        Get
            Return _schedule
        End Get
    End Property

    Friend ReadOnly Property RootEvent() As cEvent
        Get
            Return _main
        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return Location.Name
        End Get
    End Property

    Friend Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim XMLNode As Xml.XmlElement = XMLDoc.CreateElement("Location")

        XMLNode.SetAttribute("Location", Location.ID)
        XMLNode.SetAttribute("DatabaseID", DatabaseID)

        Dim XMLRoles As Xml.XmlElement = XMLDoc.CreateElement("Roles")
        XMLNode.AppendChild(XMLRoles)

        For Each TmpRole As cStaffScheduleRole In Roles
            XMLRoles.AppendChild(TmpRole.CreateXML(XMLDoc))
        Next
        Return XMLNode
    End Function

    Friend Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        Location = _main.Locations(Node.GetAttribute("Location"))
        DatabaseID = Node.GetAttribute("DatabaseID")

        Roles.Clear()
        Dim XmlNode As Xml.XmlElement = Node.GetElementsByTagName("Roles").Item(0).FirstChild
        While Not XmlNode Is Nothing
            Dim TmpRole As New cStaffScheduleRole(_main, Me)
            TmpRole.CreateFromXML(XmlNode)
            Roles.Add(TmpRole, TmpRole.Role.ID)
            XmlNode = XmlNode.NextSibling
        End While
    End Sub


End Class
