Public Class frmDownload
    Dim WithEvents client As New System.Net.WebClient

    Private filelist As List(Of String)
    Private server As String

    WithEvents _Downloader As New WebFileDownloader

    Public Sub setVariables(ByVal files As List(Of String), ByVal serverString As String)
        filelist = files
        server = serverString
    End Sub

    Private Sub Download()
        'make sure the tmp files dont exists
        If IO.File.Exists(My.Application.Info.DirectoryPath & "\" & filelist(0).Substring(0, filelist(0).Length - 1) & "_") Then
            IO.File.Delete(My.Application.Info.DirectoryPath & "\" & filelist(0).Substring(0, filelist(0).Length - 1) & "_")
        End If

        lblFile.Text = "Found updated files!" & vbCrLf & vbCrLf & "Downloading: " & filelist(0)

        'http://" & Ini.Text("Server", "Address") & "/trinity/

        Try
            _Downloader.DownloadFileWithProgress(server & filelist(0), My.Application.Info.DirectoryPath & "\" & filelist(0).Substring(0, filelist(0).Length - 1) & "_")
            'client.DownloadDataAsync(New Uri(server & filelist(0).Substring(0, filelist(0).Length - 1) & "_"), My.Application.Info.DirectoryPath & "\" & filelist(0).Substring(0, filelist(0).Length - 1) & "_")
        Catch
            Windows.Forms.MessageBox.Show("Could not download files.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub frmDownload_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Download()
    End Sub
    Private Sub frmDownload_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    'FIRES WHEN WE HAVE GOTTEN THE DOWNLOAD SIZE, SO WE KNOW WHAT BOUNDS TO GIVE THE PROGRESS BAR
    Private Sub _Downloader_FileDownloadSizeObtained(ByVal FileSize As Long) Handles _Downloader.FileDownloadSizeObtained
        pbProgress.Value = 0
        pbProgress.Maximum = Convert.ToInt32(FileSize)
    End Sub

    'FIRES WHEN DOWNLOAD IS COMPLETE
    Private Sub _Downloader_FileDownloadComplete() Handles _Downloader.FileDownloadComplete
        pbProgress.Value = pbProgress.Maximum

        Try
            If IO.File.Exists(My.Application.Info.DirectoryPath & "\" & filelist(0)) Then
                IO.File.Delete(My.Application.Info.DirectoryPath & "\" & filelist(0))
            End If
            Rename(My.Application.Info.DirectoryPath & "\" & filelist(0).Substring(0, filelist(0).Length - 1) & "_", My.Application.Info.DirectoryPath & "\" & filelist(0))

            'if we have the updates text list we display it aswell
            If filelist(0) = "updates.txt" Then
                Process.Start(My.Application.Info.DirectoryPath & "\updates.txt")
            End If

            filelist.RemoveAt(0)
            If Not filelist.Count = 0 Then
                Download()
                Exit Sub
            End If
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("Error while updating(" & filelist(0) & ")")
            'no additions is required
        End Try

        If IO.File.Exists(My.Application.Info.DirectoryPath & "\version.xml") Then
            IO.File.Delete(My.Application.Info.DirectoryPath & "\version.xml")
        End If

        IO.File.Copy(My.Application.Info.DirectoryPath & "\versions.xml", My.Application.Info.DirectoryPath & "\version.xml")
        Process.Start(My.Application.Info.DirectoryPath & "\Trinity.exe")
        System.Environment.Exit(1)
    End Sub

    'FIRES WHEN DOWNLOAD FAILES. PASSES IN EXCEPTION INFO
    Private Sub _Downloader_FileDownloadFailed(ByVal ex As System.Exception) Handles _Downloader.FileDownloadFailed
        MessageBox.Show("An error has occured during download: " & ex.Message)
    End Sub

    'FIRES WHEN MORE OF THE FILE HAS BEEN DOWNLOADED
    Private Sub _Downloader_AmountDownloadedChanged(ByVal iNewProgress As Long) Handles _Downloader.AmountDownloadedChanged
        pbProgress.Value = Convert.ToInt32(iNewProgress)
        'lblProgress.Text = WebFileDownloader.FormatFileSize(iNewProgress) & " of " & WebFileDownloader.FormatFileSize(ProgBar.Maximum) & " downloaded"
        Application.DoEvents()
    End Sub
End Class
