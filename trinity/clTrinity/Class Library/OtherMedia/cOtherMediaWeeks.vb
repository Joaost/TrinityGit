Namespace Trinity
    Public Class cOtherMediaWeeks
        Implements IEnumerable

        Private _col As New Collection

        Public Function Add(Name As String, StartDate As Date, EndDate As Date) As cOtherMediaWeek
            Dim _week As New cOtherMediaWeek With {.Name = Name, .StartDate = StartDate, .EndDate = EndDate}

            _col.Add(_week, Name)

            Return _week
        End Function

        Public Sub Remove(Key As String)
            _col.Remove(Key)
        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return _col.GetEnumerator
        End Function

        Public ReadOnly Property Count As Integer
            Get
                Return _col.Count
            End Get
        End Property

    End Class
End Namespace
