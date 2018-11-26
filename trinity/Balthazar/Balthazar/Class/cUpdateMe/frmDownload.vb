Imports System.Net
Imports System.IO

Public Class frmDownload
    Dim WithEvents client As New System.Net.WebClient

    Private filelist As List(Of String)
    Private server As String
    Private local As String
    Private AppName As String
    Private _dlls As List(Of String)
    WithEvents _Downloader As New WebFileDownloader

    Public Sub setVariables(ByVal ApplicationName As String, ByVal files As List(Of String), ByVal RemoteLocation As String, Optional ByVal LocalLocation As String = "", Optional ByVal DLLs As List(Of String) = Nothing)
        filelist = files
        server = RemoteLocation
        If LocalLocation = "" Then
            local = My.Application.Info.DirectoryPath
        End If
        AppName = ApplicationName
        _dlls = DLLs
    End Sub

    Private Sub Download()
        'make sure the tmp files dont exists
        If IO.File.Exists(local & "_" & filelist(0)) Then
            IO.File.Delete(local & "_" & filelist(0))
        End If

        lblFile.Text = "Found updated files!" & vbCrLf & vbCrLf & "Downloading: " & filelist(0)

        'http://" & Ini.Text("Server", "Address") & "/trinity/

        Try
            _Downloader.DownloadFileWithProgress(server & filelist(0), local & "_" & filelist(0))
            'client.DownloadDataAsync(New Uri(server & filelist(0).Substring(0, filelist(0).Length - 1) & "_"), local &"" & filelist(0).Substring(0, filelist(0).Length - 1) & "_")
        Catch
            Windows.Forms.MessageBox.Show("Could not download file " & filelist(0), "Application update", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            'Check if it's a new dll, and if it is register it with GAC
            If _dlls.Contains(filelist(0)) Then
                If My.Computer.FileSystem.FileExists(local & filelist(0)) Then
                    Kill(local & filelist(0))
                End If
                My.Computer.FileSystem.RenameFile(local & "_" & filelist(0), filelist(0))
                Dim TmpPub As New System.EnterpriseServices.Internal.Publish
                TmpPub.GacRemove(local & "Matrix40.dll")
                TmpPub.GacInstall(local & "Matrix40.dll")
                Kill(local & "" & filelist(0))
            ElseIf Not filelist(0) = AppName Then
                If IO.File.Exists(local & "" & filelist(0)) Then
                    IO.File.Delete(local & "" & filelist(0))
                End If
                Rename(local & "_" & filelist(0), local & "" & filelist(0))
            End If

            'if we have the updates text list we display it aswell
            If filelist(0) = "Updates.txt" Then
                Process.Start(local & "Updates.txt")
            End If

            filelist.RemoveAt(0)
            If Not filelist.Count = 0 Then
                Download()
                Exit Sub
            End If
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("Error while updating " & filelist(0) & vbCrLf & vbCrLf & ex.Message, "M A T R I X", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'no additions is required
        End Try

        If IO.File.Exists(local & "version.xml") Then
            IO.File.Delete(local & "version.xml")
        End If

        IO.File.Copy(local & "_version.xml", local & "version.xml")
        Kill(local & "_version.xml")
        If Not My.Computer.FileSystem.FileExists(local & "_" & AppName) Then
            My.Computer.FileSystem.CopyFile(local & AppName, local & "_" & AppName)
        End If

        Process.Start(local & "_" & AppName, "reload")
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

    Private Class WebFileDownloader

        Public Event AmountDownloadedChanged(ByVal iNewProgress As Long)
        Public Event FileDownloadSizeObtained(ByVal iFileSize As Long)
        Public Event FileDownloadComplete()
        Public Event FileDownloadFailed(ByVal ex As Exception)

        Private mCurrentFile As String = String.Empty

        Public ReadOnly Property CurrentFile() As String
            Get
                Return mCurrentFile
            End Get
        End Property
        Public Function DownloadFile(ByVal URL As String, ByVal Location As String) As Boolean
            Try
                mCurrentFile = GetFileName(URL)
                Dim WC As New WebClient
                WC.DownloadFile(URL, Location)
                RaiseEvent FileDownloadComplete()
                Return True
            Catch ex As Exception
                RaiseEvent FileDownloadFailed(ex)
                Return False
            End Try
        End Function

        Private Function GetFileName(ByVal URL As String) As String
            Try
                Return URL.Substring(URL.LastIndexOf("/") + 1)
            Catch ex As Exception
                Return URL
            End Try
        End Function
        Public Function DownloadFileWithProgress(ByVal URL As String, ByVal Location As String) As Boolean
            Dim FS As FileStream = Nothing
            Try
                mCurrentFile = GetFileName(URL)
                Dim wRemote As WebRequest
                Dim bBuffer As Byte()
                ReDim bBuffer(256)
                Dim iBytesRead As Integer
                Dim iTotalBytesRead As Integer

                FS = New FileStream(Location, FileMode.Create, FileAccess.Write)
                wRemote = WebRequest.Create(URL)
                Dim myWebResponse As WebResponse = wRemote.GetResponse
                RaiseEvent FileDownloadSizeObtained(myWebResponse.ContentLength)
                Dim sChunks As Stream = myWebResponse.GetResponseStream
                Do
                    iBytesRead = sChunks.Read(bBuffer, 0, 256)
                    FS.Write(bBuffer, 0, iBytesRead)
                    iTotalBytesRead += iBytesRead
                    If myWebResponse.ContentLength < iTotalBytesRead Then
                        RaiseEvent AmountDownloadedChanged(myWebResponse.ContentLength)
                    Else
                        RaiseEvent AmountDownloadedChanged(iTotalBytesRead)
                    End If
                Loop While Not iBytesRead = 0
                sChunks.Close()
                FS.Close()
                RaiseEvent FileDownloadComplete()
                Return True
            Catch ex As Exception
                If Not (FS Is Nothing) Then
                    FS.Close()
                    FS = Nothing
                End If
                RaiseEvent FileDownloadFailed(ex)
                Return False
            End Try
        End Function

        Public Shared Function FormatFileSize(ByVal Size As Long) As String
            Try
                Dim KB As Integer = 1024
                Dim MB As Integer = KB * KB
                ' Return size of file in kilobytes.
                If Size < KB Then
                    Return (Size.ToString("D") & " bytes")
                Else
                    Select Case Size / KB
                        Case Is < 1000
                            Return (Size / KB).ToString("N") & "KB"
                        Case Is < 1000000
                            Return (Size / MB).ToString("N") & "MB"
                        Case Is < 10000000
                            Return (Size / MB / KB).ToString("N") & "GB"
                        Case Is >= 10000000
                            Return (Size / MB / KB).ToString("N") & "GB"
                    End Select
                End If
            Catch ex As Exception
                Return Size.ToString
            End Try
        End Function
    End Class


End Class
