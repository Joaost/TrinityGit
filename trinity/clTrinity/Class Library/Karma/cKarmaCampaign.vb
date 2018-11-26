Namespace Trinity

    Class _cKarmaCampaign

        Const Iterations = 100

        Private mvarReach(,,) As Single
        Private mvarTRP() As Single
        Private mvarB1(0 To 10) As Single
        Private mvarB2(0 To 10) As Single
        Public Name As String
        Public TrinityCampaign As cKampanj
        Private mvarProfileTRP(0 To 14) As Single
        Dim Adedge As New ConnectWrapper.Brands
        Private PickedSpots(Iterations) As SortedDictionary(Of String, Long)

        Public Parent As _cKarmaCampaigns
        Public Event Progress(ByVal p As Single)

        Private Structure MaxedOutStruct
            Public PickedTRP As Single
            Public PieceTRP As Single
            Public Channel As String
            Public Daypart As String
        End Structure

        Private _runCancelled As Boolean = False
        Public Sub CancelRun()
            _runCancelled = True
        End Sub

        Public Function GetReachAtTRP(ByVal TRP As Single, ByVal Freq As Integer) As Single
            Dim ReachSum As Single = 0
            For c As Integer = 1 To Iterations
                Parent.Karma.KarmaAdedge.clearGroup()
                For Each kv As KeyValuePair(Of String, Long) In PickedSpots(c)
                    Parent.Karma.KarmaAdedge.addToGroup(kv.Value)
                Next
                Parent.Karma.KarmaAdedge.recalcRF(Connect.eSumModes.smGroup)

                Dim CurrentTRP As Single = 0
                Dim s As Integer = 0

                While s <= Parent.Karma.KarmaAdedge.getGroupCount - 1 AndAlso TRP > CurrentTRP
                    CurrentTRP += Parent.Karma.KarmaAdedge.getUnitGroup(Connect.eUnits.uTRP, s, 0, 0, 0)
                    s += 1
                End While
                ReachSum += Parent.Karma.KarmaAdedge.getRF(s, , , , Freq)
            Next
            Return ReachSum / Iterations
        End Function

        'Sub to calculate the estimated reach for a campaign
        Public Sub Run(Optional ByVal WeekNumber As Integer = 0)
            Dim Diff As Single
            Dim PickedTRP As Single 'Variable containing how many TRPs was picked each Channel, Week and Daypart
            Dim PieceTRP As Single ' Variable containing the target TRPs for each Channel, week and Daypart
            Dim c As Integer
            Dim i As Integer
            Dim TmpBT As cBookingType
            Dim CampTRP As Single 'The total amount of TRPs to be allocated for each Campaign
            Dim DP As Integer
            Dim r As Long
            Dim UseCampaign As cKampanj
            Dim kv As KeyValuePair(Of String, Long)
            Dim PickedInDaypart(Parent.Karma.Channels.Count, Parent.Karma.Channels(1).Weeks.Count, 10) As Integer ' How many spots is picked in each Channel, Week and Daypart?
            Dim MaxedOut As Boolean = False
            Dim MaxedOutList As New Dictionary(Of String, MaxedOutStruct) 'List of Pieces that we didn't manage to fill up with TRPs before we ran out of spots
            Dim FromWeek As Integer
            Dim ToWeek As Integer
            Dim PickedSpecificsSponsorTRPs As Single

            If WeekNumber > 0 Then
                FromWeek = 1
                ToWeek = 1
            Else
                FromWeek = 1
                ToWeek = Parent.Karma.Channels(1).Weeks.Count
            End If

            'r = Rnd(-1)
            Randomize(1000)

            ReDim mvarReach(0 To Iterations, 0 To 10, 0 To 1)
            ReDim mvarTRP(0 To Iterations)
            Dim HighestReach(0 To 10) As Single
            Dim LowestReach(0 To 10) As Single
            For i = 0 To 10
                LowestReach(i) = 100
            Next

            UseCampaign = TrinityCampaign

            If WeekNumber = 0 Then
                CampTRP = UseCampaign.TotalTRP
            Else
                CampTRP = 0
                For Each TmpChan As Trinity.cChannel In UseCampaign.Channels
                    For Each TmpBT In TmpChan.BookingTypes
                        If TmpBT.BookIt AndAlso Not (TmpBT.IsSponsorship AndAlso TmpBT.IsSpecific) Then 'Do not include non booked TRPs. Also skip Specific sponsorships. We will get those TRPs from the reference period.
                            CampTRP += TmpBT.Weeks(WeekNumber).TRP
                            For Each TmpComp As cCompensation In TmpBT.Compensations
                                Dim Length As Integer = TmpComp.ToDate.ToOADate - TmpComp.FromDate.ToOADate + 1
                                Dim TRP As Single = (TmpComp.TRPs / Length) * (TmpBT.IndexMainTarget / 100)
                                For d As Long = TmpComp.FromDate.ToOADate To TmpComp.ToDate.ToOADate
                                    If d >= TmpBT.Weeks(WeekNumber).StartDate AndAlso d <= TmpBT.Weeks(WeekNumber).EndDate Then
                                        CampTRP += TRP
                                    End If
                                Next
                            Next
                        End If
                    Next
                Next
            End If
            For c = 1 To Iterations 'The more iterations - the more accurate the result. Each iteration creates a random spotlist mirroring the users Campaign and calculates the reach
                RaiseEvent Progress((c / Iterations) * 100)

                PickedSpecificsSponsorTRPs = 0

                If _runCancelled Then
                    _runCancelled = False
                    Exit Sub
                End If
                Diff = 0
                PickedSpots(c) = New SortedDictionary(Of String, Long) 'Reset spotlist for this iteration
                For i = 1 To Parent.Karma.Channels.Count
                    For Each TmpBT In UseCampaign.Channels(Parent.Karma.Channels(i).Name).BookingTypes
                        If TmpBT.BookIt Then
                            For DP = 0 To Campaign.Dayparts.Count - 1
                                For w As Integer = FromWeek To ToWeek
                                    If TmpBT.Weeks(w).TRP > 0 Then 'Are there any TRPs that week?
                                        If Not (TmpBT.IsSponsorship AndAlso TmpBT.IsSpecific) Then
                                            PickedInDaypart(i, w, DP + 1) = 0 'Reset counter for number of picked spots
                                            Dim WN As Integer
                                            'Determine how many TRPs we are to pick for this Channel, Week and Daypart
                                            If WeekNumber > 0 Then
                                                PieceTRP = TmpBT.Weeks(WeekNumber).TRP * (TmpBT.MainDaypartSplit(DP) / 100)
                                                WN = WeekNumber
                                            Else
                                                PieceTRP = TmpBT.Weeks(w).TRP * (TmpBT.MainDaypartSplit(DP) / 100)
                                                WN = w
                                            End If
                                            'Include Compensation TRPs
                                            For Each TmpComp As cCompensation In TmpBT.Compensations
                                                Dim Length As Integer = TmpComp.ToDate.ToOADate - TmpComp.FromDate.ToOADate + 1
                                                Dim TRP As Single = ((TmpComp.TRPs / Length) * (TmpBT.MainDaypartSplit(DP) / 100)) * (TmpBT.IndexMainTarget / 100)
                                                For d As Long = TmpComp.FromDate.ToOADate To TmpComp.ToDate.ToOADate
                                                    If d >= TmpBT.Weeks(WN).StartDate AndAlso d <= TmpBT.Weeks(WN).EndDate Then
                                                        PieceTRP += TRP
                                                    End If
                                                Next
                                            Next
                                            PickedTRP = 0
                                            Dim FailedAttempts As Integer = 0
                                            'Keep picking spots as long as we have less TRPs (PickedTRP) than our goal (PieceTRP) and as long as the number of picked spots are less than the number of available spots. If we have failed to find a decent spot 100 times, we also move on.
                                            While Int(PickedTRP) < Int(PieceTRP) And PickedInDaypart(i, w, DP + 1) < Parent.Karma.Channels(i).Weeks(w).Dayparts(DP + 1).Spots.Count - 1 AndAlso FailedAttempts < 100
                                                'Are there any spots to pick from?
                                                If Parent.Karma.Channels(i).Weeks(w).Dayparts(DP + 1).Spots.Count > 0 Then
                                                    'Yes, there are spots to pick from, so pick some
                                                    r = Int(Rnd() * Parent.Karma.Channels(i).Weeks(w).Dayparts(DP + 1).Spots.Count) 'Get a random spot from the list

                                                    If Not TmpBT.IsSponsorship Then
                                                        'If the BT is not a Sponsorship, then pick spots
                                                        r = Parent.Karma.Channels(i).Weeks(w).Dayparts(DP + 1).Spots(r) 'Get the index of the chosen spot
                                                        If Not PickedSpots(c).ContainsKey("S" & r) Then 'Check if we picked this spot before
                                                            'If we haven't picked it before, add it's TRP to PickedTRP
                                                            If Campaign.MainTarget.Universe = "" Then
                                                                PickedTRP += Parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, r)
                                                            Else
                                                                PickedTRP += Parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, r, , 1)
                                                            End If
                                                            'Add the spotnumber to the list of picked spots and increase number of picked spots for the daypart by one
                                                            PickedSpots(c).Add("S" & r, r)
                                                            PickedInDaypart(i, w, DP + 1) += 1
                                                        End If
                                                    Else
                                                        'Check if there are any sponsorship spots to pick from
                                                        If Parent.Karma.Channels(i).Weeks(w).Dayparts(DP + 1).Sponsorships.Count > 0 Then
                                                            r = Int(Rnd() * Parent.Karma.Channels(i).Weeks(w).Dayparts(DP + 1).Sponsorships.Count) ' Get a random spons spot
                                                            Dim TmpList As List(Of Long) = GetSponsorship(r, Parent.Karma.Channels(i).Weeks(w).Dayparts(DP + 1), TmpBT.ParentChannel.UseBillboards, TmpBT.ParentChannel.UseBreakBumpers) 'Get all spons breaks for chosen program
                                                            'Check if we found any spons spots in the break and that this break is not chosen before
                                                            If TmpList.Count > 0 AndAlso Not PickedSpots(c).ContainsKey("S" & TmpList(0)) Then ' AndAlso Parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, TmpList(0), , , 1) < 3 Then
                                                                Dim TotTRP As Single = 0 'What is the total rating of all spons spots in the program
                                                                For Each TmpR As Long In TmpList
                                                                    TotTRP += Parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, TmpR)
                                                                    If c = 1 Then
                                                                        Debug.Print(Date.FromOADate(Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aDate, TmpR)) & vbTab & _
                                                                            Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aChannel, TmpR) & vbTab & _
                                                                            Parent.Karma.KarmaAdedge.convertTime(Connect.eTimeModes.tmSecNice, Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aFromTime, TmpR)) & vbTab & _
                                                                            Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, TmpR) & vbTab & _
                                                                            Parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, TmpR))
                                                                    End If
                                                                Next
                                                                'Check that the PickedTRP doesn't end over 10% over PieceTRP
                                                                If (Int(PickedTRP + TotTRP) <= Int(PieceTRP * 1.1)) OrElse FailedAttempts > 19 Then
                                                                    For Each TmpR As Long In TmpList
                                                                        Try
                                                                            'If a spot has already been picked, decrease the picked TRP, otherwise add it to the list of picked spots
                                                                            If PickedSpots(c).ContainsKey("S" & TmpR) Then
                                                                                TotTRP -= Parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, TmpR)
                                                                            Else
                                                                                PickedSpots(c).Add("S" & TmpR, TmpR)
                                                                            End If
                                                                        Catch
                                                                            'Something went wrong, so don't count this spot
                                                                            TotTRP -= Parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, TmpR)
                                                                        End Try
                                                                        'End If
                                                                    Next
                                                                    'Increase the number of picked TRP and reset FailedAttempts counter
                                                                    PickedTRP += TotTRP
                                                                    FailedAttempts = 0

                                                                Else
                                                                    'This break is used before or there where no spots to use
                                                                    FailedAttempts += 1
                                                                End If
                                                            Else
                                                                FailedAttempts += 1
                                                            End If
                                                        Else
                                                            'If there are no Sponsorship spots we need to emulate sponsorships through commercial spots
                                                            Dim TmpList As List(Of Long) = GetSponsorshipSpots(r, Parent.Karma.Channels(i).Weeks(w).Dayparts(DP + 1), TmpBT.ParentChannel.UseBillboards, TmpBT.ParentChannel.UseBreakBumpers)
                                                            If TmpList.Count > 0 AndAlso Not PickedSpots(c).ContainsKey("S" & TmpList(0)) Then ' AndAlso Parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, TmpList(0), , , 1) < 3 Then
                                                                Dim TotTRP As Single = 0 'What is the total rating of all spons spots in the program
                                                                For Each TmpR As Long In TmpList
                                                                    TotTRP += Parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, TmpR)
                                                                Next
                                                                'Check that the PickedTRP doesn't end over 10% over PieceTRP
                                                                If (Int(PickedTRP + TotTRP) <= Int(PieceTRP * 1.1)) OrElse FailedAttempts > 19 Then
                                                                    For Each TmpR As Long In TmpList
                                                                        If Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandSpotCount, TmpR) > 1 AndAlso PickedSpots(c).ContainsKey("S" & TmpR) Then
                                                                            PickedSpots(c).Add("SS" & TmpR, TmpR)
                                                                        ElseIf Not PickedSpots(c).ContainsKey("S" & TmpR) Then
                                                                            PickedSpots(c).Add("S" & TmpR, TmpR)
                                                                        Else
                                                                            TotTRP -= Parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, TmpR)
                                                                        End If
                                                                    Next
                                                                    PickedTRP += TotTRP
                                                                    FailedAttempts = 0
                                                                    'The next line used to be:
                                                                End If
                                                                'So remove
                                                                'Else
                                                                'FailedAttempts +=1
                                                                'To make it like it was before
                                                                '/Hannes
                                                                'Else
                                                                '   FailedAttempts += 1
                                                                ' End If - uncomment this line
                                                            Else
                                                                FailedAttempts += 1
                                                            End If
                                                        End If
                                                    End If
                                                Else
                                                    'No there are no spots to pick from so make sure we get out of While loop
                                                    PickedTRP = PieceTRP
                                                    If c = 1 Then CampTRP -= PieceTRP
                                                End If
                                            End While
                                            'If we have picked all the spots or failed more than 100 times, consider the piece Maxed out
                                            If PickedInDaypart(i, w, DP + 1) >= Parent.Karma.Channels(i).Weeks(w).Dayparts(DP + 1).Spots.Count - 1 OrElse FailedAttempts > 100 Then
                                                Dim TmpMO As New MaxedOutStruct
                                                MaxedOut = True
                                                TmpMO.Channel = Parent.Karma.Channels(i).Name
                                                TmpMO.Daypart = Campaign.Dayparts(DP).Name
                                                TmpMO.PickedTRP = PickedTRP
                                                TmpMO.PieceTRP = PieceTRP
                                                If Not MaxedOutList.ContainsKey(TmpMO.Channel & " " & TmpMO.Daypart) Then
                                                    MaxedOutList.Add(TmpMO.Channel & " " & TmpMO.Daypart, TmpMO)
                                                End If
                                            End If
                                            'What is the difference between the needed TRP and how many we picked?
                                            Diff = Diff + PickedTRP - PieceTRP
                                        Else
                                            'Specific sponsorship
                                            If Parent.Karma.Channels(i).Weeks(w).Dayparts(DP + 1).Sponsorships.Count > 0 Then
                                                For s As Integer = 0 To Parent.Karma.Channels(i).Weeks(w).Dayparts(DP + 1).Sponsorships.Count - 1
                                                    r = Parent.Karma.Channels(i).Weeks(w).Dayparts(DP + 1).Sponsorships(s)
                                                    If TmpBT.SpecificSponsringPrograms.Contains(Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, r)) AndAlso Not PickedSpots(c).ContainsKey("S" & r) Then
                                                        'Debug.Print(Date.FromOADate(Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aDate, r)) & vbTab & _
                                                        '            Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aChannel, r) & vbTab & _
                                                        '            Parent.Karma.KarmaAdedge.convertTime(Connect.eTimeModes.tmSecNice, Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aFromTime, r)) & vbTab & _
                                                        '            Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, r))
                                                        Dim TmpList As List(Of Long) = GetSponsorship(s, Parent.Karma.Channels(i).Weeks(w).Dayparts(DP + 1), TmpBT.ParentChannel.UseBillboards, TmpBT.ParentChannel.UseBreakBumpers)
                                                        If TmpList.Count > 0 AndAlso TmpBT.SpecificSponsringPrograms.Contains(Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, TmpList(0))) Then
                                                            For Each TmpR As Long In TmpList
                                                                'If a spot has already been picked, decrease the picked TRP, otherwise add it to the list of picked spots
                                                                If Not PickedSpots(c).ContainsKey("S" & TmpR) Then
                                                                    PickedSpots(c).Add("S" & TmpR, TmpR)
                                                                    PickedSpecificsSponsorTRPs += Parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, r)
                                                                End If
                                                            Next
                                                        End If
                                                    End If
                                                Next
                                            Else
                                                For s As Integer = 1 To Parent.Karma.Channels(i).Weeks(w).Dayparts(DP).Spots.Count
                                                    r = Parent.Karma.Channels(i).Weeks(w).Dayparts(DP + 1).Spots(s)
                                                    If TmpBT.SpecificSponsringPrograms.Contains(Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, r)) Then
                                                        Dim TmpList As List(Of Long) = GetSponsorshipSpots(r, Parent.Karma.Channels(i).Weeks(w).Dayparts(DP + 1), TmpBT.ParentChannel.UseBillboards, TmpBT.ParentChannel.UseBreakBumpers)
                                                        For Each TmpR As Long In TmpList
                                                            'If a spot has already been picked, decrease the picked TRP, otherwise add it to the list of picked spots
                                                            If Not PickedSpots(c).ContainsKey("S" & TmpR) Then
                                                                PickedSpots(c).Add("S" & TmpR, TmpR)
                                                                PickedSpecificsSponsorTRPs += Parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, r)
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            End If
                                        End If
                                    End If ' Closing if, checked if there were TRPs that week
                                Next
                            Next
                        End If
                    Next
                Next
                'Debug.Print(c & vbTab & Diff)
                Parent.Karma.KarmaAdedge.clearGroup()
                For Each kv In PickedSpots(c)
                    Parent.Karma.KarmaAdedge.addToGroup(kv.Value)
                Next
                Parent.Karma.KarmaAdedge.recalcRF(Connect.eSumModes.smGroup)
                PieceTRP = 0
                'Add the TRPs from Specificssponsorships to the total TRPs
                CampTRP += PickedSpecificsSponsorTRPs
                'Since all campaigns will be bigger than desired, find what the reach would have been if the campaign was as small as intended
                For r = 0 To Parent.Karma.KarmaAdedge.getGroupCount - 1
                    If Campaign.MainTarget.Universe = "" Then
                        PieceTRP = PieceTRP + Parent.Karma.KarmaAdedge.getUnitGroup(Connect.eUnits.uTRP, r, 0, 0, 0)
                    Else
                        PieceTRP = PieceTRP + Parent.Karma.KarmaAdedge.getUnitGroup(Connect.eUnits.uTRP, r, 0, 1, 0)
                    End If
                    'When we exceed CampTRP we have found the threshold where this campaign is bigger
                    If PieceTRP > CampTRP Then
                        Exit For
                    End If
                Next
                If Parent.Karma.KarmaAdedge.getGroupCount = r Then
                    MaxedOut = True
                End If
                mvarTRP(c) = PieceTRP
                If MaxedOut Then r = Parent.Karma.KarmaAdedge.getGroupCount - 1
                If r < 0 Then r = 0
                For i = 1 To 10
                    'Save reached for the picked spotlist and save it in mvarReach(Iteration, Frequency, Target) (0=Main target, 1=Secondary Target)
                    If Campaign.MainTarget.Universe = "" Then
                        mvarReach(c, i, 0) += Parent.Karma.KarmaAdedge.getRF(r, , , , i)
                        If Parent.Karma.KarmaAdedge.getRF(r, , , , i) > HighestReach(i) Then
                            HighestReach(i) = Parent.Karma.KarmaAdedge.getRF(r, , , , i)
                        End If
                        If Parent.Karma.KarmaAdedge.getRF(r, , , , i) < LowestReach(i) Then
                            LowestReach(i) = Parent.Karma.KarmaAdedge.getRF(r, , , , i)
                        End If
                    Else
                        mvarReach(c, i, 0) += Parent.Karma.KarmaAdedge.getRF(r, , Trinity.Helper.UniverseIndex(Parent.Karma.KarmaAdedge, Campaign.MainTarget.Universe), , i)
                    End If
                    If Campaign.SecondaryTarget.Universe = "" Then
                        mvarReach(c, i, 1) += Parent.Karma.KarmaAdedge.getRF(r, , , 1, i)
                    Else
                        mvarReach(c, i, 1) += Parent.Karma.KarmaAdedge.getRF(r, , Trinity.Helper.UniverseIndex(Parent.Karma.KarmaAdedge, Campaign.SecondaryTarget.Universe), 1, i)
                    End If
                    'reset B1 and B2 used to calculate the reach curve
                    mvarB1(i) = 0
                    mvarB2(i) = 0
                Next
            Next
            If MaxedOutList.Count > 0 Then
                Dim TmpStr As String
                TmpStr = "These channels and dayparts can not handle" & vbCrLf & "the amount of TRPs allocated:" & vbCrLf
                For Each TmpMO As MaxedOutStruct In MaxedOutList.Values
                    TmpStr &= TmpMO.Channel & " " & TmpMO.Daypart & ": Needed " & TmpMO.PieceTRP & " TRPs, found " & TmpMO.PickedTRP & " TRPs" & vbCrLf
                Next
                TmpStr &= vbCrLf & "Reach result might not be accurate."
                Windows.Forms.MessageBox.Show(TmpStr, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            End If
        End Sub

        Private Function GetSponsorshipSpots(ByVal SpotNr As Long, ByVal Daypart As cKarmaDaypart, ByVal UseBillboards As Boolean, ByVal UseBreakbumpers As Boolean) As List(Of Long)
            Dim TmpList As New List(Of Long)
            Dim TmpR As Integer = Daypart.Spots(SpotNr)
            Dim ThisProg As String = Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, TmpR)
            TmpR -= Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandSpotInBreak, TmpR) - 1
            While TmpR > 0 AndAlso Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, TmpR) = ThisProg
                TmpR -= 1
                TmpR -= Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandSpotInBreak, TmpR) - 1
            End While
            'Add the last spot in the break before
            TmpR += Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandSpotCount, TmpR) - 1
            If Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, TmpR) <> Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, TmpR) Then
                If UseBillboards Then TmpList.Add(TmpR)
            End If
            TmpR += 1
            While TmpR < Daypart.Spots(Daypart.Spots.Count - 1) AndAlso ThisProg = Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, TmpR) AndAlso ThisProg = Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, TmpR)
                If UseBreakbumpers Then TmpList.Add(TmpR)
                TmpR += Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandSpotCount, TmpR)
                If UseBreakbumpers Then TmpList.Add(TmpR - 1)
            End While
            If TmpR < Daypart.Spots(Daypart.Spots.Count - 1) AndAlso Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, TmpR) <> Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, TmpR) Then
                If UseBillboards Then TmpList.Add(TmpR)
            End If
            Return TmpList
        End Function

        Private Function GetSponsorship(ByVal SpotNr As Long, ByVal Daypart As cKarmaDaypart, ByVal UseBillboards As Boolean, ByVal UseBreakbumpers As Boolean) As List(Of Long)
            Dim TmpList As New List(Of Long)
            Dim ThisProgBefore As String = Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, Daypart.Sponsorships(SpotNr))
            Dim ThisProgAfter As String = Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, Daypart.Sponsorships(SpotNr))
            Dim ThisProd As String = Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProduct, Daypart.Sponsorships(SpotNr))
            Dim ThisProg As String
            Dim OrigSpotNr As Long = SpotNr
            If SpotNr = Daypart.Sponsorships.Count - 1 OrElse ThisProd <> Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProduct, Daypart.Sponsorships(SpotNr + 1)) Then
                'Not end break
                ThisProg = ThisProgBefore
            Else
                ThisProg = ThisProgAfter
            End If
            While SpotNr > 0 AndAlso (Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProduct, Daypart.Sponsorships(SpotNr)) = ThisProd AndAlso (Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, Daypart.Sponsorships(SpotNr)) = ThisProg OrElse Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, Daypart.Sponsorships(SpotNr)) = ThisProg))
                SpotNr -= 1
            End While
            SpotNr += 1
            If UseBillboards AndAlso SpotNr < Daypart.Sponsorships.Count Then TmpList.Add(Daypart.Sponsorships(SpotNr))
            SpotNr += 1
            While SpotNr < Daypart.Sponsorships.Count AndAlso (Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProduct, Daypart.Sponsorships(SpotNr)) = ThisProd AndAlso (Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, Daypart.Sponsorships(SpotNr)) = ThisProg OrElse Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, Daypart.Sponsorships(SpotNr)) = ThisProg))
                If UseBreakbumpers AndAlso (Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, Daypart.Sponsorships(SpotNr)) = ThisProg AndAlso Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, Daypart.Sponsorships(SpotNr)) = ThisProg) Then TmpList.Add(Daypart.Sponsorships(SpotNr))
                If UseBillboards AndAlso (Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, Daypart.Sponsorships(SpotNr)) = ThisProg AndAlso Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, Daypart.Sponsorships(SpotNr)) <> ThisProg) Then TmpList.Add(Daypart.Sponsorships(SpotNr))
                SpotNr += 1
            End While
            SpotNr -= 1
            'For Each TmpR As Long In TmpList
            '    Debug.Print(TmpR & vbTab & Format(Date.FromOADate(Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aDate, TmpR)), "Short date") & vbTab & Parent.Karma.KarmaAdedge.convertTime(Connect.eTimeModes.tmSecNice, Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aFromTime, TmpR)) & vbTab & Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aChannel, TmpR) & vbTab & ThisProgAfter)
            'Next
            If TmpList.Count = 1 Then
                Return New List(Of Long)
            Else
                Return TmpList
            End If
        End Function

        'Private Function GetSponsorship(ByVal SpotNr As Long, ByVal daypart As cKarmaDaypart, ByVal UseBillboards As Boolean, ByVal UseBreakbumpers As Boolean) As List(Of Long)
        '    Dim TmpList As New List(Of Long)
        '    Dim TmpR As Integer = daypart.Sponsorships(SpotNr)
        '    Dim ThisProg As String = Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, TmpR)
        '    'If this is a start break, find the last spot in the break
        '    TmpR -= Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandSpotInBreak, TmpR) - 1
        '    'If Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, TmpR) <> ThisProg AndAlso Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, TmpR) = ThisProg Then
        '    '    While Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, TmpR) <> ThisProg AndAlso Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, TmpR) = ThisProg
        '    '        TmpR += 1
        '    '    End While
        '    '    TmpR -= 1
        '    'End If
        '    'If this is not a start break, find the last spot in the start break
        '    While TmpR > 0 AndAlso Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, TmpR) = ThisProg
        '        TmpR -= 1
        '        TmpR -= Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandSpotInBreak, TmpR) - 1
        '    End While
        '    'Add the last spot in the break before
        '    TmpR += Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandSpotCount, TmpR) - 1
        '    If Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, TmpR) <> Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, TmpR) Then
        '        If UseBillboards Then TmpList.Add(TmpR)
        '    End If
        '    TmpR += 1
        '    While TmpR < daypart.Spots(daypart.Spots.Count - 1) AndAlso ThisProg = Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, TmpR) AndAlso ThisProg = Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, TmpR)
        '        If UseBreakbumpers Then TmpList.Add(TmpR)
        '        TmpR += Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandSpotCount, TmpR)
        '        If UseBreakbumpers Then TmpList.Add(TmpR - 1)
        '    End While
        '    If TmpR < daypart.Spots(daypart.Spots.Count - 1) AndAlso Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, TmpR) <> Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, TmpR) Then
        '        While Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, TmpR) <> ThisProg AndAlso Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, TmpR) = ThisProg
        '            TmpR -= 1
        '        End While
        '        If UseBillboards Then TmpList.Add(TmpR)
        '    End If
        '    Return TmpList
        'End Function

        'Public Sub Run(Optional ByVal WeekNumber As Integer = 0)
        '    Dim Diff As Single
        '    Dim PickedTRP As Single
        '    Dim PieceTRP As Single
        '    Dim c As Integer
        '    Dim i As Integer
        '    Dim TmpBT As cBookingType
        '    Dim CampTRP As Single
        '    Dim DP As Integer
        '    Dim r As Long
        '    Dim UseCampaign As cKampanj
        '    Dim kv As KeyValuePair(Of String, Long)
        '    Dim PickedInDaypart(Parent.Karma.Channels.Count, Campaign.DaypartCount) As Integer
        '    Dim MaxedOut As Boolean = False
        '    Dim MaxedOutList As New Dictionary(Of String, MaxedOutStruct)

        '    Randomize(Timer)

        '    ReDim mvarReach(0 To Iterations, 0 To 10, 0 To 1)
        '    ReDim mvarTRP(0 To Iterations)

        '    UseCampaign = TrinityCampaign

        '    If WeekNumber = 0 Then
        '        CampTRP = UseCampaign.TotalTRP
        '    Else
        '        CampTRP = 0
        '        For Each TmpChan As Trinity.cChannel In UseCampaign.Channels
        '            For Each TmpBT In TmpChan.BookingTypes
        '                If TmpBT.BookIt Then
        '                    CampTRP += TmpBT.Weeks(WeekNumber).TRP
        '                End If
        '            Next
        '        Next
        '    End If
        '    For c = 1 To Iterations
        '        RaiseEvent Progress((c / Iterations) * 100)
        '        Diff = 0
        '        PickedSpots(c) = New SortedDictionary(Of String, Long)
        '        For i = 1 To Parent.Karma.Channels.Count
        '            For DP = 0 To UseCampaign.DaypartCount - 1
        '                PieceTRP = 0
        '                For Each TmpBT In UseCampaign.Channels(Parent.Karma.Channels(i).Name).BookingTypes
        '                    If TmpBT.BookIt Then
        '                        If WeekNumber > 0 Then
        '                            PieceTRP += TmpBT.Weeks(WeekNumber).TRP * (TmpBT.DaypartSplit(DP) / 100)
        '                        Else
        '                            PieceTRP += TmpBT.TotalTRP * (TmpBT.DaypartSplit(DP) / 100)
        '                        End If
        '                    End If
        '                Next
        '                PickedTRP = 0
        '                PickedInDaypart(i, DP + 1) = 0
        '                While PickedTRP < PieceTRP And PickedInDaypart(i, DP + 1) < Parent.Karma.Channels(i).Dayparts(DP + 1).Spots.Count - 1
        '                    If Parent.Karma.Channels(i).Dayparts(DP + 1).Spots.Count > 0 Then
        '                        r = Int(Rnd() * Parent.Karma.Channels(i).Dayparts(DP + 1).Spots.Count)
        '                        r = Parent.Karma.Channels(i).Dayparts(DP + 1).Spots(r)
        '                        If Not PickedSpots(c).ContainsKey("S" & r) Then
        '                            If Campaign.MainTarget.Universe = "" Then
        '                                PickedTRP = PickedTRP + Parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, r)
        '                            Else
        '                                PickedTRP = PickedTRP + Parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, r, , 1)
        '                            End If
        '                            PickedSpots(c).Add("S" & r, r)
        '                            PickedInDaypart(i, DP + 1) += 1
        '                        End If
        '                    Else
        '                    PickedTRP = PieceTRP
        '                    If c = 1 Then CampTRP -= PieceTRP
        '                    End If
        '                End While
        '                If PickedInDaypart(i, DP + 1) >= Parent.Karma.Channels(i).Dayparts(DP + 1).Spots.Count - 1 Then
        '                    Dim TmpMO As New MaxedOutStruct
        '                    MaxedOut = True
        '                    TmpMO.Channel = Parent.Karma.Channels(i).Name
        '                    TmpMO.Daypart = Campaign.DaypartName(DP)
        '                    TmpMO.PickedTRP = PickedTRP
        '                    TmpMO.PieceTRP = PieceTRP
        '                    If Not MaxedOutList.ContainsKey(TmpMO.Channel & " " & TmpMO.Daypart) Then
        '                        MaxedOutList.Add(TmpMO.Channel & " " & TmpMO.Daypart, TmpMO)
        '                    End If
        '                End If
        '                Diff = Diff + PickedTRP - PieceTRP
        '            Next
        '        Next
        '        Parent.Karma.KarmaAdedge.clearGroup()
        '        For Each kv In PickedSpots(c)
        '            Parent.Karma.KarmaAdedge.addToGroup(kv.Value)
        '        Next
        '        Parent.Karma.KarmaAdedge.recalcRF(Connect.eSumModes.smGroup)
        '        PieceTRP = 0
        '        For r = 0 To Parent.Karma.KarmaAdedge.getGroupCount - 1
        '            If Campaign.MainTarget.Universe = "" Then
        '                PieceTRP = PieceTRP + Parent.Karma.KarmaAdedge.getUnitGroup(Connect.eUnits.uTRP, r, 0, 0, 0)
        '            Else
        '                PieceTRP = PieceTRP + Parent.Karma.KarmaAdedge.getUnitGroup(Connect.eUnits.uTRP, r, 0, 1, 0)
        '            End If
        '            If PieceTRP > CampTRP Then
        '                Exit For
        '            End If
        '        Next
        '        If Parent.Karma.KarmaAdedge.getGroupCount = r Then
        '            MaxedOut = True
        '        End If
        '        mvarTRP(c) = PieceTRP
        '        If MaxedOut Then r = Parent.Karma.KarmaAdedge.getGroupCount - 1
        '        If r < 0 Then r = 0
        '        For i = 1 To 10
        '            If Campaign.MainTarget.Universe = "" Then
        '                mvarReach(c, i, 0) += Parent.Karma.KarmaAdedge.getRF(r, , , , i)
        '            Else
        '                mvarReach(c, i, 0) += Parent.Karma.KarmaAdedge.getRF(r, , Trinity.Helper.UniverseIndex(Parent.Karma.KarmaAdedge, Campaign.MainTarget.Universe), , i)
        '            End If
        '            If Campaign.SecondaryTarget.Universe = "" Then
        '                mvarReach(c, i, 1) += Parent.Karma.KarmaAdedge.getRF(r, , , 1, i)
        '            Else
        '                mvarReach(c, i, 1) += Parent.Karma.KarmaAdedge.getRF(r, , Trinity.Helper.UniverseIndex(Parent.Karma.KarmaAdedge, Campaign.SecondaryTarget.Universe), 1, i)
        '            End If
        '            mvarB1(i) = 0
        '            mvarB2(i) = 0
        '        Next
        '        'Quicksort(PickedSpots(c), 1, PickedSpots(c).Count)
        '    Next
        '    If MaxedOutList.Count > 0 Then
        '        Dim TmpStr As String
        '        TmpStr = "These channels and dayparts can not handle" & vbCrLf & "the amount of TRPs allocated:" & vbCrLf
        '        For Each TmpMO As MaxedOutStruct In MaxedOutList.Values
        '            TmpStr &= TmpMO.Channel & " " & TmpMO.Daypart & ": Needed " & TmpMO.PieceTRP & " TRPs, found " & TmpMO.PickedTRP & " TRPs" & vbCrLf
        '        Next
        '        TmpStr &= vbCrLf & "Reach result might not be accurate."
        '        Windows.Forms.MessageBox.Show(TmpStr, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        '    End If
        'End Sub

        Public Sub CalculateMichaelisMenten(Optional ByVal Freq = 1)
            Dim c As Integer
            Dim r As Long
            Dim TRP As Single
            Dim b As Single
            Dim SumB As Single
            Dim x1, x2, y1, y2, SumXY, SumX2
            Dim kv As KeyValuePair(Of String, Long)

            SumB = 0
            x1 = 0
            x2 = 0
            y1 = 0
            y2 = 0
            SumXY = 0
            SumX2 = 0
            Dim TotalTRP As Single = 0
            For c = 1 To Iterations
                TotalTRP += mvarTRP(c)
                TRP = 0
                Parent.Karma.KarmaAdedge.clearGroup()
                For Each kv In PickedSpots(c)
                    Parent.Karma.KarmaAdedge.addToGroup(kv.Value)
                Next
                Parent.Karma.KarmaAdedge.recalcRF(Connect.eSumModes.smGroup)
                SumXY = 0
                SumX2 = 0
                For r = 1 To Parent.Karma.KarmaAdedge.getGroupCount - 1
                    TRP = TRP + Parent.Karma.KarmaAdedge.getUnitGroup(Connect.eUnits.uTRP, r, 0, 0, 0)
                    x1 = TRP - Parent.Karma.KarmaAdedge.getUnitGroup(Connect.eUnits.uTRP, r, 0, 0, 0)
                    x2 = TRP
                    y1 = Parent.Karma.KarmaAdedge.getRF(r - 1, , , , Freq)
                    y2 = Parent.Karma.KarmaAdedge.getRF(r, , , , Freq)
                    SumXY = SumXY + x2 * y1 * x1 - x1 * y2 * x2
                    SumX2 = SumX2 + y2 * x1 - y1 * x2
                Next
                If SumX2 <> 0 Then
                    b = SumXY / SumX2
                Else
                    b = 1
                End If
                SumB = SumB + b
            Next
            TotalTRP = TotalTRP / Iterations

            mvarB2(Freq) = SumB / Iterations
            mvarB1(Freq) = Reach(0, Freq) * (TotalTRP + (SumB / Iterations)) / TotalTRP
        End Sub

        Private Sub Quicksort(ByVal List As Object, ByVal min As Integer, ByVal max As Integer)
            Dim med_value As Long
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
                Do While List(hi) >= med_value
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
                Do While List(lo) < med_value
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

        Public ReadOnly Property TRP(ByVal Iteration) As Single
            Get
                Dim i As Integer
                Dim _trp As Single = 0
                If Iteration = 0 Then
                    For i = 1 To Iterations
                        _trp += mvarTRP(i)
                    Next
                    _trp /= Iterations
                Else
                    _trp = mvarTRP(Iteration)
                End If
                Return _trp
            End Get
        End Property

        Public ReadOnly Property Reach(ByVal Iteration As Integer, ByVal Freq As Byte, Optional ByVal Target As Trinity.cKampanj.ReachTargetEnum = cKampanj.ReachTargetEnum.rteMainTarget) As Single
            Get
                Dim i As Integer

                On Error GoTo ErrHandle
                Dim _reach As Single = 0
                If Iteration = 0 Then
                    For i = 1 To Iterations
                        _reach += mvarReach(i, Freq, Target)
                    Next
                    _reach /= Iterations
                Else
                    _reach = mvarReach(Iteration, Freq, Target)
                End If
                Return _reach
                Exit Property

ErrHandle:
                Return 0
            End Get
        End Property

        Public Function B1(Optional ByVal Freq = 1) As Single
            If mvarB1(Freq) = 0 Then
                CalculateMichaelisMenten(Freq)
            End If
            B1 = mvarB1(Freq)
        End Function

        Public Function B2(Optional ByVal Freq = 1) As Single
            If mvarB2(Freq) = 0 Then
                CalculateMichaelisMenten(Freq)
            End If
            B2 = mvarB2(Freq)
        End Function

        Function TRPProfile(ByVal Profile As Integer) As Single
            TRPProfile = mvarProfileTRP(Profile)
        End Function

        Public Sub CalculateProfile()
            Dim SpotCount As Long
            Dim s As Long
            Dim c As Integer
            Dim TmpChan As cKarmaChannel
            Dim ChanStr As String
            Dim kv As KeyValuePair(Of String, Long)

            ChanStr = ""
            For Each TmpChan In Parent.Karma.Channels
                ChanStr = ChanStr & Campaign.Channels(TmpChan.Name).AdEdgeNames & ","
            Next

            Adedge.setArea(Campaign.Area)
            Trinity.Helper.AddTargetsToAdedge(Adedge, False, True)
            'Adedge.setTargetMnemonic(Helper.CreateTargetString(False, True)) ' CreateTargetString(False, True)
            Adedge.setBrandType("COMMERCIAL")
            Adedge.setChannelsArea(ChanStr, Campaign.Area)
            Adedge.setPeriod(Parent.Karma.ReferencePeriod)
            Adedge.setUniverseUserDefined(Campaign.MainTarget.SecondUniverse)

            SpotCount = Adedge.Run(False, False, 0)

            For s = 1 To 14
                mvarProfileTRP(s) = 0
            Next
            For c = 1 To Iterations
                Adedge.clearGroup()
                For Each kv In PickedSpots(c)
                    Adedge.addToGroup(kv.Value)
                Next
                For s = 1 To 14
                    mvarProfileTRP(s) = mvarProfileTRP(s) + Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , , Helper.TargetIndex(Adedge, Campaign.ThirdTarget) + s)
                Next
            Next
            For s = 1 To 14
                mvarProfileTRP(s) = mvarProfileTRP(s) / Iterations
            Next

        End Sub

        Function ProfileTarget(ByVal s As Integer) As String
            ProfileTarget = Adedge.getTargetTitle(s + 3)
        End Function

        Public Sub Reset()
            ReDim mvarReach(0, 0, 0)
        End Sub

    End Class

End Namespace
