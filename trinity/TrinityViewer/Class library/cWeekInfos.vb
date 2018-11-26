Namespace TrinityViewer
    Public Class cWeekInfos
        Implements IEnumerable

        Private _col As New Collection

        Public Function Add(ByVal Name As String) As cWeekInfo
            Dim TmpWeek As New cWeekInfo(Me)

            TmpWeek.name = Name

            _col.Add(TmpWeek, Name)

            Return _col(Name)

        End Function

        Public Function Count() As Integer
            Return _col.Count
        End Function

        Default Public ReadOnly Property Item(ByVal idx As Object) As cWeekInfo
            Get
                Return _col(idx)
            End Get
        End Property

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return _col.GetEnumerator
        End Function
    End Class
End Namespace