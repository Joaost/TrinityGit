Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports Microsoft.VisualBasic.Compatibility
Imports System.ComponentModel
Imports VB6 = Microsoft.VisualBasic.Compatibility.VB6.Support
Imports System.Xml
Imports Microsoft
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Security.Principal
Imports System.IO
Imports System.Security.AccessControl


Public Class clsIni
    ' 
    <DllImport("KERNEL32.DLL", EntryPoint:="GetPrivateProfileIntA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Private Shared Function GetPrivateProfileInt(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal nDefault As Integer, ByVal lpFileName As String) As Integer
    End Function
    <DllImport("KERNEL32.DLL", EntryPoint:="WritePrivateProfileStringA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Private Shared Function WritePrivateProfileString(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    End Function
    <DllImport("KERNEL32.DLL", EntryPoint:="GetPrivateProfileStringA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Private Shared Function GetPrivateProfileString(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    End Function
    <DllImport("KERNEL32.DLL", EntryPoint:="WritePrivateProfileStructA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Private Shared Function WritePrivateProfileStruct(ByVal lpszSection As String, ByVal lpszKey As String, ByVal lpStruct() As Byte, ByVal uSizeStruct As Integer, ByVal szFile As String) As Integer
    End Function
    <DllImport("KERNEL32.DLL", EntryPoint:="GetPrivateProfileStructA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Private Shared Function GetPrivateProfileStruct(ByVal lpszSection As String, ByVal lpszKey As String, ByVal lpStruct() As Byte, ByVal uSizeStruct As Integer, ByVal szFile As String) As Integer
    End Function
    <DllImport("KERNEL32.DLL", EntryPoint:="GetPrivateProfileSectionNamesA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Private Shared Function GetPrivateProfileSectionNames(ByVal lpszReturnBuffer() As Byte, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    End Function
    <DllImport("KERNEL32.DLL", EntryPoint:="WritePrivateProfileSectionA", SetLastError:=False, CharSet:=CharSet.Ansi, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
    Private Shared Function WritePrivateProfileSection(ByVal lpAppName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    End Function
    ' 
    '/// Constructs a new INIReader object.
    '/// Specifies the INI file to use.
    Public Sub Create(ByVal File As String)
        Filename = File
    End Sub
    '/// Specifies the INI file to use.
    '/// A String representing the full path of the INI file.
    Public Property Filename() As String
        Get
            Return m_Filename
        End Get
        Set(ByVal Value As String)
            m_Filename = Value
        End Set
    End Property
    '/// Specifies the section you're working in. (aka 'the active section')
    '/// A String representing the section you're working in.
    Public Property Section() As String
        Get
            Return m_Section
        End Get
        Set(ByVal Value As String)
            m_Section = Value
        End Set
    End Property
    '/// Reads an Integer from the specified key of the specified section.
    '/// Specifies the section to search in.
    '/// Specifies the key from which to return the value.
    '/// Specifies the value to return if the specified key isn't found.
    '/// Returns the value of the specified Section/Key pair, or returns the default value if the specified Section/Key pair isn't found in the INI file.
    Public Overloads Function ReadInteger(ByVal Section As String, ByVal Key As String, ByVal DefVal As Integer) As Integer
        Try
            Return GetPrivateProfileInt(Section, Key, DefVal, Filename)
        Catch
            Return DefVal
        End Try
    End Function
    '/// Reads an Integer from the specified key of the specified section.
    '/// Specifies the section to search in.
    '/// Specifies the key from which to return the value.
    '/// Returns the value of the specified Section/Key pair, or returns 0 if the specified Section/Key pair isn't found in the INI file.
    Public Overloads Function ReadInteger(ByVal Section As String, ByVal Key As String) As Integer
        Return ReadInteger(Section, Key, 0)
    End Function
    '/// Reads an Integer from the specified key of the active section.
    '/// Specifies the key from which to return the value.
    '/// Specifies the section to search in.
    '/// Returns the value of the specified Key, or returns the default value if the specified Key isn't found in the active section of the INI file.
    Public Overloads Function ReadInteger(ByVal Key As String, ByVal DefVal As Integer) As Integer
        Return ReadInteger(Section, Key, DefVal)
    End Function
    '/// Reads an Integer from the specified key of the active section.
    '/// Specifies the key from which to return the value.
    '/// Returns the value of the specified Key, or returns 0 if the specified Key isn't found in the active section of the INI file.
    Public Overloads Function ReadInteger(ByVal Key As String) As Integer
        Return ReadInteger(Key, 0)
    End Function
    '/// Reads a String from the specified key of the specified section.
    '/// Specifies the section to search in.
    '/// Specifies the key from which to return the value.
    '/// Specifies the value to return if the specified key isn't found.
    '/// Returns the value of the specified Section/Key pair, or returns the default value if the specified Section/Key pair isn't found in the INI file.
    Public Overloads Function ReadString(ByVal Section As String, ByVal Key As String, ByVal DefVal As String) As String
        Try
            Dim sb As New StringBuilder(MAX_ENTRY)
            Dim Ret As Integer = GetPrivateProfileString(Section, Key, DefVal, sb, MAX_ENTRY, Filename)
            Return sb.ToString
        Catch
            Return DefVal
        End Try
    End Function
    '/// Reads a String from the specified key of the specified section.
    '/// Specifies the section to search in.
    '/// Specifies the key from which to return the value.
    '/// Returns the value of the specified Section/Key pair, or returns an empty String if the specified Section/Key pair isn't found in the INI file.
    Public Overloads Function ReadString(ByVal Section As String, ByVal Key As String) As String
        Return ReadString(Section, Key, "")
    End Function
    '/// Reads a String from the specified key of the active section.
    '/// Specifies the key from which to return the value.
    '/// Returns the value of the specified Key, or returns an empty String if the specified Key isn't found in the active section of the INI file.
    Public Overloads Function ReadString(ByVal Key As String) As String
        Return ReadString(Section, Key)
    End Function
    '/// Reads a Long from the specified key of the specified section.
    '/// Specifies the section to search in.
    '/// Specifies the key from which to return the value.
    '/// Specifies the value to return if the specified key isn't found.
    '/// Returns the value of the specified Section/Key pair, or returns the default value if the specified Section/Key pair isn't found in the INI file.
    Public Overloads Function ReadLong(ByVal Section As String, ByVal Key As String, ByVal DefVal As Long) As Long
        Return Long.Parse(ReadString(Section, Key, DefVal.ToString))
    End Function
    '/// Reads a Long from the specified key of the specified section.
    '/// Specifies the section to search in.
    '/// Specifies the key from which to return the value.
    '/// Returns the value of the specified Section/Key pair, or returns 0 if the specified Section/Key pair isn't found in the INI file.
    Public Overloads Function ReadLong(ByVal Section As String, ByVal Key As String) As Long
        Return ReadLong(Section, Key, 0)
    End Function
    '/// Reads a Long from the specified key of the active section.
    '/// Specifies the key from which to return the value.
    '/// Specifies the section to search in.
    '/// Returns the value of the specified Key, or returns the default value if the specified Key isn't found in the active section of the INI file.
    Public Overloads Function ReadLong(ByVal Key As String, ByVal DefVal As Long) As Long
        Return ReadLong(Section, Key, DefVal)
    End Function
    '/// Reads a Long from the specified key of the active section.
    '/// Specifies the key from which to return the value.
    '/// Returns the value of the specified Key, or returns 0 if the specified Key isn't found in the active section of the INI file.
    Public Overloads Function ReadLong(ByVal Key As String) As Long
        Return ReadLong(Key, 0)
    End Function
    '/// Reads a Byte array from the specified key of the specified section.
    '/// Specifies the section to search in.
    '/// Specifies the key from which to return the value.
    '/// Specifies the value to return if the specified key isn't found.
    '/// Returns the value of the specified Section/Key pair, or returns Nothing (Null in C#, C++.NET) if the specified Section/Key pair isn't found in the INI file.
    Public Function ReadByteArray(ByVal Section As String, ByVal Key As String, ByVal Length As Integer) As Byte()
        If Length > 0 Then
            Try
                Dim Buffer(Length - 1) As Byte
                If GetPrivateProfileStruct(Section, Key, Buffer, Buffer.Length, Filename) = 0 Then
                    Return Nothing
                Else
                    Return Buffer
                End If
            Catch
                Return Nothing
            End Try
        Else
            Return Nothing
        End If
    End Function
    '/// Reads a Byte array from the specified key of the active section.
    '/// Specifies the key from which to return the value.
    '/// Specifies the value to return if the specified key isn't found.
    '/// Returns the value of the specified Key, or returns Nothing (Null in C#, C++.NET) if the specified Key pair isn't found in the active section of the INI file.
    Public Function ReadByteArray(ByVal Key As String, ByVal Length As Integer) As Byte()
        Return ReadByteArray(Section, Key, Length)
    End Function
    '/// Reads a Boolean from the specified key of the specified section.
    '/// Specifies the section to search in.
    '/// Specifies the key from which to return the value.
    '/// Specifies the value to return if the specified key isn't found.
    '/// Returns the value of the specified Section/Key pair, or returns the default value if the specified Section/Key pair isn't found in the INI file.
    Public Overloads Function ReadBoolean(ByVal Section As String, ByVal Key As String, ByVal DefVal As Boolean) As Boolean
        Return Boolean.Parse(ReadString(Section, Key, DefVal.ToString))
    End Function
    '/// Reads a Boolean from the specified key of the specified section.
    '/// Specifies the section to search in.
    '/// Specifies the key from which to return the value.
    '/// Returns the value of the specified Section/Key pair, or returns False if the specified Section/Key pair isn't found in the INI file.
    Public Overloads Function ReadBoolean(ByVal Section As String, ByVal Key As String) As Boolean
        Return ReadBoolean(Section, Key, False)
    End Function
    '/// Reads a Boolean from the specified key of the specified section.
    '/// Specifies the key from which to return the value.
    '/// Specifies the value to return if the specified key isn't found.
    '/// Returns the value of the specified Key pair, or returns the default value if the specified Key isn't found in the active section of the INI file.
    Public Overloads Function ReadBoolean(ByVal Key As String, ByVal DefVal As Boolean) As Boolean
        Return ReadBoolean(Section, Key, DefVal)
    End Function
    '/// Reads a Boolean from the specified key of the specified section.
    '/// Specifies the key from which to return the value.
    '/// Returns the value of the specified Key, or returns False if the specified Key isn't found in the active section of the INI file.
    Public Overloads Function ReadBoolean(ByVal Key As String) As Boolean
        Return ReadBoolean(Section, Key)
    End Function
    '/// Writes an Integer to the specified key in the specified section.
    '/// Specifies the section to write in.
    '/// Specifies the key to write to.
    '/// Specifies the value to write.
    '/// Returns True if the function succeeds, False otherwise.
    Public Overloads Function Write(ByVal Section As String, ByVal Key As String, ByVal Value As Integer) As Boolean
        Try
            Return (WritePrivateProfileString(Section, Key, Value.ToString, Filename) <> 0)
        Catch
            Return False
        End Try
    End Function
    '/// Writes an Integer to the specified key in the active section.
    '/// Specifies the key to write to.
    '/// Specifies the value to write.
    '/// Returns True if the function succeeds, False otherwise.
    Public Overloads Function Write(ByVal Key As String, ByVal Value As Integer) As Boolean
        Return Write(Section, Key, Value)
    End Function
    '/// Writes a String to the specified key in the specified section.
    '/// Specifies the section to write in.
    '/// Specifies the key to write to.
    '/// Specifies the value to write.
    '/// Returns True if the function succeeds, False otherwise.
    Public Overloads Function Write(ByVal Section As String, ByVal Key As String, ByVal Value As String) As Boolean
        Try
            Return (WritePrivateProfileString(Section, Key, Value, Filename) <> 0)
        Catch
            Return False
        End Try
    End Function
    '/// Writes a String to the specified key in the active section.
    '/// Specifies the key to write to.
    '/// Specifies the value to write.
    '/// Returns True if the function succeeds, False otherwise.
    Public Overloads Function Write(ByVal Key As String, ByVal Value As String) As Boolean
        Return Write(Section, Key, Value)
    End Function
    '/// Writes a Long to the specified key in the specified section.
    '/// Specifies the section to write in.
    '/// Specifies the key to write to.
    '/// Specifies the value to write.
    '/// Returns True if the function succeeds, False otherwise.
    Public Overloads Function Write(ByVal Section As String, ByVal Key As String, ByVal Value As Long) As Boolean
        Return Write(Section, Key, Value.ToString)
    End Function
    '/// Writes a Long to the specified key in the active section.
    '/// Specifies the key to write to.
    '/// Specifies the value to write.
    '/// Returns True if the function succeeds, False otherwise.
    Public Overloads Function Write(ByVal Key As String, ByVal Value As Long) As Boolean
        Return Write(Section, Key, Value)
    End Function
    '/// Writes a Byte array to the specified key in the specified section.
    '/// Specifies the section to write in.
    '/// Specifies the key to write to.
    '/// Specifies the value to write.
    '/// Returns True if the function succeeds, False otherwise.
    Public Overloads Function Write(ByVal Section As String, ByVal Key As String, ByVal Value() As Byte) As Boolean
        Try
            Return (WritePrivateProfileStruct(Section, Key, Value, Value.Length, Filename) <> 0)
        Catch
            Return False
        End Try
    End Function
    '/// Writes a Byte array to the specified key in the active section.
    '/// Specifies the key to write to.
    '/// Specifies the value to write.
    '/// Returns True if the function succeeds, False otherwise.
    Public Overloads Function Write(ByVal Key As String, ByVal Value() As Byte) As Boolean
        Return Write(Section, Key, Value)
    End Function
    '/// Writes a Boolean to the specified key in the specified section.
    '/// Specifies the section to write in.
    '/// Specifies the key to write to.
    '/// Specifies the value to write.
    '/// Returns True if the function succeeds, False otherwise.
    Public Overloads Function Write(ByVal Section As String, ByVal Key As String, ByVal Value As Boolean) As Boolean
        Return Write(Section, Key, Value.ToString)
    End Function
    '/// Writes a Boolean to the specified key in the active section.
    '/// Specifies the key to write to.
    '/// Specifies the value to write.
    '/// Returns True if the function succeeds, False otherwise.
    Public Overloads Function Write(ByVal Key As String, ByVal Value As Boolean) As Boolean
        Return Write(Section, Key, Value)
    End Function
    '/// Deletes a key from the specified section.
    '/// Specifies the section to delete from.
    '/// Specifies the key to delete.
    '/// Returns True if the function succeeds, False otherwise.
    Public Function DeleteKey(ByVal Section As String, ByVal Key As String) As Boolean
        Try
            Return (WritePrivateProfileString(Section, Key, Nothing, Filename) <> 0)
        Catch
            Return False
        End Try
    End Function
    '/// Deletes a key from the active section.
    '/// Specifies the key to delete.
    '/// Returns True if the function succeeds, False otherwise.
    Public Function DeleteKey(ByVal Key As String) As Boolean
        Try
            Return (WritePrivateProfileString(Section, Key, Nothing, Filename) <> 0)
        Catch
            Return False
        End Try
    End Function
    '/// Deletes a section from an INI file.
    '/// Specifies the section to delete.
    '/// Returns True if the function succeeds, False otherwise.
    Public Function DeleteSection(ByVal Section As String) As Boolean
        Try
            Return WritePrivateProfileSection(Section, Nothing, Filename) <> 0
        Catch
            Return False
        End Try
    End Function
    '/// Retrieves a list of all available sections in the INI file.
    '/// Returns an ArrayList with all available sections.
    Public Function GetSectionNames() As ArrayList
        GetSectionNames = New ArrayList
        Dim Buffer(MAX_ENTRY) As Byte
        Dim BuffStr As String
        Dim PrevPos As Integer = 0
        Dim Length As Integer
        Try
            Length = GetPrivateProfileSectionNames(Buffer, MAX_ENTRY, Filename)
        Catch
            Exit Function
        End Try
        Dim ASCII As New ASCIIEncoding
        If Length > 0 Then
            BuffStr = ASCII.GetString(Buffer)
            Length = 0
            PrevPos = -1
            Do
                Length = BuffStr.IndexOf(ControlChars.NullChar, PrevPos + 1)
                If Length - PrevPos = 1 OrElse Length = -1 Then Exit Do
                Try
                    GetSectionNames.Add(BuffStr.Substring(PrevPos + 1, Length - PrevPos))
                Catch
                End Try
                PrevPos = Length
            Loop
        End If
    End Function

    Public Property Text(ByVal sSection As String, ByVal sKey As String) As String
        Get
            Return ReadString(sSection, sKey, "")
        End Get
        Set(ByVal value As String)
            Write(sSection, sKey, value)
        End Set
    End Property

    Public Property Data(ByVal sSection As String, ByVal sKey As String) As Integer
        Get
            Return ReadInteger(sSection, sKey, -1)
        End Get
        Set(ByVal value As Integer)
            Write(sSection, sKey, value)
        End Set
    End Property

    'Private variables and constants
    Private m_Filename As String
    Private m_Section As String
    Private Const MAX_ENTRY As Integer = 32768
End Class


Namespace My


    ' The following events are availble for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Dim WithEvents client As New System.Net.WebClient
        Dim LoggingIsOn As Boolean = False
        Dim XmlLog As Xml.XmlDocument

        Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown

        End Sub

        Private Sub WriteToLogFile(ByVal Str As String)
            If LoggingIsOn Then
                Dim TmpNode As XmlElement
                If XmlLog Is Nothing Then
                    XmlLog = New Xml.XmlDocument
                    XmlLog.AppendChild(XmlLog.CreateElement("Log"))
                End If
                TmpNode = XmlLog.CreateElement("Entry")
                TmpNode.SetAttribute("DateTime", Now)
                TmpNode.AppendChild(XmlLog.CreateTextNode(Str))
                XmlLog.ChildNodes.Item(0).AppendChild(TmpNode)
                XmlLog.Save("C:\debugLauncher.xml")
            End If
        End Sub

        Private Function HasWriteAccess(ByVal directory As DirectoryInfo) As Boolean

            Dim currentUser As System.Security.Principal.WindowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent()
            Dim currentPrincipal As System.Security.Principal.WindowsPrincipal = DirectCast(System.Threading.Thread.CurrentPrincipal, WindowsPrincipal)

            ' Get the collection of authorization rules that apply to the current directory
            Dim acl As AuthorizationRuleCollection = directory.GetAccessControl().GetAccessRules(True, True, GetType(System.Security.Principal.SecurityIdentifier))

            ' These are set to true if either the allow read or deny read access rights are set
            Dim allowWrite As Boolean = False
            Dim denyWrite As Boolean = False

            For x As Integer = 0 To acl.Count - 1
                Dim currentRule As FileSystemAccessRule = DirectCast(acl(x), FileSystemAccessRule)
                ' If the current rule applies to the current user
                If currentUser.User.Equals(currentRule.IdentityReference) OrElse currentPrincipal.IsInRole(DirectCast(currentRule.IdentityReference, SecurityIdentifier)) Then
                    If currentRule.AccessControlType.Equals(AccessControlType.Deny) Then
                        If (currentRule.FileSystemRights And FileSystemRights.Write) = FileSystemRights.Write Then
                            denyWrite = True
                        End If
                    ElseIf currentRule.AccessControlType.Equals(AccessControlType.Allow) Then
                        If (currentRule.FileSystemRights And FileSystemRights.Write) = FileSystemRights.Write Then
                            allowWrite = True
                        End If
                    End If
                End If
            Next

            If allowWrite And Not denyWrite Then
                Return True
            Else
                Return False
            End If
        End Function


        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup

            'Check that LaunchTrinity has write access to it's own folder
            If Not HasWriteAccess(My.Computer.FileSystem.GetDirectoryInfo(My.Application.Info.DirectoryPath)) Then
                'The user does not have write access, so start LaunchTrintiy from application data directory
                If Not My.Computer.FileSystem.FileExists(GetSpecialFolder(CSIDLEnum.UserProfile) & "Trinity 4.0\LaunchTrinity.exe") Then
                    My.Computer.FileSystem.CopyFile(Path.Combine(My.Application.Info.DirectoryPath, "LaunchTrinity.exe"), Path.Combine(GetSpecialFolder(CSIDLEnum.UserProfile), "Trinity 4.0\LaunchTrinity.exe"))
                    My.Computer.FileSystem.CopyFile(Path.Combine(My.Application.Info.DirectoryPath, "launch.ini"), Path.Combine(GetSpecialFolder(CSIDLEnum.UserProfile), "Trinity 4.0\launch.ini"))
                End If
                Process.Start(Path.Combine(GetSpecialFolder(CSIDLEnum.UserProfile), "Trinity 4.0\LaunchTrinity.exe"))
                End
            End If

            Dim Ini As New clsIni
            'Ini.Create(My.Application.Info.DirectoryPath & "\launch.ini")

            Ini.Create(My.Application.Info.DirectoryPath & "\launch.ini")

            If Not Ini.Text("Launcher", "Debug") = "" Then
                LoggingIsOn = True
            End If
            Dim ca As New System.Text.StringBuilder
            For Each arg As String In My.Application.CommandLineArgs
                ca.Append(arg & " ")
            Next
            WriteToLogFile("Command line args: " & ca.ToString)
            If Not My.Application.CommandLineArgs.Contains("-file") OrElse My.Application.CommandLineArgs.IndexOf("-file") = My.Application.CommandLineArgs.Count - 1 Then
                Dim LatestVersion As String
                Dim MyVersion As String
                Dim intMyVersion As Integer
                Dim intLatestVersion As Integer

                WriteToLogFile("Trying to register DLL")

                'check if the Dll is registered or not
                Try
                    If Not System.IO.File.Exists(GetSpecialFolder(CSIDLEnum.UserProfile) & "Trinity 4.0\Trinity.ini") Then
                        'we need to register the dll
                        Dim pInfo As New ProcessStartInfo
                        pInfo.FileName = "regsvr32"
                        pInfo.Arguments = "/s " & Ini.Text("AdvantEdge", "Folder") & Ini.Text("AdvantEdge", "Dll")
                        pInfo.CreateNoWindow = False
                        pInfo.WindowStyle = ProcessWindowStyle.Hidden
                        Process.Start(pInfo)
                    End If
                Catch
                    WriteToLogFile("ERROR While trying to register DLL")
                    Windows.Forms.MessageBox.Show("Error while registering Connect.dll", "T R I N I T Y", MessageBoxButtons.OK)
                End Try


                WriteToLogFile("Server Address: " & Ini.Text("Server", "Address"))
                'check if it is a web adress
                ' if its a web adress we connect and update from the adress
                'if not then its a network adress and then we have a different procedure
                If Ini.Text("Server", "Address") = "" Then
                    Windows.Forms.MessageBox.Show("Launch.ini is damaged or missing.", "T R I N I T Y", MessageBoxButtons.OK)
                    End
                End If
                '    /JOOS 2019-01-22
                '   Changed If-Statement: If Ini.Text("Server", "Address").Substring(0, 3).ToUpper = "WWW" Then
                '   due to problems with connection to STO-DMZ and downloading latest version and LaunchTrinity
                If Ini.Text("Server", "Address") = "apps.mecglobal.se" Then
                    Try
                        WriteToLogFile("Connecting to HTTP")
                        Dim fileList As New List(Of String)

                        Dim client As New System.Net.WebClient

                        'download the file
                        client.DownloadFile(New Uri("http://" & Ini.Text("Server", "Address") & "/trinity/versions.xml"), My.Application.Info.DirectoryPath & "\versions.xml")

                        WriteToLogFile("Reading version")

                        'get the server version (it was downloaded by the launcher
                        '/JOOS
                        'Changed My.Application.Info.DirectoryPath & "\versions.xml") cause it was not working
                        Dim xmlDocServer As New Xml.XmlDocument
                        xmlDocServer.Load(IO.Directory.Exists(IO.Path.Combine(My.Application.Info.DirectoryPath, "\versions.xml")))

                        Dim xmlServerList As Xml.XmlElement
                        xmlServerList = xmlDocServer.SelectSingleNode("//LauncherFiles")

                        'if we have no old we download all files, if not we check for versions
                        If System.IO.File.Exists(My.Application.Info.DirectoryPath & "\version.xml") Then

                            'get the current version on the client
                            Dim xmlDocClient As New Xml.XmlDocument
                            xmlDocClient.Load(My.Application.Info.DirectoryPath & "\version.xml")

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
                            frmDownload.setVariables(fileList, "http://" & Ini.Text("Server", "Address") & "/trinity/")
                            frmDownload.Show()
                        Else
                            Process.Start(My.Application.Info.DirectoryPath & "\Trinity.exe")

                            System.Environment.Exit(1)
                        End If


                        ''**********************************************
                        '    If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\versions.xml") Then
                        '        MyVersion = IO.File.ReadAllText(My.Application.Info.DirectoryPath & "\Version.txt")
                        '    Else
                        '        MyVersion = 0
                        '    End If

                        '    intMyVersion = CInt(MyVersion.Trim)
                        '    intLatestVersion = CInt(LatestVersion.Trim)

                        '    WriteToLogFile("Client version = " & intMyVersion & ", Latest version = " & intLatestVersion)
                        '    If intMyVersion < intLatestVersion Then
                        '        WriteToLogFile("frmDownload")
                        '        frmDownload.Show()
                        '        WriteToLogFile("frmDownload end")
                        '    Else
                        '        WriteToLogFile("Loading Trinity")
                        '        Shell(My.Application.Info.DirectoryPath & "\Trinity.exe", AppWinStyle.MaximizedFocus, False, -1)
                        '        End
                        '    End If
                    Catch
                        WriteToLogFile("ERROR connecting to the web server")
                        Windows.Forms.MessageBox.Show("There was an error while connecting to the server." & vbCrLf & "Could not check for new versions. + 1", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Shell(My.Application.Info.DirectoryPath & "\Trinity.exe", AppWinStyle.MaximizedFocus, False, -1)
                        End
                    End Try
                Else 'end of web and start of network procedure
                    Try
                        WriteToLogFile("Connecting to Network")
                        Dim objReader As New System.IO.StreamReader(Ini.Text("Server", "Address") & "version.txt")
                        LatestVersion = objReader.ReadToEnd
                        objReader.Close()

                        If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\Version.txt") Then
                            MyVersion = IO.File.ReadAllText(My.Application.Info.DirectoryPath & "\Version.txt")
                        Else
                            MyVersion = 0
                        End If

                        'type them as integers
                        intMyVersion = CInt(MyVersion.Trim)
                        intLatestVersion = CInt(LatestVersion.Trim)

                        'we always check the folder for files
                        'Dim dir As IO.Directory
                        'Dim strTmp() As String = dir.GetFiles(Ini.Text("Server", "Address") & "files\")
                        'Dim Dir As New IO.Directory(strDir)
                        Dim networkFileInfo As IO.FileInfo
                        Dim localFileInfo As IO.FileInfo


                        Dim myfile As String
                        For Each myfile In IO.Directory.GetFiles(Ini.Text("Server", "Address") & "files\")
                            'strip off the path
                            myfile = myfile.Substring(myfile.LastIndexOf("\") + 1)
                            'check if the program folder has this file
                            If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\" & myfile) Then
                                'check if the network fiel in a newer one, if so we copy it
                                networkFileInfo = New IO.FileInfo(Ini.Text("Server", "Address") & "files\" & myfile)
                                localFileInfo = New IO.FileInfo(My.Application.Info.DirectoryPath & "\" & myfile)

                                If localFileInfo.LastWriteTime < networkFileInfo.LastWriteTime Then
                                    IO.File.Copy(Ini.Text("Server", "Address") & "files\" & myfile, My.Application.Info.DirectoryPath & "\" & myfile, True)
                                End If
                            Else
                                IO.File.Copy(Ini.Text("Server", "Address") & "files\" & myfile, My.Application.Info.DirectoryPath & "\" & myfile, True)
                            End If
                        Next

                        If intMyVersion < intLatestVersion Then
                            'copy the updatefiles
                            IO.File.Copy(Ini.Text("Server", "Address") & "Trinity.exe", My.Application.Info.DirectoryPath & "\Trinity.exe", True)
                            IO.File.Copy(Ini.Text("Server", "Address") & "updates.txt", My.Application.Info.DirectoryPath & "\updates.txt", True)
                            IO.File.Copy(Ini.Text("Server", "Address") & "version.txt", My.Application.Info.DirectoryPath & "\version.txt", True)

                            Shell("notepad " & My.Application.Info.DirectoryPath & "\updates.txt", AppWinStyle.MaximizedFocus, False, -1)
                            Shell(My.Application.Info.DirectoryPath & "\Trinity.exe", AppWinStyle.MaximizedFocus, False, -1)
                            End
                        Else
                            Shell(My.Application.Info.DirectoryPath & "\Trinity.exe", AppWinStyle.MaximizedFocus, False, -1)
                            End
                        End If

                    Catch 'if something fails we start trinity and display a error messege
                        WriteToLogFile("ERROR in connecting to the network")
                        Windows.Forms.MessageBox.Show("There was an error while connecting to the network server." & vbCrLf & "Could not check for new versions. + 2", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Shell(My.Application.Info.DirectoryPath & "\Trinity.exe", AppWinStyle.MaximizedFocus, False, -1)
                        End
                    End Try
                End If

                WriteToLogFile("End of launcher")
            Else
                WriteToLogFile("Download specific development version")
                Dim file As String = My.Application.CommandLineArgs(My.Application.CommandLineArgs.IndexOf("-file") + 1)
                WriteToLogFile("Download " & "http://" & Ini.Text("Server", "Address") & "/trinity/" & file)

                client.DownloadFile(New Uri("http://" & Ini.Text("Server", "Address") & "/trinity/" & file), My.Application.Info.DirectoryPath & "\" & file)
                WriteToLogFile("Download ok, copy file")

                IO.File.Copy(My.Application.Info.DirectoryPath & "\" & file, My.Application.Info.DirectoryPath & "\Trinity.exe", True)
                WriteToLogFile("Copy ok, delete temp file")

                Kill(My.Application.Info.DirectoryPath & "\" & file)

                WriteToLogFile("Delete ok, Launch!")
                Shell(My.Application.Info.DirectoryPath & "\Trinity.exe", AppWinStyle.MaximizedFocus, False, -1)
                End
            End If

        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub



        'functions for getting local user

        Shared Function Pathify(ByVal Path As String) As String
            If Right(Path, 1) = "\" Then
                Pathify = Path
            Else
                Pathify = Path & "\"
            End If
        End Function

        Declare Function SHGetSpecialFolderLocation Lib "Shell32.dll" (ByVal hwndOwner As Integer, ByVal nFolder As Integer, ByRef pidl As ITEMIDLIST) As Integer

        Declare Function SHGetPathFromIDList Lib "Shell32.dll" Alias "SHGetPathFromIDListA" (ByVal pidl As Integer, ByVal pszPath As String) As Integer

        Public Structure SH_ITEMID
            Dim cb As Integer
            Dim abID As Byte
        End Structure

        Public Structure ITEMIDLIST
            Dim mkid As SH_ITEMID
        End Structure

        Public Const MAX_PATH As Short = 260

        Public Enum CSIDLEnum
            StartMenuPrograms = 2 'C:\Documents and Settings\<USERNAME>\Start Menu\Programs
            MyDocuments = 5 'C:\Documents and Settings\<USERNAME>\My Documents
            Favorites = 6 'C:\Documents and Settings\<USERNAME>\Favorites
            Startup = 7 'C:\Documents and Settings\<USERNAME>\Start Menu\Programs\Startup
            Recent = 8 'C:\Documents and Settings\<USERNAME>\Recent
            SendTo = 9 'C:\Documents and Settings\<USERNAME>\SendTo
            StartMenu = 11 'C:\Documents and Settings\<USERNAME>\Start Menu
            Desktop = 16 'C:\Documents and Settings\<USERNAME>\Desktop
            NetHood = 19 'C:\Documents and Settings\<USERNAME>\NetHood
            Fonts = 20 'C:\WINNT\Fonts
            Templates = 21 'C:\Documents and Settings\<USERNAME>\Templates
            StartMenuAllUsers = 22 'C:\Documents and Settings\All Users\Start Menu
            StartMenuProgramsAllUsers = 23 'C:\Documents and Settings\All Users\Start Menu\Programs
            StartupAllUsers = 24 'C:\Documents and Settings\All Users\Start Menu\Programs\Startup
            DesktopAllUsers = 25 'C:\Documents and Settings\All Users\Desktop
            ApplicationData = 26 'C:\Documents and Settings\<USERNAME>\Application Data
            ProntHood = 27 'C:\Documents and Settings\<USERNAME>\PrintHood
            LocalApplicationData = 28 'C:\Documents and Settings\<USERNAME>\Local Settings\Application Data
            FavoritesAllUsers = 31 'C:\Documents and Settings\All Users\Favorites
            TemporaryInternetFiles = 32 'C:\Documents and Settings\<USERNAME>\Local Settings\Temporary Internet Files
            Cookies = 33 'C:\Documents and Settings\<USERNAME>\Cookies
            History = 34 'C:\Documents and Settings\<USERNAME>\Local Settings\History
            ApplicationDataAllUsers = 35 'C:\Documents and Settings\All Users\Application Data
            WinNT = 36 'C:\WINNT
            System32 = 37 'C:\WINNT\system32
            ProgramFiles = 38 'C:\Program Files
            MyPictures = 39 'C:\Data\My Pictures
            UserProfile = 40 'C:\Documents and Settings\<USERNAME>
            'System32 = 41            'C:\WINNT\system32
            CommonFiles = 43 'C:\Program Files\Common Files
            TemplatesAllUsers = 45 'C:\Documents and Settings\All Users\Templates
            DocumentsAllUsers = 46 'C:\Documents and Settings\All Users\Documents
            AdministrativeToolsAllUsers = 47 'C:\Documents and Settings\All Users\Start Menu\Programs\Administrative Tools
            AdministrativeTools = 48 'C:\Documents and Settings\<USERNAME>\Start Menu\Programs\Administrative Tools
            Temp = 49
        End Enum

        Function GetSpecialFolder(ByRef CSIDL As CSIDLEnum) As String
            Dim sPath As String
            Dim IDL As ITEMIDLIST
            Dim s As String

            '
            ' Retrieve info about system folders such as the
            ' "Desktop" folder. Info is stored in the IDL structure.
            '
            If CSIDL = CSIDLEnum.Temp Then
                s = My.Computer.FileSystem.SpecialDirectories.Temp

                ' Add backslash if one absent
                If Len(s) > 0 Then
                    Return Pathify(s)
                Else
                    Return "\"
                End If
            Else
                If SHGetSpecialFolderLocation(VB6.GetHInstance.ToInt32, CInt(CSIDL), IDL) = 0 Then
                    '
                    ' Get the path from the ID list, and return the folder.
                    '
                    sPath = Space(MAX_PATH)
                    If SHGetPathFromIDList(IDL.mkid.cb, sPath) Then
                        Return Pathify(Left(sPath, InStr(sPath, vbNullChar) - 1))
                    End If
                End If
            End If
            Return ""
        End Function


    End Class

End Namespace
