Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic
Imports System.Xml

Namespace Trinity
    Public Class cCombinationChannels
        Implements IEnumerable

        Dim mCol As New Collection
        Dim Main As cKampanj
        Dim Combination As cCombination

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator
        End Function

        Public Function count()
            Return mCol.Count
        End Function

        Public Function Add(ByVal Bookingtype As cBookingType, ByVal Relation As Single) As cCombinationChannel
            Dim TmpCC As New cCombinationChannel(Main, Combination, mCol)
            TmpCC.Bookingtype = Bookingtype
            Bookingtype.Combination = Combination
            TmpCC.Relation = Relation
            mCol.Add(TmpCC, TmpCC.ID)
            Return TmpCC
        End Function

        Public Function Add(ByVal Bookingtype As cContractBookingtype, relation As Single) As cCombinationChannel
            Dim TmpCC As New cCombinationChannel(Main, Combination, mCol)
            TmpCC.ChannelName = Bookingtype.ParentChannel.ChannelName
            TmpCC.BookingTypeName = Bookingtype.Name
            TmpCC.Bookingtype = Nothing
            TmpCC.Relation = relation
            mCol.Add(TmpCC, TmpCC.ID)
            Return TmpCC
        End Function

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cCombinationChannel
            Get
                Return mCol(vntIndexKey)
            End Get
        End Property

        Public Sub Remove(ByVal ID As String)
            mCol(ID).Bookingtype.Combination = Nothing
            mCol.Remove(ID)
        End Sub

        Public Sub Remove(ByVal Index As Integer)
            mCol(Index).Bookingtype.Combination = Nothing
            mCol.Remove(Index)
        End Sub

        Public Sub remove(ByVal CombinationChannel As cCombinationChannel)
            CombinationChannel.Bookingtype.Combination = Nothing
            mCol.Remove(CombinationChannel.ID)
        End Sub

        Public Sub New(ByVal MainObject As cKampanj, ByVal ParentCombination As cCombination)
            Main = MainObject
            Combination = ParentCombination
        End Sub
    End Class
End Namespace
