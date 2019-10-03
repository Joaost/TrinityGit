'REMOVE this import
Imports System.Windows.Forms
Imports System.Linq
Imports System.Xml.Linq
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Collections.Generic
Imports clTrinity.Trinity

Public Class frmBooking
    Dim pnlDetails As System.Windows.Forms.SplitterPanel
    Dim pnlSchedule As System.Windows.Forms.SplitterPanel
    Dim pnlSpotlist As System.Windows.Forms.SplitterPanel

    Dim eventsTable As DataTable

    'containers for the filter budget 
    Dim bolFilterBudget As Boolean = False
    Dim dblFilterBudget(15) As Double
    Dim dateEndWeek() As Date

    Dim DateIndex As Dictionary(Of Date, Single)

    Dim BookingFilter As New Trinity.cFilter

    Dim BookingFilterCopy As New Trinity.cFilter

    Dim ProgramFilterChecked As Boolean = False
    Dim TimeFilterChecked As Boolean = False

    Dim SkipIt As Boolean = False

    Dim comboboxRun As Boolean = False

    Dim SortSpotlistColumn As Windows.Forms.DataGridViewColumn
    Dim SortScheduleColumn As Windows.Forms.DataGridViewColumn

    Dim saveCounter As Integer = 0

    Dim otherBookedSpots As New Trinity.cBookedSpots(Campaign)
    Dim otherBookedSpotsList As New List(Of String)
    Dim showOtherBookedSpots As Boolean = True

    'a list of channels read from another campaign
    Dim readChannels As New List(Of Trinity.cChannel)

    Dim oldCI As System.Globalization.CultureInfo
    Dim AvailList As List(Of Trinity.cExtendedInfo)
    Dim NoEstimationTargetColumn As Boolean = False

    Dim Days() As String = {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"}

    Dim UpdateThread As Threading.Thread
    Dim ProfileThread As Threading.Thread
    Dim CrossCheckSpotsThread As Threading.Thread

    Dim sngPeakTRP As Single
    Dim sngOffPeakTRP As Single

    Sub UpdateSchedule(ByVal Rebuild As Boolean, ByVal RebuildGfx As Boolean)
        Dim Chan As String
        Dim BT As String
        Dim SQL As String
        'Dim TmpWeek As Trinity.cWeek 'a week
        Dim TmpEI As Trinity.cExtendedInfo
        Dim ID As String
        Dim SaveRow As Integer
        Dim TmpSpot As Trinity.cBookedSpot 'a spot that has been booked
        Dim List As New List(Of Trinity.cExtendedInfo)

        Dim c As Integer

        'if we dont need to rebuild/refetch we just update the grid through cellvalueneeded
        If Not Rebuild And Campaign.ExtendedInfos.Count > 0 Then
            grdSchedule.Invalidate()
            Exit Sub
        End If

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        If cmbChannel.SelectedIndex < 0 Then

            'If we only have one specific bookingtype we select it
            If cmbChannel.Items.Count = 1 Then
                cmbChannel.SelectedIndex = 0
            End If

            Me.Cursor = Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Check the combo boxes for the current channel Booking type and film
        Chan = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        BT = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Name

        Dim TmpFilm As Trinity.cFilm = Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(cmbFilm.Text)

        If TabSchedule.SelectedTab Is tpGfxSchedule Then
            TabSchedule.SelectTab(tpNumeric)
        End If

        SaveRow = grdSchedule.FirstDisplayedScrollingRowIndex
        grdSchedule.SuspendLayout()
        grdSchedule.Rows.Clear()
        grdSchedule.Columns.Clear()
        grdSchedule.Columns.Add("colID", "ID")
        grdSchedule.Columns("colID").Tag = "ID"
        grdSchedule.Columns("colID").Visible = False
        SkipIt = True
        For c = 1 To TrinitySettings.BookingColumnCount

            grdSchedule.Columns.Add("col" & TrinitySettings.BookingColumn(c), TrinitySettings.BookingColumn(c))
            grdSchedule.Columns("col" & TrinitySettings.BookingColumn(c)).Tag = TrinitySettings.BookingColumn(c)
            If TrinitySettings.BookingColumnWidth(c) > 0 Then
                grdSchedule.Columns("col" & TrinitySettings.BookingColumn(c)).Width = TrinitySettings.BookingColumnWidth(c)
            ElseIf TrinitySettings.BookingColumnWidth(c) = 0 Then
                grdSchedule.Columns("col" & TrinitySettings.BookingColumn(c)).Visible = False
            Else
                grdSchedule.Columns("col" & TrinitySettings.BookingColumn(c)).Width = 100
            End If
        Next
        SkipIt = False
        If Not SortScheduleColumn Is Nothing Then
            grdSchedule.Columns(SortScheduleColumn.Name).HeaderCell.SortGlyphDirection = SortScheduleColumn.HeaderCell.SortGlyphDirection
        End If


        'new DB handler
        If eventsTable Is Nothing Then
            eventsTable = GetEventsTable(Campaign.Channels(Chan).BookingTypes(BT))
        End If

CreateRows:
        grdSchedule.Rows.Clear()
        If cmbDatabase.SelectedIndex = 0 Then
            For Each dr As DataRow In eventsTable.Rows
                ID = dr.Item("Channel") & dr.Item("Date") & dr.Item("StartMam")
                If Not Campaign.ExtendedInfos.Exists(ID) Then
                    TmpEI = New Trinity.cExtendedInfo(Campaign, dr, cmbChannel.SelectedItem, NoEstimationTargetColumn)
                    
                    Campaign.ExtendedInfos.Add(TmpEI, ID)
                End If
                TmpEI = DirectCast(Campaign.ExtendedInfos.Item(ID), Trinity.cExtendedInfo)

                If otherBookedSpotsList.LastIndexOf(TmpEI.ID) > -1 Then
                    Campaign.ExtendedInfos(ID).IsBooked = True
                End If
                If FilterIn(Campaign.ExtendedInfos(ID)) Then
                    List.Add(Campaign.ExtendedInfos(ID))
                End If
            Next
        Else
            Dim sortedAvailList = From x In AvailList
            For Each TmpEI In AvailList
                If FilterIn(TmpEI) Then
                    List.Add(TmpEI)
                End If
            Next
        End If
        If Not SortScheduleColumn Is Nothing Then
            If SortScheduleColumn.HeaderCell.SortGlyphDirection = Windows.Forms.SortOrder.Descending Then
                QuicksortDescending(List, SortScheduleColumn, 0, List.Count - 1, TmpFilm, Campaign.Channels(Chan).BookingTypes(BT))
            Else
                QuicksortAscending(List, SortScheduleColumn, 0, List.Count - 1, TmpFilm, Campaign.Channels(Chan).BookingTypes(BT))
            End If
        End If
        For i As Integer = 0 To List.Count - 1
            grdSchedule.Rows.Add()
            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Tag = List(i).ID
        Next

        For Each TmpSpot In Campaign.BookedSpots
            If TmpSpot.Channel.ChannelName = Chan And TmpSpot.Bookingtype.Name = BT Then
                If Campaign.ExtendedInfos.Exists(TmpSpot.DatabaseID) Then
                    DirectCast(Campaign.ExtendedInfos(TmpSpot.DatabaseID), Trinity.cExtendedInfo).IsBooked = True
                End If
            End If
        Next
        'grdSchedule.AutoResizeRows()
        If Not SaveRow > grdSchedule.Rows.Count AndAlso SaveRow > 0 Then
            grdSchedule.FirstDisplayedScrollingRowIndex = SaveRow
        End If

        'grdSchedule.Top += 100
        grdSchedule.ResumeLayout()



        'If RebuildGfx Then
        '    GfxSchedule2.StartDate = Date.FromOADate(Campaign.StartDate)
        '    GfxSchedule2.EndDate = Date.FromOADate(Campaign.EndDate)
        GfxSchedule2.ExtendedInfos = Campaign.ExtendedInfos
        'GfxSchedule2.SetupDays()
        'End If
        Me.Cursor = Windows.Forms.Cursors.Default

    End Sub

    'Sub UpdateSchedule_old()

    '    Dim Chan As String
    '    Dim BT As String
    '    Dim SQL As String
    '    Dim TmpFilm As Trinity.cFilm 'a comercial film
    '    Dim TmpWeek As Trinity.cWeek 'a week
    '    Dim TmpEI As Trinity.cExtendedInfo
    '    Dim TmpCol As System.Windows.Forms.DataGridViewColumn
    '    Dim ID As String
    '    Dim SaveRow As Integer
    '    Dim Est As Single
    '    Dim EstBT As Single
    '    Dim TmpSpot As Trinity.cBookedSpot 'a spot that has been booked
    '    Dim c As Integer
    '    Me.Cursor = Windows.Forms.Cursors.WaitCursor

    '    If cmbChannel.SelectedIndex < 0 OrElse cmbFilm.SelectedIndex < 0 Then
    '        Me.Cursor = Windows.Forms.Cursors.Default
    '        Exit Sub
    '    End If

    '    'Check the combo boxes for the current channel Booking type and film
    '    Chan = cmbChannel.Text.Substring(0, InStr(cmbChannel.Text, " ") - 1)
    '    BT = Mid(cmbChannel.Text, InStr(cmbChannel.Text, " ") + 1)
    '    TmpFilm = Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(cmbFilm.Text)

    '    If TabSchedule.SelectedTab Is tpGfxSchedule Then
    '        TabSchedule.SelectTab(tpNumeric)
    '    End If

    '    If ScheduleCmd Is Nothing Then
    '        SQL = "SELECT * FROM events WHERE ("
    '        For Each TmpWeek In Campaign.Channels(Chan).BookingTypes(BT).Weeks
    '            SQL = SQL + "(Date>=" & TmpWeek.StartDate & " AND Date<=" & TmpWeek.EndDate & ") OR "
    '        Next
    '        SQL = SQL.Substring(0, Len(SQL) - 4) & ") AND Channel='" & Chan & "' ORDER BY Date,Time"
    '        ScheduleCmd = New System.Data.Odbc.OdbcCommand(SQL, DBConn)
    '    End If

    '    ScheduleRd = ScheduleCmd.ExecuteReader
    '    SaveRow = grdSchedule.FirstDisplayedScrollingRowIndex
    '    grdSchedule.SuspendLayout()
    '    grdSchedule.Rows.Clear()
    '    grdSchedule.Columns.Clear()
    '    grdSchedule.Columns.Add("colID", "ID")
    '    grdSchedule.Columns("colID").Tag = "ID"
    '    grdSchedule.Columns("colID").Visible = False
    '    SkipIt = True
    '    For c = 1 To TrinitySettings.BookingColumnCount

    '        grdSchedule.Columns.Add("col" & TrinitySettings.BookingColumn(c), TrinitySettings.BookingColumn(c))
    '        grdSchedule.Columns("col" & TrinitySettings.BookingColumn(c)).Tag = TrinitySettings.BookingColumn(c)
    '        If TrinitySettings.BookingColumnWidth(c) > 0 Then
    '            grdSchedule.Columns("col" & TrinitySettings.BookingColumn(c)).Width = TrinitySettings.BookingColumnWidth(c)
    '        ElseIf TrinitySettings.BookingColumnWidth(c) = 0 Then
    '            grdSchedule.Columns("col" & TrinitySettings.BookingColumn(c)).Visible = False
    '        Else
    '            grdSchedule.Columns("col" & TrinitySettings.BookingColumn(c)).Width = 100
    '        End If
    '    Next
    '    SkipIt = False


    '    Do While ScheduleRd.Read

    '        ID = ScheduleRd.Item("ID")
    '        If Not Campaign.ExtendedInfos.Exists(ID) Then
    '            TmpEI = New Trinity.cExtendedInfo
    '            TmpEI.AirDate = Date.FromOADate(ScheduleRd.Item("Date"))
    '            TmpEI.MaM = ScheduleRd.Item("StartMam")
    '            If IsDBNull(ScheduleRd.Item("EstimationPeriod")) OrElse ScheduleRd.Item("EstimationPeriod") Is Nothing Then
    '                TmpEI.EstimationPeriod = "-4fw"
    '            Else
    '                TmpEI.EstimationPeriod = ScheduleRd.Item("EstimationPeriod")
    '            End If
    '            TmpEI.ProgAfter = ScheduleRd.Item("Name")
    '            TmpEI.Channel = ScheduleRd.Item("Channel")
    '            TmpEI.GrossPrice30 = ScheduleRd.Item("price") '* (Campaign.Channels(Chan).BookingTypes(BT).Indexes.GetIndexForDate(TmpEI.AirDate, Trinity.cIndex.IndexOnEnum.eGrossCPP) / 100)
    '            'If TmpEI.AirDate = #9/18/2006# Then Stop
    '            TmpEI.ChannelEstimate = ScheduleRd.Item("ChanEst")
    '            TmpEI.Remark = ""
    '            If ScheduleRd.Item("IsLocal") Then
    '                TmpEI.Remark = TmpEI.Remark & "L"
    '            End If
    '            If ScheduleRd.Item("IsRB") Then
    '                TmpEI.Remark = TmpEI.Remark & "R"
    '            End If
    '            TmpEI.Duration = ScheduleRd.Item("Duration")
    '            TmpEI.ID = ID
    '            For Each TmpSpot In Campaign.BookedSpots
    '                If TmpSpot.DatabaseID = ID Then
    '                    TmpEI.IsBooked = True
    '                    Exit For
    '                End If
    '            Next
    '            Campaign.ExtendedInfos.Add(TmpEI, ID)
    '        End If
    '        Campaign.ExtendedInfos(ID).NetPrice = Campaign.ExtendedInfos(ID).GrossPrice30 * (TmpFilm.Index / 100) * (1 - Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Discount) * DateIndex(Format(Date.FromOADate(ScheduleRd!Date), "Short Date"))
    '        Campaign.ExtendedInfos(ID).GrossPrice = Campaign.ExtendedInfos(ID).GrossPrice30 * (TmpFilm.Index / 100)
    '        If FilterIn() Then
    '            grdSchedule.Rows.Add()
    '            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Tag = ID
    '            For Each TmpCol In grdSchedule.Columns
    '                If Campaign.ExtendedInfos(ID).IsBooked Then
    '                    grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Style.ForeColor = Drawing.Color.Red
    '                End If
    '                If TmpCol.Tag = "ID" Then
    '                    grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = ScheduleRd.Item("ID")
    '                ElseIf TmpCol.Visible = True Then
    '                    Est = Campaign.ExtendedInfos(ID).EstimatedRating
    '                    EstBT = Campaign.ExtendedInfos(ID).EstimatedRatingBuyingTarget
    '                    SkipIt = True
    '                    Select Case TmpCol.Tag
    '                        Case "Date"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(Date.FromOADate(ScheduleRd.Item("Date")), "Short Date")
    '                        Case "Week"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = DatePart(DateInterval.WeekOfYear, Date.FromOADate(ScheduleRd.Item("Date")), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
    '                        Case "Weekday"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Days(DatePart(DateInterval.Weekday, Date.FromOADate(ScheduleRd.Item("Date")), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) - 1)
    '                        Case "Time"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Trinity.Helper.Mam2Tid(ScheduleRd.Item("StartMam"))
    '                        Case "Channel"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Campaign.Channels(ScheduleRd.Item("Channel")).Shortname
    '                        Case "Program"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = ScheduleRd.Item("Name")
    '                        Case "Gross Price"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(ScheduleRd.Item("price") * (TmpFilm.Index / 100), "##,##0 kr")
    '                        Case "Program"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = ScheduleRd.Item("Name")
    '                        Case "Chan Est"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(ScheduleRd.Item("ChanEst"), "0.0")
    '                        Case "Net Price"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(ScheduleRd.Item("price") * (TmpFilm.Index / 100) * (1 - Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Discount) * (Campaign.Channels(Chan).BookingTypes(BT).Indexes.GetIndexForDate(Date.FromOADate(ScheduleRd.Item("Date")), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100) * (Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(ScheduleRd.Item("Date")), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100), "##,##0 kr")
    '                        Case "Solus"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(Campaign.ExtendedInfos(ID).Solus, "0.0")
    '                        Case "CPS"
    '                            If Campaign.ExtendedInfos(ID).Solus <> 0 Then
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(Campaign.ExtendedInfos(ID).NetPrice / Campaign.ExtendedInfos(ID).Solus, "##,##0 kr")
    '                            Else
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(0, "0.0")
    '                            End If
    '                        Case "Solus 1st"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(Campaign.ExtendedInfos(ID).SolusFirst, "0.0")
    '                        Case "Remarks"
    '                            If ScheduleRd.Item("IsLocal") Then
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = "Local"
    '                            End If
    '                        Case "Gross CPP"
    '                            If ScheduleRd.Item("ChanEst") > 0 Then
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format((ScheduleRd.Item("price") / ScheduleRd.Item("ChanEst")), "##,##0 kr")
    '                            Else
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = "0 kr"
    '                            End If
    '                        Case "CPP Main"
    '                            If Est > 0 Then
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format((ScheduleRd.Item("price") * (1 - Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Discount) * (Campaign.Channels(Chan).BookingTypes(BT).Indexes.GetIndexForDate(Date.FromOADate(ScheduleRd.Item("Date")), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100) * (Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(ScheduleRd.Item("Date")), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100)) / Est, "##,##0 kr")
    '                            Else
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = "0 kr"
    '                            End If
    '                        Case "CPP (Chn Est)"
    '                            If ScheduleRd.Item("ChanEst") > 0 Then
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(((ScheduleRd.Item("price") * (1 - Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Discount) * (Campaign.Channels(Chan).BookingTypes(BT).Indexes.GetIndexForDate(Date.FromOADate(ScheduleRd.Item("Date")), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100) * (Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(ScheduleRd.Item("Date")), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100)) / ScheduleRd.Item("ChanEst")), "##,##0 kr")
    '                            Else
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = "0 kr"
    '                            End If
    '                        Case "Est"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(Est, "0.0")
    '                        Case "Est Buy"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(EstBT, "0.0")
    '                        Case "Idx Chan Est"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format((Est / ScheduleRd!ChanEst) * 100, "0")
    '                            If Format((Est / ScheduleRd!ChanEst) * 100, "0") > 100 Then
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Style.ForeColor = Drawing.Color.LightGreen
    '                            ElseIf Format((Est / ScheduleRd!ChanEst) * 100, "0") < 100 Then
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Style.ForeColor = Drawing.Color.Red
    '                            End If
    '                    End Select
    '                    SkipIt = False
    '                End If
    '            Next
    '        End If
    '    Loop

    '    grdSchedule.AutoResizeRows()
    '    If Not SaveRow > grdSchedule.Rows.Count AndAlso SaveRow > 0 Then
    '        grdSchedule.FirstDisplayedScrollingRowIndex = SaveRow
    '    End If
    '    ScheduleRd.Close()
    '    grdSchedule.ResumeLayout()

    '    GfxSchedule2.StartDate = Date.FromOADate(Campaign.StartDate)
    '    GfxSchedule2.EndDate = Date.FromOADate(Campaign.EndDate)
    '    GfxSchedule2.ExtendedInfos = Campaign.ExtendedInfos
    '    GfxSchedule2.SetupDays()

    '    Me.Cursor = Windows.Forms.Cursors.Default
    'End Sub

    'Private Sub numericTabThread()

    '    If ScheduleCmd Is Nothing Then
    '        SQL = "SELECT * FROM events WHERE ("
    '        For Each TmpWeek In Campaign.Channels(Chan).BookingTypes(BT).Weeks
    '            SQL = SQL + "(Date>=" & TmpWeek.StartDate & " AND Date<=" & TmpWeek.EndDate & ") OR "
    '        Next
    '        SQL = SQL.Substring(0, Len(SQL) - 4) & ") AND Channel='" & Chan & "' ORDER BY Date,Time"
    '        ScheduleCmd = New System.Data.Odbc.OdbcCommand(SQL, DBConn)
    '    End If

    '    ScheduleRd = ScheduleCmd.ExecuteReader
    '    SaveRow = grdSchedule.FirstDisplayedScrollingRowIndex
    '    grdSchedule.SuspendLayout()
    '    grdSchedule.Rows.Clear()
    '    grdSchedule.Columns.Clear()
    '    grdSchedule.Columns.Add("colID", "ID")
    '    grdSchedule.Columns("colID").Tag = "ID"
    '    grdSchedule.Columns("colID").Visible = False
    '    SkipIt = True
    '    For c = 1 To TrinitySettings.BookingColumnCount

    '        grdSchedule.Columns.Add("col" & TrinitySettings.BookingColumn(c), TrinitySettings.BookingColumn(c))
    '        grdSchedule.Columns("col" & TrinitySettings.BookingColumn(c)).Tag = TrinitySettings.BookingColumn(c)
    '        If TrinitySettings.BookingColumnWidth(c) > 0 Then
    '            grdSchedule.Columns("col" & TrinitySettings.BookingColumn(c)).Width = TrinitySettings.BookingColumnWidth(c)
    '        ElseIf TrinitySettings.BookingColumnWidth(c) = 0 Then
    '            grdSchedule.Columns("col" & TrinitySettings.BookingColumn(c)).Visible = False
    '        Else
    '            grdSchedule.Columns("col" & TrinitySettings.BookingColumn(c)).Width = 100
    '        End If
    '    Next
    '    SkipIt = False
    '    'Dim startTickCount = My.Computer.Clock.TickCount
    '    Do While ScheduleRd.Read
    '        ID = ScheduleRd.Item("ID")
    '        If Not Campaign.ExtendedInfos.Exists(ID) Then
    '            TmpEI = New Trinity.cExtendedInfo
    '            TmpEI.AirDate = Date.FromOADate(ScheduleRd.Item("Date"))
    '            TmpEI.MaM = ScheduleRd.Item("StartMam")
    '            If IsDBNull(ScheduleRd.Item("EstimationPeriod")) OrElse ScheduleRd.Item("EstimationPeriod") Is Nothing Then
    '                TmpEI.EstimationPeriod = "-4fw"
    '            Else
    '                TmpEI.EstimationPeriod = ScheduleRd.Item("EstimationPeriod")
    '            End If
    '            TmpEI.ProgAfter = ScheduleRd.Item("Name")
    '            TmpEI.Channel = ScheduleRd.Item("Channel")
    '            TmpEI.GrossPrice30 = ScheduleRd.Item("price") '* (Campaign.Channels(Chan).BookingTypes(BT).Indexes.GetIndexForDate(TmpEI.AirDate, Trinity.cIndex.IndexOnEnum.eGrossCPP) / 100)
    '            'If TmpEI.AirDate = #9/18/2006# Then Stop
    '            TmpEI.ChannelEstimate = ScheduleRd.Item("ChanEst")
    '            TmpEI.Remark = ""
    '            If ScheduleRd.Item("IsLocal") Then
    '                TmpEI.Remark = TmpEI.Remark & "L"
    '            End If
    '            If ScheduleRd.Item("IsRB") Then
    '                TmpEI.Remark = TmpEI.Remark & "R"
    '            End If
    '            TmpEI.Duration = ScheduleRd.Item("Duration")
    '            TmpEI.ID = ID
    '            For Each TmpSpot In Campaign.BookedSpots
    '                If TmpSpot.DatabaseID = ID Then
    '                    TmpEI.IsBooked = True
    '                    Exit For
    '                End If
    '            Next
    '            Campaign.ExtendedInfos.Add(TmpEI, ID)
    '        End If
    '        Campaign.ExtendedInfos(ID).NetPrice = Campaign.ExtendedInfos(ID).GrossPrice30 * (TmpFilm.Index / 100) * (1 - Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Discount) * DateIndex(Format(Date.FromOADate(ScheduleRd!Date), "Short Date"))
    '        Campaign.ExtendedInfos(ID).GrossPrice = Campaign.ExtendedInfos(ID).GrossPrice30 * (TmpFilm.Index / 100)
    '        If FilterIn() Then
    '            grdSchedule.Rows.Add()
    '            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Tag = ID
    '            For Each TmpCol In grdSchedule.Columns
    '                If Campaign.ExtendedInfos(ID).IsBooked Then
    '                    grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Style.ForeColor = Drawing.Color.Red
    '                End If
    '                If TmpCol.Tag = "ID" Then
    '                    grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = ScheduleRd.Item("ID")
    '                ElseIf TmpCol.Visible = True Then
    '                    Est = Campaign.ExtendedInfos(ID).EstimatedRating
    '                    EstBT = Campaign.ExtendedInfos(ID).EstimatedRatingBuyingTarget
    '                    SkipIt = True
    '                    Select Case TmpCol.Tag
    '                        Case "Date"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(Date.FromOADate(ScheduleRd.Item("Date")), "Short Date")
    '                        Case "Week"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = DatePart(DateInterval.WeekOfYear, Date.FromOADate(ScheduleRd.Item("Date")), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
    '                        Case "Weekday"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Days(DatePart(DateInterval.Weekday, Date.FromOADate(ScheduleRd.Item("Date")), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) - 1)
    '                        Case "Time"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Trinity.Helper.Mam2Tid(ScheduleRd.Item("StartMam"))
    '                        Case "Channel"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Campaign.Channels(ScheduleRd.Item("Channel")).Shortname
    '                        Case "Program"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = ScheduleRd.Item("Name")
    '                        Case "Gross Price"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(ScheduleRd.Item("price") * (TmpFilm.Index / 100), "##,##0 kr")
    '                        Case "Program"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = ScheduleRd.Item("Name")
    '                        Case "Chan Est"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(ScheduleRd.Item("ChanEst"), "0.0")
    '                        Case "Net Price"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(ScheduleRd.Item("price") * (TmpFilm.Index / 100) * (1 - Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Discount) * (Campaign.Channels(Chan).BookingTypes(BT).Indexes.GetIndexForDate(Date.FromOADate(ScheduleRd.Item("Date")), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100) * (Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(ScheduleRd.Item("Date")), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100), "##,##0 kr")
    '                        Case "Solus"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(Campaign.ExtendedInfos(ID).Solus, "0.0")
    '                        Case "CPS"
    '                            If Campaign.ExtendedInfos(ID).Solus <> 0 Then
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(Campaign.ExtendedInfos(ID).NetPrice / Campaign.ExtendedInfos(ID).Solus, "##,##0 kr")
    '                            Else
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(0, "0.0")
    '                            End If
    '                        Case "Solus 1st"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(Campaign.ExtendedInfos(ID).SolusFirst, "0.0")
    '                        Case "Remarks"
    '                            If ScheduleRd.Item("IsLocal") Then
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = "Local"
    '                            End If
    '                        Case "Gross CPP"
    '                            If ScheduleRd.Item("ChanEst") > 0 Then
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format((ScheduleRd.Item("price") / ScheduleRd.Item("ChanEst")), "##,##0 kr")
    '                            Else
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = "0 kr"
    '                            End If
    '                        Case "CPP Main"
    '                            If Est > 0 Then
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format((ScheduleRd.Item("price") * (1 - Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Discount) * (Campaign.Channels(Chan).BookingTypes(BT).Indexes.GetIndexForDate(Date.FromOADate(ScheduleRd.Item("Date")), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100) * (Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(ScheduleRd.Item("Date")), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100)) / Est, "##,##0 kr")
    '                            Else
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = "0 kr"
    '                            End If
    '                        Case "CPP (Chn Est)"
    '                            If ScheduleRd.Item("ChanEst") > 0 Then
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(((ScheduleRd.Item("price") * (1 - Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Discount) * (Campaign.Channels(Chan).BookingTypes(BT).Indexes.GetIndexForDate(Date.FromOADate(ScheduleRd.Item("Date")), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100) * (Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(ScheduleRd.Item("Date")), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100)) / ScheduleRd.Item("ChanEst")), "##,##0 kr")
    '                            Else
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = "0 kr"
    '                            End If
    '                        Case "Est"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(Est, "0.0")
    '                        Case "Est Buy"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(EstBT, "0.0")
    '                        Case "Idx Chan Est"
    '                            grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Value = Format((Est / ScheduleRd!ChanEst) * 100, "0")
    '                            If Format((Est / ScheduleRd!ChanEst) * 100, "0") > 100 Then
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Style.ForeColor = Drawing.Color.LightGreen
    '                            ElseIf Format((Est / ScheduleRd!ChanEst) * 100, "0") < 100 Then
    '                                grdSchedule.Rows(grdSchedule.Rows.Count - 1).Cells(TmpCol.Name).Style.ForeColor = Drawing.Color.Red
    '                            End If
    '                    End Select
    '                    SkipIt = False
    '                End If
    '            Next
    '        End If
    '    Loop
    '    'MsgBox("Antal millisekunder för DO WHILE: " & My.Computer.Clock.TickCount - startTickCount)
    '    grdSchedule.AutoResizeRows()
    '    If Not SaveRow > grdSchedule.Rows.Count AndAlso SaveRow > 0 Then
    '        grdSchedule.FirstDisplayedScrollingRowIndex = SaveRow
    '    End If
    '    ScheduleRd.Close()
    '    grdSchedule.ResumeLayout()
    '    tpNumeric.Enabled = True
    'End Sub

    Private Sub graphicTabThread()
        GfxSchedule2.SetupDays()
        tpGfxSchedule.Enabled = True
    End Sub

    Sub UpdateSpotlist()

        Dim WeekDays() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}

        Dim SaveRow As Integer

        Dim Savelist As Windows.Forms.DataGridViewSelectedRowCollection
        Dim TmpSpot As Trinity.cBookedSpot
        Dim c As Integer
        Dim List As New List(Of Trinity.cBookedSpot)

        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        If cmbChannel.SelectedIndex = -1 Then
            Me.Cursor = Windows.Forms.Cursors.Default
            Exit Sub
        End If


        'If TrinitySettings.Developer Then
        '    For Each spott As Trinity.cBookedSpot In Campaign.BookedSpots
        '        If spott.AirDate < "2014-12-29" Then
        '            Campaign.BookedSpots.Remove(spott)
        '        End If
        '    Next

        '    For i As Integer = 0 To Campaign.BookedSpots.Count

        '    Next
        'End If

        Dim TmpBT As Trinity.cBookingType = cmbChannel.SelectedItem

        Dim antalbokadespottar = Campaign.BookedSpots.Count()

        SaveRow = grdSpotlist.FirstDisplayedScrollingRowIndex
        Savelist = grdSpotlist.SelectedRows
        grdSpotlist.SuspendLayout()
        grdSpotlist.ReadOnly = False
        grdSpotlist.Rows.Clear()
        grdSpotlist.Columns.Clear()
        grdSpotlist.Columns.Add("colSpotlistID", "ID")
        grdSpotlist.Columns("colSpotlistID").HeaderText = "ID"
        grdSpotlist.Columns("colSpotlistID").Visible = False
        grdSpotlist.Columns.Add("colSpotlistDateTimeSerial", "DateTimeSerial")
        grdSpotlist.Columns("colSpotlistDateTimeSerial").HeaderText = "DateTimeSerial"
        grdSpotlist.Columns("colSpotlistDateTimeSerial").Visible = False
        If SortSpotlistColumn Is Nothing Then SortSpotlistColumn = grdSpotlist.Columns("colSpotlistDateTimeSerial")
        SkipIt = True

        For c = 1 To TrinitySettings.SpotlistColumnCount
            grdSpotlist.Columns.Add("colSpotlist" & TrinitySettings.SpotlistColumn(c), TrinitySettings.SpotlistColumn(c))
            grdSpotlist.Columns("colSpotlist" & TrinitySettings.SpotlistColumn(c)).Tag = TrinitySettings.SpotlistColumn(c)
            If TrinitySettings.SpotlistColumnWidth(c) > 0 Then
                grdSpotlist.Columns("colSpotlist" & TrinitySettings.SpotlistColumn(c)).Width = TrinitySettings.SpotlistColumnWidth(c)
            ElseIf TrinitySettings.SpotlistColumnWidth(c) = 0 Then
                grdSpotlist.Columns("colSpotlist" & TrinitySettings.SpotlistColumn(c)).Visible = False
            Else
                grdSpotlist.Columns("colSpotlist" & TrinitySettings.SpotlistColumn(c)).Width = 100
            End If
            If Not TrinitySettings.SpotlistColumn(c) = "colSpotlistCPP (Main)" Then
                grdSpotlist.Columns("colSpotlist" & TrinitySettings.SpotlistColumn(c)).Tag = 0
            End If
        Next
        SkipIt = False

        grdSpotlist.Columns(SortSpotlistColumn.Name).HeaderCell.SortGlyphDirection = SortSpotlistColumn.HeaderCell.SortGlyphDirection
        For Each TmpSpot In Campaign.BookedSpots

            ' Apply all the filters
            If (TmpSpot.Channel Is TmpBT.ParentChannel AndAlso TmpSpot.Bookingtype Is TmpBT) OrElse BookingFilter.Data("Channel", TmpSpot.Bookingtype.ToString) Then

                ' Stored what daypart this spot is in
                Dim tmpSpotDaypart As Trinity.cDaypart = Campaign.Dayparts.GetDaypartForMam(TmpSpot.MaM)

                ' Sort by the filters
                If (BookingFilter.Data("Actual Weekday", WeekDays(Weekday(TmpSpot.AirDate, vbMonday) - 1))) Then
                    If Not TmpSpot.week Is Nothing AndAlso BookingFilter.Data("ActualWeek", TmpSpot.week.Name) Then
                        If BookingFilter.Data("Actual Daypart", tmpSpotDaypart.Name) Then

                            If Campaign.ExtendedInfos.Exists(TmpSpot.DatabaseID) Then
                                DirectCast(Campaign.ExtendedInfos(TmpSpot.DatabaseID), Trinity.cExtendedInfo).IsBooked = True
                            End If
                            List.Add(TmpSpot)
                        ElseIf TmpSpot.week Is Nothing Then
                            List.Add(TmpSpot)
                        End If
                    End If
                End If
            End If
        Next
        If SortSpotlistColumn.HeaderCell.SortGlyphDirection = Windows.Forms.SortOrder.Descending Then
            QuicksortDescending(List, SortSpotlistColumn, 0, List.Count - 1)
        Else
            QuicksortAscending(List, SortSpotlistColumn, 0, List.Count - 1)
        End If

        For i As Integer = 0 To List.Count - 1
            grdSpotlist.Rows.Add()
            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Tag = List(i).ID

            If grdSpotlist.Columns.Contains("colSpotlistGross Price") Then
                If grdSpotlist.Columns("colSpotlistGross Price").Tag Is Nothing OrElse grdSpotlist.Columns("colSpotlistGross Price").Tag.ToString = "" Then grdSpotlist.Columns("colSpotlistGross Price").Tag = 0
                grdSpotlist.Columns("colSpotlistGross Price").Tag += List(i).GrossPrice * List(i).AddedValueIndex(False)
            End If
            If grdSpotlist.Columns.Contains("colSpotlistNet Price") Then
                If grdSpotlist.Columns("colSpotlistNet Price").Tag Is Nothing OrElse grdSpotlist.Columns("colSpotlistNet Price").Tag.ToString = "" Then grdSpotlist.Columns("colSpotlistNet Price").Tag = 0
                grdSpotlist.Columns("colSpotlistNet Price").Tag += +List(i).NetPrice * List(i).AddedValueIndex
            End If
            If grdSpotlist.Columns.Contains("colSpotlistChan Est") Then
                If grdSpotlist.Columns("colSpotlistChan Est").Tag Is Nothing OrElse grdSpotlist.Columns("colSpotlistChan Est").Tag.ToString = "" Then grdSpotlist.Columns("colSpotlistChan Est").Tag = 0
                grdSpotlist.Columns("colSpotlistChan Est").Tag += List(i).ChannelEstimate
            End If
            If grdSpotlist.Columns.Contains("colSpotlistEst") Then
                If grdSpotlist.Columns("colSpotlistEst").Tag Is Nothing OrElse grdSpotlist.Columns("colSpotlistEst").Tag.ToString = "" Then grdSpotlist.Columns("colSpotlistEst").Tag = 0
                grdSpotlist.Columns("colSpotlistEst").Tag += List(i).MyEstimate
            End If
            If grdSpotlist.Columns.Contains("colSpotlistCPP (Main)") Then
                If grdSpotlist.Columns("colSpotlistCPP (Main)").Tag.GetType.Name = "Int32" Then
                    grdSpotlist.Columns("colSpotlistCPP (Main)").Tag = New CPPColumnTag
                End If
                If grdSpotlist.Columns("colSpotlistCPP (Main)").Tag Is Nothing OrElse grdSpotlist.Columns("colSpotlistCPP (Main)").Tag.ToString = "" Then grdSpotlist.Columns("colSpotlistCPP (Main)").Tag = New CPPColumnTag
                DirectCast(grdSpotlist.Columns("colSpotlistCPP (Main)").Tag, CPPColumnTag).Budget += List(i).NetPrice * List(i).AddedValueIndex(True)
                DirectCast(grdSpotlist.Columns("colSpotlistCPP (Main)").Tag, CPPColumnTag).Estimate += List(i).MyEstimate
            End If
            If grdSpotlist.Columns.Contains("colSpotlistCPP 30"" (Main)") Then
                If grdSpotlist.Columns("colSpotlistCPP 30"" (Main)").Tag.GetType.Name = "Int32" Then
                    grdSpotlist.Columns("colSpotlistCPP 30"" (Main)").Tag = New CPPColumnTag
                End If
                If grdSpotlist.Columns("colSpotlistCPP 30"" (Main)").Tag Is Nothing OrElse grdSpotlist.Columns("colSpotlistCPP 30"" (Main)").Tag.ToString = "" Then grdSpotlist.Columns("colSpotlistCPP 30"" (Main)").Tag = New CPPColumnTag
                DirectCast(grdSpotlist.Columns("colSpotlistCPP 30"" (Main)").Tag, CPPColumnTag).Budget += List(i).NetPrice * List(i).AddedValueIndex(True)
                If List(i).Film IsNot Nothing Then
                    DirectCast(grdSpotlist.Columns("colSpotlistCPP 30"" (Main)").Tag, CPPColumnTag).Estimate += List(i).MyEstimate / (List(i).Film.Index / 100)
                Else
                    DirectCast(grdSpotlist.Columns("colSpotlistCPP 30"" (Main)").Tag, CPPColumnTag).Estimate = 0
                End If

            End If
        Next

        If showOtherBookedSpots Then
            For i As Integer = 0 To otherBookedSpots.Count - 1
                If Not otherBookedSpots(i + 1).week Is Nothing Then
                    If BookingFilter.Data("Channel", otherBookedSpots(i + 1).Bookingtype.ToString) And BookingFilter.Data("ActualWeek", otherBookedSpots(i + 1).week.Name) Then
                        grdSpotlist.Rows.Add()
                        grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Tag = otherBookedSpots(i + 1).ID

                        If grdSpotlist.Columns.Contains("colSpotlistGross Price") Then
                            If grdSpotlist.Columns("colSpotlistGross Price").Tag Is Nothing OrElse grdSpotlist.Columns("colSpotlistGross Price").Tag.ToString = "" Then grdSpotlist.Columns("colSpotlistGross Price").Tag = 0
                            grdSpotlist.Columns("colSpotlistGross Price").Tag = grdSpotlist.Columns("colSpotlistGross Price").Tag + otherBookedSpots(i + 1).GrossPrice * otherBookedSpots(i + 1).AddedValueIndex(False)
                        End If
                        If grdSpotlist.Columns.Contains("colSpotlistNet Price") Then
                            If grdSpotlist.Columns("colSpotlistNet Price").Tag Is Nothing OrElse grdSpotlist.Columns("colSpotlistNet Price").Tag.ToString = "" Then grdSpotlist.Columns("colSpotlistNet Price").Tag = 0
                            grdSpotlist.Columns("colSpotlistNet Price").Tag = grdSpotlist.Columns("colSpotlistNet Price").Tag + otherBookedSpots(i + 1).NetPrice * otherBookedSpots(i + 1).AddedValueIndex
                        End If
                        If grdSpotlist.Columns.Contains("colSpotlistChan Est") Then
                            If grdSpotlist.Columns("colSpotlistChan Est").Tag Is Nothing OrElse grdSpotlist.Columns("colSpotlistChan Est").Tag.ToString = "" Then grdSpotlist.Columns("colSpotlistChan Est").Tag = 0
                            grdSpotlist.Columns("colSpotlistChan Est").Tag = grdSpotlist.Columns("colSpotlistChan Est").Tag + otherBookedSpots(i + 1).ChannelEstimate
                        End If
                        If grdSpotlist.Columns.Contains("colSpotlistEst") Then
                            If grdSpotlist.Columns("colSpotlistEst").Tag Is Nothing OrElse grdSpotlist.Columns("colSpotlistEst").Tag.ToString = "" Then grdSpotlist.Columns("colSpotlistEst").Tag = 0
                            grdSpotlist.Columns("colSpotlistEst").Tag = grdSpotlist.Columns("colSpotlistEst").Tag + otherBookedSpots(i + 1).MyEstimate
                        End If
                    End If
                End If
            Next
        End If


        grdSpotlist.Rows.Add()
        grdSpotlist.AutoResizeRows()
        'grdSpotlist.Sort(grdSpotlist.Columns("colSpotlistDateTimeSerial"), ComponentModel.ListSortDirection.Ascending)
        If Not SaveRow > grdSpotlist.Rows.Count AndAlso SaveRow > -1 Then
            Try
                grdSpotlist.FirstDisplayedScrollingRowIndex = SaveRow
            Catch

            End Try
        End If
        If TmpBT.AddedValues.Count = 0 Then
            cmdSpotlistAV.Enabled = False
        Else
            cmdSpotlistAV.Enabled = True
        End If

        grdSpotlist.ResumeLayout()
        Me.Cursor = Windows.Forms.Cursors.Default

    End Sub

    Private Sub grdSpotlist_CellErrorTextNeeded(sender As Object, e As System.Windows.Forms.DataGridViewCellErrorTextNeededEventArgs) Handles grdSpotlist.CellErrorTextNeeded
        Dim TmpSpot As Trinity.cBookedSpot = Campaign.BookedSpots(grdSpotlist.Rows(e.RowIndex).Tag)
        If TmpSpot Is Nothing OrElse TmpSpot.Bookingtype IsNot cmbChannel.SelectedItem Then Exit Sub
        e.ErrorText = ""
        If Not cmdCheckForChanges.Checked Then Exit Sub
        Dim ID As String = TmpSpot.Channel.ChannelName & TmpSpot.AirDate.ToOADate & TmpSpot.MaM
        Dim TmpEI As Trinity.cExtendedInfo
        If Not Campaign.ExtendedInfos.Exists(ID) Then
            For _mam As Integer = TmpSpot.MaM - 5 To TmpSpot.MaM + 5
                ID = TmpSpot.Channel.ChannelName & TmpSpot.AirDate.ToOADate & _mam
                If Campaign.ExtendedInfos.Exists(ID) Then
                    Exit For
                End If
            Next
        End If
        If Campaign.ExtendedInfos.Exists(ID) Then
            TmpEI = Campaign.ExtendedInfos(ID)
            Select Case grdSpotlist.Columns(e.ColumnIndex).HeaderText
                Case "Gross Price"
                    If Math.Floor(TmpSpot.GrossPrice30) <> Math.Floor(TmpEI.GrossPrice30(False, 0)) Then
                        e.ErrorText = String.Format("The Gross price for this break has changed to {0}", Format(TmpEI.GrossPrice30(False), "C0"))
                    End If
                Case "Net Price"
                    If Math.Floor(TmpSpot.NetPrice) <> Math.Floor(TmpEI.NetPrice(TmpSpot.Film, TmpSpot.Bookingtype, TmpSpot.Bid)) Then
                        e.ErrorText = String.Format("This Net price is not consistent with the price of this break (Should be {0})", Format(TmpEI.NetPrice(TmpSpot.Film, TmpSpot.Bookingtype, TmpSpot.Bid) * TmpSpot.AddedValueIndex, "C0"))
                    End If
                Case "Program"
                    If TmpSpot.Programme <> TmpEI.ProgAfter AndAlso TmpEI.ProgAfter.IndexOf(TmpSpot.Programme) < 0 Then
                        e.ErrorText = String.Format("The program for this break has changed to '{0}'", TmpEI.ProgAfter)
                    End If
                Case "Chan Est", "Est"
                    If TmpSpot.ChannelEstimate <> TmpEI.ChannelEstimate Then
                        e.ErrorText = String.Format("The estimate for this break has changed, it is now {0}", Format(TmpEI.ChannelEstimate, "N1"))
                    End If
            End Select
        ElseIf grdSpotlist.Columns(e.ColumnIndex).HeaderText = "Time" Then
            e.ErrorText = "This break does not seem to exist anymore, it is either moved or removed."
        End If
    End Sub

    Private Sub grdSpotlist_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdSpotlist.CellFormatting

        If e.RowIndex = grdSpotlist.Rows.Count - 1 Then Exit Sub
        Dim TmpBT As Trinity.cBookingType = cmbChannel.SelectedItem
        Dim TmpSpot As Trinity.cBookedSpot = Campaign.BookedSpots(grdSpotlist.Rows(e.RowIndex).Tag)
        If TmpSpot Is Nothing Then
            TmpSpot = otherBookedSpots(grdSpotlist.Rows(e.RowIndex).Tag)
        End If
        If TmpSpot Is Nothing Then Exit Sub

        If TmpSpot.otherCampaign Then
            e.CellStyle.ForeColor = Color.Gray
        Else
            If TmpSpot.Bookingtype Is TmpBT Then
                If TmpSpot.NotFound Then
                    e.CellStyle.ForeColor = Color.Red
                ElseIf TmpSpot.MostRecentlyBooked Then
                    e.CellStyle.ForeColor = Color.Blue
                ElseIf TmpSpot.RecentlyBooked Then
                    e.CellStyle.ForeColor = Color.Green
                Else
                    e.CellStyle.ForeColor = Color.Black
                End If
            Else
                e.CellStyle.ForeColor = Color.Gray
            End If
        End If

    End Sub

    Private Sub grdSpotlist_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSpotlist.CellValueNeeded
        Dim TmpCol As System.Windows.Forms.DataGridViewColumn = grdSpotlist.Columns(e.ColumnIndex)
        Dim TmpStr As String
        Dim kv As KeyValuePair(Of String, Trinity.cAddedValue)
        Dim Est As Single
        Dim EstBT As Single

        Dim Chan As String
        Dim BT As String

        Chan = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        BT = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Name

        If e.RowIndex = grdSpotlist.Rows.Count - 1 Then
            If grdSpotlist.Columns(e.ColumnIndex).DisplayIndex = 0 Then
                e.Value = "Sum:"
            Else
                Select Case TmpCol.HeaderText
                    Case "Program"
                        e.Value = "Spot count: " & grdSpotlist.Rows.Count - 1
                    Case "Gross Price"
                        e.Value = Format(TmpCol.Tag, "##,##0 kr")
                    Case "Chan Est"
                        e.Value = Format(TmpCol.Tag, "N1")
                    Case "Net Price"
                        e.Value = Format(TmpCol.Tag, "##,##0 kr")
                    Case "Est"
                        e.Value = Format(TmpCol.Tag, "N1")
                    Case "Est (Buying)"
                        e.Value = Format(TmpCol.Tag, "0.0")
                    Case "CPP (Main)"
                        Dim totalTRP As Double
                        Dim totalBudget As Double
                        Dim fullPrice As Double
                        For Each bookedSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                            If bookedSpot.Channel.ChannelName = Chan AndAlso bookedSpot.Bookingtype.Name = BT Then
                                totalTRP += bookedSpot.MyEstimate
                                fullPrice = bookedSpot.NetPrice
                                For Each added As KeyValuePair(Of String, clTrinity.Trinity.cAddedValue) In bookedSpot.AddedValues
                                    If Not added.Value Is Nothing Then fullPrice *= added.Value.IndexNet / 100
                                Next
                                totalBudget += fullPrice
                            End If
                        Next
                        e.Value = "Avg: " & Format(totalBudget / totalTRP, "##,##0 kr")
                        'e.Value = Format(DirectCast(TmpCol.Tag, CPPColumnTag).CPP, "C1")
                    Case "CPP 30"" (Main)"
                        Dim totalTRP30 As Double = 0
                        Dim totalBudget As Double = 0
                        Dim fullPrice As Double
                        For Each bookedSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                            If bookedSpot.Film IsNot Nothing Then
                                If bookedSpot.Channel.ChannelName = Chan AndAlso bookedSpot.Bookingtype.Name = BT Then
                                    totalTRP30 += bookedSpot.MyEstimate * (bookedSpot.Film.Index / 100)
                                    fullPrice = bookedSpot.NetPrice
                                    For Each added As KeyValuePair(Of String, clTrinity.Trinity.cAddedValue) In bookedSpot.AddedValues
                                        If Not added.Value Is Nothing Then fullPrice *= added.Value.IndexNet / 100
                                    Next
                                    totalBudget += fullPrice
                                End If
                            End If

                        Next
                        e.Value = "Avg: " & Format(totalBudget / totalTRP30, "##,##0 kr")
                        'e.Value = Format(DirectCast(TmpCol.Tag, CPPColumnTag).CPP, "C1")
                    Case Else
                        e.Value = ""
                End Select
                grdSpotlist.Rows(e.RowIndex).DefaultCellStyle.BackColor = Drawing.Color.Gray
            End If
            Exit Sub
        End If

        Dim TmpSpot As Trinity.cBookedSpot = Campaign.BookedSpots(grdSpotlist.Rows(e.RowIndex).Tag)

        If TmpSpot Is Nothing Then
            TmpSpot = otherBookedSpots(grdSpotlist.Rows(e.RowIndex).Tag)
        End If

        If TmpSpot Is Nothing Then Exit Sub

        If TmpCol.HeaderText = "ID" Then
            e.Value = TmpSpot.ID
        ElseIf TmpCol.HeaderText = "DateTimeSerial" Then
            e.Value = Trinity.Helper.DateTimeSerial(TmpSpot.AirDate, TmpSpot.MaM)
        ElseIf TmpCol.Visible = True Then
            Est = TmpSpot.MyEstimate
            EstBT = TmpSpot.MyEstimateBuyTarget
            Select Case TmpCol.HeaderText
                Case "Date"
                    e.Value = Format(TmpSpot.AirDate, "Short Date")
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = True
                Case "Week"
                    e.Value = DatePart(DateInterval.WeekOfYear, TmpSpot.AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = True
                Case "Weekday"
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = True
                    e.Value = Days(Weekday(TmpSpot.AirDate, vbMonday) - 1)
                Case "Time"
                    e.Value = Trinity.Helper.Mam2Tid(TmpSpot.MaM)
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = True
                Case "Channel"
                    e.Value = TmpSpot.Channel.Shortname
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = True
                Case "Program"
                    e.Value = TmpSpot.Programme
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = True
                Case "Gross Price"
                    e.Value = Format(TmpSpot.GrossPrice * TmpSpot.AddedValueIndex(False), "##,##0 kr")
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = False
                Case "Chan Est"
                    e.Value = Format(TmpSpot.ChannelEstimate, "0.0")
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = False
                Case "Net Price"
                    e.Value = Format(TmpSpot.NetPrice * TmpSpot.AddedValueIndex(True), "##,##0 kr")
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = False
                Case "Notes"
                    e.Value = TmpSpot.Comments
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = False
                Case "Bid"
                    e.Value = TmpSpot.Bid
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = True
                Case "Remarks"
                    If TmpSpot.IsLocal Then
                        e.Value = "Local"
                    End If
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = True
                Case "Gross CPP"
                    If TmpSpot.ChannelEstimate > 0 Then
                        e.Value = Format((TmpSpot.NetPrice * TmpSpot.AddedValueIndex) / TmpSpot.ChannelEstimate, "##,##0 kr")
                    Else
                        e.Value = "0 kr"
                    End If
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = True
                Case "CPP (Main)"
                    If Est > 0 Then
                        e.Value = Format((TmpSpot.NetPrice * TmpSpot.AddedValueIndex) / Est, "##,##0 kr")
                    Else
                        e.Value = "0 kr"
                    End If
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = True
                Case "CPP 30"" (Main)"
                    If Est > 0 AndAlso TmpSpot.Film IsNot Nothing Then
                        e.Value = Format((TmpSpot.NetPrice * TmpSpot.AddedValueIndex) / (Est * (TmpSpot.Film.Index / 100)), "##,##0 kr")
                    Else
                        e.Value = "0 kr"
                    End If
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = True
                Case "CPP (Chn Est)"
                    If TmpSpot.ChannelEstimate > 0 Then
                        e.Value = Format((TmpSpot.NetPrice * TmpSpot.AddedValueIndex) / TmpSpot.ChannelEstimate, "##,##0 kr")
                    Else
                        e.Value = "0 kr"
                    End If
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = True
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = True
                Case "Est"
                    e.Value = Format(TmpSpot.MyEstimate, "0.0")
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = False
                Case "Est (Buying)"
                    e.Value = Format(EstBT, "0.0")
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = False
                Case "Filmcode"
                    e.Value = TmpSpot.Filmcode
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = True
                Case "Film"
                    If Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(TmpSpot.Filmcode) Is Nothing Then
                        e.Value = "<unknown>"
                    Else
                        e.Value = Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(TmpSpot.Filmcode).Name
                    End If
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = True
                Case "Film dscr"
                    If Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(TmpSpot.Filmcode) Is Nothing Then
                        e.Value = ""
                    Else
                        e.Value = Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(TmpSpot.Filmcode).Description
                    End If
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = True
                Case "Added value"
                    TmpStr = ""
                    For Each kv In TmpSpot.AddedValues
                        If Not kv.Value Is Nothing Then TmpStr = TmpStr + kv.Value.Name & "/"
                    Next
                    TmpStr = TmpStr.TrimEnd("/")
                    e.Value = TmpStr
                    grdSpotlist.Rows(e.RowIndex).Cells(TmpCol.Name).ReadOnly = True
            End Select
        End If
    End Sub

    'Sub UpdateSpotlist_old()
    '    Dim Chan As String
    '    Dim BT As String
    '    Dim SaveRow As Integer
    '    Dim Savelist As Windows.Forms.DataGridViewSelectedRowCollection
    '    Dim TmpSpot As Trinity.cBookedSpot
    '    Dim TmpCol As System.Windows.Forms.DataGridViewColumn
    '    Dim Days() As String = {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"}
    '    Dim c As Integer
    '    Dim TmpStr As String
    '    Dim kv As KeyValuePair(Of String, Trinity.cAddedValue)
    '    Dim Est As Single
    '    Dim EstBT As Single

    '    Me.Cursor = Windows.Forms.Cursors.WaitCursor
    '    If cmbChannel.SelectedIndex = -1 Then Exit Sub
    '    Chan = cmbChannel.Text.Substring(0, InStr(cmbChannel.Text, " ") - 1)
    '    BT = Mid(cmbChannel.Text, InStr(cmbChannel.Text, " ") + 1)

    '    SaveRow = grdSpotlist.FirstDisplayedScrollingRowIndex
    '    Savelist = grdSpotlist.SelectedRows
    '    grdSpotlist.SuspendLayout()
    '    grdSpotlist.ReadOnly = False
    '    grdSpotlist.Rows.Clear()
    '    grdSpotlist.Columns.Clear()
    '    grdSpotlist.Columns.Add("colSpotlistID", "ID")
    '    grdSpotlist.Columns("colSpotlistID").Tag = "ID"
    '    grdSpotlist.Columns("colSpotlistID").Visible = False
    '    grdSpotlist.Columns.Add("colSpotlistDateTimeSerial", "DateTimeSerial")
    '    grdSpotlist.Columns("colSpotlistDateTimeSerial").Tag = "DateTimeSerial"
    '    grdSpotlist.Columns("colSpotlistDateTimeSerial").Visible = False
    '    SkipIt = True
    '    For c = 1 To TrinitySettings.SpotlistColumnCount
    '        grdSpotlist.Columns.Add("colSpotlist" & TrinitySettings.SpotlistColumn(c), TrinitySettings.SpotlistColumn(c))
    '        grdSpotlist.Columns("colSpotlist" & TrinitySettings.SpotlistColumn(c)).Tag = TrinitySettings.SpotlistColumn(c)
    '        If TrinitySettings.SpotlistColumnWidth(c) > 0 Then
    '            grdSpotlist.Columns("colSpotlist" & TrinitySettings.SpotlistColumn(c)).Width = TrinitySettings.SpotlistColumnWidth(c)
    '        ElseIf TrinitySettings.SpotlistColumnWidth(c) = 0 Then
    '            grdSpotlist.Columns("colSpotlist" & TrinitySettings.SpotlistColumn(c)).Visible = False
    '        Else
    '            grdSpotlist.Columns("colSpotlist" & TrinitySettings.SpotlistColumn(c)).Width = 100
    '        End If
    '    Next
    '    SkipIt = False
    '    For Each TmpSpot In Campaign.BookedSpots
    '        If TmpSpot.Channel.ChannelName = Chan And TmpSpot.Bookingtype.Name = BT Then
    '            If Campaign.ExtendedInfos.Exists(TmpSpot.DatabaseID) Then
    '                DirectCast(Campaign.ExtendedInfos(TmpSpot.DatabaseID), Trinity.cExtendedInfo).IsBooked = True
    '            End If
    '            grdSpotlist.Rows.Add()
    '            If TmpSpot.MostRecentlyBooked Then
    '                grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).DefaultCellStyle.ForeColor = Drawing.Color.Blue
    '            ElseIf TmpSpot.RecentlyBooked Then
    '                grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).DefaultCellStyle.ForeColor = Drawing.Color.Green
    '            End If
    '            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Tag = TmpSpot.ID
    '            For Each TmpCol In grdSpotlist.Columns
    '                If TmpCol.Tag = "ID" Then
    '                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = TmpSpot.ID
    '                ElseIf TmpCol.Tag = "DateTimeSerial" Then
    '                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = Trinity.Helper.DateTimeSerial(TmpSpot.AirDate, TmpSpot.MaM)
    '                ElseIf TmpCol.Visible = True Then
    '                    Est = TmpSpot.MyEstimate
    '                    EstBT = TmpSpot.MyEstimateBuyTarget
    '                    Select Case TmpCol.Tag
    '                        Case "Date"
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(TmpSpot.AirDate, "Short Date")
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
    '                        Case "Week"
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = DatePart(DateInterval.WeekOfYear, TmpSpot.AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
    '                        Case "Weekday"
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = Days(Weekday(TmpSpot.AirDate, vbMonday) - 1)
    '                        Case "Time"
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = Trinity.Helper.Mam2Tid(TmpSpot.MaM)
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
    '                        Case "Channel"
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = TmpSpot.Channel.Shortname
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
    '                        Case "Program"
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = TmpSpot.Programme
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
    '                        Case "Gross Price"
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(TmpSpot.GrossPrice * TmpSpot.AddedValueIndex(False), "##,##0 kr")
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = False
    '                        Case "Chan Est"
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(TmpSpot.ChannelEstimate, "0.0")
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = False
    '                        Case "Net Price"
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(TmpSpot.NetPrice * TmpSpot.AddedValueIndex, "##,##0 kr")
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = False
    '                        Case "Notes"
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = TmpSpot.Comments
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = False
    '                        Case "Remarks"
    '                            If TmpSpot.IsLocal Then
    '                                grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = "Local"
    '                            End If
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
    '                        Case "Gross CPP"
    '                            If TmpSpot.ChannelEstimate > 0 Then
    '                                grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = Format((TmpSpot.NetPrice * TmpSpot.AddedValueIndex) / TmpSpot.ChannelEstimate, "##,##0 kr")
    '                            Else
    '                                grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = "0 kr"
    '                            End If
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
    '                        Case "CPP (" & Campaign.MainTarget.TargetNameNice & ")"
    '                            If Est > 0 Then
    '                                grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = Format((TmpSpot.NetPrice * TmpSpot.AddedValueIndex) / Est, "##,##0 kr")
    '                            Else
    '                                grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = "0 kr"
    '                            End If
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
    '                        Case "CPP (Chn Est)"
    '                            If TmpSpot.ChannelEstimate > 0 Then
    '                                grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = Format((TmpSpot.NetPrice * TmpSpot.AddedValueIndex) / TmpSpot.ChannelEstimate, "##,##0 kr")
    '                            Else
    '                                grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = "0 kr"
    '                            End If
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
    '                        Case "Est"
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(TmpSpot.MyEstimate, "0.0")
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = False
    '                        Case "Est (" & Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.TargetName & ")"
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = Format(EstBT, "0.0")
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = False
    '                        Case "Filmcode"
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = TmpSpot.Filmcode
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
    '                        Case "Film"
    '                            If Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(TmpSpot.Filmcode) Is Nothing Then
    '                                grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = "<unknown>"
    '                            Else
    '                                grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(TmpSpot.Filmcode).Name
    '                            End If
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
    '                        Case "Film dscr"
    '                            If Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(TmpSpot.Filmcode) Is Nothing Then
    '                                grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = ""
    '                            Else
    '                                grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(TmpSpot.Filmcode).Description
    '                            End If
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
    '                        Case "Added value"
    '                            TmpStr = ""
    '                            For Each kv In TmpSpot.AddedValues
    '                                TmpStr = TmpStr + kv.Value.Name & "/"
    '                            Next
    '                            TmpStr = TmpStr.TrimEnd("/")
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).Value = TmpStr
    '                            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
    '                    End Select
    '                End If

    '            Next
    '        Else

    '        End If
    '    Next
    '    grdSpotlist.AutoResizeRows()
    '    grdSpotlist.Sort(grdSpotlist.Columns("colSpotlistDateTimeSerial"), ComponentModel.ListSortDirection.Ascending)
    '    If Not SaveRow > grdSpotlist.Rows.Count AndAlso SaveRow > -1 Then
    '        Try
    '            grdSpotlist.FirstDisplayedScrollingRowIndex = SaveRow
    '        Catch

    '        End Try
    '    End If
    '    If Campaign.Channels(Chan).BookingTypes(BT).AddedValues.Count = 0 Then
    '        cmdSpotlistAV.Enabled = False
    '    Else
    '        cmdSpotlistAV.Enabled = True
    '    End If
    '    grdSpotlist.ResumeLayout()
    '    Me.Cursor = Windows.Forms.Cursors.Default

    'End Sub

    Private Class SortSpotlist
        Implements System.Collections.IComparer

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
            Dim DataGridViewRow1 As System.Windows.Forms.DataGridViewRow = CType(x, System.Windows.Forms.DataGridViewRow)
            Dim DataGridViewRow2 As System.Windows.Forms.DataGridViewRow = CType(y, System.Windows.Forms.DataGridViewRow)

            Dim CompareResult As Integer = System.String.Compare( _
                DataGridViewRow1.Cells("colSpotlistDateTimeSerial").Value, _
                DataGridViewRow2.Cells("colSpotlistDateTimeSerial").Value)

            Return CompareResult
        End Function
    End Class

    Sub UpdatePlannedTRP()
        Dim Chan As String
        Dim BT As String
        Dim TmpWeek As Trinity.cWeek
        Dim i As Integer

        If cmbChannel.SelectedIndex = -1 Then Exit Sub
        Chan = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        BT = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Name

        grdPlannedTRP.Columns(0).Tag = 0
        grdPlannedTRP.Columns(1).Tag = 0
        grdPlannedTRP.Columns(3).Tag = 0
        grdPlannedTRP.Rows.Clear()
        grdPlannedTRP.Height = 10000
        For Each TmpWeek In Campaign.Channels(Chan).BookingTypes(BT).Weeks
            If Not Me.Controls("lblWeek" & TmpWeek.Name) Is Nothing Then
                Me.Controls.RemoveByKey("lblWeek" & TmpWeek.Name)
            End If
            Dim tmpLabel As New System.Windows.Forms.Label
            tmpLabel.Name = "lblWeek" & TmpWeek.Name
            tmpLabel.Text = TmpWeek.Name
            tmpLabel.Parent = grpPlannedTRP
            grdPlannedTRP.Rows.Add()
            grdPlannedTRP.Rows(grdPlannedTRP.Rows.Count - 1).HeaderCell.Value = TmpWeek.Name
            grdPlannedTRP.Rows(grdPlannedTRP.Rows.Count - 1).Cells(0).Value = Format(TmpWeek.TRPBuyingTarget, "N1")
            grdPlannedTRP.Rows(grdPlannedTRP.Rows.Count - 1).Cells(1).Value = Format(TmpWeek.TRP, "N1")
            grdPlannedTRP.Rows(grdPlannedTRP.Rows.Count - 1).Cells(3).Value = Format(TmpWeek.NetBudget, "N0")
            grdPlannedTRP.Columns(0).Tag = grdPlannedTRP.Columns(0).Tag + TmpWeek.TRPBuyingTarget
            grdPlannedTRP.Columns(1).Tag = grdPlannedTRP.Columns(1).Tag + TmpWeek.TRP
            grdPlannedTRP.Columns(3).Tag = grdPlannedTRP.Columns(3).Tag + TmpWeek.NetBudget
            grdPlannedTRP.Rows(grdPlannedTRP.Rows.Count - 1).Height = grdPlannedTRP.Rows(grdPlannedTRP.Rows.Count - 1).GetPreferredHeight(0, Windows.Forms.DataGridViewAutoSizeRowMode.AllCells, True)
            grdPlannedTRP.AutoResizeRow(grdPlannedTRP.Rows.Count - 1)
            tmpLabel.Top = grdPlannedTRP.GetRowDisplayRectangle(grdPlannedTRP.Rows.Count - 1, False).Top + grdPlannedTRP.Top
            tmpLabel.Height = grdPlannedTRP.GetRowDisplayRectangle(grdPlannedTRP.Rows.Count - 1, False).Height
            tmpLabel.Left = 1
            tmpLabel.Width = grdPlannedTRP.Left - 1
            tmpLabel.TextAlign = Drawing.ContentAlignment.MiddleRight
        Next
        For i = 0 To grdPlannedTRP.Rows.Count - 1
            If grdPlannedTRP.Columns(1).Tag > 0 Then
                grdPlannedTRP.Rows(i).Cells(2).Value = Format(grdPlannedTRP.Rows(i).Cells(1).Value / grdPlannedTRP.Columns(1).Tag, "0%")
            Else
                grdPlannedTRP.Rows(i).Cells(2).Value = "0%"
            End If
        Next
        If Not Me.Controls("lblSumPlannedTRP") Is Nothing Then
            Me.Controls.RemoveByKey("lblSumPlannedTRP")
        End If
        Dim tmpSumLabel As New System.Windows.Forms.Label
        tmpSumLabel.Name = "lblSumPlannedTRP"
        tmpSumLabel.Text = "Sum:"
        tmpSumLabel.Parent = grpPlannedTRP
        grdPlannedTRP.Rows.Add()
        grdPlannedTRP.Rows(grdPlannedTRP.Rows.Count - 1).Cells(0).Value = Format(grdPlannedTRP.Columns(0).Tag, "N1")
        grdPlannedTRP.Rows(grdPlannedTRP.Rows.Count - 1).Cells(1).Value = Format(grdPlannedTRP.Columns(1).Tag, "N1")
        grdPlannedTRP.Rows(grdPlannedTRP.Rows.Count - 1).Cells(2).Value = "100%"
        grdPlannedTRP.Rows(grdPlannedTRP.Rows.Count - 1).Cells(3).Value = Format(grdPlannedTRP.Columns(3).Tag, "N0")

        grdPlannedTRP.AutoResizeRow(grdPlannedTRP.Rows.Count - 1)
        tmpSumLabel.Top = grdPlannedTRP.GetRowDisplayRectangle(grdPlannedTRP.Rows.Count - 1, False).Top + grdPlannedTRP.Top
        tmpSumLabel.Height = grdPlannedTRP.GetRowDisplayRectangle(grdPlannedTRP.Rows.Count - 1, False).Height
        tmpSumLabel.Left = 1
        tmpSumLabel.Width = grdPlannedTRP.Left - 1
        tmpSumLabel.TextAlign = Drawing.ContentAlignment.MiddleRight

        grdPlannedTRP.Height = grdPlannedTRP.Rows.GetRowsHeight(Windows.Forms.DataGridViewElementStates.None) + 3
        lblChanHeadline.Left = grdPlannedTRP.Left
        lblChanHeadline.Width = grdPlannedTRP.GetColumnDisplayRectangle(0, False).Width
        lblEstHeadline.Left = lblChanHeadline.Right + 1
        lblEstHeadline.Width = grdPlannedTRP.GetColumnDisplayRectangle(1, False).Width
        lblPercentHeadline.Left = lblEstHeadline.Right + 1
        lblPercentHeadline.Width = grdPlannedTRP.GetColumnDisplayRectangle(2, False).Width
        lblTRPHeadline.Left = lblChanHeadline.Left
        lblTRPHeadline.Width = lblPercentHeadline.Right - lblTRPHeadline.Left
        lblNetHeadline.Left = lblPercentHeadline.Right + 1
        lblNetHeadline.Width = grdPlannedTRP.GetColumnDisplayRectangle(3, False).Width
        lblBudgetHeadline.Left = lblNetHeadline.Left
        lblBudgetHeadline.Width = lblNetHeadline.Width
    End Sub

    Sub UpdatePictureAndInfo(ID As String)
        Dim TmpEI As Trinity.cExtendedInfo
        Dim Chan As String
        Dim BT As String

        If cmbChannel.SelectedIndex = -1 Then Exit Sub
        If ID = "" Then Exit Sub
        Chan = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        BT = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Name

        If Campaign.ExtendedInfos.Count = 0 Then UpdateSchedule(True, False)

        TmpEI = Campaign.ExtendedInfos(ID)

        Dim _thread As New Threading.Thread(Sub()
                                                Try
                                                    Dim _prog As String = TmpEI.ProgAfter
                                                    If _prog.LastIndexOf(" ") = _prog.Length - 2 AndAlso IsNumeric(_prog.Substring(_prog.Length - 1)) Then
                                                        _prog = _prog.Substring(0, _prog.LastIndexOf(" "))
                                                    End If
                                                    Dim _isFilm As Boolean = False
                                                    If _prog.ToLower.StartsWith("film:") Then
                                                        _isFilm = True
                                                    End If
                                                    _prog = _prog.ToLower.Replace("film:", "").Trim
                                                    cmdImdb.Tag = _prog
                                                    _prog = _prog.Replace(" ", "").Replace("&", "").Replace("å", "a").Replace("ä", "a").Replace("ö", "o").Replace("'", "").Replace("(r)", "")
                                                    If _prog.IndexOf(",") >= 0 Then
                                                        _prog = _prog.Substring(_prog.IndexOf(",") + 1) + _prog.Substring(0, _prog.IndexOf(","))
                                                    End If

                                                    Dim _infoFound As Boolean = True
                                                    If Not FindTVInfo(_prog, _isFilm) Then
                                                        If Not _prog.StartsWith("the") OrElse Not FindTVInfo(_prog.Substring(3), _isFilm) Then
                                                            _infoFound = False
                                                            Invoke(Sub()
                                                                       lblInfo.Text = ""
                                                                   End Sub)
                                                        End If
                                                    End If

                                                    If _isFilm Then
                                                        Dim _search As New MovieSearch.MovieSearch
                                                        AddHandler _search.SearchResult, Sub(result As MovieSearch.Movie)
                                                                                             Try
                                                                                                 If result IsNot Nothing AndAlso result.TMDB IsNot Nothing Then
                                                                                                     For i As Integer = 1 To 10
                                                                                                         If i <= Math.Round(result.RottenTomatoes.Ratings.CriticsScore / 10) Then
                                                                                                             'DirectCast(pnlScore.Controls(10 - i), PictureBox).Image = My.Resources.star_yellow
                                                                                                         Else
                                                                                                             DirectCast(pnlScore.Controls(10 - i), PictureBox).Image = My.Resources.star_grey
                                                                                                         End If
                                                                                                     Next
                                                                                                     Invoke(Sub()
                                                                                                                Try
                                                                                                                    If Not _infoFound Then
                                                                                                                        lblInfo.Text = result.TMDB.Overview
                                                                                                                        If result.TMDB.Posters.Count > 0 Then
                                                                                                                            Dim request = WebRequest.Create(result.TMDB.Posters.First.Info.URL)
                                                                                                                            Dim response = CType(request.GetResponse(), HttpWebResponse)
                                                                                                                            picPicture.Image = Bitmap.FromStream(response.GetResponseStream)
                                                                                                                        End If
                                                                                                                    End If
                                                                                                                    cmdImdb.Tag = result.TMDB.IMDbID
                                                                                                                    pnlScore.Visible = True
                                                                                                                    UpdatePanelPositions()
                                                                                                                Catch ex As Exception

                                                                                                                End Try
                                                                                                            End Sub)
                                                                                                 Else
                                                                                                     Invoke(Sub()
                                                                                                                pnlScore.Visible = False
                                                                                                                UpdatePanelPositions()
                                                                                                            End Sub)
                                                                                                 End If
                                                                                             Catch ex As Exception

                                                                                             End Try
                                                                                         End Sub
                                                        _search.SearchAsync(cmdImdb.Tag)
                                                    End If
                                                Catch ex As Exception

                                                End Try
                                            End Sub)
        _thread.Start()
    End Sub

    Function FindTVInfo(Program As String, ByRef IsFilm As Boolean) As Boolean
        Try
            Dim request As WebRequest = WebRequest.Create(String.Format("http://www.tv.nu/program/{0}", Program))
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            If response.ResponseUri.ToString.Contains("/film/") Then
                IsFilm = True
            End If
            Dim _isFilm As Boolean = IsFilm

            Invoke(Sub()
                       cmdImdb.Visible = _isFilm
                       pnlScore.Visible = False
                   End Sub)
            Dim dataStream As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream, Encoding.GetEncoding("iso-8859-1"))
            Dim source As String = reader.ReadToEnd()
            Dim _regex As String = "<a\sclass=\""plink.*?>.*?img.*?src=\""(.*?)\"""
            Dim match As String = System.Text.RegularExpressions.Regex.Matches(source, _regex, System.Text.RegularExpressions.RegexOptions.Singleline)(0).Groups(1).Value
            'Dim url As String = match.Substring(match.IndexOf("src=") + 5)
            'url = url.Substring(0, url.LastIndexOf("alt") - 2)
            request = WebRequest.Create("http://www.tv.nu/" + match)
            response = CType(request.GetResponse(), HttpWebResponse)
            Invoke(Sub()
                       Try
                           picPicture.Image = Bitmap.FromStream(response.GetResponseStream)
                       Catch
                           picPicture.Image = Nothing
                       End Try
                   End Sub)
            _regex = "<!-- END BUBBLES -->.*?<p.*?>(.*?)</p>"
            match = System.Text.RegularExpressions.Regex.Matches(source, _regex, System.Text.RegularExpressions.RegexOptions.Singleline)(0).Groups(1).Value
            If match = "" Then
                Return False
            End If
            Invoke(Sub()
                       Try
                           lblInfo.Text = System.Text.RegularExpressions.Regex.Replace(match, "<a\s.*?</a>", "").Replace("<span>...</span><span class=""invisible cutoff"">", "").Replace("</span>", "").Replace("<br>", "")
                       Catch
                           lblInfo.Text = ""
                       End Try
                   End Sub)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Sub UpdateDetails(ByVal ID As String)
        Dim TmpEI As Trinity.cExtendedInfo
        Dim b As Integer
        Dim TmpPeriod As Trinity.cPeriod
        Dim Chan As String
        Dim BT As String

        If cmbChannel.SelectedIndex = -1 Then Exit Sub
        If ID = "" Then Exit Sub
        Chan = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        BT = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Name

        If Campaign.ExtendedInfos.Count = 0 Then UpdateSchedule(True, False)

        TmpEI = Campaign.ExtendedInfos(ID)

        grdDetails.Rows.Clear()
        If Not TmpEI.BreakList Is Nothing AndAlso TmpEI.BreakList.Count > 0 Then
            grdDetails.Rows.Add(TmpEI.BreakList.Count)
        End If
        grdDetails.Columns(4).HeaderText = Campaign.MainTarget.TargetNameNice
        If TmpEI.EstimationTarget <> "" Then
            grdDetails.Columns(5).HeaderText = TmpEI.EstimationTarget
        Else
            grdDetails.Columns(5).HeaderText = Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Target.TargetNameNice
        End If
        If Not TmpEI.EstimatedOnPeriod Is Nothing AndAlso Campaign.EstimationPeriods.Exists(TmpEI.EstimatedOnPeriod) Then
            TmpPeriod = DirectCast(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod), Trinity.cPeriod)
            If Not TmpEI.BreakList Is Nothing Then
                For b = 1 To TmpEI.BreakList.Count
                    grdDetails.Rows(b - 1).Cells(0).Value = Format(TmpEI.BreakList(b - 1).AirDate, "yyyyMMdd")
                    grdDetails.Rows(b - 1).Cells(1).Value = Trinity.Helper.Mam2Tid(TmpEI.BreakList(b - 1).MaM)
                    grdDetails.Rows(b - 1).Cells(2).Value = TmpEI.BreakList(b - 1).ProgBefore
                    grdDetails.Rows(b - 1).Cells(3).Value = TmpEI.BreakList(b - 1).ProgAfter
                    grdDetails.Rows(b - 1).Cells(4).Value = Format(TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, Campaign.MainTarget)), "0.0")
                    grdDetails.Rows(b - 1).Cells(5).Value = Format(TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Target)), "0.0")
                    grdDetails.AutoResizeRow(b - 1)
                Next
            End If
        End If
    End Sub

    Sub UpdatePanelPositions()

        If PlannedToolStripMenuItem.Checked Then
            grpPlannedTRP.Height = grdPlannedTRP.Bottom + 6
            grpPlannedTRP.Visible = True
        Else
            grpPlannedTRP.Height = 0
            grpPlannedTRP.Visible = False
        End If

        If BookedToolStripMenuItem.Checked Then
            grpBookedTRP.Height = grdBookedTRP.Bottom + 6
            grpBookedTRP.Visible = True
        Else
            grpBookedTRP.Height = 0
            grpBookedTRP.Visible = False
        End If
        grpBookedTRP.Top = grpPlannedTRP.Bottom + 6

        If LeftToBookToolStripMenuItem.Checked Then
            grpLeftToBook.Height = grdLeftToBook.Bottom + 6
            grpLeftToBook.Visible = True
        Else
            grpLeftToBook.Height = 0
            grpLeftToBook.Visible = False
        End If
        grpLeftToBook.Top = grpBookedTRP.Bottom + 6

        If FilmsToolStripMenuItem.Checked Then
            grpFilms.Height = grdFilms.Bottom + 10
            grpFilms.Visible = True
        Else
            grpFilms.Height = 0
            grpFilms.Visible = False
        End If
        grpFilms.Top = grpLeftToBook.Bottom + 6

        If DaypartsToolStripMenuItem.Checked Then
            grpDayparts.Height = grdDayparts.Bottom + 6
            grpDayparts.Visible = True
        Else
            grpDayparts.Height = 0
            grpDayparts.Visible = False
        End If
        grpDayparts.Top = grpFilms.Bottom + 6

        grpPeak.Height = chtPrimePeak.Bottom + 6
        grpPeak.Top = grpDayparts.Bottom + 6

        grpReach.Top = grpPeak.Bottom + 6
        grdReach.Height = 10000
        grpReach.Height = 10000
        grdReach.Height = grdReach.Rows.GetRowsHeight(Windows.Forms.DataGridViewElementStates.None) + grdReach.ColumnHeadersHeight + 3
        If ReachToolStripMenuItem.Checked Then
            grpReach.Height = grdReach.Bottom + 6
            grpReach.Visible = True
        Else
            grpReach.Height = 0
            grpReach.Visible = False
        End If

        grpEstProfile.Top = grpReach.Bottom + 6
        If Not EstimatedTargetProfileToolStripMenuItem.Checked Then
            grpEstProfile.Height = 0
            grpEstProfile.Visible = False
        Else
            grpEstProfile.Height = 93
            grpEstProfile.Visible = True
        End If

        grpPicture.Top = grpEstProfile.Bottom + 6
        If Not PictureToolStripMenuItem.Checked Then
            grpPicture.Height = 0
            grpPicture.Visible = False
        Else
            grpPicture.Height = 150
            grpPicture.Visible = True
        End If

        grpInfo.Top = grpPicture.Bottom + 6
        lblInfo.MaximumSize = New Size(grpInfo.Width - grpInfo.Left * 2 - IIf(cmdImdb.Visible, cmdImdb.Width, 0), 0)
        If Not InfoToolStripMenuItem.Checked Then
            grpInfo.Height = 0
            grpInfo.Visible = False
        Else
            If Not pnlScore.Visible Then
                grpInfo.Height = lblInfo.Height + lblInfo.Top * 2
            Else
                pnlScore.Top = lblInfo.Bottom
                grpInfo.Height = pnlScore.Bottom + 4
            End If
            grpInfo.Visible = True
        End If

        grpDetails.Top = grpInfo.Bottom + 6
        If Not DetailsToolStripMenuItem.Checked Then
            grpDetails.Height = 0
            grpDetails.Visible = False
        Else
            grdDetails.AutoSizeRowsMode = Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
            grdDetails.Height = 10000
            grpDetails.Height = 10000
            grdDetails.Height = grdDetails.Rows.GetRowsHeight(Windows.Forms.DataGridViewElementStates.None) + grdDetails.ColumnHeadersHeight + 3
            grpDetails.Height = grdDetails.Bottom + 6
            grpDetails.Visible = True
        End If

        grpOther.Top = grpDetails.Bottom + 6
        If Not ProgsInOtherChannelsToolStripMenuItem.Checked Then
            grpOther.Height = 0
            grpOther.Visible = False
        Else
            grdOther.AutoResizeRows()
            grdOther.Height = 10000
            grdOther.Height = grdOther.Rows.GetRowsHeight(Windows.Forms.DataGridViewElementStates.None) + grdOther.ColumnHeadersHeight + 3
            grpOther.Height = 10000
            grpOther.Height = grdOther.Bottom + 6
            grpOther.Visible = True
        End If

        grpTrend.Top = grpOther.Bottom + 6
        If TrendToolStripMenuItem.Checked Then
            grpTrend.Height = 93
            grpTrend.Visible = True
        Else
            grpTrend.Height = 0
            grpTrend.Visible = False
        End If

        grpProfile.Top = grpTrend.Bottom + 6
        If TargetProfileToolStripMenuItem.Checked Then
            grpProfile.Height = 93
            grpProfile.Visible = True
        Else
            grpProfile.Height = 0
            grpProfile.Visible = 0
        End If

        grpGender.Top = grpProfile.Bottom + 6
        If TargetProfileToolStripMenuItem.Checked Then
            grpGender.Height = 93
            grpGender.Visible = True
        Else
            grpGender.Height = 0
            grpGender.Visible = 0
        End If

        grpPIB.Top = grpGender.Bottom + 6
        If PositionInBreakToolStripMenuItem.Checked Then
            grpPIB.Height = 93
            grpPIB.Visible = True
        Else
            grpPIB.Height = 0
            grpPIB.Visible = False
        End If

    End Sub

    Sub UpdateOther(ByVal ID As String)
        Dim MaM As Integer
        Dim TmpDate As Date
        Dim TmpEI As Trinity.cExtendedInfo

        If ID = "" Then Exit Sub
        If Campaign.ExtendedInfos.Count = 0 Then UpdateSchedule(True, False)


        TmpEI = Campaign.ExtendedInfos(ID)
        MaM = Campaign.ExtendedInfos(ID).MaM
        TmpDate = Campaign.ExtendedInfos(ID).AirDate
        grdOther.AutoGenerateColumns = True
        grdOther.DataSource = Nothing
        grdOther.Columns.Clear()
        grdOther.Columns.Add("Wait", "Please wait...")
        grdOther.Columns(0).AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        grdOther.Rows.Add("Please wait...")
        grdOther.ColumnHeadersVisible = False

        'direct call from DBreader

        If UpdateThread IsNot Nothing Then
            If UpdateThread.IsAlive Then
                'UpdateThread.Abort()
            End If
        End If
        UpdateThread = New System.Threading.Thread(AddressOf FindProgs)
        UpdateThread.IsBackground = True
        UpdateThread.Start(New Object() {MaM, TmpDate, Campaign.Area})
    End Sub

    Sub FindProgs(ByVal Params As Object)
        DBReader.FindProgramDuring(DirectCast(Params(0), Integer), DirectCast(Params(1), Date), DirectCast(Params(2), String), Me)
    End Sub

    Sub SetOtherGrid(ByVal dt As DataTable)
        grdOther.Columns.Clear()
        grdOther.ColumnHeadersVisible = True
        grdOther.DataSource = dt
        'grdOther.DataSource = FindProgramDuring(MaM, TmpDate)
        grdOther.Columns(0).Width = 50
        grdOther.Columns(1).Width = 30
        grdOther.Columns(2).Width = 40
        UpdatePanelPositions()
    End Sub

    Sub UpdateRF()
        Dim i As Integer

        grdReach.Rows.Clear()
        grdReach.Columns.Clear()
        grdReach.Columns.Add("colReach", "Reach")
        grdReach.Columns(grdReach.Columns.Count - 1).Width = 60
        For i = 1 To Campaign.RFEstimation.ReferencePeriods.Count
            grdReach.Columns.Add("col" & Campaign.RFEstimation.ReferencePeriods(i).Name, Campaign.RFEstimation.ReferencePeriods(i).Name)
            grdReach.Columns(grdReach.Columns.Count - 1).Width = 60
        Next
        Dim lblTRP As New System.Windows.Forms.Label
        grdReach.Rows.Add()
        grdReach.Rows(grdReach.Rows.Count - 1).Cells(0).Value = Format(Campaign.RFEstimation.TRP, "N1")
        For j As Integer = 1 To Campaign.RFEstimation.ReferencePeriods.Count
            grdReach.Rows(grdReach.Rows.Count - 1).Cells(j).Value = Format(Campaign.RFEstimation.ReferencePeriods(j).TRP, "N1")
        Next
        lblTRP.Parent = grpReach
        lblTRP.Left = 1
        lblTRP.Top = grdReach.GetRowDisplayRectangle(grdReach.Rows.Count - 1, False).Top + grdReach.Top
        lblTRP.Height = grdReach.GetRowDisplayRectangle(grdReach.Rows.Count - 1, False).Height
        lblTRP.TextAlign = Drawing.ContentAlignment.MiddleRight
        lblTRP.Text = "TRP"
        lblTRP.Width = grdReach.Left - 2
        For i = 1 To 5
            Dim lblFreq As New System.Windows.Forms.Label
            grdReach.Rows.Add()
            grdReach.Rows(grdReach.Rows.Count - 1).Cells(0).Value = Format(Campaign.RFEstimation.Reach(i), "N1")
            For j As Integer = 1 To Campaign.RFEstimation.ReferencePeriods.Count
                grdReach.Rows(grdReach.Rows.Count - 1).Cells(j).Value = Format(Campaign.RFEstimation.ReferencePeriods(j).Reach(i), "N1")
            Next
            grdReach.AutoResizeRow(grdReach.Rows.Count - 1)
            lblFreq.Parent = grpReach
            lblFreq.Left = 1
            lblFreq.Top = grdReach.GetRowDisplayRectangle(grdReach.Rows.Count - 1, False).Top + grdReach.Top
            lblFreq.Height = grdReach.GetRowDisplayRectangle(grdReach.Rows.Count - 1, False).Height
            lblFreq.TextAlign = Drawing.ContentAlignment.MiddleRight
            lblFreq.Text = i & "+"
            lblFreq.Width = grdReach.Left - 2
        Next
    End Sub

    Sub UpdateLeftToBook()
        Dim Chan As String
        Dim BT As String
        Dim TmpWeek As Trinity.cWeek
        'Dim TmpSpot As Trinity.cBookedSpot
        Dim i As Integer

        If cmbChannel.SelectedIndex = -1 Then Exit Sub
        Chan = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        BT = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Name

        grdLeftToBook.Columns(0).Tag = 0
        grdLeftToBook.Columns(1).Tag = 0
        grdLeftToBook.Columns(3).Tag = 0
        grdLeftToBook.Rows.Clear()
        grdLeftToBook.Height = 10000
        For Each TmpWeek In Campaign.Channels(Chan).BookingTypes(BT).Weeks
            If Not Me.Controls("lblWeek" & TmpWeek.Name) Is Nothing Then
                Me.Controls.RemoveByKey("lblWeek" & TmpWeek.Name)
            End If
            Dim tmpLabel As New System.Windows.Forms.Label
            tmpLabel.Name = "lblWeek" & TmpWeek.Name
            tmpLabel.Text = TmpWeek.Name
            tmpLabel.Parent = grpLeftToBook
            grdLeftToBook.Rows.Add()
            grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(0).Value = 0
            grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(1).Value = 0
            grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(3).Value = 0
            grdLeftToBook.AutoResizeRow(grdLeftToBook.Rows.Count - 1)
            tmpLabel.Top = grdLeftToBook.GetRowDisplayRectangle(grdLeftToBook.Rows.Count - 1, False).Top + grdLeftToBook.Top
            tmpLabel.Height = grdLeftToBook.GetRowDisplayRectangle(grdLeftToBook.Rows.Count - 1, False).Height
            tmpLabel.Left = 1
            tmpLabel.Width = grdLeftToBook.Left - 1
            tmpLabel.TextAlign = Drawing.ContentAlignment.MiddleRight

            grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(0).Value = grdPlannedTRP.Rows(grdLeftToBook.Rows.Count - 1).Cells(0).Value - grdBookedTRP.Rows(grdLeftToBook.Rows.Count - 1).Cells(0).Value
            grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(1).Value = grdPlannedTRP.Rows(grdLeftToBook.Rows.Count - 1).Cells(1).Value - grdBookedTRP.Rows(grdLeftToBook.Rows.Count - 1).Cells(1).Value
            grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(3).Value = grdPlannedTRP.Rows(grdLeftToBook.Rows.Count - 1).Cells(3).Value - grdBookedTRP.Rows(grdLeftToBook.Rows.Count - 1).Cells(3).Value

            grdLeftToBook.Columns(0).Tag += grdPlannedTRP.Rows(grdLeftToBook.Rows.Count - 1).Cells(0).Value - grdBookedTRP.Rows(grdLeftToBook.Rows.Count - 1).Cells(0).Value
            grdLeftToBook.Columns(1).Tag += grdPlannedTRP.Rows(grdLeftToBook.Rows.Count - 1).Cells(1).Value - grdBookedTRP.Rows(grdLeftToBook.Rows.Count - 1).Cells(1).Value
            grdLeftToBook.Columns(3).Tag += grdPlannedTRP.Rows(grdLeftToBook.Rows.Count - 1).Cells(3).Value - grdBookedTRP.Rows(grdLeftToBook.Rows.Count - 1).Cells(3).Value

            If grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(1).Value > 0 Then
                grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(1).Style.ForeColor = Drawing.Color.Red
            Else
                grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(1).Style.ForeColor = Drawing.Color.Green
            End If
            If grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(3).Value < 0 Then
                grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(3).Style.ForeColor = Drawing.Color.Red
            Else
                grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(3).Style.ForeColor = Drawing.Color.Green
            End If
            grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Height = grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).GetPreferredHeight(0, Windows.Forms.DataGridViewAutoSizeRowMode.AllCells, True)
            grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(0).Value = Format(grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(0).Value, "N1")
            grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(1).Value = Format(grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(1).Value, "N1")
            grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(3).Value = Format(grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(3).Value, "N0")
        Next
        For i = 0 To grdLeftToBook.Rows.Count - 1
            If grdLeftToBook.Columns(1).Tag > 0 Then
                grdLeftToBook.Rows(i).Cells(2).Value = Format(grdLeftToBook.Rows(i).Cells(1).Value / grdLeftToBook.Columns(1).Tag, "0%")
            Else
                grdLeftToBook.Rows(i).Cells(2).Value = "0%"
            End If
        Next
        If Not Me.Controls("lblSumLeftToBook") Is Nothing Then
            Me.Controls.RemoveByKey("lblSumLeftToBook")
        End If
        Dim tmpSumLabel As New System.Windows.Forms.Label
        tmpSumLabel.Name = "lblSumLeftToBook"
        tmpSumLabel.Text = "Sum:"
        tmpSumLabel.Parent = grpLeftToBook
        grdLeftToBook.Rows.Add()
        grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(0).Value = Format(grdLeftToBook.Columns(0).Tag, "N1")
        grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(1).Value = Format(grdLeftToBook.Columns(1).Tag, "N1")
        grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(2).Value = "100%"
        grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(3).Value = Format(grdLeftToBook.Columns(3).Tag, "N0")

        grdLeftToBook.AutoResizeRow(grdLeftToBook.Rows.Count - 1)
        If grdLeftToBook.Columns(1).Tag > 0 Then
            grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(1).Style.ForeColor = Drawing.Color.Red
        Else
            grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(1).Style.ForeColor = Drawing.Color.Green
        End If
        If grdLeftToBook.Columns(3).Tag < 0 Then
            grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(3).Style.ForeColor = Drawing.Color.Red
        Else
            grdLeftToBook.Rows(grdLeftToBook.Rows.Count - 1).Cells(3).Style.ForeColor = Drawing.Color.Green
        End If
        tmpSumLabel.Top = grdLeftToBook.GetRowDisplayRectangle(grdLeftToBook.Rows.Count - 1, False).Top + grdLeftToBook.Top
        tmpSumLabel.Height = grdLeftToBook.GetRowDisplayRectangle(grdLeftToBook.Rows.Count - 1, False).Height
        tmpSumLabel.Left = 1
        tmpSumLabel.Width = grdLeftToBook.Left - 1
        tmpSumLabel.TextAlign = Drawing.ContentAlignment.MiddleRight

        grdLeftToBook.Height = grdLeftToBook.Rows.GetRowsHeight(Windows.Forms.DataGridViewElementStates.None) + 3

        lblLeftChanHeadline.Left = grdLeftToBook.Left
        lblLeftChanHeadline.Width = grdLeftToBook.GetColumnDisplayRectangle(0, False).Width
        lblLeftEstHeadline.Left = lblLeftChanHeadline.Right + 1
        lblLeftEstHeadline.Width = grdLeftToBook.GetColumnDisplayRectangle(1, False).Width
        lblLeftPercentHeadline.Left = lblLeftEstHeadline.Right + 1
        lblLeftPercentHeadline.Width = grdLeftToBook.GetColumnDisplayRectangle(2, False).Width
        lblLeftTRPHeadline.Left = lblLeftChanHeadline.Left
        lblLeftTRPHeadline.Width = lblLeftPercentHeadline.Right - lblLeftTRPHeadline.Left
        lblLeftNetHeadline.Left = lblLeftPercentHeadline.Right + 1
        lblLeftNetHeadline.Width = grdLeftToBook.GetColumnDisplayRectangle(3, False).Width
        lblLeftBudgetHeadline.Left = lblLeftNetHeadline.Left
        lblLeftBudgetHeadline.Width = lblLeftNetHeadline.Width
    End Sub

    Sub UpdateBookedTRP()
        Dim tmpChannel As Trinity.cChannel
        Dim Chan As String
        Dim BT As String
        Dim TmpWeek As Trinity.cWeek
        Dim TmpSpot As Trinity.cBookedSpot
        Dim i As Integer
        Dim k5NetProblem As Boolean = False

        If cmbChannel.SelectedIndex = -1 Then Exit Sub
        Chan = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        BT = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Name
        'tmpChannel = DirectCast(cmbChannel.SelectedItem, Trinity.cChannel)

        grdBookedTRP.Columns(0).Tag = 0
        grdBookedTRP.Columns(1).Tag = 0
        grdBookedTRP.Columns(3).Tag = 0
        grdBookedTRP.Rows.Clear()
        grdBookedTRP.Height = 10000
        For Each TmpWeek In Campaign.Channels(Chan).BookingTypes(BT).Weeks
            If Not Me.Controls("lblWeek" & TmpWeek.Name) Is Nothing Then
                Me.Controls.RemoveByKey("lblWeek" & TmpWeek.Name)
            End If
            Dim tmpLabel As New System.Windows.Forms.Label
            tmpLabel.Name = "lblWeek" & TmpWeek.Name
            tmpLabel.Text = TmpWeek.Name
            tmpLabel.Parent = grpBookedTRP
            grdBookedTRP.Rows.Add()
            grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(0).Value = 0
            grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(1).Value = 0
            grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(3).Value = 0
            grdBookedTRP.AutoResizeRow(grdBookedTRP.Rows.Count - 1)
            tmpLabel.Top = grdBookedTRP.GetRowDisplayRectangle(grdBookedTRP.Rows.Count - 1, False).Top + grdBookedTRP.Top
            tmpLabel.Height = grdBookedTRP.GetRowDisplayRectangle(grdBookedTRP.Rows.Count - 1, False).Height
            tmpLabel.Left = 1
            tmpLabel.Width = grdBookedTRP.Left - 1
            tmpLabel.TextAlign = Drawing.ContentAlignment.MiddleRight
            For Each TmpSpot In Campaign.BookedSpots
                If TmpSpot.AirDate.ToOADate >= TmpWeek.StartDate AndAlso TmpSpot.AirDate.ToOADate <= TmpWeek.EndDate AndAlso TmpSpot.Channel.ChannelName = Chan AndAlso TmpSpot.Bookingtype.Name = BT Then
                    grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(0).Value = grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(0).Value + TmpSpot.ChannelEstimate
                    grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(1).Value = grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(1).Value + TmpSpot.MyEstimate
                    grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(3).Value = grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(3).Value + (TmpSpot.NetPrice * TmpSpot.AddedValueIndex)
                    grdBookedTRP.Columns(0).Tag = grdBookedTRP.Columns(0).Tag + TmpSpot.ChannelEstimate
                    grdBookedTRP.Columns(1).Tag = grdBookedTRP.Columns(1).Tag + TmpSpot.MyEstimate
                    grdBookedTRP.Columns(3).Tag = grdBookedTRP.Columns(3).Tag + (TmpSpot.NetPrice * TmpSpot.AddedValueIndex)
                End If
                'Special för kanal5 då bekräftelser inte innehåller spottar med budget.
                If TmpSpot.Channel.ChannelName = "Kanal5" Or TmpSpot.Channel.ChannelName = "Kanal9" Then

                    If TmpWeek.NetBudget > 0 And TmpSpot.week Is TmpWeek Then
                        'grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(3).Value = TmpWeek.NetBudget
                    End If
                End If
            Next
            If grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(1).Value < TmpWeek.TRP Then
                grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(1).Style.ForeColor = Drawing.Color.Red
            Else
                grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(1).Style.ForeColor = Drawing.Color.Green
            End If
            If grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(3).Value > TmpWeek.NetBudget Then
                grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(3).Style.ForeColor = Drawing.Color.Red
            Else
                grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(3).Style.ForeColor = Drawing.Color.Green
            End If
            grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Height = grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).GetPreferredHeight(0, Windows.Forms.DataGridViewAutoSizeRowMode.AllCells, True)
            grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(0).Value = Format(grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(0).Value, "N1")
            grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(1).Value = Format(grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(1).Value, "N1")
            grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(3).Value = Format(grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(3).Value, "N0")
        Next
        For i = 0 To grdBookedTRP.Rows.Count - 1
            If grdBookedTRP.Columns(1).Tag > 0 Then
                grdBookedTRP.Rows(i).Cells(2).Value = Format(grdBookedTRP.Rows(i).Cells(1).Value / grdBookedTRP.Columns(1).Tag, "0%")
            Else
                grdBookedTRP.Rows(i).Cells(2).Value = "0%"
            End If
        Next
        If Not Me.Controls("lblSumBookedTRP") Is Nothing Then
            Me.Controls.RemoveByKey("lblSumBookedTRP")
        End If
        Dim tmpSumLabel As New System.Windows.Forms.Label
        tmpSumLabel.Name = "lblSumBookedTRP"
        tmpSumLabel.Text = "Sum:"
        tmpSumLabel.Parent = grpBookedTRP
        grdBookedTRP.Rows.Add()
        grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(0).Value = Format(grdBookedTRP.Columns(0).Tag, "N1")
        grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(1).Value = Format(grdBookedTRP.Columns(1).Tag, "N1")
        grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(2).Value = "100%"
        grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(3).Value = Format(grdBookedTRP.Columns(3).Tag, "N0")

        grdBookedTRP.AutoResizeRow(grdBookedTRP.Rows.Count - 1)
        If grdBookedTRP.Columns(1).Tag < grdPlannedTRP.Columns(1).Tag Then
            grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(1).Style.ForeColor = Drawing.Color.Red
        Else
            grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(1).Style.ForeColor = Drawing.Color.Green
        End If
        If grdBookedTRP.Columns(3).Tag > grdPlannedTRP.Columns(3).Tag Then
            grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(3).Style.ForeColor = Drawing.Color.Red
        Else
            grdBookedTRP.Rows(grdBookedTRP.Rows.Count - 1).Cells(3).Style.ForeColor = Drawing.Color.Green
        End If
        tmpSumLabel.Top = grdBookedTRP.GetRowDisplayRectangle(grdBookedTRP.Rows.Count - 1, False).Top + grdBookedTRP.Top
        tmpSumLabel.Height = grdBookedTRP.GetRowDisplayRectangle(grdBookedTRP.Rows.Count - 1, False).Height
        tmpSumLabel.Left = 1
        tmpSumLabel.Width = grdBookedTRP.Left - 1
        tmpSumLabel.TextAlign = Drawing.ContentAlignment.MiddleRight

        grdBookedTRP.Height = grdBookedTRP.Rows.GetRowsHeight(Windows.Forms.DataGridViewElementStates.None) + 3

        lblBookedChanHeadline.Left = grdBookedTRP.Left
        lblBookedChanHeadline.Width = grdBookedTRP.GetColumnDisplayRectangle(0, False).Width
        lblBookedEstHeadline.Left = lblBookedChanHeadline.Right + 1
        lblBookedEstHeadline.Width = grdBookedTRP.GetColumnDisplayRectangle(1, False).Width
        lblBookedPercentHeadline.Left = lblBookedEstHeadline.Right + 1
        lblBookedPercentHeadline.Width = grdBookedTRP.GetColumnDisplayRectangle(2, False).Width
        lblBookedTRPHeadline.Left = lblBookedChanHeadline.Left
        lblBookedTRPHeadline.Width = lblBookedPercentHeadline.Right - lblBookedTRPHeadline.Left
        lblBookedNetHeadline.Left = lblBookedPercentHeadline.Right + 1
        lblBookedNetHeadline.Width = grdBookedTRP.GetColumnDisplayRectangle(3, False).Width
        lblBookedBudgetHeadline.Left = lblBookedNetHeadline.Left
        lblBookedBudgetHeadline.Width = lblBookedNetHeadline.Width
    End Sub

    Sub UpdateTrend(ByVal ID As String)
        If Not ID Is Nothing AndAlso Campaign.ExtendedInfos.Exists(ID) AndAlso Not Campaign.ExtendedInfos(ID).EstimatedOnPeriod Is Nothing AndAlso Campaign.EstimationPeriods.Exists(Campaign.ExtendedInfos(ID).EstimatedOnPeriod) Then
            chtTrend.ExtendedInfo = Campaign.ExtendedInfos(ID)
            chtTrend.Period = Campaign.EstimationPeriods(Campaign.ExtendedInfos(ID).EstimatedOnPeriod)
        Else
            chtTrend.ExtendedInfo = Nothing
        End If
    End Sub

    Sub UpdateEstimatedTrend()

        'If ProfileThread IsNot Nothing Then
        '    If ProfileThread.IsAlive Then
        '        ProfileThread.Abort()
        '    End If
        'End If
        'ProfileThread = New System.Threading.Thread(AddressOf CreateEstimatedTrend)
        'ProfileThread.Start(New Object() {Campaign})
        CreateEstimatedTrend(Campaign)
    End Sub

    Sub CreateEstimatedTrend(ByVal Params As Object)
        Dim Camp As Trinity.cKampanj = Params
        Dim AgeTRP(0 To 13) As Single
        For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
            For t As Integer = 0 To 13
                If TmpSpot.AgeIndex(t).ToString <> "NaN" Then
                    AgeTRP(t) += TmpSpot.MyEstimate * TmpSpot.AgeIndex(t)
                End If
            Next
        Next
        DrawEstimatedProfileChart(AgeTRP)
    End Sub

    Sub DrawEstimatedProfileChart(ByVal AgeTRP() As Single)
        Dim AvgRating As Single = 0
        For i As Integer = 0 To 13
            chtEstProfile.AgeTRP(i) = AgeTRP(i)
            AvgRating += AgeTRP(i) / 14
        Next
        chtEstProfile.AverageRating = AvgRating
        chtEstProfile.Target = Campaign.MainTarget
    End Sub

    Sub UpdateProfile(ByVal ID As String)
        Dim AgeTRP(0 To 13) As Single
        Dim TmpSex As String
        Dim TmpStr As String
        Dim i As Integer
        Dim b As Integer
        Dim AvgRating As Single = 0

        If Not ID Is Nothing AndAlso Campaign.ExtendedInfos.Exists(ID) AndAlso DirectCast(Campaign.ExtendedInfos(ID), Trinity.cExtendedInfo).EstimatedOnPeriod IsNot Nothing AndAlso Campaign.EstimationPeriods.Exists(DirectCast(Campaign.ExtendedInfos(ID), Trinity.cExtendedInfo).EstimatedOnPeriod) Then
            Dim TmpEI As Trinity.cExtendedInfo = Campaign.ExtendedInfos(ID)
            If Campaign.MainTarget.TargetType = Trinity.cTarget.TargetTypeEnum.trgMnemonicTarget Then
                Select Case Campaign.MainTarget.TargetNameNice.ToUpper.ToString.Substring(0, 1)
                    Case "A" : TmpSex = ""
                    Case "M" : TmpSex = "M"
                    Case "W" : TmpSex = "W"
                    Case Else : TmpSex = ""
                End Select
            Else
                TmpSex = ""
            End If
            Dim TmpArray() As String = {TmpSex & "3-11", TmpSex & "12-14", TmpSex & "15-19", TmpSex & "20-24", TmpSex & "25-29", TmpSex & "30-34", TmpSex & "35-39", TmpSex & "40-44", TmpSex & "45-49", TmpSex & "50-54", TmpSex & "55-59", TmpSex & "60-64", TmpSex & "60-69", TmpSex & "70+"}

            For i = 0 To 13
                AgeTRP(i) = 0
                If Not TmpEI Is Nothing AndAlso Not TmpEI.BreakList Is Nothing Then
                    For b = 1 To TmpEI.BreakList.Count
                        TmpStr = TmpArray(i)
                        AgeTRP(i) += DirectCast(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod), Trinity.cPeriod).Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod).Adedge, TmpStr))
                        AvgRating += DirectCast(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod), Trinity.cPeriod).Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod).Adedge, TmpStr)) / 14
                    Next
                End If
                chtProfile.AgeTRP(i) = AgeTRP(i)
            Next
            chtProfile.AverageRating = AvgRating
            chtProfile.Target = Campaign.MainTarget
        End If

    End Sub

    Sub UpdatePIB(ByVal ID As String)
        Dim First As Single = 0
        Dim Middle As Single = 0
        Dim Last As Single = 0
        Dim b As Integer

        If ID Is Nothing OrElse Not Campaign.ExtendedInfos.Exists(ID) OrElse Campaign.ExtendedInfos(ID).breaklist Is Nothing Then
            chtPIB.First = 0
            chtPIB.Average = 0
            chtPIB.Last = 0
            Exit Sub
        End If
        Dim TmpEI As Trinity.cExtendedInfo = Campaign.ExtendedInfos(ID)
        If Not Campaign.EstimationPeriods.Exists(TmpEI.EstimatedOnPeriod) Then Exit Sub
        Dim TmpPeriod As Trinity.cPeriod = DirectCast(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod), Trinity.cPeriod)

        For b = 1 To TmpEI.BreakList.Count
            First = First + TmpPeriod.Adedge.getUnit(Connect.eUnits.uBreakFirstPIB, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, Campaign.MainTarget))
            Middle = Middle + TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, Campaign.MainTarget))
            Last = Last + TmpPeriod.Adedge.getUnit(Connect.eUnits.uBreakLastPIB, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, Campaign.MainTarget))
        Next
        If TmpEI.BreakList.Count > 0 Then
            chtPIB.First = First / TmpEI.BreakList.Count
            chtPIB.Average = Middle / TmpEI.BreakList.Count
            chtPIB.Last = Last / TmpEI.BreakList.Count
        Else
            chtPIB.First = 0
            chtPIB.Average = 0
            chtPIB.Last = 0
        End If

    End Sub

    '    Function FindProgramDuring(ByVal MaM As Integer, ByVal DuringDate As Date) As DataTable

    '        On Error GoTo FindProgramDuring_Error

    '        Dim SQLString As String = "select format(events.date,""yyyy-mm-dd"") as [Date],events.time as [Time],events.channel as [Chan],events.name as [Programme] from events where (date=" & DuringDate.ToOADate & ") and ((StartMam<=" & MaM & " and StartMam+Duration>" & MaM & ") or (StartMam>=" & MaM & " and StartMam<Duration+" & MaM & "))"
    '        SQLString = "SELECT format(events.date,'yyyymmdd') as [Date],events.time as [Time],events.channel as [Chan],events.name as [Programme] from events where (date=" & DuringDate.ToOADate & ") and ((StartMam<=" & MaM & " and StartMam+Duration>" & MaM & ") or (StartMam>=" & MaM & " and StartMam<Duration+" & MaM & "))"
    '        Dim Command As New Odbc.OdbcCommand(SQLString, DBConn)
    '        Dim Adapter As New Odbc.OdbcDataAdapter
    '        Dim Rd As Odbc.OdbcDataReader
    '        Rd = Command.ExecuteReader
    '        Adapter.SelectCommand = Command

    '        Dim Table As New DataTable

    '        Table.Locale = System.Globalization.CultureInfo.InvariantCulture
    '        Adapter.FillLoadOption = LoadOption.OverwriteChanges
    '        Table.Load(Rd)
    '        FindProgramDuring = Table

    '        On Error GoTo 0
    '        Exit Function

    'FindProgramDuring_Error:

    '        'Err.Raise Err.Number, "frmSchedule: FindProgramDuring", Err.Description
    '        MsgBox("Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error")

    '    End Function


    'Shows those spots which do NOT have a property which corresponds to an entry in BookingFilter
    'BookingFilter returns TRUE if the spot is not being filtered, while this function returns TRUE is this
    'spot is supposed to be shown
    'If a spot is not to be shown, BookingFilter should return False for the spot and this function should return false
    Function FilterIn(ByVal EI As Trinity.cExtendedInfo) As Boolean
        Dim Ret As Boolean = True
        Dim WeekDays() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}
        Dim TmpBT As Trinity.cBookingType = cmbChannel.SelectedItem
        Dim TmpFilm As Trinity.cFilm = TmpBT.Weeks(1).Films(cmbFilm.SelectedItem)

        'Hannes

        Dim trim() As Char = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", " "}

        Dim s As String = EI.ProgAfter.ToString.ToUpper.TrimEnd(trim)

        If Not BookingFilter.Data("RealtimeProgramFilter", EI.ProgAfter.ToString) Then
            Ret = False
            GoTo ExitFilter
        End If

        If Not BookingFilter.Data("Program", s, True) Then
            Ret = False
            GoTo ExitFilter
        End If

        If Not BookingFilter.Data("MaM", EI.MaM) Then
            Ret = False
            GoTo ExitFilter
        End If

        If BookingFilter.Data("Availability", "OnlyShowBuyable") AndAlso EI.GrossPrice30(True) = 0 Then
            Ret = False
            GoTo ExitFilter
        End If
        If Not BookingFilter.Data("Daypart", TmpBT.Dayparts.GetDaypartForMam(EI.MaM).Name) Then
            Ret = False
            GoTo ExitFilter
        End If
        If TmpBT.GetWeek(EI.AirDate) Is Nothing OrElse Not BookingFilter.Data("Week", TmpBT.GetWeek(EI.AirDate).Name) Then
            Ret = False
            GoTo ExitFilter
        End If
        'If Not BookingFilter.Data("Availability", "ShowAffordableCampaign") Then
        '    Dim BudgetLeft As Decimal = grdPlannedTRP.Rows(grdPlannedTRP.RowCount - 1).Cells(3).Value - grdBookedTRP.Rows(grdBookedTRP.RowCount - 1).Cells(3).Value
        '    Ret = (EI.NetPrice(TmpFilm, TmpBT) <= BudgetLeft)
        'End If
        'If Not BookingFilter.Data("Availability", "ShowAffordableWeek") Then
        '    Stop
        'End If
        If Not BookingFilter.Data("Weekday", WeekDays(DatePart(DateInterval.Weekday, EI.AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) - 1)) Then
            Ret = False
            GoTo ExitFilter
        End If


ExitFilter:
        Return Ret
    End Function

    Private Sub updateFilterBudget()
        If bolFilterBudget Then
            dblFilterBudget(0) = grdPlannedTRP.Rows(grdPlannedTRP.RowCount - 1).Cells(3).Value - grdBookedTRP.Rows(grdBookedTRP.RowCount - 1).Cells(3).Value

            For week As Integer = 1 To grdPlannedTRP.Rows.Count - 1
                dblFilterBudget(week) = grdPlannedTRP.Rows(week - 1).Cells(3).Value - grdBookedTRP.Rows(week - 1).Cells(3).Value
            Next
        End If

    End Sub

    Private Function getWeekCount(ByVal d As Date) As Integer
        If dateEndWeek Is Nothing Then
            ReDim dateEndWeek(15)
            For i As Integer = 1 To Campaign.Channels(1).BookingTypes(1).Weeks.Count
                dateEndWeek(i - 1) = Date.FromOADate(Campaign.Channels(1).BookingTypes(1).Weeks(i).EndDate)
            Next
        End If

        For week As Integer = 0 To dateEndWeek.Length - 1
            If d <= dateEndWeek(week) Then Return (week + 1)
        Next

    End Function



    Private Sub deleteSpotsOnBudget()
        If bolFilterBudget Then

            Dim row As Integer
            grdSchedule.ClearSelection()

            If Not BookingFilter.Data("Availability", "ShowAffordableWeek") Then
                Dim TmpEI As Trinity.cExtendedInfo

                row = grdSchedule.RowCount - 1
                While row > -1
                    If row >= grdSchedule.RowCount Then row -= 1
                    TmpEI = Campaign.ExtendedInfos(grdSchedule.Rows(row).Tag)
                    If grdSchedule.Rows(row).Cells("colNet Price").Value > dblFilterBudget(getWeekCount(TmpEI.AirDate)) Then
                        grdSchedule.Rows(row).Visible = False
                    Else
                        grdSchedule.Rows(row).Visible = True
                    End If
                    row -= 1
                End While
            ElseIf Not BookingFilter.Data("Availability", "ShowAffordableCampaign") Then
                row = grdSchedule.RowCount - 1
                While row > -1
                    If row > grdSchedule.RowCount Then row -= 1
                    If grdSchedule.Rows(row).Cells("colNet Price").Value > dblFilterBudget(0) Then
                        grdSchedule.Rows(row).Visible = False
                    Else
                        grdSchedule.Rows(row).Visible = True
                    End If
                    row -= 1
                End While
            Else
                row = grdSchedule.RowCount - 1
                While row > -1
                    grdSchedule.Rows(row).Visible = True
                    row -= 1
                End While
            End If

        End If
    End Sub

    Private Sub frmBooking_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If Not comboboxRun Then
            If cmbChannel.SelectedIndex = -1 Then
                'cmbChannel.SelectedIndex = 0
            End If
            If cmbFilm.SelectedIndex = -1 Then
                cmbFilm.SelectedIndex = 0
            End If
            If cmbDatabase.SelectedIndex = -1 Then
                cmbDatabase.SelectedIndex = 0
            End If
            comboboxRun = True
        End If


        If cmbSolusFreq.SelectedIndex = -1 Then
            cmbSolusFreq.SelectedIndex = 0
        End If
        If Campaign.RFEstimation.ReferencePeriods.Count > 0 Then
            cmdRFEst.Image = imgOn.Image
            cmdSolus.Enabled = True
            cmbSolusFreq.Enabled = True
        Else
            cmdRFEst.Image = imgOff.Image
            cmdSolus.Enabled = False
            cmbSolusFreq.Enabled = False
        End If
        Me.Tag = ""
        cmdTweak.Enabled = (Campaign.MainTarget.TargetType = Trinity.cTarget.TargetTypeEnum.trgMnemonicTarget)

        UpdateSpotlist()
        UpdateSchedule(True, True)
        UpdatePlannedTRP()
        UpdateBookedTRP()
        UpdateLeftToBook()
        UpdateFilm()
        UpdateDaypart()
        UpdateRF()
        UpdateEstimatedTrend()
        UpdateInfoPanels("")
        UpdatePrimePeak()

    End Sub

    Private Sub QuicksortAscending(ByVal List As List(Of Trinity.cBookedSpot), ByVal Column As Windows.Forms.DataGridViewColumn, ByVal min As Integer, ByVal max As Integer)
        Dim med_value As Trinity.cBookedSpot
        Dim hi As Integer
        Dim lo As Integer
        Dim i As Integer

        ' If the list has no more than 1 element, it's sorted.
        If min >= max Then Exit Sub

        ' Pick a dividing item.
        i = Int((max - min + 1) * Rnd() + min)
        med_value = List(i)

        ' Swap it to the front so we can find it easily.
        List(i) = List(min)

        ' Move the items smaller than this into the left
        ' half of the list. Move the others into the right.
        lo = min
        hi = max
        Do
            ' Look down from hi for a value < med_value.
            'Do While List(hi).DateTimeSerial >= med_value.DateTimeSerial
            '    hi = hi - 1
            '    If hi <= lo Then Exit Do
            'Loop
            Select Case Column.HeaderText
                Case "DateTimeSerial"
                    Do While List(hi).DateTimeSerial >= med_value.DateTimeSerial
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Date"
                    Do While List(hi).AirDate >= med_value.AirDate
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "ID"
                    Do While List(hi).Chronological >= med_value.Chronological
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Week"
                    Do While DatePart(DateInterval.WeekOfYear, List(hi).AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) >= DatePart(DateInterval.WeekOfYear, med_value.AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Weekday"
                    Do While Weekday(List(hi).AirDate, FirstDayOfWeek.Monday) >= Weekday(med_value.AirDate, FirstDayOfWeek.Monday)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Time"
                    Do While List(hi).MaM >= med_value.MaM
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Channel"
                    Do While List(hi).Channel.ListNumber >= med_value.Channel.ListNumber
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Program"
                    Do While List(hi).Programme >= med_value.Programme
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Gross Price"
                    Do While List(hi).GrossPrice >= med_value.GrossPrice
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Chan Est"
                    Do While List(hi).ChannelEstimate >= med_value.ChannelEstimate
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Net Price"
                    Do While List(hi).NetPrice >= med_value.NetPrice
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Notes"
                    Do While List(hi).Comments >= med_value.Comments
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Gross CPP"
                    Do While List(hi).MyEstimate = 0 OrElse (List(hi).GrossPrice30 / List(hi).MyEstimate) >= (med_value.GrossPrice30 / med_value.MyEstimate)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "CPP (" & Campaign.MainTarget.TargetNameNice & ")"
                    Do While List(hi).MyEstimate = 0 OrElse (List(hi).NetPrice / List(hi).MyEstimate) >= (med_value.NetPrice / med_value.MyEstimate)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "CPP (Chn Est)"
                    Do While List(hi).MyEstimate = 0 OrElse (List(hi).NetPrice / List(hi).ChannelEstimate) >= (med_value.NetPrice / med_value.ChannelEstimate)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Est"
                    Do While List(hi).MyEstimate >= med_value.MyEstimate
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Est (" & List(hi).Bookingtype.BuyingTarget.TargetName & ")"
                    Do While List(hi).MyEstimateBuyTarget >= med_value.MyEstimateBuyTarget
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Filmcode"
                    Do While List(hi).Filmcode >= med_value.Filmcode
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Film"
                    Do While (Not med_value.Film Is Nothing AndAlso List(hi).Film Is Nothing AndAlso "<Unknown>" >= med_value.Film.Name) OrElse (Not List(hi).Film Is Nothing AndAlso Not med_value.Film Is Nothing AndAlso List(hi).Film.Name >= med_value.Film.Name) OrElse (Not List(hi).Film Is Nothing AndAlso med_value.Film Is Nothing AndAlso List(hi).Film.Name >= "<Unknown>")
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Film dscr"
                    Do While List(hi).Film.Description >= med_value.Film.Description
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
            End Select

            If hi <= lo Then
                List(lo) = med_value
                Exit Do
            End If

            ' Swap the lo and hi values.
            List(lo) = List(hi)

            ' Look up from lo for a value >= med_value.
            lo = lo + 1
            Select Case Column.HeaderText
                Case "DateTimeSerial"
                    Do While List(lo).DateTimeSerial < med_value.DateTimeSerial
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Date"
                    Do While List(lo).AirDate < med_value.AirDate
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "ID"
                    Do While List(lo).Chronological < med_value.Chronological
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Week"
                    Do While DatePart(DateInterval.WeekOfYear, List(lo).AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) < DatePart(DateInterval.WeekOfYear, med_value.AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Weekday"
                    Do While Weekday(List(lo).AirDate, FirstDayOfWeek.Monday) < Weekday(med_value.AirDate, FirstDayOfWeek.Monday)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Time"
                    Do While List(lo).MaM < med_value.MaM
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Channel"
                    Do While List(lo).Channel.ListNumber < med_value.Channel.ListNumber
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Program"
                    Do While List(lo).Programme < med_value.Programme
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Gross Price"
                    Do While List(lo).GrossPrice < med_value.GrossPrice
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Chan Est"
                    Do While List(lo).ChannelEstimate < med_value.ChannelEstimate
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Net Price"
                    Do While List(lo).NetPrice < med_value.NetPrice
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Notes"
                    Do While List(lo).Comments < med_value.Comments
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Gross CPP"
                    Do While List(lo).MyEstimate = 0 OrElse (List(lo).GrossPrice30 / List(lo).MyEstimate) < (med_value.GrossPrice30 / med_value.MyEstimate)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "CPP (" & Campaign.MainTarget.TargetNameNice & ")"
                    Do While List(lo).MyEstimate = 0 OrElse (List(lo).NetPrice / List(lo).MyEstimate) < (med_value.NetPrice / med_value.MyEstimate)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "CPP (Chn Est)"
                    Do While List(lo).MyEstimate = 0 OrElse (List(lo).NetPrice / List(lo).ChannelEstimate) < (med_value.NetPrice / med_value.ChannelEstimate)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Est"
                    Do While List(lo).MyEstimate < med_value.MyEstimate
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Est (" & List(lo).Bookingtype.BuyingTarget.TargetName & ")"
                    Do While List(lo).MyEstimateBuyTarget < med_value.MyEstimateBuyTarget
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Filmcode"
                    Do While List(lo).Filmcode < med_value.Filmcode
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Film"
                    Do While (List(lo).Film Is Nothing AndAlso Not med_value.Film Is Nothing AndAlso "<Unknown>" < med_value.Film.Name) OrElse (Not med_value.Film Is Nothing AndAlso Not List(lo).Film Is Nothing AndAlso List(lo).Film.Name < med_value.Film.Name) OrElse (med_value.Film Is Nothing AndAlso Not List(lo).Film Is Nothing AndAlso List(lo).Film.Name < "<Unknown>")
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Film dscr"
                    Do While List(lo).Film.Description < med_value.Film.Description
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
            End Select
            If lo >= hi Then
                lo = hi
                List(hi) = med_value
                Exit Do
            End If

            ' Swap the lo and hi values.
            List(hi) = List(lo)
        Loop

        ' Sort the two sublists
        QuicksortAscending(List, Column, min, lo - 1)
        QuicksortAscending(List, Column, lo + 1, max)

    End Sub

    Private Sub QuicksortAscending(ByVal List As List(Of Trinity.cExtendedInfo), ByVal Column As Windows.Forms.DataGridViewColumn, ByVal min As Integer, ByVal max As Integer, ByVal Film As Trinity.cFilm, ByVal Bookingtype As Trinity.cBookingType)
        Dim med_value As Trinity.cExtendedInfo

        Dim hi As Integer
        Dim lo As Integer
        Dim i As Integer

        ' If the list has no more than 1 element, it's sorted.
        If min >= max Then Exit Sub

        ' Pick a dividing item.
        i = Int((max - min + 1) * Rnd() + min)
        med_value = List(i)

        ' Swap it to the front so we can find it easily.
        List(i) = List(min)

        ' Move the items smaller than this into the left
        ' half of the list. Move the others into the right.
        lo = min
        hi = max
        Do
            ' Look down from hi for a value < med_value.
            'Do While List(hi).DateTimeSerial >= med_value.DateTimeSerial
            '    hi = hi - 1
            '    If hi <= lo Then Exit Do
            'Loop
            Select Case Column.Tag
                Case "Date"
                    Do While List(hi).AirDate >= med_value.AirDate
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "ID"
                    Do While List(hi).ID >= med_value.ID
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Week"
                    Do While DatePart(DateInterval.WeekOfYear, List(hi).AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) >= DatePart(DateInterval.WeekOfYear, med_value.AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Weekday"
                    Do While Weekday(List(hi).AirDate, FirstDayOfWeek.Monday) >= Weekday(med_value.AirDate, FirstDayOfWeek.Monday)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Time"
                    Do While List(hi).MaM >= med_value.MaM
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Channel"
                    Do While Campaign.Channels(List(hi).Channel).ListNumber >= Campaign.Channels(med_value.Channel).ListNumber
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Program"
                    Do While List(hi).ProgAfter >= med_value.ProgAfter
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Gross Price"
                    Do While List(hi).GrossPrice(Film) >= med_value.GrossPrice(Film)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Chan Est"
                    Do While List(hi).ChannelEstimate >= med_value.ChannelEstimate
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Net Price"
                    Do While List(hi).NetPrice(Film, Bookingtype) >= med_value.NetPrice(Film, Bookingtype)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Gross CPP"
                    Do While List(hi).GrossCPP30 >= med_value.GrossCPP30
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "CPP Main"
                    Do While List(hi).NetCPP(Film, Bookingtype) >= med_value.NetCPP(Film, Bookingtype)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                    ' These two are the same. The old stays for compability reason
                Case "CPP (Chn Est)"
                    Do While List(hi).NetCPPChannel(Film, Bookingtype) >= med_value.NetCPPChannel(Film, Bookingtype)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Gross CPP (Chn Est)"
                    Do While List(hi).GrossCPPChannel >= med_value.GrossCPPChannel
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop

                Case "Net CPP (Chn Est)"
                    Do While List(hi).NetCPPChannel(Film, Bookingtype) >= med_value.NetCPPChannel(Film, Bookingtype)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop

                Case "Est"
                    Do While List(hi).EstimatedRating >= med_value.EstimatedRating
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Idx Chan Est"
                    Do While List(hi).IndexVsChannelEstimate >= med_value.IndexVsChannelEstimate
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Idx Est Buy"
                    Do While List(hi).IndexVsBuyingEstimate >= med_value.IndexVsBuyingEstimate
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Solus"
                    Do While List(hi).Solus >= med_value.Solus
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Est Buy"
                    Do While List(hi).EstimatedRatingBuyingTarget >= med_value.EstimatedRatingBuyingTarget
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Remarks"
                    Do While List(hi).Remark >= med_value.Remark
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Solus 1st"
                    Do While List(hi).SolusFirst >= med_value.SolusFirst
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "CPS"
                    Do While List(hi).CostPerSolus(Film, Bookingtype) >= med_value.CostPerSolus(Film, Bookingtype)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                    '---------------------------------------------------------------------------'
                    ' Tillagt av Hannes, ta ev.bort detta case                                     '
                    '---------------------------------------------------------------------------'
                Case "CPT Main"
                    Do While List(hi).NetCPT(Film, Bookingtype) >= med_value.NetCPT(Film, Bookingtype)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop

                    'Case "Est (" & List(hi).Bookingtype.BuyingTarget.TargetName & ")"
                    '    Do While List(hi).MyEstimateBuyTarget >= med_value.MyEstimateBuyTarget
                    '        hi = hi - 1
                    '        If hi <= lo Then Exit Do
                    '    Loop
            End Select

            If hi <= lo Then
                List(lo) = med_value
                Exit Do
            End If

            ' Swap the lo and hi values.
            List(lo) = List(hi)

            ' Look up from lo for a value >= med_value.
            lo = lo + 1
            Select Case Column.HeaderText
                Case "Date"
                    Do While List(lo).AirDate < med_value.AirDate
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "ID"
                    Do While List(lo).ID < med_value.ID
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Week"
                    Do While DatePart(DateInterval.WeekOfYear, List(lo).AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) < DatePart(DateInterval.WeekOfYear, med_value.AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Weekday"
                    Do While Weekday(List(lo).AirDate, FirstDayOfWeek.Monday) < Weekday(med_value.AirDate, FirstDayOfWeek.Monday)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Time"
                    Do While List(lo).MaM < med_value.MaM
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Channel"
                    Do While Campaign.Channels(List(lo).Channel).ListNumber < Campaign.Channels(med_value.Channel).ListNumber
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Program"
                    Do While List(lo).ProgAfter < med_value.ProgAfter
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Gross Price"
                    Do While List(lo).GrossPrice(Film) < med_value.GrossPrice(Film)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Chan Est"
                    Do While List(lo).ChannelEstimate < med_value.ChannelEstimate
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Net Price"
                    Do While List(lo).NetPrice(Film, Bookingtype) < med_value.NetPrice(Film, Bookingtype)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Gross CPP"
                    Do While List(lo).GrossCPP30 < med_value.GrossCPP30
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "CPP Main"
                    Do While List(lo).NetCPP(Film, Bookingtype) < med_value.NetCPP(Film, Bookingtype)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop

                    ' These two does the same
                Case "CPP (Chn Est)"
                    Do While List(lo).NetCPPChannel(Film, Bookingtype) < med_value.NetCPPChannel(Film, Bookingtype)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Gross CPP (Chn Est)"
                    Do While List(lo).GrossCPPChannel < med_value.GrossCPPChannel
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Net CPP (Chn Est)"
                    Do While List(lo).NetCPPChannel(Film, Bookingtype) < med_value.NetCPPChannel(Film, Bookingtype)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop

                Case "Est"
                    Do While List(lo).EstimatedRating < med_value.EstimatedRating
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Idx Chan Est"
                    Do While List(lo).IndexVsChannelEstimate < med_value.IndexVsChannelEstimate
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Idx Est Buy"
                    Do While List(lo).IndexVsBuyingEstimate < med_value.IndexVsBuyingEstimate
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Solus"
                    Do While List(lo).Solus < med_value.Solus
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Est Buy"
                    Do While List(lo).EstimatedRatingBuyingTarget < med_value.EstimatedRatingBuyingTarget
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Remarks"
                    Do While List(lo).Remark < med_value.Remark
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Solus 1st"
                    Do While List(lo).SolusFirst < med_value.SolusFirst
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "CPS"
                    Do While List(lo).CostPerSolus(Film, Bookingtype) < med_value.CostPerSolus(Film, Bookingtype)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                    '---------------------------------------------------------------------------'
                    ' Tillagt av Hannes, ta ev. bort detta case                                     '
                    '---------------------------------------------------------------------------'
                Case "CPT Main"
                    Do While List(lo).NetCPT(Film, Bookingtype) < med_value.NetCPT(Film, Bookingtype)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                    'Case "Est (" & List(lo).Bookingtype.BuyingTarget.TargetName & ")"
                    '    Do While List(lo).MyEstimateBuyTarget < med_value.MyEstimateBuyTarget
                    '        lo = lo + 1
                    '        If lo >= hi Then Exit Do
                    '    Loop

            End Select
            If lo >= hi Then
                lo = hi
                List(hi) = med_value
                Exit Do
            End If

            ' Swap the lo and hi values.
            List(hi) = List(lo)
        Loop

        ' Sort the two sublists
        QuicksortAscending(List, Column, min, lo - 1, Film, Bookingtype)
        QuicksortAscending(List, Column, lo + 1, max, Film, Bookingtype)

    End Sub

    Private Sub QuicksortDescending(ByVal List As List(Of Trinity.cExtendedInfo), ByVal Column As Windows.Forms.DataGridViewColumn, ByVal min As Integer, ByVal max As Integer, ByVal Film As Trinity.cFilm, ByVal Bookingtype As Trinity.cBookingType)
        Dim med_value As Trinity.cExtendedInfo

        Dim hi As Integer
        Dim lo As Integer
        Dim i As Integer

        ' If the list has no more than 1 element, it's sorted.
        If min >= max Then Exit Sub

        ' Pick a dividing item.
        i = Int((max - min + 1) * Rnd() + min)
        med_value = List(i)

        ' Swap it to the front so we can find it easily.
        List(i) = List(min)

        ' Move the items smaller than this into the left
        ' half of the list. Move the others into the right.
        lo = min
        hi = max
        Do
            ' Look down from hi for a value < med_value.
            'Do While List(hi).DateTimeSerial >= med_value.DateTimeSerial
            '    hi = hi - 1
            '    If hi <= lo Then Exit Do
            'Loop
            Select Case Column.Tag
                Case "Date"
                    Do While List(hi).AirDate < med_value.AirDate
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "ID"
                    Do While List(hi).ID < med_value.ID
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Week"
                    Do While DatePart(DateInterval.WeekOfYear, List(hi).AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) < DatePart(DateInterval.WeekOfYear, med_value.AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Weekday"
                    Do While Weekday(List(hi).AirDate, FirstDayOfWeek.Monday) < Weekday(med_value.AirDate, FirstDayOfWeek.Monday)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Time"
                    Do While List(hi).MaM < med_value.MaM
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Channel"
                    Do While Campaign.Channels(List(hi).Channel).ListNumber < Campaign.Channels(med_value.Channel).ListNumber
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Program"
                    Do While List(hi).ProgAfter < med_value.ProgAfter
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Gross Price"
                    Do While List(hi).GrossPrice(Film) < med_value.GrossPrice(Film)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Chan Est"
                    Do While List(hi).ChannelEstimate < med_value.ChannelEstimate
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Net Price"
                    Do While List(hi).NetPrice(Film, Bookingtype) < med_value.NetPrice(Film, Bookingtype)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Gross CPP"
                    Do While List(hi).GrossCPP30 < med_value.GrossCPP30
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "CPP Main"
                    Do While List(hi).NetCPP(Film, Bookingtype) < med_value.NetCPP(Film, Bookingtype)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "CPP (Chn Est)"
                    Do While List(hi).NetCPPChannel(Film, Bookingtype) < med_value.NetCPPChannel(Film, Bookingtype)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Gross CPP (Chn Est)"
                    Do While List(hi).GrossCPPChannel < med_value.GrossCPPChannel
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Net CPP (Chn Est)"
                    Do While List(hi).NetCPPChannel(Film, Bookingtype) < med_value.NetCPPChannel(Film, Bookingtype)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop

                Case "Est"
                    Do While List(hi).EstimatedRating < med_value.EstimatedRating
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Idx Chan Est"
                    Do While List(hi).IndexVsChannelEstimate < med_value.IndexVsChannelEstimate
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Idx Est Buy"
                    Do While List(hi).IndexVsBuyingEstimate < med_value.IndexVsBuyingEstimate
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Solus"
                    Do While List(hi).Solus < med_value.Solus
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Est Buy"
                    Do While List(hi).EstimatedRatingBuyingTarget < med_value.EstimatedRatingBuyingTarget
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Remarks"
                    Do While List(hi).Remark < med_value.Remark
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Solus 1st"
                    Do While List(hi).SolusFirst < med_value.SolusFirst
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "CPS"
                    Do While List(hi).CostPerSolus(Film, Bookingtype) < med_value.CostPerSolus(Film, Bookingtype)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                    '---------------------------------------------------------------------------'
                    ' Tillagt av Hannes, ta ev. bort detta case                                     '
                    '---------------------------------------------------------------------------'
                Case "CPT Main"
                    Do While List(hi).NetCPT(Film, Bookingtype) < med_value.NetCPT(Film, Bookingtype)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                    'Case "Est (" & List(hi).Bookingtype.BuyingTarget.TargetName & ")"
                    '    Do While List(hi).MyEstimateBuyTarget < med_value.MyEstimateBuyTarget
                    '        hi = hi - 1
                    '        If hi <= lo Then Exit Do
                    '    Loop
            End Select

            If hi <= lo Then
                List(lo) = med_value
                Exit Do
            End If

            ' Swap the lo and hi values.
            List(lo) = List(hi)

            ' Look up from lo for a value >= med_value.
            lo = lo + 1
            Select Case Column.HeaderText
                Case "Date"
                    Do While List(lo).AirDate >= med_value.AirDate
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "ID"
                    Do While List(lo).ID >= med_value.ID
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Week"
                    Do While DatePart(DateInterval.WeekOfYear, List(lo).AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) >= DatePart(DateInterval.WeekOfYear, med_value.AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Weekday"
                    Do While Weekday(List(lo).AirDate, FirstDayOfWeek.Monday) >= Weekday(med_value.AirDate, FirstDayOfWeek.Monday)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Time"
                    Do While List(lo).MaM >= med_value.MaM
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Channel"
                    Do While Campaign.Channels(List(lo).Channel).ListNumber >= Campaign.Channels(med_value.Channel).ListNumber
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Program"
                    Do While List(lo).ProgAfter >= med_value.ProgAfter
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Gross Price"
                    Do While List(lo).GrossPrice(Film) >= med_value.GrossPrice(Film)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Chan Est"
                    Do While List(lo).ChannelEstimate >= med_value.ChannelEstimate
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Net Price"
                    Do While List(lo).NetPrice(Film, Bookingtype) >= med_value.NetPrice(Film, Bookingtype)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Gross CPP"
                    Do While List(lo).GrossCPP30 >= med_value.GrossCPP30
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "CPP Main"
                    Do While List(lo).NetCPP(Film, Bookingtype) >= med_value.NetCPP(Film, Bookingtype)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "CPP (Chn Est)"
                    Do While List(lo).NetCPPChannel(Film, Bookingtype) >= med_value.NetCPPChannel(Film, Bookingtype)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Gross CPP (Chn Est)"
                    Do While List(lo).GrossCPPChannel >= med_value.GrossCPPChannel
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Net CPP (Chn Est)"
                    Do While List(lo).NetCPPChannel(Film, Bookingtype) >= med_value.NetCPPChannel(Film, Bookingtype)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop

                Case "Est"
                    Do While List(lo).EstimatedRating >= med_value.EstimatedRating
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Idx Chan Est"
                    Do While List(lo).IndexVsChannelEstimate >= med_value.IndexVsChannelEstimate
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Idx Est Buy"
                    Do While List(lo).IndexVsBuyingEstimate >= med_value.IndexVsBuyingEstimate
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Solus"
                    Do While List(lo).Solus >= med_value.Solus
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Est Buy"
                    Do While List(lo).EstimatedRatingBuyingTarget >= med_value.EstimatedRatingBuyingTarget
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Remarks"
                    Do While List(lo).Remark >= med_value.Remark
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Solus 1st"
                    Do While List(lo).SolusFirst >= med_value.SolusFirst
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "CPS"
                    Do While List(lo).CostPerSolus(Film, Bookingtype) >= med_value.CostPerSolus(Film, Bookingtype)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                    '---------------------------------------------------------------------------'
                    ' Tillagt av Hannes, ta ev. bort detta case                                     '
                    '---------------------------------------------------------------------------'
                Case "CPT Main"
                    Do While List(lo).NetCPT(Film, Bookingtype) >= med_value.NetCPT(Film, Bookingtype)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop

                    'Case "Est (" & List(lo).Bookingtype.BuyingTarget.TargetName & ")"
                    '    Do While List(lo).MyEstimateBuyTarget >= med_value.MyEstimateBuyTarget
                    '        lo = lo + 1
                    '        If lo >= hi Then Exit Do
                    '    Loop

            End Select
            If lo >= hi Then
                lo = hi
                List(hi) = med_value
                Exit Do
            End If

            ' Swap the lo and hi values.
            List(hi) = List(lo)
        Loop

        ' Sort the two sublists
        QuicksortDescending(List, Column, min, lo - 1, Film, Bookingtype)
        QuicksortDescending(List, Column, lo + 1, max, Film, Bookingtype)

    End Sub

    Private Sub QuicksortDescending(ByVal List As List(Of Trinity.cBookedSpot), ByVal Column As Windows.Forms.DataGridViewColumn, ByVal min As Integer, ByVal max As Integer)
        Dim med_value As Trinity.cBookedSpot
        Dim hi As Integer
        Dim lo As Integer
        Dim i As Integer

        ' If the list has no more than 1 element, it's sorted.
        If min >= max Then Exit Sub

        ' Pick a dividing item.
        i = Int((max - min + 1) * Rnd() + min)
        med_value = List(i)

        ' Swap it to the front so we can find it easily.
        List(i) = List(min)

        ' Move the items smaller than this into the left
        ' half of the list. Move the others into the right.
        lo = min
        hi = max
        Do
            ' Look down from hi for a value < med_value.
            'Do While List(hi).DateTimeSerial >= med_value.DateTimeSerial
            '    hi = hi - 1
            '    If hi <= lo Then Exit Do
            'Loop
            Select Case Column.HeaderText
                Case "DateTimeSerial"
                    Do While List(hi).DateTimeSerial < med_value.DateTimeSerial
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Date"
                    Do While List(hi).AirDate < med_value.AirDate
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "ID"
                    Do While List(hi).Chronological < med_value.Chronological
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Week"
                    Do While DatePart(DateInterval.WeekOfYear, List(hi).AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) < DatePart(DateInterval.WeekOfYear, med_value.AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Weekday"
                    Do While Weekday(List(hi).AirDate, FirstDayOfWeek.Monday) < Weekday(med_value.AirDate, FirstDayOfWeek.Monday)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Time"
                    Do While List(hi).MaM < med_value.MaM
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Channel"
                    Do While List(hi).Channel.ListNumber < med_value.Channel.ListNumber
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Program"
                    Do While List(hi).Programme < med_value.Programme
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Gross Price"
                    Do While List(hi).GrossPrice < med_value.GrossPrice
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Chan Est"
                    Do While List(hi).ChannelEstimate < med_value.ChannelEstimate
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Net Price"
                    Do While List(hi).NetPrice < med_value.NetPrice
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Notes"
                    Do While List(hi).Comments < med_value.Comments
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Gross CPP"
                    Do While List(hi).MyEstimate = 0 OrElse (List(hi).GrossPrice30 / List(hi).MyEstimate) < (med_value.GrossPrice30 / med_value.MyEstimate)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "CPP (" & Campaign.MainTarget.TargetNameNice & ")"
                    Do While List(hi).MyEstimate = 0 OrElse (List(hi).NetPrice / List(hi).MyEstimate) < (med_value.NetPrice / med_value.MyEstimate)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "CPP (Chn Est)"
                    Do While List(hi).MyEstimate = 0 OrElse (List(hi).NetPrice / List(hi).ChannelEstimate) < (med_value.NetPrice / med_value.ChannelEstimate)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Gross CPP (Chn Est)"
                    Do While List(hi).MyEstimate = 0 OrElse (List(hi).GrossPrice / List(hi).ChannelEstimate) < (med_value.GrossPrice / med_value.ChannelEstimate)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop

                Case "Net CPP (Chn Est)"
                    Do While List(hi).MyEstimate = 0 OrElse (List(hi).NetPrice / List(hi).ChannelEstimate) < (med_value.NetPrice / med_value.ChannelEstimate)
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop

                Case "Est"
                    Do While List(hi).MyEstimate < med_value.MyEstimate
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Est (" & List(hi).Bookingtype.BuyingTarget.TargetName & ")"
                    Do While List(hi).MyEstimateBuyTarget < med_value.MyEstimateBuyTarget
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Filmcode"
                    Do While List(hi).Filmcode < med_value.Filmcode
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Film"
                    Do While (List(hi).Film Is Nothing AndAlso "<Unknown>" < med_value.Film.Name) OrElse (Not med_value.Film Is Nothing AndAlso List(hi).Film.Name < med_value.Film.Name) OrElse (med_value.Film Is Nothing AndAlso List(hi).Film.Name < "<Unknown>")
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
                Case "Film dscr"
                    Do While List(hi).Film.Description < med_value.Film.Description
                        hi = hi - 1
                        If hi <= lo Then Exit Do
                    Loop
            End Select

            If hi <= lo Then
                List(lo) = med_value
                Exit Do
            End If

            ' Swap the lo and hi values.
            List(lo) = List(hi)

            ' Look up from lo for a value >= med_value.
            lo = lo + 1
            Select Case Column.HeaderText
                Case "DateTimeSerial"
                    Do While List(lo).DateTimeSerial >= med_value.DateTimeSerial
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Date"
                    Do While List(lo).AirDate >= med_value.AirDate
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "ID"
                    Do While List(lo).Chronological >= med_value.Chronological
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Week"
                    Do While DatePart(DateInterval.WeekOfYear, List(lo).AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) >= DatePart(DateInterval.WeekOfYear, med_value.AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Weekday"
                    Do While Weekday(List(lo).AirDate, FirstDayOfWeek.Monday) >= Weekday(med_value.AirDate, FirstDayOfWeek.Monday)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Time"
                    Do While List(lo).MaM >= med_value.MaM
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Channel"
                    Do While List(lo).Channel.ListNumber >= med_value.Channel.ListNumber
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Program"
                    Do While List(lo).Programme >= med_value.Programme
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Gross Price"
                    Do While List(lo).GrossPrice >= med_value.GrossPrice
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Chan Est"
                    Do While List(lo).ChannelEstimate >= med_value.ChannelEstimate
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Net Price"
                    Do While List(lo).NetPrice >= med_value.NetPrice
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Notes"
                    Do While List(lo).Comments >= med_value.Comments
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Gross CPP"
                    Do While List(lo).MyEstimate = 0 OrElse (List(lo).GrossPrice30 / List(lo).MyEstimate) >= (med_value.GrossPrice30 / med_value.MyEstimate)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "CPP (" & Campaign.MainTarget.TargetNameNice & ")"
                    Do While List(lo).MyEstimate = 0 OrElse (List(lo).NetPrice / List(lo).MyEstimate) >= (med_value.NetPrice / med_value.MyEstimate)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "CPP (Chn Est)"
                    Do While List(lo).MyEstimate = 0 OrElse (List(lo).NetPrice / List(lo).ChannelEstimate) >= (med_value.NetPrice / med_value.ChannelEstimate)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Gross CPP (Chn Est)"
                    Do While List(lo).MyEstimate = 0 OrElse (List(lo).GrossPrice / List(lo).ChannelEstimate) >= (med_value.GrossPrice / med_value.ChannelEstimate)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Net CPP (Chn Est)"
                    Do While List(lo).MyEstimate = 0 OrElse (List(lo).NetPrice / List(lo).ChannelEstimate) >= (med_value.NetPrice / med_value.ChannelEstimate)
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop

                Case "Est"
                    Do While List(lo).MyEstimate >= med_value.MyEstimate
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Est (" & List(lo).Bookingtype.BuyingTarget.TargetName & ")"
                    Do While List(lo).MyEstimateBuyTarget >= med_value.MyEstimateBuyTarget
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Filmcode"
                    Do While List(lo).Filmcode >= med_value.Filmcode
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Film"
                    Do While (List(lo).Film Is Nothing AndAlso "<Unknown>" >= med_value.Film.Name) OrElse (Not med_value.Film Is Nothing AndAlso List(lo).Film.Name >= med_value.Film.Name) OrElse (med_value.Film Is Nothing AndAlso List(lo).Film.Name >= "<Unknown>")
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
                Case "Film dscr"
                    Do While List(lo).Film.Description >= med_value.Film.Description
                        lo = lo + 1
                        If lo >= hi Then Exit Do
                    Loop
            End Select
            If lo >= hi Then
                lo = hi
                List(hi) = med_value
                Exit Do
            End If

            ' Swap the lo and hi values.
            List(hi) = List(lo)
        Loop

        ' Sort the two sublists
        QuicksortDescending(List, Column, min, lo - 1)
        QuicksortDescending(List, Column, lo + 1, max)

    End Sub

    Private Sub frmBooking_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        eventsTable = Nothing
    End Sub

    Private Sub frmBooking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Sizes the tabs of tabControl1.
        Me.TabSchedule.ItemSize = New Size(120, 19)

        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpFilm As Trinity.cFilm

        '        SchedulePercent = 0.5
        pnlDetails = sptBooking.Panel2
        pnlSchedule = sptSpotlist.Panel1
        pnlSpotlist = sptSpotlist.Panel2

        'grdSchedule.Top = stpSchedule.Bottom

        For Each TmpChan In Campaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                If TmpBT.BookIt And (TmpBT.IsSpecific Or TmpBT.IsPremium) Then
                    cmbChannel.Items.Add(TmpBT)
                End If
            Next
        Next
        cmbFilm.Items.Clear()
        grdFilms.Rows.Clear()
        For Each TmpFilm In Campaign.Channels(1).BookingTypes(1).Weeks(1).Films
            cmbFilm.Items.Add(TmpFilm.Name)
            grdFilms.Rows.Add()
            grdFilms.Rows(grdFilms.Rows.Count - 1).Tag = TmpFilm
        Next
        grdFilms.Height = grdFilms.Rows.GetRowsHeight(Windows.Forms.DataGridViewElementStates.None) + grdFilms.ColumnHeadersHeight + 3
        sptBooking.SplitterDistance = Me.Width - TrinitySettings.SummaryWidth

    End Sub

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub cmdScheduleColumns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdScheduleColumns.Click
        If cmbChannel.SelectedItem IsNot Nothing Then
            Dim Chan As String = cmbChannel.Text.Substring(0, InStr(cmbChannel.Text, " ") - 1)
            Dim BT As String = Mid(cmbChannel.Text, InStr(cmbChannel.Text, " ") + 1)
            Dim Columns() As String = {"Date", "Time", "Channel", "Program", "Week", "Weekday", "Gross Price", "Net Price", "Est", "Est Buy", "Chan Est", "Est Second", "CPT Main", "Gross CPP", "CPP Main", "Gross CPP (Chn Est)", "Net CPP (Chn Est)", "CPP Second", "Quality", "Remarks", "Solus", "Solus 1st", "CPS", "Idx Chan Est", "Idx Est Buy", "Idx Scnd/Chan", "Addition", "Est Rch TRP"}
            Dim i As Integer
            Dim j As Integer
            Dim FoundIt As Boolean
            Dim TmpNode As System.Windows.Forms.TreeNode
            Dim TmpCol As Windows.Forms.DataGridViewColumn

            frmColumns.tvwChosen.Nodes.Clear()
            TmpCol = grdSchedule.Columns.GetFirstColumn(Windows.Forms.DataGridViewElementStates.None)

            While Not TmpCol Is Nothing
                If TmpCol.Visible Then
                    frmColumns.tvwChosen.Nodes.Add(CreateGUID, TmpCol.Tag)
                End If
                TmpCol = grdSchedule.Columns.GetNextColumn(TmpCol, Windows.Forms.DataGridViewElementStates.None, Windows.Forms.DataGridViewElementStates.None)
            End While
            frmColumns.tvwAvailable.Nodes.Clear()
            For j = 0 To 26
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
                TrinitySettings.BookingColumnCount = frmColumns.tvwChosen.Nodes.Count
                TmpNode = frmColumns.tvwChosen.Nodes(1)
                While Not TmpNode.PrevNode Is Nothing
                    TmpNode = TmpNode.PrevNode
                End While
                i = 1
                While Not TmpNode Is Nothing
                    TrinitySettings.BookingColumn(i) = TmpNode.Text
                    i = i + 1
                    TmpNode = TmpNode.NextNode
                End While
                'ScheduleRd.Close()
                SortScheduleColumn = Nothing
                UpdateSchedule(True, False)
            End If
        Else
            MessageBox.Show("You must select a channel", "Trinity")
        End If
    End Sub

    Private Sub grdSchedule_BookMany() 'ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Saved = False
        Dim SpotID As String
        Dim ID As String
        Dim TmpEI As Trinity.cExtendedInfo
        Dim Chan As String
        Dim BT As String
        Dim TmpCol As System.Windows.Forms.DataGridViewColumn
        Dim TmpRow As System.Windows.Forms.DataGridViewRow
        Dim TmpWeek As Trinity.cWeek
        Dim TmpSpot As Trinity.cBookedSpot
        Dim Bid As Single
        Dim Gross As Decimal

        saveCounter += 1

        Dim a As Integer
        Dim en As Long
        Dim ed As String
        On Error GoTo grdSchedule_DblClick_Error

        Trinity.Helper.WriteToLogFile("Start of frmBooking/grdSchedule_DblClick")

        If Not Campaign.RootCampaign Is Nothing Then Exit Sub

        Chan = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        BT = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Name
        Dim TmpFilm As Trinity.cFilm = Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(cmbFilm.Text)

        For Each row As DataGridViewRow In grdSchedule.SelectedRows
            ID = row.Tag
            TmpEI = Campaign.ExtendedInfos(ID)
            SpotID = CreateGUID()
            If Campaign.Channels(Chan).UseBid Then
                Bid = InputBox("Set your bid:", "T R I N I T Y", 0)
                Gross = TmpEI.GrossPrice(TmpFilm, Bid)
            Else
                Gross = TmpEI.GrossPrice(TmpFilm)
            End If

            'warn if you are booking a spot that has no ratings for this week
            If Campaign.Channels(Chan).BookingTypes(BT).Weeks(Campaign.Channels(Chan).BookingTypes(BT).GetWeek(TmpEI.AirDate).Name).Films(TmpFilm.Name).Share = 0 Then
                If MsgBox("This film are not scheduled for this week, do you still want to book the spot?", MsgBoxStyle.YesNo, "Continue?") = MsgBoxResult.No Then Exit Sub
            End If
            Campaign.BookedSpots.Add(SpotID, ID, TmpEI.Channel, TmpEI.AirDate, TmpEI.MaM, TmpEI.ProgAfter, TmpEI.ProgAfter, "", Gross, TmpEI.NetPrice(TmpFilm, Campaign.Channels(Chan).BookingTypes(BT), Bid), TmpEI.ChannelEstimate, TmpEI.EstimatedRating, TmpEI.EstimatedRatingBuyingTarget, cmbFilm.Text, BT, InStr(TmpEI.Remark, "L") > 0, InStr(TmpEI.Remark, "R") > 0, Bid)

            'AddToPivot(Campaign.BookedSpots(SpotID))
            For Each TmpWeek In Campaign.Channels(TmpEI.Channel).BookingTypes(BT).Weeks
                If Campaign.Channels(TmpEI.Channel).BookingTypes(BT).Weeks(TmpWeek.Name).StartDate <= TmpEI.AirDate.ToOADate Then
                    If Campaign.Channels(TmpEI.Channel).BookingTypes(BT).Weeks(TmpWeek.Name).EndDate >= TmpEI.AirDate.ToOADate Then
                        Campaign.BookedSpots(SpotID).week = Campaign.Channels(TmpEI.Channel).BookingTypes(BT).Weeks(TmpWeek.Name)
                    End If
                End If
            Next
            Campaign.BookedSpots(SpotID).Filmcode = Campaign.BookedSpots(SpotID).week.Films(cmbFilm.Text).Filmcode
            Campaign.BookedSpots(SpotID).RecentlyBooked = True
            For Each TmpSpot In Campaign.BookedSpots
                If TmpSpot.MostRecentlyBooked AndAlso TmpSpot.ID <> SpotID Then
                    TmpSpot.MostRecentlyBooked = False
                End If
            Next
            Campaign.BookedSpots(SpotID).MostRecentlyBooked = True

            Dim TmpSex As String
            If Campaign.MainTarget.TargetType = Trinity.cTarget.TargetTypeEnum.trgMnemonicTarget Then
                Select Case Campaign.MainTarget.TargetNameNice.ToUpper.ToString.Substring(0, 1)
                    Case "A" : TmpSex = ""
                    Case "M" : TmpSex = "M"
                    Case "W" : TmpSex = "W"
                    Case Else : TmpSex = ""
                End Select
            Else
                TmpSex = ""
            End If
            Dim AgeTRP(0 To 13) As Single
            Dim TmpArray() As String = {TmpSex & "3-11", TmpSex & "12-14", TmpSex & "15-19", TmpSex & "20-24", TmpSex & "25-29", TmpSex & "30-34", TmpSex & "35-39", TmpSex & "40-44", TmpSex & "45-49", TmpSex & "50-54", TmpSex & "55-59", TmpSex & "60-64", TmpSex & "60-69", TmpSex & "70+"}
            Dim AvgRating As Single
            For i As Integer = 0 To 13
                AgeTRP(i) = 0
                If Not TmpEI Is Nothing AndAlso Not TmpEI.BreakList Is Nothing Then
                    For b As Integer = 1 To TmpEI.BreakList.Count
                        Dim TmpStr As String = TmpArray(i)
                        AgeTRP(i) = AgeTRP(i) + DirectCast(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod), Trinity.cPeriod).Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod).Adedge, TmpStr))
                        AvgRating = AvgRating + DirectCast(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod), Trinity.cPeriod).Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod).Adedge, TmpStr)) / 14
                    Next
                End If
            Next
            For i As Integer = 0 To 13
                Campaign.BookedSpots(SpotID).AgeIndex(i) = AgeTRP(i) / AvgRating
            Next
            Campaign.RFEstimation.Calculate()
            For Each TmpCol In grdSchedule.Columns
                grdSchedule.SelectedRows.Item(0).Cells(TmpCol.Name).Style.ForeColor = Drawing.Color.Red
            Next
            If grdSchedule.Columns.Contains("colSolus") OrElse grdSchedule.Columns.Contains("colCPS") Then
                For Each TmpRow In grdSchedule.Rows
                    ID = TmpRow.Tag
                    If ID <> "" Then
                        Campaign.ExtendedInfos(ID).Solus = 0
                        If grdSchedule.Columns.Contains("colSolus") Then
                            TmpRow.Cells("colSolus").Value = Format(0, "0.0")
                        End If
                        If grdSchedule.Columns.Contains("colCPS") Then
                            TmpRow.Cells("colCPS").Value = Format(0, "0 kr")
                        End If
                    End If
                Next
            End If
        Next
        UpdateSpotlist()
        UpdateBookedTRP()
        UpdateLeftToBook()
        UpdateFilm()
        UpdateDaypart()
        UpdateRF()
        UpdateEstimatedTrend()
        UpdatePrimePeak()

        'update budget filtering if available
        updateFilterBudget()
        deleteSpotsOnBudget()

        'make sure the changes are done in the graphic part aswell
        GfxSchedule2.updateGraphics(TmpEI.AirDate, TmpEI.MaM, TmpEI.ProgAfter)

        'save promt
        If saveCounter = 10 Then
            saveCounter = 0
            If MsgBox("It has been a while since your last save. Do you wish to save now?", MsgBoxStyle.YesNo, "Save campaign") = MsgBoxResult.Yes Then
                If TrinitySettings.SaveCampaignsAsFiles Then
                    'if the file name is empty (not saved before) it opens a save file dialog and then saves the campaign
                    If Campaign.Filename = "" Then
                        Dim dlgDialog As New Windows.Forms.SaveFileDialog
                        dlgDialog.Title = "Save campaign as..."
                        dlgDialog.FileName = "*.cmp"
                        dlgDialog.DefaultExt = "*.cmp"
                        dlgDialog.Filter = "Trinity campaigns|*.cmp"
                        dlgDialog.InitialDirectory = TrinitySettings.CampaignFiles
                        If dlgDialog.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
                        Me.Cursor = Windows.Forms.Cursors.WaitCursor
                        Campaign.SaveCampaign(dlgDialog.FileName)
                        Me.Cursor = Windows.Forms.Cursors.Default
                    Else
                        'if the filename is set the campagn is saved
                        Me.Cursor = Windows.Forms.Cursors.WaitCursor
                        Campaign.SaveCampaign(Campaign.Filename)
                        Me.Cursor = Windows.Forms.Cursors.Default
                    End If
                Else
                    frmMain.SaveToDB()
                End If

            End If
        End If
        On Error GoTo 0
        Exit Sub

grdSchedule_DblClick_Error:

        en = Err.Number
        ed = Err.Description
        If IsIDE() Then
            a = MsgBox("Error:" & Chr(10) & Chr(10) & "Runtime Error '" & en & "':" & Chr(10) & Chr(10) & ed & Chr(10) & Chr(10) & "Vill du utföra en felsökning?", vbYesNo, "TRINITY")
            If a = vbNo Then Exit Sub
            ' Stop
            Resume
        End If
        MsgBox("Runtime Error '" & en & "':" & Chr(10) & Chr(10) & ed & " in grdSchedule_DblClick.", vbCritical, "Error")
        Trinity.Helper.WriteToLogFile("ERROR IN frmBooking/grdSchedule_DblClick!")
    End Sub

    Private Sub grdSchedule_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSchedule.CellDoubleClick
        Saved = False
        Dim SpotID As String
        Dim ID As String
        Dim TmpEI As Trinity.cExtendedInfo
        Dim Chan As String
        Dim BT As String
        Dim TmpCol As System.Windows.Forms.DataGridViewColumn
        Dim TmpRow As System.Windows.Forms.DataGridViewRow
        Dim TmpWeek As Trinity.cWeek
        Dim TmpSpot As Trinity.cBookedSpot
        Dim Bid As Single
        Dim Gross As Decimal

        saveCounter += 1

        Dim a As Integer
        Dim en As Long
        Dim ed As String
        On Error GoTo grdSchedule_DblClick_Error

        Trinity.Helper.WriteToLogFile("Start of frmBooking/grdSchedule_DblClick")

        If Not Campaign.RootCampaign Is Nothing Then Exit Sub

        Chan = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        BT = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Name
        Dim TmpFilm As Trinity.cFilm = Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(cmbFilm.Text)

        ID = grdSchedule.SelectedRows.Item(0).Tag
        TmpEI = Campaign.ExtendedInfos(ID)
        SpotID = CreateGUID()
        If Campaign.Channels(Chan).UseBid Then
            Bid = InputBox("Set your bid:", "T R I N I T Y", 0)
            Gross = TmpEI.GrossPrice(TmpFilm, Bid)
        Else
            Gross = TmpEI.GrossPrice(TmpFilm)
        End If

        'warn if you are booking a spot that has no ratings for this week
        If Campaign.Channels(Chan).BookingTypes(BT).Weeks(Campaign.Channels(Chan).BookingTypes(BT).GetWeek(TmpEI.AirDate).Name).Films(TmpFilm.Name).Share = 0 Then
            If MsgBox("This film are not scheduled for this week, do you still want to book the spot?", MsgBoxStyle.YesNo, "Continue?") = MsgBoxResult.No Then Exit Sub
        End If
        Campaign.BookedSpots.Add(SpotID, ID, TmpEI.Channel, TmpEI.AirDate, TmpEI.MaM, TmpEI.ProgAfter, TmpEI.ProgAfter, "", Gross, TmpEI.NetPrice(TmpFilm, Campaign.Channels(Chan).BookingTypes(BT), Bid), TmpEI.ChannelEstimate, TmpEI.EstimatedRating, TmpEI.EstimatedRatingBuyingTarget, cmbFilm.Text, BT, InStr(TmpEI.Remark, "L") > 0, InStr(TmpEI.Remark, "R") > 0, Bid)

        'AddToPivot(Campaign.BookedSpots(SpotID))
        For Each TmpWeek In Campaign.Channels(TmpEI.Channel).BookingTypes(BT).Weeks
            If Campaign.Channels(TmpEI.Channel).BookingTypes(BT).Weeks(TmpWeek.Name).StartDate <= TmpEI.AirDate.ToOADate Then
                If Campaign.Channels(TmpEI.Channel).BookingTypes(BT).Weeks(TmpWeek.Name).EndDate >= TmpEI.AirDate.ToOADate Then
                    Campaign.BookedSpots(SpotID).week = Campaign.Channels(TmpEI.Channel).BookingTypes(BT).Weeks(TmpWeek.Name)
                End If
            End If
        Next
        Campaign.BookedSpots(SpotID).Filmcode = Campaign.BookedSpots(SpotID).week.Films(cmbFilm.Text).Filmcode
        Campaign.BookedSpots(SpotID).RecentlyBooked = True
        For Each TmpSpot In Campaign.BookedSpots
            If TmpSpot.MostRecentlyBooked AndAlso TmpSpot.ID <> SpotID Then
                TmpSpot.MostRecentlyBooked = False
            End If
        Next
        Campaign.BookedSpots(SpotID).MostRecentlyBooked = True

        Dim TmpSex As String
        If Campaign.MainTarget.TargetType = Trinity.cTarget.TargetTypeEnum.trgMnemonicTarget Then
            Select Case Campaign.MainTarget.TargetNameNice.ToUpper.ToString.Substring(0, 1)
                Case "A" : TmpSex = ""
                Case "M" : TmpSex = "M"
                Case "W" : TmpSex = "W"
                Case Else : TmpSex = ""
            End Select
        Else
            TmpSex = ""
        End If
        Dim AgeTRP(0 To 13) As Single
        Dim TmpArray() As String = {TmpSex & "3-11", TmpSex & "12-14", TmpSex & "15-19", TmpSex & "20-24", TmpSex & "25-29", TmpSex & "30-34", TmpSex & "35-39", TmpSex & "40-44", TmpSex & "45-49", TmpSex & "50-54", TmpSex & "55-59", TmpSex & "60-64", TmpSex & "60-69", TmpSex & "70+"}
        Dim AvgRating As Single
        For i As Integer = 0 To 13
            AgeTRP(i) = 0
            If Not TmpEI Is Nothing AndAlso Not TmpEI.BreakList Is Nothing Then
                For b As Integer = 1 To TmpEI.BreakList.Count
                    Dim TmpStr As String = TmpArray(i)
                    AgeTRP(i) = AgeTRP(i) + DirectCast(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod), Trinity.cPeriod).Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod).Adedge, TmpStr))
                    AvgRating = AvgRating + DirectCast(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod), Trinity.cPeriod).Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod).Adedge, TmpStr)) / 14
                Next
            End If
        Next
        For i As Integer = 0 To 13
            Campaign.BookedSpots(SpotID).AgeIndex(i) = AgeTRP(i) / AvgRating
        Next
        Campaign.RFEstimation.Calculate()
        For Each TmpCol In grdSchedule.Columns
            grdSchedule.SelectedRows.Item(0).Cells(TmpCol.Name).Style.ForeColor = Drawing.Color.Red
        Next
        If grdSchedule.Columns.Contains("colSolus") OrElse grdSchedule.Columns.Contains("colCPS") Then
            For Each TmpRow In grdSchedule.Rows
                ID = TmpRow.Tag
                If ID <> "" Then
                    Campaign.ExtendedInfos(ID).Solus = 0
                    If grdSchedule.Columns.Contains("colSolus") Then
                        TmpRow.Cells("colSolus").Value = Format(0, "0.0")
                    End If
                    If grdSchedule.Columns.Contains("colCPS") Then
                        TmpRow.Cells("colCPS").Value = Format(0, "0 kr")
                    End If
                End If
            Next
        End If
        UpdateSpotlist()
        UpdateBookedTRP()
        UpdateLeftToBook()
        UpdateFilm()
        UpdateDaypart()
        UpdateRF()
        UpdateEstimatedTrend()
        UpdatePrimePeak()

        'update budget filtering if available
        updateFilterBudget()
        deleteSpotsOnBudget()

        'make sure the changes are done in the graphic part aswell
        GfxSchedule2.updateGraphics(TmpEI.AirDate, TmpEI.MaM, TmpEI.ProgAfter)

        'save promt
        If saveCounter = 10 Then
            saveCounter = 0
            If MsgBox("It has been a while since your last save. Do you wish to save now?", MsgBoxStyle.YesNo, "Save campaign") = MsgBoxResult.Yes Then
                'if the file name is empty (not saved before) it opens a save file dialog and then saves the campaign
                If TrinitySettings.SaveCampaignsAsFiles Then
                    If Campaign.Filename = "" Then
                        Dim dlgDialog As New Windows.Forms.SaveFileDialog
                        dlgDialog.Title = "Save campaign as..."
                        dlgDialog.FileName = "*.cmp"
                        dlgDialog.DefaultExt = "*.cmp"
                        dlgDialog.Filter = "Trinity campaigns|*.cmp"
                        dlgDialog.InitialDirectory = TrinitySettings.CampaignFiles
                        If dlgDialog.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
                        Me.Cursor = Windows.Forms.Cursors.WaitCursor
                        Campaign.SaveCampaign(dlgDialog.FileName)
                        Me.Cursor = Windows.Forms.Cursors.Default
                    Else
                        'if the filename is set the campagn is saved
                        Me.Cursor = Windows.Forms.Cursors.WaitCursor
                        Campaign.SaveCampaign(Campaign.Filename)
                        Me.Cursor = Windows.Forms.Cursors.Default
                    End If
                Else
                    frmMain.SaveToDB()
                End If
            End If
        End If
        On Error GoTo 0
        Exit Sub

grdSchedule_DblClick_Error:

        en = Err.Number
        ed = Err.Description
        If IsIDE() Then
            a = MsgBox("Error:" & Chr(10) & Chr(10) & "Runtime Error '" & en & "':" & Chr(10) & Chr(10) & ed & Chr(10) & Chr(10) & "Vill du utföra en felsökning?", vbYesNo, "TRINITY")
            If a = vbNo Then Exit Sub
            'Stop
            Resume
        End If
        MsgBox("Runtime Error '" & en & "':" & Chr(10) & Chr(10) & ed & " in grdSchedule_DblClick.", vbCritical, "Error")
        Trinity.Helper.WriteToLogFile("ERROR IN frmBooking/grdSchedule_DblClick!")
    End Sub

    Private Sub cmdEstimate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEstimate.Click
        Saved = False
        Dim TmpPeriod As Trinity.cPeriod
        Dim TmpBreak As Trinity.cBreak
        Dim TmpChannel As Trinity.cChannel
        Dim Tmpstr As String
        Dim b As Long

        Dim TmpRow As System.Windows.Forms.DataGridViewRow
        Dim RowCount As Integer
        Dim ID As String
        Dim TmpEI As Trinity.cExtendedInfo
        Dim TotRating As Single
        Dim TotRatingBT As Single
        Dim TotRatingSec As Single
        Dim Chan As String
        Dim BT As String
        Dim Count As Integer

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        Chan = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        BT = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Name


        '********* get breaks for standard period
        If txtCustomPeriod.Text = "" Then
            lblStatus.Text = "Finding breaks..."
            System.Windows.Forms.Application.DoEvents()
            For Each kv As DictionaryEntry In Campaign.ExtendedInfos
                TmpEI = kv.Value
                Tmpstr = TmpEI.EstimationPeriod
                If Not Campaign.EstimationPeriods.Exists(Tmpstr) Then
                    TmpPeriod = New Trinity.cPeriod
                    TmpPeriod.Period = TmpEI.EstimationPeriod
                    TmpPeriod.Adedge.setArea(Campaign.Area)
                    TmpPeriod.Adedge.setChannelsArea(Campaign.ChannelString, Campaign.Area)
                    TmpPeriod.Adedge.setPeriod(TmpPeriod.Period)
                    Trinity.Helper.AddTargetsToAdedge(TmpPeriod.Adedge)
                    TmpPeriod.Adedge.setTargetMnemonic("M3+,W3+,3-49,50+,3+", True)
                    TmpPeriod.BreakCount = TmpPeriod.Adedge.Run
                    TmpPeriod.Breaks = New ArrayList
                    For b = 0 To TmpPeriod.BreakCount - 1
                        If TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBreaktitle, b) = "Break" OrElse TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBreaktitle, b).ToString.ToUpper.StartsWith("REKLAME") OrElse (Campaign.Area = "DK" AndAlso Not TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aChannel, b).ToString.ToUpper = "TV 2") Then
                            TmpBreak = New Trinity.cBreak(Campaign)
                            TmpBreak.AirDate = Date.FromOADate(TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aDate, b))
                            TmpBreak.BreakIdx = b
                            TmpBreak.BreakList = TmpPeriod.Period
                            TmpBreak.ID = TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBreakSequenceID, b)
                            TmpBreak.MaM = TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aFromTime, b) \ 60
                            TmpBreak.ProgAfter = TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBreakProgAfter, b)
                            TmpBreak.ProgBefore = TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBreakProgBefore, b)
                            For Each TmpChannel In Campaign.Channels

                                'InStr("string to search in", "String we are looking for")
                                If InStr("," & TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aChannel, b).ToString.ToUpper & ",", "," & TmpChannel.AdEdgeNames.ToUpper & ",") > 0 Then
                                    TmpBreak.Channel = TmpChannel
                                    Exit For
                                End If
                            Next
                            TmpPeriod.Breaks.Add(TmpBreak) ', Format(TmpBreak.AirDate, "yymmdd") & TmpBreak.ID)
                        End If
                    Next
                    Campaign.EstimationPeriods.Add(TmpPeriod, TmpPeriod.Period)
                End If

            Next
        End If

        '********* get breaks in custom period
        Tmpstr = txtCustomPeriod.Text
        If Not Campaign.EstimationPeriods.Exists(Tmpstr) Then
            lblStatus.Text = "Finding breaks..."
            System.Windows.Forms.Application.DoEvents()
            TmpPeriod = New Trinity.cPeriod
            TmpPeriod.Period = txtCustomPeriod.Text
            TmpPeriod.Adedge.setArea(Campaign.Area)
            TmpPeriod.Adedge.setChannelsArea(Campaign.ChannelString, Campaign.Area)
            TmpPeriod.Adedge.setPeriod(TmpPeriod.Period)
            Trinity.Helper.AddTargetsToAdedge(TmpPeriod.Adedge)
            TmpPeriod.Adedge.setTargetMnemonic("M3+,W3+,3-49,50+,3+", True)
            TmpPeriod.BreakCount = TmpPeriod.Adedge.Run
            TmpPeriod.Breaks = New ArrayList
            For b = 0 To TmpPeriod.BreakCount - 1
                If TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBreaktitle, b) = "Break" OrElse TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBreaktitle, b).ToString.ToUpper = "REKLAMEBLOK" OrElse (Campaign.Area = "DK" AndAlso Not TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aChannel, b).ToString.ToUpper = "TV 2") Then
                    TmpBreak = New Trinity.cBreak(Campaign)
                    TmpBreak.AirDate = Date.FromOADate(TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aDate, b))
                    TmpBreak.BreakIdx = b
                    TmpBreak.BreakList = TmpPeriod.Period
                    TmpBreak.ID = TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBreakSequenceID, b)
                    TmpBreak.MaM = TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aFromTime, b) \ 60
                    TmpBreak.ProgAfter = TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBreakProgAfter, b)
                    TmpBreak.ProgBefore = TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aBreakProgBefore, b)
                    TmpBreak.Duration = TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aDuration, b)
                    For Each TmpChannel In Campaign.Channels
                        If InStr(UCase("," & TmpPeriod.Adedge.getAttrib(Connect.eAttribs.aChannel, b) & ","), UCase("," & TmpChannel.AdEdgeNames & ",")) > 0 Then
                            TmpBreak.Channel = TmpChannel
                            Exit For
                        End If
                    Next
                    TmpPeriod.Breaks.Add(TmpBreak) ', Format(TmpBreak.AirDate, "yyMMdd") & TmpBreak.ID)
                End If
            Next
            Campaign.EstimationPeriods.Add(TmpPeriod, TmpPeriod.Period)
        End If
        '********* End getting breaks


        lblStatus.Visible = True
        For Each TmpRow In grdSchedule.SelectedRows
            RowCount += 1
            lblStatus.Text = "Estimating... " & Format(RowCount / grdSchedule.SelectedRows.Count, "P0")
            System.Windows.Forms.Application.DoEvents()
            ID = TmpRow.Tag
            If ID = "" Then
                lblStatus.Visible = False
                Exit Sub
            End If
            TmpEI = Campaign.ExtendedInfos(ID)
            If txtCustomPeriod.Text = "" Then
                TmpPeriod = Campaign.EstimationPeriods(TmpEI.EstimationPeriod)
            Else
                TmpPeriod = Campaign.EstimationPeriods(txtCustomPeriod.Text)
            End If
            TmpEI.EstimatedOnPeriod = TmpPeriod.Period
            TmpEI.BreakList = Trinity.Helper.Estimate(TmpEI.AirDate, TmpEI.MaM, TmpEI.ProgAfter, TmpEI.Channel, TmpPeriod.Breaks)
            TotRating = 0
            TotRatingBT = 0
            TotRatingSec = 0
            Count = 0
            If Not TmpEI.BreakList Is Nothing Then
                For b = 1 To TmpEI.BreakList.Count
                    TotRating += TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, Campaign.MainTarget))
                    If TmpEI.EstimationTarget = "" Then
                        TotRatingBT += TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Target))
                    Else
                        TmpEI.EstimationTarget = TmpEI.EstimationTarget.Replace("P", "").Trim
                        If Trinity.Helper.TargetIndex(TmpPeriod.Adedge, TmpEI.EstimationTarget) < 0 Then
                            TmpPeriod.Adedge.setTargetMnemonic(TmpEI.EstimationTarget, True)
                            TmpPeriod.Adedge.Run()
                        End If
                        TotRatingBT += TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, TmpEI.EstimationTarget))
                    End If
                    TotRatingSec += TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, Campaign.SecondaryTarget))
                    Count += 1
                Next
                If Count > 0 Then
                    TmpEI.EstimatedRating = TotRating / Count
                    TmpEI.EstimatedRatingBuyingTarget = TotRatingBT / Count
                    TmpEI.EstimatedRatingSecondTarget = TotRatingSec / Count
                End If
                TmpEI.Estimation = Trinity.Helper.EstimationQuality(TmpEI.BreakList, TmpEI, TmpPeriod.Adedge)
            End If
        Next
        lblStatus.Text = "Estimating...100%"
        System.Windows.Forms.Application.DoEvents()
        UpdateSchedule(False, False)
        lblStatus.Text = ""
        grdSchedule_RowEnter(New Object, New Windows.Forms.DataGridViewCellEventArgs(1, grdSchedule.SelectedRows(0).Index))
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub grdSchedule_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSchedule.RowEnter
        UpdateInfoPanels(grdSchedule.Rows(e.RowIndex).Tag)
        If grdSchedule.SelectedRows.Count > 0 Then
            Dim ID As String = grdSchedule.SelectedRows.Item(0).Tag
            Dim TmpEI As Trinity.cExtendedInfo = Campaign.ExtendedInfos(ID)
            If TmpEI.BreakList Is Nothing OrElse TmpEI.BreakList.Count = 0 Then
                cmdTweak.Enabled = False
            Else
                cmdTweak.Enabled = True
            End If
        End If
    End Sub

    Sub UpdateInfoPanels(ByVal ID As Object)
        UpdatePictureAndInfo(ID)
        UpdateDetails(ID)
        UpdateOther(ID)
        UpdateTrend(ID)
        UpdateProfile(ID)
        UpdatePIB(ID)
        UpdateGender(ID)
        UpdatePanelPositions()
    End Sub

    Sub menuFilterFunction()
    End Sub
    Private Sub cmdScheduleFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal dropDownMenuParent As String = "") Handles cmdScheduleFilter.Click
        Dim i As Integer
        Dim TmpWeek As Trinity.cWeek
        Dim TmpBt As Trinity.cBookingType

        ' Make sure that a booking type is chosen
        If cmbChannel.SelectedItem Is Nothing Then
            MessageBox.Show("No booking type is chosen!", "T R I N I T Y")
            Exit Sub
        Else
            TmpBt = cmbChannel.SelectedItem
        End If

        Dim WeekDays() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}

        Dim mnuFilter As New Windows.Forms.ContextMenuStrip

        Dim mnuAttributes As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Availability")
        Dim mnuDayparts As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Dayparts")
        mnuDayparts.Tag = "Daypart"
        Dim mnuWeeks As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Weeks")
        mnuWeeks.Tag = "Week"
        Dim mnuWeekdays As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Weekdays")
        mnuWeekdays.Tag = "Weekday"

        mnuFilter.Items.Add("-")
        Dim mnuRemoveAllFilters As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("No filters")

        AddHandler mnuRemoveAllFilters.Click, AddressOf mnuRemoveAllFilters_Click

        Dim mnuOnlyShowBuyable As New Windows.Forms.ToolStripMenuItem
        mnuOnlyShowBuyable.Text = "Only show buyable"
        mnuOnlyShowBuyable.Checked = BookingFilter.Data("Availability", "OnlyShowBuyable")
        AddHandler mnuOnlyShowBuyable.Click, AddressOf mnuOnlyShowBuyable_Click
        mnuAttributes.DropDownItems.Add(mnuOnlyShowBuyable)

        Dim mnuShowAffordableWeek As New Windows.Forms.ToolStripMenuItem
        mnuShowAffordableWeek.Text = "Only show affordable (Week)"
        mnuShowAffordableWeek.Checked = Not BookingFilter.Data("Availability", "ShowAffordableWeek")
        'mnuShowAffordableWeek.Enabled = False
        AddHandler mnuShowAffordableWeek.Click, AddressOf mnuShowAffordableWeek_Click
        mnuAttributes.DropDownItems.Add(mnuShowAffordableWeek)

        Dim mnuShowAffordableCampaign As New Windows.Forms.ToolStripMenuItem
        mnuShowAffordableCampaign.Text = "Only show affordable (Campaign)"
        mnuShowAffordableCampaign.Checked = Not BookingFilter.Data("Availability", "ShowAffordableCampaign")
        'mnuShowAffordableCampaign.Enabled = False
        AddHandler mnuShowAffordableCampaign.Click, AddressOf mnuShowAffordableCampaign_Click
        mnuAttributes.DropDownItems.Add(mnuShowAffordableCampaign)

        mnuDayparts.DropDownItems.Clear()
        mnuWeeks.DropDownItems.Clear()
        mnuWeekdays.DropDownItems.Clear()

        For i = 0 To TmpBt.Dayparts.Count - 1
            Dim TmpSubMenu As New Windows.Forms.ToolStripMenuItem
            TmpSubMenu.Checked = BookingFilter.Data("Daypart", TmpBt.Dayparts(i).Name)
            TmpSubMenu.Text = TmpBt.Dayparts(i).Name
            TmpSubMenu.Tag = i
            AddHandler TmpSubMenu.Click, AddressOf mnuDaypart_Click
            mnuDayparts.DropDownItems.Add(TmpSubMenu)
        Next
        mnuDayparts.DropDownItems.Add("-")
        mnuDayparts.DropDownItems.Add("Invert", Nothing, AddressOf InvertMenu)

        For Each TmpWeek In Campaign.Channels(1).BookingTypes(1).Weeks
            Dim TmpSubMenu As New Windows.Forms.ToolStripMenuItem
            TmpSubMenu.Checked = BookingFilter.Data("Week", TmpWeek.Name)
            TmpSubMenu.Text = TmpWeek.Name
            TmpSubMenu.Tag = TmpWeek
            AddHandler TmpSubMenu.Click, AddressOf mnuWeek_Click
            mnuWeeks.DropDownItems.Add(TmpSubMenu)
        Next
        mnuWeeks.DropDownItems.Add("-")
        mnuWeeks.DropDownItems.Add("Invert", Nothing, AddressOf InvertMenu)


        For i = 0 To 6
            Dim TmpSubMenu As New Windows.Forms.ToolStripMenuItem
            TmpSubMenu.Checked = BookingFilter.Data("Weekday", WeekDays(i))
            TmpSubMenu.Text = WeekDays(i)
            TmpSubMenu.Tag = i

            AddHandler TmpSubMenu.Click, AddressOf mnuWeekday_Click
            mnuWeekdays.DropDownItems.Add(TmpSubMenu)
        Next
        mnuWeekdays.DropDownItems.Add("-")
        mnuWeekdays.DropDownItems.Add("Invert", Nothing, AddressOf InvertMenu)



        mnuFilter.Show(stpSchedule, cmdScheduleFilter.Bounds.Left, cmdScheduleFilter.Bounds.Height)
        If dropDownMenuParent = "WeekdaysDropDown"
            mnuWeekdays.DropDown.Show()
        ElseIf dropDownMenuParent = "WeeksDropDown"
            mnuWeeks.DropDown.Show()
        ElseIf dropDownMenuParent = "DaypartsDropDown"
            mnuDayparts.DropDown.Show()

        End If
    End Sub
    Sub InvertMenu(sender As ToolStripMenuItem, e As EventArgs)
        Dim _mnu As ToolStripMenuItem = DirectCast(sender, System.Windows.Forms.ToolStripMenuItem).OwnerItem

        For i As Integer = 0 To _mnu.DropDownItems.Count - 3
            Dim _item As ToolStripMenuItem = _mnu.DropDownItems(i)
            BookingFilter.Data(_mnu.Tag, _item.Text) = Not BookingFilter.Data(_mnu.Tag, _item.Text)
        Next
        UpdateSchedule(True, False)
        UpdateSpotlist()
    End Sub

    Private Sub mnuActualWeek_Click(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs)
        BookingFilter.Data("ActualWeek", sender.Text) = Not BookingFilter.Data("ActualWeek", sender.Text)
        UpdateSpotlist()
        cmdSpotlistFilter_Click(sender, e, sender.GetCurrentParent.AccessibilityObject.Name)
    End Sub

    Private Sub mnuother_Click(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs)
        showOtherBookedSpots = Not showOtherBookedSpots
        UpdateSpotlist()
        cmdScheduleFilter_Click(sender, e, sender.GetCurrentParent.Name)
    End Sub

    Private Sub mnuOnlyShowBuyable_Click(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs)
        BookingFilter.Data("Availability", "OnlyShowBuyable") = Not BookingFilter.Data("Availability", "OnlyShowBuyable")
        UpdateSchedule(True, False)
        cmdScheduleFilter_Click(sender, e, sender.GetCurrentParent.AccessibilityObject.Name)
    End Sub

    Private Sub mnuRemoveAllFilters_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        BookingFilter = New Trinity.cFilter
        UpdateSchedule(True, False)
        UpdateSpotlist()
    End Sub

    Private Sub mnuDaypart_Click(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles DaypartsToolStripMenuItem.Click
        BookingFilter.Data("Daypart", sender.Text) = Not BookingFilter.Data("Daypart", sender.Text)
        UpdateSchedule(True, False)
        cmdScheduleFilter_Click(sender, e, sender.GetCurrentParent.AccessibilityObject.Name)
    End Sub

    Private Sub mnuActualDaypart_Click(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs) Handles DaypartsToolStripMenuItem.Click
        BookingFilter.Data("Actual Daypart", sender.Text) = Not BookingFilter.Data("Actual Daypart", sender.Text)
        UpdateSpotlist()
        cmdSpotlistFilter_Click(sender, e, sender.GetCurrentParent.AccessibilityObject.Name)
    End Sub

    Private Sub mnuWeek_Click(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs)
        BookingFilter.Data("Week", sender.Text) = Not BookingFilter.Data("Week", sender.Text)
        UpdateSchedule(True, False)
        cmdScheduleFilter_Click(sender, e, sender.GetCurrentParent.AccessibilityObject.Name)
    End Sub

    Private Sub mnuWeekday_Click(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs)
        BookingFilter.Data("Weekday", sender.Text) = Not BookingFilter.Data("Weekday", sender.Text)
        UpdateSchedule(True, False)
        cmdScheduleFilter_Click(sender, e, sender.GetCurrentParent.AccessibilityObject.Name)
    End Sub

    Private Sub mnuActualWeekday_Click(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs)
        BookingFilter.Data("Actual Weekday", sender.Text) = Not BookingFilter.Data("Actual Weekday", sender.Text)
        UpdateSpotlist()
        cmdSpotlistFilter_Click(sender, e, sender.GetCurrentParent.AccessibilityObject.Name)
    End Sub

    'Hannes
    Private Sub mnuWeekMulti_Click(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs)
        BookingFilter.Data("Week", sender.Text) = Not BookingFilter.Data("Week", sender.Text)
        UpdateSchedule(True, False)
        cmdScheduleFilter_Click(sender, e, sender.GetCurrentParent.AccessibilityObject.Name)
    End Sub

    'Hannes
    Private Sub mnuWeekdayMulti_Click(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs)
        BookingFilter.Data("Weekday", sender.Text) = Not BookingFilter.Data("Weekday", sender.Text)
        UpdateSchedule(True, False)
        cmdScheduleFilter_Click(sender, e, sender.GetCurrentParent.AccessibilityObject.Name)
    End Sub

    Private Sub mnuBtnOKMulti_click(ByVal sender As Object, ByVal e As System.EventArgs)

        ProgramFilterChecked = Not ProgramFilterChecked

        Dim trim() As Char = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", " "}

        If ProgramFilterChecked Then
            For Each row As DataGridViewRow In grdSchedule.Rows
                Dim s As String = row.Cells("colProgram").Value.ToString.ToUpper.TrimEnd(trim)
                'BookingFilter.Data("Program", s) = Not BookingFilter.Data("Program", s)
                BookingFilter.Data("Program", s) = False
            Next
            For Each row As DataGridViewRow In grdSpotlist.SelectedRows
                Dim s As String = row.Cells("colSpotlistProgram").Value.ToString.ToUpper.TrimEnd(trim)
                BookingFilter.Data("Program", s) = Not BookingFilter.Data("Program", s)
            Next
        Else
            BookingFilter.AllTrue("Program")
        End If

        UpdateSchedule(True, False)
        'If ProgramFilterChecked Then grdSchedule.SelectAll()

    End Sub

    Private Sub mnuBtnOKMulti2_click(ByVal sender As Object, ByVal e As System.EventArgs)

        TimeFilterChecked = Not TimeFilterChecked
        Dim maxmam As Integer = 28 * 60

        If TimeFilterChecked Then
            For i As Integer = 0 To maxmam
                BookingFilter.Data("MaM", i) = False
            Next
            For Each row As DataGridViewRow In grdSpotlist.SelectedRows
                For i As Integer = Trinity.Helper.Tid2Mam(row.Cells("colSpotlistTime").Value) - 5 To Trinity.Helper.Tid2Mam(row.Cells("colSpotlistTime").Value) + 5
                    BookingFilter.Data("MaM", i) = Not BookingFilter.Data("MaM", i)
                Next
            Next
        Else
            BookingFilter.AllTrue("MaM")
        End If
        UpdateSchedule(True, False)
        'If TimeFilterChecked Then grdSchedule.SelectAll()
    End Sub

    Private Sub mnuShowAffordableWeek_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        BookingFilter.Data("Availability", "ShowAffordableWeek") = Not BookingFilter.Data("Availability", "ShowAffordableWeek")

        'we only filter either per week or per campaign, not both
        BookingFilter.Data("Availability", "ShowAffordableCampaign") = True
        bolFilterBudget = True
        updateFilterBudget()
        deleteSpotsOnBudget()

        bolFilterBudget = Not BookingFilter.Data("Availability", "ShowAffordableWeek")
    End Sub


    Private Sub mnuShowAffordableCampaign_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        BookingFilter.Data("Availability", "ShowAffordableCampaign") = Not BookingFilter.Data("Availability", "ShowAffordableCampaign")

        'we only filter either per week or per campaign, not both
        BookingFilter.Data("Availability", "ShowAffordableWeek") = True
        bolFilterBudget = True
        updateFilterBudget()
        deleteSpotsOnBudget()

        bolFilterBudget = Not BookingFilter.Data("Availability", "ShowAffordableCampaign")
    End Sub

    Private Sub cmdSpotlistFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal dropDownMenuParent As String = "") Handles cmdSpotlistFilter.Click
        Dim WeekDays() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}

        Dim mnuFilter As New Windows.Forms.ContextMenuStrip

        Dim mnuChannel As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Channel")
        mnuChannel.Tag = "Channel"
        'Dim mnuAttributes As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Availability")
        Dim mnuDayparts As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Dayparts")
        mnuDayparts.Tag = "Actual Daypart"
        Dim mnuWeeks As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Weeks")
        mnuWeeks.Tag = "ActualWeek"
        Dim mnuWeekdays As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Weekdays")
        mnuWeekdays.Tag = "Actual Weekday"

        mnuFilter.Items.Add("-")
        Dim mnuRemoveAllFilters As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("No filters")
        AddHandler mnuRemoveAllFilters.Click, AddressOf mnuRemoveAllFilters_Click

        Dim list As List(Of Trinity.cChannel) = readChannels
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                If TmpBT.BookIt Then
                    Dim TmpSubMenu As New Windows.Forms.ToolStripMenuItem
                    TmpSubMenu.Checked = BookingFilter.Data("Channel", TmpBT.ToString)
                    TmpSubMenu.Text = TmpBT.ToString
                    TmpSubMenu.Tag = TmpBT
                    AddHandler TmpSubMenu.Click, AddressOf mnuBookingType_Click
                    mnuChannel.DropDownItems.Add(TmpSubMenu)
                    'removes any occurances in the list of read channels
                    list.Remove(TmpChan)
                End If
            Next
        Next



        For i As Integer = 0 To list.Count - 1
            Dim TmpSubMenu As New Windows.Forms.ToolStripMenuItem
            TmpSubMenu.Checked = BookingFilter.Data("Channel", list.Item(i).BookingTypes("Specifics").ToString)
            TmpSubMenu.Text = list.Item(i).BookingTypes("Specifics").ToString
            TmpSubMenu.Tag = list.Item(i).BookingTypes("Specifics")
            AddHandler TmpSubMenu.Click, AddressOf mnuBookingType_Click
            mnuChannel.DropDownItems.Add(TmpSubMenu)
        Next
        ' Add the invert
        mnuChannel.DropDownItems.Add("-")
        Dim TmpChannelInvert As Windows.Forms.ToolStripMenuItem = mnuChannel.DropDownItems.Add("Invert")
        AddHandler TmpChannelInvert.Click, AddressOf InvertMenu

        ' Add dayparts
        For Each tmpBT As Trinity.cDaypart In Campaign.Dayparts
            Dim TmpSubMenu As New Windows.Forms.ToolStripMenuItem
            TmpSubMenu.Checked = BookingFilter.Data("Actual Daypart", tmpBT.Name)
            TmpSubMenu.Text = tmpBT.Name
            TmpSubMenu.Tag = tmpBT.Name
            mnuDayparts.DropDownItems.Add(TmpSubMenu)
            AddHandler TmpSubMenu.Click, AddressOf mnuActualDaypart_Click
        Next
        ' Add the invert
        mnuDayparts.DropDownItems.Add("-")
        Dim TmpDaypartInvert As Windows.Forms.ToolStripMenuItem = mnuDayparts.DropDownItems.Add("Invert")
        AddHandler TmpDaypartInvert.Click, AddressOf InvertMenu

        For Each TmpWeek As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
            Dim TmpSubMenu As New Windows.Forms.ToolStripMenuItem
            TmpSubMenu.Checked = BookingFilter.Data("ActualWeek", TmpWeek.Name)
            TmpSubMenu.Text = TmpWeek.Name
            TmpSubMenu.Tag = TmpWeek
            AddHandler TmpSubMenu.Click, AddressOf mnuActualWeek_Click
            mnuWeeks.DropDownItems.Add(TmpSubMenu)
        Next
        ' Add the invert
        mnuWeeks.DropDownItems.Add("-")
        Dim TmpWeeksInvert As Windows.Forms.ToolStripMenuItem = mnuWeeks.DropDownItems.Add("Invert")
        AddHandler TmpWeeksInvert.Click, AddressOf InvertMenu

        ' Add weekdays 
        For i As Integer = 0 To WeekDays.Length - 1
            Dim TmpSubMenu As New ToolStripMenuItem
            TmpSubMenu.Checked = BookingFilter.Data("Actual Weekday", WeekDays(i))
            TmpSubMenu.Text = WeekDays(i)
            TmpSubMenu.Tag = i
            mnuWeekdays.DropDownItems.Add(TmpSubMenu)
            AddHandler TmpSubMenu.Click, AddressOf mnuActualWeekday_Click
        Next
        ' Add the invert
        mnuWeekdays.DropDownItems.Add("-")
        Dim TmpWeekdaysInvert As Windows.Forms.ToolStripMenuItem = mnuWeekdays.DropDownItems.Add("Invert")
        AddHandler TmpWeekdaysInvert.Click, AddressOf InvertMenu

        If otherBookedSpots.Count > 0 Then
            Dim mnuOther As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Read Spots")
            mnuOther.Checked = showOtherBookedSpots
            mnuOther.Text = "Read spots"
            mnuOther.Tag = "read"
            AddHandler mnuOther.Click, AddressOf mnuother_Click
            'mnuChannel.DropDownItems.Add(mnuOther)
        End If

        'Dim mnuOnlyShowBuyable As New Windows.Forms.ToolStripMenuItem
        'mnuOnlyShowBuyable.Text = "Only show buyable"
        'mnuOnlyShowBuyable.Checked = BookingFilter.Data("Availability", "OnlyShowBuyable")
        'AddHandler mnuOnlyShowBuyable.Click, AddressOf mnuOnlyShowBuyable_Click
        'mnuAttributes.DropDownItems.Add(mnuOnlyShowBuyable)

        'Dim mnuShowAffordableWeek As New Windows.Forms.ToolStripMenuItem
        'mnuShowAffordableWeek.Text = "Only show affordable (Week)"
        ''mnuShowAffordableWeek.Checked = BookingFilter.Data("Availability", "ShowAffordableWeek")
        'mnuShowAffordableWeek.Enabled = False
        'AddHandler mnuShowAffordableWeek.Click, AddressOf mnuShowAffordableWeek_Click
        'mnuAttributes.DropDownItems.Add(mnuShowAffordableWeek)

        'Dim mnuShowAffordableCampaign As New Windows.Forms.ToolStripMenuItem
        'mnuShowAffordableCampaign.Text = "Only show affordable (Campaign)"
        ''mnuShowAffordableCampaign.Checked = BookingFilter.Data("Availability", "ShowAffordableCampaign")
        'mnuShowAffordableCampaign.Enabled = False
        'AddHandler mnuShowAffordableCampaign.Click, AddressOf mnuShowAffordableCampaign_Click
        'mnuAttributes.DropDownItems.Add(mnuShowAffordableCampaign)

        'mnuDayparts.DropDownItems.Clear()
        'mnuWeeks.DropDownItems.Clear()
        'mnuWeekdays.DropDownItems.Clear()
        'For i = 0 To Campaign.DaypartCount - 1
        '    Dim TmpSubMenu As New Windows.Forms.ToolStripMenuItem
        '    TmpSubMenu.Checked = BookingFilter.Data("Daypart", Campaign.DaypartName(i))
        '    TmpSubMenu.Text = Campaign.DaypartName(i)
        '    TmpSubMenu.Tag = i
        '    AddHandler TmpSubMenu.Click, AddressOf mnuDaypart_Click
        '    mnuDayparts.DropDownItems.Add(TmpSubMenu)
        'Next

        'For Each TmpWeek In Campaign.Channels(1).BookingTypes(1).Weeks
        '    Dim TmpSubMenu As New Windows.Forms.ToolStripMenuItem
        '    TmpSubMenu.Checked = BookingFilter.Data("Week", TmpWeek.Name)
        '    TmpSubMenu.Text = TmpWeek.Name
        '    TmpSubMenu.Tag = TmpWeek
        '    AddHandler TmpSubMenu.Click, AddressOf mnuWeek_Click
        '    mnuWeeks.DropDownItems.Add(TmpSubMenu)
        'Next

        'For i = 0 To 6
        '    Dim TmpSubMenu As New Windows.Forms.ToolStripMenuItem
        '    TmpSubMenu.Checked = BookingFilter.Data("Weekday", WeekDays(i))
        '    TmpSubMenu.Text = WeekDays(i)
        '    TmpSubMenu.Tag = i
        '    AddHandler TmpSubMenu.Click, AddressOf mnuWeekday_Click
        '    mnuWeekdays.DropDownItems.Add(TmpSubMenu)
        'Next

        mnuFilter.Show(stpSpotlist, cmdSpotlistFilter.Bounds.Left, cmdSpotlistFilter.Bounds.Height)
        If dropDownMenuParent = "WeekdaysDropDown"
            mnuWeekdays.DropDown.Show()
        ElseIf dropDownMenuParent = "WeeksDropDown"
            mnuWeeks.DropDown.Show()
        ElseIf dropDownMenuParent = "DaypartsDropDown"
            mnuDayparts.DropDown.Show()
        ElseIf dropDownMenuParent = "ChannelDropDown"
            mnuChannel.DropDown.Show()
        End If


    End Sub

    Private Sub mnuBookingType_Click(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs)
        BookingFilter.Data("Channel", sender.Text) = Not BookingFilter.Data("Channel", sender.Text)
        UpdateSpotlist()
        cmdSpotlistFilter_Click(sender, e, sender.GetCurrentParent.AccessibilityObject.Name)
    End Sub

    Private Sub cmdSpotlistDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSpotlistDelete.Click
        Saved = False
        Dim TmpRow As Windows.Forms.DataGridViewRow

        If Not Campaign.RootCampaign Is Nothing Then Exit Sub

        If grdSpotlist.SelectedRows.Count = 0 Then
            System.Windows.Forms.MessageBox.Show("No spot selected.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        For Each TmpRow In grdSpotlist.SelectedRows
            If Not TmpRow.Tag = Nothing Then
                If Not Campaign.BookedSpots(TmpRow.Tag).Locked Then
                    If Campaign.ExtendedInfos.Exists(Campaign.BookedSpots(TmpRow.Tag).DatabaseID) Then
                        GfxSchedule2.updateGraphics(Campaign.ExtendedInfos(Campaign.BookedSpots(TmpRow.Tag).DatabaseID).AirDate, Campaign.ExtendedInfos(Campaign.BookedSpots(TmpRow.Tag).DatabaseID).MaM, Campaign.ExtendedInfos(Campaign.BookedSpots(TmpRow.Tag).DatabaseID).ProgAfter)
                    End If
                    Campaign.BookedSpots.Remove(TmpRow.Tag)
                End If
            End If
        Next
        Campaign.RFEstimation.Calculate()
        UpdateSchedule(False, False)
        UpdateBookedTRP()
        UpdateLeftToBook()
        UpdateFilm()
        UpdateDaypart()
        UpdateSpotlist()
        UpdateRF()
        UpdateEstimatedTrend()
        UpdatePrimePeak()


        'update budget filtering if available
        updateFilterBudget()
        deleteSpotsOnBudget()
    End Sub

    Private Sub cmdNotes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNotes.Click
        Saved = False
        If Not Campaign.RootCampaign Is Nothing Then Exit Sub
        If grdSpotlist.SelectedRows.Count = 0 Then
            System.Windows.Forms.MessageBox.Show("No spot selected.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim TmpRow As Windows.Forms.DataGridViewRow


        'Removed as of 15/8 BF
        'If Not Campaign.BookedSpots(grdSpotlist.SelectedRows.Item(0).Tag).Locked Then
        'Campaign.BookedSpots(grdSpotlist.SelectedRows.Item(0).Tag).Comments = InputBox("Notes:", "TRINITY", Campaign.BookedSpots(grdSpotlist.SelectedRows.Item(0).Tag).Comments)
        'End If

        Dim tmpDialogResult As inputDialogResult = inputDialogResult.Empty

        If grdSpotlist.SelectedRows.Count = 1 Then
            If Not grdSpotlist.SelectedRows.Item(0).Tag = Nothing Then
                'tmpNote = InputBox("Notes:", "TRINITY", Campaign.BookedSpots(grdSpotlist.SelectedRows.Item(0).Tag).Comments)
                tmpDialogResult = frmInputDialog.Show("Notes:", "T R I N I T Y", Campaign.BookedSpots(grdSpotlist.SelectedRows.Item(0).Tag).Comments)



            End If
        Else
            tmpDialogResult = frmInputDialog.Show("Notes:", "T R I N I T Y", "")
        End If

        If tmpDialogResult.DialogResult = Windows.Forms.DialogResult.OK Then
            For Each TmpRow In grdSpotlist.SelectedRows
                If Not TmpRow.Tag = Nothing Then
                    If Not Campaign.BookedSpots(TmpRow.Tag).Locked Then
                        Campaign.BookedSpots(TmpRow.Tag).Comments = tmpDialogResult.strResult
                    End If
                End If
            Next
        End If
        grdSpotlist.Invalidate()
    End Sub


    Private Sub cmdSpotlistColumns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSpotlistColumns.Click
        Dim i As Integer
        Dim j As Integer
        Dim FoundIt As Boolean
        Dim TmpNode As Windows.Forms.TreeNode
        Dim Chan As String
        Dim BT As String
        Dim TmpCol As Windows.Forms.DataGridViewColumn

        If grdSpotlist.RowCount = 0 Then
            Exit Sub
        End If

        Chan = cmbChannel.Text.Substring(0, InStr(cmbChannel.Text, " ") - 1)
        BT = Mid(cmbChannel.Text, InStr(cmbChannel.Text, " ") + 1)

        Dim Columns() As String = {"Date", "Time", "Channel", "Program", "Week", "Weekday", "Gross Price", "Net Price", "Filmcode", "Film", "Film dscr", "Position", "Est", "Est (Buying)", "Chan Est", "Gross CPP", "CPP (Main)", "CPP 30"" (Main)", "CPP (Chn Est)", "Quality", "Remarks", "Notes", "Added value", "Bid"}

        frmColumns.tvwChosen.Nodes.Clear()
        TmpCol = grdSpotlist.Columns.GetFirstColumn(Windows.Forms.DataGridViewElementStates.None)
        While Not TmpCol Is Nothing
            If TmpCol.Visible Then
                frmColumns.tvwChosen.Nodes.Add(CreateGUID, TmpCol.HeaderText)
            End If
            TmpCol = grdSpotlist.Columns.GetNextColumn(TmpCol, Windows.Forms.DataGridViewElementStates.None, Windows.Forms.DataGridViewElementStates.None)
        End While
        frmColumns.tvwAvailable.Nodes.Clear()
        For j = 0 To 23
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
        SortSpotlistColumn = Nothing
        If frmColumns.ShowDialog = Windows.Forms.DialogResult.OK Then
            TrinitySettings.SpotlistColumnCount = frmColumns.tvwChosen.Nodes.Count
            TmpNode = frmColumns.tvwChosen.Nodes(1)
            While Not TmpNode.PrevNode Is Nothing
                TmpNode = TmpNode.PrevNode
            End While
            i = 1
            While Not TmpNode Is Nothing
                TrinitySettings.SpotlistColumn(i) = TmpNode.Text
                i = i + 1
                TmpNode = TmpNode.NextNode
            End While
            UpdateSpotlist()
        End If

    End Sub

    Public Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click
        Dim _list As New List(Of Trinity.cBookingType)
        For Each _bt As Trinity.cBookingType In cmbChannel.Items
            _list.Add(_bt)
        Next

        Dim frmPrint As New frmPrintBooking(_list)

        frmPrint.ShowDialog()
    End Sub

    Private Sub cmdUpdatePrices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdatePrices.Click
        Saved = False
        DateIndex = New Dictionary(Of Date, Single)
        Dim TmpBT As Trinity.cBookingType = cmbChannel.SelectedItem

        'updates the gross price of the booked spots
        Campaign.BookedSpots.crossCheckSpotsWithDatabase(True)



        'update the net price of the booked spots
        Dim oldPrice As Single = 0
        If grdSpotlist.SelectedRows.Count < 2 Then
            For Each tmprow As DataGridViewRow In grdSpotlist.SelectedRows
                Dim s As String = tmprow.Tag
                For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                    If tmpSpot.ID = s Then
                        oldPrice = tmpSpot.NetPrice
                        tmpSpot.NetPrice = tmpSpot.GrossPrice30 * (tmpSpot.Film.Index / 100) * (1 - tmpSpot.Bookingtype.BuyingTarget.Discount) * (TmpBT.Indexes.GetIndexForDate(tmpSpot.AirDate, Trinity.cIndex.IndexOnEnum.eNetCPP) / 100)
                        If oldPrice <> tmpSpot.NetPrice Then
                            'Stop
                        End If
                    End If
                Next
            Next
        Else
            For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                oldPrice = TmpSpot.NetPrice
                TmpSpot.NetPrice = TmpSpot.GrossPrice30 * (TmpSpot.Film.Index / 100) * (1 - TmpSpot.Bookingtype.BuyingTarget.Discount) * (TmpBT.Indexes.GetIndexForDate(TmpSpot.AirDate, Trinity.cIndex.IndexOnEnum.eNetCPP) / 100)
                If oldPrice <> TmpSpot.NetPrice Then
                    'Stop
                End If
            Next
        End If

        grdSpotlist.Invalidate()
        UpdatePlannedTRP()
        UpdateBookedTRP()
        UpdateLeftToBook()
        UpdateFilm()
        UpdateSpotlist()
        UpdateDaypart()
        UpdatePrimePeak()
    End Sub

    Private Sub cmdRFEst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRFEst.Click
        Saved = False
        frmRFEst.ShowDialog()
        If Campaign.RFEstimation.ReferencePeriods.Count > 0 Then
            cmdRFEst.Image = imgOn.Image
            cmdSolus.Enabled = True
            cmbSolusFreq.Enabled = True
        Else
            cmdRFEst.Image = imgOff.Image
            cmdSolus.Enabled = False
            cmbSolusFreq.Enabled = False
        End If
        UpdateRF()
    End Sub

    Private Sub cmdSolus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSolus.Click
        Dim ID As String
        Dim TmpEI As Trinity.cExtendedInfo
        Dim TmpRow As Windows.Forms.DataGridViewRow
        Dim RowCount As Integer

        lblStatus.Visible = True
        For Each TmpRow In grdSchedule.SelectedRows
            lblStatus.Text = "Estimating... " & Format(RowCount / grdSchedule.SelectedRows.Count, "0%")
            System.Windows.Forms.Application.DoEvents()
            ID = TmpRow.Tag
            If ID <> "" Then
                TmpEI = Campaign.ExtendedInfos(ID)
                TmpEI.Solus = Campaign.RFEstimation.Solus(TmpEI.AirDate, TmpEI.MaM, TmpEI.Channel, cmbSolusFreq.SelectedIndex + 1)
                TmpEI.SolusFirst = Campaign.RFEstimation.Solus(TmpEI.AirDate, TmpEI.MaM, TmpEI.Channel, cmbSolusFreq.SelectedIndex + 1, -1)
                TmpEI.EstimatedReachRating = Campaign.RFEstimation.TRP(TmpEI.AirDate, TmpEI.MaM, TmpEI.Channel)
            End If
            RowCount += 1
        Next
        lblStatus.Text = ""
        UpdateSchedule(False, False)
    End Sub


    Private Sub cmdFilm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFilm.Click
        Saved = False
        Dim TmpFilm As Trinity.cFilm
        Dim IsChecked As Boolean
        Dim Chan As String = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        Dim BT As String = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Name

        Dim mnuFilm As New Windows.Forms.ContextMenuStrip

        If Not Campaign.RootCampaign Is Nothing Then Exit Sub
        If grdSpotlist.SelectedRows.Count = 0 Then
            System.Windows.Forms.MessageBox.Show("No spot selected.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf grdSpotlist.SelectedRows.Count = 1 And grdSpotlist.SelectedRows(0).Tag = Nothing Then
            Exit Sub
        End If


        For Each TmpFilm In Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films

            If grdSpotlist.SelectedRows.Item(0).Tag Is Nothing OrElse Campaign.BookedSpots(grdSpotlist.SelectedRows.Item(0).Tag).Film Is Nothing Then
                IsChecked = False
            Else
                IsChecked = (TmpFilm.Name = Campaign.BookedSpots(grdSpotlist.SelectedRows.Item(0).Tag).Film.Name)
            End If

            Dim TmpFilmItem As New Windows.Forms.ToolStripMenuItem
            TmpFilmItem.Text = TmpFilm.Name
            TmpFilmItem.Checked = IsChecked
            AddHandler TmpFilmItem.Click, AddressOf SwitchFilms
            mnuFilm.Items.Add(TmpFilmItem)
        Next
        mnuFilm.Show(stpSpotlist, cmdFilm.Bounds.Left, cmdFilm.Bounds.Height)
    End Sub

    Private Sub SwitchFilms(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Chan As String = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        Dim OldIndex As Single
        Dim OldIndexes As New Dictionary(Of String, Single)
        Dim TmpRow As Windows.Forms.DataGridViewRow
        Dim bolOtherChannels As Boolean = False
        Dim Break As Boolean = False

        If cmbChannel.SelectedItem.Weeks(1).Films(sender.text).Filmcode = "" Then Break = True



        For Each TmpRow In grdSpotlist.SelectedRows
            If Not TmpRow.Tag = Nothing Then
                If Campaign.BookedSpots(TmpRow.Tag).Filmcode = "" Then
                    Campaign.BookedSpots(TmpRow.Tag).Filmcode = "Temp"
                End If
                'Break = True

            End If
        Next

        If Break Then
            MessageBox.Show("Films need to have a filmcode for this operation to work. Visit Setup-> Films and give them names please.")
            Exit Sub
        End If

        For Each TmpRow In grdSpotlist.SelectedRows
            If Not Campaign.BookedSpots(TmpRow.Tag).Locked Then
                'you cant change the AV on a "greyed" item.
                If Not Chan = Campaign.BookedSpots(TmpRow.Tag).Channel.ChannelName Then
                    bolOtherChannels = True
                Else
                    If Campaign.BookedSpots(TmpRow.Tag).Film Is Nothing Then
                        If OldIndexes.ContainsKey(Campaign.BookedSpots(TmpRow.Tag).Filmcode) Then
                            OldIndex = OldIndexes(Campaign.BookedSpots(TmpRow.Tag).Filmcode)
                        Else
                            OldIndex = InputBox("The film for this spot is no longer in the campaign." & vbCrLf & "What was the index of the film " & Campaign.BookedSpots(TmpRow.Tag).Filmcode & "?", "T R I N I T Y")
                            OldIndexes.Add(Campaign.BookedSpots(TmpRow.Tag).Filmcode, OldIndex)
                        End If
                    Else
                        OldIndex = Campaign.BookedSpots(TmpRow.Tag).Film.Index
                    End If
                    'SwitchFilm(TmpRow.Tag, sender.text, OldIndex) gamla raden
                    SwitchFilm(Campaign.BookedSpots(TmpRow.Tag), sender.text, OldIndex)
                End If
            End If
        Next
        UpdateBookedTRP()
        UpdateLeftToBook()
        UpdateFilm()
        grdFilms.Invalidate()
        grdSpotlist.Invalidate()
        grdDayparts.Invalidate()
        If bolOtherChannels Then
            MsgBox("Spots from several channels was marked but only spots in " + Chan + " was changed", MsgBoxStyle.Information, "Spots ignored")
        End If
    End Sub

    Sub SwitchFilm(ByVal Spot As Trinity.cBookedSpot, ByVal NewFilm As String, ByVal OldIndex As Single)
        If grdSpotlist.Columns.Contains("colSpotlistGross Price") Then
            grdSpotlist.Columns("colSpotlistGross Price").Tag -= Spot.GrossPrice * Spot.AddedValueIndex(False)
        End If
        If grdSpotlist.Columns.Contains("colSpotlistNet Price") Then
            grdSpotlist.Columns("colSpotlistNet Price").Tag -= Spot.NetPrice * Spot.AddedValueIndex(True)
        End If
        Spot.Filmcode = Spot.week.Films(NewFilm).Filmcode
        Spot.NetPrice = (Spot.NetPrice / (OldIndex / 100)) * (Spot.Film.Index / 100)
        Spot.GrossPrice = (Spot.GrossPrice / (OldIndex / 100)) * (Spot.Film.Index / 100)
        If grdSpotlist.Columns.Contains("colSpotlistGross Price") Then
            grdSpotlist.Columns("colSpotlistGross Price").Tag += Spot.GrossPrice * Spot.AddedValueIndex(False)
        End If
        If grdSpotlist.Columns.Contains("colSpotlistNet Price") Then
            grdSpotlist.Columns("colSpotlistNet Price").Tag += Spot.NetPrice * Spot.AddedValueIndex(True)
        End If
    End Sub

    Private Sub cmdSpotlistAV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSpotlistAV.Click


        Saved = False
        Dim Chan As String = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        Dim BT As String = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Name

        If Not grdSpotlist.SelectedRows(0).Tag = Nothing Then
            'you cant change the AV on a "greyed" item.
            If Not Chan = Campaign.BookedSpots(grdSpotlist.SelectedRows(0).Tag).Channel.ChannelName Then
                MsgBox("You cant change added values on a spot in a channel that is not currently bookable", MsgBoxStyle.Critical, "Erronous action")
                Exit Sub
            End If
        End If

        Dim TmpAV As Trinity.cAddedValue

        Dim mnuAV As New Windows.Forms.ContextMenuStrip

        If Not (grdSpotlist.SelectedRows.Count = 1 And grdSpotlist.SelectedRows(0).Tag = Nothing) Then

            For Each TmpAV In Campaign.Channels(Chan).BookingTypes(BT).AddedValues
                If Not TmpAV.ShowIn = Trinity.cAddedValue.ShowInEnum.siAllocate Then

                    Dim mnuAVItem As Windows.Forms.ToolStripMenuItem = mnuAV.Items.Add(TmpAV.Name)

                    mnuAVItem.Checked = False

                    For Each tmpRow As DataGridViewRow In grdSpotlist.SelectedRows
                        If Not grdSpotlist.SelectedRows(0).Tag = Nothing Then
                            If Campaign.BookedSpots(grdSpotlist.SelectedRows.Item(0).Tag).AddedValues.ContainsKey(TmpAV.ID) Then mnuAVItem.Checked = True
                        End If
                        'mnuAVItem.Checked = Campaign.BookedSpots(grdSpotlist.SelectedRows.Item(0).Tag).AddedValues.ContainsKey(TmpAV.ID)
                    Next

                    mnuAVItem.Tag = TmpAV
                    AddHandler mnuAVItem.Click, AddressOf SwitchAV
                End If
            Next
        End If
        mnuAV.Show(stpSpotlist, cmdSpotlistAV.Bounds.Left, cmdSpotlistAV.Bounds.Height)

    End Sub

    Private Sub SwitchAV(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim TmpRow As Windows.Forms.DataGridViewRow
        Dim TmpAV As Trinity.cAddedValue

        Dim ChanBT As String = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).ToString

        For Each TmpRow In grdSpotlist.SelectedRows
            If Not TmpRow.Tag = Nothing Then
                If Campaign.BookedSpots(TmpRow.Tag).Bookingtype.ToString = ChanBT Then
                    If Not Campaign.BookedSpots(TmpRow.Tag).Locked Then
                        TmpAV = sender.tag
                        If sender.checked Then
                            If Campaign.BookedSpots(TmpRow.Tag).AddedValues.ContainsKey(TmpAV.ID) Then
                                If grdSpotlist.Columns.Contains("colSpotlistGross Price") Then
                                    grdSpotlist.Columns("colSpotlistGross Price").Tag -= Campaign.BookedSpots(TmpRow.Tag).GrossPrice * Campaign.BookedSpots(TmpRow.Tag).AddedValueIndex(False)
                                End If
                                If grdSpotlist.Columns.Contains("colSpotlistNet Price") Then
                                    grdSpotlist.Columns("colSpotlistNet Price").Tag -= Campaign.BookedSpots(TmpRow.Tag).NetPrice * Campaign.BookedSpots(TmpRow.Tag).AddedValueIndex(True)
                                End If
                                Campaign.BookedSpots(TmpRow.Tag).AddedValues.Remove(TmpAV.ID)
                                If grdSpotlist.Columns.Contains("colSpotlistGross Price") Then
                                    grdSpotlist.Columns("colSpotlistGross Price").Tag += Campaign.BookedSpots(TmpRow.Tag).GrossPrice * Campaign.BookedSpots(TmpRow.Tag).AddedValueIndex(False)
                                End If
                                If grdSpotlist.Columns.Contains("colSpotlistNet Price") Then
                                    grdSpotlist.Columns("colSpotlistNet Price").Tag += Campaign.BookedSpots(TmpRow.Tag).NetPrice * Campaign.BookedSpots(TmpRow.Tag).AddedValueIndex(True)
                                End If
                            End If
                        Else
                            If Not Campaign.BookedSpots(TmpRow.Tag).AddedValues.ContainsKey(TmpAV.ID) Then
                                If Not Campaign.BookedSpots(TmpRow.Tag).IsLocal OrElse Windows.Forms.MessageBox.Show("This break is a local break:" & vbCrLf & vbCrLf & Campaign.BookedSpots(TmpRow.Tag).Programme & vbCrLf & vbCrLf & "Are you sure you want to add an Added value to that?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                    If grdSpotlist.Columns.Contains("colSpotlistGross Price") Then
                                        grdSpotlist.Columns("colSpotlistGross Price").Tag -= Campaign.BookedSpots(TmpRow.Tag).GrossPrice * Campaign.BookedSpots(TmpRow.Tag).AddedValueIndex(False)
                                    End If
                                    If grdSpotlist.Columns.Contains("colSpotlistNet Price") Then
                                        grdSpotlist.Columns("colSpotlistNet Price").Tag -= Campaign.BookedSpots(TmpRow.Tag).NetPrice * Campaign.BookedSpots(TmpRow.Tag).AddedValueIndex(True)
                                    End If
                                    If TmpAV.IndexGross > 0 And TmpAV.IndexNet > 0 Then
                                        Campaign.BookedSpots(TmpRow.Tag).AddedValues.Add(TmpAV.ID, TmpAV)
                                    Else
                                        Dim Idx As Single
                                        TmpAV = New Trinity.cAddedValue(Campaign, New Collection)
                                        TmpAV.ID = DirectCast(sender.tag, Trinity.cAddedValue).ID
                                        TmpAV.Name = DirectCast(sender.tag, Trinity.cAddedValue).Name
                                        Idx = InputBox("The Added value you have chosen have a variable index." & vbCrLf & "What index do you want to add to the spot?", "T R I N I T Y", (-Campaign.MultiplyAddedValues) * 100)
                                        TmpAV.IndexNet = Idx
                                        TmpAV.IndexGross = Idx
                                        Campaign.BookedSpots(TmpRow.Tag).AddedValues.Add(TmpAV.ID, TmpAV)
                                    End If
                                    If grdSpotlist.Columns.Contains("colSpotlistGross Price") Then
                                        grdSpotlist.Columns("colSpotlistGross Price").Tag += Campaign.BookedSpots(TmpRow.Tag).GrossPrice * Campaign.BookedSpots(TmpRow.Tag).AddedValueIndex(False)
                                    End If
                                    If grdSpotlist.Columns.Contains("colSpotlistNet Price") Then
                                        grdSpotlist.Columns("colSpotlistNet Price").Tag += Campaign.BookedSpots(TmpRow.Tag).NetPrice * Campaign.BookedSpots(TmpRow.Tag).AddedValueIndex(True)
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Next
        grdSpotlist.Invalidate()
        UpdateBookedTRP()
        UpdateLeftToBook()
    End Sub


    Private Sub cmbChannel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbChannel.SelectedIndexChanged

        Dim d As Integer
        Dim TmpBT As Trinity.cBookingType

        TmpBT = cmbChannel.SelectedItem

        eventsTable = Nothing
        DateIndex = New Dictionary(Of Date, Single)
        For d = Campaign.StartDate To Campaign.EndDate
            DateIndex.Add(Date.FromOADate(d), (TmpBT.Indexes.GetIndexForDate(Date.FromOADate(d), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100) * (TmpBT.BuyingTarget.GetCPPForDate(d)))
        Next

        grdDayparts.Rows.Clear()
        grdDayparts.Rows.Add(TmpBT.Dayparts.Count)
        grdDayparts.Height = grdDayparts.Rows.GetRowsHeight(Windows.Forms.DataGridViewElementStates.None) + grdDayparts.ColumnHeadersHeight + 3

        If comboboxRun Then
            UpdateSpotlist()
            UpdateSchedule(True, True)
            UpdatePlannedTRP()
            UpdateBookedTRP()
            UpdateLeftToBook()
            UpdateFilm()
            UpdateDaypart()
            UpdateRF()
            UpdateEstimatedTrend()
            UpdateInfoPanels("")
            UpdatePrimePeak()
        End If

        'grdSchedule.Dock = DockStyle.Bottom + DockStyle.Left + DockStyle.Right

    End Sub

    Private Sub grdSchedule_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles grdSchedule.ColumnWidthChanged
        Dim TmpCol As Windows.Forms.DataGridViewColumn
        Dim Count As Integer = 0
        If Not SkipIt Then
            For Each TmpCol In grdSchedule.Columns
                If TmpCol.Visible Then
                    Count = Count + 1
                    TrinitySettings.BookingColumnCount = Count
                    TrinitySettings.BookingColumn(TmpCol.DisplayIndex) = TmpCol.Tag
                    TrinitySettings.BookingColumnWidth(TmpCol.DisplayIndex) = TmpCol.Width
                End If
            Next
        End If
    End Sub

    Private Sub grdSchedule_ColumnDisplayIndexChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles grdSchedule.ColumnDisplayIndexChanged
        Dim TmpCol As Windows.Forms.DataGridViewColumn
        Dim Count As Integer = 0
        If Not SkipIt Then
            For Each TmpCol In grdSchedule.Columns
                If TmpCol.Visible Then
                    Count = Count + 1
                    TrinitySettings.BookingColumnCount = Count
                    TrinitySettings.BookingColumn(TmpCol.DisplayIndex) = TmpCol.Tag
                    TrinitySettings.BookingColumnWidth(TmpCol.DisplayIndex) = TmpCol.Width
                End If
            Next
        End If
    End Sub

    Private Sub grdSpotlist_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs)
        Dim TmpCol As Windows.Forms.DataGridViewColumn = grdSpotlist.Columns(e.ColumnIndex)
        Dim TmpSpot As Trinity.cBookedSpot = Campaign.BookedSpots(grdSpotlist.Rows(e.RowIndex).Tag)
        Select Case TmpCol.HeaderText
            Case "Gross Price"
                grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(TmpSpot.GrossPrice * TmpSpot.AddedValueIndex(False), "0")
            Case "Net Price"
                grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format((TmpSpot.NetPrice * TmpSpot.AddedValueIndex), "0")
        End Select

    End Sub

    Private Sub grdSpotlist_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Dim Chan As String = cmbChannel.Text.Substring(0, InStr(cmbChannel.Text, " ") - 1)
        Dim BT As String = Mid(cmbChannel.Text, InStr(cmbChannel.Text, " ") + 1)
        Dim TmpCol As Windows.Forms.DataGridViewColumn = grdSpotlist.Columns(e.ColumnIndex)
        Dim TmpSpot As Trinity.cBookedSpot = Campaign.BookedSpots(grdSpotlist.Rows(e.RowIndex).Tag)

        Select Case TmpCol.HeaderText
            Case "Gross Price"
                TmpSpot.GrossPrice = grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value / TmpSpot.AddedValueIndex(False)
                grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(TmpSpot.GrossPrice * TmpSpot.AddedValueIndex(False), "##,##0 kr")
            Case "Net Price"
                TmpSpot.NetPrice = grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value / TmpSpot.AddedValueIndex
                grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format((TmpSpot.NetPrice * TmpSpot.AddedValueIndex), "##,##0 kr")
            Case "Chan Est"
                grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString.Replace(".", ",")
                TmpSpot.ChannelEstimate = grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(TmpSpot.ChannelEstimate, "N1")
            Case "Notes"
                TmpSpot.Comments = grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            Case "Est"
                grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString.Replace(".", ",")
                TmpSpot.MyEstimate = grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(TmpSpot.MyEstimate, "N1")
            Case "Est (" & Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.TargetName & ")"
                grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString.Replace(".", ",")
                TmpSpot.MyEstimateBuyTarget = grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Format(TmpSpot.MyEstimateBuyTarget, "N1")
        End Select
        UpdateFilm()
        UpdateDaypart()
        UpdatePlannedTRP()
        UpdateBookedTRP()
        UpdateLeftToBook()
    End Sub

    Private Sub grdSpotlist_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles grdSpotlist.ColumnWidthChanged
        Dim TmpCol As Windows.Forms.DataGridViewColumn
        Dim Count As Integer = 0
        If Not SkipIt Then
            For Each TmpCol In grdSpotlist.Columns
                If TmpCol.Visible Then
                    Count = Count + 1
                    TrinitySettings.SpotlistColumnCount = Count
                    TrinitySettings.SpotlistColumn(TmpCol.DisplayIndex - 1) = TmpCol.HeaderText
                    TrinitySettings.SpotlistColumnWidth(TmpCol.DisplayIndex - 1) = TmpCol.Width
                End If
            Next
        End If
    End Sub

    Private Sub grdSpotlist_ColumnDisplayIndexChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles grdSpotlist.ColumnDisplayIndexChanged
        Dim TmpCol As Windows.Forms.DataGridViewColumn
        Dim Count As Integer = 0
        For Each TmpCol In grdSpotlist.Columns
            If TmpCol.Visible Then
                Count = Count + 1
                TrinitySettings.SpotlistColumnCount = Count
                TrinitySettings.SpotlistColumn(TmpCol.DisplayIndex - 1) = TmpCol.HeaderText
                TrinitySettings.SpotlistColumnWidth(TmpCol.DisplayIndex - 1) = TmpCol.Width
            End If
        Next
        grdSpotlist.Invalidate()
    End Sub

    Private Sub sptBooking_SplitterMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles sptBooking.SplitterMoved
        If Me.Width - sptBooking.SplitterDistance < 0 Then Exit Sub
        TrinitySettings.SummaryWidth = Me.Width - sptBooking.SplitterDistance
        'Temporary solution
        For Each TmpGrpBox As Windows.Forms.GroupBox In sptBooking.Panel2.Controls
            TmpGrpBox.Width = sptBooking.Panel2.Width - TmpGrpBox.Left * 3
        Next
    End Sub

    Private Sub grdSpotlist_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSpotlist.KeyUp
        If e.KeyCode = Windows.Forms.Keys.Delete Then
            Dim TmpRow As Windows.Forms.DataGridViewRow

            If Not Campaign.RootCampaign Is Nothing Then Exit Sub

            If grdSpotlist.SelectedRows.Count = 0 Then
                System.Windows.Forms.MessageBox.Show("No spot selected.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            For Each TmpRow In grdSpotlist.SelectedRows
                If Campaign.ExtendedInfos.Exists(Campaign.BookedSpots(TmpRow.Tag).DatabaseID) Then
                    GfxSchedule2.updateGraphics(Campaign.ExtendedInfos(Campaign.BookedSpots(TmpRow.Tag).DatabaseID).AirDate, Campaign.ExtendedInfos(Campaign.BookedSpots(TmpRow.Tag).DatabaseID).MaM, Campaign.ExtendedInfos(Campaign.BookedSpots(TmpRow.Tag).DatabaseID).ProgAfter)
                End If
                Campaign.BookedSpots.Remove(TmpRow.Tag)
            Next
            Campaign.RFEstimation.Calculate()
            UpdateSchedule(False, False)
            UpdateBookedTRP()
            UpdateLeftToBook()
            UpdateSpotlist()
            UpdateFilm()
            UpdateDaypart()
            UpdateRF()
            UpdateEstimatedTrend()

            'update budget filtering if available
            updateFilterBudget()
            deleteSpotsOnBudget()
        End If
    End Sub

    Private Sub cmbDatabase_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDatabase.SelectedIndexChanged
        If comboboxRun Then
            If cmbDatabase.SelectedIndex = 1 Then
                ReadAvail()
            End If
            UpdateSpotlist()
            UpdateSchedule(True, True)
            UpdatePlannedTRP()
            UpdateBookedTRP()
            UpdateLeftToBook()
            UpdateFilm()
            UpdateDaypart()
            UpdateRF()
            UpdateEstimatedTrend()

            UpdateInfoPanels("")
        End If
    End Sub

    Private Class K2Spot
        Implements IComparable(Of K2Spot)

        Public Channel As String
        Public [Date] As DateTime
        Public MaM As Integer 
        Public Duration As Byte
        Public ProgBefore As String
        Public ProgAfter As String
        Public Estimate As Single

        Public Function CompareTo(other As K2Spot) As Integer Implements IComparable(Of K2Spot).CompareTo
            If Me.Date < other.Date Then
                Return -1
            ElseIf Me.Date = other.Date Then
                If Me.MaM < other.MaM Then
                    Return -1
                ElseIf Me.MaM > other.MaM then
                    Return 1
                Else
                    Return 0
                End If
            else
                Return 1
            End If
        End Function
    End Class

    Private Sub ReadK2Spotlist() 
        Dim SpotList As New List(Of K2Spot)
        Dim Excel As CultureSafeExcel.Application
        Dim WB As CultureSafeExcel.Workbook

        Dim dlgOpen As New Windows.Forms.OpenFileDialog
        dlgOpen.DefaultExt = "*.xls"
        dlgOpen.FileName = "*.xls"
        dlgOpen.Filter = "Excel workbook|*.xls;*.xlsx"
        dlgOpen.Multiselect = False
        dlgOpen.Title = "Open K2 spotlist"

        Excel = New CultureSafeExcel.Application(False)

        Excel.DisplayAlerts = False
        Excel.ScreenUpdating = false

        If dlgOpen.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
            cmbDatabase.SelectedIndex = 0
            Return 
        End If
        WB = Excel.OpenWorkbook(Filename:=dlgOpen.FileName, CorruptLoad:=2)

        Dim pb As New frmProgress
        With WB.Sheets(1)
            Dim row As Integer = 1
            Dim column As Integer = 1

            'Find top row
            While .Cells(row, 1).Value <> "User Markup" AndAlso row < 20
                row += 1
            End While
            If row = 20 Then
                MessageBox.Show("Could not find headline row. Aborting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                WB.Close
                Excel.Quit
                Return
            End If

            'Find columns
            Dim ChannelColumn As Integer = -1
            Dim DateColumn As Integer = -1
            Dim TimeColumn As Integer = -1
            Dim DurColumn As Integer = -1
            Dim ProgBeforeColumn As Integer = -1
            Dim ProgAfterColumn As Integer = -1
            Dim TRPColumn As Integer = -1

            While .Cells(row, column).Value <> ""
                Select Case .Cells(row, column).Value
                    Case "Channel"
                        ChannelColumn = column
                    Case "Date"
                        DateColumn = column
                    Case "Time"
                        TimeColumn = column
                    Case "Dur sec"
                        DurColumn = column
                    Case "Prog Before"
                        ProgBeforeColumn = column
                    Case "Prog After"
                        ProgAfterColumn = column
                    Case "TRP " & Campaign.MainTarget.TargetNameNice
                        TRPColumn = column
                    Case Else

                End Select
                column += 1
            End While
            row += 1

            If ChannelColumn = -1 Then
                MessageBox.Show("Could not find column 'Channel'. Aborting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                WB.Close
                Excel.Quit
                Return 
            End If
            If DateColumn = -1 Then
                MessageBox.Show("Could not find column 'Date'. Aborting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                WB.Close
                Excel.Quit
                Return 
            End If
            If TimeColumn = -1 Then
                MessageBox.Show("Could not find column 'Time'. Aborting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                WB.Close
                Excel.Quit
                Return 
            End If
            If DurColumn = -1 Then
                MessageBox.Show("Could not find column 'Dur sec'. Aborting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                WB.Close
                Excel.Quit
                Return 
            End If
            If ProgBeforeColumn = -1 Then
                MessageBox.Show("Could not find column 'Prog Before'. Aborting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                WB.Close
                Excel.Quit
                Return 
            End If
            If ProgAfterColumn = -1 Then
                MessageBox.Show("Could not find column 'Prog After'. Aborting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                WB.Close
                Excel.Quit
                Return 
            End If
            If TRPColumn = -1 Then
                MessageBox.Show("Could not find column 'TRP " & Campaign.MainTarget.TargetNameNice & "'." & vbCrLf & vbCrLf & "Spot estimates will be 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            Dim firstDate As DateTime = DateTime.Parse(.Cells(row, DateColumn).Value.ToString())

            If DatePart(DateInterval.Weekday, firstDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) <> DatePart(DateInterval.Weekday, DateTime.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) Then
                For Each TmpChan As cChannel In Campaign.Channels 
                    For each TmpBT As cBookingType In TmpChan.BookingTypes 
                        TmpBT.Weeks(1).StartDate -= 1
                    Next
                Next
            End If

            While DatePart(DateInterval.Weekday, firstDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) <> DatePart(DateInterval.Weekday, DateTime.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                firstDate = firstDate.AddDays(-1)
            End While 

            Dim MaxRow As Long = .UsedRange.Rows.Count

            pb.Status = "Reading Excel file..."
            pb.MaxValue = MaxRow + 1
            pb.Show()

            'Read all spots
            While .Cells(row, ChannelColumn).Value <> ""
                Dim spot As New K2Spot
                spot.Channel = .Cells(row, ChannelColumn).Value.ToString()
                spot.Date = DateTime.FromOADate(Campaign.StartDate) + (DateTime.Parse(.Cells(row, DateColumn).Value.ToString()) - firstDate )
                spot.MaM = Int(.Cells(row, TimeColumn ).Value.ToString.Substring(0, 2)) * 60 + Int(.Cells(row, TimeColumn).Value.ToString.Substring(3, 2))
                spot.Duration = .Cells(row, DurColumn).Value
                spot.ProgBefore = .Cells(row, ProgBeforeColumn).Value.ToString()
                spot.ProgAfter = .Cells(row, ProgAfterColumn).Value.ToString()
                If TRPColumn > -1 Then
                    spot.Estimate = .Cells(row, TRPColumn).Value
                End If

                SpotList.Add(spot)

                pb.Progress = row
                row+=1
            End While
        End With
        WB.Close
        Excel.Quit

        For each TmpBT As cBookingType In cmbChannel.Items

            Dim Spots As IEnumerable(Of K2Spot) = SpotList.Where(Function(s) TmpBT.ParentChannel Is Trinity.Helper.Adedge2Channel(s.Channel))
            pb.Status = "Finding spots for " & TmpBT.ToString() & " 0 / " & Spots.Count
            pb.MaxValue = Spots.Count
            pb.Progress = 0

            Dim events As DataTable = GetEventsTable(TmpBT)

            Dim GetExtendedInfo As Func(Of Integer, cExtendedInfo) = Function(evt As Integer) As cExtendedInfo
                                    If evt >= events.Rows.Count Then
                                        Return Nothing
                                    End If
                                    Dim row As DataRow = events.Rows(evt)
                                    Dim TmpEI As cExtendedInfo
                                    Dim ID As String = row.Item("Channel") & row.Item("Date") & row.Item("StartMam")

                                    If Not Campaign.ExtendedInfos.Exists(ID) Then
                                    TmpEI = New cExtendedInfo(Campaign, row, TmpBT, NoEstimationTargetColumn)

                                    Campaign.ExtendedInfos.Add(TmpEI, ID)
                                    End If
                                    TmpEI = DirectCast(Campaign.ExtendedInfos.Item(ID), Trinity.cExtendedInfo)

                                    Return TmpEI
                                End function

            Spots = Spots.OrderBy(Of K2Spot)(Function(s) s)
            
            Dim e As Long = 0
            Dim lastDateChangeIndex As Long = 0
            For Each Spot As K2Spot In Spots

                pb.Status = "Finding spots for " & TmpBT.ToString() & pb.Progress & " / " & Spots.Count

                e=lastDateChangeIndex 

                Dim FirstBreakIndexForDate As Func(Of DateTime, cExtendedInfo)=Function(d As DateTime) As cExtendedInfo
                                               e = lastDateChangeIndex
                                               Dim tmpEI As cExtendedInfo = GetExtendedInfo(e)  
                                            
                                               While Not IsNothing(tmpEI) AndAlso tmpEI.AirDate < d.AddDays(-1)
                                                  e+=1
                                                  tmpEI = GetExtendedInfo(e)
                                               End While
                                               lastDateChangeIndex = e
                                               Return tmpEI
                                           End Function
                
                Dim ei As cExtendedInfo = FirstBreakIndexForDate(Spot.Date)
                Dim closestBreakDistance As Integer = Integer.MaxValue
                Dim closestBreakIndex As Long  = e
                Dim closestBreakHasSameProgAfter As Boolean = false
                Dim bestFound = False

                While Not IsNothing(ei) AndAlso ei.AirDate <= Spot.Date.AddDays(1) AndAlso Not bestFound
                    Dim MaM = ei.MaM 
                    'The closest break could belong to the date before or after so go through them but change start time accordingly
                    If ei.AirDate < Spot.Date Then
                        MaM-=24*60
                    ElseIf ei.AirDate > Spot.Date then
                        MaM+=24*60
                    End If

                    Dim sameProg = Trinity.Helper.ProgramHit(Spot.ProgAfter, ei.ProgAfter)
                    Dim distance As Integer = Math.Abs(MaM - Spot.MaM)
                    If distance < closestBreakDistance Then
                        If closestBreakHasSameProgAfter then
                            If closestBreakDistance > 30 AndAlso distance <10 Then
                                closestBreakDistance = distance
                                closestBreakHasSameProgAfter = sameProg
                                closestBreakIndex = e
                            End If
                        Else
                            closestBreakDistance = distance
                            closestBreakHasSameProgAfter = sameProg
                            closestBreakIndex = e
                        End If
                    ElseIf sameProg AndAlso Not closestBreakHasSameProgAfter then
                        If distance <=30 Then
                            closestBreakDistance = distance
                            closestBreakHasSameProgAfter = sameProg
                            closestBreakIndex = e
                        End If
                    End If

                    If closestBreakDistance < distance AndAlso distance > 30 Then
                        bestFound = True
                    Else
                        e+=1
                        ei = GetExtendedInfo(e)
                    End  If

                End While
                If bestFound then
                    Dim BestBreak As cExtendedInfo = GetExtendedInfo(closestBreakIndex)
                    Dim TmpFilm As cFilm = Nothing
                    For Each TmpFilm In TmpBT.Weeks(1).Films
                        If TmpFilm.FilmLength = Spot.Duration Then
                            Exit For
                        End If
                    Next
                    If Not IsNothing(TmpFilm) Then
                        Campaign.BookedSpots.Add(CreateGUID(), BestBreak.ID, BestBreak.Channel, BestBreak.AirDate, BestBreak.MaM, BestBreak.ProgAfter, BestBreak.ProgAfter, "", BestBreak.GrossPrice(TmpFilm), BestBreak.NetPrice(TmpFilm,TmpBT), BestBreak.ChannelEstimate, Spot.Estimate, BestBreak.EstimatedRatingBuyingTarget, cmbFilm.Text, TmpBT.Name, InStr(BestBreak.Remark, "L") > 0, InStr(BestBreak.Remark, "R") > 0, 0)
                    End if
                End if
                pb.Progress += 1
            Next
        Next
        pb.Close 
        Campaign.ExtendedInfos = New cWrapper()
    End Sub



    Private Sub ReadAvail()
        'On Error GoTo Error_Reading_avail

        AvailList = New List(Of Trinity.cExtendedInfo)
        Dim dlgOpen As New Windows.Forms.OpenFileDialog
        Dim Excel As CultureSafeExcel.Application
        Dim WB As CultureSafeExcel.Workbook
        Dim ID As String
        Dim TmpEI As Trinity.cExtendedInfo
        Dim TmpBT As Trinity.cBookingType = cmbChannel.SelectedItem

        If TmpBT Is Nothing Then
            MsgBox("You need to select a channel before you can read an avail.", MsgBoxStyle.Information, "T R I N I T Y")
            Exit Sub
        End If
        'holders for the columns
        Dim intDateCol As Integer
        Dim intTimeCol As Integer
        Dim intProgramCol As Integer
        Dim intEstCol As Integer
        Dim intPriceCol As Integer
        Dim c As Integer
        Dim r As Integer = 1

        dlgOpen.DefaultExt = "*.xls"
        dlgOpen.FileName = "*.xls"
        dlgOpen.Filter = "Excel workbook|*.xls;*.xlsx|Word document|*.doc"
        dlgOpen.Multiselect = False
        dlgOpen.Title = "Open channel schedule"

        Excel = New CultureSafeExcel.Application(False)

        Excel.DisplayAlerts = False
        Excel.ScreenUpdating = False
        If dlgOpen.ShowDialog = Windows.Forms.DialogResult.Cancel Then
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
            cmbDatabase.SelectedIndex = 0
            Exit Sub
        End If
        WB = Excel.OpenWorkbook(Filename:=dlgOpen.FileName, CorruptLoad:=2)

        With WB.Sheets(1)
            If InStr(.Cells(1, 1).Value, "breaks förenklad") > 0 Or InStr(.Cells(1, 1).Value, "Lediga breaks") > 0 Then

                'Read in a TV4 (se) avail


                Dim AirDate As Double
                Dim Mam As Integer
                Dim Chronology As Long

                'search for where the columns are
                While r < 100
                    'if we are at the headlines we exit
                    If .Cells(r, 1).Value = "Kanal" Then
                        Exit While
                    End If
                    r += 1
                End While

                c = 1
                While c < 20
                    'if we are at the Datum headline we exit
                    If .Cells(r, c).Value = "Datum" Then
                        intDateCol = c
                        Exit While
                    End If
                    c += 1
                End While

                c = 1
                While c < 20
                    'if we are at the Tid headline we exit
                    If .Cells(r, c).Value = "Tid" Then
                        intTimeCol = c
                        Exit While
                    End If
                    c += 1
                End While

                c = 1
                While c < 20
                    'if we are at the Program headline we exit
                    If .Cells(r, c).Value = "Program" Then
                        intProgramCol = c
                        Exit While
                    End If
                    c += 1
                End While

                c = 1
                While c < 20
                    'if we are at the Estimate headline we exit
                    If InStr(.Cells(r, c).Value, "TRP") Then
                        intEstCol = c
                        Exit While
                    End If
                    c += 1
                End While

                c = 1
                While c < 20
                    'if we are at the price headline we exit
                    If InStr(.Cells(r, c).Value, "CPP") Then
                        intPriceCol = c
                        Exit While
                    End If
                    c += 1
                End While

                r += 1
                While Not .Cells(r, 1).Value = ""
                    Try
                        'Dim readableDate As Date = CDate(WB.sheets(1).cells(r, intDateCol).value)
                        Try
                            AirDate = .Cells(r, intDateCol).Value
                        Catch ex As Exception
                            AirDate = CDate(.Cells(r, intDateCol).Value).ToOADate
                        End Try


                        Mam = Int(.Cells(r, intTimeCol).Value.ToString.Substring(0, 2)) * 60 + Int(.Cells(r, intTimeCol).Value.ToString.Substring(3, 2))

                        If Mam < 360 Then
                            AirDate = AirDate - 1 'CDate(.cells(r, intDateCol).value).AddDays(-1).ToOADate
                            Mam = Mam + 24 * 60
                        End If
                        If (.Cells(r, 1).Value.ToString.Contains("Plus") Or .Cells(r, 1).Value.ToString.Contains("+")) Then
                            ID = "TV4+" & AirDate & Mam
                            Chronology = AirDate + Mam
                        ElseIf (.Cells(r, 1).Value.ToString.Contains(“Sjuan”)) Then 'Added Sjuan 2018-05-24
                            ID = “Sjuan” & AirDate & Mam
                        Else
                            ID = "TV4" & AirDate & Mam
                            Chronology = AirDate + Mam
                        End If

                        'Added Sjuan 2018-05-24
                        If TmpBT.GetWeek(Date.FromOADate(AirDate)) IsNot Nothing Then
                            If Not Campaign.ExtendedInfos.Exists(ID) Then
                                TmpEI = New Trinity.cExtendedInfo(Campaign)
                                TmpEI.AirDate = Date.FromOADate(AirDate)
                                TmpEI.MaM = Mam
                                TmpEI.EstimationPeriod = "-4fw"
                                TmpEI.ProgAfter = .Cells(r, intProgramCol).Value
                                If (.Cells(r, 1).Value.ToString.Contains("Plus") Or .Cells(r, 1).Value.ToString.Contains("+")) Then
                                    TmpEI.Channel = "TV4+"
                                ElseIf (.Cells(r, 1).Value.ToString.Contains("Sjuan")) Then
                                    TmpEI.Channel = "Sjuan"
                                Else
                                    TmpEI.Channel = "TV4"
                                End If
                                TmpEI.Bookingtype = cmbChannel.SelectedItem.ToString
                                If intEstCol > 0 Then
                                    TmpEI.GrossPrice30(False) = .Cells(r, intEstCol).Value * .Cells(r, intPriceCol).Value
                                Else
                                    TmpEI.GrossPrice30(False) = 0
                                End If
                                If intEstCol > 0 Then
                                    TmpEI.ChannelEstimate = .Cells(r, intEstCol).Value
                                Else
                                    TmpEI.ChannelEstimate = 0
                                End If
                                TmpEI.Remark = ""
                                TmpEI.ID = ID
                                For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If TmpSpot.DatabaseID = ID Then
                                        TmpEI.IsBooked = True
                                        Exit For
                                    End If
                                Next

                                TmpEI.IsAvail = True
                                TmpEI.Chronological = Chronology
                                Campaign.ExtendedInfos.Add(TmpEI, ID)
                                'AvailList.Add(Campaign.ExtendedInfos(ID))
                            End If
                            TmpEI = DirectCast(Campaign.ExtendedInfos.Item(ID), Trinity.cExtendedInfo)
                            If otherBookedSpotsList.LastIndexOf(TmpEI.ID) > -1 Then
                                Campaign.ExtendedInfos(ID).IsBooked = True
                            End If
                            AvailList.Add(Campaign.ExtendedInfos(ID))
                        Else
                            'Stop
                        End If
                        r += 1
                    Catch ex As Exception
                        Windows.Forms.MessageBox.Show("Error on line " & r & ", quitting avail reading.", "TRINITY", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        WB.Close()
                        Excel.Quit()
                        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
                    End Try
                End While

            Else
                System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
                Windows.Forms.MessageBox.Show("The selected file is not an avail or the format is not recognized.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
                cmbDatabase.SelectedIndex = 0
                Exit Sub
            End If
        End With

        WB.Close()
        Excel.Quit()
        Exit Sub

    End Sub



    Private Sub gfxSchedule_ProgramDoubleClicked(ByVal sender As Object, ByVal e As ProgramEvent)  'gfxSchedule.ProgramDoubleClicked


        Trinity.Helper.WriteToLogFile("Start of frmBooking/grdSchedule_DblClick")

        If Not Campaign.RootCampaign Is Nothing Then Exit Sub

        Dim Chan As String = cmbChannel.Text.Substring(0, InStr(cmbChannel.Text, " ") - 1)
        Dim BT As String = Mid(cmbChannel.Text, InStr(cmbChannel.Text, " ") + 1)
        Dim TmpFilm As Trinity.cFilm = Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(cmbFilm.Text)

        Dim TmpEI As Trinity.cExtendedInfo = e.ExtendedInfo
        Dim SpotID As String = CreateGUID()
        Dim Bid As Single
        Dim Gross As Decimal

        If Campaign.Channels(Chan).UseBid Then
            Bid = InputBox("Set your bid:", "T R I N I T Y", 0)
            Gross = TmpEI.GrossPrice30(True, Bid)
        Else
            Gross = TmpEI.GrossPrice(TmpFilm)
        End If

        Campaign.BookedSpots.Add(SpotID, e.ExtendedInfo.ID, TmpEI.Channel, TmpEI.AirDate, TmpEI.MaM, TmpEI.ProgAfter, TmpEI.ProgAfter, "", TmpEI.GrossPrice(TmpFilm), TmpEI.NetPrice(TmpFilm, Campaign.Channels(Chan).BookingTypes(BT)), TmpEI.ChannelEstimate, TmpEI.EstimatedRating, TmpEI.EstimatedRatingBuyingTarget, cmbFilm.Text, BT, InStr(TmpEI.Remark, "L") > 0, InStr(TmpEI.Remark, "R") > 0, Bid)
        'AddToPivot(Campaign.BookedSpots(SpotID))
        For Each TmpWeek As Trinity.cWeek In Campaign.Channels(TmpEI.Channel).BookingTypes(BT).Weeks
            If Campaign.Channels(TmpEI.Channel).BookingTypes(BT).Weeks(TmpWeek.Name).StartDate <= TmpEI.AirDate.ToOADate Then
                If Campaign.Channels(TmpEI.Channel).BookingTypes(BT).Weeks(TmpWeek.Name).EndDate >= TmpEI.AirDate.ToOADate Then
                    Campaign.BookedSpots(SpotID).week = Campaign.Channels(TmpEI.Channel).BookingTypes(BT).Weeks(TmpWeek.Name)
                End If
            End If
        Next
        Campaign.BookedSpots(SpotID).RecentlyBooked = True
        Campaign.BookedSpots(SpotID).MostRecentlyBooked = True
        For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
            If TmpSpot.MostRecentlyBooked AndAlso TmpSpot.ID <> SpotID Then
                TmpSpot.MostRecentlyBooked = False
            End If
        Next
        Campaign.RFEstimation.Calculate()

        UpdateSpotlist()
        UpdateBookedTRP()
        UpdateLeftToBook()
        UpdateFilm()
        UpdateDaypart()
        UpdateRF()
        UpdateEstimatedTrend()

    End Sub

    Sub UpdateFilm()
        grdFilms.Invalidate()
    End Sub

    Sub UpdateDaypart()
        grdDayparts.Invalidate()
    End Sub

    Sub UpdatePrimePeak()
        If cmbChannel.SelectedIndex > -1 Then
            sngPeakTRP = 0
            Dim prime As Integer = 0
            'get the current bookingtype
            Dim TmpBT As Trinity.cBookingType = cmbChannel.SelectedItem

            'go through all rows in the grid (we use the grid since they are filtered and ready for use)
            For Each row As Windows.Forms.DataGridViewRow In grdSpotlist.Rows
                If row.Index = grdSpotlist.Rows.Count - 1 Then Exit For
                Dim TmpSpot As Trinity.cBookedSpot = Campaign.BookedSpots(row.Tag)
                If Not TmpSpot Is Nothing Then
                    If Not TmpSpot.otherCampaign Then
                        If TmpSpot.Bookingtype Is TmpBT Then
                            'check for a prime spot
                            If TmpSpot.MaM < 1411 AndAlso TmpSpot.MaM > 1079 Then
                                'check if it is a peak prime spot
                                If TmpSpot.MaM < 1321 AndAlso TmpSpot.MaM > 1199 Then
                                    sngPeakTRP += TmpSpot.MyEstimate
                                Else
                                    prime += TmpSpot.MyEstimate
                                End If
                            End If
                        End If
                    End If
                End If
            Next

            chtPrimePeak.Peak = sngPeakTRP
            chtPrimePeak.OffPeak = prime
        End If
    End Sub

    Sub UpdateGender(ByVal ID As String)
        Dim b As Integer
        Dim TRPMen As Decimal = 0
        Dim TRPWomen As Decimal = 0
        If Not ID Is Nothing AndAlso Campaign.ExtendedInfos.Exists(ID) AndAlso Campaign.ExtendedInfos(ID).EstimatedOnPeriod IsNot Nothing AndAlso Campaign.EstimationPeriods.Exists(Campaign.ExtendedInfos(ID).EstimatedOnPeriod) Then
            Dim TmpEI As Trinity.cExtendedInfo = Campaign.ExtendedInfos(ID)
            If Not TmpEI Is Nothing AndAlso Not TmpEI.BreakList Is Nothing Then
                For b = 1 To TmpEI.BreakList.Count
                    TRPMen += DirectCast(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod), Trinity.cPeriod).Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod).Adedge, "M3+"))
                    TRPWomen += DirectCast(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod), Trinity.cPeriod).Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod).Adedge, "W3+"))
                Next
            End If
            If (TRPMen + TRPWomen) > 0 Then
                chtGender.Men = TRPMen / (TRPMen + TRPWomen) * 100
                chtGender.Women = TRPWomen / (TRPMen + TRPWomen) * 100
            Else
                chtGender.Men = 0
                chtGender.Women = 0
            End If
        End If

    End Sub

    Private Sub grdFilms_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdFilms.CellFormatting
        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        Select Case e.ColumnIndex
            Case 1
                e.Value = Format(e.Value, "N1")
            Case 2
                e.Value = Format(e.Value, "N1")
            Case 3
                e.Value = Format(e.Value, "N1")
            Case 4
                e.Value = Format(e.Value, "N1")
            Case 5
                e.Value = Format(e.Value, "N0")
        End Select
    End Sub

    Private Sub grdFilms_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdFilms.CellValueNeeded
        'in case we have no channel selected
        If cmbChannel.SelectedIndex < 0 Then Exit Sub

        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        Dim TmpFilm As Trinity.cFilm = grdFilms.Rows(e.RowIndex).Tag

        If e.ColumnIndex = 0 Then
            e.Value = TmpFilm.Name
        ElseIf e.ColumnIndex = 1 Then
            Dim TmpBT As Trinity.cBookingType = cmbChannel.SelectedItem
            Dim TRP As Single = 0
            For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                TRP = TRP + TmpWeek.TRP * (TmpWeek.Films(TmpFilm.Name).Share / 100)
            Next
            e.Value = TRP
        ElseIf e.ColumnIndex = 2 Then
            Dim TmpBT As Trinity.cBookingType = cmbChannel.SelectedItem
            Dim TRP As Single = 0
            For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                For Each tmpFilm2 As Trinity.cFilm In TmpWeek.Films
                    TRP += TmpWeek.TRP * (TmpWeek.Films(tmpFilm2.Name).Share / 100)
                Next
            Next

            e.Value = (grdFilms.Rows(e.RowIndex).Cells(1).Value / TRP) * 100

        ElseIf e.ColumnIndex = 3 Then
            Dim TRP As Single = 0
            For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                If TmpSpot.Bookingtype Is cmbChannel.SelectedItem AndAlso Not TmpSpot.Film Is Nothing AndAlso TmpSpot.Film.Name = TmpFilm.Name Then
                    TRP = TRP + TmpSpot.MyEstimate
                End If
            Next
            e.Value = TRP

        ElseIf e.ColumnIndex = 4 Then
            Dim TRP As Single = 0
            For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                If TmpSpot.Bookingtype Is cmbChannel.SelectedItem AndAlso Not TmpSpot.Film Is Nothing Then
                    TRP = TRP + TmpSpot.MyEstimate
                End If
            Next

            e.Value = (grdFilms.Rows(e.RowIndex).Cells(3).Value / TRP) * 100

        ElseIf e.ColumnIndex = 5 Then
            Dim Budget As Single = 0
            For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                If TmpSpot.Bookingtype Is cmbChannel.SelectedItem AndAlso Not TmpSpot.Film Is Nothing AndAlso TmpSpot.Film.Name = TmpFilm.Name Then
                    Budget = Budget + (TmpSpot.NetPrice * TmpSpot.AddedValueIndex)
                End If
            Next
            e.Value = Budget
        End If
    End Sub

    Private Sub grdDayparts_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdDayparts.CellFormatting
        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        Select Case e.ColumnIndex
            Case 1
                e.Value = Format(e.Value, "N1")
            Case 2
                e.Value = Format(e.Value, "N1")
            Case 3
                e.Value = Format(e.Value, "N0")
        End Select
    End Sub

    Private Sub grdDayparts_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdDayparts.CellValueNeeded
        Dim TmpBT As Trinity.cBookingType = cmbChannel.SelectedItem
        'in case we have no channel selected
        If cmbChannel.SelectedIndex < 0 Then Exit Sub

        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub

        If e.ColumnIndex = 0 Then
            e.Value = TmpBT.Dayparts(e.RowIndex).Name
        ElseIf e.ColumnIndex = 1 Then
            Dim TRP As Single = 0
            For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                TRP = TRP + TmpWeek.TRP * (TmpBT.Dayparts(e.RowIndex).Share / 100)
            Next
            e.Value = TRP
        ElseIf e.ColumnIndex = 2 Then
            Dim TRP As Single = 0
            For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                If TmpSpot.Bookingtype Is cmbChannel.SelectedItem AndAlso TmpBT.Dayparts.GetDaypartIndexForMam(TmpSpot.MaM) = e.RowIndex Then
                    TRP = TRP + TmpSpot.MyEstimate
                End If
            Next
            e.Value = TRP
        ElseIf e.ColumnIndex = 3 Then
            Dim Budget As Single = 0
            For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                If TmpSpot.Bookingtype Is cmbChannel.SelectedItem AndAlso TmpBT.Dayparts.GetDaypartIndexForMam(TmpSpot.MaM) = e.RowIndex Then
                    Budget = Budget + (TmpSpot.NetPrice * TmpSpot.AddedValueIndex)
                End If
            Next
            e.Value = Budget
        End If
    End Sub

    'edit by KK Dec 2006
    'Private Sub PanesMenuClicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlannedToolStripMenuItem.CheckedChanged, BookedToolStripMenuItem.CheckedChanged, DaypartsToolStripMenuItem.CheckedChanged, DaypartsToolStripMenuItem.CheckedChanged, DetailsToolStripMenuItem.CheckedChanged, FilmsToolStripMenuItem.CheckedChanged, PositionInBreakToolStripMenuItem.CheckedChanged, ProgsInOtherChannelsToolStripMenuItem.CheckedChanged, ReachToolStripMenuItem.CheckedChanged, TargetProfileToolStripMenuItem.CheckedChanged, TrendToolStripMenuItem.CheckedChanged
    'ändrat tillbaka av Johan. Om man använder Clicked så raisar eventet innan checkedstatusen ändras, vilket ger fel i UpdatePanelPositions
    'Private Sub PanesMenuClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles mnuPanes.ItemClicked
    Private Sub PanesMenuClicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureToolStripMenuItem.CheckedChanged, PlannedToolStripMenuItem.CheckedChanged, BookedToolStripMenuItem.CheckedChanged, DaypartsToolStripMenuItem.CheckedChanged, DaypartsToolStripMenuItem.CheckedChanged, DetailsToolStripMenuItem.CheckedChanged, FilmsToolStripMenuItem.CheckedChanged, PositionInBreakToolStripMenuItem.CheckedChanged, ProgsInOtherChannelsToolStripMenuItem.CheckedChanged, ReachToolStripMenuItem.CheckedChanged, TargetProfileToolStripMenuItem.CheckedChanged, TrendToolStripMenuItem.CheckedChanged, EstimatedTargetProfileToolStripMenuItem.CheckedChanged
        UpdatePlannedTRP()
        UpdateBookedTRP()
        UpdateLeftToBook()
        UpdateFilm()
        UpdateDaypart()
        UpdateRF()
        UpdateInfoPanels("")
        UpdateFilm()
        UpdateEstimatedTrend()
        UpdatePanelPositions()
        UpdatePrimePeak()
    End Sub

    Private Sub txtCustomPeriod_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'Campaign.EstimationPeriods.RemoveAll()
    End Sub

    Private Sub cmbFilm_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFilm.SelectedIndexChanged

        'in case we have no channel selected
        If cmbChannel.SelectedIndex < 0 Then
            Me.Cursor = Windows.Forms.Cursors.Default
            Exit Sub
        End If

        Dim Chan As String = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        Dim BT As String = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Name
        Dim d As Integer

        eventsTable = Nothing
        DateIndex = New Dictionary(Of Date, Single)
        For d = Campaign.StartDate To Campaign.EndDate
            DateIndex.Add(Date.FromOADate(d), (Campaign.Channels(Chan).BookingTypes(BT).Indexes.GetIndexForDate(Date.FromOADate(d), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100) * (Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.GetCPPForDate(d)))
        Next
        If comboboxRun Then
            grdSpotlist.Invalidate()
            UpdateSchedule(False, False)
            UpdatePlannedTRP()
            UpdateBookedTRP()
            UpdateLeftToBook()
            UpdateFilm()
            UpdateDaypart()
            UpdateRF()
            UpdateEstimatedTrend()
            UpdateInfoPanels("")
            UpdatePrimePeak()
        End If
    End Sub

    Private Sub tabSchedule_SelectedIndexChanging(ByVal sender As Object, ByVal e As TabSelectionChangingArgs) Handles TabSchedule.SelectedIndexChanging
        If Not TabSchedule.TabPages(e.NewTabIndex).Enabled Then
            e.Cancel = True
        End If

        If e.NewTabIndex = 1 Then
            cmbWeeks.Items.Clear()
            For Each week As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                cmbWeeks.Items.Add(week.Name)
            Next
        End If
    End Sub

    Private Sub cmdOptimize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOptimize.Click
        Saved = False
        Dim PercentBig As Single = 1
        Dim Chan As String = cmbChannel.Text.Substring(0, InStr(cmbChannel.Text, " ") - 1)
        Dim BT As String = Mid(cmbChannel.Text, InStr(cmbChannel.Text, " ") + 1)
        Dim OldIndex As Single
        Dim OldIndexes As New Dictionary(Of String, Single)
        Dim i As Integer

        Dim IndividualCount As Integer = 50
        Dim Individual As Integer
        Dim BestSoFar As cIndividual
        Dim FilmBuckets As New Collection ' Create a collection of buckets for the films

        For Each TmpWeek As Trinity.cWeek In Campaign.Channels(Chan).BookingTypes(BT).Weeks 'Optimize each week
            If BookingFilter.Data("ActualWeek", TmpWeek.Name) Then
                Dim Individuals As New SortedList(Of cIndividual, cIndividual)(New IndComparer) 'Create list to hold Individuals for the GA
                Dim Spots As New SortedList(Of Trinity.cBookedSpot, Trinity.cBookedSpot)(New MyEstComparer) 'Create list to holds the spots in this week
                Dim TotalTRP As Single = 0

                For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots 'Iterate through all booked spots
                    If TmpSpot.week Is TmpWeek Then ' If the spot belongs to this week then save it in the Spots list
                        If TmpSpot.MyEstimate > 0 Then
                            TotalTRP += TmpSpot.MyEstimate
                        Else
                            TotalTRP += TmpSpot.ChannelEstimate
                        End If
                        If TmpSpot.Film Is Nothing Then
                            If Not OldIndexes.ContainsKey(TmpSpot.Filmcode) Then
                                OldIndex = InputBox("The film for a spot is no longer in the campaign." & vbCrLf & "What was the index of the film " & TmpSpot.Filmcode & "?", "T R I N I T Y")
                                OldIndexes.Add(TmpSpot.Filmcode, OldIndex)
                            End If
                        End If
                        Spots.Add(TmpSpot, TmpSpot) 'Save it
                    End If
                Next

                For Individual = 1 To IndividualCount ' Create a population of descent individuals for the GA
                    Dim TotalBigTRP As Single = 0
                    Dim BigBucket As New SortedList(Of Trinity.cBookedSpot, Trinity.cBookedSpot)(New MyEstComparer) ' Create a bucket with all spots
                    Dim Seed As Single
                    Dim TmpBucket As FilmBucket
                    FilmBuckets.Clear()

                    Randomize()

                    For i = 0 To Int(Spots.Count * PercentBig - 1)
                        BigBucket.Add(Spots.Keys(i), Spots.Values(i)) 'Add all the spots to the bucket
                        If Spots.Values(i).MyEstimate > 0 Then
                            TotalBigTRP += Spots.Values(i).MyEstimate
                        Else
                            TotalBigTRP += Spots.Values(i).ChannelEstimate
                        End If
                    Next

                    For Each TmpFilm As Trinity.cFilm In TmpWeek.Films
                        FilmBuckets.Add(New FilmBucket, TmpFilm.Name) ' Create a filmbucket for each film. The FilmBucket will be filled with spots for that film from the BigBucket
                        FilmBuckets(TmpFilm.Name).TRPToPick = TotalBigTRP * (TmpFilm.Share / 100) ' How many TRP should be in each bucket?
                        FilmBuckets(TmpFilm.Name).film = TmpFilm
                        FilmBuckets(TmpFilm.Name).index = FilmBuckets.Count
                    Next

                    While BigBucket.Count > 0 ' Loop until the BigBucket is emptied
                        Seed = Int(Rnd() * TotalBigTRP) ' Randomly pick a place along the TRP scale 
                        i = 1
                        If BigBucket.Values(0).Locked Then
                            TmpBucket = FilmBuckets(BigBucket.Values(0).Film.Name)
                        Else
                            TmpBucket = FilmBuckets(i)
                            While Seed > TmpBucket.TRPToPick - TmpBucket.PickedTRP 'Find out which bucket to add the spot to
                                i += 1
                                Seed -= TmpBucket.TRPToPick - TmpBucket.PickedTRP
                                If i <= FilmBuckets.Count Then
                                    TmpBucket = FilmBuckets(i)
                                Else
                                    TmpBucket = FilmBuckets(Int(Rnd() * FilmBuckets.Count) + 1)
                                    Exit While
                                End If
                            End While
                        End If
                        If BigBucket.Values(0).MyEstimate > 0 Then
                            TmpBucket.PickedTRP += BigBucket.Values(0).MyEstimate 'Increase the PickedTRP for the chosen FilmBucket
                            TmpBucket.Spots.Add(BigBucket.Values(0), BigBucket.Values(0)) ' Add the spot to the spotlist
                            TotalBigTRP -= BigBucket.Values(0).MyEstimate
                        Else
                            TmpBucket.PickedTRP += BigBucket.Values(0).ChannelEstimate  'Increase the PickedTRP for the chosen FilmBucket
                            TmpBucket.Spots.Add(BigBucket.Values(0), BigBucket.Values(0)) ' Add the spot to the spotlist
                            TotalBigTRP -= BigBucket.Values(0).ChannelEstimate
                        End If
                        BigBucket.RemoveAt(0)
                    End While

                    Dim TmpInd As New cIndividual
                    For Each kv As KeyValuePair(Of Trinity.cBookedSpot, Trinity.cBookedSpot) In Spots
                        Dim NewSpot As New Trinity.cBookedSpot(Campaign)
                        NewSpot.ID = kv.Value.ID
                        NewSpot.MyEstimate = kv.Value.MyEstimate
                        NewSpot.ChannelEstimate = kv.Value.ChannelEstimate
                        NewSpot.Bookingtype = kv.Value.Bookingtype
                        TmpInd.Spots.Add(NewSpot, NewSpot.ID)
                    Next
                    TmpInd.FilmCount = TmpWeek.Films.Count
                    For i = 1 To FilmBuckets.Count
                        TmpInd.FilmSplit(i) = FilmBuckets(i).pickedtrp / TotalTRP
                        TmpInd.IdealFilmSplit(i) = FilmBuckets(i).trptopick / TotalTRP
                        TmpInd.FilmCode(i) = FilmBuckets(i).film.filmcode
                        For Each kv As KeyValuePair(Of Trinity.cBookedSpot, Trinity.cBookedSpot) In FilmBuckets(i).spots
                            TmpInd.Spots(kv.Value.ID).Filmcode = FilmBuckets(i).film.filmcode
                        Next
                    Next
                    TmpInd.CalculateFitness()
                    Individuals.Add(TmpInd, TmpInd)
                Next
                BestSoFar = Individuals.Values(0)
                For Generation As Integer = 1 To 20
                    Dim TmpIndividuals As New List(Of cIndividual)
                    For i = 0 To 0.25 * IndividualCount
                        Dim Parent1 As Integer = (Rnd() * IndividualCount * 0.75)
                        Dim Parent2 As Integer = (Rnd() * IndividualCount * 0.75)
                        Dim BP As Integer
                        Dim s As Integer
                        Dim TmpInd As New cIndividual
                        TmpInd.FilmCount = Individuals.Values(Parent1).FilmCount
                        Dim FilmTRP(TmpInd.FilmCount) As Single

                        BP = Int(Rnd() * Individuals.Values(Parent1).Spots.Count) + 1
                        If Individuals.Values(Parent1).Spots.Count = 0 Then BP = 0

                        'CrossOver
                        For s = 1 To BP
                            TmpInd.Spots.Add(Individuals.Values(Parent1).Spots(s), Individuals.Values(Parent1).Spots(s).ID)
                            If Individuals.Values(Parent1).Spots(s).MyEstimate > 0 Then
                                FilmTRP(FilmBuckets(Individuals.Values(Parent1).Spots(s).Film.name).index) += Individuals.Values(Parent1).Spots(s).MyEstimate
                            Else
                                FilmTRP(FilmBuckets(Individuals.Values(Parent1).Spots(s).Film.name).index) += Individuals.Values(Parent1).Spots(s).ChannelEstimate
                            End If
                        Next
                        For s = BP + 1 To Individuals.Values(Parent1).Spots.Count
                            TmpInd.Spots.Add(Individuals.Values(Parent2).Spots(s), Individuals.Values(Parent2).Spots(s).ID)
                            If Individuals.Values(Parent2).Spots(s).MyEstimate > 0 Then
                                FilmTRP(FilmBuckets(Individuals.Values(Parent2).Spots(s).Film.name).index) += Individuals.Values(Parent2).Spots(s).MyEstimate
                            Else
                                FilmTRP(FilmBuckets(Individuals.Values(Parent2).Spots(s).Film.name).index) += Individuals.Values(Parent2).Spots(s).ChannelEstimate
                            End If
                        Next

                        'Might be an idea to add mutation?

                        For s = 1 To TmpInd.FilmCount
                            TmpInd.FilmSplit(s) = FilmTRP(s) / TotalTRP
                            TmpInd.IdealFilmSplit(s) = Individuals.Values(Parent1).IdealFilmSplit(s)
                        Next
                        TmpInd.CalculateFitness()

                        TmpIndividuals.Add(TmpInd)

                    Next
                    For i = 0 To TmpIndividuals.Count - 1
                        Individuals.Add(TmpIndividuals(i), TmpIndividuals(i))
                    Next
                Next
                For Each TmpSpot As Trinity.cBookedSpot In Individuals.Values(0).Spots
                    If Campaign.BookedSpots(TmpSpot.ID).Film Is Nothing Then
                        OldIndex = OldIndexes(Campaign.BookedSpots(TmpSpot.ID).Filmcode)
                    Else
                        OldIndex = Campaign.BookedSpots(TmpSpot.ID).Film.Index
                    End If
                    SwitchFilm(Campaign.BookedSpots(TmpSpot.ID), TmpSpot.Filmcode, OldIndex)
                Next
            End If
        Next
        UpdateSpotlist()
        grdSpotlist.Invalidate()
        UpdateBookedTRP()
        UpdateLeftToBook()
        UpdateFilm()
        UpdateDaypart()
        UpdatePrimePeak()
    End Sub

    Private Sub grdSpotlist_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSpotlist.CellValuePushed
        Dim Chan As String = cmbChannel.Text.Substring(0, InStr(cmbChannel.Text, " ") - 1)
        Dim BT As String = Mid(cmbChannel.Text, InStr(cmbChannel.Text, " ") + 1)
        Dim TmpCol As Windows.Forms.DataGridViewColumn = grdSpotlist.Columns(e.ColumnIndex)
        Dim TmpSpot As Trinity.cBookedSpot = Campaign.BookedSpots(grdSpotlist.Rows(e.RowIndex).Tag)

        Select Case TmpCol.HeaderText
            Case "Gross Price"
                grdSpotlist.Columns(e.ColumnIndex).Tag -= TmpSpot.GrossPrice
                TmpSpot.GrossPrice = e.Value / TmpSpot.AddedValueIndex(False)
                grdSpotlist.Columns(e.ColumnIndex).Tag += TmpSpot.GrossPrice
                grdSpotlist.InvalidateRow(grdSpotlist.RowCount - 1)
            Case "Net Price"
                grdSpotlist.Columns(e.ColumnIndex).Tag -= TmpSpot.NetPrice
                TmpSpot.NetPrice = e.Value / TmpSpot.AddedValueIndex
                grdSpotlist.Columns(e.ColumnIndex).Tag += TmpSpot.NetPrice
                grdSpotlist.InvalidateRow(grdSpotlist.RowCount - 1)
            Case "Chan Est"
                grdSpotlist.Columns(e.ColumnIndex).Tag -= TmpSpot.ChannelEstimate
                TmpSpot.ChannelEstimate = e.Value
                grdSpotlist.Columns(e.ColumnIndex).Tag += TmpSpot.ChannelEstimate
                grdSpotlist.InvalidateRow(grdSpotlist.RowCount - 1)
            Case "Notes"
                TmpSpot.Comments = e.Value
                'Case "Bid"
                '    TmpSpot.Bid = e.Value
            Case "Est"
                grdSpotlist.Columns(e.ColumnIndex).Tag -= TmpSpot.MyEstimate
                TmpSpot.MyEstimate = e.Value
                grdSpotlist.Columns(e.ColumnIndex).Tag += TmpSpot.MyEstimate
                grdSpotlist.InvalidateRow(grdSpotlist.RowCount - 1)

            Case "Est (" & Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.TargetName & ")"
                TmpSpot.MyEstimateBuyTarget = e.Value
        End Select
        UpdateFilm()
        UpdateDaypart()
        UpdatePlannedTRP()
        UpdateBookedTRP()
        UpdateLeftToBook()
        UpdateEstimatedTrend()
        grdSpotlist.InvalidateRow(e.RowIndex)
        UpdatePrimePeak()
    End Sub

    Private Sub grdSpotlist_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdSpotlist.ColumnHeaderMouseClick
        SortSpotlistColumn = grdSpotlist.Columns(e.ColumnIndex)
        If SortSpotlistColumn.HeaderCell.SortGlyphDirection = Windows.Forms.SortOrder.Ascending Then
            SortSpotlistColumn.HeaderCell.SortGlyphDirection = Windows.Forms.SortOrder.Descending
        Else
            SortSpotlistColumn.HeaderCell.SortGlyphDirection = Windows.Forms.SortOrder.Ascending
        End If
        UpdateSpotlist()
        'Stop
    End Sub

    Private Sub grdSchedule_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSchedule.CellValueNeeded
        Dim Est As Single
        Dim EstBT As Single
        Dim Chan As String
        Dim BT As String

        Dim TmpCol As Windows.Forms.DataGridViewColumn = grdSchedule.Columns(e.ColumnIndex)
        Dim ID As String = grdSchedule.Rows(e.RowIndex).Tag
        Dim TmpEI As Trinity.cExtendedInfo = Campaign.ExtendedInfos(ID)

        If TmpEI Is Nothing Then
            Debug.Print("Didn't match to a break in Extended Infos")
        Else

            'Check the combo boxes for the current channel Booking type and film
            Chan = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
            BT = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Name

            Dim TmpFilm As Trinity.cFilm = Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(cmbFilm.Text)

            If TmpCol.Tag = "ID" Then
                e.Value = TmpEI.ID
            ElseIf TmpCol.Visible = True Then
                Est = TmpEI.EstimatedRating
                EstBT = TmpEI.EstimatedRatingBuyingTarget
                SkipIt = True
                Select Case TmpCol.Tag
                    Case "Date"
                        e.Value = Format(TmpEI.AirDate, "Short Date")
                    Case "Week"
                        e.Value = DatePart(DateInterval.WeekOfYear, TmpEI.AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                    Case "Weekday"
                        e.Value = Days(DatePart(DateInterval.Weekday, TmpEI.AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) - 1)
                    Case "Time"
                        If CInt(Trinity.Helper.Mam2Tid(TmpEI.MaM).Substring(0, 2)) > 70 Then
                            Dim cp As Integer = 0
                        End If
                        e.Value = Trinity.Helper.Mam2Tid(TmpEI.MaM)
                    Case "Channel"
                        e.Value = Campaign.Channels(TmpEI.Channel).Shortname
                    Case "Program"
                        e.Value = TmpEI.ProgAfter
                    Case "Gross Price"
                        e.Value = Format(TmpEI.GrossPrice(TmpFilm), "##,##0 kr")
                    Case "Chan Est"
                        e.Value = Format(TmpEI.ChannelEstimate, "0.0")
                    Case "Net Price"
                        e.Value = Format(TmpEI.NetPrice(TmpFilm, Campaign.Channels(Chan).BookingTypes(BT)), "##,##0 kr")
                    Case "Solus"
                        e.Value = Format(TmpEI.Solus, "0.0")
                    Case "CPS"
                        e.Value = Format(TmpEI.CostPerSolus(TmpFilm, Campaign.Channels(Chan).BookingTypes(BT)), "##,##0 kr")
                    Case "Solus 1st"
                        e.Value = Format(TmpEI.SolusFirst, "0.0")
                    Case "Remarks"
                        If TmpEI.Remark = "L" Then
                            e.Value = "Local"
                        Else
                            e.Value = TmpEI.Remark
                        End If
                    Case "Gross CPP"
                        e.Value = Format(TmpEI.GrossCPP30, "##,##0 kr")
                    Case "CPP Main"
                        e.Value = Format(TmpEI.NetCPP(TmpFilm, Campaign.Channels(Chan).BookingTypes(BT)), "##,##0 kr")
                    Case "CPT Main"
                        e.Value = Format(TmpEI.NetCPT(TmpFilm, Campaign.Channels(Chan).BookingTypes(BT)), "##,##0 kr")

                    Case "CPP (Chn Est)"
                        e.Value = Format(TmpEI.NetCPPChannel(TmpFilm, Campaign.Channels(Chan).BookingTypes(BT)), "##,##0 kr")
                    Case "Gross CPP (Chn Est)"
                        e.Value = Format(TmpEI.GrossCPPChannel, "##,##0 kr")
                    Case "Net CPP (Chn Est)"
                        e.Value = Format(TmpEI.NetCPPChannel(TmpFilm, Campaign.Channels(Chan).BookingTypes(BT)), "##,##0 kr")


                    Case "Est"
                        e.Value = Format(Est, "0.0")
                    Case "Est Buy"
                        e.Value = Format(EstBT, "0.0")
                    Case "Idx Chan Est"
                        e.Value = Format(TmpEI.IndexVsChannelEstimate, "0")
                    Case "Idx Est Buy"
                        If EstBT > 0 Then
                            e.Value = Format((Est / EstBT) * 100, "0")
                        Else
                            e.Value = "-"
                        End If
                    Case "Addition"
                        e.Value = TmpEI.Addition
                    Case "Idx Scnd/Chan"
                        e.Value = Format(TmpEI.IndexSecondVsChannelEstimate, "0")
                    Case "Est Second"
                        e.Value = Format(TmpEI.EstimatedRatingSecondTarget, "0.0")
                    Case "CPP Second"
                        e.Value = Format(TmpEI.NetCPPSecond(TmpFilm, Campaign.Channels(Chan).BookingTypes(BT)), "##,##0 kr")
                    Case "Quality"
                        e.Value = TmpEI.Estimation
                    Case "Est Rch TRP"
                        e.Value = Format(TmpEI.EstimatedReachRating, "N1")
                End Select
            End If
        End If
    End Sub

    Private Sub grdSchedule_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdSchedule.CellFormatting
        Dim TmpEI As Trinity.cExtendedInfo = Campaign.ExtendedInfos(grdSchedule.Rows(e.RowIndex).Tag)
        If TmpEI Is Nothing Then Exit Sub
        If TmpEI.IsBooked Then
            e.CellStyle.ForeColor = Color.Blue
        Else
            e.CellStyle.ForeColor = Color.Black
        End If
        If grdSchedule.Columns(e.ColumnIndex).Tag = "Idx Chan Est" Then
            Dim Est As Single = TmpEI.EstimatedRating
            If Format((Est / TmpEI.ChannelEstimate) * 100, "0") > 100 Then
                e.CellStyle.ForeColor = Drawing.Color.LightGreen
            ElseIf Format((Est / TmpEI.ChannelEstimate) * 100, "0") < 100 Then
                e.CellStyle.ForeColor = Drawing.Color.Red
            End If
        ElseIf grdSchedule.Columns(e.ColumnIndex).Tag = "Idx Est Buy" Then
            If e.Value <> "-" Then
                If e.Value > 100 Then
                    e.CellStyle.ForeColor = Color.LightGreen
                ElseIf e.Value < 100 Then
                    e.CellStyle.ForeColor = Color.Red
                End If
            End If
        ElseIf grdSchedule.Columns(e.ColumnIndex).Tag = "Idx Scnd/Chan" Then
            If e.Value <> "-" Then
                If e.Value > 100 Then
                    e.CellStyle.ForeColor = Color.LightGreen
                ElseIf e.Value < 100 Then
                    e.CellStyle.ForeColor = Color.Red
                End If
            End If
        ElseIf grdSchedule.Columns(e.ColumnIndex).Tag = "Quality" Then
            If e.Value > 0.8 Then
                e.CellStyle.ForeColor = Color.Green
            ElseIf e.Value > 0.5 Then
                e.CellStyle.ForeColor = Color.Orange
            Else
                e.CellStyle.ForeColor = Color.Red
            End If
            e.CellStyle.Format = "P1"
        End If
    End Sub

    Private Sub grdSchedule_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdSchedule.ColumnHeaderMouseClick
        Dim Chan As String = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
        Dim BT As String = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Name

        Dim TmpFilm As Trinity.cFilm = Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(cmbFilm.Text)
        Dim List As New List(Of Trinity.cExtendedInfo)

        SortScheduleColumn = grdSchedule.Columns(e.ColumnIndex)

        If SortScheduleColumn.HeaderCell.SortGlyphDirection = Windows.Forms.SortOrder.Ascending Then
            SortScheduleColumn.HeaderCell.SortGlyphDirection = Windows.Forms.SortOrder.Descending
        Else
            SortScheduleColumn.HeaderCell.SortGlyphDirection = Windows.Forms.SortOrder.Ascending
        End If

        For Each TmpEI As DictionaryEntry In Campaign.ExtendedInfos
            If TmpEI.Value.bookingtype = cmbChannel.SelectedItem.ToString Then
                If (cmbDatabase.SelectedIndex = 1 And TmpEI.Value.IsAvail = True) Or (cmbDatabase.SelectedIndex = 0 And TmpEI.Value.isavail = False) Then
                    If FilterIn(TmpEI.Value) Then
                        List.Add(TmpEI.Value)
                    End If
                End If
            End If
        Next

        If Not SortScheduleColumn Is Nothing Then
            If SortScheduleColumn.HeaderCell.SortGlyphDirection = Windows.Forms.SortOrder.Descending Then
                If SortScheduleColumn.Tag = "Date" Then
                    QuicksortDescending(List, grdSchedule.Columns("colID"), 0, List.Count - 1, TmpFilm, Campaign.Channels(Chan).BookingTypes(BT))
                Else
                    QuicksortDescending(List, SortScheduleColumn, 0, List.Count - 1, TmpFilm, Campaign.Channels(Chan).BookingTypes(BT))
                End If

            Else
                If SortScheduleColumn.Tag = "Date" Then
                    QuicksortAscending(List, grdSchedule.Columns("colID"), 0, List.Count - 1, TmpFilm, Campaign.Channels(Chan).BookingTypes(BT))
                Else
                    QuicksortAscending(List, SortScheduleColumn, 0, List.Count - 1, TmpFilm, Campaign.Channels(Chan).BookingTypes(BT))
                End If

            End If
        End If

        For i As Integer = 0 To List.Count - 1
            grdSchedule.Rows(i).Tag = List(i).ID
        Next

        For Each TmpCol As Windows.Forms.DataGridViewColumn In grdSchedule.Columns
            If Not TmpCol Is SortScheduleColumn Then
                TmpCol.HeaderCell.SortGlyphDirection = Windows.Forms.SortOrder.None
            End If
        Next
        UpdateSchedule(False, False)

    End Sub

    Private Sub GfxSchedule2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GfxSchedule2.DoubleClick
        Saved = False
    End Sub

    ' Old version using XML document

    'Private Sub cmdLoadOtherSpots_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadOtherSpots.Click
    '    'sets a "open" dialog
    '    Dim dlgdialog As New System.Windows.Forms.OpenFileDialog
    '    dlgdialog.Title = "Open campaign"
    '    dlgdialog.FileName = "*.cmp"
    '    dlgdialog.Filter = "Trinity campaigns|*.cmp|Trinity 3.0 campaigns|*.kmp"
    '    dlgdialog.FilterIndex = 0
    '    dlgdialog.CheckFileExists = True
    '    dlgdialog.InitialDirectory = TrinitySettings.CampaignFiles
    '    If dlgdialog.ShowDialog <> Windows.Forms.DialogResult.OK Then
    '        Exit Sub
    '    End If
    '    Me.Cursor = Windows.Forms.Cursors.WaitCursor


    '    Dim TmpID As String
    '    Dim TmpDate As Date
    '    Dim TmpBT As String
    '    Dim TmpChannelEstimate As Single
    '    Dim TmpDBID As String
    '    Dim TmpFilmcode As String
    '    Dim TmpGrossPrice As Decimal
    '    Dim TmpMaM As Short
    '    Dim tmpMyEstimate As Single
    '    Dim TmpMyEstChanTarget As Single
    '    Dim TmpNetPrice As Decimal
    '    Dim TmpPlacement As Trinity.cBookedSpot.PlaceEnum
    '    Dim TmpProgAfter As String
    '    Dim TmpProgBefore As String
    '    Dim TmpProg As String
    '    Dim TmpIsLocal As Boolean
    '    Dim TmpIsRB As Boolean
    '    'Dim TmpAddition As Single

    '    Dim XMLSpot As Xml.XmlElement
    '    Dim XMLCamp As Xml.XmlElement
    '    Dim XMLDoc As Xml.XmlDocument = New Xml.XmlDocument
    '    Dim TmpChannel As String
    '    Dim TmpBookingType As Trinity.cBookingType


    '    XMLDoc.Load(dlgdialog.FileName)
    '    XMLCamp = XMLDoc.GetElementsByTagName("Campaign").Item(0)
    '    XMLSpot = XMLCamp.GetElementsByTagName("BookedSpots").Item(0).FirstChild

    '    While Not XMLSpot Is Nothing
    '        TmpChannel = XMLSpot.GetAttribute("Channel") ' Channel
    '        Dim c As Trinity.cChannel = Campaign.Channels(TmpChannel)
    '        TmpID = XMLSpot.GetAttribute("ID")
    '        TmpDate = XMLSpot.GetAttribute("AirDate")
    '        TmpBookingType = c.BookingTypes(XMLSpot.GetAttribute("Bookingtype"))
    '        TmpChannelEstimate = 0
    '        TmpDBID = XMLSpot.GetAttribute("DatabaseID")
    '        TmpFilmcode = XMLSpot.GetAttribute("Filmcode")
    '        TmpGrossPrice = 0
    '        TmpMaM = XMLSpot.GetAttribute("MaM")
    '        tmpMyEstimate = 0
    '        TmpMyEstChanTarget = 0
    '        TmpNetPrice = 0
    '        TmpPlacement = Val(XMLSpot.GetAttribute("Placement"))
    '        TmpProgAfter = XMLSpot.GetAttribute("ProgAfter")
    '        TmpProgBefore = XMLSpot.GetAttribute("ProgBefore")
    '        TmpProg = XMLSpot.GetAttribute("Programme")
    '        TmpBT = XMLSpot.GetAttribute("Bookingtype")
    '        TmpDBID = XMLSpot.GetAttribute("DatabaseID")
    '        'If Not XMLSpot.GetAttribute("Addition") Is Nothing Then TmpAddition = XMLSpot.GetAttribute("Addition")

    '        If IsDBNull(XMLSpot.GetAttribute("IsLocal")) Then
    '            TmpIsLocal = False
    '            TmpIsRB = False
    '        Else
    '            TmpIsLocal = XMLSpot.GetAttribute("IsLocal")
    '            TmpIsRB = XMLSpot.GetAttribute("IsRB")
    '        End If

    '        otherBookedSpotsList.Add(TmpDBID)
    '        otherBookedSpots.Add(TmpID, TmpDBID, TmpChannel, TmpDate, TmpMaM, TmpProg, TmpProgAfter, TmpProgBefore, TmpGrossPrice, TmpNetPrice, TmpChannelEstimate, tmpMyEstimate, TmpMyEstChanTarget, TmpFilmcode, TmpBT, TmpIsLocal, TmpIsRB, 0)
    '        otherBookedSpots(TmpID).otherCampaign = True
    '        If Not IsDBNull(XMLSpot.GetAttribute("Comments")) Then
    '            otherBookedSpots(TmpID).Comments = XMLSpot.GetAttribute("Comments")
    '        End If
    '        'add it to the list of channels
    '        If readChannels.IndexOf(c) = -1 Then
    '            readChannels.Add(c)
    '        End If
    '        XMLSpot = XMLSpot.NextSibling
    '    End While

    '    System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
    '    Me.Cursor = Windows.Forms.Cursors.Default

    '    UpdateSpotlist()
    '    UpdateSchedule(True, True)
    '    UpdateBookedTRP()
    '    UpdateLeftToBook()
    '    UpdateFilm()
    '    UpdateDaypart()
    '    UpdateRF()
    '    UpdateEstimatedTrend()
    '    UpdatePrimePeak()
    'End Sub

    ' New version using xdocument 
    Private Sub cmdLoadOtherSpots_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadOtherSpots.Click
        Dim XMLDoc As XDocument
        'sets a "open" dialog
        If TrinitySettings.SaveCampaignsAsFiles Then
            Dim dlgdialog As New System.Windows.Forms.OpenFileDialog
            dlgdialog.Title = "Open campaign"
            dlgdialog.FileName = "*.cmp"
            dlgdialog.Filter = "Trinity campaigns|*.cmp|Trinity 3.0 campaigns|*.kmp"
            dlgdialog.FilterIndex = 0
            dlgdialog.CheckFileExists = True
            dlgdialog.InitialDirectory = TrinitySettings.CampaignFiles
            If dlgdialog.ShowDialog <> Windows.Forms.DialogResult.OK Then
                Exit Sub
            End If
            XMLDoc = XDocument.Load(dlgdialog.FileName)
        Else
            Dim _frm As New frmOpenFromDB()
            _frm.DoNotLoadCampaign = True
            If _frm.ShowDialog = Windows.Forms.DialogResult.OK Then

                Dim _campXML As String
                _campXML = DBReader.GetCampaign(_frm.CampaignID, True)

                XMLDoc = XDocument.Parse(_campXML)
            Else
                Exit Sub
            End If
        End If
        Me.Cursor = Windows.Forms.Cursors.WaitCursor


        Dim TmpID As String
        Dim TmpDate As Date
        Dim TmpBT As String
        Dim TmpChannelEstimate As Single
        Dim TmpDBID As String
        Dim TmpFilmcode As String
        Dim TmpGrossPrice As Decimal
        Dim TmpMaM As Short
        Dim tmpMyEstimate As Single
        Dim TmpMyEstChanTarget As Single
        Dim TmpNetPrice As Decimal
        Dim TmpPlacement As Trinity.cBookedSpot.PlaceEnum
        Dim TmpProgAfter As String
        Dim TmpProgBefore As String
        Dim TmpProg As String
        Dim TmpIsLocal As Boolean
        Dim TmpIsRB As Boolean
        'Dim TmpAddition As Single

        Dim XMLCamp As XElement

        Dim TmpChannel As String
        Dim TmpBookingType As Trinity.cBookingType

        XMLCamp = XMLDoc.Root


        For Each tmpSpot As XElement In XMLCamp.Element("BookedSpots").Elements()

            TmpChannel = tmpSpot.Attribute("Channel").Value
            Dim c As Trinity.cChannel = Campaign.Channels(TmpChannel)

            TmpID = tmpSpot.Attribute("ID").Value
            TmpDate = tmpSpot.Attribute("AirDate").Value
            TmpBookingType = c.BookingTypes(tmpSpot.Attribute("Bookingtype").Value)
            TmpChannelEstimate = 0
            TmpDBID = tmpSpot.Attribute("DatabaseID").Value
            TmpFilmcode = tmpSpot.Attribute("Filmcode").Value
            TmpGrossPrice = 0
            TmpMaM = tmpSpot.Attribute("MaM").Value
            tmpMyEstimate = 0
            TmpMyEstChanTarget = 0
            TmpNetPrice = 0

            If (tmpSpot.Attribute("Placement") IsNot Nothing) Then
                TmpPlacement = Val(tmpSpot.Attribute("Placement").Value)
            End If
            TmpProgAfter = tmpSpot.Attribute("ProgAfter").Value
            TmpProgBefore = tmpSpot.Attribute("ProgBefore").Value
            TmpProg = tmpSpot.Attribute("Programme").Value
            TmpBT = tmpSpot.Attribute("Bookingtype").Value
            TmpDBID = tmpSpot.Attribute("DatabaseID").Value

            If IsDBNull(tmpSpot.Attribute("IsLocal").Value) Then
                TmpIsLocal = False
                TmpIsRB = False
            Else
                TmpIsLocal = tmpSpot.Attribute("IsLocal").Value
                TmpIsRB = tmpSpot.Attribute("IsRB").Value
            End If

            otherBookedSpotsList.Add(TmpDBID)
            otherBookedSpots.Add(TmpID, TmpDBID, TmpChannel, TmpDate, TmpMaM, TmpProg, TmpProgAfter, TmpProgBefore, TmpGrossPrice, TmpNetPrice, TmpChannelEstimate, tmpMyEstimate, TmpMyEstChanTarget, TmpFilmcode, TmpBT, TmpIsLocal, TmpIsRB, 0)
            otherBookedSpots(TmpID).otherCampaign = True
            If Not IsDBNull(tmpSpot.Attribute("Comments").Value) Then
                otherBookedSpots(TmpID).Comments = tmpSpot.Attribute("Comments").Value
            End If
            'add it to the list of channels
            If readChannels.IndexOf(c) = -1 Then
                readChannels.Add(c)
            End If

        Next

        Me.Cursor = Windows.Forms.Cursors.Default

        UpdateSpotlist()
        UpdateSchedule(True, True)
        UpdateBookedTRP()
        UpdateLeftToBook()
        UpdateFilm()
        UpdateDaypart()
        UpdateRF()
        UpdateEstimatedTrend()
        UpdatePrimePeak()
    End Sub


    Private Sub cmdLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLock.Click
        For Each TmpRow As Windows.Forms.DataGridViewRow In grdSpotlist.SelectedRows
            If TmpRow.Tag IsNot Nothing Then
                If Campaign.BookedSpots(TmpRow.Tag) Is Nothing Then
                    MsgBox("You can not lock spots from another campaign.")
                Else
                    Campaign.BookedSpots(TmpRow.Tag).Locked = cmdLock.Checked
                End If
            End If
        Next
    End Sub

    Private Sub grdSpotlist_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSpotlist.RowEnter
        If Not e.RowIndex = grdSpotlist.RowCount - 1 Then
            If Campaign.BookedSpots(grdSpotlist.Rows(e.RowIndex).Tag) Is Nothing Then
                cmdLock.Checked = False
            Else
                cmdLock.Checked = Campaign.BookedSpots(grdSpotlist.Rows(e.RowIndex).Tag).Locked
            End If
        End If
    End Sub


    Private Class cIndividual
        Const WeightCoeff = 3
        Public Spots As New Collection
        Private _idealFilmsplit() As Single
        Private _filmsplit() As Single
        Private _filmCount As Integer
        Private _fitness As Single
        Public FilmCode() As String
        Public ID As String

        Public ReadOnly Property Fitness() As Single
            Get
                Return _fitness
            End Get
        End Property

        Public Sub CalculateFitness()
            _fitness = 0
            For i As Integer = 1 To _filmCount
                If _idealFilmsplit(i) = 0 Then
                    _fitness += 1
                Else
                    _fitness += (1 + Math.Abs(1 - (_filmsplit(i) / _idealFilmsplit(i)))) ^ WeightCoeff
                End If
            Next
            _fitness /= _filmCount
            'If _fitness < 1 Then Stop
        End Sub

        Public Property FilmCount() As Integer
            Get
                Return _filmCount
            End Get
            Set(ByVal value As Integer)
                _filmCount = value
                ReDim _filmsplit(value)
                ReDim _idealFilmsplit(value)
                ReDim FilmCode(value)
            End Set
        End Property

        Public Property FilmSplit(ByVal Film As Integer) As Single
            Get
                Return _filmsplit(Film)
            End Get
            Set(ByVal value As Single)
                _filmsplit(Film) = value
            End Set
        End Property

        Public Property IdealFilmSplit(ByVal Film As Integer) As Single
            Get
                Return _idealFilmsplit(Film)
            End Get
            Set(ByVal value As Single)
                _idealFilmsplit(Film) = value
            End Set
        End Property

        Public Sub New()
            ID = CreateGUID()
        End Sub
    End Class

    Private Class IndComparer
        Implements IComparer(Of cIndividual)

        Public Function Compare(ByVal x As cIndividual, ByVal y As cIndividual) As Integer Implements System.Collections.Generic.IComparer(Of cIndividual).Compare
            If x.Fitness > y.Fitness Then
                Return 1
            ElseIf x.Fitness < y.Fitness Then
                Return -1
            Else
                Return 1
            End If
        End Function
    End Class

    Private Class FilmBucket
        Public Spots As New SortedList(Of Trinity.cBookedSpot, Trinity.cBookedSpot)(New MyEstComparer)
        Public PickedTRP As Single
        Public TRPToPick As Single
        Public Film As Trinity.cFilm
        Public Index As Integer
    End Class

    Private Class MyEstComparer
        Implements IComparer(Of Trinity.cBookedSpot)

        Public Function Compare(ByVal x As Trinity.cBookedSpot, ByVal y As Trinity.cBookedSpot) As Integer Implements System.Collections.Generic.IComparer(Of Trinity.cBookedSpot).Compare
            If x.MyEstimate > 0 AndAlso y.MyEstimate > 0 Then
                If x.MyEstimate < y.MyEstimate Then
                    Return 1
                ElseIf x.MyEstimate > y.MyEstimate Then
                    Return -1
                Else
                    Return 1
                End If
            ElseIf x.MyEstimate = 0 AndAlso y.MyEstimate = 0 Then
                If x.ChannelEstimate < y.ChannelEstimate Then
                    Return 1
                ElseIf x.ChannelEstimate > y.ChannelEstimate Then
                    Return -1
                Else
                    Return 1
                End If
            ElseIf x.MyEstimate = 0 Then
                If x.ChannelEstimate < y.MyEstimate Then
                    Return 1
                ElseIf x.ChannelEstimate > y.MyEstimate Then
                    Return -1
                Else
                    Return 1
                End If
            ElseIf y.MyEstimate = 0 Then
                If x.MyEstimate < y.ChannelEstimate Then
                    Return 1
                ElseIf x.MyEstimate > y.ChannelEstimate Then
                    Return -1
                Else
                    Return 1
                End If
            End If
        End Function
    End Class

    'Private Class MyEstimate
    '    Implements IComparable(Of MyEstimate)

    '    Public Name As String

    '    Public Overloads Function CompareTo(ByVal other As MyEstimate) As Integer Implements System.IComparable(Of MyEstimate).CompareTo
    '        Return -Rating.CompareTo(other.Rating)
    '    End Function

    '    Protected Rating As Single

    '    Public Sub New(ByVal MyRating As Single)
    '        Rating = MyRating
    '        Name = CreateGUID()
    '    End Sub
    'End Class

    Private Class CPPColumnTag
        Public Budget As Decimal
        Public Estimate As Single

        Public Function CPP() As Single
            If Estimate > 0 Then
                Return Math.Round(Budget, 0) / Math.Round(Estimate, 1)
            Else
                Return 0
            End If
        End Function
    End Class

    Private Sub cmdExportToAllocate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExportToAllocate.Click
        If Windows.Forms.MessageBox.Show( _
                                    "Are you sure you want to replace the current allocation" & vbCrLf & _
                                    "with the budget and TRPs from this Booking?" & vbCrLf & _
                                    vbCrLf & _
                                    "This will also modify the pricelist for this channel.", _
                                    "T R I N I T Y", _
                                    Windows.Forms.MessageBoxButtons.YesNoCancel, _
                                    Windows.Forms.MessageBoxIcon.Question) _
                        <> Windows.Forms.DialogResult.Yes Then Exit Sub

        Dim TmpBT As Trinity.cBookingType = cmbChannel.SelectedItem
        Dim TotTRP As Single = 0
        Dim TotTRPBuy As Single = 0
        Dim SavePricelist As New List(Of Trinity.cPricelistPeriod)
        Dim WasCalcCPP As Boolean = TmpBT.BuyingTarget.CalcCPP
        For Each TmpPeriod As Trinity.cPricelistPeriod In TmpBT.BuyingTarget.PricelistPeriods
            SavePricelist.Add(TmpPeriod)
        Next
        TmpBT.BuyingTarget.PricelistPeriods.Clear()

        Dim BookedWeeks As New List(Of Trinity.cWeek)
        For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
            If Not BookedWeeks.Contains(TmpSpot.week) Then
                BookedWeeks.Add(TmpSpot.week)
            End If
        Next
        Dim WeekNumber As Integer = 0
        For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
            WeekNumber += 1
            If BookedWeeks.Contains(TmpWeek) Then
                TmpWeek.TRPControl = False
                Dim FilmTRP As New Dictionary(Of String, Single)
                With TmpBT.BuyingTarget.PricelistPeriods.Add("")
                    .FromDate = Date.FromOADate(TmpWeek.StartDate)
                    .ToDate = Date.FromOADate(TmpWeek.EndDate)
                    .TargetNat = TmpBT.BuyingTarget.Target.UniSize
                    .TargetUni = TmpBT.BuyingTarget.Target.UniSize
                    .PriceIsCPP = True
                    TmpBT.BuyingTarget.CalcCPP = False

                    Dim Budget As Decimal = 0
                    Dim Gross As Decimal = 0
                    Dim TRP As Single = 0
                    Dim AVShare As New Dictionary(Of Trinity.cAddedValue, Single)
                    For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                        If TmpSpot.week Is TmpWeek Then
                            If TmpSpot.GrossPrice30 = 0 Then
                                Gross += (TmpSpot.NetPrice / (1 - TmpSpot.Bookingtype.BuyingTarget.Discount)) / (TmpSpot.Film.Index / 100) * TmpSpot.AddedValueIndex(False)
                            Else
                                Gross += TmpSpot.GrossPrice30 * TmpSpot.AddedValueIndex(False)
                            End If
                            Budget += TmpSpot.NetPrice * TmpSpot.AddedValueIndex
                            If TmpSpot.AddedValues.Count > 0 Then
                                For Each TmpAV As Trinity.cAddedValue In TmpSpot.AddedValues.Values
                                    If Not AVShare.ContainsKey(TmpAV) Then
                                        AVShare.Add(TmpAV, TmpSpot.NetPrice * TmpSpot.AddedValueIndex)
                                    Else
                                        AVShare(TmpAV) += TmpSpot.NetPrice * TmpSpot.AddedValueIndex
                                    End If
                                Next
                            End If
                            If TmpSpot.ChannelEstimate = 0 Then
                                TRP += TmpSpot.MyEstimate
                            Else
                                TRP += TmpSpot.ChannelEstimate
                            End If
                            TotTRP += TmpSpot.MyEstimate
                            If FilmTRP.ContainsKey(TmpSpot.Film.Name) Then
                                If TmpSpot.ChannelEstimate = 0 Then
                                    FilmTRP(TmpSpot.Film.Name) += TmpSpot.MyEstimate
                                Else
                                    FilmTRP(TmpSpot.Film.Name) += TmpSpot.ChannelEstimate
                                End If
                            Else
                                If TmpSpot.MyEstimateBuyTarget = 0 Then
                                    FilmTRP.Add(TmpSpot.Film.Name, TmpSpot.MyEstimate)
                                Else
                                    FilmTRP.Add(TmpSpot.Film.Name, TmpSpot.ChannelEstimate)
                                End If
                            End If
                        End If
                    Next
                    For Each TmpFilm As Trinity.cFilm In TmpWeek.Films
                        If FilmTRP.ContainsKey(TmpFilm.Name) AndAlso TRP > 0 Then
                            TmpFilm.Share = (FilmTRP(TmpFilm.Name) / TRP) * 100
                        Else
                            TmpFilm.Share = 0
                        End If
                    Next
                    If TRP > 0 Then
                        .Price(True) = Gross / TRP
                    Else
                        .Price(True) = 0
                    End If

                    'TmpWeek.TRP = TRP    Går inte att sätta båda. Vad gör vi?
                    TmpWeek.NetBudget = Budget
                    TotTRPBuy += TRP

                    For Each TmpAV As Trinity.cAddedValue In TmpBT.AddedValues
                        If Budget > 0 AndAlso AVShare.ContainsKey(TmpAV) Then
                            TmpAV.Amount(WeekNumber) = (AVShare(TmpAV) / Budget) * 100
                        Else
                            TmpAV.Amount(WeekNumber) = 0
                        End If
                    Next
                End With
            Else
                Dim TmpUsePeriod As Trinity.cPricelistPeriod = Nothing
                For d As Long = TmpWeek.StartDate To TmpWeek.EndDate
                    For Each TmpPeriod As Trinity.cPricelistPeriod In SavePricelist
                        If TmpPeriod.FromDate.ToOADate <= d AndAlso TmpPeriod.ToDate.ToOADate >= d Then
                            With TmpBT.BuyingTarget.PricelistPeriods.Add("")
                                .FromDate = Date.FromOADate(d)
                                .ToDate = Date.FromOADate(d)
                                .Name = Format(Date.FromOADate(d), "Short date")
                                .PriceIsCPP = TmpPeriod.PriceIsCPP
                                If WasCalcCPP Then
                                    For dp As Integer = 0 To TmpBT.Dayparts.Count - 1
                                        .Price(TmpPeriod.PriceIsCPP, -1) += TmpPeriod.Price(TmpPeriod.PriceIsCPP, dp) * (TmpBT.Dayparts(dp).Share / 100)
                                    Next
                                Else
                                    .Price(TmpPeriod.PriceIsCPP, -1) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, -1)
                                End If
                                .TargetNat = TmpPeriod.TargetNat
                                .TargetUni = TmpPeriod.TargetUni
                            End With
                        End If
                    Next
                Next
            End If
        Next
        If TotTRPBuy > 0 Then
            TmpBT.IndexMainTarget = (TotTRP / TotTRPBuy) * 100
        End If
    End Sub

    Private Sub pbFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbFirst.Click
        cmbWeeks.SelectedIndex = 0
    End Sub

    Private Sub pbPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbPrevious.Click
        If cmbWeeks.SelectedIndex = 0 Then Exit Sub

        cmbWeeks.SelectedIndex = cmbWeeks.SelectedIndex - 1
    End Sub

    Private Sub pbNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbNext.Click
        If cmbWeeks.SelectedIndex = cmbWeeks.Items.Count - 1 Then Exit Sub

        cmbWeeks.SelectedIndex = cmbWeeks.SelectedIndex + 1
    End Sub

    Private Sub pbLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbLast.Click
        cmbWeeks.SelectedIndex = cmbWeeks.Items.Count - 1
    End Sub

    Private Sub cmbWeeks_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbWeeks.SelectedIndexChanged
        Dim week As Trinity.cWeek = Campaign.Channels(1).BookingTypes(1).Weeks(cmbWeeks.SelectedItem)
        GfxSchedule2.StartDate = Date.FromOADate(week.StartDate)
        GfxSchedule2.EndDate = Date.FromOADate(week.EndDate)
        GfxSchedule2.SetupDays()
    End Sub

    Private Sub cmdTweak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTweak.Click
        '***** NEED addBreak FUNCTION. MEANWHILE USE BRANDS
        Dim GenderFactor As Single = 0
        Dim AgeFactor As Single = 0
        Dim Count As Integer = 0
        Dim LowAge As Integer
        Dim HighAge As Integer
        Dim NormalCurve(99) As Single
        Dim OriginalTRP(99) As Single
        Dim MainTRP As Single = 0

        Const MAX = 100
        Const MULTIPLY = 1

        If InStr(Campaign.MainTarget.TargetNameNice, "+") > 0 Then
            LowAge = Mid(Campaign.MainTarget.TargetNameNice, 2, InStr(Campaign.MainTarget.TargetNameNice, "+") - 2)
            HighAge = 99
        Else
            LowAge = Mid(Campaign.MainTarget.TargetNameNice, 2, InStr(Campaign.MainTarget.TargetNameNice, "-") - 2)
            HighAge = Mid(Campaign.MainTarget.TargetNameNice, Len(Campaign.MainTarget.TargetNameNice) - (Len(Campaign.MainTarget.TargetNameNice) - 3 - Len(Trim(LowAge))))
        End If

        Dim Adedge As New ConnectWrapper.Brands
        Adedge.setArea(Campaign.Area)
        Adedge.setPeriod("-1d")
        Adedge.setChannelsArea(Campaign.ChannelString, Campaign.Area)
        Adedge.setTargetMnemonic("3+,M3+,W3+,3-49,50+", True)
        Trinity.Helper.AddTarget(Adedge, Campaign.MainTarget, True)
        For i As Integer = LowAge To HighAge
            Adedge.setTargetMnemonic(CStr(i), True)
        Next
        Adedge.clearList()
        Using frm As New frmTweak
            For Each TmpRow As Windows.Forms.DataGridViewRow In grdSchedule.SelectedRows
                Dim TmpEI As Trinity.cExtendedInfo = Campaign.ExtendedInfos(TmpRow.Tag)
                Dim TmpPeriod As Trinity.cPeriod = Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod)
                Dim ReRun As Boolean = False
                If Not TmpEI.BreakList Is Nothing Then
                    For b As Integer = 1 To TmpEI.BreakList.Count
                        Dim MenTRP As Single = TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, "M3+"))
                        Dim WomenTPR As Single = TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, "W3+"))
                        Dim AllAdultsTRP As Single = TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, "3+"))
                        Dim YoungTRP As Single = TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, "3-49"))
                        Dim OldTRP As Single = TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, "50+"))
                        Dim MainTargetTRP As Single = TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, Campaign.MainTarget))
                        MainTRP += MainTargetTRP
                        GenderFactor += MenTRP / (AllAdultsTRP * 2) - 0.5
                        AgeFactor += 0.5 - YoungTRP / (YoungTRP + OldTRP)
                        Count += 1
                        Dim TmpBreak As Trinity.cBreak = TmpEI.BreakList(b - 1)
                        Adedge.addBrand(Format(TmpEI.AirDate, "ddMMyy"), Trinity.Helper.Mam2Tid(TmpBreak.MaM), TmpBreak.Channel.AdEdgeNames, Campaign.Area, TmpBreak.Duration)
                    Next
                End If
            Next
            If Count > 0 Then
                GenderFactor /= Count
                AgeFactor /= Count
                MainTRP /= Count
            End If
            Adedge.Run(False)
            Dim TRP As Single = 0
            For i As Integer = LowAge To HighAge
                NormalCurve(i) = MAX - (((-51 + i) * MULTIPLY - (AgeFactor * 100)) / 10) ^ 2
                OriginalTRP(i) = Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smAll, 0, Campaign.TimeShift, Trinity.Helper.TargetIndex(Adedge, CStr(i))) / Count
                TRP += OriginalTRP(i)
            Next
            TRP /= HighAge - LowAge + 1
            frm.CorrectionFactor = MainTRP / TRP
            frm.TweakChart1.GenderFactor = GenderFactor
            frm.TweakChart1.AgeFactor = AgeFactor
            frm.TweakChart1.OriginalAgeFactor = AgeFactor
            frm.TweakChart1.OriginalGenderFactor = GenderFactor
            frm.SetNormalCurve(NormalCurve)
            frm.SetOriginalTRP(OriginalTRP)
            frm.BaseRating = Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smAll, 0, Campaign.TimeShift, Trinity.Helper.TargetIndex(Adedge, "3+")) / Count
            frm.LowAge = LowAge
            frm.HighAge = HighAge
            Select Case Campaign.MainTarget.TargetNameNice.Substring(0, 1)
                Case "A"
                    frm.Gender = frmTweak.GenderEnum.A
                Case "W"
                    frm.Gender = frmTweak.GenderEnum.W
                Case "M"
                    frm.Gender = frmTweak.GenderEnum.M
            End Select
            frm.ShowDialog()

        End Using
    End Sub

    Private Sub grdDetails_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdDetails.KeyUp
        If grdSchedule.SelectedRows.Count = 0 Then Exit Sub
        If e.KeyCode = Windows.Forms.Keys.Delete Then
            Dim TmpEI As Trinity.cExtendedInfo
            Dim TotRating As Single
            Dim TotRatingBT As Single
            Dim TotRatingSec As Single
            Dim Chan As String
            Dim BT As String
            Dim Count As Integer
            Dim ID As String = grdSchedule.SelectedRows(0).Tag
            Dim TmpPeriod As Trinity.cPeriod
            Dim b As Long

            If cmbChannel.SelectedIndex = -1 Then Exit Sub
            If ID = "" Then Exit Sub
            Chan = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).ParentChannel.ChannelName
            BT = DirectCast(cmbChannel.SelectedItem, Trinity.cBookingType).Name

            If Campaign.ExtendedInfos.Count = 0 Then UpdateSchedule(True, False)

            TmpEI = Campaign.ExtendedInfos(ID)

            For Each TmpRow As Windows.Forms.DataGridViewRow In grdDetails.SelectedRows
                TmpEI.BreakList.RemoveAt(TmpRow.Index)
                grdDetails.Rows.RemoveAt(TmpRow.Index)
            Next
            If Not TmpEI.EstimatedOnPeriod Is Nothing AndAlso Campaign.EstimationPeriods.Exists(TmpEI.EstimatedOnPeriod) Then
                TmpPeriod = DirectCast(Campaign.EstimationPeriods(TmpEI.EstimatedOnPeriod), Trinity.cPeriod)
                If Not TmpEI.BreakList Is Nothing Then
                    For b = 1 To TmpEI.BreakList.Count
                        TotRating += TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, Campaign.MainTarget))
                        If TmpEI.EstimationTarget = "" Then
                            TotRatingBT += TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Target))
                        Else
                            TotRatingBT += TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, TmpEI.EstimationTarget))
                        End If
                        TotRatingSec += TmpPeriod.Adedge.getUnit(Connect.eUnits.uTRP, TmpEI.BreakList(b - 1).BreakIdx, , , Trinity.Helper.TargetIndex(TmpPeriod.Adedge, Campaign.SecondaryTarget))
                        Count += 1
                    Next
                    If Count > 0 Then
                        TmpEI.EstimatedRating = TotRating / Count
                        TmpEI.EstimatedRatingBuyingTarget = TotRatingBT / Count
                        TmpEI.EstimatedRatingSecondTarget = TotRatingSec / Count
                    End If
                    TmpEI.Estimation = Trinity.Helper.EstimationQuality(TmpEI.BreakList, TmpEI, TmpPeriod.Adedge)
                End If
            End If
            grdSchedule.Invalidate()
        End If
    End Sub

    Private Sub grdSpotlist_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdSpotlist.MouseDown

        If e.Button = Windows.Forms.MouseButtons.Left Then
            Exit Sub
        End If


        Dim i As Integer
        Dim TmpWeek As Trinity.cWeek
        Dim WeekDays() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}
        Dim trim() As Char = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", " "}

        'Start building the base window that pops up when a row is right clicked
        Dim mnuFilter As New Windows.Forms.ContextMenuStrip

        'This is added as a submenu
        Dim mnuBookSameSpot As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Filter same program")
        Dim mnuBookSameSlot As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Filter same timeslot")
        'Dim mnuTransferToBooked As Windows.Forms.ToolStripMenuItem = mnuFilter.Items.Add("Transfer selected to this window")

        'And these are submenus to mnuBookSameSpot and mnuBookSameslot
        Dim mnuWeeks As Windows.Forms.ToolStripMenuItem = mnuBookSameSpot.DropDownItems.Add("Weeks")
        Dim mnuWeekdays As Windows.Forms.ToolStripMenuItem = mnuBookSameSpot.DropDownItems.Add("Weekdays")
        Dim mnuBtnOK As Windows.Forms.ToolStripMenuItem = mnuBookSameSpot.DropDownItems.Add("OK")

        Dim mnuWeeks2 As Windows.Forms.ToolStripMenuItem = mnuBookSameSlot.DropDownItems.Add("Weeks")
        Dim mnuWeekdays2 As Windows.Forms.ToolStripMenuItem = mnuBookSameSlot.DropDownItems.Add("Weekdays")
        Dim mnuBtnOK2 As Windows.Forms.ToolStripMenuItem = mnuBookSameSlot.DropDownItems.Add("OK")

        'Maybe not neccessary
        mnuWeeks.DropDownItems.Clear()
        mnuWeekdays.DropDownItems.Clear()

        'For each week present in the bookingperiod, we add a drop down item to mnuWeekdays
        'We also add a handler for each item
        For Each TmpWeek In Campaign.Channels(1).BookingTypes(1).Weeks
            Dim TmpSubMenu As New Windows.Forms.ToolStripMenuItem
            TmpSubMenu.Checked = BookingFilter.Data("Week", TmpWeek.Name)
            TmpSubMenu.Text = TmpWeek.Name
            TmpSubMenu.Tag = TmpWeek
            AddHandler TmpSubMenu.Click, AddressOf mnuWeekMulti_Click
            mnuWeeks.DropDownItems.Add(TmpSubMenu)
        Next

        For i = 0 To 6
            Dim TmpSubMenu As New Windows.Forms.ToolStripMenuItem
            TmpSubMenu.Checked = BookingFilter.Data("Weekday", WeekDays(i))
            TmpSubMenu.Text = WeekDays(i)
            TmpSubMenu.Tag = i
            AddHandler TmpSubMenu.Click, AddressOf mnuWeekdayMulti_Click
            mnuWeekdays.DropDownItems.Add(TmpSubMenu)

        Next

        For Each TmpWeek In Campaign.Channels(1).BookingTypes(1).Weeks
            Dim TmpSubMenu As New Windows.Forms.ToolStripMenuItem
            TmpSubMenu.Checked = BookingFilter.Data("Week", TmpWeek.Name)
            TmpSubMenu.Text = TmpWeek.Name
            TmpSubMenu.Tag = TmpWeek
            AddHandler TmpSubMenu.Click, AddressOf mnuWeekMulti_Click
            mnuWeeks2.DropDownItems.Add(TmpSubMenu)
        Next

        For i = 0 To 6
            Dim TmpSubMenu As New Windows.Forms.ToolStripMenuItem
            TmpSubMenu.Checked = BookingFilter.Data("Weekday", WeekDays(i))
            TmpSubMenu.Text = WeekDays(i)
            TmpSubMenu.Tag = i
            AddHandler TmpSubMenu.Click, AddressOf mnuWeekdayMulti_Click
            mnuWeekdays2.DropDownItems.Add(TmpSubMenu)
        Next


        'mnuBtnOK.Checked = BookingFilter.Data("Program", grdSpotlist.CurrentRow.Cells("colSpotlistProgram").Value)
        mnuBtnOK.Text = "OK" 'grdSpotlist.CurrentRow.Cells("colSpotlistProgram").Value
        mnuBtnOK.Checked = ProgramFilterChecked ' BookingFilter.Data("Program", grdSpotlist.CurrentRow.Cells("colSpotlistProgram").Value.ToString.ToUpper.TrimEnd(trim))
        AddHandler mnuBtnOK.Click, AddressOf mnuBtnOKMulti_click

        mnuBtnOK2.Text = "OK" ' grdSpotlist.CurrentRow.Cells("colSpotlistProgram").Value
        mnuBtnOK2.Checked = TimeFilterChecked ' BookingFilter.Data("MaM", Trinity.Helper.Tid2Mam(grdSpotlist.CurrentRow.Cells("colSpotlistTime").Value))
        AddHandler mnuBtnOK2.Click, AddressOf mnuBtnOKMulti2_click

        'AddHandler mnuTransferToBooked.Click, AddressOf mnuBtnTransferToBooked_click

        mnuFilter.Show(MousePosition.X, MousePosition.Y)

    End Sub

    Private Sub grdSchedule_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdSchedule.MouseDown
        If Not e.Button = Windows.Forms.MouseButtons.Right Then
            Exit Sub
        End If
        Dim mnuBookSelected As New Windows.Forms.ContextMenuStrip
        Dim tmpMenuItem As New Windows.Forms.ToolStripMenuItem
        tmpMenuItem.Text = "Book all selected spots"
        mnuBookSelected.Items.Add(tmpMenuItem)
        mnuBookSelected.Show(MousePosition.X, MousePosition.Y)
        AddHandler tmpMenuItem.Click, AddressOf grdSchedule_BookMany


    End Sub


    Private Sub ToolStripTextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProgramNameFilter.TextChanged

        tmrFilter.Enabled = False
        tmrFilter.Enabled = True

    End Sub

    Private Sub ToolStripTextBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProgramNameFilter.Click

    End Sub

    Private Sub tmrFilter_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrFilter.Tick

        If txtProgramNameFilter.TextLength = 0 Then
            'MessageBox.Show(ToolStripTextBox1.Text)
            BookingFilter.AllTrue("RealtimeProgramFilter")
            UpdateSchedule(True, False)
            tmrFilter.Enabled = False
            Exit Sub
        End If

        BookingFilter.AllTrue("RealtimeProgramFilter")
        UpdateSchedule(True, False)

        For Each row As DataGridViewRow In grdSchedule.Rows
            Dim s As String = row.Cells("colProgram").Value
            BookingFilter.Data("RealtimeProgramFilter", s) = False
        Next

        For Each row As DataGridViewRow In grdSchedule.Rows
            If row.Cells("colProgram").Value.ToString.ToUpper.Contains(txtProgramNameFilter.Text.ToUpper) Then
                Dim s As String = row.Cells("colProgram").Value
                BookingFilter.Data("RealtimeProgramFilter", s) = True 'Not BookingFilter.Data("RealtimeProgramFilter", s)
            End If
        Next

        UpdateSchedule(True, False)
        tmrFilter.Enabled = False
    End Sub

    Private Sub grpReach_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpReach.Enter

    End Sub

    Private Sub ToolStripLabel1_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripLabel1.MouseHover
        Dim thisTooltip As New ToolTip
        thisTooltip.Show("Enter part of the name of the program(s) you want to find here", Me, MousePosition.X, MousePosition.Y, 3000)
    End Sub

    Private Sub grdReach_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdReach.CellContentDoubleClick
        If e.ColumnIndex > 0 Then
            Dim frmDetails As New frmRFEstimationBreaks(Campaign.RFEstimation.ReferencePeriods(e.ColumnIndex))
            frmDetails.ShowDialog()
        End If
    End Sub

    Private Function GetEventsTable(BookingType As Trinity.cBookingType) As DataTable
        Dim TmpType As Integer = 0
        If BookingType.IsPremium Then TmpType = 1

        Dim currentStartDate As Long = BookingType.Weeks(1).StartDate
        Dim lastEndDate As Long = currentStartDate - 1

        Dim _dates As New List(Of KeyValuePair(Of Date, Date))
        For _w As Integer = 1 To BookingType.Weeks.Count
            If Not lastEndDate + 1 = BookingType.Weeks(_w).StartDate Then
                _dates.Add(New KeyValuePair(Of Date, Date)(Date.FromOADate(currentStartDate), Date.FromOADate(lastEndDate)))
            End If
            lastEndDate = BookingType.Weeks(_w).EndDate
        Next
        _dates.Add(New KeyValuePair(Of Date, Date)(Date.FromOADate(currentStartDate), Date.FromOADate(lastEndDate)))

        Dim _table As System.Data.DataTable = DBReader.getEvents(_dates, BookingType.ParentChannel.ChannelName, TmpType)
        If _table.Rows.Count = 0 AndAlso TmpType = 1 Then
            _table = DBReader.getEvents(_dates, BookingType.ParentChannel.ChannelName, 0)
        End If
        Return _table
    End Function

    Private Sub cmdCheckForChanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheckForChanges.Click
        grdSpotlist.Invalidate()
    End Sub

    Private Sub PlannedToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PlannedToolStripMenuItem.Click

    End Sub

    Private Sub lblInfo_Resize(sender As Object, e As System.EventArgs) Handles lblInfo.Resize
        UpdatePanelPositions()
    End Sub

    Private Sub cmdImdb_Click(sender As System.Object, e As System.EventArgs) Handles cmdImdb.Click
        Process.Start(String.Format("http://www.imdb.com/find?q={0}", System.Web.HttpUtility.UrlEncode(cmdImdb.Tag)))
    End Sub

    Private Sub cmdReadK2Spotlist_Click(sender As Object, e As EventArgs) Handles cmdReadK2Spotlist.Click
        ReadK2Spotlist
    End Sub

    Private Sub grdSchedule_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdSchedule.CellContentClick

    End Sub

    Private Sub cmbSolusFreq_Click(sender As Object, e As EventArgs) Handles cmbSolusFreq.Click

    End Sub

    Private Sub grdSpotlist_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdSpotlist.CellContentClick

    End Sub

    Private Sub cmdScheduleFilter_Click(sender As Object, e As EventArgs) Handles cmdScheduleFilter.Click

    End Sub
End Class