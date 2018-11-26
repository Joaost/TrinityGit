Namespace TrinityViewer
    Public Class cBookingTypeInfos
        Implements ICollection

        Private _col As New Collection
        Private Main As cCampaignInfo

        Public Function Add(ByVal Name As String) As cBookingTypeInfo

            Dim TmpBT As New cBookingTypeInfo(Main)

            TmpBT.Name = Name

            _col.Add(TmpBT, Name)

            Return _col(Name)

        End Function

        Default Public ReadOnly Property Item(ByVal idx As Object) As cBookingTypeInfo
            Get
                Return _col(idx)
            End Get
        End Property

        Public Sub CopyTo(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo

        End Sub

        Public ReadOnly Property Count() As Integer Implements System.Collections.ICollection.Count
            Get
                Return _col.Count
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
            Return _col.GetEnumerator
        End Function

        Public Sub New(ByVal MainObject As cCampaignInfo)
            Main = MainObject
        End Sub

    End Class
End Namespace