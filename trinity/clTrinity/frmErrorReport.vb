Public Class frmErrorReport

    Private _exception As Exception

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        If dlgOpen.ShowDialog() = vbOK Then
            txtFile.Text = dlgOpen.FileName
        End If
    End Sub

    Public Overloads Sub Show(ByVal e As Exception)
        _exception = e
        MyBase.Show()
    End Sub

    Public Overloads Function ShowDialog(e As Exception)
        _exception = e
        Return MyBase.ShowDialog()
    End Function

    Private Sub cmdSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSend.Click
        Try
            'Added by JK
            Me.Visible = False
            My.Application.DoEvents()
            Threading.Thread.Sleep(100)


            Dim _body As New System.Text.StringBuilder()
            Dim _files As New List(Of String)
            Dim campaignName As String = Campaign.Name
            Dim _campFile As String = Trinity.Helper.GetSpecialFolder(clTrinity.Trinity.Helper.CSIDLEnum.Temp) & "errorCampaign" & CreateGUID() & ".cmp"
            Dim MyVersion As String = Format(Trinity.Helper.CompileTime.AddHours(1), "yyyy-MM-dd HH:mm:ss")

            _body.AppendLine("Trinity build: " & MyVersion)
            _body.AppendLine()
            _body.AppendLine("Campaign name:")
            _body.AppendLine(campaignName)
            _body.AppendLine("User comments:")
            _body.AppendLine(txtDescription.Text)
            _body.AppendLine()
            If _exception IsNot Nothing Then
                _body.AppendLine("Error message:")
                _body.AppendLine(_exception.Message)
                _body.AppendLine()
                _body.AppendLine("Stack Trace:")
                _body.AppendLine(_exception.StackTrace)

            End If
            _body.AppendLine()
            _body.AppendLine("Debug Stack:")
            _body.Append(String.Join(vbCrLf, DebugStack.GetLog.ToArray))

            If My.Computer.FileSystem.FileExists(_campFile) Then
                Kill(_campFile)
            End If
            Try
                Campaign.SaveCampaign(_campFile)
            Catch ex As Exception
                _body.AppendLine("Error while saving file:")
                _body.AppendLine(ex.Message)
                _body.AppendLine()
                _body.AppendLine("Stack Trace while saving:")
                _body.AppendLine(ex.StackTrace)
            End Try


            Trinity.Helper.SendEmail("trinity.support@groupm.com", txtName.Text, TrinitySettings.UserEmail, "Trinity error report" & campaignName, _body.ToString, _files)
            'Trinity.Helper.SendEmail("mecstomail.trinity@mecglobal.com", txtName.Text, TrinitySettings.UserEmail, "Trinity error report", _body.ToString)

            Windows.Forms.MessageBox.Show("Error report has been sent", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("Could not send error report:" & vbCrLf & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub frmErrorReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtName.Text = TrinitySettings.UserName
        txtFile.Text = Campaign.Name
    End Sub
End Class