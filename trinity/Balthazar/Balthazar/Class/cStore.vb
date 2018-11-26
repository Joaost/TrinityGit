Public Class cStore

    Private _databaseID As Integer
    Public Property DatabaseID() As Integer
        Get
            Return _databaseID
        End Get
        Set(ByVal value As Integer)
            _databaseID = value
        End Set
    End Property

    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Private _address As String
    Public Property Address() As String
        Get
            Return _address
        End Get
        Set(ByVal value As String)
            _address = value
        End Set
    End Property

    Private _city As String
    Public Property City() As String
        Get
            Return _city
        End Get
        Set(ByVal value As String)
            _city = value
        End Set
    End Property

    Private _phoneNo As String
    Public Property PhoneNo() As String
        Get
            Return _phoneNo
        End Get
        Set(ByVal value As String)
            _phoneNo = value
        End Set
    End Property

End Class
