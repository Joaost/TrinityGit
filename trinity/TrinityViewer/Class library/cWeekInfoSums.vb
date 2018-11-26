Namespace TrinityViewer
    Public Class cWeekInfoSums
        Implements ICollection

        Private _main As cCampaignInfo
        Default Public ReadOnly Property Item(ByVal idx As Object) As cWeekInfo
            Get
                Dim TmpWeek As New cWeekInfo(Me)

                TmpWeek.Name = _main.Channels(1).BookingTypes(1).Weeks(idx).Name

                Return TmpWeek
            End Get
        End Property

        Public Sub CopyTo(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo

        End Sub

        Public ReadOnly Property Count() As Integer Implements System.Collections.ICollection.Count
            Get
                Return _main.Channels(1).BookingTypes(1).Weeks.Count
            End Get
        End Property

        Public ReadOnly Property IsSynchronized() As Boolean Implements System.Collections.ICollection.IsSynchronized
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property SyncRoot() As Object Implements System.Collections.ICollection.SyncRoot
            Get
                Return Nothing
            End Get
        End Property

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return _main.Channels(1).BookingTypes(1).Weeks.GetEnumerator
        End Function

        Public Sub New(ByVal Main As cCampaignInfo)
            _main = Main
        End Sub

        Public ReadOnly Property MainObject() As cCampaignInfo
            Get
                Return _main
            End Get
        End Property
    End Class
End Namespace