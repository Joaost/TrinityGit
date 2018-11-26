Namespace Trinity
    Public Class cKarmaCampaigns
        Implements IEnumerable

        'local variable to hold collection
        Private mCol As Collection
        Private _karma As cKarma

        Public Function Add(ByVal Name As String, ByVal TrinityCampaign As Trinity.cKampanj) As cKarmaCampaign
            'create a new object

            'If Name Is Nothing Or Name = "" Then
            '    Windows.Forms.MessageBox.Show("This campaign has no name. Give it a name so Trinity can create a new Karma campaign and estimate it.")
            '    Return Nothing
            'End If

            Dim objNewMember As cKarmaCampaign

            On Error GoTo Add_Error

            objNewMember = New cKarmaCampaign(Me)

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
            Resume

        End Function

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cKarmaCampaign
            'used when referencing an element in the collection
            'vntIndexKey contains either the Index or Key to the collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Get
                On Error GoTo ErrHandle
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

        Public Sub New(ByVal Karma As cKarma)
            _karma = Karma
            mCol = New Collection
        End Sub

        Friend ReadOnly Property Karma() As cKarma
            Get
                Return _karma
            End Get
        End Property

        Protected Overrides Sub Finalize()
            mCol = Nothing
            MyBase.Finalize()
        End Sub
    End Class
End Namespace