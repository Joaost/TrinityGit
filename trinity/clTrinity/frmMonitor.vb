Public Class frmMonitor

    Private Sub cmbChart_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbChart.SelectedIndexChanged
        Dim i As Integer
        Dim s As Integer

        If cmbChart.SelectedIndex = cmbChart.Items.Count - 1 Then
            Campaign.CalculateSpots()
            cmbShowGoal.Visible = True
            cmdFrequency.Visible = True
            chtReach.Days = Campaign.EndDate - Campaign.StartDate + 1
            Dim d As Integer = Campaign.StartDate
            Dim x As Integer = 1
            s = 0
            While s < Campaign.Adedge.getGroupCount - 1
                While s < Campaign.Adedge.getGroupCount - 1 AndAlso d = Campaign.Adedge.getAttribGroup(Connect.eAttribs.aDate, s)
                    s = s + 1
                End While
                For i = 1 To 10
                    If cmbTarget.Text = "Main Target" Then
                        chtReach.ActualReach(x, i) = Campaign.Adedge.getRF(s, , , Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, i)
                    ElseIf cmbTarget.Text = "Secondary Target" Then
                        chtReach.ActualReach(x, i) = Campaign.Adedge.getRF(s, , , Campaign.TargColl(Campaign.SecondaryTarget.TargetName, Campaign.Adedge) - 1, i)
                    ElseIf cmbTarget.Text = "Buying Target" Then
                        chtReach.ActualReach(x, i) = Campaign.Adedge.getRF(s, , , Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, i)
                    Else
                        chtReach.ActualReach(x, i) = Campaign.Adedge.getRF(s, , , Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge) - 1, i)
                    End If
                Next
                d = d + 1
                x = x + 1
            End While
            For i = 1 To 10
                chtReach.ReachGoal(i) = Campaign.ReachGoal(i)
            Next
            cmbDisplayType.Visible = False
            chtReach.UpdateChart()
            chtReach.BringToFront()
        Else
            cmbShowGoal.Visible = False
            cmdFrequency.Visible = False
            UpdatePlanned()
            UpdateConfirmed()
            UpdateActual()
            cmbDisplayType.Visible = True
            chtMain.UpdateChart()
            chtMain.BringToFront()
        End If
    End Sub

    Sub UpdatePlanned()
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpWeek As Trinity.cWeek
        Dim TmpFilm As Trinity.cFilm
        Dim dp As Integer
        Dim i As Integer
        Dim TRP As Single
        Dim s As Integer
        Dim ShowIt As Boolean
        Dim DPIndex As Single

        Dim sum1 As Double = 0
        For Each spot As Trinity.cBookedSpot In Campaign.BookedSpots
            If spot.Bookingtype.Name = "Last Minute" Then
                sum1 += 1
            End If
        Next

        If cmbTarget.SelectedIndex = -1 Then Exit Sub
        'chtMain.ShowBought = Settings.ShowBought
        chtMain.Headline = cmbChart.Text
        If cmbChart.Text = "Allocation" Then
            chtMain.Dimensions = 0

            For Each TmpC As Trinity.cCombination In Campaign.Combinations
                If TmpC.ShowAsOne Then
                    chtMain.Dimensions = chtMain.Dimensions + 1

                    If cmbTarget.SelectedIndex = 2 Then
                        If TmpC.BuyingTarget = "" Then
                            chtMain.DimensionLabel(chtMain.Dimensions - 1) = TmpC.Name & "(-)"
                        Else
                            chtMain.DimensionLabel(chtMain.Dimensions - 1) = TmpC.Name & "(" & TmpC.BuyingTarget & ")"
                        End If
                    Else
                        chtMain.DimensionLabel(chtMain.Dimensions - 1) = TmpC.Name
                    End If

                    For Each tmpCC As Trinity.cCombinationChannel In TmpC.Relations
                        For Each TmpWeek In tmpCC.Bookingtype.Weeks
                            TmpWeek.RecalculateCPP()

                            For Each TmpFilm In TmpWeek.Films
                                For dp = 0 To Campaign.Dayparts.Count - 1
                                    DPIndex = tmpCC.Bookingtype.MainDaypartSplit(dp) / 100
                                    Select Case cmbTarget.SelectedIndex
                                        Case 0 : TRP = TmpWeek.TRP * DPIndex
                                        Case 1 : TRP = TmpWeek.TRP * (tmpCC.Bookingtype.IndexSecondTarget / tmpCC.Bookingtype.IndexMainTarget) * DPIndex
                                        Case 2 : TRP = TmpWeek.TRPBuyingTarget * DPIndex
                                        Case 3 : TRP = TmpWeek.TRPAllAdults * DPIndex
                                    End Select
                                    If cmbDisplayType.SelectedIndex = 2 Then
                                        TRP *= TmpFilm.Index / 100
                                    End If

                                    If FilterIn(TmpC.Name, , dp, TmpFilm.Name, TmpWeek.Name) Then
                                        chtMain.Planned(chtMain.Dimensions - 1) = chtMain.Planned(chtMain.Dimensions - 1) + (TmpFilm.Share / 100) * TRP
                                    End If
                                Next
                            Next
                        Next
                    Next

                End If
            Next
            For Each TmpChan In Campaign.Channels
                For Each TmpBT In TmpChan.BookingTypes
                    If TmpBT.BookIt AndAlso TmpBT.ShowMe Then
                        chtMain.Dimensions = chtMain.Dimensions + 1
                        If cmbTarget.SelectedIndex = 2 Then
                            chtMain.DimensionLabel(chtMain.Dimensions - 1) = TmpChan.Shortname & " " & TmpBT.Shortname & " (" & TmpBT.BuyingTarget.Target.TargetNameNice & ")"
                        Else
                            chtMain.DimensionLabel(chtMain.Dimensions - 1) = TmpChan.Shortname & " " & TmpBT.Shortname
                        End If

                        For Each TmpWeek In TmpBT.Weeks

                            'Added to get correct CPP/TRP in monitor if you dont open Allocate window **********
                            TmpWeek.RecalculateCPP()

                            For Each TmpFilm In TmpWeek.Films
                                For dp = 0 To Campaign.Dayparts.Count - 1
                                    DPIndex = TmpBT.MainDaypartSplit(dp) / 100
                                    Select Case cmbTarget.SelectedIndex
                                        Case 0 : TRP = TmpWeek.TRP * DPIndex
                                        Case 1 : TRP = TmpWeek.TRP * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget) * DPIndex
                                        Case 2 : TRP = TmpWeek.TRPBuyingTarget * DPIndex
                                        Case 3 : TRP = TmpWeek.TRPAllAdults * DPIndex
                                    End Select
                                    If cmbDisplayType.SelectedIndex = 2 Then
                                        TRP *= TmpFilm.Index / 100
                                    End If
                                    'If TRP = 0 And TmpBT.Name = "Specifics" Then Stop
                                    If FilterIn(TmpChan.ChannelName, TmpBT.Name, dp, TmpFilm.Name, TmpWeek.Name) Then
                                        chtMain.Planned(chtMain.Dimensions - 1) = chtMain.Planned(chtMain.Dimensions - 1) + (TmpFilm.Share / 100) * TRP
                                    End If
                                Next
                            Next
                        Next
                        For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                            Dim countActualDays As Integer = 0
                            'count the actual days the campaign will run in the compensation
                            For Each w As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                                For day As Integer = w.StartDate To w.EndDate
                                    If day >= TmpComp.FromDate.ToOADate AndAlso day <= TmpComp.ToDate.ToOADate Then
                                        countActualDays += 1
                                    End If
                                Next
                            Next

                            For Each week As Trinity.cWeek In TmpBT.Weeks
                                If FilterIn(Channel:=TmpComp.Bookingtype.ParentChannel.ChannelName, Bookingtype:=TmpComp.Bookingtype.Name, Daypart:=-1, Film:=Nothing, Week:=week.Name) Then
                                    Select Case cmbTarget.SelectedIndex
                                        Case 0
                                            TRP = TmpComp.TRPMainTarget
                                        Case 1
                                            TRP = TmpComp.TRPMainTarget * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                                        Case 2
                                            TRP = TmpComp.TRPs
                                        Case 3
                                            TRP = TmpComp.TRPMainTarget * (TmpBT.IndexAllAdults / TmpBT.IndexMainTarget)
                                    End Select

                                    If week.StartDate >= TmpComp.FromDate.ToOADate AndAlso week.EndDate <= TmpComp.ToDate.ToOADate Then
                                        TRP = (week.EndDate - week.StartDate + 1) / countActualDays * TmpComp.TRPs
                                    Else
                                        Dim iz As Integer
                                        Dim count As Integer = 0
                                        For iz = week.StartDate To week.EndDate
                                            If iz >= TmpComp.FromDate.ToOADate AndAlso iz <= TmpComp.ToDate.ToOADate Then
                                                count += 1
                                            End If
                                        Next
                                        If countActualDays * TmpComp.TRPs <> 0 Then
                                            TRP = count / countActualDays * TmpComp.TRPs
                                        End If
                                    End If
                                    For dayp As Integer = 0 To Campaign.Dayparts.Count - 1
                                        For Each film As Trinity.cFilm In week.Films
                                            If FilterIn(TmpChan.ChannelName, TmpBT.Name, dayp, film.Name, ) Then
                                                chtMain.Planned(chtMain.Dimensions - 1) += TRP * (TmpBT.MainDaypartSplit(dayp) / 100) * (film.Share / 100)
                                            End If
                                        Next

                                    Next
                                    'chtMain.Planned(chtMain.Dimensions - 1) += TRP
                                End If
                            Next

                            'For d As Integer = TmpComp.FromDate.ToOADate To TmpComp.ToDate.ToOADate
                            '    If FilterIn(TmpChan.ChannelName, TmpBT.Name, , , TmpBT.GetWeek(Date.FromOADate(d)).Name) Then
                            '        Select Case cmbTarget.SelectedIndex
                            '            Case 0
                            '                TRP = TmpComp.TRPMainTarget
                            '            Case 1
                            '                TRP = TmpComp.TRPMainTarget * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                            '            Case 2
                            '                TRP = TmpComp.TRPs
                            '            Case 3
                            '                TRP = TmpComp.TRPMainTarget * (TmpBT.IndexAllAdults / TmpBT.IndexMainTarget)
                            '        End Select
                            '        TRP = TRP / (TmpComp.ToDate.ToOADate - TmpComp.FromDate.ToOADate + 1)
                            '        chtMain.Planned(chtMain.Dimensions - 1) += TRP
                            '    End If
                            'Next
                        Next
                        For s = 1 To Campaign.BookedSpots.Count
                            If Campaign.BookedSpots(s).Channel Is TmpChan AndAlso Campaign.BookedSpots(s).Bookingtype Is TmpBT Then
                                Try
                                    If cmbTarget.SelectedIndex = 2 Then
                                        TRP = Campaign.BookedSpots(s).MyEstimateBuyTarget
                                    Else
                                        TRP = Campaign.BookedSpots(s).MyEstimate
                                    End If
                                    If cmbDisplayType.SelectedIndex = 2 Then
                                        TRP *= Campaign.BookedSpots(s).Film.Index / 100
                                    End If
                                    If FilterIn(Campaign.BookedSpots(s).Channel.ChannelName, Campaign.BookedSpots(s).Bookingtype.Name, Campaign.Dayparts.GetDaypartIndexForMam(Campaign.BookedSpots(s).MaM), Campaign.BookedSpots(s).Film.Name, Campaign.BookedSpots(s).week.Name) Then
                                        chtMain.Booked(chtMain.Dimensions - 1) += TRP
                                    End If
                                Catch
                                    'Stop
                                End Try
                            End If
                        Next
                    End If
                Next
            Next
        ElseIf cmbChart.Text = "Total" Then
            chtMain.Dimensions = 1
            chtMain.Planned(0) = 0
            chtMain.Booked(0) = 0
            chtMain.Confirmed(0) = 0
            chtMain.Actual(0) = 0
            chtMain.DimensionLabel(0) = "Total"
            For Each TmpChan In Campaign.Channels
                For Each TmpBT In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        For Each TmpWeek In TmpBT.Weeks
                            For Each TmpFilm In TmpWeek.Films
                                For dp = 0 To Campaign.Dayparts.Count - 1
                                    DPIndex = TmpBT.MainDaypartSplit(dp) / 100
                                    Select Case cmbTarget.SelectedIndex
                                        Case 0 : TRP = TmpWeek.TRP * DPIndex
                                        Case 1 : TRP = TmpWeek.TRP * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget) * DPIndex
                                        Case 2 : TRP = TmpWeek.TRPBuyingTarget * DPIndex
                                        Case 3 : TRP = TmpWeek.TRPAllAdults * DPIndex
                                    End Select
                                    If cmbDisplayType.SelectedIndex = 2 Then
                                        TRP *= TmpFilm.Index / 100
                                    End If
                                    If FilterIn(TmpChan.ChannelName, TmpBT.Name, dp, TmpFilm.Name, TmpWeek.Name) Then
                                        chtMain.Planned(0) += (TmpFilm.Share / 100) * TRP
                                    End If
                                Next
                            Next
                        Next
                        For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                            Dim countActualDays As Integer = 0
                            'count the actual days the campaign will run in the compensation
                            For Each w As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                                For day As Integer = w.StartDate To w.EndDate
                                    If day >= TmpComp.FromDate.ToOADate AndAlso day <= TmpComp.ToDate.ToOADate Then
                                        countActualDays += 1
                                    End If
                                Next
                            Next

                            For Each week As Trinity.cWeek In TmpBT.Weeks
                                If FilterIn(TmpChan.ChannelName, TmpBT.Name, , , week.Name) Then
                                    Select Case cmbTarget.SelectedIndex
                                        Case 0
                                            TRP = TmpComp.TRPMainTarget
                                        Case 1
                                            TRP = TmpComp.TRPMainTarget * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                                        Case 2
                                            TRP = TmpComp.TRPs
                                        Case 3
                                            TRP = TmpComp.TRPMainTarget * (TmpBT.IndexAllAdults / TmpBT.IndexMainTarget)
                                    End Select

                                    If week.StartDate >= TmpComp.FromDate.ToOADate AndAlso week.EndDate <= TmpComp.ToDate.ToOADate Then
                                        TRP = (week.EndDate - week.StartDate + 1) / countActualDays * TmpComp.TRPs
                                    Else
                                        Dim iz As Integer
                                        Dim count As Integer = 0
                                        For iz = week.StartDate To week.EndDate
                                            If iz >= TmpComp.FromDate.ToOADate AndAlso iz <= TmpComp.ToDate.ToOADate Then
                                                count += 1
                                            End If
                                        Next
                                        If countActualDays * TmpComp.TRPs = 0 Then
                                            TRP = 0
                                        Else
                                            TRP = count / countActualDays * TmpComp.TRPs
                                        End If
                                    End If
                                    chtMain.Planned(0) += TRP
                                End If
                            Next
                        Next
                        For s = 1 To Campaign.BookedSpots.Count
                            If Campaign.BookedSpots(s).Channel Is TmpChan AndAlso Campaign.BookedSpots(s).Bookingtype Is TmpBT Then
                                Try
                                    If cmbTarget.SelectedIndex = 2 Then
                                        TRP = Campaign.BookedSpots(s).MyEstimateBuyTarget
                                    Else
                                        TRP = Campaign.BookedSpots(s).MyEstimate
                                    End If
                                    If cmbDisplayType.SelectedIndex = 2 Then
                                        TRP *= Campaign.BookedSpots(s).Film.Index / 100
                                    End If
                                    If FilterIn(Campaign.BookedSpots(s).Channel.ChannelName, Campaign.BookedSpots(s).Bookingtype.Name, Campaign.Dayparts.GetDaypartIndexForMam(Campaign.BookedSpots(s).MaM), Campaign.BookedSpots(s).Film.Name, Campaign.BookedSpots(s).week.Name) Then
                                        chtMain.Booked(0) += TRP
                                    End If
                                Catch
                                    'Stop
                                End Try
                            End If
                        Next
                    End If
                Next
            Next
        ElseIf cmbChart.Text = "Channels" Then
            chtMain.Dimensions = 0
            For Each TmpChan In Campaign.Channels
                ShowIt = False
                For Each TmpBT In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        ShowIt = True
                        Exit For
                    End If
                Next
                If ShowIt Then
                    chtMain.Dimensions = chtMain.Dimensions + 1
                    chtMain.DimensionLabel(chtMain.Dimensions - 1) = TmpChan.ChannelName
                    For Each TmpBT In TmpChan.BookingTypes
                        If TmpBT.BookIt Then
                            For Each TmpWeek In TmpBT.Weeks
                                'Added to ger correct CPP/TRP in monitor if you dont open Allocate window **********
                                TmpWeek.RecalculateCPP()
                                For Each TmpFilm In TmpWeek.Films
                                    For dp = 0 To Campaign.Dayparts.Count - 1
                                        DPIndex = TmpBT.MainDaypartSplit(dp) / 100
                                        Select Case cmbTarget.SelectedIndex
                                            Case 0 : TRP = TmpWeek.TRP * DPIndex
                                            Case 1 : TRP = TmpWeek.TRP * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget) * DPIndex
                                            Case 2 : TRP = TmpWeek.TRPBuyingTarget * DPIndex
                                            Case 3 : TRP = TmpWeek.TRPAllAdults * DPIndex
                                        End Select
                                        If cmbDisplayType.SelectedIndex = 2 Then
                                            TRP *= TmpFilm.Index / 100
                                        End If
                                        If FilterIn(TmpChan.ChannelName, TmpBT.Name, dp, TmpFilm.Name, TmpWeek.Name) Then
                                            chtMain.Planned(chtMain.Dimensions - 1) = chtMain.Planned(chtMain.Dimensions - 1) + (TmpFilm.Share / 100) * TRP
                                        End If
                                    Next
                                Next
                            Next
                            For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                                Dim countActualDays As Integer = 0
                                'count the actual days the campaign will run in the compensation
                                For Each w As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                                    For day As Integer = w.StartDate To w.EndDate
                                        If day >= TmpComp.FromDate.ToOADate AndAlso day <= TmpComp.ToDate.ToOADate Then
                                            countActualDays += 1
                                        End If
                                    Next
                                Next

                                For Each week As Trinity.cWeek In TmpBT.Weeks
                                    If FilterIn(TmpChan.ChannelName, TmpBT.Name, , , week.Name) Then
                                        Select Case cmbTarget.SelectedIndex
                                            Case 0
                                                TRP = TmpComp.TRPMainTarget
                                            Case 1
                                                TRP = TmpComp.TRPMainTarget * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                                            Case 2
                                                TRP = TmpComp.TRPs
                                            Case 3
                                                TRP = TmpComp.TRPMainTarget * (TmpBT.IndexAllAdults / TmpBT.IndexMainTarget)
                                        End Select

                                        If week.StartDate >= TmpComp.FromDate.ToOADate AndAlso week.EndDate <= TmpComp.ToDate.ToOADate Then
                                            TRP = (week.EndDate - week.StartDate + 1) / countActualDays * TmpComp.TRPs
                                        Else
                                            Dim iz As Integer
                                            Dim count As Integer = 0
                                            For iz = week.StartDate To week.EndDate
                                                If iz >= TmpComp.FromDate.ToOADate AndAlso iz <= TmpComp.ToDate.ToOADate Then
                                                    count += 1
                                                End If
                                            Next
                                            If countActualDays * TmpComp.TRPs <> 0 Then
                                                TRP = count / countActualDays * TmpComp.TRPs
                                            End If
                                        End If
                                        chtMain.Planned(chtMain.Dimensions - 1) += TRP
                                    End If
                                Next

                                'For d As Integer = TmpComp.FromDate.ToOADate To TmpComp.ToDate.ToOADate
                                '    If FilterIn(TmpChan.ChannelName, TmpBT.Name, , , TmpBT.GetWeek(Date.FromOADate(d)).Name) Then
                                '        Select Case cmbTarget.SelectedIndex
                                '            Case 0
                                '                TRP = TmpComp.TRPMainTarget
                                '            Case 1
                                '                TRP = TmpComp.TRPMainTarget * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                                '            Case 2
                                '                TRP = TmpComp.TRPs
                                '            Case 3
                                '                TRP = TmpComp.TRPMainTarget * (TmpBT.IndexAllAdults / TmpBT.IndexMainTarget)
                                '        End Select
                                '        TRP = TRP / (TmpComp.ToDate.ToOADate - TmpComp.FromDate.ToOADate + 1)
                                '        chtMain.Planned(chtMain.Dimensions - 1) += TRP
                                '    End If
                                'Next
                            Next
                            For s = 1 To Campaign.BookedSpots.Count
                                If Campaign.BookedSpots(s).Channel Is TmpChan AndAlso Campaign.BookedSpots(s).Bookingtype Is TmpBT Then
                                    Try
                                        If cmbTarget.SelectedIndex = 2 Then
                                            TRP = Campaign.BookedSpots(s).MyEstimateBuyTarget
                                        Else
                                            TRP = Campaign.BookedSpots(s).MyEstimate
                                        End If
                                        If cmbDisplayType.SelectedIndex = 2 Then
                                            TRP *= Campaign.BookedSpots(s).Film.Index / 100
                                        End If
                                        If FilterIn(Campaign.BookedSpots(s).Channel.ChannelName, Campaign.BookedSpots(s).Bookingtype.Name, Campaign.Dayparts.GetDaypartIndexForMam(Campaign.BookedSpots(s).MaM), Campaign.BookedSpots(s).Film.Name, Campaign.BookedSpots(s).week.Name) Then
                                            chtMain.Booked(chtMain.Dimensions - 1) += TRP
                                        End If
                                    Catch
                                        'Stop
                                    End Try
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        ElseIf cmbChart.Text = "Weekdays" Then
            Dim WD() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}
            chtMain.Dimensions = 0
            chtMain.Dimensions = 7
            For i = 0 To 6
                chtMain.DimensionLabel(i) = WD(i)
                For dp = 0 To Campaign.Dayparts.Count - 1
                    For Each TmpChan In Campaign.Channels
                        For Each TmpBT In TmpChan.BookingTypes
                            If TmpBT.BookIt Then
                                For Each TmpWeek In TmpBT.Weeks
                                    'Added to ger correct CPP/TRP in monitor if you dont open Allocate window **********
                                    TmpWeek.RecalculateCPP()
                                    For Each TmpFilm In TmpWeek.Films
                                        Select Case cmbTarget.SelectedIndex
                                            Case 0 : TRP = TmpWeek.TRP
                                            Case 1 : TRP = TmpWeek.TRP * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                                            Case 2 : TRP = TmpWeek.TRPBuyingTarget
                                            Case 3 : TRP = TmpWeek.TRPAllAdults
                                        End Select
                                        If cmbDisplayType.SelectedIndex = 2 Then
                                            TRP *= TmpFilm.Index / 100
                                        End If
                                        If FilterIn(TmpChan.ChannelName, TmpBT.Name, dp, TmpFilm.Name, TmpWeek.Name) Then
                                            chtMain.Planned(i) = chtMain.Planned(i) + ((TmpFilm.Share / 100) * TRP * (TmpBT.MainDaypartSplit(dp) / 100)) / 7
                                        End If
                                    Next
                                Next
                                For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                                    Dim countActualDays As Integer = 0
                                    'count the actual days the campaign will run in the compensation
                                    For Each w As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                                        For day As Integer = w.StartDate To w.EndDate
                                            If day >= TmpComp.FromDate.ToOADate AndAlso day <= TmpComp.ToDate.ToOADate Then
                                                countActualDays += 1
                                            End If
                                        Next
                                    Next

                                    For Each week As Trinity.cWeek In TmpBT.Weeks
                                        If FilterIn(TmpChan.ChannelName, TmpBT.Name, , , week.Name) Then
                                            Select Case cmbTarget.SelectedIndex
                                                Case 0
                                                    TRP = TmpComp.TRPMainTarget
                                                Case 1
                                                    TRP = TmpComp.TRPMainTarget * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                                                Case 2
                                                    TRP = TmpComp.TRPs
                                                Case 3
                                                    TRP = TmpComp.TRPMainTarget * (TmpBT.IndexAllAdults / TmpBT.IndexMainTarget)
                                            End Select

                                            Dim iz As Integer
                                            For iz = week.StartDate To week.EndDate
                                                If iz >= TmpComp.FromDate.ToOADate AndAlso iz <= TmpComp.ToDate.ToOADate Then
                                                    If Weekday(Date.FromOADate(iz), FirstDayOfWeek.Monday) = i + 1 Then
                                                        TRP = TRP / (TmpComp.ToDate.ToOADate - TmpComp.FromDate.ToOADate + 1) / Campaign.Dayparts.Count
                                                        chtMain.Planned(i) += TRP / (1 / countActualDays) / Campaign.Dayparts.Count
                                                    End If
                                                End If
                                            Next
                                        End If
                                    Next

                                    'For d As Integer = TmpComp.FromDate.ToOADate To TmpComp.ToDate.ToOADate
                                    '    If Weekday(Date.FromOADate(d), FirstDayOfWeek.Monday) = i + 1 Then
                                    '        If FilterIn(TmpChan.ChannelName, TmpBT.Name, , , TmpBT.GetWeek(Date.FromOADate(d)).Name) Then
                                    '            Select Case cmbTarget.SelectedIndex
                                    '                Case 0
                                    '                    TRP = TmpComp.TRPMainTarget
                                    '                Case 1
                                    '                    TRP = TmpComp.TRPMainTarget * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                                    '                Case 2
                                    '                    TRP = TmpComp.TRPs
                                    '                Case 3
                                    '                    TRP = TmpComp.TRPMainTarget * (TmpBT.IndexAllAdults / TmpBT.IndexMainTarget)
                                    '            End Select
                                    '            TRP = TRP / (TmpComp.ToDate.ToOADate - TmpComp.FromDate.ToOADate + 1) / Campaign.Dayparts.Count
                                    '            chtMain.Planned(i) += TRP
                                    '        End If
                                    '    End If
                                    'Next
                                Next
                            End If
                        Next
                    Next
                Next
                For s = 1 To Campaign.BookedSpots.Count
                    If Weekday(Campaign.BookedSpots(s).AirDate, FirstDayOfWeek.Monday) = i + 1 Then
                        Try
                            If cmbTarget.SelectedIndex = 2 Then
                                TRP = Campaign.BookedSpots(s).MyEstimateBuyTarget
                            Else
                                TRP = Campaign.BookedSpots(s).MyEstimate
                            End If
                            If cmbDisplayType.SelectedIndex = 2 Then
                                TRP *= Campaign.BookedSpots(s).Film.Index / 100
                            End If
                            If FilterIn(Campaign.BookedSpots(s).Channel.ChannelName, Campaign.BookedSpots(s).Bookingtype.Name, Campaign.Dayparts.GetDaypartIndexForMam(Campaign.BookedSpots(s).MaM), Campaign.BookedSpots(s).Film.Name, Campaign.BookedSpots(s).week.Name) Then
                                chtMain.Booked(i) += TRP
                            End If
                        Catch
                            'Stop
                        End Try
                    End If
                Next
            Next

        ElseIf cmbChart.Text = "Dayparts" Then
            chtMain.Dimensions = 0
            For dp = 0 To Campaign.Dayparts.Count - 1
                chtMain.Dimensions = chtMain.Dimensions + 1
                chtMain.DimensionLabel(chtMain.Dimensions - 1) = Campaign.Dayparts(dp).Name
                For Each TmpChan In Campaign.Channels
                    For Each TmpBT In TmpChan.BookingTypes
                        If TmpBT.BookIt Then
                            For Each TmpWeek In TmpBT.Weeks
                                'Added to ger correct CPP/TRP in monitor if you dont open Allocate window **********
                                TmpWeek.RecalculateCPP()
                                For Each TmpFilm In TmpWeek.Films
                                    Select Case cmbTarget.SelectedIndex
                                        Case 0 : TRP = TmpWeek.TRP
                                        Case 1 : TRP = TmpWeek.TRP * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                                        Case 2 : TRP = TmpWeek.TRPBuyingTarget
                                        Case 3 : TRP = TmpWeek.TRPAllAdults
                                    End Select
                                    If cmbDisplayType.SelectedIndex = 2 Then
                                        TRP *= TmpFilm.Index / 100
                                    End If
                                    If FilterIn(TmpChan.ChannelName, TmpBT.Name, dp, TmpFilm.Name, TmpWeek.Name) Then
                                        chtMain.Planned(chtMain.Dimensions - 1) += (TmpFilm.Share / 100) * TRP * (TmpBT.MainDaypartSplit(dp) / 100)
                                    End If
                                Next
                            Next
                            For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                                Dim countActualDays As Integer = 0
                                'count the actual days the campaign will run in the compensation
                                For Each w As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                                    For day As Integer = w.StartDate To w.EndDate
                                        If day >= TmpComp.FromDate.ToOADate AndAlso day <= TmpComp.ToDate.ToOADate Then
                                            countActualDays += 1
                                        End If
                                    Next
                                Next

                                For Each week As Trinity.cWeek In TmpBT.Weeks
                                    If FilterIn(TmpChan.ChannelName, TmpBT.Name, , , week.Name) Then
                                        Select Case cmbTarget.SelectedIndex
                                            Case 0
                                                TRP = TmpComp.TRPMainTarget
                                            Case 1
                                                TRP = TmpComp.TRPMainTarget * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                                            Case 2
                                                TRP = TmpComp.TRPs
                                            Case 3
                                                TRP = TmpComp.TRPMainTarget * (TmpBT.IndexAllAdults / TmpBT.IndexMainTarget)
                                        End Select

                                        Dim iz As Integer
                                        For iz = week.StartDate To week.EndDate
                                            If iz >= TmpComp.FromDate.ToOADate AndAlso iz <= TmpComp.ToDate.ToOADate Then
                                                chtMain.Planned(dp) += TRP * (1 / countActualDays) * (TmpBT.MainDaypartSplit(dp) / 100)
                                            End If
                                        Next
                                    End If
                                Next

                                'For d As Integer = TmpComp.FromDate.ToOADate To TmpComp.ToDate.ToOADate
                                '    If FilterIn(TmpChan.ChannelName, TmpBT.Name, , , TmpBT.GetWeek(Date.FromOADate(d)).Name) Then
                                '        Select Case cmbTarget.SelectedIndex
                                '            Case 0
                                '                TRP = TmpComp.TRPMainTarget
                                '            Case 1
                                '                TRP = TmpComp.TRPMainTarget * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                                '            Case 2
                                '                TRP = TmpComp.TRPs
                                '            Case 3
                                '                TRP = TmpComp.TRPMainTarget * (TmpBT.IndexAllAdults / TmpBT.IndexMainTarget)
                                '        End Select
                                '        TRP = (TRP / (TmpComp.ToDate.ToOADate - TmpComp.FromDate.ToOADate + 1)) * (TmpBT.MainDaypartSplit(dp) / 100)
                                '        chtMain.Planned(dp) += TRP
                                '    End If
                                'Next
                            Next
                        End If
                    Next
                Next
                For s = 1 To Campaign.BookedSpots.Count
                    If Campaign.Dayparts.GetDaypartIndexForMam(Campaign.BookedSpots(s).MaM) = dp Then
                        Try
                            If cmbTarget.SelectedIndex = 2 Then
                                TRP = Campaign.BookedSpots(s).MyEstimateBuyTarget
                            Else
                                TRP = Campaign.BookedSpots(s).MyEstimate
                            End If
                            If cmbDisplayType.SelectedIndex = 2 Then
                                TRP *= Campaign.BookedSpots(s).Film.Index / 100
                            End If
                            If FilterIn(Campaign.BookedSpots(s).Channel.ChannelName, Campaign.BookedSpots(s).Bookingtype.Name, Campaign.Dayparts.GetDaypartIndexForMam(Campaign.BookedSpots(s).MaM), Campaign.BookedSpots(s).Film.Name, Campaign.BookedSpots(s).week.Name) Then
                                chtMain.Booked(dp) += TRP
                            End If
                        Catch
                            'Stop
                        End Try
                    End If
                Next
            Next
        ElseIf cmbChart.Text = "Films" Then
            chtMain.Dimensions = 0
            For i = 1 To Campaign.Channels(1).BookingTypes(1).Weeks(1).Films.Count
                chtMain.Dimensions = chtMain.Dimensions + 1
                chtMain.DimensionLabel(chtMain.Dimensions - 1) = Campaign.Channels(1).BookingTypes(1).Weeks(1).Films(i).Name
                For dp = 0 To Campaign.Dayparts.Count - 1
                    For Each TmpChan In Campaign.Channels
                        For Each TmpBT In TmpChan.BookingTypes
                            If TmpBT.BookIt Then
                                For Each TmpWeek In TmpBT.Weeks
                                    'Added to ger correct CPP/TRP in monitor if you dont open Allocate window **********
                                    TmpWeek.RecalculateCPP()
                                    Select Case cmbTarget.SelectedIndex
                                        Case 0 : TRP = TmpWeek.TRP
                                        Case 1 : TRP = TmpWeek.TRP * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                                        Case 2 : TRP = TmpWeek.TRPBuyingTarget
                                        Case 3 : TRP = TmpWeek.TRPAllAdults
                                    End Select
                                    If cmbDisplayType.SelectedIndex = 2 Then
                                        TRP *= TmpWeek.Films(i).Index / 100
                                    End If
                                    If FilterIn(TmpChan.ChannelName, TmpBT.Name, dp, Campaign.Channels(1).BookingTypes(1).Weeks(1).Films(i).Name, TmpWeek.Name) Then
                                        chtMain.Planned(chtMain.Dimensions - 1) = chtMain.Planned(chtMain.Dimensions - 1) + (TmpWeek.Films(i).Share / 100) * TRP * (TmpBT.MainDaypartSplit(dp) / 100)
                                    End If
                                Next
                            End If
                        Next
                    Next
                Next
                For s = 1 To Campaign.BookedSpots.Count
                    If Not Campaign.BookedSpots(s).Film Is Nothing Then
                        If Campaign.BookedSpots(s).week.Films(i).Name = Campaign.BookedSpots(s).Film.Name Then
                            Try
                                If cmbTarget.SelectedIndex = 2 Then
                                    TRP = Campaign.BookedSpots(s).MyEstimateBuyTarget
                                Else
                                    TRP = Campaign.BookedSpots(s).MyEstimate
                                End If
                                If cmbDisplayType.SelectedIndex = 2 Then
                                    TRP *= Campaign.BookedSpots(s).Film.Index / 100
                                End If
                                If FilterIn(Campaign.BookedSpots(s).Channel.ChannelName, Campaign.BookedSpots(s).Bookingtype.Name, Campaign.Dayparts.GetDaypartIndexForMam(Campaign.BookedSpots(s).MaM), Campaign.BookedSpots(s).Film.Name, Campaign.BookedSpots(s).week.Name) Then
                                    chtMain.Booked(chtMain.Dimensions - 1) += TRP
                                End If
                            Catch
                                'Stop
                            End Try
                        End If
                    End If
                Next
            Next
        ElseIf cmbChart.Text = "Weeks" Then
            chtMain.Dimensions = 0
            For i = 1 To Campaign.Channels(1).BookingTypes(1).Weeks.Count
                chtMain.Dimensions = chtMain.Dimensions + 1
                chtMain.DimensionLabel(chtMain.Dimensions - 1) = Campaign.Channels(1).BookingTypes(1).Weeks(i).Name
                For dp = 0 To Campaign.Dayparts.Count - 1
                    For Each TmpChan In Campaign.Channels
                        For Each TmpBT In TmpChan.BookingTypes
                            If TmpBT.BookIt Then
                                TmpWeek = TmpBT.Weeks(i)
                                'Added to ger correct CPP/TRP in monitor if you dont open Allocate window **********
                                TmpWeek.RecalculateCPP()
                                For Each TmpFilm In TmpWeek.Films
                                    Select Case cmbTarget.SelectedIndex
                                        Case 0 : TRP = TmpWeek.TRP
                                        Case 1 : TRP = TmpWeek.TRP * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                                        Case 2 : TRP = TmpWeek.TRPBuyingTarget
                                        Case 3 : TRP = TmpWeek.TRPAllAdults
                                    End Select
                                    If cmbDisplayType.SelectedIndex = 2 Then
                                        TRP *= TmpFilm.Index / 100
                                    End If
                                    If FilterIn(TmpChan.ChannelName, TmpBT.Name, dp, TmpFilm.Name, TmpWeek.Name) Then
                                        chtMain.Planned(chtMain.Dimensions - 1) = chtMain.Planned(chtMain.Dimensions - 1) + (TmpFilm.Share / 100) * TRP * (TmpBT.MainDaypartSplit(dp) / 100)
                                    End If
                                Next
                                For Each TmpComp As Trinity.cCompensation In TmpBT.Compensations
                                    Dim countActualDays As Integer = 0
                                    'count the actual days the campaign will run in the compensation
                                    For Each w As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                                        For day As Integer = w.StartDate To w.EndDate
                                            If day >= TmpComp.FromDate.ToOADate AndAlso day <= TmpComp.ToDate.ToOADate Then
                                                countActualDays += 1
                                            End If
                                        Next
                                    Next


                                    If FilterIn(TmpChan.ChannelName, TmpBT.Name, , , TmpWeek.Name) Then
                                        Select Case cmbTarget.SelectedIndex
                                            Case 0
                                                TRP = TmpComp.TRPMainTarget
                                            Case 1
                                                TRP = TmpComp.TRPMainTarget * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                                            Case 2
                                                TRP = TmpComp.TRPs
                                            Case 3
                                                TRP = TmpComp.TRPMainTarget * (TmpBT.IndexAllAdults / TmpBT.IndexMainTarget)
                                        End Select

                                        Dim iz As Integer
                                        For iz = TmpWeek.StartDate To TmpWeek.EndDate
                                            If iz >= TmpComp.FromDate.ToOADate AndAlso iz <= TmpComp.ToDate.ToOADate Then
                                                chtMain.Planned(chtMain.Dimensions - 1) += TRP * (1 / countActualDays) * (TmpBT.MainDaypartSplit(dp) / 100)
                                            End If
                                        Next
                                    End If



                                    'For d As Integer = TmpComp.FromDate.ToOADate To TmpComp.ToDate.ToOADate
                                    '    If TmpBT.GetWeek(Date.FromOADate(d)) Is TmpWeek Then
                                    '        If FilterIn(TmpChan.ChannelName, TmpBT.Name, , , TmpBT.GetWeek(Date.FromOADate(d)).Name) Then
                                    '            Select Case cmbTarget.SelectedIndex
                                    '                Case 0
                                    '                    TRP = TmpComp.TRPMainTarget
                                    '                Case 1
                                    '                    TRP = TmpComp.TRPMainTarget * (TmpBT.IndexSecondTarget / TmpBT.IndexMainTarget)
                                    '                Case 2
                                    '                    TRP = TmpComp.TRPs
                                    '                Case 3
                                    '                    TRP = TmpComp.TRPMainTarget * (TmpBT.IndexAllAdults / TmpBT.IndexMainTarget)
                                    '            End Select
                                    '            TRP = (TRP / (TmpComp.ToDate.ToOADate - TmpComp.FromDate.ToOADate + 1)) * (TmpBT.MainDaypartSplit(dp) / 100)
                                    '            chtMain.Planned(i - 1) += TRP
                                    '        End If
                                    '    End If
                                    'Next
                                Next
                            End If
                        Next
                    Next
                Next
                For s = 1 To Campaign.BookedSpots.Count
                    If Campaign.BookedSpots(s).Bookingtype.Weeks(i) Is Campaign.BookedSpots(s).week Then
                        Try
                            If cmbTarget.SelectedIndex = 2 Then
                                TRP = Campaign.BookedSpots(s).MyEstimateBuyTarget
                            Else
                                TRP = Campaign.BookedSpots(s).MyEstimate
                            End If
                            If cmbDisplayType.SelectedIndex = 2 Then
                                TRP *= Campaign.BookedSpots(s).Film.Index / 100
                            End If
                            If FilterIn(Campaign.BookedSpots(s).Channel.ChannelName, Campaign.BookedSpots(s).Bookingtype.Name, Campaign.Dayparts.GetDaypartIndexForMam(Campaign.BookedSpots(s).MaM), Campaign.BookedSpots(s).Film.Name, Campaign.BookedSpots(s).week.Name) Then
                                chtMain.Booked(i - 1) += TRP
                            End If
                        Catch
                            'Stop
                        End Try
                    End If
                Next
            Next
        End If

        'planned spots
        If cmbDisplayType.SelectedIndex = 1 Then
            Dim sum As Double = 0
            For z As Integer = 0 To chtMain.Dimensions - 1
                sum += chtMain.Planned(z)
            Next
            For z As Integer = 0 To chtMain.Dimensions - 1
                If Not chtMain.Planned(z) = 0 Then
                    chtMain.Planned(z) = chtMain.Planned(z) / sum * 100
                End If
            Next
        End If

        'booked spots
        If cmbDisplayType.SelectedIndex = 1 Then
            Dim sum As Double = 0
            For z As Integer = 0 To chtMain.Dimensions - 1
                sum += chtMain.Booked(z)
            Next
            For z As Integer = 0 To chtMain.Dimensions - 1
                If Not chtMain.Booked(z) = 0 Then
                    chtMain.Booked(z) = chtMain.Booked(z) / sum * 100
                End If
            Next
        End If
    End Sub

    Sub UpdateConfirmed()
        Dim TmpSpot As Trinity.cPlannedSpot
        Dim i As Integer
        Dim TRP As Single
        For Each TmpSpot In Campaign.PlannedSpots
            If TmpSpot.AirDate > Campaign.UpdatedTo Then
                If FilterIn(TmpSpot) Then
                    If cmbTarget.Text = "Main Target" Then
                        TRP = TmpSpot.MyRating
                    ElseIf cmbTarget.Text = "Secondary Target" Then
                        TRP = TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget) '* (TmpSpot.Bookingtype.IndexSecondTarget / TmpSpot.Bookingtype.IndexMainTarget)
                    ElseIf cmbTarget.Text = "Buying Target" Then
                        TRP = TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
                    Else
                        TRP = TmpSpot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteAllAdults) '* (TmpSpot.Bookingtype.IndexAllAdults / TmpSpot.Bookingtype.IndexMainTarget)
                    End If
                    If cmbDisplayType.SelectedIndex = 2 Then
                        TRP *= TmpSpot.Film.Index / 100
                    End If
                    If cmbChart.Text = "Allocation" Then
                        For i = 0 To chtMain.Dimensions - 1
                            If TmpSpot.Bookingtype.ShowMe Then
                                If chtMain.DimensionLabel(i) = TmpSpot.Channel.Shortname & " " & TmpSpot.Bookingtype.Shortname & " (" & TmpSpot.Bookingtype.BuyingTarget.Target.TargetNameNice & ")" OrElse chtMain.DimensionLabel(i) = TmpSpot.Channel.Shortname & " " & TmpSpot.Bookingtype.Shortname Then
                                    chtMain.Confirmed(i) = chtMain.Confirmed(i) + TRP
                                End If
                            Else
                                For Each comb As Trinity.cCombination In Campaign.Combinations
                                    If InStr(chtMain.DimensionLabel(i), comb.Name) > 0 Then
                                        For Each rel As Trinity.cCombinationChannel In comb.Relations
                                            If rel.Bookingtype Is TmpSpot.Bookingtype Then
                                                chtMain.Confirmed(i) = chtMain.Confirmed(i) + TRP
                                                Exit For
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                    ElseIf cmbChart.Text = "Total" Then
                        chtMain.Confirmed(0) += TRP
                    ElseIf cmbChart.Text = "Channels" Then
                        For i = 0 To chtMain.Dimensions - 1
                            If chtMain.DimensionLabel(i) = TmpSpot.Channel.ChannelName Then
                                chtMain.Confirmed(i) = chtMain.Confirmed(i) + TRP
                            End If
                        Next
                    ElseIf cmbChart.Text = "Weekdays" Then
                        Dim WD() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}
                        For i = 0 To chtMain.Dimensions - 1
                            If chtMain.DimensionLabel(i) = WD(Weekday(Date.FromOADate(TmpSpot.AirDate), vbMonday) - 1) Then
                                chtMain.Confirmed(i) = chtMain.Confirmed(i) + TRP
                            End If
                        Next
                    ElseIf cmbChart.Text = "Dayparts" Then
                        For i = 0 To chtMain.Dimensions - 1
                            If chtMain.DimensionLabel(i) = Campaign.Dayparts.GetDaypartForMam(TmpSpot.MaM).Name Then
                                chtMain.Confirmed(i) = chtMain.Confirmed(i) + TRP
                            End If
                        Next
                    ElseIf cmbChart.Text = "Films" Then
                        For i = 0 To chtMain.Dimensions - 1
                            If chtMain.DimensionLabel(i) = TmpSpot.Film.Name Then
                                chtMain.Confirmed(i) = chtMain.Confirmed(i) + TRP
                            End If
                        Next
                    ElseIf cmbChart.Text = "Weeks" Then
                        For i = 0 To chtMain.Dimensions - 1
                            If chtMain.DimensionLabel(i) = TmpSpot.Week.Name Then
                                chtMain.Confirmed(i) = chtMain.Confirmed(i) + TRP
                            End If
                        Next
                    End If
                End If
            End If
        Next

        If cmbDisplayType.SelectedIndex = 1 Then
            Dim sum As Double = 0
            For z As Integer = 0 To chtMain.Dimensions - 1
                sum += chtMain.Confirmed(z)
            Next
            For z As Integer = 0 To chtMain.Dimensions - 1
                If Not chtMain.Confirmed(z) = 0 Then
                    chtMain.Confirmed(z) = chtMain.Confirmed(z) / sum * 100
                End If
            Next
        End If
    End Sub

    Sub UpdateActual()
        Dim TmpSpot As Trinity.cActualSpot
        Dim i As Integer
        Dim TRP As Single

        Campaign.CalculateSpots()
        If cmbChart.Text = "Pos In Break" Then
            Dim CountArray() As String = {"2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th"}
            chtMain.Dimensions = 0
            chtMain.Dimensions = TrinitySettings.PPFirst + TrinitySettings.PPLast + 1
            chtMain.DimensionLabel(0) = "First"
            For i = 1 To TrinitySettings.PPFirst - 1
                chtMain.DimensionLabel(i) = CountArray(i - 1) & " First"
            Next
            chtMain.DimensionLabel(TrinitySettings.PPFirst) = "Middle"
            For i = 1 To TrinitySettings.PPLast - 1
                chtMain.DimensionLabel(TrinitySettings.PPFirst + TrinitySettings.PPLast - i) = CountArray(i - 1) & " Last"
            Next
            chtMain.DimensionLabel(TrinitySettings.PPFirst + TrinitySettings.PPLast) = "Last"
        ElseIf cmbChart.Text = "Break Type" Then
            chtMain.Dimensions = 0
            chtMain.Dimensions = 2
            chtMain.DimensionLabel(0) = "Block"
            chtMain.DimensionLabel(1) = "Break"
        End If
        Dim tempsum As Double = 0
        Dim _notSetChannels As New List(Of String)
        For Each TmpSpot In Campaign.ActualSpots
            If FilterIn(TmpSpot) Then
                If cmbDisplayType.SelectedIndex = 2 Then
                    If cmbTarget.Text = "Main Target" Then
                        TRP = TmpSpot.Rating30
                    ElseIf cmbTarget.Text = "Secondary Target" Then
                        TRP = TmpSpot.Rating30(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget)
                    ElseIf cmbTarget.Text = "Buying Target" Then
                        TRP = TmpSpot.Rating30(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget)
                    Else
                        TRP = TmpSpot.Rating30(Trinity.cActualSpot.ActualTargetEnum.ateAllAdults)
                    End If
                Else
                    If cmbTarget.Text = "Main Target" Then
                        TRP = TmpSpot.Rating
                    ElseIf cmbTarget.Text = "Secondary Target" Then
                        TRP = TmpSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget)
                    ElseIf cmbTarget.Text = "Buying Target" Then
                        TRP = TmpSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget)
                    Else
                        TRP = TmpSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateAllAdults)
                    End If
                End If

                tempsum += TRP
                If cmbChart.Text = "Allocation" Then
                    For i = 0 To chtMain.Dimensions - 1
                        If TmpSpot.Bookingtype.ShowMe Then
                            If chtMain.DimensionLabel(i) = TmpSpot.Channel.Shortname & " " & TmpSpot.Bookingtype.Shortname OrElse chtMain.DimensionLabel(i) = TmpSpot.Channel.Shortname & " " & TmpSpot.Bookingtype.Shortname & " (" & TmpSpot.Bookingtype.BuyingTarget.Target.TargetNameNice & ")" Then
                                chtMain.Actual(i) = chtMain.Actual(i) + TRP
                            End If
                        Else
                            For Each comb As Trinity.cCombination In Campaign.Combinations
                                If chtMain.DimensionLabel(i).StartsWith(comb.Name) Then
                                    For Each rel As Trinity.cCombinationChannel In comb.Relations
                                        If rel.Bookingtype Is TmpSpot.Bookingtype Then
                                            chtMain.Actual(i) = chtMain.Actual(i) + TRP
                                            Exit For
                                        End If
                                    Next
                                End If
                            Next
                        End If

                    Next
                ElseIf cmbChart.Text = "Total" Then
                    chtMain.Actual(0) += TRP
                ElseIf cmbChart.Text = "Channels" Then
                    Dim isset As Boolean = False
                    For i = 0 To chtMain.Dimensions - 1
                        If chtMain.DimensionLabel(i) = TmpSpot.Channel.ChannelName Then
                            chtMain.Actual(i) = chtMain.Actual(i) + TRP
                            isset = True
                        End If
                    Next
                    If Not isset AndAlso Not _notSetChannels.Contains(TmpSpot.Channel.ChannelName) Then
                        _notSetChannels.Add(TmpSpot.Channel.ChannelName)
                    End If
                ElseIf cmbChart.Text = "Weekdays" Then
                    Dim WD() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}
                    For i = 0 To chtMain.Dimensions - 1
                        If chtMain.DimensionLabel(i) = WD(Weekday(Date.FromOADate(TmpSpot.AirDate), vbMonday) - 1) Then
                            chtMain.Actual(i) = chtMain.Actual(i) + TRP
                        End If
                    Next
                ElseIf cmbChart.Text = "Dayparts" Then
                    For i = 0 To chtMain.Dimensions - 1
                        If chtMain.DimensionLabel(i) = Campaign.Dayparts.GetDaypartForMam(TmpSpot.MaM).Name Then
                            chtMain.Actual(i) = chtMain.Actual(i) + TRP
                        End If
                    Next
                ElseIf cmbChart.Text = "Films" Then
                    For i = 0 To chtMain.Dimensions - 1
                        If chtMain.DimensionLabel(i) = TmpSpot.Week.Films(TmpSpot.Filmcode).Name Then
                            chtMain.Actual(i) = chtMain.Actual(i) + TRP
                        End If
                    Next
                ElseIf cmbChart.Text = "Weeks" Then
                    For i = 0 To chtMain.Dimensions - 1
                        If chtMain.DimensionLabel(i) = TmpSpot.Week.Name Then
                            chtMain.Actual(i) = chtMain.Actual(i) + TRP
                        End If
                    Next
                ElseIf cmbChart.Text = "Pos In Break" Then
                    If TmpSpot.PosInBreak <= TrinitySettings.PPFirst Then
                        chtMain.Actual(TmpSpot.PosInBreak - 1) += TRP
                    ElseIf TmpSpot.PosInBreak >= TmpSpot.SpotsInBreak - TrinitySettings.PPLast + 1 Then
                        chtMain.Actual(chtMain.Dimensions - (TmpSpot.SpotsInBreak - TmpSpot.PosInBreak + 1)) += TRP
                    Else
                        chtMain.Actual(TrinitySettings.PPFirst) += TRP
                    End If
                ElseIf cmbChart.Text = "Break Type" Then
                    chtMain.Actual(TmpSpot.BreakType) = chtMain.Actual(TmpSpot.BreakType) + TRP
                End If
            End If
        Next
        If _notSetChannels.Count > 0 Then
            Windows.Forms.MessageBox.Show("There are spots on the following channels that do not belong to the campaign:" & vbNewLine & vbNewLine & String.Join(vbNewLine, _notSetChannels.ToArray), "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        End If
        If cmbDisplayType.SelectedIndex = 1 Then
            Dim sum As Double = 0
            For z As Integer = 0 To chtMain.Dimensions - 1
                sum += chtMain.Actual(z)
            Next
            For z As Integer = 0 To chtMain.Dimensions - 1
                If Not chtMain.Actual(z) = 0 Then
                    chtMain.Actual(z) = chtMain.Actual(z) / sum * 100
                End If
            Next
        End If
    End Sub

    Private Sub frmMonitor_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        cmbChart_SelectedIndexChanged(sender, e)
        lblUpdatedTo.Text = "Updated To: " & Date.FromOADate(Campaign.UpdatedTo)
    End Sub

    Private Sub frmMonitor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cmbTarget.SelectedIndex = 0
        cmbChart.SelectedIndex = TrinitySettings.DefaultMonitorChart
        cmbShowGoal.SelectedIndex = 0
        cmbDisplayType.SelectedIndex = 0
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
        If Spot.Week Is Nothing OrElse Spot.Week.Films(Spot.Filmcode) Is Nothing OrElse Not GeneralFilter.Data("Film", Spot.Week.Films(Spot.Filmcode).Name) Then
            Return False
        End If
        If Not GeneralFilter.Data("Week", Spot.Week.Name) Then
            Return False
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

    Function FilterIn(Optional ByVal Channel As String = Nothing, Optional ByVal Bookingtype As String = Nothing, Optional ByVal Daypart As Integer = -1, Optional ByVal Film As String = Nothing, Optional ByVal Week As String = Nothing) As Boolean
        If Not Channel Is Nothing AndAlso Not GeneralFilter.Data("Channels", Channel) Then
            Return False
        End If
        If Not Bookingtype Is Nothing AndAlso Not GeneralFilter.Data("Bookingtype", Bookingtype) Then
            Return False
        End If
        If Not Daypart = -1 AndAlso Not GeneralFilter.Data("Daypart", Campaign.Dayparts(Daypart).Name) Then
            Return False
        End If
        If Not Film Is Nothing AndAlso Not GeneralFilter.Data("Film", Film) Then
            Return False
        End If
        If Not Week Is Nothing AndAlso Not GeneralFilter.Data("Week", Week) Then
            Return False
        End If

        Return True

    End Function


    Private Sub cmdFilter_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFilter.DropDownOpening
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpFilm As Trinity.cFilm
        Dim TmpWeek As Trinity.cWeek
        Dim WeekDays() As String = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}

        Dim IsUsed As Boolean
        Dim i As Integer

        cmdFilter.DropDownItems.Clear()
        With DirectCast(cmdFilter.DropDownItems.Add("Channels"), Windows.Forms.ToolStripMenuItem)
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
            .DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
            With DirectCast(.DropDownItems.Add("Invert selection"), Windows.Forms.ToolStripMenuItem)
                .Tag = "Channels"
                .Checked = False
                AddHandler .Click, AddressOf ChangeFilter
            End With
        End With
        With DirectCast(cmdFilter.DropDownItems.Add("Weekdays"), Windows.Forms.ToolStripMenuItem)
            For i = 0 To 6
                With DirectCast(.DropDownItems.Add(WeekDays(i)), Windows.Forms.ToolStripMenuItem)
                    .Tag = "Weekday"
                    .Checked = GeneralFilter.Data("Weekday", WeekDays(i))
                    AddHandler .Click, AddressOf ChangeFilter
                End With
            Next
            .DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
            With DirectCast(.DropDownItems.Add("Invert selection"), Windows.Forms.ToolStripMenuItem)
                .Tag = "Weekday"
                .Checked = False
                AddHandler .Click, AddressOf ChangeFilter
            End With
        End With
        With DirectCast(cmdFilter.DropDownItems.Add("Dayparts"), Windows.Forms.ToolStripMenuItem)
            For i = 0 To Campaign.Dayparts.Count - 1
                With DirectCast(.DropDownItems.Add(Campaign.Dayparts(i).Name), Windows.Forms.ToolStripMenuItem)
                    .Tag = "Daypart"
                    .Checked = GeneralFilter.Data("Daypart", Campaign.Dayparts(i).Name)
                    AddHandler .Click, AddressOf ChangeFilter
                End With
            Next
            .DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
            With DirectCast(.DropDownItems.Add("Invert selection"), Windows.Forms.ToolStripMenuItem)
                .Tag = "Daypart"
                .Checked = False
                AddHandler .Click, AddressOf ChangeFilter
            End With
        End With
        With DirectCast(cmdFilter.DropDownItems.Add("Films"), Windows.Forms.ToolStripMenuItem)
            For Each TmpFilm In Campaign.Channels(1).BookingTypes(1).Weeks(1).Films
                With DirectCast(.DropDownItems.Add(TmpFilm.Name), Windows.Forms.ToolStripMenuItem)
                    .Tag = "Film"
                    .Checked = GeneralFilter.Data("Film", TmpFilm.Name)
                    AddHandler .Click, AddressOf ChangeFilter
                End With
            Next
            .DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
            With DirectCast(.DropDownItems.Add("Invert selection"), Windows.Forms.ToolStripMenuItem)
                .Tag = "Film"
                .Checked = False
                AddHandler .Click, AddressOf ChangeFilter
            End With
        End With
        With DirectCast(cmdFilter.DropDownItems.Add("Weeks"), Windows.Forms.ToolStripMenuItem)
            For Each TmpWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                With DirectCast(.DropDownItems.Add(TmpWeek.Name), Windows.Forms.ToolStripMenuItem)
                    .Tag = "Week"
                    .Checked = GeneralFilter.Data("Week", TmpWeek.Name)
                    AddHandler .Click, AddressOf ChangeFilter
                End With
            Next
            .DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
            With DirectCast(.DropDownItems.Add("Invert selection"), Windows.Forms.ToolStripMenuItem)
                .Tag = "Week"
                .Checked = False
                AddHandler .Click, AddressOf ChangeFilter
            End With
        End With

        With DirectCast(cmdFilter.DropDownItems.Add("Bookingtypes"), Windows.Forms.ToolStripMenuItem)
            Dim BTList As New List(Of String)
            For Each TmpChan In Campaign.Channels
                For Each TmpBT In TmpChan.BookingTypes
                    If Not BTList.Contains(TmpBT.Name) Then
                        BTList.Add(TmpBT.Name)
                    End If
                Next
            Next
            For Each s As String In BTList
                With DirectCast(.DropDownItems.Add(s), Windows.Forms.ToolStripMenuItem)
                    .Tag = "Bookingtype"
                    .Checked = GeneralFilter.Data("Bookingtype", s)
                    AddHandler .Click, AddressOf ChangeFilter
                End With
            Next
            .DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
            With DirectCast(.DropDownItems.Add("Invert selection"), Windows.Forms.ToolStripMenuItem)
                .Tag = "Bookingtype"
                .Checked = False
                AddHandler .Click, AddressOf ChangeFilter
            End With
        End With
        With DirectCast(cmdFilter.DropDownItems.Add("Position in break"), Windows.Forms.ToolStripMenuItem)
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
            .DropDownItems.Add(New Windows.Forms.ToolStripSeparator)
            With DirectCast(.DropDownItems.Add("Invert selection"), Windows.Forms.ToolStripMenuItem)
                .Tag = "PIB"
                .Checked = False
                AddHandler .Click, AddressOf ChangeFilter
            End With
        End With

        With cmdFilter.DropDownItems.Add("No filters")
            AddHandler .Click, AddressOf NoFilters
        End With

    End Sub

    Sub NoFilters(ByVal sender As Object, ByVal e As EventArgs)
        GeneralFilter = New Trinity.cFilter
        cmbChart_SelectedIndexChanged(New Object, New EventArgs)
    End Sub

    Sub ChangeFilter(ByVal sender As Object, ByVal e As EventArgs)
        If sender.text = "Invert selection" Then
            For Each TmpItem As Windows.Forms.ToolStripItem In DirectCast(sender.OwnerItem, Windows.Forms.ToolStripMenuItem).DropDownItems
                If Not TmpItem Is sender AndAlso Not TmpItem.GetType.Name = "ToolStripSeparator" Then
                    GeneralFilter.Data(TmpItem.Tag, TmpItem.Text) = Not GeneralFilter.Data(TmpItem.Tag, TmpItem.Text)
                End If
            Next
            cmbChart_SelectedIndexChanged(New Object, New EventArgs)
            If frmInfo.Visible Then
                frmInfo.grdReach.Invalidate()
            End If
        Else
            GeneralFilter.Data(sender.tag, sender.text) = Not sender.checked
            cmbChart_SelectedIndexChanged(New Object, New EventArgs)
            If frmMain.pnlInfo.Visible Then
                frmMain.grdReach.Invalidate()
            Else
                If frmInfo.Visible Then
                    frmInfo.grdReach.Invalidate()
                End If
            End If
        End If
    End Sub


    Private Sub cmdFrequency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFrequency.Click

    End Sub

    Private Sub cmdFrequency_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFrequency.DropDownOpening
        Dim i As Integer
        cmdFrequency.DropDownItems.Clear()
        For i = 1 To 10
            With DirectCast(cmdFrequency.DropDownItems.Add(i & "+", Nothing, AddressOf ChangeShowFrequency), System.Windows.Forms.ToolStripMenuItem)
                .Checked = chtReach.ShowReach(i)
                .Tag = i
            End With
        Next
    End Sub

    Sub ChangeShowFrequency(ByVal sender As Object, ByVal e As EventArgs)
        chtReach.ShowReach(sender.tag) = Not chtReach.ShowReach(sender.tag)
    End Sub

    Private Sub cmbShowGoal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbShowGoal.SelectedIndexChanged
        If cmbShowGoal.SelectedIndex = 0 Then
            chtReach.ShowBuildup = True
            chtReach.ShowIntersect = False
        ElseIf cmbShowGoal.SelectedIndex = 1 Then
            chtReach.ShowBuildup = False
            chtReach.ShowIntersect = True
        Else
            chtReach.ShowBuildup = True
            chtReach.ShowIntersect = True
        End If
        chtReach.UpdateChart()
    End Sub

    Private Sub cmbTarget_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTarget.SelectedIndexChanged
        cmbChart_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub cmbDisplayType_selctedindexchange(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDisplayType.SelectedIndexChanged
        UpdatePlanned()
        UpdateConfirmed()
        UpdateActual()
        chtMain.UpdateChart()
        chtMain.BringToFront()
    End Sub

    Private Sub cmdPictureToCB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPictureToCB.Click
        If cmbChart.SelectedItem = "Reach" Then
            'chtReach.
        Else
            chtMain.CopyToClipboard()
        End If
    End Sub

    Private Sub cmdFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFilter.Click

    End Sub

    Private Sub VOSDALToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DefaultToolStripMenuItem.Click, LiveToolStripMenuItem.Click, VOSDAL7ToolStripMenuItem.Click
        Dim _mnu As Windows.Forms.ToolStripMenuItem = sender
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        My.Application.DoEvents()
        Campaign.TimeShift = _mnu.Tag

        cmbChart_SelectedIndexChanged(Nothing, Nothing)
        Me.Cursor = Windows.Forms.Cursors.Default
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