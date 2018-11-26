Namespace Trinity

    Public Class FilmChangedArgs : Inherits EventArgs

        Public ReadOnly Film As cFilm

        Public Sub New(ByVal film As cFilm)
            Me.Film = film
        End Sub
    End Class
End Namespace
