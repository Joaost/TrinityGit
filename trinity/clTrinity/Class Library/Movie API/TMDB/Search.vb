Imports System.Net
Imports System.Runtime.Serialization.Json
Imports System.Web

Namespace TMDB
    Public Class Search
        Private Const _searchUrl As String = "http://api.themoviedb.org/2.1/Movie.search/sv/json/0953b6723ebfdc9deb325323bbe2480e/{0}"

        Public Event SearchResult(Result As List(Of Movie))

        Public Function Search(SearchTerm As String) As List(Of Movie)
            Dim request As WebRequest = WebRequest.Create(String.Format(_searchUrl, HttpUtility.UrlEncode(SearchTerm)))
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)

            Dim _res As List(Of Movie)
            Try
                Dim _serializer As New DataContractJsonSerializer(GetType(List(Of Movie)), New Type() {GetType(Image), GetType(List(Of Image))})

                _res = _serializer.ReadObject(response.GetResponseStream)
            Catch
                _res = New List(Of Movie)
            End Try
            RaiseEvent SearchResult(_res)
            Return _res
        End Function

        Public Sub SearchAsync(SearchTerm As String)
            Dim _thread As New Threading.Thread(AddressOf Search)

            _thread.Start(SearchTerm)

        End Sub

    End Class
End Namespace