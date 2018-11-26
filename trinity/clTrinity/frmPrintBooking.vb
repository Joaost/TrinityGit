Public Class frmPrintBooking
    'prints the booking so you can send it to the channel
    'Test
    Dim mnuLanguage As New Windows.Forms.ContextMenuStrip
    Dim mnuChannels As New Windows.Forms.ContextMenuStrip
    Dim mnuWeeks As New Windows.Forms.ContextMenuStrip
    Dim frmDetails As New frmDetails
    Dim _specialColumns As System.Windows.Forms.ListBox = Nothing

    Dim _defaultCheckBookingTypes As List(Of Trinity.cBookingType)

    Dim strPricelistUpdate As String = ""

    'Dim specificsColumns As Dictionary(Of String, Integer)


    Structure LanguageTag
        Public Abbrevation As String
        Public Path As String
    End Structure

    Private Sub frmPrintSpecifics_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim TmpWeek As Trinity.cWeek
        Dim LangIni As New Trinity.clsIni
        Dim File As String
        Dim i As Integer
        Dim BundledSales As Boolean
        Dim AlreadyAdded As New Collection

        For Each address As String In TrinitySettings.BillingAddresses
            cmbBillingAddress.Items.Add(address)
        Next
        'cmbBillingAddress.SelectedIndex = 0

        For Each address As String In TrinitySettings.OrderPlacerAddresses
            cmbOrderPlacer.Items.Add(address)
        Next
        'cmbOrderPlacer.SelectedIndex = 0

        cmbLogo.Items.Clear()
        cmbLogo.Items.Add("None")
        mnuLanguage.Items.Clear()
        For Each File In My.Computer.FileSystem.GetFiles(DataPath & "Logos\", FileIO.SearchOption.SearchAllSubDirectories, "*.bmp", "*.gif", "*.jpg")
            cmbLogo.Items.Add(My.Computer.FileSystem.GetName(File))
        Next
        LangIni.Create(TrinitySettings.ActiveDataPath & "language.ini")
        Dim TmpLang As LanguageTag
        TmpLang.Path = TrinitySettings.ActiveDataPath & "language.ini"
        TmpLang.Abbrevation = LangIni.Text("General", "Code")
        With mnuLanguage.Items.Add(LangIni.Text("General", "Name"))
            .Tag = TmpLang
            AddHandler .Click, AddressOf ChangeLanguage
        End With
        For i = 1 To TrinitySettings.AreaCount
            LangIni.Create(TrinitySettings.ActiveDataPath & TrinitySettings.Area(i) & "\language.ini")
            With DirectCast(mnuLanguage.Items.Add(LangIni.Text("General", "Name")), Windows.Forms.ToolStripMenuItem)
                Dim TmpLangTag As LanguageTag
                TmpLangTag.Path = TrinitySettings.ActiveDataPath & TrinitySettings.Area(i) & "\language.ini"
                TmpLangTag.Abbrevation = LangIni.Text("General", "Code")
                .Tag = TmpLangTag
                If TrinitySettings.Area(i) = Campaign.Area Then
                    .Checked = True
                    cmdLanguage.Tag = TmpLangTag.Path
                End If
                AddHandler .Click, AddressOf ChangeLanguage
            End With
        Next
        grdContacts.Rows.Clear()
        mnuChannels.Items.Clear()
        chkIncludePremiums.Enabled = False
        For Each TmpChan In Campaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                If TmpBT.BookIt AndAlso (TmpBT.Combination Is Nothing OrElse Not TmpBT.Combination.PrintAsOne) Then
                    BundledSales = False
                    'Deprecated - Use PrintAsOne instead
                    'If TmpChan.ConnectedChannel <> "" AndAlso _rbs Then
                    '    If Not Campaign.Channels(TmpChan.ConnectedChannel) Is Nothing AndAlso Not Campaign.Channels(TmpChan.ConnectedChannel).BookingTypes(TmpBT.Name) Is Nothing AndAlso Campaign.Channels(TmpChan.ConnectedChannel).BookingTypes(TmpBT.Name).BookIt Then
                    '        BundledSales = True
                    '    End If
                    'End If
                    If BundledSales Then
                        If Not AlreadyAdded.Contains(TmpChan.ChannelName & " " & TmpBT.Name) Then
                            With DirectCast(mnuChannels.Items.Add(TmpChan.ChannelName & "/" & TmpChan.ConnectedChannel & " Bundled"), Windows.Forms.ToolStripMenuItem)
                                .Checked = _defaultCheckBookingTypes Is Nothing OrElse _defaultCheckBookingTypes.Contains(TmpBT)
                                .Tag = TmpBT
                                AddHandler .Click, AddressOf ChangeChannel
                            End With
                            grdContacts.Rows.Add()
                            grdContacts.Rows(grdContacts.Rows.Count - 1).HeaderCell.Value = TmpChan.ChannelName & "/" & TmpChan.ConnectedChannel & " Bundled"
                            grdContacts.Rows(grdContacts.Rows.Count - 1).HeaderCell.Tag = TmpChan.ChannelName
                            Dim strContacts As String = TrinitySettings.ChannelContact(TmpChan.Shortname)
                            Dim strAdd() As String = strContacts.Split("|")
                            For z As Integer = 0 To strAdd.Length - 1
                                DirectCast(grdContacts.Rows(grdContacts.Rows.Count - 1).Cells(0), ExtendedComboBoxCellWrite).Items.add(strAdd(z))
                            Next
                            grdContacts.Rows(grdContacts.Rows.Count - 1).Tag = strAdd
                            If Not AlreadyAdded.Contains(Campaign.Channels(TmpChan.ConnectedChannel).ChannelName & " " & Campaign.Channels(TmpChan.ConnectedChannel).BookingTypes(TmpBT.Name).Name) Then
                                AlreadyAdded.Add(True, Campaign.Channels(TmpChan.ConnectedChannel).ChannelName & " " & Campaign.Channels(TmpChan.ConnectedChannel).BookingTypes(TmpBT.Name).Name)
                            End If
                        End If
                    Else
                        With DirectCast(mnuChannels.Items.Add(TmpChan.ChannelName & " " & TmpBT.Name), Windows.Forms.ToolStripMenuItem)
                            .Checked = _defaultCheckBookingTypes Is Nothing OrElse _defaultCheckBookingTypes.Contains(TmpBT)
                            .Tag = TmpBT
                            AddHandler .Click, AddressOf ChangeChannel
                        End With
                        grdContacts.Rows.Add()
                        grdContacts.Rows(grdContacts.Rows.Count - 1).HeaderCell.Value = TmpChan.Shortname & " " & TmpBT.Shortname
                        grdContacts.Rows(grdContacts.Rows.Count - 1).HeaderCell.Tag = TmpChan.Shortname
                        Dim strContacts As String = TrinitySettings.ChannelContact(TmpChan.Shortname)
                        Dim strAdd() As String = strContacts.Split("|")
                        For z As Integer = 0 To strAdd.Length - 1
                            DirectCast(grdContacts.Rows(grdContacts.Rows.Count - 1).Cells(0), ExtendedComboBoxCellWrite).Items.add(strAdd(z))
                        Next
                        grdContacts.Rows(grdContacts.Rows.Count - 1).Tag = strAdd
                    End If
                End If
            Next
        Next

        'add combinations to the menu
        cmdColumns.Visible = False
        For Each c As Trinity.cCombination In Campaign.Combinations
            If c.PrintAsOne Then
                With DirectCast(mnuChannels.Items.Add(c.Name), Windows.Forms.ToolStripMenuItem)
                    .Checked = True
                    .Tag = c
                    AddHandler .Click, AddressOf ChangeChannel
                End With
                grdContacts.Rows.Add()
                grdContacts.Rows(grdContacts.Rows.Count - 1).HeaderCell.Value = c.Name
                grdContacts.Rows(grdContacts.Rows.Count - 1).HeaderCell.Tag = c.Name
                Dim strContacts As String = TrinitySettings.ChannelContact(c.Name)
                Dim strAdd() As String = strContacts.Split("|")
                For z As Integer = 0 To strAdd.Length - 1
                    DirectCast(grdContacts.Rows(grdContacts.Rows.Count - 1).Cells(0), ExtendedComboBoxCellWrite).Items.add(strAdd(z))
                Next
                grdContacts.Rows(grdContacts.Rows.Count - 1).Tag = strAdd
                If (From _cc As Trinity.cCombinationChannel In c.Relations Select _cc Where _cc.Bookingtype.IsPremium).Count > 0 Then
                    chkIncludePremiums.Enabled = True
                    chkIncludePremiums.Checked = True
                End If
            End If
        Next

        With DirectCast(mnuChannels.Items.Add("Invert"), Windows.Forms.ToolStripMenuItem)
            .Checked = False
            .Tag = "Invert"
            AddHandler .Click, AddressOf ChangeChannel
        End With

        mnuWeeks.Items.Clear()
        For Each TmpWeek In Campaign.Channels(1).BookingTypes(1).Weeks
            With DirectCast(mnuWeeks.Items.Add(TmpWeek.Name), Windows.Forms.ToolStripMenuItem)
                .Tag = TmpWeek
                .Checked = True
                .Name = "mnuWeek" & TmpWeek.Name
                AddHandler .Click, AddressOf ChangeWeek
            End With
        Next

        With DirectCast(mnuWeeks.Items.Add("Invert"), Windows.Forms.ToolStripMenuItem)
            .Checked = False
            .Tag = "Invert"
            AddHandler .Click, AddressOf ChangeWeek
        End With

        cmbColorScheme.Items.Clear()
        If Campaign.xmlColorSchemes.Count = 0 Then
            For i = 1 To TrinitySettings.ColorSchemeCount
                cmbColorScheme.Items.Add(TrinitySettings.ColorScheme(i))
            Next
        Else
            cmbColorScheme.DisplayMember = "name"
            For Each Scheme As Trinity.cColorScheme In Campaign.xmlColorSchemes
                cmbColorScheme.Items.Add(Scheme)
            Next
        End If

        Try
            cmbColorScheme.SelectedIndex = TrinitySettings.DefaultColorScheme
            cmbLogo.SelectedIndex = TrinitySettings.DefaultLogo
            cmbAlign.SelectedIndex = TrinitySettings.LogoAlignment
        Catch
            cmbLogo.SelectedIndex = 0
            cmbAlign.SelectedIndex = 0
            cmbColorScheme.SelectedIndex = 0
        End Try

        cmdLanguage.Image = frmMain.ilsBig.Images(Campaign.Area)


        grdContacts.RowHeadersWidth = 100
        grdContacts.AutoResizeRowHeadersWidth(Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders)
        grdContacts.AutoResizeColumns(Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells)
        grdContacts.Columns(0).Width = 140
        'grdContacts.Width = grdContacts.PreferredSize.Width
        grpContacts.Width = grdContacts.PreferredSize.Width + 40
        Me.Width = grpContacts.Right + 10
        'grdContacts.Columns(0).Width = grdContacts.Width - grdContacts.RowHeadersWidth - 20

        'clears the grid
        frmDetails.grdDetails.Rows.Clear()
        frmDetails.grdDetails.Rows.Add(100)
        frmDetails.grdDetails.ForeColor = Color.Black

        Dim check As New Trinity.cPricelistCheck(Campaign, Me)
        Dim Thread1 As New System.Threading.Thread(AddressOf check.checkPricelists)
        Thread1.IsBackground = True
        Thread1.Start()

        Dim _notAll As Boolean = False
        Dim _chanStr As String = ""
        For Each _mnu As System.Windows.Forms.ToolStripMenuItem In mnuChannels.Items
            If _mnu.Checked Then
                _chanStr &= _mnu.Text & ","
            Else
                _notAll = True
            End If
        Next
        If _notAll Then
            If _chanStr = "" Then
                lblChannels.Text = "<None>"
            Else
                lblChannels.Text = _chanStr.TrimEnd(",")
            End If
        Else
            lblChannels.Text = "All"
        End If

        'Dim Row As Integer = 1
        'lblOldPricelist.Visible = False
        'Dim bolChan As Boolean = False
        'For Each TmpChan In Campaign.Channels
        '    For Each TmpBT In TmpChan.BookingTypes
        '        If TmpBT.BookIt Then
        '            Dim TmpNewBT As New Trinity.cBookingType(Campaign)
        '            TmpNewBT.ParentChannel = TmpBT.ParentChannel
        '            TmpNewBT.Name = TmpBT.Name
        '            TmpNewBT.ReadPricelist()
        '            With TmpNewBT.Weeks.Add("")
        '                .StartDate = Campaign.StartDate
        '                .EndDate = Campaign.EndDate
        '            End With

        '            Dim TmpTarget As Trinity.cPricelistTarget = TmpBT.BuyingTarget
        '            Dim TmpTarget2 As Trinity.cPricelistTarget = TmpNewBT.Pricelist.Targets(TmpTarget.TargetName)
        '            If Not TmpTarget2 Is Nothing Then
        '                If Not TmpTarget.CheckTargetWReason(TmpTarget2) = "" Then
        '                    lblOldPricelist.Visible = True
        '                    If strPricelistUpdate = "" Then
        '                        strPricelistUpdate = TmpBT.ToString & ": " & vbCrLf & TmpTarget.CheckTargetWReason(TmpTarget2)
        '                    Else
        '                        If bolChan Then
        '                            strPricelistUpdate = strPricelistUpdate & ", " & vbCrLf & TmpTarget.CheckTargetWReason(TmpTarget2)
        '                        Else
        '                            strPricelistUpdate = strPricelistUpdate & " " & vbCrLf & TmpBT.ToString & ": " & vbCrLf & TmpTarget.CheckTargetWReason(TmpTarget2)
        '                        End If
        '                    End If
        '                End If
        '            End If
        '        End If
        '    Next
        'Next
    End Sub

    Public Sub setPricelistLabel(ByVal Errors As String)
        strPricelistUpdate = Errors

        If Errors = "" Then
            lblOldPricelist.Visible = False
        Else
            lblOldPricelist.Visible = True
        End If
        lblOldPricelist.Text = "The pricelists of this campaign are not all up to date"
    End Sub

    Private Sub cmdLanguage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLanguage.Click
        'changes language
        mnuLanguage.Show(cmdLanguage, 0, cmdLanguage.Height)
    End Sub

    Private Sub cmdChannels_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdChannels.Click
        'sets what channel(s)
        mnuChannels.Show(cmdChannels, 0, cmdChannels.Height)
    End Sub

    Private Sub cmdWeeks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdWeeks.Click
        'set what week(s)
        mnuWeeks.Show(cmdWeeks, 0, cmdWeeks.Height)
    End Sub

    Sub ChangeLanguage(ByVal sender As Object, ByVal e As EventArgs)
        Dim TmpMnu As Windows.Forms.ToolStripMenuItem

        cmdLanguage.Tag = sender.tag.path
        cmdLanguage.Image = frmMain.ilsBig.Images(sender.tag.abbrevation)

        For Each TmpMnu In mnuLanguage.Items
            TmpMnu.Checked = False
        Next
        sender.checked = True
    End Sub

    Sub ChangeChannel(ByVal sender As Object, ByVal e As EventArgs)
        Dim TmpChannelStr As String = ""
        Dim NotAll As Boolean
        Dim TmpMnu As Windows.Forms.ToolStripMenuItem

        If sender.tag.GetType.FullName = "System.String" AndAlso sender.tag = "Invert" Then
            For Each TmpMnu In mnuChannels.Items
                TmpMnu.Checked = Not TmpMnu.Checked
            Next
            sender.checked = True
        End If

        sender.checked = Not sender.checked

        NotAll = False
        For Each TmpMnu In mnuChannels.Items
            If TmpMnu.Checked Then
                TmpChannelStr = TmpChannelStr + TmpMnu.Text & ","
            Else
                NotAll = True
            End If
        Next
        If NotAll Then
            If TmpChannelStr = "" Then
                lblChannels.Text = "<None>"
            Else
                lblChannels.Text = TmpChannelStr.TrimEnd(",")
            End If
        Else
            lblChannels.Text = "All"
        End If

    End Sub

    Sub ChangeWeek(ByVal sender As Object, ByVal e As EventArgs)
        Dim TmpWeekStr As String = ""
        Dim NotAll As Boolean
        Dim TmpMnu As Windows.Forms.ToolStripMenuItem

        If sender.tag.GetType.FullName = "System.String" AndAlso sender.tag = "Invert" Then
            For Each TmpMnu In mnuWeeks.Items
                TmpMnu.Checked = Not TmpMnu.Checked
            Next
            sender.checked = True

        End If

        sender.checked = Not sender.checked

        NotAll = False
        For Each TmpMnu In mnuWeeks.Items
            If TmpMnu.Checked Then
                TmpWeekStr = TmpWeekStr & TmpMnu.Text & ","
            Else
                NotAll = True
            End If
        Next
        If NotAll Then
            If TmpWeekStr = "" Then
                lblWeeks.Text = ""
            Else
                lblWeeks.Text = TmpWeekStr.TrimEnd(",")
            End If
        Else
            lblWeeks.Text = "All"
        End If

    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        'when you click ok this sub is executed. It send the information to a excel sheet
        Dim LangIni As New Trinity.clsIni
        Dim Excel As CultureSafeExcel.Application
        Dim WB As Microsoft.Office.Interop.Excel.Workbook

        Dim i As Integer
        Dim x As Integer
        Dim s As Integer
        Dim Row As Integer
        Dim Scal As Single

        Dim Chan As String = Nothing
        Dim BT As String = Nothing
        Dim BTName As String = "RBS"
        Dim TmpStr As String

        Dim PeriodStr As String
        Dim Targ As String
        Dim UniSize As Integer
        Dim Gender As String
        Dim dp As Integer
        Dim TotTRP As Single
        Dim y As Integer
        Dim r As Integer
        Dim z As Integer
        Dim TopRow As Integer
        Dim RubrRange As String
        Dim TotBud As Decimal
        Dim w As Integer
        Dim FirstRow As Integer
        Dim q As Integer
        Dim CPP As Single
        Dim PrintedPremiums As New List(Of Trinity.cBookingType)

        On Error Resume Next

        Dim TmpMnuChan As Windows.Forms.ToolStripMenuItem

        If lblOldPricelist.Visible Then
            If Windows.Forms.MessageBox.Show("The pricelists on this campaign is not the same" & vbCrLf & "as those on your server." & vbCrLf & "This probably means a change has been" & vbCrLf & "made to the pricelist after the campaign was created." & vbCrLf & vbCrLf & "Are you sure you want to print bookings?" & vbCrLf & vbCrLf & "(To update pricelists choose Edit pricelist from the" & vbCrLf & "Settings menu, and click Update)", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Exclamation) <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If
        End If

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        For Each tmpChan As Trinity.cChannel In Campaign.Channels
            Dim A As String = tmpChan.ChannelName
            Dim B As String = tmpChan.ConnectedChannel
            If B <> "" Then
                If Not Campaign.Channels.Contains(B) Then
                    Windows.Forms.MessageBox.Show(A & " is connected to an improperly named channel - '" & B & "'")
                Else
                    If A <> Campaign.Channels(B).ConnectedChannel Then
                        Windows.Forms.MessageBox.Show(A & " is connected to " & B & ", but " & B & " is not connected to " & A & ". Please correct this in Define Channels")
                    End If
                End If

            End If
        Next

        If chkDefault.Checked Then
            TrinitySettings.DefaultLogo = cmbLogo.SelectedIndex
            TrinitySettings.DefaultColorScheme = cmbColorScheme.SelectedIndex
            TrinitySettings.LogoAlignment = cmbAlign.SelectedIndex
        End If

        Excel = New CultureSafeExcel.Application(False)
      
        LangIni.Create(cmdLanguage.Tag)

        Dim combination As Trinity.cCombination = Nothing

        i = 0

        For Each TmpMnuChan In mnuChannels.Items
            If TmpMnuChan.Checked Then
                WB = Excel.AddWorkbook

                WB.Colors(17) = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                WB.Colors(18) = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                WB.Colors(19) = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")

                If TmpMnuChan.Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                    combination = TmpMnuChan.Tag
                    GoTo goRBS
                Else
                    Chan = DirectCast(TmpMnuChan.Tag, Trinity.cBookingType).ParentChannel.ChannelName
                    BT = Mid(TmpMnuChan.Text, Len(Chan) + 2)
                    If BT.EndsWith("Bundled") Then
                        Chan = TmpMnuChan.Text.Substring(0, TmpMnuChan.Text.Length - 8)
                        BT = "Bundled"
                        BTName = TmpMnuChan.Tag.name
                    End If
                End If

                If Not BT = "Bundled" AndAlso Campaign.Channels(Chan).BookingTypes(BT).IsSpecific Then

                    'Specifics

                    For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                        If TmpSpot.Film Is Nothing Then
                            Windows.Forms.MessageBox.Show("One of the spots are booked with a film that is" & vbCrLf & "no longer a part of the campaign (" & TmpSpot.Filmcode & ")." & vbCrLf & "You need to replace that spot in order to print bookings.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                            WB.Close()
                            Excel.Quit()
                            Me.Cursor = Windows.Forms.Cursors.Default
                            Exit Sub
                        End If
                    Next
                    With WB.Sheets(1)
                        .Name = Chan & " " & BT
                        .AllCells.Font.Name = TrinitySettings.SchemeFont(cmbColorScheme.SelectedIndex + 1)
                        .AllCells.Font.Size = 9
                        .AllCells.Interior.Color = RGB(255, 255, 255)

                        'Excel.Visible = True
                        'Excel.Screenupdating = False


                        With .Cells(1, 2)
                            .Value = Chan & " " & BT & " " & LangIni.Text("Booking", "Booking") & " - " & Campaign.Name
                            .Font.Bold = True
                            .Font.Size = 14
                            .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                        End With

                        .Cells(3, 2).value = LangIni.Text("Booking", "To")
                        .Cells(3, 3) = grdContacts.Rows(i).Cells(0).Value
                        .Cells(4, 2) = LangIni.Text("Booking", "From")
                        .Cells(4, 3) = TrinitySettings.UserName
                        .Cells(5, 3) = TrinitySettings.UserEmail
                        .Cells(6, 3) = TrinitySettings.UserPhoneNr
                        .Cells(8, 2) = LangIni.Text("Booking", "Advertiser")
                        .Cells(8, 3) = Campaign.Client
                        .Cells(9, 2) = LangIni.Text("Booking", "Product")
                        .Cells(9, 3) = Campaign.Product
                        .Cells(10, 2) = LangIni.Text("Booking", "CampaignPeriod")
                        .Cells(10, 3) = Trinity.Helper.FormatDateForBooking(Date.FromOADate(Campaign.StartDate)) & " - " & Trinity.Helper.FormatDateForBooking(Date.FromOADate(Campaign.EndDate))
                        .Range("B3:B10").Font.Bold = True
                        With .Range("B3:E10")
                            .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                            .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                            For x = 7 To 10
                                With .Borders(x)
                                    .LineStyle = 1
                                    .Weight = -4138
                                    .ColorIndex = -4105
                                End With
                            Next
                        End With


                        If Campaign.Channels(Chan).BookingTypes(BT).PrintBookingCode Then
                            With .Range("B12:C14")
                                .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                For x = 7 To 10
                                    With .Borders(x)
                                        .LineStyle = 1
                                        .Weight = -4138
                                        .ColorIndex = -4105
                                    End With
                                Next
                            End With
                            .Cells(12, 2) = LangIni.Text("Booking", "Bookingcode")
                        Else
                            With .Range("B13:C14")
                                .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                For x = 7 To 10
                                    With .Borders(x)
                                        .LineStyle = 1
                                        .Weight = -4138
                                        .ColorIndex = -4105
                                    End With
                                Next
                            End With
                        End If
                        .Cells(13, 2) = LangIni.Text("Booking", "OrderNumber")
                        .Cells(13, 3) = Campaign.Channels(Chan).BookingTypes(BT).OrderNumber
                        .Cells(14, 2) = LangIni.Text("Booking", "ContractNumber")
                        .cells(14, 3) = Campaign.Channels(Chan).BookingTypes(BT).ContractNumber

                        .Range("B12:B14").Font.Bold = True

                        With .Range("B16:C17")
                            .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                            .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                            For x = 7 To 10
                                With .Borders(x)
                                    .LineStyle = 1
                                    .Weight = -4138
                                    .ColorIndex = -4105
                                End With
                            Next
                        End With
                        .Range("B16:B17").Font.Bold = True

                        .Cells(16, 2) = LangIni.Text("Booking", "Gross")
                        .Cells(16, 3) = "=I23"
                        .Cells(16, 3).NumberFormat = "#,##0 $"
                        .Cells(17, 2) = LangIni.Text("Booking", "Net")
                        .Cells(17, 3) = "=K23"
                        .Cells(17, 3).NumberFormat = "#,##0 $"
                        '.Cells(17, 2) = LangIni.Text("Booking", "Budget")
                        '.Cells(17, 3) = Campaign.Channels(Chan).BookingTypes(BT).PlannedNetBudget
                        '.Cells(17, 3).NumberFormat = "#,##0 $"

                        With .Range("H8:M17")
                            .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                            .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                            For x = 7 To 10
                                With .Borders(x)
                                    .LineStyle = 1
                                    .Weight = -4138
                                    .ColorIndex = -4105
                                End With
                            Next
                        End With
                        With .range("H9:M17")
                            .merge()
                            .WrapText = True
                            .HorizontalAlignment = -4131
                            .VerticalAlignment = -4160
                        End With
                        .cells(8, 8).value = LangIni.Text("Booking", "Comments")
                        .cells(9, 8) = Campaign.Channels(Chan).BookingTypes(BT).Comments
                        If Campaign.Area = "SE" AndAlso (TrinitySettings.UserEmail.Contains("mecglobal") Or TrinitySettings.UserEmail.Contains("maxus")) Then
                            .cells(9, 8) = Campaign.Channels(Chan).BookingTypes(BT).Comments & Chr(10) & _
                            "Vi ber er ange ovanstående ordernummer vid all korrespondens angående denna order. " & _
                            "Detta ordernummer måste uppges på er faktura. Fakturor utan ordernummer kommer att skickas " & _
                            "tillbaka utan att betalas."
                        ElseIf Campaign.Area = "NO" Then
                            .cells(9, 8) = Campaign.Channels(Chan).BookingTypes(BT).Comments & Chr(10) & _
                                "Vi ber dere angi ovenstående ordrenummer ved all korrespondanse angående denne ordren." & _
                                "Ordrenummeret må oppgis på faktura. Fakturaer uten ordrenummer vil returneres uten å betales"
                        End If
                        .cells(8, 8).font.bold = True

                        'The column headings. It is now 12 columns wide
                        With .Range("B20:M20")
                            .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                            .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                            .Font.Bold = True
                            .HorizontalAlignment = -4108
                            For x = 7 To 10
                                With .Borders(x)
                                    .LineStyle = 1
                                    .Weight = -4138
                                    .ColorIndex = -4105
                                End With
                            Next
                        End With

                        'This is where we should print the columns in the order of their column number in TrinitySettings
                        'But we don't at the moment
                        .Cells(20, 2) = LangIni.Text("Booking", "Date")
                        .Cells(20, 3) = LangIni.Text("Booking", "Time")
                        .cells(20, 4) = LangIni.Text("Booking", "Week")
                        .Cells(20, 5) = LangIni.Text("Booking", "Programme")
                        .Cells(20, 6) = LangIni.Text("Booking", "Spotlength")
                        .Cells(20, 7) = LangIni.Text("Booking", "Filmcode")
                        .Cells(20, 8) = LangIni.Text("Booking", "Estimate")
                        .Cells(20, 9) = LangIni.Text("Booking", "Gross")
                        .Cells(20, 10) = LangIni.Text("Booking", "Discount")
                        .Cells(20, 11) = LangIni.Text("Booking", "Net")
                        .Cells(20, 12) = LangIni.Text("Booking", "Placement")
                        .Cells(20, 13) = LangIni.Text("Booking", "Extra")

                        For Each column As String In _specialColumns.Items

                        Next

                        With .Range("B23:M23")
                            .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                            .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                            .Font.Bold = True
                            .HorizontalAlignment = -4108
                            For x = 7 To 10
                                With .Borders(x)
                                    .LineStyle = 1
                                    .Weight = -4138
                                    .ColorIndex = -4105
                                End With
                            Next
                        End With
                        .Cells(23, 2) = LangIni.Text("Booking", "Total")
                        .Cells(23, 8) = "=SUM(H21:H22)"
                        .Cells(23, 9) = "=SUM(I21:I22)"
                        .Cells(23, 9).NumberFormat = "$#,##0_);($#,##0)"
                        .Cells(23, 10) = "=1-(K23/I23)"
                        .Cells(23, 10).NumberFormat = "0.00%"
                        .Cells(23, 11) = "=SUM(K21:K22)"
                        .Cells(23, 11).NumberFormat = "$#,##0_);($#,##0)"

                        'Start printing the spots here
                        Row = 22
                        For s = 1 To Campaign.BookedSpots.Count
                            If Campaign.BookedSpots(s).Channel.ChannelName = Chan Then
                                If Campaign.BookedSpots(s).Bookingtype.Name = BT Then
                                    If DirectCast(mnuWeeks.Items("mnuWeek" & Campaign.BookedSpots(s).week.Name), Windows.Forms.ToolStripMenuItem).Checked Then
                                        .Rows(Row).EntireRow.Insert()
                                        .Cells(Row, 2) = Trinity.Helper.FormatDateForBooking(Campaign.BookedSpots(s).AirDate)
                                        .Cells(Row, 3) = Trinity.Helper.Mam2Tid(Campaign.BookedSpots(s).MaM)
                                        .cells(Row, 4) = DatePart(DateInterval.WeekOfYear, Campaign.BookedSpots(s).AirDate, FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                                        .Cells(Row, 5) = Campaign.BookedSpots(s).Programme
                                        .Cells(Row, 6) = Campaign.BookedSpots(s).Film.FilmLength
                                        If Campaign.BookedSpots(s).Filmcode <> "" Then
                                            .Cells(Row, 7) = Campaign.BookedSpots(s).Filmcode
                                        Else
                                            .Cells(Row, 7) = Campaign.BookedSpots(s).Film.Name
                                        End If
                                        .Cells(Row, 8) = Campaign.BookedSpots(s).ChannelEstimate
                                        .cells(Row, 9).numberformat = "#,##0 $"
                                        .Cells(Row, 9).value = Campaign.BookedSpots(s).GrossPrice * Campaign.BookedSpots(s).AddedValueIndex(False)
                                        .Cells(Row, 10) = Campaign.BookedSpots(s).Bookingtype.BuyingTarget.Discount
                                        .Cells(Row, 10).NumberFormat = "0.00%"
                                        .Cells(Row, 11) = (Campaign.BookedSpots(s).NetPrice * Campaign.BookedSpots(s).AddedValueIndex)
                                        .Cells(Row, 11).NumberFormat = "#,##0 $"

                                        .Cells(Row, 12) = ""
                                        If Not Campaign.BookedSpots(s).AddedValues.Count = 0 Then
                                            TmpStr = ""
                                            For Each kv As KeyValuePair(Of String, Trinity.cAddedValue) In Campaign.BookedSpots(s).AddedValues
                                                TmpStr = TmpStr + kv.Value.Name & "/"
                                            Next
                                            TmpStr = TmpStr.TrimEnd("/")
                                            .Cells(Row, 12) = TmpStr
                                            .Cells(Row, 13) = Campaign.BookedSpots(s).AddedValueIndex - 1
                                        End If
                                        .Cells(Row, 13).NumberFormat = "0.00%"
                                        .Rows(Row).HorizontalAlignment = -4108
                                        Row = Row + 1
                                    End If
                                End If
                            End If
                        Next
                        .Rows(Row).EntireRow.Delete()
                        .Rows(21).EntireRow.Delete()
                        .range("B20:M" & Row).Sort(Key1:=.Range("B21"), Order1:=1, Key2:=.Range("C21"), Order2:=1, Header:=0, OrderCustom:=1, MatchCase:=False, Orientation:=1, DataOption1:=0, DataOption2:=0)
                        Row = Row + 1
                        'Print compensations
                        'Print compensations
                        If Campaign.Channels(Chan).BookingTypes(BT).Compensations.Count > 0 Then
                            Row += 2
                            Dim HeadlineRow As Integer = Row
                            .range("B" & Row & ":G" & Row).merge()
                            .cells(Row, 2).value = LangIni.Text("Booking", "Compensations")
                            .cells(Row, 2).font.italic = True
                            Row = Row + 1
                            .Rows(Row).HorizontalAlignment = -4108
                            .cells(Row, 2).value = LangIni.Text("Booking", "From")
                            .cells(Row, 3).value = LangIni.Text("Booking", "To")
                            .cells(Row, 4).value = LangIni.Text("Booking", "TRP")
                            If LangIni.Text("Booking", "Expense") = "" Then
                                .cells(Row, 5).value = "Expense"
                            Else
                                .cells(Row, 5).value = LangIni.Text("Booking", "Expense")
                            End If
                            .cells(Row, 6).value = LangIni.Text("Booking", "Comments")
                            .range("F" & Row & ":G" & Row).merge()
                            For Each TmpComp As Trinity.cCompensation In Campaign.Channels(Chan).BookingTypes(BT).Compensations
                                Row = Row + 1
                                .Cells(Row, 2).NumberFormat = "@"
                                .Cells(Row, 3).NumberFormat = "@"
                                .cells(Row, 2).value = Trinity.Helper.FormatDateForBooking(TmpComp.FromDate)
                                .cells(Row, 3).value = Trinity.Helper.FormatDateForBooking(TmpComp.ToDate)
                                .cells(Row, 4).numberformat = "##,##0.0"
                                .cells(Row, 4).value = TmpComp.TRPs
                                If TmpComp.Expense > 0 Then
                                    .cells(Row, 5).value = TmpComp.Expense
                                End If
                                .cells(Row, 6).value = TmpComp.Comment
                                .range("F" & Row & ":G" & Row).merge()
                                .Rows(Row).HorizontalAlignment = -4108
                            Next
                            With .range("B" & HeadlineRow & ":G" & Row)
                                For x = 7 To 10
                                    .Borders(x).LineStyle = 1
                                    .Borders(x).Weight = 2
                                    .Borders(x).ColorIndex = -4105
                                Next
                            End With
                            With .range("B" & HeadlineRow & ":G" & HeadlineRow + 1)
                                For x = 7 To 10
                                    .Borders(x).LineStyle = 1
                                    .Borders(x).Weight = -4138
                                    .Borders(x).ColorIndex = -4105
                                Next
                                .interior.color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                .font.color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                .font.bold = True
                            End With
                        End If
                        'If Campaign.Channels(Chan).BookingTypes(BT).Compensations.Count > 0 Then
                        '    'Row = Row + 2
                        '    Dim HeadlineRow As Integer = Row
                        '    .range("B" & Row & ":G" & Row).merge()
                        '    .cells(Row, 2).value = LangIni.Text("Booking", "Compensations")
                        '    .cells(Row, 2).font.italic = True
                        '    Row = Row + 1
                        '    .Rows(Row).HorizontalAlignment = -4108
                        '    .cells(Row, 2).value = LangIni.Text("Booking", "From")
                        '    .cells(Row, 3).value = LangIni.Text("Booking", "To")
                        '    .cells(Row, 4).value = LangIni.Text("Booking", "TRP")
                        '    .cells(Row, 5).value = LangIni.Text("Booking", "Comments")
                        '    .range("E" & Row & ":G" & Row).merge()
                        '    For Each TmpComp As Trinity.cCompensation In Campaign.Channels(Chan).BookingTypes(BT).Compensations
                        '        Row = Row + 1
                        '        .cells(Row, 2).numberformat = "@"
                        '        .cells(Row, 3).numberformat = "@"
                        '        .cells(Row, 2).value = Trinity.Helper.FormatDateForBooking(TmpComp.FromDate)
                        '        .cells(Row, 3).value = Trinity.Helper.FormatDateForBooking(TmpComp.ToDate)
                        '        .cells(Row, 4).numberformat = "##,##0.0"
                        '        .cells(Row, 4).value = TmpComp.TRPs
                        '        .cells(Row, 5).value = TmpComp.Comment
                        '        .range("E" & Row & ":G" & Row).merge()
                        '        .Rows(Row).HorizontalAlignment = -4108
                        '    Next
                        '    With .range("B" & HeadlineRow & ":G" & Row)
                        '        For x = 7 To 10
                        '            .Borders(x).LineStyle = 1
                        '            .Borders(x).Weight = 2
                        '            .Borders(x).ColorIndex = -4105
                        '        Next
                        '    End With
                        '    With .range("B" & HeadlineRow & ":G" & HeadlineRow + 1)
                        '        For x = 7 To 10
                        '            .Borders(x).LineStyle = 1
                        '            .Borders(x).Weight = -4138
                        '            .Borders(x).ColorIndex = -4105
                        '        Next
                        '        .interior.color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                        '        .font.color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                        '        .font.bold = True
                        '    End With
                        'End If
                        .Columns(1).ColumnWidth = 3
                        .Columns(2).ColumnWidth = 14.5
                        .Columns(3).ColumnWidth = 12
                        .Columns(5).ColumnWidth = 22.29

                        If Campaign.Channels(Chan).BookingTypes(BT).ContractNumber = "" Then
                            .rows(14).entirerow.delete()
                        End If

                        If cmbLogo.SelectedIndex > 0 Then
                            .pictures.Insert(DataPath & "Logos\" & cmbLogo.Text)
                            Scal = 180 / .pictures(1).Width
                            Scal = 1
                            If Scal < 1 Then
                                .pictures(1).ScaleWidth(Scal, 0, 0)
                                .pictures(1).ScaleHeight(Scal, 0, 0)
                            End If
                            .pictures(1).Top = .Rows(2).Top
                            '.Rows(1).RowHeight = .pictures(1).Height + 10
                            .pictures(1).Left = .Columns("N").Left - .pictures(1).Width - 10
                        End If
                        With Excel.ActiveSheet.PageSetup
                            .Orientation = 2
                            .PrintArea = "$A$1:$M$" & Row
                            .PrintTitleRows = "$19:$19"
                        End With
                        'Excel.ActiveWindow.SelectedSheets.PrintPreview()
                        Excel.ActiveWindow.View = 2
                        While Excel.ActiveSheet.VPageBreaks.Count > 0
                            Excel.ActiveSheet.VPageBreaks(1).DragOff(Direction:=-4161, RegionIndex:=1)
                        End While
                        Excel.ActiveWindow.View = 1
                    End With
                Else
GoRBS:
                    With WB.Sheets(1)
                        If BT = "Bundled" Then

                            'Bundled

                            Dim AreaIni As New Trinity.clsIni
                            Dim printIndex As Boolean
                            Dim tmp As String
                            tmp = cmdLanguage.Tag
                            tmp = tmp.Substring(0, tmp.LastIndexOf("\") + 1)
                            tmp = tmp + "Area.ini"
                            AreaIni.Create(tmp)
                            printIndex = AreaIni.ReadBoolean("Print", "Rbsindex")

                            Dim RightColumn As Integer = 14
                            With WB.Sheets(1)
                                .AllCells.Font.Name = TrinitySettings.SchemeFont(cmbColorScheme.SelectedIndex + 1)
                                .AllCells.Font.Size = 9
                                .AllCells.interior.Color = RGB(255, 255, 255)
                                .Range("A:B").columnwidth = 8
                                .Columns(3).columnwidth = 13.57
                                .Columns(4).columnwidth = 9.29
                                .Columns(5).columnwidth = 8.71
                                .Range("F:L").columnwidth = 8.86
                                .Range("M:M").columnwidth = 3.43
                                Chan = Chan.Substring(0, InStr(Chan, "/") - 1)
                                .cells(3, 1) = Campaign.Channels(Chan).ChannelName & " / " & Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).ChannelName & " - " & LangIni.Text("Booking", "Bundled") & " " & LangIni.Text("Booking", "Booking")
                                .cells(5, 1) = LangIni.Text("Booking", "To")
                                .cells(5, 3) = grdContacts.Rows(i).Cells(0).Value
                                .cells(5, 10) = Campaign.Channels(Chan).ChannelName
                                .cells(6, 10) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).ChannelName
                                .cells(7, 1) = LangIni.Text("Booking", "From")
                                .cells(7, 3) = TrinitySettings.UserName
                                .cells(8, 3) = TrinitySettings.UserEmail
                                .cells(9, 3) = TrinitySettings.UserPhoneNr
                                .cells(11, 1) = LangIni.Text("Booking", "Advertiser")
                                .cells(11, 3) = Campaign.Client

                                'Hannes added this block
                                If chkCustomOrderPlacer.Checked Then
                                    Dim addressSplit As String() = Strings.Split(cmbOrderPlacer.Text, ",")
                                    .cells(7, 7) = "Booked on behalf of:"
                                    .cells(7, 7).font.bold = True
                                    Dim addressRow As Integer = 0
                                    For Each rowstring As String In addressSplit
                                        .cells(7 + addressRow, 8) = rowstring
                                        addressRow += 1
                                    Next
                                    .range("H7:H10").font.color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                End If

                                If chkCustomBilling.Checked Then
                                    Dim addressSplit As String() = Strings.Split(cmbBillingAddress.Text, ",")
                                    .cells(11, 7) = "Please bill:"
                                    .cells(11, 7).font.bold = True
                                    Dim addressRow As Integer = 0
                                    For Each rowstring As String In addressSplit
                                        .cells(11 + addressRow, 8) = rowstring
                                        addressRow += 1
                                    Next
                                    .range("H11:H14").font.color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                End If

                                .cells(13, 1) = LangIni.Text("Booking", "Product")
                                .cells(13, 3) = Campaign.Product

                                With .Range("A3:M3")
                                    .MERGE()
                                    .horizontalalignment = -4108
                                    .Font.Bold = True
                                    .Font.Size = 16
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                End With
                                .Range("C7:C22").Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                .cells(11, 3).Font.Bold = True
                                .cells(13, 3).Font.Bold = True
                                .cells(19, 3).Font.Bold = True
                                .cells(21, 5).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                .cells(21, 4).Font.Bold = True
                                .cells(21, 5).Font.Bold = True
                                .cells(22, 5).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                .cells(22, 4).Font.Bold = True
                                .cells(22, 5).Font.Bold = True
                                With .Range("B24:" & Chr(65 + Campaign.Channels(Chan).BookingTypes(BTName).Dayparts.Count) & "24")
                                    .MERGE()
                                    .horizontalalignment = -4108
                                    .Font.Bold = True
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                    .interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")

                                    For q = 7 To 10
                                        .Borders(q).LineStyle = 1
                                        .Borders(q).Weight = 2
                                        .Borders(q).Color = 0
                                    Next
                                End With
                                With .Range("B24:" & Chr(65 + Campaign.Channels(Chan).BookingTypes(BTName).Dayparts.Count) & "26")
                                    .horizontalalignment = -4108
                                    For q = 7 To 10
                                        .Borders(q).LineStyle = 1
                                        .Borders(q).Weight = 2
                                        .Borders(q).Color = 0
                                    Next
                                End With
                                With .Range("F24:K24")
                                    .horizontalalignment = -4108
                                    .Font.Bold = True
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                    .interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                    For q = 7 To 10
                                        .Borders(q).LineStyle = 1
                                        .Borders(q).Weight = 2
                                        .Borders(q).Color = 0
                                    Next
                                End With
                                With .Range("H23:I23")
                                    .MERGE()
                                    .horizontalalignment = -4108
                                    .Font.Bold = True
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                    .interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                    For q = 7 To 10
                                        .Borders(q).LineStyle = 1
                                        .Borders(q).Weight = 2
                                        .Borders(q).Color = 0
                                    Next
                                End With
                                With .Range("J23:K23")
                                    .MERGE()
                                    .horizontalalignment = -4108
                                    .Font.Bold = True
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                    .interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                    For q = 7 To 10
                                        .Borders(q).LineStyle = 1
                                        .Borders(q).Weight = 2
                                        .Borders(q).Color = 0
                                    Next
                                End With
                                If DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) = DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) Then
                                    PeriodStr = LangIni.Text("Spotlist", "Week") & " " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & ", " & Year(Date.FromOADate(Campaign.StartDate))
                                Else
                                    PeriodStr = LangIni.Text("Spotlist", "Week") & " " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & "-" & DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & ", " & Year(Date.FromOADate(Campaign.StartDate))
                                End If
                                .cells(15, 1) = LangIni.Text("Booking", "CampaignPeriod") & ":"
                                .cells(15, 3) = PeriodStr
                                .cells(17, 1) = LangIni.Text("Booking", "BookingPeriod")
                                .cells(17, 8) = LangIni.Text("Booking", "Ordernumber") & " " & Campaign.Channels(Chan).ChannelName
                                .Cells(16, 8) = "Channel reference:"
                                .cells(16, 10) = Campaign.Channels(Chan).BookingTypes(BT).ContractNumber
                                .cells(17, 8).Font.Bold = True
                                .cells(17, 10) = Campaign.Channels(Chan).BookingTypes(BTName).OrderNumber
                                .cells(18, 8) = LangIni.Text("Booking", "Ordernumber") & " " & Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).ChannelName

                                If Campaign.Area = "SE" AndAlso (TrinitySettings.UserEmail.Contains("mecglobal") Or TrinitySettings.UserEmail.Contains("maxus")) Then
                                    With .range("H19:L22")
                                        .merge()
                                        .value = "Vi ber er ange ovanstående ordernummer vid all korrespondens angående denna order. " & _
                                        "Detta ordernummer måste uppges på er faktura. Fakturor utan ordernummer kommer att skickas " & _
                                        "tillbaka utan att betalas."
                                        .font.size = 8
                                        .wraptext = True
                                    End With
                                ElseIf Campaign.Area = "NO" Then
                                    With .range("H19:L22")
                                        .merge()
                                        .value = "Vi ber dere angi ovenstående ordrenummer ved all korrespondanse angående denne ordren." & _
                                                 "Ordrenummeret må oppgis på faktura. Fakturaer uten ordrenummer vil returneres uten å betales."
                                        .font.size = 8
                                        .wraptext = True
                                    End With

                                End If

                                .cells(18, 8).Font.Bold = True
                                .cells(18, 10) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).OrderNumber
                                With .Range("H17:J18")
                                    For q = 7 To 10
                                        .Borders(q).LineStyle = 1
                                        .Borders(q).Weight = 2
                                        .Borders(q).Color = 0
                                    Next
                                    .interior.Colorindex = 6
                                End With
                                .cells(19, 1) = LangIni.Text("Booking", "Target")
                                Targ = Campaign.Channels(Chan).BookingTypes(BTName).BuyingTarget.TargetName
                                Select Case Targ.Substring(0, 1)
                                    Case "A" : Gender = LangIni.Text("Targets", "All") & " "
                                    Case "K" : Gender = LangIni.Text("Targets", "Women") & " "
                                    Case "M" : Gender = LangIni.Text("Targets", "Men") & " "
                                    Case "W" : Gender = LangIni.Text("Targets", "Women") & " "
                                    Case "0" To "9" : Gender = LangIni.Text("Targets", "All") & " "
                                    Case Else : Gender = ""
                                End Select
                                Targ = Gender + Mid(Targ, 2)
                                .cells(19, 3) = Targ


                                '.cells(21, 1) = Campaign.Channels(Chan).ChannelName
                                '.cells(21, 2) = LangIni.Text("Booking", "CPT30")
                                '.cells(21, 3).Numberformat = "0.0"
                                '.cells(21, 3) = Campaign.Channels(Chan).BookingTypes(BTName).BuyingTarget.NetCPT
                                '.cells(21, 4) = LangIni.Text("Booking", "Discount")
                                '.cells(21, 5) = Campaign.Channels(Chan).BookingTypes(BTName).BuyingTarget.Discount
                                '.cells(21, 6) = LangIni.Text("Booking", "TRPShare")
                                'If (Campaign.Channels(Chan).BookingTypes(BTName).TotalTRPBuyingTarget + Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).TotalTRPBuyingTarget) <> 0 Then
                                '    .cells(21, 7) = Campaign.Channels(Chan).BookingTypes(BTName).TotalTRPBuyingTarget / (Campaign.Channels(Chan).BookingTypes(BTName).TotalTRPBuyingTarget + Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).TotalTRPBuyingTarget)
                                'Else
                                '    .cells(21, 7) = 0
                                'End If
                                '.cells(21, 5).NUMBERFORMAT = "0.0%"
                                '.cells(21, 6).Font.Bold = True
                                '.cells(21, 7).NUMBERFORMAT = "0.0%"
                                '.cells(22, 1) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).ChannelName
                                '.cells(22, 2) = LangIni.Text("Booking", "CPT30")
                                '.cells(22, 3).Numberformat = "0.0"
                                '.cells(22, 3) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).BuyingTarget.NetCPT
                                '.cells(22, 4) = LangIni.Text("Booking", "Discount")
                                '.cells(22, 5) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).BuyingTarget.Discount
                                '.cells(22, 6) = LangIni.Text("Booking", "TRPShare")
                                'If (Campaign.Channels(Chan).BookingTypes(BTName).TotalTRPBuyingTarget + Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).TotalTRPBuyingTarget) <> 0 Then
                                '    .cells(22, 7) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).TotalTRPBuyingTarget / (Campaign.Channels(Chan).BookingTypes(BTName).TotalTRPBuyingTarget + Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).TotalTRPBuyingTarget)
                                'Else
                                '    .cells(22, 7) = 0
                                'End If
                                '.cells(22, 5).NUMBERFORMAT = "0.0%"
                                '.cells(22, 6).Font.Bold = True
                                '.cells(22, 7).NUMBERFORMAT = "0.0%"

                                'Print CPT
                                .cells(5, 6) = Campaign.Channels(Chan).ChannelName
                                .cells(5, 7) = LangIni.Text("Booking", "CPT30")
                                .cells(5, 8).Numberformat = "0.0"
                                .cells(5, 8) = Campaign.Channels(Chan).BookingTypes(BTName).BuyingTarget.NetCPT
                                .cells(5, 9) = LangIni.Text("Booking", "Discount")
                                .cells(5, 10) = Campaign.Channels(Chan).BookingTypes(BTName).BuyingTarget.Discount
                                .cells(5, 11) = LangIni.Text("Booking", "TRPShare")
                                If (Campaign.Channels(Chan).BookingTypes(BTName).TotalTRPBuyingTarget + Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).TotalTRPBuyingTarget) <> 0 Then
                                    .cells(21, 12) = Campaign.Channels(Chan).BookingTypes(BTName).TotalTRPBuyingTarget / (Campaign.Channels(Chan).BookingTypes(BTName).TotalTRPBuyingTarget + Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).TotalTRPBuyingTarget)
                                Else
                                    .cells(21, 12) = 0
                                End If
                                .cells(5, 10).NUMBERFORMAT = "0.0%"
                                .cells(5, 11).Font.Bold = True
                                .cells(5, 12).NUMBERFORMAT = "0.0%"
                                .cells(6, 6) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).ChannelName
                                .cells(6, 7) = LangIni.Text("Booking", "CPT30")
                                .cells(6, 8).Numberformat = "0.0"
                                .cells(6, 8) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).BuyingTarget.NetCPT
                                .cells(6, 9) = LangIni.Text("Booking", "Discount")
                                .cells(6, 10) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).BuyingTarget.Discount
                                .cells(6, 11) = LangIni.Text("Booking", "TRPShare")
                                If (Campaign.Channels(Chan).BookingTypes(BTName).TotalTRPBuyingTarget + Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).TotalTRPBuyingTarget) <> 0 Then
                                    .cells(6, 12) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).TotalTRPBuyingTarget / (Campaign.Channels(Chan).BookingTypes(BTName).TotalTRPBuyingTarget + Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).TotalTRPBuyingTarget)
                                Else
                                    .cells(6, 12) = 0
                                End If
                                .cells(6, 10).NUMBERFORMAT = "0.0%"
                                .cells(6, 11).Font.Bold = True
                                .cells(6, 12).NUMBERFORMAT = "0.0%"

                                'Print CPP
                                .cells(7, 6) = Campaign.Channels(Chan).ChannelName
                                .cells(7, 7) = LangIni.Text("Booking", "CPP30")
                                .cells(7, 8).Numberformat = "0.0"
                                .cells(7, 8) = Campaign.Channels(Chan).BookingTypes(BTName).PlannedCPP30
                                .cells(7, 9) = LangIni.Text("Booking", "Discount")
                                .cells(7, 10) = Campaign.Channels(Chan).BookingTypes(BTName).BuyingTarget.Discount
                                .cells(7, 11) = LangIni.Text("Booking", "TRPShare")
                                If (Campaign.Channels(Chan).BookingTypes(BTName).TotalTRPBuyingTarget + Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).TotalTRPBuyingTarget) <> 0 Then
                                    .cells(7, 12) = Campaign.Channels(Chan).BookingTypes(BTName).TotalTRPBuyingTarget / (Campaign.Channels(Chan).BookingTypes(BTName).TotalTRPBuyingTarget + Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).TotalTRPBuyingTarget)
                                Else
                                    .cells(7, 12) = 0
                                End If
                                .cells(7, 10).NUMBERFORMAT = "0.0%"
                                .cells(7, 11).Font.Bold = True
                                .cells(7, 12).NUMBERFORMAT = "0.0%"
                                .cells(8, 6) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).ChannelName
                                .cells(8, 7) = LangIni.Text("Booking", "CPP30")
                                .cells(8, 8).Numberformat = "0.0"
                                .cells(8, 8) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).PlannedCPP30
                                .cells(8, 9) = LangIni.Text("Booking", "Discount")
                                .cells(8, 10) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).BuyingTarget.Discount
                                .cells(8, 11) = LangIni.Text("Booking", "TRPShare")
                                If (Campaign.Channels(Chan).BookingTypes(BTName).TotalTRPBuyingTarget + Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).TotalTRPBuyingTarget) <> 0 Then
                                    .cells(8, 12) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).TotalTRPBuyingTarget / (Campaign.Channels(Chan).BookingTypes(BTName).TotalTRPBuyingTarget + Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).TotalTRPBuyingTarget)
                                Else
                                    .cells(8, 12) = 0
                                End If
                                .cells(8, 10).NUMBERFORMAT = "0.0%"
                                .cells(8, 11).Font.Bold = True
                                .cells(8, 12).NUMBERFORMAT = "0.0%"


                                .cells(24, 2) = LangIni.Text("Booking", "DaypartSplit") & " " & Campaign.Channels(Chan).ChannelName
                                For dp = 0 To Campaign.Channels(Chan).BookingTypes(BTName).Dayparts.Count - 1
                                    If LangIni.Text("Booking", Campaign.Channels(Chan).BookingTypes(BTName).Dayparts(dp).Name) <> "" Then
                                        .cells(25, 2 + dp) = LangIni.Text("Booking", Campaign.Channels(Chan).BookingTypes(BTName).Dayparts(dp).Name)
                                    Else
                                        .cells(25, 2 + dp) = Campaign.Channels(Chan).BookingTypes(BTName).Dayparts(dp).Name
                                    End If
                                Next
                                For z = 2 To Campaign.Channels(Chan).BookingTypes(BTName).Dayparts.Count + 1
                                    .cells(26, z) = Campaign.Channels(Chan).BookingTypes(BTName).Dayparts(z - 2).Share / 100
                                    .cells(26, z).NUMBERFORMAT = "0%"
                                    .cells(26, z).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                Next
                                .cells(24, 6) = LangIni.Text("Booking", "Filmcode")
                                .cells(24, 7) = LangIni.Text("Booking", "Spotlength")
                                .cells(23, 8) = Campaign.Channels(Chan).ChannelName
                                .cells(24, 8) = LangIni.Text("Booking", "Share")
                                .cells(24, 9) = LangIni.Text("Booking", "TRP")
                                .cells(23, 10) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).ChannelName
                                .cells(24, 10) = LangIni.Text("Booking", "Share")
                                .cells(24, 11) = LangIni.Text("Booking", "TRP")
                                .cells(23, 6).horizontalalignment = -4108
                                .cells(24, 7).horizontalalignment = -4108
                                .cells(24, 8).horizontalalignment = -4108
                                .cells(24, 9).horizontalalignment = -4108

                                BT = BTName
                                Dim FilmTRP(Campaign.Channels(Chan).BookingTypes(BTName).Weeks(1).Films.Count) As Single
                                Dim FilmTRP2(Campaign.Channels(Chan).BookingTypes(BTName).Weeks(1).Films.Count) As Single
                                Dim TotTRP2 As Single = 0
                                TotTRP = 0
                                For z = 1 To Campaign.Channels(Chan).BookingTypes(BTName).Weeks.Count
                                    If DirectCast(mnuWeeks.Items("mnuWeek" & Campaign.Channels(Chan).BookingTypes(BT).Weeks(z).Name), Windows.Forms.ToolStripMenuItem).Checked Then
                                        For q = 1 To Campaign.Channels(Chan).BookingTypes(BTName).Weeks(z).Films.Count
                                            FilmTRP(q) = FilmTRP(q) + (Campaign.Channels(Chan).BookingTypes(BTName).Weeks(z).TRPBuyingTarget * (Campaign.Channels(Chan).BookingTypes(BTName).Weeks(z).Films(q).Share / 100))
                                            TotTRP = TotTRP + (Campaign.Channels(Chan).BookingTypes(BTName).Weeks(z).TRPBuyingTarget * (Campaign.Channels(Chan).BookingTypes(BTName).Weeks(z).Films(q).Share / 100))
                                            FilmTRP2(q) = FilmTRP2(q) + (Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).Weeks(z).TRPBuyingTarget * (Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).Weeks(z).Films(q).Share / 100))
                                            TotTRP2 = TotTRP2 + (Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).Weeks(z).TRPBuyingTarget * (Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).Weeks(z).Films(q).Share / 100))
                                        Next
                                    End If
                                Next
                                Dim rr As Integer = 2
                                For q = 1 To Campaign.Channels(Chan).BookingTypes(BTName).Weeks(1).Films.Count
                                    If FilmTRP(q) > 0 Or FilmTRP2(q) > 0 Then
                                        .cells(24 + rr, 5) = q
                                        .cells(24 + rr, 5).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                        .cells(24 + rr, 5).horizontalalignment = -4152
                                        .cells(24 + rr, 6).NUMBERFORMAT = "@"
                                        If Campaign.Channels(Chan).BookingTypes(BTName).Weeks(1).Films(q).Filmcode = "" Then
                                            .cells(24 + rr, 6) = Campaign.Channels(Chan).BookingTypes(BTName).Weeks(1).Films(q).Name
                                        Else
                                            .cells(24 + rr, 6) = Campaign.Channels(Chan).BookingTypes(BTName).Weeks(1).Films(q).Filmcode
                                        End If

                                        .cells(24 + rr, 6).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                        .cells(24 + rr, 6).Font.Bold = True
                                        .cells(24 + rr, 7) = Campaign.Channels(Chan).BookingTypes(BTName).Weeks(1).Films(q).FilmLength
                                        .cells(24 + rr, 7).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                        If TotTRP > 0 Then
                                            .cells(24 + rr, 8) = FilmTRP(q) / TotTRP
                                        Else
                                            .cells(24 + rr, 8) = 0
                                        End If
                                        .cells(24 + rr, 8).NUMBERFORMAT = "0%"
                                        .cells(24 + rr, 8).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                        .cells(24 + rr, 9) = FilmTRP(q)
                                        .cells(24 + rr, 9).NUMBERFORMAT = "##,##0.0"
                                        .cells(24 + rr, 9).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                        If TotTRP2 > 0 Then
                                            .cells(24 + rr, 10) = FilmTRP2(q) / TotTRP2
                                        Else
                                            .cells(24 + rr, 10) = 0
                                        End If
                                        .cells(24 + rr, 10).NUMBERFORMAT = "0%"
                                        .cells(24 + rr, 10).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                        .cells(24 + rr, 11) = FilmTRP2(q)
                                        .cells(24 + rr, 11).NUMBERFORMAT = "##,##0.0"
                                        .cells(24 + rr, 11).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                        rr = rr + 1
                                    End If
                                Next
                                With .Range("F24:K" & 24 + rr)
                                    For z = 7 To 11
                                        .Borders(z).LineStyle = 1
                                        .Borders(z).Weight = 2
                                        .Borders(z).Color = 0
                                    Next
                                    .horizontalalignment = -4108
                                End With
                                TopRow = 26 + q
                                .cells(TopRow, 1) = LangIni.Text("Booking", "Week")
                                .cells(TopRow, 2) = LangIni.Text("Booking", "CampaignPeriod")
                                .Range("B" & TopRow & ":C" & TopRow).MERGE()
                                .Range("B" & TopRow & ":C" & TopRow).horizontalalignment = -4108
                                .Range("B" & TopRow + 1 & ":C" & TopRow + 1).MERGE()
                                .cells(TopRow, 4) = LangIni.Text("Booking", "TRPShare")
                                .Range("D" & TopRow & ":G" & TopRow).MERGE()
                                .cells(TopRow, 8) = LangIni.Text("Booking", "FilmSplit")
                                .cells(TopRow + 1, 8) = Campaign.Channels(Chan).ChannelName
                                .cells(TopRow + 1, 9) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).ChannelName
                                .Range("H" & TopRow & ":I" & TopRow).MERGE()
                                .Range("H" & TopRow & ":I" & TopRow).horizontalalignment = -4108
                                .cells(TopRow, 10) = LangIni.Text("Booking", "SeasonIndex")
                                .Range("J" & TopRow & ":K" & TopRow).MERGE()
                                .cells(TopRow + 1, 10) = Campaign.Channels(Chan).ChannelName
                                .cells(TopRow + 1, 11) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).ChannelName
                                .cells(TopRow, 12) = LangIni.Text("Booking", "Budget")
                                .cells(TopRow + 1, 4) = "%"
                                .cells(TopRow + 1, 5) = Campaign.Channels(Chan).ChannelName
                                .cells(TopRow + 1, 6) = "%"
                                .cells(TopRow + 1, 7) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).ChannelName
                                RubrRange = "A" & TopRow & ":L" & TopRow + 1
                                TopRow = TopRow + 1
                                TotBud = 0
                                z = 0
                                For w = 1 To Campaign.Channels(Chan).BookingTypes(BTName).Weeks.Count
                                    If DirectCast(mnuWeeks.Items("mnuWeek" & Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).Name), Windows.Forms.ToolStripMenuItem).Checked Then
                                        z += 1
                                        .cells(TopRow + z, 1) = Trim(Campaign.Channels(Chan).BookingTypes(BTName).Weeks(w).Name)
                                        .cells(TopRow + z, 1).NUMBERFORMAT = "@"
                                        .cells(TopRow + z, 2) = Trinity.Helper.FormatShortDateForBooking(Date.FromOADate(Campaign.Channels(Chan).BookingTypes(BTName).Weeks(w).StartDate)) & " - " & Trinity.Helper.FormatShortDateForBooking(Date.FromOADate(Campaign.Channels(Chan).BookingTypes(BTName).Weeks(w).EndDate))
                                        .Range("B" & TopRow + z & ":C" & TopRow + z).MERGE()
                                        .Range("B" & TopRow + z & ":C" & TopRow + z).horizontalalignment = -4108
                                        If lblWeeks.Text = "All" Then
                                            .cells(TopRow + z, 4).FORMULA = "=" & .cells(TopRow + z, 5).Address & "/" & .cells(TopRow + Campaign.Channels(Chan).BookingTypes(BTName).Weeks.Count + 1, 5).Address
                                            .cells(TopRow + z, 6).FORMULA = "=" & .cells(TopRow + z, 7).Address & "/" & .cells(TopRow + Campaign.Channels(Chan).BookingTypes(BTName).Weeks.Count + 1, 7).Address
                                        Else
                                            .cells(TopRow + z, 4).FORMULA = "=" & .cells(TopRow + z, 5).Address & "/" & .cells(TopRow + countCharInString(lblWeeks.Text, ",") + 2, 5).Address
                                            .cells(TopRow + z, 6).FORMULA = "=" & .cells(TopRow + z, 7).Address & "/" & .cells(TopRow + countCharInString(lblWeeks.Text, ",") + 2, 7).Address
                                        End If
                                        .cells(TopRow + z, 4).NUMBERFORMAT = "0.00%"
                                        .cells(TopRow + z, 5) = Campaign.Channels(Chan).BookingTypes(BTName).Weeks(w).TRPBuyingTarget
                                        .cells(TopRow + z, 5).NUMBERFORMAT = "0.0"
                                        .cells(TopRow + z, 6).NUMBERFORMAT = "0.00%"
                                        .cells(TopRow + z, 7) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).Weeks(w).TRPBuyingTarget
                                        .cells(TopRow + z, 7).NUMBERFORMAT = "0.0"
                                        Dim FilmSplit As String = ""
                                        For q = 1 To Campaign.Channels(Chan).BookingTypes(BTName).Weeks(w).Films.Count
                                            If FilmTRP(q) > 0 Or FilmTRP2(q) > 0 Then
                                                FilmSplit = FilmSplit & Campaign.Channels(Chan).BookingTypes(BTName).Weeks(w).Films(q).Share & "/"
                                            End If
                                        Next
                                        If FilmSplit <> "" Then
                                            FilmSplit = FilmSplit.Substring(0, Len(FilmSplit) - 1) + "%"
                                        End If
                                        .cells(TopRow + z, 8) = FilmSplit
                                        FilmSplit = ""
                                        For q = 1 To Campaign.Channels(Chan).BookingTypes(BTName).Weeks(w).Films.Count
                                            If FilmTRP(q) > 0 Or FilmTRP2(q) > 0 Then
                                                FilmSplit = FilmSplit & Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).Weeks(w).Films(q).Share & "/"
                                            End If
                                        Next
                                        If FilmSplit <> "" Then
                                            FilmSplit = FilmSplit.Substring(0, Len(FilmSplit) - 1) & "%"
                                        End If
                                        .cells(TopRow + z, 9) = FilmSplit
                                        .cells(TopRow + z, 10) = Campaign.Channels(Chan).BookingTypes(BTName).Weeks(w).Index * 100
                                        .cells(TopRow + z, 10).NUMBERFORMAT = "0"
                                        .cells(TopRow + z, 11) = Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).Weeks(w).Index * 100
                                        .cells(TopRow + z, 11).NUMBERFORMAT = "0"
                                        CPP = Campaign.Channels(Chan).BookingTypes(BTName).Weeks(z).NetCPP
                                        .cells(TopRow + z, 12).NUMBERFORMAT = "#,##0 $"
                                        .cells(TopRow + z, 12) = Campaign.Channels(Chan).BookingTypes(BTName).Weeks(w).NetBudget + Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).Weeks(w).NetBudget
                                        TotBud = TotBud + CPP * Campaign.Channels(Chan).BookingTypes(BTName).Weeks(w).TRPBuyingTarget
                                    End If
                                Next
                                z += 1
                                With .Range("A" & TopRow + 1 & ":L" & TopRow + z)
                                    .horizontalalignment = -4108
                                    For q = 7 To 12
                                        .Borders(q).LineStyle = 1
                                        .Borders(q).Weight = 2
                                        .Borders(q).Color = 0
                                    Next
                                End With
                                .cells(TopRow + z, 1) = LangIni.Text("Booking", "Total")
                                .cells(TopRow + z, 4) = "100%"
                                .cells(TopRow + z, 5).FORMULA = "=SUM(" & .cells(TopRow + 1, 5).Address & ":" & .cells(TopRow + z - 1, 5).Address
                                .cells(TopRow + z, 6) = "100%"
                                .cells(TopRow + z, 7).FORMULA = "=SUM(" & .cells(TopRow + 1, 7).Address & ":" & .cells(TopRow + z - 1, 7).Address
                                .cells(TopRow + z, 12).NUMBERFORMAT = "#,##0 $"
                                .cells(TopRow + z, 12).FORMULA = "=SUM(" & .cells(TopRow + 1, 12).Address & ":" & .cells(TopRow + z - 1, 12).Address
                                .Columns(11).autofit()

                                'If optTRP Then
                                '    .cells(TopRow + z + 1, 1) = LangIni.Text("Booking", "Disclaimer") & " " & LangIni.Text("Booking", "TRPLimit")
                                'Else
                                '    .cells(TopRow + z + 1, 1) = LangIni.Text("Booking", "Disclaimer") & " " & LangIni.Text("Booking", "BudgetLimit")
                                'End If
                                .cells(TopRow + z + 1, 1).Font.Bold = True
                                .cells(TopRow + z + 1, 1).Font.Color = RGB(255, 0, 0)

                                .Range("B" & TopRow + z & ":C" & TopRow + z).MERGE()
                                .Range("B" & TopRow + z & ":C" & TopRow + z).horizontalalignment = -4108
                                TopRow = TopRow + z + 3
                                If Campaign.Channels(Chan).BookingTypes(BTName).Indexes.Count > 0 Then
                                    .cells(TopRow, 1) = "*" & LangIni.Text("Booking", "Indexes") & " " & Chan
                                    .cells(TopRow + 1, 1) = LangIni.Text("Booking", "Name")
                                    .cells(TopRow + 1, 2) = LangIni.Text("Spotlist", "Period")
                                    .cells(TopRow + 1, 4) = LangIni.Text("Booking", "Index")
                                    With .Range("A" & TopRow & ":M" & TopRow + 1)
                                        .Font.Bold = True
                                        .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                        .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                        For q = 7 To 10
                                            .Borders(q).LineStyle = 1
                                            .Borders(q).Weight = 2
                                            .Borders(q).Color = 0
                                        Next
                                    End With
                                    TopRow += 1
                                    For Each TmpIndex As Trinity.cIndex In Campaign.Channels(Chan).BookingTypes(BTName).Indexes
                                        If (TmpIndex.ToDate.ToOADate >= Campaign.StartDate And TmpIndex.ToDate.ToOADate <= Campaign.EndDate) Or (TmpIndex.FromDate.ToOADate >= Campaign.StartDate And TmpIndex.FromDate.ToOADate <= Campaign.EndDate) Or (TmpIndex.FromDate.ToOADate <= Campaign.StartDate And TmpIndex.ToDate.ToOADate >= Campaign.EndDate) Then
                                            TopRow += 1
                                            .cells(TopRow, 1) = TmpIndex.Name
                                            .range("B" & TopRow & ":C" & TopRow).merge()
                                            .cells(TopRow, 2) = Trinity.Helper.FormatShortDateForBooking(TmpIndex.FromDate) & " - " & Trinity.Helper.FormatShortDateForBooking(TmpIndex.ToDate)
                                            .cells(TopRow, 4).numberformat = "##,##0.0"
                                            .cells(TopRow, 4).HorizontalAlignment = -4131
                                            .cells(TopRow, 4) = TmpIndex.Index
                                        End If
                                    Next
                                    TopRow += 2
                                End If

                                If Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).Indexes.Count > 0 Then
                                    .cells(TopRow, 1) = "*" & LangIni.Text("Booking", "Indexes") & " " & Campaign.Channels(Chan).ConnectedChannel
                                    .cells(TopRow + 1, 1) = LangIni.Text("Booking", "Name")
                                    .cells(TopRow + 1, 2) = LangIni.Text("Spotlist", "Period")
                                    .cells(TopRow + 1, 4) = LangIni.Text("Booking", "Index")
                                    With .Range("A" & TopRow & ":M" & TopRow + 1)
                                        .Font.Bold = True
                                        .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                        .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                        For q = 7 To 10
                                            .Borders(q).LineStyle = 1
                                            .Borders(q).Weight = 2
                                            .Borders(q).Color = 0
                                        Next
                                    End With
                                    TopRow += 1
                                    For Each tmpIndex As Trinity.cIndex In Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).Indexes
                                        'For Each TmpIndex As Trinity.cIndex In Campaign.Channels(Chan).BookingTypes(BTName).Indexes
                                        If (tmpIndex.ToDate.ToOADate >= Campaign.StartDate And tmpIndex.ToDate.ToOADate <= Campaign.EndDate) Or (tmpIndex.FromDate.ToOADate >= Campaign.StartDate And tmpIndex.FromDate.ToOADate <= Campaign.EndDate) Or (tmpIndex.FromDate.ToOADate <= Campaign.StartDate And tmpIndex.ToDate.ToOADate >= Campaign.EndDate) Then
                                            TopRow += 1
                                            .cells(TopRow, 1) = tmpIndex.Name
                                            .range("B" & TopRow & ":C" & TopRow).merge()
                                            .cells(TopRow, 2) = Trinity.Helper.FormatShortDateForBooking(tmpIndex.FromDate) & " - " & Trinity.Helper.FormatShortDateForBooking(tmpIndex.ToDate)
                                            .cells(TopRow, 4).numberformat = "##,##0.0"
                                            .cells(TopRow, 4).HorizontalAlignment = -4131
                                            .cells(TopRow, 4) = tmpIndex.Index
                                        End If
                                    Next
                                    TopRow += 2
                                End If


                                .cells(TopRow, 1) = LangIni.Text("Booking", "Comments")
                                .cells(TopRow + 1, 1) = Campaign.Channels(Chan).BookingTypes(BTName).Comments
                                With .Range("A" & TopRow & ":M" & TopRow)
                                    .Font.Bold = True
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                    .interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                    For q = 7 To 10
                                        .Borders(q).LineStyle = 1
                                        .Borders(q).Weight = 2
                                        .Borders(q).Color = 0
                                    Next
                                End With
                                .cells(TopRow + 6, 1) = LangIni.Text("Booking", "Other")
                                With .Range("A" & TopRow + 6 & ":M" & TopRow + 6)
                                    .Font.Bold = True
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                    .interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                    For q = 7 To 10
                                        .Borders(q).LineStyle = 1
                                        .Borders(q).Weight = 2
                                        .Borders(q).Color = 0
                                    Next
                                End With
                                With .Range(RubrRange)
                                    .horizontalalignment = -4108
                                    .Font.Bold = True
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                    .interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                    For q = 7 To 11
                                        .Borders(q).LineStyle = 1
                                        .Borders(q).Weight = 3
                                        .Borders(q).Color = 0
                                    Next
                                End With
                                'Print compensations
                                'Print compensations
                                If Campaign.Channels(Chan).BookingTypes(BT).Compensations.Count > 0 OrElse Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BTName).Compensations.Count Then
                                    Row = TopRow + 8
                                    Dim HeadlineRow As Integer = Row
                                    .range("B" & Row & ":G" & Row).merge()
                                    .cells(Row, 2).value = LangIni.Text("Booking", "Compensations")
                                    .cells(Row, 2).font.italic = True
                                    Row = Row + 1
                                    .Rows(Row).HorizontalAlignment = -4108
                                    .cells(Row, 2).value = LangIni.Text("Spotlist", "Channel")
                                    .cells(Row, 3).value = LangIni.Text("Booking", "From")
                                    .cells(Row, 4).value = LangIni.Text("Booking", "To")
                                    .cells(Row, 5).value = LangIni.Text("Booking", "TRP")
                                    If LangIni.Text("Booking", "Expense") = "" Then
                                        .cells(Row, 6).value = "Expense"
                                    Else
                                        .cells(Row, 6).value = LangIni.Text("Booking", "Expense")
                                    End If
                                    .cells(Row, 7).value = LangIni.Text("Booking", "Comments")
                                    .range("G" & Row & ":H" & Row).merge()
                                    For Each TmpComp As Trinity.cCompensation In Campaign.Channels(Chan).BookingTypes(BT).Compensations
                                        Row = Row + 1
                                        .cells(Row, 2).value = Chan
                                        .Cells(Row, 3).NumberFormat = "@"
                                        .Cells(Row, 4).NumberFormat = "@"
                                        .cells(Row, 3).value = Trinity.Helper.FormatDateForBooking(TmpComp.FromDate)
                                        .cells(Row, 4).value = Trinity.Helper.FormatDateForBooking(TmpComp.ToDate)
                                        .cells(Row, 5).numberformat = "##,##0.0"
                                        .cells(Row, 5).value = TmpComp.TRPs
                                        If TmpComp.Expense > 0 Then
                                            .cells(Row, 6).value = TmpComp.Expense
                                        End If
                                        .cells(Row, 7).value = TmpComp.Comment
                                        .range("G" & Row & ":H" & Row).merge()
                                        .Rows(Row).HorizontalAlignment = -4108
                                    Next
                                    For Each TmpComp As Trinity.cCompensation In Campaign.Channels(Campaign.Channels(Chan).ConnectedChannel).BookingTypes(BT).Compensations
                                        Row = Row + 1
                                        .cells(Row, 2).value = Campaign.Channels(Chan).ConnectedChannel
                                        .Cells(Row, 3).NumberFormat = "@"
                                        .Cells(Row, 4).NumberFormat = "@"
                                        .cells(Row, 3).value = Trinity.Helper.FormatDateForBooking(TmpComp.FromDate)
                                        .cells(Row, 4).value = Trinity.Helper.FormatDateForBooking(TmpComp.ToDate)
                                        .cells(Row, 5).numberformat = "##,##0.0"
                                        .cells(Row, 5).value = TmpComp.TRPs
                                        If TmpComp.Expense > 0 Then
                                            .cells(Row, 6).value = TmpComp.Expense
                                        End If
                                        .cells(Row, 7).value = TmpComp.Comment
                                        .range("G" & Row & ":H" & Row).merge()
                                        .Rows(Row).HorizontalAlignment = -4108
                                    Next
                                    With .range("B" & HeadlineRow & ":H" & Row)
                                        For x = 7 To 10
                                            .Borders(x).LineStyle = 1
                                            .Borders(x).Weight = 2
                                            .Borders(x).ColorIndex = -4105
                                        Next
                                    End With
                                    With .range("B" & HeadlineRow & ":H" & HeadlineRow + 1)
                                        For x = 7 To 10
                                            .Borders(x).LineStyle = 1
                                            .Borders(x).Weight = -4138
                                            .Borders(x).ColorIndex = -4105
                                        Next
                                        .interior.color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                        .font.color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                        .font.bold = True
                                    End With
                                    'If Row is below the bottom row, increase TopRow so that the bottom row is moved
                                    If Row > TopRow + 12 Then TopRow = Row - 11
                                End If
                                'If Campaign.Channels(Chan).BookingTypes(BT).Compensations.Count > 0 Then
                                '    Row = TopRow + 8
                                '    Dim HeadlineRow As Integer = Row
                                '    .range("B" & Row & ":G" & Row).merge()
                                '    .cells(Row, 2).value = LangIni.Text("Booking", "Compensations")
                                '    .cells(Row, 2).font.italic = True
                                '    Row = Row + 1
                                '    .Rows(Row).HorizontalAlignment = -4108
                                '    .cells(Row, 2).value = LangIni.Text("Booking", "From")
                                '    .cells(Row, 3).value = LangIni.Text("Booking", "To")
                                '    .cells(Row, 4).value = LangIni.Text("Booking", "TRP")
                                '    .cells(Row, 5).value = LangIni.Text("Booking", "Comments")
                                '    .range("E" & Row & ":G" & Row).merge()
                                '    For Each TmpComp As Trinity.cCompensation In Campaign.Channels(Chan).BookingTypes(BT).Compensations
                                '        Row = Row + 1
                                '        .cells(TopRow + 13, 1).NUMBERFORMAT = "@"
                                '        .cells(TopRow + 13, 1).NUMBERFORMAT = "@"
                                '        .cells(Row, 2).value = Trinity.Helper.FormatDateForBooking(TmpComp.FromDate)
                                '        .cells(Row, 3).value = Trinity.Helper.FormatDateForBooking(TmpComp.ToDate)
                                '        .cells(Row, 4).numberformat = "##,##0.0"
                                '        .cells(Row, 4).value = TmpComp.TRPs
                                '        .cells(Row, 5).value = TmpComp.Comment
                                '        .range("E" & Row & ":G" & Row).merge()
                                '        .Rows(Row).HorizontalAlignment = -4108
                                '    Next
                                '    With .range("B" & HeadlineRow & ":G" & Row)
                                '        For x = 7 To 10
                                '            .Borders(x).LineStyle = 1
                                '            .Borders(x).Weight = 2
                                '            .Borders(x).ColorIndex = -4105
                                '        Next
                                '    End With
                                '    With .range("B" & HeadlineRow & ":G" & HeadlineRow + 1)
                                '        For x = 7 To 10
                                '            .Borders(x).LineStyle = 1
                                '            .Borders(x).Weight = -4138
                                '            .Borders(x).ColorIndex = -4105
                                '        Next
                                '        .interior.color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                '        .font.color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                '        .font.bold = True
                                '    End With
                                'End If
                                With .Range("A1:M" & TopRow + 13)
                                    For q = 7 To 10
                                        .Borders(q).LineStyle = 1
                                        .Borders(q).Weight = 3
                                        .Borders(q).Color = 0
                                    Next
                                End With
                                .Columns(2).ColumnWidth = 9.57
                                .Columns(7).autofit()
                                .Range("A" & TopRow + 13 & ":M" & TopRow + 13).MERGE()
                                .cells(TopRow + 13, 1).interior.Color = 0
                                .cells(TopRow + 13, 1).Font.Color = RGB(255, 255, 255)
                                .cells(TopRow + 13, 1).Font.Italic = True
                                .cells(TopRow + 13, 1).Font.Bold = True
                                .cells(TopRow + 13, 1).NUMBERFORMAT = "yyyy-mm-dd"
                                .cells(TopRow + 13, 1).horizontalalignment = -4108
                                .cells(TopRow + 13, 1) = Now
                                Excel.Windows(WB.Name).View = 2
                                While .VPageBreaks.count > 0
                                    .VPageBreaks(1).DragOff(Direction:=-4161, RegionIndex:=1)
                                End While
                                .PageSetup.PrintArea = .Range("A1:M" & TopRow + 13).Address
                                Excel.Windows(WB.Name).View = 1
                                .Rows(23).Insert()
                                If cmbLogo.SelectedIndex > 0 Then
                                    .pictures.Insert(DataPath & "Logos\" + cmbLogo.Text) 'LocalDataPath & "\" & Inifile.Text("Logos", "Path" & cmbLogo.ListIndex))
                                    Scal = 180 / .pictures(1).Width
                                    Scal = 1
                                    .pictures(1).ScaleWidth(Scal, 0, 0)
                                    .pictures(1).ScaleHeight(Scal, 0, 0)
                                    .pictures(1).Top = 10
                                    .Rows(1).RowHeight = .pictures(1).Height + 10
                                    If cmbAlign.SelectedIndex = 1 Then
                                        .pictures(1).Left = .Columns(14).Left / 2 - .pictures(1).Width / 2
                                    ElseIf cmbAlign.SelectedIndex = 2 Then
                                        .pictures(1).Left = .Columns(14).Left - .pictures(1).Width - 10
                                    Else
                                        .pictures(1).Left = 10
                                    End If

                                End If
                            End With

                        Else

                            'RBS
                            'get info from ini if we are to print indexes or not
                            Dim AreaIni As New Trinity.clsIni
                            Dim printIndex As Boolean
                            Dim tmp As String
                            tmp = cmdLanguage.Tag
                            tmp = tmp.Substring(0, tmp.LastIndexOf("\") + 1)
                            tmp = tmp + "Area.ini"
                            AreaIni.Create(tmp)
                            printIndex = AreaIni.ReadBoolean("Print", "Rbsindex")


                            Excel.DisplayAlerts = False
                            .AllCells.Font.Size = 9
                            .AllCells.Font.Name = TrinitySettings.SchemeFont(cmbColorScheme.SelectedIndex + 1)
                            .AllCells.Interior.Color = RGB(255, 255, 255)
                            .Columns(1).ColumnWidth = 8
                            .Columns(2).ColumnWidth = 9.57
                            .Columns(3).ColumnWidth = 13.57
                            .Columns(4).ColumnWidth = 9.29
                            .Columns(5).ColumnWidth = 8.71
                            .Range("F:K").ColumnWidth = 8.86
                            .Range("L:L").ColumnWidth = 3.43

                            'a huge if for combination/normal bookings
                            If combination Is Nothing Then
                                .Cells(3, 1) = Campaign.Channels(Chan).ChannelName & " - " & LangIni.Text("Booking", Campaign.Channels(Chan).BookingTypes(BT).Name) & " " & LangIni.Text("Booking", "Booking")
                                .Cells(5, 9) = Campaign.Channels(Chan).ChannelName
                                .Cells(5, 1) = LangIni.Text("Booking", "To")
                                .Cells(5, 3) = grdContacts.Rows(i).Cells(0).Value
                                .Cells(7, 1) = LangIni.Text("Booking", "From")
                                .Cells(7, 3) = TrinitySettings.UserName
                                .Cells(8, 3) = TrinitySettings.UserEmail
                                .Cells(9, 3) = TrinitySettings.UserPhoneNr
                                .Cells(11, 1) = LangIni.Text("Booking", "Advertiser")
                                .Cells(11, 3) = Campaign.Client
                                .Cells(13, 1) = LangIni.Text("Booking", "Product")
                                .Cells(13, 3) = Campaign.Product
                                .range("A5:A19").font.bold = True

                                If chkCustomOrderPlacer.Checked Then
                                    Dim addressSplit As String() = Strings.Split(cmbOrderPlacer.Text, ",")
                                    .cells(7, 7) = "Booked on behalf of:"
                                    .cells(7, 7).font.bold = True
                                    Dim addressRow As Integer = 0
                                    For Each rowstring As String In addressSplit
                                        .cells(7 + addressRow, 8) = rowstring
                                        addressRow += 1
                                    Next
                                    .range("H7:H10").font.color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                End If

                                If chkCustomBilling.Checked Then
                                    Dim addressSplit As String() = Strings.Split(cmbBillingAddress.Text, ",")
                                    .cells(11, 7) = "Please bill:"
                                    .cells(11, 7).font.bold = True
                                    Dim addressRow As Integer = 0
                                    For Each rowstring As String In addressSplit
                                        .cells(11 + addressRow, 8) = rowstring
                                        addressRow += 1
                                    Next
                                    .range("H11:H14").font.color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                End If

                                With .Range("A3:L3")
                                    .Merge()
                                    .HorizontalAlignment = -4108
                                    .Font.Bold = True
                                    .Font.Size = 16
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                End With
                                .Range("C7:C21").Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                .Cells(21, 5).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")

                                With .Range("G23:J23")
                                    .HorizontalAlignment = -4108
                                    .Font.Bold = True
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                    .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                    '.Interior.Color = RGB(Trinity.Helper.ConvertIntToARGB(TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")).R, Trinity.Helper.ConvertIntToARGB(TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")).G, Trinity.Helper.ConvertIntToARGB(TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")).B)
                                    For x = 7 To 10
                                        .Borders(x).LineStyle = 1
                                        .Borders(x).Weight = 2
                                        .Borders(x).Color = 0
                                    Next
                                End With
                                If DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) = DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) Then
                                    PeriodStr = LangIni.Text("Spotlist", "Week") & " " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & ", " & Year(Date.FromOADate(Campaign.StartDate))
                                Else
                                    PeriodStr = LangIni.Text("Spotlist", "Week") & " " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & "-" & DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & ", " & Year(Date.FromOADate(Campaign.StartDate))
                                End If
                                .Cells(15, 1) = LangIni.Text("Booking", "CampaignPeriod")
                                .Cells(15, 3) = PeriodStr
                                .Cells(17, 1) = LangIni.Text("Booking", "BookingPeriod")
                                .Cells(17, 7) = LangIni.Text("Booking", "Ordernumber")
                                .Cells(16, 7) = "Channel reference:"
                                .cells(16, 9) = Campaign.Channels(Chan).BookingTypes(BT).ContractNumber
                                .Cells(17, 7).Font.Bold = True

                                If Campaign.Area = "SE" AndAlso (TrinitySettings.UserEmail.Contains("mecglobal") Or TrinitySettings.UserEmail.Contains("maxus")) Then
                                    With .range("G18:K21")
                                        .merge()
                                        .value = "Vi ber er ange ovanstående ordernummer vid all korrespondens angående denna order. " & _
                                        "Detta ordernummer måste uppges på er faktura. Fakturor utan ordernummer kommer att skickas " & _
                                        "tillbaka utan att betalas."
                                        .font.size = 8
                                        .wraptext = True
                                    End With
                                ElseIf Campaign.Area = "NO" Then
                                    With .range("G18:K21")
                                        .merge()
                                        .value = "Vi ber dere angi ovenstående ordrenummer ved all korrespondanse angående denne ordren." & _
                                        "Ordrenummeret må oppgis på faktura. Fakturaer uten ordrenummer vil returneres uten å betales"
                                        .font.size = 8
                                        .wraptext = True
                                    End With
                                End If

                                'Hannes added this block
                                '.cells(18, 7) = LangIni.Text("Booking", "NameContactBilling")
                                '.cells(18, 8) = TrinitySettings.NameContactBilling
                                '.cells(19, 7) = LangIni.Text("Booking", "PhoneContactBilling")
                                '.cells(19, 8) = TrinitySettings.PhoneContactBilling

                                If printIndex Then
                                    .Cells(17, 9) = Campaign.Channels(Chan).BookingTypes(BT).OrderNumber
                                End If
                                With .Range("G16:I17")
                                    For x = 7 To 10
                                        .Borders(x).LineStyle = 1
                                        .Borders(x).Weight = 2
                                        .Borders(x).Color = 0
                                    Next
                                    .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                End With


                                'if Bookingcode is wanted we print the markers for it
                                If Campaign.Channels(Chan).BookingTypes(BT).PrintBookingCode Then
                                    With .Range("G15:I15")
                                        For x = 7 To 10
                                            .Borders(x).LineStyle = 1
                                            .Borders(x).Weight = 2
                                            .Borders(x).Color = 0
                                        Next
                                        .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                        .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                    End With
                                    .Cells(15, 7) = LangIni.Text("Booking", "Bookingcode")
                                    .Cells(15, 7).Font.Bold = True
                                End If

                                Targ = Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.TargetName

                                UniSize = Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.getUniSizeUni(Campaign.Channels(Chan).BookingTypes(BT).Weeks(CInt(Campaign.Channels(Chan).BookingTypes(BT).Weeks.Count - 1)).StartDate)
                                'Dim weeknos As Object = Campaign.Channels(Chan).BookingTypes(BT).Weeks(CInt(Campaign.Channels(Chan).BookingTypes(BT).Weeks.Count - 1)).StartDate

                                '.Cells(15, 9) = Campaign.Channels(Chan).BookingTypes(BT).OrderNumber

                                .Cells(19, 1) = LangIni.Text("Booking", "Target")
                                Select Case Targ.Substring(0, 1)
                                    Case "A" : Gender = LangIni.Text("Targets", "All") & " "
                                    Case "K" : Gender = LangIni.Text("Targets", "Women") & " "
                                    Case "M" : Gender = LangIni.Text("Targets", "Men") & " "
                                    Case "W" : Gender = LangIni.Text("Targets", "Women") & " "
                                    Case "0" To "9" : Gender = LangIni.Text("Targets", "All") & " "
                                    Case Else : Gender = ""
                                End Select
                                Targ = Gender & " " & Targ
                                'Targ = Gender + Mid(Targ, 2)
                                .Cells(19, 3) = Targ & " (" & Format(UniSize, "N0") & ")"
                                .Cells(21, 3) = LangIni.Text("Booking", "CPP30")
                                .Cells(21, 4) = LangIni.Text("Booking", "CPT")
                                .Cells(22, 1) = LangIni.Text("Booking", "Gross")
                                Dim Idx As Decimal = 1

                                For Each TmpIndex As Trinity.cIndex In Campaign.Channels(Chan).BookingTypes(BT).Indexes
                                    If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
                                        If (TmpIndex.FromDate.ToOADate <= Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).StartDate And TmpIndex.ToDate.ToOADate >= Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).StartDate) Or (TmpIndex.FromDate.ToOADate <= Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).EndDate And TmpIndex.ToDate.ToOADate >= Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).EndDate) Then
                                            Idx = Idx * (TmpIndex.Index / 100)
                                        End If
                                    End If
                                Next
                                If Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).SpotIndex(True) > 0 Then
                                    Idx *= Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).SpotIndex(True) / 100
                                End If

                                .Cells(22, 3) = Format(Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.GetCPPForDate(Campaign.StartDate), "0.0")
                                .Cells(23, 1) = LangIni.Text("Booking", "Discount")
                                .Cells(23, 3) = Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Discount
                                .Cells(23, 4) = Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.Discount
                                .Cells(22, 4) = (Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.GetCPPForDate(Campaign.StartDate) / Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.getUniSizeUni(0)) * 100
                                .Cells(24, 1) = LangIni.Text("Booking", "Net")
                                .cells(24, 3) = Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.NetCPP
                                .cells(24, 4) = (Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.NetCPP / Campaign.Channels(Chan).BookingTypes(BT).BuyingTarget.getUniSizeUni(0)) * 100
                                .Cells(21, 4).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                .Cells(22, 4).NumberFormat = "##,##0.0"
                                .Cells(24, 4).NumberFormat = "##,##0.0"
                                .Cells(22, 3).NumberFormat = "##,##0.0"
                                .Cells(24, 3).NumberFormat = "##,##0.0"
                                .Cells(21, 3).Font.Bold = True
                                .Cells(21, 4).Font.Bold = True
                                .Cells(22, 1).Font.Bold = True
                                .Cells(23, 1).Font.Bold = True
                                .Cells(24, 1).Font.Bold = True
                                .Cells(23, 3).NumberFormat = "0.0%"
                                .Cells(23, 4).NumberFormat = "0.0%"
                                .range("C21:D24").HorizontalAlignment = -4108


                                'if we are supposed to print dayparts
                                If Campaign.Channels(Chan).BookingTypes(BT).PrintDayparts Then
                                    With .Range("B26:" & Chr(65 + Campaign.Channels(Chan).BookingTypes(BT).Dayparts.Count) & "26")
                                        .Merge()
                                        .HorizontalAlignment = -4108
                                        .Font.Bold = True
                                        .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                        .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")

                                        For x = 7 To 10
                                            .Borders(x).LineStyle = 1
                                            .Borders(x).Weight = 2
                                            .Borders(x).Color = 0
                                        Next
                                    End With
                                    With .Range("B26:" & Chr(65 + Campaign.Channels(Chan).BookingTypes(BT).Dayparts.Count) & "28")
                                        .HorizontalAlignment = -4108
                                        For x = 7 To 10
                                            .Borders(x).LineStyle = 1
                                            .Borders(x).Weight = 2
                                            .Borders(x).Color = 0
                                        Next
                                    End With
                                    .Cells(26, 2) = LangIni.Text("Booking", "DaypartSplit")
                                    For dp = 0 To Campaign.Channels(Chan).BookingTypes(BT).Dayparts.Count - 1
                                        If LangIni.Text("Booking", Campaign.Channels(Chan).BookingTypes(BT).Dayparts(dp).Name) <> "" Then
                                            .Cells(27, 2 + dp) = LangIni.Text("Booking", Campaign.Channels(Chan).BookingTypes(BT).Dayparts(dp).Name)
                                        Else
                                            .Cells(27, 2 + dp) = Campaign.Channels(Chan).BookingTypes(BT).Dayparts(dp).Name
                                        End If
                                    Next
                                    For x = 2 To Campaign.Channels(Chan).BookingTypes(BT).Dayparts.Count + 1
                                        .Cells(28, x) = Campaign.Channels(Chan).BookingTypes(BT).Dayparts(x - 2).Share / 100
                                        .Cells(28, x).NumberFormat = "0%"
                                        .Cells(28, x).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                    Next
                                End If
                                .Cells(23, 7) = LangIni.Text("Booking", "Filmcode")
                                .Cells(23, 8) = LangIni.Text("Booking", "Spotlength")
                                .Cells(23, 9) = LangIni.Text("Booking", "Share")
                                .Cells(23, 10) = LangIni.Text("Booking", "TRP")
                                .Cells(23, 7).HorizontalAlignment = -4108
                                .Cells(23, 8).HorizontalAlignment = -4108
                                .Cells(23, 9).HorizontalAlignment = -4108
                                .Cells(23, 10).HorizontalAlignment = -4108
                                Dim FilmTRP(Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films.Count)
                                TotTRP = 0
                                For x = 1 To Campaign.Channels(Chan).BookingTypes(BT).Weeks.Count
                                    If DirectCast(mnuWeeks.Items("mnuWeek" & Campaign.Channels(Chan).BookingTypes(BT).Weeks(x).Name), Windows.Forms.ToolStripMenuItem).Checked Then
                                        For y = 1 To Campaign.Channels(Chan).BookingTypes(BT).Weeks(x).Films.Count
                                            FilmTRP(y) += (Campaign.Channels(Chan).BookingTypes(BT).Weeks(x).TRPBuyingTarget * (Campaign.Channels(Chan).BookingTypes(BT).Weeks(x).Films(y).Share / 100))
                                            TotTRP = TotTRP + (Campaign.Channels(Chan).BookingTypes(BT).Weeks(x).TRPBuyingTarget * (Campaign.Channels(Chan).BookingTypes(BT).Weeks(x).Films(y).Share / 100))
                                        Next
                                    End If
                                Next
                                r = 1
                                For x = 1 To Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films.Count
                                    If FilmTRP(x) > 0 Then
                                        .Cells(24 + r, 6) = x
                                        .Cells(24 + r, 6).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                        .Cells(24 + r, 6).HorizontalAlignment = -4152
                                        .Cells(24 + r, 7).NumberFormat = "@"
                                        If Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(x).Filmcode <> "" Then
                                            .Cells(24 + r, 7) = Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(x).Filmcode
                                        Else
                                            .Cells(24 + r, 7) = Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(x).Name
                                        End If
                                        .Cells(24 + r, 7).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                        .Cells(24 + r, 7).Font.Bold = True
                                        .Cells(24 + r, 8) = Campaign.Channels(Chan).BookingTypes(BT).Weeks(1).Films(x).FilmLength
                                        .Cells(24 + r, 8).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                        If TotTRP > 0 Then
                                            .Cells(24 + r, 9) = FilmTRP(x) / TotTRP
                                        Else
                                            .Cells(24 + r, 9) = 0
                                        End If
                                        .Cells(24 + r, 9).NumberFormat = "0%"
                                        .Cells(24 + r, 9).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                        .Cells(24 + r, 10) = FilmTRP(x)
                                        .Cells(24 + r, 10).NumberFormat = "##,##0.0"
                                        .Cells(24 + r, 10).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                        r = r + 1
                                    End If
                                Next
                                With .Range("G23:J" & 24 + r)
                                    For z = 7 To 11
                                        .Borders(z).LineStyle = 1
                                        .Borders(z).Weight = 2
                                        .Borders(z).Color = 0
                                    Next
                                    .HorizontalAlignment = -4108
                                End With
                                TopRow = 26 + r

                                'here we start with printing the booking itself, all before is just channel/planner information
                                If TopRow < 30 Then TopRow = 30

                                'if we are supposed to print indexes we do so
                                Dim colCPP As Integer
                                Dim colBudget As Integer
                                If printIndex Then
                                    colCPP = 10
                                    colBudget = 12
                                Else
                                    colCPP = 9
                                    colBudget = 11
                                End If

                                .Cells(TopRow, 1) = LangIni.Text("Booking", "Week")
                                .Cells(TopRow, 2) = LangIni.Text("Booking", "CampaignPeriod")
                                .Range("B" & TopRow & ":C" & TopRow).Merge()
                                .Range("B" & TopRow & ":C" & TopRow).HorizontalAlignment = -4108
                                .Range("B" & TopRow + 1 & ":C" & TopRow + 1).Merge()
                                .Cells(TopRow, 4) = LangIni.Text("Booking", "TRPShare")
                                .Range("D" & TopRow & ":E" & TopRow).Merge()
                                .Cells(TopRow, 6) = LangIni.Text("Booking", "FilmSplit")
                                .Range("F" & TopRow & ":H" & TopRow).Merge()
                                .Range("F" & TopRow & ":H" & TopRow).HorizontalAlignment = -4108
                                .Cells(TopRow, colCPP) = LangIni.Text("Booking", "CPP")
                                .Cells(TopRow, colBudget) = LangIni.Text("Booking", "Budget")
                                .Cells(TopRow, colBudget - 1) = LangIni.Text("Booking", "Budget")
                                .Cells(TopRow + 1, 8) = LangIni.Text("Booking", "TRP")
                                .Cells(TopRow + 1, colBudget - 1) = LangIni.Text("Booking", "Filmcode")
                                .Cells(TopRow + 1, colBudget) = LangIni.Text("Booking", "Week")
                                .Cells(TopRow + 1, 4) = "%"
                                .Cells(TopRow + 1, 5) = LangIni.Text("Booking", "TRP")
                                .cells(TopRow + 1, 6) = LangIni.Text("Booking", "Filmcode")
                                .Cells(TopRow + 1, 7) = "%"
                                .Cells(TopRow + 1, 8) = LangIni.Text("Booking", "TRP")
                                If printIndex Then
                                    .Cells(TopRow + 1, 9) = LangIni.Text("Booking", "Index") & "*"
                                    RubrRange = "A" & TopRow & ":L" & TopRow + 1
                                Else
                                    RubrRange = "A" & TopRow & ":K" & TopRow + 1
                                End If

                                TopRow = TopRow + 1
                                TotBud = 0
                                z = 1
                                Dim RowColors(0 To 1) As Integer
                                Dim Color As Integer = 0
                                RowColors(0) = 48
                                RowColors(1) = 15
                                For w = 1 To Campaign.Channels(Chan).BookingTypes(BT).Weeks.Count
                                    If DirectCast(mnuWeeks.Items("mnuWeek" & Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).Name), Windows.Forms.ToolStripMenuItem).Checked Then

                                        FirstRow = TopRow + z
                                        .Cells(TopRow + z, 1) = Trim(Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).Name)
                                        .Cells(TopRow + z, 2) = Trinity.Helper.FormatShortDateForBooking(Date.FromOADate(Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).StartDate)) & " - " & Trinity.Helper.FormatShortDateForBooking(Date.FromOADate(Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).EndDate))
                                        .Range("B" & TopRow + z & ":C" & TopRow + z).Merge()
                                        .Range("B" & TopRow + z & ":C" & TopRow + z).HorizontalAlignment = -4108
                                        .Cells(TopRow + z, 4).NumberFormat = "0%"
                                        .Cells(TopRow + z, 5) = Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).TRPBuyingTarget
                                        .Cells(TopRow + z, 5).NumberFormat = "0.0"
                                        If printIndex Then
                                            .Cells(TopRow + z, 9) = Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).Index() * 100
                                            .Cells(TopRow + z, 9).NumberFormat = "0"
                                        End If
                                        .Cells(TopRow + z, colBudget).NumberFormat = "#,##0 $"
                                        .Cells(TopRow + z, colBudget) = Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).NetBudget
                                        TotBud = TotBud + Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).NetBudget
                                        For q = 1 To Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).Films.Count
                                            If Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).Films(q).Share > 0 Then
                                                CPP = Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).NetCPP30 * (Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).Films(q).Index / 100)
                                                .Cells(TopRow + z, colCPP).NumberFormat = "#,#0.0"
                                                .Cells(TopRow + z, colCPP) = CPP
                                                .Cells(TopRow + z, colBudget - 1).NumberFormat = "#,##0 $"
                                                .Cells(TopRow + z, colBudget - 1) = Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).NetBudget * (Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).Films(q).Share(Trinity.cFilm.FilmShareEnum.fseBudget) / 100)
                                                If Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).Films(q).Filmcode.TrimEnd(",") <> "" Then
                                                    .Cells(TopRow + z, 6) = Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).Films(q).Filmcode.TrimEnd(",")
                                                Else
                                                    .Cells(TopRow + z, 6) = Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).Films(q).Name
                                                End If
                                                .Cells(TopRow + z, 7).NumberFormat = "0%"
                                                .Cells(TopRow + z, 7) = Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).Films(q).Share / 100
                                                .Cells(TopRow + z, 8).NumberFormat = "#,#0.0"
                                                .Cells(TopRow + z, 8) = (Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).Films(q).Share / 100) * Campaign.Channels(Chan).BookingTypes(BT).Weeks(w).TRPBuyingTarget
                                                If printIndex Then
                                                    .range("A" & TopRow + z & ":L" & TopRow + z).Interior.ColorIndex = RowColors(Color)
                                                Else
                                                    .range("A" & TopRow + z & ":K" & TopRow + z).Interior.ColorIndex = RowColors(Color)
                                                End If
                                                z = z + 1
                                            End If
                                        Next
                                        While TopRow + z - 1 < FirstRow
                                            z = z + 1
                                        End While
                                        .Range("A" & FirstRow & ":A" & TopRow + z - 1).Merge()
                                        .Range("B" & FirstRow & ":C" & TopRow + z - 1).Merge()
                                        .Range("D" & FirstRow & ":D" & TopRow + z - 1).Merge()
                                        .Range("E" & FirstRow & ":E" & TopRow + z - 1).Merge()
                                        If printIndex Then
                                            .Range("L" & FirstRow & ":L" & TopRow + z - 1).Merge()
                                        Else
                                            .Range("K" & FirstRow & ":K" & TopRow + z - 1).Merge()
                                        End If
                                        Color = 1 + (Color = 1)
                                    End If
                                Next
                                For rr As Integer = TopRow + 1 To TopRow + z - 1
                                    .Cells(rr, 4).Formula = "=" & .Cells(rr, 5).Address & "/" & .Cells(TopRow + z, 5).Address
                                Next
                                If printIndex Then
                                    With .Range("A" & TopRow + 1 & ":L" & TopRow + z)
                                        .HorizontalAlignment = -4108
                                        For q = 7 To 12
                                            .Borders(q).LineStyle = 1
                                            .Borders(q).Weight = 2
                                            .Borders(q).Color = 0
                                        Next
                                    End With
                                Else
                                    With .Range("A" & TopRow + 1 & ":K" & TopRow + z)
                                        .HorizontalAlignment = -4108
                                        For q = 7 To 12
                                            .Borders(q).LineStyle = 1
                                            .Borders(q).Weight = 2
                                            .Borders(q).Color = 0
                                        Next
                                    End With
                                End If
                                .Cells(TopRow + z, 1) = LangIni.Text("Booking", "Total")
                                .Cells(TopRow + z, 4) = "100%"
                                .Cells(TopRow + z, 5).Formula = "=SUM(" & .Cells(TopRow + 1, 5).Address & ":" & .Cells(TopRow + z - 1, 5).Address
                                .Cells(TopRow + z, colBudget).NumberFormat = "#,##0 $"
                                .Cells(TopRow + z, colBudget).Formula = "=SUM(" & .Cells(TopRow + 1, colBudget).Address & ":" & .Cells(TopRow + z - 1, colBudget).Address
                                .Columns(11).AutoFit()
                                .Range("B" & TopRow + z & ":C" & TopRow + z).Merge()
                                .Range("B" & TopRow + z & ":C" & TopRow + z).HorizontalAlignment = -4108
                                .Range("F" & TopRow + z & ":H" & TopRow + z).Merge()
                                .Range("F" & TopRow + z & ":H" & TopRow + z).HorizontalAlignment = -4108
                                TopRow = TopRow + z + 3
                                If printIndex AndAlso Campaign.Channels(Chan).BookingTypes(BT).Indexes.Count > 0 Then
                                    .cells(TopRow, 1) = "*" & LangIni.Text("Booking", "Indexes")
                                    .cells(TopRow + 1, 1) = LangIni.Text("Booking", "Name")
                                    .cells(TopRow + 1, 2) = LangIni.Text("Spotlist", "Period")
                                    .cells(TopRow + 1, 4) = LangIni.Text("Booking", "Index")
                                    With .Range("A" & TopRow & ":L" & TopRow + 1)
                                        .Font.Bold = True
                                        .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                        .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                        For q = 7 To 10
                                            .Borders(q).LineStyle = 1
                                            .Borders(q).Weight = 2
                                            .Borders(q).Color = 0
                                        Next
                                    End With
                                    TopRow += 1
                                    For Each TmpIndex As Trinity.cIndex In Campaign.Channels(Chan).BookingTypes(BT).Indexes
                                        If (TmpIndex.ToDate.ToOADate >= Campaign.StartDate And TmpIndex.ToDate.ToOADate <= Campaign.EndDate) Or (TmpIndex.FromDate.ToOADate >= Campaign.StartDate And TmpIndex.FromDate.ToOADate <= Campaign.EndDate) Or (TmpIndex.FromDate.ToOADate <= Campaign.StartDate And TmpIndex.ToDate.ToOADate >= Campaign.EndDate) Then
                                            TopRow += 1
                                            .cells(TopRow, 1) = TmpIndex.Name
                                            .range("B" & TopRow & ":C" & TopRow).merge()
                                            .cells(TopRow, 2) = Trinity.Helper.FormatShortDateForBooking(TmpIndex.FromDate) & " - " & Trinity.Helper.FormatShortDateForBooking(TmpIndex.ToDate)
                                            .cells(TopRow, 4).numberformat = "##,##0.0"
                                            .cells(TopRow, 4).HorizontalAlignment = -4131
                                            .cells(TopRow, 4) = TmpIndex.Index
                                        End If
                                    Next
                                    TopRow += 3
                                End If
                                .Cells(TopRow, 1) = LangIni.Text("Booking", "Comments")
                                .cells(TopRow + 1, 1) = Campaign.Channels(Chan).BookingTypes(BT).Comments
                                With .Range("A" & TopRow & ":L" & TopRow)
                                    .Font.Bold = True
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                    .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                    For q = 7 To 10
                                        .Borders(q).LineStyle = 1
                                        .Borders(q).Weight = 2
                                        .Borders(q).Color = 0
                                    Next
                                End With
                                .Cells(TopRow + 6, 1) = LangIni.Text("Booking", "Other")
                                With .Range("A" & TopRow + 6 & ":L" & TopRow + 6)
                                    .Font.Bold = True
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                    .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                    For x = 7 To 10
                                        .Borders(x).LineStyle = 1
                                        .Borders(x).Weight = 2
                                        .Borders(x).Color = 0
                                    Next
                                End With
                                With .Range(RubrRange)
                                    .HorizontalAlignment = -4108
                                    .Font.Bold = True
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                    .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                    For x = 7 To 11
                                        .Borders(x).LineStyle = 1
                                        .Borders(x).Weight = 3
                                        .Borders(x).Color = 0
                                    Next
                                End With

                                'Print compensations
                                If Campaign.Channels(Chan).BookingTypes(BT).Compensations.Count > 0 Then
                                    Row = TopRow + 8
                                    Dim HeadlineRow As Integer = Row
                                    .range("B" & Row & ":G" & Row).merge()
                                    .cells(Row, 2).value = LangIni.Text("Booking", "Compensations")
                                    .cells(Row, 2).font.italic = True
                                    Row = Row + 1
                                    .Rows(Row).HorizontalAlignment = -4108
                                    .cells(Row, 2).value = LangIni.Text("Booking", "From")
                                    .cells(Row, 3).value = LangIni.Text("Booking", "To")
                                    .cells(Row, 4).value = LangIni.Text("Booking", "TRP")
                                    If LangIni.Text("Booking", "Expense") = "" Then
                                        .cells(Row, 5).value = "Expense"
                                    Else
                                        .cells(Row, 5).value = LangIni.Text("Booking", "Expense")
                                    End If
                                    .cells(Row, 6).value = LangIni.Text("Booking", "Comments")
                                    .range("F" & Row & ":G" & Row).merge()
                                    For Each TmpComp As Trinity.cCompensation In Campaign.Channels(Chan).BookingTypes(BT).Compensations
                                        Row = Row + 1
                                        .Cells(Row, 2).NumberFormat = "@"
                                        .Cells(Row, 3).NumberFormat = "@"
                                        .cells(Row, 2).value = Trinity.Helper.FormatDateForBooking(TmpComp.FromDate)
                                        .cells(Row, 3).value = Trinity.Helper.FormatDateForBooking(TmpComp.ToDate)
                                        .cells(Row, 4).numberformat = "##,##0.0"
                                        .cells(Row, 4).value = TmpComp.TRPs
                                        If TmpComp.Expense > 0 Then
                                            .cells(Row, 5).value = TmpComp.Expense
                                        End If
                                        .cells(Row, 6).value = TmpComp.Comment
                                        .range("F" & Row & ":G" & Row).merge()
                                        .Rows(Row).HorizontalAlignment = -4108
                                    Next
                                    With .range("B" & HeadlineRow & ":G" & Row)
                                        For x = 7 To 10
                                            .Borders(x).LineStyle = 1
                                            .Borders(x).Weight = 2
                                            .Borders(x).ColorIndex = -4105
                                        Next
                                    End With
                                    With .range("B" & HeadlineRow & ":G" & HeadlineRow + 1)
                                        For x = 7 To 10
                                            .Borders(x).LineStyle = 1
                                            .Borders(x).Weight = -4138
                                            .Borders(x).ColorIndex = -4105
                                        Next
                                        .interior.color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                        .font.color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                        .font.bold = True
                                    End With
                                End If
                                Dim PRow As Integer = 0
                                If chkIncludePremiums.Checked Then
                                    Dim PremiumBT As Trinity.cBookingType = Nothing
                                    For Each TmpBT As Trinity.cBookingType In Campaign.Channels(Chan).BookingTypes
                                        If TmpBT.IsPremium AndAlso TmpBT.BookIt Then
                                            If Not PrintedPremiums.Contains(TmpBT) Then
                                                PremiumBT = TmpBT
                                                Exit For
                                            End If
                                        End If
                                    Next
                                    If Not PremiumBT Is Nothing Then
                                        .Cells(TopRow + 13 + PRow, 1) = LangIni.Text("Booking", "Premiums")
                                        With .Range("A" & TopRow + 13 + PRow & ":L" & TopRow + 13 + PRow)
                                            .Font.Bold = True
                                            .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                            .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                            For q = 7 To 10
                                                .Borders(q).LineStyle = 1
                                                .Borders(q).Weight = 2
                                                .Borders(q).Color = 0
                                            Next
                                        End With
                                        PRow += 1
                                        .Cells(TopRow + 13 + PRow, 1) = LangIni.Text("Booking", "Date")
                                        .Cells(TopRow + 13 + PRow, 2) = LangIni.Text("Booking", "Time")
                                        .Cells(TopRow + 13 + PRow, 3) = LangIni.Text("Booking", "Programme")
                                        .rows(TopRow + 13 + PRow).Font.Bold = True
                                        PRow += 1
                                        For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots
                                            If TmpSpot.Bookingtype Is PremiumBT Then
                                                .Cells(TopRow + 13 + PRow, 1) = Format(TmpSpot.AirDate, "Short date")
                                                .Cells(TopRow + 13 + PRow, 2) = Trinity.Helper.Mam2Tid(TmpSpot.MaM)
                                                .Cells(TopRow + 13 + PRow, 3) = TmpSpot.Programme
                                                .rows(TopRow + 13 + PRow).HorizontalAlignment = -4131
                                                PRow += 1
                                            End If
                                        Next
                                    End If
                                    PRow += 1
                                    .columns(1).ColumnWidth = 10
                                End If

                                With .Range("A1:L" & TopRow + 13 + PRow)
                                    For q = 7 To 10
                                        .Borders(q).LineStyle = 1
                                        .Borders(q).Weight = 3
                                        .Borders(q).Color = 0
                                    Next
                                End With
                                .Columns(7).AutoFit()
                                .Columns(12).AutoFit()
                                .Range("A" & TopRow + 13 + PRow & ":L" & TopRow + 13 + PRow).Merge()
                                If cmbLogo.SelectedIndex > 0 Then
                                    WB.Sheets(1).pictures.Insert(DataPath & "Logos\" + cmbLogo.Text) 'LocalDataPath & "\" & Inifile.Text("Logos", "Path" & cmbLogo.ListIndex))
                                    'Scal = 180 / WB.Sheets(1).pictures(1).Width
                                    Scal = 1
                                    WB.Sheets(1).pictures(1).ScaleWidth(Scal, 0, 0)
                                    WB.Sheets(1).pictures(1).ScaleHeight(Scal, 0, 0)
                                    WB.Sheets(1).pictures(1).Top = 10
                                    WB.Sheets(1).Rows(1).RowHeight = WB.Sheets(1).pictures(1).Height + 10
                                    If cmbAlign.SelectedIndex = 1 Then
                                        WB.Sheets(1).pictures(1).Left = WB.Sheets(1).Columns(13).Left / 2 - WB.Sheets(1).pictures(1).Width / 2
                                    ElseIf cmbAlign.SelectedIndex = 2 Then
                                        WB.Sheets(1).pictures(1).Left = WB.Sheets(1).Columns(13).Left - WB.Sheets(1).pictures(1).Width - 10
                                    Else
                                        WB.Sheets(1).pictures(1).Left = 10
                                    End If
                                End If
                                Excel.Windows(WB.Name).View = 2
                                While .VPageBreaks.count > 0
                                    .VPageBreaks(1).DragOff(Direction:=-4161, RegionIndex:=1)
                                End While
                                Excel.Windows(WB.Name).View = 1

                                .Cells(TopRow + 13 + PRow, 1).Interior.Color = 0
                                .Cells(TopRow + 13 + PRow, 1).Font.Color = RGB(255, 255, 255)
                                .Cells(TopRow + 13 + PRow, 1).Font.Italic = True
                                .Cells(TopRow + 13 + PRow, 1).Font.Bold = True
                                .Cells(TopRow + 13 + PRow, 1).NumberFormat = "yyyy-mm-dd"
                                .Cells(TopRow + 13 + PRow, 1).HorizontalAlignment = -4108
                                .Cells(TopRow + 13 + PRow, 1) = Now

                            Else 'start of combination print
                                .Cells(3, 1) = combination.Name & " - " & LangIni.Text("Booking", "RBS") & " " & LangIni.Text("Booking", "Booking")
                                .Cells(5, 9) = combination.Name
                                .Cells(5, 1) = LangIni.Text("Booking", "To")
                                .Cells(5, 3) = grdContacts.Rows(i).Cells(0).Value
                                .Cells(7, 1) = LangIni.Text("Booking", "From")
                                .Cells(7, 3) = TrinitySettings.UserName
                                .Cells(8, 3) = TrinitySettings.UserEmail
                                .Cells(9, 3) = TrinitySettings.UserPhoneNr
                                .Cells(11, 1) = LangIni.Text("Booking", "Advertiser")
                                .Cells(11, 3) = Campaign.Client
                                .Cells(13, 1) = LangIni.Text("Booking", "Product")
                                .Cells(13, 3) = Campaign.Product
                                .range("A5:A19").font.bold = True
                                With .Range("A3:L3")
                                    .Merge()
                                    .HorizontalAlignment = -4108
                                    .Font.Bold = True
                                    .Font.Size = 16
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                End With
                                .Range("C7:C21").Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                .Cells(21, 5).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")

                                With .Range("G23:J23")
                                    .HorizontalAlignment = -4108
                                    .Font.Bold = True
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                    .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                    For x = 7 To 10
                                        .Borders(x).LineStyle = 1
                                        .Borders(x).Weight = 2
                                        .Borders(x).Color = 0
                                    Next
                                End With
                                If DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) = DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) Then
                                    PeriodStr = LangIni.Text("Spotlist", "Week") & " " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & ", " & Year(Date.FromOADate(Campaign.StartDate))
                                Else
                                    PeriodStr = LangIni.Text("Spotlist", "Week") & " " & DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & "-" & DatePart(DateInterval.WeekOfYear, Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & ", " & Year(Date.FromOADate(Campaign.StartDate))
                                End If
                                .Cells(15, 1) = LangIni.Text("Booking", "CampaignPeriod")
                                .Cells(15, 3) = PeriodStr
                                .Cells(17, 1) = LangIni.Text("Booking", "BookingPeriod")
                                .Cells(17, 7) = LangIni.Text("Booking", "Ordernumber")
                                .Cells(17, 7).Font.Bold = True
                                If printIndex Then
                                    .Cells(17, 9) = combination.Relations(1).Bookingtype.OrderNumber
                                End If
                                With .Range("G17:I17")
                                    For x = 7 To 10
                                        .Borders(x).LineStyle = 1
                                        .Borders(x).Weight = 2
                                        .Borders(x).Color = 0
                                    Next
                                    .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                End With


                                'if Bookingcode is wanted we print the markers for it
                                If combination.Relations(1).Bookingtype.PrintBookingCode Then
                                    With .Range("G15:I15")
                                        For x = 7 To 10
                                            .Borders(x).LineStyle = 1
                                            .Borders(x).Weight = 2
                                            .Borders(x).Color = 0
                                        Next
                                        .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                        .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                    End With
                                    .Cells(15, 7) = LangIni.Text("Booking", "Bookingcode")
                                    .Cells(15, 7).Font.Bold = True
                                End If

                                Targ = combination.BuyingTarget
                                UniSize = 0
                                For Each cc As Trinity.cCombinationChannel In combination.Relations
                                    UniSize += cc.Bookingtype.BuyingTarget.getUniSizeUni(0) * 1000 * cc.Percent
                                Next

                                .Cells(19, 1) = LangIni.Text("Booking", "Target")
                                Select Case Targ.Substring(0, 1)
                                    Case "A" : Gender = LangIni.Text("Targets", "All") & " "
                                    Case "K" : Gender = LangIni.Text("Targets", "Women") & " "
                                    Case "M" : Gender = LangIni.Text("Targets", "Men") & " "
                                    Case "W" : Gender = LangIni.Text("Targets", "Women") & " "
                                    Case "0" To "9" : Gender = LangIni.Text("Targets", "All") & " "
                                    Case Else : Gender = ""
                                End Select
                                Targ = Gender + Mid(Targ, 2)
                                .Cells(19, 3) = Targ & " (" & Format(UniSize, "N0") & ")"
                                .Cells(21, 3) = LangIni.Text("Booking", "CPP30")
                                .Cells(21, 4) = LangIni.Text("Booking", "CPT")
                                .Cells(22, 1) = LangIni.Text("Booking", "Gross")
                                Dim Idx As Decimal = 1


                                For Each TmpIndex As Trinity.cIndex In combination.Relations(1).Bookingtype.Indexes
                                    If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
                                        If (TmpIndex.FromDate.ToOADate <= combination.Relations(1).Bookingtype.Weeks(1).StartDate And TmpIndex.ToDate.ToOADate >= combination.Relations(1).Bookingtype.Weeks(1).StartDate) Or (TmpIndex.FromDate.ToOADate <= combination.Relations(1).Bookingtype.Weeks(1).EndDate And TmpIndex.ToDate.ToOADate >= combination.Relations(1).Bookingtype.Weeks(1).EndDate) Then
                                            Idx = Idx * (TmpIndex.Index / 100)
                                        End If
                                    End If
                                Next


                                Dim GrossCPP As Double = 0
                                Dim GrossCPT As Double = 0
                                Dim Discount As Double = 0
                                Dim NetCPT30 As Double = 0
                                Dim NetCPP30 As Double = 0

                                For Each cc As Trinity.cCombinationChannel In combination.Relations
                                    Dim G_CPP As Single = 0
                                    If cc.Bookingtype.BuyingTarget.CalcCPP Then
                                        For dp = 0 To cc.Bookingtype.Dayparts.Count - 1
                                            GrossCPP += cc.Bookingtype.BuyingTarget.GetCPPForDate(Campaign.StartDate, dp) * (cc.Bookingtype.Dayparts(dp).Share / 100) * cc.Percent
                                            G_CPP += cc.Bookingtype.BuyingTarget.GetCPPForDate(Campaign.StartDate, dp) * (cc.Bookingtype.Dayparts(dp).Share / 100)
                                        Next
                                    Else
                                        GrossCPP += cc.Bookingtype.BuyingTarget.GetCPPForDate(Campaign.StartDate) * cc.Percent
                                        G_CPP += cc.Bookingtype.BuyingTarget.GetCPPForDate(Campaign.StartDate)
                                    End If
                                    GrossCPT += (G_CPP / cc.Bookingtype.BuyingTarget.getUniSizeUni(0)) * 100 * cc.Percent
                                    Discount += cc.Bookingtype.BuyingTarget.Discount * cc.Percent
                                    NetCPT30 += (cc.Bookingtype.BuyingTarget.NetCPP / cc.Bookingtype.BuyingTarget.getUniSizeUni(0)) * 100 * cc.Percent
                                    NetCPP30 += cc.Bookingtype.BuyingTarget.NetCPP * cc.Percent
                                Next

                                .Cells(22, 3) = Format(GrossCPP, "0.0")
                                .Cells(23, 1) = LangIni.Text("Booking", "Discount")
                                .Cells(23, 3) = Discount
                                .Cells(23, 4) = Discount
                                .Cells(22, 4) = GrossCPT
                                .Cells(24, 1) = LangIni.Text("Booking", "Net")
                                .cells(24, 3) = NetCPP30
                                .Cells(24, 4) = NetCPT30
                                .Cells(21, 4).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                .Cells(22, 4).NumberFormat = "##,##0.0"
                                .Cells(24, 4).NumberFormat = "##,##0.0"
                                .Cells(22, 3).NumberFormat = "##,##0.0"
                                .Cells(24, 3).NumberFormat = "##,##0.0"
                                .Cells(21, 3).Font.Bold = True
                                .Cells(21, 4).Font.Bold = True
                                .Cells(22, 1).Font.Bold = True
                                .Cells(23, 1).Font.Bold = True
                                .Cells(24, 1).Font.Bold = True
                                .Cells(23, 3).NumberFormat = "0.0%"
                                .Cells(23, 4).NumberFormat = "0.0%"
                                .range("C21:D24").HorizontalAlignment = -4108


                                'if we are supposed to print dayparts
                                If combination.Relations(1).Bookingtype.PrintDayparts Then
                                    With .Range("B26:" & Chr(65 + combination.Relations(1).Bookingtype.Dayparts.Count) & "26")
                                        .Merge()
                                        .HorizontalAlignment = -4108
                                        .Font.Bold = True
                                        .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                        .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")

                                        For x = 7 To 10
                                            .Borders(x).LineStyle = 1
                                            .Borders(x).Weight = 2
                                            .Borders(x).Color = 0
                                        Next
                                    End With
                                    With .Range("B26:" & Chr(65 + combination.Relations(1).Bookingtype.Dayparts.Count) & "28")
                                        .HorizontalAlignment = -4108
                                        For x = 7 To 10
                                            .Borders(x).LineStyle = 1
                                            .Borders(x).Weight = 2
                                            .Borders(x).Color = 0
                                        Next
                                    End With
                                    .Cells(26, 2) = LangIni.Text("Booking", "DaypartSplit")
                                    For dp = 0 To combination.Relations(1).Bookingtype.Dayparts.Count - 1
                                        If LangIni.Text("Booking", combination.Relations(1).Bookingtype.Dayparts(dp).Name) <> "" Then
                                            .Cells(27, 2 + dp) = LangIni.Text("Booking", combination.Relations(1).Bookingtype.Dayparts(dp).Name)
                                        Else
                                            .Cells(27, 2 + dp) = combination.Relations(1).Bookingtype.Dayparts(dp).Name
                                        End If
                                    Next
                                    For x = 2 To combination.Relations(1).Bookingtype.Dayparts.Count + 1
                                        Dim split As Double = 0
                                        For Each cc As Trinity.cCombinationChannel In combination.Relations
                                            split += (cc.Bookingtype.Dayparts(x - 2).Share / 100) * cc.Percent
                                        Next
                                        .Cells(28, x) = split
                                        .Cells(28, x).NumberFormat = "0%"
                                        .Cells(28, x).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                    Next
                                End If
                                .Cells(23, 7) = LangIni.Text("Booking", "Filmcode")
                                .Cells(23, 8) = LangIni.Text("Booking", "Spotlength")
                                .Cells(23, 9) = LangIni.Text("Booking", "Share")
                                .Cells(23, 10) = LangIni.Text("Booking", "TRP")
                                .Cells(23, 7).HorizontalAlignment = -4108
                                .Cells(23, 8).HorizontalAlignment = -4108
                                .Cells(23, 9).HorizontalAlignment = -4108
                                .Cells(23, 10).HorizontalAlignment = -4108
                                Dim FilmTRP(combination.Relations(1).Bookingtype.Weeks(1).Films.Count)
                                TotTRP = 0
                                For x = 1 To combination.Relations(1).Bookingtype.Weeks.Count
                                    If DirectCast(mnuWeeks.Items("mnuWeek" & combination.Relations(1).Bookingtype.Weeks(x).Name), Windows.Forms.ToolStripMenuItem).Checked Then
                                        For y = 1 To combination.Relations(1).Bookingtype.Weeks(x).Films.Count
                                            For Each cc As Trinity.cCombinationChannel In combination.Relations
                                                FilmTRP(y) += cc.Bookingtype.Weeks(x).TRPBuyingTarget * (cc.Bookingtype.Weeks(x).Films(y).Share / 100)
                                                TotTRP += cc.Bookingtype.Weeks(x).TRPBuyingTarget * (cc.Bookingtype.Weeks(x).Films(y).Share / 100)
                                            Next
                                        Next
                                    End If
                                Next
                                r = 1
                                For x = 1 To combination.Relations(1).Bookingtype.Weeks(1).Films.Count
                                    If FilmTRP(x) > 0 Then
                                        .Cells(24 + r, 6) = x
                                        .Cells(24 + r, 6).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                        .Cells(24 + r, 6).HorizontalAlignment = -4152
                                        .Cells(24 + r, 7).NumberFormat = "@"
                                        If combination.Relations(1).Bookingtype.Weeks(1).Films(x).Filmcode <> "" Then
                                            .Cells(24 + r, 7) = combination.Relations(1).Bookingtype.Weeks(1).Films(x).Filmcode
                                        Else
                                            .Cells(24 + r, 7) = combination.Relations(1).Bookingtype.Weeks(1).Films(x).Name
                                        End If
                                        .Cells(24 + r, 7).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                        .Cells(24 + r, 7).Font.Bold = True
                                        .Cells(24 + r, 8) = combination.Relations(1).Bookingtype.Weeks(1).Films(x).FilmLength
                                        .Cells(24 + r, 8).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                        If TotTRP > 0 Then
                                            .Cells(24 + r, 9) = FilmTRP(x) / TotTRP
                                        Else
                                            .Cells(24 + r, 9) = 0
                                        End If
                                        .Cells(24 + r, 9).NumberFormat = "0%"
                                        .Cells(24 + r, 9).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                        .Cells(24 + r, 10) = FilmTRP(x)
                                        .Cells(24 + r, 10).NumberFormat = "##,##0.0"
                                        .Cells(24 + r, 10).Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline")
                                        r = r + 1
                                    End If
                                Next
                                With .Range("G23:J" & 24 + r)
                                    For z = 7 To 11
                                        .Borders(z).LineStyle = 1
                                        .Borders(z).Weight = 2
                                        .Borders(z).Color = 0
                                    Next
                                    .HorizontalAlignment = -4108
                                End With
                                TopRow = 26 + r


                                'here we start with printing the booking itself, all before is just channel/planner information
                                If TopRow < 30 Then TopRow = 30

                                'if we are supposed to print indexes we do so
                                Dim colCPP As Integer
                                Dim colBudget As Integer
                                If printIndex Then
                                    colCPP = 10
                                    colBudget = 12
                                Else
                                    colCPP = 9
                                    colBudget = 11
                                End If

                                .Cells(TopRow, 1) = LangIni.Text("Booking", "Week")
                                .Cells(TopRow, 2) = LangIni.Text("Booking", "CampaignPeriod")
                                .Range("B" & TopRow & ":C" & TopRow).Merge()
                                .Range("B" & TopRow & ":C" & TopRow).HorizontalAlignment = -4108
                                .Range("B" & TopRow + 1 & ":C" & TopRow + 1).Merge()
                                .Cells(TopRow, 4) = LangIni.Text("Booking", "TRPShare")
                                .Range("D" & TopRow & ":E" & TopRow).Merge()
                                .Cells(TopRow, 6) = LangIni.Text("Booking", "FilmSplit")
                                .Range("F" & TopRow & ":H" & TopRow).Merge()
                                .Range("F" & TopRow & ":H" & TopRow).HorizontalAlignment = -4108
                                .Cells(TopRow, colCPP) = LangIni.Text("Booking", "CPP")
                                .Cells(TopRow, colBudget - 1) = LangIni.Text("Booking", "Budget")
                                .Cells(TopRow, colBudget) = LangIni.Text("Booking", "Budget")
                                .Cells(TopRow + 1, 4) = "%"
                                .Cells(TopRow + 1, 5) = LangIni.Text("Booking", "TRP")
                                .cells(TopRow + 1, 6) = LangIni.Text("Booking", "Filmcode")
                                .Cells(TopRow + 1, 7) = "%"
                                .Cells(TopRow + 1, 8) = LangIni.Text("Booking", "TRP")
                                .Cells(TopRow + 1, colBudget - 1) = LangIni.Text("Booking", "Filmcode")
                                .Cells(TopRow + 1, colBudget) = LangIni.Text("Booking", "Week")
                                If printIndex Then
                                    .Cells(TopRow + 1, 9) = LangIni.Text("Booking", "Index") & "*"
                                    RubrRange = "A" & TopRow & ":L" & TopRow + 1
                                Else
                                    RubrRange = "A" & TopRow & ":K" & TopRow + 1
                                End If

                                TopRow = TopRow + 1
                                TotBud = 0
                                z = 1
                                Dim RowColors(0 To 1) As Integer
                                Dim Color As Integer = 0
                                RowColors(0) = 48
                                RowColors(1) = 15
                                For w = 1 To combination.Relations(1).Bookingtype.Weeks.Count
                                    If DirectCast(mnuWeeks.Items("mnuWeek" & combination.Relations(1).Bookingtype.Weeks(w).Name), Windows.Forms.ToolStripMenuItem).Checked Then

                                        FirstRow = TopRow + z
                                        .Cells(TopRow + z, 1) = Trim(combination.Relations(1).Bookingtype.Weeks(w).Name)
                                        .Cells(TopRow + z, 2) = Trinity.Helper.FormatShortDateForBooking(Date.FromOADate(combination.Relations(1).Bookingtype.Weeks(w).StartDate)) & " - " & Trinity.Helper.FormatShortDateForBooking(Date.FromOADate(combination.Relations(1).Bookingtype.Weeks(w).EndDate))
                                        .Range("B" & TopRow + z & ":C" & TopRow + z).Merge()
                                        .Range("B" & TopRow + z & ":C" & TopRow + z).HorizontalAlignment = -4108
                                        .Cells(TopRow + z, 4).NumberFormat = "0%"
                                        Dim trp As Double = 0
                                        Dim index As Double = 0
                                        Dim NetBudget As Double = 0
                                        CPP = 0
                                        For Each cc As Trinity.cCombinationChannel In combination.Relations
                                            trp += cc.Bookingtype.Weeks(w).TRPBuyingTarget
                                            index += cc.Bookingtype.Weeks(w).Index() * cc.Percent
                                            CPP += cc.Bookingtype.Weeks(w).NetCPP * cc.Percent
                                            NetBudget += cc.Bookingtype.Weeks(w).NetBudget
                                        Next
                                        .Cells(TopRow + z, 5) = trp
                                        .Cells(TopRow + z, 5).NumberFormat = "0.0"
                                        If printIndex Then
                                            .Cells(TopRow + z, 9) = index * 100
                                            .Cells(TopRow + z, 9).NumberFormat = "0"
                                        End If
                                        .Cells(TopRow + z, colBudget).NumberFormat = "#,##0 $"
                                        .Cells(TopRow + z, colBudget) = NetBudget
                                        TotBud = TotBud + NetBudget
                                        For q = 1 To combination.Relations(1).Bookingtype.Weeks(w).Films.Count

                                            Dim share As Double = 0
                                            For Each cc As Trinity.cCombinationChannel In combination.Relations
                                                share += cc.Bookingtype.Weeks(w).Films(q).Share * cc.Percent
                                            Next
                                            CPP = 0
                                            NetBudget = 0
                                            For Each cc As Trinity.cCombinationChannel In combination.Relations
                                                CPP += cc.Bookingtype.Weeks(w).NetCPP30 * (cc.Bookingtype.Weeks(w).Films(q).Index / 100) * cc.Percent
                                                NetBudget += cc.Bookingtype.Weeks(w).NetBudget * (cc.Bookingtype.Weeks(w).Films(q).Share(Trinity.cFilm.FilmShareEnum.fseBudget) / 100)
                                            Next
                                            If printIndex Then
                                                .range("A" & TopRow + z & ":L" & TopRow + z).Interior.ColorIndex = RowColors(Color)
                                            Else
                                                .range("A" & TopRow + z & ":K" & TopRow + z).Interior.ColorIndex = RowColors(Color)
                                            End If
                                            If Not share = 0 Then
                                                .Cells(TopRow + z, colCPP).NumberFormat = "#,#0.0"
                                                .Cells(TopRow + z, colCPP) = CPP
                                                .Cells(TopRow + z, colBudget - 1).NumberFormat = "#,##0 $"
                                                .Cells(TopRow + z, colBudget - 1) = NetBudget
                                                If combination.Relations(1).Bookingtype.Weeks(w).Films(q).Filmcode.TrimEnd(",") <> "" Then
                                                    .Cells(TopRow + z, 6) = combination.Relations(1).Bookingtype.Weeks(w).Films(q).Filmcode.TrimEnd(",")
                                                Else
                                                    .Cells(TopRow + z, 6) = combination.Relations(1).Bookingtype.Weeks(w).Films(q).Name
                                                End If
                                                .Cells(TopRow + z, 7).NumberFormat = "0%"
                                                .Cells(TopRow + z, 7) = share / 100
                                                .Cells(TopRow + z, 8).NumberFormat = "#,#0.0"
                                                .Cells(TopRow + z, 8) = (share / 100) * trp
                                                z += 1
                                            End If
                                        Next
                                        While TopRow + z - 1 < FirstRow
                                            z = z + 1
                                        End While
                                        .Range("A" & FirstRow & ":A" & TopRow + z - 1).Merge()
                                        .Range("B" & FirstRow & ":C" & TopRow + z - 1).Merge()
                                        .Range("D" & FirstRow & ":D" & TopRow + z - 1).Merge()
                                        .Range("E" & FirstRow & ":E" & TopRow + z - 1).Merge()
                                        If printIndex Then
                                            .Range("I" & FirstRow & ":I" & TopRow + z - 1).Merge()
                                            .Range("L" & FirstRow & ":L" & TopRow + z - 1).Merge()
                                        Else
                                            .Range("K" & FirstRow & ":K" & TopRow + z - 1).Merge()
                                        End If
                                        Color = 1 + (Color = 1)
                                    End If
                                Next
                                For rr As Integer = TopRow + 1 To TopRow + z - 1
                                    .Cells(rr, 4).Formula = "=" & .Cells(rr, 5).Address & "/" & .Cells(TopRow + z, 5).Address
                                Next
                                If printIndex Then
                                    With .Range("A" & TopRow + 1 & ":L" & TopRow + z)
                                        .HorizontalAlignment = -4108
                                        For q = 7 To 12
                                            .Borders(q).LineStyle = 1
                                            .Borders(q).Weight = 2
                                            .Borders(q).Color = 0
                                        Next
                                    End With
                                Else
                                    With .Range("A" & TopRow + 1 & ":K" & TopRow + z)
                                        .HorizontalAlignment = -4108
                                        For q = 7 To 12
                                            .Borders(q).LineStyle = 1
                                            .Borders(q).Weight = 2
                                            .Borders(q).Color = 0
                                        Next
                                    End With
                                End If
                                .Cells(TopRow + z, 1) = LangIni.Text("Booking", "Total")
                                .Cells(TopRow + z, 4) = "100%"
                                .Cells(TopRow + z, 5).Formula = "=SUM(" & .Cells(TopRow + 1, 5).Address & ":" & .Cells(TopRow + z - 1, 5).Address
                                .Cells(TopRow + z, colBudget).NumberFormat = "#,##0 $"
                                .Cells(TopRow + z, colBudget).Formula = "=SUM(" & .Cells(TopRow + 1, colBudget).Address & ":" & .Cells(TopRow + z - 1, colBudget).Address
                                .Columns(11).AutoFit()
                                .Range("B" & TopRow + z & ":C" & TopRow + z).Merge()
                                .Range("B" & TopRow + z & ":C" & TopRow + z).HorizontalAlignment = -4108
                                .Range("F" & TopRow + z & ":H" & TopRow + z).Merge()
                                .Range("F" & TopRow + z & ":H" & TopRow + z).HorizontalAlignment = -4108
                                TopRow = TopRow + z + 3
                                If printIndex AndAlso combination.Relations(1).Bookingtype.Indexes.Count > 0 Then
                                    .cells(TopRow, 1) = "*" & LangIni.Text("Booking", "Indexes")
                                    .cells(TopRow + 1, 1) = LangIni.Text("Booking", "Name")
                                    .cells(TopRow + 1, 2) = LangIni.Text("Spotlist", "Period")
                                    .cells(TopRow + 1, 4) = LangIni.Text("Booking", "Index")
                                    With .Range("A" & TopRow & ":L" & TopRow + 1)
                                        .Font.Bold = True
                                        .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                        .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                        For q = 7 To 10
                                            .Borders(q).LineStyle = 1
                                            .Borders(q).Weight = 2
                                            .Borders(q).Color = 0
                                        Next
                                    End With
                                    TopRow += 1
                                    For Each TmpIndex As Trinity.cIndex In combination.Relations(1).Bookingtype.Indexes
                                        If (TmpIndex.ToDate.ToOADate >= Campaign.StartDate And TmpIndex.ToDate.ToOADate <= Campaign.EndDate) Or (TmpIndex.FromDate.ToOADate >= Campaign.StartDate And TmpIndex.ToDate.ToOADate <= Campaign.EndDate) Or (TmpIndex.FromDate.ToOADate <= Campaign.StartDate And TmpIndex.ToDate.ToOADate >= Campaign.EndDate) Then
                                            TopRow += 1
                                            .cells(TopRow, 1) = TmpIndex.Name
                                            .range("B" & TopRow & ":C" & TopRow).merge()
                                            .cells(TopRow, 2) = Trinity.Helper.FormatShortDateForBooking(TmpIndex.FromDate) & " - " & Trinity.Helper.FormatShortDateForBooking(TmpIndex.ToDate)
                                            .cells(TopRow, 4).numberformat = "##,##0.0"
                                            .cells(TopRow, 4).HorizontalAlignment = -4131
                                            .cells(TopRow, 4) = TmpIndex.Index
                                        End If
                                    Next
                                    TopRow += 3
                                End If
                                .Cells(TopRow, 1) = LangIni.Text("Booking", "Comments")
                                With .Range("A" & TopRow & ":L" & TopRow)
                                    .Font.Bold = True
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                    .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                    For q = 7 To 10
                                        .Borders(q).LineStyle = 1
                                        .Borders(q).Weight = 2
                                        .Borders(q).Color = 0
                                    Next
                                End With
                                .cells(TopRow + 1, 1) = combination.Relations(1).Bookingtype.Comments
                                .Cells(TopRow + 6, 1) = LangIni.Text("Booking", "Other")
                                With .Range("A" & TopRow + 6 & ":L" & TopRow + 6)
                                    .Font.Bold = True
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                    .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                    For x = 7 To 10
                                        .Borders(x).LineStyle = 1
                                        .Borders(x).Weight = 2
                                        .Borders(x).Color = 0
                                    Next
                                End With
                                With .Range(RubrRange)
                                    .HorizontalAlignment = -4108
                                    .Font.Bold = True
                                    .Font.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                    .Interior.Color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                    For x = 7 To 11
                                        .Borders(x).LineStyle = 1
                                        .Borders(x).Weight = 3
                                        .Borders(x).Color = 0
                                    Next
                                End With

                                'Print compensations
                                If combination.Relations(1).Bookingtype.Compensations.Count > 0 Then
                                    Row = TopRow + 8
                                    Dim HeadlineRow As Integer = Row
                                    .range("B" & Row & ":G" & Row).merge()
                                    .cells(Row, 2).value = LangIni.Text("Booking", "Compensations")
                                    .cells(Row, 2).font.italic = True
                                    Row = Row + 1
                                    .Rows(Row).HorizontalAlignment = -4108
                                    .cells(Row, 2).value = LangIni.Text("Booking", "From")
                                    .cells(Row, 3).value = LangIni.Text("Booking", "To")
                                    .cells(Row, 4).value = LangIni.Text("Booking", "TRP")
                                    If LangIni.Text("Booking", "Expense") = "" Then
                                        .cells(Row, 5).value = "Expense"
                                    Else
                                        .cells(Row, 5).value = LangIni.Text("Booking", "Expense")
                                    End If
                                    .cells(Row, 6).value = LangIni.Text("Booking", "Comments")
                                    .range("F" & Row & ":G" & Row).merge()
                                    For Each TmpComp As Trinity.cCompensation In combination.Relations(1).Bookingtype.Compensations
                                        Row = Row + 1
                                        .Cells(Row, 2).NumberFormat = "@"
                                        .Cells(Row, 3).NumberFormat = "@"
                                        .cells(Row, 2).value = Trinity.Helper.FormatDateForBooking(TmpComp.FromDate)
                                        .cells(Row, 3).value = Trinity.Helper.FormatDateForBooking(TmpComp.ToDate)
                                        .cells(Row, 4).numberformat = "##,##0.0"
                                        Dim sumTRP As Single = 0
                                        Dim sumExpense As Single = 0
                                        For Each cc As Trinity.cCombinationChannel In combination.Relations
                                            sumTRP += cc.Bookingtype.Compensations(TmpComp.ID).TRPs
                                            sumExpense += cc.Bookingtype.Compensations(TmpComp.ID).Expense
                                        Next
                                        .cells(Row, 4).value = sumTRP
                                        If sumExpense > 0 Then
                                            .cells(Row, 5).value = sumExpense
                                        End If
                                        .cells(Row, 6).value = TmpComp.Comment
                                        .range("F" & Row & ":G" & Row).merge()
                                        .Rows(Row).HorizontalAlignment = -4108
                                    Next
                                    With .range("B" & HeadlineRow & ":G" & Row)
                                        For x = 7 To 10
                                            .Borders(x).LineStyle = 1
                                            .Borders(x).Weight = 2
                                            .Borders(x).ColorIndex = -4105
                                        Next
                                    End With
                                    With .range("B" & HeadlineRow & ":G" & HeadlineRow + 1)
                                        For x = 7 To 10
                                            .Borders(x).LineStyle = 1
                                            .Borders(x).Weight = -4138
                                            .Borders(x).ColorIndex = -4105
                                        Next
                                        .interior.color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                        .font.color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                        .font.bold = True
                                    End With
                                End If
                                'Print compensations
                                'If Campaign.Channels(Chan).BookingTypes(BT).Compensations.Count > 0 Then
                                '    Row = TopRow + 8
                                '    Dim HeadlineRow As Integer = Row
                                '    .range("B" & Row & ":G" & Row).merge()
                                '    .cells(Row, 2).value = LangIni.Text("Booking", "Compensations")
                                '    .cells(Row, 2).font.italic = True
                                '    Row = Row + 1
                                '    .Rows(Row).HorizontalAlignment = -4108
                                '    .cells(Row, 2).value = LangIni.Text("Booking", "From")
                                '    .cells(Row, 3).value = LangIni.Text("Booking", "To")
                                '    .cells(Row, 4).value = LangIni.Text("Booking", "TRP")
                                '    .cells(Row, 5).value = LangIni.Text("Booking", "Comments")
                                '    .range("E" & Row & ":G" & Row).merge()
                                '    For Each TmpComp As Trinity.cCompensation In Campaign.Channels(Chan).BookingTypes(BT).Compensations
                                '        Row = Row + 1
                                '        .cells(Row, 2).value = Format(TmpComp.FromDate, "yyyy-MM-dd")
                                '        .cells(Row, 3).value = Format(TmpComp.ToDate, "yyyy-MM-dd")
                                '        .cells(Row, 4).numberformat = "##,##0.0"
                                '        .cells(Row, 4).value = TmpComp.TRPs
                                '        .cells(Row, 5).value = TmpComp.Comment
                                '        .range("E" & Row & ":G" & Row).merge()
                                '        .Rows(Row).HorizontalAlignment = -4108
                                '    Next
                                '    With .range("B" & HeadlineRow & ":G" & Row)
                                '        For x = 7 To 10
                                '            .Borders(x).LineStyle = 1
                                '            .Borders(x).Weight = 2
                                '            .Borders(x).ColorIndex = -4105
                                '        Next
                                '    End With
                                '    With .range("B" & HeadlineRow & ":G" & HeadlineRow + 1)
                                '        For x = 7 To 10
                                '            .Borders(x).LineStyle = 1
                                '            .Borders(x).Weight = -4138
                                '            .Borders(x).ColorIndex = -4105
                                '        Next
                                '        .interior.color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel")
                                '        .font.color = TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText")
                                '        .font.bold = True
                                '    End With
                                'End If
                                With .Range("A1:L" & TopRow + 13)
                                    For q = 7 To 10
                                        .Borders(q).LineStyle = 1
                                        .Borders(q).Weight = 3
                                        .Borders(q).Color = 0
                                    Next
                                End With
                                .Columns(7).AutoFit()
                                .Range("A" & TopRow + 13 & ":L" & TopRow + 13).Merge()
                                If cmbLogo.SelectedIndex > 0 Then
                                    WB.Sheets(1).pictures.Insert(DataPath & "Logos\" + cmbLogo.Text) 'LocalDataPath & "\" & Inifile.Text("Logos", "Path" & cmbLogo.ListIndex))
                                    'Scal = 180 / WB.Sheets(1).pictures(1).Width
                                    Scal = 1
                                    WB.Sheets(1).pictures(1).ScaleWidth(Scal, 0, 0)
                                    WB.Sheets(1).pictures(1).ScaleHeight(Scal, 0, 0)
                                    WB.Sheets(1).pictures(1).Top = 10
                                    WB.Sheets(1).Rows(1).RowHeight = WB.Sheets(1).pictures(1).Height + 10
                                    If cmbAlign.SelectedIndex = 1 Then
                                        WB.Sheets(1).pictures(1).Left = WB.Sheets(1).Columns(13).Left / 2 - WB.Sheets(1).pictures(1).Width / 2
                                    ElseIf cmbAlign.SelectedIndex = 2 Then
                                        WB.Sheets(1).pictures(1).Left = WB.Sheets(1).Columns(13).Left - WB.Sheets(1).pictures(1).Width - 10
                                    Else
                                        WB.Sheets(1).pictures(1).Left = 10
                                    End If
                                End If
                                Excel.Windows(WB.Name).View = 2
                                While .VPageBreaks.count > 0
                                    .VPageBreaks(1).DragOff(Direction:=-4161, RegionIndex:=1)
                                End While
                                Excel.Windows(WB.Name).View = 1

                                .Cells(TopRow + 13, 1).Interior.Color = 0
                                .Cells(TopRow + 13, 1).Font.Color = RGB(255, 255, 255)
                                .Cells(TopRow + 13, 1).Font.Italic = True
                                .Cells(TopRow + 13, 1).Font.Bold = True
                                .Cells(TopRow + 13, 1).NumberFormat = "yyyy-mm-dd"
                                .Cells(TopRow + 13, 1).HorizontalAlignment = -4108
                                .Cells(TopRow + 13, 1) = Now

                            End If
                        End If
                    End With
                End If
            End If
            i = i + 1
        Next
        Me.Cursor = Windows.Forms.Cursors.Default
        Excel.displayalerts = True
        Excel.ScreenUpdating = True
        Excel.Visible = True
 
    End Sub

    Public Function countCharInString(ByVal input As String, ByVal delimiter As Char) As Integer
        Dim count, index As Integer
        index = input.IndexOf(delimiter)
        Do Until index < 0
            count += 1
            index = input.IndexOf(delimiter, index + 1)
        Loop
        Return count
    End Function

    Private Sub grdContacts_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdContacts.CellValueNeeded
        If e.ColumnIndex < 0 OrElse e.RowIndex < 0 Then Exit Sub

        e.Value = grdContacts.Rows(e.RowIndex).Cells(0).Tag
    End Sub

    Private Sub grdContacts_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdContacts.CellValuePushed
        If e.Value = "" Then Exit Sub
        Dim strItems() As String

        grdContacts.Rows(e.RowIndex).Cells(0).Tag = e.Value
        If grdContacts.Rows(e.RowIndex).Tag(0) = "" Then
            strItems = New String() {e.Value}
            grdContacts.Rows(e.RowIndex).Tag = strItems
            DirectCast(grdContacts.Rows(e.RowIndex).Cells(0), ExtendedComboBoxCellWrite).Items.Add(e.Value)
            grdContacts.Rows(e.RowIndex).Cells(0).Tag = e.Value
            TrinitySettings.ChannelContact(grdContacts.Rows(e.RowIndex).HeaderCell.Tag) = e.Value
        Else
            strItems = grdContacts.Rows(e.RowIndex).Tag
            Dim strTot As String = ""
            For z As Integer = 0 To strItems.Length - 1
                If strItems(z).ToUpper = grdContacts.Rows(e.RowIndex).Cells(0).Value.ToString.ToUpper.Trim Then
                    Exit Sub
                End If
                strTot = strTot & "|" & strItems(z)
            Next

            strTot = strTot.Substring(1)

            'add the name to the combobox
            DirectCast(grdContacts.Rows(e.RowIndex).Cells(0), ExtendedComboBoxCellWrite).Items.Add(e.Value)
            grdContacts.Rows(e.RowIndex).Cells(0).Tag = e.Value

            ReDim Preserve strItems(strItems.Length)
            strItems.SetValue(grdContacts.Rows(e.RowIndex).Cells(0).Value, strItems.Length - 1)
            grdContacts.Rows(e.RowIndex).Tag = strItems
            TrinitySettings.ChannelContact(grdContacts.Rows(e.RowIndex).HeaderCell.Tag) = strTot & "|" & e.Value
        End If

    End Sub

    Public Sub New(Optional DefaultCheckBookingTypes As List(Of Trinity.cBookingType) = Nothing)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _defaultCheckBookingTypes = DefaultCheckBookingTypes

    End Sub

    Private Sub lblOldPricelist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblOldPricelist.Click
        MsgBox(strPricelistUpdate, MsgBoxStyle.Information, "")
    End Sub

    Private Sub cmdRemoveContracts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveContracts.Click
        For Each row As Windows.Forms.DataGridViewRow In grdContacts.SelectedRows
            TrinitySettings.ChannelContact(row.HeaderCell.Tag) = ""
            DirectCast(row.Cells(0), ExtendedComboBoxCellWrite).Items.Clear()
        Next
    End Sub

    Private Sub cmdColumns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdColumns.Click
        Dim columnsPicker As New frmPickSpecificsColumns
        columnsPicker.ShowDialog()
        _specialColumns = columnsPicker.specificsColumns
    End Sub

    Private Sub cmbBillingAddress_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBillingAddress.SelectedIndexChanged

    End Sub

    Private Sub cmdPrint_Click(sender As System.Object, e As System.EventArgs) Handles cmdPrint.Click
        Dim _printBooking As New cPrintBookings(cmdLanguage.Tag)

        _printBooking.PanelColor = Color.FromArgb(TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Panel"))
        _printBooking.PanelTextColor = Color.FromArgb(TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "PanelText"))
        _printBooking.HeadlineColor = Color.FromArgb(TrinitySettings.Color(cmbColorScheme.SelectedIndex + 1, "Headline"))

        _printBooking.FontName = TrinitySettings.SchemeFont(cmbColorScheme.SelectedIndex + 1)
        _printBooking.LogoAlignment = cmbAlign.SelectedIndex
        _printBooking.LogoPath = DataPath & "Logos\" & cmbLogo.Text

        _printBooking.SkipWeeks = (From _mnu As Windows.Forms.ToolStripMenuItem In mnuWeeks.Items Where Not _mnu.Checked).Select(Function(mnu) mnu.Text).ToList()

        Dim i As Integer = 0
        For Each _mnu As Windows.Forms.ToolStripMenuItem In mnuChannels.Items
            If _mnu.Checked Then
                Try
                    _printBooking.CreateBooking(_mnu.Tag, grdContacts.Rows(i).Cells(0).Value)
                Catch ex As Exception
                    Windows.Forms.MessageBox.Show(ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)                    
                End Try
            End If
            i += 1
        Next

        If chkDefault.Checked Then
            TrinitySettings.DefaultLogo = cmbLogo.SelectedIndex
            TrinitySettings.DefaultColorScheme = cmbColorScheme.SelectedIndex
            TrinitySettings.LogoAlignment = cmbAlign.SelectedIndex
        End If
    End Sub
End Class