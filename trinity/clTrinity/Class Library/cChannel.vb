Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic
Imports System.Xml

Namespace Trinity

    Public Class cChannel
        Implements IDetectsProblems

        'a cChannel object is like a real channel, it has a name, area (country) etc.
        'It also have some variables that is needed within the program (like the targeted
        'age group and Marathon, the billing info) 
        Private mvarID As Long
        Private mvarDatabaseID As String = "NOT SAVED"
        Private mvarChannelSet As Integer = 0
        Private mvarChannelName As String
        Private mvarShortname As String
        Private mvarMainTarget As cTarget 'the main tagetes group
        Private mvarSecondaryTarget As cTarget ' the secondary target group
        Private mvarBookingTypes As cBookingTypes
        'the booking type used for the channel
        ' one with booking type 1 and the second channnel with booking type 2)
        Private mvarBuyingUniverse As String
        Private mvarAdEdgeNames As String
        'Public Panel As Integer
        Private mvarDefaultArea As String
        Private mvarIsVisible As Boolean
        Private mvarAgencyCommission As Single
        Private mvarListNumber As Integer
        Private mvarArea As String
        Private mvarMatrixName As String
        Private mvarMarathonName As String
        Private mvarDeliveryAddress As String
        Private mvarVHSAddress As String
        Private mvarPenalty As Single
        Private mvarConnectedChannel As String
        Public LogoPath As String
        Private mvarFile As String
        'Private strID As String
        Private mvarUseBillboards As Boolean = True
        Private mvarUseBreakbumpers As Boolean = True
        Private mvarUseBid As Boolean = False

        Private ParentColl As Collection
        Private Main As cKampanj

        'Public Property DBID() As String
        '    Get
        '        Return strID
        '    End Get
        '    Set(ByVal value As String)
        '        strID = value
        '    End Set
        'End Property

        Public Enum ChannelProblems
            NonexistentChannel = 1
            ImproperConnection = 2
        End Enum

        ' Events and delgate to raise when the TRP might have been changed
        Public Event TRPChanged(ByVal sender As Object, ByVal e As WeekEventArgs)

        Public Sub _trpChanged(ByVal sender As Object, ByVal e As WeekEventArgs)
            RaiseEvent TRPChanged(sender, e)
        End Sub

        Public Event FilmChanged(Film As cFilm)
        Public Event WeekChanged(Week As cWeek)
        Public Event BookingtypeChanged(Bookingtype As cBookingType)
        Public Event ChannelChanged(Channel As cChannel)

        Private Sub _filmChanged(Film As cFilm)
            RaiseEvent FilmChanged(Film)
            RaiseEvent ChannelChanged(Me)
        End Sub

        Private Sub _weekChanged(Week As cWeek)
            RaiseEvent WeekChanged(Week)
            RaiseEvent ChannelChanged(Me)
        End Sub

        Private Sub _bookingtypeChanged(Bookingtype As cBookingType)
            RaiseEvent BookingtypeChanged(Bookingtype)
            RaiseEvent ChannelChanged(Me)
        End Sub

        Public Function GetImage() As Image
            If My.Computer.FileSystem.FileExists(TrinitySettings.ActiveDataPath & Campaign.Area & "\" & LogoPath) Then
                Return Drawing.Image.FromFile(TrinitySettings.ActiveDataPath & Campaign.Area & "\" & LogoPath)
            ElseIf My.Computer.FileSystem.FileExists(IO.Path.Combine(TrinitySettings.ActiveDataPath, "Channel logos/" & ChannelName & ".gif")) Then
                Return Drawing.Image.FromFile(IO.Path.Combine(TrinitySettings.ActiveDataPath, "Channel logos/" & ChannelName & ".gif"))
            Else
                Return Nothing
            End If
        End Function

        Private mvarUserEditable As Boolean = True

        Public Property IsUserEditable As Boolean
            Get
                Return mvarUserEditable
            End Get
            Set(ByVal value As Boolean)
                mvarUserEditable = value
                RaiseEvent ChannelChanged(Me)
            End Set
        End Property

        Private _adTooxChannelID As Long = 0
        Public Property AdTooxChannelID() As Long
            Get
                Return _adTooxChannelID
            End Get
            Set(ByVal value As Long)
                _adTooxChannelID = value
                RaiseEvent ChannelChanged(Me)
            End Set
        End Property

        Private _channelGroup As String
        Public Property ChannelGroup() As String
            Get
                Return _channelGroup
            End Get
            Set(ByVal value As String)
                _channelGroup = value
            End Set
        End Property


        Public Property fileName() As String
            Get
                Return mvarFile
            End Get
            Set(ByVal value As String)
                mvarFile = value
                RaiseEvent ChannelChanged(Me)
            End Set
        End Property

        Public Property channelSet() As Integer
            Get
                Return mvarChannelSet
            End Get
            Set(ByVal value As Integer)
                mvarChannelSet = value
                RaiseEvent ChannelChanged(Me)
            End Set
        End Property

        Public Property UseBid() As Boolean
            Get
                Return mvarUseBid
            End Get
            Set(ByVal value As Boolean)
                mvarUseBid = value
                RaiseEvent ChannelChanged(Me)
            End Set
        End Property

        Public Function GetXML(ByRef colXml As XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            Dim XMLTarget As XmlElement

            'get the bookingtypes
            XMLTarget = xmlDoc.CreateElement("BookingTypes")
            Me.BookingTypes.GetXML(XMLTarget, errorMessege, xmlDoc)
            colXml.AppendChild(XMLTarget)

            'get the channel attributes
            colXml.SetAttribute("Name", Me.ChannelName) ' as String
            colXml.SetAttribute("ID", Me.ID) ' as Long
            colXml.SetAttribute("ShortName", Me.Shortname) ' as String
            colXml.SetAttribute("BuyingUniverse", Me.BuyingUniverse) ' as String
            colXml.SetAttribute("AdEdgeNames", Me.AdEdgeNames) ' as String
            colXml.SetAttribute("DefaultArea", Me.DefaultArea) ' as String
            colXml.SetAttribute("AgencyCommission", Me.AgencyCommission) ' as Single
            colXml.SetAttribute("Marathon", Me.MarathonName)
            colXml.SetAttribute("DeliveryAddress", Me.DeliveryAddress)
            colXml.SetAttribute("VHSAddress", Me.VHSAddress)
            colXml.SetAttribute("ConnectedChannel", Me.ConnectedChannel)
            colXml.SetAttribute("fileName", Me.fileName)

            'Save the targets
            XMLTarget = xmlDoc.CreateElement("MainTarget")
            XMLTarget.SetAttribute("Name", Me.MainTarget.TargetName) ' as New cTarget
            XMLTarget.SetAttribute("Universe", Me.MainTarget.Universe) ' as New cTarget
            XMLTarget.SetAttribute("SecondUniverse", Me.MainTarget.SecondUniverse)
            XMLTarget.SetAttribute("TargetType", Me.MainTarget.TargetType)
            XMLTarget.SetAttribute("TargetGroup", Me.MainTarget.TargetGroup)

            colXml.AppendChild(XMLTarget)

            XMLTarget = xmlDoc.CreateElement("SecondaryTarget")
            XMLTarget.SetAttribute("Name", Me.SecondaryTarget.TargetName) ' as New cTarget
            XMLTarget.SetAttribute("Type", Me.SecondaryTarget.TargetType) ' as New cTarget
            XMLTarget.SetAttribute("Universe", Me.SecondaryTarget.Universe) ' as New cTarget
            XMLTarget.SetAttribute("SecondUniverse", Me.SecondaryTarget.SecondUniverse)
            XMLTarget.SetAttribute("TargetType", Me.SecondaryTarget.TargetType)
            XMLTarget.SetAttribute("TargetGroup", Me.SecondaryTarget.TargetGroup)
            colXml.AppendChild(XMLTarget)

            Exit Function

On_Error:
            colXml.AppendChild(xmlDoc.CreateComment("ERROR (" & Err.Number & "): " & Err.Description))
            errorMessege.Add("Error saving Channel " & Me.ChannelName)
            Resume Next
        End Function

        Friend WriteOnly Property MainObject() As cKampanj
            Set(ByVal value As cKampanj)
                Main = value
                mvarMainTarget.MainObject = value
                mvarSecondaryTarget.MainObject = value
                mvarBookingTypes.MainObject = value
                RaiseEvent ChannelChanged(Me)
            End Set

        End Property

        Public Overrides Function ToString() As String
            Return mvarChannelName
        End Function

        Public Property UseBillboards() As Boolean
            Get
                Return mvarUseBillboards
            End Get
            Set(ByVal value As Boolean)
                mvarUseBillboards = value
                RaiseEvent ChannelChanged(Me)
            End Set
        End Property

        Public Property UseBreakBumpers() As Boolean
            Get
                Return mvarUseBreakbumpers
            End Get
            Set(ByVal value As Boolean)
                mvarUseBreakbumpers = value
                RaiseEvent ChannelChanged(Me)
            End Set
        End Property

        Private WriteOnly Property ParentCollection() As Collection
            '---------------------------------------------------------------------------------------
            ' Procedure : ParentCollection
            ' DateTime  : 2003-07-07 13:18
            ' Author    : joho
            ' Purpose   : Sets the Collection of wich this channel is a member. This is used
            '             when a new ChannelName is set. See that property for further explanation
            '---------------------------------------------------------------------------------------
            '
            Set(ByVal value As Collection)
                ParentColl = value
                RaiseEvent ChannelChanged(Me)
            End Set

        End Property

        Public Property ID() As Long
            '---------------------------------------------------------------------------------------
            ' Procedure : ID
            ' DateTime  : 2003-07-07 13:06
            ' Author    : joho
            ' Purpose   :
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo ID_Error
                ID = mvarID
                On Error GoTo 0
                Exit Property
ID_Error:
                Err.Raise(Err.Number, "cChannel: ID", Err.Description)
            End Get
            Set(ByVal value As Long)
                On Error GoTo ID_Error
                mvarID = value
                RaiseEvent ChannelChanged(Me)
                On Error GoTo 0
                Exit Property
ID_Error:
                Err.Raise(Err.Number, "cChannel: ID", Err.Description)
            End Set

        End Property

        Public Property databaseID() As String
            Get
                On Error GoTo ID_Error
                databaseID = mvarDatabaseID
                On Error GoTo 0
                Exit Property
ID_Error:
                Err.Raise(Err.Number, "cChannel: databaseID", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo ID_Error
                mvarDatabaseID = value
                RaiseEvent ChannelChanged(Me)
                On Error GoTo 0
                Exit Property
ID_Error:
                Err.Raise(Err.Number, "cChannel: databaseID", Err.Description)
            End Set

        End Property

        Public Property ChannelName() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : ChannelName
            ' DateTime  : 2003-07-07 13:06
            ' Author    : joho
            ' Purpose   : Returns/sets the channel name. If a new channel name is set, the channel
            '             has to be removed from the collection and re-added with the new name
            '             as key.
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo ChannelName_Error

                ChannelName = mvarChannelName

                On Error GoTo 0
                Exit Property

ChannelName_Error:

                Err.Raise(Err.Number, "cChannel: ChannelName", Err.Description)

            End Get
            Set(ByVal value As String)
                Dim TmpChannel As cChannel

                On Error GoTo ChannelName_Error

                If value <> mvarChannelName And mvarChannelName <> "" Then

                    TmpChannel = ParentColl(mvarChannelName)
                    ParentColl.Add(TmpChannel, value)
                    ParentColl.Remove(mvarChannelName)

                End If
                mvarChannelName = value

                RaiseEvent ChannelChanged(Me)

                On Error GoTo 0
                Exit Property

ChannelName_Error:

                Err.Raise(Err.Number, "cChannel: ChannelName", Err.Description)

            End Set
        End Property

        Public Property Shortname() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : Shortname
            ' DateTime  : 2003-07-07 13:06
            ' Author    : joho
            ' Purpose   : Returns/sets the abbrevation for the channel
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Shortname_Error

                Shortname = mvarShortname

                On Error GoTo 0
                Exit Property

Shortname_Error:

                Err.Raise(Err.Number, "cChannel: Shortname", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo Shortname_Error

                mvarShortname = value
                RaiseEvent ChannelChanged(Me)

                On Error GoTo 0
                Exit Property

Shortname_Error:

                Err.Raise(Err.Number, "cChannel: Shortname", Err.Description)

            End Set
        End Property

        Public Property MainTarget() As cTarget
            '---------------------------------------------------------------------------------------
            ' Procedure : MainTarget
            ' DateTime  : 2003-07-07 13:06
            ' Author    : joho
            ' Purpose   : Returns/sets the main target for this channel
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo MainTarget_Error

                MainTarget = mvarMainTarget

                On Error GoTo 0
                Exit Property

MainTarget_Error:

                Err.Raise(Err.Number, "cChannel: MainTarget", Err.Description)

            End Get
            Set(ByVal value As cTarget)
                On Error GoTo MainTarget_Error

                mvarMainTarget = value
                RaiseEvent ChannelChanged(Me)

                On Error GoTo 0
                Exit Property

MainTarget_Error:

                Err.Raise(Err.Number, "cChannel: MainTarget", Err.Description)

            End Set
        End Property

        Public Property SecondaryTarget() As cTarget
            '---------------------------------------------------------------------------------------
            ' Procedure : SecondaryTarget
            ' DateTime  : 2003-07-08 14:43
            ' Author    : joho
            ' Purpose   : Returns/sets the Secondary Target for this channel
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo SecondaryTarget_Error

                SecondaryTarget = mvarSecondaryTarget

                On Error GoTo 0
                Exit Property

SecondaryTarget_Error:

                Err.Raise(Err.Number, "cChannel: SecondaryTarget", Err.Description)
            End Get
            Set(ByVal value As cTarget)
                On Error GoTo SecondaryTarget_Error

                mvarSecondaryTarget = value
                RaiseEvent ChannelChanged(Me)

                On Error GoTo 0
                Exit Property

SecondaryTarget_Error:

                Err.Raise(Err.Number, "cChannel: SecondaryTarget", Err.Description)

            End Set
        End Property

        Public Property BuyingUniverse() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : BuyingUniverse
            ' DateTime  : 2003-07-08 14:43
            ' Author    : joho
            ' Purpose   : The Universe used for buying in the channel
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo BuyingUniverse_Error

                BuyingUniverse = mvarBuyingUniverse

                On Error GoTo 0
                Exit Property

BuyingUniverse_Error:

                Err.Raise(Err.Number, "cChannel: BuyingUniverse", Err.Description)

            End Get
            Set(ByVal value As String)
                Dim TmpBookingType As cBookingType

                On Error GoTo BuyingUniverse_Error

                mvarBuyingUniverse = value

                For Each TmpBookingType In mvarBookingTypes
                    TmpBookingType.Pricelist.BuyingUniverse = mvarBuyingUniverse
                Next
                RaiseEvent ChannelChanged(Me)

                On Error GoTo 0
                Exit Property

BuyingUniverse_Error:

                Err.Raise(Err.Number, "cChannel: BuyingUniverse", Err.Description)

            End Set
        End Property

        Public Function saveChannelToDatabase(ByVal saveBookingTypes As Boolean)
            Dim returnValue As Boolean = True

            'Save only the Channel information
            If Not saveChannelInfoToDatabase() Then
                returnValue = False
            End If

            'includes bookingtypes but EXCLUDES pricelists
            If saveBookingTypes Then
                For Each bt As cBookingType In Me.BookingTypes
                    If Not bt.saveBookingTypeToDatabase() Then 'Save bookingtypes rekursive
                        returnValue = False
                    End If
                Next
            End If

            Return returnValue
        End Function

        Public Function updateChannelFromDatabase(ByVal updateBookingTypes As Boolean) As Boolean
            Dim returnValue As Boolean = True

            'includes bookingtypes but EXCLUDES pricelists
            If updateBookingTypes Then
                For Each bt As cBookingType In Me.BookingTypes
                    bt.updateBookingTypeInfoFromDatabase()
                Next
            End If

            'Updates only the Channel information
            If Not updateChannelInfoFromDatabase() Then
                returnValue = False
            End If

            Return returnValue
        End Function

        Private Function updateChannelInfoFromDatabase()
            Return DBReader.updateChannelInfo(Me)
        End Function

        Private Function saveChannelInfoToDatabase()
            Dim returnValue As Boolean = False

            returnValue = DBReader.saveChannelInfo(Me)

            Return returnValue
        End Function

        Public Sub readDefaultBookingTypes()
            'read bookingtypes

            '(TrinitySettings.ConnectionStringCommon <> "")
            If (TrinitySettings.ConnectionStringCommon <> "") Then
                DBReader.readBookingTypes(Me)
            Else
                Dim Ini As New clsIni
                Dim XMLDoc As New Xml.XmlDocument
                Dim XMLChannels As Xml.XmlElement
                Dim XMLTmpNode As Xml.XmlElement
                Dim XMLBookingTypes As Xml.XmlElement
                Dim XMLtmpnode2 As Xml.XmlElement
                Dim tmpBT As Trinity.cBookingType

                If Area = "" Then
                    Area = Main.Area
                End If

                If Me.fileName = "" Then
                    XMLDoc.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Area & "\Channels.xml")
                Else
                    XMLDoc.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Area & "\" & Me.fileName)
                End If

                XMLChannels = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("Channels")

                XMLTmpNode = XMLChannels.SelectSingleNode("Channel[@Name='" & mvarChannelName & "']")

                If XMLTmpNode IsNot Nothing Then
                    XMLBookingTypes = XMLTmpNode.SelectSingleNode("Bookingtypes")
                    If XMLBookingTypes Is Nothing Then
                        XMLBookingTypes = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("Bookingtypes")
                    End If
                    XMLtmpnode2 = XMLBookingTypes.ChildNodes.Item(0)
                    While Not XMLtmpnode2 Is Nothing
                        tmpBT = Me.BookingTypes.Add(XMLtmpnode2.GetAttribute("Name"), False)
                        tmpBT.Shortname = XMLtmpnode2.GetAttribute("Shortname")
                        tmpBT.IsRBS = XMLtmpnode2.GetAttribute("IsRBS")
                        tmpBT.IsSpecific = XMLtmpnode2.GetAttribute("IsSpecific")
                        tmpBT.PricelistName = XMLtmpnode2.GetAttribute("Pricelist")
                        If Not XMLtmpnode2.GetAttribute("IsPremium") = "" Then
                            tmpBT.IsPremium = XMLtmpnode2.GetAttribute("IsPremium")
                        End If
                        If Not XMLtmpnode2.GetAttribute("SpecificsFactor") = "" Then
                            tmpBT.EnhancementFactor = XMLtmpnode2.GetAttribute("SpecificsFactor")
                        End If

                        Try
                            tmpBT.PrintDayparts = XMLtmpnode2.GetAttribute("PrintDayparts")
                            tmpBT.PrintBookingCode = XMLtmpnode2.GetAttribute("PrintBookingCode")
                        Catch
                            tmpBT.PrintDayparts = False
                            tmpBT.PrintBookingCode = False
                        End Try
                        XMLtmpnode2 = XMLtmpnode2.NextSibling
                    End While
                End If
            End If
            'done reading bookingtypes
        End Sub

        Public ReadOnly Property BookingTypes() As cBookingTypes
            '---------------------------------------------------------------------------------------
            ' Procedure : BookingTypes
            ' DateTime  : 2003-07-08 14:44
            ' Author    : joho
            ' Purpose   : Returns/sets the pointer to the cBookingTypes object holding each
            '             added BookingType
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo BookingTypes_Error

                If mvarBookingTypes.MainObject Is Nothing And Not Main Is Nothing Then
                    mvarBookingTypes.MainObject = Main
                End If
                If Not mvarBookingTypes.ParentChannel Is Me Then
                    mvarBookingTypes.ParentChannel = Me
                End If
                BookingTypes = mvarBookingTypes

                On Error GoTo 0
                Exit Property

BookingTypes_Error:

                Err.Raise(Err.Number, "cChannel: BookingTypes", Err.Description)
            End Get
            '            Set(ByVal value As cBookingTypes)
            '                On Error GoTo BookingTypes_Error

            '                mvarBookingTypes = value
            '                mvarBookingTypes.MainObject = Main
            '                RaiseEvent ChannelChanged(Me)

            '                On Error GoTo 0
            '                Exit Property

            'BookingTypes_Error:

            '                Err.Raise(Err.Number, "cChannel: BookingTypes", Err.Description)


            '            End Set
        End Property

        Public Property AdEdgeNames() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : AdEdgeNames
            ' DateTime  : 2003-07-08 14:45
            ' Author    : joho
            ' Purpose   : Returns/sets a comma separated string with the names used to reference
            '             this channel in AdEdge. Most commonly there will only be one name,
            '             but occasionally a second name might be used (Discovery se, Disc Mix se)
            '---------------------------------------------------------------------------------------
            Get
                On Error GoTo AdEdgeNames_Error

                AdEdgeNames = mvarAdEdgeNames

                On Error GoTo 0
                Exit Property

AdEdgeNames_Error:

                Err.Raise(Err.Number, "cChannel: AdEdgeNames", Err.Description)

            End Get
            Set(ByVal value As String)
                On Error GoTo AdEdgeNames_Error

                mvarAdEdgeNames = value
                RaiseEvent ChannelChanged(Me)

                On Error GoTo 0
                Exit Property

AdEdgeNames_Error:

                Err.Raise(Err.Number, "cChannel: AdEdgeNames", Err.Description)

            End Set
            '
        End Property

        Public Property DefaultArea() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : DefaultArea
            ' DateTime  : 2003-07-09 11:35
            ' Author    : joho
            ' Purpose   : Returns/sets the default Area for this channel. Might not be implemented
            '---------------------------------------------------------------------------------------
            Get
                On Error GoTo DefaultArea_Error

                DefaultArea = mvarDefaultArea

                On Error GoTo 0
                Exit Property

DefaultArea_Error:

                Err.Raise(Err.Number, "cChannel: DefaultArea", Err.Description)

            End Get
            Set(ByVal value As String)
                On Error GoTo DefaultArea_Error

                mvarDefaultArea = value
                RaiseEvent ChannelChanged(Me)

                On Error GoTo 0
                Exit Property

DefaultArea_Error:

                Err.Raise(Err.Number, "cChannel: DefaultArea", Err.Description)

            End Set
        End Property

        Public Property IsVisible() As Boolean
            '---------------------------------------------------------------------------------------
            ' Procedure : IsVisible
            ' DateTime  : 2003-07-09 11:35
            ' Author    : joho
            ' Purpose   : Returns/sets wether this channel should be shown in the charts or not. No longer used.
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IsVisible_Error

                IsVisible = mvarIsVisible

                On Error GoTo 0
                Exit Property

IsVisible_Error:

                Err.Raise(Err.Number, "cChannel: IsVisible", Err.Description)

            End Get
            Set(ByVal value As Boolean)
                On Error GoTo IsVisible_Error

                mvarIsVisible = value
                RaiseEvent ChannelChanged(Me)

                On Error GoTo 0
                Exit Property

IsVisible_Error:

                Err.Raise(Err.Number, "cChannel: IsVisible", Err.Description)

            End Set
        End Property

        Public Property AgencyCommission() As Single
            '---------------------------------------------------------------------------------------
            ' Procedure : AgencyCommission
            ' DateTime  : 2003-07-09 11:35
            ' Author    : joho
            ' Purpose   : Returns/sets the Agency comission used for this channel
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo AgencyCommission_Error

                AgencyCommission = mvarAgencyCommission

                On Error GoTo 0
                Exit Property

AgencyCommission_Error:

                Err.Raise(Err.Number, "cChannel: AgencyCommission", Err.Description)

            End Get
            Set(ByVal value As Single)
                On Error GoTo AgencyCommission_Error

                mvarAgencyCommission = value
                RaiseEvent ChannelChanged(Me)

                On Error GoTo 0
                Exit Property

AgencyCommission_Error:

                Err.Raise(Err.Number, "cChannel: AgencyCommission", Err.Description)

            End Set
        End Property

        Public Function ReadDefaultProperties(Optional ByVal Area As String = "") As Boolean
            '---------------------------------------------------------------------------------------
            ' Procedure : ReadDefaultProperties
            ' DateTime  : 2003-07-07 12:46
            ' Author    : joho
            ' Purpose   : Reads in general settings such as Buying Targets, Universe sizes,
            '             Names in AdvantEdge etc. from the XML and ini files
            '             Returns True if everything goes well, otherwise false
            '---------------------------------------------------------------------------------------
            '

            On Error GoTo ReadDefaultProperties_Error

            Dim Ini As New clsIni

            If Area = "" AndAlso Not Main.Area Is Nothing Then
                Area = Main.Area
            End If

            '(TrinitySettings.ConnectionStringCommon <> "")
            If (TrinitySettings.ConnectionStringCommon <> "") Then
                If Not DBReader.updateChannelInfo(Me) Then
                    MsgBox("FAILED TO UPDATE CHANNEL", MsgBoxStyle.Critical)
                End If
            Else
                Dim XMLDoc As New Xml.XmlDocument
                Dim XMLChannels As Xml.XmlElement
                Dim XMLTmpNode As Xml.XmlElement

                If Me.fileName = "" Then
                    XMLDoc.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Area & "\Channels.xml")
                Else
                    XMLDoc.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Area & "\" & Me.fileName)
                End If

                XMLChannels = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("Channels")

                XMLTmpNode = XMLChannels.SelectSingleNode("Channel[@Name='" & mvarChannelName & "']")

                If Not XMLTmpNode Is Nothing Then
                    mvarShortname = XMLTmpNode.GetAttribute("Shortname")
                    mvarBuyingUniverse = XMLTmpNode.GetAttribute("BuyingUniverse")
                    mvarAdEdgeNames = XMLTmpNode.GetAttribute("AdEdgeNames")
                    mvarMatrixName = XMLTmpNode.GetAttribute("MatrixName")
                    mvarMarathonName = XMLTmpNode.GetAttribute("Marathon")
                    mvarConnectedChannel = XMLTmpNode.GetAttribute("ConnectedChannel")
                    mvarPenalty = Val(XMLTmpNode.GetAttribute("Penalty")) / 100
                    mvarAgencyCommission = XMLTmpNode.GetAttribute("AgencyCommission")
                    mvarListNumber = XMLTmpNode.GetAttribute("ListNumber")
                    If Not XMLTmpNode.GetAttribute("AdtooxID") = "" Then
                        _adTooxChannelID = XMLTmpNode.GetAttribute("AdtooxID")
                    End If
                    If Not XMLTmpNode.GetAttribute("UseBid") = "" Then
                        mvarUseBid = XMLTmpNode.GetAttribute("UseBid")
                    End If
                    _channelGroup = XMLTmpNode.GetAttribute("ChannelGroup")
                    LogoPath = XMLTmpNode.GetAttribute("Logo")
                    If mvarListNumber = 0 Then
                        mvarListNumber = ParentColl(ParentColl.Count).ListNumber + 1
                    End If
                End If

                mvarDefaultArea = Area


                If My.Computer.FileSystem.FileExists(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Area & "\Channel info\" & mvarChannelName & ".xml") Then
                    XMLDoc.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Area & "\Channel info\" & mvarChannelName & ".xml")
                    XMLTmpNode = XMLDoc.GetElementsByTagName("Address").Item(0)
                    mvarDeliveryAddress = XMLTmpNode.InnerText
                    XMLTmpNode = XMLDoc.GetElementsByTagName("VHSAddress").Item(0)
                    If Not XMLTmpNode Is Nothing Then
                        mvarVHSAddress = XMLTmpNode.InnerText
                    End If
                Else
                    mvarDeliveryAddress = ""
                    mvarVHSAddress = ""
                End If
            End If
            RaiseEvent ChannelChanged(Me)
            Return True


ReadDefaultProperties_Error:
            Return False


            '            Err.Raise(Err.Number, "cChannel: ReadDefaultProperties", Err.Description)

        End Function

        Public ReadOnly Property HasSpecifics() As Boolean
            Get
                Dim tmp As String = Me.ChannelName
                Dim result As System.Array = (From bts In Me.BookingTypes Select bts Where bts.isspecific = True).ToArray
                If result.Length > 0 Then
                    Return True
                Else
                    Return False
                End If

            End Get
        End Property

        Public Function PlannedBudget() As Decimal
            '---------------------------------------------------------------------------------------
            ' Procedure : PlannedBudget
            ' DateTime  : 2003-07-15 12:54
            ' Author    : joho
            ' Purpose   : Returns the planned budget for this channel calculated based on
            '             estimated ratings and booked CPPs if no Price is set for the spot.
            '---------------------------------------------------------------------------------------
            '

            Dim TmpPlannedSpot As cPlannedSpot

            On Error GoTo PlannedBudget_Error

            PlannedBudget = 0
            For Each TmpPlannedSpot In Main.PlannedSpots
                If TmpPlannedSpot.Channel Is Me Then
                    If TmpPlannedSpot.PriceNet > 0 Then
                        PlannedBudget = PlannedBudget + TmpPlannedSpot.PriceNet
                    Else
                        PlannedBudget = PlannedBudget + TmpPlannedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) * TmpPlannedSpot.Week.NetCPP
                    End If
                End If
            Next

            On Error GoTo 0
            Exit Function

PlannedBudget_Error:

            Err.Raise(Err.Number, "cChannel: PlannedBudget", Err.Description)

        End Function

        Public Property ListNumber() As Integer
            '---------------------------------------------------------------------------------------
            ' Procedure : ListNumber
            ' DateTime  : 2003-07-25 19:44
            ' Author    : joho
            ' Purpose   : Returns/sets a number indicating in wich order channels should be
            '             sorted. The channel with the lowest channel number should be higher
            '             in the list. See cChannels.Add for more details.
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo ListNumber_Error

                ListNumber = mvarListNumber

                On Error GoTo 0
                Exit Property

ListNumber_Error:

                Err.Raise(Err.Number, "cChannel: ListNumber", Err.Description)

            End Get
            Set(ByVal value As Integer)
                On Error GoTo ListNumber_Error

                mvarListNumber = value
                RaiseEvent ChannelChanged(Me)

                On Error GoTo 0
                Exit Property

ListNumber_Error:

                Err.Raise(Err.Number, "cChannel: ListNumber", Err.Description)

            End Set
        End Property

        Public Property Area() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : Area
            ' DateTime  : 2003-08-31 22:43
            ' Author    : joho
            ' Purpose   : Returns/sets the Area (Country) for this channel
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Area_Error

                Area = mvarArea

                On Error GoTo 0
                Exit Property

Area_Error:

                Err.Raise(Err.Number, "cChannel: Area", Err.Description)

            End Get
            Set(ByVal value As String)
                On Error GoTo Area_Error

                mvarArea = value
                RaiseEvent ChannelChanged(Me)

                On Error GoTo 0
                Exit Property

Area_Error:

                Err.Raise(Err.Number, "cChannel: Area", Err.Description)

            End Set
        End Property

        Public Property MatrixName() As String
            Get
                On Error GoTo MatrixName_Error
                Helper.WriteToLogFile("MatrixName")

                MatrixName = mvarMatrixName

                On Error GoTo 0
                Exit Property

MatrixName_Error:

                Helper.WriteToLogFile("ERROR in cChannel.MatrixName: " & Err.Description)
                Err.Raise(Err.Number, "cChannel: MatrixName", Err.Description)
                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"
            End Get
            Set(ByVal value As String)
                On Error GoTo MatrixName_Error
                Helper.WriteToLogFile("MatrixName")

                mvarMatrixName = value
                RaiseEvent ChannelChanged(Me)

                On Error GoTo 0
                Exit Property

MatrixName_Error:

                Helper.WriteToLogFile("ERROR in cChannel.MatrixName: " & Err.Description)
                Err.Raise(Err.Number, "cChannel: MatrixName", Err.Description)
                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"
            End Set
        End Property

        Public Property MarathonName() As String
            Get
                On Error GoTo MarathonName_Error
                Helper.WriteToLogFile("MarathonName")

                MarathonName = mvarMarathonName

                On Error GoTo 0
                Exit Property

MarathonName_Error:

                Helper.WriteToLogFile("ERROR in cChannel.MarathonName: " & Err.Description)
                Err.Raise(Err.Number, "cChannel: MarathonName", Err.Description)
                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"
            End Get
            Set(ByVal value As String)
                On Error GoTo MarathonName_Error
                Helper.WriteToLogFile("MarathonName")

                mvarMarathonName = value
                RaiseEvent ChannelChanged(Me)

                On Error GoTo 0
                Exit Property

MarathonName_Error:

                Helper.WriteToLogFile("ERROR in cChannel.MarathonName: " & Err.Description)
                Err.Raise(Err.Number, "cChannel: MarathonName", Err.Description)
                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"

            End Set
        End Property

        Public Property DeliveryAddress() As String
            'delivery adress is where the channel is located (used for sending contracts etc)
            Get
                On Error GoTo DeliveryAddress_Error
                Helper.WriteToLogFile("DeliveryAddress")

                DeliveryAddress = mvarDeliveryAddress

                On Error GoTo 0
                Exit Property

DeliveryAddress_Error:

                Helper.WriteToLogFile("ERROR in cChannel.DeliveryAddress: " & Err.Description)
                Err.Raise(Err.Number, "cChannel: DeliveryAddress", Err.Description)
                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"
            End Get
            Set(ByVal value As String)
                On Error GoTo DeliveryAddress_Error
                Helper.WriteToLogFile("DeliveryAddress")

                mvarDeliveryAddress = value
                RaiseEvent ChannelChanged(Me)

                On Error GoTo 0
                Exit Property

DeliveryAddress_Error:

                Helper.WriteToLogFile("ERROR in cChannel.DeliveryAddress: " & Err.Description)
                Err.Raise(Err.Number, "cChannel: DeliveryAddress", Err.Description)
                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"


            End Set
        End Property

        Public Property VHSAddress() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : VHSAddress
            ' DateTime  : -
            ' Author    : joho
            ' Purpose   : Sets the adress for where the Comercial films whould be sent
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo VHSAddress_Error
                Helper.WriteToLogFile("VHSAddress")

                VHSAddress = mvarVHSAddress

                On Error GoTo 0
                Exit Property

VHSAddress_Error:

                Helper.WriteToLogFile("ERROR in cChannel.VHSAddress: " & Err.Description)
                Err.Raise(Err.Number, "cChannel: VHSAddress", Err.Description)
                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"
            End Get
            Set(ByVal value As String)
                On Error GoTo VHSAddress_Error
                Helper.WriteToLogFile("VHSAddress")

                mvarVHSAddress = value
                RaiseEvent ChannelChanged(Me)

                On Error GoTo 0
                Exit Property

VHSAddress_Error:

                Helper.WriteToLogFile("ERROR in cChannel.VHSAddress: " & Err.Description)
                Err.Raise(Err.Number, "cChannel: VHSAddress", Err.Description)
                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"


            End Set
        End Property

        Public Property Penalty() As Single
            Get
                On Error GoTo Penalty_Error
                Helper.WriteToLogFile("Penalty")

                Penalty = mvarPenalty

                On Error GoTo 0
                Exit Property

Penalty_Error:

                Helper.WriteToLogFile("ERROR in cChannel.Penalty: " & Err.Description)
                Err.Raise(Err.Number, "cChannel: Penalty", Err.Description)
                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"
            End Get
            Set(ByVal value As Single)
                On Error GoTo Penalty_Error
                Helper.WriteToLogFile("Penalty")

                mvarPenalty = value
                RaiseEvent ChannelChanged(Me)

                On Error GoTo 0
                Exit Property

Penalty_Error:

                Helper.WriteToLogFile("ERROR in cChannel.Penalty: " & Err.Description)
                Err.Raise(Err.Number, "cChannel: Penalty", Err.Description)
                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"

            End Set
        End Property

        Public Property ConnectedChannel() As String
            Get
                ConnectedChannel = mvarConnectedChannel
            End Get
            Set(ByVal value As String)
                mvarConnectedChannel = value
                RaiseEvent ChannelChanged(Me)
            End Set
        End Property

        Public Function TotalTRP() As Single
            Dim TmpBT As cBookingType
            Dim TRP As Single

            For Each TmpBT In mvarBookingTypes
                TRP = TRP + TmpBT.TotalTRP
            Next
            TotalTRP = TRP
        End Function

        Public Sub New(ByVal Main As cKampanj, ParentCollection As Collection)
            'initialize new variables for a new booking
            mvarBookingTypes = New cBookingTypes(Main)
            mvarMainTarget = New cTarget(Main)
            mvarSecondaryTarget = New cTarget(Main)
            Main.RegisterProblemDetection(Me)

            AddHandler mvarBookingTypes.TRPChanged, AddressOf _trpChanged
            AddHandler mvarBookingTypes.BookingtypeChanged, AddressOf _bookingtypeChanged
            AddHandler mvarBookingTypes.WeekChanged, AddressOf _weekChanged
            AddHandler mvarBookingTypes.FilmChanged, AddressOf _filmChanged
            ParentColl = ParentCollection
        End Sub

        Protected Overrides Sub Finalize()
            mvarBookingTypes = Nothing
            MyBase.Finalize()
        End Sub

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

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems

            If (From BT As cBookingType In Me.BookingTypes Select BT Where BT.BookIt).Count = 0 Then
                Return New List(Of cProblem)
            End If

            Dim _problems As New List(Of cProblem)

            'Check connected channels

            Dim A As String = Me.ChannelName
            Dim B As String = Me.ConnectedChannel
            If B <> "" Then
                If Main.Channels(B) IsNot Nothing AndAlso A <> Main.Channels(B).ConnectedChannel Then
                    Dim _helpText As New Text.StringBuilder

                    _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Improper connection</p>")
                    _helpText.AppendLine("<p>" & Me.ChannelName & " is connected to " & Me.ConnectedChannel & " but " & Me.ConnectedChannel & " is not connected to " & Me.ChannelName & "</p>")
                    _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                    _helpText.AppendLine("<p>If you <i>want</i> these channels to be bundled, open the 'Define Channels'-window, select '" & Me.ChannelName & "' in the topmost dropdown and review connected channel. Then select '" & Me.ConnectedChannel & "' and make sure it is connected to " & Me.ChannelName & "</p>")

                    Dim _problem As New cProblem(ChannelProblems.ImproperConnection, cProblem.ProblemSeverityEnum.Warning, "Improper channel connection", Me.ChannelName, _helpText.ToString, Me)
                    _problems.Add(_problem)

                End If
            End If

            If Not Me.ConnectedChannel Is Nothing AndAlso Not Me.ConnectedChannel = "" AndAlso Not Main.Channels.Contains(Me.ConnectedChannel) Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Connected channel does not exist</p>")
                _helpText.AppendLine("<p>" & Me.ChannelName & " is connected to a channel which does not exist in the campaign - " & Me.ConnectedChannel & ". It may just be a spelling error.</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Define Channels'-window, select '" & Me.ChannelName & "' in the topmost dropdown and review connected channel. </p>")

                Dim _problem As New cProblem(ChannelProblems.NonexistentChannel, cProblem.ProblemSeverityEnum.Warning, "Connection to nonexistent channel", Me.ChannelName, _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
            Return _problems

        End Function

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound

    End Class
End Namespace