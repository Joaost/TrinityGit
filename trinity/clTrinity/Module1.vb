Imports Microsoft.VisualBasic.Compatibility
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D


'*******************************************

Imports System.Runtime.CompilerServices

Public Class CampaignEssentials
    Private _ID As Integer
    Public campaignid As String
    Public _name As String
    Public Year As Integer
    Public Month As Integer
    Public client As Integer
    Public product As Integer
    Public planner As String
    Public buyer As String
    Public lastopened As Date
    Public lastsaved As Date
    Public lockedby As String
    Public originalLocation As String
    Public originalfilechangeddate As String
    Public status As String
    Public startdate As Date
    Public enddate As Date
    Public contractid As Long = 0

    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public Property id() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property
End Class

Module modTrinity
    Public TrinitySettings As Trinity.cSettings
    Public DBReader As Trinity.cDBReader
    Public Matrix As Object

    'Public DBConn As System.Data.Odbc.OdbcConnection
    Public WithEvents Campaign As Trinity.cKampanj
    Public LoggingIsOn As Boolean = False

    Public Saved As Boolean
    Public LongName As New Dictionary(Of String, String)
    Public LongBT As New Dictionary(Of String, String)
    Public DataPath As String

    Public BookingFilter As New Trinity.cFilter
    Public GeneralFilter As New Trinity.cFilter
    Public AdedgeUserTarget As Dictionary(Of String, List(Of String))
    Public AdedgeAdvertisers As SortedList(Of String, String)
    Public AdedgeProducts As SortedList(Of String, String)

    Public DefaultSecondUniverse As Object = Nothing

    'Variable to hold the replacement targets picked when targets could not be found
    Public PickedTargetsList As New Dictionary(Of String, Object)

    Public Enum MatrixVersionEnum
        NotInstalled
        Matrix30
        Matrix40
    End Enum

    Public MatrixVersion As MatrixVersionEnum = MatrixVersionEnum.NotInstalled

    Public AutoSaveName As String

        Public Enum ActiveTargetEnum
            eMainTarget = 0
            eSecondaryTarget = 1
            eThirdTarget = 2
            eBuyingTarget = 3
            eAllAdults = 4
        End Enum

        Public ActiveTarget As ActiveTargetEnum

    Private Structure Break
        Public AirDate As Date
        Public MaM As Integer
        Public Channel As String
        Public ProgramInDB As String
        Public ProgramInAdedge As String
        Public Rating As Decimal
        Public NetCost As Decimal

        Public Function NetCPP() As Decimal
            If Rating <> 0 Then
                Return NetCost / Rating
            End If
            Return 0
        End Function

    End Structure

    Function CreateGUID() As String
        CreateGUID = Guid.NewGuid.ToString
    End Function

    Public Function IsIDE() As Boolean

        On Error GoTo ErrHandler
        'because debug statements are ignored when
        'the app is compiled, the next statment will
        'never be executed in the EXE.
        '        Debug.Print(1 / 0)
        IsIDE = False
        Exit Function
ErrHandler:
        'If we get an error then we are
        'running in IDE / Debug mode
        IsIDE = True
    End Function

    Private Sub Campaign_ReadNewSpot(ByVal SpotNr As Integer, ByVal SpotCount As Integer) Handles Campaign.ReadNewSpot
        frmMain.lblStatus.Text = "Reading spots..." & Int((SpotNr / SpotCount) * 100) & "%"
        Windows.Forms.Application.DoEvents()
    End Sub

    Sub ExportNewPostAnalysis(ByVal Excel As CultureSafeExcel.Application, ByVal singleRowCombinations As Boolean, Optional ByVal Extended As Boolean = False, Optional ByVal Competitors() As String = Nothing, Optional ByVal ProgressWindow As frmProgress = Nothing)

        Dim x As Integer = 0
        Dim i As Integer
        Dim AllKeysNotFound As Boolean = False
        Dim p As Integer
        Dim currentSpot As Integer = 0
        Dim currentChannel As String
        Dim currentBT As String
        Dim currentWeek As String
        Dim Spot As Trinity.cActualSpot

        On Error GoTo ErrHandle

        Dim Vars As New Dictionary(Of String, Object)

        If Not ProgressWindow Is Nothing Then
            ProgressWindow.Status = "Creating post-campaign analysis..."
            ProgressWindow.Progress = 0
            p = 0
        End If

        With Excel.Sheets("Setup")
            Dim r As Integer = 3
            While Not .cells(r, 2).Value Is Nothing
                Vars.Add(.cells(r, 2).value, .cells(r, 3).value)
                r += 1
            End While
        End With

        If Campaign.Channels(1).BookingTypes(1).Weeks.Count > Vars("Weeks") Then
            Windows.Forms.MessageBox.Show("The template you are trying to use is adjusted for " & Vars("Weeks") & " weeks" & vbCrLf & "and this campaign has " & Campaign.Channels(1).BookingTypes(1).Weeks.Count & " weeks, wich may cause errors in the plan.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        End If
        If Not Vars.ContainsKey("ActualNetValueCol") Then
            Windows.Forms.MessageBox.Show("The template you are using does not have support for printing" & vbCrLf & "the actual campaign value." & vbCrLf & "Contact the system administrator if you want to use this feature.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        If Campaign.Channels(1).BookingTypes(1).Weeks(1).Films.Count > Vars("Filmcodes") Then
            Windows.Forms.MessageBox.Show("The template you are using does not have support for for than " & Vars("Filmcodes") & ". This may cause a problem.")
        End If
        With Excel.Sheets(Vars("ActualSheet").ToString)
            .range("B3").Value = Campaign.Name
            .range("B4").value = Campaign.Client
            .range("B5").value = Campaign.Product
            Dim StartWeek As Integer = DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
            Dim EndWeek As Integer = DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
            Dim PeriodStr As String = StartWeek
            If EndWeek > StartWeek Then PeriodStr = PeriodStr & " - " & EndWeek
            PeriodStr = PeriodStr & ", " & Format(Date.FromOADate(Campaign.StartDate), "yyyy")
            .Range("B6").value = PeriodStr
            .Range("B7").value = StartWeek
            .Range("B8").value = EndWeek
            .Range("B9").value = Campaign.StartDate
            .Range("B10").value = Campaign.EndDate
            .Range("E3").value = Campaign.Buyer
            .Range("G3").value = Campaign.BuyerEmail
            .Range("I3").value = Campaign.BuyerPhone
            .Range("E4").value = Campaign.Planner
            .Range("G4").value = Campaign.PlannerEmail
            .Range("I4").value = Campaign.PlannerPhone

            If Campaign.Adedge.validate > 0 Then
                Campaign.Adedge.setPeriod("-1d")
                Campaign.Adedge.Run()
            Else
                Campaign.Adedge.Run()
            End If


            .Range("D6").Numberformat = "@"
            .Range("D6").FORMULA = Campaign.MainTarget.TargetNameNice
            .Range("E6").value = Campaign.MainTarget.UniSizeTot * 1000
            .Range("F6").value = Campaign.MainTarget.UniSizeSec * 1000

            .Range("D7").NUMBERFORMAT = "@"
            .Range("D7").FORMULA = Campaign.SecondaryTarget.TargetNameNice
            .Range("E7").value = Campaign.SecondaryTarget.UniSizeTot * 1000
            .Range("F7").value = Campaign.SecondaryTarget.UniSizeSec * 1000

            .Range("D8").NUMBERFORMAT = "@"
            .Range("D8").FORMULA = Campaign.ThirdTarget.TargetNameNice
            .Range("E8").value = Campaign.ThirdTarget.UniSizeTot * 1000
            .Range("F8").value = Campaign.ThirdTarget.UniSizeSec * 1000

            .Range("E9").value = Campaign.Adedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , , Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge) - 1) * 1000
            'If Campaign.UniColl(Campaign.MainTarget.SecondUniverse) >= 0 Then
            ' .Range("F9").Value = Campaign.Adedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , Campaign.UniColl(Campaign.MainTarget.SecondUniverse) - 1, Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge) - 1) * 1000
            'Else
            .Range("F9").Value = 0
            'End If
            Campaign.Adedge.clearTargetSelection()
            Trinity.Helper.AddTargetsToAdedge(Campaign.Adedge, True)
            Campaign.Adedge.clearBrandFilter() 'Tillagd av Kristian så att inte filterna växer för varje körning
            Campaign.Adedge.setBrandFilmCode(Campaign.AreaLog, "Ö")
            'Campaign.Adedge.setTargetMnemonic(Trinity.Helper.CreateTargetString(True))
            If Not ProgressWindow Is Nothing Then
                Campaign.Adedge.registerCallback(New ConnectCallback(ProgressWindow))
            End If
            Campaign.Adedge.Run(False, False, 10)
            Campaign.Adedge.unregisterCallback()

            If Campaign.Adedge.getGroupCount > 0 Then
                For i = 1 To 10
                    .Cells(Vars("ActualReachRow") + i - 1, Vars("ActualReachCol") + 0).Value = Campaign.GetRF(i, Campaign.MainTarget.TargetName) '.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, 0, Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, i)
                    .Cells(Vars("ActualReachRow") + i - 1, Vars("ActualReachCol") + 1).Value = Campaign.GetRF(i, Campaign.SecondaryTarget.TargetName) 'Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, 0, Campaign.TimeShift, Campaign.TargColl(Campaign.SecondaryTarget.TargetName, Campaign.Adedge) - 1, i)
                    .Cells(Vars("ActualReachRow") + i - 1, Vars("ActualReachCol") + 2).Value = Campaign.GetRF(i, Campaign.ThirdTarget.TargetName) 'Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, 0, Campaign.TimeShift, Campaign.TargColl(Campaign.ThirdTarget.TargetName, Campaign.Adedge) - 1, i)
                    .Cells(Vars("ActualReachRow") + i - 1, Vars("ActualReachCol") + 3).Value = Campaign.GetRF(i, Campaign.AllAdults) 'Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, 0, Campaign.TimeShift, Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge) - 1, i)
                Next

                .Cells(Vars("ActualSummaryRow") + Vars("Channels"), Vars("ActualSummaryCol") + 3).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1)
                .Cells(Vars("ActualSummaryRow") + Vars("Channels"), Vars("ActualSummaryCol") + 6).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.SecondaryTarget.TargetName, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1)
                .Cells(Vars("ActualSummaryRow") + Vars("Channels"), Vars("ActualSummaryCol") + 9).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.ThirdTarget.TargetName, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1)
                .Cells(Vars("ActualSummaryRow") + Vars("Channels"), Vars("ActualSummaryCol") + 12).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1)
            End If

            i = 0
            Dim r As Integer = Vars("ActualReachBuildupRow")
            Dim c As Integer = Vars("ActualReachBuildupCol")
            Dim next10 As Integer = 10
            Dim TRPSum As Single = 0

            For s As Integer = 0 To Campaign.ActualSpots.Count - 1
                currentSpot = s
                TRPSum = TRPSum + Campaign.Adedge.getUnit(Connect.eUnits.uTRP, s, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)
                If TRPSum >= next10 Then
                    .Cells(r, c).Value = Campaign.Adedge.getRF(s, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, 1) / 100
                    .Cells(r, c + 3).Value = Campaign.Adedge.getRF(s, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1) / 100
                    r = r + 1
                    next10 = next10 + 10
                End If
            Next

            r = Vars("ActualReachBuildupRow")
            c = Vars("ActualReachBuildupCol")
            next10 = 10
            TRPSum = 0
            For s As Integer = 0 To Campaign.ActualSpots.Count - 1
                TRPSum = TRPSum + Campaign.Adedge.getUnit(Connect.eUnits.uTRP, s, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)
                If TRPSum >= next10 Then
                    .Cells(r, c + 1).Value = Campaign.Adedge.getRF(s, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, 1) / 100
                    .Cells(r, c + 4).Value = Campaign.Adedge.getRF(s, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1) / 100
                    r = r + 1
                    next10 = next10 + 10
                End If
            Next

            'Debug.Print(Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, 0, Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, 1))

            Dim Chan As Integer = -1
            For Each TmpChan As Trinity.cChannel In Campaign.Channels
                currentChannel = TmpChan.ChannelName
                'Debug.Print(TmpChan.ChannelName & " - " & Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, 0, Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, 1))
                Dim UseIt As Boolean = False
                Dim BT As Integer = -1
                Dim TopRow As Integer = Val(Vars("ActualChannelSumsRow")) + ((Chan + 1) * (Val(Vars("Bookingtypes")) + 2))
                Dim AllBTs As New List(Of Trinity.cBookingType)

                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                    currentBT = TmpBT.Name
                    If TmpBT.BookIt AndAlso Not (singleRowCombinations AndAlso Not TmpBT.ShowMe) Then
                        UseIt = True
                        AllBTs.Add(TmpBT)
                    End If
                Next
                If Not ProgressWindow Is Nothing Then
                    p += 1
                    ProgressWindow.Status = "Post-campaign analysing... " & TmpChan.ChannelName
                    ProgressWindow.Progress = (p / Campaign.Channels.Count) * 100
                End If
                If UseIt Then
                    Chan = Chan + 1
                    .Cells(TopRow, 1).Value = TmpChan.ChannelName
                    .Cells(TopRow + Val(Vars("Bookingtypes")), 1).Value = TmpChan.Shortname
                    i = 0
                    For Each TmpWeek As Trinity.cWeek In TmpChan.BookingTypes(1).Weeks
                        currentWeek = TmpWeek.Name
                        .Cells(TopRow - 1, Vars("ActualNationalWeekCol") + i).Value = TmpWeek.Name
                        i += 1
                    Next
                    Campaign.CalculateSpots(UseFilters:=False, CalculateReach:=True, Bookingtypes:=AllBTs)
                    If Campaign.Adedge.getGroupCount > 0 Then
                        For i = 1 To 5
                            .Cells(TopRow + Val(Vars("Bookingtypes")), Vars("ActualNationalSumsCol") + 2 + i).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, i)
                        Next

                        'Summary
                        'Main target
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 3).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1)
                        'Secondary target
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 4).Value = Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.SecondaryTarget.TargetName, Campaign.Adedge) - 1)
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 5).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.SecondaryTarget.TargetName, Campaign.Adedge) - 1, 1)
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 6).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.SecondaryTarget.TargetName, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1)
                        'Third target
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 7).Value = Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.ThirdTarget.TargetName, Campaign.Adedge) - 1)
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 8).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.ThirdTarget.TargetName, Campaign.Adedge) - 1, 1)
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 9).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.ThirdTarget.TargetName, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1)
                        'All adults
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 10).Value = Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge) - 1)
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 11).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge) - 1, 1)
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 12).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1)
                    Else
                        For i = 1 To 5
                            .Cells(TopRow + Val(Vars("Bookingtypes")), Vars("ActualNationalSumsCol") + 2 + i).Value = 0
                        Next

                        'Summary
                        'Main target
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 3).Value = 0
                        'Secondary target
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 4).Value = 0
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 5).Value = 0
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 6).Value = 0
                        'Third target
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 7).Value = 0
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 8).Value = 0
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 9).Value = 0
                        'All adults
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 10).Value = 0
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 11).Value = 0
                        .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 12).Value = 0

                    End If
                    'Per daypart
                    For dp As Integer = 0 To Campaign.Dayparts.Count - 1
                        Campaign.CalculateSpots(CalculateReach:=False, UseFilters:=False, Bookingtypes:=AllBTs, Daypart:=dp)
                        .Cells(Vars("ActualDaypartRow") - 1, Vars("ActualDaypartCol")).Value = Campaign.Dayparts(dp).Name
                        .Cells(Vars("ActualDaypartRow") + Chan, Vars("ActualDaypartCol") + 1 + dp).Value = Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)
                    Next

                    'Position in break (detailed)
                    Campaign.CalculateSpots(CalculateReach:=False, UseFilters:=False, Bookingtypes:=AllBTs)
                    For Each TmpSpot As Trinity.cActualSpot In Campaign.ActualSpots
                        Spot = TmpSpot
                        If AllBTs.Contains(TmpSpot.Bookingtype) Then
                            Dim PIB As Integer
                            If TmpSpot.PosInBreak = 1 Then
                                PIB = 1
                            ElseIf TmpSpot.PosInBreak = 2 Then
                                PIB = 2
                            ElseIf TmpSpot.PosInBreak = TmpSpot.SpotsInBreak Then
                                PIB = 5
                            ElseIf TmpSpot.PosInBreak = TmpSpot.SpotsInBreak - 1 Then
                                PIB = 4
                            Else
                                PIB = 3
                            End If
                            .Cells(Vars("ActualPIBRow") + Chan, Vars("ActualPIBCol") + PIB).Value += Campaign.Adedge.getUnitGroup(Connect.eUnits.uTRP, TmpSpot.GroupIdx, 0, Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)
                        End If
                    Next

                    For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                        currentBT = TmpBT.Name
                        If TmpBT.BookIt AndAlso Not (singleRowCombinations AndAlso Not TmpBT.ShowMe) Then
                            BT = BT + 1
                            Campaign.CalculateSpots(UseFilters:=False, CalculateReach:=True, Bookingtype:=TmpBT)
                            .Cells(TopRow + BT, 2).Value = TmpBT.Name
                            .Cells(TopRow + BT, Vars("ActualSpotsCol")).Value = Campaign.Adedge.getGroupCount
                            .Cells(TopRow + BT, Vars("ActualChannelSumsCol") + 0).Value = TmpBT.BuyingTarget.TargetName

                            If Vars.ContainsKey("ActualNetValueCol") Then
                                'Print Campaign actual value
                                .Cells(TopRow + BT, Vars("ActualNetValueCol") + 0).Value = TmpBT.ActualNetValue
                            End If

                            If Campaign.Adedge.getGroupCount > 0 Then
                                .Cells(TopRow + BT, Vars("ActualChannelSumsCol") + 1).Value = Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, TmpBT.BuyingTarget.Target))
                            Else
                                .Cells(TopRow + BT, Vars("ActualChannelSumsCol") + 1).Value = 0
                            End If
                            If Campaign.Adedge.getGroupCount = 0 Then
                                .Cells(TopRow + BT, Vars("ActualChannelSumsCol") + 2).Value = 0
                            Else
                                .Cells(TopRow + BT, Vars("ActualChannelSumsCol") + 2).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, TmpBT.BuyingTarget.Target), 1)
                            End If
                            For i = 1 To 5
                                If Campaign.Adedge.getGroupCount = 0 Then
                                    .Cells(TopRow + BT, Vars("ActualNationalSumsCol") + 2 + i).Value = 0
                                Else
                                    .Cells(TopRow + BT, Vars("ActualNationalSumsCol") + 2 + i).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, Campaign.MainTarget), i)
                                End If
                            Next
                            Campaign.CalculateSpots(UseFilters:=False, CalculateReach:=False, Bookingtype:=TmpBT, Daypart:=1)
                            .Cells(TopRow + BT, Vars("ActualNationalSumsCol") + 0).Value = Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)
                            Campaign.CalculateSpots(UseFilters:=False, CalculateReach:=False, Bookingtype:=TmpBT, PosInBreak:=1)
                            .Cells(TopRow + BT, Vars("ActualNationalSumsCol") + 1).Value = Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)
                            Campaign.CalculateSpots(UseFilters:=False, CalculateReach:=False, Bookingtype:=TmpBT, PosInBreak:=3)
                            .Cells(TopRow + BT, Vars("ActualNationalSumsCol") + 1).Value = .Cells(TopRow + BT, Vars("ActualNationalSumsCol") + 1).Value + Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)

                            Dim w As Integer = 0
                            For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                                currentWeek = TmpWeek.Name
                                Campaign.CalculateSpots(UseFilters:=False, CalculateReach:=True, OnlyWeek:=TmpWeek.Name, Bookingtype:=TmpBT)
                                If Vars.ContainsKey("ActualSummaryMode") AndAlso Vars("ActualSummaryMode") = "TRP30" Then
                                    .Cells(TopRow + BT, Vars("ActualNationalWeekCol") + w).Value = TmpBT.ActualTRP30(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget, TmpWeek.Name)
                                Else
                                    .Cells(TopRow + BT, Vars("ActualNationalWeekCol") + w).Value = _
                                    Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, _
                                    Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)
                                End If


                                'Reach
                                Campaign.CalculateSpots(UseFilters:=False, CalculateReach:=True, OnlyWeek:=TmpWeek.Name)
                                If Campaign.Adedge.getGroupCount > 0 Then
                                    .Cells(Vars("ActualChannelSumsRow") + Vars("Channels") * (Vars("Bookingtypes") + 2), Vars("ActualNationalWeekCol") + w).Value = _
                                    Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, _
                                    Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, 1)
                                Else
                                    .Cells(Vars("ActualChannelSumsRow") + Vars("Channels") * (Vars("Bookingtypes") + 2), Vars("ActualNationalWeekCol") + w).Value = 0
                                End If
                                Campaign.CalculateSpots(CalculateReach:=True, UseFilters:=False, ToDate:=Date.FromOADate(TmpWeek.EndDate))
                                If Campaign.Adedge.getGroupCount > 0 Then
                                    .Cells(Vars("ActualChannelSumsRow") + Vars("Channels") * (Vars("Bookingtypes") + 2) + 1, Vars("ActualNationalWeekCol") + w).Value = _
                                    Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, _
                                    Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, 1)
                                End If

                                w += 1

                                Dim f As Integer = 0
                                For Each TmpFilm As Trinity.cFilm In TmpWeek.Films
                                    Campaign.CalculateSpots(CalculateReach:=False, UseFilters:=False, Film:=TmpFilm, OnlyWeek:=TmpWeek.Name, Bookingtype:=TmpBT)
                                    .Cells(Vars("ActualFilmcodeRow") + f, Vars("ActualFilmcodeCol") + 0).Value = TmpFilm.Name
                                    .Cells(Vars("ActualFilmcodeRow") + f, Vars("ActualFilmcodeCol") + 1).Value = TmpFilm.FilmLength
                                    If .Cells(Vars("ActualFilmcodeRow") + f, Vars("ActualFilmcodeCol") + 3 + Chan).Value Is Nothing OrElse .Cells(Vars("ActualFilmcodeRow") + f, Vars("ActualFilmcodeCol") + 3 + Chan).Value.ToString = "" Then
                                        .Cells(Vars("ActualFilmcodeRow") + f, Vars("ActualFilmcodeCol") + 3 + Chan).Value = 0
                                    End If
                                    .Cells(Vars("ActualFilmcodeRow") + f, Vars("ActualFilmcodeCol") + 3 + Chan).Value += Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)
                                    .Cells(Vars("ActualFilmcodeRow") + f, Vars("ActualFilmcodeCol") + Vars("Channels") + 3 + Chan).Value += Campaign.Adedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)
                                    f += 1
                                Next
                            Next
                        End If
                    Next
                End If
            Next

            'if we have combinations
            If singleRowCombinations Then
                For Each comb As Trinity.cCombination In Campaign.Combinations
                    If comb.ShowAsOne Then
                        Dim BTList As New List(Of Trinity.cBookingType)
                        For Each cc As Trinity.cCombinationChannel In comb.Relations
                            BTList.Add(cc.Bookingtype)
                        Next
                        Dim BT As Integer = -1
                        Dim TopRow As Integer = Val(Vars("ActualChannelSumsRow")) + ((Chan + 1) * (Val(Vars("Bookingtypes")) + 2))
                        Chan = Chan + 1
                        .Cells(TopRow, 1).Value = comb.Name
                        .Cells(TopRow + Val(Vars("Bookingtypes")), 1).Value = comb.Name
                        i = 0
                        For Each TmpWeek As Trinity.cWeek In comb.Relations(1).Bookingtype.Weeks
                            .Cells(TopRow - 1, Vars("ActualNationalWeekCol") + i).Value = TmpWeek.Name
                            i += 1
                        Next
                        Dim strChan As String = ""
                        Campaign.CalculateSpots(UseFilters:=False, CalculateReach:=True, Bookingtypes:=BTList)
                        If Campaign.Adedge.getGroupCount > 0 Then
                            For i = 1 To 5
                                .Cells(TopRow + Val(Vars("Bookingtypes")), Vars("ActualNationalSumsCol") + 2 + i).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, i)
                            Next
                            'Summary
                            'Main target
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 3).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1)
                            'Secondary target
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 4).Value = Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.SecondaryTarget.TargetName, Campaign.Adedge) - 1)
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 5).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.SecondaryTarget.TargetName, Campaign.Adedge) - 1, 1)
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 6).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.SecondaryTarget.TargetName, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1)
                            'Third target
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 7).Value = Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.ThirdTarget.TargetName, Campaign.Adedge) - 1)
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 8).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.ThirdTarget.TargetName, Campaign.Adedge) - 1, 1)
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 9).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.ThirdTarget.TargetName, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1)
                            'All adults
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 10).Value = Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge) - 1)
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 11).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge) - 1, 1)
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 12).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1)
                        Else
                            For i = 1 To 5
                                .Cells(TopRow + Val(Vars("Bookingtypes")), Vars("ActualNationalSumsCol") + 2 + i).Value = 0
                            Next

                            'Summary
                            'Main target
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 3).Value = 0
                            'Secondary target
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 4).Value = 0
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 5).Value = 0
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 6).Value = 0
                            'Third target
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 7).Value = 0
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 8).Value = 0
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 9).Value = 0
                            'All adults
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 10).Value = 0
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 11).Value = 0
                            .Cells(Vars("ActualSummaryRow") + Chan, Vars("ActualSummaryCol") + 12).Value = 0

                        End If
                        'Per daypart
                        For dp As Integer = 0 To Campaign.Dayparts.Count - 1
                            Dim combTRP As Double = 0
                            For Each cc As Trinity.cCombinationChannel In comb.Relations
                                Campaign.CalculateSpots(UseFilters:=False, CalculateReach:=False, Bookingtype:=cc.Bookingtype, Daypart:=dp)

                                .Cells(Vars("ActualDaypartRow") + Chan, Vars("ActualDaypartCol") + 1 + dp).Value += Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)
                            Next
                            '.cells(Vars("ActualDaypartRow") + Chan, Vars("ActualDaypartCol") + 1 + dp).value = Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)
                        Next

                        'Position in break (detailed)
                        For Each cc As Trinity.cCombinationChannel In comb.Relations
                            Campaign.CalculateSpots(UseFilters:=False, CalculateReach:=False, Bookingtype:=cc.Bookingtype)
                            For Each TmpSpot As Trinity.cActualSpot In Campaign.ActualSpots
                                If cc.Bookingtype Is TmpSpot.Bookingtype Then
                                    Dim PIB As Integer
                                    If TmpSpot.PosInBreak = 1 Then
                                        PIB = 1
                                    ElseIf TmpSpot.PosInBreak = 2 Then
                                        PIB = 2
                                    ElseIf TmpSpot.PosInBreak = TmpSpot.SpotsInBreak Then
                                        PIB = 5
                                    ElseIf TmpSpot.PosInBreak = TmpSpot.SpotsInBreak - 1 Then
                                        PIB = 4
                                    Else
                                        PIB = 3
                                    End If
                                    .Cells(Vars("ActualPIBRow") + Chan, Vars("ActualPIBCol") + PIB).Value += Campaign.Adedge.getUnitGroup(Connect.eUnits.uTRP, TmpSpot.GroupIdx, 0, Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)
                                End If
                            Next
                        Next

                        For Each cc As Trinity.cCombinationChannel In comb.Relations
                            BT = BT + 1
                            Campaign.CalculateSpots(UseFilters:=False, CalculateReach:=True, Bookingtype:=cc.Bookingtype)
                            .Cells(TopRow + BT, 2).Value = cc.Bookingtype.Name
                            .Cells(TopRow + BT, Vars("ActualSpotsCol")).Value = Campaign.Adedge.getGroupCount
                            .Cells(TopRow + BT, Vars("ActualChannelSumsCol") + 0).Value = cc.Bookingtype.BuyingTarget.TargetName

                            If Vars.ContainsKey("ActualNetValueCol") Then
                                'Print Campaign actual value
                                .Cells(TopRow + BT, Vars("ActualNetValueCol") + 0).Value = cc.Bookingtype.ActualNetValue
                            End If

                            If Campaign.Adedge.getGroupCount > 0 Then
                                .Cells(TopRow + BT, Vars("ActualChannelSumsCol") + 1).Value = Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, cc.Bookingtype.BuyingTarget.Target))
                            Else
                                .Cells(TopRow + BT, Vars("ActualChannelSumsCol") + 1).Value = 0
                            End If
                            If Campaign.Adedge.getGroupCount = 0 Then
                                .Cells(TopRow + BT, Vars("ActualChannelSumsCol") + 2).Value = 0
                            Else
                                .Cells(TopRow + BT, Vars("ActualChannelSumsCol") + 2).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, cc.Bookingtype.BuyingTarget.Target), 1)
                            End If
                            For i = 1 To 5
                                If Campaign.Adedge.getGroupCount = 0 Then
                                    .Cells(TopRow + BT, Vars("ActualNationalSumsCol") + 2 + i).Value = 0
                                Else
                                    .Cells(TopRow + BT, Vars("ActualNationalSumsCol") + 2 + i).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, Campaign.MainTarget), i)
                                End If
                            Next
                            Campaign.CalculateSpots(UseFilters:=False, CalculateReach:=False, Bookingtype:=cc.Bookingtype, Daypart:=1)
                            .Cells(TopRow + BT, Vars("ActualNationalSumsCol") + 0).Value = Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)
                            Campaign.CalculateSpots(UseFilters:=False, CalculateReach:=False, Bookingtype:=cc.Bookingtype, PosInBreak:=1)
                            .Cells(TopRow + BT, Vars("ActualNationalSumsCol") + 1).Value = Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)
                            Campaign.CalculateSpots(UseFilters:=False, CalculateReach:=False, Bookingtype:=cc.Bookingtype, PosInBreak:=3)
                            .Cells(TopRow + BT, Vars("ActualNationalSumsCol") + 1).Value = .Cells(TopRow + BT, Vars("ActualNationalSumsCol") + 1).Value + Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)

                            Dim w As Integer = 0
                            For Each TmpWeek As Trinity.cWeek In cc.Bookingtype.Weeks
                                Campaign.CalculateSpots(UseFilters:=False, CalculateReach:=True, OnlyWeek:=TmpWeek.Name, Bookingtype:=cc.Bookingtype)
                                .Cells(TopRow + BT, Vars("ActualNationalWeekCol") + w).Value = Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)

                                'Reach
                                Campaign.CalculateSpots(UseFilters:=False, CalculateReach:=True, OnlyWeek:=TmpWeek.Name)
                                If Campaign.Adedge.getGroupCount > 0 Then
                                    .Cells(Vars("ActualChannelSumsRow") + Vars("Channels") * (Vars("Bookingtypes") + 2), Vars("ActualNationalWeekCol") + w).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, 1)
                                Else
                                    .Cells(Vars("ActualChannelSumsRow") + Vars("Channels") * (Vars("Bookingtypes") + 2), Vars("ActualNationalWeekCol") + w).Value = 0
                                End If
                                Campaign.CalculateSpots(CalculateReach:=True, UseFilters:=False, ToDate:=Date.FromOADate(TmpWeek.EndDate))
                                If Campaign.Adedge.getGroupCount > 0 Then
                                    .Cells(Vars("ActualChannelSumsRow") + Vars("Channels") * (Vars("Bookingtypes") + 2) + 1, Vars("ActualNationalWeekCol") + w).Value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, 1)
                                End If

                                w += 1

                                Dim f As Integer = 0
                                For Each TmpFilm As Trinity.cFilm In TmpWeek.Films
                                    Campaign.CalculateSpots(CalculateReach:=False, UseFilters:=False, Film:=TmpFilm, OnlyWeek:=TmpWeek.Name, Bookingtype:=cc.Bookingtype)
                                    .Cells(Vars("ActualFilmcodeRow") + f, Vars("ActualFilmcodeCol") + 0).Value = TmpFilm.Name
                                    .Cells(Vars("ActualFilmcodeRow") + f, Vars("ActualFilmcodeCol") + 1).Value = TmpFilm.FilmLength
                                    If .Cells(Vars("ActualFilmcodeRow") + f, Vars("ActualFilmcodeCol") + 3 + Chan).Value Is Nothing OrElse .Cells(Vars("ActualFilmcodeRow") + f, Vars("ActualFilmcodeCol") + 3 + Chan).Value.ToString = "" Then
                                        .Cells(Vars("ActualFilmcodeRow") + f, Vars("ActualFilmcodeCol") + 3 + Chan).Value = 0
                                    End If
                                    .Cells(Vars("ActualFilmcodeRow") + f, Vars("ActualFilmcodeCol") + 3 + Chan).Value += Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)
                                    .Cells(Vars("ActualFilmcodeRow") + f, Vars("ActualFilmcodeCol") + Vars("Channels") + 3 + Chan).Value += Campaign.Adedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)
                                    f += 1
                                Next
                            Next
                        Next

                    End If
                Next
            End If

            If Extended Then
                If Not ProgressWindow Is Nothing Then
                    ProgressWindow.Status = "Creating competition analysis... 1/4"
                    ProgressWindow.Progress = 0
                    p = 0
                End If
                Dim CompAdedge As ConnectWrapper.Brands = New ConnectWrapper.Brands
                Dim TmpSex As String
                Select Case Left(Campaign.MainTarget.TargetNameNice, 1)
                    Case "A" : TmpSex = ""
                    Case "M" : TmpSex = "M"
                    Case "W" : TmpSex = "W"
                    Case Else : TmpSex = ""
                End Select
                CompAdedge.setTargetMnemonic(Campaign.MainTarget.TargetName, False)
                CompAdedge.setTargetMnemonic(TmpSex & "3-11," & TmpSex & "12-14," & TmpSex & "15-19," & TmpSex & "20-24," & TmpSex & "25-29," & TmpSex & "30-34," & TmpSex & "35-39," & TmpSex & "40-44," & TmpSex & "45-49," & TmpSex & "50-54," & TmpSex & "55-59," & TmpSex & "60-64," & TmpSex & "65-69," & TmpSex & "70-99", False)
                CompAdedge.clearList()
                CompAdedge.clearBrandFilter()
                CompAdedge.setBrandType("COMMERCIAL")
                CompAdedge.setBrandFilmCode(Campaign.AreaLog, Campaign.FilmcodeString)
                CompAdedge.setPeriod(Format(Date.FromOADate(Campaign.StartDate), "ddMMyy") & "-" & Format(Date.FromOADate(Campaign.EndDate), "ddMMyy"))
                CompAdedge.setArea(Campaign.Area)
                CompAdedge.setChannelsArea(Campaign.ChannelString, Campaign.Area)
                CompAdedge.setSplitOff()
                If Not ProgressWindow Is Nothing Then
                    CompAdedge.registerCallback(New ConnectCallback(ProgressWindow))
                End If
                Dim SpotCount As Integer = CompAdedge.Run(False, False, Campaign.FrequencyFocus + 1)
                Dim MyTRP As Single = CompAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smAll, , , Trinity.Helper.TargetIndex(CompAdedge, Campaign.MainTarget))
                .Range(.Cells(Vars("ActualReachCompRow") + 2, Vars("ActualReachCompCol") + 0).Address & ":" & .Cells(Vars("ActualReachCompRow") + 21, Vars("ActualReachCompCol") + 6).Address).Value = ""
                .Range(.Cells(Vars("ActualReachCompRow") + 2, Vars("ActualReachCompCol") + 8 + 0).Address & ":" & .Cells(Vars("ActualReachCompRow") + 21, Vars("ActualReachCompCol") + 14).Address).Value = ""
                For i = 0 To 13
                    .Cells(Vars("ActualReachCompRow") + i + 2, Vars("ActualReachCompCol") + 0).Value = CompAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smAll, , , i + 1)
                    .Cells(Vars("ActualReachCompRow") + i + 2, Vars("ActualReachCompCol") + 8 + 0).Value = CompAdedge.getRF(SpotCount - 1, , , i + 1, Campaign.FrequencyFocus + 1)
                Next
                For d As Integer = Campaign.StartDate To Campaign.EndDate
                    .Cells(Vars("ActualHeatmapRow") + 1, Vars("ActualHeatmapCol") + d - Campaign.StartDate + 1).Value = Date.FromOADate(d)
                    CompAdedge.clearGroup()
                    For s As Integer = 0 To SpotCount - 1
                        If CompAdedge.getAttrib(Connect.eAttribs.aDate, s) = d Then
                            CompAdedge.addToGroup(s)
                        End If
                    Next
                    For t As Integer = 1 To 14
                        If .Cells(Vars("ActualHeatmapRow") + t + 1, Vars("ActualHeatmapCol") + d - Campaign.StartDate + 1).Value Is Nothing OrElse .Cells(Vars("ActualHeatmapRow") + t + 1, Vars("ActualHeatmapCol") + d - Campaign.StartDate + 1).Value.ToString = "" Then
                            .Cells(Vars("ActualHeatmapRow") + t + 1, Vars("ActualHeatmapCol") + d - Campaign.StartDate + 1).Value = 0
                        End If
                        .Cells(Vars("ActualHeatmapRow") + t + 1, Vars("ActualHeatmapCol") + d - Campaign.StartDate + 1).Value += CompAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , , t)
                    Next
                Next
                If Not ProgressWindow Is Nothing Then
                    ProgressWindow.Status = "Creating competition analysis... 2/4"
                    ProgressWindow.Progress = 0
                    p = 0
                End If
                CompAdedge.clearGroup()
                Dim ProdStr As String = ""
                For j As Integer = 0 To UBound(Competitors)
                    ProdStr += Competitors(j) & ","
                Next
                Dim MyCatTRP As Single
                CompAdedge.clearList()
                CompAdedge.clearBrandFilter()
                CompAdedge.setPeriod(Format(Date.FromOADate(Campaign.StartDate), "ddMMyy") & "-" & Format(Date.FromOADate(Campaign.EndDate), "ddMMyy"))
                CompAdedge.setArea(Campaign.Area)
                CompAdedge.setTargetMnemonic(TmpSex & "3-11," & TmpSex & "12-14," & TmpSex & "15-19," & TmpSex & "20-24," & TmpSex & "25-29," & TmpSex & "30-34," & TmpSex & "35-39," & TmpSex & "40-44," & TmpSex & "45-49," & TmpSex & "50-54," & TmpSex & "55-59," & TmpSex & "60-64," & TmpSex & "60-69," & TmpSex & "70-99", False)
                Trinity.Helper.AddTarget(CompAdedge, Campaign.MainTarget)
                Trinity.Helper.AddTarget(CompAdedge, Campaign.SecondaryTarget)
                Trinity.Helper.AddTarget(CompAdedge, Campaign.ThirdTarget)
                CompAdedge.setChannelsArea(Campaign.ChannelString, Campaign.Area)
                CompAdedge.setSplitOff()
                CompAdedge.clearList()
                CompAdedge.clearBrandFilter()
                CompAdedge.setBrandType("COMMERCIAL")
                If ProdStr <> "" Then
                    CompAdedge.setBrandProduct(Campaign.AreaLog, ProdStr)
                    SpotCount = CompAdedge.Run(False, False, 10)
                    MyCatTRP = CompAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smAll, , , Trinity.Helper.TargetIndex(CompAdedge, Campaign.MainTarget))

                    .Range(.Cells(Vars("ActualSharesRow") + 2, Vars("ActualSharesCol") + 1).Address & ":" & .Cells(Vars("ActualSharesRow") + 2, Vars("ActualSharesCol") + 7).Address).Value = ""
                    .Range(.Cells(Vars("ActualReachBuildupCompRow") + 2, Vars("ActualReachBuildupCompCol") + 0).Address & ":" & .Cells(Vars("ActualReachBuildupCompRow") + 2, Vars("ActualReachBuildupCompCol") + 4).Address).Value = ""
                    c = CompAdedge.setSplitVar("product")
                    For i = 0 To c - 1
                        CompAdedge.setSplitList(i)
                        Dim j As Integer = 0
                        While ((CompAdedge.getSplitName(i, 0) <> Campaign.ActualSpots(1).Product And .Cells(Vars("ActualSharesRow") + 2, Vars("ActualSharesCol") + 2 + j).Value > CompAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smSplit, , , 0)) Or (CompAdedge.getSplitName(i, 0) = Campaign.ActualSpots(1).Product And .Cells(Vars("ActualSharesRow") + 2, Vars("ActualSharesCol") + 2 + j).Value > CompAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smSplit, , , Trinity.Helper.TargetIndex(CompAdedge, Campaign.MainTarget)) - MyTRP)) And j < 5
                            j = j + 1
                        End While
                        If j < 5 Then
                            .Cells(Vars("ActualSharesRow") + 2, Vars("ActualSharesCol") + 0).Value = .Cells(Vars("ActualSharesRow") + 2, Vars("ActualSharesCol") + 7).Value + .Cells(Vars("ActualSharesRow") + 2, Vars("ActualSharesCol") + 6).Value
                            For w As Integer = 3 To j Step -1
                                .Cells(Vars("ActualSharesRow") + 2, Vars("ActualSharesCol") + 2 + w + 1).Value = .Cells(Vars("ActualSharesRow") + 2, Vars("ActualSharesCol") + 2 + w).Value
                                .Cells(Vars("ActualSharesRow") + 1, Vars("ActualSharesCol") + 2 + w + 1).Value = .Cells(Vars("ActualSharesRow") + 1, Vars("ActualSharesCol") + 2 + w).Value
                                If Vars.ContainsKey("ActualCompetitionCol") Then
                                    For t As Integer = 0 To 2
                                        .Cells(Vars("ActualCompetitionRow") + t + (w + 1) * 3, Vars("ActualCompetitionCol") + 2).Value = .Cells(Vars("ActualCompetitionRow") + t + w * 3, Vars("ActualCompetitionCol") + 2).Value
                                        .Cells(Vars("ActualCompetitionRow") + t + (w + 1) * 3, Vars("ActualCompetitionCol") + 3).Value = .Cells(Vars("ActualCompetitionRow") + t + w * 3, Vars("ActualCompetitionCol") + 3).Value
                                        For f As Integer = 1 To 10
                                            .Cells(Vars("ActualCompetitionRow") + t + (w + 1) * 3, Vars("ActualCompetitionCol") + 3 + f).Value = .Cells(Vars("ActualCompetitionRow") + t + w * 3, Vars("ActualCompetitionCol") + 3 + f).Value
                                        Next
                                    Next
                                End If
                            Next
                            .Cells(Vars("ActualSharesRow") + 2, Vars("ActualSharesCol") + 2 + j).Value = CompAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smSplit, , , Trinity.Helper.TargetIndex(CompAdedge, Campaign.MainTarget))
                            .Cells(Vars("ActualSharesRow") + 1, Vars("ActualSharesCol") + 2 + j).Value = CompAdedge.getSplitName(i, 0)
                            If Vars.ContainsKey("ActualCompetitionCol") Then
                                .Cells(Vars("ActualCompetitionRow") + 0 + j * 3, Vars("ActualCompetitionCol") + 2).Value = CompAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smSplit, , Campaign.TimeShift, Trinity.Helper.TargetIndex(CompAdedge, Campaign.MainTarget))
                                .Cells(Vars("ActualCompetitionRow") + 0 + j * 3, Vars("ActualCompetitionCol") + 3).Value = CompAdedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smSplit, , Campaign.TimeShift, Trinity.Helper.TargetIndex(CompAdedge, Campaign.MainTarget))
                                For f As Integer = 1 To 10
                                    Dim sc As Integer = CompAdedge.recalcRF(Connect.eSumModes.smSplit)
                                    .Cells(Vars("ActualCompetitionRow") + 0 + j * 3, Vars("ActualCompetitionCol") + 3 + f).Value = CompAdedge.getRF(sc - 1, 0, Campaign.TimeShift, Trinity.Helper.TargetIndex(CompAdedge, Campaign.MainTarget), f)
                                Next

                                .Cells(Vars("ActualCompetitionRow") + 1 + j * 3, Vars("ActualCompetitionCol") + 2).Value = CompAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smSplit, , Campaign.TimeShift, Trinity.Helper.TargetIndex(CompAdedge, Campaign.SecondaryTarget))
                                .Cells(Vars("ActualCompetitionRow") + 1 + j * 3, Vars("ActualCompetitionCol") + 3).Value = CompAdedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smSplit, , Campaign.TimeShift, Trinity.Helper.TargetIndex(CompAdedge, Campaign.SecondaryTarget))
                                For f As Integer = 1 To 10
                                    Dim sc As Integer = CompAdedge.recalcRF(Connect.eSumModes.smSplit)
                                    .Cells(Vars("ActualCompetitionRow") + 1 + j * 3, Vars("ActualCompetitionCol") + 3 + f).Value = CompAdedge.getRF(sc - 1, 0, Campaign.TimeShift, Trinity.Helper.TargetIndex(CompAdedge, Campaign.SecondaryTarget), f)
                                Next

                                .Cells(Vars("ActualCompetitionRow") + 2 + j * 3, Vars("ActualCompetitionCol") + 2).Value = CompAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smSplit, , Campaign.TimeShift, Trinity.Helper.TargetIndex(CompAdedge, Campaign.ThirdTarget))
                                .Cells(Vars("ActualCompetitionRow") + 2 + j * 3, Vars("ActualCompetitionCol") + 3).Value = CompAdedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smSplit, , Campaign.TimeShift, Trinity.Helper.TargetIndex(CompAdedge, Campaign.ThirdTarget))
                                For f As Integer = 1 To 10
                                    Dim sc As Integer = CompAdedge.recalcRF(Connect.eSumModes.smSplit)
                                    .Cells(Vars("ActualCompetitionRow") + 2 + j * 3, Vars("ActualCompetitionCol") + 3 + f).Value = CompAdedge.getRF(sc - 1, 0, Campaign.TimeShift, Trinity.Helper.TargetIndex(CompAdedge, Campaign.ThirdTarget), f)
                                Next
                            End If
                            'Stop
                        Else
                            .Cells(Vars("ActualSharesRow") + 2, Vars("ActualSharesCol") + 7).Value += CompAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smSplit, , , Trinity.Helper.TargetIndex(CompAdedge, Campaign.MainTarget))
                        End If
                    Next
                    Dim Found As Boolean = False
                    For j As Integer = 0 To 4
                        If .Cells(Vars("ActualSharesRow") + 1, Vars("ActualSharesCol") + 2 + j).Value = Campaign.ActualSpots(1).Product Then
                            .Cells(Vars("ActualSharesRow") + 2, Vars("ActualSharesCol") + 2 + j).Value = .Cells(Vars("ActualSharesRow") + 2, Vars("ActualSharesCol") + 2 + j).Value - MyTRP
                            Found = True
                        End If
                    Next
                    '            If Not Found Then
                    '                .cells(100, 15) = .cells(100, 15) - MyTRP
                    '            End If
                    .Cells(Vars("ActualSharesRow") + 2, Vars("ActualSharesCol") + 1).Value = MyTRP

                    c = CompAdedge.setSplitVar("product")
                    For i = 0 To c - 1
                        Dim sc As Integer = CompAdedge.setSplitList(i)
                        CompAdedge.recalcRF(Connect.eSumModes.smSplit)
                        If CompAdedge.getSplitName(i, 0) <> Campaign.ActualSpots(1).Product Then
                            Dim j As Integer = 1
                            '                    While .cells(100, 10 + j) > CompAdedge.getSumU(uBrandTrp30, smSplit, , , 0) And j < 5
                            While CompAdedge.getSplitName(i, 0) <> .Cells(Vars("ActualSharesRow") + 1, Vars("ActualSharesCol") + 1 + j).Value And j < 7
                                j = j + 1
                            End While
                            If j < 7 Then
                                For t As Integer = 1 To 14
                                    'For w As Integer = 3 To j Step -1
                                    '    .cells(Vars("ActualReachCompRow") + 1 + t, Vars("ActualReachCompCol") + 1 + w + 1).value = .cells(Vars("ActualReachCompRow") + 1 + t, Vars("ActualReachCompCol") + 1 + w).value
                                    '    .cells(Vars("ActualReachCompRow") + 1 + t, Vars("ActualReachCompCol") + 9 + w + 1).value = .cells(Vars("ActualReachCompRow") + 1 + t, Vars("ActualReachCompCol") + 9 + w).value
                                    'Next
                                    .Cells(Vars("ActualReachCompRow") + 1 + t, Vars("ActualReachCompCol") + j).Value = CompAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smSplit, , , t)
                                    .Cells(Vars("ActualReachCompRow") + 1 + t, Vars("ActualReachCompCol") + 8 + j).Value = CompAdedge.getRF(sc - 1, , , t, Campaign.FrequencyFocus + 1)
                                Next
                                'For w As Integer = 3 To j Step -1
                                '    .cells(Vars("ActualReachCompRow") + 14, Vars("ActualReachCompCol") + 1 + w + 1).value = .cells(41, Vars("ActualReachCompCol") + 1 + w).value
                                '    .cells(Vars("ActualReachCompRow") + 1, Vars("ActualReachCompCol") + 1 + w + 1).value = .cells(Vars("ActualReachCompRow") + 1, Vars("ActualReachCompCol") + 1 + w).value
                                'Next
                                .Cells(Vars("ActualReachCompRow") + 14, Vars("ActualReachCompCol") + j).Value = CompAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smSplit, , , Trinity.Helper.TargetIndex(CompAdedge, Campaign.MainTarget))
                            End If
                            j -= 1
                            If j < 6 Then
                                .Cells(Vars("ActualReachBuildupCompRow") - 1, j + 2).Value = .Cells(Vars("ActualSharesRow") + 1, Vars("ActualSharesCol") + 2 + j).Value
                                Dim TRP As Single = 0
                                Dim q As Integer = Vars("ActualReachBuildupCompRow")
                                x = 0
                                For s As Integer = 0 To SpotCount - 1
                                    If CompAdedge.getAttrib(Connect.eAttribs.aBrandProduct, s) = CompAdedge.getSplitName(i, 0) Then
                                        TRP = TRP + CompAdedge.getUnit(Connect.eUnits.uTRP, s, , , Trinity.Helper.TargetIndex(CompAdedge, Campaign.MainTarget))
                                        If TRP > .Cells(q, 1).Value Then
                                            .Cells(q, j + 2).Value = CompAdedge.getRF(x, , , Trinity.Helper.TargetIndex(CompAdedge, Campaign.MainTarget), 1) / 100
                                            q = q + 1
                                        End If
                                        x = x + 1
                                    End If
                                Next
                            End If
                        End If
                    Next

                    If Not ProgressWindow Is Nothing Then
                        ProgressWindow.Status = "Creating competition analysis... 3/4"
                        ProgressWindow.Progress = 0
                        p = 0
                    End If

                    'Per channel for Competition
                    If Vars.ContainsKey("ActualCompetitionChannelRow") Then
                        Dim cp As Integer = CompAdedge.setSplitVar("channel,product")

                        r = Vars("ActualSummaryRow")
                        Dim offsetList As New List(Of String)

                        While Campaign.Channels.Contains(.Cells(r, Vars("ActualSummaryCol")).Value)
                            'While .cells(r, Vars("ActualSummaryCol")).value <> "" AndAlso .cells(r, Vars("ActualSummaryCol")).value <> "0" AndAlso .cells(r, Vars("ActualSummaryCol")).value IsNot Nothing AndAlso CDbl(.cells(r, Vars("ActualSummaryCol")).value) > 0
                            offsetList.Add(.Cells(r, Vars("ActualSummaryCol")).Value)
                            r += 1
                        End While

                        For i = 0 To cp - 1
                            CompAdedge.setSplitList(i)
                            Debug.Print(CompAdedge.getSplitName(i, 0) & ", " & CompAdedge.getSplitName(i, 1))
                            If Not ProgressWindow Is Nothing Then
                                ProgressWindow.Progress = (i / (cp - 1)) * 100
                            End If
                            Dim sc As Integer = CompAdedge.setSplitList(i)

                            r = Vars("ActualCompetitionChannelRow") + 1
                            c = Vars("ActualCompetitionChannelCol") + offsetList.IndexOf(Trinity.Helper.Adedge2Channel(CompAdedge.getSplitName(i, 0)).ChannelName) + 1
                            While .Cells(Vars("ActualCompetitionChannelRow"), c).Value <> CompAdedge.getSplitName(i, 0) AndAlso .Cells(Vars("ActualCompetitionChannelRow"), c).Value <> ""
                                c += 1
                            End While
                            .Cells(Vars("ActualCompetitionChannelRow"), c).Value = CompAdedge.getSplitName(i, 0)
                            While .Cells(r, Vars("ActualCompetitionChannelCol")).Value <> CompAdedge.getSplitName(i, 1)
                                r += 1
                            End While
                            .Cells(r, c).Value = CompAdedge.getSumU(Connect.eUnits.uBrandTrp30, Connect.eSumModes.smSplit, , Campaign.TimeShift, Trinity.Helper.TargetIndex(CompAdedge, Campaign.MainTarget))
                        Next
                    End If

                    If Not ProgressWindow Is Nothing Then
                        ProgressWindow.Status = "Creating competition analysis... 4/4"
                        ProgressWindow.Progress = 0
                        p = 0
                    End If
                    CompAdedge.clearList()
                    CompAdedge.clearBrandFilter()
                    CompAdedge.setBrandType("COMMERCIAL")
                    CompAdedge.Run(False)
                    Dim TotTRP As Decimal = CompAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smAll, , , Trinity.Helper.TargetIndex(CompAdedge, Campaign.MainTarget))
                    .Cells(Vars("ActualSharesRow") + 6, Vars("ActualSharesCol") + 3).Value = TotTRP - MyCatTRP - MyTRP
                End If
                If Not ProgressWindow Is Nothing Then
                    ProgressWindow.Status = "Creating category analysis... "
                    ProgressWindow.Progress = 0
                    p = 0
                End If
                CompAdedge.clearList()
                CompAdedge.clearBrandFilter()
                CompAdedge.setBrandType("COMMERCIAL")
                SpotCount = CompAdedge.Run(False)
                CompAdedge.unregisterCallback()

                Dim Categories As New SortedList(Of String, Single)
                For s As Integer = 0 To SpotCount - 1
                    If Categories.ContainsKey(CompAdedge.getAttrib(Connect.eAttribs.aBrandTopCategory, s)) Then
                        Categories(CompAdedge.getAttrib(Connect.eAttribs.aBrandTopCategory, s)) += CompAdedge.getUnit(Connect.eUnits.uTRP, s, , , Trinity.Helper.TargetIndex(CompAdedge, Campaign.MainTarget))
                    Else
                        Categories.Add(CompAdedge.getAttrib(Connect.eAttribs.aBrandTopCategory, s), CompAdedge.getUnit(Connect.eUnits.uTRP, s, , , Trinity.Helper.TargetIndex(CompAdedge, Campaign.MainTarget)))
                    End If
                Next
                For i = 0 To Categories.Count - 1
                    .Cells(Vars("ActualSharesRow") + 9, Vars("ActualSharesCol") + 1 + i).Value = Categories.Keys(i).ToString
                    .Cells(Vars("ActualSharesRow") + 10, Vars("ActualSharesCol") + 1 + i).Value = Categories(Categories.Keys(i).ToString)
                Next

                If Not ProgressWindow Is Nothing Then
                    ProgressWindow.Status = "Creating index analysis... "
                    ProgressWindow.Progress = 0
                    p = 0
                End If

                x = Vars("ActualBuyingIndexRow")
                Dim TmpAdedge As New ConnectWrapper.Brands
                TmpAdedge.setArea(Campaign.Area)
                TmpAdedge.setPeriod(Format(Date.FromOADate(Campaign.StartDate), "ddMMyy") & "-" & Format(Date.FromOADate(Campaign.EndDate), "ddMMyy"))
                TmpAdedge.setTargetMnemonic(Campaign.MainTarget.TargetName & "," & Campaign.AllAdults, True)

                'TODO: make this function Advanced compatible
                For Each TmpChan As Trinity.cChannel In Campaign.Channels
                    Dim UseIt As Boolean = False
                    Dim AllBTs As New List(Of Trinity.cBookingType)
                    For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                        If TmpBT.BookIt AndAlso Not (singleRowCombinations AndAlso Not TmpBT.ShowMe) Then
                            UseIt = True
                            AllBTs.Add(TmpBT)
                        End If
                    Next
                    If Not ProgressWindow Is Nothing Then
                        p += 1
                        ProgressWindow.Status = "Creating index analysis... " & TmpChan.ChannelName
                        ProgressWindow.Progress = (p / (Campaign.Channels.Count + Campaign.Combinations.Count)) * 100
                    End If
                    If UseIt Then
                        Campaign.CalculateSpots(CalculateReach:=False, UseFilters:=False, Bookingtypes:=AllBTs)
                        If Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge) - 1) > 0 Then
                            .Cells(x, Vars("ActualBuyingIndexCol") + 3).Value = (Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, Campaign.MainTarget)) / Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, Campaign.AllAdults))) * 100
                        Else
                            .Cells(x, Vars("ActualBuyingIndexCol") + 3).Value = 0
                        End If
                        TmpAdedge.clearList()
                        TmpAdedge.setChannelsArea(TmpChan.AdEdgeNames, Campaign.Area)
                        TmpAdedge.Run(False, False, 0)
                        .Cells(x, Vars("ActualBuyingIndexCol") + 2).Value = (TmpAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smAll, , , 0) / TmpAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smAll, , , 1)) * 100
                        x += 1
                    End If
                Next
                If singleRowCombinations Then
                    For Each comb As Trinity.cCombination In Campaign.Combinations
                        If Not ProgressWindow Is Nothing Then
                            p += 1
                            ProgressWindow.Status = "Creating index analysis... " & comb.Name
                            ProgressWindow.Progress = (p / (Campaign.Channels.Count + Campaign.Combinations.Count)) * 100
                        End If
                        If comb.ShowAsOne Then
                            Dim BTList As New List(Of Trinity.cBookingType)
                            Dim ChanStr As String = ""
                            For Each cc As Trinity.cCombinationChannel In comb.Relations
                                BTList.Add(cc.Bookingtype)
                                ChanStr &= cc.Bookingtype.ParentChannel.AdEdgeNames & ","
                            Next

                            Campaign.CalculateSpots(CalculateReach:=False, UseFilters:=False, Bookingtypes:=BTList)
                            If Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge) - 1) > 0 Then
                                .Cells(x, Vars("ActualBuyingIndexCol") + 3).Value = (Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, Campaign.MainTarget)) / Campaign.Adedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smGroup, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, Campaign.AllAdults))) * 100
                            Else
                                .Cells(x, Vars("ActualBuyingIndexCol") + 3).Value = 0
                            End If
                            TmpAdedge.clearList()
                            TmpAdedge.setChannelsArea(ChanStr, Campaign.Area)
                            TmpAdedge.Run(False, False, 0)
                            .Cells(x, Vars("ActualBuyingIndexCol") + 2).Value = (TmpAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smAll, , , 0) / TmpAdedge.getSumU(Connect.eUnits.uTRP, Connect.eSumModes.smAll, , , 1)) * 100
                            x += 1

                        End If
                    Next
                End If
                If Vars.ContainsKey("ActualUniqueReachCol") Then
                    Dim RF(0 To 9) As Single
                    Dim s As Integer = Campaign.Adedge.recalcRF(Connect.eSumModes.smAll)
                    For i = 0 To 9
                        RF(i) = Campaign.Adedge.getRF(s - 1, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, Campaign.MainTarget), i + 1)
                    Next
                    x = 0
                    p = 0
                    For Each TmpChan As Trinity.cChannel In Campaign.Channels
                        Dim AllBTs As New List(Of Trinity.cBookingType)
                        Dim UseIt As Boolean = False
                        For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                            If TmpBT.BookIt AndAlso Not (singleRowCombinations AndAlso Not TmpBT.ShowMe) Then
                                UseIt = True
                                AllBTs.Add(TmpBT)
                            End If
                        Next
                        If Not ProgressWindow Is Nothing Then
                            p += 1
                            ProgressWindow.Status = "Creating unique reach... " & TmpChan.ChannelName
                            ProgressWindow.Progress = (p / (Campaign.Channels.Count + Campaign.Combinations.Count)) * 100
                        End If
                        If UseIt Then
                            Campaign.CalculateSpots(CalculateReach:=True, UseFilters:=False, ExcludeBookingtypes:=AllBTs)
                            .Cells(Vars("ActualUniqueReachRow") + 2 + x, Vars("ActualUniqueReachCol") + 0).Value = TmpChan.ChannelName
                            For i = 0 To 9
                                .Cells(Vars("ActualUniqueReachRow") + 2 + x, Vars("ActualUniqueReachCol") + i + 1).Value = RF(i) - Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, Campaign.MainTarget), i + 1)
                            Next
                            x += 1
                        End If
                    Next
                    If singleRowCombinations Then
                        For Each comb As Trinity.cCombination In Campaign.Combinations
                            If Not ProgressWindow Is Nothing Then
                                p += 1
                                ProgressWindow.Status = "Creating unique reach... " & comb.Name
                                ProgressWindow.Progress = (p / (Campaign.Channels.Count + Campaign.Combinations.Count)) * 100
                            End If
                            If comb.ShowAsOne Then
                                Dim BTList As New List(Of Trinity.cBookingType)
                                For Each cc As Trinity.cCombinationChannel In comb.Relations
                                    BTList.Add(cc.Bookingtype)
                                Next
                                Campaign.CalculateSpots(CalculateReach:=True, UseFilters:=False, ExcludeBookingtypes:=BTList)
                                .Cells(Vars("ActualUniqueReachRow") + 2 + x, Vars("ActualUniqueReachCol") + 0).Value = comb.Name
                                For i = 0 To 9
                                    .Cells(Vars("ActualUniqueReachRow") + 2 + x, Vars("ActualUniqueReachCol") + i + 1).Value = RF(i) - Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Trinity.Helper.TargetIndex(Campaign.Adedge, Campaign.MainTarget), i + 1)
                                Next
                                x += 1
                            End If
                        Next
                    End If
                End If

            End If
        End With
        If Extended Then
            With Excel.Sheets(Vars("SpotlistSheet").ToString)

                Dim c As Integer = 0
                Dim TmpAdedge As New ConnectWrapper.Breaks

                TmpAdedge.setArea(Campaign.Area)
                Trinity.Helper.AddTarget(TmpAdedge, Campaign.MainTarget)
                Campaign.CalculateSpots(UseFilters:=False)
                For Each TmpChan As Trinity.cChannel In Campaign.Channels
                    For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                        If TmpBT.BookIt AndAlso TmpBT.IsSpecific Then
                            If Not ProgressWindow Is Nothing Then
                                ProgressWindow.Status = "Creating spotlist analysis... " & TmpBT.ToString
                                ProgressWindow.Progress = 0
                                p = 0
                            End If
                            Dim eventsTable As DataTable
                            Dim PeriodStr As String = ""
                            Dim r As Integer = 4
                            Dim BreakCount As Long
                            Dim b As Long = 0

                            TmpAdedge.clearList()
                            TmpAdedge.setChannelsArea(TmpChan.AdEdgeNames, Campaign.Area)

                            .Cells(1, c + 1).Value = TmpChan.ChannelName
                            .Cells(1, c + 2).Value = TmpBT.Name
                            Dim _dates As New List(Of KeyValuePair(Of Date, Date))
                            For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                                _dates.Add(New KeyValuePair(Of Date, Date)(Date.FromOADate(TmpWeek.StartDate), Date.FromOADate(TmpWeek.EndDate)))
                                PeriodStr &= Format(Date.FromOADate(TmpWeek.StartDate), "ddMMyy") & "-" & Format(Date.FromOADate(TmpWeek.EndDate), "ddMMyy") & ","
                            Next
                            TmpAdedge.setPeriod(PeriodStr)

                            BreakCount = TmpAdedge.Run()

                            eventsTable = DBReader.getEvents(_dates, TmpChan.ChannelName)

                            For Each dr As DataRow In eventsTable.Rows

                                If Not ProgressWindow Is Nothing Then
                                    p += 1
                                    ProgressWindow.Progress = (p / (eventsTable.Rows.Count + Campaign.ActualSpots.Count)) * 100
                                End If

                                Dim LastDist As Integer

                                If dr!Startmam >= 26 * 60 Then
                                    dr!Startmam -= 24 * 60
                                    dr!Date += 1
                                End If
                                .Cells(r, c + 1).Value = dr!Date
                                .Cells(r, c + 2).Value = Trinity.Helper.Mam2Tid(dr!StartMam)
                                .Cells(r, c + 3).Value = dr!Name
                                Dim TmpDummy As Boolean
                                On Error Resume Next
                                TmpDummy = IsDBNull(dr.Item("EstimationTarget"))
                                If Err.Number = 0 Then
                                    On Error GoTo ErrHandle
                                    If IsDBNull(dr.Item("EstimationTarget")) Then dr.Item("EstimationTarget") = ""
                                    If IsDBNull(dr.Item("price")) Then dr.Item("price") = 0
                                    If dr.Item("UseCPP") Then
                                        If Not TmpBT.Pricelist.Targets(Trim(dr.Item("EstimationTarget"))) Is Nothing Then
                                            .Cells(r, c + 5).Value = Math.Round(dr.Item("ChanEst") * (1 + (dr.Item("Addition") / 100)) * (TmpBT.Pricelist.Targets(Trim(dr.Item("EstimationTarget"))).GetCPPForDate(dr!Date, dr!Daypart) * (TmpBT.Indexes.GetIndexForDate(Date.FromOADate(dr!Date), Trinity.cIndex.IndexOnEnum.eGrossCPP) / 100) * (1 - TmpBT.BuyingTarget.Discount) * (TmpBT.Indexes.GetIndexForDate(Date.FromOADate(dr!Date), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100) * TmpBT.BuyingTarget.GetCPPForDate(dr!Date)), 0)
                                        End If
                                    Else
                                        'Calculate the appropriate Net price for that break
                                        .Cells(r, c + 5).Value = Math.Round(dr.Item("price") * (TmpBT.Indexes.GetIndexForDate(Date.FromOADate(dr!Date), Trinity.cIndex.IndexOnEnum.eGrossCPP) / 100) * (1 - TmpBT.BuyingTarget.Discount) * (TmpBT.Indexes.GetIndexForDate(Date.FromOADate(dr!Date), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100), 0)
                                    End If
                                Else
                                    On Error GoTo ErrHandle
                                    .Cells(r, c + 5).Value = Math.Round(dr.Item("price") * (TmpBT.Indexes.GetIndexForDate(Date.FromOADate(dr!Date), Trinity.cIndex.IndexOnEnum.eGrossCPP) / 100) * (1 - TmpBT.BuyingTarget.Discount) * (TmpBT.Indexes.GetIndexForDate(Date.FromOADate(dr!Date), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100), 0)
                                End If
                                If .cells(r, c + 5).value > 0 Then
                                    .cells(r, c + 6).FormulaR1C1 = "=RC[-1]/RC[-2]"

                                    'Step a little bit back
                                    If b > 10 Then
                                        b -= 10
                                    Else
                                        b = 0
                                    End If

                                    While b < BreakCount AndAlso TmpAdedge.getAttrib(Connect.eAttribs.aDate, b) < dr!Date
                                        b += 1
                                    End While
                                    LastDist = 9999
                                    While b < BreakCount AndAlso (Math.Abs(dr!startMam - TmpAdedge.getAttrib(Connect.eAttribs.aFromTime, b) \ 60) < LastDist OrElse TmpAdedge.getAttrib(Connect.eAttribs.aBreaktitle, b) <> "Break")
                                        If TmpAdedge.getAttrib(Connect.eAttribs.aBreaktitle, b) = "Break" Then
                                            LastDist = Math.Abs(dr!startMam - TmpAdedge.getAttrib(Connect.eAttribs.aFromTime, b) \ 60)
                                        End If
                                        b += 1
                                    End While
                                    b -= 1
                                    While TmpAdedge.getAttrib(Connect.eAttribs.aBreaktitle, b) <> "Break"
                                        b -= 1
                                    End While
                                    .cells(r, c + 4).value = Math.Round(TmpAdedge.getUnit(Connect.eUnits.uTRP, b), 1)
                                    .cells(r, c + 7).value = TmpAdedge.getAttrib(Connect.eAttribs.aBreakProgAfter, b)
                                    'Stop
                                    r += 1
                                End If
                            Next
                            r = 4
                            For Each TmpSpot As Trinity.cActualSpot In Campaign.ActualSpots
                                If Not ProgressWindow Is Nothing Then
                                    p += 1
                                    ProgressWindow.Progress = (p / (eventsTable.Rows.Count + Campaign.ActualSpots.Count)) * 100
                                End If
                                If TmpSpot.Bookingtype Is TmpBT Then
                                    .Cells(r, c + 8).Value = TmpSpot.AirDate
                                    .Cells(r, c + 9).Value = Trinity.Helper.Mam2Tid(TmpSpot.MaM)
                                    .Cells(r, c + 11).Value = Math.Round(TmpSpot.Rating30(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget), 1)
                                    .cells(r, c + 13).FormulaR1C1 = "=RC[-1]/RC[-2]"
                                    .Cells(r, c + 14).Value = TmpSpot.ProgAfter
                                    If TmpSpot.MatchedSpot Is Nothing OrElse TmpSpot.MatchedSpot.PriceNet = 0 Then
                                        Dim LastDist As Integer = 9999
                                        For Each dr As DataRow In eventsTable.Rows
                                            If dr!Date = TmpSpot.AirDate Then
                                                If Math.Abs(TmpSpot.MaM - dr!Startmam) > LastDist Then
                                                    dr = dr.Table.Rows(dr.Table.Rows.IndexOf(dr) - 1)
                                                    .Cells(r, c + 10).Value = dr!Name
                                                    Dim TmpDummy As Boolean
                                                    On Error Resume Next
                                                    TmpDummy = IsDBNull(dr.Item("EstimationTarget"))
                                                    If Err.Number = 0 Then
                                                        On Error GoTo ErrHandle
                                                        If IsDBNull(dr.Item("EstimationTarget")) Then dr.Item("EstimationTarget") = ""
                                                        If IsDBNull(dr.Item("price")) Then dr.Item("price") = 0
                                                        If dr.Item("UseCPP") Then
                                                            If Not TmpBT.Pricelist.Targets(Trim(dr.Item("EstimationTarget"))) Is Nothing Then
                                                                .Cells(r, c + 12).Value = Math.Round(dr.Item("ChanEst") * (1 + (dr.Item("Addition") / 100)) * (TmpBT.Pricelist.Targets(Trim(dr.Item("EstimationTarget"))).GetCPPForDate(dr!Date, dr!Daypart)) * (TmpBT.Indexes.GetIndexForDate(Date.FromOADate(dr!Date), Trinity.cIndex.IndexOnEnum.eGrossCPP) / 100) * (1 - TmpBT.BuyingTarget.Discount) * (TmpBT.Indexes.GetIndexForDate(Date.FromOADate(dr!Date), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100) * (TmpBT.BuyingTarget.GetCPPForDate(dr!Date)), 0)
                                                            End If
                                                        Else
                                                            .Cells(r, c + 12).Value = Math.Round(dr.Item("price") * (TmpBT.Indexes.GetIndexForDate(Date.FromOADate(dr!Date), Trinity.cIndex.IndexOnEnum.eGrossCPP) / 100) * (1 - TmpBT.BuyingTarget.Discount) * (TmpBT.Indexes.GetIndexForDate(Date.FromOADate(dr!Date), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100), 0)
                                                        End If
                                                    Else
                                                        On Error GoTo ErrHandle
                                                        .Cells(r, c + 12).Value = Math.Round(dr.Item("price") * (TmpBT.Indexes.GetIndexForDate(Date.FromOADate(dr!Date), Trinity.cIndex.IndexOnEnum.eGrossCPP) / 100) * (1 - TmpBT.BuyingTarget.Discount) * (TmpBT.Indexes.GetIndexForDate(Date.FromOADate(dr!Date), Trinity.cIndex.IndexOnEnum.eNetCPP) / 100), 0)
                                                    End If
                                                    Exit For
                                                End If
                                                LastDist = Math.Abs(TmpSpot.MaM - dr!Startmam)
                                            End If
                                        Next
                                    Else
                                        .Cells(r, c + 10).Value = "-"
                                        .Cells(r, c + 12).Value = Math.Round(TmpSpot.MatchedSpot.PriceNet, 0)
                                    End If
                                    r += 1
                                End If
                            Next
                            c += 14
                        End If
                    Next
                Next
            End With
        End If

        Campaign.CalculateSpots(True) ' Denna resettar allt, om den tas bort fuckar reachen ur.

        If AllKeysNotFound Then
            Windows.Forms.MessageBox.Show("There was an error in the template. Not all data was exported correctly." & vbCrLf & "Please mail the campaign file and the template you are using to your system administrator.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        Exit Sub

ErrHandle:
        If Err.GetException.TargetSite.Name = "ThrowKeyNotFoundException" Then
            AllKeysNotFound = True
            'Resume Next
        End If
        Err.Raise(Err.Number, Err.Source, Err.Description & " spot number" & currentSpot)
        Resume Next
    End Sub
    'Sub ExportPostAnalysis(ByVal Excel As Object)
    '    Dim i As Integer

    '    'pbStatus.Value = 0
    '    'pbStatus.Visible = True

    '    Dim Vars As New Dictionary(Of String, Object)

    '    With Excel.Sheets("Setup")
    '        Dim r As Integer = 3
    '        While Not .cells(r, 2).Value Is Nothing
    '            Vars.Add(.cells(r, 2).value, .cells(r, 3).value)
    '            r += 1
    '        End While
    '    End With

    '    If Campaign.Channels(1).BookingTypes(1).Weeks.Count > Vars("Weeks") Then
    '        Windows.Forms.MessageBox.Show("The template you are trying to use is adjusted for " & Vars("Weeks") & " weeks" & vbCrLf & "and this campaign has " & Campaign.Channels(1).BookingTypes(1).Weeks.Count & " weeks, wich may cause errors in the plan.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
    '    End If

    '    With Excel.Sheets(Vars("ActualSheet").ToString)
    '        .range("B3").Value = Campaign.Name
    '        .range("B4").value = Campaign.Client
    '        .range("B5").value = Campaign.Product
    '        Dim StartWeek As Integer = DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
    '        Dim EndWeek As Integer = DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
    '        Dim PeriodStr As String = StartWeek
    '        If EndWeek > StartWeek Then PeriodStr = PeriodStr & " - " & EndWeek
    '        PeriodStr = PeriodStr & ", " & Format(Date.FromOADate(Campaign.StartDate), "yyyy")
    '        .Range("B6").value = PeriodStr
    '        .Range("B7").value = StartWeek
    '        .Range("B8").value = EndWeek
    '        .Range("B9").value = Campaign.StartDate
    '        .Range("B10").value = Campaign.EndDate
    '        .Range("E3").value = Campaign.Buyer
    '        .Range("E4").value = Campaign.Planner
    '        If Campaign.Adedge.validate > 0 Then
    '            Campaign.Adedge.setPeriod("-1d")
    '            Campaign.Adedge.Run()
    '        End If

    '        .Range("D6").Numberformat = "@"
    '        .Range("D6").FORMULA = Campaign.MainTarget.TargetNameNice
    '        .Range("E6").value = Campaign.MainTarget.UniSizeTot * 1000
    '        .Range("F6").value = Campaign.MainTarget.UniSizeSec * 1000

    '        .Range("D7").NUMBERFORMAT = "@"
    '        .Range("D7").FORMULA = Campaign.SecondaryTarget.TargetNameNice
    '        .Range("E7").value = Campaign.SecondaryTarget.UniSizeTot * 1000
    '        .Range("F7").value = Campaign.SecondaryTarget.UniSizeSec * 1000

    '        .Range("D8").NUMBERFORMAT = "@"
    '        .Range("D8").FORMULA = Campaign.ThirdTarget.TargetNameNice
    '        .Range("E8").value = Campaign.ThirdTarget.UniSizeTot * 1000
    '        .Range("F8").value = Campaign.ThirdTarget.UniSizeSec * 1000

    '        .Range("E9").value = Campaign.Adedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , , Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge) - 1) * 1000
    '        .Range("F9").value = Campaign.Adedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , Campaign.UniColl(Campaign.MainTarget.SecondUniverse) - 1, Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge) - 1) * 1000

    '        Campaign.Adedge.Run(False, , 10)

    '        For i = 1 To 10
    '            ' + 0 is needed to avoid the error "Ogiltig token". No idea why, but might have to do with type casting
    '            .cells(Vars("ActualReachRow") + i - 1, Vars("ActualReachCol") + 0).value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, 0, Campaign.TimeShift, Campaign.TargColl(Campaign.SecondaryTarget.TargetName, Campaign.Adedge), i)
    '            .cells(Vars("ActualReachRow") + i - 1, Vars("ActualReachCol") + 1).value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, 0, Campaign.TimeShift, Campaign.TargColl(Campaign.SecondaryTarget.TargetName, Campaign.Adedge), i)
    '            .cells(Vars("ActualReachRow") + i - 1, Vars("ActualReachCol") + 2).value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, 0, Campaign.TimeShift, Campaign.TargColl(Campaign.ThirdTarget.TargetName, Campaign.Adedge), i)
    '            .cells(Vars("ActualReachRow") + i - 1, Vars("ActualReachCol") + 3).value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, 0, Campaign.TimeShift, Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge), i)
    '        Next

    '        .cells(Vars("ActualSummaryRow") + Vars("Channels") - 1, Vars("ActualSummaryCol") + 3).value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1)
    '        .cells(Vars("ActualSummaryRow") + Vars("Channels") - 1, Vars("ActualSummaryCol") + 6).value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1)
    '        .cells(Vars("ActualSummaryRow") + Vars("Channels") - 1, Vars("ActualSummaryCol") + 9).value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1)
    '        .cells(Vars("ActualSummaryRow") + Vars("Channels") - 1, Vars("ActualSummaryCol") + 12).value = Campaign.Adedge.getRF(Campaign.Adedge.getGroupCount - 1, , Campaign.TimeShift, Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1)

    '        i = 0
    '        Dim r As Integer = Vars("ActualReachBuildupRow")
    '        Dim c As Integer = Vars("ActualReachBuildupCol")
    '        Dim next10 As Integer = 10
    '        Dim TRPSum As Single = 0
    '        For s As Integer = 0 To Campaign.ActualSpots.Count - 1
    '            TRPSum = TRPSum + Campaign.Adedge.getUnit(Connect.eUnits.uTRP, s, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1)
    '            If TRPSum >= next10 Then
    '                .cells(r, c) = Campaign.Adedge.getRF(s, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, 1) / 100
    '                .cells(r, c + 3) = Campaign.Adedge.getRF(s, , Campaign.TimeShift, Campaign.TargColl(Campaign.MainTarget.TargetName, Campaign.Adedge) - 1, Campaign.FrequencyFocus + 1) / 100
    '                r = r + 1
    '                next10 = next10 + 10
    '            End If
    '        Next

    '    End With
    'End Sub

    Sub ExportNewPreAnalysis(ByVal Excel As CultureSafeExcel.Application, ByVal includeCompensations As Boolean, ByVal singleRowCombinations As Boolean, Optional ByVal ProgressWindow As frmProgress = Nothing)


        Debug.WriteLine("ExportPREAnalysis START")
        Debug.WriteLine(Campaign.Adedge.debug)
        Debug.WriteLine("groupcount:" & Campaign.Adedge.getGroupCount)

        Dim ChannelCount As Integer = 0
        Dim BTCount As Integer = 0
        Dim p As Integer

        'Excel.Screenupdating = True
        'Read the variables
        'Excel.visible = True
        Dim Vars As New Dictionary(Of String, Object)
        Dim rowPlus As Integer = 0

        If Not ProgressWindow Is Nothing Then
            ProgressWindow.Status = "Creating pre-campaign analysis..."
        End If
        With Excel.Sheets("Setup")
            Dim r As Integer = 3
            While Not .cells(r, 2).Value Is Nothing
                Vars.Add(.cells(r, 2).value, .cells(r, 3).value)
                r += 1
            End While
        End With

        If Campaign.Channels(1).BookingTypes(1).Weeks.Count > Vars("Weeks") Then
            Windows.Forms.MessageBox.Show("The template you are trying to use is adjusted for " & Vars("Weeks") & " weeks" & vbCrLf & "and this campaign has " & Campaign.Channels(1).BookingTypes(1).Weeks.Count & " weeks, wich may cause errors in the plan.", "T R I N I T Y", MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        End If

        If singleRowCombinations Then
            For Each TmpChan As Trinity.cChannel In Campaign.Channels
                Dim BTCountThis As Integer = 0
                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                    If TmpBT.BookIt AndAlso TmpBT.ShowMe Then
                        If BTCountThis = 0 Then
                            ChannelCount += 1
                        End If
                        BTCountThis += 1
                    End If
                Next
                If BTCountThis > BTCount Then BTCount = BTCountThis
            Next
            For Each c As Trinity.cCombination In Campaign.Combinations
                If c.ShowAsOne Then
                    ChannelCount += 1
                End If
            Next
        Else
            For Each TmpChan As Trinity.cChannel In Campaign.Channels
                Dim BTCountThis As Integer = 0
                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                    If TmpBT.BookIt Then
                        If BTCountThis = 0 Then
                            ChannelCount += 1
                        End If
                        BTCountThis += 1
                    End If
                Next
                If BTCountThis > BTCount Then BTCount = BTCountThis
            Next
        End If

        If ChannelCount > Vars("Channels") Then
            Windows.Forms.MessageBox.Show("The template you are trying to use is adjusted for " & Vars("Channels") & " channels" & vbCrLf & "and this campaign has " & ChannelCount & " channels, wich may cause errors in the plan.", "T R I N I T Y", MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        End If
        If BTCount > Vars("Bookingtypes") Then
            Windows.Forms.MessageBox.Show("The template you are trying to use is adjusted for " & Vars("Bookingtypes") & " bookingtypes" & vbCrLf & "and this campaign has " & BTCount & " bookingtypes, wich may cause errors in the plan.", "T R I N I T Y", MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        End If
        With Excel.Sheets(Vars("PlanSheet").ToString)
            .range("B3").Value = Campaign.Name
            .range("B4").value = Campaign.Client
            .range("B5").value = Campaign.Product
            Dim StartWeek As Integer = DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
            Dim EndWeek As Integer = DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
            Dim PeriodStr As String = StartWeek
            If EndWeek > StartWeek Then PeriodStr = PeriodStr & " - " & EndWeek
            PeriodStr = PeriodStr & ", " & Format(Date.FromOADate(Campaign.StartDate), "yyyy")
            .Range("B6").value = PeriodStr
            .Range("B7").value = StartWeek
            .Range("B8").value = EndWeek
            .Range("B9").value = Campaign.StartDate
            .Range("B10").value = Campaign.EndDate
            .Range("E3").value = Campaign.Buyer
            .Range("G3").value = Campaign.BuyerEmail
            .Range("I3").value = Campaign.BuyerPhone
            .Range("E4").value = Campaign.Planner
            .Range("G4").value = Campaign.PlannerEmail
            .Range("I4").value = Campaign.PlannerPhone
            'add the Emails and phonenumbers aswell

            If Campaign.Adedge.validate > 0 Then
                Campaign.Adedge.setPeriod("-1d")
                Campaign.Adedge.setChannelsArea(Campaign.ChannelString, Campaign.Area)
                Campaign.Adedge.setArea(Campaign.Area)
                'Campaign.Adedge.Run()
            End If
            Trinity.Helper.AddTargetsToAdedge(Campaign.Adedge, True)

            .Range("D6").Numberformat = "@"
            .Range("D6").FORMULA = Campaign.MainTarget.TargetNameNice
            .Range("E6").value = Campaign.MainTarget.UniSize * 1000
            ' .Range("F6").value = Campaign.MainTarget.UniSizeSec * 1000

            .Range("D7").NUMBERFORMAT = "@"
            .Range("D7").FORMULA = Campaign.SecondaryTarget.TargetNameNice
            .Range("E7").value = Campaign.SecondaryTarget.UniSize * 1000
            '  .Range("F7").value = Campaign.SecondaryTarget.UniSizeSec * 1000

            .Range("D8").NUMBERFORMAT = "@"
            .Range("D8").FORMULA = Campaign.ThirdTarget.TargetNameNice
            .Range("E8").value = Campaign.ThirdTarget.UniSize * 1000
            '  .Range("F8").value = Campaign.ThirdTarget.UniSizeSec * 1000

            .Range("E9").value = Campaign.Adedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , , Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge) - 1) * 1000
            '.Range("F9").value = Campaign.Adedge.getUniSampleInfo(Connect.eSample.mUniSize, 0, , Campaign.UniColl(Campaign.MainTarget.SecondUniverse) - 1, Campaign.TargColl(Campaign.AllAdults, Campaign.Adedge) - 1) * 1000


            .cells(Val(Vars("PlanFFRow")), Val(Vars("PlanFFCol"))).value = (Campaign.FrequencyFocus + 1) & "+"
            .cells(Val(Vars("PlanFFRow")), Val(Vars("PlanFFCol")) + 1).value = Campaign.ReachGoal(Campaign.FrequencyFocus + 1)

            'Prints the numbers for 1+ to 10+ reach
            For i As Integer = 1 To 10
                .cells(Val(Vars("PlanReachRow")), Val(Vars("PlanReachCol")) + i - 1).value = Campaign.ReachGoal(i)
            Next

            'Done printing the "static" facts about the campaign

            'print the films (Film description, film name, film length
            Dim Film As Integer = 0
            For Each TmpFilm As Trinity.cFilm In Campaign.Channels(1).BookingTypes(1).Weeks(1).Films
                .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol"))).value = TmpFilm.Name
                .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 1).value = TmpFilm.Description
                .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 2).value = TmpFilm.FilmLength
                Film = Film + 1
            Next
            Dim Chan As Integer = -1

            '-----------------------------------------------------------------------------
            'The CHANNEL part of the for loop
            '-----------------------------------------------------------------------------

            Dim TRPweekList As New Collection
            Dim TRPfilmList As New Collection

            For Each TmpChan As Trinity.cChannel In Campaign.Channels
                Dim BT As Integer = -1


                Dim TopRow As Integer = Val(Vars("PlanChannelSumsRow")) + ((Chan + 1) * (Val(Vars("Bookingtypes")) + 2))
                If Not ProgressWindow Is Nothing Then
                    p += 1
                    ProgressWindow.Status = "Creating pre-campaign analysis... " & TmpChan.ChannelName
                    ProgressWindow.Progress = (p / Campaign.Channels.Count) * 100
                End If

                If Vars.ContainsKey("PlanCostPerFilmCodeRow") Then
                    If Vars("PlanCostPerFilmcodeRow") > 0 And Vars("PlanCostPerFilmcodeCol") > 0 Then
                        .cells(Vars("PlanCostPerFilmcodeRow") + Chan, Vars("PlanCostPerFilmcodeCol")).value = TmpChan.Shortname
                    End If
                End If
                '-----------------------------------------------------------------------------
                'The BOOKING TYPE part of the for loop
                '-----------------------------------------------------------------------------

                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                    '  (singleRowCombinations AndAlso Not TmpBT.ShowMe) return table
                    'singleRowCombinations      TmpBT.ShowMe        Returns
                    'True                       True                True
                    'True                       False               False    
                    'False                      True                True
                    'False                      False               True

                    Dim filmCodeCollection As New Dictionary(Of String, Double)

                    If TmpBT.BookIt AndAlso Not (singleRowCombinations AndAlso Not TmpBT.ShowMe) Then

                        If BT = -1 Then
                            Chan = Chan + 1
                            BT = 0
                        End If
                        If BT < Vars("Bookingtypes") Then
                            .cells(TopRow, 1).value = TmpChan.ChannelName
                            .cells(TopRow + Val(Vars("Bookingtypes")), 1).value = TmpChan.Shortname
                            .cells(TopRow + BT, 2).value = TmpBT.Name

                            'This bit prints out the net cost per film code by channel and booking type
                            If Vars.ContainsKey("PlanCostPerFilmcodeCol") Then
                                .cells(Vars("PlanCostPerFilmcodeRow") + Chan + BT + rowPlus, Vars("PlanCostPerFilmcodeCol")).value = TmpBT.ParentChannel.ChannelName
                                .cells(Vars("PlanCostPerFilmcodeRow") + Chan + BT + rowPlus, Vars("PlanCostPerFilmcodeCol") + 1).value = TmpBT.Shortname

                                If Not TmpBT.IsSpecific Then
                                    'If the booking type is not specifics, we calculate the cost by the film's ratings, share, index, and the week's net CPP30
                                    Dim tmpBudget As Decimal = 0

                                    For Each tmpWeek As Trinity.cWeek In TmpBT.Weeks
                                        For Each tmpFilm As Trinity.cFilm In tmpWeek.Films
                                            If Not filmCodeCollection.ContainsKey(tmpFilm.Filmcode) Then
                                                filmCodeCollection.Add(tmpFilm.Filmcode, 0)
                                                filmCodeCollection(tmpFilm.Filmcode) += tmpWeek.TRPBuyingTarget * (tmpFilm.Share / 100) * (tmpFilm.Index / 100) * tmpWeek.NetCPP30
                                            Else
                                                filmCodeCollection(tmpFilm.Filmcode) += tmpWeek.TRPBuyingTarget * (tmpFilm.Share / 100) * (tmpFilm.Index / 100) * tmpWeek.NetCPP30
                                            End If
                                        Next
                                    Next
                                    For Each tmpFilmcode As KeyValuePair(Of String, Double) In filmCodeCollection
                                        .cells(Vars("PlanCostPerFilmcodeRow") + Chan + BT + rowPlus, Vars("PlanCostPerFilmcodeCol") + 2).value = tmpFilmcode.Key
                                        .cells(Vars("PlanCostPerFilmcodeRow") + Chan + BT + rowPlus, Vars("PlanCostPerFilmcodeCol") + 3).value = tmpFilmcode.Value
                                        rowPlus += 1
                                    Next

                                Else
                                    'If the booking type is specific, we just go through all spots and get their cost
                                    For Each BookedSpot As Trinity.cBookedSpot In Campaign.BookedSpots

                                        If BookedSpot.Channel.ChannelName = TmpChan.ChannelName Then
                                            If BookedSpot.Bookingtype.Name = TmpBT.Name Then
                                                If Not filmCodeCollection.ContainsKey(BookedSpot.Filmcode) Then
                                                    filmCodeCollection.Add(BookedSpot.Filmcode, 0)
                                                    filmCodeCollection(BookedSpot.Filmcode) += BookedSpot.NetPrice
                                                Else
                                                    filmCodeCollection(BookedSpot.Filmcode) += BookedSpot.NetPrice
                                                End If
                                            End If

                                        End If

                                    Next
                                    'Make sure there is a space between one booking type and the next even if the BT has no films
                                    If filmCodeCollection.Count = 0 Then
                                        rowPlus += 1
                                    Else
                                        'Print the film codes and costs
                                        For Each tmpFilmcode As KeyValuePair(Of String, Double) In filmCodeCollection
                                            .cells(Vars("PlanCostPerFilmcodeRow") + Chan + BT + rowPlus, Vars("PlanCostPerFilmcodeCol") + 2).value = tmpFilmcode.Key
                                            .cells(Vars("PlanCostPerFilmcodeRow") + Chan + BT + rowPlus, Vars("PlanCostPerFilmcodeCol") + 3).value = tmpFilmcode.Value
                                            rowPlus += 1
                                        Next
                                    End If
                                End If
                            End If

                            'If TmpBT.ConfirmedSpotCount > 0 Then
                            '    .cells(TopRow + BT, Val(Vars("PlanSpotsCol"))).value = TmpBT.ConfirmedSpotCount
                            'Else
                            .cells(TopRow + BT, Val(Vars("PlanSpotsCol"))).value = TmpBT.EstimatedSpotCount
                            'End If
                            If TmpBT.ConfirmedNetBudget > 0 Then
                                .Cells(TopRow + BT, 3).Value = TmpBT.ConfirmedGrossBudget
                                .Cells(TopRow + BT, 4).Value = TmpBT.ConfirmedNetBudget
                            Else
                                .Cells(TopRow + BT, 3).Value = TmpBT.PlannedGrossBudget
                                .Cells(TopRow + BT, 4).Value = TmpBT.PlannedNetBudget
                            End If
                            .cells(TopRow + BT, Val(Vars("PlanChannelSumsCol"))).value = TmpBT.BuyingTarget.TargetName
                            .cells(TopRow + BT, Val(Vars("PlanChannelSumsCol")) + 3).value = TmpBT.BuyingTarget.NetCPT

                            'Hannes added
                            'Prints target indexes for the booking type
                            If Vars.ContainsKey("TargetIndexesCol") Then
                                .cells(TopRow + BT, Val(Vars("TargetIndexesCol"))).value = TmpBT.IndexMainTarget
                            End If

                            'Hannes added
                            'Prints the commissions for each booking type individually if the variable PlanMediaNetRowDetailed is set
                            'This was requested because it's possible that different channels have different commissions
                            If Vars.ContainsKey("PlanMediaNetRowDetailed") Then
                                .cells(Vars("PlanMediaNetRowDetailed") + Chan + BT + 1, Vars("PlanMediaNetColDetailed") + 0).value = TmpBT.ParentChannel.Shortname & " " & TmpBT.Name
                                .cells(Vars("PlanMediaNetRowDetailed") + Chan + BT + 1, Vars("PlanMediaNetColDetailed") + 1).value = TmpBT.ParentChannel.AgencyCommission * -1
                                .cells(Vars("PlanMediaNetRowDetailed") + Chan + BT + 1, Vars("PlanMediaNetColDetailed") + 2).value = TmpBT.PlannedNetBudget * TmpBT.ParentChannel.AgencyCommission * -1
                            End If

                            'Hannes added
                            'Prints CPP30 for each booking type
                            If Vars.ContainsKey("BookingTypeCPP30Col") Then
                                .cells(TopRow + BT, Val(Vars("BookingTypeCPP30Col"))).value = TmpBT.PlannedNetBudget / TmpBT.PlannedTRP30(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget)
                            End If

                            Dim Week As Integer = 0

                            If Vars.ContainsKey("PlanNaturalDeliveryRow") AndAlso Vars.ContainsKey("PlanNaturalDeliveryCol") Then
                                .cells(Vars("PlanNaturalDeliveryRow") + Chan + BT + 1, Vars("PlanNaturalDeliveryCol") + 0).value = TmpBT.ParentChannel.Shortname & " " & TmpBT.Name
                                .cells(Vars("PlanNaturalDeliveryRow") + Chan + BT + 1, Vars("PlanNaturalDeliveryCol") + 1).value = TmpBT.EstimatedSpotCount
                                .cells(Vars("PlanNaturalDeliveryRow") + Chan + BT + 1, Vars("PlanNaturalDeliveryCol") + 2).value = TmpBT.AverageRating
                            End If

                            '-----------------------------------------------------------------------------
                            'The WEEK part of the for loop
                            '-----------------------------------------------------------------------------

                            For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks


                                Dim TRPcomp As Double = 0
                                If Not TmpBT.Compensations.Count = 0 AndAlso includeCompensations Then

                                    For Each c As Trinity.cCompensation In TmpBT.Compensations

                                        Dim countActualDays As Integer = 0
                                        'count the actual days the campaign will run in the compensation
                                        For Each w As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                                            For day As Integer = w.StartDate To w.EndDate
                                                If day >= c.FromDate.ToOADate AndAlso day <= c.ToDate.ToOADate Then
                                                    countActualDays += 1
                                                End If
                                            Next
                                        Next
                                        If countActualDays <> 0 Then
                                            If TmpWeek.StartDate >= c.FromDate.ToOADate AndAlso TmpWeek.EndDate <= c.ToDate.ToOADate Then
                                                TRPcomp += (TmpWeek.EndDate - TmpWeek.StartDate + 1) / countActualDays * (c.TRPs * (TmpBT.IndexMainTarget / 100))
                                            Else
                                                Dim i As Integer
                                                Dim count As Integer = 0
                                                For i = TmpWeek.StartDate To TmpWeek.EndDate
                                                    If i >= c.FromDate.ToOADate AndAlso i <= c.ToDate.ToOADate Then
                                                        count += 1
                                                    End If
                                                Next
                                                TRPcomp += count / countActualDays * (c.TRPs * (TmpBT.IndexMainTarget / 100))
                                            End If
                                        End If
                                    Next
                                End If

                                .cells(TopRow - 1, Val(Vars("PlanChannelWeekCol")) + Week).numberformat = "@"
                                .cells(TopRow - 1, Val(Vars("PlanChannelWeekCol")) + Week).value = TmpWeek.Name
                                .cells(TopRow + BT, Val(Vars("PlanChannelWeekCol")) + Week).value = TmpWeek.TRPBuyingTarget
                                .cells(TopRow - 1, Val(Vars("PlanNationalWeekCol")) + Week).value = TmpWeek.Name
                                .cells(TopRow + BT, Val(Vars("PlanNationalWeekCol")) + Week).value = TmpWeek.TRP + TRPcomp
                                Film = 0


                                For Each TmpFilm As Trinity.cFilm In TmpWeek.Films
                                    'TRP
                                    If .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 4 + Chan).value Is Nothing Then
                                        .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 4 + Chan).value = 0
                                    End If
                                    .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 4 + Chan).value += ((TmpWeek.TRP + TRPcomp) * (TmpFilm.Share / 100))

                                    'TRP 30"
                                    If .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 4 + Chan + Val(Vars("Channels"))).value Is Nothing Then
                                        .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 4 + Chan + Val(Vars("Channels"))).value = 0
                                    End If
                                    .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 4 + Chan + Val(Vars("Channels"))).value += ((TmpWeek.TRP + TRPcomp) * (TmpFilm.Share / 100) * (TmpFilm.Index / 100))

                                    'TRP per film per week
                                    If .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 4 + Val(Vars("Channels")) * 2).value Is Nothing Then
                                        .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 4 + Val(Vars("Channels")) * 2 + Week).value = 0
                                    End If
                                    .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 4 + Val(Vars("Channels")) * 2 + Week).value += (TmpWeek.TRP + TRPcomp) * (TmpFilm.Share / 100)

                                    Film += 1
                                Next
                                For i As Integer = 0 To Campaign.Dayparts.Count - 1
                                    If .cells(Val(Vars("PlanDaypartRow")) + Chan, Val(Vars("PlanDaypartCol")) + 1 + i).value Is Nothing Then
                                        .cells(Val(Vars("PlanDaypartRow")) + Chan, Val(Vars("PlanDaypartCol")) + 1 + i).value = 0
                                    End If
                                    .cells(Val(Vars("PlanDaypartRow")) + Chan, Val(Vars("PlanDaypartCol")) + 1 + i).value = .cells(Val(Vars("PlanDaypartRow")) + Chan, Val(Vars("PlanDaypartCol")) + 1 + i).value + (TmpWeek.TRP + TRPcomp) * (TmpBT.MainDaypartSplit(i) / 100)
                                Next
                                Week += 1
                            Next
                            'Done printing the things that are different per week in the bookingtype in this channel

                        End If

                        BT += 1
                    End If

                    'This next loops the next booking type
                Next
                'This next loops the next channel
            Next

            'print weekly reach
            If Vars.ContainsKey("PlanWeeklyReachCol") Then
                Dim Row As Integer
                Row = Vars("PlanWeeklyReachRow")
                For i As Integer = 0 To Campaign.Channels(1).BookingTypes(1).Weeks.Count - 1
                    .Cells(Row, Vars("PlanWeeklyReachCol") + i).Value = Campaign.EstimatedWeeklyReach(Campaign.Channels(1).BookingTypes(1).Weeks(i + 1).Name)
                Next
            Else
                Dim Row As Integer = Vars("PlanChannelSumsRow") + Vars("Channels") * (Vars("Bookingtypes") + 2)
                For i As Integer = 0 To Campaign.Channels(1).BookingTypes(1).Weeks.Count - 1
                    .Cells(Row, Vars("PlanNationalWeekCol") + i).Value = Campaign.EstimatedWeeklyReach(Campaign.Channels(1).BookingTypes(1).Weeks(i + 1).Name)
                Next
            End If

            'the above for loop will print everything except the one lined allocation bookings in the 
            'singleRowCombinations is set to true, so we print them here
            If singleRowCombinations Then
                For Each c As Trinity.cCombination In Campaign.Combinations
                    If c.ShowAsOne Then
                        Dim BT As Integer = -1
                        Dim TopRow As Integer = Val(Vars("PlanChannelSumsRow")) + ((Chan + 1) * (Val(Vars("Bookingtypes")) + 2))
                        For Each cc As Trinity.cCombinationChannel In c.Relations
                            If BT = -1 Then
                                Chan = Chan + 1
                                BT = 0
                            End If
                            If BT < Vars("Bookingtypes") Then
                                .Cells(TopRow, 1).Value = c.Name
                                .Cells(TopRow + Val(Vars("Bookingtypes")), 1).Value = c.Name
                                .Cells(TopRow + BT, 2).Value = cc.Bookingtype.ParentChannel.Shortname & " " & cc.Bookingtype.Name
                                .cells(TopRow + BT, Val(Vars("PlanSpotsCol"))).value = cc.Bookingtype.EstimatedSpotCount
                                'End If
                                If cc.Bookingtype.ConfirmedNetBudget > 0 Then
                                    .Cells(TopRow + BT, 3).Value = cc.Bookingtype.ConfirmedGrossBudget
                                    .Cells(TopRow + BT, 4).Value = cc.Bookingtype.ConfirmedNetBudget
                                Else
                                    .Cells(TopRow + BT, 3).Value = cc.Bookingtype.PlannedGrossBudget
                                    .Cells(TopRow + BT, 4).Value = cc.Bookingtype.PlannedNetBudget
                                End If
                                .cells(TopRow + BT, Val(Vars("PlanChannelSumsCol"))).value = cc.Bookingtype.BuyingTarget.TargetName
                                .cells(TopRow + BT, Val(Vars("PlanChannelSumsCol")) + 3).value = cc.Bookingtype.BuyingTarget.NetCPT
                                Dim Week As Integer = 0
                                For Each TmpWeek As Trinity.cWeek In cc.Bookingtype.Weeks
                                    Dim TRPcomp As Double = 0
                                    If Not cc.Bookingtype.Compensations.Count = 0 AndAlso includeCompensations Then
                                        For Each comp As Trinity.cCompensation In cc.Bookingtype.Compensations
                                            Dim countActualDays As Integer = 0
                                            'count the actual days the campaign will run in the compensation
                                            For Each w As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                                                For day As Integer = w.StartDate To w.EndDate
                                                    If day >= comp.FromDate.ToOADate AndAlso day <= comp.ToDate.ToOADate Then
                                                        countActualDays += 1
                                                    End If
                                                Next
                                            Next

                                            If TmpWeek.StartDate >= comp.FromDate.ToOADate AndAlso TmpWeek.EndDate <= comp.ToDate.ToOADate Then
                                                TRPcomp += (TmpWeek.EndDate - TmpWeek.StartDate + 1) / countActualDays * comp.TRPs
                                            Else
                                                Dim i As Integer
                                                Dim count As Integer = 0
                                                For i = TmpWeek.StartDate To TmpWeek.EndDate
                                                    If i >= comp.FromDate.ToOADate AndAlso i <= comp.ToDate.ToOADate Then
                                                        count += 1
                                                    End If
                                                Next
                                                TRPcomp += count / countActualDays * comp.TRPs
                                            End If
                                        Next
                                    End If

                                    .cells(TopRow - 1, Val(Vars("PlanChannelWeekCol")) + Week).numberformat = "@"
                                    .cells(TopRow - 1, Val(Vars("PlanChannelWeekCol")) + Week).value = TmpWeek.Name
                                    .cells(TopRow + BT, Val(Vars("PlanChannelWeekCol")) + Week).value = TmpWeek.TRPBuyingTarget
                                    .cells(TopRow - 1, Val(Vars("PlanNationalWeekCol")) + Week).value = TmpWeek.Name
                                    .cells(TopRow + BT, Val(Vars("PlanNationalWeekCol")) + Week).value = TmpWeek.TRP + TRPcomp
                                    Film = 0
                                    For Each TmpFilm As Trinity.cFilm In TmpWeek.Films
                                        'TRP
                                        If .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 4 + Chan).value Is Nothing Then
                                            .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 4 + Chan).value = 0
                                        End If
                                        .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 4 + Chan).value += ((TmpWeek.TRP + TRPcomp) * (TmpFilm.Share / 100))

                                        'TRP 30"
                                        If .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 4 + Chan + Val(Vars("Channels"))).value Is Nothing Then
                                            .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 4 + Chan + Val(Vars("Channels"))).value = 0
                                        End If
                                        .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 4 + Chan + Val(Vars("Channels"))).value += ((TmpWeek.TRP + TRPcomp) * (TmpFilm.Share / 100) * (TmpFilm.Index / 100))

                                        'TRP per film per week
                                        If .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 4 + Val(Vars("Channels")) * 2).value Is Nothing Then
                                            .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 4 + Val(Vars("Channels")) * 2 + Week).value = 0
                                        End If
                                        .cells(Val(Vars("PlanFilmcodeRow")) + Film, Val(Vars("PlanFilmcodeCol")) + 4 + Val(Vars("Channels")) * 2 + Week).value += (TmpWeek.TRP + TRPcomp) * (TmpFilm.Share / 100)

                                        Film += 1
                                    Next
                                    For i As Integer = 0 To Campaign.Dayparts.Count - 1
                                        If .cells(Val(Vars("PlanDaypartRow")) + Chan, Val(Vars("PlanDaypartCol")) + 1 + i).value Is Nothing Then
                                            .cells(Val(Vars("PlanDaypartRow")) + Chan, Val(Vars("PlanDaypartCol")) + 1 + i).value = 0
                                        End If
                                        .cells(Val(Vars("PlanDaypartRow")) + Chan, Val(Vars("PlanDaypartCol")) + 1 + i).value = .cells(Val(Vars("PlanDaypartRow")) + Chan, Val(Vars("PlanDaypartCol")) + 1 + i).value + (TmpWeek.TRP + TRPcomp) * (cc.Bookingtype.MainDaypartSplit(i) / 100)
                                    Next
                                    Week += 1
                                Next
                            End If
                            BT += 1
                        Next
                    End If
                Next
            End If

            '-----------------------------------------------------------------------------
            'We print the summary of net, net-net and CTC from here on
            '-----------------------------------------------------------------------------

            '.cells(Val(Vars("PlanMediaNetRow")), Val(Vars("PlanMediaNetCol")) + 2).value = Campaign.PlannedMediaNet
            Dim r As Integer = 2
            .Cells(Val(Vars("PlanMediaNetRow")) + 1, Val(Vars("PlanMediaNetCol"))).Value = "Commission"
            If Campaign.PlannedMediaNet = 0 Then
                .Cells(Val(Vars("PlanMediaNetRow")) + 1, Val(Vars("PlanMediaNetCol")) + 1).Value = "0,0%"
            Else
                .Cells(Val(Vars("PlanMediaNetRow")) + 1, Val(Vars("PlanMediaNetCol")) + 1).Value = -(Campaign.EstimatedCommission / Campaign.PlannedMediaNet)
            End If
            .Cells(Val(Vars("PlanMediaNetRow")) + 1, Val(Vars("PlanMediaNetCol")) + 2).Value = -Campaign.EstimatedCommission

            'The costs as a percentage of media net, easy bit
            For Each TmpCost As Trinity.cCost In Campaign.Costs
                If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                    If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnMediaNet Then
                        .Cells(Val(Vars("PlanMediaNetRow")) + r, Val(Vars("PlanMediaNetCol"))).Value = TmpCost.CostName
                        .Cells(Val(Vars("PlanMediaNetRow")) + r, Val(Vars("PlanMediaNetCol")) + 1).Value = TmpCost.Amount
                        .Cells(Val(Vars("PlanMediaNetRow")) + r, Val(Vars("PlanMediaNetCol")) + 2).Value = TmpCost.Amount * Campaign.PlannedMediaNet
                        r = r + 1
                    End If
                End If
            Next


            r = 0
            r = r + 1
            Dim UnitStr As String = ""
            'If 
            For Each TmpCost As Trinity.cCost In Campaign.Costs
                If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                    If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNet Then
                        .Cells(Val(Vars("PlanNetRow")) + r, Val(Vars("PlanNetCol")) + 0).Value = TmpCost.CostName
                        .cells(Val(Vars("PlanNetRow")) + r, Val(Vars("PlanNetCol")) + 1).NumberFormat = "##,##0.0%"
                        .Cells(Val(Vars("PlanNetRow")) + r, Val(Vars("PlanNetCol")) + 1).Value = TmpCost.Amount
                        .Cells(Val(Vars("PlanNetRow")) + r, Val(Vars("PlanNetCol")) + 2).Value = TmpCost.Amount * Campaign.PlannedNet
                        r = r + 1
                    End If
                ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeFixed Then
                    .cells(Val(Vars("PlanNetRow")) + r, Val(Vars("PlanNetCol")) + 0).Value = TmpCost.CostName
                    .cells(Val(Vars("PlanNetRow")) + r, Val(Vars("PlanNetCol")) + 2).Value = TmpCost.Amount
                    r = r + 1
                ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeOnDiscount Then
                    Dim SumUnit As Single = 0
                    If TmpCost.CountCostOn Is Nothing Then
                        For Each TmpChan As Trinity.cChannel In Campaign.Channels
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
                    .Cells(Val(Vars("PlanNetRow")) + r, Val(Vars("PlanNetCol")) + 0).Value = TmpCost.CostName
                    .cells(Val(Vars("PlanNetRow")) + r, Val(Vars("PlanNetCol")) + 1).NumberFormat = "##,##0.0%"
                    .Cells(Val(Vars("PlanNetRow")) + r, Val(Vars("PlanNetCol")) + 1).Value = TmpCost.Amount
                    .Cells(Val(Vars("PlanNetRow")) + r, Val(Vars("PlanNetCol")) + 2).Value = SumUnit
                    r = r + 1

                ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePerUnit Then
                    Dim SumUnit As Single = 0
                    If TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnSpots Then
                        For Each TmpChan As Trinity.cChannel In Campaign.Channels
                            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                                If TmpBT.BookIt = True Then
                                    SumUnit = SumUnit + TmpBT.EstimatedSpotCount * TmpCost.Amount
                                End If
                            Next
                        Next
                        UnitStr = " / Spot"
                    ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnBuyingTRP Then
                        For Each TmpChan As Trinity.cChannel In Campaign.Channels
                            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                                SumUnit = SumUnit + TmpBT.TotalTRPBuyingTarget * TmpCost.Amount
                            Next
                        Next
                        UnitStr = " / TRP"
                    ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnBuyingTRP Then
                        For Each TmpChan As Trinity.cChannel In Campaign.Channels
                            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                                SumUnit = SumUnit + TmpBT.TotalTRP * TmpCost.Amount
                            Next
                        Next
                        UnitStr = " / TRP"
                    End If
                    .cells(Val(Vars("PlanNetRow")) + r, Val(Vars("PlanNetCol")) + 0).Value = TmpCost.CostName
                    .cells(Val(Vars("PlanNetRow")) + r, Val(Vars("PlanNetCol")) + 1).NumberFormat = "##,##0 kr"
                    .cells(Val(Vars("PlanNetRow")) + r, Val(Vars("PlanNetCol")) + 1).Value = TmpCost.Amount
                    .cells(Val(Vars("PlanNetRow")) + r, Val(Vars("PlanNetCol")) + 2).Value = SumUnit
                    r = r + 1
                End If
            Next
            r = 0
            '.cells(Val(Vars("PlanNetNetRow")) + r, Val(Vars("PlanNetNetCol")) + 2).Value = Campaign.PlannedNetNet
            r = r + 1
            For Each TmpCost As Trinity.cCost In Campaign.Costs
                If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                    If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNetNet Then
                        .cells(Val(Vars("PlanNetNetRow")) + r, Val(Vars("PlanNetNetCol")) + 0).Value = TmpCost.CostName
                        .cells(Val(Vars("PlanNetNetRow")) + r, Val(Vars("PlanNetNetCol")) + 1).Value = TmpCost.Amount
                        .cells(Val(Vars("PlanNetNetRow")) + r, Val(Vars("PlanNetNetCol")) + 2).Value = TmpCost.Amount * Campaign.PlannedNetNet
                        r = r + 1
                    ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnRatecard Then
                        .cells(Val(Vars("PlanNetNetRow")) + r, Val(Vars("PlanNetNetCol")) + 0).Value = TmpCost.CostName
                        .cells(Val(Vars("PlanNetNetRow")) + r, Val(Vars("PlanNetNetCol")) + 1).Value = TmpCost.Amount
                        .cells(Val(Vars("PlanNetNetRow")) + r, Val(Vars("PlanNetNetCol")) + 2).Value = TmpCost.Amount * Campaign.PlannedGross
                        r = r + 1
                    End If
                End If
            Next
            '.Cells(Val(Vars("PlanCTCRow")), Val(Vars("PlanCTCCol")) + 2).Value = Campaign.PlannedTotCTC
        End With

        Debug.WriteLine("ExportPREAnalysis END")
        Debug.WriteLine(Campaign.Adedge.debug)
        Debug.WriteLine("groupcount:" & Campaign.Adedge.getGroupCount)
    End Sub

    Sub ExportOldPreAnalysis(ByVal Excel As Object)

        Dim Msg As String
        Dim StartWeek As Integer
        Dim EndWeek As Integer
        Dim PeriodStr As String
        Dim TmpCost As Trinity.cCost
        Dim SkipIt As Boolean

        Dim i As Integer
        Dim x As Integer
        Dim f As Integer
        Dim k As Integer
        Dim BT As Integer
        Dim v As Integer
        Dim b As Integer
        Dim q As Integer
        Dim TVCheck As Integer

        Dim TargCol As Integer
        Dim CPTCol As Integer
        Dim WeekCol As Integer
        Dim WeekColSat As Integer
        Dim BuyTargetTRPCol As Integer
        Dim SpotCountCol As Integer
        Dim FilmRow As Integer
        Dim DPRow As Integer

        On Error GoTo ErrHandle


        Msg = "While Sheets(Prognos)"
        Trinity.Helper.WriteToLogFile("modDU.ExporteraPrognos : " & Msg)

        With Excel.Sheets("prognos")

            Msg = "Campaign info"
            Trinity.Helper.WriteToLogFile("modDU.ExporteraPrognos : " & Msg)
            .Range("B3").value = Campaign.Name
            .Range("B4").value = Campaign.Client
            .Range("B5").value = Campaign.Product
            StartWeek = DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
            EndWeek = DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
            PeriodStr = "Week " & StartWeek
            If EndWeek > StartWeek Then PeriodStr = PeriodStr & " - " & EndWeek
            PeriodStr = PeriodStr & ", " & Format(Date.FromOADate(Campaign.StartDate), "yyyy")
            .Range("Period").value = PeriodStr
            .Range("Startvecka").value = StartWeek
            .Range("Slutvecka").value = EndWeek
            .Range("FrDatum").value = Campaign.StartDate
            .Range("TillDatum").value = Campaign.EndDate
            .Range("Planerare").value = Campaign.Buyer
            .Range("Rådgivare").value = Campaign.Planner
            'If Mtx Is Nothing Then
            '    .Range("PlanerareMail") = PlanerareEmail
            '    .Range("PlanerareTelefon") = PlanerareTelefon
            'Else
            '    For Each u In Mtx.Users
            '        If u.RealName = Campaign.Buyer Then
            '            .Range("PlanerareMail") = u.Email
            '            .Range("PlanerareTelefon") = u.PhoneNr
            '        End If
            '        If u.RealName = Campaign.Planner Then
            '            .Range("RådgivareMail") = u.Email
            '            .Range("RådgivareTelefon") = u.PhoneNr
            '        End If
            '    Next
            'End If


            Msg = "PrepareAdedge"
            Trinity.Helper.WriteToLogFile("modDU.ExporteraPrognos : " & Msg)

            'PrepareAdedge(Adedge, prepall)

            'Msg = "ClearBrandFilter and List"
            'Trinity.Helper.WriteToLogFile("modDU.ExporteraPrognos : " & Msg)
            'Adedge.clearBrandFilter()
            'Adedge.clearList()

            'Msg = "Set Targets: " & Campaign.TargStr
            'Trinity.Helper.WriteToLogFile("modDU.ExporteraPrognos : " & Msg)

            'Adedge.setTargetMnemonic("3+")
            'Msg = "Set Period: " & Format(Campaign.StartDate, "ddmmyy")
            'Trinity.Helper.WriteToLogFile("modDU.ExporteraPrognos : " & Msg)
            'Adedge.setPeriod(Format(Campaign.StartDate, "ddmmyy"))

            ''TODO: Hitta lösning

            'If DebugIt Then
            '    Msg = "Set Universe: " & Campaign.MainTarget.SecondUniverse
            '    Trinity.Helper.WriteToLogFile("modDU.ExporteraPrognos : " & Msg)
            'End If
            'Adedge.setUniverseUserDefined(Campaign.MainTarget.SecondUniverse)
            If Campaign.Adedge.validate > 0 Then
                Campaign.Adedge.setPeriod("-1d")
                Campaign.Adedge.Run()
            End If

            'If DebugIt Then
            '    Msg = "Run"
            '    Trinity.Helper.WriteToLogFile("modDU.ExporteraPrognos : " & Msg)
            '    Trinity.Helper.WriteToLogFile(Adedge.debug)
            'End If
            'spotcount = Adedge.Run(False, False, -1)

            'If DebugIt Then
            '    Msg = "Run OK"
            '    Trinity.Helper.WriteToLogFile("modDU.ExporteraPrognos : " & Msg)
            '    Trinity.Helper.WriteToLogFile(Adedge.debug)
            'End If

            .Range("FrekvFokus") = (Campaign.FrequencyFocus + 1) & "+"
            .cells(.Range("FrekvFokus").Row, .Range("FrekvFokus").Column + 1) = Campaign.ReachGoal(Campaign.FrequencyFocus + 1)


            Msg = "Main Target"
            Trinity.Helper.WriteToLogFile("modDU.ExporteraPrognos : " & Msg)
            .Range("D7").Numberformat = "@"
            .Range("D7").FORMULA = Campaign.MainTarget.TargetNameNice
            .Range("E7").value = Campaign.MainTarget.UniSizeTot * 1000
            .Range("F7").value = Campaign.MainTarget.UniSizeSec * 1000
            .Range("E10").value = Campaign.Adedge.getUniSampleInfo(Connect.eSample.mUniSize, 0) * 1000
            .Range("F10").value = Campaign.Adedge.getUniSampleInfo(Connect.eSample.mUniSize, 0) * 1000


            Msg = "Second Target"
            Trinity.Helper.WriteToLogFile("modDU.ExporteraPrognos : " & Msg)
            .Range(Excel.Range("Prognos!Target2").Address.ToString).NUMBERFORMAT = "@"
            .Range(Excel.Range("Prognos!Target2").Address.ToString).FORMULA = Campaign.SecondaryTarget.TargetNameNice
            .Range(Excel.Range("Prognos!TargetNat2").Address.ToString).value = Campaign.SecondaryTarget.UniSizeTot * 1000
            .Range(Excel.Range("Prognos!TargetSat2").Address.ToString).value = Campaign.SecondaryTarget.UniSizeSec * 1000

            .Range(Excel.Range("Prognos!Target3").Address.ToString).NUMBERFORMAT = "@"
            .Range(Excel.Range("Prognos!Target3").Address.ToString).FORMULA = Campaign.ThirdTarget.TargetNameNice
            .Range(Excel.Range("Prognos!TargetNat3").Address.ToString).value = Campaign.ThirdTarget.UniSizeTot * 1000
            .Range(Excel.Range("Prognos!TargetSat3").Address.ToString).value = Campaign.ThirdTarget.UniSizeSec * 1000

            TmpCost = Nothing
            For Each TmpCost In Campaign.Costs
                If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePerUnit And TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnSpots Then
                    Exit For
                End If
            Next

            If Not TmpCost Is Nothing Then
                .Range("Spotkontroll") = TmpCost.Amount
                TVCheck = TmpCost.Amount
            End If
            For i = 1 To 10
                .Range("Reach" & i) = Campaign.ReachGoal(i)
                '.cells(.Range("Reach" & i).Row + 1, .Range("Reach" & i).Column) = Campaign.ReachTargets(i, "Sat")
            Next

            x = 0
            TargCol = .Range("TargetCol").Column
            CPTCol = .Range("CPT").Column
            WeekCol = .Range("PerVeckaLeft").Column
            WeekColSat = .Range("PerVeckaSat").Column
            BuyTargetTRPCol = .Range("KöpMGTrp").Column
            SpotCountCol = .Range("AntSpots").Column

            FilmRow = .Range("FilmkodRubrik").Row
            DPRow = .Range("DaypartSplit").Row
            For f = 1 To Campaign.Channels(1).BookingTypes(1).Weeks(1).Films.Count
                .cells(FilmRow + f, 1).NUMBERFORMAT = "@"
                .cells(FilmRow + f, 1).value = Campaign.Channels(1).BookingTypes(1).Weeks(1).Films(f).Filmcode
                .cells(FilmRow + f, 2).value = Campaign.Channels(1).BookingTypes(1).Weeks(1).Films(f).FilmLength
                .cells(FilmRow + f, 3).value = Campaign.Channels(1).BookingTypes(1).Weeks(1).Films(f).Index
            Next
            Dim TRPIndex As Single = 0
            Dim TRP30 As Single = 0
            For k = 1 To Campaign.Channels.Count
                SkipIt = True
                For BT = 1 To Campaign.Channels(k).BookingTypes.Count
                    If Campaign.Channels(k).BookingTypes(BT).BookIt Then
                        SkipIt = False
                    End If
                Next
                If Not SkipIt Then
                    x = x + 1
                    .cells(9 + (x * 5), 1).value = Campaign.Channels(k).ChannelName
                    .cells(12 + (x * 5), 1).value = Campaign.Channels(k).Shortname
                    For v = 1 To Campaign.Channels(k).BookingTypes(1).Weeks.Count
                        .cells(8 + (x * 5), WeekCol + v).value = Campaign.Channels(k).BookingTypes(1).Weeks(v).Name
                        .cells(8 + (x * 5), WeekColSat + v).value = Campaign.Channels(k).BookingTypes(1).Weeks(v).Name
                    Next
                    b = 0
                    For BT = 1 To Campaign.Channels(k).BookingTypes.Count
                        If Campaign.Channels(k).BookingTypes(BT).BookIt Then
                            b = b + 1
                            .cells(9 + (x * 5) + b - 1, 2).value = Campaign.Channels(k).BookingTypes(BT).Name
                            If Campaign.Channels(k).BookingTypes(BT).BookIt Then
                                .cells(9 + (x * 5) + b - 1, TargCol).value = Campaign.Channels(k).BookingTypes(BT).BuyingTarget.TargetName
                                .cells(9 + (x * 5) + b - 1, TargCol).NUMBERFORMAT = "@"
                                .cells(9 + (x * 5) + b - 1, CPTCol).value = Campaign.Channels(k).BookingTypes(BT).BuyingTarget.NetCPT
                                .cells(9 + (x * 5) + b - 1, BuyTargetTRPCol).value = 0
                                If Campaign.Channels(k).BookingTypes(BT).ConfirmedSpotCount = 0 Then
                                    .cells(9 + (x * 5) + b - 1, SpotCountCol).value = Campaign.Channels(k).BookingTypes(BT).EstimatedSpotCount
                                Else
                                    .cells(9 + (x * 5) + b - 1, SpotCountCol).value = Campaign.Channels(k).BookingTypes(BT).ConfirmedSpotCount
                                End If
                                For v = 1 To Campaign.Channels(k).BookingTypes(BT).Weeks.Count
                                    Dim TRP As Single = Campaign.Channels(k).BookingTypes(BT).Weeks(v).TRP
                                    .cells(9 + (x * 5) + b - 1, WeekColSat + v).value = Campaign.Channels(k).BookingTypes(BT).Weeks(v).TRPBuyingTarget
                                    .cells(9 + (x * 5) + b - 1, WeekCol + v).value = Campaign.Channels(k).BookingTypes(BT).Weeks(v).TRP
                                    .cells(53 + x * 3 + b - 1, WeekCol + v).value = Campaign.Channels(k).BookingTypes(BT).Weeks(v).SpotIndex / 100

                                    TRP30 = TRP30 + TRP
                                    For q = 1 To Campaign.Channels(k).BookingTypes(BT).Weeks(v).Films.Count
                                        TRPIndex = TRPIndex + (TRP * (Campaign.Channels(k).BookingTypes(BT).Weeks(v).Films(q).Share / 100)) * (Campaign.Channels(k).BookingTypes(BT).Weeks(v).Films(q).Index / 100)
                                        .cells(FilmRow + q, 3 + x).NUMBERFORMAT = "0.0"
                                        .cells(FilmRow + q, 3 + x).value = .cells(FilmRow + q, 3 + x).value + (TRP * (Campaign.Channels(k).BookingTypes(BT).Weeks(v).Films(q).Share / 100))
                                    Next
                                    For i = 0 To 2
                                        .cells(DPRow + x, 2 + i).value = .cells(DPRow + x, 2 + i).value + (TRP * (Campaign.Channels(k).BookingTypes(BT).MainDaypartSplit(i) / 100))
                                    Next
                                    .cells(9 + (x * 5) + b - 1, BuyTargetTRPCol).value = .cells(9 + (x * 5) + b - 1, BuyTargetTRPCol).value + Campaign.Channels(k).BookingTypes(BT).Weeks(v).TRPBuyingTarget
                                    '.cells(9 + (x * 5) + b-1, 4) = .cells(9 + (x * 5) + b-1, 4) + Campaign.Channels(K).Bookingtypes(bt).Weeks(v).NettoBudget
                                    If Campaign.Channels(k).BookingTypes(BT).ConfirmedNetBudget > 0 Then
                                        .cells(9 + (x * 5) + b - 1, 4).value = Campaign.Channels(k).BookingTypes(BT).ConfirmedNetBudget
                                    Else
                                        If .cells(9 + (x * 5) + b - 1, 4).value Is Nothing OrElse .cells(9 + (x * 5) + b - 1, 4).value.ToString = "" Then
                                            .cells(9 + (x * 5) + b - 1, 4).value = 0
                                        End If
                                        .cells(9 + (x * 5) + b - 1, 4).value = .cells(9 + (x * 5) + b - 1, 4).value + (Campaign.Channels(k).BookingTypes(BT).Weeks(v).NetBudget)
                                    End If
                                    .cells(9 + (x * 5) + b - 1, 3).value = .cells(9 + (x * 5) + b - 1, 3).value + (Campaign.Channels(k).BookingTypes(BT).Weeks(v).TRPBuyingTarget * Campaign.Channels(k).BookingTypes(BT).Weeks(v).GrossCPP * (Campaign.Channels(k).BookingTypes(BT).Weeks(v).SpotIndex / 100))
                                Next

                            End If
                        End If
                    Next
                End If
            Next
            If TRP30 > 0 Then
                .Range("Spotindex") = TRPIndex / TRP30
            End If
            .Range("Retur").FORMULA = -Campaign.EstimatedCommission
            For Each TmpCost In Campaign.Costs
                If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                    .Range("Påslag") = TmpCost.Amount
                    If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnMediaNet Then
                        .cells(.Range("Påslag").Row + 1, .Range("Påslag").Column).FORMULA = "=(TVBudget*Påslag)"
                    Else
                        .cells(.Range("Påslag").Row + 1, .Range("Påslag").Column).FORMULA = "=(TVBudget+Retur)*Påslag"
                    End If
                End If
                If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeFixed Then
                    .Range("Marketwatch") = TmpCost.Amount
                End If
            Next

            Exit Sub
            Dim TVCheckCost As Integer = 0
            For Each TmpChan As Trinity.cChannel In Campaign.Channels
                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                    If TmpBT.ConfirmedSpotCount = 0 Then
                        TVCheckCost = TVCheckCost + TVCheck * TmpBT.EstimatedSpotCount
                    Else
                        TVCheckCost = TVCheckCost + TVCheck * TmpBT.ConfirmedSpotCount
                    End If
                Next
            Next
            .Range("TVCheck") = TVCheckCost
            For x = 1 To 10
                .cells(.Range("FrekvEst").Row + x, .Range("FrekvEst").Column) = Campaign.ReachGoal(x)
            Next
        End With

        Exit Sub

ErrHandle:
        Resume
        System.Windows.Forms.MessageBox.Show("Error:" & Chr(10) & Chr(10) & "Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, "Error", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
    End Sub

    Private Class ConnectCallback
        Implements Connect.ICallBack

        Private Progress As frmProgress

        Public Sub callback(ByVal p As Integer) Implements Connect.ICallBack.callback
            Progress.Progress = p
        End Sub

        Public Sub New(ByVal ProgressWindow As frmProgress)
            Progress = ProgressWindow
        End Sub
    End Class
End Module
