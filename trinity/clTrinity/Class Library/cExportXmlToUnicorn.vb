Imports clTrinity.Trinity

Public Class cExportXmlToUnicorn

    Dim _campaign As Trinity.cKampanj
    Dim amountOfWeeks As Integer = 0

    Dim ownCommission As Boolean = False
    Dim ownCommissionAmount As Single = 0

    Public Sub New(ByRef Campaign As Trinity.cKampanj)
        _campaign = Campaign
        amountOfWeeks = getAmountOfWeeks

    End Sub

    Private Function getAmountOfWeeks()
        Dim tmpWeeks As Integer
        For each tmpChannel As cChannel  In _campaign.Channels
            For each tmpBt As cBookingType In tmpChannel.BookingTypes
                tmpWeeks = tmpBt.Weeks.Count
                Return tmpWeeks
            Next
        Next
        Return False
    End Function

    Public sub exportAsXML(optional byref useOwnCommission As Boolean = False, Optional ByVal useOwnCommissionAmount As Decimal = 0)
            If useOwnCommission Then
            ownCommission = True
            ownCommissionAmount = useOwnCommissionAmount / 100
        End If
    End sub
    
End Class
