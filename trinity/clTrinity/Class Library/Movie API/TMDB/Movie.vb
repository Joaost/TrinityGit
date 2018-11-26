Imports System.Runtime.Serialization

Namespace TMDB
    <DataContract()>
    Public Class Movie

        <DataMember(Name:="name")>
        Public Property Title As String

        <DataMember(Name:="original_name")>
        Public Property OriginalTitle As String

        <DataMember(Name:="rating")>
        Public Property Rating As Single

        <DataMember(Name:="certification")>
        Public Property Certification As String

        <DataMember(Name:="imdb_id")>
        Public Property IMDbID As String

        <DataMember(Name:="overview")>
        Public Property Overview As String

        <DataMember(Name:="posters")>
        Public Property Posters As List(Of Image)

    End Class
End Namespace