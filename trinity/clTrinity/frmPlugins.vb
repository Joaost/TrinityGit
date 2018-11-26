Imports System.Xml
Imports System.Xml.Linq
Imports System.Linq

Public Class frmPlugins

    Private Class Plugin

        Public Property Label As String
        Public Property Name As String

        Public Overrides Function ToString() As String
            If String.IsNullOrEmpty(Label) Then
                Return Name
            End If
            Return Label
        End Function

    End Class

    Dim WithEvents _downloader As New WebFileDownloader

    Private Sub cmdOk_Click(sender As System.Object, e As System.EventArgs) Handles cmdOk.Click
        Dim _hasChanged As Boolean = False
        For Each _p As Plugin In lstPlugins.Items
            If lstPlugins.CheckedItems.Contains(_p) AndAlso Not IO.File.Exists(IO.Path.Combine(IO.Path.Combine(My.Application.Info.DirectoryPath, "Plugins"), _p.Name)) Then
                InstallPlugin(_p.Name)
                _hasChanged = True
            ElseIf Not lstPlugins.CheckedItems.Contains(_p) AndAlso IO.File.Exists(IO.Path.Combine(IO.Path.Combine(My.Application.Info.DirectoryPath, "Plugins"), _p.Name)) Then
                UninstallPlugin(_p.Name)
                _hasChanged = True
            End If
        Next
        If _hasChanged Then
            frmProgress.Hide()
            If Windows.Forms.MessageBox.Show("These changes will take effect next time you start Trinity." & vbNewLine & vbNewLine & "Do you want to restart Trinity now?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Shell(IO.Path.Combine(My.Application.Info.DirectoryPath, "launchtrinity.exe"), AppWinStyle.NormalFocus)
                End
            End If
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Public Sub InstallPlugin(name As String)
        If Not IO.Directory.Exists(IO.Path.Combine(My.Application.Info.DirectoryPath, "Plugins")) Then
            IO.Directory.CreateDirectory(IO.Path.Combine(My.Application.Info.DirectoryPath, "Plugins"))
        End If
        Dim Ini As New Trinity.clsIni
        Ini.Create(My.Application.Info.DirectoryPath & "\launch.ini")
        Dim _url = "http://" & Ini.Text("Server", "Address") & "/trinity/"
        frmProgress.lblStatus.Text = "Downloading " & name & "..."
        frmProgress.pbProgress.Minimum = 0
        frmProgress.pbProgress.Maximum = 100
        frmProgress.Show()
        _downloader.DownloadFileWithProgress(_url & "Plugins/" & name, IO.Path.Combine(IO.Path.Combine(My.Application.Info.DirectoryPath, "Plugins"), name))
    End Sub
    Public Sub ReInstallPlugin(name As String)
        If Not IO.Directory.Exists(IO.Path.Combine(My.Application.Info.DirectoryPath, "Plugins")) Then
            IO.Directory.CreateDirectory(IO.Path.Combine(My.Application.Info.DirectoryPath, "Plugins"))
        End If
        Dim Ini As New Trinity.clsIni
        Ini.Create(My.Application.Info.DirectoryPath & "\launch.ini")
        Dim _url = "http://" & Ini.Text("Server", "Address") & "/trinity/"
        frmProgress.lblStatus.Text = "Downloading " & name & "..."
        frmProgress.pbProgress.Minimum = 0
        frmProgress.pbProgress.Maximum = 100
        frmProgress.Show()
        _downloader.DownloadFileWithProgress(_url & "Plugins/" & name, IO.Path.Combine(IO.Path.Combine(My.Application.Info.DirectoryPath, "Plugins"), name))
    End Sub

    Sub UninstallPlugin(name As String)
        Try
            IO.File.Delete(IO.Path.Combine(IO.Path.Combine(My.Application.Info.DirectoryPath, "Plugins"), name))
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("There was an error while uninstalling plugin '" & name & "':" & vbNewLine & vbNewLine & ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmPlugins_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        lstPlugins.Items.Clear()
        Dim _doc As XDocument = XDocument.Load(IO.Path.Combine(My.Application.Info.DirectoryPath, "versions.xml"))
        For Each _plugin As XElement In _doc.<data>.<Plugins>...<Plugin>
            If _plugin.@Developer = "0" OrElse TrinitySettings.Developer Then
                Dim _p As New Plugin
                _p.Label = _plugin.@Label
                _p.Name = _plugin.@Name
                Dim _idx = lstPlugins.Items.Add(_p)
                lstPlugins.SetItemChecked(_idx, IO.File.Exists(IO.Path.Combine(IO.Path.Combine(My.Application.Info.DirectoryPath, "Plugins"), _p.Name)))
            End If
        Next
    End Sub

    Private Sub _downloader_AmountDownloadedChanged(iNewProgress As Long) Handles _downloader.AmountDownloadedChanged


        frmProgress.pbProgress.Value = iNewProgress
    End Sub

    Private Sub _downloader_FileDownloadComplete() Handles _downloader.FileDownloadComplete
        frmProgress.Hide()
    End Sub

    Private Sub btnReInstallPlugin_Click(sender As Object, e As EventArgs) Handles btnReInstallPlugin.Click
        Dim _hasChanged As Boolean = False
        For Each _p As Plugin In lstPlugins.Items
            If lstPlugins.CheckedItems.Contains(_p)                
                ReInstallPlugin(_p.Name)
                _hasChanged = True
            End If
            If _hasChanged Then
                frmProgress.Hide()
                If Windows.Forms.MessageBox.Show("These changes will take effect next time you start Trinity." & vbNewLine & vbNewLine & "Do you want to restart Trinity now?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Shell(IO.Path.Combine(My.Application.Info.DirectoryPath, "launchtrinity.exe"), AppWinStyle.NormalFocus)
                    End
                End If
            End If      
        Next      
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class