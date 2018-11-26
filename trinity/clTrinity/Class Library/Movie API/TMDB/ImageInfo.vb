Imports System.Runtime.Serialization


Namespace TMDB
    <DataContract()>
    Public Class ImageInfo
        <DataMember(Name:="size")>
        Public Property Size As String

        <DataMember(Name:="url")>
        Public Property URL As String

    End Class
End Namespace
