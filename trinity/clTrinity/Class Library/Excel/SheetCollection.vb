Namespace CultureSafeExcel
    Public Class SheetCollection
        Implements ICollection(Of Worksheet)

        Private _coll As New Collection

        Public Sub Add(item As Worksheet, Key As String) Implements System.Collections.Generic.ICollection(Of Worksheet).Add
            _coll.Add()
        End Sub

        Public Sub Clear() Implements System.Collections.Generic.ICollection(Of Worksheet).Clear
            _coll.Clear()
        End Sub

        Public Function Contains(item As Worksheet) As Boolean Implements System.Collections.Generic.ICollection(Of Worksheet).Contains

        End Function

        Public Sub CopyTo(array() As Worksheet, arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of Worksheet).CopyTo

        End Sub

        Public ReadOnly Property Count As Integer Implements System.Collections.Generic.ICollection(Of Worksheet).Count
            Get

            End Get
        End Property

        Public ReadOnly Property IsReadOnly As Boolean Implements System.Collections.Generic.ICollection(Of Worksheet).IsReadOnly
            Get

            End Get
        End Property

        Public Function Remove(item As Worksheet) As Boolean Implements System.Collections.Generic.ICollection(Of Worksheet).Remove

        End Function

        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of Worksheet) Implements System.Collections.Generic.IEnumerable(Of Worksheet).GetEnumerator

        End Function

        Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator

        End Function
    End Class
End Namespace