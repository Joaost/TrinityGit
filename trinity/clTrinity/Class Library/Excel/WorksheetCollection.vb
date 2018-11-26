Namespace CultureSafeExcel
    Public Class WorksheetCollection
        Implements ICollection(Of Worksheet)

        Private _coll As New Dictionary(Of String, Worksheet)

        Public Sub Add(item As Worksheet) Implements System.Collections.Generic.ICollection(Of Worksheet).Add
            Me.Add(item, item.Name)
        End Sub

        Public Sub Add(item As Worksheet, Key As String)
            _coll.Add(Key, item)
        End Sub

        Public Sub Clear() Implements System.Collections.Generic.ICollection(Of Worksheet).Clear
            _coll.Clear()
        End Sub

        Public Function Contains(item As Worksheet) As Boolean Implements System.Collections.Generic.ICollection(Of Worksheet).Contains
            Return _coll.ContainsValue(item)
        End Function

        Public Sub CopyTo(array() As Worksheet, arrayIndex As Integer) Implements System.Collections.Generic.ICollection(Of Worksheet).CopyTo

        End Sub

        Public ReadOnly Property Count As Integer Implements System.Collections.Generic.ICollection(Of Worksheet).Count
            Get
                Return _coll.Count
            End Get
        End Property

        Public ReadOnly Property IsReadOnly As Boolean Implements System.Collections.Generic.ICollection(Of Worksheet).IsReadOnly
            Get
                Return True
            End Get
        End Property

        Default Public ReadOnly Property Item(Index As Integer) As Worksheet
            Get
                Return _coll.Values(Index - 1)
            End Get
        End Property

        Default Public ReadOnly Property Item(Key As String) As Worksheet
            Get
                Return _coll(Key)
            End Get
        End Property

        Public Function Remove(item As Worksheet) As Boolean Implements System.Collections.Generic.ICollection(Of Worksheet).Remove
            _coll.Remove(item.Name)
        End Function

        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of Worksheet) Implements System.Collections.Generic.IEnumerable(Of Worksheet).GetEnumerator
            Return _coll.Values.GetEnumerator
        End Function

        Public Function GetEnumerator1() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return _coll.GetEnumerator
        End Function
    End Class
End Namespace