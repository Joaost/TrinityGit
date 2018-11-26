Public Class frmFlashPlayer

    Public Sub New(ByVal Movie As String, Optional ByVal FlashVars As String = "", Optional ByVal Width As Integer = 400, Optional ByVal Height As Integer = 300)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.Width = Width
        Me.Height = Height + 45

        Flash.FlashVars = FlashVars
        Flash.Movie = Movie
    End Sub

    Private Sub frmFlashPlayer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub
End Class