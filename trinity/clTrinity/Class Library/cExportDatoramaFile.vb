Imports clTrinity.Trinity

Public Class cExportDatoramaFile
    
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
    Dim _listOfWeeks As New List(Of cWeek)
    Dim _listOfDays As New List(Of DayOfWeek)
    Dim d As Date

    'Dim _campaign As Trinity.cKampanj

    Public Sub exportDatoramaFile(Optional ByVal tempBundleTV4 As Boolean = False, Optional ByVal tempBundleMTG As Boolean = False, Optional ByVal tempBundleMTGSpecial As Boolean = False, Optional ByVal tempBundleSBS As Boolean = False, Optional ByVal tempBundleFOX As Boolean = False, Optional ByVal tempBundleCMORE As Boolean = False, Optional ByVal tempBundleDisney As Boolean = False, Optional ByVal temBundleTNT As Boolean = False, Optional ByVal useOwnCommission As Boolean = False, Optional ByVal useOwnCommissionAmount As Decimal = 0, Optional ByVal tmpPrintExportAsCampaign As Boolean = False)

        If useOwnCommission Then
            ownCommission = True
            ownCommissionAmount = useOwnCommissionAmount / 100
        End If
        prepareEnivorment()
        checkAmountOfBookedChannels()
        checkBundling(tempBundleTV4, tempBundleMTG, tempBundleMTGSpecial, tempBundleSBS, tempBundleFOX, tempBundleCMORE, temBundleTNT, tempBundleDisney, tmpPrintExportAsCampaign)
        _wb = _excel.AddWorkbook
        _sheet1 = _wb.Sheets(1)
        _sheet1.Name = "Ark 1"
        _sheet2 = _wb.Sheets(2)
        _sheet2.Name = "Ark 2"
         _sheet3 = _wb.AddSheet(_sheet1)
        Dim worksheet3 As clTrinity.CultureSafeExcel.Worksheet = _sheet3
        _sheet3.Name ="Ark 3"


        getFilms()
        GetAmountOfFilmLengths()
        checkBefore2016()    
        GetWeeks()    
                            
        Dim value = Trinity.Helper.FormatDateForBooking(Campaign.StartDate)                
        With _sheet1
            PrintTable1()
            printHeader1()
            For i As Integer = 1 To 40
                .Columns(i).AutoFit()
            Next
        End With
        With _sheet2
            PrintTable2()
            For i As Integer = 1 To 40
                .Columns(i).AutoFit()
            Next
        End With
        

        With _sheet3           
            PrintTable3()
            For i As Integer = 1 To 40
                .Columns(i).AutoFit()
            Next
        End With
        _sheet1.Range("B" & 2 & ":M" & 200).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
        _sheet1.Range("B" & 2 & ":M" & 200).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
        _sheet2.Range("B" & 2 & ":M" & 200).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
        _sheet2.Range("B" & 2 & ":M" & 200).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
        _sheet3.Range("B" & 2 & ":M" & 200).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
        _sheet3.Range("B" & 2 & ":M" & 200).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
        resetEnivorment()
    End Sub
    Sub checkBefore2016
        If Campaign.StartDate <= 42735
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

    Public Function GetWeeks()
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

    'Public Shared Function DataRange(Start As DateTime, Thru As DateTime) As IEnumerable(Of Date)
    '    Return Enumerable.Range(0, (Thru.Date - Start.Date).Days + 1).Select(Function(i) Start.AddDays(i))
    'End Function
    Public Function checkCampaignForHoles
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


    Sub printHeader1()
        _sheet1.Range("B" & 2 & ":M" & 999).Font.Name = "Calibri"
        _sheet1.Range("B" & 2 & ":M" & 999).Font.Size = 10
        With _sheet1
            'Create textfields
            .Cells(1, 1).Value = "Campaign title"
            .Cells(1, 2).Value = "Area"
            .Cells(1, 3).Value = "Agency"
            .Cells(1, 4).Value = "Planner"
            .Cells(1, 5).Value = "Campaign type"
            .Cells(1, 6).Value = "Campaign type 2"
            .Cells(1, 7).Value = "Concern"
            .Cells(1, 8).Value = "Advertiser"
            .Cells(1, 9).Value = "Planned Budget incl. Fee"
            .Cells(1, 10).Value = "Marathonclient ID"
            .Cells(1, 11).Value = "Marathon client agreement"
            .Cells(1, 12).Value = "Product/Brand"

            .Cells(1, 13).Value = "Primary target"
            .Cells(1, 14).Value = "Secondary target"
            .Cells(1, 15).Value = "Eff. Frequency"            

            .Cells(1, 16).Value = "Start date"
            .Cells(1, 17).Value = "End date"
            .Cells(1, 18).Value = "Stations"

            '.Cells(1, 2).Value = "Frequency and reach"
            '.Range("B" & 24 & ":C" & 24).Merge()
            '.Cells(25, 2).Value = "Frequency"
            '.Cells(25, 3).Value = "Reach"

            '.Cells(37, 2).Value = "Station budget"
            '.Cells(38, 2).Value = "Network"
            '.Cells(38, 3).Value = "Station"
            '.Cells(38, 4).Value = "Gross"
            '.Cells(38, 5).Value = "Net"
            '.Cells(38, 6).Value = "Net net"
            '.Cells(38, 7).Value = "Fees"
            '.Cells(38, 8).Value = "Fond"
            '.Cells(38, 9).Value = "Other"
            '.Cells(38, 10).Value = "Ctc"
            '.Cells(38, 11).Value = "TRP"
            '.Cells(38, 12).Value = "Buying Target"

            'Print data
            For row As Integer = 2 To lastRowComboTable1 - 2
                .cells(row, 1).Value = Campaign.Name
                .cells(row, 2).Value = "NO"
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

                .cells(row, 3).Value = userCompany
                .cells(row, 4).Value = Campaign.Planner
                .cells(row, 5).Value = "TV Campaign"
                .cells(row, 6).Value = "Adults"
                .cells(row, 7).Value = Campaign.Client
                .cells(row, 8).Value = Campaign.Client

                .cells(row, 9).Value = Campaign.PlannedTotCTC.ToString()
                .cells(row, 10).Value = Campaign.ClientID.ToString()
                .cells(row, 11).Value = Campaign.ContractID.ToString()
                .cells(row, 12).Value = Campaign.Product
            
                Dim tmpMainTarget As String = translateTargetName(Campaign.MainTarget.TargetName)
                'Dim tmpSecondTarget As String = translateTargetName(Campaign.SecondaryTarget.TargetName)
                Dim tmpSecondTarget As String = translateTargetName(Campaign.MainTarget.TargetName)
                .cells(row, 13).Value = tmpMainTarget
                .cells(row, 14).Value = tmpSecondTarget
                .cells(row, 15).Value = (Campaign.FrequencyFocus + 1) & "+"

                .cells(row, 16).Value = Trinity.Helper.FormatDateForBooking(Campaign.StartDate)
                .Cells(row, 17).Value = Trinity.Helper.FormatDateForBooking(Campaign.EndDate)
                '.Cells(row, 18).Value = Trinity.
            Next            
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

    Sub printHeader3(Optional ByVal tmpCol As Integer = 0, Optional ByVal tmpRow As Integer = 0)
        _sheet1.Range("B" & 2 & ":M" & 999).Font.Name = "Calibri"
        _sheet1.Range("B" & 2 & ":M" & 999).Font.Size = 10
        With _sheet3
            'Create textfields
            .Cells(tmpRow, 1).Value = "Campaign title"
            .Cells(tmpRow, 2).Value = "Area"
            .Cells(tmpRow, 3).Value = "Agency"
            .Cells(tmpRow, 4).Value = "Planner"
            .Cells(tmpRow, 5).Value = "Campaign type"
            .Cells(tmpRow, 6).Value = "Campaign type 2"
            .Cells(tmpRow, 7).Value = "Concern"
            .Cells(tmpRow, 8).Value = "Advertiser"
            .Cells(tmpRow, 9).Value = "Planned Budget incl. Fee"
            .Cells(tmpRow, 10).Value = "Marathonclient ID"
            .Cells(tmpRow, 11).Value = "Marathon client agreement"
            .Cells(tmpRow, 12).Value = "Product/Brand"

            .Cells(tmpRow, 13).Value = "Primary target"
            .Cells(tmpRow, 14).Value = "Secondary target"
            .Cells(tmpRow, 15).Value = "Eff. Frequency"            

            .Cells(tmpRow, 16).Value = "Start date"
            .Cells(tmpRow, 17).Value = "End date"

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
    Sub HelperTable1(ByVal tmpChan As Trinity.cChannel, ByVal tmpB As Trinity.cBookingType, ByVal tmpGroupName As String, ByVal bundle As Boolean, ByVal row As Integer, ByVal tmpChannelName As String, Optional ByVal savedRow As Integer = 0, Optional ByVal printExportAsCampaign As Boolean = False, Optional ByVal column As Integer = 0, Optional ByVal week As Trinity.cWeek = Nothing)

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
            'If Not .Cells(savedRow, column - 1) Is Nothing Then
            '.Cells(savedRow - 1, column - 1).Value = "Week"
            'End If

            If Not .Cells(savedRow, column) Is Nothing Then
                .Cells(savedRow - 1, column).Value = "Stations"
            End If
            If tmpChan.AdEdgeNames <> "" Then
                stationChannels += tmpChan.AdEdgeNames
            Else
                stationChannels += tmpChan.ChannelName + "; "
            End If
            .Cells(row, column).Value += stationChannels
            If .Cells(savedRow - 1, column + 1).Value Is Nothing Then
                .Cells(savedRow - 1, column + 1).Value = "Spotlength"
            End If
            If .Cells(row, column + 1).Value Is Nothing Then

                For i As Integer = 0 To _amountOfFilmLength.Count - 1
                    .Cells(row, column + 1).Value += _amountOfFilmLength(i).ToString() + " sec;"
                Next
            End If

            If .Cells(savedRow - 1, column + 2).Value Is Nothing Then
                .Cells(savedRow - 1, column + 2).Value = "Spot Title"
            End If
            If .Cells(row, column + 2).Value Is Nothing Then
                For Each tmpf As Trinity.cFilm In listOfFilms
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

            'week.Name used to print current week in the export

            'If .Cells(savedRow - 1, column + 3).Value Is Nothing Then
            .Cells(savedRow - 1, column + 3).Value = "Week"
            'End If
            .Cells(row, column + 3).Value = week.Name

            If .Cells(savedRow - 1, column + 4).Value Is Nothing Then
                .Cells(savedRow - 1, column + 4).Value = "Buying target: TRP pr. station, pr. spotlength"
            End If
            .Cells(row, column + 4).Value += Math.Round(totalTRPForBT, 2)

            If .Cells(savedRow - 1, column + 5).Value Is Nothing Then
                .Cells(savedRow - 1, column + 5).Value = "Primary target: TRP pr. station, pr. spotlength"
            End If
            .Cells(row, column + 5).Value += Math.Round(totalTRPForPT, 2)
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

    Sub PrintTable1()
        'Print the first table for channels
        With _sheet1
            '.Range("B" & 37 & ":L" & 37).Interior.Color = RGB(0, 37, 110)
            '.Range("B" & 37 & ":L" & 37).Font.Color = RGB(255, 255, 255)
            '.Range("B" & 38 & ":L" & 38).Interior.Color = RGB(0, 137, 178)
            '.Range("B" & 38 & ":L" & 38).Font.Color = RGB(255, 255, 255)


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

            Dim firstDataRow As Integer = 2
            Dim firstCol As Integer = 18
            Dim sameRow As Integer = 1


            Dim tmpChannelNameInputForCheckBundling As String = ""

            tmpRow = firstDataRow

            'Added 2018-04-26. To make it possible to print current week In HelperTable1
            'For Each loop of _listOfWeeks 

            If (_printExportAsCampaign = False) Or (Campaign.Combinations.Count > 0 And _printExportAsCampaign) Or (Campaign.Combinations.Count < 1 And _printExportAsCampaign) Then
                For Each tmpWeek As Trinity.cWeek In _listOfWeeks

                    For Each tmpChan As Trinity.cChannel In Campaign.Channels
                        For Each tmpBook As Trinity.cBookingType In tmpChan.BookingTypes
                            For i As Integer = 1 To tmpBook.Weeks.Count
                                If tmpWeek.Name = tmpBook.Weeks(i).Name And tmpBook.BookIt Then
                                    HelperTable1(tmpChan, tmpBook, "", False, tmpRow, tmpChan.ChannelName, firstDataRow, False, firstCol, tmpWeek)
                                    tmpRow += 1
                                End If
                            Next
                        Next
                    Next
                Next
            End If
            lastRowComboTable1 = tmpRow + 1
        End With
        '_sheet.Range("B" & 39 & ":M" & 48).Numberformat = "0.0"
    End Sub
    Sub HelperTable2(ByVal tmpChan As Trinity.cChannel, ByVal tmpB As Trinity.cBookingType, ByVal tmpGroupName As String, ByVal bundle As Boolean, ByVal row As Integer, ByVal tmpChannelName As String, Optional ByVal savedRow As Integer = 0, Optional ByVal printExportAsCampaign As Boolean = False, Optional ByVal tmpCol As Integer = 0)

        Dim tmpBook As Trinity.cBookingType = tmpB
        Dim tmpBundle = bundle

        Dim stationChannels = ""

        Dim totalPlannedGrossBudget As Integer = 0
        Dim totalPlannedNetBudget As Integer = 0

        Dim listOfSpots As New List(Of Trinity.cBookedSpot)

        Dim totalTRPForBT As Decimal = 0

        With _sheet2
            If groupName = "" Then
                    'Station
                    If .Cells(row, tmpCol).Value Is Nothing Then
                        .Cells(row, tmpCol).Value += tmpChan.AdEdgeNames + " ;"
                    ElseIf Not .Cells(row, tmpCol).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                        .Cells(row, tmpCol).Value += tmpChan.AdEdgeNames.ToString() + " ;"
                    End If   
                    'Network
                    If CheckChannel(tmpChan.ChannelName) = "DNN"
                        If .Cells(row, tmpCol + 1).Value Is Nothing
                            .Cells(row, tmpCol + 1).Value += "Discovery Networks Norway" + "; "
                        ElseIf not .Cells(row, tmpCol + 1).Value.ToString().Contains("Discovery Networks Norway")
                            .Cells(row, tmpCol + 1).Value += "Discovery Networks Norway" + "; "
                        End If
                    ElseIf CheckChannel(tmpChan.ChannelName) = "TV2"
                        If .Cells(row, tmpCol + 1).Value Is Nothing
                            .Cells(row, tmpCol + 1).Value += "TV2 Group" + "; "
                        ElseIf not .Cells(row, tmpCol + 1).Value.ToString().Contains("TV2 Group")
                            .Cells(row, tmpCol + 1).Value += "TV2 Group" + "; "
                        End If
                    ElseIf CheckChannel(tmpChan.ChannelName) = "MTG"                        
                        If .Cells(row, tmpCol + 1).Value Is Nothing
                            .Cells(row, tmpCol + 1).Value += "MTG" + "; "
                        ElseIf not .Cells(row, tmpCol + 1).Value.ToString().Contains("MTG")
                            .Cells(row, tmpCol + 1).Value += "MTG" + "; "
                        End If
                    ElseIf CheckChannel(tmpChan.ChannelName) = "FOX"                        
                        If .Cells(row, tmpCol + 1).Value Is Nothing
                            .Cells(row, tmpCol + 1).Value += "FOX" + "; "
                        ElseIf not .Cells(row, tmpCol + 1).Value.ToString().Contains("FOX")
                            .Cells(row, tmpCol + 1).Value += "FOX" + "; "
                        End If
                    End If                                  
                Else
                    
                End If
            
        End With
    End Sub
    Sub PrintTable2()
        'Dim filmColumnArray As String() = {":B", ":C", ":D", ":E", ":F", ":G", ":H", ":I", ":J", ":K", ":L", ":M", ":N", ":O", ":P", ":Q", ":R", ":S", ":T", ":U", ":V", ":W", ":X", ":Y", ":Z", ":AA", ":AB", ":AC", ":AD"}
        'Dim columnValue As String = filmColumnArray.GetValue(1 + amountOfWeeks)
        Dim currentRow As Integer = 2
        Dim currentCol As Integer = 4
        printHeader2(currentCol)
        For each ch As Trinity.cChannel In Campaign.Channels
            For each bt As Trinity.cBookingType In ch.BookingTypes
                If bt.BookIt
                    HelperTable2(ch, bt, "", False, currentRow, ch.ChannelName, currentRow, False, 22)
                End If
            Next
        Next
        'currentRow = currentRow + 1
        
    End Sub

    Sub helperTable3(ByVal tmpChan As Trinity.cChannel, ByVal tmpB As Trinity.cBookingType, ByVal tmpGroupName As String, ByVal bundle As Boolean, ByVal row As Integer, ByVal tmpChannelName As String, Optional ByVal savedRow As Integer = 0, Optional ByVal printExportAsCampaign As Boolean = False, Optional ByVal col As Integer = 0, Optional ByVal week As Trinity.cWeek = Nothing)

        Dim tmpBook As Trinity.cBookingType = tmpB
        Dim tmpBundle = bundle

        Dim stationChannels = ""

        Dim totalPlannedGrossBudget As Integer = 0
        Dim totalPlannedNetBudget As Integer = 0

        Dim listOfSpots As New List(Of Trinity.cBookedSpot)

        Dim totalTRPForBT As Decimal = 0
        Dim totalTRPForPT As Decimal = 0

        Dim target As String = ""

        With _sheet3
            Dim valueTest = .Cells(savedRow, 2).Value

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

            If Not printExportAsCampaign Then
                If groupName = "" Then
                    'Station
                    If .Cells(row, col).Value Is Nothing Then
                        .Cells(row, col).Value += tmpChan.AdEdgeNames + " ;"
                    ElseIf Not .Cells(row, col).Value.ToString.Contains(tmpChan.AdEdgeNames) Then
                        .Cells(row, col).Value += tmpChan.AdEdgeNames.ToString() + " ;"
                    End If   
                    'Network
                    If CheckChannel(tmpChan.ChannelName) = "DNN"
                        If .Cells(row, col + 1).Value Is Nothing
                            .Cells(row, col + 1).Value += "Discovery Networks Norway" + "; "
                        ElseIf not .Cells(row, col + 1).Value.ToString().Contains("Discovery Networks Norway")
                            .Cells(row, col + 1).Value += "Discovery Networks Norway" + "; "
                        End If
                    ElseIf CheckChannel(tmpChan.ChannelName) = "TV2"
                        If .Cells(row, col + 1).Value Is Nothing
                            .Cells(row, col + 1).Value += "TV2 Group" + "; "
                        ElseIf not .Cells(row, col + 1).Value.ToString().Contains("TV2 Group")
                            .Cells(row, col + 1).Value += "TV2 Group" + "; "
                        End If
                    ElseIf CheckChannel(tmpChan.ChannelName) = "MTG"                        
                        If .Cells(row, col + 1).Value Is Nothing
                            .Cells(row, col + 1).Value += "MTG" + "; "
                        ElseIf not .Cells(row, col + 1).Value.ToString().Contains("MTG")
                            .Cells(row, col + 1).Value += "MTG" + "; "
                        End If
                    ElseIf CheckChannel(tmpChan.ChannelName) = "FOX"                        
                        If .Cells(row, col + 1).Value Is Nothing
                            .Cells(row, col + 1).Value += "FOX" + "; "
                        ElseIf not .Cells(row, col + 1).Value.ToString().Contains("FOX")
                            .Cells(row, col + 1).Value += "FOX" + "; "
                        End If
                    ElseIf CheckChannel(tmpChan.ChannelName) = "Matkanalen"                        
                        If .Cells(row, col + 1).Value Is Nothing
                            .Cells(row, col + 1).Value += "Matkanalen" + "; "
                        ElseIf not .Cells(row, col + 1).Value.ToString().Contains("Matkanalen")
                            .Cells(row, col + 1).Value += "Matkanalen" + "; "
                        End If
                    End If                                  
                Else
                    
                End If
            Else
            End If

            
            'Print data
            .Cells(row, 1).Value = Campaign.Name
            .Cells(row, 2).Value = "NO"
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

            .Cells(row, 3).Value = userCompany
            .Cells(row, 4).Value = Campaign.Planner
            .Cells(row, 5).Value = "TV Campaign"
            .Cells(row, 6).Value = "Adults"
            .Cells(row, 7).Value = Campaign.Client
            .Cells(row, 8).Value = Campaign.Client

            .Cells(row, 9).Value = Campaign.PlannedTotCTC.ToString()
            .Cells(row, 10).Value = Campaign.ClientID.ToString()
            .Cells(row, 11).Value = Campaign.ContractID.ToString()
            .Cells(row, 12).Value = Campaign.Product
            
            Dim tmpMainTarget As String = translateTargetName(Campaign.MainTarget.TargetName)

            Dim tmpSecondTarget As String = translateTargetName(Campaign.MainTarget.TargetName)
            .Cells(row, 13).Value = tmpMainTarget
            .Cells(row, 14).Value = tmpSecondTarget
            .Cells(row, 15).Value = (Campaign.FrequencyFocus + 1) & "+"

            .Cells(row, 16).Value = Trinity.Helper.FormatDateForBooking(Campaign.StartDate)
            .Cells(row, 17).Value = Trinity.Helper.FormatDateForBooking(Campaign.EndDate)
            .Cells(1, 18).Value = "Station"
            .Cells(1, 19).Value = "Network"
            
            For i As Integer = 1 To tmpB.Weeks.Count
                If tmpB.Weeks(i).Name = week.Name                    
                    totalTRPForBT = tmpB.Weeks(i).TRPBuyingTarget
                    totalTRPForPT = tmpB.Weeks(i).TRP
                End If
            Next

            .Cells(1, col + 2).Value = "Week"
            .Cells(row, col + 2).Value = week.Name
            .Cells(1, col + 3).Value = "Primary target: TRP pr. station, pr. week"
            .Cells(row, col + 3).Value += Math.Round(totalTRPForPT, 2)
            .Cells(1, col + 4).Value = "Buying target: TRP pr. station, pr. week"
            .Cells(row, col + 4).Value += Math.Round(totalTRPForBT, 2)
        End With
    End Sub


    Sub PrintTable3()

        Dim currentRow As Integer = 2
        Dim currentCol = 18
        
        printHeader3(currentCol, currentRow - 1)
        
        With _sheet3

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
            
            For each tmpWeek As Trinity.cWeek in _listOfWeeks
                For Each tmpChan As Trinity.cChannel In Campaign.Channels
                    For Each tmpBook As Trinity.cBookingType In tmpChan.BookingTypes
                        For i As Integer = 1 To tmpBook.Weeks.Count
                            If tmpWeek.Name = tmpBook.Weeks(i).Name And tmpBook.BookIt
                                helperTable3(tmpChan, tmpBook, "", _BundleDisney, currentRow, tmpChan.ChannelName, currentRow, False, currentCol, tmpWeek)
                            End If
                        Next
                    Next
                Next
                currentRow = currentRow + 1
            Next              
        End With
    End Sub

    Sub HelperTable4(ByVal tmpChan As Trinity.cChannel, ByVal tmpB As Trinity.cBookingType, ByVal tmpGroupName As String, ByVal bundle As Boolean, ByVal row As Integer, ByVal tmpChannelName As String, Optional ByVal savedRow As Integer = 0, Optional rowheader As Integer = 0, Optional ByVal printExportAsCampaign As Boolean = False)

        Dim tmpBook As Trinity.cBookingType = tmpB
        Dim tmpBundle = bundle

        Dim stationChannels = ""

        Dim totalPlannedGrossBudget As Integer = 0
        Dim totalPlannedNetBudget As Integer = 0

        Dim listOfSpots As New List(Of Trinity.cBookedSpot)

        Dim target As String = ""

        With _sheet1
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
                        .Cells(row, 2).Value = "MTG TV"
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
                    .Cells(row, 2).Value = "MTG Combo"
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
                        For Each tmpWeek As Trinity.cWeek In tmpB.Weeks
                            trpForShare = tmpWeek.TRP
                            If (tmpF.Filmcode <> "") Then
                                If tmpWeek.Films(tmpF.Name) IsNot Nothing
                                    tmpShare = tmpWeek.Films(tmpF.Name).Share()
                                    tmpFilm = tmpWeek.Films(tmpF.Name).FilmLength
                                End if
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
            Next
        End With
    End Sub
    Sub PrintTable4()
        Dim filmColumnArray As String() = {":B", ":C", ":D", ":E", ":F", ":G", ":H", ":I", ":J", ":K", ":L", ":M", ":N", ":O", ":P", ":Q", ":R", ":S", ":T", ":U", ":V", ":W", ":X", ":Y", ":Z", ":AA", ":AB", ":AC", ":AD"}
        Dim columnValue As String
        columnValue = filmColumnArray.GetValue(amountOfFilms)

        Dim currentRow As Integer = lastRowComboTable3
        Dim rowHeader = currentRow
        With _sheet1
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
                        For Each tmpB As cBookingType In _
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

        With _sheet1
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
                                            HelperTable4(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)
                                        ElseIf _BundleTV4 Then
                                            HelperTable4(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)
                                        Else
                                            rowTV4 = tmpRow
                                            tv4String = tmpChan.ChannelName
                                            HelperTable4(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)
                                        End If
                                    ElseIf (rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Or (rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific Or tmpBook.isrbs) And tmpChan.ChannelName = channelName) Then
                                        rowTV4 = rowTV4

                                        tv4String = tmpChan.ChannelName
                                        HelperTable4(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)

                                    ElseIf rowOnlyTV4 <> 0 And tmpChan.ChannelName = "TV4" Or tmpChan.ChannelName = "TV4" And _BundleTV4 Then
                                        If rowOnlyTV4 = 0 Then
                                            rowOnlyTV4 = tmpRow
                                            tmpRow = tmpRow + 1
                                        End If

                                        tv4String = tmpChan.ChannelName
                                        HelperTable4(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowOnlyTV4, rowHeader)
                                    Else
                                        rowTV4 = tmpRow
                                        tmpRow = tmpRow + 1

                                        tv4String = tmpChan.ChannelName
                                        HelperTable4(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)
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

                                            HelperTable4(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTGSpecial, rowHeader)
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
                                            helperTable4(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG, rowHeader)
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
                                        HelperTable4(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG, rowHeader)
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
                                    HelperTable4(tmpChan, tmpBook, "SBS", _BundleSBS, tmpRow, tmpChan.ChannelName, rowSBS, rowHeader)
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
                                    HelperTable4(tmpChan, tmpBook, "FOX", _BundleFOX, tmpRow, tmpChan.ChannelName, rowFOX, rowHeader)
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
                                    HelperTable4(tmpChan, tmpBook, "CMORE", _BundleCMORE, tmpRow, tmpChan.ChannelName, rowCMORE, rowHeader)
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
                                    HelperTable4(tmpChan, tmpBook, "Cartoon", _BundleCartoon, tmpRow, tmpChan.ChannelName, rowCartoon, rowHeader)
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
                                    HelperTable4(tmpChan, tmpBook, "Disney", _BundleDisney, tmpRow, tmpChan.ChannelName, rowDisney, rowHeader)
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
                                    HelperTable4(tmpChan, tmpBook, "TNT", _BundleTNT, tmpRow, tmpChan.ChannelName, rowTNT, rowHeader)
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
    Sub helperTable5(ByVal tmpChan As Trinity.cChannel, ByVal tmpB As Trinity.cBookingType, ByVal tmpGroupName As String, ByVal bundle As Boolean, ByVal row As Integer, ByVal tmpChannelName As String, Optional ByVal savedRow As Integer = 0, Optional rowheader As Integer = 0, Optional ByVal printExportAsCampaign As Boolean = False)

        Dim tmpBook As Trinity.cBookingType = tmpB
        Dim tmpBundle = bundle

        Dim stationChannels = ""

        Dim totalPlannedGrossBudget As Integer = 0
        Dim totalPlannedNetBudget As Integer = 0

        Dim listOfSpots As New List(Of Trinity.cBookedSpot)

        Dim target As String = ""

        With _sheet1
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
                        .Cells(row, 2).Value = "MTG TV"
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
                    .Cells(row, 2).Value = "MTG Combo"
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
        With _sheet1
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
                        For Each tmpB As cBookingType In _
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
                            helperTable5(tmpChan, tmpB, tmpChannelNameInputForCheckBundling, False, tmpRow,
                                         c.ChannelName, rowCombination, rowHeader, True)
                            Exit For
                        Next
                    Next
                Next
                tmpRow = tmpRow + 1
            Next
            ExportedAsCampaignCombination = True
        End If
        With _sheet1
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
                                            helperTable5(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)
                                        ElseIf _BundleTV4 Then
                                            helperTable5(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)
                                        Else
                                            rowTV4 = tmpRow
                                            tv4String = tmpChan.ChannelName
                                            helperTable5(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)
                                        End If
                                    ElseIf (rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific)) And (tmpChan.ChannelName = channelName) Or (_printExportAsCampaign And rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific) And tmpChan.ChannelName = channelName) Or (rowTV4 <> 0 And (tmpBook.IsCompensation Or tmpBook.IsSpecific Or tmpBook.isrbs) And tmpChan.ChannelName = channelName) Then
                                        rowTV4 = rowTV4

                                        tv4String = tmpChan.ChannelName
                                        helperTable5(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)

                                    ElseIf rowOnlyTV4 <> 0 And tmpChan.ChannelName = "TV4" Or tmpChan.ChannelName = "TV4" And _BundleTV4 Then
                                        If rowOnlyTV4 = 0 Then
                                            rowOnlyTV4 = tmpRow
                                            tmpRow = tmpRow + 1
                                        End If

                                        tv4String = tmpChan.ChannelName
                                        helperTable5(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowOnlyTV4, rowHeader)
                                    Else
                                        rowTV4 = tmpRow
                                        tmpRow = tmpRow + 1

                                        tv4String = tmpChan.ChannelName
                                        helperTable5(tmpChan, tmpBook, "TV4", _BundleTV4, tmpRow, tmpChan.ChannelName, rowTV4, rowHeader)
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

                                            helperTable5(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTGSpecial, rowHeader)
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
                                            helperTable5(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG, rowHeader)
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
                                        helperTable5(tmpChan, tmpBook, "MTG", _BundleMTG, tmpRow, tmpChan.ChannelName, rowMTG, rowHeader)
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
                                    helperTable5(tmpChan, tmpBook, "SBS", _BundleSBS, tmpRow, tmpChan.ChannelName, rowSBS, rowHeader)
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
                                    helperTable5(tmpChan, tmpBook, "FOX", _BundleFOX, tmpRow, tmpChan.ChannelName, rowFOX, rowHeader)
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
                                    helperTable5(tmpChan, tmpBook, "CMORE", _BundleCMORE, tmpRow, tmpChan.ChannelName, rowCMORE, rowHeader)
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
                                    helperTable5(tmpChan, tmpBook, "Cartoon", _BundleCartoon, tmpRow, tmpChan.ChannelName, rowCartoon, rowHeader)
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
                                    helperTable5(tmpChan, tmpBook, "Disney", _BundleDisney, tmpRow, tmpChan.ChannelName, rowDisney, rowHeader)
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
                                    helperTable5(tmpChan, tmpBook, "TNT", _BundleTNT, tmpRow, tmpChan.ChannelName, rowTNT, rowHeader)
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
     Sub printTable6()

        Dim weekCol As String() = {":B", ":C", ":D", ":E", ":F", ":G", ":H", ":I", ":J", ":K", ":L", ":M", ":N", ":O", ":P", ":Q", ":R", ":S", ":T", ":U", ":V", ":W", ":X", ":Y", ":Z", ":AA", ":AB", ":AC", ":AD"}
        Dim columnValue As String = weekCol.GetValue(3 + amountOfWeeks)
        Dim currentRow As Integer = lastRowComboTable5
        Dim headerRow = 0
        With _sheet1
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
                With _sheet1
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
