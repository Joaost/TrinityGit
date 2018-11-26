Public Class frmEvaluateSpecifics

    Dim spotlistGrossPriceTotal As Double = 0
    Dim spotlistChanEstTotal As Double = 0
    Dim spotlistMyEstTotal As Double = 0
    Dim spotlistNetPriceTotal As Double = 0

    Private Sub cmdConfirmedColumns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConfirmedColumns.Click
        Dim Columns() As String = {"Date", "Time", "Channel", "Bookingtype", "Program", "Week", "Weekday", "Gross Price", "Net Price", "Filmcode", "Film", "Film dscr", "Position", "My Est", "Chan Est", "Actual", "Gross CPP", "CPP (" & Campaign.MainTarget.TargetNameNice & ")", "CPP (Chn Est)", "Quality", "Remarks", "Notes", "Added value"}
        Dim i As Integer
        Dim j As Integer
        Dim FoundIt As Boolean
        Dim TmpNode As System.Windows.Forms.TreeNode
        Dim TmpCol As Windows.Forms.DataGridViewColumn

        frmColumns.tvwChosen.Nodes.Clear()
        TmpCol = grdConfirmed.Columns.GetFirstColumn(Windows.Forms.DataGridViewElementStates.None)

        While Not TmpCol Is Nothing
            If TmpCol.Visible Then
                frmColumns.tvwChosen.Nodes.Add(CreateGUID, TmpCol.HeaderText)
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
            TmpNode = frmColumns.tvwChosen.Nodes(1)
            While Not TmpNode.PrevNode Is Nothing
                TmpNode = TmpNode.PrevNode
            End While
            i = 1
            While Not TmpNode Is Nothing
                TrinitySettings.ConfirmedColumn(i) = TmpNode.Text
                i = i + 1
                TmpNode = TmpNode.NextNode
            End While
            UpdateConfirmed()
        End If
    End Sub


    Private Sub cmdBookingtype_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBookingtype.DropDownOpening
        Dim TmpBT As Trinity.cBookingType
        If grdConfirmed.SelectedRows.Count = 0 Then
            Exit Sub
        End If
        cmdBookingtype.DropDownItems.Clear()
        For Each TmpBT In Campaign.Channels(1).BookingTypes
            With DirectCast(cmdBookingtype.DropDownItems.Add(TmpBT.Name), System.Windows.Forms.ToolStripMenuItem)
                If TmpBT.Name = Campaign.PlannedSpots(grdConfirmed.SelectedRows.Item(0).Tag).Bookingtype.Name Then
                    .Checked = True
                End If
                AddHandler .Click, AddressOf ChangeBookingtype
            End With
        Next
    End Sub

    Sub ChangeBookingtype(ByVal sender As Object, ByVal e As EventArgs)

        Dim TmpRow As Windows.Forms.DataGridViewRow

        If grdConfirmed.SelectedRows.Count = 0 Then
            Windows.Forms.MessageBox.Show("No spot chosen.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        For Each TmpRow In grdConfirmed.SelectedRows
            Campaign.PlannedSpots(TmpRow.Tag).Bookingtype = Campaign.PlannedSpots(TmpRow.Tag).Channel.BookingTypes(sender.text)
            If Not Campaign.PlannedSpots(TmpRow.Tag).MatchedSpot Is Nothing Then
                Campaign.PlannedSpots(TmpRow.Tag).MatchedSpot.Bookingtype = Campaign.PlannedSpots(TmpRow.Tag).Channel.BookingTypes(sender.text)
            End If
        Next

        grdConfirmed.Invalidate()
        grdSpotlist.Invalidate()

    End Sub

    Sub ChangeFilm(ByVal sender As Object, ByVal e As EventArgs)

        Dim TmpRow As Windows.Forms.DataGridViewRow

        If grdConfirmed.SelectedRows.Count = 0 Then
            Windows.Forms.MessageBox.Show("No spot chosen.", "TRINITY", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        For Each TmpRow In grdConfirmed.SelectedRows
            Campaign.PlannedSpots(TmpRow.Tag).Filmcode = Campaign.PlannedSpots(TmpRow.Tag).Week.Films(sender.text).Filmcode
        Next

        grdConfirmed.Invalidate()
        grdSpotlist.Invalidate()

    End Sub

    Private Sub cmdFilm_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFilm.DropDownOpening
        Dim TmpFilm As Trinity.cFilm
        If grdConfirmed.SelectedRows.Count = 0 Then
            Exit Sub
        End If
        cmdFilm.DropDownItems.Clear()
        For Each TmpFilm In Campaign.Channels(1).BookingTypes(1).Weeks(1).Films
            With DirectCast(cmdFilm.DropDownItems.Add(TmpFilm.Name), System.Windows.Forms.ToolStripMenuItem)
                If TmpFilm.Name = Campaign.PlannedSpots(grdConfirmed.SelectedRows.Item(0).Tag).Film.Name Then
                    .Checked = True
                End If
                AddHandler .Click, AddressOf ChangeFilm
            End With
        Next
    End Sub

    Private Sub cmdSpotlistColumns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSpotlistColumns.Click
        Dim i As Integer
        Dim j As Integer
        Dim FoundIt As Boolean
        Dim TmpNode As Windows.Forms.TreeNode
        Dim Chan As String
        Dim BT As String
        Dim TmpCol As Windows.Forms.DataGridViewColumn

        Chan = cmbChannel.Text.Substring(0, InStr(cmbChannel.Text, " ") - 1)
        BT = Mid(cmbChannel.Text, InStr(cmbChannel.Text, " ") + 1)

        Dim Columns() As String = {"Date", "Time", "Channel", "Program", "Week", "Weekday", "Gross Price", "Net Price", "Filmcode", "Film", "Film dscr", "Position", "Est", "Est (" & Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.TargetName & ")", "Chan Est", "Gross CPP", "CPP (" & Campaign.MainTarget.TargetNameNice & ")", "CPP (Chn Est)", "Quality", "Remarks", "Notes", "Added value"}

        frmColumns.tvwChosen.Nodes.Clear()
        TmpCol = grdSpotlist.Columns.GetFirstColumn(Windows.Forms.DataGridViewElementStates.None)
        While Not TmpCol Is Nothing
            If Not TmpCol.Tag Is Nothing And TmpCol.Visible Then
                frmColumns.tvwChosen.Nodes.Add(CreateGUID, TmpCol.Tag)
            End If
            TmpCol = grdSpotlist.Columns.GetNextColumn(TmpCol, Windows.Forms.DataGridViewElementStates.None, Windows.Forms.DataGridViewElementStates.None)
        End While
        frmColumns.tvwAvailable.Nodes.Clear()
        For j = 0 To 21
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
        If Spot.Week Is Nothing Then Return False
        If Not GeneralFilter.Data("Film", Spot.Week.Films(Spot.Filmcode).Name) Then
            Return False
        End If
        If Not GeneralFilter.Data("Week", Spot.Week.Name) Then
            Return False
        End If
        Return True
    End Function

    Function FilterIn(ByVal Spot As Trinity.cBookedSpot) As Boolean
        Dim WeekDays() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}
        If Not GeneralFilter.Data("Channels", Spot.Channel.ChannelName) Then
            Return False
        End If
        If Not GeneralFilter.Data("Bookingtype", Spot.Bookingtype.Name) Then
            Return False
        End If
        If Not GeneralFilter.Data("Weekday", WeekDays(Weekday(Spot.AirDate, FirstDayOfWeek.Monday) - 1)) Then
            Return False
        End If
        If Not GeneralFilter.Data("Daypart", Campaign.Dayparts.GetDaypartForMam(Spot.MaM).Name) Then
            Return False
        End If
        If Spot.week Is Nothing OrElse Spot.week.Films(Spot.Filmcode) Is Nothing OrElse Not GeneralFilter.Data("Film", Spot.week.Films(Spot.Filmcode).Name) Then
            Return False
        End If
        If Not GeneralFilter.Data("Week", Spot.Week.Name) Then
            Return False
        End If
        Return True
    End Function

    Sub UpdateConfirmed()

        Dim List As New List(Of Trinity.cPlannedSpot)

        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        grdConfirmed.Columns.Clear()
        grdConfirmed.Rows.Clear()
        grdConfirmed.Columns.Add("colConfirmedDateTimeSerial", "DateTimeSerial")
        grdConfirmed.Columns("colConfirmedDateTimeSerial").Tag = "DateTimeSerial"
        grdConfirmed.Columns("colConfirmedDateTimeSerial").Visible = False
        For c As Integer = 1 To TrinitySettings.ConfirmedColumnCount
            grdConfirmed.Columns.Add("colConfirmed" & TrinitySettings.ConfirmedColumn(c), TrinitySettings.ConfirmedColumn(c))
            If TrinitySettings.ConfirmedColumnWidth(c) > 0 Then
                grdConfirmed.Columns("colConfirmed" & TrinitySettings.ConfirmedColumn(c)).Width = TrinitySettings.ConfirmedColumnWidth(c)
            ElseIf TrinitySettings.ConfirmedColumnWidth(c) = 0 Then
                grdConfirmed.Columns("colConfirmed" & TrinitySettings.ConfirmedColumn(c)).Visible = False
            Else
                grdConfirmed.Columns("colConfirmed" & TrinitySettings.ConfirmedColumn(c)).Width = 100
            End If
            grdConfirmed.Columns("colConfirmed" & TrinitySettings.ConfirmedColumn(c)).SortMode = Windows.Forms.DataGridViewColumnSortMode.Programmatic
        Next
        For Each TmpSpot As Trinity.cPlannedSpot In Campaign.PlannedSpots
            If TmpSpot.Bookingtype Is cmbChannel.SelectedItem Then
                If FilterIn(TmpSpot) Then
                    List.Add(TmpSpot)
                End If
            End If
        Next
        Quicksort(List, 0, List.Count - 1)
        For i As Integer = 0 To List.Count - 1
            grdConfirmed.Rows.Add()
            If Not List(i).MatchedBookedSpot Is Nothing Then
                grdConfirmed.Rows(grdConfirmed.Rows.Count - 1).DefaultCellStyle.ForeColor = Drawing.Color.Blue
            End If
            grdConfirmed.Rows(grdConfirmed.Rows.Count - 1).Tag = List(i).ID
            If grdConfirmed.Columns.Contains("colConfirmedGross Price") Then
                If grdConfirmed.Columns("colConfirmedGross Price").Tag Is Nothing OrElse grdConfirmed.Columns("colConfirmedGross Price").Tag.ToString = "" Then grdConfirmed.Columns("colConfirmedGross Price").Tag = 0
                grdConfirmed.Columns("colConfirmedGross Price").Tag = grdConfirmed.Columns("colConfirmedGross Price").Tag + List(i).PriceGross
            End If
            If grdConfirmed.Columns.Contains("colConfirmedNet Price") Then
                If grdConfirmed.Columns("colConfirmedNet Price").Tag Is Nothing OrElse grdConfirmed.Columns("colConfirmedNet Price").Tag.ToString = "" Then grdConfirmed.Columns("colConfirmedNet Price").Tag = 0
                grdConfirmed.Columns("colConfirmedNet Price").Tag = grdConfirmed.Columns("colConfirmedNet Price").Tag + List(i).PriceNet
            End If
            If grdConfirmed.Columns.Contains("colConfirmedChan Est") Then
                If grdConfirmed.Columns("colConfirmedChan Est").Tag Is Nothing OrElse grdConfirmed.Columns("colConfirmedChan Est").Tag.ToString = "" Then grdConfirmed.Columns("colConfirmedChan Est").Tag = 0
                grdConfirmed.Columns("colConfirmedChan Est").Tag = grdConfirmed.Columns("colConfirmedChan Est").Tag + List(i).ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
            End If
            If grdConfirmed.Columns.Contains("colConfirmedMy Est") Then
                If grdConfirmed.Columns("colConfirmedMy Est").Tag Is Nothing OrElse grdConfirmed.Columns("colConfirmedMy Est").Tag.ToString = "" Then grdConfirmed.Columns("colConfirmedMy Est").Tag = 0
                grdConfirmed.Columns("colConfirmedMy Est").Tag = grdConfirmed.Columns("colConfirmedMy Est").Tag + List(i).MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget)
            End If
        Next
        grdConfirmed.Rows.Add()
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Sub UpdateSpotlist()
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        If cmbChannel.SelectedIndex = -1 Then Exit Sub

        Dim List As New List(Of Trinity.cBookedSpot)
        Dim Chan As String = cmbChannel.Text.Substring(0, InStr(cmbChannel.Text, " ") - 1)
        Dim BT As String = Mid(cmbChannel.Text, InStr(cmbChannel.Text, " ") + 1)

        grdSpotlist.ReadOnly = False
        grdSpotlist.Columns.Clear()
        grdSpotlist.Rows.Clear()
        grdSpotlist.Columns.Add("colSpotlistID", "ID")
        grdSpotlist.Columns("colSpotlistID").Tag = "ID"
        grdSpotlist.Columns("colSpotlistID").Visible = False
        grdSpotlist.Columns.Add("colSpotlistDateTimeSerial", "DateTimeSerial")
        grdSpotlist.Columns("colSpotlistDateTimeSerial").Tag = "DateTimeSerial"
        grdSpotlist.Columns("colSpotlistDateTimeSerial").Visible = False

        For c As Integer = 1 To TrinitySettings.SpotlistColumnCount
            grdSpotlist.Columns.Add("colSpotlist" & TrinitySettings.SpotlistColumn(c), TrinitySettings.SpotlistColumn(c))
            If TrinitySettings.SpotlistColumnWidth(c) > 0 Then
                grdSpotlist.Columns("colSpotlist" & TrinitySettings.SpotlistColumn(c)).Width = TrinitySettings.SpotlistColumnWidth(c)
            ElseIf TrinitySettings.SpotlistColumnWidth(c) = 0 Then
                grdSpotlist.Columns("colSpotlist" & TrinitySettings.SpotlistColumn(c)).Visible = False
            Else
                grdSpotlist.Columns("colSpotlist" & TrinitySettings.SpotlistColumn(c)).Width = 100
            End If
            'Hannes added
            grdSpotlist.Columns("colSpotlist" & TrinitySettings.SpotlistColumn(c)).Tag = TrinitySettings.SpotlistColumn(c)
        Next

        For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
            If TmpSpot.Bookingtype Is cmbChannel.SelectedItem Then
                If FilterIn(TmpSpot) Then
                    List.Add(TmpSpot)
                End If
            End If
        Next

        Quicksort(List, 0, List.Count - 1)

        For i As Integer = 0 To List.Count - 1
            grdSpotlist.Rows.Add()
            grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Tag = List(i).ID
            If Not List(i).MatchedSpot Is Nothing Then
                grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).DefaultCellStyle.ForeColor = Drawing.Color.Blue
            End If
            'If grdSpotlist.Columns.Contains("colSpotlistGross Price") Then
            '    If grdSpotlist.Columns("colSpotlistGross Price").Tag Is Nothing OrElse grdSpotlist.Columns("colSpotlistGross Price").Tag.ToString = "" Then grdSpotlist.Columns("colSpotlistGross Price").Tag = 0
            '    grdSpotlist.Columns("colSpotlistGross Price").Tag = grdSpotlist.Columns("colSpotlistGross Price").Tag + List(i).GrossPrice * List(i).AddedValueIndex(False)
            'End If
            'If grdSpotlist.Columns.Contains("colSpotlistNet Price") Then
            '    If grdSpotlist.Columns("colSpotlistNet Price").Tag Is Nothing OrElse grdSpotlist.Columns("colSpotlistNet Price").Tag.ToString = "" Then grdSpotlist.Columns("colSpotlistNet Price").Tag = 0
            '    grdSpotlist.Columns("colSpotlistNet Price").Tag = grdSpotlist.Columns("colSpotlistNet Price").Tag + List(i).NetPrice * List(i).AddedValueIndex
            'End If
            'If grdSpotlist.Columns.Contains("colSpotlistChan Est") Then
            '    If grdSpotlist.Columns("colSpotlistChan Est").Tag Is Nothing OrElse grdSpotlist.Columns("colSpotlistChan Est").Tag.ToString = "" Then grdSpotlist.Columns("colSpotlistChan Est").Tag = 0
            '    grdSpotlist.Columns("colSpotlistChan Est").Tag = grdSpotlist.Columns("colSpotlistChan Est").Tag + List(i).ChannelEstimate
            'End If
            'If grdSpotlist.Columns.Contains("colSpotlistEst") Then
            '    If grdSpotlist.Columns("colSpotlistEst").Tag Is Nothing OrElse grdSpotlist.Columns("colSpotlistEst").Tag.ToString = "" Then grdSpotlist.Columns("colSpotlistEst").Tag = 0
            '    grdSpotlist.Columns("colSpotlistEst").Tag = grdSpotlist.Columns("colSpotlistEst").Tag + List(i).MyEstimate
            'End If
        Next
        grdSpotlist.Rows.Add()

        spotlistGrossPriceTotal = 0
        For i As Integer = 0 To grdSpotlist.Rows.Count - 2
            If grdSpotlist.Rows(i).Visible Then spotlistGrossPriceTotal += CDbl(Trinity.Helper.RemoveNonNumbersFromString(grdSpotlist.Rows(i).Cells("colSpotlistGross Price").Value))
        Next

        spotlistNetPriceTotal = 0
        For i As Integer = 0 To grdSpotlist.Rows.Count - 2
            If grdSpotlist.Rows(i).Visible Then spotlistNetPriceTotal += CDbl(Trinity.Helper.RemoveNonNumbersFromString(grdSpotlist.Rows(i).Cells("colSpotlistNet Price").Value))
        Next

        spotlistChanEstTotal = 0
        For i As Integer = 0 To grdSpotlist.Rows.Count - 2
            If grdSpotlist.Rows(i).Visible Then spotlistChanEstTotal += CDbl(Trinity.Helper.RemoveNonNumbersFromString(grdSpotlist.Rows(i).Cells("colSpotlistChan Est").Value))
        Next

        spotlistMyEstTotal = 0
        For i As Integer = 0 To grdSpotlist.Rows.Count - 2
            If grdSpotlist.Rows(i).Visible Then spotlistMyEstTotal += CDbl(Trinity.Helper.RemoveNonNumbersFromString(grdSpotlist.Rows(i).Cells("colSpotlistEst").Value))
        Next


    End Sub

    Private Sub grdConfirmed_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdConfirmed.CellClick

        For Each row As Windows.Forms.DataGridViewRow In grdSpotlist.Rows
            row.Selected = False
        Next

        Dim ClickedRow As Windows.Forms.DataGridViewRow = grdConfirmed.Rows(e.RowIndex)
        Dim ConfirmedAirDate As DateTime = Date.FromOADate(ClickedRow.Cells("colConfirmedDateTimeSerial").Value)

        For Each RowToSelect As Windows.Forms.DataGridViewRow In grdSpotlist.Rows
            Dim SpotlistAirDate As DateTime = Date.FromOADate(RowToSelect.Cells("colSpotlistDateTimeSerial").Value)
            If Math.Abs(DateDiff(DateInterval.Minute, ConfirmedAirDate, SpotlistAirDate)) < 10 Then
                grdSpotlist.FirstDisplayedScrollingRowIndex = RowToSelect.Index
                RowToSelect.Selected = True
            End If
        Next

    End Sub

    Private Sub grdConfirmed_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdConfirmed.CellValueNeeded
        If e.RowIndex = grdConfirmed.Rows.Count - 1 Then
            If grdConfirmed.Columns(e.ColumnIndex).Name = "colConfirmedGross Price" Then
                e.Value = Format(grdConfirmed.Columns("colConfirmedGross Price").Tag, "##,##0 kr")
            ElseIf grdConfirmed.Columns(e.ColumnIndex).Name = "colConfirmedNet Price" Then
                e.Value = Format(grdConfirmed.Columns("colConfirmedNet Price").Tag, "##,##0 kr")
            ElseIf grdConfirmed.Columns(e.ColumnIndex).Name = "colConfirmedChan Est" Then
                e.Value = Format(grdConfirmed.Columns("colConfirmedChan Est").Tag, "N1")
            ElseIf grdConfirmed.Columns(e.ColumnIndex).Name = "colConfirmedMy Est" Then
                e.Value = Format(grdConfirmed.Columns("colConfirmedMy Est").Tag, "N1")
            ElseIf e.ColumnIndex = 1 Then
                e.Value = "Sum:"
            Else
                e.Value = ""
            End If
            grdConfirmed.Rows(e.RowIndex).DefaultCellStyle.BackColor = Drawing.Color.Gray
            Exit Sub
        End If

        Dim TmpSpot As Trinity.cPlannedSpot = Campaign.PlannedSpots(grdConfirmed.Rows(e.RowIndex).Tag)
        Dim TmpCol As Windows.Forms.DataGridViewColumn = grdConfirmed.Columns(e.ColumnIndex)
        Dim Days() As String = {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"}

        Dim Est As Single = TmpSpot.MyRating
        Dim EstBT As Single = TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
        Select Case TmpCol.HeaderText
            Case "ID"
                e.Value = TmpSpot.ID
            Case "DateTimeSerial"
                e.Value = Trinity.Helper.DateTimeSerial(Date.FromOADate(TmpSpot.AirDate), TmpSpot.MaM)
            Case "Date"
                e.Value = Format(Date.FromOADate(TmpSpot.AirDate), "Short Date")
            Case "Week"
                e.Value = DatePart(DateInterval.WeekOfYear, Date.FromOADate(TmpSpot.AirDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
            Case "Weekday"
                e.Value = Days(Weekday(Date.FromOADate(TmpSpot.AirDate), vbMonday) - 1)
            Case "Time"
                e.Value = Trinity.Helper.Mam2Tid(TmpSpot.MaM)
            Case "Channel"
                e.Value = TmpSpot.Channel.Shortname
            Case "Bookingtype"
                e.Value = TmpSpot.Bookingtype.Name
            Case "Program"
                e.Value = TmpSpot.Programme
            Case "Gross Price"
                e.Value = Format(TmpSpot.PriceGross, "##,##0 kr")
                If Not TmpSpot.MatchedBookedSpot Is Nothing Then
                    Dim TmpBooked As Trinity.cBookedSpot = TmpSpot.MatchedBookedSpot
                    If Format(TmpSpot.PriceGross, "0") <> Format(TmpBooked.GrossPrice * TmpBooked.AddedValueIndex(False), "0") Then
                        grdConfirmed.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Red
                    Else
                        grdConfirmed.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Blue
                    End If
                End If
            Case "Chan Est"
                e.Value = Format(TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "0.0")
                If Not TmpSpot.MatchedBookedSpot Is Nothing Then
                    Dim TmpBooked As Trinity.cBookedSpot = TmpSpot.MatchedBookedSpot
                    If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) <> TmpBooked.ChannelEstimate Then
                        grdConfirmed.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Red
                    Else
                        grdConfirmed.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Blue
                    End If
                End If
            Case "Net Price"
                e.Value = Format(TmpSpot.PriceNet, "##,##0 kr")
                If Not TmpSpot.MatchedBookedSpot Is Nothing Then
                    Dim TmpBooked As Trinity.cBookedSpot = TmpSpot.MatchedBookedSpot
                    If Format(TmpSpot.PriceNet, "0") <> Format(TmpBooked.NetPrice * TmpBooked.AddedValueIndex, "0") Then
                        grdConfirmed.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Red
                    Else
                        grdConfirmed.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Blue
                    End If
                End If
            Case "Remarks"
                If TmpSpot.Remark = "L" Then
                    e.Value = "Local"
                End If
            Case "Gross CPP"
                If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                    e.Value = Format(TmpSpot.PriceNet / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "##,##0 kr")
                Else
                    e.Value = "0 kr"
                End If
            Case "CPP (" & Campaign.MainTarget.TargetNameNice & ")"
                If Est > 0 Then
                    e.Value = Format(TmpSpot.PriceNet / Est, "##,##0 kr")
                Else
                    e.Value = "0 kr"
                End If
            Case "CPP (Chn Est)"
                If TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                    e.Value = Format(TmpSpot.PriceNet / TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "##,##0 kr")
                Else
                    e.Value = "0 kr"
                End If
            Case "My Est"
                e.Value = Format(TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget), "N1")
                If Not TmpSpot.MatchedBookedSpot Is Nothing Then
                    Dim TmpBooked As Trinity.cBookedSpot = TmpSpot.MatchedBookedSpot
                    If TmpSpot.MyRating <> TmpBooked.MyEstimate Then
                        grdConfirmed.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Red
                    Else
                        grdConfirmed.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Blue
                    End If
                End If
            Case "Actual"
                If Not TmpSpot.MatchedSpot Is Nothing Then
                    e.Value = Format(TmpSpot.MatchedSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget), "N1")
                End If
            Case "Filmcode"
                e.Value = TmpSpot.Filmcode
                If Not TmpSpot.MatchedBookedSpot Is Nothing Then
                    Dim TmpBooked As Trinity.cBookedSpot = TmpSpot.MatchedBookedSpot
                    If TmpSpot.Filmcode <> TmpBooked.Filmcode Then
                        grdConfirmed.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Red
                    Else
                        grdConfirmed.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Blue
                    End If
                End If
            Case "Film"
                If TmpSpot.Film Is Nothing Then
                    e.Value = "<Unknown>"
                Else
                    e.Value = TmpSpot.Film.Name
                End If
            Case "Film dscr"
                e.Value = TmpSpot.Film.Description
            Case "Added value"
                If TmpSpot.AddedValue IsNot Nothing Then
                    e.Value = TmpSpot.AddedValue.Name
                End If
        End Select

    End Sub

    Private Sub frmEvaluateSpecifics_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        cmbChannel.Items.Clear()

        'Just add the channels in the campaign that have specifics to the listbox
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                If TmpBT.BookIt AndAlso TmpBT.IsSpecific Then
                    cmbChannel.Items.Add(TmpBT)
                End If
            Next
        Next

        Dim count As Integer = 0
        Dim count2 As Integer = 0
        'Go through all spots that have been booked in Booking
        For Each TmpBookedSpot As Trinity.cBookedSpot In Campaign.BookedSpots
            count += 1
            count2 = 1
            'If the booked spot has not been matched to any spots then
            If TmpBookedSpot.MatchedSpot Is Nothing Then
                'Check all the spots that have been confirmed by the channels
                For Each TmpPlannedspot As Trinity.cPlannedSpot In Campaign.PlannedSpots
                    count2 += 1
                    'If the confirmed spot does not have a booked spot which it is matched against, then
                    If TmpPlannedspot.MatchedBookedSpot Is Nothing Then
                        'If the date of the booked and planned spots are the same, then
                        If TmpBookedSpot.AirDate.ToOADate = TmpPlannedspot.AirDate Then
                            'If the confirmed spot is plus or minus 10 mins from the booked airtime, then
                            If TmpBookedSpot.MaM >= TmpPlannedspot.MaM - 10 And TmpBookedSpot.MaM <= TmpPlannedspot.MaM + 10 Then
                                'If the booking type on the confirmed and booked spot are the same, then
                                If TmpBookedSpot.Bookingtype Is TmpPlannedspot.Bookingtype Then
                                    'Make this confirmed spot match the one in booking
                                    TmpBookedSpot.MatchedSpot = TmpPlannedspot
                                    'Make the confirmed spot match this booked spot
                                    TmpPlannedspot.MatchedBookedSpot = TmpBookedSpot
                                    'And transfer the estimate from the booked spot to this confirmed spot
                                    TmpPlannedspot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = TmpBookedSpot.MyEstimate
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        Next
        cmbChannel.SelectedIndex = 0
    End Sub

    Private Sub cmbChannel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbChannel.SelectedIndexChanged
        UpdateSpotlist()
        UpdateConfirmed()
    End Sub

    Private Sub Quicksort(ByVal List As List(Of Trinity.cPlannedSpot), ByVal min As Integer, ByVal max As Integer)
        Dim med_value As Trinity.cPlannedSpot
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
            Do While List(hi).DateTimeSerial >= med_value.DateTimeSerial
                hi = hi - 1
                If hi <= lo Then Exit Do
            Loop
            If hi <= lo Then
                List(lo) = med_value
                Exit Do
            End If

            ' Swap the lo and hi values.
            List(lo) = List(hi)

            ' Look up from lo for a value >= med_value.
            lo = lo + 1
            Do While List(lo).DateTimeSerial < med_value.DateTimeSerial
                lo = lo + 1
                If lo >= hi Then Exit Do
            Loop
            If lo >= hi Then
                lo = hi
                List(hi) = med_value
                Exit Do
            End If

            ' Swap the lo and hi values.
            List(hi) = List(lo)
        Loop

        ' Sort the two sublists
        Quicksort(List, min, lo - 1)
        Quicksort(List, lo + 1, max)

    End Sub

    Private Sub Quicksort(ByVal List As List(Of Trinity.cBookedSpot), ByVal min As Integer, ByVal max As Integer)
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
            Do While List(hi).DateTimeSerial >= med_value.DateTimeSerial
                hi = hi - 1
                If hi <= lo Then Exit Do
            Loop
            If hi <= lo Then
                List(lo) = med_value
                Exit Do
            End If

            ' Swap the lo and hi values.
            List(lo) = List(hi)

            ' Look up from lo for a value >= med_value.
            lo = lo + 1
            Do While List(lo).DateTimeSerial < med_value.DateTimeSerial
                lo = lo + 1
                If lo >= hi Then Exit Do
            Loop
            If lo >= hi Then
                lo = hi
                List(hi) = med_value
                Exit Do
            End If

            ' Swap the lo and hi values.
            List(hi) = List(lo)
        Loop

        ' Sort the two sublists
        Quicksort(List, min, lo - 1)
        Quicksort(List, lo + 1, max)

    End Sub

    Private Sub grdSpotlist_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSpotlist.CellClick

    End Sub

    Private Sub grdSpotlist_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSpotlist.CellValueNeeded
        Dim TmpBT As Trinity.cBookingType = cmbChannel.SelectedItem
        Dim Days() As String = {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"}
        Dim TmpCol As Windows.Forms.DataGridViewColumn = grdSpotlist.Columns(e.ColumnIndex)
        If e.RowIndex = grdSpotlist.Rows.Count - 1 Then
            Select Case TmpCol.HeaderText
                Case "Gross Price"

                    e.Value = Format(spotlistGrossPriceTotal, "##,##0 kr")
                    'e.Value = Format(TmpCol.Tag, "##,##0 kr")
                Case "Chan Est"
                    'e.Value = Format(TmpCol.Tag, "N1")
                    e.Value = Format(spotlistChanEstTotal, "N1")
                Case "Net Price"
                    ' e.Value = Format(TmpCol.Tag, "##,##0 kr")
                    e.Value = Format(spotlistNetPriceTotal, "##,##0 kr")
                Case "Est"
                    ' e.Value = Format(TmpCol.Tag, "N1")
                    e.Value = Format(spotlistMyEstTotal, "N1")
                Case "Est (" & TmpBT.BuyingTarget.TargetName & ")"
                    e.Value = Format(TmpCol.Tag, "0.0")
            End Select
            If e.ColumnIndex = 2 Then
                e.Value = "Sum:"
            End If
            grdSpotlist.Rows(e.RowIndex).DefaultCellStyle.BackColor = Drawing.Color.Gray
            Exit Sub
        End If
        Dim TmpSpot As Trinity.cBookedSpot = Campaign.BookedSpots(grdSpotlist.Rows(e.RowIndex).Tag)

        If TmpSpot Is Nothing Then Exit Sub

        If TmpCol.HeaderText = "ID" Then
            e.Value = TmpSpot.ID
        ElseIf TmpCol.HeaderText = "DateTimeSerial" Then
            e.Value = Trinity.Helper.DateTimeSerial(TmpSpot.AirDate, TmpSpot.MaM)
        ElseIf TmpCol.Visible = True Then
            Dim Est As Single = TmpSpot.MyEstimate
            Dim EstBT As Single = TmpSpot.MyEstimateBuyTarget
            Select Case TmpCol.HeaderText
                Case "Date"
                    e.Value = Format(TmpSpot.AirDate, "Short Date")
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
                Case "Week"
                    e.Value = DatePart(DateInterval.WeekOfYear, TmpSpot.AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
                Case "Weekday"
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
                    e.Value = Days(Weekday(TmpSpot.AirDate, vbMonday) - 1)
                Case "Time"
                    e.Value = Trinity.Helper.Mam2Tid(TmpSpot.MaM)
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
                Case "Channel"
                    e.Value = TmpSpot.Channel.Shortname
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
                Case "Program"
                    e.Value = TmpSpot.Programme
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
                Case "Gross Price"
                    e.Value = Format(TmpSpot.GrossPrice * TmpSpot.AddedValueIndex(False), "##,##0 kr")
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = False
                    If Not TmpSpot.MatchedSpot Is Nothing Then
                        Dim TmpBooked As Trinity.cPlannedSpot = TmpSpot.MatchedSpot
                        If Format(TmpSpot.GrossPrice * TmpSpot.AddedValueIndex(False), "0") <> Format(TmpBooked.PriceGross, "0") Then
                            grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Red
                        Else
                            grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Blue
                        End If
                    End If
                Case "Chan Est"
                    e.Value = Format(TmpSpot.ChannelEstimate, "0.0")
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = False
                    If Not TmpSpot.MatchedSpot Is Nothing Then
                        Dim TmpBooked As Trinity.cPlannedSpot = TmpSpot.MatchedSpot
                        If TmpSpot.ChannelEstimate <> TmpBooked.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) Then
                            grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Red
                        Else
                            grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Blue
                        End If
                    End If
                Case "Net Price"
                    e.Value = Format(TmpSpot.NetPrice * TmpSpot.AddedValueIndex, "##,##0 kr")
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = False
                    If Not TmpSpot.MatchedSpot Is Nothing Then
                        Dim TmpBooked As Trinity.cPlannedSpot = TmpSpot.MatchedSpot
                        If Format(TmpSpot.NetPrice * TmpSpot.AddedValueIndex, "0") <> Format(TmpBooked.PriceNet, "0") Then
                            grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Red
                        Else
                            grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Blue
                        End If
                    End If
                Case "Notes"
                    e.Value = TmpSpot.Comments
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = False
                Case "Remarks"
                    If TmpSpot.IsLocal Then
                        e.Value = "Local"
                    End If
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
                Case "Gross CPP"
                    If TmpSpot.ChannelEstimate > 0 Then
                        e.Value = Format(TmpSpot.NetPrice / TmpSpot.ChannelEstimate, "##,##0 kr")
                    Else
                        e.Value = "0 kr"
                    End If
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
                Case "CPP (" & Campaign.MainTarget.TargetNameNice & ")"
                    If Est > 0 Then
                        e.Value = Format(TmpSpot.NetPrice / Est, "##,##0 kr")
                    Else
                        e.Value = "0 kr"
                    End If
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
                Case "CPP (Chn Est)"
                    If TmpSpot.ChannelEstimate > 0 Then
                        e.Value = Format(TmpSpot.NetPrice / TmpSpot.ChannelEstimate, "##,##0 kr")
                    Else
                        e.Value = "0 kr"
                    End If
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
                Case "Est"
                    e.Value = Format(TmpSpot.MyEstimate, "0.0")
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = False
                Case "Est (" & TmpBT.BuyingTarget.TargetName & ")"
                    e.Value = Format(EstBT, "0.0")
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = False
                Case "Filmcode"
                    e.Value = TmpBT.Weeks(1).Films(TmpSpot.Filmcode).Filmcode
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
                    If Not TmpSpot.MatchedSpot Is Nothing Then
                        Dim TmpBooked As Trinity.cPlannedSpot = TmpSpot.MatchedSpot
                        If TmpSpot.Filmcode <> TmpBooked.Filmcode Then
                            grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Red
                        Else
                            grdSpotlist.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Drawing.Color.Blue
                        End If
                    End If
                Case "Film"
                    If TmpBT.Weeks(1).Films(TmpSpot.Filmcode) Is Nothing Then
                        e.Value = "<unknown>"
                    Else
                        e.Value = TmpBT.Weeks(1).Films(TmpSpot.Filmcode).Name
                    End If
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
                Case "Film dscr"
                    e.Value = TmpBT.Weeks(1).Films(TmpSpot.Filmcode).Description
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
                Case "Added value"
                    Dim TmpStr As String = ""
                    For Each kv As KeyValuePair(Of String, Trinity.cAddedValue) In TmpSpot.AddedValues
                        Try
                            TmpStr = TmpStr + kv.Value.Name & "/"
                        Catch
                            TmpStr = "Unknown added value"
                        End Try
                    Next
                    TmpStr = TmpStr.TrimEnd("/")
                    e.Value = TmpStr
                    grdSpotlist.Rows(grdSpotlist.Rows.Count - 1).Cells(TmpCol.Name).ReadOnly = True
            End Select
        End If

    End Sub

    Private Sub grdSpotlist_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdSpotlist.CellValuePushed

    End Sub


    Private Sub cmdImportSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImportSchedule.Click
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        ImportSchedule()
        UpdateConfirmed()
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAccept.Click
        If Windows.Forms.MessageBox.Show("This will replace all booked spots." & vbCrLf & vbCrLf & "Are you sure you want to proceed?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then Exit Sub
        Select Case Windows.Forms.MessageBox.Show("Do you want to save the campaign to history before you proceed?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question)
            Case Windows.Forms.DialogResult.Cancel
                Exit Sub
            Case Windows.Forms.DialogResult.Yes
                Campaign.SaveCampaignToHistory(InputBox("Comment:", "T R I N I T Y"))
        End Select
        Dim TmpBT As Trinity.cBookingType = cmbChannel.SelectedItem
        Dim LowDate As Long = 9999999
        Dim HighDate As Long = 0
        For Each TmpSpot As Trinity.cPlannedSpot In Campaign.PlannedSpots

            If TmpSpot.Bookingtype Is TmpBT Then

                If TmpSpot.AirDate < LowDate Then
                    LowDate = TmpSpot.AirDate
                Else

                End If

                If TmpSpot.AirDate > HighDate Then
                    HighDate = TmpSpot.AirDate
                Else

                End If
            Else
                'Stop
            End If

        Next

        For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
            If TmpSpot.Bookingtype Is TmpBT AndAlso TmpSpot.AirDate.ToOADate >= LowDate AndAlso TmpSpot.AirDate.ToOADate <= HighDate Then
                Campaign.ExtendedInfos.Remove(TmpSpot.DatabaseID)
                Campaign.BookedSpots.Remove(TmpSpot.ID)
                'Else
                'Stop
            End If
        Next
        For Each TmpSpot As Trinity.cPlannedSpot In Campaign.PlannedSpots
            If TmpSpot.Bookingtype Is TmpBT Then
                If TmpSpot.AddedValue IsNot Nothing Then
                    TmpSpot.MatchedBookedSpot = Campaign.BookedSpots.Add(CreateGUID, TmpSpot.Channel.ChannelName & Format(TmpSpot.AirDate, "yyyymmdd") & Trinity.Helper.Mam2Tid(TmpSpot.MaM).Substring(0, 2) & Mid(Trinity.Helper.Mam2Tid(TmpSpot.MaM), 3), TmpSpot.Channel.ChannelName, Date.FromOADate(TmpSpot.AirDate), TmpSpot.MaM, TmpSpot.Programme, TmpSpot.ProgAfter, TmpSpot.ProgBefore, TmpSpot.PriceGross / (TmpSpot.AddedValue.IndexGross / 100), TmpSpot.PriceNet / (TmpSpot.AddedValue.IndexNet / 100), TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget), TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), TmpSpot.Filmcode, TmpSpot.Bookingtype.Name, False, False, 0)
                    TmpSpot.MatchedBookedSpot.MatchedSpot = TmpSpot
                    TmpSpot.MatchedBookedSpot.AddedValues.Add(TmpSpot.AddedValue.ID, TmpSpot.AddedValue)
                Else
                    TmpSpot.MatchedBookedSpot = Campaign.BookedSpots.Add(CreateGUID, TmpSpot.Channel.ChannelName & Format(TmpSpot.AirDate, "yyyymmdd") & Trinity.Helper.Mam2Tid(TmpSpot.MaM).Substring(0, 2) & Mid(Trinity.Helper.Mam2Tid(TmpSpot.MaM), 3), TmpSpot.Channel.ChannelName, Date.FromOADate(TmpSpot.AirDate), TmpSpot.MaM, TmpSpot.Programme, TmpSpot.ProgAfter, TmpSpot.ProgBefore, TmpSpot.PriceGross, TmpSpot.PriceNet, TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget), TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), TmpSpot.Filmcode, TmpSpot.Bookingtype.Name, False, False, 0)
                    TmpSpot.MatchedBookedSpot.MatchedSpot = TmpSpot
                End If
            End If
        Next
        UpdateSpotlist()
        UpdateConfirmed()
    End Sub

    Private Sub grdConfirmed_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdConfirmed.CellValuePushed
        Dim TmpBT As Trinity.cBookingType = cmbChannel.SelectedItem
        Dim TmpCol As Windows.Forms.DataGridViewColumn = grdConfirmed.Columns(e.ColumnIndex)
        Dim TmpSpot As Trinity.cPlannedSpot = Campaign.PlannedSpots(grdConfirmed.Rows(e.RowIndex).Tag)

        If TmpSpot Is Nothing Then Exit Sub
        Select Case TmpCol.HeaderText
            Case "My Est"
                TmpCol.Tag -= TmpSpot.MyRating
                TmpSpot.MyRating = e.Value
                TmpCol.Tag += TmpSpot.MyRating
            Case "Net Price"
                TmpCol.Tag -= TmpSpot.PriceNet
                TmpSpot.PriceNet = e.Value
                TmpCol.Tag += TmpSpot.PriceNet
        End Select
        grdSpotlist.Invalidate()
    End Sub

    Private Sub UseSamePeriodLastYearToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseSamePeriodLastYearToolStripMenuItem.Click

    End Sub

    Private Sub cmdRemoveMatchings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveMatchings.Click

        For Each spot As Trinity.cBookedSpot In Campaign.BookedSpots
            spot.Matched = 0
            spot.MatchedSpot = Nothing
        Next
        cmbChannel.Items.Clear()

        'Just add the channels in the campaign that have specifics to the listbox
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                If TmpBT.BookIt AndAlso TmpBT.IsSpecific Then
                    cmbChannel.Items.Add(TmpBT)
                End If
            Next
        Next

        Dim count As Integer = 0
        Dim count2 As Integer = 0
        'Go through all spots that have been booked in Booking
        For Each TmpBookedSpot As Trinity.cBookedSpot In Campaign.BookedSpots
            count += 1
            count2 = 1
            'If the booked spot has not been matched to any spots then
            If TmpBookedSpot.MatchedSpot Is Nothing Then
                'Check all the spots that have been confirmed by the channels
                For Each TmpPlannedspot As Trinity.cPlannedSpot In Campaign.PlannedSpots
                    count2 += 1
                    'If the confirmed spot does not have a booked spot which it is matched against, then
                    If TmpPlannedspot.MatchedBookedSpot Is Nothing Then
                        'If the date of the booked and planned spots are the same, then
                        If TmpBookedSpot.AirDate.ToOADate = TmpPlannedspot.AirDate Then
                            'If the confirmed spot is plus or minus 10 mins from the booked airtime, then
                            If TmpBookedSpot.MaM >= TmpPlannedspot.MaM - 10 And TmpBookedSpot.MaM <= TmpPlannedspot.MaM + 10 Then
                                'If the booking type on the confirmed and booked spot are the same, then
                                If TmpBookedSpot.Bookingtype Is TmpPlannedspot.Bookingtype Then
                                    'Make this confirmed spot match the one in booking
                                    TmpBookedSpot.MatchedSpot = TmpPlannedspot
                                    'Make the confirmed spot match this booked spot
                                    TmpPlannedspot.MatchedBookedSpot = TmpBookedSpot
                                    'And transfer the estimate from the booked spot to this confirmed spot
                                    TmpPlannedspot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget) = TmpBookedSpot.MyEstimate
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        Next
        cmbChannel.SelectedIndex = 0
    End Sub

End Class