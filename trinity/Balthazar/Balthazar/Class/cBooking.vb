Public Class cBooking

    Public Structure DateTime
        Public [Date] As Date
        Public Time As Integer

        Public ReadOnly Property TimeString() As String
            Get
                If [Date].DayOfWeek = DayOfWeek.Saturday Then
                    If Time = 0 Then
                        Return "10-15"
                    Else
                        Return "11-16"
                    End If
                Else
                    If Time = 0 Then
                        Return "10-18"
                    Else
                        Return "11-19"
                    End If
                End If
            End Get
        End Property
    End Structure

    Public Enum BookingStatusEnum
        bsRejected = -1
        bsPending = 0
        bsConfirmed = 1
    End Enum

    Private _eventID As Integer
    Public Property EventID() As Integer
        Get
            Return _eventID
        End Get
        Set(ByVal value As Integer)
            _eventID = value
        End Set
    End Property

    Private _dates As List(Of DateTime)
    Public Property Dates() As List(Of DateTime)
        Get
            Return _dates
        End Get
        Set(ByVal value As List(Of DateTime))
            _dates = value
        End Set
    End Property

    Private _salesperson As cStaff
    Public Property Salesperson() As cStaff
        Get
            Return _salesperson
        End Get
        Set(ByVal value As cStaff)
            _salesperson = value
        End Set
    End Property

    Private _store As String
    Public Property Store() As String
        Get
            Return _store
        End Get
        Set(ByVal value As String)
            _store = value
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

    Private _contact As String
    Public Property Contact() As String
        Get
            Return _contact
        End Get
        Set(ByVal value As String)
            _contact = value
        End Set
    End Property

    Private _phoneNr As String
    Public Property PhoneNr() As String
        Get
            Return _phoneNr
        End Get
        Set(ByVal value As String)
            _phoneNr = value
        End Set
    End Property

    Private _products As New List(Of String)
    Public Property Products() As List(Of String)
        Get
            Return _products
        End Get
        Set(ByVal value As List(Of String))
            _products = value
        End Set
    End Property

    Private _providerConfirmed As cBooking.BookingStatusEnum
    Public Property ProviderConfirmed() As cBooking.BookingStatusEnum
        Get
            Return _providerConfirmed
        End Get
        Set(ByVal value As cBooking.BookingStatusEnum)
            _providerConfirmed = value
        End Set
    End Property

    Private _placement As String
    Public Property Placement() As String
        Get
            Return _placement
        End Get
        Set(ByVal value As String)
            _placement = value
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

    Private _responsible As String
    Public Property Responsible() As String
        Get
            Return _responsible
        End Get
        Set(ByVal value As String)
            _responsible = value
        End Set
    End Property

    Private _client As cClient
    Public Property Client() As cClient
        Get
            Return _client
        End Get
        Set(ByVal value As cClient)
            _client = value
        End Set
    End Property

    Private _comments As String
    Public Property Comments() As String
        Get
            Return _comments
        End Get
        Set(ByVal value As String)
            _comments = value
        End Set
    End Property

    Private _provider As cStaff
    Public Property Provider() As cStaff
        Get
            Return _provider
        End Get
        Set(ByVal value As cStaff)
            _provider = value
        End Set
    End Property

    Private _providerName As String
    Public Property ProviderName() As String
        Get
            If _provider Is Nothing Then
                Return _providerName
            Else
                Return _provider.Firstname
            End If
        End Get
        Set(ByVal value As String)
            _providerName = value
        End Set
    End Property

    Private _requestedProvider As String
    Public Property RequestedProvider() As String
        Get
            Return _requestedProvider
        End Get
        Set(ByVal value As String)
            _requestedProvider = value
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

    Private _status As BookingStatusEnum
    Public Property Status() As BookingStatusEnum
        Get
            Return _status
        End Get
        Set(ByVal value As BookingStatusEnum)
            _status = value
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

    Private _rejectionComment As String
    Public Property RejectionComment() As String
        Get
            Return _rejectionComment
        End Get
        Set(ByVal value As String)
            _rejectionComment = value
        End Set
    End Property

    Public Sub New(ByVal DatabaseID As Integer)
        _databaseID = DatabaseID
        If Not MyEvent Is Nothing AndAlso Not MyEvent.BookingData.ContainsKey(DatabaseID) Then
            MyEvent.BookingData.Add(DatabaseID, New cEvent.BookingDataStruct)
        End If
    End Sub

    Private _invoiced As Boolean
    Public Property Invoiced() As Boolean
        Get
            Return _invoiced
        End Get
        Set(ByVal value As Boolean)
            _invoiced = value
        End Set
    End Property

    Private _answeredQuestionaireDays As String
    Public Property AnsweredQuestionaireDays() As String
        Get
            Return _answeredQuestionaireDays
        End Get
        Set(ByVal value As String)
            _answeredQuestionaireDays = value
        End Set
    End Property

End Class
