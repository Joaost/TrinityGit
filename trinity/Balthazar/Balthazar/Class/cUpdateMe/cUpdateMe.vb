Imports System.Xml

Public Class cUpdateMe
    Private _dlls As New List(Of String)
    Private _appName As String = ""
    Private _remoteLocation As String = ""
    Private _localLocation As String = My.Application.Info.DirectoryPath
    Private WithEvents _updateTimer As Timer
    Private _autoUpdateInterval As Integer = 5 * 60000
    Private _mainForm As Object
    Public Event FoundNewVersion()

    Public ReadOnly Property CurrentAppVersion() As Integer
        Get
            Dim xmlDocClient As New XmlDocument
            xmlDocClient.Load(_localLocation & "version.xml")
            Dim xmlClientList As Xml.XmlElement
            xmlClientList = xmlDocClient.SelectSingleNode("//LauncherFiles")
            Return DirectCast(xmlClientList.SelectSingleNode("//File[@Name='" & _appName & "']"), XmlElement).GetAttribute("Version")
        End Get
    End Property

    Property AutoUpdateInterval() As Integer
        Get
            Return _autoUpdateInterval
        End Get
        Set(ByVal value As Integer)
            _autoUpdateInterval = value
            If _updateTimer IsNot Nothing Then
                _updateTimer.Interval = value
            End If
        End Set
    End Property

    Public Property CheckAutomaticallyForUpdates() As Boolean
        Get
            If _updateTimer Is Nothing Then
                Return False
            End If
            Return _updateTimer.Enabled
        End Get
        Set(ByVal value As Boolean)
            If _updateTimer Is Nothing Then
                _updateTimer = New Timer
                _updateTimer.Interval = _autoUpdateInterval
            End If
            _updateTimer.Enabled = value
        End Set
    End Property


    Public Sub New(ByVal ApplicationName As String, ByVal RemoteLocation As String, Optional ByVal LocalLocation As String = "")
        _remoteLocation = RemoteLocation
        '_mainForm = MainForm
        If Not _remoteLocation.Contains(":") Then
            _remoteLocation = "http://" & _remoteLocation
        End If
        If Not _remoteLocation.EndsWith("/") Then
            _remoteLocation &= "/"
        End If
        If LocalLocation <> "" Then _localLocation = LocalLocation
        If Not _localLocation.EndsWith("\") Then
            _localLocation &= "\"
        End If

        _appName = ApplicationName
    End Sub

    Public Property DLLs() As List(Of String)
        Get
            Return _dlls
        End Get
        Set(ByVal value As List(Of String))
            _dlls = value
        End Set
    End Property

    Public Sub GetNewVersion()
        'If a new version is just downloaded then the reload argument will be set
        If My.Application.CommandLineArgs.Contains("reload") Then
            Kill(_localLocation & _appName)
            My.Computer.FileSystem.CopyFile(_localLocation & "_" & _appName, _localLocation & _appName)
            Process.Start(_localLocation & _appName)
            System.Environment.Exit(1)
        End If
        Try
            If My.Computer.FileSystem.FileExists(_localLocation & "_" & _appName) Then
                'Wait for _Matrix.exe to quit
                While Diagnostics.Process.GetProcessesByName("_" & _appName).Count > 0
                    My.Application.DoEvents()
                End While
                Kill(_localLocation & "_" & _appName)
            End If
        Catch

        End Try

        'Check for latest version

        If Not My.Application.CommandLineArgs.Contains("nocheck") Then
            Try
                Dim fileList As New List(Of String)
                Dim client As New System.Net.WebClient

                'download the file
                client.DownloadFile(New Uri(_remoteLocation & "version.xml"), My.Application.Info.DirectoryPath & "\_version.xml")

                'get the server version (it was downloaded by the launcher
                Dim xmlDocServer As New Xml.XmlDocument
                xmlDocServer.Load(_localLocation & "_version.xml")

                Dim xmlServerList As Xml.XmlElement
                xmlServerList = xmlDocServer.SelectSingleNode("//LauncherFiles")

                'if we have no old we download all files, if not we check for versions
                If System.IO.File.Exists(_localLocation & "version.xml") Then

                    'get the current version on the client
                    Dim xmlDocClient As New Xml.XmlDocument
                    xmlDocClient.Load(_localLocation & "version.xml")

                    Dim xmlClientList As Xml.XmlElement
                    xmlClientList = xmlDocClient.SelectSingleNode("//LauncherFiles")

                    'go through all files in the server list
                    For Each xe As Xml.XmlElement In xmlServerList.ChildNodes
                        'check the old xml for the same file, if it exits we check the version number
                        If xmlClientList.SelectSingleNode("//File[@Name='" & xe.GetAttribute("Name") & "']") Is Nothing Then
                            fileList.Add(xe.GetAttribute("Name"))
                        Else
                            If DirectCast(xmlClientList.SelectSingleNode("//File[@Name='" & xe.GetAttribute("Name") & "']"), XmlElement).GetAttribute("Version") < xe.GetAttribute("Version") Then
                                fileList.Add(xe.GetAttribute("Name"))
                            End If
                        End If
                    Next
                Else
                    'go through all files in the server list
                    For Each xe As Xml.XmlElement In xmlServerList.ChildNodes
                        fileList.Add(xe.GetAttribute("Name"))
                    Next
                End If

                If fileList.Count > 0 Then
                    frmDownload.setVariables(_appName, fileList, _remoteLocation, _localLocation, _dlls)
                    frmDownload.Show()
                End If
            Catch ex As Exception
                Windows.Forms.MessageBox.Show("Could not check for new versions." & vbCrLf & vbCrLf & "Message:" & vbCrLf & ex.Message, "M A T R I X", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
            If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\_version.xml") Then
                Kill(My.Application.Info.DirectoryPath & "\_version.xml")
            End If
        End If
    End Sub

    Friend ReadOnly Property ApplicationName() As String
        Get
            Return _appName
        End Get
    End Property

    Friend Sub EndThread(ByVal NewVersion As Boolean)
        If NewVersion Then
            RaiseEvent FoundNewVersion()
        End If
    End Sub

    Private Sub _updateTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _updateTimer.Tick
        Dim t As New cCheckNewVersion(Me)
        Dim thdNewVersion As New System.Threading.Thread(AddressOf t.Run)
        thdNewVersion.Start(New Object() {_remoteLocation, _localLocation})
    End Sub


    Private Class cCheckNewVersion
        Dim _parent As cUpdateMe

        Dim WithEvents client As New System.Net.WebClient

        Delegate Sub EndThread(ByVal IsNewVersion As Boolean)

        Public Sub New(ByVal Parent As cUpdateMe)
            _parent = Parent
        End Sub

        Public Sub Run(ByVal Params As Object)
            Dim RemoteLocation As String = Params(0)
            Dim LocalLocation As String = Params(1)

            'check for a newer version
            Dim intMyVersion As Integer
            Dim intLatestVersion As Integer

            Try
                If LocalLocation = "" Then LocalLocation = My.Application.Info.DirectoryPath
                If Not LocalLocation.EndsWith("\") Then
                    LocalLocation &= "\"
                End If

                If Not RemoteLocation.Contains(":") Then
                    RemoteLocation = "http://" & RemoteLocation
                End If
                If Not RemoteLocation.EndsWith("/") Then
                    RemoteLocation &= "/"
                End If

                If RemoteLocation.Substring(0, 4).ToUpper = "HTTP" Then
                    Dim data As IO.Stream = client.OpenRead(RemoteLocation & "version.xml")
                    Dim sw As New IO.StreamReader(data, True)

                    'get the server version (it was downloaded by the launcher
                    Dim xmlDocServer As New Xml.XmlDocument
                    xmlDocServer.LoadXml(sw.ReadToEnd)

                    intLatestVersion = DirectCast(xmlDocServer.SelectSingleNode("//File[@Name='" & _parent.ApplicationName & "']"), XmlElement).GetAttribute("Version")

                    If My.Computer.FileSystem.FileExists(LocalLocation & "version.xml") Then
                        xmlDocServer.Load(LocalLocation & "version.xml")

                        intMyVersion = DirectCast(xmlDocServer.SelectSingleNode("//File[@Name='" & _parent.ApplicationName & "']"), XmlElement).GetAttribute("Version")
                    Else
                        intMyVersion = 0
                    End If

                Else 'end of web and start of network procedure

                    'get the server version (it was downloaded by the launcher
                    Dim xmlDocServer As New Xml.XmlDocument
                    xmlDocServer.Load(RemoteLocation & "version.xml")

                    intLatestVersion = DirectCast(xmlDocServer.SelectSingleNode("//File[@Name='" & _parent.ApplicationName & "']"), XmlElement).GetAttribute("Version")

                    If My.Computer.FileSystem.FileExists(LocalLocation & "version.xml") Then
                        xmlDocServer.Load(LocalLocation & "version.xml")

                        intMyVersion = DirectCast(xmlDocServer.SelectSingleNode("//File[@Name='" & _parent.ApplicationName & "']"), XmlElement).GetAttribute("Version")
                    Else
                        intMyVersion = 0
                    End If

                End If


                If intMyVersion < intLatestVersion Then
                    _parent.EndThread(True)
                    '_parent.Invoke(New EndThread(AddressOf _parent.EndThread), True)
                Else
                    _parent.EndThread(False)
                    '_parent.Invoke(New EndThread(AddressOf _parent.EndThread), False)
                End If

            Catch
                _parent.EndThread(False)
                '_parent.Invoke(New EndThread(AddressOf _parent.EndThread), False)
            End Try
        End Sub
    End Class

End Class
