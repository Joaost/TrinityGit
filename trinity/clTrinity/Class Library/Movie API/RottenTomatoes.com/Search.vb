Imports System.Net
Imports System.Runtime.Serialization.Json

Namespace RottenTomatoes
    Public Class Search
        Private Const _searchUrl As String = "http://api.rottentomatoes.com/api/public/v1.0/movies.json?q={0}&apikey=fb4kjqn24953ummj5bkq5zs2"
        Private Const _imdbSearchUrl As String = "http://api.rottentomatoes.com/api/public/v1.0/movie_alias.json?id={0}&apikey=fb4kjqn24953ummj5bkq5zs2&type=imdb"

        Public Event SearchResult(Result As SearchResult)

        Public Function Search(SearchTerm As String) As SearchResult
            Dim request As WebRequest = WebRequest.Create(String.Format(_searchUrl, SearchTerm))
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)

            Dim _serializer As New DataContractJsonSerializer(GetType(SearchResult))

            Dim _res As SearchResult = _serializer.ReadObject(response.GetResponseStream)

            RaiseEvent SearchResult(_res)
            Return _res
        End Function

        Public Function ImdbSearch(id As String) As Movie
            Dim request As WebRequest = WebRequest.Create(String.Format(_imdbSearchUrl, id.TrimStart("tt")))
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)

            Dim _serializer As New DataContractJsonSerializer(GetType(Movie))

            Dim _res As Movie = _serializer.ReadObject(response.GetResponseStream)

            Return _res
        End Function

        Public Sub SearchAsync(SearchTerm As String)
            Dim _thread As New Threading.Thread(AddressOf Search)

            _thread.Start(SearchTerm)

        End Sub

    End Class
End Namespace