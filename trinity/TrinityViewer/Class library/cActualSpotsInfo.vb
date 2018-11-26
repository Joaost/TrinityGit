Namespace TrinityViewer

    Public Class cActualSpotsInfo
        Implements System.Collections.IEnumerable

        Public TotalTRP As Decimal
        Private mCol As New Collection
        Private mvarMainObject As cCampaignInfo

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator
        End Function

        Public Function Add(ByVal AirDate As Date, ByVal MaM As Integer, ByVal Second As Byte, Optional ByVal Filmcode As String = "", Optional ByVal Channel As cChannelInfo = Nothing, Optional ByVal SpotLength As Byte = 30, Optional ByVal Product As String = "", Optional ByVal Index As Integer = 100, Optional ByVal PosInBreak As Byte = 0, Optional ByVal SpotsInBreak As Byte = 0, Optional ByVal BreakType As cActualSpotInfo.EnumBreakType = cActualSpotInfo.EnumBreakType.btBreak, Optional ByVal AdedgeChannel As String = "") As cActualSpotInfo
            Dim objNewMember As New cActualSpotInfo(mvarMainObject)
            objNewMember.AirDate = AirDate.ToOADate
            objNewMember.MaM = MaM
            objNewMember.Second = Second
            objNewMember.Filmcode = Filmcode
            objNewMember.PosInBreak = PosInBreak
            objNewMember.SpotsInBreak = SpotsInBreak
            objNewMember.SpotLength = SpotLength
            mCol.Add(objNewMember, objNewMember.ID)
            Return objNewMember
        End Function

        Public Sub New(ByVal Main As cCampaignInfo)
            mvarMainObject = Main
        End Sub

        Public Function Count() As Integer
            Return mCol.Count
        End Function

        Default Public ReadOnly Property Item(ByVal idx As Object) As cActualSpotInfo
            Get
                Return mCol(idx)
            End Get
        End Property
    End Class

End Namespace