Imports System.Windows.Forms
Imports System.Xml.Linq

Public Class frmAddChannelWizard

    Dim _campaign As Trinity.cKampanj
    Dim _savedChannelSets As XDocument

    Private Sub frmAddChannelWizard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim _chanList As List(Of Trinity.cChannel) = (From _chan As Trinity.cChannel In _campaign.Channels Where (From _bt As Trinity.cBookingType In _chan.BookingTypes From _targ As Trinity.cPricelistTarget In _bt.Pricelist.Targets From _pp As Trinity.cPricelistPeriod In _targ.PricelistPeriods Where _pp.FromDate.ToOADate <= _campaign.EndDate OrElse _pp.ToDate.ToOADate >= _campaign.StartDate).Count > 0 Select _chan).ToList
        Dim _channelGroups = (From _chan As Trinity.cChannel In _chanList Select _cg = _chan.ChannelGroup Where _cg <> "").Distinct

        For Each _cg As String In _channelGroups
            Dim _chanGroup As String = _cg
            With tvwAvailable.Nodes.Add(_cg, _cg)
                .ForeColor = Color.Blue
                Dim _bookingTypes = (From _chan As Trinity.cChannel In _chanList Where _chan.ChannelGroup = _chanGroup From _bt As Trinity.cBookingType In _chan.BookingTypes Where (From _targ As Trinity.cPricelistTarget In _bt.Pricelist.Targets From _pp As Trinity.cPricelistPeriod In _targ.PricelistPeriods Where _pp.FromDate.ToOADate <= _campaign.EndDate OrElse _pp.ToDate.ToOADate >= _campaign.StartDate).Count > 0 Select _bt.Name).Distinct
                For Each _bt As String In _bookingTypes
                    .Nodes.Add(_cg & " " & _bt, _bt).ForeColor = Color.Blue
                Next
            End With
        Next
        For Each _chan As Trinity.cChannel In _chanList
            For Each _bt As Trinity.cBookingType In From _bookingtype As Trinity.cBookingType In _chan.BookingTypes Where (From _targ As Trinity.cPricelistTarget In _bookingtype.Pricelist.Targets From _pp As Trinity.cPricelistPeriod In _targ.PricelistPeriods Where _pp.FromDate.ToOADate <= _campaign.EndDate OrElse _pp.ToDate.ToOADate >= _campaign.StartDate).Count > 0 Select _bookingtype
                Dim _nodes As Windows.Forms.TreeNodeCollection
                If tvwAvailable.Nodes.ContainsKey(_chan.ChannelGroup) Then
                    _nodes = tvwAvailable.Nodes(_chan.ChannelGroup).Nodes(_chan.ChannelGroup & " " & _bt.Name).Nodes
                Else
                    If Not tvwAvailable.Nodes.ContainsKey(_chan.ChannelName) Then
                        tvwAvailable.Nodes.Add(_chan.ChannelName, _chan.ChannelName).ForeColor = Color.Blue
                    End If
                    _nodes = tvwAvailable.Nodes(_chan.ChannelName).Nodes
                End If
                With _nodes.Add(_bt.ToString, _bt.ToString)
                    .Tag = _bt                   
                End With
            Next
        Next

        cmdOpen.Enabled = False
        If My.Computer.FileSystem.FileExists(IO.Path.Combine(Trinity.Helper.GetSpecialFolder(Trinity.Helper.CSIDLEnum.UserProfile), "Trinity 4.0\ChannelSets.xml")) Then
            _savedChannelSets = XDocument.Load(IO.Path.Combine(Trinity.Helper.GetSpecialFolder(Trinity.Helper.CSIDLEnum.UserProfile), "Trinity 4.0\ChannelSets.xml"))
            If _savedChannelSets.<ChannelSets>...<ChannelSet>.Count > 0 Then cmdOpen.Enabled = True
        End If

    End Sub


    Public Sub New(Campaign As Trinity.cKampanj)

        ' This call is required by the designer.
        InitializeComponent()
        _campaign = Campaign
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdAddChannel_Click(sender As System.Object, e As System.EventArgs) Handles cmdAddChannel.Click
        Dim _node As TreeNode = tvwAvailable.SelectedNode
        If _node Is Nothing Then Exit Sub

        If _node.Tag IsNot Nothing Then
            If Not lstChosen.Items.Contains(_node.Tag) Then lstChosen.Items.Add(_node.Tag)
        ElseIf _node.Nodes.Count > 0 AndAlso _node.Nodes(0).Tag IsNot Nothing Then
            For Each _child As TreeNode In _node.Nodes
                If Not lstChosen.Items.Contains(_child.Tag) Then lstChosen.Items.Add(_child.Tag)
            Next
        End If
    End Sub

    Private Sub cmdRemoveChannel_Click(sender As System.Object, e As System.EventArgs) Handles cmdRemoveChannel.Click, lstChosen.DoubleClick
        While lstChosen.SelectedItems.Count > 0
            lstChosen.Items.Remove(lstChosen.SelectedItems(0))
        End While
    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        cmdAdd.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        For Each _bt As Trinity.cBookingType In lstChosen.Items
            _bt.BookIt = True
            Dim _affinities As New Dictionary(Of String, Single)
            Dim _notFound As Boolean = True
            For Each _targ As Trinity.cPricelistTarget In _bt.Pricelist.Targets
                If optBestFit.Checked Then
                    If _targ.Target.TargetName = _campaign.MainTarget.TargetName Then
                        _bt.BuyingTarget = _targ
                        _notFound = False
                        Exit For
                    End If
                Else

                End If
            Next
            If optBestFit.Checked AndAlso _notFound Then
                For Each _targ As Trinity.cPricelistTarget In _bt.Pricelist.Targets
                    _affinities.Add(_targ.TargetName, GetAffinity(_targ, _bt.ParentChannel.AdEdgeNames))
                Next
                Dim _best As KeyValuePair(Of String, Single) = _affinities.OrderBy(Function(_kv) _kv.Value)(0)
                _bt.BuyingTarget = _bt.Pricelist.Targets(_best.Key)
            End If
        Next
        Me.Cursor = Cursors.Default
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Dispose()
    End Sub

    Dim _adedge As ConnectWrapper.Brands
    Function GetAffinity(Target As Trinity.cPricelistTarget, Channel As String) As Single
        If _adedge Is Nothing Then
            _adedge = New ConnectWrapper.Brands
            _adedge.setPeriod("-3d")
            _adedge.setArea(_campaign.Area)
        End If
        _adedge.clearTargetSelection()
        Trinity.Helper.AddTarget(_adedge, _campaign.MainTarget, False)
        Trinity.Helper.AddTarget(_adedge, Target.Target, False)
        _adedge.setChannels(Channel)
        _adedge.Run(False)
        Dim _aff As Single
        If _adedge.getSumU(eUnits.uBrandTrp30, eSumModes.smAll, 0, 0, 1) > 0 Then
            _aff = _adedge.getSumU(eUnits.uBrandTrp30, eSumModes.smAll) / _adedge.getSumU(eUnits.uBrandTrp30, eSumModes.smAll, 0, 0, 1)
        Else
            _aff = 1
        End If
        Return Math.Abs((_aff - 1) * 100)
    End Function

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Dim _name As String = InputBox("Name of channel set: (must be unique)")
        If _name = "" Then Exit Sub
        If _savedChannelSets Is Nothing Then
            _savedChannelSets = New XDocument()
            _savedChannelSets.Add(<ChannelSets></ChannelSets>)
        End If
        If (From _set As XElement In _savedChannelSets.<ChannelSets>...<ChannelSet> Where _set.@Name = _name).Count > 0 Then
            Windows.Forms.MessageBox.Show("A channel set with that name already exists.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        _savedChannelSets.<ChannelSets>(0).Add(<ChannelSet Name=<%= _name %>>
                                                   <%= From _bt As Trinity.cBookingType In lstChosen.Items Select <Channel Name=<%= _bt.ParentChannel.ChannelName %> BookingType=<%= _bt.Name %>></Channel> %>
                                               </ChannelSet>)
        _savedChannelSets.Save(IO.Path.Combine(Trinity.Helper.GetSpecialFolder(Trinity.Helper.CSIDLEnum.UserProfile), "Trinity 4.0\ChannelSets.xml"))
        cmdOpen.Enabled = True
    End Sub

    Private Sub cmdOpen_Click(sender As System.Object, e As System.EventArgs) Handles cmdOpen.Click
        Dim _mnu As New ContextMenu()
        For Each _set As XElement In _savedChannelSets.<ChannelSets>...<ChannelSet>
            _mnu.MenuItems.Add(_set.@Name, AddressOf OpenSavedSet).Tag = _set
        Next
        _mnu.Show(cmdOpen, New Point(0, cmdOpen.Height))
    End Sub

    Sub OpenSavedSet(sender As Object, e As EventArgs)
        lstChosen.Items.Clear()
        For Each _node As XElement In DirectCast(sender.tag, XElement).Nodes
            Dim _chan As String = _node.@Name
            Dim _bt As String = _node.@BookingType
            If _campaign.Channels(_chan) IsNot Nothing AndAlso _campaign.Channels(_chan).BookingTypes(_bt) IsNot Nothing Then
                lstChosen.Items.Add(_campaign.Channels(_chan).BookingTypes(_bt))
            End If
        Next
    End Sub

End Class