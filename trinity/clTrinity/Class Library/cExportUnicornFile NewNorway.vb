
Imports clTrinity
Imports clTrinity.Trinity
Imports Microsoft.Office.Interop.Excel

Public Class CExportUnicornFileNewNorway
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



    Dim BundleDNN As Boolean = False
    Dim BundleTV2 As Boolean = False
    Dim BundleFOX As Boolean = False
    Dim BundleMTG As Boolean = False
    Dim BundleMatkanalen As Boolean = False
    Dim BundleTNT As Boolean = False

    Dim tmpPrintExportAsCampaign As Boolean = False
    Dim ExportedAsCampaignCombination As Boolean = False

    Dim tmpBundleTV4 As Boolean = True
    Dim tmpBundleMTG As Boolean = False
    Dim tmpBundleDiscovery As Boolean = False
    Dim tmpBundleFOX As Boolean = False
    Dim tmpBundleCMORE As Boolean = False
    Dim tmpBundleTNT As Boolean = False
    Dim tmpBundleDisney As Boolean = False
    Dim tmpBundleCartoon As Boolean = False
    Dim _campaignType As String = ""
    Dim _campaignType2 As  String = ""

    Dim _campaign As Trinity.cKampanj

    Dim tmpRow As Integer = 39


    Dim groupName As String = ""
    Dim channelName As String = ""

    Dim targets As String = ""


    Dim tmpOwnFee As Decimal = 0
    Dim ownCommission As Boolean = False
    Dim ownCommissionAmount As Single = 0

    Dim tmpUserCompany As String = ""
    Dim userCompany As String = ""

    '   /JOOS
    '   2019-02-12 Chenges:
    '   Changed name on MTG to NENT in every PrinTtable and removed Comedy Central from Export
    Public Sub printUnicornFile(Optional ByVal bundleTV2 As Boolean = False, Optional ByVal bundleMTG As Boolean = False, Optional ByVal bundleDNN As Boolean = False, Optional ByVal useOwnCommission As Boolean = False, Optional ByVal useOwnCommissionAmount As Decimal = 0, Optional ByVal tmpPrintExportAsCampaign As Boolean = False, Optional ByVal ct As String = "", Optional ByVal ct2 As String = "")

        If useOwnCommission Then
            ownCommission = True
            ownCommissionAmount = useOwnCommissionAmount / 100
        End If
        prepareEnivorment()
        _campaignType = ct
        _campaignType2 = ct2
        checkAmountOfBookedChannels()
        checkBundling(bundleTV2, bundleMTG, bundleDNN, tmpPrintExportAsCampaign)
        _wb = _excel.AddWorkbook
        _sheet = _wb.Sheets(1)
        getFilms()
        GetAmountOfFilmLengths()
        With _sheet

            printHeader()
            PrintTable1()
            PrintTable2()
            PrintTable3()
            PrintTable4()
            printTable5()
            printTable6()
            For i As Integer = 1 To 20
                .Columns(i).AutoFit()
            Next
        End With
        _sheet.Range("B" & 2 & ":M" & 200).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
        _sheet.Range("B" & 2 & ":M" & 200).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
        resetEnivorment()
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
                    For Each tmpWeek As Trinity.cWeek In tmpBT.Weeks
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
    Sub checkBundling(Optional ByVal tmpBundleTV2 As Boolean = False, Optional ByVal tmpBundleMTG As Boolean = False, Optional ByVal tmpBundleDNN As Boolean = False, Optional ByVal tmpBundleFOX As Boolean = False, Optional ByVal tmpPrintAsCampaign As Boolean = False)
        BundleTV2 = tmpBundleTV2
        BundleMTG = tmpBundleMTG
        BundleDNN = tmpBundleDNN
        BundleFOX = tmpBundleFOX
        _printExportAsCampaign = tmpPrintAsCampaign
    End Sub

    Public Function checkNameTV2(ByVal tmpChannelName As String)
        If tmpChannelName.Contains("TV2") Or tmpChannelName = "Bliss" Or tmpChannelName = "Nyhetskanalen" Then
            Return True
        End If
        Return False
    End Function
    Public Function CheckNameDNN(ByVal tmpChannelName As String)
        If tmpChannelName.Contains("TVN") Or tmpChannelName.Contains("FEM") Or tmpChannelName.Contains("Eurosport") Or tmpChannelName.Contains("MAX") Or tmpChannelName.Contains("Discovery") Or tmpChannelName.Contains("TLC") Or tmpChannelName.Contains("VOX") Then
            Return True
        End If
        Return False
    End Function
    '   //JOOS
    '   Added Nat Geo, Fox, BBC to MTG/NENT export
    '   2019-02-12: Removed Comedy Central from export
    Public Function checkNameMTG(ByVal tmpChannelName As String)
        If tmpChannelName.Contains("TV3") Or tmpChannelName.Contains("TV6") Or tmpChannelName.Contains("MTV") Or tmpChannelName.Contains("Viasat") Or tmpChannelName.Contains("National Geographic") Or tmpChannelName.Contains("Fox") Or tmpChannelName.Contains("BBC Brit") Then
            Return True
        End If
        Return False
    End Function
    Public Function checkNameMatkanalen(ByVal tmpChannelName As String)
        If tmpChannelName.Contains("Matkanalen") Then
            Return True
        End If
        Return False
    End Function
    'Remove this 
    'Public Function checkNameFOXold(ByVal tmpChannelName As String)
    '    If tmpChannelName.Contains("National Geographic") Or tmpChannelName.Contains("Fox") Or tmpChannelName.Contains("BBC Brit") Then
    '        Return True
    '    End If
    '    Return False
    'End Function
    Public Function checkNameTNT(ByVal tmpChannelName As String)
        If tmpChannelName.Contains("TNT") Then
            Return True
        End If
        Return False
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
            .Range("B" & 25 & ":C" & 25).Font.Bold = True
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
            .Cells(3, 3).Value = "NO"

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

            'If checkDatabasePath() Then
            '    userCompany = tmpUserCompany
            'ElseIf checkPathSettings() Then
            '    userCompany = tmpUserCompany
            'ElseIf checkEmailSettings() Then
            '    userCompany = tmpUserCompany
            'Else
            '    userCompany = ""
            'End If

            .Cells(4, 3).Value = userCompany
            .Cells(5, 3).Value = Campaign.Planner
            .Cells(6, 3).Value = _campaignType
            .Cells(7, 3).Value = _campaignType2
            .Cells(8, 3).Value = Campaign.Client
            .Cells(9, 3).Value = Campaign.Client
            .Cells(10, 3).Value = Campaign.PlannedTotCTC.ToString()
            .Cells(11, 3).Value = Campaign.ClientID.ToString()
            .Cells(12, 3).Value = Campaign.ContractID.ToString()
            .Cells(13, 3).Value = Campaign.MarathonPlanNr 'Budget namn?
            .Cells(14, 3).Value = Campaign.MarathonPlanNr 'Budget nr?
            .Cells(15, 3).Value = Campaign.Product

            .Cells(17, 3).Value = Trinity.Helper.FormatDateForBooking(Campaign.StartDate)
            .Cells(18, 3).Value = Trinity.Helper.FormatDateForBooking(Campaign.EndDate)


            Dim tmpMainTarget As String = translateTargetName(Campaign.MainTarget.TargetName)
            'Dim tmpSecondTarget As String = translateTargetName(Campaign.SecondaryTarget.TargetName)
            Dim tmpSecondTarget As String = translateTargetName(Campaign.SecondaryTarget.TargetName)
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

    Sub HelperTable1(ByVal tmpChan As Trinity.cChannel, ByVal tmpB As Trinity.cBookingType, ByVal tmpGroupName As String, ByVal bundle As Boolean, ByVal row As Integer, ByVal tmpChannelName As String, Optional ByVal savedRow As Integer = 0, Optional ByVal printExportAsCampaign As Boolean = False)


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
            If tmpBundle And groupName = tmpGroupName Then
                row = savedRow
            ElseIf tmpBundle Then
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
            Dim chValue = .Cells(row, 3).Value
            'If TrinitySettings.Developer Then
            '    .Cells(row, 13).Value += tmpChan.ChannelName + " " + tmpBook.Name + " "
            'End If
            If Not printExportAsCampaign Then
                If groupName = "DNN" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Discovery Networks Norway"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                ElseIf groupName = "TV2" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "TV2 Group"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                        End If
                    End If
                    'ElseIf groupName = "FOXold" Then
                    '    If tmpBundle Then
                    '        .Cells(row, 2).Value = "FOXold"
                    '    Else
                    '        If .Cells(row, 2).Value Is Nothing Then
                    '            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                    '        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                    '            .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                    '        End If
                    '    End If
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
                Else
                    If .Cells(row, 2).Value Is Nothing Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                    ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                    End If
                End If
            Else
                If groupName = "MTG" Then
                    .Cells(row, 2).Value = "NENT"
                ElseIf groupName = "DNN" Then
                    .Cells(row, 2).Value = "Discovery Networks Norway"
                    'ElseIf groupName = "FOXold" Then
                    '    .Cells(row, 2).Value = "FOXold"
                ElseIf groupName = "TV2" Then
                    .Cells(row, 2).Value = "TV2 Group"
                ElseIf groupName = "¨TNT" Then
                    .Cells(row, 2).Value = "TNT"
                Else
                    If .Cells(row, 2).Value Is Nothing Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                    ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                    End If
                End If
            End If
            'Eurosport NO changed to Eurosport 1 Norge
            If Not .Cells(row, 3) Is Nothing Then
                If tmpBundle Or printExportAsCampaign Then
                    If groupName = "TV2" Then
                        .Cells(row, 3).Value = "TV 2 no; TV 2 Zebra; TV 2 Sport 1; TV 2 Sport 2; TV2 Nyhetskanalen; TV 2 Humor; TV 2 Livsstil;"
                    ElseIf groupName = "DNN" Then
                        .Cells(row, 3).Value = "TVN; FEM; Eurosport 1 Norge; MAX; Discovery no; TLC Norge; VOX; Eurosport Norge"
                        'ElseIf groupName = "FOXold" Then
                        '    .Cells(row, 3).Value = "National GeographicREMOVE; Fox NorwayREMOVE; BBC BritREMOVE; Fox CrimeREMOVE;"
                    ElseIf groupName = "MTG" Then
                        .Cells(row, 3).Value = "TV3; Viasat 4; TV6; National Geographic; Fox Norway; BBC Brit; Fox Crime;"
                    ElseIf groupName = "TNT" Then
                        .Cells(row, 3).Value = "TNT"
                    End If
                ElseIf chValue Is Nothing Then
                    If tmpChan.AdEdgeNames <> "" Then
                        stationChannels += tmpChan.AdEdgeNames + "; "
                    Else
                        stationChannels += tmpChan.ChannelName + "; "
                    End If
                Else
                    If Not chValue.ToString.Contains(tmpChan.AdEdgeNames) Then
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
            If tmpBook.ConfirmedGrossBudget = 0 Then
                tmptotalPlannedGrossBudget = tmpBook.PlannedGrossBudget
            Else
                tmptotalPlannedGrossBudget = tmpBook.ConfirmedGrossBudget
            End If
            Dim tmptotalPlannedNetBudget = 0
            tmptotalPlannedNetBudget = tmpBook.PlannedNetBudget
            totalPlannedGrossBudget += tmptotalPlannedGrossBudget
            totalPlannedNetBudget += tmptotalPlannedNetBudget


            For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                totalTRPForBT += tmpWeek.TRP
            Next
            .Cells(row, 11).Value += Math.Round(totalTRPForBT, 2)


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

            'Cost

            For Each TmpCost In Campaign.Costs
                If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                    If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnRatecard Then
                        tmptotalPlannedGrossBudget = TmpCost.Amount * tmpB.PlannedGrossBudget
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
                                Dim Discount As Single = TmpWeek.GrossBudget - TmpWeek.NetBudget
                                SumUnit += (Discount * TmpCost.Amount)
                            Next
                        Else
                            For Each tmpB In DirectCast(TmpCost.CountCostOn, Trinity.cChannel).BookingTypes
                                For Each TmpWeek As Trinity.cWeek In tmpB.Weeks
                                    Dim Discount As Single = TmpWeek.GrossBudget - TmpWeek.NetBudget
                                    SumUnit += (Discount * TmpCost.Amount)
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
        If checkNameTV2(tmpChName) Then
            result = "TV2"
        ElseIf checkNameMTG(tmpChName) Then
            result = "MTG"
        ElseIf CheckNameDNN(tmpChName) Then
            result = "DNN"
            'ElseIf checkNameFOXold(tmpChName) Then
            '   result = "FOXold"
        ElseIf checkNameMatkanalen(tmpChName) Then
            result = "Matkanalen"
        ElseIf checkNameTNT(tmpChName) Then
            result = "TNT"
        End If
        Return result
    End Function
    Sub PrintTable1()
        'Print the first table for channels
        With _sheet
            .Range("B" & 37 & ":L" & 37).Interior.Color = RGB(0, 37, 110)
            .Range("B" & 37 & ":L" & 37).Font.Color = RGB(255, 255, 255)
            .Range("B" & 38 & ":L" & 38).Interior.Color = RGB(0, 137, 178)
            .Range("B" & 38 & ":L" & 38).Font.Color = RGB(255, 255, 255)


            Dim rowCombination As Integer = 0
            Dim rowTV2 = 0
            Dim rowDNN = 0
            Dim rowFOX = 0
            Dim rowMTG = 0
            Dim rowMatkanalen = 0
            Dim rowTNT = 0

            Dim TV2String As String = ""
            Dim DNNString As String = ""
            Dim NatGeoString As String = ""
            Dim MTGString As String = ""
            Dim Matkanalenstring As String = ""
            Dim TNTString As String = ""

            Dim firstRow As Integer = 39

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
                                If checkNameTV2(c.ChannelName) Then
                                    rowCombination = tmpRow
                                    rowTV2 = rowCombination
                                ElseIf CheckNameDNN(c.ChannelName) Then
                                    rowCombination = tmpRow
                                    rowDNN = rowCombination
                                    'ElseIf checkNameFOXold(c.ChannelName) Then
                                    '   rowCombination = tmpRow
                                    '  rowFOX = rowCombination
                                ElseIf checkNameMTG(c.ChannelName) Then
                                    rowCombination = tmpRow
                                    rowMTG = rowCombination
                                ElseIf checkNameMatkanalen(c.ChannelName) Then
                                    rowCombination = tmpRow
                                    rowMatkanalen = rowCombination
                                End If
                                tmpChannelNameInputForCheckBundling = CheckChannel(c.ChannelName)
                                HelperTable1(tmpChan, tmpB, tmpChannelNameInputForCheckBundling, False, tmpRow,
                                             c.ChannelName, rowCombination, True)
                                Exit For
                            Next
                        Next
                    Next
                    tmpRow = tmpRow + 1
                Next
                ExportedAsCampaignCombination = True
            End If

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
                            If Not jumpBookingType Then
                                'MTG
                                If checkNameTV2(tmpChan.ChannelName) Then
                                    If BundleTV2 And rowTV2 <> 0 Then
                                        If rowTV2 = 0 Then
                                            rowTV2 = tmpRow
                                        End If
                                    Else
                                        rowTV2 = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    TV2String = tmpChan.ChannelName
                                    HelperTable1(tmpChan, tmpBook, "TV2", BundleTV2, tmpRow, tmpChan.ChannelName, rowTV2)
                                End If
                                If CheckNameDNN(tmpChan.ChannelName) Then
                                    If BundleDNN And rowDNN <> 0 Then
                                        If rowDNN = 0 Then
                                            rowDNN = tmpRow
                                        End If
                                    Else
                                        rowDNN = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    DNNString = tmpChan.ChannelName
                                    HelperTable1(tmpChan, tmpBook, "DNN", BundleDNN, tmpRow, tmpChan.ChannelName, rowDNN)
                                End If
                                'If checkNameFOXold(tmpChan.ChannelName) Then
                                '    If BundleFOX And rowFOX <> 0 Then
                                '        If rowFOX = 0 Then
                                '            rowFOX = tmpRow
                                '        End If
                                '    Else
                                '        rowFOX = tmpRow
                                '        tmpRow = tmpRow + 1
                                '    End If
                                '    NatGeoString = tmpChan.ChannelName
                                '    HelperTable1(tmpChan, tmpBook, "FOXold", BundleFOX, tmpRow, tmpChan.ChannelName, rowFOX)
                                'End If

                                If checkNameMTG(tmpChan.ChannelName) Then
                                    If BundleMTG And rowMTG <> 0 Then
                                        If rowMTG = 0 Then
                                            rowMTG = tmpRow
                                        End If
                                    Else
                                        rowMTG = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    MTGString = tmpChan.ChannelName
                                    HelperTable1(tmpChan, tmpBook, "MTG", BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG)
                                End If
                                If checkNameMatkanalen(tmpChan.ChannelName) Then
                                    If BundleMatkanalen And rowMatkanalen <> 0 Then
                                        If rowMatkanalen = 0 Then
                                            rowMatkanalen = tmpRow
                                        End If
                                    Else
                                        rowMatkanalen = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    Matkanalenstring = tmpChan.ChannelName
                                    HelperTable1(tmpChan, tmpBook, "Matkanalen", BundleMatkanalen, tmpRow, tmpChan.ChannelName, rowMatkanalen)
                                End If
                                If checkNameTNT(tmpChan.ChannelName) Then
                                    If BundleTNT And rowTNT <> 0 Then
                                        If rowTNT = 0 Then
                                            rowTNT = tmpRow
                                        End If
                                    Else
                                        rowTNT = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    TNTString = tmpChan.ChannelName
                                    HelperTable1(tmpChan, tmpBook, "TNT", BundleTNT, tmpRow, tmpChan.ChannelName, rowTNT)
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
    Sub HelperTable2(ByVal tmpChan As Trinity.cChannel, ByVal tmpB As Trinity.cBookingType, ByVal tmpGroupName As String, ByVal bundle As Boolean, ByVal row As Integer, ByVal tmpChannelName As String, Optional ByVal savedRow As Integer = 0, Optional ByVal printExportAsCampaign As Boolean = False)

        Dim tmpBook As Trinity.cBookingType = tmpB
        Dim tmpBundle = bundle

        Dim stationChannels = ""

        Dim totalPlannedGrossBudget As Integer = 0
        Dim totalPlannedNetBudget As Integer = 0

        Dim listOfSpots As New List(Of Trinity.cBookedSpot)

        Dim totalTRPForBT As Decimal = 0

        Dim target As String = ""

        With _sheet
            'Dim valueTest = .Cells(savedRow, 2).Value
            If tmpBundle And groupName = tmpGroupName Then
                row = savedRow
            ElseIf tmpBundle Then
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
                If groupName = "TV2" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "TV2 Group"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "DNN" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Discovery Networks Norway"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                    'ElseIf groupName = "FOXold" Then
                    '    If tmpBundle Then
                    '        .Cells(row, 2).Value = "FOXold"
                    '    Else
                    '        If .Cells(row, 2).Value Is Nothing Then
                    '            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                    '        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                    '            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                    '        End If
                    '    End If
                ElseIf groupName = "MTG" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "NENT"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "Matkanalen" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Matkanalen"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "TNT" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "TNT"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                End If
            Else
                If groupName = "MTG" Then
                    .Cells(row, 2).Value = "NENT"
                ElseIf groupName = "DNN" Then
                    .Cells(row, 2).Value = "Discovery Networks Norway"
                ElseIf groupName = "FOX" Then
                    .Cells(row, 2).Value = "FOX"
                ElseIf groupName = "TV2" Then
                    .Cells(row, 2).Value = "TV2 Group"
                Else
                    If .Cells(row, 2).Value Is Nothing Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                    ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                    End If
                End If
            End If


            Dim column = 3
            For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                totalTRPForBT = tmpWeek.TRP
                .Cells(row, column).Value += Math.Round(totalTRPForBT, 2)
                column = column + 1
            Next
        End With
    End Sub
    Sub PrintTable2()
        Dim filmColumnArray As String() = {":B", ":C", ":D", ":E", ":F", ":G", ":H", ":I", ":J", ":K", ":L", ":M", ":N", ":O", ":P", ":Q", ":R", ":S", ":T", ":U", ":V", ":W", ":X", ":Y", ":Z", ":AA", ":AB", ":AC", ":AD"}
        Dim columnValue As String = filmColumnArray.GetValue(1 + amountOfWeeks)
        Dim currentRow As Integer = lastRowComboTable1
        currentRow = currentRow + 1
        With _sheet
            .Range("B" & lastRowComboTable1 & columnValue & lastRowComboTable1).Interior.Color = RGB(0, 37, 110)
            .Range("B" & lastRowComboTable1 & columnValue & lastRowComboTable1).Font.Color = RGB(255, 255, 255)
            .Cells(lastRowComboTable1, 2).Value = "Primary target: TRP pr. station, pr. week"


            .Range("B" & lastRowComboTable1 + 1 & columnValue & lastRowComboTable1 + 1).Interior.Color = RGB(0, 137, 178)
            .Range("B" & lastRowComboTable1 + 1 & columnValue & lastRowComboTable1 + 1).Font.Color = RGB(255, 255, 255)
            .Cells(currentRow, 2).Value = "Network"

            'Print week headers
            For Each tmpchan As Trinity.cChannel In Campaign.Channels
                For Each tmpBook As Trinity.cBookingType In tmpchan.BookingTypes
                    If tmpBook.BookIt Then
                        Dim column As Integer = 3
                        For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
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

            Dim rowTV2 = 0
            Dim rowMTG = 0
            Dim rowDNN = 0
            Dim rowFOX = 0
            Dim rowTNT = 0

            Dim rowMatkanalen = 0
            Dim Matkanalenstring As String = ""

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
                                If checkNameTV2(c.ChannelName) Then
                                    rowCombination = tmpRow
                                    rowTV2 = rowCombination
                                ElseIf CheckNameDNN(c.ChannelName) Then
                                    rowCombination = tmpRow
                                    rowDNN = rowCombination
                                    'ElseIf checkNameFOXold(c.ChannelName) Then
                                    '    rowCombination = tmpRow
                                    '    rowFOX = rowCombination
                                ElseIf checkNameMTG(c.ChannelName) Then
                                    rowCombination = tmpRow
                                    rowMTG = rowCombination
                                ElseIf checkNameMatkanalen(c.ChannelName) Then
                                    rowCombination = tmpRow
                                    rowMatkanalen = rowCombination
                                End If
                                tmpChannelNameInputForCheckBundling = CheckChannel(c.ChannelName)
                                HelperTable2(tmpChan, tmpB, tmpChannelNameInputForCheckBundling, False, tmpRow,
                                             c.ChannelName, rowCombination, True)
                                Exit For
                            Next
                        Next
                    Next
                    tmpRow = tmpRow + 1
                Next
                ExportedAsCampaignCombination = True
            End If

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
                            If Not jumpBookingType Then
                                'MTG
                                If checkNameTV2(tmpChan.ChannelName) Then
                                    If BundleTV2 And rowTV2 <> 0 Then
                                        If rowTV2 = 0 Then
                                            rowTV2 = tmpRow
                                        End If
                                    Else
                                        rowTV2 = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    HelperTable2(tmpChan, tmpBook, "TV2", BundleTV2, tmpRow, tmpChan.ChannelName, rowTV2)
                                    currentRow = tmpRow
                                End If
                                If checkNameMTG(tmpChan.ChannelName) Then
                                    If BundleMTG And rowMTG <> 0 Then
                                        If rowMTG = 0 Then
                                            rowMTG = tmpRow
                                        End If
                                    Else
                                        rowMTG = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    HelperTable2(tmpChan, tmpBook, "MTG", BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG)
                                    currentRow = tmpRow
                                End If
                                If CheckNameDNN(tmpChan.ChannelName) Then
                                    If BundleDNN And rowDNN <> 0 Then
                                        If rowDNN = 0 Then
                                            rowDNN = tmpRow
                                        End If
                                    Else
                                        rowDNN = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    HelperTable2(tmpChan, tmpBook, "DNN", BundleDNN, tmpRow, tmpChan.ChannelName, rowDNN)
                                    currentRow = tmpRow
                                End If
                                'If checkNameFOXold(tmpChan.ChannelName) Then
                                '    If BundleFOX And rowFOX <> 0 Then
                                '        If rowFOX = 0 Then
                                '            rowFOX = tmpRow
                                '        End If
                                '    Else
                                '        rowFOX = tmpRow
                                '        tmpRow = tmpRow + 1
                                '    End If
                                '    HelperTable2(tmpChan, tmpBook, "FOXold", BundleFOX, tmpRow, tmpChan.ChannelName, rowFOX)
                                '    currentRow = tmpRow
                                'End If
                                If checkNameMatkanalen(tmpChan.ChannelName) Then
                                    If BundleMatkanalen And rowMatkanalen <> 0 Then
                                        If rowMatkanalen = 0 Then
                                            rowMatkanalen = tmpRow
                                        End If
                                    Else
                                        rowMatkanalen = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    HelperTable2(tmpChan, tmpBook, "Matkanalen", BundleMatkanalen, tmpRow, tmpChan.ChannelName, rowMatkanalen)
                                    currentRow = tmpRow
                                End If
                                If checkNameTNT(tmpChan.ChannelName) Then
                                    If BundleTNT And rowTNT <> 0 Then
                                        If rowTNT = 0 Then
                                            rowTNT = tmpRow
                                        End If
                                    Else
                                        rowTNT = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    HelperTable2(tmpChan, tmpBook, "TNT", BundleTNT, tmpRow, tmpChan.ChannelName, rowTNT)
                                    currentRow = tmpRow
                                End If
                            End If
                        End If
                    Next
                Next
            End If
        End With
        If currentRow < tmpRow Then
            currentRow = tmpRow
        End If
        _sheet.Range("B" & lastRowComboTable1 - 1 & ":M" & currentRow).Numberformat = "0.0"
        lastRowComboTable2 = currentRow + 1
    End Sub

    Sub helperTable3(ByVal tmpChan As Trinity.cChannel, ByVal tmpB As Trinity.cBookingType, ByVal tmpGroupName As String, ByVal bundle As Boolean, ByVal row As Integer, ByVal tmpChannelName As String, Optional ByVal savedRow As Integer = 0, Optional ByVal printExportAsCampaign As Boolean = False)

        Dim tmpBook As Trinity.cBookingType = tmpB
        Dim tmpBundle = bundle

        Dim stationChannels = ""

        Dim totalPlannedGrossBudget As Integer = 0
        Dim totalPlannedNetBudget As Integer = 0

        Dim listOfSpots As New List(Of Trinity.cBookedSpot)

        Dim totalTRPForBT As Decimal = 0

        Dim target As String = ""

        With _sheet

            If tmpBundle And groupName = tmpGroupName Then
                row = savedRow
            ElseIf tmpBundle Then
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
                    If .Cells(row, 2).Value Is Nothing Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames
                    ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames
                    End If
                ElseIf groupName = "MTG" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "NENT"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "DNN" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Discovery Networks Norway"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                    'ElseIf groupName = "FOXold" Then
                    '    If tmpBundle Then
                    '        .Cells(row, 2).Value = "FOXold"
                    '    Else
                    '        If .Cells(row, 2).Value Is Nothing Then
                    '            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                    '        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                    '            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                    '        End If
                    '    End If
                ElseIf groupName = "TV2" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "TV2 Group"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "Disney" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Disney C/XD"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "TNT" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "TNT"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "CMORE" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "C More"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "Matkanalen" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Matkanalen"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                End If
            Else
                If groupName = "MTG" Then
                    .Cells(row, 2).Value = "NENT"
                ElseIf groupName = "DNN" Then
                    .Cells(row, 2).Value = "Discovery Networks Norway"
                ElseIf groupName = "FOX" Then
                    .Cells(row, 2).Value = "FOX"
                ElseIf groupName = "TV2" Then
                    .Cells(row, 2).Value = "TV2 Group"
                Else
                    If .Cells(row, 2).Value Is Nothing Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                    ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                    End If
                End If
            End If
            Dim column = 3
            For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                totalTRPForBT = tmpWeek.TRPBuyingTarget
                .Cells(row, column).Value += Math.Round(totalTRPForBT, 2)
                column = column + 1
            Next
        End With
    End Sub
    Sub PrintTable3()
        Dim filmColumnArray As String()
        filmColumnArray = {":B", ":C", ":D", ":E", ":F", ":G", ":H", ":I", ":J", ":K", ":L", ":M", ":N", ":O", ":P", ":Q", ":R", ":S", ":T", ":U", ":V", ":W", ":X", ":Y", ":Z", ":AA", ":AB", ":AC", ":AD"}
        Dim columnValue As String = filmColumnArray.GetValue(1 + amountOfWeeks)
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
                        For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
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

            Dim rowTV2 = 0
            Dim rowDNN = 0
            Dim rowFOX = 0
            Dim rowMTG = 0
            Dim rowMatkanalen = 0
            Dim rowTNT = 0

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
                                If checkNameTV2(c.ChannelName) Then
                                    rowCombination = tmpRow
                                    rowTV2 = rowCombination
                                ElseIf CheckNameDNN(c.ChannelName) Then
                                    rowCombination = tmpRow
                                    rowDNN = rowCombination
                                    'ElseIf checkNameFOXold(c.ChannelName) Then
                                    '    rowCombination = tmpRow
                                    '    rowFOX = rowCombination
                                ElseIf checkNameMTG(c.ChannelName) Then
                                    rowCombination = tmpRow
                                    rowMTG = rowCombination
                                ElseIf checkNameMatkanalen(c.ChannelName) Then
                                    rowCombination = tmpRow
                                    rowMatkanalen = rowCombination
                                End If
                                tmpChannelNameInputForCheckBundling = CheckChannel(c.ChannelName)
                                helperTable3(tmpChan, tmpB, tmpChannelNameInputForCheckBundling, False, tmpRow,
                                             c.ChannelName, rowCombination, True)
                                Exit For
                            Next
                        Next
                    Next
                    tmpRow = tmpRow + 1
                Next
                ExportedAsCampaignCombination = True
            End If

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
                            If Not jumpBookingType Then
                                'TV2
                                If checkNameTV2(tmpChan.ChannelName) Then
                                    If BundleTV2 And rowTV2 <> 0 Then
                                        If rowTV2 = 0 Then
                                            rowTV2 = tmpRow
                                        End If
                                    Else
                                        rowTV2 = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    helperTable3(tmpChan, tmpBook, "TV2", BundleTV2, tmpRow, tmpChan.ChannelName, rowTV2)
                                    currentRow = tmpRow
                                End If
                                'MTG
                                If checkNameMTG(tmpChan.ChannelName) Then
                                    If BundleMTG And rowMTG <> 0 Then
                                        If rowMTG = 0 Then
                                            rowMTG = tmpRow
                                        End If
                                    Else
                                        rowMTG = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    helperTable3(tmpChan, tmpBook, "MTG", BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG)
                                    currentRow = tmpRow
                                End If
                                'DNN
                                If CheckNameDNN(tmpChan.ChannelName) Then
                                    If BundleDNN And rowDNN <> 0 Then
                                        If rowDNN = 0 Then
                                            rowDNN = tmpRow
                                        End If
                                    Else
                                        rowDNN = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    helperTable3(tmpChan, tmpBook, "DNN", BundleDNN, tmpRow, tmpChan.ChannelName, rowDNN)
                                    currentRow = tmpRow
                                End If
                                'If checkNameFOXold(tmpChan.ChannelName) Then
                                '    If BundleFOX And rowFOX <> 0 Then
                                '        If rowFOX = 0 Then
                                '            rowFOX = tmpRow
                                '        End If
                                '    Else
                                '        rowFOX = tmpRow
                                '        tmpRow = tmpRow + 1
                                '    End If
                                '    helperTable3(tmpChan, tmpBook, "FOXold", BundleFOX, tmpRow, tmpChan.ChannelName, rowFOX)
                                '    currentRow = tmpRow
                                'End If
                                If checkNameMatkanalen(tmpChan.ChannelName) Then
                                    If BundleMatkanalen And rowMatkanalen <> 0 Then
                                        If rowMatkanalen = 0 Then
                                            rowMatkanalen = tmpRow
                                        End If
                                    Else
                                        rowMatkanalen = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    helperTable3(tmpChan, tmpBook, "Matkanalen", BundleMatkanalen, tmpRow, tmpChan.ChannelName, rowMatkanalen)
                                    currentRow = tmpRow
                                End If
                                If checkNameTNT(tmpChan.ChannelName) Then
                                    If BundleTNT And rowTNT <> 0 Then
                                        If rowTNT = 0 Then
                                            rowTNT = tmpRow
                                        End If
                                    Else
                                        rowTNT = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    helperTable3(tmpChan, tmpBook, "TNT", BundleTNT, tmpRow, tmpChan.ChannelName, rowTNT)
                                    currentRow = tmpRow
                                End If
                            End If
                        End If
                    Next
                Next
            End If
        End With
        If currentRow < tmpRow Then
            currentRow = tmpRow
        End If
        _sheet.Range("B" & lastRowComboTable1 - 1 & ":M" & currentRow).Numberformat = "0.0"

        lastRowComboTable3 = currentRow + 1
    End Sub

    Sub HelperTable4(ByVal tmpChan As Trinity.cChannel, ByVal tmpB As Trinity.cBookingType, ByVal tmpGroupName As String, ByVal bundle As Boolean, ByVal row As Integer, ByVal tmpChannelName As String, Optional ByVal savedRow As Integer = 0, Optional rowheader As Integer = 0, Optional ByVal printExportAsCampaign As Boolean = False)

        Dim tmpBook As Trinity.cBookingType = tmpB
        Dim tmpBundle = bundle

        Dim stationChannels = ""

        Dim totalPlannedGrossBudget As Integer = 0
        Dim totalPlannedNetBudget As Integer = 0

        Dim listOfSpots As New List(Of Trinity.cBookedSpot)

        Dim target As String = ""

        With _sheet
            If tmpBundle And groupName = tmpGroupName Then
                row = savedRow
            ElseIf tmpBundle Then
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
                    If .Cells(row, 2).Value Is Nothing Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames
                    ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames
                    End If
                ElseIf groupName = "TV2" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "TV2 Group"
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
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "DNN" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Discovery Networks Norway"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                    'ElseIf groupName = "FOXold" Then
                    '    If tmpBundle Then
                    '        .Cells(row, 2).Value = "FOXold"
                    '    Else
                    '        If .Cells(row, 2).Value Is Nothing Then
                    '            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                    '        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                    '            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                    '        End If
                    '    End If
                ElseIf groupName = "Cartoon" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Cartoon"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "Disney" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Disney C/XD"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "TNT" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "TNT"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "CMORE" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "C More"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "Matkanalen" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Matkanalen"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                End If
            Else
                If groupName = "MTG" Then
                    .Cells(row, 2).Value = "NENT"
                ElseIf groupName = "DNN" Then
                    .Cells(row, 2).Value = "Discovery Networks Norway"
                    'ElseIf groupName = "FOXold" Then
                    '    .Cells(row, 2).Value = "FOXold"
                ElseIf groupName = "TV2" Then
                    .Cells(row, 2).Value = "TV2 Group"
                Else
                    If .Cells(row, 2).Value Is Nothing Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                    ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                    End If
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
                        For Each tmpWeek As Trinity.cWeek In tmpB.Weeks
                            trpForShare = tmpWeek.TRP
                            If (tmpF.Filmcode <> "") And tmpWeek.Films(tmpF.Filmcode) IsNot Nothing Then
                                tmpShare = tmpWeek.Films(tmpF.Filmcode).Share()
                                tmpFilm = tmpWeek.Films(tmpF.Filmcode).FilmLength
                            Else
                                tmpShare = tmpWeek.Films(tmpF.Name).Share()
                                tmpFilm = tmpWeek.Films(tmpF.Name).FilmLength
                            End If
                            If trpForShare <> 0 Then
                                trpForFilm += trpForShare * (tmpShare / 100)
                            End If
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
    Sub PrintTable4()
        Dim filmColumnArray As String() = {":B", ":C", ":D", ":E", ":F", ":G", ":H", ":I", ":J", ":K", ":L", ":M", ":N", ":O", ":P", ":Q", ":R", ":S", ":T", ":U", ":V", ":W", ":X", ":Y", ":Z", ":AA", ":AB", ":AC", ":AD"}
        Dim columnValue As String
        columnValue = filmColumnArray.GetValue(amountOfFilms)

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


        Dim rowTV2 = 0
        Dim rowMTG = 0
        Dim rowDNN = 0
        Dim rowFOX = 0
        Dim rowMatkanalen = 0
        Dim rowTNT = 0

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
                            If checkNameTV2(c.ChannelName) Then
                                rowCombination = tmpRow
                                rowTV2 = rowCombination
                            ElseIf CheckNameDNN(c.ChannelName) Then
                                rowCombination = tmpRow
                                rowDNN = rowCombination
                                'ElseIf checkNameFOXold(c.ChannelName) Then
                                '    rowCombination = tmpRow
                                '    rowFOX = rowCombination
                            ElseIf checkNameMTG(c.ChannelName) Then
                                rowCombination = tmpRow
                                rowMTG = rowCombination
                            ElseIf checkNameMatkanalen(c.ChannelName) Then
                                rowCombination = tmpRow
                                rowMatkanalen = rowCombination
                            End If
                            tmpChannelNameInputForCheckBundling = CheckChannel(c.ChannelName)
                            HelperTable4(tmpChan, tmpB, tmpChannelNameInputForCheckBundling, False, tmpRow,
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
                                If checkNameTV2(tmpChan.ChannelName) Then
                                    If BundleTV2 And rowTV2 <> 0 Then
                                        If rowTV2 = 0 Then
                                            rowTV2 = tmpRow
                                        End If
                                    Else
                                        rowTV2 = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    HelperTable4(tmpChan, tmpBook, "TV2", BundleTV2, tmpRow, tmpChan.ChannelName, rowTV2, rowHeader)
                                    currentRow = tmpRow
                                End If
                                If checkNameMTG(tmpChan.ChannelName) Then
                                    If BundleMTG And rowMTG <> 0 Then
                                        If rowMTG = 0 Then
                                            rowMTG = tmpRow
                                        End If
                                    Else
                                        rowMTG = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    HelperTable4(tmpChan, tmpBook, "MTG", BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG, rowHeader)
                                    currentRow = tmpRow
                                End If
                                If CheckNameDNN(tmpChan.ChannelName) Then
                                    If BundleDNN And rowDNN <> 0 Then
                                        If rowDNN = 0 Then
                                            rowDNN = tmpRow
                                        End If
                                    Else
                                        rowDNN = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    HelperTable4(tmpChan, tmpBook, "DNN", BundleDNN, tmpRow, tmpChan.ChannelName, rowDNN, rowHeader)
                                    currentRow = tmpRow
                                End If
                                'If checkNameFOXold(tmpChan.ChannelName) Then
                                '    If BundleFOX And rowFOX <> 0 Then
                                '        If rowFOX = 0 Then
                                '            rowFOX = tmpRow
                                '        End If
                                '    Else
                                '        rowFOX = tmpRow
                                '        tmpRow = tmpRow + 1
                                '    End If
                                '    HelperTable4(tmpChan, tmpBook, "FOXold", BundleFOX, tmpRow, tmpChan.ChannelName, rowFOX, rowHeader)
                                '    currentRow = tmpRow
                                'End If
                                If checkNameMatkanalen(tmpChan.ChannelName) Then
                                    If BundleMatkanalen And rowMatkanalen <> 0 Then
                                        If rowMatkanalen = 0 Then
                                            rowMatkanalen = tmpRow
                                        End If
                                    Else
                                        rowMatkanalen = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    HelperTable4(tmpChan, tmpBook, "Matkanalen", BundleMatkanalen, tmpRow, tmpChan.ChannelName, rowMatkanalen, rowHeader)
                                    currentRow = tmpRow
                                End If
                                If checkNameTNT(tmpChan.ChannelName) Then
                                    If BundleTNT And rowTNT <> 0 Then
                                        If rowTNT = 0 Then
                                            rowTNT = tmpRow
                                        End If
                                    Else
                                        rowTNT = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    HelperTable4(tmpChan, tmpBook, "TNT", BundleTNT, tmpRow, tmpChan.ChannelName, rowTNT, rowHeader)
                                    currentRow = tmpRow
                                End If
                            End If
                        End If
                    Next
                Next
            End If
        End With
        If currentRow < tmpRow Then
            currentRow = tmpRow
        End If
        _sheet.Range("B" & lastRowComboTable1 - 1 & ":M" & currentRow).Numberformat = "0.0"
        lastRowComboTable4 = currentRow + 1
    End Sub
    Sub helperTable5(ByVal tmpChan As Trinity.cChannel, ByVal tmpB As Trinity.cBookingType, ByVal tmpGroupName As String, ByVal bundle As Boolean, ByVal row As Integer, ByVal tmpChannelName As String, Optional ByVal savedRow As Integer = 0, Optional rowheader As Integer = 0, Optional ByVal printExportAsCampaign As Boolean = False)

        Dim tmpBook As Trinity.cBookingType = tmpB
        Dim tmpBundle = bundle

        Dim stationChannels = ""

        Dim totalPlannedGrossBudget As Integer = 0
        Dim totalPlannedNetBudget As Integer = 0

        Dim listOfSpots As New List(Of Trinity.cBookedSpot)

        Dim target As String = ""

        With _sheet
            If tmpBundle And groupName = tmpGroupName Then
                row = savedRow
            ElseIf tmpBundle And groupName = tmpGroupName Then
            ElseIf tmpBundle Then
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
                    If .Cells(row, 2).Value Is Nothing Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames
                    ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames
                    End If
                ElseIf groupName = "TV2" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "TV2 Group"
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
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "DNN" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Discovery Networks Norway"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                    'ElseIf groupName = "FOXold" Then
                    '    If tmpBundle Then
                    '        .Cells(row, 2).Value = "FOXold"
                    '    Else
                    '        If .Cells(row, 2).Value Is Nothing Then
                    '            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                    '        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                    '            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                    '        End If
                    '    End If
                ElseIf groupName = "Cartoon" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Cartoon"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "Disney" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Disney C/XD"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "TNT" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "TNT"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "CMORE" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "C More"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                ElseIf groupName = "Matkanalen" Then
                    If tmpBundle Then
                        .Cells(row, 2).Value = "Matkanalen"
                    Else
                        If .Cells(row, 2).Value Is Nothing Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                            .Cells(row, 2).Value += tmpChan.AdEdgeNames
                        End If
                    End If
                End If
            Else
                If groupName = "MTG" Then
                    .Cells(row, 2).Value = "NENT"
                ElseIf groupName = "DNN" Then
                    .Cells(row, 2).Value = "Discovery Networks Norway"
                    'ElseIf groupName = "FOXold" Then
                    '    .Cells(row, 2).Value = "FOXold"
                ElseIf groupName = "TV2" Then
                    .Cells(row, 2).Value = "TV2 Group"
                Else
                    If .Cells(row, 2).Value Is Nothing Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                    ElseIf Not .Cells(row, 2).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                        .Cells(row, 2).Value += tmpChan.AdEdgeNames + " "
                    End If
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
            .Range("B" & row & ":B" & row).Interior.Color = RGB(0, 137, 178)
            .Range("B" & row & ":B" & row).Font.Color = RGB(255, 255, 255)
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
                            trpForShare = tmpWeek.TRPBuyingTarget
                            If (tmpF.Filmcode <> "") and tmpWeek.Films(tmpF.Filmcode) isnot nothing Then
                                tmpShare = tmpWeek.Films(tmpF.Filmcode).Share()
                                tmpFilm = tmpWeek.Films(tmpF.Filmcode).FilmLength
                            Else
                                tmpShare = tmpWeek.Films(tmpF.Name).Share()
                                tmpFilm = tmpWeek.Films(tmpF.Name).FilmLength
                            End If
                            If trpForShare <> 0 Then
                                trpForFilm += trpForShare * (tmpShare / 100)
                            End If
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


    Sub printTable5()
        Dim filmColumnArray As String() = {":B", ":C", ":D", ":E", ":F", ":G", ":H", ":I", ":J", ":K", ":L", ":M", ":N", ":O", ":P", ":Q", ":R", ":S", ":T", ":U", ":V", ":W", ":X", ":Y", ":Z", ":AA", ":AB", ":AC", ":AD"}
        Dim columnValue As String = filmColumnArray.GetValue(amountOfFilms)

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


        Dim rowTV2 = 0
        Dim rowMTG = 0
        Dim rowDNN = 0
        Dim rowFOX = 0
        Dim rowMatkanalen = 0
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
                            If checkNameTV2(c.ChannelName) Then
                                rowCombination = tmpRow
                                rowTV2 = rowCombination
                            ElseIf CheckNameDNN(c.ChannelName) Then
                                rowCombination = tmpRow
                                rowDNN = rowCombination
                                'ElseIf checkNameFOXold(c.ChannelName) Then
                                '    rowCombination = tmpRow
                                '    rowFOX = rowCombination
                            ElseIf checkNameMTG(c.ChannelName) Then
                                rowCombination = tmpRow
                                rowMTG = rowCombination
                            ElseIf checkNameMatkanalen(c.ChannelName) Then
                                rowCombination = tmpRow
                                rowMatkanalen = rowCombination
                            End If
                            tmpChannelNameInputForCheckBundling = CheckChannel(c.ChannelName)
                            helperTable5(tmpChan, tmpB, tmpChannelNameInputForCheckBundling, False, tmpRow,
                                         c.ChannelName, rowCombination, rowHeader, true)
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
                                If checkNameTV2(tmpChan.ChannelName) Then
                                    If BundleTV2 And rowTV2 <> 0 Then
                                        If rowTV2 = 0
                                            rowTV2 = tmpRow
                                        End If
                                    Else
                                        rowTV2 = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    helperTable5(tmpChan, tmpBook, "TV2", BundleTV2, tmpRow, tmpChan.ChannelName, rowTV2, rowHeader)
                                    currentRow = tmpRow
                                End If
                                If checkNameMTG(tmpChan.ChannelName) Then
                                    If BundleMTG And rowMTG <> 0 Then
                                        If rowMTG = 0
                                            rowMTG = tmpRow
                                        End If
                                    Else
                                        rowMTG = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    helperTable5(tmpChan, tmpBook, "MTG", BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG, rowHeader)
                                    currentRow = tmpRow
                                End If
                                If CheckNameDNN(tmpChan.ChannelName) Then
                                     If BundleDNN And rowDNN <> 0 Then
                                        If rowDNN = 0
                                            rowDNN = tmpRow
                                        End If
                                    Else
                                        rowDNN = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    helperTable5(tmpChan, tmpBook, "DNN", BundleDNN, tmpRow, tmpChan.ChannelName, rowDNN, rowHeader)
                                    currentRow = tmpRow
                                End If
                                'If checkNameFOXold(tmpChan.ChannelName) Then
                                '    If BundleFOX And rowFOX <> 0 Then
                                '        If rowFOX = 0 Then
                                '            rowFOX = tmpRow
                                '        End If
                                '    Else
                                '        rowFOX = tmpRow
                                '        tmpRow = tmpRow + 1
                                '    End If
                                '    helperTable5(tmpChan, tmpBook, "FOXold", BundleFOX, tmpRow, tmpChan.ChannelName, rowFOX, rowHeader)
                                '    currentRow = tmpRow
                                'End If

                                If checkNameMatkanalen(tmpChan.ChannelName) Then
                                    If BundleMatkanalen And rowMatkanalen <> 0 Then
                                        If rowMatkanalen = 0
                                            rowMatkanalen = tmpRow
                                        End If
                                    Else
                                        rowMatkanalen = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    helperTable5(tmpChan, tmpBook, "Matkanalen", BundleMatkanalen, tmpRow, tmpChan.ChannelName, rowMatkanalen, rowHeader)
                                    currentRow = tmpRow
                                End If
                                If checkNameTNT(tmpChan.ChannelName) Then
                                    If BundleTNT And rowTNT <> 0 Then
                                        If rowTNT = 0
                                            rowTNT = tmpRow
                                        End If
                                    Else
                                        rowTNT = tmpRow
                                        tmpRow = tmpRow + 1
                                    End If
                                    helperTable5(tmpChan, tmpBook, "TNT", BundleTNT, tmpRow, tmpChan.ChannelName, rowTNT, rowHeader)
                                    currentRow = tmpRow
                                End If
                            End If
                        End If
                    Next
                Next
            End If
        End With
        If currentRow < tmpRow
            currentRow = tmpRow
        End If
        _sheet.Range("B" & lastRowComboTable1 - 1 & ":M" & currentRow).Numberformat = "0.0"
        lastRowComboTable5 = currentRow + 1
    End Sub
     Sub printTable6()

        Dim weekCol As String() = {":B", ":C", ":D", ":E", ":F", ":G", ":H", ":I", ":J", ":K", ":L", ":M", ":N", ":O", ":P", ":Q", ":R", ":S", ":T", ":U", ":V", ":W", ":X", ":Y", ":Z", ":AA", ":AB", ":AC", ":AD"}
        Dim columnValue As String = weekCol.GetValue(3 + amountOfWeeks)
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

        Dim filmLengths As new List(Of Integer)
        
        Dim x As Integer = 0
        For each tmpfilm as cFilm In listOfFilms
            If not filmLengths.contains(listOfFilms.Item(x).FilmLength)
                filmLengths.Add(listOfFilms.Item(x).FilmLength)
            End If
            x = x + 1
        Next

        For each tmpLength As Byte In filmLengths
            For i As Integer = 1 To listOfFilms.Count
                With _sheet
                    For Each tmpChan As Trinity.cChannel In Campaign.Channels
                        For Each tmpBt As Trinity.cBookingType In tmpChan.BookingTypes
                            If tmpBt.BookIt Then
                                .Cells(currentRow, 2).Value = amountOfFilms
                                Dim totalTRPForWeek As Decimal = 0
                                Dim startingColumn = 6
                                For Each tmpWeek As Trinity.cWeek In tmpBt.Weeks                                    
                                    If tmpWeek.Films(i).FilmLength = tmpLength                                            
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
                                        ElseIf Not .Cells(currentRow, 4).Text.Contains(tmpWeek.Films(i).Filmcode)
                                            .Cells(currentRow, 4).Value += tmpWeek.Films(i).Filmcode + ";"
                                        End If
                                        If .Cells(currentRow, 5).Value Is Nothing Then
                                            .Cells(currentRow, 5).Value = tmpWeek.Films(i).FilmLength
                                        End If
                                        .Cells(currentRow, startingColumn).Value += Math.Round(totalTRPForWeek, 2)
                                        startingColumn += 1
                                        totalTRPForWeek = 0
                                    End If
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
