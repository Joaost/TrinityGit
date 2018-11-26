Public Class cUserInfo

    Enum UserTypeEnum
        Staff = 1
        Salesman = 2
        Provider = 3
        HeadOfSales = 4
    End Enum

    Private _type As UserTypeEnum
    Public Property Type() As UserTypeEnum
        Get
            Return _type
        End Get
        Set(ByVal value As UserTypeEnum)
            _type = value
        End Set
    End Property

    Private _firstName As String
    Public Property FirstName() As String
        Get
            Return _firstName
        End Get
        Set(ByVal value As String)
            _firstName = value
        End Set
    End Property

    Private _lastName As String
    Public Property LastName() As String
        Get
            Return _lastName
        End Get
        Set(ByVal value As String)
            _lastName = value
        End Set
    End Property

    Private _mobilePhone As String
    Public Property MobilePhone() As String
        Get
            Return _mobilePhone
        End Get
        Set(ByVal value As String)
            _mobilePhone = value
        End Set
    End Property

    Private _id As Integer
    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _clientID As Integer
    Public Property clientID() As Integer
        Get
            Return _clientID
        End Get
        Set(ByVal value As Integer)
            _clientID = value
        End Set
    End Property

    Private _canCreateBookings As Boolean
    Public Property CanCreateBookings() As Boolean
        Get
            Return _canCreateBookings
        End Get
        Set(ByVal value As Boolean)
            _canCreateBookings = value
        End Set
    End Property
End Class
