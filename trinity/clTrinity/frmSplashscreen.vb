Public Class frmSplashscreen
    'the splash sscreen shows when starting Trinity and continue to show 
    'until all connections is made and the program is fully loaded

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        e.Graphics.DrawRectangle(Drawing.Pens.LightGreen, New Drawing.Rectangle(3, 3, Me.Width - 7, Me.Height - 7))
    End Sub

    Private Sub frmSplashscreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'sets the version label to the current version installed
        lblVersion.Text = "Version: " & My.Application.Info.Version.ToString
        lblVersion.Left = picLogo.Right - lblVersion.Width
        lblLoggingIsOn.Visible = LoggingIsOn
    End Sub

    Public Property Status() As String
        Get
            Return lblStatus.Text
        End Get
        Set(ByVal value As String)
            lblStatus.Text = value
            Windows.Forms.Application.DoEvents()
        End Set
    End Property

    Public Property Build() As String
        Get
            Return lblStatus.Tag
        End Get
        Set(ByVal value As String)
            lblBuild.Tag = value
            lblBuild.Text = "Build: " & Format(Trinity.Helper.CompileTime, "yyyy-MM-dd HH:mm:ss")
            lblBuild.Left = picLogo.Right - lblBuild.Width
        End Set
    End Property

    Public Sub New(Build As String)

        ' This call is required by the designer.
        InitializeComponent()

        Me.Build = Build

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class