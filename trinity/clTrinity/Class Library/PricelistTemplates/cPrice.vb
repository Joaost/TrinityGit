Namespace PricelistTemplates
    Public Class cPrice

        Public Property TargetName As String
        Public Property UniSize As Integer

        Private _price(10) As Single
        Public Property Price(Daypart As Integer) As Single
            Get
                Return _price(Daypart)
            End Get
            Set(value As Single)
                _price(Daypart) = value
            End Set
        End Property

    End Class
End Namespace