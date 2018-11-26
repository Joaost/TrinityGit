Namespace Trinity
    Class _cKarmaCampaigns
        Implements IEnumerable

        'local variable to hold collection
        Private mCol As Collection
        Public Karma As _cKarma

        Public Function Add(ByVal Name As String, ByVal TrinityCampaign As cKampanj) As _cKarmaCampaign
            'create a new object
            Dim objNewMember As _cKarmaCampaign

            On Error GoTo Add_Error

            objNewMember = New _cKarmaCampaign

            objNewMember.Name = Name
            objNewMember.TrinityCampaign = TrinityCampaign

            On Error Resume Next
            If Not mCol.Contains(Name) Then
                mCol.Add(objNewMember, Name)
            End If
            On Error GoTo Add_Error

            'return the object created
            Add = mCol(Name)
            objNewMember = Nothing


            On Error GoTo 0
            Exit Function

Add_Error:

            Err.Raise(Err.Number, "cKarmaCampaigns: Add", Err.Description)

        End Function

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As _cKarmaCampaign
            'used when referencing an element in the collection
            'vntIndexKey contains either the Index or Key to the collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Get
                On Error GoTo ErrHandle
                mCol(vntIndexKey).Parent = Me
                Item = mCol(vntIndexKey)
                Exit Property

ErrHandle:
                Err.Raise(Err.Number, "cKarmaCampaigns", "Unknown Campaign: " & vntIndexKey)
            End Get
        End Property

        Public ReadOnly Property Count() As Long
            'used when retrieving the number of elements in the
            'collection. Syntax: Debug.Print x.Count
            Get
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
