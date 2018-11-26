Public Class cCriteria

    Private _Equal As String
    Public Property Equal() As String
        Get
            Return _Equal
        End Get
        Set(ByVal value As String)
            _Equal = value
        End Set
    End Property

    Private _MoreThan As String
    Public Property More() As String
        Get
            Return _MoreThan
        End Get
        Set(ByVal value As String)
            _MoreThan = value
        End Set
    End Property

    Private _Less As String
    Public Property Less() As String
        Get
            Return _Less
        End Get
        Set(ByVal value As String)
            _Less = value
        End Set
    End Property

    Private _EqualOrMore As String
    Public Property EqualOrMore() As String
        Get
            Return _EqualOrMore
        End Get
        Set(ByVal value As String)
            _EqualOrMore = value
        End Set
    End Property




End Class
