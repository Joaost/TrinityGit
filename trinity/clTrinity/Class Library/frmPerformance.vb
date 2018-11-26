Public Class frmPerformance

    Dim writeTime As String
    Dim readTime As String
    Dim combinedTime As String

    Private Sub fileSystemTestButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fileSystemTestButton.Click
        Select Case cPerformanceTest.ReadWriteTest()
            Case cPerformanceTest.FileScores.Excellent
                resultFileSystem.Text = "Excellent"
            Case cPerformanceTest.FileScores.Good
                resultFileSystem.Text = "Good"
            Case cPerformanceTest.FileScores.Adequate
                resultFileSystem.Text = "Adequate"
            Case cPerformanceTest.FileScores.Poor
                resultFileSystem.Text = "Poor"
            Case Else
                resultFileSystem.Text = "Poor"
        End Select
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler cPerformanceTest.Progress, AddressOf Progress
        AddHandler CallBack.Progress, AddressOf Progress
    End Sub


    Private Sub testNetworkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles testAdEdgeButton.Click

        Select Case cPerformanceTest.AdvantEdgeTest()
            Case cPerformanceTest.AdEdgeScores.Excellent
                resultAdEdge.Text = "Excellent"
            Case cPerformanceTest.AdEdgeScores.Good
                resultAdEdge.Text = "Good"
            Case cPerformanceTest.AdEdgeScores.Adequate
                resultAdEdge.Text = "Adequate"
            Case cPerformanceTest.AdEdgeScores.Poor
                resultAdEdge.Text = "Poor"
            Case Else
                resultAdEdge.Text = "Poor"
        End Select
    End Sub

    Private Sub testAllButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles testAllButton.Click

        fileSystemTestButton_Click(Me, e)
        testNetworkButton_Click(Me, e)

    End Sub

    Private Sub Progress(ByVal percent As Integer)
        frmProgress.Progress = percent
    End Sub
End Class