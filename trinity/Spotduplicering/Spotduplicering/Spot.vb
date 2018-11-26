
Public Class Spots
    Implements IEnumerable

    Dim mList As Collection

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return mList.GetEnumerator
    End Function

    Public Sub Add(ByVal tmpSpot As Spot)
        mList.Add(tmpSpot, tmpSpot.Guid)
    End Sub

    Public Sub Remove(ByVal tmpSpot As Spot)
        mList.Remove(tmpSpot.Guid)
    End Sub

    Public Sub New()
        mList = New Collection
    End Sub

End Class

Public Class Spot
    Public Advertiser As String
    Public Product As String
    Public Datum As String
    Public Tid As String
    Public Kanal As String
    Public BreakID As String
    Public Filmkod As String
    Public ProgramAfter As String
    Public Produkt As String
    Public TRP As Double
    Public Remarks As String
    Public Key As String
    Public Guid As String

    Public Sub New()
        Guid = System.Guid.NewGuid.ToString
    End Sub
End Class
