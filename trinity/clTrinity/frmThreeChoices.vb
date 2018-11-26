Public Class frmThreeChoices

    Private _DoForAll As Boolean = False
    Private _Result As Windows.Forms.DialogResult

    Public ReadOnly Property Result() As Windows.Forms.DialogResult
        Get
            Return _Result
        End Get
    End Property

    Private Sub btnReplace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReplace.Click
        _Result = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        _Result = Windows.Forms.DialogResult.Retry
        Me.Close()
    End Sub

    Private Sub btnSkip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSkip.Click
        _Result = Windows.Forms.DialogResult.Ignore
        Me.Close()
    End Sub

    Private Sub chkDoForAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDoForAll.CheckedChanged
        _DoForAll = Not _DoForAll
    End Sub

    Public Sub New(ByVal message As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lblExplanation.Text = message

    End Sub
End Class