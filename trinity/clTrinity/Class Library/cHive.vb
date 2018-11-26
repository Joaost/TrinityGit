Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cHive
        Implements IEnumerator

        Private Const csClassName As String = "CHive"

        ' Default behaviours of Hive
        Private Const DefaultInitialAlloc As Long = 100
        Private Const DefaultGrowthFactor As Double = 1.5

        Private GrowthFactor As Double
        Private InitialAlloc As Long

        ' Sentinel is Node(0)
        Private Const Sentinel As Long = 0

        ' Node Color
        Private Enum EColor
            Black
            Red
        End Enum

        ' fields associated with each node
        Private Structure ItemData
            Public lLeft As Long          ' Left child
            Public lRight As Long         ' Right child
            Public lParent As Long        ' Parent
            Public Color As EColor        ' red or black
            Public vKey As Object        ' item key
            Public vData As Object       ' item data
        End Structure

        Public Enum HiveErrors
            InvalidIndex = &H1
            KeyNotFound = &H2
            KeyCannotBeInteger = &H4
            DuplicateKey = &H8
            InvalidParameter = &H100
            KeyCannotBeBlankOrZero = &H1000
        End Enum
        Private Items() As ItemData     ' Array which stored all item

        ' support for FindFirst and FindNext
        Private StackIndex As Integer
        Private Stack(0 To 32) As Long
        Private NextNode As Long

        Private Root As Long            ' root of binary tree
        Private Node As cNode           ' class for allocating nodes

        Private lCount As Long          ' No of items
        Private lIndex() As Long        ' Used to map items to index

        Private mCompareMode As Microsoft.VisualBasic.CompareMethod

        Public Errors As cErrorCollection   ' Contains all the errors

        Public RaiseError As Boolean    ' True: Call Err.Raise; False: Don't
        Public AllowDuplicate As Boolean ' True: Allow duplicate; False: Don't

        ' GUID Key generation code
        Private Declare Function CoCreateGuid Lib _
            "OLE32.DLL" (ByVal pGuid As Guid) As Long
        Private Declare Function StringFromGUID2 Lib _
            "OLE32.DLL" (ByVal pGuid As Guid, _
            ByVal PointerToString As Long, _
            ByVal MaxLength As Long) As Long

        ' GUID Result
        Private Const GUID_OK As Long = 0

        Private EnumPointer As Integer = 0

        ' Structure to hold GUID
        'Private Structure GUID
        '    Public Guid1 As Long             ' 32 bit
        '    Public Guid2 As Integer          ' 16 bit
        '    Public Guid3 As Integer          ' 16 bit
        '    Public Guid4(0 To 7) As Byte             ' 64 bit
        'End Structure

        ' For Raw memory copy
        Private Declare Sub MoveMemory Lib "kernel32" _
              Alias "RtlMoveMemory" (ByVal dest As Long, _
        ByVal Source As Long, ByVal numBytes As Long)

        Public Function CreateGUIDKey() As String
            Return Guid.NewGuid.ToString
        End Function

        'Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        '    Dim TmpItem As ItemData
        '    Dim TmpEnum As IEnumerator
        '    TmpItem = Items.GetEnumerator.Current
        'End Function

        Private Sub Raise(ByVal errno As HiveErrors, ByVal sLocation As String)
            '   inputs:
            '       errno       Error ID
            '       sLocation   Location of the error

            Dim sErrMsg As String = ""

            Select Case errno

                Case HiveErrors.InvalidIndex
                    sErrMsg = "Invalid Index."
                Case HiveErrors.KeyNotFound
                    sErrMsg = "Key not Found in the Hive."
                Case HiveErrors.KeyCannotBeInteger
                    sErrMsg = "Key cannot be Integer or Long or Byte or any kind of Fixed Digit."
                Case HiveErrors.DuplicateKey
                    sErrMsg = "Duplicate Key."
                Case HiveErrors.InvalidParameter
                    sErrMsg = "Invalid Parameter."

            End Select

            If RaiseError Then
                Err.Raise(vbObjectError + 5000 + errno, csClassName + "." + sLocation, sErrMsg)
            End If
            If Errors Is Nothing Then
                Errors = New cErrorCollection
            End If
            Errors.Add(vbObjectError + 5000 + errno, sLocation, sErrMsg)

        End Sub

        Private Function GetKeyIndex(ByVal vKey As Object) As Long
            '   inputs:
            '       vKeysVal          vKeys of the node
            '   returns:
            '       the index of the item in the Array

            Select Case VarType(vKey)
                Case vbByte, vbInteger, vbLong
                    If vKey < 0 Or vKey > lCount Then
                        Raise(HiveErrors.InvalidIndex, "GetIndex")
                    Else
                        GetKeyIndex = lIndex(vKey)
                    End If
                Case Else
                    GetKeyIndex = FindNode(vKey)
                    If GetKeyIndex = 0 Then Raise(HiveErrors.KeyNotFound, "FindNode")
            End Select
        End Function

        Private Function GetIndex(ByVal KeyIndex As Long) As Long
            Dim i As Long

            For i = 1 To lCount
                If lIndex(i) = KeyIndex Then
                    GetIndex = i
                    Exit Function
                End If
            Next
        End Function
        Private Function FindNode(ByVal KeyVal As Object) As Long
            '   inputs:
            '       Key                   ' designates key to find
            '   returns:
            '       index to node
            '   action:
            '       Search tree for designated key, and return index to node.
            '   errors:
            '       Key Not Found
            '
            Dim current As Long

            ' find node specified by key
            current = Root

            ' ------------------------------------
            ' if compare mode is binary
            ' then match exact key otherwise
            ' ignore case if key is a string
            ' ------------------------------------
            If mCompareMode <> vbBinaryCompare And VarType(KeyVal) = vbString Then
                KeyVal = LCase(KeyVal)
                Do While current <> Sentinel
                    If LCase(Items(current).vKey) = KeyVal Then
                        FindNode = current
                        Exit Function
                    Else
                        If KeyVal < LCase(Items(current).vKey) Then
                            current = Items(current).lLeft
                        Else
                            current = Items(current).lRight
                        End If
                    End If
                Loop
            Else
                Do While current <> Sentinel
                    If Items(current).vKey = KeyVal Then
                        FindNode = current
                        Exit Function
                    Else
                        If KeyVal < Items(current).vKey Then
                            current = Items(current).lLeft
                        Else
                            current = Items(current).lRight
                        End If
                    End If
                Loop

            End If
        End Function

        Private Sub RotateLeft(ByVal x As Long)
            '   inputs:
            '       x                     designates node
            '   action:
            '       perform a lLeft tree rotation about "x"
            '
            Dim y As Long

            ' rotate node x to lLeft

            y = Items(x).lRight

            ' establish x.lRight link
            Items(x).lRight = Items(y).lLeft
            If Items(y).lLeft <> Sentinel Then Items(Items(y).lLeft).lParent = x

            ' establish y.lParent link
            If y <> Sentinel Then Items(y).lParent = Items(x).lParent
            If Items(x).lParent <> 0 Then
                If x = Items(Items(x).lParent).lLeft Then
                    Items(Items(x).lParent).lLeft = y
                Else
                    Items(Items(x).lParent).lRight = y
                End If
            Else
                Root = y
            End If

            ' link x and y
            Items(y).lLeft = x
            If x <> Sentinel Then Items(x).lParent = y
        End Sub

        Private Sub RotateRight(ByVal x As Long)
            '   inputs:
            '       x                     designates node
            '   action:
            '       perform a lRight tree rotation about "x"
            '
            Dim y As Long

            ' rotate node x to lRight

            y = Items(x).lLeft

            ' establish x.lLeft link
            Items(x).lLeft = Items(y).lRight
            If Items(y).lRight <> Sentinel Then Items(Items(y).lRight).lParent = x

            ' establish y.lParent link
            If y <> Sentinel Then Items(y).lParent = Items(x).lParent
            If Items(x).lParent <> 0 Then
                If x = Items(Items(x).lParent).lRight Then
                    Items(Items(x).lParent).lRight = y
                Else
                    Items(Items(x).lParent).lLeft = y
                End If
            Else
                Root = y
            End If

            ' link x and y
            Items(y).lRight = x
            If x <> Sentinel Then Items(x).lParent = y
        End Sub

        Private Sub InsertFixup(ByRef x As Long)
            '   inputs:
            '       x                     designates node
            '   action:
            '       maintains red-black tree properties after inserting node x
            '
            Dim y As Long

            Do While x <> Root
                If Items(Items(x).lParent).Color <> EColor.Red Then Exit Do
                ' we have a violation
                If Items(x).lParent = Items(Items(Items(x).lParent).lParent).lLeft Then
                    y = Items(Items(Items(x).lParent).lParent).lRight
                    If Items(y).Color = EColor.Red Then

                        ' uncle is Red
                        Items(Items(x).lParent).Color = EColor.Black
                        Items(y).Color = EColor.Black
                        Items(Items(Items(x).lParent).lParent).Color = EColor.Red
                        x = Items(Items(x).lParent).lParent
                    Else

                        ' uncle is Black
                        If x = Items(Items(x).lParent).lRight Then
                            ' make x a lLeft child
                            x = Items(x).lParent
                            RotateLeft(x)
                        End If

                        ' recolor and rotate
                        Items(Items(x).lParent).Color = EColor.Black
                        Items(Items(Items(x).lParent).lParent).Color = EColor.Red
                        RotateRight(Items(Items(x).lParent).lParent)
                    End If
                Else

                    ' mirror image of above code
                    y = Items(Items(Items(x).lParent).lParent).lLeft
                    If Items(y).Color = EColor.Red Then

                        ' uncle is Red
                        Items(Items(x).lParent).Color = EColor.Black
                        Items(y).Color = EColor.Black
                        Items(Items(Items(x).lParent).lParent).Color = EColor.Red
                        x = Items(Items(x).lParent).lParent
                    Else

                        ' uncle is Black
                        If x = Items(Items(x).lParent).lLeft Then
                            x = Items(x).lParent
                            RotateRight(x)
                        End If
                        Items(Items(x).lParent).Color = EColor.Black
                        Items(Items(Items(x).lParent).lParent).Color = EColor.Red
                        RotateLeft(Items(Items(x).lParent).lParent)
                    End If
                End If
            Loop
            Items(Root).Color = EColor.Black
        End Sub

        Public Function Add(ByRef Item As Object, Optional ByVal Key As Object = Nothing, Optional ByVal Before As Object = Nothing, Optional ByVal After As Object = Nothing)
            '   inputs:
            '       Item        Item to store
            '       Key         Key to use
            '       Before      The item before which this item will be inserted
            '       After      The item After which this item will be inserted
            '   action:
            '       Inserts Item with Key.
            '   error:
            '       DuplicateKey
            '
            Dim current As Long
            Dim p As Long
            Dim x As Long
            Dim i As Long
            Dim lItems As Long
            Dim strTempKey As String = ""  ' Used to store lcase key

            ' Validate Key
            If Key Is Nothing Then
                Key = CreateGUIDKey()
            Else
                Select Case VarType(Key)
                    Case vbLong, vbInteger, vbByte
                        Raise(HiveErrors.KeyCannotBeInteger, "Add")
                        Return Nothing
                        Exit Function

                    Case vbString
                        If Key = "" Then
                            Raise(HiveErrors.KeyCannotBeBlankOrZero, "Add")
                            Return Nothing
                            Exit Function
                        End If

                End Select
            End If
            ' allocate node for data and insert in tree
            If Node Is Nothing Then Init(InitialAlloc, GrowthFactor)

            ' find where node belongs
            current = Root
            p = 0

            ' ---------------------------------------------------------------
            ' Search hive if the key already exist. If exist then if duplicate
            ' allowed then accept otherwise get out. After serching look for a
            ' position where the new items key will be stored in the Red-Black
            ' tree. Thank you.
            ' ---------------------------------------------------------------
            If VarType(Key) = vbString Then strTempKey = LCase(Key)
            Do While current <> Sentinel
                If mCompareMode <> vbBinaryCompare And VarType(Key) = vbString Then

                    If LCase(Items(current).vKey) = strTempKey Then
                        If Not AllowDuplicate Then
                            Raise(HiveErrors.DuplicateKey, "Add")
                            Return Nothing
                            Exit Function
                        End If
                    End If

                    p = current
                    If strTempKey < LCase(Items(current).vKey) Then
                        current = Items(current).lLeft
                    Else
                        current = Items(current).lRight
                    End If

                Else
                    If Items(current).vKey = Key Then
                        If Not AllowDuplicate Then
                            Raise(HiveErrors.DuplicateKey, "Add")
                            Return Nothing
                            Exit Function
                        End If
                    End If

                    p = current
                    If Key < Items(current).vKey Then
                        current = Items(current).lLeft
                    Else
                        current = Items(current).lRight
                    End If
                End If


            Loop

            ' setup new node
            x = Node.Alloc()
            lItems = UBound(Items)
            If x > lItems Then
                ReDim Preserve Items(0 To lItems * GrowthFactor)
                ReDim Preserve lIndex(0 To (lItems * GrowthFactor) + 2)
            End If

            Items(x).lParent = p
            Items(x).lLeft = Sentinel
            Items(x).lRight = Sentinel
            Items(x).Color = EColor.Red

            ' Increase the counter. Increased value is
            ' required below
            lCount = lCount + 1
            ' Adjust position
            If Not Before Is Nothing Then
                Before = GetKeyIndex(Before)
                If Before = 0 Then
                    Raise(HiveErrors.KeyNotFound, "Add")
                    Return Nothing
                    Exit Function
                End If
                i = GetIndex(Before)
                InsertItem(i)
                lIndex(i) = x

            ElseIf Not After Is Nothing Then
                After = GetKeyIndex(After)
                If After = 0 Then
                    Raise(HiveErrors.KeyNotFound, "Add")
                    Return Nothing
                    Exit Function
                End If
                i = GetIndex(After) + 1
                InsertItem(i)
                lIndex(i) = x
            Else
                lIndex(lCount) = x
            End If

            ' copy fields to node
            Items(x).vKey = Key
            Items(x).vData = Item

            ' insert node in tree
            If p <> 0 Then
                If mCompareMode <> vbBinaryCompare And VarType(Key) = vbString Then
                    If strTempKey < LCase(Items(p).vKey) Then
                        Items(p).lLeft = x
                    Else
                        Items(p).lRight = x
                    End If
                Else
                    If Key < Items(p).vKey Then
                        Items(p).lLeft = x
                    Else
                        Items(p).lRight = x
                    End If
                End If
            Else
                Root = x
            End If

            InsertFixup(x)
            Return Nothing
        End Function

        Private Sub DeleteFixup(ByRef x As Long)
            '   inputs:
            '       x                     designates node
            '   action:
            '       maintains red-black tree properties after deleting a node
            '
            Dim w As Long

            Do While (x <> Root)
                If Items(x).Color <> EColor.Black Then Exit Do
                If x = Items(Items(x).lParent).lLeft Then
                    w = Items(Items(x).lParent).lRight
                    If Items(w).Color = EColor.Red Then
                        Items(w).Color = EColor.Black
                        Items(Items(x).lParent).Color = EColor.Red
                        RotateLeft(Items(x).lParent)
                        w = Items(Items(x).lParent).lRight
                    End If
                    If Items(Items(w).lLeft).Color = EColor.Black _
                    And Items(Items(w).lRight).Color = EColor.Black Then
                        Items(w).Color = EColor.Red
                        x = Items(x).lParent
                    Else
                        If Items(Items(w).lRight).Color = EColor.Black Then
                            Items(Items(w).lLeft).Color = EColor.Black
                            Items(w).Color = EColor.Red
                            RotateRight(w)
                            w = Items(Items(x).lParent).lRight
                        End If
                        Items(w).Color = Items(Items(x).lParent).Color
                        Items(Items(x).lParent).Color = EColor.Black
                        Items(Items(w).lRight).Color = EColor.Black
                        RotateLeft(Items(x).lParent)
                        x = Root
                    End If
                Else
                    w = Items(Items(x).lParent).lLeft
                    If Items(w).Color = EColor.Red Then
                        Items(w).Color = EColor.Black
                        Items(Items(x).lParent).Color = EColor.Red
                        RotateRight(Items(x).lParent)
                        w = Items(Items(x).lParent).lLeft
                    End If
                    If Items(Items(w).lRight).Color = EColor.Black _
                    And Items(Items(w).lLeft).Color = EColor.Black Then
                        Items(w).Color = EColor.Red
                        x = Items(x).lParent
                    Else
                        If Items(Items(w).lLeft).Color = EColor.Black Then
                            Items(Items(w).lRight).Color = EColor.Black
                            Items(w).Color = EColor.Red
                            RotateLeft(w)
                            w = Items(Items(x).lParent).lLeft
                        End If
                        Items(w).Color = Items(Items(x).lParent).Color
                        Items(Items(x).lParent).Color = EColor.Black
                        Items(Items(w).lLeft).Color = EColor.Black
                        RotateRight(Items(x).lParent)
                        x = Root
                    End If
                End If
            Loop
            Items(x).Color = EColor.Black
        End Sub

        Public Function Remove(ByVal KeyVal As Object) As Long
            '   inputs:
            '       KeyVal                key of node to delete
            '   action:
            '       Deletes record with key KeyVal.
            '   error:
            '       errKeyNotFound
            '
            Dim x As Long
            Dim y As Long
            Dim z As Long
            Dim i As Long

            z = GetKeyIndex(KeyVal) ' FindNode(KeyVal)
            If z = 0 Then
                Raise(HiveErrors.InvalidIndex, "Remove")
            End If

            '  delete node z from tree
            If Items(z).lLeft = Sentinel Or Items(z).lRight = Sentinel Then
                ' y has a Sentinel node as a child
                y = z
            Else
                ' find tree successor with a Sentinel node as a child
                y = Items(z).lRight
                Do While Items(y).lLeft <> Sentinel
                    y = Items(y).lLeft
                Loop
            End If

            ' x is y's only child, and x may be a sentinel node
            If Items(y).lLeft <> Sentinel Then
                x = Items(y).lLeft
            Else
                x = Items(y).lRight
            End If

            ' remove y from the lParent chain
            Items(x).lParent = Items(y).lParent
            If Items(y).lParent <> 0 Then
                If y = Items(Items(y).lParent).lLeft Then
                    Items(Items(y).lParent).lLeft = x
                Else
                    Items(Items(y).lParent).lRight = x
                End If
            Else
                Root = x
            End If
            If y <> z Then
                Dim j As Long
                ' copy data fields from y to z
                ' z item now contains y item
                Items(z).vKey = Items(y).vKey
                Items(z).vData = Items(y).vData

                ' Swap index of z and y
                i = GetIndex(z)
                j = GetIndex(y)

                lIndex(i) = y
                lIndex(j) = z


            End If

            ' if we removed a black node, we need to do some fixup
            If Items(y).Color = EColor.Black Then DeleteFixup(x)

            Items(y).vData = Nothing
            Items(y).vData = Nothing
            Items(y).vKey = Nothing
            Items(y).vKey = Nothing

            ' Delete index of y
            i = GetIndex(y)
            LiftItem(i)
            Remove = i

            lIndex(lCount) = 0
            lCount = lCount - 1

            Node.Free(y)
        End Function

        Private Function GetNextNode() As Long
            '   returns:
            '       index to next node, 0 if none
            '   action:
            '       Finds index to next node.
            '
            Do While (NextNode <> 0 Or StackIndex <> 0)
                Do While NextNode <> 0
                    StackIndex = StackIndex + 1
                    Stack(StackIndex) = NextNode
                    NextNode = Items(NextNode).lLeft
                Loop
                GetNextNode = Stack(StackIndex)
                StackIndex = StackIndex - 1
                NextNode = Items(GetNextNode).lRight
                Exit Function
            Loop
            Raise(HiveErrors.KeyNotFound, "GetNextNode")
        End Function


        Public Function FindFirst(ByRef KeyVal As Object) As Object
            '   outputs:
            '       KeyVal                key of node to find
            '   returns:
            '       record associated with key
            '   action:
            '       For sequential access, finds first record.
            '   errors:
            '       errKeyNotFound
            '
            Dim n As Long

            ' for sequential access, call FindFirst, followed by
            ' repeated calls to FindNext

            NextNode = Root
            n = GetNextNode()
            KeyVal = Items(n).vKey
            FindFirst = Items(n).vData
        End Function

        Public Function FindNext(ByRef KeyVal As Object) As Object
            '   outputs:
            '       KeyVal                record key
            '   returns:
            '       record associated with key
            '   action:
            '       For sequential access, finds next record.
            '   errors:
            '       errKeyNotFound
            '
            Dim n As Long

            ' for sequential access, call FindFirst, followed by
            ' repeated calls to FindNext

            n = GetNextNode()
            KeyVal = Items(n).vKey
            FindNext = Items(n).vData
        End Function

        Public Sub Init( _
                ByVal InitialAllocVal As Long, _
                ByVal GrowthFactorVal As Single)
            '   inputs:
            '       InitialAllocVal         initial value for allocating nodes
            '       GrowthFactorVal         amount to grow node storage space
            '   action:
            '       initialize tree
            '
            GrowthFactor = GrowthFactorVal

            ' allocate nodes
            ReDim Items(0 To InitialAllocVal)
            ReDim lIndex(0 To InitialAllocVal + 1)

            ' initialize root and sentinel
            Items(Sentinel).lLeft = Sentinel
            Items(Sentinel).lRight = Sentinel
            Items(Sentinel).lParent = 0
            Items(Sentinel).Color = EColor.Black
            Root = Sentinel

            ' startup node manager
            Node = New cNode
            Node.Init(InitialAllocVal, GrowthFactorVal)

            ' Initialize error container
            Errors = New cErrorCollection

            StackIndex = 0
            lCount = 0
        End Sub

        Public Function Clear() As Long
            '   action:
            '       Clears memory
            Dim i As Long

            If Node Is Nothing Then Exit Function
            Node = Nothing
            Errors = Nothing

            For i = 1 To lCount - 1
                Items(i).vData = Nothing
                Items(i).vData = Nothing
            Next

            For i = 1 To lCount - 1
                Items(i).vKey = Nothing
                Items(i).vKey = Nothing
            Next

            Erase Items
            lCount = 0
            Clear = lCount
        End Function

        Public Function Exist(ByVal vKey As Object) As Boolean
            '   action:
            '       Searches in the array for the specified item
            '   inputs:
            '       vKey        The key or Index of the item
            '   returns:
            '       True is item exist. Otherwise false

            Exist = GetKeyIndex(vKey) > 0

        End Function





        Default Public Property Item(ByVal vKey As Object) As Object
            '   action:
            '       Returns the item specified in vKey
            '   inputs:
            '       vKey        The key or Index of the item
            Get
                Dim lIndex As Long

                Item = Nothing
                lIndex = GetKeyIndex(vKey)
                If lIndex > 0 Then
                    Item = Items(lIndex).vData
                Else
                    Raise(HiveErrors.KeyNotFound, "Item")
                End If
            End Get
            Set(ByVal value As Object)
                Dim lIndex As Long

                lIndex = GetKeyIndex(vKey)
                If lIndex > 0 Then
                    Items(lIndex).vData = value
                End If
            End Set
        End Property

        Public Property Key(ByVal Index As Long) As Object
            Get
                If Index < 0 Or Index > lCount Then
                    Raise(HiveErrors.InvalidIndex, "Key[Read]")
                    Return Nothing
                Else
                    Return Items(lIndex(Index)).vKey
                End If
            End Get

            Set(ByVal value As Object)

                If Index < 0 Or Index > lCount Then
                    Raise(HiveErrors.InvalidIndex, "Key[Assign]")
                Else
                    If FindNode(value) <> 0 Then
                        If Not AllowDuplicate Then
                            Raise(HiveErrors.DuplicateKey, "Key[Assign]")
                        Else
                            Items(lIndex(Index)).vKey = value
                        End If
                    End If
                End If
            End Set
        End Property

        Public ReadOnly Property Count() As Long
            Get
                Return lCount
            End Get
        End Property

        Private Sub LiftItem(ByVal i As Long)
            'Dim x As Long
            'For x = i To lCount - 1
            '    lIndex(x) = lIndex(x + 1)
            'Next
            MoveMemory(lIndex(i), lIndex(i + 1), (lCount - i) * 4)
        End Sub

        Private Sub InsertItem(ByVal i As Long)
            'Dim j As Long
            'For j = lCount To i + 1 Step -1
            '  lIndex(j) = lIndex(j - 1)
            'Next
            MoveMemory(lIndex(i + 1), lIndex(i), (lCount - i) * 4)
        End Sub

        Public Property CompareMode() As Microsoft.VisualBasic.CompareMethod
            Get
                Return mCompareMode
            End Get
            Set(ByVal value As Microsoft.VisualBasic.CompareMethod)
                mCompareMode = value
            End Set
        End Property

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            Clear()
            Errors = Nothing
        End Sub

        Public Sub New()
            InitialAlloc = DefaultInitialAlloc
            GrowthFactor = DefaultGrowthFactor
            mCompareMode = vbTextCompare
        End Sub

        Public ReadOnly Property Current() As Object Implements System.Collections.IEnumerator.Current
            Get
                Dim TmpItem As ItemData
                TmpItem = Items(EnumPointer)
                Return TmpItem.vData
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            EnumPointer += 1
            If EnumPointer > UBound(Items) Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            EnumPointer = 0
        End Sub
    End Class
End Namespace