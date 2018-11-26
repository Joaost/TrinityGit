Namespace TrinityViewer

    Public Class cBookedSpotInfo

        Private _priceNet As Decimal
        Private _priceGross As Decimal
        Private _channel As cChannelInfo
        Private _bookingtype As cBookingTypeInfo
        Private _mam As Integer
        Private _airDate As Date
        Private _filmcode As String
        Private _myEstimate As Decimal
        Private _myEstimateBuyTarget As Decimal

        Public Week As cWeekInfo

        Public Property MyEstimate()
            Get
                Return _myEstimate
            End Get
            Set(ByVal value)
                _myEstimate = value
            End Set
        End Property

        Public Property MyEstimateBuyTarget()
            Get
                Return _myEstimateBuyTarget
            End Get
            Set(ByVal value)
                _myEstimateBuyTarget = value
            End Set
        End Property

        Public Property Filmcode() As String
            Get
                Return _filmcode
            End Get
            Set(ByVal value As String)
                _filmcode = value
            End Set
        End Property

        Public Property AirDate() As Date
            Get
                Return _airDate
            End Get
            Set(ByVal value As Date)
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
