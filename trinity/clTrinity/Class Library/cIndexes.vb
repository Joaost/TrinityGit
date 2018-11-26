Namespace Trinity
    Public Class cIndexes
        Implements Collections.IEnumerable
        Implements IDetectsProblems

        'Indexes holds a collection of index, which is a index of the booked CPP/TRP
        Private mCol As New Collection
        Private Main As cKampanj
        Private Parent As Object

        Public WriteOnly Property MainObject() As cKampanj
            Set(ByVal value As cKampanj)
                Main = value
            End Set
        End Property

        Public ReadOnly Property HasName(ByVal name As String) As Boolean
            Get
                For Each idx As cIndex In mCol
                    If idx.Name = name Then
                        Return True
                    End If
                Next
                Return False
            End Get
        End Property

        Public ReadOnly Property IndexForName(ByVal name As String) As String
            Get
                For Each idx As cIndex In mCol
                    If idx.Name = name Then Return idx.ID
                Next
                Return Nothing
            End Get
        End Property

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided
            'it will return True of succeded and false if failed

            On Error GoTo On_Error

            Dim xmlIndex As Xml.XmlElement
            For Each TmpIndex As Trinity.cIndex In Me
                xmlIndex = xmlDoc.CreateElement("Index")
                TmpIndex.GetXML(xmlIndex, errorMessege, xmlDoc)
                colXml.AppendChild(xmlIndex)
            Next TmpIndex

            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving Indexes for " & Parent.ToString)
            Return False
        End Function

        Public Function Add(ByVal Name As String, Optional ByVal ID As String = "") As cIndex

            Dim TmpIndex As cIndex
            If Parent.GetType.ToString.EndsWith("Trinity.cBookingType") Then
                TmpIndex = New cIndex(Main, DirectCast(Parent, Trinity.cBookingType), mCol)
            ElseIf Parent.GetType.ToString.EndsWith("Trinity.cContractBookingtype") Then
                TmpIndex = New cIndex(Main, mCol)
            ElseIf Parent.GetType.ToString.EndsWith("Trinity.cContractTarget") Then
                TmpIndex = New cIndex(Main, mCol)
            Else
                TmpIndex = New cIndex(Main, DirectCast(Parent, Trinity.cPricelistTarget), mCol)
            End If

            'sets ID if there is no one set
            If ID = "" Then ID = CreateGUID()
            TmpIndex.ID = ID
            TmpIndex.Name = Name
            If Main.Channels.Count > 0 AndAlso Main.Channels(1).BookingTypes(1).Weeks.Count > 0 Then
                TmpIndex.FromDate = Date.FromOADate(Main.StartDate)
                TmpIndex.ToDate = Date.FromOADate(Main.EndDate)
            End If
            On Error GoTo 0
            'mCol.Add(TmpIndex, ID)  ' *** NOT NEEDED SINCE IT IS ADDED WHEN ID IS SET
            Add = mCol(ID)
        End Function

        '        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cIndex
        '            Get
        '                Dim e As Long

        '                '    used when referencing an element in the collection
        '                '    vntIndexKey contains either the Index or Key to the collection,
        '                '    this is why it is declared as a Variant
        '                '    Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
        '                On Error GoTo Item_Error

        '                If mCol.Contains(vntIndexKey) Then
        '                    Item = mCol(vntIndexKey)
        '                Else
        '                    Item = Nothing
        '                End If


        '                On Error GoTo 0
        '                Exit Property

        'Item_Error:
        '                e = Err.Number

        '                Err.Raise(e, "cCosts", "Unknown Index (" & vntIndexKey & ")")
        '                Resume

        '            End Get
        '        End Property

        Default Public ReadOnly Property Item(ByVal Key As String) As cIndex
            'used when referencing an element in the Collection
            'vntIndexKey contains either the Index or Key to the Collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Get
                If mCol Is Nothing Then Return Nothing
                If mCol.Contains(Key) Then
                    Return mCol(Key)
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal Index As Integer) As cIndex
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

        Public Function Exists(ByVal Index As String) As Boolean
            Return mCol.Contains(Index)
        End Function

        Public ReadOnly Property Count() As Long
            Get
                'used when retrieving the number of elements in the
                'collection. Syntax: Debug.Print x.Count
                Count = mCol.Count
            End Get
        End Property

        Public Sub Remove(ByVal vntIndexKey As String)
            'used when removing an element from the collection
            'vntIndexKey contains either the Index or Key, which is why
            'it is declared as a Variant
            'Syntax: x.Remove(xyz)

            If vntIndexKey IsNot Nothing AndAlso mCol.Contains(vntIndexKey) Then
                mCol.Remove(vntIndexKey)
            End If
            'mCol.Remove(vntIndexKey)
        End Sub

        Public Function GetIndexForDate(ByVal WhatDate As Date, ByVal IndexType As cIndex.IndexOnEnum, Optional ByVal Daypart As Integer = -1) As Decimal
            Dim TmpIndex As cIndex
            Dim Idx As Decimal
            Dim EnhIdx As Decimal = 0

            'creates an index for a specific date 
            Idx = 100
            For Each TmpIndex In mCol
                If TmpIndex.UseThis Then
                    If TmpIndex.IndexOn = IndexType Then
                        If TmpIndex.FromDate <= WhatDate Then
                            If TmpIndex.ToDate >= WhatDate Then
                                If TmpIndex.Enhancements.Count = 0 Then
                                    Idx *= (TmpIndex.Index(Daypart) / 100)
                                Else
                                    EnhIdx += TmpIndex.Index(Daypart) - 100
                                End If
                            End If
                        End If
                    End If
                End If
            Next
            Dim SpecFactor As Single
            If Parent.GetType Is GetType(Trinity.cBookingType) Then
                SpecFactor = Parent.EnhancementFactor
            Else
                SpecFactor = Parent.Bookingtype.EnhancementFactor
            End If
            EnhIdx = EnhIdx / (1 + EnhIdx) / SpecFactor
            EnhIdx = (1 / (1 - EnhIdx))

            Return Idx * EnhIdx
        End Function

        Public Sub Clear()
            mCol = New Collection
        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            GetEnumerator = mCol.GetEnumerator
        End Function

        'returns the main campaign
        Public Sub New(ByVal MainObj As cKampanj, ByVal ParentObj As Trinity.cBookingType)
            Main = MainObj
            Parent = ParentObj
            mCol = New Collection
        End Sub

        Public Sub New(ByVal MainObj As cKampanj, ByVal ParentObj As Trinity.cContractBookingtype)
            Main = MainObj
            Parent = ParentObj
            mCol = New Collection
        End Sub

        Public Sub New(ByVal MainObj As cKampanj, ByVal ParentObj As Trinity.cPricelistTarget)
            Main = MainObj
            Parent = ParentObj
            mCol = New Collection
        End Sub

        Public Sub New(ByVal MainObj As cKampanj, ByVal ParentObj As Trinity.cContractTarget)
            Main = MainObj
            Parent = ParentObj
            mCol = New Collection
        End Sub

        Protected Overrides Sub Finalize()
            mCol = Nothing
            MyBase.Finalize()
        End Sub




        Public Enum ProblemsEnum
            ValueOne = 1
            ValueTwo = 2
        End Enum

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems

            Dim _problems As New List(Of cProblem)


            If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
            Return _problems
        End Function

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound

    End Class
End Namespace