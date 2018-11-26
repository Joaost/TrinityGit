Imports System.Linq
Namespace Trinity
    Public Class cKarmaCampaign
        Const Iterations = 100

        Private Structure MaxedOutStruct
            Public PickedTRP As Single
            Public PieceTRP As Single
            Public Channel As String
            Public Daypart As String
        End Structure

        Private mvarReach(,,) As Single
        Private mvarTRP() As Single
        Private mvarB1(0 To 10) As Single
        Private mvarB2(0 To 10) As Single
        Private mvarFC() As Single
        Private mvarN As Integer

        Private _name As String
        Private _trinityCampaign As Trinity.cKampanj

        Private mvarProfileTRP(0 To 14) As Single
        Private Adedge As New ConnectWrapper.Brands

        Private _parent As cKarmaCampaigns

        Public Event Progress(ByVal p As Single)

        Private _runCancelled As Boolean = False
        Public Sub CancelRun()
            _runCancelled = True
        End Sub

        Public Sub Run(Optional ByVal WeekNumber As Integer = 0)

            Dim _diff As Single
            Dim _maxedOut As Boolean = False
            Dim _maxedOutList As New Dictionary(Of String, MaxedOutStruct)
            Dim _pickedSpecificsSponsorTRPs As Single = 0

            Dim _fromWeek As Integer = 0
            Dim _toWeek As Integer = 0

            If WeekNumber > 0 Then
                _fromWeek = 1
                _toWeek = 1
            Else
                _fromWeek = 1
                _toWeek = _parent.Karma.Weeks
            End If

            _runCancelled = False

            'Set random seed
            'r = Rnd(-1)
            Randomize(1000)

            'Setup variables to hold results for each iteration
            ReDim mvarReach(0 To Iterations, 0 To 10, 0 To 1)
            ReDim mvarTRP(0 To Iterations)

            'Setup variables to hold highest possible and lowest possible reach
            Dim _highestReach(0 To 10) As Single
            Dim _lowestReach(0 To 10) As Single
            For i As Integer = 0 To 10
                _lowestReach(i) = 100
            Next

            'Get the total amount of TRPs to pick
            Dim _campTRP As Single
            If WeekNumber = 0 Then
                'If no week is set the calculation is for the entire campaign
                _campTRP = TrinityCampaign.TotalTRP
            Else
                'If a week is set, get the TRPs for that week
                _campTRP = 0
                For Each _chan As Trinity.cChannel In TrinityCampaign.Channels
                    For Each _bt As Trinity.cBookingType In _chan.BookingTypes
                        If _bt.BookIt AndAlso Not (_bt.IsSponsorship AndAlso _bt.IsSpecific) Then
                            'Only use chosen bookingtypes that is not specicic sponsorships (TRPs for them will instead be fetched)
                            _campTRP += _bt.Weeks(WeekNumber).TRP
                            For Each _comp As Trinity.cCompensation In _bt.Compensations
                                'Include compensations
                                Dim _length As Integer = DateDiff(DateInterval.Day, _comp.FromDate, _comp.ToDate)
                                Dim _trpPerDay As Single = (_comp.TRPs / _length) * (_bt.IndexMainTarget / 100)
                                For _d As Long = _comp.FromDate.ToOADate To _comp.ToDate.ToOADate
                                    _campTRP += _trpPerDay
                                Next
                            Next
                        End If
                    Next
                Next
            End If

            Dim _grossSpotlist As New Dictionary(Of String, Dictionary(Of Trinity.cBookingType, Dictionary(Of Trinity.cDaypart, Dictionary(Of Integer, List(Of Trinity.cKarmaSpot)))))
            For Each _chan As String In _parent.Karma.Channels
                If Not _grossSpotlist.ContainsKey(_chan) Then
                    _grossSpotlist.Add(_chan, New Dictionary(Of Trinity.cBookingType, Dictionary(Of Trinity.cDaypart, Dictionary(Of Integer, List(Of Trinity.cKarmaSpot)))))
                End If
                For Each _bt As Trinity.cBookingType In TrinityCampaign.Channels(_chan).BookingTypes
                    If Not _grossSpotlist(_chan).ContainsKey(_bt) Then
                        _grossSpotlist(_chan).Add(_bt, New Dictionary(Of Trinity.cDaypart, Dictionary(Of Integer, List(Of Trinity.cKarmaSpot))))
                    End If
                    If _bt.BookIt Then
                        For Each _dp As Trinity.cDaypart In _bt.Dayparts
                            If Not _grossSpotlist(_chan)(_bt).ContainsKey(_dp) Then
                                _grossSpotlist(_chan)(_bt).Add(_dp, New Dictionary(Of Integer, List(Of Trinity.cKarmaSpot)))
                            End If
                            For _w As Integer = _fromWeek To _toWeek
                                If Not _grossSpotlist(_chan)(_bt)(_dp).ContainsKey(_w) Then
                                    _grossSpotlist(_chan)(_bt)(_dp).Add(_w, New List(Of Trinity.cKarmaSpot))
                                End If
                                If _bt.Weeks(IIf(WeekNumber > 0, WeekNumber, _w)).TRP > 0 Then
                                    'Linq does not work well with iteration variables, so their values must be passed to a local variable
                                    Dim _linqDP As Trinity.cDaypart = _dp
                                    Dim _linqBT As Trinity.cBookingType = _bt
                                    Dim _linqW As Integer = _w
                                    _grossSpotlist(_chan)(_bt)(_dp)(_w) = (From _spot As cKarmaSpot In _parent.Karma.Spots Select _spot Where Not _spot.HasBeenPicked AndAlso _spot.Channel = _linqBT.ParentChannel.ChannelName AndAlso _spot.Week = _linqW AndAlso _linqBT.Dayparts.GetDaypartForMam(_spot.Mam) Is _linqDP).ToList
                                    If _bt.IsSponsorship Then
                                        If (From _spot As cKarmaSpot In _grossSpotlist(_chan)(_bt)(_dp)(_w) Select _spot Where _spot.Type = cKarmaSpot.KarmaSpotType.Sponsorship).Count > 0 Then
                                            _grossSpotlist(_chan)(_bt)(_dp)(_w) = (From _spot As cKarmaSpot In _grossSpotlist(_chan)(_bt)(_dp)(_w) Select _spot Where _spot.Type = cKarmaSpot.KarmaSpotType.Sponsorship).ToList
                                        Else
                                            _grossSpotlist(_chan)(_bt)(_dp)(_w) = (From _spot As cKarmaSpot In _grossSpotlist(_chan)(_bt)(_dp)(_w) Select _spot Where _spot.SpotInBreak = 1 OrElse _spot.SpotInBreak = _spot.SpotCount).ToList
                                        End If
                                    End If
                                Else
                                    Debug.Print("TRPs for " & _bt.ToString & " week " & _w & " was less than or equal to 0")
                                End If
                            Next
                        Next
                    End If
                Next
            Next

            'Start the calculation
            For _iteration As Integer = 1 To Iterations
                If _runCancelled Then
                    Exit Sub
                End If
                RaiseEvent Progress((_iteration / Iterations) * 100)

                'Set all spots as unpicked
                For Each _spot As cKarmaSpot In (From _kspot As cKarmaSpot In _parent.Karma.Spots Select _kspot)
                    _spot.HasBeenPicked = False
                    _spot.UseInCampaign = False
                Next
                _diff = 0
                For Each _chan As String In _parent.Karma.Channels
                    For Each _bt As Trinity.cBookingType In TrinityCampaign.Channels(_chan).BookingTypes

                        If _bt.BookIt Then
                            For Each _dp As Trinity.cDaypart In _bt.Dayparts
                                For _w As Integer = _fromWeek To _toWeek
                                    If _bt.Weeks(IIf(WeekNumber > 0, WeekNumber, _w)).TRP > 0 Then
                                        Dim _spotList As List(Of cKarmaSpot)
                                        _spotList = _grossSpotlist(_chan)(_bt)(_dp)(_w)
                                        If _spotList.Count = 0 Then
                                            'Stop
                                        End If
                                        If Not (_bt.IsSponsorship AndAlso _bt.IsSpecific) Then
                                            Dim _wn As Integer
                                            Dim _pieceTRP As Single = 0
                                            'Determine how many TRPs to pick for this Bookingtype, week and daypart
                                            If WeekNumber > 0 Then
                                                _pieceTRP = _bt.Weeks(WeekNumber).TRP * (_dp.Share / 100)
                                                _wn = WeekNumber
                                            Else
                                                _pieceTRP = _bt.Weeks(_w).TRP * (_dp.Share / 100)
                                                _wn = _w
                                            End If
                                            'Include compensation TRPs
                                            For Each _comp As Trinity.cCompensation In _bt.Compensations
                                                Dim _length As Integer = DateDiff(DateInterval.Day, _comp.FromDate, _comp.ToDate)
                                                Dim _trpPerDay As Single = (_comp.TRPs / _length) * (_bt.IndexMainTarget / 100) * (_dp.Share / 100)
                                                For _d As Long = _comp.FromDate.ToOADate To _comp.ToDate.ToOADate
                                                    _pieceTRP += _trpPerDay
                                                Next
                                            Next
                                            'Keep picking spots as long as we have less TRPs (PickedTRP) than our goal (PieceTRP) and as long as there are unpicked spots left in the list
                                            Dim _pickedTRP As Single = 0
                                            If Not _bt.IsSponsorship Then
                                                _spotList = (From _spot As cKarmaSpot In _spotList Select _spot Where _spot.Type = cKarmaSpot.KarmaSpotType.Spot).ToList
                                                If _spotList.Count = 0 Then
                                                    ' Stop
                                                End If
                                                While Int(_pickedTRP) < Int(_pieceTRP) And _spotList.Count > 0
                                                    Dim _spotIndex As Long = Int(Rnd() * _spotList.Count)

                                                    If TrinityCampaign.MainTarget.Universe = "" Then
                                                        Dim pick As Double = _parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, _spotList(_spotIndex).ListIndex)
                                                        _pickedTRP += pick
                                                    Else
                                                        Dim pick As Double = _parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, _spotList(_spotIndex).ListIndex, , 1)
                                                        _pickedTRP += pick
                                                    End If
                                                    _spotList(_spotIndex).HasBeenPicked = True
                                                    _spotList(_spotIndex).UseInCampaign = True
                                                    _spotList = (From _spot As cKarmaSpot In _spotList Select _spot Where _spot.HasBeenPicked = False).ToList
                                                End While


                                                ' Stop
                                            Else
                                                'RBS Sponsorship
                                                Dim _sponsorshipsInList As Boolean = True
                                                _spotList = _grossSpotlist(_chan)(_bt)(_dp)(_w)
                                                Dim _failedAttempts As Integer = 0
                                                Dim _unpicked As List(Of Trinity.cKarmaSpot) = (From _spot As Trinity.cKarmaSpot In _spotList Select _spot Where Not _spot.HasBeenPicked).ToList
                                                While Int(_pickedTRP) < Int(_pieceTRP * 1.1) And _unpicked.Count > 0
                                                    Dim _idx As Long = Int(Rnd() * _unpicked.Count)
                                                    Dim _tmpList As List(Of Long)
                                                    'If _sponsorshipsInList Then
                                                    _tmpList = GetSponsorship(_idx, _unpicked, _bt.ParentChannel.UseBillboards, _bt.ParentChannel.UseBreakBumpers)
                                                    'Else
                                                    '_tmpList = GetSponsorshipSpots(_idx, _unpicked, _bt.ParentChannel.UseBillboards, _bt.ParentChannel.UseBreakBumpers)
                                                    'End If
                                                    If _tmpList.Count = 0 Then
                                                        _unpicked(_idx).HasBeenPicked = True
                                                    End If
                                                    Dim _totTRP As Single = 0
                                                    For Each _spotIndex As Long In _tmpList
                                                        _totTRP += _parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, _unpicked(_spotIndex).ListIndex)
                                                        If _iteration = 1 Then
                                                            Debug.Print(Date.FromOADate(_parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aDate, _unpicked(_spotIndex).ListIndex)) & vbTab & _
                                                                _parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aChannel, _unpicked(_spotIndex).ListIndex) & vbTab & _
                                                                _parent.Karma.KarmaAdedge.convertTime(Connect.eTimeModes.tmSecNice, _parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aFromTime, _unpicked(_spotIndex).ListIndex)) & vbTab & _
                                                                _parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, _unpicked(_spotIndex).ListIndex) & vbTab & _
                                                                _parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, _unpicked(_spotIndex).ListIndex))
                                                        End If
                                                    Next
                                                    If (Int(_pickedTRP + _totTRP) <= Int(_pieceTRP * 1.1)) OrElse _failedAttempts > 19 Then
                                                        For Each _spotIndex As Long In _tmpList
                                                            Dim _spotIdx As Long = _spotIndex
                                                            _unpicked(_spotIndex).HasBeenPicked = True
                                                            _unpicked(_spotIndex).UseInCampaign = True
                                                        Next
                                                        If Format(_totTRP, "0.00") = 0 Then
                                                            _failedAttempts += 1
                                                            _spotList(_idx).HasBeenPicked = True
                                                        Else
                                                            _pickedTRP += _totTRP
                                                            _failedAttempts = 0
                                                        End If
                                                    Else
                                                        If (Int(_pickedTRP + _totTRP) > Int(_pieceTRP * 1.1)) Then
                                                            'Mark spots as not pickable
                                                            For Each _spotIndex As Long In _tmpList
                                                                _unpicked(_spotIndex).HasBeenPicked = True
                                                            Next
                                                        End If
                                                        _failedAttempts += 1
                                                    End If
                                                    _unpicked = (From _spot As Trinity.cKarmaSpot In _spotList Select _spot Where Not _spot.HasBeenPicked).ToList
                                                End While
                                            End If
                                            If Int(_pickedTRP) < Int(_pieceTRP) Then
                                                Dim _mo As New MaxedOutStruct
                                                _maxedOut = True
                                                _mo.Channel = _bt.ToString
                                                _mo.Daypart = _dp.Name
                                                _mo.PickedTRP = _pickedTRP
                                                _mo.PieceTRP = _pieceTRP
                                                If Not _maxedOutList.ContainsKey(_mo.Channel & " " & _mo.Daypart) Then
                                                    _maxedOutList.Add(_mo.Channel & " " & _mo.Daypart, _mo)
                                                End If
                                            End If
                                            _diff = _diff + _pickedTRP - _pieceTRP
                                        Else
                                            'Specific sponsorship
                                            _spotList = _grossSpotlist(_chan)(_bt)(_dp)(_w)
                                            Dim _failedAttempts As Integer = 0

                                            'Linq does not work well with iteration variables, so their values must be passed to a local variable
                                            Dim _linqBT As Trinity.cBookingType = _bt
                                            Dim _specSponsList As List(Of cKarmaSpot) = (From _spot As cKarmaSpot In _spotList Select _spot Where _linqBT.SpecificSponsringPrograms.Contains(_spot.ProgAfter) OrElse _linqBT.SpecificSponsringPrograms.Contains(_spot.ProgBefore)).ToList
                                            Dim _unpicked As List(Of cKarmaSpot) = (From _spot As cKarmaSpot In _specSponsList Select _spot Where Not _spot.HasBeenPicked).ToList
                                            While _unpicked.Count > 0
                                                Dim _idx As Integer = 0
                                                Dim _tmpList As List(Of Long)
                                                _tmpList = GetSponsorship(_idx, _unpicked, _bt.ParentChannel.UseBillboards, _bt.ParentChannel.UseBreakBumpers)
                                                If _tmpList.Count = 0 Then
                                                    'Make sure this spot is not picked again
                                                    _unpicked(_idx).HasBeenPicked = True
                                                Else
                                                    For Each _spotIndex As Long In _tmpList
                                                        _unpicked(_spotIndex).HasBeenPicked = True
                                                        _unpicked(_spotIndex).UseInCampaign = True
                                                        _pickedSpecificsSponsorTRPs += _parent.Karma.KarmaAdedge.getUnit(Connect.eUnits.uTRP, _spotIndex)
                                                    Next
                                                End If
                                                _unpicked = (From _spot As cKarmaSpot In _specSponsList Select _spot Where Not _spot.HasBeenPicked).ToList
                                            End While
                                        End If
                                    End If
                                Next
                            Next
                        End If
                    Next
                Next

                _parent.Karma.KarmaAdedge.clearGroup()

                Dim list As new List(Of cKarmaSpot)

                For Each _spot As cKarmaSpot In (From _kspot As cKarmaSpot In _parent.Karma.Spots Select _kspot Where _kspot.UseInCampaign)
                    _parent.Karma.KarmaAdedge.addToGroup(_spot.ListIndex)
                    list.Add(_spot)
                Next



                'Windows.Forms.MessageBox.Show(list.Count())
                '  If _parent.Karma.Spots.Count > 0 Then
                _parent.Karma.KarmaAdedge.recalcRF(Connect.eSumModes.smGroup)

                Dim rf = _parent.Karma.KarmaAdedge.getRF(list.Count() - 1, , , ,3)

                'Since all campaigns will be bigger than desired, find what the reach would have been if the campaign was as small as intended
                Dim _TRP As Single = 0
                _campTRP += _pickedSpecificsSponsorTRPs

                Dim _r As Integer
                For _r = 0 To _parent.Karma.KarmaAdedge.getGroupCount - 1
                    If TrinityCampaign.MainTarget.Universe = "" Then
                        _TRP += _parent.Karma.KarmaAdedge.getUnitGroup(Connect.eUnits.uTRP, _r, 0, 0, 0)
                    Else
                        _TRP += _parent.Karma.KarmaAdedge.getUnitGroup(Connect.eUnits.uTRP, _r, 0, 1, 0)
                    End If
                    'When we exceed CampTRP we have found the threshold where this campaign is bigger
                    If _TRP > _campTRP Then
                        Exit For
                    End If
                Next
                If _parent.Karma.KarmaAdedge.getGroupCount = _r Then
                    _maxedOut = True
                End If
                mvarTRP(_iteration) = _TRP
                If _maxedOut Then _r = _parent.Karma.KarmaAdedge.getGroupCount - 1
                If _r < 0 Then _r = 0
                mvarN = 11
                ReDim mvarFC(mvarN)
                Dim lastReach As Single = 1
                For _freq As Integer = 1 To 10
                    'Save reached for the picked spotlist and save it in mvarReach(Iteration, Frequency, Target) (0=Main target, 1=Secondary Target)
                    If TrinityCampaign.MainTarget.Universe = "" Then
                        mvarReach(_iteration, _freq, 0) += _parent.Karma.KarmaAdedge.getRF(_r, , , , _freq)
                        If _parent.Karma.KarmaAdedge.getRF(_r, , , , _freq) > _highestReach(_freq) Then
                            _highestReach(_freq) = _parent.Karma.KarmaAdedge.getRF(_r, , , , _freq)
                        End If
                        If _parent.Karma.KarmaAdedge.getRF(_r, , , , _freq) < _lowestReach(_freq) Then
                            _lowestReach(_freq) = _parent.Karma.KarmaAdedge.getRF(_r, , , , _freq)
                        End If
                    Else
                        mvarReach(_iteration, _freq, 0) += _parent.Karma.KarmaAdedge.getRF(_r, , TrinityCampaign.TimeShift, , _freq)
                    End If
                    If TrinityCampaign.SecondaryTarget.Universe = "" Then
                        mvarReach(_iteration, _freq, 1) += _parent.Karma.KarmaAdedge.getRF(_r, , , 1, _freq)
                    Else
                        mvarReach(_iteration, _freq, 1) += _parent.Karma.KarmaAdedge.getRF(_r, , TrinityCampaign.TimeShift, 1, _freq)
                    End If
                    'reset B1 and B2 used to calculate the reach curve
                    mvarB1(_freq) = 0
                    mvarB2(_freq) = 0
                    'Calculate fc and n for NBD
                    If _freq < 10 Then
                        mvarFC(_freq - 1) = lastReach - (_parent.Karma.KarmaAdedge.getRF(_r, , TrinityCampaign.TimeShift, 0, _freq) / 100)
                    End If
                    lastReach = (_parent.Karma.KarmaAdedge.getRF(_r, , TrinityCampaign.TimeShift, 0, _freq) / 100)
                Next
                mvarFC(10) = (_parent.Karma.KarmaAdedge.getRF(_r, , TrinityCampaign.TimeShift, 0, 10) / 100)
                ' Else

                '    End If
            Next
            If _maxedOutList.Count > 0 Then
                Dim TmpStr As String
                TmpStr = "These channels and dayparts can not handle" & vbCrLf & "the amount of TRPs allocated:" & vbCrLf
                For Each TmpMO As MaxedOutStruct In _maxedOutList.Values
                    TmpStr &= TmpMO.Channel & " " & TmpMO.Daypart & ": Needed " & TmpMO.PieceTRP & " TRPs, found " & TmpMO.PickedTRP & " TRPs" & vbCrLf
                Next
                TmpStr &= vbCrLf & "Reach result might not be accurate."
                Windows.Forms.MessageBox.Show(TmpStr, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            End If
         
            
        End Sub

        Private Function GetSponsorship(ByVal SpotNr As Long, ByVal SpotList As List(Of cKarmaSpot), ByVal UseBillboards As Boolean, ByVal UseBreakbumpers As Boolean) As List(Of Long)
            Dim TmpList As New List(Of Long)
            Dim ThisProgBefore As String = SpotList(SpotNr).ProgBefore
            Dim ThisProgAfter As String = SpotList(SpotNr).ProgAfter
            Dim ThisProd As String = SpotList(SpotNr).Product
            Dim ThisProg As String
            Dim OrigSpotNr As Long = SpotNr
            If SpotNr = SpotList.Count - 1 OrElse ThisProd <> SpotList(SpotNr + 1).Product Then
                'Not end break
                ThisProg = ThisProgBefore
            Else
                ThisProg = ThisProgAfter
            End If
            While SpotNr > 0 AndAlso SpotList(SpotNr).Product = ThisProd AndAlso (SpotList(SpotNr).ProgAfter = ThisProg OrElse SpotList(SpotNr).ProgBefore = ThisProg)
                SpotNr -= 1
            End While
            SpotNr += 1
            If UseBillboards AndAlso SpotNr < SpotList.Count Then TmpList.Add(SpotNr)
            SpotNr += 1
            While SpotNr < SpotList.Count AndAlso (SpotList(SpotNr).Product = ThisProd AndAlso (SpotList(SpotNr).ProgAfter = ThisProg OrElse SpotList(SpotNr).ProgBefore = ThisProg))
                If UseBreakbumpers AndAlso (SpotList(SpotNr).ProgBefore = ThisProg AndAlso SpotList(SpotNr).ProgAfter = ThisProg) Then TmpList.Add(SpotNr)
                If UseBillboards AndAlso (SpotList(SpotNr).ProgBefore = ThisProg AndAlso SpotList(SpotNr).ProgAfter <> ThisProg) Then TmpList.Add(SpotNr)
                SpotNr += 1
            End While
            SpotNr -= 1
            'For Each TmpR As Long In TmpList
            '    Debug.Print(TmpR & vbTab & Format(Date.FromOADate(Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aDate, TmpR)), "Short date") & vbTab & Parent.Karma.KarmaAdedge.convertTime(Connect.eTimeModes.tmSecNice, Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aFromTime, TmpR)) & vbTab & Parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aChannel, TmpR) & vbTab & ThisProgAfter)
            'Next
            'If TmpList.Count = 1 Then
            '    Return New List(Of Long)
            'Else
            '    Return TmpList
            'End If
            Return TmpList
        End Function

        Private Function GetSponsorshipSpots(ByVal SpotNr As Long, ByVal SpotList As List(Of cKarmaSpot), ByVal UseBillboards As Boolean, ByVal UseBreakbumpers As Boolean) As List(Of Long)
            Dim TmpList As New List(Of Long)
            Dim ThisProg As String = SpotList(SpotNr).ProgAfter
            Dim OrigSpotNr As Long = SpotNr
            Dim TmpR As Long = SpotList(SpotNr).ListIndex
            SpotNr -= SpotList(SpotNr).SpotInBreak - 1
            While SpotNr > 0 AndAlso SpotList(SpotNr).ProgBefore = ThisProg
                SpotNr -= 1
                SpotNr -= SpotList(SpotNr).SpotInBreak - 1
            End While
            'Add the last spot in the break before
            SpotNr += SpotList(SpotNr).SpotCount - 1
            If _parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, TmpR) <> _parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, TmpR) Then
                If UseBillboards Then TmpList.Add(TmpR)
            End If
            TmpR += 1
            While TmpR < SpotList(SpotList.Count - 1).ListIndex AndAlso ThisProg = _parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, TmpR) AndAlso ThisProg = _parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, TmpR)
                If UseBreakbumpers Then TmpList.Add(TmpR)
                TmpR += _parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandSpotCount, TmpR)
                If UseBreakbumpers Then TmpList.Add(TmpR - 1)
            End While
            If TmpR < SpotList(SpotList.Count - 1).ListIndex AndAlso _parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, TmpR) <> _parent.Karma.KarmaAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, TmpR) Then
                If UseBillboards Then TmpList.Add(TmpR)
            End If
            Return TmpList
        End Function

        Private fp(), pp(), pc(), Sum As Single, j As Integer, i As Integer, c As Double
        Private NBDa As Single
        Private NBDb As Single
        Private k As Double
        Private ap As Double
        Private X As Double
        Private ro As Double
        Private suro As Double

        Private Sub NBD(ByRef fc As Object, ByVal tp As Single, ByVal tc As Single, ByVal n As Integer)
            'NBD: Basic Input and declaration
            ReDim fp(n), pp(n), pc(n)
            Sum = 0
            'NBD:Start        NBD-function as described in BARB
            j = 0
            If n <> 1 Then
                While fc(j) = 0
                    fp(j) = 0
                    j = j + 1
                    tc = tc - 100
                    tp = tp - 100
                End While
                If fc(j) = 1 Then
                    '100% Reach
                    If j = n And tp > 0 Then tp = 0
                    fp(j) = 1 - Math.Abs(tp) / 100
                    If tp >= 0 Then
                        If j < n Then fp(j + 1) = tp / 100
                    Else
                        fp(j - 1) = -tp / 100
                        If j < n Then fp(j + 1) = 0
                    End If
                    For i = j + 2 To n
                        fp(i) = 0
                    Next i
                Else
                    c = tc / (100 * Math.Log(fc(j)))
                    If c < -1 Then
                        NBDa = -2 * (1 + c)
                        NBDb = NBDa
                        NBDa = c * (NBDa - (1 + NBDa) * Math.Log(1 + NBDa)) / (1 + NBDa + c)
                        While Math.Abs(NBDb - NBDa) >= 0.0001
                            NBDb = NBDa
                            NBDa = c * (NBDa - (1 + NBDa) * Math.Log(1 + NBDa)) / (1 + NBDa + c)
                        End While
                        K = tc / (100 * NBDa)
                        ap = NBDa * tp / tc
                        pc(j) = fc(j)
                        pp(j) = (1 / (1 + ap)) ^ K
                        fp(j) = pp(j)
                        Sum = fp(j)
                        NBDa = NBDa / (1 + NBDa)
                        ap = ap / (1 + ap)
                        For i = j + 1 To n - 1
                            X = (K + i - j - 1) / (i - j)
                            pc(i) = X * NBDa * pc(i - 1)
                            pp(i) = X * ap * pp(i - 1)
                            fp(i) = pp(i) + fc(i) - pc(i)
                            Sum = Sum + fp(i)
                        Next i
                    End If
                End If
            End If
            If n = 1 Or c >= -1 Then
                ro = tc / 100
                suro = tp / 100
                pc(j) = Math.Exp(-ro)
                pp(j) = Math.Exp(-suro)
                fp(j) = pp(j) + fc(j) - pc(j)
                Sum = fp(j)
                For i = j + 1 To n - 1
                    pc(i) = ro * pc(i - 1) / (i - j)
                    pp(i) = suro * pp(i - 1) / (i - j)
                    fp(i) = pp(i) + fc(i) - pc(i)
                    Sum = Sum + fp(i)
                Next i
            End If
            fp(n) = 1 - Sum
            For X = 1 To n
                fc(X) = fp(X)
            Next
            'NBD:End

        End Sub

        Public Sub CalculateMichaelisMenten(Optional ByVal Freq = 1)
            Dim c As Integer
            Dim r As Long
            Dim TRP As Single
            Dim b As Single
            Dim SumB As Single
            Dim x1, x2, y1, y2, SumXY, SumX2

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
                _parent.Karma.KarmaAdedge.clearGroup()
                For Each _spot As cKarmaSpot In (From _kspot As cKarmaSpot In _parent.Karma.Spots Select _kspot Where _kspot.HasBeenPicked)
                    _parent.Karma.KarmaAdedge.addToGroup(_spot.ListIndex)
                Next
                _parent.Karma.KarmaAdedge.recalcRF(Connect.eSumModes.smGroup)
                SumXY = 0
                SumX2 = 0
                For r = 1 To _parent.Karma.KarmaAdedge.getGroupCount - 1
                    TRP = TRP + _parent.Karma.KarmaAdedge.getUnitGroup(Connect.eUnits.uTRP, r, 0, 0, 0)
                    x1 = TRP - _parent.Karma.KarmaAdedge.getUnitGroup(Connect.eUnits.uTRP, r, 0, 0, 0)
                    x2 = TRP
                    y1 = _parent.Karma.KarmaAdedge.getRF(r - 1, , , , Freq)
                    y2 = _parent.Karma.KarmaAdedge.getRF(r, , , , Freq)
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

        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Public Property TrinityCampaign() As Trinity.cKampanj
            Get
                Return _trinityCampaign
            End Get
            Set(ByVal value As Trinity.cKampanj)
                _trinityCampaign = value
            End Set
        End Property
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

        Public ReadOnly Property Reach(ByVal Iteration As Integer, ByVal Freq As Byte, Optional ByVal Target As Trinity.cKampanj.ReachTargetEnum = Trinity.cKampanj.ReachTargetEnum.rteMainTarget) As Single
            Get
                Dim i As Integer

                Dim _reach As Single = 0
                On Error GoTo ErrHandle
                If Iteration = 0 Then
                    For i = 1 To Iterations
                        _reach += mvarReach(i, Freq, Target)
                    Next
                    _reach /= Iterations
                Else
                    _reach = mvarReach(Iteration, Freq, Target)
                End If
                Return _reach

ErrHandle:
                Return 0
            End Get
        End Property

        Public ReadOnly Property ReachAtTRP(ByVal TRP As Single, Optional ByVal Freq As Integer = 1) As Single
            Get
                Dim _fc As Object = mvarFC.Clone

                NBD(_fc, TRP, Me.TRP(0), mvarN - 1)

                Dim _reach As Single = 0
                For i As Integer = Freq To mvarN
                    'If (_fc(i) > 0) Then
                    _reach += _fc(i)
                    'End If
                Next
                'If Freq > 1 AndAlso _fc(1) < 0 Then _reach += _fc(1)
                Return _reach * 100
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
            Dim ChanStr As String

            ChanStr = ""
            For Each TmpChan As String In _parent.Karma.Channels
                ChanStr = ChanStr & TrinityCampaign.Channels(TmpChan).AdEdgeNames & ","
            Next

            Adedge.setArea(TrinityCampaign.Area)
            Trinity.Helper.AddTargetsToAdedge(Adedge, False, True)
            'Adedge.setTargetMnemonic(Helper.CreateTargetString(False, True)) ' CreateTargetString(False, True)
            Adedge.setBrandType("COMMERCIAL,SPONSOR")
            Adedge.setChannelsArea(ChanStr, TrinityCampaign.Area)
            Adedge.setPeriod(_parent.Karma.ReferencePeriod)
            'Adedge.setUniverseUserDefined(TrinityCampaign.MainTarget.SecondUniverse)

            SpotCount = Adedge.Run(False, False, 0)

            For s = 1 To 14
                mvarProfileTRP(s) = 0
            Next
            For c = 1 To Iterations
                Adedge.clearGroup()
                For Each _spot As cKarmaSpot In (From _kspot As cKarmaSpot In _parent.Karma.Spots Select _kspot Where _kspot.HasBeenPicked)
                    Adedge.addToGroup(_spot.ListIndex)
                Next
                For s = 1 To 14
                    mvarProfileTRP(s) = mvarProfileTRP(s) + Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , , Trinity.Helper.TargetIndex(Adedge, TrinityCampaign.ThirdTarget) + s)
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

        Public Sub New(ByVal Parent As cKarmaCampaigns)
            _parent = Parent
        End Sub
    End Class
End Namespace