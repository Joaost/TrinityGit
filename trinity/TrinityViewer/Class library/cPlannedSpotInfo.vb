Namespace TrinityViewer
    Public Class cPlannedSpotInfo

        Private _priceNet As Decimal
        Private _priceGross As Decimal
        Private _channel As cChannelInfo
        Private _bookingtype As cBookingTypeInfo
        Private _mam As Integer
        Private _airDate As Long
        Private _myRating As Decimal
        Private _rating As Decimal
        Private _filmcode As String

        Public Week As cWeekInfo

        Public Enum PlannedTargetEnum
            pteMainTarget = 0
            pteSecondTarget = 1
            pteThirdTarget = 2
            pteAllAdults = 3
            pteBuyingTarget = 4
        End Enum

        Public Property Filmcode() As String
            Get
                Return _filmcode
            End Get
            Set(ByVal value As String)
                _filmcode = value
            End Set
        End Property

        Public ReadOnly Property Film() As cFilmInfo
            '---------------------------------------------------------------------------------------
            ' Procedure : Film
            ' DateTime  : 2003-07-18 09:50
            ' Author    : joho
            ' Purpose   : Pointer to a cFilm corresponding to the film to be aired
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Film_Error
                Film = _bookingtype.Weeks(1).Films(_Filmcode)
                On Error GoTo 0
                Exit Property
Film_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Film", Err.Description)
            End Get
            '            Set(ByVal value As cFilm)
            '                On Error GoTo Film_Error
            '                _Film = value
            '                On Error GoTo 0
            '                Exit Property
            'Film_Error:
            '                Err.Raise(Err.Number, "cPlannedSpot: Film", Err.Description)
            '            End Set
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
                    ChannelRating = _Rating
                ElseIf Target = PlannedTargetEnum.pteMainTarget Then
                    ChannelRating = _rating * _bookingtype.BuyingTarget.UniIndex(cTargetInfo.EnumUni.uniMainCmp) * (_bookingtype.IndexMainTarget / 100)
                ElseIf Target = PlannedTargetEnum.pteSecondTarget Then
                    ChannelRating = _rating * _bookingtype.BuyingTarget.UniIndex(cTargetInfo.EnumUni.uniMainCmp) * (_bookingtype.IndexSecondTarget / 100)
                ElseIf Target = PlannedTargetEnum.pteAllAdults Then
                    ChannelRating = _rating * _bookingtype.BuyingTarget.UniIndex(cTargetInfo.EnumUni.uniMainCmp) * (_bookingtype.IndexAllAdults / 100)
                End If

                On Error GoTo 0
                Exit Property

Rating_Error:

                Err.Raise(Err.Number, "cPlannedSpot: Rating", Err.Description)
            End Get
            Set(ByVal value As Single)
                On Error GoTo Rating_Error

                If Target = PlannedTargetEnum.pteBuyingTarget Then
                    _Rating = value
                ElseIf Target = PlannedTargetEnum.pteMainTarget Then
                    If (_channel.MainTarget.UniIndex(cTargetInfo.EnumUni.uniMainCmp) * (_bookingtype.IndexMainTarget / 100)) <> 0 Then
                        _rating = value / (_channel.MainTarget.UniIndex(cTargetInfo.EnumUni.uniMainCmp) * (_bookingtype.IndexMainTarget / 100))
                    Else
                        _rating = 0
                    End If
                ElseIf Target = PlannedTargetEnum.pteSecondTarget Then
                    If (_channel.MainTarget.UniIndex(cTargetInfo.EnumUni.uniMainCmp) * (_bookingtype.IndexSecondTarget / 100)) <> 0 Then
                        _rating = value / (_channel.MainTarget.UniIndex(cTargetInfo.EnumUni.uniMainCmp) * (_bookingtype.IndexSecondTarget / 100))
                    Else
                        _rating = 0
                    End If
                ElseIf Target = PlannedTargetEnum.pteAllAdults Then
                    If (_channel.MainTarget.UniIndex(cTargetInfo.EnumUni.uniMainCmp) * (_bookingtype.IndexAllAdults / 100)) <> 0 Then
                        _rating = value / (_channel.MainTarget.UniIndex(cTargetInfo.EnumUni.uniMainCmp) * (_bookingtype.IndexAllAdults / 100))
                    Else
                        _rating = 0
                    End If
                End If
                On Error GoTo 0
                Exit Property
Rating_Error:
                Err.Raise(Err.Number, "cPlannedSpot: Rating", Err.Description)
            End Set
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

                If Target = PlannedTargetEnum.pteMainTarget Then
                    MyRating = _MyRating
                ElseIf Target = PlannedTargetEnum.pteBuyingTarget Then
                    If ChannelRating(PlannedTargetEnum.pteBuyingTarget) > 0 Then
                        UI = (_MyRating / ChannelRating(PlannedTargetEnum.pteBuyingTarget))
                    Else
                        UI = 0
                    End If
                    If UI > 0 Then
                        MyRating = _MyRating / UI
                    Else
                        MyRating = 0
                    End If
                ElseIf Target = PlannedTargetEnum.pteSecondTarget Then
                    MyRating = (_bookingtype.IndexSecondTarget / _bookingtype.IndexMainTarget) * _MyRating
                ElseIf Target = PlannedTargetEnum.pteThirdTarget Then
                    'TODO: Ändra
                    MyRating = (_bookingtype.IndexSecondTarget / _bookingtype.IndexMainTarget) * _MyRating
                ElseIf Target = PlannedTargetEnum.pteAllAdults Then
                    MyRating = (_bookingtype.IndexAllAdults / _bookingtype.IndexMainTarget) * _MyRating
                End If
                On Error GoTo 0
                Exit Property
MyRating_Error:
                Err.Raise(Err.Number, "cPlannedSpot: MyRating", Err.Description)
            End Get
            Set(ByVal value As Single)
                Dim UI As Single

                On Error GoTo MyRating_Error

                If Target = PlannedTargetEnum.pteMainTarget Then
                    _MyRating = value
                ElseIf Target = PlannedTargetEnum.pteSecondTarget Then
                    _MyRating = value / (_bookingtype.IndexSecondTarget / _bookingtype.IndexMainTarget)
                ElseIf Target = PlannedTargetEnum.pteAllAdults Then
                    _MyRating = value / (_bookingtype.IndexAllAdults / _bookingtype.IndexMainTarget)
                ElseIf Target = PlannedTargetEnum.pteBuyingTarget Then
                    'If ChannelRating(PlannedTargetEnum.pteBuyingTarget) > 0 Then
                    '    UI = (value / ChannelRating(PlannedTargetEnum.pteBuyingTarget))
                    'Else
                    '    UI = 0
                    'End If
                    UI = _bookingtype.IndexMainTarget / 100
                    If UI > 0 Then
                        _MyRating = value * UI
                    End If
                End If
                On Error GoTo 0
                Exit Property
MyRating_Error:
                Err.Raise(Err.Number, "cPlannedSpot: MyRating", Err.Description)
            End Set
        End Property

        Public Property AirDate() As Long
            Get
                Return _airDate
            End Get
            Set(ByVal value As Long)
                _airDate = value
            End Set
        End Property

        Public Property MaM() As Integer
            Get
                Return _mam
            End Get
            Set(ByVal value As Integer)
                _mam = value
            End Set
        End Property

        Public Property Bookingtype() As cBookingTypeInfo
            Get
                Return _bookingtype
            End Get
            Set(ByVal value As cBookingTypeInfo)
                _bookingtype = value
            End Set
        End Property

        Public Property Channel() As cChannelInfo
            Get
                Return _channel
            End Get
            Set(ByVal value As cChannelInfo)
                _channel = value
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
                PriceNet = _priceNet
                On Error GoTo 0
                Exit Property
PriceNet_Error:
                Err.Raise(Err.Number, "cPlannedSpot: PriceNet", Err.Description)
            End Get
            Set(ByVal value As Decimal)
                On Error GoTo PriceNet_Error
                _priceNet = value
                On Error GoTo 0
                Exit Property
PriceNet_Error:
                Err.Raise(Err.Number, "cPlannedSpot: PriceNet", Err.Description)
            End Set
        End Property


        Public Property PriceGross() As Decimal
            '---------------------------------------------------------------------------------------
            ' Procedure : PriceGross
            ' DateTime  : 2003-07-18 09:51
            ' Author    : joho
            ' Purpose   : Returns/sets the Gross price of this spot
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo PriceGross_Error
                PriceGross = _priceGross
                On Error GoTo 0
                Exit Property
PriceGross_Error:
                Err.Raise(Err.Number, "cPlannedSpot: PriceGross", Err.Description)
            End Get
            Set(ByVal value As Decimal)
                On Error GoTo PriceGross_Error
                _priceGross = value
                On Error GoTo 0
                Exit Property
PriceGross_Error:
                Err.Raise(Err.Number, "cPlannedSpot: PriceGross", Err.Description)
            End Set
        End Property

    End Class
End Namespace