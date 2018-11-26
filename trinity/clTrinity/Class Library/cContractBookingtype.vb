

Namespace Trinity
    Public Class cContractBookingtype
        Private ParentColl As Collection

        Private mvarIsRBS As Boolean
        Private mvarIsSpecific As Boolean
        Private mvarIsPremium As Boolean

        Private mvarBuyingTarget As cContractTarget
        Private mvarParentChannel As cContractChannel

        Private mvarName As String = ""
        Private mvarShortName As String = ""

        Private mvarPrintDayparts As Boolean
        Private mvarPrintBookingCode As Boolean

        Private mvarIndexes As cIndexes
        Private mvarAddedValues As cAddedValues
        Private mvarFilmIndex(500) As Single

        Private mvarVolume As Decimal
        Private mvarActive As Boolean = False
        Private mvarActiveDate As Date = Nothing
        Private mvarMaxDiscount As Single = 1

        Private mvarRatecardCPPIsGross As Boolean = True

        Private mvarContractTargets As New Trinity.cContractTargets(Me)
        Private bolContractBookingtype

        Public Property MaxDiscount() As Single
            Get
                Return mvarMaxDiscount
            End Get
            Set(ByVal value As Single)
                mvarMaxDiscount = value
            End Set
        End Property

        Public Property IsContractBookingtype() As Boolean
            'This boolean represents if the Target is a Contract only Target
            Get
                Return bolContractBookingtype
            End Get
            Set(ByVal value As Boolean)
                bolContractBookingtype = value
            End Set
        End Property

        Public Property RatecardCPPIsGross() As Boolean
            Get
                Return mvarRatecardCPPIsGross
            End Get
            Set(ByVal value As Boolean)
                mvarRatecardCPPIsGross = value
            End Set
        End Property

        Public Property IsRBS() As Boolean
            '---------------------------------------------------------------------------------------
            ' Procedure : IsRBS
            ' DateTime  : 2003-07-10 15:33
            ' Author    : joho
            ' Purpose   : Returns/sets wether this booking tyoe should be regarded as a RBS
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IsRBS_Error

                IsRBS = mvarIsRBS

                On Error GoTo 0
                Exit Property

IsRBS_Error:

                Err.Raise(Err.Number, "cBookingType: IsRBS", Err.Description)
            End Get
            Set(ByVal value As Boolean)
                On Error GoTo IsRBS_Error

                mvarIsRBS = value

                On Error GoTo 0
                Exit Property

IsRBS_Error:

                Err.Raise(Err.Number, "cBookingType: IsRBS", Err.Description)


            End Set
        End Property

        Public Property IsSpecific() As Boolean
            '---------------------------------------------------------------------------------------
            ' Procedure : IsSpecific
            ' DateTime  : 2003-07-10 15:33
            ' Author    : joho
            ' Purpose   : Returns/sets wether this BookingType should be regarded as a specific
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IsSpecific_Error

                IsSpecific = mvarIsSpecific

                On Error GoTo 0
                Exit Property

IsSpecific_Error:

                Err.Raise(Err.Number, "cBookingType: IsSpecific", Err.Description)
            End Get
            Set(ByVal value As Boolean)
                On Error GoTo IsSpecific_Error

                mvarIsSpecific = value

                On Error GoTo 0
                Exit Property

IsSpecific_Error:

                Err.Raise(Err.Number, "cBookingType: IsSpecific", Err.Description)

            End Set
        End Property

        Public Property IsPremium() As Boolean
            '---------------------------------------------------------------------------------------
            ' Procedure : IsPremium
            ' DateTime  : 2003-07-10 15:33
            ' Author    : joho
            ' Purpose   : Returns/sets wether this BookingType should be regarded as a Premium
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IsPremium_Error

                IsPremium = mvarIsPremium

                On Error GoTo 0
                Exit Property

IsPremium_Error:

                Err.Raise(Err.Number, "cBookingType: IsPremium", Err.Description)
            End Get
            Set(ByVal value As Boolean)
                On Error GoTo IsPremium_Error

                mvarIsPremium = value

                On Error GoTo 0
                Exit Property

IsPremium_Error:

                Err.Raise(Err.Number, "cBookingType: IsPremium", Err.Description)

            End Set
        End Property

        Public Property PrintBookingCode() As Boolean
            Get
                Return mvarPrintBookingCode
            End Get
            Set(ByVal value As Boolean)
                mvarPrintBookingCode = value
            End Set
        End Property

        Public Property PrintDayparts() As Boolean
            Get
                Return mvarPrintDayparts
            End Get
            Set(ByVal value As Boolean)
                mvarPrintDayparts = value
            End Set
        End Property

        Public Property ContractTargets() As Trinity.cContractTargets
            Get
                Return mvarContractTargets
            End Get
            Set(ByVal value As Trinity.cContractTargets)
                mvarContractTargets = value
            End Set
        End Property

        Public Property ActiveFromDate() As Date
            Get
                Return mvarActiveDate
            End Get
            Set(ByVal value As Date)
                mvarActiveDate = value
            End Set
        End Property

        Public Property Active() As Boolean
            Get
                Return mvarActive
            End Get
            Set(ByVal value As Boolean)
                mvarActive = value
            End Set
        End Property

        Public Property NegotiatedVolume() As Decimal
            'this value is for the 
            Get
                Return mvarVolume
            End Get
            Set(ByVal value As Decimal)
                mvarVolume = value
            End Set
        End Property

        Public Property Indexes() As cIndexes
            Get
                Indexes = mvarIndexes
            End Get
            Set(ByVal value As cIndexes)
                mvarIndexes = value
            End Set
        End Property

        Public Property AddedValues() As cAddedValues
            Get
                AddedValues = mvarAddedValues
            End Get
            Set(ByVal value As cAddedValues)
                mvarAddedValues = value
            End Set
        End Property

        Public Property FilmIndex(ByVal Length As Integer) As Single
            Get
                Try
                    FilmIndex = mvarFilmIndex(Length)
                Catch

                End Try
            End Get
            Set(ByVal value As Single)
                mvarFilmIndex(Length) = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return ParentChannel.ChannelName & " " & mvarName
        End Function

        Public Property Name() As String
            Get
                Name = mvarName
            End Get
            Set(ByVal value As String)
                Dim TmpBookingType As cContractBookingtype

                If value <> mvarName Then
                    If Not ParentColl Is Nothing Then
                        If ParentColl.Contains(value) Then
                            Throw New System.Exception("Bookingtype already exists.")
                        Else
                            If ParentColl.Contains(mvarName) Then
                                TmpBookingType = ParentColl(mvarName)
                                ParentColl.Remove(mvarName)
                                If Not TmpBookingType Is Nothing Then
                                    ParentColl.Add(TmpBookingType, value)
                                End If
                            End If
                        End If
                    End If
                End If
                mvarName = value
            End Set
        End Property

        Public Property ShortName() As String
            Get
                ShortName = mvarShortName
            End Get
            Set(ByVal value As String)
                mvarShortName = value
            End Set
        End Property

        Friend WriteOnly Property ParentCollection()
            ' Purpose   : Sets the Collection of wich this channel is a member. This is used
            '             when a new Name is set. See that property for further explanation
            Set(ByVal value)
                ParentColl = value
            End Set
        End Property

        Friend Property ParentChannel() As cContractChannel
            Get
                ParentChannel = mvarParentChannel
            End Get
            Set(ByVal value As cContractChannel)
                mvarParentChannel = value
            End Set
        End Property

        Public Property BuyingTarget() As cContractTarget
            ' Purpose   : Pointer to the cPriceListTarget object containing the BuyingTarget
            '             for the BuyingType
            Get
                On Error GoTo BuyingTarget_Error

                Dim SaveUniSize As Boolean

                If Not mvarBuyingTarget Is Nothing Then
                    'If mvarBuyingTarget.Target Is Nothing Then mvarBuyingTarget.Target = New cTarget(Campaign)
                    'SaveUniSize = mvarBuyingTarget.Target.NoUniverseSize
                    'mvarBuyingTarget.Target.NoUniverseSize = True
                    'mvarBuyingTarget.Target.Universe = mvarParentChannel.BuyingUniverse
                    'mvarBuyingTarget.Target.NoUniverseSize = SaveUniSize
                    'mvarBuyingTarget.Bookingtype = Me
                Else
                    'mvarBuyingTarget = New cPricelistTarget(Campaign)
                End If
                BuyingTarget = mvarBuyingTarget

                On Error GoTo 0
                Exit Property

BuyingTarget_Error:

                Err.Raise(Err.Number, "cBookingType: BuyingTarget", Err.Description)
            End Get
            Set(ByVal value As cContractTarget)
                On Error GoTo BuyingTarget_Error

                Dim TmpWeek As cWeek
                Dim TmpIndex As cIndex
                Dim i As Integer

                mvarBuyingTarget = value

                On Error GoTo 0
                Exit Property

BuyingTarget_Error:

                Err.Raise(Err.Number, "cBookingType: BuyingTarget", Err.Description)


            End Set
        End Property

        Public Sub New(ByVal Main As cKampanj, ByVal parentChannel As cContractChannel)
            mvarParentChannel = parentChannel
            mvarAddedValues = New cAddedValues(MainObject:=Main)
        End Sub
    End Class
End Namespace
