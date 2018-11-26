
Public Class Writer

    Private Shared TrinityIni As New Trinity.clsIni

    Shared Event Progress(p As Integer, Message As String)

    Shared Sub Write(Server As String, Database As String, Breaks As List(Of Break))
        Using _conn As New SqlClient.SqlConnection(String.Format("Server=stosqlp01101;Database=Trinity_sweden;Integrated Security=True;"&";"))
            _conn.Open()

            Using _tran As SqlClient.SqlTransaction = _conn.BeginTransaction()
                Try
                    For Each _channel As String In (From _break As Break In Breaks Select _break.Channel).Distinct
                        Dim _tmpChannel = _channel
                        Dim _dates = From _break As Break In Breaks Where _break.Channel = _tmpChannel Select _break.Date
                        Dim _from As Long = _dates.Min.ToOADate
                        Dim _to As Long = _dates.Max.ToOADate
                        Using _com As New SqlClient.SqlCommand("DELETE FROM events WHERE channel=@channel AND date>=@from AND date<=@to", _conn)
                            _com.Transaction = _tran
                            _com.Parameters.AddWithValue("channel", _channel)
                            _com.Parameters.AddWithValue("to", _to)
                            _com.Parameters.AddWithValue("from", _from)
                            _com.ExecuteNonQuery()
                        End Using
                    Next
                    Dim _b As Long = 0
                    For Each _break In Breaks
                        Using _com As New SqlClient.SqlCommand("INSERT INTO events ([ID],[Channel],[Date],[Time],[StartMam],[Duration],[Name],[ChanEst],[Price],[IsLocal],[EstimationPeriod],[Area],[Comment],[Type],[UseCPP],[EstimationTarget],[Addition]) VALUES (@ID,@Channel,@Date,@Time,@StartMam,@Duration,@Name," & _break.ChanEst.ToString.Replace(",", ".") & ",@Price,@IsLocal,@EstimationPeriod,@Area,@Comment,0,@UseCPP,@EstTarget,@Addition)", _conn)
                            _com.Transaction = _tran
                            _com.Parameters.AddWithValue("ID", _break.ID)
                            _com.Parameters.AddWithValue("Channel", _break.Channel)
                            _com.Parameters.AddWithValue("Date", _break.Date.ToOADate)
                            _com.Parameters.AddWithValue("Time", _break.Time)
                            _com.Parameters.AddWithValue("StartMam", _break.MaM)
                            _com.Parameters.AddWithValue("Duration", _break.Duration)
                            _com.Parameters.AddWithValue("Name", _break.Programme.Substring(0, IIf(_break.Programme.Length > 60, 60, _break.Programme.Length)))
                            _com.Parameters.AddWithValue("Price", _break.Price)
                            _com.Parameters.AddWithValue("IsLocal", _break.IsLocal)
                            _com.Parameters.AddWithValue("EstimationPeriod", "-4fw")
                            _com.Parameters.AddWithValue("Area", IIf(String.IsNullOrEmpty(_break.Area), "SE", _break.Area))
                            _com.Parameters.AddWithValue("UseCPP", _break.UseCPP)
                            _com.Parameters.AddWithValue("EstTarget", IIf(String.IsNullOrEmpty(_break.EstimationTarget), "", _break.EstimationTarget))
                            _com.Parameters.AddWithValue("Addition", _break.Addition)
                            _com.Parameters.AddWithValue("Comment", "")
                            _com.ExecuteNonQuery()
                            _b += 1
                            RaiseEvent Progress((_b / Breaks.Count) * 100, "Writing " & _break.Channel & " to " & Server & "/" & Database)
                        End Using
                    Next
                    _tran.Commit()
                Catch ex As Exception
                    _tran.Rollback()
                    _conn.Close()
                    Throw ex
                End Try
            End Using
            _conn.Close()
        End Using
    End Sub


    Private Shared ReadOnly Property DBUser() As String
        Get
            TrinityIni.Create(GetSpecialFolder(CSIDLEnum.UserProfile) & "Trinity 4.0" & "\Trinity.ini")
            DBUser = Decode(TrinityIni.Text("NetworkDB", "Uid"))
        End Get
    End Property

    Private Shared ReadOnly Property DBPwd() As String
        Get
            TrinityIni.Create(GetSpecialFolder(CSIDLEnum.UserProfile) & "Trinity 4.0" & "\Trinity.ini")
            DBPwd = Decode(TrinityIni.Text("NetworkDB", "Pwd"))
        End Get
    End Property

    Shared Function Decode(ByVal strString As String) As String

        'get the intitial string
        Dim strInput As String = strString

        'declare variables for the coded values
        Dim strTemp As String = ""
        Dim strMessege As String = ""
        Dim strTemp2 As String = ""

        'loop all the encoded chars
        If strInput.Length > 2 Then
            strTemp = strInput.Substring(0, 3)
            strInput = strInput.Substring(3, strInput.Length - 3)
        End If
        While Not strTemp = ""

            strTemp2 = ReverseString(strTemp)
            strTemp2 = CDbl(strTemp2) - 78

            strMessege = strMessege & Convert.ToChar(CInt(strTemp2))

            If strInput.Length > 2 Then
                strTemp = strInput.Substring(0, 3)
                strInput = strInput.Substring(3, strInput.Length - 3)
            Else
                strTemp = ""
            End If
        End While

        Return strMessege

    End Function

    Shared Function ReverseString(ByVal InputString As String) As String

        Dim lLen As Long, lCtr As Long
        Dim sChar As String
        Dim sAns As String = ""

        lLen = Len(InputString)
        For lCtr = lLen To 1 Step -1
            sChar = Mid(InputString, lCtr, 1)
            sAns = sAns & sChar
        Next

        ReverseString = sAns

    End Function
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

    Shared Function GetSpecialFolder(ByRef CSIDL As CSIDLEnum) As String
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
                Return s & "\"
            Else
                Return "\"
            End If
        Else
            If SHGetSpecialFolderLocation(System.Runtime.InteropServices.Marshal.GetHINSTANCE(System.Reflection.Assembly.GetExecutingAssembly.GetModules()(0)).ToInt32(), CInt(CSIDL), IDL) = 0 Then
                '
                ' Get the path from the ID list, and return the folder.
                '
                sPath = Space(MAX_PATH)
                If SHGetPathFromIDList(IDL.mkid.cb, sPath) Then
                    Return Left(sPath, InStr(sPath, vbNullChar) - 1) & "\"
                End If
            End If
        End If
        Return ""
    End Function
End Class
