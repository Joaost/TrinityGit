Imports System.Windows.Forms

Public Class frmPivotDefinition

#Region " Handles drag and drop, and dubble clicks on the TreeViews"
    Private Sub myItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvwRows.ItemDrag, tvwColumns.ItemDrag, tvwAvailable.ItemDrag
        Dim tree As TreeView = CType(sender, TreeView)

        tree.DoDragDrop(e.Item, Windows.Forms.DragDropEffects.All)
    End Sub

    Private Sub myDragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwColumns.DragDrop, tvwRows.DragDrop, tvwAvailable.DragDrop
        Dim tree As TreeView = CType(sender, TreeView)
        Dim targetPoint As New Point(e.X, e.Y)
        targetPoint = tree.PointToClient(targetPoint)

        ' Retrieve the node at the drop location.
        Dim targetNode As TreeNode = tree.GetNodeAt(targetPoint)

        ' Retrieve the node that was dragged.
        Dim draggedNode As TreeNode = CType(e.Data.GetData(GetType(TreeNode)), TreeNode)
        If draggedNode.Text = "Unit" AndAlso tree.Name = "tvwAvailable" Then Exit Sub 'Units cannot be removed from pivot
        If draggedNode.Text = "Status" AndAlso tree.Name = "tvwAvailable" Then Exit Sub 'Status cannot be removed from pivot

        draggedNode.TreeView.Nodes.Remove(draggedNode)

        If targetNode Is Nothing Then
            tree.Nodes.Add(draggedNode)
        Else
            tree.Nodes.Insert(targetNode.Index, draggedNode)
        End If
    End Sub

    Private Sub tvwAvailable_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwAvailable.NodeMouseDoubleClick
        tvwAvailable.Nodes.Remove(e.Node)
        tvwColumns.Nodes.Add(e.Node)
    End Sub

    Private Sub tvwRows_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwRows.NodeMouseDoubleClick
        tvwRows.Nodes.Remove(e.Node)
        tvwAvailable.Nodes.Add(e.Node)
    End Sub

    Private Sub tvwColumns_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwColumns.NodeMouseDoubleClick
        tvwColumns.Nodes.Remove(e.Node)
        tvwAvailable.Nodes.Add(e.Node)
    End Sub

    Private Sub tvwRows_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwRows.DragEnter, tvwColumns.DragEnter, tvwAvailable.DragEnter
        e.Effect = e.AllowedEffect
    End Sub

    Private Sub myDragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwColumns.DragOver, tvwRows.DragOver 'tvwAvailable.DragOver, 
        Dim tree As TreeView = CType(sender, TreeView)

        e.Effect = e.AllowedEffect

        If Not e.Data.GetData(GetType(TreeNode)) Is Nothing Then
            Dim targetPoint As New Point(e.X, e.Y)
            targetPoint = tree.PointToClient(targetPoint)

            ' Select the node at the mouse position.
            tree.SelectedNode = tree.GetNodeAt(targetPoint)

        End If
    End Sub
#End Region

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub lstUnits_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstUnits.SelectedIndexChanged

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdSaveDefaultUnits_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveDefaultUnits.Click
        Dim tmpList As New List(Of String)

        For Each listitem As Object In lstUnits.CheckedItems
            tmpList.Add(listitem.ToString())
        Next
        TrinitySettings.setPivotDefaultUnits = tmpList

        tmpList.Clear()
        For Each node As TreeNode In tvwColumns.Nodes
            tmpList.Add(node.Text)
        Next
        TrinitySettings.setPivotDefaultColumns = tmpList

        tmpList.Clear()
        For Each node As TreeNode In tvwRows.Nodes
            tmpList.Add(node.Text)
        Next
        TrinitySettings.setPivotDefaultRows = tmpList
    End Sub

    Private Sub frmPivotDefinition_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class