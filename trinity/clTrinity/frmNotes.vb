Public Class frmNotes
    'a simple notebook for short comments
    Private Sub frmNotes_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        If Campaign.Commentary <> "" Then 'if there is a note the backgriound if the icon is crimson
            'frmMain.cmdNotes.BackColor = Drawing.Color.Crimson
        Else
            'frmMain.cmdNotes.BackColor = frmMain.cmdAllocate.BackColor
        End If
    End Sub

    Private Sub frmNotes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'when the window is loaded all saved text is retrieved from storage
        txtNotes.Enabled = Campaign.RootCampaign Is Nothing
        If Me.Tag Is Nothing Then
            txtNotes.Text = Campaign.Commentary
        End If
    End Sub

    Private Sub txtNotes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNotes.TextChanged
        'updates the saved string whenever a change is made
        If Me.Tag Is Nothing Then
            Campaign.Commentary = txtNotes.Text
        Else
            DirectCast(Me.Tag, Trinity.cBookingType).Comments = txtNotes.Text
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.Tag = Nothing
    End Sub

    Public Sub New(ByVal Bookingtype As Trinity.cBookingType)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.Text = "Notes - " & Bookingtype.ToString
        Me.Tag = Bookingtype
        txtNotes.Text = Bookingtype.Comments
    End Sub

    Public Sub New(ByVal Combo As Trinity.cCombination)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.Text = "Notes - " & Combo.Name

        Me.Tag = Combo.Relations(1).Bookingtype
        txtNotes.Text = Combo.Relations(1).Bookingtype.Comments
    End Sub
End Class