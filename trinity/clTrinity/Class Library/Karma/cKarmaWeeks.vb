Namespace Trinity
    Class cKarmaWeeks
        Implements Collections.IEnumerable

        'local variable to hold collection
        Private mCol As Collection

        Public Function Add() As cKarmaWeek
            'create a new object
            Dim objNewMember As cKarmaWeek

            On Error GoTo Add_Error

            objNewMember = New cKarmaWeek

            'Make sure to remove all extra spaces


            'set the properties passed into the method

            'Find channel before or after

            mCol.Add(objNewMember)

            On Error GoTo Add_Error

            'return the object created

            objNewMember = Nothing

            Add = mCol(mCol.Count)

            On Error GoTo 0
            Exit Function

Add_Error:

            Err.Raise(Err.Number, "cKarmaWeeks: Add", Err.Description)


        End Function

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cKarmaWeek
            Get
                On Error GoTo ErrHandle
                Item = mCol(vntIndexKey)
                Exit Property

ErrHandle:
                Err.Raise(Err.Number, "cKarmaWeeks", "Unknown week: " & vntIndexKey)
            End Get
        End Property

        Public ReadOnly Property Count() As Long
            Get
                'used when retrieving the number of elements in the
                'collection. Syntax: Debug.Print x.Count
                Count = mCol.Count
            End Get
        End Property

        Public Sub Remove(ByVal vntIndexKey As Object)
            'used when removing an element from the collection
            'vntIndexKey contains either the Index or Key, which is why
            'it is declared as a Variant
            'Syntax: x.Remove(xyz)


            mCol.Remove(vntIndexKey)
        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            GetEnumerator = mCol.GetEnumerator
        End Function

        Public Sub New()
            mCol = New Collection
        End Sub

        Protected Overrides Sub Finalize()
            mCol = Nothing
            MyBase.Finalize()
        End Sub
    End Class
End Namespace
