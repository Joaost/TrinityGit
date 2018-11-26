Namespace Trinity
    Public Class FilmFilmcodeArgs : Inherits FilmChangedArgs

        Public ReadOnly FilmCode As String

        Public Sub New(ByVal film As cFilm, ByVal filmCode As String)
            MyBase.New(film)
            Me.FilmCode = filmCode
        End Sub
    End Class
End Namespace