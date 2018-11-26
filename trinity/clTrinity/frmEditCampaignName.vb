Public Class frmEditCampaignName

    Public result As String
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        result = txtCampaignName.Text
        Me.Close()
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        result = Nothing
        Me.Close()
    End Sub

    Public Sub New(ByVal name As String)
        'txtCampaignName.Text = name

        InitializeComponent()
    End Sub

End Class
