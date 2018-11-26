Imports System.ServiceModel

Public Class frmConfirm

    Dim _bt As Object
    Dim _camp As Object

    Dim _booking As New TV4Online.SpotlightApiV23.xsd.Booking
    'Private _client As New TV4Online.SpotlightApiV23.xsd.SpotlightApiV3Client(DirectCast(IIf(TV4OnlinePlugin.InternalApplication.GetSharedNetworkPreference("TV4Spotlight", "Endpoint").StartsWith("https"), New WSHttpBinding(SecurityMode.Transport), New BasicHttpBinding), System.ServiceModel.Channels.Binding), New EndpointAddress(TV4OnlinePlugin.InternalApplication.GetSharedNetworkPreference("TV4Spotlight", "Endpoint")))
    
    Private _client As New TV4Online.SpotlightApiV23.xsd.SpotlightApiV3Client(DirectCast(IIf(TV4OnlinePlugin.InternalApplication.GetSharedNetworkPreference("TV4Spotlight", "Endpoint").StartsWith("https"), New WSHttpBinding(SecurityMode.Transport), New BasicHttpBinding), System.ServiceModel.Channels.Binding), New EndpointAddress(TV4OnlinePlugin.InternalApplication.GetSharedNetworkPreference("TV4Spotlight", "Endpoint")))

    Shared _addedValueToSurcharge As Dictionary(Of String, String)
    Shared _indexToIndex As Dictionary(Of String, String)

    Private _surcharges As New List(Of String)
    Private _tv4Indices As New List(Of String)

    Private _skipSpecifics As New List(Of TV4Online.SpotlightApiV23.xsd.SpecificSpot)
    Private _skipRBS As New List(Of TV4Online.SpotlightApiV23.xsd.RbsFilm)

    Private ReadOnly Property AddedValueToSurcharge As Dictionary(Of String, String)
        Get
            If _addedValueToSurcharge Is Nothing Then
                _addedValueToSurcharge = New Dictionary(Of String, String)
            End If
            Return _addedValueToSurcharge
        End Get
    End Property

    Private ReadOnly Property IndexToIndex As Dictionary(Of String, String)
        Get
            If _indexToIndex Is Nothing Then
                _indexToIndex = New Dictionary(Of String, String)
            End If
            Return _indexToIndex
        End Get
    End Property

    Public Sub New(Bookingtype As Object, Campaign As Object)

        ' This call is required by the designer.
        InitializeComponent()

        _surcharges = _client.GetSurchargeNames.ToList
        _tv4Indices = _client.GetIndexNames.ToList

        ' Add any initialization after the InitializeComponent() call.
        _bt = Bookingtype
        _camp = Campaign

        Me.Text &= _bt.ToString
        lblBookingType.Text = _bt.ToString

        txtMaxBudget.Text = Math.Round(_bt.PlannedNetBudget)

        _booking.Channel = _bt.ParentChannel.ChannelName
        _booking.Type = _bt.Name
        _booking.AgencyBookingRefNo = _bt.OrderNumber
        _booking.ChannelBookingRefNo = _bt.ContractNumber
        _booking.Comments = _bt.Comments
        _booking.StartDate = Date.FromOADate(_camp.StartDate)
        _booking.EndDate = Date.FromOADate(_camp.EndDate)
        _booking.MaxBudget = _bt.PlannedNetBudget
        _booking.UniverseSize = _bt.BuyingTarget.getUniSizeNat(_camp.StartDate)
        _booking.Target = _bt.BuyingTarget.TargetName.ToString.Trim.Replace(" ", "")
        If IsNumeric(_booking.Target.Substring(0, 1)) Then
            _booking.Target = "A" & _booking.Target
        End If
        imgLogo.Image = _bt.ParentChannel.GetImage()

        If _bt.IsRBS Then
            lnkRBS.Enabled = True
            chkRBS.Checked = True
            _booking.Type = "RBS"
            Dim _periods As New List(Of TV4Online.SpotlightApiV23.xsd.RbsPeriod)
            For Each _week In _bt.Weeks
                Dim _period As New TV4Online.SpotlightApiV23.xsd.RbsPeriod
                _period.StartDate = Date.FromOADate(_week.StartDate)
                _period.EndDate = Date.FromOADate(_week.EndDate)
                Dim _dayparts As New List(Of TV4Online.SpotlightApiV23.xsd.RbsDaypart)
                For Each _dp In _bt.Dayparts
                    Dim _dayp As New TV4Online.SpotlightApiV23.xsd.RbsDaypart
                    _dayp.NegotiatedDiscount = Math.Round(_bt.BuyingTarget.Discount, 2)
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
            Next
            _booking.RbsPeriods = _periods.ToArray
        Else
            chkRBS.Enabled = False
            lnkRBS.Enabled = False
        End If
        If _bt.IsSpecific Then
            lnkSpecifics.Enabled = True
            chkSpecifics.Checked = True
            _booking.Type = "Specifics"
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
                        _s.FilmCode = _spot.Film.Name
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
                        Dim _avName As String = _av.name
                        Dim _tmpSC = _surcharges.FirstOrDefault(Function(s) s.ToLower = _avName.ToLower.Replace(" ", ""))
                        If String.IsNullOrEmpty(_tmpSC) Then
                            If TV4OnlinePlugin.InternalApplication.GetUserPreference("TV4Spotlight", _av.Name) <> "" Then
                                _sc.Name = TV4OnlinePlugin.InternalApplication.GetUserPreference("TV4Spotlight", _av.Name)
                            Else
                                _sc.Name = _av.Name
                                imgSpecWarning.Visible = True
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
            _booking.SpecificsSpots = _spots.ToArray.OrderBy(Function(_s) _s.BroadcastTime).OrderBy(Function(_s) _s.BroadcastDate).ToArray
        Else
            chkSpecifics.Enabled = False
            lnkSpecifics.Enabled = False
        End If
    End Sub

    'Private Function Mam2Tid(ByVal MaM As Integer) As String
    '    Dim h As Integer
    '    Dim m As Integer
    '    Dim Tmpstr As String

    '    h = MaM \ 60
    '    m = MaM Mod 60

    '    Tmpstr = Trim(h) & ":" & Format(m, "00")
    '    While Len(Tmpstr) < 5
    '        Tmpstr = "0" & Tmpstr
    '    End While
    '    Mam2Tid = Tmpstr
    'End Function

    Public ReadOnly Property Booking As TV4Online.SpotlightApiV23.xsd.Booking
        Get
            Return _booking
        End Get
    End Property

    Private Sub frmConfirm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        For Each _targ As String In _client.GetTargetsForChannel(_bt.ParentChannel.ChannelName, _camp.StartDate)
            Dim _idx = cmbTarget.Items.Add(_targ)
            If _targ = _booking.Target Then
                cmbTarget.SelectedIndex = _idx
            End If
        Next
        For Each _bt As String In _client.GetBookingTypes
            Dim _idx = cmbBookingType.Items.Add(_bt)
            If _bt = _booking.Type Then
                cmbBookingType.SelectedIndex = _idx
            End If
        Next
        Dim _errors As New Text.StringBuilder
        If cmbTarget.SelectedIndex = -1 Then
            _errors.AppendLine("")
            _errors.AppendLine("The target '" & _booking.Target & "' was not found among the bookable targets.")
            _errors.AppendLine("Please specify what target to use.")
        End If
        If cmbBookingType.SelectedIndex = -1 Then
            _errors.AppendLine("")
            _errors.AppendLine("The bookingtype '" & _booking.Type & "' was not found among the available bookingtypes.")
            _errors.AppendLine("Please specify what bookingtype to use.")
        End If
        If _errors.ToString <> "" Then
            Windows.Forms.MessageBox.Show(_errors.ToString, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)

        End If
    End Sub

    Private Sub cmdOk_Click(sender As System.Object, e As System.EventArgs) Handles cmdOk.Click

        If String.IsNullOrEmpty(_booking.AgencyBookingRefNo) AndAlso Windows.Forms.MessageBox.Show("You have not created a Marathon Order for this Booking type." & vbNewLine & vbNewLine & "Are you sure you want to proceed?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        _booking.MaxBudget = txtMaxBudget.Text
        _booking.Target = cmbTarget.SelectedItem
        _booking.Type = cmbBookingType.SelectedItem

        If Not chkRBS.Checked Then
            _booking.RbsPeriods = New TV4Online.SpotlightApiV23.xsd.RbsPeriod() {}
        Else
            Dim _rbsPeriods As List(Of TV4Online.SpotlightApiV23.xsd.RbsPeriod) = _booking.RbsPeriods.ToList
            For Each _period In _rbsPeriods
                For Each _dp In _period.RbsDayparts
                    _dp.RbsFilms = _dp.RbsFilms.Where(Function(f) Not _skipRBS.Contains(f)).ToArray
                Next
                _period.RbsDayparts = _period.RbsDayparts.Where(Function(d) d.RbsFilms.Count > 0).ToArray
            Next
            _booking.RbsPeriods = _rbsPeriods.Where(Function(p) p.RbsDayparts.Count > 0).ToArray


        End If
        If Not chkSpecifics.Checked Then
            _booking.SpecificsSpots = New TV4Online.SpotlightApiV23.xsd.SpecificSpot() {}
        Else
            _booking.SpecificsSpots = _booking.SpecificsSpots.Where(Function(s) Not _skipSpecifics.Contains(s)).ToArray
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cmdCancel_Click(sender As System.Object, e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub lnkSpecifics_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkSpecifics.LinkClicked
        Dim _frm As New frmSpecifics(_booking.SpecificsSpots, _skipSpecifics)
        _frm.ShowDialog()
        _skipSpecifics = _frm.SkipList
        imgSpecWarning.Visible = False
        For Each _row As System.Windows.Forms.DataGridViewRow In _frm.grdSpecifics.Rows
            If _row.Cells(_frm.colSurcharges.Index).ErrorText <> "" Then
                imgSpecWarning.Visible = True
            End If
        Next
        cmdRefresh.Visible = (_frm.grdSpecifics.GetColumnSum(_frm.colNetPrice.Index) <> txtMaxBudget.Text)
        cmdRefresh.Tag = _frm.grdSpecifics.GetColumnSum(_frm.colNetPrice.Index)
        If _skipSpecifics.Count > 0 Then
            chkSpecifics.CheckState = Windows.Forms.CheckState.Indeterminate
        End If
    End Sub

    Private Sub lnkRBS_Click(sender As Object, e As System.EventArgs) Handles lnkRBS.Click
        'Dim _frm As New frmRBS(_booking.RbsPeriods, _skipRBS)
        '_frm.ShowDialog()
        '_skipRBS = _frm.SkipList

        'cmdRefresh.Visible = (_frm.grdRBS.GetColumnSum(_frm.colNetPrice.Index) <> txtMaxBudget.Text)
        'cmdRefresh.Tag = _frm.grdRBS.GetColumnSum(_frm.colNetPrice.Index)

        'If _skipSpecifics.Count > 0 Then
        '    chkSpecifics.CheckState = Windows.Forms.CheckState.Indeterminate
        'End If
    End Sub

    Private Sub cmdSkip_Click(sender As System.Object, e As System.EventArgs) Handles cmdSkip.Click
        Me.DialogResult = Windows.Forms.DialogResult.Ignore
        Me.Close()
    End Sub

    Private Sub cmdRefresh_Click(sender As System.Object, e As System.EventArgs) Handles cmdRefresh.Click
        txtMaxBudget.Text = cmdRefresh.Tag
    End Sub

    Private Sub chkSpecifics_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkSpecifics.CheckStateChanged
        If chkSpecifics.CheckState = Windows.Forms.CheckState.Checked Then
            _skipSpecifics = New List(Of TV4Online.SpotlightApiV23.xsd.SpecificSpot)
        End If
    End Sub

    Private Sub chkRBS_CheckStateChanged(sender As Object, e As System.EventArgs) Handles chkRBS.CheckStateChanged
        If chkRBS.CheckState = Windows.Forms.CheckState.Checked Then
            _skipRBS = New List(Of TV4Online.SpotlightApiV23.xsd.RbsFilm)
        End If
    End Sub
End Class