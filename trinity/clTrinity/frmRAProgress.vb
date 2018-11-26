Public Class frmRAProgress
    Sub Go()
        Me.ProgressBar1.Value = 1
        Me.TopMost = True
        ShowProgress()
    End Sub

    Public Sub ShowProgress()
        If Me.InvokeRequired Then
            Invoke(New ShowModalDelegate(AddressOf ShowProgress))
        Else
            Me.ShowDialog()
        End If
    End Sub

    Public Delegate Sub ShowModalDelegate()
End Class