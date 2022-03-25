Public Class cAgency

    Private _agencyName As String
    Public Property AgencyName() As String
        Get
            Return _agencyName
        End Get
        Set(ByVal value As String)
            _agencyName = value
        End Set
    End Property
    Private _agencyGUID As String
    Public Property agencyGUID() As String
        Get
            Return _agencyGUID
        End Get
        Set(ByVal value As String)
            _agencyGUID = value
        End Set
    End Property

    Private _listOfClients As New List(Of Object)
    Public Property listOfClients() As List(Of Object)
        Get
            Return _listOfClients
        End Get
        Set(ByVal value As List(Of Object))
            _listOfClients = value
        End Set
    End Property

End Class
