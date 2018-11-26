Public Class cStaffSchedule
    Public Locations As New Collection

    Private _main As cEvent

    Public Sub New(ByVal Main As cEvent, Optional ByVal BuildMe As Boolean = False)
        _main = Main

        If BuildMe Then
            For Each TmpLoc As cLocation In MyEvent.Locations
                Dim TmpStaffLoc As New cStaffScheduleLocation(_main, Main.Schedule)
                TmpStaffLoc.Location = TmpLoc
                For Each TmpRole As cRole In MyEvent.Roles
                    Dim TmpStaffRole As New cStaffScheduleRole(_main, TmpStaffLoc)
                    TmpStaffRole.Role = TmpRole
                    For Each TmpDay As cEventDay In TmpLoc.Days
                        Dim TmpStaffDay As New cStaffScheduleDay(_main, TmpStaffRole)
                        TmpStaffDay.Day = TmpDay
                        TmpStaffRole.Days.Add(TmpStaffDay, TmpDay.DayDate)
                        For Each TmpShift As cShift In TmpDay.Template.Shifts.Values
                            Dim TmpStaffShift As New cStaffScheduleShift(_main, TmpStaffDay)
                            TmpStaffShift.Shift = TmpShift
                            TmpStaffDay.Shifts.Add(TmpStaffShift)
                            If TmpShift.Roles.Contains(TmpRole.ID) AndAlso Not TmpStaffLoc.Roles.Contains(TmpStaffRole.Role.ID) Then
                                TmpStaffLoc.Roles.Add(TmpStaffRole, TmpStaffRole.Role.ID)
                            End If
                        Next
                    Next
                Next
                Locations.Add(TmpStaffLoc, TmpStaffLoc.Location.ID)
            Next
        End If
    End Sub

    Public Sub Rebuild()
        ' Build a temporary Dictionary of current schedule
        Dim TmpSchedule As New cStaffSchedule(MyEvent, False)
        For Each TmpLoc As cLocation In MyEvent.Locations
            Dim TmpStaffLoc As New cStaffScheduleLocation(_main, TmpSchedule)
            TmpStaffLoc.Location = TmpLoc
            For Each TmpRole As cRole In MyEvent.Roles
                Dim TmpStaffRole As New cStaffScheduleRole(_main, TmpStaffLoc)
                TmpStaffRole.Role = TmpRole
                For Each TmpDay As cEventDay In TmpLoc.Days
                    Dim TmpStaffDay As New cStaffScheduleDay(_main, TmpStaffRole)
                    TmpStaffDay.Day = TmpDay
                    TmpStaffRole.Days.Add(TmpStaffDay, TmpDay.DayDate)
                    For Each TmpShift As cShift In TmpDay.Template.Shifts.Values
                        Dim TmpStaffShift As New cStaffScheduleShift(_main, TmpStaffDay)
                        TmpStaffShift.Shift = TmpShift
                        TmpStaffDay.Shifts.Add(TmpStaffShift)
                        If TmpShift.Roles.Contains(TmpRole.ID) AndAlso Not TmpStaffLoc.Roles.Contains(TmpStaffRole.Role.ID) Then
                            TmpStaffLoc.Roles.Add(TmpStaffRole, TmpStaffRole.Role.ID)
                        End If
                    Next
                Next
            Next
            TmpSchedule.Locations.Add(TmpStaffLoc, TmpStaffLoc.Location.ID)
        Next
        'Look for removed objects
RestartRemoved:
        For Each TmpLoc As cStaffScheduleLocation In Locations
            If Not TmpSchedule.Locations.Contains(TmpLoc.Location.ID) Then
                Locations.Remove(TmpLoc.Location.ID)
                GoTo RestartRemoved
            Else
                TmpLoc.Location = TmpSchedule.Locations(TmpLoc.Location.ID).Location
                For Each TmpRole As cStaffScheduleRole In TmpLoc.Roles
                    If Not TmpSchedule.Locations(TmpLoc.Location.ID).Roles.Contains(TmpRole.Role.ID) Then
                        TmpLoc.Roles.Remove(TmpRole.Role.ID)
                        GoTo RestartRemoved
                    Else
                        TmpRole.Role = TmpSchedule.Locations(TmpLoc.Location.ID).Roles(TmpRole.Role.ID).Role
                        For Each TmpDay As cStaffScheduleDay In TmpRole.Days
                            If Not TmpSchedule.Locations(TmpLoc.Location.ID).Roles(TmpRole.Role.ID).Days.Contains(TmpDay.Day.DayDate.ToShortDateString) Then
                                TmpRole.Days.Remove(TmpDay.Day.DayDate)
                                GoTo RestartRemoved
                            Else
                                TmpDay.Day = TmpSchedule.Locations(TmpLoc.Location.ID).Roles(TmpRole.Role.ID).Days(TmpDay.Day.DayDate.ToShortDateString).Day
                                For Each TmpShift As cStaffScheduleShift In TmpDay.Shifts
                                    If Not TmpSchedule.Locations(TmpLoc.Location.ID).Roles(TmpRole.Role.ID).Days(TmpDay.Day.DayDate.ToShortDateString).Shifts.Contains(TmpShift.Shift.ID) Then
                                        TmpDay.Shifts.Remove(TmpShift)
                                        GoTo RestartRemoved
                                    Else
                                        TmpShift.Shift = TmpSchedule.Locations(TmpLoc.Location.ID).Roles(TmpRole.Role.ID).Days(TmpDay.Day.DayDate.ToShortDateString).Shifts(TmpShift.Shift.ID).Shift
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        Next
        'Look for new objects
        Dim TmpXMLDoc As New Xml.XmlDocument
        For Each TmpLoc As cStaffScheduleLocation In TmpSchedule.Locations
            If Not Locations.Contains(TmpLoc.Location.ID) Then
                Locations.Add(TmpLoc, TmpLoc.Location.ID)
            End If
            For Each TmpRole As cStaffScheduleRole In TmpLoc.Roles
                If Not Locations(TmpLoc.Location.ID).Roles.Contains(TmpRole.Role.ID) Then
                    Dim NewRole As New cStaffScheduleRole(MyEvent, Locations(TmpLoc.Location.ID))
                    NewRole.CreateFromXML(TmpRole.CreateXML(TmpXMLDoc))
                    Locations(TmpLoc.Location.ID).Roles.Add(NewRole, NewRole.Role.ID)
                End If
                For Each TmpDay As cStaffScheduleDay In TmpRole.Days
                    If Not Locations(TmpLoc.Location.ID).Roles(TmpRole.Role.ID).Days.Contains(TmpDay.Day.DayDate.ToShortDateString) Then
                        Dim NewDay As New cStaffScheduleDay(MyEvent, Locations(TmpLoc.Location.ID).Roles(TmpRole.Role.ID))
                        NewDay.CreateFromXML(TmpDay.CreateXML(TmpXMLDoc))
                        Locations(TmpLoc.Location.ID).Roles(TmpRole.Role.ID).Days.Add(NewDay, NewDay.Day.DayDate.ToShortDateString)
                    End If
                    For Each TmpShift As cStaffScheduleShift In TmpDay.Shifts
                        If Not Locations(TmpLoc.Location.ID).Roles(TmpRole.Role.ID).Days(TmpDay.Day.DayDate.ToShortDateString).Shifts.Contains(TmpShift.Shift.ID) Then
                            Dim NewShift As New cStaffScheduleShift(MyEvent, Locations(TmpLoc.Location.ID).Roles(TmpRole.Role.ID).Days(TmpDay.Day.DayDate.ToShortDateString))
                            NewShift.CreateFromXML(TmpShift.CreateXML(TmpXMLDoc))
                            Locations(TmpLoc.Location.ID).Roles(TmpRole.Role.ID).Days(TmpDay.Day.DayDate.ToShortDateString).Shifts.Add(NewShift, NewShift.Shift.ID)
                        End If
                    Next
                Next
            Next
        Next
    End Sub

    Friend Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim XMLNode As Xml.XmlElement = XMLDoc.CreateElement("Schedule")
        Dim LocNode As Xml.XmlElement = XMLDoc.CreateElement("Locations")
        XMLNode.AppendChild(LocNode)

        For Each TmpLoc As cStaffScheduleLocation In Locations
            LocNode.AppendChild(TmpLoc.CreateXML(XMLDoc))
        Next
        Return XMLNode
    End Function

    Friend Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        Locations.Clear()
        Dim XmlNode As Xml.XmlElement = Node.GetElementsByTagName("Locations").Item(0).FirstChild
        While Not XmlNode Is Nothing
            Dim TmpLoc As New cStaffScheduleLocation(_main, Me)
            TmpLoc.CreateFromXML(XmlNode)
            Locations.Add(TmpLoc, TmpLoc.Location.ID)
            XmlNode = XmlNode.NextSibling
        End While
    End Sub

    Public ReadOnly Property RootEvent() As cEvent
        Get
            Return _main
        End Get
    End Property

    Private _shiftlist As New List(Of cStaffScheduleShift)
    Public ReadOnly Property ShiftList() As List(Of cStaffScheduleShift)
        Get
            Return _shiftList
        End Get
    End Property
End Class
