Imports clTrinity.CultureSafeExcel

Namespace Trinity
    Public Class cPostAnalysisExcel
        Inherits cExcelAnalysis
        Implements Connect.ICallBack

        Public Shadows Event DoBeforeRun()

        ''' <summary>
        ''' List of bookingtypes to be printed for the currently active channel
        ''' </summary>
        Friend _allBTs As List(Of Trinity.cBookingType)

        Friend _progress As Integer = 0
        ''' <summary>
        ''' Variable to hold the film counter for a week
        ''' </summary>
        Friend _filmNumber As Integer
        ''' <summary>
        ''' Variable to hold the week counter for a booking type
        ''' </summary>
        Friend _weekNumber As Integer
        ''' <summary>
        ''' Variable to hold the booking type counter for each channel. -1 means no booking type has yet been added
        ''' </summary>
        Friend _btNumber As Integer
        ''' <summary>
        ''' Variable to hold the channel counter for each _campaign
        ''' </summary>
        Friend _chanNumber As Integer
        ''' <summary>
        ''' Variable to hold the topmost row of the information that is currently printed
        ''' </summary>
        Friend _topRow As Integer

        Friend _doPreAnalysis As Boolean = False
        ''' <summary>
        ''' Gets or sets a value indicating whether pre analysis should also be done.
        ''' </summary>
        ''' <value><c>true</c> if pre analysis should be done otherwise, <c>false</c>.</value>
        Public Property DoPreAnalysis() As Boolean
            Get
                Return _doPreAnalysis
            End Get
            Set(ByVal value As Boolean)
                _doPreAnalysis = value
            End Set
        End Property

        Friend Overrides Sub Run(ByVal NoBeforeEvent As Boolean, ByVal NoAfterEvent As Boolean)

            ' Run the base Run method, which includes the checktemplate method before we print the pre-analysis. If errors occurs the user can chose to stop run
            MyBase.Run(True, NoAfterEvent)

            ' As the Run can get an error message we must check if the priting should continue
            If Me.FlagCancelExport = False Then
                If Not NoBeforeEvent Then RaiseEvent DoBeforeRun()
                If _doPreAnalysis Then
                    CreateProgressWindow()
                    Dim _preAnalysis As New cPreAnalysisExcel(_campaign, _excel)
                    _preAnalysis.DoNotShowTemplateCheckAlerts = DoNotShowTemplateCheckAlerts
                    _preAnalysis.ShowCombinationsAsSingleRow = ShowCombinationsAsSingleRow
                    _preAnalysis.ShowProgressWindow = ShowProgressWindow
                    _preAnalysis.SetProgressWindow(_progressWindow)
                    _preAnalysis.AutoHideProgressWindow = False
                    AddHandler _preAnalysis.TemplateError, AddressOf PreAnalysisError
                    _preAnalysis.Run()
                End If
            End If
        End Sub

        Private Sub PreAnalysisError(sender As Object, e As TemplateErrorArgs)
            Me.FlagCancelExport = False
        End Sub

        ''' <summary>
        ''' Checks that the template contains the relevant variables.
        ''' </summary>
        ''' <returns></returns>
        Friend Overrides Function CheckTemplate() As System.Collections.Generic.List(Of String)
            Dim _list As List(Of String) = MyBase.CheckTemplate()
            If Not VariableExists("ActualNetValueCol") Then
                ShowTemplateError("The template you are using does not have support for printing" & vbCrLf & "the actual _campaign value." & vbCrLf & "Contact the system administrator if you want to use this feature.", _list)
            End If
            Return _list
        End Function

        ''' <summary>
        ''' Initializes a new instance of the <see cref="cPostAnalysisExcel" /> class.
        ''' </summary>
        ''' <param name="Campaign">The _campaign to be analyzed.</param>
        ''' <param name="Excel">The excel workbook to print analysis to.</param>
        Sub New(ByVal Campaign As cKampanj, ByVal Excel As Application)
            MyBase.New(Campaign, Excel)
            ParseVariables()
        End Sub

        ''' <summary>
        ''' Gets the Excel sheet where data should be written.
        ''' </summary>
        ''' <returns></returns>
        Friend Overrides Function GetSheet() As String
            Return GetVariable("ActualSheet", "Actual")
        End Function


        Friend Overrides Sub PrintBookingType(ByVal BookingType As cBookingType, Optional ByVal ChannelLabel As String = "", Optional ByVal BookingtypeLabel As String = "")
            If _btNumber = -1 Then
                _chanNumber += 1
                _btNumber = 0
            End If
            _campaign.CalculateSpots(UseFilters:=False, CalculateReach:=True, Bookingtype:=BookingType)
            With _excel.Sheets(GetSheet())
                .Cells(_topRow + _btNumber, 2).Value = BookingType.Name
                .Cells(_topRow + _btNumber, GetVariable("ActualSpotsCol")).Value = _campaign.Adedge.getGroupCount
                .Cells(_topRow + _btNumber, GetVariable("ActualChannelSumsCol") + 0).Value = BookingType.BuyingTarget.TargetName
                
                If VariableExists("ActualTRP30Col")
                    Dim tmpRow As Integer = GetVariable("ActualTRP30Row")
                    If ShowCombinationsAsSingleRow                        
                        tmpRow = tmpRow + _chanNumber 
                    Else
                        tmpRow = tmpRow + _chanNumber 
                    End If

                    Dim colWeek As Integer = 0          
                    Dim Trp30TopRow As Integer  = 0
                    Dim Trp30StartCol As Integer  = 0
                    For i As Integer = 1 To BookingType.Weeks.Count          
                        Dim TRP30 As Decimal = 0
                        Dim netValue As Integer = 0
                        
                        For each w As Trinity.cWeek In BookingType.Weeks
                            If w.Name = BookingType.Weeks(i).Name
                                For each tmpSpot As Trinity.cActualSpot in Campaign.ActualSpots
                                    If tmpSpot.Week.Name = w.Name
                                        If tmpSpot.Bookingtype is BookingType
                                            'TmpSpot.Rating30(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget)
                                            TRP30 += TmpSpot.Rating30(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget)                
                                            If  .Cells(GetVariable("ActualTRP30Row") - 1, GetVariable("ActualTRP30Col") + colWeek).Value <> ""                                                
                                                .Cells(GetVariable("ActualTRP30Row") - 1, GetVariable("ActualTRP30Col") + colWeek).Value = w.Name
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        Next                       
                        .Cells(tmpRow, GetVariable("ActualTRP30Col") + colWeek).Value += TRP30
                        If ShowCombinationsAsSingleRow                            
                            If ChannelLabel <>""
                                .Cells(tmpRow, GetVariable("ActualTRP30Col") -1 ).Value = ChannelLabel 
                            Else
                                .Cells(tmpRow, GetVariable("ActualTRP30Col") -1 ).Value = BookingType.ToString() & " "
                            end if

                        Else
                            .Cells(tmpRow, GetVariable("ActualTRP30Col") -1 ).Value = BookingType.ToString() & " "
                            
                        End If
                        colWeek = colWeek + 1
                    Next
                End If               

                If VariableExists("ActualNetValueCol") Then

                    'Print _campaign actual value
                    If TrinitySettings.DefaultArea = "NO" Then
                        If Campaign.PrintConfirmedNet
                            
                            .Cells(_topRow + _btNumber, GetVariable("ActualNetValueCol") + 0).Value = BookingType.ConfirmedNetBudget
                        Else
                            If BookingType.ActualGrossBudget IsNot Nothing Then
                                .Cells(_topRow + _btNumber, GetVariable("ActualNetValueCol") + 0).Value = BookingType.ActualNetValue
                            Else
                                .Cells(_topRow + _btNumber, GetVariable("ActualNetValueCol") + 0).Value = BookingType.ConfirmedNetBudget
                            End If
                        End If
                    Else
                        If Campaign.PrintConfirmedNet Then
                            .Cells(_topRow + _btNumber, GetVariable("ActualNetValueCol") + 0).Value = BookingType.ConfirmedNetBudget
                        Else
                            If BookingType.ActualGrossBudget IsNot Nothing Then
                                .Cells(_topRow + _btNumber, GetVariable("ActualNetValueCol") + 0).Value = BookingType.ActualNetValue
                            Else
                                .Cells(_topRow + _btNumber, GetVariable("ActualNetValueCol") + 0).Value = BookingType.ConfirmedNetBudget
                            End If
                        End If
                    End If
                End If

                If _campaign.Adedge.getGroupCount > 0 Then
                    .Cells(_topRow + _btNumber, GetVariable("ActualChannelSumsCol") + 1).Value = _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, Trinity.Helper.TargetIndex(_campaign.Adedge, BookingType.BuyingTarget.Target))
                Else
                    .Cells(_topRow + _btNumber, GetVariable("ActualChannelSumsCol") + 1).Value = 0
                End If
                If _campaign.Adedge.getGroupCount > 0 Then
                    .Cells(_topRow + _btNumber, GetVariable("ActualChannelSumsCol") + 2).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, Trinity.Helper.TargetIndex(_campaign.Adedge, BookingType.BuyingTarget.Target), 1)
                Else
                    .Cells(_topRow + _btNumber, GetVariable("ActualChannelSumsCol") + 2).Value = 0
                End If
                For i As Integer = 1 To 5
                    'Print reach per booking type
                    If _campaign.Adedge.getGroupCount = 0 Then
                        .Cells(_topRow + _btNumber, GetVariable("ActualNationalSumsCol") + 2 + i).Value = 0
                    Else
                        .Cells(_topRow + _btNumber, GetVariable("ActualNationalSumsCol") + 2 + i).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, Trinity.Helper.TargetIndex(_campaign.Adedge, _campaign.MainTarget), i)
                    End If
                Next
                'Calculate and print prime time TRP
                _campaign.CalculateSpots(UseFilters:=False, CalculateReach:=False, Bookingtype:=BookingType, Daypart:=_campaign.Dayparts.FirstPrimeIndex - 1)
                .Cells(_topRow + _btNumber, GetVariable("ActualNationalSumsCol") + 0).Value = _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1)
                'Calculate and print First In Break
                _campaign.CalculateSpots(UseFilters:=False, CalculateReach:=False, Bookingtype:=BookingType, PosInBreak:=1)
                .Cells(_topRow + _btNumber, GetVariable("ActualNationalSumsCol") + 1).Value = _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1)
                'Calculate and print Last In Break
                _campaign.CalculateSpots(UseFilters:=False, CalculateReach:=False, Bookingtype:=BookingType, PosInBreak:=3)
                .Cells(_topRow + _btNumber, GetVariable("ActualNationalSumsCol") + 1).Value = .Cells(_topRow + _btNumber, GetVariable("ActualNationalSumsCol") + 1).Value + _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1)
            End With

            _weekNumber = 0
            For Each _week As Trinity.cWeek In BookingType.Weeks
                PrintWeek(_week)
                _weekNumber += 1
            Next
            _hasPrintedWeekTotals = True
        End Sub

        Friend Overrides Sub PrintBudgets(ByVal BookingType As cBookingType)

        End Sub

        Friend Overrides Sub PrintChannel(ByVal Channel As cChannel)

            _allBTs = (From _bt As Trinity.cBookingType In Channel.BookingTypes Select _bt Where _bt.BookIt AndAlso Not (ShowCombinationsAsSingleRow AndAlso Not _bt.ShowMe)).ToList

            'Check that the channel has bookingtypes that should be printed - otherwise Exit Sub
            If _allBTs.Count = 0 Then Exit Sub

            _topRow = Val(GetVariable("ActualChannelSumsRow")) + ((_chanNumber + 1) * (Val(GetVariable("Bookingtypes")) + 2))

            Dim _usedChannels As Integer = (From _chan As Trinity.cChannel In _campaign.Channels Select _chan Where (From _bt As Trinity.cBookingType In _chan.BookingTypes Select _bt Where _bt.BookIt AndAlso Not (ShowCombinationsAsSingleRow AndAlso Not _bt.ShowMe)).Count > 0).Count
            _progress += 1
            SetProgress("Post-campaign analysing... " & Channel.ChannelName, (_progress / _usedChannels) * 100)

            _btNumber = -1
            For Each _bt As Trinity.cBookingType In (From bt As Trinity.cBookingType In Channel.BookingTypes Select bt Where bt.BookIt AndAlso Not (ShowCombinationsAsSingleRow AndAlso Not bt.ShowMe))
                PrintBookingType(_bt)
                _btNumber += 1
            Next

            With _excel.Sheets(GetSheet())
                .Cells(_topRow, 1).Value = Channel.ChannelName
                .Cells(_topRow + Val(GetVariable("Bookingtypes")), 1).Value = Channel.Shortname

                _campaign.CalculateSpots(UseFilters:=False, CalculateReach:=True, Bookingtypes:=_allBTs) 'Get all actual spots for the bookingtypes that should be printed for this channel
                If _campaign.Adedge.getGroupCount > 0 Then
                    'There are actual spots meeting the conditions
                    For i As Integer = 1 To 5
                        'Print reach for this channel in 1+ to 5+
                        .Cells(_topRow + Val(GetVariable("Bookingtypes")), GetVariable("ActualNationalSumsCol") + 2 + i).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1, i)
                    Next

                    'Summary
                    'Main target
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 3).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1, _campaign.FrequencyFocus + 1)
                    'Secondary target
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 4).Value = _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, _campaign.TargColl(_campaign.SecondaryTarget.TargetName, _campaign.Adedge) - 1)
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 5).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.SecondaryTarget.TargetName, _campaign.Adedge) - 1, 1)
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 6).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.SecondaryTarget.TargetName, _campaign.Adedge) - 1, _campaign.FrequencyFocus + 1)
                    'Third target
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 7).Value = _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, _campaign.TargColl(_campaign.ThirdTarget.TargetName, _campaign.Adedge) - 1)
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 8).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.ThirdTarget.TargetName, _campaign.Adedge) - 1, 1)
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 9).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.ThirdTarget.TargetName, _campaign.Adedge) - 1, _campaign.FrequencyFocus + 1)
                    'All adults
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 10).Value = _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, _campaign.TargColl(_campaign.AllAdults, _campaign.Adedge) - 1)
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 11).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.AllAdults, _campaign.Adedge) - 1, 1)
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 12).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.AllAdults, _campaign.Adedge) - 1, _campaign.FrequencyFocus + 1)
                Else
                    'There are NO actual spots meeting the conditions
                    For i As Integer = 1 To 5
                        'Print reach for this channel in 1+ to 5+
                        .Cells(_topRow + Val(GetVariable("Bookingtypes")), GetVariable("ActualNationalSumsCol") + 2 + i).Value = 0
                    Next

                    'Summary
                    'Main target
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 3).Value = 0
                    'Secondary target
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 4).Value = 0
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 5).Value = 0
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 6).Value = 0
                    'Third target
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 7).Value = 0
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 8).Value = 0
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 9).Value = 0
                    'All adults
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 10).Value = 0
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 11).Value = 0
                    .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 12).Value = 0

                End If

                For dp As Integer = 0 To _campaign.Dayparts.Count - 1
                    .Cells(GetVariable("ActualDaypartRow") - 1, GetVariable("ActualDaypartCol") + dp - 1).Value = _campaign.Dayparts(dp).Name
                Next
                For dp As Integer = 0 To _campaign.Dayparts.Count - 1
                    PrintDaypart(dp, Channel)
                Next

                PrintPosInBreak()
                PrintPosInBreakSecondayTargetTRP30(Channel)

            End With
        End Sub

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

        Friend Overrides Sub PrintCombinations()
            With _excel.Sheets(GetSheet())
                For Each c As Trinity.cCombination In _campaign.Combinations
                    If c.ShowAsOne Then
                        _btNumber = -1
                        _topRow = Val(GetVariable("ActualChannelSumsRow")) + ((_chanNumber + 1) * (Val(GetVariable("Bookingtypes")) + 2))
                        .Cells(_topRow, 1).Value = c.Name
                        .Cells(_topRow + Val(GetVariable("Bookingtypes")), 1).Value = c.Name
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
                        _allBTs = (From _cc As Trinity.cCombinationChannel In c.Relations Select _bt = _cc.Bookingtype).ToList
                        _campaign.CalculateSpots(UseFilters:=False, CalculateReach:=True, Bookingtypes:=_allBTs)
                        If _campaign.Adedge.getGroupCount > 0 Then
                            For _i As Integer = 1 To 5
                                .Cells(_topRow + Val(GetVariable("Bookingtypes")), GetVariable("ActualNationalSumsCol") + 2 + _i).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1, _i)
                            Next
                            'Summary
                            'Main target
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 3).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1, _campaign.FrequencyFocus + 1)
                            'Secondary target
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 4).Value = _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, _campaign.TargColl(_campaign.SecondaryTarget.TargetName, _campaign.Adedge) - 1)
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 5).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.SecondaryTarget.TargetName, _campaign.Adedge) - 1, 1)
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 6).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.SecondaryTarget.TargetName, _campaign.Adedge) - 1, _campaign.FrequencyFocus + 1)
                            'Third target
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 7).Value = _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, _campaign.TargColl(_campaign.ThirdTarget.TargetName, _campaign.Adedge) - 1)
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 8).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.ThirdTarget.TargetName, _campaign.Adedge) - 1, 1)
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 9).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.ThirdTarget.TargetName, _campaign.Adedge) - 1, _campaign.FrequencyFocus + 1)
                            'All adults
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 10).Value = _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, _campaign.TargColl(_campaign.AllAdults, _campaign.Adedge) - 1)
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 11).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.AllAdults, _campaign.Adedge) - 1, 1)
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 12).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.AllAdults, _campaign.Adedge) - 1, _campaign.FrequencyFocus + 1)
                        Else
                            For _i As Integer = 1 To 5
                                .Cells(_topRow + Val(GetVariable("Bookingtypes")), GetVariable("ActualNationalSumsCol") + 2 + _i).Value = 0
                            Next

                            'Summary
                            'Main target
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 3).Value = 0
                            'Secondary target
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 4).Value = 0
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 5).Value = 0
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 6).Value = 0
                            'Third target
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 7).Value = 0
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 8).Value = 0
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 9).Value = 0
                            'All adults
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 10).Value = 0
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 11).Value = 0
                            .Cells(GetVariable("ActualSummaryRow") + _chanNumber, GetVariable("ActualSummaryCol") + 12).Value = 0

                        End If
                        'Per daypart
                        For dp As Integer = 0 To _campaign.Dayparts.Count - 1
                            Dim combTRP As Double = 0
                            .Cells(GetVariable("ActualDaypartRow") + _chanNumber, GetVariable("ActualDaypartCol")).Value = c.Name
                            For Each cc As Trinity.cCombinationChannel In c.Relations
                                _campaign.CalculateSpots(UseFilters:=False, CalculateReach:=False, Bookingtype:=cc.Bookingtype, Daypart:=dp)
                                .Cells(GetVariable("ActualDaypartRow") + _chanNumber, GetVariable("ActualDaypartCol") + 1 + dp).Value += _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1)
                            Next
                            '.cells(Vars("ActualDaypartRow") + Chan, Vars("ActualDaypartCol") + 1 + dp).value = _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.UniColl(_campaign.MainTarget.Universe) - 1, _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1)
                        Next
                        PrintPosInBreak()
                        PrintPosInBreakSecondayTargetTRP30()
                    End If

                Next
            End With
        End Sub

        Friend Overrides Sub PrintCompensations(ByVal Week As cWeek)

        End Sub

        Friend Overrides Sub PrintCostDetails()

        End Sub

        Friend Overrides Sub PrintCostPerFilmcode(ByVal BookingType As cBookingType)

        End Sub


        Friend Overrides Sub PrintDaypart(ByVal Daypart As Integer, ByVal Channel As Trinity.cChannel)
            _campaign.CalculateSpots(CalculateReach:=False, UseFilters:=False, Bookingtypes:=_allBTs, Daypart:=Daypart)
            With _excel.Sheets(GetSheet())
                .Cells(GetVariable("ActualDaypartRow") + _chanNumber, GetVariable("ActualDaypartCol")).Value = Channel.ChannelName
                .Cells(GetVariable("ActualDaypartRow") + _chanNumber, GetVariable("ActualDaypartCol") + 1 + Daypart).Value = _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1)
            End With
        End Sub
         Public Sub PrintPosInBreakSecondayTargetTRP30(Optional byval channel As cChannel = Nothing)            
            If VariableExists("ActualPIBRowSecondary")
                _campaign.CalculateSpots(CalculateReach:=False, UseFilters:=False, Bookingtypes:=_allBTs)            
                Dim tmpRow As Integer = GetVariable("ActualPIBRowSecondary")
                Dim tmpCol As Integer = GetVariable("ActualPIBColSecondary")
                Dim _pib(5) As Single
                For Each _spot As Trinity.cActualSpot In From spot As Trinity.cActualSpot In _campaign.ActualSpots Select spot Where _allBTs.Contains(spot.Bookingtype)
                    Dim PIB As Integer
                    If _spot.PosInBreak = 1 Then
                        PIB = 1
                    ElseIf _spot.PosInBreak = 2 Then
                        PIB = 2
                    ElseIf _spot.PosInBreak = _spot.SpotsInBreak Then
                        PIB = 5
                    ElseIf _spot.PosInBreak = _spot.SpotsInBreak - 1 Then
                        PIB = 4
                    Else
                        PIB = 3
                    End If
                    _pib(PIB) += _campaign.Adedge.getUnitGroup(Connect.eUnits.uBrandTrp30, _spot.GroupIdx, 0, _campaign.TimeShift, _campaign.TargColl(_campaign.SecondaryTarget.TargetName, _campaign.Adedge) - 1)
                Next
                For PIB As Integer = 1 To 5
                    If channel IsNot Nothing
                        _excel.Sheets(GetSheet()).Cells(GetVariable("ActualPIBRowSecondary") + _chanNumber, GetVariable("ActualPIBColSecondary") - 1).Value =   channel.ChannelName
                    Else                    
                        If ShowCombinationsAsSingleRow
                            _excel.Sheets(GetSheet()).Cells(GetVariable("ActualPIBRowSecondary") + _chanNumber, GetVariable("ActualPIBColSecondary") - 1).Value = _allBTs.Item(0).Combination.Name
                        End If
                    End If
                    _excel.Sheets(GetSheet()).Cells(GetVariable("ActualPIBRowSecondary") + _chanNumber, GetVariable("ActualPIBColSecondary") + PIB).Value = _pib(PIB)
                Next
            End If
        End Sub
        Friend Overrides Sub PrintPosInBreak()
            _campaign.CalculateSpots(CalculateReach:=False, UseFilters:=False, Bookingtypes:=_allBTs)
            Dim _pib(5) As Single
            For Each _spot As Trinity.cActualSpot In From spot As Trinity.cActualSpot In _campaign.ActualSpots Select spot Where _allBTs.Contains(spot.Bookingtype)
                Dim PIB As Integer
                If _spot.PosInBreak = 1 Then
                    PIB = 1
                ElseIf _spot.PosInBreak = 2 Then
                    PIB = 2
                ElseIf _spot.PosInBreak = _spot.SpotsInBreak Then
                    PIB = 5
                ElseIf _spot.PosInBreak = _spot.SpotsInBreak - 1 Then
                    PIB = 4
                Else
                    PIB = 3
                End If
                _pib(PIB) += _campaign.Adedge.getUnitGroup(Connect.eUnits.uTRP, _spot.GroupIdx, 0, _campaign.TimeShift, _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1)
            Next
            For PIB As Integer = 1 To 5
                _excel.Sheets(GetSheet()).Cells(GetVariable("ActualPIBRow") + _chanNumber, GetVariable("ActualPIBCol") + PIB).Value = _pib(PIB)
            Next
        End Sub

        Friend Overrides Sub PrintFilm(ByVal Film As cFilm, ByVal Week As cWeek)
            With _excel.Sheets(GetSheet())
                _campaign.CalculateSpots(CalculateReach:=False, UseFilters:=False, Film:=Film, OnlyWeek:=Week.Name, Bookingtype:=Week.Bookingtype)
                .Cells(GetVariable("ActualFilmcodeRow") + _filmNumber, GetVariable("ActualFilmcodeCol") + 0).Value = Film.Name
                .Cells(GetVariable("ActualFilmcodeRow") + _filmNumber, GetVariable("ActualFilmcodeCol") + 1).Value = Film.FilmLength
                If .Cells(GetVariable("ActualFilmcodeRow") + _filmNumber, GetVariable("ActualFilmcodeCol") + 3 + _chanNumber).Value Is Nothing OrElse .Cells(GetVariable("ActualFilmcodeRow") + _filmNumber, GetVariable("ActualFilmcodeCol") + 3 + _chanNumber).Value.ToString = "" Then
                    .Cells(GetVariable("ActualFilmcodeRow") + _filmNumber, GetVariable("ActualFilmcodeCol") + 3 + _chanNumber).Value = 0
                End If
                .Cells(GetVariable("ActualFilmcodeRow") + _filmNumber, GetVariable("ActualFilmcodeCol") + 3 + _chanNumber).Value += _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1)
                .Cells(GetVariable("ActualFilmcodeRow") + _filmNumber, GetVariable("ActualFilmcodeCol") + GetVariable("Channels") + 3 + _chanNumber).Value += _campaign.Adedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smGroup, , _campaign.TimeShift, _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1)
                .Cells(GetVariable("ActualFilmcodeRow") + _filmNumber, GetVariable("ActualFilmcodeCol") + GetVariable("Channels") * 2 + 3 + _weekNumber).Value += _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1)
            End With
        End Sub

        Friend Overrides Sub PrintFilmList()

        End Sub

        Private Sub PrintReachBuildup(ByVal TargetName As String, ByVal UniverseName As String, ByVal Column As Integer)
            Dim i As Integer = 0
            Dim r As Integer = GetVariable("ActualReachBuildupRow")
            Dim c As Integer = GetVariable("ActualReachBuildupCol")
            Dim _next10 As Integer = 10
            Dim _trpSum As Single = 0

            For _s As Integer = 0 To _campaign.Adedge.getGroupCount - 1
                _trpSum = _trpSum + _campaign.Adedge.getUnit(Connect.eUnits.uTRP, _s, , _campaign.TimeShift, _campaign.TargColl(TargetName, _campaign.Adedge) - 1)
                If _trpSum >= _next10 Then
                    With _excel.Sheets(GetSheet())
                        .Cells(r, c + Column).Value = _campaign.Adedge.getRF(_s, , _campaign.TimeShift, _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1, 1) / 100
                        .Cells(r, c + 3 + Column).Value = _campaign.Adedge.getRF(_s, , _campaign.TimeShift, _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1, _campaign.FrequencyFocus + 1) / 100
                    End With
                    r = r + 1
                    _next10 = _next10 + 10
                End If
            Next
        End Sub


        Friend Overrides Sub PrintReach()
            _campaign.CalculateSpots()
            If _campaign.Adedge.getGroupCount > 0 Then
                With _excel.Sheets(GetSheet())
                    For i As Integer = 1 To 10
                        .Cells(GetVariable("ActualReachRow") + i - 1, GetVariable("ActualReachCol") + 0).Value = _campaign.GetRF(i, _campaign.MainTarget.TargetName) '.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, 0, _campaign.UniColl(_campaign.MainTarget.Universe) - 1, _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1, i)
                        .Cells(GetVariable("ActualReachRow") + i - 1, GetVariable("ActualReachCol") + 1).Value = _campaign.GetRF(i, _campaign.SecondaryTarget.TargetName) '_campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, 0, _campaign.UniColl(_campaign.SecondaryTarget.Universe) - 1, _campaign.TargColl(_campaign.SecondaryTarget.TargetName, _campaign.Adedge) - 1, i)
                        .Cells(GetVariable("ActualReachRow") + i - 1, GetVariable("ActualReachCol") + 2).Value = _campaign.GetRF(i, _campaign.ThirdTarget.TargetName) '_campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, 0, _campaign.UniColl(_campaign.ThirdTarget.Universe) - 1, _campaign.TargColl(_campaign.ThirdTarget.TargetName, _campaign.Adedge) - 1, i)
                        .Cells(GetVariable("ActualReachRow") + i - 1, GetVariable("ActualReachCol") + 3).Value = _campaign.GetRF(i, _campaign.AllAdults) '_campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, 0, _campaign.UniColl(_campaign.MainTarget.Universe) - 1, _campaign.TargColl(_campaign.AllAdults, _campaign.Adedge) - 1, i)
                    Next

                    .Cells(GetVariable("ActualSummaryRow") + GetVariable("Channels"), GetVariable("ActualSummaryCol") + 3).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1, _campaign.FrequencyFocus + 1)
                    .Cells(GetVariable("ActualSummaryRow") + GetVariable("Channels"), GetVariable("ActualSummaryCol") + 6).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.SecondaryTarget.TargetName, _campaign.Adedge) - 1, _campaign.FrequencyFocus + 1)
                    .Cells(GetVariable("ActualSummaryRow") + GetVariable("Channels"), GetVariable("ActualSummaryCol") + 9).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.ThirdTarget.TargetName, _campaign.Adedge) - 1, _campaign.FrequencyFocus + 1)
                    .Cells(GetVariable("ActualSummaryRow") + GetVariable("Channels"), GetVariable("ActualSummaryCol") + 12).Value = _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _campaign.TargColl(_campaign.AllAdults, _campaign.Adedge) - 1, _campaign.FrequencyFocus + 1)
                End With
            End If
            'Print reach build up in Main Target
            PrintReachBuildup(_campaign.MainTarget.TargetName, _campaign.MainTarget.Universe, 0)
            'Print reach build up in Secondary target
            PrintReachBuildup(_campaign.SecondaryTarget.TargetName, _campaign.MainTarget.Universe, 1)
        End Sub

        Public Overrides Sub InitializeAdvantedge()
            MyBase.InitializeAdvantedge()
            _campaign.Adedge.clearTargetSelection()
            Trinity.Helper.AddTargetsToAdedge(_campaign.Adedge, True)
            _campaign.Adedge.clearBrandFilter() 'Tillagd av Kristian så att inte filterna växer för varje körning
            _campaign.Adedge.setBrandFilmCode(_campaign.AreaLog, "Ö")
            '_campaign.Adedge.setTargetMnemonic(Trinity.Helper.CreateTargetString(True))
            _campaign.Adedge.registerCallback(Me)
            _campaign.Adedge.Run(False, False, 10)
            _campaign.Adedge.unregisterCallback()
        End Sub

        ''' <summary>
        ''' Variable to show wether weekly totals for all channels/bookingtypes has been printed and does not need to be printed/calculated again
        ''' </summary>
        Private _hasPrintedWeekTotals As Boolean = False
        Friend Overrides Sub PrintWeek(ByVal Week As cWeek)
            With _excel.Sheets(GetSheet())

                _campaign.CalculateSpots(UseFilters:=False, CalculateReach:=True, OnlyWeek:=Week.Name, Bookingtype:=Week.Bookingtype)
                If VariableExists("ActualSummaryMode") AndAlso GetVariable("ActualSummaryMode") = "TRP30" Then
                    .Cells(_topRow + _btNumber, GetVariable("ActualNationalWeekCol") + _weekNumber).Value = _
                        Week.Bookingtype.ActualTRP30(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget, Week.Name)
                Else
                    .Cells(_topRow + _btNumber, GetVariable("ActualNationalWeekCol") + _weekNumber).Value = _
                        _campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , _campaign.TimeShift, _
                        _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1)
                End If

                'Print totals for all channels/bookingtypes for this week
                If True OrElse Not _hasPrintedWeekTotals Then
                    'Print week name
                    .Cells(_topRow - 1, GetVariable("ActualNationalWeekCol") + _weekNumber).Value = Week.Name

                    'Print reach for week
                    _campaign.CalculateSpots(UseFilters:=False, CalculateReach:=True, OnlyWeek:=Week.Name)
                    If _campaign.Adedge.getGroupCount > 0 Then
                        .Cells(GetVariable("ActualChannelSumsRow") + GetVariable("Channels") * (GetVariable("Bookingtypes") + 2), GetVariable("ActualNationalWeekCol") + _weekNumber).Value = _
                        _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _
                        _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1, 1)
                    Else
                        .Cells(GetVariable("ActualChannelSumsRow") + GetVariable("Channels") * (GetVariable("Bookingtypes") + 2), GetVariable("ActualNationalWeekCol") + _weekNumber).Value = 0
                    End If
                    'Print accumulated reach for week
                    _campaign.CalculateSpots(CalculateReach:=True, UseFilters:=False, ToDate:=Date.FromOADate(Week.EndDate))
                    If _campaign.Adedge.getGroupCount > 0 Then
                        .Cells(GetVariable("ActualChannelSumsRow") + GetVariable("Channels") * (GetVariable("Bookingtypes") + 2) + 1, GetVariable("ActualNationalWeekCol") + _weekNumber).Value = _
                        _campaign.Adedge.getRF(_campaign.Adedge.getGroupCount - 1, , _campaign.TimeShift, _
                        _campaign.TargColl(_campaign.MainTarget.TargetName, _campaign.Adedge) - 1, 1)
                    End If
                End If
                _filmNumber = 0
                For Each _film As Trinity.cFilm In Week.Films
                    PrintFilm(_film, Week)
                    _filmNumber += 1
                Next
            End With
        End Sub

        Public Sub callback(ByVal p As Integer) Implements Connect.ICallBack.callback
            SetProgress(ProgressValue:=p)
        End Sub

        Sub AfterRun() Handles Me.DoAfterRun
            _campaign.CalculateSpots(True) ' Denna resettar allt, om den tas bort fuckar reachen ur.
        End Sub

        Friend Overloads Overrides Sub PrintDaypart(Daypart As Integer, Week As cWeek)

        End Sub
    End Class
End Namespace