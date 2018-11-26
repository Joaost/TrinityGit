Public Class frmMultistart

    Dim WithEvents Reg As New Process

    Dim tmpIni As Trinity.clsIni
    Dim registeredManually As Boolean = False

    Private Sub frmMultistart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tmpIni = New Trinity.clsIni

        TmpINI.Create(TrinitySettings.LocalDataPath & "\multistart.ini")

        For i As Integer = 1 To TmpINI.Data("Multistart", "Count")
            cmbProfiles.Items.Add(TmpINI.Text("Multistart", "Name" & i))
        Next
        cmbProfiles.SelectedIndex = TmpINI.Data("Multistart", "Active") - 1

    End Sub

    Private Sub cdmOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cdmOk.Click
        tmpIni = New Trinity.clsIni

        TmpINI.Create(TrinitySettings.LocalDataPath & "\multistart.ini")

        If Not cmbProfiles.SelectedIndex = TmpINI.Data("Multistart", "Active") - 1 Then

            Dim TmpFN As String = TmpINI.Text("Multistart", "File" & TmpINI.Data("Multistart", "Active"))

            My.Computer.FileSystem.CopyFile(TrinitySettings.LocalDataPath & "\Trinity.ini", TrinitySettings.LocalDataPath & "\" & TmpFN, True)

            TmpFN = TmpINI.Text("Multistart", "File" & cmbProfiles.SelectedIndex + 1)

            My.Computer.FileSystem.CopyFile(TrinitySettings.LocalDataPath & "\" & TmpFN, TrinitySettings.LocalDataPath & "\Trinity.ini", True)

            Dim TmpConnect As String = TmpINI.Text("Multistart", "Connect" & cmbProfiles.SelectedIndex + 1)

            TmpINI.Data("Multistart", "Active") = cmbProfiles.SelectedIndex + 1

            If Not registeredManually Then
                Reg.StartInfo.FileName = "regsvr32"
                Reg.StartInfo.Arguments = "/s """ & TmpConnect & """"
                Reg.Start()
                Reg.WaitForExit()
            End If
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK

    End Sub

    Private Sub cmbProfiles_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbProfiles.SelectedIndexChanged
        ' Reg.StartInfo.FileName = "regsvr32"
        'Reg.StartInfo.Arguments = """" & tmpIni.Text("Multistart", "Connect" & cmbProfiles.SelectedIndex + 1) & """"
        ' Reg.Start()
    End Sub

    Private Sub btnRegDLL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegDLL.Click
        registeredManually = True
        Reg.StartInfo.FileName = "regsvr32"
        Reg.StartInfo.Arguments = """" & tmpIni.Text("Multistart", "Connect" & cmbProfiles.SelectedIndex + 1) & """"
        Reg.Start()
    End Sub
End Class