Imports System.Net.HttpWebResponse
Imports System.Net.HttpWebRequest
Imports DevExpress.Data
Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Public Class connect




    Private url As String = "https://api.mediatool.com"

    Private token As String = "4944a94cf3fc51a8f9281eee7b20fda199d5c1fa"



    Public Sub New()


        ' Create a request for the URL. 		


        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3

        Dim request As Net.HttpWebRequest = Net.HttpWebRequest.Create(url)

        System.Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Ssl3
        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf AcceptAll)
        request.Credentials = CredentialCache.DefaultCredentials

        request.Method = "GET"
        request.UserAgent = "api-explorer-client "
        request.Headers.Add("Authorization", "Bearer " & token)
        request.ContentType = "Application/Json"



        Dim response As Net.HttpWebResponse = request.GetResponse

        Debug.Print(response.ToString())




    End Sub
    Private Function AcceptAll(ByVal sender As Object, ByVal certificate As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function
End Class
