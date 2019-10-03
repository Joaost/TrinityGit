
Imports clTrinity
Imports clTrinity.Trinity
Imports Microsoft.Office.Interop.Excel

Public Class CExportUnicornFileNew
    Dim _excel As CultureSafeExcel.Application
    Dim _wb As CultureSafeExcel.Workbook
    Dim _sheet As CultureSafeExcel.Worksheet

    Dim _langIni As New IniFile()

    Private _bookingType As Trinity.cBookingType
    Private _combination As Trinity.cCombination

    Private lastRowComboTable1 As Integer
    Private lastRowComboTable2 As Integer
    Private lastRowComboTable3 As Integer
    Private lastRowComboTable4 As Integer
    Private lastRowComboTable5 As Integer

    Private amountOfWeeks As Integer
    Private amountOfFilms As Integer
    Private amountOfChannels As Integer
    Dim _amountOfFilmLength As New List(Of Integer)

    Dim _printExportAsCampaign As Boolean = False

    Dim listOfFilms As New List(Of Trinity.cFilm)

    Dim _BundleTV4 As Boolean = True
    Dim _BundleMTG As Boolean = False
    Dim _BundleSBS As Boolean = False
    Dim _BundleFOX As Boolean = False
    Dim _BundleCMORE As Boolean = False
    Dim _BundleTNT As Boolean = False
    Dim _BundleDisney As Boolean = False
    Dim _BundleCartoon As Boolean = False
    Dim tmpPrintExportAsCampaign As Boolean = False
    Dim ExportedAsCampaignCombination As Boolean = False
    Dim before2016 As Boolean = True

    Dim _bundleMTGSpecial As Boolean = False
    Dim _campaign As Trinity.cKampanj

    Dim tmpRow As Integer = 39

    Dim targetTV4 As String = ""
    Dim targetTMTG As String = ""
    Dim targetTSBS As String = ""
    Dim targetTFOX As String = ""
    Dim targetTCMORE As String = ""
    Dim targetTNT As String = ""
    Dim targetDisney As String = ""
    Dim targetCartoon As String = ""

    Dim groupName As String = ""
    Dim channelName As String = ""

    Dim targets As String = ""

    Dim tmpUserCompany As String = ""

    Dim tmpOwnFee As Decimal = 0
    Dim ownCommission As Boolean = False
    Dim ownCommissionAmount As Single = 0
    Dim listOfWeeks As New List(Of cWeek)
    Dim filteredChannels As New List(Of cChannel)

    'Dim _campaign As Trinity.cKampanj
    '2018-10-26 changed MTG TV to NENT in every PrinTtable'

    Public Sub printUnicornFile(Optional ByVal tempBundleTV4 As Boolean = False, Optional ByVal tempBundleMTG As Boolean = False, Optional ByVal tempBundleMTGSpecial As Boolean = False, Optional ByVal tempBundleSBS As Boolean = False, Optional ByVal tempBundleFOX As Boolean = False, Optional ByVal tempBundleCMORE As Boolean = False, Optional ByVal tempBundleDisney As Boolean = False, Optional ByVal temBundleTNT As Boolean = False, Optional ByVal useOwnCommission As Boolean = False, Optional ByVal useOwnCommissionAmount As Decimal = 0, Optional ByVal tmpPrintExportAsCampaign As Boolean = False, Optional ByVal weeks As List(Of cWeek) = Nothing, Optional ByVal tmpFilteredChannels As List(Of cChannel) = Nothing)


        listOfWeeks = weeks
        filteredChannels = tmpFilteredChannels

        If useOwnCommission Then
            ownCommission = True
            ownCommissionAmount = useOwnCommissionAmount / 100
        End If
        prepareEnivorment()
        checkAmountOfBookedChannels()
        checkBundling(tempBundleTV4, tempBundleMTG, tempBundleMTGSpecial, tempBundleSBS, tempBundleFOX, tempBundleCMORE, temBundleTNT, tempBundleDisney, tmpPrintExportAsCampaign)
        _wb = _excel.AddWorkbook
        _sheet = _wb.Sheets(1)
        If checkCampaignForHoles() Then

        End If
        getFilms()
        GetAmountOfFilmLengths()
        checkBefore2016()

        Dim value = Trinity.Helper.FormatDateForBooking(Campaign.StartDate)
        With _sheet
            'Print Functions
            printHeader()   ' Print Main header With overview data
            printStationBudgetTable()    'First table. Print first table containing all stations budget. Gross, Net, Netnet, Fees, Ctc, TRP and Buying target
            printPrimarytargetTRPWeekly()   'Second table.Print Primary target TRP per station and week
            printBuyingtargetTRPWeekly()    'Third table. Print buying target TRP per station and week
            printPrimarytargetTRPSpotlength()   'Fourth table. Print Primary target total TRP per station and spotlength
            printBuyingtargetTRPSpotlength()   'Fifth table. Print Buying target total TRP pers station and spotlength
            printFilmcodesSummery()   'Sixth table. Print Filmcodes, spotlength, duration and total Primary target TRP for all stations per week
            For i As Integer = 1 To 20
                .Columns(i).AutoFit()
            Next
        End With
        _sheet.Range("B" & 2 & ":M" & 200).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
        _sheet.Range("B" & 2 & ":M" & 200).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
        resetEnivorment()
    End Sub
    Sub checkBefore2016()
        If Campaign.StartDate <= 42735 Then
            before2016 = True
        Else
            before2016 = False
        End If
    End Sub
    Sub checkAmountOfBookedChannels()
        For Each tmpChan As Trinity.cChannel In Campaign.Channels
            For Each tmpBt As Trinity.cBookingType In tmpChan.BookingTypes
                If tmpBt.BookIt Then
                    amountOfChannels = amountOfChannels + 1
                End If
            Next
        Next
    End Sub
    Public Sub New(ByRef Campaign As Trinity.cKampanj)
        GetAmountOfWeeks()
        _excel = New CultureSafeExcel.Application(False)
        _campaign = Campaign
    End Sub
    Sub GetAmountOfWeeks()

        For Each tmpC As Trinity.cChannel In Campaign.Channels
            For Each tmpBT As Trinity.cBookingType In tmpC.BookingTypes
                If tmpBT.BookIt Then
                    For Each tmpWeek As Trinity.cWeek In listOfWeeks
                        amountOfWeeks = amountOfWeeks + 1
                    Next
                    Exit Sub
                End If
            Next
        Next
    End Sub
    Sub getFilms()
        For Each tmpC As Trinity.cChannel In Campaign.Channels
            For Each tmpBt As Trinity.cBookingType In tmpC.BookingTypes
                For Each tmpW As Trinity.cWeek In tmpBt.Weeks
                    For Each tmpf As Trinity.cFilm In tmpW.Films
                        listOfFilms.Add(tmpf)
                        amountOfFilms = amountOfFilms + 1
                    Next
                    Exit Sub
                Next
            Next
        Next
    End Sub
    Sub GetAmountOfFilmLengths()
        Dim x As Integer = 0
        For Each tmpFilm As Trinity.cFilm In listOfFilms
            If Not (_amountOfFilmLength.Contains((tmpFilm.FilmLength)) Or _amountOfFilmLength Is Nothing) Then
                _amountOfFilmLength.Add((tmpFilm.FilmLength))
            End If
        Next
    End Sub
    Function translateTargetName(ByVal tmptargetName As String)
        Dim targetValue As String = tmptargetName
        If targetValue.Contains("A") Or (targetValue.Contains("M") Or targetValue.Contains("W")) Then
            Return targetValue
        ElseIf targetValue.Contains("K") Then
            targetValue = targetValue.Replace("K", "W")
        Else
            targetValue = "A" & targetValue
        End If
        Return targetValue
    End Function
    Sub checkBundling(Optional ByVal tempBundleTV4 As Boolean = False, Optional ByVal tempBundleMTG As Boolean = False, Optional ByVal tempBundleMTGSpecial As Boolean = False, Optional ByVal tempBundleSBS As Boolean = False, Optional ByVal tempBundleFOX As Boolean = False, Optional ByVal tempBundleCMORE As Boolean = False, Optional ByVal tempBundleTNT As Boolean = False, Optional ByVal tempBundleDisney As Boolean = False, Optional ByVal tmpPrintAsCampaign As Boolean = False)

        _BundleTV4 = tempBundleTV4
        _BundleMTG = tempBundleMTG
        _bundleMTGSpecial = tempBundleMTGSpecial
        _BundleSBS = tempBundleSBS
        _BundleFOX = tempBundleFOX
        _BundleCMORE = tempBundleCMORE
        _BundleTNT = tempBundleTNT
        _BundleDisney = tempBundleDisney
        _printExportAsCampaign = tmpPrintAsCampaign

    End Sub
    Public Function checkNameTV4(ByVal tmpChannelName As String)
        If tmpChannelName.Contains("TV4") Or tmpChannelName = "Sjuan" Or tmpChannelName = "TV12" Or tmpChannelName = "Sportkanalen" Then
            Return True
        End If
        Return False
    End Function
    Public Function checkNameMTG(ByVal tmpChannelName As String)
        If before2016 Then
            If tmpChannelName = "TV3" Or tmpChannelName = "TV6" Or tmpChannelName = "MTV" Or tmpChannelName = "TV8" Or tmpChannelName = "TV10" Or tmpChannelName = "Comedy central" Or tmpChannelName.Contains("Comedy") Or tmpChannelName = "Nickelodeon" Then
                Return True
            End If
        Else
            If tmpChannelName = "TV3" Or tmpChannelName = "TV6" Or tmpChannelName = "MTV" Or tmpChannelName = "TV8" Or tmpChannelName = "TV10" Or tmpChannelName = "Comedy central" Or tmpChannelName.Contains("Comedy") Or tmpChannelName = "Nickelodeon" Or tmpChannelName.Contains("FOX") Or tmpChannelName.Contains("National") Or tmpChannelName.Contains("History") Or tmpChannelName.Contains("Paramount") Then
                Return True
            End If
        End If
        Return False
    End Function
    Public Function checkNameSBS(ByVal tmpChannelName As String)
        If tmpChannelName = "Kanal5" Or tmpChannelName = "Kanal9" Or tmpChannelName = "Kanal 11" Or tmpChannelName = "Kanal11" Or tmpChannelName = "TV11" Or tmpChannelName = "Eurosport" Or tmpChannelName = "Discovery" Or tmpChannelName = "TLC" Or tmpChannelName = "ID" Or tmpChannelName = "Investigation Discovery" Then
            Return True
        End If
        Return False
    End Function

    Public Function checkNameFOX(ByVal tmpChannelName As String)
        If before2016 Then
            If tmpChannelName.Contains("FOX") Or tmpChannelName.Contains("National") Then
                Return True
            End If
        End If
        Return False
    End Function
    Public Function checkNameCMORE(ByVal tmpChannelName As String)
        If tmpChannelName = "CMore" Or tmpChannelName = "C More" Then
            Return True
        End If
        Return False
    End Function
    Public Function checkNameTNT(ByVal tmpChannelName As String)
        If tmpChannelName.Contains("TNT") Then
            Return True
        End If
        Return False
    End Function
    Public Function checkNameCartoon(ByVal tmpChannelName As String)
        If tmpChannelName.Contains("Cartoon") Then
            Return True
        End If
        Return False
    End Function
    Public Function checkNameDisney(ByVal tmpChannelName As String)
        If tmpChannelName.Contains("Disney") Then
            Return True
        End If
        Return False
    End Function

    Public Function GetWeeks()
        For Each tmpC As Trinity.cChannel In Campaign.Channels
            For Each tmpBT As Trinity.cBookingType In tmpC.BookingTypes
                If tmpBT.BookIt Then
                    For Each tmpWeek As Trinity.cWeek In tmpBT.Weeks
                        listOfWeeks.Add(tmpWeek)
                    Next
                    Return listOfWeeks
                End If
            Next
        Next
    End Function
    Public Function checkCampaignForHoles()
        'Dim daysBetween As Integer = 0
        'Dim holeInCampaign As Boolean = False

        'Dim listOfWeeks As List(Of cWeek)

        'listOfWeeks = GetWeeks()

        'For i As Integer = 0 To listOfWeeks.Count - 1
        '    Dim p1End As Long = listOfWeeks.Item(i).EndDate
        '    Dim p2Start As Long
        '    If i < listOfWeeks.Count - 1          
        '         p2Start = listOfWeeks.Item(i + 1).StartDate
        '    End If
        '    If (p2Start - p1End) > 1
        '        For each tmpC As Trinity.cChannel in _campaign.Channels
        '            For each tmpBt As Trinity.cBookingType in tmpC.BookingTypes
        '                Dim newWeek As cWeek

        '                newWeek = tmpBt.Weeks.Add("test week")
        '                newWeek.EndDate = p2Start - 1
        '                newWeek.StartDate = p1End + 1
        '                newWeek.TRP = 0

        '                Dim TmpFilms As Trinity.cFilms = TmpBT.Weeks(1).Films

        '                Dim TmpFilm As Trinity.cFilm
        '                For Each TmpFilm In TmpFilms
        '                    newWeek.Films.Add(TmpFilm.Name).Clone(TmpFilm)
        '                Next
        '            Next
        '        Next
        '        Return True
        '    End If
        'Next
        ''If listOfWeeks IsNot Nothing Or listOfWeeks.Count > 1
        ''    For each tmpWeek As cWeek In listOfWeeks
        ''        Dim p1End = tmpWeek.EndDate
        ''        listOfWeeks.Item
        ''    Next
        ''End If

        'Return False
    End Function


    Sub printHeader()
        _sheet.Range("B" & 2 & ":M" & 999).Font.Name = "Calibri"
        _sheet.Range("B" & 2 & ":M" & 999).Font.Size = 10
        With _sheet
            'Create colorfields
            .Range("B" & 2 & ":B" & 15).Interior.Color = RGB(0, 137, 178) 'Light blue color
            .Range("B" & 2 & ":B" & 15).Font.Bold = True
            .Range("C" & 2 & ":C" & 15).Interior.Color = RGB(242, 242, 242)
            .Range("B" & 2 & ":B" & 15).Font.Color = RGB(255, 255, 255)

            .Range("B" & 17 & ":B" & 18).Interior.Color = RGB(0, 137, 178)
            .Range("B" & 17 & ":B" & 18).Font.Bold = True
            .Range("C" & 17 & ":C" & 18).Interior.Color = RGB(242, 242, 242)
            .Range("B" & 17 & ":B" & 18).Font.Color = RGB(255, 255, 255)

            .Range("B" & 20 & ":B" & 22).Interior.Color = RGB(0, 137, 178)
            .Range("B" & 20 & ":B" & 22).Font.Bold = True
            .Range("C" & 20 & ":C" & 22).Interior.Color = RGB(242, 242, 242)
            .Range("B" & 20 & ":B" & 22).Font.Color = RGB(255, 255, 255)

            .Range("B" & 24 & ":C" & 24).Interior.Color = RGB(0, 37, 110) 'Dark blue color
            .Range("B" & 24 & ":C" & 24).Font.Color = RGB(255, 255, 255)
            .Range("B" & 25 & ":C" & 25).Interior.Color = RGB(0, 137, 178)
            .Range("B" & 25 & ":C" & 25).Font.Color = RGB(255, 255, 255)

        End With
        With _sheet
            'Create textfields
            .Cells(2, 2).Value = "Campaign title"
            .Cells(3, 2).Value = "Area"
            .Cells(4, 2).Value = "Agency"
            .Cells(5, 2).Value = "Planner"
            .Cells(6, 2).Value = "Campaign type"
            .Cells(7, 2).Value = "Campaign type 2"
            .Cells(8, 2).Value = "Concern"
            .Cells(9, 2).Value = "Advertiser"
            .Cells(10, 2).Value = "Planned Budget incl. Fee"
            .Cells(11, 2).Value = "Marathonclient ID"
            .Cells(12, 2).Value = "Marathon client agreement"
            .Cells(13, 2).Value = "Budget name"
            .Cells(14, 2).Value = "Budget nr"
            .Cells(15, 2).Value = "Product/Brand"

            .Cells(17, 2).Value = "Start date"
            .Cells(18, 2).Value = "End date"

            .Cells(20, 2).Value = "Primary target"
            .Cells(21, 2).Value = "Secondary target"
            .Cells(22, 2).Value = "Eff. Frequency"

            .Cells(24, 2).Value = "Frequency and reach"
            .Range("B" & 24 & ":C" & 24).Merge()
            .Cells(25, 2).Value = "Frequency"
            .Cells(25, 3).Value = "Reach"

            .Cells(37, 2).Value = "Station budget"
            .Cells(38, 2).Value = "Network"
            .Cells(38, 3).Value = "Station"
            .Cells(38, 4).Value = "Gross"
            .Cells(38, 5).Value = "Net"
            .Cells(38, 6).Value = "Net net"
            .Cells(38, 7).Value = "Fees"
            .Cells(38, 8).Value = "Fond"
            .Cells(38, 9).Value = "Other"
            .Cells(38, 10).Value = "Ctc"
            .Cells(38, 11).Value = "TRP"
            .Cells(38, 12).Value = "Buying Target"

            'Print data
            .Cells(2, 3).Value = Campaign.Name
            .Cells(3, 3).Value = "SE"
            Dim userCompany As String = ""

            Dim cs As String = TrinitySettings.ConnectionString(Trinity.cSettings.SettingsLocationEnum.locNetwork)

            If cs.Contains("mec") Then
                userCompany = "MEC"
            ElseIf cs.Contains("mc") Then
                userCompany = "Mediacom"
            ElseIf cs.Contains("maxus") Then
                userCompany = "Maxus"
            ElseIf cs.Contains("ms") Then
                userCompany = "Mindshare"
            End If

            .Cells(4, 3).Value = userCompany
            .Cells(5, 3).Value = Campaign.Planner
            .Cells(6, 3).Value = "TV Campaign"
            .Cells(7, 3).Value = "Adults"
            .Cells(8, 3).Value = Campaign.Client
            .Cells(9, 3).Value = Campaign.Client
            .Cells(10, 3).Value = Campaign.PlannedTotCTC.ToString()
            .Cells(11, 3).Value = Campaign.ClientID.ToString()
            .Cells(12, 3).Value = Campaign.ContractID.ToString()
            .Cells(13, 3).Value = Campaign.MarathonPlanNr 'Budget namn?
            .Cells(14, 3).Value = Campaign.MarathonPlanNr 'Budget nr?
            .Cells(15, 3).Value = Campaign.Product

            '.Cells(17, 3).Value = Trinity.Helper.FormatDateForBooking(Campaign.StartDate)
            '.Cells(18, 3).Value = Trinity.Helper.FormatDateForBooking(Campaign.EndDate)


            .Cells(17, 3).Value = Trinity.Helper.FormatDateForBooking(listOfWeeks.First().StartDate)
            .Cells(18, 3).Value = Trinity.Helper.FormatDateForBooking(listOfWeeks.Last().EndDate)




            Dim tmpMainTarget As String = translateTargetName(Campaign.MainTarget.TargetName)
            'Dim tmpSecondTarget As String = translateTargetName(Campaign.SecondaryTarget.TargetName)
            Dim tmpSecondTarget As String = translateTargetName(Campaign.MainTarget.TargetName)
            .Cells(20, 3).Value = tmpMainTarget
            .Cells(21, 3).Value = tmpSecondTarget
            .Cells(22, 3).Value = (Campaign.FrequencyFocus + 1) & "+"

            Dim row As Integer = 26
            Dim plus As String = "+"
            For i As Integer = 1 To 10
                .Cells(row, 2).Value = i & plus
                .Range("B" & row & ":B" & row).Font.Color = RGB(255, 255, 255)
                .Range("B" & row & ":B" & row).Interior.Color = RGB(0, 137, 178) 'Blue color

                Dim reach As Decimal = Campaign.ReachGoal(i, Trinity.cKampanj.ReachTargetEnum.rteMainTarget)
                reach = reach / 100
                .Cells(row, 3).Value = reach
                '.Cells(row, 3).Numberformat = "0"
                .Cells(row, 3).Numberformat = "#%"
                row = row + 1

            Next
        End With
    End Sub
    Function checkEmailSettings()
        If TrinitySettings.DefaultArea = "NO" Then
        Else
            If TrinitySettings.UserEmail = "" Or TrinitySettings.UserEmail.Contains("-") Then
                If TrinitySettings.DataPath = "\\stosqlp01101\databas\Trinity Data 4.0\" Then
                    tmpUserCompany = "Mediacom"
                ElseIf TrinitySettings.DataPath = "F:\AVM\11.Trinitydata 4.0\" Then
                    tmpUserCompany = "MEC"
                ElseIf TrinitySettings.DataPath = "\\stosqlp01101\media\Trinity\Trinity Data 4.0\" Then
                    tmpUserCompany = "Mindshare"
                ElseIf TrinitySettings.DataPath = "\\stosqlp01101\databas\Trinity Data Maxus\" Then
                    tmpUserCompany = "Maxus"
                End If
            End If
        End If

        Return True
    End Function
    Function checkPathSettings()
        If TrinitySettings.DefaultArea = "NO" Then
        Else
            If TrinitySettings.UserEmail.Contains("mecglobal") Then
                tmpUserCompany = "MEC"
            ElseIf TrinitySettings.UserEmail.Contains("mediacom") Then
                tmpUserCompany = "Mediacom"
            ElseIf TrinitySettings.UserEmail.Contains("maxus") Then
                tmpUserCompany = "Maxus"
            ElseIf TrinitySettings.UserEmail.Contains("mindshare") Then
                tmpUserCompany = "Mindshare"
            End If
        End If
        Return True
    End Function
    Function checkDatabasePath()
        If TrinitySettings.DefaultArea = "NO" Then
        Else
            If TrinitySettings.DataPath = "Trintiy_mec" Then
                tmpUserCompany = "MEC"
            ElseIf TrinitySettings.DataPath = "Trinity_mc" Then
                tmpUserCompany = "Mediacom"
            ElseIf TrinitySettings.DataPath = "Trinity_ms" Then
                tmpUserCompany = "Mindshare"
            ElseIf TrinitySettings.DataPath = "Trinity_maxus" Then
                tmpUserCompany = "Maxus"
            End If
        End If
        Return True
    End Function
    Sub StationBudgetTableData(ByVal tmpChan As Trinity.cChannel, ByVal tmpB As Trinity.cBookingType, ByVal tmpGroupName As String, ByVal bundle As Boolean, ByVal row As Integer, ByVal tmpChannelName As String, Optional ByVal savedRow As Integer = 0, Optional ByVal printExportAsCampaign As Boolean = False)


        Dim tmpBook As Trinity.cBookingType = tmpB
        Dim tmpBundle = bundle

        Dim stationChannels = ""

        Dim totalPlannedGrossBudget As Decimal = 0
        Dim totalPlannedNetBudget As Decimal = 0
        Dim totalPlannedNetNetBudget As Decimal = 0
        Dim totalPlannedFee As Decimal = 0

        Dim listOfSpots As New List(Of Trinity.cBookedSpot)

        Dim totalTRPForBT As Decimal = 0

        Dim lastRow As Integer = 0

        With _sheet
            Dim valueTest = .Cells(savedRow, 3).Value

            'If tmpBundle And groupName = tmpGroupName Or tmpGroupName <> "TV4" Then
            '    row = savedRow
            'ElseIf tmpBundle And groupName = tmpGroupName Or tmpGroupName = "TV4" Then
            '    If tmpBundle And tmpGroupName = "TV4" And tmpChan.ChannelName <> "TV4" Then
            '        row = savedRow
            '    Else
            '        If valueTest IsNot Nothing Then
            '            If valueTest.ToString.Contains(tmpChan.AdEdgeNames) Then
            '                row = savedRow
            '            Else
            '                row = tmpRow
            '                tmpRow = tmpRow + 1
            '            End If
            '        Else
            '            row = savedRow
            '        End If
            '    End If

            If tmpBundle Then
                row = savedRow
            ElseIf tmpB.IsCompensation Or tmpB.IsSpecific And (channelName = tmpChannelName) Then
                row = savedRow
            Else
                If savedRow = 0 Then
                    row = tmpRow
                Else
                    row = savedRow
                End If
            End If
            channelName = tmpChannelName
            groupName = tmpGroupName

            .Range("B" & row & ":C" & row).Interior.Color = RGB(0, 137, 178)
            .Range("B" & row & ":C" & row).Font.Color = RGB(255, 255, 255)
            Dim test = .Cells(row, 3).Value
            'If TrinitySettings.Developer Then
            '    .Cells(row, 13).Value += tmpChan.ChannelName + " " + tmpBook.Name + " "
            'End If
            If Not printExportAsCampaign Then
                If groupName = "TV4" Then
                    If tmpBundle Then
                        If tmpChan.ChannelName = "TV4" Then
                            .Cells(row, 2).Value = "TV4"
                        Else
                            .Cells(row, 2).Value = "TV4 Nish"
                        End If
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "MTG" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "NENT"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "SBS" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Discovery"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "FOX" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Fox Int"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "Cartoon" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Cartoon"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "Disney" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Disney C/XD"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "TNT" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "TNT"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "CMORE" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "C More"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                Else
                    If .Cells(row, 2).Value Is Nothing Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                    ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                    End If
                End If
            Else
                If groupName = "MTG" Then
                    .Cells(row, 2).Value = "MTG TV"
                ElseIf groupName = "SBS" Then
                    .Cells(row, 2).Value = "SBS Combo"
                ElseIf groupName = "FOX" Then
                    .Cells(row, 2).Value = "FOX Combo"
                ElseIf groupName = "Cartoon" Then
                    .Cells(row, 2).Value = "Cartoon Combo"
                ElseIf groupName = "Disney" Then
                    .Cells(row, 2).Value = "Disney Combo"
                ElseIf groupName = "TNT" Then
                    .Cells(row, 2).Value = "TNT Combo"
                ElseIf groupName = "CMORE" Then
                    .Cells(row, 2).Value = "C More Combo"
                End If
            End If

            If Not .Cells(row, 3) Is Nothing Then
                If tmpBundle And tmpGroupName <> "TV4" Then
                    If groupName = "MTG" Then
                        If before2016 Then
                            .Cells(row, 3).Value = "TV3 se; TV6 SE; MTV se; TV8; Comedy Central; TV10; Nickelodeon;"
                        Else
                            .Cells(row, 3).Value = "TV3 se; TV6 SE; MTV se; TV8; Comedy Central; TV10; FOX; NatlGeo se; Nickelodeon; History; Paramount Network;"
                        End If
                    ElseIf groupName = "SBS" Then
                        .Cells(row, 3).Value = "Kanal 5; Eurosport SE; Discovery se; TLC; Kanal 9; Kanal11; Investigation Discovery; Eurosport 2 SE;"
                    ElseIf groupName = "FOX" Then
                        .Cells(row, 3).Value = "Fox; NatlGeo se;"
                    ElseIf groupName = "Cartoon" Then
                        .Cells(row, 3).Value = "Cartoon Network Sweden; Cartoon Network Nordic;"
                    ElseIf groupName = "Disney" Then
                        .Cells(row, 3).Value = "Disney Channel; Disney XD se;"
                    ElseIf groupName = "TNT" Then
                        .Cells(row, 3).Value = "TNT"
                    ElseIf groupName = "CMORE" Then
                        .Cells(row, 3).Value = "C More"
                    End If
                ElseIf test Is Nothing Then
                    If tmpChan.AdEdgeNames <> "" Then
                        stationChannels += tmpChan.AdEdgeNames + "; "
                    Else
                        stationChannels += tmpChan.ChannelName + "; "
                    End If
                Else
                    If Not test.ToString.Contains(tmpChan.AdEdgeNames) Then
                        If tmpChan.AdEdgeNames <> "" Then
                            stationChannels += tmpChan.AdEdgeNames + "; "
                        Else
                            stationChannels += tmpChan.ChannelName + "; "
                        End If
                    End If
                End If
            End If
            .Cells(row, 3).Value += stationChannels


            Dim tmpCTC As Integer = 0
            Dim tmptotalPlannedGrossBudget = 0
            Dim tmptotalPlannedNetBudget = 0
            Dim tmpServiceFee As Boolean = False
            Dim spotControll As Boolean = False
            Dim otherFee As Boolean = False

            Dim serviceFeeAmount As Decimal = 0
            Dim spotControllAmount As Decimal = 0
            Dim otherFeeAmount As Decimal = 0

            Dim totalCTC = 0

            Dim tmpCostAmount As Decimal = 0
            Dim TmpCost As Trinity.cCost
            Dim SumUnit = 0
            Dim totalFees = 0
            Dim tmpPrintNetNet = 0

            For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                For Each _w As cWeek In listOfWeeks
                    If _w.Name = tmpWeek.Name Then
                        totalTRPForBT += tmpWeek.TRP
                    End If
                Next
            Next
            .Cells(row, 11).Value += Math.Round(totalTRPForBT, 2)
            If Not tmpB.IsCompensation Then

                For Each _w As cWeek In listOfWeeks
                    For Each _w2 As cWeek In tmpBook.Weeks
                        If _w2.Name = _w.Name Then

                            If _w2.GrossBudget = 0 Then
                                tmptotalPlannedGrossBudget += _w2.GrossCPP + _w2.TRP
                            Else
                                tmptotalPlannedGrossBudget += _w2.GrossBudget
                            End If

                            '   /JOOS
                            '   Changed so that the TotalPLannedGross budget doesnt add with itself
                            ' B4: 'totalPlannedGrossBudget += tmptotalPlannedGrossBudget
                            '
                            tmptotalPlannedNetBudget += _w2.NetBudget
                            totalPlannedGrossBudget = tmptotalPlannedGrossBudget
                            totalPlannedNetBudget = tmptotalPlannedNetBudget

                            'If tmpBook.ConfirmedGrossBudget = 0 Then
                            '    tmptotalPlannedGrossBudget = tmpBook.PlannedGrossBudget
                            'Else
                            '    tmptotalPlannedGrossBudget = tmpBook.ConfirmedGrossBudget
                            'End If
                            'Dim tmptotalPlannedNetBudget = 0
                            'tmptotalPlannedNetBudget = tmpBook.PlannedNetBudget
                            'totalPlannedGrossBudget += tmptotalPlannedGrossBudget
                            'totalPlannedNetBudget += tmptotalPlannedNetBudget
                        End If
                    Next
                Next


                'Cost

                For Each TmpCost In Campaign.Costs
                    If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                        If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnRatecard Then
                            For Each _w As cWeek In listOfWeeks
                                For Each _w2 As cWeek In tmpB.Weeks
                                    If _w.Name = _w2.Name Then
                                        tmptotalPlannedGrossBudget = TmpCost.Amount * _w2.NetBudget
                                        'tmptotalPlannedGrossBudget = TmpCost.Amount * tmpB.PlannedGrossBudget                                    
                                    End If
                                Next
                            Next
                        End If
                    End If
                Next

                'Costs that are based on the media net, like maybe a service fee based on net after discounts
                If Campaign.Costs.Count > 0 Then
                    For Each TmpCost In Campaign.Costs
                        If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                            If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnMediaNet Then
                                tmpCostAmount = TmpCost.Amount * totalPlannedNetBudget
                                totalFees += tmpCostAmount
                            End If
                        End If
                    Next
                    If ownCommission Then
                        tmpPrintNetNet = tmptotalPlannedNetBudget * (1 - ownCommissionAmount)
                        totalPlannedNetBudget = tmptotalPlannedNetBudget * (1 - ownCommissionAmount) + totalFees
                    Else
                        tmpPrintNetNet = tmptotalPlannedNetBudget * (1 - tmpChan.AgencyCommission)
                        totalPlannedNetBudget = tmptotalPlannedNetBudget * (1 - tmpChan.AgencyCommission) + totalFees
                    End If
                    totalPlannedNetNetBudget = totalPlannedNetBudget
                    tmpCTC = totalPlannedNetNetBudget + totalFees
                    For Each TmpCost In Campaign.Costs
                        If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                            If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNet Then
                                tmpCostAmount = TmpCost.Amount * totalPlannedNetBudget
                                totalFees += tmpCostAmount
                                totalPlannedNetNetBudget += TmpCost.Amount * totalPlannedNetBudget
                            End If
                        ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeFixed Then
                            tmpCostAmount = (TmpCost.Amount / amountOfChannels)
                            totalFees += tmpCostAmount
                            totalPlannedNetNetBudget += tmpCostAmount
                        ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypeOnDiscount Then
                            SumUnit = 0
                            If TmpCost.CountCostOn Is Nothing Then
                                For Each TmpWeek As Trinity.cWeek In tmpB.Weeks
                                    For Each _w As cWeek In listOfWeeks
                                        If _w.Name = TmpWeek.Name Then
                                            Dim Discount As Single = TmpWeek.GrossBudget - TmpWeek.NetBudget
                                            SumUnit += (Discount * TmpCost.Amount)
                                        End If
                                    Next
                                Next
                            Else
                                For Each tmpB In DirectCast(TmpCost.CountCostOn, Trinity.cChannel).BookingTypes
                                    For Each TmpWeek As Trinity.cWeek In tmpB.Weeks
                                        For Each _w As cWeek In listOfWeeks
                                            If _w.Name = TmpWeek.Name Then
                                                Dim Discount As Single = TmpWeek.GrossBudget - TmpWeek.NetBudget
                                                SumUnit += (Discount * TmpCost.Amount)
                                            End If
                                        Next
                                    Next
                                Next
                            End If
                            tmpCTC = tmpCTC + SumUnit
                        ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePerUnit Then
                            SumUnit = 0
                            If TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnSpots Then
                                SumUnit = SumUnit + tmpB.EstimatedSpotCount * TmpCost.Amount
                            ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnBuyingTRP Then
                                SumUnit = SumUnit + tmpB.TotalTRPBuyingTarget * TmpCost.Amount
                            ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnBuyingTRP Then
                                SumUnit = SumUnit + tmpB.TotalTRP * TmpCost.Amount
                            ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnWeeks Then
                                SumUnit = Campaign.Channels(1).BookingTypes(1).Weeks.Count * TmpCost.Amount
                            End If
                            totalFees += SumUnit
                            totalPlannedNetNetBudget = totalPlannedNetBudget + SumUnit
                        End If
                    Next

                    tmpCTC = totalPlannedNetNetBudget
                    'Costs on net net
                    For Each TmpCost In Campaign.Costs
                        If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                            If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnNetNet Then
                                totalFees += TmpCost.Amount * (totalPlannedNetNetBudget)
                                totalPlannedNetNetBudget += TmpCost.Amount * totalPlannedNetNetBudget
                            End If
                        End If
                    Next
                Else
                    If (ownCommission) Then
                        totalPlannedNetNetBudget = totalPlannedNetBudget * (1 - ownCommissionAmount)
                        tmpPrintNetNet = totalPlannedNetNetBudget
                    Else
                        totalPlannedNetNetBudget = totalPlannedNetBudget * (1 - tmpChan.AgencyCommission)
                        tmpPrintNetNet = totalPlannedNetNetBudget
                    End If
                End If

            End If



            tmpCTC = totalPlannedNetNetBudget
            totalCTC = tmpCTC

            totalPlannedFee = totalFees
            .Cells(row, 10).Value += Math.Round(totalCTC, 2) 'CTC

            .Cells(row, 4).Value += Math.Round(totalPlannedGrossBudget) 'Gross budget
            .Cells(row, 5).Value += Math.Round(tmptotalPlannedNetBudget) 'Net budget
            .Cells(row, 6).Value += Math.Round(tmpPrintNetNet) 'Net net
            Dim tmpFee As Integer = 0

            .Cells(row, 7).Value += Math.Round(totalPlannedFee) 'Fees

            .Cells(row, 8).Value = "" 'Other
            .Cells(row, 9).Value = "" 'Fond

            targets = .Cells(row, 12).Value


            If targets Is Nothing Then
                targets = targets & translateTargetName(tmpBook.BuyingTarget.TargetName) & "; "
            Else
                Dim tmpTarget = translateTargetName(tmpBook.BuyingTarget.TargetName)
                If Not targets.Contains(tmpTarget) Then
                    targets = targets & translateTargetName(tmpBook.BuyingTarget.ToString) & "; "
                End If
            End If
            .Cells(row, 12).Value = targets
        End With
    End Sub
    Function CheckChannel(ByVal tmpChName As String)
        Dim result As String = ""
        If checkNameTV4(tmpChName) Then
            result = "TV4"
        ElseIf checkNameMTG(tmpChName) Then
            result = "MTG"
        ElseIf checkNameSBS(tmpChName) Then
            result = "SBS"
        ElseIf checkNameFOX(tmpChName) Then
            result = "FOX"
        ElseIf checkNameCartoon(tmpChName) Then
            result = "Cartoon"
        ElseIf checkNameDisney(tmpChName) Then
            result = "Disney"
        ElseIf checkNameTNT(tmpChName) Then
            result = "TNT"
        ElseIf checkNameCMORE(tmpChName) Then
            result = "C MORE"
        End If
        Return result
    End Function
    Sub printStationBudgetTable()
        'Print the first table for channels
        With _sheet
            .Range("B" & 37 & ":L" & 37).Interior.Color = RGB(0, 37, 110)
            .Range("B" & 37 & ":L" & 37).Font.Color = RGB(255, 255, 255)
            .Range("B" & 38 & ":L" & 38).Interior.Color = RGB(0, 137, 178)
            .Range("B" & 38 & ":L" & 38).Font.Color = RGB(255, 255, 255)


            Dim rowCombination As Integer = 0
            Dim rowTV4Tmp = 0
            Dim rowOnlyTV4 = 0
            Dim rowTV4 = 0
            Dim rowMTG = 0
            Dim rowMTGSpecial = 0
            Dim rowSBS = 0
            Dim rowFOX = 0
            Dim rowCMORE = 0
            Dim rowTNT = 0
            Dim rowDisney = 0
            Dim rowCartoon = 0

            Dim tv4String As String = ""

            Dim firstRow As Integer = 39

            Dim tmpChannelNameInputForCheckBundling As String = ""

            'Bundle'
            If Campaign.Combinations.Count > 0 And _printExportAsCampaign Then
                For Each tmpCombo As Trinity.cCombination In Campaign.Combinations
                    For Each tmpCC As Trinity.cCombinationChannel In tmpCombo.Relations
                        Dim tmpChan As Trinity.cChannel = Nothing
                        Dim tmpBook As Trinity.cBookingType = Nothing
                        For Each c As Trinity.cChannel In Campaign.Channels
                            For Each tmpB As cBookingType In
                                From tmpB1 As cBookingType In c.BookingTypes Where tmpB1 Is tmpCC.Bookingtype
                                tmpChan = c
                                tmpBook = tmpB
                                If CheckChannel(c.ChannelName) = "TV4" Then
                                    If rowCombination = 0 Then
                                        rowCombination = tmpRow
                                        rowTV4 = rowCombination
                                    End If
                                ElseIf CheckChannel(c.ChannelName) = "MTG" Then
                                    rowCombination = tmpRow
                                    rowMTG = rowCombination
                                ElseIf CheckChannel(c.ChannelName) = "SBS" Then
                                    rowCombination = tmpRow
                                    rowSBS = rowCombination
                                ElseIf CheckChannel(c.ChannelName) = "FOX" Then
                                    rowCombination = tmpRow
                                    rowSBS = rowCombination
                                ElseIf CheckChannel(c.ChannelName) = "C MORE" Then
                                    rowCombination = tmpRow
                                    rowCMORE = rowCombination
                                ElseIf CheckChannel(c.ChannelName) = "Cartoon" Then
                                    rowCombination = tmpRow
                                    rowCartoon = rowCombination
                                ElseIf CheckChannel(c.ChannelName) = "TNT" Then
                                    rowCombination = tmpRow
                                    rowTNT = rowCombination
                                ElseIf CheckChannel(c.ChannelName) = "Disney" Then
                                    rowCombination = tmpRow
                                    rowDisney = rowCombination
                                End If
                                tmpChannelNameInputForCheckBundling = CheckChannel(c.ChannelName)
                                StationBudgetTableData(tmpChan, tmpB, tmpChannelNameInputForCheckBundling, False, tmpRow,
                                             c.ChannelName, rowCombination, True)
                                Exit For
                            Next
                        Next
                    Next
                    tmpRow = tmpRow + 1
                Next
                ExportedAsCampaignCombination = True
            End If

            'Using filteredChannels instead of Campaign.Channels to speed up the process'
            If (_printExportAsCampaign = False) Or (Campaign.Combinations.Count > 0 And _printExportAsCampaign) Or (Campaign.Combinations.Count < 1 And _printExportAsCampaign) Then
                For Each tmpChan As Trinity.cChannel In filteredChannels
                    For Each tmpBook As Trinity.cBookingType In tmpChan.BookingTypes
                        If tmpBook.BookIt Then
                            Dim jumpBookingType As Boolean = False
                            If (_printExportAsCampaign) Then
                                For i As Integer = 1 To Campaign.Combinations.Count
                                    If Campaign.Combinations.Item(i).Relations.Cast(Of cCombinationChannel)().Any(Function(tmpC) tmpC.Bookingtype Is tmpBook) Then
                                        jumpBookingType = True
                                    End If
                                Next
                            End If
                            'TV4
                            If Not jumpBookingType Then
                                If checkNameTV4(tmpChan.ChannelName) Then
                                    If _BundleTV4 And rowTV4 <> 0 And rowOnlyTV4 <> 0 Then
                                        If rowTV4 = 0 Then
                                            rowTV4 = tmpRow
                                            tv4String = tmpChan.ChannelName
                                            StationBudgetTableData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4)
                                        ElseIf _BundleTV4 Then
                                            StationBudgetTableData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4)
                                        Else
                                            rowTV4 = tmpRow
                                            tv4String = tmpChan.ChannelName
                                            StationBudgetTableData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4)
                                        End If
                                    ElseIf (rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Or (rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific Or tmpBook.IsRBS) And tmpChan.ChannelName = channelName) Then
                                        rowTV4 = rowTV4

                                        tv4String = tmpChan.ChannelName
                                        StationBudgetTableData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4)

                                    ElseIf rowOnlyTV4 <> 0 And tmpChan.ChannelName = "TV4" Or tmpChan.ChannelName = "TV4" And _BundleTV4 Then
                                        If rowOnlyTV4 = 0 Then
                                            rowOnlyTV4 = tmpRow
                                            tmpRow = tmpRow + 1
                                        End If

                                        tv4String = tmpChan.ChannelName
                                        StationBudgetTableData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowOnlyTV4)
                                    Else
                                        rowTV4 = tmpRow
                                        tmpRow = tmpRow + 1


                                        tv4String = tmpChan.ChannelName
                                        StationBudgetTableData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4)
                                    End If
                                End If
                                'MTG
                                If checkNameMTG(tmpChan.ChannelName) Then
                                    If _bundleMTGSpecial Then
                                        If tmpChan.ChannelName.Contains("TV3") Or tmpChan.ChannelName.Contains("TV6") Then
                                            If rowMTGSpecial <> 0 Then
                                            Else
                                                rowMTGSpecial = tmpRow
                                                tmpRow = tmpRow + 1
                                            End If

                                            StationBudgetTableData(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTGSpecial)
                                        Else
                                            If _BundleMTG And rowMTG <> 0 Then
                                                If rowMTG = 0 Then
                                                    rowMTG = tmpRow
                                                End If
                                            ElseIf (rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Then
                                                rowMTG = rowMTG
                                            Else
                                                rowMTG = tmpRow
                                                tmpRow = tmpRow + 1
                                            End If
                                            StationBudgetTableData(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG)
                                        End If
                                    Else
                                        If _BundleMTG And rowMTG <> 0 Then
                                            If rowMTG = 0 Then
                                                rowMTG = tmpRow
                                            End If
                                        ElseIf (rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Then
                                            rowMTG = rowMTG
                                        Else
                                            rowMTG = tmpRow
                                            tmpRow = tmpRow + 1
                                        End If
                                        StationBudgetTableData(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG)
                                    End If

                                End If
                                'SBS
                                If checkNameSBS(tmpChan.ChannelName) Then
                                    If _BundleSBS And rowSBS <> 0 Then
                                        If rowSBS = 0 Then
                                            rowSBS = tmpRow
                                        End If
                                    ElseIf (rowSBS <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowSBS <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Then
                                        rowSBS = rowSBS
                                    Else
                                        rowSBS = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    StationBudgetTableData(tmpChan, tmpBook, "SBS", _BundleSBS, tmpRow, tmpChan.ChannelName, rowSBS)
                                End If
                                'Fox
                                If checkNameFOX(tmpChan.ChannelName) Then
                                    If _BundleFOX And rowFOX <> 0 Then
                                        If rowFOX = 0 Then
                                            rowFOX = tmpRow
                                        End If
                                    ElseIf (rowFOX <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowFOX <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) Then
                                        rowFOX = rowFOX
                                    Else
                                        rowFOX = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    StationBudgetTableData(tmpChan, tmpBook, "FOX", _BundleFOX, tmpRow, tmpChan.ChannelName, rowFOX)
                                End If
                                'Cmore
                                If checkNameCMORE(tmpChan.ChannelName) Then
                                    If _BundleCMORE And rowCMORE <> 0 Then
                                        If rowCMORE = 0 Then
                                            rowCMORE = tmpRow
                                        End If
                                    ElseIf (rowCMORE <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowCMORE <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) Then
                                        rowCMORE = rowCMORE
                                    Else
                                        rowCMORE = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    StationBudgetTableData(tmpChan, tmpBook, "CMORE", _BundleCMORE, tmpRow, tmpChan.ChannelName, rowCMORE)
                                End If
                                'TNT
                                If checkNameTNT(tmpChan.ChannelName) Then
                                    If _BundleTNT And rowTNT <> 0 Then
                                        If rowTNT = 0 Then
                                            rowTNT = tmpRow
                                        End If
                                    ElseIf (rowTNT <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowTNT <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) Then
                                        rowTNT = rowTNT
                                    Else
                                        rowTNT = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    StationBudgetTableData(tmpChan, tmpBook, "TNT", _BundleTNT, tmpRow, tmpChan.ChannelName, rowTNT)
                                End If
                                'Cartoon
                                If checkNameCartoon(tmpChan.ChannelName) Then
                                    If _BundleCartoon And rowCartoon <> 0 Then
                                        If rowCartoon = 0 Then
                                            rowCartoon = tmpRow
                                        End If
                                    ElseIf rowCartoon <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And (tmpChan.ChannelName = channelName) Then
                                        rowCartoon = rowCartoon
                                    Else
                                        rowCartoon = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    StationBudgetTableData(tmpChan, tmpBook, "Cartoon", _BundleCartoon, tmpRow, tmpChan.ChannelName, rowCartoon)
                                End If
                                'Disney
                                If checkNameDisney(tmpChan.ChannelName) Then
                                    If _BundleDisney Then
                                        If rowDisney = 0 Then
                                            rowDisney = tmpRow
                                        End If
                                    ElseIf rowDisney <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And (tmpChan.ChannelName = channelName) Then
                                        rowDisney = rowDisney
                                    Else
                                        rowDisney = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    StationBudgetTableData(tmpChan, tmpBook, "Disney", _BundleDisney, tmpRow, tmpChan.ChannelName, rowDisney)
                                End If
                            End If
                        End If
                    Next
                Next
            End If

            lastRowComboTable1 = tmpRow + 1
        End With
        '_sheet.Range("B" & 39 & ":M" & 48).Numberformat = "0.0"
    End Sub
    Sub PrimarytargetTRPweeklyData(ByVal tmpChan As Trinity.cChannel, ByVal tmpB As Trinity.cBookingType, ByVal tmpGroupName As String, ByVal bundle As Boolean, ByVal row As Integer, ByVal tmpChannelName As String, Optional ByVal savedRow As Integer = 0, Optional ByVal printExportAsCampaign As Boolean = False)

        Dim tmpBook As Trinity.cBookingType = tmpB
        Dim tmpBundle = bundle

        Dim stationChannels = ""

        Dim totalPlannedGrossBudget As Integer = 0
        Dim totalPlannedNetBudget As Integer = 0

        Dim listOfSpots As New List(Of Trinity.cBookedSpot)

        Dim totalTRPForBT As Decimal = 0

        Dim target As String = ""

        With _sheet
            Dim valueTest = .Cells(savedRow, 2).Value
            'If tmpBundle And groupName = tmpGroupName Or tmpGroupName <> "TV4" Then
            '    row = savedRow
            'ElseIf tmpBundle And groupName = tmpGroupName Or tmpGroupName = "TV4" Then
            '    If valueTest IsNot Nothing Then
            '        If valueTest.ToString.Contains(tmpChan.AdEdgeNames) Then
            '            row = savedRow
            '        Else
            '            row = tmpRow
            '            tmpRow = tmpRow + 1
            '        End If
            '    Else
            '        row = savedRow
            '    End If
            If tmpBundle Then
                row = savedRow
            ElseIf tmpB.IsCompensation Or tmpB.IsSpecific And (channelName = tmpChannelName) Then
                row = savedRow
            Else
                If savedRow = 0 Then
                    row = tmpRow
                Else
                    row = savedRow
                End If
            End If
            channelName = tmpChannelName
            groupName = tmpGroupName

            .Range("B" & row & ":B" & row).Interior.Color = RGB(0, 137, 178)
            .Range("B" & row & ":B" & row).Font.Color = RGB(255, 255, 255)
            Dim test = .Cells(row, 2).Value
            'If TrinitySettings.Developer Then
            '    .Cells(row, 13).Value += tmpChan.ChannelName + " " + tmpBook.Name + " "
            'End If
            If Not printExportAsCampaign Then
                If groupName = "TV4" Then
                    If tmpBundle Then
                        If tmpChan.ChannelName = "TV4" Then
                            .Cells(row, 2).Value = "TV4"
                        Else
                            .Cells(row, 2).Value = "TV4 Nish"
                        End If
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "MTG" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "NENT"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "SBS" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Discovery"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "FOX" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Fox Int"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "Cartoon" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Cartoon"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "Disney" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Disney C/XD"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "TNT" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "TNT"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "CMORE" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "C More"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                End If
            Else
                If groupName = "MTG" Then
                    .Cells(row, 2).Value = "MTG TV"
                ElseIf groupName = "SBS" Then
                    .Cells(row, 2).Value = "SBS Combo"
                ElseIf groupName = "FOX" Then
                    .Cells(row, 2).Value = "FOX Combo"
                ElseIf groupName = "Cartoon" Then
                    .Cells(row, 2).Value = "Cartoon Combo"
                ElseIf groupName = "Disney" Then
                    .Cells(row, 2).Value = "Disney Combo"
                ElseIf groupName = "TNT" Then
                    .Cells(row, 2).Value = "TNT Combo"
                ElseIf groupName = "CMORE" Then
                    .Cells(row, 2).Value = "C More Combo"
                End If
            End If


            Dim column = 3
            For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                For Each w As cWeek In listOfWeeks
                    If w.Name = tmpWeek.Name Then
                        totalTRPForBT = tmpWeek.TRP
                        .Cells(row, column).Value += Math.Round(totalTRPForBT, 2)
                        column = column + 1
                    End If
                Next
            Next
        End With
    End Sub
    Sub printPrimarytargetTRPWeekly()
        Dim filmColumnArray As String() = {":B", ":C", ":D", ":E", ":F", ":G", ":H", ":I", ":J", ":K", ":L", ":M", ":N", ":O", ":P", ":Q", ":R", ":S", ":T", ":U", ":V", ":W", ":X", ":Y", ":Z", ":AA", ":AB", ":AC", ":AD"}
        Dim columnValue As String = filmColumnArray.GetValue(listOfWeeks.Count)
        Dim currentRow As Integer = lastRowComboTable1
        currentRow = currentRow + 1
        With _sheet
            .Range("B" & lastRowComboTable1 & columnValue & lastRowComboTable1).Interior.Color = RGB(0, 37, 110)
            .Range("B" & lastRowComboTable1 & columnValue & lastRowComboTable1).Font.Color = RGB(255, 255, 255)
            .Cells(lastRowComboTable1, 2).Value = "Primary target: TRP pr. station, pr. week"


            .Range("B" & lastRowComboTable1 + 1 & columnValue & lastRowComboTable1 + 1).Interior.Color = RGB(0, 137, 178)
            .Range("B" & lastRowComboTable1 + 1 & columnValue & lastRowComboTable1 + 1).Font.Color = RGB(255, 255, 255)
            .Cells(currentRow, 2).Value = "Network"

            ''
            'Print week headers
            For Each tmpchan As Trinity.cChannel In Campaign.Channels
                For Each tmpBook As Trinity.cBookingType In tmpchan.BookingTypes
                    If tmpBook.BookIt Then
                        Dim column As Integer = 3
                        For Each tmpWeek As Trinity.cWeek In listOfWeeks
                            '.Cells(currentRow, column).Value = currentYearForCampaign.Year.ToString() & "w" & tmpWeek.Name
                            Dim yearForWeek As Date = Trinity.Helper.FormatDateForBooking(tmpWeek.StartDate)
                            .Cells(currentRow, column).Value = yearForWeek.Year.ToString() & "w" & tmpWeek.Name
                            column = column + 1
                        Next
                        Exit For
                    End If
                Next
            Next
            currentRow = currentRow + 1
            tmpRow = currentRow

            Dim rowMTGSpecial = 0
            Dim rowOnlyTV4 = 0
            Dim rowTV4 = 0
            Dim rowMTG = 0
            Dim rowSBS = 0
            Dim rowFOX = 0
            Dim rowCMORE = 0
            Dim rowTNT = 0
            Dim rowCartoon = 0
            Dim rowDisney = 0

            Dim tv4String As String = ""

            Dim rowCombination As Integer = 0
            Dim tmpChannelNameInputForCheckBundling As String = ""

            If Campaign.Combinations.Count > 0 And _printExportAsCampaign Then

                For Each tmpCombo As Trinity.cCombination In Campaign.Combinations
                    For Each tmpCC As Trinity.cCombinationChannel In tmpCombo.Relations
                        Dim tmpChan As Trinity.cChannel = Nothing
                        Dim tmpBook As Trinity.cBookingType = Nothing
                        For Each c As Trinity.cChannel In Campaign.Channels
                            For Each tmpB As cBookingType In
                                From tmpB1 As cBookingType In c.BookingTypes Where tmpB1 Is tmpCC.Bookingtype
                                tmpChan = c
                                tmpBook = tmpB
                                If CheckChannel(c.ChannelName) = "TV4" Then
                                    If rowCombination = 0 Then
                                        rowCombination = tmpRow
                                        rowTV4 = rowCombination
                                    End If
                                ElseIf CheckChannel(c.ChannelName) = "MTG" Then
                                    rowCombination = tmpRow
                                    rowMTG = rowCombination
                                ElseIf CheckChannel(c.ChannelName) = "SBS" Then
                                    rowCombination = tmpRow
                                    rowSBS = rowCombination
                                ElseIf CheckChannel(c.ChannelName) = "FOX" Then
                                    rowCombination = tmpRow
                                    rowSBS = rowCombination
                                ElseIf CheckChannel(c.ChannelName) = "C MORE" Then
                                    rowCombination = tmpRow
                                    rowCMORE = rowCombination
                                ElseIf CheckChannel(c.ChannelName) = "Cartoon" Then
                                    rowCombination = tmpRow
                                    rowCartoon = rowCombination
                                ElseIf CheckChannel(c.ChannelName) = "TNT" Then
                                    rowCombination = tmpRow
                                    rowTNT = rowCombination
                                ElseIf CheckChannel(c.ChannelName) = "Disney" Then
                                    rowCombination = tmpRow
                                    rowDisney = rowCombination
                                End If
                                tmpChannelNameInputForCheckBundling = CheckChannel(c.ChannelName)
                                PrimarytargetTRPweeklyData(tmpChan, tmpB, tmpChannelNameInputForCheckBundling, False, tmpRow,
                                             c.ChannelName, rowCombination, True)
                                Exit For
                            Next
                        Next
                    Next
                    tmpRow = tmpRow + 1
                Next
                ExportedAsCampaignCombination = True
            End If

            'Looping with filteredChannels instead of Campaign.Channels to speed up the export'
            If (_printExportAsCampaign = False) Or (Campaign.Combinations.Count > 0 And _printExportAsCampaign) Or (Campaign.Combinations.Count < 1 And _printExportAsCampaign) Then
                For Each tmpChan As Trinity.cChannel In filteredChannels
                    For Each tmpBook As cBookingType In tmpChan.BookingTypes
                        If tmpBook.BookIt Then
                            Dim jumpBookingType As Boolean = False
                            If (_printExportAsCampaign) Then
                                For i As Integer = 1 To Campaign.Combinations.Count
                                    If Campaign.Combinations.Item(i).Relations.Cast(Of cCombinationChannel)().Any(Function(tmpC) tmpC.Bookingtype Is tmpBook) Then
                                        jumpBookingType = True
                                    End If
                                Next
                            End If
                            If Not jumpBookingType Then
                                'TV4
                                If checkNameTV4(tmpChan.ChannelName) Then
                                    If _BundleTV4 And rowTV4 <> 0 And rowOnlyTV4 <> 0 Then
                                        If rowTV4 = 0 Then
                                            rowTV4 = tmpRow
                                            tv4String = tmpChan.ChannelName
                                            PrimarytargetTRPweeklyData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4)
                                        ElseIf _BundleTV4 Then
                                            PrimarytargetTRPweeklyData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4)
                                        Else
                                            rowTV4 = tmpRow
                                            tv4String = tmpChan.ChannelName
                                            PrimarytargetTRPweeklyData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4)
                                        End If
                                    ElseIf (rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Or (rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific Or tmpBook.IsRBS) And tmpChan.ChannelName = channelName) Then
                                        rowTV4 = rowTV4

                                        tv4String = tmpChan.ChannelName
                                        PrimarytargetTRPweeklyData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4)

                                    ElseIf rowOnlyTV4 <> 0 And tmpChan.ChannelName = "TV4" Or tmpChan.ChannelName = "TV4" And _BundleTV4 Then
                                        If rowOnlyTV4 = 0 Then
                                            rowOnlyTV4 = tmpRow
                                            tmpRow = tmpRow + 1
                                        End If

                                        tv4String = tmpChan.ChannelName
                                        PrimarytargetTRPweeklyData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowOnlyTV4)
                                    Else
                                        rowTV4 = tmpRow
                                        tmpRow = tmpRow + 1

                                        tv4String = tmpChan.ChannelName
                                        PrimarytargetTRPweeklyData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4)
                                    End If
                                    currentRow = tmpRow
                                End If

                                If checkNameMTG(tmpChan.ChannelName) Then
                                    If _bundleMTGSpecial Then
                                        If tmpChan.ChannelName.Contains("TV3") Or tmpChan.ChannelName.Contains("TV6") Then
                                            If rowMTGSpecial <> 0 Then
                                            Else
                                                rowMTGSpecial = tmpRow
                                                tmpRow = tmpRow + 1
                                            End If

                                            PrimarytargetTRPweeklyData(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTGSpecial)
                                        Else
                                            If _BundleMTG And rowMTG <> 0 Then
                                                If rowMTG = 0 Then
                                                    rowMTG = tmpRow
                                                End If
                                            ElseIf (rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Then
                                                rowMTG = rowMTG
                                            Else
                                                rowMTG = tmpRow
                                                tmpRow = tmpRow + 1
                                            End If
                                            PrimarytargetTRPweeklyData(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG)
                                        End If
                                    Else
                                        If _BundleMTG And rowMTG <> 0 Then
                                            If rowMTG = 0 Then
                                                rowMTG = tmpRow
                                            End If
                                        ElseIf (rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Then
                                            rowMTG = rowMTG
                                        Else
                                            rowMTG = tmpRow
                                            tmpRow = tmpRow + 1
                                        End If
                                        PrimarytargetTRPweeklyData(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG)
                                    End If
                                    currentRow = tmpRow
                                End If
                                If checkNameSBS(tmpChan.ChannelName) Then
                                    If _BundleSBS And rowSBS <> 0 Then
                                        If rowSBS = 0 Then
                                            rowSBS = tmpRow
                                        End If
                                    ElseIf (rowSBS <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowSBS <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Then
                                        rowSBS = rowSBS
                                    Else
                                        rowSBS = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    PrimarytargetTRPweeklyData(tmpChan, tmpBook, "SBS", _BundleSBS, tmpRow, tmpChan.ChannelName, rowSBS)
                                    currentRow = tmpRow
                                End If
                                If checkNameFOX(tmpChan.ChannelName) Then
                                    If _BundleFOX And rowFOX <> 0 Then
                                        If rowFOX = 0 Then
                                            rowFOX = tmpRow
                                        End If
                                    ElseIf (rowFOX <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowFOX <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) Then
                                        rowFOX = rowFOX
                                    Else
                                        rowFOX = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    PrimarytargetTRPweeklyData(tmpChan, tmpBook, "FOX", _BundleFOX, tmpRow, tmpChan.ChannelName, rowFOX)
                                    currentRow = tmpRow
                                End If
                                If checkNameCMORE(tmpChan.ChannelName) Then
                                    If _BundleCMORE And rowCMORE <> 0 Then
                                        If rowCMORE = 0 Then
                                            rowCMORE = tmpRow
                                        End If
                                    ElseIf (rowCMORE <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowCMORE <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) Then
                                        rowCMORE = rowCMORE
                                    Else
                                        rowCMORE = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    PrimarytargetTRPweeklyData(tmpChan, tmpBook, "CMORE", _BundleCMORE, tmpRow, tmpChan.ChannelName, rowCMORE)
                                    currentRow = tmpRow
                                End If
                                If checkNameTNT(tmpChan.ChannelName) Then
                                    If _BundleTNT And rowTNT <> 0 Then
                                        If rowTNT = 0 Then
                                            rowTNT = tmpRow
                                        End If
                                    ElseIf rowTNT <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And (tmpChan.ChannelName = channelName) Then
                                        rowTNT = rowTNT
                                    Else
                                        rowTNT = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    PrimarytargetTRPweeklyData(tmpChan, tmpBook, "TNT", _BundleTNT, tmpRow, tmpChan.ChannelName, rowTNT)
                                    currentRow = tmpRow
                                End If
                                If checkNameCartoon(tmpChan.ChannelName) Then
                                    If _BundleCartoon And rowCartoon <> 0 Then
                                        If rowCartoon = 0 Then
                                            rowCartoon = tmpRow
                                        End If
                                    ElseIf rowCartoon <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And (tmpChan.ChannelName = channelName) Then
                                        rowCartoon = rowCartoon
                                    Else
                                        rowCartoon = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    PrimarytargetTRPweeklyData(tmpChan, tmpBook, "Cartoon", _BundleCartoon, tmpRow, tmpChan.ChannelName, rowCartoon)
                                    currentRow = tmpRow
                                End If
                                If checkNameDisney(tmpChan.ChannelName) Then
                                    If _BundleDisney And rowDisney <> 0 Then
                                        If rowDisney = 0 Then
                                            rowDisney = tmpRow
                                        End If
                                    ElseIf rowDisney <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And (tmpChan.ChannelName = channelName) Then
                                        rowDisney = rowDisney
                                    Else
                                        rowDisney = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    PrimarytargetTRPweeklyData(tmpChan, tmpBook, "Disney", _BundleDisney, tmpRow, tmpChan.ChannelName, rowDisney)
                                    currentRow = tmpRow
                                End If
                            End If
                        End If
                    Next
                Next
            End If
        End With
        '_sheet.Range("B" & lastRowComboTable1 - 1 & ":M" & currentRow).Numberformat = "0.0"
        lastRowComboTable2 = currentRow + 1
    End Sub

    Sub BuyingtargetTRPWeeklyDATA(ByVal tmpChan As Trinity.cChannel, ByVal tmpB As Trinity.cBookingType, ByVal tmpGroupName As String, ByVal bundle As Boolean, ByVal row As Integer, ByVal tmpChannelName As String, Optional ByVal savedRow As Integer = 0, Optional ByVal printExportAsCampaign As Boolean = False)

        Dim tmpBook As Trinity.cBookingType = tmpB
        Dim tmpBundle = bundle

        Dim stationChannels = ""

        Dim totalPlannedGrossBudget As Integer = 0
        Dim totalPlannedNetBudget As Integer = 0

        Dim listOfSpots As New List(Of Trinity.cBookedSpot)

        Dim totalTRPForBT As Decimal = 0

        Dim target As String = ""

        With _sheet
            Dim valueTest = .Cells(savedRow, 2).Value
            'If tmpBundle And groupName = tmpGroupName Or tmpGroupName <> "TV4" Then
            '    row = savedRow
            'ElseIf tmpBundle And groupName = tmpGroupName Or tmpGroupName = "TV4" Then
            '    If valueTest IsNot Nothing Then
            '        If valueTest.ToString.Contains(tmpChan.AdEdgeNames) Then
            '            row = savedRow
            '        Else
            '            row = tmpRow
            '            tmpRow = tmpRow + 1
            '        End If
            '    Else
            '        row = savedRow
            '    End If
            If tmpBundle Then
                row = savedRow
            ElseIf tmpB.IsCompensation Or tmpB.IsSpecific And (channelName = tmpChannelName) Then
                row = savedRow
            Else
                If savedRow = 0 Then
                    row = tmpRow
                Else
                    row = savedRow
                End If
            End If
            channelName = tmpChannelName
            groupName = tmpGroupName

            .Range("B" & row & ":B" & row).Interior.Color = RGB(0, 137, 178)
            .Range("B" & row & ":B" & row).Font.Color = RGB(255, 255, 255)
            Dim test = .Cells(row, 2).Value
            'If TrinitySettings.Developer Then
            '    .Cells(row, 13).Value += tmpChan.ChannelName + " " + tmpBook.Name + " "
            'End If
            If Not printExportAsCampaign Then
                If groupName = "TV4" Then
                    If tmpBundle Then
                        If tmpChan.ChannelName = "TV4" Then
                            .Cells(row, 2).Value = "TV4"
                        Else
                            .Cells(row, 2).Value = "TV4 Nish"
                        End If
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If

                    End If
                ElseIf groupName = "MTG" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "NENT"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "SBS" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Discovery"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "FOX" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Fox Int"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "Cartoon" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Cartoon"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "Disney" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Disney C/XD"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "TNT" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "TNT"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "CMORE" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "C More"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                End If
            Else
                If groupName = "MTG" Then
                    .Cells(row, 2).Value = "MTG TV"
                ElseIf groupName = "SBS" Then
                    .Cells(row, 2).Value = "SBS Combo"
                ElseIf groupName = "FOX" Then
                    .Cells(row, 2).Value = "FOX Combo"
                ElseIf groupName = "Cartoon" Then
                    .Cells(row, 2).Value = "Cartoon Combo"
                ElseIf groupName = "Disney" Then
                    .Cells(row, 2).Value = "Disney Combo"
                ElseIf groupName = "TNT" Then
                    .Cells(row, 2).Value = "TNT Combo"
                ElseIf groupName = "CMORE" Then
                    .Cells(row, 2).Value = "C More Combo"
                End If
            End If
            Dim column = 3
            For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                For Each w As cWeek In listOfWeeks
                    If w.Name = tmpWeek.Name Then
                        totalTRPForBT = tmpWeek.TRPBuyingTarget
                        .Cells(row, column).Value += Math.Round(totalTRPForBT, 2)
                        column = column + 1
                    End If
                Next
            Next
        End With
    End Sub
    Sub printBuyingtargetTRPWeekly()
        Dim filmColumnArray As String()
        filmColumnArray = {":B", ":C", ":D", ":E", ":F", ":G", ":H", ":I", ":J", ":K", ":L", ":M", ":N", ":O", ":P", ":Q", ":R", ":S", ":T", ":U", ":V", ":W", ":X", ":Y", ":Z", ":AA", ":AB", ":AC", ":AD"}
        Dim columnValue As String = filmColumnArray.GetValue(listOfWeeks.Count)
        Dim currentRow As Integer = lastRowComboTable2
        currentRow = currentRow + 1
        With _sheet
            .Range("B" & lastRowComboTable2 & columnValue & lastRowComboTable2).Interior.Color = RGB(0, 37, 110)
            .Range("B" & lastRowComboTable2 & columnValue & lastRowComboTable2).Font.Color = RGB(255, 255, 255)
            .Cells(lastRowComboTable2, 2).Value = "Buying target: TRP pr. station, pr. week"


            .Range("B" & lastRowComboTable2 + 1 & columnValue & lastRowComboTable2 + 1).Interior.Color = RGB(0, 137, 178)
            .Range("B" & lastRowComboTable2 + 1 & columnValue & lastRowComboTable2 + 1).Font.Color = RGB(255, 255, 255)
            .Cells(currentRow, 2).Value = "Network"


            For Each tmpchan As Trinity.cChannel In Campaign.Channels
                For Each tmpBook As Trinity.cBookingType In tmpchan.BookingTypes
                    If tmpBook.BookIt Then
                        Dim column As Integer = 3
                        For Each tmpWeek As Trinity.cWeek In listOfWeeks
                            Dim yearForWeek As Date = Trinity.Helper.FormatDateForBooking(tmpWeek.StartDate)
                            .Cells(currentRow, column).Value = yearForWeek.Year.ToString() & "w" & tmpWeek.Name
                            column = column + 1
                        Next
                        Exit For
                    End If
                Next
            Next
            currentRow = currentRow + 1
            tmpRow = currentRow

            Dim rowOnlyTV4 = 0
            Dim rowTV4 = 0
            Dim rowMTG = 0
            Dim rowMTGSpecial = 0
            Dim rowSBS = 0
            Dim rowFOX = 0
            Dim rowCMORE = 0
            Dim rowCartoon = 0
            Dim rowDisney = 0
            Dim rowTNT = 0

            Dim tv4String As String = ""

            Dim rowCombination As Integer = 0
            Dim tmpChannelNameInputForCheckBundling As String = ""
            'use filteredChannels'
            If Campaign.Combinations.Count > 0 And _printExportAsCampaign Then
                For Each tmpCombo As Trinity.cCombination In Campaign.Combinations
                    For Each tmpCC As Trinity.cCombinationChannel In tmpCombo.Relations
                        Dim tmpChan As Trinity.cChannel = Nothing
                        Dim tmpBook As Trinity.cBookingType = Nothing
                        For Each c As Trinity.cChannel In Campaign.Channels
                            For Each tmpB As cBookingType In
                                From tmpB1 As cBookingType In c.BookingTypes Where tmpB1 Is tmpCC.Bookingtype
                                tmpChan = c
                                tmpBook = tmpB
                                If CheckChannel(c.ChannelName) = "TV4" Then
                                    If rowCombination = 0 Then
                                        rowCombination = tmpRow
                                        rowTV4 = rowCombination
                                    End If
                                ElseIf CheckChannel(c.ChannelName) = "MTG" Then
                                    rowCombination = tmpRow
                                    rowMTG = rowCombination
                                ElseIf CheckChannel(c.ChannelName) = "SBS" Then
                                    rowCombination = tmpRow
                                    rowSBS = rowCombination
                                ElseIf CheckChannel(c.ChannelName) = "FOX" Then
                                    rowCombination = tmpRow
                                    rowSBS = rowCombination
                                ElseIf CheckChannel(c.ChannelName) = "C MORE" Then
                                    rowCombination = tmpRow
                                    rowCMORE = rowCombination
                                ElseIf CheckChannel(c.ChannelName) = "Cartoon" Then
                                    rowCombination = tmpRow
                                    rowCartoon = rowCombination
                                ElseIf CheckChannel(c.ChannelName) = "TNT" Then
                                    rowCombination = tmpRow
                                    rowTNT = rowCombination
                                ElseIf CheckChannel(c.ChannelName) = "Disney" Then
                                    rowCombination = tmpRow
                                    rowDisney = rowCombination
                                End If
                                tmpChannelNameInputForCheckBundling = CheckChannel(c.ChannelName)
                                BuyingtargetTRPWeeklyDATA(tmpChan, tmpB, tmpChannelNameInputForCheckBundling, False, tmpRow,
                                             c.ChannelName, rowCombination, True)
                                Exit For
                            Next
                        Next
                    Next
                    tmpRow = tmpRow + 1
                Next
                ExportedAsCampaignCombination = True
            End If

            'Looping with filteredChannels instead of Campaign.Channels to speed'
            If (_printExportAsCampaign = False) Or (Campaign.Combinations.Count > 0 And _printExportAsCampaign) Or (Campaign.Combinations.Count < 1 And _printExportAsCampaign) Then
                For Each tmpChan As Trinity.cChannel In filteredChannels
                    For Each tmpBook As Trinity.cBookingType In tmpChan.BookingTypes
                        If tmpBook.BookIt Then
                            Dim jumpBookingType As Boolean = False
                            If (_printExportAsCampaign) Then
                                For i As Integer = 1 To Campaign.Combinations.Count
                                    If Campaign.Combinations.Item(i).Relations.Cast(Of cCombinationChannel)().Any(Function(tmpC) tmpC.Bookingtype Is tmpBook) Then
                                        jumpBookingType = True
                                    End If
                                Next
                            End If
                            If Not jumpBookingType Then
                                'TV4
                                If checkNameTV4(tmpChan.ChannelName) Then
                                    If _BundleTV4 And rowTV4 <> 0 And rowOnlyTV4 <> 0 Then
                                        If rowTV4 = 0 Then
                                            rowTV4 = tmpRow
                                            tv4String = tmpChan.ChannelName
                                            BuyingtargetTRPWeeklyDATA(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4)
                                        ElseIf _BundleTV4 Then
                                            BuyingtargetTRPWeeklyDATA(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4)
                                        Else
                                            rowTV4 = tmpRow
                                            tv4String = tmpChan.ChannelName
                                            BuyingtargetTRPWeeklyDATA(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4)
                                        End If
                                    ElseIf (rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Or (rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific Or tmpBook.IsRBS) And tmpChan.ChannelName = channelName) Then
                                        rowTV4 = rowTV4

                                        tv4String = tmpChan.ChannelName
                                        BuyingtargetTRPWeeklyDATA(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4)

                                    ElseIf rowOnlyTV4 <> 0 And tmpChan.ChannelName = "TV4" Or tmpChan.ChannelName = "TV4" And _BundleTV4 Then
                                        If rowOnlyTV4 = 0 Then
                                            rowOnlyTV4 = tmpRow
                                            tmpRow = tmpRow + 1
                                        End If

                                        tv4String = tmpChan.ChannelName
                                        BuyingtargetTRPWeeklyDATA(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowOnlyTV4)
                                    Else
                                        rowTV4 = tmpRow
                                        tmpRow = tmpRow + 1


                                        tv4String = tmpChan.ChannelName
                                        BuyingtargetTRPWeeklyDATA(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4)
                                    End If
                                    currentRow = tmpRow
                                End If
                                If checkNameMTG(tmpChan.ChannelName) Then
                                    If _bundleMTGSpecial Then
                                        If tmpChan.ChannelName.Contains("TV3") Or tmpChan.ChannelName.Contains("TV6") Then
                                            If rowMTGSpecial <> 0 Then
                                            Else
                                                rowMTGSpecial = tmpRow
                                                tmpRow = tmpRow + 1
                                            End If

                                            BuyingtargetTRPWeeklyDATA(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTGSpecial)
                                        Else
                                            If _BundleMTG And rowMTG <> 0 Then
                                                If rowMTG = 0 Then
                                                    rowMTG = tmpRow
                                                End If
                                            ElseIf (rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Then
                                                rowMTG = rowMTG
                                            Else
                                                rowMTG = tmpRow
                                                tmpRow = tmpRow + 1
                                            End If
                                            BuyingtargetTRPWeeklyDATA(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG)
                                        End If
                                    Else
                                        If _BundleMTG And rowMTG <> 0 Then
                                            If rowMTG = 0 Then
                                                rowMTG = tmpRow
                                            End If
                                        ElseIf (rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Then
                                            rowMTG = rowMTG
                                        Else
                                            rowMTG = tmpRow
                                            tmpRow = tmpRow + 1
                                        End If
                                        BuyingtargetTRPWeeklyDATA(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG)
                                    End If
                                    currentRow = tmpRow
                                End If
                                If checkNameSBS(tmpChan.ChannelName) Then
                                    If _BundleSBS And rowSBS <> 0 Then
                                        If rowSBS = 0 Then
                                            rowSBS = tmpRow
                                        End If
                                    ElseIf (rowSBS <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowSBS <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Then
                                        rowSBS = rowSBS
                                    Else
                                        rowSBS = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    BuyingtargetTRPWeeklyDATA(tmpChan, tmpBook, "SBS", _BundleSBS, tmpRow, tmpChan.ChannelName, rowSBS)
                                    currentRow = tmpRow
                                End If
                                If checkNameFOX(tmpChan.ChannelName) Then
                                    If _BundleFOX And rowFOX <> 0 Then
                                        If rowFOX = 0 Then
                                            rowFOX = tmpRow
                                        End If
                                    ElseIf (rowFOX <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowFOX <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) Then
                                        rowFOX = rowFOX
                                    Else
                                        rowFOX = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    BuyingtargetTRPWeeklyDATA(tmpChan, tmpBook, "FOX", _BundleFOX, tmpRow, tmpChan.ChannelName, rowFOX)
                                    currentRow = tmpRow
                                End If
                                If checkNameCMORE(tmpChan.ChannelName) Then
                                    If _BundleCMORE And rowCMORE <> 0 Then
                                        If rowCMORE = 0 Then
                                            rowCMORE = tmpRow
                                        End If
                                    ElseIf (rowCMORE <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowCMORE <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) Then
                                        rowCMORE = rowCMORE
                                    Else
                                        rowCMORE = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    BuyingtargetTRPWeeklyDATA(tmpChan, tmpBook, "CMORE", _BundleCMORE, tmpRow, tmpChan.ChannelName, rowCMORE)
                                    currentRow = tmpRow
                                End If
                                If checkNameTNT(tmpChan.ChannelName) Then
                                    If _BundleTNT And rowTNT <> 0 Then
                                        If rowTNT = 0 Then
                                            rowTNT = tmpRow
                                        End If
                                    ElseIf rowTNT <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And (tmpChan.ChannelName = channelName) Then
                                        rowTNT = rowTNT
                                    Else
                                        rowTNT = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    BuyingtargetTRPWeeklyDATA(tmpChan, tmpBook, "TNT", _BundleTNT, tmpRow, tmpChan.ChannelName, rowTNT)
                                    currentRow = tmpRow
                                End If
                                If checkNameCartoon(tmpChan.ChannelName) Then
                                    If _BundleCartoon And rowCartoon <> 0 Then
                                        If rowCartoon = 0 Then
                                            rowCartoon = tmpRow
                                        End If
                                    ElseIf rowCartoon <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And (tmpChan.ChannelName = channelName) Then
                                        rowCartoon = rowCartoon
                                    Else
                                        rowCartoon = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    BuyingtargetTRPWeeklyDATA(tmpChan, tmpBook, "Cartoon", _BundleCartoon, tmpRow, tmpChan.ChannelName, rowCartoon)
                                    currentRow = tmpRow
                                End If
                                If checkNameDisney(tmpChan.ChannelName) Then
                                    If _BundleDisney And rowDisney <> 0 Then
                                        If rowDisney = 0 Then
                                            rowDisney = tmpRow
                                        End If
                                    ElseIf rowDisney <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And (tmpChan.ChannelName = channelName) Then
                                        rowDisney = rowDisney
                                    Else
                                        rowDisney = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    BuyingtargetTRPWeeklyDATA(tmpChan, tmpBook, "Disney", _BundleDisney, tmpRow, tmpChan.ChannelName, rowDisney)
                                    currentRow = tmpRow
                                End If
                            End If
                        End If
                    Next
                Next
            End If
        End With
        '_sheet.Range("B" & lastRowComboTable1 - 1 & ":M" & currentRow).Numberformat = "0.0"

        If currentRow < tmpRow Then
            currentRow = tmpRow
        End If

        lastRowComboTable3 = currentRow + 1
    End Sub

    Sub PrimTargetTRPSpotlengthData(ByVal tmpChan As Trinity.cChannel, ByVal tmpB As Trinity.cBookingType, ByVal tmpGroupName As String, ByVal bundle As Boolean, ByVal row As Integer, ByVal tmpChannelName As String, Optional ByVal savedRow As Integer = 0, Optional rowheader As Integer = 0, Optional ByVal printExportAsCampaign As Boolean = False)

        Dim tmpBook As Trinity.cBookingType = tmpB
        Dim tmpBundle = bundle

        Dim stationChannels = ""

        Dim totalPlannedGrossBudget As Integer = 0
        Dim totalPlannedNetBudget As Integer = 0

        Dim listOfSpots As New List(Of Trinity.cBookedSpot)

        Dim target As String = ""

        With _sheet
            Dim valueTest = .Cells(savedRow, 2).Value
            'If tmpBundle And groupName = tmpGroupName Or tmpGroupName <> "TV4" Then
            '    row = savedRow
            'ElseIf tmpBundle And groupName = tmpGroupName Or tmpGroupName = "TV4" Then
            '    If valueTest IsNot Nothing Then
            '        If valueTest.ToString.Contains(tmpChan.AdEdgeNames) Then
            '            row = savedRow
            '        Else
            '            row = tmpRow
            '            tmpRow = tmpRow + 1
            '        End If
            '    Else
            '        row = savedRow
            '    End If
            If tmpBundle Then
                row = savedRow
            ElseIf tmpB.IsCompensation Or tmpB.IsSpecific And (channelName = tmpChannelName) Then
                row = savedRow
            Else
                If savedRow = 0 Then
                    row = tmpRow
                Else
                    row = savedRow
                End If
            End If
            channelName = tmpChannelName
            groupName = tmpGroupName

            .Range("B" & row & ":B" & row).Interior.Color = RGB(0, 137, 178)
            .Range("B" & row & ":B" & row).Font.Color = RGB(255, 255, 255)
            Dim test = .Cells(row, 2).Value
            'If TrinitySettings.Developer Then
            '    .Cells(row, 13).Value += tmpChan.ChannelName + " " + tmpBook.Name + " "
            'End If
            If Not printExportAsCampaign Then
                If groupName = "TV4" Then
                    If tmpBundle Then
                        If tmpChan.ChannelName = "TV4" Then
                            .Cells(row, 2).Value = "TV4"
                        Else
                            .Cells(row, 2).Value = "TV4 Nish"
                        End If
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "MTG" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "NENT"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "SBS" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Discovery"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "FOX" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Fox Int"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "Cartoon" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Cartoon"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "Disney" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Disney C/XD"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "TNT" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "TNT"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "CMORE" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "C More"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                End If
            Else
                If groupName = "MTG" Then
                    .Cells(row, 2).Value = "MTG TV"
                ElseIf groupName = "SBS" Then
                    .Cells(row, 2).Value = "SBS Combo"
                ElseIf groupName = "FOX" Then
                    .Cells(row, 2).Value = "FOX Combo"
                ElseIf groupName = "Cartoon" Then
                    .Cells(row, 2).Value = "Cartoon Combo"
                ElseIf groupName = "Disney" Then
                    .Cells(row, 2).Value = "Disney Combo"
                ElseIf groupName = "TNT" Then
                    .Cells(row, 2).Value = "TNT Combo"
                ElseIf groupName = "CMORE" Then
                    .Cells(row, 2).Value = "C More Combo"
                End If
            End If

            Dim column = 3
            If row = 0 Then
                row = row
                .Range("B" & row & ":B" & row).Interior.Color = RGB(0, 137, 178)
                .Range("B" & row & ":B" & row).Font.Color = RGB(255, 255, 255)
            End If
            For x As Integer = 0 To _amountOfFilmLength.Count - 1
                Dim tmpFilm = ""
                Dim tmpTotalShareForFilm As Decimal = 0
                Dim totalTRPForBT As Double = 0
                Dim trpForFilm As Double = 0
                Dim trpForShare As Double = 0
                Dim tmpShare As Double = 0
                For Each tmpF As Trinity.cFilm In listOfFilms
                    If _amountOfFilmLength(x).ToString = tmpF.FilmLength Then
                        For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                            For Each w As cWeek In listOfWeeks
                                If tmpWeek.Name = w.Name Then
                                    trpForShare = tmpWeek.TRP
                                    If (tmpF.Filmcode <> "") Then
                                        If tmpWeek.Films(tmpF.Name) IsNot Nothing Then
                                            tmpShare = tmpWeek.Films(tmpF.Name).Share()
                                            tmpFilm = tmpWeek.Films(tmpF.Name).FilmLength
                                        End If
                                    Else
                                        tmpShare = tmpWeek.Films(tmpF.Name).Share()
                                        tmpFilm = tmpWeek.Films(tmpF.Name).FilmLength
                                    End If
                                    If trpForShare <> 0 Then
                                        trpForFilm += trpForShare * (tmpShare / 100)
                                    End If
                                End If
                            Next
                        Next
                    End If
                Next
                totalTRPForBT = trpForFilm
                If .Cells(rowheader, column).Value Is Nothing Then
                    .Cells(rowheader, column).Value = tmpFilm
                End If
                .Cells(row, column).Value += Math.Round(totalTRPForBT, 2)
                column = column + 1
            Next
        End With
    End Sub
    Sub printPrimarytargetTRPSpotlength()
        Dim filmColumnArray As String() = {":B", ":C", ":D", ":E", ":F", ":G", ":H", ":I", ":J", ":K", ":L", ":M", ":N", ":O", ":P", ":Q", ":R", ":S", ":T", ":U", ":V", ":W", ":X", ":Y", ":Z", ":AA", ":AB", ":AC", ":AD"}
        Dim columnValue As String
        columnValue = filmColumnArray.GetValue(listOfFilms.Count)

        Dim currentRow As Integer = lastRowComboTable3
        Dim rowHeader = currentRow
        With _sheet
            .Range("B" & currentRow + 1 & columnValue & currentRow + 1).Interior.Color = RGB(0, 137, 178)
            .Range("B" & currentRow + 1 & columnValue & currentRow + 1).Font.Color = RGB(255, 255, 255)

            .Range("B" & currentRow & columnValue & currentRow).Interior.Color = RGB(0, 37, 110)
            .Range("B" & currentRow & columnValue & currentRow).Font.Color = RGB(255, 255, 255)
            .Cells(currentRow, 2).Value = "Primary target: TRP pr. station, pr. spotlength"
            currentRow = currentRow + 1
            .Cells(currentRow, 2).Value = "Network"

            rowHeader = currentRow
        End With
        currentRow = currentRow + 1
        tmpRow = currentRow

        Dim rowOnlyTV4 = 0
        Dim rowTV4 = 0
        Dim rowMTG = 0
        Dim rowMTGSpecial = 0
        Dim rowSBS = 0
        Dim rowFOX = 0
        Dim rowCMORE = 0
        Dim rowCartoon = 0
        Dim rowDisney = 0
        Dim rowTNT = 0
        Dim tv4String = ""

        Dim rowCombination As Integer = 0
        Dim tmpChannelNameInputForCheckBundling As String = ""

        If Campaign.Combinations.Count > 0 And _printExportAsCampaign Then
            For Each tmpCombo As Trinity.cCombination In Campaign.Combinations
                For Each tmpCC As Trinity.cCombinationChannel In tmpCombo.Relations
                    Dim tmpChan As Trinity.cChannel = Nothing
                    Dim tmpBook As Trinity.cBookingType = Nothing
                    For Each c As Trinity.cChannel In Campaign.Channels
                        For Each tmpB As cBookingType In
                            From tmpB1 As cBookingType In c.BookingTypes Where tmpB1 Is tmpCC.Bookingtype
                            tmpChan = c
                            tmpBook = tmpB
                            If CheckChannel(c.ChannelName) = "TV4" Then
                                If rowCombination = 0 Then
                                    rowCombination = tmpRow
                                    rowTV4 = rowCombination
                                End If
                            ElseIf CheckChannel(c.ChannelName) = "MTG" Then
                                rowCombination = tmpRow
                                rowMTG = rowCombination
                            ElseIf CheckChannel(c.ChannelName) = "SBS" Then
                                rowCombination = tmpRow
                                rowSBS = rowCombination
                            ElseIf CheckChannel(c.ChannelName) = "FOX" Then
                                rowCombination = tmpRow
                                rowSBS = rowCombination
                            ElseIf CheckChannel(c.ChannelName) = "C MORE" Then
                                rowCombination = tmpRow
                                rowCMORE = rowCombination
                            ElseIf CheckChannel(c.ChannelName) = "Cartoon" Then
                                rowCombination = tmpRow
                                rowCartoon = rowCombination
                            ElseIf CheckChannel(c.ChannelName) = "TNT" Then
                                rowCombination = tmpRow
                                rowTNT = rowCombination
                            ElseIf CheckChannel(c.ChannelName) = "Disney" Then
                                rowCombination = tmpRow
                                rowDisney = rowCombination
                            End If
                            tmpChannelNameInputForCheckBundling = CheckChannel(c.ChannelName)
                            PrimTargetTRPSpotlengthData(tmpChan, tmpB, tmpChannelNameInputForCheckBundling, False, tmpRow,
                                         c.ChannelName, rowCombination, rowHeader, True)
                            Exit For
                        Next
                    Next
                Next
                tmpRow = tmpRow + 1
            Next
            ExportedAsCampaignCombination = True
        End If

        With _sheet
            'TODO- Change Campaign.Channels to filteredChannels'
            If (_printExportAsCampaign = False) Or (Campaign.Combinations.Count > 0 And _printExportAsCampaign) Or (Campaign.Combinations.Count < 1 And _printExportAsCampaign) Then
                For Each tmpChan As Trinity.cChannel In Campaign.Channels
                    For Each tmpBook As Trinity.cBookingType In tmpChan.BookingTypes
                        If tmpBook.BookIt Then
                            Dim jumpBookingType As Boolean = False
                            If (_printExportAsCampaign) Then
                                For i As Integer = 1 To Campaign.Combinations.Count
                                    If Campaign.Combinations.Item(i).Relations.Cast(Of cCombinationChannel)().Any(Function(tmpC) tmpC.Bookingtype Is tmpBook) Then
                                        jumpBookingType = True
                                    End If
                                Next
                            End If
                            If (Not jumpBookingType) Then
                                If checkNameTV4(tmpChan.ChannelName) Then
                                    If _BundleTV4 And rowTV4 <> 0 And rowOnlyTV4 <> 0 Then
                                        If rowTV4 = 0 Then
                                            rowTV4 = tmpRow
                                            tv4String = tmpChan.ChannelName
                                            PrimTargetTRPSpotlengthData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)
                                        ElseIf _BundleTV4 Then
                                            PrimTargetTRPSpotlengthData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)
                                        Else
                                            rowTV4 = tmpRow
                                            tv4String = tmpChan.ChannelName
                                            PrimTargetTRPSpotlengthData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)
                                        End If
                                    ElseIf (rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Or (rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific Or tmpBook.IsRBS) And tmpChan.ChannelName = channelName) Then
                                        rowTV4 = rowTV4

                                        tv4String = tmpChan.ChannelName
                                        PrimTargetTRPSpotlengthData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)

                                    ElseIf rowOnlyTV4 <> 0 And tmpChan.ChannelName = "TV4" Or tmpChan.ChannelName = "TV4" And _BundleTV4 Then
                                        If rowOnlyTV4 = 0 Then
                                            rowOnlyTV4 = tmpRow
                                            tmpRow = tmpRow + 1
                                        End If

                                        tv4String = tmpChan.ChannelName
                                        PrimTargetTRPSpotlengthData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowOnlyTV4, rowHeader)
                                    Else
                                        rowTV4 = tmpRow
                                        tmpRow = tmpRow + 1

                                        tv4String = tmpChan.ChannelName
                                        PrimTargetTRPSpotlengthData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)
                                    End If
                                    currentRow = tmpRow
                                End If
                                If checkNameMTG(tmpChan.ChannelName) Then
                                    If _bundleMTGSpecial Then
                                        If tmpChan.ChannelName.Contains("TV3") Or tmpChan.ChannelName.Contains("TV6") Then
                                            If rowMTGSpecial <> 0 Then
                                            Else
                                                rowMTGSpecial = tmpRow
                                                tmpRow = tmpRow + 1
                                            End If

                                            PrimTargetTRPSpotlengthData(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTGSpecial, rowHeader)
                                        Else
                                            If _BundleMTG And rowMTG <> 0 Then
                                                If rowMTG = 0 Then
                                                    rowMTG = tmpRow
                                                End If
                                            ElseIf (rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Then
                                                rowMTG = rowMTG
                                            Else
                                                rowMTG = tmpRow
                                                tmpRow = tmpRow + 1
                                            End If
                                            PrimTargetTRPSpotlengthData(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG, rowHeader)
                                        End If
                                    Else
                                        If _BundleMTG And rowMTG <> 0 Then
                                            If rowMTG = 0 Then
                                                rowMTG = tmpRow
                                            End If
                                        ElseIf (rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Then
                                            rowMTG = rowMTG
                                        Else
                                            rowMTG = tmpRow
                                            tmpRow = tmpRow + 1
                                        End If
                                        PrimTargetTRPSpotlengthData(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG, rowHeader)
                                    End If
                                End If
                                If checkNameSBS(tmpChan.ChannelName) Then
                                    If _BundleSBS And rowSBS <> 0 Then
                                        If rowSBS = 0 Then
                                            rowSBS = tmpRow
                                        End If
                                    ElseIf (rowSBS <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowSBS <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Then
                                        rowSBS = rowSBS
                                    Else
                                        rowSBS = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    PrimTargetTRPSpotlengthData(tmpChan, tmpBook, "SBS", _BundleSBS, tmpRow, tmpChan.ChannelName, rowSBS, rowHeader)
                                    currentRow = tmpRow
                                End If
                                If checkNameFOX(tmpChan.ChannelName) Then
                                    If _BundleFOX And rowFOX <> 0 Then
                                        If rowFOX = 0 Then
                                            rowFOX = tmpRow
                                        End If
                                    ElseIf (rowFOX <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowFOX <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) Then
                                        rowFOX = rowFOX
                                    Else
                                        rowFOX = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    PrimTargetTRPSpotlengthData(tmpChan, tmpBook, "FOX", _BundleFOX, tmpRow, tmpChan.ChannelName, rowFOX, rowHeader)
                                    currentRow = tmpRow
                                End If
                                If checkNameCMORE(tmpChan.ChannelName) Then
                                    If _BundleCMORE And rowCMORE <> 0 Then
                                        If rowCMORE = 0 Then
                                            rowCMORE = tmpRow
                                        End If
                                    ElseIf (rowCMORE <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowCMORE <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) Then
                                        rowCMORE = rowCMORE
                                    Else
                                        rowCMORE = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    PrimTargetTRPSpotlengthData(tmpChan, tmpBook, "CMORE", _BundleCMORE, tmpRow, tmpChan.ChannelName, rowCMORE, rowHeader)
                                    currentRow = tmpRow
                                End If
                                If checkNameCartoon(tmpChan.ChannelName) Then
                                    If _BundleCartoon And rowCartoon <> 0 Then
                                        If rowCartoon = 0 Then
                                            rowCartoon = tmpRow
                                        End If
                                    ElseIf rowCartoon <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And (tmpChan.ChannelName = channelName) Then
                                        rowCartoon = rowCartoon
                                    Else
                                        rowCartoon = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    PrimTargetTRPSpotlengthData(tmpChan, tmpBook, "Cartoon", _BundleCartoon, tmpRow, tmpChan.ChannelName, rowCartoon, rowHeader)
                                    currentRow = tmpRow
                                End If
                                If checkNameDisney(tmpChan.ChannelName) Then
                                    If _BundleDisney And rowDisney <> 0 Then
                                        If rowDisney = 0 Then
                                            rowDisney = tmpRow
                                        End If
                                    ElseIf rowDisney <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And (tmpChan.ChannelName = channelName) Then
                                        rowDisney = rowDisney
                                    Else
                                        rowDisney = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    PrimTargetTRPSpotlengthData(tmpChan, tmpBook, "Disney", _BundleDisney, tmpRow, tmpChan.ChannelName, rowDisney, rowHeader)
                                    currentRow = tmpRow
                                End If
                                If checkNameTNT(tmpChan.ChannelName) Then
                                    If _BundleTNT And rowTNT <> 0 Then
                                        If rowTNT = 0 Then
                                            rowTNT = tmpRow
                                        End If
                                    ElseIf rowTNT <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And (tmpChan.ChannelName = channelName) Then
                                        rowTNT = rowTNT
                                    Else
                                        rowTNT = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    PrimTargetTRPSpotlengthData(tmpChan, tmpBook, "TNT", _BundleTNT, tmpRow, tmpChan.ChannelName, rowTNT, rowHeader)
                                    currentRow = tmpRow
                                End If
                            End If
                        End If
                    Next
                Next
            End If
        End With
        '_sheet.Range("B" & lastRowComboTable1 - 1 & ":M" & currentRow).Numberformat = "0.0"
        lastRowComboTable4 = currentRow + 1
    End Sub
    Sub BuyTargetTRPSpotlengthData(ByVal tmpChan As Trinity.cChannel, ByVal tmpB As Trinity.cBookingType, ByVal tmpGroupName As String, ByVal bundle As Boolean, ByVal row As Integer, ByVal tmpChannelName As String, Optional ByVal savedRow As Integer = 0, Optional rowheader As Integer = 0, Optional ByVal printExportAsCampaign As Boolean = False)

        Dim tmpBook As Trinity.cBookingType = tmpB
        Dim tmpBundle = bundle

        Dim stationChannels = ""

        Dim totalPlannedGrossBudget As Integer = 0
        Dim totalPlannedNetBudget As Integer = 0

        Dim listOfSpots As New List(Of Trinity.cBookedSpot)

        Dim target As String = ""

        With _sheet
            Dim valueTest = .Cells(savedRow, 2).Value
            'If tmpBundle And groupName = tmpGroupName Or tmpGroupName <> "TV4" Then
            '    row = savedRow
            'ElseIf tmpBundle And groupName = tmpGroupName Or tmpGroupName = "TV4" Then
            '    If valueTest IsNot Nothing Then
            '        If valueTest.ToString.Contains(tmpChan.AdEdgeNames) Then
            '            row = savedRow
            '        Else
            '            row = tmpRow
            '            tmpRow = tmpRow + 1
            '        End If
            '    Else
            '        row = savedRow
            '    End If
            If tmpBundle Then
                row = savedRow
            ElseIf tmpB.IsCompensation Or tmpB.IsSpecific And (channelName = tmpChannelName) Then
                row = savedRow
            Else
                If savedRow = 0 Then
                    row = tmpRow
                Else
                    row = savedRow
                End If
            End If
            channelName = tmpChannelName
            groupName = tmpGroupName

            .Range("B" & row & ":B" & row).Interior.Color = RGB(0, 137, 178)
            .Range("B" & row & ":B" & row).Font.Color = RGB(255, 255, 255)
            Dim test = .Cells(row, 2).Value
            'If TrinitySettings.Developer Then
            '    .Cells(row, 13).Value += tmpChan.ChannelName + " " + tmpBook.Name + " "
            'End If
            If Not printExportAsCampaign Then
                If groupName = "TV4" Then
                    If tmpBundle Then
                        If tmpChan.ChannelName = "TV4" Then
                            .Cells(row, 2).Value = "TV4"
                        Else
                            .Cells(row, 2).Value = "TV4 Nish"
                        End If
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "MTG" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "NENT"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "SBS" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Discovery"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "FOX" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Fox Int"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "Cartoon" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Cartoon"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "Disney" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Disney C/XD"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "TNT" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "TNT"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "CMORE" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "C More"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                End If
            Else
                If groupName = "MTG" Then
                    .Cells(row, 2).Value = "MTG TV"
                ElseIf groupName = "SBS" Then
                    .Cells(row, 2).Value = "SBS Combo"
                ElseIf groupName = "FOX" Then
                    .Cells(row, 2).Value = "FOX Combo"
                ElseIf groupName = "Cartoon" Then
                    .Cells(row, 2).Value = "Cartoon Combo"
                ElseIf groupName = "Disney" Then
                    .Cells(row, 2).Value = "Disney Combo"
                ElseIf groupName = "TNT" Then
                    .Cells(row, 2).Value = "TNT Combo"
                ElseIf groupName = "CMORE" Then
                    .Cells(row, 2).Value = "C More Combo"
                End If
            End If

            Dim column = 3
            If tmpChan.ChannelName = "TV4" Then
                If row = 0 Then
                    row = row
                    row = row + 1
                End If
            End If
            If tmpChan.ChannelName = "TV4" Then
                row = row
            End If
            For x As Integer = 0 To _amountOfFilmLength.Count - 1
                Dim tmpFilm = ""
                Dim tmpTotalShareForFilm As Decimal = 0
                Dim totalTRPForBT As Double = 0
                Dim trpForFilm As Double = 0
                Dim trpForShare As Double = 0
                Dim tmpShare As Double = 0
                For Each tmpF As Trinity.cFilm In listOfFilms
                    If _amountOfFilmLength(x).ToString = tmpF.FilmLength Then
                        For Each tmpWeek As Trinity.cWeek In tmpB.Weeks
                            For Each w As cWeek In listOfWeeks
                                If tmpWeek.Name = w.Name Then
                                    trpForShare = tmpWeek.TRPBuyingTarget
                                    If (tmpF.Filmcode <> "") Then
                                        tmpShare = tmpWeek.Films(tmpF.Name).Share()
                                        tmpFilm = tmpWeek.Films(tmpF.Name).FilmLength
                                    Else
                                        tmpShare = tmpWeek.Films(tmpF.Name).Share()
                                        tmpFilm = tmpWeek.Films(tmpF.Name).FilmLength
                                    End If
                                    If trpForShare <> 0 Then
                                        trpForFilm += trpForShare * (tmpShare / 100)
                                    End If
                                End If
                            Next
                        Next
                    End If
                Next
                totalTRPForBT = trpForFilm
                If .Cells(rowheader, column).Value Is Nothing Then
                    .Cells(rowheader, column).Value = tmpFilm
                End If
                .Cells(row, column).Value += Math.Round(totalTRPForBT, 2)
                column = column + 1
            Next
        End With
    End Sub


    Sub printBuyingtargetTRPSpotlength()
        Dim filmColumnArray As String() = {":B", ":C", ":D", ":E", ":F", ":G", ":H", ":I", ":J", ":K", ":L", ":M", ":N", ":O", ":P", ":Q", ":R", ":S", ":T", ":U", ":V", ":W", ":X", ":Y", ":Z", ":AA", ":AB", ":AC", ":AD"}
        Dim columnValue As String = filmColumnArray.GetValue(listOfFilms.Count)

        Dim currentRow As Integer = lastRowComboTable4
        Dim rowHeader = 0
        With _sheet
            .Range("B" & currentRow + 1 & columnValue & currentRow + 1).Interior.Color = RGB(0, 137, 178)
            .Range("B" & currentRow + 1 & columnValue & currentRow + 1).Font.Color = RGB(255, 255, 255)

            .Range("B" & currentRow & columnValue & currentRow).Interior.Color = RGB(0, 37, 110)
            .Range("B" & currentRow & columnValue & currentRow).Font.Color = RGB(255, 255, 255)
            .Cells(currentRow, 2).Value = "Buying target: TRP pr. station, pr. spotlength"
            currentRow = currentRow + 1
            .Cells(currentRow, 2).Value = "Network"
            rowHeader = currentRow

            For Each f As Trinity.cFilm In listOfFilms
                '.Cells(currentRow, columnValue).Value = f.FilmLength
            Next
        End With
        currentRow = currentRow + 1
        tmpRow = currentRow

        Dim rowOnlyTV4 = 0
        Dim rowTV4 = 0
        Dim rowMTG = 0
        Dim rowMTGSpecial = 0
        Dim rowSBS = 0
        Dim rowFOX = 0
        Dim rowCMORE = 0
        Dim rowCartoon = 0
        Dim rowDisney = 0
        Dim rowTNT = 0

        Dim tv4String = ""

        Dim rowCombination As Integer = 0
        Dim tmpChannelNameInputForCheckBundling As String = ""

        If Campaign.Combinations.Count > 0 And _printExportAsCampaign Then
            For Each tmpCombo As Trinity.cCombination In Campaign.Combinations
                For Each tmpCC As Trinity.cCombinationChannel In tmpCombo.Relations
                    Dim tmpChan As Trinity.cChannel = Nothing
                    Dim tmpBook As Trinity.cBookingType = Nothing
                    For Each c As Trinity.cChannel In Campaign.Channels
                        For Each tmpB As cBookingType In
                            From tmpB1 As cBookingType In c.BookingTypes Where tmpB1 Is tmpCC.Bookingtype
                            tmpChan = c
                            tmpBook = tmpB
                            If CheckChannel(c.ChannelName) = "TV4" Then
                                If rowCombination = 0 Then
                                    rowCombination = tmpRow
                                    rowTV4 = rowCombination
                                End If
                            ElseIf CheckChannel(c.ChannelName) = "MTG" Then
                                rowCombination = tmpRow
                                rowMTG = rowCombination
                            ElseIf CheckChannel(c.ChannelName) = "SBS" Then
                                rowCombination = tmpRow
                                rowSBS = rowCombination
                            ElseIf CheckChannel(c.ChannelName) = "FOX" Then
                                rowCombination = tmpRow
                                rowSBS = rowCombination
                            ElseIf CheckChannel(c.ChannelName) = "C MORE" Then
                                rowCombination = tmpRow
                                rowCMORE = rowCombination
                            ElseIf CheckChannel(c.ChannelName) = "Cartoon" Then
                                rowCombination = tmpRow
                                rowCartoon = rowCombination
                            ElseIf CheckChannel(c.ChannelName) = "TNT" Then
                                rowCombination = tmpRow
                                rowTNT = rowCombination
                            ElseIf CheckChannel(c.ChannelName) = "Disney" Then
                                rowCombination = tmpRow
                                rowDisney = rowCombination
                            End If
                            tmpChannelNameInputForCheckBundling = CheckChannel(c.ChannelName)
                            BuyTargetTRPSpotlengthData(tmpChan, tmpB, tmpChannelNameInputForCheckBundling, False, tmpRow,
                                         c.ChannelName, rowCombination, rowHeader, True)
                            Exit For
                        Next
                    Next
                Next
                tmpRow = tmpRow + 1
            Next
            ExportedAsCampaignCombination = True
        End If
        With _sheet
            If (_printExportAsCampaign = False) Or (Campaign.Combinations.Count > 0 And _printExportAsCampaign) Or (Campaign.Combinations.Count < 1 And _printExportAsCampaign) Then
                For Each tmpChan As Trinity.cChannel In Campaign.Channels
                    For Each tmpBook As Trinity.cBookingType In tmpChan.BookingTypes
                        If tmpBook.BookIt Then
                            Dim jumpBookingType As Boolean = False
                            If (_printExportAsCampaign) Then
                                For i As Integer = 1 To Campaign.Combinations.Count
                                    If Campaign.Combinations.Item(i).Relations.Cast(Of cCombinationChannel)().Any(Function(tmpC) tmpC.Bookingtype Is tmpBook) Then
                                        jumpBookingType = True
                                    End If
                                Next
                            End If
                            If (Not jumpBookingType) Then
                                If checkNameTV4(tmpChan.ChannelName) Then
                                    If _BundleTV4 And rowTV4 <> 0 And rowOnlyTV4 <> 0 Then
                                        If rowTV4 = 0 Then
                                            rowTV4 = tmpRow
                                            tv4String = tmpChan.ChannelName
                                            BuyTargetTRPSpotlengthData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)
                                        ElseIf _BundleTV4 Then
                                            BuyTargetTRPSpotlengthData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)
                                        Else
                                            rowTV4 = tmpRow
                                            tv4String = tmpChan.ChannelName
                                            BuyTargetTRPSpotlengthData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)
                                        End If
                                    ElseIf (rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Or (rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific Or tmpBook.IsRBS) And tmpChan.ChannelName = channelName) Then
                                        rowTV4 = rowTV4

                                        tv4String = tmpChan.ChannelName
                                        BuyTargetTRPSpotlengthData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)

                                    ElseIf rowOnlyTV4 <> 0 And tmpChan.ChannelName = "TV4" Or tmpChan.ChannelName = "TV4" And _BundleTV4 Then
                                        If rowOnlyTV4 = 0 Then
                                            rowOnlyTV4 = tmpRow
                                            tmpRow = tmpRow + 1
                                        End If

                                        tv4String = tmpChan.ChannelName
                                        BuyTargetTRPSpotlengthData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowOnlyTV4, rowHeader)
                                    Else
                                        rowTV4 = tmpRow
                                        tmpRow = tmpRow + 1

                                        tv4String = tmpChan.ChannelName
                                        BuyTargetTRPSpotlengthData(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)
                                    End If
                                    currentRow = tmpRow
                                End If
                                If checkNameMTG(tmpChan.ChannelName) Then
                                    If _bundleMTGSpecial Then
                                        If tmpChan.ChannelName.Contains("TV3") Or tmpChan.ChannelName.Contains("TV6") Then
                                            If rowMTGSpecial <> 0 Then
                                            Else
                                                rowMTGSpecial = tmpRow
                                                tmpRow = tmpRow + 1
                                            End If

                                            BuyTargetTRPSpotlengthData(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTGSpecial, rowHeader)
                                        Else
                                            If _BundleMTG And rowMTG <> 0 Then
                                                If rowMTG = 0 Then
                                                    rowMTG = tmpRow
                                                End If
                                            ElseIf (rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Then
                                                rowMTG = rowMTG
                                            Else
                                                rowMTG = tmpRow
                                                tmpRow = tmpRow + 1
                                            End If
                                            BuyTargetTRPSpotlengthData(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG, rowHeader)
                                        End If
                                    Else
                                        If _BundleMTG And rowMTG <> 0 Then
                                            If rowMTG = 0 Then
                                                rowMTG = tmpRow
                                            End If
                                        ElseIf (rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowMTG <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Then
                                            rowMTG = rowMTG
                                        Else
                                            rowMTG = tmpRow
                                            tmpRow = tmpRow + 1
                                        End If
                                        BuyTargetTRPSpotlengthData(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG, rowHeader)
                                    End If
                                    currentRow = tmpRow
                                End If
                                If checkNameSBS(tmpChan.ChannelName) Then
                                    If _BundleSBS And rowSBS <> 0 Then
                                        If rowSBS = 0 Then
                                            rowSBS = tmpRow
                                        End If
                                    ElseIf (rowSBS <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowSBS <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Then
                                        rowSBS = rowSBS
                                    Else
                                        rowSBS = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    BuyTargetTRPSpotlengthData(tmpChan, tmpBook, "SBS", _BundleSBS, tmpRow, tmpChan.ChannelName, rowSBS, rowHeader)
                                    currentRow = tmpRow
                                End If
                                If checkNameFOX(tmpChan.ChannelName) Then
                                    If _BundleFOX And rowFOX <> 0 Then
                                        If rowFOX = 0 Then
                                            rowFOX = tmpRow
                                        End If
                                    ElseIf (rowFOX <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowFOX <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) Then
                                        rowFOX = rowFOX
                                    Else
                                        rowFOX = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    BuyTargetTRPSpotlengthData(tmpChan, tmpBook, "FOX", _BundleFOX, tmpRow, tmpChan.ChannelName, rowFOX, rowHeader)
                                    currentRow = tmpRow
                                End If
                                If checkNameCMORE(tmpChan.ChannelName) Then
                                    If _BundleCMORE And rowCMORE <> 0 Then
                                        If rowCMORE = 0 Then
                                            rowCMORE = tmpRow
                                        End If
                                    ElseIf (rowCMORE <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowCMORE <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) Then
                                        rowCMORE = rowCMORE
                                    Else
                                        rowCMORE = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    BuyTargetTRPSpotlengthData(tmpChan, tmpBook, "CMORE", _BundleCMORE, tmpRow, tmpChan.ChannelName, rowCMORE, rowHeader)
                                    currentRow = tmpRow
                                End If
                                If checkNameCartoon(tmpChan.ChannelName) Then
                                    If _BundleCartoon And rowCartoon <> 0 Then
                                        If rowCartoon = 0 Then
                                            rowCartoon = tmpRow
                                        End If
                                    ElseIf rowCartoon <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And (tmpChan.ChannelName = channelName) Then
                                        rowCartoon = rowCartoon
                                    Else
                                        rowCartoon = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    BuyTargetTRPSpotlengthData(tmpChan, tmpBook, "Cartoon", _BundleCartoon, tmpRow, tmpChan.ChannelName, rowCartoon, rowHeader)
                                    currentRow = tmpRow
                                End If
                                If checkNameDisney(tmpChan.ChannelName) Then
                                    If _BundleDisney And rowDisney <> 0 Then
                                        If rowDisney = 0 Then
                                            rowDisney = tmpRow
                                        End If
                                    ElseIf rowDisney <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And (tmpChan.ChannelName = channelName) Then
                                        rowDisney = rowDisney
                                    Else
                                        rowDisney = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    BuyTargetTRPSpotlengthData(tmpChan, tmpBook, "Disney", _BundleDisney, tmpRow, tmpChan.ChannelName, rowDisney, rowHeader)
                                    currentRow = tmpRow
                                End If
                                If checkNameTNT(tmpChan.ChannelName) Then
                                    If _BundleTNT And rowTNT <> 0 Then
                                        If rowTNT = 0 Then
                                            rowTNT = tmpRow
                                        End If
                                    ElseIf rowTNT <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And (tmpChan.ChannelName = channelName) Then
                                        rowTNT = rowTNT
                                    Else
                                        rowTNT = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    BuyTargetTRPSpotlengthData(tmpChan, tmpBook, "TNT", _BundleTNT, tmpRow, tmpChan.ChannelName, rowTNT, rowHeader)
                                    currentRow = tmpRow
                                End If
                            End If
                        End If
                    Next
                Next
            End If
        End With
        '_sheet.Range("B" & lastRowComboTable1 - 1 & ":M" & currentRow).Numberformat = "0.0"
        lastRowComboTable5 = currentRow + 1
    End Sub
    Sub printFilmcodesSummery()

        Dim weekCol As String() = {":B", ":C", ":D", ":E", ":F", ":G", ":H", ":I", ":J", ":K", ":L", ":M", ":N", ":O", ":P", ":Q", ":R", ":S", ":T", ":U", ":V", ":W", ":X", ":Y", ":Z", ":AA", ":AB", ":AC", ":AD"}
        Dim columnValue As String = weekCol.GetValue(3 + listOfWeeks.Count)
        Dim currentRow As Integer = lastRowComboTable5
        Dim headerRow = 0
        With _sheet
            .Range("B" & currentRow + 1 & columnValue & currentRow + 1).Interior.Color = RGB(0, 137, 178)
            .Range("B" & currentRow + 1 & columnValue & currentRow + 1).Font.Color = RGB(255, 255, 255)

            .Range("B" & currentRow & columnValue & currentRow).Interior.Color = RGB(0, 37, 110)
            .Range("B" & currentRow & columnValue & currentRow).Font.Color = RGB(255, 255, 255)
            .Cells(currentRow, 2).Value = "Filmcodes"
            currentRow = currentRow + 1
            .Cells(currentRow, 2).Value = "Group"
            .Cells(currentRow, 3).Value = "Title"
            .Cells(currentRow, 4).Value = "Filmcode"
            .Cells(currentRow, 5).Value = "Duration"
            headerRow = currentRow
        End With

        currentRow = currentRow + 1
        Dim amountOfFilms = 1

        Dim filmLengths As New List(Of Integer)

        Dim x As Integer = 0
        For Each tmpfilm As cFilm In listOfFilms
            If Not filmLengths.Contains(listOfFilms.Item(x).FilmLength) Then
                filmLengths.Add(listOfFilms.Item(x).FilmLength)
            End If
            x = x + 1
        Next

        For Each tmpLength As Byte In filmLengths
            For i As Integer = 1 To listOfFilms.Count
                With _sheet
                    For Each tmpChan As Trinity.cChannel In Campaign.Channels
                        For Each tmpBt As Trinity.cBookingType In tmpChan.BookingTypes
                            If tmpBt.BookIt Then
                                .Cells(currentRow, 2).Value = amountOfFilms
                                Dim totalTRPForWeek As Decimal = 0
                                Dim startingColumn = 6
                                For Each tmpWeek As Trinity.cWeek In tmpBt.Weeks
                                    For Each w As cWeek In listOfWeeks
                                        If w.Name = tmpWeek.Name Then
                                            If tmpWeek.Films(i).FilmLength = tmpLength Then
                                                totalTRPForWeek = tmpWeek.TRP * (tmpWeek.Films(i).Share / 100)
                                                If .Cells(headerRow, startingColumn).Value = "" Then
                                                    Dim yearForWeek As Date = Trinity.Helper.FormatDateForBooking(tmpWeek.StartDate)
                                                    .Cells(headerRow, startingColumn).Value = yearForWeek.Year.ToString() & "w" & tmpWeek.Name
                                                End If
                                                If .Cells(currentRow, 3).Value Is Nothing Then
                                                    'If tmpWeek.Films(i).Description = "" Then
                                                    '    .Cells(currentRow, 3).Value = tmpWeek.Films(i).Name
                                                    'Else
                                                    '    .Cells(currentRow, 3).Value = tmpWeek.Films(i).Description
                                                    'End If
                                                    .Cells(currentRow, 3).Value = tmpLength & " Sec"
                                                End If
                                                If .Cells(currentRow, 4).Value Is Nothing Then
                                                    .Cells(currentRow, 4).Value += tmpWeek.Films(i).Filmcode + ";"
                                                ElseIf Not .Cells(currentRow, 4).Text.Contains(tmpWeek.Films(i).Filmcode) Then
                                                    .Cells(currentRow, 4).Value += tmpWeek.Films(i).Filmcode + ";"
                                                End If
                                                If .Cells(currentRow, 5).Value Is Nothing Then
                                                    .Cells(currentRow, 5).Value = tmpWeek.Films(i).FilmLength
                                                End If
                                                .Cells(currentRow, startingColumn).Value += Math.Round(totalTRPForWeek, 2)
                                                startingColumn += 1
                                                totalTRPForWeek = 0
                                            End If
                                        End If
                                    Next
                                Next
                                .Range("B" & currentRow & ":B" & currentRow).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & currentRow & ":B" & currentRow).Font.Color = RGB(255, 255, 255)
                            End If
                        Next
                    Next
                End With
            Next
            amountOfFilms += 1
            currentRow = currentRow + 1
        Next
        '_sheet.Range("E" & lastRowComboTable1 - 1 & ":M" & currentRow).Numberformat = "0.0"
    End Sub

    Sub resetEnivorment()
        _excel.ScreenUpdating = True
        _excel.Visible = True
    End Sub
    Sub prepareEnivorment()

        If _excel IsNot Nothing Then
            _excel.ScreenUpdating = False
            _excel.DisplayAlerts = False
        End If
    End Sub
End Class
