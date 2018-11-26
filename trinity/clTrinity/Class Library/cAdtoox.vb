Imports System.Xml
Imports System.Net
Imports System.IO

'Class Adtoox
'    GetCustomersForUser()
'        -Loads the customers the user has access to into Adtoox.CustomerCollection
'    GetFilmcodesForCustomer(customer as String)
'        -Loads all filmcodes for a customer that the user has access to into Adtoox.Filmcodecollection
'    getSingleFilmCodeInfo(ByVal copyCode As String) As Trinity.cAdTooxFilmcode
'        -Returns complete information about a copycode including status in it's statuses collection

'Class AdtooxFilmcodes
'       - Collection of AdtooxFilmcode
'    GetPasswordForFilmcode(copycode as string)

'Class AdtooxCustomers
'       -Collection of AdtooxCustomer

'Class cAdtooxFilmcode
'GetStatus(username as string,password as string) as Collection
'    - Loads the status for this filmcode into the films collection of statuses, one for each channel
'       If not called, the filmcodes have no statuses
'       After this is called, you can retrieve the status of the filmcode with the Statuses() property
'GetLinkToPlayVideo() as String
'    - Gets a link to where the video can be played
'GetLinkToUploadStatus(username as String)
'    - Gets a link to where the upload status can be seen (on the Adtoox site)
'GetPassword() as String
'    - Gets the password for this video

'Plus properties for all the characteristics of the video like length, title, brand, etc

Namespace Trinity

    Public Class cAdtoox

        Private CustomerCollection As Trinity.cAdtooxCustomers
        Private DivisionCollection As Trinity.cAdtooxDivisions
        Private BrandCollection As Trinity.cAdtooxBrands
        Private FilmCodeCollection As Trinity.cAdTooxFilmCodes
        Private MediaCollection As Trinity.cAdtooxMedias

        Friend username As String
        Friend password As String

        Dim HTTP As System.Net.WebClient
        Dim ReceiveXML As New System.Xml.XmlDocument


        Public Function GetCustomersForUser() As Trinity.cAdtooxCustomers
            Dim ResourceURL As String = "https://ecexpress.adtoox.com/ec/publicapi.jsp"
            Dim Action As String = "?username=" & username & "&sid_or_pwd=" & password & "&method=GetLocalities&skin=groupm.xsl"

            Dim request As WebRequest = WebRequest.Create(ResourceURL & Action)
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            CustomerCollection = New Trinity.cAdtooxCustomers

            ReceiveXML.LoadXml(responseFromServer)

            For Each tmpXMLNode As XmlElement In ReceiveXML.GetElementsByTagName("advertiser")
                Dim newCustomer As New cAdtooxCustomer
                newCustomer.ID = tmpXMLNode.GetAttribute("id")
                newCustomer.Name = tmpXMLNode.GetAttribute("name")
                CustomerCollection.add(newCustomer)
            Next
            Return CustomerCollection
        End Function

        Public Function GetDivisionsForCustomer(ByVal CustomerID As String) As Trinity.cAdtooxDivisions

            'https://ecexpress.adtoox.com/ec/publicapi.jsp?username=trinity@adtoox.com&
            'sid_or_pwd=xxx&method=GetDivisions&locality_id=39&region_code=se&skin=groupm.xsl

            Dim ResourceURL As String = "https://ecexpress.adtoox.com/ec/publicapi.jsp"
            Dim Action As String = "?username=" & username & "&sid_or_pwd=" & password & "&method=GetDivisions&locality_id=" & CustomerID & "&region_code=se&skin=groupm.xsl"

            Dim request As WebRequest = WebRequest.Create(ResourceURL & Action)
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            DivisionCollection = New Trinity.cAdtooxDivisions

            ReceiveXML.LoadXml(responseFromServer)

            For Each tmpXMLNode As XmlElement In ReceiveXML.GetElementsByTagName("division")
                Dim newDivision As New cAdtooxDivision
                newDivision.ID = tmpXMLNode.GetAttribute("id")
                newDivision.Name = tmpXMLNode.GetAttribute("name")
                DivisionCollection.add(newDivision)
            Next
            Return DivisionCollection
        End Function


        Public Function GetBrandsForDivision(ByVal DivisionID As String) As Trinity.cAdtooxBrands

            'https://ecexpress.adtoox.com/ec/publicapi.jsp?username=trinity@adtoox.com&
            'sid_or_pwd=xxx&method=GetBrands&division_id=97565&skin=groupm.xsl

            Dim ResourceURL As String = "https://ecexpress.adtoox.com/ec/publicapi.jsp"
            Dim Action As String = "?username=" & username & "&sid_or_pwd=" & password & "&method=GetBrands&division_id=" & DivisionID & "&skin=groupm.xsl"

            Dim request As WebRequest = WebRequest.Create(ResourceURL & Action)
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            BrandCollection = New Trinity.cAdtooxBrands

            ReceiveXML.LoadXml(responseFromServer)

            For Each tmpXMLNode As XmlElement In ReceiveXML.GetElementsByTagName("brand")
                Dim newBrand As New cAdtooxBrand
                newBrand.ID = tmpXMLNode.GetAttribute("id")
                newBrand.Name = tmpXMLNode.GetAttribute("name")
                BrandCollection.add(newBrand)
            Next
            Return BrandCollection
        End Function

        Public Function GetMediaForCountry(ByVal Country As String) As Trinity.cAdtooxMedias
            'https://ecexpress.adtoox.com/ec/publicapi.jsp?username=trinity@adtoox.com&
            'sid_or_pwd=xxx&method=GetMedias&region_code=se&skin=groupm.xsl

            Dim ResourceURL As String = "https://ecexpress.adtoox.com/ec/publicapi.jsp"
            Dim Action As String = "?username=" & username & "&sid_or_pwd=" & password & "&method=GetMedias&region_code=" & Country.ToLower & "&skin=groupm.xsl"

            Dim request As WebRequest = WebRequest.Create(ResourceURL & Action)
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            MediaCollection = New Trinity.cAdtooxMedias

            ReceiveXML.LoadXml(responseFromServer)

            For Each tmpXMLNode As XmlElement In ReceiveXML.GetElementsByTagName("media")
                Dim newBrand As New cAdtooxMedia
                newBrand.ID = tmpXMLNode.GetAttribute("id")
                newBrand.Name = tmpXMLNode.GetAttribute("name")
                MediaCollection.add(newBrand)
            Next
            Return MediaCollection

        End Function

        Friend Function UserHasAccess(ByVal CopyCode As String) As Boolean
            Dim ResourceURL As String = "https://ecexpress.adtoox.com/ec/publicapi.jsp"
            If username = "" OrElse CopyCode = "" Then Return False
            Dim Action As String = "?username=" & username & "&sid_or_pwd=" & password & "&method=HasCopyCodeReaderAccess&copycode=" & CopyCode & "&add_copy_envelope=true&skin=groupm.xsl"
            Dim TmpStr As String

            HTTP = New System.Net.WebClient
            HTTP.Credentials = System.Net.CredentialCache.DefaultCredentials
            TmpStr = HTTP.UploadString(ResourceURL & Action, "POST", "")
            ReceiveXML.LoadXml(TmpStr)
            If ReceiveXML.GetElementsByTagName("responsedata")(0) IsNot Nothing AndAlso ReceiveXML.GetElementsByTagName("responsedata")(0).Attributes.GetNamedItem("success").Value = "true" Then
                If ReceiveXML.SelectSingleNode("responsedata/data/hascopycodereaderaccess").Attributes("value").Value = "true" Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Function

        Public Function getSingleFilmCodeInfo(ByVal copyCode As String) As Trinity.cAdTooxFilmcode

            If Not UserHasAccess(copyCode) Then
                Return New Trinity.cAdTooxFilmcode(Me)
            End If

            Dim timer As New Stopwatch
            timer.Start()

            'While timer.ElapsedMilliseconds < 10000

            Dim film As New Trinity.cAdTooxFilmcode(Me)

            Dim HTTP As System.Net.WebClient
            Dim ReceiveXML As New System.Xml.XmlDocument

            Dim ResourceURL As String = "https://ecexpress.adtoox.com/ec/publicapi.jsp"
            If username = "" Then Return Nothing
            Dim Action As String = "?username=" & username & "&sid_or_pwd=" & password & "&method=GetCopyCode&copycode=" & copyCode & "&add_copy_envelope=true&skin=groupm.xsl"
            Dim TmpStr As String
            Dim status As Trinity.cAdTooxStatus

            HTTP = New System.Net.WebClient
            HTTP.Credentials = System.Net.CredentialCache.DefaultCredentials
            TmpStr = HTTP.UploadString(ResourceURL & Action, "POST", "")
            ReceiveXML.LoadXml(TmpStr)

            If ReceiveXML.GetElementsByTagName("responsedata")(0).Attributes.GetNamedItem("success").Value = "true" Then
                For Each tmpXMLNode As XmlElement In ReceiveXML.GetElementsByTagName("copycode")
                    film.CopyCode = tmpXMLNode.GetAttribute("code")
                    film.Length = tmpXMLNode.GetAttribute("film_length")
                    Try
                        film.FirstAirdate = tmpXMLNode.GetAttribute("first_airdate")
                    Catch ex As Exception
                        film.FirstAirdate = Nothing
                    End Try

                    film.Region = tmpXMLNode.GetAttribute("region")
                    film.Title = tmpXMLNode.GetAttribute("title")
                    If Not Date.TryParse(tmpXMLNode.GetAttribute("upload_deadline"), film.UploadDeadline) Then
                        film.UploadDeadline = Nothing
                    End If

                    film.AccessPassword = tmpXMLNode.GetAttribute("access_password")

                    For Each tmpXMLNode2 As XmlElement In tmpXMLNode.GetElementsByTagName("brand")
                        film.BrandID = tmpXMLNode2.GetAttribute("id")
                        film.BrandName = tmpXMLNode2.GetAttribute("name")

                    Next
                    For Each tmpXMLNode2 As XmlElement In tmpXMLNode.GetElementsByTagName("advertiser")
                        film.AdvertiserID = tmpXMLNode2.GetAttribute("id")
                        film.AdvertiserName = tmpXMLNode2.GetAttribute("name")
                    Next
                Next

                For Each tmpXMLNode As XmlElement In ReceiveXML.GetElementsByTagName("status")
                    status = New Trinity.cAdTooxStatus
                    status.MediaID = tmpXMLNode.GetAttribute("media_id")
                    status.MediaName = tmpXMLNode.GetAttribute("media_name")
                    For temp As Integer = 1 To 8
                        status.Steps(temp - 1) = tmpXMLNode.GetAttribute("step" & temp)
                    Next
                    film.addStatus(status)
                Next

                Return film
            Else
                Throw New Exception(ReceiveXML.GetElementsByTagName("responsedata")(0).Attributes.GetNamedItem("error_message").Value)
            End If
            'End While
        End Function

        Public Sub New()
            username = TrinitySettings.AdtooxUsername
            password = TrinitySettings.AdtooxPassword

            CustomerCollection = New Trinity.cAdtooxCustomers
            DivisionCollection = New Trinity.cAdtooxDivisions
            BrandCollection = New Trinity.cAdtooxBrands


            FilmCodeCollection = New Trinity.cAdTooxFilmCodes(username, password)
        End Sub

        Public Sub New(ByVal user As String, ByVal pass As String)
            CustomerCollection = New Trinity.cAdtooxCustomers
            FilmCodeCollection = New Trinity.cAdTooxFilmCodes(username, password)
            DivisionCollection = New Trinity.cAdtooxDivisions
            BrandCollection = New Trinity.cAdtooxBrands

            username = user
            password = pass
        End Sub

        Public Function GetFilmCodesForCustomer(ByVal Customer As String) As cAdTooxFilmCodes

            Dim ResourceURL As String = "https://ecexpress.adtoox.com/ec/publicapi.jsp"
            Dim Action As String = "?username=" & username & "&sid_or_pwd=" & password & "&method=GetCopyCodesByLocality&locality_id=" & Customer & "&skin=groupm.xsl"

            Dim request As WebRequest = WebRequest.Create(ResourceURL & Action)
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()

            ReceiveXML.LoadXml(responseFromServer)

            For Each tmpXMLNode As XmlElement In ReceiveXML.GetElementsByTagName("copycode")
                Dim newFilm As New cAdTooxFilmcode(Me)
                newFilm.CopyCode = tmpXMLNode.GetAttribute("code")
                newFilm.Length = tmpXMLNode.GetAttribute("film_length")
                Try
                    newFilm.FirstAirdate = CDate(tmpXMLNode.GetAttribute("first_airdate"))
                Catch ex As Exception
                    newFilm.FirstAirdate = Nothing
                End Try

                newFilm.Region = tmpXMLNode.GetAttribute("region")
                newFilm.Title = tmpXMLNode.GetAttribute("title")
                Try
                    newFilm.UploadDeadline = CDate(tmpXMLNode.GetAttribute("upload_deadline"))
                Catch
                    newFilm.UploadDeadline = Nothing
                End Try
                newFilm.AccessPassword = tmpXMLNode.GetAttribute("access_password")
                For Each tmpXMLNode2 As XmlElement In tmpXMLNode.GetElementsByTagName("brand")
                    newFilm.BrandID = tmpXMLNode2.GetAttribute("id")
                    newFilm.BrandName = tmpXMLNode2.GetAttribute("name")

                Next
                For Each tmpXMLNode2 As XmlElement In tmpXMLNode.GetElementsByTagName("advertiser")
                    newFilm.AdvertiserID = tmpXMLNode2.GetAttribute("id")
                    newFilm.AdvertiserName = tmpXMLNode2.GetAttribute("name")
                Next
                FilmCodeCollection.add(newFilm)
            Next
            Return FilmCodeCollection
        End Function

    End Class

    Public Class cAdtooxCustomers
        Implements IEnumerable

        Private mCol As New Collection

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator
        End Function

        Public Sub add(ByVal Customer As cAdtooxCustomer)

            Try
                mCol.Add(Customer, Customer.ID)
            Catch
            End Try

        End Sub

        Public Function Count() As Integer
            Return mCol.Count
        End Function

    End Class

    Public Class cAdtooxDivisions
        Implements IEnumerable

        Private mCol As New Collection

        Public Sub add(ByVal Division As Trinity.cAdtooxDivision)
            Try
                mCol.Add(Division, Division.ID)
            Catch
            End Try
        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator
        End Function
    End Class

    Public Class cAdtooxMedias
        Implements IEnumerable

        Private mCol As New Collection

        Public Sub add(ByVal Media As Trinity.cAdtooxMedia)
            Try
                mCol.Add(Media, Media.ID)
            Catch
            End Try
        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator
        End Function
    End Class

    Public Class cAdtooxBrands
        Implements IEnumerable
        Private mCol As New Collection

        Public Sub add(ByVal brand As Trinity.cAdtooxBrand)
            Try
                mCol.Add(brand, brand.ID)
            Catch
            End Try
        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator()
        End Function
    End Class

    Public Class cAdTooxFilmCodes
        Implements IEnumerable
        Private mCol As New Collection
        Private _username As String
        Private _password As String

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator
        End Function

        Public Sub add(ByVal FilmCode As cAdTooxFilmcode)
            Try
                mCol.Add(FilmCode, FilmCode.CopyCode)
            Catch
            End Try
        End Sub

        Public Function getPasswordForFilmCode(ByVal copyCode As String) As String
            Return mCol(copyCode).accesspassword
        End Function

        Public Sub New(ByVal user As String, ByVal pass As String)
            _username = user
            _password = pass
        End Sub

        Public Function Count() As Integer
            Return mCol.Count
        End Function

    End Class

    Public Class cAdtooxCustomer
        Private _ID As String
        Private _Name As String

        Public Property ID() As String
            Get
                Return _ID
            End Get
            Set(ByVal value As String)
                _ID = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property
    End Class

    Public Class cAdtooxDivision
        Inherits cAdtooxCustomer
    End Class

    Public Class cAdtooxBrand
        Inherits cAdtooxCustomer
    End Class

    Public Class cAdtooxMedia
        Inherits cAdtooxCustomer
    End Class

    Public Class cAdTooxFilmcode

        Private _copyCode As String
        Private _length As String
        Private _firstAirdate As Date
        Private _region As String
        Private _title As String
        Private _uploadDeadline As Date
        Private _accessPassword As String
        Private _brandID As String
        Private _brandName As String
        Private _advertiserID As String
        Private _advertiserName As String
        Private _statusCollection As New Collection
        Private _Parent As cAdtoox

        Public Sub addStatus(ByVal status As Trinity.cAdTooxStatus)
            _statusCollection.Add(status, status.MediaName)
        End Sub

        Public Function getStatusForChannel(ByVal channelIndex As Long) As Trinity.cAdTooxStatus
            If _statusCollection Is Nothing Then
                getStatus()
            Else
                If _statusCollection.Count = 0 Then
                    Dim _status As New cAdTooxStatus
                    Return _status
                Else
                    For Each status As Trinity.cAdTooxStatus In _statusCollection
                        If status.MediaID = channelIndex OrElse status.MediaID = -1 Then
                            Return status
                        End If
                    Next
                End If
            End If
            Throw New Exception("Status for channel could not be found")
        End Function

        Public Function getStatusForAllChannels() As Trinity.cAdTooxStatus
            If _statusCollection Is Nothing Then
                getStatus()
            Else
                For Each status As Trinity.cAdTooxStatus In _statusCollection
                    If status.MediaName = "'All'" Then
                        Return status
                    End If
                Next
                Return New Trinity.cAdTooxStatus
            End If
            Throw New Exception("Total status could not be found")
        End Function

        Private Sub getStatus()
            If _Parent.UserHasAccess(_copyCode) Then
                Dim HTTP As System.Net.WebClient
                Dim ReceiveXML As New System.Xml.XmlDocument

                Dim ResourceURL As String = "https://ecexpress.adtoox.com/ec/publicapi.jsp"
                Dim Action As String = "?username=" & _Parent.username & "&sid_or_pwd=" & _Parent.password & "&method=GetCopyCode&copycode=" & _copyCode & "&add_copy_envelope=true&skin=groupm.xsl"
                Dim TmpStr As String
                Dim status As Trinity.cAdTooxStatus

                HTTP = New System.Net.WebClient
                HTTP.Credentials = System.Net.CredentialCache.DefaultCredentials
                TmpStr = HTTP.UploadString(ResourceURL & Action, "POST", "")
                ReceiveXML.LoadXml(TmpStr)

                For Each tmpXMLNode As XmlElement In ReceiveXML.GetElementsByTagName("status")
                    status = New Trinity.cAdTooxStatus
                    status.MediaID = tmpXMLNode.GetAttribute("media_id")
                    status.MediaName = tmpXMLNode.GetAttribute("media_name")
                    For temp As Integer = 1 To 8
                        status.Steps(temp - 1) = tmpXMLNode.GetAttribute("step" & temp)
                    Next
                    addStatus(status)
                Next
            End If
        End Sub

        Public Function GetLinkToPlayVideo() As String
            Return "https://ecexpress.adtoox.com/ec/html.jsp?method=playcopycode&copycode=" & _
            CopyCode & "&ccpd=" & getPasswordForFilmCode()
        End Function

        Public Function getLinkToUploadStatus() As String

            Return "https://ecexpress.adtoox.com/ec/index.jsp?action=searchstatus&admin=&section=search&expanded=" & _
                     "block&search_view_selection=show_all&search_copy_code=" & _copyCode & "&username=" & _Parent.username
        End Function

        Public Function getPasswordForFilmCode() As String
            Return AccessPassword
        End Function

        Public Property CopyCode() As String
            Get
                Return _copyCode
            End Get
            Set(ByVal value As String)
                _copyCode = value
            End Set
        End Property
        Public Property Length() As String
            Get
                Return _length
            End Get
            Set(ByVal value As String)
                _length = value
            End Set
        End Property
        Public Property FirstAirdate() As Date
            Get
                Return _firstAirdate
            End Get
            Set(ByVal value As Date)
                _firstAirdate = value
            End Set
        End Property
        Public Property Region() As String
            Get
                Return _region
            End Get
            Set(ByVal value As String)
                _region = value
            End Set
        End Property
        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property
        Public Property UploadDeadline() As Date
            Get
                Return _uploadDeadline
            End Get
            Set(ByVal value As Date)
                _uploadDeadline = value
            End Set
        End Property
        Public Property AccessPassword() As String
            Get
                Return _accessPassword
            End Get
            Set(ByVal value As String)
                _accessPassword = value
            End Set
        End Property
        Public Property BrandID() As String
            Get
                Return _brandID
            End Get
            Set(ByVal value As String)
                _brandID = value
            End Set
        End Property
        Public Property BrandName() As String
            Get
                Return _brandName
            End Get
            Set(ByVal value As String)
                _brandName = value
            End Set
        End Property
        Public Property AdvertiserID() As String
            Get
                Return _advertiserID
            End Get
            Set(ByVal value As String)
                _advertiserID = value
            End Set
        End Property
        Public Property AdvertiserName() As String
            Get
                Return _advertiserName
            End Get
            Set(ByVal value As String)
                _advertiserName = value
            End Set
        End Property

        Public Sub New(ByVal Parent As cAdtoox)
            _Parent = Parent
        End Sub

    End Class


    Public Class cAdTooxStatus

        Private _mediaID As String
        Private _mediaName As String
        Private _steps(7) As String

        Public Property Steps(ByVal index As Integer) As String
            Get
                Return _steps(index)
            End Get
            Set(ByVal value As String)
                _steps(index) = value
            End Set
        End Property

        Public Property MediaID() As String
            Get
                Return _mediaID
            End Get
            Set(ByVal value As String)
                _mediaID = value
            End Set
        End Property
        Public Property MediaName() As String
            Get
                Return _mediaName
            End Get
            Set(ByVal value As String)
                _mediaName = value
            End Set
        End Property

    End Class
End Namespace
