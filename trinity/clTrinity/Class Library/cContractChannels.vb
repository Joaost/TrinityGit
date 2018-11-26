
Namespace Trinity
    Public Class cContractChannels
        'the cChannels is a collection of cChannel objects. It is fairly simple as it is only used as a container.
        Implements Collections.IEnumerable

        'local variable to hold Collection
        Private mCol As Collection
        Private Main As cKampanj

        Friend WriteOnly Property MainObject() As cKampanj
            Set(ByVal value As cKampanj)
                Dim TmpChannel As cChannel

                Main = value
                For Each TmpChannel In mCol
                    TmpChannel.MainObject = value
                Next
            End Set
        End Property

        Public Function Add(ByVal Name As String, ByVal filename As String, Optional ByVal Area As String = "") As cContractChannel
            'creates a new object
            Dim objNewMember As cContractChannel
            Dim TmpChannel As cContractChannel
            Dim i As Integer

            On Error GoTo Add_Error

            objNewMember = New cContractChannel(Main)

            'Make sure to remove all extra spaces
            Name = Trim(Name)
            Area = Trim(Area)

            'set the properties passed into the method
            objNewMember.ChannelName = Name
            objNewMember.ParentCollection = mCol

            'Find channel before or after
            TmpChannel = Nothing
            If mCol.Count > 0 And objNewMember.ListNumber > 0 Then
                i = 1
                TmpChannel = mCol(1)
                While TmpChannel.ListNumber < objNewMember.ListNumber And i <= mCol.Count
                    TmpChannel = mCol(i)
                    i = i + 1
                End While
                If i >= mCol.Count And TmpChannel.ListNumber < objNewMember.ListNumber Then
                    TmpChannel = Nothing
                End If
            End If

            On Error Resume Next

            If TmpChannel Is Nothing OrElse TmpChannel.ChannelName = "" Then
                mCol.Add(objNewMember, Name)
            ElseIf TmpChannel.ListNumber > objNewMember.ListNumber Then
                mCol.Add(objNewMember, Name, TmpChannel.ChannelName)
            Else
                mCol.Add(objNewMember, Name, , TmpChannel.ChannelName)
            End If

            On Error GoTo Add_Error

            'return the object created
            Add = mCol(Name)
            objNewMember = Nothing


            On Error GoTo 0
            Exit Function

Add_Error:

            Err.Raise(Err.Number, "cChannels: Add", Err.Description)


        End Function

        Default Public ReadOnly Property Item(ByVal Key As String) As cContractChannel
            'used when referencing an element in the Collection
            'vntIndexKey contains either the Index or Key to the Collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Get
                Try
                    If mCol.Contains(Key) Then
                        Return mCol(Key)
                    Else
                        Return Nothing
                    End If
                Catch
                    Return Nothing
                End Try
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal Index As Integer) As cContractChannel
            'used when referencing an element in the Collection
            'vntIndexKey contains either the Index or Key to the Collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Get
                Try
                    If Index <= mCol.Count Then
                        Return mCol(Index)
                    Else
                        Return Nothing
                    End If
                Catch
                    Return Nothing
                End Try
            End Get
        End Property

        Public ReadOnly Property Count() As Long
            'used when retrieving the number of elements in the
            'Collection. Syntax: Debug.Print x.Count
            Get
                Count = mCol.Count
            End Get
        End Property

        Public Sub Remove(ByVal vntIndexKey As Object)
            'used when removing an element from the Collection
            'vntIndexKey contains either the Index or Key, which is why
            'it is declared as a Variant
            'Syntax: x.Remove(xyz)
            mCol.Remove(vntIndexKey)
        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            GetEnumerator = mCol.GetEnumerator
        End Function

        Public Sub New(ByVal MainObject As cKampanj)
            mCol = New Collection
            Main = MainObject
        End Sub

        Protected Overrides Sub Finalize()
            mCol = Nothing
            MyBase.Finalize()
        End Sub
    End Class
End Namespace
