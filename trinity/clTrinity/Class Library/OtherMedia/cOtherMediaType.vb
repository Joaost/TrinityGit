Namespace Trinity
    Public Class cOtherMediaType

        Property Name As String
        Property FixedWeeks As Boolean = False
        Property FixedWeekStartWeekday As Integer = 0
        Property FixedWeekEndWeekday As Integer = 6

        Private _main As cKampanj

        Public ReadOnly Property Initialized As Boolean
            Get
                Return _weekTemplates.Count > 0
            End Get
        End Property

        Private _medias As New cOtherMedias
        Public ReadOnly Property Medias() As cOtherMedias
            Get
                Return _medias
            End Get
        End Property

        Private _weekTemplates As New cOtherMediaWeeks
        Public ReadOnly Property WeekTemplates() As cOtherMediaWeeks
            Get
                Return _weekTemplates
            End Get
        End Property

        Private _availableMedias As List(Of cOtherMedia)
        Public Function GetAvailableMedias() As List(Of cOtherMedia)
            If _availableMedias Is Nothing Then

            End If
            Return _availableMedias
        End Function

        Public Sub New(Main As Trinity.cKampanj)
            _main = Main
        End Sub

        Sub Init()
            Dim Ini As New clsIni

            Ini.Create(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & _main.Area & "\Other\" & Name & ".ini")

            FixedWeeks = Ini.Data("General", "FixedWeeks")
            FixedWeekStartWeekday = Ini.Data("General", "FixedWeekStartWeekday")
            FixedWeekEndWeekday = Ini.Data("General", "FixedWeekEndWeekday")

        End Sub

    End Class
End Namespace