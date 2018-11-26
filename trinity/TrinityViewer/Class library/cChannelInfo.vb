Namespace TrinityViewer
    Public Class cChannelInfo

        Private _channelName As String
        Private _bookingTypes As cBookingTypeInfos
        Private _budgetTotalCTC As Decimal
        Private _actualTotalCTC As Decimal
        Private _confirmedTotalCTC As Decimal
        Private _adedgeNames As String
        Private _mainTarget As cTargetInfo
        Private _actualTRP As Decimal
        Private Main As cCampaignInfo
        Private _shortName As String

        Public Property ShortName() As String
            Get
                Return _shortName
            End Get
            Set(ByVal value As String)
                _shortName = value
            End Set
        End Property

        Public Function NetBudget() As Decimal
            Dim TmpBudget As Decimal = 0
            For Each TmpBT As cBookingTypeInfo In _bookingTypes
                If TmpBT.BookIt Then
                    For Each TmpWeek As TrinityViewer.cWeekInfo In TmpBT.Weeks
                        TmpBudget += TmpWeek.NetBudget
                    Next
                End If
            Next
            Return TmpBudget
        End Function

        Public Property AdedgeNames() As String
            Get
                Return _adedgeNames
            End Get
            Set(ByVal value As String)
                _adedgeNames = value
            End Set
        End Property

        Public ReadOnly Property BookingTypes() As cBookingTypeInfos
            Get
                Return _bookingTypes
            End Get
        End Property

        Public Property ChannelName() As String
            Get
                Return _channelName
            End Get
            Set(ByVal value As String)
                _channelName = value
            End Set
        End Property

        Public Property MainTarget() As cTargetInfo
            Get
                Return _mainTarget
            End Get
            Set(ByVal value As cTargetInfo)
                _mainTarget = value
            End Set
        End Property

        Public Sub New(ByVal MainObject As cCampaignInfo)
            Main = MainObject
            _bookingTypes = New cBookingTypeInfos(Main)
            _mainTarget = New cTargetInfo(Main)
        End Sub

        Public Function PlannedTRP() As Decimal
            Dim TRP As Decimal = 0
            For Each TmpBT As TrinityViewer.cBookingTypeInfo In _bookingTypes
                If TmpBT.BookIt Then
                    For Each TmpWeek As TrinityViewer.cWeekInfo In TmpBT.Weeks
                        TRP += TmpWeek.TRP
                    Next
                End If
            Next
            Return TRP
        End Function

        Public Function TRPToDeliver() As Decimal
            Dim TRP As Decimal = 0
            For Each TmpSpot As TrinityViewer.cPlannedSpotInfo In Main.PlannedSpots
                If TmpSpot.Channel Is Me Then
                    If TmpSpot.AirDate > Main.UpdatedTo.ToOADate Then
                        TRP += TmpSpot.MyRating(cPlannedSpotInfo.PlannedTargetEnum.pteMainTarget)
                    End If
                End If
            Next
            Return TRP
        End Function

        Public Property ActualTRP() As Decimal
            Get
                Return _actualtrp
            End Get
            Set(ByVal value As Decimal)
                _actualtrp = value
            End Set
        End Property

    End Class

End Namespace
