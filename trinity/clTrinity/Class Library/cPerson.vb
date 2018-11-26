Namespace Trinity
    Public Class cPerson
        Private _id As Integer
        Private _name As String = ""
        Public Property Phone As String = ""
        Public Property Email As String = ""

        Public Property id() As Integer
            Get
                Return _id
            End Get
            Set(ByVal value As Integer)
                _id = value
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
        Private _statusActive As Boolean
        Public Property statusActive() As Boolean
            Get
                Return _statusActive
            End Get
            Set(ByVal value As Boolean)
                _statusActive = value
            End Set
        End Property
    End Class
End Namespace

