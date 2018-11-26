Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cActualSpot
        Implements IDetectsProblems


        '---------------------------------------------------------------------------------------
        ' Class : cActualSpot
        ' DateTime  : -
        ' Author    : joho
        ' Purpose   : Actual spots are spots that have been booked, planned and aired
        '---------------------------------------------------------------------------------------
        '
        Public Enum EnumBreakType
            btBlock = 0
            btBreak = 1
        End Enum

        Public Enum SCStatusEnum
            scsNone = 0
            scsRemindMe = 1
            scsOK = 2
        End Enum

        Private mvarChannel As cChannel ' Holds the channel where the spot was aired
        Private mvarAirDate As Long
        Private mvarMaM As Integer  'a time measurement used (Minutes After Midnight)
        Private mvarSecond As Integer 'the second in the minut
        Public ProgBefore As String 'The program that was aired before the commercial (optional)
        Public ProgAfter As String ' the program that was aired after the commercial (optional)
        Public Programme As String 'the program that the commercial was aired in
        Public Advertiser As String
        Public Product As String 'the product that was advertised
        Private _filmcode As String 'the filmcode for the film used
        Public Index As Integer
        Public PosInBreak As Byte 'in what order was the spot aired
        Public SpotsInBreak As Byte 'how many spots there where in the break
        Public BrandBreakSeqID As Integer 'The unique ID of the break in which this spot was aired
        Public SharesBreak As Boolean 'Indicates whether this spot has the same BrandBreakSeqID as another spot in the campaign
        Public MatchedSpot As cPlannedSpot
        Public SpotLength As Integer  'the lenght of the spot
        Public Deactivated As Boolean
        Public SpotType As Byte
        Private mvarWeek As cWeek 'in what week the spot was aired
        Private mvarBreakType As EnumBreakType
        Public SecondRating As Single
        Public AdedgeChannel As String
        Private mvarID As String
        Private mvarBookingtype As cBookingType 'how the spot was booked
        Private mvarGroupIdx As Integer
        Private mvarSpotControlStatus As SCStatusEnum
        Private mvarSpotControlRemark As String
        Private Main As cKampanj
        Public Remark As String

        'booleans to keep track of when we need to refetch the ratings from AdvantEdge
        Dim _mainTargetChanged As Boolean = True
        Dim _secondaryTargetChanged As Boolean = True
        Dim _thirdTargetChanged As Boolean = True
        Dim _buyingTargetChanged As Boolean = True

        Dim _updateNetValue As Boolean = True
        Dim _netValue As Single
        Dim _updateGrossValue As Boolean = True
        Dim _grossValue As Single

        'Singles to hold the stored ratings
        Dim sngAllAdultsRating As Single = 0 'needs to be 0 by default

        'rating variables
        Dim sngMainTargetRating As Single
        Dim sngSecondaryTargetRating As Single
        Dim sngThirdTargetRating As Single
        Dim _sngBuyingTargetRating As Single

        'rating 30 variables
        Dim sngMainTargetRating30 As Single
        Dim sngSecondaryTargetRating30 As Single
        Dim sngThirdTargetRating30 As Single
        Dim sngBuyingTargetRating30 As Single

        'viewers in 1000
        Dim sngMainTargetRating000 As Single
        Dim sngSecondaryTargetRating000 As Single
        Dim sngThirdTargetRating000 As Single
        Dim sngBuyingTargetRating000 As Single

        Public Enum ActualTargetEnum
            ateMainTarget = 0
            ateSecondTarget = 1
            ateThirdTarget = 2
            ateAllAdults = 3
            ateBuyingTarget = 4
            ateCustomTarget = 5
        End Enum

        Public Property Filmcode() As String
            Get
                Return _filmcode
            End Get
            Set(ByVal value As String)
                _filmcode = value
                InvalidateTargets()
                InvalidateValue()
            End Set
        End Property

        Public Property sngBuyingTargetRating() As Single
            Get
                Return _sngBuyingTargetRating
            End Get
            Set(ByVal value As Single)
                _sngBuyingTargetRating = value
            End Set
        End Property

        Friend Sub InvalidateTargets()
            InvalidateMainTarget()
            InvalidateBuyingTarget()
            InvalidateSecondTarget()
            InvalidateThirdTarget()
        End Sub

        Friend Sub InvalidateMainTarget()
            _mainTargetChanged = True
        End Sub

        Friend Sub InvalidateSecondTarget()
            _secondaryTargetChanged = True
        End Sub

        Friend Sub InvalidateThirdTarget()
            _thirdTargetChanged = True
        End Sub

        Friend Sub InvalidateBuyingTarget()
            _buyingTargetChanged = True
            If mvarWeek IsNot Nothing Then
                mvarWeek.InvalidateActualTRPBuying()
            End If
            InvalidateValue()
        End Sub

        Friend Sub InvalidateValue()
            _updateNetValue = True
            _updateGrossValue = True
        End Sub

        Public Property Week() As Trinity.cWeek
            Get
                If mvarWeek Is Nothing AndAlso mvarBookingtype IsNot Nothing Then
                    Return mvarBookingtype.GetWeek(Date.FromOADate(mvarAirDate))
                Else
                    Return mvarWeek
                End If
            End Get
            Set(ByVal value As Trinity.cWeek)
                mvarWeek = value
                InvalidateValue()
            End Set
        End Property

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            colXml.SetAttribute("ID", Me.ID) 'as String
            colXml.SetAttribute("AirDate", Me.AirDate) 'as Long
            colXml.SetAttribute("MaM", Me.MaM) 'as Integer
            colXml.SetAttribute("Second", Me.Second) 'as Integer
            colXml.SetAttribute("Channel", Me.Channel.ChannelName) 'as cChannel
            colXml.SetAttribute("ProgBefore", Me.ProgBefore) 'as String
            colXml.SetAttribute("ProgAfter", Me.ProgAfter) 'as String
            colXml.SetAttribute("Programme", Me.Programme) 'as String
            colXml.SetAttribute("Advertiser", Me.Advertiser) 'as String
            colXml.SetAttribute("Product", Me.Product) 'as String
            colXml.SetAttribute("Filmcode", Me.Filmcode) 'as String
            colXml.SetAttribute("Index", Me.Index) 'as Integer
            colXml.SetAttribute("PosInBreak", Me.PosInBreak) 'as Byte
            colXml.SetAttribute("SpotsInBreak", Me.SpotsInBreak) 'as Byte
            If Not Me.MatchedSpot Is Nothing Then
                colXml.SetAttribute("MatchedSpot", Me.MatchedSpot.ID) 'as cPlannedSpot
            Else
                colXml.SetAttribute("MatchedSpot", "")
            End If
            colXml.SetAttribute("SpotLength", Me.SpotLength) 'as Byte
            colXml.SetAttribute("Deactivated", Me.Deactivated) 'as Boolean
            colXml.SetAttribute("SpotType", Me.SpotType) 'as Byte
            colXml.SetAttribute("BreakType", Me.BreakType) 'as EnumBreakType
            colXml.SetAttribute("SecondRating", Me.SecondRating) 'as Single
            colXml.SetAttribute("AdEdgeChannel", Me.AdedgeChannel) 'as String
            colXml.SetAttribute("BookingType", Me.Bookingtype.Name) 'as cBookingType
            colXml.SetAttribute("GroupIdx", Me.GroupIdx) 'as Integer
            colXml.SetAttribute("RatingMain", Me.Rating(cActualSpot.ActualTargetEnum.ateMainTarget))
            colXml.SetAttribute("RatingBuying", Me.Rating(cActualSpot.ActualTargetEnum.ateBuyingTarget))
            colXml.SetAttribute("SpotControlRemark", Me.SpotControlRemark)
            colXml.SetAttribute("SpotControlStatus", Me.SpotControlStatus)

            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving the Actual spot " & Me.ID)
            Return False
        End Function

        Public Sub PriceListChanged()
            InvalidateValue()

            'makes the bookingtype recalculate its net value aswell
            mvarBookingtype.InvalidateActualNetValue()
        End Sub

        Public Sub daypartsChanged()
            InvalidateValue()

            'makes the bookingtype recalculate its net value aswell
            mvarBookingtype.InvalidateActualNetValue()
        End Sub

        Public Sub TargetsChanged()
            InvalidateTargets()
        End Sub

        Public Sub BuyingTargetChanged()
            InvalidateBuyingTarget()
        End Sub

        Public Sub MainTargetChanged()
            InvalidateMainTarget()
        End Sub

        Public Sub SecondaryTargetChanged()
            InvalidateSecondTarget()
        End Sub

        Public Sub ThirdTargetChanged()
            InvalidateThirdTarget()
        End Sub

        Public Property SpotControlStatus() As SCStatusEnum
            Get
                Return mvarSpotControlStatus
            End Get
            Set(ByVal value As SCStatusEnum)
                mvarSpotControlStatus = value
            End Set
        End Property

        Public Property SpotControlRemark() As String
            Get
                Return mvarSpotControlRemark
            End Get
            Set(ByVal value As String)
                mvarSpotControlRemark = value
            End Set
        End Property

        'returns the campaign the spot belongs to
        Friend WriteOnly Property MainObject() As cKampanj
            Set(ByVal value As cKampanj)

                Main = value

            End Set
        End Property

        '---------------------------------------------------------------------------------------
        ' Procedure : ID
        ' DateTime  : 2003-07-18 10:06
        ' Author    : joho
        ' Purpose   : Returns/sets the ID to be used when saving relations between spots
        '             to file
        '---------------------------------------------------------------------------------------
        '        
        Friend Property ID() As String
            Get
                On Error GoTo ID_Error
                Return mvarID
                On Error GoTo 0
                Exit Property
ID_Error:
                Err.Raise(Err.Number, "cActualSpot: ID", Err.Description)
            End Get
            Set(ByVal value As String)
                Dim TmpSpot As cActualSpot
                On Error GoTo ID_Error
                'mvarID can't be empty
                If mvarID <> "" Then
                    TmpSpot = Main.ActualSpots(mvarID)
                    Main.ActualSpotsCollection.Remove(mvarID)
                    Main.ActualSpotsCollection.Add(value, TmpSpot)
                End If
                mvarID = value
                On Error GoTo 0
                Exit Property

ID_Error:

                Err.Raise(Err.Number, "cActualSpot: ID", Err.Description)
            End Set
        End Property



        '---------------------------------------------------------------------------------------
        ' Procedure : Channel
        ' DateTime  : 2003-06-12 16:05
        ' Author    : joho
        ' Purpose   : Pointer to an object of the cChannel class that contains data about
        '             the channel where the spot was aired
        '---------------------------------------------------------------------------------------
        '
        Public Property Channel() As cChannel

            Get
                On Error GoTo Channel_Error
                Channel = mvarChannel
                On Error GoTo 0
                Exit Property
Channel_Error:
                Err.Raise(Err.Number, "cActualSpot: Channel", Err.Description)
            End Get
            Set(ByVal value As cChannel)
                On Error GoTo Channel_Error
                mvarChannel = value
                'JOHO har tagit bort nedanstående block. Varför är det där? Det får alla spotar i en sammanslagen kanal att hamna på första kanalen, vilket blir fel utfall
                'If value Is Nothing Then
                '    AdedgeChannel = ""
                'Else
                '    If InStr(value.AdEdgeNames, ",") = 0 Then
                '        AdedgeChannel = Channel.AdEdgeNames
                '    Else
                '        AdedgeChannel = Left(value.AdEdgeNames, InStr(value.AdEdgeNames, ",") - 1)
                '    End If
                'End If
                On Error GoTo 0
                Exit Property
Channel_Error:
                Err.Raise(Err.Number, "cActualSpot: Channel", Err.Description)
            End Set

        End Property


        Public Property AirDate() As Long
            Get
                Return mvarAirDate
            End Get
            Set(ByVal value As Long)
                mvarAirDate = value

                'this will only run if the spot changes date after creating
                If Not mvarBookingtype Is Nothing Then
                    'this adds the spot to a list so it will be updated when the pricelist changes
                    mvarBookingtype.BuyingTarget.AddActualspotToWatch(Me)
                End If
            End Set
        End Property

        Public Property MaM() As Long
            Get
                Return mvarMaM
            End Get
            Set(ByVal value As Long)
                If value > 2000 Then value = 1140 'Stop
                mvarMaM = value
            End Set
        End Property

        Public Property Second() As Long
            Get
                Return mvarSecond
            End Get
            Set(ByVal value As Long)

                mvarSecond = value
            End Set
        End Property

        '---------------------------------------------------------------------------------------
        ' Procedure : BookingType
        ' DateTime  : 2003-07-15 12:51
        ' Author    : joho
        ' Purpose   : Pointer to a cBookingType representing the Booking Type this spot
        '             is a part of
        '---------------------------------------------------------------------------------------
        '
        Public Property Bookingtype() As cBookingType
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

                If mvarBookingtype IsNot Nothing AndAlso mvarBookingtype.ActualSpots.Contains(ID) Then
                    mvarBookingtype.InvalidateActualNetValue()
                    mvarBookingtype.ActualSpots.Remove(ID)
                End If

                mvarBookingtype = value

                If mvarWeek IsNot Nothing Then
                    If mvarBookingtype IsNot Nothing Then
                        mvarWeek.InvalidateActualTRPBuying()
                        mvarWeek = mvarBookingtype.Weeks(mvarWeek.Name)
                        mvarWeek.InvalidateActualTRPBuying()
                    Else
                        mvarWeek = Nothing
                    End If
                End If
                If mvarBookingtype IsNot Nothing Then
                    mvarBookingtype.ActualSpots.Add(Me, ID)

                    mvarBookingtype.InvalidateActualNetValue()
                    InvalidateBuyingTarget()
                    If Not mvarBookingtype.BuyingTarget.TargetName Is Nothing AndAlso mvarBookingtype.BookIt AndAlso Not mvarBookingtype.Pricelist.Targets(mvarBookingtype.BuyingTarget.TargetName) Is Nothing Then
                        mvarBookingtype.Pricelist.Targets(mvarBookingtype.BuyingTarget.TargetName).AddActualspotToWatch(Me)
                    End If
                End If
                InvalidateValue()

                'this adds the spot to a list so it will be updated when the pricelist changes

                On Error GoTo 0
                Exit Property
BookingType_Error:
                Err.Raise(Err.Number, "cPlannedSpot: BookingType", Err.Description)
            End Set
        End Property

        Public Function ActualGrossValue() As Decimal
            If _updateGrossValue Then

                If Not Week Is Nothing AndAlso Not Week.Films(Filmcode) Is Nothing Then
                    _grossValue = Rating(cActualSpot.ActualTargetEnum.ateBuyingTarget) * (Week.Films(Filmcode).Index / 100) * Bookingtype.GetGrossCPP30ForDate(Date.FromOADate(AirDate), Bookingtype.Dayparts.GetDaypartIndexForMam(MaM))
                    _updateGrossValue = False
                    Return _grossValue

                Else
                    Return 0
                End If
            Else
                Return _grossValue
            End If


            'If Not week Is Nothing Then
            '    Return Rating30(cActualSpot.ActualTargetEnum.ateBuyingTarget) * week.NetCPP30(Trinity.Helper.GetDaypart(MaM))
            'Else
            '    Return 0
            'End If
        End Function

        Public Function ActualNetValue() As Decimal
            If _updateNetValue Then
                If Not Week Is Nothing AndAlso Not Week.Films(Filmcode) Is Nothing Then
                    Dim _cpp As Single = 0
                    If Bookingtype Is Nothing OrElse Bookingtype.IsSpecific = False OrElse MatchedSpot Is Nothing OrElse MatchedSpot.PriceNet = 0 OrElse MatchedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = 0 Then
                        _cpp = Bookingtype.GetDiscountedCPPForDate(Date.FromOADate(AirDate), Bookingtype.Dayparts.GetDaypartIndexForMam(MaM))
                    Else
                        _cpp = MatchedSpot.PriceNet / MatchedSpot.ChannelRating(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
                    End If
                    _netValue = Rating30(cActualSpot.ActualTargetEnum.ateBuyingTarget) * _cpp
                    _updateNetValue = False
                    Return _netValue
                Else
                    Return 0
                End If
            Else
                Return _netValue
            End If


            'If Not week Is Nothing Then
            '    Return Rating30(cActualSpot.ActualTargetEnum.ateBuyingTarget) * week.NetCPP30(Trinity.Helper.GetDaypart(MaM))
            'Else
            '    Return 0
            'End If
        End Function

        '---------------------------------------------------------------------------------------
        ' Procedure : GroupIdx
        ' DateTime  : 2003-09-04 13:53
        ' Author    : joho
        ' Purpose   : Returns/sets the index used to access this spot in Adedge
        '---------------------------------------------------------------------------------------
        '
        Public Property GroupIdx() As Integer
            Get
                On Error GoTo GroupIdx_Error
                GroupIdx = mvarGroupIdx
                On Error GoTo 0
                Exit Property
GroupIdx_Error:
                Err.Raise(Err.Number, "cActualSpot: GroupIdx", Err.Description)
            End Get
            Set(ByVal value As Integer)
                On Error GoTo GroupIdx_Error
                mvarGroupIdx = value
                On Error GoTo 0
                Exit Property
GroupIdx_Error:
                Err.Raise(Err.Number, "cActualSpot: GroupIdx", Err.Description)
            End Set
        End Property



        '---------------------------------------------------------------------------------------
        ' Procedure : BreakType
        ' DateTime  : 2003-09-08 14:27
        ' Author    : joho
        ' Purpose   : Returns/sets the type of break this spot was aired in.
        '
        '---------------------------------------------------------------------------------------
        '
        Public Property BreakType() As EnumBreakType
            Get
                On Error GoTo BreakType_Error
                BreakType = mvarBreakType
                On Error GoTo 0
                Exit Property
BreakType_Error:
                Err.Raise(Err.Number, "cActualSpot: BreakType", Err.Description)
            End Get
            Set(ByVal value As EnumBreakType)
                On Error GoTo BreakType_Error
                mvarBreakType = value
                On Error GoTo 0
                Exit Property
BreakType_Error:
                Err.Raise(Err.Number, "cActualSpot: BreakType", Err.Description)
            End Set
        End Property

        Private Sub recalculateMainTarget()
            Dim Dummy As Integer

            Dummy = Trinity.Helper.TargetIndex(Main.Adedge, Main.MainTarget) - 1
            If Err.Number > 0 Then
                Dummy = Main.MainTarget.UniSize
            End If

            _mainTargetChanged = False

            'rating
            Try
                If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                    sngMainTargetRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.MainTarget))
                Else
                    sngMainTargetRating = 0
                    _mainTargetChanged = True 'will be racalculated next time
                End If
            Catch
                If mvarGroupIdx < Main.Adedge.getGroupCount Then
                    Trinity.Helper.AddTargetsToAdedge(Main.Adedge)
                    Main.Adedge.Run(True, False, 10, True)
                    sngMainTargetRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.MainTarget))
                Else
                    sngMainTargetRating = 0
                    _mainTargetChanged = True 'will be racalculated next time
                End If
            End Try

            'rating 30
            Try
                If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                    If Week Is Nothing OrElse Week.Films(Filmcode) Is Nothing Then
                        sngMainTargetRating30 = Main.Adedge.getUnitGroup(Connect.eUnits.uBrandTrp30, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.MainTarget))
                    Else
                        sngMainTargetRating30 = sngMainTargetRating * (Week.Films(Filmcode).Index / 100)
                    End If
                Else
                    sngMainTargetRating30 = 0
                    _mainTargetChanged = True
                End If
            Catch
                sngMainTargetRating30 = 0
                _mainTargetChanged = True
            End Try


            'rating 000
            Try
                If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                    sngMainTargetRating000 = Int(Main.Adedge.getUnitGroup(Connect.eUnits.u000, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.MainTarget)) + 0.5)
                Else
                    sngMainTargetRating000 = 0
                    _mainTargetChanged = True 'will be racalculated next time
                End If
            Catch
                Trinity.Helper.AddTargetsToAdedge(Main.Adedge)
                Main.Adedge.Run(True, False, 10, True)
                sngMainTargetRating000 = Int(Main.Adedge.getUnitGroup(Connect.eUnits.u000, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.MainTarget)) + 0.5)
            End Try
        End Sub

        Private Sub recalculateSecondaryTarget()
            Dim Dummy As Integer

            Dummy = Trinity.Helper.TargetIndex(Main.Adedge, Main.SecondaryTarget) - 1
            If Err.Number > 0 Then
                Dummy = Main.SecondaryTarget.UniSize
            End If

            _secondaryTargetChanged = False

            'rating
            Try
                If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                    sngSecondaryTargetRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.SecondaryTarget))
                Else
                    sngSecondaryTargetRating = 0
                    _secondaryTargetChanged = True 'will be racalculated next time
                End If
            Catch
                If mvarGroupIdx < Main.Adedge.getGroupCount Then
                    Trinity.Helper.AddTargetsToAdedge(Main.Adedge)
                    Main.Adedge.Run(True, False, 10, True)
                    sngSecondaryTargetRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.SecondaryTarget))
                Else
                    sngSecondaryTargetRating = 0
                    _secondaryTargetChanged = True 'will be racalculated next time
                End If
            End Try

            'rating 30
            Try
                If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                    sngSecondaryTargetRating30 = Main.Adedge.getUnitGroup(Connect.eUnits.uBrandTrp30, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.SecondaryTarget))
                Else
                    sngSecondaryTargetRating30 = 0
                    _secondaryTargetChanged = True
                End If
            Catch
                sngSecondaryTargetRating30 = 0
                _secondaryTargetChanged = True
            End Try


            'rating 000
            Try
                If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                    sngSecondaryTargetRating000 = Int(Main.Adedge.getUnitGroup(Connect.eUnits.u000, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.SecondaryTarget)) + 0.5)
                Else
                    sngSecondaryTargetRating000 = 0
                    _secondaryTargetChanged = True 'will be racalculated next time
                End If
            Catch
                Trinity.Helper.AddTargetsToAdedge(Main.Adedge)
                Main.Adedge.Run(True, False, 10, True)
                sngSecondaryTargetRating000 = Int(Main.Adedge.getUnitGroup(Connect.eUnits.u000, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.SecondaryTarget)) + 0.5)
            End Try
        End Sub

        Private Sub recalculateThirdTarget()
            Dim Dummy As Integer

            Dummy = Trinity.Helper.TargetIndex(Main.Adedge, Main.ThirdTarget) - 1
            If Err.Number > 0 Then
                Dummy = Main.ThirdTarget.UniSize
            End If

            _thirdTargetChanged = False

            'rating
            Try
                If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                    sngThirdTargetRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.ThirdTarget))
                Else
                    sngThirdTargetRating = 0
                    _thirdTargetChanged = True 'will be racalculated next time
                End If
            Catch
                If mvarGroupIdx < Main.Adedge.getGroupCount Then
                    Trinity.Helper.AddTargetsToAdedge(Main.Adedge)
                    Main.Adedge.Run(True, False, 10, True)
                    sngThirdTargetRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.ThirdTarget))
                Else
                    sngThirdTargetRating = 0
                    _thirdTargetChanged = True 'will be racalculated next time
                End If
            End Try

            'rating 30
            Try
                If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                    sngThirdTargetRating30 = Main.Adedge.getUnitGroup(Connect.eUnits.uBrandTrp30, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.ThirdTarget))
                Else
                    sngThirdTargetRating30 = 0
                    _thirdTargetChanged = True
                End If
            Catch
                sngThirdTargetRating30 = 0
                _thirdTargetChanged = True
            End Try

            'rating 000
            Try
                If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                    sngThirdTargetRating000 = Int(Main.Adedge.getUnitGroup(Connect.eUnits.u000, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.ThirdTarget)) + 0.5)
                Else
                    sngSecondaryTargetRating000 = 0
                    _thirdTargetChanged = True 'will be racalculated next time
                End If
            Catch
                Trinity.Helper.AddTargetsToAdedge(Main.Adedge)
                Main.Adedge.Run(True, False, 10, True)
                sngThirdTargetRating000 = Int(Main.Adedge.getUnitGroup(Connect.eUnits.u000, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.ThirdTarget)) + 0.5)
            End Try
        End Sub

        Private Function recalculateBuyingTarget() As String
            Dim Dummy As Integer

            Dummy = Trinity.Helper.TargetIndex(Main.Adedge, mvarBookingtype.BuyingTarget.Target) - 1
            If Err.Number > 0 Then
                Dummy = mvarBookingtype.BuyingTarget.Target.UniSize
            End If

            _buyingTargetChanged = False

            InvalidateValue()

            'If mvarChannel.BuyingUniverse = "" Then mvarChannel.BuyingUniverse = mvarBookingtype.BuyingTarget.Target.Universe
            'If mvarChannel.BuyingUniverse = "" Then Exit Sub
            'rating
            Try
                If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                    sngBuyingTargetRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, mvarBookingtype.BuyingTarget.Target))
                Else
                    sngBuyingTargetRating = 0
                    _buyingTargetChanged = True 'will be racalculated next time
                End If
            Catch
                Try
                    mvarBookingtype.BuyingTarget.Target.TargetType = cTarget.TargetTypeEnum.trgUserTarget
                    mvarBookingtype.BuyingTarget.Target.TargetGroup = "Trinity"
                    sngBuyingTargetRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, mvarBookingtype.BuyingTarget.Target))
                Catch

                    If mvarGroupIdx < Main.Adedge.getGroupCount Then
                        'Trinity.Helper.AddTargetsToAdedge(Main.Adedge)
                        'Trinity.Helper.AddTarget(Main.Adedge, mvarBookingtype.BuyingTarget.Target, True)
                        Trinity.Helper.AddTarget(Main.Adedge, mvarBookingtype.BuyingTarget.Target, True)
                        Main.Adedge.Run(True, False, 10, True)
                        If Trinity.Helper.TargetIndex(Main.Adedge, mvarBookingtype.BuyingTarget.Target) < 0 Then
                            MsgBox("Target " & mvarBookingtype.BuyingTarget.TargetName & _
                                   "not found in the pricelist. Maybe its a user defined target group " & vbNewLine & _
                                   "but AdvantEdge is looking for a simple one like A20-44?" & vbNewLine & _
                                   "Go into Edit Pricelist " & vbNewLine & _
                                   "Channel: " & mvarBookingtype.ParentChannel.ChannelName & vbNewLine & _
                                   "Bookingtype " & mvarBookingtype.Name & vbNewLine & _
                                   "Target name: " & mvarBookingtype.BuyingTarget.TargetName & vbNewLine & _
                                   "Then correct the target definition. ")
                            Return "Erroneous target group definition"
                        End If
                        sngBuyingTargetRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, mvarBookingtype.BuyingTarget.Target))
                    Else
                        sngBuyingTargetRating = 0
                        _buyingTargetChanged = True 'will be racalculated next time
                    End If
                End Try
            End Try
            'rating 30
            Try
                If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                    If Week Is Nothing OrElse Week.Films(Filmcode) Is Nothing Then
                        sngBuyingTargetRating30 = Main.Adedge.getUnitGroup(Connect.eUnits.uBrandTrp30, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, mvarBookingtype.BuyingTarget.Target))
                    Else
                        sngBuyingTargetRating30 = sngBuyingTargetRating * (Week.Films(Filmcode).Index / 100)
                    End If
                Else
                    sngBuyingTargetRating30 = 0
                    _buyingTargetChanged = True
                End If
           Catch
                sngBuyingTargetRating30 = 0
                _buyingTargetChanged = True
            End Try

            'rating 000
            Try
                If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                    sngBuyingTargetRating000 = Int(Main.Adedge.getUnitGroup(Connect.eUnits.u000, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, mvarBookingtype.BuyingTarget.Target)) + 0.5)
                Else
                    sngBuyingTargetRating000 = 0
                    _buyingTargetChanged = True 'will be racalculated next time
                End If
            Catch
                Trinity.Helper.AddTargetsToAdedge(Main.Adedge)
                Main.Adedge.Run(True, False, 10, True)

                sngBuyingTargetRating000 = Int(Main.Adedge.getUnitGroup(Connect.eUnits.u000, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, mvarBookingtype.BuyingTarget.Target)) + 0.5)

            End Try
            Return ""
        End Function

        Public ReadOnly Property Rating(Optional ByVal Target As ActualTargetEnum = ActualTargetEnum.ateMainTarget, Optional ByVal CustomTarget As String = "3+", Optional ByVal Customuniverse As String = "") As Single
            Get
                Dim t As Object
                Dim u As String


                'if target is = 0
                If Target = ActualTargetEnum.ateMainTarget Then

                    If _mainTargetChanged Then
                        'recalculate the main target rating
                        recalculateMainTarget()

                        'Dummy = Trinity.Helper.TargetIndex(Main.Adedge, Main.MainTarget) - 1
                        'If Err.Number > 0 Then
                        '    Dummy = Main.MainTarget.UniSize
                        'End If

                        'Try
                        '    If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                        '        sngMainTargetRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Trinity.Helper.UniverseIndex(Main.Adedge, Main.MainTarget.Universe), Trinity.Helper.TargetIndex(Main.Adedge, Main.MainTarget))
                        '        bolMainTargetChanged = False
                        '        Return sngMainTargetRating
                        '    Else
                        '        Return 0
                        '    End If
                        'Catch
                        '    If mvarGroupIdx < Main.Adedge.getGroupCount Then
                        '        Trinity.Helper.AddTargetsToAdedge(Main.Adedge)
                        '        Main.Adedge.Run(True, False, 10, True)
                        '        sngMainTargetRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Trinity.Helper.UniverseIndex(Main.Adedge, Main.MainTarget.Universe), Trinity.Helper.TargetIndex(Main.Adedge, Main.MainTarget))
                        '        bolMainTargetChanged = False
                        '        Return sngMainTargetRating
                        '    Else
                        '        Return 0
                        '    End If
                        'End Try
                    End If
                    'return the stored value
                    Return sngMainTargetRating

                    Exit Property


                    ''set the main target age and universe
                    't = Main.MainTarget
                    'u = Main.MainTarget.Universe
                    'Dummy = Trinity.Helper.TargetIndex(Main.Adedge, t) - 1
                    'If Err.Number > 0 Then
                    '    Dummy = Main.MainTarget.UniSize
                    'End If


                    'Try
                    '    If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                    '        Return Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Trinity.Helper.UniverseIndex(Main.Adedge, u), Trinity.Helper.TargetIndex(Main.Adedge, t))
                    '    Else
                    '        Return 0
                    '    End If
                    'Catch
                    '    If mvarGroupIdx < Main.Adedge.getGroupCount Then
                    '        Trinity.Helper.AddTargetsToAdedge(Main.Adedge)
                    '        Main.Adedge.Run(True, False, 10)
                    '        Rating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Trinity.Helper.UniverseIndex(Main.Adedge, u), Trinity.Helper.TargetIndex(Main.Adedge, t))
                    '    Else
                    '        Rating = 0
                    '    End If
                    'End Try

                    'Exit Property

                    'if target is = 1
                ElseIf Target = ActualTargetEnum.ateSecondTarget Then
                    If _secondaryTargetChanged Then
                        recalculateSecondaryTarget()
                        'refetch the main target rating
                        '    Try
                        '        If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                        '            sngSecondaryTargetRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.SecondaryTarget))
                        '            bolSecondaryTargetChanged = False
                        '            Return sngSecondaryTargetRating
                        '        Else
                        '            Return 0
                        '        End If
                        '    Catch
                        '        If mvarGroupIdx < Main.Adedge.getGroupCount Then
                        '            Trinity.Helper.AddTargetsToAdedge(Main.Adedge)
                        '            Main.Adedge.Run(True, False, 10)
                        '            sngSecondaryTargetRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.SecondaryTarget))
                        '            bolSecondaryTargetChanged = False
                        '            Return sngSecondaryTargetRating
                        '        Else
                        '            Return 0
                        '        End If
                        '    End Try
                        'Else
                    End If

                    'return the stored value
                    Return sngSecondaryTargetRating
                    Exit Property




                    ''set the secondary target age and universe
                    't = Main.SecondaryTarget
                    'u = Main.SecondaryTarget.Universe
                    'Dummy = Trinity.Helper.TargetIndex(Main.Adedge, t) - 1
                    'If Err.Number > 0 Then
                    '    Dummy = Main.SecondaryTarget.UniSize
                    'End If

                    'if target is = 2
                ElseIf Target = ActualTargetEnum.ateThirdTarget Then
                    If _thirdTargetChanged Then
                        'refetch the main target rating
                        recalculateThirdTarget()
                        '    Try
                        '        If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                        '            sngSecondaryTargetRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Trinity.Helper.UniverseIndex(Main.Adedge, Main.ThirdTarget.Universe), Trinity.Helper.TargetIndex(Main.Adedge, Main.ThirdTarget))
                        '            bolThirdTargetChanged = False
                        '            Return sngThirdTargetRating
                        '        Else
                        '            Return 0
                        '        End If
                        '    Catch
                        '        If mvarGroupIdx < Main.Adedge.getGroupCount Then
                        '            Trinity.Helper.AddTargetsToAdedge(Main.Adedge)
                        '            Main.Adedge.Run(True, False, 10)
                        '            sngThirdTargetRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Trinity.Helper.UniverseIndex(Main.Adedge, Main.ThirdTarget.Universe), Trinity.Helper.TargetIndex(Main.Adedge, Main.ThirdTarget))
                        '            bolThirdTargetChanged = False
                        '            Return sngThirdTargetRating
                        '        Else
                        '            Return 0
                        '        End If
                        '    End Try
                        'Else
                    End If
                    'return the stored value
                    Return sngThirdTargetRating
                    'End If
                    Exit Property



                    ''set the third target age and universe
                    't = Main.ThirdTarget
                    'u = Main.ThirdTarget.Universe
                    'Dummy = Trinity.Helper.TargetIndex(Main.Adedge, t) - 1
                    'If Err.Number > 0 Then
                    '    Dummy = Main.ThirdTarget.UniSize
                    'End If

                    'if target is = 4
                ElseIf Target = ActualTargetEnum.ateBuyingTarget Then
                    'set the booking type and booking universe for the campaign

                    If _buyingTargetChanged Then
                        'refetch the main target rating
                        If recalculateBuyingTarget() = "Erroneous target group definition" Then Return -1
                        '    Try
                        '        If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                        '            sngBuyingTargetRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Trinity.Helper.UniverseIndex(Main.Adedge, mvarChannel.BuyingUniverse), Trinity.Helper.TargetIndex(Main.Adedge, mvarBookingtype.BuyingTarget.Target))
                        '            bolBuyingTargetChanged = False
                        '            Return sngBuyingTargetRating
                        '        Else
                        '            Return 0
                        '        End If
                        '    Catch
                        '        If mvarGroupIdx < Main.Adedge.getGroupCount Then
                        '            Trinity.Helper.AddTargetsToAdedge(Main.Adedge)
                        '            Main.Adedge.Run(True, False, 10)
                        '            sngBuyingTargetRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Trinity.Helper.UniverseIndex(Main.Adedge, mvarChannel.BuyingUniverse), Trinity.Helper.TargetIndex(Main.Adedge, mvarBookingtype.BuyingTarget.Target))
                        '            bolBuyingTargetChanged = False
                        '            Return sngBuyingTargetRating
                        '        Else
                        '            Return 0
                        '        End If
                        '    End Try
                        'Else'
                    End If
                    'return the stored value
                    Return sngBuyingTargetRating
                    Me.Bookingtype.ToString()

                    'End If
                    Exit Property


                    't = mvarBookingtype.BuyingTarget.Target
                    'u = mvarChannel.BuyingUniverse
                    'Dummy = Trinity.Helper.TargetIndex(Main.Adedge, t) - 1
                    'If Err.Number > 0 Then
                    '    Dummy = mvarBookingtype.BuyingTarget.Target.UniSize
                    'End If

                    'if target is = 3
                ElseIf Target = ActualTargetEnum.ateAllAdults Then
                    'get the AllAdults target rating
                    If sngAllAdultsRating = 0 Then
                        Try
                            If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                                sngAllAdultsRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.AllAdults))
                                Return sngAllAdultsRating
                            Else
                                Return 0
                            End If
                        Catch
                            If mvarGroupIdx < Main.Adedge.getGroupCount Then
                                Trinity.Helper.AddTargetsToAdedge(Main.Adedge)
                                Main.Adedge.Run(True, False, 10)
                                sngAllAdultsRating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.AllAdults))
                                Return sngAllAdultsRating
                            Else
                                Return 0
                            End If
                        End Try
                    Else
                        Return sngAllAdultsRating
                    End If

                    Exit Property
                    't = Main.AllAdults
                    'u = ""

                    'if target is = 5
                ElseIf Target = ActualTargetEnum.ateCustomTarget Then
                    'set a custom target and universe
                    t = CustomTarget
                    u = Customuniverse
                    If u Is Nothing Then
                        u = ""
                    Else
                        'if no one was applyable we set them empty
                        t = ""
                        u = ""
                    End If
                    Try
                        If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                            Return Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, t))
                        Else
                            Return 0
                        End If
                    Catch
                        If mvarGroupIdx < Main.Adedge.getGroupCount Then
                            Trinity.Helper.AddTargetsToAdedge(Main.Adedge)
                            Main.Adedge.Run(True, False, 10)
                            Rating = Main.Adedge.getUnitGroup(Connect.eUnits.uTRP, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, t))
                        Else
                            Rating = 0
                        End If
                    End Try
                End If
            End Get
        End Property

        Public ReadOnly Property Rating000(Optional ByVal Target As ActualTargetEnum = ActualTargetEnum.ateMainTarget, Optional ByVal CustomTarget As String = "3+", Optional ByVal Customuniverse As String = "") As Single
            Get
                Dim t As Object
                Dim u As String
                'Dim Dummy As Integer


                'if target is = 0
                If Target = ActualTargetEnum.ateMainTarget Then

                    If _mainTargetChanged Then
                        'recalculate the main target rating
                        recalculateMainTarget()
                    End If
                    'return the stored value
                    Return sngMainTargetRating000

                    Exit Property

                    ''set the main target age and universe
                    't = Main.MainTarget
                    'u = Main.MainTarget.Universe
                    'Dummy = Trinity.Helper.TargetIndex(Main.Adedge, t) - 1
                    'If Err.Number > 0 Then
                    '    Dummy = Main.MainTarget.UniSize
                    'End If

                    'if target is = 1
                ElseIf Target = ActualTargetEnum.ateSecondTarget Then
                    If _secondaryTargetChanged Then
                        'recalculate the main target rating
                        recalculateSecondaryTarget()
                    End If
                    'return the stored value
                    Return sngSecondaryTargetRating000

                    Exit Property

                    ''set the secondary target age and universe
                    't = Main.SecondaryTarget
                    'u = Main.SecondaryTarget.Universe
                    'Dummy = Trinity.Helper.TargetIndex(Main.Adedge, t) - 1
                    'If Err.Number > 0 Then
                    '    Dummy = Main.SecondaryTarget.UniSize
                    'End If

                    'if target is = 2
                ElseIf Target = ActualTargetEnum.ateThirdTarget Then

                    If _thirdTargetChanged Then
                        'recalculate the main target rating
                        recalculateThirdTarget()
                    End If
                    'return the stored value
                    Return sngThirdTargetRating000

                    Exit Property

                    ''set the third target age and universe
                    't = Main.ThirdTarget
                    'u = Main.ThirdTarget.Universe
                    'Dummy = Trinity.Helper.TargetIndex(Main.Adedge, t) - 1
                    'If Err.Number > 0 Then
                    '    Dummy = Main.ThirdTarget.UniSize
                    'End If

                    'if target is = 4
                ElseIf Target = ActualTargetEnum.ateBuyingTarget Then

                    If _buyingTargetChanged Then
                        'recalculate the main target rating
                        If recalculateBuyingTarget() = "Erroneous target group definition" Then Return -1
                    End If
                    'return the stored value
                    Return sngBuyingTargetRating000

                    Exit Property

                    ''set the booking type and booking universe for the campaign
                    't = mvarBookingtype.BuyingTarget.Target
                    'u = mvarChannel.BuyingUniverse
                    'Dummy = Trinity.Helper.TargetIndex(Main.Adedge, t) - 1
                    'If Err.Number > 0 Then
                    '    Dummy = mvarBookingtype.BuyingTarget.Target.UniSize
                    'End If

                    'if target is = 3
                ElseIf Target = ActualTargetEnum.ateAllAdults Then
                    'sets teh target to maximum
                    t = Main.AllAdults
                    u = ""

                    'if target is = 5
                ElseIf Target = ActualTargetEnum.ateCustomTarget Then
                    'set a custom target and universe
                    t = CustomTarget
                    u = Customuniverse
                    If u Is Nothing Then u = ""
                Else
                    'if no one was applyable we set them empty
                    t = ""
                    u = ""
                End If
                Try
                    If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                        Return Int(Main.Adedge.getUnitGroup(Connect.eUnits.u000, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, t)) + 0.5)
                    Else
                        Return 0
                    End If
                Catch
                    Trinity.Helper.AddTargetsToAdedge(Main.Adedge)
                    Main.Adedge.Run(True, False, 10)
                    Return Int(Main.Adedge.getUnitGroup(Connect.eUnits.u000, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, t)) + 0.5)
                End Try
            End Get
        End Property

        Public ReadOnly Property Rating30(Optional ByVal Target As ActualTargetEnum = ActualTargetEnum.ateMainTarget, Optional ByVal CustomTarget As String = "3+", Optional ByVal Customuniverse As String = "") As Single
            Get
                Dim t As Object
                Dim u As String
                'Dim Dummy As Integer

                If Target = ActualTargetEnum.ateMainTarget Then

                    If _mainTargetChanged Then
                        'recalculate the main target rating
                        recalculateMainTarget()
                    End If
                    'return the stored value
                    Return sngMainTargetRating30

                    Exit Property

                    't = Main.MainTarget
                    'u = Main.MainTarget.Universe
                    'Dummy = Trinity.Helper.TargetIndex(Main.Adedge, t) - 1
                    'If Err.Number > 0 Then
                    '    Dummy = Main.MainTarget.UniSize
                    'End If
                ElseIf Target = ActualTargetEnum.ateSecondTarget Then

                    If _secondaryTargetChanged Then
                        'recalculate the main target rating
                        recalculateSecondaryTarget()
                    End If
                    'return the stored value
                    Return sngSecondaryTargetRating30

                    't = Main.SecondaryTarget
                    'u = Main.SecondaryTarget.Universe
                    'Dummy = Trinity.Helper.TargetIndex(Main.Adedge, t) - 1
                    'If Err.Number > 0 Then
                    '    Dummy = Main.SecondaryTarget.UniSize
                    'End If
                ElseIf Target = ActualTargetEnum.ateSecondTarget Then

                    If _thirdTargetChanged Then
                        'recalculate the main target rating
                        recalculateThirdTarget()
                    End If
                    'return the stored value
                    Return sngThirdTargetRating30

                ElseIf Target = ActualTargetEnum.ateBuyingTarget Then

                    If _buyingTargetChanged Then
                        'recalculate the main target rating
                        If recalculateBuyingTarget() = "Erroneous target group definition" Then Return -1
                    End If
                    'return the stored value
                    Return sngBuyingTargetRating30

                    't = mvarBookingtype.BuyingTarget.Target
                    'u = mvarChannel.BuyingUniverse
                    'Dummy = Trinity.Helper.TargetIndex(Main.Adedge, t) - 1
                    'If Err.Number > 0 Then
                    '    Dummy = mvarBookingtype.BuyingTarget.Target.UniSize
                    'End If
                ElseIf Target = ActualTargetEnum.ateAllAdults Then
                    t = Main.AllAdults
                    u = ""
                ElseIf Target = ActualTargetEnum.ateCustomTarget Then
                    t = CustomTarget
                    u = Customuniverse
                Else
                    t = ""
                    u = ""
                End If
                If Main.Adedge.getGroupCount > 0 AndAlso mvarGroupIdx >= 0 Then
                    Rating30 = Main.Adedge.getUnitGroup(Connect.eUnits.uBrandTrp30, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, t))
                Else
                    Return 0
                End If
            End Get
        End Property

        Public Sub New(ByVal MainObject As cKampanj)
            'returns the main campaign
            Main = MainObject
            Main.RegisterProblemDetection(Me)
        End Sub

        Public Enum ProblemsEnum
            OutsideCampaignPeriod = 1
            OutsideWeekPeriod = 2
            NoChannel = 3
            NoBookingtype = 4
            NoWeek = 5
            FilmNotAllocated = 6
            FilmDoesNotExist = 7
            AdvantEdgeError = 8
            DoubleSpotsFromAdvantEdge = 9
        End Enum

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems
            Dim _problems As New List(Of cProblem)


            Dim Tolerance As Double = (1 / (24 * 60)) * 360 ' 6 hours in office automation time

            Dim Spots As cActualSpot() = (From tmpSpot As cActualSpot In Main.ActualSpots Select tmpSpot Where tmpSpot.AirDate = Me.AirDate And Me.Bookingtype Is tmpSpot.Bookingtype And Me.MaM = tmpSpot.MaM).ToArray
            If Spots.Length > 1 Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>AdvantEdge has delivered double spots</p>")
                _helpText.AppendLine("<p>Trinity has found double entries for one or more spots in the Actual spotlist</p>")
                _helpText.AppendLine("<table><tr><td>" & Format(Date.FromOADate(AirDate), "Short date") & "</td><td>" & Helper.Mam2Tid(MaM) & "</td><td>" & Bookingtype.ToString & "</td><td>" & Programme & "</td></tr></table>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>In the main window, select the 'Campaign'-menu and select 'Re-read spots'</p>")

                Dim _problem As New cProblem(ProblemsEnum.DoubleSpotsFromAdvantEdge, cProblem.ProblemSeverityEnum.Warning, "AdvantEdge has delivered double spots", Bookingtype.ToString, _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            'Check date
            If (AirDate < Main.StartDate - Tolerance OrElse AirDate > Main.EndDate + Tolerance) Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Actual spot outside of campaign period</p>")
                _helpText.AppendLine("<p>An actual spot has been found on a date that is either before campaign start or after campaign end</p>")
                _helpText.AppendLine("<table><tr><td>" & Format(Date.FromOADate(AirDate), "Short date") & "</td><td>" & Helper.Mam2Tid(MaM) & "</td><td>" & Bookingtype.ToString & "</td><td>" & Programme & "</td></tr></table>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>In the main window, select the 'Campaign'-menu and select 'Re-read spots'</p>")

                Dim _problem As New cProblem(ProblemsEnum.OutsideCampaignPeriod, cProblem.ProblemSeverityEnum.Warning, "Actual spot on a date outside of the campaign period", Bookingtype.ToString, _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            'Check week date
            If Week IsNot Nothing AndAlso (AirDate < Week.StartDate - Tolerance OrElse AirDate > Week.EndDate + Tolerance) Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Actual spot outside of week period</p>")
                _helpText.AppendLine("<p>An actual spot has been found on a date that is not included in any week on the campaign</p>")
                _helpText.AppendLine("<table><tr><td>" & Format(Date.FromOADate(AirDate), "Short date") & "</td><td>" & Helper.Mam2Tid(MaM) & "</td><td>" & Bookingtype.ToString & "</td><td>" & Programme & "</td></tr></table>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>The channel has broadcasted a spot on " & Format(Date.FromOADate(AirDate), "Short date") & " which is not part of the campaign. Take necessary action.</p>")

                Dim _problem As New cProblem(ProblemsEnum.OutsideWeekPeriod, cProblem.ProblemSeverityEnum.Error, "Actual spot on a date not belonging to any week", Bookingtype.ToString, _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            'Check that it has a channel
            If Channel Is Nothing Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Actual spot without a channel</p>")
                _helpText.AppendLine("<p>An actual spot does not have a channel associated to it</p>")
                _helpText.AppendLine("<table><tr><td>" & Format(Date.FromOADate(AirDate), "Short date") & "</td><td>" & Helper.Mam2Tid(MaM) & "</td><td>" & Bookingtype.ToString & "</td><td>" & Programme & "</td></tr></table>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Contact the system administrator</p>")

                Dim _problem As New cProblem(ProblemsEnum.NoChannel, cProblem.ProblemSeverityEnum.Error, "An actual spot does not have a channel", Bookingtype.ToString, _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            'Check that it has a bookingtype
            If Bookingtype Is Nothing Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Actual spot without a bookingtype</p>")
                _helpText.AppendLine("<p>An actual spot does not have a bookingtype associated to it</p>")
                _helpText.AppendLine("<table><tr><td>" & Format(Date.FromOADate(AirDate), "Short date") & "</td><td>" & Helper.Mam2Tid(MaM) & "</td><td>" & Bookingtype.ToString & "</td><td>" & Programme & "</td></tr></table>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Contact the system administrator</p>")

                Dim _problem As New cProblem(ProblemsEnum.NoChannel, cProblem.ProblemSeverityEnum.Error, "An actual spot does not have a bookingtype", "Actual spots", _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            'Check that it has a week
            If Week Is Nothing Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Actual spot without a week</p>")
                _helpText.AppendLine("<p>A spot in the list of booked spots does not have a week associated to it. This usually means it has been broadcasted in a date that does not belong to any of the campaign weeks.</p>")
                _helpText.AppendLine("<table><tr><td>" & Format(Date.FromOADate(AirDate), "Short date") & "</td><td>" & Helper.Mam2Tid(MaM) & "</td><td>" & Bookingtype.ToString & "</td><td>" & Programme & "</td></tr></table>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Contact the system administrator</p>")

                Dim _problem As New cProblem(ProblemsEnum.NoWeek, cProblem.ProblemSeverityEnum.Error, "An actual spot does not have a week", Bookingtype.ToString, _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            'Check film exists
            If Week IsNot Nothing AndAlso Week.Films(Filmcode) Is Nothing Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Actual spot with a film that does not exist</p>")
                _helpText.AppendLine("<p>There is an actual spot with a filmcode that does not exist on the bookingtype associated with the spot.</p>")
                _helpText.AppendLine("<table><tr><td>" & Format(Date.FromOADate(AirDate), "Short date") & "</td><td>" & Helper.Mam2Tid(MaM) & "</td><td>" & Bookingtype.ToString & "</td><td>" & Programme & "</td></tr></table>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Open the 'Setup'-window, select the 'Films'-tab and add the filmcode '" & Filmcode & "' to one of the films for '" & Bookingtype.ToString & "'</p>")

                Dim _problem As New cProblem(ProblemsEnum.FilmDoesNotExist, cProblem.ProblemSeverityEnum.Warning, "An actual spot has a filmcode that does not exist", Filmcode & " (" & Bookingtype.ToString & ")", _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            'Check film is allocated
            If Week IsNot Nothing AndAlso Week.Films(Filmcode) IsNot Nothing AndAlso Week.Films(Filmcode).Share = 0 Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>An actual spot was found on a week planned without that spot</p>")
                _helpText.AppendLine("<p>An actual spot with the film '" & Week.Films(Filmcode).Name & "' has been broadcasted on week '" & Week.Name & "' where that spot has not been allocated in 'Allocate'-window</p>")
                _helpText.AppendLine("<table><tr><td>" & Format(Date.FromOADate(AirDate), "Short date") & "</td><td>" & Helper.Mam2Tid(MaM) & "</td><td>" & Bookingtype.ToString & "</td><td>" & Programme & "</td></tr></table>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Either:<ul><li>The film has been broadcasted a week where it shouldn't have been. Take appropriate action.</li></ul>")
                _helpText.AppendLine("- or -<ul><li>Open the 'Allocate'-window, select '" & Bookingtype.ToString & "' in the pane in the top right corner ('Films') and set the share on '" & Week.Films(Filmcode).Name & "' to at least 1%</li><ul>")

                Dim _problem As New cProblem(ProblemsEnum.FilmNotAllocated, cProblem.ProblemSeverityEnum.Warning, "An actual spot has been broadcasted on a week where it is not planned", Bookingtype.ToString, _helpText.ToString, Me)
                _problems.Add(_problem)
            End If

            Try
                If mvarGroupIdx >= 0 And Main.Adedge.validate = 0 AndAlso Main.Adedge.getGroupCount > 0 Then
                    Main.Adedge.getUnitGroup(Connect.eUnits.uBrandTrp30, mvarGroupIdx, 0, Main.TimeShift, Trinity.Helper.TargetIndex(Main.Adedge, Main.MainTarget))
                End If
            Catch ex As Exception
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>An actual spot caused an error in AdvantEdge</p>")
                _helpText.AppendLine("<p>An actual spot has generated the following error in AdvantEdge:</p>")
                _helpText.AppendLine("<p>" & ex.Message & "</p>")
                _helpText.AppendLine("<table><tr><td>" & Format(Date.FromOADate(AirDate), "Short date") & "</td><td>" & Helper.Mam2Tid(MaM) & "</td><td>" & Bookingtype.ToString & "</td><td>" & Programme & "</td></tr></table>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Send a mail to the system administrator with the campaign and the error message above. Also paste this information:</p>")
                _helpText.AppendLine("<ul><li>Group index: " & mvarGroupIdx & "</li>")
                Try
                    _helpText.AppendLine("<li>Universe: " & Main.MainTarget.Universe & "</li>")
                Catch e As Exception
                    _helpText.AppendLine("<li>Could not get universe: " & e.Message & "</li>")
                End Try
                Try
                    _helpText.AppendLine("<li>Target: " & Main.MainTarget.TargetName & "(" & Helper.TargetIndex(Main.Adedge, Main.MainTarget) & ")</li>")
                Catch e As Exception
                    _helpText.AppendLine("<li>Could not get Target: " & e.Message & "</li>")
                End Try
                Try
                    _helpText.AppendLine("<li>Target group: " & Main.MainTarget.TargetGroup & "</li>")
                Catch e As Exception
                    _helpText.AppendLine("<li>Could not get Target group: " & e.Message & "</li>")
                End Try
                Try
                    _helpText.AppendLine("<li>Target type: " & Main.MainTarget.TargetType & "</li>")
                Catch e As Exception
                    _helpText.AppendLine("<li>Could not get Target tyoe: " & e.Message & "</li>")
                End Try
                Try
                    _helpText.AppendLine("<li>Adedge.Validate: " & Main.Adedge.validate & "</li>")
                Catch e As Exception
                    _helpText.AppendLine("<li>Could not get AdEdge.Validate: " & e.Message & "</li>")
                End Try
                Try
                    _helpText.AppendLine("<li>Adedge.Debug: " & Main.Adedge.debug & "</li>")
                Catch e As Exception
                    _helpText.AppendLine("<li>Could not get AdEdge.Debug: " & e.Message & "</li>")
                End Try

                Dim _problem As New cProblem(ProblemsEnum.AdvantEdgeError, cProblem.ProblemSeverityEnum.Error, "Actual spot caused AdEdge-error", "Spot no " & mvarGroupIdx, _helpText.ToString, Me)
                _problems.Add(_problem)
            End Try

            If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
            Return _problems

        End Function

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound

    End Class
End Namespace