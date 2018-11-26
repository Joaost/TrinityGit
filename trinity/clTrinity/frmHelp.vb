Public Class frmHelp

    Private _problem As Trinity.cProblem

    Public Sub New(ByVal HelpText As String, ByVal Problem As Trinity.cProblem)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Dim _text As New System.Text.StringBuilder()

        _text.AppendLine("<body style='font-family: Segoe UI; font-size: 12px; width:100%;height:100%;background: #FFFFC0;'>")

        _text.AppendLine(HelpText)

        _text.AppendLine("</body>")

        webHelpText.DocumentText = _text.ToString

        _problem = Problem

        If _problem.IsVisible Then
            cmdDoNotShow.Visible = True
            cmdShow.Visible = False
        Else
            cmdDoNotShow.Visible = False
            cmdShow.Visible = True
        End If
        If _problem.Severity = Trinity.cProblem.ProblemSeverityEnum.Error Then
            cmdDoNotShow.Enabled = False
        Else
            cmdDoNotShow.Enabled = True
        End If

    End Sub

    Private Sub cmdDoNotShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDoNotShow.Click
        _problem.HideAllProblemsOfThisType()
        cmdDoNotShow.Visible = False
        cmdShow.Visible = True
    End Sub

    Private Sub cmdShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShow.Click
        _problem.ShowAllProblemsOfThisType()
        cmdDoNotShow.Visible = True
        cmdShow.Visible = False
    End Sub

    Private Sub cmdAutoFix_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAutoFix.Click
        _problem.FixAllProblemsOfThisType()
    End Sub
End Class