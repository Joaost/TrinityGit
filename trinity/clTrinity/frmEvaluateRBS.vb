

Public Class frmEvaluateRBS

    Private Class cBookedPlanned
        Public Week As Trinity.cWeek
        Public Film As Trinity.cFilm
        Public Daypart As Integer
        Public Booked As Single = 0
        Public Planned As Single = 0
        Public Actual As Single = 0
    End Class

    Private _eval As New Dictionary(Of Trinity.cBookingType, Dictionary(Of String, Dictionary(Of String, Dictionary(Of Integer, cBookedPlanned))))

    Private Sub frmEvaluateRBS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbChannel.Items.Clear()
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                If TmpBT.BookIt AndAlso TmpBT.IsRBS Then
                    cmbChannel.Items.Add(TmpBT)
                End If
            Next
        Next
        If cmbChannel.Items.Count > 0 Then cmbChannel.SelectedIndex = 0
    End Sub

    Private Sub cmbChannel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbChannel.SelectedIndexChanged
        Dim TmpBT As Trinity.cBookingType = cmbChannel.SelectedItem
        If TmpBT Is Nothing Then Exit Sub

        If Not _eval.ContainsKey(TmpBT) Then
            Dim WeekDict As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of Integer, cBookedPlanned)))
            For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                Dim FilmDict As New Dictionary(Of String, Dictionary(Of Integer, cBookedPlanned))
                For Each TmpFilm As Trinity.cFilm In TmpWeek.Films
                    Dim DPDict As New Dictionary(Of Integer, cBookedPlanned)
                    For dp As Integer = 0 To Campaign.Dayparts.Count - 1
                        Dim BP As New cBookedPlanned
                        BP.Booked = TmpWeek.TRPBuyingTarget * (TmpFilm.Share / 100) * (TmpBT.MainDaypartSplit(dp) / 100)
                        BP.Week = TmpWeek
                        BP.Film = TmpFilm
                        BP.Daypart = dp
                        'Hitta de actual TRP som ligger på den här dayparten för den här filmen, den här veckan
                        Dim TRPs As Single = 0
                        For Each Spot As Trinity.cActualSpot In Campaign.ActualSpots
                            If TmpWeek.EndDate = Spot.Week.EndDate Then 'Same week?
                                If TmpFilm.Filmcode = Spot.Filmcode Then 'Same filmcode?
                                    If TmpBT.ParentChannel.ChannelName = Spot.Channel.ChannelName Then 'Same channel?
                                        If dp = Campaign.Dayparts.GetDaypartIndexForMam(Spot.MaM) Then 'Same daypart?
                                            TRPs += Spot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget) 'OK then sum the TRP of the spots which make it this far
                                        End If
                                    End If
                                End If
                            End If
                        Next
                        BP.Actual = TRPs
                        DPDict.Add(dp, BP)
                    Next
                    FilmDict.Add(TmpFilm.Name, DPDict)
                Next
                WeekDict.Add(TmpWeek.Name, FilmDict)
            Next
            _eval.Add(TmpBT, WeekDict)
            For Each TmpSpot As Trinity.cPlannedSpot In Campaign.PlannedSpots
                If TmpSpot.Bookingtype Is TmpBT Then
                    _eval(TmpBT)(TmpSpot.Week.Name)(TmpSpot.Film.Name)(Campaign.Dayparts.GetDaypartIndexForMam(TmpSpot.MaM)).Planned += TmpSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
                End If
            Next
        End If
        grdEvaluate.Rows.Clear()
        For Each WeekKV As KeyValuePair(Of String, Dictionary(Of String, Dictionary(Of Integer, cBookedPlanned))) In _eval(TmpBT)
            For Each FilmKV As KeyValuePair(Of String, Dictionary(Of Integer, cBookedPlanned)) In WeekKV.Value
                For Each DPKV As KeyValuePair(Of Integer, cBookedPlanned) In FilmKV.Value
                    If (DPKV.Value.Booked > 0 AndAlso DPKV.Value.Planned > 0) OrElse Not chkHideZero.Checked Then
                        grdEvaluate.Rows(grdEvaluate.Rows.Add).Tag = DPKV.Value
                    End If
                Next
            Next
        Next
    End Sub

    Private Sub grdEvaluate_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdEvaluate.CellContentClick
    End Sub

    Private Sub grdEvaluate_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdEvaluate.CellFormatting
        Dim TmpBP As cBookedPlanned = grdEvaluate.Rows(e.RowIndex).Tag
        If e.ColumnIndex > 2 Then
            e.Value = Format(e.Value, "N1")
        End If
        If TmpBP.Planned < TmpBP.Booked * 0.9 Then
            e.CellStyle.ForeColor = Color.Red
        End If
    End Sub

    Private Sub grdEvaluate_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdEvaluate.CellValueNeeded
        Dim TmpBP As cBookedPlanned = grdEvaluate.Rows(e.RowIndex).Tag

        Select Case grdEvaluate.Columns(e.ColumnIndex).HeaderText
            Case "Week"
                e.Value = TmpBP.Week.Name
            Case "Film"
                e.Value = TmpBP.Film.Name
            Case "Daypart"
                e.Value = Campaign.Dayparts(TmpBP.Daypart).Name
            Case "Booked"
                e.Value = TmpBP.Booked
            Case "Planned"
                e.Value = TmpBP.Planned
            Case "Actual"
                e.Value = TmpBP.Actual
        End Select

    End Sub

    Private Sub chkHideZero_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkHideZero.CheckedChanged
        cmbChannel_SelectedIndexChanged(sender, e)
    End Sub
End Class