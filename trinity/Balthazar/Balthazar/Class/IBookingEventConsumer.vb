Public Interface IBookingEventConsumer

    Sub BookingsUpdated(ByRef e As BookingsUpdatedEventArgs)

    Class BookingsUpdatedEventArgs
        Inherits EventArgs

        Private _listOfUpdatedEntries As New Dictionary(Of Integer, cBooking)
        Public Property ListOfUpdatedEntries() As Dictionary(Of Integer, cBooking)
            Get
                Return _listOfUpdatedEntries
            End Get
            Set(ByVal value As Dictionary(Of Integer, cBooking))
                _listOfUpdatedEntries = value
            End Set
        End Property

        Private _listOfAllEntries As New Dictionary(Of Integer, cBooking)
        Public Property ListOfAllEntries() As Dictionary(Of Integer, cBooking)
            Get
                Return _listOfAllEntries
            End Get
            Set(ByVal value As Dictionary(Of Integer, cBooking))
                _listOfAllEntries = value
            End Set
        End Property
    End Class
End Interface


