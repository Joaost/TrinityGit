Namespace Trinity

    Public Class cContractTarget

        Private mvarBookingtype As cContractBookingtype
        Private mvarDefaultDaypart(4) As Byte

        'this is only used when there are targets/channels not present in the regular pricelist
        Private mvarPricelistPeriods As cPricelistPeriods
        Private mvarIsEntered As EnteredEnum
        Private mvarEnteredValue As Double

        Private mvarContractTargets As cContractTargets
        Private mvarCalcCPP As Boolean
        Private strTargetName As String
        Private strAdedgeTargetName As String
        Private mvarIndexes As cIndexes

        Private bolContractTarget As Boolean = False

        Public Enum TargetTypeEnum
            trgMnemonicTarget = 0
            trgUserTarget = 1
            trgDynamicTarget = 2
        End Enum

        Private mvarTargetType As TargetTypeEnum

        Public Enum EnteredEnum
            eDiscount = 0
            eCPT = 1
            eCPP = 2
            eEnhancement = 3
        End Enum

        Public Property IsContractTarget() As Boolean
            'This boolean represents if the Target is a Contract only Target
            Get
                Return bolContractTarget
            End Get
            Set(ByVal value As Boolean)
                bolContractTarget = value
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

        Public Overrides Function ToString() As String
            Return strTargetName
        End Function

        Public Property TargetType() As TargetTypeEnum
            ' Purpose   : Returns/sets the type of target used:
            '
            '               0 - Mnemonic Target
            '               1 - User Target     
            '               2 - Dynamic Target  (not implemented)
            '
            '---------------------------------------------------------------------------------------
            Get
                TargetType = mvarTargetType
            End Get
            Set(ByVal value As TargetTypeEnum)
                mvarTargetType = value
            End Set
        End Property

        Public Property CalcCPP() As Boolean
            Get
                Return mvarCalcCPP
            End Get
            Set(ByVal value As Boolean)
                mvarCalcCPP = value
            End Set
        End Property

        Public Property PricelistPeriods() As cPricelistPeriods
            Get
                PricelistPeriods = mvarPricelistPeriods
            End Get
            Set(ByVal value As cPricelistPeriods)
                mvarPricelistPeriods = value
            End Set
        End Property

        Public Property EnteredValue() As Double
            Get
                EnteredValue = mvarEnteredValue
            End Get
            Set(ByVal value As Double)
                mvarEnteredValue = value
            End Set
        End Property

        Public Property IsEntered() As EnteredEnum
            Get
                IsEntered = mvarIsEntered
            End Get
            Set(ByVal value As EnteredEnum)
                mvarIsEntered = value
            End Set
        End Property

        Friend Property Bookingtype() As cContractBookingtype
            Get
                Return mvarBookingtype
            End Get
            Set(ByVal value As cContractBookingtype)
                mvarBookingtype = value
            End Set
        End Property

        Public Property TargetName() As String
            Get
                Return strTargetName
            End Get
            Set(ByVal value As String)
                If Not strTargetName Is Nothing Then
                    mvarContractTargets.Remove(strTargetName)
                End If
                'Try
                '    mvarContractTargets.Remove(strTargetName)
                'Catch

                'End Try
                strTargetName = value
                mvarContractTargets.Add(Me)
            End Set
        End Property

        Public Property AdEdgeTargetName() As String
            Get
                Return strAdedgeTargetName
            End Get
            Set(ByVal value As String)
                strAdedgeTargetName = value
            End Set
        End Property

        Public Property DefaultDaypart(ByVal Daypart) As Byte
            Get
                On Error GoTo DefaultDay_Error

                DefaultDaypart = mvarDefaultDaypart(Daypart)

                On Error GoTo 0
                Exit Property

DefaultDay_Error:

                Err.Raise(Err.Number, "cPriceList: DefaultDay", Err.Description)
            End Get
            Set(ByVal value As Byte)
                On Error GoTo DefaultDay_Error

                mvarDefaultDaypart(Daypart) = value

                On Error GoTo 0
                Exit Property

DefaultDay_Error:

                Err.Raise(Err.Number, "cPriceList: DefaultDay", Err.Description)

            End Set
        End Property

        Public Sub New(ByVal main As cContractTargets)
            mvarContractTargets = main
            mvarPricelistPeriods = New cPricelistPeriods(Me)
            mvarIndexes = New cIndexes(main.Bookingtype.ParentChannel.MainObject, Me)
        End Sub
    End Class
End Namespace
