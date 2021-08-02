Imports System.Xml.Linq

Public Class Marathon

    Private _marathonCommand As String
    Public Property MarathonCommand() As String
        Get
            Return _marathonCommand
        End Get
        Set(ByVal value As String)
            _marathonCommand = value
        End Set
    End Property

    Private Function AcceptAll(ByVal sender As Object, ByVal certificate As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function

    Public Function GetClientList(ByVal Company As String, Optional ByVal ClientFilter As String = "") As List(Of Marathon.Client)
        Dim _xml = <?xml version='1.0' encoding='ISO-8859-1'?>
                   <marathon>
                       <type>get_clients</type>
                       <company_id><%= Company %></company_id>
                       <filter_client_name>*<%= ClientFilter %>*</filter_client_name>
                   </marathon>
        Dim _responseXML As Xml.Linq.XDocument
        Dim _list As New List(Of Marathon.Client)
        Try
            _responseXML = SendMarathonRequest(_xml.ToString)
        Catch ex As Exception
            Throw New Exception("Could not get client list." & vbCrLf & vbCrLf & ex.Message)
        End Try

        For Each _client As XElement In _responseXML.<marathon>...<client>
            Dim _mClient As New Marathon.Client
            _mClient.Name = _client.@name
            _mClient.ID = _client.@id
            _list.Add(_mClient)
        Next
        Return _list
    End Function

    Public Function GetProjectList(ByVal Company As String, ByVal Client As String) As List(Of Marathon.Project)
        Dim _xml = <?xml version='1.0' encoding='ISO-8859-1'?>
                   <marathon>
                       <type>get_projects</type>
                       <company_id><%= Company %></company_id>
                       <client_id><%= Client %></client_id>
                   </marathon>

        Dim _responseXML As Xml.Linq.XDocument
        Dim _list As New List(Of Marathon.Project)
        Try
            _responseXML = SendMarathonRequest(_xml.ToString)
        Catch ex As Exception
            Throw New Exception("Could not get project list." & vbCrLf & vbCrLf & ex.Message)
        End Try

        For Each _project As XElement In _responseXML.<marathon>...<project>
            Dim _mProject As New Marathon.Project
            _mProject.Name = _project.@name
            _mProject.ID = _project.@id
            _list.Add(_mProject)
        Next
        Return _list
    End Function

    Public Function GetActivityList(ByVal Company As String) As List(Of Marathon.Activity)
        Dim _xml = <?xml version='1.0' encoding='ISO-8859-1'?>
                   <marathon>
                       <type>get_timecodes</type>
                       <company_id><%= Company %></company_id>
                   </marathon>
        Dim _responseXML As Xml.Linq.XDocument
        Dim _list As New List(Of Marathon.Activity)
        Try
            _responseXML = SendMarathonRequest(_xml.ToString)
        Catch ex As Exception
            Throw New Exception("Could not get project list." & vbCrLf & vbCrLf & ex.Message)
        End Try

        For Each _activity As XElement In _responseXML.<marathon>...<timecode>
            Dim _mActivity As New Marathon.Activity
            _mActivity.Name = _activity.@name
            _mActivity.ID = _activity.@id
            _list.Add(_mActivity)
        Next
        Return _list
    End Function

    Public Sub UploadTimeEntry(ByVal Entry As TimeEntry)
        Dim _xml = <?xml version='1.0' encoding='ISO-8859-1'?>
                   <marathon>
                       <type>put_timereport</type>
                       <company_id><%= Entry.CompanyID %></company_id>
                       <employee_id><%= Entry.UserId %></employee_id>
                       <date><%= Entry.Date %></date>
                       <client_id><%= Entry.ClientID %></client_id>
                       <project_id><%= Entry.ProjectID %></project_id>
                       <timecode_id><%= Entry.TypeID %></timecode_id>
                       <hours><%= Entry.Hours %></hours>
                       <comment><%= Entry.Comment %></comment>
                   </marathon>
        Dim _responseXML As Xml.Linq.XDocument
        Try
            _responseXML = SendMarathonRequest(_xml.ToString)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Function CreatePlan(Plan As Plan) As String
        Dim _xml = <?xml version='1.0' encoding='ISO-8859-1'?>
                   <marathon>
                       <company><%= Plan.CompanyID %></company>
                       <type>create_plan</type>
                       <name><%= Plan.Name %></name>
                       <client><%= Plan.ClientID %></client>
                       <product><%= Plan.ProductID %></product>
                       <agreement><%= Plan.AgreementID %></agreement>
                       <user><%= Plan.UserID %></user>
                   </marathon>
        Dim _responseXML As Xml.Linq.XDocument

        Try
            _responseXML = SendMarathonRequest(_xml.ToString)
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return Nothing
        End Try
        Dim _planNo As String = _responseXML.<marathon>.<plan_number>.Value
        Return _planNo
    End Function

    Function CreateOrder(Order As Order) As String
        Dim _xml = <?xml version='1.0' encoding='ISO-8859-1'?>
                   <marathon>
                       <company><%= Order.CompanyID %></company>
                       <type>create_order</type>
                       <plan_number><%= Order.PlanNumber %></plan_number>
                       <media><%= Order.MediaID %></media>
                   </marathon>
        Dim _responseXML As Xml.Linq.XDocument
        Try
            _responseXML = SendMarathonRequest(_xml.ToString)
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return Nothing
        End Try
        Dim _orderNo As String = _responseXML.<marathon>.<order_number>.Value
        Return _orderNo
    End Function

    Sub CreateInsertion(Insertion As Insertion)
        Dim _insXML As XDocument = <?xml version='1.0' encoding='ISO-8859-1'?>
                                   <marathon>
                                       <company><%= Insertion.CompanyID %></company>
                                       <type>create_insertion</type>
                                       <order_number><%= Insertion.OrderNumber %></order_number>
                                       <insertion_date><%= Insertion.GetFormattedInsertionDate %></insertion_date>
                                       <end_date><%= Insertion.GetFormattedEndDate %></end_date>
                                   </marathon>
        Dim _hasPrice As Boolean = False
        For Each _price As Pricerow In Insertion.PriceRows
            If _price.UnitPrice > 0 AndAlso Math.Round(_price.NetCost) > 0 Then
                Dim _priceXML = <price>
                                    <code><%= _price.Code %></code>
                                    <quantity><%= _price.Quantity %></quantity>
                                    <unitprice><%= _price.UnitPrice.ToString.Replace(".", ",") %></unitprice>
                                    <discount_code><%= _price.DiscountCode %></discount_code>
                                    <net><%= Math.Round(_price.NetCost) %></net>
                                </price>
                _insXML.Element("marathon").Add(_priceXML)
                _hasPrice = True
            End If
        Next
        If Not _hasPrice Then Exit Sub
        Dim _responseXML As Xml.Linq.XDocument
        Try
            _responseXML = SendMarathonRequest(_insXML.ToString)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub MakeOrderDefinitive(CompanyID As String, OrderNumber As String)
        Dim _xml = <?xml version='1.0' encoding='ISO-8859-1'?>
                   <marathon>
                       <company><%= CompanyID %></company>
                       <type>change_order</type>
                       <order_number><%= OrderNumber %></order_number>
                       <status>D</status>
                   </marathon>
        Dim _responseXML As Xml.Linq.XDocument
        Try
            _responseXML = SendMarathonRequest(_xml.ToString)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Function GetCTCForPlan(CompanyID As String, PlanNumber As String) As Single
        Dim _xml = <marathon>
                       <company><%= CompanyID %></company>
                       <type>query_plan</type>
                       <plan_number><%= PlanNumber %></plan_number>
                   </marathon>
        Dim _responseXML As Xml.Linq.XDocument
        Try
            _responseXML = SendMarathonRequest(_xml.ToString)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return _responseXML.<marathon>.<ctc>.Value
    End Function

    Private Function SendMarathonRequest(ByVal request As String) As Xml.Linq.XDocument
        Dim HTTP As System.Net.WebClient
        Dim _receiveXML As New System.Xml.Linq.XDocument

        System.Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls
        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf AcceptAll)

        HTTP = New System.Net.WebClient
        HTTP.Credentials = System.Net.CredentialCache.DefaultCredentials

        Dim _response As String = HTTP.UploadString(MarathonCommand, "POST", request.Replace("&amp;", "&"))
        _receiveXML = Xml.Linq.XDocument.Parse(_response)

        If _receiveXML.<marathon>.<status>.Value <> "OK" Then
            Throw New Exception(_receiveXML.<marathon>.<message>.Value)
        End If
        Return _receiveXML
    End Function

    Public Sub New(MarathonCommand As String)
        _marathonCommand = MarathonCommand
    End Sub
End Class
