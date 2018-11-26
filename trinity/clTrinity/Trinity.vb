'Imports System.Runtime.InteropServices
'Imports System
'Imports System.Text
'Imports System.Collections
'Imports Microsoft.VisualBasic

'Namespace Trinity

    '    Public Class cActualSpot
    '        '---------------------------------------------------------------------------------------
    '        ' Class : cActualSpot
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   : 
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Enum EnumBreakType
    '            btBlock = 0
    '            btBreak = 1
    '        End Enum

    '        Private mvarChannel As cChannel
    '        Public AirDate As Long
    '        Public MaM As Integer
    '        Public ProgBefore As String
    '        Public ProgAfter As String
    '        Public Programme As String
    '        Public Advertiser As String
    '        Public Product As String
    '        Public Filmcode As String
    '        Public Index As Integer
    '        Public PosInBreak As Byte
    '        Public SpotsInBreak As Byte
    '        Public MatchedSpot As cPlannedSpot
    '        Public SpotLength As Byte
    '        Public Deactivated As Boolean
    '        Public SpotType As Byte
    '        Public week As cWeek
    '        Private mvarBreakType As EnumBreakType
    '        Public SecondRating As Single
    '        Public AdedgeChannel As String
    '        Private mvarID As String
    '        Private mvarBookingtype As cBookingType
    '        Private mvarGroupIdx As Integer
    '        Private Main As cKampanj

    '        Public Enum ActualTargetEnum
    '            ateMainTarget = 0
    '            ateSecondTarget = 1
    '            ateThirdTarget = 2
    '            ateAllAdults = 3
    '            ateBuyingTarget = 4
    '            ateCustomTarget = 5
    '        End Enum

    '        Friend WriteOnly Property MainObject() As cKampanj
    '            Set(ByVal value As cKampanj)

    '                Main = value

    '            End Set
    '        End Property

    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ID
    '        ' DateTime  : 2003-07-18 10:06
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets the ID to be used when saving relations between spots
    '        '             to file
    '        '---------------------------------------------------------------------------------------
    '        '        
    '        Friend Property ID() As String

    '            Get
    '                On Error GoTo ID_Error

    '                Return mvarID

    '                On Error GoTo 0
    '                Exit Property
    'ID_Error:

    '                Err.Raise(Err.Number, "cActualSpot: ID", Err.Description)

    '            End Get

    '            Set(ByVal value As String)
    '                Dim TmpSpot As cActualSpot

    '                On Error GoTo ID_Error

    '                If mvarID <> "" Then
    '                    TmpSpot = Main.ActualSpots(mvarID)
    '                    Main.ActualSpotsCollection.Remove(mvarID)
    '                    Main.ActualSpotsCollection.Add(TmpSpot, value)
    '                End If
    '                mvarID = value

    '                On Error GoTo 0
    '                Exit Property

    'ID_Error:

    '                Err.Raise(Err.Number, "cActualSpot: ID", Err.Description)
    '            End Set
    '        End Property



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : Channel
    '        ' DateTime  : 2003-06-12 16:05
    '        ' Author    : joho
    '        ' Purpose   : Pointer to an object of the cChannel class that contains data about
    '        '             the channel where the spot was aired
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property Channel() As cChannel


    '            Get
    '                On Error GoTo Channel_Error

    '                Channel = mvarChannel

    '                On Error GoTo 0
    '                Exit Property

    'Channel_Error:

    '                Err.Raise(Err.Number, "cActualSpot: Channel", Err.Description)
    '            End Get
    '            Set(ByVal value As cChannel)
    '                On Error GoTo Channel_Error

    '                mvarChannel = value

    '                If value Is Nothing Then
    '                    AdedgeChannel = ""
    '                Else
    '                    If InStr(value.AdEdgeNames, ",") = 0 Then
    '                        AdedgeChannel = Channel.AdEdgeNames
    '                    Else
    '                        AdedgeChannel = Left(value.AdEdgeNames, InStr(value.AdEdgeNames, ",") - 1)
    '                    End If
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'Channel_Error:

    '                Err.Raise(Err.Number, "cActualSpot: Channel", Err.Description)
    '            End Set

    '        End Property



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : BookingType
    '        ' DateTime  : 2003-07-15 12:51
    '        ' Author    : joho
    '        ' Purpose   : Pointer to a cBookingType representing the Booking Type this spot
    '        '             is a part of
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property Bookingtype() As cBookingType

    '            Get
    '                On Error GoTo BookingType_Error

    '                Bookingtype = mvarBookingtype

    '                On Error GoTo 0
    '                Exit Property

    'BookingType_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: BookingType", Err.Description)
    '            End Get
    '            Set(ByVal value As cBookingType)
    '                On Error GoTo BookingType_Error

    '                mvarBookingtype = value

    '                On Error GoTo 0
    '                Exit Property

    'BookingType_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: BookingType", Err.Description)
    '            End Set
    '        End Property



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : GroupIdx
    '        ' DateTime  : 2003-09-04 13:53
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets the index used to access this spot in Adedge
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property GroupIdx() As Integer

    '            Get
    '                On Error GoTo GroupIdx_Error

    '                GroupIdx = mvarGroupIdx

    '                On Error GoTo 0
    '                Exit Property

    'GroupIdx_Error:

    '                Err.Raise(Err.Number, "cActualSpot: GroupIdx", Err.Description)

    '            End Get
    '            Set(ByVal value As Integer)
    '                On Error GoTo GroupIdx_Error

    '                mvarGroupIdx = value

    '                On Error GoTo 0
    '                Exit Property

    'GroupIdx_Error:

    '                Err.Raise(Err.Number, "cActualSpot: GroupIdx", Err.Description)

    '            End Set
    '        End Property



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : BreakType
    '        ' DateTime  : 2003-09-08 14:27
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets the type of break this spot was aired in.
    '        '
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property BreakType() As EnumBreakType

    '            Get
    '                On Error GoTo BreakType_Error

    '                BreakType = mvarBreakType

    '                On Error GoTo 0
    '                Exit Property

    'BreakType_Error:

    '                Err.Raise(Err.Number, "cActualSpot: BreakType", Err.Description)

    '            End Get
    '            Set(ByVal value As EnumBreakType)
    '                On Error GoTo BreakType_Error

    '                mvarBreakType = value

    '                On Error GoTo 0
    '                Exit Property

    'BreakType_Error:

    '                Err.Raise(Err.Number, "cActualSpot: BreakType", Err.Description)

    '            End Set
    '        End Property

    '        Public ReadOnly Property Rating(Optional ByVal Target As ActualTargetEnum = ActualTargetEnum.ateMainTarget, Optional ByVal CustomTarget As String = "3+", Optional ByVal Customuniverse As String = "") As Single
    '            Get
    '                Dim t As String
    '                Dim u As String
    '                Dim Dummy As Integer

    '                If Target = ActualTargetEnum.ateMainTarget Then
    '                    t = Main.MainTarget.TargetName
    '                    u = Main.MainTarget.Universe
    '                    Dummy = Main.TargColl(t, Main.Adedge)
    '                    If Err.Number > 0 Then
    '                        Dummy = Main.MainTarget.UniSize
    '                    End If
    '                ElseIf Target = ActualTargetEnum.ateSecondTarget Then
    '                    t = Main.SecondaryTarget.TargetName
    '                    u = Main.SecondaryTarget.Universe
    '                    Dummy = Main.TargColl(t, Main.Adedge)
    '                    If Err.Number > 0 Then
    '                        Dummy = Main.SecondaryTarget.UniSize
    '                    End If
    '                ElseIf Target = ActualTargetEnum.ateThirdTarget Then
    '                    t = Main.ThirdTarget.TargetName
    '                    u = Main.ThirdTarget.Universe
    '                    Dummy = Main.TargColl(t, Main.Adedge)
    '                    If Err.Number > 0 Then
    '                        Dummy = Main.ThirdTarget.UniSize
    '                    End If
    '                ElseIf Target = ActualTargetEnum.ateBuyingTarget Then
    '                    t = mvarBookingtype.BuyingTarget.Target.TargetName
    '                    u = mvarChannel.BuyingUniverse
    '                    Dummy = Main.TargColl(t, Main.Adedge)
    '                    If Err.Number > 0 Then
    '                        Dummy = mvarBookingtype.BuyingTarget.Target.UniSize
    '                    End If
    '                ElseIf Target = ActualTargetEnum.ateAllAdults Then
    '                    t = Main.AllAdults
    '                    u = ""
    '                ElseIf Target = ActualTargetEnum.ateCustomTarget Then
    '                    t = CustomTarget
    '                    u = Customuniverse
    '                Else
    '                    t = ""
    '                    u = ""
    '                End If
    '                Rating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Main.UniColl(u) - 1, Main.TargColl(t, Main.Adedge) - 1)
    '            End Get
    '        End Property

    '        Public ReadOnly Property Rating30(Optional ByVal Target As ActualTargetEnum = ActualTargetEnum.ateMainTarget, Optional ByVal CustomTarget As String = "3+", Optional ByVal Customuniverse As String = "") As Single
    '            Get

    '                Dim t As String
    '                Dim u As String
    '                Dim Dummy As Integer

    '                If Target = ActualTargetEnum.ateMainTarget Then
    '                    t = Main.MainTarget.TargetName
    '                    u = Main.MainTarget.Universe
    '                    Dummy = Main.TargColl(t, Main.Adedge)
    '                    If Err.Number > 0 Then
    '                        Dummy = Main.MainTarget.UniSize
    '                    End If
    '                ElseIf Target = ActualTargetEnum.ateSecondTarget Then
    '                    t = Main.SecondaryTarget.TargetName
    '                    u = Main.SecondaryTarget.Universe
    '                    Dummy = Main.TargColl(t, Main.Adedge)
    '                    If Err.Number > 0 Then
    '                        Dummy = Main.SecondaryTarget.UniSize
    '                    End If
    '                ElseIf Target = ActualTargetEnum.ateBuyingTarget Then
    '                    t = mvarBookingtype.BuyingTarget.Target.TargetName
    '                    u = mvarChannel.BuyingUniverse
    '                    Dummy = Main.TargColl(t, Main.Adedge)
    '                    If Err.Number > 0 Then
    '                        Dummy = mvarBookingtype.BuyingTarget.Target.UniSize
    '                    End If
    '                ElseIf Target = ActualTargetEnum.ateAllAdults Then
    '                    t = Main.AllAdults
    '                    u = ""
    '                ElseIf Target = ActualTargetEnum.ateCustomTarget Then
    '                    t = CustomTarget
    '                    u = Customuniverse
    '                Else
    '                    t = ""
    '                    u = ""
    '                End If
    '                Rating30 = Main.Adedge.getUnitGroup(Connect.eUnits.uBrandTrp30, mvarGroupIdx, 0, Main.UniColl(u) - 1, Main.TargColl(t, Main.Adedge) - 1)
    '            End Get
    '        End Property

    '        Public Sub New(ByVal MainObject As cKampanj)
    '            Main = MainObject
    '        End Sub

    '    End Class

    'Public Class cActualSpots
    '    Implements System.Collections.IEnumerable

    '    'local variable to hold collection
    '    Private mCol As Collection
    '    Private mvarLastSpot As cActualSpot
    '    Private mvarMainObject As cKampanj

    '    Public Function Add(ByVal AirDate As Date, ByVal MaM As Integer, Optional ByVal Filmcode As String = "", Optional ByVal Channel As cChannel = Nothing, Optional ByVal Programme As String = "", Optional ByVal SpotLength As Byte = 30, Optional ByVal Advertiser As String = "", Optional ByVal Product As String = "", Optional ByVal Index As Integer = 100, Optional ByVal PosInBreak As Byte = 0, Optional ByVal SpotsInBreak As Byte = 0, Optional ByVal BreakType As Byte = 0, Optional ByVal AdedgeChannel As String = "", Optional ByVal SpotType As Byte = 0, Optional ByVal Sort As Boolean = False) As cActualSpot
    '        'create a new object
    '        Dim objNewMember As New cActualSpot(mvarMainObject)
    '        Dim i As Integer
    '        Dim BeforeSpot As cActualSpot

    '        If Not Channel Is Nothing Then
    '            objNewMember.Channel = Channel
    '        End If

    '        If AdedgeChannel <> "" Then
    '            objNewMember.AdedgeChannel = AdedgeChannel
    '        End If

    '        objNewMember.AirDate = AirDate.ToOADate
    '        objNewMember.MaM = MaM
    '        objNewMember.Programme = Programme
    '        objNewMember.Advertiser = Advertiser
    '        objNewMember.Product = Product
    '        objNewMember.Filmcode = Filmcode
    '        'objNewMember.Rating = Rating
    '        objNewMember.Index = Index
    '        objNewMember.PosInBreak = PosInBreak
    '        objNewMember.SpotsInBreak = SpotsInBreak
    '        objNewMember.SpotLength = SpotLength
    '        objNewMember.Deactivated = False
    '        objNewMember.SpotType = SpotType
    '        objNewMember.BreakType = BreakType
    '        objNewMember.ID = CreateGUID()
    '        objNewMember.MainObject = mvarMainObject

    '        'set the properties passed into the method

    '        BeforeSpot = Nothing
    '        If Sort Then
    '            For i = 1 To mCol.Count
    '                If objNewMember.AirDate < mCol(i).AirDate Then
    '                    BeforeSpot = mCol(i)
    '                ElseIf objNewMember.AirDate = mCol(i).AirDate Then
    '                    If objNewMember.MaM < mCol(i).MaM Then
    '                        BeforeSpot = mCol(i)
    '                        Exit For
    '                    End If
    '                End If
    '            Next
    '        End If

    '        If Not BeforeSpot Is Nothing Then
    '            mCol.Add(objNewMember, objNewMember.ID, BeforeSpot.ID)
    '        Else
    '            mCol.Add(objNewMember, objNewMember.ID)
    '        End If
    '        'return the object created
    '        Add = objNewMember
    '        objNewMember = Nothing


    '    End Function

    '    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cActualSpot
    '        'used when referencing an element in the collection
    '        'vntIndexKey contains either the Index or Key to the collection,
    '        'this is why it is declared as a Variant
    '        'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
    '        Get
    '            Item = mCol(vntIndexKey)
    '        End Get
    '    End Property

    '    Public ReadOnly Property Count() As Long
    '        'used when retrieving the number of elements in the
    '        'collection. Syntax: Debug.Print x.Count
    '        Get
    '            Count = mCol.Count
    '        End Get
    '    End Property

    '    Public Sub Remove(ByVal vntIndexKey As Object)
    '        'used when removing an element from the collection
    '        'vntIndexKey contains either the Index or Key, which is why
    '        'it is declared as a Variant
    '        'Syntax: x.Remove(xyz)


    '        mCol.Remove(vntIndexKey)
    '    End Sub

    '    Public ReadOnly Property LastSpot() As cActualSpot
    '        Get
    '            LastSpot = mvarLastSpot
    '        End Get
    '    End Property

    '    Friend WriteOnly Property MainObject() As cKampanj
    '        Set(ByVal value As cKampanj)
    '            mvarMainObject = value
    '        End Set
    '    End Property

    '    Friend ReadOnly Property Collection() As Collection
    '        Get
    '            Collection = mCol
    '        End Get
    '    End Property

    '    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
    '        GetEnumerator = mCol.GetEnumerator
    '    End Function

    '    Public Sub New(ByVal Main As cKampanj)
    '        mCol = New Collection
    '        mvarMainObject = Main
    '    End Sub

    '    Protected Overrides Sub Finalize()
    '        mCol = Nothing
    '        MyBase.Finalize()
    '    End Sub

    'End Class

    '    Public Class cChannel
    '        Private mvarID As Long
    '        Private mvarChannelName As String
    '        Private mvarShortname As String
    '        Private mvarMainTarget As cTarget
    '        Private mvarSecondaryTarget As cTarget
    '        Private mvarBookingTypes As cBookingTypes
    '        Private mvarBuyingUniverse As String
    '        Private mvarAdEdgeNames As String
    '        'Public Panel As Integer
    '        Private mvarDefaultArea As String
    '        Private mvarIsVisible As Boolean
    '        Private mvarAgencyCommission As Single
    '        Private mvarListNumber As Integer
    '        Private mvarArea As String
    '        Private mvarMatrixName As String
    '        Private mvarMarathonName As String
    '        Private mvarDeliveryAddress As String
    '        Private mvarVHSAddress As String
    '        Private mvarPenalty As Single
    '        Private mvarConnectedChannel As String
    '        Public LogoPath As String

    '        Private ParentColl As Collection
    '        Private Main As cKampanj

    '        Friend WriteOnly Property MainObject() As cKampanj
    '            Set(ByVal value As cKampanj)
    '                Main = value
    '                mvarMainTarget.MainObject = value
    '                mvarSecondaryTarget.MainObject = value
    '                mvarBookingTypes.MainObject = value
    '            End Set

    '        End Property

    '        Friend WriteOnly Property ParentCollection() As Collection
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : ParentCollection
    '            ' DateTime  : 2003-07-07 13:18
    '            ' Author    : joho
    '            ' Purpose   : Sets the Collection of wich this channel is a member. This is used
    '            '             when a new ChannelName is set. See that property for further explanation
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Set(ByVal value As Collection)
    '                ParentColl = value
    '            End Set

    '        End Property

    '        Public Property ID() As Long
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : ID
    '            ' DateTime  : 2003-07-07 13:06
    '            ' Author    : joho
    '            ' Purpose   :
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo ID_Error

    '                ID = mvarID

    '                On Error GoTo 0
    '                Exit Property

    'ID_Error:

    '                Err.Raise(Err.Number, "cChannel: ID", Err.Description)
    '            End Get
    '            Set(ByVal value As Long)
    '                On Error GoTo ID_Error

    '                mvarID = value

    '                On Error GoTo 0
    '                Exit Property

    'ID_Error:

    '                Err.Raise(Err.Number, "cChannel: ID", Err.Description)

    '            End Set

    '        End Property

    '        Public Property ChannelName() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : ChannelName
    '            ' DateTime  : 2003-07-07 13:06
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the channel name. If a new channel name is set, the channel
    '            '             has to be removed from the collection and re-added with the new name
    '            '             as key.
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo ChannelName_Error

    '                ChannelName = mvarChannelName

    '                On Error GoTo 0
    '                Exit Property

    'ChannelName_Error:

    '                Err.Raise(Err.Number, "cChannel: ChannelName", Err.Description)

    '            End Get
    '            Set(ByVal value As String)
    '                Dim TmpChannel As cChannel

    '                On Error GoTo ChannelName_Error

    '                If value <> mvarChannelName And mvarChannelName <> "" Then

    '                    TmpChannel = ParentColl(mvarChannelName)
    '                    ParentColl.Add(TmpChannel, value)
    '                    ParentColl.Remove(mvarChannelName)

    '                End If
    '                mvarChannelName = value


    '                On Error GoTo 0
    '                Exit Property

    'ChannelName_Error:

    '                Err.Raise(Err.Number, "cChannel: ChannelName", Err.Description)

    '            End Set
    '        End Property

    '        Public Property Shortname() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Shortname
    '            ' DateTime  : 2003-07-07 13:06
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the abbrevation for the channel
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Shortname_Error

    '                Shortname = mvarShortname

    '                On Error GoTo 0
    '                Exit Property

    'Shortname_Error:

    '                Err.Raise(Err.Number, "cChannel: Shortname", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo Shortname_Error

    '                mvarShortname = value

    '                On Error GoTo 0
    '                Exit Property

    'Shortname_Error:

    '                Err.Raise(Err.Number, "cChannel: Shortname", Err.Description)

    '            End Set
    '        End Property

    '        Public Property MainTarget() As cTarget
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : MainTarget
    '            ' DateTime  : 2003-07-07 13:06
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the main target for this channel
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo MainTarget_Error

    '                MainTarget = mvarMainTarget

    '                On Error GoTo 0
    '                Exit Property

    'MainTarget_Error:

    '                Err.Raise(Err.Number, "cChannel: MainTarget", Err.Description)

    '            End Get
    '            Set(ByVal value As cTarget)
    '                On Error GoTo MainTarget_Error

    '                mvarMainTarget = value

    '                On Error GoTo 0
    '                Exit Property

    'MainTarget_Error:

    '                Err.Raise(Err.Number, "cChannel: MainTarget", Err.Description)

    '            End Set
    '        End Property

    '        Public Property SecondaryTarget() As cTarget
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : SecondaryTarget
    '            ' DateTime  : 2003-07-08 14:43
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the Secondary Target for this channel
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo SecondaryTarget_Error

    '                SecondaryTarget = mvarSecondaryTarget

    '                On Error GoTo 0
    '                Exit Property

    'SecondaryTarget_Error:

    '                Err.Raise(Err.Number, "cChannel: SecondaryTarget", Err.Description)
    '            End Get
    '            Set(ByVal value As cTarget)
    '                On Error GoTo SecondaryTarget_Error

    '                mvarSecondaryTarget = value

    '                On Error GoTo 0
    '                Exit Property

    'SecondaryTarget_Error:

    '                Err.Raise(Err.Number, "cChannel: SecondaryTarget", Err.Description)

    '            End Set
    '        End Property

    '        Public Property BuyingUniverse() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : BuyingUniverse
    '            ' DateTime  : 2003-07-08 14:43
    '            ' Author    : joho
    '            ' Purpose   : The Universe used for buying in the channel
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo BuyingUniverse_Error

    '                BuyingUniverse = mvarBuyingUniverse

    '                On Error GoTo 0
    '                Exit Property

    'BuyingUniverse_Error:

    '                Err.Raise(Err.Number, "cChannel: BuyingUniverse", Err.Description)

    '            End Get
    '            Set(ByVal value As String)
    '                Dim TmpBookingType As cBookingType

    '                On Error GoTo BuyingUniverse_Error

    '                mvarBuyingUniverse = value

    '                For Each TmpBookingType In mvarBookingTypes
    '                    TmpBookingType.Pricelist.BuyingUniverse = mvarBuyingUniverse
    '                Next

    '                On Error GoTo 0
    '                Exit Property

    'BuyingUniverse_Error:

    '                Err.Raise(Err.Number, "cChannel: BuyingUniverse", Err.Description)

    '            End Set
    '        End Property

    '        Public Property BookingTypes() As cBookingTypes
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : BookingTypes
    '            ' DateTime  : 2003-07-08 14:44
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the pointer to the cBookingTypes object holding each
    '            '             added BookingType
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo BookingTypes_Error

    '                If mvarBookingTypes.MainObject Is Nothing And Not Main Is Nothing Then
    '                    mvarBookingTypes.MainObject = Main
    '                End If
    '                If Not mvarBookingTypes.ParentChannel Is Me Then
    '                    mvarBookingTypes.ParentChannel = Me
    '                End If
    '                BookingTypes = mvarBookingTypes

    '                On Error GoTo 0
    '                Exit Property

    'BookingTypes_Error:

    '                Err.Raise(Err.Number, "cChannel: BookingTypes", Err.Description)
    '            End Get
    '            Set(ByVal value As cBookingTypes)
    '                On Error GoTo BookingTypes_Error

    '                mvarBookingTypes = value
    '                mvarBookingTypes.MainObject = Main

    '                On Error GoTo 0
    '                Exit Property

    'BookingTypes_Error:

    '                Err.Raise(Err.Number, "cChannel: BookingTypes", Err.Description)


    '            End Set
    '        End Property

    '        Public Property AdEdgeNames() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : AdEdgeNames
    '            ' DateTime  : 2003-07-08 14:45
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets a comma separated string with the names used to reference
    '            '             this channel in AdEdge. Most commonly there will only be one name,
    '            '             but occasionally a second name might be used (Discovery se, Disc Mix se)
    '            '---------------------------------------------------------------------------------------
    '            Get
    '                On Error GoTo AdEdgeNames_Error

    '                AdEdgeNames = mvarAdEdgeNames

    '                On Error GoTo 0
    '                Exit Property

    'AdEdgeNames_Error:

    '                Err.Raise(Err.Number, "cChannel: AdEdgeNames", Err.Description)

    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo AdEdgeNames_Error

    '                mvarAdEdgeNames = value

    '                On Error GoTo 0
    '                Exit Property

    'AdEdgeNames_Error:

    '                Err.Raise(Err.Number, "cChannel: AdEdgeNames", Err.Description)

    '            End Set
    '            '
    '        End Property

    '        Public Property DefaultArea() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : DefaultArea
    '            ' DateTime  : 2003-07-09 11:35
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the default Area for this channel. Might not be implemented
    '            '---------------------------------------------------------------------------------------
    '            Get
    '                On Error GoTo DefaultArea_Error

    '                DefaultArea = mvarDefaultArea

    '                On Error GoTo 0
    '                Exit Property

    'DefaultArea_Error:

    '                Err.Raise(Err.Number, "cChannel: DefaultArea", Err.Description)

    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo DefaultArea_Error

    '                mvarDefaultArea = value

    '                On Error GoTo 0
    '                Exit Property

    'DefaultArea_Error:

    '                Err.Raise(Err.Number, "cChannel: DefaultArea", Err.Description)

    '            End Set
    '        End Property

    '        Public Property IsVisible() As Boolean
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : IsVisible
    '            ' DateTime  : 2003-07-09 11:35
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets wether this channel should be shown in the charts or not. No longer used.
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo IsVisible_Error

    '                IsVisible = mvarIsVisible

    '                On Error GoTo 0
    '                Exit Property

    'IsVisible_Error:

    '                Err.Raise(Err.Number, "cChannel: IsVisible", Err.Description)

    '            End Get
    '            Set(ByVal value As Boolean)
    '                On Error GoTo IsVisible_Error

    '                mvarIsVisible = value

    '                On Error GoTo 0
    '                Exit Property

    'IsVisible_Error:

    '                Err.Raise(Err.Number, "cChannel: IsVisible", Err.Description)

    '            End Set
    '        End Property

    '        Public Property AgencyCommission() As Single
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : AgencyCommission
    '            ' DateTime  : 2003-07-09 11:35
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the Agency comission used for this channel
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo AgencyCommission_Error

    '                AgencyCommission = mvarAgencyCommission

    '                On Error GoTo 0
    '                Exit Property

    'AgencyCommission_Error:

    '                Err.Raise(Err.Number, "cChannel: AgencyCommission", Err.Description)

    '            End Get
    '            Set(ByVal value As Single)
    '                On Error GoTo AgencyCommission_Error

    '                mvarAgencyCommission = value

    '                On Error GoTo 0
    '                Exit Property

    'AgencyCommission_Error:

    '                Err.Raise(Err.Number, "cChannel: AgencyCommission", Err.Description)

    '            End Set
    '        End Property

    '        Public Sub ReadDefaultProperties(Optional ByVal Area As String = "")
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : ReadDefaultProperties
    '            ' DateTime  : 2003-07-07 12:46
    '            ' Author    : joho
    '            ' Purpose   : Reads in general settings such as Buying Targets, Universe sizes,
    '            '             Names in AdvantEdge etc. from the data files
    '            '---------------------------------------------------------------------------------------
    '            '

    '            'On Error GoTo ReadDefaultProperties_Error

    '            Dim Ini As New clsIni
    '            Dim XMLDoc As New Xml.XmlDocument
    '            Dim XMLChannels As Xml.XmlElement
    '            Dim XMLTmpNode As Xml.XmlElement

    '            If Area = "" Then
    '                Area = Main.Area
    '            End If

    '            XMLDoc.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Area & "\Channels.xml")

    '            XMLChannels = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("Channels")

    '            XMLTmpNode = XMLChannels.SelectSingleNode("Channel[@Name='" & mvarChannelName & "']")

    '            If Not XMLTmpNode Is Nothing Then
    '                mvarShortname = XMLTmpNode.GetAttribute("Shortname")
    '                mvarBuyingUniverse = XMLTmpNode.GetAttribute("BuyingUniverse")
    '                mvarAdEdgeNames = XMLTmpNode.GetAttribute("AdEdgeNames")
    '                mvarMatrixName = XMLTmpNode.GetAttribute("MatrixName")
    '                mvarMarathonName = XMLTmpNode.GetAttribute("Marathon")
    '                mvarConnectedChannel = XMLTmpNode.GetAttribute("ConnectedChannel")
    '                mvarPenalty = Val(XMLTmpNode.GetAttribute("Penalty")) / 100
    '                mvarAgencyCommission = XMLTmpNode.GetAttribute("AgencyCommission")
    '                mvarListNumber = XMLTmpNode.GetAttribute("ListNumber")
    '                LogoPath = XMLTmpNode.GetAttribute("Logo")
    '            End If

    '            mvarDefaultArea = Area
    '            If mvarListNumber = 0 Then
    '                mvarListNumber = ParentColl(ParentColl.Count).ListNumber + 1
    '            End If

    '            If My.Computer.FileSystem.FileExists(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Area & "\Channel info\" & mvarChannelName & ".xml") Then
    '                XMLDoc.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Area & "\Channel info\" & mvarChannelName & ".xml")
    '                XMLTmpNode = XMLDoc.GetElementsByTagName("Address").Item(0)
    '                mvarDeliveryAddress = XMLTmpNode.InnerText
    '            Else
    '                mvarDeliveryAddress = ""
    '            End If
    '            'On Error GoTo SkipAddress
    '            '            FileNum = FreeFile()
    '            '    Open TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Area & "\Channel info\" & mvarChannelName & ".inf" For Input As FileNum

    '            '    Line Input #FileNum, Data

    '            '            Do Until Data = "[Address]"
    '            '        Line Input #FileNum, Data
    '            '                If EOF(FileNum) Then
    '            '                    Exit Do
    '            '                End If
    '            '            Loop

    '            '    Line Input #FileNum, Data
    '            '            Address = ""

    '            '            Do Until Left(Data, 1) = "["
    '            '                Address = Address + Left(Data, Len(Data)) + Chr(10)
    '            '        Line Input #FileNum, Data
    '            '                If EOF(FileNum) Then Exit Do
    '            '            Loop
    '            '            mvarDeliveryAddress = Address

    '            'SkipAddress:
    '            '            Close(FileNum)
    '            '            On Error GoTo 0
    '            '            Exit Sub

    'ReadDefaultProperties_Error:

    '            '            Err.Raise(Err.Number, "cChannel: ReadDefaultProperties", Err.Description)

    '        End Sub

    '        Public Function PlannedBudget() As Decimal
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : PlannedBudget
    '            ' DateTime  : 2003-07-15 12:54
    '            ' Author    : joho
    '            ' Purpose   : Returns the planned budget for this channel calculated based on
    '            '             estimated ratings and booked CPPs if no Price is set for the spot.
    '            '---------------------------------------------------------------------------------------
    '            '

    '            Dim TmpPlannedSpot As cPlannedSpot

    '            On Error GoTo PlannedBudget_Error

    '            PlannedBudget = 0
    '            For Each TmpPlannedSpot In Main.PlannedSpots
    '                If TmpPlannedSpot.Channel Is Me Then
    '                    If TmpPlannedSpot.PriceNet > 0 Then
    '                        PlannedBudget = PlannedBudget + TmpPlannedSpot.PriceNet
    '                    Else
    '                        PlannedBudget = PlannedBudget + TmpPlannedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) * TmpPlannedSpot.Week.NetCPP
    '                    End If
    '                End If
    '            Next

    '            On Error GoTo 0
    '            Exit Function

    'PlannedBudget_Error:

    '            Err.Raise(Err.Number, "cChannel: PlannedBudget", Err.Description)

    '        End Function

    '        Public Property ListNumber() As Integer
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : ListNumber
    '            ' DateTime  : 2003-07-25 19:44
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets a number indicating in wich order channels should be
    '            '             sorted. The channel with the lowest channel number should be higher
    '            '             in the list. See cChannels.Add for more details.
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo ListNumber_Error

    '                ListNumber = mvarListNumber

    '                On Error GoTo 0
    '                Exit Property

    'ListNumber_Error:

    '                Err.Raise(Err.Number, "cChannel: ListNumber", Err.Description)

    '            End Get
    '            Set(ByVal value As Integer)
    '                On Error GoTo ListNumber_Error

    '                mvarListNumber = value

    '                On Error GoTo 0
    '                Exit Property

    'ListNumber_Error:

    '                Err.Raise(Err.Number, "cChannel: ListNumber", Err.Description)

    '            End Set
    '        End Property

    '        Public Property Area() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Area
    '            ' DateTime  : 2003-08-31 22:43
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the Area (Country) for this channel
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Area_Error

    '                Area = mvarArea

    '                On Error GoTo 0
    '                Exit Property

    'Area_Error:

    '                Err.Raise(Err.Number, "cChannel: Area", Err.Description)

    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo Area_Error

    '                mvarArea = value

    '                On Error GoTo 0
    '                Exit Property

    'Area_Error:

    '                Err.Raise(Err.Number, "cChannel: Area", Err.Description)

    '            End Set
    '        End Property

    '        Public Property MatrixName() As String
    '            Get
    '                On Error GoTo MatrixName_Error
    '                Helper.WriteToLogFile("MatrixName")

    '                MatrixName = mvarMatrixName

    '                On Error GoTo 0
    '                Exit Property

    'MatrixName_Error:

    '                Helper.WriteToLogFile("ERROR in cChannel.MatrixName: " & Err.Description)
    '                Err.Raise(Err.Number, "cChannel: MatrixName", Err.Description)
    '                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo MatrixName_Error
    '                Helper.WriteToLogFile("MatrixName")

    '                mvarMatrixName = value

    '                On Error GoTo 0
    '                Exit Property

    'MatrixName_Error:

    '                Helper.WriteToLogFile("ERROR in cChannel.MatrixName: " & Err.Description)
    '                Err.Raise(Err.Number, "cChannel: MatrixName", Err.Description)
    '                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"
    '            End Set
    '        End Property

    '        Public Property MarathonName() As String
    '            Get
    '                On Error GoTo MarathonName_Error
    '                Helper.WriteToLogFile("MarathonName")

    '                MarathonName = mvarMarathonName

    '                On Error GoTo 0
    '                Exit Property

    'MarathonName_Error:

    '                Helper.WriteToLogFile("ERROR in cChannel.MarathonName: " & Err.Description)
    '                Err.Raise(Err.Number, "cChannel: MarathonName", Err.Description)
    '                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo MarathonName_Error
    '                Helper.WriteToLogFile("MarathonName")

    '                mvarMarathonName = value

    '                On Error GoTo 0
    '                Exit Property

    'MarathonName_Error:

    '                Helper.WriteToLogFile("ERROR in cChannel.MarathonName: " & Err.Description)
    '                Err.Raise(Err.Number, "cChannel: MarathonName", Err.Description)
    '                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"

    '            End Set
    '        End Property

    '        Public Property DeliveryAddress() As String
    '            Get
    '                On Error GoTo DeliveryAddress_Error
    '                Helper.WriteToLogFile("DeliveryAddress")

    '                DeliveryAddress = mvarDeliveryAddress

    '                On Error GoTo 0
    '                Exit Property

    'DeliveryAddress_Error:

    '                Helper.WriteToLogFile("ERROR in cChannel.DeliveryAddress: " & Err.Description)
    '                Err.Raise(Err.Number, "cChannel: DeliveryAddress", Err.Description)
    '                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo DeliveryAddress_Error
    '                Helper.WriteToLogFile("DeliveryAddress")

    '                mvarDeliveryAddress = value

    '                On Error GoTo 0
    '                Exit Property

    'DeliveryAddress_Error:

    '                Helper.WriteToLogFile("ERROR in cChannel.DeliveryAddress: " & Err.Description)
    '                Err.Raise(Err.Number, "cChannel: DeliveryAddress", Err.Description)
    '                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"


    '            End Set
    '        End Property

    '        Public Property VHSAddress() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : VHSAddress
    '            ' DateTime  : -
    '            ' Author    : joho
    '            ' Purpose   : Sets the adress for where the Comercial films whould be sent?????????????
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo VHSAddress_Error
    '                Helper.WriteToLogFile("VHSAddress")

    '                VHSAddress = mvarVHSAddress

    '                On Error GoTo 0
    '                Exit Property

    'VHSAddress_Error:

    '                Helper.WriteToLogFile("ERROR in cChannel.VHSAddress: " & Err.Description)
    '                Err.Raise(Err.Number, "cChannel: VHSAddress", Err.Description)
    '                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo VHSAddress_Error
    '                Helper.WriteToLogFile("VHSAddress")

    '                mvarVHSAddress = value

    '                On Error GoTo 0
    '                Exit Property

    'VHSAddress_Error:

    '                Helper.WriteToLogFile("ERROR in cChannel.VHSAddress: " & Err.Description)
    '                Err.Raise(Err.Number, "cChannel: VHSAddress", Err.Description)
    '                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"


    '            End Set
    '        End Property

    '        Public Property Penalty() As Single
    '            Get
    '                On Error GoTo Penalty_Error
    '                Helper.WriteToLogFile("Penalty")

    '                Penalty = mvarPenalty

    '                On Error GoTo 0
    '                Exit Property

    'Penalty_Error:

    '                Helper.WriteToLogFile("ERROR in cChannel.Penalty: " & Err.Description)
    '                Err.Raise(Err.Number, "cChannel: Penalty", Err.Description)
    '                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"
    '            End Get
    '            Set(ByVal value As Single)
    '                On Error GoTo Penalty_Error
    '                Helper.WriteToLogFile("Penalty")

    '                mvarPenalty = value

    '                On Error GoTo 0
    '                Exit Property

    'Penalty_Error:

    '                Helper.WriteToLogFile("ERROR in cChannel.Penalty: " & Err.Description)
    '                Err.Raise(Err.Number, "cChannel: Penalty", Err.Description)
    '                'MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error"

    '            End Set
    '        End Property

    '        Public Property ConnectedChannel() As String
    '            Get
    '                ConnectedChannel = mvarConnectedChannel
    '            End Get
    '            Set(ByVal value As String)
    '                mvarConnectedChannel = value
    '            End Set
    '        End Property

    '        Public Function TotalTRP() As Single
    '            Dim TmpBT As cBookingType
    '            Dim TRP As Single

    '            For Each TmpBT In mvarBookingTypes
    '                TRP = TRP + TmpBT.TotalTRP
    '            Next
    '            TotalTRP = TRP
    '        End Function

    '        Public Sub New(ByVal Main As cKampanj)
    '            'initialize new variables for a new booking
    '            mvarBookingTypes = New cBookingTypes(Main)
    '            mvarMainTarget = New cTarget(Main)
    '            mvarSecondaryTarget = New cTarget(Main)
    '        End Sub

    '        Protected Overrides Sub Finalize()
    '            mvarBookingTypes = Nothing
    '            MyBase.Finalize()
    '        End Sub

    '    End Class

    '    Public Class cChannels
    '        Implements Collections.IEnumerable

    '        'local variable to hold Collection
    '        Private mCol As Collection
    '        Private Main As cKampanj

    '        Friend WriteOnly Property MainObject() As cKampanj
    '            Set(ByVal value As cKampanj)
    '                Dim TmpChannel As cChannel

    '                Main = value
    '                For Each TmpChannel In mCol
    '                    TmpChannel.MainObject = value
    '                Next
    '            End Set
    '        End Property

    '        Public Function Add(ByVal Name As String, Optional ByVal Area As String = "") As cChannel
    '            'create a new object
    '            Dim objNewMember As cChannel
    '            Dim TmpChannel As New cChannel(Main)
    '            Dim i As Integer

    '            On Error GoTo Add_Error

    '            objNewMember = New cChannel(Main)

    '            'Make sure to remove all extra spaces
    '            Name = Trim(Name)
    '            Area = Trim(Area)

    '            'set the properties passed into the method
    '            objNewMember.MainObject = Main
    '            objNewMember.ChannelName = Name
    '            objNewMember.ParentCollection = mCol
    '            objNewMember.ReadDefaultProperties(Area)

    '            'Find channel before or after
    '            TmpChannel = Nothing
    '            If mCol.Count > 0 And objNewMember.ListNumber > 0 Then
    '                i = 1
    '                TmpChannel = mCol(1)
    '                While TmpChannel.ListNumber < objNewMember.ListNumber And i <= mCol.Count
    '                    TmpChannel = mCol(i)
    '                    i = i + 1
    '                End While
    '                If i >= mCol.Count And TmpChannel.ListNumber < objNewMember.ListNumber Then
    '                    TmpChannel = Nothing
    '                End If
    '            End If

    '            On Error Resume Next
    '            If TmpChannel Is Nothing OrElse TmpChannel.ChannelName = "" Then
    '                mCol.Add(objNewMember, Name)
    '            ElseIf TmpChannel.ListNumber > objNewMember.ListNumber Then
    '                mCol.Add(objNewMember, Name, TmpChannel.ChannelName)
    '            Else
    '                mCol.Add(objNewMember, Name, , TmpChannel.ChannelName)
    '            End If
    '            On Error GoTo Add_Error

    '            'return the object created
    '            Add = mCol(Name)
    '            objNewMember = Nothing


    '            On Error GoTo 0
    '            Exit Function

    'Add_Error:

    '            Err.Raise(Err.Number, "cChannels: Add", Err.Description)


    '        End Function

    '        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cChannel
    '            'used when referencing an element in the Collection
    '            'vntIndexKey contains either the Index or Key to the Collection,
    '            'this is why it is declared as a Variant
    '            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
    '            Get
    '                On Error GoTo ErrHandle
    '                Item = mCol(vntIndexKey)
    '                Exit Property

    'ErrHandle:
    '                Err.Raise(Err.Number, "cChannels", "Unknown channel: " & vntIndexKey)
    '            End Get
    '        End Property

    '        Public ReadOnly Property Count() As Long
    '            'used when retrieving the number of elements in the
    '            'Collection. Syntax: Debug.Print x.Count
    '            Get
    '                Count = mCol.Count
    '            End Get
    '        End Property

    '        Public Sub Remove(ByVal vntIndexKey As Object)
    '            'used when removing an element from the Collection
    '            'vntIndexKey contains either the Index or Key, which is why
    '            'it is declared as a Variant
    '            'Syntax: x.Remove(xyz)


    '            mCol.Remove(vntIndexKey)
    '        End Sub

    '        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
    '            GetEnumerator = mCol.GetEnumerator
    '        End Function

    '        Public Sub New(ByVal MainObject As cKampanj)
    '            mCol = New Collection
    '            Main = MainObject
    '        End Sub

    '        Protected Overrides Sub Finalize()
    '            mCol = Nothing
    '            MyBase.Finalize()
    '        End Sub
    '    End Class

    '    Public Class cBookingType
    '        '---------------------------------------------------------------------------------------
    '        ' Module    : cBookingType
    '        ' DateTime  : 2003-07-10 15:37
    '        ' Author    : joho
    '        ' Purpose   : Used to handle each different bookingtype used for each channel
    '        '---------------------------------------------------------------------------------------

    '        Private mvarName As String = ""
    '        Private mvarShortname As String
    '        Private mvarBuyingTarget As cPricelistTarget
    '        Private mvarIndexMainTarget As Integer
    '        Private mvarIndexSecondTarget As Integer
    '        Private mvarIndexAllAdults As Integer
    '        Private mvarDaypartSplit(0 To 5) As Byte
    '        Private mvarWeeks As cWeeks
    '        Private mvarBookIt As Boolean
    '        Private mvarGrossCPP As Single
    '        Private mvarAverageRating As Single
    '        Private mvarConfirmedNetBudget As Decimal
    '        Private mvarMarathonNetBudget As Decimal
    '        Private mvarBookingtype As Byte
    '        Private mvarContractNumber As String
    '        Private mvarOrderNumber As String
    '        Private mvarPriceList As cPricelist
    '        Private mvarIsVisible As Boolean
    '        Private mvarIsRBS As Boolean
    '        Private mvarIsSpecific As Boolean
    '        'Private mvarDiscount As Single
    '        'Private mvarNetCPT As Single
    '        'Private mvarNetCPP As Single
    '        'Private mvarIsEntered As EnteredEnum
    '        Private mvarIndexes As New cIndexes(Main)
    '        Private mvarAddedValues As New cAddedValues
    '        Private mvarFilmIndex(500) As Single
    '        Private mvarCompensations As New cCompensations(Me)

    '        Private ParentColl As Collection
    '        Private Main As cKampanj
    '        Private mvarParentChannel As cChannel

    '        Public Overrides Function ToString() As String
    '            Return ParentChannel.ChannelName & " " & mvarName
    '        End Function

    '        Public Function DistinctString() As String
    '            Return ParentChannel.ChannelName & "|" & mvarName
    '        End Function

    '        Public Property Compensations() As cCompensations
    '            Get
    '                Return mvarCompensations
    '            End Get
    '            Set(ByVal value As cCompensations)
    '                mvarCompensations = value
    '            End Set
    '        End Property

    '        Friend Property ParentChannel() As cChannel
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : ParentChannel
    '            ' DateTime  : 2003-08-15 16:18
    '            ' Author    : joho
    '            ' Purpose   :
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo ParentChannel_Error

    '                ParentChannel = mvarParentChannel

    '                On Error GoTo 0
    '                Exit Property

    'ParentChannel_Error:

    '                Err.Raise(Err.Number, "cBookingType: ParentChannel", Err.Description)
    '            End Get
    '            Set(ByVal value As cChannel)
    '                On Error GoTo ParentChannel_Error

    '                mvarParentChannel = value

    '                On Error GoTo 0
    '                Exit Property

    'ParentChannel_Error:

    '                Err.Raise(Err.Number, "cBookingType: ParentChannel", Err.Description)

    '            End Set
    '        End Property

    '        Friend WriteOnly Property MainObject()
    '            Set(ByVal value)
    '                Main = value
    '                mvarPriceList.MainObject = value
    '                mvarPriceList.Bookingtype = Me
    '            End Set
    '        End Property

    '        Friend WriteOnly Property ParentCollection()
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : ParentCollection
    '            ' DateTime  : 2003-07-07 13:18
    '            ' Author    : joho
    '            ' Purpose   : Sets the Collection of wich this channel is a member. This is used
    '            '             when a new Name is set. See that property for further explanation
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Set(ByVal value)
    '                ParentColl = value
    '            End Set
    '        End Property

    '        Public Property DaypartSplit(ByVal Index) As Byte
    '            Get
    '                On Error GoTo DaypartSplit_Error

    '                Return mvarDaypartSplit(Index)

    '                On Error GoTo 0
    '                Exit Property

    'DaypartSplit_Error:

    '                Err.Raise(Err.Number, "cBookingType: DaypartSplit", Err.Description)
    '            End Get
    '            Set(ByVal value As Byte)
    '                Dim TmpCPP As Decimal

    '                On Error GoTo DaypartSplit_Error

    '                mvarDaypartSplit(Index) = value

    '                If BuyingTarget.CalcCPP Then
    '                    BuyingTarget.CalculateCPP()
    '                End If
    '                TmpCPP = BuyingTarget.CPP
    '                mvarGrossCPP = TmpCPP
    '                On Error GoTo 0
    '                Exit Property

    'DaypartSplit_Error:

    '                Err.Raise(Err.Number, "cBookingType: DaypartSplit", Err.Description)

    '            End Set
    '        End Property

    '        Public Function PlannedNetBudget()
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : PlannedNetBudget
    '            ' DateTime  : 2003-07-03 12:03
    '            ' Author    : joho
    '            ' Purpose   : Calculates the planned net budget for the bookingtype based on CPP
    '            '             and booked TRPs
    '            '---------------------------------------------------------------------------------------
    '            '

    '            On Error GoTo PlannedNetBudget_Error

    '            Dim TmpWeek As cWeek

    '            PlannedNetBudget = 0
    '            For Each TmpWeek In mvarWeeks
    '                PlannedNetBudget = PlannedNetBudget + TmpWeek.NetBudget
    '            Next

    '            On Error GoTo 0
    '            Exit Function

    'PlannedNetBudget_Error:

    '            Err.Raise(Err.Number, "cBookingType: PlannedNetBudget", Err.Description)

    '        End Function

    '        Public Function ActualNetBudget()
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : ActualNetBudget
    '            ' DateTime  : 2003-07-03 12:06
    '            ' Author    : joho
    '            ' Purpose   : Calculates the actual net budget based on CPP and delivered TRPs
    '            '---------------------------------------------------------------------------------------
    '            '

    '            On Error GoTo ActualNetBudget_Error

    '            ActualNetBudget = 0

    '            On Error GoTo 0
    '            Exit Function

    'ActualNetBudget_Error:

    '            Err.Raise(Err.Number, "cBookingType: ActualNetBudget", Err.Description)

    '        End Function

    '        Public Property Name() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Name
    '            ' DateTime  : 2003-07-10 15:31
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the name of the booking type
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Name_Error

    '                Name = mvarName

    '                On Error GoTo 0
    '                Exit Property

    'Name_Error:

    '                Err.Raise(Err.Number, "cBookingType: Name", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                Dim TmpBookingType As cBookingType

    '                On Error GoTo Name_Error

    '                If value <> mvarName Then
    '                    If Not ParentColl Is Nothing Then
    '                        If ParentColl.Contains(value) Then
    '                            Throw New System.Exception("Bookingtype already exists.")
    '                        Else
    '                            If ParentColl.Contains(mvarName) Then
    '                                TmpBookingType = ParentColl(mvarName)
    '                                ParentColl.Remove(mvarName)
    '                                If Not TmpBookingType Is Nothing Then
    '                                    ParentColl.Add(TmpBookingType, value)
    '                                End If
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '                mvarName = value

    '                On Error GoTo 0
    '                Exit Property

    'Name_Error:

    '                Err.Raise(Err.Number, "cBookingType: Name", Err.Description)

    '            End Set
    '        End Property

    '        Public Property Shortname() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Shortname
    '            ' DateTime  : 2003-07-10 15:31
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the abbrevation for the booking type
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Shortname_Error

    '                Shortname = mvarShortname

    '                On Error GoTo 0
    '                Exit Property

    'Shortname_Error:

    '                Err.Raise(Err.Number, "cBookingType: Shortname", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo Shortname_Error

    '                mvarShortname = value

    '                On Error GoTo 0
    '                Exit Property

    'Shortname_Error:

    '                Err.Raise(Err.Number, "cBookingType: Shortname", Err.Description)

    '            End Set
    '        End Property

    '        Public Property BuyingTarget() As cPricelistTarget
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : BuyingTarget
    '            ' DateTime  : 2003-07-10 15:31
    '            ' Author    : joho
    '            ' Purpose   : Pointer to the cPriceListTarget object containing the BuyingTarget
    '            '             for the BuyingType
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo BuyingTarget_Error

    '                Dim SaveUniSize As Boolean

    '                If Not mvarBuyingTarget Is Nothing Then
    '                    If mvarBuyingTarget.Target Is Nothing Then mvarBuyingTarget.Target = New cTarget(Main)
    '                    SaveUniSize = mvarBuyingTarget.Target.NoUniverseSize
    '                    mvarBuyingTarget.Target.NoUniverseSize = True
    '                    mvarBuyingTarget.Target.Universe = mvarParentChannel.BuyingUniverse
    '                    mvarBuyingTarget.Target.NoUniverseSize = SaveUniSize
    '                    mvarBuyingTarget.Bookingtype = Me
    '                Else
    '                    mvarBuyingTarget = New cPricelistTarget(Main)
    '                End If
    '                BuyingTarget = mvarBuyingTarget

    '                On Error GoTo 0
    '                Exit Property

    'BuyingTarget_Error:

    '                Err.Raise(Err.Number, "cBookingType: BuyingTarget", Err.Description)
    '            End Get
    '            Set(ByVal value As cPricelistTarget)
    '                On Error GoTo BuyingTarget_Error

    '                Dim TmpWeek As cWeek
    '                Dim TmpIndex As cIndex
    '                Dim i As Integer

    '                mvarBuyingTarget = value

    '                If Not mvarBuyingTarget Is Nothing Then
    '                    mvarGrossCPP = mvarBuyingTarget.CPP
    '                Else
    '                    mvarGrossCPP = 0
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'BuyingTarget_Error:

    '                Err.Raise(Err.Number, "cBookingType: BuyingTarget", Err.Description)


    '            End Set
    '        End Property

    '        Public Property IndexMainTarget() As Integer
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : IndexMainTarget
    '            ' DateTime  : 2003-07-10 15:31
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the _expected_ index between the buying target and
    '            '             the Main target
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo IndexMainTarget_Error

    '                IndexMainTarget = mvarIndexMainTarget

    '                On Error GoTo 0
    '                Exit Property

    'IndexMainTarget_Error:

    '                Err.Raise(Err.Number, "cBookingType: IndexMainTarget", Err.Description)
    '            End Get
    '            Set(ByVal value As Integer)
    '                On Error GoTo IndexMainTarget_Error

    '                mvarIndexMainTarget = value

    '                On Error GoTo 0
    '                Exit Property

    'IndexMainTarget_Error:

    '                Err.Raise(Err.Number, "cBookingType: IndexMainTarget", Err.Description)

    '            End Set
    '        End Property

    '        Public Property IndexSecondTarget() As Integer
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : IndexSecondTarget
    '            ' DateTime  : 2003-07-10 15:31
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the _expected_ index between the buying target and
    '            '             the Second target
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo IndexSecondTarget_Error

    '                IndexSecondTarget = mvarIndexSecondTarget

    '                On Error GoTo 0
    '                Exit Property

    'IndexSecondTarget_Error:

    '                Err.Raise(Err.Number, "cBookingType: IndexSecondTarget", Err.Description)

    '            End Get
    '            Set(ByVal value As Integer)
    '                On Error GoTo IndexSecondTarget_Error

    '                mvarIndexSecondTarget = value

    '                On Error GoTo 0
    '                Exit Property

    'IndexSecondTarget_Error:

    '                Err.Raise(Err.Number, "cBookingType: IndexSecondTarget", Err.Description)

    '            End Set
    '        End Property

    '        Public Property IndexAllAdults() As Integer
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : IndexAllAdults
    '            ' DateTime  : 2003-07-10 15:32
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the _expected_ index between the buying target and the
    '            '             entire TV-population
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo IndexAllAdults_Error

    '                IndexAllAdults = mvarIndexAllAdults

    '                On Error GoTo 0
    '                Exit Property

    'IndexAllAdults_Error:

    '                Err.Raise(Err.Number, "cBookingType: IndexAllAdults", Err.Description)
    '            End Get
    '            Set(ByVal value As Integer)
    '                On Error GoTo IndexAllAdults_Error

    '                mvarIndexAllAdults = value

    '                On Error GoTo 0
    '                Exit Property

    'IndexAllAdults_Error:

    '                Err.Raise(Err.Number, "cBookingType: IndexAllAdults", Err.Description)

    '            End Set
    '        End Property

    '        Public Property Weeks() As cWeeks
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Weeks
    '            ' DateTime  : 2003-07-10 15:32
    '            ' Author    : joho
    '            ' Purpose   : Pointer to the collection of cWeek objects containing data for each
    '            '             week
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Weeks_Error

    '                Weeks = mvarWeeks

    '                On Error GoTo 0
    '                Exit Property

    'Weeks_Error:

    '                Err.Raise(Err.Number, "cBookingType: Weeks", Err.Description)
    '            End Get
    '            Set(ByVal value As cWeeks)
    '                On Error GoTo Weeks_Error

    '                mvarWeeks = value
    '                mvarWeeks.Bookingtype = Me
    '                mvarWeeks.MainObject = Main

    '                On Error GoTo 0
    '                Exit Property

    'Weeks_Error:

    '                Err.Raise(Err.Number, "cBookingType: Weeks", Err.Description)

    '            End Set
    '        End Property

    '        Public Property BookIt() As Boolean
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : BookIt
    '            ' DateTime  : 2003-07-10 15:32
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets wether the BookingType is to be used. No bookingtype
    '            '             should be Removed, instead BookIt should be set to False.
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo BookIt_Error

    '                BookIt = mvarBookIt

    '                On Error GoTo 0
    '                Exit Property

    'BookIt_Error:

    '                Err.Raise(Err.Number, "cBookingType: BookIt", Err.Description)
    '            End Get
    '            Set(ByVal value As Boolean)
    '                On Error GoTo BookIt_Error

    '                mvarBookIt = value

    '                On Error GoTo 0
    '                Exit Property

    'BookIt_Error:

    '                Err.Raise(Err.Number, "cBookingType: BookIt", Err.Description)


    '            End Set
    '        End Property

    '        Public Property GrossCPP() As Single
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : GrossCPP
    '            ' DateTime  : 2003-07-10 15:32
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the Gross-CPP for this Buying type. GrossCPP is
    '            '             automatically updated when a new BuyingTarget is set if the optional
    '            '             parameter KeepGrossCPP is not set to False.
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo GrossCPP_Error

    '                GrossCPP = mvarGrossCPP

    '                On Error GoTo 0
    '                Exit Property

    'GrossCPP_Error:

    '                Err.Raise(Err.Number, "cBookingType: GrossCPP", Err.Description)
    '            End Get
    '            Set(ByVal value As Single)
    '                On Error GoTo GrossCPP_Error

    '                mvarGrossCPP = value

    '                On Error GoTo 0
    '                Exit Property

    'GrossCPP_Error:

    '                Err.Raise(Err.Number, "cBookingType: GrossCPP", Err.Description)

    '            End Set
    '        End Property

    '        Public Property AverageRating() As Single
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : AverageRating
    '            ' DateTime  : 2003-07-10 15:32
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the _expected_ TRP per spot for this Booking Type.
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo AverageRating_Error

    '                AverageRating = mvarAverageRating

    '                On Error GoTo 0
    '                Exit Property

    'AverageRating_Error:

    '                Err.Raise(Err.Number, "cBookingType: AverageRating", Err.Description)
    '            End Get
    '            Set(ByVal value As Single)
    '                On Error GoTo AverageRating_Error

    '                mvarAverageRating = value

    '                On Error GoTo 0
    '                Exit Property

    'AverageRating_Error:

    '                Err.Raise(Err.Number, "cBookingType: AverageRating", Err.Description)

    '            End Set
    '        End Property

    '        Public Property ConfirmedNetBudget() As Decimal
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : ConfirmedNetBudget
    '            ' DateTime  : 2003-07-10 15:32
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the Net Budget Confirmed by the channel in their
    '            '             booking confirmation.
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                Dim TmpSpot As cPlannedSpot
    '                Dim TmpBudget As Decimal
    '                On Error GoTo ConfirmedNetBudget_Error

    '                For Each TmpSpot In Main.PlannedSpots
    '                    If TmpSpot.Bookingtype Is Me Then
    '                        TmpBudget = TmpBudget + TmpSpot.PriceNet
    '                    End If
    '                Next
    '                If mvarConfirmedNetBudget > 0 Then
    '                    ConfirmedNetBudget = mvarConfirmedNetBudget
    '                Else
    '                    ConfirmedNetBudget = TmpBudget
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'ConfirmedNetBudget_Error:

    '                Err.Raise(Err.Number, "cBookingType: ConfirmedNetBudget", Err.Description)
    '            End Get
    '            Set(ByVal value As Decimal)
    '                On Error GoTo ConfirmedNetBudget_Error

    '                mvarConfirmedNetBudget = value
    '                mvarMarathonNetBudget = value

    '                On Error GoTo 0
    '                Exit Property

    'ConfirmedNetBudget_Error:

    '                Err.Raise(Err.Number, "cBookingType: ConfirmedNetBudget", Err.Description)

    '            End Set
    '        End Property

    '        Public ReadOnly Property ConfirmedGrossBudget() As Decimal
    '            Get
    '                Dim TmpSpot As cPlannedSpot
    '                Dim TmpBudget As Decimal
    '                On Error GoTo ConfirmedGrossBudget_Error

    '                For Each TmpSpot In Main.PlannedSpots
    '                    If TmpSpot.Bookingtype Is Me Then
    '                        TmpBudget = TmpBudget + TmpSpot.PriceGross
    '                    End If
    '                Next
    '                If TmpBudget = 0 Then
    '                    If PlannedNetBudget() > 0 AndAlso PlannedGrossBudget > 0 Then
    '                        ConfirmedGrossBudget = mvarConfirmedNetBudget / (Format(PlannedNetBudget, "0") / Format(PlannedGrossBudget, "0"))
    '                    Else
    '                        ConfirmedGrossBudget = 0
    '                    End If
    '                Else
    '                    ConfirmedGrossBudget = TmpBudget
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'ConfirmedGrossBudget_Error:

    '                Err.Raise(Err.Number, "cBookingType: ConfirmedGrossBudget", Err.Description)
    '            End Get

    '        End Property

    '        Public Property Bookingtype() As Byte
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : BookingType
    '            ' DateTime  : 2003-07-10 15:32
    '            ' Author    : joho
    '            ' Purpose   : Returns/set the index number indicating what Bookingtype this
    '            '             should be counted as:
    '            '
    '            '               0  - RBS
    '            '               1  - Specific
    '            '               2  - Last minute
    '            '               3> - User specified
    '            '---------------------------------------------------------------------------------------
    '            Get
    '                On Error GoTo BookingType_Error

    '                Bookingtype = mvarBookingtype

    '                On Error GoTo 0
    '                Exit Property

    'BookingType_Error:

    '                Err.Raise(Err.Number, "cBookingType: BookingType", Err.Description)

    '            End Get
    '            Set(ByVal value As Byte)
    '                On Error GoTo BookingType_Error

    '                mvarBookingtype = value

    '                On Error GoTo 0
    '                Exit Property

    'BookingType_Error:

    '                Err.Raise(Err.Number, "cBookingType: BookingType", Err.Description)

    '            End Set
    '        End Property

    '        Public Property ContractNumber() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : ContractNumber
    '            ' DateTime  : 2003-07-10 15:32
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the ContractNumber according to the booking confirmation
    '            '             from the channel.
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo ContractNumber_Error

    '                ContractNumber = mvarContractNumber

    '                On Error GoTo 0
    '                Exit Property

    'ContractNumber_Error:

    '                Err.Raise(Err.Number, "cBookingType: ContractNumber", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo ContractNumber_Error

    '                mvarContractNumber = value

    '                On Error GoTo 0
    '                Exit Property

    'ContractNumber_Error:

    '                Err.Raise(Err.Number, "cBookingType: ContractNumber", Err.Description)

    '            End Set
    '        End Property

    '        Public Property OrderNumber() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : OrderNumber
    '            ' DateTime  : 2003-07-10 15:32
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the OrderNumber according to Marathon
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo OrderNumber_Error

    '                OrderNumber = mvarOrderNumber

    '                On Error GoTo 0
    '                Exit Property

    'OrderNumber_Error:

    '                Err.Raise(Err.Number, "cBookingType: OrderNumber", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo OrderNumber_Error

    '                mvarOrderNumber = value

    '                On Error GoTo 0
    '                Exit Property

    'OrderNumber_Error:

    '                Err.Raise(Err.Number, "cBookingType: OrderNumber", Err.Description)

    '            End Set
    '        End Property

    '        Public Property Pricelist() As cPricelist
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : PriceList
    '            ' DateTime  : 2003-07-10 15:32
    '            ' Author    : joho
    '            ' Purpose   : Pointer to the cPricelist object containing the Pricelist. The
    '            '             pricelist is read when the Booking type is created.
    '            '             See Class.Initialize
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo PriceList_Error

    '                'Set mvarPriceList.MainObject = Main
    '                mvarPriceList.Bookingtype = Me
    '                Pricelist = mvarPriceList

    '                On Error GoTo 0
    '                Exit Property

    'PriceList_Error:

    '                Err.Raise(Err.Number, "cBookingType: PriceList", Err.Description)
    '            End Get
    '            Set(ByVal value As cPricelist)
    '                On Error GoTo PriceList_Error

    '                mvarPriceList = value

    '                On Error GoTo 0
    '                Exit Property

    'PriceList_Error:

    '                Err.Raise(Err.Number, "cBookingType: PriceList", Err.Description)

    '            End Set
    '        End Property

    '        Public Property IsVisible() As Boolean
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : IsVisible
    '            ' DateTime  : 2003-07-10 15:33
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets wether this BookingType should be visible in the charts
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo IsVisible_Error

    '                IsVisible = mvarIsVisible

    '                On Error GoTo 0
    '                Exit Property

    'IsVisible_Error:

    '                Err.Raise(Err.Number, "cBookingType: IsVisible", Err.Description)
    '            End Get
    '            Set(ByVal value As Boolean)
    '                On Error GoTo IsVisible_Error

    '                mvarIsVisible = value

    '                On Error GoTo 0
    '                Exit Property

    'IsVisible_Error:

    '                Err.Raise(Err.Number, "cBookingType: IsVisible", Err.Description)

    '            End Set
    '        End Property

    '        Public Property IsRBS() As Boolean
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : IsRBS
    '            ' DateTime  : 2003-07-10 15:33
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets wether this booking tyoe should be regarded as a RBS
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo IsRBS_Error

    '                IsRBS = mvarIsRBS

    '                On Error GoTo 0
    '                Exit Property

    'IsRBS_Error:

    '                Err.Raise(Err.Number, "cBookingType: IsRBS", Err.Description)
    '            End Get
    '            Set(ByVal value As Boolean)
    '                On Error GoTo IsRBS_Error

    '                mvarIsRBS = value

    '                On Error GoTo 0
    '                Exit Property

    'IsRBS_Error:

    '                Err.Raise(Err.Number, "cBookingType: IsRBS", Err.Description)


    '            End Set
    '        End Property

    '        Public Property IsSpecific() As Boolean
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : IsSpecific
    '            ' DateTime  : 2003-07-10 15:33
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets wether this BookingType should be regarded as a specific
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo IsSpecific_Error

    '                IsSpecific = mvarIsSpecific

    '                On Error GoTo 0
    '                Exit Property

    'IsSpecific_Error:

    '                Err.Raise(Err.Number, "cBookingType: IsSpecific", Err.Description)
    '            End Get
    '            Set(ByVal value As Boolean)
    '                On Error GoTo IsSpecific_Error

    '                mvarIsSpecific = value

    '                On Error GoTo 0
    '                Exit Property

    'IsSpecific_Error:

    '                Err.Raise(Err.Number, "cBookingType: IsSpecific", Err.Description)

    '            End Set
    '        End Property

    '        Public Sub ReadPricelist(Optional ByVal Area As String = "")
    '            Dim XMLDoc As New Xml.XmlDocument
    '            Dim XMLBTPrice As Xml.XmlElement
    '            Dim XMLTmpNode As Xml.XmlElement
    '            Dim XMLTmpNode2 As Xml.XmlElement
    '            Dim TmpTarget As cPricelistTarget
    '            Dim TmpIndex As cIndex
    '            Dim i As Integer

    '            If Area = "" Then
    '                If My.Computer.FileSystem.FileExists(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Main.Area & "\Pricelists\" + mvarParentChannel.ChannelName & ".xml") Then
    '                    Helper.WriteToLogFile("Reading pricelist from " & TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Main.Area & "\Pricelists\" + mvarParentChannel.ChannelName & ".xml")
    '                    XMLDoc.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Main.Area & "\Pricelists\" + mvarParentChannel.ChannelName & ".xml")
    '                Else
    '                    Helper.WriteToLogFile("Could not read pricelist for " & ToString() & ". Area was nothing.")
    '                    Exit Sub
    '                End If
    '            Else
    '                If My.Computer.FileSystem.FileExists(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Area & "\Pricelists\" & mvarParentChannel.ChannelName & ".xml") Then
    '                    Helper.WriteToLogFile("Reading pricelist from " & TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Area & "\Pricelists\" & mvarParentChannel.ChannelName & ".xml")
    '                    XMLDoc.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Area & "\Pricelists\" & mvarParentChannel.ChannelName & ".xml")
    '                Else
    '                    Helper.WriteToLogFile("Could not read pricelist for " & ToString() & ". Area was " & Area & ".")
    '                    Exit Sub
    '                End If
    '            End If
    '            If XMLDoc.OuterXml = "" Then Exit Sub
    '            XMLBTPrice = XMLDoc.GetElementsByTagName("Pricelist").Item(0).SelectSingleNode("Price[@Name='" & mvarName & "']")
    '            If XMLBTPrice Is Nothing Then Exit Sub

    '            mvarAverageRating = XMLBTPrice.GetAttribute("AverageRating")
    '            XMLTmpNode = XMLBTPrice.FirstChild.FirstChild
    '            While Not XMLTmpNode Is Nothing
    '                TmpTarget = mvarPriceList.Targets.Add(XMLTmpNode.GetAttribute("Target"))
    '                TmpTarget.CalcCPP = XMLTmpNode.GetAttribute("CalcCPP")
    '                For i = 1 To Main.DaypartCount
    '                    TmpTarget.DefaultDaypart(i - 1) = XMLBTPrice.GetAttribute("DP" & i)
    '                Next
    '                If Not System.DBNull.Value Is (XMLTmpNode.GetAttribute("CPP")) Then
    '                    TmpTarget.CPP = Val(XMLTmpNode.GetAttribute("CPP"))
    '                End If
    '                TmpTarget.CalcCPP = False
    '                For i = 1 To Main.DaypartCount
    '                    If Not XMLTmpNode.GetAttribute("CPP_DP" & i) Is System.DBNull.Value Then
    '                        TmpTarget.CPPDaypart(i - 1) = XMLTmpNode.GetAttribute("CPP_DP" & i)
    '                    End If
    '                Next
    '                TmpTarget.CalcCPP = XMLTmpNode.GetAttribute("CalcCPP")
    '                TmpTarget.StandardTarget = XMLTmpNode.GetAttribute("StandardTarget")
    '                TmpTarget.Target.NoUniverseSize = True
    '                If XMLTmpNode.GetAttribute("AdEdgeTarget") Is Nothing OrElse XMLTmpNode.GetAttribute("AdEdgeTarget") = "" Then
    '                    TmpTarget.Target.TargetName = XMLTmpNode.GetAttribute("AdedgeTarget")
    '                Else
    '                    TmpTarget.Target.TargetName = XMLTmpNode.GetAttribute("AdEdgeTarget")
    '                End If
    '                'TmpTarget.Target.NoUniverseSize = False
    '                TmpTarget.UniSize = XMLTmpNode.GetAttribute("TargetUni")
    '                TmpTarget.UniSizeNat = XMLTmpNode.GetAttribute("TargetNat")
    '                XMLTmpNode2 = XMLTmpNode.FirstChild
    '                While Not XMLTmpNode2 Is Nothing
    '                    TmpIndex = TmpTarget.Indexes.Add(XMLTmpNode2.GetAttribute("Name"))
    '                    TmpIndex.FromDate = XMLTmpNode2.GetAttribute("FromDate")
    '                    TmpIndex.ToDate = XMLTmpNode2.GetAttribute("ToDate")
    '                    If XMLTmpNode2.GetAttribute("IndexDP0") = "" Then
    '                        TmpIndex.Index = XMLTmpNode2.GetAttribute("Index")
    '                    Else
    '                        For i = 0 To Main.DaypartCount - 1
    '                            TmpIndex.Index(i) = XMLTmpNode2.GetAttribute("IndexDP" & i)
    '                        Next
    '                    End If
    '                    TmpIndex.IndexOn = cIndex.IndexOnEnum.eGrossCPP
    '                    TmpIndex.SystemGenerated = True
    '                    XMLTmpNode2 = XMLTmpNode2.NextSibling
    '                End While
    '                XMLTmpNode = XMLTmpNode.NextSibling
    '            End While
    '            Helper.WriteToLogFile("Found " & mvarPriceList.Targets.Count & " targets")
    '        End Sub

    '        Public Function TotalTRP(Optional ByVal IncludeCompensation As Boolean = True) As Single

    '            Dim TmpWeek As cWeek
    '            Dim TmpTRP As Single = 0

    '            For Each TmpWeek In mvarWeeks
    '                TmpTRP = TmpTRP + TmpWeek.TRP
    '            Next
    '            If IncludeCompensation Then
    '                For Each TmpComp As Trinity.cCompensation In mvarCompensations
    '                    TmpTRP += TmpComp.TRPMainTarget
    '                Next
    '            End If
    '            Return TmpTRP
    '        End Function

    '        Public Function TotalTRPBuyingTarget() As Single

    '            Dim TmpWeek As cWeek

    '            For Each TmpWeek In mvarWeeks
    '                TotalTRPBuyingTarget = TotalTRPBuyingTarget + TmpWeek.TRPBuyingTarget
    '            Next
    '        End Function

    '        Public Property EstimatedSpotCount() As Integer
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : EstimatedSpotCount
    '            ' DateTime  : 2003-08-21 15:00
    '            ' Author    : joho
    '            ' Purpose   : Returns/Sets the estimated number of spots for this BookingType
    '            '             When set, the new AverageRating is calculated
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo EstimatedSpotCount_Error

    '                If mvarAverageRating <> 0 Then
    '                    EstimatedSpotCount = TotalTRP() / mvarAverageRating
    '                Else
    '                    EstimatedSpotCount = 0
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'EstimatedSpotCount_Error:

    '                Err.Raise(Err.Number, "cBookingType: EstimatedSpotCount", Err.Description)
    '            End Get
    '            Set(ByVal value As Integer)
    '                On Error GoTo EstimatedSpotCount_Error

    '                If value <> 0 Then
    '                    mvarAverageRating = TotalTRP() / value
    '                Else
    '                    mvarAverageRating = 0
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'EstimatedSpotCount_Error:

    '                Err.Raise(Err.Number, "cBookingType: EstimatedSpotCount", Err.Description)


    '            End Set
    '        End Property

    '        Public ReadOnly Property ConfirmedSpotCount() As Integer
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : ConfirmedSpotCount
    '            ' DateTime  : 2003-08-21 15:00
    '            ' Author    : joho
    '            ' Purpose   : Returns the number of spots that has been confirmed for this BookingType
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo ConfirmedSpotCount_Error

    '                Dim TmpSpot As cPlannedSpot

    '                For Each TmpSpot In Main.PlannedSpots

    '                    If TmpSpot.Bookingtype Is Me Then
    '                        ConfirmedSpotCount = ConfirmedSpotCount + 1
    '                    End If

    '                Next
    '                On Error GoTo 0
    '                Exit Property

    'ConfirmedSpotCount_Error:

    '                Err.Raise(Err.Number, "cBookingType: ConfirmedSpotCount", Err.Description)
    '            End Get

    '        End Property

    '        Public ReadOnly Property PlannedGrossBudget()
    '            Get
    '                Dim TmpWeek As cWeek
    '                Dim GB As Decimal

    '                GB = 0
    '                For Each TmpWeek In mvarWeeks
    '                    GB = GB + TmpWeek.GrossBudget
    '                Next
    '                Return GB
    '            End Get
    '        End Property

    '        Public Function PlannedTRP30(ByVal Target As cPlannedSpot.PlannedTargetEnum) As Single

    '            Dim TmpWeek As cWeek
    '            Dim TRPSum As Single

    '            If Target = cPlannedSpot.PlannedTargetEnum.pteMainTarget Then

    '                For Each TmpWeek In mvarWeeks
    '                    TRPSum = TRPSum + TmpWeek.TRP * (TmpWeek.SpotIndex / 100)
    '                Next
    '                PlannedTRP30 = TRPSum

    '            ElseIf Target = cPlannedSpot.PlannedTargetEnum.pteBuyingTarget Then

    '                For Each TmpWeek In mvarWeeks
    '                    TRPSum = TRPSum + TmpWeek.TRPBuyingTarget * (TmpWeek.SpotIndex / 100)
    '                Next
    '                PlannedTRP30 = TRPSum

    '            End If

    '        End Function

    '        Public Function PlannedTRP(ByVal Target As cPlannedSpot.PlannedTargetEnum) As Single

    '            Dim TmpWeek As cWeek
    '            Dim TRPSum As Single

    '            If Target = cPlannedSpot.PlannedTargetEnum.pteMainTarget Then

    '                For Each TmpWeek In mvarWeeks
    '                    TRPSum = TRPSum + TmpWeek.TRP
    '                Next
    '                Return TRPSum

    '            ElseIf Target = cPlannedSpot.PlannedTargetEnum.pteBuyingTarget Then

    '                For Each TmpWeek In mvarWeeks
    '                    TRPSum = TRPSum + TmpWeek.TRPBuyingTarget
    '                Next
    '                Return TRPSum

    '            End If

    '        End Function

    '        Public Function SpotIndex() As Single

    '            Dim TmpWeek As cWeek
    '            Dim TmpFilm As cFilm
    '            Dim TRPs() As Single
    '            Dim TRPSum As Single
    '            Dim x As Integer
    '            Dim TmpIndex As Single

    '            ReDim TRPs(mvarWeeks(1).Films.Count)

    '            For Each TmpWeek In mvarWeeks
    '                x = 1
    '                For Each TmpFilm In TmpWeek.Films
    '                    TRPs(x) = TRPs(x) + TmpWeek.TRP * (TmpFilm.Share / 100)
    '                    TRPSum = TRPSum + TmpWeek.TRP * (TmpFilm.Share / 100)
    '                    x = x + 1
    '                Next
    '            Next
    '            TmpIndex = 0
    '            For x = 1 To mvarWeeks(1).Films.Count
    '                If TRPSum > 0 Then
    '                    TmpIndex = TmpIndex + (TRPs(x) / TRPSum) * mvarWeeks(1).Films(x).Index
    '                Else
    '                    TmpIndex = 0
    '                End If
    '            Next
    '            SpotIndex = TmpIndex / 100
    '        End Function

    '        Public Property Indexes() As cIndexes
    '            Get
    '                Indexes = mvarIndexes
    '            End Get
    '            Set(ByVal value As cIndexes)
    '                mvarIndexes = value
    '            End Set
    '        End Property

    '        Public Property AddedValues() As cAddedValues
    '            Get
    '                mvarAddedValues.Bookingtype = Me
    '                AddedValues = mvarAddedValues
    '            End Get
    '            Set(ByVal value As cAddedValues)
    '                mvarAddedValues = value
    '            End Set
    '        End Property

    '        Public Property FilmIndex(ByVal Length As Integer) As Single
    '            Get
    '                FilmIndex = mvarFilmIndex(Length)
    '            End Get
    '            Set(ByVal value As Single)
    '                mvarFilmIndex(Length) = value
    '            End Set
    '        End Property

    '        Public Function GetWeek(ByVal d As Date) As cWeek
    '            Dim TmpWeek As cWeek

    '            GetWeek = Nothing
    '            For Each TmpWeek In mvarWeeks
    '                If TmpWeek.StartDate <= Int(d.ToOADate) Then
    '                    If TmpWeek.EndDate >= Int(d.ToOADate) Then
    '                        GetWeek = TmpWeek
    '                        Exit For
    '                    End If
    '                End If
    '            Next
    '        End Function

    '        Public Property MarathonNetBudget() As Decimal
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : MarathonNetBudget
    '            ' DateTime  : 2003-07-10 15:32
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the Net Budget Confirmed by the channel in their
    '            '             booking confirmation.
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo MarathonNetBudget_Error

    '                MarathonNetBudget = mvarMarathonNetBudget

    '                On Error GoTo 0
    '                Exit Property

    'MarathonNetBudget_Error:

    '                Err.Raise(Err.Number, "cBookingType: MarathonNetBudget", Err.Description)
    '            End Get
    '            Set(ByVal value As Decimal)
    '                On Error GoTo MarathonNetBudget_Error

    '                mvarMarathonNetBudget = value

    '                On Error GoTo 0
    '                Exit Property

    'MarathonNetBudget_Error:

    '                Err.Raise(Err.Number, "cBookingType: MarathonNetBudget", Err.Description)

    '            End Set
    '        End Property

    '        Public Sub New(ByVal Main As cKampanj)
    '            mvarIndexAllAdults = 100
    '            mvarIndexMainTarget = 100
    '            mvarIndexSecondTarget = 100
    '            mvarIsVisible = True
    '            mvarPriceList = New cPricelist(Main)
    '            mvarBuyingTarget = New cPricelistTarget(Main)
    '            mvarBuyingTarget.Bookingtype = Me
    '            MainObject = Main
    '            mvarWeeks = New cWeeks(Main)
    '            mvarWeeks.Bookingtype = Me
    '        End Sub

    '        Protected Overrides Sub Finalize()
    '            mvarBuyingTarget = Nothing

    '            mvarWeeks = Nothing
    '            mvarPriceList = Nothing
    '            mvarParentChannel = Nothing
    '            mvarIndexes = Nothing
    '            MyBase.Finalize()
    '        End Sub

    '    End Class

    '    Public Class cBookingTypes
    '        Implements Collections.IEnumerable

    '        'local variable to hold collection
    '        Private mCol As Collection
    '        Private Main As cKampanj
    '        Private mvarParentChannel As cChannel


    '        Friend Property ParentChannel() As cChannel
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : ParentChannel
    '            ' DateTime  : 2003-08-15 16:18
    '            ' Author    : joho
    '            ' Purpose   :
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo ParentChannel_Error

    '                ParentChannel = mvarParentChannel

    '                On Error GoTo 0
    '                Exit Property

    'ParentChannel_Error:

    '                Err.Raise(Err.Number, "cBookingType: ParentChannel", Err.Description)
    '            End Get
    '            Set(ByVal value As cChannel)
    '                On Error GoTo ParentChannel_Error

    '                mvarParentChannel = value
    '                Dim TmpBookingType As cBookingType

    '                For Each TmpBookingType In mCol
    '                    If TmpBookingType.ParentChannel Is Nothing Then
    '                        TmpBookingType.ParentChannel = value
    '                    End If
    '                Next

    '                On Error GoTo 0
    '                Exit Property

    'ParentChannel_Error:

    '                Err.Raise(Err.Number, "cBookingType: ParentChannel", Err.Description)


    '            End Set
    '        End Property

    '        Friend Property MainObject()
    '            Get
    '                MainObject = Main
    '            End Get
    '            Set(ByVal value)
    '                Dim TmpBookingType As cBookingType

    '                Main = value
    '                For Each TmpBookingType In mCol
    '                    TmpBookingType.MainObject = value
    '                Next
    '            End Set
    '        End Property

    '        Public Overloads Function Add(ByVal Name As String, Optional ByVal ReadPricelist As Boolean = False) As cBookingType

    '            'create a new object
    '            Dim objNewMember As cBookingType

    '            On Error GoTo Add_Error

    '            objNewMember = New cBookingType(MainObject)

    '            'set the properties passed into the method
    '            Dim Msg As String
    '            Helper.WriteToLogFile("cBookingTypes.Add : SetObjects")

    '            objNewMember.MainObject = Main
    '            objNewMember.ParentCollection = mCol
    '            objNewMember.ParentChannel = mvarParentChannel

    '            objNewMember.Name = Name

    '            If ReadPricelist Then
    '                Helper.WriteToLogFile("cBookingTypes.Add : ReadPricelist")
    '                objNewMember.ReadPricelist()
    '            End If

    '            Helper.WriteToLogFile("cBookingTypes.Add : Add To Collection")
    '            mCol.Add(objNewMember, Name)


    '            'return the object created
    '            Add = objNewMember
    '            objNewMember = Nothing


    '            On Error GoTo 0
    '            Exit Function

    'Add_Error:

    '            If Err.Number = 457 Then
    '                Add = mCol(Name)
    '                Exit Function
    '            End If
    '            Helper.WriteToLogFile("ERROR: " & Err.Description)
    '            Err.Raise(Err.Number, "cBookingTypes: Add", Err.Description)


    '        End Function

    '        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cBookingType

    '            'used when referencing an element in the collection
    '            'vntIndexKey contains either the Index or Key to the collection,
    '            'this is why it is declared as a Variant
    '            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
    '            Get
    '                On Error GoTo ErrHandle
    '                Item = mCol(vntIndexKey)
    '                Exit Property

    'ErrHandle:
    '                If vntIndexKey = 1 And mCol.Count > 0 Then
    '                    Item = mCol(vntIndexKey)
    '                End If
    '                Err.Raise(Err.Number, "cBookingTypes", "Unknown Bookingtype: " & vntIndexKey)
    '            End Get
    '        End Property

    '        Public Overloads ReadOnly Property Count() As Long
    '            Get
    '                'used when retrieving the number of elements in the
    '                'collection. Syntax: Debug.Print x.Count
    '                Count = mCol.Count
    '            End Get
    '        End Property

    '        Public Overloads Sub Remove(ByVal vntIndexKey As Object)
    '            'used when removing an element from the collection
    '            'vntIndexKey contains either the Index or Key, which is why
    '            'it is declared as a Variant
    '            'Syntax: x.Remove(xyz)

    '            mCol.Remove(vntIndexKey)

    '        End Sub

    '        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
    '            GetEnumerator = mCol.GetEnumerator()
    '        End Function

    '        Public Sub New(ByVal Main As cKampanj)
    '            mCol = New Collection
    '            MainObject = Main
    '        End Sub

    '        Protected Overrides Sub Finalize()
    '            mCol = Nothing
    '            MyBase.Finalize()
    '        End Sub

    '    End Class

    '    Public Class cPlannedSpot

    '        Public Enum EstEnum
    '            EstNotOk = 0
    '            EstSemiOk = 1
    '            EstOk = 2
    '        End Enum

    '        Private mvarChannel As cChannel
    '        Private mvarChannelID As String
    '        Private mvarAirDate As Long
    '        Private mvarMaM As Integer
    '        Private mvarProgBefore As String
    '        Private mvarProgAfter As String
    '        Private mvarProgramme As String
    '        Private mvarAdvertiser As String
    '        Private mvarProduct As String
    '        Private mvarFilmcode As String
    '        Private mvarFilm As cFilm
    '        Private mvarMyRating As Single
    '        Private mvarIndex As Integer
    '        Private mvarMatchedSpot As cActualSpot
    '        Private mvarMatchedBookedSpot As cBookedSpot
    '        Private mvarSpotLength As Byte
    '        Private mvarSpotType As Byte
    '        Private mvarWeek As cWeek
    '        Private mvarBookingtype As cBookingType
    '        Private mvarPriceNet As Decimal
    '        Private mvarPriceGross As Decimal
    '        Private mvarID As String
    '        Private mvarRating As Single
    '        Private Main As cKampanj
    '        Private mvarEstimation As EstEnum
    '        Private mvarRemark As String
    '        Public Matched As Byte
    '        Private mvarPlacement As PlaceEnum

    '        Public Enum PlaceEnum
    '            PlaceAny = 1
    '            PlaceTop = 2
    '            PlaceTail = 4
    '            PlaceTopOrTail = 8
    '            PlaceCentreBreak = 16
    '            PlaceStartBreak = 32
    '            PlaceEndBreak = 64
    '            PlaceRoadBlock = 128
    '            PlaceRequestedBreak = 256
    '            PlaceSecond = 512
    '            PlaceSecondLast = 1024
    '        End Enum

    '        Public BreakList As Collection

    '        Public Enum PlannedTargetEnum
    '            pteMainTarget = 0
    '            pteSecondTarget = 1
    '            pteThirdTarget = 2
    '            pteAllAdults = 3
    '            pteBuyingTarget = 4
    '        End Enum

    '        Friend WriteOnly Property MainObject()
    '            Set(ByVal value)
    '                Main = value
    '            End Set
    '        End Property

    '        Function DateTimeSerial() As Single
    '            Return Helper.DateTimeSerial(Date.FromOADate(AirDate), MaM)
    '        End Function

    '        Friend Property ID() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : ID
    '            ' DateTime  : 2003-07-18 10:06
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the ID to be used when saving relations between spots
    '            '             to file
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo ID_Error

    '                ID = mvarID

    '                On Error GoTo 0
    '                Exit Property

    'ID_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: ID", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo ID_Error

    '                mvarID = value

    '                On Error GoTo 0
    '                Exit Property

    'ID_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: ID", Err.Description)

    '            End Set
    '        End Property

    '        Friend Property ChannelID() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : ChannelID
    '            ' DateTime  : 2003-07-02 14:39
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the ChannelID. If a ChannelID is set the appropriate
    '            '             channel will be mapped to the Channel property
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo ChannelID_Error

    '                ChannelID = mvarChannelID

    '                On Error GoTo 0
    '                Exit Property

    'ChannelID_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: ChannelID", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo ChannelID_Error

    '                mvarChannelID = value

    '                On Error GoTo 0
    '                Exit Property

    'ChannelID_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: ChannelID", Err.Description)

    '            End Set
    '        End Property

    '        Friend Property Filmcode() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Filmcode
    '            ' DateTime  : 2003-07-02 14:48
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the Filmcode. If a Filmcode is set the appropriate film
    '            '             will be mapped to the Film Property
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Filmcode_Error

    '                Filmcode = mvarFilmcode

    '                On Error GoTo 0
    '                Exit Property

    'Filmcode_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Filmcode", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo Filmcode_Error

    '                mvarFilmcode = value


    '                On Error GoTo 0
    '                Exit Property

    'Filmcode_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Filmcode", Err.Description)


    '            End Set
    '        End Property

    '        Public Property Week() As cWeek
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Week
    '            ' DateTime  : 2003-07-15 12:26
    '            ' Author    : joho
    '            ' Purpose   : Pointer to a cWeek representing the week that this spot will be
    '            '             aired
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Week_Error

    '                Week = mvarWeek

    '                On Error GoTo 0
    '                Exit Property

    'Week_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Week", Err.Description)
    '            End Get
    '            Set(ByVal value As cWeek)
    '                On Error GoTo Week_Error

    '                mvarWeek = value

    '                On Error GoTo 0
    '                Exit Property

    'Week_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Week", Err.Description)
    '            End Set
    '        End Property

    '        Public Property AirDate() As Long
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : AirDate
    '            ' DateTime  : 2003-07-15 12:27
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the date when the spot is due to be aired
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo AirDate_Error

    '                AirDate = mvarAirDate

    '                On Error GoTo 0
    '                Exit Property

    'AirDate_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: AirDate", Err.Description)
    '            End Get
    '            Set(ByVal value As Long)
    '                On Error GoTo AirDate_Error

    '                mvarAirDate = value


    '                On Error GoTo 0
    '                Exit Property

    'AirDate_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: AirDate", Err.Description)

    '            End Set
    '        End Property

    '        Public Property Bookingtype() As cBookingType
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : BookingType
    '            ' DateTime  : 2003-07-15 12:51
    '            ' Author    : joho
    '            ' Purpose   : Pointer to a cBookingType representing the Booking Type this spot
    '            '             is a part of
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo BookingType_Error

    '                Bookingtype = mvarBookingtype

    '                On Error GoTo 0
    '                Exit Property

    'BookingType_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: BookingType", Err.Description)
    '            End Get
    '            Set(ByVal value As cBookingType)
    '                On Error GoTo BookingType_Error

    '                mvarBookingtype = value

    '                On Error GoTo 0
    '                Exit Property

    'BookingType_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: BookingType", Err.Description)

    '            End Set
    '        End Property

    '        Public Property Channel() As cChannel
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Channel
    '            ' DateTime  : 2003-07-18 09:49
    '            ' Author    : joho
    '            ' Purpose   : Pointer to a cChannel representing the channel where this spot is
    '            '             planned
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Channel_Error


    '                Channel = mvarChannel

    '                On Error GoTo 0
    '                Exit Property

    'Channel_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Channel", Err.Description)
    '            End Get
    '            Set(ByVal value As cChannel)

    '                On Error GoTo Channel_Error

    '                mvarChannel = value

    '                On Error GoTo 0
    '                Exit Property

    'Channel_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Channel", Err.Description)

    '            End Set
    '        End Property

    '        Public Property MaM() As Integer
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : MaM
    '            ' DateTime  : 2003-07-18 09:49
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the Minute after Midnight that this spot will be aired
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo MaM_Error

    '                MaM = mvarMaM

    '                On Error GoTo 0
    '                Exit Property

    'MaM_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: MaM", Err.Description)
    '            End Get
    '            Set(ByVal value As Integer)

    '                On Error GoTo MaM_Error

    '                mvarMaM = value

    '                On Error GoTo 0
    '                Exit Property

    'MaM_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: MaM", Err.Description)

    '            End Set
    '        End Property

    '        Public Property ProgBefore() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : ProgBefore
    '            ' DateTime  : 2003-07-18 09:49
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the programme to be aired before the break where this
    '            '             spot will be placed
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo ProgBefore_Error

    '                ProgBefore = mvarProgBefore

    '                On Error GoTo 0
    '                Exit Property

    'ProgBefore_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: ProgBefore", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo ProgBefore_Error

    '                mvarProgBefore = value

    '                On Error GoTo 0
    '                Exit Property

    'ProgBefore_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: ProgBefore", Err.Description)


    '            End Set
    '        End Property

    '        Public Property ProgAfter() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : ProgAfter
    '            ' DateTime  : 2003-07-18 09:49
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the programme to be aired after the break where this
    '            '             spot will be aired
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo ProgAfter_Error

    '                ProgAfter = mvarProgAfter

    '                On Error GoTo 0
    '                Exit Property

    'ProgAfter_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: ProgAfter", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo ProgAfter_Error

    '                mvarProgAfter = value

    '                On Error GoTo 0
    '                Exit Property

    'ProgAfter_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: ProgAfter", Err.Description)

    '            End Set
    '        End Property

    '        Public Property Programme() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Programme
    '            ' DateTime  : 2003-07-18 09:49
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the programme that this spot is considered to belong
    '            '             to. Most commonly the same as ProgAfter.
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Programme_Error

    '                Programme = mvarProgramme

    '                On Error GoTo 0
    '                Exit Property

    'Programme_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Programme", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo Programme_Error

    '                mvarProgramme = value

    '                On Error GoTo 0
    '                Exit Property

    'Programme_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Programme", Err.Description)


    '            End Set
    '        End Property

    '        Public Property Advertiser() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Advertiser
    '            ' DateTime  : 2003-07-18 09:49
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the name of the Advertiser for this spot
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Advertiser_Error

    '                Advertiser = mvarAdvertiser

    '                On Error GoTo 0
    '                Exit Property

    'Advertiser_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Advertiser", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo Advertiser_Error

    '                mvarAdvertiser = value

    '                On Error GoTo 0
    '                Exit Property

    'Advertiser_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Advertiser", Err.Description)

    '            End Set
    '        End Property

    '        Public Property Product() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Product
    '            ' DateTime  : 2003-07-18 09:50
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the name of the Product for this spot
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Product_Error

    '                Product = mvarProduct

    '                On Error GoTo 0
    '                Exit Property

    'Product_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Product", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo Product_Error

    '                mvarProduct = value

    '                On Error GoTo 0
    '                Exit Property

    'Product_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Product", Err.Description)

    '            End Set
    '        End Property

    '        Public ReadOnly Property Film() As cFilm
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Film
    '            ' DateTime  : 2003-07-18 09:50
    '            ' Author    : joho
    '            ' Purpose   : Pointer to a cFilm corresponding to the film to be aired
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Film_Error

    '                Film = mvarBookingtype.Weeks(1).Films(mvarFilmcode)

    '                On Error GoTo 0
    '                Exit Property

    'Film_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Film", Err.Description)
    '            End Get
    '            '            Set(ByVal value As cFilm)
    '            '                On Error GoTo Film_Error

    '            '                mvarFilm = value

    '            '                On Error GoTo 0
    '            '                Exit Property

    '            'Film_Error:

    '            '                Err.Raise(Err.Number, "cPlannedSpot: Film", Err.Description)

    '            '            End Set
    '        End Property

    '        Public Property MyRating(Optional ByVal Target As PlannedTargetEnum = 0) As Single
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : MyRating
    '            ' DateTime  : 2003-07-18 09:50
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the users own rating in the main target and universe
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                Dim UI As Single

    '                On Error GoTo MyRating_Error

    '                If Target = PlannedTargetEnum.pteMainTarget Then
    '                    MyRating = mvarMyRating
    '                ElseIf Target = PlannedTargetEnum.pteBuyingTarget Then
    '                    If ChannelRating(PlannedTargetEnum.pteBuyingTarget) > 0 Then
    '                        UI = (mvarMyRating / ChannelRating(PlannedTargetEnum.pteBuyingTarget))
    '                    Else
    '                        UI = 0
    '                    End If
    '                    If UI > 0 Then
    '                        MyRating = mvarMyRating / UI
    '                    Else
    '                        MyRating = 0
    '                    End If
    '                ElseIf Target = PlannedTargetEnum.pteSecondTarget Then
    '                    MyRating = (mvarBookingtype.IndexSecondTarget / mvarBookingtype.IndexMainTarget) * mvarMyRating
    '                ElseIf Target = PlannedTargetEnum.pteThirdTarget Then
    '                    'TODO: Ändra
    '                    MyRating = (mvarBookingtype.IndexSecondTarget / mvarBookingtype.IndexMainTarget) * mvarMyRating
    '                ElseIf Target = PlannedTargetEnum.pteAllAdults Then
    '                    MyRating = (mvarBookingtype.IndexAllAdults / mvarBookingtype.IndexMainTarget) * mvarMyRating
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'MyRating_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: MyRating", Err.Description)
    '            End Get
    '            Set(ByVal value As Single)
    '                Dim UI As Single

    '                On Error GoTo MyRating_Error

    '                If Target = PlannedTargetEnum.pteMainTarget Then
    '                    mvarMyRating = value
    '                ElseIf Target = PlannedTargetEnum.pteSecondTarget Then
    '                    mvarMyRating = value / (mvarBookingtype.IndexSecondTarget / mvarBookingtype.IndexMainTarget)
    '                ElseIf Target = PlannedTargetEnum.pteAllAdults Then
    '                    mvarMyRating = value / (mvarBookingtype.IndexSecondTarget / mvarBookingtype.IndexMainTarget)
    '                ElseIf Target = PlannedTargetEnum.pteBuyingTarget Then
    '                    If ChannelRating(PlannedTargetEnum.pteBuyingTarget) > 0 Then
    '                        UI = (mvarMyRating / ChannelRating(PlannedTargetEnum.pteBuyingTarget))
    '                    Else
    '                        UI = 0
    '                    End If
    '                    If UI > 0 Then
    '                        mvarMyRating = value * UI
    '                    End If
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'MyRating_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: MyRating", Err.Description)


    '            End Set
    '        End Property

    '        Public Property Index() As Integer
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Index
    '            ' DateTime  : 2003-07-18 09:50
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets a standard index to be multiplied with all spots
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Index_Error

    '                Index = mvarIndex

    '                On Error GoTo 0
    '                Exit Property

    'Index_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Index", Err.Description)
    '            End Get
    '            Set(ByVal value As Integer)
    '                On Error GoTo Index_Error

    '                mvarIndex = value

    '                On Error GoTo 0
    '                Exit Property

    'Index_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Index", Err.Description)

    '            End Set
    '        End Property

    '        Public Property MatchedSpot() As cActualSpot
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : MatchedSpot
    '            ' DateTime  : 2003-07-18 09:50
    '            ' Author    : joho
    '            ' Purpose   : Pointer to a cActualSpot corresponding to the spot that was actually
    '            '             aired
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo MatchedSpot_Error

    '                MatchedSpot = mvarMatchedSpot

    '                On Error GoTo 0
    '                Exit Property

    'MatchedSpot_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: MatchedSpot", Err.Description)
    '            End Get
    '            Set(ByVal value As cActualSpot)
    '                On Error GoTo MatchedSpot_Error

    '                mvarMatchedSpot = value

    '                On Error GoTo 0
    '                Exit Property

    'MatchedSpot_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: MatchedSpot", Err.Description)

    '            End Set
    '        End Property

    '        Public Property MatchedBookedSpot() As cBookedSpot
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : MatchedSpot
    '            ' DateTime  : 2003-07-18 09:50
    '            ' Author    : joho
    '            ' Purpose   : Pointer to a cBookedSpot corresponding to the spot that was originally
    '            '             booked
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo MatchedSpot_Error

    '                Return mvarMatchedBookedSpot

    '                On Error GoTo 0
    '                Exit Property

    'MatchedSpot_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: MatchedSpot", Err.Description)
    '            End Get
    '            Set(ByVal value As cBookedSpot)
    '                On Error GoTo MatchedSpot_Error

    '                mvarMatchedBookedSpot = value

    '                On Error GoTo 0
    '                Exit Property

    'MatchedSpot_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: MatchedSpot", Err.Description)

    '            End Set
    '        End Property

    '        Public Property SpotLength() As Byte
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : SpotLength
    '            ' DateTime  : 2003-07-18 09:50
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the spot length of the spot
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo SpotLength_Error

    '                SpotLength = mvarSpotLength

    '                On Error GoTo 0
    '                Exit Property

    'SpotLength_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: SpotLength", Err.Description)
    '            End Get
    '            Set(ByVal value As Byte)
    '                On Error GoTo SpotLength_Error

    '                mvarSpotLength = value

    '                On Error GoTo 0
    '                Exit Property

    'SpotLength_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: SpotLength", Err.Description)

    '            End Set
    '        End Property

    '        Public Property SpotType() As Byte
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : SpotType
    '            ' DateTime  : 2003-07-18 09:50
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets an index number indicating what type of spot this is:
    '            '
    '            '               0 - RBS
    '            '               1 - Specific
    '            '               2 - Last minute
    '            '
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo SpotType_Error

    '                SpotType = mvarSpotType

    '                On Error GoTo 0
    '                Exit Property

    'SpotType_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: SpotType", Err.Description)
    '            End Get
    '            Set(ByVal value As Byte)
    '                On Error GoTo SpotType_Error

    '                mvarSpotType = value

    '                On Error GoTo 0
    '                Exit Property

    'SpotType_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: SpotType", Err.Description)

    '            End Set
    '        End Property

    '        Public Property PriceNet() As Decimal
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : PriceNet
    '            ' DateTime  : 2003-07-18 09:51
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the net price of this spot
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo PriceNet_Error

    '                PriceNet = mvarPriceNet

    '                On Error GoTo 0
    '                Exit Property

    'PriceNet_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: PriceNet", Err.Description)
    '            End Get
    '            Set(ByVal value As Decimal)

    '                On Error GoTo PriceNet_Error

    '                mvarPriceNet = value

    '                On Error GoTo 0
    '                Exit Property

    'PriceNet_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: PriceNet", Err.Description)
    '            End Set

    '        End Property

    '        Public Property PriceGross() As Decimal
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : PriceGross
    '            ' DateTime  : 2003-07-18 10:04
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the gross price for this spot
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo PriceGross_Error

    '                PriceGross = mvarPriceGross

    '                On Error GoTo 0
    '                Exit Property

    'PriceGross_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: PriceGross", Err.Description)
    '            End Get
    '            Set(ByVal value As Decimal)
    '                On Error GoTo PriceGross_Error

    '                mvarPriceGross = value

    '                On Error GoTo 0
    '                Exit Property

    'PriceGross_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: PriceGross", Err.Description)

    '            End Set
    '        End Property

    '        Public Property ChannelRating(Optional ByVal Target As PlannedTargetEnum = 4) As Single
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Rating
    '            ' DateTime  : 2003-07-22 21:10
    '            ' Author    : joho
    '            ' Purpose   :
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Rating_Error

    '                If Target = PlannedTargetEnum.pteBuyingTarget Then
    '                    ChannelRating = mvarRating
    '                ElseIf Target = PlannedTargetEnum.pteMainTarget Then
    '                    ChannelRating = mvarRating * mvarBookingtype.BuyingTarget.UniIndex(cTarget.EnumUni.uniMainCmp) * (mvarBookingtype.IndexMainTarget / 100)
    '                ElseIf Target = PlannedTargetEnum.pteSecondTarget Then
    '                    ChannelRating = mvarRating * mvarBookingtype.BuyingTarget.UniIndex(cTarget.EnumUni.uniMainCmp) * (mvarBookingtype.IndexSecondTarget / 100)
    '                ElseIf Target = PlannedTargetEnum.pteAllAdults Then
    '                    ChannelRating = mvarRating * mvarBookingtype.BuyingTarget.UniIndex(cTarget.EnumUni.uniMainCmp) * (mvarBookingtype.IndexAllAdults / 100)
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'Rating_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Rating", Err.Description)
    '            End Get
    '            Set(ByVal value As Single)
    '                On Error GoTo Rating_Error

    '                If Target = PlannedTargetEnum.pteBuyingTarget Then
    '                    mvarRating = value
    '                ElseIf Target = PlannedTargetEnum.pteMainTarget Then
    '                    If (mvarChannel.MainTarget.UniIndex(cTarget.EnumUni.uniMainCmp) * (mvarBookingtype.IndexMainTarget / 100)) <> 0 Then
    '                        mvarRating = value / (mvarChannel.MainTarget.UniIndex(cTarget.EnumUni.uniMainCmp) * (mvarBookingtype.IndexMainTarget / 100))
    '                    Else
    '                        mvarRating = 0
    '                    End If
    '                ElseIf Target = PlannedTargetEnum.pteSecondTarget Then
    '                    If (mvarChannel.MainTarget.UniIndex(cTarget.EnumUni.uniMainCmp) * (mvarBookingtype.IndexSecondTarget / 100)) <> 0 Then
    '                        mvarRating = value / (mvarChannel.MainTarget.UniIndex(cTarget.EnumUni.uniMainCmp) * (mvarBookingtype.IndexSecondTarget / 100))
    '                    Else
    '                        mvarRating = 0
    '                    End If
    '                ElseIf Target = PlannedTargetEnum.pteAllAdults Then
    '                    If (mvarChannel.MainTarget.UniIndex(cTarget.EnumUni.uniMainCmp) * (mvarBookingtype.IndexAllAdults / 100)) <> 0 Then
    '                        mvarRating = value / (mvarChannel.MainTarget.UniIndex(cTarget.EnumUni.uniMainCmp) * (mvarBookingtype.IndexAllAdults / 100))
    '                    Else
    '                        mvarRating = 0
    '                    End If
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'Rating_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Rating", Err.Description)


    '            End Set
    '        End Property

    '        Public Property Estimation() As EstEnum
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Estimation
    '            ' DateTime  : 2003-11-04 11:39
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets how well the spot has been estimated
    '            '---------------------------------------------------------------------------------------
    '            Get
    '                On Error GoTo Estimation_Error

    '                Estimation = mvarEstimation

    '                On Error GoTo 0
    '                Exit Property

    'Estimation_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Estimation", Err.Description)
    '            End Get
    '            Set(ByVal value As EstEnum)
    '                On Error GoTo Estimation_Error

    '                mvarEstimation = value

    '                On Error GoTo 0
    '                Exit Property

    'Estimation_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Estimation", Err.Description)

    '            End Set
    '        End Property

    '        Public Property Remark() As String

    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Remark
    '            ' DateTime  : 2003-07-22 21:10
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets a remark on a spot
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Remark_Error

    '                Remark = mvarRemark

    '                On Error GoTo 0
    '                Exit Property

    'Remark_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Remark", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo Remark_Error

    '                mvarRemark = value

    '                On Error GoTo 0
    '                Exit Property

    'Remark_Error:

    '                Err.Raise(Err.Number, "cPlannedSpot: Remark", Err.Description)

    '            End Set
    '        End Property

    '        Public Property Placement()
    '            Set(ByVal value)
    '                'used when assigning an Object to the property, on the left side of a Set statement.
    '                'Syntax: Set x.Placement = Form1
    '                mvarPlacement = value
    '            End Set
    '            Get
    '                Placement = mvarPlacement
    '            End Get
    '        End Property

    '        Public Sub New()
    '            Matched = False
    '        End Sub

    '        Protected Overrides Sub Finalize()
    '            mvarWeek = Nothing

    '            mvarBookingtype = Nothing
    '            mvarChannel = Nothing
    '            mvarFilm = Nothing
    '            mvarMatchedSpot = Nothing
    '            MyBase.Finalize()
    '        End Sub
    '
    ' End Class

    'Public Class cPlannedSpots
    '    Implements Collections.IEnumerable

    '    'local variable to hold collection
    '    Private mCol As Collection
    '    Private Main As cKampanj

    '    Friend WriteOnly Property MainObject()
    '        Set(ByVal value)
    '            Dim TmpSpot As cPlannedSpot

    '            Main = value
    '            For Each TmpSpot In mCol
    '                TmpSpot.MainObject = value
    '            Next
    '        End Set
    '    End Property

    '    Public Function Add(Optional ByVal ID As String = "") As cPlannedSpot
    '        'create a new object
    '        Dim objNewMember As cPlannedSpot
    '        objNewMember = New cPlannedSpot

    '        'sets a ID
    '        If ID = "" Then
    '            ID = CreateGUID()
    '        End If

    '        'set the properties passed into the method
    '        objNewMember.ID = ID
    '        mCol.Add(objNewMember, ID)

    '        'return the object created
    '        Add = objNewMember
    '        objNewMember = Nothing

    '    End Function

    '    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cPlannedSpot
    '        'used when referencing an element in the collection
    '        'vntIndexKey contains either the Index or Key to the collection,
    '        'this is why it is declared as a Variant
    '        'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
    '        Get
    '            If vntIndexKey.GetType.ToString = "System.String" Then
    '                If mCol.Contains(vntIndexKey) Then
    '                    Item = mCol(vntIndexKey)
    '                Else
    '                    Return Nothing
    '                End If
    '            Else
    '                Item = mCol(vntIndexKey)
    '            End If
    '        End Get
    '    End Property

    '    Public ReadOnly Property Count() As Long
    '        'used when retrieving the number of elements in the
    '        'collection. Syntax: Debug.Print x.Count
    '        Get
    '            Count = mCol.Count
    '        End Get
    '    End Property

    '    Public Sub Remove(ByVal vntIndexKey As Object)
    '        'used when removing an element from the collection
    '        'vntIndexKey contains either the Index or Key, which is why
    '        'it is declared as a Variant
    '        'Syntax: x.Remove(xyz)


    '        mCol.Remove(vntIndexKey)
    '    End Sub

    '    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
    '        GetEnumerator = mCol.GetEnumerator
    '    End Function

    '    Public Sub New()
    '        mCol = New Collection
    '    End Sub

    '    Protected Overrides Sub Finalize()
    '        mCol = Nothing
    '        MyBase.Finalize()
    '    End Sub

    'End Class

    '    Public Class cWeek
    '        'the class cWeek represents several days, not nessesary a "real" week but its always a 7-day period 

    '        Private mvarFilms As cFilms 'A collection of films to be used in the campaign
    '        Private mvarTRPBuyingTarget As Single
    '        Private mvarNetBudget As Decimal
    '        Private mvarStartDate As Long 'where the "week" starts (not nessesary a monday)
    '        Private mvarEndDate As Long 'where the "week" ends
    '        Private mvarName As String = ""
    '        Private mvarControlSaved As Boolean
    '        Private mvarControlSent As Boolean
    '        Private mvarControlConfirmed As Boolean
    '        Private mvarControlSentToClient As Boolean
    '        Private mvarControlInvoiced As Boolean
    '        Private mvarIsVisible As Boolean

    '        ' The variable below is set to true when TRP is regarded as the fixed value.
    '        ' When this is the case, all CPP changes influence the Net budget.
    '        ' If TRPControl is set to false all CPP changes influences the TRPs

    '        Public TRPControl As Boolean

    '        Private Main As cKampanj
    '        Private mvarBookingtype As cBookingType 'The booking type used
    '        Private ParentColl As Collection


    '        Friend WriteOnly Property ParentCollection()
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : ParentCollection
    '            ' DateTime  : 2003-07-07 13:18
    '            ' Author    : joho
    '            ' Purpose   : Sets the Collection of wich this week is a member. This is used
    '            '             when a new week Name is set. See that property for further explanation
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Set(ByVal value)
    '                ParentColl = value
    '            End Set
    '        End Property

    '        Friend WriteOnly Property Bookingtype()
    '            Set(ByVal value)
    '                mvarBookingtype = value
    '                mvarFilms.Bookingtype = value
    '            End Set
    '        End Property

    '        Friend WriteOnly Property MainObject()
    '            Set(ByVal value)
    '                Main = value
    '                mvarFilms.MainObject = value
    '            End Set
    '        End Property

    '        Public ReadOnly Property NetCPP() As Single
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : NetCPP
    '            ' DateTime  : 2003-07-12 11:39
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the actual Net CPP for this week
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo NetCPP_Error

    '                Dim TmpCPP As Single = 0
    '                For dp As Integer = 0 To Main.DaypartCount - 1
    '                    TmpCPP += mvarBookingtype.BuyingTarget.NetCPP * Index(True) * (SpotIndex() / 100) * GrossIndex() * AddedValueIndexNet()
    '                Next
    '                Return TmpCPP

    '                On Error GoTo 0
    '                Exit Property

    'NetCPP_Error:

    '                Err.Raise(Err.Number, "cWeek: NetCPP", Err.Description)
    '            End Get

    '        End Property

    '        Public ReadOnly Property NetCPP30(Optional ByVal Effective As Boolean = False) As Single
    '            Get
    '                On Error GoTo NetCPP30_Error

    '                Dim TmpCPP As Single = 0
    '                For dp As Integer = 0 To Main.DaypartCount - 1
    '                    TmpCPP += (mvarBookingtype.BuyingTarget.NetCPP * Index(Effective) * GrossIndex() * AddedValueIndexNet()) * (mvarBookingtype.DaypartSplit(dp) / 100)
    '                Next
    '                Return TmpCPP

    '                On Error GoTo 0
    '                Exit Property

    'NetCPP30_Error:

    '                Err.Raise(Err.Number, "cWeek: NetCPP30", Err.Description)
    '            End Get
    '        End Property

    '        Public Property Films() As cFilms
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Films
    '            ' DateTime  : 2003-07-12 11:27
    '            ' Author    : joho
    '            ' Purpose   : Pointer to a collection of cFilm, containing each spot to be used
    '            '             in the campaign
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Films_Error

    '                Films = mvarFilms

    '                On Error GoTo 0
    '                Exit Property

    'Films_Error:

    '                Err.Raise(Err.Number, "cWeek: Films", Err.Description)
    '            End Get
    '            Set(ByVal value As cFilms)
    '                On Error GoTo Films_Error

    '                mvarFilms = value

    '                On Error GoTo 0
    '                Exit Property

    'Films_Error:

    '                Err.Raise(Err.Number, "cWeek: Films", Err.Description)

    '            End Set
    '        End Property

    '        Public Property TRP() As Single
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : TRP
    '            ' DateTime  : 2003-07-12 11:28
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the amount of TRPs for this week in the actual target
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo TRP_Error

    '                TRP = mvarTRPBuyingTarget * mvarBookingtype.BuyingTarget.UniIndex(True) * (mvarBookingtype.IndexMainTarget / 100)

    '                On Error GoTo 0
    '                Exit Property

    'TRP_Error:

    '                Err.Raise(Err.Number, "cWeek: TRP", Err.Description)
    '            End Get
    '            Set(ByVal value As Single)
    '                On Error GoTo TRP_Error

    '                If mvarBookingtype.BuyingTarget.UniIndex(True) <> 0 Then '* (mvarBookingtype.IndexMainTarget / 100)
    '                    If (mvarBookingtype.IndexMainTarget / 100) > 0 And mvarBookingtype.BuyingTarget.UniIndex(True) > 0 Or IsError(value) Then
    '                        mvarTRPBuyingTarget = value / mvarBookingtype.BuyingTarget.UniIndex(True) / (mvarBookingtype.IndexMainTarget / 100)
    '                    Else
    '                        mvarTRPBuyingTarget = 0
    '                    End If
    '                Else
    '                    mvarTRPBuyingTarget = 0
    '                End If
    '                'TRPControl = True

    '                On Error GoTo 0
    '                Exit Property

    'TRP_Error:

    '                Err.Raise(Err.Number, "cWeek: TRP", Err.Description)


    '            End Set
    '        End Property

    '        Public Property NetBudget() As Decimal
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : NetBudget
    '            ' DateTime  : 2003-07-12 11:28
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the Net budget for this week
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo NetBudget_Error

    '                RecalculateCPP()
    '                NetBudget = mvarNetBudget

    '                On Error GoTo 0
    '                Exit Property

    'NetBudget_Error:

    '                Err.Raise(Err.Number, "cWeek: NetBudget", Err.Description)
    '            End Get
    '            Set(ByVal value As Decimal)
    '                On Error GoTo NetBudget_Error

    '                mvarNetBudget = value
    '                If NetCPP = 0 Then
    '                    TRPBuyingTarget = 0
    '                Else 'If Not Main.Loading Then
    '                    TRPBuyingTarget = value / NetCPP
    '                End If
    '                'TRPControl = False

    '                On Error GoTo 0
    '                Exit Property

    'NetBudget_Error:

    '                Err.Raise(Err.Number, "cWeek: NetBudget", Err.Description)

    '            End Set
    '        End Property

    '        Public ReadOnly Property Discount(Optional ByVal Effective As Boolean = False) As Single
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Discount
    '            ' DateTime  : 2003-07-12 11:28
    '            ' Author    : joho
    '            ' Purpose   : Returns the actual discount for this week
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Discount_Error

    '                If mvarBookingtype.GrossCPP <> 0 And GrossIndex <> 0 Then
    '                    Discount = 1 - (NetCPP30(Effective) / (mvarBookingtype.GrossCPP * GrossIndex))
    '                Else
    '                    Discount = 1
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'Discount_Error:

    '                Err.Raise(Err.Number, "cWeek: Discount", Err.Description)
    '            End Get

    '        End Property

    '        Public Property StartDate() As Long
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : StartDate
    '            ' DateTime  : 2003-07-12 11:28
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the date when this week is set to start
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo StartDate_Error

    '                StartDate = mvarStartDate

    '                On Error GoTo 0
    '                Exit Property

    'StartDate_Error:

    '                Err.Raise(Err.Number, "cWeek: StartDate", Err.Description)
    '            End Get
    '            Set(ByVal value As Long)
    '                Dim i As Integer
    '                Dim Added As Boolean

    '                On Error GoTo StartDate_Error

    '                mvarStartDate = value
    '                On Error Resume Next
    '                ParentColl.Remove(mvarName)
    '                On Error GoTo StartDate_Error
    '                Added = False
    '                For i = 1 To ParentColl.Count
    '                    If ParentColl(i).StartDate >= mvarStartDate Then
    '                        ParentColl.Add(Me, mvarName, i)
    '                        Added = True
    '                        Exit For
    '                    End If
    '                Next
    '                If Not Added Then
    '                    ParentColl.Add(Me, mvarName)
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'StartDate_Error:

    '                Err.Raise(Err.Number, "cWeek: StartDate", Err.Description)

    '            End Set
    '        End Property

    '        Public Property EndDate() As Long
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : EndDate
    '            ' DateTime  : 2003-07-12 11:28
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the date when this week is set to end
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo EndDate_Error

    '                EndDate = mvarEndDate

    '                On Error GoTo 0
    '                Exit Property

    'EndDate_Error:

    '                Err.Raise(Err.Number, "cWeek: EndDate", Err.Description)
    '            End Get
    '            Set(ByVal value As Long)
    '                On Error GoTo EndDate_Error

    '                mvarEndDate = value

    '                On Error GoTo 0
    '                Exit Property

    'EndDate_Error:

    '                Err.Raise(Err.Number, "cWeek: EndDate", Err.Description)


    '            End Set
    '        End Property

    '        Public Property Name() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Name
    '            ' DateTime  : 2003-07-12 11:28
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the name of this week
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Name_Error

    '                Name = mvarName

    '                On Error GoTo 0
    '                Exit Property

    'Name_Error:

    '                Err.Raise(Err.Number, "cWeek: Name", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                Dim Ini As New clsIni
    '                Dim Added As Boolean
    '                Dim i As Integer

    '                On Error GoTo Name_Error

    '                '    If Name <> mvarName And mvarName <> "" Then
    '                '
    '                '        Set TmpWeek = ParentColl(mvarName)
    '                '        ParentColl.Remove mvarName
    '                '        ParentColl.Add TmpWeek, Name
    '                '
    '                '    End If
    '                If ParentColl.Contains(mvarName) Then
    '                    ParentColl.Remove(mvarName)
    '                End If
    '                mvarName = Trim(value)
    '                Added = False
    '                For i = 1 To ParentColl.Count
    '                    If ParentColl(i).StartDate >= mvarStartDate Then
    '                        ParentColl.Add(Me, mvarName, i)
    '                        Added = True
    '                        Exit For
    '                    End If
    '                Next
    '                If Not Added Then
    '                    ParentColl.Add(Me, mvarName)
    '                End If
    '                Ini = Nothing
    '                On Error GoTo 0
    '                Exit Property

    'Name_Error:

    '                Err.Raise(Err.Number, "cWeek: Name", Err.Description)

    '            End Set
    '        End Property

    '        Public Property IsVisible() As Boolean
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : IsVisible
    '            ' DateTime  : 2003-07-12 11:30
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets wether this week will be shown in the charts
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo IsVisible_Error

    '                IsVisible = mvarIsVisible

    '                On Error GoTo 0
    '                Exit Property

    'IsVisible_Error:

    '                Err.Raise(Err.Number, "cWeek: IsVisible", Err.Description)
    '            End Get
    '            Set(ByVal value As Boolean)
    '                On Error GoTo IsVisible_Error

    '                mvarIsVisible = value

    '                On Error GoTo 0
    '                Exit Property

    'IsVisible_Error:

    '                Err.Raise(Err.Number, "cWeek: IsVisible", Err.Description)

    '            End Set
    '        End Property

    '        Public Function SpotIndex() As Single
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : SpotIndex
    '            ' DateTime  : 2003-07-12 12:04
    '            ' Author    : joho
    '            ' Purpose   : Returns the spotindex calculated from all the films
    '            '---------------------------------------------------------------------------------------
    '            '

    '            Dim TmpFilm As cFilm

    '            On Error GoTo SpotIndex_Error

    '            SpotIndex = 0
    '            If mvarFilms.Count = 0 Then
    '                SpotIndex = 100
    '            Else
    '                For Each TmpFilm In mvarFilms
    '                    SpotIndex = SpotIndex + (TmpFilm.Share / 100) * (TmpFilm.Index)
    '                Next
    '            End If

    '            On Error GoTo 0
    '            Exit Function

    'SpotIndex_Error:

    '            Err.Raise(Err.Number, "cWeek: SpotIndex", Err.Description)


    '        End Function

    '        Public Property TRPBuyingTarget() As Single
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : TRPBuyingTarget
    '            ' DateTime  : 2003-07-13 17:20
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the amount of TRPs for this week in the buying target
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo TRPBuyingTarget_Error

    '                TRPBuyingTarget = mvarTRPBuyingTarget

    '                On Error GoTo 0
    '                Exit Property

    'TRPBuyingTarget_Error:

    '                Err.Raise(Err.Number, "cWeek: TRPBuyingTarget", Err.Description)
    '            End Get
    '            Set(ByVal value As Single)
    '                On Error GoTo TRPBuyingTarget_Error

    '                mvarTRPBuyingTarget = value
    '                'TRPControl = True
    '                On Error GoTo 0
    '                Exit Property

    'TRPBuyingTarget_Error:

    '                Err.Raise(Err.Number, "cWeek: TRPBuyingTarget", Err.Description)

    '            End Set
    '        End Property

    '        Public Property TRPAllAdults() As Single
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : TRPAllAdults
    '            ' DateTime  : 2003-07-15 11:43
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the amount of TRPs for this week in All adults.
    '            '             The name 3Plus is kept for backward compatibility
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo TRPAllAdults_Error

    '                TRPAllAdults = mvarTRPBuyingTarget * mvarBookingtype.BuyingTarget.UniIndex(True) * (mvarBookingtype.IndexAllAdults / 100)

    '                On Error GoTo 0
    '                Exit Property

    'TRPAllAdults_Error:

    '                Err.Raise(Err.Number, "cWeek: TRPAllAdults", Err.Description)
    '            End Get
    '            Set(ByVal value As Single)
    '                On Error GoTo TRPAllAdults_Error

    '                If mvarBookingtype.BuyingTarget.UniIndex <> 0 Then
    '                    mvarTRPBuyingTarget = value / mvarBookingtype.BuyingTarget.UniIndex(True) / (mvarBookingtype.IndexAllAdults / 100)
    '                Else
    '                    mvarTRPBuyingTarget = 0
    '                End If
    '                'TRPControl = True

    '                On Error GoTo 0
    '                Exit Property

    'TRPAllAdults_Error:

    '                Err.Raise(Err.Number, "cWeek: TRPAllAdults", Err.Description)


    '            End Set
    '        End Property

    '        Private Sub RecalculateCPP()
    '            If TRPControl Then

    '                mvarNetBudget = mvarTRPBuyingTarget * NetCPP

    '            Else

    '                If NetCPP <> 0 Then
    '                    mvarTRPBuyingTarget = mvarNetBudget / NetCPP
    '                Else
    '                    mvarTRPBuyingTarget = 0
    '                End If

    '            End If
    '        End Sub

    '        Public ReadOnly Property GrossBudget() As Decimal
    '            Get
    '                GrossBudget = mvarBookingtype.GrossCPP * mvarTRPBuyingTarget * GrossIndex * (SpotIndex() / 100)
    '            End Get
    '        End Property

    '        Public ReadOnly Property GrossIndex() As Single
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : GrossIndex
    '            ' DateTime  : 2003-09-22 13:50
    '            ' Author    : joho
    '            ' Purpose   : Returns the Index to be used to calculate Gross CPPs
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                Dim Idx As Single
    '                Dim i As Integer
    '                On Error GoTo GrossIndex_Error

    '                Idx = 0
    '                For i = mvarStartDate To mvarEndDate
    '                    For dp As Integer = 0 To Main.DaypartCount - 1
    '                        Idx += ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, dp) * mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, dp)) / (mvarEndDate - mvarStartDate + 1)) * (mvarBookingtype.DaypartSplit(dp) / 100)
    '                    Next
    '                    'Original before introduction of Daypart indexes
    '                    'Idx = Idx + ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP) * mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP)) / (mvarEndDate - mvarStartDate + 1))
    '                Next
    '                Return Format(Idx / 10000, "0.0000000")

    '                On Error GoTo 0
    '                Exit Property

    'GrossIndex_Error:

    '                Err.Raise(Err.Number, "cWeek: GrossIndex", Err.Description)
    '            End Get

    '        End Property

    '        Public ReadOnly Property ExtraTRP() As Single
    '            Get
    '                On Error GoTo ExtraTRP_Error
    '                Dim TmpIndex As cIndex
    '                Dim Idx As Single
    '                Dim i As Integer

    '                '    If TRPControl Then
    '                '        ExtraTRP = (TRPBuyingTarget - (TRPBuyingTarget * (SeasonIndex / 100)))
    '                '    Else
    '                '        ExtraTRP = (NetBudget / NetCPP) - ((NetBudget / NetCPP) * (SeasonIndex / 100))
    '                '    End If

    '                Idx = 0
    '                For i = mvarStartDate To mvarEndDate
    '                    Idx = Idx + ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eTRP) * (mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eTRP))) / (mvarEndDate - mvarStartDate + 1))
    '                Next

    '                ExtraTRP = Format(mvarTRPBuyingTarget * (Idx / 10000) - mvarTRPBuyingTarget, "0.0000")

    '                On Error GoTo 0
    '                Exit Property

    'ExtraTRP_Error:

    '                Err.Raise(Err.Number, "cWeek: ExtraTRP", Err.Description)
    '            End Get
    '        End Property

    '        Public Function Index(Optional ByVal Effective As Boolean = False) As Single
    '            Dim Idx As Single
    '            Dim i As Long
    '            Dim ExtraIndex As Single

    '            On Error GoTo GrossIndex_Error

    '            Idx = 0
    '            For i = mvarStartDate To mvarEndDate
    '                For dp As Integer = 0 To Main.DaypartCount - 1
    '                    Idx += ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP, dp) * (mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP, dp))) / (mvarEndDate - mvarStartDate + 1)) * (mvarBookingtype.DaypartSplit(dp) / 100)
    '                    If Effective Then
    '                        ExtraIndex += ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eTRP, dp) * (mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eTRP, dp))) / (mvarEndDate - mvarStartDate + 1)) * (mvarBookingtype.DaypartSplit(dp) / 100)
    '                    End If
    '                Next
    '                ' Old statement before introduction of Daypart index
    '                ' Idx += ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP) * (mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP))) / (mvarEndDate - mvarStartDate + 1))
    '            Next

    '            If Effective Then
    '                If ExtraIndex <> 0 Then
    '                    Index = Format((Idx / 10000) * (10000 / ExtraIndex), "0.000000")
    '                Else
    '                    Index = Format((Idx / 10000), "0.000000")
    '                End If
    '            Else
    '                Index = Format((Idx / 10000), "0.000000")
    '            End If
    '            If Index = 0 Then
    '                Index = 1
    '            End If
    '            On Error GoTo 0
    '            Exit Function

    'GrossIndex_Error:

    '            Err.Raise(Err.Number, "cWeek: GrossIndex", Err.Description)

    '        End Function

    '        Public Function AddedValueIndexNet()
    '            Dim TmpAV As cAddedValue
    '            Dim i As Integer
    '            Dim TmpWeek As cWeek
    '            Dim TmpIndex As Single

    '            i = 1
    '            For Each TmpWeek In mvarBookingtype.Weeks
    '                If TmpWeek Is Me Then
    '                    Exit For
    '                End If
    '                i = i + 1
    '            Next
    '            TmpIndex = 1
    '            For Each TmpAV In mvarBookingtype.AddedValues
    '                TmpIndex = TmpIndex * (100 + (TmpAV.IndexNet - 100) * (TmpAV.Amount(i) / 100)) / 100
    '            Next
    '            AddedValueIndexNet = TmpIndex
    '        End Function

    '        Public Function AddedValueIndexGross()
    '            Dim TmpAV As cAddedValue
    '            Dim i As Integer
    '            Dim TmpWeek As cWeek
    '            Dim TmpIndex As Single

    '            i = 1
    '            For Each TmpWeek In mvarBookingtype.Weeks
    '                If TmpWeek Is Me Then
    '                    Exit For
    '                End If
    '                i = i + 1
    '            Next
    '            TmpIndex = 1
    '            For Each TmpAV In mvarBookingtype.AddedValues
    '                TmpIndex = TmpIndex * (100 + (TmpAV.IndexGross - 100) * (TmpAV.Amount(i) / 100)) / 100
    '            Next
    '            AddedValueIndexGross = TmpIndex
    '        End Function

    '        Public Sub New(ByVal MainObject As cKampanj)
    '            mvarIsVisible = True
    '            mvarFilms = New cFilms(Main)
    '            Main = MainObject
    '        End Sub

    '    End Class

    '    Public Class cWeeks
    '        Implements Collections.IEnumerable
    '        'weeks is a container/collection of several week
    '        'the class week represents several days, not nessesary a "real" week but its always a 7-day period 

    '        'local variable to hold collection
    '        Private mCol As Collection
    '        Private Main As cKampanj
    '        Private mvarBookingtype As cBookingType

    '        Friend WriteOnly Property Bookingtype()
    '            Set(ByVal value)
    '                Dim TmpWeek As cWeek

    '                mvarBookingtype = value
    '                For Each TmpWeek In mCol
    '                    TmpWeek.Bookingtype = value
    '                Next
    '            End Set

    '        End Property

    '        Friend WriteOnly Property MainObject()
    '            Set(ByVal value)
    '                Dim TmpWeek As cWeek

    '                Main = value
    '                For Each TmpWeek In mCol
    '                    TmpWeek.MainObject = value
    '                Next
    '            End Set
    '        End Property

    '        Public Function Add(ByVal Name As String) As cWeek
    '            'create a new object
    '            Dim objNewMember As cWeek
    '            Dim Ini As New clsIni

    '            objNewMember = New cWeek(Main)


    '            'set the properties passed into the method

    '            objNewMember.ParentCollection = mCol
    '            objNewMember.Name = Trim(Name)
    '            objNewMember.Bookingtype = mvarBookingtype
    '            objNewMember.MainObject = Main
    '            If Not mCol.Contains(Trim(Name)) Then
    '                mCol.Add(objNewMember, Trim(Name))
    '            End If

    '            'return the object created
    '            Add = mCol(Trim(Name))
    '            objNewMember = Nothing
    '            Ini = Nothing

    '        End Function

    '        Default Public Property Item(ByVal vntIndexKey As Object) As cWeek
    '            'used when referencing an element in the collection
    '            'vntIndexKey contains either the Index or Key to the collection,
    '            'this is why it is declared as a Variant
    '            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
    '            Get
    '                On Error GoTo ErrHandle

    '                Item = mCol(vntIndexKey)
    '                Exit Property

    'ErrHandle:
    '                Err.Raise(Err.Number, "cWeeks", "Unknown week: " & vntIndexKey)
    '            End Get
    '            Set(ByVal value As cWeek)
    '                mCol.Remove(vntIndexKey)
    '                mCol.Add(value, vntIndexKey)
    '            End Set
    '        End Property

    '        Public ReadOnly Property Count() As Long
    '            'used when retrieving the number of elements in the
    '            'collection. Syntax: Debug.Print x.Count
    '            Get
    '                Count = mCol.Count
    '            End Get
    '        End Property

    '        Public Sub Remove(ByVal vntIndexKey As Object)
    '            'used when removing an element from the collection
    '            'vntIndexKey contains either the Index or Key, which is why
    '            'it is declared as a Variant
    '            'Syntax: x.Remove(xyz)


    '            mCol.Remove(vntIndexKey)
    '        End Sub

    '        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
    '            GetEnumerator = mCol.GetEnumerator
    '        End Function

    '        Public Sub New(ByVal MainObject As cKampanj)
    '            mCol = New Collection
    '            Main = MainObject
    '        End Sub

    '        Protected Overrides Sub Finalize()
    '            mCol = Nothing
    '            MyBase.Finalize()
    '        End Sub
    '    End Class

    '    Public Class cTarget

    '        Public Enum EnumUni

    '            uniMainTot = 0
    '            uniMainSec = 1
    '            uniSecTot = 2
    '            uniMainCmp = 3

    '        End Enum

    '        Public Enum TargetTypeEnum

    '            trgMnemonicTarget = 0
    '            trgUserTarget = 1
    '            trgDynamicTarget = 2

    '        End Enum

    '        Private mvarTargetType As TargetTypeEnum
    '        Private mvarUniverse As String
    '        Private mvarUniSize As Long
    '        Private mvarTargetName As String
    '        Private Main As cKampanj
    '        Private mvarNoUniSize As Boolean
    '        Private mvarSecondUniverse As String

    '        Private LastTargStr As String

    '        Private SaveUniIndex(0 To 3)

    '        Private mvarSaveSize As Single
    '        Private mvarSaveTarget As String

    '        Public Property TargetType() As TargetTypeEnum
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : TargetType
    '            ' DateTime  : 2003-07-08 15:06
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the type of target used:
    '            '
    '            '               0 - Mnemonic Target
    '            '               1 - User Target     (not implemented)
    '            '               2 - Dynamic Target  (not implemented)
    '            '
    '            '---------------------------------------------------------------------------------------
    '            Get
    '                On Error GoTo TargetType_Error

    '                TargetType = mvarTargetType

    '                On Error GoTo 0
    '                Exit Property

    'TargetType_Error:

    '                Err.Raise(Err.Number, "cTarget: TargetType", Err.Description)
    '            End Get
    '            Set(ByVal value As TargetTypeEnum)
    '                On Error GoTo TargetType_Error

    '                mvarTargetType = value

    '                On Error GoTo 0
    '                Exit Property

    'TargetType_Error:

    '                Err.Raise(Err.Number, "cTarget: TargetType", Err.Description)

    '            End Set
    '        End Property

    '        Public Property Universe() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Universe
    '            ' DateTime  : 2003-07-08 15:06
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the universe used. An error is raised if the universe
    '            '             does not exist.
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Universe_Error

    '                Universe = mvarUniverse

    '                On Error GoTo 0
    '                Exit Property

    'Universe_Error:

    '                Err.Raise(Err.Number, "cTarget: Universe", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                Dim i As Integer

    '                On Error GoTo Universe_Error

    '                If value = mvarUniverse Then Exit Property
    '                mvarSaveTarget = ""
    '                If value <> "" Then

    '                    Helper.WriteToLogFile("cTarget.Universe : Check if Universe(" & value & ") exists")
    '                    i = Main.InternalAdedge.setUniverseUserDefined(value)
    '                    Helper.WriteToLogFile("cTarget.Universe : Reset Universes")
    '                    If Main.UniStr Is Nothing Then
    '                        PrepareAdedge()
    '                    End If
    '                    Main.InternalAdedge.setUniverseUserDefined(Main.UniStr)
    '                    '        If Kampanj.InternalAdedge.Validate And 2 Then
    '                    '            Kampanj.InternalAdedge.setChannels Kampanj.Channels(1).AdEdgeNames
    '                    '        End If
    '                    '        Kampanj.InternalAdedge.Run
    '                End If

    '                If i <> 2 And value <> "" Then
    '                    mvarUniverse = ""
    '                Else
    '                    mvarUniverse = value
    '                    If Not mvarNoUniSize Then
    '                        Helper.WriteToLogFile("cTarget.Universe : Get UniverseSizes")
    '                        GetUniSizes()
    '                    End If
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'Universe_Error:

    '                If Err.Number = 2000 Then
    '                    Err.Raise(9, "cTarget: Universe", "Unknown universe: " & Universe)
    '                Else
    '                    Err.Raise(Err.Number, "cTarget: Universe", Err.Description)
    '                End If

    '            End Set
    '        End Property

    '        Public ReadOnly Property UniSize() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : UniSize
    '            ' DateTime  : 2003-07-08 15:06
    '            ' Author    : joho
    '            ' Purpose   : Returns the Universe size of the target.
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                Dim Test As Integer
    '                Dim HasBeenErrors As Boolean
    '                Dim TmpChan As cChannel
    '                Dim ChanStr As String
    '                'Dim TargIdx As Integer
    '                'Dim i As Integer
    '                'Dim Target As String
    '                On Error GoTo UniSize_Error

    '                'Target = Trim(mvarTargetName)
    '                'TargIdx = -1
    '                'For i = 0 To Main.InternalAdedge.getTargetCount - 1
    '                '    If Main.InternalAdedge.getTargetTitle(i) = Target Then
    '                '        TargIdx = i
    '                '        Exit For
    '                '    End If
    '                '    If Main.InternalAdedge.getTargetTitle(i) = "A" & Target Then
    '                '        TargIdx = i
    '                '        Exit For
    '                '    End If
    '                'Next
    '                On Error Resume Next
    '                If Main.TargColl(mvarTargetName.ToString, Main.InternalAdedge) < 1 Then
    '                    Universe = mvarUniverse
    '                End If
    '                If Main.TargColl(mvarTargetName.ToString, Main.InternalAdedge) < 1 Then
    '                    UniSize = 0
    '                    Exit Property
    '                End If
    '                On Error Resume Next
    '                If Not Main.UniColl.Contains(mvarUniverse.ToString) Then
    '                    UniSize = 0
    '                    Exit Property
    '                End If
    '                On Error GoTo UniSize_Error

    '                If mvarTargetName <> "" Then
    '                    'Main.Adedge.setTargetMnemonic(Main.TargStr)
    '                    If mvarSaveTarget <> mvarTargetName Then
    '                        mvarSaveSize = Main.InternalAdedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , Main.UniColl(mvarUniverse.ToString) - 1, Main.TargColl(mvarTargetName, Main.InternalAdedge) - 1)
    '                        mvarSaveTarget = mvarTargetName
    '                    End If
    '                    UniSize = mvarSaveSize
    '                    'Stop
    '                Else
    '                    UniSize = 0
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'UniSize_Error:
    '                If Not HasBeenErrors Then
    '                    Universe = mvarUniverse
    '                    For Each TmpChan In Main.Channels
    '                        ChanStr = ChanStr & TmpChan.AdEdgeNames & ","
    '                    Next
    '                    HasBeenErrors = True
    '                    'Main.Adedge.setTargetMnemonic(Main.TargStr)
    '                    Main.InternalAdedge.setTargetMnemonic(Main.TargStr)
    '                    Main.InternalAdedge.setUniverseUserDefined(Main.UniStr)
    '                    Main.InternalAdedge.setArea(Main.Area)
    '                    Main.InternalAdedge.setChannelsArea(ChanStr)
    '                    Main.InternalAdedge.setPeriod("-1d")
    '                    Main.InternalAdedge.Run()
    '                    Resume
    '                End If
    '                If Err.GetException.GetType.Name = "COMException" AndAlso Err.GetException.Message = "getUniSampleInfo: Must call Run before extraction of values" Then
    '                    Return 0
    '                End If
    '                Err.Raise(Err.Number, "cTarget: UniSize", Err.Description)
    '            End Get

    '        End Property

    '        Public Property TargetName() As String
    '            Get
    '                On Error GoTo TargetName_Error

    '                If mvarTargetName = "" Then
    '                    If Not Main Is Nothing Then
    '                        mvarTargetName = Main.AllAdults
    '                    End If
    '                End If
    '                Return mvarTargetName

    '                On Error GoTo 0
    '                Exit Property

    'TargetName_Error:

    '                Err.Raise(Err.Number, "cTarget: TargetName", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo TargetName_Error

    '                If Right(value, 3) = "-99" Then
    '                    value = Left(value, Len(value) - 3) & "+"
    '                End If
    '                If value = "" Then
    '                    If Not Main Is Nothing Then
    '                        mvarTargetName = Main.AllAdults
    '                    End If
    '                Else
    '                    mvarTargetName = UCase(value)
    '                End If
    '                'If Main.InternalAdedge.setTargetMnemonic(mvarTargetName) = 0 Then
    '                '    Err.Raise(13, Me.ToString, "Target " & mvarTargetName & " is illegal")
    '                'End If
    '                If Not mvarNoUniSize Then
    '                    GetUniSizes()
    '                End If
    '                mvarSaveTarget = ""
    '                On Error GoTo 0

    '                Exit Property

    'TargetName_Error:

    '                Err.Raise(Err.Number, "cTarget: TargetName", Err.Description)

    '            End Set
    '        End Property

    '        Friend WriteOnly Property MainObject()
    '            Set(ByVal value)
    '                Dim Ini As New clsIni

    '                Main = value

    '                If Not Main Is Nothing Then
    '                    Ini.Create(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Main.Area & "\Area.ini")
    '                    mvarSecondUniverse = Ini.Text("Universe", "Second")
    '                End If
    '            End Set
    '        End Property

    '        Public ReadOnly Property UniSizeTot() As Long
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : UniSizeTot
    '            ' DateTime  : 2003-07-08 15:56
    '            ' Author    : joho
    '            ' Purpose   : Returns the _national_ Universe size of the target
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                Dim Test As Integer
    '                Dim HasBeenErrors As Boolean
    '                Dim TmpChan As cChannel
    '                Dim ChanStr As String

    '                On Error GoTo UniSizeTot_Error

    '                If mvarUniSize = 0 Then
    '                    Universe = mvarUniverse
    '                End If

    '                On Error Resume Next
    '                If Main.TargColl(Trim(mvarTargetName), Main.InternalAdedge) < 0 Then
    '                    PrepareAdedge()
    '                    Main.InternalAdedge.setTargetMnemonic(Main.TargStr)
    '                    If Main.TargColl(Trim(mvarTargetName), Main.InternalAdedge) < 0 Then
    '                        UniSizeTot = 0
    '                        Exit Property
    '                    End If
    '                End If
    '                On Error GoTo UniSizeTot_Error

    '                If mvarTargetName <> "" Then
    '                    UniSizeTot = Main.InternalAdedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , 0, Main.TargColl(mvarTargetName, Main.InternalAdedge) - 1)
    '                Else
    '                    UniSizeTot = 0
    '                End If
    '                On Error GoTo 0
    '                Exit Property

    'UniSizeTot_Error:
    '                If Not HasBeenErrors Then
    '                    PrepareAdedge()
    '                    For Each TmpChan In Main.Channels
    '                        ChanStr = ChanStr & TmpChan.AdEdgeNames & ","
    '                    Next
    '                    HasBeenErrors = True
    '                    'Main.Adedge.setTargetMnemonic(Main.TargStr)
    '                    Main.InternalAdedge.setTargetMnemonic(Main.TargStr)
    '                    Main.InternalAdedge.setUniverseUserDefined(Main.UniStr)
    '                    Main.InternalAdedge.setArea(Main.Area)
    '                    Main.InternalAdedge.setChannelsArea(ChanStr)
    '                    Main.InternalAdedge.setPeriod("-1d")
    '                    Main.InternalAdedge.Run()
    '                    Resume
    '                End If

    '                Err.Raise(Err.Number, "cTarget: UniSizeTot", Err.Description)
    '            End Get

    '        End Property

    '        Private Sub GetUniSizes()
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : GetUniSizes
    '            ' DateTime  : 2003-07-09 13:13
    '            ' Author    : joho
    '            ' Purpose   : Gets the universe sizes for this Target
    '            '---------------------------------------------------------------------------------------
    '            '

    '            Dim i As Integer
    '            Dim UniColl As New Collection
    '            Dim TmpDate As Date

    '            On Error GoTo GetUniSizes_Error

    '            If Not PrepareAdedge() Then
    '                Exit Sub
    '            End If

    '            If mvarTargetType = 0 And Not Main Is Nothing Then

    '                Main.InternalAdedge.setArea(Main.Area)
    '                Main.InternalAdedge.setPeriod("-1d")
    '                Main.InternalAdedge.clearList()
    '                'TODO: Replace call below with a better one
    '                If Main.Channels.Count > 0 Then
    '                    Main.InternalAdedge.clearList()
    '                    Main.InternalAdedge.setChannelsArea(Main.Channels(1).AdEdgeNames)
    '                    TmpDate = Date.FromOADate(Main.InternalAdedge.getDataRangeTo(Connect.eDataType.mSpot))
    '                    Main.InternalAdedge.addBrand(Format(TmpDate, "ddMMyy"), "12:00", Main.Channels(1).AdEdgeNames)
    '                End If
    '                Main.InternalAdedge.setTargetMnemonic(Main.TargStr)
    '                i = Main.InternalAdedge.Run(False, False, -1)
    '                '        If i > 0 And Kampanj.InternalAdedge.Validate <> 4 Then
    '                '            If mvarUniverse = "" Then
    '                '                mvarUniSize = Kampanj.InternalAdedge.getUniSampleInfo(mUniSize, 0, , 0)
    '                '                mvarUniSizeTot = mvarUniSize
    '                '            Else
    '                '                mvarUniSize = Kampanj.InternalAdedge.getUniSampleInfo(mUniSize, 0, , UniColl(mvarUniverse))
    '                '                mvarUniSizeTot = Kampanj.InternalAdedge.getUniSampleInfo(mUniSize, 0, , 0)
    '                '            End If
    '                '            If mvarSecondUniverse = mvarUniverse Then
    '                '                mvarUniSizeSec = mvarUniSize
    '                '            Else
    '                '                mvarUniSizeSec = Kampanj.InternalAdedge.getUniSampleInfo(mUniSize, 0, , Kampanj.UniColl(mvarSecondUniverse))
    '                '            End If
    '                '        Else
    '                '            mvarUniSize = 0
    '                '            mvarUniSizeTot = 0
    '                '            mvarUniSizeSec = 0
    '                '        End If
    '            End If
    '            SaveUniIndex(0) = 0
    '            SaveUniIndex(1) = 0
    '            SaveUniIndex(2) = 0
    '            SaveUniIndex(3) = 0

    '            UniColl = Nothing
    '            On Error GoTo 0
    '            Exit Sub

    'GetUniSizes_Error:

    '            Err.Raise(Err.Number, "cTarget: GetUniSizes", Err.Description)


    '        End Sub

    '        Public Function UniIndex(Optional ByVal Universes As EnumUni = 0) As Single
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : UniIndex
    '            ' DateTime  : 2003-07-09 13:15
    '            ' Author    : joho
    '            ' Purpose   : Macro to return the index between the different universes
    '            '---------------------------------------------------------------------------------------
    '            '

    '            Dim Dividend As Long

    '            On Error GoTo UniIndex_Error


    '            If SaveUniIndex(Universes) = 0 Then
    '                If Universes = cTarget.EnumUni.uniMainTot Then
    '                    Dividend = UniSizeTot
    '                    If Dividend > 0 Then
    '                        SaveUniIndex(cTarget.EnumUni.uniMainTot) = UniSize / Dividend
    '                    Else
    '                        SaveUniIndex(cTarget.EnumUni.uniMainTot) = 0
    '                    End If
    '                ElseIf Universes = cTarget.EnumUni.uniMainSec Then
    '                    Dividend = UniSizeSec
    '                    If Dividend > 0 Then
    '                        SaveUniIndex(Universes) = Dividend / UniSize
    '                    Else
    '                        SaveUniIndex(Universes) = 0
    '                    End If
    '                ElseIf Universes = cTarget.EnumUni.uniSecTot Then
    '                    If UniSizeTot > 0 Then
    '                        SaveUniIndex(Universes) = UniSizeSec / UniSizeTot
    '                    Else
    '                        SaveUniIndex(Universes) = 0
    '                    End If
    '                ElseIf Universes = cTarget.EnumUni.uniMainCmp Then
    '                    If Main.MainTarget.Universe = mvarUniverse Then
    '                        Return 1
    '                    Else
    '                        If Main.MainTarget.Universe = "" Then
    '                            Dividend = UniSizeTot
    '                        Else
    '                            Dividend = Main.MainTarget.UniSize
    '                        End If
    '                        If Dividend > 0 Then
    '                            SaveUniIndex(Universes) = UniSize / Dividend
    '                        Else
    '                            SaveUniIndex(Universes) = 0
    '                        End If
    '                    End If
    '                End If
    '            End If
    '            UniIndex = SaveUniIndex(Universes)

    '            On Error GoTo 0
    '            Exit Function

    'UniIndex_Error:

    '            Err.Raise(Err.Number, "cTarget: UniIndex", Err.Description)

    '        End Function

    '        Public Property NoUniverseSize() As Boolean
    '            Get
    '                NoUniverseSize = mvarNoUniSize
    '            End Get
    '            Set(ByVal value As Boolean)
    '                mvarNoUniSize = value
    '            End Set
    '        End Property

    '        Public ReadOnly Property TargetNameNice() As String
    '            Get
    '                Dim Tmpstr As String

    '                If mvarTargetName = "" Then
    '                    Return ""
    '                Else
    '                    If Val(Left(mvarTargetName, 1)) <> 0 Then
    '                        Tmpstr = "A" & mvarTargetName
    '                    Else
    '                        Tmpstr = mvarTargetName
    '                    End If
    '                    Mid(Tmpstr, 1, 1) = UCase(Mid(Tmpstr, 1, 1))
    '                    Return Tmpstr
    '                End If
    '            End Get
    '        End Property

    '        Public Property SecondUniverse() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : SecondUniverse
    '            ' DateTime  : 2003-09-23 08:53
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the secondary Universe for Target
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo SecondUniverse_Error

    '                SecondUniverse = mvarSecondUniverse

    '                On Error GoTo 0
    '                Exit Property

    'SecondUniverse_Error:

    '                Err.Raise(Err.Number, "cTarget: SecondUniverse", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                Dim i As Integer

    '                On Error GoTo SecondUniverse_Error

    '                If value <> "" Then
    '                    i = Main.InternalAdedge.setUniverseUserDefined(value)
    '                    Main.InternalAdedge.setUniverseUserDefined(Main.UniStr)
    '                    '        Kampanj.InternalAdedge.Run
    '                End If

    '                If i <> 2 And Universe <> "" Then
    '                    mvarSecondUniverse = ""
    '                Else
    '                    mvarSecondUniverse = value
    '                    If Not mvarNoUniSize Then
    '                        GetUniSizes()
    '                    End If
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'SecondUniverse_Error:

    '                Err.Raise(Err.Number, "cTarget: SecondUniverse", Err.Description)


    '            End Set
    '        End Property

    '        Public ReadOnly Property UniSizeSec() As Long
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : UniSizeSec
    '            ' DateTime  : 2003-09-23 08:54
    '            ' Author    : joho
    '            ' Purpose   : Returns the Universe size of the secondary Universe
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                Dim Test As Integer
    '                Dim HasBeenErrors As Boolean
    '                Dim TmpChan As cChannel
    '                Dim ChanStr As String

    '                On Error GoTo UniSizeSec_Error

    '                '    If mvarUniSizeSec = 0 Then
    '                '        SecondUniverse = mvarSecondUniverse
    '                '    End If

    '                On Error Resume Next
    '                Test = Main.TargColl(mvarTargetName, Main.InternalAdedge)
    '                If Err.Number <> 0 Then
    '                    UniSizeSec = 0
    '                    Exit Property
    '                End If
    '                On Error Resume Next
    '                Test = Main.UniColl(mvarUniverse)
    '                If Err.Number <> 0 Then
    '                    UniSizeSec = 0
    '                    Exit Property
    '                End If
    '                On Error GoTo UniSizeSec_Error

    '                If mvarTargetName <> "" Then
    '                    UniSizeSec = Main.InternalAdedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , Main.UniColl(mvarSecondUniverse) - 1, Main.TargColl(mvarTargetName, Main.InternalAdedge) - 1)
    '                Else
    '                    UniSizeSec = 0
    '                End If
    '                On Error GoTo 0
    '                Exit Property

    'UniSizeSec_Error:
    '                If Not HasBeenErrors Then
    '                    PrepareAdedge()
    '                    For Each TmpChan In Main.Channels
    '                        ChanStr = ChanStr & TmpChan.AdEdgeNames & ","
    '                    Next
    '                    HasBeenErrors = True
    '                    'Main.Adedge.setTargetMnemonic(Main.TargStr)
    '                    Main.InternalAdedge.setTargetMnemonic(Main.TargStr)
    '                    Main.InternalAdedge.setUniverseUserDefined(Main.UniStr)
    '                    Main.InternalAdedge.setArea(Main.Area)
    '                    Main.InternalAdedge.setChannelsArea(ChanStr)
    '                    Main.InternalAdedge.setPeriod("-1d")
    '                    Main.InternalAdedge.Run()
    '                    Resume
    '                End If

    '                Err.Raise(Err.Number, "cTarget: UniSizeSec", Err.Description)
    '            End Get

    '        End Property

    '        Private Function PrepareAdedge() As Boolean
    '            Dim Y As Integer
    '            If mvarTargetName = "" Then Exit Function
    '            PrepareAdedge = False
    '            If InStr(Main.TargStr, "," + mvarTargetName & ",") = 0 Or Main.TargStr <> LastTargStr Then
    '                If Main.TargStr = "" Then
    '                    Main.TargStr = "," & Main.AllAdults & ","
    '                End If
    '                If Main.InternalAdedge.setTargetMnemonic(mvarTargetName) = 1 Then
    '                    On Error Resume Next
    '                    If InStr(Main.TargStr, "," & mvarTargetName) = 0 And InStr(Main.TargStr, ",A" & mvarTargetName) = 0 Then
    '                        Main.TargStr = Main.TargStr + "," + mvarTargetName + ","
    '                    End If
    '                End If
    '                LastTargStr = Main.TargStr
    '                Main.InternalAdedge.setTargetMnemonic(Main.TargStr)
    '                'Main.Adedge.setTargetMnemonic(Main.TargStr)
    '                PrepareAdedge = True
    '            End If
    '            If InStr(Main.UniStr, mvarUniverse & ",") = 0 Then
    '                If Not Main.UniColl.Contains("") Then
    '                    Main.UniColl.Add(1, "")
    '                    Main.UniColl.Add(1, Nothing)
    '                End If
    '                If Main.UniStr = "" Then Main.UniStr = ","
    '                If Not Main.UniColl.Contains(mvarUniverse) Then
    '                    Y = Main.UniColl.Count
    '                    Main.UniColl.Add(Y, mvarUniverse)
    '                    Main.UniStr = Main.UniStr + mvarUniverse + ","
    '                    Y = Y + 1
    '                ElseIf InStr(Main.UniStr, mvarUniverse & ",") = 0 Then
    '                    Main.UniColl = New Collection
    '                    Main.UniColl.Add(1, "")
    '                    Main.UniColl.Add(1, Nothing)
    '                    If Main.UniStr = "" Then Main.UniStr = ","
    '                    Y = Main.UniColl.Count
    '                    Main.UniColl.Add(Y, mvarUniverse)
    '                    Main.UniStr = Main.UniStr + mvarUniverse + ","
    '                    Y = Y + 1
    '                End If
    '                Main.InternalAdedge.setUniverseUserDefined(Main.UniStr)
    '                PrepareAdedge = True
    '            End If
    '            If InStr(Main.UniStr, mvarSecondUniverse & ",") = 0 Then
    '                If Not Main.UniColl.Contains("") Then
    '                    Main.UniColl.Add(1, "")
    '                    Main.UniColl.Add(1, Nothing)
    '                End If
    '                If Main.UniStr = "" Then Main.UniStr = ","
    '                Y = Main.UniColl.Count
    '                If Not Main.UniColl.Contains(mvarSecondUniverse) Then
    '                    Main.UniColl.Add(Y, mvarSecondUniverse)
    '                    Main.UniStr = Main.UniStr + mvarSecondUniverse + ","
    '                    Y = Y + 1
    '                ElseIf InStr(Main.UniStr, mvarSecondUniverse & ",") = 0 Then
    '                    Main.UniColl = New Collection
    '                    Main.UniColl.Add(1, "")
    '                    Main.UniColl.Add(1, Nothing)
    '                    If Main.UniStr = "" Then Main.UniStr = ","
    '                    Y = Main.UniColl.Count
    '                    Main.UniColl.Add(Y, mvarSecondUniverse)
    '                    Main.UniStr = Main.UniStr + mvarSecondUniverse + ","
    '                    Y = Y + 1
    '                End If
    '                Main.InternalAdedge.setUniverseUserDefined(Main.UniStr)
    '                PrepareAdedge = True
    '            End If
    '        End Function

    '        Public Sub New(ByVal Main As cKampanj)
    '            mvarTargetType = 0
    '            mvarUniverse = ""
    '            mvarTargetName = "3+"
    '            MainObject = Main
    '        End Sub

    '    End Class

    '    Public Class cPricelist

    '        Private mvarStartDate As Long
    '        Private mvarEndDate As Long
    '        Private mvarTargets As cPricelistTargets ' a collection of PricelistTarget
    '        Private Main As cKampanj
    '        Private mvarBookingtype As cBookingType 'The booking type for the spot
    '        Private mvarBuyingUniverse As String


    '        Friend WriteOnly Property Bookingtype() As cBookingType
    '            Set(ByVal value As cBookingType)
    '                mvarBookingtype = value
    '                mvarTargets.Bookingtype = value
    '            End Set
    '        End Property

    '        Friend WriteOnly Property MainObject() As cKampanj
    '            Set(ByVal value As cKampanj)
    '                mvarTargets.MainObject = value
    '                Main = value
    '            End Set
    '        End Property

    '        Public Property StartDate() As Long
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : StartDate
    '            ' DateTime  : 2003-07-11 11:47
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the date when this pricelist started to be used
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo StartDate_Error

    '                StartDate = mvarStartDate

    '                On Error GoTo 0
    '                Exit Property

    'StartDate_Error:

    '                Err.Raise(Err.Number, "cPriceList: StartDate", Err.Description)
    '            End Get
    '            Set(ByVal value As Long)
    '                On Error GoTo StartDate_Error

    '                mvarStartDate = StartDate

    '                On Error GoTo 0
    '                Exit Property

    'StartDate_Error:

    '                Err.Raise(Err.Number, "cPriceList: StartDate", Err.Description)
    '            End Set
    '        End Property

    '        Public Property EndDate() As Long
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : EndDate
    '            ' DateTime  : 2003-07-11 11:47
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the date when this pricelist will stop being used
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo EndDate_Error

    '                EndDate = mvarEndDate

    '                On Error GoTo 0
    '                Exit Property

    'EndDate_Error:

    '                Err.Raise(Err.Number, "cPriceList: EndDate", Err.Description)
    '            End Get
    '            Set(ByVal value As Long)
    '                On Error GoTo EndDate_Error

    '                mvarEndDate = value

    '                On Error GoTo 0
    '                Exit Property

    'EndDate_Error:

    '                Err.Raise(Err.Number, "cPriceList: EndDate", Err.Description)

    '            End Set
    '        End Property

    '        Public Property Targets() As cPricelistTargets
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Targets
    '            ' DateTime  : 2003-07-11 11:52
    '            ' Author    : joho
    '            ' Purpose   : Pointer to a collection of cPriceListTarget
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Targets_Error

    '                Targets = mvarTargets

    '                On Error GoTo 0
    '                Exit Property

    'Targets_Error:

    '                Err.Raise(Err.Number, "cPriceList: Targets", Err.Description)
    '            End Get
    '            Set(ByVal value As cPricelistTargets)
    '                On Error GoTo Targets_Error

    '                mvarTargets = value

    '                On Error GoTo 0
    '                Exit Property

    'Targets_Error:

    '                Err.Raise(Err.Number, "cPriceList: Targets", Err.Description)

    '            End Set
    '        End Property

    '        Public Property BuyingUniverse() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : BuyingUniverse
    '            ' DateTime  : 2003-08-19 14:11
    '            ' Author    : joho
    '            ' Purpose   :
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo BuyingUniverse_Error

    '                BuyingUniverse = mvarBuyingUniverse

    '                On Error GoTo 0
    '                Exit Property

    'BuyingUniverse_Error:

    '                Err.Raise(Err.Number, "cPriceList: BuyingUniverse", Err.Description)
    '            End Get
    '            Set(ByVal value As String)

    '                On Error GoTo BuyingUniverse_Error

    '                mvarBuyingUniverse = value

    '                On Error GoTo 0
    '                Exit Property

    'BuyingUniverse_Error:

    '                Err.Raise(Err.Number, "cPriceList: BuyingUniverse", Err.Description)


    '            End Set
    '        End Property

    '        Public Sub New(ByVal Main As cKampanj)
    '            mvarTargets = New cPricelistTargets(Main)
    '            MainObject = Main
    '        End Sub

    '        Public Sub Save(ByVal Path As String)
    '            Dim XMLDoc As New XmlDocument
    '            Dim Node As XmlNode
    '            Dim XMLPricelist As XmlElement
    '            Dim TmpBT As cBookingType
    '            Dim TmpTarget As cPricelistTarget
    '            Dim BTNode As XmlElement
    '            Dim TargetNode As XmlElement
    '            Dim TargetsNode As XmlElement
    '            Dim IndexNode As XmlElement
    '            Dim TmpIndex As Trinity.cIndex
    '            Dim i As Integer

    '            XMLDoc.PreserveWhitespace = True
    '            Node = XMLDoc.CreateProcessingInstruction("xml", "version='1.0'")
    '            XMLDoc.AppendChild(Node)

    '            Node = XMLDoc.CreateComment("Trinity pricelist. Created by Trinity.")
    '            XMLDoc.AppendChild(Node)

    '            XMLPricelist = XMLDoc.CreateElement("Pricelist")
    '            XMLDoc.AppendChild(XMLPricelist)

    '            For Each TmpBT In mvarBookingtype.ParentChannel.BookingTypes
    '                BTNode = XMLDoc.CreateElement("Price")
    '                BTNode.SetAttribute("Name", TmpBT.Name)
    '                BTNode.SetAttribute("AverageRating", TmpBT.AverageRating)
    '                For i = 0 To Main.DaypartCount - 1
    '                    If TmpBT.Pricelist.Targets.Count > 0 Then
    '                        BTNode.SetAttribute("DP" & i + 1, TmpBT.Pricelist.Targets(1).DefaultDaypart(i))
    '                    End If
    '                Next
    '                TargetsNode = XMLDoc.CreateElement("Targets")
    '                For Each TmpTarget In TmpBT.Pricelist.Targets
    '                    TargetNode = XMLDoc.CreateElement("Price")
    '                    TargetNode.SetAttribute("Target", TmpTarget.TargetName)
    '                    TargetNode.SetAttribute("AdedgeTarget", TmpTarget.Target.TargetName)
    '                    TargetNode.SetAttribute("TargetNat", TmpTarget.UniSizeNat)
    '                    TargetNode.SetAttribute("TargetUni", TmpTarget.UniSize)
    '                    TargetNode.SetAttribute("CalcCPP", TmpTarget.CalcCPP)
    '                    TargetNode.SetAttribute("StandardTarget", TmpTarget.StandardTarget)
    '                    For i = 0 To Main.DaypartCount - 1
    '                        TargetNode.SetAttribute("CPP_DP" & i + 1, TmpTarget.CPPDaypart(i))
    '                    Next
    '                    TargetNode.SetAttribute("CPP", TmpTarget.CPP)
    '                    For Each TmpIndex In TmpTarget.Indexes
    '                        'If TmpIndex.Index <> 100 OrElse TmpIndex.Index(0) <> 100 Then
    '                        IndexNode = XMLDoc.CreateElement("Index")
    '                        IndexNode.SetAttribute("Name", TmpIndex.Name)
    '                        IndexNode.SetAttribute("Index", TmpIndex.Index)
    '                        For i = 0 To Main.DaypartCount - 1
    '                            IndexNode.SetAttribute("IndexDP" & i, TmpIndex.Index(i))
    '                        Next
    '                        IndexNode.SetAttribute("FromDate", TmpIndex.FromDate)
    '                        IndexNode.SetAttribute("ToDate", TmpIndex.ToDate)
    '                        TargetNode.AppendChild(IndexNode)
    '                        'End If
    '                    Next
    '                    TargetsNode.AppendChild(TargetNode)
    '                Next
    '                BTNode.AppendChild(TargetsNode)
    '                XMLPricelist.AppendChild(BTNode)
    '            Next
    '            XMLDoc.Save(Helper.Pathify(Path) & mvarBookingtype.ParentChannel.ChannelName & ".xml")
    '        End Sub

    '    End Class

    '    Public Class cPricelistTarget

    '        Private mvarTargetName As String
    '        Private mvarTarget As cTarget
    '        Private mvarUniSizeNat As Long
    '        Private mvarUniSize As Long
    '        Private mvarCPPDaypart(0 To 5) As Integer
    '        Private mvarCPP As Single
    '        Private mvarCalcCPP As Boolean
    '        Private Main As cKampanj
    '        Private mvarBookingtype As cBookingType
    '        Private mvarStandardTarget As Boolean
    '        Private mvarIndexes As New cIndexes(Main)
    '        Private mvarIsEntered As EnteredEnum
    '        Private mvarNetCPT As Single
    '        Private mvarDefaultDaypart(0 To 5) As Byte
    '        Private mvarDiscount As Single
    '        Private mvarNetCPP As Single

    '        Public Enum EnteredEnum
    '            eDiscount = 0
    '            eCPT = 1
    '            eCPP = 2
    '        End Enum

    '        Friend WriteOnly Property Bookingtype() As cBookingType
    '            Set(ByVal value As cBookingType)
    '                mvarBookingtype = value
    '            End Set
    '        End Property

    '        Friend WriteOnly Property MainObject() As cKampanj
    '            Set(ByVal value As cKampanj)
    '                Main = value
    '            End Set
    '        End Property

    '        Public Property TargetName() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : TargetName
    '            ' DateTime  : 2003-07-11 12:00
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the name of the target
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo TargetName_Error

    '                TargetName = mvarTargetName

    '                On Error GoTo 0
    '                Exit Property

    'TargetName_Error:

    '                Err.Raise(Err.Number, "cPriceListTarget: TargetName", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo TargetName_Error

    '                mvarTargetName = value

    '                On Error GoTo 0
    '                Exit Property

    'TargetName_Error:

    '                Err.Raise(Err.Number, "cPriceListTarget: TargetName", Err.Description)


    '            End Set
    '        End Property

    '        Public Property Target() As cTarget
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Target
    '            ' DateTime  : 2003-07-11 12:00
    '            ' Author    : joho
    '            ' Purpose   : Pointer to a cTarget object that holds the actual Target
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Target_Error
    '                If mvarTarget Is Nothing Then
    '                    mvarTarget = New cTarget(Main)
    '                End If
    '                Target = mvarTarget

    '                On Error GoTo 0
    '                Exit Property

    'Target_Error:

    '                Err.Raise(Err.Number, "cPriceListTarget: Target", Err.Description)
    '            End Get
    '            Set(ByVal value As cTarget)
    '                On Error GoTo Target_Error

    '                mvarTarget = value

    '                On Error GoTo 0
    '                Exit Property

    'Target_Error:

    '                Err.Raise(Err.Number, "cPriceListTarget: Target", Err.Description)

    '            End Set
    '        End Property

    '        Public Property UniSizeNat() As Long
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : UniSizeNat
    '            ' DateTime  : 2003-07-11 12:00
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the National Universe Size for the buying target
    '            '             according to the channel pricelist
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo UniSizeNat_Error

    '                UniSizeNat = mvarUniSizeNat

    '                On Error GoTo 0
    '                Exit Property

    'UniSizeNat_Error:

    '                Err.Raise(Err.Number, "cPriceListTarget: UniSizeNat", Err.Description)
    '            End Get
    '            Set(ByVal value As Long)
    '                On Error GoTo UniSizeNat_Error

    '                mvarUniSizeNat = value

    '                On Error GoTo 0
    '                Exit Property

    'UniSizeNat_Error:

    '                Err.Raise(Err.Number, "cPriceListTarget: UniSizeNat", Err.Description)

    '            End Set
    '        End Property

    '        Public Property UniSize() As Long
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : UniSize
    '            ' DateTime  : 2003-07-11 12:00
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the universe size in the buying universe according to
    '            '             the channel pricelist
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo UniSize_Error

    '                UniSize = mvarUniSize

    '                On Error GoTo 0
    '                Exit Property

    'UniSize_Error:

    '                Err.Raise(Err.Number, "cPriceListTarget: UniSize", Err.Description)
    '            End Get
    '            Set(ByVal value As Long)

    '                On Error GoTo UniSize_Error

    '                mvarUniSize = value

    '                On Error GoTo 0
    '                Exit Property

    'UniSize_Error:

    '                Err.Raise(Err.Number, "cPriceListTarget: UniSize", Err.Description)

    '            End Set
    '        End Property

    '        Public Property CPPDaypart(ByVal Daypart) As Integer
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : CPPDaypart
    '            ' DateTime  : 2003-07-11 12:01
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the CPP during each daypart
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo CPP_Error

    '                CPPDaypart = mvarCPPDaypart(Daypart)

    '                On Error GoTo 0
    '                Exit Property

    'CPP_Error:

    '                Err.Raise(Err.Number, "cPriceListTarget: CPP_DT", Err.Description)
    '            End Get
    '            Set(ByVal value As Integer)
    '                On Error GoTo CPP_Error

    '                mvarCPPDaypart(Daypart) = value

    '                If mvarCalcCPP Then
    '                    CalculateCPP()
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'CPP_Error:

    '                Err.Raise(Err.Number, "cPriceListTarget: CPP_DT", Err.Description)

    '            End Set
    '        End Property

    '        Public Property CPP() As Single
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : CPP
    '            ' DateTime  : 2003-07-11 12:01
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the CPP during the entire day
    '            '---------------------------------------------------------------------------------------
    '            '

    '            Get
    '                On Error GoTo CPP_Error

    '                If mvarCPP = 0 And mvarCalcCPP Then
    '                    CalculateCPP()
    '                End If

    '                CPP = mvarCPP

    '                On Error GoTo 0
    '                Exit Property

    'CPP_Error:

    '                Err.Raise(Err.Number, "cPriceListTarget: CPP", Err.Description)
    '            End Get
    '            Set(ByVal value As Single)
    '                On Error GoTo CPP_Error

    '                mvarCPP = value

    '                On Error GoTo 0
    '                Exit Property

    'CPP_Error:

    '                Err.Raise(Err.Number, "cPriceListTarget: CPP", Err.Description)

    '            End Set
    '        End Property

    '        Public Property CalcCPP() As Boolean
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : CalcCPP
    '            ' DateTime  : 2003-07-11 12:02
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets wether the CPP should be calculated from the individual
    '            '             daypart-CPPs
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo CalcCPP_Error

    '                CalcCPP = mvarCalcCPP

    '                On Error GoTo 0
    '                Exit Property

    'CalcCPP_Error:

    '                Err.Raise(Err.Number, "cPriceListTarget: CalcCPP", Err.Description)
    '            End Get
    '            Set(ByVal value As Boolean)

    '                On Error GoTo CalcCPP_Error

    '                mvarCalcCPP = value

    '                On Error GoTo 0
    '                Exit Property

    'CalcCPP_Error:

    '                Err.Raise(Err.Number, "cPriceListTarget: CalcCPP", Err.Description)

    '            End Set
    '        End Property

    '        Friend Sub CalculateCPP()

    '            Dim x As Integer
    '            Dim TmpCPP As Decimal

    '            For x = 0 To Main.DaypartCount - 1
    '                TmpCPP = TmpCPP + (mvarBookingtype.DaypartSplit(x) / 100) * mvarCPPDaypart(x)
    '            Next

    '            mvarCPP = TmpCPP

    '        End Sub

    '        Public Function UniIndex(Optional ByVal CompareWithMain As Boolean = False) As Single
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : UniIndex
    '            ' DateTime  : 2003-07-09 13:15
    '            ' Author    : joho
    '            ' Purpose   : Macro to return the index between the actual universe and the
    '            '             national
    '            '             CompareWithMain: Decides whether the return value should be the
    '            '                              index between the channel universe and the
    '            '                              national universe (False) or between channel
    '            '                              universe and the chosen universe for the main
    '            '                              target (True).
    '            '---------------------------------------------------------------------------------------
    '            '

    '            On Error GoTo UniIndex_Error
    '            If CompareWithMain Then
    '                If Main.MainTarget.Universe = mvarBookingtype.ParentChannel.BuyingUniverse OrElse (Main.MainTarget.TargetName = mvarBookingtype.BuyingTarget.Target.TargetName AndAlso Main.MainTarget.UniSize = mvarBookingtype.BuyingTarget.Target.UniSize) Then
    '                    UniIndex = 1
    '                Else
    '                    If UniSizeNat > 0 Then
    '                        UniIndex = UniSize / UniSizeNat
    '                    Else
    '                        UniIndex = 1
    '                    End If
    '                End If
    '            Else
    '                If UniSizeNat > 0 Then
    '                    UniIndex = UniSize / UniSizeNat
    '                Else
    '                    UniIndex = 1
    '                End If
    '            End If
    '            If UniIndex = 0 Then
    '                UniIndex = 1
    '            End If
    '            On Error GoTo 0
    '            Exit Function

    'UniIndex_Error:

    '            Err.Raise(Err.Number, "cTarget: UniIndex", Err.Description)


    '        End Function

    '        Public Property StandardTarget() As Boolean
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : StandardTarget
    '            ' DateTime  : 2003-10-15 13:58
    '            ' Author    : joho
    '            ' Purpose   : If set to true it indicates that the target is in the
    '            '             standard pricelist of the channel.
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo StandardTarget_Error

    '                StandardTarget = mvarStandardTarget

    '                On Error GoTo 0
    '                Exit Property

    'StandardTarget_Error:

    '                Err.Raise(Err.Number, "cPriceListTarget: StandardTarget", Err.Description)
    '            End Get
    '            Set(ByVal value As Boolean)
    '                On Error GoTo StandardTarget_Error

    '                mvarStandardTarget = value

    '                On Error GoTo 0
    '                Exit Property

    'StandardTarget_Error:

    '                Err.Raise(Err.Number, "cPriceListTarget: StandardTarget", Err.Description)

    '            End Set
    '        End Property

    '        Public Property Indexes() As cIndexes
    '            Get
    '                Indexes = mvarIndexes
    '            End Get
    '            Set(ByVal value As cIndexes)
    '                mvarIndexes = value
    '            End Set
    '        End Property

    '        Public Property Discount() As Single
    '            Get
    '                If mvarIsEntered = EnteredEnum.eDiscount Then
    '                    Discount = mvarDiscount
    '                Else
    '                    If mvarBookingtype.GrossCPP <> 0 Then
    '                        Discount = 1 - (mvarNetCPP / mvarBookingtype.GrossCPP)
    '                    Else
    '                        Discount = 1
    '                    End If
    '                End If
    '            End Get
    '            Set(ByVal value As Single)
    '                mvarDiscount = value
    '                mvarNetCPP = mvarBookingtype.GrossCPP * (1 - value)
    '                If UniSize <> 0 Then
    '                    mvarNetCPT = (mvarNetCPP / UniSize) * 100
    '                Else
    '                    mvarNetCPT = 0
    '                End If
    '            End Set
    '        End Property

    '        Public Property NetCPT() As Single
    '            Get
    '                NetCPT = mvarNetCPT
    '            End Get
    '            Set(ByVal value As Single)
    '                mvarNetCPT = value
    '                mvarNetCPP = (value * UniSize) / 100
    '                If mvarBookingtype.GrossCPP <> 0 Then
    '                    mvarDiscount = 1 - (mvarNetCPP / mvarBookingtype.GrossCPP)
    '                Else
    '                    mvarDiscount = 1
    '                End If
    '            End Set
    '        End Property

    '        Public Property NetCPP() As Single
    '            Get
    '                NetCPP = mvarNetCPP
    '            End Get
    '            Set(ByVal value As Single)
    '                Dim TmpIndex As Single

    '                If mvarBookingtype.Weeks.Count > 0 Then
    '                    TmpIndex = mvarBookingtype.Weeks(1).GrossIndex
    '                Else
    '                    TmpIndex = 1
    '                End If
    '                mvarNetCPP = value
    '                If mvarBookingtype.GrossCPP <> 0 Then
    '                    mvarDiscount = 1 - (value / mvarBookingtype.GrossCPP) * TmpIndex
    '                Else
    '                    mvarDiscount = 1
    '                End If
    '                If mvarBookingtype.BuyingTarget.UniSize <> 0 Then
    '                    mvarNetCPT = (mvarNetCPP / mvarBookingtype.BuyingTarget.UniSize) * 100
    '                Else
    '                    mvarNetCPT = 0
    '                End If
    '            End Set
    '        End Property

    '        Public Property IsEntered() As EnteredEnum
    '            Get
    '                IsEntered = mvarIsEntered
    '            End Get
    '            Set(ByVal value As EnteredEnum)
    '                mvarIsEntered = value
    '            End Set
    '        End Property

    '        Public Property DefaultDaypart(ByVal Daypart) As Byte
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : DefaultDaypart
    '            ' DateTime  : 2003-07-11 11:48
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the default share in each daypart
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo DefaultDay_Error

    '                DefaultDaypart = mvarDefaultDaypart(Daypart)

    '                On Error GoTo 0
    '                Exit Property

    'DefaultDay_Error:

    '                Err.Raise(Err.Number, "cPriceList: DefaultDay", Err.Description)
    '            End Get
    '            Set(ByVal value As Byte)
    '                On Error GoTo DefaultDay_Error

    '                mvarDefaultDaypart(Daypart) = value

    '                On Error GoTo 0
    '                Exit Property

    'DefaultDay_Error:

    '                Err.Raise(Err.Number, "cPriceList: DefaultDay", Err.Description)

    '            End Set
    '        End Property

    '        Public Sub New(ByVal Main As cKampanj)
    '            mvarStandardTarget = False
    '            mvarIsEntered = 0
    '            mvarNetCPT = 0
    '            mvarTarget = New cTarget(Main)
    '            MainObject = Main
    '        End Sub

    '        Protected Overrides Sub Finalize()

    '            mvarTarget = Nothing

    '            mvarIndexes = Nothing
    '            MyBase.Finalize()
    '        End Sub

    '    End Class

    '    Public Class cPricelistTargets
    '        Implements Collections.IEnumerable

    '        'local variable to hold collection
    '        Private mCol As Collection
    '        Private Main As cKampanj
    '        Private mvarBookingtype As cBookingType

    '        Public Sub Clear()
    '            mCol.Clear()
    '        End Sub

    '        Friend WriteOnly Property Bookingtype() As cBookingType
    '            Set(ByVal value As cBookingType)
    '                Dim TmpTarget As cPricelistTarget

    '                mvarBookingtype = value
    '                For Each TmpTarget In mCol
    '                    TmpTarget.Bookingtype = value
    '                Next
    '            End Set
    '        End Property

    '        Friend WriteOnly Property MainObject() As cKampanj
    '            Set(ByVal value As cKampanj)
    '                Dim TmpTarget As cPricelistTarget

    '                Main = value
    '                For Each TmpTarget In mCol
    '                    TmpTarget.MainObject = value
    '                Next
    '            End Set
    '        End Property

    '        Public Function Add(ByVal TargetName As String, Optional ByVal Target As cTarget = Nothing, Optional ByVal UniSize As Long = 0, Optional ByVal UniSizeNat As Long = 0, Optional ByVal CPP As Single = 0, Optional ByVal CalcCPP As Boolean = False) As cPricelistTarget

    '            'create a new object
    '            Dim objNewMember As cPricelistTarget
    '            On Error GoTo Add_Error

    '            objNewMember = New cPricelistTarget(Main)

    '            objNewMember.CalcCPP = CalcCPP
    '            objNewMember.CPP = CPP
    '            objNewMember.Target = Target
    '            objNewMember.TargetName = TargetName
    '            objNewMember.UniSize = UniSize
    '            objNewMember.UniSizeNat = UniSizeNat
    '            objNewMember.Bookingtype = mvarBookingtype
    '            If Main Is Nothing Then
    '                objNewMember = objNewMember
    '            End If

    '            'set the properties passed into the method
    '            mCol.Add(objNewMember, TargetName)


    '            'return the object created
    '            Add = objNewMember
    '            objNewMember = Nothing


    '            On Error GoTo 0
    '            Exit Function

    'Add_Error:

    '            Err.Raise(Err.Number, "cPriceListTargets: Add", Err.Description)


    '        End Function

    '        Default Public Property Item(ByVal vntIndexKey As Object) As cPricelistTarget
    '            Get
    '                On Error GoTo Item_Error

    '                'used when referencing an element in the collection
    '                'vntIndexKey contains either the Index or Key to the collection,
    '                'this is why it is declared as a Variant
    '                'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)

    '                Item = mCol(vntIndexKey)

    '                If Item.CalcCPP Then
    '                    Item.CalculateCPP()
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'Item_Error:
    '                Item = Nothing
    '            End Get
    '            Set(ByVal value As cPricelistTarget)
    '                If Not mCol(vntIndexKey) Is value Then
    '                    If mCol(vntIndexKey).TargetName = value.TargetName Then
    '                        mCol.Add(value, "<temp>", value.TargetName)
    '                        mCol.Remove(value.TargetName)
    '                        mCol.Add(value, value.TargetName, "<temp>")
    '                        mCol.Remove("<temp>")
    '                    Else
    '                        Try
    '                            mCol.Add(value, value.TargetName, vntIndexKey)
    '                            mCol.Remove(vntIndexKey + 1)
    '                        Catch ex As Exception
    '                            Throw New Exception("That target is already used.")
    '                        End Try
    '                    End If
    '                End If
    '            End Set
    '        End Property

    '        Public ReadOnly Property Contains(ByVal vntIndexKey As Object) As Boolean
    '            Get
    '                Return mCol.Contains(vntIndexKey)
    '            End Get
    '        End Property


    '        Public ReadOnly Property Count() As Long
    '            Get
    '                'used when retrieving the number of elements in the
    '                'collection. Syntax: Debug.Print x.Count
    '                Count = mCol.Count
    '            End Get
    '        End Property

    '        Public Sub Remove(ByVal vntIndexKey As Object)
    '            'used when removing an element from the collection
    '            'vntIndexKey contains either the Index or Key, which is why
    '            'it is declared as a Variant
    '            'Syntax: x.Remove(xyz)


    '            mCol.Remove(vntIndexKey)
    '        End Sub

    '        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
    '            GetEnumerator = mCol.GetEnumerator
    '        End Function

    '        Public Sub New(ByVal Main As cKampanj)
    '            mCol = New Collection
    '            MainObject = Main
    '        End Sub

    '        Protected Overrides Sub Finalize()
    '            mCol = Nothing
    '            MyBase.Finalize()
    '        End Sub

    '    End Class

    '    Public Class cFilm

    '        Private mvarFilmcode As String = ""
    '        Private mvarName As String = ""
    '        Private mvarFilmLength As Byte
    '        Private mvarIndex As Single
    '        Private mvarShare As Integer
    '        Private mvarDescription As String = ""
    '        Private mvarIsVisible As Boolean

    '        Private Main As cKampanj
    '        Private mvarBookingtype As cBookingType
    '        Private ParentColl As Collection

    '        Friend WriteOnly Property ParentCollection() As Collection
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : ParentCollection
    '            ' DateTime  : 2003-07-07 13:18
    '            ' Author    : joho
    '            ' Purpose   : Sets the Collection of wich this film is a member. This is used
    '            '             when a new Filmcode is set. See that property for further explanation
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Set(ByVal value As Collection)
    '                ParentColl = value
    '            End Set

    '        End Property

    '        Friend WriteOnly Property Bookingtype() As cBookingType
    '            Set(ByVal value As cBookingType)
    '                mvarBookingtype = value
    '            End Set
    '        End Property

    '        Friend WriteOnly Property MainObject() As cKampanj
    '            Set(ByVal value As cKampanj)
    '                Main = value
    '            End Set
    '        End Property

    '        Public Property Filmcode() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Filmcode
    '            ' DateTime  : 2003-07-13 17:27
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the filmcode for this film
    '            '---------------------------------------------------------------------------------------
    '            Get
    '                On Error GoTo Filmcode_Error

    '                Filmcode = mvarFilmcode

    '                On Error GoTo 0
    '                Exit Property

    'Filmcode_Error:

    '                Err.Raise(Err.Number, "cFilm: Filmcode", Err.Description)
    '            End Get
    '            Set(ByVal value As String)

    '                On Error GoTo Filmcode_Error

    '                mvarFilmcode = value

    '                On Error GoTo 0
    '                Exit Property

    'Filmcode_Error:

    '                Err.Raise(Err.Number, "cFilm: Filmcode", Err.Description)

    '            End Set
    '        End Property

    '        Public Property FilmLength() As Byte
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : FilmLength
    '            ' DateTime  : 2003-07-13 17:27
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the length in seconds of this film
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo FilmLength_Error

    '                FilmLength = mvarFilmLength

    '                On Error GoTo 0
    '                Exit Property

    'FilmLength_Error:

    '                Err.Raise(Err.Number, "cFilm: FilmLength", Err.Description)
    '            End Get
    '            Set(ByVal value As Byte)
    '                On Error GoTo FilmLength_Error

    '                mvarFilmLength = value

    '                On Error GoTo 0
    '                Exit Property

    'FilmLength_Error:

    '                Err.Raise(Err.Number, "cFilm: FilmLength", Err.Description)

    '            End Set
    '        End Property

    '        Public Property Index() As Single
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Index
    '            ' DateTime  : 2003-07-13 17:27
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the filmlength index for this film
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Index_Error

    '                Index = mvarIndex

    '                On Error GoTo 0
    '                Exit Property

    'Index_Error:

    '                Err.Raise(Err.Number, "cFilm: Index", Err.Description)
    '            End Get
    '            Set(ByVal value As Single)
    '                On Error GoTo Index_Error

    '                mvarIndex = value

    '                On Error GoTo 0
    '                Exit Property

    'Index_Error:

    '                Err.Raise(Err.Number, "cFilm: Index", Err.Description)

    '            End Set
    '        End Property

    '        Public Property Share() As Integer
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Share
    '            ' DateTime  : 2003-07-13 17:27
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets the planned share of contacts for this film in percent
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Share_Error

    '                Share = mvarShare

    '                On Error GoTo 0
    '                Exit Property

    'Share_Error:

    '                Err.Raise(Err.Number, "cFilm: Share", Err.Description)
    '            End Get
    '            Set(ByVal value As Integer)
    '                On Error GoTo Share_Error

    '                mvarShare = value

    '                On Error GoTo 0
    '                Exit Property

    'Share_Error:

    '                Err.Raise(Err.Number, "cFilm: Share", Err.Description)

    '            End Set
    '        End Property

    '        Public Property Description() As String
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : Description
    '            ' DateTime  : 2003-07-13 17:27
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets a description for this film
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo Description_Error

    '                Description = mvarDescription

    '                On Error GoTo 0
    '                Exit Property

    'Description_Error:

    '                Err.Raise(Err.Number, "cFilm: Description", Err.Description)
    '            End Get
    '            Set(ByVal value As String)
    '                On Error GoTo Description_Error

    '                mvarDescription = value

    '                On Error GoTo 0
    '                Exit Property

    'Description_Error:

    '                Err.Raise(Err.Number, "cFilm: Description", Err.Description)

    '            End Set
    '        End Property

    '        Public Property IsVisible() As Boolean
    '            '---------------------------------------------------------------------------------------
    '            ' Procedure : IsVisible
    '            ' DateTime  : 2003-07-13 17:28
    '            ' Author    : joho
    '            ' Purpose   : Returns/sets wether this filmcode should be visible in the charts
    '            '---------------------------------------------------------------------------------------
    '            '
    '            Get
    '                On Error GoTo IsVisible_Error

    '                IsVisible = mvarIsVisible

    '                On Error GoTo 0
    '                Exit Property

    'IsVisible_Error:

    '                Err.Raise(Err.Number, "cFilm: IsVisible", Err.Description)
    '            End Get
    '            Set(ByVal value As Boolean)
    '                On Error GoTo IsVisible_Error

    '                mvarIsVisible = value

    '                On Error GoTo 0
    '                Exit Property

    'IsVisible_Error:

    '                Err.Raise(Err.Number, "cFilm: IsVisible", Err.Description)


    '            End Set
    '        End Property

    '        Public Property Name() As String
    '            Get
    '                Name = mvarName
    '            End Get
    '            Set(ByVal value As String)
    '                Dim TmpFilm As cFilm

    '                If value <> mvarName Then

    '                    If (Not ParentColl Is Nothing) AndAlso ParentColl.Contains(mvarName) Then
    '                        TmpFilm = ParentColl(mvarName)
    '                        If ParentColl.Contains(value) Then
    '                            Err.Raise(vbObjectError + 600, "cFilm.Name", "Two films can not share name.")
    '                        End If
    '                        ParentColl.Remove(mvarName)
    '                        ParentColl.Add(TmpFilm, value)
    '                    End If
    '                End If
    '                mvarName = value
    '            End Set
    '        End Property

    '        Public Function FilmString() As String
    '            Dim Tmpstr As String
    '            Dim TmpChan As cChannel
    '            Dim TmpBT As cBookingType
    '            Dim TmpWeek As cWeek

    '            Tmpstr = ""
    '            For Each TmpChan In Main.Channels
    '                For Each TmpBT In TmpChan.BookingTypes
    '                    For Each TmpWeek In TmpBT.Weeks
    '                        If TmpBT.BookIt Then
    '                            If InStr(Tmpstr, TmpWeek.Films(mvarName).Filmcode & ",") = 0 Then
    '                                If TmpWeek.Films(mvarName).Filmcode <> "" Then
    '                                    Tmpstr = Tmpstr + TmpWeek.Films(mvarName).Filmcode & ","
    '                                End If
    '                            End If
    '                        End If
    '                    Next
    '                Next
    '            Next
    '            If Len(Tmpstr) = 0 Then
    '                FilmString = ""
    '            Else
    '                FilmString = Left(Tmpstr, Len(Tmpstr) - 1)
    '            End If
    '        End Function

    '        Public Sub New(ByVal MainObject As cKampanj)
    '            Main = MainObject
    '            mvarIsVisible = True
    '        End Sub

    '    End Class

    '    Public Class cFilms
    '        Implements Collections.IEnumerable

    '        'local variable to hold collection
    '        Private mCol As Collection
    '        Private Main As cKampanj
    '        Private mvarBookingtype As cBookingType

    '        Friend WriteOnly Property Bookingtype() As cBookingType
    '            Set(ByVal value As cBookingType)
    '                Dim TmpFilm As cFilm

    '                mvarBookingtype = value
    '                For Each TmpFilm In mCol
    '                    TmpFilm.Bookingtype = value
    '                Next
    '            End Set

    '        End Property

    '        Friend WriteOnly Property MainObject() As cKampanj

    '            Set(ByVal value As cKampanj)
    '                Dim TmpFilm As cFilm

    '                Main = value
    '                For Each TmpFilm In mCol
    '                    TmpFilm.MainObject = value
    '                Next
    '            End Set

    '        End Property

    '        Public Function Add(ByVal Name As String) As cFilm
    '            'create a new object
    '            Dim objNewMember As cFilm
    '            Dim e As Long

    '            objNewMember = New cFilm(Main)


    '            'set the properties passed into the method
    '            objNewMember.Name = Trim(Name)
    '            objNewMember.ParentCollection = mCol
    '            objNewMember.Bookingtype = mvarBookingtype

    '            On Error Resume Next
    '            mCol.Add(objNewMember, Trim(Name))
    '            e = Err.Number
    '            On Error GoTo 0


    '            'return the object created
    '            If e = 0 Then
    '                Add = objNewMember
    '            Else
    '                Add = mCol(Name)
    '            End If
    '            objNewMember = Nothing


    '        End Function

    '        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cFilm
    '            Get
    '                Dim TmpFilm As cFilm
    '                Dim e As Long

    '                '    used when referencing an element in the collection
    '                '    vntIndexKey contains either the Index or Key to the collection,
    '                '    this is why it is declared as a Variant
    '                '    Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
    '                On Error GoTo Item_Error

    '                If vntIndexKey.GetType.Name = "String" Then
    '                    If mCol.Contains(vntIndexKey) Then
    '                        Item = mCol(vntIndexKey)
    '                    Else
    '                        Item = Nothing
    '                        For Each TmpFilm In mCol
    '                            If InStr("," & UCase(TmpFilm.Filmcode) & ",", "," & UCase(vntIndexKey) & ",") > 0 And ((vntIndexKey <> "" And TmpFilm.Filmcode <> "") Or (vntIndexKey = "" And TmpFilm.Filmcode = "")) Then
    '                                Item = TmpFilm
    '                                Exit Property
    '                            End If
    '                        Next
    '                    End If
    '                Else
    '                    Return mCol(vntIndexKey)
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'Item_Error:
    '                e = Err.Number

    '                If Err.Number = 5 Then
    '                    For Each TmpFilm In mCol
    '                        If InStr("," & UCase(TmpFilm.Filmcode) & ",", "," & UCase(vntIndexKey) & ",") > 0 And vntIndexKey <> "" Then
    '                            Item = TmpFilm
    '                            Exit Property
    '                        End If
    '                    Next
    '                End If
    '                Err.Raise(e, "cFilms", "Unknown Film (" & vntIndexKey & ")")
    '            End Get
    '        End Property

    '        Public ReadOnly Property Count() As Long
    '            Get
    '                'used when retrieving the number of elements in the
    '                'collection. Syntax: Debug.Print x.Count
    '                Count = mCol.Count
    '            End Get
    '        End Property

    '        Public Sub Remove(ByVal vntIndexKey As Object)
    '            'used when removing an element from the collection
    '            'vntIndexKey contains either the Index or Key, which is why
    '            'it is declared as a Variant
    '            'Syntax: x.Remove(xyz)


    '            mCol.Remove(vntIndexKey)
    '        End Sub

    '        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
    '            GetEnumerator = mCol.GetEnumerator
    '        End Function

    '        Public Sub New(ByVal MainObject As cKampanj)
    '            Main = MainObject
    '            mCol = New Collection
    '        End Sub

    '        Protected Overrides Sub Finalize()
    '            mCol = Nothing
    '            MyBase.Finalize()
    '        End Sub

    '    End Class


    
    '    Public Class cKampanj
    '        '---------------------------------------------------------------------------------------
    '        ' Purpose   : The class holds all possible information about a campagin           
    '        '---------------------------------------------------------------------------------------


    '        Private Const MY_VERSION As Short = 40

    '        Private Ini As New clsIni 'Declares a *.INI reader object


    '        Private mvarArea As String 'a two letter string which define what country a campaign in created in
    '        Private mvarAreaLog As String 'a additional coutry setting (in case there is different settings in different parts of the country)
    '        Private mvarActualSpots As New cActualSpots(Me) 'Actual spots hold the spots planned bokked and confirmed
    '        Private mvarActualTotCTC As Decimal ' actual Cost To CLient
    '        Private mvarAllAdults As String ' A string with all measurable persons (the hole population)
    '        Private mvarPlannedSpots As New cPlannedSpots 'The planned spots (not booked and not confirmed)
    '        Private mvarBookedSpots As New cBookedSpots(Me) 'the planned and booked spots (not confimed)
    '        Private mvarBudgetTotalCTC As Decimal
    '        Private mvarBuyer As String 'The buyers name (the media agent)

    '        Private mvarCancelled As Byte
    '        Private mvarClient As String 'the client/customer
    '        Private mvarClientID As Short 'client/customer ID
    '        Private mvarCommentary As String 'A string with all text writen in the Notepad window
    '        Private mvarControlFilmcodeFromClient As Boolean ' sets true if film codes has been recieved from client
    '        Private mvarControlFilmcodeToBureau As Boolean ' sets true if the film is send to the creative bureau
    '        Private mvarControlFilmcodeToChannels As Boolean ' sets true if the film is send to the channels
    '        Private mvarControlOKFromClient As Boolean ' Is set true if the client has agreed to the campaign
    '        Private mvarControlTracking As Boolean
    '        Private mvarControlFollowedUp As Boolean ' true if the campaign has been followed up
    '        Private mvarControlFollowUpToClient As Boolean ' True if the follow up has been send to the customer
    '        Private mvarControlTransferredToMatrix As Boolean
    '        Private mvarCosts As New cCosts 'a collection of costs
    '        Private mvarContract As cContract 'a contract (cheaper prices)

    '        Public DaypartCount As Byte
    '        Private mvarDaypartName(5) As String
    '        Private mvarDaypartStart(5) As Short
    '        Private mvarDaypartEnd(5) As Short
    '        Private mvarDebugPath As String
    '        Private mvarFilename As String
    '        Private mvarFrequencyFocus As Byte
    '        Private mvarName As String
    '        Private mvarVersion As Byte
    '        Private mvarPlanner As String
    '        Private mvarUpdatedTo As Integer

    '        Private mvarMainTarget As New cTarget(Me) 'sets the main targeted age area
    '        Private mvarSecondaryTarget As New cTarget(Me) 'sets the secondary targeted age area
    '        Private mvarThirdTarget As New cTarget(Me) 'sets the targeted age area number three

    '        'Private mvarRTColl As New Dictionary(Of String Dictionary(Of Object, Byte))
    '        Private mvarReachGoal(0 To 10) As Single

    '        Private mvarProduct As String 'The campaign product
    '        Private mvarProductID As Short 'the campaign product ID

    '        Private mvarInternalAdedge As ConnectWrapper.Brands

    '        Private mvarTargColl As Collection
    '        Private mvarUniColl As Collection
    '        Private mvarTargStr As String
    '        Private mvarUniStr As String

    '        Private mvarMarathonCTC As Single 'holds the cost that is to be charged of the client

    '        'a collection of strings containing information about the universes applyed
    '        Private mvarUniverses As New Collections.Specialized.NameValueCollection

    '        Public ID As String 'Campaign ID

    '        Public ExtendedInfos As New cWrapper
    '        Public EstimationPeriods As New cWrapper

    '        Public RFEstimation As New cReachguide
    '        Public Campaigns As Collections.Generic.IDictionary(Of String, Trinity.cKampanj)
    '        Public History As New Dictionary(Of String, Trinity.cKampanj)
    '        Public RootCampaign As cKampanj
    '        Public HistoryComment As String
    '        Public WasExportedToMarathon As Boolean
    '        Private mvarHistoryDate As Date

    '        Private mvarChannels As New cChannels(Me) 'a collection of Channels

    '        Private mvarAdedge As ConnectWrapper.Brands = New ConnectWrapper.Brands
    '        Public Loading As Boolean
    '        Public Saving As Boolean
    '        Public MarathonPlanNr As Integer
    '        Private mvarMarathonOtherOrder As Integer

    '        Public Event ReadNewSpot(ByVal SpotNr As Integer, ByVal SpotCount As Integer)


    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : Universes
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   : 
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public ReadOnly Property Universes() As Collections.Specialized.NameValueCollection
    '            Get
    '                Return mvarUniverses
    '            End Get
    '        End Property


    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : MarathonCTC
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property MarathonCTC() As Single
    '            Get
    '                Return mvarMarathonCTC
    '            End Get
    '            Set(ByVal value As Single)
    '                mvarMarathonCTC = value
    '            End Set
    '        End Property




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : GetNewActualSpots
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Sub GetNewActualSpots()

    '            Dim SpotCount As Integer
    '            Dim i As Integer
    '            Dim TmpSpot As cActualSpot
    '            If Not Me.RootCampaign Is Nothing Then Exit Sub
    '            Helper.WriteToLogFile("GetNewActualSpots: Register Connect.dll")
    '            Dim TmpAdedge As New ConnectWrapper.Brands
    '            Helper.WriteToLogFile("OK")
    '            If mvarChannels(1).BookingTypes(1).Weeks.Count = 0 Then Exit Sub 'if there are no bookings then exit

    '            TmpAdedge.clearList()
    '            If mvarUpdatedTo < StartDate - 1 Then
    '                mvarUpdatedTo = StartDate - 1
    '            End If
    '            TmpAdedge.setPeriod(Format(Date.FromOADate(mvarUpdatedTo).AddDays(1), "ddMMyy") & "-" & Format(Date.FromOADate(EndDate), "ddMMyy"))
    '            TmpAdedge.setArea(mvarArea)
    '            TmpAdedge.setChannelsArea(ChannelString)
    '            TmpAdedge.setTargetMnemonic(Helper.CreateTargetString(True, True))
    '            TmpAdedge.setBrandFilmCode(mvarAreaLog, FilmcodeString() & ",Ö")
    '            SpotCount = TmpAdedge.Run(True, False, -1)
    '            TmpAdedge.sort("date(asc),fromtime(asc)")

    '            For i = 0 To SpotCount - 1
    '                RaiseEvent ReadNewSpot(i, SpotCount)
    '                TmpSpot = mvarActualSpots.Add(Date.FromOADate(TmpAdedge.getAttrib(Connect.eAttribs.aDate, i)), TmpAdedge.getAttrib(Connect.eAttribs.aFromTime, i) \ 60)
    '                TmpSpot.AdedgeChannel = TmpAdedge.getAttrib(Connect.eAttribs.aChannel, i)
    '                TmpSpot.Advertiser = TmpAdedge.getAttrib(Connect.eAttribs.aBrandAdvertiser, i)
    '                If TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, i) = TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, i) Then
    '                    TmpSpot.BreakType = cActualSpot.EnumBreakType.btBlock
    '                Else
    '                    TmpSpot.BreakType = cActualSpot.EnumBreakType.btBreak
    '                End If
    '                TmpSpot.Channel = Helper.Adedge2Channel(TmpSpot.AdedgeChannel)
    '                TmpSpot.Bookingtype = TmpSpot.Channel.BookingTypes(1)
    '                TmpSpot.Deactivated = False
    '                TmpSpot.Filmcode = TmpAdedge.getAttrib(Connect.eAttribs.aBrandFilmCode, i)
    '                TmpSpot.PosInBreak = TmpAdedge.getAttrib(Connect.eAttribs.aBrandSpotInBreak, i)
    '                TmpSpot.SpotsInBreak = TmpAdedge.getAttrib(Connect.eAttribs.aBrandSpotCount, i)
    '                TmpSpot.Product = TmpAdedge.getAttrib(Connect.eAttribs.aBrandProduct, i)
    '                TmpSpot.ProgAfter = TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, i)
    '                TmpSpot.ProgBefore = TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, i)
    '                TmpSpot.Programme = TmpSpot.ProgAfter
    '                TmpSpot.SpotLength = TmpAdedge.getAttrib(Connect.eAttribs.aBrandDurationNom, i)
    '                TmpSpot.week = TmpSpot.Bookingtype.GetWeek(Date.FromOADate(TmpSpot.AirDate))
    '                If TmpSpot.week Is Nothing Then
    '                    TmpSpot.week = TmpSpot.Bookingtype.Weeks(1)
    '                End If
    '            Next
    '            If SpotCount > 0 Then
    '                CreateAdedgeSpots()
    '            End If
    '            If TmpAdedge.getDataRangeTo(Connect.eDataType.mSpot) < Campaign.EndDate Then
    '                mvarUpdatedTo = TmpAdedge.getDataRangeTo(Connect.eDataType.mSpot)
    '            Else
    '                mvarUpdatedTo = Campaign.EndDate
    '            End If
    '        End Sub





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : CreateAdegeSpots
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Sub CreateAdedgeSpots()

    '            Dim TmpSpot As cActualSpot
    '            Dim SpotCount As Long

    '            mvarAdedge.clearList()
    '            mvarAdedge.clearBrandFilter()
    '            mvarAdedge.clearGroup()
    '            mvarAdedge.setArea(mvarArea)
    '            mvarAdedge.setPeriod(Format(Date.FromOADate(StartDate), "ddMMyy") & "-" & Format(Date.FromOADate(EndDate), "ddMMyy"))
    '            mvarAdedge.setChannelsArea(ChannelString)
    '            mvarAdedge.setTargetMnemonic(Helper.CreateTargetString(True, False))
    '            mvarAdedge.setUniverseUserDefined(mvarUniStr)
    '            mvarAdedge.sort("")

    '            For Each TmpSpot In mvarActualSpots
    '                SpotCount = mvarAdedge.addBrand(Format(Date.FromOADate(TmpSpot.AirDate), "ddMMyy"), Helper.Mam2Tid(TmpSpot.MaM), TmpSpot.AdedgeChannel, TmpSpot.SpotLength)
    '            Next
    '            SpotCount = mvarAdedge.Run(True, False, 10)
    '            mvarAdedge.sort("date(asc),fromtime(asc)")

    '        End Sub



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : CalculateSpots
    '        ' DateTime  : 2006-03-23 10:07
    '        ' Author    : joho
    '        ' Purpose   : Calculate ratings of spots
    '        ' Parameters:
    '        '               CalculateReach As Boolean = True
    '        '                   Should reach be calculated?
    '        '               FromDate As Date = -1,ToDate As Date = -1
    '        '                   Calculate spots between these dates
    '        '               FreqFocus As Byte = 1
    '        '                   What is the Frequency focus?
    '        '               UseFilters As Boolean = True
    '        '                   Should the filters in trinityfilters.ini be used?
    '        '                   When set to true, all parameters after this are ignored
    '        '               Chan As cChannel = Nothing
    '        '                   Only calculate spots in the specified channel
    '        '               OnlyWeek = -1
    '        '                   Only calculate spots in the specified week
    '        '               Bookingtype As cBookingType = Nothing
    '        '                   Only calculate spots in the specified bookingtype
    '        '               Film As cFilm = Nothing
    '        '                   Only calculate spots with the specified film
    '        '               Daypart = -1
    '        '                   Only calculate spots in the specified daypart
    '        '               PosInBreak = -1
    '        '                   Only calculate spots with the specified position in break
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Function CalculateSpots(Optional ByVal CalculateReach As Boolean = True, Optional ByVal FromDate As Date = Nothing, Optional ByVal ToDate As Date = Nothing, Optional ByVal FreqFocus As Byte = 1, Optional ByVal UseFilters As Boolean = True, Optional ByVal Chan As cChannel = Nothing, Optional ByVal OnlyWeek As String = "", Optional ByVal Bookingtype As cBookingType = Nothing, Optional ByVal Film As cFilm = Nothing, Optional ByVal Daypart As Integer = -1, Optional ByVal PosInBreak As Integer = -1) As Single

    '            Dim s As Integer
    '            Dim ContinueThis() As Boolean
    '            Dim Cont As Boolean
    '            Dim q As Integer
    '            Dim LowBound As Integer
    '            Dim SpotCount As Integer

    '            'if there are no ActualSpots we cant make any calculations
    '            If mvarActualSpots.Count = 0 Then Exit Function

    '            'sets the boolean array length to match the number of ActualSpots
    '            ReDim ContinueThis(mvarActualSpots.Count)

    '            mvarAdedge.clearGroup()
    '            For s = 1 To mvarActualSpots.Count
    '                'sets the Array(s) to False
    '                ContinueThis(s) = False
    '                'if we are using filters 
    '                If UseFilters Then
    '                    'There has to be a ActualSpot and a matching Film for a certain channel, in a certain week
    '                    'If that is true, the we set ContinuwThis = true
    '                    If GeneralFilter.Data("Channels", mvarActualSpots(s).Channel.ChannelName) Then
    '                        If Not mvarActualSpots(s).week Is Nothing AndAlso GeneralFilter.Data("Weeks", mvarActualSpots(s).week.Name) Then
    '                            If GeneralFilter.Data("Bookingtypes", mvarActualSpots(s).Bookingtype.Name) Then
    '                                If Not mvarActualSpots(s).week Is Nothing AndAlso Not mvarActualSpots(s).week.Films(mvarActualSpots(s).Filmcode) Is Nothing AndAlso GeneralFilter.Data("Films", mvarActualSpots(s).week.Films(mvarActualSpots(s).Filmcode).Name) Then
    '                                    If GeneralFilter.Data("Dayparts", mvarDaypartName(Helper.GetDaypart(mvarActualSpots(s).MaM))) Then
    '                                        If Not mvarActualSpots(s).Deactivated Then
    '                                            ContinueThis(s) = True
    '                                        End If
    '                                    End If
    '                                End If
    '                            End If
    '                        End If
    '                    End If
    '                Else 'if we are not useing filters we continue here
    '                    Cont = False
    '                    If Chan Is Nothing Then
    '                        Cont = True
    '                    Else
    '                        If mvarActualSpots(s).Channel Is Chan Then
    '                            Cont = True
    '                        End If
    '                    End If
    '                    If Cont Then
    '                        Cont = False
    '                        If Bookingtype Is Nothing Then
    '                            Cont = True
    '                        Else
    '                            If mvarActualSpots(s).Bookingtype Is Bookingtype Then
    '                                Cont = True
    '                            End If
    '                        End If
    '                        If Cont Then
    '                            Cont = False
    '                            If OnlyWeek = "" Then
    '                                Cont = True
    '                            Else
    '                                If mvarActualSpots(s).week.Name = OnlyWeek Then
    '                                    Cont = True
    '                                End If
    '                            End If
    '                            If Cont Then
    '                                Cont = False
    '                                If Film Is Nothing Then
    '                                    Cont = True
    '                                Else
    '                                    If mvarActualSpots(s).week.Films(Film.Name).Filmcode = mvarActualSpots(s).week.Films(mvarActualSpots(s).Filmcode).Filmcode Then
    '                                        Cont = True
    '                                    End If
    '                                End If
    '                                If Cont Then
    '                                    Cont = False
    '                                    If Daypart = -1 Then
    '                                        Cont = True
    '                                    Else
    '                                        If Daypart = Helper.GetDaypart(mvarActualSpots(s).MaM) Then
    '                                            Cont = True
    '                                        End If
    '                                    End If
    '                                    If Cont Then
    '                                        Cont = False
    '                                        If PosInBreak = -1 Then
    '                                            Cont = True
    '                                        Else
    '                                            If mvarActualSpots(s).PosInBreak <= TrinitySettings.PPFirst Then
    '                                                If PosInBreak = 1 Then
    '                                                    Cont = True
    '                                                End If
    '                                            ElseIf mvarActualSpots(s).PosInBreak >= mvarActualSpots(s).SpotsInBreak + 1 - TrinitySettings.PPLast Then
    '                                                If PosInBreak = 3 Then
    '                                                    Cont = True
    '                                                End If
    '                                            Else
    '                                                If PosInBreak = 2 Then
    '                                                    Cont = True
    '                                                End If
    '                                            End If
    '                                        End If
    '                                        If Cont Then
    '                                            Cont = False
    '                                            If FromDate = Nothing Then
    '                                                Cont = True
    '                                            Else
    '                                                If FromDate.ToOADate <= mvarActualSpots(s).AirDate Then
    '                                                    Cont = True
    '                                                End If
    '                                            End If
    '                                            If Cont Then
    '                                                Cont = False
    '                                                If ToDate = Nothing Then
    '                                                    Cont = True
    '                                                Else
    '                                                    If ToDate.ToOADate >= mvarActualSpots(s).AirDate Then
    '                                                        Cont = True
    '                                                    End If
    '                                                End If
    '                                                If Cont Then
    '                                                    ContinueThis(s) = True
    '                                                End If
    '                                            End If
    '                                        End If
    '                                    End If
    '                                End If
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '                'If either the Filter sections or the non filter section has set ContinueThis = true we continue here
    '                If ContinueThis(s) Then
    '                    '            On Error Resume Next
    '                    '            If ActiveTarget = eMainTarget Then
    '                    '                t = mvarMainTarget
    '                    '                u = mvarMainTarget.Universe
    '                    '                Dummy = mvarTargColl(t)
    '                    '                If Err.Number > 0 Then
    '                    '                    Dummy = mvarMainTarget.UniSize
    '                    '                End If
    '                    '            ElseIf ActiveTarget = eSecondaryTarget Then
    '                    '                t = mvarSecondaryTarget
    '                    '                u = mvarSecondaryTarget.Universe
    '                    '                Dummy = mvarTargColl(t)
    '                    '                If Err.Number > 0 Then
    '                    '                    Dummy = mvarSecondaryTarget.UniSize
    '                    '                End If
    '                    '            ElseIf ActiveTarget = eBuyingTarget Then
    '                    '                t = mvarActualSpots(s).Bookingtype.BuyingTarget.Target.TargetName
    '                    '                u = mvarActualSpots(s).Channel.BuyingUniverse
    '                    '                Dummy = mvarTargColl(t)
    '                    '                If Err.Number > 0 Then
    '                    '                    Dummy = mvarActualSpots(s).Bookingtype.BuyingTarget.Target.UniSize
    '                    '                End If
    '                    '            ElseIf ActiveTarget = eAllAdults Then
    '                    '                t = mvarAllAdults
    '                    '                u = ""
    '                    '            End If

    '                    q = 0
    '                    If InStr(UCase(mvarActualSpots(s).Channel.AdEdgeNames) & ",", UCase(mvarAdedge.getAttrib(Connect.eAttribs.aChannel, s - 1)) & ",") = 0 Then
    '                        LowBound = -5
    '                        If s + LowBound - 1 < 0 Then
    '                            LowBound = -s + 1
    '                        End If
    '                        For q = LowBound To 5
    '                            '                    If s + q - 1 < 0 Then
    '                            '                        q = q - (s + q) + 1
    '                            '                    End If
    '                            If mvarActualSpots(s).MaM = mvarAdedge.getAttrib(Connect.eAttribs.aFromTime, s + q - 1) \ 60 Then
    '                                If InStr(UCase(mvarActualSpots(s).Channel.AdEdgeNames) & ",", UCase(mvarAdedge.getAttrib(Connect.eAttribs.aChannel, s + q - 1)) & ",") > 0 Then
    '                                    If mvarActualSpots(s).AirDate = mvarAdedge.getAttrib(Connect.eAttribs.aDate, s + q - 1) Then
    '                                        Exit For
    '                                    End If
    '                                End If
    '                            End If
    '                        Next
    '                    End If
    '                    mvarAdedge.deleteFromGroup(s + q - 1, False)
    '                    On Error GoTo ErrHandle
    '                    mvarActualSpots(s).GroupIdx = mvarAdedge.addToGroup(s + q - 1) - 1


    '                Else 'If ContinueThis was false we go from here
    '                    q = 0
    '                    If InStr(UCase(mvarActualSpots(s).Channel.AdEdgeNames), UCase(mvarAdedge.getAttrib(Connect.eAttribs.aChannel, s - 1))) = 0 Then
    '                        LowBound = -5
    '                        'for the 5 first times through the loop LowBound is is 1-s (1- TimesThroughLoop) after that it will be -5
    '                        If s + LowBound - 1 < 0 Then
    '                            LowBound = 1 - s
    '                        End If
    '                        '                   s =   1, 2, 3, 4, 5, 6, 7, 8, 9,10,*
    '                        '           LowBound =    0,-1,-2,-3,-4,-5,-5,-5,-5,-5,*
    '                        'this code will be looped 5, 6, 7, 8, 9,10,10,10,10,10,*
    '                        '                   q =   0,-1,-2,-3,-4,-5,-5,-5,-5,-5,*
    '                        For q = LowBound To 5
    '                            'if we have been through all the ActualSpots we exit the loop
    '                            If s + q - 1 > mvarActualSpots.Count Then
    '                                Debug.Assert(False)
    '                                Exit For
    '                            End If

    '                            If mvarActualSpots(s).MaM = mvarAdedge.getAttrib(Connect.eAttribs.aFromTime, s + q - 1) \ 60 Then
    '                                If InStr(UCase(mvarActualSpots(s).Channel.AdEdgeNames), UCase(mvarAdedge.getAttrib(Connect.eAttribs.aChannel, s + q - 1))) > 0 Then
    '                                    If mvarActualSpots(s).AirDate = mvarAdedge.getAttrib(Connect.eAttribs.aDate, s + q - 1) Then
    '                                        Exit For
    '                                    End If
    '                                End If
    '                            End If
    '                        Next
    '                    End If
    '                    mvarAdedge.deleteFromGroup(s + q - 1, False)
    '                End If
    '                'end of the Loop For s = 1 To mvarActualSpots.Count
    '            Next

    '            If CalculateReach Then
    '                'Calculate the reach
    '                SpotCount = mvarAdedge.recalcRF(Connect.eSumModes.smGroup)
    '            End If
    '            Exit Function

    'ErrHandle:
    '        End Function



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ReachTargetEnum
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Enum ReachTargetEnum
    '            rteMainTarget = 0
    '            rteSecondTarget = 1
    '            rteThirdTarget = 2
    '            rteAllAdults = 4
    '            rteCustomTarget = 5
    '        End Enum



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : HistoryDate
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property HistoryDate() As Date
    '            Get
    '                HistoryDate = mvarHistoryDate
    '            End Get
    '            Set(ByVal Value As Date)
    '                mvarHistoryDate = Value
    '            End Set
    '        End Property



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : Channels
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property Channels() As cChannels
    '            Get
    '                'used when retrieving value of a property, on the right side of an assignment.
    '                'Syntax: Debug.Print X.Channels
    '                Channels = mvarChannels
    '            End Get
    '            Set(ByVal Value As cChannels)
    '                'used when assigning an Object to the property, on the left side of a Set statement.
    '                'Syntax: Set x.Channels = Form1
    '                mvarChannels = Value
    '                mvarChannels.MainObject = Me
    '            End Set
    '        End Property
    '        '
    '        '**************************************************************************************************************
    '        'Public Function LoadCampaign(Path As String) As Byte
    '        '
    '        '    Dim i As Integer
    '        '    Dim j As Integer
    '        '
    '        '    Dim Channels As Integer
    '        '    Dim c As Integer
    '        '    Dim BTs As Integer
    '        '    Dim b As Integer
    '        '    Dim Weeks As Integer
    '        '    Dim w As Integer
    '        '    Dim Films As Integer
    '        '    Dim f As Integer
    '        '    Dim PLTs As Integer
    '        '    Dim plt As Integer
    '        '    Dim Spots As Integer
    '        '    Dim s As Integer
    '        '    Dim Count As Integer
    '        '    Dim Group As String
    '        '    Dim Freqs As Integer
    '        '    Dim Freq As Integer
    '        '    Dim Reach As Byte
    '        '    Dim ChangeDate As Date
    '        '    Dim Who As String
    '        '    Dim Activity As String
    '        '    Dim ChangeTime As String
    '        '    Dim a As Integer
    '        '    Dim Dummy As Single
    '        '
    '        '    Dim TmpString As String
    '        '
    '        '    Dim TmpChannel As cChannel
    '        '    Dim TmpBookingtype As cBookingType
    '        '    Dim TmpWeek As cWeek
    '        '    Dim TmpFilm As cFilm
    '        '    Dim TmpPLTarget As cPriceListTarget
    '        '    Dim TmpPlannedSpot As cPlannedSpot
    '        '    Dim TmpActualSpot As cActualSpot
    '        '
    '        '    Dim TmpByte As Byte
    '        '    Dim TmpInt As Integer
    '        '    Dim TmpLong As Long
    '        '    Dim TmpCurr As Currency
    '        '    Dim TmpSng As Single
    '        '    Dim TmpStr As String
    '        '    Dim CurrentlyReading As String
    '        '    Dim StartPos As Long
    '        '
    '        '    On Error GoTo ErrHandle
    '        '
    '        '    If Path = "" Then
    '        '        LoadCampaign = 1
    '        '        Exit Function
    '        '    End If
    '        '
    '        '    Close 99
    '        '
    '        '    If Loading Or Saving Then Exit Function
    '        '
    '        '    Loading = True
    '        '    On Error GoTo ErrHandle
    '        '    Open Path For Random Lock Read Write As 99 Len = 512
    '        '
    '        '    IniFile.Create TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & mvarArea & "\area.ini"
    '        '
    '        '    mvarVersion = GetVal("Byte")
    '        '    If mvarVersion > MY_VERSION Then
    '        '        MsgBox "This campaign is created with a later version of" & Chr(10) & "Trinity than the one you are currently using.", vbInformation, "TRINITY"
    '        '        LoadCampaign = 1
    '        '        Loading = False
    '        '        Close 99
    '        '        Exit Function
    '        '    End If
    '        '    If mvarVersion < 10 Then
    '        '        Close 99
    '        '        ImportOldCampaign Path
    '        '        Loading = False
    '        '        Exit Function
    '        '    End If
    '        '    If mvarVersion = 10 Then
    '        '        Close 99
    '        '        Open Path For Random Lock Read Write As 99
    '        '        mvarVersion = GetVal("Byte")
    '        '    ElseIf mvarVersion < 13 Then
    '        '        Close 99
    '        '        Open Path For Random Lock Read Write As 99 Len = 256
    '        '        mvarVersion = GetVal("Byte")
    '        '    End If
    '        '    mvarStartDate = GetVal("Long")
    '        '    mvarEndDate = GetVal("Long")
    '        '    mvarName = GetVal("String")
    '        '    mvarUpdatedTo = GetVal("Long")
    '        '    mvarPlanner = GetVal("String")
    '        '    mvarBuyer = GetVal("String")
    '        '    mvarCancelled = GetVal("Byte")
    '        '    mvarFrequencyFocus = GetVal("Byte")
    '        '    mvarFilename = GetVal("String")
    '        '    mvarProduct = GetVal("String")
    '        '    mvarClient = GetVal("String")
    '        '    mvarBudgetTotalCTC = GetVal("Currency")
    '        '    mvarActualTotCTC = GetVal("Currency")
    '        '    mvarServiceFee = GetVal("Single")
    '        '    mvarTrackingCost = GetVal("Currency")
    '        '    mvarTVCheck = GetVal("Currency")
    '        '    mvarCommentary = GetVal("String")
    '        '    mvarControlFilmcodeFromClient = GetVal("Boolean")
    '        '    mvarControlFilmcodeToBureau = GetVal("Boolean")
    '        '    mvarControlFilmcodeToChannels = GetVal("Boolean")
    '        '    mvarControlOKFromClient = GetVal("Boolean")
    '        '    mvarControlTracking = GetVal("Boolean")
    '        '    mvarControlFollowedUp = GetVal("Boolean")
    '        '    mvarControlFollowUpToClient = GetVal("Boolean")
    '        '    mvarControlTransferredToMatrix = GetVal("Boolean")
    '        '    mvarServiceFeeOnNet1 = GetVal("Boolean")
    '        '    mvarArea = GetVal("String")
    '        '    mvarAreaLog = GetVal("String")
    '        '    TmpByte = GetVal("Byte")
    '        '
    '        '    For i = 0 To 5
    '        '
    '        '        mvarDaypartName(i) = GetVal("String")
    '        '        mvarDaypartStart(i) = GetVal("Integer")
    '        '        mvarDaypartEnd(i) = GetVal("Integer")
    '        '
    '        '    Next
    '        '
    '        '    If DaypartCount = 0 Then
    '        '        Area = mvarArea
    '        '    End If
    '        '
    '        '    mvarAllAdults = GetVal("String")
    '        '    If mvarAllAdults = "" Then
    '        '        Ini.Create LocalTrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & "\Data\" & mvarArea & "\Area.ini"
    '        '        mvarAllAdults = Ini.Text("General", "EntirePopulation")
    '        '    End If
    '        '
    '        '    'Load channels and all sub-collections and objects
    '        '
    '        '    Channels = GetVal("Integer")
    '        '
    '        '    Set mvarChannels = New cChannels
    '        '    Set mvarChannels.MainObject = Me
    '        '
    '        '    For c = 1 To Channels
    '        '        TmpString = GetVal("String")
    '        '        Set TmpChannel = mvarChannels.Add(TmpString)
    '        '        TmpChannel.ID = GetVal("Long")
    '        '        TmpChannel.Shortname = GetVal("String")
    '        '        TmpChannel.BuyingUniverse = GetVal("String")
    '        '        TmpChannel.AdEdgeNames = GetVal("String")
    '        '        TmpChannel.DefaultArea = GetVal("String")
    '        '        TmpChannel.AgencyCommission = GetVal("Single")
    '        '
    '        '        'Save the targets
    '        '
    '        '        TmpChannel.MainTarget.NoUniverseSize = True
    '        '        TmpChannel.MainTarget.TargetName = GetVal("String")
    '        '        TmpChannel.MainTarget.TargetType = GetVal("Byte")
    '        '        TmpChannel.MainTarget.Universe = GetVal("String")
    '        '        TmpChannel.MainTarget.SecondUniverse = GetVal("String")
    '        '        If TmpChannel.MainTarget.SecondUniverse = "" Then
    '        '            TmpChannel.MainTarget.SecondUniverse = IniFile.Text("Universe", "Second")
    '        '        End If
    '        '        TmpChannel.MainTarget.NoUniverseSize = False
    '        '
    '        '        TmpChannel.SecondaryTarget.NoUniverseSize = True
    '        '        TmpChannel.SecondaryTarget.TargetName = GetVal("String")
    '        '        TmpChannel.SecondaryTarget.TargetType = GetVal("Byte")
    '        '        TmpChannel.SecondaryTarget.Universe = GetVal("String")
    '        '        TmpChannel.SecondaryTarget.SecondUniverse = GetVal("String")
    '        '        If TmpChannel.SecondaryTarget.SecondUniverse = "" Then
    '        '            TmpChannel.SecondaryTarget.SecondUniverse = IniFile.Text("Universe", "Second")
    '        '        End If
    '        '        TmpChannel.SecondaryTarget.NoUniverseSize = False
    '        '
    '        '        'Read Booking types
    '        '
    '        '        BTs = GetVal("Integer")
    '        '
    '        '        For b = 1 To BTs
    '        '
    '        '            TmpString = GetVal("String")
    '        '            Set TmpBookingtype = TmpChannel.BookingTypes.Add(TmpString)
    '        '            TmpBookingtype.Shortname = GetVal("String")
    '        '
    '        '            'Read Buyingtarget
    '        '
    '        '            TmpBookingtype.BuyingTarget.CalcCPP = GetVal("Boolean")
    '        '            If mvarVersion < 16 Then
    '        '                TmpBookingtype.BuyingTarget.CPP = GetVal("Integer")
    '        '            Else
    '        '                TmpBookingtype.BuyingTarget.CPP = GetVal("Single")
    '        '            End If
    '        '            TmpBookingtype.BuyingTarget.CPPDaypart(0) = GetVal("Integer")
    '        '            TmpBookingtype.BuyingTarget.CPPDaypart(1) = GetVal("Integer")
    '        '            TmpBookingtype.BuyingTarget.CPPDaypart(2) = GetVal("Integer")
    '        '            TmpBookingtype.BuyingTarget.CPPDaypart(3) = GetVal("Integer")
    '        '            TmpBookingtype.BuyingTarget.CPPDaypart(4) = GetVal("Integer")
    '        '            TmpBookingtype.BuyingTarget.CPPDaypart(5) = GetVal("Integer")
    '        '            TmpBookingtype.BuyingTarget.TargetName = GetVal("String")
    '        '            TmpBookingtype.BuyingTarget.UniSize = GetVal("Long")
    '        '            TmpBookingtype.BuyingTarget.UniSizeNat = GetVal("Long")
    '        '            If TmpChannel.BuyingUniverse = "" Then
    '        '                TmpBookingtype.BuyingTarget.UniSize = TmpBookingtype.BuyingTarget.UniSizeNat
    '        '            End If
    '        '            TmpBookingtype.BuyingTarget.Target.NoUniverseSize = True
    '        '            TmpBookingtype.BuyingTarget.Target.TargetName = GetVal("String")
    '        '            TmpBookingtype.BuyingTarget.Target.TargetType = GetVal("Integer")
    '        '            TmpBookingtype.BuyingTarget.Target.Universe = GetVal("String")
    '        '            TmpBookingtype.BuyingTarget.Target.SecondUniverse = GetVal("String")
    '        '            If TmpBookingtype.BuyingTarget.Target.SecondUniverse = "" Then
    '        '                TmpBookingtype.BuyingTarget.Target.SecondUniverse = IniFile.Text("Universe", "Second")
    '        '            End If
    '        '            TmpBookingtype.BuyingTarget.Target.NoUniverseSize = False
    '        '
    '        '            'Save the rest of the booking type
    '        '
    '        '            TmpBookingtype.IndexMainTarget = GetVal("Integer")
    '        '            TmpBookingtype.IndexAllAdults = GetVal("Integer")
    '        '            For i = 0 To 4
    '        '                TmpBookingtype.DaypartSplit(i) = GetVal("Byte")
    '        '            Next
    '        '            TmpBookingtype.BookIt = GetVal("Boolean")
    '        '            TmpBookingtype.GrossCPP = GetVal("Single")
    '        '            TmpBookingtype.AverageRating = GetVal("Single")
    '        '            TmpBookingtype.ConfirmedNetBudget = GetVal("Currency")
    '        '            TmpBookingtype.Bookingtype = GetVal("Byte")
    '        '            TmpBookingtype.ContractNumber = GetVal("String")
    '        '            TmpBookingtype.IsRBS = GetVal("Boolean")
    '        '            TmpBookingtype.IsSpecific = GetVal("Boolean")
    '        '            TmpBookingtype.Discount = GetVal("Single")
    '        '            TmpBookingtype.NetCPT = GetVal("Single")
    '        '            TmpBookingtype.NetCPP = GetVal("Single")
    '        '
    '        '            'Read the pricelist
    '        '
    '        '            TmpBookingtype.Pricelist.DefaultDaypart(0) = GetVal("Byte")
    '        '            TmpBookingtype.Pricelist.DefaultDaypart(1) = GetVal("Byte")
    '        '            TmpBookingtype.Pricelist.DefaultDaypart(2) = GetVal("Byte")
    '        '            TmpBookingtype.Pricelist.DefaultDaypart(3) = GetVal("Byte")
    '        '            TmpBookingtype.Pricelist.DefaultDaypart(4) = GetVal("Byte")
    '        '            TmpBookingtype.Pricelist.DefaultDaypart(5) = GetVal("Byte")
    '        '            TmpBookingtype.Pricelist.StartDate = GetVal("Long")
    '        '            TmpBookingtype.Pricelist.EndDate = GetVal("Long")
    '        '            TmpBookingtype.Pricelist.BuyingUniverse = GetVal("String")
    '        '
    '        '            PLTs = GetVal("Integer")
    '        '
    '        '            For plt = 1 To PLTs
    '        '
    '        '                TmpString = GetVal("String")
    '        '                Set TmpPLTarget = TmpBookingtype.Pricelist.Targets.Add(TmpString)
    '        '                TmpPLTarget.CalcCPP = GetVal("Boolean")
    '        '                If mvarVersion < 16 Then
    '        '                    TmpPLTarget.CPP = GetVal("Integer")
    '        '                Else
    '        '                    TmpPLTarget.CPP = GetVal("Single")
    '        '                End If
    '        '                TmpPLTarget.CPPDaypart(0) = GetVal("Integer")
    '        '                TmpPLTarget.CPPDaypart(1) = GetVal("Integer")
    '        '                TmpPLTarget.CPPDaypart(2) = GetVal("Integer")
    '        '                TmpPLTarget.CPPDaypart(3) = GetVal("Integer")
    '        '                TmpPLTarget.CPPDaypart(4) = GetVal("Integer")
    '        '                TmpPLTarget.CPPDaypart(5) = GetVal("Integer")
    '        '                TmpPLTarget.UniSize = GetVal("Long")
    '        '                TmpPLTarget.UniSizeNat = GetVal("Long")
    '        '                TmpPLTarget.Target.NoUniverseSize = True            'For speed. No unisizes are calculated
    '        '                TmpPLTarget.Target.TargetName = GetVal("String")
    '        '                TmpPLTarget.Target.TargetType = GetVal("Byte")
    '        '                TmpPLTarget.Target.Universe = GetVal("String")
    '        '                TmpPLTarget.Target.SecondUniverse = GetVal("String")
    '        '                If TmpPLTarget.Target.SecondUniverse = "" Then
    '        '                    TmpPLTarget.Target.SecondUniverse = IniFile.Text("Universe", "Second")
    '        '                End If
    '        '                TmpPLTarget.Target.NoUniverseSize = False
    '        '                If mvarVersion >= 12 Then
    '        '                    TmpPLTarget.StandardTarget = GetVal("Boolean")
    '        '                Else
    '        '                    TmpPLTarget.StandardTarget = True
    '        '                End If
    '        '            Next
    '        '
    '        '            'Save weeks
    '        '
    '        '            Weeks = GetVal("Integer")
    '        '
    '        '            For w = 1 To Weeks
    '        '
    '        '                TmpString = GetVal("String")
    '        '                Set TmpWeek = TmpBookingtype.Weeks.Add(TmpString)
    '        '                TmpWeek.TRPControl = GetVal("Boolean")
    '        '                TmpWeek.TRP = GetVal("Single")
    '        '                If mvarVersion < 17 Then
    '        '                    TmpWeek.TRPBuyingTarget = GetVal("Single")
    '        '                    TmpWeek.TRPAllAdults = GetVal("Single")
    '        '                Else
    '        '                    Dummy = GetVal("Single")
    '        '                    Dummy = GetVal("Single")
    '        '                End If
    '        '                TmpWeek.NetBudget = GetVal("Currency")
    '        '                TmpWeek.Discount = GetVal("Single")
    '        '                TmpWeek.SeasonIndex = GetVal("Single")
    '        '                TmpWeek.StartDate = GetVal("Long")
    '        '                TmpWeek.EndDate = GetVal("Long")
    '        '                TmpWeek.ControlSaved = GetVal("Boolean")
    '        '                TmpWeek.ControlSent = GetVal("Boolean")
    '        '                TmpWeek.ControlConfirmed = GetVal("Boolean")
    '        '                TmpWeek.ControlSentToClient = GetVal("Boolean")
    '        '                TmpWeek.ControlInvoiced = GetVal("Boolean")
    '        '                TmpWeek.GrossIndex = GetVal("Single")
    '        '                If Kampanj.Version > 16 Then
    '        '                    TmpWeek.Modifier = GetVal("Byte")
    '        '                Else
    '        '                    TmpWeek.Modifier = 0
    '        '                End If
    '        '
    '        '                'Save Films
    '        '
    '        '                Films = GetVal("Integer")
    '        '
    '        '                For f = 1 To Films
    '        '
    '        '                    TmpString = GetVal("String")
    '        '                    Set TmpFilm = TmpWeek.Films.Add(TmpString)
    '        '                    TmpFilm.FilmLength = GetVal("Byte")
    '        '                    TmpFilm.Index = GetVal("Single")
    '        '                    TmpFilm.Share = GetVal("Integer")
    '        '                    TmpFilm.Description = GetVal("String")
    '        '
    '        '                Next
    '        '            Next
    '        '        Next
    '        '    Next
    '        '
    '        '    'Save the targets
    '        '
    '        '    mvarMainTarget.TargetName = GetVal("String")
    '        '    mvarMainTarget.TargetType = GetVal("Byte")
    '        '    mvarMainTarget.Universe = GetVal("String")
    '        '    mvarMainTarget.SecondUniverse = GetVal("String")
    '        '    If mvarMainTarget.SecondUniverse = "" Then
    '        '        mvarMainTarget.SecondUniverse = IniFile.Text("Universe", "Second")
    '        '    End If
    '        '
    '        '    mvarSecondaryTarget.TargetName = GetVal("String")
    '        '    mvarSecondaryTarget.TargetType = GetVal("Byte")
    '        '    mvarSecondaryTarget.Universe = GetVal("String")
    '        '    mvarSecondaryTarget.SecondUniverse = GetVal("String")
    '        '    If mvarSecondaryTarget.SecondUniverse = "" Then
    '        '        mvarSecondaryTarget.SecondUniverse = IniFile.Text("Universe", "Second")
    '        '    End If
    '        '
    '        '    mvarThirdTarget.TargetName = GetVal("String")
    '        '    mvarThirdTarget.TargetType = GetVal("Byte")
    '        '    mvarThirdTarget.Universe = GetVal("String")
    '        '    mvarThirdTarget.SecondUniverse = GetVal("String")
    '        '    If mvarThirdTarget.SecondUniverse = "" Then
    '        '        mvarThirdTarget.SecondUniverse = IniFile.Text("Universe", "Second")
    '        '    End If
    '        '
    '        '    'Save Planned spots
    '        '
    '        '    CurrentlyReading = "Planned spots"
    '        '    Spots = GetVal("Integer")
    '        '
    '        '    For s = 1 To Spots
    '        'NextPlannedSpot:
    '        '        StartPos = Loc(99)
    '        '        TmpString = GetVal("String")
    '        '        Set TmpPlannedSpot = mvarPlannedSpots.Add(TmpString)
    '        '        TmpString = GetVal("String")
    '        '        Set TmpPlannedSpot.Channel = mvarChannels(TmpString)
    '        '        TmpPlannedSpot.ChannelID = GetVal("String")
    '        '        TmpString = GetVal("String")
    '        '        Set TmpPlannedSpot.Bookingtype = TmpPlannedSpot.Channel.BookingTypes(TmpString) ' ,"cBookingType
    '        '        TmpString = GetVal("String")
    '        '        If TmpString <> "" Then
    '        '            Set TmpPlannedSpot.week = TmpPlannedSpot.Bookingtype.Weeks(TmpString) ' ,"cWeek
    '        '        End If
    '        '        TmpLong = GetVal("Long")
    '        '        TmpPlannedSpot.AirDate = TmpLong
    '        '        TmpPlannedSpot.MaM = GetVal("Integer")
    '        '        TmpPlannedSpot.ProgBefore = GetVal("String")
    '        '        TmpPlannedSpot.ProgAfter = GetVal("String")
    '        '        TmpPlannedSpot.Programme = GetVal("String")
    '        '        TmpPlannedSpot.Advertiser = GetVal("String")
    '        '        TmpPlannedSpot.Product = GetVal("String")
    '        '        TmpPlannedSpot.Filmcode = GetVal("String")
    '        '        On Error Resume Next
    '        '        Set TmpPlannedSpot.Film = TmpPlannedSpot.week.Films(TmpPlannedSpot.Filmcode)
    '        '        On Error GoTo ErrHandle
    '        '        TmpPlannedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = GetVal("Single")
    '        '        TmpPlannedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteMainTarget) = GetVal("Single")
    '        '        If mvarVersion > 14 Then
    '        '            TmpPlannedSpot.Estimation = GetVal("Integer")
    '        '        End If
    '        '        TmpPlannedSpot.MyRating = GetVal("Single")
    '        '        If mvarVersion > 13 Then
    '        '            TmpPlannedSpot.MyRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = GetVal("Single")
    '        '        Else
    '        '            TmpPlannedSpot.MyRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = (TmpPlannedSpot.MyRating / TmpPlannedSpot.Bookingtype.BuyingTarget.Target.UniIndex(uniMainTot)) / (TmpPlannedSpot.Bookingtype.IndexMainTarget / 100)
    '        '        End If
    '        '        TmpPlannedSpot.Index = GetVal("Integer")
    '        '        TmpPlannedSpot.SpotLength = GetVal("Byte")
    '        '        TmpPlannedSpot.SpotType = GetVal("Byte")
    '        '        TmpPlannedSpot.PriceNet = GetVal("Currency")
    '        '        TmpPlannedSpot.PriceGross = GetVal("Currency")
    '        '
    '        '    Next
    '        '
    '        'ActualSpots:
    '        '    'Save Actual spots
    '        '    CurrentlyReading = "Actual spots"
    '        '    Spots = GetVal("Integer")
    '        '
    '        '    For s = 1 To Spots
    '        '
    '        '        TmpLong = GetVal("Long")
    '        '        TmpInt = GetVal("Integer")
    '        '        Set TmpActualSpot = mvarActualSpots.Add(CDate(TmpLong), TmpInt)
    '        '        TmpStr = GetVal("String")
    '        '        Set TmpActualSpot.Channel = mvarChannels(TmpStr)
    '        '        TmpActualSpot.ProgBefore = GetVal("String")
    '        '        TmpActualSpot.ProgAfter = GetVal("String")
    '        '        TmpActualSpot.Programme = GetVal("String")
    '        '        TmpActualSpot.Advertiser = GetVal("String")
    '        '        TmpActualSpot.Product = GetVal("String")
    '        '        TmpActualSpot.Filmcode = GetVal("String")
    '        '        TmpActualSpot.Rating = GetVal("Single")
    '        '        TmpActualSpot.Index = GetVal("Integer")
    '        '        TmpActualSpot.PosInBreak = GetVal("Byte")
    '        '        TmpActualSpot.SpotsInBreak = GetVal("Byte")
    '        '        TmpStr = GetVal("String")
    '        '        If TmpStr <> "" Then
    '        '            On Error Resume Next
    '        '            Set TmpActualSpot.MatchedSpot = mvarPlannedSpots(TmpStr)
    '        '            Set mvarPlannedSpots(TmpStr).MatchedSpot = TmpActualSpot
    '        '            On Error GoTo ErrHandle
    '        '        End If
    '        '        TmpActualSpot.SpotLength = GetVal("Byte")
    '        '        TmpActualSpot.Deactivated = GetVal("Boolean")
    '        '        TmpActualSpot.SpotType = GetVal("Byte")
    '        '        TmpActualSpot.BreakType = GetVal("Byte")
    '        '        TmpActualSpot.SecondRating = GetVal("Single")
    '        '        TmpActualSpot.AdEdgeChannel = GetVal("String")
    '        '        TmpActualSpot.ID = GetVal("String")
    '        '        TmpStr = GetVal("String")
    '        '        If TmpStr <> "" Then
    '        '            Set TmpActualSpot.Bookingtype = TmpActualSpot.Channel.BookingTypes(TmpStr)
    '        '        Else
    '        '            Set TmpActualSpot.Bookingtype = TmpActualSpot.Channel.BookingTypes(1)
    '        '        End If
    '        '        TmpActualSpot.GroupIdx = GetVal("Integer")
    '        '
    '        '    Next
    '        '
    '        '    CurrentlyReading = ""
    '        '    Count = GetVal("Integer")
    '        '    For i = 1 To Count
    '        '
    '        '        Group = GetVal("Variant")
    '        '        Freqs = GetVal("Integer")
    '        '
    '        '        For j = 1 To Freqs
    '        '
    '        '            Freq = GetVal("String")
    '        '            Reach = GetVal("Byte")
    '        '            ReachTargets(Freq, Group) = Reach
    '        '
    '        '        Next
    '        '    Next
    '        '
    '        '    Count = GetVal("Integer")
    '        '    For i = 1 To Count
    '        '
    '        '        ChangeDate = GetVal("Date")
    '        '        ChangeTime = GetVal("String")
    '        '        Who = GetVal("String")
    '        '        Activity = GetVal("String")
    '        '
    '        '        mvarActivities.Add ChangeDate, ChangeTime, Activity, Who
    '        '
    '        '    Next
    '        '
    '        '    Dim TmpID As String
    '        '    Dim TmpAdjustBy As Byte
    '        '    Dim TmpDate As Date
    '        '    Dim TmpBookingType As String
    '        '    Dim TmpChannelEstimate As Single
    '        '    Dim TmpDBID As String
    '        '    Dim TmpFilmcode As String
    '        '    Dim TmpGrossPrice As Currency
    '        '    Dim TmpMaM As Integer
    '        '    Dim tmpMyEstimate As Single
    '        '    Dim TmpMyEstChanTarget As Single
    '        '    Dim TmpNetPrice As Currency
    '        '    Dim TmpPlacement As PlaceEnum
    '        '    Dim TmpProgAfter As String
    '        '    Dim TmpProgBefore As String
    '        '    Dim TmpProg As String
    '        '
    '        '    Count = GetVal("Integer")
    '        '
    '        '    For i = 1 To Count
    '        '        TmpID = GetVal("String")
    '        '        TmpAdjustBy = GetVal("Byte")
    '        '        TmpDate = GetVal("Date")
    '        '        TmpBookingType = GetVal("String")
    '        '        TmpString = GetVal("String") ' Channel
    '        '        TmpChannelEstimate = GetVal("Single")
    '        '        TmpDBID = GetVal("String")
    '        '        TmpFilmcode = GetVal("String")
    '        '        TmpGrossPrice = GetVal("Currency")
    '        '        TmpMaM = GetVal("Integer")
    '        '        tmpMyEstimate = GetVal("Single")
    '        '        TmpMyEstChanTarget = GetVal("Single")
    '        '        TmpNetPrice = GetVal("Currency")
    '        '        TmpPlacement = GetVal("Integer")
    '        '        TmpProgAfter = GetVal("String")
    '        '        TmpProgBefore = GetVal("String")
    '        '        TmpProg = GetVal("String")
    '        '        TmpDBID = TmpString & Format(TmpDate, "yyyymmdd") & Left(helper.mam2tid(TmpMaM), 2) & Right(helper.mam2tid(TmpMaM), 2)
    '        '        mvarBookedSpots.Add TmpID, TmpDBID, TmpString, TmpDate, TmpMaM, TmpProg, TmpProgAfter, TmpProgBefore, TmpGrossPrice, TmpNetPrice, TmpChannelEstimate, tmpMyEstimate, TmpMyEstChanTarget, TmpPlacement, TmpAdjustBy, TmpFilmcode, TmpBookingType, 0, 0
    '        '    Next
    '        '
    '        '    mvarFilename = Path
    '        '    Loading = False
    '        '    Exit Function
    '        '
    '        'ErrHandle:
    '        '    If CurrentlyReading = "Planned spots" Then
    '        '        a = MsgBox("The follwing error: '" & Err.Description & "' occured while reading planned spot no " & s & Chr(10) & Chr(10) & "Please click one of the following:" & Chr(10) & "'Abort': Continue to read file but ignore planned spots" & Chr(10) & "'Retry': Read the next planned spot" & Chr(10) & "'Ignore': Continue reading other info on this spot", vbAbortRetryIgnore, "TRINITY")
    '        '        If a = vbIgnore Then
    '        '            Resume Next
    '        '        ElseIf a = vbAbort Then
    '        '            Seek #99, StartPos + (Spots - s + 1) * 23 + 1
    '        '            Kampanj.PlannedSpots.Remove s
    '        '            s = Spots
    '        '            Resume ActualSpots
    '        '        ElseIf a = vbRetry Then
    '        '            s = s + 1
    '        '            Seek #99, StartPos + 24
    '        '            Resume NextPlannedSpot
    '        '        End If
    '        '    End If
    '        '    LoadCampaign = 1
    '        '    Loading = False
    '        '    Err.Raise Err.Number, Err.Source, Err.Description
    '        'End Function
    '        '
    '        ''---------------------------------------------------------------------------------------
    '        '' Procedure : SaveTemplate
    '        '' DateTime  : 2003-10-08 14:43
    '        '' Author    : joho
    '        '' Purpose   : Save campaign as a template file
    '        ''---------------------------------------------------------------------------------------
    '        ''
    '        'Public Sub SaveTemplate(Path As String)
    '        '
    '        '    Dim TmpChan As cChannel
    '        '    Dim TmpBookingType As cBookingType
    '        '    Dim FileNum As Integer
    '        '
    '        '   On Error GoTo SaveTemplate_Error
    '        '
    '        '    FileNum = FreeFile
    '        '    Open Path For Random Lock Read Write As FileNum Len = 256
    '        '
    '        '    Put #FileNum, , mvarVersion
    '        '    Put #FileNum, , mvarClient
    '        '    Put #FileNum, , mvarProduct
    '        '    Put #FileNum, , mvarServiceFee
    '        '    Put #FileNum, , mvarTVCheck
    '        '    Put #FileNum, , mvarBuyer
    '        '    Put #FileNum, , mvarPlanner
    '        '    Put #FileNum, , mvarMainTarget.TargetName
    '        '    Put #FileNum, , mvarMainTarget.Universe
    '        '    Put #FileNum, , mvarSecondaryTarget.TargetName
    '        '    Put #FileNum, , mvarSecondaryTarget.Universe
    '        '    Put #FileNum, , mvarThirdTarget.TargetName
    '        '    Put #FileNum, , mvarThirdTarget.Universe
    '        '
    '        '    Put #FileNum, , mvarChannels.Count
    '        '    For Each TmpChan In Kampanj.Channels
    '        '        Put #FileNum, , TmpChan.ChannelName
    '        '        Put #FileNum, , TmpChan.BookingTypes.Count
    '        '        For Each TmpBookingType In TmpChan.BookingTypes
    '        '            Put #FileNum, , TmpBookingType.Name
    '        '            Put #FileNum, , TmpBookingType.Shortname
    '        '            Put #FileNum, , TmpBookingType.BuyingTarget.TargetName
    '        '            Put #FileNum, , TmpBookingType.Discount
    '        '        Next
    '        '    Next
    '        '    Close FileNum
    '        '
    '        '   On Error GoTo 0
    '        '   Exit Sub
    '        '
    '        'SaveTemplate_Error:
    '        '
    '        '        Err.Raise Err.Number, "cKampanj: SaveTemplate", Err.Description
    '        '
    '        'End Sub
    '        '
    '        '
    '        ''---------------------------------------------------------------------------------------
    '        '' Procedure : LoadTemplate
    '        '' DateTime  : 2003-10-06 11:18
    '        '' Author    : joho
    '        '' Purpose   : Load a template file (for use with applications like
    '        ''             SuperOffice
    '        ''---------------------------------------------------------------------------------------
    '        ''
    '        'Public Sub LoadTemplate(Path As String)
    '        '
    '        '    Dim ChanCount As Integer
    '        '    Dim BTCount As Integer
    '        '
    '        '    Dim TmpStr As String
    '        '    Dim TmpChan As cChannel
    '        '    Dim TmpBookingType As cBookingType
    '        '
    '        '    Dim c As Integer
    '        '    Dim b As Integer
    '        '    Dim FileNum As Integer
    '        '
    '        '   On Error GoTo LoadTemplate_Error
    '        '
    '        '    Close 99
    '        '
    '        '    Open Path For Random Lock Read Write As 99 Len = 256
    '        '
    '        '    Class_Initialize
    '        '
    '        '    Area = mvarArea
    '        '    mvarVersion = GetVal("Byte")
    '        '    mvarClient = GetVal("String")
    '        '    mvarProduct = GetVal("String")
    '        '    mvarServiceFee = GetVal("Single")
    '        '    mvarTVCheck = GetVal("Currency")
    '        '    mvarBuyer = GetVal("String")
    '        '    mvarPlanner = GetVal("String")
    '        '    mvarMainTarget.TargetName = GetVal("String")
    '        '    mvarMainTarget.Universe = GetVal("String")
    '        '    mvarSecondaryTarget.TargetName = GetVal("String")
    '        '    mvarSecondaryTarget.Universe = GetVal("String")
    '        '    mvarThirdTarget.TargetName = GetVal("String")
    '        '    mvarThirdTarget.Universe = GetVal("String")
    '        '    'mvarStartDate = Now
    '        '    'mvarEndDate = Now
    '        '
    '        '    ChanCount = GetVal("Integer")
    '        '    For c = 1 To ChanCount
    '        '        TmpStr = GetVal("String")
    '        '        On Error Resume Next
    '        '        Set TmpChan = mvarChannels.Add(TmpStr)
    '        '        TmpChan.ReadDefaultProperties
    '        '        On Error GoTo LoadTemplate_Error
    '        '        BTCount = GetVal("Integer")
    '        '        For b = 1 To BTCount
    '        '            TmpStr = GetVal("String")
    '        '            On Error Resume Next
    '        '            Set TmpBookingType = TmpChan.BookingTypes.Add(TmpStr, True) '
    '        '            TmpStr = GetVal("String")
    '        '            TmpBookingType.Shortname = TmpStr
    '        '            TmpStr = GetVal("String")
    '        '            If TmpStr <> "" Then
    '        '                Set TmpBookingType.BuyingTarget = TmpBookingType.Pricelist.Targets(TmpStr)
    '        '            End If
    '        '            TmpBookingType.Discount = GetVal("Single")
    '        '            On Error GoTo LoadTemplate_Error
    '        '        Next
    '        '    Next
    '        '    RedefineWeeks
    '        '   On Error GoTo 0
    '        '   Exit Sub
    '        '
    '        'LoadTemplate_Error:
    '        '
    '        '        Err.Raise Err.Number, "cKampanj: LoadTemplate", Err.Description
    '        '
    '        'End Sub
    '        '
    '        '
    '        ''---------------------------------------------------------------------------------------
    '        '' Procedure : ImportOldCampaign
    '        '' DateTime  : 2003-07-02 13:39
    '        '' Author    : joho
    '        '' Purpose   : Imports a campaign in the old KampanjType-format and converts it to
    '        ''             the new object oriented format
    '        ''---------------------------------------------------------------------------------------
    '        ''
    '        'Public Function ImportOldCampaign(Path As String)
    '        '
    '        '    Dim TmpKampanj As KampanjType
    '        '    Dim TmpPlannedSpot As cPlannedSpot
    '        '    Dim TmpActualSpot As cActualSpot
    '        '    Dim TmpChannel As cChannel
    '        '    Dim TmpBookingtype As cBookingType
    '        '    Dim TmpWeek As cWeek
    '        '    Dim TmpFilm As cFilm
    '        '    Dim TmpPricelist As cPriceList
    '        '    Dim TmpPLTarget As cPriceListTarget
    '        '
    '        '    Dim i As Integer
    '        '    Dim j As Integer
    '        '
    '        '    Dim k As Integer
    '        '    Dim b As Integer
    '        '    Dim v As Integer
    '        '    Dim q As Integer
    '        '
    '        '    Dim TmpStr As String
    '        '    Dim BT As String
    '        '    Dim ReadingVar As String
    '        '    Dim Dummy As Integer
    '        '
    '        '    On Error GoTo ImportOldCampaign_Error
    '        '
    '        '    Open Path For Binary As 1
    '        '
    '        '    Get #1, , TmpKampanj
    '        '
    '        '    IniFile.Create LocalTrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & "\Data\Trinity.ini"
    '        '
    '        '    If TmpKampanj.Version >= 3 Then
    '        '
    '        '        ReadingVar = "Version"
    '        '        mvarVersion = TmpKampanj.Version
    '        '        ReadingVar = "Name"
    '        '        mvarName = TmpKampanj.KampanjNamn
    '        '        ReadingVar = "Area"
    '        '        mvarArea = TmpKampanj.Area
    '        '        ReadingVar = "AreaLog"
    '        '        mvarAreaLog = TmpKampanj.AreaLog
    '        '        If mvarArea = "" Then
    '        '            mvarArea = Ini.Text("Locale", "Area")
    '        '            mvarAreaLog = Ini.Text("Locale", "AreaLog")
    '        '            If mvarAreaLog = "" Or mvarArea = "SE" Then
    '        '                mvarArea = "SE"
    '        '                mvarAreaLog = "SEMMS"
    '        '            End If
    '        '        End If
    '        '        ReadingVar = "ServiceFee"
    '        '        mvarServiceFee = TmpKampanj.Avtalspåslag
    '        '        ReadingVar = "Cancelled"
    '        '        mvarCancelled = TmpKampanj.Cancelled
    '        '        ReadingVar = "ControlTracking"
    '        '        mvarControlTracking = TmpKampanj.Eftermätning
    '        '        ReadingVar = "mvarControlFilmcodeFromClient"
    '        '        mvarControlFilmcodeFromClient = TmpKampanj.FilmkodFrånkund
    '        '        ReadingVar = "mvarControlFilmcodeToBureau"
    '        '        mvarControlFilmcodeToBureau = TmpKampanj.FilmkodTillByrå
    '        '        ReadingVar = "mvarControlFilmcodeToChannels"
    '        '        mvarControlFilmcodeToChannels = TmpKampanj.FilmkodTillKanal
    '        '        ReadingVar = "mvarFilename"
    '        '        mvarFilename = TmpKampanj.Filnamn
    '        '        ReadingVar = "mvarFrequencyFocus"
    '        '        mvarFrequencyFocus = TmpKampanj.FrekvensFokus
    '        '        ReadingVar = "mvarControlOKFromClient"
    '        '        mvarControlOKFromClient = TmpKampanj.Godkänd
    '        '        ReadingVar = "mvarControlTransferredToMatrix"
    '        '        mvarControlTransferredToMatrix = TmpKampanj.InlaggdIMatrix
    '        '        ReadingVar = "mvarCommentary"
    '        '        mvarCommentary = TmpKampanj.Kommentar
    '        '        ReadingVar = "mvarClient"
    '        '        mvarClient = TmpKampanj.Kund
    '        '        ReadingVar = "mvarTrackingCost"
    '        '        mvarTrackingCost = TmpKampanj.Marketwatch
    '        '        ReadingVar = "mvarMainTarget"
    '        '        mvarMainTarget = Target2AdEdge(Target2Text(TmpKampanj.MålgruppID))
    '        '        ReadingVar = "mvarMainTarget.SecondUniverse"
    '        '        mvarMainTarget.SecondUniverse = "sat"
    '        '        ReadingVar = "mvarSecondaryTarget"
    '        '        mvarSecondaryTarget = Target2AdEdge(Target2Text(TmpKampanj.SekundärMålgruppID))
    '        '        ReadingVar = "mvarSecondaryTarget.SecondUniverse"
    '        '        mvarSecondaryTarget.SecondUniverse = "sat"
    '        '        ReadingVar = "mvarThirdTarget"
    '        '        mvarThirdTarget = Target2AdEdge(Target2Text(TmpKampanj.TertiärMålgruppID))
    '        '        ReadingVar = "mvarThirdTarget.SecondUniverse"
    '        '        mvarThirdTarget.SecondUniverse = "sat"
    '        '        ReadingVar = "mvarBuyer"
    '        '        mvarBuyer = TmpKampanj.Planerare
    '        '        ReadingVar = "mvarProduct"
    '        '        mvarProduct = TmpKampanj.Produkt
    '        '        ReadingVar = "mvarPlanner"
    '        '        mvarPlanner = TmpKampanj.Rådgivare
    '        '        ReadingVar = "mvarUpdatedTo"
    '        '        mvarUpdatedTo = TmpKampanj.SenasteDag
    '        '        ReadingVar = "mvarServiceFeeOnNet1"
    '        '        mvarServiceFeeOnNet1 = TmpKampanj.ServiceFee
    '        '        ReadingVar = "mvarStartDate"
    '        '        mvarStartDate = TmpKampanj.StartDatum
    '        '        ReadingVar = "mvarEndDate"
    '        '        mvarEndDate = TmpKampanj.SlutDatum
    '        '        ReadingVar = "mvarBudgetTotalCTC"
    '        '        mvarBudgetTotalCTC = TmpKampanj.TotCTC
    '        '        ReadingVar = "mvarTVCheck"
    '        '        mvarTVCheck = TmpKampanj.TVCheck
    '        '        ReadingVar = "mvarControlFollowedUp"
    '        '        mvarControlFollowedUp = TmpKampanj.Uppföljd
    '        '        ReadingVar = "mvarControlFollowUpToClient"
    '        '        mvarControlFollowUpToClient = TmpKampanj.UppföljningTillKund
    '        '        For i = 1 To 10
    '        '            ReachTargets(i, "Nat") = TmpKampanj.NRVMål(i)
    '        '            ReachTargets(i, "Sat") = TmpKampanj.NRVMålSat(i)
    '        '            For j = 1 To 12
    '        '                If TmpKampanj.NRVPanelNamn(j) <> "" Then
    '        '                    ReachTargets(i, TmpKampanj.NRVPanelNamn(j)) = TmpKampanj.NRVPanel(j, i)
    '        '                End If
    '        '            Next
    '        '        Next
    '        '
    '        '        Set mvarChannels = New cChannels
    '        '        Set mvarChannels.MainObject = Me
    '        '
    '        '        For k = 1 To ANT_KANALER
    '        '            For b = 0 To 2
    '        '                If TmpKampanj.ExtKanaler(k).Bokningstyp(b).Boka Then
    '        '                    ReadingVar = "mvarChannels.Add " & TmpKampanj.ExtKanaler(k).KanalNamn
    '        '                    mvarChannels.Add TmpKampanj.ExtKanaler(k).KanalNamn
    '        '                    Select Case b
    '        '                        Case 0: TmpStr = "RBS"
    '        '                        Case 1: TmpStr = "Specifics"
    '        '                        Case 2: TmpStr = "Last Minute"
    '        '                    End Select
    '        '
    '        '                    ReadingVar = "mvarChannels.Add " & TmpKampanj.ExtKanaler(k).KanalNamn
    '        '                    Set TmpBookingtype = mvarChannels(TmpKampanj.ExtKanaler(k).KanalNamn).BookingTypes.Add(TmpStr)
    '        '                    TmpBookingtype.ConfirmedNetBudget = TmpKampanj.ExtKanaler(k).Bokningstyp(b).BekrBudget
    '        '                    TmpBookingtype.Bookingtype = b
    '        '                    TmpBookingtype.BookIt = True
    '        '
    '        '                    IniFile.Create LocalTrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & "\old\prislistor" & mvarArea & ".ini"
    '        '
    '        '                    Set TmpBookingtype.BuyingTarget = New cPriceListTarget
    '        '                    Set TmpBookingtype.BuyingTarget.MainObject = Me
    '        '                    Set TmpBookingtype.BuyingTarget.Bookingtype = TmpBookingtype
    '        '
    '        '                    TmpBookingtype.BuyingTarget.Target = Target2AdEdge(Target2Text(IniFile.Text(TmpKampanj.ExtKanaler(k).Kortnamn & " " & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Kortnamn, "TargetID" & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Köpmålgrupp + 1)))
    '        '                    TmpBookingtype.BuyingTarget.Target.SecondUniverse = "sat"
    '        '                    TmpBookingtype.BuyingTarget.CalcCPP = (IniFile.Text(TmpKampanj.ExtKanaler(k).Kortnamn & " " & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Kortnamn, "CalcCPP" & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Köpmålgrupp + 1) = "True") Or (IniFile.Text(TmpKampanj.ExtKanaler(k).Kortnamn & " " & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Kortnamn, "CalcCPP" & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Köpmålgrupp + 1) = "-1")
    '        '                    TmpBookingtype.BuyingTarget.CPP = Val(IniFile.Text(TmpKampanj.ExtKanaler(k).Kortnamn & " " & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Kortnamn, "CPP" & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Köpmålgrupp + 1))
    '        '                    TmpBookingtype.BuyingTarget.CPPDaypart(0) = Val(IniFile.Text(TmpKampanj.ExtKanaler(k).Kortnamn & " " & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Kortnamn, "CPP_DT" & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Köpmålgrupp + 1))
    '        '                    TmpBookingtype.BuyingTarget.CPPDaypart(1) = Val(IniFile.Text(TmpKampanj.ExtKanaler(k).Kortnamn & " " & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Kortnamn, "CPP_PT" & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Köpmålgrupp + 1))
    '        '                    TmpBookingtype.BuyingTarget.CPPDaypart(2) = Val(IniFile.Text(TmpKampanj.ExtKanaler(k).Kortnamn & " " & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Kortnamn, "CPP_NT" & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Köpmålgrupp + 1))
    '        '                    TmpBookingtype.BuyingTarget.TargetName = IniFile.Text(TmpKampanj.ExtKanaler(k).Kortnamn & " " & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Kortnamn, "Target" & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Köpmålgrupp + 1)
    '        '                    TmpBookingtype.BuyingTarget.UniSizeNat = Val(IniFile.Text(TmpKampanj.ExtKanaler(k).Kortnamn & " " & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Kortnamn, "TargetNat" & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Köpmålgrupp + 1))
    '        '                    If IniFile.Text(TmpKampanj.ExtKanaler(k).Kortnamn & " " & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Kortnamn, "Satellit") = "-1" Or UCase(IniFile.Text(TmpKampanj.ExtKanaler(k).Kortnamn & " " & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Kortnamn, "Satellit")) = "TRUE" Then
    '        '                        TmpBookingtype.BuyingTarget.UniSize = Val(IniFile.Text(TmpKampanj.ExtKanaler(k).Kortnamn & " " & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Kortnamn, "TargetSat" & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Köpmålgrupp + 1))
    '        '                        'TODO: Not international
    '        '                        TmpBookingtype.BuyingTarget.Target.Universe = "sat"
    '        '                    Else
    '        '                        TmpBookingtype.BuyingTarget.UniSize = IniFile.Text(TmpKampanj.ExtKanaler(k).Kortnamn & " " & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Kortnamn, "TargetNat" & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Köpmålgrupp + 1)
    '        '                        TmpBookingtype.BuyingTarget.Target.Universe = ""
    '        '                    End If
    '        '
    '        '                    TmpBookingtype.ContractNumber = TmpKampanj.ExtKanaler(k).Ordernummer
    '        '                    TmpBookingtype.DaypartSplit(0) = TmpKampanj.ExtKanaler(k).Bokningstyp(b).DaypartSplit(0)
    '        '                    TmpBookingtype.DaypartSplit(1) = TmpKampanj.ExtKanaler(k).Bokningstyp(b).DaypartSplit(1)
    '        '                    TmpBookingtype.DaypartSplit(2) = TmpKampanj.ExtKanaler(k).Bokningstyp(b).DaypartSplit(2)
    '        '                    TmpBookingtype.GrossCPP = TmpKampanj.ExtKanaler(k).Bokningstyp(b).BruttoCPP
    '        '                    If TmpBookingtype.BuyingTarget.CPP = 0 Then
    '        '                        TmpBookingtype.BuyingTarget.CPP = TmpKampanj.ExtKanaler(k).Bokningstyp(b).BruttoCPP
    '        '                    End If
    '        '                    TmpBookingtype.IndexAllAdults = TmpKampanj.ExtKanaler(k).Bokningstyp(b).IndexAllAdults
    '        '                    TmpBookingtype.IndexMainTarget = TmpKampanj.ExtKanaler(k).Bokningstyp(b).IndexVerkligMg
    '        '                    TmpBookingtype.IsRBS = (b = 0)
    '        '                    TmpBookingtype.IsSpecific = (b > 0)
    '        '                    TmpBookingtype.IsVisible = True
    '        '                    TmpBookingtype.NetCPP = TmpKampanj.ExtKanaler(k).Bokningstyp(b).NettoCPP
    '        '
    '        '                    Set TmpPricelist = TmpBookingtype.Pricelist
    '        '                    Set TmpPricelist.Bookingtype = TmpBookingtype
    '        '
    '        '                    TmpStr = TmpKampanj.ExtKanaler(k).Kortnamn & " " & TmpKampanj.ExtKanaler(k).Bokningstyp(b).Kortnamn
    '        '                    TmpPricelist.DefaultDaypart(0) = IniFile.Text(TmpStr, "Day")
    '        '                    TmpPricelist.DefaultDaypart(1) = IniFile.Text(TmpStr, "Prime")
    '        '                    TmpPricelist.DefaultDaypart(2) = IniFile.Text(TmpStr, "Night")
    '        '
    '        '                    For i = 1 To IniFile.Data(TmpStr, "TargetCount")
    '        '
    '        '                        Set TmpPLTarget = TmpPricelist.Targets.Add(IniFile.Text(TmpStr, "Target" & i))
    '        '                        TmpPLTarget.Target.NoUniverseSize = True
    '        '                        TmpPLTarget.Target.TargetName = Target2AdEdge(Target2Text(IniFile.Text(TmpStr, "TargetID" & i)))
    '        '                        TmpPLTarget.Target.SecondUniverse = "sat"
    '        '                        TmpPLTarget.CalcCPP = (IniFile.Text(TmpStr, "CalcCPP" & i) = "True")
    '        '                        TmpPLTarget.CPP = IniFile.Data(TmpStr, "CPP" & i)
    '        '                        TmpPLTarget.CPPDaypart(0) = IniFile.Data(TmpStr, "CPP_DT" & i)
    '        '                        TmpPLTarget.CPPDaypart(1) = IniFile.Data(TmpStr, "CPP_PT" & i)
    '        '                        TmpPLTarget.CPPDaypart(2) = IniFile.Data(TmpStr, "CPP_NT" & i)
    '        '                        TmpPLTarget.UniSize = IniFile.Data(TmpStr, "TargetSat" & i)
    '        '                        TmpPLTarget.UniSizeNat = IniFile.Data(TmpStr, "TargetNat" & i)
    '        '                        'TmpPLTarget.Target.TargetName = Target2AdEdge(Target2Text(Inifile.Text(TmpStr, "TargetID" & i)))
    '        '                        If IniFile.Text(TmpStr, "Satellit") = "True" Or IniFile.Text(TmpStr, "Satellit") = "-1" Then
    '        '                            'TODO: Not international
    '        '                            TmpPLTarget.Target.Universe = "sat"
    '        '                            TmpPricelist.BuyingUniverse = "sat"
    '        '                        Else
    '        '                            TmpPLTarget.Target.Universe = ""
    '        '                            TmpPricelist.BuyingUniverse = ""
    '        '                        End If
    '        '
    '        '                    Next
    '        '                    TmpBookingtype.Shortname = TmpKampanj.ExtKanaler(k).Bokningstyp(b).Kortnamn
    '        '                    TmpBookingtype.AverageRating = TmpKampanj.ExtKanaler(k).Bokningstyp(b).TRPPerSpot
    '        '                    For v = 1 To TmpKampanj.ExtKanaler(k).Bokningstyp(b).AntVeckor
    '        '                        Set TmpWeek = TmpBookingtype.Weeks.Add(Trim(TmpKampanj.ExtKanaler(k).Bokningstyp(b).Veckor(v).Namn))
    '        '                        TmpWeek.GrossIndex = 1
    '        '                        TmpWeek.ControlConfirmed = TmpKampanj.ExtKanaler(k).Bokningstyp(b).Veckor(v).Bekräftad
    '        '                        TmpWeek.ControlInvoiced = TmpKampanj.ExtKanaler(k).Bokningstyp(b).Veckor(v).Fakturerad
    '        '                        TmpWeek.ControlSaved = TmpKampanj.ExtKanaler(k).Bokningstyp(b).Veckor(v).Sparad
    '        '                        TmpWeek.ControlSent = TmpKampanj.ExtKanaler(k).Bokningstyp(b).Veckor(v).Skickad
    '        '                        TmpWeek.ControlSentToClient = TmpKampanj.ExtKanaler(k).Bokningstyp(b).Veckor(v).SkickadTillKund
    '        '                        TmpWeek.NetCPP30 = TmpKampanj.ExtKanaler(k).Bokningstyp(b).Veckor(v).NettoCPP
    '        '                        TmpWeek.EndDate = TmpKampanj.ExtKanaler(k).Bokningstyp(b).Veckor(v).SlutDatum
    '        '                        TmpWeek.StartDate = TmpKampanj.ExtKanaler(k).Bokningstyp(b).Veckor(v).StartDatum
    '        '                        TmpWeek.TRPBuyingTarget = TmpKampanj.ExtKanaler(k).Bokningstyp(b).Veckor(v).TRP
    '        '                        TmpWeek.TRPControl = True
    '        '                        For q = 1 To TmpKampanj.ExtKanaler(k).Bokningstyp(b).Veckor(v).AntFilmer
    '        '                            Set TmpFilm = TmpWeek.Films.Add(TmpKampanj.ExtKanaler(k).Bokningstyp(b).Veckor(v).Filmer(q).Filmkod)
    '        '                            TmpFilm.FilmLength = TmpKampanj.ExtKanaler(k).Bokningstyp(b).Veckor(v).Filmer(q).Filmlängd
    '        '                            TmpFilm.Index = TmpKampanj.ExtKanaler(k).Bokningstyp(b).Veckor(v).Filmer(q).Index
    '        '                            TmpFilm.Description = TmpKampanj.ExtKanaler(k).Bokningstyp(b).Veckor(v).Filmer(q).Namn
    '        '                            TmpFilm.Share = TmpKampanj.ExtKanaler(k).Bokningstyp(b).Veckor(v).Filmer(q).Andel
    '        '                        Next
    '        '                    Next
    '        '                End If
    '        '            Next
    '        '        Next
    '        '
    '        '        For i = 1 To TmpKampanj.AntPlaneradeSpots
    '        '
    '        '            Set TmpPlannedSpot = mvarPlannedSpots.Add
    '        '            TmpPlannedSpot.Advertiser = TmpKampanj.PlaneradeSpots(i).Annonsör
    '        '            TmpPlannedSpot.AirDate = TmpKampanj.PlaneradeSpots(i).Datum
    '        '
    '        '            'Check wether the channel has been added to the collection
    '        '
    '        '            On Error Resume Next
    '        '            j = mvarChannels(TmpKampanj.ExtKanaler(TmpKampanj.PlaneradeSpots(i).KanalNr).KanalNamn).ID
    '        '            j = Err.Number
    '        '            On Error GoTo ImportOldCampaign_Error
    '        '
    '        '            If j <> 0 Then
    '        '                Set TmpChannel = mvarChannels.Add(TmpKampanj.ExtKanaler(TmpKampanj.PlaneradeSpots(i).KanalNr).KanalNamn)
    '        '                TmpChannel.ID = TmpKampanj.ExtKanaler(TmpKampanj.PlaneradeSpots(i).KanalNr).ID
    '        '                TmpChannel.IsVisible = True
    '        '                TmpChannel.MainTarget.TargetName = Target2AdEdge(Target2Text(TmpKampanj.ExtKanaler(TmpKampanj.PlaneradeSpots(i).KanalNr).MålgruppID))
    '        '                TmpChannel.SecondaryTarget.TargetName = Target2AdEdge(Target2Text(TmpKampanj.ExtKanaler(TmpKampanj.PlaneradeSpots(i).KanalNr).SekundärMålgruppID))
    '        '            End If
    '        '            Set TmpPlannedSpot.Channel = mvarChannels(TmpKampanj.ExtKanaler(TmpKampanj.PlaneradeSpots(i).KanalNr).KanalNamn)
    '        '
    '        '            TmpPlannedSpot.Filmcode = TmpKampanj.PlaneradeSpots(i).Filmkod
    '        '            TmpPlannedSpot.Index = TmpKampanj.PlaneradeSpots(i).Index
    '        '            TmpPlannedSpot.MaM = TmpKampanj.PlaneradeSpots(i).MaM
    '        '            TmpPlannedSpot.MyRating = TmpKampanj.PlaneradeSpots(i).MinRating
    '        '            'TODO: Räkna ut MyRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
    '        '            TmpPlannedSpot.MyRating = 0
    '        '            TmpPlannedSpot.Product = TmpKampanj.PlaneradeSpots(i).Produkt
    '        '            If TmpKampanj.PlaneradeSpots(i).Program <> "RBS TV4" Then
    '        '                TmpPlannedSpot.Programme = TmpKampanj.PlaneradeSpots(i).Program
    '        '            Else
    '        '                TmpPlannedSpot.Programme = "RBS Spot Marker"
    '        '            End If
    '        '            TmpPlannedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteMainTarget) = TmpKampanj.PlaneradeSpots(i).Rating
    '        '            TmpPlannedSpot.SpotLength = TmpKampanj.PlaneradeSpots(i).Spotlängd
    '        '            TmpPlannedSpot.SpotType = TmpKampanj.PlaneradeSpots(i).Spottyp
    '        '
    '        '        Next
    '        '
    '        '        For i = 1 To TmpKampanj.AntUtfallSpots
    '        '
    '        '            Set TmpActualSpot = mvarActualSpots.Add(CDate(TmpKampanj.UtfallSpots(i).Datum), TmpKampanj.UtfallSpots(i).MaM)
    '        '            If TmpKampanj.UtfallSpots(i).AdEdgeKanal = "" Then
    '        '                TmpActualSpot.AdEdgeChannel = TmpKampanj.ExtKanaler(TmpKampanj.UtfallSpots(i).KanalNr).AdEdgeNamn
    '        '            Else
    '        '                TmpActualSpot.AdEdgeChannel = TmpKampanj.UtfallSpots(i).AdEdgeKanal
    '        '            End If
    '        '            TmpActualSpot.Advertiser = TmpKampanj.UtfallSpots(i).Annonsör
    '        '
    '        '            TmpActualSpot.BreakType = TmpKampanj.UtfallSpots(i).BlockTyp
    '        '
    '        '            For Each TmpChannel In mvarChannels
    '        '                If TmpChannel.ChannelName = TmpKampanj.ExtKanaler(TmpKampanj.UtfallSpots(i).KanalNr).KanalNamn Then
    '        '                    Set TmpActualSpot.Channel = TmpChannel
    '        '                End If
    '        '            Next
    '        '
    '        '            TmpActualSpot.Deactivated = TmpKampanj.UtfallSpots(i).EjAktiv
    '        '            TmpActualSpot.Filmcode = TmpKampanj.UtfallSpots(i).Filmkod
    '        '            TmpActualSpot.Index = TmpKampanj.UtfallSpots(i).Index
    '        '            TmpActualSpot.PosInBreak = TmpKampanj.UtfallSpots(i).PosInBreak
    '        '            TmpActualSpot.Product = TmpKampanj.UtfallSpots(i).Produkt
    '        '            TmpActualSpot.Programme = TmpKampanj.UtfallSpots(i).Program
    '        '            TmpActualSpot.Rating = TmpKampanj.UtfallSpots(i).Rating
    '        '            TmpActualSpot.SpotLength = TmpKampanj.UtfallSpots(i).Spotlängd
    '        '            TmpActualSpot.SpotsInBreak = TmpKampanj.UtfallSpots(i).SpotsInBreak
    '        '            TmpActualSpot.SpotType = TmpKampanj.UtfallSpots(i).Spottyp
    '        '            BT = TmpKampanj.ExtKanaler(TmpKampanj.UtfallSpots(i).KanalNr).Bokningstyp(TmpKampanj.UtfallSpots(i).Spottyp).Namn
    '        '            If BT = "Sista minuten" Then
    '        '                BT = "Last Minute"
    '        '            ElseIf BT = "Specific" Then
    '        '                BT = "Specifics"
    '        '            End If
    '        '            If Not TmpActualSpot.Channel Is Nothing Then
    '        '                On Error Resume Next
    '        '                Set TmpActualSpot.Bookingtype = TmpActualSpot.Channel.BookingTypes(BT)
    '        '                If Err.Number > 0 Then
    '        '                    Set TmpActualSpot.Bookingtype = TmpActualSpot.Channel.BookingTypes(1)
    '        '                End If
    '        '                On Error GoTo ImportOldCampaign_Error
    '        '            End If
    '        '        Next
    '        '
    '        '        For Each TmpPlannedSpot In mvarPlannedSpots
    '        '            Select Case TmpPlannedSpot.SpotType
    '        '                Case 0: TmpStr = "RBS"
    '        '                Case 1: TmpStr = "Specifics"
    '        '                Case 2: TmpStr = "Last Minute"
    '        '            End Select
    '        '            On Error Resume Next
    '        '            Dummy = TmpPlannedSpot.Channel.BookingTypes(TmpStr).Name
    '        '            If Err.Number <> 0 Then '
    '        '                TmpStr = TmpPlannedSpot.Channel.BookingTypes(1).Name
    '        '            End If
    '        '            On Error GoTo ImportOldCampaign_Error
    '        '
    '        '            If Not TmpPlannedSpot.Channel.BookingTypes(TmpStr) Is Nothing Then
    '        '                Set TmpPlannedSpot.Bookingtype = TmpPlannedSpot.Channel.BookingTypes(TmpStr)
    '        '            ElseIf TmpPlannedSpot.Channel.BookingTypes.Count > 0 Then
    '        '                Set TmpPlannedSpot.Bookingtype = TmpPlannedSpot.Channel.BookingTypes(1)
    '        '                TmpPlannedSpot.SpotType = 0
    '        '            End If
    '        '            For Each TmpWeek In TmpPlannedSpot.Bookingtype.Weeks
    '        '                If TmpPlannedSpot.AirDate >= TmpWeek.StartDate Then
    '        '                    If TmpPlannedSpot.AirDate <= TmpWeek.EndDate Then
    '        '                        Set TmpPlannedSpot.week = TmpWeek
    '        '                    End If
    '        '                End If
    '        '            Next
    '        '            If TmpPlannedSpot.week Is Nothing Then
    '        '                Set TmpPlannedSpot.week = TmpPlannedSpot.Bookingtype.Weeks(1)
    '        '            End If
    '        '            Set TmpPlannedSpot.Film = TmpPlannedSpot.week.Films(TmpPlannedSpot.Filmcode)
    '        '            TmpPlannedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = (TmpPlannedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteMainTarget) / TmpPlannedSpot.Bookingtype.BuyingTarget.UniIndex) / (TmpPlannedSpot.Bookingtype.IndexMainTarget / 100)
    '        '        Next
    '        '    End If
    '        '    Kampanj.DaypartCount = 3
    '        '    Kampanj.AllAdults = "3+"
    '        '    mvarFilename = Path
    '        '
    '        '   On Error GoTo 0
    '        '   Exit Function
    '        '
    '        'ImportOldCampaign_Error:
    '        '
    '        '    If IsIDE Then
    '        '        j = MsgBox("Error:" & Chr(10) & Chr(10) & "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description & Chr(10) & Chr(10) & "Vill du utföra en felsökning?", vbYesNo, "TRINITY")
    '        '        If j = vbNo Then Exit Function
    '        '        Stop
    '        '        Resume Next
    '        '    End If
    '        '    Screen.MousePointer = vbNormal
    '        '    MsgBox "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description & Chr(10) & "While reading: " & ReadingVar, vbCritical, "Error"
    '        '
    '        'End Function
    '        '**************************************************************************************************************


    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : Name
    '        ' DateTime  : 2003-06-11 13:38
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets the name of the campaign
    '        '---------------------------------------------------------------------------------------
    '        '

    '        Public Property Name() As String
    '            Get

    '                On Error GoTo Name_Error

    '                Name = mvarName

    '                On Error GoTo 0
    '                Exit Property

    'Name_Error:

    '                Err.Raise(Err.Number, "cKampanj: Name", Err.Description)

    '            End Get
    '            Set(ByVal Value As String)

    '                On Error GoTo Name_Error

    '                mvarName = Value

    '                On Error GoTo 0
    '                Exit Property

    'Name_Error:

    '                Err.Raise(Err.Number, "cKampanj: Name", Err.Description)

    '            End Set
    '        End Property




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : Version
    '        ' DateTime  : 2003-06-11 13:47
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets the version on wich the campaign was saved.
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property VERSION() As Byte
    '            Get

    '                On Error GoTo Version_Error

    '                VERSION = mvarVersion

    '                On Error GoTo 0
    '                Exit Property

    'Version_Error:

    '                Err.Raise(Err.Number, "cKampanj: Version", Err.Description)


    '            End Get
    '            Set(ByVal Value As Byte)

    '                On Error GoTo Version_Error

    '                mvarVersion = Value

    '                On Error GoTo 0
    '                Exit Property

    'Version_Error:

    '                Err.Raise(Err.Number, "cKampanj: Version", Err.Description)


    '            End Set
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : Planner
    '        ' DateTime  : 2003-06-11 13:48
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets the name of the responsible Planner for the campaign
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property Planner() As String
    '            Get

    '                On Error GoTo Planner_Error

    '                Planner = mvarPlanner

    '                On Error GoTo 0
    '                Exit Property

    'Planner_Error:

    '                Err.Raise(Err.Number, "cKampanj: Planner", Err.Description)


    '            End Get
    '            Set(ByVal Value As String)

    '                On Error GoTo Planner_Error

    '                mvarPlanner = Value

    '                On Error GoTo 0
    '                Exit Property

    'Planner_Error:

    '                Err.Raise(Err.Number, "cKampanj: Planner", Err.Description)


    '            End Set
    '        End Property



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : Buyer
    '        ' DateTime  : 2003-06-11 13:48
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets the name of the responsible Buyer for the campaign
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property Buyer() As String
    '            Get

    '                On Error GoTo Buyer_Error

    '                Buyer = mvarBuyer

    '                On Error GoTo 0
    '                Exit Property

    'Buyer_Error:

    '                Err.Raise(Err.Number, "cKampanj: Buyer", Err.Description)


    '            End Get
    '            Set(ByVal Value As String)

    '                On Error GoTo Buyer_Error

    '                mvarBuyer = Value

    '                On Error GoTo 0
    '                Exit Property

    'Buyer_Error:

    '                Err.Raise(Err.Number, "cKampanj: Buyer", Err.Description)


    '            End Set
    '        End Property



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : Cancelled
    '        ' DateTime  : 2003-06-11 13:48
    '        ' Author    : joho
    '        ' Purpose   : Returns/set to true if the campaign is to be regarded as cancelled
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property Cancelled() As Byte
    '            Get

    '                On Error GoTo Cancelled_Error

    '                Cancelled = mvarCancelled

    '                On Error GoTo 0
    '                Exit Property

    'Cancelled_Error:

    '                Err.Raise(Err.Number, "cKampanj: Cancelled", Err.Description)


    '            End Get
    '            Set(ByVal Value As Byte)

    '                On Error GoTo Cancelled_Error

    '                mvarCancelled = Value

    '                On Error GoTo 0
    '                Exit Property

    'Cancelled_Error:

    '                Err.Raise(Err.Number, "cKampanj: Cancelled", Err.Description)


    '            End Set
    '        End Property



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : StartDate
    '        ' DateTime  : 2003-06-11 13:49
    '        ' Author    : joho
    '        ' Purpose   : Returns the starting date of the campaign
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public ReadOnly Property StartDate() As Integer
    '            Get

    '                On Error GoTo StartDate_Error

    '                If mvarChannels(1).BookingTypes(1).Weeks.Count = 0 Then
    '                    Return 0
    '                Else
    '                    StartDate = mvarChannels(1).BookingTypes(1).Weeks(1).StartDate
    '                End If
    '                On Error GoTo 0
    '                Exit Property

    'StartDate_Error:

    '                Err.Raise(Err.Number, "cKampanj: StartDate", Err.Description)


    '            End Get
    '        End Property



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : EndDate
    '        ' DateTime  : 2003-06-11 13:49
    '        ' Author    : joho
    '        ' Purpose   : Returns the ending date of the campaign
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public ReadOnly Property EndDate() As Integer
    '            Get

    '                On Error GoTo EndDate_Error

    '                If mvarChannels(1).BookingTypes(1).Weeks.Count = 0 Then
    '                    Return 0
    '                Else
    '                    EndDate = mvarChannels(1).BookingTypes(1).Weeks(mvarChannels(1).BookingTypes(1).Weeks.Count).EndDate
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'EndDate_Error:

    '                Err.Raise(Err.Number, "cKampanj: EndDate", Err.Description)


    '            End Get
    '        End Property




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : UpdatedTo
    '        ' DateTime  : 2003-06-11 13:53
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets the date that was latest read as Actual
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property UpdatedTo() As Integer
    '            Get

    '                On Error GoTo UpdatedTo_Error

    '                If mvarUpdatedTo > 0 Then
    '                    UpdatedTo = mvarUpdatedTo
    '                Else
    '                    UpdatedTo = StartDate - 1
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'UpdatedTo_Error:

    '                Err.Raise(Err.Number, "cKampanj: UpdatedTo", Err.Description)


    '            End Get
    '            Set(ByVal Value As Integer)

    '                On Error GoTo UpdatedTo_Error

    '                mvarUpdatedTo = Value

    '                On Error GoTo 0
    '                Exit Property

    'UpdatedTo_Error:

    '                Err.Raise(Err.Number, "cKampanj: UpdatedTo", Err.Description)


    '            End Set
    '        End Property




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : MainTarget
    '        ' DateTime  : 2003-06-11 13:53
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets the main target for the campaign.
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property MainTarget() As cTarget
    '            Get

    '                On Error GoTo MainTarget_Error

    '                MainTarget = mvarMainTarget

    '                On Error GoTo 0
    '                Exit Property

    'MainTarget_Error:

    '                Err.Raise(Err.Number, "cKampanj: MainTarget", Err.Description)


    '            End Get
    '            Set(ByVal Value As cTarget)

    '                On Error GoTo MainTarget_Error

    '                mvarMainTarget = Value

    '                On Error GoTo 0
    '                Exit Property

    'MainTarget_Error:

    '                Err.Raise(Err.Number, "cKampanj: MainTarget", Err.Description)


    '            End Set
    '        End Property




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : SecondaryTarget
    '        ' DateTime  : 2003-06-11 13:53
    '        ' Author    : joho
    '        ' Purpose   : Sets/returns the secondary target for the campaign
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property SecondaryTarget() As cTarget
    '            Get

    '                On Error GoTo SecondaryTarget_Error

    '                SecondaryTarget = mvarSecondaryTarget

    '                On Error GoTo 0
    '                Exit Property

    'SecondaryTarget_Error:

    '                Err.Raise(Err.Number, "cKampanj: SecondaryTarget", Err.Description)


    '            End Get
    '            Set(ByVal Value As cTarget)

    '                On Error GoTo SecondaryTarget_Error

    '                mvarSecondaryTarget = Value

    '                On Error GoTo 0
    '                Exit Property

    'SecondaryTarget_Error:

    '                Err.Raise(Err.Number, "cKampanj: SecondaryTarget", Err.Description)


    '            End Set
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ThirdTarget
    '        ' DateTime  : 2003-06-11 13:53
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets the tertiary target of the campaign
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property ThirdTarget() As cTarget
    '            Get

    '                On Error GoTo ThirdTarget_Error

    '                ThirdTarget = mvarThirdTarget

    '                On Error GoTo 0
    '                Exit Property

    'ThirdTarget_Error:

    '                Err.Raise(Err.Number, "cKampanj: ThirdTarget", Err.Description)


    '            End Get
    '            Set(ByVal Value As cTarget)

    '                On Error GoTo ThirdTarget_Error

    '                mvarThirdTarget = Value

    '                On Error GoTo 0
    '                Exit Property

    'ThirdTarget_Error:

    '                Err.Raise(Err.Number, "cKampanj: ThirdTarget", Err.Description)


    '            End Set
    '        End Property


    '        '**************************************************************************************************************
    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ReachTargets
    '        ' DateTime  : 2003-06-11 14:48
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets the target reach on a specific frequency level
    '        '---------------------------------------------------------------------------------------
    '        '

    '        '        Public Property ReachTargets(ByVal Frequency As Byte, ByVal ReachGroup As String) As Byte
    '        '            Get

    '        '                'UPGRADE_ISSUE: Dictionary object was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
    '        '                Dim TmpDict As Dictionary
    '        '                On Error GoTo ReachTargets_Error

    '        '                'UPGRADE_WARNING: Couldn't resolve default property of object mvarRTColl.Exists. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
    '        '                If mvarRTColl.ContainsKeyExists(ReachGroup) Then
    '        '                    TmpDict = mvarRTColl(ReachGroup)
    '        '                    'UPGRADE_WARNING: Couldn't resolve default property of object TmpDict.Exists. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
    '        '                    If Not TmpDict.Exists(Frequency) Then
    '        '                        ReachTargets = 0
    '        '                    Else
    '        '                        'UPGRADE_WARNING: Couldn't resolve default property of object TmpDict(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
    '        '                        ReachTargets = TmpDict(Frequency)
    '        '                    End If
    '        '                Else
    '        '                    ReachTargets = 0
    '        '                End If

    '        '                'UPGRADE_NOTE: Object TmpDict may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
    '        '                TmpDict = Nothing
    '        '                On Error GoTo 0
    '        '                Exit Property

    '        'ReachTargets_Error:

    '        '                Err.Raise(Err.Number, "cKampanj: ReachTargets", Err.Description)


    '        '            End Get
    '        '            Set(ByVal Value As Byte)

    '        '                'UPGRADE_ISSUE: Dictionary object was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
    '        '                Dim TmpDict As New Dictionary

    '        '                On Error GoTo ReachTargets_Error

    '        '                'UPGRADE_WARNING: Couldn't resolve default property of object mvarRTColl.Exists. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
    '        '                If Not mvarRTColl.Exists(ReachGroup) Then
    '        '                    TmpDict = New Dictionary
    '        '                    'UPGRADE_WARNING: Couldn't resolve default property of object mvarRTColl.Add. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
    '        '                    mvarRTColl.Add(ReachGroup, TmpDict)
    '        '                Else
    '        '                    TmpDict = mvarRTColl(ReachGroup)
    '        '                End If
    '        '                'UPGRADE_WARNING: Couldn't resolve default property of object TmpDict.Exists. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
    '        '                If Not TmpDict.Exists(Frequency) Then
    '        '                    'UPGRADE_WARNING: Couldn't resolve default property of object TmpDict.Add. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
    '        '                    TmpDict.Add(Frequency, Value)
    '        '                Else
    '        '                    'UPGRADE_WARNING: Couldn't resolve default property of object TmpDict(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
    '        '                    TmpDict(Frequency) = Value
    '        '                End If

    '        '                On Error GoTo 0
    '        '                Exit Property

    '        'ReachTargets_Error:

    '        '                Err.Raise(Err.Number, "cKampanj: ReachTargets", Err.Description)


    '        '            End Set
    '        '        End Property


    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ReachGroups
    '        ' DateTime  : 2003-09-17 13:34
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        '        Public ReadOnly Property ReachGroups() As Dictionary
    '        '            Get

    '        '                On Error GoTo ReachGroups_Error

    '        '                ReachGroups = mvarRTColl

    '        '                On Error GoTo 0
    '        '                Exit Property

    '        'ReachGroups_Error:

    '        '                Err.Raise(Err.Number, "cKampanj: ReachGroups", Err.Description)


    '        '            End Get
    '        '        End Property
    '        '**************************************************************************************************************






    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ActualSpots
    '        ' DateTime  : 2003-06-11 14:54
    '        ' Author    : joho
    '        ' Purpose   : Property to access the Dictionary of cAcualSpot items.
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property ActualSpots() As cActualSpots
    '            Get

    '                On Error GoTo ActualSpots_Error

    '                ActualSpots = mvarActualSpots

    '                On Error GoTo 0
    '                Exit Property

    'ActualSpots_Error:

    '                Err.Raise(Err.Number, "cKampanj: ActualSpots", Err.Description)


    '            End Get
    '            Set(ByVal Value As cActualSpots)

    '                On Error GoTo ActualSpots_Error

    '                mvarActualSpots = Value

    '                On Error GoTo 0
    '                Exit Property

    'ActualSpots_Error:

    '                Err.Raise(Err.Number, "cKampanj: ActualSpots", Err.Description)


    '            End Set
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : PlannedSpots
    '        ' DateTime  : 2003-06-11 14:54
    '        ' Author    : joho
    '        ' Purpose   : Property to access the Dictionary of cPlannedSpot items
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property PlannedSpots() As cPlannedSpots
    '            Get

    '                On Error GoTo PlannedSpots_Error

    '                PlannedSpots = mvarPlannedSpots

    '                On Error GoTo 0
    '                Exit Property

    'PlannedSpots_Error:

    '                Err.Raise(Err.Number, "cKampanj: PlannedSpots", Err.Description)


    '            End Get
    '            Set(ByVal Value As cPlannedSpots)

    '                On Error GoTo PlannedSpots_Error

    '                mvarPlannedSpots = Value
    '                mvarPlannedSpots.MainObject = Me

    '                On Error GoTo 0
    '                Exit Property

    'PlannedSpots_Error:

    '                Err.Raise(Err.Number, "cKampanj: PlannedSpots", Err.Description)


    '            End Set
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : FreqencyFocus
    '        ' DateTime  : 2003-06-11 14:54
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets the frequency level on wich the campaign is optimized
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property FrequencyFocus() As Byte
    '            Get

    '                On Error GoTo FreqencyFocus_Error

    '                FrequencyFocus = mvarFrequencyFocus

    '                On Error GoTo 0
    '                Exit Property

    'FreqencyFocus_Error:

    '                Err.Raise(Err.Number, "cKampanj: FreqencyFocus", Err.Description)


    '            End Get
    '            Set(ByVal Value As Byte)

    '                On Error GoTo FreqencyFocus_Error

    '                mvarFrequencyFocus = Value

    '                On Error GoTo 0
    '                Exit Property

    'FreqencyFocus_Error:

    '                Err.Raise(Err.Number, "cKampanj: FreqencyFocus", Err.Description)


    '            End Set
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : Filename
    '        ' DateTime  : 2003-06-11 14:58
    '        ' Author    : joho
    '        ' Purpose   : Returns the last filename used to refernce the campaign.
    '        '             Is set by LoadCampaign or SaveCampaign
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public ReadOnly Property Filename() As String
    '            Get

    '                On Error GoTo Filename_Error

    '                Filename = mvarFilename

    '                On Error GoTo 0
    '                Exit Property

    'Filename_Error:

    '                Err.Raise(Err.Number, "cKampanj: Filename", Err.Description)


    '            End Get
    '        End Property




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : Product
    '        ' DateTime  : 2003-06-11 14:59
    '        ' Author    : joho
    '        ' Purpose   : Return/sets the name of the product for the campaign
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public ReadOnly Property Product() As String
    '            Get

    '                On Error GoTo Product_Error

    '                Product = mvarProduct

    '                On Error GoTo 0
    '                Exit Property

    'Product_Error:

    '                Err.Raise(Err.Number, "cKampanj: Product", Err.Description)


    '            End Get
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : Client
    '        ' DateTime  : 2003-06-11 15:00
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets the name of the client for the campaign
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public ReadOnly Property Client() As String
    '            Get

    '                On Error GoTo Client_Error

    '                Client = mvarClient

    '                On Error GoTo 0
    '                Exit Property

    'Client_Error:

    '                Err.Raise(Err.Number, "cKampanj: Client", Err.Description)


    '            End Get
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : BudgetTotalCTC
    '        ' DateTime  : 2003-06-11 15:00
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets the maximum budget with all fees included
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property BudgetTotalCTC() As Decimal
    '            Get

    '                On Error GoTo BudgetTotalCTC_Error

    '                BudgetTotalCTC = mvarBudgetTotalCTC

    '                On Error GoTo 0
    '                Exit Property

    'BudgetTotalCTC_Error:

    '                Err.Raise(Err.Number, "cKampanj: BudgetTotalCTC", Err.Description)


    '            End Get
    '            Set(ByVal Value As Decimal)

    '                On Error GoTo BudgetTotalCTC_Error

    '                mvarBudgetTotalCTC = Value

    '                On Error GoTo 0
    '                Exit Property

    'BudgetTotalCTC_Error:

    '                Err.Raise(Err.Number, "cKampanj: BudgetTotalCTC", Err.Description)


    '            End Set
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : Commentary
    '        ' DateTime  : 2003-06-12 13:46
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets a commentary that can be anything the user wants
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property Commentary() As String
    '            Get

    '                On Error GoTo Commentary_Error

    '                Commentary = mvarCommentary

    '                On Error GoTo 0
    '                Exit Property

    'Commentary_Error:

    '                Err.Raise(Err.Number, "cKampanj: Commentary", Err.Description)


    '            End Get
    '            Set(ByVal Value As String)

    '                On Error GoTo Commentary_Error

    '                mvarCommentary = Value

    '                On Error GoTo 0
    '                Exit Property

    'Commentary_Error:

    '                Err.Raise(Err.Number, "cKampanj: Commentary", Err.Description)


    '            End Set
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ControlFilmcodeFromClient
    '        ' DateTime  : 2003-06-12 14:00
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets wether the filmcodes have been received from the client
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property ControlFilmcodeFromClient() As Boolean
    '            Get

    '                On Error GoTo ControlFilmcodeFromClient_Error

    '                ControlFilmcodeFromClient = mvarControlFilmcodeFromClient

    '                On Error GoTo 0
    '                Exit Property

    'ControlFilmcodeFromClient_Error:

    '                Err.Raise(Err.Number, "cKampanj: ControlFilmcodeFromClient", Err.Description)


    '            End Get
    '            Set(ByVal Value As Boolean)

    '                On Error GoTo ControlFilmcodeFromClient_Error

    '                mvarControlFilmcodeFromClient = Value

    '                On Error GoTo 0
    '                Exit Property

    'ControlFilmcodeFromClient_Error:

    '                Err.Raise(Err.Number, "cKampanj: ControlFilmcodeFromClient", Err.Description)


    '            End Set
    '        End Property






    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ControlFilmcodeToBureau
    '        ' DateTime  : 2003-06-12 14:00
    '        ' Author    : joho
    '        ' Purpose   : Return/sets wether the filmcode has been sent to the creative bureau
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property ControlFilmcodeToBureau() As Boolean
    '            Get

    '                On Error GoTo ControlFilmcodeToBureau_Error

    '                ControlFilmcodeToBureau = mvarControlFilmcodeToBureau

    '                On Error GoTo 0
    '                Exit Property

    'ControlFilmcodeToBureau_Error:

    '                Err.Raise(Err.Number, "cKampanj: ControlFilmcodeToBureau", Err.Description)


    '            End Get
    '            Set(ByVal Value As Boolean)

    '                On Error GoTo ControlFilmcodeToBureau_Error

    '                mvarControlFilmcodeToBureau = Value

    '                On Error GoTo 0
    '                Exit Property

    'ControlFilmcodeToBureau_Error:

    '                Err.Raise(Err.Number, "cKampanj: ControlFilmcodeToBureau", Err.Description)


    '            End Set
    '        End Property






    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ControlFilmcodeToChannels
    '        ' DateTime  : 2003-06-12 14:00
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets wether the filmcode has been sent to the channels
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property ControlFilmcodeToChannels() As Boolean
    '            Get

    '                On Error GoTo ControlFilmcodeToChannels_Error

    '                ControlFilmcodeToChannels = mvarControlFilmcodeToChannels

    '                On Error GoTo 0
    '                Exit Property

    'ControlFilmcodeToChannels_Error:

    '                Err.Raise(Err.Number, "cKampanj: ControlFilmcodeToChannels", Err.Description)


    '            End Get
    '            Set(ByVal Value As Boolean)

    '                On Error GoTo ControlFilmcodeToChannels_Error

    '                mvarControlFilmcodeToChannels = Value

    '                On Error GoTo 0
    '                Exit Property

    'ControlFilmcodeToChannels_Error:

    '                Err.Raise(Err.Number, "cKampanj: ControlFilmcodeToChannels", Err.Description)


    '            End Set
    '        End Property







    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ControlTracking
    '        ' DateTime  : 2003-06-12 14:00
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets wether the tracking has been ordered
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property ControlTracking() As Boolean
    '            Get

    '                On Error GoTo ControlTracking_Error

    '                ControlTracking = mvarControlTracking

    '                On Error GoTo 0
    '                Exit Property

    'ControlTracking_Error:

    '                Err.Raise(Err.Number, "cKampanj: ControlTracking", Err.Description)


    '            End Get
    '            Set(ByVal Value As Boolean)

    '                On Error GoTo ControlTracking_Error

    '                mvarControlTracking = Value

    '                On Error GoTo 0
    '                Exit Property

    'ControlTracking_Error:

    '                Err.Raise(Err.Number, "cKampanj: ControlTracking", Err.Description)


    '            End Set
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ControlFollowedUp
    '        ' DateTime  : 2003-06-12 14:00
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets wether the campaign has been followed up
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property ControlFollowedUp() As Boolean
    '            Get

    '                On Error GoTo ControlFollowedUp_Error

    '                ControlFollowedUp = mvarControlFollowedUp

    '                On Error GoTo 0
    '                Exit Property

    'ControlFollowedUp_Error:

    '                Err.Raise(Err.Number, "cKampanj: ControlFollowedUp", Err.Description)


    '            End Get
    '            Set(ByVal Value As Boolean)

    '                On Error GoTo ControlFollowedUp_Error

    '                mvarControlFollowedUp = Value

    '                On Error GoTo 0
    '                Exit Property

    'ControlFollowedUp_Error:

    '                Err.Raise(Err.Number, "cKampanj: ControlFollowedUp", Err.Description)


    '            End Set
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ControlFollowUpToClient
    '        ' DateTime  : 2003-06-12 14:41
    '        ' Author    : joho
    '        ' Purpose   : Returns/set wether the follow-up has been sent to the client
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property ControlFollowUpToClient() As Boolean
    '            Get

    '                On Error GoTo ControlFollowUpToClient_Error

    '                ControlFollowUpToClient = mvarControlFollowUpToClient

    '                On Error GoTo 0
    '                Exit Property

    'ControlFollowUpToClient_Error:

    '                Err.Raise(Err.Number, "cKampanj: ControlFollowUpToClient", Err.Description)


    '            End Get
    '            Set(ByVal Value As Boolean)

    '                On Error GoTo ControlFollowUpToClient_Error

    '                mvarControlFollowUpToClient = Value

    '                On Error GoTo 0
    '                Exit Property

    'ControlFollowUpToClient_Error:

    '                Err.Raise(Err.Number, "cKampanj: ControlFollowUpToClient", Err.Description)


    '            End Set
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ControlTransferredToMatrix
    '        ' DateTime  : 2003-06-12 14:41
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets wether the campaign has been exported to Matrix
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property ControlTransferredToMatrix() As Boolean
    '            Get

    '                On Error GoTo ControlTransferredToMatrix_Error

    '                ControlTransferredToMatrix = mvarControlTransferredToMatrix

    '                On Error GoTo 0
    '                Exit Property

    'ControlTransferredToMatrix_Error:

    '                Err.Raise(Err.Number, "cKampanj: ControlTransferredToMatrix", Err.Description)


    '            End Get
    '            Set(ByVal Value As Boolean)

    '                On Error GoTo ControlTransferredToMatrix_Error

    '                mvarControlTransferredToMatrix = Value

    '                On Error GoTo 0
    '                Exit Property

    'ControlTransferredToMatrix_Error:

    '                Err.Raise(Err.Number, "cKampanj: ControlTransferredToMatrix", Err.Description)


    '            End Set
    '        End Property




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : Area
    '        ' DateTime  : 2003-06-12 14:42
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets the two letter abbrevation for the Area in which the
    '        '             campaign is created.
    '        '
    '        '             Examples:
    '        '                       SE - Sweden
    '        '                       NO - Norway
    '        '                       DK - Denmark
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property Area() As String
    '            Get

    '                On Error GoTo Area_Error

    '                Area = mvarArea

    '                On Error GoTo 0
    '                Exit Property

    'Area_Error:

    '                Err.Raise(Err.Number, "cKampanj: Area", Err.Description)


    '            End Get
    '            Set(ByVal Value As String)
    '                Dim DataPath As Object

    '                'UPGRADE_ISSUE: clsINI object was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
    '                Dim Ini As New clsIni
    '                Dim i As Short
    '                Dim en As Integer
    '                Dim ed As String

    '                On Error GoTo Area_Error

    '                Helper.WriteToLogFile("Set Area: Set String")
    '                mvarArea = Trim(Value)

    '                Helper.WriteToLogFile("Set Area: Set Adedge.Area")
    '                Adedge.setArea(Value)

    '                Helper.WriteToLogFile("Set Area: Open IniFile (" & Helper.Pathify(TrinitySettings.DataPath) & mvarArea & "\Area.ini)")
    '                Ini.Create(Helper.Pathify(TrinitySettings.DataPath) & mvarArea & "\Area.ini")
    '                Helper.WriteToLogFile("Set Area: Get DaypartCount")
    '                If Ini.Data("Dayparts", "Count") < 0 Or Ini.Data("Universes", "Count") < 0 Then
    '                    Err.Raise(13, , "Area.ini for area """ & mvarArea & """ is corrupted or does not exist.")
    '                End If
    '                DaypartCount = Ini.Data("Dayparts", "Count")
    '                Helper.WriteToLogFile("Set Area: Get Daypart Definitions")
    '                For i = 1 To DaypartCount
    '                    mvarDaypartName(i - 1) = Ini.Text("Dayparts", "Daypart" & i)
    '                    mvarDaypartStart(i - 1) = Ini.Data("Dayparts", "StartTime" & i)
    '                    mvarDaypartEnd(i - 1) = Ini.Data("Dayparts", "EndTime" & i)
    '                Next
    '                mvarUniverses = New Collections.Specialized.NameValueCollection
    '                For i = 1 To Ini.Data("Universes", "Count")
    '                    Universes.Add(Ini.Text("Universes", "AdedgeUni" & i), Ini.Text("Universes", "Uni" & i))
    '                Next
    '                On Error GoTo 0
    '                Exit Property

    'Area_Error:
    '                ed = Err.Description
    '                en = Err.Number
    '                Helper.WriteToLogFile("ERROR: " & ed)
    '                Err.Raise(en, "cKampanj: Area", ed)

    '            End Set
    '        End Property



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : AreaLog
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   : Set/get additional country settings
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property AreaLog() As String
    '            Get

    '                On Error GoTo AreaLog_Error

    '                AreaLog = mvarAreaLog

    '                On Error GoTo 0
    '                Exit Property

    'AreaLog_Error:

    '                Err.Raise(Err.Number, "cKampanj: AreaLog", Err.Description)


    '            End Get
    '            Set(ByVal Value As String)

    '                On Error GoTo AreaLog_Error

    '                mvarAreaLog = Value
    '                Adedge.setBrandFilmCode(Value, "")

    '                On Error GoTo 0
    '                Exit Property

    'AreaLog_Error:

    '                Err.Raise(Err.Number, "cKampanj: AreaLog", Err.Description)


    '            End Set
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ActualTotCTC
    '        ' DateTime  : 2003-06-12 14:59
    '        ' Author    : joho
    '        ' Purpose   : Returns the calculated actual cost to client
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public ReadOnly Property ActualTotCTC() As Decimal
    '            Get

    '                On Error GoTo ActualTotCTC_Error

    '                ActualTotCTC = mvarActualTotCTC

    '                On Error GoTo 0
    '                Exit Property

    'ActualTotCTC_Error:

    '                Err.Raise(Err.Number, "cKampanj: ActualTotCTC", Err.Description)


    '            End Get
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ControlOKFromClient
    '        ' DateTime  : 2003-07-02 13:50
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets wether he campaign has gotten an OK from the client
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property ControlOKFromClient() As Boolean
    '            Get

    '                On Error GoTo ControlOKFromClient_Error

    '                ControlOKFromClient = mvarControlOKFromClient

    '                On Error GoTo 0
    '                Exit Property

    'ControlOKFromClient_Error:

    '                Err.Raise(Err.Number, "cKampanj: ControlOKFromClient", Err.Description)


    '            End Get
    '            Set(ByVal Value As Boolean)

    '                On Error GoTo ControlOKFromClient_Error

    '                mvarControlOKFromClient = Value

    '                On Error GoTo 0
    '                Exit Property

    'ControlOKFromClient_Error:

    '                Err.Raise(Err.Number, "cKampanj: ControlOKFromClient", Err.Description)


    '            End Set
    '        End Property


    '        Public Property Adedge() As ConnectWrapper.Brands
    '            Get

    '                Adedge = mvarAdedge

    '            End Get
    '            Set(ByVal Value As ConnectWrapper.Brands)

    '                mvarAdedge = Value

    '            End Set
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : PlannedTotCTC
    '        ' DateTime  : 2003-08-21 16:18
    '        ' Author    : joho
    '        ' Purpose   : Returns the planned CTC
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public ReadOnly Property PlannedTotCTC() As Decimal

    '            Get
    '                Dim CostTypeFixed As Object
    '                Dim CostTypePerUnit As Object
    '                Dim CostOnUnitEnum As Object
    '                Dim CostTypePercent As Object
    '                Dim CostOnPercentEnum As Object
    '                Dim Campaign As Object

    '                On Error GoTo PlannedTotCTC_Error

    '                Dim TmpChan As cChannel
    '                Dim TmpBT As cBookingType
    '                Dim TmpWeek As cWeek
    '                Dim TmpCost As cCost
    '                Dim TotCost As Decimal
    '                Dim TotCommission As Decimal
    '                Dim TmpPlannedTotCTC As Decimal

    '                For Each TmpChan In mvarChannels
    '                    For Each TmpBT In TmpChan.BookingTypes
    '                        If TmpBT.BookIt Then
    '                            For Each TmpWeek In TmpBT.Weeks
    '                                TmpPlannedTotCTC = TmpPlannedTotCTC + TmpWeek.NetBudget
    '                                TotCommission = TotCommission + TmpWeek.NetBudget * TmpChan.AgencyCommission
    '                            Next TmpWeek
    '                        End If
    '                    Next TmpBT
    '                Next TmpChan
    '                TotCost = 0
    '                For Each TmpCost In mvarCosts
    '                    If TmpCost.CostType = cCost.CostTypeEnum.CostTypePercent Then
    '                        If TmpCost.CountCostOn = cCost.CostOnPercentEnum.CostOnMediaNet Then
    '                            TotCost = TotCost + TmpPlannedTotCTC * TmpCost.Amount
    '                        End If
    '                    End If
    '                Next TmpCost
    '                TmpPlannedTotCTC = TmpPlannedTotCTC + TotCost - TotCommission
    '                TotCost = 0
    '                For Each TmpCost In mvarCosts
    '                    If TmpCost.CostType = cCost.CostTypeEnum.CostTypePercent Then
    '                        If TmpCost.CountCostOn = cCost.CostOnPercentEnum.CostOnNet Then
    '                            TotCost = TotCost + TmpPlannedTotCTC * TmpCost.Amount
    '                        End If
    '                    End If
    '                Next TmpCost
    '                TmpPlannedTotCTC = TmpPlannedTotCTC + TotCost

    '                For Each TmpCost In mvarCosts
    '                    If TmpCost.CostType = cCost.CostTypeEnum.CostTypeFixed Then
    '                        TmpPlannedTotCTC = TmpPlannedTotCTC + TmpCost.Amount
    '                    ElseIf TmpCost.CostType = cCost.CostTypeEnum.CostTypePerUnit Then
    '                        If TmpCost.CountCostOn = cCost.CostOnUnitEnum.CostOnSpots Then
    '                            For Each TmpChan In mvarChannels
    '                                For Each TmpBT In TmpChan.BookingTypes
    '                                    TmpPlannedTotCTC = TmpPlannedTotCTC + TmpBT.EstimatedSpotCount * TmpCost.Amount
    '                                Next TmpBT
    '                            Next TmpChan
    '                        ElseIf TmpCost.CountCostOn = cCost.CostOnUnitEnum.CostOnBuyingTRP Then
    '                            For Each TmpChan In mvarChannels
    '                                For Each TmpBT In TmpChan.BookingTypes
    '                                    TmpPlannedTotCTC = TmpPlannedTotCTC + TmpBT.TotalTRPBuyingTarget * TmpCost.Amount
    '                                Next TmpBT
    '                            Next TmpChan
    '                        ElseIf TmpCost.CountCostOn = cCost.CostOnUnitEnum.CostOnMainTRP Then
    '                            For Each TmpChan In mvarChannels
    '                                For Each TmpBT In TmpChan.BookingTypes
    '                                    TmpPlannedTotCTC = TmpPlannedTotCTC + TmpBT.TotalTRP * TmpCost.Amount
    '                                Next TmpBT
    '                            Next TmpChan
    '                        End If
    '                    End If
    '                Next TmpCost
    '                TotCost = 0
    '                For Each TmpCost In mvarCosts
    '                    If TmpCost.CostType = cCost.CostTypeEnum.CostTypePercent Then
    '                        If TmpCost.CountCostOn = cCost.CostOnPercentEnum.CostOnNetNet Then
    '                            TotCost = TotCost + TmpPlannedTotCTC * TmpCost.Amount
    '                        End If
    '                    End If
    '                Next TmpCost
    '                TmpPlannedTotCTC = TmpPlannedTotCTC + TotCost

    '                PlannedTotCTC = TmpPlannedTotCTC
    '                On Error GoTo 0
    '                Exit Property

    'PlannedTotCTC_Error:

    '                Err.Raise(Err.Number, "cKampanj: PlannedTotCTC", Err.Description)

    '            End Get
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : InternalAdedge
    '        ' DateTime  : 2003-09-04 13:35
    '        ' Author    : joho
    '        ' Purpose   : Instance of ConnectWrapper.Brands to be used on internal calculations
    '        '             within the campaign
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Friend ReadOnly Property InternalAdedge() As ConnectWrapper.Brands

    '            Get

    '                On Error GoTo InternalAdedge_Error

    '                InternalAdedge = mvarInternalAdedge

    '                On Error GoTo 0
    '                Exit Property

    'InternalAdedge_Error:

    '                Err.Raise(Err.Number, "cKampanj: InternalAdedge", Err.Description)


    '            End Get
    '        End Property




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

    '                On Error GoTo 0
    '                Exit Property

    'DaypartEnd_Error:

    '                Err.Raise(Err.Number, "cKampanj: DaypartEnd", Err.Description)


    '            End Set
    '        End Property






    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : AllAdults
    '        ' DateTime  : 2003-09-23 13:43
    '        ' Author    : joho
    '        ' Purpose   : Returns/sets the AdEdge target mnemonic that represents the
    '        '             local definition of All Adults
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property AllAdults() As String

    '            Get

    '                On Error GoTo AllAdults_Error

    '                AllAdults = mvarAllAdults

    '                On Error GoTo 0
    '                Exit Property

    'AllAdults_Error:

    '                Err.Raise(Err.Number, "cKampanj: AllAdults", Err.Description)


    '            End Get
    '            Set(ByVal Value As String)

    '                On Error GoTo AllAdults_Error

    '                If Value = "" Then
    '                    mvarAllAdults = "3+"
    '                End If
    '                mvarAllAdults = Value

    '                On Error GoTo 0
    '                Exit Property

    'AllAdults_Error:

    '                Err.Raise(Err.Number, "cKampanj: AllAdults", Err.Description)


    '            End Set
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : TargColl
    '        ' DateTime  : 2003-10-07 16:27
    '        ' Author    : joho
    '        ' Purpose   : Collection with targets used in InternalAdedge
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Friend ReadOnly Property TargColl(ByVal Target As String, ByVal Adedge As Connect.Brands) As Short

    '            Get

    '                Dim i As Short
    '                Dim Temp As Short
    '                On Error GoTo TargColl_Error

    '                '    If mvarTargColl Is Nothing Then
    '                '        Set mvarTargColl = New Collection
    '                '    End If
    '                '    Set TargColl = mvarTargColl
    '                Target = Trim(Target)
    '                Temp = -1
    '                For i = 0 To Adedge.getTargetCount - 1
    '                    If Adedge.getTargetTitle(i) = Target Then
    '                        Temp = i
    '                        Exit For
    '                    End If
    '                    If Adedge.getTargetTitle(i) = "A" & Target Then
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

    'TargColl_Error:

    '                Err.Raise(Err.Number, "cKampanj: TargColl", Err.Description)


    '            End Get
    '        End Property

    '        '********************************************************************************************************
    '        'Friend Property Set TargColl(TargColl As Collection)
    '        '
    '        '   On Error GoTo TargColl_Error
    '        '
    '        '    Set mvarTargColl = TargColl
    '        '
    '        '   On Error GoTo 0
    '        '   Exit Property
    '        '
    '        'TargColl_Error:
    '        '
    '        '        Err.Raise Err.Number, "cKampanj: TargColl", Err.Description
    '        '
    '        '
    '        'End Property
    '        '**********************************************************************************************************






    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : UniColl
    '        ' DateTime  : 2003-10-07 16:28
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Friend Property UniColl() As Collection
    '            Get

    '                On Error GoTo UniColl_Error

    '                If mvarUniColl Is Nothing Then
    '                    mvarUniColl = New Collection
    '                End If
    '                UniColl = mvarUniColl

    '                On Error GoTo 0
    '                Exit Property

    'UniColl_Error:

    '                Err.Raise(Err.Number, "cKampanj: UniColl", Err.Description)


    '            End Get
    '            Set(ByVal Value As Collection)

    '                On Error GoTo UniColl_Error

    '                mvarUniColl = Value

    '                On Error GoTo 0
    '                Exit Property

    'UniColl_Error:

    '                Err.Raise(Err.Number, "cKampanj: UniColl", Err.Description)


    '            End Set
    '        End Property






    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : TargStr
    '        ' DateTime  : 2003-10-07 16:28
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
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





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : UniStr
    '        ' DateTime  : 2003-10-07 16:28
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
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


    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : DebugPath
    '        ' DateTime  : 2003-11-13 13:17
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property DebugPath() As String
    '            Get

    '                On Error GoTo DebugPath_Error

    '                DebugPath = mvarDebugPath

    '                On Error GoTo 0
    '                Exit Property

    'DebugPath_Error:

    '                Err.Raise(Err.Number, "cKampanj: DebugPath", Err.Description)


    '            End Get
    '            Set(ByVal Value As String)

    '                On Error GoTo DebugPath_Error

    '                mvarDebugPath = Value

    '                If mvarDebugPath <> "" Then
    '                    FileClose(200)
    '                    FileOpen(200, mvarDebugPath, OpenMode.Output)
    '                Else
    '                    FileClose(200)
    '                End If

    '                On Error GoTo 0
    '                Exit Property

    'DebugPath_Error:

    '                Err.Raise(Err.Number, "cKampanj: DebugPath", Err.Description)


    '            End Set
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : BookedSpots
    '        ' DateTime  : 2003-12-30 11:38
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property BookedSpots() As cBookedSpots
    '            Get

    '                On Error GoTo BookedSpots_Error

    '                BookedSpots = mvarBookedSpots

    '                On Error GoTo 0
    '                Exit Property

    'BookedSpots_Error:

    '                Err.Raise(Err.Number, "cKampanj: BookedSpots", Err.Description)


    '            End Get
    '            Set(ByVal Value As cBookedSpots)

    '                On Error GoTo BookedSpots_Error

    '                mvarBookedSpots = Value

    '                On Error GoTo 0
    '                Exit Property

    'BookedSpots_Error:

    '                Err.Raise(Err.Number, "cKampanj: BookedSpots", Err.Description)


    '            End Set
    '        End Property




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ActualSpotsCollection
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Friend ReadOnly Property ActualSpotsCollection() As Collection
    '            Get

    '                ActualSpotsCollection = mvarActualSpots.Collection

    '            End Get
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : Costs
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property Costs() As cCosts
    '            Get
    '                Return mvarCosts
    '            End Get
    '            Set(ByVal Value As cCosts)
    '                mvarCosts = Value
    '            End Set
    '        End Property



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : Contract
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property Contract() As cContract
    '            Get

    '                Contract = mvarContract

    '            End Get
    '            Set(ByVal Value As cContract)

    '                mvarContract = Value

    '            End Set
    '        End Property





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ClientID
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property ClientID() As Short
    '            Get
    '                ClientID = mvarClientID
    '            End Get
    '            Set(ByVal Value As Short)
    '                mvarClientID = Value
    '                If Value = 0 Then
    '                    mvarClient = ""
    '                Else
    '                    Dim com As New Odbc.OdbcCommand("SELECT Name FROM Clients WHERE id=" & Value, DBConn)
    '                    mvarClient = com.ExecuteScalar
    '                End If
    '            End Set
    '        End Property




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ProductID
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property ProductID() As Short
    '            Get
    '                ProductID = mvarProductID
    '            End Get
    '            Set(ByVal Value As Short)
    '                mvarProductID = Value
    '                If Value = 0 Then
    '                    mvarProduct = ""
    '                Else
    '                    Dim com As New Odbc.OdbcCommand("SELECT Name FROM Products WHERE id=" & Value, DBConn)
    '                    mvarProduct = com.ExecuteScalar
    '                End If
    '            End Set
    '        End Property




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : MarathonOtherOrder
    '        ' DateTime  : 2006-03-31 11:51
    '        ' Author    : joho
    '        ' Purpose   : Stores the Order number for the channel containing
    '        '             costs that are not channel related
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property MarathonOtherOrder() As Integer
    '            Get

    '                MarathonOtherOrder = mvarMarathonOtherOrder

    '            End Get
    '            Set(ByVal Value As Integer)

    '                mvarMarathonOtherOrder = Value

    '            End Set
    '        End Property




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : SaveCampaignToHistory
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Sub SaveCampaignToHistory(Optional ByRef Comment As String = "")
    '            Dim TmpCampaign As New cKampanj

    '            TmpCampaign.LoadCampaign("", True, SaveCampaign("", True, True, True))
    '            TmpCampaign.ID = CreateGUID()
    '            TmpCampaign.RootCampaign = Me
    '            TmpCampaign.HistoryComment = Comment
    '            TmpCampaign.HistoryDate = Now
    '            History.Add(TmpCampaign.ID, TmpCampaign)

    '        End Sub




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : RevertToRootCampaign
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Sub RevertToRootCampaign()
    '            If Not RootCampaign Is Nothing Then
    '                Campaign = RootCampaign
    '            End If
    '        End Sub



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : OpenHistory
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Sub OpenHistory(ByRef Index As String)

    '            If Not RootCampaign Is Nothing Then
    '                RootCampaign.OpenHistory(Index)
    '            Else
    '                Campaign = History(Index)
    '            End If

    '        End Sub




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : New
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Sub New()
    '            MyBase.New()

    '            Helper.WriteToLogFile("Register Connect.dll")
    '            mvarInternalAdedge = New ConnectWrapper.Brands
    '            Helper.WriteToLogFile("OK")
    '            mvarActualSpots.MainObject = Me
    '            mvarChannels.MainObject = Me
    '            mvarBookedSpots.MainObject = Me

    '            mvarMainTarget.MainObject = Me
    '            mvarSecondaryTarget.MainObject = Me
    '            mvarThirdTarget.MainObject = Me

    '            mvarArea = TrinitySettings.DefaultArea
    '            mvarAreaLog = TrinitySettings.DefaultAreaLog

    '            ID = CreateGUID()
    '            Loading = False
    '        End Sub




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : Class_Terminate_Renamed
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Private Sub Class_Terminate_Renamed()

    '            mvarActualSpots = Nothing

    '            mvarPlannedSpots = Nothing

    '            FileClose()
    '            mvarInternalAdedge = Nothing
    '            mvarTargColl = Nothing
    '            mvarUniColl = Nothing
    '            mvarBookedSpots = Nothing
    '            mvarContract = Nothing
    '        End Sub




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : Finalize
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Protected Overrides Sub Finalize()
    '            Class_Terminate_Renamed()
    '            MyBase.Finalize()
    '        End Sub




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : AddValueToNode
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Sub AddValueToNode(ByRef doc As Xml.XmlDocument, ByRef Node As Xml.XmlNode, ByRef Name As String, ByRef Value As Object)

    '            Node.AppendChild(doc.CreateNode(XmlNodeType.Element, Name, "")).AppendChild(doc.CreateTextNode(Value))

    '        End Sub




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : SaveCampaign
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Function SaveCampaign(Optional ByRef Path As String = "", Optional ByRef DoNotSaveToFile As Boolean = False, Optional ByRef SkipHistory As Boolean = False, Optional ByRef SkipLab As Boolean = False) As String

    '            Helper.WriteToLogFile("Start saving")
    '            Dim TmpChannel As cChannel
    '            Dim TmpBookingType As cBookingType
    '            Dim TmpWeek As cWeek
    '            Dim TmpFilm As cFilm
    '            Dim TmpPLTarget As cPricelistTarget
    '            Dim TmpPlannedSpot As cPlannedSpot
    '            Dim TmpActualSpot As cActualSpot
    '            Dim TmpCost As cCost
    '            Dim TmpIndex As cIndex
    '            Dim TmpAV As cAddedValue

    '            Dim Tmpstr As String

    '            Dim i As Short
    '            Dim j As Short

    '            Dim a As Object
    '            Dim b As Object

    '            On Error GoTo SaveCampaign_Error

    '            If Loading Or Saving Then
    '                SaveCampaign = ""
    '                Exit Function
    '            End If

    '            'Saving = True

    '            If Not DoNotSaveToFile Then
    '                If Path = "" And mvarFilename = "" Then
    '                    SaveCampaign = CStr(1)
    '                    Exit Function
    '                ElseIf Path = "" Then
    '                    Path = mvarFilename
    '                End If

    '                On Error Resume Next
    '                Helper.WriteToLogFile("Copy Files")
    '                FileCopy(Path, Path & ".bak")
    '                Kill(Path)
    '            End If
    '            On Error GoTo SaveCampaign_Error

    '            mvarVersion = MY_VERSION

    '            Helper.WriteToLogFile("Init XML")
    '            Dim XMLDoc As New Xml.XmlDocument
    '            Dim XMLTmpDoc As New Xml.XmlDocument
    '            Dim XMLCamp As Xml.XmlElement
    '            Dim TmpNode As Xml.XmlElement
    '            Dim XMLTmpNode As Xml.XmlElement
    '            Dim XMLTmpNode2 As Xml.XmlElement
    '            Dim XMLTmpPI As Xml.XmlProcessingInstruction
    '            Dim XMLWeek As Xml.XmlElement
    '            Dim XMLWeeks As Xml.XmlElement
    '            Dim XMLChannel As Xml.XmlElement
    '            Dim XMLChannels As Xml.XmlElement
    '            Dim XMLBookingType As Xml.XmlElement
    '            Dim XMLBookingTypes As Xml.XmlElement
    '            Dim XMLTarget As Xml.XmlElement
    '            Dim XMLTargets As Xml.XmlElement
    '            Dim XMLFilm As Xml.XmlElement
    '            Dim XMLFilms As Xml.XmlElement
    '            Dim XMLBuyTarget As Xml.XmlElement
    '            Dim XMLPricelist As Xml.XmlElement
    '            Dim XMLSpots As Xml.XmlElement
    '            Dim XMLSpot As Xml.XmlElement
    '            Dim XMLContractDoc As New Xml.XmlDocument
    '            Dim XMLContract As Xml.XmlElement
    '            Dim XMLIndexes As Xml.XmlElement
    '            Dim XMLIndex As Xml.XmlElement

    '            Dim Node As Object

    '            Helper.WriteToLogFile("Start creating document")
    '            XMLDoc.PreserveWhitespace = True
    '            Node = XMLDoc.CreateProcessingInstruction("xml", "version='1.0'")
    '            XMLDoc.AppendChild(Node)

    '            Node = XMLDoc.CreateComment("Trinity campaign.")
    '            XMLDoc.AppendChild(Node)

    '            XMLCamp = XMLDoc.CreateElement("Campaign")
    '            XMLDoc.AppendChild(XMLCamp)
    '            XMLCamp.SetAttribute("Version", mvarVersion)
    '            XMLCamp.SetAttribute("Name", mvarName)
    '            XMLCamp.SetAttribute("ID", ID)
    '            XMLCamp.SetAttribute("UpdatedTo", mvarUpdatedTo)
    '            XMLCamp.SetAttribute("Planner", mvarPlanner) 'as String
    '            XMLCamp.SetAttribute("Buyer", mvarBuyer) 'as String
    '            XMLCamp.SetAttribute("Cancelled", mvarCancelled) 'as Byte
    '            XMLCamp.SetAttribute("FrequencyFocus", mvarFrequencyFocus) 'as Byte
    '            XMLCamp.SetAttribute("Filename", mvarFilename) 'as String
    '            XMLCamp.SetAttribute("ProductID", mvarProductID) 'as String
    '            XMLCamp.SetAttribute("ClientID", mvarClientID) 'as String
    '            XMLCamp.SetAttribute("BudgetTotalCTC", mvarBudgetTotalCTC) 'as Currency
    '            XMLCamp.SetAttribute("ActualTotCTC", mvarActualTotCTC) 'as Currency
    '            XMLCamp.SetAttribute("MarathonCTC", mvarMarathonCTC) 'as Currency
    '            XMLCamp.SetAttribute("Commentary", mvarCommentary) 'as String
    '            XMLCamp.SetAttribute("ControlFilmcodeFromClient", mvarControlFilmcodeFromClient) 'as Boolean
    '            XMLCamp.SetAttribute("ControlFilmcodeToBureau", mvarControlFilmcodeToBureau) 'as Boolean
    '            XMLCamp.SetAttribute("ControlFilmcodeToChannels", mvarControlFilmcodeToChannels) 'as Boolean
    '            XMLCamp.SetAttribute("ControlOKFromClient", mvarControlOKFromClient) 'as Boolean
    '            XMLCamp.SetAttribute("ControlTracking", mvarControlTracking) 'as Boolean
    '            XMLCamp.SetAttribute("ControlFollowedUp", mvarControlFollowedUp) 'as Boolean
    '            XMLCamp.SetAttribute("ControlFollowUpToClient", mvarControlFollowUpToClient) 'as Boolean
    '            XMLCamp.SetAttribute("ControlTransferredToMatrix", mvarControlTransferredToMatrix) 'as Boolean
    '            XMLCamp.SetAttribute("Area", mvarArea) 'as String
    '            XMLCamp.SetAttribute("AreaLog", mvarAreaLog) 'as String
    '            XMLCamp.SetAttribute("AllAdults", mvarAllAdults)
    '            XMLCamp.SetAttribute("MarathonOtherOrder", mvarMarathonOtherOrder)
    '            XMLCamp.SetAttribute("MarathonPlan", MarathonPlanNr)

    '            XMLCamp.SetAttribute("HistoryDate", HistoryDate)
    '            XMLCamp.SetAttribute("HistoryComment", HistoryComment)

    '            XMLTmpNode = XMLDoc.CreateElement("Dayparts")

    '            For i = 0 To DaypartCount - 1
    '                XMLTmpNode2 = XMLDoc.CreateElement(mvarDaypartName(i))
    '                XMLTmpNode2.SetAttribute("Start", mvarDaypartStart(i))
    '                XMLTmpNode2.SetAttribute("End", mvarDaypartEnd(i))
    '                XMLTmpNode.AppendChild(XMLTmpNode2)
    '            Next
    '            XMLCamp.AppendChild(XMLTmpNode)

    '            XMLTmpNode = XMLDoc.CreateElement("Reach")
    '            For i = 1 To 10
    '                XMLTmpNode2 = XMLDoc.CreateElement("RF")
    '                XMLTmpNode2.SetAttribute("Freq", i)
    '                XMLTmpNode2.SetAttribute("Reach", mvarReachGoal(i))
    '                XMLTmpNode.AppendChild(XMLTmpNode2)
    '            Next
    '            XMLCamp.AppendChild(XMLTmpNode)

    '            'Save Contract

    '            If mvarContract Is Nothing Then
    '                XMLContract = XMLDoc.CreateElement("Contract")
    '                XMLCamp.AppendChild(XMLContract)
    '            Else
    '                XMLContractDoc.LoadXml(mvarContract.Save("", True))
    '                XMLContract = XMLDoc.ImportNode(XMLContractDoc.GetElementsByTagName("Contract").Item(0), True)
    '                XMLCamp.AppendChild(XMLContract)
    '            End If

    '            'Save costs

    '            Node = XMLDoc.CreateElement("Costs")
    '            For Each TmpCost In mvarCosts
    '                TmpNode = XMLDoc.CreateElement("Node")
    '                TmpNode.SetAttribute("Name", TmpCost.CostName)
    '                TmpNode.SetAttribute("ID", TmpCost.ID)
    '                TmpNode.SetAttribute("Amount", TmpCost.Amount)
    '                TmpNode.SetAttribute("CostOn", TmpCost.CountCostOn)
    '                TmpNode.SetAttribute("CostType", TmpCost.CostType)
    '                TmpNode.SetAttribute("MarathonID", TmpCost.MarathonID)
    '                Node.appendChild(TmpNode)
    '            Next TmpCost
    '            XMLCamp.AppendChild(Node)

    '            'Save channels and all sub-collections and objects

    '            XMLChannels = XMLDoc.CreateElement("Channels")

    '            For Each TmpChannel In mvarChannels

    '                XMLChannel = XMLDoc.CreateElement("Channel")
    '                XMLChannel.SetAttribute("Name", TmpChannel.ChannelName) ' as String
    '                XMLChannel.SetAttribute("ID", TmpChannel.ID) ' as Long
    '                XMLChannel.SetAttribute("ShortName", TmpChannel.Shortname) ' as String
    '                XMLChannel.SetAttribute("BuyingUniverse", TmpChannel.BuyingUniverse) ' as String
    '                XMLChannel.SetAttribute("AdEdgeNames", TmpChannel.AdEdgeNames) ' as String
    '                XMLChannel.SetAttribute("DefaultArea", TmpChannel.DefaultArea) ' as String
    '                XMLChannel.SetAttribute("AgencyCommission", TmpChannel.AgencyCommission) ' as Single
    '                XMLChannel.SetAttribute("Marathon", TmpChannel.MarathonName)
    '                XMLChannel.SetAttribute("DeliveryAddress", TmpChannel.DeliveryAddress)

    '                'Save the targets

    '                XMLTarget = XMLDoc.CreateElement("MainTarget")
    '                XMLTarget.SetAttribute("Name", TmpChannel.MainTarget.TargetName) ' as New cTarget
    '                XMLTarget.SetAttribute("Universe", TmpChannel.MainTarget.Universe) ' as New cTarget
    '                XMLTarget.SetAttribute("SecondUniverse", TmpChannel.MainTarget.SecondUniverse)
    '                XMLChannel.AppendChild(XMLTarget)

    '                XMLTarget = XMLDoc.CreateElement("SecondaryTarget")
    '                XMLTarget.SetAttribute("Name", TmpChannel.SecondaryTarget.TargetName) ' as New cTarget
    '                XMLTarget.SetAttribute("Type", TmpChannel.SecondaryTarget.TargetType) ' as New cTarget
    '                XMLTarget.SetAttribute("Universe", TmpChannel.SecondaryTarget.Universe) ' as New cTarget
    '                XMLTarget.SetAttribute("SecondUniverse", TmpChannel.SecondaryTarget.SecondUniverse)
    '                XMLChannel.AppendChild(XMLTarget)

    '                'Save Booking types

    '                XMLBookingTypes = XMLDoc.CreateElement("BookingTypes")

    '                For Each TmpBookingType In TmpChannel.BookingTypes

    '                    XMLBookingType = XMLDoc.CreateElement("BookingType") ' as String
    '                    XMLBookingType.SetAttribute("Name", TmpBookingType.Name) ' as String
    '                    XMLBookingType.SetAttribute("Shortname", TmpBookingType.Shortname) ' as String

    '                    'Save the rest of the booking type

    '                    XMLBookingType.SetAttribute("IndexMainTarget", TmpBookingType.IndexMainTarget) ' as Integer
    '                    XMLBookingType.SetAttribute("IndexAllAdults", TmpBookingType.IndexAllAdults) ' as Integer
    '                    XMLBookingType.SetAttribute("BookIt", TmpBookingType.BookIt) ' as Boolean
    '                    XMLBookingType.SetAttribute("GrossCPP", TmpBookingType.GrossCPP) ' as Single
    '                    XMLBookingType.SetAttribute("AverageRating", TmpBookingType.AverageRating) ' as Single
    '                    XMLBookingType.SetAttribute("ConfirmedNetBudget", TmpBookingType.ConfirmedNetBudget) ' as Currency
    '                    XMLBookingType.SetAttribute("Bookingtype", TmpBookingType.Bookingtype) ' as Byte
    '                    XMLBookingType.SetAttribute("ContractNumber", TmpBookingType.ContractNumber) ' as String
    '                    XMLBookingType.SetAttribute("OrderNumber", TmpBookingType.OrderNumber)
    '                    XMLBookingType.SetAttribute("IsRBS", TmpBookingType.IsRBS) ' as Boolean
    '                    XMLBookingType.SetAttribute("IsSpecific", TmpBookingType.IsSpecific) ' as Boolean

    '                    XMLTmpNode = XMLDoc.CreateElement("DaypartSplit")
    '                    For i = 0 To DaypartCount - 1
    '                        XMLTmpNode2 = XMLDoc.CreateElement(DaypartName(i))
    '                        XMLTmpNode2.SetAttribute("Share", TmpBookingType.DaypartSplit(i)) ' as Byte
    '                        XMLTmpNode.AppendChild(XMLTmpNode2)
    '                    Next
    '                    XMLBookingType.AppendChild(XMLTmpNode)

    '                    'Save Indexes, AddedValues and Spotindexes

    '                    XMLIndexes = XMLDoc.CreateElement("Indexes")
    '                    For Each TmpIndex In TmpBookingType.Indexes
    '                        XMLIndex = XMLDoc.CreateElement("Index")
    '                        XMLIndex.SetAttribute("ID", TmpIndex.ID)
    '                        XMLIndex.SetAttribute("Name", TmpIndex.Name)
    '                        For i = 0 To DaypartCount - 1
    '                            XMLIndex.SetAttribute("IndexDP" & i, TmpIndex.Index(i))
    '                        Next
    '                        XMLIndex.SetAttribute("IndexOn", TmpIndex.IndexOn)
    '                        XMLIndex.SetAttribute("SystemGenerated", TmpIndex.SystemGenerated)
    '                        XMLIndex.SetAttribute("FromDate", TmpIndex.FromDate)
    '                        XMLIndex.SetAttribute("ToDate", TmpIndex.ToDate)
    '                        XMLIndexes.AppendChild(XMLIndex)
    '                    Next TmpIndex
    '                    XMLBookingType.AppendChild(XMLIndexes)
    '                    XMLIndexes = XMLDoc.CreateElement("AddedValues")
    '                    For Each TmpAV In TmpBookingType.AddedValues
    '                        XMLIndex = XMLDoc.CreateElement("AddedValue")
    '                        XMLIndex.SetAttribute("ID", TmpAV.ID)
    '                        XMLIndex.SetAttribute("Name", TmpAV.Name)
    '                        XMLIndex.SetAttribute("IndexGross", TmpAV.IndexGross)
    '                        XMLIndex.SetAttribute("IndexNet", TmpAV.IndexNet)
    '                        XMLIndexes.AppendChild(XMLIndex)
    '                    Next TmpAV
    '                    XMLBookingType.AppendChild(XMLIndexes)
    '                    Dim XMLComps As Xml.XmlElement = XMLDoc.CreateElement("Compensations")
    '                    For Each TmpComp As Trinity.cCompensation In TmpBookingType.Compensations
    '                        Dim XMLComp As Xml.XmlElement = XMLDoc.CreateElement("Compensation")
    '                        XMLComp.SetAttribute("From", TmpComp.FromDate)
    '                        XMLComp.SetAttribute("To", TmpComp.ToDate)
    '                        XMLComp.SetAttribute("TRPs", TmpComp.TRPs)
    '                        XMLComp.SetAttribute("Comment", TmpComp.Comment)
    '                        XMLComps.AppendChild(XMLComp)
    '                    Next
    '                    XMLBookingType.AppendChild(XMLComps)

    '                    'Save the pricelist

    '                    XMLPricelist = XMLDoc.CreateElement("Pricelist")

    '                    XMLPricelist.SetAttribute("StartDate", TmpBookingType.Pricelist.StartDate)
    '                    XMLPricelist.SetAttribute("EndDate", TmpBookingType.Pricelist.EndDate)
    '                    XMLPricelist.SetAttribute("BuyingUniverse", TmpBookingType.Pricelist.BuyingUniverse)

    '                    XMLTargets = XMLDoc.CreateElement("Targets")
    '                    For Each TmpPLTarget In TmpBookingType.Pricelist.Targets
    '                        XMLBuyTarget = XMLDoc.CreateElement("BuyingTarget")
    '                        XMLBuyTarget.SetAttribute("Name", TmpPLTarget.TargetName)
    '                        XMLBuyTarget.SetAttribute("CalcCPP", TmpPLTarget.CalcCPP) ' as New cPriceListTarget
    '                        XMLBuyTarget.SetAttribute("CPP", TmpPLTarget.CPP)

    '                        XMLBuyTarget.SetAttribute("UniSize", TmpPLTarget.UniSize)
    '                        XMLBuyTarget.SetAttribute("UniSizeNat", TmpPLTarget.UniSizeNat)

    '                        XMLBuyTarget.SetAttribute("Discount", TmpPLTarget.Discount) ' as Single
    '                        XMLBuyTarget.SetAttribute("NetCPT", TmpPLTarget.NetCPT) ' as Single
    '                        XMLBuyTarget.SetAttribute("NetCPP", TmpPLTarget.NetCPP) ' as Single
    '                        XMLBuyTarget.SetAttribute("IsEntered", TmpPLTarget.IsEntered) ' as Single

    '                        XMLTmpNode = XMLDoc.CreateElement("DaypartSplit")
    '                        For i = 0 To DaypartCount - 1
    '                            XMLTmpNode2 = XMLDoc.CreateElement(DaypartName(i))
    '                            XMLTmpNode2.SetAttribute("DefaultSplit", TmpPLTarget.DefaultDaypart(i)) ' as New cPriceList
    '                            XMLTmpNode.AppendChild(XMLTmpNode2)
    '                        Next
    '                        XMLBuyTarget.AppendChild(XMLTmpNode)

    '                        XMLTarget = XMLDoc.CreateElement("Target")
    '                        XMLTarget.SetAttribute("Name", TmpPLTarget.Target.TargetName)
    '                        XMLTarget.SetAttribute("Type", TmpPLTarget.Target.TargetType)
    '                        XMLTarget.SetAttribute("Universe", TmpPLTarget.Target.Universe)
    '                        XMLTarget.SetAttribute("SecondUniverse", TmpPLTarget.Target.SecondUniverse)
    '                        XMLTarget.SetAttribute("StandardTarget", TmpPLTarget.StandardTarget)
    '                        XMLBuyTarget.AppendChild(XMLTarget)

    '                        XMLTmpNode = XMLDoc.CreateElement("DaypartCPP")
    '                        For i = 0 To DaypartCount - 1
    '                            XMLTmpNode2 = XMLDoc.CreateElement(DaypartName(i))

    '                            XMLTmpNode2.SetAttribute("CPP", TmpPLTarget.CPPDaypart(i))
    '                            XMLTmpNode.AppendChild(XMLTmpNode2)
    '                        Next
    '                        XMLBuyTarget.AppendChild(XMLTmpNode)
    '                        XMLTargets.AppendChild(XMLBuyTarget)
    '                    Next TmpPLTarget

    '                    XMLPricelist.AppendChild(XMLTargets)
    '                    XMLBookingType.AppendChild(XMLPricelist)

    '                    'Save Buyingtarget

    '                    XMLBuyTarget = XMLDoc.CreateElement("BuyingTarget")

    '                    XMLBuyTarget.SetAttribute("CalcCPP", TmpBookingType.BuyingTarget.CalcCPP) ' as New cPriceListTarget
    '                    XMLBuyTarget.SetAttribute("CPP", TmpBookingType.BuyingTarget.CPP)
    '                    XMLBuyTarget.SetAttribute("TargetName", TmpBookingType.BuyingTarget.TargetName)
    '                    XMLBuyTarget.SetAttribute("UniSize", TmpBookingType.BuyingTarget.UniSize)
    '                    XMLBuyTarget.SetAttribute("UniSizeNat", TmpBookingType.BuyingTarget.UniSizeNat)
    '                    XMLTmpNode = XMLDoc.CreateElement("DaypartCPP")
    '                    For i = 0 To DaypartCount - 1
    '                        XMLTmpNode.SetAttribute(mvarDaypartName(i), TmpBookingType.BuyingTarget.CPPDaypart(i))
    '                    Next
    '                    XMLBuyTarget.AppendChild(XMLTmpNode)

    '                    XMLBuyTarget.SetAttribute("IsEntered", TmpBookingType.BuyingTarget.IsEntered)
    '                    If TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
    '                        XMLBuyTarget.SetAttribute("Amount", TmpBookingType.BuyingTarget.NetCPP)
    '                    ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPT Then
    '                        XMLBuyTarget.SetAttribute("Amount", TmpBookingType.BuyingTarget.NetCPT)
    '                    Else
    '                        XMLBuyTarget.SetAttribute("Amount", TmpBookingType.BuyingTarget.Discount)
    '                    End If

    '                    XMLTarget = XMLDoc.CreateElement("Target")
    '                    XMLTarget.SetAttribute("Name", TmpBookingType.BuyingTarget.Target.TargetName)
    '                    XMLTarget.SetAttribute("Type", TmpBookingType.BuyingTarget.Target.TargetType)
    '                    XMLTarget.SetAttribute("Universe", TmpBookingType.BuyingTarget.Target.Universe)
    '                    XMLTarget.SetAttribute("SecondUniverse", TmpBookingType.BuyingTarget.Target.SecondUniverse)

    '                    XMLIndexes = XMLDoc.CreateElement("Indexes")
    '                    For Each TmpIndex In TmpBookingType.BuyingTarget.Indexes
    '                        XMLIndex = XMLDoc.CreateElement("Index")
    '                        XMLIndex.SetAttribute("ID", TmpIndex.ID)
    '                        XMLIndex.SetAttribute("Name", TmpIndex.Name)
    '                        For i = 0 To DaypartCount - 1
    '                            XMLIndex.SetAttribute("IndexDP" & i, TmpIndex.Index(i))
    '                        Next
    '                        XMLIndex.SetAttribute("IndexOn", TmpIndex.IndexOn)
    '                        XMLIndex.SetAttribute("SystemGenerated", TmpIndex.SystemGenerated)
    '                        XMLIndex.SetAttribute("FromDate", TmpIndex.FromDate)
    '                        XMLIndex.SetAttribute("ToDate", TmpIndex.ToDate)
    '                        XMLIndexes.AppendChild(XMLIndex)
    '                    Next TmpIndex
    '                    XMLBuyTarget.AppendChild(XMLIndexes)

    '                    XMLBuyTarget.AppendChild(XMLTarget)

    '                    XMLBookingType.AppendChild(XMLBuyTarget)

    '                    XMLIndexes = XMLDoc.CreateElement("SpotIndex")
    '                    For i = 0 To 500
    '                        If TmpBookingType.FilmIndex(i) > 0 Then
    '                            XMLIndex = XMLDoc.CreateElement("Index")
    '                            XMLIndex.SetAttribute("Length", i)
    '                            XMLIndex.SetAttribute("Idx", TmpBookingType.FilmIndex(i))
    '                            XMLIndexes.AppendChild(XMLIndex)
    '                        End If
    '                    Next
    '                    XMLBookingType.AppendChild(XMLIndexes)

    '                    'Save weeks

    '                    Dim w As Integer = 0
    '                    XMLWeeks = XMLDoc.CreateElement("Weeks")
    '                    For Each TmpWeek In TmpBookingType.Weeks
    '                        w += 1
    '                        XMLWeek = XMLDoc.CreateElement("Week") 'as String
    '                        XMLWeek.SetAttribute("Name", TmpWeek.Name)
    '                        XMLWeek.SetAttribute("TRPControl", TmpWeek.TRPControl)
    '                        XMLWeek.SetAttribute("TRP", TmpWeek.TRP) 'as Single
    '                        XMLWeek.SetAttribute("TRPBuyingTarget", TmpWeek.TRPBuyingTarget) 'as Single
    '                        XMLWeek.SetAttribute("TRPAllAdults", TmpWeek.TRPAllAdults) 'as Single
    '                        XMLWeek.SetAttribute("NetBudget", TmpWeek.NetBudget) 'as Currency
    '                        XMLWeek.SetAttribute("Discount", TmpWeek.Discount) 'as Single
    '                        XMLWeek.SetAttribute("StartDate", TmpWeek.StartDate) 'as Long
    '                        XMLWeek.SetAttribute("EndDate", TmpWeek.EndDate) 'as Long
    '                        XMLWeek.SetAttribute("GrossIndex", TmpWeek.GrossIndex)

    '                        'Added values

    '                        XMLIndexes = XMLDoc.CreateElement("AddedValues")
    '                        For Each TmpAV In TmpBookingType.AddedValues
    '                            XMLIndex = XMLDoc.CreateElement("AddedValue")
    '                            XMLIndex.SetAttribute("ID", TmpAV.ID)
    '                            XMLIndex.SetAttribute("Amount", TmpAV.Amount(w))
    '                            XMLIndexes.AppendChild(XMLIndex)
    '                        Next
    '                        XMLWeek.AppendChild(XMLIndexes)

    '                        'Save Films

    '                        XMLFilms = XMLDoc.CreateElement("Films")

    '                        For Each TmpFilm In TmpWeek.Films

    '                            XMLFilm = XMLDoc.CreateElement("Film")
    '                            XMLFilm.SetAttribute("Name", TmpFilm.Name)
    '                            XMLFilm.SetAttribute("Filmcode", TmpFilm.Filmcode) ' as String
    '                            '                    XMLFilm.setAttribute "AltFilmcode", TmpFilm.AltFilmcode ' as String
    '                            XMLFilm.SetAttribute("FilmLength", TmpFilm.FilmLength) ' as Byte
    '                            XMLFilm.SetAttribute("Index", TmpFilm.Index) ' as Single
    '                            XMLFilm.SetAttribute("Share", TmpFilm.Share) ' as Integer
    '                            XMLFilm.SetAttribute("Description", TmpFilm.Description) ' as String
    '                            XMLFilms.AppendChild(XMLFilm)
    '                        Next TmpFilm
    '                        XMLWeek.AppendChild(XMLFilms)
    '                        XMLWeeks.AppendChild(XMLWeek)
    '                    Next TmpWeek
    '                    XMLBookingType.AppendChild(XMLWeeks)
    '                    XMLBookingTypes.AppendChild(XMLBookingType)
    '                Next TmpBookingType
    '                XMLChannel.AppendChild(XMLBookingTypes)
    '                XMLChannels.AppendChild(XMLChannel)
    '            Next TmpChannel
    '            XMLCamp.AppendChild(XMLChannels)

    '            'Save the targets
    '            XMLTarget = XMLDoc.CreateElement("MainTarget")
    '            XMLTarget.SetAttribute("Name", mvarMainTarget.TargetName) ' as New cTarget
    '            XMLTarget.SetAttribute("Type", mvarMainTarget.TargetType) ' as New cTarget
    '            XMLTarget.SetAttribute("Universe", mvarMainTarget.Universe) ' as New cTarget
    '            XMLTarget.SetAttribute("SecondUniverse", mvarMainTarget.SecondUniverse) ' as New cTarget
    '            XMLCamp.AppendChild(XMLTarget)

    '            XMLTarget = XMLDoc.CreateElement("SecondaryTarget")
    '            XMLTarget.SetAttribute("Name", mvarSecondaryTarget.TargetName) ' as New cTarget
    '            XMLTarget.SetAttribute("Type", mvarSecondaryTarget.TargetType) ' as New cTarget
    '            XMLTarget.SetAttribute("Universe", mvarSecondaryTarget.Universe) ' as New cTarget
    '            XMLTarget.SetAttribute("SecondUniverse", mvarSecondaryTarget.SecondUniverse) ' as New cTarget
    '            XMLCamp.AppendChild(XMLTarget)

    '            XMLTarget = XMLDoc.CreateElement("ThirdTarget")
    '            XMLTarget.SetAttribute("Name", mvarThirdTarget.TargetName) ' as New cTarget
    '            XMLTarget.SetAttribute("Universe", mvarThirdTarget.Universe) ' as New cTarget
    '            XMLTarget.SetAttribute("SecondUniverse", mvarThirdTarget.SecondUniverse) ' as New cTarget
    '            XMLCamp.AppendChild(XMLTarget)

    '            'Save Planned spots

    '            XMLSpots = XMLDoc.CreateElement("PlannedSpots")

    '            For Each TmpPlannedSpot In mvarPlannedSpots

    '                XMLSpot = XMLDoc.CreateElement("Spot")
    '                XMLSpot.SetAttribute("ID", TmpPlannedSpot.ID) ' as String
    '                XMLSpot.SetAttribute("Channel", TmpPlannedSpot.Channel.ChannelName) ' as cChannel
    '                XMLSpot.SetAttribute("ChannelID", TmpPlannedSpot.ChannelID) ' as String
    '                XMLSpot.SetAttribute("BookingType", TmpPlannedSpot.Bookingtype.Name) ' as cBookingType
    '                On Error Resume Next
    '                XMLSpot.SetAttribute("Week", TmpPlannedSpot.Week.Name) ' as cWeek
    '                If Err.Number <> 0 Then
    '                    XMLSpot.SetAttribute("Week", "") ' as cWeek
    '                End If
    '                On Error GoTo SaveCampaign_Error
    '                XMLSpot.SetAttribute("AirDate", TmpPlannedSpot.AirDate) ' as Long
    '                XMLSpot.SetAttribute("MaM", TmpPlannedSpot.MaM) ' as Integer
    '                XMLSpot.SetAttribute("ProgBefore", TmpPlannedSpot.ProgBefore) ' as String
    '                XMLSpot.SetAttribute("ProgAfter", TmpPlannedSpot.ProgAfter) ' as String
    '                XMLSpot.SetAttribute("Programme", TmpPlannedSpot.Programme) ' as String
    '                XMLSpot.SetAttribute("Advertiser", TmpPlannedSpot.Advertiser) ' as String
    '                XMLSpot.SetAttribute("Product", TmpPlannedSpot.Product) ' as String
    '                XMLSpot.SetAttribute("Filmcode", TmpPlannedSpot.Filmcode) ' as String
    '                'Put #99, , TmpPlannedSpot.Film ' as cFilm
    '                XMLSpot.SetAttribute("RatingBuyTarget", TmpPlannedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)) ' as Single
    '                XMLSpot.SetAttribute("Estimation", TmpPlannedSpot.Estimation)
    '                XMLSpot.SetAttribute("MyRating", TmpPlannedSpot.MyRating) ' as Single
    '                XMLSpot.SetAttribute("Index", TmpPlannedSpot.Index) ' as Integer
    '                XMLSpot.SetAttribute("SpotLength", TmpPlannedSpot.SpotLength) ' as Byte
    '                XMLSpot.SetAttribute("SpotType", TmpPlannedSpot.SpotType) ' as Byte
    '                XMLSpot.SetAttribute("PriceNet", TmpPlannedSpot.PriceNet) ' as Currency
    '                XMLSpot.SetAttribute("PriceGross", TmpPlannedSpot.PriceGross) ' as Currency
    '                XMLSpot.SetAttribute("Remark", TmpPlannedSpot.Remark)
    '                XMLSpot.SetAttribute("Placement", TmpPlannedSpot.Placement)
    '                XMLSpots.AppendChild(XMLSpot)
    '            Next TmpPlannedSpot
    '            XMLCamp.AppendChild(XMLSpots)

    '            'Save Actual spots
    '            XMLSpots = XMLDoc.CreateElement("ActualSpots")

    '            For Each TmpActualSpot In mvarActualSpots

    '                XMLSpot = XMLDoc.CreateElement("Spot")
    '                XMLSpot.SetAttribute("ID", TmpActualSpot.ID) 'as String
    '                XMLSpot.SetAttribute("AirDate", TmpActualSpot.AirDate) 'as Long
    '                XMLSpot.SetAttribute("MaM", TmpActualSpot.MaM) 'as Integer
    '                XMLSpot.SetAttribute("Channel", TmpActualSpot.Channel.ChannelName) 'as cChannel
    '                XMLSpot.SetAttribute("ProgBefore", TmpActualSpot.ProgBefore) 'as String
    '                XMLSpot.SetAttribute("ProgAfter", TmpActualSpot.ProgAfter) 'as String
    '                XMLSpot.SetAttribute("Programme", TmpActualSpot.Programme) 'as String
    '                XMLSpot.SetAttribute("Advertiser", TmpActualSpot.Advertiser) 'as String
    '                XMLSpot.SetAttribute("Product", TmpActualSpot.Product) 'as String
    '                XMLSpot.SetAttribute("Filmcode", TmpActualSpot.Filmcode) 'as String
    '                XMLSpot.SetAttribute("Index", TmpActualSpot.Index) 'as Integer
    '                XMLSpot.SetAttribute("PosInBreak", TmpActualSpot.PosInBreak) 'as Byte
    '                XMLSpot.SetAttribute("SpotsInBreak", TmpActualSpot.SpotsInBreak) 'as Byte
    '                If Not TmpActualSpot.MatchedSpot Is Nothing Then
    '                    XMLSpot.SetAttribute("MatchedSpot", TmpActualSpot.MatchedSpot.ID) 'as cPlannedSpot
    '                Else
    '                    XMLSpot.SetAttribute("MatchedSpot", "")
    '                End If
    '                XMLSpot.SetAttribute("SpotLength", TmpActualSpot.SpotLength) 'as Byte
    '                XMLSpot.SetAttribute("Deactivated", TmpActualSpot.Deactivated) 'as Boolean
    '                XMLSpot.SetAttribute("SpotType", TmpActualSpot.SpotType) 'as Byte
    '                XMLSpot.SetAttribute("BreakType", TmpActualSpot.BreakType) 'as EnumBreakType
    '                XMLSpot.SetAttribute("SecondRating", TmpActualSpot.SecondRating) 'as Single
    '                XMLSpot.SetAttribute("AdEdgeChannel", TmpActualSpot.AdedgeChannel) 'as String
    '                XMLSpot.SetAttribute("BookingType", TmpActualSpot.Bookingtype.Name) 'as cBookingType
    '                XMLSpot.SetAttribute("GroupIdx", TmpActualSpot.GroupIdx) 'as Integer
    '                XMLSpots.AppendChild(XMLSpot)
    '            Next TmpActualSpot
    '            XMLCamp.AppendChild(XMLSpots)

    '            Helper.WriteToLogFile("Create more XML")

    '            Dim XMLTmpList As Xml.XmlElement
    '            Dim XMLTmpList2 As Xml.XmlElement
    '            Dim XMLTmpElement As Xml.XmlElement

    '            XMLTmpNode = XMLDoc.CreateElement("ReachGoals")

    '            'For i = 1 To mvarRTColl.Count

    '            '    a = mvarRTColl.Keys
    '            '    XMLTmpNode2 = XMLDoc.createElement("ReachGoal")
    '            '    XMLTmpNode2.setAttribute("Name", a(i - 1))
    '            '    For j = 1 To mvarRTColl(a(i - 1)).Count
    '            '        b = mvarRTColl.Item(a(i - 1)).Keys
    '            '        Tmpstr = b(j - 1)
    '            '        XMLTmpElement = XMLDoc.createElement("Goal")
    '            '        XMLTmpElement.setAttribute("Freq", Tmpstr)
    '            '        XMLTmpElement.setAttribute("Reach", CByte(mvarRTColl(a(i - 1)).Item(b(j - 1))))
    '            '        XMLTmpNode2.appendChild(XMLTmpElement)
    '            '    Next
    '            '    XMLTmpNode.appendChild(XMLTmpNode2)
    '            'Next

    '            XMLCamp.AppendChild(XMLTmpNode)

    '            XMLTmpNode = XMLDoc.CreateElement("Activities")

    '            XMLCamp.AppendChild(XMLTmpNode)

    '            XMLSpots = XMLDoc.CreateElement("BookedSpots")

    '            For i = 1 To mvarBookedSpots.Count
    '                XMLSpot = XMLDoc.CreateElement("Spot")
    '                XMLSpot.SetAttribute("ID", mvarBookedSpots(i).ID)
    '                XMLSpot.SetAttribute("AirDate", mvarBookedSpots(i).AirDate)
    '                XMLSpot.SetAttribute("Bookingtype", mvarBookedSpots(i).Bookingtype.Name)
    '                XMLSpot.SetAttribute("Channel", mvarBookedSpots(i).Channel.ChannelName)
    '                XMLSpot.SetAttribute("ChannelEstimate", mvarBookedSpots(i).ChannelEstimate)
    '                XMLSpot.SetAttribute("DatabaseID", mvarBookedSpots(i).DatabaseID)
    '                XMLSpot.SetAttribute("Filmcode", mvarBookedSpots(i).Filmcode)
    '                XMLSpot.SetAttribute("GrossPrice", mvarBookedSpots(i).GrossPrice)
    '                XMLSpot.SetAttribute("MaM", mvarBookedSpots(i).MaM)
    '                XMLSpot.SetAttribute("MyEstimate", mvarBookedSpots(i).MyEstimate)
    '                XMLSpot.SetAttribute("MyEstimateBuyTarget", mvarBookedSpots(i).MyEstimateBuyTarget)
    '                XMLSpot.SetAttribute("NetPrice", mvarBookedSpots(i).NetPrice)
    '                XMLSpot.SetAttribute("ProgAfter", mvarBookedSpots(i).ProgAfter)
    '                XMLSpot.SetAttribute("ProgBefore", mvarBookedSpots(i).ProgBefore)
    '                XMLSpot.SetAttribute("Programme", mvarBookedSpots(i).Programme)
    '                XMLSpot.SetAttribute("IsLocal", mvarBookedSpots(i).IsLocal)
    '                XMLSpot.SetAttribute("IsRB", mvarBookedSpots(i).IsRB)
    '                XMLSpot.SetAttribute("Comments", mvarBookedSpots(i).Comments)
    '                XMLIndexes = XMLDoc.CreateElement("AddedValues")
    '                If Not mvarBookedSpots(i).AddedValues Is Nothing Then
    '                    For Each kv As KeyValuePair(Of String, Trinity.cAddedValue) In mvarBookedSpots(i).AddedValues
    '                        XMLIndex = XMLDoc.CreateElement("AddedValue")
    '                        XMLIndex.SetAttribute("ID", kv.Key)
    '                        XMLIndexes.AppendChild(XMLIndex)
    '                    Next
    '                End If
    '                XMLSpot.AppendChild(XMLIndexes)
    '                XMLSpots.AppendChild(XMLSpot)
    '                If Not DoNotSaveToFile Then
    '                    mvarBookedSpots(i).RecentlyBooked = False
    '                End If
    '            Next
    '            XMLCamp.AppendChild(XMLSpots)

    '            XMLTmpNode = XMLDoc.CreateElement("LabCampaigns")
    '            If Not SkipLab Then
    '                If Not Campaigns Is Nothing Then
    '                    For Each kv As KeyValuePair(Of String, cKampanj) In Campaigns
    '                        XMLTmpDoc.LoadXml(kv.Value.SaveCampaign("", True, True, True))
    '                        XMLTmpNode2 = XMLDoc.ImportNode(XMLTmpDoc.GetElementsByTagName("Campaign").Item(0), True)
    '                        XMLTmpNode2.SetAttribute("LabName", kv.Key)
    '                        XMLTmpNode.AppendChild(XMLTmpNode2)
    '                    Next
    '                End If
    '            End If
    '            XMLCamp.AppendChild(XMLTmpNode)


    '            XMLTmpNode = XMLDoc.CreateElement("History")
    '            If Not SkipHistory Then
    '                For Each kv As KeyValuePair(Of String, cKampanj) In History
    '                    XMLTmpDoc.LoadXml(kv.Value.SaveCampaign("", True, True, True))
    '                    XMLTmpNode2 = XMLDoc.ImportNode(XMLTmpDoc.GetElementsByTagName("Campaign").Item(0), True)
    '                    XMLTmpNode.AppendChild(XMLTmpNode2)
    '                Next
    '            End If
    '            XMLCamp.AppendChild(XMLTmpNode)

    '            On Error Resume Next

    '            Helper.WriteToLogFile("Save the file")

    '            If DoNotSaveToFile Then
    '                SaveCampaign = XMLDoc.OuterXml
    '            Else
    '                XMLDoc.Save(Path)
    '                Kill(Path & ".bak")
    '                If Path <> "" Then
    '                    mvarFilename = Path
    '                End If
    '                SaveCampaign = ""
    '            End If

    '            '    Put #99, , mvarReachTargets 'as New Dictionary
    '            Saving = False
    '            On Error GoTo 0
    '            Exit Function

    'SaveCampaign_Error:

    '            Saving = False
    '            Helper.WriteToLogFile("ERROR (" & Err.Number & "): " & Err.Description)
    '            Err.Raise(Err.Number, "cKampanj: SaveCampaign", Err.Description & Chr(10) & Chr(10) & "The original file was saved as " & mvarFilename & ".bak" & Chr(10) & Chr(10) & "DO NOT ATTEMPT TO SAVE AGAIN!!!")
    '            Resume Next

    '        End Function




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : LoadCampaign
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Function LoadCampaign(ByRef Path As String, Optional ByRef DoNotLoadFromFile As Boolean = False, Optional ByRef XML As String = "") As Byte

    '            Dim i As Short

    '            Dim IniFile As New clsIni

    '            Dim s As Short
    '            Dim Group As String
    '            Dim Freq As Short
    '            Dim Reach As Byte
    '            Dim ChangeDate As Date
    '            Dim Who As String
    '            Dim Activity As String
    '            Dim ChangeTime As String
    '            Dim a As Short
    '            Dim Start As Short

    '            Dim TmpString As String

    '            Dim TmpChannel As cChannel
    '            Dim TmpBookingType As cBookingType
    '            Dim TmpWeek As cWeek
    '            Dim TmpFilm As cFilm
    '            Dim TmpPLTarget As cPricelistTarget
    '            Dim TmpPlannedSpot As cPlannedSpot
    '            Dim TmpActualSpot As cActualSpot

    '            Dim TmpInt As Short
    '            Dim TmpLong As Integer
    '            Dim Tmpstr As String
    '            Dim CurrentlyReading As String

    '            On Error GoTo ErrHandle

    '            If Path = "" And Not DoNotLoadFromFile Then
    '                LoadCampaign = 1
    '                Exit Function
    '            End If

    '            If Loading Or Saving Then Exit Function

    '            Loading = True

    '            Dim XMLDoc As Xml.XmlDocument = New Xml.XmlDocument
    '            Dim XMLCamp As Xml.XmlElement
    '            Dim XMLTmpNode As Xml.XmlElement
    '            Dim XMLTmpNode2 As Xml.XmlElement
    '            Dim XMLContract As Xml.XmlElement
    '            Dim TmpNode As Xml.XmlElement

    '            On Error Resume Next
    '            If DoNotLoadFromFile Then
    '                XMLDoc.LoadXml(XML)
    '            Else
    '                XMLDoc.Load(Path)
    '            End If

    '            On Error GoTo ErrHandle

    '            IniFile.Create(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & mvarArea & "\area.ini")
    '            XMLCamp = XMLDoc.GetElementsByTagName("Campaign").Item(0)

    '            mvarVersion = XMLCamp.GetAttribute("Version")
    '            If mvarVersion > MY_VERSION Then
    '                MsgBox("This campaign is created with a later version of" & Chr(10) & "Trinity than the one you are currently using." & Chr(10) & "Contact the system administrator to get the latest version of the system.", MsgBoxStyle.Information, "TRINITY")
    '                LoadCampaign = 1
    '                Loading = False
    '                FileClose(99)
    '                Exit Function
    '            ElseIf mvarVersion < 40 Then
    '                LoadOldCampaign(Path)
    '                Exit Function
    '            End If

    '            '    mvarStartDate = XMLCamp.getAttribute("StartDate")
    '            '    mvarEndDate = XMLCamp.getAttribute("EndDate")
    '            mvarName = XMLCamp.GetAttribute("Name")
    '            ID = XMLCamp.GetAttribute("ID")
    '            mvarUpdatedTo = XMLCamp.GetAttribute("UpdatedTo")
    '            mvarPlanner = XMLCamp.GetAttribute("Planner")
    '            mvarBuyer = XMLCamp.GetAttribute("Buyer")
    '            mvarCancelled = XMLCamp.GetAttribute("Cancelled")
    '            mvarFrequencyFocus = XMLCamp.GetAttribute("FrequencyFocus")
    '            mvarFilename = XMLCamp.GetAttribute("Filename")
    '            ProductID = XMLCamp.GetAttribute("ProductID")
    '            ClientID = XMLCamp.GetAttribute("ClientID")
    '            mvarBudgetTotalCTC = XMLCamp.GetAttribute("BudgetTotalCTC")
    '            mvarActualTotCTC = XMLCamp.GetAttribute("ActualTotCTC")
    '            If Not XMLCamp.GetAttribute("MarathonCTC") Is Nothing Then mvarMarathonCTC = Val(XMLCamp.GetAttribute("MarathonCTC"))
    '            mvarCommentary = XMLCamp.GetAttribute("Commentary")
    '            Start = 1
    '            While InStr(Start, mvarCommentary, Chr(10)) > 0
    '                Start = InStr(Start, mvarCommentary, Chr(10))
    '                mvarCommentary = Left(mvarCommentary, Start - 1) & Chr(13) & Chr(10) & Mid(mvarCommentary, Start + 1)
    '                Start = Start + 2
    '                '        Mid(mvarCommentary, InStr(mvarCommentary, Chr(10))) = Chr(13)
    '            End While
    '            mvarControlFilmcodeFromClient = XMLCamp.GetAttribute("ControlFilmcodeFromClient")
    '            mvarControlFilmcodeToBureau = XMLCamp.GetAttribute("ControlFilmcodeToBureau")
    '            mvarControlFilmcodeToChannels = XMLCamp.GetAttribute("ControlFilmcodeToChannels")
    '            mvarControlOKFromClient = XMLCamp.GetAttribute("ControlOKFromClient")
    '            mvarControlTracking = XMLCamp.GetAttribute("ControlTracking")
    '            mvarControlFollowedUp = XMLCamp.GetAttribute("ControlFollowedUp")
    '            mvarControlFollowUpToClient = XMLCamp.GetAttribute("ControlFollowUpToClient")
    '            mvarControlTransferredToMatrix = XMLCamp.GetAttribute("ControlTransferredToMatrix")
    '            mvarArea = XMLCamp.GetAttribute("Area")
    '            mvarAreaLog = XMLCamp.GetAttribute("AreaLog")
    '            mvarMarathonOtherOrder = XMLCamp.GetAttribute("MarathonOtherOrder")
    '            MarathonPlanNr = XMLCamp.GetAttribute("MarathonPlan")

    '            mvarHistoryDate = XMLCamp.GetAttribute("HistoryDate")
    '            HistoryComment = XMLCamp.GetAttribute("HistoryComment")

    '            XMLTmpNode = XMLCamp.GetElementsByTagName("Dayparts").Item(0).FirstChild
    '            i = 0
    '            While Not XMLTmpNode Is Nothing

    '                mvarDaypartName(i) = XMLTmpNode.LocalName
    '                mvarDaypartStart(i) = XMLTmpNode.GetAttribute("Start")
    '                mvarDaypartEnd(i) = XMLTmpNode.GetAttribute("End")
    '                XMLTmpNode = XMLTmpNode.NextSibling
    '                i = i + 1
    '            End While

    '            If DaypartCount = 0 Then
    '                Area = mvarArea
    '            End If

    '            If Not XMLCamp.GetElementsByTagName("Reach") Is Nothing AndAlso XMLCamp.GetElementsByTagName("Reach").Count > 0 Then
    '                XMLTmpNode = XMLCamp.GetElementsByTagName("Reach").Item(0).FirstChild
    '                While Not XMLTmpNode Is Nothing
    '                    mvarReachGoal(XMLTmpNode.GetAttribute("Freq")) = XMLTmpNode.GetAttribute("Reach")
    '                    XMLTmpNode = XMLTmpNode.NextSibling
    '                End While
    '            End If

    '            mvarAllAdults = XMLCamp.GetAttribute("AllAdults")
    '            If mvarAllAdults = "" Then
    '                Ini.Create(TrinitySettings.LocalDataPath & "\Data\" & mvarArea & "\Area.ini")
    '                mvarAllAdults = Ini.Text("General", "EntirePopulation")
    '            End If

    '            'Load costs

    '            TmpNode = XMLCamp.GetElementsByTagName("Costs").Item(0).ChildNodes.Item(0)
    '            While Not TmpNode Is Nothing
    '                mvarCosts.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("CostType"), Conv(TmpNode.GetAttribute("Amount")), TmpNode.GetAttribute("CostOn"), TmpNode.GetAttribute("MarathonID"))
    '                TmpNode = TmpNode.NextSibling
    '            End While

    '            XMLContract = XMLCamp.GetElementsByTagName("Contract").Item(0)
    '            If Not XMLContract.ChildNodes.Item(0) Is Nothing Then
    '                mvarContract = New cContract(Me)
    '                mvarContract.Load("", True, XMLContract.OuterXml)
    '            End If

    '            'Load channels and all sub-collections and objects

    '            Dim XMLChannel As Xml.XmlElement
    '            Dim XMLBookingType As Xml.XmlElement
    '            Dim XMLWeek As Xml.XmlElement
    '            Dim XMLFilm As Xml.XmlElement
    '            Dim XMLTarget As Xml.XmlElement
    '            Dim XMLBuyTarget As Xml.XmlElement
    '            Dim XMLPricelist As Xml.XmlElement
    '            Dim XMLSpot As Xml.XmlElement

    '            XMLTarget = XMLCamp.SelectSingleNode("MainTarget")
    '            mvarMainTarget.TargetName = XMLTarget.GetAttribute("Name")
    '            mvarMainTarget.TargetType = XMLTarget.GetAttribute("Type")
    '            mvarMainTarget.Universe = XMLTarget.GetAttribute("Universe")
    '            mvarMainTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
    '            If mvarMainTarget.SecondUniverse = "" Then
    '                mvarMainTarget.SecondUniverse = IniFile.Text("Universe", "Second")
    '            End If

    '            XMLTarget = XMLCamp.SelectSingleNode("SecondaryTarget")
    '            mvarSecondaryTarget.TargetName = XMLTarget.GetAttribute("Name")
    '            mvarSecondaryTarget.TargetType = XMLTarget.GetAttribute("Type")
    '            mvarSecondaryTarget.Universe = XMLTarget.GetAttribute("Universe")
    '            mvarSecondaryTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
    '            If mvarSecondaryTarget.SecondUniverse = "" Then
    '                mvarSecondaryTarget.SecondUniverse = IniFile.Text("Universe", "Second")
    '            End If

    '            XMLTarget = XMLCamp.SelectSingleNode("ThirdTarget")
    '            mvarThirdTarget.TargetName = XMLTarget.GetAttribute("Name")
    '            mvarThirdTarget.TargetType = Val(XMLTarget.GetAttribute("Type"))
    '            mvarThirdTarget.Universe = XMLTarget.GetAttribute("Universe")
    '            mvarThirdTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
    '            If mvarThirdTarget.SecondUniverse = "" Then
    '                mvarThirdTarget.SecondUniverse = IniFile.Text("Universe", "Second")
    '            End If

    '            mvarChannels = New cChannels(Me)
    '            mvarChannels.MainObject = Me

    '            XMLChannel = XMLCamp.SelectSingleNode("Channels").FirstChild
    '            While Not XMLChannel Is Nothing
    '                TmpString = XMLChannel.GetAttribute("Name")
    '                TmpChannel = mvarChannels.Add(TmpString)
    '                TmpChannel.ID = XMLChannel.GetAttribute("ID")
    '                TmpChannel.Shortname = XMLChannel.GetAttribute("ShortName")
    '                TmpChannel.BuyingUniverse = XMLChannel.GetAttribute("BuyingUniverse")
    '                TmpChannel.AdEdgeNames = XMLChannel.GetAttribute("AdEdgeNames")
    '                TmpChannel.DefaultArea = XMLChannel.GetAttribute("DefaultArea")
    '                TmpChannel.AgencyCommission = Conv(XMLChannel.GetAttribute("AgencyCommission"))
    '                If Not IsDBNull(XMLChannel.GetAttribute("DeliveryAddress")) Then
    '                    TmpChannel.DeliveryAddress = XMLChannel.GetAttribute("DeliveryAddress")
    '                End If
    '                TmpChannel.MarathonName = XMLChannel.GetAttribute("Marathon")

    '                'Save the targets

    '                XMLTarget = XMLChannel.GetElementsByTagName("MainTarget").Item(0)
    '                TmpChannel.MainTarget.NoUniverseSize = True
    '                TmpChannel.MainTarget.TargetName = XMLTarget.GetAttribute("Name")
    '                TmpChannel.MainTarget.TargetType = Val(XMLTarget.GetAttribute("Type"))
    '                TmpChannel.MainTarget.Universe = XMLTarget.GetAttribute("Universe")
    '                TmpChannel.MainTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
    '                If TmpChannel.MainTarget.SecondUniverse = "" Then
    '                    TmpChannel.MainTarget.SecondUniverse = IniFile.Text("Universe", "Second")
    '                End If
    '                TmpChannel.MainTarget.NoUniverseSize = False

    '                XMLTarget = XMLChannel.GetElementsByTagName("SecondaryTarget").Item(0)
    '                TmpChannel.SecondaryTarget.NoUniverseSize = True
    '                TmpChannel.SecondaryTarget.TargetName = XMLTarget.GetAttribute("Name")
    '                TmpChannel.SecondaryTarget.TargetType = XMLTarget.GetAttribute("Type")
    '                TmpChannel.SecondaryTarget.Universe = XMLTarget.GetAttribute("Universe")
    '                TmpChannel.SecondaryTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
    '                If TmpChannel.SecondaryTarget.SecondUniverse = "" Then
    '                    TmpChannel.SecondaryTarget.SecondUniverse = IniFile.Text("Universe", "Second")
    '                End If
    '                TmpChannel.SecondaryTarget.NoUniverseSize = False
    '                If Not IsDBNull(XMLChannel.GetAttribute("Marathon")) Then
    '                    TmpChannel.MarathonName = XMLChannel.GetAttribute("Marathon")
    '                End If
    '                If Not IsDBNull(XMLChannel.GetAttribute("Penalty")) AndAlso XMLChannel.GetAttribute("Penalty") <> "" Then
    '                    TmpChannel.Penalty = XMLChannel.GetAttribute("Penalty")
    '                    TmpChannel.ConnectedChannel = XMLChannel.GetAttribute("ConnectedChannel")
    '                Else
    '                    TmpChannel.ConnectedChannel = ""
    '                    TmpChannel.Penalty = 0
    '                End If

    '                'Read Booking types

    '                XMLBookingType = XMLChannel.GetElementsByTagName("BookingTypes").Item(0).FirstChild
    '                While Not XMLBookingType Is Nothing

    '                    TmpString = XMLBookingType.GetAttribute("Name")
    '                    TmpBookingType = TmpChannel.BookingTypes.Add(TmpString)
    '                    TmpBookingType.Shortname = XMLBookingType.GetAttribute("Shortname")

    '                    'Save the rest of the booking type

    '                    TmpBookingType.IndexMainTarget = Conv(XMLBookingType.GetAttribute("IndexMainTarget"))
    '                    TmpBookingType.IndexAllAdults = Conv(XMLBookingType.GetAttribute("IndexAllAdults"))
    '                    XMLTmpNode = XMLBookingType.GetElementsByTagName("DaypartSplit").Item(0)
    '                    For i = 0 To DaypartCount - 1
    '                        XMLTmpNode2 = XMLTmpNode.GetElementsByTagName(mvarDaypartName(i)).Item(0)
    '                        TmpBookingType.DaypartSplit(i) = Conv(XMLTmpNode2.GetAttribute("Share"))
    '                    Next
    '                    TmpBookingType.BookIt = XMLBookingType.GetAttribute("BookIt")
    '                    TmpBookingType.GrossCPP = Conv(XMLBookingType.GetAttribute("GrossCPP"))
    '                    TmpBookingType.AverageRating = Conv(XMLBookingType.GetAttribute("AverageRating"))
    '                    TmpBookingType.ConfirmedNetBudget = Conv(XMLBookingType.GetAttribute("ConfirmedNetBudget"))
    '                    TmpBookingType.Bookingtype = XMLBookingType.GetAttribute("Bookingtype")
    '                    TmpBookingType.ContractNumber = XMLBookingType.GetAttribute("ContractNumber")
    '                    TmpBookingType.IsRBS = XMLBookingType.GetAttribute("IsRBS")
    '                    TmpBookingType.IsSpecific = XMLBookingType.GetAttribute("IsSpecific")
    '                    '            TmpBookingtype.Discount = XMLBookingType.getAttribute("Discount")
    '                    '            TmpBookingtype.NetCPT = Conv(XMLBookingType.getAttribute("NetCPT"))
    '                    '            TmpBookingtype.NetCPP = Conv(XMLBookingType.getAttribute("NetCPP"))
    '                    '            If Not IsNull(XMLBookingType.getAttribute("IsEntered")) Then
    '                    '                TmpBookingtype.IsEntered = XMLBookingType.getAttribute("IsEntered")
    '                    '            End If

    '                    If Not IsDBNull(XMLBookingType.GetAttribute("OrderNumber")) Then
    '                        TmpBookingType.OrderNumber = XMLBookingType.GetAttribute("OrderNumber")
    '                    Else
    '                        TmpBookingType.OrderNumber = ""
    '                    End If
    '                    'Read the pricelist

    '                    'Read Buyingtarget

    '                    XMLBuyTarget = XMLBookingType.SelectSingleNode("BuyingTarget")

    '                    TmpBookingType.BuyingTarget.CalcCPP = XMLBuyTarget.GetAttribute("CalcCPP")
    '                    TmpBookingType.BuyingTarget.CPP = Conv(XMLBuyTarget.GetAttribute("CPP"))
    '                    TmpBookingType.BuyingTarget.TargetName = XMLBuyTarget.GetAttribute("TargetName")
    '                    TmpBookingType.BuyingTarget.UniSize = XMLBuyTarget.GetAttribute("UniSize")
    '                    TmpBookingType.BuyingTarget.UniSizeNat = XMLBuyTarget.GetAttribute("UniSizeNat")
    '                    If TmpChannel.BuyingUniverse = "" Then
    '                        TmpBookingType.BuyingTarget.UniSize = TmpBookingType.BuyingTarget.UniSizeNat
    '                    End If
    '                    XMLTmpNode = XMLBuyTarget.GetElementsByTagName("DaypartCPP").Item(0)
    '                    For i = 0 To DaypartCount - 1
    '                        If IsDBNull(XMLTmpNode.GetAttribute(mvarDaypartName(i))) Then
    '                            XMLTmpNode.SetAttribute(mvarDaypartName(i), 0)
    '                        End If
    '                        TmpBookingType.BuyingTarget.CPPDaypart(i) = Conv(XMLTmpNode.GetAttribute(mvarDaypartName(i)))
    '                    Next

    '                    TmpBookingType.BuyingTarget.IsEntered = XMLBuyTarget.GetAttribute("IsEntered")
    '                    If TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
    '                        TmpBookingType.BuyingTarget.NetCPP = Conv(XMLBuyTarget.GetAttribute("Amount"))
    '                    ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPT Then
    '                        TmpBookingType.BuyingTarget.NetCPT = Conv(XMLBuyTarget.GetAttribute("Amount"))
    '                    ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eDiscount Then
    '                        TmpBookingType.BuyingTarget.Discount = Conv(XMLBuyTarget.GetAttribute("Amount"))
    '                    End If

    '                    XMLTarget = XMLBuyTarget.GetElementsByTagName("Target").Item(0)
    '                    TmpBookingType.BuyingTarget.Target.NoUniverseSize = True
    '                    TmpBookingType.BuyingTarget.Target.TargetName = XMLTarget.GetAttribute("Name")
    '                    TmpBookingType.BuyingTarget.Target.TargetType = XMLTarget.GetAttribute("Type")
    '                    TmpBookingType.BuyingTarget.Target.Universe = XMLTarget.GetAttribute("Universe")
    '                    TmpBookingType.BuyingTarget.Target.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
    '                    If TmpBookingType.BuyingTarget.Target.SecondUniverse = "" Then
    '                        TmpBookingType.BuyingTarget.Target.SecondUniverse = IniFile.Text("Universe", "Second")
    '                    End If
    '                    TmpBookingType.BuyingTarget.Target.NoUniverseSize = False
    '                    TmpNode = XMLBuyTarget.GetElementsByTagName("Indexes").Item(0).FirstChild
    '                    While Not TmpNode Is Nothing
    '                        TmpBookingType.BuyingTarget.Indexes.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("ID"))
    '                        TmpBookingType.BuyingTarget.Indexes(TmpNode.GetAttribute("ID")).FromDate = TmpNode.GetAttribute("FromDate")
    '                        TmpBookingType.BuyingTarget.Indexes(TmpNode.GetAttribute("ID")).ToDate = TmpNode.GetAttribute("ToDate")
    '                        If TmpNode.GetAttribute("IndexDP0") = "" Then
    '                            TmpBookingType.BuyingTarget.Indexes(TmpNode.GetAttribute("ID")).Index = TmpNode.GetAttribute("Value")
    '                        Else
    '                            For i = 0 To DaypartCount - 1
    '                                TmpBookingType.BuyingTarget.Indexes(TmpNode.GetAttribute("ID")).Index(i) = TmpNode.GetAttribute("IndexDP" & i)
    '                            Next
    '                        End If
    '                        TmpBookingType.BuyingTarget.Indexes(TmpNode.GetAttribute("ID")).IndexOn = TmpNode.GetAttribute("IndexOn")
    '                        TmpBookingType.BuyingTarget.Indexes(TmpNode.GetAttribute("ID")).SystemGenerated = TmpNode.GetAttribute("SystemGenerated")
    '                        TmpNode = TmpNode.NextSibling
    '                    End While

    '                    XMLPricelist = XMLBookingType.GetElementsByTagName("Pricelist").Item(0)

    '                    TmpBookingType.Pricelist.StartDate = XMLPricelist.GetAttribute("StartDate")
    '                    TmpBookingType.Pricelist.EndDate = XMLPricelist.GetAttribute("EndDate")
    '                    TmpBookingType.Pricelist.BuyingUniverse = XMLPricelist.GetAttribute("BuyingUniverse")

    '                    XMLBuyTarget = XMLPricelist.GetElementsByTagName("Targets").Item(0).FirstChild

    '                    While Not XMLBuyTarget Is Nothing

    '                        TmpString = XMLBuyTarget.GetAttribute("Name")
    '                        TmpPLTarget = TmpBookingType.Pricelist.Targets.Add(TmpString)
    '                        TmpPLTarget.CalcCPP = XMLBuyTarget.GetAttribute("CalcCPP")
    '                        TmpPLTarget.CPP = Conv(XMLBuyTarget.GetAttribute("CPP"))

    '                        XMLTmpNode = XMLBuyTarget.GetElementsByTagName("DaypartCPP").Item(0)
    '                        For i = 0 To DaypartCount - 1
    '                            XMLTmpNode2 = XMLTmpNode.GetElementsByTagName(mvarDaypartName(i)).Item(0)
    '                            TmpPLTarget.CPPDaypart(i) = Conv(XMLTmpNode2.GetAttribute("CPP"))
    '                        Next

    '                        XMLTmpNode = XMLBuyTarget.GetElementsByTagName("DaypartSplit").Item(0)
    '                        For i = 0 To DaypartCount - 1
    '                            XMLTmpNode2 = XMLTmpNode.GetElementsByTagName(mvarDaypartName(i)).Item(0)
    '                            TmpPLTarget.DefaultDaypart(i) = Conv(XMLTmpNode2.GetAttribute("DefaultSplit"))
    '                        Next

    '                        TmpPLTarget.UniSize = Conv(XMLBuyTarget.GetAttribute("UniSize"))
    '                        TmpPLTarget.UniSizeNat = Conv(XMLBuyTarget.GetAttribute("UniSizeNat"))

    '                        XMLTarget = XMLBuyTarget.GetElementsByTagName("Target").Item(0)
    '                        TmpPLTarget.Target.NoUniverseSize = True 'For speed. No unisizes are calculated
    '                        TmpPLTarget.Target.TargetName = XMLTarget.GetAttribute("Name")
    '                        TmpPLTarget.Target.TargetType = XMLTarget.GetAttribute("Type")
    '                        TmpPLTarget.Target.Universe = XMLTarget.GetAttribute("Universe")
    '                        TmpPLTarget.Target.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
    '                        If TmpPLTarget.Target.SecondUniverse = "" Then
    '                            TmpPLTarget.Target.SecondUniverse = IniFile.Text("Universe", "Second")
    '                        End If
    '                        TmpPLTarget.Target.NoUniverseSize = False
    '                        TmpPLTarget.StandardTarget = XMLTarget.GetAttribute("StandardTarget")
    '                        If Not IsDBNull(XMLBookingType.GetAttribute("IsEntered")) Then
    '                            TmpPLTarget.IsEntered = XMLBuyTarget.GetAttribute("IsEntered")
    '                            If TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
    '                                TmpPLTarget.NetCPP = Conv(XMLBuyTarget.GetAttribute("NetCPP"))
    '                            ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPT Then
    '                                TmpPLTarget.NetCPT = Conv(XMLBuyTarget.GetAttribute("NetCPT"))
    '                            ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eDiscount Then
    '                                TmpPLTarget.Discount = Conv(XMLBuyTarget.GetAttribute("Discount"))
    '                            End If
    '                        Else
    '                            TmpPLTarget.Discount = Conv(XMLBuyTarget.GetAttribute("Discount"))
    '                            TmpPLTarget.NetCPT = Conv(XMLBuyTarget.GetAttribute("NetCPT"))
    '                            TmpPLTarget.NetCPP = Conv(XMLBuyTarget.GetAttribute("NetCPP"))
    '                        End If

    '                        XMLBuyTarget = XMLBuyTarget.NextSibling
    '                    End While

    '                    'Load Indexes, AddedValues, Compensations and SpotIndexes

    '                    TmpNode = XMLBookingType.GetElementsByTagName("Indexes").Item(0).FirstChild
    '                    While Not TmpNode Is Nothing
    '                        TmpBookingType.Indexes.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("ID"))
    '                        TmpBookingType.Indexes(TmpNode.GetAttribute("ID")).FromDate = TmpNode.GetAttribute("FromDate")
    '                        TmpBookingType.Indexes(TmpNode.GetAttribute("ID")).ToDate = TmpNode.GetAttribute("ToDate")
    '                        If TmpNode.GetAttribute("IndexDP0") = "" Then
    '                            TmpBookingType.Indexes(TmpNode.GetAttribute("ID")).Index = TmpNode.GetAttribute("Value")
    '                        Else
    '                            For i = 0 To DaypartCount - 1
    '                                TmpBookingType.Indexes(TmpNode.GetAttribute("ID")).Index(i) = TmpNode.GetAttribute("IndexDP" & i)
    '                            Next
    '                        End If
    '                        TmpBookingType.Indexes(TmpNode.GetAttribute("ID")).IndexOn = TmpNode.GetAttribute("IndexOn")
    '                        TmpBookingType.Indexes(TmpNode.GetAttribute("ID")).SystemGenerated = TmpNode.GetAttribute("SystemGenerated")
    '                        TmpNode = TmpNode.NextSibling
    '                    End While
    '                    If Not XMLBookingType.GetElementsByTagName("Compensations").Item(0) Is Nothing Then
    '                        TmpNode = XMLBookingType.GetElementsByTagName("Compensations").Item(0).FirstChild
    '                    Else
    '                        TmpNode = Nothing
    '                    End If
    '                    While Not TmpNode Is Nothing
    '                        Dim TmpComp As Trinity.cCompensation = TmpBookingType.Compensations.Add
    '                        TmpComp.FromDate = TmpNode.GetAttribute("From")
    '                        TmpComp.ToDate = TmpNode.GetAttribute("To")
    '                        TmpComp.TRPs = TmpNode.GetAttribute("TRPs")
    '                        TmpComp.Comment = TmpNode.GetAttribute("Comment")
    '                        TmpNode = TmpNode.NextSibling
    '                    End While
    '                    TmpNode = XMLBookingType.GetElementsByTagName("AddedValues").Item(0).FirstChild
    '                    While Not TmpNode Is Nothing
    '                        TmpBookingType.AddedValues.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("ID"))
    '                        TmpBookingType.AddedValues(TmpNode.GetAttribute("ID")).IndexGross = TmpNode.GetAttribute("IndexGross")
    '                        TmpBookingType.AddedValues(TmpNode.GetAttribute("ID")).IndexNet = TmpNode.GetAttribute("IndexNet")
    '                        TmpNode = TmpNode.NextSibling
    '                    End While
    '                    TmpNode = XMLBookingType.GetElementsByTagName("SpotIndex").Item(0).FirstChild
    '                    While Not TmpNode Is Nothing
    '                        TmpBookingType.FilmIndex(TmpNode.GetAttribute("Length")) = TmpNode.GetAttribute("Idx")
    '                        TmpNode = TmpNode.NextSibling
    '                    End While

    '                    'Load weeks
    '                    Dim w As Integer = 0
    '                    XMLWeek = XMLBookingType.GetElementsByTagName("Weeks").Item(0).FirstChild

    '                    While Not XMLWeek Is Nothing
    '                        w += 1
    '                        TmpString = XMLWeek.GetAttribute("Name")
    '                        TmpWeek = TmpBookingType.Weeks.Add(TmpString)
    '                        TmpWeek.TRPControl = XMLWeek.GetAttribute("TRPControl")
    '                        If TmpWeek.TRPControl Then
    '                            TmpWeek.TRP = Conv(XMLWeek.GetAttribute("TRP"))
    '                        Else
    '                            TmpWeek.NetBudget = Conv(XMLWeek.GetAttribute("NetBudget"))
    '                        End If
    '                        TmpWeek.StartDate = XMLWeek.GetAttribute("StartDate")
    '                        TmpWeek.EndDate = XMLWeek.GetAttribute("EndDate")

    '                        'Added values

    '                        If Not XMLWeek.GetElementsByTagName("AddedValues").Item(0) Is Nothing Then
    '                            Dim XMLIndex As XmlElement = XMLWeek.GetElementsByTagName("AddedValues").Item(0).FirstChild

    '                            While Not XMLIndex Is Nothing
    '                                TmpBookingType.AddedValues(XMLIndex.GetAttribute("ID")).Amount(w) = XMLIndex.GetAttribute("Amount")
    '                                XMLIndex = XMLIndex.NextSibling
    '                            End While
    '                        End If

    '                        'Save Films

    '                        XMLFilm = XMLWeek.GetElementsByTagName("Films").Item(0).FirstChild

    '                        While Not XMLFilm Is Nothing

    '                            TmpString = XMLFilm.GetAttribute("Name")
    '                            TmpFilm = TmpWeek.Films.Add(TmpString)
    '                            TmpFilm.Filmcode = XMLFilm.GetAttribute("Filmcode")
    '                            TmpFilm.FilmLength = XMLFilm.GetAttribute("FilmLength")
    '                            '                    If Not IsNull(XMLFilm.getAttribute("AltFilmcode")) Then
    '                            '                        TmpFilm.AltFilmcode = XMLFilm.getAttribute("AltFilmcode")
    '                            '                    End If
    '                            TmpFilm.Index = Conv(XMLFilm.GetAttribute("Index"))
    '                            TmpFilm.Share = Conv(XMLFilm.GetAttribute("Share"))
    '                            TmpFilm.Description = XMLFilm.GetAttribute("Description")
    '                            XMLFilm = XMLFilm.NextSibling

    '                        End While
    '                        XMLWeek = XMLWeek.NextSibling
    '                    End While
    '                    XMLBookingType = XMLBookingType.NextSibling
    '                End While
    '                XMLChannel = XMLChannel.NextSibling
    '            End While

    '            'Save Planned spots

    '            CurrentlyReading = "Planned spots"
    '            XMLSpot = XMLCamp.GetElementsByTagName("PlannedSpots").Item(0).FirstChild
    '            mvarPlannedSpots = New cPlannedSpots

    '            While Not XMLSpot Is Nothing
    'NextPlannedSpot:
    '                TmpString = XMLSpot.GetAttribute("ID")
    '                TmpPlannedSpot = mvarPlannedSpots.Add(TmpString)
    '                TmpString = XMLSpot.GetAttribute("Channel")
    '                TmpPlannedSpot.Channel = mvarChannels(TmpString)
    '                TmpPlannedSpot.ChannelID = XMLSpot.GetAttribute("ChannelID")
    '                TmpString = XMLSpot.GetAttribute("BookingType")
    '                TmpPlannedSpot.Bookingtype = TmpPlannedSpot.Channel.BookingTypes(TmpString) ' ,"cBookingType
    '                TmpString = XMLSpot.GetAttribute("Week")
    '                If TmpString <> "" Then
    '                    TmpPlannedSpot.Week = TmpPlannedSpot.Bookingtype.Weeks(TmpString) ' ,"cWeek
    '                End If
    '                TmpLong = XMLSpot.GetAttribute("AirDate")
    '                TmpPlannedSpot.AirDate = TmpLong
    '                TmpPlannedSpot.MaM = XMLSpot.GetAttribute("MaM")
    '                TmpPlannedSpot.ProgBefore = XMLSpot.GetAttribute("ProgBefore")
    '                TmpPlannedSpot.ProgAfter = XMLSpot.GetAttribute("ProgAfter")
    '                TmpPlannedSpot.Programme = XMLSpot.GetAttribute("Programme")
    '                TmpPlannedSpot.Advertiser = XMLSpot.GetAttribute("Advertiser")
    '                TmpPlannedSpot.Product = XMLSpot.GetAttribute("Product")
    '                TmpPlannedSpot.Filmcode = XMLSpot.GetAttribute("Filmcode")
    '                On Error Resume Next
    '                'For Each TmpFilm In TmpPlannedSpot.Week.Films
    '                '    If TmpFilm.Filmcode = TmpPlannedSpot.Filmcode Then
    '                '        TmpPlannedSpot.Film = TmpPlannedSpot.Week.Films(TmpFilm.Filmcode)
    '                '    End If
    '                'Next TmpFilm
    '                On Error GoTo ErrHandle
    '                TmpPlannedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = Conv(XMLSpot.GetAttribute("RatingBuyTarget"))
    '                'TmpPlannedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteMainTarget) = Conv(XMLSpot.getAttribute("ChannelRating(cPlannedSpot.PlannedTargetEnum.pteMainTarget)"))
    '                TmpPlannedSpot.Estimation = XMLSpot.GetAttribute("Estimation")
    '                TmpPlannedSpot.MyRating(cPlannedSpot.PlannedTargetEnum.pteMainTarget) = Conv(XMLSpot.GetAttribute("MyRating"))
    '                'TmpPlannedSpot.MyRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = Conv(XMLSpot.getAttribute("MyRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)"))
    '                TmpPlannedSpot.Index = Conv(XMLSpot.GetAttribute("Index"))
    '                TmpPlannedSpot.SpotLength = XMLSpot.GetAttribute("SpotLength")
    '                TmpPlannedSpot.SpotType = XMLSpot.GetAttribute("SpotType")
    '                TmpPlannedSpot.PriceNet = (XMLSpot.GetAttribute("PriceNet"))
    '                TmpPlannedSpot.PriceGross = (XMLSpot.GetAttribute("PriceGross"))
    '                If Not IsDBNull(XMLSpot.GetAttribute("Remark")) Then
    '                    TmpPlannedSpot.Remark = XMLSpot.GetAttribute("Remark")
    '                End If
    '                If Not IsDBNull(XMLSpot.GetAttribute("Placement")) Then
    '                    TmpPlannedSpot.Placement = XMLSpot.GetAttribute("Placement")
    '                End If
    '                XMLSpot = XMLSpot.NextSibling
    '            End While

    'ActualSpots:
    '            'Save Actual spots
    '            CurrentlyReading = "Actual spots"
    '            XMLSpot = XMLCamp.GetElementsByTagName("ActualSpots").Item(0).FirstChild
    '            mvarActualSpots = New Trinity.cActualSpots(Me)
    '            While Not XMLSpot Is Nothing

    '                TmpLong = XMLSpot.GetAttribute("AirDate")
    '                TmpInt = XMLSpot.GetAttribute("MaM")
    '                TmpActualSpot = mvarActualSpots.Add(System.DateTime.FromOADate(TmpLong), TmpInt)
    '                Tmpstr = XMLSpot.GetAttribute("Channel")
    '                TmpActualSpot.Channel = mvarChannels(Tmpstr)
    '                TmpActualSpot.ProgBefore = XMLSpot.GetAttribute("ProgBefore")
    '                TmpActualSpot.ProgAfter = XMLSpot.GetAttribute("ProgAfter")
    '                TmpActualSpot.Programme = XMLSpot.GetAttribute("Programme")
    '                TmpActualSpot.Advertiser = XMLSpot.GetAttribute("Advertiser")
    '                TmpActualSpot.Product = XMLSpot.GetAttribute("Product")
    '                TmpActualSpot.Filmcode = XMLSpot.GetAttribute("Filmcode")
    '                'TmpActualSpot.Rating = Conv(XMLSpot.getAttribute("Rating"))
    '                TmpActualSpot.Index = Conv(XMLSpot.GetAttribute("Index"))
    '                TmpActualSpot.PosInBreak = XMLSpot.GetAttribute("PosInBreak")
    '                TmpActualSpot.SpotsInBreak = XMLSpot.GetAttribute("SpotsInBreak")
    '                Tmpstr = XMLSpot.GetAttribute("MatchedSpot")
    '                If Tmpstr <> "" Then
    '                    TmpActualSpot.MatchedSpot = mvarPlannedSpots(Tmpstr)
    '                    If Not mvarPlannedSpots(Tmpstr) Is Nothing Then
    '                        mvarPlannedSpots(Tmpstr).MatchedSpot = TmpActualSpot
    '                    End If
    '                    On Error GoTo ErrHandle
    '                End If
    '                TmpActualSpot.SpotLength = XMLSpot.GetAttribute("SpotLength")
    '                TmpActualSpot.Deactivated = XMLSpot.GetAttribute("Deactivated")
    '                TmpActualSpot.SpotType = XMLSpot.GetAttribute("SpotType")
    '                TmpActualSpot.BreakType = XMLSpot.GetAttribute("BreakType")
    '                TmpActualSpot.SecondRating = Conv(XMLSpot.GetAttribute("SecondRating"))
    '                TmpActualSpot.AdedgeChannel = XMLSpot.GetAttribute("AdEdgeChannel")
    '                TmpActualSpot.ID = XMLSpot.GetAttribute("ID")
    '                Tmpstr = XMLSpot.GetAttribute("BookingType")
    '                If Tmpstr <> "" Then
    '                    TmpActualSpot.Bookingtype = TmpActualSpot.Channel.BookingTypes(Tmpstr)
    '                Else
    '                    TmpActualSpot.Bookingtype = TmpActualSpot.Channel.BookingTypes(1)
    '                End If
    '                TmpActualSpot.week = TmpActualSpot.Bookingtype.GetWeek(Date.FromOADate(TmpActualSpot.AirDate))
    '                XMLSpot = XMLSpot.NextSibling

    '            End While

    '            CurrentlyReading = ""
    '            XMLTmpNode = XMLCamp.GetElementsByTagName("ReachGoals").Item(0).FirstChild
    '            While Not XMLTmpNode Is Nothing

    '                Group = XMLTmpNode.GetAttribute("Name")
    '                XMLTmpNode2 = XMLTmpNode.FirstChild

    '                'While Not XMLTmpNode2 Is Nothing

    '                '    Freq = XMLTmpNode2.GetAttribute("Freq")
    '                '    Reach = Conv(XMLTmpNode2.GetAttribute("Reach"))
    '                '    ReachTargets(Freq, Group) = Reach
    '                '    XMLTmpNode2 = XMLTmpNode2.NextSibling

    '                'End While
    '                XMLTmpNode = XMLTmpNode.NextSibling

    '            End While


    '            Dim TmpID As String
    '            Dim TmpAdjustBy As Byte
    '            Dim TmpDate As Date
    '            Dim TmpBT As String
    '            Dim TmpChannelEstimate As Single
    '            Dim TmpDBID As String
    '            Dim TmpFilmcode As String
    '            Dim TmpGrossPrice As Decimal
    '            Dim TmpMaM As Short
    '            Dim tmpMyEstimate As Single
    '            Dim TmpMyEstChanTarget As Single
    '            Dim TmpNetPrice As Decimal
    '            Dim TmpPlacement As cBookedSpot.PlaceEnum
    '            Dim TmpProgAfter As String
    '            Dim TmpProgBefore As String
    '            Dim TmpProg As String
    '            Dim TmpIsLocal As Boolean
    '            Dim TmpIsRB As Boolean

    '            mvarBookedSpots = New cBookedSpots(Me)
    '            mvarBookedSpots.MainObject = Me

    '            XMLSpot = XMLCamp.GetElementsByTagName("BookedSpots").Item(0).FirstChild

    '            While Not XMLSpot Is Nothing
    '                TmpID = XMLSpot.GetAttribute("ID")
    '                TmpDate = XMLSpot.GetAttribute("AirDate")
    '                TmpString = XMLSpot.GetAttribute("Channel") ' Channel
    '                TmpBookingType = mvarChannels(TmpString).BookingTypes(XMLSpot.GetAttribute("Bookingtype"))
    '                TmpChannelEstimate = CSng(Conv(XMLSpot.GetAttribute("ChannelEstimate")))
    '                TmpDBID = XMLSpot.GetAttribute("DatabaseID")
    '                TmpFilmcode = XMLSpot.GetAttribute("Filmcode")
    '                TmpGrossPrice = Conv(XMLSpot.GetAttribute("GrossPrice"))
    '                TmpMaM = XMLSpot.GetAttribute("MaM")
    '                tmpMyEstimate = CSng(Conv(XMLSpot.GetAttribute("MyEstimate")))
    '                TmpMyEstChanTarget = CSng(Conv(XMLSpot.GetAttribute("MyEstimateBuyTarget")))
    '                TmpNetPrice = CDec(Conv(XMLSpot.GetAttribute("NetPrice")))
    '                TmpPlacement = Val(XMLSpot.GetAttribute("Placement"))
    '                TmpProgAfter = XMLSpot.GetAttribute("ProgAfter")
    '                TmpProgBefore = XMLSpot.GetAttribute("ProgBefore")
    '                TmpProg = XMLSpot.GetAttribute("Programme")
    '                TmpBT = XMLSpot.GetAttribute("Bookingtype")
    '                TmpDBID = XMLSpot.GetAttribute("DatabaseID") 'TmpString & Format(TmpDate, "yyyymmdd") & Left(helper.mam2tid(TmpMaM), 2) & Right(helper.mam2tid(TmpMaM), 2)
    '                If IsDBNull(XMLSpot.GetAttribute("IsLocal")) Then
    '                    TmpIsLocal = False
    '                    TmpIsRB = False
    '                Else
    '                    TmpIsLocal = XMLSpot.GetAttribute("IsLocal")
    '                    TmpIsRB = XMLSpot.GetAttribute("IsRB")
    '                End If

    '                mvarBookedSpots.Add(TmpID, TmpDBID, TmpString, TmpDate, TmpMaM, TmpProg, TmpProgAfter, TmpProgBefore, TmpGrossPrice, TmpNetPrice, TmpChannelEstimate, tmpMyEstimate, TmpMyEstChanTarget, TmpFilmcode, TmpBT, TmpIsLocal, TmpIsRB)
    '                If Not IsDBNull(XMLSpot.GetAttribute("Comments")) Then
    '                    mvarBookedSpots(TmpID).Comments = XMLSpot.GetAttribute("Comments")
    '                End If
    '                mvarBookedSpots(TmpID).AddedValues = New Dictionary(Of String, Trinity.cAddedValue)
    '                If Not XMLSpot.GetElementsByTagName("AddedValues").Item(0) Is Nothing Then
    '                    Dim XMLIndex As XmlElement = XMLSpot.GetElementsByTagName("AddedValues").Item(0).FirstChild
    '                    While Not XMLIndex Is Nothing
    '                        Tmpstr = XMLIndex.GetAttribute("ID")
    '                        Dim TmpAV As Trinity.cAddedValue = mvarBookedSpots(TmpID).Bookingtype.AddedValues(XMLIndex.GetAttribute("ID"))
    '                        mvarBookedSpots(TmpID).AddedValues.Add(Tmpstr, TmpAV)
    '                        XMLIndex = XMLIndex.NextSibling
    '                    End While
    '                End If
    '                XMLSpot = XMLSpot.NextSibling
    '            End While

    '            Dim TmpCampaign As cKampanj

    '            XMLTmpNode = XMLDoc.GetElementsByTagName("LabCampaigns").Item(0).FirstChild
    '            While Not XMLTmpNode Is Nothing
    '                TmpCampaign = New cKampanj
    '                TmpCampaign.LoadCampaign("", True, XMLTmpNode.OuterXml)
    '                If Campaigns Is Nothing Then
    '                    Campaigns = New Dictionary(Of String, Trinity.cKampanj)
    '                End If
    '                Campaigns.Add(XMLTmpNode.GetAttribute("LabName"), TmpCampaign)
    '                XMLTmpNode = XMLTmpNode.NextSibling
    '            End While

    '            XMLTmpNode = XMLCamp.SelectSingleNode("./History").FirstChild
    '            While Not XMLTmpNode Is Nothing
    '                TmpCampaign = New cKampanj
    '                TmpCampaign.LoadCampaign("", True, XMLTmpNode.OuterXml)
    '                TmpCampaign.RootCampaign = Me
    '                History.Add(TmpCampaign.ID, TmpCampaign)
    '                XMLTmpNode = XMLTmpNode.NextSibling
    '            End While
    '            '    Stop

    '            For Each TmpChannel In mvarChannels
    '                For Each TmpBookingType In TmpChannel.BookingTypes
    '                    For Each TmpWeek In TmpBookingType.Weeks
    '                        For Each TmpFilm In TmpWeek.Films
    '                            If TmpFilm.Filmcode = "" Then
    '                                For Each TmpChan As Trinity.cChannel In mvarChannels
    '                                    For Each TmpBT2 As Trinity.cBookingType In TmpChan.BookingTypes
    '                                        If TmpBT2.Weeks(1).Films(TmpFilm.Name).Filmcode <> "" Then
    '                                            TmpFilm.Filmcode = TmpBT2.Weeks(1).Films(TmpFilm.Name).Filmcode
    '                                        End If
    '                                    Next
    '                                Next
    '                            End If
    '                        Next
    '                    Next
    '                Next
    '            Next

    '            mvarFilename = Path
    '            Loading = False
    '            Exit Function

    'ErrHandle:
    '            If CurrentlyReading = "Planned spots" Then
    '                a = MsgBox("The follwing error: '" & Err.Description & "' occured while reading planned spot no " & s & Chr(10) & Chr(10) & "Please click one of the following:" & Chr(10) & "'Abort': Continue to read file but ignore planned spots" & Chr(10) & "'Retry': Read the next planned spot" & Chr(10) & "'Ignore': Continue reading other info on this spot", MsgBoxStyle.AbortRetryIgnore, "TRINITY")
    '                If a = MsgBoxResult.Ignore Then
    '                    Resume Next
    '                ElseIf a = MsgBoxResult.Abort Then
    '                    Resume ActualSpots
    '                ElseIf a = MsgBoxResult.Retry Then
    '                    Resume
    '                End If
    '            End If
    '            LoadCampaign = 1
    '            Loading = False
    '            Err.Raise(Err.Number, Err.Source, Err.Description)
    '            Resume Next

    '        End Function





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : TotalTRP
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Function TotalTRP(Optional ByVal IncludeCompensation As Boolean = True) As Single

    '            Dim TmpChannel As cChannel
    '            Dim TmpBookingType As cBookingType
    '            Dim TmpTRP As Single = 0

    '            For Each TmpChannel In mvarChannels
    '                For Each TmpBookingType In TmpChannel.BookingTypes
    '                    If TmpBookingType.BookIt Then
    '                        TmpTRP += TmpBookingType.TotalTRP(IncludeCompensation)
    '                    End If
    '                Next TmpBookingType
    '            Next TmpChannel
    '            Return TmpTRP

    '        End Function




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : EstimatedCommission
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Function EstimatedCommission() As Object

    '            Dim TmpChan As cChannel
    '            Dim TmpBT As cBookingType
    '            Dim TmpWeek As cWeek

    '            EstimatedCommission = 0
    '            For Each TmpChan In mvarChannels
    '                For Each TmpBT In TmpChan.BookingTypes
    '                    If TmpBT.BookIt Then
    '                        For Each TmpWeek In TmpBT.Weeks
    '                            EstimatedCommission = EstimatedCommission + TmpWeek.NetBudget * TmpChan.AgencyCommission
    '                        Next TmpWeek
    '                    End If
    '                Next TmpBT
    '            Next TmpChan
    '        End Function




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : PlannedMediaNet
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Function PlannedMediaNet() As Decimal
    '            Dim TmpChan As cChannel
    '            Dim TmpBT As cBookingType
    '            Dim TmpWeek As cWeek
    '            Dim TmpMediaNet As Decimal

    '            For Each TmpChan In mvarChannels
    '                For Each TmpBT In TmpChan.BookingTypes
    '                    If TmpBT.BookIt Then
    '                        For Each TmpWeek In TmpBT.Weeks
    '                            TmpMediaNet = TmpMediaNet + TmpWeek.NetBudget
    '                        Next TmpWeek
    '                    End If
    '                Next TmpBT
    '            Next TmpChan
    '            PlannedMediaNet = TmpMediaNet
    '        End Function



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : PlannedNet
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Function PlannedNet() As Object
    '            Dim TmpChan As cChannel
    '            Dim TmpBT As cBookingType
    '            Dim TmpWeek As cWeek
    '            Dim TmpCost As cCost
    '            Dim TotCost As Decimal
    '            Dim TotCommission As Decimal
    '            Dim TmpNet As Decimal

    '            For Each TmpChan In mvarChannels
    '                For Each TmpBT In TmpChan.BookingTypes
    '                    If TmpBT.BookIt Then
    '                        For Each TmpWeek In TmpBT.Weeks
    '                            TmpNet = TmpNet + TmpWeek.NetBudget
    '                            TotCommission = TotCommission + TmpWeek.NetBudget * TmpChan.AgencyCommission
    '                        Next TmpWeek
    '                    End If
    '                Next TmpBT
    '            Next TmpChan
    '            TotCost = 0
    '            For Each TmpCost In mvarCosts
    '                If TmpCost.CostType = cCost.CostTypeEnum.CostTypePercent Then
    '                    If TmpCost.CountCostOn = cCost.CostOnPercentEnum.CostOnMediaNet Then
    '                        TotCost = TotCost + TmpNet * TmpCost.Amount
    '                    End If
    '                End If
    '            Next TmpCost
    '            PlannedNet = TmpNet + TotCost - TotCommission

    '        End Function



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : PlannedNetNet
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Function PlannedNetNet() As Object
    '            Dim TmpChan As cChannel
    '            Dim TmpBT As cBookingType
    '            Dim TmpWeek As cWeek
    '            Dim TmpCost As cCost
    '            Dim TotCost As Decimal
    '            Dim TotCommission As Decimal
    '            Dim TmpNetNet As Decimal

    '            For Each TmpChan In mvarChannels
    '                For Each TmpBT In TmpChan.BookingTypes
    '                    If TmpBT.BookIt Then
    '                        For Each TmpWeek In TmpBT.Weeks
    '                            TmpNetNet = TmpNetNet + TmpWeek.NetBudget
    '                            TotCommission = TotCommission + TmpWeek.NetBudget * TmpChan.AgencyCommission
    '                        Next TmpWeek
    '                    End If
    '                Next TmpBT
    '            Next TmpChan
    '            TotCost = 0
    '            For Each TmpCost In mvarCosts
    '                If TmpCost.CostType = cCost.CostTypeEnum.CostTypePercent Then
    '                    If TmpCost.CountCostOn = cCost.CostOnPercentEnum.CostOnMediaNet Then
    '                        TotCost = TotCost + TmpNetNet * TmpCost.Amount
    '                    End If
    '                End If
    '            Next TmpCost
    '            TmpNetNet = TmpNetNet + TotCost - TotCommission
    '            TotCost = 0
    '            For Each TmpCost In mvarCosts
    '                If TmpCost.CostType = cCost.CostTypeEnum.CostTypePercent Then
    '                    If TmpCost.CountCostOn = cCost.CostOnPercentEnum.CostOnNet Then
    '                        TotCost = TotCost + TmpNetNet * TmpCost.Amount
    '                    End If
    '                End If
    '            Next TmpCost
    '            TmpNetNet = TmpNetNet + TotCost

    '            For Each TmpCost In mvarCosts
    '                If TmpCost.CostType = cCost.CostTypeEnum.CostTypeFixed Then
    '                    TmpNetNet = TmpNetNet + TmpCost.Amount
    '                ElseIf TmpCost.CostType = cCost.CostTypeEnum.CostTypePerUnit Then
    '                    If TmpCost.CountCostOn = cCost.CostOnUnitEnum.CostOnSpots Then
    '                        For Each TmpChan In mvarChannels
    '                            For Each TmpBT In TmpChan.BookingTypes
    '                                TmpNetNet = TmpNetNet + TmpBT.EstimatedSpotCount * TmpCost.Amount
    '                            Next TmpBT
    '                        Next TmpChan
    '                    ElseIf TmpCost.CountCostOn = cCost.CostOnUnitEnum.CostOnBuyingTRP Then
    '                        For Each TmpChan In mvarChannels
    '                            For Each TmpBT In TmpChan.BookingTypes
    '                                TmpNetNet = TmpNetNet + TmpBT.TotalTRPBuyingTarget * TmpCost.Amount
    '                            Next TmpBT
    '                        Next TmpChan
    '                    ElseIf TmpCost.CountCostOn = cCost.CostOnUnitEnum.CostOnMainTRP Then
    '                        For Each TmpChan In mvarChannels
    '                            For Each TmpBT In TmpChan.BookingTypes
    '                                TmpNetNet = TmpNetNet + TmpBT.TotalTRP * TmpCost.Amount
    '                            Next TmpBT
    '                        Next TmpChan
    '                    End If
    '                End If
    '            Next TmpCost
    '            PlannedNetNet = TmpNetNet
    '        End Function



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : SpotIndex
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Function SpotIndex() As Single

    '            Dim TmpBT As cBookingType
    '            Dim TmpChannel As cChannel
    '            Dim TmpWeek As cWeek
    '            Dim TmpFilm As cFilm
    '            Dim TRP As Single
    '            Dim TRP30 As Single
    '            Dim x As Short
    '            Dim TmpIndex As Single

    '            TmpIndex = 0
    '            For Each TmpChannel In mvarChannels
    '                For Each TmpBT In TmpChannel.BookingTypes
    '                    If TmpBT.BookIt Then
    '                        For Each TmpWeek In TmpBT.Weeks
    '                            x = 1
    '                            For Each TmpFilm In TmpWeek.Films
    '                                TRP = TRP + TmpWeek.TRP * (TmpFilm.Share / 100)
    '                                TRP30 = TRP30 + (TmpWeek.TRP * (TmpFilm.Share / 100)) * (TmpFilm.Index / 100)
    '                                x = x + 1
    '                            Next TmpFilm
    '                        Next TmpWeek
    '                    End If
    '                Next TmpBT
    '            Next TmpChannel
    '            If TRP > 0 Then
    '                SpotIndex = TRP30 / TRP
    '            Else
    '                SpotIndex = 0
    '            End If
    '        End Function



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : Conv
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Private Function Conv(ByVal strn As String) As String

    '            Dim Tmpstr As String

    '            Tmpstr = strn
    '            If InStr(Tmpstr, ".") > 0 Then
    '                Mid(Tmpstr, InStr(Tmpstr, "."), 1) = ","
    '            End If
    '            Conv = Tmpstr


    '        End Function




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : CreateChannels
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Sub CreateChannels()

    '            Dim XMLDoc As Xml.XmlDocument = New Xml.XmlDocument
    '            Dim XMLChannels As Xml.XmlElement
    '            Dim XMLTmpNode As Xml.XmlElement
    '            Dim XMLTmpNode2 As Xml.XmlElement
    '            Dim XMLBookingTypes As Xml.XmlElement
    '            Dim TmpChannel As cChannel
    '            Dim TmpBT As cBookingType

    '            XMLDoc.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & mvarArea & "\Channels.xml")

    '            XMLChannels = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("Channels")
    '            XMLBookingTypes = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("Bookingtypes")

    '            XMLTmpNode = XMLChannels.ChildNodes.Item(0)

    '            mvarChannels = New cChannels(Me)
    '            mvarChannels.MainObject = Me
    '            While Not XMLTmpNode Is Nothing
    '                TmpChannel = mvarChannels.Add(XMLTmpNode.GetAttribute("Name"), Campaign.Area)
    '                XMLTmpNode2 = XMLBookingTypes.ChildNodes.Item(0)
    '                While Not XMLTmpNode2 Is Nothing
    '                    TmpBT = TmpChannel.BookingTypes.Add(XMLTmpNode2.GetAttribute("Name"), False)
    '                    TmpBT.Shortname = XMLTmpNode2.GetAttribute("Shortname")
    '                    TmpBT.IsRBS = XMLTmpNode2.GetAttribute("IsRBS")
    '                    TmpBT.IsSpecific = XMLTmpNode2.GetAttribute("IsSpecific")
    '                    XMLTmpNode2 = XMLTmpNode2.NextSibling
    '                End While
    '                XMLTmpNode2 = XMLTmpNode.GetElementsByTagName("SpotIndex").Item(0).FirstChild
    '                While Not XMLTmpNode2 Is Nothing
    '                    For Each TmpBT In TmpChannel.BookingTypes
    '                        TmpBT.FilmIndex(XMLTmpNode2.GetAttribute("Length")) = XMLTmpNode2.GetAttribute("Idx")
    '                    Next TmpBT
    '                    XMLTmpNode2 = XMLTmpNode2.NextSibling
    '                End While
    '                XMLTmpNode = XMLTmpNode.NextSibling
    '            End While
    '        End Sub





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ChannelString
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Function ChannelString() As String
    '            Dim TmpChan As cChannel
    '            Dim TmpBT As cBookingType
    '            Dim IsUsed As Boolean
    '            Dim Tmpstr As String

    '            Tmpstr = ""
    '            For Each TmpChan In mvarChannels
    '                IsUsed = False
    '                For Each TmpBT In TmpChan.BookingTypes
    '                    If TmpBT.BookIt Then
    '                        IsUsed = True
    '                        Exit For
    '                    End If
    '                Next TmpBT
    '                If IsUsed Then
    '                    Tmpstr = Tmpstr & TmpChan.AdEdgeNames & ","
    '                End If
    '            Next TmpChan
    '            ChannelString = Tmpstr
    '        End Function



    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : FilmcodeString
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Function FilmcodeString() As String
    '            Dim TmpChan As cChannel
    '            Dim TmpBT As cBookingType
    '            Dim TmpFilm As cFilm
    '            Dim Tmpstr As String

    '            Tmpstr = ""
    '            For Each TmpChan In mvarChannels
    '                For Each TmpBT In TmpChan.BookingTypes
    '                    If TmpBT.BookIt Then
    '                        For Each TmpFilm In TmpBT.Weeks(1).Films
    '                            If InStr(Tmpstr, "," & TmpFilm.Filmcode & ",") = 0 Then
    '                                Tmpstr = Tmpstr & "," & TmpFilm.Filmcode & ","
    '                            End If
    '                        Next TmpFilm
    '                    End If
    '                Next TmpBT
    '            Next TmpChan
    '            FilmcodeString = Tmpstr
    '        End Function





    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ReachActual
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Function ReachActual(ByRef Freq As Object, Optional ByRef Target As ReachTargetEnum = 0, Optional ByRef CustomTarget As String = "3+", Optional ByRef Customuniverse As String = "") As Single
    '            Dim t As String
    '            Dim u As String
    '            Dim Dummy As Short

    '            On Error Resume Next
    '            If Target = ReachTargetEnum.rteMainTarget Then
    '                t = mvarMainTarget.TargetName
    '                u = mvarMainTarget.Universe
    '                Dummy = TargColl(t, mvarAdedge)
    '                If Err.Number > 0 Then
    '                    Dummy = mvarMainTarget.UniSize
    '                End If
    '            ElseIf Target = ReachTargetEnum.rteSecondTarget Then
    '                t = mvarSecondaryTarget.TargetName
    '                u = mvarSecondaryTarget.Universe
    '                Dummy = TargColl(t, mvarAdedge)
    '                If Err.Number > 0 Then
    '                    Dummy = mvarSecondaryTarget.UniSize
    '                End If
    '            ElseIf Target = ReachTargetEnum.rteThirdTarget Then
    '                t = mvarThirdTarget.TargetName
    '                u = mvarThirdTarget.Universe
    '                Dummy = TargColl(t, mvarAdedge)
    '                If Err.Number > 0 Then
    '                    Dummy = mvarThirdTarget.UniSize
    '                End If
    '            ElseIf Target = ReachTargetEnum.rteAllAdults Then
    '                t = mvarAllAdults
    '                u = ""
    '            ElseIf Target = ReachTargetEnum.rteCustomTarget Then
    '                t = CustomTarget
    '                u = Customuniverse
    '            Else
    '                t = ""
    '                u = ""
    '            End If
    '            If Not mvarAdedge.getGroupCount = 0 Then
    '                Return mvarAdedge.getRF(mvarAdedge.getGroupCount - 1, , UniColl.Item(u) - 1, TargColl(t, mvarAdedge) - 1, Freq)
    '            Else
    '                Return 0
    '            End If
    '        End Function




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : ReachGoal
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Property ReachGoal(ByVal Freq As Single)
    '            Get
    '                Return mvarReachGoal(Freq)
    '            End Get
    '            Set(ByVal value)
    '                mvarReachGoal(Freq) = value
    '            End Set
    '        End Property




    '        '---------------------------------------------------------------------------------------
    '        ' Procedure : LoadOldCampaign
    '        ' DateTime  : -
    '        ' Author    : joho
    '        ' Purpose   :
    '        '---------------------------------------------------------------------------------------
    '        '
    '        Public Sub LoadOldCampaign(ByRef Path As String)

    '            Dim XMLDoc As Xml.XmlDocument = New Xml.XmlDocument
    '            Dim XMLCamp As Xml.XmlElement
    '            Dim XMLTmpNode As Xml.XmlElement
    '            Dim XMLTmpNode2 As Xml.XmlElement
    '            Dim IniFile As New clsIni

    '            Dim XMLChannel As Xml.XmlElement
    '            Dim XMLBookingType As Xml.XmlElement
    '            Dim XMLWeek As Xml.XmlElement
    '            Dim XMLFilm As Xml.XmlElement
    '            Dim XMLTarget As Xml.XmlElement
    '            Dim XMLBuyTarget As Xml.XmlElement
    '            Dim XMLPricelist As Xml.XmlElement
    '            Dim XMLSpot As Xml.XmlElement

    '            Dim TmpChannel As cChannel
    '            Dim TmpBookingType As cBookingType
    '            Dim TmpWeek As cWeek
    '            Dim TmpFilm As cFilm
    '            Dim TmpPLTarget As cPricelistTarget
    '            Dim TmpIndex As cIndex
    '            Dim TmpPlannedSpot As cPlannedSpot
    '            Dim TmpActualSpot As cActualSpot

    '            Dim i As Short
    '            Dim TmpString As String
    '            Dim TmpLong As Integer
    '            Dim TmpInt As Short

    '            On Error Resume Next

    '            XMLDoc.Load(Path)

    '            IniFile.Create(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & mvarArea & "\area.ini")
    '            XMLCamp = XMLDoc.GetElementsByTagName("Campaign").Item(0)

    '            mvarVersion = XMLCamp.GetAttribute("Version")
    '            mvarName = XMLCamp.GetAttribute("Name")
    '            mvarUpdatedTo = XMLCamp.GetAttribute("UpdatedTo")
    '            mvarPlanner = XMLCamp.GetAttribute("Planner")
    '            mvarBuyer = XMLCamp.GetAttribute("Buyer")
    '            mvarFrequencyFocus = XMLCamp.GetAttribute("FrequencyFocus")
    '            mvarFilename = ""
    '            'TODO: Produkt och Kund - om de inte finns ska de läggas till? Annars
    '            '      skall ett id hittas i databasen
    '            mvarBudgetTotalCTC = XMLCamp.GetAttribute("BudgetTotalCTC")
    '            If XMLCamp.GetAttribute("ServiceFee") > 0 Then
    '                If XMLCamp.GetAttribute("ServiceFeeOnNet1") Then
    '                    mvarCosts.Add("Service fee", cCost.CostTypeEnum.CostTypePercent, XMLCamp.GetAttribute("ServiceFee") / 100, cCost.CostOnPercentEnum.CostOnNetNet, 0)
    '                Else
    '                    mvarCosts.Add("Service fee", cCost.CostTypeEnum.CostTypePercent, XMLCamp.GetAttribute("ServiceFee") / 100, cCost.CostOnPercentEnum.CostOnNet, 0)
    '                End If
    '            End If
    '            If XMLCamp.GetAttribute("TrackingCost") > 0 Then
    '                mvarCosts.Add("Tracking", cCost.CostTypeEnum.CostTypeFixed, XMLCamp.GetAttribute("TrackingCost"), 0, 217)
    '            End If
    '            If XMLCamp.GetAttribute("TVCheck") > 0 Then
    '                mvarCosts.Add("TV Check", cCost.CostTypeEnum.CostTypePerUnit, XMLCamp.GetAttribute("TVCheck"), cCost.CostOnUnitEnum.CostOnSpots, 206)
    '            End If
    '            mvarCommentary = XMLCamp.GetAttribute("Commentary")
    '            Area = XMLCamp.GetAttribute("Area")
    '            mvarAreaLog = XMLCamp.GetAttribute("AreaLog")
    '            mvarAllAdults = XMLCamp.GetAttribute("AllAdults")

    '            XMLTmpNode = XMLCamp.GetElementsByTagName("Dayparts").Item(0).FirstChild
    '            i = 0
    '            While Not XMLTmpNode Is Nothing

    '                mvarDaypartName(i) = XMLTmpNode.LocalName
    '                mvarDaypartStart(i) = XMLTmpNode.GetAttribute("Start")
    '                mvarDaypartEnd(i) = XMLTmpNode.GetAttribute("End")
    '                XMLTmpNode = XMLTmpNode.NextSibling
    '                i = i + 1
    '            End While
    '            DaypartCount = i
    '            XMLTarget = XMLCamp.SelectSingleNode("MainTarget")
    '            mvarMainTarget.TargetName = XMLTarget.GetAttribute("Name")
    '            mvarMainTarget.TargetType = XMLTarget.GetAttribute("Type")
    '            mvarMainTarget.Universe = XMLTarget.GetAttribute("Universe")
    '            mvarMainTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
    '            If mvarMainTarget.SecondUniverse = "" Then
    '                mvarMainTarget.SecondUniverse = IniFile.Text("Universe", "Second")
    '            End If

    '            XMLTarget = XMLCamp.SelectSingleNode("SecondaryTarget")
    '            mvarSecondaryTarget.TargetName = XMLTarget.GetAttribute("Name")
    '            mvarSecondaryTarget.TargetType = XMLTarget.GetAttribute("Type")
    '            mvarSecondaryTarget.Universe = XMLTarget.GetAttribute("Universe")
    '            mvarSecondaryTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
    '            If mvarSecondaryTarget.SecondUniverse = "" Then
    '                mvarSecondaryTarget.SecondUniverse = IniFile.Text("Universe", "Second")
    '            End If

    '            XMLTarget = XMLCamp.SelectSingleNode("ThirdTarget")
    '            mvarThirdTarget.TargetName = XMLTarget.GetAttribute("Name")
    '            mvarThirdTarget.TargetType = XMLTarget.GetAttribute("Type")
    '            mvarThirdTarget.Universe = XMLTarget.GetAttribute("Universe")
    '            mvarThirdTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
    '            If mvarThirdTarget.SecondUniverse = "" Then
    '                mvarThirdTarget.SecondUniverse = IniFile.Text("Universe", "Second")
    '            End If

    '            mvarChannels = New cChannels(Me)
    '            mvarChannels.MainObject = Me

    '            XMLChannel = XMLCamp.SelectSingleNode("Channels").FirstChild
    '            While Not XMLChannel Is Nothing
    '                TmpString = XMLChannel.GetAttribute("Name")
    '                TmpChannel = Nothing
    '                TmpChannel = mvarChannels.Add(TmpString)
    '                If Not TmpChannel Is Nothing Then
    '                    TmpChannel.ID = XMLChannel.GetAttribute("ID")
    '                    TmpChannel.Shortname = XMLChannel.GetAttribute("ShortName")
    '                    TmpChannel.BuyingUniverse = XMLChannel.GetAttribute("BuyingUniverse")
    '                    TmpChannel.AdEdgeNames = XMLChannel.GetAttribute("AdEdgeNames")
    '                    TmpChannel.DefaultArea = XMLChannel.GetAttribute("DefaultArea")
    '                    TmpChannel.AgencyCommission = Conv(XMLChannel.GetAttribute("AgencyCommission"))
    '                    If Not IsDBNull(XMLChannel.GetAttribute("DeliveryAddress")) Then
    '                        TmpChannel.DeliveryAddress = XMLChannel.GetAttribute("DeliveryAddress")
    '                    End If

    '                    'Save the targets

    '                    XMLTarget = XMLChannel.GetElementsByTagName("MainTarget").Item(0)
    '                    TmpChannel.MainTarget.NoUniverseSize = True
    '                    If XMLTarget.GetAttribute("Name") = "4-" Then
    '                        TmpChannel.MainTarget.TargetName = "20-44"
    '                    Else
    '                        TmpChannel.MainTarget.TargetName = XMLTarget.GetAttribute("Name")
    '                    End If
    '                    TmpChannel.MainTarget.TargetType = XMLTarget.GetAttribute("Type")
    '                    TmpChannel.MainTarget.Universe = XMLTarget.GetAttribute("Universe")
    '                    TmpChannel.MainTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
    '                    If TmpChannel.MainTarget.SecondUniverse = "" Then
    '                        TmpChannel.MainTarget.SecondUniverse = IniFile.Text("Universe", "Second")
    '                    End If
    '                    TmpChannel.MainTarget.NoUniverseSize = False

    '                    XMLTarget = XMLChannel.GetElementsByTagName("SecondaryTarget").Item(0)
    '                    TmpChannel.SecondaryTarget.NoUniverseSize = True
    '                    TmpChannel.SecondaryTarget.TargetName = XMLTarget.GetAttribute("Name")
    '                    TmpChannel.SecondaryTarget.TargetType = XMLTarget.GetAttribute("Type")
    '                    TmpChannel.SecondaryTarget.Universe = XMLTarget.GetAttribute("Universe")
    '                    TmpChannel.SecondaryTarget.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
    '                    If TmpChannel.SecondaryTarget.SecondUniverse = "" Then
    '                        TmpChannel.SecondaryTarget.SecondUniverse = IniFile.Text("Universe", "Second")
    '                    End If
    '                    TmpChannel.SecondaryTarget.NoUniverseSize = False
    '                    If Not IsDBNull(XMLTarget.GetAttribute("Marathon")) AndAlso XMLTarget.GetAttribute("Marathon") <> "" Then
    '                        TmpChannel.MarathonName = XMLTarget.GetAttribute("Marathon")
    '                    End If
    '                    If Not IsDBNull(XMLTarget.GetAttribute("Penalty")) AndAlso XMLTarget.GetAttribute("Penalty") <> "" Then
    '                        TmpChannel.Penalty = XMLTarget.GetAttribute("Penalty")
    '                        TmpChannel.ConnectedChannel = XMLTarget.GetAttribute("ConnectedChannel")
    '                    End If

    '                    'Read Booking types

    '                    XMLBookingType = XMLChannel.GetElementsByTagName("BookingTypes").Item(0).FirstChild
    '                    While Not XMLBookingType Is Nothing

    '                        TmpString = XMLBookingType.GetAttribute("Name")
    '                        TmpBookingType = TmpChannel.BookingTypes.Add(TmpString)
    '                        TmpBookingType.Shortname = XMLBookingType.GetAttribute("Shortname")

    '                        TmpBookingType.IndexMainTarget = Conv(XMLBookingType.GetAttribute("IndexMainTarget"))
    '                        TmpBookingType.IndexAllAdults = Conv(XMLBookingType.GetAttribute("Index3plus"))
    '                        XMLTmpNode = XMLBookingType.GetElementsByTagName("DaypartSplit").Item(0)
    '                        For i = 0 To DaypartCount - 1
    '                            XMLTmpNode2 = XMLTmpNode.GetElementsByTagName(mvarDaypartName(i)).Item(0)
    '                            TmpBookingType.DaypartSplit(i) = Val(XMLTmpNode2.GetAttribute("Share"))
    '                        Next
    '                        TmpBookingType.BookIt = XMLBookingType.GetAttribute("BookIt")
    '                        TmpBookingType.GrossCPP = Conv(XMLBookingType.GetAttribute("GrossCPP"))
    '                        TmpBookingType.AverageRating = Conv(XMLBookingType.GetAttribute("AverageRating"))
    '                        TmpBookingType.ConfirmedNetBudget = Conv(XMLBookingType.GetAttribute("ConfirmedNetBudget"))
    '                        TmpBookingType.Bookingtype = XMLBookingType.GetAttribute("Bookingtype")
    '                        TmpBookingType.ContractNumber = XMLBookingType.GetAttribute("ContractNumber")
    '                        TmpBookingType.IsRBS = XMLBookingType.GetAttribute("IsRBS")
    '                        TmpBookingType.IsSpecific = XMLBookingType.GetAttribute("IsSpecific")
    '                        If Not IsDBNull(XMLBookingType.GetAttribute("OrderNumber")) Then
    '                            TmpBookingType.OrderNumber = XMLBookingType.GetAttribute("OrderNumber")
    '                        Else
    '                            TmpBookingType.OrderNumber = ""
    '                        End If

    '                        'Read Buyingtarget

    '                        XMLBuyTarget = XMLBookingType.SelectSingleNode("BuyingTarget")

    '                        TmpBookingType.BuyingTarget.CalcCPP = XMLBuyTarget.GetAttribute("CalcCPP")
    '                        TmpBookingType.BuyingTarget.CPP = Conv(XMLBuyTarget.GetAttribute("CPP"))
    '                        TmpBookingType.BuyingTarget.TargetName = XMLBuyTarget.GetAttribute("TargetName")
    '                        TmpBookingType.BuyingTarget.UniSize = XMLBuyTarget.GetAttribute("UniSize")
    '                        TmpBookingType.BuyingTarget.UniSizeNat = XMLBuyTarget.GetAttribute("UniSizeNat")
    '                        If TmpChannel.BuyingUniverse = "" Then
    '                            TmpBookingType.BuyingTarget.UniSize = TmpBookingType.BuyingTarget.UniSizeNat
    '                        End If

    '                        XMLTmpNode = XMLBuyTarget.GetElementsByTagName("DaypartCPP").Item(0)
    '                        For i = 0 To DaypartCount - 1
    '                            If IsDBNull(XMLTmpNode.GetAttribute(mvarDaypartName(i))) Then
    '                                XMLTmpNode.SetAttribute(mvarDaypartName(i), 0)
    '                            End If
    '                            TmpBookingType.BuyingTarget.CPPDaypart(i) = Conv(XMLTmpNode.GetAttribute(mvarDaypartName(i)))
    '                        Next

    '                        XMLTarget = XMLBuyTarget.GetElementsByTagName("Target").Item(0)
    '                        TmpBookingType.BuyingTarget.Target.NoUniverseSize = True
    '                        TmpBookingType.BuyingTarget.Target.TargetName = XMLTarget.GetAttribute("Name")
    '                        TmpBookingType.BuyingTarget.Target.TargetType = XMLTarget.GetAttribute("Type")
    '                        TmpBookingType.BuyingTarget.Target.Universe = XMLTarget.GetAttribute("Universe")
    '                        TmpBookingType.BuyingTarget.Target.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
    '                        If TmpBookingType.BuyingTarget.Target.SecondUniverse = "" Then
    '                            TmpBookingType.BuyingTarget.Target.SecondUniverse = IniFile.Text("Universe", "Second")
    '                        End If
    '                        TmpBookingType.BuyingTarget.Target.NoUniverseSize = False
    '                        If Not IsDBNull(XMLBookingType.GetAttribute("IsEntered")) Then
    '                            TmpBookingType.BuyingTarget.IsEntered = XMLBookingType.GetAttribute("IsEntered")
    '                        End If
    '                        If TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eDiscount Then
    '                            TmpBookingType.BuyingTarget.Discount = XMLBookingType.GetAttribute("Discount")
    '                        ElseIf TmpBookingType.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPT Then
    '                            TmpBookingType.BuyingTarget.NetCPT = Conv(XMLBookingType.GetAttribute("NetCPT"))
    '                        Else
    '                            TmpBookingType.BuyingTarget.NetCPP = Conv(XMLBookingType.GetAttribute("NetCPP"))
    '                        End If

    '                        'Read the pricelist

    '                        XMLPricelist = XMLBookingType.GetElementsByTagName("Pricelist").Item(0)

    '                        XMLTmpNode = XMLPricelist.GetElementsByTagName("DaypartSplit").Item(0)
    '                        '            For i = 0 To DaypartCount - 1
    '                        '                Set XMLTmpNode2 = XMLTmpNode.getElementsByTagName(mvarDaypartName(i)).Item(0)
    '                        '                TmpBookingType.Pricelist.DefaultDaypart(i) = Val(XMLTmpNode2.getAttribute("DefaultSplit"))
    '                        '            Next

    '                        TmpBookingType.Pricelist.StartDate = XMLPricelist.GetAttribute("StartDate")
    '                        TmpBookingType.Pricelist.EndDate = XMLPricelist.GetAttribute("EndDate")
    '                        TmpBookingType.Pricelist.BuyingUniverse = XMLPricelist.GetAttribute("BuyingUniverse")

    '                        XMLBuyTarget = XMLPricelist.GetElementsByTagName("Targets").Item(0).FirstChild

    '                        While Not XMLBuyTarget Is Nothing

    '                            TmpString = XMLBuyTarget.GetAttribute("Name")
    '                            TmpPLTarget = TmpBookingType.Pricelist.Targets.Add(TmpString)
    '                            TmpPLTarget.CalcCPP = XMLBuyTarget.GetAttribute("CalcCPP")
    '                            TmpPLTarget.CPP = Conv(XMLBuyTarget.GetAttribute("CPP"))

    '                            XMLTmpNode = XMLBuyTarget.GetElementsByTagName("DaypartCPP").Item(0)
    '                            For i = 0 To DaypartCount - 1
    '                                XMLTmpNode2 = XMLTmpNode.GetElementsByTagName(mvarDaypartName(i)).Item(0)
    '                                TmpPLTarget.CPPDaypart(i) = Conv(XMLTmpNode2.GetAttribute("CPP"))
    '                            Next

    '                            TmpPLTarget.UniSize = Conv(XMLBuyTarget.GetAttribute("UniSize"))
    '                            TmpPLTarget.UniSizeNat = Conv(XMLBuyTarget.GetAttribute("UniSizeNat"))

    '                            XMLTarget = XMLBuyTarget.GetElementsByTagName("Target").Item(0)
    '                            TmpPLTarget.Target.NoUniverseSize = True 'For speed. No unisizes are calculated
    '                            TmpPLTarget.Target.TargetName = XMLTarget.GetAttribute("Name")
    '                            TmpPLTarget.Target.TargetType = XMLTarget.GetAttribute("Type")
    '                            TmpPLTarget.Target.Universe = XMLTarget.GetAttribute("Universe")
    '                            TmpPLTarget.Target.SecondUniverse = XMLTarget.GetAttribute("SecondUniverse")
    '                            If TmpPLTarget.Target.SecondUniverse = "" Then
    '                                TmpPLTarget.Target.SecondUniverse = IniFile.Text("Universe", "Second")
    '                            End If
    '                            If Not IsDBNull(XMLBookingType.GetAttribute("IsEntered")) Then
    '                                TmpPLTarget.IsEntered = XMLBookingType.GetAttribute("IsEntered")
    '                            End If

    '                            For i = 0 To DaypartCount
    '                                TmpPLTarget.DefaultDaypart(i) = TmpBookingType.DaypartSplit(i)
    '                            Next

    '                            If TmpPLTarget.IsEntered = cPricelistTarget.EnteredEnum.eDiscount Then
    '                                TmpPLTarget.Discount = XMLBookingType.GetAttribute("Discount")
    '                            ElseIf TmpPLTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPT Then
    '                                TmpPLTarget.NetCPT = Conv(XMLBookingType.GetAttribute("NetCPT"))
    '                            Else
    '                                TmpPLTarget.NetCPP = Conv(XMLBookingType.GetAttribute("NetCPP"))
    '                            End If

    '                            TmpPLTarget.Target.NoUniverseSize = False
    '                            TmpPLTarget.StandardTarget = XMLTarget.GetAttribute("StandardTarget")

    '                            XMLBuyTarget = XMLBuyTarget.NextSibling
    '                        End While

    '                        'Save weeks

    '                        XMLWeek = XMLBookingType.GetElementsByTagName("Weeks").Item(0).FirstChild

    '                        While Not XMLWeek Is Nothing
    '                            'If TmpBookingType.Name = "Specifics" And TmpChannel.ChannelName = "TV4" Then Stop
    '                            TmpString = XMLWeek.GetAttribute("Name")
    '                            TmpWeek = TmpBookingType.Weeks.Add(TmpString)
    '                            TmpWeek.TRPControl = XMLWeek.GetAttribute("TRPControl")
    '                            If TmpWeek.TRPControl Then
    '                                TmpWeek.TRP = Conv(XMLWeek.GetAttribute("TRP"))
    '                                'TmpWeek.TRPBuyingTarget = XMLWeek.GETATTRIBUTE("TRPBuyingTarget")
    '                                'TmpWeek.TRP3Plus = XMLWeek.GETATTRIBUTE("TRP3Plus")
    '                            Else
    '                                TmpWeek.NetBudget = Conv(XMLWeek.GetAttribute("NetBudget"))
    '                            End If
    '                            TmpWeek.StartDate = XMLWeek.GetAttribute("StartDate")
    '                            TmpWeek.EndDate = XMLWeek.GetAttribute("EndDate")
    '                            If XMLWeek.GetAttribute("Modifier") = 2 Or XMLWeek.GetAttribute("Modifier") = 0 Then
    '                                If CDbl(Conv(XMLWeek.GetAttribute("SeasonIndex"))) <> 100 Then
    '                                    TmpIndex = TmpBookingType.Indexes.Add("Season index")
    '                                    TmpIndex.Index = XMLWeek.GetAttribute("SeasonIndex")
    '                                    TmpIndex.FromDate = Date.FromOADate(TmpWeek.StartDate)
    '                                    TmpIndex.ToDate = Date.FromOADate(TmpWeek.EndDate)
    '                                    TmpIndex.IndexOn = cIndex.IndexOnEnum.eNetCPP
    '                                End If
    '                            ElseIf XMLWeek.GetAttribute("Modifier") = 3 Then
    '                                TmpIndex = TmpBookingType.Indexes.Add("Season index")
    '                                TmpIndex.Index = ((1 - XMLWeek.GetAttribute("Discount")) / (1 - XMLBookingType.GetAttribute("Discount"))) * 100
    '                                TmpIndex.FromDate = Date.FromOADate(TmpWeek.StartDate)
    '                                TmpIndex.ToDate = Date.FromOADate(TmpWeek.EndDate)
    '                                TmpIndex.IndexOn = cIndex.IndexOnEnum.eNetCPP
    '                            Else
    '                                System.Diagnostics.Debug.Assert(False, "")
    '                            End If
    '                            If XMLWeek.GetAttribute("GrossIndex") <> 1 Then
    '                                TmpIndex = TmpBookingType.BuyingTarget.Indexes.Add("Index")
    '                                TmpIndex.IndexOn = cIndex.IndexOnEnum.eGrossCPP
    '                                TmpIndex.Index = XMLWeek.GetAttribute("GrossIndex") * 100
    '                                TmpIndex.FromDate = Date.FromOADate(TmpWeek.StartDate)
    '                                TmpIndex.ToDate = Date.FromOADate(TmpWeek.EndDate)
    '                            End If
    '                            For Each TmpPLTarget In TmpBookingType.Pricelist.Targets
    '                                If CDbl(Conv(XMLWeek.GetAttribute("GrossIndex"))) <> 1 Then
    '                                    TmpIndex = TmpPLTarget.Indexes.Add("Gross index")
    '                                    TmpIndex.Index = CDbl(Conv(XMLWeek.GetAttribute("GrossIndex"))) * 100
    '                                    TmpIndex.FromDate = Date.FromOADate(TmpWeek.StartDate)
    '                                    TmpIndex.ToDate = Date.FromOADate(TmpWeek.EndDate)
    '                                    TmpIndex.IndexOn = cIndex.IndexOnEnum.eGrossCPP
    '                                    TmpIndex.SystemGenerated = True
    '                                End If
    '                            Next

    '                            'Save Films

    '                            XMLFilm = XMLWeek.GetElementsByTagName("Films").Item(0).FirstChild

    '                            While Not XMLFilm Is Nothing

    '                                TmpString = XMLFilm.GetAttribute("Filmcode")
    '                                TmpFilm = TmpWeek.Films.Add(TmpString)
    '                                TmpFilm.Filmcode = TmpString
    '                                TmpFilm.FilmLength = XMLFilm.GetAttribute("FilmLength")
    '                                If Not IsDBNull(XMLFilm.GetAttribute("AltFilmcode")) AndAlso XMLFilm.GetAttribute("AltFilmcode") <> "" Then
    '                                    TmpFilm.Filmcode = TmpFilm.Filmcode & "," & XMLFilm.GetAttribute("AltFilmcode")
    '                                End If
    '                                TmpFilm.Index = Conv(XMLFilm.GetAttribute("Index"))
    '                                TmpFilm.Share = Conv(XMLFilm.GetAttribute("Share"))
    '                                TmpFilm.Description = XMLFilm.GetAttribute("Description")
    '                                TmpBookingType.FilmIndex(TmpFilm.FilmLength) = TmpFilm.Index
    '                                XMLFilm = XMLFilm.NextSibling

    '                            End While
    '                            XMLWeek = XMLWeek.NextSibling
    '                        End While
    '                        XMLBookingType = XMLBookingType.NextSibling
    '                    End While
    '                End If
    '                XMLChannel = XMLChannel.NextSibling
    '            End While


    '            'Save Planned spots

    '            XMLSpot = XMLCamp.GetElementsByTagName("PlannedSpots").Item(0).FirstChild

    '            While Not XMLSpot Is Nothing
    'NextPlannedSpot:
    '                TmpString = XMLSpot.GetAttribute("ID")
    '                TmpPlannedSpot = mvarPlannedSpots.Add(TmpString)
    '                TmpString = XMLSpot.GetAttribute("Channel")
    '                TmpPlannedSpot.Channel = mvarChannels(TmpString)
    '                TmpPlannedSpot.ChannelID = XMLSpot.GetAttribute("ChannelID")
    '                TmpString = XMLSpot.GetAttribute("BookingType")
    '                TmpPlannedSpot.Bookingtype = TmpPlannedSpot.Channel.BookingTypes(TmpString) ' ,"cBookingType
    '                TmpLong = XMLSpot.GetAttribute("AirDate")
    '                TmpPlannedSpot.AirDate = TmpLong
    '                TmpString = XMLSpot.GetAttribute("Week")
    '                If TmpString <> "" Then
    '                    TmpPlannedSpot.Week = TmpPlannedSpot.Bookingtype.Weeks(TmpString) ' ,"cWeek
    '                Else
    '                    TmpPlannedSpot.Week = TmpPlannedSpot.Bookingtype.GetWeek(Date.FromOADate(TmpPlannedSpot.AirDate))
    '                End If
    '                If TmpPlannedSpot.Week Is Nothing Then
    '                    i = i
    '                End If
    '                TmpPlannedSpot.MaM = XMLSpot.GetAttribute("MaM")
    '                TmpPlannedSpot.ProgBefore = XMLSpot.GetAttribute("ProgBefore")
    '                TmpPlannedSpot.ProgAfter = XMLSpot.GetAttribute("ProgAfter")
    '                TmpPlannedSpot.Programme = XMLSpot.GetAttribute("Programme")
    '                TmpPlannedSpot.Advertiser = XMLSpot.GetAttribute("Advertiser")
    '                TmpPlannedSpot.Product = XMLSpot.GetAttribute("Product")
    '                TmpPlannedSpot.Filmcode = XMLSpot.GetAttribute("Filmcode")
    '                'On Error Resume Next
    '                'For Each TmpFilm In TmpPlannedSpot.Week.Films
    '                '    If TmpFilm.Filmcode = TmpPlannedSpot.Filmcode Or InStr(TmpFilm.Filmcode, TmpPlannedSpot.Filmcode) > 0 Then
    '                '        TmpPlannedSpot.Film = TmpPlannedSpot.Week.Films(TmpFilm.Filmcode)
    '                '    End If
    '                'Next TmpFilm
    '                'On Error GoTo ErrHandle
    '                TmpPlannedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = Conv(XMLSpot.GetAttribute("RatingBuyTarget"))
    '                TmpPlannedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteMainTarget) = Conv(XMLSpot.GetAttribute("RatingMainTarget"))
    '                TmpPlannedSpot.Estimation = XMLSpot.GetAttribute("Estimation")
    '                TmpPlannedSpot.MyRating(cPlannedSpot.PlannedTargetEnum.pteMainTarget) = Conv(XMLSpot.GetAttribute("MyRating"))
    '                TmpPlannedSpot.MyRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = Conv(XMLSpot.GetAttribute("MyRatingBuyTarget"))
    '                TmpPlannedSpot.Index = Conv(XMLSpot.GetAttribute("Index"))
    '                TmpPlannedSpot.SpotLength = XMLSpot.GetAttribute("SpotLength")
    '                TmpPlannedSpot.SpotType = XMLSpot.GetAttribute("SpotType")
    '                TmpPlannedSpot.PriceNet = (XMLSpot.GetAttribute("PriceNet"))
    '                TmpPlannedSpot.PriceGross = (XMLSpot.GetAttribute("PriceGross"))
    '                If Not IsDBNull(XMLSpot.GetAttribute("Remark")) Then
    '                    TmpPlannedSpot.Remark = XMLSpot.GetAttribute("Remark")
    '                End If
    '                If Not IsDBNull(XMLSpot.GetAttribute("Placement")) Then
    '                    TmpPlannedSpot.Placement = XMLSpot.GetAttribute("Placement")
    '                End If
    '                XMLSpot = XMLSpot.NextSibling
    '            End While

    'ActualSpots:
    '            'Save Actual spots
    '            XMLSpot = XMLCamp.GetElementsByTagName("ActualSpots").Item(0).FirstChild

    '            While Not XMLSpot Is Nothing

    '                TmpLong = XMLSpot.GetAttribute("AirDate")
    '                TmpInt = XMLSpot.GetAttribute("MaM")
    '                TmpActualSpot = mvarActualSpots.Add(System.DateTime.FromOADate(TmpLong), TmpInt)
    '                TmpString = XMLSpot.GetAttribute("Channel")
    '                TmpActualSpot.Channel = mvarChannels(TmpString)
    '                TmpActualSpot.ProgBefore = XMLSpot.GetAttribute("ProgBefore")
    '                TmpActualSpot.ProgAfter = XMLSpot.GetAttribute("ProgAfter")
    '                TmpActualSpot.Programme = XMLSpot.GetAttribute("Programme")
    '                TmpActualSpot.Advertiser = XMLSpot.GetAttribute("Advertiser")
    '                TmpActualSpot.Product = XMLSpot.GetAttribute("Product")
    '                TmpActualSpot.Filmcode = XMLSpot.GetAttribute("Filmcode")
    '                TmpActualSpot.Index = Val(XMLSpot.GetAttribute("Index"))
    '                TmpActualSpot.PosInBreak = XMLSpot.GetAttribute("PosInBreak")
    '                TmpActualSpot.SpotsInBreak = XMLSpot.GetAttribute("SpotsInBreak")
    '                TmpString = XMLSpot.GetAttribute("MatchedSpot")
    '                If TmpString <> "" Then
    '                    'On Error Resume Next
    '                    TmpActualSpot.MatchedSpot = mvarPlannedSpots(TmpString)
    '                    mvarPlannedSpots(TmpString).MatchedSpot = TmpActualSpot
    '                    'On Error GoTo ErrHandle
    '                End If
    '                TmpActualSpot.SpotLength = XMLSpot.GetAttribute("SpotLength")
    '                TmpActualSpot.Deactivated = XMLSpot.GetAttribute("Deactivated")
    '                TmpActualSpot.SpotType = XMLSpot.GetAttribute("SpotType")
    '                TmpActualSpot.BreakType = XMLSpot.GetAttribute("BreakType")
    '                TmpActualSpot.SecondRating = Conv(XMLSpot.GetAttribute("SecondRating"))
    '                TmpActualSpot.AdedgeChannel = XMLSpot.GetAttribute("AdEdgeChannel")
    '                TmpActualSpot.ID = XMLSpot.GetAttribute("ID")
    '                TmpString = XMLSpot.GetAttribute("BookingType")
    '                If TmpString <> "" Then
    '                    TmpActualSpot.Bookingtype = TmpActualSpot.Channel.BookingTypes(TmpString)
    '                Else
    '                    TmpActualSpot.Bookingtype = TmpActualSpot.Channel.BookingTypes(1)
    '                End If
    '                TmpActualSpot.week = TmpActualSpot.Bookingtype.GetWeek(Date.FromOADate(TmpActualSpot.AirDate))
    '                TmpActualSpot.GroupIdx = XMLSpot.GetAttribute("GroupIdx")
    '                XMLSpot = XMLSpot.NextSibling

    '            End While

    '            Dim TmpID As String
    '            Dim TmpAdjustBy As Byte
    '            Dim TmpDate As Date
    '            Dim TmpBT As String
    '            Dim TmpChannelEstimate As Single
    '            Dim TmpDBID As String
    '            Dim TmpFilmcode As String
    '            Dim TmpGrossPrice As Decimal
    '            Dim TmpMaM As Short
    '            Dim tmpMyEstimate As Single
    '            Dim TmpMyEstChanTarget As Single
    '            Dim TmpNetPrice As Decimal
    '            Dim TmpProgAfter As String
    '            Dim TmpProgBefore As String
    '            Dim TmpProg As String
    '            Dim TmpIsLocal As Boolean
    '            Dim TmpIsRB As Boolean

    '            mvarBookedSpots = New cBookedSpots(Me)

    '            XMLSpot = XMLCamp.GetElementsByTagName("BookedSpots").Item(0).FirstChild

    '            While Not XMLSpot Is Nothing
    '                TmpID = XMLSpot.GetAttribute("ID")
    '                TmpAdjustBy = Val(XMLSpot.GetAttribute("AdjustBy"))
    '                TmpDate = XMLSpot.GetAttribute("AirDate")
    '                TmpBT = XMLSpot.GetAttribute("Bookingtype")
    '                TmpString = XMLSpot.GetAttribute("Channel") ' Channel
    '                TmpChannelEstimate = CSng(Conv(XMLSpot.GetAttribute("ChannelEstimate")))
    '                TmpDBID = XMLSpot.GetAttribute("DatabaseID")
    '                TmpFilmcode = XMLSpot.GetAttribute("Filmcode")
    '                TmpGrossPrice = Val(XMLSpot.GetAttribute("GrossPrice"))
    '                TmpMaM = XMLSpot.GetAttribute("MaM")
    '                tmpMyEstimate = CSng(Conv(XMLSpot.GetAttribute("MyEstimate")))
    '                TmpMyEstChanTarget = CSng(Conv(XMLSpot.GetAttribute("MyEstimateBuyTarget")))
    '                TmpNetPrice = CDec(Conv(XMLSpot.GetAttribute("NetPrice")))
    '                TmpProgAfter = XMLSpot.GetAttribute("ProgAfter")
    '                TmpProgBefore = XMLSpot.GetAttribute("ProgBefore")
    '                TmpProg = XMLSpot.GetAttribute("Programme")
    '                TmpDBID = TmpString & Format(TmpDate, "yyyymmdd") & Left(Helper.Mam2Tid(TmpMaM), 2) & Right(Helper.Mam2Tid(TmpMaM), 2)
    '                If IsDBNull(XMLSpot.GetAttribute("IsLocal")) Then
    '                    TmpIsLocal = False
    '                    TmpIsRB = False
    '                Else
    '                    TmpIsLocal = XMLSpot.GetAttribute("IsLocal")
    '                    TmpIsRB = XMLSpot.GetAttribute("IsRB")
    '                End If
    '                mvarBookedSpots.Add(TmpID, TmpDBID, TmpString, TmpDate, TmpMaM, TmpProg, TmpProg, TmpProg, TmpGrossPrice, TmpNetPrice, TmpChannelEstimate, tmpMyEstimate, TmpMyEstChanTarget, TmpFilmcode, TmpBT, TmpIsLocal, TmpIsRB)
    '                If Not IsDBNull(XMLSpot.GetAttribute("Comments")) Then
    '                    mvarBookedSpots(TmpID).Comments = XMLSpot.GetAttribute("Comments")
    '                End If
    '                XMLSpot = XMLSpot.NextSibling
    '            End While

    '            mvarFilename = ""
    '            Loading = False

    '        End Sub

    '    End Class


    

    'Public Class cAddedValue
    '    Public Name As String
    '    Public IndexNet As Single
    '    Public IndexGross As Single
    '    Public ID As String
    '    Private mvarAmount(0 To 255) As Single
    '    Private Parent As cBookingType

    '    Public Property Amount(ByVal week) As Single
    '        Get
    '            Amount = mvarAmount(week)
    '        End Get
    '        Set(ByVal value As Single)
    '            mvarAmount(week) = value
    '            If Parent.Weeks(week).TRPControl Then
    '                Parent.Weeks(week).TRP = Parent.Weeks(week).TRP
    '            Else
    '                Parent.Weeks(week).NetBudget = Parent.Weeks(week).NetBudget
    '            End If
    '        End Set
    '    End Property

    '    Friend Property Bookingtype() As cBookingType
    '        Get
    '            Return Parent
    '        End Get
    '        Set(ByVal value As cBookingType)
    '            Parent = value
    '        End Set
    '    End Property

    'End Class

    '    Public Class cAddedValues
    '        Implements Collections.IEnumerable

    '        Private mCol As New Collection
    '        Private Parent As cBookingType

    '        Public WriteOnly Property Bookingtype()
    '            Set(ByVal value)
    '                Parent = value
    '            End Set
    '        End Property

    '        Public Function Add(ByVal Name As String, Optional ByVal ID As String = "") As cAddedValue

    '            Dim TmpAV As New cAddedValue

    '            If ID = "" Then ID = CreateGUID()
    '            TmpAV.ID = ID
    '            TmpAV.Name = Name
    '            TmpAV.Bookingtype = Parent

    '            mCol.Add(TmpAV, ID)
    '            Add = mCol(ID)

    '        End Function

    '        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cAddedValue
    '            Get
    '                Dim e As Long

    '                '    used when referencing an element in the collection
    '                '    vntIndexKey contains either the Index or Key to the collection,
    '                '    this is why it is declared as a Variant
    '                '    Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
    '                On Error GoTo Item_Error

    '                Item = mCol(vntIndexKey)

    '                On Error GoTo 0
    '                Exit Property

    'Item_Error:
    '                e = Err.Number

    '                Err.Raise(e, "cCosts", "Unknown Index (" & vntIndexKey & ")")
    '            End Get
    '        End Property

    '        Public ReadOnly Property Count() As Long
    '            'used when retrieving the number of elements in the
    '            'collection. Syntax: Debug.Print x.Count
    '            Get
    '                Count = mCol.Count
    '            End Get
    '        End Property

    '        Public Sub Remove(ByVal vntIndexKey As Object)
    '            'used when removing an element from the collection
    '            'vntIndexKey contains either the Index or Key, which is why
    '            'it is declared as a Variant
    '            'Syntax: x.Remove(xyz)


    '            mCol.Remove(vntIndexKey)
    '        End Sub

    '        Public Sub Clear()
    '            mCol = New Collection
    '        End Sub

    '        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
    '            GetEnumerator = mCol.GetEnumerator
    '        End Function

    '        Public Sub New()
    '            mCol = New Collection
    '        End Sub

    '        Protected Overrides Sub Finalize()
    '            mCol = Nothing
    '            MyBase.Finalize()
    '        End Sub

    '    End Class

    'Public Class cIndex

    '    Public Enum IndexOnEnum
    '        eNetCPP = 0
    '        eGrossCPP = 1
    '        eTRP = 2
    '    End Enum

    '    Public ID As String
    '    Private mvarIndexOn As IndexOnEnum
    '    Private mvarName As String
    '    Private mvarFromDate As Date
    '    Private mvarToDate As Date
    '    Private mvarIndex(0 To 6) As Single
    '    Private mvarIndexAllDay As Single
    '    Private mvarChannel As cChannel
    '    Private mvarSystemGenerated As Boolean

    '    Public Property IndexOn() As IndexOnEnum
    '        Get
    '            IndexOn = mvarIndexOn
    '        End Get
    '        Set(ByVal value As IndexOnEnum)
    '            mvarIndexOn = value
    '        End Set
    '    End Property

    '    Public Property Name() As String
    '        Get
    '            Name = mvarName
    '        End Get
    '        Set(ByVal value As String)
    '            mvarName = value
    '        End Set
    '    End Property

    '    Public Property FromDate() As Date
    '        Get
    '            FromDate = mvarFromDate
    '        End Get
    '        Set(ByVal value As Date)
    '            mvarFromDate = value
    '        End Set
    '    End Property

    '    Public Property ToDate() As Date
    '        Get
    '            ToDate = mvarToDate
    '        End Get
    '        Set(ByVal value As Date)
    '            mvarToDate = value
    '        End Set
    '    End Property

    '    Public Property Index(Optional ByVal Daypart As Integer = -1) As Single
    '        Get
    '            If Daypart = -1 OrElse mvarIndex(Daypart) = 0 Then
    '                Return mvarIndexAllDay
    '            Else
    '                Return mvarIndex(Daypart)
    '            End If
    '        End Get
    '        Set(ByVal value As Single)
    '            If Daypart = -1 Then
    '                mvarIndexAllDay = value
    '            Else
    '                mvarIndex(Daypart) = value
    '            End If
    '        End Set
    '    End Property

    '    Public Property SystemGenerated() As Boolean
    '        Get
    '            SystemGenerated = mvarSystemGenerated
    '        End Get
    '        Set(ByVal value As Boolean)
    '            mvarSystemGenerated = value
    '        End Set
    '    End Property

    '    Public Sub New()
    '        mvarSystemGenerated = False
    '    End Sub

    '    Protected Overrides Sub Finalize()
    '        mvarChannel = Nothing
    '        MyBase.Finalize()
    '    End Sub

    'End Class

    '    Public Class cIndexes
    '        Implements Collections.IEnumerable

    '        Private mCol As New Collection
    '        Private Main As cKampanj

    '        Public WriteOnly Property MainObject() As cKampanj
    '            Set(ByVal value As cKampanj)
    '                Main = value
    '            End Set
    '        End Property

    '        Public Function Add(ByVal Name As String, Optional ByVal ID As String = "") As cIndex

    '            Dim TmpIndex As New cIndex

    '            If ID = "" Then ID = CreateGUID()
    '            TmpIndex.ID = ID
    '            TmpIndex.Name = Name
    '            If Campaign.Channels.Count > 0 AndAlso Campaign.Channels(1).BookingTypes(1).Weeks.Count > 0 Then
    '                TmpIndex.FromDate = Date.FromOADate(Campaign.StartDate)
    '                TmpIndex.ToDate = Date.FromOADate(Campaign.EndDate)
    '            End If
    '            On Error GoTo 0
    '            mCol.Add(TmpIndex, ID)
    '            Add = mCol(ID)
    '        End Function

    '        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cIndex
    '            Get
    '                Dim e As Long

    '                '    used when referencing an element in the collection
    '                '    vntIndexKey contains either the Index or Key to the collection,
    '                '    this is why it is declared as a Variant
    '                '    Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
    '                On Error GoTo Item_Error

    '                Item = mCol(vntIndexKey)

    '                On Error GoTo 0
    '                Exit Property

    'Item_Error:
    '                e = Err.Number

    '                Err.Raise(e, "cCosts", "Unknown Index (" & vntIndexKey & ")")
    '            End Get
    '        End Property

    '        Public ReadOnly Property Count() As Long
    '            Get
    '                'used when retrieving the number of elements in the
    '                'collection. Syntax: Debug.Print x.Count
    '                Count = mCol.Count
    '            End Get
    '        End Property

    '        Public Sub Remove(ByVal vntIndexKey As Object)
    '            'used when removing an element from the collection
    '            'vntIndexKey contains either the Index or Key, which is why
    '            'it is declared as a Variant
    '            'Syntax: x.Remove(xyz)


    '            mCol.Remove(vntIndexKey)
    '        End Sub

    '        Public Function GetIndexForDate(ByVal WhatDate As Date, ByVal IndexType As cIndex.IndexOnEnum, Optional ByVal Daypart As Integer = -1) As Single
    '            Dim TmpIndex As cIndex
    '            Dim Idx As Single

    '            Idx = 100
    '            For Each TmpIndex In mCol
    '                If TmpIndex.IndexOn = IndexType Then
    '                    If TmpIndex.FromDate <= WhatDate Then
    '                        If TmpIndex.ToDate >= WhatDate Then
    '                            Idx = Idx * (TmpIndex.Index(Daypart) / 100)
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            Return Idx
    '        End Function

    '        Public Sub Clear()
    '            mCol = New Collection
    '        End Sub

    '        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
    '            GetEnumerator = mCol.GetEnumerator
    '        End Function

    '        Public Sub New(ByRef MainObject As cKampanj)
    '            Main = MainObject
    '            mCol = New Collection
    '        End Sub

    '        Protected Overrides Sub Finalize()
    '            mCol = Nothing
    '            MyBase.Finalize()
    '        End Sub

    '    End Class

    'Public Class clsIni
    '    ' This class is a *.INI file reader object




    '    <DllImport("KERNEL32.DLL", EntryPoint:="GetPrivateProfileIntA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    '    Private Shared Function GetPrivateProfileInt(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal nDefault As Integer, ByVal lpFileName As String) As Integer
    '    End Function
    '    <DllImport("KERNEL32.DLL", EntryPoint:="WritePrivateProfileStringA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    '    Private Shared Function WritePrivateProfileString(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    '    End Function
    '    <DllImport("KERNEL32.DLL", EntryPoint:="GetPrivateProfileStringA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    '    Private Shared Function GetPrivateProfileString(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    '    End Function
    '    <DllImport("KERNEL32.DLL", EntryPoint:="WritePrivateProfileStructA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    '    Private Shared Function WritePrivateProfileStruct(ByVal lpszSection As String, ByVal lpszKey As String, ByVal lpStruct() As Byte, ByVal uSizeStruct As Integer, ByVal szFile As String) As Integer
    '    End Function
    '    <DllImport("KERNEL32.DLL", EntryPoint:="GetPrivateProfileStructA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    '    Private Shared Function GetPrivateProfileStruct(ByVal lpszSection As String, ByVal lpszKey As String, ByVal lpStruct() As Byte, ByVal uSizeStruct As Integer, ByVal szFile As String) As Integer
    '    End Function
    '    <DllImport("KERNEL32.DLL", EntryPoint:="GetPrivateProfileSectionNamesA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    '    Private Shared Function GetPrivateProfileSectionNames(ByVal lpszReturnBuffer() As Byte, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    '    End Function
    '    <DllImport("KERNEL32.DLL", EntryPoint:="WritePrivateProfileSectionA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    '    Private Shared Function WritePrivateProfileSection(ByVal lpAppName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    '    End Function
    '    ' 
    '    '/// Constructs a new INIReader object.
    '    '/// Specifies the INI file to use.
    '    Public Sub Create(ByVal File As String)
    '        Filename = File
    '    End Sub
    '    '/// Specifies the INI file to use.
    '    '/// A String representing the full path of the INI file.
    '    Public Property Filename() As String
    '        Get
    '            Return m_Filename
    '        End Get
    '        Set(ByVal Value As String)
    '            m_Filename = Value
    '        End Set
    '    End Property
    '    '/// Specifies the section you're working in. (aka 'the active section')
    '    '/// A String representing the section you're working in.
    '    Public Property Section() As String
    '        Get
    '            Return m_Section
    '        End Get
    '        Set(ByVal Value As String)
    '            m_Section = Value
    '        End Set
    '    End Property
    '    '/// Reads an Integer from the specified key of the specified section.
    '    '/// Specifies the section to search in.
    '    '/// Specifies the key from which to return the value.
    '    '/// Specifies the value to return if the specified key isn't found.
    '    '/// Returns the value of the specified Section/Key pair, or returns the default value if the specified Section/Key pair isn't found in the INI file.
    '    Public Overloads Function ReadInteger(ByVal Section As String, ByVal Key As String, ByVal DefVal As Integer) As Integer
    '        Try
    '            Return GetPrivateProfileInt(Section, Key, DefVal, Filename)
    '        Catch
    '            Return DefVal
    '        End Try
    '    End Function
    '    '/// Reads an Integer from the specified key of the specified section.
    '    '/// Specifies the section to search in.
    '    '/// Specifies the key from which to return the value.
    '    '/// Returns the value of the specified Section/Key pair, or returns 0 if the specified Section/Key pair isn't found in the INI file.
    '    Public Overloads Function ReadInteger(ByVal Section As String, ByVal Key As String) As Integer
    '        Return ReadInteger(Section, Key, 0)
    '    End Function
    '    '/// Reads an Integer from the specified key of the active section.
    '    '/// Specifies the key from which to return the value.
    '    '/// Specifies the section to search in.
    '    '/// Returns the value of the specified Key, or returns the default value if the specified Key isn't found in the active section of the INI file.
    '    Public Overloads Function ReadInteger(ByVal Key As String, ByVal DefVal As Integer) As Integer
    '        Return ReadInteger(Section, Key, DefVal)
    '    End Function
    '    '/// Reads an Integer from the specified key of the active section.
    '    '/// Specifies the key from which to return the value.
    '    '/// Returns the value of the specified Key, or returns 0 if the specified Key isn't found in the active section of the INI file.
    '    Public Overloads Function ReadInteger(ByVal Key As String) As Integer
    '        Return ReadInteger(Key, 0)
    '    End Function
    '    '/// Reads a String from the specified key of the specified section.
    '    '/// Specifies the section to search in.
    '    '/// Specifies the key from which to return the value.
    '    '/// Specifies the value to return if the specified key isn't found.
    '    '/// Returns the value of the specified Section/Key pair, or returns the default value if the specified Section/Key pair isn't found in the INI file.
    '    Public Overloads Function ReadString(ByVal Section As String, ByVal Key As String, ByVal DefVal As String) As String
    '        Try
    '            Dim sb As New StringBuilder(MAX_ENTRY)
    '            Dim Ret As Integer = GetPrivateProfileString(Section, Key, DefVal, sb, MAX_ENTRY, Filename)
    '            Return sb.ToString
    '        Catch
    '            Return DefVal
    '        End Try
    '    End Function
    '    '/// Reads a String from the specified key of the specified section.
    '    '/// Specifies the section to search in.
    '    '/// Specifies the key from which to return the value.
    '    '/// Returns the value of the specified Section/Key pair, or returns an empty String if the specified Section/Key pair isn't found in the INI file.
    '    Public Overloads Function ReadString(ByVal Section As String, ByVal Key As String) As String
    '        Return ReadString(Section, Key, "")
    '    End Function
    '    '/// Reads a String from the specified key of the active section.
    '    '/// Specifies the key from which to return the value.
    '    '/// Returns the value of the specified Key, or returns an empty String if the specified Key isn't found in the active section of the INI file.
    '    Public Overloads Function ReadString(ByVal Key As String) As String
    '        Return ReadString(Section, Key)
    '    End Function
    '    '/// Reads a Long from the specified key of the specified section.
    '    '/// Specifies the section to search in.
    '    '/// Specifies the key from which to return the value.
    '    '/// Specifies the value to return if the specified key isn't found.
    '    '/// Returns the value of the specified Section/Key pair, or returns the default value if the specified Section/Key pair isn't found in the INI file.
    '    Public Overloads Function ReadLong(ByVal Section As String, ByVal Key As String, ByVal DefVal As Long) As Long
    '        Return Long.Parse(ReadString(Section, Key, DefVal.ToString))
    '    End Function
    '    '/// Reads a Long from the specified key of the specified section.
    '    '/// Specifies the section to search in.
    '    '/// Specifies the key from which to return the value.
    '    '/// Returns the value of the specified Section/Key pair, or returns 0 if the specified Section/Key pair isn't found in the INI file.
    '    Public Overloads Function ReadLong(ByVal Section As String, ByVal Key As String) As Long
    '        Return ReadLong(Section, Key, 0)
    '    End Function
    '    '/// Reads a Long from the specified key of the active section.
    '    '/// Specifies the key from which to return the value.
    '    '/// Specifies the section to search in.
    '    '/// Returns the value of the specified Key, or returns the default value if the specified Key isn't found in the active section of the INI file.
    '    Public Overloads Function ReadLong(ByVal Key As String, ByVal DefVal As Long) As Long
    '        Return ReadLong(Section, Key, DefVal)
    '    End Function
    '    '/// Reads a Long from the specified key of the active section.
    '    '/// Specifies the key from which to return the value.
    '    '/// Returns the value of the specified Key, or returns 0 if the specified Key isn't found in the active section of the INI file.
    '    Public Overloads Function ReadLong(ByVal Key As String) As Long
    '        Return ReadLong(Key, 0)
    '    End Function
    '    '/// Reads a Byte array from the specified key of the specified section.
    '    '/// Specifies the section to search in.
    '    '/// Specifies the key from which to return the value.
    '    '/// Specifies the value to return if the specified key isn't found.
    '    '/// Returns the value of the specified Section/Key pair, or returns Nothing (Null in C#, C++.NET) if the specified Section/Key pair isn't found in the INI file.
    '    Public Function ReadByteArray(ByVal Section As String, ByVal Key As String, ByVal Length As Integer) As Byte()
    '        If Length > 0 Then
    '            Try
    '                Dim Buffer(Length - 1) As Byte
    '                If GetPrivateProfileStruct(Section, Key, Buffer, Buffer.Length, Filename) = 0 Then
    '                    Return Nothing
    '                Else
    '                    Return Buffer
    '                End If
    '            Catch
    '                Return Nothing
    '            End Try
    '        Else
    '            Return Nothing
    '        End If
    '    End Function
    '    '/// Reads a Byte array from the specified key of the active section.
    '    '/// Specifies the key from which to return the value.
    '    '/// Specifies the value to return if the specified key isn't found.
    '    '/// Returns the value of the specified Key, or returns Nothing (Null in C#, C++.NET) if the specified Key pair isn't found in the active section of the INI file.
    '    Public Function ReadByteArray(ByVal Key As String, ByVal Length As Integer) As Byte()
    '        Return ReadByteArray(Section, Key, Length)
    '    End Function
    '    '/// Reads a Boolean from the specified key of the specified section.
    '    '/// Specifies the section to search in.
    '    '/// Specifies the key from which to return the value.
    '    '/// Specifies the value to return if the specified key isn't found.
    '    '/// Returns the value of the specified Section/Key pair, or returns the default value if the specified Section/Key pair isn't found in the INI file.
    '    Public Overloads Function ReadBoolean(ByVal Section As String, ByVal Key As String, ByVal DefVal As Boolean) As Boolean
    '        Return Boolean.Parse(ReadString(Section, Key, DefVal.ToString))
    '    End Function
    '    '/// Reads a Boolean from the specified key of the specified section.
    '    '/// Specifies the section to search in.
    '    '/// Specifies the key from which to return the value.
    '    '/// Returns the value of the specified Section/Key pair, or returns False if the specified Section/Key pair isn't found in the INI file.
    '    Public Overloads Function ReadBoolean(ByVal Section As String, ByVal Key As String) As Boolean
    '        Return ReadBoolean(Section, Key, False)
    '    End Function
    '    '/// Reads a Boolean from the specified key of the specified section.
    '    '/// Specifies the key from which to return the value.
    '    '/// Specifies the value to return if the specified key isn't found.
    '    '/// Returns the value of the specified Key pair, or returns the default value if the specified Key isn't found in the active section of the INI file.
    '    Public Overloads Function ReadBoolean(ByVal Key As String, ByVal DefVal As Boolean) As Boolean
    '        Return ReadBoolean(Section, Key, DefVal)
    '    End Function
    '    '/// Reads a Boolean from the specified key of the specified section.
    '    '/// Specifies the key from which to return the value.
    '    '/// Returns the value of the specified Key, or returns False if the specified Key isn't found in the active section of the INI file.
    '    Public Overloads Function ReadBoolean(ByVal Key As String) As Boolean
    '        Return ReadBoolean(Section, Key)
    '    End Function
    '    '/// Writes an Integer to the specified key in the specified section.
    '    '/// Specifies the section to write in.
    '    '/// Specifies the key to write to.
    '    '/// Specifies the value to write.
    '    '/// Returns True if the function succeeds, False otherwise.
    '    Public Overloads Function Write(ByVal Section As String, ByVal Key As String, ByVal Value As Integer) As Boolean
    '        Try
    '            Return (WritePrivateProfileString(Section, Key, Value.ToString, Filename) <> 0)
    '        Catch
    '            Return False
    '        End Try
    '    End Function
    '    '/// Writes an Integer to the specified key in the active section.
    '    '/// Specifies the key to write to.
    '    '/// Specifies the value to write.
    '    '/// Returns True if the function succeeds, False otherwise.
    '    Public Overloads Function Write(ByVal Key As String, ByVal Value As Integer) As Boolean
    '        Return Write(Section, Key, Value)
    '    End Function
    '    '/// Writes a String to the specified key in the specified section.
    '    '/// Specifies the section to write in.
    '    '/// Specifies the key to write to.
    '    '/// Specifies the value to write.
    '    '/// Returns True if the function succeeds, False otherwise.
    '    Public Overloads Function Write(ByVal Section As String, ByVal Key As String, ByVal Value As String) As Boolean
    '        Try
    '            Return (WritePrivateProfileString(Section, Key, Value, Filename) <> 0)
    '        Catch
    '            Return False
    '        End Try
    '    End Function
    '    '/// Writes a String to the specified key in the active section.
    '    '/// Specifies the key to write to.
    '    '/// Specifies the value to write.
    '    '/// Returns True if the function succeeds, False otherwise.
    '    Public Overloads Function Write(ByVal Key As String, ByVal Value As String) As Boolean
    '        Return Write(Section, Key, Value)
    '    End Function
    '    '/// Writes a Long to the specified key in the specified section.
    '    '/// Specifies the section to write in.
    '    '/// Specifies the key to write to.
    '    '/// Specifies the value to write.
    '    '/// Returns True if the function succeeds, False otherwise.
    '    Public Overloads Function Write(ByVal Section As String, ByVal Key As String, ByVal Value As Long) As Boolean
    '        Return Write(Section, Key, Value.ToString)
    '    End Function
    '    '/// Writes a Long to the specified key in the active section.
    '    '/// Specifies the key to write to.
    '    '/// Specifies the value to write.
    '    '/// Returns True if the function succeeds, False otherwise.
    '    Public Overloads Function Write(ByVal Key As String, ByVal Value As Long) As Boolean
    '        Return Write(Section, Key, Value)
    '    End Function
    '    '/// Writes a Byte array to the specified key in the specified section.
    '    '/// Specifies the section to write in.
    '    '/// Specifies the key to write to.
    '    '/// Specifies the value to write.
    '    '/// Returns True if the function succeeds, False otherwise.
    '    Public Overloads Function Write(ByVal Section As String, ByVal Key As String, ByVal Value() As Byte) As Boolean
    '        Try
    '            Return (WritePrivateProfileStruct(Section, Key, Value, Value.Length, Filename) <> 0)
    '        Catch
    '            Return False
    '        End Try
    '    End Function
    '    '/// Writes a Byte array to the specified key in the active section.
    '    '/// Specifies the key to write to.
    '    '/// Specifies the value to write.
    '    '/// Returns True if the function succeeds, False otherwise.
    '    Public Overloads Function Write(ByVal Key As String, ByVal Value() As Byte) As Boolean
    '        Return Write(Section, Key, Value)
    '    End Function
    '    '/// Writes a Boolean to the specified key in the specified section.
    '    '/// Specifies the section to write in.
    '    '/// Specifies the key to write to.
    '    '/// Specifies the value to write.
    '    '/// Returns True if the function succeeds, False otherwise.
    '    Public Overloads Function Write(ByVal Section As String, ByVal Key As String, ByVal Value As Boolean) As Boolean
    '        Return Write(Section, Key, Value.ToString)
    '    End Function
    '    '/// Writes a Boolean to the specified key in the active section.
    '    '/// Specifies the key to write to.
    '    '/// Specifies the value to write.
    '    '/// Returns True if the function succeeds, False otherwise.
    '    Public Overloads Function Write(ByVal Key As String, ByVal Value As Boolean) As Boolean
    '        Return Write(Section, Key, Value)
    '    End Function
    '    '/// Deletes a key from the specified section.
    '    '/// Specifies the section to delete from.
    '    '/// Specifies the key to delete.
    '    '/// Returns True if the function succeeds, False otherwise.
    '    Public Function DeleteKey(ByVal Section As String, ByVal Key As String) As Boolean
    '        Try
    '            Return (WritePrivateProfileString(Section, Key, Nothing, Filename) <> 0)
    '        Catch
    '            Return False
    '        End Try
    '    End Function
    '    '/// Deletes a key from the active section.
    '    '/// Specifies the key to delete.
    '    '/// Returns True if the function succeeds, False otherwise.
    '    Public Function DeleteKey(ByVal Key As String) As Boolean
    '        Try
    '            Return (WritePrivateProfileString(Section, Key, Nothing, Filename) <> 0)
    '        Catch
    '            Return False
    '        End Try
    '    End Function
    '    '/// Deletes a section from an INI file.
    '    '/// Specifies the section to delete.
    '    '/// Returns True if the function succeeds, False otherwise.
    '    Public Function DeleteSection(ByVal Section As String) As Boolean
    '        Try
    '            Return WritePrivateProfileSection(Section, Nothing, Filename) <> 0
    '        Catch
    '            Return False
    '        End Try
    '    End Function
    '    '/// Retrieves a list of all available sections in the INI file.
    '    '/// Returns an ArrayList with all available sections.
    '    Public Function GetSectionNames() As ArrayList
    '        GetSectionNames = New ArrayList
    '        Dim Buffer(MAX_ENTRY) As Byte
    '        Dim BuffStr As String
    '        Dim PrevPos As Integer = 0
    '        Dim Length As Integer
    '        Try
    '            Length = GetPrivateProfileSectionNames(Buffer, MAX_ENTRY, Filename)
    '        Catch
    '            Exit Function
    '        End Try
    '        Dim ASCII As New ASCIIEncoding
    '        If Length > 0 Then
    '            BuffStr = ASCII.GetString(Buffer)
    '            Length = 0
    '            PrevPos = -1
    '            Do
    '                Length = BuffStr.IndexOf(ControlChars.NullChar, PrevPos + 1)
    '                If Length - PrevPos = 1 OrElse Length = -1 Then Exit Do
    '                Try
    '                    GetSectionNames.Add(BuffStr.Substring(PrevPos + 1, Length - PrevPos))
    '                Catch
    '                End Try
    '                PrevPos = Length
    '            Loop
    '        End If
    '    End Function

    '    Public Property Text(ByVal sSection As String, ByVal sKey As String) As String
    '        Get
    '            Return ReadString(sSection, sKey, "")
    '        End Get
    '        Set(ByVal value As String)
    '            Write(sSection, sKey, value)
    '        End Set
    '    End Property

    '    Public Property Data(ByVal sSection As String, ByVal sKey As String) As Integer
    '        Get
    '            Return ReadInteger(sSection, sKey, -1)
    '        End Get
    '        Set(ByVal value As Integer)
    '            Write(sSection, sKey, value)
    '        End Set
    '    End Property

    '    'Private variables and constants
    '    Private m_Filename As String
    '    Private m_Section As String
    '    Private Const MAX_ENTRY As Integer = 32768
    'End Class

    'Class cSettings

    '    'Inifiles
    '    Private TrinityIni As New clsIni
    '    Private NetworkIni As New clsIni
    '    Private Main As cKampanj

    '    'Variables
    '    Public Enum SettingsLocationEnum
    '        locLocal = 0
    '        locNetwork = 1
    '    End Enum

    '    Private mvarLocalDataPath As String

    '    Public ReadOnly Property GeneralMedia()
    '        Get
    '            Return NetworkIni.Text("Marathon", "GeneralMedia")
    '        End Get
    '    End Property

    '    Public Property LogoAlignment() As Integer
    '        Get
    '            If TrinityIni.Data("Settings", "LogoAlignment") = -1 Then
    '                Return 1
    '            Else
    '                Return TrinityIni.Data("Settings", "LogoAlignment")
    '            End If
    '        End Get
    '        Set(ByVal value As Integer)
    '            TrinityIni.Data("Settings", "LogoAlignment") = value
    '        End Set
    '    End Property

    '    Public WriteOnly Property MainObject() As cKampanj
    '        Set(ByVal value As cKampanj)
    '            Main = value
    '        End Set
    '    End Property

    '    Public Property LocalDataPath() As String
    '        Get
    '            Return mvarLocalDataPath
    '        End Get
    '        Set(ByVal value As String)
    '            mvarLocalDataPath = value
    '            Setup()
    '        End Set
    '    End Property

    '    Public ReadOnly Property StartMode() As Integer
    '        Get
    '            StartMode = TrinityIni.Data("Startup", "StartMode")
    '        End Get
    '    End Property

    '    Public Property DataPath(Optional ByVal Where As SettingsLocationEnum = SettingsLocationEnum.locNetwork) As String
    '        Get

    '            If Where = SettingsLocationEnum.locNetwork Then
    '                DataPath = Helper.Pathify(TrinityIni.Text("Paths", "DataPath"))
    '            Else
    '                DataPath = Helper.Pathify(Helper.Pathify(LocalDataPath) & "Data")
    '            End If
    '            If Not My.Computer.FileSystem.DirectoryExists(DataPath) Then
    '                DataPath = Helper.Pathify(Helper.Pathify(LocalDataPath) & "Data")
    '            End If
    '        End Get
    '        Set(ByVal value As String)
    '            If Where = SettingsLocationEnum.locNetwork Then
    '                TrinityIni.Text("Paths", "DataPath") = value
    '            End If
    '        End Set
    '    End Property

    '    Public Function DataBase(ByVal Where As SettingsLocationEnum) As String
    '        If Where = SettingsLocationEnum.locNetwork Then
    '            DataBase = TrinityIni.Text("NetworkDB", "DB")
    '        Else
    '            DataBase = TrinityIni.Text("LocalDB", "DB")
    '        End If
    '    End Function

    '    Public Property Debug() As Boolean
    '        Get
    '            Return TrinityIni.Data("General", "Debug")
    '        End Get
    '        Set(ByVal value As Boolean)
    '            TrinityIni.Data("General", "Debug") = value
    '        End Set
    '    End Property

    '    Public Function ConnectionString(ByVal Where As SettingsLocationEnum) As String
    '        If Where = SettingsLocationEnum.locNetwork Then
    '            Return TrinityIni.Text("NetworkDB", "Connection")
    '        Else
    '            Return TrinityIni.Text("LocalDB", "Connection")
    '        End If
    '    End Function

    '    Public ReadOnly Property DefaultArea() As String
    '        Get
    '            DefaultArea = NetworkIni.Text("Locale", "Area")
    '        End Get
    '    End Property

    '    Public ReadOnly Property DefaultAreaLog() As String
    '        Get
    '            DefaultAreaLog = NetworkIni.Text("Locale", "AreaLog")
    '        End Get
    '    End Property

    '    Public ReadOnly Property DefaultCPTDropdown() As String
    '        Get
    '            If TrinityIni.Text("Preferences", "DefaultCPTDropdown") = "" Then
    '                TrinityIni.Text("Preferences", "DefaultCPTDropdown") = "CPT"
    '            End If
    '            DefaultCPTDropdown = TrinityIni.Text("Preferences", "DefaultCPTDropdown")
    '        End Get
    '    End Property

    '    Public ReadOnly Property AreaCount() As Integer
    '        Get
    '            AreaCount = NetworkIni.Data("Locale", "AreaCount")
    '        End Get
    '    End Property

    '    Public ReadOnly Property Area(ByVal Index As Integer) As String
    '        Get
    '            Area = NetworkIni.Text("Locale", "Area" & Index)
    '        End Get
    '    End Property

    '    Public ReadOnly Property AreaName(ByVal Index As Integer) As String
    '        Get
    '            AreaName = NetworkIni.Text("Locale", "AreaName" & Index)
    '        End Get
    '    End Property

    '    Public ReadOnly Property AreaName(ByVal Area As String) As String
    '        Get
    '            Dim TmpIni As New clsIni

    '            TmpIni.Create(DataPath(SettingsLocationEnum.locNetwork) & Campaign.Area & "\Area.ini")
    '            Return TmpIni.Text("General", "Areaname")
    '        End Get
    '    End Property

    '    Public Property TopPercent() As Single
    '        Get
    '            If TrinityIni.Text("Booking", "TopPercent") <> "" Then
    '                TopPercent = TrinityIni.Text("Booking", "TopPercent")
    '            Else
    '                TopPercent = 0.5
    '            End If
    '        End Get
    '        Set(ByVal value As Single)
    '            TrinityIni.Text("Booking", "TopPercent") = value
    '        End Set
    '    End Property

    '    Public Property SummaryWidth() As Single
    '        Get
    '            If TrinityIni.Text("Booking", "SummaryWidth") = "" Then
    '                SummaryWidth = 3315
    '            Else
    '                SummaryWidth = TrinityIni.Text("Booking", "SummaryWidth")
    '            End If
    '        End Get
    '        Set(ByVal value As Single)
    '            TrinityIni.Text("Booking", "SummaryWidth") = value
    '        End Set
    '    End Property

    '    Public Property BookingColumnCount() As Integer
    '        Get
    '            BookingColumnCount = TrinityIni.Data("Columns", "Count")
    '        End Get
    '        Set(ByVal value As Integer)
    '            TrinityIni.Data("Columns", "Count") = value
    '        End Set
    '    End Property

    '    Public Property BookingColumn(ByVal Index As Integer) As String
    '        Get
    '            BookingColumn = TrinityIni.Text("Columns", "Column" & Index)
    '        End Get
    '        Set(ByVal value As String)
    '            TrinityIni.Text("Columns", "Column" & Index) = value
    '        End Set
    '    End Property

    '    Public Property BookingColumnWidth(ByVal Index As Integer) As Single
    '        Get
    '            BookingColumnWidth = TrinityIni.Data("ColumnWidth", TrinityIni.Text("Columns", "Column" & Index))
    '        End Get
    '        Set(ByVal value As Single)
    '            TrinityIni.Data("ColumnWidth", TrinityIni.Text("Columns", "Column" & Index)) = value
    '        End Set
    '    End Property

    '    Public Property SpotlistColumnCount() As Integer
    '        Get
    '            SpotlistColumnCount = TrinityIni.Data("SpotlistColumns", "Count")
    '        End Get
    '        Set(ByVal value As Integer)
    '            TrinityIni.Data("SpotlistColumns", "Count") = value
    '        End Set
    '    End Property

    '    Public Property SpotlistColumn(ByVal Index As Integer) As String
    '        Get
    '            SpotlistColumn = TrinityIni.Text("SpotlistColumns", "Column" & Index)
    '        End Get
    '        Set(ByVal value As String)
    '            TrinityIni.Text("SpotlistColumns", "Column" & Index) = value
    '        End Set
    '    End Property

    '    Public Property SpotlistColumnWidth(ByVal Index As Integer) As Single
    '        Get
    '            SpotlistColumnWidth = TrinityIni.Data("SpotlistColumnWidth", TrinityIni.Text("SpotlistColumns", "Column" & Index))
    '        End Get
    '        Set(ByVal value As Single)
    '            TrinityIni.Data("SpotlistColumnWidth", TrinityIni.Text("SpotlistColumns", "Column" & Index)) = value
    '        End Set
    '    End Property

    '    Public Property ConfirmedColumnCount() As Integer
    '        Get
    '            ConfirmedColumnCount = TrinityIni.Data("ConfirmedColumns", "Count")
    '        End Get
    '        Set(ByVal value As Integer)
    '            TrinityIni.Data("ConfirmedColumns", "Count") = value
    '        End Set
    '    End Property

    '    Public Property ConfirmedColumn(ByVal Index As Integer) As String
    '        Get
    '            Return TrinityIni.Text("ConfirmedColumns", "Column" & Index)
    '        End Get
    '        Set(ByVal value As String)
    '            TrinityIni.Text("ConfirmedColumns", "Column" & Index) = value
    '        End Set
    '    End Property

    '    Public Property ConfirmedColumnWidth(ByVal Index As Integer) As Single
    '        Get
    '            ConfirmedColumnWidth = TrinityIni.Data("ConfirmedColumnWidth", TrinityIni.Text("ConfirmedColumns", "Column" & Index))
    '        End Get
    '        Set(ByVal value As Single)
    '            TrinityIni.Data("ConfirmedColumnWidth", TrinityIni.Text("ConfirmedColumns", "Column" & Index)) = value
    '        End Set
    '    End Property

    '    Public Property ActualColumnCount() As Integer
    '        Get
    '            ActualColumnCount = TrinityIni.Data("ActualColumns", "Count")
    '        End Get
    '        Set(ByVal value As Integer)
    '            TrinityIni.Data("ActualColumns", "Count") = value
    '        End Set
    '    End Property

    '    Public Property ActualColumn(ByVal Index As Integer) As String
    '        Get
    '            ActualColumn = TrinityIni.Text("ActualColumns", "Column" & Index)
    '        End Get
    '        Set(ByVal value As String)
    '            TrinityIni.Text("ActualColumns", "Column" & Index) = value
    '        End Set
    '    End Property

    '    Public Property ActualColumnWidth(ByVal Index As Integer) As Single
    '        Get
    '            ActualColumnWidth = TrinityIni.Data("ActualColumnWidth", TrinityIni.Text("ActualColumns", "Column" & Index))
    '        End Get
    '        Set(ByVal value As Single)
    '            TrinityIni.Data("ActualColumnWidth", TrinityIni.Text("ActualColumns", "Column" & Index)) = value
    '        End Set
    '    End Property

    '    Public Property PrintColumnCount() As Integer
    '        Get
    '            PrintColumnCount = TrinityIni.Data("PrintColumns", "Count")
    '        End Get
    '        Set(ByVal value As Integer)
    '            TrinityIni.Data("PrintColumns", "Count") = value
    '        End Set
    '    End Property

    '    Public Property PrintColumn(ByVal Index As Integer) As String
    '        Get
    '            PrintColumn = TrinityIni.Text("PrintColumns", "Column" & Index)
    '        End Get
    '        Set(ByVal value As String)
    '            TrinityIni.Text("PrintColumns", "Column" & Index) = value
    '        End Set
    '    End Property

    '    Public Property PrintColumnWidth(ByVal Index As Integer) As Single
    '        Get
    '            PrintColumnWidth = TrinityIni.Data("SpotlsitColumnWidth", TrinityIni.Text("PrintColumns", "Column" & Index))
    '        End Get
    '        Set(ByVal value As Single)
    '            TrinityIni.Data("PrintColumnWidth", TrinityIni.Text("PrintColumns", "Column" & Index)) = value
    '        End Set
    '    End Property

    '    Public ReadOnly Property PPFirst() As Integer
    '        Get
    '            If NetworkIni.Text("Settings", "PPFirst") = "" Then
    '                PPFirst = 1
    '            Else
    '                PPFirst = NetworkIni.Text("Settings", "PPFirst")
    '            End If
    '        End Get
    '    End Property

    '    Public ReadOnly Property PPLast() As Integer
    '        Get
    '            If NetworkIni.Text("Settings", "PPLast") = "" Then
    '                PPLast = 1
    '            Else
    '                PPLast = NetworkIni.Text("Settings", "PPLast")
    '            End If
    '        End Get
    '    End Property

    '    Public ReadOnly Property ShowBought() As Boolean
    '        Get
    '            ShowBought = TrinityIni.Data("Settings", "ShowBought")
    '        End Get
    '    End Property

    '    Public ReadOnly Property MatchFilmcode() As Boolean
    '        Get
    '            MatchFilmcode = TrinityIni.Data("Settings", "MatchFilmcode")
    '        End Get
    '    End Property

    '    Public Property ShowInfoInWindow() As Boolean
    '        Get
    '            ShowInfoInWindow = TrinityIni.Data("Settings", "ShowInfoInWindow")
    '        End Get
    '        Set(ByVal value As Boolean)
    '            TrinityIni.Data("Settings", "ShowInfoInWindow") = value

    '        End Set
    '    End Property

    '    Public Function AllAdults() As String
    '        Dim TmpIni As New clsIni

    '        TmpIni.Create(DataPath(SettingsLocationEnum.locNetwork) & Campaign.Area & "\Area.ini")
    '        AllAdults = TmpIni.Text("General", "EntirePopulation")

    '    End Function

    '    Public Function MarathonCommand() As String
    '        MarathonCommand = NetworkIni.Text("Marathon", "Command")
    '    End Function

    '    Public Function MarathonUseSpotcount() As Boolean
    '        Return NetworkIni.Data("Marathon", "UseSpotCount")
    '    End Function

    '    Public Function MarathonDiscountCode() As String
    '        If NetworkIni.Text("Marathon", "DiscountCode") = "" Then
    '            Return "VO"
    '        Else
    '            Return NetworkIni.Text("Marathon", "DiscountCode")
    '        End If
    '    End Function

    '    Public Property ChannelContact(ByVal Channel As String) As String
    '        Get
    '            ChannelContact = TrinityIni.Text("Contacts", Channel)
    '        End Get
    '        Set(ByVal value As String)
    '            TrinityIni.Text("Contacts", Channel) = value
    '        End Set
    '    End Property

    '    Public Property ColorSchemeCount() As Integer
    '        Get
    '            ColorSchemeCount = NetworkIni.Data("ColorSchemes", "Count")
    '        End Get
    '        Set(ByVal value As Integer)
    '            NetworkIni.Data("ColorSchemes", "Count") = value
    '        End Set
    '    End Property

    '    Public Property ColorScheme(ByVal Index) As String
    '        Get
    '            ColorScheme = NetworkIni.Text("ColorScheme" & Index, "Name")
    '        End Get
    '        Set(ByVal value As String)
    '            NetworkIni.Text("ColorScheme" & Index, "Name") = value
    '        End Set
    '    End Property

    '    Public Property SchemeFont(ByVal index) As String
    '        Get
    '            If NetworkIni.Text("ColorScheme" & index, "Font") = "" Then
    '                Return "Segoe UI"
    '            Else
    '                Return NetworkIni.Text("ColorScheme" & index, "Font")
    '            End If
    '        End Get
    '        Set(ByVal value As String)
    '            NetworkIni.Text("ColorScheme" & index, "Font") = value
    '        End Set
    '    End Property

    '    Public Property Color(ByVal Index As Integer, ByVal Var As String) As Long
    '        Get
    '            Color = Val(NetworkIni.Text("ColorScheme" & Index, Var))
    '        End Get
    '        Set(ByVal value As Long)
    '            NetworkIni.Text("ColorScheme" & Index, Var) = value
    '        End Set
    '    End Property

    '    Public Property DefaultColorScheme() As Integer
    '        Get
    '            DefaultColorScheme = Val(TrinityIni.Text("Preferences", "ColorScheme"))
    '        End Get
    '        Set(ByVal value As Integer)
    '            TrinityIni.Text("Preferences", "ColorScheme") = value
    '        End Set
    '    End Property

    '    Public Property DefaultLogo() As Integer
    '        Get
    '            DefaultLogo = Val(TrinityIni.Text("Preferences", "Logo"))
    '        End Get
    '        Set(ByVal value As Integer)
    '            TrinityIni.Text("Preferences", "Logo") = value
    '        End Set
    '    End Property

    '    Public Property DefaultLogoPlacement() As Integer
    '        Get
    '            DefaultLogoPlacement = Val(TrinityIni.Text("Preferences", "LogoPlacement"))
    '        End Get
    '        Set(ByVal value As Integer)
    '            TrinityIni.Text("Preferences", "LogoPlacement") = value
    '        End Set
    '    End Property

    '    Public Property DefaultLanguage() As Integer
    '        Get
    '            DefaultLanguage = Val(TrinityIni.Text("Preferences", "Language"))
    '        End Get
    '        Set(ByVal value As Integer)
    '            TrinityIni.Text("Preferences", "Language") = value
    '        End Set
    '    End Property

    '    Public Property UserName() As String
    '        Get
    '            UserName = TrinityIni.Text("ID", "UserName")
    '        End Get
    '        Set(ByVal value As String)
    '            TrinityIni.Text("ID", "UserName") = value
    '        End Set
    '    End Property

    '    Public Property UserEmail() As String
    '        Get
    '            UserEmail = TrinityIni.Text("ID", "UserEmail")
    '        End Get
    '        Set(ByVal value As String)
    '            TrinityIni.Text("ID", "UserEmail") = value
    '        End Set
    '    End Property

    '    Public Property UserPhoneNr()
    '        Get
    '            UserPhoneNr = TrinityIni.Text("ID", "UserPhoneNr")
    '        End Get
    '        Set(ByVal value)
    '            TrinityIni.Text("ID", "UserPhoneNr") = value
    '        End Set
    '    End Property

    '    Public Property DefaultInfo(ByVal Var As String) As Boolean
    '        Get
    '            DefaultInfo = NetworkIni.Data("Preferences", "Info" & Var)
    '        End Get
    '        Set(ByVal value As Boolean)
    '            NetworkIni.Data("Preferences", "Info" & Var) = value
    '        End Set
    '    End Property

    '    Public Property DefaultColorCodeChannels() As Boolean
    '        Get
    '            DefaultColorCodeChannels = TrinityIni.Data("Preferences", "ColorCode")
    '        End Get
    '        Set(ByVal value As Boolean)
    '            TrinityIni.Data("Preferences", "ColorCode") = value
    '        End Set
    '    End Property

    '    Public Property DefaultCapitals() As Boolean
    '        Get
    '            DefaultCapitals = TrinityIni.Data("Preferences", "Capitals")
    '        End Get
    '        Set(ByVal value As Boolean)
    '            TrinityIni.Data("Preferences", "Capitals") = value
    '        End Set
    '    End Property

    '    Public Property DefaultConvertToRealTime() As Boolean
    '        Get
    '            DefaultConvertToRealTime = TrinityIni.Data("Preferences", "ConvertToRealTime")
    '        End Get
    '        Set(ByVal value As Boolean)
    '            TrinityIni.Data("Preferences", "ConvertToRealTime") = value
    '        End Set
    '    End Property

    '    Public Property MarathonUser() As String
    '        Get
    '            MarathonUser = TrinityIni.Text("Marathon", "User")
    '        End Get
    '        Set(ByVal value As String)
    '            TrinityIni.Text("Marathon", "User") = value
    '        End Set
    '    End Property

    '    Public Sub New(ByVal LocalDataPath As String)
    '        mvarLocalDataPath = LocalDataPath
    '        Setup()
    '    End Sub

    '    Private Sub Setup()

    '        TrinityIni.Create(mvarLocalDataPath & "\Trinity.ini")
    '        If My.Computer.FileSystem.FileExists(Helper.Pathify(TrinityIni.Text("Paths", "Datapath")) & "Trinity.ini") Then
    '            NetworkIni.Create(Helper.Pathify(TrinityIni.Text("Paths", "Datapath")) & "Trinity.ini")
    '        Else
    '            NetworkIni.Create(mvarLocalDataPath & "\Data\Trinity.ini")
    '        End If
    '    End Sub

    '    Public Property CampaignFiles() As String
    '        Get
    '            Return TrinityIni.Text("Paths", "CampaignsPath")
    '        End Get
    '        Set(ByVal value As String)
    '            TrinityIni.Text("Paths", "CampaignsPath") = value
    '        End Set
    '    End Property

    '    Public Property ChannelSchedules() As String
    '        Get
    '            Return TrinityIni.Text("Paths", "SchedulesPath")
    '        End Get
    '        Set(ByVal value As String)
    '            TrinityIni.Text("Paths", "SchedulesPath") = value
    '        End Set
    '    End Property

    '    Public Property Universes() As Integer
    '        Get
    '            Return NetworkIni.Data("Universes", "Count")
    '        End Get
    '        Set(ByVal value As Integer)
    '            NetworkIni.Data("Universes", "Count") = value
    '        End Set
    '    End Property

    '    Public Property Universe(ByVal index As Integer) As String
    '        Get
    '            Return NetworkIni.Text("Universes", "Uni" & index)
    '        End Get
    '        Set(ByVal value As String)
    '            NetworkIni.Text("Universes", "Uni" & index) = value
    '        End Set
    '    End Property

    '    Public Property UniverseAdedge(ByVal index As Integer) As String
    '        Get
    '            Return NetworkIni.Text("Universes", "AdedgeUni" & index)
    '        End Get
    '        Set(ByVal value As String)
    '            NetworkIni.Text("Universes", "AdedgeUni" & index) = value
    '        End Set
    '    End Property
    'End Class

    'Public Class cBookedSpots
    '    Implements Collections.IEnumerable

    '    'local variable to hold collection
    '    Private mCol As Collection
    '    Private Main As cKampanj

    '    Friend WriteOnly Property MainObject() As cKampanj
    '        Set(ByVal value As cKampanj)
    '            Main = value
    '        End Set
    '    End Property

    '    Public Function Add(ByVal ID As String, ByVal DatabaseID As String, ByVal Channel As String, ByVal AirDate As Date, ByVal MaM As Integer, ByVal Programme As String, ByVal ProgAfter As String, ByVal ProgBefore As String, ByVal GrossPrice As Decimal, ByVal NetPrice As Decimal, ByVal ChannelEstimate As Single, ByVal MyEstimate As Single, ByVal MyEstimateChannelTarget As Single, ByVal Filmcode As String, ByVal Bookingtype As String, ByVal IsLocal As Boolean, ByVal IsRB As Boolean) As cBookedSpot
    '        'create a new object
    '        Dim objNewMember As cBookedSpot
    '        objNewMember = New cBookedSpot(Main)


    '        'set the properties passed into the method
    '        objNewMember.ID = ID
    '        objNewMember.DatabaseID = DatabaseID
    '        objNewMember.AirDate = AirDate
    '        objNewMember.MaM = MaM
    '        objNewMember.Programme = Programme
    '        objNewMember.ProgAfter = ProgAfter
    '        objNewMember.ProgBefore = ProgBefore
    '        objNewMember.GrossPrice = GrossPrice
    '        objNewMember.NetPrice = NetPrice
    '        objNewMember.ChannelEstimate = ChannelEstimate
    '        objNewMember.MyEstimate = MyEstimate
    '        objNewMember.Filmcode = Filmcode
    '        objNewMember.IsLocal = IsLocal
    '        objNewMember.IsRB = IsRB
    '        objNewMember.AddedValues = New Dictionary(Of String, Trinity.cAddedValue)
    '        'Main.ExtendedInfos(DatabaseID).IsBooked = True
    '        Main.RFEstimation.Spots.Add(AirDate, MaM, Main.Channels(Channel).AdEdgeNames, ID)

    '        objNewMember.Channel = Main.Channels(Channel)
    '        objNewMember.Bookingtype = objNewMember.Channel.BookingTypes(Bookingtype)
    '        objNewMember.week = objNewMember.Bookingtype.GetWeek(AirDate)
    '        If Not objNewMember.Bookingtype.Weeks(1).Films(Filmcode) Is Nothing Then
    '            objNewMember.GrossPrice30 = GrossPrice / (objNewMember.Bookingtype.Weeks(1).Films(Filmcode).Index / 100)
    '        End If
    '        objNewMember.MyEstimateBuyTarget = MyEstimateChannelTarget
    '        mCol.Add(objNewMember, ID)


    '        'return the object created
    '        Add = objNewMember
    '        objNewMember = Nothing


    '    End Function

    '    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cBookedSpot
    '        'used when referencing an element in the collection
    '        'vntIndexKey contains either the Index or Key to the collection,
    '        'this is why it is declared as a Variant
    '        'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
    '        Get
    '            If vntIndexKey.GetType.ToString = "System.String" Then
    '                If mCol.Contains(vntIndexKey) Then
    '                    Return mCol(vntIndexKey)
    '                Else
    '                    Return Nothing
    '                End If
    '            Else
    '                Return mCol(vntIndexKey)
    '            End If
    '        End Get
    '    End Property

    '    Public ReadOnly Property Count() As Long
    '        'used when retrieving the number of elements in the
    '        'collection. Syntax: Debug.Print x.Count
    '        Get
    '            Count = mCol.Count
    '        End Get
    '    End Property

    '    Public Sub Remove(ByVal vntIndexKey As Object)
    '        'used when removing an element from the collection
    '        'vntIndexKey contains either the Index or Key, which is why
    '        'it is declared as a Variant
    '        'Syntax: x.Remove(xyz)
    '        If Main.ExtendedInfos.Exists(mCol(vntIndexKey).DatabaseID) Then
    '            Main.ExtendedInfos(mCol(vntIndexKey).DatabaseID).IsBooked = False
    '        End If
    '        Main.RFEstimation.Spots.Remove(vntIndexKey)
    '        mCol.Remove(vntIndexKey)
    '    End Sub

    '    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
    '        GetEnumerator = mCol.GetEnumerator
    '    End Function

    '    Public Sub New(ByVal MainObject As cKampanj)
    '        mCol = New Collection
    '        Main = MainObject
    '        If Not Main.RFEstimation Is Nothing Then
    '            Main.RFEstimation.Spots.Clear()
    '        End If
    '    End Sub

    '    Protected Overrides Sub Finalize()
    '        mCol = Nothing
    '        MyBase.Finalize()
    '    End Sub
    'End Class

    '    Public Class cBookedSpot

    '        Public ID As String

    '        'local variable(s) to hold property value(s)
    '        Private mvarDatabaseID As String 'local copy
    '        Private mvarAirDate As Date 'local copy
    '        Private mvarMaM As Integer 'local copy
    '        Private mvarProgramme As String 'local copy
    '        Private mvarProgAfter As String 'local copy
    '        Private mvarProgBefore As String 'local copy
    '        Private mvarGrossPrice As Decimal 'local copy
    '        Private mvarNetPrice As Decimal 'local copy
    '        Private mvarChannelEstimate As Single 'local copy
    '        Private mvarMyEstimate As Single 'local copy
    '        Private mvarMyEstimateBuyTarget As Single
    '        Private mvarPlacement As PlaceEnum 'local copy
    '        Private mvarMatchedSpot As cPlannedSpot
    '        Public RecentlyBooked As Boolean
    '        Public MostRecentlyBooked As Boolean

    '        Public Enum PlaceEnum
    '            PlaceAny = 1
    '            PlaceTop = 2
    '            PlaceTail = 4
    '            PlaceTopOrTail = 8
    '            PlaceCentreBreak = 16
    '            PlaceStartBreak = 32
    '            PlaceEndBreak = 64
    '            PlaceRoadBlock = 128
    '            PlaceRequestedBreak = 256
    '            PlaceSecond = 512
    '            PlaceSecondLast = 1024
    '        End Enum

    '        Public AddedValues As Dictionary(Of String, Trinity.cAddedValue)
    '        Private mvarFilmcode As String 'local copy
    '        Private mvarBookingtype As cBookingType
    '        Private mvarWeek As cWeek

    '        Public IsLocal As Boolean
    '        Public IsRB As Boolean
    '        Public Comments As String
    '        Public Matched As Byte
    '        Public GrossPrice30 As Decimal

    '        'local variable(s) to hold property value(s)
    '        Private mvarChannel As cChannel 'local copy
    '        Private Main As cKampanj

    '        Function DateTimeSerial() As Single
    '            Return Helper.DateTimeSerial(AirDate, MaM)
    '        End Function

    '        Public WriteOnly Property MainObject() As cKampanj
    '            Set(ByVal value As cKampanj)
    '                Main = value
    '            End Set
    '        End Property

    '        Public Property MatchedSpot() As cPlannedSpot
    '            Get
    '                Return mvarMatchedSpot
    '            End Get
    '            Set(ByVal value As cPlannedSpot)
    '                mvarMatchedSpot = value
    '            End Set
    '        End Property

    '        Public Property Channel() As cChannel
    '            Get
    '                Channel = mvarChannel
    '            End Get
    '            Set(ByVal value As cChannel)
    '                mvarChannel = value
    '            End Set
    '        End Property

    '        Public Property Filmcode() As String
    '            Get
    '                Filmcode = mvarFilmcode
    '            End Get
    '            Set(ByVal value As String)
    '                mvarFilmcode = value
    '            End Set
    '        End Property

    '        Public Property MyEstimate() As Single
    '            Get
    '                MyEstimate = mvarMyEstimate
    '            End Get
    '            Set(ByVal value As Single) '
    '                mvarMyEstimate = value
    '            End Set
    '        End Property

    '        Public Property ChannelEstimate() As Single
    '            Get
    '                ChannelEstimate = mvarChannelEstimate
    '            End Get
    '            Set(ByVal value As Single)
    '                mvarChannelEstimate = value
    '            End Set
    '        End Property

    '        Public Property NetPrice() As Decimal
    '            'used when retrieving value of a property, on the right side of an assignment.
    '            'Syntax: Debug.Print X.NetPrice
    '            Get
    '                'Dim AVIndex As Single
    '                'Dim kv As KeyValuePair(Of String, cAddedValue)
    '                'Dim TmpAV As cAddedValue

    '                'AVIndex = 1
    '                'If AddedValues Is Nothing Then
    '                '    AddedValues = New Dictionary(Of String, cAddedValue)
    '                'End If
    '                'For Each kv In AddedValues
    '                '    TmpAV = kv.Value
    '                '    AVIndex = AVIndex * (TmpAV.IndexNet / 100)
    '                'Next
    '                'If AddedValues.Count = 0 Then
    '                Return mvarNetPrice
    '                'Else
    '                'Return mvarNetPrice * AVIndex
    '                'End If
    '            End Get
    '            Set(ByVal value As Decimal)
    '                mvarNetPrice = value
    '            End Set
    '        End Property

    '        Public Property GrossPrice() As Decimal
    '            'used when retrieving value of a property, on the right side of an assignment.
    '            'Syntax: Debug.Print X.GrossPrice
    '            Get
    '                'Dim AVIndex As Single
    '                'Dim kv As KeyValuePair(Of String, cAddedValue)
    '                'Dim TmpAV As cAddedValue

    '                'AVIndex = 1
    '                'If AddedValues Is Nothing Then
    '                '    AddedValues = New Dictionary(Of String, cAddedValue)
    '                'End If
    '                'For Each kv In AddedValues
    '                '    TmpAV = kv.Value
    '                '    AVIndex = AVIndex * (TmpAV.IndexGross / 100)
    '                'Next
    '                'If AddedValues.Count = 0 Then
    '                GrossPrice = mvarGrossPrice
    '                'Else
    '                '    GrossPrice = mvarGrossPrice * AVIndex
    '                'End If
    '            End Get
    '            Set(ByVal value As Decimal)
    '                mvarGrossPrice = value
    '            End Set
    '        End Property

    '        Public Function AddedValueIndex(Optional ByVal NetIndex As Boolean = True) As Single
    '            Dim AVIndex As Single
    '            Dim kv As KeyValuePair(Of String, cAddedValue)
    '            Dim TmpAV As cAddedValue

    '            AVIndex = 1
    '            If AddedValues Is Nothing Then
    '                AddedValues = New Dictionary(Of String, cAddedValue)
    '            End If
    '            For Each kv In AddedValues
    '                TmpAV = kv.Value
    '                If NetIndex Then
    '                    AVIndex = AVIndex * (TmpAV.IndexNet / 100)
    '                Else
    '                    AVIndex = AVIndex * (TmpAV.IndexGross / 100)
    '                End If
    '            Next
    '            Return AVIndex
    '        End Function

    '        Public Property ProgBefore()
    '            Get
    '                ProgBefore = mvarProgBefore
    '            End Get
    '            Set(ByVal value)
    '                mvarProgBefore = value
    '            End Set
    '        End Property

    '        Public Property ProgAfter()
    '            Get
    '                ProgAfter = mvarProgAfter
    '            End Get
    '            Set(ByVal value)
    '                mvarProgAfter = value
    '            End Set
    '        End Property

    '        Public Property Programme()
    '            Get
    '                Programme = mvarProgramme
    '            End Get
    '            Set(ByVal value)
    '                mvarProgramme = value
    '            End Set
    '        End Property

    '        'MaM is a time stamp
    '        Public Property MaM() As Integer
    '            Get
    '                MaM = mvarMaM
    '            End Get
    '            Set(ByVal value As Integer)
    '                mvarMaM = value
    '            End Set
    '        End Property

    '        Public Property AirDate() As Date
    '            Get
    '                AirDate = mvarAirDate
    '            End Get
    '            Set(ByVal value As Date)
    '                mvarAirDate = value
    '            End Set
    '        End Property

    '        Public Property DatabaseID() As String
    '            Get
    '                DatabaseID = mvarDatabaseID
    '            End Get
    '            Set(ByVal value As String)
    '                mvarDatabaseID = value
    '            End Set
    '        End Property

    '        Public Property Bookingtype() As cBookingType
    '            Get
    '                Bookingtype = mvarBookingtype
    '            End Get
    '            Set(ByVal value As cBookingType)
    '                mvarBookingtype = value
    '            End Set
    '        End Property

    '        Public Property MyEstimateBuyTarget() As Single
    '            Get
    '                On Error GoTo MyEstimateBuyTarget_Error

    '                MyEstimateBuyTarget = mvarMyEstimateBuyTarget

    '                On Error GoTo 0
    '                Exit Property

    'MyEstimateBuyTarget_Error:

    '                Err.Raise(Err.Number, "cBookedSpot: MyEstimateBuyTarget", Err.Description)
    '            End Get
    '            Set(ByVal value As Single)
    '                On Error GoTo MyEstimateBuyTarget_Error

    '                mvarMyEstimateBuyTarget = value

    '                On Error GoTo 0
    '                Exit Property

    'MyEstimateBuyTarget_Error:

    '                Err.Raise(Err.Number, "cBookedSpot: MyEstimateBuyTarget", Err.Description)
    '            End Set

    '        End Property

    '        Public Function Film() As cFilm
    '            Film = mvarBookingtype.Weeks(1).Films(mvarFilmcode)
    '        End Function

    '        Public Property week() As cWeek
    '            Get
    '                week = mvarWeek
    '            End Get
    '            Set(ByVal value As cWeek)
    '                mvarWeek = value
    '            End Set
    '        End Property

    '        Public Sub New(ByVal MainObject As cKampanj)
    '            Matched = False
    '            AddedValues = New Dictionary(Of String, cAddedValue)
    '            Main = MainObject
    '        End Sub
    '    End Class

    '    Public Class cCosts
    '        Implements Collections.IEnumerable

    '        'local variable to hold collection
    '        Private mCol As Collection

    '        Public Function Add(ByVal Name As String, ByVal CostType As cCost.CostTypeEnum, ByVal Amount As Single, ByVal CountCostOn As Byte, ByVal MarathonID As Long) As cCost
    '            'create a new object
    '            Dim objNewMember As cCost
    '            Dim e As Long
    '            Dim ID As String

    '            objNewMember = New cCost

    '            ID = CreateGUID()

    '            'set the properties passed into the method
    '            objNewMember.ID = ID
    '            objNewMember.CostName = Name
    '            objNewMember.Amount = Amount
    '            objNewMember.CountCostOn = CountCostOn
    '            objNewMember.CostType = CostType
    '            objNewMember.MarathonID = MarathonID

    '            On Error Resume Next
    '            mCol.Add(objNewMember, ID)
    '            e = Err.Number
    '            On Error GoTo 0


    '            'return the object created
    '            If e = 0 Then
    '                Add = objNewMember
    '            Else
    '                Add = mCol(ID)
    '            End If
    '            objNewMember = Nothing


    '        End Function

    '        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cCost
    '            Get
    '                Dim e As Long

    '                '    used when referencing an element in the collection
    '                '    vntIndexKey contains either the Index or Key to the collection,
    '                '    this is why it is declared as a Variant
    '                '    Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
    '                On Error GoTo Item_Error

    '                Item = mCol(vntIndexKey)

    '                On Error GoTo 0
    '                Exit Property

    'Item_Error:
    '                e = Err.Number

    '                Err.Raise(e, "cCosts", "Unknown Cost (" & vntIndexKey & ")")
    '            End Get

    '        End Property

    '        Public ReadOnly Property Count() As Long
    '            'used when retrieving the number of elements in the
    '            'collection. Syntax: Debug.Print x.Count
    '            Get
    '                Count = mCol.Count
    '            End Get
    '        End Property

    '        Public Sub Remove(ByVal vntIndexKey As Object)
    '            'used when removing an element from the collection
    '            'vntIndexKey contains either the Index or Key, which is why
    '            'it is declared as a Variant
    '            'Syntax: x.Remove(xyz)


    '            mCol.Remove(vntIndexKey)
    '        End Sub

    '        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
    '            Return mCol.GetEnumerator
    '        End Function

    '        Public Sub New()
    '            mCol = New Collection
    '        End Sub

    '        Protected Overrides Sub Finalize()
    '            mCol = Nothing
    '            MyBase.Finalize()
    '        End Sub
    '    End Class

    'Public Class cCost

    '    Public Enum CostTypeEnum
    '        CostTypeFixed = 0
    '        CostTypePercent = 1
    '        CostTypePerUnit = 2
    '    End Enum

    '    Public Enum CostOnUnitEnum
    '        CostOnSpots = 0
    '        CostOnBuyingTRP = 1
    '        CostOnMainTRP = 2
    '    End Enum

    '    Public Enum CostOnPercentEnum
    '        CostOnMediaNet = 0
    '        CostOnNet = 1
    '        CostOnNetNet = 2
    '    End Enum

    '    Private mvarCostName As String
    '    Private mvarAmount As Single
    '    Private mvarCountCostOn As Single
    '    Private mvarCostType As CostTypeEnum
    '    Private mvarMarathonID As Integer

    '    Public ID As String

    '    Public Property CostName() As String
    '        'gets/sets the Cost name
    '        Get
    '            CostName = mvarCostName
    '        End Get
    '        Set(ByVal value As String)
    '            mvarCostName = value
    '        End Set
    '    End Property

    '    Public Property Amount() As Single
    '        'gets/sets the amount
    '        Get
    '            Amount = mvarAmount
    '        End Get
    '        Set(ByVal value As Single)
    '            mvarAmount = value
    '        End Set
    '    End Property

    '    Public Property CountCostOn() As Byte
    '        'gets/sets on what the cost is calculated (gross net etc)
    '        Get
    '            CountCostOn = mvarCountCostOn
    '        End Get
    '        Set(ByVal value As Byte)
    '            mvarCountCostOn = value
    '        End Set
    '    End Property

    '    Public Property CostType() As CostTypeEnum
    '        'gets/sets what type of cost it is
    '        Get
    '            CostType = mvarCostType
    '        End Get
    '        Set(ByVal value As CostTypeEnum)
    '            mvarCostType = value
    '        End Set
    '    End Property

    '    Public Function FormattedAmount() As String
    '        If mvarCostType = CostTypeEnum.CostTypeFixed Or mvarCostType = CostTypeEnum.CostTypePerUnit Then
    '            FormattedAmount = Format(mvarAmount, "##,##0 kr")
    '        Else
    '            FormattedAmount = Format(mvarAmount, "##,##0.0%")
    '        End If
    '    End Function

    '    Public Function CostOnText() As String
    '        'a function for setting a string depending on what the cost is supposed to be calculated on
    '        '(if its a fixed/variable cost and when its goning to be debited
    '        If mvarCostType = CostTypeEnum.CostTypeFixed Then
    '            CostOnText = "-"
    '        ElseIf mvarCostType = CostTypeEnum.CostTypePercent Then
    '            Select Case mvarCountCostOn
    '                Case CostOnPercentEnum.CostOnMediaNet : CostOnText = "Media Net"
    '                Case CostOnPercentEnum.CostOnNet : CostOnText = "Net"
    '                Case CostOnPercentEnum.CostOnNetNet : CostOnText = "Net Net"
    '                Case Else : CostOnText = ""
    '            End Select
    '        Else
    '            Select Case mvarCountCostOn
    '                Case CostOnUnitEnum.CostOnBuyingTRP : CostOnText = "Buy TRP"
    '                Case CostOnUnitEnum.CostOnMainTRP : CostOnText = "Main TRP"
    '                Case CostOnUnitEnum.CostOnSpots : CostOnText = "Spots"
    '                Case Else : CostOnText = ""
    '            End Select
    '        End If
    '    End Function

    '    Public Property MarathonID() As Integer
    '        'gets/sets the MarathonID for the cost
    '        Get
    '            MarathonID = mvarMarathonID
    '        End Get
    '        Set(ByVal value As Integer)
    '            mvarMarathonID = value
    '        End Set
    '    End Property

    'End Class

    'Public Class cContract

    '    Const VERSION As Integer = 1

    '    Public Costs As cCosts 'a collection of cost
    '    Public Name As String
    '    Private mvarChannels As cChannels 'collection of channels
    '    Public FromDate As Date
    '    Public ToDate As Date
    '    Private Main As cKampanj 'the main campaign
    '    Private mvarVolume As Decimal

    '    Public WriteOnly Property MainObject() As cKampanj
    '        Set(ByVal value As cKampanj)
    '            Main = value
    '        End Set
    '    End Property

    '    Public Property NegotiatedVolume() As Decimal
    '        Get
    '            Return mvarVolume
    '        End Get
    '        Set(ByVal value As Decimal)
    '            mvarVolume = value
    '        End Set
    '    End Property

    '    Private Sub CreateChannels()
    '        'saves all channels from the XML files to a cChannels object

    '        Dim XMLDoc As New Xml.XmlDocument
    '        Dim XMLChannels As Xml.XmlElement
    '        Dim XMLTmpNode As Xml.XmlElement
    '        Dim XMLTmpNode2 As Xml.XmlElement
    '        Dim XMLBookingTypes As Xml.XmlElement
    '        Dim TmpChannel As cChannel
    '        Dim TmpBT As cBookingType

    '        'gets the path where the XML files are located
    '        XMLDoc.Load(TrinitySettings.DataPath & Campaign.Area & "\Channels.xml")

    '        'gets all the channels and booking types into a XML element
    '        XMLChannels = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("Channels")
    '        XMLBookingTypes = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("Bookingtypes")

    '        'selects the first channel tp the temp object
    '        XMLTmpNode = XMLChannels.ChildNodes.Item(0)

    '        'creates a new channel collection
    '        Channels = New cChannels(Main)
    '        Channels.MainObject = Main

    '        'as long as there are a object in the temp variable
    '        While Not XMLTmpNode Is Nothing
    '            'gets the Name and adds the "SE" tag
    '            TmpChannel = mvarChannels.Add(XMLTmpNode.GetAttribute("Name"), "SE")
    '            'selects the first booking type
    '            XMLTmpNode2 = XMLBookingTypes.ChildNodes.Item(0)

    '            'this loop goes through all available booking types for the selected channel
    '            While Not XMLTmpNode2 Is Nothing
    '                TmpBT = TmpChannel.BookingTypes.Add(XMLTmpNode2.GetAttribute("Name"), False)
    '                TmpBT.Shortname = XMLTmpNode2.GetAttribute("Shortname")
    '                TmpBT.IsRBS = XMLTmpNode2.GetAttribute("IsRBS")
    '                TmpBT.IsSpecific = XMLTmpNode2.GetAttribute("IsSpecific")
    '                XMLTmpNode2 = XMLTmpNode2.NextSibling
    '            End While
    '            'selects the first spot index
    '            XMLTmpNode2 = XMLTmpNode.GetElementsByTagName("SpotIndex").Item(0).FirstChild
    '            'the loop gets all availavle spot indexes
    '            While Not XMLTmpNode2 Is Nothing
    '                For Each TmpBT In TmpChannel.BookingTypes
    '                    TmpBT.FilmIndex(XMLTmpNode2.GetAttribute("Length")) = XMLTmpNode2.GetAttribute("Idx")
    '                Next
    '                XMLTmpNode2 = XMLTmpNode2.NextSibling
    '            End While
    '            XMLTmpNode = XMLTmpNode.NextSibling
    '        End While
    '        'now all the channels are set in the channels variable


    '    End Sub

    '    Function Save(ByVal Path As String, Optional ByVal DoNotSaveToFile As Boolean = False)
    '        'the function saves the contract ot a XML file

    '        On Error Resume Next
    '        If Not DoNotSaveToFile Then
    '            Kill(Path)
    '        End If
    '        On Error GoTo 0

    '        Helper.WriteToLogFile("Init XML")
    '        Dim XMLDoc As New Xml.XmlDocument
    '        Dim XMLContract As Xml.XmlElement
    '        Dim XMLChannel As Xml.XmlElement
    '        Dim XMLBT As Xml.XmlElement
    '        Dim XMLIndexes As Xml.XmlElement
    '        Dim XMLIndex As Xml.XmlElement
    '        Dim TmpNode As Xml.XmlElement
    '        Dim XMLTargets As Xml.XmlElement
    '        Dim XMLTarget As Xml.XmlElement
    '        Dim Node

    '        Dim TmpCost As cCost
    '        Dim TmpChannel As cChannel
    '        Dim TmpBT As cBookingType
    '        Dim TmpIndex As cIndex
    '        Dim TmpAV As cAddedValue
    '        Dim TmpTarget As cPricelistTarget

    '        Dim i As Integer

    '        Helper.WriteToLogFile("Start creating document")
    '        'XMLDoc.async = False
    '        'XMLDoc.validateOnParse = False
    '        'XMLDoc.resolveExternals = False
    '        XMLDoc.PreserveWhitespace = True
    '        Node = XMLDoc.CreateProcessingInstruction("xml", "version='1.0'")
    '        XMLDoc.AppendChild(Node)

    '        Node = XMLDoc.CreateComment("Trinity contract.")
    '        XMLDoc.AppendChild(Node)

    '        XMLContract = XMLDoc.CreateElement("Contract")
    '        XMLDoc.AppendChild(XMLContract)
    '        XMLContract.SetAttribute("Version", VERSION)
    '        XMLContract.SetAttribute("Name", Name)
    '        XMLContract.SetAttribute("From", FromDate)
    '        XMLContract.SetAttribute("To", ToDate)
    '        XMLContract.SetAttribute("NegotiatedVolume", mvarVolume)

    '        Node = XMLDoc.CreateElement("Costs")
    '        For Each TmpCost In Costs
    '            TmpNode = XMLDoc.CreateElement("Node")
    '            TmpNode.SetAttribute("Name", TmpCost.CostName)
    '            TmpNode.SetAttribute("ID", TmpCost.ID)
    '            TmpNode.SetAttribute("Amount", TmpCost.Amount)
    '            TmpNode.SetAttribute("CostOn", TmpCost.CountCostOn)
    '            TmpNode.SetAttribute("CostType", TmpCost.CostType)
    '            TmpNode.SetAttribute("MarathonID", TmpCost.MarathonID)
    '            Node.appendChild(TmpNode)
    '        Next
    '        XMLContract.AppendChild(Node)

    '        Node = XMLDoc.CreateElement("Channels")
    '        For Each TmpChannel In Channels
    '            For Each TmpBT In TmpChannel.BookingTypes
    '                TmpNode = XMLDoc.CreateElement("Channel")
    '                TmpNode.SetAttribute("Chan", TmpChannel.ChannelName)
    '                TmpNode.SetAttribute("BT", TmpBT.Name)
    '                XMLIndexes = XMLDoc.CreateElement("Indexes")
    '                For Each TmpIndex In TmpBT.Indexes
    '                    XMLIndex = XMLDoc.CreateElement("Index")
    '                    XMLIndex.SetAttribute("ID", TmpIndex.ID)
    '                    XMLIndex.SetAttribute("Name", TmpIndex.Name)
    '                    XMLIndex.SetAttribute("Value", TmpIndex.Index)
    '                    XMLIndex.SetAttribute("IndexOn", TmpIndex.IndexOn)
    '                    XMLIndex.SetAttribute("SystemGenerated", TmpIndex.SystemGenerated)
    '                    XMLIndex.SetAttribute("FromDate", TmpIndex.FromDate)
    '                    XMLIndex.SetAttribute("ToDate", TmpIndex.ToDate)
    '                    XMLIndexes.AppendChild(XMLIndex)
    '                Next
    '                TmpNode.AppendChild(XMLIndexes)

    '                XMLIndexes = XMLDoc.CreateElement("AddedValues")
    '                For Each TmpAV In TmpBT.AddedValues
    '                    XMLIndex = XMLDoc.CreateElement("AddedValue")
    '                    XMLIndex.SetAttribute("ID", TmpAV.ID)
    '                    XMLIndex.SetAttribute("Name", TmpAV.Name)
    '                    XMLIndex.SetAttribute("IndexGross", TmpAV.IndexGross)
    '                    XMLIndex.SetAttribute("IndexNet", TmpAV.IndexNet)
    '                    XMLIndexes.AppendChild(XMLIndex)
    '                Next
    '                TmpNode.AppendChild(XMLIndexes)
    '                XMLIndexes = XMLDoc.CreateElement("SpotIndex")
    '                For i = 0 To 500
    '                    If TmpBT.FilmIndex(i) > 0 Then
    '                        XMLIndex = XMLDoc.CreateElement("Index")
    '                        XMLIndex.SetAttribute("Length", i)
    '                        XMLIndex.SetAttribute("Idx", TmpBT.FilmIndex(i))
    '                        XMLIndexes.AppendChild(XMLIndex)
    '                    End If
    '                Next
    '                TmpNode.AppendChild(XMLIndexes)

    '                XMLTargets = XMLDoc.CreateElement("Targets")
    '                For Each TmpTarget In TmpBT.Pricelist.Targets
    '                    XMLTarget = XMLDoc.CreateElement("Target")
    '                    XMLTarget.SetAttribute("Name", TmpTarget.TargetName)
    '                    XMLTarget.SetAttribute("Target", TmpTarget.Target.TargetName)
    '                    XMLTarget.SetAttribute("Universe", TmpTarget.Target.Universe)
    '                    XMLTarget.SetAttribute("CalcCPP", TmpTarget.CalcCPP)
    '                    XMLTarget.SetAttribute("CPP", TmpTarget.CPP)
    '                    XMLTarget.SetAttribute("UniSize", TmpTarget.UniSize)
    '                    XMLTarget.SetAttribute("UniSizeNat", TmpTarget.UniSizeNat)
    '                    XMLTarget.SetAttribute("IsEntered", TmpTarget.IsEntered)
    '                    If TmpTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
    '                        XMLTarget.SetAttribute("Value", TmpTarget.NetCPP)
    '                    ElseIf TmpTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPT Then
    '                        XMLTarget.SetAttribute("Value", TmpTarget.NetCPT)
    '                    ElseIf TmpTarget.IsEntered = cPricelistTarget.EnteredEnum.eDiscount Then
    '                        XMLTarget.SetAttribute("Value", TmpTarget.Discount)
    '                    End If
    '                    For i = 0 To Main.DaypartCount - 1
    '                        XMLTarget.SetAttribute("DP" & i, TmpTarget.DefaultDaypart(i))
    '                        XMLTarget.SetAttribute("CPP_DP" & i, TmpTarget.CPPDaypart(i))
    '                    Next
    '                    XMLIndexes = XMLDoc.CreateElement("Indexes")
    '                    For Each TmpIndex In TmpTarget.Indexes
    '                        XMLIndex = XMLDoc.CreateElement("Index")
    '                        XMLIndex.SetAttribute("ID", TmpIndex.ID)
    '                        XMLIndex.SetAttribute("Name", TmpIndex.Name)
    '                        XMLIndex.SetAttribute("Value", TmpIndex.Index)
    '                        XMLIndex.SetAttribute("IndexOn", TmpIndex.IndexOn)
    '                        XMLIndex.SetAttribute("SystemGenerated", TmpIndex.SystemGenerated)
    '                        XMLIndex.SetAttribute("FromDate", TmpIndex.FromDate)
    '                        XMLIndex.SetAttribute("ToDate", TmpIndex.ToDate)
    '                        XMLIndexes.AppendChild(XMLIndex)
    '                    Next
    '                    XMLTarget.AppendChild(XMLIndexes)
    '                    XMLTargets.AppendChild(XMLTarget)
    '                Next
    '                TmpNode.AppendChild(XMLTargets)

    '                Node.appendChild(TmpNode)
    '            Next
    '        Next
    '        XMLContract.AppendChild(Node)
    '        If Not DoNotSaveToFile Then
    '            XMLDoc.Save(Path)
    '        End If
    '        Save = XMLDoc.OuterXml
    '    End Function

    '    Sub Load(ByVal Path As String, Optional ByVal LoadXML As Boolean = False, Optional ByVal XML As String = "")
    '        'loads a contract from a XML file
    '        Dim XMLDoc As New Xml.XmlDocument
    '        Dim XMLContract As Xml.XmlElement
    '        Dim XMLChannel As Xml.XmlElement
    '        Dim XMLIndex As Xml.XmlElement
    '        Dim TmpNode As Xml.XmlElement

    '        Dim Chan As String
    '        Dim BT As String
    '        Dim i As Integer

    '        If LoadXML Then
    '            XMLDoc.LoadXml(XML)
    '        Else
    '            XMLDoc.Load(Path)
    '        End If

    '        XMLContract = XMLDoc.GetElementsByTagName("Contract").Item(0)

    '        Name = XMLContract.GetAttribute("Name")
    '        FromDate = XMLContract.GetAttribute("From")
    '        ToDate = XMLContract.GetAttribute("To")
    '        If Not XMLContract.GetAttribute("NegotiatedVolume") Is Nothing AndAlso Not XMLContract.GetAttribute("NegotiatedVolume") = "" Then
    '            mvarVolume = XMLContract.GetAttribute("NegotiatedVolume")
    '        End If
    '        TmpNode = XMLContract.GetElementsByTagName("Costs").Item(0).ChildNodes.Item(0)
    '        While Not TmpNode Is Nothing
    '            Costs.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("CostType"), TmpNode.GetAttribute("Amount"), TmpNode.GetAttribute("CostOn"), TmpNode.GetAttribute("MarathonID"))
    '            TmpNode = TmpNode.NextSibling
    '        End While
    '        XMLChannel = XMLDoc.GetElementsByTagName("Channels").Item(0).ChildNodes.Item(0)
    '        While Not XMLChannel Is Nothing
    '            If Not XMLChannel.GetAttribute("Name") Is Nothing AndAlso XMLChannel.GetAttribute("Name") <> "" Then
    '                Chan = Left(XMLChannel.GetAttribute("Name"), InStr(XMLChannel.GetAttribute("Name"), " ") - 1)
    '                BT = Mid(XMLChannel.GetAttribute("Name"), InStr(XMLChannel.GetAttribute("Name"), " ") + 1)
    '            Else
    '                Chan = XMLChannel.GetAttribute("Chan")
    '                BT = XMLChannel.GetAttribute("BT")
    '            End If
    '            TmpNode = XMLChannel.GetElementsByTagName("Indexes").Item(0).FirstChild
    '            While Not TmpNode Is Nothing
    '                Channels(Chan).BookingTypes(BT).Indexes.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("ID"))
    '                Channels(Chan).BookingTypes(BT).Indexes(TmpNode.GetAttribute("ID")).Name = TmpNode.GetAttribute("Name")
    '                Channels(Chan).BookingTypes(BT).Indexes(TmpNode.GetAttribute("ID")).FromDate = TmpNode.GetAttribute("FromDate")
    '                Channels(Chan).BookingTypes(BT).Indexes(TmpNode.GetAttribute("ID")).ToDate = TmpNode.GetAttribute("ToDate")
    '                Channels(Chan).BookingTypes(BT).Indexes(TmpNode.GetAttribute("ID")).Index = TmpNode.GetAttribute("Value")
    '                Channels(Chan).BookingTypes(BT).Indexes(TmpNode.GetAttribute("ID")).IndexOn = TmpNode.GetAttribute("IndexOn")
    '                Channels(Chan).BookingTypes(BT).Indexes(TmpNode.GetAttribute("ID")).SystemGenerated = TmpNode.GetAttribute("SystemGenerated")
    '                TmpNode = TmpNode.NextSibling
    '            End While
    '            TmpNode = XMLChannel.GetElementsByTagName("AddedValues").Item(0).FirstChild
    '            While Not TmpNode Is Nothing
    '                Channels(Chan).BookingTypes(BT).AddedValues.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("ID"))
    '                Channels(Chan).BookingTypes(BT).AddedValues(TmpNode.GetAttribute("ID")).IndexGross = TmpNode.GetAttribute("IndexGross")
    '                Channels(Chan).BookingTypes(BT).AddedValues(TmpNode.GetAttribute("ID")).IndexNet = TmpNode.GetAttribute("IndexNet")
    '                TmpNode = TmpNode.NextSibling
    '            End While
    '            TmpNode = XMLChannel.GetElementsByTagName("SpotIndex").Item(0).FirstChild
    '            While Not TmpNode Is Nothing
    '                Channels(Chan).BookingTypes(BT).FilmIndex(TmpNode.GetAttribute("Length")) = TmpNode.GetAttribute("Idx")
    '                TmpNode = TmpNode.NextSibling
    '            End While
    '            TmpNode = XMLChannel.GetElementsByTagName("Targets").Item(0).FirstChild
    '            While Not TmpNode Is Nothing
    '                Channels(Chan).BookingTypes(BT).Pricelist.Targets.Add(TmpNode.GetAttribute("Name"))
    '                Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).Target.TargetName = TmpNode.GetAttribute("Target")
    '                Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).Target.Universe = TmpNode.GetAttribute("Universe")
    '                Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).CalcCPP = TmpNode.GetAttribute("CalcCPP")
    '                Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).CPP = TmpNode.GetAttribute("CPP")
    '                Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).StandardTarget = True
    '                Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).UniSize = TmpNode.GetAttribute("UniSize")
    '                Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).UniSizeNat = TmpNode.GetAttribute("UniSizeNat")
    '                Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).IsEntered = TmpNode.GetAttribute("IsEntered")
    '                For i = 0 To Main.DaypartCount - 1
    '                    Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).DefaultDaypart(i) = TmpNode.GetAttribute("DP" & i)
    '                    Channels(Chan).BookingTypes(BT).DaypartSplit(i) = TmpNode.GetAttribute("DP" & i)
    '                    Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).CPPDaypart(i) = TmpNode.GetAttribute("CPP_DP" & i)
    '                Next
    '                Channels(Chan).BookingTypes(BT).GrossCPP = Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).CPP
    '                XMLIndex = TmpNode.GetElementsByTagName("Indexes").Item(0).FirstChild
    '                While Not XMLIndex Is Nothing
    '                    Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).Indexes.Add(XMLIndex.GetAttribute("Name"), XMLIndex.GetAttribute("ID"))
    '                    Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).Indexes(XMLIndex.GetAttribute("ID")).FromDate = XMLIndex.GetAttribute("FromDate")
    '                    Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).Indexes(XMLIndex.GetAttribute("ID")).ToDate = XMLIndex.GetAttribute("ToDate")
    '                    Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).Indexes(XMLIndex.GetAttribute("ID")).Index = XMLIndex.GetAttribute("Value")
    '                    Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).Indexes(XMLIndex.GetAttribute("ID")).IndexOn = XMLIndex.GetAttribute("IndexOn")
    '                    Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).Indexes(XMLIndex.GetAttribute("ID")).SystemGenerated = XMLIndex.GetAttribute("SystemGenerated")
    '                    XMLIndex = XMLIndex.NextSibling
    '                End While
    '                If Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
    '                    Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).NetCPP = TmpNode.GetAttribute("Value")
    '                ElseIf Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).IsEntered = cPricelistTarget.EnteredEnum.eCPT Then
    '                    Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).NetCPT = TmpNode.GetAttribute("Value")
    '                Else
    '                    Channels(Chan).BookingTypes(BT).Pricelist.Targets(TmpNode.GetAttribute("Name")).Discount = TmpNode.GetAttribute("Value")
    '                End If
    '                TmpNode = TmpNode.NextSibling
    '            End While
    '            XMLChannel = XMLChannel.NextSibling
    '        End While
    '    End Sub

    '    Public Property Channels() As cChannels
    '        Get
    '            Channels = mvarChannels
    '        End Get
    '        Set(ByVal value As cChannels)
    '            mvarChannels = value
    '        End Set
    '    End Property

    '    Public Sub New(ByVal Main As cKampanj)
    '        Costs = New cCosts
    '        FromDate = Now
    '        ToDate = Now
    '        MainObject = Main
    '        CreateChannels()
    '    End Sub

    'End Class

    'Public Class cReachguide

    '    Private mvarReferencePeriods As New cReferencePeriods
    '    Private mvarSpots As New cSpots
    '    Public StartDate As Date
    '    Public EndDate As Date
    '    Public Target As String

    '    Public ReadOnly Property ReferencePeriods() As cReferencePeriods
    '        Get
    '            ReferencePeriods = mvarReferencePeriods
    '        End Get
    '    End Property

    '    Friend ReadOnly Property Spots() As cSpots
    '        Get
    '            Spots = mvarSpots
    '        End Get
    '    End Property

    '    Public Sub Calculate()

    '        Dim TmpPeriod As cReferencePeriod

    '        For Each TmpPeriod In mvarReferencePeriods
    '            TmpPeriod.Calculate()
    '        Next

    '    End Sub

    '    Public Function Reach(ByVal Freq As Byte) As Single

    '        Dim TotReach As Single
    '        Dim i As Integer

    '        For i = 1 To mvarReferencePeriods.Count
    '            TotReach = TotReach + mvarReferencePeriods(i).Reach(Freq)
    '        Next
    '        If mvarReferencePeriods.Count > 0 Then
    '            Reach = TotReach / mvarReferencePeriods.Count
    '        Else
    '            Reach = 0
    '        End If
    '    End Function


    '    Public Function Solus(ByVal AirDate As Date, ByVal MaM As Integer, ByVal Channel As String, Optional ByVal Freq As Byte = 1, Optional ByVal Placement As Integer = 0) As Single
    '        Dim ID As String
    '        Dim ReachBefore As Single
    '        Dim ReachAfter As Single

    '        ID = CreateGUID()
    '        Calculate()
    '        ReachBefore = Reach(CByte(Freq))
    '        mvarSpots.Add(AirDate, MaM, Channel, ID, Placement)
    '        Calculate()
    '        ReachAfter = Reach(CByte(Freq))
    '        mvarSpots.Remove(ID)
    '        Calculate()
    '        Solus = ReachAfter - ReachBefore

    '    End Function

    '    Public Sub New()

    '        Base = Me
    '        mvarSpots.Parent = Me

    '    End Sub
    'End Class

    'Public Class cReferencePeriod
    '    'Implements ConnectWrapper.ICallBack

    '    Private m_dtStartDate As Date

    '    Private InternalAdedge As ConnectWrapper.Brands
    '    Private mvarSpots As New cSpots
    '    Private m_sName As String

    '    Public Event Progress(ByVal ReferencePeriod As String, ByVal Progress As Long)

    '    Friend Sub Calculate()

    '        Dim TmpSpot As cSpot

    '        If InternalAdedge.validate > 0 Then
    '            InternalAdedge = New ConnectWrapper.Brands
    '            InternalAdedge.setArea("SE")
    '            InternalAdedge.setBrandType("COMMERCIAL")
    '            InternalAdedge.setPeriod(Format(m_dtStartDate, "ddMMyy") & "-" & Format(m_dtStartDate + (Base.EndDate - Base.StartDate), "ddMMyy"))
    '            InternalAdedge.setChannelsArea("TV3 se,TV4,Kan 5")
    '            InternalAdedge.setTargetMnemonic(Base.Target)
    '            InternalAdedge.Run(True, False, 10)
    '            InternalAdedge.sort("channel,date(asc),fromtime(asc)")
    '        End If
    '        InternalAdedge.clearGroup()
    '        For Each kv As DictionaryEntry In mvarSpots
    '            TmpSpot = kv.Value
    '            'TmpSpot = mvarSpots(i)
    '            InternalAdedge.addToGroup(TmpSpot.SpotIndex)
    '        Next
    '        'If InternalAdedge.getGroupCount > 0 Then
    '        InternalAdedge.recalcRF(Connect.eSumModes.smGroup)
    '        'End If

    '    End Sub

    '    Public ReadOnly Property Spots() As cSpots
    '        Get
    '            Spots = mvarSpots
    '        End Get
    '    End Property

    '    Friend Function Reach(ByVal Freq As Byte) As Single
    '        If InternalAdedge.getGroupCount > 0 Then
    '            Reach = InternalAdedge.getRF(InternalAdedge.getGroupCount - 1, , , , Freq)
    '        Else
    '            Reach = 0
    '        End If
    '    End Function

    '    Public ReadOnly Property Adedge() As ConnectWrapper.Brands
    '        Get
    '            Adedge = InternalAdedge
    '        End Get
    '    End Property

    '    Public Property StartDate() As Date
    '        Get
    '            StartDate = m_dtStartDate
    '        End Get
    '        Set(ByVal value As Date)
    '            m_dtStartDate = value
    '        End Set
    '    End Property

    '    Private Sub ICallBack_callback(ByVal p As Long)
    '        RaiseEvent Progress(m_sName, p)
    '    End Sub

    '    Public Property Name() As String
    '        Get
    '            Name = m_sName
    '        End Get
    '        Set(ByVal value As String)
    '            m_sName = value
    '        End Set
    '    End Property

    '    Public Sub New()
    '        mvarSpots.Parent = Me
    '        InternalAdedge = New ConnectWrapper.Brands
    '    End Sub

    '    'Public Sub callback(ByVal p As Integer) Implements ConnectWrapper.ICallBack.callback

    '    'End Sub
    'End Class


   

    'Public Class cReferencePeriods
    '    Implements Collections.IEnumerable
    '    Private mCol As New Collection

    '    Function Add(ByVal StartDate As Date, ByVal Name As String) As cReferencePeriod

    '        Dim TmpPeriod As New cReferencePeriod

    '        TmpPeriod.StartDate = StartDate
    '        TmpPeriod.Name = Name

    '        mCol.Add(TmpPeriod, Name)

    '        Add = mCol(Name)
    '    End Function

    '    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cReferencePeriod
    '        Get
    '            Item = mCol(vntIndexKey)
    '        End Get
    '    End Property

    '    Public Function Count() As Integer
    '        Count = mCol.Count
    '    End Function

    '    Public Sub Remove(ByVal vntIndexKey As Object)
    '        mCol.Remove(vntIndexKey)
    '    End Sub

    '    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
    '        GetEnumerator = mCol.GetEnumerator
    '    End Function
    'End Class

    '    Public Class cSpots
    '        Implements IEnumerable

    '        Private mCol As New cWrapper
    '        Private mvarParent As Object

    '        Public Function Add(ByVal AirDate As Date, ByVal MaM As Integer, ByVal Channel As String, ByVal ID As String, Optional ByVal Placement As Integer = 0)

    '            Dim TmpSpot As New cSpot
    '            Dim i As Integer

    '            If mvarParent.StartDate = Base.StartDate Then
    '                TmpSpot.AirDate = AirDate
    '                TmpSpot.MaM = MaM
    '                TmpSpot.Channel = Channel
    '                For i = 1 To Base.ReferencePeriods.Count
    '                    Base.ReferencePeriods.Item(i).Spots.Add(AirDate - (Base.StartDate - Base.ReferencePeriods.Item(i).StartDate), MaM, Channel, ID, Placement)
    '                Next
    '            Else
    '                TmpSpot = FindBestSpot(AirDate, MaM, Channel, Placement)
    '            End If
    '            On Error Resume Next
    '            Add = mCol.Add(TmpSpot, ID)
    '            On Error GoTo 0
    '        End Function

    '        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cSpot
    '            Get
    '                Item = mCol(vntIndexKey)
    '            End Get
    '        End Property

    '        Friend WriteOnly Property Parent()
    '            Set(ByVal value)
    '                mvarParent = value
    '            End Set
    '        End Property

    '        Private Function FindBestSpot(ByVal AirDate As Date, ByVal MaM As Integer, ByVal Channel As String, Optional ByVal Placement As Integer = 0) As cSpot

    '            Dim i As Long
    '            Dim LastDistance As Long
    '            Dim TmpSpot As cSpot
    '            Dim TmpPeriod As Trinity.cReferencePeriod = mvarParent

    '            LastDistance = 999999
    '            i = 0
    '            While Channel <> TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aChannel, i)
    '                i = i + TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBrandSpotCount, i)
    '            End While
    '            While Format(Date.FromOADate(TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aDate, i)), "yyyy-MM-dd") < Format(AirDate, "yyyy-MM-dd")
    '                i = i + TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBrandSpotCount, i)
    '            End While
    '            While Math.Abs(TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aFromTime, i) \ 60 - MaM) <= LastDistance
    '                LastDistance = Math.Abs(TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aFromTime, i) \ 60 - MaM)
    '                i = i + TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBrandSpotCount, i)
    '            End While
    '            i = i - TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBrandSpotInBreak, i - 1)
    '            If Placement = 0 Then
    '                i = i + Int(TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBrandSpotCount, i) / 2)
    '            ElseIf Placement = -1 Then
    '                i = i
    '            ElseIf Placement = 1 Then
    '                i = i + Int(TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBrandSpotCount, i)) - 1
    '            End If
    '            TmpSpot = New cSpot
    '            TmpSpot.AirDate = AirDate
    '            TmpSpot.Channel = Channel
    '            TmpSpot.MaM = TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aFromTime, i) \ 60
    '            TmpSpot.SpotIndex = i
    '            FindBestSpot = TmpSpot
    '            '    Stop
    '        End Function

    '        Public Function Count() As Integer
    '            Count = mCol.Count
    '        End Function

    '        Public Sub Remove(ByVal vntIndexKey As Object)

    '            Dim i As Integer

    '            On Error Resume Next
    '            If mCol.Exists(vntIndexKey) Then mCol.Remove(vntIndexKey)
    '            On Error GoTo ErrHandle

    '            If mvarParent.StartDate = Base.StartDate Then
    '                For i = 1 To Base.ReferencePeriods.Count
    '                    Base.ReferencePeriods.Item(i).Spots.Remove(vntIndexKey)
    '                Next
    '            End If
    '            Exit Sub

    'ErrHandle:
    '            Err.Raise(Err.Number)
    '        End Sub

    '        Public Sub Clear()
    '            Dim i As Integer

    '            mCol.RemoveAll()
    '            If mvarParent.StartDate = Base.StartDate Then
    '                For i = 1 To Base.ReferencePeriods.Count
    '                    Base.ReferencePeriods.Item(i).Spots.Clear()
    '                Next
    '            End If

    '        End Sub

    '        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
    '            Return mCol.GetEnumerator
    '        End Function
    '    End Class

    'Public Class cSpot
    '    Public AirDate As Date
    '    Public MaM As Integer 'Minutes after Midnight, a time format
    '    Public Channel As String
    '    Public SpotIndex As Long
    'End Class

'Public Class cWrapper
'    Implements Collections.IEnumerable

'    Dim TmpHive As New Hashtable
'    Dim CurrentIndex As Integer

'    'Dim Keys() As Variant

'    Public Function Add(ByRef Item As Object, ByRef Key As Object) As Object

'        TmpHive.Add(Key, Item)

'        Add = TmpHive(Key)

'        '    ReDim Preserve Keys(TmpTreap.Count)
'        '
'        '    Keys(TmpTreap.Count) = Key
'    End Function

'    Public Sub RemoveAll()
'        TmpHive.Clear()
'    End Sub

'    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As Object
'        Get
'            Item = TmpHive.Item(vntIndexKey)
'        End Get
'    End Property

'    Public Function Exists(ByVal vntIndexKey As Object) As Boolean
'        Exists = TmpHive.ContainsKey(vntIndexKey)
'    End Function

'    Public Sub Remove(ByVal vntIndexKey As Object)
'        TmpHive.Remove(vntIndexKey)
'    End Sub

'    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
'        Dim kv As Object
'        kv = TmpHive.GetEnumerator
'        Return kv
'    End Function

'    Public ReadOnly Property Count() As Integer
'        Get
'            Count = TmpHive.Count
'        End Get
'    End Property
'End Class

    'Public Class cExtendedInfo
    '    Private mvarNetPrice As Decimal
    '    Private mvarEstimatedRating As Single
    '    Private mvarBreakList As Collection
    '    Private mvarEstimatedRatingBuyingTarget As Single
    '    Public IsBooked As Boolean
    '    Public Estimation As Trinity.cPlannedSpot.EstEnum
    '    Public AirDate As Date
    '    Public MaM As Integer
    '    Public EstimationPeriod As String
    '    Public EstimatedOnPeriod As String
    '    Public ProgAfter As String
    '    Public Channel As String
    '    Public GrossPrice As Decimal
    '    Public GrossPrice30 As Decimal
    '    Public ChannelEstimate As Single
    '    Public Remark As String
    '    Public Solus As Single
    '    Public SolusFirst As Single
    '    Public Duration As Integer
    '    Public ID As String

    '    Public Property NetPrice() As Decimal
    '        Get
    '            Return mvarNetPrice
    '        End Get
    '        Set(ByVal value As Decimal)
    '            mvarNetPrice = value
    '        End Set
    '    End Property

    '    Public Property EstimatedRating() As Single
    '        Get
    '            Return mvarEstimatedRating
    '        End Get
    '        Set(ByVal value As Single)
    '            mvarEstimatedRating = value
    '        End Set
    '    End Property

    '    Public Property EstimatedRatingBuyingTarget() As Single
    '        Get
    '            Return mvarEstimatedRatingBuyingTarget
    '        End Get
    '        Set(ByVal value As Single)
    '            mvarEstimatedRatingBuyingTarget = value
    '        End Set
    '    End Property


    '    Public Property BreakList() As Collection
    '        Get
    '            BreakList = mvarBreakList
    '        End Get
    '        Set(ByVal value As Collection)
    '            mvarBreakList = value
    '        End Set
    '    End Property

    '    Public Sub New()
    '        IsBooked = False
    '    End Sub

    '    Protected Overrides Sub Finalize()
    '        MyBase.Finalize()
    '        mvarBreakList = Nothing
    '    End Sub
    'End Class

    'Public Class cBreak
    '    Public AirDate As Date
    '    Public MaM As Integer
    '    Public Duration As Integer
    '    Public ProgBefore As String
    '    Public ProgAfter As String
    '    Public BreakIdx As Long
    '    Public ID As String
    '    Public BreakList As String

    '    Private mvarChannel As String

    '    Public Property Channel() As cChannel
    '        Get
    '            Return Campaign.Channels(mvarChannel)
    '        End Get
    '        Set(ByVal value As cChannel)
    '            mvarChannel = value.ChannelName
    '        End Set
    '    End Property
    'End Class

    'Public Class cPeriod
    '    Public Period As String
    '    Public Adedge As New Connect.Breaks
    '    Public Breaks As Collection
    '    Public BreakCount As Long
    'End Class

    'Public Class cHive
    '    Implements IEnumerator

    '    Private Const csClassName As String = "CHive"

    '    ' Default behaviours of Hive
    '    Private Const DefaultInitialAlloc As Long = 100
    '    Private Const DefaultGrowthFactor As Double = 1.5

    '    Private GrowthFactor As Double
    '    Private InitialAlloc As Long

    '    ' Sentinel is Node(0)
    '    Private Const Sentinel As Long = 0

    '    ' Node Color
    '    Private Enum EColor
    '        Black
    '        Red
    '    End Enum

    '    ' fields associated with each node
    '    Private Structure ItemData
    '        Public lLeft As Long          ' Left child
    '        Public lRight As Long         ' Right child
    '        Public lParent As Long        ' Parent
    '        Public Color As EColor        ' red or black
    '        Public vKey As Object        ' item key
    '        Public vData As Object       ' item data
    '    End Structure

    '    Public Enum HiveErrors
    '        InvalidIndex = &H1
    '        KeyNotFound = &H2
    '        KeyCannotBeInteger = &H4
    '        DuplicateKey = &H8
    '        InvalidParameter = &H100
    '        KeyCannotBeBlankOrZero = &H1000
    '    End Enum
    '    Private Items() As ItemData     ' Array which stored all item

    '    ' support for FindFirst and FindNext
    '    Private StackIndex As Integer
    '    Private Stack(0 To 32) As Long
    '    Private NextNode As Long

    '    Private Root As Long            ' root of binary tree
    '    Private Node As cNode           ' class for allocating nodes

    '    Private lCount As Long          ' No of items
    '    Private lIndex() As Long        ' Used to map items to index

    '    Private mCompareMode As Microsoft.VisualBasic.CompareMethod

    '    Public Errors As cErrorCollection   ' Contains all the errors

    '    Public RaiseError As Boolean    ' True: Call Err.Raise; False: Don't
    '    Public AllowDuplicate As Boolean ' True: Allow duplicate; False: Don't

    '    ' GUID Key generation code
    '    Private Declare Function CoCreateGuid Lib _
    '        "OLE32.DLL" (ByVal pGuid As Guid) As Long
    '    Private Declare Function StringFromGUID2 Lib _
    '        "OLE32.DLL" (ByVal pGuid As Guid, _
    '        ByVal PointerToString As Long, _
    '        ByVal MaxLength As Long) As Long

    '    ' GUID Result
    '    Private Const GUID_OK As Long = 0

    '    Private EnumPointer As Integer = 0

    '    ' Structure to hold GUID
    '    'Private Structure GUID
    '    '    Public Guid1 As Long             ' 32 bit
    '    '    Public Guid2 As Integer          ' 16 bit
    '    '    Public Guid3 As Integer          ' 16 bit
    '    '    Public Guid4(0 To 7) As Byte             ' 64 bit
    '    'End Structure

    '    ' For Raw memory copy
    '    Private Declare Sub MoveMemory Lib "kernel32" _
    '          Alias "RtlMoveMemory" (ByVal dest As Long, _
    '    ByVal Source As Long, ByVal numBytes As Long)

    '    Public Function CreateGUIDKey() As String
    '        Return Guid.NewGuid.ToString
    '    End Function

    '    'Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
    '    '    Dim TmpItem As ItemData
    '    '    Dim TmpEnum As IEnumerator
    '    '    TmpItem = Items.GetEnumerator.Current
    '    'End Function

    '    Private Sub Raise(ByVal errno As HiveErrors, ByVal sLocation As String)
    '        '   inputs:
    '        '       errno       Error ID
    '        '       sLocation   Location of the error

    '        Dim sErrMsg As String = ""

    '        Select Case errno

    '            Case HiveErrors.InvalidIndex
    '                sErrMsg = "Invalid Index."
    '            Case HiveErrors.KeyNotFound
    '                sErrMsg = "Key not Found in the Hive."
    '            Case HiveErrors.KeyCannotBeInteger
    '                sErrMsg = "Key cannot be Integer or Long or Byte or any kind of Fixed Digit."
    '            Case HiveErrors.DuplicateKey
    '                sErrMsg = "Duplicate Key."
    '            Case HiveErrors.InvalidParameter
    '                sErrMsg = "Invalid Parameter."

    '        End Select

    '        If RaiseError Then
    '            Err.Raise(vbObjectError + 5000 + errno, csClassName + "." + sLocation, sErrMsg)
    '        End If
    '        If Errors Is Nothing Then
    '            Errors = New cErrorCollection
    '        End If
    '        Errors.Add(vbObjectError + 5000 + errno, sLocation, sErrMsg)

    '    End Sub

    '    Private Function GetKeyIndex(ByVal vKey As Object) As Long
    '        '   inputs:
    '        '       vKeysVal          vKeys of the node
    '        '   returns:
    '        '       the index of the item in the Array

    '        Select Case VarType(vKey)
    '            Case vbByte, vbInteger, vbLong
    '                If vKey < 0 Or vKey > lCount Then
    '                    Raise(HiveErrors.InvalidIndex, "GetIndex")
    '                Else
    '                    GetKeyIndex = lIndex(vKey)
    '                End If
    '            Case Else
    '                GetKeyIndex = FindNode(vKey)
    '                If GetKeyIndex = 0 Then Raise(HiveErrors.KeyNotFound, "FindNode")
    '        End Select
    '    End Function

    '    Private Function GetIndex(ByVal KeyIndex As Long) As Long
    '        Dim i As Long

    '        For i = 1 To lCount
    '            If lIndex(i) = KeyIndex Then
    '                GetIndex = i
    '                Exit Function
    '            End If
    '        Next
    '    End Function
    '    Private Function FindNode(ByVal KeyVal As Object) As Long
    '        '   inputs:
    '        '       Key                   ' designates key to find
    '        '   returns:
    '        '       index to node
    '        '   action:
    '        '       Search tree for designated key, and return index to node.
    '        '   errors:
    '        '       Key Not Found
    '        '
    '        Dim current As Long

    '        ' find node specified by key
    '        current = Root

    '        ' ------------------------------------
    '        ' if compare mode is binary
    '        ' then match exact key otherwise
    '        ' ignore case if key is a string
    '        ' ------------------------------------
    '        If mCompareMode <> vbBinaryCompare And VarType(KeyVal) = vbString Then
    '            KeyVal = LCase(KeyVal)
    '            Do While current <> Sentinel
    '                If LCase(Items(current).vKey) = KeyVal Then
    '                    FindNode = current
    '                    Exit Function
    '                Else
    '                    If KeyVal < LCase(Items(current).vKey) Then
    '                        current = Items(current).lLeft
    '                    Else
    '                        current = Items(current).lRight
    '                    End If
    '                End If
    '            Loop
    '        Else
    '            Do While current <> Sentinel
    '                If Items(current).vKey = KeyVal Then
    '                    FindNode = current
    '                    Exit Function
    '                Else
    '                    If KeyVal < Items(current).vKey Then
    '                        current = Items(current).lLeft
    '                    Else
    '                        current = Items(current).lRight
    '                    End If
    '                End If
    '            Loop

    '        End If
    '    End Function

    '    Private Sub RotateLeft(ByVal x As Long)
    '        '   inputs:
    '        '       x                     designates node
    '        '   action:
    '        '       perform a lLeft tree rotation about "x"
    '        '
    '        Dim y As Long

    '        ' rotate node x to lLeft

    '        y = Items(x).lRight

    '        ' establish x.lRight link
    '        Items(x).lRight = Items(y).lLeft
    '        If Items(y).lLeft <> Sentinel Then Items(Items(y).lLeft).lParent = x

    '        ' establish y.lParent link
    '        If y <> Sentinel Then Items(y).lParent = Items(x).lParent
    '        If Items(x).lParent <> 0 Then
    '            If x = Items(Items(x).lParent).lLeft Then
    '                Items(Items(x).lParent).lLeft = y
    '            Else
    '                Items(Items(x).lParent).lRight = y
    '            End If
    '        Else
    '            Root = y
    '        End If

    '        ' link x and y
    '        Items(y).lLeft = x
    '        If x <> Sentinel Then Items(x).lParent = y
    '    End Sub

    '    Private Sub RotateRight(ByVal x As Long)
    '        '   inputs:
    '        '       x                     designates node
    '        '   action:
    '        '       perform a lRight tree rotation about "x"
    '        '
    '        Dim y As Long

    '        ' rotate node x to lRight

    '        y = Items(x).lLeft

    '        ' establish x.lLeft link
    '        Items(x).lLeft = Items(y).lRight
    '        If Items(y).lRight <> Sentinel Then Items(Items(y).lRight).lParent = x

    '        ' establish y.lParent link
    '        If y <> Sentinel Then Items(y).lParent = Items(x).lParent
    '        If Items(x).lParent <> 0 Then
    '            If x = Items(Items(x).lParent).lRight Then
    '                Items(Items(x).lParent).lRight = y
    '            Else
    '                Items(Items(x).lParent).lLeft = y
    '            End If
    '        Else
    '            Root = y
    '        End If

    '        ' link x and y
    '        Items(y).lRight = x
    '        If x <> Sentinel Then Items(x).lParent = y
    '    End Sub

    '    Private Sub InsertFixup(ByRef x As Long)
    '        '   inputs:
    '        '       x                     designates node
    '        '   action:
    '        '       maintains red-black tree properties after inserting node x
    '        '
    '        Dim y As Long

    '        Do While x <> Root
    '            If Items(Items(x).lParent).Color <> EColor.Red Then Exit Do
    '            ' we have a violation
    '            If Items(x).lParent = Items(Items(Items(x).lParent).lParent).lLeft Then
    '                y = Items(Items(Items(x).lParent).lParent).lRight
    '                If Items(y).Color = EColor.Red Then

    '                    ' uncle is Red
    '                    Items(Items(x).lParent).Color = EColor.Black
    '                    Items(y).Color = EColor.Black
    '                    Items(Items(Items(x).lParent).lParent).Color = EColor.Red
    '                    x = Items(Items(x).lParent).lParent
    '                Else

    '                    ' uncle is Black
    '                    If x = Items(Items(x).lParent).lRight Then
    '                        ' make x a lLeft child
    '                        x = Items(x).lParent
    '                        RotateLeft(x)
    '                    End If

    '                    ' recolor and rotate
    '                    Items(Items(x).lParent).Color = EColor.Black
    '                    Items(Items(Items(x).lParent).lParent).Color = EColor.Red
    '                    RotateRight(Items(Items(x).lParent).lParent)
    '                End If
    '            Else

    '                ' mirror image of above code
    '                y = Items(Items(Items(x).lParent).lParent).lLeft
    '                If Items(y).Color = EColor.Red Then

    '                    ' uncle is Red
    '                    Items(Items(x).lParent).Color = EColor.Black
    '                    Items(y).Color = EColor.Black
    '                    Items(Items(Items(x).lParent).lParent).Color = EColor.Red
    '                    x = Items(Items(x).lParent).lParent
    '                Else

    '                    ' uncle is Black
    '                    If x = Items(Items(x).lParent).lLeft Then
    '                        x = Items(x).lParent
    '                        RotateRight(x)
    '                    End If
    '                    Items(Items(x).lParent).Color = EColor.Black
    '                    Items(Items(Items(x).lParent).lParent).Color = EColor.Red
    '                    RotateLeft(Items(Items(x).lParent).lParent)
    '                End If
    '            End If
    '        Loop
    '        Items(Root).Color = EColor.Black
    '    End Sub

    '    Public Function Add(ByRef Item As Object, Optional ByVal Key As Object = Nothing, Optional ByVal Before As Object = Nothing, Optional ByVal After As Object = Nothing)
    '        '   inputs:
    '        '       Item        Item to store
    '        '       Key         Key to use
    '        '       Before      The item before which this item will be inserted
    '        '       After      The item After which this item will be inserted
    '        '   action:
    '        '       Inserts Item with Key.
    '        '   error:
    '        '       DuplicateKey
    '        '
    '        Dim current As Long
    '        Dim p As Long
    '        Dim x As Long
    '        Dim i As Long
    '        Dim lItems As Long
    '        Dim strTempKey As String = ""  ' Used to store lcase key

    '        ' Validate Key
    '        If Key Is Nothing Then
    '            Key = CreateGUIDKey()
    '        Else
    '            Select Case VarType(Key)
    '                Case vbLong, vbInteger, vbByte
    '                    Raise(HiveErrors.KeyCannotBeInteger, "Add")
    '                    Return Nothing
    '                    Exit Function

    '                Case vbString
    '                    If Key = "" Then
    '                        Raise(HiveErrors.KeyCannotBeBlankOrZero, "Add")
    '                        Return Nothing
    '                        Exit Function
    '                    End If

    '            End Select
    '        End If
    '        ' allocate node for data and insert in tree
    '        If Node Is Nothing Then Init(InitialAlloc, GrowthFactor)

    '        ' find where node belongs
    '        current = Root
    '        p = 0

    '        ' ---------------------------------------------------------------
    '        ' Search hive if the key already exist. If exist then if duplicate
    '        ' allowed then accept otherwise get out. After serching look for a
    '        ' position where the new items key will be stored in the Red-Black
    '        ' tree. Thank you.
    '        ' ---------------------------------------------------------------
    '        If VarType(Key) = vbString Then strTempKey = LCase(Key)
    '        Do While current <> Sentinel
    '            If mCompareMode <> vbBinaryCompare And VarType(Key) = vbString Then

    '                If LCase(Items(current).vKey) = strTempKey Then
    '                    If Not AllowDuplicate Then
    '                        Raise(HiveErrors.DuplicateKey, "Add")
    '                        Return Nothing
    '                        Exit Function
    '                    End If
    '                End If

    '                p = current
    '                If strTempKey < LCase(Items(current).vKey) Then
    '                    current = Items(current).lLeft
    '                Else
    '                    current = Items(current).lRight
    '                End If

    '            Else
    '                If Items(current).vKey = Key Then
    '                    If Not AllowDuplicate Then
    '                        Raise(HiveErrors.DuplicateKey, "Add")
    '                        Return Nothing
    '                        Exit Function
    '                    End If
    '                End If

    '                p = current
    '                If Key < Items(current).vKey Then
    '                    current = Items(current).lLeft
    '                Else
    '                    current = Items(current).lRight
    '                End If
    '            End If


    '        Loop

    '        ' setup new node
    '        x = Node.Alloc()
    '        lItems = UBound(Items)
    '        If x > lItems Then
    '            ReDim Preserve Items(0 To lItems * GrowthFactor)
    '            ReDim Preserve lIndex(0 To (lItems * GrowthFactor) + 2)
    '        End If

    '        Items(x).lParent = p
    '        Items(x).lLeft = Sentinel
    '        Items(x).lRight = Sentinel
    '        Items(x).Color = EColor.Red

    '        ' Increase the counter. Increased value is
    '        ' required below
    '        lCount = lCount + 1
    '        ' Adjust position
    '        If Not Before Is Nothing Then
    '            Before = GetKeyIndex(Before)
    '            If Before = 0 Then
    '                Raise(HiveErrors.KeyNotFound, "Add")
    '                Return Nothing
    '                Exit Function
    '            End If
    '            i = GetIndex(Before)
    '            InsertItem(i)
    '            lIndex(i) = x

    '        ElseIf Not After Is Nothing Then
    '            After = GetKeyIndex(After)
    '            If After = 0 Then
    '                Raise(HiveErrors.KeyNotFound, "Add")
    '                Return Nothing
    '                Exit Function
    '            End If
    '            i = GetIndex(After) + 1
    '            InsertItem(i)
    '            lIndex(i) = x
    '        Else
    '            lIndex(lCount) = x
    '        End If

    '        ' copy fields to node
    '        Items(x).vKey = Key
    '        Items(x).vData = Item

    '        ' insert node in tree
    '        If p <> 0 Then
    '            If mCompareMode <> vbBinaryCompare And VarType(Key) = vbString Then
    '                If strTempKey < LCase(Items(p).vKey) Then
    '                    Items(p).lLeft = x
    '                Else
    '                    Items(p).lRight = x
    '                End If
    '            Else
    '                If Key < Items(p).vKey Then
    '                    Items(p).lLeft = x
    '                Else
    '                    Items(p).lRight = x
    '                End If
    '            End If
    '        Else
    '            Root = x
    '        End If

    '        InsertFixup(x)
    '        Return Nothing
    '    End Function

    '    Private Sub DeleteFixup(ByRef x As Long)
    '        '   inputs:
    '        '       x                     designates node
    '        '   action:
    '        '       maintains red-black tree properties after deleting a node
    '        '
    '        Dim w As Long

    '        Do While (x <> Root)
    '            If Items(x).Color <> EColor.Black Then Exit Do
    '            If x = Items(Items(x).lParent).lLeft Then
    '                w = Items(Items(x).lParent).lRight
    '                If Items(w).Color = EColor.Red Then
    '                    Items(w).Color = EColor.Black
    '                    Items(Items(x).lParent).Color = EColor.Red
    '                    RotateLeft(Items(x).lParent)
    '                    w = Items(Items(x).lParent).lRight
    '                End If
    '                If Items(Items(w).lLeft).Color = EColor.Black _
    '                And Items(Items(w).lRight).Color = EColor.Black Then
    '                    Items(w).Color = EColor.Red
    '                    x = Items(x).lParent
    '                Else
    '                    If Items(Items(w).lRight).Color = EColor.Black Then
    '                        Items(Items(w).lLeft).Color = EColor.Black
    '                        Items(w).Color = EColor.Red
    '                        RotateRight(w)
    '                        w = Items(Items(x).lParent).lRight
    '                    End If
    '                    Items(w).Color = Items(Items(x).lParent).Color
    '                    Items(Items(x).lParent).Color = EColor.Black
    '                    Items(Items(w).lRight).Color = EColor.Black
    '                    RotateLeft(Items(x).lParent)
    '                    x = Root
    '                End If
    '            Else
    '                w = Items(Items(x).lParent).lLeft
    '                If Items(w).Color = EColor.Red Then
    '                    Items(w).Color = EColor.Black
    '                    Items(Items(x).lParent).Color = EColor.Red
    '                    RotateRight(Items(x).lParent)
    '                    w = Items(Items(x).lParent).lLeft
    '                End If
    '                If Items(Items(w).lRight).Color = EColor.Black _
    '                And Items(Items(w).lLeft).Color = EColor.Black Then
    '                    Items(w).Color = EColor.Red
    '                    x = Items(x).lParent
    '                Else
    '                    If Items(Items(w).lLeft).Color = EColor.Black Then
    '                        Items(Items(w).lRight).Color = EColor.Black
    '                        Items(w).Color = EColor.Red
    '                        RotateLeft(w)
    '                        w = Items(Items(x).lParent).lLeft
    '                    End If
    '                    Items(w).Color = Items(Items(x).lParent).Color
    '                    Items(Items(x).lParent).Color = EColor.Black
    '                    Items(Items(w).lLeft).Color = EColor.Black
    '                    RotateRight(Items(x).lParent)
    '                    x = Root
    '                End If
    '            End If
    '        Loop
    '        Items(x).Color = EColor.Black
    '    End Sub

    '    Public Function Remove(ByVal KeyVal As Object) As Long
    '        '   inputs:
    '        '       KeyVal                key of node to delete
    '        '   action:
    '        '       Deletes record with key KeyVal.
    '        '   error:
    '        '       errKeyNotFound
    '        '
    '        Dim x As Long
    '        Dim y As Long
    '        Dim z As Long
    '        Dim i As Long

    '        z = GetKeyIndex(KeyVal) ' FindNode(KeyVal)
    '        If z = 0 Then
    '            Raise(HiveErrors.InvalidIndex, "Remove")
    '        End If

    '        '  delete node z from tree
    '        If Items(z).lLeft = Sentinel Or Items(z).lRight = Sentinel Then
    '            ' y has a Sentinel node as a child
    '            y = z
    '        Else
    '            ' find tree successor with a Sentinel node as a child
    '            y = Items(z).lRight
    '            Do While Items(y).lLeft <> Sentinel
    '                y = Items(y).lLeft
    '            Loop
    '        End If

    '        ' x is y's only child, and x may be a sentinel node
    '        If Items(y).lLeft <> Sentinel Then
    '            x = Items(y).lLeft
    '        Else
    '            x = Items(y).lRight
    '        End If

    '        ' remove y from the lParent chain
    '        Items(x).lParent = Items(y).lParent
    '        If Items(y).lParent <> 0 Then
    '            If y = Items(Items(y).lParent).lLeft Then
    '                Items(Items(y).lParent).lLeft = x
    '            Else
    '                Items(Items(y).lParent).lRight = x
    '            End If
    '        Else
    '            Root = x
    '        End If
    '        If y <> z Then
    '            Dim j As Long
    '            ' copy data fields from y to z
    '            ' z item now contains y item
    '            Items(z).vKey = Items(y).vKey
    '            Items(z).vData = Items(y).vData

    '            ' Swap index of z and y
    '            i = GetIndex(z)
    '            j = GetIndex(y)

    '            lIndex(i) = y
    '            lIndex(j) = z


    '        End If

    '        ' if we removed a black node, we need to do some fixup
    '        If Items(y).Color = EColor.Black Then DeleteFixup(x)

    '        Items(y).vData = Nothing
    '        Items(y).vData = Nothing
    '        Items(y).vKey = Nothing
    '        Items(y).vKey = Nothing

    '        ' Delete index of y
    '        i = GetIndex(y)
    '        LiftItem(i)
    '        Remove = i

    '        lIndex(lCount) = 0
    '        lCount = lCount - 1

    '        Node.Free(y)
    '    End Function

    '    Private Function GetNextNode() As Long
    '        '   returns:
    '        '       index to next node, 0 if none
    '        '   action:
    '        '       Finds index to next node.
    '        '
    '        Do While (NextNode <> 0 Or StackIndex <> 0)
    '            Do While NextNode <> 0
    '                StackIndex = StackIndex + 1
    '                Stack(StackIndex) = NextNode
    '                NextNode = Items(NextNode).lLeft
    '            Loop
    '            GetNextNode = Stack(StackIndex)
    '            StackIndex = StackIndex - 1
    '            NextNode = Items(GetNextNode).lRight
    '            Exit Function
    '        Loop
    '        Raise(HiveErrors.KeyNotFound, "GetNextNode")
    '    End Function


    '    Public Function FindFirst(ByRef KeyVal As Object) As Object
    '        '   outputs:
    '        '       KeyVal                key of node to find
    '        '   returns:
    '        '       record associated with key
    '        '   action:
    '        '       For sequential access, finds first record.
    '        '   errors:
    '        '       errKeyNotFound
    '        '
    '        Dim n As Long

    '        ' for sequential access, call FindFirst, followed by
    '        ' repeated calls to FindNext

    '        NextNode = Root
    '        n = GetNextNode()
    '        KeyVal = Items(n).vKey
    '        FindFirst = Items(n).vData
    '    End Function

    '    Public Function FindNext(ByRef KeyVal As Object) As Object
    '        '   outputs:
    '        '       KeyVal                record key
    '        '   returns:
    '        '       record associated with key
    '        '   action:
    '        '       For sequential access, finds next record.
    '        '   errors:
    '        '       errKeyNotFound
    '        '
    '        Dim n As Long

    '        ' for sequential access, call FindFirst, followed by
    '        ' repeated calls to FindNext

    '        n = GetNextNode()
    '        KeyVal = Items(n).vKey
    '        FindNext = Items(n).vData
    '    End Function

    '    Public Sub Init( _
    '            ByVal InitialAllocVal As Long, _
    '            ByVal GrowthFactorVal As Single)
    '        '   inputs:
    '        '       InitialAllocVal         initial value for allocating nodes
    '        '       GrowthFactorVal         amount to grow node storage space
    '        '   action:
    '        '       initialize tree
    '        '
    '        GrowthFactor = GrowthFactorVal

    '        ' allocate nodes
    '        ReDim Items(0 To InitialAllocVal)
    '        ReDim lIndex(0 To InitialAllocVal + 1)

    '        ' initialize root and sentinel
    '        Items(Sentinel).lLeft = Sentinel
    '        Items(Sentinel).lRight = Sentinel
    '        Items(Sentinel).lParent = 0
    '        Items(Sentinel).Color = EColor.Black
    '        Root = Sentinel

    '        ' startup node manager
    '        Node = New cNode
    '        Node.Init(InitialAllocVal, GrowthFactorVal)

    '        ' Initialize error container
    '        Errors = New cErrorCollection

    '        StackIndex = 0
    '        lCount = 0
    '    End Sub

    '    Public Function Clear() As Long
    '        '   action:
    '        '       Clears memory
    '        Dim i As Long

    '        If Node Is Nothing Then Exit Function
    '        Node = Nothing
    '        Errors = Nothing

    '        For i = 1 To lCount - 1
    '            Items(i).vData = Nothing
    '            Items(i).vData = Nothing
    '        Next

    '        For i = 1 To lCount - 1
    '            Items(i).vKey = Nothing
    '            Items(i).vKey = Nothing
    '        Next

    '        Erase Items
    '        lCount = 0
    '        Clear = lCount
    '    End Function

    '    Public Function Exist(ByVal vKey As Object) As Boolean
    '        '   action:
    '        '       Searches in the array for the specified item
    '        '   inputs:
    '        '       vKey        The key or Index of the item
    '        '   returns:
    '        '       True is item exist. Otherwise false

    '        Exist = GetKeyIndex(vKey) > 0

    '    End Function





    '    Default Public Property Item(ByVal vKey As Object) As Object
    '        '   action:
    '        '       Returns the item specified in vKey
    '        '   inputs:
    '        '       vKey        The key or Index of the item
    '        Get
    '            Dim lIndex As Long

    '            Item = Nothing
    '            lIndex = GetKeyIndex(vKey)
    '            If lIndex > 0 Then
    '                Item = Items(lIndex).vData
    '            Else
    '                Raise(HiveErrors.KeyNotFound, "Item")
    '            End If
    '        End Get
    '        Set(ByVal value As Object)
    '            Dim lIndex As Long

    '            lIndex = GetKeyIndex(vKey)
    '            If lIndex > 0 Then
    '                Items(lIndex).vData = value
    '            End If
    '        End Set
    '    End Property

    '    Public Property Key(ByVal Index As Long) As Object
    '        Get
    '            If Index < 0 Or Index > lCount Then
    '                Raise(HiveErrors.InvalidIndex, "Key[Read]")
    '                Return Nothing
    '            Else
    '                Return Items(lIndex(Index)).vKey
    '            End If
    '        End Get

    '        Set(ByVal value As Object)

    '            If Index < 0 Or Index > lCount Then
    '                Raise(HiveErrors.InvalidIndex, "Key[Assign]")
    '            Else
    '                If FindNode(value) <> 0 Then
    '                    If Not AllowDuplicate Then
    '                        Raise(HiveErrors.DuplicateKey, "Key[Assign]")
    '                    Else
    '                        Items(lIndex(Index)).vKey = value
    '                    End If
    '                End If
    '            End If
    '        End Set
    '    End Property

    '    Public ReadOnly Property Count() As Long
    '        Get
    '            Return lCount
    '        End Get
    '    End Property

    '    Private Sub LiftItem(ByVal i As Long)
    '        'Dim x As Long
    '        'For x = i To lCount - 1
    '        '    lIndex(x) = lIndex(x + 1)
    '        'Next
    '        MoveMemory(lIndex(i), lIndex(i + 1), (lCount - i) * 4)
    '    End Sub

    '    Private Sub InsertItem(ByVal i As Long)
    '        'Dim j As Long
    '        'For j = lCount To i + 1 Step -1
    '        '  lIndex(j) = lIndex(j - 1)
    '        'Next
    '        MoveMemory(lIndex(i + 1), lIndex(i), (lCount - i) * 4)
    '    End Sub

    '    Public Property CompareMode() As Microsoft.VisualBasic.CompareMethod
    '        Get
    '            Return mCompareMode
    '        End Get
    '        Set(ByVal value As Microsoft.VisualBasic.CompareMethod)
    '            mCompareMode = value
    '        End Set
    '    End Property

    '    Protected Overrides Sub Finalize()
    '        MyBase.Finalize()
    '        Clear()
    '        Errors = Nothing
    '    End Sub

    '    Public Sub New()
    '        InitialAlloc = DefaultInitialAlloc
    '        GrowthFactor = DefaultGrowthFactor
    '        mCompareMode = vbTextCompare
    '    End Sub

    '    Public ReadOnly Property Current() As Object Implements System.Collections.IEnumerator.Current
    '        Get
    '            Dim TmpItem As ItemData
    '            TmpItem = Items(EnumPointer)
    '            Return TmpItem.vData
    '        End Get
    '    End Property

    '    Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
    '        EnumPointer += 1
    '        If EnumPointer > UBound(Items) Then
    '            Return False
    '        Else
    '            Return True
    '        End If
    '    End Function

    '    Public Sub Reset() Implements System.Collections.IEnumerator.Reset
    '        EnumPointer = 0
    '    End Sub
    'End Class

    'Public Class cNode

    '    ' class CNode, node allocator

    '    Private FreeList() As Long              ' linked list of free nodes
    '    Private FreeHdr As Long                 ' head of free FreeList
    '    Private GrowthFactor As Single          ' how much to grow

    '    Public Sub Init(ByVal InitialAllocVal As Long, ByVal GrowthFactorVal As Single)
    '        '   inputs:
    '        '       InitialAlloc          initial allocation for nodes
    '        '       GrowthFactor          amount to grow allocation
    '        '   action:
    '        '       Allocates internal structures to manage node allocation.
    '        '
    '        Dim i As Long

    '        GrowthFactor = GrowthFactorVal
    '        ReDim FreeList(0 To InitialAllocVal)
    '        For i = 1 To InitialAllocVal - 1
    '            FreeList(i) = i + 1
    '        Next i
    '        FreeList(InitialAllocVal) = 0
    '        FreeHdr = 1
    '    End Sub

    '    Public Function Alloc() As Long
    '        '   returns:
    '        '       Allocated subscript.
    '        '   action:
    '        '       Allocates subscript.
    '        '
    '        Dim i As Long

    '        ' if Free is empty, reallocate array
    '        If FreeHdr = 0 Then
    '            FreeHdr = UBound(FreeList) + 1
    '            ReDim Preserve FreeList(0 To UBound(FreeList) * GrowthFactor)
    '            For i = FreeHdr To UBound(FreeList) - 1
    '                FreeList(i) = i + 1
    '            Next i
    '            FreeList(UBound(FreeList)) = 0
    '        End If

    '        ' return index to free node
    '        Alloc = FreeHdr
    '        FreeHdr = FreeList(FreeHdr)
    '    End Function

    '    Public Sub Free(ByVal i As Long)
    '        '   input:
    '        '       i             subscript to free
    '        '   action:
    '        '       Frees subscript for reuse.
    '        FreeList(i) = FreeHdr
    '        FreeHdr = i
    '    End Sub

    'End Class

    'Public Class cErrorCollection

    '    Private Structure ErrorInformation
    '        Public sDescription As String
    '        Public sHelpContext As String
    '        Public sHelpFile As String
    '        Public lNumber As Long
    '        Public sSource As String
    '    End Structure

    '    Private Errors() As ErrorInformation

    '    Public lErrors As Long

    '    Public Sub Clear()
    '        Erase Errors
    '        lErrors = 0
    '    End Sub

    '    Public Function IsError() As Boolean
    '        If Err.Number <> 0 Then
    '            Add(Err.Number, Err.Source, Err.Description, Err.HelpContext, Err.HelpFile)
    '            IsError = True
    '        Else
    '            IsError = False
    '        End If
    '    End Function

    '    Public Sub Add( _
    '    Optional ByVal lErrNumber As Long = 0, _
    '    Optional ByVal sSource As String = "", _
    '    Optional ByVal sDescription As String = "", _
    '    Optional ByVal sHelpContext As String = "", _
    '    Optional ByVal sHelpFile As String = "" _
    '        )

    '        If lErrNumber = 0 Then
    '            If Err.Number = 0 Then Exit Sub
    '            lErrNumber = Err.Number
    '        End If
    '        ReDim Preserve Errors(lErrors)
    '        With Errors(lErrors)
    '            .sDescription = sDescription
    '            .sHelpContext = sHelpContext
    '            .sHelpFile = sHelpFile
    '            .lNumber = lErrNumber
    '            .sSource = sSource
    '        End With
    '        lErrors = lErrors + 1
    '    End Sub


    '    Public Sub Remove(Optional ByVal Index As Object = Nothing)
    '        Dim i As Long

    '        If Index Is Nothing Then
    '            If lErrors > 0 Then
    '                ReDim Preserve Errors(lErrors - 1)
    '                lErrors = lErrors - 1
    '            End If
    '        ElseIf IsNumeric(Index) Then
    '            If Index < 0 Or Index > lErrors - 1 Then Exit Sub
    '            For i = Index To lErrors - 1
    '                Errors(i) = Errors(i + 1)
    '            Next
    '            ReDim Preserve Errors(lErrors - 1)
    '            lErrors = lErrors - 1
    '        End If

    '    End Sub

    '    Public ReadOnly Property ErrNumber(Optional ByVal Index As Object = Nothing) As Long
    '        Get
    '            If Index Is Nothing Then
    '                If lErrors > 0 Then Index = lErrors - 1
    '            ElseIf IsNumeric(Index) Then
    '                Return Errors(Index).lNumber
    '            End If
    '        End Get
    '    End Property

    '    Public ReadOnly Property Description(Optional ByVal Index As Object = Nothing) As Long
    '        Get
    '            If Index Is Nothing Then
    '                If lErrors > 0 Then Index = lErrors - 1
    '            ElseIf IsNumeric(Index) Then
    '                Description = Errors(Index).sDescription
    '            End If
    '        End Get
    '    End Property

    '    Public ReadOnly Property HelpContext(Optional ByVal Index As Object = Nothing) As Long
    '        Get
    '            If Index Is Nothing Then
    '                If lErrors > 0 Then Index = lErrors - 1
    '            ElseIf IsNumeric(Index) Then
    '                HelpContext = Errors(Index).sHelpContext
    '            End If
    '        End Get
    '    End Property

    '    Public ReadOnly Property HelpFile(Optional ByVal Index As Object = Nothing) As Long
    '        Get
    '            If Index Is Nothing Then
    '                If lErrors > 0 Then Index = lErrors - 1
    '            ElseIf IsNumeric(Index) Then
    '                HelpFile = Errors(Index).sHelpFile
    '            End If
    '        End Get
    '    End Property

    '    Public ReadOnly Property Source(Optional ByVal Index As Object = Nothing) As Long
    '        Get
    '            If Index Is Nothing Then
    '                If lErrors > 0 Then Index = lErrors - 1
    '            ElseIf IsNumeric(Index) Then
    '                Source = Errors(Index).sSource
    '            End If
    '        End Get
    '    End Property

    '    Public ReadOnly Property Count() As Long
    '        Get
    '            Return lErrors
    '        End Get
    '    End Property




    '    Protected Overrides Sub Finalize()
    '        MyBase.Finalize()
    '        Clear()
    '    End Sub
    'End Class

    'Public Class cFilter
    '    Private mCol As New Hashtable

    '    Public Property Data(ByVal Headline As String, ByVal SubItem As String) As Boolean
    '        Get
    '            If mCol.ContainsKey(Headline) Then
    '                Dim ItemCol As Hashtable = mCol(Headline)
    '                If ItemCol.ContainsKey(SubItem) Then
    '                    Return ItemCol(SubItem)
    '                Else
    '                    Return True
    '                End If
    '            Else
    '                Return True
    '            End If
    '        End Get
    '        Set(ByVal value As Boolean)
    '            Dim ItemCol As Hashtable
    '            If mCol.ContainsKey(Headline) Then
    '                ItemCol = mCol(Headline)
    '            Else
    '                ItemCol = New Hashtable
    '            End If
    '            If ItemCol.ContainsKey(SubItem) Then
    '                ItemCol(SubItem) = value
    '            Else
    '                ItemCol.Add(SubItem, value)
    '            End If
    '            If Not mCol.ContainsKey(Headline) Then
    '                mCol.Add(Headline, ItemCol)
    '            End If
    '        End Set
    '    End Property

    'End Class

    'Public Class cCompensations
    '    Implements IEnumerable

    '    Private mCol As New Collection
    '    Private _bookingtype As Trinity.cBookingType

    '    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
    '        Return mCol.GetEnumerator
    '    End Function

    '    Public ReadOnly Property Count() As Integer
    '        Get
    '            Return mCol.Count
    '        End Get
    '    End Property

    '    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cCompensation
    '        Get
    '            Return mCol(vntIndexKey)
    '        End Get
    '    End Property

    '    Public Sub Remove(ByVal vntIndexKey As Object)
    '        mCol.Remove(vntIndexKey)
    '    End Sub

    '    Public Function Add() As cCompensation
    '        Dim TmpComp As New cCompensation(_bookingtype)

    '        mCol.Add(TmpComp, TmpComp.ID)
    '        Return TmpComp

    '    End Function

    '    Public Sub New(ByVal Bookingtype As Trinity.cBookingType)
    '        _bookingtype = Bookingtype
    '    End Sub

    '    Public Function GetCompensationForDate(ByVal d As Date) As Single
    '        Dim TmpTRP As Single = 0

    '        For Each TmpComp As Trinity.cCompensation In mCol
    '            If d >= TmpComp.FromDate AndAlso d <= TmpComp.ToDate Then
    '                TmpTRP = TmpTRP + TmpComp.TRPs / (TmpComp.ToDate.ToOADate - TmpComp.FromDate.ToOADate + 1)
    '            End If
    '        Next
    '        Return TmpTRP

    '    End Function

    '    Public Function GetCompensationForDateInMainTarget(ByVal d As Date) As Single
    '        Dim TmpTRP As Single = 0

    '        For Each TmpComp As Trinity.cCompensation In mCol
    '            If d >= TmpComp.FromDate AndAlso d <= TmpComp.ToDate Then
    '                TmpTRP = TmpTRP + (TmpComp.TRPMainTarget / (TmpComp.ToDate.ToOADate - TmpComp.FromDate.ToOADate + 1))
    '            End If
    '        Next
    '        Return TmpTRP

    '    End Function

    '    Public Function GetCompensationForDateInAllAdults(ByVal d As Date) As Single
    '        Dim TmpTRP As Single = 0

    '        For Each TmpComp As Trinity.cCompensation In mCol
    '            If d >= TmpComp.FromDate AndAlso d <= TmpComp.ToDate Then
    '                TmpTRP = TmpTRP + (TmpComp.TRPAllAdults / (TmpComp.ToDate.ToOADate - TmpComp.FromDate.ToOADate + 1))
    '            End If
    '        Next
    '        Return TmpTRP

    '    End Function

    'End Class

    'Public Class cCompensation
    '    Private mvarID As String
    '    Private _fromDate As Date
    '    Private _toDate As Date
    '    Private _trps As Single
    '    Private _comment As String
    '    Private _bookingType As Trinity.cBookingType

    '    Public ReadOnly Property Bookingtype() As Trinity.cBookingType
    '        Get
    '            Return _bookingType
    '        End Get
    '    End Property

    '    Public Property Comment() As String
    '        Get
    '            Return _comment
    '        End Get
    '        Set(ByVal value As String)
    '            _comment = value
    '        End Set
    '    End Property

    '    Public Property TRPs() As Single
    '        Get
    '            Return _trps
    '        End Get
    '        Set(ByVal value As Single)
    '            _trps = value
    '        End Set
    '    End Property

    '    Public Property ToDate() As Date
    '        Get
    '            Return _toDate
    '        End Get
    '        Set(ByVal value As Date)
    '            _toDate = value
    '        End Set
    '    End Property

    '    Public Property FromDate() As Date
    '        Get
    '            Return _fromDate
    '        End Get
    '        Set(ByVal value As Date)
    '            _fromDate = value
    '        End Set
    '    End Property

    '    Public ReadOnly Property ID() As String
    '        Get
    '            Return mvarID
    '        End Get
    '    End Property

    '    Public Sub New(ByVal Bookingtype As Trinity.cBookingType)
    '        mvarID = Guid.NewGuid.ToString
    '        _bookingType = Bookingtype
    '    End Sub

    '    Public Function TRPMainTarget() As Single
    '        Return _trps * (_bookingType.IndexMainTarget / 100) * _bookingType.BuyingTarget.UniIndex(True)
    '    End Function

    '    Public Function TRPAllAdults() As Single
    '        Return _trps * (_bookingType.IndexAllAdults / 100) * _bookingType.BuyingTarget.UniIndex(True)
    '    End Function
    'End Class
'
'End Namespace
