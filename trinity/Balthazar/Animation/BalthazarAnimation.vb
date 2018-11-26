Public Class BalthazarAnimation
    Dim frmAnimation As New frmAnimation

    Public Sub Start(ByVal ParentWindow As Windows.Forms.Form)
        frmAnimation.Start()
        frmAnimation.Show()
        If frmAnimation.Tag = 0 Then
            frmAnimation.Tag = 1
            frmAnimation.Left = -frmAnimation.Width
            frmAnimation.Top = ParentWindow.Height / 2
            ParentWindow.Focus()
        End If
    End Sub
End Class
