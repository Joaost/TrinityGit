Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cWrapper
        Implements Collections.IEnumerable

        Dim TmpHive As New Hashtable
        Dim CurrentIndex As Integer
        'Dim Keys() As Variant
        Public Function Add(ByRef Item As Object, ByRef Key As Object) As Object
            TmpHive.Add(Key, Item)
            Add = TmpHive(Key)
            '    ReDim Preserve Keys(TmpTreap.Count)
            '
            '    Keys(TmpTreap.Count) = Key
        End Function

        Public Sub RemoveAll()
            TmpHive.Clear()
        End Sub

        Default Public ReadOnly Property Item(ByVal Index As Integer) As Object
            Get
                Item = TmpHive(TmpHive.Keys(Index - 1))
            End Get
        End Property


        Default Public ReadOnly Property Item(ByVal Key As String) As Object
            Get
                Item = TmpHive(Key)
            End Get
        End Property

        Public Function Exists(ByVal vntIndexKey As Object) As Boolean
            Exists = TmpHive.ContainsKey(vntIndexKey)
        End Function


        Public Sub Remove(ByVal vntIndexKey As Object)
            TmpHive.Remove(vntIndexKey)
        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Dim kv As Object
            kv = TmpHive.GetEnumerator
            Return kv
        End Function

        Public Function GetHive()
            Return TmpHive.Values
        End Function

        Public ReadOnly Property Count() As Integer
            Get
                Count = TmpHive.Count
            End Get
        End Property
    End Class
End Namespace