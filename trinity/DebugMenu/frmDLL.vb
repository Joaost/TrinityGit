Public Class frmDLL

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

    Private Sub frmDLL_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim _tmpIni As New Trinity.clsIni

        _tmpIni.Create(IO.Path.Combine(GetTrinityFolder, "multistart.ini"))

        For i As Integer = 1 To _tmpIni.Data("Multistart", "Count")
            With grdInstalls.Rows(grdInstalls.Rows.Add(False, _tmpIni.Text("Multistart", "Name" & i), "Please wait...", "..."))
                .Tag = IO.Path.GetDirectoryName(_tmpIni.Text("Multistart", "Connect" & i))
                AsyncGetLatestVersion(.Index, _tmpIni.Text("Multistart", "Connect" & i))
            End With
        Next
    End Sub

    Private Sub AsyncGetLatestVersion(row As Integer, folder As String)
        Dim thread As New Threading.Thread(Sub()
                                               Dim _version As String = GetLatestVersion(folder)
                                               Me.Invoke(Sub()
                                                             grdInstalls.Rows(row).Cells(colVersion.Index).Value = _version
                                                         End Sub)
                                           End Sub)
        thread.Start()
    End Sub

    Function GetTrinityFolder() As String
        Dim sPath As String
        Dim IDL As ITEMIDLIST

        If SHGetSpecialFolderLocation(Microsoft.VisualBasic.Compatibility.VB6.Support.GetHInstance.ToInt32, CInt(CSIDLEnum.UserProfile), IDL) = 0 Then
            sPath = Space(MAX_PATH)
            If SHGetPathFromIDList(IDL.mkid.cb, sPath) Then
                Return IO.Path.Combine(sPath.Substring(0, sPath.IndexOf(vbNullChar)), "Trinity 4.0")
            End If
        End If
        Return ""
    End Function

    Function GetLatestVersion(Path As String) As String
        Dim _folder As String = IO.Path.GetDirectoryName(Path)
        Return GetLatestDLLVersion(_folder)
    End Function

    Private Function GetLatestDLLVersion(DllPath As String) As Integer
        Dim Path As String = GetLatestDLLPath(DllPath)
        Return System.Diagnostics.FileVersionInfo.GetVersionInfo(Path).FileVersion.Substring(System.Diagnostics.FileVersionInfo.GetVersionInfo(Path).FileVersion.LastIndexOf(".") + 1)
    End Function

    Private Function GetLatestDLLPath(DllPath As String) As String
        Dim HighestVersion As Integer = 0
        Dim Path As String = ""

        For Each TmpFile As String In My.Computer.FileSystem.GetFiles(DLLPath, FileIO.SearchOption.SearchTopLevelOnly, "Connect*.dll")
            Dim Version As Integer = System.Diagnostics.FileVersionInfo.GetVersionInfo(TmpFile).FileVersion.Substring(System.Diagnostics.FileVersionInfo.GetVersionInfo(TmpFile).FileVersion.LastIndexOf(".") + 1)
            If Version > HighestVersion Then
                HighestVersion = Version
                Path = TmpFile
            End If
        Next
        Return Path
    End Function

    Private Sub cmdRollback_Click(sender As System.Object, e As System.EventArgs) Handles cmdRollback.Click
        colStatus.Visible = True

    End Sub

    Private Sub cmdUpload_Click(sender As System.Object, e As System.EventArgs) Handles cmdUpload.Click
        Dim _fd As New Windows.Forms.OpenFileDialog()

        _fd.Filter = "DLL-files|*.dll"
        _fd.FileName = "Connect*.dll"
        If _fd.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        colStatus.Visible = True
        For Each _row As Windows.Forms.DataGridViewRow In grdInstalls.Rows
            If DirectCast(_row.Cells(colUse.Index), Windows.Forms.DataGridViewCheckBoxCell).Value Then
                Try
                    IO.File.Copy(_fd.FileName, IO.Path.Combine(_row.Tag, IO.Path.GetFileName(_fd.FileName)), True)
                    _row.Cells(colStatus.Index).Value = "Success!"
                Catch ex As Exception
                    _row.Cells(colStatus.Index).Value = "Failed"
                    _row.Cells(colStatus.Index).ErrorText = ex.Message
                End Try
            Else
                _row.Cells(colStatus.Index).Value = ""
            End If
        Next
    End Sub

    Private Sub grdInstalls_CellContentClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdInstalls.CellContentClick
        If e.ColumnIndex = colUse.Index Then
            grdInstalls.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Not grdInstalls.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
        End If
    End Sub
End Class