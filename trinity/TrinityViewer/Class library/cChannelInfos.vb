Namespace TrinityViewer
    Public Class cChannelInfos
        Implements IEnumerable

        Private _col As New Collection

        Private Main As cCampaignInfo

        Public Function Add(ByVal ChannelName As String) As cChannelInfo

            Dim TmpChan As New cChannelInfo(Main)

            TmpChan.ChannelName = ChannelName

            _col.Add(TmpChan, ChannelName)

            Return TmpChan

        End Function

        Default Public ReadOnly Property Item(ByVal idx As Object) As cChannelInfo
            Get
                If idx.GetType.Name = "String" AndAlso Not _col.Contains(idx) Then
                    Return Nothing
                End If
                Return _col(idx)
            End Get
        End Property

        Public Sub Remove(ByVal ChannelName As String)
            _col.Remove(ChannelName)
        End Sub

        Public ReadOnly Property Count() As Integer
            Get
                Return _col.Count
            End Get
        End Property

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return _col.GetEnumerator
        End Function

        Public Sub New(ByVal MainObject As cCampaignInfo)
            Main = MainObject
        End Sub
    End Class
End Namespace