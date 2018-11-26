Partial Public Class Marathon
    Public Class Pricerow
        Private _code As String = "000"
        Public Property Code() As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property

        Private _quantity As Integer = 1
        Public Property Quantity() As Integer
            Get
                Return _quantity
            End Get
            Set(ByVal value As Integer)
                _quantity = value
            End Set
        End Property

        Private _unitPrice As Single
        Public Property UnitPrice() As Single
            Get
                Return _unitPrice
            End Get
            Set(ByVal value As Single)
                _unitPrice = value
            End Set
        End Property

        Private _discountCode As String
        Public Property DiscountCode() As String
            Get
                Return _discountCode
            End Get
            Set(ByVal value As String)
                _discountCode = value
            End Set
        End Property

        Private _netCose As Single
        Public Property NetCost() As Single
            Get
                Return _netCose
            End Get
            Set(ByVal value As Single)
                _netCose = value
            End Set
        End Property

    End Class
End Class