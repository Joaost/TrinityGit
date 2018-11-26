Public Class frmFindTarget

    Private Sub frmFindTarget_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If TrinitySettings.SortTargetsByUser Then
            chkSortByUser.Checked = True
        Else
            UpdateTreeView()
        End If
    End Sub

    Sub UpdateTreeView()
        Trinity.Helper.WriteToLogFile("UpdateTreeView: Get Targets from AdEdge")
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        tvwTargets.Nodes.Clear()
        tvwTargets.BeginUpdate()
        Windows.Forms.Application.DoEvents()
        If AdedgeUserTarget Is Nothing Then UpdateAdedgeTargets()
        For Each kv As KeyValuePair(Of String, List(Of String)) In AdedgeUserTarget
            If chkSortByUser.Checked Then
                With DirectCast(tvwTargets.Nodes.Add(kv.Key), System.Windows.Forms.TreeNode)
                    For Each TmpTarget As String In kv.Value
                        With DirectCast(.Nodes.Add(TmpTarget), System.Windows.Forms.TreeNode)
                            .Tag = kv.Key
                        End With
                    Next
                End With
            Else
                For Each TmpTarget As String In kv.Value
                    With DirectCast(tvwTargets.Nodes.Add(TmpTarget), System.Windows.Forms.TreeNode)
                        .Tag = kv.Key
                    End With
                Next
            End If
        Next
        tvwTargets.Sort()
        tvwTargets.EndUpdate()
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub UpdateAdedgeTargets()
        AdedgeUserTarget = New Dictionary(Of String, List(Of String))

        'Trinity.Helper.WriteToLogFile("UpdateAdedgeTargets: Found " & Campaign.Adedge.lookupNoUsers & " users")
        For i As Integer = 0 To Campaign.Adedge.lookupNoUsers - 1
            Dim TmpList As New List(Of String)
            Dim tim As Decimal = Now.ToOADate
            Try
                Dim targetsforuser As Integer = Campaign.Adedge.lookupNoTargetsByUser(Campaign.Adedge.lookupUsers(i))
                Dim targetgroup As String = Campaign.Adedge.lookupUsers(i)
                'Try
                'Trinity.Helper.WriteToLogFile("UpdateTreeView: Added user " & Campaign.Adedge.lookupUsers(i) & " to Collection")
                'Trinity.Helper.WriteToLogFile("Found " & Campaign.Adedge.lookupNoTargetsByUser(Campaign.Adedge.lookupUsers(i)) & " targets.")

                'If targetsforuser < 100 Then
                If targetsforuser > 1000 Then

                    For t As Integer = 0 To targetsforuser - 1
                        'Trinity.Helper.WriteToLogFile("Found  target " & Campaign.Adedge.lookupTargetsByUser(Campaign.Adedge.lookupUsers(i), t))
                        Dim tookHowLong As New Stopwatch
                        tookHowLong.Start()
                        Dim customTarget As String = Campaign.Adedge.lookupTargetsByUser(targetgroup, t)
                        tookHowLong.Stop()
                        'Debug.Print("Operation took " & tookHowLong.ElapsedMilliseconds / 1000 & " seconds.")
                        TmpList.Add(customTarget)
                    Next
                Else
                    Dim TargStr As String = Campaign.Adedge.lookupTargetsByUserList(Campaign.Adedge.lookupUsers(i))
                    Dim Targets() As String = TargStr.Split("|")
                    For t As Integer = 0 To Targets.Length - 1
                        TmpList.Add(Targets(t))
                    Next
                End If
                'Catch
                'Debug.Print("Problem with finding user defined target group")
                'End Try
                tim = Now.ToOADate - tim
                AdedgeUserTarget.Add(Campaign.Adedge.lookupUsers(i), TmpList)
            Catch

            End Try
        Next
    End Sub

    Private Sub chkSortByUser_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSortByUser.CheckedChanged
        TrinitySettings.SortTargetsByUser = chkSortByUser.Checked
        UpdateTreeView()
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        If tvwTargets.SelectedNode Is Nothing Then Exit Sub
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub tvwTargets_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvwTargets.NodeMouseDoubleClick
        cmdOk_Click(sender, New System.EventArgs)
    End Sub

    Public Overloads Function ShowDialog(Optional ByVal Message As String = "")

        If Message <> "" Then
            tvwTargets.Left = 12
            tvwTargets.Top = 26
            tvwTargets.Height = 381
            tvwTargets.Width = 441
        Else
            tvwTargets.Left = 12
            tvwTargets.Top = 12
            tvwTargets.Height = 395
            tvwTargets.Width = 441
        End If
        lblMessage.Text = Message

        Return MyBase.ShowDialog()

    End Function

    Public Sub New()


        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class