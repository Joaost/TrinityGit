Namespace Trinity
    Public Class cOtherMedias
        Implements IEnumerable

        Dim _col As New Collection

        Public Function Add(Optional Name As String = "") As cOtherMedia

            Dim _media As New cOtherMedia With {.Name = Name}

            _col.Add(_media, _media.Name)

            Return _media
        End Function

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return _col.GetEnumerator
        End Function

    End Class
End Namespace
