Imports clTrinity.Trinity

Public Class cExportFileToMediatool
    
    Dim _excel As CultureSafeExcel.Application
    Dim _wb As CultureSafeExcel.Workbook
    Dim _sheet1 As CultureSafeExcel.Worksheet
    Dim _sheet2 As CultureSafeExcel.Worksheet
    Dim _sheet3 As CultureSafeExcel.Worksheet

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
    Dim _listOfWeeks As new List(Of cWeek)
    
    'Dim _campaign As Trinity.cKampanj

    Public Sub exportDatoramaFile(Optional ByVal tempBundleTV4 As Boolean = False, Optional ByVal tempBundleMTG As Boolean = False, Optional ByVal tempBundleMTGSpecial As Boolean = False, Optional ByVal tempBundleSBS As Boolean = False, Optional ByVal tempBundleFOX As Boolean = False, Optional ByVal tempBundleCMORE As Boolean = False, Optional ByVal tempBundleDisney As Boolean = False, Optional ByVal temBundleTNT As Boolean = False, Optional ByVal useOwnCommission As Boolean = False, Optional ByVal useOwnCommissionAmount As Decimal = 0, Optional ByVal tmpPrintExportAsCampaign As Boolean = False)
        
        prepareEnivorment()
        checkAmountOfBookedChannels()
        _wb = _excel.AddWorkbook
        _sheet1 = _wb.Sheets(1)
        _sheet1.Name = "Mediatool"

        getFilms()
        GetAmountOfFilmLengths()
        GetWeeks()    
                            
        Dim value = Trinity.Helper.FormatDateForBooking(Campaign.StartDate)                
        With _sheet1
            printHeader()
            filterData1()
            For i As Integer = 1 To 40
                .Columns(i).AutoFit()
            Next
        End With
        _sheet1.Range("B" & 2 & ":M" & 200).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
        _sheet1.Range("B" & 2 & ":M" & 200).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
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
        If tmpChannelName.Contains("TV4") Or tmpChannelName = "Sjuan" Or tmpChannelName = "TV12" Then
            Return True
        End If
        Return False
    End Function
    Public Function checkNameSBS(ByVal tmpChannelName As String)
        If tmpChannelName = "Kanal5" Or tmpChannelName = "Kanal9" Or tmpChannelName = "Kanal 11" Or tmpChannelName = "Kanal11" Or tmpChannelName = "TV11" Or tmpChannelName = "Eurosport" Or tmpChannelName = "Discovery" Or tmpChannelName = "TLC" Or tmpChannelName = "ID" Or tmpChannelName = "Investigation Discovery" Then
            Return True
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

    Public Function GetWeeks
        For Each tmpC As Trinity.cChannel In Campaign.Channels
            For Each tmpBT As Trinity.cBookingType In tmpC.BookingTypes
                If tmpBT.BookIt Then
                    For Each tmpWeek As Trinity.cWeek In tmpBT.Weeks
                        _listOfWeeks.Add(tmpWeek)
                    Next
                    Return _listOfWeeks
                End If
            Next
        Next
    End Function


    Sub printHeader()
        _sheet1.Range("B" & 2 & ":M" & 999).Font.Name = "Calibri"
        _sheet1.Range("B" & 2 & ":M" & 999).Font.Size = 10
        With _sheet1
            'Create textfields
            .Cells(1, 1).Value = "Media vehicle"
            .Cells(1, 2).Value = "Brand"
            .Cells(1, 3).Value = "Start date"
            .Cells(1, 4).Value = "End date"
            .Cells(1, 5).Value = "Material date"
            .Cells(1, 6).Value = "Movie code"
            .Cells(1, 7).Value = "Target"
            .Cells(1, 8).Value = "Objective"
            .Cells(1, 9).Value = "TRP"
            .Cells(1, 10).Value = "CPP"
            .Cells(1, 11).Value = "Gross cost"
            .Cells(1, 12).Value = "Discount in %"
            .Cells(1, 13).Value = "Net cost"
            .Cells(1, 14).Value = "Commission"
            .Cells(1, 15).Value = "Net Net cost"     
            .Cells(1, 16).Value = "Service fee"
            .Cells(1, 17).Value = "CTC"
            .Cells(1, 18).Value = "Comments"
            .Cells(1, 19).Value = "Events"
            
            ''Print data
            '.Cells(2, 1).Value = Campaign.Name
            '.Cells(2, 2).Value = "NO"
            'Dim userCompany As String = ""

            'Dim cs As String = TrinitySettings.ConnectionString(Trinity.cSettings.SettingsLocationEnum.locNetwork)

            'If cs.Contains("mec") Then
            '    userCompany = "MEC"
            'ElseIf cs.Contains("mc") Then
            '    userCompany = "Mediacom"
            'ElseIf cs.Contains("maxus") Then
            '    userCompany = "Maxus"
            'ElseIf cs.Contains("ms") Then
            '    userCompany = "Mindshare"
            'End If

            '.Cells(2, 3).Value = userCompany
            '.Cells(2, 4).Value = Campaign.Planner
            '.Cells(2, 5).Value = "TV Campaign"
            '.Cells(2, 6).Value = "Adults"
            '.Cells(2, 7).Value = Campaign.Client
            '.Cells(2, 8).Value = Campaign.Client

            '.Cells(2, 9).Value = Campaign.PlannedTotCTC.ToString()
            '.Cells(2, 10).Value = Campaign.ClientID.ToString()
            '.Cells(2, 11).Value = Campaign.ContractID.ToString()
            '.Cells(2, 12).Value = Campaign.Product
            
            'Dim tmpMainTarget As String = translateTargetName(Campaign.MainTarget.TargetName)
            ''Dim tmpSecondTarget As String = translateTargetName(Campaign.SecondaryTarget.TargetName)
            'Dim tmpSecondTarget As String = translateTargetName(Campaign.MainTarget.TargetName)
            '.Cells(2, 13).Value = tmpMainTarget
            '.Cells(2, 14).Value = tmpSecondTarget
            '.Cells(2, 15).Value = (Campaign.FrequencyFocus + 1) & "+"

            '.Cells(2, 16).Value = Trinity.Helper.FormatDateForBooking(Campaign.StartDate)
            '.Cells(2, 17).Value = Trinity.Helper.FormatDateForBooking(Campaign.EndDate)
            
        End With
    End Sub

    Sub printHeader2(byval tmpCol As Integer)
        _sheet2.Range("B" & 2 & ":M" & 999).Font.Name = "Calibri"
        _sheet2.Range("B" & 2 & ":M" & 999).Font.Size = 10
        With _sheet2
            'Create textfields
            .Cells(1, tmpcol + 1).Value = "Campaign title"
            .Cells(1, tmpcol + 2).Value = "Area"
            .Cells(1, tmpcol +  3).Value = "Agency"
            .Cells(1, tmpcol +  4).Value = "Planner"
            .Cells(1, tmpcol + 5).Value = "Campaign type"
            .Cells(1, tmpcol + 6).Value = "Campaign type 2"
            .Cells(1, tmpcol + 7).Value = "Concern"
            .Cells(1, tmpcol + 8).Value = "Advertiser"
            .Cells(1, tmpcol + 9).Value = "Planned Budget incl. Fee"
            .Cells(1, tmpcol + 10).Value = "Marathonclient ID"
            .Cells(1, tmpcol + 11).Value = "Marathon client agreement"
            .Cells(1, tmpcol + 12).Value = "Product/Brand"

            .Cells(1, tmpcol + 13).Value = "Primary target"
            .Cells(1, tmpcol + 14).Value = "Secondary target"
            .Cells(1, tmpcol + 15).Value = "Eff. Frequency"            

            .Cells(1, tmpcol + 16).Value = "Start date"
            .Cells(1, tmpcol + 17).Value = "End date"
            
            'Print data
            .Cells(2, tmpcol + 1).Value = Campaign.Name
            .Cells(2, tmpcol + 2).Value = "NO"
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

            .Cells(2, tmpcol + 3).Value = userCompany
            .Cells(2, tmpcol + 4).Value = Campaign.Planner
            .Cells(2, tmpcol + 5).Value = "TV Campaign"
            .Cells(2, tmpcol + 6).Value = "Adults"
            .Cells(2, tmpcol + 7).Value = Campaign.Client
            .Cells(2, tmpcol + 8).Value = Campaign.Client

            .Cells(2, tmpcol + 9).Value = Campaign.PlannedTotCTC.ToString()
            .Cells(2, tmpcol + 10).Value = Campaign.ClientID.ToString()
            .Cells(2, tmpcol + 11).Value = Campaign.ContractID.ToString()
            .Cells(2, tmpcol + 12).Value = Campaign.Product
            
            Dim tmpMainTarget As String = translateTargetName(Campaign.MainTarget.TargetName)
            'Dim tmpSecondTarget As String = translateTargetName(Campaign.SecondaryTarget.TargetName)
            Dim tmpSecondTarget As String = translateTargetName(Campaign.MainTarget.TargetName)
            .Cells(2, tmpcol + 13).Value = tmpMainTarget
            .Cells(2, tmpcol + 14).Value = tmpSecondTarget
            .Cells(2, tmpcol + 15).Value = (Campaign.FrequencyFocus + 1) & "+"


            .Cells(2, tmpcol + 16).Value = Trinity.Helper.FormatDateForBooking(Campaign.StartDate)
            .Cells(2, tmpcol + 17).Value = Trinity.Helper.FormatDateForBooking(Campaign.EndDate)

            .Cells(1, tmpCol + 18).Value = "Station"
            .Cells(1, tmpCol + 19).Value = "Network"


            Dim rfCol As Integer = tmpCol + 20
            Dim plus As String = "+"
            For i As Integer = 1 To 10
                .Cells(2 - 1, rfCol).Value = i & plus

                Dim reach As Decimal = Campaign.ReachGoal(i, Trinity.cKampanj.ReachTargetEnum.rteMainTarget)
                reach = reach / 100
                .Cells(2, rfCol).Value = reach
                .Cells(2, rfCol).Numberformat = "#%"
                rfCol = rfCol + 1
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
    Sub HelperTable1(ByVal tmpChan As Trinity.cChannel, ByVal tmpB As Trinity.cBookingType, ByVal tmpGroupName As String, ByVal bundle As Boolean, ByVal row As Integer, ByVal tmpChannelName As String, Optional ByVal savedRow As Integer = 0, Optional ByVal printExportAsCampaign As Boolean = False, Optional ByVal column as integer = 0)
        
        Dim tmpBook As Trinity.cBookingType = tmpB
        Dim tmpBundle = bundle

        Dim stationChannels = ""

        Dim totalPlannedGrossBudget As Decimal = 0
        Dim totalPlannedNetBudget As Decimal = 0
        Dim totalPlannedNetNetBudget As Decimal = 0
        Dim totalPlannedFee As Decimal = 0

        Dim listOfSpots As New List(Of Trinity.cBookedSpot)

        Dim totalTRPForBT As Decimal = 0
        Dim totalTRPForPT As Decimal = 0

        Dim lastRow As Integer = 0

        With _sheet1
            
            If Not .Cells(row, 18) Is Nothing Then
                .Cells(row - 1, column).Value = "Stations"
            End If
            If tmpChan.AdEdgeNames <> "" Then
                    stationChannels += tmpChan.AdEdgeNames + "; "
            Else
                stationChannels += tmpChan.ChannelName + "; "
            End If
            .Cells(row, column).Value += stationChannels
            If .Cells(row - 1, column  + 1).Value Is Nothing Then 
                .Cells(row - 1, column + 1).Value = "Spotlength"
            End If
            If .Cells(row, column + 1).Value Is Nothing
                For i As Integer = 0 to _amountOfFilmLength.Count - 1
                    .Cells(row, column + 1).Value += _amountOfFilmLength(i).ToString() + " sec;"
                Next
            End If

            If .Cells(row - 1, column + 2).value Is Nothing Then                                
                .Cells(row - 1, column + 2).Value = "Spot Title"
            End If
            If .Cells(row, column + 2).Value Is Nothing 
                For each tmpf As Trinity.cFilm in listOfFilms
                    .Cells(row, column + 2).Value += tmpf.Filmcode + ";"
                Next
            End If
            
            Dim tmpCTC As Integer = 0
            Dim tmptotalPlannedGrossBudget = 0
            If tmpBook.ConfirmedGrossBudget = 0 Then
                tmptotalPlannedGrossBudget = tmpBook.PlannedGrossBudget
            Else
                tmptotalPlannedGrossBudget = tmpBook.ConfirmedGrossBudget
            End If
            
            For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                totalTRPForPT += tmpWeek.TRP
            Next
            For Each tmpWeek As Trinity.cWeek In tmpBook.Weeks
                totalTRPForBT += tmpWeek.TRPBuyingTarget
            Next
            If .Cells(row - 1, column + 3).Value Is Nothing                
                .Cells(row -1, column + 3).Value = "Buying target: TRP pr. station, pr. spotlength"
            End If
            .Cells(row, column + 3).Value += Math.Round(totalTRPForBT, 2)
            
            If .Cells(row - 1, column + 4).Value Is Nothing                
                .Cells(row - 1, column + 4).Value = "Primary target: TRP pr. station, pr. spotlength"
            End If
            .Cells(row, column + 4).Value += Math.Round(totalTRPForPT, 2)            
            
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
        ElseIf checkNameFOX(tmpChName) Then
            result = "FOX"
        ElseIf checkNameMatkanalen(tmpChName) Then
            result = "Matkanalen"
        End If
        Return result
    End Function

    
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
    Public Function checkNameMTG(ByVal tmpChannelName As String)
        If tmpChannelName.Contains("TV3") Or tmpChannelName.Contains("TV6") Or tmpChannelName.Contains("MTV") Or tmpChannelName.Contains("Comedy central") Or tmpChannelName.Contains("Comedy") Or tmpChannelName.Contains("Viasat") Then
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
    Public Function checkNameFOX(ByVal tmpChannelName As String)
        If tmpChannelName.Contains("National Geographic") Or tmpChannelName.Contains("Fox") Or tmpChannelName.Contains("BBC Brit") Or tmpChannelName.Contains("TNT")Then
            Return True
        End If
        Return False
    End Function
    Sub printData1(ByVal ch As cChannel, ByVal bt As cBookingType, ByVal currentRow As Integer)
        
        Dim totalTRPForChannel As Decimal = 0
        Dim totalNet As Decimal = 0
        Dim totalNetNet As Decimal = 0
        Dim totalGrossCost As Decimal = 0
        Dim netCPP As Decimal = 0
        Dim filmCode As String = ""     
        Dim effDisc As Decimal = 0 
        Dim listofWeeks As New List(Of Trinity.cWeek)
            
        With _sheet1
            For i As Integer = 1 To bt.Weeks.Count                
                For each tmpFIlm As cFilm In bt.Weeks(i).Films
                    If tmpFIlm.Share <> Nothing Or tmpFIlm.Share.ToString <> ""
                        totalTRPForChannel += bt.Weeks(i).TRP
                        If not bt.IsCompensation                            
                            totalNet += bt.Weeks(i).NetBudget
                            totalNetNet = totalNet * (1 - ch.AgencyCommission)
                            totalGrossCost += bt.Weeks(i).GrossBudget
                            effDisc = bt.Weeks(i).Discount
                            If netCPP = 0
                                netCPP = bt.Weeks(i).NetCPP30
                            End If
                        End If
                    End If
                    filmCode = tmpFIlm.Filmcode
                Next
            Next

            For each week As Trinity.cWeek in bt.Weeks
                listofWeeks.add(week)
            Next

            For Each TmpCost As Trinity.cCost In Campaign.Costs
                If TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePercent Then
                    If TmpCost.CountCostOn = Trinity.cCost.CostOnPercentEnum.CostOnRatecard Then
                        If TmpCost.CountCostOn

                        End If
                        totalNetNet = totalNet * (1 + TmpCost.Amount)
                    End If
                End If
            Next

            Dim tmpCostAmount
            Dim totalFees
            Dim totalPlannedNetBudget = totalNet
            Dim tmpPrintNetNet
            Dim tmptotalPlannedNetBudget
            Dim totalPlannedNetNetBudget
            Dim tmpCTC
            Dim SumUnit

            If Campaign.Costs.Count > 0 Then
                For Each TmpCost As Trinity.cCost In Campaign.Costs
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
                    tmpPrintNetNet = tmptotalPlannedNetBudget * (1 - ch.AgencyCommission)
                    totalPlannedNetBudget = tmptotalPlannedNetBudget * (1 - ch.AgencyCommission) + totalFees
                End If
                totalPlannedNetNetBudget = totalPlannedNetBudget
                tmpCTC = totalPlannedNetNetBudget + totalFees
                For Each TmpCost As Trinity.cCost In Campaign.Costs
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
                            For Each TmpWeek As Trinity.cWeek In bt.Weeks                                
                                For each _w As cWeek In listOfWeeks
                                    If _w.Name = tmpWeek.Name
                                        Dim Discount As Single = TmpWeek.GrossBudget - TmpWeek.NetBudget
                                        SumUnit += (Discount * TmpCost.Amount)
                                    End if
                                Next
                            Next
                        Else
                            For Each bt In DirectCast(TmpCost.CountCostOn, Trinity.cChannel).BookingTypes
                                For Each TmpWeek As Trinity.cWeek In bt.Weeks
                                    For each _w As cWeek In listOfWeeks
                                         If _w.Name = tmpWeek.Name
                                            Dim Discount As Single = TmpWeek.GrossBudget - TmpWeek.NetBudget
                                            SumUnit += (Discount * TmpCost.Amount)
                                         End If
                                    next
                                Next
                            Next
                        End If
                        tmpCTC = tmpCTC + SumUnit
                    ElseIf TmpCost.CostType = Trinity.cCost.CostTypeEnum.CostTypePerUnit Then
                        SumUnit = 0
                        If TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnSpots Then
                            SumUnit = SumUnit + bt.EstimatedSpotCount * TmpCost.Amount
                        ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnBuyingTRP Then
                            SumUnit = SumUnit + bt.TotalTRPBuyingTarget * TmpCost.Amount
                        ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnBuyingTRP Then
                            SumUnit = SumUnit + bt.TotalTRP * TmpCost.Amount
                        ElseIf TmpCost.CountCostOn = Trinity.cCost.CostOnUnitEnum.CostOnWeeks Then
                            SumUnit = Campaign.Channels(1).BookingTypes(1).Weeks.Count * TmpCost.Amount
                        End If
                        totalFees += SumUnit
                        totalPlannedNetNetBudget = totalPlannedNetBudget + SumUnit
                    End If
                Next

                tmpCTC = totalPlannedNetNetBudget
                'Costs on net net
                For Each TmpCost As Trinity.cCost In Campaign.Costs
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
                    totalPlannedNetNetBudget = totalPlannedNetBudget * (1 - ch.AgencyCommission)
                    tmpPrintNetNet = totalPlannedNetNetBudget
                End If
            End If


            Dim averageCPP As Integer = 0
            If totalTRPForChannel <> 0                
                averageCPP = totalNet / totalTRPForChannel
            End If

            .Cells(currentRow, 1).Value = ch.ChannelName 'Kanal
            .Cells(currentRow, 6).Value = filmCode
            .Cells(currentRow, 9).Value += totalTRPForChannel
            .Cells(currentRow, 10).Value = netCPP '"CPP act"
            .Cells(currentRow, 11).Value += totalGrossCost
            .Cells(currentRow, 12).Value = effDisc '"Discount in %"
            .Cells(currentRow, 13).Value += totalNet '"Net cost"
            .Cells(currentRow, 14).Value = ""'"Commission"
            .Cells(currentRow, 15).Value += totalNetNet'"Net Net cost"     
            .Cells(currentRow, 16).Value = ""'"Service fee"
            .Cells(currentRow, 17).Value = ""'"CTC"
            .Cells(currentRow, 18).Value = ""'"Comments"
            .Cells(currentRow, 19).Value = ""'"Events"
        End With
    End Sub
    Sub filterData1()
        'Print data for sheet 1
        With _sheet1
            tmpRow = 2            
            For each ch As cChannel In Campaign.Channels
                For each bt As cbookingtype In ch.BookingTypes
                    If bt.BookIt
                        
                        .Cells(tmpRow, 2).Value = Campaign.Client
                        .Cells(tmpRow, 3).Value = Date.FromOADate(Campaign.StartDate)
                        .Cells(tmpRow, 4).Value = Date.FromOADate(Campaign.EndDate)
                        .Cells(tmpRow, 5).Value = "" '"Material date" 'Struna i
                        .Cells(tmpRow, 7).Value = "" '"Target" 'Struna i
                        .Cells(tmpRow, 8).Value = "" '"Objective" 'Strunta i
            
                        printData1(ch, bt, tmpRow)
                    End If
                Next
                If .Cells(tmpRow, 9).Value IsNot nothing
                    tmpRow = tmpRow + 1
                End If
            Next
        End With
        _sheet1.Range("B" & 39 & ":M" & 48).Numberformat = "0.0"
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
