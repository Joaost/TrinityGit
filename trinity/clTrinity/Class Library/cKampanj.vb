Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.IO
Imports System.Windows.Forms
Imports System.Diagnostics
Imports clTrinity.CultureSafeExcel
Imports System.Xml.Linq

'Imports System.Xml.Linq

Namespace Trinity
    Public Class cKampanj
        Implements IDetectsProblems
        Implements IDisposable

        '---------------------------------------------------------------------------------------
        ' Purpose   : The class holds all possible information about a campaign          
        '---------------------------------------------------------------------------------------



        Public Const MAX_DAYPARTS = 10

        Private Const MY_VERSION As Short = 41

        Private Ini As New clsIni 'Declares a *.INI reader object

        Private mvarArea As String 'a two letter string which define what country a campaign in created in
        Private mvarAreaLog As String 'a additional coutry setting (in case there is different settings in different parts of the country)
        Private mvarActualSpots As New cActualSpots(Me) 'Actual spots hold the spots planned bokked and confirmed
        Private mvarActualTotCTC As Decimal ' actual Cost To CLient
        Private mvarAllAdults As String ' A string with all measurable persons (the whole population)
        Private mvarPlannedSpots As New cPlannedSpots(Me) 'a collection of Planned spots (spots that is booked and confirmed)
        Private mvarBookedSpots As New cBookedSpots(Me) 'a collection of booked spots (they have not been handles by the channel yet)
        Private mvarBudgetTotalCTC As Decimal
        Private mvarBuyer As String 'The buyers name (the media agent)
        Private mvarBuyerEmail As String 'The buyers email (the media agent)
        Private mvarBuyerPhone As String 'The buyers phone (the media agent)
        Private mvarBuyerID As Integer = -1

        Private mvarClient As String 'the client/customer
        Private mvarClientID As Short 'client/customer ID
        Private mvarCommentary As String 'A string with all text writen in the Notepad window
        Private mvarControlFilmcodeFromClient As Boolean ' sets true if film codes has been recieved from client
        Private mvarControlFilmcodeToBureau As Boolean ' sets true if the film is send to the creative bureau
        Private mvarControlFilmcodeToChannels As Boolean ' sets true if the film is send to the channels
        Private mvarControlOKFromClient As Boolean ' Is set true if the client has agreed to the campaign
        Private mvarControlTracking As Boolean
        Private mvarControlFollowedUp As Boolean ' true if the campaign has been followed up
        Private mvarControlFollowUpToClient As Boolean ' True if the follow up has been send to the customer
        Private mvarControlTransferredToMatrix As Boolean
        Private mvarCosts As New cCosts(Me) 'a collection of costs
        Private mvarContract As cContract 'a contract (cheaper prices)
        Private mvarContractID As Long = 0 'The database ID, if any, of the contract being used
        Private mvarDatabaseID As Long = 0
        Private mvarFilmindexAsDiscount As Boolean = False
        Private mvarExtranetDatabaseID As Integer
        Private mvarMatrixID As String = Nothing

        'Public DaypartCount As Byte
        'Private mvarDaypartName(5) As String
        'Private mvarDaypartStart(5) As Short
        'Private mvarDaypartEnd(5) As Short
        Private mvarDebugPath As String
        Private mvarFilename As String
        Private mvarFrequencyFocus As Byte
        Private mvarName As String
        Private mvarVersion As Byte
        Private mvarPlanner As String
        Private mvarPlannerEmail As String
        Private mvarPlannerPhone As String
        Private mvarPlannerID As Integer = -1
        Private mvarUpdatedTo As Integer
        Private mvarAdtoox As cAdtoox
        Private mvarCampaignAreas As List(Of dbUA)
        Private mvarCampaignArea As String = ""

        Dim newSpots As Boolean = True

        Public Event RequestAdtooxStatusUpdate()


        Private WithEvents mvarMainTarget As New cTarget(Me) 'sets the main targeted age area
        Private WithEvents mvarSecondaryTarget As New cTarget(Me) 'sets the secondary targeted age area
        Private WithEvents mvarThirdTarget As New cTarget(Me) 'sets the targeted age area number three
        Private mvarCustomTarget As New cTarget(Me) 'sets a custom target that is not saved but can be used in analysis

        'Private mvarRTColl As New Dictionary(Of String Dictionary(Of Object, Byte))
        Private mvarReachGoal(0 To 10, 0 To 1) As Single

        Private mvarProduct As String 'The campaign product
        Private mvarProductID As Short 'the campaign product ID
        Private mvarAdEdgeProducts As New List(Of String) 'A list of the names of the product in Advantedge

        Private mvarInternalAdedge As ConnectWrapper.Brands

        Private mvarTargColl As Collection
        Private mvarUniColl As Collection
        Private mvarTargStr As String
        Private mvarUniStr As String

        Private mvarMarathonCTC As Single 'holds the cost that is to be charged of the client
        Private mvarMarathonInsertions As System.Xml.XmlNode

        Private mvarStatus As String ' Holds the status of the campaign. "Planned","Running","Finished" or "Cancelled"

        'a collection of strings containing information about the universes applyed
        Private mvarUniverses As New Collections.Specialized.NameValueCollection

        Public ID As String 'Campaign ID


        Public ExtendedInfos As New cWrapper
        Public EstimationPeriods As New cWrapper

        Public RFEstimation As New cReachguide(Me)
        Public Campaigns As Collections.Generic.IDictionary(Of String, Trinity.cKampanj)
        Public History As New Dictionary(Of String, Trinity.cKampanj)
        Public RootCampaign As cKampanj
        Public HistoryComment As String
        Public WasExportedToMarathon As Boolean
        Public BlockCalculateSpots As Boolean = False 'If set, Trinity will not Calculate spots unless argument ForceCalculate is set on call
        Public FilterPlannedSpots As Boolean = True
        Public WebTV As New cOtherMediaType(Me) With {.Name = "Web-TV"}
        Public Cinema As New cOtherMediaType(Me) With {.Name = "Cinema"}

        Private mvarHistoryDate As Date
        Private mvarErrorCheckingEnabled As Boolean = False

        Private mvarChannels As New cChannels(Me) 'a collection of Channels
        Private mvarCombinations As New cCombinations(Me, True) ' a collection of Combinations

        Private mvarAdedge As New ConnectWrapper.Brands
        Public Loading As Boolean
        Public Saving As Boolean
        Public MarathonPlanNr As Integer
        Private mvarMarathonOtherOrder As Integer
        Private _targcoll As New List(Of cTarget)
        Private _unicoll As New List(Of String)
        Private GettingSpots As Boolean = False
        Private _multiplyAddedValues As Boolean = False

        Public Event ReadNewSpot(ByVal SpotNr As Integer, ByVal SpotCount As Integer)
        Public Event CheckForBookedSpotsProblems()

        Private Delegate Sub MainTargetChangedHandler()
        Private Delegate Sub SecondaryTargetChangedHandler()
        Private Delegate Sub ThirdTargetChangedHandler()

        Private Delegate Sub DaypartsChangedHandler()

        'Now we declare the actual delegate objects
        Private MainTargetDelegate As MainTargetChangedHandler
        Private SecondaryTargetDelegate As SecondaryTargetChangedHandler
        Private ThirdTargetDelegate As ThirdTargetChangedHandler

        Private DaypartsDelegate As DaypartsChangedHandler

        Private _estimatedWeeklyReach As New Dictionary(Of String, Single)

        Public fs As FileStream

        Public DefaultSecondUniverse As Object = Nothing

        Public xmlColorSchemes As New cColorSchemes
        Public ChannelBundles As New cChannelBundles(Me)

        Public Event LockChanged(ByVal Username As String)

        Private mvarNewContractVersion As Boolean = False

        Public ReadOnly Property NewContractVersion As Boolean
            Get
                Return mvarNewContractVersion
            End Get
        End Property

        Public Enum AutoLinkCampaignsEnum
            DoNotAutoLink = 0
            LinkToAllCampaigns = 1
            LinkToSameClient = 2
            LinkToSameProduct = 3
        End Enum

        Public Enum TRPTypeEnum
            TRPMain = 0
            TRPSecond = 1
            TRPThird = 2
            TRPAll = 3
        End Enum

        ' TRP Changed event and delegate
        Public Event TRPChanged(ByVal sender As Object, ByVal e As WeekEventArgs)

        Public Sub _trpChanged(ByVal sender As Object, ByVal e As WeekEventArgs)
            RaiseEvent TRPChanged(sender, e)
        End Sub

        Public Event FilmChanged(Film As cFilm)
        Public Event WeekChanged(Week As cWeek)
        Public Event BookingtypeChanged(Bookingtype As cBookingType)
        Public Event ChannelChanged(Channel As cChannel)
        Public Event CampaignChanged(Campaign As cKampanj)
        Public Event DaypartDefinitionsChanged()

        Public Event TimeShiftChanged(newTimeShift As TimeShiftEnum)

        Private Sub _filmChanged(Film As cFilm)
            RaiseEvent FilmChanged(Film)
            RaiseEvent CampaignChanged(Me)

            If mvarErrorCheckingEnabled Then
                Film.DetectAdTooxProblems()
            End If
        End Sub

        Private Sub _weekChanged(Week As cWeek)
            RaiseEvent WeekChanged(Week)
            RaiseEvent CampaignChanged(Me)
        End Sub

        Private Sub _bookingtypeChanged(Bookingtype As cBookingType)
            RaiseEvent BookingtypeChanged(Bookingtype)
            RaiseEvent CampaignChanged(Me)
        End Sub

        Private Sub _channelChanged(Channel As cChannel)
            RaiseEvent ChannelChanged(Channel)
            RaiseEvent CampaignChanged(Me)
        End Sub

        Private Sub _daypartDefinitionChanged()
            RaiseEvent DaypartDefinitionsChanged()
        End Sub

        Private _autoLinkCampaigns As AutoLinkCampaignsEnum
        Public Property AutoLinkCampaigns() As AutoLinkCampaignsEnum
            Get
                Return _autoLinkCampaigns
            End Get
            Set(ByVal value As AutoLinkCampaignsEnum)
                _autoLinkCampaigns = value
            End Set
        End Property

        Private _linkedCampaigns As New List(Of cLinkedCampaign)
        Public Property LinkedCampaigns() As List(Of cLinkedCampaign)
            Get
                Return _linkedCampaigns
            End Get
            Set(ByVal value As List(Of cLinkedCampaign))
                _linkedCampaigns = value
            End Set
        End Property

        Private _isStripped As Boolean = False
        ''' <summary>
        ''' Gets or sets a value indicating whether this campign has been stripped of unused channels.
        ''' </summary>
        ''' <value>
        ''' <c>true</c> if this instance is stripped; otherwise, <c>false</c>.
        ''' </value>
        Public Property IsStripped() As Boolean
            Get
                Return _isStripped
            End Get
            Private Set(ByVal value As Boolean)
                _isStripped = value
            End Set
        End Property
        Private _PrintPlannedNet As Boolean
        Public Property PrintPlannedNet() As Boolean
            Get
                Return _PrintPlannedNet
            End Get
            Set(ByVal value As Boolean)
                _PrintPlannedNet = value
            End Set
        End Property
        Private _PrintConfirmedNet As Boolean
        Public Property PrintConfirmedNet() As Boolean
            Get
                Return _PrintConfirmedNet
            End Get
            Set(ByVal value As Boolean)
                _PrintConfirmedNet = value
            End Set
        End Property


        Private _printPlannedGrossConfNet As Boolean
        Public Property PrintPlannedGrossConfNet() As Boolean
            Get
                Return _printPlannedGrossConfNet
            End Get
            Set(ByVal value As Boolean)
                _printPlannedGrossConfNet = value
            End Set
        End Property

        Public Property DatabaseID()
            Get
                Return mvarDatabaseID
            End Get
            Set(ByVal value)
                mvarDatabaseID = value
            End Set
        End Property
        Public Property ContractID()
            Get
                Return mvarContractID
            End Get
            Set(ByVal value)
                mvarContractID = value
            End Set
        End Property

        Sub DeleteUnusedChannels()
            Dim j As Integer = Channels.Count
            Dim i As Integer

            While j > 0
                i = Channels(j).BookingTypes.Count
                While i > 0
                    If Not Channels(j).BookingTypes(i).BookIt Then
                        Channels(j).BookingTypes.Remove(i)
                    End If
                    i -= 1
                End While
                If Channels(j).BookingTypes.Count = 0 Then
                    Channels.Remove(j)
                End If
                j -= 1
            End While
            IsStripped = True
        End Sub

        Dim _progress As frmProgress
        Sub ReloadDeletedChannels()
            'reloads the Bookingtypes that have been deleted
            Dim chanName As String = Campaign.Channels(1).ChannelName
            Dim btName As String = Campaign.Channels(1).BookingTypes(1).Name
            Me._progress = New frmProgress
            Me._progress.MaxValue = Channels.Count
            Me._progress.Show()
            Me._progress.BarType = cProgressBarType.SingleBar

            For Each TmpChan As Trinity.cChannel In Channels

                Me._progress.Progress += 1
                Me._progress.Status = "Updating channel " & TmpChan.ChannelName

                If TmpChan.fileName = "Channels.xml" Then
                    Dim tmpChanNew As New Trinity.cChannel(Campaign, Channels.RawCollection)
                    tmpChanNew.fileName = "Channels.xml"
                    tmpChanNew.ChannelName = TmpChan.ChannelName
                    tmpChanNew.MainObject = Campaign
                    tmpChanNew.ReadDefaultProperties("")
                    tmpChanNew.readDefaultBookingTypes()

                    'add BT that does not exists
                    For Each bt As Trinity.cBookingType In tmpChanNew.BookingTypes
                        If TmpChan.BookingTypes(bt.Name) Is Nothing Then
                            TmpChan.BookingTypes.Add(bt.Name, True)
                            TmpChan.BookingTypes(bt.Name).Shortname = tmpChanNew.BookingTypes(bt.Name).Shortname
                            TmpChan.BookingTypes(bt.Name).IsRBS = tmpChanNew.BookingTypes(bt.Name).IsRBS
                            TmpChan.BookingTypes(bt.Name).IsSpecific = tmpChanNew.BookingTypes(bt.Name).IsSpecific
                            TmpChan.BookingTypes(bt.Name).PrintDayparts = tmpChanNew.BookingTypes(bt.Name).PrintDayparts
                            TmpChan.BookingTypes(bt.Name).PrintBookingCode = tmpChanNew.BookingTypes(bt.Name).PrintBookingCode
                            TmpChan.BookingTypes(bt.Name).ReadDefaultDayparts()

                            'if we are in the middle of an campaign we need to add the weeks to the BT aswell
                            If Channels(chanName).BookingTypes(btName).Weeks.Count > 0 Then
                                For Each week As Trinity.cWeek In Channels(chanName).BookingTypes(btName).Weeks
                                    TmpChan.BookingTypes(bt.Name).Weeks.Add(week.Name)
                                    TmpChan.BookingTypes(bt.Name).Weeks(week.Name).StartDate = week.StartDate
                                    TmpChan.BookingTypes(bt.Name).Weeks(week.Name).EndDate = week.EndDate
                                    For Each f As Trinity.cFilm In Channels(chanName).BookingTypes(btName).Weeks(1).Films
                                        TmpChan.BookingTypes(bt.Name).Weeks(week.Name).Films.Add(f.Name)
                                        TmpChan.BookingTypes(bt.Name).Weeks(week.Name).Films(f.Name).Filmcode = f.Filmcode
                                        TmpChan.BookingTypes(bt.Name).Weeks(week.Name).Films(f.Name).FilmLength = f.FilmLength
                                        TmpChan.BookingTypes(bt.Name).Weeks(week.Name).Films(f.Name).Index = f.Index
                                    Next
                                Next
                            End If

                            TmpChan.BookingTypes(bt.Name).ReadPricelist()
                        End If

                    Next
                End If
            Next

            Dim XMLDoc As New Xml.XmlDocument
            Dim XMLChannels As Xml.XmlElement
            Dim XMLTmpNode As Xml.XmlElement
            Dim XMLTmpNode2 As Xml.XmlElement
            Dim XMLBookingtypes As Xml.XmlElement
            Dim tmpchannel As Trinity.cChannel
            Dim tmpBT As Trinity.cBookingType

            XMLDoc.Load(IO.Path.Combine(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork), Area) & "\Channels.xml")
            XMLChannels = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("Channels")
            XMLTmpNode = XMLChannels.ChildNodes.Item(0)

            Dim channel As Trinity.cChannel
            Dim tmpStr As String
            Dim found As Boolean = False
            'check that all channels are present
            While Not XMLTmpNode Is Nothing
                tmpStr = XMLTmpNode.GetAttribute("Name")
                For Each channel In Channels
                    If channel.ChannelName = tmpStr Then
                        found = True
                    End If
                Next
                'if the channel dont exist we add it
                If Not found Then
                    tmpchannel = Channels.Add(XMLTmpNode.GetAttribute("Name"), "", Area)
                    XMLBookingtypes = XMLTmpNode.SelectSingleNode("Bookingtypes")
                    If XMLBookingtypes Is Nothing Then
                        XMLBookingtypes = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("Bookingtypes")
                    End If
                    XMLTmpNode2 = XMLBookingtypes.ChildNodes.Item(0)
                    While Not XMLTmpNode2 Is Nothing
                        tmpBT = tmpchannel.BookingTypes.Add(XMLTmpNode2.GetAttribute("Name"), False)
                        tmpBT.Shortname = XMLTmpNode2.GetAttribute("Shortname")
                        tmpBT.IsRBS = XMLTmpNode2.GetAttribute("IsRBS")
                        tmpBT.IsSpecific = XMLTmpNode2.GetAttribute("IsSpecific")
                        tmpBT.IsPremium = XMLTmpNode2.GetAttribute("IsPremium")
                        Try
                            tmpBT.PrintDayparts = XMLTmpNode2.GetAttribute("PrintDayparts")
                            tmpBT.PrintBookingCode = XMLTmpNode2.GetAttribute("PrintBookingCode")
                        Catch
                            tmpBT.PrintDayparts = False
                            tmpBT.PrintBookingCode = False
                        End Try
                        tmpBT.ReadDefaultDayparts()

                        'if we are in the middle of an campaign we need to add hte weeks to the BT aswell
                        If Channels(chanName).BookingTypes(btName).Weeks.Count > 0 Then
                            For Each week As Trinity.cWeek In Channels(chanName).BookingTypes(btName).Weeks
                                tmpBT.Weeks.Add(week.Name)
                                tmpBT.Weeks(week.Name).StartDate = week.StartDate
                                tmpBT.Weeks(week.Name).EndDate = week.EndDate
                                For Each f As Trinity.cFilm In Channels(chanName).BookingTypes(btName).Weeks(1).Films
                                    tmpBT.Weeks(week.Name).Films.Add(f.Name)
                                    tmpBT.Weeks(week.Name).Films(f.Name).Filmcode = f.Filmcode
                                    tmpBT.Weeks(week.Name).Films(f.Name).FilmLength = f.FilmLength
                                    tmpBT.Weeks(week.Name).Films(f.Name).Index = f.Index
                                Next
                            Next
                        End If
                        tmpBT.Pricelist.Targets.Clear()
                        tmpBT.ReadPricelist()
                        XMLTmpNode2 = XMLTmpNode2.NextSibling
                    End While
                    XMLTmpNode2 = XMLTmpNode.GetElementsByTagName("SpotIndex").Item(0).FirstChild
                    While Not XMLTmpNode2 Is Nothing
                        For Each tmpBT In tmpchannel.BookingTypes
                            tmpBT.FilmIndex(XMLTmpNode2.GetAttribute("Length")) = XMLTmpNode2.GetAttribute("Idx")
                        Next tmpBT
                        XMLTmpNode2 = XMLTmpNode2.NextSibling
                    End While
                End If
                found = False
                XMLTmpNode = XMLTmpNode.NextSibling
            End While

            If Contract IsNot Nothing Then
                If MessageBox.Show("Your campaign is connected to a contract, would you like to apply contract on campaign again?", "T R I N I T Y", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.yes Then
                    Campaign.Contract.ApplyToCampaign()
                End If
            End If

            Me._progress.Hide()
            Me._progress.Dispose()
            IsStripped = False
        End Sub

        Public Overrides Function ToString() As String
            Return Name
        End Function

        Public Property AdToox() As Trinity.cAdtoox
            Get
                Return mvarAdtoox
            End Get
            Set(ByVal value As Trinity.cAdtoox)
                If TrinitySettings.AdtooxEnabled Then
                    mvarAdtoox = value
                    _adTooxStatusForChannel = New Dictionary(Of String, Dictionary(Of String, cAdTooxStatus))
                    _totalAdTooxStatus = New Dictionary(Of String, Trinity.cAdTooxStatus)
                    For Each TmpChan As Trinity.cChannel In mvarChannels
                        For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                            If TmpBT.BookIt Then
                                For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                                    For Each TmpFilm As Trinity.cFilm In TmpWeek.Films
                                        TmpFilm.InvalidateAdtooxStatus()
                                        Dim TmpDummy As Trinity.cAdTooxStatus = TmpFilm.AdTooxStatus
                                    Next
                                Next
                            End If
                        Next
                    Next
                End If
            End Set
        End Property

        Public Property MarathonInsertions() As System.Xml.XmlNode
            Get
                Return mvarMarathonInsertions
            End Get
            Set(ByVal value As System.Xml.XmlNode)
                mvarMarathonInsertions = value
            End Set
        End Property

        Public Property MatrixID() As String
            Get
                Return mvarMatrixID
            End Get
            Set(ByVal Value As String)
                mvarMatrixID = Value
            End Set
        End Property


        Public Property EstimatedWeeklyReach(ByVal week As String) As Single
            Get
                If _estimatedWeeklyReach.ContainsKey(week) Then
                    Return _estimatedWeeklyReach(week)
                Else
                    Return 0
                End If
            End Get
            Set(ByVal value As Single)
                If _estimatedWeeklyReach.ContainsKey(week) Then
                    _estimatedWeeklyReach(week) = value
                Else
                    _estimatedWeeklyReach.Add(week, value)
                End If
            End Set
        End Property

        Private _totalAdTooxStatus As New Dictionary(Of String, cAdTooxStatus)
        Public Function GetTotalAdTooxStatus(ByVal Filmcode As String) As Object
            If Not _totalAdTooxStatus.ContainsKey(Filmcode) Then
                Try
                    _totalAdTooxStatus.Add(Filmcode, AdToox.getSingleFilmCodeInfo(Filmcode).getStatusForAllChannels())
                Catch
                    _totalAdTooxStatus.Add(Filmcode, Nothing)
                End Try
            End If
            Return _totalAdTooxStatus(Filmcode)
        End Function

        Private WithEvents _adTooxStatusForChannel As New Dictionary(Of String, Dictionary(Of String, Trinity.cAdTooxStatus))
        Friend Function GetAdTooxStatusForChannel(ByVal Channel As Trinity.cChannel, ByVal Filmcode As String) As cAdTooxStatus
            If AdToox Is Nothing Then Exit Function
            If Not _adTooxStatusForChannel.ContainsKey(Filmcode) Then
                _adTooxStatusForChannel.Add(Filmcode, New Dictionary(Of String, cAdTooxStatus))
            End If
            If Not _adTooxStatusForChannel(Filmcode).ContainsKey(Channel.ChannelName) OrElse _adTooxStatusForChannel(Filmcode)(Channel.ChannelName) Is Nothing Then
                If AdToox.getSingleFilmCodeInfo(Filmcode) IsNot Nothing Then
                    If Not _adTooxStatusForChannel(Filmcode).ContainsKey(Channel.ChannelName) Then
                        _adTooxStatusForChannel(Filmcode).Add(Channel.ChannelName, AdToox.getSingleFilmCodeInfo(Filmcode).getStatusForChannel(Channel.AdTooxChannelID))
                    Else
                        _adTooxStatusForChannel(Filmcode)(Channel.ChannelName) = AdToox.getSingleFilmCodeInfo(Filmcode).getStatusForChannel(Channel.AdTooxChannelID)
                    End If
                ElseIf Not _adTooxStatusForChannel(Filmcode).ContainsKey(Channel.ChannelName) Then
                    _adTooxStatusForChannel(Filmcode).Add(Channel.ChannelName, Nothing)
                End If
            End If
            Return _adTooxStatusForChannel(Filmcode)(Channel.ChannelName)


        End Function

        Private Sub MainTargetAltered() Handles mvarMainTarget.wasAltered
            If MainTargetDelegate Is Nothing Then Exit Sub
            MainTargetDelegate.DynamicInvoke()
        End Sub

        Private Sub SecondaryTargetAltered() Handles mvarSecondaryTarget.wasAltered
            If SecondaryTargetDelegate Is Nothing Then Exit Sub
            SecondaryTargetDelegate.DynamicInvoke()
        End Sub

        Private Sub ThirdTargetAltered() Handles mvarThirdTarget.wasAltered
            If ThirdTargetDelegate Is Nothing Then Exit Sub
            ThirdTargetDelegate.DynamicInvoke()
        End Sub

        Public Sub AddActualspotToTargetChanged(ByVal spot As cActualSpot)

            Dim del As New MainTargetChangedHandler(AddressOf spot.MainTargetChanged)
            MainTargetDelegate = MulticastDelegate.Combine(MainTargetDelegate, del)

            Dim del1 As New SecondaryTargetChangedHandler(AddressOf spot.SecondaryTargetChanged)
            SecondaryTargetDelegate = MulticastDelegate.Combine(SecondaryTargetDelegate, del1)

            Dim del2 As New ThirdTargetChangedHandler(AddressOf spot.ThirdTargetChanged)
            ThirdTargetDelegate = MulticastDelegate.Combine(ThirdTargetDelegate, del2)

            Dim del3 As New DaypartsChangedHandler(AddressOf spot.daypartsChanged)
            DaypartsDelegate = MulticastDelegate.Combine(DaypartsDelegate, del3)
        End Sub

        Public Enum TargetEnum
            MainTarget = 1
            SecondaryTarget = 2
            ThirdTarget = 3
        End Enum

        Public Enum PIBEnum
            PIBFirst = 1
            PIBMiddle = 2
            PIBLast = 3
            PIB2ndFirst = 4
            PIB2ndLast = 5
        End Enum


        Public Property Status() As String
            Get
                Return mvarStatus
            End Get
            Set(ByVal value As String)
                mvarStatus = value
            End Set
        End Property

        Public Enum ReachTargetEnum
            rteMainTarget = 0
            rteSecondTarget = 1
            rteThirdTarget = 2
            rteAllAdults = 4
            rteCustomTarget = 5
        End Enum

        Public Property Combinations() As cCombinations
            Get
                Return mvarCombinations
            End Get
            Set(ByVal value As cCombinations)
                mvarCombinations = value
            End Set
        End Property

        '---------------------------------------------------------------------------------------
        ' Procedure : Universes
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   : returns the universe for the campaign
        '---------------------------------------------------------------------------------------
        '
        Public ReadOnly Property Universes() As Collections.Specialized.NameValueCollection
            Get
                Return mvarUniverses
            End Get
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : MarathonCTC
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   : gets and sets the cost which will we billed the client
        '---------------------------------------------------------------------------------------
        '
        Public Property MarathonCTC() As Single
            Get
                Return mvarMarathonCTC
            End Get
            Set(ByVal value As Single)
                mvarMarathonCTC = value
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : GetNewActualSpots
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   : gets all the spots that have been aired
        '---------------------------------------------------------------------------------------
        '
        Function GetNewActualSpots(Optional ByVal ForceGet As Boolean = False) As Boolean
            If GettingSpots AndAlso Not ForceGet Then Return False
            On Error GoTo errhandle
            GettingSpots = True
            Dim SpotCount As Integer
            Dim i As Integer
            Dim TmpSpot As cActualSpot
            Dim FoundNew As Boolean
            If Not Me.RootCampaign Is Nothing Then
                GettingSpots = False
                Exit Function
            End If
            Helper.WriteToLogFile("GetNewActualSpots: Register Connect.dll")
            Dim TmpAdedge As New ConnectWrapper.Brands
            Helper.WriteToLogFile("OK")
            If mvarChannels.Count = 0 OrElse mvarChannels(1).BookingTypes.Count = 0 OrElse mvarChannels(1).BookingTypes(1).Weeks.Count = 0 OrElse mvarChannels(1).BookingTypes(1).Weeks(1).Films.Count = 0 Then
                GettingSpots = False
                Exit Function 'if there are no bookings then exit
            End If

            TmpAdedge.clearList()
            If mvarUpdatedTo < StartDate - 1 Then
                mvarUpdatedTo = StartDate - 1
            End If
            TmpAdedge.setPeriod(Format(Date.FromOADate(mvarUpdatedTo).AddDays(1), "ddMMyy") & "-" & Format(Date.FromOADate(EndDate), "ddMMyy"))
            TmpAdedge.setArea(mvarArea)
            TmpAdedge.setChannelsArea(ChannelString, mvarArea)


            Trinity.Helper.AddTargetsToAdedge(TmpAdedge, True, True)
            TmpAdedge.setBrandFilmCode(mvarAreaLog, "Ö")
            TmpAdedge.setBrandFilmCode(mvarAreaLog, FilmcodeString())
            For Each TmpString As String In AdEdgeProducts
                If TmpString.Substring(0, 3) = "[A]" Then
                    TmpAdedge.setBrandAdvertiser(AreaLog, TmpString.Substring(3))
                Else
                    TmpAdedge.setBrandProduct(AreaLog, TmpString.Substring(3))
                End If
            Next
            TmpAdedge.setBrandType("COMMERCIAL,SPONSOR,PROMO")
            SpotCount = TmpAdedge.Run(True, False, -1)
            TmpAdedge.sort("date(asc),fromtime(asc)")
            FoundNew = SpotCount > 0
            If FoundNew Then Trinity.Helper.ClearFilmList()

            Dim BreakSeqIDs As New Collection

            For i = 0 To SpotCount - 1
                RaiseEvent ReadNewSpot(i, SpotCount)
                TmpSpot = mvarActualSpots.Add(Date.FromOADate(TmpAdedge.getAttrib(Connect.eAttribs.aDate, i)), TmpAdedge.getAttrib(Connect.eAttribs.aFromTime, i) \ 60)
                TmpSpot.Second = TmpAdedge.getAttrib(Connect.eAttribs.aFromTime, i) Mod 60
                TmpSpot.AdedgeChannel = TmpAdedge.getAttrib(Connect.eAttribs.aChannel, i)
                TmpSpot.Advertiser = TmpAdedge.getAttrib(Connect.eAttribs.aBrandAdvertiser, i)
                If TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, i) = TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, i) Then
                    TmpSpot.BreakType = cActualSpot.EnumBreakType.btBlock
                Else
                    TmpSpot.BreakType = cActualSpot.EnumBreakType.btBreak
                End If
                TmpSpot.Channel = Helper.Adedge2Channel(TmpSpot.AdedgeChannel)
                'For Each TmpBT As Trinity.cBookingType In TmpSpot.Channel.BookingTypes
                '    If TmpBT.BookIt And TmpBT.IsRBS = True Then
                '        TmpSpot.Bookingtype = TmpBT
                '        Exit For
                '    End If
                'Next
                If TmpSpot.Bookingtype Is Nothing Then
                    For Each TmpBT As Trinity.cBookingType In TmpSpot.Channel.BookingTypes
                        If TmpBT.BookIt Then
                            TmpSpot.Bookingtype = TmpBT
                            Exit For
                        End If
                    Next
                End If
                'If TmpSpot.Bookingtype Is Nothing Then
                '    TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(1)
                'End If
                'TmpSpot.Bookingtype.ActualSpots.Add(TmpSpot, TmpSpot.ID)
                TmpSpot.Deactivated = False
                TmpSpot.Filmcode = TmpAdedge.getAttrib(Connect.eAttribs.aBrandFilmCode, i)
                TmpSpot.PosInBreak = TmpAdedge.getAttrib(Connect.eAttribs.aBrandSpotInBreak, i)
                TmpSpot.SpotsInBreak = TmpAdedge.getAttrib(Connect.eAttribs.aBrandSpotCount, i)
                TmpSpot.Product = TmpAdedge.getAttrib(Connect.eAttribs.aBrandProduct, i)
                TmpSpot.ProgAfter = TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, i)
                TmpSpot.ProgBefore = TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, i)
                TmpSpot.Programme = TmpSpot.ProgAfter
                TmpSpot.SpotLength = TmpAdedge.getAttrib(Connect.eAttribs.aBrandDurationNom, i)
                TmpSpot.Week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
                TmpSpot.BrandBreakSeqID = TmpAdedge.getAttrib(Connect.eAttribs.aBrandBreakSeqID, i)
                If TmpSpot.Week Is Nothing Then
                    'denna rad ger fel ibland på gamla kampanjer
                    TmpSpot.Week = TmpSpot.Bookingtype.Weeks(1)
                End If

                For Each prgArgument As String In Environment.GetCommandLineArgs
                    If prgArgument = "NoNewSpots" Then newSpots = False
                Next

                If TmpSpot.Week.Films(TmpSpot.Filmcode) Is Nothing And newSpots Then
                    Dim frmfilmnotfound As frmActualSpotFilmNotFound = New frmActualSpotFilmNotFound(TmpSpot.Filmcode, TmpSpot.SpotLength, TmpSpot.Channel.ChannelName)
                    frmfilmnotfound.ShowDialog()
                End If

                If Not BreakSeqIDs.Contains(TmpSpot.BrandBreakSeqID) Then
                    BreakSeqIDs.Add(TmpSpot.BrandBreakSeqID)
                    TmpSpot.SharesBreak = False
                Else
                    TmpSpot.SharesBreak = True
                End If

            Next


            If TmpAdedge.validate = 0 AndAlso TmpAdedge.getDataRangeTo(Connect.eDataType.mSpot) < EndDate Then
                mvarUpdatedTo = TmpAdedge.getDataRangeTo(Connect.eDataType.mSpot)
            ElseIf TmpAdedge.validate = 0 Then
                mvarUpdatedTo = EndDate
            End If
            GettingSpots = False
            If SpotCount > 0 Then
                CreateAdedgeSpots()
            End If
            Return FoundNew

errhandle:
            GettingSpots = False
        End Function

        'Public Function GetRF(ByVal freq As Object, ByVal tar As String) As Object
        '    Dim f As Object = campaign.Adedge.getRF(campaign.Adedge.getGroupCount - 1, 0, campaign.UniColl(campaign.MainTarget.Universe) - 1, campaign.TargColl(tar, campaign.Adedge) - 1, freq)
        '    Return f
        'End Function

        Private Function GetWildcardFilmcodes(ByVal Wildcard As String) As String
            If Not Wildcard.EndsWith("*") Then
                Return ""
            End If
            Dim WCAdedge As New ConnectWrapper.Brands
            WCAdedge.setPeriod(Format(Date.FromOADate(mvarUpdatedTo).AddDays(1), "ddMMyy") & "-" & Format(Date.FromOADate(EndDate), "ddMMyy"))
            WCAdedge.setArea(mvarArea)
            WCAdedge.setChannelsArea(ChannelString, mvarArea)
            WCAdedge.setTargetMnemonic("3+", False)
            WCAdedge.setBrandFilmCode(mvarAreaLog, "")
            WCAdedge.setBrandType("PROMO")
            Dim Spotcount As Integer = WCAdedge.Run
            'Not possible to split on filmcodes. Wait for Techedge to respond.
            Dim Films As Integer = WCAdedge.setSplitVar("filmcode")
            Dim FilmString As String = ""
            Wildcard = Wildcard.TrimEnd("*")
            For i As Integer = 0 To Films
                If UCase(WCAdedge.getSplitName(i, 0)).ToString.StartsWith(UCase(Wildcard)) Then
                    FilmString += WCAdedge.getSplitName(i, 0) & ","
                End If
            Next
            Return FilmString
        End Function

        '---------------------------------------------------------------------------------------
        ' Procedure : CreateAdegeSpots
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        Sub CreateAdedgeSpots(Optional ByVal DoItFast As Boolean = False)
            If GettingSpots Then Exit Sub
            GettingSpots = True

            Dim TmpSpot As cActualSpot
            Dim SpotCount As Long

            Dim debugMessage As String
            Try
                mvarAdedge.clearList()
                mvarAdedge.clearBrandFilter()
                mvarAdedge.clearGroup()
                mvarAdedge.setArea(mvarArea)
                mvarAdedge.setPeriod(Format(Date.FromOADate(StartDate), "ddMMyy") & "-" & Format(Date.FromOADate(EndDate), "ddMMyy"))
                'Trinity.Helper.AddTimeShift(mvarAdedge)
                mvarAdedge.setChannelsArea(ChannelString, mvarArea)
                Helper.AddTargetsToAdedge(mvarAdedge, True)
                'mvarAdedge.setUniverseUserDefined(mvarUniStr)
                mvarAdedge.sort("")

                Dim i As Long = 0
                For Each TmpSpot In mvarActualSpots
                    SpotCount = mvarAdedge.addBrand(Format(Date.FromOADate(TmpSpot.AirDate), "ddMMyy"), Helper.Mam2Tid(TmpSpot.MaM) & ":" & Format(TmpSpot.Second, "00"), TmpSpot.AdedgeChannel, mvarArea, TmpSpot.SpotLength)
                Next
                debugMessage = mvarAdedge.debug()
                If SpotCount > 0 Then
                    If DoItFast Then
                        SpotCount = mvarAdedge.Run(False, False, 0)
                    Else
                        SpotCount = mvarAdedge.Run(True, False, 10)
                    End If
                End If
                mvarAdedge.sort("date(asc),fromtime(asc)")
                GettingSpots = False
            Catch ex As Exception
                MsgBox("Create adedge spots failed" & debugMessage)
                GettingSpots = False
            End Try
        End Sub



        '---------------------------------------------------------------------------------------
        ' Procedure : CalculateSpots
        ' DateTime  : 2006-03-23 10:07
        ' Author    : joho
        ' Purpose   : Calculate ratings of spots
        ' Parameters:
        '               CalculateReach As Boolean = True
        '                   Should reach be calculated?
        '               FromDate As Date = -1,ToDate As Date = -1
        '                   Calculate spots between these dates
        '               FreqFocus As Byte = 1
        '                   What is the Frequency focus?
        '               UseFilters As Boolean = True
        '                   Should the filters in trinityfilters.ini be used?
        '                   When set to true, all parameters after this are ignored
        '               Chan As cChannel = Nothing
        '                   Only calculate spots in the specified channel
        '               OnlyWeek = -1
        '                   Only calculate spots in the specified week
        '               Bookingtype As cBookingType = Nothing
        '                   Only calculate spots in the specified bookingtype
        '               Film As cFilm = Nothing
        '                   Only calculate spots with the specified film
        '               Daypart = -1
        '                   Only calculate spots in the specified daypart
        '               PosInBreak = -1
        '                   Only calculate spots with the specified position in break
        '---------------------------------------------------------------------------------------
        '
        Function CalculateSpots(Optional ByVal CalculateReach As Boolean = True, Optional ByVal FromDate As Date = Nothing, Optional ByVal ToDate As Date = Nothing, Optional ByVal FreqFocus As Byte = 1, Optional ByVal UseFilters As Boolean = True, Optional ByVal OnlyWeek As String = "", Optional ByVal Bookingtype As cBookingType = Nothing, Optional ByVal Film As cFilm = Nothing, Optional ByVal Daypart As Integer = -1, Optional ByVal PosInBreak As PIBEnum = -1, Optional ByVal ExcludeBookingtypes As List(Of cBookingType) = Nothing, Optional ByVal ForceCalculate As Boolean = False, Optional ByVal Bookingtypes As List(Of cBookingType) = Nothing, Optional PrimeOnly As Boolean = False) As Single
            If GettingSpots OrElse mvarAdedge.validate > 0 OrElse (BlockCalculateSpots AndAlso Not ForceCalculate) Then Return 0
            GettingSpots = True

            Dim s As Integer
            Dim ContinueThis() As Boolean
            Dim Cont As Boolean
            Dim q As Integer
            Dim LowBound As Integer
            Dim SpotCount As Integer
            Dim WeekDays() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}

            'if there are no ActualSpots we cant make any calculations
            If mvarActualSpots.Count = 0 Then Exit Function

            'sets the boolean array length to match the number of ActualSpots
            ReDim ContinueThis(mvarActualSpots.Count)

            mvarAdedge.clearGroup()
            '***************************************
            mvarAdedge.sort("date(asc),fromtime(asc)")
            '***************************************
            For s = 1 To mvarActualSpots.Count
                'sets the Array(s) to False
                ContinueThis(s) = False
                'if we are using filters 
                If UseFilters Then
                    'There has to be a ActualSpot and a matching Film for a certain channel, in a certain week
                    'If that is true, the we set ContinuwThis = true
                    If GeneralFilter.Data("Channels", mvarActualSpots(s).Channel.ChannelName) Then
                        If Not mvarActualSpots(s).Week Is Nothing AndAlso GeneralFilter.Data("Weeks", mvarActualSpots(s).Week.Name) Then
                            If GeneralFilter.Data("Bookingtype", mvarActualSpots(s).Bookingtype.Name) Then
                                If Not mvarActualSpots(s).Week Is Nothing AndAlso Not mvarActualSpots(s).Week.Films(mvarActualSpots(s).Filmcode) Is Nothing AndAlso GeneralFilter.Data("Films", mvarActualSpots(s).Week.Films(mvarActualSpots(s).Filmcode).Name) Then
                                    If Dayparts.GetDaypartForMam(mvarActualSpots(s).MaM) IsNot Nothing AndAlso GeneralFilter.Data("Daypart", Dayparts.GetDaypartForMam(mvarActualSpots(s).MaM).Name) Then
                                        If GeneralFilter.Data("Week", mvarActualSpots(s).Week.Name) Then
                                            If GeneralFilter.Data("Weekday", WeekDays(Weekday(Date.FromOADate(mvarActualSpots(s).AirDate), FirstDayOfWeek.Monday) - 1)) Then
                                                If GeneralFilter.Data("Film", mvarActualSpots(s).Week.Films(mvarActualSpots(s).Filmcode).Name) Then
                                                    If Not mvarActualSpots(s).Deactivated Then
                                                        ContinueThis(s) = True
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Else 'if we are not useing filters we continue here
                    Cont = True
                    'Cont = False
                    'If Chan Is Nothing Then
                    '    Cont = True
                    'Else
                    '    If mvarActualSpots(s).Channel Is Chan Then
                    '        Cont = True
                    '    End If
                    'End If
                    If Cont Then
                        Cont = False
                        If Bookingtype Is Nothing Then
                            Cont = True
                        Else
                            If mvarActualSpots(s).Bookingtype Is Bookingtype Then
                                Cont = True
                            End If
                        End If
                        If Cont Then
                            Cont = False
                            If OnlyWeek = "" Then
                                Cont = True
                            Else
                                If Not mvarActualSpots(s).Week Is Nothing AndAlso mvarActualSpots(s).Week.Name = OnlyWeek Then
                                    Cont = True
                                End If
                            End If
                            If Cont Then
                                Cont = False
                                If Film Is Nothing Then
                                    Cont = True
                                Else
                                    If Not mvarActualSpots(s).Week.Films(mvarActualSpots(s).Filmcode) Is Nothing AndAlso mvarActualSpots(s).Week.Films(Film.Name).Filmcode = mvarActualSpots(s).Week.Films(mvarActualSpots(s).Filmcode).Filmcode Then
                                        Cont = True
                                    End If
                                End If
                                If Cont Then
                                    Cont = False
                                    If Daypart = -1 Then
                                        Cont = True
                                    Else
                                        If Daypart = Dayparts.GetDaypartIndexForMam(mvarActualSpots(s).MaM) Then
                                            Cont = True
                                        End If
                                    End If
                                    If Cont Then
                                        Cont = False
                                        If PosInBreak = -1 Then
                                            Cont = True
                                        Else
                                            If mvarActualSpots(s).PosInBreak = 1 Then
                                                If PosInBreak = 1 Then
                                                    Cont = True
                                                End If
                                            ElseIf mvarActualSpots(s).PosInBreak = mvarActualSpots(s).SpotsInBreak Then
                                                If PosInBreak = 3 Then
                                                    Cont = True
                                                End If
                                            ElseIf mvarActualSpots(s).PosInBreak = 2 Then
                                                If PosInBreak = PIBEnum.PIB2ndFirst Then
                                                    Cont = True
                                                End If
                                            ElseIf mvarActualSpots(s).PosInBreak = mvarActualSpots(s).SpotsInBreak - 1 Then
                                                If PosInBreak = PIBEnum.PIB2ndLast Then
                                                    Cont = True
                                                End If
                                            Else
                                                If PosInBreak = 2 Then
                                                    Cont = True
                                                End If
                                            End If
                                        End If
                                        If Cont Then
                                            Cont = False
                                            If FromDate = Nothing Then
                                                Cont = True
                                            Else
                                                If FromDate.ToOADate <= mvarActualSpots(s).AirDate Then
                                                    Cont = True
                                                End If
                                            End If
                                            If Cont Then
                                                Cont = False
                                                If ToDate = Nothing Then
                                                    Cont = True
                                                Else
                                                    If ToDate.ToOADate >= mvarActualSpots(s).AirDate Then
                                                        Cont = True
                                                    End If
                                                End If
                                                If Bookingtypes Is Nothing Then
                                                    Cont = True
                                                Else
                                                    Cont = Bookingtypes.Contains(mvarActualSpots(s).Bookingtype)
                                                End If
                                                If Cont Then
                                                    If PrimeOnly AndAlso Dayparts.GetDaypartForMam(mvarActualSpots(s).MaM).IsPrime Then
                                                        Cont = True
                                                    End If
                                                    If Cont Then
                                                        If Not ExcludeBookingtypes Is Nothing Then
                                                            If Not ExcludeBookingtypes.Contains(mvarActualSpots(s).Bookingtype) Then
                                                                ContinueThis(s) = True
                                                            End If
                                                        Else
                                                            ContinueThis(s) = True
                                                        End If
                                                    End If
                                                End If
                                            End If
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
                    If InStr(UCase(mvarActualSpots(s).Channel.AdEdgeNames) & ",", UCase(mvarAdedge.getAttrib(Connect.eAttribs.aChannel, s - 1)) & ",") = 0 Then
                        LowBound = -5
                        If s + LowBound - 1 < 0 Then
                            LowBound = -s + 1
                        End If
                        For q = LowBound To 5
                            '                    If s + q - 1 < 0 Then
                            '                        q = q - (s + q) + 1
                            '                    End If

                            'edit made
                            If (s + q - 1) = mvarActualSpots.Count Then
                                MsgBox("The spot '" & mvarActualSpots(s).ProgAfter & "' in " & mvarActualSpots(s).Channel.ChannelName & " " & mvarActualSpots(s).Bookingtype.Name & ", aired at " & Date.FromOADate(mvarActualSpots(s).AirDate) & " " & Helper.Mam2Tid(mvarActualSpots(s).MaM) & " could not be found.", MsgBoxStyle.Critical, "ERROR")
                                Exit For
                            End If
                            '*
                            If mvarActualSpots(s).MaM = mvarAdedge.getAttrib(Connect.eAttribs.aFromTime, s + q - 1) \ 60 Then
                                If InStr(UCase(mvarActualSpots(s).Channel.AdEdgeNames) & ",", UCase(mvarAdedge.getAttrib(Connect.eAttribs.aChannel, s + q - 1)) & ",") > 0 Then
                                    If mvarActualSpots(s).AirDate = mvarAdedge.getAttrib(Connect.eAttribs.aDate, s + q - 1) Then
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                    End If
                    'mvarAdedge.deleteFromGroup(s + q - 1, False)
                    On Error GoTo ErrHandle
                    mvarActualSpots(s).GroupIdx = mvarAdedge.addToGroup(s + q - 1) - 1
max_count:

                Else 'If ContinueThis was false we go from here
                    q = 0
                    If InStr(UCase(mvarActualSpots(s).Channel.AdEdgeNames), UCase(mvarAdedge.getAttrib(Connect.eAttribs.aChannel, s - 1))) = 0 Then
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
                            If s + q - 1 >= mvarActualSpots.Count Then
                                '??????????????????????????????????????????????????????????????????????????????????????????
                                'Debug.Assert(False)
                                Exit For
                            End If

                            If mvarActualSpots(s).MaM = mvarAdedge.getAttrib(Connect.eAttribs.aFromTime, s + q - 1) \ 60 Then
                                If InStr(UCase(mvarActualSpots(s).Channel.AdEdgeNames), UCase(mvarAdedge.getAttrib(Connect.eAttribs.aChannel, s + q - 1))) > 0 Then
                                    If mvarActualSpots(s).AirDate = mvarAdedge.getAttrib(Connect.eAttribs.aDate, s + q - 1) Then
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                    End If
                    'mvarAdedge.deleteFromGroup(s + q - 1, False)
                    mvarActualSpots(s).GroupIdx = -1
                End If
                'end of the Loop For s = 1 To mvarActualSpots.Count
                mvarActualSpots(s).InvalidateTargets()
            Next

            If CalculateReach Then
                'Calculate the reach
                On Error GoTo ErrHandle
                SpotCount = mvarAdedge.recalcRF(Connect.eSumModes.smGroup)
            End If
            GettingSpots = False

            Exit Function

ErrHandle:
            ' If statement is for us to be able to resume at an error in debug mode
            If False Then
                Resume
            End If
            GettingSpots = False
        End Function


        '---------------------------------------------------------------------------------------
        ' Procedure : HistoryDate
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        Public Property HistoryDate() As Date
            Get
                HistoryDate = mvarHistoryDate
            End Get
            Set(ByVal Value As Date)
                mvarHistoryDate = Value
            End Set
        End Property

        Public Property ErrorCheckingEnabled() As Boolean
            Get
                Return mvarErrorCheckingEnabled
            End Get
            Set(ByVal value As Boolean)
                mvarErrorCheckingEnabled = value
            End Set
        End Property

        Public Property ExtranetDatabaseID() As Integer
            Get
                Return mvarExtranetDatabaseID
            End Get
            Set(ByVal value As Integer)
                mvarExtranetDatabaseID = value
            End Set
        End Property

        '---------------------------------------------------------------------------------------
        ' Procedure : Channels
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        Public ReadOnly Property Channels() As cChannels
            Get
                'used when retrieving value of a property, on the right side of an assignment.
                'Syntax: Debug.Print X.Channels
                Channels = mvarChannels
            End Get
            'Set(ByVal Value As cChannels)
            '    'used when assigning an Object to the property, on the left side of a Set statement.
            '    'Syntax: Set x.Channels = Form1
            '    mvarChannels = Value
            '    mvarChannels.MainObject = Me
            'End Set
        End Property

        '---------------------------------------------------------------------------------------
        ' Procedure : Name
        ' DateTime  : 2003-06-11 13:38
        ' Author    : joho
        ' Purpose   : Returns/sets the name of the campaign
        '---------------------------------------------------------------------------------------
        '
        Public Property Name() As String
            Get
                On Error GoTo Name_Error
                DebugStack.Log("cKampanj.Name_get")
                Name = mvarName
                On Error GoTo 0
                Exit Property
Name_Error:
                Err.Raise(Err.Number, "cKampanj: Name", Err.Description)
            End Get
            Set(ByVal Value As String)
                On Error GoTo Name_Error
                DebugStack.Log("cKampanj.Name_set")
                mvarName = Value
                On Error GoTo 0
                Exit Property
Name_Error:
                Err.Raise(Err.Number, "cKampanj: Name", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : Version
        ' DateTime  : 2003-06-11 13:47
        ' Author    : joho
        ' Purpose   : Returns/sets the version on wich the campaign was saved.
        '---------------------------------------------------------------------------------------
        '
        Public Property VERSION() As Byte
            Get
                On Error GoTo Version_Error
                VERSION = mvarVersion
                On Error GoTo 0
                Exit Property
Version_Error:
                Err.Raise(Err.Number, "cKampanj: Version", Err.Description)
            End Get
            Set(ByVal Value As Byte)
                On Error GoTo Version_Error
                mvarVersion = Value
                On Error GoTo 0
                Exit Property
Version_Error:
                Err.Raise(Err.Number, "cKampanj: Version", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : Planner
        ' DateTime  : 2003-06-11 13:48
        ' Author    : joho
        ' Purpose   : Returns/sets the name of the responsible Planner for the campaign
        '---------------------------------------------------------------------------------------
        '
        Public Property Planner() As String
            Get
                On Error GoTo Planner_Error
                DebugStack.Log("cKampanj.Planner_get")
                Planner = mvarPlanner
                On Error GoTo 0
                Exit Property
Planner_Error:
                Err.Raise(Err.Number, "cKampanj: Planner", Err.Description)
            End Get
            Set(ByVal Value As String)
                On Error GoTo Planner_Error
                DebugStack.Log("cKampanj.Planner_set")
                mvarPlanner = Value
                On Error GoTo 0
                Exit Property
Planner_Error:
                Err.Raise(Err.Number, "cKampanj: Planner", Err.Description)
            End Set
        End Property

        Public Property PlannerEmail() As String
            Get
                On Error GoTo Planner_Error
                PlannerEmail = mvarPlannerEmail
                On Error GoTo 0
                Exit Property
Planner_Error:
                Err.Raise(Err.Number, "cKampanj: PlannerEmail", Err.Description)
            End Get
            Set(ByVal Value As String)
                On Error GoTo Planner_Error
                mvarPlannerEmail = Value
                On Error GoTo 0
                Exit Property
Planner_Error:
                Err.Raise(Err.Number, "cKampanj: PlannerEmail", Err.Description)
            End Set
        End Property


        Public Property PlannerPhone() As String
            Get
                On Error GoTo Planner_Error
                PlannerPhone = mvarPlannerPhone
                On Error GoTo 0
                Exit Property
Planner_Error:
                Err.Raise(Err.Number, "cKampanj: PlannerPhone", Err.Description)
            End Get
            Set(ByVal Value As String)
                On Error GoTo Planner_Error
                mvarPlannerPhone = Value
                On Error GoTo 0
                Exit Property
Planner_Error:
                Err.Raise(Err.Number, "cKampanj: PlannerPhone", Err.Description)
            End Set
        End Property
        '---------------------------------------------------------------------------------------
        ' Procedure : PlannerID
        ' DateTime  : 2007-02-05 13:48
        ' Author    : Krku
        ' Purpose   : Returns/sets the ID of the responsible Planner for the campaign
        '---------------------------------------------------------------------------------------
        '
        Public Property PlannerID() As Integer
            Get
                On Error GoTo Planner_Error
                PlannerID = mvarPlannerID
                On Error GoTo 0
                Exit Property
Planner_Error:
                Err.Raise(Err.Number, "cKampanj: PlannerID", Err.Description)
            End Get
            Set(ByVal Value As Integer)
                On Error GoTo Planner_Error
                mvarPlannerID = Value
                On Error GoTo 0
                Exit Property
Planner_Error:
                Err.Raise(Err.Number, "cKampanj: PlannerID", Err.Description)
            End Set
        End Property



        '---------------------------------------------------------------------------------------
        ' Procedure : Buyer
        ' DateTime  : 2003-06-11 13:48
        ' Author    : joho
        ' Purpose   : Returns/sets the name of the responsible Buyer for the campaign
        '---------------------------------------------------------------------------------------
        '
        Public Property Buyer() As String
            Get
                On Error GoTo Buyer_Error
                Buyer = mvarBuyer
                On Error GoTo 0
                Exit Property
Buyer_Error:
                Err.Raise(Err.Number, "cKampanj: Buyer", Err.Description)
            End Get
            Set(ByVal Value As String)
                On Error GoTo Buyer_Error
                mvarBuyer = Value
                On Error GoTo 0
                Exit Property
Buyer_Error:
                Err.Raise(Err.Number, "cKampanj: Buyer", Err.Description)
            End Set
        End Property

        Public Property BuyerEmail() As String
            Get
                On Error GoTo Buyer_Error
                BuyerEmail = mvarBuyerEmail
                On Error GoTo 0
                Exit Property
Buyer_Error:
                Err.Raise(Err.Number, "cKampanj: BuyerEmail", Err.Description)
            End Get
            Set(ByVal Value As String)
                On Error GoTo Buyer_Error
                mvarBuyerEmail = Value
                On Error GoTo 0
                Exit Property
Buyer_Error:
                Err.Raise(Err.Number, "cKampanj: BuyerEmail", Err.Description)
            End Set
        End Property

        Public Property BuyerPhone() As String
            Get
                On Error GoTo Buyer_Error
                BuyerPhone = mvarBuyerPhone
                On Error GoTo 0
                Exit Property
Buyer_Error:
                Err.Raise(Err.Number, "cKampanj: BuyerPhone", Err.Description)
            End Get
            Set(ByVal Value As String)
                On Error GoTo Buyer_Error
                mvarBuyerPhone = Value
                On Error GoTo 0
                Exit Property
Buyer_Error:
                Err.Raise(Err.Number, "cKampanj: BuyerPhone", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : BuyerID
        ' DateTime  : 2007-02-05 13:48
        ' Author    : KrKu
        ' Purpose   : Returns/sets the ID of the responsible Buyer for the campaign
        '---------------------------------------------------------------------------------------
        '
        Public Property BuyerID() As Integer
            Get
                On Error GoTo Buyer_Error
                BuyerID = mvarBuyerID
                On Error GoTo 0
                Exit Property
Buyer_Error:
                Err.Raise(Err.Number, "cKampanj: BuyerID", Err.Description)
            End Get
            Set(ByVal Value As Integer)
                On Error GoTo Buyer_Error
                mvarBuyerID = Value
                On Error GoTo 0
                Exit Property
Buyer_Error:
                Err.Raise(Err.Number, "cKampanj: BuyerID", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : StartDate
        ' DateTime  : 2003-06-11 13:49
        ' Author    : joho
        ' Purpose   : Returns the starting date of the campaign
        '---------------------------------------------------------------------------------------
        '
        Public ReadOnly Property StartDate() As Integer
            Get
                On Error GoTo StartDate_Error
                If mvarChannels(1) Is Nothing OrElse mvarChannels(1).BookingTypes(1).Weeks.Count = 0 Then
                    Return 0
                Else
                    Return mvarChannels(1).BookingTypes(1).Weeks(1).StartDate
                End If
                On Error GoTo 0
                Exit Property

StartDate_Error:
                Err.Raise(Err.Number, "cKampanj: StartDate", Err.Description)
            End Get
        End Property



        '---------------------------------------------------------------------------------------
        ' Procedure : EndDate
        ' DateTime  : 2003-06-11 13:49
        ' Author    : joho
        ' Purpose   : Returns the ending date of the campaign
        '---------------------------------------------------------------------------------------
        '
        Public ReadOnly Property EndDate() As Integer
            Get
                On Error GoTo EndDate_Error
                If mvarChannels(1) Is Nothing OrElse mvarChannels(1).BookingTypes(1).Weeks.Count = 0 Then
                    Return 0
                Else
                    EndDate = mvarChannels(1).BookingTypes(1).Weeks(CInt(mvarChannels(1).BookingTypes(1).Weeks.Count)).EndDate
                End If
                On Error GoTo 0
                Exit Property
EndDate_Error:
                Err.Raise(Err.Number, "cKampanj: EndDate", Err.Description)
            End Get
        End Property




        '---------------------------------------------------------------------------------------
        ' Procedure : UpdatedTo
        ' DateTime  : 2003-06-11 13:53
        ' Author    : joho
        ' Purpose   : Returns/sets the date that was latest read as Actual
        '---------------------------------------------------------------------------------------
        '
        Public Property UpdatedTo() As Integer
            Get
                On Error GoTo UpdatedTo_Error
                If mvarUpdatedTo > 0 Then
                    UpdatedTo = mvarUpdatedTo
                Else
                    UpdatedTo = StartDate - 1
                End If
                On Error GoTo 0
                Exit Property
UpdatedTo_Error:
                Err.Raise(Err.Number, "cKampanj: UpdatedTo", Err.Description)
            End Get
            Set(ByVal Value As Integer)
                On Error GoTo UpdatedTo_Error
                mvarUpdatedTo = Value
                On Error GoTo 0
                Exit Property
UpdatedTo_Error:
                Err.Raise(Err.Number, "cKampanj: UpdatedTo", Err.Description)
            End Set
        End Property




        '---------------------------------------------------------------------------------------
        ' Procedure : MainTarget
        ' DateTime  : 2003-06-11 13:53
        ' Author    : joho
        ' Purpose   : Returns/sets the main target for the campaign.
        '---------------------------------------------------------------------------------------
        '
        Public Property MainTarget() As cTarget
            Get
                On Error GoTo MainTarget_Error
                MainTarget = mvarMainTarget
                On Error GoTo 0
                Exit Property
MainTarget_Error:
                Err.Raise(Err.Number, "cKampanj: MainTarget", Err.Description)
            End Get
            Set(ByVal Value As cTarget)
                On Error GoTo MainTarget_Error
                mvarMainTarget = Value

                'MainTargetDelegate.DynamicInvoke()
                On Error GoTo 0
                Exit Property
MainTarget_Error:
                Err.Raise(Err.Number, "cKampanj: MainTarget", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : SecondaryTarget
        ' DateTime  : 2003-06-11 13:53
        ' Author    : joho
        ' Purpose   : Sets/returns the secondary target for the campaign
        '---------------------------------------------------------------------------------------
        '
        Public ReadOnly Property SecondaryTarget() As cTarget
            Get
                On Error GoTo SecondaryTarget_Error
                SecondaryTarget = mvarSecondaryTarget
                On Error GoTo 0
                Exit Property
SecondaryTarget_Error:
                Err.Raise(Err.Number, "cKampanj: SecondaryTarget", Err.Description)
            End Get
            '            Set(ByVal Value As cTarget)
            '                On Error GoTo SecondaryTarget_Error
            '                mvarSecondaryTarget = Value

            '                'SecondaryTargetDelegate.DynamicInvoke()
            '                On Error GoTo 0
            '                Exit Property
            'SecondaryTarget_Error:
            '                Err.Raise(Err.Number, "cKampanj: SecondaryTarget", Err.Description)
            '            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : ThirdTarget
        ' DateTime  : 2003-06-11 13:53
        ' Author    : joho
        ' Purpose   : Returns/sets the tertiary target of the campaign
        '---------------------------------------------------------------------------------------
        '
        Public Property ThirdTarget() As cTarget
            Get
                On Error GoTo ThirdTarget_Error
                ThirdTarget = mvarThirdTarget
                On Error GoTo 0
                Exit Property
ThirdTarget_Error:
                Err.Raise(Err.Number, "cKampanj: ThirdTarget", Err.Description)
            End Get
            Set(ByVal Value As cTarget)
                On Error GoTo ThirdTarget_Error
                mvarThirdTarget = Value

                'ThirdTargetDelegate.DynamicInvoke()
                On Error GoTo 0
                Exit Property
ThirdTarget_Error:
                Err.Raise(Err.Number, "cKampanj: ThirdTarget", Err.Description)
            End Set
        End Property

        '---------------------------------------------------------------------------------------
        ' Procedure : CustomTarget
        ' DateTime  : 2003-06-11 13:53
        ' Author    : joho
        ' Purpose   : Returns/sets the Custom target for the campaign.
        '---------------------------------------------------------------------------------------
        '
        Public Property CustomTarget() As cTarget
            Get
                On Error GoTo CustomTarget_Error
                CustomTarget = mvarCustomTarget
                On Error GoTo 0
                Exit Property
CustomTarget_Error:
                Err.Raise(Err.Number, "cKampanj: CustomTarget", Err.Description)
            End Get
            Set(ByVal Value As cTarget)
                On Error GoTo CustomTarget_Error
                mvarCustomTarget = Value
                On Error GoTo 0
                Exit Property
CustomTarget_Error:
                Err.Raise(Err.Number, "cKampanj: CustomTarget", Err.Description)
            End Set
        End Property

        '---------------------------------------------------------------------------------------
        ' Procedure : ActualSpots
        ' DateTime  : 2003-06-11 14:54
        ' Author    : joho
        ' Purpose   : Property to access the Dictionary of cAcualSpot items.
        '---------------------------------------------------------------------------------------
        '
        Public Property ActualSpots() As cActualSpots
            Get
                On Error GoTo ActualSpots_Error
                ActualSpots = mvarActualSpots
                On Error GoTo 0
                Exit Property
ActualSpots_Error:
                Err.Raise(Err.Number, "cKampanj: ActualSpots", Err.Description)
            End Get
            Set(ByVal Value As cActualSpots)
                On Error GoTo ActualSpots_Error
                mvarActualSpots = Value
                On Error GoTo 0
                Exit Property
ActualSpots_Error:
                Err.Raise(Err.Number, "cKampanj: ActualSpots", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : PlannedSpots
        ' DateTime  : 2003-06-11 14:54
        ' Author    : joho
        ' Purpose   : Property to access the Dictionary of cPlannedSpot items
        '---------------------------------------------------------------------------------------
        '
        Public Property PlannedSpots() As cPlannedSpots
            Get
                On Error GoTo PlannedSpots_Error
                PlannedSpots = mvarPlannedSpots
                On Error GoTo 0
                Exit Property
PlannedSpots_Error:
                Err.Raise(Err.Number, "cKampanj: PlannedSpots", Err.Description)
            End Get
            Set(ByVal Value As cPlannedSpots)
                On Error GoTo PlannedSpots_Error
                mvarPlannedSpots = Value
                mvarPlannedSpots.MainObject = Me
                On Error GoTo 0
                Exit Property
PlannedSpots_Error:
                Err.Raise(Err.Number, "cKampanj: PlannedSpots", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : FreqencyFocus
        ' DateTime  : 2003-06-11 14:54
        ' Author    : joho
        ' Purpose   : Returns/sets the frequency level on wich the campaign is optimized
        '---------------------------------------------------------------------------------------
        '
        Public Property FrequencyFocus() As Byte
            Get
                On Error GoTo FreqencyFocus_Error
                FrequencyFocus = mvarFrequencyFocus
                On Error GoTo 0
                Exit Property
FreqencyFocus_Error:
                Err.Raise(Err.Number, "cKampanj: FreqencyFocus", Err.Description)
            End Get
            Set(ByVal Value As Byte)
                On Error GoTo FreqencyFocus_Error
                mvarFrequencyFocus = Value
                On Error GoTo 0
                Exit Property
FreqencyFocus_Error:
                Err.Raise(Err.Number, "cKampanj: FreqencyFocus", Err.Description)
            End Set
        End Property

        Private _weeklyFrequency As Byte = 1
        ''' <summary>
        ''' Gets or sets the frequency use when calculating weekly reach.
        ''' </summary>
        ''' <value>
        ''' The weekly frequency.
        ''' </value>
        Public Property WeeklyFrequency() As Byte
            Get
                Return _weeklyFrequency
            End Get
            Set(ByVal value As Byte)
                _weeklyFrequency = value
            End Set
        End Property



        '---------------------------------------------------------------------------------------
        ' Procedure : Filename
        ' DateTime  : 2003-06-11 14:58
        ' Author    : joho
        ' Purpose   : Returns the last filename used to refernce the campaign.
        '             Is set by LoadCampaign or SaveCampaign
        '---------------------------------------------------------------------------------------
        '
        Public ReadOnly Property Filename() As String
            Get
                On Error GoTo Filename_Error
                Filename = mvarFilename
                On Error GoTo 0
                Exit Property
Filename_Error:
                Err.Raise(Err.Number, "cKampanj: Filename", Err.Description)
            End Get
        End Property



        '---------------------------------------------------------------------------------------
        ' Procedure : Product
        ' DateTime  : 2003-06-11 14:59
        ' Author    : joho
        ' Purpose   : Return/sets the name of the product for the campaign
        '---------------------------------------------------------------------------------------
        '
        Public ReadOnly Property Product() As String
            Get
                On Error GoTo Product_Error
                Product = mvarProduct
                On Error GoTo 0
                Exit Property
Product_Error:
                Err.Raise(Err.Number, "cKampanj: Product", Err.Description)
            End Get
        End Property

        Public ReadOnly Property AdEdgeProducts() As List(Of String)
            Get
                Return mvarAdEdgeProducts
            End Get
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : Client
        ' DateTime  : 2003-06-11 15:00
        ' Author    : joho
        ' Purpose   : Returns/sets the name of the client for the campaign
        '---------------------------------------------------------------------------------------
        '
        Public ReadOnly Property Client() As String
            Get
                On Error GoTo Client_Error
                Client = mvarClient
                On Error GoTo 0
                Exit Property
Client_Error:
                Err.Raise(Err.Number, "cKampanj: Client", Err.Description)
            End Get
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : BudgetTotalCTC
        ' DateTime  : 2003-06-11 15:00
        ' Author    : joho
        ' Purpose   : Returns/sets the maximum budget with all fees included
        '---------------------------------------------------------------------------------------
        '
        Public Property BudgetTotalCTC() As Decimal
            Get
                On Error GoTo BudgetTotalCTC_Error
                BudgetTotalCTC = mvarBudgetTotalCTC
                On Error GoTo 0
                Exit Property
BudgetTotalCTC_Error:
                Err.Raise(Err.Number, "cKampanj: BudgetTotalCTC", Err.Description)
            End Get
            Set(ByVal Value As Decimal)
                On Error GoTo BudgetTotalCTC_Error
                mvarBudgetTotalCTC = Value
                On Error GoTo 0
                Exit Property
BudgetTotalCTC_Error:
                Err.Raise(Err.Number, "cKampanj: BudgetTotalCTC", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : Commentary
        ' DateTime  : 2003-06-12 13:46
        ' Author    : joho
        ' Purpose   : Returns/sets a commentary that can be anything the user wants
        '---------------------------------------------------------------------------------------
        '
        Public Property Commentary() As String
            Get
                On Error GoTo Commentary_Error
                Commentary = mvarCommentary
                On Error GoTo 0
                Exit Property
Commentary_Error:
                Err.Raise(Err.Number, "cKampanj: Commentary", Err.Description)
            End Get
            Set(ByVal Value As String)
                On Error GoTo Commentary_Error
                mvarCommentary = Value
                On Error GoTo 0
                Exit Property
Commentary_Error:
                Err.Raise(Err.Number, "cKampanj: Commentary", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : ControlFilmcodeFromClient
        ' DateTime  : 2003-06-12 14:00
        ' Author    : joho
        ' Purpose   : Returns/sets wether the filmcodes have been received from the client
        '---------------------------------------------------------------------------------------
        '
        Public Property ControlFilmcodeFromClient() As Boolean
            Get
                On Error GoTo ControlFilmcodeFromClient_Error
                ControlFilmcodeFromClient = mvarControlFilmcodeFromClient
                On Error GoTo 0
                Exit Property
ControlFilmcodeFromClient_Error:
                Err.Raise(Err.Number, "cKampanj: ControlFilmcodeFromClient", Err.Description)
            End Get
            Set(ByVal Value As Boolean)
                On Error GoTo ControlFilmcodeFromClient_Error
                mvarControlFilmcodeFromClient = Value
                On Error GoTo 0
                Exit Property
ControlFilmcodeFromClient_Error:
                Err.Raise(Err.Number, "cKampanj: ControlFilmcodeFromClient", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : ControlFilmcodeToBureau
        ' DateTime  : 2003-06-12 14:00
        ' Author    : joho
        ' Purpose   : Return/sets wether the filmcode has been sent to the creative bureau
        '---------------------------------------------------------------------------------------
        '
        Public Property ControlFilmcodeToBureau() As Boolean
            Get
                On Error GoTo ControlFilmcodeToBureau_Error
                ControlFilmcodeToBureau = mvarControlFilmcodeToBureau
                On Error GoTo 0
                Exit Property
ControlFilmcodeToBureau_Error:
                Err.Raise(Err.Number, "cKampanj: ControlFilmcodeToBureau", Err.Description)
            End Get
            Set(ByVal Value As Boolean)
                On Error GoTo ControlFilmcodeToBureau_Error
                mvarControlFilmcodeToBureau = Value
                On Error GoTo 0
                Exit Property
ControlFilmcodeToBureau_Error:
                Err.Raise(Err.Number, "cKampanj: ControlFilmcodeToBureau", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : ControlFilmcodeToChannels
        ' DateTime  : 2003-06-12 14:00
        ' Author    : joho
        ' Purpose   : Returns/sets wether the filmcode has been sent to the channels
        '---------------------------------------------------------------------------------------
        '
        Public Property ControlFilmcodeToChannels() As Boolean
            Get
                On Error GoTo ControlFilmcodeToChannels_Error
                ControlFilmcodeToChannels = mvarControlFilmcodeToChannels
                On Error GoTo 0
                Exit Property
ControlFilmcodeToChannels_Error:
                Err.Raise(Err.Number, "cKampanj: ControlFilmcodeToChannels", Err.Description)
            End Get
            Set(ByVal Value As Boolean)
                On Error GoTo ControlFilmcodeToChannels_Error
                mvarControlFilmcodeToChannels = Value
                On Error GoTo 0
                Exit Property
ControlFilmcodeToChannels_Error:
                Err.Raise(Err.Number, "cKampanj: ControlFilmcodeToChannels", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : ControlTracking
        ' DateTime  : 2003-06-12 14:00
        ' Author    : joho
        ' Purpose   : Returns/sets wether the tracking has been ordered
        '---------------------------------------------------------------------------------------
        '
        Public Property ControlTracking() As Boolean
            Get
                On Error GoTo ControlTracking_Error
                ControlTracking = mvarControlTracking
                On Error GoTo 0
                Exit Property
ControlTracking_Error:
                Err.Raise(Err.Number, "cKampanj: ControlTracking", Err.Description)
            End Get
            Set(ByVal Value As Boolean)
                On Error GoTo ControlTracking_Error
                mvarControlTracking = Value
                On Error GoTo 0
                Exit Property
ControlTracking_Error:
                Err.Raise(Err.Number, "cKampanj: ControlTracking", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : ControlFollowedUp
        ' DateTime  : 2003-06-12 14:00
        ' Author    : joho
        ' Purpose   : Returns/sets wether the campaign has been followed up
        '---------------------------------------------------------------------------------------
        '
        Public Property ControlFollowedUp() As Boolean
            Get
                On Error GoTo ControlFollowedUp_Error
                ControlFollowedUp = mvarControlFollowedUp
                On Error GoTo 0
                Exit Property
ControlFollowedUp_Error:
                Err.Raise(Err.Number, "cKampanj: ControlFollowedUp", Err.Description)
            End Get
            Set(ByVal Value As Boolean)
                On Error GoTo ControlFollowedUp_Error
                mvarControlFollowedUp = Value
                On Error GoTo 0
                Exit Property
ControlFollowedUp_Error:
                Err.Raise(Err.Number, "cKampanj: ControlFollowedUp", Err.Description)
            End Set
        End Property



        '---------------------------------------------------------------------------------------
        ' Procedure : ControlFollowUpToClient
        ' DateTime  : 2003-06-12 14:41
        ' Author    : joho
        ' Purpose   : Returns/set wether the follow-up has been sent to the client
        '---------------------------------------------------------------------------------------
        '
        Public Property ControlFollowUpToClient() As Boolean
            Get
                On Error GoTo ControlFollowUpToClient_Error
                ControlFollowUpToClient = mvarControlFollowUpToClient
                On Error GoTo 0
                Exit Property
ControlFollowUpToClient_Error:
                Err.Raise(Err.Number, "cKampanj: ControlFollowUpToClient", Err.Description)
            End Get
            Set(ByVal Value As Boolean)
                On Error GoTo ControlFollowUpToClient_Error
                mvarControlFollowUpToClient = Value
                On Error GoTo 0
                Exit Property
ControlFollowUpToClient_Error:
                Err.Raise(Err.Number, "cKampanj: ControlFollowUpToClient", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : ControlTransferredToMatrix
        ' DateTime  : 2003-06-12 14:41
        ' Author    : joho
        ' Purpose   : Returns/sets wether the campaign has been exported to Matrix
        '---------------------------------------------------------------------------------------
        '
        Public Property ControlTransferredToMatrix() As Boolean
            Get
                On Error GoTo ControlTransferredToMatrix_Error
                ControlTransferredToMatrix = mvarControlTransferredToMatrix
                On Error GoTo 0
                Exit Property
ControlTransferredToMatrix_Error:
                Err.Raise(Err.Number, "cKampanj: ControlTransferredToMatrix", Err.Description)
            End Get
            Set(ByVal Value As Boolean)
                On Error GoTo ControlTransferredToMatrix_Error
                mvarControlTransferredToMatrix = Value
                On Error GoTo 0
                Exit Property
ControlTransferredToMatrix_Error:
                Err.Raise(Err.Number, "cKampanj: ControlTransferredToMatrix", Err.Description)
            End Set
        End Property

        Public Property MultiplyAddedValues() As Boolean
            Get
                Return _multiplyAddedValues
            End Get
            Set(ByVal value As Boolean)
                _multiplyAddedValues = value
            End Set
        End Property

        '---------------------------------------------------------------------------------------
        ' Procedure : Area
        ' DateTime  : 2003-06-12 14:42
        ' Author    : joho
        ' Purpose   : Returns/sets the two letter abbrevation for the Area in which the
        '             campaign is created.
        '
        '             Examples:
        '                       SE - Sweden
        '                       NO - Norway
        '                       DK - Denmark
        '---------------------------------------------------------------------------------------
        '
        Public Property Area() As String
            Get
                On Error GoTo Area_Error
                Area = mvarArea
                On Error GoTo 0
                Exit Property
Area_Error:
                Err.Raise(Err.Number, "cKampanj: Area", Err.Description)
            End Get
            Set(ByVal Value As String)
                Dim DataPath As Object
                'UPGRADE_ISSUE: clsINI object was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
                Dim Ini As New clsIni
                Dim i As Short
                Dim en As Integer
                Dim ed As String
                On Error GoTo Area_Error
                Helper.WriteToLogFile("Set Area: Set String")
                mvarArea = Trim(Value)
                Helper.WriteToLogFile("Set Area: Set Adedge.Area")
                Adedge.setArea(Value)
                If My.Computer.FileSystem.FileExists(TrinitySettings.ActiveDataPath & mvarArea & "\Area.ini") Then
                    Helper.WriteToLogFile("Set Area: Open IniFile (" & TrinitySettings.ActiveDataPath & mvarArea & "\Area.ini)")
                    Ini.Create(TrinitySettings.ActiveDataPath & mvarArea & "\Area.ini")
                Else
                    Helper.WriteToLogFile("Set Area: Open IniFile (" & Helper.Pathify(TrinitySettings.DataPath) & mvarArea & "\Area.ini)")
                    Ini.Create(Helper.Pathify(TrinitySettings.DataPath) & mvarArea & "\Area.ini")
                End If
                Helper.WriteToLogFile("Set Area: Get DaypartCount")
                If Ini.Data("Dayparts", "Count") < 0 Or Ini.Data("Universes", "Count") < 0 Then
                    Err.Raise(13, , "Area.ini for area """ & mvarArea & """ is corrupted or does not exist.")
                End If

                'Read new daypart definition

                Dim XMLDaypartsDoc As New Xml.XmlDocument
                If IO.File.Exists(TrinitySettings.ActiveDataPath & mvarArea & "\Dayparts.xml") Then
                    XMLDaypartsDoc.Load(TrinitySettings.ActiveDataPath & mvarArea & "\Dayparts.xml")
                End If

                Dim XmlDayparts As Xml.XmlElement = XMLDaypartsDoc.SelectSingleNode("Dayparts")

                _dayparts.Clear()
                If XmlDayparts IsNot Nothing Then
                    For Each xmlDaypart As Xml.XmlElement In XmlDayparts.SelectSingleNode("Campaign").ChildNodes
                        Dim _dp As New Trinity.cDaypart
                        _dp.Name = xmlDaypart.GetAttribute("Name")
                        _dp.StartMaM = xmlDaypart.GetAttribute("StartMaM")
                        _dp.EndMaM = xmlDaypart.GetAttribute("EndMaM")
                        _dp.IsPrime = xmlDaypart.GetAttribute("IsPrime")
                        _dayparts.Add(_dp)
                    Next
                End If

                If _dayparts.Count = 0 Then

                    'New Daypart definition not present. Read old definition and translate to new implementation
                    Dayparts = TrinitySettings.DefaultDayparts(Me)

                End If

                mvarUniverses = New Collections.Specialized.NameValueCollection
                For i = 1 To Ini.Data("Universes", "Count")
                    Universes.Add(Ini.Text("Universes", "AdedgeUni" & i), Ini.Text("Universes", "Uni" & i))
                Next
                _multiplyAddedValues = Not Ini.Data("General", "AddAddedValues")
                DefaultSecondUniverse = Nothing

                On Error GoTo 0
                Exit Property
Area_Error:
                ed = Err.Description
                en = Err.Number
                Helper.WriteToLogFile("ERROR: " & ed)
                Err.Raise(en, "cKampanj: Area", ed)
            End Set
        End Property



        '---------------------------------------------------------------------------------------
        ' Procedure : AreaLog
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   : Set/get additional country settings
        '---------------------------------------------------------------------------------------
        '
        Public Property AreaLog() As String
            Get
                On Error GoTo AreaLog_Error
                AreaLog = mvarAreaLog
                On Error GoTo 0
                Exit Property
AreaLog_Error:
                Err.Raise(Err.Number, "cKampanj: AreaLog", Err.Description)
            End Get
            Set(ByVal Value As String)
                On Error GoTo AreaLog_Error
                mvarAreaLog = Value
                Adedge.setBrandFilmCode(Value, "")
                On Error GoTo 0
                Exit Property
AreaLog_Error:
                Err.Raise(Err.Number, "cKampanj: AreaLog", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : ActualTotCTC
        ' DateTime  : 2003-06-12 14:59
        ' Author    : joho
        ' Purpose   : Returns the calculated actual cost to client
        '---------------------------------------------------------------------------------------
        '
        Public ReadOnly Property ActualTotCTC() As Decimal
            Get
                On Error GoTo ActualTotCTC_Error
                ActualTotCTC = mvarActualTotCTC
                On Error GoTo 0
                Exit Property
ActualTotCTC_Error:
                Err.Raise(Err.Number, "cKampanj: ActualTotCTC", Err.Description)
            End Get
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : ControlOKFromClient
        ' DateTime  : 2003-07-02 13:50
        ' Author    : joho
        ' Purpose   : Returns/sets wether he campaign has gotten an OK from the client
        '---------------------------------------------------------------------------------------
        '
        Public Property ControlOKFromClient() As Boolean
            Get
                On Error GoTo ControlOKFromClient_Error
                ControlOKFromClient = mvarControlOKFromClient
                On Error GoTo 0
                Exit Property
ControlOKFromClient_Error:
                Err.Raise(Err.Number, "cKampanj: ControlOKFromClient", Err.Description)
            End Get
            Set(ByVal Value As Boolean)
                On Error GoTo ControlOKFromClient_Error
                mvarControlOKFromClient = Value
                On Error GoTo 0
                Exit Property
ControlOKFromClient_Error:
                Err.Raise(Err.Number, "cKampanj: ControlOKFromClient", Err.Description)
            End Set
        End Property


        Public Property Adedge() As ConnectWrapper.Brands
            Get
                Adedge = mvarAdedge
            End Get
            Set(ByVal Value As ConnectWrapper.Brands)
                mvarAdedge = Value
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : PlannedTotCTC
        ' DateTime  : 2003-08-21 16:18
        ' Author    : joho
        ' Purpose   : Returns the planned CTC
        '---------------------------------------------------------------------------------------
        '
        Public Property PlannedTotCTC() As Decimal
            Get
                Dim CostTypeFixed As Object
                Dim CostTypePerUnit As Object
                Dim CostOnUnitEnum As Object
                Dim CostTypePercent As Object
                Dim CostOnPercentEnum As Object
                Dim Campaign As Object

                On Error GoTo PlannedTotCTC_Error

                Dim TmpChan As cChannel
                Dim TmpBT As cBookingType
                Dim TmpWeek As cWeek
                Dim TmpCost As cCost
                Dim TotCost As Decimal
                Dim TotCommission As Decimal
                Dim TmpPlannedTotCTC As Decimal
                Dim TmpPlannedTotGross As Decimal

                For Each TmpChan In mvarChannels
                    For Each TmpBT In TmpChan.BookingTypes
                        If TmpBT.BookIt AndAlso Not TmpBT.IsCompensation Then
                            For Each TmpWeek In TmpBT.Weeks
                                TmpPlannedTotCTC = TmpPlannedTotCTC + TmpWeek.NetBudget
                                TmpPlannedTotGross += TmpWeek.GrossBudget
                                TotCommission = TotCommission + TmpWeek.NetBudget * TmpChan.AgencyCommission
                            Next TmpWeek
                        End If
                    Next TmpBT
                Next TmpChan
                TotCost = 0
                For Each TmpCost In mvarCosts
                    If TmpCost.CostType = cCost.CostTypeEnum.CostTypePercent Then
                        If TmpCost.CountCostOn = cCost.CostOnPercentEnum.CostOnMediaNet Then
                            TotCost = TotCost + TmpPlannedTotCTC * TmpCost.Amount
                        End If
                    End If
                Next TmpCost
                TmpPlannedTotCTC = TmpPlannedTotCTC + TotCost - TotCommission
                TotCost = 0
                For Each TmpCost In mvarCosts
                    If TmpCost.CostType = cCost.CostTypeEnum.CostTypePercent Then
                        If TmpCost.CountCostOn = cCost.CostOnPercentEnum.CostOnNet Then
                            TotCost = TotCost + TmpPlannedTotCTC * TmpCost.Amount
                        ElseIf TmpCost.CountCostOn = cCost.CostOnPercentEnum.CostOnRatecard Then
                            TotCost += TmpPlannedTotGross * TmpCost.Amount
                        End If
                    End If
                Next TmpCost
                TmpPlannedTotCTC = TmpPlannedTotCTC + TotCost

                For Each TmpCost In mvarCosts
                    If TmpCost.CostType = cCost.CostTypeEnum.CostTypeFixed Then
                        TmpPlannedTotCTC = TmpPlannedTotCTC + TmpCost.Amount
                    ElseIf TmpCost.CostType = cCost.CostTypeEnum.CostTypePerUnit Then
                        If TmpCost.CountCostOn = cCost.CostOnUnitEnum.CostOnSpots Then
                            For Each TmpChan In mvarChannels
                                For Each TmpBT In TmpChan.BookingTypes
                                    If TmpBT.BookIt Then
                                        TmpPlannedTotCTC = TmpPlannedTotCTC + TmpBT.EstimatedSpotCount * TmpCost.Amount
                                    End If
                                Next TmpBT
                            Next TmpChan
                        ElseIf TmpCost.CountCostOn = cCost.CostOnUnitEnum.CostOnBuyingTRP Then
                            For Each TmpChan In mvarChannels
                                For Each TmpBT In TmpChan.BookingTypes
                                    If TmpBT.BookIt Then
                                        TmpPlannedTotCTC = TmpPlannedTotCTC + TmpBT.TotalTRPBuyingTarget * TmpCost.Amount
                                    End If
                                Next TmpBT
                            Next TmpChan
                        ElseIf TmpCost.CountCostOn = cCost.CostOnUnitEnum.CostOnMainTRP Then
                            For Each TmpChan In mvarChannels
                                For Each TmpBT In TmpChan.BookingTypes
                                    If TmpBT.BookIt Then
                                        TmpPlannedTotCTC += TmpBT.TotalTRP * TmpCost.Amount
                                    End If
                                Next TmpBT
                            Next TmpChan
                        ElseIf TmpCost.CountCostOn = cCost.CostOnUnitEnum.CostOnWeeks Then
                            If Channels.Count > 0 AndAlso Channels(1).BookingTypes.Count > 0 Then
                                TmpPlannedTotCTC += TmpCost.Amount * Channels(1).BookingTypes(1).Weeks.Count
                            End If
                        End If
                    ElseIf TmpCost.CostType = cCost.CostTypeEnum.CostTypeOnDiscount Then
                        If TmpCost.CountCostOn Is Nothing Then
                            For Each TmpChan In mvarChannels
                                For Each TmpBT In TmpChan.BookingTypes
                                    If TmpBT.BookIt AndAlso Not TmpBT.IsCompensation Then
                                        For Each TmpWeek In TmpBT.Weeks
                                            Dim Discount As Single = TmpWeek.GrossBudget - TmpWeek.NetBudget
                                            TmpPlannedTotCTC += (Discount * TmpCost.Amount)
                                        Next
                                    End If
                                Next
                            Next
                        Else
                            For Each TmpBT In DirectCast(TmpCost.CountCostOn, cChannel).BookingTypes
                                If TmpBT.BookIt AndAlso Not TmpBT.IsCompensation Then
                                    For Each TmpWeek In TmpBT.Weeks
                                        Dim Discount As Single = TmpWeek.GrossBudget - TmpWeek.NetBudget
                                        TmpPlannedTotCTC += (Discount * TmpCost.Amount)
                                    Next
                                End If
                            Next
                        End If
                    End If
                Next TmpCost
                TotCost = 0
                For Each TmpCost In mvarCosts
                    If TmpCost.CostType = cCost.CostTypeEnum.CostTypePercent Then
                        If TmpCost.CountCostOn = cCost.CostOnPercentEnum.CostOnNetNet Then
                            TotCost = TotCost + TmpPlannedTotCTC * TmpCost.Amount
                        End If
                    End If
                Next TmpCost
                TmpPlannedTotCTC = TmpPlannedTotCTC + TotCost

                PlannedTotCTC = Math.Round(TmpPlannedTotCTC)
                On Error GoTo 0
                Exit Property

PlannedTotCTC_Error:
                Err.Raise(Err.Number, "cKampanj: PlannedTotCTC", Err.Description)
            End Get
            Set(ByVal value As Decimal)
                'FindCTC(value, 10000) the old funktion

                setCTC(value, 10000)
            End Set
        End Property

        Private Function setCTC(ByVal CTC As Decimal, ByVal MoneyStep As Decimal, Optional ByVal Flips As Integer = 0, Optional ByVal LastDirectionWasUp As Boolean = True) As Boolean
            If MoneyStep < 1 Then Return True
            If CTC = Math.Round(PlannedTotCTC, 0) Then Return True

            If CTC > Math.Round(PlannedTotCTC, 0) Then
                If Not LastDirectionWasUp Then
                    Flips += 1
                End If
                AddNetBudget(MoneyStep)
                If Math.Abs(CTC - PlannedTotCTC) < MoneyStep OrElse Flips >= 10 Then
                    MoneyStep /= 2
                    Flips = 0
                End If
                If setCTC(CTC, MoneyStep, Flips, True) Then
                    Return True
                Else
                    MoneyStep /= 2
                    Return setCTC(CTC, MoneyStep, Flips, True)
                End If
            Else
                If LastDirectionWasUp Then
                    Flips += 1
                End If
                AddNetBudget(-MoneyStep)
                If Math.Abs(CTC - PlannedTotCTC) < MoneyStep OrElse Flips >= 10 Then
                    MoneyStep /= 2
                    Flips = 0
                End If
                If setCTC(CTC, MoneyStep, Flips, False) Then
                    Return True
                Else
                    MoneyStep /= 2
                    Return setCTC(CTC, MoneyStep, Flips, False)
                End If
            End If

        End Function

        '**** The old function, it was replaced by setCTC
        'Private Sub FindCTC(ByVal CTC As Decimal, ByVal MoneyStep As Decimal)
        '    If MoneyStep < 1 Then Exit Sub
        '    Try
        '        If CTC > Math.Round(PlannedTotCTC, 0) Then
        '            AddNetBudget(MoneyStep)
        '            While CTC > Math.Round(PlannedTotCTC)
        '                FindCTC(CTC, MoneyStep)
        '            End While
        '            MoneyStep /= 2
        '            FindCTC(CTC, MoneyStep)
        '        ElseIf CTC < Math.Round(PlannedTotCTC, 0) Then
        '            AddNetBudget(-MoneyStep)
        '            While CTC < Math.Round(PlannedTotCTC, 0)
        '                FindCTC(CTC, MoneyStep)
        '            End While
        '            MoneyStep /= 2
        '            FindCTC(CTC, MoneyStep)
        '        End If
        '        Exit Sub
        '    Catch ex As Exception
        '        Throw ex
        '    End Try

        'End Sub

        Private Sub AddNetBudget(ByVal Budget As Decimal)
            Dim BudgetSum As Decimal = 0
            For Each TmpChan As Trinity.cChannel In mvarChannels
                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                            BudgetSum += TmpWeek.NetBudget
                        Next
                    End If
                Next
            Next
            If BudgetSum = 0 Then
                'Err.Raise(vbObjectError + 513, "Trinity.cKampanj", "You can not enter a budget sum without having a general budget distribution.")
                Throw New Exception("You can not enter a budget sum without having a general budget distribution.")
                Exit Sub
            End If
            For Each TmpChan As Trinity.cChannel In mvarChannels
                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                            TmpWeek.NetBudget = (BudgetSum + Budget) * (TmpWeek.NetBudget / BudgetSum)
                            TmpWeek.TRPControl = False
                        Next
                    End If
                Next
            Next

        End Sub

        '---------------------------------------------------------------------------------------
        ' Procedure : InternalAdedge
        ' DateTime  : 2003-09-04 13:35
        ' Author    : joho
        ' Purpose   : Instance of ConnectWrapper.Brands to be used on internal calculations
        '             within the campaign
        '---------------------------------------------------------------------------------------
        '
        Friend ReadOnly Property InternalAdedge() As ConnectWrapper.Brands
            Get
                On Error GoTo InternalAdedge_Error
                InternalAdedge = mvarInternalAdedge
                On Error GoTo 0
                Exit Property
InternalAdedge_Error:
                Err.Raise(Err.Number, "cKampanj: InternalAdedge", Err.Description)
            End Get
        End Property


        '        '---------------------------------------------------------------------------------------
        '        ' Procedure : DaypartName
        '        ' DateTime  : 2003-09-22 16:25
        '        ' Author    : joho
        '        ' Purpose   : Returns/sets the name of a daypart
        '        '---------------------------------------------------------------------------------------
        '        '
        '        Public Property DaypartName(ByVal Daypart As Object) As String
        '            Get
        '                On Error GoTo DaypartName_Error
        '                DaypartName = mvarDaypartName(Daypart)
        '                On Error GoTo 0
        '                Exit Property
        'DaypartName_Error:
        '                Err.Raise(Err.Number, "cKampanj: DaypartName", Err.Description)
        '            End Get
        '            Set(ByVal Value As String)

        '                On Error GoTo DaypartName_Error
        '                mvarDaypartName(Daypart) = Value
        '                On Error GoTo 0
        '                Exit Property
        'DaypartName_Error:
        '                Err.Raise(Err.Number, "cKampanj: DaypartName", Err.Description)
        '            End Set
        '        End Property


        '        '---------------------------------------------------------------------------------------
        '        ' Procedure : DaypartStart
        '        ' DateTime  : 2003-09-22 16:35
        '        ' Author    : joho
        '        ' Purpose   : Returns/sets what time a daypart starts
        '        '---------------------------------------------------------------------------------------
        '        '
        '        Public Property DaypartStart(ByVal Daypart As Object) As Short
        '            Get
        '                On Error GoTo DaypartStart_Error
        '                DaypartStart = mvarDaypartStart(Daypart)
        '                On Error GoTo 0
        '                Exit Property
        'DaypartStart_Error:
        '                Err.Raise(Err.Number, "cKampanj: DaypartStart", Err.Description)
        '            End Get
        '            Set(ByVal Value As Short)
        '                On Error GoTo DaypartStart_Error
        '                mvarDaypartStart(Daypart) = Value

        '                'update spots if dayparts changed
        '                If DaypartsDelegate IsNot Nothing Then
        '                    DaypartsDelegate.DynamicInvoke()
        '                End If
        '                On Error GoTo 0
        '                Exit Property
        'DaypartStart_Error:
        '                Err.Raise(Err.Number, "cKampanj: DaypartStart", Err.Description)
        '            End Set
        '        End Property


        '        '---------------------------------------------------------------------------------------
        '        ' Procedure : DaypartEnd
        '        ' DateTime  : 2003-09-22 16:35
        '        ' Author    : joho
        '        ' Purpose   : Returns/sets the time when a Dayparts ends
        '        '---------------------------------------------------------------------------------------
        '        '
        '        Public Property DaypartEnd(ByVal Daypart As Object) As Short
        '            Get
        '                On Error GoTo DaypartEnd_Error
        '                DaypartEnd = mvarDaypartEnd(Daypart)
        '                On Error GoTo 0
        '                Exit Property
        'DaypartEnd_Error:
        '                Err.Raise(Err.Number, "cKampanj: DaypartEnd", Err.Description)
        '            End Get
        '            Set(ByVal Value As Short)
        '                On Error GoTo DaypartEnd_Error
        '                mvarDaypartEnd(Daypart) = Value

        '                'update spots if dayparts changed
        '                If DaypartsDelegate IsNot Nothing Then
        '                    DaypartsDelegate.DynamicInvoke()
        '                End If
        '                On Error GoTo 0
        '                Exit Property
        'DaypartEnd_Error:
        '                Err.Raise(Err.Number, "cKampanj: DaypartEnd", Err.Description)
        '            End Set
        '        End Property

        Private _dayparts As New cDayparts(Me)
        Public Property Dayparts() As cDayparts
            Get
                Return _dayparts
            End Get
            Set(ByVal value As cDayparts)
                _dayparts = value
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : AllAdults
        ' DateTime  : 2003-09-23 13:43
        ' Author    : joho
        ' Purpose   : Returns/sets the AdEdge target mnemonic that represents the
        '             local definition of All Adults
        '---------------------------------------------------------------------------------------
        '
        Public Property AllAdults() As String
            Get
                On Error GoTo AllAdults_Error
                AllAdults = mvarAllAdults
                On Error GoTo 0
                Exit Property
AllAdults_Error:
                Err.Raise(Err.Number, "cKampanj: AllAdults", Err.Description)
            End Get
            Set(ByVal Value As String)
                On Error GoTo AllAdults_Error
                If Value = "" Then
                    mvarAllAdults = "3+"
                End If
                mvarAllAdults = Value
                On Error GoTo 0
                Exit Property
AllAdults_Error:
                Err.Raise(Err.Number, "cKampanj: AllAdults", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : TargColl
        ' DateTime  : 2003-10-07 16:27
        ' Author    : joho
        ' Purpose   : Function to return index for a target
        '---------------------------------------------------------------------------------------
        '
        Friend ReadOnly Property TargColl(ByVal Target As String, ByVal Adedge As Connect.Brands) As Short
            Get
                Dim i As Short
                Dim Temp As Short
                On Error GoTo TargColl_Error

                '    If mvarTargColl Is Nothing Then
                '        Set mvarTargColl = New Collection
                '    End If
                '    Set TargColl = mvarTargColl
                Target = Trim(Target)
                Temp = -1
                For i = 0 To Adedge.getTargetCount - 1
                    If Adedge.getTargetTitle(i) = Target Then
                        Temp = i
                        Exit For
                    End If
                    If Adedge.getTargetTitle(i) = "A" & Target Then
                        Temp = i
                        Exit For
                    End If
                Next
                If Temp = -1 Then
                    Return -1
                Else
                    Return Temp + 1
                End If
                On Error GoTo 0
                Exit Property
TargColl_Error:
                Err.Raise(Err.Number, "cKampanj: TargColl", Err.Description)
            End Get
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : UniColl
        ' DateTime  : 2003-10-07 16:28
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        '        Friend ReadOnly Property UniColl(ByVal Universe As String, Optional ByVal Adedge As Connect.Brands = Nothing) As Short
        '            Get
        '                If Universe = "" Then Return 1
        '                If Adedge Is Nothing Then Adedge = InternalAdedge
        '                Dim i As Short
        '                Dim Temp As Short
        '                On Error GoTo UniColl_Error

        '                Universe = Trim(Universe)
        '                Temp = -1
        '                For i = 0 To Adedge.getUniverseCount - 1
        '                    If Adedge.getUniverseTitle(i) = Universe Then
        '                        Temp = i
        '                        Exit For
        '                    End If
        '                Next
        '                If Temp = -1 Then
        '                    Return -1
        '                Else
        '                    Return Temp + 1
        '                End If
        '                On Error GoTo 0
        '                Exit Property
        'UniColl_Error:
        '                Err.Raise(Err.Number, "cKampanj: UniColl", Err.Description)
        '            End Get
        '        End Property

        Friend ReadOnly Property TargetCollection() As List(Of cTarget)
            Get
                Return _targcoll
            End Get
        End Property

        'Friend ReadOnly Property UniverseCollection() As List(Of String)
        '    Get
        '        Return _unicoll
        '    End Get
        'End Property

        '---------------------------------------------------------------------------------------
        ' Procedure : TargStr
        ' DateTime  : 2003-10-07 16:28
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        '        Friend Property TargStr() As String
        '            Get
        '                On Error GoTo TargStr_Error
        '                TargStr = mvarTargStr
        '                On Error GoTo 0
        '                Exit Property
        'TargStr_Error:
        '                Err.Raise(Err.Number, "cKampanj: TargStr", Err.Description)
        '            End Get
        '            Set(ByVal Value As String)
        '                On Error GoTo TargStr_Error
        '                mvarTargStr = Value
        '                On Error GoTo 0
        '                Exit Property
        'TargStr_Error:
        '                Err.Raise(Err.Number, "cKampanj: TargStr", Err.Description)
        '            End Set
        '        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : UniStr
        ' DateTime  : 2003-10-07 16:28
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        '        Friend Property UniStr() As String
        '            Get
        '                On Error GoTo UniStr_Error
        '                UniStr = mvarUniStr
        '                On Error GoTo 0
        '                Exit Property
        'UniStr_Error:
        '                Err.Raise(Err.Number, "cKampanj: UniStr", Err.Description)
        '            End Get
        '            Set(ByVal Value As String)
        '                On Error GoTo UniStr_Error
        '                mvarUniStr = Value
        '                On Error GoTo 0
        '                Exit Property
        'UniStr_Error:
        '                Err.Raise(Err.Number, "cKampanj: UniStr", Err.Description)
        '            End Set
        '        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : DebugPath
        ' DateTime  : 2003-11-13 13:17
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        Public Property DebugPath() As String
            Get
                On Error GoTo DebugPath_Error
                DebugPath = mvarDebugPath
                On Error GoTo 0
                Exit Property
DebugPath_Error:
                Err.Raise(Err.Number, "cKampanj: DebugPath", Err.Description)
            End Get
            Set(ByVal Value As String)
                On Error GoTo DebugPath_Error
                mvarDebugPath = Value
                If mvarDebugPath <> "" Then
                    FileClose(200)
                    FileOpen(200, mvarDebugPath, OpenMode.Output)
                Else
                    FileClose(200)
                End If
                On Error GoTo 0
                Exit Property
DebugPath_Error:
                Err.Raise(Err.Number, "cKampanj: DebugPath", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : BookedSpots
        ' DateTime  : 2003-12-30 11:38
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        Public Property BookedSpots() As cBookedSpots
            Get
                On Error GoTo BookedSpots_Error
                BookedSpots = mvarBookedSpots
                On Error GoTo 0
                Exit Property
BookedSpots_Error:
                Err.Raise(Err.Number, "cKampanj: BookedSpots", Err.Description)
            End Get
            Set(ByVal Value As cBookedSpots)
                On Error GoTo BookedSpots_Error
                mvarBookedSpots = Value
                On Error GoTo 0
                Exit Property
BookedSpots_Error:
                Err.Raise(Err.Number, "cKampanj: BookedSpots", Err.Description)
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : ActualSpotsCollection
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        Friend ReadOnly Property ActualSpotsCollection() As Dictionary(Of String, cActualSpot)
            Get
                ActualSpotsCollection = mvarActualSpots.Collection
            End Get
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : Costs
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        Public Property Costs() As cCosts
            Get
                Return mvarCosts
            End Get
            Set(ByVal Value As cCosts)
                mvarCosts = Value
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : Contract
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        Public Property Contract() As cContract
            Get
                Contract = mvarContract
            End Get
            Set(ByVal Value As cContract)
                mvarContract = Value
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
                ClientID = mvarClientID
            End Get
            Set(ByVal Value As Short)
                mvarClientID = Value
                If Value = 0 Then
                    mvarClient = ""
                Else
                    Try
                        mvarClient = DBReader.getClient(Value)
                    Catch
                        mvarClient = ""
                    End Try
                    'Dim com As New Odbc.OdbcCommand("SELECT Name FROM Clients WHERE id=" & Value, DBConn)
                    'mvarClient = com.ExecuteScalar
                End If
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
                ProductID = mvarProductID
            End Get
            Set(ByVal Value As Short)
                mvarProductID = Value
                If Value = 0 Then
                    mvarProduct = ""
                Else
                    Try
                        mvarProduct = DBReader.getProduct(Value)
                    Catch
                        mvarProduct = ""
                    End Try
                    'Dim com As New Odbc.OdbcCommand("SELECT Name FROM Products WHERE id=" & Value, DBConn)
                    'mvarProduct = com.ExecuteScalar
                End If
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : MarathonOtherOrder
        ' DateTime  : 2006-03-31 11:51
        ' Author    : joho
        ' Purpose   : Stores the Order number for the channel containing
        '             costs that are not channel related
        '---------------------------------------------------------------------------------------
        '
        Public Property MarathonOtherOrder() As Integer
            Get
                MarathonOtherOrder = mvarMarathonOtherOrder
            End Get
            Set(ByVal Value As Integer)
                mvarMarathonOtherOrder = Value
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : SaveCampaignToHistory
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   : Saves a campaign to history for backward reference
        '---------------------------------------------------------------------------------------
        '
        Public Sub SaveCampaignToHistory(Optional ByRef Comment As String = "")
            Dim TmpCampaign As New cKampanj(False)

            TmpCampaign.LoadCampaign("", True, SaveCampaign("", True, True, True))
            TmpCampaign.ID = CreateGUID()
            TmpCampaign.RootCampaign = Me
            TmpCampaign.HistoryComment = Comment
            TmpCampaign.HistoryDate = Now
            TmpCampaign.DeleteUnusedChannels()
            History.Add(TmpCampaign.ID, TmpCampaign)

        End Sub


        '---------------------------------------------------------------------------------------
        ' Procedure : RevertToRootCampaign
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   : Sets the root campaign
        '---------------------------------------------------------------------------------------
        '
        Public Sub RevertToRootCampaign(ByRef Camp As cKampanj)
            If Not RootCampaign Is Nothing Then
                Camp = RootCampaign
            End If
        End Sub

        Public Sub SetAsMain(ByRef Camp As cKampanj)
            If Not RootCampaign Is Nothing Then
                Select Case Windows.Forms.MessageBox.Show("Should the current campaign be saved to history?", "T R I N I T Y", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                    Case DialogResult.Yes
                        Dim _comment As String = InputBox("Comment:", "T R I N I T Y")
                        RootCampaign.SaveCampaignToHistory(_comment)
                    Case DialogResult.Cancel
                        Return
                End Select
                Dim TmpCampaign As New Trinity.cKampanj(TrinitySettings.ErrorChecking)
                Dim _labCampaigns As Dictionary(Of String, cKampanj) = Campaigns
                Dim _history As Dictionary(Of String, cKampanj) = History
                Dim _id As String = ID
                _history.Remove(ID)
                TmpCampaign.LoadCampaign("", True, SaveCampaign("", True, True, True))
                ID = _id
                Campaigns = _labCampaigns
                History = _history
            End If
        End Sub


        '---------------------------------------------------------------------------------------
        ' Procedure : OpenHistory
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   : Opens a campaign from the history
        '---------------------------------------------------------------------------------------
        '
        Public Sub OpenHistory(ByRef Index As String, ByRef Camp As cKampanj)

            If Not RootCampaign Is Nothing Then
                RootCampaign.OpenHistory(Index, RootCampaign)
            Else
                Camp = History(Index)
            End If

        End Sub
        Public Function GetCampaignAreas() As List(Of dbUA)
            Dim valueFromDB = ""

            Dim listOfDBValues As New List(Of dbUA)
            Dim sqlCommand = ""

            Dim UA1 As New dbUA
            UA1.dbName = "mc_access"
            UA1.dbValue = False
            listOfDBValues.Add(UA1)

            Dim UA2 As New dbUA
            UA2.dbName = "ms_access"
            UA2.dbValue = False
            listOfDBValues.Add(UA2)

            Dim UA3 As New dbUA
            UA3.dbName = "wm_access"
            UA3.dbValue = False
            listOfDBValues.Add(UA3)

            For Each tmpObj As dbUA In listOfDBValues
                Dim tmpRows As Boolean
                tmpRows = DBReader.GetCampaignsUserAccess(sqlCommand, tmpObj.dbName)
                If tmpRows Then
                    tmpObj.dbValue = True
                Else
                End If
            Next
            'Get area table for user

            Return listOfDBValues

        End Function
        Public Property mvarCampagignArea_prop() As String
            Get
                Return mvarCampaignArea
            End Get
            Set(ByVal value As String)
                mvarCampaignArea = value
            End Set
        End Property


        '---------------------------------------------------------------------------------------
        ' Procedure : New
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   : Creates a new campaign
        '---------------------------------------------------------------------------------------
        '
        Public Sub New(ByVal UseProblemDetection As Boolean)
            MyBase.New()

            mvarErrorCheckingEnabled = UseProblemDetection

            Helper.WriteToLogFile("Register Connect.dll")
            mvarInternalAdedge = New ConnectWrapper.Brands
            Helper.WriteToLogFile("OK")
            mvarActualSpots.MainObject = Me
            mvarChannels.MainObject = Me
            mvarBookedSpots.MainObject = Me
            mvarPlannedSpots.MainObject = Me

            mvarMainTarget.MainObject = Me
            mvarSecondaryTarget.MainObject = Me
            mvarThirdTarget.MainObject = Me

            mvarArea = TrinitySettings.DefaultArea
            mvarAreaLog = TrinitySettings.DefaultAreaLog
            mvarCampaignAreas = GetCampaignAreas()

            mvarStatus = "Planned"

            RegisterProblemDetection(Me)
            'RegisterProblemDetection(DBReader)

            AddChannelsEventHandling()

            TimeShift = TrinitySettings.DefaultTimeShift
            'Dim res = checkIfCampaignHasRescritions(TrinitySettings.UserName)
            ID = CreateGUID()
            Loading = False

        End Sub


        Sub AddChannelsEventHandling()
            AddHandler mvarChannels.TRPChanged, AddressOf _trpChanged

            AddHandler mvarChannels.ChannelChanged, AddressOf _channelChanged
            AddHandler mvarChannels.BookingtypeChanged, AddressOf _bookingtypeChanged
            AddHandler mvarChannels.WeekChanged, AddressOf _weekChanged
            AddHandler mvarChannels.FilmChanged, AddressOf _filmChanged
            AddHandler mvarChannels.DaypartDefinitionsChanged, AddressOf _daypartDefinitionChanged
        End Sub

        '---------------------------------------------------------------------------------------
        ' Procedure : AddValueToNode
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   : Creates a new node in the XML document
        '---------------------------------------------------------------------------------------
        '
        Public Sub AddValueToNode(ByRef doc As Xml.XmlDocument, ByRef Node As Xml.XmlNode, ByRef Name As String, ByRef Value As Object)

            Node.AppendChild(doc.CreateNode(XmlNodeType.Element, Name, "")).AppendChild(doc.CreateTextNode(Value))

        End Sub

        Public Function GetXML(ByRef colXml As XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As XmlDocument) As Boolean
            'this function saves the collection to the xml provided
            'it will return True of succeded and false if failed

            On Error GoTo On_Error

            Dim XMLChannels As XmlElement

            For Each TmpChan As Trinity.cChannel In Me.Channels
                XMLChannels = xmlDoc.CreateElement("Channels")
                If TmpChan.GetXML(XMLChannels, errorMessege, xmlDoc) Then
                    colXml.AppendChild(XMLChannels)
                End If
            Next TmpChan

            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving the campaign")
            Return False
        End Function


        Public Function SaveUnicornFile()



            Return True

        End Function
        '---------------------------------------------------------------------------------------
        ' Procedure : SaveCampaign
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   : Saves the current campaign
        '---------------------------------------------------------------------------------------
        '
        Public Function SaveCampaignTest()

        End Function

        Public Function SaveCampaign(Optional ByRef Path As String = "", Optional ByRef DoNotSaveToFile As Boolean = False, Optional ByRef SkipHistory As Boolean = False, Optional ByRef SkipLab As Boolean = False, Optional ByVal SkipReach As Boolean = False, Optional ByVal ToDB As Boolean = False) As String

            Helper.WriteToLogFile("Start saving")
            Dim TmpChannel As cChannel
            Dim TmpBookingType As cBookingType
            Dim TmpWeek As cWeek
            Dim TmpFilm As cFilm
            Dim TmpPLTarget As cPricelistTarget
            Dim TmpPlannedSpot As cPlannedSpot
            Dim TmpActualSpot As cActualSpot
            Dim TmpCost As cCost
            Dim TmpIndex As cIndex
            Dim TmpPeriod As cPricelistPeriod
            Dim TmpAV As cAddedValue

            Dim Tmpstr As String

            Dim i As Short
            Dim j As Short

            Dim a As Object
            Dim b As Object

            If Loading Or Saving Then
                SaveCampaign = ""
                Exit Function
            End If

            'Saving = True

            If Not DoNotSaveToFile Then
                If Path = "" And mvarFilename = "" Then
                    SaveCampaign = CStr(1)
                    Exit Function
                ElseIf Path = "" Then
                    Path = mvarFilename
                End If

                'On Error Resume Next
                'Helper.WriteToLogFile("Copy Files")
                'FileCopy(Path, Path & ".bak")
                'Kill(Path)
            End If

            mvarVersion = MY_VERSION

            Try
                Helper.WriteToLogFile("Init XML")
                Dim XMLDoc As New Xml.XmlDocument
                Dim XMLTmpDoc As New Xml.XmlDocument
                Dim XMLCamp As Xml.XmlElement
                Dim TmpNode As Xml.XmlElement
                Dim XMLTmpNode As Xml.XmlElement
                Dim XMLTmpNode2 As Xml.XmlElement
                Dim XMLTmpPI As Xml.XmlProcessingInstruction
                Dim XMLWeek As Xml.XmlElement
                Dim XMLWeeks As Xml.XmlElement
                Dim XMLChannel As Xml.XmlElement
                Dim XMLChannels As Xml.XmlElement
                Dim XMLBookingType As Xml.XmlElement
                Dim XMLBookingTypes As Xml.XmlElement
                Dim XMLTarget As Xml.XmlElement
                Dim XMLTargets As Xml.XmlElement
                Dim XMLFilm As Xml.XmlElement
                Dim XMLFilms As Xml.XmlElement
                Dim XMLBuyTarget As Xml.XmlElement
                Dim XMLPricelist As Xml.XmlElement
                Dim XMLSpots As Xml.XmlElement
                Dim XMLSpot As Xml.XmlElement
                Dim XMLContractDoc As New Xml.XmlDocument
                Dim XMLContract As Xml.XmlElement
                Dim XMLIndexes As Xml.XmlElement
                Dim XMLIndex As Xml.XmlElement
                Dim XMLPricelistPeriods As Xml.XmlElement
                Dim XMLPeriod As Xml.XmlElement


                Dim Node As Object

                System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")

                Helper.WriteToLogFile("Start creating document")
                XMLDoc.PreserveWhitespace = True
                Node = XMLDoc.CreateProcessingInstruction("xml", "version='1.0'")
                XMLDoc.AppendChild(Node)

                Node = XMLDoc.CreateComment("Trinity campaign.")
                XMLDoc.AppendChild(Node)

                XMLCamp = XMLDoc.CreateElement("Campaign")
                XMLDoc.AppendChild(XMLCamp)
                XMLCamp.SetAttribute("Version", mvarVersion)
                XMLCamp.SetAttribute("Name", mvarName)
                XMLCamp.SetAttribute("ID", ID)
                XMLCamp.SetAttribute("DatabaseID", DatabaseID)
                XMLCamp.SetAttribute("UpdatedTo", mvarUpdatedTo)
                XMLCamp.SetAttribute("Planner", mvarPlanner) 'as String
                XMLCamp.SetAttribute("Buyer", mvarBuyer) 'as String
                XMLCamp.SetAttribute("Status", mvarStatus) 'as Byte
                XMLCamp.SetAttribute("FrequencyFocus", mvarFrequencyFocus) 'as Byte
                XMLCamp.SetAttribute("WeeklyFrequency", WeeklyFrequency)
                XMLCamp.SetAttribute("Filename", Path) 'as String
                XMLCamp.SetAttribute("ProductID", mvarProductID) 'as String
                XMLCamp.SetAttribute("ClientID", mvarClientID) 'as String
                XMLCamp.SetAttribute("BudgetTotalCTC", mvarBudgetTotalCTC) 'as Currency
                XMLCamp.SetAttribute("ActualTotCTC", mvarActualTotCTC) 'as Currency
                XMLCamp.SetAttribute("MarathonCTC", mvarMarathonCTC) 'as Currency
                XMLCamp.SetAttribute("Commentary", mvarCommentary) 'as String
                XMLCamp.SetAttribute("ControlFilmcodeFromClient", mvarControlFilmcodeFromClient) 'as Boolean
                XMLCamp.SetAttribute("ControlFilmcodeToBureau", mvarControlFilmcodeToBureau) 'as Boolean
                XMLCamp.SetAttribute("ControlFilmcodeToChannels", mvarControlFilmcodeToChannels) 'as Boolean
                XMLCamp.SetAttribute("ControlOKFromClient", mvarControlOKFromClient) 'as Boolean
                XMLCamp.SetAttribute("ControlTracking", mvarControlTracking) 'as Boolean
                XMLCamp.SetAttribute("ControlFollowedUp", mvarControlFollowedUp) 'as Boolean
                XMLCamp.SetAttribute("ControlFollowUpToClient", mvarControlFollowUpToClient) 'as Boolean
                XMLCamp.SetAttribute("ControlTransferredToMatrix", mvarControlTransferredToMatrix) 'as Boolean
                XMLCamp.SetAttribute("Area", mvarArea) 'as String
                XMLCamp.SetAttribute("AreaLog", mvarAreaLog) 'as String
                XMLCamp.SetAttribute("AllAdults", mvarAllAdults)
                XMLCamp.SetAttribute("MarathonOtherOrder", mvarMarathonOtherOrder)
                XMLCamp.SetAttribute("MarathonPlan", MarathonPlanNr)
                XMLCamp.SetAttribute("FilmindexAsDiscount", mvarFilmindexAsDiscount)
                XMLCamp.SetAttribute("MultiplyAddedValues", _multiplyAddedValues)
                XMLCamp.SetAttribute("ExtranetDatabaseID", mvarExtranetDatabaseID)
                XMLCamp.SetAttribute("MatrixID", mvarMatrixID)
                XMLCamp.SetAttribute("ContractID", mvarContractID)
                If mvarContractID > 0 Then
                    XMLCamp.SetAttribute("ContractVersion", DBReader.getContractSaveTime(mvarContractID))
                End If
                XMLCamp.SetAttribute("HistoryDate", HistoryDate)
                XMLCamp.SetAttribute("HistoryComment", HistoryComment)
                XMLCamp.SetAttribute("IsStripped", IsStripped)

                'Vars for Trinity Statistics
                XMLCamp.SetAttribute("TotalTRP", TotalTRP)
                XMLCamp.SetAttribute("PlannedTotCTC", PlannedTotCTC)

                'This section saves the contents of the Marathon insertions generated in the session, if any
                If Not MarathonInsertions Is Nothing Then
                    XMLCamp.AppendChild(XMLDoc.ImportNode(MarathonInsertions, True))
                End If

                XMLTmpNode = XMLDoc.CreateElement("AdedgeBrands")
                For Each TmpString As String In mvarAdEdgeProducts
                    XMLTmpNode2 = XMLDoc.CreateElement("Brand")
                    XMLTmpNode2.SetAttribute("Name", TmpString)
                    XMLTmpNode.AppendChild(XMLTmpNode2)
                Next
                XMLCamp.AppendChild(XMLTmpNode)

                XMLTmpNode = XMLDoc.CreateElement("WeeklyReach")
                For Each TmpWeek In mvarChannels(1).BookingTypes(1).Weeks
                    XMLTmpNode2 = XMLDoc.CreateElement("Week")
                    XMLTmpNode2.SetAttribute("Name", TmpWeek.Name)
                    XMLTmpNode2.SetAttribute("Reach", EstimatedWeeklyReach(TmpWeek.Name))
                    XMLTmpNode.AppendChild(XMLTmpNode2)
                Next
                XMLCamp.AppendChild(XMLTmpNode)

                'TODO: **DAYPART** Save new daypart definitions

                'XMLTmpNode = XMLDoc.CreateElement("Dayparts")
                'For i = 0 To DaypartCount - 1
                '    XMLTmpNode2 = XMLDoc.CreateElement(mvarDaypartName(i))
                '    XMLTmpNode2.SetAttribute("Start", mvarDaypartStart(i))
                '    XMLTmpNode2.SetAttribute("End", mvarDaypartEnd(i))
                '    XMLTmpNode.AppendChild(XMLTmpNode2)
                'Next
                'XMLCamp.AppendChild(XMLTmpNode)

                ' Save planned reach
                XMLTmpNode = XMLDoc.CreateElement("ReachMain")
                For i = 1 To 10
                    XMLTmpNode2 = XMLDoc.CreateElement("RF")
                    XMLTmpNode2.SetAttribute("Freq", i)
                    XMLTmpNode2.SetAttribute("Reach", mvarReachGoal(i, 0))
                    XMLTmpNode.AppendChild(XMLTmpNode2)
                Next
                XMLCamp.AppendChild(XMLTmpNode)

                XMLTmpNode = XMLDoc.CreateElement("ReachSecond")
                For i = 1 To 10
                    XMLTmpNode2 = XMLDoc.CreateElement("RF")
                    XMLTmpNode2.SetAttribute("Freq", i)
                    XMLTmpNode2.SetAttribute("Reach", mvarReachGoal(i, 1))
                    XMLTmpNode.AppendChild(XMLTmpNode2)
                Next
                XMLCamp.AppendChild(XMLTmpNode)

                If Not SkipReach Then
                    'Save actual reach for Statistics
                    CreateAdedgeSpots()
                    CalculateSpots(True)
                    XMLTmpNode = XMLDoc.CreateElement("ActualReach")
                    For i = 1 To 10
                        XMLTmpNode2 = XMLDoc.CreateElement("RF")
                        XMLTmpNode2.SetAttribute("Freq", i)
                        XMLTmpNode2.SetAttribute("Reach", ReachActual(i))
                        XMLTmpNode.AppendChild(XMLTmpNode2)
                    Next
                    XMLCamp.AppendChild(XMLTmpNode)
                End If

                XMLTmpNode = XMLDoc.CreateElement("LinkedCampaigns")
                XMLTmpNode.SetAttribute("AutoLink", _autoLinkCampaigns)
                For Each _link As cLinkedCampaign In _linkedCampaigns
                    XMLTmpNode2 = XMLDoc.CreateElement("Campaign")
                    XMLTmpNode2.SetAttribute("Name", _link.Name)
                    XMLTmpNode2.SetAttribute("Path", _link.Path)
                    XMLTmpNode2.SetAttribute("DatabaseID", _link.DatabaseID)
                    XMLTmpNode2.SetAttribute("Link", _link.Link)
                    XMLTmpNode2.SetAttribute("ClientID", _link.ClientID)
                    XMLTmpNode2.SetAttribute("ProductID", _link.ProductID)
                    XMLTmpNode.AppendChild(XMLTmpNode2)
                Next
                XMLCamp.AppendChild(XMLTmpNode)

                XMLTmpNode = XMLDoc.CreateElement("HiddenProblems")
                For Each _problem As String In TrinitySettings.HiddenProblems
                    Dim _type As String = _problem.Substring(0, _problem.IndexOf("("))
                    Dim _problemID As Integer = _problem.Substring(_problem.IndexOf("(") + 1).TrimEnd(")")
                    XMLTmpNode2 = XMLDoc.CreateElement("Problem")
                    XMLTmpNode2.SetAttribute("SourceType", _type)
                    XMLTmpNode2.SetAttribute("ProblemID", _problemID)
                    XMLTmpNode.AppendChild(XMLTmpNode2)
                Next
                XMLCamp.AppendChild(XMLTmpNode)

                'Save Contract
                If mvarContractID > 0 Then
                Else
                    'TODO: Insert code to maybe ask if the contract should be updated in the database
                    If mvarContract Is Nothing Then
                        XMLContract = XMLDoc.CreateElement("Contract")
                        XMLCamp.AppendChild(XMLContract)
                    Else
                        XMLContractDoc.LoadXml(mvarContract.Save("", True))
                        XMLContract = XMLDoc.ImportNode(XMLContractDoc.GetElementsByTagName("Contract").Item(0), True)
                        XMLCamp.AppendChild(XMLContract)
                    End If
                End If

                'Save costs

                Node = XMLDoc.CreateElement("Costs")
                For Each TmpCost In mvarCosts
                    TmpNode = XMLDoc.CreateElement("Node")
                    TmpNode.SetAttribute("Name", TmpCost.CostName)
                    TmpNode.SetAttribute("ID", TmpCost.ID)
                    TmpNode.SetAttribute("Amount", TmpCost.Amount)
                    If Not TmpCost.CountCostOn Is Nothing AndAlso TmpCost.CountCostOn.GetType.FullName = "clTrinity.Trinity.cChannel" Then
                        TmpNode.SetAttribute("CostOn", TmpCost.CountCostOn.channelname)
                    Else
                        TmpNode.SetAttribute("CostOn", TmpCost.CountCostOn)
                    End If
                    TmpNode.SetAttribute("CostType", TmpCost.CostType)
                    TmpNode.SetAttribute("MarathonID", TmpCost.MarathonID)
                    Node.appendChild(TmpNode)
                Next TmpCost
                XMLCamp.AppendChild(Node)

                'Save channels and all sub-collections and objects

                XMLChannels = XMLDoc.CreateElement("Channels")

                For Each TmpChannel In mvarChannels

                    XMLChannel = XMLDoc.CreateElement("Channel")
                    XMLChannel.SetAttribute("Name", TmpChannel.ChannelName) ' as String
                    XMLChannel.SetAttribute("ID", TmpChannel.ID) ' as Long
                    XMLChannel.SetAttribute("ShortName", TmpChannel.Shortname) ' as String
                    XMLChannel.SetAttribute("BuyingUniverse", TmpChannel.BuyingUniverse) ' as String
                    XMLChannel.SetAttribute("AdEdgeNames", TmpChannel.AdEdgeNames) ' as String
                    XMLChannel.SetAttribute("DefaultArea", TmpChannel.DefaultArea) ' as String
                    XMLChannel.SetAttribute("AgencyCommission", TmpChannel.AgencyCommission) ' as Single
                    XMLChannel.SetAttribute("Marathon", TmpChannel.MarathonName)
                    XMLChannel.SetAttribute("DeliveryAddress", TmpChannel.DeliveryAddress)
                    XMLChannel.SetAttribute("ConnectedChannel", TmpChannel.ConnectedChannel)
                    XMLChannel.SetAttribute("fileName", TmpChannel.fileName)
                    XMLChannel.SetAttribute("UseBillboards", TmpChannel.UseBillboards)
                    XMLChannel.SetAttribute("UseBreakbumpers", TmpChannel.UseBreakBumpers)
                    XMLChannel.SetAttribute("AdtooxID", TmpChannel.AdTooxChannelID)
                    XMLChannel.SetAttribute("ChannelGroup", TmpChannel.ChannelGroup)

                    'Save the targets

                    XMLTarget = XMLDoc.CreateElement("MainTarget")
                    XMLTarget.SetAttribute("Name", TmpChannel.MainTarget.TargetName) ' as New cTarget
                    XMLTarget.SetAttribute("Universe", TmpChannel.MainTarget.Universe) ' as New cTarget
                    XMLTarget.SetAttribute("SecondUniverse", TmpChannel.MainTarget.SecondUniverse)
                    XMLTarget.SetAttribute("TargetType", TmpChannel.MainTarget.TargetType)
                    XMLTarget.SetAttribute("TargetGroup", TmpChannel.MainTarget.TargetGroup)

                    XMLChannel.AppendChild(XMLTarget)

                    XMLTarget = XMLDoc.CreateElement("SecondaryTarget")
                    XMLTarget.SetAttribute("Name", TmpChannel.SecondaryTarget.TargetName) ' as New cTarget
                    XMLTarget.SetAttribute("Type", TmpChannel.SecondaryTarget.TargetType) ' as New cTarget
                    XMLTarget.SetAttribute("Universe", TmpChannel.SecondaryTarget.Universe) ' as New cTarget
                    XMLTarget.SetAttribute("SecondUniverse", TmpChannel.SecondaryTarget.SecondUniverse)
                    XMLTarget.SetAttribute("TargetType", TmpChannel.SecondaryTarget.TargetType)
                    XMLTarget.SetAttribute("TargetGroup", TmpChannel.SecondaryTarget.TargetGroup)
                    XMLChannel.AppendChild(XMLTarget)

                    'Save Booking types

                    XMLBookingTypes = XMLDoc.CreateElement("BookingTypes")

                    For Each TmpBookingType In TmpChannel.BookingTypes

                        XMLBookingType = XMLDoc.CreateElement("BookingType") ' as String
                        XMLBookingType.SetAttribute("Name", TmpBookingType.Name) ' as String
                        XMLBookingType.SetAttribute("Shortname", TmpBookingType.Shortname) ' as String

                        'Save the rest of the booking type


                        XMLBookingType.SetAttribute("BookingIdSpotlight", TmpBookingType.BookingIdSpotlight) ' as String
                        XMLBookingType.SetAttribute("BookingUrlSpotlight", TmpBookingType.BookingUrlSpotlight) ' as String
                        XMLBookingType.SetAttribute("BookingConfirmationVersion", TmpBookingType.BookingConfirmationVersion) ' as Integer
                        XMLBookingType.SetAttribute("BookingAgencyRefNo", TmpBookingType.BookingAgencyRefNo) ' as String
                        XMLBookingType.SetAttribute("CampaignRefNo", TmpBookingType.CampRefNo) ' as String

                        XMLBookingType.SetAttribute("ManualIndexes", TmpBookingType.ManualIndexes) ' as Boolean
                        XMLBookingType.SetAttribute("IndexMainTarget", TmpBookingType.IndexMainTarget) ' as Integer
                        XMLBookingType.SetAttribute("IndexSecondTarget", TmpBookingType.IndexSecondTarget) ' as Integer
                        XMLBookingType.SetAttribute("IndexAllAdults", TmpBookingType.IndexAllAdults) ' as Integer
                        XMLBookingType.SetAttribute("IndexMainTargetStatus", TmpBookingType.IndexMainTargetStatus)
                        XMLBookingType.SetAttribute("IndexSecondTargetStatus", TmpBookingType.IndexSecondTargetStatus)
                        XMLBookingType.SetAttribute("IndexAllAdultsStatus", TmpBookingType.IndexAllAdultsStatus)
                        XMLBookingType.SetAttribute("BookIt", TmpBookingType.BookIt) ' as Boolean
                        'XMLBookingType.SetAttribute("GrossCPP", TmpBookingType.GrossCPP) ' as Single
                        'XMLBookingType.SetAttribute("AverageRating", TmpBookingType.AverageRating) **** REPLACED WITH PLANNEDSPOTCOUNT
                        XMLBookingType.SetAttribute("PlannedSpotCount", TmpBookingType.EstimatedSpotCount)
                        XMLBookingType.SetAttribute("ConfirmedNetBudget", TmpBookingType.ConfirmedNetBudget) ' as Currency
                        XMLBookingType.SetAttribute("Bookingtype", TmpBookingType.Bookingtype) ' as Byte
                        XMLBookingType.SetAttribute("ContractNumber", TmpBookingType.ContractNumber) ' as String
                        XMLBookingType.SetAttribute("OrderNumber", TmpBookingType.OrderNumber)
                        XMLBookingType.SetAttribute("IsRBS", TmpBookingType.IsRBS) ' as Boolean
                        XMLBookingType.SetAttribute("IsSpecific", TmpBookingType.IsSpecific) ' as Boolean
                        XMLBookingType.SetAttribute("IsCompensation", TmpBookingType.IsCompensation) ' as Boolea
                        XMLBookingType.SetAttribute("IsPremium", TmpBookingType.IsPremium) ' as Boolean
                        XMLBookingType.SetAttribute("IsSponsorship", TmpBookingType.IsSponsorship) ' as Boolean
                        XMLBookingType.SetAttribute("PrintBookingCode", TmpBookingType.PrintBookingCode) ' as Boolean
                        XMLBookingType.SetAttribute("PrintDayparts", TmpBookingType.PrintDayparts) ' as Boolean
                        XMLBookingType.SetAttribute("SpecificsFactor", TmpBookingType.EnhancementFactor)
                        XMLBookingType.SetAttribute("Comments", TmpBookingType.Comments)
                        XMLBookingType.SetAttribute("MaxDiscount", TmpBookingType.MaxDiscount)
                        XMLTmpNode = XMLDoc.CreateElement("Dayparts")
                        For i = 0 To TmpBookingType.Dayparts.Count - 1
                            XMLTmpNode2 = XMLDoc.CreateElement("Daypart")
                            XMLTmpNode2.SetAttribute("Name", TmpBookingType.Dayparts(i).Name)
                            XMLTmpNode2.SetAttribute("Share", TmpBookingType.Dayparts(i).Share) ' as Byte
                            XMLTmpNode2.SetAttribute("Start", TmpBookingType.Dayparts(i).StartMaM) ' as Byte
                            XMLTmpNode2.SetAttribute("End", TmpBookingType.Dayparts(i).EndMaM) ' as Byte
                            XMLTmpNode2.SetAttribute("IsPrime", TmpBookingType.Dayparts(i).IsPrime) ' as Byte
                            XMLTmpNode.AppendChild(XMLTmpNode2)
                        Next
                        XMLBookingType.AppendChild(XMLTmpNode)

                        'Default Daypart split. Property was moved to BookingType.
                        XMLTmpNode = XMLDoc.CreateElement("DefaultDaypartSplit")
                        For i = 0 To TmpBookingType.Dayparts.Count - 1
                            XMLTmpNode2 = XMLDoc.CreateElement(TmpBookingType.Dayparts(i).Name)
                            XMLTmpNode2.SetAttribute("DefaultSplit", TmpBookingType.DefaultDaypart(i)) ' as New cPriceList
                            XMLTmpNode.AppendChild(XMLTmpNode2)
                        Next
                        XMLBookingType.AppendChild(XMLTmpNode)

                        XMLTmpNode = XMLDoc.CreateElement("SpecSpons")
                        For Each TmpString As String In TmpBookingType.SpecificSponsringPrograms
                            XMLTmpNode2 = XMLDoc.CreateElement("Prog")
                            XMLTmpNode2.SetAttribute("Name", TmpString)
                            XMLTmpNode.AppendChild(XMLTmpNode2)
                        Next
                        XMLBookingType.AppendChild(XMLTmpNode)

                        'Save Indexes, AddedValues and Spotindexes

                        XMLIndexes = XMLDoc.CreateElement("Indexes")
                        For Each TmpIndex In TmpBookingType.Indexes
                            XMLIndex = XMLDoc.CreateElement("Index")
                            XMLIndex.SetAttribute("ID", TmpIndex.ID)
                            XMLIndex.SetAttribute("Name", TmpIndex.Name)
                            If TmpIndex.Enhancements.Count = 0 Then
                                For i = 0 To TmpBookingType.Dayparts.Count - 1
                                    XMLIndex.SetAttribute("IndexDP" & i, TmpIndex.Index(i))
                                Next
                            Else
                                Dim XMLEnhancements As Xml.XmlElement
                                XMLEnhancements = XMLDoc.CreateElement("Enhancements")
                                For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
                                    Dim XMLEnh As Xml.XmlElement = XMLDoc.CreateElement("Enhancement")
                                    XMLEnh.SetAttribute("ID", TmpEnh.ID)
                                    XMLEnh.SetAttribute("Name", TmpEnh.Name)
                                    XMLEnh.SetAttribute("Amount", TmpEnh.Amount)
                                    XMLEnh.SetAttribute("UseThis", TmpEnh.UseThis)
                                    XMLEnhancements.AppendChild(XMLEnh)
                                Next
                                'XMLEnhancements.SetAttribute("SpecificFactor", TmpIndex.Enhancements.SpecificFactor)
                                XMLIndex.AppendChild(XMLEnhancements)
                            End If
                            XMLIndex.SetAttribute("IndexOn", TmpIndex.IndexOn)
                            XMLIndex.SetAttribute("SystemGenerated", TmpIndex.SystemGenerated)
                            XMLIndex.SetAttribute("FromDate", TmpIndex.FromDate)
                            XMLIndex.SetAttribute("ToDate", TmpIndex.ToDate)
                            XMLIndex.SetAttribute("UseThis", TmpIndex.UseThis)
                            XMLIndexes.AppendChild(XMLIndex)
                        Next TmpIndex
                        XMLBookingType.AppendChild(XMLIndexes)
                        XMLIndexes = XMLDoc.CreateElement("AddedValues")
                        For Each TmpAV In TmpBookingType.AddedValues
                            XMLIndex = XMLDoc.CreateElement("AddedValue")
                            XMLIndex.SetAttribute("ID", TmpAV.ID)
                            XMLIndex.SetAttribute("Name", TmpAV.Name)
                            XMLIndex.SetAttribute("IndexGross", TmpAV.IndexGross)
                            XMLIndex.SetAttribute("IndexNet", TmpAV.IndexNet)
                            XMLIndex.SetAttribute("ShowIn", TmpAV.ShowIn)
                            XMLIndex.SetAttribute("UseThis", TmpAV.UseThis)
                            XMLIndexes.AppendChild(XMLIndex)
                        Next TmpAV
                        XMLBookingType.AppendChild(XMLIndexes)
                        Dim XMLComps As Xml.XmlElement = XMLDoc.CreateElement("Compensations")
                        For Each TmpComp As Trinity.cCompensation In TmpBookingType.Compensations
                            Dim XMLComp As Xml.XmlElement = XMLDoc.CreateElement("Compensation")
                            XMLComp.SetAttribute("ID", TmpComp.ID)
                            XMLComp.SetAttribute("From", TmpComp.FromDate)
                            XMLComp.SetAttribute("To", TmpComp.ToDate)
                            XMLComp.SetAttribute("TRPs", TmpComp.TRPs)
                            XMLComp.SetAttribute("Expense", TmpComp.Expense)
                            XMLComp.SetAttribute("Comment", TmpComp.Comment)
                            XMLComps.AppendChild(XMLComp)
                        Next
                        XMLBookingType.AppendChild(XMLComps)

                        'Save the pricelist

                        XMLPricelist = XMLDoc.CreateElement("Pricelist")

                        XMLPricelist.SetAttribute("StartDate", TmpBookingType.Pricelist.StartDate)
                        XMLPricelist.SetAttribute("EndDate", TmpBookingType.Pricelist.EndDate)
                        XMLPricelist.SetAttribute("BuyingUniverse", TmpBookingType.Pricelist.BuyingUniverse)

                        XMLTargets = XMLDoc.CreateElement("Targets")
                        For Each TmpPLTarget In TmpBookingType.Pricelist.Targets
                            XMLBuyTarget = XMLDoc.CreateElement("BuyingTarget")
                            XMLBuyTarget.SetAttribute("Name", TmpPLTarget.TargetName)
                            XMLBuyTarget.SetAttribute("CalcCPP", TmpPLTarget.CalcCPP) ' as New cPriceListTarget
                            'XMLBuyTarget.SetAttribute("CPP", TmpPLTarget.CPP)

                            'XMLBuyTarget.SetAttribute("UniSize", TmpPLTarget.UniSize)
                            'XMLBuyTarget.SetAttribute("UniSizeNat", TmpPLTarget.UniSizeNat)

                            XMLBuyTarget.SetAttribute("Discount", TmpPLTarget.Discount) ' as Single
                            XMLBuyTarget.SetAttribute("NetCPT", TmpPLTarget.NetCPT) ' as Single
                            XMLBuyTarget.SetAttribute("NetCPP", TmpPLTarget.NetCPP) ' as Single
                            XMLBuyTarget.SetAttribute("Enhancement", TmpPLTarget.Enhancement) ' as Single
                            XMLBuyTarget.SetAttribute("IsEntered", TmpPLTarget.IsEntered) ' as Single
                            XMLBuyTarget.SetAttribute("MaxRatings", TmpPLTarget.MaxRatings) ' as Single

                            'Default Daypart split. Property was moved to BookingType.
                            'XMLTmpNode = XMLDoc.CreateElement("DaypartSplit")
                            'For i = 0 To TmpBookingType.Dayparts.Count - 1
                            '    XMLTmpNode2 = XMLDoc.CreateElement(TmpBookingType.Dayparts(i).Name)
                            '    XMLTmpNode2.SetAttribute("DefaultSplit", TmpPLTarget.DefaultDaypart(i)) ' as New cPriceList
                            '    XMLTmpNode.AppendChild(XMLTmpNode2)
                            'Next
                            'XMLBuyTarget.AppendChild(XMLTmpNode)

                            XMLTarget = XMLDoc.CreateElement("Target")
                            XMLTarget.SetAttribute("Name", TmpPLTarget.Target.TargetName)
                            XMLTarget.SetAttribute("Type", TmpPLTarget.Target.TargetType)
                            XMLTarget.SetAttribute("TargetGroup", TmpPLTarget.Target.TargetGroup)
                            XMLTarget.SetAttribute("Universe", TmpPLTarget.Target.Universe)
                            XMLTarget.SetAttribute("SecondUniverse", TmpPLTarget.Target.SecondUniverse)
                            XMLTarget.SetAttribute("StandardTarget", TmpPLTarget.StandardTarget)
                            XMLBuyTarget.AppendChild(XMLTarget)

                            'XMLTmpNode = XMLDoc.CreateElement("DaypartCPP")
                            'For i = 0 To DaypartCount - 1
                            '    XMLTmpNode2 = XMLDoc.CreateElement(DaypartName(i))

                            '    XMLTmpNode2.SetAttribute("CPP", TmpPLTarget.CPPDaypart(i))
                            '    XMLTmpNode.AppendChild(XMLTmpNode2)
                            'Next
                            XMLIndexes = XMLDoc.CreateElement("PricelistIndexes")
                            For Each TmpIndex In TmpPLTarget.Indexes
                                XMLIndex = XMLDoc.CreateElement("Index")
                                XMLIndex.SetAttribute("ID", TmpIndex.ID)
                                XMLIndex.SetAttribute("Name", TmpIndex.Name)
                                XMLIndex.SetAttribute("Index", TmpIndex.Index)
                                XMLIndex.SetAttribute("IndexOn", TmpIndex.IndexOn)
                                XMLIndex.SetAttribute("SystemGenerated", TmpIndex.SystemGenerated)
                                XMLIndex.SetAttribute("FromDate", TmpIndex.FromDate)
                                XMLIndex.SetAttribute("ToDate", TmpIndex.ToDate)
                                XMLIndexes.AppendChild(XMLIndex)
                            Next TmpIndex
                            XMLBuyTarget.AppendChild(XMLIndexes)
                            XMLPricelistPeriods = XMLDoc.CreateElement("PricelistPeriods")
                            For Each TmpPeriod In TmpPLTarget.PricelistPeriods
                                XMLPeriod = XMLDoc.CreateElement("Period")
                                XMLPeriod.SetAttribute("ID", TmpPeriod.ID)
                                XMLPeriod.SetAttribute("Name", TmpPeriod.Name)
                                XMLPeriod.SetAttribute("isCPP", TmpPeriod.PriceIsCPP)
                                XMLPeriod.SetAttribute("Price", TmpPeriod.Price(TmpPeriod.PriceIsCPP))
                                For i = 0 To TmpBookingType.Dayparts.Count - 1
                                    XMLPeriod.SetAttribute("PriceDP" & i, TmpPeriod.Price(TmpPeriod.PriceIsCPP, i))
                                Next
                                XMLPeriod.SetAttribute("UniSize", TmpPeriod.TargetUni)
                                XMLPeriod.SetAttribute("UniSizeNat", TmpPeriod.TargetNat)
                                XMLPeriod.SetAttribute("FromDate", TmpPeriod.FromDate)
                                XMLPeriod.SetAttribute("ToDate", TmpPeriod.ToDate)
                                XMLPricelistPeriods.AppendChild(XMLPeriod)
                            Next
                            XMLBuyTarget.AppendChild(XMLTmpNode)
                            XMLBuyTarget.AppendChild(XMLPricelistPeriods)
                            XMLTargets.AppendChild(XMLBuyTarget)
                        Next TmpPLTarget

                        XMLPricelist.AppendChild(XMLTargets)
                        XMLBookingType.AppendChild(XMLPricelist)

                        'Save Buyingtarget

                        XMLBuyTarget = XMLDoc.CreateElement("BuyingTarget")

                        XMLBuyTarget.SetAttribute("CalcCPP", TmpBookingType.BuyingTarget.CalcCPP) ' as New cPriceListTarget
                        'XMLBuyTarget.SetAttribute("CPP", TmpBookingType.BuyingTarget.CPP)
                        XMLBuyTarget.SetAttribute("TargetName", TmpBookingType.BuyingTarget.TargetName)
                        'XMLBuyTarget.SetAttribute("UniSize", TmpBookingType.BuyingTarget.UniSize)
                        'XMLBuyTarget.SetAttribute("UniSizeNat", TmpBookingType.BuyingTarget.UniSizeNat)
                        'XMLTmpNode = XMLDoc.CreateElement("DaypartCPP")
                        'For i = 0 To DaypartCount - 1
                        '    XMLTmpNode.SetAttribute(mvarDaypartName(i), TmpBookingType.BuyingTarget.CPPDaypart(i))
                        'Next
                        'XMLBuyTarget.AppendChild(XMLTmpNode)

                        XMLBuyTarget.SetAttribute("IsEntered", TmpBookingType.BuyingTarget.IsEntered)
                        If TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
                            XMLBuyTarget.SetAttribute("Amount", TmpBookingType.BuyingTarget.NetCPP)
                        ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPT Then
                            XMLBuyTarget.SetAttribute("Amount", TmpBookingType.BuyingTarget.NetCPT)
                        ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eDiscount Then
                            XMLBuyTarget.SetAttribute("Amount", TmpBookingType.BuyingTarget.Discount)
                        Else
                            XMLBuyTarget.SetAttribute("Amount", TmpBookingType.BuyingTarget.Enhancement)
                        End If

                        XMLTarget = XMLDoc.CreateElement("Target")
                        XMLTarget.SetAttribute("Name", TmpBookingType.BuyingTarget.Target.TargetName)
                        XMLTarget.SetAttribute("Type", TmpBookingType.BuyingTarget.Target.TargetType)
                        XMLTarget.SetAttribute("Universe", TmpBookingType.BuyingTarget.Target.Universe)
                        XMLTarget.SetAttribute("SecondUniverse", TmpBookingType.BuyingTarget.Target.SecondUniverse)
                        XMLTarget.SetAttribute("TargetGroup", TmpBookingType.BuyingTarget.Target.TargetGroup)

                        'XMLIndexes = XMLDoc.CreateElement("Indexes")
                        'For Each TmpIndex In TmpBookingType.BuyingTarget.Indexes
                        '    XMLIndex = XMLDoc.CreateElement("Index")
                        '    XMLIndex.SetAttribute("ID", TmpIndex.ID)
                        '    XMLIndex.SetAttribute("Name", TmpIndex.Name)
                        '    For i = 0 To DaypartCount - 1
                        '        XMLIndex.SetAttribute("IndexDP" & i, TmpIndex.Index(i))
                        '    Next
                        '    XMLIndex.SetAttribute("IndexOn", TmpIndex.IndexOn)
                        '    XMLIndex.SetAttribute("SystemGenerated", TmpIndex.SystemGenerated)
                        '    XMLIndex.SetAttribute("FromDate", TmpIndex.FromDate)
                        '    XMLIndex.SetAttribute("ToDate", TmpIndex.ToDate)
                        '    XMLIndexes.AppendChild(XMLIndex)
                        'Next TmpIndex
                        'XMLBuyTarget.AppendChild(XMLIndexes)
                        XMLIndexes = XMLDoc.CreateElement("PricelistIndexes")
                        For Each TmpIndex In TmpBookingType.BuyingTarget.Indexes
                            XMLIndex = XMLDoc.CreateElement("Index")
                            XMLIndex.SetAttribute("ID", TmpIndex.ID)
                            XMLIndex.SetAttribute("Name", TmpIndex.Name)
                            XMLIndex.SetAttribute("Index", TmpIndex.Index)
                            XMLIndex.SetAttribute("IndexOn", TmpIndex.IndexOn)
                            XMLIndex.SetAttribute("SystemGenerated", TmpIndex.SystemGenerated)
                            XMLIndex.SetAttribute("FromDate", TmpIndex.FromDate)
                            XMLIndex.SetAttribute("ToDate", TmpIndex.ToDate)
                            XMLIndex.SetAttribute("UseThis", TmpIndex.UseThis)
                            XMLIndexes.AppendChild(XMLIndex)
                        Next TmpIndex
                        XMLBuyTarget.AppendChild(XMLIndexes)

                        XMLIndexes = XMLDoc.CreateElement("PricelistPeriods")
                        For Each TmpPPeriod As cPricelistPeriod In TmpBookingType.BuyingTarget.PricelistPeriods
                            XMLIndex = XMLDoc.CreateElement("Period")
                            XMLIndex.SetAttribute("ID", TmpPPeriod.ID)
                            XMLIndex.SetAttribute("Name", TmpPPeriod.Name)
                            XMLIndex.SetAttribute("isCPP", TmpPPeriod.PriceIsCPP)
                            XMLIndex.SetAttribute("Price", TmpPPeriod.Price(TmpPPeriod.PriceIsCPP))
                            For i = 0 To TmpBookingType.Dayparts.Count - 1
                                XMLIndex.SetAttribute("PriceDP" & i, TmpPPeriod.Price(TmpPPeriod.PriceIsCPP, i))
                            Next
                            XMLIndex.SetAttribute("UniSize", TmpPPeriod.TargetUni)
                            XMLIndex.SetAttribute("UniSizeNat", TmpPPeriod.TargetNat)
                            XMLIndex.SetAttribute("FromDate", TmpPPeriod.FromDate)
                            XMLIndex.SetAttribute("ToDate", TmpPPeriod.ToDate)
                            XMLIndexes.AppendChild(XMLIndex)
                        Next
                        XMLBuyTarget.AppendChild(XMLIndexes)

                        XMLBuyTarget.AppendChild(XMLTarget)

                        XMLBookingType.AppendChild(XMLBuyTarget)

                        XMLIndexes = XMLDoc.CreateElement("SpotIndex")
                        For i = 0 To 500
                            If TmpBookingType.FilmIndex(i) > 0 Then
                                XMLIndex = XMLDoc.CreateElement("Index")
                                XMLIndex.SetAttribute("Length", i)
                                XMLIndex.SetAttribute("Idx", TmpBookingType.FilmIndex(i))
                                XMLIndexes.AppendChild(XMLIndex)
                            End If
                        Next
                        XMLBookingType.AppendChild(XMLIndexes)

                        'Save weeks

                        Dim w As Integer = 0
                        XMLWeeks = XMLDoc.CreateElement("Weeks")
                        For Each TmpWeek In TmpBookingType.Weeks
                            w += 1
                            ' If Not TmpWeek.Name = "25" Then
                            XMLWeek = XMLDoc.CreateElement("Week") 'as String
                            XMLWeek.SetAttribute("Name", TmpWeek.Name)
                            XMLWeek.SetAttribute("TRPControl", TmpWeek.TRPControl)
                            XMLWeek.SetAttribute("TRP", TmpWeek.TRP) 'as Single
                            XMLWeek.SetAttribute("TRPBuyingTarget", TmpWeek.TRPBuyingTarget) 'as Single
                            XMLWeek.SetAttribute("TRPAllAdults", TmpWeek.TRPAllAdults) 'as Single
                            XMLWeek.SetAttribute("GrossBudget", TmpWeek.GrossBudget)
                            XMLWeek.SetAttribute("NetBudget", TmpWeek.NetBudget) 'as Currency
                            XMLWeek.SetAttribute("Discount", TmpWeek.Discount(True)) 'as Single
                            XMLWeek.SetAttribute("StartDate", TmpWeek.StartDate) 'as Long
                            XMLWeek.SetAttribute("EndDate", TmpWeek.EndDate) 'as Long
                            XMLWeek.SetAttribute("NetCPP30", TmpWeek.NetCPP30)

                            'Added values

                            XMLIndexes = XMLDoc.CreateElement("AddedValues")
                            For Each TmpAV In TmpBookingType.AddedValues
                                XMLIndex = XMLDoc.CreateElement("AddedValue")
                                XMLIndex.SetAttribute("ID", TmpAV.ID)
                                XMLIndex.SetAttribute("Amount", TmpAV.Amount(w))
                                XMLIndexes.AppendChild(XMLIndex)
                            Next
                            XMLWeek.AppendChild(XMLIndexes)

                            'Save Films

                            XMLFilms = XMLDoc.CreateElement("Films")

                            For Each TmpFilm In TmpWeek.Films

                                XMLFilm = XMLDoc.CreateElement("Film")
                                XMLFilm.SetAttribute("Name", TmpFilm.Name)
                                XMLFilm.SetAttribute("Filmcode", TmpFilm.Filmcode) ' as String
                                '                    XMLFilm.setAttribute "AltFilmcode", TmpFilm.AltFilmcode ' as String
                                XMLFilm.SetAttribute("FilmLength", TmpFilm.FilmLength) ' as Byte
                                XMLFilm.SetAttribute("Index", TmpFilm.Index) ' as Single
                                XMLFilm.SetAttribute("GrossIndex", TmpFilm.GrossIndex)
                                XMLFilm.SetAttribute("Share", TmpFilm.Share) ' as Integer
                                XMLFilm.SetAttribute("Description", TmpFilm.Description) ' as String
                                XMLFilms.AppendChild(XMLFilm)
                            Next TmpFilm
                            XMLWeek.AppendChild(XMLFilms)
                            XMLWeeks.AppendChild(XMLWeek)
                            'End If
                        Next TmpWeek
                        XMLBookingType.AppendChild(XMLWeeks)
                        XMLBookingTypes.AppendChild(XMLBookingType)
                    Next TmpBookingType
                    XMLChannel.AppendChild(XMLBookingTypes)
                    XMLChannels.AppendChild(XMLChannel)
                Next TmpChannel
                XMLCamp.AppendChild(XMLChannels)

                Dim XMLCombos As XmlElement
                Dim XMLCombo As XmlElement
                Dim XMLComboChannel As XmlElement

                XMLCombos = XMLDoc.CreateElement("Combinations")
                For Each TmpCombo As Trinity.cCombination In mvarCombinations
                    XMLCombo = XMLDoc.CreateElement("Combo")
                    XMLCombo.SetAttribute("ID", TmpCombo.ID)
                    XMLCombo.SetAttribute("Name", TmpCombo.Name)
                    XMLCombo.SetAttribute("CombinationOn", TmpCombo.CombinationOn)
                    XMLCombo.SetAttribute("ShowAsOne", TmpCombo.ShowAsOne)
                    XMLCombo.SetAttribute("PrintAsOne", TmpCombo.PrintAsOne)
                    XMLCombo.SetAttribute("IndexMainTarget", TmpCombo.IndexMainTarget)
                    XMLCombo.SetAttribute("IndexSecondTarget", TmpCombo.IndexSecondTarget)
                    XMLCombo.SetAttribute("IndexAllAdults", TmpCombo.IndexAllAdults)
                    XMLCombo.SetAttribute("MarathonIDCombination", TmpCombo.MarathonIDCombination)
                    XMLCombo.SetAttribute("SendAsOneUnitToMarathon", TmpCombo.sendAsOneUnitTOMarathon)

                    For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                        XMLComboChannel = XMLDoc.CreateElement("Channel")
                        XMLComboChannel.SetAttribute("ID", TmpCC.ID)
                        XMLComboChannel.SetAttribute("Chan", TmpCC.Bookingtype.ParentChannel.ChannelName)
                        XMLComboChannel.SetAttribute("BT", TmpCC.Bookingtype.Name)
                        XMLComboChannel.SetAttribute("Relation", TmpCC.Relation)
                        XMLCombo.AppendChild(XMLComboChannel)
                    Next
                    XMLCombos.AppendChild(XMLCombo)
                Next
                XMLCamp.AppendChild(XMLCombos)

                'Save the targets
                XMLTarget = XMLDoc.CreateElement("MainTarget")
                XMLTarget.SetAttribute("Name", mvarMainTarget.TargetName) ' as New cTarget
                XMLTarget.SetAttribute("Type", mvarMainTarget.TargetType) ' as New cTarget
                XMLTarget.SetAttribute("Universe", mvarMainTarget.Universe) ' as New cTarget
                XMLTarget.SetAttribute("SecondUniverse", mvarMainTarget.SecondUniverse) ' as New cTarget
                XMLTarget.SetAttribute("TargetGroup", mvarMainTarget.TargetGroup)
                XMLCamp.AppendChild(XMLTarget)

                XMLTarget = XMLDoc.CreateElement("SecondaryTarget")
                XMLTarget.SetAttribute("Name", mvarSecondaryTarget.TargetName) ' as New cTarget
                XMLTarget.SetAttribute("Type", mvarSecondaryTarget.TargetType) ' as New cTarget
                XMLTarget.SetAttribute("Universe", mvarSecondaryTarget.Universe) ' as New cTarget
                XMLTarget.SetAttribute("SecondUniverse", mvarSecondaryTarget.SecondUniverse) ' as New cTarget
                XMLTarget.SetAttribute("TargetGroup", mvarSecondaryTarget.TargetGroup)
                XMLCamp.AppendChild(XMLTarget)

                XMLTarget = XMLDoc.CreateElement("ThirdTarget")
                XMLTarget.SetAttribute("Name", mvarThirdTarget.TargetName) ' as New cTarget
                XMLTarget.SetAttribute("Type", mvarThirdTarget.TargetType) ' as New cTarget
                XMLTarget.SetAttribute("Universe", mvarThirdTarget.Universe) ' as New cTarget
                XMLTarget.SetAttribute("SecondUniverse", mvarThirdTarget.SecondUniverse) ' as New cTarget
                XMLTarget.SetAttribute("TargetGroup", mvarThirdTarget.TargetGroup)
                XMLCamp.AppendChild(XMLTarget)

                'Save Planned spots

                XMLSpots = XMLDoc.CreateElement("PlannedSpots")
                XMLSpots.SetAttribute("TotalTRP", mvarPlannedSpots.TotalTRP)
                XMLSpots.SetAttribute("TRPToDeliver", mvarPlannedSpots.TotalTRPToDeliver)

                For Each TmpPlannedSpot In mvarPlannedSpots

                    If TmpPlannedSpot.Week IsNot Nothing Then
                        XMLSpot = XMLDoc.CreateElement("Spot")
                        XMLSpot.SetAttribute("ID", TmpPlannedSpot.ID) ' as String
                        XMLSpot.SetAttribute("Channel", TmpPlannedSpot.Channel.ChannelName) ' as cChannel
                        XMLSpot.SetAttribute("ChannelID", TmpPlannedSpot.ChannelID) ' as String
                        XMLSpot.SetAttribute("BookingType", TmpPlannedSpot.Bookingtype.Name) ' as cBookingType
                        Try
                            XMLSpot.SetAttribute("Week", TmpPlannedSpot.Week.Name) ' as cWeek
                        Catch
                            XMLSpot.SetAttribute("Week", "") ' as cWeek
                        End Try
                        XMLSpot.SetAttribute("AirDate", TmpPlannedSpot.AirDate) ' as Long
                        XMLSpot.SetAttribute("MaM", TmpPlannedSpot.MaM) ' as Integer
                        XMLSpot.SetAttribute("ProgBefore", TmpPlannedSpot.ProgBefore) ' as String
                        XMLSpot.SetAttribute("ProgAfter", TmpPlannedSpot.ProgAfter) ' as String
                        XMLSpot.SetAttribute("Programme", TmpPlannedSpot.Programme) ' as String
                        XMLSpot.SetAttribute("Advertiser", TmpPlannedSpot.Advertiser) ' as String
                        XMLSpot.SetAttribute("Product", TmpPlannedSpot.Product) ' as String
                        XMLSpot.SetAttribute("Filmcode", TmpPlannedSpot.Filmcode) ' as String
                        'Put #99, , TmpPlannedSpot.Film ' as cFilm
                        XMLSpot.SetAttribute("RatingBuyTarget", TmpPlannedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)) ' as Single
                        XMLSpot.SetAttribute("Estimation", TmpPlannedSpot.Estimation)
                        XMLSpot.SetAttribute("MyRating", TmpPlannedSpot.MyRating) ' as Single
                        XMLSpot.SetAttribute("Index", TmpPlannedSpot.Index) ' as Integer
                        XMLSpot.SetAttribute("SpotLength", TmpPlannedSpot.SpotLength) ' as Byte
                        XMLSpot.SetAttribute("SpotType", TmpPlannedSpot.SpotType) ' as Byte
                        XMLSpot.SetAttribute("PriceNet", TmpPlannedSpot.PriceNet) ' as Currency
                        XMLSpot.SetAttribute("PriceGross", TmpPlannedSpot.PriceGross) ' as Currency
                        XMLSpot.SetAttribute("Remark", TmpPlannedSpot.Remark)
                        XMLSpot.SetAttribute("Placement", TmpPlannedSpot.Placement)
                        If Not TmpPlannedSpot.AddedValue Is Nothing Then
                            XMLSpot.SetAttribute("AddedValue", TmpPlannedSpot.AddedValue.ID)
                        End If
                        XMLSpots.AppendChild(XMLSpot)
                    End If
                Next TmpPlannedSpot
                XMLCamp.AppendChild(XMLSpots)

                'Save Actual spots
                XMLSpots = XMLDoc.CreateElement("ActualSpots")
                XMLSpots.SetAttribute("TotalTRP", mvarActualSpots.TotalTRP)
                For Each TmpActualSpot In mvarActualSpots

                    XMLSpot = XMLDoc.CreateElement("Spot")
                    XMLSpot.SetAttribute("ID", TmpActualSpot.ID) 'as String
                    XMLSpot.SetAttribute("AirDate", TmpActualSpot.AirDate) 'as Long
                    XMLSpot.SetAttribute("MaM", TmpActualSpot.MaM) 'as Integer
                    XMLSpot.SetAttribute("Second", TmpActualSpot.Second) 'as Integer
                    XMLSpot.SetAttribute("Channel", TmpActualSpot.Channel.ChannelName) 'as cChannel
                    XMLSpot.SetAttribute("ProgBefore", TmpActualSpot.ProgBefore) 'as String
                    XMLSpot.SetAttribute("ProgAfter", TmpActualSpot.ProgAfter) 'as String
                    XMLSpot.SetAttribute("Programme", TmpActualSpot.Programme) 'as String
                    XMLSpot.SetAttribute("Advertiser", TmpActualSpot.Advertiser) 'as String
                    XMLSpot.SetAttribute("Product", TmpActualSpot.Product) 'as String
                    XMLSpot.SetAttribute("Filmcode", TmpActualSpot.Filmcode) 'as String
                    XMLSpot.SetAttribute("Index", TmpActualSpot.Index) 'as Integer
                    XMLSpot.SetAttribute("PosInBreak", TmpActualSpot.PosInBreak) 'as Byte
                    XMLSpot.SetAttribute("SpotsInBreak", TmpActualSpot.SpotsInBreak) 'as Byte
                    If Not TmpActualSpot.MatchedSpot Is Nothing Then
                        XMLSpot.SetAttribute("MatchedSpot", TmpActualSpot.MatchedSpot.ID) 'as cPlannedSpot
                    Else
                        XMLSpot.SetAttribute("MatchedSpot", "")
                    End If
                    XMLSpot.SetAttribute("SpotLength", TmpActualSpot.SpotLength) 'as Byte
                    XMLSpot.SetAttribute("Deactivated", TmpActualSpot.Deactivated) 'as Boolean
                    XMLSpot.SetAttribute("SpotType", TmpActualSpot.SpotType) 'as Byte
                    XMLSpot.SetAttribute("BreakType", TmpActualSpot.BreakType) 'as EnumBreakType
                    XMLSpot.SetAttribute("SecondRating", TmpActualSpot.SecondRating) 'as Single
                    XMLSpot.SetAttribute("AdEdgeChannel", TmpActualSpot.AdedgeChannel) 'as String
                    XMLSpot.SetAttribute("BookingType", TmpActualSpot.Bookingtype.Name) 'as cBookingType
                    XMLSpot.SetAttribute("GroupIdx", TmpActualSpot.GroupIdx) 'as Integer
                    XMLSpot.SetAttribute("RatingMain", TmpActualSpot.Rating(cActualSpot.ActualTargetEnum.ateMainTarget))
                    XMLSpot.SetAttribute("RatingBuying", TmpActualSpot.Rating(cActualSpot.ActualTargetEnum.ateBuyingTarget))
                    XMLSpot.SetAttribute("SpotControlRemark", TmpActualSpot.SpotControlRemark)
                    XMLSpot.SetAttribute("SpotControlStatus", TmpActualSpot.SpotControlStatus)
                    XMLSpots.AppendChild(XMLSpot)

                Next TmpActualSpot
                XMLCamp.AppendChild(XMLSpots)

                Helper.WriteToLogFile("Create more XML")

                Dim XMLTmpList As Xml.XmlElement
                Dim XMLTmpList2 As Xml.XmlElement
                Dim XMLTmpElement As Xml.XmlElement

                XMLTmpNode = XMLDoc.CreateElement("ReachGoals")

                'For i = 1 To mvarRTColl.Count

                '    a = mvarRTColl.Keys
                '    XMLTmpNode2 = XMLDoc.createElement("ReachGoal")
                '    XMLTmpNode2.setAttribute("Name", a(i - 1))
                '    For j = 1 To mvarRTColl(a(i - 1)).Count
                '        b = mvarRTColl.Item(a(i - 1)).Keys
                '        Tmpstr = b(j - 1)
                '        XMLTmpElement = XMLDoc.createElement("Goal")
                '        XMLTmpElement.setAttribute("Freq", Tmpstr)
                '        XMLTmpElement.setAttribute("Reach", CByte(mvarRTColl(a(i - 1)).Item(b(j - 1))))
                '        XMLTmpNode2.appendChild(XMLTmpElement)
                '    Next
                '    XMLTmpNode.appendChild(XMLTmpNode2)
                'Next

                XMLCamp.AppendChild(XMLTmpNode)

                XMLTmpNode = XMLDoc.CreateElement("Activities")

                XMLCamp.AppendChild(XMLTmpNode)

                XMLSpots = XMLDoc.CreateElement("BookedSpots")
                XMLSpots.SetAttribute("TotalTRP", mvarBookedSpots.TotalTRP)
                XMLSpots.SetAttribute("TotalNet", mvarBookedSpots.TotalNetBudget)
                XMLSpots.SetAttribute("TotalGross", mvarBookedSpots.TotalGrossBudget)

                For i = 1 To mvarBookedSpots.Count
                    XMLSpot = XMLDoc.CreateElement("Spot")
                    XMLSpot.SetAttribute("ID", mvarBookedSpots(i).ID)
                    XMLSpot.SetAttribute("AirDate", mvarBookedSpots(i).AirDate)
                    XMLSpot.SetAttribute("Bookingtype", mvarBookedSpots(i).Bookingtype.Name)
                    XMLSpot.SetAttribute("Channel", mvarBookedSpots(i).Channel.ChannelName)
                    XMLSpot.SetAttribute("ChannelEstimate", mvarBookedSpots(i).ChannelEstimate)
                    XMLSpot.SetAttribute("DatabaseID", mvarBookedSpots(i).DatabaseID)
                    XMLSpot.SetAttribute("Filmcode", mvarBookedSpots(i).Filmcode)
                    XMLSpot.SetAttribute("GrossPrice", mvarBookedSpots(i).GrossPrice)
                    XMLSpot.SetAttribute("MaM", mvarBookedSpots(i).MaM)
                    XMLSpot.SetAttribute("MyEstimate", mvarBookedSpots(i).MyEstimate)
                    XMLSpot.SetAttribute("MyEstimateBuyTarget", mvarBookedSpots(i).MyEstimateBuyTarget)
                    XMLSpot.SetAttribute("NetPrice", mvarBookedSpots(i).NetPrice)
                    XMLSpot.SetAttribute("ProgAfter", mvarBookedSpots(i).ProgAfter)
                    XMLSpot.SetAttribute("ProgBefore", mvarBookedSpots(i).ProgBefore)
                    XMLSpot.SetAttribute("Programme", mvarBookedSpots(i).Programme)
                    XMLSpot.SetAttribute("IsLocal", mvarBookedSpots(i).IsLocal)
                    XMLSpot.SetAttribute("IsRB", mvarBookedSpots(i).IsRB)
                    XMLSpot.SetAttribute("Comments", mvarBookedSpots(i).Comments)
                    XMLIndexes = XMLDoc.CreateElement("AddedValues")
                    If Not mvarBookedSpots(i).AddedValues Is Nothing Then
                        For Each kv As KeyValuePair(Of String, Trinity.cAddedValue) In mvarBookedSpots(i).AddedValues
                            XMLIndex = XMLDoc.CreateElement("AddedValue")
                            XMLIndex.SetAttribute("ID", kv.Key)
                            XMLIndexes.AppendChild(XMLIndex)
                        Next
                    End If
                    XMLSpot.AppendChild(XMLIndexes)
                    XMLSpots.AppendChild(XMLSpot)
                    If Not DoNotSaveToFile Then
                        mvarBookedSpots(i).RecentlyBooked = False
                    End If
                Next
                XMLCamp.AppendChild(XMLSpots)

                XMLTmpNode = XMLDoc.CreateElement("LabCampaigns")
                If Not SkipLab Then
                    If Not Campaigns Is Nothing Then
                        For Each kv As KeyValuePair(Of String, cKampanj) In Campaigns
                            XMLTmpDoc.LoadXml(kv.Value.SaveCampaign("", True, True, True, True))
                            XMLTmpNode2 = XMLDoc.ImportNode(XMLTmpDoc.GetElementsByTagName("Campaign").Item(0), True)
                            XMLTmpNode2.SetAttribute("LabName", kv.Key)
                            XMLTmpNode.AppendChild(XMLTmpNode2)
                        Next
                    End If
                End If
                XMLCamp.AppendChild(XMLTmpNode)


                XMLTmpNode = XMLDoc.CreateElement("History")
                If Not SkipHistory Then
                    For Each kv As KeyValuePair(Of String, cKampanj) In History
                        XMLTmpDoc.LoadXml(kv.Value.SaveCampaign("", True, True, True, True))
                        XMLTmpNode2 = XMLDoc.ImportNode(XMLTmpDoc.GetElementsByTagName("Campaign").Item(0), True)
                        XMLTmpNode.AppendChild(XMLTmpNode2)
                    Next
                End If
                XMLCamp.AppendChild(XMLTmpNode)

                'Plugin data
                If PluginSaveData IsNot Nothing AndAlso PluginSaveData.Count > 0 Then
                    XMLTmpNode = XMLDoc.CreateElement("Plugins")
                    For Each _data As KeyValuePair(Of String, XElement) In PluginSaveData
                        XMLTmpNode2 = XMLDoc.CreateElement(_data.Key)
                        XMLTmpNode2.AppendChild(XMLDoc.ImportNode(_data.Value.ToXMLElement, True))
                        XMLTmpNode.AppendChild(XMLTmpNode2)
                    Next
                    XMLCamp.AppendChild(XMLTmpNode)
                End If

                Helper.WriteToLogFile("Save the file")

                If ToDB Then
                    DBReader.SaveCampaign(Me, XMLCamp)
                ElseIf DoNotSaveToFile Then
                    SaveCampaign = XMLDoc.OuterXml
                Else
                    'The XML file is locked when you load it, here we temporarily free it so we can save it
                    Dim _tmpPath As String = IO.Path.Combine(TrinitySettings.LocalDataPath, "Cache\" & IO.Path.GetFileName(Path))
                    If Not My.Computer.FileSystem.DirectoryExists(IO.Path.Combine(TrinitySettings.LocalDataPath, "Cache")) Then
                        My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(TrinitySettings.LocalDataPath, "Cache"))
                    End If

                    If fs IsNot Nothing Then
                        fs.Close()
                    End If
                    'fs = New FileStream(Path, FileMode.Create, FileAccess.Write, FileShare.None)
                    XMLDoc.Save(_tmpPath)
                    My.Computer.FileSystem.CopyFile(_tmpPath, Path, True)

                    Dim _attempts = 0
                    Do While My.Computer.FileSystem.GetFileInfo(_tmpPath).Length <> My.Computer.FileSystem.GetFileInfo(Path).Length AndAlso _attempts < 3
                        My.Computer.FileSystem.CopyFile(_tmpPath, Path, True)
                        _attempts += 1
                    Loop

                    'Kill(Path & ".bak")
                    If Path <> "" Then
                        mvarFilename = Path
                    End If
                    SaveCampaign = ""
                    'And here we lock it again
                    'fs.Close() 'Removed because VerifySave does this whether it is verified or not
                    fs = New FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read)
                End If

                '    Put #99, , mvarReachTargets 'as New Dictionary
                Saving = False
                Exit Function

            Catch ex As Exception
                If Not fs Is Nothing Then fs.Close()
                If mvarFilename <> "" Then fs = New FileStream(mvarFilename, FileMode.Open, FileAccess.Read, FileShare.Read)
                Saving = False
                Helper.WriteToLogFile("ERROR : " & ex.Message)
                Err.Raise(Err.Number, "cKampanj: SaveCampaign", ex.Message & Chr(10) & Chr(10) & "There was a problem saving this file. Please contact the system administrators.")
            End Try

        End Function

        ''' <summary>
        ''' Tries to load the saved file. If the document passes an XMLDocument.LoadXML then it is assumed that it is well formed and complete.
        ''' </summary>
        ''' <param name="Path"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function VerifySave(ByVal Path As String) As Boolean

            Dim tmpXMLDoc As New XmlDocument

            Try
                fs.Close()
                fs = New FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.None)
                Dim fr As New StreamReader(fs)
                tmpXMLDoc.LoadXml(fr.ReadToEnd.ToString)
                fs.Close()
                tmpXMLDoc = Nothing
                Return True
            Catch ex As System.Xml.XmlException
                Debug.WriteLine("Error verifying the saved campaign - " & ex.Message)
                fs.Close()
                tmpXMLDoc = Nothing
                Return False
            Finally

            End Try
        End Function

        Public Function SaveCampaignWithGetXML(Optional ByRef Path As String = "", Optional ByRef DoNotSaveToFile As Boolean = False, Optional ByRef SkipHistory As Boolean = False, Optional ByRef SkipLab As Boolean = False, Optional ByVal SkipReach As Boolean = False) As String

            Helper.WriteToLogFile("Start saving")

            'this list will contain all eventual error messeges after saving
            Dim ErrorMessege As New List(Of String)

            Dim TmpWeek As cWeek
            Dim TmpCost As cCost

            Dim Tmpstr As String
            Dim i As Integer

            On Error GoTo SaveCampaign_Error

            If Loading Or Saving Then
                SaveCampaignWithGetXML = ""
                Exit Function
            End If

            If Not DoNotSaveToFile Then
                If Path = "" And mvarFilename = "" Then
                    SaveCampaignWithGetXML = CStr(1)
                    Exit Function
                ElseIf Path = "" Then
                    Path = mvarFilename
                End If

            End If
            On Error GoTo SaveCampaign_Error

            mvarVersion = MY_VERSION

            Helper.WriteToLogFile("Init XML")
            Dim XMLDoc As New Xml.XmlDocument
            Dim XMLTmpDoc As New Xml.XmlDocument
            Dim XMLCamp As Xml.XmlElement
            Dim TmpNode As Xml.XmlElement
            Dim XMLTmpNode As Xml.XmlElement
            Dim XMLTmpNode2 As Xml.XmlElement
            Dim XMLTmpPI As Xml.XmlProcessingInstruction
            Dim XMLWeek As Xml.XmlElement
            Dim XMLWeeks As Xml.XmlElement
            Dim XMLChannels As Xml.XmlElement
            Dim XMLTarget As Xml.XmlElement
            Dim XMLTargets As Xml.XmlElement
            Dim XMLSpots As Xml.XmlElement
            Dim XMLContractDoc As New Xml.XmlDocument
            Dim XMLContract As Xml.XmlElement


            Dim Node As Object

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")

            Helper.WriteToLogFile("Start creating document")
            XMLDoc.PreserveWhitespace = True
            Node = XMLDoc.CreateProcessingInstruction("xml", "version='1.0'")
            XMLDoc.AppendChild(Node)

            Node = XMLDoc.CreateComment("Trinity campaign.")
            XMLDoc.AppendChild(Node)

            XMLCamp = XMLDoc.CreateElement("Campaign")
            XMLDoc.AppendChild(XMLCamp)
            XMLCamp.SetAttribute("Version", mvarVersion)
            XMLCamp.SetAttribute("Name", mvarName)
            XMLCamp.SetAttribute("ID", ID)
            XMLCamp.SetAttribute("UpdatedTo", mvarUpdatedTo)
            XMLCamp.SetAttribute("Planner", mvarPlanner) 'as String
            XMLCamp.SetAttribute("Buyer", mvarBuyer) 'as String
            XMLCamp.SetAttribute("Status", mvarStatus) 'as Byte
            XMLCamp.SetAttribute("FrequencyFocus", mvarFrequencyFocus) 'as Byte
            XMLCamp.SetAttribute("Filename", mvarFilename) 'as String
            XMLCamp.SetAttribute("ProductID", mvarProductID) 'as String
            XMLCamp.SetAttribute("ClientID", mvarClientID) 'as String
            XMLCamp.SetAttribute("BudgetTotalCTC", mvarBudgetTotalCTC) 'as Currency
            XMLCamp.SetAttribute("ActualTotCTC", mvarActualTotCTC) 'as Currency
            XMLCamp.SetAttribute("MarathonCTC", mvarMarathonCTC) 'as Currency
            XMLCamp.SetAttribute("Commentary", mvarCommentary) 'as String
            XMLCamp.SetAttribute("ControlFilmcodeFromClient", mvarControlFilmcodeFromClient) 'as Boolean
            XMLCamp.SetAttribute("ControlFilmcodeToBureau", mvarControlFilmcodeToBureau) 'as Boolean
            XMLCamp.SetAttribute("ControlFilmcodeToChannels", mvarControlFilmcodeToChannels) 'as Boolean
            XMLCamp.SetAttribute("ControlOKFromClient", mvarControlOKFromClient) 'as Boolean
            XMLCamp.SetAttribute("ControlTracking", mvarControlTracking) 'as Boolean
            XMLCamp.SetAttribute("ControlFollowedUp", mvarControlFollowedUp) 'as Boolean
            XMLCamp.SetAttribute("ControlFollowUpToClient", mvarControlFollowUpToClient) 'as Boolean
            XMLCamp.SetAttribute("ControlTransferredToMatrix", mvarControlTransferredToMatrix) 'as Boolean
            XMLCamp.SetAttribute("Area", mvarArea) 'as String
            XMLCamp.SetAttribute("AreaLog", mvarAreaLog) 'as String
            XMLCamp.SetAttribute("AllAdults", mvarAllAdults)
            XMLCamp.SetAttribute("MarathonOtherOrder", mvarMarathonOtherOrder)
            XMLCamp.SetAttribute("MarathonPlan", MarathonPlanNr)
            XMLCamp.SetAttribute("FilmindexAsDiscount", mvarFilmindexAsDiscount)
            XMLCamp.SetAttribute("MultiplyAddedValues", _multiplyAddedValues)

            XMLCamp.SetAttribute("HistoryDate", HistoryDate)
            XMLCamp.SetAttribute("HistoryComment", HistoryComment)

            'Vars for Trinity Statistics
            XMLCamp.SetAttribute("TotalTRP", TotalTRP)
            XMLCamp.SetAttribute("PlannedTotCTC", PlannedTotCTC)

            XMLTmpNode = XMLDoc.CreateElement("WeeklyReach")
            For Each TmpWeek In mvarChannels(1).BookingTypes(1).Weeks
                XMLTmpNode2 = XMLDoc.CreateElement("Week")
                XMLTmpNode2.SetAttribute("Name", TmpWeek.Name)
                XMLTmpNode2.SetAttribute("Reach", EstimatedWeeklyReach(TmpWeek.Name))
                XMLTmpNode.AppendChild(XMLTmpNode2)
            Next
            XMLCamp.AppendChild(XMLTmpNode)

            XMLTmpNode = XMLDoc.CreateElement("Dayparts")
            For i = 0 To Dayparts.Count - 1
                XMLTmpNode2 = XMLDoc.CreateElement("Daypart")
                XMLTmpNode2.SetAttribute("Name", _dayparts(i).Name)
                XMLTmpNode2.SetAttribute("Start", _dayparts(i).StartMaM)
                XMLTmpNode2.SetAttribute("End", _dayparts(i).EndMaM)
                XMLTmpNode2.SetAttribute("IsPrime", _dayparts(i).IsPrime)
                XMLTmpNode.AppendChild(XMLTmpNode2)
            Next
            XMLCamp.AppendChild(XMLTmpNode)

            ' Save planned reach
            XMLTmpNode = XMLDoc.CreateElement("ReachMain")
            For i = 1 To 10
                XMLTmpNode2 = XMLDoc.CreateElement("RF")
                XMLTmpNode2.SetAttribute("Freq", i)
                XMLTmpNode2.SetAttribute("Reach", mvarReachGoal(i, 0))
                XMLTmpNode.AppendChild(XMLTmpNode2)
            Next
            XMLCamp.AppendChild(XMLTmpNode)

            XMLTmpNode = XMLDoc.CreateElement("ReachSecond")
            For i = 1 To 10
                XMLTmpNode2 = XMLDoc.CreateElement("RF")
                XMLTmpNode2.SetAttribute("Freq", i)
                XMLTmpNode2.SetAttribute("Reach", mvarReachGoal(i, 1))
                XMLTmpNode.AppendChild(XMLTmpNode2)
            Next
            XMLCamp.AppendChild(XMLTmpNode)

            If Not SkipReach Then
                'Save actual reach for Statistics
                CreateAdedgeSpots()
                CalculateSpots(True)
                XMLTmpNode = XMLDoc.CreateElement("ActualReach")
                For i = 1 To 10
                    XMLTmpNode2 = XMLDoc.CreateElement("RF")
                    XMLTmpNode2.SetAttribute("Freq", i)
                    XMLTmpNode2.SetAttribute("Reach", ReachActual(i))
                    XMLTmpNode.AppendChild(XMLTmpNode2)
                Next
                XMLCamp.AppendChild(XMLTmpNode)
            End If

            'Save Contract

            If mvarContract Is Nothing Then
                XMLContract = XMLDoc.CreateElement("Contract")
                XMLCamp.AppendChild(XMLContract)
            ElseIf Not DoNotSaveToFile Then
                XMLContractDoc.LoadXml(mvarContract.Save("", True))
                XMLContract = XMLDoc.ImportNode(XMLContractDoc.GetElementsByTagName("Contract").Item(0), True)
                XMLCamp.AppendChild(XMLContract)
            End If

            'Save costs
            Node = XMLDoc.CreateElement("Costs")
            mvarCosts.GetXML(Node, ErrorMessege, XMLDoc)
            XMLCamp.AppendChild(Node)

            'Save channels and all sub-collections and objects
            XMLChannels = XMLDoc.CreateElement("Channels")
            Channels.GetXML(XMLChannels, ErrorMessege, XMLDoc)
            XMLCamp.AppendChild(XMLChannels)


            'get added combinations
            Dim XMLCombos As XmlElement
            XMLCombos = XMLDoc.CreateElement("Combinations")
            mvarCombinations.GetXML(XMLCombos, ErrorMessege, XMLDoc)
            XMLCamp.AppendChild(XMLCombos)


            'Save the targets (first, second and third)
            XMLTarget = XMLDoc.CreateElement("MainTarget")
            mvarMainTarget.GetXML(XMLTarget, ErrorMessege, XMLDoc)
            XMLCamp.AppendChild(XMLTarget)

            XMLTarget = XMLDoc.CreateElement("SecondaryTarget")
            mvarSecondaryTarget.GetXML(XMLTarget, ErrorMessege, XMLDoc)
            XMLCamp.AppendChild(XMLTarget)

            XMLTarget = XMLDoc.CreateElement("ThirdTarget")
            mvarThirdTarget.GetXML(XMLTarget, ErrorMessege, XMLDoc)
            XMLCamp.AppendChild(XMLTarget)


            'Save Planned spots
            XMLSpots = XMLDoc.CreateElement("PlannedSpots")
            mvarPlannedSpots.GetXML(XMLSpots, ErrorMessege, XMLDoc)
            XMLCamp.AppendChild(XMLSpots)

            'Save Actual spots
            XMLSpots = XMLDoc.CreateElement("ActualSpots")
            mvarActualSpots.GetXML(XMLSpots, ErrorMessege, XMLDoc)
            XMLCamp.AppendChild(XMLSpots)

            Helper.WriteToLogFile("Create more XML")

            Dim XMLTmpList As Xml.XmlElement
            Dim XMLTmpList2 As Xml.XmlElement
            Dim XMLTmpElement As Xml.XmlElement

            XMLTmpNode = XMLDoc.CreateElement("ReachGoals")

            XMLCamp.AppendChild(XMLTmpNode)

            XMLTmpNode = XMLDoc.CreateElement("Activities")

            XMLCamp.AppendChild(XMLTmpNode)

            'save booked spots
            XMLSpots = XMLDoc.CreateElement("BookedSpots")
            mvarBookedSpots.GetXML(XMLSpots, ErrorMessege, XMLDoc)
            XMLCamp.AppendChild(XMLSpots)


            'we reset the recently booked if we save to file
            If Not DoNotSaveToFile Then
                For i = 1 To mvarBookedSpots.Count
                    mvarBookedSpots(i).RecentlyBooked = False
                Next
            End If

            XMLTmpNode = XMLDoc.CreateElement("LabCampaigns")
            If Not SkipLab Then
                If Not Campaigns Is Nothing Then
                    For Each kv As KeyValuePair(Of String, cKampanj) In Campaigns
                        XMLTmpDoc.LoadXml(kv.Value.SaveCampaign("", True, True, True, True))
                        XMLTmpNode2 = XMLDoc.ImportNode(XMLTmpDoc.GetElementsByTagName("Campaign").Item(0), True)
                        XMLTmpNode2.SetAttribute("LabName", kv.Key)
                        XMLTmpNode.AppendChild(XMLTmpNode2)
                    Next
                End If
            End If
            XMLCamp.AppendChild(XMLTmpNode)


            XMLTmpNode = XMLDoc.CreateElement("History")
            If Not SkipHistory Then
                For Each kv As KeyValuePair(Of String, cKampanj) In History
                    XMLTmpDoc.LoadXml(kv.Value.SaveCampaign("", True, True, True, True))
                    XMLTmpNode2 = XMLDoc.ImportNode(XMLTmpDoc.GetElementsByTagName("Campaign").Item(0), True)
                    XMLTmpNode.AppendChild(XMLTmpNode2)
                Next
            End If
            XMLCamp.AppendChild(XMLTmpNode)

            On Error Resume Next

            Helper.WriteToLogFile("Save the file")

            If DoNotSaveToFile Then
                SaveCampaignWithGetXML = XMLDoc.OuterXml
            Else
                'if we have no errors we save it as normal
                If ErrorMessege.Count = 0 Then
                    XMLDoc.Save(Path)
                Else
                    Path = Path.Substring(0, Path.Length - 4) & "_incomplete_.cmp"
                    XMLDoc.Save(Path)
                    MsgBox("There was a error saving the campaign. The campaign was saved with _incomplete_ added to its name." & vbCrLf & "You should not try to open this file but contact system administrators", MsgBoxStyle.Critical, "E R R O R")
                End If

                If Path <> "" Then
                    mvarFilename = Path
                End If

                SaveCampaignWithGetXML = ""
            End If

            '    Put #99, , mvarReachTargets 'as New Dictionary
            Saving = False
            On Error GoTo 0
            Exit Function

SaveCampaign_Error:

            Saving = False
            Helper.WriteToLogFile("ERROR (" & Err.Number & "): " & Err.Description)
            Err.Raise(Err.Number, "cKampanj: SaveCampaign", Err.Description & Chr(10) & Chr(10) & "The original file was saved as " & mvarFilename & ".bak" & Chr(10) & Chr(10) & "DO NOT ATTEMPT TO SAVE AGAIN!!!")
            Resume Next

        End Function

        '---------------------------------------------------------------------------------------
        ' Procedure : LoadCampaign
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   : Loads a saved campaign
        '---------------------------------------------------------------------------------------
        '

        Private Structure AdedgeTargetStruct

            Private _groupName As String
            Public Property GroupName() As String
                Get
                    Return _groupName
                End Get
                Set(ByVal value As String)
                    _groupName = value
                End Set
            End Property

            Private _targetName As String
            Public Property TargetName() As String
                Get
                    Return _targetName
                End Get
                Set(ByVal value As String)
                    _targetName = value
                End Set
            End Property

        End Structure

        Friend Sub _lockChanged()
            Dim _userName As String = ""
            Dim _lock As cDBReaderSQL.CampaignDBLock = DirectCast(DBReader, cDBReaderSQL).GetCampaignLock(Me.DatabaseID)
            If _lock.UserID <> TrinitySettings.UserID AndAlso _lock.UserID <> 0 Then
                _readOnly = True
            End If
            If _lock.UserID <> 0 Then
                _userName = DBReader.getUser(_lock.UserID)!name
            End If
            RaiseEvent LockChanged(_userName)
        End Sub

        Private _readOnly As Boolean
        Public Property [ReadOnly]() As Boolean
            Get
                Return _readOnly
            End Get
            Set(ByVal value As Boolean)
                _readOnly = value
            End Set
        End Property

        Private ReplacementTargets As New Dictionary(Of String, AdedgeTargetStruct)
        Public Function LoadCampaign(ByRef Path As String, Optional ByRef DoNotLoadFromFile As Boolean = False, Optional ByRef XML As String = "", Optional ByVal LoadStripped As Boolean = False) As Byte
            Dim i As Short

            Dim IniFile As New clsIni

            'Dim s As Short
            Dim Group As String
            'Dim Freq As Short
            'Dim Reach As Byte
            'Dim ChangeDate As Date
            'Dim Who As String
            'Dim Activity As String
            'Dim ChangeTime As String
            'Dim a As Short
            'Dim Start As Short

            Dim TmpString As String

            Dim TmpChannel As cChannel
            Dim TmpBookingType As cBookingType
            Dim TmpWeek As cWeek
            Dim TmpFilm As cFilm
            Dim TmpPLTarget As cPricelistTarget
            Dim TmpPlannedSpot As cPlannedSpot
            Dim TmpActualSpot As cActualSpot

            Dim TmpInt As Short
            Dim TmpLong As Integer
            Dim Tmpstr As String
            Dim CurrentlyReading As String
            Dim TmpFile As String

            'On Error GoTo ErrHandle

            If Path = "" And Not DoNotLoadFromFile Then
                LoadCampaign = 1
                Exit Function
            End If

            If Loading Or Saving Then Exit Function

            Loading = True


            Dim XMLDoc As Xml.XmlDocument = New Xml.XmlDocument
            Dim XMLCamp As Xml.XmlElement
            Dim XMLTmpNode As Xml.XmlElement
            Dim XMLTmpNode2 As Xml.XmlElement
            Dim XMLContract As Xml.XmlElement
            Dim TmpNode As Xml.XmlElement


            'On Error Resume Next
            _readOnly = False
            If DoNotLoadFromFile Then
                XMLDoc.LoadXml(XML)
            Else
                Try

                    ' Check if the file is readonly and warn the user about it
                    Dim infoReader As System.IO.FileInfo
                    infoReader = My.Computer.FileSystem.GetFileInfo(Path)
                    If (infoReader.IsReadOnly = True) Then
                        MessageBox.Show("The file your trying to open is Read-only. Trinity can't open it", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Return 1
                    End If

                    fs = New FileStream(Path, FileMode.Open, FileAccess.ReadWrite, FileShare.Read)
                    fs.Close()
                Catch ex As IOException
                    If Windows.Forms.MessageBox.Show("This file is probably being worked on by someone else. You will not be able to save." & vbCrLf & vbCrLf & "Continue?", "T R I N I T Y", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.No Then
                        Return 1
                    End If
                    _readOnly = True
                End Try
                Try
                    fs = New FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read)
                Catch ex2 As Exception
                    Windows.Forms.MessageBox.Show("Error opening file:" & vbCrLf & ex2.Message, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return 1
                End Try
                XMLDoc.Load(fs)
                ' XMLDoc.Load(Path)
            End If

            'On Error GoTo ErrHandle

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")

            IniFile.Create(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & mvarArea & "\area.ini")
            XMLCamp = XMLDoc.GetElementsByTagName("Campaign").Item(0)

            mvarVersion = XMLCamp.GetAttribute("Version")
            If mvarVersion > MY_VERSION Then
                MsgBox("This campaign is created with a later version of" & Chr(10) & "Trinity than the one you are currently using." & Chr(10) & "Contact the system administrator to get the latest version of the system.", MsgBoxStyle.Information, "TRINITY")
                LoadCampaign = 1
                Loading = False
                FileClose(99)
                Exit Function
            ElseIf mvarVersion < 40 Then
                LoadOldCampaign(Path)
                Exit Function
            End If

            '    mvarStartDate = XMLCamp.getAttribute("StartDate")
            '    mvarEndDate = XMLCamp.getAttribute("EndDate")
            Trinity.Helper.WriteToLogFile("Read basic variables")
            mvarName = XMLCamp.GetAttribute("Name")
            ID = XMLCamp.GetAttribute("ID")
            'If XMLCamp.GetAttribute("DatabaseID") <> "" Then DatabaseID = XMLCamp.GetAttribute("DatabaseID")
            mvarUpdatedTo = XMLCamp.GetAttribute("UpdatedTo")

            mvarPlanner = XMLCamp.GetAttribute("Planner")
            mvarBuyer = XMLCamp.GetAttribute("Buyer")
            'PlannerID = DBReader.getUserID(mvarPlanner)
            'BuyerID = DBReader.getUserID(mvarBuyer)
            If XMLCamp.GetAttribute("Cancelled") Is Nothing OrElse XMLCamp.GetAttribute("Cancelled") = "" Then
                mvarStatus = XMLCamp.GetAttribute("Status")
            Else
                If XMLCamp.GetAttribute("Cancelled") Then
                    mvarStatus = "Cancelled"
                End If
            End If
            If XMLCamp.GetAttribute("ExtranetDatabaseID") <> "" Then
                mvarExtranetDatabaseID = XMLCamp.GetAttribute("ExtranetDatabaseID")
            Else
                mvarExtranetDatabaseID = 0
            End If
            mvarFrequencyFocus = XMLCamp.GetAttribute("FrequencyFocus")
            If Not String.IsNullOrEmpty(XMLCamp.GetAttribute("WeeklyFrequency")) Then
                _weeklyFrequency = XMLCamp.GetAttribute("WeeklyFrequency")
            Else
                _weeklyFrequency = 1
            End If
            mvarFilename = XMLCamp.GetAttribute("Filename")
            ProductID = XMLCamp.GetAttribute("ProductID")
            ClientID = XMLCamp.GetAttribute("ClientID")
            mvarBudgetTotalCTC = XMLCamp.GetAttribute("BudgetTotalCTC")
            mvarActualTotCTC = XMLCamp.GetAttribute("ActualTotCTC")
            If Not XMLCamp.GetAttribute("MarathonCTC") Is Nothing Then mvarMarathonCTC = Val(XMLCamp.GetAttribute("MarathonCTC"))
            mvarCommentary = XMLCamp.GetAttribute("Commentary")
            'Start = 1
            'While InStr(Start, mvarCommentary, Chr(10)) > 0
            '    Start = InStr(Start, mvarCommentary, Chr(10))
            '    mvarCommentary = Left(mvarCommentary, Start - 1) & Chr(13) & Chr(10) & Mid(mvarCommentary, Start + 1)
            '    Start = Start + 2
            '    '        Mid(mvarCommentary, InStr(mvarCommentary, Chr(10))) = Chr(13)
            'End While
            mvarControlFilmcodeFromClient = XMLCamp.GetAttribute("ControlFilmcodeFromClient")

            If Not XMLCamp.GetAttribute("ContractID") = "" Then mvarContractID = XMLCamp.GetAttribute("ContractID")
            If XMLCamp.GetAttribute("ContractVersion") = "" OrElse DBReader.getContractSaveTime(mvarContractID) > CDate(XMLCamp.GetAttribute("ContractVersion")) Then
                mvarNewContractVersion = True
            End If
            If Not XMLCamp.GetAttribute("IsStripped") = "" Then IsStripped = XMLCamp.GetAttribute("IsStripped")

            mvarControlFilmcodeToBureau = XMLCamp.GetAttribute("ControlFilmcodeToBureau")
            mvarControlFilmcodeToChannels = XMLCamp.GetAttribute("ControlFilmcodeToChannels")
            mvarControlOKFromClient = XMLCamp.GetAttribute("ControlOKFromClient")
            mvarControlTracking = XMLCamp.GetAttribute("ControlTracking")
            mvarControlFollowedUp = XMLCamp.GetAttribute("ControlFollowedUp")
            mvarControlFollowUpToClient = XMLCamp.GetAttribute("ControlFollowUpToClient")
            mvarControlTransferredToMatrix = XMLCamp.GetAttribute("ControlTransferredToMatrix")
            If Not XMLCamp.GetAttribute("FilmindexAsDiscount") = "" Then
                mvarFilmindexAsDiscount = XMLCamp.GetAttribute("FilmindexAsDiscount")
            End If
            If Not XMLCamp.GetAttribute("ExtranetDatabaseID") = "" Then
                mvarExtranetDatabaseID = XMLCamp.GetAttribute("ExtranetDatabaseID")
            End If

            'Hannes added
            If Not XMLCamp.GetAttribute("MatrixID") = "" Then
                mvarMatrixID = XMLCamp.GetAttribute("MatrixID")
            End If

            Area = XMLCamp.GetAttribute("Area")
            mvarAreaLog = XMLCamp.GetAttribute("AreaLog")
            If mvarAreaLog = "" Then
                If mvarArea = "SE" Then
                    mvarAreaLog = "SEMMS"
                Else
                    mvarAreaLog = mvarArea
                End If
            End If
            mvarMarathonOtherOrder = XMLCamp.GetAttribute("MarathonOtherOrder")
            MarathonPlanNr = XMLCamp.GetAttribute("MarathonPlan")

            mvarHistoryDate = XMLCamp.GetAttribute("HistoryDate")
            HistoryComment = XMLCamp.GetAttribute("HistoryComment")


            XMLTmpNode = XMLCamp.GetElementsByTagName("Dayparts").Item(0).FirstChild

            _dayparts.Clear() '
            Dim _noPrime As Boolean = False
            While Not XMLTmpNode Is Nothing
                Dim _dp As New cDaypart
                If Not String.IsNullOrEmpty(XMLTmpNode.GetAttribute("Name")) Then
                    _dp.Name = XMLTmpNode.GetAttribute("Name")
                Else
                    _dp.Name = XMLTmpNode.LocalName
                End If
                _dp.StartMaM = XMLTmpNode.GetAttribute("Start")
                _dp.EndMaM = XMLTmpNode.GetAttribute("End")
                If Not XMLTmpNode.GetAttribute("IsPrime") = "" Then
                    _dp.IsPrime = XMLTmpNode.GetAttribute("IsPrime")
                Else
                    _noPrime = True
                End If
                _dayparts.Add(_dp)
                XMLTmpNode = XMLTmpNode.NextSibling
                i = i + 1
            End While
            If _noPrime Then
                'If no daypart is set as prime, then one containing 20:00 is set as prime
                Dayparts.GetDaypartForMam(20 * 60).IsPrime = True
            End If

            If Dayparts.Count = 0 Then
                Area = mvarArea
            End If

            If Not XMLCamp.GetAttribute("MultiplyAddedValues") = "" Then
                _multiplyAddedValues = XMLCamp.GetAttribute("MultiplyAddedValues")
            End If

            'load reach
            If Not XMLCamp.GetElementsByTagName("Reach") Is Nothing AndAlso XMLCamp.GetElementsByTagName("Reach").Count > 0 Then
                XMLTmpNode = XMLCamp.GetElementsByTagName("Reach").Item(0).FirstChild
                While Not XMLTmpNode Is Nothing
                    mvarReachGoal(XMLTmpNode.GetAttribute("Freq"), 0) = XMLTmpNode.GetAttribute("Reach")
                    XMLTmpNode = XMLTmpNode.NextSibling
                End While
            ElseIf Not XMLCamp.GetElementsByTagName("ReachMain") Is Nothing AndAlso XMLCamp.GetElementsByTagName("ReachMain").Count > 0 Then
                XMLTmpNode = XMLCamp.GetElementsByTagName("ReachMain").Item(0).FirstChild
                While Not XMLTmpNode Is Nothing
                    mvarReachGoal(XMLTmpNode.GetAttribute("Freq"), 0) = XMLTmpNode.GetAttribute("Reach")
                    XMLTmpNode = XMLTmpNode.NextSibling
                End While
                XMLTmpNode = XMLCamp.GetElementsByTagName("ReachSecond").Item(0).FirstChild
                While Not XMLTmpNode Is Nothing
                    mvarReachGoal(XMLTmpNode.GetAttribute("Freq"), 1) = XMLTmpNode.GetAttribute("Reach")
                    XMLTmpNode = XMLTmpNode.NextSibling
                End While
            End If


            'Read the MarathonInsertions node into memory
            If Not XMLCamp.GetElementsByTagName("MarathonInsertions") Is Nothing AndAlso XMLCamp.GetElementsByTagName("MarathonInsertions").Count > 0 Then
                MarathonInsertions = XMLCamp.GetElementsByTagName("MarathonInsertions").Item(0)
            End If

            mvarAllAdults = XMLCamp.GetAttribute("AllAdults")
            If mvarAllAdults = "" Then
                Ini.Create(TrinitySettings.LocalDataPath & "\Data\" & mvarArea & "\Area.ini")
                mvarAllAdults = Ini.Text("General", "EntirePopulation")
            End If

            'Load linked campaigns

            _linkedCampaigns.Clear()
            If XMLCamp.SelectSingleNode("LinkedCampaigns") IsNot Nothing Then
                _autoLinkCampaigns = DirectCast(XMLCamp.SelectSingleNode("LinkedCampaigns"), Xml.XmlElement).GetAttribute("AutoLink")
                For Each XMLTmpNode In XMLCamp.SelectSingleNode("LinkedCampaigns").ChildNodes
                    Dim _link As New cLinkedCampaign
                    _link.ClientID = XMLTmpNode.GetAttribute("ClientID")
                    _link.Link = XMLTmpNode.GetAttribute("Link")
                    _link.Name = XMLTmpNode.GetAttribute("Name")
                    _link.Path = XMLTmpNode.GetAttribute("Path")
                    If XMLTmpNode.GetAttribute("DatabaseID") IsNot Nothing AndAlso Not XMLTmpNode.GetAttribute("DatabaseID") = "" Then _link.DatabaseID = XMLTmpNode.GetAttribute("DatabaseID")
                    _link.ProductID = XMLTmpNode.GetAttribute("ProductID")
                    _linkedCampaigns.Add(_link)
                Next
            End If

            'Load Hidden Problems
            XMLTmpNode = XMLCamp.SelectSingleNode("HiddenProblems")
            TrinitySettings.ShowAllProblems()
            If XMLTmpNode IsNot Nothing Then
                For Each XMLTmpNode2 In XMLTmpNode.ChildNodes
                    Dim _type As String = XMLTmpNode2.GetAttribute("SourceType")
                    Dim _problemID As Integer = XMLTmpNode2.GetAttribute("ProblemID")
                    TrinitySettings.DisplayProblem(_type, _problemID) = False
                Next
            End If

            'Load channels and all sub-collections and objects

            Dim XMLChannel As Xml.XmlElement
            Dim XMLBookingType As Xml.XmlElement
            Dim XMLWeek As Xml.XmlElement
            Dim XMLFilm As Xml.XmlElement
            Dim XMLTarget As Xml.XmlElement
            Dim XMLBuyTarget As Xml.XmlElement
            Dim XMLPricelist As Xml.XmlElement
            Dim XMLSpot As Xml.XmlElement

            XMLTarget = XMLCamp.SelectSingleNode("MainTarget")
            If XMLTarget.GetAttribute("Type") <> "" AndAlso XMLTarget.GetAttribute("Type") > 0 Then
                Try
                    mvarInternalAdedge.setTargetUserDefined(XMLTarget.GetAttribute("TargetGroup"), XMLTarget.GetAttribute("Name"), True)
                    mvarMainTarget.TargetType = XMLTarget.GetAttribute("Type")
                    mvarMainTarget.TargetGroup = XMLTarget.GetAttribute("TargetGroup")
                    mvarMainTarget.TargetName = XMLTarget.GetAttribute("Name")
                Catch
                    If ReplacementTargets.ContainsKey(XMLTarget.GetAttribute("Name")) Then
                        mvarMainTarget.TargetGroup = ReplacementTargets(XMLTarget.GetAttribute("Name")).GroupName
                        mvarMainTarget.TargetName = ReplacementTargets(XMLTarget.GetAttribute("Name")).TargetName
                    Else
                        frmFindTarget.ShowDialog("The target '" & XMLTarget.GetAttribute("Name") & "' does not exist. Please choose replacement.")
                        mvarMainTarget.TargetType = XMLTarget.GetAttribute("Type")
                        mvarMainTarget.TargetGroup = frmFindTarget.tvwTargets.SelectedNode.Tag
                        mvarMainTarget.TargetName = frmFindTarget.tvwTargets.SelectedNode.Text
                        Dim TmpTarget As New AdedgeTargetStruct With {.GroupName = mvarMainTarget.TargetGroup, .TargetName = mvarMainTarget.TargetName}
                        ReplacementTargets.Add(XMLTarget.GetAttribute("Name"), TmpTarget)
                    End If
                End Try
            Else
                mvarMainTarget.TargetType = XMLTarget.GetAttribute("Type")
                mvarMainTarget.TargetGroup = XMLTarget.GetAttribute("TargetGroup")
                mvarMainTarget.TargetName = XMLTarget.GetAttribute("Name")
            End If

            'On Error GoTo ErrHandle
            mvarMainTarget.Universe = XMLTarget.GetAttribute("Universe")
            mvarMainTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")

            XMLTarget = XMLCamp.SelectSingleNode("SecondaryTarget")
            If XMLTarget.GetAttribute("Type") <> "" AndAlso XMLTarget.GetAttribute("Type") > 0 Then
                Try
                    mvarInternalAdedge.setTargetUserDefined(XMLTarget.GetAttribute("TargetGroup"), XMLTarget.GetAttribute("Name"), True)
                    mvarSecondaryTarget.TargetType = XMLTarget.GetAttribute("Type")
                    mvarSecondaryTarget.TargetGroup = XMLTarget.GetAttribute("TargetGroup")
                    mvarSecondaryTarget.TargetName = XMLTarget.GetAttribute("Name")
                Catch
                    If ReplacementTargets.ContainsKey(XMLTarget.GetAttribute("Name")) Then
                        mvarMainTarget.TargetGroup = ReplacementTargets(XMLTarget.GetAttribute("Name")).GroupName
                        mvarMainTarget.TargetName = ReplacementTargets(XMLTarget.GetAttribute("Name")).TargetName
                    Else
                        frmFindTarget.ShowDialog("The target '" & XMLTarget.GetAttribute("Name") & "' does not exist. Please choose replacement.")
                        mvarSecondaryTarget.TargetType = XMLTarget.GetAttribute("Type")
                        mvarSecondaryTarget.TargetGroup = frmFindTarget.tvwTargets.SelectedNode.Tag
                        mvarSecondaryTarget.TargetName = frmFindTarget.tvwTargets.SelectedNode.Text
                        Dim TmpTarget As New AdedgeTargetStruct With {.GroupName = mvarSecondaryTarget.TargetGroup, .TargetName = mvarSecondaryTarget.TargetName}
                        ReplacementTargets.Add(XMLTarget.GetAttribute("Name"), TmpTarget)
                    End If
                End Try
            Else
                mvarSecondaryTarget.TargetType = XMLTarget.GetAttribute("Type")
                mvarSecondaryTarget.TargetGroup = XMLTarget.GetAttribute("TargetGroup")
                mvarSecondaryTarget.TargetName = XMLTarget.GetAttribute("Name")
            End If
            'On Error GoTo ErrHandle
            mvarSecondaryTarget.Universe = XMLTarget.GetAttribute("Universe")
            mvarSecondaryTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")

            XMLTarget = XMLCamp.SelectSingleNode("ThirdTarget")
            If XMLTarget.GetAttribute("Type") <> "" AndAlso XMLTarget.GetAttribute("Type") > 0 Then
                Try
                    mvarInternalAdedge.setTargetUserDefined(XMLTarget.GetAttribute("TargetGroup"), XMLTarget.GetAttribute("Name"), True)
                    mvarThirdTarget.TargetType = Val(XMLTarget.GetAttribute("Type"))
                    mvarThirdTarget.TargetGroup = XMLTarget.GetAttribute("TargetGroup")
                    mvarThirdTarget.TargetName = XMLTarget.GetAttribute("Name")
                Catch
                    If ReplacementTargets.ContainsKey(XMLTarget.GetAttribute("Name")) Then
                        mvarMainTarget.TargetGroup = ReplacementTargets(XMLTarget.GetAttribute("Name")).GroupName
                        mvarMainTarget.TargetName = ReplacementTargets(XMLTarget.GetAttribute("Name")).TargetName
                    Else
                        frmFindTarget.ShowDialog("The target '" & XMLTarget.GetAttribute("Name") & "' does not exist. Please choose replacement.")
                        mvarThirdTarget.TargetType = Val(XMLTarget.GetAttribute("Type"))
                        mvarThirdTarget.TargetGroup = frmFindTarget.tvwTargets.SelectedNode.Tag
                        mvarThirdTarget.TargetName = frmFindTarget.tvwTargets.SelectedNode.Text
                        Dim TmpTarget As New AdedgeTargetStruct With {.GroupName = mvarThirdTarget.TargetGroup, .TargetName = mvarThirdTarget.TargetName}
                        ReplacementTargets.Add(XMLTarget.GetAttribute("Name"), TmpTarget)
                    End If
                End Try
            Else
                mvarThirdTarget.TargetType = Val(XMLTarget.GetAttribute("Type"))
                mvarThirdTarget.TargetGroup = XMLTarget.GetAttribute("TargetGroup")
                mvarThirdTarget.TargetName = XMLTarget.GetAttribute("Name")
            End If

            mvarThirdTarget.Universe = XMLTarget.GetAttribute("Universe")
            mvarThirdTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")

            mvarChannels = New cChannels(Me)
            AddChannelsEventHandling()
            mvarChannels.MainObject = Me

            XMLChannel = XMLCamp.SelectSingleNode("Channels").FirstChild

            While Not XMLChannel Is Nothing

                TmpString = XMLChannel.GetAttribute("Name")
                TmpFile = XMLChannel.GetAttribute("fileName")
                TmpChannel = mvarChannels.Add(TmpString, TmpFile)
                TmpChannel.ID = XMLChannel.GetAttribute("ID")
                TmpChannel.Shortname = XMLChannel.GetAttribute("ShortName")
                TmpChannel.BuyingUniverse = XMLChannel.GetAttribute("BuyingUniverse")
                TmpChannel.AdEdgeNames = XMLChannel.GetAttribute("AdEdgeNames")
                If TmpChannel.AdEdgeNames = "Kan 5" Then
                    TmpChannel.AdEdgeNames = "Kanal 5"
                End If
                TmpChannel.DefaultArea = XMLChannel.GetAttribute("DefaultArea")
                TmpChannel.AgencyCommission = Conv(XMLChannel.GetAttribute("AgencyCommission"))
                If Not IsDBNull(XMLChannel.GetAttribute("DeliveryAddress")) Then
                    TmpChannel.DeliveryAddress = XMLChannel.GetAttribute("DeliveryAddress")
                End If
                TmpChannel.MarathonName = XMLChannel.GetAttribute("Marathon")
                TmpChannel.ConnectedChannel = XMLChannel.GetAttribute("ConnectedChannel")
                TmpChannel.ChannelGroup = XMLChannel.GetAttribute("ChannelGroup")
                If XMLChannel.GetAttribute("AdtooxID") <> "" Then TmpChannel.AdTooxChannelID = XMLChannel.GetAttribute("AdtooxID")
                If Not XMLChannel.GetAttribute("UseBillboards") = "" Then
                    TmpChannel.UseBillboards = XMLChannel.GetAttribute("UseBillboards")
                    TmpChannel.UseBreakBumpers = XMLChannel.GetAttribute("UseBreakbumpers")
                End If

                'Save the targets

                XMLTarget = XMLChannel.GetElementsByTagName("MainTarget").Item(0)
                TmpChannel.MainTarget.NoUniverseSize = True
                TmpChannel.MainTarget.TargetName = XMLTarget.GetAttribute("Name")
                TmpChannel.MainTarget.TargetType = Val(XMLTarget.GetAttribute("Type"))
                TmpChannel.MainTarget.Universe = XMLTarget.GetAttribute("Universe")
                TmpChannel.MainTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")

                TmpChannel.MainTarget.NoUniverseSize = False

                XMLTarget = XMLChannel.GetElementsByTagName("SecondaryTarget").Item(0)
                TmpChannel.SecondaryTarget.NoUniverseSize = True
                TmpChannel.SecondaryTarget.TargetName = XMLTarget.GetAttribute("Name")
                TmpChannel.SecondaryTarget.TargetType = XMLTarget.GetAttribute("Type")
                TmpChannel.SecondaryTarget.Universe = XMLTarget.GetAttribute("Universe")
                TmpChannel.SecondaryTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")

                TmpChannel.SecondaryTarget.NoUniverseSize = False
                If Not IsDBNull(XMLChannel.GetAttribute("Marathon")) Then
                    TmpChannel.MarathonName = XMLChannel.GetAttribute("Marathon")
                End If
                If Not IsDBNull(XMLChannel.GetAttribute("Penalty")) AndAlso XMLChannel.GetAttribute("Penalty") <> "" Then
                    TmpChannel.Penalty = XMLChannel.GetAttribute("Penalty")
                Else
                    TmpChannel.Penalty = 0
                End If
                If Not IsDBNull(XMLChannel.GetAttribute("AdTooxID")) AndAlso XMLChannel.GetAttribute("AdTooxID") <> "" Then
                    TmpChannel.AdTooxChannelID = XMLChannel.GetAttribute("AdTooxID")
                End If

                'Read Booking types

                XMLBookingType = XMLChannel.GetElementsByTagName("BookingTypes").Item(0).FirstChild
                While Not XMLBookingType Is Nothing

                    TmpString = XMLBookingType.GetAttribute("Name")
                    TmpBookingType = TmpChannel.BookingTypes.Add(TmpString)
                    TmpBookingType.Shortname = XMLBookingType.GetAttribute("Shortname")

                    'Save the rest of the booking type
                    If XMLBookingType.GetAttribute("ManualIndexes") = "" Or XMLBookingType.GetAttribute("ManualIndexes") = "False" Then
                        TmpBookingType.ManualIndexes = False
                    Else
                        TmpBookingType.ManualIndexes = True
                    End If

                    TmpBookingType.IndexMainTarget = Conv(XMLBookingType.GetAttribute("IndexMainTarget"))
                    If Not XMLBookingType.GetAttribute("IndexSecondTarget") = "" Then
                        TmpBookingType.IndexSecondTarget = Conv(XMLBookingType.GetAttribute("IndexSecondTarget"))
                    End If
                    TmpBookingType.IndexAllAdults = Conv(XMLBookingType.GetAttribute("IndexAllAdults"))
                    If XMLBookingType.GetAttribute("IndexMainTargetStatus") = "" Then
                        TmpBookingType.IndexMainTargetStatus = cBookingType.IndexStatusEnum.Unknown
                        TmpBookingType.IndexSecondTargetStatus = cBookingType.IndexStatusEnum.Unknown
                        TmpBookingType.IndexAllAdultsStatus = cBookingType.IndexStatusEnum.Unknown
                    Else
                        TmpBookingType.IndexMainTargetStatus = Conv(XMLBookingType.GetAttribute("IndexMainTargetStatus"))
                        TmpBookingType.IndexSecondTargetStatus = Conv(XMLBookingType.GetAttribute("IndexSecondTargetStatus"))
                        TmpBookingType.IndexAllAdultsStatus = Conv(XMLBookingType.GetAttribute("IndexAllAdultsStatus"))
                    End If
                    XMLTmpNode = XMLBookingType.GetElementsByTagName("Dayparts").Item(0)
                    If XMLTmpNode Is Nothing Then
                        XMLTmpNode = XMLBookingType.GetElementsByTagName("DaypartSplit").Item(0)
                    End If
                    XMLTmpNode2 = XMLTmpNode.FirstChild
                    TmpBookingType.Dayparts.Clear()
                    While Not XMLTmpNode2 Is Nothing
                        Dim _dp As New cDaypart
                        If Not String.IsNullOrEmpty(XMLTmpNode2.GetAttribute("Name")) Then
                            _dp.Name = XMLTmpNode2.GetAttribute("Name")
                        Else
                            _dp.Name = XMLTmpNode2.LocalName
                        End If
                        If XMLTmpNode2.GetAttribute("Start") <> "" Then
                            _dp.StartMaM = XMLTmpNode2.GetAttribute("Start")
                            _dp.EndMaM = XMLTmpNode2.GetAttribute("End")
                            _dp.IsPrime = XMLTmpNode2.GetAttribute("IsPrime")
                        Else
                            _dp.StartMaM = Dayparts(_dp.Name).StartMaM
                            _dp.EndMaM = Dayparts(_dp.Name).EndMaM
                            _dp.IsPrime = Dayparts(_dp.Name).IsPrime
                        End If
                        If XMLTmpNode2.GetAttribute("Share") <> "" Then
                            _dp.Share = Conv(XMLTmpNode2.GetAttribute("Share"))
                        Else
                            If XMLTmpNode2.GetAttribute("DefaultSplit") <> "" Then
                                _dp.Share = XMLTmpNode2.GetAttribute("DefaultSplit")
                            End If
                        End If
                        TmpBookingType.Dayparts.Add(_dp)
                        XMLTmpNode2 = XMLTmpNode2.NextSibling
                    End While

                    'Load the default daypart split.
                    'This xml may not always be set
                    XMLTmpNode = XMLBookingType.GetElementsByTagName("DefaultDaypartSplit").Item(0)
                    If Not XMLTmpNode Is Nothing Then
                        For i = 0 To TmpBookingType.Dayparts.Count - 1
                            XMLTmpNode2 = XMLTmpNode.GetElementsByTagName(TmpBookingType.Dayparts(i).Name).Item(0)
                            TmpBookingType.DefaultDaypart(i) = Conv(XMLTmpNode2.GetAttribute("DefaultSplit"))
                        Next
                    End If

                    'Read Spotlight info
                    TmpBookingType.BookingIdSpotlight = XMLBookingType.GetAttribute("BookingIdSpotlight")
                    TmpBookingType.BookingUrlSpotlight = XMLBookingType.GetAttribute("BookingUrlSpotlight")
                    If Conv(XMLBookingType.GetAttribute("BookingConfirmationVersion")) IsNot "" Then
                        TmpBookingType.BookingConfirmationVersion = Conv(XMLBookingType.GetAttribute("BookingConfirmationVersion"))
                    End If
                    TmpBookingType.BookingAgencyRefNo = XMLBookingType.GetAttribute("BookingAgencyRefNo")
                    TmpBookingType.CampRefNo = XMLBookingType.GetAttribute("CampaignRefNo")

                    TmpBookingType.BookIt = XMLBookingType.GetAttribute("BookIt")
                    'TmpBookingType.GrossCPP = Conv(XMLBookingType.GetAttribute("GrossCPP"))
                    'TmpBookingType.AverageRating = Conv(XMLBookingType.GetAttribute("AverageRating")) ***** MOVED TO BELOW ALL WEEKS ARE READ
                    TmpBookingType.ConfirmedNetBudget = Conv(XMLBookingType.GetAttribute("ConfirmedNetBudget"))
                    TmpBookingType.Bookingtype = XMLBookingType.GetAttribute("Bookingtype")
                    TmpBookingType.ContractNumber = XMLBookingType.GetAttribute("ContractNumber")
                    TmpBookingType.IsRBS = XMLBookingType.GetAttribute("IsRBS")
                    TmpBookingType.IsSpecific = XMLBookingType.GetAttribute("IsSpecific")
                    If XMLBookingType.GetAttribute("IsPremium") = "" Then
                        TmpBookingType.IsPremium = False
                    Else
                        TmpBookingType.IsPremium = XMLBookingType.GetAttribute("IsPremium")
                        If XMLBookingType.GetAttribute("SpecificsFactor") <> "" Then TmpBookingType.EnhancementFactor = XMLBookingType.GetAttribute("SpecificsFactor")
                    End If
                    If XMLBookingType.GetAttribute("IsCompensation") = "" Then
                        TmpBookingType.IsCompensation = False
                    Else
                        TmpBookingType.IsCompensation = XMLBookingType.GetAttribute("IsCompensation")
                    End If
                    If XMLBookingType.GetAttribute("IsSponsorship") = "" Then
                        TmpBookingType.IsSponsorship = False
                    Else
                        TmpBookingType.IsSponsorship = XMLBookingType.GetAttribute("IsSponsorship")
                    End If
                    TmpBookingType.Comments = XMLBookingType.GetAttribute("Comments")
                    If XMLBookingType.GetAttribute("PrintDayparts") = "" Then
                        TmpBookingType.PrintDayparts = True
                    Else
                        TmpBookingType.PrintDayparts = XMLBookingType.GetAttribute("PrintDayparts")
                    End If
                    If XMLBookingType.GetAttribute("PrintBookingCode") = "" Then
                        TmpBookingType.PrintBookingCode = True
                    Else
                        TmpBookingType.PrintBookingCode = XMLBookingType.GetAttribute("PrintBookingCode")
                    End If
                    If XMLBookingType.GetAttribute("MaxDiscount") = "" Then
                        TmpBookingType.MaxDiscount = 1
                    Else
                        TmpBookingType.MaxDiscount = XMLBookingType.GetAttribute("MaxDiscount")
                    End If


                    '            TmpBookingtype.Discount = XMLBookingType.getAttribute("Discount")
                    '            TmpBookingtype.NetCPT = Conv(XMLBookingType.getAttribute("NetCPT"))
                    '            TmpBookingtype.NetCPP = Conv(XMLBookingType.getAttribute("NetCPP"))
                    '            If Not IsNull(XMLBookingType.getAttribute("IsEntered")) Then
                    '                TmpBookingtype.IsEntered = XMLBookingType.getAttribute("IsEntered")
                    '            End If

                    If Not IsDBNull(XMLBookingType.GetAttribute("OrderNumber")) Then
                        TmpBookingType.OrderNumber = XMLBookingType.GetAttribute("OrderNumber")
                    Else
                        TmpBookingType.OrderNumber = ""
                    End If

                    TmpBookingType.SpecificSponsringPrograms.Clear()
                    If Not XMLBookingType.GetElementsByTagName("SpecSpons") Is Nothing AndAlso XMLBookingType.GetElementsByTagName("SpecSpons").Count > 0 Then
                        XMLTmpNode = XMLBookingType.GetElementsByTagName("SpecSpons").Item(0).FirstChild
                        While Not XMLTmpNode Is Nothing
                            TmpBookingType.SpecificSponsringPrograms.Add(XMLTmpNode.GetAttribute("Name"))
                            XMLTmpNode = XMLTmpNode.NextSibling
                        End While
                    End If

                    'Read the pricelist
                    If Not LoadStripped AndAlso Not XMLBookingType.GetElementsByTagName("Pricelist").Item(0) Is Nothing Then
                        XMLPricelist = XMLBookingType.GetElementsByTagName("Pricelist").Item(0)

                        TmpBookingType.Pricelist.StartDate = XMLPricelist.GetAttribute("StartDate")
                        TmpBookingType.Pricelist.EndDate = XMLPricelist.GetAttribute("EndDate")
                        TmpBookingType.Pricelist.BuyingUniverse = XMLPricelist.GetAttribute("BuyingUniverse")

                        XMLBuyTarget = XMLPricelist.GetElementsByTagName("Targets").Item(0).FirstChild

                        While Not XMLBuyTarget Is Nothing

                            TmpString = XMLBuyTarget.GetAttribute("Name")
                            TmpPLTarget = TmpBookingType.Pricelist.Targets.Add(TmpString, TmpBookingType)
                            TmpPLTarget.CalcCPP = XMLBuyTarget.GetAttribute("CalcCPP")

                            'XMLTmpNode = XMLBuyTarget.GetElementsByTagName("DaypartSplit").Item(0)
                            'For i = 0 To TmpBookingType.Dayparts.Count - 1
                            '    XMLTmpNode2 = XMLTmpNode.GetElementsByTagName(TmpBookingType.Dayparts(i).Name).Item(0)
                            '    TmpPLTarget.DefaultDaypart(i) = Conv(XMLTmpNode2.GetAttribute("DefaultSplit"))
                            'Next
                            TmpPLTarget.Bookingtype = TmpBookingType
                            If XMLBuyTarget.GetAttribute("MaxRatings") <> "" Then
                                TmpPLTarget.MaxRatings = CSng(XMLBuyTarget.GetAttribute("MaxRatings"))
                            Else
                                TmpPLTarget.MaxRatings = 0
                            End If
                            XMLTarget = XMLBuyTarget.GetElementsByTagName("Target").Item(0)
                            TmpPLTarget.Target.NoUniverseSize = True 'For speed. No unisizes are calculated
                            TmpPLTarget.Target.Universe = XMLTarget.GetAttribute("Universe")
                            TmpPLTarget.Target.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
                            If XMLTarget.GetAttribute("Type") <> "" AndAlso XMLTarget.GetAttribute("Type") > 0 Then
                                TmpBookingType.BuyingTarget.Target.TargetType = XMLTarget.GetAttribute("Type")
                                Try
                                    mvarInternalAdedge.setTargetUserDefined(XMLTarget.GetAttribute("TargetGroup"), XMLTarget.GetAttribute("Name"), True)
                                    TmpPLTarget.Target.TargetGroup = XMLTarget.GetAttribute("TargetGroup")
                                    TmpPLTarget.Target.TargetName = XMLTarget.GetAttribute("Name")
                                Catch
                                    If ReplacementTargets.ContainsKey(XMLTarget.GetAttribute("Name")) Then
                                        TmpPLTarget.Target.TargetGroup = ReplacementTargets(XMLTarget.GetAttribute("Name")).GroupName
                                        TmpPLTarget.Target.TargetName = ReplacementTargets(XMLTarget.GetAttribute("Name")).TargetName
                                    Else
                                        frmFindTarget.ShowDialog("The target '" & XMLTarget.GetAttribute("Name") & " in channel " & TmpChannel.ChannelName & " and booking type " & TmpBookingType.Name & "' does not exist. Please choose replacement.")
                                        TmpPLTarget.Target.TargetGroup = frmFindTarget.tvwTargets.SelectedNode.Tag
                                        TmpPLTarget.Target.TargetName = frmFindTarget.tvwTargets.SelectedNode.Text
                                        Dim TmpTarget As New AdedgeTargetStruct With {.GroupName = TmpPLTarget.Target.TargetGroup, .TargetName = TmpPLTarget.Target.TargetName}
                                        ReplacementTargets.Add(XMLTarget.GetAttribute("Name"), TmpTarget)
                                    End If
                                End Try
                            Else
                                TmpPLTarget.Target.TargetName = XMLTarget.GetAttribute("Name")
                            End If
                            TmpPLTarget.Target.TargetType = XMLTarget.GetAttribute("Type")
                            'On Error GoTo ErrHandle

                            '****MOVED
                            'TmpPLTarget.Target.NoUniverseSize = False
                            'TmpPLTarget.StandardTarget = XMLTarget.GetAttribute("StandardTarget")
                            'If Not IsDBNull(XMLBookingType.GetAttribute("IsEntered")) Then
                            '    TmpPLTarget.IsEntered = XMLBuyTarget.GetAttribute("IsEntered")
                            '    If TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
                            '        TmpPLTarget.NetCPP = Conv(XMLBuyTarget.GetAttribute("NetCPP"))
                            '    ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPT Then
                            '        TmpPLTarget.NetCPT = Conv(XMLBuyTarget.GetAttribute("NetCPT"))
                            '    ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eDiscount Then
                            '        TmpPLTarget.Discount = Conv(XMLBuyTarget.GetAttribute("Discount"))
                            '    End If
                            'Else
                            '    TmpPLTarget.Discount = Conv(XMLBuyTarget.GetAttribute("Discount"))
                            '    TmpPLTarget.NetCPT = Conv(XMLBuyTarget.GetAttribute("NetCPT"))
                            '    TmpPLTarget.NetCPP = Conv(XMLBuyTarget.GetAttribute("NetCPP"))
                            'End If
                            '********************

                            'some old pricelists has no index row
                            If XMLBuyTarget.GetElementsByTagName("Indexes").Count = 0 AndAlso XMLBuyTarget.GetElementsByTagName("PricelistPeriods").Count = 0 Then
                                Dim period As cPricelistPeriod
                                period = TmpBookingType.BuyingTarget.PricelistPeriods.Add("All Year")
                                period.FromDate = New Date(2007, 1, 1)
                                period.ToDate = New Date(2007, 12, 31)
                                period.TargetNat = XMLTmpNode.GetAttribute("TargetNat")
                                period.TargetUni = XMLTmpNode.GetAttribute("TargetUni")
                                period.Price(True) = XMLTmpNode.GetAttribute("CPP")

                                For i = 0 To TmpBookingType.Dayparts.Count - 1
                                    Dim cpp As String = XMLTmpNode.GetAttribute("CPP_DP" & i + 1)
                                    If cpp = "" Then cpp = "0"
                                    period.Price(True, i) = cpp
                                Next
                            End If

                            'old pricelists saved in camapign
                            If Not XMLBuyTarget.GetElementsByTagName("Indexes").Item(0) Is Nothing Then
                                TmpNode = XMLBuyTarget.GetElementsByTagName("Indexes").Item(0).FirstChild
                                If XMLBuyTarget.GetElementsByTagName("Indexes").Item(0).HasChildNodes Then
                                    While Not TmpNode Is Nothing
                                        TmpPLTarget.PricelistPeriods.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("ID"))
                                        TmpPLTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).FromDate = TmpNode.GetAttribute("FromDate")
                                        TmpPLTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).ToDate = TmpNode.GetAttribute("ToDate")
                                        TmpPLTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).TargetNat = XMLBuyTarget.GetAttribute("UniSizeNat")
                                        TmpPLTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).TargetUni = XMLBuyTarget.GetAttribute("UniSize")

                                        If TmpPLTarget.CalcCPP Then
                                            XMLTmpNode = XMLBuyTarget.GetElementsByTagName("DaypartCPP").Item(0)
                                            For i = 0 To TmpBookingType.Dayparts.Count - 1
                                                XMLTmpNode2 = XMLTmpNode.GetElementsByTagName(TmpBookingType.Dayparts(i).Name).Item(0)
                                                Dim cpp As String = XMLTmpNode2.GetAttribute("CPP")
                                                Dim index As String = TmpNode.GetAttribute("IndexDP" & i)
                                                If cpp = "" Then cpp = "0"
                                                If index = "" Then index = "0"
                                                TmpPLTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).Price(True, i) = index * cpp / 100
                                            Next
                                        Else
                                            TmpPLTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).Price(True) = Conv(XMLBuyTarget.GetAttribute("CPP")) * TmpNode.GetAttribute("IndexDP0") / 100
                                        End If
                                        TmpNode = TmpNode.NextSibling
                                    End While
                                Else
                                    Dim period As cPricelistPeriod
                                    period = TmpPLTarget.PricelistPeriods.Add("All Year")
                                    period.FromDate = Date.FromOADate(StartDate)
                                    period.ToDate = Date.FromOADate(EndDate)
                                    period.TargetNat = XMLBuyTarget.GetAttribute("UniSizeNat")
                                    period.TargetUni = XMLBuyTarget.GetAttribute("UniSize")
                                    period.Price(True) = XMLBuyTarget.GetAttribute("CPP")
                                End If
                            End If

                            'new pricelists load into campaign
                            If Not XMLBuyTarget.GetElementsByTagName("PricelistPeriods").Item(0) Is Nothing Then
                                TmpNode = XMLBuyTarget.GetElementsByTagName("PricelistPeriods").Item(0).FirstChild
                                While Not TmpNode Is Nothing
                                    TmpPLTarget.PricelistPeriods.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("ID"))
                                    TmpPLTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).FromDate = TmpNode.GetAttribute("FromDate")
                                    TmpPLTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).ToDate = TmpNode.GetAttribute("ToDate")
                                    TmpPLTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).PriceIsCPP = TmpNode.GetAttribute("isCPP")
                                    If TmpPLTarget.CalcCPP Then
                                        For i = 0 To TmpBookingType.Dayparts.Count - 1
                                            If IsNumeric(TmpNode.GetAttribute("PriceDP" & i)) Then
                                                TmpPLTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).Price(TmpNode.GetAttribute("isCPP"), i) = TmpNode.GetAttribute("PriceDP" & i)
                                            Else
                                                TmpPLTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).Price(TmpNode.GetAttribute("isCPP"), i) = 0
                                            End If
                                        Next
                                    Else
                                        If IsNumeric(TmpNode.GetAttribute("Price")) Then
                                            TmpPLTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).Price(TmpNode.GetAttribute("isCPP")) = TmpNode.GetAttribute("Price")
                                        Else
                                            TmpPLTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).Price(TmpNode.GetAttribute("isCPP")) = 0
                                        End If
                                    End If
                                    TmpPLTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).TargetNat = Conv(TmpNode.GetAttribute("UniSizeNat"))
                                    TmpPLTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).TargetUni = Conv(TmpNode.GetAttribute("UniSize"))
                                    TmpNode = TmpNode.NextSibling
                                End While
                            End If
                            If Not XMLBuyTarget.GetElementsByTagName("PricelistIndexes").Item(0) Is Nothing Then
                                TmpNode = XMLBuyTarget.GetElementsByTagName("PricelistIndexes").Item(0).FirstChild
                                While Not TmpNode Is Nothing
                                    Dim TmpIndex As cIndex = TmpPLTarget.Indexes.Add(TmpNode.GetAttribute("Name"))
                                    TmpIndex.FromDate = TmpNode.GetAttribute("FromDate")
                                    TmpIndex.ID = TmpNode.GetAttribute("ID")
                                    TmpIndex.Index = TmpNode.GetAttribute("Index")
                                    TmpIndex.IndexOn = TmpNode.GetAttribute("IndexOn")
                                    TmpIndex.ToDate = TmpNode.GetAttribute("ToDate")
                                    If TmpNode.GetAttribute("UseThis") = "" Then
                                        TmpIndex.UseThis = True
                                    Else
                                        TmpIndex.UseThis = TmpNode.GetAttribute("UseThis")
                                    End If
                                    TmpNode = TmpNode.NextSibling
                                End While
                            End If

                            If Conv(XMLBuyTarget.GetAttribute("MaxRatings")) <> "" Then
                                TmpPLTarget.MaxRatings = Conv(XMLBuyTarget.GetAttribute("MaxRatings"))
                            Else
                                TmpPLTarget.MaxRatings = 0
                            End If
                            TmpPLTarget.Target.NoUniverseSize = False
                            TmpPLTarget.StandardTarget = XMLTarget.GetAttribute("StandardTarget")
                            If Not IsDBNull(XMLBookingType.GetAttribute("IsEntered")) Then
                                TmpPLTarget.IsEntered = XMLBuyTarget.GetAttribute("IsEntered")
                                If TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
                                    TmpPLTarget.NetCPP = Conv(XMLBuyTarget.GetAttribute("NetCPP"))
                                ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPT Then
                                    TmpPLTarget.NetCPT = Conv(XMLBuyTarget.GetAttribute("NetCPT"))
                                ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eDiscount Then
                                    TmpPLTarget.Discount = Conv(XMLBuyTarget.GetAttribute("Discount"))
                                ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eEnhancement Then
                                    TmpPLTarget.Enhancement = Conv(XMLBuyTarget.GetAttribute("Enhancement"))
                                End If
                            Else
                                TmpPLTarget.Discount = Conv(XMLBuyTarget.GetAttribute("Discount"))
                                TmpPLTarget.NetCPT = Conv(XMLBuyTarget.GetAttribute("NetCPT"))
                                TmpPLTarget.NetCPP = Conv(XMLBuyTarget.GetAttribute("NetCPP"))
                                TmpPLTarget.Enhancement = Conv(XMLBuyTarget.GetAttribute("Enhancement"))

                            End If


                            XMLBuyTarget = XMLBuyTarget.NextSibling
                        End While
                        If Not TmpBookingType.BuyingTarget.TargetName = "" AndAlso Not TmpBookingType.Pricelist.Targets(TmpBookingType.BuyingTarget.TargetName) Is Nothing AndAlso TmpBookingType.BuyingTarget.Target.TargetName <> TmpBookingType.Pricelist.Targets(TmpBookingType.BuyingTarget.TargetName).Target.TargetName Then
                            TmpBookingType.BuyingTarget.Target.TargetName = TmpBookingType.Pricelist.Targets(TmpBookingType.BuyingTarget.TargetName).Target.TargetName
                        End If
                        'Load Indexes, AddedValues, Compensations and SpotIndexes
                    End If
                    TmpNode = XMLBookingType.GetElementsByTagName("Indexes").Item(0).FirstChild
                    While Not TmpNode Is Nothing
                        If TmpNode.GetAttribute("IndexOn") < cIndex.IndexOnEnum.eFixedCPP Then
                            TmpBookingType.Indexes.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("ID"))
                            TmpBookingType.Indexes(TmpNode.GetAttribute("ID")).FromDate = TmpNode.GetAttribute("FromDate")
                            TmpBookingType.Indexes(TmpNode.GetAttribute("ID")).ToDate = TmpNode.GetAttribute("ToDate")
                            If Not TmpNode.GetAttribute("UseThis") = "" Then
                                TmpBookingType.Indexes(TmpNode.GetAttribute("ID")).UseThis = TmpNode.GetAttribute("UseThis")
                            End If
                            If TmpNode.GetAttribute("IndexDP0") = "" Then
                                If TmpNode.GetAttribute("Value") = "" AndAlso TmpNode.GetElementsByTagName("Enhancements").Item(0) IsNot Nothing Then
                                    Dim TmpENode As Xml.XmlElement = TmpNode.GetElementsByTagName("Enhancements").Item(0)
                                    'TmpBookingType.Indexes(TmpNode.GetAttribute("ID")).Enhancements.SpecificFactor = TmpENode.GetAttribute("SpecificFactor")
                                    TmpENode = TmpENode.FirstChild
                                    While Not TmpENode Is Nothing
                                        With TmpBookingType.Indexes(TmpNode.GetAttribute("ID")).Enhancements.Add()
                                            .ID = TmpENode.GetAttribute("ID")
                                            .Name = TmpENode.GetAttribute("Name")
                                            .Amount = TmpENode.GetAttribute("Amount")
                                            If TmpENode.GetAttribute("UseThis") <> "" Then
                                                .UseThis = TmpENode.GetAttribute("UseThis")
                                            End If
                                        End With
                                        TmpENode = TmpENode.NextSibling
                                    End While
                                    'If TmpIndex.Enhancements.Count = 0 Then
                                    '    For i = 0 To DaypartCount - 1
                                    '        XMLIndex.SetAttribute("IndexDP" & i, TmpIndex.Index(i))
                                    '    Next
                                    'Else
                                    '    Dim XMLEnhancements As Xml.XmlElement
                                    '    XMLEnhancements = XMLDoc.CreateElement("Enhancements")
                                    '    For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
                                    '        Dim XMLEnh As Xml.XmlElement = XMLDoc.CreateElement("Enhancement")
                                    '        XMLEnh.SetAttribute("ID", TmpEnh.ID)
                                    '        XMLEnh.SetAttribute("Name", TmpEnh.Name)
                                    '        XMLEnh.SetAttribute("Amount", TmpEnh.Amount)
                                    '        XMLEnhancements.AppendChild(XMLEnh)
                                    '    Next
                                    '    XMLEnhancements.SetAttribute("SpecificFactor", TmpIndex.Enhancements.SpecificFactor)
                                    '    XMLIndex.AppendChild(XMLEnhancements)
                                    'End If
                                Else
                                    If String.IsNullOrEmpty(TmpNode.GetAttribute("Value")) Then
                                        TmpBookingType.Indexes(TmpNode.GetAttribute("ID")).Index = 100
                                    Else
                                        TmpBookingType.Indexes(TmpNode.GetAttribute("ID")).Index = TmpNode.GetAttribute("Value")
                                    End If

                                End If
                            Else
                                For i = 0 To TmpBookingType.Dayparts.Count - 1
                                    If Not String.IsNullOrEmpty(TmpNode.GetAttribute("IndexDP" & i)) Then
                                        TmpBookingType.Indexes(TmpNode.GetAttribute("ID")).Index(i) = TmpNode.GetAttribute("IndexDP" & i)
                                    Else
                                        TmpBookingType.Indexes(TmpNode.GetAttribute("ID")).Index(i) = TmpNode.GetAttribute("IndexDP" & 0)
                                    End If
                                Next
                            End If
                            TmpBookingType.Indexes(TmpNode.GetAttribute("ID")).IndexOn = TmpNode.GetAttribute("IndexOn")
                            If TmpNode.GetAttribute("UseThis") <> "" Then
                                TmpBookingType.Indexes(TmpNode.GetAttribute("ID")).UseThis = TmpNode.GetAttribute("UseThis")
                            End If
                            TmpBookingType.Indexes(TmpNode.GetAttribute("ID")).SystemGenerated = TmpNode.GetAttribute("SystemGenerated")
                        End If
                        TmpNode = TmpNode.NextSibling
                    End While
                    If Not XMLBookingType.GetElementsByTagName("Compensations").Item(0) Is Nothing Then
                        TmpNode = XMLBookingType.GetElementsByTagName("Compensations").Item(0).FirstChild
                    Else
                        TmpNode = Nothing
                    End If
                    While Not TmpNode Is Nothing
                        Dim TmpComp As Trinity.cCompensation = TmpBookingType.Compensations.Add
                        If Not TmpNode.GetAttribute("ID") = "" Then
                            TmpComp.ID = TmpNode.GetAttribute("ID")
                        End If
                        TmpComp.FromDate = TmpNode.GetAttribute("From")
                        TmpComp.ToDate = TmpNode.GetAttribute("To")
                        TmpComp.TRPs = TmpNode.GetAttribute("TRPs")
                        If TmpNode.GetAttribute("Expense") <> "" Then TmpComp.Expense = TmpNode.GetAttribute("Expense")
                        TmpComp.Comment = TmpNode.GetAttribute("Comment")
                        TmpNode = TmpNode.NextSibling
                    End While
                    TmpNode = XMLBookingType.GetElementsByTagName("AddedValues").Item(0).FirstChild
                    While Not TmpNode Is Nothing
                        TmpBookingType.AddedValues.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("ID"))
                        TmpBookingType.AddedValues(TmpNode.GetAttribute("ID")).IndexGross = TmpNode.GetAttribute("IndexGross")
                        TmpBookingType.AddedValues(TmpNode.GetAttribute("ID")).IndexNet = TmpNode.GetAttribute("IndexNet")
                        If TmpNode.GetAttribute("ShowIn") <> "" Then TmpBookingType.AddedValues(TmpNode.GetAttribute("ID")).ShowIn = TmpNode.GetAttribute("ShowIn")
                        If TmpNode.GetAttribute("UseThis") <> "" Then TmpBookingType.AddedValues(TmpNode.GetAttribute("ID")).UseThis = TmpNode.GetAttribute("UseThis")
                        TmpNode = TmpNode.NextSibling
                    End While
                    TmpNode = XMLBookingType.GetElementsByTagName("SpotIndex").Item(0).FirstChild
                    While Not TmpNode Is Nothing
                        TmpBookingType.FilmIndex(TmpNode.GetAttribute("Length")) = TmpNode.GetAttribute("Idx")
                        TmpNode = TmpNode.NextSibling
                    End While

                    'Load weeks
                    'We add all but TRPs/Budget here since we have no pricelists
                    'To add the pricelist correct we need the periods
                    Dim w As Integer = 0
                    XMLWeek = XMLBookingType.GetElementsByTagName("Weeks").Item(0).FirstChild

                    While Not XMLWeek Is Nothing

                        w += 1
                        TmpString = XMLWeek.GetAttribute("Name")
                        TmpWeek = TmpBookingType.Weeks.Add(TmpString)
                        TmpWeek.TRPControl = XMLWeek.GetAttribute("TRPControl")
                        TmpWeek.StartDate = XMLWeek.GetAttribute("StartDate")
                        TmpWeek.EndDate = XMLWeek.GetAttribute("EndDate")
                        If TmpWeek.TRPControl Then
                            TmpWeek.TRP = Conv(XMLWeek.GetAttribute("TRP"))
                        Else
                            TmpWeek.NetBudget = Conv(XMLWeek.GetAttribute("NetBudget"))
                        End If


                        'Added values

                        If Not XMLWeek.GetElementsByTagName("AddedValues").Item(0) Is Nothing Then
                            Dim XMLIndex As XmlElement = XMLWeek.GetElementsByTagName("AddedValues").Item(0).FirstChild

                            While Not XMLIndex Is Nothing
                                TmpBookingType.AddedValues(XMLIndex.GetAttribute("ID")).Amount(w) = XMLIndex.GetAttribute("Amount")
                                XMLIndex = XMLIndex.NextSibling
                            End While
                        End If

                        'Save Films

                        XMLFilm = XMLWeek.GetElementsByTagName("Films").Item(0).FirstChild

                        While Not XMLFilm Is Nothing

                            TmpString = XMLFilm.GetAttribute("Name")
                            TmpFilm = TmpWeek.Films.Add(TmpString)
                            TmpFilm.Filmcode = XMLFilm.GetAttribute("Filmcode")
                            TmpFilm.FilmLength = XMLFilm.GetAttribute("FilmLength")
                            '                    If Not IsNull(XMLFilm.getAttribute("AltFilmcode")) Then
                            '                        TmpFilm.AltFilmcode = XMLFilm.getAttribute("AltFilmcode")
                            '                    End If
                            TmpFilm.Index = Conv(XMLFilm.GetAttribute("Index"))
                            If Not XMLFilm.GetAttribute("GrossIndex") = "" Then
                                TmpFilm.GrossIndex = XMLFilm.GetAttribute("GrossIndex")
                            End If
                            TmpFilm.Share = Conv(XMLFilm.GetAttribute("Share"))
                            TmpFilm.Description = XMLFilm.GetAttribute("Description")
                            XMLFilm = XMLFilm.NextSibling

                        End While
                        XMLWeek = XMLWeek.NextSibling
                    End While
                    'spot count låg här innan

                    'Read Buyingtarget

                    XMLBuyTarget = XMLBookingType.SelectSingleNode("BuyingTarget")

                    TmpBookingType.BuyingTarget.CalcCPP = XMLBuyTarget.GetAttribute("CalcCPP")

                    'TmpBookingType.BuyingTarget.CPP = Conv(XMLBuyTarget.GetAttribute("CPP"))
                    TmpBookingType.BuyingTarget.TargetName = XMLBuyTarget.GetAttribute("TargetName")
                    'TmpBookingType.BuyingTarget.UniSize = XMLBuyTarget.GetAttribute("UniSize")
                    'TmpBookingType.BuyingTarget.UniSizeNat = XMLBuyTarget.GetAttribute("UniSizeNat")
                    'If TmpChannel.BuyingUniverse = "" Then
                    '    TmpBookingType.BuyingTarget.UniSize = TmpBookingType.BuyingTarget.UniSizeNat
                    'End If
                    'XMLTmpNode = XMLBuyTarget.GetElementsByTagName("DaypartCPP").Item(0)
                    'For i = 0 To DaypartCount - 1
                    '    If IsDBNull(XMLTmpNode.GetAttribute(mvarDaypartName(i))) Then
                    '        XMLTmpNode.SetAttribute(mvarDaypartName(i), 0)
                    '    End If
                    '    TmpBookingType.BuyingTarget.CPPDaypart(i) = Conv(XMLTmpNode.GetAttribute(mvarDaypartName(i)))
                    'Next

                    '****MOVED
                    'TmpBookingType.BuyingTarget.IsEntered = XMLBuyTarget.GetAttribute("IsEntered")
                    'If TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
                    '    TmpBookingType.BuyingTarget.NetCPP = Conv(XMLBuyTarget.GetAttribute("Amount"))
                    'ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPT Then
                    '    TmpBookingType.BuyingTarget.NetCPT = Conv(XMLBuyTarget.GetAttribute("Amount"))
                    'ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eDiscount Then
                    '    TmpBookingType.BuyingTarget.Discount = Conv(XMLBuyTarget.GetAttribute("Amount"))
                    'End If
                    '****

                    XMLTarget = XMLBuyTarget.GetElementsByTagName("Target").Item(0)
                    TmpBookingType.BuyingTarget.Target.NoUniverseSize = True

                    If XMLTarget.GetAttribute("Type") <> "" AndAlso XMLTarget.GetAttribute("Type") > 0 AndAlso XMLTarget.GetAttribute("TargetGroup") <> "" Then
                        TmpBookingType.BuyingTarget.Target.TargetType = XMLTarget.GetAttribute("Type")
                        'On Error Resume Next
                        Try
                            mvarInternalAdedge.setTargetUserDefined(XMLTarget.GetAttribute("TargetGroup"), XMLTarget.GetAttribute("Name"), True)
                            TmpBookingType.BuyingTarget.Target.TargetGroup = XMLTarget.GetAttribute("TargetGroup")
                            TmpBookingType.BuyingTarget.Target.TargetName = XMLTarget.GetAttribute("Name")
                        Catch
                            If ReplacementTargets.ContainsKey(XMLTarget.GetAttribute("Name")) Then
                                TmpBookingType.BuyingTarget.Target.TargetGroup = ReplacementTargets(XMLTarget.GetAttribute("Name")).GroupName
                                TmpBookingType.BuyingTarget.Target.TargetName = ReplacementTargets(XMLTarget.GetAttribute("Name")).TargetName
                            Else
                                frmFindTarget.ShowDialog("The target '" & XMLTarget.GetAttribute("Name") & "' does not exist. Please choose replacement.")
                                TmpBookingType.BuyingTarget.Target.TargetGroup = frmFindTarget.tvwTargets.SelectedNode.Tag
                                TmpBookingType.BuyingTarget.Target.TargetName = frmFindTarget.tvwTargets.SelectedNode.Text
                                Dim TmpTarget As New AdedgeTargetStruct With {.GroupName = TmpBookingType.BuyingTarget.Target.TargetGroup, .TargetName = TmpBookingType.BuyingTarget.Target.TargetName}
                                ReplacementTargets.Add(XMLTarget.GetAttribute("Name"), TmpTarget)
                            End If
                        End Try
                    Else
                        TmpBookingType.BuyingTarget.Target.TargetType = XMLTarget.GetAttribute("Type")
                        TmpBookingType.BuyingTarget.Target.TargetGroup = ""
                        TmpBookingType.BuyingTarget.Target.TargetName = XMLTarget.GetAttribute("Name")
                    End If
                    'On Error GoTo ErrHandle

                    'TmpBookingType.BuyingTarget.Target.TargetName = XMLTarget.GetAttribute("Name")
                    'TmpBookingType.BuyingTarget.Target.TargetType = XMLTarget.GetAttribute("Type")
                    TmpBookingType.BuyingTarget.Target.Universe = XMLTarget.GetAttribute("Universe")
                    TmpBookingType.BuyingTarget.Target.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")

                    TmpBookingType.BuyingTarget.Target.NoUniverseSize = False

                    'some old pricelists has no index row
                    If XMLBuyTarget.GetElementsByTagName("Indexes").Count = 0 AndAlso XMLBuyTarget.GetElementsByTagName("PricelistPeriods").Count = 0 Then
                        Dim period As cPricelistPeriod
                        period = TmpBookingType.BuyingTarget.PricelistPeriods.Add("All Year")
                        period.FromDate = New Date(2007, 1, 1)
                        period.ToDate = New Date(2007, 12, 31)
                        period.TargetNat = XMLTmpNode.GetAttribute("TargetNat")
                        period.TargetUni = XMLTmpNode.GetAttribute("TargetUni")
                        period.Price(True) = XMLTmpNode.GetAttribute("CPP")

                        For i = 0 To TmpBookingType.Dayparts.Count - 1
                            Dim cpp As String = XMLTmpNode.GetAttribute("CPP_DP" & i + 1)
                            If cpp = "" Then cpp = "0"
                            period.Price(True, i) = cpp
                        Next
                    End If

                    'old pricelists
                    If XMLBuyTarget.GetElementsByTagName("Indexes").Count > 0 Then
                        TmpNode = XMLBuyTarget.GetElementsByTagName("Indexes").Item(0).FirstChild
                        If XMLBuyTarget.GetElementsByTagName("Indexes").Item(0).HasChildNodes Then
                            While Not TmpNode Is Nothing

                                TmpBookingType.BuyingTarget.PricelistPeriods.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("ID"))
                                TmpBookingType.BuyingTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).FromDate = TmpNode.GetAttribute("FromDate")
                                TmpBookingType.BuyingTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).ToDate = TmpNode.GetAttribute("ToDate")
                                TmpBookingType.BuyingTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).TargetNat = XMLBuyTarget.GetAttribute("UniSizeNat")
                                TmpBookingType.BuyingTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).TargetUni = XMLBuyTarget.GetAttribute("UniSize")

                                If TmpBookingType.BuyingTarget.CalcCPP Then
                                    XMLTmpNode = XMLBuyTarget.GetElementsByTagName("DaypartCPP").Item(0)
                                    For i = 0 To TmpBookingType.Dayparts.Count - 1
                                        'XMLTmpNode2 = XMLTmpNode.GetElementsByTagName(mvarDaypartName(i)).Item(0)
                                        Dim cpp As String = XMLTmpNode.GetAttribute(TmpBookingType.Dayparts(i).Name)
                                        Dim index As String = TmpNode.GetAttribute("IndexDP" & i)
                                        If cpp = "" Then cpp = "0"
                                        If index = "" Then index = "0"
                                        TmpBookingType.BuyingTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).Price(True, i) = index * cpp / 100
                                    Next
                                Else
                                    TmpBookingType.BuyingTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).Price(True) = XMLBuyTarget.GetAttribute("CPP") * TmpNode.GetAttribute("IndexDP0") / 100
                                End If
                                TmpNode = TmpNode.NextSibling
                            End While
                        Else
                            Dim period As cPricelistPeriod
                            period = TmpBookingType.BuyingTarget.PricelistPeriods.Add("All Year")
                            period.FromDate = Date.FromOADate(StartDate)
                            period.ToDate = Date.FromOADate(EndDate)
                            period.TargetNat = XMLBuyTarget.GetAttribute("UniSizeNat")
                            period.TargetUni = XMLBuyTarget.GetAttribute("UniSize")
                            period.Price(True) = XMLBuyTarget.GetAttribute("CPP")
                        End If
                    End If

                    If Not XMLBuyTarget.GetElementsByTagName("PricelistIndexes").Item(0) Is Nothing Then
                        TmpNode = XMLBuyTarget.GetElementsByTagName("PricelistIndexes").Item(0).FirstChild
                        While Not TmpNode Is Nothing
                            Dim TmpIndex As cIndex = TmpBookingType.BuyingTarget.Indexes.Add(TmpNode.GetAttribute("Name"))
                            TmpIndex.FromDate = TmpNode.GetAttribute("FromDate")
                            TmpIndex.ID = TmpNode.GetAttribute("ID")
                            TmpIndex.Index = TmpNode.GetAttribute("Index")
                            TmpIndex.IndexOn = TmpNode.GetAttribute("IndexOn")
                            TmpIndex.ToDate = TmpNode.GetAttribute("ToDate")
                            If TmpNode.GetAttribute("UseThis") = "" Then
                                TmpIndex.UseThis = True
                            Else
                                TmpIndex.UseThis = TmpNode.GetAttribute("UseThis")
                            End If
                            TmpNode = TmpNode.NextSibling
                        End While
                    End If
                    If XMLBuyTarget.GetElementsByTagName("PricelistPeriods").Count > 0 Then
                        'newly saved campaign (new pricelist type)
                        TmpNode = XMLBuyTarget.GetElementsByTagName("PricelistPeriods").Item(0).FirstChild
                        While Not TmpNode Is Nothing
                            TmpBookingType.BuyingTarget.PricelistPeriods.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("ID"))
                            TmpBookingType.BuyingTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).FromDate = TmpNode.GetAttribute("FromDate")
                            TmpBookingType.BuyingTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).ToDate = TmpNode.GetAttribute("ToDate")
                            TmpBookingType.BuyingTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).TargetNat = TmpNode.GetAttribute("UniSizeNat")
                            TmpBookingType.BuyingTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).TargetUni = TmpNode.GetAttribute("UniSize")
                            TmpBookingType.BuyingTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).PriceIsCPP = TmpNode.GetAttribute("isCPP")
                            If TmpBookingType.BuyingTarget.CalcCPP Then
                                For i = 0 To TmpBookingType.Dayparts.Count - 1
                                    If IsNumeric(TmpNode.GetAttribute("PriceDP" & i)) Then
                                        TmpBookingType.BuyingTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).Price(TmpNode.GetAttribute("isCPP"), i) = TmpNode.GetAttribute("PriceDP" & i) '* Conv(XMLTmpNode.GetAttribute(mvarDaypartName(i)))
                                    Else
                                        TmpBookingType.BuyingTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).Price(TmpNode.GetAttribute("isCPP"), i) = 0 ' * Conv(XMLTmpNode.GetAttribute(mvarDaypartName(i)))
                                    End If
                                Next
                            Else
                                If IsNumeric(TmpNode.GetAttribute("Price")) Then
                                    TmpBookingType.BuyingTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).Price(TmpNode.GetAttribute("isCPP")) = TmpNode.GetAttribute("Price") '* Conv(XMLTmpNode.GetAttribute(mvarDaypartName(i)))
                                Else
                                    TmpBookingType.BuyingTarget.PricelistPeriods(TmpNode.GetAttribute("ID")).Price(TmpNode.GetAttribute("isCPP")) = 0
                                End If
                            End If

                            TmpNode = TmpNode.NextSibling
                        End While
                    End If

                    'Add TRPs/Budget on weeks.
                    'This needs to be done after we have added the pricelists
                    w = 0
                    XMLWeek = XMLBookingType.GetElementsByTagName("Weeks").Item(0).FirstChild

                    While Not XMLWeek Is Nothing

                        w += 1
                        TmpWeek = TmpBookingType.Weeks(w)
                        If TmpWeek.TRPControl Then
                            TmpWeek.TRP = Conv(XMLWeek.GetAttribute("TRP"))
                        Else
                            TmpWeek.NetBudget = Conv(XMLWeek.GetAttribute("NetBudget"))
                        End If

                        XMLWeek = XMLWeek.NextSibling
                    End While

                    'we get the spot count and avrage rating
                    If XMLBookingType.GetAttribute("PlannedSpotCount") = "" Then
                        'Old campaign before change: AverageRating is in buying target not main
                        Dim TmpSng As Single = XMLBookingType.GetAttribute("AverageRating")
                        If TmpSng > 0 Then
                            TmpBookingType.EstimatedSpotCount = TmpBookingType.TotalTRP / TmpSng
                        Else
                            TmpBookingType.EstimatedSpotCount = 0
                        End If
                    Else
                        TmpBookingType.EstimatedSpotCount = XMLBookingType.GetAttribute("PlannedSpotCount")
                    End If

                    TmpBookingType.BuyingTarget.IsEntered = XMLBuyTarget.GetAttribute("IsEntered")
                    If TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
                        TmpBookingType.BuyingTarget.NetCPP = Conv(XMLBuyTarget.GetAttribute("Amount"))
                    ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPT Then
                        TmpBookingType.BuyingTarget.NetCPT = Conv(XMLBuyTarget.GetAttribute("Amount"))
                    ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eDiscount Then
                        TmpBookingType.BuyingTarget.Discount = Conv(XMLBuyTarget.GetAttribute("Amount"))
                    ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eEnhancement Then
                        TmpBookingType.BuyingTarget.Enhancement = Conv(XMLBuyTarget.GetAttribute("Amount"))
                    End If

                    XMLBookingType = XMLBookingType.NextSibling
                End While
                XMLChannel = XMLChannel.NextSibling
            End While

            Dim XMLCombo As XmlElement
            Dim XMLComboChannel As XmlElement

            If Not XMLCamp.SelectSingleNode("Combinations") Is Nothing Then
                XMLCombo = XMLCamp.SelectSingleNode("Combinations").FirstChild
                While Not XMLCombo Is Nothing
                    With Combinations.Add
                        .ID = XMLCombo.GetAttribute("ID")
                        .Name = XMLCombo.GetAttribute("Name")
                        If XMLCombo.GetAttribute("IndexMainTarget") = "0" Or XMLCombo.GetAttribute("IndexMainTarget") = "" Then
                            .IndexMainTarget = 0
                        Else
                            .IndexMainTarget = XMLCombo.GetAttribute("IndexMainTarget")
                        End If

                        If XMLCombo.GetAttribute("MarathonIDCombination") = Nothing Or XMLCombo.GetAttribute("MarathonIDCombination") = "" Then
                            .MarathonIDCombination = ""
                        Else
                            .MarathonIDCombination = XMLCombo.GetAttribute("MarathonIDCombination")
                        End If

                        If XMLCombo.GetAttribute("SendAsOneUnitToMarathon") = Nothing Or XMLCombo.GetAttribute("SendAsOneUnitToMarathon") = "" Then
                            .sendAsOneUnitTOMarathon = False
                        Else
                            .sendAsOneUnitTOMarathon = XMLCombo.GetAttribute("SendAsOneUnitToMarathon")
                        End If

                        If XMLCombo.GetAttribute("IndexSecondTarget") = "0" Or XMLCombo.GetAttribute("IndexSecondTarget") = "" Then
                            .IndexSecondTarget = 0
                        Else
                            .IndexSecondTarget = XMLCombo.GetAttribute("IndexSecondTarget")
                        End If
                        If XMLCombo.GetAttribute("IndexAllAdults") = "0" Or XMLCombo.GetAttribute("IndexAllAdults") = "" Then
                            .IndexAllAdults = 0
                        Else
                            .IndexAllAdults = XMLCombo.GetAttribute("IndexAllAdults")
                        End If
                        .CombinationOn = XMLCombo.GetAttribute("CombinationOn")
                        'If XMLCombo.GetAttribute("IndexMainTarget") <> "" Then .IndexMainTarget = XMLCombo.GetAttribute("IndexMainTarget")
                        'If XMLCombo.GetAttribute("IndexSecondTarget") <> "" Then .IndexSecondTarget = XMLCombo.GetAttribute("IndexSecondTarget")
                        'If XMLCombo.GetAttribute("IndexAllAdults") <> "" Then .IndexAllAdults = XMLCombo.GetAttribute("IndexAllAdults")
                        If Not XMLCombo.GetAttribute("ShowAsOne") = "" Then
                            .ShowAsOne = XMLCombo.GetAttribute("ShowAsOne")
                        End If
                        If Not XMLCombo.GetAttribute("PrintAsOne") = "" Then
                            .PrintAsOne = XMLCombo.GetAttribute("PrintAsOne")
                        End If
                        XMLComboChannel = XMLCombo.FirstChild
                        While Not XMLComboChannel Is Nothing
                            If mvarChannels(XMLComboChannel.GetAttribute("Chan")) IsNot Nothing AndAlso mvarChannels(XMLComboChannel.GetAttribute("Chan")).BookingTypes(XMLComboChannel.GetAttribute("BT")) IsNot Nothing Then
                                .Relations.Add(mvarChannels(XMLComboChannel.GetAttribute("Chan")).BookingTypes(XMLComboChannel.GetAttribute("BT")), XMLComboChannel.GetAttribute("Relation"))
                            End If
                            XMLComboChannel = XMLComboChannel.NextSibling
                        End While

                        'Deprecated: ShowMe now derives from ShowAsOne. See Bookingtype.ShowMe
                        '
                        'If .ShowAsOne Then
                        '    For Each tmpCC As Trinity.cCombinationChannel In .Relations
                        '        tmpCC.Bookingtype.ShowMe = False
                        '    Next
                        'End If
                    End With
                    XMLCombo = XMLCombo.NextSibling
                End While
            End If

            If mvarContractID > 0 Then
                XMLContract = DBReader.getContract(mvarContractID)
            Else
                XMLContract = XMLCamp.GetElementsByTagName("Contract").Item(0)

            End If
            If Not XMLContract Is Nothing AndAlso Not XMLContract.ChildNodes.Item(0) Is Nothing Then
                mvarContract = New cContract(Me, False)
                mvarContract.Load("", True, XMLContract.OuterXml)
            End If
            'Save Planned spots


            'Load costs

            TmpNode = XMLCamp.SelectNodes("Costs")(0).FirstChild
            While Not TmpNode Is Nothing
                If TmpNode.GetAttribute("CostOn") = "" Then
                    mvarCosts.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("CostType"), Conv(TmpNode.GetAttribute("Amount")), Nothing, TmpNode.GetAttribute("MarathonID"))
                ElseIf Not IsNumeric(TmpNode.GetAttribute("CostOn")) Then
                    Dim cost As Trinity.cCost = mvarCosts.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("CostType"), Conv(TmpNode.GetAttribute("Amount")), Nothing, TmpNode.GetAttribute("MarathonID"))
                    cost.CountCostOn = Channels(TmpNode.GetAttribute("CostOn"))
                Else
                    mvarCosts.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("CostType"), Conv(TmpNode.GetAttribute("Amount")), TmpNode.GetAttribute("CostOn"), TmpNode.GetAttribute("MarathonID"))
                End If
                TmpNode = TmpNode.NextSibling
            End While

            'Load AdvantEdge brands

            mvarAdEdgeProducts.Clear()
            If XMLCamp.SelectNodes("AdedgeBrands").Count > 0 Then
                TmpNode = XMLCamp.SelectNodes("AdedgeBrands")(0).FirstChild
                While Not TmpNode Is Nothing
                    mvarAdEdgeProducts.Add(TmpNode.GetAttribute("Name"))
                    TmpNode = TmpNode.NextSibling
                End While
            End If

            'Load Weekly Reach

            If XMLCamp.SelectNodes("WeeklyReach").Count > 0 Then
                TmpNode = XMLCamp.SelectNodes("WeeklyReach")(0).FirstChild
                While Not TmpNode Is Nothing
                    EstimatedWeeklyReach(TmpNode.GetAttribute("Name")) = TmpNode.GetAttribute("Reach")
                    TmpNode = TmpNode.NextSibling
                End While
            End If

            CurrentlyReading = "Planned spots"
            XMLSpot = XMLCamp.GetElementsByTagName("PlannedSpots").Item(0).FirstChild
            mvarPlannedSpots = New cPlannedSpots(Me)

            While Not XMLSpot Is Nothing
NextPlannedSpot:
                TmpString = XMLSpot.GetAttribute("ID")
                TmpPlannedSpot = mvarPlannedSpots.Add(TmpString)
                TmpString = XMLSpot.GetAttribute("Channel")
                TmpPlannedSpot.Channel = mvarChannels(TmpString)
                TmpPlannedSpot.ChannelID = XMLSpot.GetAttribute("ChannelID")
                TmpString = XMLSpot.GetAttribute("BookingType")
                TmpPlannedSpot.Bookingtype = TmpPlannedSpot.Channel.BookingTypes(TmpString) ' ,"cBookingType
                TmpString = XMLSpot.GetAttribute("Week")
                If TmpString <> "" Then
                    TmpPlannedSpot.Week = TmpPlannedSpot.Bookingtype.Weeks(TmpString) ' ,"cWeek
                End If
                TmpLong = XMLSpot.GetAttribute("AirDate")
                TmpPlannedSpot.AirDate = TmpLong
                TmpPlannedSpot.MaM = XMLSpot.GetAttribute("MaM")
                TmpPlannedSpot.ProgBefore = XMLSpot.GetAttribute("ProgBefore")
                TmpPlannedSpot.ProgAfter = XMLSpot.GetAttribute("ProgAfter")
                TmpPlannedSpot.Programme = XMLSpot.GetAttribute("Programme")
                TmpPlannedSpot.Advertiser = XMLSpot.GetAttribute("Advertiser")
                TmpPlannedSpot.Product = XMLSpot.GetAttribute("Product")
                TmpPlannedSpot.Filmcode = XMLSpot.GetAttribute("Filmcode")
                'On Error Resume Next
                'For Each TmpFilm In TmpPlannedSpot.Week.Films
                '    If TmpFilm.Filmcode = TmpPlannedSpot.Filmcode Then
                '        TmpPlannedSpot.Film = TmpPlannedSpot.Week.Films(TmpFilm.Filmcode)
                '    End If
                'Next TmpFilm
                'On Error GoTo ErrHandle
                TmpPlannedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = Conv(XMLSpot.GetAttribute("RatingBuyTarget"))
                'TmpPlannedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteMainTarget) = Conv(XMLSpot.getAttribute("ChannelRating(cPlannedSpot.PlannedTargetEnum.pteMainTarget)"))
                TmpPlannedSpot.Estimation = XMLSpot.GetAttribute("Estimation")
                TmpPlannedSpot.MyRating(cPlannedSpot.PlannedTargetEnum.pteMainTarget) = Conv(XMLSpot.GetAttribute("MyRating"))
                'TmpPlannedSpot.MyRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = Conv(XMLSpot.getAttribute("MyRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)"))
                TmpPlannedSpot.Index = Conv(XMLSpot.GetAttribute("Index"))
                TmpPlannedSpot.SpotLength = XMLSpot.GetAttribute("SpotLength")
                TmpPlannedSpot.SpotType = XMLSpot.GetAttribute("SpotType")
                TmpPlannedSpot.PriceNet = (XMLSpot.GetAttribute("PriceNet"))
                TmpPlannedSpot.PriceGross = (XMLSpot.GetAttribute("PriceGross"))
                If Not IsDBNull(XMLSpot.GetAttribute("Remark")) Then
                    TmpPlannedSpot.Remark = XMLSpot.GetAttribute("Remark")
                End If
                If Not IsDBNull(XMLSpot.GetAttribute("Placement")) Then
                    TmpPlannedSpot.Placement = XMLSpot.GetAttribute("Placement")
                End If
                If XMLSpot.GetAttribute("AddedValue") <> "" Then
                    TmpPlannedSpot.AddedValue = TmpPlannedSpot.Bookingtype.AddedValues(XMLSpot.GetAttribute("AddedValue"))
                End If
                XMLSpot = XMLSpot.NextSibling
            End While

ActualSpots:
            'Save Actual spots
            CurrentlyReading = "Actual spots"
            XMLSpot = XMLCamp.GetElementsByTagName("ActualSpots").Item(0).FirstChild
            mvarActualSpots = New Trinity.cActualSpots(Me)
            While Not XMLSpot Is Nothing

                TmpLong = XMLSpot.GetAttribute("AirDate")
                If XMLSpot.GetAttribute("MaM") > 0 And XMLSpot.GetAttribute("MaM") < 2000 Then

                    TmpInt = XMLSpot.GetAttribute("MaM")
                Else
                    TmpInt = 0
                End If
                TmpActualSpot = mvarActualSpots.Add(System.DateTime.FromOADate(TmpLong), TmpInt)
                If XMLSpot.GetAttribute("Second") <> "" Then
                    TmpActualSpot.Second = XMLSpot.GetAttribute("Second")
                End If
                Tmpstr = XMLSpot.GetAttribute("Channel")
                TmpActualSpot.Channel = mvarChannels(Tmpstr)
                TmpActualSpot.ProgBefore = XMLSpot.GetAttribute("ProgBefore")
                TmpActualSpot.ProgAfter = XMLSpot.GetAttribute("ProgAfter")
                TmpActualSpot.Programme = XMLSpot.GetAttribute("Programme")
                TmpActualSpot.Advertiser = XMLSpot.GetAttribute("Advertiser")
                TmpActualSpot.Product = XMLSpot.GetAttribute("Product")
                TmpActualSpot.Filmcode = XMLSpot.GetAttribute("Filmcode")
                'TmpActualSpot.Rating = Conv(XMLSpot.getAttribute("Rating"))
                TmpActualSpot.Index = Conv(XMLSpot.GetAttribute("Index"))
                TmpActualSpot.PosInBreak = XMLSpot.GetAttribute("PosInBreak")
                TmpActualSpot.SpotsInBreak = XMLSpot.GetAttribute("SpotsInBreak")
                Tmpstr = XMLSpot.GetAttribute("MatchedSpot")
                If Tmpstr <> "" Then
                    TmpActualSpot.MatchedSpot = mvarPlannedSpots(Tmpstr)
                    If Not mvarPlannedSpots(Tmpstr) Is Nothing Then
                        mvarPlannedSpots(Tmpstr).MatchedSpot = TmpActualSpot
                    End If
                    'On Error GoTo ErrHandle
                End If
                TmpActualSpot.SpotLength = XMLSpot.GetAttribute("SpotLength")
                TmpActualSpot.Deactivated = XMLSpot.GetAttribute("Deactivated")
                TmpActualSpot.SpotType = XMLSpot.GetAttribute("SpotType")
                TmpActualSpot.BreakType = XMLSpot.GetAttribute("BreakType")
                TmpActualSpot.SecondRating = Conv(XMLSpot.GetAttribute("SecondRating"))
                TmpActualSpot.AdedgeChannel = XMLSpot.GetAttribute("AdEdgeChannel")
                If TmpActualSpot.AdedgeChannel = "Kan 5" Then TmpActualSpot.AdedgeChannel = "Kanal 5"
                If XMLSpot.GetAttribute("SpotControlRemark") <> "" Then
                    TmpActualSpot.SpotControlRemark = XMLSpot.GetAttribute("SpotControlRemark")
                    TmpActualSpot.SpotControlStatus = XMLSpot.GetAttribute("SpotControlStatus")
                End If
                TmpActualSpot.ID = XMLSpot.GetAttribute("ID")
                Tmpstr = XMLSpot.GetAttribute("BookingType")
                If Tmpstr <> "" Then
                    TmpActualSpot.Bookingtype = TmpActualSpot.Channel.BookingTypes(Tmpstr)
                Else
                    TmpActualSpot.Bookingtype = TmpActualSpot.Channel.BookingTypes(1)
                End If
                'TmpActualSpot.Bookingtype.ActualSpots.Add(TmpActualSpot, TmpActualSpot.ID)
                TmpActualSpot.Week = TmpActualSpot.Bookingtype.GetWeek(Date.FromOADate(TmpActualSpot.AirDate))
                XMLSpot = XMLSpot.NextSibling

            End While

            CurrentlyReading = ""
            If XMLCamp.GetElementsByTagName("ReachGoals").Count > 0 Then
                XMLTmpNode = XMLCamp.GetElementsByTagName("ReachGoals").Item(0).FirstChild
                While Not XMLTmpNode Is Nothing

                    Group = XMLTmpNode.GetAttribute("Name")
                    XMLTmpNode2 = XMLTmpNode.FirstChild

                    'While Not XMLTmpNode2 Is Nothing

                    '    Freq = XMLTmpNode2.GetAttribute("Freq")
                    '    Reach = Conv(XMLTmpNode2.GetAttribute("Reach"))
                    '    ReachTargets(Freq, Group) = Reach
                    '    XMLTmpNode2 = XMLTmpNode2.NextSibling

                    'End While
                    XMLTmpNode = XMLTmpNode.NextSibling

                End While
            End If


            Dim TmpID As String
            Dim TmpAdjustBy As Byte
            'Dim TmpAdjustBy As Byte
            Dim TmpDate As Date
            Dim TmpBT As String
            Dim TmpChannelEstimate As Single
            Dim TmpDBID As String
            Dim TmpFilmcode As String
            Dim TmpGrossPrice As Decimal
            Dim TmpMaM As Short
            Dim tmpMyEstimate As Single
            Dim TmpMyEstChanTarget As Single
            Dim TmpNetPrice As Decimal
            Dim TmpPlacement As cBookedSpot.PlaceEnum
            Dim TmpProgAfter As String
            Dim TmpProgBefore As String
            Dim TmpProg As String
            Dim TmpIsLocal As Boolean
            Dim TmpIsRB As Boolean
            Dim TmpBid As Decimal

            mvarBookedSpots = New cBookedSpots(Me)
            mvarBookedSpots.MainObject = Me

            If XMLCamp.GetElementsByTagName("BookedSpots").Count > 0 Then
                XMLSpot = XMLCamp.GetElementsByTagName("BookedSpots").Item(0).FirstChild

                While Not XMLSpot Is Nothing
                    TmpID = XMLSpot.GetAttribute("ID")
                    TmpDate = XMLSpot.GetAttribute("AirDate")
                    TmpString = XMLSpot.GetAttribute("Channel") ' Channel
                    TmpBookingType = mvarChannels(TmpString).BookingTypes(XMLSpot.GetAttribute("Bookingtype"))
                    TmpChannelEstimate = CSng(Conv(XMLSpot.GetAttribute("ChannelEstimate")))
                    TmpDBID = XMLSpot.GetAttribute("DatabaseID")
                    TmpFilmcode = XMLSpot.GetAttribute("Filmcode")
                    TmpGrossPrice = Conv(XMLSpot.GetAttribute("GrossPrice"))
                    TmpMaM = XMLSpot.GetAttribute("MaM")
                    tmpMyEstimate = CSng(Conv(XMLSpot.GetAttribute("MyEstimate")))
                    TmpMyEstChanTarget = CSng(Conv(XMLSpot.GetAttribute("MyEstimateBuyTarget")))
                    TmpNetPrice = CDec(Conv(XMLSpot.GetAttribute("NetPrice")))
                    TmpPlacement = Val(XMLSpot.GetAttribute("Placement"))
                    TmpProgAfter = XMLSpot.GetAttribute("ProgAfter")
                    TmpProgBefore = XMLSpot.GetAttribute("ProgBefore")
                    TmpProg = XMLSpot.GetAttribute("Programme")
                    TmpBT = XMLSpot.GetAttribute("Bookingtype")
                    TmpDBID = XMLSpot.GetAttribute("DatabaseID") 'TmpString & Format(TmpDate, "yyyymmdd") & Left(helper.mam2tid(TmpMaM), 2) & Right(helper.mam2tid(TmpMaM), 2)
                    If Not XMLSpot.GetAttribute("Bid") = "" Then
                        TmpBid = XMLSpot.GetAttribute("Bid")
                    End If
                    If IsDBNull(XMLSpot.GetAttribute("IsLocal")) Then
                        TmpIsLocal = False
                        TmpIsRB = False
                    Else
                        TmpIsLocal = XMLSpot.GetAttribute("IsLocal")
                        TmpIsRB = XMLSpot.GetAttribute("IsRB")
                    End If

                    mvarBookedSpots.Add(TmpID, TmpDBID, TmpString, TmpDate, TmpMaM, TmpProg, TmpProgAfter, TmpProgBefore, TmpGrossPrice, TmpNetPrice, TmpChannelEstimate, tmpMyEstimate, TmpMyEstChanTarget, TmpFilmcode, TmpBT, TmpIsLocal, TmpIsRB, TmpBid)
                    If Not mvarBookedSpots(TmpID) Is Nothing Then
                        If Not IsDBNull(XMLSpot.GetAttribute("Comments")) Then
                            mvarBookedSpots(TmpID).Comments = XMLSpot.GetAttribute("Comments")
                        End If
                        mvarBookedSpots(TmpID).AddedValues = New Dictionary(Of String, Trinity.cAddedValue)
                        If Not XMLSpot.GetElementsByTagName("AddedValues").Item(0) Is Nothing Then
                            Dim XMLIndex As XmlElement = XMLSpot.GetElementsByTagName("AddedValues").Item(0).FirstChild
                            While Not XMLIndex Is Nothing
                                Tmpstr = XMLIndex.GetAttribute("ID")
                                Dim TmpAV As Trinity.cAddedValue = mvarBookedSpots(TmpID).Bookingtype.AddedValues(XMLIndex.GetAttribute("ID"))
                                mvarBookedSpots(TmpID).AddedValues.Add(Tmpstr, TmpAV)
                                XMLIndex = XMLIndex.NextSibling
                            End While
                        End If
                    End If
                    XMLSpot = XMLSpot.NextSibling
                End While
            End If

            Dim TmpCampaign As cKampanj

            If Not LoadStripped Then
                If XMLDoc.GetElementsByTagName("LabCampaigns").Count > 0 Then
                    XMLTmpNode = XMLDoc.GetElementsByTagName("LabCampaigns").Item(0).FirstChild
                    While Not XMLTmpNode Is Nothing
                        TmpCampaign = New cKampanj(False)
                        TmpCampaign.LoadCampaign("", True, XMLTmpNode.OuterXml)
                        If Campaigns Is Nothing Then
                            Campaigns = New Dictionary(Of String, Trinity.cKampanj)
                        End If
                        Campaigns.Add(XMLTmpNode.GetAttribute("LabName"), TmpCampaign)
                        XMLTmpNode = XMLTmpNode.NextSibling
                    End While
                End If

                If XMLCamp.SelectSingleNode("./History") IsNot Nothing Then
                    XMLTmpNode = XMLCamp.SelectSingleNode("./History").FirstChild
                    While Not XMLTmpNode Is Nothing
                        TmpCampaign = New cKampanj(False)
                        TmpCampaign.LoadCampaign("", True, XMLTmpNode.OuterXml)
                        TmpCampaign.RootCampaign = Me
                        History.Add(TmpCampaign.ID, TmpCampaign)
                        XMLTmpNode = XMLTmpNode.NextSibling
                    End While
                    '    Stop
                End If
            End If

            'If there are channels, and the first channel has at least one booking type, and this booking type has weeks added to it, and if this week has a film added to it, then

            '<--- COMMENTED OUT 2010-12-08. Do we need this function??

            'If mvarChannels.Count > 0 AndAlso mvarChannels(1).BookingTypes.Count > 0 AndAlso mvarChannels(1).BookingTypes(1).Weeks.Count > 0 AndAlso mvarChannels(1).BookingTypes(1).Weeks(1).Films.Count > 0 Then
            '    For Each TmpChannel In mvarChannels
            '        For Each TmpBookingType In TmpChannel.BookingTypes
            '            For Each TmpWeek In TmpBookingType.Weeks
            '                For Each TmpFilm In TmpWeek.Films
            '                    If TmpFilm.Filmcode = "" Then
            '                        'Update AdTooxStatus
            '                        If TrinitySettings.AdtooxEnabled Then
            '                            Dim Dummy As Trinity.cAdTooxStatus = TmpFilm.AdTooxStatus()
            '                            Dummy = GetTotalAdTooxStatus(TmpFilm.Filmcode)
            '                        End If
            '                        'If any film lacks a filmcode, then assign it the filmcode
            '                        For Each TmpChan As Trinity.cChannel In mvarChannels
            '                            For Each TmpBT2 As Trinity.cBookingType In TmpChan.BookingTypes
            '                                For Each TmpWeek2 As Trinity.cWeek In TmpBT2.Weeks
            '                                    For Each TmpFilm2 As Trinity.cFilm In TmpWeek2.Films
            '                                        If TmpFilm2.Filmcode <> "" Then
            '                                            TmpFilm.Filmcode = TmpFilm2.Filmcode
            '                                        End If
            '                                        'End If
            '                                        'If TmpBT2.Weeks.Count > 0 AndAlso TmpBT2.Weeks(1).Films(TmpFilm.Name).Filmcode <> "" Then
            '                                        'TmpFilm.Filcode = TmpBT2.Weeks(1).Films(TmpFilm.Name).Filmcode
            '                                        'End If
            '                                    Next
            '                                Next
            '                            Next
            '                        Next
            '                    End If
            '                Next
            '            Next
            '        Next
            '    Next
            'End If
            '---->

            PluginSaveData = New Dictionary(Of String, XElement)
            If XMLCamp.SelectSingleNode("Plugins") IsNot Nothing Then
                XMLTmpNode = XMLCamp.SelectSingleNode("Plugins").FirstChild
                While XMLTmpNode IsNot Nothing
                    Dim _name As String = XMLTmpNode.Name
                    PluginSaveData.Add(_name, DirectCast(XMLTmpNode.FirstChild, XmlElement).ToXElement)
                    XMLTmpNode = XMLTmpNode.NextSibling
                End While
            End If

            'update all CPPs (cuases erroronous figures in Monitor and priniting otherwize)
            For Each tmpChan As cChannel In Me.Channels
                For Each tmpBookT As cBookingType In tmpChan.BookingTypes
                    If tmpBookT.BookIt Then
                        Dim SaveSpotCount As Integer = tmpBookT.EstimatedSpotCount
                        For Each tmpWk As cWeek In tmpBookT.Weeks
                            tmpWk.RecalculateCPP()
                        Next
                        tmpBookT.EstimatedSpotCount = SaveSpotCount
                    End If
                Next
            Next

            If LoadStripped Then
                Dim j As Integer = mvarChannels.Count

                While j > 0
                    i = mvarChannels(j).BookingTypes.Count
                    While i > 0
                        If Not mvarChannels(j).BookingTypes(i).BookIt Then
                            mvarChannels(j).BookingTypes.Remove(i)
                        End If
                        i -= 1
                    End While
                    If mvarChannels(j).BookingTypes.Count = 0 Then
                        mvarChannels.Remove(j)
                    End If
                    j -= 1
                End While

            End If

            mvarFilename = Path

            Dim priceErrors As New List(Of String)
            For Each c As cChannel In Channels
                For Each BT As cBookingType In c.BookingTypes
                    If BT.BookIt Then
                        For z As Integer = StartDate To EndDate
                            If BT.BuyingTarget.CalcCPP Then
                                For x As Integer = 0 To BT.Dayparts.Count - 1
                                    If Not BT.BuyingTarget.hasCPPForDate(z, x) AndAlso Not priceErrors.Contains(BT.ToString) Then
                                        priceErrors.Add(BT.ToString)
                                        Exit For
                                    End If
                                Next
                            Else
                                If Not BT.BuyingTarget.hasCPPForDate(z) AndAlso Not priceErrors.Contains(BT.ToString) Then
                                    priceErrors.Add(BT.ToString)
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                Next
            Next

            ChannelBundles.Read()

            If priceErrors.Count > 0 AndAlso Not DoNotLoadFromFile Then
                'removes the , in the beginning 
                Dim strPriceErrors As String = ""
                For Each _priceErr As String In priceErrors
                    strPriceErrors &= _priceErr & vbCrLf
                Next

                If priceErrors.Count > 1 Then
                    MsgBox("The following medias does not have a pricelist covering the entire campaign period in the booked target:" & vbCrLf & strPriceErrors & vbCrLf & "To correct these errors make sure your price lists are updated and correct", MsgBoxStyle.Critical, "E R R O R")
                Else
                    MsgBox("This media does not have a pricelist covering the entire campaign period in the booked target:" & vbCrLf & strPriceErrors & vbCrLf & "To correct this error make sure your price list are updated and correct", MsgBoxStyle.Critical, "E R R O R")
                End If
            End If

            Loading = False
            Exit Function

ErrHandle:
            If CurrentlyReading = "Planned spots" Then
                MsgBox("The follwing error: '" & Err.Description & "' occured while reading planned spot no " & Chr(10) & Chr(10) & "Please click one of the following:" & Chr(10) & "'Abort': Continue to read file but ignore planned spots" & Chr(10) & "'Retry': Read the next planned spot" & Chr(10) & "'Ignore': Continue reading other info on this spot", MsgBoxStyle.AbortRetryIgnore, "TRINITY")
                'If r = MsgBoxResult.Ignore Then
                '    'Resume Next
                'ElseIf r = MsgBoxResult.Abort Then
                '    'Resume ActualSpots
                'ElseIf r = MsgBoxResult.Retry Then
                '    'Resume
                'End If
            End If
            LoadCampaign = 1
            Loading = False
            Err.Raise(Err.Number, Err.Source, Err.Description)
            'Resume Next

        End Function

        '---------------------------------------------------------------------------------------
        ' Procedure : TotalTRP
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   : Returns the total TRP for the campaign
        '---------------------------------------------------------------------------------------
        '
        Public Function TotalTRP(Optional ByVal IncludeCompensation As Boolean = True) As Single

            Dim TmpChannel As cChannel
            Dim TmpBookingType As cBookingType
            Dim TmpTRP As Single = 0

            For Each TmpChannel In mvarChannels
                For Each TmpBookingType In TmpChannel.BookingTypes
                    If TmpBookingType.BookIt Then
                        TmpTRP += TmpBookingType.TotalTRP(IncludeCompensation)
                    End If
                Next TmpBookingType
            Next TmpChannel
            Return TmpTRP




        End Function




        '---------------------------------------------------------------------------------------
        ' Procedure : EstimatedCommission
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   : Calculates the commision
        '---------------------------------------------------------------------------------------
        '
        Public Function EstimatedCommission() As Object

            Dim TmpChan As cChannel
            Dim TmpBT As cBookingType
            Dim TmpWeek As cWeek

            EstimatedCommission = 0
            For Each TmpChan In mvarChannels
                For Each TmpBT In TmpChan.BookingTypes
                    If TmpBT.BookIt And Not TmpBT.IsCompensation Then 'Added condition about not being compensation, we dont want commission calculated on those
                        For Each TmpWeek In TmpBT.Weeks
                            EstimatedCommission = EstimatedCommission + TmpWeek.NetBudget * TmpChan.AgencyCommission
                        Next TmpWeek
                    End If
                Next TmpBT
            Next TmpChan
        End Function


        '---------------------------------------------------------------------------------------
        ' Procedure : PlannedGross
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        Public Function PlannedGross() As Decimal
            Dim TmpChan As cChannel
            Dim TmpBT As cBookingType
            Dim TmpWeek As cWeek
            Dim TmpGross As Decimal

            For Each TmpChan In mvarChannels
                For Each TmpBT In TmpChan.BookingTypes
                    If TmpBT.BookIt AndAlso Not TmpBT.IsCompensation Then
                        For Each TmpWeek In TmpBT.Weeks
                            TmpGross += TmpWeek.GrossBudget
                        Next TmpWeek
                    End If
                Next TmpBT
            Next TmpChan
            Return TmpGross
        End Function


        '---------------------------------------------------------------------------------------
        ' Procedure : PlannedMediaNet
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        Public Function PlannedMediaNet() As Decimal
            Dim TmpChan As cChannel
            Dim TmpBT As cBookingType
            Dim TmpWeek As cWeek
            Dim TmpMediaNet As Decimal

            For Each TmpChan In mvarChannels
                For Each TmpBT In TmpChan.BookingTypes
                    If TmpBT.BookIt AndAlso Not TmpBT.IsCompensation Then
                        For Each TmpWeek In TmpBT.Weeks
                            TmpMediaNet = TmpMediaNet + TmpWeek.NetBudget
                        Next TmpWeek
                    End If
                Next TmpBT
            Next TmpChan
            PlannedMediaNet = TmpMediaNet
        End Function



        '---------------------------------------------------------------------------------------
        ' Procedure : PlannedNet
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        Public Function PlannedNet() As Object
            Dim TmpChan As cChannel
            Dim TmpBT As cBookingType
            Dim TmpWeek As cWeek
            Dim TmpCost As cCost
            Dim TotCost As Decimal
            Dim TotCommission As Decimal
            Dim TmpNet As Decimal

            For Each TmpChan In mvarChannels
                For Each TmpBT In TmpChan.BookingTypes
                    If TmpBT.BookIt AndAlso Not TmpBT.IsCompensation Then
                        For Each TmpWeek In TmpBT.Weeks
                            TmpNet = TmpNet + TmpWeek.NetBudget
                            TotCommission = TotCommission + TmpWeek.NetBudget * TmpChan.AgencyCommission
                        Next TmpWeek
                    End If
                Next TmpBT
            Next TmpChan
            TotCost = 0
            For Each TmpCost In mvarCosts
                If TmpCost.CostType = cCost.CostTypeEnum.CostTypePercent Then
                    If TmpCost.CountCostOn = cCost.CostOnPercentEnum.CostOnMediaNet Then
                        TotCost = TotCost + TmpNet * TmpCost.Amount
                    End If
                End If
            Next TmpCost
            PlannedNet = TmpNet + TotCost - TotCommission

        End Function



        '---------------------------------------------------------------------------------------
        ' Procedure : PlannedNetNet
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        Public Function PlannedNetNet() As Object
            Dim TmpChan As cChannel
            Dim TmpBT As cBookingType
            Dim TmpWeek As cWeek
            Dim TmpCost As cCost
            Dim TotCost As Decimal
            Dim TotCommission As Decimal
            Dim TmpNetNet As Decimal

            For Each TmpChan In mvarChannels
                For Each TmpBT In TmpChan.BookingTypes
                    If TmpBT.BookIt AndAlso Not TmpBT.IsCompensation Then
                        For Each TmpWeek In TmpBT.Weeks
                            TmpNetNet = TmpNetNet + TmpWeek.NetBudget
                            TotCommission = TotCommission + TmpWeek.NetBudget * TmpChan.AgencyCommission
                        Next TmpWeek
                    End If
                Next TmpBT
            Next TmpChan
            TotCost = 0
            For Each TmpCost In mvarCosts
                If TmpCost.CostType = cCost.CostTypeEnum.CostTypePercent Then
                    If TmpCost.CountCostOn = cCost.CostOnPercentEnum.CostOnMediaNet Then
                        TotCost = TotCost + TmpNetNet * TmpCost.Amount
                    End If
                End If
            Next TmpCost
            TmpNetNet = TmpNetNet + TotCost - TotCommission
            TotCost = 0
            For Each TmpCost In mvarCosts
                If TmpCost.CostType = cCost.CostTypeEnum.CostTypePercent Then
                    If TmpCost.CountCostOn = cCost.CostOnPercentEnum.CostOnNet Then
                        TotCost = TotCost + TmpNetNet * TmpCost.Amount
                    End If
                End If
            Next TmpCost
            TmpNetNet = TmpNetNet + TotCost

            For Each TmpCost In mvarCosts
                If TmpCost.CostType = cCost.CostTypeEnum.CostTypeFixed Then
                    TmpNetNet = TmpNetNet + TmpCost.Amount
                ElseIf TmpCost.CostType = cCost.CostTypeEnum.CostTypePerUnit Then
                    If TmpCost.CountCostOn = cCost.CostOnUnitEnum.CostOnSpots Then
                        For Each TmpChan In mvarChannels
                            For Each TmpBT In TmpChan.BookingTypes
                                TmpNetNet = TmpNetNet + TmpBT.EstimatedSpotCount * TmpCost.Amount
                            Next TmpBT
                        Next TmpChan
                    ElseIf TmpCost.CountCostOn = cCost.CostOnUnitEnum.CostOnBuyingTRP Then
                        For Each TmpChan In mvarChannels
                            For Each TmpBT In TmpChan.BookingTypes
                                TmpNetNet = TmpNetNet + TmpBT.TotalTRPBuyingTarget * TmpCost.Amount
                            Next TmpBT
                        Next TmpChan
                    ElseIf TmpCost.CountCostOn = cCost.CostOnUnitEnum.CostOnMainTRP Then
                        For Each TmpChan In mvarChannels
                            For Each TmpBT In TmpChan.BookingTypes
                                TmpNetNet = TmpNetNet + TmpBT.TotalTRP * TmpCost.Amount
                            Next TmpBT
                        Next TmpChan
                    End If
                End If
            Next TmpCost
            PlannedNetNet = TmpNetNet
        End Function

        Public Property FilmindexAsDiscount() As Boolean
            Get
                Return mvarFilmindexAsDiscount
            End Get
            Set(ByVal value As Boolean)
                mvarFilmindexAsDiscount = value
            End Set
        End Property

        '---------------------------------------------------------------------------------------
        ' Procedure : SpotIndex
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   : Calculates the spot index
        '---------------------------------------------------------------------------------------
        '
        Public Function SpotIndex() As Single

            Dim TmpBT As cBookingType
            Dim TmpChannel As cChannel
            Dim TmpWeek As cWeek
            Dim TmpFilm As cFilm
            Dim TRP As Single
            Dim TRP30 As Single
            Dim x As Short
            Dim TmpIndex As Single

            TmpIndex = 0
            For Each TmpChannel In mvarChannels
                For Each TmpBT In TmpChannel.BookingTypes
                    If TmpBT.BookIt Then
                        For Each TmpWeek In TmpBT.Weeks
                            x = 1
                            For Each TmpFilm In TmpWeek.Films
                                TRP = TRP + TmpWeek.TRP * (TmpFilm.Share / 100)
                                TRP30 = TRP30 + (TmpWeek.TRP * (TmpFilm.Share / 100)) * (TmpFilm.Index / 100)
                                x = x + 1
                            Next TmpFilm
                        Next TmpWeek
                    End If
                Next TmpBT
            Next TmpChannel
            If TRP > 0 Then
                SpotIndex = TRP30 / TRP
            Else
                SpotIndex = 0
            End If
        End Function



        '---------------------------------------------------------------------------------------
        ' Procedure : Conv
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   : Replaces a . with a ,
        '---------------------------------------------------------------------------------------
        '
        Private Function Conv(ByVal strn As String) As String

            Dim Tmpstr As String

            Tmpstr = strn
            If InStr(Tmpstr, ".") > 0 Then
                Mid(Tmpstr, InStr(Tmpstr, "."), 1) = ","
            End If
            Conv = Tmpstr


        End Function

        'Public Sub ReadBundles()

        '    Dim _ChannelBundles As New Dictionary(Of String, Collection)
        '    Dim _ChannelBundle As List(Of cBookingType)

        '    If System.IO.File.Exists(TrinitySettings.DataPath & "bundledchannels.xml") Then
        '        Dim _Bundles As New XmlDocument
        '        Try
        '            _Bundles.Load(TrinitySettings.DataPath & "bundledchannels.xml")
        '            For Each tmpBundle As XmlElement In _Bundles.SelectNodes("/bundles/bundle")
        '                _ChannelBundle = New List(Of cBookingType)
        '                For Each tmpChannel As XmlElement In tmpBundle.SelectNodes("channels/channel")
        '                    _ChannelBundle.Add(Campaign.Channels(tmpChannel.GetAttribute("name")).BookingTypes(tmpChannel.GetAttribute("bookingtype")))
        '                Next
        '                ChannelBundles.Add(tmpBundle.GetAttribute("name"), _ChannelBundle)
        '            Next

        '        Catch

        '        End Try
        '    Else
        '        Exit Sub
        '    End If
        'End Sub

        '---------------------------------------------------------------------------------------
        ' Procedure : CreateChannels
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        Public Sub CreateChannels()
            '(TrinitySettings.ConnectionStringCommon <> "")
            If (TrinitySettings.ConnectionStringCommon <> "") Then
                If Not DBReader.readChannels(Me) Then
                    MsgBox("Could not read channels from database...", MsgBoxStyle.Critical, "ERROR")
                End If

                For Each TmpChan As cChannel In Me.Channels
                    If Not DBReader.readBookingTypes(TmpChan) Then
                        MsgBox("Could not read bookingtypes for channel: " & TmpChan.ChannelName, MsgBoxStyle.Critical, "ERROR")
                    End If
                Next
                Helper.WriteToLogFile("Done reading channels from database")
            Else
                Dim XMLDoc As Xml.XmlDocument = New Xml.XmlDocument
                Dim XMLChannels As Xml.XmlElement
                Dim XMLTmpNode As Xml.XmlElement
                Dim XMLTmpNode2 As Xml.XmlElement
                Dim XMLBookingTypes As Xml.XmlElement
                Dim TmpChannel As cChannel
                Dim TmpBT As cBookingType

                XMLDoc.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & mvarArea & "\Channels.xml")

                XMLChannels = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("Channels")

                XMLTmpNode = XMLChannels.ChildNodes.Item(0)

                mvarChannels = New cChannels(Me)
                AddChannelsEventHandling()
                mvarChannels.MainObject = Me

                While Not XMLTmpNode Is Nothing
                    TmpChannel = mvarChannels.Add(XMLTmpNode.GetAttribute("Name"), "", mvarArea)
                    TmpChannel.fileName = "Channels.xml"

                    If XMLTmpNode.GetAttribute("IsUserEditable") <> "" Then TmpChannel.IsUserEditable = XMLTmpNode.GetAttribute("IsUserEditable")

                    XMLBookingTypes = XMLTmpNode.SelectSingleNode("Bookingtypes")
                    If XMLBookingTypes Is Nothing Then
                        XMLBookingTypes = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("Bookingtypes")
                    End If
                    XMLTmpNode2 = XMLBookingTypes.ChildNodes.Item(0)
                    Dim FoundSpecialIndex As Boolean = False
                    While Not XMLTmpNode2 Is Nothing
                        TmpBT = TmpChannel.BookingTypes.Add(XMLTmpNode2.GetAttribute("Name"), False)
                        TmpBT.Shortname = XMLTmpNode2.GetAttribute("Shortname")
                        TmpBT.IsRBS = XMLTmpNode2.GetAttribute("IsRBS")
                        TmpBT.IsSpecific = XMLTmpNode2.GetAttribute("IsSpecific")
                        If Not XMLTmpNode2.GetAttribute("IndexDiffersFromChannel") = "" Then
                            TmpBT.IndexDiffersFromChannel = XMLTmpNode2.GetAttribute("IndexDiffersFromChannel")
                        End If
                        If Not XMLTmpNode2.GetAttribute("IsPremium") = "" Then
                            TmpBT.IsPremium = XMLTmpNode2.GetAttribute("IsPremium") '
                        End If
                        If Not XMLTmpNode2.GetAttribute("IsCompensation") = "" Then
                            TmpBT.IsCompensation = XMLTmpNode2.GetAttribute("IsCompensation") '
                        End If
                        If Not XMLTmpNode2.GetAttribute("IsSponsorship") = "" Then
                            TmpBT.IsSponsorship = XMLTmpNode2.GetAttribute("IsSponsorship") '
                        End If
                        If Not XMLTmpNode2.GetAttribute("SpecificsFactor") = "" Then
                            TmpBT.EnhancementFactor = XMLTmpNode2.GetAttribute("SpecificsFactor")
                        End If
                        TmpBT.PricelistName = XMLTmpNode2.GetAttribute("Pricelist")
                        If Not XMLTmpNode2.GetAttribute("PrintDayparts") = "" Then
                            TmpBT.PrintDayparts = XMLTmpNode2.GetAttribute("PrintDayparts")
                            TmpBT.PrintBookingCode = XMLTmpNode2.GetAttribute("PrintBookingCode")
                        Else
                            TmpBT.PrintDayparts = True
                            TmpBT.PrintBookingCode = False
                        End If

                        Dim XMLSpotIndexes As Xml.XmlElement

                        XMLSpotIndexes = XMLTmpNode2.GetElementsByTagName("SpotIndex")(0)

                        If XMLSpotIndexes IsNot Nothing Then
                            TmpBT.IndexDiffersFromChannel = True
                            For Each XMLSpotIndex As Xml.XmlElement In XMLSpotIndexes
                                TmpBT.FilmIndex(XMLSpotIndex.GetAttribute("Length")) = XMLSpotIndex.GetAttribute("Idx")
                            Next
                        End If


                        TmpBT.ReadDefaultDayparts()

                        Dim xmltmpnode3 As Xml.XmlElement

                        If TmpBT.IndexDiffersFromChannel Then
                            xmltmpnode3 = XMLTmpNode2.GetElementsByTagName("SpotIndex").Item(0).FirstChild
                            While Not xmltmpnode3 Is Nothing
                                TmpBT.FilmIndex(xmltmpnode3.GetAttribute("Length")) = xmltmpnode3.GetAttribute("Idx")
                                xmltmpnode3 = xmltmpnode3.NextSibling
                            End While
                        Else
                            xmltmpnode3 = XMLTmpNode.GetElementsByTagName("SpotIndex").Item(0).FirstChild
                            While Not xmltmpnode3 Is Nothing
                                TmpBT.FilmIndex(xmltmpnode3.GetAttribute("Length")) = xmltmpnode3.GetAttribute("Idx")
                                xmltmpnode3 = xmltmpnode3.NextSibling
                            End While
                        End If

                        XMLTmpNode2 = XMLTmpNode2.NextSibling
                    End While
                    XMLTmpNode = XMLTmpNode.NextSibling
                End While

                'get the default film index
                Dim TmpXml As XmlElement = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("DefaultSpotIndex")
                If Not TmpXml Is Nothing Then
                    XMLTmpNode = TmpXml.ChildNodes.Item(0)
                    While Not XMLTmpNode Is Nothing
                        Me.Channels.DefaultFilmIndex(XMLTmpNode.GetAttribute("Length")) = XMLTmpNode.GetAttribute("Idx")
                        XMLTmpNode = XMLTmpNode.NextSibling
                    End While
                End If

                'For Each tmpChan As cChannel In mvarChannels
                '    Dim A As String = tmpChan.ChannelName
                '    Dim B As String = tmpChan.ConnectedChannel
                '    If B <> "" Then
                '        If Not mvarChannels.Contains(B) Then
                '            MessageBox.Show(A & " is connected to an improperly named channel - '" & B & "'")
                '        Else
                '            If A <> mvarChannels(B).ConnectedChannel Then
                '                MessageBox.Show(A & " is connected to " & B & ", but " & B & " is not connected to " & A & ". Please correct this in Define Channels")
                '            End If
                '        End If

                '    End If
                'Next

            End If

        End Sub

        Public Sub LoadDefaultContract()
            Contract = New Trinity.cContract(Campaign)
            If TrinitySettings.SaveCampaignsAsFiles Then
                Contract.Load(TrinitySettings.DefaultContractPath)
            Else
                Contract.Load("", True, DBReader.getContract(TrinitySettings.DefaultContractDatabaseID).OuterXml.ToString)
            End If
            Contract.ApplyToCampaign()
            If TrinitySettings.DefaultContractLoadCosts Then
                Costs = Campaign.Contract.Costs
            End If
            If TrinitySettings.DefaultContractLoadCombinations Then
                Combinations = Campaign.Contract.Combinations
            End If
            'Contract = Nothing

            'For Each TmpChan As cChannel In Channels
            '    For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
            '        If Not Contract.Channels(TmpChan.ChannelName) Is Nothing AndAlso Not Contract.Channels(TmpChan.ChannelName).BookingTypes(1)(TmpBT.Name) Is Nothing Then
            '            If TrinitySettings.DefaultContractLoadAddedValues Then
            '                For Each TmpAV As Trinity.cAddedValue In Contract.Channels(TmpChan.ChannelName).BookingTypes(1)(TmpBT.Name).AddedValues
            '                    With TmpBT.AddedValues.Add(TmpAV.Name)
            '                        .IndexGross = TmpAV.IndexGross
            '                        .IndexNet = TmpAV.IndexNet
            '                        .UseThis = TrinitySettings.DefaultUseThis
            '                    End With
            '                Next
            '            End If
            '            If TrinitySettings.DefaultContractLoadIndexes Then
            '                'TmpBT.Indexes = Contract.Channels(TmpChan.ChannelName).BookingTypes(1)(TmpBT.Name).Indexes
            '                TmpBT.Indexes.Clear()
            '                For Each TmpIndex As Trinity.cIndex In Contract.Channels(TmpChan.ChannelName).BookingTypes(1)(TmpBT.Name).Indexes
            '                    With TmpBT.Indexes.Add(TmpIndex.Name, TmpIndex.ID)
            '                        .FromDate = TmpIndex.FromDate
            '                        .Index = TmpIndex.Index
            '                        .IndexOn = TmpIndex.IndexOn '
            '                        .SystemGenerated = TmpIndex.SystemGenerated
            '                        .ToDate = TmpIndex.ToDate
            '                        For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
            '                            With .Enhancements.Add
            '                                .Amount = TmpEnh.Amount
            '                                .Name = TmpEnh.Name
            '                                .ID = TmpEnh.ID
            '                            End With
            '                        Next

            '                    End With
            '                Next
            '            End If
            '            If TrinitySettings.DefaultContractLoadTargets Then
            '                If Contract.Channels(TmpChan.ChannelName).BookingTypes(1)(TmpBT.Name).ContractTargets.Count > 0 Then
            '                    For Each TmpTarget As Trinity.cContractTarget In Contract.Channels(TmpChan.ChannelName).BookingTypes(1)(TmpBT.Name).ContractTargets
            '                        If TmpTarget.IsContractTarget Then
            '                            Dim Target As Trinity.cPricelistTarget = TmpBT.Pricelist.Targets.Add(TmpTarget.TargetName, TmpBT)

            '                            Target.IsEntered = TmpTarget.IsEntered

            '                            If TmpTarget.IsEntered = Trinity.cContractTarget.EnteredEnum.eCPP Then
            '                                Target.NetCPP = TmpTarget.EnteredValue
            '                            ElseIf TmpTarget.IsEntered = Trinity.cContractTarget.EnteredEnum.eCPT Then
            '                                Target.NetCPT = TmpTarget.EnteredValue
            '                            Else
            '                                Target.Discount = TmpTarget.EnteredValue
            '                            End If

            '                            'For i = 0 To TmpBT.Dayparts.Count
            '                            'Target.DefaultDaypart(i) = TmpTarget.DefaultDaypart(i)
            '                            'Next

            '                            Target.CalcCPP = TmpTarget.CalcCPP
            '                            Target.Target.TargetType = TmpTarget.TargetType
            '                            For Each TmpPeriod As Trinity.cPricelistPeriod In TmpTarget.PricelistPeriods
            '                                Dim period As Trinity.cPricelistPeriod = TmpTarget.PricelistPeriods.Add(TmpPeriod.Name)
            '                                period.FromDate = TmpPeriod.FromDate
            '                                period.ToDate = TmpPeriod.ToDate
            '                                period.PriceIsCPP = TmpPeriod.PriceIsCPP
            '                                period.TargetNat = TmpPeriod.TargetNat
            '                                period.TargetUni = TmpPeriod.TargetUni
            '                                For j As Integer = 0 To TmpBT.Dayparts.Count - 1
            '                                    period.Price(j) = TmpPeriod.Price(j)
            '                                Next
            '                            Next

            '                            Target.Target.TargetName = TmpTarget.AdEdgeTargetName
            '                        End If
            '                    Next
            '                    'TmpBT.Pricelist = Contract.Channels(TmpChan.ChannelName).BookingTypes(1)(TmpBT.Name).Pricelist
            '                End If
            '            End If
            '            TmpBT.BookIt = False
            '        End If
            '        'If Not Contract.Channels(TmpChan.ChannelName) Is Nothing AndAlso Not Contract.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name) Is Nothing Then
            '        '    If TrinitySettings.DefaultContractLoadAddedValues Then
            '        '        For Each TmpAV As Trinity.cAddedValue In Contract.Channels(TmpChan.ChannelName).BookingTypes(Contract.Channels(TmpChan.ChannelName).ActiveContractLevel)(TmpBT.Name).AddedValues
            '        '            With TmpBT.AddedValues.Add(TmpAV.Name)
            '        '                .IndexGross = TmpAV.IndexGross
            '        '                .IndexNet = TmpAV.IndexNet
            '        '            End With
            '        '        Next
            '        '    End If
            '        '    If TrinitySettings.DefaultContractLoadIndexes Then
            '        '        TmpBT.Indexes = Contract.Channels(TmpChan.ChannelName).BookingTypes(Contract.Channels(TmpChan.ChannelName).ActiveContractLevel)(TmpBT.Name).Indexes
            '        '    End If
            '        '    If TrinitySettings.DefaultContractLoadPrices Then
            '        '        Stop
            '        '        'If Contract.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Pricelist.Targets.Count > 0 Then
            '        '        '    TmpBT.Pricelist = Contract.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Pricelist
            '        '        'End If
            '        '    End If
            '        '    TmpBT.BookIt = False
            '        'End If
            '    Next
            'Next
        End Sub

        '---------------------------------------------------------------------------------------
        ' Procedure : ChannelString
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   : Returns a string with channels???
        '---------------------------------------------------------------------------------------
        '
        Public Function ChannelString() As String
            Dim TmpChan As cChannel
            Dim TmpBT As cBookingType
            Dim IsUsed As Boolean
            Dim Tmpstr As String

            Tmpstr = ""
            For Each TmpChan In mvarChannels
                IsUsed = False
                For Each TmpBT In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        IsUsed = True
                        Exit For
                    End If
                Next TmpBT
                If IsUsed Then
                    Tmpstr = Tmpstr & TmpChan.AdEdgeNames & ","
                End If
            Next TmpChan
            ChannelString = Tmpstr
        End Function



        '---------------------------------------------------------------------------------------
        ' Procedure : FilmcodeString
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   : Creates a string of all films in the campaign???
        '---------------------------------------------------------------------------------------
        '
        Public Function FilmcodeString() As String
            Dim TmpChan As cChannel
            Dim TmpBT As cBookingType
            Dim TmpFilm As cFilm
            Dim Tmpstr As String

            Tmpstr = ""
            For Each TmpChan In mvarChannels
                For Each TmpBT In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        For Each TmpFilm In TmpBT.Weeks(1).Films
                            If TmpFilm.Filmcode.EndsWith("*") Then
                                Tmpstr += GetWildcardFilmcodes(TmpFilm.Filmcode)
                            Else
                                If InStr(Tmpstr, "," & TmpFilm.Filmcode & ",") = 0 Then
                                    Tmpstr = Tmpstr & "," & TmpFilm.Filmcode & ","
                                End If
                            End If
                        Next TmpFilm
                    End If
                Next TmpBT
            Next TmpChan
            FilmcodeString = Tmpstr
        End Function




        '---------------------------------------------------------------------------------------
        ' Procedure : ReachActual
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        Public Function ReachActual(ByRef Freq As Object, Optional ByRef Target As ReachTargetEnum = 0, Optional ByRef CustomTarget As String = "3+", Optional ByRef Customuniverse As String = "") As Single
            Dim t As String
            Dim u As String
            Dim Dummy As Short

            On Error Resume Next
            If Target = ReachTargetEnum.rteMainTarget Then
                t = mvarMainTarget.TargetName
                u = mvarMainTarget.Universe
                Dummy = TargColl(t, mvarAdedge)
                If Err.Number > 0 Then
                    Dummy = mvarMainTarget.UniSize
                End If
            ElseIf Target = ReachTargetEnum.rteSecondTarget Then
                t = mvarSecondaryTarget.TargetName
                u = mvarSecondaryTarget.Universe
                Dummy = TargColl(t, mvarAdedge)
                If Err.Number > 0 Then
                    Dummy = mvarSecondaryTarget.UniSize
                End If
            ElseIf Target = ReachTargetEnum.rteThirdTarget Then
                t = mvarThirdTarget.TargetName
                u = mvarThirdTarget.Universe
                Dummy = TargColl(t, mvarAdedge)
                If Err.Number > 0 Then
                    Dummy = mvarThirdTarget.UniSize
                End If
            ElseIf Target = ReachTargetEnum.rteAllAdults Then
                t = mvarAllAdults
                u = ""
            ElseIf Target = ReachTargetEnum.rteCustomTarget Then
                t = CustomTarget
                u = Customuniverse
            Else
                t = ""
                u = ""
            End If
            If Not mvarAdedge.getGroupCount = 0 Then
                Return mvarAdedge.getRF(mvarAdedge.getGroupCount - 1, , TimeShift, TargColl(t, mvarAdedge) - 1, Freq)
            Else
                Return 0
            End If

            If Freq = 1 Then
                Debug.WriteLine("ReachActual")
                Debug.WriteLine("Groupcount: " & mvarAdedge.getGroupCount - 1)
                Debug.WriteLine("u: " & u)
                Debug.WriteLine("t: " & t)
                Debug.WriteLine("targcoll: " & TargColl(t, mvarAdedge) - 1)
                Debug.WriteLine("obj: " & mvarAdedge.debug)
                Debug.WriteLine("groupcount:" & mvarAdedge.getGroupCount)
            End If
        End Function

        Public Function GetRF(ByVal freq As Object, ByVal tar As String) As Object
            Dim f As Object = Adedge.getRF(Adedge.getGroupCount - 1, 0, TimeShift, TargColl(tar, Adedge) - 1, freq)
            Return f
        End Function

        '---------------------------------------------------------------------------------------
        ' Procedure : ReachGoal
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        Public Property ReachGoal(ByVal Freq As Single, Optional ByVal Target As ReachTargetEnum = ReachTargetEnum.rteMainTarget)
            Get
                Return mvarReachGoal(Freq, Target)
            End Get
            Set(ByVal value)
                mvarReachGoal(Freq, Target) = value
            End Set
        End Property




        '---------------------------------------------------------------------------------------
        ' Procedure : LoadOldCampaign
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   :
        '---------------------------------------------------------------------------------------
        '
        Public Sub LoadOldCampaign(ByRef Path As String)

            Dim XMLDoc As Xml.XmlDocument = New Xml.XmlDocument
            Dim XMLCamp As Xml.XmlElement
            Dim XMLTmpNode As Xml.XmlElement
            Dim XMLTmpNode2 As Xml.XmlElement
            Dim IniFile As New clsIni

            Dim XMLChannel As Xml.XmlElement
            Dim XMLBookingType As Xml.XmlElement
            Dim XMLWeek As Xml.XmlElement
            Dim XMLFilm As Xml.XmlElement
            Dim XMLTarget As Xml.XmlElement
            Dim XMLBuyTarget As Xml.XmlElement
            Dim XMLPricelist As Xml.XmlElement
            Dim XMLSpot As Xml.XmlElement

            Dim TmpChannel As cChannel
            Dim TmpBookingType As cBookingType
            Dim TmpWeek As cWeek
            Dim TmpFilm As cFilm
            Dim TmpPLTarget As cPricelistTarget
            Dim TmpIndex As cIndex
            Dim TmpPlannedSpot As cPlannedSpot
            Dim TmpActualSpot As cActualSpot

            Dim i As Short
            Dim TmpString As String
            Dim TmpLong As Integer
            Dim TmpInt As Short

            On Error Resume Next

            XMLDoc.Load(Path)

            IniFile.Create(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & mvarArea & "\area.ini")
            XMLCamp = XMLDoc.GetElementsByTagName("Campaign").Item(0)

            mvarVersion = XMLCamp.GetAttribute("Version")
            mvarName = XMLCamp.GetAttribute("Name")
            mvarUpdatedTo = XMLCamp.GetAttribute("UpdatedTo")
            mvarPlanner = XMLCamp.GetAttribute("Planner")
            mvarBuyer = XMLCamp.GetAttribute("Buyer")
            mvarFrequencyFocus = XMLCamp.GetAttribute("FrequencyFocus")
            mvarFilename = ""
            'TODO: Produkt och Kund - om de inte finns ska de läggas till? Annars
            '      skall ett id hittas i databasen
            mvarBudgetTotalCTC = XMLCamp.GetAttribute("BudgetTotalCTC")
            If XMLCamp.GetAttribute("ServiceFee") > 0 Then
                If XMLCamp.GetAttribute("ServiceFeeOnNet1") Then
                    mvarCosts.Add("Service fee", cCost.CostTypeEnum.CostTypePercent, XMLCamp.GetAttribute("ServiceFee") / 100, cCost.CostOnPercentEnum.CostOnNetNet, 0)
                Else
                    mvarCosts.Add("Service fee", cCost.CostTypeEnum.CostTypePercent, XMLCamp.GetAttribute("ServiceFee") / 100, cCost.CostOnPercentEnum.CostOnNet, 0)
                End If
            End If
            If XMLCamp.GetAttribute("TrackingCost") > 0 Then
                mvarCosts.Add("Tracking", cCost.CostTypeEnum.CostTypeFixed, XMLCamp.GetAttribute("TrackingCost"), 0, 217)
            End If
            If XMLCamp.GetAttribute("TVCheck") > 0 Then
                mvarCosts.Add("TV Check", cCost.CostTypeEnum.CostTypePerUnit, XMLCamp.GetAttribute("TVCheck"), cCost.CostOnUnitEnum.CostOnSpots, 206)
            End If
            mvarCommentary = XMLCamp.GetAttribute("Commentary")
            Area = XMLCamp.GetAttribute("Area")
            mvarAreaLog = XMLCamp.GetAttribute("AreaLog")
            mvarAllAdults = XMLCamp.GetAttribute("AllAdults")

            XMLTmpNode = XMLCamp.GetElementsByTagName("Dayparts").Item(0).FirstChild
            _dayparts.Clear()
            While Not XMLTmpNode Is Nothing
                Dim _dp As New cDaypart
                _dp.Name = XMLTmpNode.LocalName
                _dp.StartMaM = XMLTmpNode.GetAttribute("Start")
                _dp.EndMaM = XMLTmpNode.GetAttribute("End")
                _dayparts.Add(_dp)
                XMLTmpNode = XMLTmpNode.NextSibling
            End While
            XMLTarget = XMLCamp.SelectSingleNode("MainTarget")
            mvarMainTarget.TargetName = XMLTarget.GetAttribute("Name")
            mvarMainTarget.TargetType = XMLTarget.GetAttribute("Type")
            mvarMainTarget.Universe = XMLTarget.GetAttribute("Universe")
            mvarMainTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
            If mvarMainTarget.SecondUniverse = "" Then
                mvarMainTarget.SecondUniverse = IniFile.Text("Universe", "Second")
            End If

            XMLTarget = XMLCamp.SelectSingleNode("SecondaryTarget")
            mvarSecondaryTarget.TargetName = XMLTarget.GetAttribute("Name")
            mvarSecondaryTarget.TargetType = XMLTarget.GetAttribute("Type")
            mvarSecondaryTarget.Universe = XMLTarget.GetAttribute("Universe")
            mvarSecondaryTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
            If mvarSecondaryTarget.SecondUniverse = "" Then
                mvarSecondaryTarget.SecondUniverse = IniFile.Text("Universe", "Second")
            End If

            XMLTarget = XMLCamp.SelectSingleNode("ThirdTarget")
            mvarThirdTarget.TargetName = XMLTarget.GetAttribute("Name")
            mvarThirdTarget.TargetType = XMLTarget.GetAttribute("Type")
            mvarThirdTarget.Universe = XMLTarget.GetAttribute("Universe")
            mvarThirdTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
            If mvarThirdTarget.SecondUniverse = "" Then
                mvarThirdTarget.SecondUniverse = IniFile.Text("Universe", "Second")
            End If

            mvarChannels = New cChannels(Me)
            AddChannelsEventHandling()
            mvarChannels.MainObject = Me

            Dim ObsoleteChannels As New List(Of String)

            ObsoleteChannels.Add("The Voice se")
            ObsoleteChannels.Add("Jetix se")

            XMLChannel = XMLCamp.SelectSingleNode("Channels").FirstChild
            While Not XMLChannel Is Nothing And Not ObsoleteChannels.Contains(XMLChannel.GetAttribute("AdEdgeNames"))
                TmpString = XMLChannel.GetAttribute("Name")
                TmpChannel = Nothing
                TmpChannel = mvarChannels.Add(TmpString, "")
                If Not TmpChannel Is Nothing Then
                    TmpChannel.ID = XMLChannel.GetAttribute("ID")
                    TmpChannel.Shortname = XMLChannel.GetAttribute("ShortName")
                    TmpChannel.BuyingUniverse = XMLChannel.GetAttribute("BuyingUniverse")
                    TmpChannel.AdEdgeNames = XMLChannel.GetAttribute("AdEdgeNames")
                    If TmpChannel.AdEdgeNames = "Kan 5" Then
                        TmpChannel.AdEdgeNames = "Kanal 5"
                    End If
                    TmpChannel.DefaultArea = XMLChannel.GetAttribute("DefaultArea")
                    TmpChannel.AgencyCommission = Conv(XMLChannel.GetAttribute("AgencyCommission"))
                    If Not IsDBNull(XMLChannel.GetAttribute("DeliveryAddress")) Then
                        TmpChannel.DeliveryAddress = XMLChannel.GetAttribute("DeliveryAddress")
                    End If

                    'Save the targets

                    XMLTarget = XMLChannel.GetElementsByTagName("MainTarget").Item(0)
                    TmpChannel.MainTarget.NoUniverseSize = True
                    If XMLTarget.GetAttribute("Name") = "4-" Then
                        TmpChannel.MainTarget.TargetName = "20-44"
                    Else
                        TmpChannel.MainTarget.TargetName = XMLTarget.GetAttribute("Name")
                    End If
                    TmpChannel.MainTarget.TargetType = XMLTarget.GetAttribute("Type")
                    TmpChannel.MainTarget.Universe = XMLTarget.GetAttribute("Universe")
                    TmpChannel.MainTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
                    If TmpChannel.MainTarget.SecondUniverse = "" Then
                        TmpChannel.MainTarget.SecondUniverse = IniFile.Text("Universe", "Second")
                    End If
                    TmpChannel.MainTarget.NoUniverseSize = False

                    XMLTarget = XMLChannel.GetElementsByTagName("SecondaryTarget").Item(0)
                    TmpChannel.SecondaryTarget.NoUniverseSize = True
                    TmpChannel.SecondaryTarget.TargetName = XMLTarget.GetAttribute("Name")
                    TmpChannel.SecondaryTarget.TargetType = XMLTarget.GetAttribute("Type")
                    TmpChannel.SecondaryTarget.Universe = XMLTarget.GetAttribute("Universe")
                    TmpChannel.SecondaryTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
                    If TmpChannel.SecondaryTarget.SecondUniverse = "" Then
                        TmpChannel.SecondaryTarget.SecondUniverse = IniFile.Text("Universe", "Second")
                    End If
                    TmpChannel.SecondaryTarget.NoUniverseSize = False
                    If Not IsDBNull(XMLTarget.GetAttribute("Marathon")) AndAlso XMLTarget.GetAttribute("Marathon") <> "" Then
                        TmpChannel.MarathonName = XMLTarget.GetAttribute("Marathon")
                    End If
                    If Not IsDBNull(XMLTarget.GetAttribute("Penalty")) AndAlso XMLTarget.GetAttribute("Penalty") <> "" Then
                        TmpChannel.Penalty = XMLTarget.GetAttribute("Penalty")
                        TmpChannel.ConnectedChannel = XMLTarget.GetAttribute("ConnectedChannel")
                    End If

                    'Read Booking types

                    XMLBookingType = XMLChannel.GetElementsByTagName("BookingTypes").Item(0).FirstChild
                    While Not XMLBookingType Is Nothing

                        TmpString = XMLBookingType.GetAttribute("Name")
                        TmpBookingType = TmpChannel.BookingTypes.Add(TmpString)
                        TmpBookingType.Shortname = XMLBookingType.GetAttribute("Shortname")

                        TmpBookingType.IndexMainTarget = Conv(XMLBookingType.GetAttribute("IndexMainTarget"))
                        TmpBookingType.IndexAllAdults = Conv(XMLBookingType.GetAttribute("Index3plus"))
                        XMLTmpNode = XMLBookingType.GetElementsByTagName("DaypartSplit").Item(0)
                        For i = 0 To TmpBookingType.Dayparts.Count - 1
                            XMLTmpNode2 = XMLTmpNode.GetElementsByTagName(TmpBookingType.Dayparts(i).Name).Item(0)
                            TmpBookingType.Dayparts(i).Share = Val(XMLTmpNode2.GetAttribute("Share"))
                        Next
                        TmpBookingType.BookIt = XMLBookingType.GetAttribute("BookIt")
                        'TmpBookingType.GrossCPP = Conv(XMLBookingType.GetAttribute("GrossCPP"))
                        TmpBookingType.AverageRating = Conv(XMLBookingType.GetAttribute("AverageRating"))
                        TmpBookingType.ConfirmedNetBudget = Conv(XMLBookingType.GetAttribute("ConfirmedNetBudget"))
                        TmpBookingType.Bookingtype = XMLBookingType.GetAttribute("Bookingtype")
                        TmpBookingType.ContractNumber = XMLBookingType.GetAttribute("ContractNumber")
                        TmpBookingType.IsRBS = XMLBookingType.GetAttribute("IsRBS")
                        TmpBookingType.IsSpecific = XMLBookingType.GetAttribute("IsSpecific")
                        If Not XMLBookingType.GetAttribute("IsPremium") = "" Then
                            TmpBookingType.IsPremium = XMLBookingType.GetAttribute("IsPremium")
                        End If
                        If XMLBookingType.GetAttribute("PrintDayparts") = "" Then
                            TmpBookingType.PrintDayparts = True
                        Else
                            TmpBookingType.PrintDayparts = XMLBookingType.GetAttribute("PrintDayparts")
                        End If
                        If XMLBookingType.GetAttribute("PrintBookingCode") = "" Then
                            TmpBookingType.PrintBookingCode = True
                        Else
                            TmpBookingType.PrintBookingCode = XMLBookingType.GetAttribute("PrintBookingCode")
                        End If



                        If Not IsDBNull(XMLBookingType.GetAttribute("OrderNumber")) Then
                            TmpBookingType.OrderNumber = XMLBookingType.GetAttribute("OrderNumber")
                        Else
                            TmpBookingType.OrderNumber = ""
                        End If

                        'Read Buyingtarget

                        XMLBuyTarget = XMLBookingType.SelectSingleNode("BuyingTarget")

                        TmpBookingType.BuyingTarget.CalcCPP = XMLBuyTarget.GetAttribute("CalcCPP")
                        'TmpBookingType.BuyingTarget.CPP = Conv(XMLBuyTarget.GetAttribute("CPP"))
                        TmpBookingType.BuyingTarget.TargetName = XMLBuyTarget.GetAttribute("TargetName")
                        'TmpBookingType.BuyingTarget.UniSize = XMLBuyTarget.GetAttribute("UniSize")
                        'TmpBookingType.BuyingTarget.UniSizeNat = XMLBuyTarget.GetAttribute("UniSizeNat")
                        'If TmpChannel.BuyingUniverse = "" Then
                        '    TmpBookingType.BuyingTarget.UniSize = TmpBookingType.BuyingTarget.UniSizeNat
                        'End If

                        'XMLTmpNode = XMLBuyTarget.GetElementsByTagName("DaypartCPP").Item(0)
                        'For i = 0 To DaypartCount - 1
                        '    If IsDBNull(XMLTmpNode.GetAttribute(mvarDaypartName(i))) Then
                        '        XMLTmpNode.SetAttribute(mvarDaypartName(i), 0)
                        '    End If
                        '    TmpBookingType.BuyingTarget.CPPDaypart(i) = Conv(XMLTmpNode.GetAttribute(mvarDaypartName(i)))
                        'Next

                        XMLTarget = XMLBuyTarget.GetElementsByTagName("Target").Item(0)
                        TmpBookingType.BuyingTarget.Target.NoUniverseSize = True
                        TmpBookingType.BuyingTarget.Target.TargetName = XMLTarget.GetAttribute("Name")
                        TmpBookingType.BuyingTarget.Target.TargetType = XMLTarget.GetAttribute("Type")
                        TmpBookingType.BuyingTarget.Target.Universe = XMLTarget.GetAttribute("Universe")
                        TmpBookingType.BuyingTarget.Target.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
                        If TmpBookingType.BuyingTarget.Target.SecondUniverse = "" Then
                            TmpBookingType.BuyingTarget.Target.SecondUniverse = IniFile.Text("Universe", "Second")
                        End If
                        TmpBookingType.BuyingTarget.Target.NoUniverseSize = False
                        If Not IsDBNull(XMLBookingType.GetAttribute("IsEntered")) Then
                            TmpBookingType.BuyingTarget.IsEntered = XMLBookingType.GetAttribute("IsEntered")
                        End If
                        If TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eDiscount Then
                            TmpBookingType.BuyingTarget.Discount = XMLBookingType.GetAttribute("Discount")
                        ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPT Then
                            TmpBookingType.BuyingTarget.NetCPT = Conv(XMLBookingType.GetAttribute("NetCPT"))
                        Else
                            TmpBookingType.BuyingTarget.NetCPP = Conv(XMLBookingType.GetAttribute("NetCPP"))
                        End If

                        'Read the pricelist

                        XMLPricelist = XMLBookingType.GetElementsByTagName("Pricelist").Item(0)

                        XMLTmpNode = XMLPricelist.GetElementsByTagName("DaypartSplit").Item(0)
                        '            For i = 0 To DaypartCount - 1
                        '                Set XMLTmpNode2 = XMLTmpNode.getElementsByTagName(mvarDaypartName(i)).Item(0)
                        '                TmpBookingType.Pricelist.DefaultDaypart(i) = Val(XMLTmpNode2.getAttribute("DefaultSplit"))
                        '            Next

                        TmpBookingType.Pricelist.StartDate = XMLPricelist.GetAttribute("StartDate")
                        TmpBookingType.Pricelist.EndDate = XMLPricelist.GetAttribute("EndDate")
                        TmpBookingType.Pricelist.BuyingUniverse = XMLPricelist.GetAttribute("BuyingUniverse")

                        XMLBuyTarget = XMLPricelist.GetElementsByTagName("Targets").Item(0).FirstChild

                        While Not XMLBuyTarget Is Nothing

                            TmpString = XMLBuyTarget.GetAttribute("Name")
                            TmpPLTarget = TmpBookingType.Pricelist.Targets.Add(TmpString, TmpBookingType)
                            TmpPLTarget.CalcCPP = XMLBuyTarget.GetAttribute("CalcCPP")
                            'TmpPLTarget.CPP = Conv(XMLBuyTarget.GetAttribute("CPP"))

                            'XMLTmpNode = XMLBuyTarget.GetElementsByTagName("DaypartCPP").Item(0)
                            'For i = 0 To DaypartCount - 1
                            '    XMLTmpNode2 = XMLTmpNode.GetElementsByTagName(mvarDaypartName(i)).Item(0)
                            '    TmpPLTarget.CPPDaypart(i) = Conv(XMLTmpNode2.GetAttribute("CPP"))
                            'Next

                            'TmpPLTarget.UniSize = Conv(XMLBuyTarget.GetAttribute("UniSize"))
                            'TmpPLTarget.UniSizeNat = Conv(XMLBuyTarget.GetAttribute("UniSizeNat"))

                            XMLTarget = XMLBuyTarget.GetElementsByTagName("Target").Item(0)
                            TmpPLTarget.Target.NoUniverseSize = True 'For speed. No unisizes are calculated
                            TmpPLTarget.Target.TargetName = XMLTarget.GetAttribute("Name")
                            TmpPLTarget.Target.TargetType = XMLTarget.GetAttribute("Type")
                            TmpPLTarget.Target.Universe = XMLTarget.GetAttribute("Universe")
                            TmpPLTarget.Target.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
                            If TmpPLTarget.Target.SecondUniverse = "" Then
                                TmpPLTarget.Target.SecondUniverse = IniFile.Text("Universe", "Second")
                            End If
                            If Not IsDBNull(XMLBookingType.GetAttribute("IsEntered")) Then
                                TmpPLTarget.IsEntered = XMLBookingType.GetAttribute("IsEntered")
                            End If

                            'For i = 0 To TmpBookingType.Dayparts.Count
                            '    TmpPLTarget.DefaultDaypart(i) = TmpBookingType.Dayparts(i).Share
                            'Next

                            If TmpPLTarget.IsEntered = cPricelistTarget.EnteredEnum.eDiscount Then
                                TmpPLTarget.Discount = XMLBookingType.GetAttribute("Discount")
                            ElseIf TmpPLTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPT Then
                                TmpPLTarget.NetCPT = Conv(XMLBookingType.GetAttribute("NetCPT"))
                            Else
                                TmpPLTarget.NetCPP = Conv(XMLBookingType.GetAttribute("NetCPP"))
                            End If

                            TmpPLTarget.Target.NoUniverseSize = False
                            TmpPLTarget.StandardTarget = XMLTarget.GetAttribute("StandardTarget")

                            XMLBuyTarget = XMLBuyTarget.NextSibling
                        End While

                        'Save weeks

                        XMLWeek = XMLBookingType.GetElementsByTagName("Weeks").Item(0).FirstChild

                        While Not XMLWeek Is Nothing
                            'If TmpBookingType.Name = "Specifics" And TmpChannel.ChannelName = "TV4" Then Stop
                            TmpString = XMLWeek.GetAttribute("Name")
                            TmpWeek = TmpBookingType.Weeks.Add(TmpString)
                            TmpWeek.TRPControl = XMLWeek.GetAttribute("TRPControl")
                            If TmpWeek.TRPControl Then
                                TmpWeek.TRP = Conv(XMLWeek.GetAttribute("TRP"))
                                'TmpWeek.TRPBuyingTarget = XMLWeek.GETATTRIBUTE("TRPBuyingTarget")
                                'TmpWeek.TRP3Plus = XMLWeek.GETATTRIBUTE("TRP3Plus")
                            Else
                                TmpWeek.NetBudget = Conv(XMLWeek.GetAttribute("NetBudget"))
                            End If
                            TmpWeek.StartDate = XMLWeek.GetAttribute("StartDate")
                            TmpWeek.EndDate = XMLWeek.GetAttribute("EndDate")
                            If XMLWeek.GetAttribute("Modifier") = 2 Or XMLWeek.GetAttribute("Modifier") = 0 Then
                                If CDbl(Conv(XMLWeek.GetAttribute("SeasonIndex"))) <> 100 Then
                                    TmpIndex = TmpBookingType.Indexes.Add("Season index")
                                    TmpIndex.Index = XMLWeek.GetAttribute("SeasonIndex")
                                    TmpIndex.FromDate = Date.FromOADate(TmpWeek.StartDate)
                                    TmpIndex.ToDate = Date.FromOADate(TmpWeek.EndDate)
                                    TmpIndex.IndexOn = cIndex.IndexOnEnum.eNetCPP
                                End If
                            ElseIf XMLWeek.GetAttribute("Modifier") = 3 Then
                                TmpIndex = TmpBookingType.Indexes.Add("Season index")
                                TmpIndex.Index = ((1 - XMLWeek.GetAttribute("Discount")) / (1 - XMLBookingType.GetAttribute("Discount"))) * 100
                                TmpIndex.FromDate = Date.FromOADate(TmpWeek.StartDate)
                                TmpIndex.ToDate = Date.FromOADate(TmpWeek.EndDate)
                                TmpIndex.IndexOn = cIndex.IndexOnEnum.eNetCPP
                            Else
                                If CDbl(Conv(XMLWeek.GetAttribute("SeasonIndex"))) <> 100 Then
                                    TmpIndex = TmpBookingType.Indexes.Add("Season index")
                                    TmpIndex.Index = XMLWeek.GetAttribute("SeasonIndex")
                                    TmpIndex.FromDate = Date.FromOADate(TmpWeek.StartDate)
                                    TmpIndex.ToDate = Date.FromOADate(TmpWeek.EndDate)
                                    TmpIndex.IndexOn = cIndex.IndexOnEnum.eNetCPP
                                End If
                            End If
                            Dim TmpPeriod As cPricelistPeriod
                            If XMLWeek.GetAttribute("GrossIndex") <> 1 Then
                                TmpPeriod = TmpBookingType.BuyingTarget.PricelistPeriods.Add("Index")
                                'TmpIndex.IndexOn = cIndex.IndexOnEnum.eGrossCPP
                                'TmpIndex.Index = XMLWeek.GetAttribute("GrossIndex") * 100
                                TmpPeriod.FromDate = Date.FromOADate(TmpWeek.StartDate)
                                TmpPeriod.ToDate = Date.FromOADate(TmpWeek.EndDate)
                                TmpPeriod.Price(True) = XMLWeek.GetAttribute("GrossIndex") * XMLBookingType.GetAttribute("NetCPP")
                            End If
                            For Each TmpPLTarget In TmpBookingType.Pricelist.Targets
                                If CDbl(Conv(XMLWeek.GetAttribute("GrossIndex"))) <> 1 Then
                                    TmpPeriod = TmpPLTarget.PricelistPeriods.Add("Gross index")
                                    'TmpIndex.Index = CDbl(Conv(XMLWeek.GetAttribute("GrossIndex"))) * 100
                                    TmpPeriod.FromDate = Date.FromOADate(TmpWeek.StartDate)
                                    TmpPeriod.ToDate = Date.FromOADate(TmpWeek.EndDate)
                                    'TmpIndex.IndexOn = cIndex.IndexOnEnum.eGrossCPP
                                    'TmpIndex.SystemGenerated = True
                                    TmpPeriod.Price(True) = XMLWeek.GetAttribute("GrossIndex") * XMLBookingType.GetAttribute("NetCPP")
                                End If
                            Next

                            'Save Films

                            XMLFilm = XMLWeek.GetElementsByTagName("Films").Item(0).FirstChild

                            While Not XMLFilm Is Nothing

                                TmpString = XMLFilm.GetAttribute("Filmcode")
                                TmpFilm = TmpWeek.Films.Add(TmpString)
                                TmpFilm.Filmcode = TmpString
                                TmpFilm.FilmLength = XMLFilm.GetAttribute("FilmLength")
                                If Not IsDBNull(XMLFilm.GetAttribute("AltFilmcode")) AndAlso XMLFilm.GetAttribute("AltFilmcode") <> "" Then
                                    TmpFilm.Filmcode = TmpFilm.Filmcode & "," & XMLFilm.GetAttribute("AltFilmcode")
                                End If
                                TmpFilm.Index = Conv(XMLFilm.GetAttribute("Index"))
                                TmpFilm.Share = Conv(XMLFilm.GetAttribute("Share"))
                                TmpFilm.Description = XMLFilm.GetAttribute("Description")
                                TmpBookingType.FilmIndex(TmpFilm.FilmLength) = TmpFilm.Index
                                XMLFilm = XMLFilm.NextSibling

                            End While
                            XMLWeek = XMLWeek.NextSibling
                        End While
                        XMLBookingType = XMLBookingType.NextSibling
                    End While
                End If
                XMLChannel = XMLChannel.NextSibling
            End While


            'Save Planned spots

            XMLSpot = XMLCamp.GetElementsByTagName("PlannedSpots").Item(0).FirstChild

            While Not XMLSpot Is Nothing
NextPlannedSpot:
                TmpString = XMLSpot.GetAttribute("ID")
                TmpPlannedSpot = mvarPlannedSpots.Add(TmpString)
                TmpString = XMLSpot.GetAttribute("Channel")
                TmpPlannedSpot.Channel = mvarChannels(TmpString)
                TmpPlannedSpot.ChannelID = XMLSpot.GetAttribute("ChannelID")
                TmpString = XMLSpot.GetAttribute("BookingType")
                TmpPlannedSpot.Bookingtype = TmpPlannedSpot.Channel.BookingTypes(TmpString) ' ,"cBookingType
                TmpLong = XMLSpot.GetAttribute("AirDate")
                TmpPlannedSpot.AirDate = TmpLong
                TmpString = XMLSpot.GetAttribute("Week")
                If TmpString <> "" Then
                    TmpPlannedSpot.Week = TmpPlannedSpot.Bookingtype.Weeks(TmpString) ' ,"cWeek
                Else
                    TmpPlannedSpot.Week = TmpPlannedSpot.Bookingtype.GetWeek(Date.FromOADate(TmpPlannedSpot.AirDate))
                End If
                If TmpPlannedSpot.Week Is Nothing Then
                    i = i
                End If
                TmpPlannedSpot.MaM = XMLSpot.GetAttribute("MaM")
                TmpPlannedSpot.ProgBefore = XMLSpot.GetAttribute("ProgBefore")
                TmpPlannedSpot.ProgAfter = XMLSpot.GetAttribute("ProgAfter")
                TmpPlannedSpot.Programme = XMLSpot.GetAttribute("Programme")
                TmpPlannedSpot.Advertiser = XMLSpot.GetAttribute("Advertiser")
                TmpPlannedSpot.Product = XMLSpot.GetAttribute("Product")
                TmpPlannedSpot.Filmcode = XMLSpot.GetAttribute("Filmcode")
                'On Error Resume Next
                'For Each TmpFilm In TmpPlannedSpot.Week.Films
                '    If TmpFilm.Filmcode = TmpPlannedSpot.Filmcode Or InStr(TmpFilm.Filmcode, TmpPlannedSpot.Filmcode) > 0 Then
                '        TmpPlannedSpot.Film = TmpPlannedSpot.Week.Films(TmpFilm.Filmcode)
                '    End If
                'Next TmpFilm
                'On Error GoTo ErrHandle
                TmpPlannedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = Conv(XMLSpot.GetAttribute("RatingBuyTarget"))
                TmpPlannedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteMainTarget) = Conv(XMLSpot.GetAttribute("RatingMainTarget"))
                TmpPlannedSpot.Estimation = XMLSpot.GetAttribute("Estimation")
                TmpPlannedSpot.MyRating(cPlannedSpot.PlannedTargetEnum.pteMainTarget) = Conv(XMLSpot.GetAttribute("MyRating"))
                TmpPlannedSpot.MyRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = Conv(XMLSpot.GetAttribute("MyRatingBuyTarget"))
                TmpPlannedSpot.Index = Conv(XMLSpot.GetAttribute("Index"))
                TmpPlannedSpot.SpotLength = XMLSpot.GetAttribute("SpotLength")
                TmpPlannedSpot.SpotType = XMLSpot.GetAttribute("SpotType")
                TmpPlannedSpot.PriceNet = (XMLSpot.GetAttribute("PriceNet"))
                TmpPlannedSpot.PriceGross = (XMLSpot.GetAttribute("PriceGross"))
                If Not IsDBNull(XMLSpot.GetAttribute("Remark")) Then
                    TmpPlannedSpot.Remark = XMLSpot.GetAttribute("Remark")
                End If
                If Not IsDBNull(XMLSpot.GetAttribute("Placement")) Then
                    TmpPlannedSpot.Placement = XMLSpot.GetAttribute("Placement")
                End If
                XMLSpot = XMLSpot.NextSibling
            End While

ActualSpots:
            'Save Actual spots
            XMLSpot = XMLCamp.GetElementsByTagName("ActualSpots").Item(0).FirstChild

            While Not XMLSpot Is Nothing

                TmpLong = XMLSpot.GetAttribute("AirDate")
                TmpInt = XMLSpot.GetAttribute("MaM")
                TmpActualSpot = mvarActualSpots.Add(System.DateTime.FromOADate(TmpLong), TmpInt)
                TmpString = XMLSpot.GetAttribute("Channel")
                TmpActualSpot.Channel = mvarChannels(TmpString)
                TmpActualSpot.ProgBefore = XMLSpot.GetAttribute("ProgBefore")
                TmpActualSpot.ProgAfter = XMLSpot.GetAttribute("ProgAfter")
                TmpActualSpot.Programme = XMLSpot.GetAttribute("Programme")
                TmpActualSpot.Advertiser = XMLSpot.GetAttribute("Advertiser")
                TmpActualSpot.Product = XMLSpot.GetAttribute("Product")
                TmpActualSpot.Filmcode = XMLSpot.GetAttribute("Filmcode")
                TmpActualSpot.Index = Val(XMLSpot.GetAttribute("Index"))
                TmpActualSpot.PosInBreak = XMLSpot.GetAttribute("PosInBreak")
                TmpActualSpot.SpotsInBreak = XMLSpot.GetAttribute("SpotsInBreak")
                TmpString = XMLSpot.GetAttribute("MatchedSpot")
                If TmpString <> "" Then
                    'On Error Resume Next
                    TmpActualSpot.MatchedSpot = mvarPlannedSpots(TmpString)
                    mvarPlannedSpots(TmpString).MatchedSpot = TmpActualSpot
                    'On Error GoTo ErrHandle
                End If
                TmpActualSpot.SpotLength = XMLSpot.GetAttribute("SpotLength")
                TmpActualSpot.Deactivated = XMLSpot.GetAttribute("Deactivated")
                TmpActualSpot.SpotType = XMLSpot.GetAttribute("SpotType")
                TmpActualSpot.BreakType = XMLSpot.GetAttribute("BreakType")
                TmpActualSpot.SecondRating = Conv(XMLSpot.GetAttribute("SecondRating"))
                TmpActualSpot.AdedgeChannel = XMLSpot.GetAttribute("AdEdgeChannel")
                If TmpActualSpot.AdedgeChannel = "Kan 5" Then TmpActualSpot.AdedgeChannel = "Kanal 5"
                TmpActualSpot.ID = XMLSpot.GetAttribute("ID")
                TmpString = XMLSpot.GetAttribute("BookingType")
                If TmpString <> "" Then
                    TmpActualSpot.Bookingtype = TmpActualSpot.Channel.BookingTypes(TmpString)
                Else
                    TmpActualSpot.Bookingtype = TmpActualSpot.Channel.BookingTypes(1)
                End If
                TmpActualSpot.Week = TmpActualSpot.Bookingtype.GetWeek(Date.FromOADate(TmpActualSpot.AirDate))
                TmpActualSpot.GroupIdx = XMLSpot.GetAttribute("GroupIdx")
                XMLSpot = XMLSpot.NextSibling

            End While

            Dim TmpID As String
            Dim TmpAdjustBy As Byte
            Dim TmpDate As Date
            Dim TmpBT As String
            Dim TmpChannelEstimate As Single
            Dim TmpDBID As String
            Dim TmpFilmcode As String
            Dim TmpGrossPrice As Decimal
            Dim TmpMaM As Short
            Dim tmpMyEstimate As Single
            Dim TmpMyEstChanTarget As Single
            Dim TmpNetPrice As Decimal
            Dim TmpProgAfter As String
            Dim TmpProgBefore As String
            Dim TmpProg As String
            Dim TmpIsLocal As Boolean
            Dim TmpIsRB As Boolean

            mvarBookedSpots = New cBookedSpots(Me)

            XMLSpot = XMLCamp.GetElementsByTagName("BookedSpots").Item(0).FirstChild

            While Not XMLSpot Is Nothing
                TmpID = XMLSpot.GetAttribute("ID")
                TmpAdjustBy = Val(XMLSpot.GetAttribute("AdjustBy"))
                TmpDate = XMLSpot.GetAttribute("AirDate")
                TmpBT = XMLSpot.GetAttribute("Bookingtype")
                TmpString = XMLSpot.GetAttribute("Channel") ' Channel
                TmpChannelEstimate = CSng(Conv(XMLSpot.GetAttribute("ChannelEstimate")))
                TmpDBID = XMLSpot.GetAttribute("DatabaseID")
                TmpFilmcode = XMLSpot.GetAttribute("Filmcode")
                TmpGrossPrice = Val(XMLSpot.GetAttribute("GrossPrice"))
                TmpMaM = XMLSpot.GetAttribute("MaM")
                tmpMyEstimate = CSng(Conv(XMLSpot.GetAttribute("MyEstimate")))
                TmpMyEstChanTarget = CSng(Conv(XMLSpot.GetAttribute("MyEstimateBuyTarget")))
                TmpNetPrice = CDec(Conv(XMLSpot.GetAttribute("NetPrice")))
                TmpProgAfter = XMLSpot.GetAttribute("ProgAfter")
                TmpProgBefore = XMLSpot.GetAttribute("ProgBefore")
                TmpProg = XMLSpot.GetAttribute("Programme")
                TmpDBID = TmpString & Format(TmpDate, "yyyymmdd") & Left(Helper.Mam2Tid(TmpMaM), 2) & Right(Helper.Mam2Tid(TmpMaM), 2)
                If IsDBNull(XMLSpot.GetAttribute("IsLocal")) Then
                    TmpIsLocal = False
                    TmpIsRB = False
                Else
                    TmpIsLocal = XMLSpot.GetAttribute("IsLocal")
                    TmpIsRB = XMLSpot.GetAttribute("IsRB")
                End If
                mvarBookedSpots.Add(TmpID, TmpDBID, TmpString, TmpDate, TmpMaM, TmpProg, TmpProg, TmpProg, TmpGrossPrice, TmpNetPrice, TmpChannelEstimate, tmpMyEstimate, TmpMyEstChanTarget, TmpFilmcode, TmpBT, TmpIsLocal, TmpIsRB, 0)
                If Not IsDBNull(XMLSpot.GetAttribute("Comments")) Then
                    mvarBookedSpots(TmpID).Comments = XMLSpot.GetAttribute("Comments")
                End If
                XMLSpot = XMLSpot.NextSibling
            End While

            mvarFilename = ""
            Loading = False

        End Sub

        Function UpdateLinkedCampaignList() As List(Of Trinity.cLinkedCampaign)
            If _autoLinkCampaigns = AutoLinkCampaignsEnum.DoNotAutoLink Then Return Nothing
            Dim _newFound As New List(Of Trinity.cLinkedCampaign)
            If TrinitySettings.SaveCampaignsAsFiles Then
                Dim _folder As String = Path.GetFullPath(Filename).Substring(0, Path.GetFullPath(Filename).Length - Path.GetFileName(Filename).Length)
                Dim _linksByName As New Dictionary(Of String, Integer)
                Dim _linksByPath As New Dictionary(Of String, Integer)

                Dim i As Integer
                For Each _link As cLinkedCampaign In _linkedCampaigns
                    If Not IO.File.Exists(_link.Path) Then
                        _link.BrokenLink = True
                    Else
                        Dim _linkedCampaign As Trinity.cLinkedCampaign = GetLinkedCampaign(_link.Path)
                        _link.Name = _linkedCampaign.Name
                    End If
                    If Not _linksByName.ContainsKey(_link.Name) Then _linksByName.Add(_link.Name, i)
                    If Not _linksByName.ContainsKey(_link.Path) Then _linksByPath.Add(_link.Path, i)
                    i += 1
                Next

                Dim di As New DirectoryInfo(_folder)
                Dim _files() As FileSystemInfo = di.GetFileSystemInfos("*.cmp")
                For Each _file As FileSystemInfo In _files
                    Dim _linkedCampaign As cLinkedCampaign = GetLinkedCampaign(_file.FullName)
                    If Not _linkedCampaign Is Nothing Then
                        Dim _link As Boolean = False
                        Select Case _autoLinkCampaigns
                            Case AutoLinkCampaignsEnum.LinkToAllCampaigns
                                _link = True
                            Case AutoLinkCampaignsEnum.LinkToSameClient
                                _link = (_linkedCampaign.ClientID = mvarClientID)
                            Case AutoLinkCampaignsEnum.LinkToSameProduct
                                _link = (_linkedCampaign.ProductID = mvarProductID)
                        End Select
                        If _link Then
                            If Not (_linksByName.ContainsKey(_linkedCampaign.Name) OrElse _linksByPath.ContainsKey(_linkedCampaign.Path)) Then
                                LinkedCampaigns.Add(_linkedCampaign)
                                _newFound.Add(_linkedCampaign)
                            End If
                        End If
                    End If
                Next
            Else
                Dim _linksByName As New Dictionary(Of String, Integer)
                Dim i As Integer
                For Each _link As cLinkedCampaign In _linkedCampaigns

                    If _link.DatabaseID = 0 AndAlso Not IO.File.Exists(_link.Path) Then
                        _link.BrokenLink = True
                    ElseIf _link.DatabaseID = 0 Then
                        Dim _linkedCampaign As Trinity.cLinkedCampaign = GetLinkedCampaign(_link.Path)
                        _link.Name = _linkedCampaign.Name
                    Else
                        Dim _linkedCampaign As Trinity.cLinkedCampaign = GetLinkedCampaign(_link.DatabaseID)
                        If _linkedCampaign Is Nothing Then
                            _link.BrokenLink = True
                        Else
                            _link.Name = _linkedCampaign.Name
                        End If
                    End If
                    If Not _linksByName.ContainsKey(_link.Name) Then _linksByName.Add(_link.Name, i)
                    i += 1
                Next
                Dim _sql As String = "SELECT * FROM campaigns WHERE "
                Select Case AutoLinkCampaigns
                    Case AutoLinkCampaignsEnum.LinkToSameClient
                        _sql &= "client=" & ClientID
                    Case AutoLinkCampaignsEnum.LinkToSameProduct
                        _sql &= "product=" & ProductID
                End Select
                Dim _campaigns As List(Of CampaignEssentials) = DBReader.GetCampaigns(_sql)
                For Each _camp As CampaignEssentials In _campaigns
                    Dim _linkedCampaign As cLinkedCampaign = GetLinkedCampaign(_camp.id)
                    If Not _linkedCampaign Is Nothing Then
                        If Not _linksByName.ContainsKey(_linkedCampaign.Name) Then
                            LinkedCampaigns.Add(_linkedCampaign)
                            _newFound.Add(_linkedCampaign)
                        End If
                    End If
                Next
            End If
            Return _newFound
        End Function

        Private Function GetLinkedCampaign(ByVal Path As String) As Trinity.cLinkedCampaign
            If Path = mvarFilename Then
                Return Nothing
            End If
            Dim _xml As New Xml.XmlDocument
            Try
                _xml.Load(Path)
                Dim _linkedCampaign As New Trinity.cLinkedCampaign With {.Link = True, .Name = DirectCast(_xml.GetElementsByTagName("Campaign").Item(0), Xml.XmlElement).GetAttribute("Name"), .ClientID = DirectCast(_xml.GetElementsByTagName("Campaign").Item(0), Xml.XmlElement).GetAttribute("ClientID"), .ProductID = DirectCast(_xml.GetElementsByTagName("Campaign").Item(0), Xml.XmlElement).GetAttribute("ProductID"), .Path = Path}
                Return _linkedCampaign
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Private Function GetLinkedCampaign(ByVal DatabaseID As Long) As Trinity.cLinkedCampaign
            If DatabaseID = Me.DatabaseID Then
                Return Nothing
            End If
            Dim _xml As New Xml.XmlDocument
            Try
                Dim _campXML As String = DBReader.GetCampaign(DatabaseID, True)
                If _campXML = "" Then Return Nothing
                _xml.LoadXml(_campXML)
                Dim _linkedCampaign As New Trinity.cLinkedCampaign With {.Link = True, .Name = DirectCast(_xml.GetElementsByTagName("Campaign").Item(0), Xml.XmlElement).GetAttribute("Name"), .ClientID = DirectCast(_xml.GetElementsByTagName("Campaign").Item(0), Xml.XmlElement).GetAttribute("ClientID"), .ProductID = DirectCast(_xml.GetElementsByTagName("Campaign").Item(0), Xml.XmlElement).GetAttribute("ProductID"), .DatabaseID = DatabaseID}
                Return _linkedCampaign
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Property PluginSaveData As Dictionary(Of String, XElement)

        Public Sub InvalidateMainDaypartSplit()
            For Each _chan As Trinity.cChannel In mvarChannels
                For Each _bt As Trinity.cBookingType In _chan.BookingTypes
                    _bt.InvalidateMainDaypartSplit()
                Next
            Next
        End Sub

        Private Sub cKampanj_ReadNewSpot(ByVal SpotNr As Integer, ByVal SpotCount As Integer) Handles Me.ReadNewSpot

        End Sub

        Private Delegate Sub ProblemDetectionDelegate()
        Private Delegate Sub DetectedProblemsDelegate(ByVal problems As List(Of cProblem))
        Private _problemDetectors As New List(Of ProblemDetectionDelegate)
        Private _problems As List(Of cProblem)
        Private _thread As Threading.Thread

        Public Event FoundProblems()
        Public Event AllProblemsFound(ByVal Problems As List(Of cProblem))

        Public ReadOnly Property Problems() As List(Of cProblem)
            Get
                Return _problems
            End Get
        End Property

        Public Sub RegisterProblemDetection(ByVal Detector As IDetectsProblems)
            If Not mvarErrorCheckingEnabled Then Exit Sub
            If _problemDetectors Is Nothing Then _problemDetectors = New List(Of ProblemDetectionDelegate)
            _problemDetectors.Add(New ProblemDetectionDelegate(AddressOf Detector.DetectProblems))
            AddHandler Detector.ProblemsFound, AddressOf DetectedProblems

        End Sub

        Public Sub UnRegisterProblemDetection(ByVal Detector As IDetectsProblems)
            If _problemDetectors Is Nothing Then _problemDetectors = New List(Of ProblemDetectionDelegate)
            '_problemDetectors.Remove(Detector)
            RemoveHandler Detector.ProblemsFound, AddressOf DetectedProblems
            'Dim _detector As ProblemDetectionDelegate = _problemDetectors.Find(Function(d) d.Method Is Detector.DetectProblems)
            '_problemDetectors.Remove(_detector)
        End Sub

        Public Sub UnRegisterAllProblemDetectors()
            _problemDetectors = Nothing
            'If _thread IsNot Nothing AndAlso _thread.IsAlive Then
            '    _thread.Abort()
            'End If
        End Sub

        Public Enum TimeShiftEnum
            tsDefault = 0
            tsLive = 1
            'tsVOSDAL = 2
            'tsVOSDALplus1 = 3
            'tsVOSDALplus2 = 4
            'tsVOSDALplus3 = 5
            'tsVOSDALplus4 = 6
            'tsVOSDALplus5 = 7
            'tsVOSDALplus6 = 8
            tsVOSDALplus7 = 2
        End Enum

        Private _timeShift As TimeShiftEnum = TimeShiftEnum.tsDefault
        Public Property TimeShift As TimeShiftEnum
            Get
                Return _timeShift
            End Get
            Set(value As TimeShiftEnum)
                _timeShift = value
                RaiseEvent TimeShiftChanged(value)
            End Set
        End Property

        Dim _form As Form
        Public Sub AsyncDetectAllProblems()
            If Not mvarErrorCheckingEnabled OrElse Loading OrElse Saving OrElse Not TrinitySettings.ProblemDetectionEnabled Then Exit Sub
            If _thread Is Nothing OrElse Not _thread.IsAlive Then
                _thread = New Threading.Thread(AddressOf DetectAllProblems)
                _thread.Priority = Threading.ThreadPriority.Lowest
                _thread.Name = "DetectProblems"
                _thread.Start()
            End If
        End Sub

        Public Function DetectAllProblems() As List(Of cProblem)
            If Threading.Thread.CurrentThread IsNot _thread AndAlso _thread IsNot Nothing Then
                _thread.Join()
            End If
            If Loading OrElse Saving Then Return New List(Of cProblem)
            _problems = New List(Of cProblem)
            Dim _detect As [Delegate] = System.MulticastDelegate.Combine(_problemDetectors.ToArray)
            If _detect Is Nothing Then
                _problems = New List(Of cProblem)
                RaiseEvent AllProblemsFound(_problems.ToList)
                Return _problems
            End If
            Try
                _detect.DynamicInvoke()
            Catch

            End Try
            RaiseEvent AllProblemsFound(_problems.ToList)
            Return _problems.ToList
        End Function

        Sub DetectedProblems(ByVal problems As List(Of cProblem))
            _problems.AddRange(problems.Where(Function(p) Not _problems.Contains(p)))
            RaiseEvent FoundProblems()
        End Sub

        Public Enum ProblemsEnum
            NoCampaignName = 1
            NoClient = 2
            NoProduct = 3
            NoBuyer = 4
            NoPlanner = 5
            NotRunning = 6
            NotFinished = 7

        End Enum

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems

            Dim _problems As New List(Of cProblem)

            Dim _helpText As New Text.StringBuilder

            If Name = "" Then
                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Campaign does not have a name</p>")
                _helpText.AppendLine("<p>All campaigns should be given a name</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Setup'-window and enter a campaign name</p>")
                Dim _problem As New cProblem(ProblemsEnum.NoCampaignName, cProblem.ProblemSeverityEnum.Warning, "Campaign does not have a name", "Campaign", _helpText.ToString, Me)

                _problems.Add(_problem)
            End If
            If Client Is Nothing OrElse Client = "" Then
                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Campaign does not have a client</p>")
                _helpText.AppendLine("<p>All campaigns should have a client set</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Setup'-window and select a client in the drop down</p>")
                Dim _problem As New cProblem(ProblemsEnum.NoClient, cProblem.ProblemSeverityEnum.Warning, "Campaign does not have a client", "Campaign", _helpText.ToString, Me)

                _problems.Add(_problem)
            End If
            If Product Is Nothing OrElse Product = "" Then
                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Campaign does not have a product</p>")
                _helpText.AppendLine("<p>All campaigns should have a product set</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Setup'-window and select a product in the drop down</p>")
                Dim _problem As New cProblem(ProblemsEnum.NoProduct, cProblem.ProblemSeverityEnum.Warning, "Campaign does not have a product", "Campaign", _helpText.ToString, Me)

                _problems.Add(_problem)
            End If
            If Buyer = "" Then
                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Campaign does not have a buyer</p>")
                _helpText.AppendLine("<p>All campaigns should have a buyer set</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Setup'-window and select a buyer in the drop down</p>")
                Dim _problem As New cProblem(ProblemsEnum.NoBuyer, cProblem.ProblemSeverityEnum.Warning, "Campaign does not have a buyer", "Campaign", _helpText.ToString, Me)

                _problems.Add(_problem)
            End If
            If Planner = "" Then
                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Campaign does not have a planner</p>")
                _helpText.AppendLine("<p>All campaigns should have a planner set</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Setup'-window and select a planner in the drop down</p>")
                Dim _problem As New cProblem(ProblemsEnum.NoPlanner, cProblem.ProblemSeverityEnum.Warning, "Campaign does not have a planner", "Campaign", _helpText.ToString, Me)

                _problems.Add(_problem)
            End If

            ''Noncontigous campaign period
            'Dim weeksList As List(Of cWeek) = (From tmpWeek As cWeek In Me.Channels(1).BookingTypes(1).Weeks Select tmpWeek Order By tmpWeek.StartDate Ascending).ToList
            'Dim lastEndDate As Long
            'For Each tmpWeek As cWeek In weeksList
            '    lastEndDate = tmpWeek.EndDate
            '    If tmpWeek.StartDate <> lastEndDate + 1 Then
            '        _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Gap in the campaign period</p>")
            '        _helpText.AppendLine("<p>There are gaps in the campaign period. This may be intentional.</p>")
            '        _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
            '        _helpText.AppendLine("<p>If you want the campaign period to be noncontigous, leave it as it is. Otherwise adjust the campaign period in Setup.</p>")
            '        Dim _problem As New cProblem(ProblemsEnum.NoPlanner, cProblem.ProblemSeverityEnum.Warning, "Gap in the campaign period", "Campaign", _helpText.ToString, Me)
            '        _problems.Add(_problem)
            '    End If
            'Next

            'Status setting issues
            If StartDate > 0 And EndDate > 0 Then
                If Date.FromOADate(StartDate) >= Now And Date.FromOADate(EndDate) <= Now And Status <> "Running" Then
                    _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Campaign is running, but status is still 'Planned'</p>")
                    _helpText.AppendLine("<p>If your company has a Seraph license, you will want to set this campaign's status as 'Running' in order to track it on the big screen. Otherwise you may safely ignore this message.</p>")
                    _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                    _helpText.AppendLine("<p>No action required.</p>")
                    Dim _problem As New cProblem(ProblemsEnum.NotRunning, cProblem.ProblemSeverityEnum.Message, "Campaign is running, but status is still 'Planned'", "Campaign", _helpText.ToString, Me)

                    _problems.Add(_problem)
                ElseIf Now >= Date.FromOADate(EndDate) And Status <> "Finished" Then
                    _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Campaign has ended, but status is not 'Finished'</p>")
                    _helpText.AppendLine("<p>If your company has a Seraph license, you will want to set this campaign's status as 'Finished' in order to remove it from tracking on the big screen. Otherwise you may safely ignore this message.</p>")
                    _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                    _helpText.AppendLine("<p>No action required.</p>")
                    Dim _problem As New cProblem(ProblemsEnum.NotFinished, cProblem.ProblemSeverityEnum.Message, "Campaign has ended, but status is not 'Finished'", "Campaign", _helpText.ToString, Me)

                    _problems.Add(_problem)
                ElseIf Date.FromOADate(StartDate) > Now And Status <> "Planned" Then
                    _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Campaign has not started, status should be 'Planned'</p>")
                    _helpText.AppendLine("<p>If your company has a Seraph license, you will want to set this campaign's status as 'Planned' in order to remove it from tracking on the big screen. Otherwise you may safely ignore this message.</p>")
                    _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                    _helpText.AppendLine("<p>No action required.</p>")
                    Dim _problem As New cProblem(ProblemsEnum.NotFinished, cProblem.ProblemSeverityEnum.Message, "Campaign has not started, status should be 'Planned'", "Campaign", _helpText.ToString, Me)

                    _problems.Add(_problem)
                End If
            End If

            If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
            Return _problems
        End Function

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    UnRegisterAllProblemDetectors()
                    mvarActualSpots = Nothing
                    mvarPlannedSpots = Nothing
                    FileClose()
                    mvarInternalAdedge = Nothing
                    mvarTargColl = Nothing
                    mvarUniColl = Nothing
                    mvarBookedSpots = Nothing
                    mvarContract = Nothing
                End If
                If DatabaseID > 0 Then
                    DBReader.unlockCampaign(DatabaseID)
                End If

                If fs IsNot Nothing Then fs.Close()
                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        Protected Overrides Sub Finalize()
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(False)
            MyBase.Finalize()
        End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

        Public Event ProblemsFound(problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound

        Public Function checkIfCampaignHasRescritions(ByVal userName As String, ByVal campaignID As Integer) As List(Of Client)

            userName = Environment.UserName

            Dim cList As New List(Of Client)
            If Campaign IsNot Nothing Then
                If userName = "" Then
                    MessageBox.Show("Please fill in the your nane in the Settings window to be able to open campaign", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    'Check if campgaign client is resctricted
                    'Sets up the table
                    If campaignID <> 0 Then
                        Dim sqlBuilder = "SELECT * from campaigns WHERE " & "id ='" & campaignID & "' AND deletedon < '2001-01-01' "
                        Dim tempCampaign = DBReader.GetCampaigns(sqlBuilder)
                        Dim restricition As Boolean
                        Dim clientList As DataTable = DBReader.getAllClients("", tempCampaign(0).client)
                        For Each dr As DataRow In clientList.Rows
                            Dim tempClient As New Client
                            tempClient.name = dr.Item("name") 'rd!name
                            tempClient.id = dr.Item("id") 'rd!id
                            If Not IsDBNull(dr.Item("restricted")) Then
                                tempClient.restricted = dr.Item("restricted") 'rd!Restricted 
                                cList.Add(tempClient)
                            Else

                            End If
                        Next
                    Else

                    End If
                End If
                Return cList
            End If

        End Function
        Public Class restrictedUsers
            Private _id As Integer
            Private _name As String
            Private _restricted As Boolean
            Public Property restricted() As Boolean
                Get
                    Return _restricted
                End Get
                Set(ByVal value As Boolean)
                    _restricted = value
                End Set
            End Property
            Public Property id() As Integer
                Get
                    Return _id
                End Get
                Set(ByVal value As Integer)
                    _id = value
                End Set
            End Property

            Public Property name() As String
                Get
                    Return _name
                End Get
                Set(ByVal value As String)
                    _name = value
                End Set
            End Property
        End Class

        Public Class Clientrestriction
            Private _id As Integer
            Private _name As String
            Private _restricted As Boolean
            Public listOfRestrictedUsers As New List(Of restrictedUsers)

            Public Property id() As Integer
                Get
                    Return _id
                End Get
                Set(ByVal value As Integer)
                    _id = value
                End Set
            End Property

            Public Property name() As String
                Get
                    Return _name
                End Get
                Set(ByVal value As String)
                    _name = value
                End Set
            End Property
        End Class

        Public Function checkIfUserIsValid(ByVal clientName As String) As Boolean
            Dim loggedIndUser = Environment.UserName
            If System.IO.File.Exists(Trinity.Helper.Pathify(TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork)) & "restrictedClients.xml") Then
                'this code gets all planners and buyers from a XML file
                'note that not all locations h ave a XML file, some still have the old people.lst file
                Dim xmldoc As New Xml.XmlDocument
                xmldoc.Load(Trinity.Helper.Pathify(TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork)) & "restrictedClients.xml")

                Dim listOfClients As New List(Of Clientrestriction)
                Dim clients = xmldoc.GetElementsByTagName("clients").Item(0)
                Dim clientRestrictionFound As Boolean = False
                Dim clientuserFound As Boolean = False

                Dim xmlTmp As Xml.XmlElement
                Dim xmlTmpUser As Xml.XmlElement
                For Each xmlTmp In clients.ChildNodes
                    Dim tempClient As New Clientrestriction
                    tempClient.name = xmlTmp.GetAttribute("clientName").ToString()
                    For Each xmlTmpUser In xmlTmp.ChildNodes
                        Dim res As New restrictedUsers
                        res.name = xmlTmpUser.GetAttribute("name").ToString()
                        Dim resString = xmlTmpUser.GetAttribute("relation").ToString()
                        If resString = "False" Then
                            res.restricted = False
                        Else
                            res.restricted = True
                        End If
                        tempClient.listOfRestrictedUsers.Add(res)
                    Next
                    listOfClients.Add(tempClient)
                Next

                For i As Integer = 0 To listOfClients.Count - 1
                    'Check if 
                    If listOfClients(i).name = clientName Then
                        clientRestrictionFound = True
                        For q As Integer = 0 To listOfClients(i).listOfRestrictedUsers.Count - 1
                            If listOfClients(i).listOfRestrictedUsers(q).name = loggedIndUser Then
                                Return True
                            End If
                        Next
                    End If
                Next
                Return False
                'For Each xmlTmp In clients.ChildNodes
                '    listOfClients.Add(xmlTmp.GetAttribute("clientName").ToString())
                'Next


                'planners = xmldoc.GetElementsByTagName("planners").Item(0)
                'buyers = xmldoc.GetElementsByTagName("buyers").Item(0)

                'Dim xmlTmp As Xml.XmlElement
                'For Each xmlTmp In planners.ChildNodes
                '    cmbPlanner.Items.Add(xmlTmp.GetAttribute("name"))
                'Next


                'For Each xmlTmp In buyers.ChildNodes
                '    cmbBuyer.Items.Add(xmlTmp.GetAttribute("name"))
                'Next

                xmldoc = Nothing
            Else
                'read the planners and buyers from the people.lst file
                Using sr As System.IO.StreamReader = New System.IO.StreamReader(Trinity.Helper.Pathify(TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork)) & "people.lst")
                    Dim line As String
                    Do
                        'line = sr.ReadLine()
                        'If Not line Is Nothing Then
                        '    cmbBuyer.Items.Add(line)
                        '    cmbPlanner.Items.Add(line)
                        'End If
                    Loop Until line Is Nothing
                    sr.Close()
                End Using
            End If


        End Function
    End Class
End Namespace