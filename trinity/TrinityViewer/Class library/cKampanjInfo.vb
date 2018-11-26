Imports System
Imports System.Xml

Namespace TrinityViewer
    Public Class cCampaignInfo

        Private Const MY_VERSION = 41
        Private _version As Integer
        Private _name As String
        Public ID As String
        Private _area As String
        Private _areaLog As String
        Private _updatedTo As Long
        Private _planner As String
        Private _buyer As String
        Private _status As String
        Private _frequencyFocus As String
        Private _fileName As String
        Private _clientID As Integer
        Private _productID As Integer
        Private _marathonCTC As Decimal
        Private _commentary As String
        Private _client As String
        Private _product As String
        Private _startDate As Long
        Private _endDate As Long
        Private _totalTRP As Single
        Private _budgetTotalCTC As Decimal
        Private _actualTotalCTC As Decimal
        Private _confirmedTotalCTC As Decimal
        Private _bookedSpots As cBookedSpotsInfo
        Private _plannedSpots As cPlannedSpotsInfo
        Private _actualSpots As cActualSpotsInfo
        Private _reachPlanned(0 To 9) As Single
        Private _reachActual(0 To 9) As Single
        Private _mainTarget As New TrinityViewer.cTargetInfo(Me) 'sets the main targeted age area
        Private _secondaryTarget As New TrinityViewer.cTargetInfo(Me) 'sets the main targeted age area
        Private _thirdTarget As New TrinityViewer.cTargetInfo(Me) 'sets the main targeted age area
        Private _customTarget As New TrinityViewer.cTargetInfo(Me) 'sets the main targeted age area
        Private _daypartName(0 To 5) As String
        Private _daypartStart(0 To 5) As Short
        Private _daypartEnd(0 To 5) As Short
        Private _notCorrect As Boolean

        Public DaypartCount As Byte
        Public AllAdults As String = "3+"

        Public Adedge As New ConnectWrapper.Brands

        Private _channels As cChannelInfos
        Private _weeks As cWeekInfoSums

        Private GettingSpots As Boolean = False


        Public ReadOnly Property NotCorrect() As Boolean
            Get
                Return _notCorrect
            End Get
        End Property

        Public Property Area() As String
            Get
                Return _area
            End Get
            Set(ByVal value As String)
                _area = value
            End Set
        End Property

        '---------------------------------------------------------------------------------------
        ' Procedure : DaypartName
        ' DateTime  : 2003-09-22 16:25
        ' Author    : joho
        ' Purpose   : Returns/sets the name of a daypart
        '---------------------------------------------------------------------------------------
        '
        Public Property DaypartName(ByVal Daypart As Object) As String
            Get
                On Error GoTo DaypartName_Error
                DaypartName = _daypartName(Daypart)
                On Error GoTo 0
                Exit Property
DaypartName_Error:
                Err.Raise(Err.Number, "cKampanj: DaypartName", Err.Description)
            End Get
            Set(ByVal Value As String)

                On Error GoTo DaypartName_Error
                _daypartName(Daypart) = Value
                On Error GoTo 0
                Exit Property
DaypartName_Error:
                Err.Raise(Err.Number, "cKampanj: DaypartName", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : DaypartStart
        ' DateTime  : 2003-09-22 16:35
        ' Author    : joho
        ' Purpose   : Returns/sets what time a daypart starts
        '---------------------------------------------------------------------------------------
        '
        Public Property DaypartStart(ByVal Daypart As Object) As Short
            Get
                On Error GoTo DaypartStart_Error
                DaypartStart = _daypartStart(Daypart)
                On Error GoTo 0
                Exit Property
DaypartStart_Error:
                Err.Raise(Err.Number, "cKampanj: DaypartStart", Err.Description)
            End Get
            Set(ByVal Value As Short)
                On Error GoTo DaypartStart_Error
                _daypartStart(Daypart) = Value
                On Error GoTo 0
                Exit Property
DaypartStart_Error:
                Err.Raise(Err.Number, "cKampanj: DaypartStart", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : DaypartEnd
        ' DateTime  : 2003-09-22 16:35
        ' Author    : joho
        ' Purpose   : Returns/sets the time when a Dayparts ends
        '---------------------------------------------------------------------------------------
        '
        Public Property DaypartEnd(ByVal Daypart As Object) As Short
            Get
                On Error GoTo DaypartEnd_Error
                DaypartEnd = _daypartEnd(Daypart)
                On Error GoTo 0
                Exit Property
DaypartEnd_Error:
                Err.Raise(Err.Number, "cKampanj: DaypartEnd", Err.Description)
            End Get
            Set(ByVal Value As Short)
                On Error GoTo DaypartEnd_Error
                _daypartEnd(Daypart) = Value
                On Error GoTo 0
                Exit Property
DaypartEnd_Error:
                Err.Raise(Err.Number, "cKampanj: DaypartEnd", Err.Description)
            End Set
        End Property

        Public Property ReachPlanned(ByVal freq As Integer) As Single
            Get
                Return _reachPlanned(freq - 1)
            End Get
            Set(ByVal value As Single)
                _reachPlanned(freq - 1) = value
            End Set
        End Property

        Public Property ReachActual(ByVal freq As Integer) As Single
            Get
                Return _reachActual(freq - 1)
            End Get
            Set(ByVal value As Single)
                _reachActual(freq - 1) = value
            End Set
        End Property

        Public ReadOnly Property BookedSpots() As cBookedSpotsInfo
            Get
                Return _bookedSpots
            End Get
        End Property

        Public ReadOnly Property PlannedSpots() As cPlannedSpotsInfo
            Get
                Return _plannedSpots
            End Get
        End Property

        Public ReadOnly Property ActualSpots() As cActualSpotsInfo
            Get
                Return _actualSpots
            End Get
        End Property

        Public ReadOnly Property FileName() As String
            Get
                Return _fileName
            End Get
        End Property

        Public ReadOnly Property Weeks() As cWeekInfoSums
            Get
                Return _weeks
            End Get
        End Property

        Public ReadOnly Property Channels() As cChannelInfos
            Get
                Return _channels
            End Get
        End Property

        Public Property TotalTRP() As Single
            Get
                Return _totalTRP
            End Get
            Set(ByVal value As Single)
                _totalTRP = value
            End Set
        End Property

        Public Property FrequencyFocus() As Integer
            Get
                Return _frequencyFocus + 1
            End Get
            Set(ByVal value As Integer)
                _frequencyFocus = value - 1
            End Set
        End Property

        Public Property ConfirmedTotalCTC() As Decimal
            Get
                Return _confirmedTotalCTC
            End Get
            Set(ByVal value As Decimal)
                _confirmedTotalCTC = value
            End Set
        End Property

        Public Property BudgetTotalCTC() As Decimal
            Get
                Return _budgetTotalCTC
            End Get
            Set(ByVal value As Decimal)
                _budgetTotalCTC = value
            End Set
        End Property

        'Public Property StrippedCampaign() As Trinity.cKampanj
        '    Get
        '        Return _strippedCampaign
        '    End Get
        '    Set(ByVal value As Trinity.cKampanj)
        '        _strippedCampaign = value
        '    End Set
        'End Property

        Property UpdatedTo() As Date
            Get
                Return Date.FromOADate(_updatedTo)
            End Get
            Set(ByVal value As Date)
                _updatedTo = value.ToOADate
            End Set
        End Property

        Public Function LoadCampaignInfo(ByVal Campaign As String, Optional ByVal LoadDetails As Boolean = False) As Boolean
            Dim XMLDoc As Xml.XmlDocument = New Xml.XmlDocument
            Dim XMLCamp As Xml.XmlElement
            Dim XMLBookedSpots As Xml.XmlElement
            Dim XMLPlannedSpots As Xml.XmlElement
            Dim XMLActualSpots As Xml.XmlElement

            Dim XMLTmpNode As Xml.XmlElement

            Dim TmpString As String
            Dim TmpChannel As cChannelInfo
            Dim TmpBookingtype As cBookingTypeInfo
            Dim TmpWeek As cWeekInfo
            Dim TmpFilm As cFilmInfo
            Dim TmpBookedSpot As cBookedSpotInfo
            Dim TmpPlannedSpot As cPlannedSpotInfo
            Dim TmpActualSpot As cActualSpotInfo
            Dim NotCorrect As Boolean = False

            Dim i As Integer

            'Dim XMLTmpNode As Xml.XmlElement

            XMLDoc.LoadXml(Campaign)

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")

            XMLCamp = XMLDoc.GetElementsByTagName("Campaign").Item(0)


            _name = XMLCamp.GetAttribute("Name")
            ID = XMLCamp.GetAttribute("ID")
            _version = XMLCamp.GetAttribute("Version")
            If _version < MY_VERSION Then
                NotCorrect = True
            End If
            _updatedTo = XMLCamp.GetAttribute("UpdatedTo")
            _planner = XMLCamp.GetAttribute("Planner")
            _buyer = XMLCamp.GetAttribute("Buyer")
            _area = XMLCamp.GetAttribute("Area")
            _areaLog = XMLCamp.GetAttribute("AreaLog")

            XMLTmpNode = XMLCamp.GetElementsByTagName("Dayparts").Item(0).FirstChild
            i = 0
            While Not XMLTmpNode Is Nothing

                _daypartName(i) = XMLTmpNode.LocalName
                _daypartStart(i) = XMLTmpNode.GetAttribute("Start")
                _daypartEnd(i) = XMLTmpNode.GetAttribute("End")
                XMLTmpNode = XMLTmpNode.NextSibling
                i += 1
            End While
            DaypartCount = i

            If XMLCamp.GetAttribute("Cancelled") Is Nothing OrElse XMLCamp.GetAttribute("Cancelled") = "" Then
                _status = XMLCamp.GetAttribute("Status")
            Else
                If XMLCamp.GetAttribute("Cancelled") Then
                    _status = "Cancelled"
                End If
            End If
            _frequencyFocus = XMLCamp.GetAttribute("FrequencyFocus")
            If Not XMLCamp.GetAttribute("ProductID") = "" Then
                ProductID = XMLCamp.GetAttribute("ProductID")
            Else
                ProductID = 0
            End If
            If Not XMLCamp.GetAttribute("ClientID") = "" Then
                ClientID = XMLCamp.GetAttribute("ClientID")
            Else
                ClientID = 0
            End If

            _budgetTotalCTC = XMLCamp.GetAttribute("BudgetTotalCTC")
            _actualTotalCTC = XMLCamp.GetAttribute("ActualTotCTC")
            If Not XMLCamp.GetAttribute("TotalTRP") = "" Then _totalTRP = XMLCamp.GetAttribute("TotalTRP")
            If Not XMLCamp.GetAttribute("MarathonCTC") Is Nothing Then _marathonCTC = Val(XMLCamp.GetAttribute("MarathonCTC"))
            _commentary = XMLCamp.GetAttribute("Commentary")

            Dim XMLTarget As Xml.XmlElement = XMLCamp.SelectSingleNode("MainTarget")
            If XMLTarget.GetAttribute("Type") <> "" AndAlso XMLTarget.GetAttribute("Type") > 0 Then
                Adedge.clearTargetSelection()
                On Error Resume Next
                Adedge.setTargetUserDefined(XMLTarget.GetAttribute("TargetGroup"), XMLTarget.GetAttribute("Name"), True)
                If Err.Number <> 0 Then
                    'frmFindTarget.ShowDialog("The target '" & XMLTarget.GetAttribute("Name") & "' does not exist. Please choose replacement.")
                    '_mainTarget.TargetGroup = frmFindTarget.tvwTargets.SelectedNode.Tag
                    '_mainTarget.TargetName = frmFindTarget.tvwTargets.SelectedNode.Text
                Else
                    _mainTarget.TargetName = XMLTarget.GetAttribute("Name")
                    _mainTarget.TargetGroup = XMLTarget.GetAttribute("TargetGroup")
                End If
            Else
                _mainTarget.TargetName = XMLTarget.GetAttribute("Name")
                _mainTarget.TargetGroup = XMLTarget.GetAttribute("TargetGroup")
            End If
            _mainTarget.TargetType = XMLTarget.GetAttribute("Type")
            _mainTarget.Universe = XMLTarget.GetAttribute("Universe")
            _mainTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")

            XMLTarget = XMLCamp.SelectSingleNode("SecondaryTarget")
            If XMLTarget.GetAttribute("Type") <> "" AndAlso XMLTarget.GetAttribute("Type") > 0 Then
                Adedge.clearTargetSelection()
                On Error Resume Next
                Adedge.setTargetUserDefined(XMLTarget.GetAttribute("TargetGroup"), XMLTarget.GetAttribute("Name"), True)
                If Err.Number <> 0 Then
                    'frmFindTarget.ShowDialog("The target '" & XMLTarget.GetAttribute("Name") & "' does not exist. Please choose replacement.")
                    '_secondaryTarget.TargetGroup = frmFindTarget.tvwTargets.SelectedNode.Tag
                    '_secondaryTarget.TargetName = frmFindTarget.tvwTargets.SelectedNode.Text
                Else
                    _secondaryTarget.TargetName = XMLTarget.GetAttribute("Name")
                    _secondaryTarget.TargetGroup = XMLTarget.GetAttribute("TargetGroup")
                End If
            Else
                _secondaryTarget.TargetGroup = XMLTarget.GetAttribute("TargetGroup")
                _secondaryTarget.TargetName = XMLTarget.GetAttribute("Name")
            End If
            _secondaryTarget.TargetType = XMLTarget.GetAttribute("Type")
            _secondaryTarget.Universe = XMLTarget.GetAttribute("Universe")
            _secondaryTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")

            If Not XMLCamp.GetElementsByTagName("ReachMain") Is Nothing AndAlso XMLCamp.GetElementsByTagName("ReachMain").Count > 0 Then
                XMLTmpNode = XMLCamp.GetElementsByTagName("ReachMain").Item(0).FirstChild
                While Not XMLTmpNode Is Nothing
                    ReachPlanned(XMLTmpNode.GetAttribute("Freq")) = XMLTmpNode.GetAttribute("Reach")
                    XMLTmpNode = XMLTmpNode.NextSibling
                End While
            End If

            If Not XMLCamp.GetElementsByTagName("ActualReach") Is Nothing AndAlso XMLCamp.GetElementsByTagName("ActualReach").Count > 0 Then
                XMLTmpNode = XMLCamp.GetElementsByTagName("ActualReach").Item(0).FirstChild
                While Not XMLTmpNode Is Nothing
                    ReachActual(XMLTmpNode.GetAttribute("Freq")) = XMLTmpNode.GetAttribute("Reach")
                    XMLTmpNode = XMLTmpNode.NextSibling
                End While
            End If

            If XMLCamp.GetAttribute("StartDate") Is Nothing OrElse XMLCamp.GetAttribute("StartDate") = "" Then
                Dim XMLChannel As Xml.XmlElement
                Dim XMLBookingType As Xml.XmlElement
                Dim XMLWeek As Xml.XmlElement
                Dim XMLFilm As Xml.XmlElement

                XMLChannel = XMLCamp.SelectSingleNode("Channels").FirstChild
                XMLBookingType = XMLChannel.GetElementsByTagName("BookingTypes").Item(0).FirstChild
                XMLWeek = XMLBookingType.GetElementsByTagName("Weeks").Item(0).FirstChild

                _startDate = XMLWeek.GetAttribute("StartDate")

                While Not XMLWeek Is Nothing
                    If XMLWeek.GetAttribute("EndDate") > _endDate Then
                        _endDate = XMLWeek.GetAttribute("EndDate")
                    End If
                    XMLWeek = XMLWeek.NextSibling
                End While

                'Read Channels
                XMLChannel = XMLCamp.SelectSingleNode("Channels").FirstChild
                While Not XMLChannel Is Nothing
                    TmpString = XMLChannel.GetAttribute("Name")
                    TmpChannel = _channels.Add(TmpString)
                    TmpChannel = _channels(TmpString)
                    TmpChannel.AdedgeNames = XMLChannel.GetAttribute("AdEdgeNames")
                    TmpChannel.ShortName = XMLChannel.GetAttribute("Shortname")
                    XMLTarget = XMLChannel.GetElementsByTagName("MainTarget").Item(0)
                    TmpChannel.MainTarget.TargetName = XMLTarget.GetAttribute("Name")
                    TmpChannel.MainTarget.TargetType = Val(XMLTarget.GetAttribute("Type"))
                    TmpChannel.MainTarget.Universe = XMLTarget.GetAttribute("Universe")
                    TmpChannel.MainTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")

                    'read Bookingtypes
                    XMLBookingType = XMLChannel.GetElementsByTagName("BookingTypes").Item(0).FirstChild
                    While Not XMLBookingType Is Nothing
                        Dim XMLBuyTarget As Xml.XmlElement

                        'If XMLBookingType.GetAttribute("BookIt") Then
                        TmpString = XMLBookingType.GetAttribute("Name")
                        TmpBookingtype = TmpChannel.BookingTypes.Add(TmpString)
                        XMLTmpNode = XMLBookingType.GetElementsByTagName("DaypartSplit").Item(0)
                        For i = 0 To DaypartCount - 1
                            Dim XMLTmpNode2 As Xml.XmlElement = XMLTmpNode.GetElementsByTagName(_daypartName(i)).Item(0)
                            TmpBookingtype.DaypartSplit(i) = XMLTmpNode2.GetAttribute("Share")
                        Next

                        TmpBookingtype.ShortName = XMLBookingType.GetAttribute("Shortname")
                        TmpBookingtype.BookIt = XMLBookingType.GetAttribute("BookIt")
                        TmpBookingtype.ConfirmedNetBudget = XMLBookingType.GetAttribute("ConfirmedNetBudget")
                        TmpBookingtype.IndexMainTarget = XMLBookingType.GetAttribute("IndexMainTarget")
                        TmpBookingtype.IndexAllAdults = XMLBookingType.GetAttribute("IndexAllAdults")

                        XMLBuyTarget = XMLBookingType.SelectSingleNode("BuyingTarget")
                        XMLTarget = XMLBuyTarget.GetElementsByTagName("Target").Item(0)

                        TmpBookingtype.BuyingTarget.TargetName = XMLTarget.GetAttribute("Name")
                        TmpBookingtype.BuyingTarget.TargetType = XMLTarget.GetAttribute("Type")
                        TmpBookingtype.BuyingTarget.Universe = XMLTarget.GetAttribute("Universe")
                        TmpBookingtype.BuyingTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")

                        XMLWeek = XMLBookingType.GetElementsByTagName("Weeks").Item(0).FirstChild

                        While Not XMLWeek Is Nothing
                            TmpString = XMLWeek.GetAttribute("Name")
                            TmpWeek = TmpBookingtype.Weeks.Add(TmpString)
                            TmpWeek.TRP = XMLWeek.GetAttribute("TRP")
                            TmpWeek.StartDate = Date.FromOADate(XMLWeek.GetAttribute("StartDate"))
                            TmpWeek.EndDate = Date.FromOADate(XMLWeek.GetAttribute("EndDate"))
                            If Not XMLWeek.GetAttribute("GrossBudget") = "" Then
                                TmpWeek.GrossBudget = XMLWeek.GetAttribute("GrossBudget")
                            Else
                                If Not XMLWeek.GetAttribute("Discount") = 1 Then
                                    TmpWeek.GrossBudget = XMLWeek.GetAttribute("NetBudget") / (1 - XMLWeek.GetAttribute("Discount"))
                                End If
                                NotCorrect = True
                            End If
                            TmpWeek.NetBudget = XMLWeek.GetAttribute("NetBudget")
                            If Not XMLWeek.GetAttribute("NetCPP30") = "" Then
                                TmpWeek.NetCPP30 = XMLWeek.GetAttribute("NetCPP30")
                            Else
                                NotCorrect = True
                            End If
                            TmpWeek.TRPBuyingTarget = XMLWeek.GetAttribute("TRPBuyingTarget")
                            XMLFilm = XMLBookingType.GetElementsByTagName("Films").Item(0).FirstChild
                            While Not XMLFilm Is Nothing
                                TmpFilm = TmpWeek.Films.Add(XMLFilm.GetAttribute("Name"))
                                TmpFilm.Filmcode = XMLFilm.GetAttribute("Filmcode")
                                TmpFilm.Share = XMLFilm.GetAttribute("Share")
                                TmpFilm.Index = XMLFilm.GetAttribute("Index")
                                XMLFilm = XMLFilm.NextSibling
                            End While
                            XMLWeek = XMLWeek.NextSibling
                        End While
                        'End If
                        XMLBookingType = XMLBookingType.NextSibling
                    End While
                    If TmpChannel.BookingTypes.Count = 0 Then _channels.Remove(TmpChannel.ChannelName)
                    XMLChannel = XMLChannel.NextSibling
                End While

                XMLBookedSpots = XMLCamp.SelectSingleNode("BookedSpots")
                If LoadDetails Then
                    XMLTmpNode = XMLBookedSpots.FirstChild
                    While XMLTmpNode IsNot Nothing
                        TmpBookedSpot = _bookedSpots.Add
                        TmpBookedSpot.AirDate = XMLTmpNode.GetAttribute("AirDate")
                        TmpBookedSpot.MaM = XMLTmpNode.GetAttribute("MaM")
                        TmpBookedSpot.MyEstimate = XMLTmpNode.GetAttribute("MyEstimate")
                        TmpBookedSpot.MyEstimateBuyTarget = XMLTmpNode.GetAttribute("MyEstimateBuyTarget")
                        TmpBookedSpot.PriceGross = XMLTmpNode.GetAttribute("GrossPrice")
                        TmpBookedSpot.PriceNet = XMLTmpNode.GetAttribute("NetPrice")
                        TmpBookedSpot.Channel = _channels(XMLTmpNode.GetAttribute("Channel"))
                        TmpBookedSpot.Bookingtype = TmpBookedSpot.Channel.BookingTypes(XMLTmpNode.GetAttribute("Bookingtype"))
                        TmpBookedSpot.Week = TmpBookedSpot.Bookingtype.GetWeek(TmpBookedSpot.AirDate)
                        TmpBookedSpot.Filmcode = XMLTmpNode.GetAttribute("Filmcode")
                        XMLTmpNode = XMLTmpNode.NextSibling
                    End While
                    XMLPlannedSpots = XMLCamp.SelectSingleNode("PlannedSpots")
                    XMLTmpNode = XMLPlannedSpots.FirstChild
                    While XMLTmpNode IsNot Nothing
                        TmpPlannedSpot = _plannedSpots.Add
                        TmpPlannedSpot.AirDate = XMLTmpNode.GetAttribute("AirDate")
                        TmpPlannedSpot.MaM = XMLTmpNode.GetAttribute("MaM")
                        TmpPlannedSpot.ChannelRating(cPlannedSpotInfo.PlannedTargetEnum.pteBuyingTarget) = XMLTmpNode.GetAttribute("RatingBuyTarget")
                        TmpPlannedSpot.MyRating = XMLTmpNode.GetAttribute("MyRating")
                        TmpPlannedSpot.PriceGross = XMLTmpNode.GetAttribute("PriceGross")
                        TmpPlannedSpot.PriceNet = XMLTmpNode.GetAttribute("PriceNet")
                        TmpPlannedSpot.Channel = _channels(XMLTmpNode.GetAttribute("Channel"))
                        TmpPlannedSpot.Bookingtype = TmpPlannedSpot.Channel.BookingTypes(XMLTmpNode.GetAttribute("BookingType"))
                        TmpPlannedSpot.Week = TmpPlannedSpot.Bookingtype.GetWeek(Date.FromOADate(TmpPlannedSpot.AirDate))
                        TmpPlannedSpot.Filmcode = XMLTmpNode.GetAttribute("Filmcode")
                        XMLTmpNode = XMLTmpNode.NextSibling
                    End While
                    XMLActualSpots = XMLCamp.SelectSingleNode("ActualSpots")
                    XMLTmpNode = XMLActualSpots.FirstChild
                    While XMLTmpNode IsNot Nothing
                        If XMLTmpNode.GetAttribute("Second") = "" OrElse XMLTmpNode.GetAttribute("Second") > 60 Then
                            TmpActualSpot = _actualSpots.Add(Date.FromOADate(XMLTmpNode.GetAttribute("AirDate")), XMLTmpNode.GetAttribute("MaM"), 0)
                        Else
                            TmpActualSpot = _actualSpots.Add(Date.FromOADate(XMLTmpNode.GetAttribute("AirDate")), XMLTmpNode.GetAttribute("MaM"), XMLTmpNode.GetAttribute("Second"))
                        End If
                        TmpActualSpot.Channel = _channels(XMLTmpNode.GetAttribute("Channel"))
                        TmpActualSpot.Bookingtype = TmpActualSpot.Channel.BookingTypes(XMLTmpNode.GetAttribute("BookingType"))
                        TmpActualSpot.Week = TmpActualSpot.Bookingtype.GetWeek(Date.FromOADate(TmpActualSpot.AirDate))
                        TmpActualSpot.Filmcode = XMLTmpNode.GetAttribute("Filmcode")
                        TmpActualSpot.AdEdgeChannel = XMLTmpNode.GetAttribute("AdEdgeChannel")
                        TmpActualSpot.BreakType = XMLTmpNode.GetAttribute("BreakType")
                        TmpActualSpot.PosInBreak = XMLTmpNode.GetAttribute("PosInBreak")
                        TmpActualSpot.SpotLength = XMLTmpNode.GetAttribute("SpotLength")
                        TmpActualSpot.SpotsInBreak = XMLTmpNode.GetAttribute("SpotsInBreak")
                        If Not XMLTmpNode.GetAttribute("RatingMain") = "" Then
                            TmpActualSpot.Channel.ActualTRP += XMLTmpNode.GetAttribute("RatingMain")
                        Else
                            NotCorrect = True
                        End If
                        XMLTmpNode = XMLTmpNode.NextSibling
                    End While
                End If
                If XMLBookedSpots.GetAttribute("TotalTRP") <> "" Then
                    _bookedSpots.TotalTRP = XMLBookedSpots.GetAttribute("TotalTRP")
                    _bookedSpots.TotalNet = XMLBookedSpots.GetAttribute("TotalNet")
                    _bookedSpots.TotalGross = XMLBookedSpots.GetAttribute("TotalGross")
                    XMLPlannedSpots = XMLCamp.SelectSingleNode("PlannedSpots")
                    _plannedSpots.TotalTRP = XMLPlannedSpots.GetAttribute("TotalTRP")
                    _plannedSpots.TRPToDeliver = XMLPlannedSpots.GetAttribute("TRPToDeliver")

                    XMLActualSpots = XMLCamp.SelectSingleNode("ActualSpots")
                    _actualSpots.TotalTRP = XMLActualSpots.GetAttribute("TotalTRP")
                Else
                    'Clear()
                    'Campaign = New Trinity.cKampanj
                    'TrinitySettings.MainObject = Campaign
                    'TrinityViewer.Helper.MainObject = Campaign
                    'Campaign.LoadCampaign(Path)
                    'Campaign.SaveCampaign(Path)
                    'LoadCampaignInfo(Path)
                    'Exit Sub
                End If
            Else
                _startDate = XMLCamp.GetAttribute("StartDate")
                _endDate = XMLCamp.GetAttribute("EndDate")
            End If
            'If _endDate <> _updatedTo Then Stop
            Adedge.setArea(_area)
            On Error Resume Next
            Adedge.setBrandFilmCode(_areaLog, "Ö")
            Adedge.setPeriod(Format(_startDate, "ddMMyy") & "-" & Format(_endDate, "ddMMyy"))
            Adedge.setChannels("TV3 se")
            _notCorrect = NotCorrect
            Return Not NotCorrect
        End Function

        Public Property MainTarget() As TrinityViewer.cTargetInfo
            Get
                On Error GoTo MainTarget_Error
                MainTarget = _mainTarget
                On Error GoTo 0
                Exit Property
MainTarget_Error:
                Err.Raise(Err.Number, "cKampanj: MainTarget", Err.Description)
            End Get
            Set(ByVal Value As TrinityViewer.cTargetInfo)
                On Error GoTo MainTarget_Error
                _mainTarget = Value
                On Error GoTo 0
                Exit Property
MainTarget_Error:
                Err.Raise(Err.Number, "cKampanj: MainTarget", Err.Description)
            End Set
        End Property

        Public Property CustomTarget() As TrinityViewer.cTargetInfo
            Get
                On Error GoTo customTarget_Error
                CustomTarget = _customTarget
                On Error GoTo 0
                Exit Property
customTarget_Error:
                Err.Raise(Err.Number, "cKampanj: customTarget", Err.Description)
            End Get
            Set(ByVal Value As TrinityViewer.cTargetInfo)
                On Error GoTo customTarget_Error
                _customTarget = Value
                On Error GoTo 0
                Exit Property
customTarget_Error:
                Err.Raise(Err.Number, "cKampanj: customTarget", Err.Description)
            End Set
        End Property

        Public Property SecondaryTarget() As TrinityViewer.cTargetInfo
            Get
                On Error GoTo SecondaryTarget_Error
                SecondaryTarget = _secondaryTarget
                On Error GoTo 0
                Exit Property
SecondaryTarget_Error:
                Err.Raise(Err.Number, "cKampanj: SecondaryTarget", Err.Description)
            End Get
            Set(ByVal Value As TrinityViewer.cTargetInfo)
                On Error GoTo SecondaryTarget_Error
                _secondaryTarget = Value
                On Error GoTo 0
                Exit Property
SecondaryTarget_Error:
                Err.Raise(Err.Number, "cKampanj: SecondaryTarget", Err.Description)
            End Set
        End Property

        Public Property ThirdTarget() As TrinityViewer.cTargetInfo
            Get
                On Error GoTo ThirdTarget_Error
                ThirdTarget = _thirdTarget
                On Error GoTo 0
                Exit Property
ThirdTarget_Error:
                Err.Raise(Err.Number, "cKampanj: ThirdTarget", Err.Description)
            End Get
            Set(ByVal Value As TrinityViewer.cTargetInfo)
                On Error GoTo ThirdTarget_Error
                _thirdTarget = Value
                On Error GoTo 0
                Exit Property
ThirdTarget_Error:
                Err.Raise(Err.Number, "cKampanj: ThirdTarget", Err.Description)
            End Set
        End Property

        '---------------------------------------------------------------------------------------
        ' Procedure : ClientID
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        Public Property ClientID() As Short
            Get
                ClientID = _clientID
            End Get
            Set(ByVal Value As Short)
                _clientID = Value
                If Value = 0 Then
                    _client = ""
                Else
                    _client = "" 'DBReader.getClient(_clientID)
                End If
            End Set
        End Property

        Public Property Client() As String
            Get
                Return _client
            End Get
            Set(ByVal value As String)
                _client = value
            End Set
        End Property

        Public ReadOnly Property Product() As String
            Get
                Return _product
            End Get
        End Property

        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Public Property StartDate() As Date
            Get
                Return Date.FromOADate(_startDate)
            End Get
            Set(ByVal value As Date)
                _startDate = value.ToOADate
            End Set
        End Property

        Public Property EndDate() As Date
            Get
                Return Date.FromOADate(_endDate)
            End Get
            Set(ByVal value As Date)
                _endDate = value.ToOADate
            End Set
        End Property

        '---------------------------------------------------------------------------------------
        ' Procedure : ProductID
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        Public Property ProductID() As Short
            Get
                ProductID = _productID
            End Get
            Set(ByVal Value As Short)
                _productID = Value
                If Value = 0 Then
                    _product = ""
                Else
                    _product = ""
                End If
            End Set
        End Property


        Public Sub New()
            _channels = New cChannelInfos(Me)
            _weeks = New cWeekInfoSums(Me)
            _bookedSpots = New cBookedSpotsInfo
            _plannedSpots = New cPlannedSpotsInfo
            _actualSpots = New cActualSpotsInfo(Me)
        End Sub

        Sub Clear()
            _channels = New cChannelInfos(Me)
            _weeks = New cWeekInfoSums(Me)
            _bookedSpots = New cBookedSpotsInfo
            _plannedSpots = New cPlannedSpotsInfo
            _actualSpots = New cActualSpotsInfo(Me)
        End Sub

        Function CalculateSpots(Optional ByVal CalculateReach As Boolean = True, Optional ByVal FromDate As Date = Nothing, Optional ByVal ToDate As Date = Nothing, Optional ByVal FreqFocus As Byte = 1, Optional ByVal Chan As cChannelInfo = Nothing, Optional ByVal OnlyWeek As String = "", Optional ByVal Bookingtype As cBookingTypeInfo = Nothing, Optional ByVal Film As cFilmInfo = Nothing, Optional ByVal Daypart As Integer = -1) As Single
            If GettingSpots Then Return 0
            GettingSpots = True

            Dim s As Integer
            Dim ContinueThis() As Boolean
            Dim Cont As Boolean
            Dim q As Integer
            Dim LowBound As Integer
            Dim SpotCount As Integer

            'if there are no ActualSpots we cant make any calculations
            If _actualSpots.Count = 0 Then Exit Function

            'sets the boolean array length to match the number of ActualSpots
            ReDim ContinueThis(_actualSpots.Count)

            Adedge.clearGroup()
            For s = 1 To _actualSpots.Count
                'sets the Array(s) to False
                ContinueThis(s) = False
                'if we are using filters 
                Cont = False
                If Chan Is Nothing Then
                    Cont = True
                Else
                    If _actualSpots(s).Channel Is Chan Then
                        Cont = True
                    End If
                End If
                If Cont Then
                    Cont = False
                    If Bookingtype Is Nothing Then
                        Cont = True
                    Else
                        If _actualSpots(s).Bookingtype Is Bookingtype Then
                            Cont = True
                        End If
                    End If
                    If Cont Then
                        Cont = False
                        If OnlyWeek = "" Then
                            Cont = True
                        Else
                            If _actualSpots(s).Week.Name = OnlyWeek Then
                                Cont = True
                            End If
                        End If
                        If Cont Then
                            Cont = False
                            If Film Is Nothing Then
                                Cont = True
                            Else
                                If Not _actualSpots(s).Week.Films(_actualSpots(s).Filmcode) Is Nothing AndAlso _actualSpots(s).Week.Films(Film.Name).Filmcode = _actualSpots(s).Week.Films(_actualSpots(s).Filmcode).Filmcode Then
                                    Cont = True
                                End If
                            End If
                            If Cont Then
                                Cont = False
                                If Daypart = -1 Then
                                    Cont = True
                                Else
                                    If Daypart = TrinityViewer.Helper.GetDaypart(_actualSpots(s).MaM) Then
                                        Cont = True
                                    End If
                                End If
                                If Cont Then
                                    Cont = False
                                    If FromDate = Nothing Then
                                        Cont = True
                                    Else
                                        If FromDate.ToOADate <= _actualSpots(s).AirDate Then
                                            Cont = True
                                        End If
                                    End If
                                    If Cont Then
                                        Cont = False
                                        If ToDate = Nothing Then
                                            Cont = True
                                        Else
                                            If ToDate.ToOADate >= _actualSpots(s).AirDate Then
                                                Cont = True
                                            End If
                                        End If
                                        If Cont Then
                                            ContinueThis(s) = True
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                'If either the Filter sections or the non filter section has set ContinueThis = true we continue here
                If ContinueThis(s) Then
                    q = 0
                    If InStr(UCase(_actualSpots(s).Channel.AdedgeNames) & ",", UCase(Adedge.getAttrib(Connect.eAttribs.aChannel, s - 1)) & ",") = 0 Then
                        LowBound = -5
                        If s + LowBound - 1 < 0 Then
                            LowBound = -s + 1
                        End If
                        For q = LowBound To 5
                            '                    If s + q - 1 < 0 Then
                            '                        q = q - (s + q) + 1
                            '                    End If
                            If _actualSpots(s).MaM = Adedge.getAttrib(Connect.eAttribs.aFromTime, s + q - 1) \ 60 Then
                                If InStr(UCase(_actualSpots(s).Channel.AdedgeNames) & ",", UCase(Adedge.getAttrib(Connect.eAttribs.aChannel, s + q - 1)) & ",") > 0 Then
                                    If _actualSpots(s).AirDate = Adedge.getAttrib(Connect.eAttribs.aDate, s + q - 1) Then
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                    End If
                    Adedge.deleteFromGroup(s + q - 1, False)
                    On Error GoTo ErrHandle
                    _actualSpots(s).GroupIdx = Adedge.addToGroup(s + q - 1) - 1


                Else 'If ContinueThis was false we go from here
                    q = 0
                    If InStr(UCase(_actualSpots(s).Channel.AdedgeNames), UCase(Adedge.getAttrib(Connect.eAttribs.aChannel, s - 1))) = 0 Then
                        LowBound = -5
                        'for the 5 first times through the loop LowBound is is 1-s (1- TimesThroughLoop) after that it will be -5
                        If s + LowBound - 1 < 0 Then
                            LowBound = 1 - s
                        End If
                        '                   s =   1, 2, 3, 4, 5, 6, 7, 8, 9,10,*
                        '           LowBound =    0,-1,-2,-3,-4,-5,-5,-5,-5,-5,*
                        'this code will be looped 5, 6, 7, 8, 9,10,10,10,10,10,*
                        '                   q =   0,-1,-2,-3,-4,-5,-5,-5,-5,-5,*
                        For q = LowBound To 5
                            'if we have been through all the ActualSpots we exit the loop
                            If s + q - 1 >= _actualSpots.Count Then
                                '??????????????????????????????????????????????????????????????????????????????????????????
                                'Debug.Assert(False)
                                Exit For
                            End If

                            If _actualSpots(s).MaM = Adedge.getAttrib(Connect.eAttribs.aFromTime, s + q - 1) \ 60 Then
                                If InStr(UCase(_actualSpots(s).Channel.AdedgeNames), UCase(Adedge.getAttrib(Connect.eAttribs.aChannel, s + q - 1))) > 0 Then
                                    If _actualSpots(s).AirDate = Adedge.getAttrib(Connect.eAttribs.aDate, s + q - 1) Then
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                    End If
                    Adedge.deleteFromGroup(s + q - 1, False)
                End If
                'end of the Loop For s = 1 To _actualspots.Count
            Next

            If CalculateReach Then
                'Calculate the reach
                SpotCount = Adedge.recalcRF(Connect.eSumModes.smGroup)
            End If
            GettingSpots = False

            Exit Function

ErrHandle:
            GettingSpots = False
        End Function

        Sub CreateAdedgeSpots()
            If GettingSpots Then Exit Sub
            GettingSpots = True

            Dim TmpSpot As cActualSpotInfo
            Dim SpotCount As Long

            Adedge.clearList()
            Adedge.clearBrandFilter()
            Adedge.clearGroup()
            Adedge.setArea(_area)
            Adedge.setPeriod(Format(StartDate, "ddMMyy") & "-" & Format(EndDate, "ddMMyy"))
            Adedge.setChannels(ChannelString)
            TrinityViewer.Helper.AddTargetsToAdedge(Adedge, True)
            Adedge.setUniverseUserDefined(UniStr)
            Adedge.sort("")

            For Each TmpSpot In _actualSpots
                SpotCount = Adedge.addBrand(Format(Date.FromOADate(TmpSpot.AirDate), "ddMMyy"), TrinityViewer.Helper.Mam2Tid(TmpSpot.MaM) & ":" & Format(TmpSpot.Second, "00"), TmpSpot.AdEdgeChannel, TmpSpot.SpotLength)
            Next
            If SpotCount > 0 Then
                SpotCount = Adedge.Run(False, False, 0)
            End If
            Adedge.sort("date(asc),fromtime(asc)")
            GettingSpots = False

        End Sub

        Private Function ChannelString() As String
            Dim TmpChan As cChannelInfo
            Dim TmpBT As cBookingTypeInfo
            Dim IsUsed As Boolean
            Dim Tmpstr As String

            Tmpstr = ""
            For Each TmpChan In _channels
                IsUsed = False
                For Each TmpBT In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        IsUsed = True
                        Exit For
                    End If
                Next TmpBT
                If IsUsed Then
                    Tmpstr = Tmpstr & TmpChan.AdedgeNames & ","
                End If
            Next TmpChan
            Return Tmpstr
        End Function

        Public Function UniStr()
            Dim TmpStr As String = ""

            For i As Integer = 0 To Adedge.getUniverseCount - 1
                TmpStr += Adedge.getUniverseTitle(i) & ","
            Next
            Return TmpStr
        End Function
    End Class

End Namespace
