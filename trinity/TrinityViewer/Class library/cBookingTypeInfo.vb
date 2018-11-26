Namespace TrinityViewer
    Public Class cBookingTypeInfo

        Private _name As String
        Private _weeks As New cWeekInfos
        Private _buyingTarget As cTargetInfo
        Private _bookIt As Boolean
        Private _confirmedNetBudget As Decimal
        Private _daypartSplit(0 To 5) As Integer
        Private _indexMainTarget As Single
        Private _indexSecondTarget As Single
        Private _indexAllAdults As Single
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

        Public Property IndexMainTarget() As Single
            Get
                Return _indexMainTarget
            End Get
            Set(ByVal value As Single)
                _indexMainTarget = value
            End Set
        End Property

        Public Property IndexSecondTarget() As Single
            Get
                Return _indexSecondTarget
            End Get
            Set(ByVal value As Single)
                _indexSecondTarget = value
            End Set
        End Property

        Public Property IndexAllAdults() As Single
            Get
                Return _indexAllAdults
            End Get
            Set(ByVal value As Single)
                _indexAllAdults = value
            End Set
        End Property

        Public Function PlannedNetBudget()
            '---------------------------------------------------------------------------------------
            ' Procedure : PlannedNetBudget
            ' DateTime  : 2003-07-03 12:03
            ' Author    : joho
            ' Purpose   : Calculates the planned net budget for the bookingtype based on CPP
            '             and booked TRPs
            '---------------------------------------------------------------------------------------
            '

            On Error GoTo PlannedNetBudget_Error

            Dim TmpWeek As cWeekInfo

            PlannedNetBudget = 0
            For Each TmpWeek In _weeks
                PlannedNetBudget = PlannedNetBudget + TmpWeek.NetBudget
            Next

            On Error GoTo 0
            Exit Function

PlannedNetBudget_Error:

            Err.Raise(Err.Number, "cBookingType: PlannedNetBudget", Err.Description)

        End Function

        Public Property DaypartSplit(ByVal dp As Integer) As Integer
            Get
                Return _daypartSplit(dp)
            End Get
            Set(ByVal value As Integer)
                _daypartSplit(dp) = value
            End Set
        End Property

        Public Property ConfirmedNetBudget() As Decimal
            Get
                Return _confirmedNetBudget
            End Get
            Set(ByVal value As Decimal)
                _confirmedNetBudget = value
            End Set
        End Property

        Public Property BookIt() As Boolean
            Get
                Return _bookIt
            End Get
            Set(ByVal value As Boolean)
                _bookIt = value
            End Set
        End Property

        Public Property BuyingTarget() As cTargetInfo
            Get
                Return _buyingTarget
            End Get
            Set(ByVal value As cTargetInfo)
                _buyingTarget = value
            End Set
        End Property

        Public ReadOnly Property Weeks() As cWeekInfos
            Get
                Return _weeks
            End Get
        End Property

        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Public Function GetWeek(ByVal d As Date) As cWeekInfo
            Dim TmpWeek As cWeekInfo

            GetWeek = Nothing
            For Each TmpWeek In _weeks
                If TmpWeek.StartDate.ToOADate <= Int(d.ToOADate) Then
                    If TmpWeek.EndDate.ToOADate >= Int(d.ToOADate) Then
                        GetWeek = TmpWeek
                        Exit For
                    End If
                End If
            Next
        End Function

        Public Sub New(ByVal MainObject As cCampaignInfo)
            Main = MainObject
            _buyingTarget = New cTargetInfo(Main)
        End Sub
    End Class
End Namespace
