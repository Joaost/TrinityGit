Imports System.Runtime
Imports System.Runtime.Serialization

Partial Public Class Marathon
    Public Class TimeEntry

        Private _userID As String
        Public Property UserId() As String
            Get
                Return _userID
            End Get
            Set(ByVal value As String)
                _userID = value.ToUpper
            End Set
        End Property

        Private _companyID As String
        Public Property CompanyID() As String
            Get
                Return _companyID
            End Get
            Set(ByVal value As String)
                _companyID = value
            End Set
        End Property

        Private _projectID As String
        Public Property ProjectID() As String
            Get
                Return _projectID
            End Get
            Set(ByVal value As String)
                _projectID = value
            End Set
        End Property

        Private _clientID As String
        Public Property ClientID() As String
            Get
                Return _clientID
            End Get
            Set(ByVal value As String)
                _clientID = value
            End Set
        End Property

        Private _typeID As String
        Public Property TypeID() As String
            Get
                Return _typeID
            End Get
            Set(ByVal value As String)
                _typeID = value
            End Set
        End Property

        Private _id As Long
        Public Property ID() As Long
            Get
                Return _id
            End Get
            Set(ByVal value As Long)
                _id = value
            End Set
        End Property

        Private _hours As Single
        Public Property Hours() As Single
            Get
                Return _hours
            End Get
            Set(ByVal value As Single)
                _hours = value
            End Set
        End Property

        Private _date As String
        Public Property [Date]() As String
            Get
                Return _date
            End Get
            Set(ByVal value As String)
                _date = value
            End Set
        End Property

        Private _comment As String
        Public Property Comment() As String
            Get
                Return _comment
            End Get
            Set(ByVal value As String)
                _comment = value
            End Set
        End Property

    End Class
End Class
