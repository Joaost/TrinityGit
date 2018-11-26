Public Class frmSplashscreen
    Public Property Status() As String
        Get
            Return lblStatus.Text
        End Get
        Set(ByVal value As String)
            lblStatus.Text = value
            Application.DoEvents()
        End Set
    End Property
End Class