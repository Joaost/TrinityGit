Public Class frmColumns

    Private Sub tvwAvailable_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvwAvailable.ItemDrag
        tvwChosen.DoDragDrop(e.Item, Windows.Forms.DragDropEffects.All)
    End Sub

    Private Sub tvwChosen_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwChosen.DragDrop
        Dim targetPoint As System.Drawing.Point = tvwChosen.PointToClient(New System.Drawing.Point(e.X, e.Y))

        ' Retrieve the node at the drop location.
        Dim targetNode As System.Windows.Forms.TreeNode = tvwChosen.GetNodeAt(targetPoint)

        ' Retrieve the node that was dragged.
        Dim draggedNode As System.Windows.Forms.TreeNode = CType(e.Data.GetData(GetType(System.Windows.Forms.TreeNode)), System.Windows.Forms.TreeNode)

        draggedNode.TreeView.Nodes.Remove(draggedNode)

        If targetNode Is Nothing Then
            tvwChosen.Nodes.Add(draggedNode)
        Else
            tvwChosen.Nodes.Insert(targetNode.Index, draggedNode)
        End If
    End Sub

    Private Sub tvwChosen_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwChosen.DragEnter
        e.Effect = e.AllowedEffect
    End Sub

    Private Sub tvwChosen_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvwChosen.DragOver
        Dim targetPoint As System.Drawing.Point = tvwChosen.PointToClient(New System.Drawing.Point(e.X, e.Y))

        e.Effect = e.AllowedEffect
        ' Select the node at the mouse position.
        tvwChosen.SelectedNode = tvwChosen.GetNodeAt(targetPoint)

    End Sub

    Private Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub tvwAvailable_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwAvailable.AfterSelect

    End Sub

    Private Sub tvwAvailable_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwAvailable.NodeMouseDoubleClick
        tvwAvailable.Nodes.Remove(e.Node)
        tvwChosen.Nodes.Add(e.Node)
    End Sub

    Private Sub tvwChosen_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvwChosen.AfterSelect

    End Sub

    Private Sub tvwChosen_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwChosen.NodeMouseDoubleClick
        tvwChosen.Nodes.Remove(e.Node)
        tvwAvailable.Nodes.Add(e.Node)
    End Sub

    Private Sub tvwChosen_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvwChosen.ItemDrag
        tvwChosen.DoDragDrop(e.Item, Windows.Forms.DragDropEffects.All)
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub frmColumns_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub tvwAvailable_ParentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tvwAvailable.ParentChanged

    End Sub
End Class