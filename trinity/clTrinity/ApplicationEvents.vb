Imports System.Xml.Linq

Namespace My

    ' The following events are availble for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Private oldCI As System.Globalization.CultureInfo

        Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown
            Trinity.Helper.SaveLog()
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        End Sub

        Private _startTrinityFast As Boolean = False

        Public Property startTrinityFast() As Boolean
            Get
                Return _startTrinityFast
            End Get
            Set(value As Boolean)
                _startTrinityFast = value
            End Set
        End Property

        Public Function checkDeveloper()
            If TrinitySettings.UserEmail = "joakim.koch@groupm.com" Then
                _startTrinityFast = True
            End If
        End Function
        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            Dim watch As New System.Diagnostics.Stopwatch()

            watch.Start()

            Dim MyVersion As String = ""

            Dim Ini As New Trinity.clsIni
            Ini.Create(My.Application.Info.DirectoryPath & "\launch.ini")

            Console.WriteLine("Try: " & watch.Elapsed.TotalSeconds)

            'firstly we check for a new launcher version
            Try
                If Not CommandLineArgs.Contains("noloader") Then
                    'if this file does not exists we have a old version of the launcher and needs to get it
                    'If System.IO.File.Exists(My.Application.Info.DirectoryPath & "\versions.xml") Then
                    'Above code didn't quite work. Now a new launcher is downloaded every time Trinity is started
                    'Change this later...
                    If False Then
                        'should not happen but as a precausion
                        If Not System.IO.File.Exists(My.Application.Info.DirectoryPath & "\version.xml") Then GoTo get_new_launcher

                        'get the server version (it was downloaded by the launcher
                        Dim xmlDoc As New Xml.XmlDocument
                        xmlDoc.Load(My.Application.Info.DirectoryPath & "\versions.xml")

                        Dim xmlE As Xml.XmlElement
                        xmlE = xmlDoc.SelectSingleNode("//Launcher")

                        Dim sV As Integer = xmlE.GetAttribute("Version")

                        'get the current version on the client
                        Dim xmlDoc2 As New Xml.XmlDocument
                        xmlDoc2.Load(My.Application.Info.DirectoryPath & "\version.xml")

                        Dim xmlE2 As Xml.XmlElement
                        xmlE2 = xmlDoc2.SelectSingleNode("//Launcher")

                        Dim myV As Integer = xmlE2.GetAttribute("Version")


                        If myV < sV Then GoTo get_new_launcher

                        'get the Trinity.exe version
                        xmlE2 = xmlDoc2.SelectSingleNode("//File[@Name='Trinity.exe']")
                        MyVersion = xmlE2.GetAttribute("Version")

                        'delete the file, it is not needed
                        System.IO.File.Delete(My.Application.Info.DirectoryPath & "\versions.xml")
                    Else

get_new_launcher:
                        Dim client As New System.Net.WebClient

                        'download the file
                        client.DownloadFile(New Uri("http://" & Ini.Text("Server", "Address") & "/trinity/LaunchTrinity.exe"), My.Application.Info.DirectoryPath & "\LaunchTrinity.ex_")

                        'sleep for a few seconds (1) to let the file get down properly
                        System.Threading.Thread.Sleep(1000)

                        'rename the file
                        If System.IO.File.Exists(My.Application.Info.DirectoryPath & "\LaunchTrinity.exe") Then
                            Kill(My.Application.Info.DirectoryPath & "\LaunchTrinity.exe")
                        End If
                        Rename(My.Application.Info.DirectoryPath & "\LaunchTrinity.ex_", My.Application.Info.DirectoryPath & "\LaunchTrinity.exe")

                        'The launcher will not be restarted but used the next time...
                        'Process.Start(My.Application.Info.DirectoryPath & "\LaunchTrinity.exe")
                        'System.Environment.Exit(1)
                        'Exit Sub
                    End If

                End If
            Catch
                MsgBox("Error getting new launcher", MsgBoxStyle.Information, "Error")
            End Try

            Console.WriteLine("new Launcher: " & watch.Elapsed.TotalSeconds)

            ' launch Trinity

            'This will be something like C:\Documents and Settings\hfalth\Trinity 4.0\Trinity.ini
            If Not System.IO.File.Exists(Trinity.Helper.GetSpecialFolder(Trinity.Helper.CSIDLEnum.UserProfile) & "Trinity 4.0\Trinity.ini") Then
                'show a user info dialogue if the user has no Trinity.ini
                Dim frm As New frmNewUser
                frm.ShowDialog()
            Else
                TrinitySettings = New Trinity.cSettings(Trinity.Helper.GetSpecialFolder(Trinity.Helper.CSIDLEnum.UserProfile) & "Trinity 4.0")
            End If

            If CommandLineArgs.Contains("forceplugins") AndAlso IO.Directory.Exists(IO.Path.Combine(My.Application.Info.DirectoryPath, "Plugins")) Then
                For Each _file As IO.FileInfo In My.Computer.FileSystem.GetDirectoryInfo(IO.Path.Combine(My.Application.Info.DirectoryPath, "Plugins")).GetFiles
                    frmPlugins.InstallPlugin(_file.Name)
                Next
            End If

            LoggingIsOn = TrinitySettings.Debug

            Console.WriteLine("New Trinitysettings: " & watch.Elapsed.TotalSeconds)
            'Dim test As New Trinity.cBlockIniReader
            'test.ReadIni()

            'check what DB is supposed to be used
            'If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\Version.txt") Then
            '    MyVersion = IO.File.ReadAllText(My.Application.Info.DirectoryPath & "\Version.txt")
            'End If


            If My.Computer.FileSystem.FileExists(TrinitySettings.LocalDataPath & "\multistart.ini") Then

                frmMultistart.ShowDialog()
            End If

            Console.WriteLine("Multistart: " & watch.Elapsed.TotalSeconds)
            'OK now the Connect.dll of the user installation is registered
            'frmSplashscreen.Build = 
            Using frmSplashscreen As New frmSplashscreen(MyVersion)
                frmSplashscreen.Show()

                'frmSplashscreen.Status = "Checking .NET framework version"

                'frmSplashscreen.Status = ".NET framework version is " & Environment.Version.ToString

                'If System.Environment.Version.Major < 3.5 Then
                '    Windows.Forms.MessageBox.Show("Please ask your system administrator to install the Microsoft .NET 3.5 Framework." & vbNewLine _
                '                                  & "Certain features of Trinity may not function properly.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK _
                '                                  , Windows.Forms.MessageBoxIcon.Information)
                'End If


                Console.WriteLine("Splash: " & watch.Elapsed.TotalSeconds)


                checkDeveloper()
                If (Not CommandLineArgs.Contains("nodll") AndAlso TrinitySettings.CheckForNewDLL) OrElse CommandLineArgs.Contains("checkdll") Then

                    frmSplashscreen.Status = "Checking DLL..."
                    If Trinity.Helper.GetRegisteredDLLVersion() < Trinity.Helper.GetLatestDLLVersion() Then
                        Console.WriteLine("new DLL")
                        frmSplashscreen.Status = "Found newer DLL..."
                        Dim Reg As New Process
                        Reg.StartInfo.FileName = "regsvr32"
                        Dim DLLPath As String = Trinity.Helper.GetLatestDLLPath()
                        ''Uncomment this to register a specific DLL version
                        'DLLPath = Trinity.Helper.ReverseString(DLLPath)
                        'DLLPath = DLLPath.Substring(9)
                        'DLLPath = Trinity.Helper.ReverseString(DLLPath)
                        'DLLPath = DLLPath & "16755.dll"
                        Reg.StartInfo.Arguments = "/s """ & DLLPath & """"
                        Reg.Start()
                        frmSplashscreen.Status = "Found newer DLL... Registering"
                        Reg.WaitForExit()

                    End If

                End If

                Console.WriteLine("DLL: " & watch.Elapsed.TotalSeconds)
                If Right(TrinitySettings.DataBase(Trinity.cSettings.SettingsLocationEnum.locNetwork), 3).ToUpper = "MDB" Then
                    DBReader = New Trinity.cDBReaderAccess
                ElseIf TrinitySettings.DataBase(Trinity.cSettings.SettingsLocationEnum.locNetwork).ToUpper = "SQL" Then
                    DBReader = New Trinity.cDBReaderSQL
                Else
                    Throw New Exception("Neither Access nor SQL database available!")

                End If



                Trinity.Helper.WriteToLogFile("Setup Database=" & TrinitySettings.ConnectionString(Trinity.cSettings.SettingsLocationEnum.locNetwork) & TrinitySettings.DataBase(Trinity.cSettings.SettingsLocationEnum.locNetwork))

                Console.WriteLine("Database type: " & watch.Elapsed.TotalSeconds)
                frmSplashscreen.Status = "Connecting to database..."
                'connect the handler to the database
                DBReader.Connect(Trinity.cDBReader.ConnectionPlace.Network, (Not CommandLineArgs.Contains("nodb") AndAlso TrinitySettings.UpdateDBSchema))
                Trinity.Helper.WriteToLogFile("Connection OK")
                Console.WriteLine("Connected: " & watch.Elapsed.TotalSeconds)
                'if there is no connection we try local
                If Not DBReader.alive Then
                    frmSplashscreen.Status = "Connecting to database... FAILED!"
                    DBReader = New Trinity.cDBReaderAccess ' Use access
                    DBReader.Connect(Trinity.cDBReader.ConnectionPlace.Local, Not CommandLineArgs.Contains("nodb"))
                End If
                If Not DBReader.alive Then
                    MsgBox("Could not connect to database", MsgBoxStyle.Critical, "Error connecting")
                    Exit Sub
                End If
                DataPath = DBReader.DataPath

                If DataPath <> TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork) Then
                    Windows.Forms.MessageBox.Show("You are not connected to the network database. Please note that" & vbCrLf & "pricelists may not be updated.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Warning)
                End If

                System.Windows.Forms.Application.DoEvents()

                frmSplashscreen.Status = "Connecting to AdvantEdge..."

                Console.WriteLine("DB Datapath/Do events: " & watch.Elapsed.TotalSeconds)
                'we try to create a new campaign
                'If we fails we try to reg the AdvantEdge dll usual cause)
                'if that didnt solve the problem we tell the user we had a failure
                Try
                    Campaign = New Trinity.cKampanj(TrinitySettings.ErrorChecking)

                    'Campaign.ErrorCheckingEnabled = TrinitySettings.ErrorChecking
                Catch ex1 As Exception
                    Try
                        Trinity.Helper.WriteToLogFile("Campaign = new Trinity.cKampanj FAILED. Attempting to register AdvantEdge DLL: " & ex1.Message)
                        Dim pInfo As New ProcessStartInfo
                        pInfo.FileName = "regsvr32"
                        pInfo.Arguments = "/s " & Ini.Text("AdvantEdge", "Folder") & Ini.Text("AdvantEdge", "Dll")
                        pInfo.CreateNoWindow = False
                        pInfo.WindowStyle = ProcessWindowStyle.Hidden
                        Process.Start(pInfo)

                        'sleep for a few seconds (8) to let the Dll get registered
                        System.Threading.Thread.Sleep(8000)

                        Campaign = New Trinity.cKampanj(TrinitySettings.ErrorChecking)

                    Catch ex2 As Exception
                        Trinity.Helper.WriteToLogFile("Couldnt register AdvantEdge DLL, shutting down: " & ex2.Message)
                        frmSplashscreen.Close()
                        If frmMain IsNot Nothing Then
                            frmMain.Close()
                        End If
                        MsgBox("Trinity was unable to establish a connection to AdvantEdge and will close down.", MsgBoxStyle.Critical, "Error")
                        End 'close down everything
                    End Try
                End Try
                Console.WriteLine("Connect to advantedge: " & watch.Elapsed.TotalSeconds)

                TrinitySettings.MainObject = Campaign
                Trinity.Helper.MainObject = Campaign

                Console.WriteLine("Main object campaign: " & watch.Elapsed.TotalSeconds)
                Campaign.AdToox = New Trinity.cAdtoox(TrinitySettings.AdtooxUsername, TrinitySettings.AdtooxPassword)

                Console.WriteLine("Adtoox: " & watch.Elapsed.TotalSeconds)

                frmSplashscreen.Status = "Creating objects..."
                Dim i As Integer
                Dim TmpChan As Trinity.cChannel
                Dim TmpBT As Trinity.cBookingType

                Dim a As Integer
                Dim ErrDescription As String
                Dim ErrNumber As Long
                Dim StartMode As Integer

                'On Error GoTo Main_Error


                Trinity.Helper.WriteToLogFile("Start of modTrinity40/Main")

                oldCI = System.Threading.Thread.CurrentThread.CurrentCulture
                System.Threading.Thread.CurrentThread.CurrentCulture = _
                    New System.Globalization.CultureInfo("sv-SE")

                frmSplashscreen.Status = "Creating paths..."

                Trinity.Helper.WriteToLogFile("Set Data path=" & Trinity.Helper.GetSpecialFolder(Trinity.Helper.CSIDLEnum.UserProfile) & "Trinity 4.0")
                TrinitySettings.LocalDataPath = Trinity.Helper.GetSpecialFolder(Trinity.Helper.CSIDLEnum.UserProfile) & "Trinity 4.0"

                StartMode = TrinitySettings.StartMode

                Console.WriteLine("Paths: " & watch.Elapsed.TotalSeconds)
                'Check Matrix version
                If IO.File.Exists(IO.Path.Combine(My.Application.Info.DirectoryPath, "Matrix40.dll")) Then
                    Trinity.Helper.WriteToLogFile("Matrix40 was found")
                    MatrixVersion = MatrixVersionEnum.Matrix40
                ElseIf My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\Interop.Matrix30.dll") Then
                    Trinity.Helper.WriteToLogFile("Matrix30 was found")
                    MatrixVersion = MatrixVersionEnum.Matrix30
                Else
                    Trinity.Helper.WriteToLogFile(My.Application.Info.DirectoryPath & "\Interop.Matrix30.dll was not found")
                    MatrixVersion = MatrixVersionEnum.NotInstalled
                End If

                Console.WriteLine("Matrix: " & watch.Elapsed.TotalSeconds)
                Try

                    Trinity.Helper.WriteToLogFile("Set the status of the window to say reading default area")

                    frmSplashscreen.Status = "Reading default area..."

                    Trinity.Helper.WriteToLogFile("Now assigning Campaign.Area the value of " & TrinitySettings.DefaultArea)
                    Campaign.Area = TrinitySettings.DefaultArea

                    Trinity.Helper.WriteToLogFile("Now assigning Campaign.Arealog the value of " & TrinitySettings.DefaultAreaLog)
                    Campaign.AreaLog = TrinitySettings.DefaultAreaLog

                    Trinity.Helper.WriteToLogFile("Now changing splash screen to setting up channels")
                    frmSplashscreen.Status = "Setting up channels..."

                    Trinity.Helper.WriteToLogFile("Create channels")
                    Trinity.Helper.WriteToLogFile("Now running CreateChannels()")

                    
                    Campaign.CreateChannels()
                    ' Campaign.ReadBundles()
                    Console.WriteLine("Create channels: " & watch.Elapsed.TotalSeconds)

                    'Campaign.xmlColorSchemes.ReadColorSchemes()

                    frmSplashscreen.Status = "Setting up people..."
                    'If Not DBReader.transferPeopleToDB() Then
                    '    Trinity.Helper.WriteToLogFile("Failed to setup people list")
                    'Else
                    '    Trinity.Helper.WriteToLogFile("Successfully setup people list")
                    'End If


                    frmSplashscreen.Status = "Setting up targets..."
                    Dim test = 0

                    Try
                        Trinity.Helper.WriteToLogFile("Set Basic Targets")
                        Trinity.Helper.WriteToLogFile("Now setting Campaign.Alladults to" & TrinitySettings.AllAdults)
                        Campaign.AllAdults = TrinitySettings.AllAdults
                        Trinity.Helper.WriteToLogFile("Now setting Campaign.Maintarget.TargetName to " & Campaign.AllAdults)
                        Campaign.MainTarget.TargetName = Campaign.AllAdults
                        Trinity.Helper.WriteToLogFile("Now setting Campaign.SecondaryTarget.Alladults to" & Campaign.AllAdults)
                        Campaign.SecondaryTarget.TargetName = Campaign.AllAdults
                    Catch ex As Exception
                        Windows.Forms.MessageBox.Show(ex.Message)
                    End Try
                    
                    If startTrinityFast = false
                        Trinity.Helper.WriteToLogFile("Now adding all channels")
                        For i = 1 To Campaign.Channels.Count
                            LongName.Add(Campaign.Channels(i).Shortname, Campaign.Channels(i).ChannelName)
                        Next
                        Trinity.Helper.WriteToLogFile("Now add all booking types to channels")
                        For i = 1 To Campaign.Channels(1).BookingTypes.Count
                            'Dim what1 As String = Campaign.Channels(1).BookingTypes(i).Shortname
                            'Dim what2 As String = Campaign.Channels(1).BookingTypes(i).Name
                            If Not LongBT.ContainsKey(Campaign.Channels(1).BookingTypes(i).Shortname) Then
                                LongBT.Add(Campaign.Channels(1).BookingTypes(i).Shortname, Campaign.Channels(1).BookingTypes(i).Name)
                            End If
                        Next

                        frmSplashscreen.Status = "Reading pricelist..."

                        Console.WriteLine("LongBT names: " & watch.Elapsed.TotalSeconds)
                        'Trinity.Helper.WriteToLogFile("Read Pricelists")
                        For Each TmpChan In Campaign.Channels
                            For Each TmpBT In TmpChan.BookingTypes
                                frmSplashscreen.Status = "Reading pricelist... " & TmpBT.ToString
                                TmpBT.ReadPricelist()
                            Next
                        Next

                        Campaign.ChannelBundles.Read()

                        Console.WriteLine("read pricelists: " & watch.Elapsed.TotalSeconds)


                        If TrinitySettings.DefaultContractPath <> "" Then

                            Trinity.Helper.WriteToLogFile("Read default contract")
                            frmSplashscreen.Status = "Reading default contract..."
                            Try
                                Campaign.LoadDefaultContract()

                            Catch ex As Exception
                                Windows.Forms.MessageBox.Show("Error while loading default contract: " & TrinitySettings.DefaultContractPath & vbCrLf & vbCrLf & "Message:" & vbCrLf & ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                            End Try

                            Campaign.Contract = Nothing
                        End If
                        Console.WriteLine("read contracts: " & watch.Elapsed.TotalSeconds)

                    End If
                    

                    Trinity.Helper.WriteToLogFile("Show frmMain")
                    frmSplashscreen.Close()
                    frmMain.Show()
                    'frmMain.showRecentCampaigns()
                    frmMain.showRecentCampaignsMenu()
                    Console.WriteLine("Main.show() : " & watch.Elapsed.TotalSeconds)
                    Console.WriteLine("end startup: " & watch.Elapsed.TotalSeconds)
                    watch.Stop()
                    Exit Sub
                    
                Catch ex As Exception
                    ErrNumber = Err.Number
                    ErrDescription = Err.Description
                    If IsIDE() Then
                        a = MsgBox("Error:" & Chr(10) & Chr(10) & "Runtime Error '" & ErrNumber & "':" & Chr(10) & Chr(10) & ErrDescription & Chr(10) & Chr(10) & "Vill du utföra en felsökning?", vbYesNo, "TRINITY")
                        If a = vbNo Then Exit Sub
                        'Stop
                    End If
                    Trinity.Helper.WriteToLogFile(ex.Message)
                    System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
                    MsgBox("Runtime Error '" & ErrNumber & "':" & Chr(10) & Chr(10) & Err.Description & " in Main.", vbCritical, "Error")
                End Try
            End Using
        End Sub

        'Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
        '    Trinity.Helper.WriteToLogFile("ERROR:")
        '    Trinity.Helper.WriteToLogFile(e.Exception.Message)
        '    Trinity.Helper.WriteToLogFile(e.Exception.StackTrace)
        '    Trinity.Helper.SaveLog()
        '    e.ExitApplication = False
        'End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            DebugStack.Log("UnhandledException")
            Dim frm As New frmUncaughtException(e.Exception)
            Select Case frm.ShowDialog
                Case vbOK
                    e.ExitApplication = True
                Case vbCancel
                    e.ExitApplication = False
            End Select
        End Sub
    End Class


End Namespace
