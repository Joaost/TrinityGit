Imports System
Imports System.Xml

Public Class cEvent
    Public InternalContacts As New cContacts(Me)
    Public ExternalContacts As New cContacts(Me)
    Public Client As cClient
    Public Product As cProduct
    Public Name As String
    Public Campaign As New cCampaign
    Public OurMission As String
    Public Purposes As New List(Of String)
    Public Target As String
    Public Message As String
    Public Goals As New List(Of String)
    Public CoreValues As String
    Public Locations As New cLocations(Me)
    Public Roles As New cRoles(Me)
    Public StaffCategories As New cStaffCategories(Me)
    'Public MaterialCosts As New List(Of cCost)
    'Public LogisticsCosts As New List(Of cCost)
    'Public PlanningCosts As New List(Of cHourCost)
    Public DayTemplates As New cDayTemplates(Me)
    Public Budgets As New Dictionary(Of String, cBudget)
    Public DatabaseID As Integer = -1
    Public Questionaires As New List(Of cQuestionaire)
    Public Schedule As cStaffSchedule
    Public QuestionAndAnswers As New List(Of cQuestionAndAnswer)
    Public Templates As New List(Of cTemplate)
    Public ImportantDates As New List(Of cImportantDate)
    Public Documents As New SortedList(Of String, cDocument)
    Public Budget As New cBudget(Me, Nothing)
    Public InStore As New cInStore(Me)
    Public UseInStore As Boolean = False

    Public Event SaveProgres(ByVal p As Integer)

    Friend Class BookingDataStruct
        Public Status As cBooking.BookingStatusEnum
        Public RejectionComment As String

        Private _provider As cStaff
        Public Property Provider() As cStaff
            Get
                Return _provider
            End Get
            Set(ByVal value As cStaff)
                _provider = value
            End Set
        End Property

        Private _providerName As String
        Public Property ProviderName() As String
            Get
                If _provider Is Nothing Then
                    Return _providerName
                Else
                    Return _provider.Firstname
                End If
            End Get
            Set(ByVal value As String)
                _providerName = value
            End Set
        End Property

        Friend Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
            Dim TmpNode As Xml.XmlElement = XMLDoc.CreateElement("Cost")
            If _provider IsNot Nothing Then
                TmpNode.SetAttribute("ProviderID", _provider.DatabaseID)
            Else
                TmpNode.SetAttribute("ProviderID", 0)
            End If
            TmpNode.SetAttribute("ProviderName", ProviderName)
            TmpNode.SetAttribute("Confirmed", Status)
            TmpNode.SetAttribute("RejectionComment", RejectionComment)
            Return TmpNode
        End Function


        Friend Overridable Sub CreateFromXML(ByVal Node As Xml.XmlElement)
            If Node.GetAttribute("ProviderID") = 0 Then
                _provider = Nothing
            Else
                _provider = Database.GetSingleStaff(Node.GetAttribute("ProviderID"))
            End If
            ProviderName = Node.GetAttribute("ProviderName")
            Status = Node.GetAttribute("Confirmed")
            RejectionComment = Node.GetAttribute("RejectionComment")
        End Sub

        Public Sub New()

        End Sub

        Public Sub New(ByVal Node As Xml.XmlElement)
            CreateFromXML(Node)
        End Sub
    End Class

    Private _bookingData As New Dictionary(Of Integer, BookingDataStruct)
    Friend ReadOnly Property BookingData() As Dictionary(Of Integer, BookingDataStruct)
        Get
            Return _bookingData
        End Get
    End Property

    Public ReadOnly Property StartDate() As Date
        Get
            If UseInStore Then
                Return InStore.StartDate
            Else
                Dim tmpStart As Date = Nothing
                For Each TmpLoc As cLocation In Locations
                    If tmpStart = Nothing OrElse tmpStart > TmpLoc.FromDate Then
                        tmpStart = TmpLoc.FromDate
                    End If
                Next
                Return tmpStart
            End If
        End Get
    End Property

    Public ReadOnly Property EndDate() As Date
        Get
            If UseInStore Then
                Return InStore.EndDate
            Else
                Dim tmpEnd As Date = Nothing
                For Each TmpLoc As cLocation In Locations
                    If tmpEnd = Nothing OrElse tmpEnd < TmpLoc.FromDate Then
                        tmpEnd = TmpLoc.FromDate
                    End If
                Next
                Return tmpEnd
            End If
        End Get
    End Property


    Public Sub Save()
        Dim XMLDoc As New XmlDocument
        Dim XMLEvent As XmlElement
        Dim XMLCampaign As XmlElement
        Dim XMLNodes As XmlElement
        Dim XMLNode As XmlElement
        Dim TmpNode As Object

        RaiseEvent SaveProgres(0)

        XMLDoc.PreserveWhitespace = True
        TmpNode = XMLDoc.CreateProcessingInstruction("xml", "version='1.0'")
        XMLDoc.AppendChild(TmpNode)

        TmpNode = XMLDoc.CreateComment("Balthazar Event.")
        XMLDoc.AppendChild(TmpNode)

        XMLEvent = XMLDoc.CreateElement("Event")
        XMLDoc.AppendChild(XMLEvent)

        XMLEvent.SetAttribute("Name", Name)
        If Client IsNot Nothing Then
            XMLEvent.SetAttribute("ClientID", Client.ID)
            XMLEvent.SetAttribute("ClientName", Client.Name)
        End If
        If Product IsNot Nothing Then
            XMLEvent.SetAttribute("ProductID", Product.ID)
            XMLEvent.SetAttribute("ProductName", Product.Name)
        End If
        XMLEvent.SetAttribute("UseInStore", UseInStore)

        RaiseEvent SaveProgres(4)

        XMLNodes = XMLDoc.CreateElement("InternalContacts")
        XMLNodes = InternalContacts.CreateXML(XMLNodes, XMLDoc)
        XMLEvent.AppendChild(XMLNodes)

        RaiseEvent SaveProgres(8)

        XMLNodes = XMLDoc.CreateElement("ExternalContacts")
        XMLNodes = ExternalContacts.CreateXML(XMLNodes, XMLDoc)
        XMLEvent.AppendChild(XMLNodes)

        RaiseEvent SaveProgres(12)
        XMLCampaign = XMLDoc.CreateElement("Campaign")
        XMLCampaign = Campaign.CreateXML(XMLCampaign, XMLDoc)
        XMLEvent.AppendChild(XMLCampaign)

        RaiseEvent SaveProgres(16)
        XMLNodes = XMLDoc.CreateElement("StaffCategories")
        XMLNodes = StaffCategories.CreateXML(XMLNodes, XMLDoc)
        XMLEvent.AppendChild(XMLNodes)

        XMLEvent.SetAttribute("Mission", OurMission)
        XMLEvent.SetAttribute("CoreValues", CoreValues)
        XMLEvent.SetAttribute("Target", Target)
        XMLEvent.SetAttribute("Message", Message)

        RaiseEvent SaveProgres(20)
        XMLNodes = XMLDoc.CreateElement("Purposes")
        For Each s As String In Purposes
            XMLNode = XMLDoc.CreateElement("Purpose")
            XMLNode.SetAttribute("Text", s)
            XMLNodes.AppendChild(XMLNode)
        Next
        XMLEvent.AppendChild(XMLNodes)

        RaiseEvent SaveProgres(24)
        XMLNodes = XMLDoc.CreateElement("Goals")
        For Each s As String In Goals
            XMLNode = XMLDoc.CreateElement("Goal")
            XMLNode.SetAttribute("Text", s)
            XMLNodes.AppendChild(XMLNode)
        Next
        XMLEvent.AppendChild(XMLNodes)

        XMLEvent.AppendChild(Budget.CreateXML(XMLDoc))

        RaiseEvent SaveProgres(40)
        XMLNodes = XMLDoc.CreateElement("QuestionAndAnswers")
        For Each TmpQA As cQuestionAndAnswer In QuestionAndAnswers
            XMLNodes.AppendChild(TmpQA.CreateXML(XMLDoc))
        Next
        XMLEvent.AppendChild(XMLNodes)

        RaiseEvent SaveProgres(44)
        XMLNodes = XMLDoc.CreateElement("Roles")
        XMLNodes = Roles.CreateXML(XMLNodes, XMLDoc)
        XMLEvent.AppendChild(XMLNodes)

        RaiseEvent SaveProgres(48)
        XMLNodes = XMLDoc.CreateElement("Templates")
        For Each TmpTemplate As cTemplate In Templates
            XMLNodes.AppendChild(TmpTemplate.CreateXML(XMLDoc))
        Next
        XMLEvent.AppendChild(XMLNodes)

        RaiseEvent SaveProgres(52)
        XMLNodes = XMLDoc.CreateElement("ImportantDates")
        For Each TmpDate As cImportantDate In ImportantDates
            XMLNodes.AppendChild(TmpDate.CreateXML(XMLDoc))
        Next
        XMLEvent.AppendChild(XMLNodes)

        RaiseEvent SaveProgres(56)
        XMLEvent.AppendChild(DayTemplates.CreateXML(XMLDoc))

        RaiseEvent SaveProgres(60)
        XMLEvent.AppendChild(Locations.CreateXML(XMLDoc))

        RaiseEvent SaveProgres(64)
        XMLNodes = XMLDoc.CreateElement("Questionaires")
        For Each TmpQuestionaire As cQuestionaire In Questionaires
            XMLNodes.AppendChild(TmpQuestionaire.CreateXML(XMLDoc))
        Next
        XMLEvent.AppendChild(XMLNodes)

        RaiseEvent SaveProgres(68)
        If Not Schedule Is Nothing Then
            XMLEvent.AppendChild(Schedule.CreateXML(XMLDoc))
        End If

        RaiseEvent SaveProgres(72)
        XMLNodes = XMLDoc.CreateElement("Documents")
        For Each TmpDoc As cDocument In Documents.Values
            XMLNodes.AppendChild(TmpDoc.CreateXML(XMLDoc))
        Next
        XMLEvent.AppendChild(XMLNodes)

        RaiseEvent SaveProgres(76)
        XMLEvent.AppendChild(InStore.CreateXML(XMLDoc))

        RaiseEvent SaveProgres(80)
        XMLNodes = XMLDoc.CreateElement("Bookings")
        For Each kv As KeyValuePair(Of Integer, BookingDataStruct) In BookingData
            XMLNode = kv.Value.CreateXML(XMLDoc)
            XMLNode.SetAttribute("ID", kv.Key)
            XMLNodes.AppendChild(XMLNode)
        Next
        XMLEvent.AppendChild(XMLNodes)

        If Product IsNot Nothing Then
            DatabaseID = Database.SaveEventToDB(Name, XMLDoc.OuterXml, Me, Product.ID, DatabaseID)
        Else
            DatabaseID = Database.SaveEventToDB(Name, XMLDoc.OuterXml, Me, -1, DatabaseID)
        End If
        RaiseEvent SaveProgres(100)
    End Sub

    Public Sub Load(ByVal XML As String)
        StaffCategories.Clear()
        Budget = New cBudget(Me, Nothing)

        Dim XMLDoc As New XmlDocument
        Dim XMLEvent As XmlElement

        XMLDoc.LoadXml(XML)

        XMLEvent = XMLDoc.GetElementsByTagName("Event").Item(0)

        'Read basic variables

        Name = XMLEvent.GetAttribute("Name")
        If Not XMLEvent.GetAttribute("ClientID") = "" Then
            Client = Database.GetClientByID(XMLEvent.GetAttribute("ClientID"))
            If XMLEvent.GetAttribute("ClientName") <> Client.Name Then
                '*** Ask if you are sure you want to use client
            End If
        End If
        If Not XMLEvent.GetAttribute("ProductID") = "" Then
            Product = Database.GetProductByID(XMLEvent.GetAttribute("ProductID"))
            If XMLEvent.GetAttribute("ProductName") <> Product.Name Then
                '*** Ask if you are sure you want to use Product
            End If
        End If

        If Not XMLEvent.GetAttribute("UseInStore") = "" Then
            UseInStore = XMLEvent.GetAttribute("UseInStore")
        End If

        Dim XmlNode As Xml.XmlElement = XMLDoc.GetElementsByTagName("InternalContacts").Item(0)
        InternalContacts.CreateFromXML(XmlNode)

        XmlNode = XMLDoc.GetElementsByTagName("ExternalContacts").Item(0)
        ExternalContacts.CreateFromXML(XmlNode)

        XmlNode = XMLDoc.GetElementsByTagName("Campaign").Item(0)
        Campaign.CreateFromXML(XmlNode)

        OurMission = XMLEvent.GetAttribute("Mission")
        CoreValues = XMLEvent.GetAttribute("CoreValues")
        Target = XMLEvent.GetAttribute("Target")
        Message = XMLEvent.GetAttribute("Message")

        XmlNode = XMLDoc.GetElementsByTagName("StaffCategories").Item(0)
        StaffCategories.CreateFromXML(XmlNode)

        Purposes.Clear()
        XmlNode = XMLDoc.GetElementsByTagName("Purposes").Item(0).FirstChild
        While Not XmlNode Is Nothing
            Dim TmpStr = XmlNode.GetAttribute("Text")
            Purposes.Add(TmpStr)
            XmlNode = XmlNode.NextSibling
        End While

        Goals.Clear()
        XmlNode = XMLDoc.GetElementsByTagName("Goals").Item(0).FirstChild
        While Not XmlNode Is Nothing
            Dim TmpStr = XmlNode.GetAttribute("Text")
            Goals.Add(TmpStr)
            XmlNode = XmlNode.NextSibling
        End While

        Budget.CreateFromXML(XMLDoc.GetElementsByTagName("Budget")(0))

        QuestionAndAnswers.Clear()
        XmlNode = XMLDoc.GetElementsByTagName("QuestionAndAnswers").Item(0).FirstChild
        While Not XmlNode Is Nothing
            QuestionAndAnswers.Add(New cQuestionAndAnswer(XmlNode))
            XmlNode = XmlNode.NextSibling
        End While

        Templates.Clear()
        XmlNode = XMLDoc.GetElementsByTagName("Templates").Item(0).FirstChild
        While Not XmlNode Is Nothing
            Templates.Add(New cTemplate(XmlNode))
            XmlNode = XmlNode.NextSibling
        End While

        ImportantDates.Clear()
        XmlNode = XMLDoc.GetElementsByTagName("ImportantDates").Item(0).FirstChild
        While Not XmlNode Is Nothing
            ImportantDates.Add(New cImportantDate(XmlNode))
            XmlNode = XmlNode.NextSibling
        End While

        Documents.Clear()
        XmlNode = XMLDoc.GetElementsByTagName("Documents").Item(0).FirstChild
        While Not XmlNode Is Nothing
            Dim TmpDoc As New cDocument(XmlNode)
            Documents.Add(TmpDoc.ID, TmpDoc)
            XmlNode = XmlNode.NextSibling
        End While

        BookingData.Clear()
        'XmlNode = XMLDoc.GetElementsByTagName("Bookings").Item(0).FirstChild
        'While Not XmlNode Is Nothing
        '    BookingData.Add(XmlNode.GetAttribute("ID"), New BookingDataStruct(XmlNode))
        '    XmlNode = XmlNode.NextSibling
        'End While

        Roles.CreateFromXML(XMLDoc.GetElementsByTagName("Roles").Item(0))

        DayTemplates.CreateFromXML(XMLDoc.GetElementsByTagName("DayTemplates").Item(0))

        Locations.CreateFromXML(XMLDoc.GetElementsByTagName("Locations").Item(0))

        Questionaires.Clear()
        If Not XMLDoc.GetElementsByTagName("Questionaires").Count = 0 Then
            For Each _node As XmlElement In XMLDoc.GetElementsByTagName("Questionaires")(0).ChildNodes
                Dim _questionaire As New cQuestionaire
                _questionaire.CreateFromXML(_node)
                Questionaires.Add(_questionaire)
            Next
        Else
            If Not XMLDoc.GetElementsByTagName("Questionaire").Count = 0 Then
                MessageBox.Show("This event uses an old Questionaire format that is" & vbCrLf & "no longer supported." & vbCrLf & "No questionaire was loaded.", "BALTHAZAR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        InStore.CreateFromXML(XMLDoc.GetElementsByTagName("InStore").Item(0))

        If XMLDoc.GetElementsByTagName("Schedule").Item(0) IsNot Nothing Then
            Schedule = New cStaffSchedule(Me)
            Schedule.CreateFromXML(XMLDoc.GetElementsByTagName("Schedule").Item(0))
        End If
    End Sub

    Public Function ShiftList() As List(Of cShift)
        Dim TmpList As New List(Of cShift)
        For Each TmpLoc As cLocation In Locations
            For Each TmpDay As cEventDay In TmpLoc.Days
                If TmpDay.Template IsNot Nothing Then
                    For Each TmpShift As cShift In TmpDay.Template.Shifts.Values
                        TmpList.Add(TmpShift)
                    Next
                End If
            Next
        Next
        Return TmpList
    End Function

    Public Function CTC() As Decimal
        Dim _ctc As Single = 0

        For Each TmpCat As cStaffCategory In Budget.StaffCosts
            _ctc += TmpCat.CTC
        Next
        For Each TmpCost As cCost In Budget.MaterialCosts
            _ctc += TmpCost.CTC
        Next
        For Each TmpCost As cCost In Budget.LogisticsCosts
            _ctc += TmpCost.CTC
        Next
        For Each TmpCost As cHourCost In Budget.PlanningCosts
            _ctc += TmpCost.CTC
        Next
        Return _ctc
    End Function

    Public Function ActualCost() As Decimal
        Dim _cost As Single = 0

        For Each TmpCat As cStaffCategory In Budget.StaffCosts
            _cost += TmpCat.ActualCost
        Next
        For Each TmpCost As cCost In Budget.MaterialCosts
            _cost += TmpCost.ActualCost
        Next
        For Each TmpCost As cCost In Budget.LogisticsCosts
            _cost += TmpCost.ActualCost
        Next
        For Each TmpCost As cCost In Budget.PlanningCosts
            _cost += TmpCost.ActualCost
        Next
        Return _cost
    End Function

    Public Sub New()
        For Each TmpCat As cStaffCategory In Database.StaffCategories
            With Budget.StaffCosts.Add(TmpCat.Name)
                .Description = TmpCat.Description
                .CostPerHourCTC = TmpCat.CostPerHourCTC
                .CostPerHourActual = TmpCat.CostPerHourActual
            End With
        Next
        For Each TmpCost As cHourCost In Database.PlanningCosts
            Budget.PlanningCosts.Add(TmpCost)
        Next
        For Each TmpCost As cCost In Database.MaterialCosts
            Budget.MaterialCosts.Add(TmpCost)
        Next
        For Each TmpCost As cCost In Database.LogisticsCosts
            Budget.LogisticsCosts.Add(TmpCost)
        Next
    End Sub
End Class
