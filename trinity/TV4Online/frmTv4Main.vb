Imports System.ComponentModel.Composition
Imports System.ComponentModel.Composition.Hosting
Imports System.Reflection
Imports TrinityPlugin
Imports System.ServiceModel
Imports System.Xml.Serialization
Imports System.Drawing
Imports System.Collections
Imports System.Diagnostics


Public Class frmTv4Main

    Dim _emptyList As New List(Of Object)
    Dim _booking As New TV4Online.SpotlightApiV23.xsd.Booking



    Private _client As New TV4Online.SpotlightApiV23.xsd.SpotlightApiV5Client(DirectCast(IIf(TV4OnlinePlugin.InternalApplication.GetSharedNetworkPreference("TV4Spotlight_V5", "Endpoint").StartsWith("https"), New WSHttpBinding(SecurityMode.Transport), New BasicHttpBinding), System.ServiceModel.Channels.Binding), New EndpointAddress(TV4OnlinePlugin.InternalApplication.GetSharedNetworkPreference("TV4Spotlight_V5", "Endpoint")))
    Private _bookings As New List(Of TV4Online.SpotlightApiV23.xsd.Booking)
    Dim res As New TV4OnlinePlugin

    Shared _addedValueToSurcharge As Dictionary(Of String, String)
    Shared _indexToIndex As Dictionary(Of String, String)

    Dim _camp As Object
    Dim llistOfAgencies As List(Of Object)
    Dim selectecAgency As Object

    Private availChannels As String()
    Private _iTrinityApplication As ITrinityApplication

    Private _types As New List(Of String)
    Private _surcharges As New List(Of String)
    Private _tv4Indices As New List(Of String)
    Private _targets As New List(Of Object)
    Private _logo As New List(Of Object)
    Private _error As New List(Of Integer)
    Private showWrnAddedValue As Boolean = False
    Private multipleOrganizations As Boolean = False
    Private orgID As String = ""
    Private _nettoListSpot As List(Of clTrinity.Trinity.cBookedSpot)

    Private _skipSpecifics As New List(Of TV4Online.SpotlightApiV23.xsd.SpecificSpot)
    Private _skipRBS As New List(Of TV4Online.SpotlightApiV23.xsd.RbsFilm)

    Dim _periods As New List(Of TV4Online.SpotlightApiV23.xsd.RbsPeriod)
    Public ReadOnly Property Booking As TV4Online.SpotlightApiV23.xsd.Booking
        Get
            Return _booking
        End Get
    End Property
    Public ReadOnly Property Bookings As List(Of TV4Online.SpotlightApiV23.xsd.Booking)
        Get
            Return _bookings
        End Get
    End Property

    Private Sub getOrganizations()
        Dim test = _client.GetUserOrganizations(res.Preferences.Username, res.Preferences.Token)

        If test.Status = TV4Online.SpotlightApiV23.xsd.StatusType.Success Then
            For Each tmpOrg As Object In test.ReturnValues.Values
                cmbOrganizations.Items.Add(tmpOrg)
            Next
            If cmbOrganizations.Items.Count > 1 Then
                cmbOrganizations.Visible = True
                multipleOrganizations = True
                For Each item In cmbOrganizations.Items
                    If item.ToString().Contains("Stockholm") Then
                        cmbOrganizations.SelectedItem = item
                    Else
                        cmbOrganizations.SelectedIndex = 0
                    End If
                Next
            End If
        Else
            cmbOrganizations.Visible = False
        End If
        'Test for receieving targets from WS
        Dim target = _client.GetTargetsForChannel("TV4", "2015-03-03")
        Dim allTarget = _client.GetTargets("2015-03-03")
    End Sub

    Private Function getOrgID(ByVal oID As Object)
        Dim test = _client.GetUserOrganizations(res.Preferences.Username, res.Preferences.Token)
        Dim orgList As New List(Of Object)
        If test.Status = TV4Online.SpotlightApiV23.xsd.StatusType.Success Then
            For Each tmpOrg In test.ReturnValues
                orgList.Add(tmpOrg)
            Next
            For Each tmpOrg In orgList
                If tmpOrg.Value = oID Then
                    orgID = tmpOrg.Key
                    Return tmpOrg.Key
                End If
            Next
        End If
        Return Nothing
    End Function
    Sub checkBookingIdAndURL(ByVal b As TV4Online.SpotlightApiV23.xsd.Booking)
        If b.BookingIdSpotlight IsNot Nothing Then
            lblBookingUrlSpotlight.Visible = True
            lblBookingUrlSpotlight.Text = b.BookingUrlSpotlight
        Else
            lblBookingUrlSpotlight.Visible = False
        End If
    End Sub
    Sub WhoLoggingIn()
        lblLoginName.Text = res.Preferences.Username
    End Sub
    Public Sub New(Campaign As Object, availableChannels As String(), ByRef tmpNettoSpotlist As List(Of clTrinity.Trinity.cBookedSpot))

        InitializeComponent()
        _nettoListSpot = tmpNettoSpotlist
        _surcharges = _client.GetSurchargeNames.ToList
        _tv4Indices = _client.GetIndexNames.ToList
        _types = _client.GetBookingTypes.ToList

        getOrganizations()
        'WhoLoggingIn()
        _camp = Campaign


        Dim _availableChannels = availableChannels
        Dim combinationsChannels As New List(Of String)
        For Each _tmpComb In _camp.combinations
            If _tmpComb.ShowAsOne And _tmpComb.Relations.Count > 0 Then
                Dim b As New TV4Online.SpotlightApiV23.xsd.Booking
                Dim foundIt = False
                For Each _tmpCC In _tmpComb.Relations
                    If _availableChannels.Contains(_tmpCC.ChannelName) Then
                        foundIt = True
                        For Each _week In _tmpCC.Bookingtype.Weeks
                            Dim _period As New TV4Online.SpotlightApiV23.xsd.RbsPeriod
                            Dim _dayparts As New List(Of TV4Online.SpotlightApiV23.xsd.RbsDaypart)

                            _period.StartDate = Date.FromOADate(_week.StartDate)
                            _period.EndDate = Date.FromOADate(_week.EndDate)

                            For Each _dp In _tmpCC.Bookingtype.Dayparts
                                Dim _dayp As New TV4Online.SpotlightApiV23.xsd.RbsDaypart
                                _dayp.NegotiatedDiscount = Math.Round(_tmpCC.Bookingtype.BuyingTarget.Discount, 4)
                                If _dp.IsPrime Then
                                    _dayp.Name = "Prime"
                                Else
                                    _dayp.Name = "Offprime"
                                End If
                                _dayp.GrossCPP30 = _week.GrossCPP30(_dp.MyIndex - 1)
                                Dim _films As New List(Of TV4Online.SpotlightApiV23.xsd.RbsFilm)
                                For Each _f In _week.Films
                                    Dim _film As New TV4Online.SpotlightApiV23.xsd.RbsFilm
                                    If String.IsNullOrEmpty(_f.Filmcode) Then
                                        _film.FilmCode = _f.Name
                                    Else
                                        _film.FilmCode = _f.Filmcode
                                    End If
                                    _film.FilmLength = _f.FilmLength
                                    _film.FilmIndex = _f.Index
                                    _film.TRP = _week.TRPBuyingTarget * (_f.Share / 100) * (_dp.Share / 100)
                                    _film.NetBudget = _film.TRP * _week.NetCPP30(_dp.MyIndex - 1) * (_f.Index / 100)
                                    Dim _indices As New List(Of TV4Online.SpotlightApiV23.xsd.Index)
                                    For Each _idx In _tmpCC.Bookingtype.Indexes
                                        If _idx.FromDate <= _period.EndDate AndAlso _idx.ToDate >= _period.StartDate Then
                                            Dim _index As New TV4Online.SpotlightApiV23.xsd.Index
                                            _index.Modifier = _idx.Index / 100
                                            _index.Name = _idx.Name
                                            _indices.Add(_index)
                                        End If
                                    Next
                                    For Each _idx In _tmpCC.Bookingtype.BuyingTarget.Indexes
                                        If _idx.FromDate <= _period.EndDate AndAlso _idx.ToDate >= _period.StartDate Then
                                            Dim _index As New TV4Online.SpotlightApiV23.xsd.Index
                                            _index.Modifier = _idx.Index / 100
                                            _index.Name = _idx.Name
                                            _indices.Add(_index)
                                        End If
                                    Next
                                    _film.Indices = _indices.ToArray
                                    If _film.TRP > 0 OrElse _film.NetBudget > 0 Then
                                        _films.Add(_film)
                                    End If
                                Next
                                _dayp.RbsFilms = _films.ToArray
                                If _dayp.RbsFilms.Count > 0 Then
                                    _dayparts.Add(_dayp)
                                End If
                            Next
                            _period.RbsDayparts = _dayparts.ToArray
                            If _period.RbsDayparts.Count > 0 Then
                                _periods.Add(_period)
                            End If
                            _period.Channel = _tmpCC.ChannelName
                        Next
                        If _tmpCC.Bookingtype.IsCompensation Then
                            b.Type = "Kompensation - RBS"
                        Else
                            b.Type = _tmpCC.BookingType.Name
                        End If
                        b.TrinityType = _tmpComb.Name

                        'Booking information which is stored and receieved when uploading booking to Spotlight.
                        b.BookingIdSpotlight = _tmpCC.Bookingtype.BookingIdSpotlight
                        b.BookingUrlSpotlight = _tmpCC.Bookingtype.BookingUrlSpotlight
                        If (b.Channel Is Nothing) Then
                            b.Channel = _tmpComb.Name
                        End If
                        If (b.AgencyBookingRefNo Is Nothing) Then
                            b.AgencyBookingRefNo = _tmpCC.Bookingtype.OrderNumber
                        End If
                        If (b.ChannelBookingRefNo Is Nothing) Then
                            b.ChannelBookingRefNo = _tmpCC.Bookingtype.ContractNumber
                        End If
                        b.Comments += _tmpCC.Bookingtype.Comments
                        b.StartDate = Date.FromOADate(_camp.StartDate)
                        b.EndDate = Date.FromOADate(_camp.EndDate)
                        'b.MaxBudget = _tmpCC.Bookingtype.PlannedNetBudget
                        If (b.UniverseSize = 0) Then
                            b.UniverseSize = _tmpCC.Bookingtype.BuyingTarget.getUniSizeNat(_camp.StartDate)
                        End If
                        If (b.Target Is Nothing) Then
                            b.Target = _tmpCC.Bookingtype.BuyingTarget.TargetName.ToString.Trim.Replace(" ", "")
                            If b.Target.Contains("W") Then
                                b.Target = b.Target.Replace("W", "K")
                            End If
                            If IsNumeric(b.Target.Substring(0, 1)) Then
                                b.Target = "A" & b.Target
                            End If
                        End If

                        Try 'Add logo to list so its easier to find the specific logo
                            imgLogo.Image = _tmpCC.Bookingtype.ParentChannel.GetImage()

                            Dim pic = imgLogo.Image
                            If pic IsNot Nothing Then
                                pic.Tag = _tmpCC.Bookingtype.ChannelName
                            End If
                            If pic IsNot Nothing Then
                                _logo.Add(pic)
                            End If
                        Catch ex As Exception

                        End Try
                        b.MaxBudget += Math.Round(_tmpCC.Bookingtype.PlannedNetBudget)
                        txtBudget.Text = Format(_booking.MaxBudget, "#####0")
                        b.RbsPeriods = _periods.ToArray
                        b.Selected = True
                        b.Targets = _client.GetTargetsForChannel(_tmpCC.ChannelName, b.StartDate)
                    End If
                    combinationsChannels.Add(_tmpCC.Bookingtype.ParentChannel.ChannelName + " " + _tmpCC.Bookingtype.name)
                Next
                If foundIt = True Then
                    _bookings.Add(b)
                End If
            End If
        Next
        For Each _chan In _camp.Channels
            If _availableChannels.Contains(_chan.ChannelName) Then
                For Each _bt In _chan.BookingTypes

                    If _bt.BookIt And Not combinationsChannels.Contains(_chan.ChannelName + " " + _bt.name) Then
                        Dim b As New TV4Online.SpotlightApiV23.xsd.Booking
                        'Dim booking As New TV4Bookings
                        Dim _periods As New List(Of TV4Online.SpotlightApiV23.xsd.RbsPeriod)
                        If _bt.IsRBS Then
                            For Each _week In _bt.Weeks
                                Dim _period As New TV4Online.SpotlightApiV23.xsd.RbsPeriod
                                Dim _dayparts As New List(Of TV4Online.SpotlightApiV23.xsd.RbsDaypart)

                                _period.StartDate = Date.FromOADate(_week.StartDate)
                                _period.EndDate = Date.FromOADate(_week.EndDate)

                                For Each _dp In _bt.Dayparts
                                    Dim _dayp As New TV4Online.SpotlightApiV23.xsd.RbsDaypart
                                    _dayp.NegotiatedDiscount = Math.Round(_bt.BuyingTarget.Discount, 4)
                                    If _dp.IsPrime Then
                                        _dayp.Name = "Prime"
                                    Else
                                        _dayp.Name = "Offprime"
                                    End If
                                    _dayp.GrossCPP30 = _week.GrossCPP30(_dp.MyIndex - 1)
                                    Dim _films As New List(Of TV4Online.SpotlightApiV23.xsd.RbsFilm)
                                    For Each _f In _week.Films
                                        Dim _film As New TV4Online.SpotlightApiV23.xsd.RbsFilm
                                        If String.IsNullOrEmpty(_f.Filmcode) Then
                                            _film.FilmCode = _f.Name
                                        Else
                                            _film.FilmCode = _f.Filmcode
                                        End If
                                        _film.FilmLength = _f.FilmLength
                                        _film.FilmIndex = _f.Index
                                        _film.TRP = _week.TRPBuyingTarget * (_f.Share / 100) * (_dp.Share / 100)
                                        _film.NetBudget = _film.TRP * _week.NetCPP30(_dp.MyIndex - 1) * (_f.Index / 100)
                                        Dim _indices As New List(Of TV4Online.SpotlightApiV23.xsd.Index)
                                        For Each _idx In _bt.Indexes
                                            If _idx.FromDate <= _period.EndDate AndAlso _idx.ToDate >= _period.StartDate Then
                                                Dim _index As New TV4Online.SpotlightApiV23.xsd.Index
                                                _index.Modifier = _idx.Index / 100
                                                _index.Name = _idx.Name
                                                _indices.Add(_index)
                                            End If
                                        Next
                                        For Each _idx In _bt.BuyingTarget.Indexes
                                            If _idx.FromDate <= _period.EndDate AndAlso _idx.ToDate >= _period.StartDate Then
                                                Dim _index As New TV4Online.SpotlightApiV23.xsd.Index
                                                _index.Modifier = _idx.Index / 100
                                                _index.Name = _idx.Name
                                                _indices.Add(_index)
                                            End If
                                        Next
                                        _film.Indices = _indices.ToArray
                                        If _film.TRP > 0 OrElse _film.NetBudget > 0 Then
                                            _films.Add(_film)
                                        End If
                                    Next
                                    _dayp.RbsFilms = _films.ToArray
                                    If _dayp.RbsFilms.Count > 0 Then
                                        _dayparts.Add(_dayp)
                                    End If
                                Next
                                _period.RbsDayparts = _dayparts.ToArray
                                If _period.RbsDayparts.Count > 0 Then
                                    _periods.Add(_period)
                                End If
                                _period.Channel = _chan.ChannelName
                            Next
                            If _bt.IsCompensation Then
                                b.Type = "Kompensation - RBS"
                            Else
                                b.Type = "RBS"
                            End If
                            b.TrinityType = _bt.Name

                            'Booking information which is stored and receieved when uploading booking to Spotlight.
                            b.BookingIdSpotlight = _bt.BookingIdSpotlight
                            b.BookingUrlSpotlight = _bt.BookingUrlSpotlight

                            b.Channel = _bt.ParentChannel.ChannelName
                            b.AgencyBookingRefNo = _bt.OrderNumber
                            b.ChannelBookingRefNo = _bt.ContractNumber
                            b.Comments = _bt.Comments
                            b.StartDate = Date.FromOADate(_camp.StartDate)
                            b.EndDate = Date.FromOADate(_camp.EndDate)
                            b.MaxBudget = _bt.PlannedNetBudget
                            b.UniverseSize = _bt.BuyingTarget.getUniSizeNat(_camp.StartDate)
                            b.Target = _bt.BuyingTarget.TargetName.ToString.Trim.Replace(" ", "")
                            If b.Target.Contains("W") Then
                                b.Target = b.Target.Replace("W", "K")
                            End If
                            If IsNumeric(b.Target.Substring(0, 1)) Then
                                b.Target = "A" & b.Target
                            End If

                            Try 'Add logo to list so its easier to find the specific logo
                                imgLogo.Image = _bt.ParentChannel.GetImage()

                                Dim pic = imgLogo.Image
                                If pic IsNot Nothing Then
                                    pic.Tag = _chan.ChannelName
                                End If
                                If pic IsNot Nothing Then
                                    _logo.Add(pic)
                                End If
                            Catch ex As Exception

                            End Try
                            b.MaxBudget = Math.Round(_bt.PlannedNetBudget)
                            txtBudget.Text = Format(_booking.MaxBudget, "#####0")
                            b.RbsPeriods = _periods.ToArray
                            b.Selected = True
                            b.Targets = _client.GetTargetsForChannel(b.Channel, b.StartDate)
                            Dim Tmptargets = _client.GetTargetsForChannel(b.Channel, b.StartDate)
                            _bookings.Add(b)
                        Else
                            'Changed from _camp.BookedSpots to _nettoListSpot'
                            Dim _spots As New List(Of TV4Online.SpotlightApiV23.xsd.SpecificSpot)
                            For Each _spot In _nettoListSpot

                                If _spot.Bookingtype Is _bt Then
                                    Dim _s As New TV4Online.SpotlightApiV23.xsd.SpecificSpot
                                    _s.BreakID = _spot.breakID
                                    _s.BroadcastDate = _spot.AirDate
                                    _s.BroadcastTime = _spot.MaM
                                    _s.EstimatedTRP = _spot.ChannelEstimate
                                    If _spot.Filmcode <> "" Then
                                        _s.FilmCode = _spot.Filmcode
                                    Else
                                        _s.FilmCode = "No filmcode"
                                    End If
                                    _s.FilmIndex = _spot.Film.Index
                                    _s.FilmLength = _spot.Film.FilmLength
                                    _s.GrossPrice30 = _spot.GrossPrice30
                                    _s.NegotiatedDiscount = _bt.Buyingtarget.Discount
                                    _s.NetPrice = _spot.NetPrice * _spot.AddedValueIndex
                                    _s.ProgramName = _spot.Programme
                                    _s.Target = _booking.Target
                                    Dim _indices As New List(Of TV4Online.SpotlightApiV23.xsd.Index)
                                    For Each _idx In _bt.Indexes
                                        If _idx.FromDate <= _s.BroadcastDate AndAlso _idx.ToDate >= _s.BroadcastDate Then
                                            Dim _index As New TV4Online.SpotlightApiV23.xsd.Index
                                            _index.Modifier = _idx.Index / 100
                                            _index.Name = _idx.Name
                                            _indices.Add(_index)
                                        End If
                                    Next
                                    For Each _idx In _bt.BuyingTarget.Indexes
                                        If _idx.FromDate <= _s.BroadcastDate AndAlso _idx.ToDate >= _s.BroadcastDate Then
                                            Dim _index As New TV4Online.SpotlightApiV23.xsd.Index
                                            _index.Modifier = _idx.Index / 100
                                            _index.Name = _idx.Name
                                            _indices.Add(_index)
                                        End If
                                    Next
                                    _s.Indices = _indices.ToArray
                                    Dim _scs As New List(Of TV4Online.SpotlightApiV23.xsd.Surcharge)
                                    For Each _av In _spot.AddedValues.Values
                                        Dim _sc As New TV4Online.SpotlightApiV23.xsd.Surcharge
                                        Dim _avName As String = _av.Name
                                        Dim _tmpSC = _surcharges.FirstOrDefault(Function(s) s.ToLower = _avName.ToLower.Replace(" ", ""))
                                        If String.IsNullOrEmpty(_tmpSC) Then
                                            If TV4OnlinePlugin.InternalApplication.GetUserPreference("TV4Spotlight", _av.Name) <> "" Then
                                                _sc.Name = TV4OnlinePlugin.InternalApplication.GetUserPreference("TV4Spotlight", _av.Name)
                                            Else
                                                _sc.Name = _av.Name
                                                'imgSpecWarning.Visible = True
                                            End If
                                        Else
                                            _sc.Name = _tmpSC
                                        End If
                                        _sc.Modifier = _av.IndexNet / 100
                                        _sc.NetCost = _spot.NetPrice * (_sc.Modifier - 1)
                                        Debug.Print(_spot.NetPrice & " = " & _s.NetPrice & " - " & _sc.NetCost)
                                        _scs.Add(_sc)
                                    Next
                                    _s.Surcharges = _scs.ToArray
                                    _spots.Add(_s)
                                End If

                                b.SpecificsSpots = _spots.ToArray.OrderBy(Function(_s) _s.BroadcastTime).OrderBy(Function(_s) _s.BroadcastDate).ToArray
                            Next
                            Try 'Add logo to list so its easier to find the specific logo
                                imgLogo.Image = _bt.ParentChannel.GetImage()

                                Dim pic = imgLogo.Image
                                If pic IsNot Nothing Then
                                    pic.Tag = _chan.ChannelName
                                End If
                                If pic IsNot Nothing Then
                                    _logo.Add(pic)
                                End If
                            Catch ex As Exception

                            End Try
                            If _bt.IsCompensation Then
                                b.Type = "Kompensation - Specific"
                            Else
                                b.Type = "Specifics"
                            End If
                            b.BookingIdSpotlight = _bt.BookingIdSpotlight
                            b.BookingUrlSpotlight = _bt.BookingUrlSpotlight
                            b.TrinityType = _bt.Name
                            b.Channel = _bt.ParentChannel.ChannelName
                            b.AgencyBookingRefNo = _bt.OrderNumber
                            b.ChannelBookingRefNo = _bt.ContractNumber
                            b.Comments = _bt.Comments
                            If b.SpecificsSpots IsNot Nothing Then
                                If b.SpecificsSpots.Count > 0 Then
                                    Dim tmpStartDate = (From p In b.SpecificsSpots Order By p.BroadcastDate Ascending Select p).First
                                    Dim tmpEndtDate = (From p In b.SpecificsSpots Order By p.BroadcastDate Descending).First

                                    b.StartDate = tmpStartDate.BroadcastDate
                                    b.EndDate = tmpEndtDate.BroadcastDate
                                Else
                                    b.StartDate = Date.FromOADate(_camp.StartDate)
                                    b.EndDate = Date.FromOADate(_camp.EndDate)

                                End If
                            End If
                            b.MaxBudget = _bt.PlannedNetBudget
                            b.UniverseSize = _bt.BuyingTarget.getUniSizeNat(_camp.StartDate)
                            b.Target = _bt.BuyingTarget.TargetName.ToString.Trim.Replace(" ", "")
                            b.Selected = True
                            If IsNumeric(b.Target.Substring(0, 1)) Then
                                b.Target = "A" & b.Target
                            End If
                            b.Targets = _client.GetTargetsForChannel(b.Channel, b.StartDate)
                            _bookings.Add(b)
                        End If
                    End If
                Next
            End If
        Next
        fillgrid()

    End Sub
    Private Shared _internalApplication As ITrinityApplication
    Friend Shared Function InternalApplication() As ITrinityApplication
        Return _internalApplication
    End Function
    Sub fillgrid()
        For Each _bt As TV4Online.SpotlightApiV23.xsd.Booking In _bookings
            Dim newRow As Integer = grdBookings.Rows.Add
            grdBookings.Rows(newRow).Tag = _bt
        Next
    End Sub
    Private Sub lnkRBS_Click(sender As Object, e As System.EventArgs) Handles lnkRBS.Click
        Dim _frm As New frmRBS(_booking.RbsPeriods, _skipRBS, _booking.Channel)
        _frm.ShowDialog()
        _frm.Text = _booking.Channel
        If _skipRBS.Count < 1 Then
            _skipRBS = _frm.SkipList
        Else
            For Each _skip As TV4Online.SpotlightApiV23.xsd.RbsFilm In _frm.SkipList
                If Not _skipRBS.Contains(_skip) Then
                    _skipRBS.Add(_skip)
                End If
            Next
        End If
        If Not txtBudget.Text = "" Then
            cmdRefreshBudget.Visible = (_frm.grdRBS.GetColumnSum(_frm.colNetPrice.Index) <> txtBudget.Text)
            cmdRefreshBudget.Tag = _frm.grdRBS.GetColumnSum(_frm.colNetPrice.Index)
        End If
        If _skipRBS.Count > 0 Then
            chkRBS.CheckState = Windows.Forms.CheckState.Indeterminate
        End If
    End Sub
    Private Sub lnkSpecifics_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSpecifics.LinkClicked
        Dim _frm As New frmSpecifics(_booking.SpecificsSpots, _skipSpecifics)
        _frm.ShowDialog()
        If _skipSpecifics.Count < 1 Then
            _skipSpecifics = _frm.SkipList
        Else
            For Each _skip As TV4Online.SpotlightApiV23.xsd.SpecificSpot In _frm.SkipList
                If Not _skipSpecifics.Contains(_skip) Then
                    _skipSpecifics.Add(_skip)
                End If
            Next
        End If
        grdBookings.Invalidate()
        If Not txtBudget.Text = "" Then
            cmdRefreshBudget.Visible = (_frm.grdSpecifics.GetColumnSum(_frm.colNetPrice.Index) <> txtBudget.Text)
            cmdRefreshBudget.Tag = _frm.grdSpecifics.GetColumnSum(_frm.colNetPrice.Index)
            If _skipSpecifics.Count > 0 Then
                chkSpecifics.CheckState = Windows.Forms.CheckState.Indeterminate
            End If
        End If

    End Sub
    Private Sub grdBookings_CellValueNeeded(sender As Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdBookings.CellValueNeeded
        Dim _period As TV4Online.SpotlightApiV23.xsd.Booking = _bookings(e.RowIndex)
        Select Case e.ColumnIndex
            Case colSelected.Index
                e.Value = _period.Selected
            Case colTarget.Index
                e.Value = _period.Target
            Case (colStart.Index)
                e.Value = Format(_period.StartDate, "yyyy-MM-dd")
            Case colEnd.Index
                e.Value = Format(_period.EndDate, "yyyy-MM-dd")
            Case colBudget.Index
                e.Value = Format(_period.MaxBudget, "###,##0" + " kr")
            Case colName.Index
                e.Value = _period.Channel
            Case colType.Index
                e.Value = _period.TrinityType
            Case colTv4BT.Index
                e.Value = _period.Type
        End Select
    End Sub
    Private Sub grdBookings_CellValuePushed(sender As System.Object, e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdBookings.CellValuePushed
        Dim _period As TV4Online.SpotlightApiV23.xsd.Booking = _bookings(e.RowIndex)
        Select Case e.ColumnIndex
            Case colSelected.Index
                _period.Selected = e.Value
        End Select
    End Sub
    Private Sub grdBookings_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdBookings.CellContentClick
        If e.RowIndex < 0 Then Exit Sub
    End Sub
    Private Sub grdBookings_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdBookings.CellClick
        If e.RowIndex < 0 Then Exit Sub
        _booking = grdBookings.SelectedRows.Item(0).Tag
        cmdRefreshBudget.Visible = False
        checkBookingIdAndURL(_booking)

        cmbTarget.Items.Clear()
        Try
            For Each tmpTarg In _booking.Targets
                cmbTarget.Items.Add(tmpTarg)
            Next
        Catch ex As Exception
            If IsRBS(_booking) Then
                lblRbsWrn.Text = "Target is not defined"
            Else
                lblSpecWrn.Text = "Target is not defined"
            End If
        End Try
        UpdateForm()
    End Sub
    Private Sub UpdateForm()
        Try
            'Uncheck and disable links for RBS or Specifics
            If Not _booking.Type Is Nothing Then
                If _booking.Type.Contains("RBS") Then
                    chkLblRBS()
                ElseIf _booking.Type.Contains("Specific") Then
                    chkLblSpec()
                ElseIf _booking.Type.Contains("Minute") Then
                    chkLblSpec()
                End If
            Else
                lnkRBS.Enabled = False
                chkRBS.Enabled = False
                chkRBS.Checked = False
                chkSpecifics.Enabled = False
                chkSpecifics.Checked = False
                lnkSpecifics.Enabled = False
            End If
            'Present booking info form spotlight
            checkBookingIdAndURL(_booking)

            'Choose target for each viewed booking
            If cmbTarget.Items.Count < 1 Then
                If _booking.Targets IsNot Nothing Then
                    For Each tmpTarg In _booking.Targets
                        cmbTarget.Items.Add(tmpTarg)
                    Next
                End If
            End If
            For Each _target As String In cmbTarget.Items
                If _target = _booking.Target Then
                    cmbTarget.SelectedItem = _target
                    Exit For
                End If
            Next
            'Choose bookingType for each viewed booking
            For Each _bt As String In cmbBookingType.Items
                If _bt = _booking.Type.ToString() Then
                    cmbBookingType.SelectedItem = _bt
                    Exit For
                End If
            Next
            'If type does not match spotlights types
            If Not _types.Contains(_booking.Type) Then
                wrnBt.Visible = True
            Else
                wrnBt.Visible = False
            End If
            'Pick logo
            For Each pic In _logo
                If pic IsNot Nothing Then
                    If pic.tag IsNot Nothing Then
                        If pic.tag = _booking.Channel Then
                            imgLogo.Image = pic
                            Exit For
                        End If
                    End If
                End If
            Next
            lblBookingType.Text = _booking.Channel + " " + _booking.Type
            'Uncheck and disable links for RBS or Specifics
            If IsRBS(_booking) Then
                If _booking.RbsPeriods.Length < 1 Then
                    lblRbsWrn.Visible = True
                    lblSpecWrn.Visible = False
                Else
                    lblRbsWrn.Visible = False
                    lblSpecWrn.Visible = False
                End If
            Else
                If Not _booking.SpecificsSpots Is Nothing Then
                    If _booking.SpecificsSpots.Length < 1 Then
                        lblRbsWrn.Visible = False
                        lblSpecWrn.Visible = True
                        lblSpecWrn.ForeColor = Color.Red
                    Else
                        lblSpecWrn.Visible = False
                        lblRbsWrn.Visible = False
                        lblSpecWrn.ForeColor = Color.Blue
                    End If
                Else
                    lblSpecWrn.Visible = True
                    lblRbsWrn.Visible = False
                End If
            End If
            If Not IsRBS(_booking) Then
                If _booking.SpecificsSpots IsNot Nothing Then
                    If _booking.SpecificsSpots.Length > 1 Then
                        For Each _specificSpot In _booking.SpecificsSpots
                            For Each _sc In _specificSpot.Surcharges
                                Dim _tmpSC = _surcharges.FirstOrDefault(Function(s) s.ToLower = _sc.Name.ToLower.Replace(" ", ""))
                                If String.IsNullOrEmpty(_tmpSC) Then
                                    imgSpecWarning.Visible = True
                                Else
                                    imgSpecWarning.Visible = False
                                End If
                            Next
                        Next
                    Else
                        imgSpecWarning.Visible = False
                    End If
                End If
            Else
                imgSpecWarning.Visible = False
            End If
        Catch ex As Exception
        End Try
    End Sub
    Sub chkLblSpec()
        lnkRBS.Enabled = False
        chkRBS.Enabled = False
        chkRBS.Checked = False
        chkSpecifics.Enabled = True
        chkSpecifics.Checked = True
        lnkSpecifics.Enabled = True
    End Sub
    Sub chkLblRBS()
        lnkSpecifics.Enabled = False
        chkSpecifics.Enabled = False
        chkSpecifics.Checked = False
        chkRBS.Enabled = True
        chkRBS.Checked = True
        lnkRBS.Enabled = True
    End Sub
    Public Function IsRBS(_book As TV4Online.SpotlightApiV23.xsd.Booking) As Boolean
        If _book.Type IsNot Nothing Then
            If _book.Type.Contains("RBS") Then
                Return True
            ElseIf _book.Type.Contains("Guldpaket") Then
                Return True
            Else
                Return False
            End If
            Return True
        End If
    End Function
    Private Sub grdBookings_CellErrorTextNeeded(sender As System.Object, e As System.Windows.Forms.DataGridViewCellErrorTextNeededEventArgs) Handles grdBookings.CellErrorTextNeeded
        Dim _booking As TV4Online.SpotlightApiV23.xsd.Booking = _bookings(e.RowIndex)
        If e.ColumnIndex = colError.Index Then
            Dim _sb As New System.Text.StringBuilder
            For Each _spot In _camp.BookedSpots
                Dim chnString = _booking.Channel + " " + _booking.TrinityType
                If _spot.BookingType.ToString = chnString Then
                    Dim _scs As New List(Of TV4Online.SpotlightApiV23.xsd.Surcharge)
                    For Each _av In _spot.AddedValues.Values
                        Dim _sc As New TV4Online.SpotlightApiV23.xsd.Surcharge
                        Dim _avName As String = _av.name
                        Dim _tmpSC = _surcharges.FirstOrDefault(Function(s) s.ToLower = _avName.ToLower.Replace(" ", ""))
                        If String.IsNullOrEmpty(_tmpSC) Then
                            If TV4OnlinePlugin.InternalApplication.GetUserPreference("TV4Spotlight", _av.Name) <> "" Then
                                _sc.Name = TV4OnlinePlugin.InternalApplication.GetUserPreference("TV4Spotlight", _av.Name)
                            Else
                                _sc.Name = _av.Name
                                _sb.AppendLine(_sc.Name & " is not a valid Spotlight surcharge")
                            End If
                        Else
                            _sc.Name = _tmpSC
                            colError.Visible = False
                        End If
                    Next
                End If
            Next
            e.ErrorText = _sb.ToString()
        End If
        If e.ColumnIndex = colTarget.Index Then
            Dim _sb As New System.Text.StringBuilder
            Dim _res As Boolean = False
            If Not _booking.Targets Is Nothing Then
                For Each tmpTarget In _booking.Targets
                    If tmpTarget = _booking.Target Then
                        _res = True
                        Exit For
                    Else
                        _res = False
                    End If
                Next
                If _res = False Then
                    _sb.AppendLine("Target is not valid")
                End If
            End If
            e.ErrorText = _sb.ToString()
        End If
    End Sub
    Private Sub grdBookings_CellFormatting(sender As System.Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdBookings.CellFormatting

        If e.RowIndex < 0 Then Exit Sub

        _booking = grdBookings.SelectedRows.Item(0).Tag

        txtBudget.Text = Format(_booking.MaxBudget, "#####0")

        For Each item As String In cmbBookingType.Items
            If item = _booking.Type Then
                cmbBookingType.SelectedItem = item
                Exit For
            End If
        Next
        UpdateForm()
    End Sub
    Function checkFaultySpots()
        For Each TmpSpot In _camp.BookedSpots
            Dim s = TmpSpot
            If TmpSpot.Film Is Nothing Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Sub frmTv4Main_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If checkFaultySpots() Then
            Exit Sub
        Else
            UpdateForm()
            'Check if the specific bookingtype matches the spotlight bookingtype
            For Each _bt As String In _client.GetBookingTypes
                Dim _idx = cmbBookingType.Items.Add(_bt)
                If _bt.ToUpper() = _bookings.Item(0).Type.ToUpper() Then
                    cmbBookingType.SelectedIndex = _idx
                End If
            Next
            If _booking.Channel IsNot Nothing Then
                For Each tmpTarget In _client.GetTargetsForChannel(_booking.Channel, _booking.StartDate)
                    cmbTarget.Items.Add(tmpTarget)
                Next
            End If
            Dim _errors As New System.Text.StringBuilder


            For Each tmpBT In _bookings
                Dim bt = tmpBT.Type
                'Dim _tmpSC = _surcharges.FirstOrDefault(Function(s) s.ToLower = _avName.ToLower.Replace(" ", ""))
                Dim _tmpSC = _types.FirstOrDefault(Function(b) b.ToLower = bt.ToLower)
                If String.IsNullOrEmpty(_tmpSC) Then
                    _errors.AppendLine("")
                    _errors.AppendLine("The bookingtype '" & _booking.Target & "' was not found among the bookable targets in for " + _booking.Channel + ".")
                    _errors.AppendLine("Please specify what target to use.")
                End If
            Next
            'Dim localOrgID = res.GetOrg()

            'Object holding all Mediabyråer, e.g Mediacom, GroupM, Mindshare...
            llistOfAgencies = res.getClientList()


            ' Append  all agencies to a global variable through a foreach loop
            Dim _agencies As New List(Of Object)
            For Each agency As Object In llistOfAgencies
                _agencies.Add(agency)
            Next
            ' Create bindingsource for agency combobox.
            Dim _agencyBinding As New Windows.Forms.BindingSource
            ' Add agencies to data soruce
            _agencyBinding.DataSource = llistOfAgencies
            ' Append agency binding source to combobbox datasource.
            cmbAgencies.DataSource = _agencyBinding

            ' Display only Agency name to combobox.
            cmbAgencies.DisplayMember = "AgencyName"

            ' Select first item in the list of agencies
            selectecAgency = llistOfAgencies(0)

            populateClient()

            cmbClient.DisplayMember = "value"

            For Each _tmpBook In _bookings
                Dim _res As Boolean = False
                Dim _bookName = _tmpBook.Channel
                Dim resTarget
                For Each tmpTarg In _tmpBook.Targets
                    If tmpTarg = _tmpBook.Target Then
                        _res = True
                        Exit For
                    Else
                        _res = False
                        resTarget = _tmpBook.Target
                    End If
                Next
                If _res = False Then
                    _errors.AppendLine("The target '" & resTarget & "' was not found among the bookable targets for " + _bookName + ".")
                    _errors.AppendLine("Please specify what target to use.")
                    _errors.AppendLine("")
                End If
            Next
            If _errors.ToString <> "" Then
                Windows.Forms.MessageBox.Show(_errors.ToString, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Sub populateClient()
        'Create binding source.
        Dim _ClientBinding As New Windows.Forms.BindingSource
        ' Delegating selected agency clients. 
        _ClientBinding.DataSource = selectecAgency.listOfClients
        ' Set datasource to first iterator in list.
        cmbClient.DataSource = _ClientBinding
        '  Display only the value/name of in the client list.
    End Sub
    Private Sub cmbAgencies_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAgencies.SelectedIndexChanged
        If (selectecAgency IsNot Nothing) Then
            ' Fetch selected item as an integer.
            Dim tempSelectedAgencyIndex = cmbAgencies.SelectedIndex
            ' Select the correct client in the list based on integer.
            Dim tempSelectedAgency = llistOfAgencies(tempSelectedAgencyIndex)
            ' Update selected Agency
            selectecAgency = tempSelectedAgency
            populateClient()
        End If


    End Sub
    Private Sub cmdOk_Click(sender As System.Object, e As System.EventArgs) Handles cmdOk.Click
        Dim choosenOrg As String = ""
        Dim tempClientID As String = 0
        Dim tempClientName As String = ""

        If String.IsNullOrEmpty(_booking.AgencyBookingRefNo) AndAlso Windows.Forms.MessageBox.Show("You have not created a Marathon Order for this Booking type." & vbNewLine & vbNewLine & "Are you sure you want to proceed?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        If multipleOrganizations Then
            choosenOrg = getOrgID(cmbOrganizations.SelectedItem)
        End If
        If (Not cmbClient.SelectedIndex <> 0) Then
            Windows.Forms.MessageBox.Show("No client has been selected. Select a client for the booking before you can proceed.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Question)
        Else
            choosenOrg = getOrgID(cmbOrganizations.SelectedItem)
            Dim selectedItem = cmbClient.SelectedItem
            tempClientID = selectedItem.Key.ToString()
            tempClientName = selectedItem.value.ToString()
        End If
        For Each _book As TV4Online.SpotlightApiV23.xsd.Booking In _bookings
            Dim _tmpRo As Integer
            If _book.Selected = True Then
                'Empty targets because something wrong happens if a booking has an array which is not empty
                _book.Targets = Nothing
                If Not IsRBS(_book) Then
                    If _book.SpecificsSpots.Length < 1 Then
                        If Windows.Forms.MessageBox.Show("This Booking type " + _book.Channel + " " + " has no spots included in the booking" & vbNewLine & vbNewLine & "Are you sure you want to proceed?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                            For Each row In grdBookings.Rows
                                Dim TmpTag = row.Tag
                                If TmpTag Is _book Then
                                    row.DefaultCellStyle.BackColor = System.Drawing.Color.Red
                                End If
                            Next
                            Continue For
                        End If
                    Else
                        _book.SpecificsSpots = _book.SpecificsSpots.Where(Function(s) Not _skipSpecifics.Contains(s)).ToArray
                        If res.UploadBooking(_book, _tmpRo, multipleOrganizations, choosenOrg, tempClientID, tempClientName) Then
                            For Each row In grdBookings.Rows
                                Dim TmpTag = row.Tag
                                If TmpTag Is _book Then
                                    row.DefaultCellStyle.BackColor = System.Drawing.Color.Lime
                                End If
                            Next
                        Else
                            For Each row In grdBookings.Rows
                                Dim TmpTag = row.Tag
                                If TmpTag Is _book Then
                                    row.DefaultCellStyle.BackColor = System.Drawing.Color.Red
                                End If
                            Next
                        End If
                    End If
                Else
                    Dim _rbsPeriods As List(Of TV4Online.SpotlightApiV23.xsd.RbsPeriod) = _book.RbsPeriods.ToList
                    For Each _period In _book.RbsPeriods
                        For Each _dp In _period.RbsDayparts
                            _dp.RbsFilms = _dp.RbsFilms.Where(Function(f) Not _skipRBS.Contains(f)).ToArray
                        Next
                        _period.RbsDayparts = _period.RbsDayparts.Where(Function(d) d.RbsFilms.Count > 0).ToArray
                    Next
                    _book.RbsPeriods = _rbsPeriods.Where(Function(p) p.RbsDayparts.Count > 0).ToArray
                    If res.UploadBooking(_book, _tmpRo, multipleOrganizations, choosenOrg, tempClientID, tempClientName) = True Then
                        For Each row In grdBookings.Rows
                            Dim TmpTag = row.Tag
                            If TmpTag Is _book Then
                                row.DefaultCellStyle.BackColor = System.Drawing.Color.Lime
                            End If
                        Next
                    ElseIf _tmpRo > 0 Then
                        Exit Sub
                    Else
                        For Each row In grdBookings.Rows
                            Dim TmpTag = row.Tag
                            If TmpTag Is _book Then
                                row.DefaultCellStyle.BackColor = System.Drawing.Color.Red
                            End If
                        Next
                    End If
                End If
            End If
        Next
        grdBookings.Invalidate()
        Me.DialogResult = Windows.Forms.DialogResult.OK
        UpdateForm()
    End Sub
    Private Sub cmbBookingType_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cmbBookingType.SelectedValueChanged
        _booking.Type = cmbBookingType.SelectedItem
        grdBookings.Invalidate()
    End Sub
    Private Sub cmbTarget_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cmbTarget.SelectedValueChanged
        _booking.Target = cmbTarget.SelectedItem
        grdBookings.Invalidate()
    End Sub
    Private Sub cmdRefreshBudget_Click(sender As System.Object, e As System.EventArgs) Handles cmdRefreshBudget.Click
        txtBudget.Text = cmdRefreshBudget.Tag
        _booking.MaxBudget = txtBudget.Text
        grdBookings.Invalidate()
    End Sub
    Private Sub lnkSelectDeselect_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSelectDeselect.LinkClicked
        For Each book In _bookings
            If book.Selected = True Then
                book.Selected = False
            ElseIf book.Selected = False Then
                book.Selected = True
            End If
        Next
        grdBookings.Invalidate()
    End Sub
    Private Sub txtBudget_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtBudget.TextChanged
        If txtBudget.Text <> "" Then
            _booking.MaxBudget = txtBudget.Text
            grdBookings.Invalidate()
        End If
    End Sub
    Private Sub cmbTarget_TextUpdate(sender As System.Object, e As System.EventArgs) Handles cmbTarget.TextUpdate
        cmbTarget.Items.Clear()
        For Each tmpTarg In _booking.Targets
            cmbTarget.Items.Add(tmpTarg)
        Next
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblBookingUrlSpotlight.LinkClicked
        System.Diagnostics.Process.Start(lblBookingUrlSpotlight.Text)
    End Sub
End Class


