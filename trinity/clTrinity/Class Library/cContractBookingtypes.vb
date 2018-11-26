
Namespace Trinity
    Public Class cContractBookingtypes
        Implements Collections.IEnumerable

        'local variable to hold collection
        Private mCol As Collection
        Private mvarParentChannel As cContractChannel
        Private MainObject As cKampanj

        Friend Property ParentChannel() As cContractChannel
            Get
                ParentChannel = mvarParentChannel
            End Get
            Set(ByVal value As cContractChannel)
                mvarParentChannel = value
                Dim TmpBookingType As cContractBookingtype

                For Each TmpBookingType In mCol
                    If TmpBookingType.ParentChannel Is Nothing Then
                        TmpBookingType.ParentChannel = value
                    End If
                Next
            End Set
        End Property

        Public Overloads Function Add(ByVal Name As String) As cContractBookingtype

            'create a new object
            Dim objNewMember As cContractBookingtype

            objNewMember = New cContractBookingtype(MainObject, mvarParentChannel)

            'set the properties passed into the method
            objNewMember.ParentCollection = mCol
            objNewMember.ParentChannel = mvarParentChannel
            objNewMember.Name = Name

            mCol.Add(objNewMember, Name)

            'return the object created
            Add = objNewMember
            objNewMember = Nothing

        End Function

        Public Function Contains(ByVal vntIndexKey As Object) As Boolean
            Return mCol.Contains(vntIndexKey)
        End Function

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cContractBookingtype

            'used when referencing an element in the collection
            'vntIndexKey contains either the Index or Key to the collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Get
                If vntIndexKey.GetType.Name = "String" AndAlso Not mCol.Contains(vntIndexKey) Then
                    Return Nothing
                End If
                Item = mCol(vntIndexKey)
            End Get
        End Property

        Public Overloads ReadOnly Property Count() As Long
            Get
                'used when retrieving the number of elements in the
                'collection. Syntax: Debug.Print x.Count
                Count = mCol.Count
            End Get
        End Property

        Public Overloads Sub Remove(ByVal vntIndexKey As Object)
            'used when removing an element from the collection
            'vntIndexKey contains either the Index or Key, which is why
            'it is declared as a Variant
            'Syntax: x.Remove(xyz)

            mCol.Remove(vntIndexKey)

        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            GetEnumerator = mCol.GetEnumerator()
        End Function

        Public Sub New(ByVal Main As cKampanj, ByVal parent As cContractChannel)
            mCol = New Collection
            ParentChannel = parent
            MainObject = Main
        End Sub

        Protected Overrides Sub Finalize()
            mCol = Nothing
            MyBase.Finalize()
        End Sub
    End Class
End Namespace
