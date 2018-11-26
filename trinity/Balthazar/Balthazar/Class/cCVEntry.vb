Public Class cCVEntry


    Private _eventDatabaseID As Integer
    Public Property EventDatabaseID() As Integer
        Get
            Return _eventDatabaseID
        End Get
        Set(ByVal value As Integer)
            _eventDatabaseID = value
        End Set
    End Property

    Private _eventName As String
    Public Property EventName() As String
        Get
            Return _eventName
        End Get
        Set(ByVal value As String)
            _eventName = value
        End Set
    End Property

    Private _eventRole As String
    Public Property EventRole() As String
        Get
            Return _eventRole
        End Get
        Set(ByVal value As String)
            _eventRole = value
        End Set
    End Property

    Private _roleCategory As String
    Public Property RoleCategory() As String
        Get
            Return _roleCategory
        End Get
        Set(ByVal value As String)
            _roleCategory = value
        End Set
    End Property

    Private _responsiblePerson As String
    Public Property ResponsiblePerson() As String
        Get
            Return _responsiblePerson
        End Get
        Set(ByVal value As String)
            _responsiblePerson = value
        End Set
    End Property

    Private _workedMinutes As Integer
    Public Property WorkedMinutes() As Integer
        Get
            Return _workedMinutes
        End Get
        Set(ByVal value As Integer)
            _workedMinutes = value
        End Set
    End Property
End Class
