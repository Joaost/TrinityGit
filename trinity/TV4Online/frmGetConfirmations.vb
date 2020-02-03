Imports System.ComponentModel.Composition
Imports System.ComponentModel.Composition.Hosting
Imports System.Reflection
Imports TrinityPlugin
Imports System.ServiceModel
Imports System.Xml.Serialization
Imports System.Drawing
Imports System.Collections
Imports System.Diagnostics
Imports System.Windows.Forms
Imports clTrinity
Imports clTrinity.Trinity
Imports clTrinity.ScheduleTemplates

Public Class frmGetConfirmations

    Dim _bookings As New List(Of TV4Online.SpotlightApiV23.xsd.Booking)
    Dim _camp As Object
    Dim startUp As Boolean = True

    Private _client As New TV4Online.SpotlightApiV23.xsd.SpotlightApiV5Client(DirectCast(IIf(TV4OnlinePlugin.InternalApplication.GetSharedNetworkPreference("TV4Spotlight_beta", "Endpoint").StartsWith("https"), New WSHttpBinding(SecurityMode.Transport), New BasicHttpBinding), System.ServiceModel.Channels.Binding), New EndpointAddress(TV4OnlinePlugin.InternalApplication.GetSharedNetworkPreference("TV4Spotlight_beta", "Endpoint")))
    Dim res As New TV4OnlinePlugin

    Private _surcharges As New List(Of String)
    Private _logo As New List(Of Object)
    sub fillGrid()
        If grdConfBooking.RowCount > 1
            For each itm As TV4Online.SpotlightApiV23.xsd.Booking in _bookings
                Dim newRow As Integer = grdConfBooking.Rows.Add
                grdConfBooking.Rows(newRow).Tag = itm
            Next
        End If
    End sub
    Public sub New(Campaign As Object, availableChannels As String())

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.


        Dim _availableChannels = availableChannels
        _camp = Campaign

        For Each _chan In _camp.Channels
            If _availableChannels.Contains(_chan.ChannelName) Then
                For Each _bt In _chan.BookingTypes
                    Dim b As New TV4Online.SpotlightApiV23.xsd.Booking
                    If _bt.BookIt Then
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
                            b.BookingConfirmationVersion = _bt.BookingConfirmationVersion
                            b.CampaignRefNo = _bt.CampRefNo
                            b.AgencyBookingRefNo = _bt.orderNumber

                            b.Channel = _bt.ParentChannel.ChannelName
                            'b.AgencyBookingRefNo = _bt.OrderNumber
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

                            b.MaxBudget = Math.Round(_bt.PlannedNetBudget)

                            b.RbsPeriods = _periods.ToArray
                            b.Selected = True
                            b.Targets = _client.GetTargetsForChannel(b.Channel, b.StartDate)
                            Dim Tmptargets = _client.GetTargetsForChannel(b.Channel, b.StartDate)
                            _bookings.Add(b)
                        Else
                            For Each tmpSpot In _camp.BookedSpots
                                Dim _spots As New List(Of TV4Online.SpotlightApiV23.xsd.SpecificSpot)
                                For Each _spot In _camp.BookedSpots
                                    If _spot.BookingType Is _bt Then
                                        Dim _s As New TV4Online.SpotlightApiV23.xsd.SpecificSpot
                                        _s.BroadcastDate = _spot.AirDate
                                        _s.BroadcastTime = _spot.MaM
                                        _s.EstimatedTRP = _spot.ChannelEstimate
                                        If _spot.FilmCode <> "" Then
                                            _s.FilmCode = _spot.FilmCode
                                        Else
                                            _s.FilmCode = "No filmcode"
                                        End If
                                        _s.FilmIndex = _spot.Film.Index
                                        _s.FilmLength = _spot.Film.FilmLength
                                        _s.GrossPrice30 = _spot.GrossPrice30
                                        _s.NegotiatedDiscount = _bt.Buyingtarget.Discount
                                        _s.NetPrice = _spot.NetPrice * _spot.AddedValueIndex
                                        _s.ProgramName = _spot.Programme
                                        _s.Target = _bt.BuyingTarget.TargetName
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
                                            Dim _avName As String = _av.name
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
                                Next
                                b.SpecificsSpots = _spots.ToArray.OrderBy(Function(_s) _s.BroadcastTime).OrderBy(Function(_s) _s.BroadcastDate).ToArray
                            Next
                            If _bt.IsCompensation Then
                                b.Type = "Kompensation - Specific"
                            Else
                                b.Type = "Specifics"
                            End If
                            b.BookingIdSpotlight = _bt.BookingIdSpotlight
                            b.BookingUrlSpotlight = _bt.BookingUrlSpotlight
                            b.TrinityType = _bt.Name
                            b.Channel = _bt.ParentChannel.ChannelName
                            b.AgencyBookingRefNo = _bt.orderNumber
                            b.CampaignRefNo = _bt.CampRefNo
                            'b.AgencyBookingRefNo = _bt.OrderNumber
                            b.ChannelBookingRefNo = _bt.ContractNumber
                            b.Comments = _bt.Comments
                            b.StartDate = Date.FromOADate(_camp.StartDate)
                            b.EndDate = Date.FromOADate(_camp.EndDate)
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
        If res.Preferences.Username IsNot ""
            lblUserName.Text = res.Preferences.Username
            lblUserName.Visible = True

            Dim ra As Integer = Me.Width - lblUserName.Width
            lblUserName.Left = ra - 20

            Dim p As Integer = lblUserName.Left - 30
            Label1.Left = p
        End If
        checkConfirmationForBooking()
    End Sub
    Private Sub grdConfBooking_CellValueNeeded(sender As Object, e As Windows.Forms.DataGridViewCellValueEventArgs) Handles grdConfBooking.CellValueNeeded
        Dim x As TV4Online.SpotlightApiV23.xsd.Booking = _bookings(e.RowIndex)
        If x Is Nothing Then Exit Sub
        btnPreviewConf.Enabled = True
        Select Case e.ColumnIndex
            Case colSelected.Index
                e.Value = x.Selected
            Case colFromDate.Index
                e.Value = Format(x.StartDate, "yyyy-MM-dd")
            Case colToDate.Index
                e.Value = Format(x.EndDate, "yyyy-MM-dd")
            Case colName.Index
                e.Value = x.Channel + " " + x.TrinityType
            Case colAvailConf.Index
                If x.BookingAvailConfirmation = True
                    e.Value = "Available"
                Else
                    e.Value = "Not avaiable"
                End If
            Case colAgencyRef.Index
                e.Value = x.AgencyBookingRefNo
            Case colCampaignRefNo.Index
                e.Value = x.CampaignRefNo   
        End Select
    End Sub

    Private Sub grdConfBooking_CellValuePushed(sender As Object, e As Windows.Forms.DataGridViewCellValueEventArgs) Handles grdConfBooking.CellValuePushed
        Dim _period As TV4Online.SpotlightApiV23.xsd.Booking = _bookings(e.RowIndex)
        Select Case e.ColumnIndex
            Case colSelected.Index
                _period.Selected = e.Value
            Case colAgencyRef.Index
                _period.AgencyBookingRefNo = e.Value
                saveAgencyRefToCampaign(_period, e.Value)
        End Select
    End Sub
    Sub saveAgencyRefToCampaign(byval tmpBook As TV4Online.SpotlightApiV23.xsd.Booking, Byval refNo As String)
        'Method for saving agency ref number for booking to campaign. Kind of shady and should maybe not be used like this.
        For each tmpChan In _camp.Channels
            If tmpChan.ChannelName = tmpBook.Channel                
                For each tmpBt In tmpChan.BookingTypes
                    If tmpBt.Name = tmpBook.TrinityType
                        tmpBt.BookingAgencyRefNo= refNo
                        Exit Sub
                    End If
                Next
            End If
        Next

    End Sub

    Private Sub grdConfBooking_CellContentClick(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles grdConfBooking.CellContentClick
        If e.RowIndex < 0 Then Exit Sub        
        btnPreviewConf.Enabled = True
        Dim x As TV4Online.SpotlightApiV23.xsd.Booking = _bookings(e.RowIndex)
        Select Case e.ColumnIndex
            Case colAvailConf.Index
                If x.BookingAvailConfirmation
                    previewConfirmation(x)
                Else
                    Windows.Forms.MessageBox.Show(String.Format("TV4 Spotlight responded with one or more errors:" & vbNewLine & "No confirmations were found for " & x.Channel & " "  & x.Type & ".", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error))
                End If
        End Select

    End Sub
    Sub previewConfirmation(ByRef _booking As TV4Online.SpotlightApiV23.xsd.Booking)
        checkConfirmationForBooking(True, True, _booking)
    End Sub

    Sub updateGrid()
        'Method for filling confirmation booking grid.
        startUp = False
        If grdConfBooking.RowCount < 1
            grdConfBooking.Rows.Clear()
            For Each _bt As TV4Online.SpotlightApiV23.xsd.Booking In _bookings
                Dim newRow As Integer = grdConfBooking.Rows.Add
                grdConfBooking.Rows(newRow).Tag = _bt
            Next
        End If
    End Sub


    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) 
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Sub checkConfirmationForBooking(Optional ByVal preview As Boolean = False, Optional ByVal singleConf As Boolean = False, Optional ByVal book As TV4Online.SpotlightApiV23.xsd.Booking = Nothing)
        If singleConf
            If book.AgencyBookingRefNo IsNot ""
                    Dim tmpRes = res.checkConfirmationForBookingByRef(book, startUp)
                    If tmpRes IsNot nothing
                        If tmpRes.Item2 = True
                            book.BookingAvailConfirmation = True
                            If preview
                                If tmpRes.Item1.Confirmations.Length > 1                        
                                    Dim listOfConf As new List(Of TV4Online.SpotlightApiV23.xsd.Confirmation)
                                    For each item As TV4Online.SpotlightApiV23.xsd.Confirmation in tmpRes.Item1.Confirmations
                                        listOfConf.Add(item)
                                    Next
                                    Dim frmChoseConf As new frmChooseConfirmation(listOfConf)
                                    frmChoseConf.ShowDialog()
                                Else
                                    Dim frmConfPreview As new frmPreviewConfirmation(tmpRes.Item1.Confirmations(0))
                                    frmConfPreview.ShowDialog()
                                End If
                            End If
                        End If
                    End If
                Else
                    Dim tmpRes = res.checkConfirmationForBookingByGUID(book, startUp)
                    If tmpRes IsNot nothing
                        If tmpRes.Item2 = True
                            book.BookingAvailConfirmation = True
                            book.AgencyBookingRefNo = tmpRes.Item1.Confirmations(0).AgencyRefNo
                            
                            If preview
                                Dim confPrew As TV4Online.SpotlightApiV23.xsd.Booking = tmpRes.Item1
                            End If
                        End If
                    End If
            End If
        Else
            
        Dim succes = False
            For each tmpB As TV4Online.SpotlightApiV23.xsd.Booking in _bookings
                If tmpB.Selected
                    If tmpB.AgencyBookingRefNo IsNot ""
                        Dim tmpRes = res.checkConfirmationForBookingByRef(tmpB, startUp)
                        If tmpRes IsNot nothing
                            If tmpRes.Item2 = True
                                tmpB.BookingAvailConfirmation = True
                                If preview
                                    If tmpRes.Item1.Confirmations.Length > 1                        
                                        Dim listOfConf As new List(Of TV4Online.SpotlightApiV23.xsd.Confirmation)
                                        For each item As TV4Online.SpotlightApiV23.xsd.Confirmation in tmpRes.Item1.Confirmations
                                            listOfConf.Add(item)
                                        Next
                                        Dim frmChoseConf As new frmChooseConfirmation(listOfConf)
                                        frmChoseConf.ShowDialog()
                                    Else
                                        Dim frmConfPreview As new frmPreviewConfirmation(tmpRes.Item1.Confirmations(0))
                                        frmConfPreview.ShowDialog()
                                    End If
                                End If
                            End If
                        End If
                    Elseif tmpB.BookingIdSpotlight <> ""
                        Dim tmpRes = res.checkConfirmationForBookingByGUID(tmpB, startUp)
                        If tmpRes IsNot nothing
                            If tmpRes.Item2 = True
                                tmpB.BookingAvailConfirmation = True
                                tmpB.AgencyBookingRefNo = tmpRes.Item1.Confirmations(0).AgencyRefNo
                            
                                If preview
                                    If tmpRes.Item1.Confirmations.Length > 1                        
                                        Dim listOfConf As new List(Of TV4Online.SpotlightApiV23.xsd.Confirmation)
                                        For each item As TV4Online.SpotlightApiV23.xsd.Confirmation in tmpRes.Item1.Confirmations
                                            listOfConf.Add(item)
                                        Next
                                        Dim frmChoseConf As new frmChooseConfirmation(listOfConf)
                                        frmChoseConf.ShowDialog()
                                    Else
                                        Dim frmConfPreview As new frmPreviewConfirmation(tmpRes.Item1.Confirmations(0))
                                        frmConfPreview.ShowDialog()
                                    End If
                                End If
                            End If
                        End If
                    Else
                        If startUp = False
                                Windows.Forms.MessageBox.Show(String.Format("Trinity responded with one or more errors:" & vbNewLine & "No confirmations were found for " & tmpB.Channel & " " & tmpB.Type & ". No GUID or Agency ref number were connected to the booking.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error))
                        End If
                    End If
                End If
            Next
        End If
        updateGrid()
    End Sub

    Private Sub cmdApply_Click(sender As Object, e As EventArgs) Handles cmdApply.Click
        For each _book In _bookings
            If _book.Selected
                If _book.BookingAvailConfirmation
                    Dim confSender As New TV4OnlinePlugin
                    confSender.ConfirmImport(_book)
                End If
            End If
        Next
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub btnPreviewConf_Click(sender As Object, e As EventArgs) Handles btnPreviewConf.Click
        
        Dim row As TV4Online.SpotlightApiV23.xsd.Booking
        For each tmpRow In grdConfBooking.Rows
            If tmpRow.Selected = True
                row = tmpRow.Tag
                Exit For
            End If
        Next
        

        previewConfirmation(row)

        Me.DialogResult = DialogResult.None
    End Sub

    Private Sub grdConfBooking_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles grdConfBooking.CellFormatting
        Dim row As TV4Online.SpotlightApiV23.xsd.Booking = _bookings.Item(e.RowIndex)
        Select Case e.ColumnIndex
            Case colAvailConf.Index
            If row.BookingAvailConfirmation = True
                e.Cellstyle.BackColor = System.Drawing.Color.Green
            Else
                e.CellStyle.BackColor = System.Drawing.Color.Red                
            End If
        End Select
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles cmdCheckConf.Click        
        checkConfirmationForBooking()
        grdConfBooking.Refresh()
    End Sub

    Private Sub lnkSelectDeselect_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkSelectDeselect.LinkClicked
        For Each book In _bookings
            If book.Selected = True Then
                book.Selected = False
            ElseIf book.Selected = False Then
                book.Selected = True
            End If
        Next
        grdConfBooking.Invalidate()
    End Sub

    Private Sub ToolStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub
End Class