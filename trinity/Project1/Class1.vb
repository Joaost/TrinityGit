Imports System.Net.HttpWebResponse
Imports System.Net.HttpWebRequest
Imports DevExpress.Data
Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web.AspNetHostingPermission
Imports System.HttpStyleUriParser
Imports System.Web.Script.Serialization
Imports System.Collections.Generic
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.Diagnostics
Imports System.Net.Http.Headers
Imports System.Net.Http
Imports Spire.Pdf


Public Class Class1




    Public Sub Connect()





    End Sub


End Class
Module mainModule


    Private uriUsers As New Uri("https://api.mediatool.com/users/5857639464803641/organizations")
    Private uriGetCampagins As New Uri("https://api.mediatool.com/organizations/2809029426090053/campaigns")
    Private uriBulkUpload As New Uri("https://api.mediatool.com/organizations/2809029426090053/bulk/mediaentries")

    Private token As String = "4944a94cf3fc51a8f9281eee7b20fda199d5c1fa"
    Sub Main()


        'ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3

        'Dim request As Net.HttpWebRequest = Net.HttpWebRequest.Create(url)


        'request.Method = "GET"
        'request.UserAgent = "api-explorer-client "
        'request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token)

        'request.Headers.Add("Authorization", "Bearer " & token)
        'request.ContentType = "Application/Json"

        'request.GetRequestStreamAsync()
        '' Get the response.
        'Dim response As WebResponse = request.GetResponse
        '' Display the status.
        'Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
        '' Get the stream containing content returned by the server.
        'Dim dataStream As Stream = response.GetResponseStream()
        '' Open the stream using a StreamReader for easy access.
        'Dim reader As New StreamReader(dataStream)
        '' Read the content.
        'Dim responseFromServer As String = reader.ReadToEnd()
        '' Display the content.
        'Console.WriteLine(responseFromServer)
        '' Clean up the streams and the response.
        'reader.Close()
        'response.Close()

        'recieveListOfUserId()

        'insertCampaign()

        parsePDF()
    End Sub

    Sub parsePDF()
        Dim tempPDF As PdfDocument = New PdfDocument()

        'tempPDF.LoadFromFile("C:\Users\joakim.koch\Trinity 4.0\NENT_Mediacom_Tele2_Tele_2_Residential_w48-w51_Linear_Run_By_Station.pdf")

        'tempPDF.SaveToFile("tele2_nent.xlsx", FileFormat.XLSX)

        'tempPDF.Close()

        'Create a PdfDocument object
        Dim pdf As PdfDocument = New PdfDocument()

        'Load a PDF document
        pdf.LoadFromFile("C:\Users\joakim.koch\Trinity 4.0\NENT_Mediacom_Tele2_Tele_2_Residential_w48-w51_Linear_Run_By_Station.pdf")

        'Save to Excel
        pdf.SaveToFile("ToExcel.xlsx", FileFormat.XLSX)


        Dim excelDoc As exc
    End Sub
    Sub recieveListOfUserId()

        Dim request = TryCast(System.Net.WebRequest.Create(uriUsers), System.Net.HttpWebRequest)
        request.Method = "GET"

        request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token)
        request.Headers.Add("auth_access_type", "read")
        request.ContentLength = 0
        Dim responseContent As String
        Using response = TryCast(request.GetResponse(), System.Net.HttpWebResponse)
            Using reader = New System.IO.StreamReader(response.GetResponseStream())
                responseContent = reader.ReadToEnd()
            End Using
        End Using



        Dim s As String

        Dim jss As New JavaScriptSerializer()
        Dim myObject As New Dictionary(Of String, String)
        Dim myDictionary As New Dictionary(Of String, Object)
        Dim JObject As Object = Nothing
        ' Dim jsonObj As JObject = JObject.Parse(responseContent)
        Dim jsonObj As Object = jss.DeserializeObject(responseContent)
        'myObject = jss.Deserialize(Of Dictionary(Of String, String))(responseContent)




        's = myObject("campaigns")
    End Sub

    Public Class JSON_Post
        Public Property organizationId As String
        Public Property mediaTypeId As String
        Public Property vehicleId As String
        Public Property startDate As Date
        Public Property endDate As Date
    End Class
    Sub insertCampaign()

        ' Initiate webrequest
        Dim request = TryCast(System.Net.WebRequest.Create(uriBulkUpload), System.Net.HttpWebRequest)

        request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token)
        request.Headers.Add("auth_access_type", "read")
        request.ContentLength = 0

        ' Since we want to post/create campaign
        request.Method = "POST"
        'Specify type of content we are trying to send'
        request.ContentType = "application/json"
        ' Bearer with uniqe token for TOLE (edit this to a dynamic parameter)
        request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token)

        Dim jss As New JavaScriptSerializer()

        Dim rawresp As String
        rawresp = "bulkOps:  [{
                ""type"":""create"",
                ""data"":""{
                    ""organizationId"":""2809029426090053"",
                    ""mediaTypeId"":""TV4"",
                    ""vehicleId"":""5823196431916249"",
                    ""startDate"":""2022-03-02"",
                    ""endDate"":""2022-03-02"",
                    ""price"":""999999"",
                    ""trp"":""15""
                }]"

        ' Test Using a Public Class with properties
        Dim NewData As New JSON_Post
        NewData.organizationId = "2809029426090053"
        NewData.mediaTypeId = "TV4"
        NewData.vehicleId = "5823196431916249"
        NewData.startDate = "2022-03-02"
        NewData.endDate = "2022-03-02"

        Dim PostStringTwo As String = jss.Serialize(rawresp)
        Dim PostString As String = jss.Serialize(NewData)
        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(PostStringTwo)
        'request.ContentLength = byteArray.Length
        'Dim dataStream As Stream = request.GetRequestStream()
        'dataStream.Write(byteArray, 0, byteArray.Length)


        Dim request2 As HttpWebRequest = HttpWebRequest.Create(uriBulkUpload)
        request2.Method = "POST"
        request2.ContentType = "text/html"
        request2.Headers.Add("auth_access_type", "read")
        request2.ContentLength = byteArray.Length
        request2.KeepAlive = True

        Dim requestStream As Stream = request2.GetRequestStream()
        Dim postBytes As Byte() = Encoding.ASCII.GetBytes(PostStringTwo)
        requestStream.Write(postBytes, 0, postBytes.Length)
        requestStream.Close()

        'NewData.organizationId = ""
        'Dim responseContent As String = ""


        'Using response = request.GetRequestStream(TryCast(rawresp, TransportContext))
        '    Using reader = New System.IO.StreamReader(response.GetResponseStream())
        '        responseContent = reader.ReadToEnd()
        '    End Using
        'End Using


    End Sub
    Private Function AcceptAll(ByVal sender As Object, ByVal certificate As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function

    Private Class JObject
    End Class
End Module