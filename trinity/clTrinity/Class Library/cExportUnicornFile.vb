Imports clTrinity.CultureSafeExcel
Public Class cExportUnicornFile
    Dim _excel As Application
    Dim _wb As Workbook
    Dim _sheet As Worksheet

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


    Dim listOfFilms As New List(Of Trinity.cFilm)

    Dim tmpBundleTV4 As Boolean = True
    Dim tmpBundleMTG As Boolean = False
    Dim tmpBundleSBS As Boolean = False
    Dim tmpBundleFOX As Boolean = False

    Dim _campaign As Trinity.cKampanj

    Dim tmpRow As Integer = 39

    Dim targetTV4 As String = ""
    Dim targetTMTG As String = ""
    Dim targetTSBS As String = ""
    Dim targetTFOX As String = ""
    Dim targetTCMORE As String = ""

    Public Sub printUnicornFile(Optional ByVal bundleTV4 As Boolean = False, Optional ByVal bundleMTG As Boolean = False, Optional ByVal bundleSBS As Boolean = False)
        checkBundling(bundleMTG, bundleSBS)
        _wb = _excel.AddWorkbook
        _sheet = _wb.Sheets(1)
        getFilms()
        With _sheet

            printHeader()
            printTable1()
            printTable2()
            printTable3()
            printTable4()
            printTable5()
            printTable6()
            CleanUp()
            For i As Integer = 1 To 20
                .Columns(i).AutoFit()
            Next
        End With
        _sheet.Range("B" & 2 & ":M" & 200).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
        _sheet.Range("B" & 2 & ":M" & 200).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
    End Sub

    Public Sub New(ByRef Campaign As Trinity.cKampanj)
        GetAmountOfWeeks()
        _excel = New Application(False)
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
    Function translateTargetName(ByVal tmptargetName As String)
        Dim targetValue As String = tmptargetName

        If targetValue.Contains("K") Then
            targetValue = targetValue.Replace("K", "W")
        ElseIf Not targetValue.Contains("A") Then
            targetValue = "A" & targetValue
        End If

        Return targetValue
    End Function
    Sub checkBundling(Optional ByVal bundleTV4 As Boolean = False, Optional ByVal bundleMTG As Boolean = False, Optional ByVal bundleSBS As Boolean = False)
        If bundleMTG Then
            tmpBundleMTG = True
        Else
            tmpBundleMTG = False
        End If
        If bundleSBS Then
            tmpBundleSBS = True
        Else
            tmpBundleSBS = False
        End If
        If bundleTV4 Then
            tmpBundleTV4 = True
        Else
            tmpBundleTV4 = False
        End If
    End Sub

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
            .Cells(3, 3).Value = "SE"
            Dim userCompany As String = ""
            If TrinitySettings.UserEmail.Contains("mecglobal") Then
                userCompany = "MEC"
            ElseIf TrinitySettings.UserEmail.Contains("mediacom") Then
                userCompany = "Mediacom"
            ElseIf TrinitySettings.UserEmail.Contains("maxus") Then
                userCompany = "Maxus"
            ElseIf TrinitySettings.UserEmail.Contains("mindshare") Then
                userCompany = "Mindshare"
            End If

            If TrinitySettings.UserEmail = "" Then

            End If

            .Cells(4, 3).Value = userCompany
            .Cells(5, 3).Value = Campaign.Planner
            .Cells(6, 3).Value = "Adults"
            .Cells(7, 3).Value = "TV Campaign"
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


            Dim tmpMainTarget As String = translateTargetName(Campaign.MainTarget.TargetName())
            Dim tmpSecondTarget As String = translateTargetName(Campaign.SecondaryTarget.TargetName())
            .Cells(20, 3).Value = tmpMainTarget
            .Cells(21, 3).Value = tmpSecondTarget
            .Cells(22, 3).Value = (Campaign.FrequencyFocus + 1) & "+"

            Dim row As Integer = 26
            Dim plus As String = "+"
            For i As Integer = 1 To 10
                .Cells(row, 2).Value = i & plus
                .Range("B" & row & ":B" & row).Font.Color = RGB(255, 255, 255)
                .Range("B" & row & ":B" & row).Interior.Color = RGB(0, 137, 178) 'Blue color

                Dim reach As Double = Campaign.ReachGoal(i, Trinity.cKampanj.ReachTargetEnum.rteMainTarget)
                reach = Math.Round(reach, 1)
                '.Cells(row, 3).Value = reach & "%"
                .Cells(row, 3).Value = reach
                row = row + 1
            Next
        End With
    End Sub

    Sub printBookingTable1(ByVal tmpChan As Trinity.cChannel, ByVal tmpB As Trinity.cBookingType, ByVal bundle As Boolean, ByVal row As Integer, ByVal channelName As String, Optional ByVal savedRow As Integer = 0)

        Dim tmpBook As Trinity.cBookingType = tmpB
        Dim tmpBundle = bundle

        Dim stationChannels = ""

        Dim totalPlannedGrossBudget As Integer = 0
        Dim totalPlannedNetBudget As Integer = 0

        Dim listOfSpots As New List(Of Trinity.cBookedSpot)

        Dim totalTRPForBT As Decimal = 0

        Dim target As String = ""

        With _sheet
            If tmpBundle And (tmpChan.ChannelName = "TV3" Or tmpChan.ChannelName = "TV6") Then
                row = savedRow
            ElseIf channelName = "TV4" Or channelName = "SBS" Then
                If savedRow = 0 Then
                    tmpRow = tmpRow + 1
                    row = savedRow
                End If
            ElseIf tmpChan.ChannelName = "TV8" And tmpBundle Then
                tmpRow = tmpRow + 1
                row = tmpRow
                tmpRow = tmpRow + 1
            Else
                row = tmpRow
                tmpRow = tmpRow + 1
            End If
            .Range("B" & row & ":C" & row).Interior.Color = RGB(0, 137, 178)
            .Range("B" & row & ":C" & row).Font.Color = RGB(255, 255, 255)
            Dim test = .Cells(row, 2).Value
            If TrinitySettings.Developer Then
                .Cells(row, 13).Value += tmpChan.ChannelName + " " + tmpBook.Name + " "
            End If
            If .Cells(row, 2).Value = "" Then
                If channelName = "TV4" Then
                    .Cells(row, 2).Value = "TV4"
                    If .Cells(tmpRow, 2).Value = "" Then
                        .Cells(row, 2).Value = tmpChan.AdEdgeNames
                    End If
                    .Cells(row, 3).Value += tmpChan.AdEdgeNames + ";"
                ElseIf channelName = "MTG" Then
                    .Cells(row, 2).Value = "MTG TV"
                    If Not stationChannels.Contains(tmpChan.AdEdgeNames) Then
                        stationChannels = "TV3 se; TV6 SE; TV8; TV10; MTV se; Comedy Central;"
                    End If
                    .Cells(row, 3).Value = stationChannels
                ElseIf channelName = "SBS" Then
                    .Cells(row, 2).Value = "SBS Discovery"
                    If Not stationChannels.Contains(tmpChan.AdEdgeNames) Then
                        stationChannels = "Kanal 5; Kanal 9; Kanal11; Discovery se; TLC; Investigation Discovery; Eurosport SE; Eurosport 2 SE;"
                    End If
                ElseIf channelName = "FOX" Then
                    .Cells(row, 2).Value = "Fox International"
                    If Not stationChannels.Contains(tmpChan.AdEdgeNames) Then
                        .Cells(row, 2).Value = "Fox International"
                    End If
                ElseIf channelName = "CMORE" Then
                    stationChannels = "C More"
                End If
            End If

            If Not tmpBook.IsSpecific Then
                Dim tmpCTC As Integer = 0
                Dim tmptotalPlannedGrossBudget = 0
                If tmpBook.ConfirmedGrossBudget = 0 Then
                    tmptotalPlannedGrossBudget = tmpBook.PlannedGrossBudget
                Else
                    tmptotalPlannedGrossBudget = tmpBook.ConfirmedGrossBudget
                End If
                Dim tmptotalPlannedNetBudget = 0
                If tmpBook.ConfirmedNetBudget = 0 Then
                    tmptotalPlannedNetBudget = tmpBook.PlannedNetBudget
                Else
                    tmptotalPlannedNetBudget = tmpBook.ConfirmedNetBudget
                End If
                totalPlannedGrossBudget += tmptotalPlannedGrossBudget
                totalPlannedNetBudget += tmptotalPlannedNetBudget

                tmpCTC = tmptotalPlannedNetBudget * 0.06
                tmpCTC = tmptotalPlannedNetBudget - tmpCTC
                .Cells(row, 10).Value += Math.Round(tmpCTC, 2) 'CTC
                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                    totalTRPForBT += tmpWeek.TRP
                Next
                .Cells(row, 11).Value += Math.Round(totalTRPForBT, 1)
            Else
                Dim tmptotalPlannedGrossBudget = 0
                Dim tmptotalPlannedNetBudget = 0
                For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                    If tmpSpot.Bookingtype Is tmpBook Then
                        listOfSpots.Add(tmpSpot)
                    End If
                Next
                Dim tmpCTC As Integer = 0
                For Each spot As Trinity.cBookedSpot In listOfSpots
                    If spot.MyEstimate <> 0 Then
                        totalTRPForBT += spot.MyEstimate
                    Else
                        totalTRPForBT += spot.ChannelEstimate
                    End If
                    If spot.AddedValues.Count > 0 Then
                        tmptotalPlannedGrossBudget += spot.GrossPrice * spot.AddedValueIndex
                        tmptotalPlannedNetBudget += spot.NetPrice * spot.AddedValueIndex
                    Else
                        tmptotalPlannedGrossBudget += spot.GrossPrice
                        tmptotalPlannedNetBudget += spot.NetPrice
                    End If
                Next
                tmpCTC = totalPlannedNetBudget * tmpChan.AgencyCommission
                tmpCTC = totalPlannedNetBudget - tmpCTC
                .Cells(row, 10).Value = Math.Round(tmpCTC) 'CTC
                .Cells(row, 11).Value = Math.Round(totalTRPForBT, 1)
                totalPlannedGrossBudget = tmptotalPlannedGrossBudget
                totalPlannedNetBudget = tmptotalPlannedNetBudget
            End If

            .Cells(row, 4).Value = Math.Round(totalPlannedGrossBudget) 'Gross budget
            .Cells(row, 5).Value = Math.Round(totalPlannedNetBudget) 'Net budget
            .Cells(row, 6).Value = Math.Round(totalPlannedNetBudget * (1 - tmpChan.AgencyCommission)) 'Net net
            .Cells(row, 7).Value = Math.Round(totalPlannedNetBudget * tmpChan.AgencyCommission) 'Fees

            .Cells(row, 8).Value = "" 'Other
            .Cells(row, 9).Value = "" 'Fond

            If Not target.Contains(translateTargetName(tmpBook.BuyingTarget.ToString())) Then
                target = target & translateTargetName(tmpBook.BuyingTarget.ToString) & ";"
            End If
            .Cells(row, 12).Value = target
        End With
    End Sub

    Sub printTable1()
        'Print the first table for channels
        With _sheet
            .Range("B" & 37 & ":L" & 37).Interior.Color = RGB(0, 37, 110)
            .Range("B" & 37 & ":L" & 37).Font.Color = RGB(255, 255, 255)
            .Range("B" & 38 & ":L" & 38).Interior.Color = RGB(0, 137, 178)
            .Range("B" & 38 & ":L" & 38).Font.Color = RGB(255, 255, 255)

            Dim rowTV4 = 0
            Dim rowMTG = 0
            Dim rowSBS = 0
            Dim rowFOX = 0
            Dim rowCMORE = 0

            Dim testBool As Boolean = False

            Dim stationChannelsTV4 As String = ""
            Dim stationChannelsMTG As String = ""
            Dim stationChannelsSBS As String = ""
            Dim stationChannelsFOX As String = ""
            Dim stationChannelsCMORE As String = ""

            Dim totalPlannedGrossBudgetTV4 As Decimal = 0
            Dim totalPlannedGrossBudgetSBS As Decimal = 0
            Dim totalPlannedGrossBudgetMTG As Decimal = 0
            Dim totalPlannedGrossBudgetFOX As Decimal = 0
            Dim totalPlannedGrossBudgetCMORE As Decimal = 0

            Dim totalPlannedNetBudgetTV4 = 0
            Dim totalPlannedNetBudgetSBS = 0
            Dim totalPlannedNetBudgetMTG = 0
            Dim totalPlannedNetBudgetFOX = 0
            Dim totalPlannedNetBudgetCMORE = 0

            Dim totalTRPForBTMTG = 0
            Dim totalTRPForBTSBS = 0
            Dim totalTRPForBTFOX = 0
            Dim totalTRPForBTCMORE = 0

            Dim listOfTV4Spots As New List(Of Trinity.cBookedSpot)
            Dim listOfSBSSpots As New List(Of Trinity.cBookedSpot)
            Dim listOfMTGSpots As New List(Of Trinity.cBookedSpot)
            Dim listOfFoxSpots As New List(Of Trinity.cBookedSpot)
            Dim listOfCMORESpots As New List(Of Trinity.cBookedSpot)

            For Each tmpChan As Trinity.cChannel In Campaign.Channels
                For Each tmpBook As Trinity.cBookingType In tmpChan.BookingTypes
                    If tmpBook.BookIt Then
                        'TV4
                        If tmpBundleTV4 Then
                            If rowMTG = 0 Then
                                rowMTG = tmpRow
                            End If
                            printBookingTable1(tmpChan, tmpBook, tmpBundleMTG, tmpRow, "MTG", rowMTG)
                        Else
                            printBookingTable1(tmpChan, tmpBook, tmpBundleMTG, tmpRow, "MTG")
                        End If


                        'If tmpBook.ToString().Contains("TV4") Or tmpBook.ToString().Contains("Sjuan") Or tmpBook.ToString().Contains("TV12") Then
                        '    If rowTV4 = 0 Then
                        '        rowTV4 = tmpRow
                        '    End If
                        '    printBookingTable1(tmpChan, tmpBook, bundleTV4, tmpRow, "TV4", rowTV4)
                        '    tmpRow = tmpRow + 1
                        'End If

                        'If tmpBook.ToString().Contains("TV4") Or tmpBook.ToString().Contains("Sjuan") Or tmpBook.ToString().Contains("TV12") Then
                        '    Dim totalTRPForBTTV4 = 0
                        '    If tmpChan.ChannelName = "TV4" Then
                        '        If rowTV4 = 0 Then
                        '            rowTV4 = tmpRow
                        '            tmpRow = tmpRow + 1

                        '            .Range("B" & rowTV4 & ":C" & rowTV4).Interior.Color = RGB(0, 137, 178)
                        '            .Range("B" & rowTV4 & ":C" & rowTV4).Font.Color = RGB(255, 255, 255)
                        '        End If
                        '        .Range("B" & rowTV4 & ":C" & rowTV4).Interior.Color = RGB(0, 137, 178)
                        '        .Range("B" & rowTV4 & ":C" & rowTV4).Font.Color = RGB(255, 255, 255)
                        '    Else
                        '        .Range("B" & tmpRow & ":C" & tmpRow).Interior.Color = RGB(0, 137, 178)
                        '        .Range("B" & tmpRow & ":C" & tmpRow).Font.Color = RGB(255, 255, 255)
                        '    End If

                        '    If tmpChan.ChannelName = "TV4" Then
                        '        tmpRow = rowTV4
                        '    End If

                        '    If .Cells(tmpRow, 2).Value = "" Then
                        '        .Cells(tmpRow, 2).Value = tmpChan.AdEdgeNames
                        '    End If
                        '    .Cells(tmpRow, 3).Value = tmpChan.AdEdgeNames

                        '    If Not tmpBook.IsSpecific Then
                        '        Dim tmpCTC As Integer = 0
                        '        Dim tmptotalPlannedGrossBudgetTV4 = 0
                        '        If tmpBook.ConfirmedGrossBudget = 0 Then
                        '            tmptotalPlannedGrossBudgetTV4 = tmpBook.PlannedGrossBudget
                        '        Else
                        '            tmptotalPlannedGrossBudgetTV4 = tmpBook.ConfirmedGrossBudget
                        '        End If
                        '        Dim tmptotalPlannedNetBudgetTV4 = 0
                        '        If tmpBook.ConfirmedNetBudget = 0 Then
                        '            tmptotalPlannedNetBudgetTV4 = tmpBook.PlannedNetBudget
                        '        Else
                        '            tmptotalPlannedNetBudgetTV4 = tmpBook.ConfirmedNetBudget
                        '        End If
                        '        totalPlannedGrossBudgetTV4 = tmptotalPlannedGrossBudgetTV4
                        '        totalPlannedNetBudgetTV4 = tmptotalPlannedNetBudgetTV4

                        '        tmpCTC = tmptotalPlannedNetBudgetTV4 * tmpChan.AgencyCommission
                        '        tmpCTC = tmptotalPlannedNetBudgetTV4 - tmpCTC
                        '        .Cells(tmpRow, 10).Value += Math.Round(tmpCTC) 'CTC

                        '        For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                        '            totalTRPForBTTV4 += tmpWeek.TRP
                        '        Next
                        '        .Cells(tmpRow, 11).Value += Math.Round(totalTRPForBTTV4, 1)
                        '    Else
                        '        Dim tmptotalPlannedGrossBudgetTV4 = 0
                        '        Dim tmptotalPlannedNetBudgetTV4 = 0
                        '        For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                        '            If tmpSpot.Bookingtype Is tmpBook Then
                        '                listOfTV4Spots.Add(tmpSpot)
                        '            End If
                        '        Next
                        '        Dim tmpCTC As Integer = 0
                        '        Dim totalTRPForBT As Double = 0
                        '        For Each spot As Trinity.cBookedSpot In listOfTV4Spots
                        '            If spot.MyEstimate <> 0 Then
                        '                totalTRPForBT += spot.MyEstimate
                        '            Else
                        '                totalTRPForBT += spot.ChannelEstimate
                        '            End If
                        '            If spot.AddedValues.Count > 0 Then
                        '                tmptotalPlannedGrossBudgetTV4 += spot.GrossPrice * spot.AddedValueIndex
                        '                tmptotalPlannedNetBudgetTV4 += spot.NetPrice * spot.AddedValueIndex
                        '            Else
                        '                tmptotalPlannedGrossBudgetTV4 += spot.GrossPrice
                        '                tmptotalPlannedNetBudgetTV4 += spot.NetPrice
                        '            End If

                        '        Next
                        '        tmpCTC = tmptotalPlannedNetBudgetTV4 * tmpChan.AgencyCommission
                        '        tmpCTC = tmptotalPlannedNetBudgetTV4 - tmpCTC
                        '        .Cells(tmpRow, 10).Value += Math.Round(tmpCTC) 'CTC
                        '        totalTRPForBTTV4 = totalTRPForBT
                        '        .Cells(tmpRow, 11).Value += Math.Round(totalTRPForBTTV4, 1)
                        '        totalPlannedGrossBudgetTV4 = tmptotalPlannedGrossBudgetTV4
                        '        totalPlannedNetBudgetTV4 = tmptotalPlannedNetBudgetTV4
                        '    End If

                        '    .Cells(tmpRow, 4).Value += Math.Round(totalPlannedGrossBudgetTV4) 'Gross budget 
                        '    .Cells(tmpRow, 5).Value += Math.Round(totalPlannedNetBudgetTV4) 'Net budget
                        '    .Cells(tmpRow, 6).Value += Math.Round(totalPlannedNetBudgetTV4 * (1 - tmpChan.AgencyCommission)) 'Net net
                        '    .Cells(tmpRow, 7).Value += Math.Round(totalPlannedNetBudgetTV4 * tmpChan.AgencyCommission) 'Fees

                        '    .Cells(tmpRow, 8).Value += "" 'Other
                        '    .Cells(tmpRow, 9).Value += "" 'Fond

                        '    .Cells(tmpRow, 12).Value = translateTargetName(tmpBook.BuyingTarget.TargetName) & "; "
                        '    tmpRow = tmpRow + 1
                    End If
                    'MTG
                    If tmpBook.ToString().Contains("TV3") Or tmpBook.ToString().Contains("TV6") Or tmpBook.ToString().Contains("MTV") Or tmpBook.ToString().Contains("TV8") Or tmpBook.ToString().Contains("TV10") Or tmpBook.ToString().Contains("Comedy central") Or tmpBook.ToString().Contains("Comedy") Or tmpBook.ToString().Contains("Nickelodeon") Then
                        If tmpBundleMTG Then
                            If rowMTG = 0 Then
                                rowMTG = tmpRow
                            End If
                            printBookingTable1(tmpChan, tmpBook, tmpBundleMTG, tmpRow, "MTG", rowMTG)
                        Else
                            printBookingTable1(tmpChan, tmpBook, tmpBundleMTG, tmpRow, "MTG")
                        End If
                    End If
                    'SBS
                    If tmpBook.ToString().Contains("Kanal5") Or tmpBook.ToString().Contains("Kanal9") Or tmpBook.ToString().Contains("Kanal 11") Or tmpBook.ToString().Contains("Kanal11") Or tmpBook.ToString().Contains("TV11") Or tmpBook.ToString().Contains("Eurosport") Or tmpBook.ToString().Contains("Discovery") Or tmpBook.ToString().Contains("TLC") Or tmpBook.ToString().Contains("ID") Or tmpBook.ToString().Contains("Investigation Discovery") Then
                        'Temporary bool to bundle
                        tmpBundleSBS = True

                        'If tmpBundleMTG Then
                        '    If rowSBS = 0 Then
                        '        rowSBS = tmpRow
                        '    End If
                        '    printBookingTable1(tmpChan, tmpBook, tmpBundleSBS, tmpRow, "SBS", rowSBS)
                        'End If
                        If tmpBook.BookIt Then
                            If tmpBundleSBS Then
                                If rowSBS = 0 Then
                                    rowSBS = tmpRow
                                    tmpRow = tmpRow + 1
                                End If
                            Else
                                rowSBS = tmpRow
                                tmpRow = tmpRow + 1
                            End If

                            .Range("B" & rowSBS & ":C" & rowSBS).Interior.Color = RGB(0, 137, 178)
                            .Range("B" & rowSBS & ":C" & rowSBS).Font.Color = RGB(255, 255, 255)

                            If .Cells(rowSBS, 2).Value = "" Then
                                .Cells(rowSBS, 2).Value = "SBS Discovery"
                            End If

                            If Not stationChannelsSBS.Contains(tmpChan.AdEdgeNames) Then
                                stationChannelsSBS = "Kanal 5; Kanal 9; Kanal11; Discovery se; TLC; Investigation Discovery; Eurosport SE; Eurosport 2 SE;"
                            End If
                            .Cells(rowSBS, 3).Value = stationChannelsSBS
                            If TrinitySettings.Developer Then
                                .Cells(rowSBS, 13).Value += tmpChan.ChannelName + " " + tmpBook.Name + " "
                            End If
                            If Not tmpBook.IsSpecific Then
                                Dim tmpCTC As Integer = 0
                                Dim tmptotalPlannedGrossBudgetSBS = 0
                                If tmpBook.ConfirmedGrossBudget = 0 Then
                                    tmptotalPlannedGrossBudgetSBS = tmpBook.PlannedGrossBudget
                                Else
                                    tmptotalPlannedGrossBudgetSBS = tmpBook.ConfirmedGrossBudget
                                End If
                                Dim tmptotalPlannedNetBudgetSBS = 0
                                If tmpBook.ConfirmedNetBudget = 0 Then
                                    tmptotalPlannedNetBudgetSBS = tmpBook.PlannedNetBudget
                                Else
                                    tmptotalPlannedNetBudgetSBS = tmpBook.ConfirmedNetBudget
                                End If
                                totalPlannedGrossBudgetSBS += tmptotalPlannedGrossBudgetSBS
                                totalPlannedNetBudgetSBS += tmptotalPlannedNetBudgetSBS

                                tmpCTC = tmptotalPlannedNetBudgetSBS * tmpChan.AgencyCommission
                                tmpCTC = tmptotalPlannedNetBudgetSBS - tmpCTC
                                .Cells(rowSBS, 10).Value += Math.Round(tmpCTC) 'CTC

                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    totalTRPForBTSBS += tmpWeek.TRP
                                Next
                                .Cells(rowSBS, 11).Value = Math.Round(totalTRPForBTSBS, 1)
                            Else
                                Dim tmptotalPlannedGrossBudgetSBS = 0
                                Dim tmptotalPlannedNetBudgetSBS = 0
                                For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpSpot.Bookingtype Is tmpBook Then
                                        listOfSBSSpots.Add(tmpSpot)
                                    End If
                                Next
                                Dim tmpCTC As Integer = 0
                                For Each spot As Trinity.cBookedSpot In listOfSBSSpots
                                    If spot.MyEstimate <> 0 Then
                                        totalTRPForBTCMORE += spot.MyEstimate
                                    Else
                                        totalTRPForBTCMORE += spot.ChannelEstimate
                                    End If
                                    If spot.AddedValues.Count > 0 Then
                                        tmptotalPlannedGrossBudgetSBS += spot.GrossPrice * spot.AddedValueIndex
                                        tmptotalPlannedNetBudgetSBS += spot.NetPrice * spot.AddedValueIndex
                                    Else
                                        tmptotalPlannedGrossBudgetSBS += spot.GrossPrice
                                        tmptotalPlannedNetBudgetSBS += spot.NetPrice
                                    End If
                                Next
                                tmpCTC = totalPlannedNetBudgetSBS * tmpChan.AgencyCommission
                                tmpCTC = totalPlannedNetBudgetSBS - tmpCTC
                                .Cells(rowSBS, 10).Value = Math.Round(tmpCTC) 'CTC
                                .Cells(rowSBS, 11).Value = Math.Round(totalTRPForBTSBS, 1)
                                totalPlannedGrossBudgetSBS = tmptotalPlannedGrossBudgetSBS
                                totalPlannedNetBudgetSBS = tmptotalPlannedNetBudgetSBS
                            End If

                            .Cells(rowSBS, 4).Value = Math.Round(totalPlannedGrossBudgetSBS) 'Gross budget
                            .Cells(rowSBS, 5).Value = Math.Round(totalPlannedNetBudgetSBS) 'Net budget
                            .Cells(rowSBS, 6).Value = Math.Round(totalPlannedNetBudgetSBS * (1 - tmpChan.AgencyCommission)) 'Net net
                            .Cells(rowSBS, 7).Value = Math.Round(totalPlannedNetBudgetSBS * tmpChan.AgencyCommission) 'Fees

                            .Cells(rowSBS, 8).Value = "" 'Other
                            .Cells(rowSBS, 9).Value = "" 'Fond

                            If Not targetTSBS.Contains(translateTargetName(tmpBook.BuyingTarget.ToString())) Then
                                targetTSBS = targetTSBS & translateTargetName(tmpBook.BuyingTarget.ToString) & ";"
                            End If
                            .Cells(rowSBS, 12).Value = targetTSBS
                        End If
                    End If
                    'Fox
                    If Not tmpBundleFOX Then
                        If tmpBook.ToString().Contains("FOX") Or tmpBook.ToString().Contains("National") Then
                            If tmpBook.BookIt Then

                                If rowFOX = 0 Then
                                    rowFOX = tmpRow
                                    tmpRow = tmpRow + 1
                                    .Range("B" & rowFOX & ":C" & rowFOX).Interior.Color = RGB(0, 137, 178)
                                    .Range("B" & rowFOX & ":C" & rowFOX).Font.Color = RGB(255, 255, 255)
                                End If
                                'printBookingTable1(tmpChan, tmpBook, tmpRow, False, "FOX", rowFOX)


                                If .Cells(rowFOX, 2).Value = "" Then
                                    .Cells(rowFOX, 2).Value = "Fox International"
                                End If

                                If Not stationChannelsFOX.Contains(tmpChan.AdEdgeNames) Then
                                    stationChannelsFOX = stationChannelsFOX & " " & tmpChan.AdEdgeNames & ";"
                                End If
                                .Cells(rowFOX, 3).Value = stationChannelsFOX

                                If Not tmpBook.IsSpecific Then
                                    Dim tmpCTC As Integer = 0
                                    Dim tmptotalPlannedGrossBudgetFox = 0
                                    If tmpBook.ConfirmedGrossBudget = 0 Then
                                        tmptotalPlannedGrossBudgetFox = tmpBook.PlannedGrossBudget
                                    Else
                                        tmptotalPlannedGrossBudgetFox = tmpBook.ConfirmedGrossBudget
                                    End If
                                    Dim tmptotalPlannedNetBudgetFox = 0
                                    If tmpBook.ConfirmedNetBudget = 0 Then
                                        tmptotalPlannedNetBudgetFox = tmpBook.PlannedNetBudget
                                    Else
                                        tmptotalPlannedNetBudgetFox = tmpBook.ConfirmedNetBudget
                                    End If
                                    totalPlannedGrossBudgetFOX += tmptotalPlannedGrossBudgetFox
                                    totalPlannedNetBudgetFOX += tmptotalPlannedNetBudgetFox

                                    tmpCTC = totalPlannedNetBudgetFOX * tmpChan.AgencyCommission
                                    tmpCTC = totalPlannedNetBudgetFOX - tmpCTC
                                    .Cells(rowFOX, 10).Value += Math.Round(tmpCTC) 'CTC

                                    For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                        totalTRPForBTFOX += tmpWeek.TRP
                                    Next
                                    .Cells(rowFOX, 11).Value = Math.Round(totalTRPForBTFOX, 1)
                                Else
                                    Dim tmptotalPlannedGrossBudgetFox = 0
                                    Dim tmptotalPlannedNetBudgetFox = 0
                                    For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                        If tmpSpot.Bookingtype Is tmpBook Then
                                            listOfFoxSpots.Add(tmpSpot)
                                        End If
                                    Next
                                    Dim tmpCTC As Integer = 0
                                    For Each spot As Trinity.cBookedSpot In listOfFoxSpots
                                        If spot.MyEstimate <> 0 Then
                                            totalTRPForBTCMORE += spot.MyEstimate
                                        Else
                                            totalTRPForBTCMORE += spot.ChannelEstimate
                                        End If
                                        If spot.AddedValues.Count > 0 Then
                                            tmptotalPlannedGrossBudgetFox += spot.GrossPrice * spot.AddedValueIndex
                                            tmptotalPlannedNetBudgetFox += spot.NetPrice * spot.AddedValueIndex
                                        Else
                                            tmptotalPlannedGrossBudgetFox += spot.GrossPrice
                                            tmptotalPlannedNetBudgetFox += spot.NetPrice
                                        End If
                                    Next
                                    tmpCTC = totalPlannedNetBudgetFOX * tmpChan.AgencyCommission
                                    tmpCTC = totalPlannedNetBudgetFOX - tmpCTC
                                    .Cells(rowFOX, 10).Value = Math.Round(tmpCTC) 'CTC
                                    .Cells(rowFOX, 11).Value = Math.Round(totalTRPForBTFOX, 1)
                                    totalPlannedGrossBudgetFOX = tmptotalPlannedGrossBudgetFox
                                    totalPlannedNetBudgetFOX = tmptotalPlannedNetBudgetFox
                                End If

                                .Cells(rowFOX, 4).Value = Math.Round(totalPlannedGrossBudgetFOX) 'Gross budget
                                .Cells(rowFOX, 5).Value = Math.Round(totalPlannedNetBudgetFOX) 'Net budget
                                .Cells(rowFOX, 6).Value = Math.Round(totalPlannedNetBudgetFOX * (1 - tmpChan.AgencyCommission)) 'Net net
                                .Cells(rowFOX, 7).Value = Math.Round(totalPlannedNetBudgetFOX * tmpChan.AgencyCommission) 'Fees

                                .Cells(rowFOX, 8).Value = "" 'Other
                                .Cells(rowFOX, 9).Value = "" 'Fond<

                                If Not targetTFOX.Contains(translateTargetName(tmpBook.BuyingTarget.ToString())) Then
                                    targetTFOX = targetTFOX & translateTargetName(tmpBook.BuyingTarget.ToString) & ";"
                                End If
                                .Cells(rowFOX, 12).Value = targetTFOX

                            End If
                        End If
                    End If
                    'Cmore
                    If tmpBook.ToString().Contains("CMore") Or tmpBook.ToString().Contains("C More") Then


                        If rowFOX = 0 Then
                            rowFOX = tmpRow
                        End If
                        'printBookingTable1(tmpChan, tmpBook, tmpRow, False, "FOX", rowFOX)

                        If rowCMORE = 0 Then
                            rowCMORE = tmpRow
                            tmpRow = tmpRow + 1

                            .Range("B" & rowCMORE & ":C" & rowCMORE).Interior.Color = RGB(0, 137, 178)
                            .Range("B" & rowCMORE & ":C" & rowCMORE).Font.Color = RGB(255, 255, 255)
                        End If

                        If .Cells(rowCMORE, 2).Value = "" Then
                            .Cells(rowCMORE, 2).Value = "CMORE"
                        End If

                        If Not stationChannelsCMORE.Contains(tmpChan.AdEdgeNames) Then
                            stationChannelsCMORE = stationChannelsCMORE & " " & tmpChan.AdEdgeNames & ";"
                        End If
                        .Cells(rowCMORE, 3).Value = stationChannelsCMORE

                        If Not tmpBook.IsSpecific Then
                            Dim tmpCTC As Integer = 0
                            Dim tmptotalPlannedGrossBudgetCMORE = 0
                            If tmpBook.ConfirmedGrossBudget = 0 Then
                                tmptotalPlannedGrossBudgetCMORE = tmpBook.PlannedGrossBudget
                            Else
                                tmptotalPlannedGrossBudgetCMORE = tmpBook.ConfirmedGrossBudget
                            End If
                            Dim tmptotalPlannedNetBudgetCMORE = 0
                            If tmpBook.ConfirmedNetBudget = 0 Then
                                tmptotalPlannedNetBudgetCMORE = tmpBook.PlannedNetBudget
                            Else
                                tmptotalPlannedNetBudgetCMORE = tmpBook.ConfirmedNetBudget
                            End If
                            totalPlannedGrossBudgetCMORE += tmptotalPlannedGrossBudgetCMORE
                            totalPlannedNetBudgetCMORE += tmptotalPlannedNetBudgetCMORE

                            tmpCTC = totalPlannedNetBudgetCMORE * tmpChan.AgencyCommission
                            tmpCTC = totalPlannedNetBudgetCMORE - tmpCTC
                            .Cells(rowCMORE, 10).Value += Math.Round(tmpCTC) 'CTC

                            For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                totalTRPForBTCMORE += tmpWeek.TRP
                            Next
                            .Cells(rowCMORE, 11).Value = Math.Round(totalTRPForBTCMORE, 1)
                        Else
                            Dim tmptotalPlannedGrossBudgetCMORE = 0
                            Dim tmptotalPlannedNetBudgetCMORE = 0
                            For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                If tmpSpot.Bookingtype Is tmpBook Then
                                    listOfCMORESpots.Add(tmpSpot)
                                End If
                            Next
                            Dim tmpCTC As Integer = 0
                            For Each spot As Trinity.cBookedSpot In listOfCMORESpots
                                If spot.MyEstimate <> 0 Then
                                    totalTRPForBTCMORE += spot.MyEstimate
                                Else
                                    totalTRPForBTCMORE += spot.ChannelEstimate
                                End If
                                If spot.AddedValues.Count > 0 Then
                                    tmptotalPlannedGrossBudgetCMORE += spot.GrossPrice * spot.AddedValueIndex
                                    tmptotalPlannedNetBudgetCMORE += spot.NetPrice * spot.AddedValueIndex
                                Else
                                    tmptotalPlannedGrossBudgetCMORE += spot.GrossPrice
                                    tmptotalPlannedNetBudgetCMORE += spot.NetPrice
                                End If
                            Next
                            tmpCTC = totalPlannedNetBudgetCMORE * tmpChan.AgencyCommission
                            tmpCTC = totalPlannedNetBudgetCMORE - tmpCTC
                            .Cells(rowCMORE, 10).Value = Math.Round(tmpCTC) 'CTC
                            .Cells(rowCMORE, 11).Value = Math.Round(totalTRPForBTCMORE, 1)
                            totalPlannedGrossBudgetCMORE = tmptotalPlannedGrossBudgetCMORE
                            totalPlannedNetBudgetCMORE = tmptotalPlannedNetBudgetCMORE
                        End If

                        .Cells(rowCMORE, 4).Value = Math.Round(totalPlannedGrossBudgetCMORE) 'Gross budget
                        .Cells(rowCMORE, 5).Value = Math.Round(totalPlannedNetBudgetCMORE) 'Net budget
                        .Cells(rowCMORE, 6).Value = Math.Round(totalPlannedNetBudgetCMORE * (1 - tmpChan.AgencyCommission)) 'Net net
                        .Cells(rowCMORE, 7).Value = Math.Round(totalPlannedNetBudgetCMORE * tmpChan.AgencyCommission) 'Fees

                        .Cells(rowCMORE, 8).Value = "" 'Other
                        .Cells(rowCMORE, 9).Value = "" 'Fond

                        If Not targetTCMORE.Contains(translateTargetName(tmpBook.BuyingTarget.ToString())) Then
                            targetTCMORE = targetTCMORE & translateTargetName(tmpBook.BuyingTarget.ToString) & ";"
                        End If
                        .Cells(rowCMORE, 12).Value = targetTCMORE
                    End If
                Next
            Next
            lastRowComboTable1 = tmpRow + 1
        End With
        _sheet.Range("B" & 39 & ":M" & 48).Numberformat = "0.0"
    End Sub

    Sub printTable2()
        Dim filmColumnArray As Array = {":A", ":B", ":C", ":D", ":E", ":F", ":G", ":H", ":I", ":J", ":K", ":L", ":M", ":N", ":O", ":P", ":Q"}
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
                            .Cells(currentRow, column).Value = "2015w" & tmpWeek.Name
                            column = column + 1
                        Next
                        Exit For
                    End If
                Next
            Next
            currentRow = currentRow + 1

            Dim rowTV4 = 0
            Dim rowMTG = 0
            Dim rowSBS = 0
            Dim rowFOX = 0
            Dim rowCMORE = 0

            Dim totalTRPForBTTV4 = 0
            Dim totalTRPForBTMTG = 0
            Dim totalTRPForBTSBS = 0
            Dim totalTRPForBTFOX = 0
            Dim totalTRPForBTCMORE = 0

            For Each tmpChan As Trinity.cChannel In Campaign.Channels
                For Each tmpBook As Trinity.cBookingType In tmpChan.BookingTypes
                    If tmpBook.BookIt Then
                        If tmpBook.ToString().Contains("TV4") Or tmpBook.ToString().Contains("Sjuan") Or tmpBook.ToString().Contains("TV12") Then
                            If tmpChan.ChannelName = "TV4" Then
                                If rowTV4 = 0 Then
                                    rowTV4 = currentRow
                                    .Range("B" & rowTV4 & ":B" & rowTV4).Interior.Color = RGB(0, 137, 178)
                                    .Range("B" & rowTV4 & ":B" & rowTV4).Font.Color = RGB(255, 255, 255)
                                    currentRow = currentRow + 1
                                End If
                                currentRow = rowTV4
                            Else
                                .Range("B" & currentRow & ":B" & currentRow).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & currentRow & ":B" & currentRow).Font.Color = RGB(255, 255, 255)
                            End If

                            If .Cells(currentRow, 2).Value = "" Then
                                .Cells(currentRow, 2).Value = tmpChan.AdEdgeNames
                            End If

                            If tmpBook.IsSpecific Then
                                Dim listOfSpotsForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpSpot.Bookingtype Is tmpBook Then
                                        listOfSpotsForChannel.Add(tmpSpot)
                                    End If
                                Next

                                Dim listofSpotts As New List(Of Trinity.cBookedSpot)
                                Dim trpForWeek As Decimal = 0
                                Dim column As Integer = 3
                                Dim currentItem As Integer = 1
                                Dim amountOfSpott As Integer = listOfSpotsForChannel.Count()

                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    Dim spotListForWeek As New List(Of Trinity.cBookedSpot)
                                    For Each tmpSpot As Trinity.cBookedSpot In listOfSpotsForChannel
                                        If tmpSpot.week.Name = tmpWeek.Name Then
                                            spotListForWeek.Add(tmpSpot)
                                        End If
                                    Next
                                    Dim currentSpotNr As Integer = 1
                                    For Each tmpBookedSpot As Trinity.cBookedSpot In spotListForWeek
                                        If spotListForWeek.Count >= currentSpotNr Then
                                            If tmpBookedSpot.MyEstimate <> 0 Then
                                                trpForWeek += tmpBookedSpot.MyEstimate
                                            Else
                                                trpForWeek += tmpBookedSpot.ChannelEstimate
                                            End If
                                            currentSpotNr += 1
                                        Else
                                            .Cells(currentRow, column).Value = Math.Round(trpForWeek, 1)
                                            trpForWeek = 0
                                            trpForWeek += tmpBookedSpot.MyEstimate
                                            column = column + 1
                                        End If
                                    Next
                                    .Cells(currentRow, column).Value += Math.Round(trpForWeek, 1)
                                    trpForWeek = 0
                                    column = column + 1
                                Next
                            Else
                                Dim column = 3
                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    .Cells(currentRow, column).Value += Math.Round(tmpWeek.TRP, 1)
                                    column = column + 1
                                Next
                            End If
                            currentRow = currentRow + 1
                        End If
                        If tmpBook.ToString().Contains("TV3") Or tmpBook.ToString().Contains("TV6") Or tmpBook.ToString().Contains("MTV") Or tmpBook.ToString().Contains("TV8") Or tmpBook.ToString().Contains("TV10") Or tmpBook.ToString().Contains("Comedy central") Or tmpBook.ToString().Contains("Comedy") Or tmpBook.ToString().Contains("Nickelodeon") Then
                            If rowMTG = 0 Then
                                rowMTG = currentRow
                                currentRow = currentRow + 1
                                .Range("B" & rowMTG & ":B" & rowMTG).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & rowMTG & ":B" & rowMTG).Font.Color = RGB(255, 255, 255)
                            End If

                            If .Cells(rowMTG, 2).Value = "" Then
                                .Cells(rowMTG, 2).Value = "MTG TV"
                            End If

                            If tmpBook.IsSpecific Then
                                Dim listOfSpotsForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpSpot.Bookingtype Is tmpBook Then
                                        listOfSpotsForChannel.Add(tmpSpot)
                                    End If
                                Next

                                Dim listofSpotts As New List(Of Trinity.cBookedSpot)
                                Dim trpForWeek As Decimal = 0
                                Dim column As Integer = 3
                                Dim currentItem As Integer = 1
                                Dim amountOfSpott As Integer = listOfSpotsForChannel.Count()

                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    Dim spotListForWeek As New List(Of Trinity.cBookedSpot)
                                    For Each tmpSpot As Trinity.cBookedSpot In listOfSpotsForChannel
                                        If tmpSpot.week.Name = tmpWeek.Name Then
                                            spotListForWeek.Add(tmpSpot)
                                        End If
                                    Next
                                    Dim currentSpotNr As Integer = 1
                                    For Each tmpBookedSpot As Trinity.cBookedSpot In spotListForWeek
                                        If spotListForWeek.Count >= currentSpotNr Then
                                            trpForWeek += tmpBookedSpot.MyEstimate
                                            currentSpotNr += 1
                                        Else
                                            .Cells(rowMTG, column).Value = Math.Round(trpForWeek, 1)
                                            trpForWeek = 0
                                            trpForWeek += tmpBookedSpot.MyEstimate
                                            column = column + 1
                                        End If
                                    Next
                                    .Cells(rowMTG, column).Value += Math.Round(trpForWeek, 1)
                                    trpForWeek = 0
                                    column = column + 1
                                Next
                            Else
                                Dim column = 3
                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    .Cells(rowMTG, column).Value += Math.Round(tmpWeek.TRP, 1)
                                    column = column + 1
                                Next
                            End If
                        End If
                        If tmpBook.ToString().Contains("Kanal5") Or tmpBook.ToString().Contains("Kanal9") Or tmpBook.ToString().Contains("Kanal 11") Or tmpBook.ToString().Contains("Kanal11") Or tmpBook.ToString().Contains("TV11") Or tmpBook.ToString().Contains("Eurosport") Or tmpBook.ToString().Contains("Discovery") Or tmpBook.ToString().Contains("TLC") Or tmpBook.ToString().Contains("ID") Or tmpBook.ToString().Contains("Investigation Discovery") Then
                            If rowSBS = 0 Then
                                rowSBS = currentRow
                                currentRow = currentRow + 1
                                .Range("B" & rowSBS & ":B" & rowSBS).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & rowSBS & ":B" & rowSBS).Font.Color = RGB(255, 255, 255)
                            End If
                            If .Cells(rowSBS, 2).Value = "" Then
                                .Cells(rowSBS, 2).Value = "SBS Discovery"
                            End If

                            If tmpBook.IsSpecific Then

                                Dim listOfSpotsForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpSpot.Bookingtype Is tmpBook Then
                                        listOfSpotsForChannel.Add(tmpSpot)
                                    End If
                                Next

                                Dim listofSpotts As New List(Of Trinity.cBookedSpot)
                                Dim trpForWeek As Decimal = 0
                                Dim column As Integer = 3
                                Dim currentItem As Integer = 1
                                Dim amountOfSpott As Integer = listOfSpotsForChannel.Count()

                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    Dim spotListForWeek As New List(Of Trinity.cBookedSpot)
                                    For Each tmpSpot As Trinity.cBookedSpot In listOfSpotsForChannel
                                        If tmpSpot.week.Name = tmpWeek.Name Then
                                            spotListForWeek.Add(tmpSpot)
                                        End If
                                    Next
                                    Dim currentSpotNr As Integer = 1
                                    For Each tmpBookedSpot As Trinity.cBookedSpot In spotListForWeek
                                        If spotListForWeek.Count >= currentSpotNr Then
                                            trpForWeek += tmpBookedSpot.MyEstimate
                                            currentSpotNr += 1
                                        Else
                                            .Cells(rowSBS, column).Value = Math.Round(trpForWeek, 1)
                                            trpForWeek = 0
                                            trpForWeek += tmpBookedSpot.MyEstimate
                                            column = column + 1
                                        End If
                                    Next
                                    .Cells(rowSBS, column).Value += Math.Round(trpForWeek, 1)
                                    trpForWeek = 0
                                    column = column + 1
                                Next
                            Else
                                Dim column = 3
                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    .Cells(rowSBS, column).Value += Math.Round(tmpWeek.TRP, 1)
                                    column = column + 1
                                Next
                            End If
                        End If
                        If tmpBook.ToString().Contains("FOX") Or tmpBook.ToString().Contains("National") Then
                            If rowFOX = 0 Then
                                rowFOX = currentRow
                                currentRow = currentRow + 1
                                .Range("B" & rowFOX & ":B" & rowFOX).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & rowFOX & ":B" & rowFOX).Font.Color = RGB(255, 255, 255)
                            End If
                            If .Cells(rowFOX, 2).Value = "" Then
                                .Cells(rowFOX, 2).Value = "FOX International"
                            End If

                            If tmpBook.IsSpecific Then

                                Dim listOfSpotsForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpSpot.Bookingtype Is tmpBook Then
                                        listOfSpotsForChannel.Add(tmpSpot)
                                    End If
                                Next

                                Dim listofSpotts As New List(Of Trinity.cBookedSpot)
                                Dim trpForWeek As Decimal = 0
                                Dim column As Integer = 3
                                Dim currentItem As Integer = 1
                                Dim amountOfSpott As Integer = listOfSpotsForChannel.Count()

                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    Dim spotListForWeek As New List(Of Trinity.cBookedSpot)
                                    For Each tmpSpot As Trinity.cBookedSpot In listOfSpotsForChannel
                                        If tmpSpot.week.Name = tmpWeek.Name Then
                                            spotListForWeek.Add(tmpSpot)
                                        End If
                                    Next
                                    Dim currentSpotNr As Integer = 1
                                    For Each tmpBookedSpot As Trinity.cBookedSpot In spotListForWeek
                                        If spotListForWeek.Count >= currentSpotNr Then
                                            trpForWeek += tmpBookedSpot.MyEstimate
                                            currentSpotNr += 1
                                        Else
                                            .Cells(rowFOX, column).Value = Math.Round(trpForWeek, 1)
                                            trpForWeek = 0
                                            trpForWeek += tmpBookedSpot.MyEstimate
                                            column = column + 1
                                        End If
                                    Next
                                    .Cells(rowFOX, column).Value += Math.Round(trpForWeek, 1)
                                    trpForWeek = 0
                                    column = column + 1
                                Next
                            Else
                                Dim column = 3
                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    .Cells(rowFOX, column).Value += Math.Round(tmpWeek.TRP, 1)
                                    column = column + 1
                                Next
                            End If
                        End If
                        If tmpBook.ToString().Contains("CMORE") Or tmpBook.ToString().Contains("C More") Then
                            If rowCMORE = 0 Then
                                rowCMORE = currentRow
                                currentRow = currentRow + 1
                                .Range("B" & rowCMORE & ":B" & rowCMORE).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & rowCMORE & ":B" & rowCMORE).Font.Color = RGB(255, 255, 255)
                            End If
                            If .Cells(rowCMORE, 2).Value = "" Then
                                .Cells(rowCMORE, 2).Value = "CMORE"
                            End If

                            If tmpBook.IsSpecific Then

                                Dim listOfSpotsForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpSpot.Bookingtype Is tmpBook Then
                                        listOfSpotsForChannel.Add(tmpSpot)
                                    End If
                                Next

                                Dim listofSpotts As New List(Of Trinity.cBookedSpot)
                                Dim trpForWeek As Decimal = 0
                                Dim column As Integer = 3
                                Dim currentItem As Integer = 1
                                Dim amountOfSpott As Integer = listOfSpotsForChannel.Count()

                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    Dim spotListForWeek As New List(Of Trinity.cBookedSpot)
                                    For Each tmpSpot As Trinity.cBookedSpot In listOfSpotsForChannel
                                        If tmpSpot.week.Name = tmpWeek.Name Then
                                            spotListForWeek.Add(tmpSpot)
                                        End If
                                    Next
                                    Dim currentSpotNr As Integer = 1
                                    For Each tmpBookedSpot As Trinity.cBookedSpot In spotListForWeek
                                        If spotListForWeek.Count >= currentSpotNr Then
                                            trpForWeek += tmpBookedSpot.MyEstimate
                                            currentSpotNr += 1
                                        Else
                                            .Cells(rowCMORE, column).Value = Math.Round(trpForWeek, 1)
                                            trpForWeek = 0
                                            trpForWeek += tmpBookedSpot.MyEstimate
                                            column = column + 1
                                        End If
                                    Next
                                    .Cells(rowCMORE, column).Value += Math.Round(trpForWeek, 1)
                                    trpForWeek = 0
                                    column = column + 1
                                Next
                            Else
                                Dim column = 3
                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    .Cells(rowCMORE, column).Value += Math.Round(tmpWeek.TRP, 1)
                                    column = column + 1
                                Next
                            End If
                        End If
                    End If
                Next
            Next
        End With
        _sheet.Range("B" & lastRowComboTable1 - 1 & ":M" & currentRow).Numberformat = "0.0"
        lastRowComboTable2 = currentRow + 1
    End Sub
    Sub printTable3()
        Dim filmColumnArray As Array = {":A", ":B", ":C", ":D", ":E", ":F", ":G", ":H", ":I", ":J", ":K", ":L", ":M", ":N", ":O", ":P", ":Q"}
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
                            .Cells(currentRow, column).Value = "2015w" & tmpWeek.Name
                            column = column + 1
                        Next
                        Exit For
                    End If
                Next
            Next
            currentRow = currentRow + 1


            Dim rowTV4 = 0
            Dim rowMTG = 0
            Dim rowSBS = 0
            Dim rowFOX = 0
            Dim rowCMORE = 0

            Dim totalTRPForBTTV4 = 0
            Dim totalTRPForBTMTG = 0
            Dim totalTRPForBTSBS = 0
            Dim totalTRPForBTFOX = 0
            Dim totalTRPForBTCMORE = 0

            For Each tmpChan As Trinity.cChannel In Campaign.Channels
                For Each tmpBook As Trinity.cBookingType In tmpChan.BookingTypes
                    If tmpBook.BookIt Then
                        If tmpBook.ToString().Contains("TV4") Or tmpBook.ToString().Contains("Sjuan") Or tmpBook.ToString().Contains("TV12") Then
                            If tmpChan.ChannelName = "TV4" Then
                                If rowTV4 = 0 Then
                                    rowTV4 = currentRow
                                    .Range("B" & rowTV4 & ":B" & rowTV4).Interior.Color = RGB(0, 137, 178)
                                    .Range("B" & rowTV4 & ":B" & rowTV4).Font.Color = RGB(255, 255, 255)
                                    currentRow = currentRow + 1
                                End If
                                currentRow = rowTV4
                            Else
                                .Range("B" & currentRow & ":B" & currentRow).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & currentRow & ":B" & currentRow).Font.Color = RGB(255, 255, 255)
                            End If

                            If .Cells(currentRow, 2).Value = "" Then
                                .Cells(currentRow, 2).Value = tmpChan.AdEdgeNames
                            End If

                            If tmpBook.IsSpecific Then
                                Dim listOfSpotsForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpSpot.Bookingtype Is tmpBook Then
                                        listOfSpotsForChannel.Add(tmpSpot)
                                    End If
                                Next

                                Dim listofSpotts As New List(Of Trinity.cBookedSpot)
                                Dim trpForWeek As Decimal = 0
                                Dim column As Integer = 3
                                Dim currentItem As Integer = 1
                                Dim amountOfSpott As Integer = listOfSpotsForChannel.Count()

                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    Dim spotListForWeek As New List(Of Trinity.cBookedSpot)
                                    For Each tmpSpot As Trinity.cBookedSpot In listOfSpotsForChannel
                                        If tmpSpot.week.Name = tmpWeek.Name Then
                                            spotListForWeek.Add(tmpSpot)
                                        End If
                                    Next
                                    Dim currentSpotNr As Integer = 1
                                    For Each tmpBookedSpot As Trinity.cBookedSpot In spotListForWeek
                                        If spotListForWeek.Count >= currentSpotNr Then
                                            If tmpBookedSpot.MyEstimateBuyTarget <> 0 Then
                                                trpForWeek += tmpBookedSpot.MyEstimateBuyTarget
                                            Else
                                                trpForWeek += tmpBookedSpot.ChannelEstimate
                                            End If
                                            currentSpotNr += 1
                                        Else
                                            .Cells(currentRow, column).Value = Math.Round(trpForWeek, 1)
                                            trpForWeek = 0
                                            If tmpBookedSpot.MyEstimate <> 0 Then
                                                trpForWeek += tmpBookedSpot.MyEstimateBuyTarget
                                            Else
                                                trpForWeek += tmpBookedSpot.ChannelEstimate
                                            End If
                                            column = column + 1
                                        End If
                                    Next
                                    .Cells(currentRow, column).Value += Math.Round(trpForWeek, 1)
                                    trpForWeek = 0
                                    column = column + 1
                                Next
                            Else
                                Dim column = 3
                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    .Cells(currentRow, column).Value += Math.Round(tmpWeek.TRPBuyingTarget, 1)
                                    column = column + 1
                                Next
                            End If
                            currentRow = currentRow + 1
                        End If
                        If tmpBook.ToString().Contains("TV3") Or tmpBook.ToString().Contains("TV6") Or tmpBook.ToString().Contains("MTV") Or tmpBook.ToString().Contains("TV8") Or tmpBook.ToString().Contains("TV10") Or tmpBook.ToString().Contains("Comedy central") Or tmpBook.ToString().Contains("Comedy") Or tmpBook.ToString().Contains("Comedy") Or tmpBook.ToString().Contains("Nickelodeon") Then
                            If rowMTG = 0 Then
                                rowMTG = currentRow
                                currentRow = currentRow + 1
                                .Range("B" & rowMTG & ":B" & rowMTG).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & rowMTG & ":B" & rowMTG).Font.Color = RGB(255, 255, 255)
                            End If

                            If .Cells(rowMTG, 2).Value = "" Then
                                .Cells(rowMTG, 2).Value = "MTG TV"
                            End If

                            If tmpBook.IsSpecific Then
                                Dim listOfSpotsForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpSpot.Bookingtype Is tmpBook Then
                                        listOfSpotsForChannel.Add(tmpSpot)
                                    End If
                                Next

                                Dim listofSpotts As New List(Of Trinity.cBookedSpot)
                                Dim trpForWeek As Decimal = 0
                                Dim column As Integer = 3
                                Dim currentItem As Integer = 1
                                Dim amountOfSpott As Integer = listOfSpotsForChannel.Count()

                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    Dim spotListForWeek As New List(Of Trinity.cBookedSpot)
                                    For Each tmpSpot As Trinity.cBookedSpot In listOfSpotsForChannel
                                        If tmpSpot.week.Name = tmpWeek.Name Then
                                            spotListForWeek.Add(tmpSpot)
                                        End If
                                    Next
                                    Dim currentSpotNr As Integer = 1
                                    For Each tmpBookedSpot As Trinity.cBookedSpot In spotListForWeek
                                        If spotListForWeek.Count >= currentSpotNr Then
                                            If tmpBookedSpot.MyEstimateBuyTarget <> 0 Then
                                                trpForWeek += tmpBookedSpot.MyEstimateBuyTarget
                                            Else
                                                trpForWeek += tmpBookedSpot.ChannelEstimate
                                            End If
                                            currentSpotNr += 1
                                        Else
                                            .Cells(rowMTG, column).Value = Math.Round(trpForWeek, 1)
                                            trpForWeek = 0
                                            If tmpBookedSpot.MyEstimate <> 0 Then
                                                trpForWeek += tmpBookedSpot.MyEstimateBuyTarget
                                            Else
                                                trpForWeek += tmpBookedSpot.ChannelEstimate
                                            End If
                                            column = column + 1
                                        End If
                                    Next
                                    .Cells(rowMTG, column).Value += Math.Round(trpForWeek, 1)
                                    trpForWeek = 0
                                    column = column + 1
                                Next
                            Else
                                Dim column = 3
                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    .Cells(rowMTG, column).Value += Math.Round(tmpWeek.TRPBuyingTarget, 1)
                                    column = column + 1
                                Next
                            End If
                        End If
                        If tmpBook.ToString().Contains("Kanal5") Or tmpBook.ToString().Contains("Kanal9") Or tmpBook.ToString().Contains("Kanal 11") Or tmpBook.ToString().Contains("TV11") Or tmpBook.ToString().Contains("Eurosport") Or tmpBook.ToString().Contains("Discovery") Or tmpBook.ToString().Contains("TLC") Or tmpBook.ToString().Contains("ID") Or tmpBook.ToString().Contains("Investigation Discovery") Then
                            If rowSBS = 0 Then
                                rowSBS = currentRow
                                currentRow = currentRow + 1
                                .Range("B" & rowSBS & ":B" & rowSBS).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & rowSBS & ":B" & rowSBS).Font.Color = RGB(255, 255, 255)
                            End If
                            If .Cells(rowSBS, 2).Value = "" Then
                                .Cells(rowSBS, 2).Value = "SBS Discovery"
                            End If

                            If tmpBook.IsSpecific Then

                                Dim listOfSpotsForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpSpot.Bookingtype Is tmpBook Then
                                        listOfSpotsForChannel.Add(tmpSpot)
                                    End If
                                Next

                                Dim listofSpotts As New List(Of Trinity.cBookedSpot)
                                Dim trpForWeek As Decimal = 0
                                Dim column As Integer = 3
                                Dim currentItem As Integer = 1
                                Dim amountOfSpott As Integer = listOfSpotsForChannel.Count()

                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    Dim spotListForWeek As New List(Of Trinity.cBookedSpot)
                                    For Each tmpSpot As Trinity.cBookedSpot In listOfSpotsForChannel
                                        If tmpSpot.week.Name = tmpWeek.Name Then
                                            spotListForWeek.Add(tmpSpot)
                                        End If
                                    Next
                                    Dim currentSpotNr As Integer = 1
                                    For Each tmpBookedSpot As Trinity.cBookedSpot In spotListForWeek
                                        If spotListForWeek.Count >= currentSpotNr Then
                                            If tmpBookedSpot.MyEstimateBuyTarget <> 0 Then
                                                trpForWeek += tmpBookedSpot.MyEstimateBuyTarget
                                            Else
                                                trpForWeek += tmpBookedSpot.ChannelEstimate
                                            End If
                                            currentSpotNr += 1
                                        Else
                                            .Cells(rowSBS, column).Value = Math.Round(trpForWeek, 1)
                                            trpForWeek = 0
                                            If tmpBookedSpot.MyEstimate <> 0 Then
                                                trpForWeek += tmpBookedSpot.MyEstimateBuyTarget
                                            Else
                                                trpForWeek += tmpBookedSpot.ChannelEstimate
                                            End If
                                            column = column + 1
                                        End If
                                    Next
                                    .Cells(rowSBS, column).Value += Math.Round(trpForWeek, 1)
                                    trpForWeek = 0
                                    column = column + 1
                                Next
                            Else
                                Dim column = 3
                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    .Cells(rowSBS, column).Value += Math.Round(tmpWeek.TRPBuyingTarget, 1)
                                    column = column + 1
                                Next
                            End If
                        End If

                        If tmpBook.ToString().Contains("FOX") Or tmpBook.ToString().Contains("National") Then
                            If rowFOX = 0 Then
                                rowFOX = currentRow
                                currentRow = currentRow + 1
                                .Range("B" & rowFOX & ":B" & rowFOX).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & rowFOX & ":B" & rowFOX).Font.Color = RGB(255, 255, 255)
                            End If
                            If .Cells(rowFOX, 2).Value = "" Then
                                .Cells(rowFOX, 2).Value = "FOX International"
                            End If

                            If tmpBook.IsSpecific Then

                                Dim listOfSpotsForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpSpot.Bookingtype Is tmpBook Then
                                        listOfSpotsForChannel.Add(tmpSpot)
                                    End If
                                Next

                                Dim listofSpotts As New List(Of Trinity.cBookedSpot)
                                Dim trpForWeek As Decimal = 0
                                Dim column As Integer = 3
                                Dim currentItem As Integer = 1
                                Dim amountOfSpott As Integer = listOfSpotsForChannel.Count()

                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    Dim spotListForWeek As New List(Of Trinity.cBookedSpot)
                                    For Each tmpSpot As Trinity.cBookedSpot In listOfSpotsForChannel
                                        If tmpSpot.week.Name = tmpWeek.Name Then
                                            spotListForWeek.Add(tmpSpot)
                                        End If
                                    Next
                                    Dim currentSpotNr As Integer = 1
                                    For Each tmpBookedSpot As Trinity.cBookedSpot In spotListForWeek
                                        If spotListForWeek.Count >= currentSpotNr Then
                                            If tmpBookedSpot.MyEstimateBuyTarget <> 0 Then
                                                trpForWeek += tmpBookedSpot.MyEstimateBuyTarget
                                            Else
                                                trpForWeek += tmpBookedSpot.ChannelEstimate
                                            End If
                                            currentSpotNr += 1
                                        Else
                                            .Cells(rowFOX, column).Value = Math.Round(trpForWeek, 1)
                                            trpForWeek = 0
                                            If tmpBookedSpot.MyEstimate <> 0 Then
                                                trpForWeek += tmpBookedSpot.MyEstimateBuyTarget
                                            Else
                                                trpForWeek += tmpBookedSpot.ChannelEstimate
                                            End If
                                            column = column + 1
                                        End If
                                    Next
                                    .Cells(rowFOX, column).Value += Math.Round(trpForWeek, 1)
                                    trpForWeek = 0
                                    column = column + 1
                                Next
                            Else
                                Dim column = 3
                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    .Cells(rowFOX, column).Value += Math.Round(tmpWeek.TRPBuyingTarget, 1)
                                    column = column + 1
                                Next
                            End If
                        End If
                        'Cmore
                        If tmpBook.ToString().Contains("CMORE") Or tmpBook.ToString().Contains("C more") Then
                            If rowCMORE = 0 Then
                                rowCMORE = currentRow
                                currentRow = currentRow + 1
                                .Range("B" & rowCMORE & ":B" & rowCMORE).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & rowCMORE & ":B" & rowCMORE).Font.Color = RGB(255, 255, 255)
                            End If
                            If .Cells(rowCMORE, 2).Value = "" Then
                                .Cells(rowCMORE, 2).Value = "CMORE"
                            End If

                            If tmpBook.IsSpecific Then

                                Dim listOfSpotsForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpSpot.Bookingtype Is tmpBook Then
                                        listOfSpotsForChannel.Add(tmpSpot)
                                    End If
                                Next

                                Dim listofSpotts As New List(Of Trinity.cBookedSpot)
                                Dim trpForWeek As Decimal = 0
                                Dim column As Integer = 3
                                Dim currentItem As Integer = 1
                                Dim amountOfSpott As Integer = listOfSpotsForChannel.Count()

                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    Dim spotListForWeek As New List(Of Trinity.cBookedSpot)
                                    For Each tmpSpot As Trinity.cBookedSpot In listOfSpotsForChannel
                                        If tmpSpot.week.Name = tmpWeek.Name Then
                                            spotListForWeek.Add(tmpSpot)
                                        End If
                                    Next
                                    Dim currentSpotNr As Integer = 1
                                    For Each tmpBookedSpot As Trinity.cBookedSpot In spotListForWeek
                                        If spotListForWeek.Count >= currentSpotNr Then
                                            If tmpBookedSpot.MyEstimateBuyTarget <> 0 Then
                                                trpForWeek += tmpBookedSpot.MyEstimateBuyTarget
                                            Else
                                                trpForWeek += tmpBookedSpot.ChannelEstimate
                                            End If
                                            currentSpotNr += 1
                                        Else
                                            .Cells(rowCMORE, column).Value = Math.Round(trpForWeek, 1)
                                            trpForWeek = 0
                                            If tmpBookedSpot.MyEstimate <> 0 Then
                                                trpForWeek += tmpBookedSpot.MyEstimateBuyTarget
                                            Else
                                                trpForWeek += tmpBookedSpot.ChannelEstimate
                                            End If
                                            column = column + 1
                                        End If
                                    Next
                                    .Cells(rowCMORE, column).Value += Math.Round(trpForWeek, 1)
                                    trpForWeek = 0
                                    column = column + 1
                                Next
                            Else
                                Dim column = 3
                                For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                                    .Cells(rowCMORE, column).Value += Math.Round(tmpWeek.TRPBuyingTarget, 1)
                                    column = column + 1
                                Next
                            End If
                        End If

                    End If
                Next
            Next
        End With
        _sheet.Range("B" & lastRowComboTable1 - 1 & ":M" & currentRow).Numberformat = "0.0"

        lastRowComboTable3 = currentRow + 1
    End Sub
    Sub printTable4()
        Dim filmColumnArray As Array = {":B", ":C", ":D", ":E", ":F", ":G", ":H", ":I", ":J", ":K", ":L", ":M", ":N", ":O", ":P", ":Q"}
        Dim columnValue As String = filmColumnArray.GetValue(amountOfFilms)

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

        Dim rowTV4 = 0
        Dim rowMTG = 0
        Dim rowSBS = 0
        Dim rowFOX = 0
        Dim rowCMORE = 0

        With _sheet
            For Each tmpChan As Trinity.cChannel In Campaign.Channels
                For Each tmpBt As Trinity.cBookingType In tmpChan.BookingTypes
                    If tmpBt.BookIt Then
                        If tmpBt.ToString().Contains("TV4") Or tmpBt.ToString().Contains("Sjuan") Or tmpBt.ToString().Contains("TV12") Then
                            Dim column = 3
                            If tmpChan.ChannelName = "TV4" Then
                                If rowTV4 = 0 Then
                                    rowTV4 = currentRow
                                    .Range("B" & rowTV4 & ":B" & rowTV4).Interior.Color = RGB(0, 137, 178)
                                    .Range("B" & rowTV4 & ":B" & rowTV4).Font.Color = RGB(255, 255, 255)
                                    currentRow = currentRow + 1
                                End If
                                currentRow = rowTV4
                            Else
                                .Range("B" & currentRow & ":B" & currentRow).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & currentRow & ":B" & currentRow).Font.Color = RGB(255, 255, 255)
                            End If
                            If .Cells(currentRow, 2).Value = "" Then
                                .Cells(currentRow, 2).Value = tmpChan.AdEdgeNames
                            End If
                            If Not tmpBt.IsSpecific Then
                                Dim tmpFilm = ""
                                Dim tmpTotalShareForFilm As Decimal = 0
                                For i As Integer = 1 To amountOfFilms
                                    Dim totalTRPForBT As Double = 0
                                    Dim trpForFilm As Double = 0
                                    Dim trpForShare As Double = 0
                                    Dim tmpShare As Double = 0

                                    For Each tmpWeek As Trinity.cWeek In tmpBt.Weeks
                                        trpForShare = tmpWeek.TRP
                                        tmpShare = tmpWeek.Films(i).Share
                                        tmpFilm = tmpWeek.Films(i).FilmLength
                                        If trpForShare <> 0 Then
                                            trpForFilm += trpForShare * (tmpShare / 100)
                                        End If
                                    Next
                                    totalTRPForBT += trpForFilm
                                    .Cells(rowHeader, column).Value = tmpFilm
                                    .Cells(currentRow, column).Value += Math.Round(totalTRPForBT, 1)
                                    column = column + 1
                                Next
                            Else
                                Dim trpValueForFilm As Double = 0
                                Dim listOfBookedSpotForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpBookedSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpBookedSpot.Bookingtype Is tmpBt Then
                                        listOfBookedSpotForChannel.Add(tmpBookedSpot)
                                    End If
                                Next
                                For i As Integer = 1 To amountOfFilms
                                    For Each tmpSp As Trinity.cBookedSpot In listOfBookedSpotForChannel
                                        If tmpSp.MyEstimate > 0 Then
                                            If Not tmpSp.Film Is Nothing Then
                                                trpValueForFilm += tmpSp.MyEstimate * (tmpSp.Film.Share / 100)
                                            End If
                                        Else
                                            If Not tmpSp.Film Is Nothing Then
                                                trpValueForFilm += tmpSp.ChannelEstimate * (tmpSp.Film.Share / 100)
                                            End If
                                        End If
                                    Next
                                Next
                                .Cells(currentRow, column).Value += Math.Round(trpValueForFilm, 1)
                                column = column + 1
                            End If
                            currentRow = currentRow + 1
                        End If
                        If tmpBt.ToString().Contains("TV3") Or tmpBt.ToString().Contains("TV6") Or tmpBt.ToString().Contains("MTV") Or tmpBt.ToString().Contains("TV8") Or tmpBt.ToString().Contains("TV10") Or tmpBt.ToString().Contains("Comedy central") Or tmpBt.ToString().Contains("Comedy central") Or tmpBt.ToString().Contains("Nickelodeon") Then
                            Dim column = 3
                            If rowMTG = 0 Then
                                rowMTG = currentRow
                                currentRow = currentRow + 1
                                .Range("B" & rowMTG & ":B" & rowMTG).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & rowMTG & ":B" & rowMTG).Font.Color = RGB(255, 255, 255)
                            End If
                            If .Cells(rowMTG, 2).Value = "" Then
                                .Cells(rowMTG, 2).Value = "MTG TV"
                            End If
                            If Not tmpBt.IsSpecific Then
                                Dim tmpFilm = ""
                                For i As Integer = 1 To amountOfFilms
                                    Dim totalTRPForBT As Double = 0
                                    Dim trpForFilm As Double = 0
                                    Dim trpForShare As Double = 0
                                    Dim tmpShare As Double = 0

                                    For Each tmpWeek As Trinity.cWeek In tmpBt.Weeks
                                        trpForShare = tmpWeek.TRP
                                        tmpShare = tmpWeek.Films(i).Share
                                        tmpFilm = tmpWeek.Films(i).FilmLength
                                        If trpForShare <> 0 Then
                                            trpForFilm += trpForShare * (tmpShare / 100)
                                        End If
                                    Next
                                    totalTRPForBT += trpForFilm
                                    .Cells(rowHeader, column).Value = tmpFilm
                                    .Cells(rowMTG, column).Value += Math.Round(totalTRPForBT, 1)
                                    column = column + 1
                                Next
                            Else
                                Dim listOfBookedSpotForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpBookedSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpBookedSpot.Bookingtype Is tmpBt Then
                                        listOfBookedSpotForChannel.Add(tmpBookedSpot)
                                    End If
                                Next
                                Dim trpValueForFilm As Double = 0
                                For i As Integer = 1 To amountOfFilms
                                    For Each tmpSp As Trinity.cBookedSpot In listOfBookedSpotForChannel
                                        If tmpSp.MyEstimate > 0 Then
                                            If Not tmpSp.Film Is Nothing Then
                                                trpValueForFilm += tmpSp.MyEstimate * (tmpSp.Film.Share / 100)
                                            End If
                                        Else
                                            If Not tmpSp.Film Is Nothing Then
                                                trpValueForFilm += tmpSp.ChannelEstimate * (tmpSp.Film.Share / 100)
                                            End If
                                        End If
                                    Next
                                Next
                                .Cells(rowMTG, column).Value += Math.Round(trpValueForFilm, 1)
                            End If
                        End If
                        If tmpBt.ToString().Contains("Kanal5") Or tmpBt.ToString().Contains("Kanal9") Or tmpBt.ToString().Contains("Kanal 11") Or tmpBt.ToString().Contains("TV11") Or tmpBt.ToString().Contains("Eurosport") Or tmpBt.ToString().Contains("Discovery") Or tmpBt.ToString().Contains("TLC") Or tmpBt.ToString().Contains("ID") Or tmpBt.ToString().Contains("Investigation Discovery") Then
                            Dim column = 3
                            If rowSBS = 0 Then
                                rowSBS = currentRow
                                currentRow = currentRow + 1
                                .Range("B" & rowSBS & ":B" & rowSBS).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & rowSBS & ":B" & rowSBS).Font.Color = RGB(255, 255, 255)
                            End If
                            If .Cells(rowSBS, 2).Value = "" Then
                                .Cells(rowSBS, 2).Value = "SBS Discovery"
                            End If
                            If Not tmpBt.IsSpecific Then
                                Dim tmpFilm = ""
                                For i As Integer = 1 To amountOfFilms
                                    Dim totalTRPForBT As Double = 0
                                    Dim trpForFilm As Double = 0
                                    Dim trpForShare As Double = 0
                                    Dim tmpShare As Double = 0

                                    For Each tmpWeek As Trinity.cWeek In tmpBt.Weeks
                                        trpForShare = tmpWeek.TRP
                                        tmpShare = tmpWeek.Films(i).Share
                                        tmpFilm = tmpWeek.Films(i).FilmLength
                                        If trpForShare <> 0 Then
                                            trpForFilm += trpForShare * (tmpShare / 100)
                                        End If
                                    Next
                                    totalTRPForBT += trpForFilm
                                    .Cells(rowHeader, column).Value = tmpFilm
                                    .Cells(rowSBS, column).Value += Math.Round(totalTRPForBT, 1)
                                    column = column + 1
                                Next
                            Else
                                Dim listOfBookedSpotForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpBookedSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpBookedSpot.Bookingtype Is tmpBt Then
                                        listOfBookedSpotForChannel.Add(tmpBookedSpot)
                                    End If
                                Next
                                Dim trpValueForFilm As Double = 0
                                For i As Integer = 1 To amountOfFilms
                                    For Each tmpSp As Trinity.cBookedSpot In listOfBookedSpotForChannel
                                        If tmpSp.MyEstimate > 0 Then
                                            If Not tmpSp.Film Is Nothing Then
                                                trpValueForFilm += tmpSp.MyEstimate * (tmpSp.Film.Share / 100)
                                            End If
                                        Else
                                            If Not tmpSp.Film Is Nothing Then
                                                trpValueForFilm += tmpSp.ChannelEstimate * (tmpSp.Film.Share / 100)
                                            End If
                                        End If
                                    Next
                                Next
                                .Cells(rowSBS, column).Value += trpValueForFilm
                            End If
                        End If
                        If tmpBt.ToString().Contains("FOX") Or tmpBt.ToString().Contains("National") Then
                            Dim column = 3
                            If rowFOX = 0 Then
                                rowFOX = currentRow
                                currentRow = currentRow + 1
                                .Range("B" & rowFOX & ":B" & rowFOX).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & rowFOX & ":B" & rowFOX).Font.Color = RGB(255, 255, 255)
                            End If
                            If .Cells(rowFOX, 2).Value = "" Then
                                .Cells(rowFOX, 2).Value = "FOX International"
                            End If
                            If Not tmpBt.IsSpecific Then
                                Dim tmpFilm = ""
                                For i As Integer = 1 To amountOfFilms
                                    Dim totalTRPForBT As Double = 0
                                    Dim trpForFilm As Double = 0
                                    Dim trpForShare As Double = 0
                                    Dim tmpShare As Double = 0

                                    For Each tmpWeek As Trinity.cWeek In tmpBt.Weeks
                                        trpForShare = tmpWeek.TRP
                                        tmpShare = tmpWeek.Films(i).Share
                                        tmpFilm = tmpWeek.Films(i).FilmLength
                                        If trpForShare <> 0 Then
                                            trpForFilm += trpForShare * (tmpShare / 100)
                                        End If
                                    Next
                                    totalTRPForBT += trpForFilm
                                    .Cells(rowHeader, column).Value = tmpFilm
                                    .Cells(rowFOX, column).Value += Math.Round(totalTRPForBT, 1)
                                    column = column + 1
                                Next
                            Else
                                Dim listOfBookedSpotForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpBookedSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpBookedSpot.Bookingtype Is tmpBt Then
                                        listOfBookedSpotForChannel.Add(tmpBookedSpot)
                                    End If
                                Next
                                Dim trpValueForFilm As Double = 0
                                For i As Integer = 1 To amountOfFilms
                                    For Each tmpSp As Trinity.cBookedSpot In listOfBookedSpotForChannel
                                        If tmpSp.MyEstimate > 0 Then
                                            If Not tmpSp.Film Is Nothing Then
                                                trpValueForFilm += tmpSp.MyEstimate * (tmpSp.Film.Share / 100)
                                            End If
                                        Else
                                            If Not tmpSp.Film Is Nothing Then
                                                trpValueForFilm += tmpSp.ChannelEstimate * (tmpSp.Film.Share / 100)
                                            End If
                                        End If
                                    Next
                                Next
                                .Cells(rowFOX, column).Value += Math.Round(trpValueForFilm, 1)
                            End If
                        End If
                        'Cmore
                        If tmpBt.ToString().Contains("C more") Or tmpBt.ToString().Contains("CMORE") Then
                            Dim column = 3
                            If rowCMORE = 0 Then
                                rowCMORE = currentRow
                                currentRow = currentRow + 1
                                .Range("B" & rowCMORE & ":B" & rowCMORE).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & rowCMORE & ":B" & rowCMORE).Font.Color = RGB(255, 255, 255)
                            End If
                            If .Cells(rowCMORE, 2).Value = "" Then
                                .Cells(rowCMORE, 2).Value = "CMORE"
                            End If
                            If Not tmpBt.IsSpecific Then
                                Dim tmpFilm = ""
                                For i As Integer = 1 To amountOfFilms
                                    Dim totalTRPForBT As Double = 0
                                    Dim trpForFilm As Double = 0
                                    Dim trpForShare As Double = 0
                                    Dim tmpShare As Double = 0

                                    For Each tmpWeek As Trinity.cWeek In tmpBt.Weeks
                                        trpForShare = tmpWeek.TRP
                                        tmpShare = tmpWeek.Films(i).Share
                                        tmpFilm = tmpWeek.Films(i).FilmLength
                                        If trpForShare <> 0 Then
                                            trpForFilm += trpForShare * (tmpShare / 100)
                                        End If
                                    Next
                                    totalTRPForBT += trpForFilm
                                    .Cells(rowHeader, column).Value = tmpFilm
                                    .Cells(rowCMORE, column).Value += Math.Round(totalTRPForBT, 1)
                                    column = column + 1
                                Next
                            Else
                                Dim listOfBookedSpotForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpBookedSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpBookedSpot.Bookingtype Is tmpBt Then
                                        listOfBookedSpotForChannel.Add(tmpBookedSpot)
                                    End If
                                Next
                                Dim trpValueForFilm As Double = 0
                                For i As Integer = 1 To amountOfFilms
                                    For Each tmpSp As Trinity.cBookedSpot In listOfBookedSpotForChannel
                                        If tmpSp.MyEstimate > 0 Then
                                            If Not tmpSp.Film Is Nothing Then
                                                trpValueForFilm += tmpSp.MyEstimate * (tmpSp.Film.Share / 100)
                                            End If
                                        Else
                                            If Not tmpSp.Film Is Nothing Then
                                                trpValueForFilm += tmpSp.ChannelEstimate * (tmpSp.Film.Share / 100)
                                            End If
                                        End If
                                    Next
                                Next
                                .Cells(rowCMORE, column).Value += Math.Round(trpValueForFilm, 1)
                            End If
                        End If
                    End If
                Next
            Next
        End With
        _sheet.Range("B" & lastRowComboTable1 - 1 & ":M" & currentRow).Numberformat = "0.0"
        lastRowComboTable4 = currentRow + 1
    End Sub
    Sub printTable5()
        Dim filmColumnArray As Array = {":B", ":C", ":D", ":E", ":F", ":G", ":H", ":I", ":J", ":K", ":L", ":M", ":N", ":O", ":P", ":Q"}
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

        Dim rowTV4 = 0
        Dim rowMTG = 0
        Dim rowSBS = 0
        Dim rowFOX = 0
        Dim rowCMORE = 0

        With _sheet
            For Each tmpChan As Trinity.cChannel In Campaign.Channels
                For Each tmpBt As Trinity.cBookingType In tmpChan.BookingTypes
                    If tmpBt.BookIt Then
                        If tmpBt.ToString().Contains("TV4") Or tmpBt.ToString().Contains("Sjuan") Or tmpBt.ToString().Contains("TV12") Then
                            Dim column = 3
                            If tmpChan.ChannelName = "TV4" Then
                                If rowTV4 = 0 Then
                                    rowTV4 = currentRow
                                    currentRow = currentRow + 1
                                End If
                            End If
                            If tmpChan.ChannelName = "TV4" Then
                                currentRow = rowTV4
                            End If
                            .Range("B" & currentRow & ":B" & currentRow).Interior.Color = RGB(0, 137, 178)
                            .Range("B" & currentRow & ":B" & currentRow).Font.Color = RGB(255, 255, 255)
                            If .Cells(currentRow, 2).Value = "" Then
                                .Cells(currentRow, 2).Value = tmpChan.AdEdgeNames
                            End If
                            If Not tmpBt.IsSpecific Then
                                Dim tmpFilm = ""
                                Dim tmpTotalShareForFilm As Decimal = 0
                                For i As Integer = 1 To amountOfFilms
                                    Dim totalTRPForBT As Double = 0
                                    Dim trpForFilm As Double = 0
                                    Dim trpForShare As Double = 0
                                    Dim tmpShare As Double = 0

                                    For Each tmpWeek As Trinity.cWeek In tmpBt.Weeks
                                        trpForShare = tmpWeek.TRPBuyingTarget
                                        tmpShare = tmpWeek.Films(i).Share
                                        tmpFilm = tmpWeek.Films(i).FilmLength
                                        If trpForShare <> 0 Then
                                            trpForFilm += trpForShare * (tmpShare / 100)
                                        End If
                                    Next
                                    totalTRPForBT += trpForFilm
                                    .Cells(rowHeader, column).Value = tmpFilm
                                    .Cells(currentRow, column).Value += Math.Round(totalTRPForBT, 1)
                                    column = column + 1
                                Next
                            Else
                                Dim trpValueForFilm As Double = 0
                                Dim listOfBookedSpotForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpBookedSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpBookedSpot.Bookingtype Is tmpBt Then
                                        listOfBookedSpotForChannel.Add(tmpBookedSpot)
                                    End If
                                Next
                                For i As Integer = 1 To amountOfFilms
                                    For Each tmpSp As Trinity.cBookedSpot In listOfBookedSpotForChannel
                                        If Not tmpSp.Film Is Nothing Then
                                            If tmpSp.MyEstimateBuyTarget <> 0 Then
                                                If Not tmpSp.Film Is Nothing Then
                                                    trpValueForFilm += tmpSp.MyEstimateBuyTarget * (tmpSp.Film.Share / 100)
                                                End If
                                            Else
                                                If Not tmpSp.Film Is Nothing Then
                                                    trpValueForFilm += tmpSp.ChannelEstimate * (tmpSp.Film.Share / 100)
                                                End If
                                            End If
                                        End If
                                    Next
                                Next
                                .Cells(currentRow, column).Value += Math.Round(trpValueForFilm, 1)
                                column = column + 1
                            End If
                            currentRow = currentRow + 1
                        End If
                        If tmpBt.ToString().Contains("TV3") Or tmpBt.ToString().Contains("TV6") Or tmpBt.ToString().Contains("MTV") Or tmpBt.ToString().Contains("TV8") Or tmpBt.ToString().Contains("TV10") Or tmpBt.ToString().Contains("Comedy central") Or tmpBt.ToString().Contains("Comedy") Or tmpBt.ToString().Contains("Nickelodeon") Then
                            Dim column = 3
                            If rowMTG = 0 Then
                                rowMTG = currentRow
                                currentRow = currentRow + 1
                                .Range("B" & rowMTG & ":B" & rowMTG).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & rowMTG & ":B" & rowMTG).Font.Color = RGB(255, 255, 255)
                            End If
                            If .Cells(rowMTG, 2).Value = "" Then
                                .Cells(rowMTG, 2).Value = "MTG TV"
                            End If
                            If Not tmpBt.IsSpecific Then
                                Dim tmpFilm = ""
                                For i As Integer = 1 To amountOfFilms
                                    Dim totalTRPForBT As Double = 0
                                    Dim trpForFilm As Double = 0
                                    Dim trpForShare As Double = 0
                                    Dim tmpShare As Double = 0

                                    For Each tmpWeek As Trinity.cWeek In tmpBt.Weeks
                                        trpForShare = tmpWeek.TRPBuyingTarget
                                        tmpShare = tmpWeek.Films(i).Share
                                        tmpFilm = tmpWeek.Films(i).FilmLength
                                        If trpForShare <> 0 Then
                                            trpForFilm += trpForShare * (tmpShare / 100)
                                        End If
                                    Next
                                    totalTRPForBT += trpForFilm
                                    .Cells(rowHeader, column).Value = tmpFilm
                                    .Cells(rowMTG, column).Value += Math.Round(totalTRPForBT, 1)
                                    column = column + 1
                                Next
                            Else
                                Dim listOfBookedSpotForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpBookedSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpBookedSpot.Bookingtype Is tmpBt Then
                                        listOfBookedSpotForChannel.Add(tmpBookedSpot)
                                    End If
                                Next
                                Dim trpValueForFilm As Double = 0
                                For i As Integer = 1 To amountOfFilms
                                    For Each tmpSp As Trinity.cBookedSpot In listOfBookedSpotForChannel
                                        If Not tmpSp.Film Is Nothing Then
                                            If tmpSp.MyEstimateBuyTarget <> 0 Then
                                                If Not tmpSp.Film Is Nothing Then
                                                    trpValueForFilm += tmpSp.MyEstimateBuyTarget * (tmpSp.Film.Share / 100)
                                                End If
                                            Else
                                                If Not tmpSp.Film Is Nothing Then
                                                    trpValueForFilm += tmpSp.ChannelEstimate * (tmpSp.Film.Share / 100)
                                                End If
                                            End If
                                        End If
                                    Next
                                Next
                                .Cells(rowMTG, column).Value += Math.Round(trpValueForFilm, 1)
                            End If
                        End If
                        If tmpBt.ToString().Contains("Kanal5") Or tmpBt.ToString().Contains("Kanal9") Or tmpBt.ToString().Contains("Kanal 11") Or tmpBt.ToString().Contains("TV11") Or tmpBt.ToString().Contains("Eurosport") Or tmpBt.ToString().Contains("Discovery") Or tmpBt.ToString().Contains("TLC") Or tmpBt.ToString().Contains("ID") Or tmpBt.ToString().Contains("Investigation Discovery") Then
                            Dim column = 3
                            If rowSBS = 0 Then
                                rowSBS = currentRow
                                currentRow = currentRow + 1
                                .Range("B" & rowSBS & ":B" & rowSBS).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & rowSBS & ":B" & rowSBS).Font.Color = RGB(255, 255, 255)
                            End If
                            If .Cells(rowSBS, 2).Value = "" Then
                                .Cells(rowSBS, 2).Value = "SBS Discovery"
                            End If
                            If Not tmpBt.IsSpecific Then
                                Dim tmpFilm = ""
                                For i As Integer = 1 To amountOfFilms
                                    Dim totalTRPForBT As Double = 0
                                    Dim trpForFilm As Double = 0
                                    Dim trpForShare As Double = 0
                                    Dim tmpShare As Double = 0

                                    For Each tmpWeek As Trinity.cWeek In tmpBt.Weeks
                                        trpForShare = tmpWeek.TRPBuyingTarget
                                        tmpShare = tmpWeek.Films(i).Share
                                        tmpFilm = tmpWeek.Films(i).FilmLength
                                        If trpForShare <> 0 Then
                                            trpForFilm += trpForShare * (tmpShare / 100)
                                        End If
                                    Next
                                    totalTRPForBT += trpForFilm
                                    .Cells(rowHeader, column).Value = tmpFilm
                                    .Cells(rowSBS, column).Value += Math.Round(totalTRPForBT, 1)
                                    column = column + 1
                                Next
                            Else
                                Dim listOfBookedSpotForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpBookedSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpBookedSpot.Bookingtype Is tmpBt Then
                                        listOfBookedSpotForChannel.Add(tmpBookedSpot)
                                    End If
                                Next
                                Dim trpValueForFilm As Double = 0
                                For i As Integer = 1 To amountOfFilms
                                    For Each tmpSp As Trinity.cBookedSpot In listOfBookedSpotForChannel
                                        If Not tmpSp.Film Is Nothing Then
                                            If tmpSp.MyEstimateBuyTarget <> 0 Then
                                                If Not tmpSp.Film Is Nothing Then
                                                    trpValueForFilm += tmpSp.MyEstimateBuyTarget * (tmpSp.Film.Share / 100)
                                                End If
                                            Else
                                                If Not tmpSp.Film Is Nothing Then
                                                    trpValueForFilm += tmpSp.ChannelEstimate * (tmpSp.Film.Share / 100)
                                                End If
                                            End If
                                        End If
                                    Next
                                Next
                                .Cells(rowSBS, column).Value += Math.Round(trpValueForFilm, 1)
                            End If
                        End If
                        If tmpBt.ToString().Contains("FOX") Or tmpBt.ToString().Contains("National") Then
                            Dim column = 3
                            If rowFOX = 0 Then
                                rowFOX = currentRow
                                currentRow = currentRow + 1
                                .Range("B" & rowFOX & ":B" & rowFOX).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & rowFOX & ":B" & rowFOX).Font.Color = RGB(255, 255, 255)
                            End If
                            If .Cells(rowFOX, 2).Value = "" Then
                                .Cells(rowFOX, 2).Value = "FOX International"
                            End If
                            If Not tmpBt.IsSpecific Then
                                Dim tmpFilm = ""
                                For i As Integer = 1 To amountOfFilms
                                    Dim totalTRPForBT As Double = 0
                                    Dim trpForFilm As Double = 0
                                    Dim trpForShare As Double = 0
                                    Dim tmpShare As Double = 0

                                    For Each tmpWeek As Trinity.cWeek In tmpBt.Weeks
                                        trpForShare = tmpWeek.TRPBuyingTarget
                                        tmpShare = tmpWeek.Films(i).Share
                                        tmpFilm = tmpWeek.Films(i).FilmLength
                                        If trpForShare <> 0 Then
                                            trpForFilm += trpForShare * (tmpShare / 100)
                                        End If
                                    Next
                                    totalTRPForBT += trpForFilm
                                    .Cells(rowHeader, column).Value = tmpFilm
                                    .Cells(rowFOX, column).Value += Math.Round(totalTRPForBT, 1)
                                    column = column + 1
                                Next
                            Else
                                Dim listOfBookedSpotForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpBookedSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpBookedSpot.Bookingtype Is tmpBt Then
                                        listOfBookedSpotForChannel.Add(tmpBookedSpot)
                                    End If
                                Next
                                Dim trpValueForFilm As Double = 0
                                For i As Integer = 1 To amountOfFilms
                                    For Each tmpSp As Trinity.cBookedSpot In listOfBookedSpotForChannel
                                        If Not tmpSp.Film Is Nothing Then
                                            If tmpSp.MyEstimateBuyTarget <> 0 Then
                                                If Not tmpSp.Film Is Nothing Then
                                                    trpValueForFilm += tmpSp.MyEstimateBuyTarget * (tmpSp.Film.Share / 100)
                                                End If
                                            Else
                                                If Not tmpSp.Film Is Nothing Then
                                                    trpValueForFilm += tmpSp.ChannelEstimate * (tmpSp.Film.Share / 100)
                                                End If
                                            End If
                                        End If
                                    Next
                                Next
                                .Cells(rowFOX, column).Value += Math.Round(trpValueForFilm, 1)
                            End If
                        End If
                        'Cmore
                        If tmpBt.ToString().Contains("CMORE") Or tmpBt.ToString().Contains("C more") Then
                            Dim column = 3
                            If rowCMORE = 0 Then
                                rowCMORE = currentRow
                                currentRow = currentRow + 1
                                .Range("B" & rowCMORE & ":B" & rowCMORE).Interior.Color = RGB(0, 137, 178)
                                .Range("B" & rowCMORE & ":B" & rowCMORE).Font.Color = RGB(255, 255, 255)
                            End If
                            If .Cells(rowCMORE, 2).Value = "" Then
                                .Cells(rowCMORE, 2).Value = "CMORE"
                            End If
                            If Not tmpBt.IsSpecific Then
                                Dim tmpFilm = ""
                                For i As Integer = 1 To amountOfFilms
                                    Dim totalTRPForBT As Double = 0
                                    Dim trpForFilm As Double = 0
                                    Dim trpForShare As Double = 0
                                    Dim tmpShare As Double = 0

                                    For Each tmpWeek As Trinity.cWeek In tmpBt.Weeks
                                        trpForShare = tmpWeek.TRPBuyingTarget
                                        tmpShare = tmpWeek.Films(i).Share
                                        tmpFilm = tmpWeek.Films(i).FilmLength
                                        If trpForShare <> 0 Then
                                            trpForFilm += trpForShare * (tmpShare / 100)
                                        End If
                                    Next
                                    totalTRPForBT += trpForFilm
                                    .Cells(rowHeader, column).Value = tmpFilm
                                    .Cells(rowCMORE, column).Value += Math.Round(totalTRPForBT, 1)
                                    column = column + 1
                                Next
                            Else
                                Dim listOfBookedSpotForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpBookedSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpBookedSpot.Bookingtype Is tmpBt Then
                                        listOfBookedSpotForChannel.Add(tmpBookedSpot)
                                    End If
                                Next
                                Dim trpValueForFilm As Double = 0
                                For i As Integer = 1 To amountOfFilms
                                    For Each tmpSp As Trinity.cBookedSpot In listOfBookedSpotForChannel
                                        If Not tmpSp.Film Is Nothing Then
                                            If tmpSp.MyEstimateBuyTarget <> 0 Then
                                                If Not tmpSp.Film Is Nothing Then
                                                    trpValueForFilm += tmpSp.MyEstimateBuyTarget * (tmpSp.Film.Share / 100)
                                                End If
                                            Else
                                                If Not tmpSp.Film Is Nothing Then
                                                    trpValueForFilm += tmpSp.ChannelEstimate * (tmpSp.Film.Share / 100)
                                                End If
                                            End If
                                        End If
                                    Next
                                Next
                                .Cells(rowCMORE, column).Value += Math.Round(trpValueForFilm, 1)
                            End If
                        End If
                    End If
                Next
            Next
        End With
        _sheet.Range("B" & lastRowComboTable1 - 1 & ":M" & currentRow).Numberformat = "0.0"
        lastRowComboTable5 = currentRow + 1
    End Sub
    Sub printTable6()

        Dim weekCol As Array = {":A", ":B", ":C", ":D", ":E", ":F", ":G", ":H", ":I", ":J", ":K", ":L", ":M", ":N", ":O", ":P", ":Q"}
        Dim columnValue As String = weekCol.GetValue(3 + amountOfWeeks)
        Dim currentRow As Integer = lastRowComboTable5
        Dim rowHeader = 0
        With _sheet
            .Range("B" & currentRow + 1 & columnValue & currentRow + 1).Interior.Color = RGB(0, 137, 178)
            .Range("B" & currentRow + 1 & columnValue & currentRow + 1).Font.Color = RGB(255, 255, 255)

            .Range("B" & currentRow & columnValue & currentRow).Interior.Color = RGB(0, 37, 110)
            .Range("B" & currentRow & columnValue & currentRow).Font.Color = RGB(255, 255, 255)
            .Cells(currentRow, 2).Value = "Filmcodes"
            currentRow = currentRow + 1
            .Cells(currentRow, 2).Value = "Group"
            .Cells(currentRow, 3).Value = "Title"
            .Cells(currentRow, 4).Value = "Duration"
            rowHeader = currentRow
        End With

        currentRow = currentRow + 1
        Dim amountOfFilms = 1

        For i As Integer = 1 To listOfFilms.Count
            With _sheet
                For Each tmpChan As Trinity.cChannel In Campaign.Channels
                    For Each tmpBt As Trinity.cBookingType In tmpChan.BookingTypes
                        If tmpBt.BookIt Then
                            If Not tmpBt.IsSpecific Then
                                If .Cells(currentRow, 2).Value Is Nothing Then
                                    .Cells(currentRow, 2).Value = amountOfFilms
                                End If
                                Dim totalTRPForWeek = 0
                                Dim column = 5
                                For Each tmpWeek As Trinity.cWeek In tmpBt.Weeks
                                    totalTRPForWeek = tmpWeek.TRP * (tmpWeek.Films(i).Share / 100)
                                    If .Cells(rowHeader, column).Value = "" Then
                                        .Cells(rowHeader, column).Value = "2015w" & tmpWeek.Name
                                    End If
                                    If .Cells(currentRow, 3).Value Is Nothing Then
                                        If tmpWeek.Films(i).Name = "" Then
                                            .Cells(currentRow, 3).Value = tmpWeek.Films(i).Filmcode
                                        Else
                                            .Cells(currentRow, 3).Value = tmpWeek.Films(i).Name
                                        End If
                                    End If
                                    If .Cells(currentRow, 4).Value Is Nothing Then
                                        .Cells(currentRow, 4).Value = tmpWeek.Films(i).FilmLength
                                    End If
                                    .Cells(currentRow, column).Value += Math.Round(totalTRPForWeek, 1)
                                    column += 1
                                    totalTRPForWeek = 0
                                Next
                            Else
                                Dim listOfSpotsForChannel As New List(Of Trinity.cBookedSpot)
                                For Each tmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                    If tmpSpot.Bookingtype Is tmpBt Then
                                        listOfSpotsForChannel.Add(tmpSpot)
                                    End If
                                Next

                                Dim listofSpotts As New List(Of Trinity.cBookedSpot)
                                Dim trpForWeek As Decimal = 0
                                Dim column As Integer = 5
                                Dim currentItem As Integer = 1
                                Dim amountOfSpott As Integer = listOfSpotsForChannel.Count()

                                For Each tmpWeek As Trinity.cWeek In tmpBt.Weeks
                                    Dim spotListForWeek As New List(Of Trinity.cBookedSpot)
                                    For Each tmpSpot As Trinity.cBookedSpot In listOfSpotsForChannel
                                        If tmpSpot.week.Name = tmpWeek.Name Then
                                            spotListForWeek.Add(tmpSpot)
                                        End If
                                    Next
                                    Dim currentSpotNr As Integer = 1

                                    For Each tmpBookedSpot As Trinity.cBookedSpot In spotListForWeek
                                        If spotListForWeek.Count >= currentSpotNr Then
                                            If tmpBookedSpot.MyEstimate <> 0 Then
                                                trpForWeek += (tmpWeek.Films(i).Share / 100) * tmpBookedSpot.MyEstimate
                                            Else
                                                trpForWeek += (tmpWeek.Films(i).Share / 100) * tmpBookedSpot.ChannelEstimate
                                            End If
                                            currentSpotNr += 1
                                        End If
                                    Next
                                    If .Cells(currentRow, 4).Value Is Nothing Then
                                        .Cells(currentRow, 4).Value = tmpWeek.Films(i).FilmLength
                                    End If
                                    .Cells(currentRow, column).Value += Math.Round(trpForWeek, 1)
                                    trpForWeek = 0
                                    column = column + 1
                                Next
                            End If
                            .Range("B" & currentRow & ":B" & currentRow).Interior.Color = RGB(0, 137, 178)
                            .Range("B" & currentRow & ":B" & currentRow).Font.Color = RGB(255, 255, 255)
                        End If
                    Next
                Next
            End With
            amountOfFilms += 1
            currentRow = currentRow + 1
        Next
        _sheet.Range("E" & lastRowComboTable1 - 1 & ":M" & currentRow).Numberformat = "0.0"
    End Sub

    'Sub printTable6()
    '    Dim currentRow As Integer = lastRowComboTable5
    '    Dim columnValue As String = 3
    '    Dim rowHeader = currentRow

    '    With _sheet

    '        .Range("B" & currentRow + 1 & columnValue & currentRow + 1).Interior.Color = RGB(0, 137, 178)
    '        .Range("B" & currentRow + 1 & columnValue & currentRow + 1).Font.Color = RGB(255, 255, 255)

    '        .Range("B" & currentRow & columnValue & currentRow).Interior.Color = RGB(0, 37, 110)
    '        .Range("B" & currentRow & columnValue & currentRow).Font.Color = RGB(255, 255, 255)
    '        .Cells(currentRow, 2).Value = "Filmcodes"
    '        rowHeader = currentRow
    '        currentRow = currentRow + 1

    '        .Cells(currentRow, 2).Value = "Group"
    '        .Cells(currentRow, 3).Value = "Title"
    '        .Cells(currentRow, 4).Value = "Duration"

    '        currentRow = currentRow + 1
    '    End With
    '    With _sheet
    '        Dim amountOfFilms = 0
    '        For Each tmpChan As Trinity.cChannel In Campaign.Channels
    '            For Each tmpBt As Trinity.cBookingType In tmpChan.BookingTypes
    '                If tmpBt.BookIt Then

    '                    If Not tmpBt.IsSpecific Then
    '                        Dim totalTRP = 0
    '                        For Each tmpWeek As Trinity.cWeek In tmpBt.Weeks
    '                            Dim column = 5
    '                            For Each tmpF As Trinity.cFilm In tmpWeek.Films

    '                                totalTRP += tmpWeek.TRP * (tmpF.Share / 100)


    '                            Next
    '                            .Cells(currentRow, 4).Value += amountOfFilms
    '                            .Cells(currentRow, column).Value += totalTRP
    '                            column = column + 1
    '                        Next
    '                    Else

    '                    End If
    '                End If
    '            Next
    '        Next


    '        For Each f As Trinity.cFilm In listOfFilms
    '            '.Cells(currentRow, columnValue).Value = f.FilmLength
    '        Next
    '    End With

    '    currentRow = currentRow + 1
    'End Sub
    Public Function shareForFilmWeek(ByVal film As Trinity.cFilm, ByVal Week As Trinity.cWeek)

        Return Nothing
    End Function

    Sub CleanUp()

        _excel.Visible = True
    End Sub
End Class
