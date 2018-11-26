Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Public Class BalthazarSettings

    Private Shared LocalINI As New cINI
    Private Shared NetworkINI As New cINI

#Region "Database"
    Shared Property DatabaseDB() As String
        Get
            Setup()
            Return LocalINI.ReadString("Database", "DB")
        End Get
        Set(ByVal value As String)
            Setup()
            LocalINI.Write("Database", "DB", value)
        End Set
    End Property

    Shared Function ConnectionString() As String
        Setup()
        Return LocalINI.ReadString("Database", "ConnectionString", "")
    End Function

    Shared Property Database() As String
        Get
            Setup()
            Return LocalINI.ReadString("Database", "Path")
        End Get
        Set(ByVal value As String)
            Setup()
            LocalINI.Write("Database", "Path", value)
        End Set
    End Property

    Shared ReadOnly Property DatabaseType() As String
        Get
            Setup()
            Return LocalINI.ReadString("Database", "Type", "")
        End Get
    End Property
#End Region

    Private Shared Sub Setup()
        LocalINI.Create(Helper.LocalDataPath & "Setup.ini")
        If My.Computer.FileSystem.FileExists(Helper.Pathify(LocalINI.ReadString("Paths", "Datapath")) & "Setup.ini") Then
            NetworkINI.Create(Helper.Pathify(LocalINI.ReadString("Paths", "Datapath")) & "Setup.ini")
        Else
            NetworkINI = LocalINI
        End If
    End Sub

    Shared Function DataFolder() As String
        Return Helper.Pathify(LocalINI.ReadString("Paths", "Datapath"))
    End Function

    Shared Function NotesServer() As String
        Return "STO-DMS02/Stockholm/Media"
    End Function

    Shared Function NotesMailFile() As String
        Return "jhogfeld"
    End Function

    Shared Property UserEmail() As String
        Get
            Return LocalINI.ReadString("PersonalInfo", "Email", "")
        End Get
        Set(ByVal value As String)
            LocalINI.Write("PersonalInfo", "Email", value)
        End Set
    End Property

    Shared Property UserName() As String
        Get
            Return LocalINI.ReadString("PersonalInfo", "Name", "")
        End Get
        Set(ByVal value As String)
            LocalINI.Write("PersonalInfo", "Name", value)
        End Set
    End Property
End Class
