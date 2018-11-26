
Namespace Trinity
    Public Class cContractTargets
        Implements Collections.IEnumerable

        'local variable to hold collection
        Private mCol As Collection
        Private mvarBookingtype As cContractBookingtype

        Public Sub Clear()
            mCol.Clear()
        End Sub

        Friend Property Bookingtype() As cContractBookingtype
            Get
                Return mvarBookingtype
            End Get
            Set(ByVal value As cContractBookingtype)
                'Dim TmpTarget As cPricelistTarget

                mvarBookingtype = value
                'For Each TmpTarget In mCol
                '    TmpTarget.Bookingtype = value
                'Next
            End Set
        End Property

        Public Function Add(ByVal TargetName As String) As cContractTarget
            'Optional ByVal CPP As Single = 0

            ', Optional ByVal Target As cTarget = Nothing, Optional ByVal UniSize As Long = 0, Optional ByVal UniSizeNat As Long = 0, Optional ByVal CalcCPP As Boolean = False


            If TargetName.IndexOf(" ") = 1 Then
                TargetName = TargetName.Remove(1, 1)
            End If

            'create a new object
            Dim objNewMember As cContractTarget
            On Error GoTo Add_Error

            objNewMember = New cContractTarget(Me)

            'objNewMember.CalcCPP = CalcCPP
            ''objNewMember.CPP = CPP
            'objNewMember.Target = Target
            objNewMember.TargetName = TargetName
            ''objNewMember.UniSize = UniSize
            ''objNewMember.UniSizeNat = UniSizeNat
            objNewMember.Bookingtype = mvarBookingtype
            'If Main Is Nothing Then
            '    objNewMember = objNewMember
            'End If

            'set the properties passed into the method
            'mCol.Add(objNewMember, TargetName)


            'return the object created
            Add = objNewMember
            objNewMember = Nothing


            On Error GoTo 0
            Exit Function

Add_Error:

            Err.Raise(Err.Number, "cPriceListTargets: Add", Err.Description)


        End Function

        Public Function Add(ByVal objNewMember As cContractTarget) As cContractTarget
            mCol.Add(objNewMember, objNewMember.TargetName)
            Return objNewMember
        End Function

        Default Public Property Item(ByVal Key As String) As cContractTarget
            Get
                On Error GoTo Item_Error

                'used when referencing an element in the collection
                'vntIndexKey contains either the Index or Key to the collection,
                'this is why it is declared as a Variant
                'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)

                If mCol.Contains(Key) Then
                    Return mCol(Key)
                Else
                    Return Nothing
                End If

                'If Item.CalcCPP Then
                '    Item.CalculateCPP()
                'End If

                On Error GoTo 0
                Exit Property

Item_Error:
                Item = Nothing
            End Get
            Set(ByVal value As cContractTarget)
                If Not mCol(Key) Is value Then
                    If mCol(Key).TargetName = value.TargetName Then
                        mCol.Add(value, "<temp>", value.TargetName)
                        mCol.Remove(value.TargetName)
                        mCol.Add(value, value.TargetName, "<temp>")
                        mCol.Remove("<temp>")
                    Else
                        Try
                            mCol.Add(value, value.TargetName, Key)
                        Catch ex As Exception
                            Throw New Exception("That target is already used.")
                        End Try
                    End If
                End If
            End Set
        End Property

        Default Public Property Item(ByVal Index As Integer) As cContractTarget
            Get
                On Error GoTo Item_Error

                'used when referencing an element in the collection
                'vntIndexKey contains either the Index or Key to the collection,
                'this is why it is declared as a Variant
                'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
                If Index <= mCol.Count Then
                    Return mCol(Index)
                Else
                    Return Nothing
                End If

                'If Item.CalcCPP Then
                '    Item.CalculateCPP()
                'End If

                On Error GoTo 0
                Exit Property

Item_Error:
                Item = Nothing
            End Get
            Set(ByVal value As cContractTarget)
                If Not mCol(Index) Is value Then
                    If mCol(Index).TargetName = value.TargetName Then
                        mCol.Add(value, "<temp>", value.TargetName)
                        mCol.Remove(value.TargetName)
                        mCol.Add(value, value.TargetName, "<temp>")
                        mCol.Remove("<temp>")
                    Else
                        Try
                            mCol.Add(value, value.TargetName, Index)
                            mCol.Remove(Index + 1)
                        Catch ex As Exception
                            Throw New Exception("That target is already used.")
                        End Try
                    End If
                End If
            End Set
        End Property

        Public ReadOnly Property Contains(ByVal vntIndexKey As Object) As Boolean
            Get
                Return mCol.Contains(vntIndexKey)
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

        Public Sub New(ByVal BT As cContractBookingtype)
            mCol = New Collection
            mvarBookingtype = BT
        End Sub

        Protected Overrides Sub Finalize()
            mCol = Nothing
            MyBase.Finalize()
        End Sub

    End Class
End Namespace
