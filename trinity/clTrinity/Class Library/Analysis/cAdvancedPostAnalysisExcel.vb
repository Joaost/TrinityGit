Imports clTrinity.CultureSafeExcel

Namespace Trinity
    Public Class cAdvancedPostAnalysisExcel
        Inherits cPostAnalysisExcel

        ''' <summary>
        ''' AdvantEdge object to use when working with competitors
        ''' </summary>
        Private _compAdedge As ConnectWrapper.Brands
        ''' <summary>
        ''' The current spot count in _compAdedge
        ''' </summary>
        Private _spotCount As Integer
        ''' <summary>
        ''' List of competitors that the analysis should be made on
        ''' </summary>
        Private _competitors As List(Of String)
        ''' <summary>
        ''' TRPs for my campaign
        ''' </summary>
        Private _myTRP As Single = 0
        ''' <summary>
        ''' TRPs for my category
        ''' </summary>
        Private _myCatTRP As Single

        Public Shadows Event DoAfterRun()

        Public Sub New(ByVal Campaign As Trinity.cKampanj, ByVal Excel As Application, ByVal Competitors As List(Of String))
            MyBase.New(Campaign, Excel)
            _competitors = Competitors
        End Sub

        Friend Overrides Sub Run(ByVal NoBeforeEvent As Boolean, ByVal NoAfterEvent As Boolean)
            MyBase.Run(NoBeforeEvent, True)
            _progressWindow.Show()
            CreateCompetitionReach()
            CreateHeatmap()
            CreateCompetitionSharesAndReachBuildup()
            CreateCompetitionSummary()
            CreateCategoryAnalysis()
            CreateIndexAnalysis()
            CreateUniqueReachAnalysis()
            CreateSpotlistAnalysis()

            RaiseEvent DoAfterRun()
            If _progressWindow IsNot Nothing Then
                _progressWindow.Close()
            End If
        End Sub

        Public Overrides Sub InitializeAdvantedge()
            MyBase.InitializeAdvantedge()

            Dim _sex As String
            Select Case Left(_campaign.MainTarget.TargetNameNice, 1)
                Case "A" : _sex = ""
                Case "M" : _sex = "M"
                Case "W" : _sex = "W"
                Case Else : _sex = ""
            End Select
            _compAdedge = New ConnectWrapper.Brands()
            Trinity.Helper.AddTarget(_compAdedge, _campaign.MainTarget)
            Trinity.Helper.AddTarget(_compAdedge, _campaign.SecondaryTarget)
            Trinity.Helper.AddTarget(_compAdedge, _campaign.ThirdTarget)
            _compAdedge.setTargetMnemonic(_sex & "3-11," & _sex & "12-14," & _sex & "15-19," & _sex & "20-24," & _sex & "25-29," & _sex & "30-34," & _sex & "35-39," & _sex & "40-44," & _sex & "45-49," & _sex & "50-54," & _sex & "55-59," & _sex & "60-64," & _sex & "65-69," & _sex & "70-99,", False)
            _compAdedge.clearList()
            _compAdedge.clearBrandFilter()
            _compAdedge.setBrandType("COMMERCIAL")
            _compAdedge.setBrandFilmCode(_campaign.AreaLog, _campaign.FilmcodeString)
            _compAdedge.setPeriod(Format(Date.FromOADate(_campaign.StartDate), "ddMMyy") & "-" & Format(Date.FromOADate(_campaign.EndDate), "ddMMyy"))
            _compAdedge.setArea(_campaign.Area)
            _compAdedge.setChannelsArea(_campaign.ChannelString, _campaign.Area)
            _compAdedge.setSplitOff()
            _compAdedge.registerCallback(Me)
        End Sub

        Sub CreateCompetitionReach()
            SetProgress("Creating competition analysis... 1/4", 0)

            _spotCount = _compAdedge.Run(False, False, _campaign.FrequencyFocus + 1)
            _myTRP = _compAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smAll, , , Trinity.Helper.TargetIndex(_compAdedge, _campaign.MainTarget))

            With _excel.Sheets(GetSheet())
                .Range(.cells(GetVariable("ActualReachCompRow") + 2, GetVariable("ActualReachCompCol") + 0).address & ":" & .cells(GetVariable("ActualReachCompRow") + 21, GetVariable("ActualReachCompCol") + 6).address).Value = ""
                .Range(.cells(GetVariable("ActualReachCompRow") + 2, GetVariable("ActualReachCompCol") + 8 + 0).address & ":" & .cells(GetVariable("ActualReachCompRow") + 21, GetVariable("ActualReachCompCol") + 14).address).Value = ""
                For _i As Integer = 0 To 13
                    .cells(GetVariable("ActualReachCompRow") + _i + 2, GetVariable("ActualReachCompCol") + 0).value = _compAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smAll, , , _i + 3)
                    .cells(GetVariable("ActualReachCompRow") + _i + 2, GetVariable("ActualReachCompCol") + 8 + 0).value = _compAdedge.getRF(_spotCount - 1, , , _i + 1, _campaign.FrequencyFocus + 1)
                Next
            End With
        End Sub

        Sub CreateHeatmap()
            With _excel.Sheets(GetSheet())
                For d As Integer = _campaign.StartDate To _campaign.EndDate
                    .cells(GetVariable("ActualHeatmapRow") + 1, GetVariable("ActualHeatmapCol") + d - _campaign.StartDate + 1).value = Date.FromOADate(d)
                    _compAdedge.clearGroup()
                    For s As Integer = 0 To _spotCount - 1
                        If _compAdedge.getAttrib(Connect.eAttribs.aDate, s) = d Then
                            _compAdedge.addToGroup(s)
                        End If
                    Next
                    For t As Integer = 1 To 14
                        If .cells(GetVariable("ActualHeatmapRow") + t + 1, GetVariable("ActualHeatmapCol") + d - _campaign.StartDate + 1).value = "" Then
                            .cells(GetVariable("ActualHeatmapRow") + t + 1, GetVariable("ActualHeatmapCol") + d - _campaign.StartDate + 1).value = 0
                        End If
                        .cells(GetVariable("ActualHeatmapRow") + t + 1, GetVariable("ActualHeatmapCol") + d - _campaign.StartDate + 1).value += _compAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , , t + 2)
                    Next
                Next
            End With
        End Sub

        Sub CreateCompetitionSharesAndReachBuildup()
            SetProgress("Creating competition analysis... 2/4", 0)

            'Create comma separated string of competitors
            Dim _prodStr As String = ""
            For Each _comp As String In _competitors
                _prodStr &= _comp & ","
            Next
            'Reset the AdvantEdge object
            _compAdedge.clearList()
            _compAdedge.clearBrandFilter()
            _compAdedge.setSplitOff()
            _compAdedge.setBrandType("COMMERCIAL")
            With _excel.Sheets(GetSheet())
                If _prodStr <> "" Then
                    'Do another AdvantEdge run, but only with the competitor products
                    _compAdedge.setBrandProduct(_campaign.AreaLog, _prodStr)
                    _spotCount = _compAdedge.Run(False, False, 10)

                    'Sum all TRPs for this category
                    _myCatTRP = _compAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smAll, , , Trinity.Helper.TargetIndex(_compAdedge, _campaign.MainTarget))

                    'Split the list of spots by product
                    Dim _comps = _compAdedge.setSplitVar("product")
                    CreateCompetitionShares(_comps)
                    CreateCompetitionReachBuildUp(_comps)
                    CreateCompetitionPerChannel()
                End If
            End With
        End Sub

        Sub CreateCompetitionShares(ByVal CompetitorCount As Integer)
            With _excel.Sheets(GetSheet())
                'Start iterating through the created list of competitors
                For _comp As Integer = 0 To CompetitorCount - 1
                    _compAdedge.setSplitList(_comp)
                    Dim _compCol As Integer = 0

                    'Find the column where values should be written. If we have previously written values for this product then we use that column - otherwise the first empty
                    While ((_compAdedge.getSplitName(_comp, 0) <> _campaign.ActualSpots(1).Product And .cells(GetVariable("ActualSharesRow") + 2, GetVariable("ActualSharesCol") + 2 + _compCol).value > _compAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smSplit, , , 0)) Or (_compAdedge.getSplitName(_comp, 0) = _campaign.ActualSpots(1).Product And .cells(GetVariable("ActualSharesRow") + 2, GetVariable("ActualSharesCol") + 2 + _compCol).value > _compAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smSplit, , , Trinity.Helper.TargetIndex(_compAdedge, _campaign.MainTarget)) - _myTRP)) And _compCol < 5
                        _compCol += 1
                    End While

                    'Make sure the cells we are about to write to are cleared
                    .Range(.cells(GetVariable("ActualSharesRow") + 2, GetVariable("ActualSharesCol") + 1).address & ":" & .cells(GetVariable("ActualSharesRow") + 2, GetVariable("ActualSharesCol") + 7).address).Value = ""
                    .Range(.cells(GetVariable("ActualReachBuildupCompRow") + 2, GetVariable("ActualReachBuildupCompCol") + 0).address & ":" & .cells(GetVariable("ActualReachBuildupCompRow") + 2, GetVariable("ActualReachBuildupCompCol") + 4).address).Value = ""
                    If _compCol < 5 Then
                        .cells(GetVariable("ActualSharesRow") + 2, GetVariable("ActualSharesCol") + 0).value = .cells(GetVariable("ActualSharesRow") + 2, GetVariable("ActualSharesCol") + 7).value + .cells(GetVariable("ActualSharesRow") + 2, GetVariable("ActualSharesCol") + 6).value
                        For w As Integer = 3 To _compCol Step -1
                            .cells(GetVariable("ActualSharesRow") + 2, GetVariable("ActualSharesCol") + 2 + w + 1).value = .cells(GetVariable("ActualSharesRow") + 2, GetVariable("ActualSharesCol") + 2 + w).value
                            .cells(GetVariable("ActualSharesRow") + 1, GetVariable("ActualSharesCol") + 2 + w + 1).value = .cells(GetVariable("ActualSharesRow") + 1, GetVariable("ActualSharesCol") + 2 + w).value
                            If VariableExists("ActualCompetitionCol") Then
                                For t As Integer = 0 To 2
                                    .cells(GetVariable("ActualCompetitionRow") + t + (w + 1) * 3, GetVariable("ActualCompetitionCol") + 2).value = .cells(GetVariable("ActualCompetitionRow") + t + w * 3, GetVariable("ActualCompetitionCol") + 2).value
                                    .cells(GetVariable("ActualCompetitionRow") + t + (w + 1) * 3, GetVariable("ActualCompetitionCol") + 3).value = .cells(GetVariable("ActualCompetitionRow") + t + w * 3, GetVariable("ActualCompetitionCol") + 3).value
                                    For f As Integer = 1 To 10
                                        .Cells(GetVariable("ActualCompetitionRow") + t + (w + 1) * 3, GetVariable("ActualCompetitionCol") + 3 + f).Value = .Cells(GetVariable("ActualCompetitionRow") + t + w * 3, GetVariable("ActualCompetitionCol") + 3 + f).Value
                                    Next
                                Next
                            End If
                        Next
                        .cells(GetVariable("ActualSharesRow") + 2, GetVariable("ActualSharesCol") + 2 + _compCol).value = _compAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smSplit, , , Trinity.Helper.TargetIndex(_compAdedge, _campaign.MainTarget))
                        .cells(GetVariable("ActualSharesRow") + 1, GetVariable("ActualSharesCol") + 2 + _compCol).value = _compAdedge.getSplitName(_comp, 0)
                        If VariableExists("ActualCompetitionCol") Then
                            .Cells(GetVariable("ActualCompetitionRow") + 0 + _compCol * 3, GetVariable("ActualCompetitionCol") + 2).Value = _compAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smSplit, , _campaign.TimeShift, Trinity.Helper.TargetIndex(_compAdedge, _campaign.MainTarget))
                            .Cells(GetVariable("ActualCompetitionRow") + 0 + _compCol * 3, GetVariable("ActualCompetitionCol") + 3).Value = _compAdedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smSplit, , _campaign.TimeShift, Trinity.Helper.TargetIndex(_compAdedge, _campaign.MainTarget))
                            For f As Integer = 1 To 10
                                Dim sc As Integer = _compAdedge.recalcRF(Connect.eSumModes.smSplit)
                                .Cells(GetVariable("ActualCompetitionRow") + 0 + _compCol * 3, GetVariable("ActualCompetitionCol") + 3 + f).Value = _compAdedge.getRF(sc - 1, 0, _campaign.TimeShift, Trinity.Helper.TargetIndex(_compAdedge, _campaign.MainTarget), f)
                            Next

                            .Cells(GetVariable("ActualCompetitionRow") + 1 + _compCol * 3, GetVariable("ActualCompetitionCol") + 2).Value = _compAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smSplit, , _campaign.TimeShift, Trinity.Helper.TargetIndex(_compAdedge, _campaign.SecondaryTarget))
                            .Cells(GetVariable("ActualCompetitionRow") + 1 + _compCol * 3, GetVariable("ActualCompetitionCol") + 3).Value = _compAdedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smSplit, , _campaign.TimeShift, Trinity.Helper.TargetIndex(_compAdedge, _campaign.SecondaryTarget))
                            For f As Integer = 1 To 10
                                Dim sc As Integer = _compAdedge.recalcRF(Connect.eSumModes.smSplit)
                                .Cells(GetVariable("ActualCompetitionRow") + 1 + _compCol * 3, GetVariable("ActualCompetitionCol") + 3 + f).Value = _compAdedge.getRF(sc - 1, 0, _campaign.TimeShift, Trinity.Helper.TargetIndex(_compAdedge, _campaign.SecondaryTarget), f)
                            Next

                            .Cells(GetVariable("ActualCompetitionRow") + 2 + _compCol * 3, GetVariable("ActualCompetitionCol") + 2).Value = _compAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smSplit, , _campaign.TimeShift, Trinity.Helper.TargetIndex(_compAdedge, _campaign.ThirdTarget))
                            .Cells(GetVariable("ActualCompetitionRow") + 2 + _compCol * 3, GetVariable("ActualCompetitionCol") + 3).Value = _compAdedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smSplit, , _campaign.TimeShift, Trinity.Helper.TargetIndex(_compAdedge, _campaign.ThirdTarget))
                            For f As Integer = 1 To 10
                                Dim sc As Integer = _compAdedge.recalcRF(Connect.eSumModes.smSplit)
                                .Cells(GetVariable("ActualCompetitionRow") + 2 + _compCol * 3, GetVariable("ActualCompetitionCol") + 3 + f).Value = _compAdedge.getRF(sc - 1, 0, _campaign.TimeShift, Trinity.Helper.TargetIndex(_compAdedge, _campaign.ThirdTarget), f)
                            Next
                        End If
                        'Stop
                    Else
                        .cells(GetVariable("ActualSharesRow") + 2, GetVariable("ActualSharesCol") + 7).value += _compAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smSplit, , , Trinity.Helper.TargetIndex(_compAdedge, _campaign.MainTarget))
                    End If
                Next
                Dim Found As Boolean = False
                For _compCol As Integer = 0 To 4
                    If .cells(GetVariable("ActualSharesRow") + 1, GetVariable("ActualSharesCol") + 2 + _compCol).value = _campaign.ActualSpots(1).Product Then
                        .cells(GetVariable("ActualSharesRow") + 2, GetVariable("ActualSharesCol") + 2 + _compCol).value = .cells(GetVariable("ActualSharesRow") + 2, GetVariable("ActualSharesCol") + 2 + _compCol).value - _myTRP
                        Found = True
                    End If
                Next
                .cells(GetVariable("ActualSharesRow") + 2, GetVariable("ActualSharesCol") + 1).value = _myTRP
            End With
        End Sub

        Sub CreateCompetitionReachBuildUp(ByVal CompetitorCount As Integer)
            With _excel.Sheets(GetSheet())
                For _comp As Integer = 0 To CompetitorCount - 1
                    Dim sc As Integer = _compAdedge.setSplitList(_comp)
                    _compAdedge.recalcRF(Connect.eSumModes.smSplit)
                    If _compAdedge.getSplitName(_comp, 0) <> _campaign.ActualSpots(1).Product Then
                        Dim _compCol As Integer = 1
                        While _compAdedge.getSplitName(_comp, 0) <> .cells(GetVariable("ActualSharesRow") + 1, GetVariable("ActualSharesCol") + 1 + _compCol).value And _compCol < 7
                            _compCol = _compCol + 1
                        End While
                        If _compCol < 7 Then
                            For _t As Integer = 1 To 14
                                .cells(GetVariable("ActualReachCompRow") + 1 + _t, GetVariable("ActualReachCompCol") + _compCol).value = _compAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smSplit, , , _t + 2)
                                .cells(GetVariable("ActualReachCompRow") + 1 + _t, GetVariable("ActualReachCompCol") + 8 + _compCol).value = _compAdedge.getRF(sc - 1, , , _t + 2, _campaign.FrequencyFocus + 1)
                            Next
                            .cells(GetVariable("ActualReachCompRow") + 14, GetVariable("ActualReachCompCol") + _compCol).value = _compAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smSplit, , , Trinity.Helper.TargetIndex(_compAdedge, _campaign.MainTarget))
                        End If
                        _compCol -= 1
                        If _compCol < 6 Then
                            .cells(GetVariable("ActualReachBuildupCompRow") - 1, _compCol + 2).value = .cells(GetVariable("ActualSharesRow") + 1, GetVariable("ActualSharesCol") + 2 + _compCol).value
                            Dim TRP As Single = 0
                            Dim q As Integer = GetVariable("ActualReachBuildupCompRow")
                            Dim _spot As Integer = 0
                            For s As Integer = 0 To _spotCount - 1
                                If _compAdedge.getAttrib(Connect.eAttribs.aBrandProduct, s) = _compAdedge.getSplitName(_comp, 0) Then
                                    TRP = TRP + _compAdedge.getUnit(Connect.eUnits.uTRP, s, , , Trinity.Helper.TargetIndex(_compAdedge, _campaign.MainTarget))
                                    If TRP > .cells(q, 1).value Then
                                        .cells(q, _compCol + 2).value = _compAdedge.getRF(_spot, , , Trinity.Helper.TargetIndex(_compAdedge, _campaign.MainTarget), 1) / 100
                                        q = q + 1
                                    End If
                                    _spot += 1
                                End If
                            Next
                        End If
                    End If
                Next
            End With
        End Sub

        Sub CreateCompetitionPerChannel()
            With _excel.Sheets(GetSheet())
                SetProgress("Creating competition analysis... 3/4", 0)
                If VariableExists("ActualCompetitionChannelRow") Then
                    'Split by channel and product
                    Dim _cpCount As Integer = _compAdedge.setSplitVar("channel,product")

                    Dim _row = GetVariable("ActualSummaryRow")
                    Dim _col As Integer
                    Dim offsetList As New List(Of String)
                    While .cells(_row, GetVariable("ActualSummaryCol")).value <> "" AndAlso .cells(_row, GetVariable("ActualSummaryCol")).value <> "0"
                        offsetList.Add(.cells(_row, GetVariable("ActualSummaryCol")).value)
                        _row += 1
                    End While

                    For _cp As Integer = 0 To _cpCount - 1
                        _compAdedge.setSplitList(_cp)
                        SetProgress(ProgressValue:=(_cp / (_cpCount - 1)) * 100)
                        Dim sc As Integer = _compAdedge.setSplitList(_cp)

                        _row = GetVariable("ActualCompetitionChannelRow") + 1
                        _col = GetVariable("ActualCompetitionChannelCol") + offsetList.IndexOf(Trinity.Helper.Adedge2Channel(_compAdedge.getSplitName(_cp, 0)).ChannelName) + 1
                        While .cells(GetVariable("ActualCompetitionChannelRow"), _col).value <> _compAdedge.getSplitName(_cp, 0) AndAlso .cells(GetVariable("ActualCompetitionChannelRow"), _col).value <> ""
                            _col += 1
                        End While
                        .cells(GetVariable("ActualCompetitionChannelRow"), _col).value = _compAdedge.getSplitName(_cp, 0)
                        While .cells(_row, GetVariable("ActualCompetitionChannelCol")).value <> _compAdedge.getSplitName(_cp, 1)
                            _row += 1
                        End While
                        .Cells(_row, _col).Value = _compAdedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smSplit, , _campaign.TimeShift, Trinity.Helper.TargetIndex(_compAdedge, _campaign.MainTarget))
                    Next
                End If
            End With
        End Sub

        Sub CreateCompetitionSummary()
            SetProgress("Creating competition analysis... 4/4", 0)
            _compAdedge.clearList()
            _compAdedge.clearBrandFilter()
            _compAdedge.setBrandType("COMMERCIAL")
            _compAdedge.Run(False)
            Dim _totTRP As Decimal = _compAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smAll, , , Trinity.Helper.TargetIndex(_compAdedge, _campaign.MainTarget))
            _excel.Sheets(GetSheet()).cells(GetVariable("ActualSharesRow") + 6, GetVariable("ActualSharesCol") + 3).value = _totTRP - _myCatTRP - _myTRP
        End Sub

        Sub CreateCategoryAnalysis()
            SetProgress("Creating category analysis... ", 0)

            _compAdedge.clearList()
            _compAdedge.clearBrandFilter()
            _compAdedge.setBrandType("COMMERCIAL")
            _spotCount = _compAdedge.Run(False)

            Dim _categories As New SortedList(Of String, Single)
            For s As Integer = 0 To _spotCount - 1
                If _categories.ContainsKey(_compAdedge.getAttrib(Connect.eAttribs.aBrandTopCategory, s)) Then
                    _categories(_compAdedge.getAttrib(Connect.eAttribs.aBrandTopCategory, s)) += _compAdedge.getUnit(Connect.eUnits.uTRP, s, , , Trinity.Helper.TargetIndex(_compAdedge, _campaign.MainTarget))
                Else
                    _categories.Add(_compAdedge.getAttrib(Connect.eAttribs.aBrandTopCategory, s), _compAdedge.getUnit(Connect.eUnits.uTRP, s, , , Trinity.Helper.TargetIndex(_compAdedge, _campaign.MainTarget)))
                End If
            Next
            For _cat As Integer = 0 To _categories.Count - 1
                With _excel.Sheets(GetSheet())
                    .Cells(GetVariable("ActualSharesRow") + 9, GetVariable("ActualSharesCol") + 1 + _cat).Value = _categories.Keys(_cat).ToString
                    .Cells(GetVariable("ActualSharesRow") + 10, GetVariable("ActualSharesCol") + 1 + _cat).Value = _categories(_categories.Keys(_cat).ToString)
                End With
            Next
        End Sub

        Sub CreateIndexAnalysis()
            Dim _adedge As New ConnectWrapper.Brands
            _adedge.setArea(_campaign.Area)
            _adedge.setPeriod(Format(Date.FromOADate(_campaign.StartDate), "ddMMyy") & "-" & Format(Date.FromOADate(_campaign.EndDate), "ddMMyy"))
            _adedge.setTargetMnemonic(_campaign.MainTarget.TargetName & "," & _campaign.AllAdults, True)
            Dim _row As Integer = GetVariable("ActualBuyingIndexRow")
            _progress = 0
            For Each _chan As Trinity.cChannel In _campaign.Channels
                _progress += 1
                If (From _bt As Trinity.cBookingType In _chan.BookingTypes Where _bt.BookIt AndAlso Not (ShowCombinationsAsSingleRow AndAlso Not _bt.ShowMe)).Count > 0 Then
                    SetProgress("Creating index analysis... " & _chan.ChannelName, (_progress / (_campaign.Channels.Count + _campaign.Combinations.Count)) * 100)
                    _campaign.CalculateSpots(CalculateReach:=False, UseFilters:=False, Bookingtypes:=_allBTs)
                    With _excel.Sheets(GetSheet())
                        If _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, _campaign.TargColl(_campaign.AllAdults, _campaign.Adedge) - 1) > 0 Then
                            .Cells(_row, GetVariable("ActualBuyingIndexCol") + 3).Value = (_campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, Trinity.Helper.TargetIndex(_campaign.Adedge, _campaign.MainTarget)) / _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, Trinity.Helper.TargetIndex(_campaign.Adedge, _campaign.AllAdults))) * 100
                        Else
                            .Cells(_row, GetVariable("ActualBuyingIndexCol") + 3).Value = 0
                        End If
                        _adedge.clearList()
                        _adedge.setChannelsArea(_chan.AdEdgeNames, _campaign.Area)
                        _adedge.Run(False, False, 0)
                        .Cells(_row, GetVariable("ActualBuyingIndexCol") + 2).Value = (_adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smAll, , , 0) / _adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smAll, , , 1)) * 100
                    End With
                    _row += 1
                End If
            Next
            If ShowCombinationsAsSingleRow Then
                For Each _comb As Trinity.cCombination In _campaign.Combinations
                    SetProgress("Creating index analysis... " & _comb.Name, 0)
                    If _comb.ShowAsOne Then
                        Dim _btList As List(Of Trinity.cBookingType) = (From _cc As Trinity.cCombinationChannel In _comb.Relations Select _cc.Bookingtype).ToList
                        Dim _chanStr As String = String.Join(",", (From _bt As Trinity.cBookingType In _btList Select _bt.ParentChannel.ChannelName).ToArray)
                        For Each cc As Trinity.cCombinationChannel In _comb.Relations
                            _btList.Add(cc.Bookingtype)
                            _chanStr &= cc.Bookingtype.ParentChannel.AdEdgeNames & ","
                        Next


                        _campaign.CalculateSpots(CalculateReach:=False, UseFilters:=False, Bookingtypes:=_btList)
                        With _excel.Sheets(GetSheet())
                            If _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, _campaign.TargColl(_campaign.AllAdults, _campaign.Adedge) - 1) > 0 Then
                                .Cells(_row, GetVariable("ActualBuyingIndexCol") + 3).Value = (_campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, Trinity.Helper.TargetIndex(_campaign.Adedge, _campaign.MainTarget)) / _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, Trinity.Helper.TargetIndex(_campaign.Adedge, _campaign.AllAdults))) * 100
                            Else
                                .Cells(_row, GetVariable("ActualBuyingIndexCol") + 3).Value = 0
                            End If
                            _adedge.clearList()
                            _adedge.setChannelsArea(_chanStr, _campaign.Area)
                            _adedge.Run(False, False, 0)
                            .Cells(_row, GetVariable("ActualBuyingIndexCol") + 2).Value = (_adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smAll, , , 0) / _adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smAll, , , 1)) * 100
                        End With
                        _row += 1

                    End If
                Next
            End If
        End Sub

        Sub CreateUniqueReachAnalysis()
            With _excel.Sheets(GetSheet())
                If VariableExists("ActualUniqueReachCol") Then
                    Dim _rf(0 To 9) As Single
                    Dim _sc As Integer = _campaign.Adedge.recalcRF(Connect.eSumModes.smAll)
                    For _f As Integer = 0 To 9
                        _rf(_f) = _campaign.Adedge.getRF(_sc - 1, , _campaign.TimeShift, Trinity.Helper.TargetIndex(_campaign.Adedge, _campaign.MainTarget), _f + 1)
                    Next
                    Dim _row As Integer = 0
                    For Each _chan As Trinity.cChannel In _campaign.Channels
                        SetProgress("Creating unique reach... " & _chan.ChannelName, 0)
                        If (From _bt As Trinity.cBookingType In _chan.BookingTypes Where _bt.BookIt AndAlso Not (ShowCombinationsAsSingleRow AndAlso Not _bt.ShowMe)).Count > 0 Then
                            _campaign.CalculateSpots(CalculateReach:=True, UseFilters:=False, ExcludeBookingtypes:=_allBTs)
                            .cells(GetVariable("ActualUniqueReachRow") + 2 + _row, GetVariable("ActualUniqueReachCol") + 0).value = _chan.ChannelName
                            For _f As Integer = 0 To 9
                                .Cells(GetVariable("ActualUniqueReachRow") + 2 + _row, GetVariable("ActualUniqueReachCol") + _f + 1).Value = _rf(_f) - _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, Trinity.Helper.TargetIndex(_campaign.Adedge, _campaign.MainTarget), _f + 1)
                            Next
                            _row += 1
                        End If
                    Next
                    If ShowCombinationsAsSingleRow Then
                        For Each _comb As Trinity.cCombination In _campaign.Combinations
                            SetProgress("Creating unique reach... " & _comb.Name, 0)
                            If _comb.ShowAsOne Then
                                Dim _btList As List(Of Trinity.cBookingType) = (From _cc As Trinity.cCombinationChannel In _comb.Relations Select _cc.Bookingtype).ToList

                                _campaign.CalculateSpots(CalculateReach:=True, UseFilters:=False, ExcludeBookingtypes:=_btList)
                                .cells(GetVariable("ActualUniqueReachRow") + 2 + _row, GetVariable("ActualUniqueReachCol") + 0).value = _comb.Name
                                For _f As Integer = 0 To 9
                                    .Cells(GetVariable("ActualUniqueReachRow") + 2 + _row, GetVariable("ActualUniqueReachCol") + _f + 1).Value = _rf(_f) - _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, Trinity.Helper.TargetIndex(_campaign.Adedge, _campaign.MainTarget), _f + 1)
                                Next
                                _row += 1
                            End If
                        Next
                    End If
                End If
            End With
        End Sub

        Sub CreateSpotlistAnalysis()

            Dim _column As Integer = 0
            Dim _adedge As New ConnectWrapper.Breaks

            _adedge.setArea(_campaign.Area)
            Trinity.Helper.AddTarget(_adedge, _campaign.MainTarget)
            _campaign.CalculateSpots(UseFilters:=False)
            For Each _chan As Trinity.cChannel In _campaign.Channels
                For Each _bt As Trinity.cBookingType In _chan.BookingTypes
                    If _bt.BookIt AndAlso _bt.IsSpecific Then

                        CreateBookingTypeSpotlistAnalysis(_bt, _column, _adedge)

                    End If
                Next
            Next
        End Sub

        ''' <summary>
        ''' Creates the booking type spotlist analysis.
        ''' </summary>
        ''' <param name="Bookingtype">The bookingtype.</param>
        ''' <param name="Column">The column.</param>
        Sub CreateBookingTypeSpotlistAnalysis(ByVal Bookingtype As Trinity.cBookingType, ByRef Column As Integer, ByVal Adedge As ConnectWrapper.Breaks)
            With _excel.Sheets(GetVariable("SpotlistSheet"))

                SetProgress("Creating spotlist analysis... " & Bookingtype.ToString, 0)
                _progress = 0

                Dim eventsTable As DataTable
                Dim _periodStr As String = ""
                Dim _row As Integer = 4
                Dim _breakCount As Long
                Dim _break As Long = 0

                Adedge.clearList()
                Adedge.setChannelsArea(Bookingtype.ParentChannel.AdEdgeNames, _campaign.Area)

                .Cells(1, Column + 1).Value = Bookingtype.ParentChannel.ChannelName
                .Cells(1, Column + 2).Value = Bookingtype.Name

                'Create SQL-string to get all breaks for this period (TODO: should maybe be moved to cDBReader??)
                Dim _dates As New List(Of KeyValuePair(Of Date, Date))
                For Each _week As Trinity.cWeek In Bookingtype.Weeks
                    _dates.Add(New KeyValuePair(Of Date, Date)(Date.FromOADate(_week.StartDate), Date.FromOADate(_week.EndDate)))
                    _periodStr &= Format(Date.FromOADate(_week.StartDate), "ddMMyy") & "-" & Format(Date.FromOADate(_week.EndDate), "ddMMyy") & ","
                Next
                eventsTable = DBReader.getEvents(_dates, Bookingtype.ParentChannel.ChannelName)

                'Get breaks from AdvantEdge
                Adedge.setPeriod(_periodStr)
                _breakCount = Adedge.Run()

                'Connect actual breks with the breaks from the database schedule and give each database break a net price
                For Each _dr As DataRow In eventsTable.Rows
                    _progress += 1
                    SetProgress(ProgressValue:=(_progress / (eventsTable.Rows.Count + _campaign.ActualSpots.Count)) * 100)

                    'Variable to hold distance to last checked break
                    Dim _lastDist As Integer

                    If _dr!Startmam >= 26 * 60 Then
                        _dr!Startmam -= 24 * 60
                        _dr!Date += 1
                    End If
                    .Cells(_row, Column + 1).Value = _dr!Date
                    .Cells(_row, Column + 2).Value = Trinity.Helper.Mam2Tid(_dr!StartMam)
                    .Cells(_row, Column + 3).Value = _dr!Name
                    Try
                        If IsDBNull(_dr.Item("EstimationTarget")) Then _dr.Item("EstimationTarget") = ""
                        If IsDBNull(_dr.Item("price")) Then _dr.Item("price") = 0
                        'Calculate the appropriate Net price for the break
                        If _dr.Item("UseCPP") Then
                            If Not Bookingtype.Pricelist.Targets(Trim(_dr.Item("EstimationTarget"))) Is Nothing Then
                                .Cells(_row, Column + 5).Value = Math.Round(_dr.Item("ChanEst") * (1 + (_dr.Item("Addition") / 100)) * (Bookingtype.Pricelist.Targets(Trim(_dr.Item("EstimationTarget"))).GetCPPForDate(_dr!Date, _dr!Daypart) * (Bookingtype.Indexes.GetIndexForDate(Date.FromOADate(_dr!Date), Trinity.cIndex.IndexOnEnum.eGrossCPP) / 100) * (1 - Bookingtype.BuyingTarget.Discount) * (Bookingtype.Indexes.GetIndexForDate(Date.FromOADate(_dr!Date), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100) * Bookingtype.BuyingTarget.GetCPPForDate(_dr!Date)), 0)
                            End If
                        Else
                            .Cells(_row, Column + 5).Value = Math.Round(_dr.Item("price") * (Bookingtype.Indexes.GetIndexForDate(Date.FromOADate(_dr!Date), Trinity.cIndex.IndexOnEnum.eGrossCPP) / 100) * (1 - Bookingtype.BuyingTarget.Discount) * (Bookingtype.Indexes.GetIndexForDate(Date.FromOADate(_dr!Date), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100), 0)
                        End If
                    Catch
                        .Cells(_row, Column + 5).Value = Math.Round(_dr.Item("price") * (Bookingtype.Indexes.GetIndexForDate(Date.FromOADate(_dr!Date), Trinity.cIndex.IndexOnEnum.eGrossCPP) / 100) * (1 - Bookingtype.BuyingTarget.Discount) * (Bookingtype.Indexes.GetIndexForDate(Date.FromOADate(_dr!Date), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100), 0)
                    End Try
                    If .cells(_row, Column + 5).value > 0 Then
                        .cells(_row, Column + 6).FormulaR1C1 = "=RC[-1]/RC[-2]"

                        'Step a little bit back
                        If _break > 10 Then
                            _break -= 10
                        Else
                            _break = 0
                        End If

                        While _break < _breakCount AndAlso Adedge.getAttrib(Connect.eAttribs.aDate, _break) < _dr!Date
                            _break += 1
                        End While
                        _lastDist = 9999
                        'Find the closest advantedge break for each database break
                        While _break < _breakCount AndAlso (Math.Abs(_dr!startMam - Adedge.getAttrib(Connect.eAttribs.aFromTime, _break) \ 60) < _lastDist OrElse Adedge.getAttrib(Connect.eAttribs.aBreaktitle, _break) <> "Break")
                            If Adedge.getAttrib(Connect.eAttribs.aBreaktitle, _break) = "Break" Then
                                _lastDist = Math.Abs(_dr!startMam - Adedge.getAttrib(Connect.eAttribs.aFromTime, _break) \ 60)
                            End If
                            _break += 1
                        End While
                        'The distance to this break is bigger than the last break we checked, so backup to that break - it is the closest match
                        _break -= 1
                        While Adedge.getAttrib(Connect.eAttribs.aBreaktitle, _break) <> "Break"
                            _break -= 1
                        End While
                        'And print the TRPs and program for the closest break
                        .cells(_row, Column + 4).value = Math.Round(Adedge.getUnit(Connect.eUnits.uTRP, _break), 1)
                        .cells(_row, Column + 7).value = Adedge.getAttrib(Connect.eAttribs.aBreakProgAfter, _break)
                        _row += 1
                    End If
                Next
                _row = 4
                For Each _spot As Trinity.cActualSpot In From _s As Trinity.cActualSpot In _campaign.ActualSpots Where _s.Bookingtype Is Bookingtype
                    _progress += 1
                    SetProgress(ProgressValue:=(_progress / (eventsTable.Rows.Count + _campaign.ActualSpots.Count)) * 100)

                    .Cells(_row, Column + 8).Value = _spot.AirDate
                    .Cells(_row, Column + 9).Value = Trinity.Helper.Mam2Tid(_spot.MaM)
                    .Cells(_row, Column + 11).Value = Math.Round(_spot.Rating30(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget), 1)
                    .cells(_row, Column + 13).FormulaR1C1 = "=RC[-1]/RC[-2]"
                    .Cells(_row, Column + 14).Value = _spot.ProgAfter
                    If _spot.MatchedSpot Is Nothing OrElse _spot.MatchedSpot.PriceNet = 0 Then
                        Dim _lastDist As Integer = 9999
                        For Each dr As DataRow In eventsTable.Rows
                            If dr!Date = _spot.AirDate Then
                                If Math.Abs(_spot.MaM - dr!Startmam) > _lastDist Then
                                    dr = dr.Table.Rows(dr.Table.Rows.IndexOf(dr) - 1)
                                    .Cells(_row, Column + 10).Value = dr!Name
                                    Try
                                        If IsDBNull(dr.Item("EstimationTarget")) Then dr.Item("EstimationTarget") = ""
                                        If IsDBNull(dr.Item("price")) Then dr.Item("price") = 0
                                        If dr.Item("UseCPP") Then
                                            If Not Bookingtype.Pricelist.Targets(Trim(dr.Item("EstimationTarget"))) Is Nothing Then
                                                .Cells(_row, Column + 12).Value = Math.Round(dr.Item("ChanEst") * (1 + (dr.Item("Addition") / 100)) * (Bookingtype.Pricelist.Targets(Trim(dr.Item("EstimationTarget"))).GetCPPForDate(dr!Date, dr!Daypart)) * (Bookingtype.Indexes.GetIndexForDate(Date.FromOADate(dr!Date), Trinity.cIndex.IndexOnEnum.eGrossCPP) / 100) * (1 - Bookingtype.BuyingTarget.Discount) * (Bookingtype.Indexes.GetIndexForDate(Date.FromOADate(dr!Date), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100) * (Bookingtype.BuyingTarget.GetCPPForDate(dr!Date)), 0)
                                            End If
                                        Else
                                            .Cells(_row, Column + 12).Value = Math.Round(dr.Item("price") * (Bookingtype.Indexes.GetIndexForDate(Date.FromOADate(dr!Date), Trinity.cIndex.IndexOnEnum.eGrossCPP) / 100) * (1 - Bookingtype.BuyingTarget.Discount) * (Bookingtype.Indexes.GetIndexForDate(Date.FromOADate(dr!Date), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100), 0)
                                        End If
                                    Catch
                                        .Cells(_row, Column + 12).Value = Math.Round(dr.Item("price") * (Bookingtype.Indexes.GetIndexForDate(Date.FromOADate(dr!Date), Trinity.cIndex.IndexOnEnum.eGrossCPP) / 100) * (1 - Bookingtype.BuyingTarget.Discount) * (Bookingtype.Indexes.GetIndexForDate(Date.FromOADate(dr!Date), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100), 0)
                                    End Try
                                    Exit For
                                End If
                                _lastDist = Math.Abs(_spot.MaM - dr!Startmam)
                            End If
                        Next
                    Else
                        .Cells(_row, Column + 10).Value = "-"
                        .Cells(_row, Column + 12).Value = Math.Round(_spot.MatchedSpot.PriceNet, 0)
                    End If
                    _row += 1
                Next
                Column += 14
            End With

        End Sub

        Friend Overloads Overrides Sub PrintDaypart(Daypart As Integer, Week As cWeek)

        End Sub

    End Class
End Namespace