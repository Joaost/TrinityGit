Public Class frmProgress
    Public Property Progress() As Integer
        Get
            Return pb.Progress
        End Get
        Set(ByVal value As Integer)
            pb.Progress = value
            Me.Text = "Progress - " & value & "%"
            Windows.Forms.Application.DoEvents()
        End Set
    End Property

    Public Property Status() As String
        Get
            Return lblStatus.Text
        End Get
        Set(ByVal value As String)
            lblStatus.Text = value
            Windows.Forms.Application.DoEvents()
        End Set
    End Property
End Class