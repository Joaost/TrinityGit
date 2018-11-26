Public Class frmSpots

    Dim SkipIt As Boolean = False
    Dim filteredSpotsConfirmed As Collection
    Dim filteredSpotsActual As Collection
    Dim Updating As Boolean = False

    Dim styleNoMatch As Windows.Forms.DataGridViewCellStyle
    Dim styleMatchGoodRating As Windows.Forms.DataGridViewCellStyle
    Dim styleMatchBadRating As Windows.Forms.DataGridViewCellStyle
    Dim styleMatchOnRating As Windows.Forms.DataGridViewCellStyle

    Dim dtConfirmed As DataTable
    Dim dtActual As DataTable

    Dim strSortConfirmed As String = "Date ASC, Time ASC"
    Dim strSortActual As String = "Date ASC, Time ASC"

    Dim filmcodeFilters As Boolean = True


    Function FilterIn(ByVal Spot As Trinity.cPlannedSpot) As Boolean
        Dim WeekDays() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}
        If Not GeneralFilter.Data("Channels", Spot.Channel.ChannelName) Then
            Return False
        End If
        If Not GeneralFilter.Data("Bookingtype", Spot.Bookingtype.Name) Then
            Return False
        End If
        If Not GeneralFilter.Data("Weekday", WeekDays(Weekday(Date.FromOADate(Spot.AirDate), FirstDayOfWeek.Monday) - 1)) Then
            Return False
        End If
        If Not GeneralFilter.Data("Daypart", Campaign.Dayparts.GetDaypartForMam(Spot.MaM).Name) Then
            Return False
        End If
        If filmcodeFilters Then
            If Spot.Week Is Nothing OrElse Spot.Week.Films(Spot.Filmcode) Is Nothing OrElse Not GeneralFilter.Data("Film", Spot.Week.Films(Spot.Filmcode).Name) Then
                Return False
            End If
        End If
        If filmcodeFilters Then
            If Not GeneralFilter.Data("Week", Spot.Week.Name) Then
                Return False
            End If
        End If
        Return True
    End Function

    Function FilterIn(ByVal Spot As Trinity.cActualSpot) As Boolean
        Dim WeekDays() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}
        If Not GeneralFilter.Data("Channels", Spot.Channel.ChannelName) Then
            Return False
        End If
        If Not GeneralFilter.Data("Bookingtype", Spot.Bookingtype.Name) Then
            Return False
        End If
        If Not GeneralFilter.Data("Weekday", WeekDays(Weekday(Date.FromOADate(Spot.AirDate), FirstDayOfWeek.Monday) - 1)) Then
            Return False
        End If
        If Not GeneralFilter.Data("Daypart", Campaign.Dayparts.GetDaypartForMam(Spot.MaM).Name) Then
            Return False
        End If
        If Spot.Week Is Nothing OrElse Spot.Week.Films(Spot.Filmcode) Is Nothing OrElse Not GeneralFilter.Data("Film", Spot.Week.Films(Spot.Filmcode).Name) Then
            Return False
        End If
        If Not GeneralFilter.Data("Week", Spot.Week.Name) Then
            Return False
        End If
        If Not GeneralFilter.Data("PIB", "First") AndAlso Spot.PosInBreak <= TrinitySettings.PPFirst Then
            Return False
        End If
        If Not GeneralFilter.Data("PIB", "Last") AndAlso Spot.PosInBreak - TrinitySettings.PPFirst + 1 >= Spot.SpotsInBreak Then
            Return False
        End If
        If Not GeneralFilter.Data("PIB", "Middle") AndAlso Spot.PosInBreak > TrinitySettings.PPFirst AndAlso Spot.PosInBreak - TrinitySettings.PPFirst + 1 < Spot.SpotsInBreak Then
            Return False
        End If

        Return True
    End Function

    Sub UpdateConfirmed(ByVal addColumns As Boolean, ByVal setDatasource As Boolean)
        If addColumns Then
            grdConfirmed.Columns.Clear()
            grdConfirmed.Rows.Clear()
            For c As Integer = 1 To TrinitySettings.ConfirmedColumnCount
                grdConfirmed.Columns.Add("colConfirmed" & TrinitySettings.ConfirmedColumn(c), TrinitySettings.ConfirmedColumn(c))
                grdConfirmed.Columns("colConfirmed" & TrinitySettings.ConfirmedColumn(c)).Tag = TrinitySettings.ConfirmedColumn(c)
                If TrinitySettings.ConfirmedColumnWidth(c) > 0 Then
                    grdConfirmed.Columns("colConfirmed" & TrinitySettings.ConfirmedColumn(c)).Width = TrinitySettings.ConfirmedColumnWidth(c)
                ElseIf TrinitySettings.ConfirmedColumnWidth(c) = 0 Then
                    grdConfirmed.Columns("colConfirmed" & TrinitySettings.ConfirmedColumn(c)).Visible = False
                Else
                    grdConfirmed.Columns("colConfirmed" & TrinitySettings.ConfirmedColumn(c)).Width = 100
                End If
            Next

        Else
            grdConfirmed.Rows.Clear()
        End If

        If setDatasource Then
            dtConfirmed = Campaign.PlannedSpots.getDataTable(Campaign.MainTarget.TargetNameNice)
            dtConfirmed.DefaultView.Sort = strSortConfirmed
        End If

        If grdConfirmed.Columns.Count > 0 Then
            Dim i As Integer
            Dim y As Integer = 0
            For y = 0 To dtConfirmed.Rows.Count - 1
                i = grdConfirmed.Rows.Add()
                grdConfirmed.Rows(i).Tag = dtConfirmed.DefaultView.Item(y)
            Next


            grdConfirmed.Rows.Add()
        End If


        'Removed if statement when causing filting not to work /JK, BF

        'If setDatasource Then
        '    runFiltersOnConfirmed()
        'End If

        runFiltersOnConfirmed()
    End Sub

    Sub UpdateActual(ByVal addColumns As Boolean, ByVal setDatasource As Boolean)

        'Dim antalspottar As Integer = Campaign.ActualSpots.Count()

        'If antalspottar > 0 Then
        '    For i As Integer = 0 To Campaign.ActualSpots.Count
        '        Campaign.ActualSpots.Remove(i)
        '    Next
        'End If

        For Each tmptarget As Trinity.cTarget In Campaign.TargetCollection
            Dim targetname = tmptarget
        Next

        If addColumns Then
            grdActual.Columns.Clear()
            grdActual.Rows.Clear()
            For c As Integer = 1 To TrinitySettings.ActualColumnCount
                grdActual.Columns.Add("colActual" & TrinitySettings.ActualColumn(c), TrinitySettings.ActualColumn(c))
                grdActual.Columns("colActual" & TrinitySettings.ActualColumn(c)).Tag = TrinitySettings.ActualColumn(c)
                If TrinitySettings.ActualColumn(c) = "Actual Buying" Then
                    grdActual.Columns("colActual" & TrinitySettings.ActualColumn(c)).HeaderCell.ToolTipText = "This is the achieved TRP for this spot in it's booking type's buying target and nothing else"
                End If
                If TrinitySettings.ActualColumnWidth(c) > 0 Then
                    grdActual.Columns("colActual" & TrinitySettings.ActualColumn(c)).Width = TrinitySettings.ActualColumnWidth(c)
                ElseIf TrinitySettings.ActualColumnWidth(c) = 0 Then
                    grdActual.Columns("colActual" & TrinitySettings.ActualColumn(c)).Visible = False
                Else
                    grdActual.Columns("colActual" & TrinitySettings.ActualColumn(c)).Width = 100
                End If
            Next

        Else
            grdActual.Rows.Clear()
        End If

        If setDatasource Then
            dtActual = Campaign.ActualSpots.getDataTable(Campaign.MainTarget.TargetNameNice)
            dtActual.DefaultView.Sort = strSortActual
        End If

        If grdActual.Columns.Count > 0 Then
            Dim i As Integer
            Dim y As Integer = 0
            For y = 0 To dtActual.Rows.Count - 1
                i = grdActual.Rows.Add()
                grdActual.Rows(i).Tag = dtActual.DefaultView.Item(y)
            Next
            If grdActual.ColumnCount > 0 Then grdActual.Rows.Add()
        End If


        If setDatasource Then
            runFiltersOnActual()
        End If
    End Sub

    Private Sub frmSpots_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'make sure the form is updates if it has been unactive
        checkFilmCodes()
    End Sub


    Private Sub cmbTarget_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTarget.SelectedIndexChanged
        ActiveTarget = cmbTarget.SelectedIndex
        Campaign.CalculateSpots()

        grdConfirmed.Invalidate()
        grdActual.Invalidate()
    End Sub

    Private Sub cmdAutoMatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAutoMatch.Click
        Saved = False
        'the sub matched planned spot with actual spots.
        Dim TmpPlanned As Trinity.cPlannedSpot
        Dim TmpActual As Trinity.cActualSpot

        'for each planned spot we go through the actual spots and try to find a actual 
        'spot with the same times and channel.
        For Each TmpPlanned In Campaign.PlannedSpots
            If TmpPlanned.MatchedSpot Is Nothing Then
                For Each TmpActual In Campaign.ActualSpots
                    If TmpActual.MatchedSpot Is Nothing Then
                        If TmpPlanned.MaM - 15 <= TmpActual.MaM Then
                            If TmpPlanned.MaM + 15 >= TmpActual.MaM Then
                                If TmpPlanned.AirDate = TmpActual.AirDate Then
                                    If TmpPlanned.Channel Is TmpActual.Channel Then
                                        'If TmpPlanned.Filmcode = TmpActual.Filmcode Then
                                        TmpPlanned.MatchedSpot = TmpActual
                                        TmpActual.MatchedSpot = TmpPlanned
                                        TmpActual.Bookingtype = TmpPlanned.Bookingtype
                                        'End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        Next
        'update the lists
        UpdateConfirmed(False, True)
        UpdateActual(False, True)
    End Sub

    Private Sub cmdConfirmedDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConfirmedDelete.Click
        Saved = False
        Dim TmpRow As Windows.Forms.DataGridViewRow

        If grdConfirmed.SelectedRows.Count = 0 Then
            Windows.Forms.MessageBox.Show("No spot selected.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If grdConfirmed.SelectedRows.Count = 1 And grdConfirmed.SelectedRows(0).Tag Is Nothing Then
            Windows.Forms.MessageBox.Show("Cannot delete the summary row.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        For Each TmpRow In grdConfirmed.SelectedRows
            If TmpRow.Tag IsNot Nothing Then
                If TmpRow.Visible Then
                    Campaign.PlannedSpots.Remove(TmpRow.Tag.Item("ID"))
                    grdConfirmed.Rows.Remove(TmpRow)
                    dtConfirmed.Rows.Remove(dtConfirmed.Select("ID='" & TmpRow.Tag.Item("ID") & "'")(0))
                End If
            End If
        Next
    End Sub

    Private Sub runFiltersOnConfirmed()

        lblConfirmedFiltered.Tag = 0
        grdConfirmed.SuspendLayout()
        For Each row As Windows.Forms.DataGridViewRow In grdConfirmed.Rows
            If row.Tag IsNot Nothing Then
                If FilterIn(Campaign.PlannedSpots(row.Tag.Item("ID"))) Then
                    row.Visible = True
                Else
                    row.Visible = False
                    lblConfirmedFiltered.Tag += 1
                End If
            End If
        Next
        grdConfirmed.ResumeLayout()
        lblConfirmedFiltered.Text = "Filtered out: " & lblConfirmedFiltered.Tag
        If lblConfirmedFiltered.Tag = 0 Then
            lblConfirmedFiltered.Visible = False
        Else
            lblConfirmedFiltered.Visible = True
        End If
    End Sub

    Private Sub runFiltersOnActual()
        lblActualFiltered.Tag = 0
        grdActual.SuspendLayout()
        For Each row As Windows.Forms.DataGridViewRow In grdActual.Rows
            If row.Tag IsNot Nothing Then
                If FilterIn(Campaign.ActualSpots(row.Tag.Item("ID"))) Then
                    row.Visible = True
                Else
                    row.Visible = False
                    lblActualFiltered.Tag += 1
                End If
            End If
        Next
        grdActual.ResumeLayout()
        lblActualFiltered.Text = "Filtered out: " & lblActualFiltered.Tag
        If lblActualFiltered.Tag = 0 Then
            lblActualFiltered.Visible = False
        Else
            lblActualFiltered.Visible = True
        End If
    End Sub

    Sub ChangeFilter(ByVal sender As Object, ByVal e As EventArgs)
        Dim tag As String = ""
        If sender.text = "Invert selection" Then
            For Each TmpItem As Windows.Forms.ToolStripItem In DirectCast(sender.OwnerItem, Windows.Forms.ToolStripMenuItem).DropDownItems
                If Not TmpItem Is sender AndAlso Not TmpItem.GetType.Name = "ToolStripSeparator" Then
                    GeneralFilter.Data(TmpItem.Tag, TmpItem.Text) = Not GeneralFilter.Data(TmpItem.Tag, TmpItem.Text)
                End If
            Next
            If Not Updating Then
                tag = sender.tag
                Updating = True
                Campaign.CalculateSpots(True, , , , True)
                Updating = False
                runFiltersOnConfirmed()
                runFiltersOnActual()
                If frmMain.pnlInfo.Visible Then
                    frmMain.grdReach.Invalidate()
                Else
                    If frmInfo.Visible Then
                        frmInfo.grdReach.Invalidate()
                    End If
                End If
            End If
        Else
            GeneralFilter.Data(sender.tag, sender.text) = Not GeneralFilter.Data(sender.tag, sender.text)
            If Not Updating Then
                tag = sender.tag
                Updating = True
                Campaign.CalculateSpots(True, , , , True)
                Updating = False
                runFiltersOnConfirmed()
                runFiltersOnActual()
                If frmMain.pnlInfo.Visible Then
                    frmMain.grdReach.Invalidate()
                Else
                    If frmInfo.Visible Then
                        frmInfo.grdReach.Invalidate()
                    End If
                End If
            End If
        End If
        cmdConfirmedFilter_DropDownOpening(sender, e, tag)
    End Sub

    Private Sub cmdConfirmedFilter_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs, Optional ByVal dropDownMenuParent As String = "") Handles cmdConfirmedFilter.DropDownOpening
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpFilm As Trinity.cFilm
        Dim TmpWeek As Trinity.cWeek
        Dim WeekDays() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}

        Dim IsUsed As Boolean
        Dim i As Integer

        cmdConfirmedFilter.DropDownItems.Clear()
        
        Dim mnuFilter As New Windows.Forms.ContextMenuStrip

        Dim mnuChannel As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Channels")
        mnuChannel.Tag = "Channels"
        
        For Each TmpChan In Campaign.Channels
            IsUsed = False
            For Each TmpBT In TmpChan.BookingTypes
                If TmpBT.BookIt Then
                    IsUsed = True
                    Exit For
                End If
            Next
            If IsUsed Then
                Dim tmpSubMenu As New Windows.Forms.ToolStripMenuItem
                tmpSubMenu.Checked = GeneralFilter.Data("Channels", TmpChan.ChannelName)
                tmpSubMenu.Text = TmpChan.ChannelName
                tmpSubMenu.Tag = "Channels"
                AddHandler tmpSubMenu.Click, AddressOf ChangeFilter
                mnuChannel.DropDownItems.Add(tmpSubMenu)
            End If
        Next
        mnuChannel.DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
        Dim invertItemChannels As New Windows.Forms.ToolStripMenuItem
        invertItemChannels.Tag = "Channels"
        invertItemChannels.Text = "Invert selection"
        invertItemChannels.Checked = False
        AddHandler invertItemChannels.Click, AddressOf ChangeFilter
        mnuChannel.DropDownItems.Add(invertItemChannels)

        Dim mnuBookingtype As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Bookingtypes")
        mnuBookingtype.Tag = "Bookingtypes"
        Dim l As New List(Of String)
        For Each ch As Trinity.cChannel In Campaign.Channels
            For Each TmpBT In ch.BookingTypes
                If Not l.Contains(TmpBT.Name) Then
                    Dim tmpSubMenuBT As New Windows.Forms.ToolStripMenuItem
                    l.Add(TmpBT.Name)
                    tmpSubMenuBT.Tag = "Bookingtype"
                    tmpSubMenuBT.Text = TmpBT.Name
                    tmpSubMenuBT.Checked = GeneralFilter.Data("Bookingtype", TmpBT.Name)
                    AddHandler tmpSubMenuBT.Click, AddressOf ChangeFilter
                    mnuBookingtype.DropDownItems.Add(tmpSubMenuBT)
                End If
            Next
        Next

        mnuBookingtype.DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
        Dim invertItemBT As New Windows.Forms.ToolStripMenuItem
        invertItemBT.Tag = "Bookingtype"
        invertItemBT.Text = "Invert selection"
        invertItemBT.Checked = False
        AddHandler invertItemBT.Click, AddressOf ChangeFilter
        mnuBookingtype.DropDownItems.Add(invertItemBT)
        
        Dim mnuFilm As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Films")
        For Each TmpFilm In Campaign.Channels(1).BookingTypes(1).Weeks(1).Films
            Dim tmpSubMenuFilm As new Windows.Forms.ToolStripMenuItem
            tmpSubMenuFilm.Tag = "Film"
            tmpSubMenuFilm.Text = TmpFilm.Name
            tmpSubMenuFilm.Checked = GeneralFilter.Data("Film", TmpFilm.Name)
            AddHandler tmpSubMenuFilm.Click, AddressOf ChangeFilter
            mnuFilm.DropDownItems.Add(tmpSubMenuFilm)
        Next
        mnuFilm.DropDownItems.Add(New Windows.Forms.ToolStripSeparator)

        Dim invertItemFilm As New Windows.Forms.ToolStripMenuItem
        invertItemFilm.Tag = "Film"
        invertItemFilm.Text = "Invert selection"
        invertItemFilm.Checked = False
        AddHandler invertItemFilm.Click, AddressOf ChangeFilter
        mnuFilm.DropDownItems.Add(invertItemFilm)
        
        Dim mnuDayparts As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Dayparts")
        For i = 0 To Campaign.Dayparts.Count - 1
            Dim tmpSubMenuDayparts As New Windows.Forms.ToolStripMenuItem
            tmpSubMenuDayparts.Tag = "Daypart"
            tmpSubMenuDayparts.Checked = GeneralFilter.data("Daypart", Campaign.Dayparts(i).Name)
            tmpSubMenuDayparts.Text = Campaign.Dayparts(i).Name
            AddHandler tmpSubMenuDayparts.Click, AddressOf ChangeFilter
            mnuDayparts.DropDownItems.Add(tmpSubMenuDayparts)
        Next
        mnuDayparts.DropDownItems.Add(New Windows.Forms.ToolStripSeparator)

        Dim invertItemDaypart As New Windows.Forms.ToolStripMenuItem
        invertItemDaypart.Tag = "Daypart"
        invertItemDaypart.Text = "Invert selection"
        invertItemDaypart.Checked = False
        AddHandler invertItemDaypart.Click, AddressOf ChangeFilter
        mnuDayparts.DropDownItems.Add(invertItemDaypart)        

        'With DirectCast(cmdConfirmedFilter.DropDownItems.Add("Weeks"), Windows.Forms.ToolStripMenuItem)
        '    For Each TmpWeek In Campaign.Channels(1).BookingTypes(1).Weeks
        '        With DirectCast(.DropDownItems.Add(TmpWeek.Name), Windows.Forms.ToolStripMenuItem)
        '            .Tag = "Week"
        '            .Checked = GeneralFilter.Data("Week", TmpWeek.Name)
        '            AddHandler .Click, AddressOf ChangeFilter
        '        End With
        '    Next
        '    .DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
        '    With DirectCast(.DropDownItems.Add("Invert selection"), Windows.Forms.ToolStripMenuItem)
        '        .Tag = "Week"
        '        .Checked = False
        '        AddHandler .Click, AddressOf ChangeFilter
        '    End With
        'End With
        
        Dim mnuWeek As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Weeks")
        For each tmpWeek In Campaign.Channels(1).BookingTypes(1).Weeks
            Dim tmpSubMenuWeek As New Windows.Forms.ToolStripmenuitem
            tmpSubMenuWeek.Tag = "Week"
            tmpSubMenuWeek.Checked = GeneralFilter.Data("Week", TmpWeek.Name)
            tmpSubMenuWeek.Text = TmpWeek.Name
            AddHandler tmpSubMenuWeek.Click, AddressOf ChangeFilter
            mnuWeek.DropDownItems.Add(tmpSubMenuWeek)
        Next
        mnuWeek.DropDownItems.Add(New Windows.Forms.ToolStripSeparator)

        Dim invertItemWeek As New Windows.Forms.ToolStripMenuItem
        invertItemWeek.Tag = "Week"
        invertItemWeek.Text = "Invert selection"
        invertItemWeek.Checked = False
        AddHandler invertItemWeek.Click, AddressOf ChangeFilter
        mnuWeek.DropDownItems.Add(invertItemWeek)


        Dim mnuWeekdays As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Weekdays")
        For i = 0 To 6            
            Dim tmpSubMenuWeekdays As new Windows.Forms.ToolStripMenuItem
            tmpSubMenuWeekdays.Tag = "Weekday"
            tmpSubMenuWeekdays.Text = WeekDays(i)
            tmpSubMenuWeekdays.Checked = GeneralFilter.Data("Weekday", WeekDays(i))
            AddHandler tmpSubMenuWeekdays.Click, AddressOf ChangeFilter
            mnuWeekdays.DropDownItems.Add(tmpSubMenuWeekdays)
        Next

        mnuWeekdays.DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
        Dim invertItemWeekdays As New Windows.Forms.ToolStripMenuItem
        invertItemWeekdays.Tag = "Weekday"
        invertItemWeekdays.Text = "Invert selection"
        invertItemWeekdays.Checked = False
        AddHandler invertItemWeekdays.Click, AddressOf ChangeFilter
        mnuWeekdays.DropDownItems.Add(invertItemWeekdays)
        
        Dim mnuPIB As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Position in break")

        Dim tmpSubMenuPIBFirst As New Windows.Forms.ToolStripMenuItem
        tmpSubMenuPIBFirst.Tag ="PIB"
        tmpSubMenuPIBFirst.Text = "First"
        tmpSubMenuPIBFirst.Checked = GeneralFilter.Data("PIB", "First")
        AddHandler tmpSubMenuPIBFirst.Click, AddressOf ChangeFilter
        mnuPIB.DropDownItems.Add(tmpSubMenuPIBFirst)
        
        Dim tmpSubMenuPIBMiddle As New Windows.Forms.ToolStripMenuItem
        tmpSubMenuPIBMiddle.Tag ="PIB"
        tmpSubMenuPIBMiddle.Text = "Middle"
        tmpSubMenuPIBMiddle.Checked = GeneralFilter.Data("PIB", "Middle")
        AddHandler tmpSubMenuPIBMiddle.Click, AddressOf ChangeFilter
        mnuPIB.DropDownItems.Add(tmpSubMenuPIBMiddle)
        
        Dim tmpSubMenuPIBLast As New Windows.Forms.ToolStripMenuItem
        tmpSubMenuPIBLast.Tag ="PIB"
        tmpSubMenuPIBLast.Text = "Last"
        tmpSubMenuPIBLast.Checked = GeneralFilter.Data("PIB", "Last")
        AddHandler tmpSubMenuPIBLast.Click, AddressOf ChangeFilter
        mnuPIB.DropDownItems.Add(tmpSubMenuPIBLast)

        Dim invertItemPIB As New Windows.Forms.ToolStripMenuItem
        invertItemPIB.Tag ="PIB"
        invertItemPIB.Text = "Invert selection"
        invertItemPIB.Checked = False
        AddHandler invertItemPIB.Click, AddressOf ChangeFilter
        mnuPIB.DropDownItems.Add(invertItemPIB)
        
        mnuFilter.Show(ToolStrip1, cmdConfirmedFilter.Bounds.Left, cmdConfirmedFilter.Bounds.Height)
        If dropDownMenuParent = "Channels"
            mnuChannel.DropDown.Show()
        ElseIf dropDownMenuParent = "Bookingtype"
            mnuBookingtype.DropDown.Show()
        ElseIf dropDownMenuParent = "Film"
            mnuFilm.DropDown.Show()
        ElseIf dropDownMenuParent = "Week"
            mnuWeek.DropDown.Show()
        ElseIf dropDownMenuParent = "Weekday"
            mnuWeekdays.DropDown.Show()
        ElseIf dropDownMenuParent = "Daypart"
            mnuWeekdays.DropDown.Show()
        ElseIf dropDownMenuParent = "PIB"
            mnuPIB.DropDown.Show()
        End If
        
        'With DirectCast(cmdConfirmedFilter.DropDownItems.Add("Channels"), Windows.Forms.ToolStripMenuItem)
        '    For Each TmpChan In Campaign.Channels
        '        IsUsed = False
        '        For Each TmpBT In TmpChan.BookingTypes
        '            If TmpBT.BookIt Then
        '                IsUsed = True
        '                Exit For
        '            End If
        '        Next
        '        If IsUsed Then
        '            With DirectCast(.DropDownItems.Add(TmpChan.ChannelName), Windows.Forms.ToolStripMenuItem)
        '                .Tag = "Channels"
        '                .Checked = GeneralFilter.Data("Channels", TmpChan.ChannelName)
        '                AddHandler .Click, AddressOf ChangeFilter
        '            End With
        '        End If
        '    Next
        '    .DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
        '    With DirectCast(.DropDownItems.Add("Invert selection"), Windows.Forms.ToolStripMenuItem)
        '        .Tag = "Channels"
        '        .Checked = False
        '        AddHandler .Click, AddressOf ChangeFilter
        '    End With
        'End With
        'With DirectCast(cmdConfirmedFilter.DropDownItems.Add("Weekdays"), Windows.Forms.ToolStripMenuItem)
        '    For i = 0 To 6
        '        With DirectCast(.DropDownItems.Add(WeekDays(i)), Windows.Forms.ToolStripMenuItem)
        '            .Tag = "Weekday"
        '            .Checked = GeneralFilter.Data("Weekday", WeekDays(i))
        '            AddHandler .Click, AddressOf ChangeFilter
        '        End With
        '    Next
        '    .DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
        '    With DirectCast(.DropDownItems.Add("Invert selection"), Windows.Forms.ToolStripMenuItem)
        '        .Tag = "Weekday"
        '        .Checked = False
        '        AddHandler .Click, AddressOf ChangeFilter
        '    End With
        'End With
        'With DirectCast(cmdConfirmedFilter.DropDownItems.Add("Dayparts"), Windows.Forms.ToolStripMenuItem)
        '    For i = 0 To Campaign.Dayparts.Count - 1
        '        With DirectCast(.DropDownItems.Add(Campaign.Dayparts(i).Name), Windows.Forms.ToolStripMenuItem)
        '            .Tag = "Daypart"
        '            .Checked = GeneralFilter.Data("Daypart", Campaign.Dayparts(i).Name)
        '            AddHandler .Click, AddressOf ChangeFilter
        '        End With
        '    Next
        '    .DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
        '    With DirectCast(.DropDownItems.Add("Invert selection"), Windows.Forms.ToolStripMenuItem)
        '        .Tag = "Daypart"
        '        .Checked = False
        '        AddHandler .Click, AddressOf ChangeFilter
        '    End With
        'End With
        'With DirectCast(cmdConfirmedFilter.DropDownItems.Add("Films"), Windows.Forms.ToolStripMenuItem)
        '    For Each TmpFilm In Campaign.Channels(1).BookingTypes(1).Weeks(1).Films
        '        With DirectCast(.DropDownItems.Add(TmpFilm.Name), Windows.Forms.ToolStripMenuItem)
        '            .Tag = "Film"
        '            .Checked = GeneralFilter.Data("Film", TmpFilm.Name)
        '            AddHandler .Click, AddressOf ChangeFilter
        '        End With
        '    Next
        '    .DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
        '    With DirectCast(.DropDownItems.Add("Invert selection"), Windows.Forms.ToolStripMenuItem)
        '        .Tag = "Film"
        '        .Checked = False
        '        AddHandler .Click, AddressOf ChangeFilter
        '    End With
        'End With
        'With DirectCast(cmdConfirmedFilter.DropDownItems.Add("Weeks"), Windows.Forms.ToolStripMenuItem)
        '    For Each TmpWeek In Campaign.Channels(1).BookingTypes(1).Weeks
        '        With DirectCast(.DropDownItems.Add(TmpWeek.Name), Windows.Forms.ToolStripMenuItem)
        '            .Tag = "Week"
        '            .Checked = GeneralFilter.Data("Week", TmpWeek.Name)
        '            AddHandler .Click, AddressOf ChangeFilter
        '        End With
        '    Next
        '    .DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
        '    With DirectCast(.DropDownItems.Add("Invert selection"), Windows.Forms.ToolStripMenuItem)
        '        .Tag = "Week"
        '        .Checked = False
        '        AddHandler .Click, AddressOf ChangeFilter
        '    End With
        'End With

        'Dim BTList As New List(Of String)
        'For Each TmpChan In Campaign.Channels
        '    For Each TmpBT In TmpChan.BookingTypes
        '        If TmpBT.BookIt AndAlso Not BTList.Contains(TmpBT.Name) Then
        '            BTList.Add(TmpBT.Name)
        '        End If
        '    Next
        'Next

        'With DirectCast(cmdConfirmedFilter.DropDownItems.Add("Bookingtypes"), Windows.Forms.ToolStripMenuItem)
        '    'since we have different bookingtypes on different channels (local channels etc) we need to check them all
        '    Dim l As New List(Of String)
        '    For Each ch As Trinity.cChannel In Campaign.Channels
        '        For Each TmpBT In ch.BookingTypes
        '            If Not l.Contains(TmpBT.Name) Then
        '                With DirectCast(.DropDownItems.Add(TmpBT.Name), Windows.Forms.ToolStripMenuItem)
        '                    l.Add(TmpBT.Name)
        '                    .Tag = "Bookingtype"
        '                    .Checked = GeneralFilter.Data("Bookingtype", TmpBT.Name)
        '                    AddHandler .Click, AddressOf ChangeFilter
        '                End With
        '            End If
        '        Next
        '    Next
        '    .DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
        '    With DirectCast(.DropDownItems.Add("Invert selection"), Windows.Forms.ToolStripMenuItem)
        '        .Tag = "Bookingtype"
        '        .Checked = False
        '        AddHandler .Click, AddressOf ChangeFilter
        '    End With
        'End With

        ''With DirectCast(cmdConfirmedFilter.DropDownItems.Add("Bookingtypes"), Windows.Forms.ToolStripMenuItem)
        ''    For Each BT As String In BTList
        ''        With DirectCast(.DropDownItems.Add(BT), Windows.Forms.ToolStripMenuItem)
        ''            .Tag = "Bookingtype"
        ''            .Checked = GeneralFilter.Data("Bookingtype", BT)
        ''            AddHandler .Click, AddressOf ChangeFilter
        ''        End With
        ''    Next
        ''    .DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
        ''    With DirectCast(.DropDownItems.Add("Invert selection"), Windows.Forms.ToolStripMenuItem)
        ''        .Tag = "Bookingtype"
        ''        .Checked = False
        ''        AddHandler .Click, AddressOf ChangeFilter
        ''    End With
        ''End With

        'With DirectCast(cmdConfirmedFilter.DropDownItems.Add("Position in break"), Windows.Forms.ToolStripMenuItem)
        '    With DirectCast(.DropDownItems.Add("First"), Windows.Forms.ToolStripMenuItem)
        '        .Tag = "PIB"
        '        .Checked = GeneralFilter.Data("PIB", "First")
        '        AddHandler .Click, AddressOf ChangeFilter
        '    End With
        '    With DirectCast(.DropDownItems.Add("Middle"), Windows.Forms.ToolStripMenuItem)
        '        .Tag = "PIB"
        '        .Checked = GeneralFilter.Data("PIB", "Middle")
        '        AddHandler .Click, AddressOf ChangeFilter
        '    End With
        '    With DirectCast(.DropDownItems.Add("Last"), Windows.Forms.ToolStripMenuItem)
        '        .Tag = "PIB"
        '        .Checked = GeneralFilter.Data("PIB", "Last")
        '        AddHandler .Click, AddressOf ChangeFilter
        '    End With
        '    .DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
        '    With DirectCast(.DropDownItems.Add("Invert selection"), Windows.Forms.ToolStripMenuItem)
        '        .Tag = "PIB"
        '        .Checked = False
        '        AddHandler .Click, AddressOf ChangeFilter
        '    End With
        'End With

    End Sub

    Private Sub cmdConfirmedColumns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConfirmedColumns.Click
        Dim Columns() As String = {"Date", "Time", "Channel", "Bookingtype", "Program", "Prog Before", "Prog After", "Week", "Weekday", "Gross Price", "Net Price", "Filmcode", "Film", "Film dscr", "Duration", "My Est", "Chan Est", "Actual", "Gross CPP", "CPP (" & Campaign.MainTarget.TargetNameNice & ")", "CPP (Chn Est)", "Remarks", "Added value"}
        Dim i As Integer
        Dim j As Integer
        Dim FoundIt As Boolean
        Dim TmpCol As Windows.Forms.DataGridViewColumn

        frmColumns.tvwChosen.Nodes.Clear()
        TmpCol = grdConfirmed.Columns.GetFirstColumn(Windows.Forms.DataGridViewElementStates.None)

        While Not TmpCol Is Nothing
            If TmpCol.Visible Then
                frmColumns.tvwChosen.Nodes.Add(CreateGUID, TmpCol.Tag)
            End If
            TmpCol = grdConfirmed.Columns.GetNextColumn(TmpCol, Windows.Forms.DataGridViewElementStates.None, Windows.Forms.DataGridViewElementStates.None)
        End While
        frmColumns.tvwAvailable.Nodes.Clear()
        For j = 0 To 22
            For i = 0 To frmColumns.tvwChosen.Nodes.Count - 1
                FoundIt = False
                If Columns(j) = frmColumns.tvwChosen.Nodes(i).Text Then
                    FoundIt = True
                    Exit For
                End If
            Next
            If Not FoundIt Then
                frmColumns.tvwAvailable.Nodes.Add(text:=Columns(j), key:=CreateGUID)
            End If
        Next

        If frmColumns.ShowDialog = Windows.Forms.DialogResult.OK Then
            TrinitySettings.ConfirmedColumnCount = frmColumns.tvwChosen.Nodes.Count
            i = 1
            For i = 0 To frmColumns.tvwChosen.Nodes.Count - 1
                TrinitySettings.ConfirmedColumn(i + 1) = frmColumns.tvwChosen.Nodes(i).Text
            Next
            UpdateConfirmed(True, False)
            runFiltersOnConfirmed()
        End If
    End Sub

    Private Sub grdConfirmed_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdConfirmed.CellFormatting
        If e.RowIndex = grdConfirmed.Rows.Count - 1 Then
            Select Case grdConfirmed.Columns(e.ColumnIndex).Name
                Case "colConfirmedGross Price", "colConfirmedNet Price"
                    e.Value = Format(e.Value, "C0")
                Case "colConfirmedChan Est"
                    e.Value = Format(e.Value, "N2")
                Case Else
                    If e.Value.ToString <> "" Then
                        e.Value = Format(e.Value, "N1")
                    End If
            End Select
            e.CellStyle.BackColor = Color.DarkGray
        End If
    End Sub

    ' Handles right clicks on the columns of the confirmed booking view
    Private Sub grdConfirmed_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdConfirmed.CellMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim _item As DataRowView = grdConfirmed.Rows(e.RowIndex).Tag
            Dim _planned As Trinity.cPlannedSpot = Campaign.PlannedSpots(_item("ID"))
            Dim _lastDistance = -1
            Dim _lastRowIndex As Integer
            For Each _row As Windows.Forms.DataGridViewRow In grdActual.Rows
                If _row.Tag IsNot Nothing Then
                    _item = _row.Tag
                    Dim _actual As Trinity.cActualSpot = Campaign.ActualSpots(_item("ID"))
                    If _planned.Channel Is _actual.Channel Then
                        Dim hours As Integer = _planned.MaM \ 60
                        Dim mins As Integer = _planned.MaM Mod 60
                        Dim _confDate As DateTime = Date.FromOADate(_planned.AirDate)
                        If hours >= 24 Then
                            _confDate = _confDate.AddDays(1)
                            hours -= 24
                        End If
                        _confDate = CDate(_confDate.ToShortDateString & " " & Format(hours, "00") & ":" & Format(mins, "00"))
                        Dim _actDate As DateTime = Date.FromOADate(_actual.AirDate)
                        hours = _actual.MaM \ 60
                        mins = _actual.MaM Mod 60
                        If hours >= 24 Then
                            _actDate = _actDate.AddDays(1)
                            hours -= 24
                        End If
                        _actDate = CDate(_actDate.ToShortDateString & " " & Format(hours, "00") & ":" & Format(mins, "00"))

                        Dim _dist As TimeSpan = _confDate.Subtract(_actDate)
                        If _lastDistance < 0 OrElse _lastDistance > Math.Abs(_dist.TotalMilliseconds) Then
                            _lastDistance = Math.Abs(_dist.TotalMilliseconds)
                            _lastRowIndex = _row.Index
                        Else
                            grdActual.ClearSelection()
                            grdActual.Rows(_lastRowIndex).Selected = True
                            grdActual.FirstDisplayedScrollingRowIndex = _lastRowIndex
                            Exit For
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub grdConfirmed_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles grdConfirmed.ColumnWidthChanged
        Dim TmpCol As Windows.Forms.DataGridViewColumn = e.Column
        TrinitySettings.ConfirmedColumnWidth(TmpCol.DisplayIndex + 1) = TmpCol.Width
    End Sub

    Private Sub grdConfirmed_ColumnDisplayIndexChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles grdConfirmed.ColumnDisplayIndexChanged
        Dim TmpCol As Windows.Forms.DataGridViewColumn
        Dim Count As Integer = 0
        If Not SkipIt Then
            For Each TmpCol In grdConfirmed.Columns
                If TmpCol.Visible Then
                    Count = Count + 1
                    TrinitySettings.ConfirmedColumnCount = Count
                    TrinitySettings.ConfirmedColumn(TmpCol.DisplayIndex + 1) = TmpCol.Tag
                    TrinitySettings.ConfirmedColumnWidth(TmpCol.DisplayIndex + 1) = TmpCol.Width
                End If
            Next
        End If
    End Sub

    Private Sub grdActual_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdActual.CellContentDoubleClick
        If grdActual.Columns(e.ColumnIndex).HeaderText = "SC" Then

            Dim TmpSpot As Trinity.cActualSpot = Campaign.ActualSpots(grdActual.Rows(e.RowIndex).Tag)

            If TmpSpot.SpotControlRemark <> "" Then
                frmSpotControlImport.ShowDialog()
            End If
        End If
    End Sub

    Private Sub grdActual_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles grdActual.ColumnWidthChanged
        Dim TmpCol As Windows.Forms.DataGridViewColumn = e.Column
        TrinitySettings.ActualColumnWidth(TmpCol.DisplayIndex + 1) = TmpCol.Width
    End Sub

    Private Sub grdActual_ColumnDisplayIndexChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles grdActual.ColumnDisplayIndexChanged
        Dim TmpCol As Windows.Forms.DataGridViewColumn
        Dim Count As Integer = 0
        If Not SkipIt Then
            For Each TmpCol In grdActual.Columns
                If TmpCol.Visible Then
                    Count = Count + 1
                    TrinitySettings.ActualColumnCount = Count
                    TrinitySettings.ActualColumn(TmpCol.DisplayIndex + 1) = TmpCol.Tag
                    TrinitySettings.ActualColumnWidth(TmpCol.DisplayIndex + 1) = TmpCol.Width
                End If
            Next
        End If
    End Sub
    Private Sub cmdBreakAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBreakAll.Click
        Saved = False
        Dim TmpActualSpot As Trinity.cActualSpot
        Dim TmpPlannedSpot As Trinity.cPlannedSpot

        For Each TmpActualSpot In Campaign.ActualSpots
            TmpActualSpot.MatchedSpot = Nothing
        Next
        For Each TmpPlannedSpot In Campaign.PlannedSpots
            TmpPlannedSpot.MatchedSpot = Nothing
        Next

        UpdateConfirmed(False, True)
        UpdateActual(False, True)
    End Sub

    Private Sub cmdMatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMatch.Click
        Saved = False
        If grdConfirmed.SelectedRows.Count = 0 Then
            Windows.Forms.MessageBox.Show("No spot chosen among Confirmed spots.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If grdActual.SelectedRows.Count = 0 Then
            Windows.Forms.MessageBox.Show("No spot chosen among Actual spots.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        ' Check if any of the selected spots is the sum row, either in the Planned or Actual View
        If grdConfirmed.SelectedRows(0).Tag Is Nothing OrElse grdActual.SelectedRows(0).Tag Is Nothing Then
            Windows.Forms.MessageBox.Show("Cannot match the summary row, please select another row.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If Not Campaign.PlannedSpots(grdConfirmed.SelectedRows.Item(0).Tag.Item("ID")).MatchedSpot Is Nothing Then
            Windows.Forms.MessageBox.Show("The Confirmed spot you have chosen is already matched.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If Not Campaign.ActualSpots(grdActual.SelectedRows.Item(0).Tag.Item("ID")).MatchedSpot Is Nothing Then
            Windows.Forms.MessageBox.Show("The Actual spot you have chosen is already matched.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim cSpot As Trinity.cPlannedSpot = Campaign.PlannedSpots(grdConfirmed.SelectedRows.Item(0).Tag.Item("ID"))
        Dim aSpot As Trinity.cActualSpot = Campaign.ActualSpots(grdActual.SelectedRows.Item(0).Tag.Item("ID"))

        cSpot.MatchedSpot = aSpot
        aSpot.MatchedSpot = cSpot

        Dim drC As DataRowView = grdConfirmed.SelectedRows.Item(0).Tag
        Dim drA As DataRowView = grdActual.SelectedRows.Item(0).Tag
        drA.Item("MatchedSpotID") = cSpot.ID
        drA.Item("Gross Price") = Format(cSpot.PriceGross, "##,##0")
        drA.Item("Net Price") = Format(cSpot.PriceNet, "##,##0")
        drA.Item("My Est0") = Format(cSpot.MyRating, "0.0")
        drA.Item("Chan Est0") = Format(cSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget), "0.0")
        drA.Item("My Est1") = Format(cSpot.MyRating * (aSpot.Bookingtype.IndexSecondTarget / aSpot.Bookingtype.IndexMainTarget), "0.0")
        drA.Item("Chan Est1") = Format(cSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget), "0.0")
        drA.Item("My Est2") = Format(cSpot.MyRating, "0.0")
        drA.Item("Chan Est2") = Format(cSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteThirdTarget), "0.0")
        drA.Item("My Est3") = Format(cSpot.MyRating, "0.0")
        drA.Item("Chan Est3") = Format(cSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "0.0")
        drA.Item("My Est4") = Format(cSpot.MyRating, "0.0")
        drA.Item("Chan Est4") = Format(cSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteAllAdults), "0.0")

        drC.Item("Actual0") = Format(aSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget), "0.0")
        drC.Item("Actual1") = Format(aSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget), "0.0")
        drC.Item("Actual2") = Format(aSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateThirdTarget), "0.0")
        drC.Item("Actual3") = Format(aSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget), "0.0")
        drC.Item("Actual4") = Format(aSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateAllAdults), "0.0")

        aSpot.Bookingtype = cSpot.Bookingtype
        drA.Item("Bookingtype") = drC.Item("Bookingtype")

        If CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget), "0.0")) < CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget), "0.0")) Then
            drA.Item("Color0") = "RED"
            drC.Item("Color0") = "RED"
        ElseIf CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget), "0.0")) > CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget), "0.0")) Then
            drA.Item("Color0") = "GREEN"
            drC.Item("Color0") = "GREEN"
        Else
            drA.Item("Color0") = "BLUE"
            drC.Item("Color0") = "BLUE"
        End If
        If CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget), "0.0")) < CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget), "0.0")) Then
            drA.Item("Color1") = "RED"
            drC.Item("Color1") = "RED"
        ElseIf CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget), "0.0")) > CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget), "0.0")) Then
            drA.Item("Color1") = "GREEN"
            drC.Item("Color1") = "GREEN"
        Else
            drA.Item("Color1") = "BLUE"
            drC.Item("Color1") = "BLUE"
        End If
        If CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteThirdTarget), "0.0")) < CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateThirdTarget), "0.0")) Then
            drA.Item("Color2") = "RED"
            drC.Item("Color2") = "RED"
        ElseIf CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteThirdTarget), "0.0")) > CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateThirdTarget), "0.0")) Then
            drA.Item("Color2") = "GREEN"
            drC.Item("Color2") = "GREEN"
        Else
            drA.Item("Color2") = "BLUE"
            drC.Item("Color2") = "BLUE"
        End If
        If CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "0.0")) < CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget), "0.0")) Then
            drA.Item("Color3") = "RED"
            drC.Item("Color3") = "RED"
        ElseIf CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "0.0")) > CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget), "0.0")) Then
            drA.Item("Color3") = "GREEN"
            drC.Item("Color3") = "GREEN"
        Else
            drA.Item("Color3") = "BLUE"
            drC.Item("Color3") = "BLUE"
        End If
        If CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteAllAdults), "0.0")) < CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateAllAdults), "0.0")) Then
            drA.Item("Color4") = "RED"
            drC.Item("Color4") = "RED"
        ElseIf CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteAllAdults), "0.0")) > CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateAllAdults), "0.0")) Then
            drA.Item("Color4") = "GREEN"
            drC.Item("Color4") = "GREEN"
        Else
            drA.Item("Color4") = "BLUE"
            drC.Item("Color4") = "BLUE"
        End If
    End Sub

    Private Sub cmdBookingtype_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBookingtype.DropDownOpening

        If grdConfirmed.SelectedRows.Count = 0 Then
            Exit Sub
        End If

        'If only one row is selected and that row is the summary row, emty the selection
        If grdConfirmed.SelectedRows.Count = 1 And grdConfirmed.SelectedRows(0).Tag Is Nothing Then
            cmdBookingtype.DropDownItems.Clear()
            Exit Sub
        End If

        cmdBookingtype.DropDownItems.Clear()

        'Changed as of 31/8 BF
        'Dim TmpChan As Trinity.cChannel
        Dim tmpFirstBookingType As List(Of String) = New List(Of String)

        ' Creates a lsit of all possible booking types for all the selected spots
        Dim VisibleRowFound As Boolean
        For Each tmpRow As System.Windows.Forms.DataGridViewRow In grdConfirmed.SelectedRows
            If tmpRow.Tag IsNot Nothing Then
                VisibleRowFound = tmpRow.Visible
                If VisibleRowFound Then
                    'TmpChan = Campaign.ActualSpots(tmpRow.Tag.Item("ID")).Bookingtype.ParentChannel

                    For Each _bt As Trinity.cBookingType In Campaign.Channels(LongName(tmpRow.Tag.Item("Channel"))).BookingTypes
                        If _bt.BookIt AndAlso Not tmpFirstBookingType.Contains(_bt.Name) Then
                            tmpFirstBookingType.Add(_bt.Name)
                        End If
                    Next
                End If
            End If
        Next

        Dim tmpFinalBookingType As List(Of String) = New List(Of String)

        ' Interate trough all the Booking types found, and all channels of all selected Rows to filter out those who dont match
        For Each tmpType As String In tmpFirstBookingType
            Dim tmpFoundInAll = True
            For Each tmpRow As System.Windows.Forms.DataGridViewRow In grdConfirmed.SelectedRows
                If tmpRow.Tag IsNot Nothing Then
                    If Campaign.Channels(LongName(tmpRow.Tag.Item("Channel"))).BookingTypes(tmpType) Is Nothing OrElse Not Campaign.Channels(LongName(tmpRow.Tag.Item("Channel"))).BookingTypes(tmpType).BookIt Then
                        tmpFoundInAll = False
                    End If
                End If
            Next
            If tmpFoundInAll Then
                tmpFinalBookingType.Add(tmpType)
            End If
        Next

        ' Add the checked marker for the item that is currently selected (if applicable)
        For Each tmpType As String In tmpFinalBookingType
            With DirectCast(cmdBookingtype.DropDownItems.Add(tmpType), System.Windows.Forms.ToolStripMenuItem)

                ' The menu strip must be treated differently if one or if more than one row is selected
                If grdActual.SelectedRows.Count = 1 Then
                    If tmpType = grdConfirmed.SelectedRows.Item(0).Tag.Item("Bookingtype") Then
                        .Checked = True
                    End If
                ElseIf grdConfirmed.SelectedRows.Count > 1 Then
                    Dim tmpFoundInAll As Boolean = True
                    For Each tmpRow As Windows.Forms.DataGridViewRow In grdConfirmed.SelectedRows
                        If tmpRow.Tag IsNot Nothing Then
                            If Not tmpType = tmpRow.Tag.Item("Bookingtype") Then
                                tmpFoundInAll = False
                            End If
                        End If
                    Next
                    If tmpFoundInAll Then
                        .Checked = True
                    End If
                End If
                AddHandler .Click, AddressOf ChangeBookingtype
            End With
        Next

        'cmdBookingtype.DropDownItems.Clear()

        ''Dim TmpBT As Trinity.cBookingType
        'If grdConfirmed.SelectedRows.Count = 0 Then
        '    Exit Sub
        'End If

        ''Dim TmpChan As Trinity.cChannel = Campaign.PlannedSpots(grdConfirmed.SelectedRows(0).Tag.Item("ID")).Bookingtype.ParentChannel

        ''For Each TmpBT In TmpChan.BookingTypes
        ''    If TmpBT.BookIt Then
        ''        With DirectCast(cmdBookingtype.DropDownItems.Add(TmpBT.Name), System.Windows.Forms.ToolStripMenuItem)
        ''            If TmpBT.Name = grdConfirmed.SelectedRows.Item(0).Tag.Item("Bookingtype") Then
        ''                .Checked = True
        ''            End If
        ''            AddHandler .Click, AddressOf ChangeBookingtype
        ''        End With
        ''    End If
        ''Next

    End Sub

    Private Sub cmdActualBookingtype_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdActualBookingtype.DropDownOpening
        If grdActual.SelectedRows.Count = 0 Then
            Exit Sub
        End If

        'If only one row is selected and that row is the summary row, emty the selection
        If grdActual.SelectedRows.Count = 1 And grdActual.SelectedRows(0).Tag Is Nothing Then
            cmdActualBookingtype.DropDownItems.Clear()
            Exit Sub
        End If

        cmdActualBookingtype.DropDownItems.Clear()

        'Changed as of 26/8 BF
        'Dim TmpChan As Trinity.cChannel
        Dim tmpFirstBookingType As List(Of String) = New List(Of String)

        ' Creates a lsit of all possible booking types for all the selected spots
        Dim VisibleRowFound As Boolean
        For Each tmpRow As System.Windows.Forms.DataGridViewRow In grdActual.SelectedRows
            If tmpRow.Tag IsNot Nothing Then
                VisibleRowFound = tmpRow.Visible
                If VisibleRowFound Then
                    'TmpChan = Campaign.ActualSpots(tmpRow.Tag.Item("ID")).Bookingtype.ParentChannel

                    For Each _bt As Trinity.cBookingType In Campaign.Channels(LongName(tmpRow.Tag.Item("Channel"))).BookingTypes
                        If _bt.BookIt AndAlso Not tmpFirstBookingType.Contains(_bt.Name) Then
                            tmpFirstBookingType.Add(_bt.Name)
                        End If
                    Next
                End If
            End If
        Next

        Dim tmpFinalBookingType As List(Of String) = New List(Of String)

        ' Interate trough all the Booking types found, and all channels of all selected Rows to filter out those who dont match
        For Each tmpType As String In tmpFirstBookingType
            Dim tmpFoundInAll = True
            For Each tmpRow As System.Windows.Forms.DataGridViewRow In grdActual.SelectedRows
                If tmpRow.Tag IsNot Nothing Then
                    If Campaign.Channels(LongName(tmpRow.Tag.Item("Channel"))).BookingTypes(tmpType) Is Nothing OrElse Not Campaign.Channels(LongName(tmpRow.Tag.Item("Channel"))).BookingTypes(tmpType).BookIt Then
                        tmpFoundInAll = False
                    End If
                End If
            Next
            If tmpFoundInAll Then
                tmpFinalBookingType.Add(tmpType)
            End If
        Next

        ' Add the checked marker for the item that is currently selected (if applicable)
        For Each tmpType As String In tmpFinalBookingType
            With DirectCast(cmdActualBookingtype.DropDownItems.Add(tmpType), System.Windows.Forms.ToolStripMenuItem)

                ' The menu strip must be treated differently if one or if more than one row is selected
                If grdActual.SelectedRows.Count = 1 Then
                    If tmpType = grdActual.SelectedRows.Item(0).Tag.Item("Bookingtype") Then
                        .Checked = True
                    End If
                ElseIf grdActual.SelectedRows.Count > 1 Then
                    Dim tmpFoundInAll As Boolean = True
                    For Each tmpRow As Windows.Forms.DataGridViewRow In grdActual.SelectedRows
                        If tmpRow.Tag IsNot Nothing Then
                            If Not tmpType = tmpRow.Tag.Item("Bookingtype") Then
                                tmpFoundInAll = False
                            End If
                        End If
                    Next
                    If tmpFoundInAll Then
                        .Checked = True
                    End If
                End If
                AddHandler .Click, AddressOf ChangeBookingtypeActual
            End With
        Next

        'TmpChan = Campaign.ActualSpots(grdActual.SelectedRows(0).Tag.Item("ID")).Bookingtype.ParentChannel

        'For Each TmpBT In TmpChan.BookingTypes
        '    If TmpBT.BookIt Then
        '        With DirectCast(cmdActualBookingtype.DropDownItems.Add(TmpBT.Name), System.Windows.Forms.ToolStripMenuItem)
        '                If TmpBT.Name = grdActual.SelectedRows.Item(0).Tag.Item("Bookingtype") Then
        '                    .Checked = True
        '                End If
        '            AddHandler .Click, AddressOf ChangeBookingtypeActual
        '        End With
        '    End If
        'Next

    End Sub

    Sub ChangeBookingtype(ByVal sender As Object, ByVal e As EventArgs)

        Dim TmpRow As Windows.Forms.DataGridViewRow

        If grdConfirmed.SelectedRows.Count = 0 Then
            Windows.Forms.MessageBox.Show("No spot chosen.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        For Each TmpRow In grdConfirmed.SelectedRows
            Campaign.PlannedSpots(TmpRow.Tag.Item("ID")).Bookingtype = Campaign.PlannedSpots(TmpRow.Tag.Item("ID")).Channel.BookingTypes(sender.text)
            TmpRow.Tag.Item("Bookingtype") = sender.text
            If Not Campaign.PlannedSpots(TmpRow.Tag.Item("ID")).MatchedSpot Is Nothing Then
                Campaign.PlannedSpots(TmpRow.Tag.Item("ID")).MatchedSpot.Bookingtype = Campaign.PlannedSpots(TmpRow.Tag.Item("ID")).Channel.BookingTypes(sender.text)
                Dim dt As DataRow = dtActual.Select("ID ='" & Campaign.PlannedSpots(TmpRow.Tag.Item("ID")).MatchedSpot.ID & "'")(0)
                dt.Item("Bookingtype") = sender.text
            End If
        Next
    End Sub

    Sub ChangeBookingtypeActual(ByVal sender As Object, ByVal e As EventArgs)

        Dim TmpRow As Windows.Forms.DataGridViewRow

        If grdActual.SelectedRows.Count = 0 Then
            Windows.Forms.MessageBox.Show("No spot chosen.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        For Each TmpRow In grdActual.SelectedRows
            If TmpRow.Visible AndAlso TmpRow.Tag IsNot Nothing Then
                Campaign.ActualSpots(TmpRow.Tag.Item("ID")).Bookingtype = Campaign.ActualSpots(TmpRow.Tag.Item("ID")).Channel.BookingTypes(sender.text)
                TmpRow.Tag.Item("Bookingtype") = sender.text
                If Not Campaign.ActualSpots(TmpRow.Tag.Item("ID")).MatchedSpot Is Nothing Then
                    Campaign.ActualSpots(TmpRow.Tag.Item("ID")).MatchedSpot.Bookingtype = Campaign.ActualSpots(TmpRow.Tag.Item("ID")).Channel.BookingTypes(sender.text)
                    Dim selectString As String = "ID ='" & Campaign.ActualSpots(TmpRow.Tag.Item("ID")).MatchedSpot.ID.ToString & "'"
                    Dim dt As DataRow = dtConfirmed.Select(selectString)(0)
                    dt.Item("Bookingtype") = sender.text
                End If
            End If
        Next
    End Sub

    Sub ChangeFilm(ByVal sender As Object, ByVal e As EventArgs)

        Dim TmpRow As Windows.Forms.DataGridViewRow

        If grdConfirmed.SelectedRows.Count = 0 Then
            Windows.Forms.MessageBox.Show("No spot chosen.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        For Each TmpRow In grdConfirmed.SelectedRows
            Campaign.PlannedSpots(TmpRow.Tag.Item("ID")).Filmcode = Campaign.PlannedSpots(TmpRow.Tag.Item("ID")).Week.Films(sender.text).Filmcode
            TmpRow.Tag.Item("Filmcode") = Campaign.PlannedSpots(TmpRow.Tag.Item("ID")).Week.Films(sender.text).Filmcode
            grdConfirmed.InvalidateRow(TmpRow.Index)
        Next

        'UpdateConfirmed(False, False)

    End Sub

    Private Sub cmdFilm_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFilm.DropDownOpening
        Dim TmpFilm As Trinity.cFilm
        If grdConfirmed.SelectedRows.Count = 0 Then
            Exit Sub
        End If
        cmdFilm.DropDownItems.Clear()
        For Each TmpFilm In Campaign.Channels(1).BookingTypes(1).Weeks(1).Films
            With DirectCast(cmdFilm.DropDownItems.Add(TmpFilm.Name), System.Windows.Forms.ToolStripMenuItem)
                If grdConfirmed.SelectedRows.Item(0).Tag IsNot Nothing Then 'Added an isnot nothing statement /JK 
                    If TmpFilm.Name = Campaign.PlannedSpots(grdConfirmed.SelectedRows.Item(0).Tag.item("ID")).Film.Name Then
                        .Checked = True
                    End If
                    AddHandler .Click, AddressOf ChangeFilm
                End If
            End With
        Next
    End Sub

    Private Sub cmdConfirmedExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConfirmedExcel.Click
        Dim i As Integer
        Dim j As Integer
        Dim FoundIt As Boolean

        frmExportSpotlist.Tag = "PLANNED"
        'Hannes added Prog Before and Prog After to this list
        Dim Columns() As String = {"Date", "Time", "Channel", "Program", "Week", "Weekday", "Gross Price", "Net Price", "Filmcode", "Film", "Film dscr", "Est", "Chan Est", "Gross CPP", "CPP Main", "CPP (Chn Est)", "Quality", "Remarks", "Notes", "Added value", "Bookingtype", "Duration", "Prog Before", "Prog After"}

        frmExportSpotlist.tvwChosen.Nodes.Clear()
        For i = 1 To TrinitySettings.PrintColumnCount
            If TrinitySettings.PrintColumn(i) = "CPP Main" Then
                frmExportSpotlist.tvwChosen.Nodes.Add("CPP (" & Campaign.MainTarget.TargetNameNice & ")")
            Else
                frmExportSpotlist.tvwChosen.Nodes.Add(TrinitySettings.PrintColumn(i))
            End If
        Next
        frmExportSpotlist.tvwAvailable.Nodes.Clear()
        'Hannes increased this from j = 0 to 21 because of Prog Before and Prog After
        For j = 0 To 23
            For i = 0 To frmExportSpotlist.tvwChosen.Nodes.Count - 1
                FoundIt = False
                If Columns(j) = frmExportSpotlist.tvwChosen.Nodes(i).Text OrElse (Columns(j) = "CPP Main" AndAlso frmExportSpotlist.tvwChosen.Nodes(i).Text = "CPP (" & Campaign.MainTarget.TargetNameNice & ")") Then
                    FoundIt = True
                    Exit For
                End If
            Next
            If Not FoundIt Then
                If Columns(j) = "CPP Main" Then
                    frmExportSpotlist.tvwAvailable.Nodes.Add(text:="CPP (" & Campaign.MainTarget.TargetNameNice & ")", key:=CreateGUID)
                Else
                    frmExportSpotlist.tvwAvailable.Nodes.Add(text:=Columns(j), key:=CreateGUID)
                End If
            End If
        Next
        frmExportSpotlist.ShowDialog()
    End Sub

    Private Sub cmdActualColumns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdActualColumns.Click
        Dim Columns() As String = {"Date", "Time", "Channel", "Bookingtype", "Program", "Prog Before", "Prog After", "Week", "Weekday", "Gross Price", "Net Price", "Filmcode", "Film", "Film dscr", "Actual", "My Est", "Chan Est", "Gross CPP", "CPP (" & Campaign.MainTarget.TargetNameNice & ")", "CPP (Chn Est)", "Remarks", "Added value", "PIB", "Net value", "SC"}
        Dim i As Integer
        Dim j As Integer
        Dim FoundIt As Boolean
        Dim TmpNode As System.Windows.Forms.TreeNode
        Dim TmpCol As Windows.Forms.DataGridViewColumn

        frmColumns.tvwChosen.Nodes.Clear()
        TmpCol = grdActual.Columns.GetFirstColumn(Windows.Forms.DataGridViewElementStates.None)

        While Not TmpCol Is Nothing
            If TmpCol.Visible Then
                frmColumns.tvwChosen.Nodes.Add(CreateGUID, TmpCol.Tag)
            End If
            TmpCol = grdActual.Columns.GetNextColumn(TmpCol, Windows.Forms.DataGridViewElementStates.None, Windows.Forms.DataGridViewElementStates.None)
        End While
        frmColumns.tvwAvailable.Nodes.Clear()
        For j = 0 To 24
            For i = 0 To frmColumns.tvwChosen.Nodes.Count - 1
                FoundIt = False
                If Columns(j) = frmColumns.tvwChosen.Nodes(i).Text Then
                    FoundIt = True
                    Exit For
                End If
            Next
            If Not FoundIt Then
                frmColumns.tvwAvailable.Nodes.Add(text:=Columns(j), key:=CreateGUID)
            End If
        Next

        If frmColumns.ShowDialog = Windows.Forms.DialogResult.OK Then
            TrinitySettings.ActualColumnCount = frmColumns.tvwChosen.Nodes.Count
            TmpNode = frmColumns.tvwChosen.Nodes(1)
            While Not TmpNode.PrevNode Is Nothing
                TmpNode = TmpNode.PrevNode
            End While
            i = 1
            While Not TmpNode Is Nothing
                TrinitySettings.ActualColumn(i) = TmpNode.Text
                i = i + 1
                TmpNode = TmpNode.NextNode
            End While
            UpdateActual(True, False)
        End If

    End Sub

    Private Sub cmdActualFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdActualFilter.Click
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpFilm As Trinity.cFilm
        Dim TmpWeek As Trinity.cWeek
        Dim WeekDays() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}

        Dim IsUsed As Boolean
        Dim i As Integer

        cmdConfirmedFilter.DropDownItems.Clear()
        With DirectCast(cmdConfirmedFilter.DropDownItems.Add("Channels"), Windows.Forms.ToolStripMenuItem)
            For Each TmpChan In Campaign.Channels
                IsUsed = False
                For Each TmpBT In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        IsUsed = True
                        Exit For
                    End If
                Next
                If IsUsed Then
                    With DirectCast(.DropDownItems.Add(TmpChan.ChannelName), Windows.Forms.ToolStripMenuItem)
                        .Tag = "Channels"
                        .Checked = GeneralFilter.Data("Channels", TmpChan.ChannelName)
                        AddHandler .Click, AddressOf ChangeFilter
                    End With
                End If
            Next
        End With
        With DirectCast(cmdConfirmedFilter.DropDownItems.Add("Weekdays"), Windows.Forms.ToolStripMenuItem)
            For i = 0 To 6
                With DirectCast(.DropDownItems.Add(WeekDays(i)), Windows.Forms.ToolStripMenuItem)
                    .Tag = "Weekday"
                    .Checked = GeneralFilter.Data("Weekday", WeekDays(i))
                    AddHandler .Click, AddressOf ChangeFilter
                End With
            Next
        End With
        With DirectCast(cmdConfirmedFilter.DropDownItems.Add("Dayparts"), Windows.Forms.ToolStripMenuItem)
            For i = 0 To Campaign.Dayparts.Count - 1
                With DirectCast(.DropDownItems.Add(Campaign.Dayparts(i).Name), Windows.Forms.ToolStripMenuItem)
                    .Tag = "Daypart"
                    .Checked = GeneralFilter.Data("Daypart", Campaign.Dayparts(i).Name)
                    AddHandler .Click, AddressOf ChangeFilter
                End With
            Next
        End With
        With DirectCast(cmdConfirmedFilter.DropDownItems.Add("Films"), Windows.Forms.ToolStripMenuItem)
            For Each TmpFilm In Campaign.Channels(1).BookingTypes(1).Weeks(1).Films
                With DirectCast(.DropDownItems.Add(TmpFilm.Name), Windows.Forms.ToolStripMenuItem)
                    .Tag = "Film"
                    .Checked = GeneralFilter.Data("Film", TmpFilm.Name)
                    AddHandler .Click, AddressOf ChangeFilter
                End With
            Next
        End With
        With DirectCast(cmdConfirmedFilter.DropDownItems.Add("Weeks"), Windows.Forms.ToolStripMenuItem)
            For Each TmpWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                With DirectCast(.DropDownItems.Add(TmpWeek.Name), Windows.Forms.ToolStripMenuItem)
                    .Tag = "Week"
                    .Checked = GeneralFilter.Data("Week", TmpWeek.Name)
                    AddHandler .Click, AddressOf ChangeFilter
                End With
            Next
        End With

        With DirectCast(cmdConfirmedFilter.DropDownItems.Add("Bookingtypes"), Windows.Forms.ToolStripMenuItem)
            'since we have different bookingtypes on different channels (local channels etc) we need to check them all
            For Each ch As Trinity.cChannel In Campaign.Channels
                For Each TmpBT In ch.BookingTypes
                    If Not .DropDownItems.ContainsKey(TmpBT.Name) Then
                        With DirectCast(.DropDownItems.Add(TmpBT.Name), Windows.Forms.ToolStripMenuItem)
                            .Tag = "Bookingtype"
                            .Checked = GeneralFilter.Data("Bookingtype", TmpBT.Name)
                            AddHandler .Click, AddressOf ChangeFilter
                        End With
                    End If
                Next
            Next
        End With

        With DirectCast(cmdConfirmedFilter.DropDownItems.Add("Position in break"), Windows.Forms.ToolStripMenuItem)
            With DirectCast(.DropDownItems.Add("First"), Windows.Forms.ToolStripMenuItem)
                .Tag = "PIB"
                .Checked = GeneralFilter.Data("PIB", "First")
                AddHandler .Click, AddressOf ChangeFilter
            End With
            With DirectCast(.DropDownItems.Add("Middle"), Windows.Forms.ToolStripMenuItem)
                .Tag = "PIB"
                .Checked = GeneralFilter.Data("PIB", "Middle")
                AddHandler .Click, AddressOf ChangeFilter
            End With
            With DirectCast(.DropDownItems.Add("Last"), Windows.Forms.ToolStripMenuItem)
                .Tag = "PIB"
                .Checked = GeneralFilter.Data("PIB", "Last")
                AddHandler .Click, AddressOf ChangeFilter
            End With
        End With
    End Sub


    Private Sub cmdActualExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdActualExcel.Click
        Dim i As Integer
        Dim j As Integer
        Dim FoundIt As Boolean

        frmExportSpotlist.Tag = "ACTUAL"


        Dim Columns() As String = {"Date", "Time", "Channel", "Program", "Week", "Weekday", "Gross Price", "Net Price", "Filmcode", "Film", "Film dscr", "Est", "Actual", "Chan Est", "Gross CPP", "CPP (" & Campaign.MainTarget.TargetNameNice & ")", "CPP (Chn Est)", "Quality", "Remarks", "Notes", "Added value", "PIB"}

        frmExportSpotlist.tvwChosen.Nodes.Clear()
        For i = 1 To TrinitySettings.PrintColumnCount
            frmExportSpotlist.tvwChosen.Nodes.Add(TrinitySettings.PrintColumn(i))
        Next
        frmExportSpotlist.tvwAvailable.Nodes.Clear()
        For j = 0 To 21
            For i = 0 To frmExportSpotlist.tvwChosen.Nodes.Count - 1
                FoundIt = False
                If Columns(j) = frmExportSpotlist.tvwChosen.Nodes(i).Text Then
                    FoundIt = True
                    Exit For
                End If
            Next
            If Not FoundIt Then
                frmExportSpotlist.tvwAvailable.Nodes.Add(text:=Columns(j), key:=CreateGUID)
            End If
        Next
        frmExportSpotlist.ShowDialog()
    End Sub

    Private Sub cmdActualAutomatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdActualAutomatch.Click
        Saved = False
        Dim TmpPlanned As Trinity.cPlannedSpot
        Dim TmpActual As Trinity.cActualSpot

        For Each TmpPlanned In Campaign.PlannedSpots
            If TmpPlanned.MatchedSpot Is Nothing Then
                For Each TmpActual In Campaign.ActualSpots
                    If TmpActual.MatchedSpot Is Nothing Then
                        If TmpPlanned.MaM - 15 <= TmpActual.MaM Then
                            If TmpPlanned.MaM + 15 >= TmpActual.MaM Then
                                If TmpPlanned.AirDate = TmpActual.AirDate Then
                                    If TmpPlanned.Channel Is TmpActual.Channel Then
                                        If TmpPlanned.Filmcode = TmpActual.Filmcode Then
                                            TmpPlanned.MatchedSpot = TmpActual
                                            TmpActual.MatchedSpot = TmpPlanned
                                            TmpActual.Bookingtype = TmpPlanned.Bookingtype
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        Next
        UpdateConfirmed(False, True)
        UpdateActual(False, True)

    End Sub

    Private Sub cmdActualMatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdActualMatch.Click
        If grdConfirmed.SelectedRows.Count = 0 Then
            Windows.Forms.MessageBox.Show("No spot chosen among Confirmed spots.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If grdActual.SelectedRows.Count = 0 Then
            Windows.Forms.MessageBox.Show("No spot chosen among Actual spots.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If grdActual.SelectedRows(0).Tag Is Nothing OrElse grdConfirmed.SelectedRows(0).Tag Is Nothing Then
            Windows.Forms.MessageBox.Show("Cannot match the summary row, please select another row.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If Not Campaign.PlannedSpots(grdConfirmed.SelectedRows.Item(0).Tag.Item("ID")).MatchedSpot Is Nothing Then
            Windows.Forms.MessageBox.Show("The Confirmed spot you have chosen is already matched.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If Not Campaign.ActualSpots(grdActual.SelectedRows.Item(0).Tag.Item("ID")).MatchedSpot Is Nothing Then
            Windows.Forms.MessageBox.Show("The Actual spot you have chosen is already matched.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Dim cSpot As Trinity.cPlannedSpot = Campaign.PlannedSpots(grdConfirmed.SelectedRows.Item(0).Tag.Item("ID"))
        Dim aSpot As Trinity.cActualSpot = Campaign.ActualSpots(grdActual.SelectedRows.Item(0).Tag.Item("ID"))

        cSpot.MatchedSpot = aSpot
        aSpot.MatchedSpot = cSpot

        Dim drC As DataRowView = grdConfirmed.SelectedRows.Item(0).Tag
        Dim drA As DataRowView = grdActual.SelectedRows.Item(0).Tag

        drA.Item("MatchedSpotID") = cSpot.ID
        drA.Item("Gross Price") = Format(cSpot.PriceGross, "##,##0")
        drA.Item("Net Price") = Format(cSpot.PriceNet, "##,##0")
        drA.Item("My Est0") = Format(cSpot.MyRating, "0.0")
        drA.Item("Chan Est0") = Format(cSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget), "0.0")
        drA.Item("My Est1") = Format(cSpot.MyRating * (aSpot.Bookingtype.IndexSecondTarget / aSpot.Bookingtype.IndexMainTarget), "0.0")
        drA.Item("Chan Est1") = Format(cSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget), "0.0")
        drA.Item("My Est2") = Format(cSpot.MyRating, "0.0")
        drA.Item("Chan Est2") = Format(cSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteThirdTarget), "0.0")
        drA.Item("My Est3") = Format(cSpot.MyRating, "0.0")
        drA.Item("Chan Est3") = Format(cSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "0.0")
        drA.Item("My Est4") = Format(cSpot.MyRating, "0.0")
        drA.Item("Chan Est4") = Format(cSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteAllAdults), "0.0")

        drC.Item("Actual0") = Format(aSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget), "0.0")
        drC.Item("Actual1") = Format(aSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget), "0.0")
        drC.Item("Actual2") = Format(aSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateThirdTarget), "0.0")
        drC.Item("Actual3") = Format(aSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget), "0.0")
        drC.Item("Actual4") = Format(aSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateAllAdults), "0.0")

        aSpot.Bookingtype = cSpot.Bookingtype
        drA.Item("Bookingtype") = drC.Item("Bookingtype")

        If CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget), "0.0")) < CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget), "0.0")) Then
            drA.Item("Color0") = "RED"
            drC.Item("Color0") = "RED"
        ElseIf CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget), "0.0")) > CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget), "0.0")) Then
            drA.Item("Color0") = "GREEN"
            drC.Item("Color0") = "GREEN"
        Else
            drA.Item("Color0") = "BLUE"
            drC.Item("Color0") = "BLUE"
        End If
        If CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget), "0.0")) < CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget), "0.0")) Then
            drA.Item("Color1") = "RED"
            drC.Item("Color1") = "RED"
        ElseIf CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget), "0.0")) > CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget), "0.0")) Then
            drA.Item("Color1") = "GREEN"
            drC.Item("Color1") = "GREEN"
        Else
            drA.Item("Color1") = "BLUE"
            drC.Item("Color1") = "BLUE"
        End If
        If CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteThirdTarget), "0.0")) < CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateThirdTarget), "0.0")) Then
            drA.Item("Color2") = "RED"
            drC.Item("Color2") = "RED"
        ElseIf CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteThirdTarget), "0.0")) > CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateThirdTarget), "0.0")) Then
            drA.Item("Color2") = "GREEN"
            drC.Item("Color2") = "GREEN"
        Else
            drA.Item("Color2") = "BLUE"
            drC.Item("Color2") = "BLUE"
        End If
        If CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "0.0")) < CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget), "0.0")) Then
            drA.Item("Color3") = "RED"
            drC.Item("Color3") = "RED"
        ElseIf CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "0.0")) > CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget), "0.0")) Then
            drA.Item("Color3") = "GREEN"
            drC.Item("Color3") = "GREEN"
        Else
            drA.Item("Color3") = "BLUE"
            drC.Item("Color3") = "BLUE"
        End If
        If CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteAllAdults), "0.0")) < CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateAllAdults), "0.0")) Then
            drA.Item("Color4") = "RED"
            drC.Item("Color4") = "RED"
        ElseIf CSng(Format(aSpot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteAllAdults), "0.0")) > CSng(Format(cSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateAllAdults), "0.0")) Then
            drA.Item("Color4") = "GREEN"
            drC.Item("Color4") = "GREEN"
        Else
            drA.Item("Color4") = "BLUE"
            drC.Item("Color4") = "BLUE"
        End If
    End Sub

    Private Sub cmdActualBreakMatches_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdActualBreakMatches.Click
        Saved = False
        Dim TmpActualSpot As Trinity.cActualSpot
        Dim TmpPlannedSpot As Trinity.cPlannedSpot

        For Each TmpActualSpot In Campaign.ActualSpots
            TmpActualSpot.MatchedSpot = Nothing
        Next
        For Each TmpPlannedSpot In Campaign.PlannedSpots
            TmpPlannedSpot.MatchedSpot = Nothing
        Next
        UpdateConfirmed(False, True)
        UpdateActual(False, True)
    End Sub

    Private Sub cmdImportSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImportSchedule.Click
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        ImportSchedule()
        UpdateConfirmed(False, True)
        UpdateActual(False, True)
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdAddAndMatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddAndMatch.Click
        Saved = False

        For Each TmpRow As Windows.Forms.DataGridViewRow In grdActual.SelectedRows
            If TmpRow.Tag IsNot Nothing Then
                If TmpRow.Visible = True Then
                    Dim TmpActualSpot As Trinity.cActualSpot = Campaign.ActualSpots(TmpRow.Tag.Item("ID"))

                    With Campaign.PlannedSpots.Add
                        .MainObject = Campaign
                        .AirDate = TmpActualSpot.AirDate
                        .Bookingtype = TmpActualSpot.Bookingtype
                        .Channel = TmpActualSpot.Channel
                        .ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = TmpActualSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget)
                        .Filmcode = TmpActualSpot.Filmcode
                        .MaM = TmpActualSpot.MaM
                        .MatchedSpot = TmpActualSpot
                        TmpActualSpot.MatchedSpot = Campaign.PlannedSpots(.ID)
                        .MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = TmpActualSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget)
                        .Advertiser = TmpActualSpot.Advertiser
                        .Product = TmpActualSpot.Product
                        .ProgAfter = TmpActualSpot.ProgAfter
                        .ProgBefore = TmpActualSpot.ProgBefore
                        .Programme = TmpActualSpot.Programme
                        .SpotLength = TmpActualSpot.SpotLength
                        .Week = TmpActualSpot.Week
                    End With
                End If
            End If
        Next

        UpdateConfirmed(False, True)
        UpdateActual(False, True)

    End Sub

    Private Sub cmdCreateRBS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreateRBS.Click
        frmCreateRBS.ShowDialog()
        UpdateConfirmed(False, True)
    End Sub

    Private Sub cmdConfirmedEstimate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConfirmedEstimate.Click
        Dim list As New List(Of String)
        For Each TmpRow As System.Windows.Forms.DataGridViewRow In grdConfirmed.Rows
            If TmpRow.Visible AndAlso TmpRow.Tag IsNot Nothing Then
                list.Add(TmpRow.Tag.Item("ID"))
            End If
        Next
        Dim frmDlg As New frmEstimation(list)
        frmDlg.ShowDialog()
        UpdateConfirmed(False, True)
    End Sub

    Private Sub cmdBookingtype_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBookingtype.Click
        Saved = False
    End Sub

    Private Sub cmdFilm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFilm.Click
        Saved = False
    End Sub

    Private Sub ToolStripButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton7.Click
        Saved = False
    End Sub

    Private Sub grdActual_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdActual.MouseUp
        'Dim Est As Single = 0
        'Dim MyEst As Single = 0
        'Dim Actual As Single = 0
        'Dim PT As Trinity.cPlannedSpot.PlannedTargetEnum
        'Dim AT As Trinity.cActualSpot.ActualTargetEnum

        'If cmbTarget.SelectedIndex = ActiveTargetEnum.eMainTarget Then
        '    PT = Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget
        '    AT = Trinity.cActualSpot.ActualTargetEnum.ateMainTarget
        'ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eSecondaryTarget Then
        '    PT = Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget
        '    AT = Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget
        'ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eThirdTarget Then
        '    PT = Trinity.cPlannedSpot.PlannedTargetEnum.pteThirdTarget
        '    AT = Trinity.cActualSpot.ActualTargetEnum.ateThirdTarget
        'ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eBuyingTarget Then
        '    PT = Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget
        '    AT = Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget
        'ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eAllAdults Then
        '    PT = Trinity.cPlannedSpot.PlannedTargetEnum.pteAllAdults
        '    AT = Trinity.cActualSpot.ActualTargetEnum.ateAllAdults
        'End If
        'For Each TmpRow As Windows.Forms.DataGridViewRow In grdActual.SelectedRows
        '    Dim TmpSpot As Trinity.cActualSpot = TmpRow.Tag
        '    If Not TmpSpot Is Nothing Then
        '        If Not TmpSpot.MatchedSpot Is Nothing Then
        '            MyEst += TmpSpot.MatchedSpot.MyRating(PT)
        '            Est += TmpSpot.MatchedSpot.ChannelRating(PT)
        '        End If
        '        Actual += TmpSpot.Rating(AT)
        '    End If
        'Next
        'ToolTip.SetToolTip(grdActual, "My Est.: " & MyEst & vbCrLf & "Est.:" & vbCrLf & "Actual: " & Actual)

    End Sub

    Private Sub lblConfirmedFiltered_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblConfirmedFiltered.Click
        filteredSpotsConfirmed = New Collection
        For Each row As Windows.Forms.DataGridViewRow In grdConfirmed.Rows
            If Not row.Visible Then
                filteredSpotsConfirmed.Add(Campaign.PlannedSpots(row.Tag.Item("ID")))
            End If
        Next

        Dim frm As New frmFiltered
        frm.addCol(filteredSpotsConfirmed, True)
        frm.ShowDialog()
        UpdateConfirmed(False, True)
        UpdateActual(False, True)
    End Sub

    Private Sub lblActualFiltered_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblActualFiltered.Click
        filteredSpotsActual = New Collection
        For Each row As Windows.Forms.DataGridViewRow In grdActual.Rows
            If Not row.Visible Then
                filteredSpotsActual.Add(Campaign.ActualSpots(row.Tag.Item("ID")))
            End If
        Next

        Dim frm As New frmFiltered
        frm.addCol(filteredSpotsActual, False)
        frm.ShowDialog()
        UpdateConfirmed(False, True)
        UpdateActual(False, True)
    End Sub

    Sub mouse_enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblActualFiltered.MouseEnter, lblConfirmedFiltered.MouseEnter
        Me.Cursor = Windows.Forms.Cursors.Hand
    End Sub

    Sub mouse_leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblActualFiltered.MouseLeave, lblConfirmedFiltered.MouseLeave
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdActualBreakMatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdActualBreakMatch.Click
        Saved = False
        Dim spot As Trinity.cActualSpot
        Dim spot2 As Trinity.cPlannedSpot
        Dim _plannedScroll As Integer = grdConfirmed.FirstDisplayedScrollingRowIndex
        Dim _actualScroll As Integer = grdActual.FirstDisplayedScrollingRowIndex
        For Each row As System.Windows.Forms.DataGridViewRow In grdActual.SelectedRows

            If row.Tag IsNot Nothing Then ' If the tag is nothing it either means its invalid or it is the summary row
                'Get the object of the actual spot in the campaign
                spot = Campaign.ActualSpots(row.Tag.Item("ID"))
                'See if this spot has been matched with a planned spot
                If Not spot.MatchedSpot Is Nothing Then
                    'If so, get the object of the planned spot
                    spot2 = spot.MatchedSpot

                    'Now we manipulate the data of the acutal spots directly through the tag element of the row in which it exists
                    'grdActuals uses VirtualMode, so will get it's data from the tag when CellValueNeeded is triggered through the update
                    row.Tag.Item("MatchedSpotID") = ""
                    row.Tag.Item("Color0") = ""
                    row.Tag.Item("Color1") = ""
                    row.Tag.Item("Color2") = ""
                    row.Tag.Item("Color3") = ""
                    row.Tag.Item("Color4") = ""
                    row.Tag.Item("Gross CPP") = CDbl("0 kr")
                    row.Tag.Item("CPP(Chn Est)") = CDbl("0 kr")
                    row.Tag.Item("CPP (" & Campaign.MainTarget.TargetNameNice & ")") = CDbl("0 kr")
                    row.Tag.Item("Net Price") = CDbl("0 kr")
                    row.Tag.Item("My Est0") = 0 ' CDbl("")
                    row.Tag.Item("My Est1") = 0 ' CDbl("")
                    row.Tag.Item("My Est2") = 0 ' CDbl("")
                    row.Tag.Item("My Est3") = 0 ' CDbl("")
                    row.Tag.Item("My Est4") = 0 ' CDbl("")
                    row.Tag.Item("Chan Est0") = 0 ' ""
                    row.Tag.Item("Chan Est1") = 0 ' ""
                    row.Tag.Item("Chan Est2") = 0 ' ""
                    row.Tag.Item("Chan Est3") = 0 ' ""
                    row.Tag.Item("Chan Est4") = 0 ' ""

                    Dim dt As DataRow = dtConfirmed.Select("ID ='" & spot2.ID & "'")(0)
                    dt.Item("Color0") = ""
                    dt.Item("Color1") = ""
                    dt.Item("Color2") = ""
                    dt.Item("Color3") = ""
                    dt.Item("Color4") = ""
                    dt.Item("Actual0") = 0 '""
                    dt.Item("Actual1") = 0 ' ""
                    dt.Item("Actual2") = 0 ' ""
                    dt.Item("Actual3") = 0 ' ""
                    dt.Item("Actual4") = 0 ' ""

                    spot.MatchedSpot = Nothing
                    spot2.MatchedSpot = Nothing
                End If
            End If
        Next
        UpdateConfirmed(False, True)
        UpdateActual(False, True)
        grdConfirmed.FirstDisplayedScrollingRowIndex = _plannedScroll
        grdActual.FirstDisplayedScrollingRowIndex = _actualScroll
    End Sub

    Private Sub cmdBreakMatch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBreakMatch.Click
        Saved = False
        Dim spot As Trinity.cPlannedSpot
        Dim spot2 As Trinity.cActualSpot
        Dim _plannedScroll As Integer = grdConfirmed.FirstDisplayedScrollingRowIndex
        Dim _actualScroll As Integer = grdActual.FirstDisplayedScrollingRowIndex
        For Each row As System.Windows.Forms.DataGridViewRow In grdConfirmed.SelectedRows
            If row.Tag IsNot Nothing Then
                spot = Campaign.PlannedSpots(row.Tag.Item("ID"))
                If Not spot.MatchedSpot Is Nothing Then
                    spot2 = spot.MatchedSpot
                    spot.MatchedSpot = Nothing
                    spot2.MatchedSpot = Nothing

                    row.Tag.Item("Color0") = ""
                    row.Tag.Item("Color1") = ""
                    row.Tag.Item("Color2") = ""
                    row.Tag.Item("Color3") = ""
                    row.Tag.Item("Color4") = ""
                    row.Tag.Item("Actual0") = 0
                    row.Tag.Item("Actual1") = 0
                    row.Tag.Item("Actual2") = 0
                    row.Tag.Item("Actual3") = 0
                    row.Tag.Item("Actual4") = 0

                    Dim dt As DataRow = dtActual.Select("ID='" & spot2.ID & "'")(0)
                    dt.Item("MatchedSpotID") = ""
                    dt.Item("Color0") = ""
                    dt.Item("Color1") = ""
                    dt.Item("Color2") = ""
                    dt.Item("Color3") = ""
                    dt.Item("Color4") = ""
                    dt.Item("Gross CPP") = 0
                    dt.Item("CPP(Chn Est)") = 0
                    dt.Item("CPP (" & Campaign.MainTarget.TargetNameNice & ")") = 0
                    dt.Item("Net Price") = 0
                    dt.Item("My Est0") = 0
                    dt.Item("My Est1") = 0
                    dt.Item("My Est2") = 0
                    dt.Item("My Est3") = 0
                    dt.Item("My Est4") = 0
                    dt.Item("Chan Est0") = 0
                    dt.Item("Chan Est1") = 0
                    dt.Item("Chan Est2") = 0
                    dt.Item("Chan Est3") = 0
                    dt.Item("Chan Est4") = 0
                End If
            End If
        Next
        UpdateConfirmed(False, True)
        UpdateActual(False, True)
        grdConfirmed.FirstDisplayedScrollingRowIndex = _plannedScroll
        grdActual.FirstDisplayedScrollingRowIndex = _actualScroll
    End Sub

    Private Sub cmdDeleteActual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleteActual.Click
        Saved = False
        Dim TmpRow As Windows.Forms.DataGridViewRow

        If grdActual.SelectedRows.Count = 0 Then
            Windows.Forms.MessageBox.Show("No spot selected.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If



        For Each TmpRow In grdActual.SelectedRows
            If TmpRow.Tag IsNot Nothing Then
                If TmpRow.Visible Then
                    Campaign.ActualSpots.Remove(TmpRow.Tag.Item("ID"))
                    grdActual.Rows.Remove(TmpRow)
                    'dtActual.Rows.Remove(dtActual.Rows(TmpRow.Tag.Item("ID")))
                End If
            End If
        Next
        Campaign.CreateAdedgeSpots(False)
        Campaign.CalculateSpots()
    End Sub

    Private Sub lblHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblHelp.Click
        Dim frm As New frmExplain

        Dim lbl As New System.Windows.Forms.Label
        lbl.Text = "Black text means no match."
        lbl.Width = lbl.PreferredWidth
        frm.Controls.Add(lbl)
        lbl.Top = 10
        lbl.Left = 20

        lbl = New System.Windows.Forms.Label
        lbl.Text = "Green text means the matched actual spot rated better than your estimate."
        lbl.Width = lbl.PreferredWidth
        frm.Controls.Add(lbl)
        lbl.ForeColor = Color.Green
        lbl.Top = 40
        lbl.Left = 20

        lbl = New System.Windows.Forms.Label
        lbl.Text = "Red text means the matched actual spot rated lower than your estimate."
        lbl.Width = lbl.PreferredWidth
        frm.Controls.Add(lbl)
        lbl.ForeColor = Color.Red
        lbl.Top = 70
        lbl.Left = 20

        lbl = New System.Windows.Forms.Label
        lbl.Text = "Blue text means the matched actual spot rated as you estimated."
        lbl.Width = lbl.PreferredWidth
        frm.Controls.Add(lbl)
        lbl.ForeColor = Color.Blue
        lbl.Top = 100
        lbl.Left = 20

        frm.Width = 400
        frm.Height = 200
        frm.ShowDialog()
    End Sub

    Private Sub cmdFilmcodeFound_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFilmcodeFound.Click
        MsgBox(cmdFilmcodeFound.Tag, MsgBoxStyle.Information, "Filmcode not found")
    End Sub

    Private Sub checkFilmCodes()
        'we check if all filmcodes are represented in the actual spot list

        Dim bolFound As Boolean

        For Each c As Trinity.cChannel In Campaign.Channels
            For Each bt As Trinity.cBookingType In c.BookingTypes
                If bt.BookIt Then
                    Dim w As Trinity.cWeek = bt.Weeks(1)
                    'only continue if we could have data from the week
                    If w.StartDate < Campaign.UpdatedTo Then
                        For Each f As Trinity.cFilm In w.Films
                            bolFound = False
                            Dim _bt = bt
                            'check if the filmcode is represented in the actual spots
                            For Each spot As Trinity.cActualSpot In From _spot As Trinity.cActualSpot In Campaign.ActualSpots Where _spot.Bookingtype Is _bt
                                Dim filmcodes() As String = f.Filmcode.Split(",")
                                For Each fc As String In filmcodes
                                    If fc.ToUpper = spot.Filmcode.ToUpper Then
                                        bolFound = True
                                        Exit For
                                    End If
                                Next
                            Next

                            If Not bolFound Then
                                cmdFilmcodeFound.Enabled = True
                                cmdFilmcodeFound.Tag = cmdFilmcodeFound.Tag & vbCrLf & c.ChannelName & ":" & f.Filmcode.Trim(",") & " was not found"
                            End If

                        Next
                    End If

                    Exit For 'we exit the for loop with BT
                End If
            Next
        Next
    End Sub

    Private Sub cmdFilmcodeFound_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFilmcodeFound.EnabledChanged
        If cmdFilmcodeFound.Enabled Then
            Dim lbl As New System.Windows.Forms.Label
            lbl.Text = "Filmcode not found"
            lbl.BackColor = Color.LightYellow
            lbl.BorderStyle = Windows.Forms.BorderStyle.FixedSingle

            lbl.Width = lbl.PreferredWidth + 20

            Me.Controls.Add(lbl)
            lbl.Left = grdConfirmed.Left + grdConfirmed.Width - 140
            lbl.Top = grdConfirmed.Top + grdConfirmed.Height - 5
            lbl.BringToFront()

            AddHandler lbl.Click, AddressOf Me.lblclick

        End If
    End Sub

    Private Sub lblclick(ByVal sender As Object, ByVal e As System.EventArgs)
        DirectCast(sender, System.Windows.Forms.Label).Dispose()
    End Sub

    Public Sub New()

        'Dim TmpRow As Windows.Forms.DataGridViewRow
        'For Each TmpRow In grdActual.SelectedRows
        '    If TmpRow.Tag Is Nothing Then
        '        cmdDeleteActual.Enabled = False
        '    End If
        'Next

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        styleNoMatch = New Windows.Forms.DataGridViewCellStyle
        styleMatchGoodRating = New Windows.Forms.DataGridViewCellStyle
        styleMatchBadRating = New Windows.Forms.DataGridViewCellStyle
        styleMatchOnRating = New Windows.Forms.DataGridViewCellStyle

        styleMatchGoodRating.ForeColor = Color.Green
        styleMatchBadRating.ForeColor = Color.Red
        styleMatchOnRating.ForeColor = Color.Blue
    End Sub

    Private Sub grdActual_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdActual.CellValueNeeded
        If e.ColumnIndex < 0 Then Exit Sub
        If e.RowIndex < 0 Then Exit Sub

        If e.RowIndex = grdActual.Rows.Count - 1 Then
            Dim _column As String = ""
            Select Case grdActual.Columns(e.ColumnIndex).Name
                Case "colActualGross Price"
                    _column = "Gross Price"
                Case "colActualNet Price"
                    _column = "Net Price"
                Case "colActualMy Est"
                    If cmbTarget.SelectedIndex = ActiveTargetEnum.eMainTarget Then
                        _column = "My Est0"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eSecondaryTarget Then
                        _column = "My Est1"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eThirdTarget Then
                        _column = "My Est2"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eBuyingTarget Then
                        _column = "My Est3"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eAllAdults Then
                        _column = "My Est4"
                    End If
                Case "colActualChan Est"
                    _column = "Chan Est3"
                Case "colActualActual"
                    If cmbTarget.SelectedIndex = ActiveTargetEnum.eMainTarget Then
                        _column = "Actual0"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eSecondaryTarget Then
                        _column = "Actual1"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eThirdTarget Then
                        _column = "Actual2"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eBuyingTarget Then
                        _column = "Actual3"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eAllAdults Then
                        _column = "Actual4"
                    End If
                Case "colActualActual Buying"
                    _column = "Actual3"

                Case "colActualNet value"
                    _column = "Net Value"
                Case Else
                    _column = ""
            End Select
            If _column = "" Then
                e.Value = ""
            Else
                Dim _sum = (From _row As Windows.Forms.DataGridViewRow In grdActual.Rows Where _row.Tag IsNot Nothing AndAlso _row.Visible = True Select CSng(DirectCast(_row.Tag, DataRowView).Item(_column))).Sum
                e.Value = _sum
            End If

        Else
            Dim item As DataRowView = grdActual.Rows(e.RowIndex).Tag
            Select Case grdActual.Columns(e.ColumnIndex).Name
                Case "colActualDate"
                    e.Value = item.Item("Date")
                Case "colActualWeek"
                    e.Value = item.Item("Week")
                Case "colActualWeekday"
                    e.Value = item.Item("Weekday")
                Case "colActualTime"
                    e.Value = item.Item("Time")
                Case "colActualChannel"
                    e.Value = item.Item("Channel")
                Case "colActualBookingtype"
                    e.Value = item.Item("Bookingtype")
                Case "colActualProgram"
                    e.Value = item.Item("Program")
                Case "colActualGross Price"
                    e.Value = item.Item("Gross Price")
                Case "colActualNet Price"
                    e.Value = item.Item("Net Price")
                Case "colActualGross CPP"
                    e.Value = item.Item("Gross CPP")
                Case "colActualCPP (" & Campaign.MainTarget.TargetNameNice & ")"
                    e.Value = item.Item("CPP (" & Campaign.MainTarget.TargetNameNice & ")")
                Case "colActualCPP (Chn Est)"
                    e.Value = item.Item("CPP(Chn Est)")
                Case "colActualMy Est"
                    If cmbTarget.SelectedIndex = ActiveTargetEnum.eMainTarget Then
                        e.Value = item.Item("My Est0")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eSecondaryTarget Then
                        e.Value = item.Item("My Est1")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eThirdTarget Then
                        e.Value = item.Item("My Est2")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eBuyingTarget Then
                        e.Value = item.Item("My Est3")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eAllAdults Then
                        e.Value = item.Item("My Est4")
                    End If
                Case "colActualChan Est"
                    'Changed this so that only the channel's estimate in it's own selling group is shown

                    'If cmbTarget.SelectedIndex = ActiveTargetEnum.eMainTarget Then
                    'e.Value = item.Item("Chan Est0")
                    ' ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eSecondaryTarget Then
                    ' e.Value = item.Item("Chan Est1")
                    '  ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eThirdTarget Then
                    '  e.Value = item.Item("Chan Est2")
                    ' ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eBuyingTarget Then
                    e.Value = item.Item("Chan Est3")
                    '  ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eAllAdults Then
                    '  e.Value = item.Item("Chan Est4")
                    '   End If
                Case "colActualActual"
                    If cmbTarget.SelectedIndex = ActiveTargetEnum.eMainTarget Then
                        e.Value = item.Item("Actual0")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eSecondaryTarget Then
                        e.Value = item.Item("Actual1")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eThirdTarget Then
                        e.Value = item.Item("Actual2")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eBuyingTarget Then
                        e.Value = item.Item("Actual3")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eAllAdults Then
                        e.Value = item.Item("Actual4")
                    End If
                Case "colActualActual Buying"
                    e.Value = item.Item("Actual3")
                Case "colActualPIB"
                    e.Value = item.Item("PIB")
                Case "colActualNet value"
                    e.Value = item.Item("Net Value")
                Case "colActualFilmcode"
                    e.Value = item.Item("Filmcode")
                Case "colActualFilm"
                    e.Value = item.Item("Film")
                Case "colActualFilm dscr"
                    e.Value = item.Item("Film dscr")
                Case "colActualSC"
                    e.Value = item.Item("SC")
                Case "colActualRemarks"
                    e.Value = item.Item("Remarks")
                Case "colActualAdded value"
                    e.Value = item.Item("Added Value")
                Case "colActualProg Before"
                    e.Value = item.Item("Prog Before")
                Case "colActualProg After"
                    e.Value = item.Item("Prog After")
            End Select

            Select Case item.Item("Color" & cmbTarget.SelectedIndex.ToString)
                Case Is = "RED"
                    grdActual.Rows(e.RowIndex).DefaultCellStyle = styleMatchBadRating
                Case Is = "GREEN"
                    grdActual.Rows(e.RowIndex).DefaultCellStyle = styleMatchGoodRating
                Case Is = "BLUE"
                    grdActual.Rows(e.RowIndex).DefaultCellStyle = styleMatchOnRating
                Case Is = ""
                    grdActual.Rows(e.RowIndex).DefaultCellStyle = styleNoMatch
            End Select

        End If

    End Sub

    Private Sub grdConfirmed_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdConfirmed.CellValueNeeded
        If e.ColumnIndex < 0 Then Exit Sub
        If e.RowIndex < 0 Then Exit Sub
        'If e.RowIndex = 10 Then
        ' Stop
        'End If
        Dim item As DataRowView = grdConfirmed.Rows(e.RowIndex).Tag
        If item IsNot Nothing Then
            If item.Item("Program").ToString.ToUpper.Contains("SALT") AndAlso grdConfirmed.Columns(e.ColumnIndex).Name = "colConfirmedFilmcode" Then
                ' Stop
            End If
            Select Case grdConfirmed.Columns(e.ColumnIndex).Name
                Case "colConfirmedDate"
                    'e.Value = dtConfirmed.Rows(grdConfirmed.Rows(e.RowIndex).Tag).Item("Date")
                    e.Value = item.Item("Date")
                Case "colConfirmedWeek"
                    e.Value = item.Item("Week")
                Case "colConfirmedWeekday"
                    e.Value = item.Item("Weekday")
                Case "colConfirmedTime"
                    e.Value = item.Item("Time")
                Case "colConfirmedChannel"
                    e.Value = item.Item("Channel")
                Case "colConfirmedBookingtype"
                    e.Value = item.Item("Bookingtype")
                Case "colConfirmedProgram"
                    e.Value = item.Item("Program")
                Case "colConfirmedGross Price"
                    e.Value = item.Item("Gross Price")
                Case "colConfirmedMy Est"
                    If cmbTarget.SelectedIndex = ActiveTargetEnum.eMainTarget Then
                        e.Value = item.Item("My Est0")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eSecondaryTarget Then
                        e.Value = item.Item("My Est1")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eThirdTarget Then
                        e.Value = item.Item("My Est2")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eBuyingTarget Then
                        e.Value = item.Item("My Est3")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eAllAdults Then
                        e.Value = item.Item("My Est4")
                    End If
                Case "colConfirmedChan Est"
                    'Only channel estimate in its selling target shown for less confusion

                    'If cmbTarget.SelectedIndex = ActiveTargetEnum.eMainTarget Then
                    'e.Value = item.Item("Chan Est0")
                    'ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eSecondaryTarget Then
                    ' e.Value = item.Item("Chan Est1")
                    ' ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eThirdTarget Then
                    ' e.Value = item.Item("Chan Est2")
                    ' ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eBuyingTarget Then
                    e.Value = item.Item("Chan Est3")
                    'ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eAllAdults Then
                    'e.Value = item.Item("Chan Est4")
                    'End If
                Case "colConfirmedNet Price"
                    e.Value = item.Item("Net Price")
                Case "colConfirmedRemarks"
                    e.Value = item.Item("Remarks")
                Case "colConfirmedGross CPP"
                    e.Value = item.Item("Gross CPP")
                Case "colConfirmedCPP (" & Campaign.MainTarget.TargetNameNice & ")"
                    e.Value = item.Item("CPP (" & Campaign.MainTarget.TargetNameNice & ")")
                Case "colConfirmedCPP (Chn Est)"
                    e.Value = item.Item("CPP(Chn Est)")
                Case "colConfirmedDateMy Est"
                    If cmbTarget.SelectedIndex = ActiveTargetEnum.eMainTarget Then
                        e.Value = item.Item("My Est0")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eSecondaryTarget Then
                        e.Value = item.Item("My Est1")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eThirdTarget Then
                        e.Value = item.Item("My Est2")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eBuyingTarget Then
                        e.Value = item.Item("My Est3")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eAllAdults Then
                        e.Value = item.Item("My Est4")
                    End If
                Case "colConfirmedActual"
                    If cmbTarget.SelectedIndex = ActiveTargetEnum.eMainTarget Then
                        e.Value = item.Item("Actual0")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eSecondaryTarget Then
                        e.Value = item.Item("Actual1")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eThirdTarget Then
                        e.Value = item.Item("Actual2")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eBuyingTarget Then
                        e.Value = item.Item("Actual3")
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eAllAdults Then
                        e.Value = item.Item("Actual4")
                    End If
                Case "colConfirmedFilmcode"
                    e.Value = item.Item("Filmcode")
                Case "colConfirmedFilm"
                    e.Value = item.Item("Film")
                Case "colConfirmedFilm dscr"
                    e.Value = item.Item("Film dscr")
                Case "colConfirmedDuration"
                    e.Value = item.Item("Duration")
                Case "colConfirmedAdded value"
                    e.Value = item.Item("Added Value")
                Case "colConfirmedProg Before"
                    e.Value = item.Item("Prog Before")
                Case "colConfirmedProg After"
                    e.Value = item.Item("Prog After")
            End Select


            Select Case item.Item("Color" & cmbTarget.SelectedIndex.ToString)
                Case Is = "RED"
                    grdConfirmed.Rows(e.RowIndex).DefaultCellStyle = styleMatchBadRating
                Case Is = "GREEN"
                    grdConfirmed.Rows(e.RowIndex).DefaultCellStyle = styleMatchGoodRating
                Case Is = "BLUE"
                    grdConfirmed.Rows(e.RowIndex).DefaultCellStyle = styleMatchOnRating
                Case Is = ""
                    grdConfirmed.Rows(e.RowIndex).DefaultCellStyle = styleNoMatch
            End Select
        Else
            Dim _column As String = ""
            Select Case grdConfirmed.Columns(e.ColumnIndex).Name
                Case "colConfirmedGross Price"
                    _column = "Gross Price"
                Case "colConfirmedMy Est"
                    If cmbTarget.SelectedIndex = ActiveTargetEnum.eMainTarget Then
                        _column = "My Est0"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eSecondaryTarget Then
                        _column = "My Est1"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eThirdTarget Then
                        _column = "My Est2"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eBuyingTarget Then
                        _column = "My Est3"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eAllAdults Then
                        _column = "My Est4"
                    End If
                Case "colConfirmedChan Est"
                    'Only channel estimate in its selling target shown for less confusion

                    'If cmbTarget.SelectedIndex = ActiveTargetEnum.eMainTarget Then
                    '_column="Chan Est0"
                    'ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eSecondaryTarget Then
                    ' _column="Chan Est1"
                    ' ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eThirdTarget Then
                    ' _column="Chan Est2"
                    ' ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eBuyingTarget Then
                    _column = "Chan Est3"
                    'ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eAllAdults Then
                    '_column="Chan Est4"
                    'End If
                Case "colConfirmedNet Price"
                    _column = "Net Price"
                Case "colConfirmedDateMy Est"
                    If cmbTarget.SelectedIndex = ActiveTargetEnum.eMainTarget Then
                        _column = "My Est0"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eSecondaryTarget Then
                        _column = "My Est1"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eThirdTarget Then
                        _column = "My Est2"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eBuyingTarget Then
                        _column = "My Est3"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eAllAdults Then
                        _column = "My Est4"
                    End If
                Case "colConfirmedActual"
                    If cmbTarget.SelectedIndex = ActiveTargetEnum.eMainTarget Then
                        _column = "Actual0"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eSecondaryTarget Then
                        _column = "Actual1"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eThirdTarget Then
                        _column = "Actual2"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eBuyingTarget Then
                        _column = "Actual3"
                    ElseIf cmbTarget.SelectedIndex = ActiveTargetEnum.eAllAdults Then
                        _column = "Actual4"
                    End If
            End Select
            If _column = "" Then
                e.Value = ""
            Else
                Dim _sum = (From _row As Windows.Forms.DataGridViewRow In grdConfirmed.Rows Where _row.Tag IsNot Nothing AndAlso _row.Visible = True Select CSng(DirectCast(_row.Tag, DataRowView).Item(_column))).Sum
                e.Value = _sum
            End If
        End If
    End Sub

    Private Sub frmSpots_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmbTarget.SelectedIndex = ActiveTarget

        UpdateConfirmed(True, True)
        UpdateActual(True, True)
    End Sub

    Private Sub grdConfirmed_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdConfirmed.ColumnHeaderMouseClick
        Dim sortValue As String = grdConfirmed.Columns(e.ColumnIndex).HeaderText

        'we sort ASC by default
        'we can sort by 2 columns
        If strSortConfirmed = "" Then
            strSortConfirmed = sortValue & " " & "ASC"
        Else
            'if true we have more than one column sorted already
            If strSortConfirmed.IndexOf(",") = -1 Then
                If strSortConfirmed = (sortValue & " " & "ASC") OrElse strSortConfirmed = (sortValue & " " & "DESC") Then
                    'if its sorted ASC we set DESC and vice versa
                    If InStr(strSortConfirmed, "ASC") > 0 Then
                        strSortConfirmed = strSortConfirmed.Replace("ASC", "DESC")
                    Else
                        strSortConfirmed = strSortConfirmed.Replace("DESC", "ASC")
                    End If
                Else
                    strSortConfirmed = sortValue & " " & "ASC" & "," & strSortConfirmed
                End If
            Else
                'need to get the latest added sort value
                Dim oldSort As String = strSortConfirmed.Substring(0, strSortConfirmed.IndexOf(","))
                If oldSort = (sortValue & " " & "ASC") OrElse oldSort = (sortValue & " " & "DESC") Then
                    'if its sorted ASC we set DESC and vice versa
                    If InStr(oldSort, "ASC") > 0 Then
                        oldSort = oldSort.Replace("ASC", "DESC")
                    Else
                        oldSort = oldSort.Replace("DESC", "ASC")
                    End If

                    strSortConfirmed = oldSort & "," & strSortConfirmed.Substring(strSortConfirmed.IndexOf(",") + 1)
                Else
                    strSortConfirmed = sortValue & " " & "ASC" & "," & oldSort
                End If
            End If
        End If

        Select Case cmbTarget.SelectedIndex
            Case Is = 0
                sortValue = strSortConfirmed.Replace("My Est", "My Est0")
                sortValue = sortValue.Replace("Chan Est", "Chan Est0")
                sortValue = sortValue.Replace("Actual", "Actual0")
            Case Is = 1
                sortValue = strSortConfirmed.Replace("My Est", "My Est1")
                sortValue = sortValue.Replace("Chan Est", "Chan Est1")
                sortValue = sortValue.Replace("Actual", "Actual1")
            Case Is = 2
                sortValue = strSortConfirmed.Replace("My Est", "My Est2")
                sortValue = sortValue.Replace("Chan Est", "Chan Est2")
                sortValue = sortValue.Replace("Actual", "Actual2")
            Case Is = 3
                sortValue = strSortConfirmed.Replace("My Est", "My Est3")
                sortValue = sortValue.Replace("Chan Est", "Chan Est3")
                sortValue = sortValue.Replace("Actual", "Actual3")
            Case Is = 4
                sortValue = strSortConfirmed.Replace("My Est", "My Est4")
                sortValue = sortValue.Replace("Chan Est", "Chan Est4")
                sortValue = sortValue.Replace("Actual", "Actual4")
        End Select
        dtConfirmed.DefaultView.Sort = sortValue

        UpdateConfirmed(False, False)

    End Sub

    Private Sub grdActual_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdActual.ColumnHeaderMouseClick
        Dim sortValue As String = grdActual.Columns(e.ColumnIndex).HeaderText

        'we sort ASC by default
        'we can sort by 2 columns
        If strSortActual = "" Then
            strSortActual = sortValue & " " & "ASC"
        Else
            'if true we have more than one column sorted already
            If strSortActual.IndexOf(",") = -1 Then
                If strSortActual = (sortValue & " " & "ASC") OrElse strSortActual = (sortValue & " " & "DESC") Then
                    'if its sorted ASC we set DESC and vice versa
                    If InStr(strSortActual, "ASC") > 0 Then
                        strSortActual = strSortActual.Replace("ASC", "DESC")
                    Else
                        strSortActual = strSortActual.Replace("DESC", "ASC")
                    End If
                Else
                    strSortActual = sortValue & " " & "ASC" & "," & strSortActual
                End If
            Else
                'need to get the latest added sort value
                Dim oldSort As String = strSortActual.Substring(0, strSortActual.IndexOf(","))
                If oldSort = (sortValue & " " & "ASC") OrElse oldSort = (sortValue & " " & "DESC") Then
                    'if its sorted ASC we set DESC and vice versa
                    If InStr(oldSort, "ASC") > 0 Then
                        oldSort = oldSort.Replace("ASC", "DESC")
                    Else
                        oldSort = oldSort.Replace("DESC", "ASC")
                    End If

                    strSortActual = oldSort & "," & strSortActual.Substring(strSortActual.IndexOf(",") + 1)
                Else
                    strSortActual = sortValue & " " & "ASC" & "," & oldSort
                End If
            End If
        End If

        Select Case cmbTarget.SelectedIndex
            Case Is = 0
                sortValue = strSortActual.Replace("My Est", "My Est0")
                sortValue = sortValue.Replace("Chan Est", "Chan Est0")
                sortValue = sortValue.Replace("Actual", "Actual0")
            Case Is = 1
                sortValue = strSortActual.Replace("My Est", "My Est1")
                sortValue = sortValue.Replace("Chan Est", "Chan Est1")
                sortValue = sortValue.Replace("Actual", "Actual1")
            Case Is = 2
                sortValue = strSortActual.Replace("My Est", "My Est2")
                sortValue = sortValue.Replace("Chan Est", "Chan Est2")
                sortValue = sortValue.Replace("Actual", "Actual2")
            Case Is = 3
                sortValue = strSortActual.Replace("My Est", "My Est3")
                sortValue = sortValue.Replace("Chan Est", "Chan Est3")
                sortValue = sortValue.Replace("Actual", "Actual3")
            Case Is = 4
                sortValue = strSortActual.Replace("My Est", "My Est4")
                sortValue = sortValue.Replace("Chan Est", "Chan Est4")
                sortValue = sortValue.Replace("Actual", "Actual4")
        End Select
        dtActual.DefaultView.Sort = sortValue

        UpdateActual(False, False)
    End Sub

    Private Sub grdConfirmed_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdConfirmed.CellValuePushed
        If grdConfirmed.Columns(e.ColumnIndex).Tag = "My Est" Then
            grdConfirmed.Rows(e.RowIndex).Tag.Item("My Est" & cmbTarget.SelectedIndex) = e.Value

            Select Case cmbTarget.SelectedIndex
                Case 0
                    Campaign.PlannedSpots(grdConfirmed.Rows(e.RowIndex).Tag.Item("ID")).MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = e.Value
                Case 1
                    Campaign.PlannedSpots(grdConfirmed.Rows(e.RowIndex).Tag.Item("ID")).MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget) = e.Value
                Case 2
                    Campaign.PlannedSpots(grdConfirmed.Rows(e.RowIndex).Tag.Item("ID")).MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteThirdTarget) = e.Value
                Case 3
                    Campaign.PlannedSpots(grdConfirmed.Rows(e.RowIndex).Tag.Item("ID")).MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) = e.Value
                Case 4
                    Campaign.PlannedSpots(grdConfirmed.Rows(e.RowIndex).Tag.Item("ID")).MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteAllAdults) = e.Value
            End Select
        End If
    End Sub

    Private Sub grdActual_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdActual.CellFormatting
        Dim item As DataRowView = grdActual.Rows(e.RowIndex).Tag
        If e.RowIndex = grdActual.Rows.Count - 1 Then
            e.CellStyle.BackColor = Color.DarkGray
            Exit Sub
        End If

        If Not Campaign.ActualSpots(item!ID).Bookingtype.BookIt Then
            e.CellStyle.BackColor = Color.Red
        End If

    End Sub

    Private Sub grdActual_CellMouseEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdActual.CellMouseEnter
        If e.RowIndex < 0 Then Exit Sub
        Dim item As DataRowView = grdActual.Rows(e.RowIndex).Tag
        If item Is Nothing Then Exit Sub

        If Not Campaign.ActualSpots(item!ID).Bookingtype.BookIt Then
            ToolTip.Show("This bookingtype has not been chosen in Setup!", Me, New Point(grdActual.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, False).Left, grdActual.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, False).Top + grdActual.Top + SplitContainer1.Panel2.Top + SplitContainer1.Top + ToolStrip2.Height + 30))
        End If

    End Sub

    Private Sub grdActual_CellMouseLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdActual.CellMouseLeave
        If e.RowIndex < 0 Then Exit Sub
        Dim item As DataRowView = grdActual.Rows(e.RowIndex).Tag
        If item Is Nothing Then Exit Sub

        If Not Campaign.ActualSpots(item!ID).Bookingtype.BookIt Then
            ToolTip.Hide(Me)
        End If
    End Sub


    Private Sub grdActual_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdActual.CellMouseClick

        If e.RowIndex < 0 OrElse e.RowIndex = grdActual.Rows.Count - 1 Then Exit Sub

        Dim toolTipText As String

        If Not Campaign.ActualSpots(grdActual.Rows(e.RowIndex).Tag.Item("ID")).MatchedSpot Is Nothing Then
            With Campaign.ActualSpots(grdActual.Rows(e.RowIndex).Tag.Item("ID")).MatchedSpot
                toolTipText = "Matched to "
                toolTipText = toolTipText & .Programme
                toolTipText = toolTipText & " at " & Trinity.Helper.Mam2Tid(.MaM).ToString
            End With

            ToolTip.Show(toolTipText, Me, Cursor.Position, 2000)
        Else
            ToolTip.Show("Not matched", Me, Cursor.Position, 2000)
        End If

    End Sub


    Private Sub cmdIgnoreFaultySpots_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdIgnoreFaultySpots.Click
        filmcodeFilters = Not filmcodeFilters
        UpdateConfirmed(False, True)
        UpdateActual(False, True)

    End Sub

    Private Sub grdActual_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdActual.CellContentClick

    End Sub

    Private Sub cmdCheckDuplicates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheckDuplicates.Click
        For Each row As Windows.Forms.DataGridViewRow In grdConfirmed.Rows
            If row.Tag IsNot Nothing Then
                Dim spot As DataRowView = row.Tag
                For Each row2 As Windows.Forms.DataGridViewRow In grdConfirmed.Rows
                    If row2.Tag IsNot Nothing Then
                        'row2.DefaultCellStyle.BackColor = Color.Aqua
                        Dim spot2 As DataRowView = row2.Tag
                        'If spot.Item("ID") <> spot2.Item("ID") Then
                        If Campaign.PlannedSpots(spot.Item("ID")).Channel.ChannelName = Campaign.PlannedSpots(spot2.Item("ID")).Channel.ChannelName Then
                            If Campaign.PlannedSpots(spot.Item("ID")).AirDate = Campaign.PlannedSpots(spot2.Item("ID")).AirDate Then
                                If Math.Abs(Campaign.PlannedSpots(spot.Item("ID")).MaM - Campaign.PlannedSpots(spot2.Item("ID")).MaM) > 15 Then
                                    'If DateDiff(DateInterval.Minute, CDate(spot.Item("Time")), CDate(spot2.Item("Time"))) >= 15 Then
                                    row.Visible = False
                                Else
                                    Debug.Print(spot.Item(1) & " dubbelt med " & spot2.Item(1))
                                End If
                            End If
                        End If
                    End If
                    ' End If
                Next
            End If

        Next
    End Sub

    Private Sub cmdImportActualSpots_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmdCreateActualsFromPlanned_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveUnmatchedSpots.Click
        If (From Spot As Trinity.cPlannedSpot In Campaign.PlannedSpots Where Spot.MatchedSpot Is Nothing).Count > 0 Then
            If Windows.Forms.MessageBox.Show("This will delete all actual spots that are not matched with a planned spot." & vbCrLf & "There are planned spots without a matched actual." & vbCrLf & vbCrLf & "Do you want to continue?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If
        For Each _spotId As String In (From Spot As Trinity.cActualSpot In (From Spot As Trinity.cActualSpot In Campaign.ActualSpots Select Spot Where Spot.MatchedSpot Is Nothing) Select Spot.ID).ToList
            Campaign.ActualSpots.Remove(_spotId)
        Next

        Campaign.CreateAdedgeSpots()
        Campaign.CalculateSpots()

        grdActual.Columns.Clear()
        grdActual.Rows.Clear()

        UpdateConfirmed(True, True)
        UpdateActual(True, True)

    End Sub

    Private Sub grdConfirmed_ChangeUICues(sender As Object, e As System.Windows.Forms.UICuesEventArgs) Handles grdConfirmed.ChangeUICues

    End Sub

    Private Sub VOSDALToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DefaultToolStripMenuItem.Click, LiveToolStripMenuItem.Click, VOSDAL7ToolStripMenuItem.Click
        Dim _mnu As Windows.Forms.ToolStripMenuItem = sender

        Campaign.TimeShift = _mnu.Tag

        UpdateActual(True, True)
    End Sub

    Private Sub cmdTimeshift_DropDownOpening(sender As Object, e As System.EventArgs) Handles cmdTimeshift.DropDownOpening
        Dim _mnu As Windows.Forms.ToolStripDropDownButton = sender

        For Each _m As Object In _mnu.DropDownItems
            If _m.Tag IsNot Nothing AndAlso _m.Tag = Campaign.TimeShift Then
                _m.Checked = True
            ElseIf GetType(Windows.Forms.ToolStripMenuItem) Is _m.GetType Then
                _m.Checked = False
            End If
        Next
    End Sub

End Class