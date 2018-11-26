Imports System.Runtime.Serialization

Namespace RottenTomatoes
    <DataContract()>
    Public Class Rating

        <DataMember(Name:="critics_score")>
        Public Property CriticsScore As Integer

        <DataMember(Name:="audience_score")>
        Public Property AudienceScore As Integer

    End Class
End Namespace