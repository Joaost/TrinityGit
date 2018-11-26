Namespace TrinityViewer
    Public Class cFilmsInfo
        Implements IEnumerable

        Private mCol As New Collection
        Private _week As cWeekInfo

        Public Function Add(ByVal Name As String) As cFilmInfo
            Dim _newFilm As New cFilmInfo(mCol, _week)

            _newFilm.Name = Name

            mCol.Add(_newFilm, Name)

            Return _newFilm
        End Function

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator
        End Function

        Public Function Count() As Integer
            Return mCol.Count
        End Function

        Default Public ReadOnly Property Item(ByVal idx As Object) As cFilmInfo
            Get
                Try
                    Return mCol(idx)
                Catch
                    Return Nothing
                End Try
            End Get
        End Property

        Public Sub New(ByVal week As cWeekInfo)
            _week = week
        End Sub
    End Class
End Namespace
