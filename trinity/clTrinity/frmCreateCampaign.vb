Imports System.Globalization.CultureInfo

Public Class frmCreateCampaign
    Private _Adedge As New ConnectWrapper.Brands
    Private _campaign As Trinity.cKampanj
    Private _TrinitySettings As Trinity.cSettings
    Private _area As String = Campaign.Area

    Private Sub cmdCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreate.Click

        If Not lbxFilmcodes.SelectedItems.Count > 0 Then
            MsgBox("You need to select at least one filmcode", MsgBoxStyle.Information, "Information required")
            Exit Sub
        End If
        If Not lbxChannels.SelectedItems.Count > 0 Then
            MsgBox("You need to select at least one channel", MsgBoxStyle.Information, "Information required")
            Exit Sub
        End If

        Dim strChannels As String = ""
        For Each s As String In lbxChannels.SelectedItems
            strChannels = strChannels & "," & s
        Next

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        'clear old results
        _Adedge.clearList()

        'set channels
        _Adedge.setChannelsArea(strChannels, _area)

        Dim strFilmcodes As String = ""
        For Each s As String In lbxFilmcodes.SelectedItems
            strFilmcodes = strFilmcodes & "," & s
        Next

        'set the filmcodes
        _Adedge.setBrandFilmCode(cmdCountry.Tag, strFilmcodes)

        'only want commercial, Sponsor and promo spots 
        _Adedge.setBrandType("COMMERCIAL,SPONSOR,PROMO")

        Dim count As Integer

        _Adedge.setSplitOff()

        count = _Adedge.Run()

        If count = 0 Then
            MsgBox("No spots where found in current conditions", MsgBoxStyle.Information, "ERROR")
            Exit Sub
        End If

        LongName.Clear()
        For i As Integer = 1 To _campaign.Channels.Count
            LongName.Add(Campaign.Channels(i).Shortname, Campaign.Channels(i).ChannelName)
        Next

        For Each chan As Trinity.cChannel In _campaign.Channels
            For Each BT As Trinity.cBookingType In chan.BookingTypes
                If Not LongBT.ContainsKey(BT.Shortname) Then
                    LongBT.Add(BT.Shortname, BT.Name)
                End If
            Next
        Next

        Trinity.Helper.WriteToLogFile("Read Pricelists")
        For Each TmpChan As Trinity.cChannel In _campaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                TmpBT.ReadPricelist()
            Next
        Next

        'get the different film lengts
        Dim hashFilms As New Hashtable
        For Each s As String In lbxFilmcodes.SelectedItems
            For c As Integer = 0 To count - 1
                If s = _Adedge.getAttrib(Connect.eAttribs.aBrandFilmCode, c) Then
                    hashFilms.Add(s, _Adedge.getAttrib(Connect.eAttribs.aDuration, c).ToString)
                    Exit For
                End If
            Next
        Next

        'lists to keep track of the TRPs for each channel, daypart and film per week
        Dim listWeekFilms As New List(Of Hashtable)
        Dim listWeekChannels As New List(Of Hashtable)
        Dim listDaypartsChannel As New List(Of Hashtable)
        For i As Integer = 0 To _campaign.Dayparts.Count - 1
            listDaypartsChannel.Add(New Hashtable)
        Next
        'keeps track if the week numbers in the Trinity collection
        Dim hashWeeks As New Hashtable

        'set weeks
        Dim dateTmp As Date = dtStart.Value.Date
        Dim week As Trinity.cWeek
        Dim film As Trinity.cFilm
        Dim strWeek As String
        While dateTmp <= dtEnd.Value.Date
            strWeek = CurrentCulture.DateTimeFormat.Calendar.GetWeekOfYear(dateTmp, CurrentCulture.DateTimeFormat.CalendarWeekRule, CurrentCulture.DateTimeFormat.FirstDayOfWeek)

            'create a new hashtable in the weekly lists
            listWeekFilms.Add(New Hashtable)
            listWeekChannels.Add(New Hashtable)
            hashWeeks.Add(strWeek, hashWeeks.Count + 1)

            For Each ch As Trinity.cChannel In _campaign.Channels
                For Each bt As Trinity.cBookingType In ch.BookingTypes
                    week = bt.Weeks.Add(strWeek)
                    week.StartDate = dateTmp.AddDays(-dateTmp.DayOfWeek + 1).ToOADate
                    week.EndDate = dateTmp.AddDays(7 - dateTmp.DayOfWeek).ToOADate
                    For Each s As String In lbxFilmcodes.SelectedItems
                        film = week.Films.Add(s)
                        film.Filmcode = s
                        film.Bookingtype = bt
                        film.FilmLength = hashFilms(s)
                        film.Index = bt.FilmIndex(hashFilms(s))
                    Next
                Next
            Next

            dateTmp = dateTmp.AddDays(7)
        End While

        'last we make sure the start and end date is correct (dont have to be a whole week)
        For Each ch As Trinity.cChannel In _campaign.Channels
            For Each bt As Trinity.cBookingType In ch.BookingTypes
                bt.Weeks(1).StartDate = dtStart.Value.Date.ToOADate
                bt.Weeks(CInt(bt.Weeks.Count)).EndDate = dtEnd.Value.Date.ToOADate
            Next
        Next



        Dim _spot As Trinity.cPlannedSpot

        'keeps track if the differences between channel names in Trinity and in AdvantEdge
        Dim hashChannelNames As New Hashtable

        For c As Integer = 0 To count - 1

            _spot = Campaign.PlannedSpots.Add()

            If hashChannelNames(_Adedge.getAttrib(Connect.eAttribs.aChannel, c)) = "" Then
                For Each ch As Trinity.cChannel In _campaign.Channels
                    If ch.AdEdgeNames.ToUpper = _Adedge.getAttrib(Connect.eAttribs.aChannel, c).ToString.ToUpper Then

                        'the advantEdge name and real channel name is not the same
                        hashChannelNames.Add(_Adedge.getAttrib(Connect.eAttribs.aChannel, c), ch.ChannelName)

                        'This makes it appear booked
                        _campaign.Channels(hashChannelNames(_Adedge.getAttrib(Connect.eAttribs.aChannel, c))).BookingTypes("RBS").BookIt = True

                        Exit For
                    End If
                Next
            End If

            _spot.AirDate = _Adedge.getAttrib(Connect.eAttribs.aDate, c)

            _spot.Filmcode = _Adedge.getAttrib(Connect.eAttribs.aBrandFilmCode, c)

            _spot.Advertiser = _Adedge.getAttrib(Connect.eAttribs.aBrandAdvertiser, c)

            _spot.Channel = _campaign.Channels(hashChannelNames(_Adedge.getAttrib(Connect.eAttribs.aChannel, c)))

            _spot.Bookingtype = _campaign.Channels(hashChannelNames(_Adedge.getAttrib(Connect.eAttribs.aChannel, c))).BookingTypes("RBS")

            _spot.Programme = _Adedge.getAttrib(Connect.eAttribs.aBrandProgAfter, c)

            _spot.Week = _spot.Bookingtype.GetWeek(Date.FromOADate(_spot.AirDate))

            _spot.Week.TRPControl = True

            _spot.MaM = _Adedge.getAttrib(Connect.eAttribs.aFromTime, c) \ 60

            _spot.ChannelRating = Format(_Adedge.getUnit(Connect.eUnits.uTRP, c, 0, Campaign.TimeShift, Trinity.Helper.TargetIndex(_Adedge, "3+")), "#0.0")

            _spot.Week.TRP = _spot.Week.TRP + _spot.ChannelRating

            'save the TRPs to the daypart split and the week filmsplit

            strWeek = CurrentCulture.DateTimeFormat.Calendar.GetWeekOfYear(Date.FromOADate(_spot.AirDate), CurrentCulture.DateTimeFormat.CalendarWeekRule, CurrentCulture.DateTimeFormat.FirstDayOfWeek)

            'if the week/film is empty we add a new, else we increase
            If listWeekFilms(hashWeeks(strWeek) - 1)(_spot.Filmcode) Is Nothing Then
                listWeekFilms(hashWeeks(strWeek) - 1)(_spot.Filmcode) = _spot.ChannelRating
            Else
                listWeekFilms(hashWeeks(strWeek) - 1)(_spot.Filmcode) = listWeekFilms(hashWeeks(strWeek) - 1)(_spot.Filmcode) + _spot.ChannelRating
            End If

            'if the week/channel is empty we add it, else we increase it
            If listWeekChannels(hashWeeks(strWeek) - 1)(_spot.Channel.ChannelName) Is Nothing Then
                listWeekChannels(hashWeeks(strWeek) - 1).Add(_spot.Channel.ChannelName, _spot.ChannelRating)
            Else
                listWeekChannels(hashWeeks(strWeek) - 1)(_spot.Channel.ChannelName) = listWeekChannels(hashWeeks(strWeek) - 1)(_spot.Channel.ChannelName) + _spot.ChannelRating
            End If


            'check if the daypart/channel exists, if it does we increase it otherwize we create it
            If listDaypartsChannel(_spot.Bookingtype.Dayparts.GetDaypartIndexForMam(_spot.MaM))(_spot.Channel.ChannelName) Is Nothing Then
                listDaypartsChannel(_spot.Bookingtype.Dayparts.GetDaypartIndexForMam(_spot.MaM)).Add(_spot.Channel.ChannelName, _spot.ChannelRating)
            Else
                listDaypartsChannel(_spot.Bookingtype.Dayparts.GetDaypartIndexForMam(_spot.MaM))(_spot.Channel.ChannelName) = listDaypartsChannel(_spot.Bookingtype.Dayparts.GetDaypartIndexForMam(_spot.MaM))(_spot.Channel.ChannelName) + _spot.ChannelRating
            End If

        Next


        For Each ch As Trinity.cChannel In _campaign.Channels
            For Each bt As Trinity.cBookingType In ch.BookingTypes
                If bt.BookIt Then

                    Dim dblSumTRP As Double = 0
                    For i As Integer = 0 To bt.Dayparts.Count - 1
                        dblSumTRP += listDaypartsChannel(i)(ch.ChannelName)
                    Next

                    'set the dayparts split for att targets
                    'For Each target As Trinity.cPricelistTarget In bt.Pricelist.Targets
                    '    For i As Integer = 0 To bt.Dayparts.Count - 1
                    '        target.DefaultDaypart(i) = listDaypartsChannel(i)(ch.ChannelName) / dblSumTRP * 100
                    '    Next
                    'Next

                    For i As Integer = 0 To bt.Dayparts.Count - 1
                        bt.DefaultDaypart(i) = listDaypartsChannel(i)(ch.ChannelName) / dblSumTRP * 100
                    Next

                    'set the TRPs per week and the film split
                    For Each week In bt.Weeks
                        'Dim dblSumTRP As Double = 0
                        If hashWeeks(week.Name) < listWeekFilms.Count + 1 Then

                            dblSumTRP = 0
                            For Each s As String In lbxFilmcodes.SelectedItems
                                dblSumTRP += listWeekFilms(hashWeeks(week.Name) - 1)(s)
                            Next

                            week.TRPControl = True
                            week.TRP = listWeekChannels(hashWeeks(week.Name) - 1)(ch.ChannelName)


                            'set the film split
                            For Each film In week.Films
                                If listWeekFilms(hashWeeks(week.Name) - 1)(film.Filmcode) Is Nothing Then
                                    film.Share(Trinity.cFilm.FilmShareEnum.fseTRP) = 0
                                Else
                                    film.Share(Trinity.cFilm.FilmShareEnum.fseTRP) = listWeekFilms(hashWeeks(week.Name) - 1)(film.Filmcode) / dblSumTRP * 100
                                End If
                            Next
                        End If
                    Next
                End If
            Next

        Next

        Campaign = _campaign
        TrinitySettings = _TrinitySettings

        Campaign.GetNewActualSpots(True)
        Campaign.CreateAdedgeSpots()
        Campaign.CalculateSpots(UseFilters:=False)


        Me.Cursor = Windows.Forms.Cursors.Default
        Me.Close()

    End Sub

    Private Sub cmdCountry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCountry.Click
        'this procedure/button selects what country settings is supposed to use

        Dim TmpMnu As System.Windows.Forms.ToolStripMenuItem
        Saved = False

        'clears the availaable countrys
        mnuArea.Items.Clear()
        mnuArea.ShowCheckMargin = True
        mnuArea.ImageList = frmMain.ilsSmall

        'adds the available countrys in the list
        For i As Integer = 1 To _TrinitySettings.AreaCount
            TmpMnu = mnuArea.Items.Add(TrinitySettings.AreaName(i), Nothing, New EventHandler(AddressOf mnuAreaItem_Click))
            TmpMnu.Checked = (_campaign.Area = _TrinitySettings.Area(i))
            TmpMnu.ImageKey = _TrinitySettings.Area(i)
            TmpMnu.Name = _TrinitySettings.Area(i)
            TmpMnu.Tag = _TrinitySettings.AreaLog(i)
        Next
        mnuArea.Show(cmdCountry, New System.Drawing.Point(0, cmdCountry.Height))
    End Sub


    Private Sub mnuAreaItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        cmdCountry.Image = frmMain.ilsBig.Images(sender.Name)
        _Adedge.setArea(sender.name)
        _area = sender.name
        cmdCountry.Tag = sender.tag
    End Sub

    Private Sub dtEnd_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtEnd.ValueChanged
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        _Adedge.setPeriod(Format(dtStart.Value, "ddMMyy") & "-" & Format(dtEnd.Value, "ddMMyy"))

        'get all the advertisers and put them into the combobox
        For c As Integer = 0 To _Adedge.lookupNoAdvertisers(cmdCountry.Tag) - 1
            cmbAdvertisers.Items.Add(_Adedge.lookupAdvertiser(cmdCountry.Tag, c))
        Next

        'get all the products and put them into the combobox
        For c As Integer = 0 To _Adedge.lookupNoProducts(cmdCountry.Tag) - 1
            cmbProducts.Items.Add(_Adedge.lookupProduct(cmdCountry.Tag, c))
        Next

        'enable the next group
        grpAd.Enabled = True

        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmbAdvertisers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAdvertisers.SelectedIndexChanged
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        grpChannel.Enabled = True

        lblSelected.Tag = cmbAdvertisers.SelectedItem
        lblSelected.Text = cmbAdvertisers.SelectedItem & " is currently selected"

        _Adedge.setBrandAdvertiser(cmdCountry.Tag, cmbAdvertisers.SelectedItem)

        Dim strChannels As String = ""
        Dim strTargets As String = ""

        For Each ch As Trinity.cChannel In _campaign.Channels
            strChannels = strChannels & "," & ch.AdEdgeNames
            For Each bt As Trinity.cBookingType In ch.BookingTypes
                For Each target As Trinity.cPricelistTarget In bt.Pricelist.Targets
                    strTargets = strTargets & "," & target.TargetName
                Next
            Next
        Next

        _Adedge.setChannelsArea(strChannels, _area)

        _Adedge.setTargetMnemonic(strTargets, True)

        Dim i As Integer

        i = _Adedge.Run(False, False, 0, True)

        Dim list As New List(Of String)
        lbxFilmcodes.Items.Clear()
        'get all the filmcodes and put them into the listbox
        For c As Integer = 0 To i - 1
            If Not list.Contains(_Adedge.getAttrib(Connect.eAttribs.aBrandFilmCode, c)) Then
                list.Add(_Adedge.getAttrib(Connect.eAttribs.aBrandFilmCode, c))
                lbxFilmcodes.Items.Add(_Adedge.getAttrib(Connect.eAttribs.aBrandFilmCode, c))
            End If
        Next

        i = _Adedge.setSplitVar("channel")

        'get all the films and put them into the listbox
        lbxChannels.Items.Clear()
        For c As Integer = 0 To i - 1
            lbxChannels.Items.Add(_Adedge.getSplitName(c, 0))
        Next

        _Adedge.setSplitOff()

        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmbProducts_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProducts.SelectedIndexChanged
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        grpChannel.Enabled = True
        lbxFilmcodes.Items.Clear()
        lbxChannels.Items.Clear()

        lblSelected.Tag = cmbProducts.SelectedItem
        lblSelected.Text = cmbProducts.SelectedItem & " is currently selected"

        _Adedge.lookupFilmcode(cmdCountry.Tag, cmbProducts.SelectedItem, "")

        _Adedge.setBrandProduct(cmdCountry.Tag, cmbProducts.SelectedItem)

        Dim strChannels As String = ""
        Dim strTargets As String = ""

        For Each ch As Trinity.cChannel In _campaign.Channels
            strChannels = strChannels & "," & ch.AdEdgeNames
            For Each bt As Trinity.cBookingType In ch.BookingTypes
                For Each target As Trinity.cPricelistTarget In bt.Pricelist.Targets
                    strTargets = strTargets & "," & target.TargetName
                Next
            Next
        Next

        _Adedge.setChannelsArea(strChannels, _area)

        _Adedge.setTargetMnemonic(strTargets, True)

        Dim i As Integer

        i = _Adedge.Run(False, False, 0, True)

        Dim list As New List(Of String)

        'get all the filmcodes and put them into the listbox
        For c As Integer = 0 To i - 1
            If Not list.Contains(_Adedge.getAttrib(Connect.eAttribs.aBrandFilmCode, c)) Then
                list.Add(_Adedge.getAttrib(Connect.eAttribs.aBrandFilmCode, c))
                lbxFilmcodes.Items.Add(_Adedge.getAttrib(Connect.eAttribs.aBrandFilmCode, c))
            End If
        Next

        i = _Adedge.setSplitVar("channel")

        'get all the films and put them into the listbox
        For c As Integer = 0 To i - 1
            lbxChannels.Items.Add(_Adedge.getSplitName(c, 0))
        Next

        _Adedge.setSplitOff()


        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        grpChannel.Enabled = True
        lblSelected.Text = " is currently selected"
    End Sub

    Private Sub frmCreateCampaign_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _Adedge.setArea("SE")
    End Sub

    Private Sub dtStart_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtStart.ValueChanged
        If dtStart.Value = Nothing Then Exit Sub
        If dtEnd.Value = Nothing Then Exit Sub

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        _Adedge.setPeriod(Format(dtStart.Value, "ddMMyy") & "-" & Format(dtEnd.Value, "ddMMyy"))

        'get all the advertisers and put them into the combobox
        For c As Integer = 0 To _Adedge.lookupNoAdvertisers(cmdCountry.Tag) - 1
            cmbAdvertisers.Items.Add(_Adedge.lookupAdvertiser(cmdCountry.Tag, c))
        Next

        'get all the products and put them into the combobox
        For c As Integer = 0 To _Adedge.lookupNoProducts(cmdCountry.Tag) - 1
            cmbProducts.Items.Add(_Adedge.lookupProduct(cmdCountry.Tag, c))
        Next

        'enable the next group
        grpAd.Enabled = True

        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub lbxFilmcodes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbxFilmcodes.SelectedIndexChanged
        If lbxFilmcodes.SelectedItems.Count > 0 AndAlso lbxChannels.SelectedItems.Count > 0 Then
            cmdCreate.Enabled = True
        End If
    End Sub

    Private Sub lbxChannels_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbxChannels.SelectedIndexChanged
        If lbxFilmcodes.SelectedItems.Count > 0 AndAlso lbxChannels.SelectedItems.Count > 0 Then
            cmdCreate.Enabled = True
        End If
    End Sub

    Private Class TRPItems
        Implements IEnumerable

        Private mCol As New Collection

        Public Function Add(ByVal name As String, ByVal DPcount As Integer, ByVal filmcount As Integer) As TRPItem
            Dim item As New TRPItem(DPcount, filmcount)
            item.ChannelName = name
            mCol.Add(item, name)

            Return item
        End Function

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As TRPItem
            Get
                Item = mCol(vntIndexKey)
            End Get
        End Property

        Public Function Count() As Integer
            Count = mCol.Count
        End Function

        Public Sub Clear()
            mCol.Clear()
        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator
        End Function
    End Class

    Private Class TRPItem
        Private listTRP As New List(Of List(Of Single))
        Private strName As String
        Private listFilm As New List(Of List(Of Single))

        Public Sub New(ByVal DPcount As Integer, ByVal filmcount As Integer)
            For i As Integer = 1 To DPcount
                Dim l As New List(Of Single)
                listTRP.Add(l)
            Next

            For i As Integer = 1 To filmcount
                Dim l As New List(Of Single)
                listFilm.Add(l)
            Next
        End Sub

        Public Property TRPFilm(ByVal Film As Integer, ByVal week As Integer) As Single
            Get
                Return listFilm(Film)(week)
            End Get
            Set(ByVal value As Single)
                listFilm(Film)(week) = value
            End Set
        End Property

        Public Property TRP(ByVal DP As Integer, ByVal week As Integer) As Single
            Get
                Return listTRP(DP)(week)
            End Get
            Set(ByVal value As Single)
                listTRP(DP)(week) = value
            End Set
        End Property

        Public Property ChannelName() As String
            Get
                Return strName
            End Get
            Set(ByVal value As String)
                strName = value
            End Set
        End Property
    End Class

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _campaign = New Trinity.cKampanj(TrinitySettings.ErrorChecking)
        _TrinitySettings = New Trinity.cSettings(TrinitySettings.LocalDataPath)

        _TrinitySettings.MainObject = _campaign
        Trinity.Helper.MainObject = _campaign

        _campaign.Area = _TrinitySettings.DefaultArea
        _campaign.AreaLog = _TrinitySettings.DefaultAreaLog

        _campaign.CreateChannels()

        For Each ch As Trinity.cChannel In _campaign.Channels
            For Each bt As Trinity.cBookingType In ch.BookingTypes
                bt.ReadPricelist()
            Next
        Next

        _campaign.AllAdults = _TrinitySettings.AllAdults
        _campaign.MainTarget.TargetName = _campaign.AllAdults
        _campaign.SecondaryTarget.TargetName = _campaign.AllAdults
    End Sub
End Class

