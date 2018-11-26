Namespace Trinity
    Public Class PriceListEventArgs
        Inherits EventArgs

        ' The week that called the event
        Public ReadOnly Target As Trinity.cPricelistTarget
        Public ReadOnly BookingType As Trinity.cBookingType
        Public ReadOnly Progress As Integer

        Public Sub New(ByVal progress As Integer, ByVal target As Trinity.cPricelistTarget, ByVal bookingType As Trinity.cBookingType)

            Me.Target = target
            Me.BookingType = bookingType
            Me.Progress = progress

        End Sub

    End Class
End Namespace

