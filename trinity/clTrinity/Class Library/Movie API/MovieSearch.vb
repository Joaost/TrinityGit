Namespace MovieSearch
    Public Class MovieSearch

        Public Event SearchResult(Result As Movie)

        Public Function Search(SearchTerm As String) As Movie
            Dim _res As New Movie
            Try
                Dim _tmdb As New TMDB.Search
                Dim _tmdbMovie As TMDB.Movie = _tmdb.Search(SearchTerm).First

                Dim _rt As New RottenTomatoes.Search
                Dim _rtMovie As RottenTomatoes.Movie = _rt.ImdbSearch(_tmdbMovie.IMDbID)

                _res.TMDB = _tmdbMovie
                _res.RottenTomatoes = _rtMovie

                RaiseEvent SearchResult(_res)
            Catch
                _res = Nothing
            End Try
            Return _res
        End Function

        Public Sub SearchAsync(SearchTerm As String)
            Dim _thread As New Threading.Thread(AddressOf Search)

            _thread.Start(SearchTerm)
        End Sub
    End Class
End Namespace
