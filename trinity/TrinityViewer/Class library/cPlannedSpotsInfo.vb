Namespace TrinityViewer
    Public Class cPlannedSpotsInfo
        Implements Collections.IEnumerable

        Public TotalTRP As Decimal
        Public TRPToDeliver As Single
        Private mCol As New Collection

        Public Function Add() As cPlannedSpotInfo
            Dim _newSpot As New cPlannedSpotInfo

            mCol.Add(_newSpot)

            Return _newSpot
        End Function

        Public Function TotalNetBudget(Optional ByVal ChannelName As String = "", Optional ByVal Bookingtype As String = "") As Single
            Dim TmpBudget As Single = 0

            For Each TmpSpot As cPlannedSpotInfo In mCol
                If (ChannelName = "" OrElse ChannelName = TmpSpot.Channel.ChannelName) AndAlso (Bookingtype = "" OrElse Bookingtype = TmpSpot.Bookingtype.Name) Then
                    TmpBudget += TmpSpot.PriceNet
                End If
            Next
            Return TmpBudget
        End Function

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator()
        End Function
    End Class
End Namespace