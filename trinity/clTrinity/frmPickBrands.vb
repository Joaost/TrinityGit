Public Class frmPickBrands

    Private Sub frmPickBrands_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If AdedgeAdvertisers Is Nothing Then
            AdedgeAdvertisers = New SortedList(Of String, String)
            AdedgeProducts = New SortedList(Of String, String)
            For i As Integer = 0 To Campaign.InternalAdedge.lookupNoAdvertisers(Campaign.AreaLog) - 1
                Dim TmpStr As String = Campaign.InternalAdedge.lookupAdvertiser(Campaign.AreaLog, i)
                If AdedgeAdvertisers.ContainsKey(TmpStr) Then
                    Dim j As Integer = 1
                    While AdedgeAdvertisers.ContainsKey(TmpStr & j)
                        j += 1
                    End While
                    AdedgeAdvertisers.Add(TmpStr & j, TmpStr)
                Else
                    AdedgeAdvertisers.Add(TmpStr, TmpStr)
                End If
            Next
            For i As Integer = 0 To Campaign.InternalAdedge.lookupNoProducts(Campaign.AreaLog) - 1
                Dim TmpStr As String = Campaign.InternalAdedge.lookupProduct(Campaign.AreaLog, i)
                If AdedgeProducts.ContainsKey(TmpStr) Then
                    Dim j As Integer = 1
                    While AdedgeProducts.ContainsKey(TmpStr & j)
                        j += 1
                    End While
                    AdedgeProducts.Add(TmpStr & j, TmpStr)
                Else
                    AdedgeProducts.Add(TmpStr, TmpStr)
                End If
            Next
        End If
        cmbDimension.SelectedIndex = 1
        tvwChosen.Nodes.Clear()
        For Each TmpString As String In Campaign.AdEdgeProducts
            If TmpString.Substring(0, 3) = "[A]" Then
                tvwChosen.Nodes.Add(TmpString.Substring(3)).ForeColor = Color.Red
            Else
                tvwChosen.Nodes.Add(TmpString.Substring(3)).ForeColor = Color.Blue
            End If
            tvwChosen.Sort()
        Next
        txtFilter.Text = ""
    End Sub

    Private Sub cmbDimension_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDimension.SelectedIndexChanged
        UpdateList()
    End Sub

    Sub UpdateList()
        tvwAvailable.SuspendLayout()
        tvwAvailable.Nodes.Clear()
        If cmbDimension.SelectedIndex = 0 Then
            For Each TmpString As String In AdedgeAdvertisers.Values
                If TmpString.ToUpper.IndexOf(txtFilter.Text.ToUpper) > -1 Then
                    tvwAvailable.Nodes.Add(TmpString).ForeColor = Color.Red
                End If
            Next
        Else
            For Each TmpString As String In AdedgeProducts.Values
                If TmpString.ToUpper.IndexOf(txtFilter.Text.ToUpper) > -1 Then
                    tvwAvailable.Nodes.Add(TmpString).ForeColor = Color.Blue
                End If
            Next
        End If
        tvwAvailable.ResumeLayout()
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        tvwChosen.Nodes.Add(tvwAvailable.SelectedNode.Clone)
        tvwChosen.Sort()
    End Sub

    Private Sub cmdRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
        tvwChosen.SelectedNode.Remove()
    End Sub

    Private Sub txtFilter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFilter.TextChanged
        UpdateList()
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Campaign.AdEdgeProducts.Clear()
        For Each TmpNode As Windows.Forms.TreeNode In tvwChosen.Nodes
            If TmpNode.ForeColor = Color.Red Then
                Campaign.AdEdgeProducts.Add("[A]" & TmpNode.Text)
            Else
                Campaign.AdEdgeProducts.Add("[P]" & TmpNode.Text)
            End If
        Next
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub tvwAvailable_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwAvailable.NodeMouseDoubleClick
        cmdAdd_Click(sender, e)
    End Sub

    Private Sub tvwChosen_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwChosen.NodeMouseDoubleClick
        cmdRemove_Click(sender, e)
    End Sub
End Class