Public Class cInStore
    Public StartDate As Date
    Public EndDate As Date
    Public MaxBookingsPerDay As Integer
    Public ChosenSalespersons As New List(Of cChosenSalesPerson)
    Public ChosenProviders As New List(Of cStaff)
    Public ExcludedDates As New List(Of Date)
    Private _event As cEvent

    Public ReadOnly Property [Event]() As cEvent
        Get
            Return _event
        End Get
    End Property

    Private _demoInstructions As cDocument
    Public Property DemoInstructions() As cDocument
        Get
            Return _demoInstructions
        End Get
        Set(ByVal value As cDocument)
            _demoInstructions = value
        End Set
    End Property

    Friend Function CreateXML(ByVal XMLDoc As System.Xml.XmlDocument) As System.Xml.XmlElement
        Dim TmpNode As Xml.XmlElement = XMLDoc.CreateElement("InStore")
        TmpNode.SetAttribute("StartDate", StartDate)
        TmpNode.SetAttribute("EndDate", EndDate)
        TmpNode.SetAttribute("MaxBookingsPerDay", MaxBookingsPerDay)
        If DemoInstructions IsNot Nothing Then
            TmpNode.SetAttribute("DemoInstructions", DemoInstructions.ID)
        Else
            TmpNode.SetAttribute("DemoInstructions", "")
        End If
        Dim Node As Xml.XmlElement = XMLDoc.CreateElement("ChosenSalespersons")

        For Each TmpStaff As cChosenSalesPerson In ChosenSalespersons
            If TmpStaff.Staff IsNot Nothing Then
                Dim TmpSPNode As Xml.XmlElement = XMLDoc.CreateElement("Salesperson")
                TmpSPNode.SetAttribute("StaffID", TmpStaff.Staff.DatabaseID)
                TmpSPNode.SetAttribute("MaxDays", TmpStaff.MaxDays)
                Node.AppendChild(TmpSPNode)
            End If
        Next
        TmpNode.AppendChild(Node)

        Node = XMLDoc.CreateElement("ChosenProviders")
        For Each TmpStaff As cStaff In ChosenProviders
            If TmpStaff IsNot Nothing Then Node.AppendChild(TmpStaff.CreateXML(XMLDoc))
        Next
        TmpNode.AppendChild(Node)

        Node = XMLDoc.CreateElement("ExcludedDates")

        For Each TmpDate As Date In ExcludedDates
            Dim DateNode As Xml.XmlElement = XMLDoc.CreateElement("Date")
            DateNode.SetAttribute("Day", TmpDate)
            Node.AppendChild(DateNode)
        Next
        TmpNode.AppendChild(Node)

        Return TmpNode
    End Function

    Friend Sub CreateFromXML(ByVal Node As System.Xml.XmlElement)
        If Node IsNot Nothing Then
            If Node.GetAttribute("StartDate") <> "" Then
                StartDate = Node.GetAttribute("StartDate")
                EndDate = Node.GetAttribute("EndDate")
                MaxBookingsPerDay = Node.GetAttribute("MaxBookingsPerDay")
                If Node.GetAttribute("DemoInstructions") <> "" Then
                    DemoInstructions = _event.Documents(Node.GetAttribute("DemoInstructions"))
                End If
                ChosenSalespersons.Clear()
                Dim XmlNode As Xml.XmlElement = Node.GetElementsByTagName("ChosenSalespersons").Item(0).FirstChild
                While Not XmlNode Is Nothing
                    Dim TmpStaff As cStaff = Database.GetSingleStaff(XmlNode.GetAttribute("StaffID"))
                    Dim TmpSP As New cChosenSalesPerson
                    TmpSP.Staff = TmpStaff
                    TmpSP.MaxDays = XmlNode.GetAttribute("MaxDays")
                    ChosenSalespersons.Add(TmpSP)
                    XmlNode = XmlNode.NextSibling
                End While

                ChosenProviders.Clear()
                XmlNode = Node.GetElementsByTagName("ChosenProviders").Item(0).FirstChild
                While Not XmlNode Is Nothing
                    Dim TmpStaff As cStaff = Database.GetSingleStaff(XmlNode.GetAttribute("DatabaseID"))
                    ' If Not TmpStaff Is Nothing Then
                    ChosenProviders.Add(TmpStaff)
                    XmlNode = XmlNode.NextSibling
                    '   End If
                End While

                ExcludedDates.Clear()
                XmlNode = Node.GetElementsByTagName("ExcludedDates").Item(0).FirstChild
                While Not XmlNode Is Nothing
                    ExcludedDates.Add(XmlNode.GetAttribute("Day"))
                    XmlNode = XmlNode.NextSibling
                End While
            End If
        End If
    End Sub

    Public Sub New(ByVal CreateFromNode As Xml.XmlElement)
        CreateFromXML(CreateFromNode)
    End Sub

    Public Sub New(ByVal [Event] As cEvent)
        _event = [Event]
    End Sub

End Class
