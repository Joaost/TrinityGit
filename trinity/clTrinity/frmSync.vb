Imports System.Windows.Forms
Imports System.IO

Public Class frmSync

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        'DBReader.QUERY("UPDATE events SET estimationperiod = '-4fw'  WHERE estimationperiod is null")
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub cmdSync_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSync.Click
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        'copy the network database file to the local folder
        'File.Copy(TrinitySettings.DataBase(Trinity.cSettings.SettingsLocationEnum.locNetwork), TrinitySettings.DataBase(Trinity.cSettings.SettingsLocationEnum.locLocal), True)

        If DBReader.isLocal Then
            'if we are runnig on the local database then we need to exit the sub
            Exit Sub
        End If

        Dim strTmp As String
        strTmp = TrinitySettings.DataBase(Trinity.cSettings.SettingsLocationEnum.locLocal)
        Dim i As Integer = strTmp.LastIndexOf("\")
        strTmp = strTmp.Substring(0, i)
        'copy the entire network folder to the local directory
        If Not CopyDirectory(TrinitySettings.DataPath, strTmp) Then
            MessageBox.Show("Sync error")
        End If

        'copy the database to SQLce database
        Dim dt As DataTable
        Dim dr As DataRow

        If Not Microsoft.VisualBasic.Right(TrinitySettings.DataBase(Trinity.cSettings.SettingsLocationEnum.locLocal), 3).ToUpper = "SDF" Then
            'Access database  OBS DENNA KOD FÖRUTSÄTTER ATT DET PÅ NÄTVERKET KÖRS ACCESS!!!!
            Dim strLoc As String = TrinitySettings.DataBase(Trinity.cSettings.SettingsLocationEnum.locLocal)
            Dim strNet As String = TrinitySettings.DataBase(Trinity.cSettings.SettingsLocationEnum.locNetwork)
            Try
                File.Copy(strNet, strLoc, True)
            Catch ex As Exception
                MsgBox("Unable to create access database", MsgBoxStyle.Critical, "Error copying")
            End Try

        End If

        Me.Cursor = Windows.Forms.Cursors.Default

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Public Function CopyDirectory(ByVal Src As String, ByVal Dest As String, Optional _
  ByVal bQuiet As Boolean = False) As Boolean
        If Not Directory.Exists(Src) Then
            Throw New DirectoryNotFoundException("The directory " & Src & " does not exists")
        End If

        'add Directory Seperator Character (\) for the string concatenation shown later
        If Dest.Substring(Dest.Length - 1, 1) <> Path.DirectorySeparatorChar Then
            Dest += Path.DirectorySeparatorChar
        End If
        If Not Directory.Exists(Dest) Then Directory.CreateDirectory(Dest)
        Dim Files As String()
        Files = Directory.GetFileSystemEntries(Src)
        Dim element As String
        For Each element In Files
            If Directory.Exists(element) Then
                'if the current FileSystemEntry is a directory,
                'call this function recursively
                CopyDirectory(element, Dest & Path.GetFileName(element), True)
            Else
                'the current FileSystemEntry is a file so just copy it
                'the file Trinity.ini should not be copied
                'The database files should not be copied
                If Not String.Equals(element, "Trinity.ini") AndAlso Not String.Equals(element, "Trinity.mdb") Then
                    File.Copy(element, Dest & Path.GetFileName(element), True)
                End If
            End If
        Next
        Return True
    End Function

End Class