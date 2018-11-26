Imports System.IO

Public Class cFTP
    Dim strUser As String
    Dim strPWD As String
    Dim strAdress As String

    Public Property userName() As String
        Get
            Return strUser
        End Get
        Set(ByVal value As String)
            strUser = value
        End Set
    End Property

    Public Property password() As String
        Get
            Return strPWD
        End Get
        Set(ByVal value As String)
            strPWD = value
        End Set
    End Property

    Public Property ftpAdress() As String
        Get
            Return strAdress
        End Get
        Set(ByVal value As String)
            'check the format on the adress
            'a correct format is ftp://host/
            If value.Length > 5 Then
                If value.Substring(4, 2) = "//" Then
                    strAdress = value
                Else
                    strAdress = "ftp://" & value
                End If
                If Not strAdress.Substring(strAdress.Length - 1) = "/" Then
                    strAdress = strAdress & "/"
                End If
            Else
                strAdress = Nothing
            End If
        End Set
    End Property

    Public Function uploadFile(ByVal fileWithPath As String, Optional ByVal pathOnServer As String = "", Optional ByVal newFileName As String = "") As Boolean
        Try
            'get the file name
            Dim strFileName As String
            strFileName = fileWithPath.Substring(fileWithPath.LastIndexOf("\") + 1)

            'creates server path
            Dim CompleteFTPPath As String
            CompleteFTPPath = strAdress

            'make sure the server path has the right format if it exists
            If Not pathOnServer = "" Then
                If pathOnServer.Substring(0, 1) = "/" Then
                    pathOnServer = pathOnServer.Substring(1)
                End If
                If Not pathOnServer.Substring(pathOnServer.Length - 1) = "/" Then
                    pathOnServer = pathOnServer & "/"
                End If

                'adds the path to the ftp path
                CompleteFTPPath = CompleteFTPPath & pathOnServer
            End If

            'check if a new file naem should apply
            If newFileName = "" Then
                'adds the file to the ftp path
                CompleteFTPPath = CompleteFTPPath & strFileName
            Else
                'adds the file to the ftp path
                CompleteFTPPath = CompleteFTPPath & newFileName
            End If

            'Create a FTP Request Object and Specfiy a Complete Path
            Dim reqObj As Net.FtpWebRequest = Net.WebRequest.Create(CompleteFTPPath)

            'Call A FileUpload Method of FTP Request Object
            reqObj.Method = Net.WebRequestMethods.Ftp.UploadFile

            'If you want to access Resourse Protected You need to give User Name and PWD
            If Not strUser Is Nothing AndAlso Not strPWD Is Nothing Then
                reqObj.Credentials = New Net.NetworkCredential(strUser, strPWD)
            End If

            'FileStream object read file from Local Drive
            Dim streamObj As IO.FileStream = System.IO.File.OpenRead(fileWithPath)

            'Store File in Buffer
            Dim buffer(streamObj.Length) As Byte

            'Read File from Buffer
            streamObj.Read(buffer, 0, buffer.Length)

            'Close FileStream Object Set its Value to nothing
            streamObj.Close()
            streamObj = Nothing

            'Upload File to ftp://localHost/ set its object to nothing
            reqObj.ContentLength = buffer.Length
            Dim s As Stream = reqObj.GetRequestStream
            s.Write(buffer, 0, buffer.Length)
            s.Close()

            Dim response As System.Net.FtpWebResponse = reqObj.GetResponse()

            reqObj = Nothing

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Function downloadFile(ByVal fileNameWithSubfolders As String, ByVal LocalPath As String, Optional ByVal newFileName As String = "") As Boolean
        Try
            'get the file name
            Dim strFileName As String
            strFileName = fileNameWithSubfolders.Substring(fileNameWithSubfolders.LastIndexOf("/") + 1)

            'creates server path
            Dim CompleteFTPPath As String
            CompleteFTPPath = strAdress

            'make sure the server path has the right format if it exists

            If fileNameWithSubfolders.Substring(0, 1) = "/" Then
                fileNameWithSubfolders = fileNameWithSubfolders.Substring(1)
            End If

            'adds the path to the ftp path
            CompleteFTPPath = CompleteFTPPath & fileNameWithSubfolders

            If newFileName = "" Then
                LocalPath = LocalPath & strFileName
            Else
                LocalPath = LocalPath & newFileName
            End If

            'Create a FTP Request Object and Specfiy a Complete Path
            Dim reqObj As Net.FtpWebRequest = Net.WebRequest.Create(CompleteFTPPath)

            'Call A FileUpload Method of FTP Request Object
            reqObj.Method = Net.WebRequestMethods.Ftp.DownloadFile

            'If you want to access Resourse Protected You need to give User Name and PWD
            If Not strUser Is Nothing AndAlso Not strPWD Is Nothing Then
                reqObj.Credentials = New Net.NetworkCredential(strUser, strPWD)
            End If

            reqObj.KeepAlive = False
            'want binary data not text
            reqObj.UseBinary = True

            'Get the response to the Ftp request and the associated stream
            Using response As System.Net.FtpWebResponse = CType(reqObj.GetResponse, System.Net.FtpWebResponse)
                Using responseStream As IO.Stream = response.GetResponseStream
                    'loop to read & write to file
                    Using fs As New IO.FileStream(LocalPath, IO.FileMode.Create)
                        Dim buffer(2047) As Byte
                        Dim read As Integer = 0
                        Do
                            read = responseStream.Read(buffer, 0, buffer.Length)
                            fs.Write(buffer, 0, read)
                        Loop Until read = 0
                        responseStream.Close()
                        fs.Flush()
                        fs.Close()
                    End Using
                    responseStream.Close()
                End Using
                response.Close()
            End Using

            reqObj = Nothing

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CreateDir(ByVal newDir As String, Optional ByVal ServerPath As String = "") As Boolean
        Try
            'creates server path
            Dim CompleteFTPPath As String
            CompleteFTPPath = strAdress

            If Not ServerPath = "" Then
                If ServerPath.Substring(0, 1) = "/" Then
                    ServerPath = ServerPath.Substring(1)
                End If
                If Not CompleteFTPPath.Substring(CompleteFTPPath.Length - 1) = "/" Then
                    CompleteFTPPath = CompleteFTPPath & "/"
                End If
                CompleteFTPPath = CompleteFTPPath & ServerPath
            End If

            'add the new dir
            CompleteFTPPath = CompleteFTPPath & newDir

            'Create a FTP Request Object and Specfiy a Complete Path
            Dim reqObj As Net.FtpWebRequest = Net.WebRequest.Create(CompleteFTPPath)

            'set type of call 
            reqObj.Method = Net.WebRequestMethods.Ftp.MakeDirectory

            'If you want to access Resourse Protected You need to give User Name and PWD
            If Not strUser Is Nothing AndAlso Not strPWD Is Nothing Then
                reqObj.Credentials = New Net.NetworkCredential(strUser, strPWD)
            End If

            Dim response As System.Net.FtpWebResponse = reqObj.GetResponse

            reqObj = Nothing

            Return True
        Catch
            Return False
        End Try

    End Function

    Public Function deleteDir(ByVal dir As String, Optional ByVal ServerPath As String = "") As Boolean
        Try
            'creates server path
            Dim CompleteFTPPath As String
            CompleteFTPPath = strAdress

            If ServerPath = "" Then
                CompleteFTPPath = CompleteFTPPath & dir

            Else
                CompleteFTPPath = CompleteFTPPath & ServerPath
                If Not CompleteFTPPath.Substring(CompleteFTPPath.Length - 1) = "/" Then
                    CompleteFTPPath = CompleteFTPPath & "/"
                End If
                CompleteFTPPath = CompleteFTPPath & dir
            End If

            If Not CompleteFTPPath.Substring(CompleteFTPPath.Length - 1) = "/" Then
                CompleteFTPPath = CompleteFTPPath & "/"
            End If

            'Create a FTP Request Object and Specfiy a Complete Path
            Dim reqObj As Net.FtpWebRequest = Net.WebRequest.Create(CompleteFTPPath)

            'If you want to access Resourse Protected You need to give User Name and PWD
            If Not strUser Is Nothing AndAlso Not strPWD Is Nothing Then
                reqObj.Credentials = New Net.NetworkCredential(strUser, strPWD)
            End If

            reqObj.Method = System.Net.WebRequestMethods.Ftp.RemoveDirectory

            Dim response As System.Net.FtpWebResponse = reqObj.GetResponse

            reqObj = Nothing

            Return True
        Catch
            Return False
        End Try


    End Function

    Public Function dirExists(ByVal Dir As String, Optional ByVal ServerPath As String = "") As Boolean
        Try
            'creates server path
            Dim CompleteFTPPath As String
            CompleteFTPPath = strAdress

            If Not ServerPath = "" Then
                If ServerPath.Substring(0, 1) = "/" Then
                    ServerPath = ServerPath.Substring(1)
                End If
                If Not CompleteFTPPath.Substring(CompleteFTPPath.Length - 1) = "/" Then
                    CompleteFTPPath = CompleteFTPPath & "/"
                End If

                CompleteFTPPath = CompleteFTPPath & ServerPath

                If Not CompleteFTPPath.Substring(CompleteFTPPath.Length - 1) = "/" Then
                    CompleteFTPPath = CompleteFTPPath & "/"
                End If
            End If

            'add the new dir
            CompleteFTPPath = CompleteFTPPath & Dir

            'Create a FTP Request Object and Specfiy a Complete Path
            Dim reqObj As Net.FtpWebRequest = Net.WebRequest.Create(CompleteFTPPath)

            'set type of call 
            reqObj.Method = Net.WebRequestMethods.Ftp.ListDirectory

            'If you want to access Resourse Protected You need to give User Name and PWD
            If Not strUser Is Nothing AndAlso Not strPWD Is Nothing Then
                reqObj.Credentials = New Net.NetworkCredential(strUser, strPWD)
            End If

            Dim response As System.Net.FtpWebResponse = reqObj.GetResponse


            reqObj = Nothing

            Return True
        Catch
            Return False
        End Try
    End Function

    Public Function deleteFile(ByVal fileName As String, Optional ByVal ServerPath As String = "") As Boolean
        Try
            'creates server path
            Dim CompleteFTPPath As String
            CompleteFTPPath = strAdress

            If Not ServerPath = "" Then
                If ServerPath.Substring(0, 1) = "/" Then
                    ServerPath = ServerPath.Substring(1)
                End If
                CompleteFTPPath = CompleteFTPPath & ServerPath
            End If

            'add the file name
            CompleteFTPPath = CompleteFTPPath & fileName

            'Create a FTP Request Object and Specfiy a Complete Path
            Dim reqObj As Net.FtpWebRequest = Net.WebRequest.Create(CompleteFTPPath)

            'set type of call 
            reqObj.Method = Net.WebRequestMethods.Ftp.DeleteFile

            'If you want to access Resourse Protected You need to give User Name and PWD
            If Not strUser Is Nothing AndAlso Not strPWD Is Nothing Then
                reqObj.Credentials = New Net.NetworkCredential(strUser, strPWD)
            End If

            Dim response As System.Net.WebResponse = reqObj.GetResponse

            reqObj = Nothing

            Return True
        Catch
            Return False
        End Try

    End Function

    Public Function fileExists(ByVal fileNameWithServerPath As String) As Boolean
        Try
            'creates server path
            Dim CompleteFTPPath As String
            CompleteFTPPath = strAdress

            If fileNameWithServerPath.Substring(0, 1) = "/" Then
                fileNameWithServerPath = fileNameWithServerPath.Substring(1)
            End If

            CompleteFTPPath = CompleteFTPPath & fileNameWithServerPath


            'Create a FTP Request Object and Specfiy a Complete Path
            Dim reqObj As Net.FtpWebRequest = Net.WebRequest.Create(CompleteFTPPath)

            'set type of call 
            reqObj.Method = Net.WebRequestMethods.Ftp.GetFileSize

            'If you want to access Resourse Protected You need to give User Name and PWD
            If Not strUser Is Nothing AndAlso Not strPWD Is Nothing Then
                reqObj.Credentials = New Net.NetworkCredential(strUser, strPWD)
            End If

            'the getresponse will fail if there is not file and the function will be continue in catch (return false)
            Dim response As System.Net.WebResponse = reqObj.GetResponse

            reqObj = Nothing

            Return True
        Catch
            Return False
        End Try
    End Function
End Class
