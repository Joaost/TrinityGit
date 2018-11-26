Public Class cStaffCategory
    Inherits cCost

    Public ID As String
    Public CostPerHourCTC As Integer
    Public CostPerHourActual As Integer
    Private _quantity As Integer = -1
    Private _days As Integer = -1
    Public HoursPerDay As Single = 0
    Public DatabaseID As Integer = -1

    Private _main As cEvent

    Public Overrides Function ToString() As String
        Return MyBase.Name
    End Function

    Public Function getQuantityFromRoles() As Integer
        Dim TmpQuantity As Integer = 0
        For Each TmpRole As cRole In _main.Roles
            If TmpRole.Category Is Me Then
                TmpQuantity += TmpRole.Quantity
            End If
        Next
        Return TmpQuantity
    End Function

    Public Function getDaysFromLocations() As Integer
        Dim TmpDays As Integer = 0
        For Each TmpLoc As cLocation In MyEvent.Locations
            TmpDays += (Int(TmpLoc.ToDate.ToOADate) - Int(TmpLoc.FromDate.ToOADate)) + 1
        Next
        Return TmpDays
    End Function

    Public Property Quantity() As Integer
        Get
            If _quantity < 0 Then
                Return getQuantityFromRoles()
            Else
                Return _quantity
            End If
        End Get
        Set(ByVal value As Integer)
            _quantity = value
        End Set
    End Property

    Public Property Days() As Integer
        Get
            If _days < 0 Then
                Return getDaysFromLocations()
            Else
                Return _days
            End If
        End Get
        Set(ByVal value As Integer)
            _days = value
        End Set
    End Property

    Public Overrides Property CTC() As Decimal
        Get
            Return Quantity * Days * HoursPerDay * CostPerHourCTC
        End Get
        Set(ByVal value As Decimal)

        End Set
    End Property

    Public Overrides Property ActualCost() As Decimal
        Get
            Return Quantity * Days * HoursPerDay * CostPerHourActual
        End Get
        Set(ByVal value As Decimal)

        End Set
    End Property

    Public Sub New(ByVal Main As cEvent)
        _main = Main
    End Sub
End Class
