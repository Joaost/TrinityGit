Public Class cClient
    Public ID As Integer
    Public Name As String
    Public Products As New List(Of cProduct)

    Public Overrides Function ToString() As String
        Return Name
    End Function

End Class
