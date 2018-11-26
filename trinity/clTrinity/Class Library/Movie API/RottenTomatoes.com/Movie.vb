Imports System.Runtime.Serialization

Namespace RottenTomatoes
    <DataContract()>
    Public Class Movie

        <DataMember(Name:="title")>
        Public Property Title As String

        <DataMember(Name:="ratings")>
        Public Property Ratings As Rating

    End Class
End Namespace