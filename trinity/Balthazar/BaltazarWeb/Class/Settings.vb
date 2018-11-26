Public Class Settings
    Public Shared ReadOnly Property DatabaseServer() As String
        Get
            Dim INI As New cINI            
            INI.Create(My.Application.Info.DirectoryPath & "\BalthazarSettings.ini")
            If INI.ReadString("Database", "Server") = "" Then
                INI.Write("Database", "Server", "instore.mecaccess.se")
                'INI.Write("Database", "Server", "sto-app60")
            End If
            Return INI.ReadString("Database", "Server")
        End Get
    End Property

    Public Shared ReadOnly Property DatabaseTable() As String
        Get
            Dim INI As New cINI
            INI.Create(My.Application.Info.DirectoryPath & "\BalthazarSettings.ini")
            If INI.ReadString("Database", "Table") = "" Then
                INI.Write("Database", "Table", "balthazar")
            End If
            Return INI.ReadString("Database", "Table")
        End Get
    End Property
End Class
