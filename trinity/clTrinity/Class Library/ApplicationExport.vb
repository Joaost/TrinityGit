Imports System.ComponentModel.Composition
Imports TrinityPlugin

<Export(GetType(ITrinityApplication))>
Public Class ApplicationExport
    Implements ITrinityApplication

    Public Property ActiveCampaign As Object Implements TrinityPlugin.ITrinityApplication.ActiveCampaign
        Get
            Return Campaign
        End Get
        Private Set(value As Object)
            Campaign = value
        End Set
    End Property

    Public Function GetNetworkPreference(Category As String, Key As String) As String Implements TrinityPlugin.ITrinityApplication.GetNetworkPreference
        Return TrinitySettings.GetNetworkPreference(Category, Key)
    End Function

    Public Function GetUserPreference(Category As String, Key As String) As String Implements TrinityPlugin.ITrinityApplication.GetUserPreference
        Return TrinitySettings.GetUserPreference(Category, Key)
    End Function

    Public Sub SetUserPreference(Category As String, Key As String, Value As String) Implements TrinityPlugin.ITrinityApplication.SetUserPreference
        TrinitySettings.SetUserPreference(Category, Key, Value)
    End Sub

    Public Sub SetNetworkPreference(Category As String, Key As String, Value As String) Implements TrinityPlugin.ITrinityApplication.SetNetworkPreference
        TrinitySettings.SetNetworkPreference(Category, Key, Value)
    End Sub

    Public Function GetSharedNetworkPreference(Category As String, Key As String) As String Implements TrinityPlugin.ITrinityApplication.GetSharedNetworkPreference
        Return TrinitySettings.GetSharedNetworkPreference(Category, Key)
    End Function

    Public Function GetUserDataPath() As String Implements TrinityPlugin.ITrinityApplication.GetUserDataPath
        Return TrinitySettings.LocalDataPath
    End Function
End Class
