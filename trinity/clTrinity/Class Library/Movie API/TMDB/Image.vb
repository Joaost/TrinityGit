Imports System.Runtime.Serialization


Namespace TMDB
    <DataContract()>
    Public Class Image
        <DataMember(Name:="image")>
        Public Property Info As ImageInfo

    End Class
End Namespace
