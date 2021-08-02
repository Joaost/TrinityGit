Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cPlannedSpot
        Implements IDetectsProblems

        'a planned spot is booked and confirmed by the channel but not aired

        Public Enum EstEnum
            EstNotOk = 0
            EstSemiOk = 1
            EstOk = 2
        End Enum

        Private mvarChannel As cChannel 'the channel where the spot is beeing aired
        Private mvarChannelID As String
        Private mvarAirDate As Long 'the planned air date
        Private mvarMaM As Integer 'Time, Minutes After Midnight
        Private mvarProgBefore As String 'the program that is aired before the commercial (optional)
        Private mvarProgAfter As String 'the program that is aired after the commercial (optinal)
        Private mvarProgramme As String ' the program that is containing the ommercial break
        Private mvarAdvertiser As String
        Private mvarProduct As String 'the product that is advertised
        Private mvarFilmcode As String 'the film code to the film
        Private mvarFilm As cFilm 'a commercial film
        Private mvarMyRatingMain As Single
        Private mvarMyRatingSecond As Single = -1
        Private mvarMyRatingThird As Single = -1
        Private mvarMyRatingBuying As Single = -1
        Private mvarMyRatingAllAdults As Single = -1
        Private mvarIndex As Integer
        Private mvarMatchedSpot As cActualSpot
        Private mvarMatchedBookedSpot As cBookedSpot
        Private mvarSpotLength As Byte 'the length of the spot booked
        Private mvarSpotType As Byte
        Private mvarWeek As cWeek 'the week where the spot is located
        Private mvarBookingtype As cBookingType 'the booking type used
        Private mvarPriceNet As Decimal 'net price for the spot
        Private mvarPriceGross As Decimal 'gross price for the spot
        Private mvarID As String
        Private mvarRating As Single
        Private mvarAddedValue As cAddedValue
        Private Main As cKampanj 'the main campaign
        Private mvarEstimation As EstEnum
        Private mvarRemark As String
        Public Matched As Byte
        Private mvarPlacement As PlaceEnum

        Public Enum PlaceEnum
            PlaceAny = 1
            PlaceTop = 2
            PlaceTail = 4
            PlaceTopOrTail = 8
            PlaceCentreBreak = 16
            PlaceStartBreak = 32
            PlaceEndBreak = 64
            PlaceRoadBlock = 128
            PlaceRequestedBreak = 256
            PlaceSecond = 512
            PlaceSecondLast = 1024
        End Enum

        'Public BreakList As Collection
        Public BreakList As ArrayList

        Public Enum PlannedTargetEnum
            pteMainTarget = 0
            pteSecondTarget = 1
            pteThirdTarget = 2
            pteAllAdults = 3
            pteBuyingTarget = 4
        End Enum

        'returns the main campaign
        Friend WriteOnly Property MainObject()
            Set(ByVal value)
                Main = value
            End Set
        End Property

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            colXml.SetAttribute("ID", Me.ID) ' as String
            colXml.SetAttribute("Channel", Me.Channel.ChannelName) ' as cChannel
            colXml.SetAttribute("ChannelID", Me.ChannelID) ' as String
            colXml.SetAttribute("BookingType", Me.Bookingtype.Name) ' as cBookingType
            colXml.SetAttribute("Week", Me.Week.Name) ' as cWeek
            colXml.SetAttribute("AirDate", Me.AirDate) ' as Long
            colXml.SetAttribute("MaM", Me.MaM) ' as Integer
            colXml.SetAttribute("ProgBefore", Me.ProgBefore) ' as String
            colXml.SetAttribute("ProgAfter", Me.ProgAfter) ' as String
            colXml.SetAttribute("Programme", Me.Programme) ' as String
            colXml.SetAttribute("Advertiser", Me.Advertiser) ' as String
            colXml.SetAttribute("Product", Me.Product) ' as String
            colXml.SetAttribute("Filmcode", Me.Filmcode) ' as String
            colXml.SetAttribute("RatingBuyTarget", Me.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)) ' as Single
            colXml.SetAttribute("Estimation", Me.Estimation)
            colXml.SetAttribute("MyRating", Me.MyRating) ' as Single
            colXml.SetAttribute("Index", Me.Index) ' as Integer
            colXml.SetAttribute("SpotLength", Me.SpotLength) ' as Byte
            colXml.SetAttribute("SpotType", Me.SpotType) ' as Byte
            colXml.SetAttribute("PriceNet", Me.PriceNet) ' as Currency
            colXml.SetAttribute("PriceGross", Me.PriceGross) ' as Currency
            colXml.SetAttribute("Remark", Me.Remark)
            colXml.SetAttribute("Placement", Me.Placement)
            colXml.SetAttribute("BreakID", Me.breakID)
            If Not Me.AddedValue Is Nothing Then
                colXml.SetAttribute("AddedValue", Me.AddedValue.ID)
            End If

            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving the Planned spot " & Me.ID)
            Return False
        End Function


        Function DateTimeSerial() As Single
            Return Helper.DateTimeSerial(Date.FromOADate(AirDate), MaM)
        End Function

        Friend Property ID() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : ID
            ' DateTime  : 2003-07-18 10:06
            ' Author    : joho
            ' Purpose   : Returns/sets the ID to be used when saving relations between spots
            '             to file
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo ID_Error
                ID = mvarID
                On Error GoTo 0
                Exit Property
ID_Error:
                Err.Raise(Err.Number, "cPlannedSpot: ID", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo ID_Error
                mvarID = value
                On Error GoTo 0
                Exit Property
ID_Error:
                Err.Raise(Err.Number, "cPlannedSpot: ID", Err.Description)
            End Set
        End Property

        Friend Property ChannelID() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : ChannelID
            ' DateTime  : 2003-07-02 14:39
            ' Author    : joho
            ' Purpose   : Returns/sets the ChannelID. If a ChannelID is set the appropriate
            '             channel will be mapped to the Channel property
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo ChannelID_Error
                ChannelID = mvarChannelID
                On Error GoTo 0
                Exit Property
ChannelID_Error:
                Err.Raise(Err.Number, "cPlannedSpot: ChannelID", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo ChannelID_Error
                mvarChannelID = value
                On Error GoTo 0
                Exit Property
ChannelID_Error:
                Err.Raise(Err.Number, "cPlannedSpot: ChannelID", Err.Description)
            End Set
        End Property

        Friend Property Filmcode() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : Filmcode
            ' DateTime  : 2003-07-02 14:48
            ' Author    : joho
            ' Purpose   : Returns/sets the Filmcode. If a Filmcode is set the appropriate film
            '             will be mapped to the Film Property
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Filmcode_Error
                Filmcode = mvarFilmcode
                On Error GoTo 0
                Exit Property
Filmcode_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Filmcode", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo Filmcode_Error
                mvarFilmcode = value
                On Error GoTo 0
                Exit Property
Filmcode_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Filmcode", Err.Description)
            End Set
        End Property

        Public Property AddedValue() As cAddedValue
            Get
                Return mvarAddedValue
            End Get
            Set(ByVal value As cAddedValue)
                mvarAddedValue = value
            End Set
        End Property

        Public Property Week() As cWeek
            '---------------------------------------------------------------------------------------
            ' Procedure : Week
            ' DateTime  : 2003-07-15 12:26
            ' Author    : joho
            ' Purpose   : Pointer to a cWeek representing the week that this spot will be
            '             aired
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Week_Error
                Week = mvarWeek
                On Error GoTo 0
                Exit Property
Week_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Week", Err.Description)
            End Get
            Set(ByVal value As cWeek)
                On Error GoTo Week_Error
                mvarWeek = value
                On Error GoTo 0
                Exit Property
Week_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Week", Err.Description)
            End Set
        End Property

        Public Property AirDate() As Long
            '---------------------------------------------------------------------------------------
            ' Procedure : AirDate
            ' DateTime  : 2003-07-15 12:27
            ' Author    : joho
            ' Purpose   : Returns/sets the date when the spot is due to be aired
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo AirDate_Error
                AirDate = mvarAirDate
                On Error GoTo 0
                Exit Property
AirDate_Error:
                Err.Raise(Err.Number, "cPlannedSpot: AirDate", Err.Description)
            End Get
            Set(ByVal value As Long)
                On Error GoTo AirDate_Error
                mvarAirDate = value
                On Error GoTo 0
                Exit Property
AirDate_Error:
                Err.Raise(Err.Number, "cPlannedSpot: AirDate", Err.Description)
            End Set
        End Property

        Public Property Bookingtype() As cBookingType
            '---------------------------------------------------------------------------------------
            ' Procedure : BookingType
            ' DateTime  : 2003-07-15 12:51
            ' Author    : joho
            ' Purpose   : Pointer to a cBookingType representing the Booking Type this spot
            '             is a part of
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo BookingType_Error
                Bookingtype = mvarBookingtype
                On Error GoTo 0
                Exit Property
BookingType_Error:
                Err.Raise(Err.Number, "cPlannedSpot: BookingType", Err.Description)
            End Get
            Set(ByVal value As cBookingType)
                On Error GoTo BookingType_Error
                mvarBookingtype = value
                On Error GoTo 0
                Exit Property
BookingType_Error:
                Err.Raise(Err.Number, "cPlannedSpot: BookingType", Err.Description)
            End Set
        End Property

        Public Property Channel() As cChannel
            '---------------------------------------------------------------------------------------
            ' Procedure : Channel
            ' DateTime  : 2003-07-18 09:49
            ' Author    : joho
            ' Purpose   : Pointer to a cChannel representing the channel where this spot is
            '             planned
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Channel_Error
                Channel = mvarChannel
                On Error GoTo 0
                Exit Property
Channel_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Channel", Err.Description)
            End Get
            Set(ByVal value As cChannel)
                On Error GoTo Channel_Error
                mvarChannel = value
                On Error GoTo 0
                Exit Property
Channel_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Channel", Err.Description)
            End Set
        End Property

        Public Property MaM() As Integer
            '---------------------------------------------------------------------------------------
            ' Procedure : MaM
            ' DateTime  : 2003-07-18 09:49
            ' Author    : joho
            ' Purpose   : Returns/sets the Minute after Midnight that this spot will be aired
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo MaM_Error
                MaM = mvarMaM
                On Error GoTo 0
                Exit Property
MaM_Error:
                Err.Raise(Err.Number, "cPlannedSpot: MaM", Err.Description)
            End Get
            Set(ByVal value As Integer)
                On Error GoTo MaM_Error
                mvarMaM = value
                On Error GoTo 0
                Exit Property
MaM_Error:
                Err.Raise(Err.Number, "cPlannedSpot: MaM", Err.Description)
            End Set
        End Property

        Public Property ProgBefore() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : ProgBefore
            ' DateTime  : 2003-07-18 09:49
            ' Author    : joho
            ' Purpose   : Returns/sets the programme to be aired before the break where this
            '             spot will be placed
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo ProgBefore_Error
                ProgBefore = mvarProgBefore
                On Error GoTo 0
                Exit Property
ProgBefore_Error:
                Err.Raise(Err.Number, "cPlannedSpot: ProgBefore", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo ProgBefore_Error
                mvarProgBefore = value
                On Error GoTo 0
                Exit Property
ProgBefore_Error:
                Err.Raise(Err.Number, "cPlannedSpot: ProgBefore", Err.Description)
            End Set
        End Property

        Public Property ProgAfter() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : ProgAfter
            ' DateTime  : 2003-07-18 09:49
            ' Author    : joho
            ' Purpose   : Returns/sets the programme to be aired after the break where this
            '             spot will be aired
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo ProgAfter_Error
                ProgAfter = mvarProgAfter
                On Error GoTo 0
                Exit Property
ProgAfter_Error:
                Err.Raise(Err.Number, "cPlannedSpot: ProgAfter", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo ProgAfter_Error
                mvarProgAfter = value
                On Error GoTo 0
                Exit Property
ProgAfter_Error:
                Err.Raise(Err.Number, "cPlannedSpot: ProgAfter", Err.Description)
            End Set
        End Property

        Public Property Programme() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : Programme
            ' DateTime  : 2003-07-18 09:49
            ' Author    : joho
            ' Purpose   : Returns/sets the programme that this spot is considered to belong
            '             to. Most commonly the same as ProgAfter.
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Programme_Error
                Programme = mvarProgramme
                On Error GoTo 0
                Exit Property
Programme_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Programme", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo Programme_Error
                mvarProgramme = value
                On Error GoTo 0
                Exit Property
Programme_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Programme", Err.Description)
            End Set
        End Property

        Public Property Advertiser() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : Advertiser
            ' DateTime  : 2003-07-18 09:49
            ' Author    : joho
            ' Purpose   : Returns/sets the name of the Advertiser for this spot
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Advertiser_Error
                Advertiser = mvarAdvertiser
                On Error GoTo 0
                Exit Property
Advertiser_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Advertiser", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo Advertiser_Error
                mvarAdvertiser = value
                On Error GoTo 0
                Exit Property
Advertiser_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Advertiser", Err.Description)
            End Set
        End Property

        Public Property Product() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : Product
            ' DateTime  : 2003-07-18 09:50
            ' Author    : joho
            ' Purpose   : Returns/sets the name of the Product for this spot
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Product_Error
                Product = mvarProduct
                On Error GoTo 0
                Exit Property
Product_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Product", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo Product_Error
                mvarProduct = value
                On Error GoTo 0
                Exit Property
Product_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Product", Err.Description)
            End Set
        End Property

        Public ReadOnly Property Film() As cFilm
            '---------------------------------------------------------------------------------------
            ' Procedure : Film
            ' DateTime  : 2003-07-18 09:50
            ' Author    : joho
            ' Purpose   : Pointer to a cFilm corresponding to the film to be aired
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Film_Error
                Film = mvarBookingtype.Weeks(1).Films(mvarFilmcode)
                On Error GoTo 0
                Exit Property
Film_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Film", Err.Description)
            End Get
            '            Set(ByVal value As cFilm)
            '                On Error GoTo Film_Error
            '                mvarFilm = value
            '                On Error GoTo 0
            '                Exit Property
            'Film_Error:
            '                Err.Raise(Err.Number, "cPlannedSpot: Film", Err.Description)
            '            End Set
        End Property

        Public Property MyRating(Optional ByVal Target As PlannedTargetEnum = 0) As Single
            '---------------------------------------------------------------------------------------
            ' Procedure : MyRating
            ' DateTime  : 2003-07-18 09:50
            ' Author    : joho
            ' Purpose   : Returns/sets the users own rating in the main target and universe
            '---------------------------------------------------------------------------------------
            '
            Get
                Dim UI As Single

                On Error GoTo MyRating_Error

                Select Case Target
                    Case Is = PlannedTargetEnum.pteMainTarget
                        MyRating = mvarMyRatingMain
                    Case Is = PlannedTargetEnum.pteSecondTarget
                        If mvarMyRatingSecond = -1 Then
                            MyRating = (mvarBookingtype.IndexSecondTarget / mvarBookingtype.IndexMainTarget) * mvarMyRatingMain
                        Else
                            MyRating = mvarMyRatingSecond
                        End If
                    Case Is = PlannedTargetEnum.pteThirdTarget
                        If mvarMyRatingThird = -1 Then
                            MyRating = (mvarBookingtype.IndexSecondTarget / mvarBookingtype.IndexMainTarget) * mvarMyRatingMain
                        Else
                            MyRating = mvarMyRatingThird
                        End If
                    Case Is = PlannedTargetEnum.pteBuyingTarget
                        If mvarMyRatingBuying = -1 Then
                            If mvarMyRatingMain = 0 Then
                                MyRating = 0
                            Else
                                MyRating = (mvarMyRatingMain / (mvarBookingtype.IndexMainTarget / 100))
                            End If
                        Else
                            MyRating = mvarMyRatingBuying
                        End If
                    Case Is = PlannedTargetEnum.pteAllAdults
                        If mvarMyRatingAllAdults = -1 Then
                            MyRating = (mvarBookingtype.IndexAllAdults / mvarBookingtype.IndexMainTarget) * mvarMyRatingMain
                        Else
                            MyRating = mvarMyRatingAllAdults
                        End If
                End Select

                On Error GoTo 0
                Exit Property
MyRating_Error:
                Err.Raise(Err.Number, "cPlannedSpot: MyRating", Err.Description)
            End Get
            Set(ByVal value As Single)
                Dim UI As Single

                On Error GoTo MyRating_Error

                If Target = PlannedTargetEnum.pteMainTarget Then
                    mvarMyRatingMain = value
                ElseIf Target = PlannedTargetEnum.pteSecondTarget Then
                    mvarMyRatingSecond = value
                ElseIf Target = PlannedTargetEnum.pteThirdTarget Then
                    mvarMyRatingThird = value
                ElseIf Target = PlannedTargetEnum.pteAllAdults Then
                    mvarMyRatingAllAdults = value
                ElseIf Target = PlannedTargetEnum.pteBuyingTarget Then
                    mvarMyRatingBuying = value
                End If
                On Error GoTo 0
                Exit Property
MyRating_Error:
                Err.Raise(Err.Number, "cPlannedSpot: MyRating", Err.Description)
            End Set
        End Property

        Public Property Index() As Integer
            '---------------------------------------------------------------------------------------
            ' Procedure : Index
            ' DateTime  : 2003-07-18 09:50
            ' Author    : joho
            ' Purpose   : Returns/sets a standard index to be multiplied with all spots
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Index_Error
                Index = mvarIndex
                On Error GoTo 0
                Exit Property
Index_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Index", Err.Description)
            End Get
            Set(ByVal value As Integer)
                On Error GoTo Index_Error
                mvarIndex = value
                On Error GoTo 0
                Exit Property
Index_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Index", Err.Description)
            End Set
        End Property

        Public Property MatchedSpot() As cActualSpot
            '---------------------------------------------------------------------------------------
            ' Procedure : MatchedSpot
            ' DateTime  : 2003-07-18 09:50
            ' Author    : joho
            ' Purpose   : Pointer to a cActualSpot corresponding to the spot that was actually
            '             aired
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo MatchedSpot_Error
                MatchedSpot = mvarMatchedSpot
                On Error GoTo 0
                Exit Property
MatchedSpot_Error:
                Err.Raise(Err.Number, "cPlannedSpot: MatchedSpot", Err.Description)
            End Get
            Set(ByVal value As cActualSpot)
                On Error GoTo MatchedSpot_Error
                mvarMatchedSpot = value
                On Error GoTo 0
                Exit Property
MatchedSpot_Error:
                Err.Raise(Err.Number, "cPlannedSpot: MatchedSpot", Err.Description)
            End Set
        End Property

        Public Property MatchedBookedSpot() As cBookedSpot
            '---------------------------------------------------------------------------------------
            ' Procedure : MatchedSpot
            ' DateTime  : 2003-07-18 09:50
            ' Author    : joho
            ' Purpose   : Pointer to a cBookedSpot corresponding to the spot that was originally
            '             booked
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo MatchedSpot_Error
                Return mvarMatchedBookedSpot
                On Error GoTo 0
                Exit Property
MatchedSpot_Error:
                Err.Raise(Err.Number, "cPlannedSpot: MatchedSpot", Err.Description)
            End Get
            Set(ByVal value As cBookedSpot)
                On Error GoTo MatchedSpot_Error
                mvarMatchedBookedSpot = value
                On Error GoTo 0
                Exit Property
MatchedSpot_Error:
                Err.Raise(Err.Number, "cPlannedSpot: MatchedSpot", Err.Description)
            End Set
        End Property

        Public Property SpotLength() As Byte
            '---------------------------------------------------------------------------------------
            ' Procedure : SpotLength
            ' DateTime  : 2003-07-18 09:50
            ' Author    : joho
            ' Purpose   : Returns/sets the spot length of the spot
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo SpotLength_Error
                SpotLength = mvarSpotLength
                On Error GoTo 0
                Exit Property
SpotLength_Error:
                Err.Raise(Err.Number, "cPlannedSpot: SpotLength", Err.Description)
            End Get
            Set(ByVal value As Byte)
                On Error GoTo SpotLength_Error
                mvarSpotLength = value
                On Error GoTo 0
                Exit Property
SpotLength_Error:
                Err.Raise(Err.Number, "cPlannedSpot: SpotLength", Err.Description)
            End Set
        End Property

        Public Property SpotType() As Byte
            '---------------------------------------------------------------------------------------
            ' Procedure : SpotType
            ' DateTime  : 2003-07-18 09:50
            ' Author    : joho
            ' Purpose   : Returns/sets an index number indicating what type of spot this is:
            '
            '               0 - RBS
            '               1 - Specific
            '               2 - Last minute
            '
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo SpotType_Error
                SpotType = mvarSpotType
                On Error GoTo 0
                Exit Property
SpotType_Error:
                Err.Raise(Err.Number, "cPlannedSpot: SpotType", Err.Description)
            End Get
            Set(ByVal value As Byte)
                On Error GoTo SpotType_Error
                mvarSpotType = value
                On Error GoTo 0
                Exit Property
SpotType_Error:
                Err.Raise(Err.Number, "cPlannedSpot: SpotType", Err.Description)
            End Set
        End Property

        Public Property PriceNet() As Decimal
            '---------------------------------------------------------------------------------------
            ' Procedure : PriceNet
            ' DateTime  : 2003-07-18 09:51
            ' Author    : joho
            ' Purpose   : Returns/sets the net price of this spot
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo PriceNet_Error
                PriceNet = mvarPriceNet
                On Error GoTo 0
                Exit Property
PriceNet_Error:
                Err.Raise(Err.Number, "cPlannedSpot: PriceNet", Err.Description)
            End Get
            Set(ByVal value As Decimal)
                On Error GoTo PriceNet_Error
                mvarPriceNet = value
                On Error GoTo 0
                Exit Property
PriceNet_Error:
                Err.Raise(Err.Number, "cPlannedSpot: PriceNet", Err.Description)
            End Set
        End Property

        Public Property PriceGross() As Decimal
            '---------------------------------------------------------------------------------------
            ' Procedure : PriceGross
            ' DateTime  : 2003-07-18 10:04
            ' Author    : joho
            ' Purpose   : Returns/sets the gross price for this spot
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo PriceGross_Error
                PriceGross = mvarPriceGross
                On Error GoTo 0
                Exit Property
PriceGross_Error:
                Err.Raise(Err.Number, "cPlannedSpot: PriceGross", Err.Description)
            End Get
            Set(ByVal value As Decimal)
                On Error GoTo PriceGross_Error
                mvarPriceGross = value
                On Error GoTo 0
                Exit Property
PriceGross_Error:
                Err.Raise(Err.Number, "cPlannedSpot: PriceGross", Err.Description)
            End Set
        End Property

        Public Property ChannelRating(Optional ByVal Target As PlannedTargetEnum = 4) As Single
            '---------------------------------------------------------------------------------------
            ' Procedure : Rating
            ' DateTime  : 2003-07-22 21:10
            ' Author    : joho
            ' Purpose   :
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Rating_Error

                If Target = PlannedTargetEnum.pteBuyingTarget Then
                    ChannelRating = mvarRating
                ElseIf Target = PlannedTargetEnum.pteMainTarget Then
                    ChannelRating = mvarRating * mvarBookingtype.BuyingTarget.UniIndex(cTarget.EnumUni.uniMainCmp) * (mvarBookingtype.IndexMainTarget / 100)
                ElseIf Target = PlannedTargetEnum.pteSecondTarget Then
                    ChannelRating = mvarRating * mvarBookingtype.BuyingTarget.UniIndex(cTarget.EnumUni.uniMainCmp) * (mvarBookingtype.IndexSecondTarget / 100)
                ElseIf Target = PlannedTargetEnum.pteAllAdults Then
                    ChannelRating = mvarRating * mvarBookingtype.BuyingTarget.UniIndex(cTarget.EnumUni.uniMainCmp) * (mvarBookingtype.IndexAllAdults / 100)
                End If

                On Error GoTo 0
                Exit Property

Rating_Error:

                Err.Raise(Err.Number, "cPlannedSpot: Rating", Err.Description)
            End Get
            Set(ByVal value As Single)
                On Error GoTo Rating_Error

                If Target = PlannedTargetEnum.pteBuyingTarget Then
                    mvarRating = value
                ElseIf Target = PlannedTargetEnum.pteMainTarget Then
                    If (mvarChannel.MainTarget.UniIndex(cTarget.EnumUni.uniMainCmp) * (mvarBookingtype.IndexMainTarget / 100)) <> 0 Then
                        mvarRating = value / (mvarChannel.MainTarget.UniIndex(cTarget.EnumUni.uniMainCmp) * (mvarBookingtype.IndexMainTarget / 100))
                    Else
                        mvarRating = 0
                    End If
                ElseIf Target = PlannedTargetEnum.pteSecondTarget Then
                    If (mvarChannel.MainTarget.UniIndex(cTarget.EnumUni.uniMainCmp) * (mvarBookingtype.IndexSecondTarget / 100)) <> 0 Then
                        mvarRating = value / (mvarChannel.MainTarget.UniIndex(cTarget.EnumUni.uniMainCmp) * (mvarBookingtype.IndexSecondTarget / 100))
                    Else
                        mvarRating = 0
                    End If
                ElseIf Target = PlannedTargetEnum.pteAllAdults Then
                    If (mvarChannel.MainTarget.UniIndex(cTarget.EnumUni.uniMainCmp) * (mvarBookingtype.IndexAllAdults / 100)) <> 0 Then
                        mvarRating = value / (mvarChannel.MainTarget.UniIndex(cTarget.EnumUni.uniMainCmp) * (mvarBookingtype.IndexAllAdults / 100))
                    Else
                        mvarRating = 0
                    End If
                End If
                On Error GoTo 0
                Exit Property
Rating_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Rating", Err.Description)
            End Set
        End Property

        Public Property Estimation() As EstEnum
            '---------------------------------------------------------------------------------------
            ' Procedure : Estimation
            ' DateTime  : 2003-11-04 11:39
            ' Author    : joho
            ' Purpose   : Returns/sets how well the spot has been estimated
            '---------------------------------------------------------------------------------------
            Get
                On Error GoTo Estimation_Error
                Estimation = mvarEstimation
                On Error GoTo 0
                Exit Property
Estimation_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Estimation", Err.Description)
            End Get
            Set(ByVal value As EstEnum)
                On Error GoTo Estimation_Error
                mvarEstimation = value
                On Error GoTo 0
                Exit Property
Estimation_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Estimation", Err.Description)

            End Set
        End Property

        Public Property Remark() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : Remark
            ' DateTime  : 2003-07-22 21:10
            ' Author    : joho
            ' Purpose   : Returns/sets a remark on a spot
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Remark_Error
                Remark = mvarRemark
                On Error GoTo 0
                Exit Property
Remark_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Remark", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo Remark_Error
                mvarRemark = value
                On Error GoTo 0
                Exit Property
Remark_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Remark", Err.Description)
            End Set
        End Property

        Public Property Placement()
            Set(ByVal value)
                'used when assigning an Object to the property, on the left side of a Set statement.
                'Syntax: Set x.Placement = Form1
                mvarPlacement = value
            End Set
            Get
                Placement = mvarPlacement
            End Get
        End Property
        Private _breakID As String
        Public Property breakID() As String
            Get
                Return _breakID
            End Get
            Set(ByVal value As String)
                _breakID = value
            End Set
        End Property
        Public Property Breadi()
            Set(ByVal value)
                'used when assigning an Object to the property, on the left side of a Set statement.
                'Syntax: Set x.Placement = Form1
                mvarPlacement = value
            End Set
            Get
                Placement = mvarPlacement
            End Get
        End Property

        Public Sub New(ByVal Main As cKampanj)
            Matched = False
            Main.RegisterProblemDetection(Me)
        End Sub

        Protected Overrides Sub Finalize()
            mvarWeek = Nothing

            mvarBookingtype = Nothing
            mvarChannel = Nothing
            mvarFilm = Nothing
            mvarMatchedSpot = Nothing
            MyBase.Finalize()

        End Sub

        Public Enum ProblemsEnum
            Mismatched = 1
            ValueTwo = 2
        End Enum

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems

            Dim _problems As New List(Of cProblem)

            Try
                If Matched AndAlso Main.ActualSpots(MatchedSpot.ID).ID <> Me.ID Then

                    Dim _helpText As New Text.StringBuilder

                    _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Spot is mismatched</p>")
                    _helpText.AppendLine("<p>The confirmed spot " & Me.ProgAfter & " on " & Date.FromOADate(Me.AirDate).ToShortDateString & _
                                         " in channel " & Me.Channel.ChannelName & " is matched to the actual spot " & Me.MatchedSpot.ProgAfter & _
                                         " on " & Date.FromOADate(Me.MatchedSpot.AirDate).ToShortDateString & " in channel " & Me.MatchedSpot.Channel.ChannelName & _
                                         ", which is not linked back.</p>")
                    _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                    _helpText.AppendLine("<p>In Spots, break the matches on both spots, then match them to each other.</p>")
                    Dim _problem As New cProblem(ProblemsEnum.Mismatched, cProblem.ProblemSeverityEnum.Warning, "Mismatched spot", Me.Channel.ChannelName, _helpText.ToString, Me)

                    _problems.Add(_problem)

                End If
            Catch ex As Exception
                Return New List(Of cProblem)
            End Try


            If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
            Return _problems
        End Function

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound
    End Class

End Namespace