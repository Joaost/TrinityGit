Imports System.Windows.Forms

Public Class frmDefine
    'A change on the dev branch'
    Public Pivot As AxMicrosoft.Office.Interop.Owc11.AxPivotTable

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim TmpNode As TreeNode

        While Pivot.ActiveView.RowAxis.FieldSets.Count > 0
            Pivot.ActiveView.RowAxis.RemoveFieldSet(Pivot.ActiveView.RowAxis.FieldSets(0))
        End While
        If tvwRows.Nodes.Count > 0 Then
            TmpNode = tvwRows.Nodes(0)
            While Not TmpNode.LastNode Is Nothing
                TmpNode = TmpNode.LastNode
            End While
        Else
            TmpNode = Nothing
        End If
        While Not TmpNode Is Nothing
            Pivot.ActiveView.RowAxis.InsertFieldSet(Pivot.ActiveView.FieldSets(TmpNode.Text))
            Pivot.ActiveView.FieldSets(TmpNode.Text).Fields(0).Subtotals(0) = False
            TmpNode = TmpNode.NextNode
        End While
        While Pivot.ActiveView.ColumnAxis.FieldSets.Count > 0
            Pivot.ActiveView.ColumnAxis.RemoveFieldSet(Pivot.ActiveView.ColumnAxis.FieldSets(0))
        End While
        If tvwColumns.Nodes.Count > 0 Then
            TmpNode = tvwColumns.Nodes(0)
            While Not TmpNode.LastNode Is Nothing
                TmpNode = TmpNode.LastNode
            End While
        Else
            TmpNode = Nothing
        End If
        While Not TmpNode Is Nothing
            Pivot.ActiveView.ColumnAxis.InsertFieldSet(Pivot.ActiveView.FieldSets(TmpNode.Text))
            Pivot.ActiveView.FieldSets(TmpNode.Text).Fields(0).Subtotals(0) = False
            TmpNode = TmpNode.NextNode
        End While
        While Pivot.ActiveView.FilterAxis.FieldSets.Count > 0
            Pivot.ActiveView.FilterAxis.RemoveFieldSet(Pivot.ActiveView.FilterAxis.FieldSets(0))
        End While
        If tvwFilter.Nodes.Count > 0 Then
            TmpNode = tvwFilter.Nodes(0)
            While Not TmpNode.LastNode Is Nothing
                TmpNode = TmpNode.LastNode
            End While
        Else
            TmpNode = Nothing
        End If
        While Not TmpNode Is Nothing
            Pivot.ActiveView.FilterAxis.InsertFieldSet(Pivot.ActiveView.FieldSets(TmpNode.Text))
            Pivot.ActiveView.FieldSets(TmpNode.Text).Fields(0).Subtotals(0) = False
            TmpNode = TmpNode.NextNode
        End While

        'ReDim Inc(0 To lstUnits.CheckedItems.Count - 1)
        'For i = 0 To lstUnits.CheckedItems.Count - 1
        '    Inc(i) = Pivot.ActiveView.FieldSets("Unit").Member.ChildMembers(lstUnits.CheckedItems(i)).Caption
        'Next
        'For Each TmpNode In tvwAvailable.Nodes
        '    Pivot.ActiveView.FieldSets(TmpNode.Text).Fields(0).IncludedMembers = Nothing
        '    Pivot.ActiveView.FieldSets(TmpNode.Text).Fields(0).ExcludedMembers = Nothing
        '    Pivot.ActiveView.FieldSets(TmpNode.Text).AllIncludeExclude = Microsoft.Office.Interop.Owc11.PivotFieldSetAllIncludeExcludeEnum.plAllDefault
        'Next
        'If lstUnits.CheckedItems.Count > 0 Then
        '    Pivot.ActiveView.FieldSets("Unit").Fields(0).IncludedMembers = Inc
        'Else
        '    Pivot.ActiveView.FieldSets("Unit").Fields(0).IncludedMembers = Nothing
        'End If
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmDefine_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        Dim ID As String

        For i = 0 To Pivot.ActiveView.FieldSets.Count - 1
            If Pivot.ActiveView.FieldSets(i).Name <> "Value" Then
                tvwAvailable.Nodes.Add(text:=Pivot.ActiveView.FieldSets(i).Name, key:=Pivot.ActiveView.FieldSets(i).Name)
            End If
        Next
        For i = 0 To Pivot.ActiveView.RowAxis.FieldSets.Count - 1
            ID = CreateGUID()
            tvwRows.Nodes.Add(text:=Pivot.ActiveView.RowAxis.FieldSets(i).Name, key:=ID)
            tvwAvailable.Nodes(Pivot.ActiveView.RowAxis.FieldSets(i).Name).Remove()
        Next
        For i = 0 To Pivot.ActiveView.ColumnAxis.FieldSets.Count - 1
            ID = CreateGUID()
            tvwColumns.Nodes.Add(text:=Pivot.ActiveView.ColumnAxis.FieldSets(i).Name, key:=ID)
            tvwAvailable.Nodes(Pivot.ActiveView.ColumnAxis.FieldSets(i).Name).Remove()
        Next
        For i = 0 To Pivot.ActiveView.FilterAxis.FieldSets.Count - 1
            ID = CreateGUID()
            tvwFilter.Nodes.Add(text:=Pivot.ActiveView.FilterAxis.FieldSets(i).Name, key:=ID)
            tvwAvailable.Nodes(Pivot.ActiveView.FilterAxis.FieldSets(i).Name).Remove()
        Next
        'ub = -1
        'On Error Resume Next
        'ub = UBound(Pivot.ActiveView.FieldSets("Unit").Fields(0).IncludedMembers)
        'On Error GoTo 0
        'For i = 0 To Pivot.ActiveView.FieldSets("Unit").Member.ChildMembers.Count - 1
        ' Dim NewIndex As Integer = lstUnits.Items.Add(Pivot.ActiveView.FieldSets("Unit").Member.ChildMembers(i).Caption)
        ' For j = 0 To ub
        ' If lstUnits.Items(NewIndex) = Pivot.ActiveView.FieldSets("Unit").Fields(0).IncludedMembers(j).Caption Then
        ' lstUnits.SetItemChecked(NewIndex, True)
        ' End If
        ' Next
        ' Next
        tvwAvailable.AllowDrop = True
        tvwColumns.AllowDrop = True
        tvwRows.AllowDrop = True
        tvwFilter.AllowDrop = True

        tvwRows.ExpandAll()
        tvwColumns.ExpandAll()
        tvwFilter.ExpandAll()
    End Sub

    Private Sub tvwAvailable_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvwAvailable.ItemDrag
        tvwAvailable.DoDragDrop(e.Item, Windows.Forms.DragDropEffects.All)
    End Sub

    Private Sub tvwColumns_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwColumns.DragDrop
        Dim targetPoint As System.Drawing.Point = tvwColumns.PointToClient(New System.Drawing.Point(e.X, e.Y))

        ' Retrieve the node at the drop location.
        Dim targetNode As System.Windows.Forms.TreeNode = tvwColumns.GetNodeAt(targetPoint)

        ' Retrieve the node that was dragged.
        Dim draggedNode As System.Windows.Forms.TreeNode = CType(e.Data.GetData(GetType(System.Windows.Forms.TreeNode)), System.Windows.Forms.TreeNode)

        draggedNode.TreeView.Nodes.Remove(draggedNode)

        If targetNode Is Nothing Then
            tvwColumns.Nodes.Add(draggedNode)
        Else
            tvwColumns.Nodes.Insert(targetNode.Index, draggedNode)
        End If
    End Sub

    Private Sub tvwRows_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwRows.DragDrop
        Dim targetPoint As System.Drawing.Point = tvwColumns.PointToClient(New System.Drawing.Point(e.X, e.Y))

        ' Retrieve the node at the drop location.
        Dim targetNode As System.Windows.Forms.TreeNode = tvwRows.GetNodeAt(targetPoint)

        ' Retrieve the node that was dragged.
        Dim draggedNode As System.Windows.Forms.TreeNode = CType(e.Data.GetData(GetType(System.Windows.Forms.TreeNode)), System.Windows.Forms.TreeNode)

        draggedNode.TreeView.Nodes.Remove(draggedNode)

        If targetNode Is Nothing Then
            tvwRows.Nodes.Add(draggedNode)
        Else
            tvwRows.Nodes.Insert(targetNode.Index, draggedNode)
        End If
    End Sub

    Private Sub tvwFilter_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwFilter.DragDrop
        Dim targetPoint As System.Drawing.Point = tvwColumns.PointToClient(New System.Drawing.Point(e.X, e.Y))

        ' Retrieve the node at the drop location.
        Dim targetNode As System.Windows.Forms.TreeNode = tvwFilter.GetNodeAt(targetPoint)

        ' Retrieve the node that was dragged.
        Dim draggedNode As System.Windows.Forms.TreeNode = CType(e.Data.GetData(GetType(System.Windows.Forms.TreeNode)), System.Windows.Forms.TreeNode)

        draggedNode.TreeView.Nodes.Remove(draggedNode)

        If targetNode Is Nothing Then
            tvwFilter.Nodes.Add(draggedNode)
        Else
            tvwFilter.Nodes.Insert(targetNode.Index, draggedNode)
        End If
    End Sub

    Private Sub tvwColumns_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwColumns.DragEnter
        e.Effect = e.AllowedEffect
    End Sub

    Private Sub tvwColumns_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwColumns.DragOver
        Dim targetPoint As System.Drawing.Point = tvwColumns.PointToClient(New System.Drawing.Point(e.X, e.Y))

        e.Effect = e.AllowedEffect
        ' Select the node at the mouse position.
        tvwColumns.SelectedNode = tvwColumns.GetNodeAt(targetPoint)

    End Sub

    Private Sub tvwRows_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwRows.DragEnter
        e.Effect = e.AllowedEffect
    End Sub

    Private Sub tvwRows_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwRows.DragOver
        Dim targetPoint As System.Drawing.Point = tvwRows.PointToClient(New System.Drawing.Point(e.X, e.Y))

        e.Effect = e.AllowedEffect
        ' Select the node at the mouse position.
        tvwRows.SelectedNode = tvwRows.GetNodeAt(targetPoint)

    End Sub

    Private Sub tvwFilter_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwFilter.DragEnter
        e.Effect = e.AllowedEffect
    End Sub

    Private Sub tvwFilter_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwFilter.DragOver
        Dim targetPoint As System.Drawing.Point = tvwFilter.PointToClient(New System.Drawing.Point(e.X, e.Y))

        e.Effect = e.AllowedEffect
        ' Select the node at the mouse position.
        tvwFilter.SelectedNode = tvwFilter.GetNodeAt(targetPoint)

    End Sub

    Private Sub tvwAvailable_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwAvailable.NodeMouseDoubleClick
        tvwAvailable.Nodes.Remove(e.Node)
        tvwColumns.Nodes.Add(e.Node)
    End Sub

    Private Sub tvwColumns_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwColumns.NodeMouseDoubleClick
        tvwColumns.Nodes.Remove(e.Node)
        tvwAvailable.Nodes.Add(e.Node)
    End Sub

    Private Sub tvwColumns_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvwColumns.ItemDrag
        tvwColumns.DoDragDrop(e.Item, Windows.Forms.DragDropEffects.All)
    End Sub

    Private Sub tvwRows_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwRows.NodeMouseDoubleClick
        tvwRows.Nodes.Remove(e.Node)
        tvwAvailable.Nodes.Add(e.Node)
    End Sub

    Private Sub tvwRows_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvwRows.ItemDrag
        tvwRows.DoDragDrop(e.Item, Windows.Forms.DragDropEffects.All)
    End Sub

    Private Sub tvwFilter_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwFilter.NodeMouseDoubleClick
        tvwFilter.Nodes.Remove(e.Node)
        tvwAvailable.Nodes.Add(e.Node)
    End Sub

    Private Sub tvwFilter_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvwFilter.ItemDrag
        tvwFilter.DoDragDrop(e.Item, Windows.Forms.DragDropEffects.All)
    End Sub

    Public Sub New(ByVal _pivot As AxMicrosoft.Office.Interop.Owc11.AxPivotTable)

        Pivot = _pivot
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

End Class
