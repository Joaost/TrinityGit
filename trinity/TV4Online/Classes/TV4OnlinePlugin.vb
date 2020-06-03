Imports System.ComponentModel.Composition
Imports System.ComponentModel.Composition.Hosting
Imports System.Reflection
Imports TrinityPlugin
Imports System.ServiceModel
Imports System.Xml.Serialization
Imports System.Windows.Forms
Imports clTrinity

<Export(GetType(IPlugin))>
Public Class TV4OnlinePlugin
    Implements IPlugin

    Dim listOfWeek As New List(Of Object)
    Dim _camp As clTrinity.Trinity.cKampanj
    Dim _binding As Object

    Dim _endpoint As Object
    Dim _client As Object
    Dim _availableChannels As String()

    Dim nettoSpotlist As New List(Of clTrinity.Trinity.cBookedSpot)

    Private _mnu As New IPlugin.PluginMenu
    Private _prefs As Preferences

    Dim DateOutOfPeriod As New Hashtable

    Private _surcharges As List(Of String)
    Private _tv4Indices As New List(Of String)

    Private _TrinitySurchages As New List(Of String)

    Private Shared _internalApplication As ITrinityApplication
    Friend Shared Function InternalApplication() As ITrinityApplication
        Return _internalApplication
    End Function

    Public Sub New()

        _mnu.AddToMenu = ""
        _mnu.Caption = "TV4 Spotlight"

        Dim _itm As New IPlugin.PluginMenuItem
        _itm.Text = "Upload bookings to TV4 Spotlight"

        Dim _itm2 As New IPlugin.PluginMenuItem
        _itm2.Text = "Confirmations"

        _itm.OnClickFunction = AddressOf openTV4Main
        _mnu.Items.Add(_itm)

        _itm2.OnClickFunction = AddressOf confirmationStatus
        _mnu.Items.Add(_itm2)


        Dim catalog As New AggregateCatalog()
        catalog.Catalogs.Add(New AssemblyCatalog(Assembly.GetEntryAssembly))
        Dim container As New CompositionContainer(catalog)
        container.SatisfyImportsOnce(Me)

        connectToSpotlight()
        Dim _itm3 As New IPlugin.PluginMenuItem
        _itm3.Text = "Import TV4 schedule"
        _itm3.OnClickFunction = AddressOf openFrmScheduleHandler
        _mnu.Items.Add(_itm3)

    End Sub
    Public ReadOnly Property Preferences As Preferences
        Get
            If _prefs Is Nothing AndAlso IO.File.Exists(IO.Path.Combine(Application.GetUserDataPath, "TV4Online.xml")) Then
                Dim _ser As New XmlSerializer(GetType(Preferences))
                Using _stream As New IO.FileStream(IO.Path.Combine(Application.GetUserDataPath, "TV4Online.xml"), IO.FileMode.Open)
                    _prefs = _ser.Deserialize(_stream)
                End Using
            End If
            If _prefs Is Nothing Then
                _prefs = New Preferences
            End If
            Return _prefs
        End Get
    End Property
    Sub getTrinitySurcharges()

        For Each c In _camp.Channels
            If _availableChannels.Contains(c.ChannelName) Then

                For Each b In c.BookingTypes
                    For Each _Sc In b.AddedValues
                        If Not _TrinitySurchages.Select(Function(s) s.ToLower()).Contains(_Sc.Name.ToLower()) Then
                            _TrinitySurchages.Add(_Sc.Name)
                        End If
                    Next
                Next
            End If
        Next

    End Sub


    Sub campaignMoreThanHundredSpots()
        _internalApplication = Application
        _camp = Application.ActiveCampaign
        nettoSpotlist.Clear()


        If Preferences.Username = "joakim.ostling@groupm.com" Then

            If _camp.BookedSpots.Count > 100 Then
                Dim result = Windows.Forms.MessageBox.Show("You are going to upload a huge amount of spots, which can take a lot of time. Do you want to proceed? ", "Confirmation Dialog Box", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question)
                If result = DialogResult.No Then
                    MessageBox.Show("No Button Pressed", "MessageBox Title", MessageBoxButtons.OK, MessageBoxIcon.Error)

                ElseIf result = DialogResult.Yes Then
                    For Each tmpChannel As clTrinity.Trinity.cChannel In _camp.Channels
                        If _availableChannels.Contains(tmpChannel.ChannelName) Then
                            For Each tmpBT As clTrinity.Trinity.cBookingType In tmpChannel.BookingTypes
                                If tmpBT.BookIt Then
                                    Dim BTName As String = tmpChannel.ChannelName + " " + tmpBT.Name
                                    Dim result2 = Windows.Forms.MessageBox.Show("Do you want to upload this booking: " + BTName + " ?", "Bookingtype, number of spots, startdate booking, end date booking", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                                    If result2 = DialogResult.Yes Then
                                        For i = 1 To _camp.BookedSpots.Count
                                            If _camp.BookedSpots(i).Channel.ChannelName = tmpChannel.ChannelName Then
                                                nettoSpotlist.Add(_camp.BookedSpots(i))
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        End If
                    Next
                End If

            End If
        Else
            For i = 1 To _camp.BookedSpots.Count
                nettoSpotlist.Add(_camp.BookedSpots(i))
            Next

        End If





        'Skapa ny lista med FilteredBookedSpots
        ' For Each tmpspot As Object In Application.ActiveCampaign.BookedSpots
        'If tmpspot.BookingType.Name <> "TV4" Then
        ''_camp.BookedSpots.add(tmpspot)
        'End If
        'Next
    End Sub
    Sub connectToSpotlight()
        _internalApplication = Application

        If Application.GetSharedNetworkPreference("TV4Spotlight_V5", "Endpoint") = "" Then
            Windows.Forms.MessageBox.Show("No server address has been specified for TV4Spotlight in database.ini, please contact the server administrator.", "TV4 Spotlight", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        End If


        _camp = Application.ActiveCampaign


        _binding = New WSHttpBinding(SecurityMode.Transport, False)
        _binding.CloseTimeout = New TimeSpan(0, 1, 0)
        _binding.OpenTimeout = New TimeSpan(0, 1, 0)
        _binding.SendTimeout = New TimeSpan(0, 1, 0)
        _binding.ReceiveTimeout = New TimeSpan(0, 10, 0)
        _binding.BypassProxyOnLocal = False
        _binding.TransactionFlow = False
        _binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard
        _binding.MaxBufferPoolSize = 2147483647
        _binding.MaxReceivedMessageSize = 2147483647
        _binding.MessageEncoding = WSMessageEncoding.Text
        _binding.TextEncoding = Text.UTF8Encoding.UTF8
        _binding.UseDefaultWebProxy = True
        _binding.AllowCookies = False

        With _binding.ReaderQuotas
            .MaxDepth = 32
            .MaxStringContentLength = 8192
            .MaxArrayLength = 16384
            .MaxBytesPerRead = 4096
            .MaxNameTableCharCount = 16384
        End With
        With _binding.ReliableSession
            .Ordered = True
            .Enabled = False
            .InactivityTimeout = New TimeSpan(0, 10, 0)
        End With
        With _binding.Security
            .Transport.ClientCredentialType = HttpClientCredentialType.None
            .Transport.ProxyCredentialType = HttpProxyCredentialType.None
            .Transport.Realm = ""
            .Message.ClientCredentialType = MessageCredentialType.Windows
            .Message.NegotiateServiceCredential = True
        End With

        _endpoint = New EndpointAddressBuilder(New EndpointAddress(New Uri(Application.GetSharedNetworkPreference("TV4Spotlight_V5", "Endpoint"))))

        _client = New TV4Online.SpotlightApiV23.xsd.SpotlightApiV5Client(DirectCast(IIf(_endpoint.Uri.ToString.StartsWith("https"), _binding, New BasicHttpBinding), System.ServiceModel.Channels.Binding), _endpoint.ToEndpointAddress)

        'Test
        'Dim tmpListCh As String()
        _availableChannels = _client.GetChannels()


        'Dim tmpindex As Integer = 0

        'For Each tmpch As String In _availableChannels
        'If tmpch = "TV4" Then
        '_availableChannels.SetValue("", tmpindex)
        'End If
        'tmpindex = tmpindex + 1
        'Next



        Dim _client2 As New TV4Online.SpotlightApiV23.xsd.SpotlightApiV5Client(New WSHttpBinding(SecurityMode.Transport), New EndpointAddress(TV4OnlinePlugin.InternalApplication.GetSharedNetworkPreference("TV4Spotlight_V5", "Endpoint")))
        _surcharges = _client2.GetSurchargeNames.ToList()

    End Sub
    Function FaultySpots()
        For Each tmpSpot In _camp.BookedSpots
            If tmpSpot.Channel.ChannelName = "TV4" Or tmpSpot.Channel.ChannelName = "Sjuan" Then

                If tmpSpot.Film Is Nothing Then
                    Windows.Forms.MessageBox.Show(String.Format("There are one or more spots which not are connected to a film. Please correct this in the booking window."), "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                    Return False
                End If
            End If
        Next
        Return True
    End Function
    Function preferenceSuccess()
        Dim _client As New TV4Online.SpotlightApiV23.xsd.SpotlightApiV5Client(DirectCast(IIf(_endpoint.Uri.ToString.StartsWith("https"), _binding, New BasicHttpBinding), System.ServiceModel.Channels.Binding), _endpoint.ToEndpointAddress)
        Dim res = _client.GetUserOrganizations(Preferences.Username, Preferences.GetPlainTextPassword)
        If res.Status = TV4Online.SpotlightApiV23.xsd.StatusType.Error Then
            Dim message = res.ErrorMessages.Values(0).ToString()
            Windows.Forms.MessageBox.Show(String.Format(message), "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            Return False
        End If
        _client.Close()
        Return True
    End Function
    Sub openTV4Main()
        connectToSpotlight()
        campaignMoreThanHundredSpots() '20180406 - Test' 
        If Not preferenceSuccess() Then
            Exit Sub
        End If
        If Not FaultySpots() Then
            Exit Sub
        End If
        Dim frm As New frmTv4Main(_camp, _availableChannels, nettoSpotlist)
        frm.txtContact.Text = Preferences.TV4Contact
        Dim _ret = frm.ShowDialog
    End Sub


    Public Function GetOrg()
        Dim tmpClient As New TV4Online.SpotlightApiV23.xsd.SpotlightApiV5Client(DirectCast(IIf(_endpoint.Uri.ToString.StartsWith("https"), _binding, New BasicHttpBinding), System.ServiceModel.Channels.Binding), _endpoint.ToEndpointAddress)
        Dim test = tmpClient.GetUserOrganizations(Preferences.Username, Preferences.GetPlainTextPassword)
        If test.Status = TV4Online.SpotlightApiV23.xsd.StatusType.Success Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub confirmationStatus()
        Try
            connectToSpotlight()
            GetBookedSpots()
            If Not preferenceSuccess() Then
                Exit Sub
            End If
            If Not FaultySpots() Then
                Exit Sub
            End If
            Dim frm As New frmGetConfirmations(_camp, _availableChannels)
            frm.ShowDialog()
            If frm.DialogResult = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function UploadBooking(tmpBook As TV4Online.SpotlightApiV23.xsd.Booking, ByRef _ro As Integer, Optional ByVal otherOrganizations As Boolean = False, Optional ByVal orgID As String = "")

        Dim _client As New TV4Online.SpotlightApiV23.xsd.SpotlightApiV5Client(DirectCast(IIf(_endpoint.Uri.ToString.StartsWith("https"), _binding, New BasicHttpBinding), System.ServiceModel.Channels.Binding), _endpoint.ToEndpointAddress)

        Dim _campaign As New TV4Online.SpotlightApiV23.xsd.Campaign
        Dim _bookings As New List(Of TV4Online.SpotlightApiV23.xsd.Booking)

        _bookings.Add(tmpBook)

        _campaign.AgencyContactName = _camp.Buyer
        _campaign.Client = _camp.Client
        _campaign.Product = _camp.Product
        _campaign.System = "Trinity 4.0"
        _campaign.Country = "SE"
        _campaign.Name = _camp.Name
        _campaign.Bookings = _bookings.ToArray

        Try
            If otherOrganizations Then
                Dim _res = _client.CreateBookingForOrganization(Preferences.Username, Preferences.GetPlainTextPassword, orgID, _campaign, False)
                If _res.Status = TV4Online.SpotlightApiV23.xsd.StatusType.Success Then
                    Windows.Forms.MessageBox.Show("Booking was successfully uploaded.", "TV4 Spotlight", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                    receieveBookingInfoFromSpotlight(_res, tmpBook)
                ElseIf _res.Status = TV4Online.SpotlightApiV23.xsd.StatusType.Warning Then
                    Dim _r As Windows.Forms.DialogResult = Windows.Forms.MessageBox.Show(String.Format("TV4 Spotlight responded with a warning :" & vbNewLine & vbNewLine & "{0}" & vbNewLine & vbNewLine & "Do you want to book " + tmpBook.Channel + " " + tmpBook.Type + " anyway?", String.Join(vbNewLine, _res.ErrorMessages.Select(Function(_e) _e.Key & ": " & _e.Value).ToArray)), "T R I N I T Y ", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question)
                    If _r = Windows.Forms.DialogResult.Yes Then
                        _res = _client.CreateBookingForOrganization(Preferences.Username, Preferences.GetPlainTextPassword, orgID, _campaign, True)
                        If _res.Status = TV4Online.SpotlightApiV23.xsd.StatusType.Success Then
                            Windows.Forms.MessageBox.Show("Booking " + tmpBook.Channel + " " + tmpBook.Type + " was successfully uploaded.", "TV4 Spotlight", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                            receieveBookingInfoFromSpotlight(_res, tmpBook)
                            Return True
                        Else
                            Windows.Forms.MessageBox.Show(String.Format("TV4 Spotlight responded with one or more errors:" & vbNewLine & "{0}", String.Join(vbNewLine, _res.ErrorMessages.Select(Function(_e) _e.Key & ": " & _e.Value).ToArray)), "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                        End If
                    ElseIf _r = Windows.Forms.DialogResult.Cancel Or _r = Windows.Forms.DialogResult.No Then
                        Return False
                    End If
                Else
                    _ro = _res.Status
                    Windows.Forms.MessageBox.Show(String.Format("TV4 Spotlight responded with one or more errors:" & vbNewLine & "{0}", String.Join(vbNewLine, _res.ErrorMessages.Select(Function(_e) _e.Key & ": " & _e.Value).ToArray)), "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                End If
            Else
                Dim _res = _client.CreateBooking(Preferences.Username, Preferences.GetPlainTextPassword, _campaign, False)
                If _res.Status = TV4Online.SpotlightApiV23.xsd.StatusType.Success Then
                    Windows.Forms.MessageBox.Show("Booking was successfully uploaded.", "TV4 Spotlight", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                    receieveBookingInfoFromSpotlight(_res, tmpBook)
                ElseIf _res.Status = TV4Online.SpotlightApiV23.xsd.StatusType.Warning Then
                    Dim _r As Windows.Forms.DialogResult = Windows.Forms.MessageBox.Show(String.Format("TV4 Spotlight responded with a warning :" & vbNewLine & vbNewLine & "{0}" & vbNewLine & vbNewLine & "Do you want to book " + tmpBook.Channel + " " + tmpBook.Type + " anyway?", String.Join(vbNewLine, _res.ErrorMessages.Select(Function(_e) _e.Key & ": " & _e.Value).ToArray)), "T R I N I T Y ", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question)
                    If _r = Windows.Forms.DialogResult.Yes Then
                        _res = _client.CreateBooking(Preferences.Username, Preferences.GetPlainTextPassword, _campaign, True)
                        If _res.Status = TV4Online.SpotlightApiV23.xsd.StatusType.Success Then
                            Windows.Forms.MessageBox.Show("Booking " + tmpBook.Channel + " " + tmpBook.Type + " was successfully uploaded.", "TV4 Spotlight", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                            receieveBookingInfoFromSpotlight(_res, tmpBook)
                            Return True
                        Else
                            Windows.Forms.MessageBox.Show(String.Format("TV4 Spotlight responded with an error :" & vbNewLine & vbNewLine & "{0}" & vbNewLine & vbNewLine & "For " + tmpBook.Channel + " " + tmpBook.Type + ".", String.Join(vbNewLine, _res.ErrorMessages.Select(Function(_e) _e.Key & ": " & _e.Value).ToArray)), "T R I N I T Y ", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                        End If
                    ElseIf _r = Windows.Forms.DialogResult.Cancel Or _r = Windows.Forms.DialogResult.No Then
                        Return False
                    End If
                Else
                    _ro = _res.Status
                    Windows.Forms.MessageBox.Show(String.Format("TV4 Spotlight responded with one or more errors:" & vbNewLine & "{0}", String.Join(vbNewLine, _res.ErrorMessages.Select(Function(_e) _e.Key & ": " & _e.Value).ToArray)), "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                End If
            End If
        Catch ex As FaultException(Of TV4Online.SpotlightApiV23.xsd.CreateBookingResponse)
            Windows.Forms.MessageBox.Show(String.Format("TV4 Spotlight responded with one or more errors:" & vbNewLine & "{0}", String.Join(vbNewLine, ex.Detail.ErrorMessages.Select(Function(_e) _e.Key & ": " & _e.Value).ToArray)), "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        End Try
        _client.Close()
        Return False
    End Function
    Function checkConfirmationForBookingByGUID(ByRef b As TV4Online.SpotlightApiV23.xsd.Booking, ByVal startUp As Boolean)

        Dim guidParseRes = New Guid(b.BookingIdSpotlight)

        Dim _client As New TV4Online.SpotlightApiV23.xsd.SpotlightApiV5Client(DirectCast(IIf(_endpoint.Uri.ToString.StartsWith("https"), _binding, New BasicHttpBinding), System.ServiceModel.Channels.Binding), _endpoint.ToEndpointAddress)

        If b IsNot Nothing Then
            Dim res As TV4Online.SpotlightApiV23.xsd.GetConfirmationsResponse = _client.GetConfirmationForBooking(Preferences.Username, Preferences.GetPlainTextPassword, guidParseRes, False)
            If res.Status = TV4Online.SpotlightApiV23.xsd.StatusType.Success Then

                If res.Confirmations.Count > 0 Then
                    getAgencyRefNo(res)
                    Return New Tuple(Of TV4Online.SpotlightApiV23.xsd.GetConfirmationsResponse, Boolean)(res, True)
                Else
                    If startUp = False Then
                        Windows.Forms.MessageBox.Show(String.Format("TV4 Spotlight responded with one or more errors:" & vbNewLine & "No confirmations were found for " & b.Channel & " " & b.TrinityType & ".", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error))
                    End If
                End If
            Else
                If startUp = False Then
                    Windows.Forms.MessageBox.Show(String.Format("TV4 Spotlight responded with one or more errors:" & vbNewLine & "No confirmations were found for " & b.Channel & " " & b.TrinityType & ".", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error))
                End If
            End If
        End If
    End Function

    Sub openFrmScheduleHandler()

        Dim frmScheduleHandler As New frmGetSchedule()

        frmScheduleHandler.ShowDialog()

    End Sub

    Function checkAvailableSchedule(ByVal fromDate As Date, ByVal toDate As Date, ByVal channel As String) As TV4Online.SpotlightApiV23.xsd.PricesAndScheduleResponse

        Dim _client As New TV4Online.SpotlightApiV23.xsd.SpotlightApiV5Client(DirectCast(IIf(_endpoint.Uri.ToString.StartsWith("https"), _binding, New BasicHttpBinding), System.ServiceModel.Channels.Binding), _endpoint.ToEndpointAddress)
        Try
            Dim ret = _client.GetSpotPrices(Preferences.Username, Preferences.GetPlainTextPassword, fromDate, toDate, channel)

            If ret.ErrorMessages.Count < 1 Then
                Return ret
            Else
            End If
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        End Try

    End Function
    Function getChannels()

        Dim _client As New TV4Online.SpotlightApiV23.xsd.SpotlightApiV5Client(DirectCast(IIf(_endpoint.Uri.ToString.StartsWith("https"), _binding, New BasicHttpBinding), System.ServiceModel.Channels.Binding), _endpoint.ToEndpointAddress)

        Try
            Dim ret = _client.GetChannels()
            If ret IsNot Nothing Then
                Return ret
            End If
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        End Try

    End Function

    Sub GetBookedSpots()
        For Each tmpS As Object In Application.ActiveCampaign.Bookedspots

        Next
    End Sub
    Function checkConfirmationForBookingByRef(ByRef b As TV4Online.SpotlightApiV23.xsd.Booking, ByVal startUp As Boolean, Optional ByRef onlyConfirmations As Boolean = False)

        Dim _client As New TV4Online.SpotlightApiV23.xsd.SpotlightApiV5Client(DirectCast(IIf(_endpoint.Uri.ToString.StartsWith("https"), _binding, New BasicHttpBinding), System.ServiceModel.Channels.Binding), _endpoint.ToEndpointAddress)

        If b IsNot Nothing Then
            Dim res As TV4Online.SpotlightApiV23.xsd.GetConfirmationsResponse = _client.GetConfirmationsForAgencyRefNo(Preferences.Username, Preferences.GetPlainTextPassword, b.AgencyBookingRefNo, False)
            If res.Status = TV4Online.SpotlightApiV23.xsd.StatusType.Success Then

                If res.Confirmations.Count > 0 Then
                    getAgencyRefNo(res)
                    If onlyConfirmations Then
                        Return res
                    Else
                        Return New Tuple(Of TV4Online.SpotlightApiV23.xsd.GetConfirmationsResponse, Boolean)(res, True)
                    End If
                Else
                    If startUp = False Then
                        Windows.Forms.MessageBox.Show(String.Format("TV4 Spotlight responded with one or more errors:" & vbNewLine & "No confirmations were found for " & b.Channel & " " & b.TrinityType & ".", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error))
                    End If
                End If
            Else
                If startUp = False Then
                    Windows.Forms.MessageBox.Show(String.Format("TV4 Spotlight responded with one or more errors:" & vbNewLine & "No confirmations were found for " & b.Channel & " " & b.TrinityType & ".", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error))
                End If
            End If
        End If
    End Function

    Function CreateGUID() As String
        CreateGUID = Guid.NewGuid.ToString
    End Function

    Public Sub ConfirmImport(ByRef tv4BookingChannel As TV4Online.SpotlightApiV23.xsd.Booking)
        Dim totNet As Integer = 0
        Dim res As New TV4Online.SpotlightApiV23.xsd.GetConfirmationsResponse
        If tv4BookingChannel.AgencyBookingRefNo <> "" Then
            res = checkConfirmationForBookingByRef(tv4BookingChannel, False, True)
        Else
            res = checkConfirmationForBookingByGUID(tv4BookingChannel, False)
        End If
        DateOutOfPeriod.Clear()
        For Each tmpConf As TV4Online.SpotlightApiV23.xsd.Confirmation In res.Confirmations
            Dim count As Integer = 0

            If tmpConf.Versions.Max().RbsSpots.Length > 1 And tmpConf.Versions.Max().SpecificSpots.Length < 1 Then
                tmpConf.Versions.Max().btType = "RBS"
            Else
                tmpConf.Versions.Max().btType = "Specific"
            End If
            If tmpConf.Channel = tv4BookingChannel.Channel Then
                'This statemeant should filter out the ConfirmationsVersions which are not as the same type as the Trinity bookingtype. 
                'If tmpConf.Versions.Max().btType.Contains(tv4BookingChannel.Type)
                Try
                    Dim tmpChan As Object = Application.ActiveCampaign.Channels(tv4BookingChannel.Channel)

                    If tmpChan IsNot Nothing Then
                        Dim deletedList As New List(Of Object)
                        Dim frmImport As New frmImport(tv4BookingChannel, tmpConf, tmpChan, _camp)
                        Dim _btTV4Name = tmpConf.Versions().Max().Name
                        Dim tmpBt As Object
                        For Each bt As Object In tmpChan.BookingTypes
                            If bt.BookIt Then
                                Dim _btTrinName = tmpChan.ChannelName & " " & bt.Name
                                If _btTrinName = _btTV4Name Then
                                    tmpBt = tmpChan.BookingTypes(bt)
                                End If
                            End If
                        Next
                        frmImport.lblChannelName.Text = _btTV4Name.ToString()
                        Dim tmpNetBudget As Integer = tmpConf.Versions.Max().SpecificSpots.Sum(Function(e) e.NetPrice)
                        If tmpNetBudget = 0 Then
                            tmpNetBudget = tmpConf.BookedBudget
                        End If
                        frmImport.lblConfirmationBudget.Text = Format(tmpNetBudget, "#,###") & " " & "kr"
                        frmImport.Text = _btTV4Name.ToString()

                        frmImport.DateTimePickerFrom.Value = Format(getStartDate(tmpConf.Versions.Max()), "yyyy-MM-dd")
                        frmImport.DateTimePickerEnd.Value = Format(getEndDate(tmpConf.Versions.Max()), "yyyy-MM-dd")

                        If tmpBt IsNot Nothing Then
                            frmImport.cmbBookingType.SelectedItem = tmpBt.Name
                        Else
                            If frmImport.cmbBookingType.Items.Count > 1 Then
                                frmImport.cmbBookingType.SelectedIndex = 0
                            End If
                        End If
                        frmImport.ShowDialog()
                        If frmImport.DialogResult = DialogResult.Cancel Then
                            Exit For
                        ElseIf frmImport.DialogResult = DialogResult.Ignore Then
                            Exit Try
                        End If
                        If _btTV4Name <> "TV4 " & frmImport.cmbBookingType.SelectedItem.ToString() Then

                            If DialogResult.No = Windows.Forms.MessageBox.Show("Are you sure you want to add " & _btTV4Name & " spots on following bookingtypes " & tmpChan.channelName & " " & frmImport.cmbBookingType.SelectedItem.ToString(), "T R I N I T Y", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) Then
                                Exit Try
                            End If
                        End If
                        tmpBt = tmpChan.BookingTypes(frmImport.cmbBookingType.Text)

                        If frmImport.chkReplace.Checked Then
                            Dim spots As New List(Of Object)
                            For Each tmpS In _camp.PlannedSpots
                                If tmpS.BookingType Is tmpBt AndAlso tmpS.AirDate >= frmImport.DateTimePickerFrom.Value.ToOADate AndAlso tmpS.AirDate >= frmImport.DateTimePickerFrom.Value.ToOADate AndAlso tmpS.AirDate <= frmImport.DateTimePickerEnd.Value.ToOADate Then
                                    spots.Add(tmpS)
                                End If
                            Next
                            For Each rS In spots
                                'Adding spots to a another list so we can bring back the TRP estimation
                                deletedList.Add(rS)
                                _camp.PlannedSpots.Remove(rS)
                            Next
                        End If


                        'Import the confirmation starts here

                        'If any of the spots have any surchages                            

                        Dim _values As New List(Of String)
                        If tmpConf.Type.Contains("Spec") Then

                            getTrinitySurcharges()
                            Dim noMatch As Boolean = False
                            For Each _sc In tmpConf.Versions.Max().SpecificSpots
                                If Not _TrinitySurchages.Select(Function(_s) _s.ToLower).Contains(_sc.Placement.Replace(" ", "").ToLower) AndAlso _sc.Placement <> "" AndAlso Not _values.Contains(_sc.Placement) Then

                                    Dim _avName = _sc.Placement
                                    Dim _tmpSC = _TrinitySurchages.FirstOrDefault(Function(s) s.ToLower = _avName.ToLower.Replace(" ", ""))
                                    If String.IsNullOrEmpty(_tmpSC) Then
                                        If TV4OnlinePlugin.InternalApplication.GetUserPreference("SpotlightTrinity", _sc.Placement) <> "" Then
                                            _sc.Placement = TV4OnlinePlugin.InternalApplication.GetUserPreference("SpotlightTrinity", _sc.Placement)
                                        Else
                                            noMatch = True
                                        End If
                                    Else
                                        noMatch = True
                                    End If
                                    If noMatch Then
                                        _values.Add(_sc.Placement)
                                    End If
                                End If
                            Next
                            If _values.Count > 0 Then
                                Dim _frm = New frmTranslateFromSpotlight(_values, _TrinitySurchages)
                                _frm.ShowDialog()
                                For Each _spec In tmpConf.Versions.Max().SpecificSpots
                                    If Not _surcharges.Select(Function(_s) _s.ToLower).Contains(_spec.Placement.Replace(" ", "").ToLower) Then
                                        If TV4OnlinePlugin.InternalApplication.GetUserPreference("SpotlightTrinity", _spec.Placement) <> "" Then
                                            _spec.Placement = TV4OnlinePlugin.InternalApplication.GetUserPreference("SpotlightTrinity", _spec.Placement)
                                        End If
                                    End If
                                Next
                            End If
                        End If

                        If frmImport.optReplaceBudget.Checked Then

                            If tmpNetBudget > 0 Then
                                tmpBt.ConfirmedNetBudget = tmpNetBudget
                            Else
                                tmpBt.ConfirmedNetBudget = tmpConf.Versions.Max().NetPrice
                                'tmpBt.ConfirmedGrossbudget = tmpConf.Versions.Max().GrossPrice
                            End If
                        Else
                            tmpBt.ConfirmedNetBudget = tmpConf.Versions.Max().NetPrice + tmpBt.ConfirmedNetBudget
                            'tmpBt.ConfirmedGrossbudget += tmpConf.Versions.Max().GrossPrice
                        End If

                        Dim _skippedSpot As Boolean = False

                        If tmpBt.IsRBS Then

                            For Each tv4Spot As TV4Online.SpotlightApiV23.xsd.ConfirmationRbsSpot In tmpConf.Versions.Max().RbsSpots
                                If tv4Spot.BroadcastDate >= Date.FromOADate(_camp.StartDate) AndAlso tv4Spot.BroadcastDate <= Date.FromOADate(_camp.EndDate) Then

                                    Dim newSpot As New TrinityExtended.cPlannedSpot

                                    newSpot.Channel = tmpChan
                                    newSpot.AirDate = tv4Spot.BroadcastDate.ToOADate
                                    newSpot.Bookingtype = tmpBt
                                    Dim week As Object = GetWeek(tv4Spot.BroadcastDate, tmpChan, tmpBt)
                                    newSpot.Week = week
                                    newSpot.Filmcode = tv4Spot.FilmCode
                                    newSpot.SpotLength = tv4Spot.FilmLength
                                    newSpot.MaM = tv4Spot.BroadcastTime
                                    newSpot.Programme = tv4Spot.ProgramName

                                    If newSpot.Week Is Nothing Then
                                        _skippedSpot = True
                                        _camp.PlannedSpots.Remove(newSpot.ID)
                                    Else
                                        If newSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(newSpot.AirDate) Then
                                            DateOutOfPeriod.Add(DateOutOfPeriod.Count, newSpot.AirDate)
                                            _camp.PlannedSpots.Remove(newSpot.ID)
                                        End If
                                    End If

                                    Application.ActiveCampaign.PlannedSpots.AddTV4Spot(newSpot, newSpot.Channel, newSpot.Bookingtype, tv4Spot.FilmCode, CreateGUID())
                                Else
                                    If Not DateOutOfPeriod.Contains(Format(tv4Spot.BroadcastDate, "yyyy-MM-dd")) Then
                                        DateOutOfPeriod.Add(DateOutOfPeriod.Count, tv4Spot.BroadcastDate)
                                    End If
                                End If
                                count = count + 1
                            Next
                        ElseIf tmpBt.IsSpecific Then

                            For Each tv4Spot As TV4Online.SpotlightApiV23.xsd.ConfirmationSpecificSpot In tmpConf.Versions.Max().SpecificSpots
                                If tv4Spot.BroadcastDate >= Date.FromOADate(_camp.StartDate) AndAlso tv4Spot.BroadcastDate <= Date.FromOADate(_camp.EndDate) Then

                                    Dim _placDict As New Dictionary(Of String, Object)
                                    Dim newSpot As New TrinityExtended.cPlannedSpot

                                    newSpot.Channel = tmpChan
                                    newSpot.AirDate = tv4Spot.BroadcastDate.ToOADate
                                    newSpot.Bookingtype = tmpBt
                                    Dim week As Object = GetWeek(tv4Spot.BroadcastDate, tmpChan, tmpBt)
                                    newSpot.Week = week
                                    newSpot.Filmcode = tv4Spot.FilmCode
                                    newSpot.SpotLength = tv4Spot.FilmLength
                                    newSpot.MaM = tv4Spot.BroadcastTime
                                    newSpot.PriceNet = tv4Spot.NetPrice

                                    totNet += tv4Spot.NetPrice

                                    newSpot.PriceGross = tv4Spot.GrossPrice
                                    newSpot.Programme = tv4Spot.ProgramName
                                    newSpot.Estimation = tv4Spot.EstimatedTRP

                                    'For each deletedSpot As Object In deletedList
                                    '    If deletedSpot.Programme = newSpot.Programme
                                    '        newSpot.MyRating = deletedSpot.MyRating
                                    '    End If
                                    'Next

                                    Dim result As Integer = 9999999
                                    Dim validSpot As Object = Nothing
                                    For Each tmpS In Application.ActiveCampaign.BookedSpots
                                        If tmpS.BookingType Is tmpBt AndAlso tmpS.AirDate.ToOADate = newSpot.AirDate Then
                                            Dim interVall As Integer = 30
                                            Dim val As Integer = 0
                                            If (tmpS.Mam >= (newSpot.MaM - interVall) And (tmpS.Mam <= (newSpot.MaM + interVall))) Then
                                                val = tmpS.Mam - newSpot.MaM
                                                val = Math.Abs(val)
                                                If val < result Then
                                                    Dim lv = LevenshteinDistance(newSpot.Programme.ToUpper, tmpS.Programme.ToUpper)
                                                    lv = lv / tmpS.Programme.ToString().Length
                                                    If lv <= 0.3 Then
                                                        result = val
                                                        validSpot = tmpS
                                                    End If
                                                End If
                                            End If
                                        End If
                                    Next
                                    If result < 10 Then
                                        newSpot.MyRating = validSpot.MyEstimate
                                    Else
                                        If tmpBt.IndexMainTarget <> 0 Then
                                            newSpot.MyRating = newSpot.Estimation * (tmpBt.IndexMainTarget / 100)
                                        End If
                                    End If
                                    Dim tmpSurcharges As String = ""
                                    Dim tmpSurchargesObj As Object = Nothing

                                    If Not _TrinitySurchages.Select(Function(_s) _s.ToLower).Contains(tv4Spot.Placement) AndAlso tv4Spot.Placement <> "" Then

                                        tmpSurcharges = tv4Spot.Placement
                                        For Each matchSur As Object In newSpot.Bookingtype.AddedValues
                                            If matchSur.Name = tmpSurcharges Then
                                                tmpSurchargesObj = matchSur
                                            End If
                                        Next
                                    End If
                                    If tmpSurcharges <> "" Then
                                        newSpot.AddedValue = tmpSurchargesObj
                                    End If
                                    If newSpot.Week Is Nothing Then
                                        _skippedSpot = True
                                        _camp.PlannedSpots.Remove(newSpot.ID)
                                    Else

                                        If newSpot.Bookingtype.AddedValues.FindByName(tv4Spot.Placement) IsNot Nothing AndAlso Not _placDict.ContainsKey(tv4Spot.Placement) Then
                                            _placDict.Add(tv4Spot.Placement, newSpot.Bookingtype.AddedValues.FindByName(tv4Spot.Placement))
                                        End If

                                        If newSpot.Week Is Nothing And Not DateOutOfPeriod.ContainsValue(newSpot.AirDate) Then
                                            DateOutOfPeriod.Add(DateOutOfPeriod.Count, newSpot.AirDate)
                                            _camp.PlannedSpots.Remove(newSpot.ID)
                                        End If
                                    End If

                                    Application.ActiveCampaign.PlannedSpots.AddTV4Spot(newSpot, newSpot.Channel, newSpot.Bookingtype, tv4Spot.FilmCode, CreateGUID())
                                Else
                                    If Not DateOutOfPeriod.Contains(Format(tv4Spot.BroadcastDate, "yyyy-MM-dd")) Then
                                        DateOutOfPeriod.Add(DateOutOfPeriod.Count, tv4Spot.BroadcastDate)
                                    End If
                                End If
                                count = count + 1
                            Next
                        End If
                        Windows.Forms.MessageBox.Show("Succes! " & count & " spots were loaded for " & tmpChan.Channelname & " " & tmpBt.Name & ".", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        Dim _message As String = String.Format("The channel '{0}' was found in the schedule, but not the campaign." & vbCrLf & "Please choose a channel to import spots to:", tv4BookingChannel.Channel)
                    End If
                    If DateOutOfPeriod.Count > 0 Then
                        Dim Msg As String = "There were spots on the following dates (that does not belong to the campaign):" & vbCrLf
                        For i As Integer = 0 To DateOutOfPeriod.Count
                            Msg += Format(DateOutOfPeriod(i), "Short date") & vbCrLf
                        Next
                        Windows.Forms.MessageBox.Show(Msg, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Catch e As Exception
                    Windows.Forms.MessageBox.Show("An error occured when reading the spotlist, no spots were loaded!" & vbNewLine & vbNewLine & e.Message, "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Try
                'End If                
            End If
        Next
    End Sub
    Public Function LevenshteinDistance(ByVal s As String,
                    ByVal t As String) As Integer
        Dim n As Integer = s.Length
        Dim m As Integer = t.Length
        Dim d(n + 1, m + 1) As Integer

        If n = 0 Then
            Return m
        End If

        If m = 0 Then
            Return n
        End If

        Dim i As Integer
        Dim j As Integer

        For i = 0 To n
            d(i, 0) = i
        Next

        For j = 0 To m
            d(0, j) = j
        Next

        For i = 1 To n
            For j = 1 To m

                Dim cost As Integer
                If t(j - 1) = s(i - 1) Then
                    cost = 0
                Else
                    cost = 1
                End If

                d(i, j) = Math.Min(Math.Min(d(i - 1, j) + 1, d(i, j - 1) + 1),
                           d(i - 1, j - 1) + cost)
            Next
        Next

        Return d(n, m)
    End Function
    Function getStartDate(ByVal listOfSpotts As TV4Online.SpotlightApiV23.xsd.ConfirmationVersion)
        If listOfSpotts.RbsSpots.Length > 1 Then
            Dim min = (From d In listOfSpotts.RbsSpots Select d.BroadcastDate).Min()
            Return min
        Else
            Dim min = (From d In listOfSpotts.SpecificSpots Select d.BroadcastDate).Min()
            Return min
        End If
    End Function
    Function getEndDate(ByVal listOfSpotts As TV4Online.SpotlightApiV23.xsd.ConfirmationVersion)
        If listOfSpotts.RbsSpots.Length > 1 Then
            Dim max = (From d In listOfSpotts.RbsSpots Select d.BroadcastDate).Max()
            Return max
        Else
            Dim max = (From d In listOfSpotts.SpecificSpots Select d.BroadcastDate).Max()
            Return max
        End If
    End Function
    Public Function GetWeek(ByVal d As Date, ByVal channel As Object, ByVal bt As Object) As Object
        Dim tmpWeek As Object
        For Each c In Application.ActiveCampaign.Channels
            If c Is channel Then

                For Each b In c.BookingTypes
                    If b Is bt Then

                        For Each tmpWeek In b.Weeks
                            listOfWeek.Add(tmpWeek)
                        Next
                        Exit For
                    End If
                Next
            End If
        Next
        For Each period In listOfWeek
            If period.StartDate <= Int(d.ToOADate) Then
                If period.EndDate >= Int(d.ToOADate) Then
                    tmpWeek = period
                    Exit For
                End If
            End If
        Next
        Return tmpWeek
    End Function

    Function returnSpots()
    End Function
    Sub getAgencyRefNo(ByVal b As TV4Online.SpotlightApiV23.xsd.GetConfirmationsResponse)
        Dim camp = Application.ActiveCampaign

        For Each c In camp.Channels
            For Each bt In c.BookingTypes
                If bt.BookingIdSpotlight.ToString() = b.Confirmations(0).BookingId.ToString() Then

                    If bt.BookingAgencyRefNo = "" Then
                        bt.BookingAgencyRefNo = b.Confirmations(0).AgencyRefNo
                    End If
                    If bt.CampRefNo = "" Then
                        bt.CampRefNo = b.Confirmations(0).CampaignRefNo
                    End If
                End If
            Next
            Dim test As TV4Online.SpotlightApiV23.xsd.GetConfirmationsResponse = b
        Next
    End Sub
    Sub receieveConfirmationInfo(ByVal res As Object, ByVal book As TV4Online.SpotlightApiV23.xsd.Booking)
        Dim camp = Application.ActiveCampaign

        For Each c In camp.Channels
            If c.channelName = book.Channel Then

                For Each b In c.BookingTypes
                    If b.Name = book.Type Then

                    End If
                Next
            End If
        Next
    End Sub

    Sub receieveBookingInfoFromSpotlight(ByVal res As Object, ByVal book As TV4Online.SpotlightApiV23.xsd.Booking)

        Dim camp = Application.ActiveCampaign

        For Each c In camp.Channels
            If c.channelName = book.Channel Then

                For Each b In c.BookingTypes
                    If b.Name = book.Type Then

                        If res.returnValues.Count > 1 Then
                            b.BookingIdSpotlight = res.ReturnValues.item("BookingId")
                            b.BookingUrlSpotlight = res.ReturnValues.item("BookingUrl")
                            Exit Sub
                        End If
                    End If
                Next
            End If
        Next
    End Sub

    Sub UploadBookings_old()
        _internalApplication = Application
        If Application.GetSharedNetworkPreference("TV4Spotlight", "Endpoint") = "" Then
            Windows.Forms.MessageBox.Show("No server address has been specified, please contact the server administrator.", "TV4 Spotlight", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        End If
        Dim _camp = Application.ActiveCampaign
        Dim _binding As New WSHttpBinding(SecurityMode.Transport, False)
        _binding.CloseTimeout = New TimeSpan(0, 1, 0)
        _binding.OpenTimeout = New TimeSpan(0, 1, 0)
        _binding.SendTimeout = New TimeSpan(0, 1, 0)
        _binding.ReceiveTimeout = New TimeSpan(0, 10, 0)
        _binding.BypassProxyOnLocal = False
        _binding.TransactionFlow = False
        _binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard
        _binding.MaxBufferPoolSize = 2147483647
        _binding.MaxReceivedMessageSize = 2147483647
        _binding.MessageEncoding = WSMessageEncoding.Text
        _binding.TextEncoding = Text.UTF8Encoding.UTF8
        _binding.UseDefaultWebProxy = True
        _binding.AllowCookies = False

        With _binding.ReaderQuotas
            .MaxDepth = 32
            .MaxStringContentLength = 8192
            .MaxArrayLength = 16384
            .MaxBytesPerRead = 4096
            .MaxNameTableCharCount = 16384
        End With
        With _binding.ReliableSession
            .Ordered = True
            .Enabled = False
            .InactivityTimeout = New TimeSpan(0, 10, 0)
        End With
        With _binding.Security
            .Transport.ClientCredentialType = HttpClientCredentialType.None
            .Transport.ProxyCredentialType = HttpProxyCredentialType.None
            .Transport.Realm = ""
            .Message.ClientCredentialType = MessageCredentialType.Windows
            .Message.NegotiateServiceCredential = True
        End With
        Dim _endpoint As New EndpointAddressBuilder(New EndpointAddress(New Uri(Application.GetSharedNetworkPreference("TV4Spotlight", "Endpoint"))))

        Dim _client As New TV4Online.SpotlightApiV23.xsd.SpotlightApiV5Client(DirectCast(IIf(_endpoint.Uri.ToString.StartsWith("https"), _binding, New BasicHttpBinding), System.ServiceModel.Channels.Binding), _endpoint.ToEndpointAddress)

        Dim _availableChannels As String() = _client.GetChannels()

        For Each _chan In _camp.Channels
            If _availableChannels.Contains(_chan.ChannelName) Then
                For Each _bt In _chan.BookingTypes
                    If _bt.BookIt Then
                        Dim _campaign As New TV4Online.SpotlightApiV23.xsd.Campaign
                        Dim _bookings As New List(Of TV4Online.SpotlightApiV23.xsd.Booking)
                        _campaign.AgencyContactName = _camp.Buyer
                        _campaign.Client = _camp.Client
                        _campaign.Product = _camp.Product
                        _campaign.System = "Trinity 4.0"
                        _campaign.Country = "SE"
                        _campaign.Name = _camp.Name

                        Dim frm As New frmConfirm(_bt, _camp)
                        frm.txtContact.Text = Preferences.TV4Contact
                        Dim _ret = frm.ShowDialog
                        If _ret = Windows.Forms.DialogResult.OK Then
                            _campaign.ChannelContactName = frm.txtContact.Text
                            Preferences.TV4Contact = frm.txtContact.Text
                            _bookings.Add(frm.Booking)
                            _campaign.Bookings = _bookings.ToArray
                            Try
                                Dim _res = _client.CreateBooking(Preferences.Username, Preferences.GetPlainTextPassword, _campaign, False)
                                If _res.Status = TV4Online.SpotlightApiV23.xsd.StatusType.Success Then
                                    Windows.Forms.MessageBox.Show("Booking was successfully uploaded.", "TV4 Spotlight", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                ElseIf _res.Status = TV4Online.SpotlightApiV23.xsd.StatusType.Warning Then
                                    Dim _r As Windows.Forms.DialogResult = Windows.Forms.MessageBox.Show(String.Format("TV4 Spotlight responded with a warning :" & vbNewLine & vbNewLine & "{0}" & vbNewLine & vbNewLine & "Do you want to book anyway?", String.Join(vbNewLine, _res.ErrorMessages.Select(Function(_e) _e.Key & ": " & _e.Value).ToArray)), "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question)
                                    If _r = Windows.Forms.DialogResult.Cancel Then
                                        Exit Sub
                                    ElseIf _r = Windows.Forms.DialogResult.Yes Then
                                        _res = _client.CreateBooking(Preferences.Username, Preferences.GetPlainTextPassword, _campaign, True)
                                    End If
                                Else
                                    Windows.Forms.MessageBox.Show(String.Format("TV4 Spotlight responded with one or more errors:" & vbNewLine & "{0}", String.Join(vbNewLine, _res.ErrorMessages.Select(Function(_e) _e.Key & ": " & _e.Value).ToArray)), "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                                End If
                            Catch ex As FaultException(Of TV4Online.SpotlightApiV23.xsd.CreateBookingResponse)
                                Windows.Forms.MessageBox.Show(String.Format("TV4 Spotlight responded with one or more errors:" & vbNewLine & "{0}", String.Join(vbNewLine, ex.Detail.ErrorMessages.Select(Function(_e) _e.Key & ": " & _e.Value).ToArray)), "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                            End Try
                        ElseIf _ret = Windows.Forms.DialogResult.Cancel Then
                            _client.Close()
                            Exit Sub
                        End If
                    End If
                Next
            End If
        Next
        _client.Close()
    End Sub

    Public Function GetSaveData() As System.Xml.Linq.XElement Implements TrinityPlugin.IPlugin.GetSaveData

    End Function

    Public Function Menu() As TrinityPlugin.IPlugin.PluginMenu Implements TrinityPlugin.IPlugin.Menu
        Return _mnu
    End Function

    Public ReadOnly Property PluginName As String Implements TrinityPlugin.IPlugin.PluginName
        Get
            Return "TV4Online"
        End Get
    End Property

    <Import(GetType(ITrinityApplication))>
    Property Application() As ITrinityApplication

    Public Event SaveDataAvailale(sender As Object, e As System.EventArgs) Implements TrinityPlugin.IPlugin.SaveDataAvailale

    Private _preferences As New tabPreference(Me)
    Public ReadOnly Property PreferencesTab As TrinityPlugin.pluginPreferencesTab Implements TrinityPlugin.IPlugin.PreferencesTab
        Get
            _preferences.Username = Preferences.Username
            _preferences.Password = Preferences.GetPlainTextPassword
            Return _preferences
        End Get
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()

        'Dim _ser As New XmlSerializer(GetType(Preferences))
        'Using _stream As New IO.FileStream(IO.Path.Combine(Application.GetUserDataPath, "TV4Online.SpotlightApiV23.xsd.xml"), IO.FileMode.Create)
        '    _ser.Serialize(_stream, _prefs)
        'End Using
    End Sub
End Class
