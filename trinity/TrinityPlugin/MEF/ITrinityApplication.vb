Public Interface ITrinityApplication

    Property ActiveCampaign As Object

    Function GetNetworkPreference(Category As String, Key As String) As String

    Function GetUserPreference(Category As String, Key As String) As String

    Function GetSharedNetworkPreference(Category As String, Key As String) As String

    Sub SetNetworkPreference(Category As String, Key As String, Value As String)

    Sub SetUserPreference(Category As String, Key As String, Value As String)

    Function GetUserDataPath() As String

End Interface
