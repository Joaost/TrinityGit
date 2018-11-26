Imports System.Runtime.Serialization

<DataContract()>
Public Class Preferences
    <DataMember()>
    Public Property Username As String

    <DataMember()>
    Public Property TV4Contact As String

    Private _password As String
    <DataMember()>
    Public Property Password As String
        Get
            Return _password
        End Get
        Set(value As String)
            _password = value
        End Set
    End Property

    Public Function GetPlainTextPassword() As String
        Return Decrypt(_password)
    End Function

    Public Sub SetPlainTextPassword(password As String)
        _password = Encrypt(password)
    End Sub

    Private Function Encrypt(plainText As String)
        Dim _nr = Math.Sin(plainText.Length + 1)
        Randomize(_nr)
        Dim _chars() As Char = plainText.ToCharArray
        For i As Integer = 0 To plainText.Length - 1
            _nr = Rnd(-1)
            _chars(i) = Chr(Asc(_chars(i)) - Int(_nr * 20 - 10))
            Randomize(_nr)
        Next
        Return New String(_chars)
    End Function

    Private Function Decrypt(cipherText As String) As String
        If String.IsNullOrEmpty(cipherText) Then
            Return ""
        End If
        Dim _nr = Math.Sin(cipherText.Length + 1)
        Randomize(_nr)
        Dim _chars() As Char = cipherText.ToCharArray
        For i As Integer = 0 To cipherText.Length - 1
            _nr = Rnd(-1)
            _chars(i) = Chr(Asc(_chars(i)) + Int(_nr * 20 - 10))
            Randomize(_nr)
        Next
        Return New String(_chars)

    End Function

End Class
