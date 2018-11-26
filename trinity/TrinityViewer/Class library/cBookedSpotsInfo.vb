Namespace TrinityViewer
    Public Class cBookedSpotsInfo
        Implements Collections.IEnumerable

        Public TotalNet As Decimal
        Public TotalGross As Decimal
        Public TotalTRP As Decimal

        Private mCol As New Collection

        Public Function Add() As cBookedSpotInfo
            Dim _newSpot As New cBookedSpotInfo
            Dim ID As String = CreateGUID()

            mCol.Add(_newSpot, ID)

            Return mCol(ID)

        End Function

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator
        End Function

    End Class
End Namespace