Imports System.Xml
Imports Microsoft
Imports Microsoft.VisualBasic.Compatibility
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D

'To read the registry subkey.
Imports Microsoft.Win32
'To use regular expressions.
Imports System.Text.RegularExpressions


Namespace Trinity

    'Class with Helper-functions to simplify commonly used tasks and calculations
    Public Class Helper
        Const MaxDistanceIfSame = 60
        Const MaxDistanceIfNotSame = 15
        Const StrLength = 5

        Friend frmFilmNotFound

        Shared XmlLog As Xml.XmlDocument

        Shared Main As Trinity.cKampanj

        Shared Function formatStringToNumeric(ByVal str As String) As Double
            'Removes all illegal characters from a string that is supposed to be numeric
            'we use comma as decimal pointer, not for thousands
            Dim returnString As String
            Dim objRegExp As New Regex("[^,\d]+")
            returnString = objRegExp.Replace(str, "")
        End Function

        Public Shared ReadOnly Property CompileTime() As DateTime
            Get
                Dim filePath As String = System.Reflection.Assembly.GetCallingAssembly().Location
                Const c_PeHeaderOffset As Integer = 60
                Const c_LinkerTimestampOffset As Integer = 8
                Dim b As Byte() = New Byte(2047) {}
                Dim s As System.IO.Stream = Nothing

                Try
                    s = New System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    s.Read(b, 0, 2048)
                Finally
                    If s IsNot Nothing Then
                        s.Close()
                    End If
                End Try

                Dim i As Integer = System.BitConverter.ToInt32(b, c_PeHeaderOffset)
                Dim secondsSince1970 As Integer = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset)
                Dim dt As New DateTime(1970, 1, 1, 0, 0, 0)
                dt = dt.AddSeconds(secondsSince1970)
                dt = dt.AddHours(TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours)
                Return dt
            End Get
        End Property

        Shared Function ConvertIntToARGB(ByVal Int As Integer) As Drawing.Color
            Dim HexVal As String = Hex(Int)

            While HexVal.Length < 6
                HexVal = "0" & HexVal
            End While
            Return Drawing.Color.FromArgb(Val("&H" & HexVal.Substring(4, 2)), Val("&H" & HexVal.Substring(2, 2)), Val("&H" & HexVal.Substring(0, 2)))
            'Dim hexString As String = Hex(Int).Substring(2, 6)
            '  Dim hexString As String = Hex(Int)

            ' While hexString.Length < 6
            '     hexString = hexString & "0"
            '   End While
            '   Dim cR As Integer = Convert.ToInt16(hexString.Substring(0, 2), 16)
            '   Dim cG As Integer = Convert.ToInt16(hexString.Substring(2, 2), 16)
            '   Dim cB As Integer = Convert.ToInt16(hexString.Substring(4, 2), 16)
            '    'Return Drawing.Color.FromArgb(Val("&H" & HexVal.Substring(4, 2)), Val("&H" & HexVal.Substring(2, 2)), Val("&H" & HexVal.Substring(0, 2)))
            '     'Return Drawing.Color.FromArgb(Val("&H" & HexVal.Substring(0, 2)), Val("&H" & HexVal.Substring(2, 2)), Val("&H" & HexVal.Substring(4, 2)))

            '    Return Drawing.Color.FromArgb(255, cR, cG, cB)

        End Function

        Shared Function ConvertARGBToInt(ByVal Color As Drawing.Color) As Long
            'sets a windowes integer value out of a RGB value
            'Return RGB(Color.R, Color.G, Color.B)
            Dim HexVal As String
            HexVal = Hex(Color.R)
            While HexVal.Length < 2
                HexVal = "0" & HexVal
            End While
            HexVal = Hex(Color.G) & HexVal
            While HexVal.Length < 4
                HexVal = "0" & HexVal
            End While
            HexVal = Hex(Color.B) & HexVal
            While HexVal.Length < 6
                HexVal = "0" & HexVal
            End While
            Return Val("&H" & HexVal)
            '  Dim R As String = Hex(Color.R)
            ' Dim G As String = Hex(Color.G)
            ' Dim B As String = Hex(Color.B)

            '  If R.Length < 2 Then R = "0" & R
            '   If G.Length < 2 Then G = "0" & G
            '     If B.Length < 2 Then B = "0" & B

            '     Dim hexString = "FF" & R & G & B

            '    Return System.Convert.ToInt64(hexString, 16)
            '  Return Val("&H" & hexString)
        End Function

        Shared Property MainObject() As Object
            Get
                Return Main
            End Get
            Set(ByVal value As Object)
                Main = value
            End Set
        End Property

        Shared Function FormatDateForBooking(ByVal d As Date) As String
            If TrinitySettings.DefaultDateFormat = "" Then
                Return Format(d, "yyyy-MM-dd")
            Else
                Return Format(d, TrinitySettings.DefaultDateFormat)
            End If
        End Function

        Shared Function FormatDateForBooking(ByVal d As Long) As String
            If TrinitySettings.DefaultDateFormat = "" Then
                Return Format(Date.FromOADate(d), "yyyy-MM-dd")
            Else
                Return Format(Date.FromOADate(d), TrinitySettings.DefaultDateFormat)
            End If
        End Function

        Shared Function GetExcelVersionIsInstalled() As Integer

            'The subkey's string value we check is like 
            'CultureSafeExcel.<version>, i e CultureSafeExcel.11 for Excel 2003

            'The subkey we are interested of is located under the 
            'HKEY_CLASSES_ROOT class.
            Const stXL_SUBKEY As String = "\CultureSafeExcel\CurVer"

            Dim rkVersionKey As RegistryKey = Nothing
            Dim stVersion As String = String.Empty
            Dim stXLVersion As String = String.Empty

            Dim RegEx As New Regex(".[1-9][1-9]$")

            Dim iVersion As Integer = Nothing

            Try
                'Here we try to open the subkey.
                rkVersionKey = Registry.ClassesRoot.OpenSubKey(name:=stXL_SUBKEY, writable:=False)

                'If it does not exist it means that Excel is not installed at all.
                If rkVersionKey Is Nothing Then
                    iVersion = 0
                    Return iVersion
                End If

                'OK, Excel is installed let's find out which version is available.
                stXLVersion = rkVersionKey.GetValue(name:=stVersion).ToString

                iVersion = CInt(stXLVersion.Substring(stXLVersion.LastIndexOf(".") + 1))

                Return iVersion

            Catch ex As Exception

                MsgBox(ex.ToString)
                Return 0

            Finally

                If Not rkVersionKey Is Nothing Then
                    rkVersionKey.Close()
                End If

            End Try

        End Function


        Shared Function FormatShortDateForBooking(ByVal d As Date) As String
            If TrinitySettings.DefaultShortDateFormat = "" Then
                Return Format(d, "yyMMdd")
            Else
                Return Format(d, TrinitySettings.DefaultShortDateFormat)
            End If
        End Function

        Shared Function FormatShortDateForBooking(ByVal d As Long) As String
            If TrinitySettings.DefaultShortDateFormat = "" Then
                Return Format(Date.FromOADate(d), "yyMMdd")
            Else
                Return Format(Date.FromOADate(d), TrinitySettings.DefaultShortDateFormat)
            End If
        End Function

        Shared Sub SendEmail(ByVal [To] As String, ByVal FromName As String, ByVal FromEmail As String, ByVal Subject As String, ByVal Body As String, Optional ByVal Attachments As List(Of String) = Nothing)
            'Dim _notes As New Notes()
            'Dim _password As String

            'If frmNotesPassword.ShowDialog <> Windows.Forms.DialogResult.OK Then
            '    Exit Sub
            'End If
            '_password = frmNotesPassword.TextBox1.Text

            '_notes.Server = TrinitySettings.NotesMailServer
            '_notes.MailFile = TrinitySettings.NotesMailFile
            '_notes.Password = _password

            '_notes.SendMail(FromName, FromEmail, Subject, Body, [To], Attachments)

            Trinity.cOutlook.send([To], Subject, Body, Attachments)

        End Sub

        Shared Sub WriteToLogFile(ByVal Str As String)
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
                Try
                    If TrinitySettings.Developer Then
                        XmlLog.Save("C:\debug\debug.xml")
                    Else
                        'XmlLog.Save("C:\debug.xml")
                    End If
                Catch ex As FieldAccessException
                    Debug.Print("debug.xml not writeable")
                End Try

            End If
        End Sub

        Shared Sub SaveLog()
            If LoggingIsOn Then
                If TrinitySettings.Developer Then
                    XmlLog.Save("C:\Debug\debug.xml")
                Else
                    XmlLog.Save("C:\debug.xml")
                End If
            End If
        End Sub

        'Shared Function ReverseString(ByVal sText As String) As String

        '    Dim lenText As Long, lPos As Long

        '    If Len(sText) = 0 Then Return ""

        '    lenText = Len(sText)
        '    ReverseString = Space(lenText)

        '    For lPos = lenText To 1 Step -1
        '        Mid$(ReverseString, lenText - lPos + 1, 1) = Mid$(sText, lPos, 1)
        '    Next lPos

        'End Function

        Shared Function Encode(ByVal strString As String) As String
            'get the intitial string
            Dim strInput As String = strString

            'declare variables for the coded values
            Dim strMessege As String = ""
            Dim dblTemp As Double
            Dim strTemp As String


            For Each c As Char In strInput
                'get the ASCII value
                strTemp = Convert.ToInt32(c)

                '3 different cases depending on how many characters there are (we need 3)
                Select Case strTemp.Length
                    Case Is = 1
                        strTemp = "00" & strTemp
                        dblTemp = strTemp + 78

                        strTemp = dblTemp
                        strTemp = ReverseString(strTemp)
                        strMessege = strMessege & strTemp
                    Case Is = 2
                        strTemp = "0" & strTemp
                        dblTemp = strTemp + 78

                        strTemp = dblTemp
                        strTemp = ReverseString(strTemp)
                        strMessege = strMessege & strTemp
                    Case Is = 3
                        dblTemp = strTemp + 78

                        strTemp = dblTemp
                        strTemp = ReverseString(strTemp)
                        strMessege = strMessege & strTemp
                End Select
            Next

            Return strMessege
        End Function

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

        Shared Function Pathify(ByVal Path As String) As String
            If Right(Path, 1) = "\" Then
                Pathify = Path
            Else
                Pathify = Path & "\"
            End If
        End Function

        'Convert Minutes After Midnight to readable Time string
        Shared Function Mam2Tid(ByVal MaM As Integer) As String
            Dim h As Integer
            Dim m As Integer
            Dim Tmpstr As String

            h = MaM \ 60
            m = MaM Mod 60

            Tmpstr = Trim(h) & ":" & Format(m, "00")
            While Len(Tmpstr) < 5
                Tmpstr = "0" & Tmpstr
            End While
            Mam2Tid = Tmpstr
        End Function

        'Hannes lade till denna och den fungerar bara på strängar med formatet "HH:mm"
        Shared Function Tid2Mam(ByVal Time As String) As Integer

            Dim hours As Integer = 0
            Dim minutes As Integer = 0

            hours = CType(Strings.Left(Time, 2), Integer)
            minutes = CType(Strings.Right(Time, 2), Integer)

            Return hours * 60 + minutes

        End Function

        Shared Function CreateString(ByVal Chars As Integer, ByVal ascii As Byte) As String
            Dim i As Integer
            Dim TmpStr As String

            TmpStr = ""
            For i = 1 To Chars
                TmpStr = TmpStr + Chr(ascii)
            Next
            Return TmpStr

        End Function

        'Shared Function GetDaypart(ByVal MaM As Integer) As Integer
        '    Dim i As Integer

        '    For i = 0 To Main.DaypartCount - 1
        '        If MaM >= Main.DaypartStart(i) Then
        '            If MaM <= Main.DaypartEnd(i) Then
        '                Return i
        '                Exit For
        '            End If
        '        End If
        '    Next
        'End Function

        '*******************************
        'Replaced by AddTargetsToAdedge
        '*******************************
        'Shared Function CreateTargetString(Optional ByVal IncludeBuyingTargets As Boolean = True, Optional ByVal IncludeProfile As Boolean = True) As String
        '    Dim Tmpstr As String
        '    Dim TmpChan As cChannel
        '    Dim TmpBT As cBookingType
        '    Dim TmpSex As String

        '    Tmpstr = Main.MainTarget.TargetName & "," & Main.SecondaryTarget.TargetName & "," & Main.ThirdTarget.TargetName & "," & Main.AllAdults & ","
        '    If IncludeBuyingTargets Then
        '        For Each TmpChan In Main.Channels
        '            For Each TmpBT In TmpChan.BookingTypes
        '                If TmpBT.BookIt Then
        '                    Tmpstr = Tmpstr & TmpBT.BuyingTarget.Target.TargetName & ","
        '                End If
        '            Next
        '        Next
        '    End If
        '    If IncludeProfile Then
        '        Select Case Left(Main.MainTarget.TargetNameNice, 1)
        '            Case "A" : TmpSex = ""
        '            Case "M" : TmpSex = "M"
        '            Case "W" : TmpSex = "W"
        '            Case Else : TmpSex = ""
        '        End Select
        '        Tmpstr = Tmpstr & TmpSex & "3-11," & TmpSex & "12-14," & TmpSex & "15-19," & TmpSex & "20-24," & TmpSex & "25-29," & TmpSex & "30-34," & TmpSex & "35-39," & TmpSex & "40-44," & TmpSex & "45-49," & TmpSex & "50-54," & TmpSex & "55-59," & TmpSex & "60-64," & TmpSex & "60-69," & TmpSex & "70-99,"
        '    End If
        '    Return Tmpstr
        'End Function

        'Method to add set all relevant targets in an Adedge.Breaks object
        Shared Sub AddTargetsToAdedge(ByVal Adedge As ConnectWrapper.Breaks, Optional ByVal IncludeBuyingTargets As Boolean = True, Optional ByVal IncludeProfile As Boolean = True)
            Adedge.clearTargetSelection()
            AddTarget(Adedge, Main.MainTarget)
            AddTarget(Adedge, Main.SecondaryTarget)
            AddTarget(Adedge, Main.ThirdTarget)
            AddTarget(Adedge, Main.CustomTarget)

            Adedge.setTargetMnemonic(Main.AllAdults, True)

            If IncludeBuyingTargets Then
                For Each TmpChan As Trinity.cChannel In Main.Channels
                    For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                        If TmpBT.BookIt Then
                            AddTarget(Adedge, TmpBT.BuyingTarget.Target)
                        End If
                    Next
                Next
            End If
            If IncludeProfile Then
                Dim TmpSex As String
                If Main.MainTarget.TargetType = Trinity.cTarget.TargetTypeEnum.trgMnemonicTarget Then
                    Select Case Main.MainTarget.TargetNameNice.ToUpper.ToString.Substring(0, 1)
                        Case "A" : TmpSex = ""
                        Case "M" : TmpSex = "M"
                        Case "W" : TmpSex = "W"
                        Case Else : TmpSex = ""
                    End Select
                Else
                    TmpSex = ""
                End If

                Adedge.setTargetMnemonic(TmpSex & "3-11," & TmpSex & "12-14," & TmpSex & "15-19," & TmpSex & "20-24," & TmpSex & "25-29," & TmpSex & "30-34," & TmpSex & "35-39," & TmpSex & "40-44," & TmpSex & "45-49," & TmpSex & "50-54," & TmpSex & "55-59," & TmpSex & "60-64," & TmpSex & "60-69," & TmpSex & "70-99,", True)
            End If

        End Sub

        Shared Function AddTarget(ByVal Adedge As ConnectWrapper.Breaks, ByVal Target As cTarget, Optional ByVal Unique As Boolean = True) As Boolean
            Dim _returnVal As Boolean
            If Target.TargetType = cTarget.TargetTypeEnum.trgMnemonicTarget OrElse Target.TargetGroup = "" Then
                _returnVal = (Adedge.setTargetMnemonic(Target.TargetName, Unique) = 1)
            ElseIf Target.TargetType = cTarget.TargetTypeEnum.trgUserTarget Then
                _returnVal = (Adedge.setTargetUserDefined(Target.TargetGroup, Target.TargetName, Unique) = 1)
            End If
            If Unique AndAlso Not _returnVal AndAlso TargetIndex(Adedge, Target) < 0 Then
                If Target.TargetType = cTarget.TargetTypeEnum.trgMnemonicTarget OrElse Target.TargetGroup = "" Then
                    _returnVal = (Adedge.setTargetMnemonic(Target.TargetName, False) = 1)
                ElseIf Target.TargetType = cTarget.TargetTypeEnum.trgUserTarget Then
                    _returnVal = (Adedge.setTargetUserDefined(Target.TargetGroup, Target.TargetName, False) = 1)
                End If
            End If
            Return _returnVal
        End Function

        Shared Sub AddTargetsToAdedge(ByVal Adedge As ConnectWrapper.Brands, Optional ByVal IncludeBuyingTargets As Boolean = True, Optional ByVal IncludeProfile As Boolean = True)
            Adedge.clearTargetSelection()
            AddTarget(Adedge, Main.MainTarget)
            AddTarget(Adedge, Main.SecondaryTarget)
            AddTarget(Adedge, Main.ThirdTarget)
            AddTarget(Adedge, Main.CustomTarget)

            Adedge.setTargetMnemonic(Main.AllAdults, True)

            If IncludeBuyingTargets Then
                For Each TmpChan As Trinity.cChannel In Main.Channels
                    For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                        If TmpBT.BuyingTarget IsNot Nothing Then
                            AddTarget(Adedge, TmpBT.BuyingTarget.Target)
                        End If
                    Next
                Next
            End If
            If IncludeProfile Then
                Dim TmpSex As String
                Select Case Left(Main.MainTarget.TargetNameNice, 1)
                    Case "A" : TmpSex = ""
                    Case "M" : TmpSex = "M"
                    Case "W" : TmpSex = "W"
                    Case Else : TmpSex = ""
                End Select
                Adedge.setTargetMnemonic(TmpSex & "3-11," & TmpSex & "12-14," & TmpSex & "15-19," & TmpSex & "20-24," & TmpSex & "25-29," & TmpSex & "30-34," & TmpSex & "35-39," & TmpSex & "40-44," & TmpSex & "45-49," & TmpSex & "50-54," & TmpSex & "55-59," & TmpSex & "60-64," & TmpSex & "60-69," & TmpSex & "70-99,", False)
            End If
        End Sub

        Shared Function AddTarget(ByVal Adedge As ConnectWrapper.Brands, ByVal Target As cTarget, Optional ByVal Unique As Boolean = True) As Boolean
            Dim RetVal As Integer
            If Target.TargetType = cTarget.TargetTypeEnum.trgMnemonicTarget OrElse Target.TargetGroup Is Nothing Then
                If Target.TargetName.Substring(0, 1) = "A" Then
                    Target.TargetName = Target.TargetName.Substring(1)
                End If

                RetVal = Adedge.setTargetMnemonic(Target.TargetName, Unique)
            ElseIf Target.TargetType = cTarget.TargetTypeEnum.trgUserTarget Then
                RetVal = Adedge.setTargetUserDefined(Target.TargetGroup, Target.TargetName, Unique)
            End If
            Return RetVal > 0
        End Function

        Shared Function AddTarget(ByVal Adedge As ConnectWrapper.Brands, ByVal Target As String, Optional ByVal Unique As Boolean = True) As Boolean
            Dim RetVal As Integer
            RetVal = Adedge.setTargetMnemonic(Target, Unique)
            Return RetVal > 0
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

        Shared Function MondayOfWeek(ByVal year, ByVal week) As Date
            Dim TmpDate As Date

            TmpDate = CDate(year.ToString & "-06-01")
            TmpDate = TmpDate.AddDays((week - DatePart(DateInterval.WeekOfYear, TmpDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)) * 7)
            TmpDate = TmpDate.AddDays(-Weekday(TmpDate, vbMonday) + 1)
            MondayOfWeek = TmpDate

        End Function

        Shared Function MondayOfWeek(DateInWeek As Date) As Date
            While DateInWeek.DayOfWeek <> DayOfWeek.Monday
                DateInWeek = DateInWeek.AddDays(-1)
            End While
            Return DateInWeek
        End Function


        Shared Function DateTimeSerial(ByVal d As Date, ByVal MaM As Integer) As Double
            Dim h As Integer
            Dim m As Integer
            Dim Tmpstr As String

            h = MaM \ 60
            m = MaM Mod 60

            While h >= 24
                h = h - 24
                d = d.AddDays(1)
            End While
            Tmpstr = Trim(h) & ":" & Format(m, "00")

            Return CDate(Format(d, "Short Date") & " " & Tmpstr & ":00").ToOADate

        End Function

        Shared Function TargetIndex(ByVal Adedge As Object, ByVal Target As cTarget) As Integer
            Dim i As Integer
            Dim StrTarget As String = Target.TargetName
            If Target.TargetType = cTarget.TargetTypeEnum.trgMnemonicTarget AndAlso Val(Left(Target.TargetName, 1)) > 0 Then StrTarget = "A" & StrTarget
            If InStr(Adedge.GetType.Name, "Breaks") > 0 Then
                For i = 0 To DirectCast(Adedge, ConnectWrapper.Breaks).getTargetCount - 1
                    If DirectCast(Adedge, ConnectWrapper.Breaks).getTargetTitle(i) = StrTarget Then
                        Return i
                        Exit Function
                    End If
                Next
            Else
                For i = 0 To DirectCast(Adedge, ConnectWrapper.Brands).getTargetCount - 1
                    If DirectCast(Adedge, ConnectWrapper.Brands).getTargetTitle(i) = StrTarget Then
                        Return i
                        Exit Function
                    End If
                Next
            End If
            Return -1
        End Function

        Shared Function TargetIndex(ByVal Adedge As Object, ByVal Target As String) As Integer
            Dim i As Integer
            If Val(Left(Target, 1)) > 0 Then Target = "A" & Target
            If InStr(Adedge.GetType.Name, "Breaks") > 0 Then
                For i = 0 To DirectCast(Adedge, ConnectWrapper.Breaks).getTargetCount - 1
                    If DirectCast(Adedge, ConnectWrapper.Breaks).getTargetTitle(i) = Target Then
                        Return i
                        Exit Function
                    End If
                Next
            Else
                For i = 0 To DirectCast(Adedge, ConnectWrapper.Brands).getTargetCount - 1
                    If DirectCast(Adedge, ConnectWrapper.Brands).getTargetTitle(i) = Target Then
                        Return i
                        Exit Function
                    End If
                Next
            End If
            Return -1
        End Function

        'This function has been deprecated and replaced by the GetTimeShiftIndex

        'Shared Function UniverseIndex(ByVal Adedge As Object, ByVal Universe As String) As Integer
        '    Dim i As Integer
        '    If Universe = "" Then Return 0
        '    If InStr(Adedge.GetType.Name, "Breaks") > 0 Then
        '        For i = 0 To DirectCast(Adedge, ConnectWrapper.Breaks).getUniverseCount - 1
        '            If DirectCast(Adedge, ConnectWrapper.Breaks).getUniverseTitle(i) = Universe Then
        '                Return i
        '            End If
        '        Next
        '    Else
        '        For i = 0 To DirectCast(Adedge, ConnectWrapper.Brands).getUniverseCount - 1
        '            If DirectCast(Adedge, ConnectWrapper.Brands).getUniverseTitle(i) = Universe Then
        '                Return i
        '            End If
        '        Next
        '    End If
        '    Return -1
        'End Function

        Public Shared Sub AddTimeShift(ByRef Adedge As Connect.ComModule)
            Adedge.setTVSetConfigurationUserDefined(0, "Trinity", "")
            Adedge.setTVSetConfigurationDefault(0, Main.Area)
            Adedge.setTVSetConfigurationUserDefined(0, "Trinity", GetTimeShiftIndex(Adedge, "Live") & "," & GetTimeShiftIndex(Adedge, "VOSDAL+7"))
        End Sub

        Private Shared Function GetTimeShiftIndex(Adedge As Connect.ComModule, TimeShiftName As String) As String
            For i As Integer = 0 To Adedge.lookupNoTVSetConfigurationsByUser(0, "Trinity") - 1
                If Adedge.lookupTVSetConfigurationByUser(0, "Trinity", i).ToString.Replace("Trinity", "").Trim = TimeShiftName Then
                    Return i
                End If
            Next
            Return 0
        End Function

        Private Shared Function BetterBreak(ByVal FirstBreak As Integer, ByVal SecondBreak As Integer, ByVal MaM As Integer, ByVal ProgAfter As String, ByRef Breaks As Collections.ArrayList) As Boolean
            Dim OrigDist As Integer
            Dim NewDist As Integer
            Dim OrigSameProg As Boolean
            Dim NewSameProg As Boolean

            On Error GoTo BetterBreak_Error

            If FirstBreak < 0 OrElse SecondBreak < 0 Then
                Exit Function
            End If
            If Breaks(FirstBreak).MaM > Breaks(SecondBreak).MaM Then
                Return True
                Exit Function
            End If
            OrigDist = Math.Abs(MaM - Breaks(FirstBreak).MaM)
            NewDist = Math.Abs(MaM - Breaks(SecondBreak).MaM)

            'If ProgramHit(TmpSpot.ProgBefore, Break.getAttribGroup(aBreakProgBefore, FirstBreak)) Then
            If ProgramHit(ProgAfter, Breaks(FirstBreak).ProgAfter) Then
                OrigSameProg = True
            End If
            'End If
            'If ProgramHit(TmpSpot.ProgBefore, Break.getAttribGroup(aBreakProgBefore, SecondBreak)) Then
            If ProgramHit(ProgAfter, Breaks(SecondBreak).ProgAfter) Then
                NewSameProg = True
            End If
            'End If

            If OrigSameProg AndAlso Not NewSameProg Then
                If OrigDist <= MaxDistanceIfSame Then
                    Return False
                ElseIf NewDist <= MaxDistanceIfNotSame Then
                    Return True
                ElseIf OrigDist < NewDist Then
                    Return False
                Else
                    Return True
                End If
            ElseIf NewSameProg AndAlso Not OrigSameProg Then
                If NewDist < MaxDistanceIfSame Then
                    Return True
                ElseIf OrigDist <= MaxDistanceIfNotSame Then
                    Return False
                ElseIf NewDist < OrigDist Then
                    Return True
                Else
                    Return False
                End If
            ElseIf NewSameProg AndAlso OrigSameProg Then
                If NewDist <= OrigDist Then
                    Return True
                Else
                    Return False
                End If
            Else
                If NewDist <= OrigDist Then
                    Return True
                Else
                    Return False
                End If
            End If

            On Error GoTo 0
            Exit Function

BetterBreak_Error:

            'Err.Raise Err.Number, "frmSchedule: BetterBreak", Err.Description
            MsgBox("Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error")
            Exit Function
            Resume
        End Function

        Private Shared Function matchStrings(ByVal s1 As String, ByVal s2 As String) As Single
            'this function matches two stings by their sub strings and return a value between 0 and 1 where 1 is a 100% match
            ' note that the function dont see any difference between "you can go home" and "can you home is"
            ' Words shorter that 3 letters are not taken into account and neither the order of the words in the string.

            Dim tryWord As String
            Dim tryLength As Integer
            Dim Words As String()
            Dim maxWord As String
            Dim maxLength As Integer
            Dim i As Integer
            Dim countable As Integer
            Dim retur As Double

            'first we search for s1 in s2
            Words = s1.Split(" ")

            For Each maxWord In Words
                maxLength = maxWord.Length
                tryLength = maxLength
                While tryLength > 2
                    For i = 0 To maxLength - tryLength
                        tryWord = maxWord.Substring(maxLength - tryLength, tryLength)
                        If InStr(s2, tryWord) > 0 Then
                            Exit While
                        End If
                    Next
                    tryLength -= 1
                End While

                'we skip words shorter than 3 letters
                If Not tryLength < 3 Then
                    retur += tryLength / maxLength
                    countable += 1
                End If
            Next

            If countable = 0 Then
                Return 0
            Else
                Return retur / countable
            End If

        End Function

        Private Shared Function removeSpecials(ByVal s As String) As String
            Dim ca As Char() = s.ToLower.ToCharArray
            Dim retur As String = ""
            Dim ASCII As Integer

            For Each c As Char In ca
                ASCII = Convert.ToByte(c.ToString.ToLower)
                If (ASCII > 96 AndAlso ASCII < 123) OrElse (ASCII > 223 AndAlso ASCII < 247) Then
                    retur = retur & c.ToString
                End If
            Next

            Return retur
        End Function

        Shared Function RemoveNonNumbersFromString(ByVal s As String) As String
            Dim stringOfNumbers As String = Nothing
            For Each character As Char In s
                If Char.IsNumber(character) Or character = "," Or character = "." Then
                    stringOfNumbers += character
                End If
            Next
            Return stringOfNumbers
        End Function


        Public Shared Function ProgramHit(ByVal FirstProg As String, ByVal SecondProg As String) As Boolean
            On Error GoTo ProgramHit_Error

            FirstProg = UCase(FirstProg)
            SecondProg = UCase(SecondProg)
            If FirstProg = "" Then
                Return True
            Else
                If matchStrings(FirstProg, SecondProg) > 0.6 Then
                    Return True
                End If
                If matchStrings(SecondProg, FirstProg) > 0.6 Then
                    Return True
                End If
            End If

            On Error GoTo 0
            Exit Function

ProgramHit_Error:

            'Err.Raise Err.Number, "frmSchedule: ProgramHit", Err.Description
            MsgBox("Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error")


        End Function

        Shared Function EstimationQuality(ByVal Breaklist As ArrayList, ByVal EI As Trinity.cExtendedInfo, ByVal Adedge As ConnectWrapper.Breaks) As Single
            Const PROGWEIGHT = 0.2
            Const DISTWEIGHT = 0.5
            Const RATINGWEIGHT = 0.3

            Dim Quality As Single = 0
            Dim MinRating As Single = 999
            Dim MaxRating As Single = 0
            Dim TotRating As Single = 0

            For Each TmpBreak As Trinity.cBreak In Breaklist
                Dim ProgHit As Single
                If Not TmpBreak.ProgAfter Is Nothing OrElse Not EI.ProgAfter Is Nothing OrElse Not EI.ProgAfter = "" OrElse Not TmpBreak.ProgAfter = "" Then
                    ProgHit = 0
                Else
                    ProgHit = matchStrings(TmpBreak.ProgAfter, EI.ProgAfter)
                End If

                If matchStrings(EI.ProgAfter, TmpBreak.ProgAfter) > ProgHit Then ProgHit = matchStrings(EI.ProgAfter, TmpBreak.ProgAfter)
                If ProgHit < 0.6 Then ProgHit = 0.5

                Dim Dist As Integer = Math.Abs(TmpBreak.MaM - EI.MaM)
                If Dist > 120 Then Dist = 130

                Dim Rating As Single = Adedge.getUnit(Connect.eUnits.uTRP, TmpBreak.BreakIdx, , Main.TimeShift, TargetIndex(Adedge, Main.MainTarget))
                If Rating > MaxRating Then
                    MaxRating = Rating
                End If
                If Rating < MinRating Then
                    MinRating = Rating
                End If
                TotRating += Rating

                Quality += ((ProgHit - 0.5) / 0.5) * PROGWEIGHT + ((130 - Dist \ 10) / 130) * DISTWEIGHT
            Next
            Quality /= Breaklist.Count
            If (((TotRating / Breaklist.Count) - (MaxRating - MinRating)) / (TotRating / Breaklist.Count)) > 0 Then
                Quality += (((TotRating / Breaklist.Count) - (MaxRating - MinRating)) / (TotRating / Breaklist.Count)) * RATINGWEIGHT
            End If
            Return Quality
        End Function

        Shared Function Estimate(ByVal EstDate As Date, ByVal EstTime As Integer, ByVal NameAfter As String, ByVal Channel As String, ByRef Breaks As Collections.ArrayList) As Collections.ArrayList

            Dim b As Long
            Dim LastDate As Date
            Dim TmpBreakList As New Collections.ArrayList

            On Error GoTo Estimate_Error

            '    If Datum = "" Then
            '        TmpSpot.AirDate = 0
            '    Else
            '        TmpSpot.AirDate = Datum
            '    End If
            '    While Len(Tid) < 4
            '        Tid = "0" & Tid
            '    Wend
            '    TmpSpot.MaM = Left(Tid, 2) * 60 + Right(Tid, 2)
            '    TmpSpot.Estimation = EstNotOk
            '    TmpSpot.ProgBefore = NameBefore
            '    TmpSpot.ProgAfter = NameAfter
            For b = 1 To Breaks.Count - 1 'For b = 2 To Breaks.Count
                If Weekday(Breaks(b).AirDate, vbMonday) = Weekday(EstDate, vbMonday) OrElse EstDate.ToOADate = 0 Then
                    If Breaks(b).Channel.ChannelName = Channel Then
                        While BetterBreak(b - 1, b, EstTime, NameAfter, Breaks) AndAlso b > -1
                            b += 1
                            If b > Breaks.Count - 1 Then
                                b = -1
                            End If
                        End While
                        b = b - 1
                        If b > -1 Then
                            TmpBreakList.Add(Breaks(b))
                            If Breaks(b).AirDate = Breaks(Breaks.Count - 1).AirDate Then
                                Exit For
                            End If
                            LastDate = Breaks(b).AirDate
                            While Breaks(b).AirDate = LastDate
                                If b + 100 < Breaks.Count - 1 Then
                                    b += 100 'Steps of 200 to make the code faster
                                Else
                                    b += 1
                                End If
                            End While
                            While Breaks(b).airdate <> LastDate
                                b -= 1
                            End While
                            b += 1
                        Else
                            Exit For
                        End If
                    End If
                End If
            Next
            Return TmpBreakList

            On Error GoTo 0
            Exit Function

Estimate_Error:

            'Err.Raise Err.Number, "frmSchedule: Estimate", Err.Description
            MsgBox("Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error")
            Exit Function
            Resume

        End Function

        Shared Function Adedge2Channel(ByVal AdedgeChannel As String) As cChannel
            Dim TmpChan As cChannel

            For Each TmpChan In Main.Channels
                If InStr(UCase(TmpChan.AdEdgeNames.Trim) & ",", UCase(AdedgeChannel) & ",") > 0 Then
                    For each tmpBt As Trinity.cBookingType in TmpChan.BookingTypes
                        If tmpBt.BookIt
                            Return TmpChan
                        End If
                    Next
                End If
            Next
            Return Nothing
        End Function

        'Function returns True if the value is -1.#IND 
        Shared Function IsIndefinite(ByVal value As Single) As Boolean
            If Not value < 0 And Not value > 0 And value <> 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Sub New(ByVal mainObject As Trinity.cKampanj)
            Main = mainObject
        End Sub

        Shared Sub SyncLocalData()
            If Not Pathify(Pathify(TrinitySettings.LocalDataPath) & "Data") = TrinitySettings.DataPath Then
                SyncDir(TrinitySettings.DataPath, Pathify(Pathify(TrinitySettings.LocalDataPath) & "Data"))
            End If
        End Sub

        Private Shared Sub SyncDir(ByVal FromDirectory As String, ByVal ToDirectory As String)
            For Each TmpFile As IO.FileInfo In My.Computer.FileSystem.GetDirectoryInfo(FromDirectory).GetFiles
                If Not Pathify(ToDirectory) & TmpFile.Name = TrinitySettings.LocalDataBase Then
                    My.Computer.FileSystem.CopyFile(TmpFile.FullName, Pathify(ToDirectory) & TmpFile.Name, True)
                End If
            Next
            For Each TmpDir As String In My.Computer.FileSystem.GetDirectories(FromDirectory)
                If Not My.Computer.FileSystem.DirectoryExists(Pathify(ToDirectory) & My.Computer.FileSystem.GetDirectoryInfo(TmpDir).Name) Then
                    My.Computer.FileSystem.CreateDirectory(Pathify(ToDirectory) & My.Computer.FileSystem.GetDirectoryInfo(TmpDir).Name)
                End If
                SyncDir(TmpDir, Pathify(ToDirectory) & My.Computer.FileSystem.GetDirectoryInfo(TmpDir).Name)
            Next
        End Sub

        Public Shared Function GetRegisteredDLLVersion() As Integer
            Dim RegdDLL As String = GetDLLPath()
            Dim Version As String = System.Diagnostics.FileVersionInfo.GetVersionInfo(RegdDLL).FileVersion.Substring(System.Diagnostics.FileVersionInfo.GetVersionInfo(RegdDLL).FileVersion.LastIndexOf(".") + 1)

            Return CInt(Version)

        End Function


        Public Shared Function ReverseString(ByVal InputString As String) As String

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

        Public Shared Function GetLatestDLLVersion() As Integer
            Dim Path As String = GetLatestDLLPath()
            Return System.Diagnostics.FileVersionInfo.GetVersionInfo(Path).FileVersion.Substring(System.Diagnostics.FileVersionInfo.GetVersionInfo(Path).FileVersion.LastIndexOf(".") + 1)
        End Function

        Public Shared Function GetLatestDLLPath() As String
            Dim RegdDLL As String = GetDLLPath()
            Dim DLLPath As String = Trinity.Helper.Pathify(RegdDLL.Substring(0, RegdDLL.LastIndexOf("\")))
            Dim HighestVersion As Integer = 0
            Dim Path As String = ""
            Dim countDLLs As Integer = 0

#If BUILD32 Then
            For Each TmpFile As String In My.Computer.FileSystem.GetFiles(DLLPath, FileIO.SearchOption.SearchTopLevelOnly, "Connect.4.0.1*.dll")
#Else
            For Each TmpFile As String In My.Computer.FileSystem.GetFiles(DLLPath, FileIO.SearchOption.SearchTopLevelOnly, "Connect64.4.0.1*.dll")
#End If
                If System.Diagnostics.FileVersionInfo.GetVersionInfo(TmpFile).FileVersion IsNot Nothing Then
                    Dim Version As Integer = System.Diagnostics.FileVersionInfo.GetVersionInfo(TmpFile).FileVersion.Substring(System.Diagnostics.FileVersionInfo.GetVersionInfo(TmpFile).FileVersion.LastIndexOf(".") + 1)
                    If Version > HighestVersion Then
                        HighestVersion = Version
                        countDLLs = countDLLs + 1
                        Path = TmpFile
                    End If
                End If
            Next
            countDLLs = countDLLs
            Return Path
        End Function

        'Gets the directory where Connect.dll is registered
        'Also where AdvantEdge is supposed to be installed for this company
        Private Shared Function GetDLLPath() As String
            Dim GUID As String
            Dim Filepath As String
            If My.Computer.Registry.ClassesRoot.OpenSubKey("Connect.Brands") Is Nothing Then
                'If we have no previous registered DLL we return the ini-file directory
                Dim Ini As New Trinity.clsIni
                Ini.Create(My.Application.Info.DirectoryPath & "\launch.ini")
                Filepath = Ini.Text("AdvantEdge", "Folder") & Ini.Text("AdvantEdge", "Dll")
            Else
                GUID = My.Computer.Registry.ClassesRoot.OpenSubKey("Connect.Brands").OpenSubKey("Clsid").GetValue("")
                Filepath = My.Computer.Registry.ClassesRoot.OpenSubKey("CLSID").OpenSubKey(GUID).OpenSubKey("InProcServer32").GetValue("")
                Dim t As New Connect.Brands
                RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64)

            End If
            Return Filepath
        End Function

        Private Shared SetFilmAs As New Dictionary(Of Trinity.cBookingType, Hashtable)

        Shared Sub SetFilmForSpot(ByRef Spot As Trinity.cPlannedSpot)
            Dim InList As Boolean = False
            'For each of the films that this spot has had added to it for that week
            'Spot.Week.Films will contain all films added to this booking type in Setup
            For Each TmpFilm As Trinity.cFilm In Spot.Week.Films
                'If one of the film codes´is in the list, then set the Boolean to true and we do not need to find another film code from the campaign to assign to it
                If TmpFilm.Filmcode = Trim(Spot.Filmcode) Then InList = True
                If InStr("," & TmpFilm.Filmcode & ",", "," & Spot.Filmcode & ",") > 0 And Spot.Filmcode <> "" Then
                    InList = True
                End If
            Next

norway:
            If Not InList Then
                'The film code is not one of the ones that this booking type is supposed to have,
                'so we have to give it another
                If Not SetFilmAs.ContainsKey(Spot.Bookingtype) Then
                    SetFilmAs.Add(Spot.Bookingtype, New Hashtable)
                End If
                If Not SetFilmAs(Spot.Bookingtype).ContainsKey(Spot.Filmcode) Then
                    Dim frmfilmnotfound As frmFilmNotFound = New frmFilmNotFound(Spot.Filmcode, Spot.SpotLength, Spot.Channel.ChannelName, Spot.Bookingtype.Name)
                    frmfilmnotfound.ShowDialog()
                    If frmfilmnotfound.optSetAsOld.Checked Then
                        'If the user has checked the box set new film for spot do this
                        SetFilmAs(Spot.Bookingtype).Add(Spot.Filmcode, frmfilmnotfound.cmbFilm.Text)
                        If frmfilmnotfound.optChangeOnSpots.Checked Then
                            Spot.Filmcode = Spot.Week.Films(frmfilmnotfound.cmbFilm.Text).Filmcode
                        Else
                            For Each TmpWeek As Trinity.cWeek In Spot.Bookingtype.Weeks
                                TmpWeek.Films(frmfilmnotfound.cmbFilm.Text).Filmcode = Spot.Filmcode
                            Next
                        End If
                    Else
                        Spot.Filmcode = Spot.Week.Films(SetFilmAs(Spot.Bookingtype)(Spot.Filmcode)).Filmcode
                    End If
                Else
                    'Else just make a new filmcode out of this one and enter it into the campaign and booking type
                    Spot.Filmcode = Spot.Week.Films(SetFilmAs(Spot.Bookingtype)(Spot.Filmcode)).Filmcode
                End If
            End If
            If Spot.Film Is Nothing Then
                Spot.Filmcode = Spot.Week.Films(Spot.Week.Films.Count).Filmcode
            End If
        End Sub

        Shared Sub ClearFilmList()
            SetFilmAs.Clear()
        End Sub
    End Class

End Namespace