Imports System
Imports System.Windows.Forms

Friend Class frmAnimation

    Dim _frame As Integer = 0
    Dim Sp As Media.SoundPlayer

    Private Sub frmAnimation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub tmrAnimate_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrAnimate.Tick
        _frame += 1
        If _frame > 2 Then _frame = 0
        PictureBox1.Image = ImageList1.Images(_frame)
        Me.Left += 20
        If Me.Left > Screen.PrimaryScreen.Bounds.Width Then
            StopMe()
        End If

    End Sub

    Sub StopMe()
        Me.Hide()
        Me.Tag = 0
        If Not Sp Is Nothing Then
            Sp.Stop()
            Sp.Dispose()
            Sp = Nothing
        End If
    End Sub

    Public Sub Start()
        Sp = New Media.SoundPlayer(My.Resources.balt)
        Sp.PlayLooping()
    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        Dim TmpBMP As New Drawing.Bitmap(PictureBox1.Image)
        If TmpBMP.GetPixel(e.X, e.Y) <> Me.TransparencyKey Then
            StopMe()
        End If
    End Sub
End Class