Imports System.Xml
Imports Microsoft
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D

Public Class Helper

    Private Shared XmlLog As Xml.XmlDocument

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

    Shared Function GetSpecialFolder(ByVal CSIDL As CSIDLEnum) As String
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
            If SHGetSpecialFolderLocation(Marshal.GetHINSTANCE(System.Reflection.Assembly.GetExecutingAssembly.GetModules()(0)).ToInt32, CInt(CSIDL), IDL) = 0 Then
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
        End If
    End Sub

    Shared Function LocalDataPath() As String
        Return GetSpecialFolder(CSIDLEnum.UserProfile) & "Balthazar\"
    End Function

    Shared Function Pathify(ByVal Path As String) As String
        If Right(Path, 1) <> "\" Then
            Path &= "\"
        End If
        Return Path
    End Function

    Shared Function Mam2Time(ByVal MaM As Integer) As String
        Dim h As Integer
        Dim m As Integer
        h = MaM \ 60
        m = MaM Mod 60
        Return Format(h, "00") & ":" & Format(m, "00")
    End Function

    Shared Function Time2Mam(ByVal Time As String) As Integer
        Dim h As Integer
        Dim m As Integer

        If Time.Length = 5 Then
            h = Left(Time, 2)
            m = Right(Time, 2)
        ElseIf Time.Length < 3 Then
            h = Time
            m=0
        ElseIf Time.IndexOfAny(".:") > 0 Then
            h = Left(Time, Time.IndexOfAny(".:"))
            m = Mid(Time, Time.IndexOfAny(".:") + 2)
        Else
            Try
                h = Left(Time, Time.Length - 2)
                m = Right(Time, 2)
            Catch
                Return 0
            End Try
        End If
        Return h * 60 + m
    End Function

    Shared Function ARGB2Excel(ByVal ARGB As Integer) As Integer
        Dim TmpColor As Color = Color.FromArgb(ARGB)
        Return Color.FromArgb(TmpColor.B, TmpColor.G, TmpColor.R).ToArgb
    End Function

    Shared Function GetRandomColor() As Color
        Return Color.FromArgb(Rnd() * 255, Rnd() * 255, Rnd() * 255)
    End Function

    Shared Function CreateRandomPassword() As String
        Randomize()
        Dim TmpCode As String = ""
        For i As Integer = 1 To 8
            Dim TmpChar As String = ChrW(92)
            While AscW(TmpChar) > 90 AndAlso AscW(TmpChar) < 97
                TmpChar = ChrW(Int(Rnd() * 57) + 65)
            End While
            TmpCode &= TmpChar
        Next
        Return TmpCode
    End Function

    Shared Function GetDBSingle(ByVal val As Object) As Single
        If IsDBNull(val) Then Return 0
        Return val
    End Function

    Shared Function SendMail(ByVal ToName As String, ByVal ToEmail As String, ByVal FromName As String, ByVal FromEmail As String, ByVal Subject As String, ByVal Body As String)
        'Dim Mail As New Net.Mail.MailMessage(New Net.Mail.MailAddress(FromEmail, FromName), New Net.Mail.MailAddress(ToEmail, ToName))
        'Mail.Subject = Subject
        'Mail.Body = Body
        'Mail.IsBodyHtml = True

        'Dim SMTP As New Net.Mail.SmtpClient("lon-dgs01.insidemedia.net", 25)
        'SMTP.UseDefaultCredentials = True
        'SMTP.Send(Mail)

        'Stop
    End Function
End Class
