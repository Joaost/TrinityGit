Namespace Trinity
    Public Class cLinkedCampaign

        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Private _databaseID As Integer
        Public Property DatabaseID() As Integer
            Get
                Return _databaseID
            End Get
            Set(ByVal value As Integer)
                _databaseID = value
            End Set
        End Property

        Private _path As String
        Public Property Path() As String
            Get
                Return _path
            End Get
            Set(ByVal value As String)
                _path = value
            End Set
        End Property

        Private _link As Boolean
        Public Property Link() As Boolean
            Get
                Return _link
            End Get
            Set(ByVal value As Boolean)
                _link = value
            End Set
        End Property

        Private _brokenLink As Boolean = False
        Public Property BrokenLink() As Boolean
            Get
                Return _brokenLink
            End Get
            Set(ByVal value As Boolean)
                _brokenLink = value
            End Set
        End Property

        Private _clientID As Integer
        Public Property ClientID() As Integer
            Get
                Return _clientID
            End Get
            Set(ByVal value As Integer)
                _clientID = value
            End Set
        End Property

        Private _productID As Integer
        Public Property ProductID() As Integer
            Get
                Return _productID
            End Get
            Set(ByVal value As Integer)
                _productID = value
            End Set
        End Property

    End Class
End Namespace
