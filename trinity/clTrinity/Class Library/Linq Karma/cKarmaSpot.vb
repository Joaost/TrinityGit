Namespace Trinity
    Public Class cKarmaSpot
        Public Enum KarmaSpotType
            Spot
            Sponsorship
        End Enum

        Public Week As Integer
        Public Channel As String
        Public AirDate As Date
        Public Mam As Integer
        Public HasBeenPicked As Boolean = False
        Public UseInCampaign As Boolean = False
        Public ListIndex As Long
        Public Type As KarmaSpotType
        Public ProgBefore As String
        Public ProgAfter As String
        Public Product As String
        Public SpotInBreak As Integer
        Public SpotCount As Integer
    End Class
End Namespace