Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cNode

        ' class CNode, node allocator

        Private FreeList() As Long              ' linked list of free nodes
        Private FreeHdr As Long                 ' head of free FreeList
        Private GrowthFactor As Single          ' how much to grow

        Public Sub Init(ByVal InitialAllocVal As Long, ByVal GrowthFactorVal As Single)
            '   inputs:
            '       InitialAlloc          initial allocation for nodes
            '       GrowthFactor          amount to grow allocation
            '   action:
            '       Allocates internal structures to manage node allocation.
            '
            Dim i As Long

            GrowthFactor = GrowthFactorVal
            ReDim FreeList(0 To InitialAllocVal)
            For i = 1 To InitialAllocVal - 1
                FreeList(i) = i + 1
            Next i
            FreeList(InitialAllocVal) = 0
            FreeHdr = 1
        End Sub

        Public Function Alloc() As Long
            '   returns:
            '       Allocated subscript.
            '   action:
            '       Allocates subscript.
            '
            Dim i As Long

            ' if Free is empty, reallocate array
            If FreeHdr = 0 Then
                FreeHdr = UBound(FreeList) + 1
                ReDim Preserve FreeList(0 To UBound(FreeList) * GrowthFactor)
                For i = FreeHdr To UBound(FreeList) - 1
                    FreeList(i) = i + 1
                Next i
                FreeList(UBound(FreeList)) = 0
            End If

            ' return index to free node
            Alloc = FreeHdr
            FreeHdr = FreeList(FreeHdr)
        End Function

        Public Sub Free(ByVal i As Long)
            '   input:
            '       i             subscript to free
            '   action:
            '       Frees subscript for reuse.
            FreeList(i) = FreeHdr
            FreeHdr = i
        End Sub

    End Class

End Namespace