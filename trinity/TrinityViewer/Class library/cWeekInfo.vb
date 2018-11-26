Namespace TrinityViewer
    Public Class cWeekInfo
        Private _name As String
        Private _startDate As Date
        Private _endDate As Date
        Private _parent As Object
        Private _trp As Single
        Private _films As cFilmsInfo
        Private _grossBudget As Decimal
        Private _netBudget As Decimal
        Private _netCPP30 As Decimal
        Private _trpBuyingTarget As Decimal

        Public Property TRPBuyingTarget() As Decimal
            Get
                Return _trpBuyingTarget
            End Get
            Set(ByVal value As Decimal)
                _trpBuyingTarget = value
            End Set
        End Property

        Public Property NetCPP30() As Decimal
            Get
                Return _netCPP30
            End Get
            Set(ByVal value As Decimal)
                _netCPP30 = value
            End Set
        End Property

        Public Property NetBudget() As Decimal
            Get
                Return _netBudget
            End Get
            Set(ByVal value As Decimal)
                _netBudget = value
            End Set
        End Property

        Public Property GrossBudget() As Decimal
            Get
                Return _grossBudget
            End Get
            Set(ByVal value As Decimal)
                _grossBudget = value
            End Set
        End Property

        Public Property Films() As TrinityViewer.cFilmsInfo
            Get
                Return _films
            End Get
            Set(ByVal value As TrinityViewer.cFilmsInfo)
                _films = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Public Property StartDate() As Date
            Get
                If _parent.GetType.Name = "cWeekInfoSums" Then
                    Return DirectCast(_parent, cWeekInfoSums).MainObject.Channels(1).BookingTypes(1).Weeks(_name).StartDate
                Else
                    Return _startDate
                End If
            End Get
            Set(ByVal value As Date)
                _startDate = value
            End Set
        End Property

        Public Property EndDate() As Date
            Get
                If _parent.GetType.Name = "cWeekInfoSums" Then
                    Return _parent.MainObject.Channels(1).Bookingtypes(1).weeks(_name).enddate
                Else
                    Return _endDate
                End If
            End Get
            Set(ByVal value As Date)
                _endDate = value
            End Set
        End Property

        Public Sub New(ByVal Parent As Object)
            _parent = Parent
            _films = New cFilmsInfo(Me)
        End Sub

        Public Property TRP() As Single
            Get
                Dim TRPs As Single = 0
                If _parent.GetType.Name = "cWeekInfoSums" Then
                    With DirectCast(_parent, cWeekInfoSums)
                        For Each TmpChan As cChannelInfo In .MainObject.Channels
                            For Each TmpBT As cBookingTypeInfo In TmpChan.BookingTypes
                                TRPs += TmpBT.Weeks(_name).TRP
                            Next
                        Next
                    End With
                    Return TRPs
                Else
                    Return _trp
                End If
            End Get
            Set(ByVal value As Single)
                _trp = value
            End Set
        End Property

    End Class
End Namespace