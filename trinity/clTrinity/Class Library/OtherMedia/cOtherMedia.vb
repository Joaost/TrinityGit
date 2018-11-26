Namespace Trinity
    Public Class cOtherMedia

        Property Name As String

        Public ReadOnly Property GrossCPT As Single
            Get

            End Get
        End Property

        Public Property NetCPT As Single
            Get

            End Get
            Set(value As Single)

            End Set
        End Property

        Private _discount As Single
        Public Property Discount As Single
            Get
                Return _discount
            End Get
            Set(value As Single)
                _discount = value
            End Set
        End Property

    End Class
End Namespace
