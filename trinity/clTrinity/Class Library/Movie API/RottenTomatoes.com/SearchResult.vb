Imports System.Runtime.Serialization

Namespace RottenTomatoes

    <DataContract()>
    Public Class SearchResult

        <DataMember(Name:="total")>
        Property Total As Integer

        <DataMember(Name:="movies")>
        Property Movies As List(Of Movie)

    End Class

End Namespace
