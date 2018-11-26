Public Class cLanguage

    Private _languageFile As String = "default.ini"
    Private langINI As New cINI

    Public Sub SetLanguageFile(ByVal LanguageFile As String)
        _languageFile = LanguageFile
        langINI.Create(BalthazarSettings.DataFolder & "Language\" & _languageFile)
    End Sub

    Public Function CurrentLanguage() As String
        Return langINI.ReadString("Information", "Language")
    End Function

    Public Function GetLangagueInFile(ByVal LanguageFile As String)
        Dim TmpFile As New cINI
        TmpFile.Create(BalthazarSettings.DataFolder & "Language\" & LanguageFile)
        Return TmpFile.ReadString("Information", "Language")
    End Function

    Public Function GetLanguageFiles() As List(Of String)
        Dim TmpList As New List(Of String)
        For Each s As String In My.Computer.FileSystem.GetFiles(BalthazarSettings.DataFolder)
            s = s.Substring(s.LastIndexOf("\") + 1)
            TmpList.Add(s)
        Next
        Return TmpList
    End Function

    Public Sub New()
        langINI.Create(BalthazarSettings.DataFolder & "Language\" & _languageFile)
    End Sub

    Default ReadOnly Property Translate(ByVal English As String)
        Get
            Return langINI.ReadString("Language", English, English)
        End Get
    End Property

End Class
