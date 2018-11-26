Imports clTrinity.CultureSafeExcel

Namespace Trinity
    Public Class cPreAnalysisExcel
        Inherits cExcelAnalysis

        Private _progress As Integer = 0
        ''' <summary>
        ''' Variable to hold the film counter for a week
        ''' </summary>
        Private _filmNumber As Integer
        ''' <summary>
        ''' Variable to hold the week counter for a booking type
        ''' </summary>
        Private _weekNumber As Integer
        ''' <summary>
        ''' Variable to hold the booking type counter for each channel. -1 means no booking type has yet been added
        ''' </summary>
        Private _btNumber As Integer
        ''' <summary>
        ''' Variable to hold the channel counter for each campaign
        ''' </summary>
        Private _chanNumber As Integer
        ''' <summary>
        ''' Variable to hold the topmost row of the information that is currently printed
        ''' </summary>
        Private _topRow As Integer
        ''' <summary>
        ''' Collection of campaign filmcodes
        ''' </summary>
        Private _filmCodeCollection As New Dictionary(Of String, Double)
        ''' <summary>
        ''' Variable holding the amount of TRPs from compensations
        ''' </summary>
        Private _trpComp As Double

        ''' <summary>
        ''' Initializes a new instance of the <see cref="cPreAnalysisExcel" /> class.
        ''' </summary>
        ''' <param name="Campaign">The campaign to be analyzed.</param>
        ''' <param name="Excel">The excel workbook to print analysis to.</param>
        Sub New(ByVal Campaign As cKampanj, ByVal Excel As Application)
            MyBase.New(Campaign, Excel)
            ParseVariables()
        End Sub

        ''' <summary>
        ''' Prints the planned reach.
        ''' </summary>
        Friend Overrides Sub PrintReach()
            'Prints the numbers for 1+ to 10+ reach
            With _excel.Sheets(GetSheet())
                'Print frequency focus and estimated reach at frequency focus
                .Cells(Val(GetVariable("PlanFFRow")), Val(GetVariable("PlanFFCol"))).Value = (_campaign.FrequencyFocus + 1) & "+"
                .Cells(Val(GetVariable("PlanFFRow")), Val(GetVariable("PlanFFCol")) + 1).Value = _campaign.ReachGoal(_campaign.FrequencyFocus + 1)
                'Print estimated reach on each frequency level
                For i As Integer = 1 To 10
                    .Cells(Val(GetVariable("PlanReachRow")), Val(GetVariable("PlanReachCol")) + i - 1).Value = _campaign.ReachGoal(i)
                Next
            End With
        End Sub

        ''' <summary>
        ''' Prints the film list.
        ''' </summary>
        Friend Overrides Sub PrintFilmList()
            Dim _filmNumber As Integer = 0
            With _excel.Sheets(GetSheet())
                For Each _film As Trinity.cFilm In _campaign.Channels(1).BookingTypes(1).Weeks(1).Films
                    .Cells(Val(GetVariable("PlanFilmcodeRow")) + _filmNumber, Val(GetVariable("PlanFilmcodeCol"))).Value = _film.Name
                    .Cells(Val(GetVariable("PlanFilmcodeRow")) + _filmNumber, Val(GetVariable("PlanFilmcodeCol")) + 1).Value = _film.Description
                    .Cells(Val(GetVariable("PlanFilmcodeRow")) + _filmNumber, Val(GetVariable("PlanFilmcodeCol")) + 2).Value = _film.FilmLength
                    _filmNumber += 1
                Next
            End With
        End Sub

        ''' <summary>
        ''' Gets the Excel sheet where data should be written.
        ''' </summary>
        ''' <returns></returns>
        Friend Overrides Function GetSheet() As String
            Return GetVariable("PlanSheet", "Planned")
        End Function

        ''' <summary>
        ''' Prints the channels.
        ''' </summary>
        Friend Overrides Sub PrintChannels()
            _progress = 0
            _chanNumber = -1
            For Each _chan As Trinity.cChannel In _campaign.Channels
                PrintChannel(_chan)
            Next
            If ShowCombinationsAsSingleRow Then
                PrintCombinations()
            End If
        End Sub

        Private _progressMessage As String = "Creating pre-campaign analysis... "
        ''' <summary>
        ''' Gets or sets the message to be displayed in the progress window.
        ''' </summary>
        ''' <value>
        ''' The progress message.
        ''' </value>
        Public Property ProgressMessage() As String
            Get
                Return _progressMessage
            End Get
            Set(ByVal value As String)
                _progressMessage = value
            End Set
        End Property

        ''' <summary>
        ''' Prints one channel.
        ''' </summary>
        ''' <param name="Channel">The channel.</param>
        Friend Overrides Sub PrintChannel(ByVal Channel As cChannel)


            _progress += 1
            SetProgress(_progressMessage & Channel.ChannelName, (_progress / _campaign.Channels.Count) * 100)
            _btNumber = -1
            _topRow = Val(GetVariable("PlanChannelSumsRow")) + ((_chanNumber + 1) * (Val(GetVariable("Bookingtypes")) + 2))
            For Each _bt As Trinity.cBookingType In (From bt As Trinity.cBookingType In Channel.BookingTypes Select bt Where bt.BookIt AndAlso Not (ShowCombinationsAsSingleRow AndAlso (Not bt.ShowMe OrElse (bt.IsCompensation AndAlso bt.Combination IsNot Nothing AndAlso bt.Combination.ShowAsOne))))
                PrintBookingType(_bt)
                _btNumber += 1
            Next
        End Sub

        Friend Overrides Sub PrintCombinations()
            With _excel.Sheets(GetSheet())
                For Each c As Trinity.cCombination In _campaign.Combinations
                    If c.ShowAsOne Then
                        _btNumber = -1
                        _topRow = Val(GetVariable("PlanChannelSumsRow")) + ((_chanNumber + 1) * (Val(GetVariable("Bookingtypes")) + 2))
                        For Each cc As Trinity.cCombinationChannel In c.Relations
                            If _btNumber = -1 Then
                                _chanNumber += 1
                                _btNumber = 0
                            End If
                            If _btNumber < GetVariable("Bookingtypes") Then
                                PrintBookingType(cc.Bookingtype, c.Name, cc.Bookingtype.ParentChannel.Shortname & " " & cc.Bookingtype.Name)
                            End If
                            _btNumber += 1
                        Next
                    End If
                Next
            End With
        End Sub

        ''' <summary>
        ''' Prints one booking type.
        ''' </summary>
        ''' <param name="BookingType">Type of the booking.</param>
        Friend Overrides Sub PrintBookingType(ByVal BookingType As cBookingType, Optional ByVal ChannelLabel As String = "", Optional ByVal BookingtypeLabel As String = "")

            Dim _shortName As String = BookingType.ParentChannel.Shortname

            If _btNumber = -1 Then
                _chanNumber += 1
                _btNumber = 0
            End If
            If ChannelLabel = "" Then
                ChannelLabel = BookingType.ParentChannel.ChannelName
            Else
                _shortName = ChannelLabel
            End If
            If BookingtypeLabel = "" Then BookingtypeLabel = BookingType.Name

            With _excel.Sheets(GetSheet())
                If _btNumber < GetVariable("Bookingtypes") Then
                    .Cells(_topRow, 1).Value = ChannelLabel
                    .Cells(_topRow + Val(GetVariable("Bookingtypes")), 1).Value = _shortName
                    .Cells(_topRow + _btNumber, 2).Value = BookingtypeLabel

                    PrintCostPerFilmcode(BookingType)

                    PrintBudgets(BookingType)

                    'Print Estimated spot count
                    .Cells(_topRow + _btNumber, Val(GetVariable("PlanSpotsCol"))).Value = BookingType.EstimatedSpotCount

                    'Prints buying target
                    .Cells(_topRow + _btNumber, Val(GetVariable("PlanChannelSumsCol"))).Value = BookingType.BuyingTarget.TargetName
                    'Print buying target universe size
                    If BookingType.BuyingTarget.getUniSizeNat(_campaign.StartDate) > 0 Then
                        .Cells(_topRow + _btNumber, Val(GetVariable("PlanChannelSumsCol")) + 2).Value = BookingType.BuyingTarget.getUniSizeNat(_campaign.StartDate) * 1000
                    Else
                        .Cells(_topRow + _btNumber, Val(GetVariable("PlanChannelSumsCol")) + 2).Value = BookingType.BuyingTarget.Target.UniSize * 1000
                    End If
                    'Prints Buying Target CPT
                    .Cells(_topRow + _btNumber, Val(GetVariable("PlanChannelSumsCol")) + 3).Value = BookingType.BuyingTarget.NetCPT

                    'Prints target indexes for the booking type
                    If VariableExists("TargetIndexesCol") Then
                        .Cells(_topRow + _btNumber, Val(GetVariable("TargetIndexesCol"))).Value = BookingType.IndexMainTarget
                    End If

                    'Prints the commissions for each booking type individually if the variable PlanMediaNetRowDetailed is set
                    'This was requested because it's possible that different channels have different commissions
                    If VariableExists("PlanMediaNetRowDetailed") Then
                        .Cells(GetVariable("PlanMediaNetRowDetailed") + _chanNumber + _btNumber + 1, GetVariable("PlanMediaNetColDetailed") + 0).Value = BookingType.ParentChannel.Shortname & " " & BookingType.Name
                        .Cells(GetVariable("PlanMediaNetRowDetailed") + _chanNumber + _btNumber + 1, GetVariable("PlanMediaNetColDetailed") + 1).Value = BookingType.ParentChannel.AgencyCommission * -1
                        .Cells(GetVariable("PlanMediaNetRowDetailed") + _chanNumber + _btNumber + 1, GetVariable("PlanMediaNetColDetailed") + 2).Value = BookingType.PlannedNetBudget * BookingType.ParentChannel.AgencyCommission * -1
                    End If

                    'Print Natural delivery
                    If VariableExists("PlanNaturalDeliveryRow") AndAlso VariableExists("PlanNaturalDeliveryCol") Then
                        .Cells(GetVariable("PlanNaturalDeliveryRow") + _chanNumber + _btNumber + 1, GetVariable("PlanNaturalDeliveryCol") + 0).Value = BookingType.ParentChannel.Shortname & " " & BookingType.Name
                        .Cells(GetVariable("PlanNaturalDeliveryRow") + _chanNumber + _btNumber + 1, GetVariable("PlanNaturalDeliveryCol") + 1).Value = BookingType.EstimatedSpotCount
                        .Cells(GetVariable("PlanNaturalDeliveryRow") + _chanNumber + _btNumber + 1, GetVariable("PlanNaturalDeliveryCol") + 2).Value = BookingType.AverageRating
                    End If
                End If
            End With
            _weekNumber = 0
            For Each _week As Trinity.cWeek In BookingType.Weeks
                PrintWeek(_week)
                _weekNumber += 1
            Next
        End Sub

        ''' <summary>
        ''' Prints a week.
        ''' </summary>
        ''' <param name="Week">The week.</param>
        Friend Overrides Sub PrintWeek(ByVal Week As Trinity.cWeek)
            _trpComp = 0
            If IncludeCompensations Then
                PrintCompensations(Week)
            End If
            With _excel.Sheets(GetSheet())
                .Cells(_topRow - 1, Val(GetVariable("PlanChannelWeekCol")) + _weekNumber).Numberformat = "@"
                .Cells(_topRow - 1, Val(GetVariable("PlanChannelWeekCol")) + _weekNumber).Value = Week.Name
                .Cells(_topRow + _btNumber, Val(GetVariable("PlanChannelWeekCol")) + _weekNumber).Value = Week.TRPBuyingTarget
                .Cells(_topRow - 1, Val(GetVariable("PlanNationalWeekCol")) + _weekNumber).Value = Week.Name
                .Cells(_topRow + _btNumber, Val(GetVariable("PlanNationalWeekCol")) + _weekNumber).Value = Week.TRP + _trpComp
                _filmNumber = 0
                For Each _film As Trinity.cFilm In Week.Films
                    PrintFilm(_film, Week)
                    _filmNumber += 1
                Next
                For _dp As Integer = 0 To _campaign.Dayparts.Count - 1
                    PrintDaypart(_dp, Week)
                Next
                If _chanNumber = 0 AndAlso _btNumber = 0 Then
                    'print weekly reach
                    Dim Row As Integer
                    If VariableExists("PlanWeeklyReachCol") Then
                        Row = GetVariable("PlanWeeklyReachRow")
                        .Cells(Row, GetVariable("PlanWeeklyReachCol") + _weekNumber).Value = _campaign.EstimatedWeeklyReach(Week.Name)
                    Else
                        Row = GetVariable("PlanChannelSumsRow") + GetVariable("Channels") * (GetVariable("Bookingtypes") + 2)
                        For i As Integer = 0 To _campaign.Channels(1).BookingTypes(1).Weeks.Count - 1
                            .Cells(Row, GetVariable("PlanNationalWeekCol") + i).Value = _campaign.EstimatedWeeklyReach(Week.Name)
                        Next
                    End If
                End If
            End With

        End Sub

        Friend Overrides Sub PrintCostPerFilmcode(ByVal BookingType As cBookingType)
            If VariableExists("PlanCostPerFilmcodeCol") Then
                Dim _rowPlus As Integer = 0
                With _excel.Sheets(GetSheet())
                    .Cells(GetVariable("PlanCostPerFilmcodeRow") + _chanNumber + _btNumber + _rowPlus, GetVariable("PlanCostPerFilmcodeCol")).Value = BookingType.ParentChannel.ChannelName
                    .Cells(GetVariable("PlanCostPerFilmcodeRow") + _chanNumber + _btNumber + _rowPlus, GetVariable("PlanCostPerFilmcodeCol") + 1).Value = BookingType.Shortname

                    If Not BookingType.IsSpecific Then
                        'If the booking type is not specifics, we calculate the cost by the film's ratings, share, index, and the week's net CPP30
                        Dim tmpBudget As Decimal = 0

                        For Each tmpWeek As Trinity.cWeek In BookingType.Weeks
                            For Each tmpFilm As Trinity.cFilm In tmpWeek.Films
                                If Not _filmCodeCollection.ContainsKey(tmpFilm.Filmcode) Then
                                    _filmCodeCollection.Add(tmpFilm.Filmcode, 0)
                                    _filmCodeCollection(tmpFilm.Filmcode) += tmpWeek.TRPBuyingTarget * (tmpFilm.Share / 100) * (tmpFilm.Index / 100) * tmpWeek.NetCPP30
                                Else
                                    _filmCodeCollection(tmpFilm.Filmcode) += tmpWeek.TRPBuyingTarget * (tmpFilm.Share / 100) * (tmpFilm.Index / 100) * tmpWeek.NetCPP30
                                End If
                            Next
                        Next
                        For Each tmpFilmcode As KeyValuePair(Of String, Double) In _filmCodeCollection
                            .Cells(GetVariable("PlanCostPerFilmcodeRow") + _chanNumber + _btNumber + _rowPlus, GetVariable("PlanCostPerFilmcodeCol") + 2).Value = tmpFilmcode.Key
                            .Cells(GetVariable("PlanCostPerFilmcodeRow") + _chanNumber + _btNumber + _rowPlus, GetVariable("PlanCostPerFilmcodeCol") + 3).Value = tmpFilmcode.Value
                            _rowPlus += 1
                        Next

                    Else
                        'If the booking type is specific, we just go through all spots and get their cost
                        For Each BookedSpot As Trinity.cBookedSpot In _campaign.BookedSpots

                            If BookedSpot.Channel.ChannelName = BookingType.ParentChannel.ChannelName Then
                                If BookedSpot.Bookingtype.Name = BookingType.Name Then
                                    If Not _filmCodeCollection.ContainsKey(BookedSpot.Filmcode) Then
                                        _filmCodeCollection.Add(BookedSpot.Filmcode, 0)
                                        _filmCodeCollection(BookedSpot.Filmcode) += BookedSpot.NetPrice
                                    Else
                                        _filmCodeCollection(BookedSpot.Filmcode) += BookedSpot.NetPrice
                                    End If
                                End If

                            End If

                        Next
                        'Make sure there is a space between one booking type and the next even if the BT has no films
                        If _filmCodeCollection.Count = 0 Then
                            _rowPlus += 1
                        Else
                            'Print the film codes and costs
                            For Each tmpFilmcode As KeyValuePair(Of String, Double) In _filmCodeCollection
                                .Cells(GetVariable("PlanCostPerFilmcodeRow") + _chanNumber + _btNumber + _rowPlus, GetVariable("PlanCostPerFilmcodeCol") + 2).Value = tmpFilmcode.Key
                                .Cells(GetVariable("PlanCostPerFilmcodeRow") + _chanNumber + _btNumber + _rowPlus, GetVariable("PlanCostPerFilmcodeCol") + 3).Value = tmpFilmcode.Value
                                _rowPlus += 1
                            Next
                        End If
                    End If
                End With
            End If
        End Sub

        ''' <summary>
        ''' Prints the budgets.
        ''' </summary>
        ''' <param name="BookingType">Booking type to print budgets for.</param>
        Friend Overrides Sub PrintBudgets(ByVal BookingType As cBookingType)
            With _excel.Sheets(GetSheet())
                If BookingType.ConfirmedNetBudget > 0 Then
                    'This was a special solution for MEC 
                    If Campaign.PrintPlannedGrossConfNet
                        .Cells(_topRow + _btNumber, 3).Value = BookingType.PlannedGrossBudget
                        .Cells(_topRow + _btNumber, 4).Value = BookingType.ConfirmedNetBudget
                    ElseIf Campaign.PrintPlannedNet And BookingType.PlannedNetBudget > 0 And Campaign.PrintConfirmedNet <> False Then
                        .Cells(_topRow + _btNumber, 3).Value = BookingType.PlannedGrossBudget
                        .Cells(_topRow + _btNumber, 4).Value = BookingType.PlannedNetBudget
                    Else
                        .Cells(_topRow + _btNumber, 3).Value = BookingType.ConfirmedGrossBudget
                        .Cells(_topRow + _btNumber, 4).Value = BookingType.ConfirmedNetBudget
                    End If
                Else
                    .Cells(_topRow + _btNumber, 3).Value = BookingType.PlannedGrossBudget
                    .Cells(_topRow + _btNumber, 4).Value = BookingType.PlannedNetBudget
                End If
                If VariableExists("BookingTypeCPP30Col") Then
                    .Cells(_topRow + _btNumber, Val(GetVariable("BookingTypeCPP30Col"))).Value = BookingType.PlannedNetBudget / BookingType.PlannedTRP30(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget)
                End If
            End With
        End Sub

        Friend Overrides Sub PrintCompensations(ByVal Week As cWeek)
            Dim _bt As Trinity.cBookingType = Week.Bookingtype
            If Not _bt.Compensations.Count = 0 AndAlso IncludeCompensations Then

                For Each c As Trinity.cCompensation In _bt.Compensations

                    Dim countActualDays As Integer = 0
                    'count the actual days the campaign will run in the compensation
                    For Each w As Trinity.cWeek In _campaign.Channels(1).BookingTypes(1).Weeks
                        For day As Integer = w.StartDate To w.EndDate
                            If day >= c.FromDate.ToOADate AndAlso day <= c.ToDate.ToOADate Then
                                countActualDays += 1
                            End If
                        Next
                    Next
                    If countActualDays <> 0 Then
                        If Week.StartDate >= c.FromDate.ToOADate AndAlso Week.EndDate <= c.ToDate.ToOADate Then
                            _trpComp += (Week.EndDate - Week.StartDate + 1) / countActualDays * (c.TRPs * (_bt.IndexMainTarget / 100))
                        Else
                            Dim i As Integer
                            Dim count As Integer = 0
                            For i = Week.StartDate To Week.EndDate
                                If i >= c.FromDate.ToOADate AndAlso i <= c.ToDate.ToOADate Then
                                    count += 1
                                End If
                            Next
                            _trpComp += count / countActualDays * (c.TRPs * (_bt.IndexMainTarget / 100))
                        End If
                    End If
                Next
            End If
        End Sub

        Friend Overrides Sub PrintFilm(ByVal Film As cFilm, ByVal Week As cWeek)
            With _excel.Sheets(GetSheet())
                'TRP
                If .Cells(Val(GetVariable("PlanFilmcodeRow")) + _filmNumber, Val(GetVariable("PlanFilmcodeCol")) + 4 + _chanNumber).Value Is Nothing Then
                    .Cells(Val(GetVariable("PlanFilmcodeRow")) + _filmNumber, Val(GetVariable("PlanFilmcodeCol")) + 4 + _chanNumber).Value = 0
                End If
                .Cells(Val(GetVariable("PlanFilmcodeRow")) + _filmNumber, Val(GetVariable("PlanFilmcodeCol")) + 4 + _chanNumber).Value += ((Week.TRP + _trpComp) * (Film.Share / 100))

                'TRP 30"
                If .Cells(Val(GetVariable("PlanFilmcodeRow")) + _filmNumber, Val(GetVariable("PlanFilmcodeCol")) + 4 + _chanNumber + Val(GetVariable("Channels"))).Value Is Nothing Then
                    .Cells(Val(GetVariable("PlanFilmcodeRow")) + _filmNumber, Val(GetVariable("PlanFilmcodeCol")) + 4 + _chanNumber + Val(GetVariable("Channels"))).Value = 0
                End If
                .Cells(Val(GetVariable("PlanFilmcodeRow")) + _filmNumber, Val(GetVariable("PlanFilmcodeCol")) + 4 + _chanNumber + Val(GetVariable("Channels"))).Value += ((Week.TRP + _trpComp) * (Film.Share / 100) * (Film.Index / 100))

                'TRP per film per week
                If .Cells(Val(GetVariable("PlanFilmcodeRow")) + _filmNumber, Val(GetVariable("PlanFilmcodeCol")) + 4 + Val(GetVariable("Channels")) * 2).Value Is Nothing Then
                    .Cells(Val(GetVariable("PlanFilmcodeRow")) + _filmNumber, Val(GetVariable("PlanFilmcodeCol")) + 4 + Val(GetVariable("Channels")) * 2 + _weekNumber).Value = 0
                End If
                .Cells(Val(GetVariable("PlanFilmcodeRow")) + _filmNumber, Val(GetVariable("PlanFilmcodeCol")) + 4 + Val(GetVariable("Channels")) * 2 + _weekNumber).Value += (Week.TRP + _trpComp) * (Film.Share / 100)
            End With
        End Sub

        Friend Overrides Sub PrintDaypart(ByVal Daypart As Integer, ByVal Week As cWeek)
            With _excel.Sheets(GetSheet())
                If .Cells(Val(GetVariable("PlanDaypartRow")) + _chanNumber, Val(GetVariable("PlanDaypartCol")) + 1 + Daypart).Value Is Nothing Then
                    .Cells(Val(GetVariable("PlanDaypartRow")) + _chanNumber, Val(GetVariable("PlanDaypartCol")) + 1 + Daypart).Value = 0
                End If
                .Cells(Val(GetVariable("PlanDaypartRow")) + _chanNumber, Val(GetVariable("PlanDaypartCol")) + 1 + Daypart).Value = .Cells(Val(GetVariable("PlanDaypartRow")) + _chanNumber, Val(GetVariable("PlanDaypartCol")) + 1 + Daypart).Value + (Week.TRP + _trpComp) * (Week.Bookingtype.MainDaypartSplit(Daypart) / 100)
            End With
        End Sub

        Friend Overrides Sub PrintCostDetails()
            With _excel.Sheets(GetSheet())
                Dim r As Integer = 2
                .Cells(Val(GetVariable("PlanMediaNetRow")) + 1, Val(GetVariable("PlanMediaNetCol"))).Value = "Commission"
                If _campaign.PlannedMediaNet = 0 Then
                    .Cells(Val(GetVariable("PlanMediaNetRow")) + 1, Val(GetVariable("PlanMediaNetCol")) + 1).Value = "0,0%"
                Else
                    .Cells(Val(GetVariable("PlanMediaNetRow")) + 1, Val(GetVariable("PlanMediaNetCol")) + 1).Value = -(_campaign.EstimatedCommission / _campaign.PlannedMediaNet)
                End If
                .Cells(Val(GetVariable("PlanMediaNetRow")) + 1, Val(GetVariable("PlanMediaNetCol")) + 2).Value = -_campaign.EstimatedCommission

                'The costs as a percentage of media net, easy bit
                For Each TmpCost As Trinity.cCost In _campaign.Costs
                    If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                        If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnMediaNet Then
                            .Cells(Val(GetVariable("PlanMediaNetRow")) + r, Val(GetVariable("PlanMediaNetCol"))).Value = TmpCost.CostName
                            .Cells(Val(GetVariable("PlanMediaNetRow")) + r, Val(GetVariable("PlanMediaNetCol")) + 1).Value = TmpCost.Amount
                            .Cells(Val(GetVariable("PlanMediaNetRow")) + r, Val(GetVariable("PlanMediaNetCol")) + 2).Value = TmpCost.Amount * _campaign.PlannedMediaNet
                            r = r + 1
                        End If
                    End If
                Next


                r = 0
                r = r + 1
                Dim UnitStr As String = ""
                'If 
                For Each TmpCost As Trinity.cCost In _campaign.Costs
                    If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                        If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNet Then
                            .Cells(Val(GetVariable("PlanNetRow")) + r, Val(GetVariable("PlanNetCol")) + 0).Value = TmpCost.CostName
                            .Cells(Val(GetVariable("PlanNetRow")) + r, Val(GetVariable("PlanNetCol")) + 1).Numberformat = "##,##0.0%"
                            .Cells(Val(GetVariable("PlanNetRow")) + r, Val(GetVariable("PlanNetCol")) + 1).Value = TmpCost.Amount
                            .Cells(Val(GetVariable("PlanNetRow")) + r, Val(GetVariable("PlanNetCol")) + 2).Value = TmpCost.Amount * _campaign.PlannedNet
                            r = r + 1
                        End If
                    ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeFixed Then
                        .Cells(Val(GetVariable("PlanNetRow")) + r, Val(GetVariable("PlanNetCol")) + 0).Value = TmpCost.CostName
                        .Cells(Val(GetVariable("PlanNetRow")) + r, Val(GetVariable("PlanNetCol")) + 2).Value = TmpCost.Amount
                        r = r + 1
                    ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeOnDiscount Then
                        Dim SumUnit As Single = 0
                        If TmpCost.CountCostOn Is Nothing Then
                            For Each TmpChan As Trinity.cChannel In _campaign.Channels
                                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                                    For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                                        Dim Discount As Single = TmpWeek.GrossBudget - TmpWeek.NetBudget
                                        SumUnit += (Discount * TmpCost.Amount)
                                    Next
                                Next
                            Next
                        Else
                            For Each TmpBT As Trinity.cBookingType In DirectCast(TmpCost.CountCostOn, Trinity.cChannel).BookingTypes
                                For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                                    Dim Discount As Single = TmpWeek.GrossBudget - TmpWeek.NetBudget
                                    SumUnit += (Discount * TmpCost.Amount)
                                Next
                            Next
                        End If
                        .Cells(Val(GetVariable("PlanNetRow")) + r, Val(GetVariable("PlanNetCol")) + 0).Value = TmpCost.CostName
                        .Cells(Val(GetVariable("PlanNetRow")) + r, Val(GetVariable("PlanNetCol")) + 1).Numberformat = "##,##0.0%"
                        .Cells(Val(GetVariable("PlanNetRow")) + r, Val(GetVariable("PlanNetCol")) + 1).Value = TmpCost.Amount
                        .Cells(Val(GetVariable("PlanNetRow")) + r, Val(GetVariable("PlanNetCol")) + 2).Value = SumUnit
                        r = r + 1

                    ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePerUnit Then
                        Dim SumUnit As Single = 0
                        If TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnSpots Then
                            For Each TmpChan As Trinity.cChannel In _campaign.Channels
                                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                                    If TmpBT.BookIt = True Then
                                        SumUnit = SumUnit + TmpBT.EstimatedSpotCount * TmpCost.Amount
                                    End If
                                Next
                            Next
                            UnitStr = " / Spot"
                        ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnBuyingTRP Then
                            For Each TmpChan As Trinity.cChannel In _campaign.Channels
                                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                                    SumUnit = SumUnit + TmpBT.TotalTRPBuyingTarget * TmpCost.Amount
                                Next
                            Next
                            UnitStr = " / TRP"
                        ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnBuyingTRP Then
                            For Each TmpChan As Trinity.cChannel In _campaign.Channels
                                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                                    SumUnit = SumUnit + TmpBT.TotalTRP * TmpCost.Amount
                                Next
                            Next
                            UnitStr = " / TRP"
                        End If
                        .Cells(Val(GetVariable("PlanNetRow")) + r, Val(GetVariable("PlanNetCol")) + 0).Value = TmpCost.CostName
                        .Cells(Val(GetVariable("PlanNetRow")) + r, Val(GetVariable("PlanNetCol")) + 1).Numberformat = "##,##0 kr"
                        .Cells(Val(GetVariable("PlanNetRow")) + r, Val(GetVariable("PlanNetCol")) + 1).Value = TmpCost.Amount
                        .Cells(Val(GetVariable("PlanNetRow")) + r, Val(GetVariable("PlanNetCol")) + 2).Value = SumUnit
                        r = r + 1
                    End If
                Next
                r = 0
                '.cells(Val(GetVariable("PlanNetNetRow")) + r, Val(GetVariable("PlanNetNetCol")) + 2).Value = _campaign.PlannedNetNet
                r = r + 1
                For Each TmpCost As Trinity.cCost In _campaign.Costs
                    If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                        If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNetNet Then
                            .Cells(Val(GetVariable("PlanNetNetRow")) + r, Val(GetVariable("PlanNetNetCol")) + 0).Value = TmpCost.CostName
                            .Cells(Val(GetVariable("PlanNetNetRow")) + r, Val(GetVariable("PlanNetNetCol")) + 1).Value = TmpCost.Amount
                            .Cells(Val(GetVariable("PlanNetNetRow")) + r, Val(GetVariable("PlanNetNetCol")) + 2).Value = TmpCost.Amount * _campaign.PlannedNetNet
                            r = r + 1
                        ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnRatecard Then
                            .Cells(Val(GetVariable("PlanNetNetRow")) + r, Val(GetVariable("PlanNetNetCol")) + 0).Value = TmpCost.CostName
                            .Cells(Val(GetVariable("PlanNetNetRow")) + r, Val(GetVariable("PlanNetNetCol")) + 1).Value = TmpCost.Amount
                            .Cells(Val(GetVariable("PlanNetNetRow")) + r, Val(GetVariable("PlanNetNetCol")) + 2).Value = TmpCost.Amount * _campaign.PlannedGross
                            r = r + 1
                        End If
                    End If
                Next
                '.Cells(Val(GetVariable("PlanCTCRow")), Val(GetVariable("PlanCTCCol")) + 2).Value = _campaign.PlannedTotCTC
            End With
        End Sub

        Friend Overrides Sub PrintPosInBreak()

        End Sub

        Friend Overloads Overrides Sub PrintDaypart(Daypart As Integer, Channel As cChannel)

        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class
End Namespace