Imports System.Net.WebRequestMethods
Imports System.Drawing
Imports System.IO
Imports System
Imports System.Xml
Imports System.Xml.Serialization
Imports System.ComponentModel
Imports System.ComponentModel.Composition
Imports System.ComponentModel.Composition.Hosting
Imports System.Reflection
Imports TrinityPlugin

Public Class frmMain

    Private JustStarted As Boolean = True
    Dim frm As New frmAskForDate
    Private _startTrinityFast As Boolean = False

    Private Class CBItem

        Private _text As String
        Public Property Text() As String
            Get
                Return _text
            End Get
            Set(ByVal value As String)
                _text = value
            End Set
        End Property

        Private _tag As Object
        Public Property Tag() As Object
            Get
                Return _tag
            End Get
            Set(ByVal value As Object)
                _tag = value
            End Set
        End Property

    End Class

    'Modified 2018-04-27'
    Private Sub btnSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSetup.Click

        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType

        My.Application.checkDeveloper()

        If TrinitySettings.UserEmail = "joakim.koch@groupm.com" Then
            'Added to load all pricelists when clicking on Setup'
            Trinity.Helper.WriteToLogFile("Read Pricelists")
            For Each TmpChan In Campaign.Channels
                For Each TmpBT In TmpChan.BookingTypes
                    TmpBT.ReadPricelist()
                Next
            Next
        End If

        frmSetup.MdiParent = Me
        frmSetup.Show()
        JustStarted = False
    End Sub

    Private Sub AdtooxInformation()
        Campaign.AdToox = New Trinity.cAdtoox(TrinitySettings.AdtooxUsername, TrinitySettings.AdtooxPassword)
    End Sub

    Private Sub OpenCampaign(ByVal Filename As String)

        Dim tmpResult As Windows.Forms.DialogResult

        If Not Campaign.Name Is Nothing Then
            tmpResult = Windows.Forms.MessageBox.Show("You are currently working on a campaign. " &
                                                    vbNewLine & "Save it before opening another?", "T R I N I T Y",
                                                    Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question)

            'Added cancelButton if you want to stay in current campaign /JK
            If tmpResult = Windows.Forms.DialogResult.Cancel Then
                Exit Sub

                'And if you dont want to save current campaign do nothing /JK
            ElseIf tmpResult = Windows.Forms.DialogResult.No Then

            ElseIf tmpResult = Windows.Forms.DialogResult.Yes Then
                Campaign.SaveCampaign()
                System.Windows.Forms.MessageBox.Show("Saved!", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)

            End If
        End If


        'Changes the cursor to a "loading" symbol
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        'closes all open windows
        Dim j As Integer
        For j = My.Application.OpenForms.Count - 1 To 0 Step -1
            If Not My.Application.OpenForms(j).Name = "frmMain" Then
                My.Application.OpenForms(j).Dispose()
            End If
        Next

        'creates a new Campaign
        If Not Campaign.fs Is Nothing Then Campaign.fs.Close()
        Campaign = New Trinity.cKampanj(TrinitySettings.ErrorChecking)
        TrinitySettings.MainObject = Campaign
        Trinity.Helper.MainObject = Campaign


        'resets all buttons and information containers, görs inte detta nedan? knappar sets och update info körs???
        Reset()

        'if the setup dialog for a new campaign is open it will be closed
        If Not frmSetup Is Nothing Then
            frmSetup.Dispose()
        End If

        If Not My.Computer.FileSystem.FileExists(Filename) Then
            Windows.Forms.MessageBox.Show("This file no longer exists.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            Exit Sub
        End If


        If Campaign.LoadCampaign(Filename) = 1 Then
            Me.Cursor = Windows.Forms.Cursors.Default
            Exit Sub
        End If

        InitializeCampaign()


        tmrDetectProblems.Enabled = True

    End Sub

    Private Sub InitializeCampaign()

        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim j As Integer

        Dim AdtooxThread As Threading.Thread
        AdtooxThread = New Threading.Thread(AddressOf Me.AdtooxInformation)
        AdtooxThread.Priority = Threading.ThreadPriority.Lowest
        AdtooxThread.IsBackground = True
        AdtooxThread.Start()

CheckContract:
        If Campaign.Contract IsNot Nothing Then
            If Campaign.DatabaseID = 0 And Campaign.Contract.Path = "" Then

                If Windows.Forms.MessageBox.Show("The path for the Contract connected to this campaign was not saved." & vbCrLf & vbCrLf & "Do you want to browse for the contract?" & vbCrLf & vbCrLf & "(If you choose 'No' you will still be able to work on the campaign," & vbCrLf & " but no changes to the contract will be imported)", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim FileOpen As New Windows.Forms.OpenFileDialog
                    FileOpen.CheckFileExists = True
                    FileOpen.Filter = "Trinity contracts|*.tct"
                    If FileOpen.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
                    Campaign.Contract.Path = FileOpen.FileName
                    GoTo CheckContract
                End If

            ElseIf Campaign.DatabaseID = 0 And Not My.Computer.FileSystem.FileExists(Campaign.Contract.Path) Then

                If Windows.Forms.MessageBox.Show("The contract connected to this campaign has been deleted or moved:" & vbCrLf & Campaign.Contract.Path & vbCrLf & vbCrLf & "Do you want to browse for the contract?" & vbCrLf & vbCrLf & "(If you choose 'No' you will still be able to work on the campaign," & vbCrLf & " but no changes to the contract will be imported)", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim FileOpen As New Windows.Forms.OpenFileDialog
                    FileOpen.CheckFileExists = True
                    FileOpen.Filter = "Trinity contracts|*.tct"
                    If FileOpen.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
                    Campaign.Contract.Path = FileOpen.FileName
                    GoTo CheckContract
                End If

            Else
ConnectContract:
                If Not TrinitySettings.SaveCampaignsAsFiles AndAlso Campaign.ContractID = 0 Then
                    Select Case frmConnectContract.ShowDialog()
                        Case Windows.Forms.DialogResult.Yes
                            'Import contract
                            Dim _contract As New Trinity.cContract(Campaign)
                            If Not My.Computer.FileSystem.FileExists(Campaign.Contract.Path) Then
                                Select Case Windows.Forms.MessageBox.Show("The contract file '" & Campaign.Contract.Path & "' could not be found." & vbCrLf & vbCrLf & "Do you want to browse for the file?" & vbCrLf & vbCrLf & "Choosing 'No' will import the contract saved in the campaign." & vbCrLf & "Choosing 'Cancel will take you back to the previous dialog.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question)
                                    Case Windows.Forms.DialogResult.Yes
                                        'Browse
                                        Dim dlgBrowse As New Windows.Forms.OpenFileDialog
                                        dlgBrowse.Title = "Open contract..."
                                        dlgBrowse.Filter = "Trinity contracts|*.tct"
                                        dlgBrowse.FileName = "*.tct"
                                        If dlgBrowse.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                                            GoTo ConnectContract
                                        End If
                                        Try
                                            _contract.Load(dlgBrowse.FileName)
                                        Catch ex As Exception
                                            Windows.Forms.MessageBox.Show("Error while importing the contract:" & vbCrLf & vbCrLf & ex.Message)
                                            GoTo ConnectContract
                                        End Try
                                    Case Windows.Forms.DialogResult.No
                                        'Import from campaign
                                        If Windows.Forms.MessageBox.Show("Are you sure you want to import the contract from this campaign?" & vbCrLf & vbCrLf & "You should only do so if you are certain it is the latest version of the cont5ract.", "T R  I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                                            GoTo ConnectContract
                                        End If
                                        _contract = Campaign.Contract
                                    Case Windows.Forms.DialogResult.Cancel
                                        GoTo ConnectContract
                                End Select
                            Else
                                Try
                                    _contract.Load(Campaign.Contract.Path)
                                Catch ex As Exception
                                    Windows.Forms.MessageBox.Show("Error while importing the contract:" & vbCrLf & vbCrLf & ex.Message)
                                    GoTo ConnectContract
                                End Try
                            End If
                            _contract.Save("", True, True)
                        Case Windows.Forms.DialogResult.OK
                            'Connect contract
                            Dim _contractID As Integer = frmConnectContract.cmbContract.SelectedValue
                            Campaign.ContractID = _contractID
                        Case Windows.Forms.DialogResult.No
                            'Offline
                    End Select
                End If
                If (Campaign.DatabaseID = 0 AndAlso Campaign.Contract.SaveDateTime < Trinity.cContract.GetDateTime(Campaign.Contract.Path)) OrElse
                Campaign.NewContractVersion Then
                    Dim _name As String
                    If Campaign.ContractID = 0 Then
                        _name = Campaign.Contract.Path
                    Else
                        _name = Campaign.Contract.Name
                    End If
                    If Windows.Forms.MessageBox.Show("There is a newer version of the contract loaded in this campaign:" & vbCrLf & _name & vbCrLf & vbCrLf & "Do you want to reload the contract?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Dim OldContract As Trinity.cContract = Campaign.Contract
                        If Campaign.ContractID = 0 Then
                            Dim Path As String = Campaign.Contract.Path

                            'saves the old contract for comparison

                            Campaign.Contract = New Trinity.cContract(Campaign)
                            Campaign.Contract.Load(Path)
                        Else
                            Campaign.Contract = New Trinity.cContract(Campaign)
                            Campaign.Contract.Load("", True, DBReader.getContract(Campaign.ContractID).OuterXml.ToString)
                        End If

                        For Each TmpC As Trinity.cContractChannel In Campaign.Contract.Channels
                            If Not OldContract.Channels(TmpC.ChannelName) Is Nothing Then
                                If Not TmpC.ActiveContractLevel = OldContract.Channels(TmpC.ChannelName).ActiveContractLevel AndAlso TmpC.BookingTypes(TmpC.ActiveContractLevel)(1).ActiveFromDate < Date.FromOADate(Campaign.EndDate) Then
                                    If Not MsgBox("The contract file has a higher level activated on " & TmpC.ChannelName & ". Do you want load that level?", MsgBoxStyle.YesNo, "New Level") = MsgBoxResult.Yes Then
                                        TmpC.ActiveContractLevel = OldContract.Channels(TmpC.ChannelName).ActiveContractLevel

                                        For i As Integer = 1 To TmpC.LevelCount
                                            For Each TmpCBT As Trinity.cContractBookingtype In TmpC.BookingTypes(i)
                                                If i > OldContract.Channels(TmpC.ChannelName).LevelCount Then
                                                    TmpCBT.Active = False
                                                Else
                                                    TmpCBT.Active = OldContract.Channels(TmpC.ChannelName).BookingTypes(i)(TmpCBT.Name).Active
                                                End If
                                            Next
                                        Next
                                    End If
                                End If
                            End If
                            For Each TmpCBT As Trinity.cContractBookingtype In TmpC.BookingTypes(TmpC.ActiveContractLevel)
                                If Campaign.Channels(TmpC.ChannelName) IsNot Nothing Then
                                    TmpBT = Campaign.Channels(TmpC.ChannelName).BookingTypes(TmpCBT.Name)
                                Else
                                    TmpBT = Nothing
                                End If


                                If TmpBT Is Nothing AndAlso Campaign.Channels(TmpC.ChannelName) IsNot Nothing Then
                                    If MsgBox("The contract but not the campaign contains " & TmpCBT.ToString & ". Do you want to add it?", MsgBoxStyle.YesNo, "Add bookingtype") = MsgBoxResult.Yes Then
                                        Dim tmpChanNew As Trinity.cChannel
                                        If Campaign.Channels(TmpCBT.ParentChannel.ChannelName) Is Nothing Then
                                            tmpChanNew = New Trinity.cChannel(Campaign, Campaign.Channels.RawCollection)
                                            tmpChanNew.ChannelName = TmpC.ChannelName
                                            tmpChanNew.MainObject = Campaign
                                            tmpChanNew.fileName = Campaign.Channels(TmpC.ChannelName).fileName

                                            tmpChanNew.readDefaultBookingTypes()
                                        Else
                                            tmpChanNew = Campaign.Channels(TmpCBT.ParentChannel.ChannelName)
                                            tmpChanNew.readDefaultBookingTypes()
                                        End If

                                        'update the BT that exists
                                        If tmpChanNew.BookingTypes(TmpCBT.Name) Is Nothing Then
                                            MsgBox("Could not add " & TmpCBT.ToString & " to campaign, it was not available in the database. To load the contract this Bookingtype needs to be manually added.", MsgBoxStyle.Critical, "ERROR")
                                            Exit Sub
                                        Else
                                            TmpBT = tmpChanNew.BookingTypes(TmpCBT.Name)
                                            TmpBT.Shortname = tmpChanNew.BookingTypes(TmpCBT.Name).Shortname
                                            TmpBT.IsRBS = tmpChanNew.BookingTypes(TmpCBT.Name).IsRBS
                                            TmpBT.IsSpecific = tmpChanNew.BookingTypes(TmpCBT.Name).IsSpecific
                                            TmpBT.PrintDayparts = tmpChanNew.BookingTypes(TmpCBT.Name).PrintDayparts
                                            TmpBT.PrintBookingCode = tmpChanNew.BookingTypes(TmpCBT.Name).PrintBookingCode
                                        End If
                                        TmpBT.MaxDiscount = tmpChanNew.BookingTypes(TmpCBT.Name).MaxDiscount

                                        TmpBT.ReadPricelist()
                                    Else
                                        GoTo LoopBT
                                    End If
                                ElseIf Campaign.Channels(TmpC.ChannelName) Is Nothing Then
                                    GoTo LoopBT
                                End If

                                For i As Integer = 1 To 500
                                    If TmpCBT.FilmIndex(i) > 0 Then TmpBT.FilmIndex(i) = TmpCBT.FilmIndex(i)
                                Next

                                For Each TmpIndex As Trinity.cIndex In TmpCBT.Indexes
                                    Dim UseThis As Boolean = True
                                    Dim SaveEnh As New Dictionary(Of String, Trinity.cEnhancement)
                                    If Not TmpBT.Indexes(TmpIndex.ID) Is Nothing Then
                                        UseThis = TmpBT.Indexes(TmpIndex.ID).UseThis
                                        For Each TmpEnh As Trinity.cEnhancement In TmpBT.Indexes(TmpIndex.ID).Enhancements
                                            SaveEnh.Add(TmpEnh.ID, TmpEnh)
                                        Next
                                        TmpBT.Indexes.Remove(TmpIndex.ID)
                                    End If
                                    With TmpBT.Indexes.Add(TmpIndex.Name, TmpIndex.ID)
                                        .FromDate = TmpIndex.FromDate
                                        .ToDate = TmpIndex.ToDate
                                        .IndexOn = TmpIndex.IndexOn
                                        .Index = TmpIndex.Index
                                        For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
                                            If Not TmpBT.Indexes(TmpIndex.ID) Is Nothing AndAlso Not TmpBT.Indexes(TmpIndex.ID).Enhancements(TmpEnh.ID) Is Nothing Then
                                                TmpBT.Indexes(TmpIndex.ID).Enhancements.Remove(TmpEnh.ID)
                                            End If
                                            With .Enhancements.Add
                                                .Amount = TmpEnh.Amount
                                                .ID = TmpEnh.ID
                                                .Name = TmpEnh.Name
                                            End With
                                        Next
                                        For Each TmpEnh As Trinity.cEnhancement In SaveEnh.Values
                                            If TmpBT.Indexes(TmpIndex.ID).Enhancements(TmpEnh.ID) IsNot Nothing Then
                                                TmpBT.Indexes(TmpIndex.ID).Enhancements(TmpEnh.ID).UseThis = TmpEnh.UseThis
                                            Else
                                                With TmpBT.Indexes(TmpIndex.ID).Enhancements.Add
                                                    .ID = TmpEnh.ID
                                                    .Name = TmpEnh.Name
                                                    .Amount = TmpEnh.Amount
                                                    .UseThis = TmpEnh.UseThis
                                                End With
                                            End If
                                        Next
                                        .UseThis = UseThis
                                    End With
                                Next

                                For Each TmpAV As Trinity.cAddedValue In TmpCBT.AddedValues
                                    Dim UseThis As Boolean = True
                                    If Not TmpBT.AddedValues(TmpAV.ID) Is Nothing Then
                                        UseThis = TmpBT.AddedValues(TmpAV.ID).UseThis
                                        TmpBT.AddedValues.Remove(TmpAV.ID)
                                    End If
                                    With TmpBT.AddedValues.Add(TmpAV.Name, TmpAV.ID)
                                        .IndexGross = TmpAV.IndexGross
                                        .IndexNet = TmpAV.IndexNet
                                        .UseThis = UseThis
                                    End With
                                Next

                                Dim TmpTarget As Trinity.cPricelistTarget
                                For Each TmpCTarget As Trinity.cContractTarget In TmpCBT.ContractTargets
                                    If TmpCTarget.IsContractTarget OrElse TmpBT.Pricelist.Targets(TmpCTarget.TargetName) Is Nothing Then

                                        If Not TmpCTarget.IsContractTarget Then
                                            Windows.Forms.MessageBox.Show("The target '" & TmpCTarget.TargetName & "' in '" & TmpBT.ToString & "' can" & vbCrLf & "not be found in the pricelist and will be added.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                                        End If

                                        TmpTarget = TmpBT.Pricelist.Targets(TmpCTarget.TargetName)

                                        If TmpTarget Is Nothing Then
                                            TmpTarget = TmpBT.Pricelist.Targets.Add(TmpCTarget.TargetName, TmpBT, , , , )
                                        End If

                                        TmpTarget.CalcCPP = TmpCTarget.CalcCPP
                                        TmpTarget.Target.TargetType = TmpCTarget.TargetType
                                        TmpTarget.PricelistPeriods.Clear()
                                        For Each TmpPeriod As Trinity.cPricelistPeriod In TmpCTarget.PricelistPeriods
                                            Dim period As Trinity.cPricelistPeriod = TmpTarget.PricelistPeriods.Add(TmpPeriod.Name)
                                            period.FromDate = TmpPeriod.FromDate
                                            period.ToDate = TmpPeriod.ToDate
                                            period.PriceIsCPP = TmpPeriod.PriceIsCPP
                                            period.TargetNat = TmpPeriod.TargetNat
                                            period.TargetUni = TmpPeriod.TargetUni
                                            For j = 0 To TmpBT.Dayparts.Count - 1
                                                period.Price(j) = TmpPeriod.Price(j)
                                            Next
                                        Next
                                    Else
                                        TmpTarget = TmpBT.Pricelist.Targets(TmpCTarget.TargetName)
                                    End If

                                    'If Not TmpTarget.Target.TargetName.Trim = TmpCTarget.AdEdgeTargetName Then
                                    '    'due to an error with the contracts the AdEdgeTarget name was saved the same as the target name
                                    '    If InStr(TmpCTarget.AdEdgeTargetName, TmpTarget.Target.TargetName) > 0 OrElse TmpCTarget.AdEdgeTargetName = "Spec" Then
                                    '        TmpCTarget.AdEdgeTargetName = TmpTarget.Target.TargetName
                                    '    Else
                                    '        TmpTarget.Target.TargetName = TmpCTarget.AdEdgeTargetName
                                    '    End If
                                    'End If

                                    ''copy the daypart split
                                    'For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                                    '    TmpTarget.DefaultDaypart(i) = TmpCTarget.DefaultDaypart(i)
                                    'Next

                                    'copy the CPP/CPT/Discount entered
                                    TmpTarget.IsEntered = TmpCTarget.IsEntered
                                    Select Case TmpCTarget.IsEntered
                                        Case Is = Trinity.cContractTarget.EnteredEnum.eCPP
                                            TmpTarget.NetCPP = TmpCTarget.EnteredValue
                                        Case Is = Trinity.cContractTarget.EnteredEnum.eCPT
                                            TmpTarget.NetCPT = TmpCTarget.EnteredValue
                                        Case Is = Trinity.cContractTarget.EnteredEnum.eDiscount
                                            TmpTarget.Discount = TmpCTarget.EnteredValue
                                    End Select

                                    'set the buyingtarget again so it will be updated
                                    If Not TmpBT.BuyingTarget.TargetName Is Nothing AndAlso Not TmpBT.BuyingTarget.TargetName = "" Then
                                        TmpBT.BuyingTarget = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName)
                                        Select Case TmpBT.BuyingTarget.IsEntered
                                            Case Trinity.cPricelistTarget.EnteredEnum.eCPP
                                                TmpBT.BuyingTarget.NetCPP = TmpBT.BuyingTarget.NetCPP
                                            Case Trinity.cPricelistTarget.EnteredEnum.eCPT
                                                TmpBT.BuyingTarget.NetCPT = TmpBT.BuyingTarget.NetCPT
                                            Case Trinity.cPricelistTarget.EnteredEnum.eDiscount
                                                TmpBT.BuyingTarget.Discount = TmpBT.BuyingTarget.Discount
                                        End Select
                                    End If
                                Next
LoopBT:
                            Next
                        Next
                        'For Each TmpChan In Campaign.Channels
                        '    For Each TmpBT In TmpChan.BookingTypes
                        '        If Not Campaign.Contract.Channels(TmpChan.ChannelName) Is Nothing AndAlso Not Campaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(1)(TmpBT.Name) Is Nothing Then
                        '            If Campaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Pricelist.Targets.Count > 0 Then
                        '                For Each TmpAV As Trinity.cAddedValue In Campaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).AddedValues
                        '                    If Not TmpBT.AddedValues(TmpAV.ID) Is Nothing Then
                        '                        TmpBT.AddedValues.Remove(TmpAV.ID)
                        '                    End If
                        '                    With TmpBT.AddedValues.Add(TmpAV.Name, TmpAV.ID)
                        '                        .IndexGross = TmpAV.IndexGross
                        '                        .IndexNet = TmpAV.IndexNet
                        '                    End With
                        '                Next
                        '                If Not Campaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Pricelist.Targets(TmpBT.BuyingTarget.TargetName) Is Nothing Then
                        '                    TmpBT.BuyingTarget = Campaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Pricelist.Targets(TmpBT.BuyingTarget.TargetName)
                        '                    Select Case TmpBT.BuyingTarget.IsEntered
                        '                        Case Trinity.cPricelistTarget.EnteredEnum.eCPP
                        '                            TmpBT.BuyingTarget.NetCPP = TmpBT.BuyingTarget.NetCPP
                        '                        Case Trinity.cPricelistTarget.EnteredEnum.eCPT
                        '                            TmpBT.BuyingTarget.NetCPT = TmpBT.BuyingTarget.NetCPT
                        '                        Case Trinity.cPricelistTarget.EnteredEnum.eDiscount
                        '                            TmpBT.BuyingTarget.Discount = TmpBT.BuyingTarget.Discount
                        '                    End Select
                        '                End If
                        '                TmpBT.Indexes = Campaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Indexes
                        '                TmpBT.Pricelist = Campaign.Contract.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Pricelist
                        '            End If
                        '        End If
                        '    Next
                        'Next

                        'update all labels and other information boxes about the new changes
                        Campaign.Costs = Campaign.Contract.Costs
                        'update all labels and other information boxes about the new changes
                        If Campaign.Costs.Count = 0 Then
                            Campaign.Costs = Campaign.Contract.Costs
                        End If
                        If Campaign.Combinations.Count = 0 Then
                            Campaign.Combinations = Campaign.Contract.Combinations
                        End If
                    End If
                End If
            End If
        End If

        For i As Integer = 1 To Campaign.Channels.Count
            If Not LongName.ContainsKey(Campaign.Channels(i).Shortname) Then
                LongName.Add(Campaign.Channels(i).Shortname, Campaign.Channels(i).ChannelName)
            End If
        Next

        Windows.Forms.Application.DoEvents()
        GeneralFilter = New Trinity.cFilter

        'hides the recent panel
        pnlStartHelp.Visible = False
        If Not dlgDialog.FileName = "" Then
            TrinitySettings.setLastCampaign = dlgDialog.FileName
        End If

        Dim newSpots As Boolean = True

        For Each prgArgument As String In Environment.GetCommandLineArgs
            If prgArgument = "NoNewSpots" Then newSpots = False
        Next

        If newSpots Then Campaign.GetNewActualSpots()

        lblStatus.Text = ""

        If newSpots Then Campaign.CreateAdedgeSpots()

        'updates the "note" and the window name
        frmMain_Activated(New Object, New EventArgs)


        'Enables all buttons
        If Campaign.Channels.Count > 0 AndAlso Campaign.Channels(1).BookingTypes(1).Weeks.Count > 0 AndAlso Campaign.Channels(1).BookingTypes(1).Weeks(1).Films.Count > 0 Then
            Me.cmdAllocate.Enabled = True
            Me.cmdMonitor.Enabled = True
            Me.cmdSpots.Enabled = True
            Me.cmdLab.Enabled = True
            Me.cmdPivot.Enabled = False
            Me.cmdInfo.Enabled = True
            Me.cmdBudget.Enabled = True
            Me.cmdDelivery.Enabled = True
            Me.cmdNotes.Enabled = True
        End If
        'resets the cursor to normal
        Me.Cursor = Windows.Forms.Cursors.Default
        Dim Filmcodes As New List(Of String)
        For Each TmpChan In Campaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                If (TmpBT.IsSpecific OrElse TmpBT.IsPremium) AndAlso TmpBT.BookIt Then
                    Me.cmdBooking.Enabled = True
                End If

                For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                    For Each TmpFilm As Trinity.cFilm In TmpWeek.Films
                        If Not Filmcodes.Contains(TmpFilm.Filmcode) AndAlso TmpFilm.Filmcode <> "" Then
                            Filmcodes.Add(TmpFilm.Filmcode)
                        End If
                    Next
                Next
                Dim Dummy As Single = TmpBT.ActualNetValue
            Next
        Next

        Dim DamagedSpots As New List(Of DataRow)

        '
        'Deprecated, spot control information is not used or provided
        '

        'Dim dt As DataTable = Nothing
        'Try
        '    dt = DBReader.getTVCheckInfo(Campaign.StartDate, Campaign.EndDate, Filmcodes)
        'Catch
        '    dt = Nothing
        '    'Temporary line while only swedish offices has Spotcontrol in their database
        '    If Campaign.Area = "SE" Then
        '        Windows.Forms.MessageBox.Show("Could not read spotcontrol information for this campaign." & vbCrLf & vbCrLf & "Please contact the system administrator.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        '    End If
        'End Try
        'Dim ShowSCDialog As Boolean = False

        'If Not dt Is Nothing Then
        '    For Each TmpSpot As Trinity.cActualSpot In Campaign.ActualSpots
        '        For Each TmpRow As DataRow In dt.Rows
        '            If TmpSpot.MaM = TmpRow!Mam AndAlso TmpSpot.AirDate = TmpRow!Date Then
        '                If TmpSpot.SpotControlStatus = Trinity.cActualSpot.SCStatusEnum.scsNone AndAlso TmpSpot.SpotControlRemark = "" Then
        '                    TmpSpot.SpotControlRemark = TmpRow!Remark
        '                    TmpSpot.SpotControlStatus = Trinity.cActualSpot.SCStatusEnum.scsRemindMe
        '                End If
        '            End If
        '        Next
        '        If TmpSpot.SpotControlStatus = Trinity.cActualSpot.SCStatusEnum.scsRemindMe Then
        '            ShowSCDialog = True
        '        End If
        '    Next
        'End If
        'OpenCampaignExtracted()

        'If ShowSCDialog Then frmSpotControlImport.ShowDialog()

        'updates the recent files 
        showRecentCampaignsMenu()

        'Update the list of linked campaigns

        Dim newCampaigns As List(Of Trinity.cLinkedCampaign) = Campaign.UpdateLinkedCampaignList()


        If newCampaigns IsNot Nothing AndAlso newCampaigns.Count > 0 Then
            If Windows.Forms.MessageBox.Show("New campaigns has automatically been added to the" & vbCrLf _
                                          & "list of linked campaigns." & vbCrLf & vbCrLf & "Do you want to review them now?" & vbCrLf & vbCrLf _
                                          & "You can review linked campaigns at any time by " & vbCrLf &
                                          "clicking the Link-button in the toolbar.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                frmLinkCampaign.ShowDialog()
            End If
        End If
        cmdLinkCampaign.BackColor = SystemColors.Control
        For Each _link As Trinity.cLinkedCampaign In Campaign.LinkedCampaigns
            If _link.BrokenLink Then
                cmdLinkCampaign.BackColor = Color.Red
            End If
        Next
        If TrinitySettings.SaveCampaignsAsFiles Then
            If Campaign.LinkedCampaigns.Where(Function(c) c.DatabaseID = 0).Count > 0 Then
                Windows.Forms.MessageBox.Show("Your campaign is still linked to campaign files, but campaigns" & vbCrLf & "are now stored in the database." & vbCrLf & "Click the link button in the toolbar to resolve.")
                cmdLinkCampaign.BackColor = Color.Red
            End If
        End If
        'enables autosave function
        tmrAutosave.Enabled = True
        Saved = True
    End Sub

    Private Sub OpenCampaignClicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click, lblOpenCampaign.Click, mnuOpen.Click
        If TrinitySettings.SaveCampaignsAsFiles Then
            OpenFromFile()
        Else
            OpenFromDB()
        End If
    End Sub

    Private Sub frmMain_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'sets the note button to enabled/disabled depending on if there is a existing note or not
        If Not Campaign Is Nothing Then
            If Campaign.Commentary <> "" Then
                cmdNotes.BackColor = Drawing.Color.Crimson
            Else
                cmdNotes.BackColor = cmdAllocate.BackColor
            End If
            'sets the window name depending on the campaign name
            If Campaign.Name <> "" Then
                Me.Text = "T R I N I T Y   4.0  -  " & Campaign.Name & " - [" & Campaign.Filename & "]"
                If Campaign.ReadOnly Then
                    Me.Text &= " [READ ONLY]"
                End If
            Else
                Me.Text = "T R I N I T Y   4.0"
            End If

            'if we are to use Marathon options or not
            If TrinitySettings.MarathonEnabled Then
                MarathonToolStripMenuItem.Visible = True
                MarathonToolStripMenuItem.Visible = True
                ToolStripMenuItem2.Visible = True
            Else
                MarathonToolStripMenuItem.Visible = False
                ToolStripMenuItem2.Visible = False
            End If

            'reads in all spot information
            If Campaign.GetNewActualSpots() Then
                frmMonitor.Close()
                frmSpots.Close()
            End If

            'update ratings
            'Campaign.CalculateSpots()

            'update all labels and information containers
            UpdateInfo()
        End If
        Debug.Print("Activated")
        'pnlStartHelp.Visible = False 'JustStarted   '****CHANGE TO JustStarted when we want to start using panel
    End Sub


    Private Sub cmdAllocate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAllocate.Click
        frmSetup.Close()
        frmAllocate.MdiParent = Me
        frmAllocate.Show()
        frmAllocate.BringToFront()
    End Sub

    Private Sub cmdNotes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNotes.Click
        'Opens a simple notebook for comments
        frmNotes.MdiParent = Me
        frmNotes.Show()
    End Sub

    Private Sub cmdLab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLab.Click
        'opens the Lab where compareing functions can be used to evaluate different options
        frmLab.MdiParent = Me
        frmLab.Show()

    End Sub

    Private Sub frmMain_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        Campaign.GetNewActualSpots()
        'Campaign.CalculateSpots()
        UpdateInfo()
    End Sub

    Private Sub cmdBooking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBooking.Click
        frmBooking.MdiParent = Me
        frmBooking.Show()
        frmBooking.BringToFront()
    End Sub

    Private Sub cmdSpots_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSpots.Click
        frmSpots.MdiParent = Me
        frmSpots.Show()
    End Sub

    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        'first it desposes all open windows and then itself
        For Each Form As Windows.Forms.Form In Me.MdiChildren
            Form.Dispose()
        Next
        Me.Dispose()
        End
    End Sub

    Private Sub SaveToFile()
        Saved = True
        ' Save the campaign
        Dim a As Integer
        Dim en As Long
        Dim ed As String
        On Error GoTo cmdSave_Click_Error

        Dim enableAutosave As Boolean = tmrAutosave.Enabled
        tmrAutosave.Enabled = False

        Trinity.Helper.WriteToLogFile("Start of frmMain/cmdSave_Click")


        'if the file name is empty (not saved before) it opens a save file dialog and then saves the campaign
        If Campaign.Filename = "" Or Campaign.ReadOnly Then
            Dim dlgDialog As New Windows.Forms.SaveFileDialog
            dlgDialog.Title = "Save campaign as..."
            dlgDialog.FileName = Campaign.Name & ".cmp"
            dlgDialog.DefaultExt = "*.cmp"
            dlgDialog.Filter = "Trinity campaigns|*.cmp"
            dlgDialog.InitialDirectory = TrinitySettings.CampaignFiles
            If dlgDialog.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
            Me.Cursor = Windows.Forms.Cursors.WaitCursor
            Campaign.SaveCampaign(dlgDialog.FileName)
        Else
            'if the filename is set the campagn is saved
            Me.Cursor = Windows.Forms.Cursors.WaitCursor
            Campaign.SaveCampaign(Campaign.Filename)
        End If

        'Shows a messege box with save information and resets the cursor
        MsgBox("Campaign was saved.", vbInformation, "T R I N I T Y")
        Me.Cursor = Windows.Forms.Cursors.Default
        tmrAutosave.Enabled = enableAutosave

        On Error GoTo 0
        Exit Sub

cmdSave_Click_Error:

        en = Err.Number
        ed = Err.Description
        If en = 32755 Then Exit Sub
        If IsIDE() Then
            a = MsgBox("Error:" & Chr(10) & Chr(10) & "Runtime Error '" & en & "':" & Chr(10) & Chr(10) & ed & Chr(10) & Chr(10) & "Vill du utföra en felsökning?", vbYesNo, "TRINITY")
            If a = vbNo Then Exit Sub
            ' Stop
            Resume Next
        End If
        Me.Cursor = Windows.Forms.Cursors.Default
        MsgBox("Runtime Error '" & en & "':" & Chr(10) & Chr(10) & ed & " in cmdSave_Click.", vbCritical, "Error")
        Trinity.Helper.WriteToLogFile("ERROR IN frmMain/cmdSave_Click!")

    End Sub

    Private Sub mnuSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSaveAs.Click
        'opens a save dialog and if "OK" saves the campaign
        Dim dlgDialog As New Windows.Forms.SaveFileDialog
        dlgDialog.Title = "Save campaign as..."
        dlgDialog.FileName = Campaign.Name & ".cmp"
        dlgDialog.DefaultExt = "*.cmp"
        dlgDialog.Filter = "Trinity campaigns|*.cmp"
        dlgDialog.InitialDirectory = TrinitySettings.CampaignFiles
        dlgDialog.OverwritePrompt = True
        If dlgDialog.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        Campaign.ID = CreateGUID()
        Try
            Campaign.SaveCampaign(dlgDialog.FileName)
            Saved = True
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("Could not save campaign:" & vbNewLine & vbNewLine & ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSave.Click
        Saved = True
        'calls the same procedure as the save button
        cmdSave_Click(New Object, New EventArgs)
    End Sub

    Private Sub cmdHistory_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHistory.DropDownOpening
        Dim kv As KeyValuePair(Of String, Trinity.cKampanj)
        cmdHistory.DropDownItems.Clear()
        If Campaign.RootCampaign Is Nothing Then
            With DirectCast(cmdHistory.DropDownItems.Add("Add current campaign to history"), Windows.Forms.ToolStripMenuItem)
                .Tag = "Add"
                AddHandler .Click, AddressOf AddCampaignToHistory
            End With
            With DirectCast(cmdHistory.DropDownItems.Add("Clear history"), Windows.Forms.ToolStripMenuItem)
                .Tag = "Clear"
                AddHandler .Click, AddressOf ClearCampaignHistory
            End With
            cmdHistory.DropDownItems.Add("-")
            If Campaign.History.Count > 0 Then
                For Each kv In Campaign.History
                    With DirectCast(cmdHistory.DropDownItems.Add(Format(kv.Value.HistoryDate, "Short Date") & " " & kv.Value.HistoryComment), Windows.Forms.ToolStripMenuItem)
                        .Tag = kv.Value.ID
                        AddHandler .Click, AddressOf OpenHistoryCampaign
                    End With
                Next
            End If
        Else
            AddHandler DirectCast(cmdHistory.DropDownItems.Add("Set this campaign as main"), Windows.Forms.ToolStripMenuItem).Click, AddressOf SetCampaignAsMain
            AddHandler DirectCast(cmdHistory.DropDownItems.Add("Revert to root"), Windows.Forms.ToolStripMenuItem).Click, AddressOf RevertToHistoryRoot
        End If

    End Sub

    Sub ClearCampaignHistory(ByVal sender As Object, ByVal e As EventArgs)
        If Windows.Forms.MessageBox.Show("This will delete all campaigns from the history." & vbCrLf & vbCrLf & "Are you sure you wnat to proceed?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
        Campaign.History.Clear()
    End Sub

    Sub AddCampaignToHistory(ByVal sender As Object, ByVal e As EventArgs)
        'saves the campaign as "history" for comparison or reference
        Dim TmpString As String = InputBox("Comment:", "TRINITY")
        Campaign.SaveCampaignToHistory(TmpString)
    End Sub

    Sub OpenHistoryCampaign(ByVal sender As Object, ByVal e As EventArgs)
        'Loads the campaign as "history" for comparison or reference
        Campaign.OpenHistory(sender.tag, Campaign)
        Campaign.CreateAdedgeSpots()
        Campaign.CalculateSpots()

        'update all labels and information containers
        UpdateInfo()
        cmdLab.Enabled = False
    End Sub

    Sub RevertToHistoryRoot(ByVal sender As Object, ByVal e As EventArgs)
        Campaign.RevertToRootCampaign(Campaign)
        Campaign.CreateAdedgeSpots()
        Campaign.CalculateSpots()
        'update all labels and information containers
        UpdateInfo()
        'enables the lab function button
        cmdLab.Enabled = True
    End Sub

    Sub SetCampaignAsMain(ByVal sender As Object, ByVal e As EventArgs)
        Campaign.SetAsMain(Campaign)
        Campaign.CreateAdedgeSpots()
        Campaign.CalculateSpots()
        'update all labels and information containers
        UpdateInfo()
        'enables the lab function button
        cmdLab.Enabled = True
    End Sub

    Private Sub cmdMonitor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdMonitor.Click
        frmMonitor.MdiParent = Me
        frmMonitor.Show()
    End Sub



    Private Sub cmdReports_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReports.DropDownOpening
        cmdReports.DropDownItems.Clear()
        DirectCast(cmdReports.DropDown, Windows.Forms.ToolStripDropDownMenu).ShowCheckMargin = False
        DirectCast(cmdReports.DropDown, Windows.Forms.ToolStripDropDownMenu).ShowImageMargin = False
        AddHandler cmdReports.DropDownItems.Add("Print Bookings").Click, AddressOf PrintBooking
        cmdReports.DropDownItems.Add("Pre-campaign analysis", Nothing, AddressOf PrintPreAnalysis)
        cmdReports.DropDownItems.Add("Mid-campaign analysis", Nothing, Nothing).Enabled = False
        cmdReports.DropDownItems.Add("Post-campaign analysis", Nothing, AddressOf PrintPostAnalysis).Enabled = True
        cmdReports.DropDownItems.Add("Material specification", Nothing, AddressOf PrintMaterialSpecification).Enabled = True
        cmdReports.DropDownItems.Add("Invoicing details", Nothing, AddressOf PrintInvoiceDetails).Enabled = True
        If TrinitySettings.DefaultArea = "NO" Then
            cmdReports.DropDownItems.Add("Export campaign Unicorn-file", Nothing, AddressOf ExportCampaignToUnicornFileNorway).Enabled = True
            cmdReports.DropDownItems.Add("Export to datorama-file", Nothing, AddressOf ExportCampaignToDatorama).Enabled = True
            If TrinitySettings.UserEmail = "joakim.koch@groupm.com" Then
                cmdReports.DropDownItems.Add("AdEdgeRun test", Nothing, AddressOf runAdedge).Enabled = True
            End If

        ElseIf TrinitySettings.DefaultArea = "SE" Then
            cmdReports.DropDownItems.Add("Export campaign Unicorn-file", Nothing, AddressOf ExportCampaignToUnicornFile).Enabled = True
            If TrinitySettings.UserEmail = "joakim.koch@groupm.com" Then
                cmdReports.DropDownItems.Add("Export campaign to MediaTool", Nothing, AddressOf exportToMediaToolFile).Enabled = True
                'cmdReports.DropDownItems.Add("Export BSH datorama", Nothing, AddressOf getSpotlog).Enabled = True

            End If
        ElseIf TrinitySettings.ActiveDataPath.Contains("Oslfpcp01101z") Then
            cmdReports.DropDownItems.Add("Export campaign Unicorn-file", Nothing, AddressOf ExportCampaignToUnicornFileNorway).Enabled = True
            cmdReports.DropDownItems.Add("Export to datorama-file", Nothing, AddressOf ExportCampaignToDatorama).Enabled = True
        Else

        End If

    End Sub

    'Sub getSpotlog()
    '    Dim exportBSH As New cExportDatoramaBSH()
    '    exportBSH.exportDatoramaFile()
    '    'Dim TmpAdedge As New ConnectWrapper.Brands
    '    'TmpAdedge.setPeriod(Format(Date.FromOADate(mvarUpdatedTo).AddDays(1), ddMMyy) & - & Format(Date.FromOADate(EndDate), ddMMyy))
    '    'TmpAdedge.setArea(mvarArea) 'Se
    '    'TmpAdedge.setChannelsArea(ChannelString, mvarArea) 'All channels
    '    'TmpAdedge.setBrandFilmCode(NO, FilmcodeString())
    '    'TmpAdedge.setBrandType(COMMERCIAL,SPONSOR,PROMO)
    '    'SpotCount = TmpAdedge.Run(True, False, -1)
    'End Sub
    Sub runAdedge(ByVal sender As Object, ByVal e As EventArgs)
        Dim TmpAdedge As New ConnectWrapper.Brands
        Dim SpotCount As Integer
        Dim mvarUpdatedTo As Double = Nothing
        Dim EndDate As Double = Nothing
        Dim _main As clTrinity.Trinity.cKampanj
        Dim TmpChan As clTrinity.Trinity.cChannel
        Dim ChanStr As String = ""

        For Each TmpChan In _main.Channels
            ChanStr = ChanStr & TmpChan.AdEdgeNames & ","
        Next

        TmpAdedge.setPeriod(Format(Date.FromOADate(mvarUpdatedTo).AddDays(1), “ddMMyy”) & “-” & Format(Date.FromOADate(EndDate), “ddMMyy”))
        TmpAdedge.setArea(_main.Area) 'SE
        TmpAdedge.setChannelsArea(ChanStr, _main.Area) 'All channels
        TmpAdedge.setBrandFilmCode(“NO”, "NMTGMU3001N8")
        TmpAdedge.setBrandType(“COMMERCIAL,SPONSOR,PROMO”)
        SpotCount = TmpAdedge.Run(True, False, -1)
    End Sub
    Sub PrintInvoiceDetails(ByVal sender As Object, ByVal e As EventArgs)
        frmPrintInvoice.ShowDialog()
    End Sub

    Sub ExportCampaignToUnicornFile(ByVal sender As Object, ByVal e As EventArgs)
        'Dim mnuWeeks As New Windows.Forms.ContextMenuStrip
        ' mnuWeeks.Items.Clear()

        'frmExportCampaignUnicorn.ShowDialog()
        If TrinitySettings.SaveCampaignsAsFiles Then
            frmExportCampaignUnicornNorway.ShowDialog()
        Else
            frmExportCampaignUnicorn_new.ShowDialog()
        End If
    End Sub
    Sub ExportCampaignToUnicornFileNorway(ByVal sender As Object, ByVal e As EventArgs)
        'frmExportCampaignUnicorn.ShowDialog()
        frmExportCampaignUnicornNorway.ShowDialog()
    End Sub
    Sub ExportCampaignToDatorama(ByVal sender As Object, ByVal e As EventArgs)
        'frmExportCampaignUnicorn.ShowDialog()
        Dim exportDatorama As New cExportDatoramaFile(Campaign)
        exportDatorama.exportDatoramaFile()
        'frmExportCampaignToDatoramaExport.ShowDialog()
    End Sub
    Sub exportToMediaToolFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim export As New cExportFileToMediatool(Campaign)
        export.exportDatoramaFile()
    End Sub

    Sub PrintPostAnalysis(ByVal sender As Object, ByVal e As EventArgs)
        frmPostCampaignAnalysis.ShowDialog()
    End Sub

    Sub PrintPreAnalysis(ByVal sender As Object, ByVal e As EventArgs)
        frmPreCampaignAnalysis.ShowDialog()
    End Sub

    Sub PrintBooking(ByVal sender As Object, ByVal e As EventArgs)
        Dim frmPrint As New frmPrintBooking()

        frmPrint.ShowDialog()
    End Sub

    Private Sub mnuWindow_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuWindow.DropDownOpening
        Dim TmpMnu As Windows.Forms.ToolStripItem
        Dim TmpForm As Windows.Forms.Form
        Dim RemoveItems As New Collection
        For Each TmpMnu In mnuWindow.DropDownItems
            If Not TmpMnu.Tag Is Nothing Then
                RemoveItems.Add(TmpMnu)
            End If
        Next
        For Each TmpMnu In RemoveItems
            mnuWindow.DropDownItems.Remove(TmpMnu)
        Next
        For Each TmpForm In Me.MdiChildren
            TmpMnu = New Windows.Forms.ToolStripMenuItem
            TmpMnu.Text = TmpForm.Text
            TmpMnu.Tag = TmpForm
            If Me.ActiveMdiChild Is TmpForm Then
                DirectCast(TmpMnu, Windows.Forms.ToolStripMenuItem).Checked = True
            End If
            AddHandler TmpMnu.Click, AddressOf SwitchForm
            mnuWindow.DropDownItems.Add(TmpMnu)
        Next
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CascadeToolStripMenuItem.Click

        Dim PrevX As Integer = 10
        Dim PrevY As Integer = 10
        For Each TmpForm As Windows.Forms.Form In Me.MdiChildren
            TmpForm.WindowState = Windows.Forms.FormWindowState.Normal
            TmpForm.Location = New Drawing.Point(PrevX + 50, PrevY + 50)
            TmpForm.Width = Me.Width * 0.75
            TmpForm.Height = Me.Height * 0.5
            PrevX = TmpForm.Location.X
            PrevY = TmpForm.Location.X
        Next

    End Sub

    Private Sub TileHorizontallyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TileHorizontallyToolStripMenuItem.Click
        Dim Count As Integer
        Dim i As Integer = 0
        For Each TmpForm As Windows.Forms.Form In Me.MdiChildren
            Count = Count + 1
        Next

        For Each TmpForm As Windows.Forms.Form In Me.MdiChildren
            TmpForm.WindowState = Windows.Forms.FormWindowState.Normal
            TmpForm.Location = New Drawing.Point(i * (Me.Width / Count), 0)
            TmpForm.Width = Me.Width / Count
            TmpForm.Height = Me.Height
            i = i + 1
        Next
    End Sub

    Private Sub TileVerticallyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TileVerticallyToolStripMenuItem.Click
        Dim Count As Integer
        Dim i As Integer = 0
        For Each TmpForm As Windows.Forms.Form In Me.MdiChildren
            Count = Count + 1
        Next

        For Each TmpForm As Windows.Forms.Form In Me.MdiChildren
            TmpForm.WindowState = Windows.Forms.FormWindowState.Normal
            TmpForm.Location = New Drawing.Point(0, i * (Me.Height / Count))
            TmpForm.Height = Me.Height / Count
            TmpForm.Width = Me.Width
            i = i + 1
        Next
    End Sub

    Sub SwitchForm(ByVal sender As Object, ByVal e As EventArgs)
        DirectCast(sender.tag, Windows.Forms.Form).Activate()
    End Sub

    Private Sub txtMain_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMain.KeyUp
        'if main target is altered the campaign is in "not saved" status
        Saved = False
    End Sub

    Private Sub txtSec_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSec.KeyUp
        'if second target is altered the campaign is in "not saved" status
        Saved = False
    End Sub

    Private Sub txtThird_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtThird.KeyUp
        'if third target is altered the campaign is in "not saved" status
        Saved = False
    End Sub

    Private Sub txtName_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtName.KeyUp
        'if campaign name is altered the campaign is in "not saved" status, and updates are applyed to the campaign and the main window 
        Campaign.Name = txtName.Text
        Saved = False
        If Campaign.Name <> "" Then
            Me.Text = "T R I N I T Y   4.0  -  " & Campaign.Name
        Else
            Me.Text = "T R I N I T Y   4.0"
        End If
    End Sub

    Private Sub pnlInfo_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlInfo.VisibleChanged
        'If pnlInfo.Visible Then
        '    PopulateClientCombo()
        '    txtMain.Text = Campaign.MainTarget.TargetName
        '    txtSec.Text = Campaign.SecondaryTarget.TargetName
        '    txtThird.Text = Campaign.ThirdTarget.TargetName
        '    txtName.Text = Campaign.Name
        'End If
    End Sub

    Private Sub picPin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picPin.Click
        'the pin windows button is clicked, this action pins the information form to the right
        pnlInfo.Visible = False ' hides the window
        frmInfo.MdiParent = Me
        TrinitySettings.ShowInfoInWindow = True
        frmInfo.Show()
    End Sub

    Private Sub cmdInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInfo.Click
        'dispalys the infromation window
        If TrinitySettings.ShowInfoInWindow Then
            frmInfo.MdiParent = Me
            frmInfo.Show()
        End If
    End Sub

    Private Sub cmbClient_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClient.SelectedIndexChanged
        Saved = False
        'populates the product combo box according to what client is selected
        PopulateProductCombo()
    End Sub

    Private Sub cmbProduct_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProduct.SelectedIndexChanged
        Saved = False
        'sets the product ID depending on the users choise in the combo box
        Campaign.ProductID = DirectCast(cmbProduct.SelectedItem, CBItem).Tag
    End Sub

    Private Sub cmdEditClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditClient.Click
        Saved = False
        'opens a Add client window in editing mode ("EDIT" tag)
        frmAddClient.Tag = "EDIT"
        frmAddClient.ShowDialog()
        'populates the clients combo box
        PopulateClientCombo()
    End Sub

    Private Sub cmdAddProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddProduct.Click
        Saved = False
        'opens a Add product window
        frmAddProduct.Dispose()
        frmAddProduct.Tag = ""
        frmAddProduct.ShowDialog()
        'if a new prodict was added we need to update the combo box
        If frmAddProduct.txtName.Text <> "" Then
            PopulateProductCombo()
        End If
    End Sub

    Private Sub cmdEditProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditProduct.Click
        Saved = False
        'opens a Add product window with the "EDIT" tag
        If cmbClient.SelectedItem Is Nothing Then
            Exit Sub
        End If
        frmAddProduct.Tag = "EDIT"
        frmAddProduct.ShowDialog()
        'repopulates the product combo box
        PopulateProductCombo()
    End Sub


    Sub PopulateClientCombo()
        'selcts all clients and put the names into the combo box

        Try
            Dim clients As DataTable = DBReader.getAllClients()
            cmbClient.Items.Clear()
            cmbClient.DisplayMember = "Text"
            For Each dr As DataRow In clients.Rows
                Dim TmpItem As New CBItem
                TmpItem.Text = dr.Item("name") 'rd!name
                TmpItem.Tag = dr.Item("id") 'rd!id
                cmbClient.Items.Add(TmpItem)
                If TmpItem.Tag = Campaign.ClientID Then
                    cmbClient.Text = TmpItem.Text
                End If
            Next
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("There was an error while populating the Client dropdown list:" & vbCrLf & vbCrLf & "'" & ex.Message & "'")
        End Try
        'Dim com As New Odbc.OdbcCommand("SELECT * FROM Clients", DBConn)
        'Dim rd As Odbc.OdbcDataReader = com.ExecuteReader

        'cmbClient.Items.Clear()
        'cmbClient.DisplayMember = "Text"
        'While rd.Read
        '    Dim TmpItem As New CBItem
        '    TmpItem.Text = rd!name
        '    TmpItem.Tag = rd!id
        '    cmbClient.Items.Add(TmpItem)
        '    If rd!ID = Campaign.ClientID Then
        '        cmbClient.Text = rd!Name
        '    End If
        'End While
        'rd.Close()
    End Sub


    Sub PopulateProductCombo()
        'selcts all products matching the client ID and put the names into the combo box and the ID into a hidden tag
        cmbProduct.Items.Clear()
        If cmbClient.SelectedItem Is Nothing Then
            Exit Sub
        End If

        Campaign.ClientID = DirectCast(cmbClient.SelectedItem, CBItem).Tag
        cmbProduct.Items.Clear()
        cmbProduct.DisplayMember = "Text"

        Dim products As DataTable = DBReader.getAllProducts(DirectCast(cmbClient.SelectedItem, CBItem).Tag)
        For Each dr As DataRow In products.Rows
            Dim TmpItem As New CBItem
            TmpItem.Text = dr.Item("name")
            TmpItem.Tag = dr.Item("id")
            cmbProduct.Items.Add(TmpItem)
            If TmpItem.Tag = Campaign.ProductID Then
                cmbProduct.Text = TmpItem.Text
            End If
        Next



        'Dim com As New Odbc.OdbcCommand("SELECT id,Name FROM Products WHERE ClientID=" & DirectCast(cmbClient.SelectedItem, CBItem).Tag, DBConn)
        'Dim rd As Odbc.OdbcDataReader = com.ExecuteReader

        'Campaign.ClientID = DirectCast(cmbClient.SelectedItem, CBItem).Tag
        'cmbProduct.Items.Clear()
        'cmbProduct.DisplayMember = "Text"
        'While rd.Read
        '    Dim TmpItem As New CBItem
        '    TmpItem.Text = rd!Name
        '    TmpItem.Tag = rd!id
        '    cmbProduct.Items.Add(TmpItem)
        '    'if one product is already in the campaign we set it to default
        '    If rd!ID = Campaign.ProductID Then
        '        cmbProduct.Text = rd!Name
        '    End If
        'End While
    End Sub

    Public Sub UpdateInfo()
        Dim i As Integer

        If TrinitySettings.ShowInfoInWindow Then
            pnlInfo.Visible = False
            Exit Sub
        Else
            pnlInfo.Visible = cmdAllocate.Enabled
        End If

        'repopulate the client combobox in the information window
        PopulateClientCombo()

        cmbFF.SelectedIndex = Campaign.FrequencyFocus
        grdReach.Rows.Clear()
        grdReach.Rows.Add(10)
        grdReach.ReadOnly = False
        For i = 1 To 10
            '            grdReach.Rows(i).Cells(0).Value = Format(Campaign.ReachTargets(i, "Nat"), "0.0")
            grdReach.AutoResizeRow(i - 1, Windows.Forms.DataGridViewAutoSizeRowMode.AllCells)
        Next
        For i = 1 To 10
            Dim lblFreq As System.Windows.Forms.Label = pnlInfo.Controls("lblFreq" & i)
            If Not lblFreq Is Nothing Then
                lblFreq.Text = i & "+"
                lblFreq.AutoSize = True
                lblFreq.Top = grdReach.GetRowDisplayRectangle(i - 1, True).Top + grdReach.Top
                lblFreq.Height = grdReach.GetRowDisplayRectangle(i - 1, True).Height
                lblFreq.TextAlign = Drawing.ContentAlignment.MiddleLeft
                lblFreq.Left = 6
                pnlInfo.Controls.Add(lblFreq)
                grdReach.Rows(i - 1).Cells(0).ReadOnly = False
                grdReach.Rows(i - 1).Cells(1).ReadOnly = True
            End If
        Next

        'Clears the 3 comboboxes with target ranges (information window)
        cmbMainUni.Items.Clear()
        cmbSecondUni.Items.Clear()
        cmbThirdUni.Items.Clear()



        For i = 0 To Campaign.Universes.Count - 1
            cmbMainUni.Items.Add(Campaign.Universes(i))
            cmbSecondUni.Items.Add(Campaign.Universes(i))
            cmbThirdUni.Items.Add(Campaign.Universes(i))
        Next
        If Campaign.Universes.Count > 0 Then
            If Campaign.MainTarget.Universe = "" Then
                cmbMainUni.SelectedItem = Campaign.Universes(0)
            Else
                cmbMainUni.Text = Campaign.Universes(Campaign.MainTarget.Universe)
            End If
            If Campaign.SecondaryTarget.Universe = "" Then
                cmbSecondUni.SelectedItem = Campaign.Universes(0)
            Else
                cmbSecondUni.Text = Campaign.Universes(Campaign.SecondaryTarget.Universe)
            End If
            If Campaign.ThirdTarget.Universe = "" Then
                cmbThirdUni.SelectedItem = Campaign.Universes(0)
            Else
                cmbThirdUni.Text = Campaign.Universes(Campaign.ThirdTarget.Universe)
            End If
        End If

        'Set label for usercompany
        If TrinitySettings.Developer Then
            lblUserCompany.Visible = True
            lblUserCompany.Text = TrinitySettings.ConnectionString(Trinity.cSettings.SettingsLocationEnum.locNetwork)
        Else
            lblUserCompany.Visible = False

        End If

        'update the target textfields in the information window
        txtMain.Text = Campaign.MainTarget.TargetName
        txtSec.Text = Campaign.SecondaryTarget.TargetName
        txtThird.Text = Campaign.ThirdTarget.TargetName
        txtName.Text = Campaign.Name

        If Campaign.Channels.Count > 0 AndAlso Campaign.UpdatedTo >= Campaign.StartDate Then
            lblUpdatedTo.Text = "Updated to: " & Format(Date.FromOADate(Campaign.UpdatedTo), "Short date")
        Else
            lblUpdatedTo.Text = "Updated to: -"
        End If
        If Not Campaign.RootCampaign Is Nothing Then
            For Each ctrl As Windows.Forms.Control In pnlInfo.Controls
                If Not ctrl.Name = "picPin" Then ctrl.Enabled = False
            Next
        Else
            For Each ctrl As Windows.Forms.Control In Me.Controls
                ctrl.Enabled = True
            Next
        End If
        cmbFF.SelectedIndex = Campaign.FrequencyFocus
    End Sub

    Function AcceptAll(ByVal sender As Object, ByVal certificate As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function

    Private Sub AddordersInMarathonToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddOrdersInMarathonToolStripMenuItem.Click
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        Dim _marathon As New Marathon(TrinitySettings.MarathonCommand)
        Dim info As DataTable = DBReader.getAllFromProducts(Campaign.ProductID)
        If info.Rows.Count < 1 Then
            Windows.Forms.MessageBox.Show("You must choose a client and product to create orders.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        End If


        'Create orders where combinations is bundled as one and required reporting as one.
        If Campaign.Combinations.Count > 1 Then
            For Each tmpC As Trinity.cCombination In Campaign.Combinations
                If tmpC.PrintAsOne Then
                    For Each combinationChannel As Trinity.cCombinationChannel In tmpC.Relations
                        Dim netBudget As Integer = 0
                        Dim bundleName As String = ""

                    Next
                End If
                'For Each TmpChan As Trinity.cChannel In Campaign.Channels
                '    For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                '        If TmpBT.OrderNumber = "" Then
                '            If TmpBT.BookIt Then
                '                Dim _order As New Marathon.Order
                '                _order.PlanNumber = Campaign.MarathonPlanNr
                '                _order.MediaID = TmpChan.MarathonName
                '                _order.CompanyID = info.Rows(0)!MarathonCompany

                '                Dim _orderNo As String
                '                Try
                '                    _orderNo = _marathon.CreateOrder(_order)
                '                    TmpBT.OrderNumber = _orderNo
                '                Catch ex As Exception
                '                    Windows.Forms.MessageBox.Show("There was an error while creating the order for " & TmpChan.ChannelName & "." & vbCrLf & vbCrLf & "Marathon response: " & ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                '                End Try
                '            End If
                '        End If
                '    Next
                'Next
            Next
        Else
            'Create orders for each of the channels and each of the booking types in the campaign. We should get an order number from each
            For Each TmpChan As Trinity.cChannel In Campaign.Channels

                For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                    If TmpBT.OrderNumber = "" Then
                        If TmpBT.BookIt Then
                            Dim _order As New Marathon.Order
                            _order.PlanNumber = Campaign.MarathonPlanNr
                            _order.MediaID = TmpChan.MarathonName
                            _order.CompanyID = info.Rows(0)!MarathonCompany

                            Dim _orderNo As String
                            Try
                                _orderNo = _marathon.CreateOrder(_order)
                                TmpBT.OrderNumber = _orderNo
                            Catch ex As Exception
                                Windows.Forms.MessageBox.Show("There was an error while creating the order for " & TmpChan.ChannelName & "." & vbCrLf & vbCrLf & "Marathon response: " & ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                            End Try
                        End If
                    End If
                Next
            Next
        End If

        'Campaign.MarathonOtherOrder = 1
        'We have now created a plan containing orders for all booking types
        'We have not created any insertions yet, that is done later.
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub CreateOrdersInMarathonToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateOrdersInMarathonToolStripMenuItem.Click
        Dim _marathon As New Marathon(TrinitySettings.MarathonCommand)

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        Dim info As DataTable = DBReader.getAllFromProducts(Campaign.ProductID)
        If info.Rows.Count < 1 Then
            Windows.Forms.MessageBox.Show("You must choose a client and product to create orders.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            'Added by JK Annoying cursor
            Me.Cursor = Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If Campaign.MarathonPlanNr = 0 Then
            'Create the plan that corresponds to this campaign in Marathon
            Dim _plan As New Marathon.Plan
            _plan.Name = Campaign.Name
            _plan.UserID = TrinitySettings.MarathonUser
            With info.Rows(0)
                _plan.CompanyID = !MarathonCompany
                _plan.ClientID = !MarathonClient
                _plan.ProductID = !MarathonProduct
                _plan.AgreementID = !MarathonContract
            End With
CreatePlan:
            'Upload this XML to Marathon - an exception will be raised if something goes wrong
            Dim _planNo As String
            Try
                _planNo = _marathon.CreatePlan(_plan)
            Catch ex As Exception
                If Windows.Forms.MessageBox.Show("There was an error while creating the plan." & vbCrLf & vbCrLf & "Marathon response: " & ex.Message & vbCrLf & vbCrLf & "Do you want to edit the client details?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    frmAddProduct.Tag = "EDIT"
                    frmAddProduct.ShowDialog()
                    GoTo CreatePlan
                End If
                Exit Sub
            End Try
            'Get the plan number
            Campaign.MarathonPlanNr = _planNo
        End If

        'If Campaign.Costs.Count > 0 Then
        '    If TrinitySettings.GeneralMedia <> "" Then
        '        'Create the first order of the campaign, make the media type "General Media"
        '        Dim _order As New Marathon.Order
        '        _order.PlanNumber = Campaign.MarathonPlanNr
        '        _order.MediaID = Campaign.Costs.Item(3).MarathonID
        '        _order.CompanyID = info.Rows(0) !MarathonCompany

        '        Dim _orderNo As String
        '        Try
        '            _orderNo = _marathon.CreateOrder(_order)
        '            Campaign.MarathonOtherOrder = _orderNo
        '        Catch ex As Exception
        '            Windows.Forms.MessageBox.Show("There was an error while creating the order for the general channel." & vbCrLf & vbCrLf & "Marathon response: " & ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        '        End Try
        '    End If
        'End If

        If Campaign.Costs.Count > 0 Then
            For Each tmpCost As Trinity.cCost In Campaign.Costs
                If tmpCost.MarathonID <> "0" Then
                    Dim _order As New Marathon.Order
                    _order.PlanNumber = Campaign.MarathonPlanNr
                    _order.MediaID = tmpCost.MarathonID
                    _order.CompanyID = info.Rows(0)!MarathonCompany

                    Dim _orderNo As String
                    Try
                        _orderNo = _marathon.CreateOrder(_order)
                        Campaign.MarathonOtherOrder = _orderNo
                    Catch ex As Exception
                        Windows.Forms.MessageBox.Show("There was an error while creating the other expenses like service fee, spotcontroll etc." & vbCrLf & vbCrLf & "Marathon response: " & ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                    End Try
                End If
            Next
        End If
        If Campaign.Combinations.Count > 0 Then

            For Each tmpCombo As Trinity.cCombination In Campaign.Combinations
                Dim _orderNo As String = ""
                For Each cc As Trinity.cCombinationChannel In tmpCombo.Relations
                    If cc.Bookingtype.BookIt And tmpCombo.sendAsOneUnitTOMarathon Then
                        Dim _order As New Marathon.Order
                        _order.PlanNumber = Campaign.MarathonPlanNr
                        _order.MediaID = tmpCombo.MarathonIDCombination
                        _order.CompanyID = info.Rows(0)!MarathonCompany

                        Try
                            If _orderNo <> "" Then
                                _orderNo = _marathon.CreateOrder(_order)
                            End If
                            cc.Bookingtype.OrderNumber = _orderNo
                        Catch ex As Exception
                            Windows.Forms.MessageBox.Show("There was an error while creating the order for " & cc.ChannelName & "." & vbCrLf & vbCrLf & "Marathon response: " & ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                        End Try
                    End If
                Next
            Next
        End If
        'Create orders for each of the channels and each of the booking types in the campaign. We should get an order number from each
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                Dim _order As New Marathon.Order
                Dim _orderNo As String

                'JOHAN REMOVED THE ROWS BELOW BECAUSE THEY CAUSE ALL ORDERS TO BE ADDED MULTIPLE TIMES
                '

                'If TmpBT.BookIt And Campaign.Combinations.Count > 0 Then
                '    For each c As Trinity.cCombination in Campaign.Combinations
                '        For i As Integer = 1 To c.Relations.count
                '            If not c.Relations(i).Bookingtype is TmpBT          
                '                _order.PlanNumber = Campaign.MarathonPlanNr
                '                _order.MediaID = TmpChan.MarathonName
                '                _order.CompanyID = info.Rows(0) !MarathonCompany

                '                Try
                '                    _orderNo = _marathon.CreateOrder(_order)
                '                    TmpBT.OrderNumber = _orderNo
                '                Catch ex As Exception
                '                    Windows.Forms.MessageBox.Show("There was an error while creating the order for " & TmpChan.ChannelName & "." & vbCrLf & vbCrLf & "Marathon response: " & ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                '                End Try
                '            End If
                '        Next
                '    Next                    
                'End If

                If TmpBT.BookIt And TmpBT.OrderNumber <> "" Then
                    _order.PlanNumber = Campaign.MarathonPlanNr
                    _order.MediaID = TmpChan.MarathonName
                    _order.CompanyID = info.Rows(0)!MarathonCompany

                    Try
                        If TmpBT.OrderNumber = "" Then
                            _orderNo = _marathon.CreateOrder(_order)
                            TmpBT.OrderNumber = _orderNo
                        End If
                    Catch ex As Exception
                        Windows.Forms.MessageBox.Show("There was an error while creating the order for " & TmpChan.ChannelName & "." & vbCrLf & vbCrLf & "Marathon response: " & ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                    End Try
                End If
            Next
        Next
        'Campaign.MarathonOtherOrder = 1
        'We have now created a plan containing orders for all booking types
        'We have not created any insertions yet, that is done later.
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmdBudget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBudget.Click
        frmBudget.MdiParent = Me
        frmBudget.Show()
    End Sub

    Private Sub PreferencesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreferencesToolStripMenuItem.Click
        Dim _pref As New frmPreferences
        For Each _plugin As IPlugin In Plugins
            If _plugin.PreferencesTab IsNot Nothing Then
                Dim _p As TrinityPlugin.pluginPreferencesTab = _plugin.PreferencesTab
                _pref.tabPref.TabPages.Add(_p)
                AddHandler _pref.OK_Button.Click, Sub()
                                                      _p.SaveData()
                                                  End Sub
            End If
        Next
        _pref.ShowDialog()
    End Sub


    Private Sub cmdCalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCalculate.Click
        Saved = False
        Dim mnuCalculate As New System.Windows.Forms.ContextMenuStrip

        mnuCalculate.Items.Add("Use last weeks of data", Nothing, AddressOf mnuUseLastWeeks_Click)
        mnuCalculate.Items.Add("Use same period last year", Nothing, AddressOf mnuUseSamePeriod_Click)
        mnuCalculate.Items.Add("Create custom period", Nothing, AddressOf mnuUseCustomPeriod_Click)
        mnuCalculate.Show(cmdCalculate, 0, cmdCalculate.Height)
        UpdateInfo()
    End Sub

    Private Sub mnuUseLastWeeks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Periodstr As String = ""
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim IsUsed As Boolean
        Dim i As Integer
        Dim TmpStr As String
        Dim UseSponsorship As Boolean = False
        Dim UseCommercial As Boolean = False

        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        'Periodstr = "-" & Trim(Campaign.Channels(1).BookingTypes(1).Weeks.Count) & "fw"

        Dim Karma As New Trinity.cKarma(Campaign)
        Dim TmpDate As Long = Campaign.EndDate
        Dim DateDiff As Long

        While TmpDate >= Karma.KarmaAdedge.getDataRangeTo(Connect.eDataType.mSpot)
            TmpDate = TmpDate - 1
        End While
        DateDiff = Campaign.EndDate - TmpDate

        For Each TmpWeek As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
            Periodstr &= Format(Date.FromOADate(TmpWeek.StartDate - DateDiff), "ddMMyy") & "-" & Format(Date.FromOADate(TmpWeek.EndDate - DateDiff), "ddMMyy") & ","
        Next
        Karma.ReferencePeriod = Periodstr

        For Each TmpChan In Campaign.Channels
            IsUsed = False
            For Each TmpBT In TmpChan.BookingTypes
                If TmpBT.BookIt Then
                    IsUsed = True
                End If
                If TmpBT.IsSponsorship Then
                    UseSponsorship = True
                ElseIf TmpBT.BookIt Then
                    UseCommercial = True
                End If
            Next
            If IsUsed Then
                Karma.Channels.Add(TmpChan.ChannelName)
            End If
        Next
        Karma.Weeks = Campaign.Channels(1).BookingTypes(1).Weeks.Count

        Karma.Populate(UseSponsorship, UseCommercial)

        If Campaign.Name Is Nothing Then Campaign.Name = "Campaign 1"

        TmpStr = Campaign.Name
        Karma.Campaigns.Add(TmpStr, Campaign)
        frmProgress.Status = "Calculating reach for " & TmpStr
        frmProgress.Progress = 0
        frmProgress.Show()
        AddHandler Karma.Campaigns(TmpStr).Progress, AddressOf Progress
        Karma.Campaigns(TmpStr).Run()
        frmProgress.Hide()

        For i = 1 To 10
            Campaign.ReachGoal(i) = Karma.Campaigns.Item(TmpStr).Reach(0, i)
            Campaign.ReachGoal(i, Trinity.cKampanj.ReachTargetEnum.rteSecondTarget) = Karma.Campaigns.Item(TmpStr).Reach(0, i, Trinity.cKampanj.ReachTargetEnum.rteSecondTarget)
        Next

        Me.Cursor = Windows.Forms.Cursors.Default
        grdReach.Invalidate()
    End Sub

    Private Sub mnuUseSamePeriod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Periodstr As String = ""
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim IsUsed As Boolean
        Dim i As Integer
        Dim TmpStr As String
        Dim UseSponsorship As Boolean = False
        Dim UseCommercial As Boolean = False

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        Dim Karma As New Trinity.cKarma(Campaign)
        Dim TmpDate As Long = Date.FromOADate(Campaign.EndDate).AddYears(-1).ToOADate
        Dim DateDiff As Long

        While Weekday(Date.FromOADate(TmpDate), FirstDayOfWeek.Monday) <> Weekday(Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday)
            TmpDate = TmpDate + 1
        End While
        DateDiff = Campaign.EndDate - TmpDate

        For Each TmpWeek As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
            Periodstr &= Format(Date.FromOADate(TmpWeek.StartDate - DateDiff), "ddMMyy") & "-" & Format(Date.FromOADate(TmpWeek.EndDate - DateDiff), "ddMMyy") & ","
        Next
        Karma.ReferencePeriod = Periodstr

        For Each TmpChan In Campaign.Channels
            IsUsed = False
            For Each TmpBT In TmpChan.BookingTypes
                If TmpBT.BookIt Then
                    IsUsed = True
                End If
                If TmpBT.IsSponsorship Then
                    UseSponsorship = True
                ElseIf TmpBT.BookIt Then
                    UseCommercial = True
                End If
            Next
            If IsUsed Then
                Karma.Channels.Add(TmpChan.ChannelName)
            End If
        Next
        Karma.Weeks = Campaign.Channels(1).BookingTypes(1).Weeks.Count

        Karma.Populate(UseSponsorship, UseCommercial)

        TmpStr = Campaign.Name
        Karma.Campaigns.Add(TmpStr, Campaign)
        frmProgress.Status = "Calculating reach for " & TmpStr
        frmProgress.Progress = 0
        frmProgress.Show()
        AddHandler Karma.Campaigns(TmpStr).Progress, AddressOf Progress
        Karma.Campaigns(TmpStr).Run()
        frmProgress.Hide()

        For i = 1 To 10
            Campaign.ReachGoal(i) = Karma.Campaigns.Item(TmpStr).Reach(0, i)
            Campaign.ReachGoal(i, Trinity.cKampanj.ReachTargetEnum.rteSecondTarget) = Karma.Campaigns.Item(TmpStr).Reach(0, i, Trinity.cKampanj.ReachTargetEnum.rteSecondTarget)
        Next

        Me.Cursor = Windows.Forms.Cursors.Default
        grdReach.Invalidate()
    End Sub

    Private Sub mnuUseCustomPeriod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Periodstr As String
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim IsUsed As Boolean
        Dim i As Integer
        Dim TmpStr As String
        Dim UseSponsorship As Boolean = False
        Dim UseCommercial As Boolean = False

        frmDates.ShowDialog()
        If frmDates.DialogResult = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        Periodstr = Format(frmDates.dateFrom.Value, "ddMMyy") & "-" & Format(frmDates.dateTo.Value, "ddMMyy")

        Dim Karma As New Trinity.cKarma(Campaign)
        Karma.ReferencePeriod = Periodstr

        For Each TmpChan In Campaign.Channels
            IsUsed = False
            For Each TmpBT In TmpChan.BookingTypes
                If TmpBT.BookIt Then
                    IsUsed = True
                End If
                If TmpBT.IsSponsorship Then
                    UseSponsorship = True
                ElseIf TmpBT.BookIt Then
                    UseCommercial = True
                End If
            Next
            If IsUsed Then
                Karma.Channels.Add(TmpChan.ChannelName)
            End If
        Next
        Karma.Weeks = Campaign.Channels(1).BookingTypes(1).Weeks.Count

        Karma.Populate(UseSponsorship, UseCommercial)

        TmpStr = Campaign.Name
        Karma.Campaigns.Add(TmpStr, Campaign)
        frmProgress.Status = "Calculating reach for " & TmpStr
        frmProgress.Progress = 0
        frmProgress.Show()
        AddHandler Karma.Campaigns(TmpStr).Progress, AddressOf Progress
        Karma.Campaigns(TmpStr).Run()
        frmProgress.Hide()

        For i = 1 To 10
            Campaign.ReachGoal(i) = Karma.Campaigns.Item(TmpStr).Reach(0, i)
            Campaign.ReachGoal(i, Trinity.cKampanj.ReachTargetEnum.rteSecondTarget) = Karma.Campaigns.Item(TmpStr).Reach(0, i, Trinity.cKampanj.ReachTargetEnum.rteSecondTarget)
        Next

        Me.Cursor = Windows.Forms.Cursors.Default
        grdReach.Invalidate()
    End Sub

    Sub Progress(ByVal p As Single)
        frmProgress.Progress = p
    End Sub

    Private Sub cmbMainUni_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMainUni.SelectedIndexChanged
        Campaign.MainTarget.Universe = Campaign.Universes.GetKey(sender.selectedindex)
        lblMainSize.Text = Format(Campaign.MainTarget.UniSize * 1000, "##,##0")
        If Campaign.MainTarget.UniSize = 0 Then
            lblMainTarget.ForeColor = Color.Red
        Else
            lblMainTarget.ForeColor = Color.Black
        End If
    End Sub

    Private Sub cmbSecondUni_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSecondUni.SelectedIndexChanged
        Campaign.SecondaryTarget.Universe = Campaign.Universes.GetKey(sender.selectedindex)
        lblSecondSize.Text = Format(Campaign.SecondaryTarget.UniSize * 1000, "##,##0")
        If Campaign.SecondaryTarget.UniSize = 0 Then
            lblSecondTarget.ForeColor = Color.Red
        Else
            lblSecondTarget.ForeColor = Color.Black
        End If
    End Sub

    Private Sub cmbThirdUni_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbThirdUni.SelectedIndexChanged
        Campaign.ThirdTarget.Universe = Campaign.Universes.GetKey(sender.selectedindex)
        lblThirdSize.Text = Format(Campaign.ThirdTarget.UniSize * 1000, "##,##0")
        If Campaign.ThirdTarget.UniSize = 0 Then
            lblThirdTarget.ForeColor = Color.Red
        Else
            lblThirdTarget.ForeColor = Color.Black
        End If
    End Sub

    Private Sub txtMain_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMain.TextChanged
        Saved = False
        Try
            Campaign.MainTarget.TargetName = txtMain.Text
        Catch ex As Exception

        End Try
        lblMainSize.Text = Format(Campaign.MainTarget.UniSize * 1000, "##,##0")
        If Campaign.MainTarget.UniSize = 0 Then
            lblMainTarget.ForeColor = Drawing.Color.Red
        Else
            lblMainTarget.ForeColor = Drawing.Color.Black
        End If
    End Sub

    Private Sub txtSec_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSec.TextChanged
        Saved = False
        Campaign.SecondaryTarget.TargetName = txtSec.Text
        lblSecondSize.Text = Format(Campaign.SecondaryTarget.UniSize * 1000, "##,##0")
        If Campaign.SecondaryTarget.UniSize = 0 Then
            lblSecondTarget.ForeColor = Drawing.Color.Red
        Else
            lblSecondTarget.ForeColor = Drawing.Color.Black
        End If
    End Sub

    Private Sub txtThird_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtThird.TextChanged
        Saved = False
        Campaign.ThirdTarget.TargetName = txtThird.Text
        lblThirdSize.Text = Format(Campaign.ThirdTarget.UniSize * 1000, "##,##0")
        If Campaign.MainTarget.UniSize = 0 Then
            lblThirdTarget.ForeColor = Drawing.Color.Red
        Else
            lblThirdTarget.ForeColor = Drawing.Color.Black
        End If
    End Sub

    Private Sub grdReach_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdReach.CellEndEdit
        Saved = False
    End Sub

    Private Sub grdReach_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdReach.CellValueNeeded
        If cmbTarget.SelectedIndex = -1 Then cmbTarget.SelectedIndex = 0
        If e.ColumnIndex = 0 Then
            e.Value = Format(Campaign.ReachGoal(e.RowIndex + 1, cmbTarget.SelectedIndex), "N1")
        Else
            e.Value = Format(Campaign.ReachActual(e.RowIndex + 1, cmbTarget.SelectedIndex), "N1")
        End If
    End Sub

    Private Sub grdReach_CellValuePushed(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdReach.CellValuePushed
        If e.ColumnIndex = 0 Then
            Campaign.ReachGoal(e.RowIndex + 1) = e.Value
        End If
    End Sub

    Sub NewCampaign(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click, NewToolStripMenuItem.Click, lblNewCampaign.Click
        Dim i As Integer
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType
        Dim rply As Windows.Forms.DialogResult
        Me.Text = ""

        If Saved = False Then
            rply = Windows.Forms.MessageBox.Show("Do you want to save before you exit?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question)
        End If
        If rply = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        ElseIf rply = Windows.Forms.DialogResult.OK Then
            DBReader.closeConnection()
            Exit Sub
        ElseIf rply = Windows.Forms.DialogResult.Yes Then
            cmdSave_Click(New Object, New EventArgs)
        End If

        'hides the recent panel
        pnlStartHelp.Visible = False
        Campaign.ExtendedInfos.RemoveAll() 'resets extended infos

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        For Each Form As Windows.Forms.Form In Me.MdiChildren
            Form.Close()
            Form.Dispose()
        Next
        If Campaign.fs IsNot Nothing Then
            Campaign.fs.Close()
        End If
        Campaign = New Trinity.cKampanj(TrinitySettings.ErrorChecking)
        TrinitySettings.MainObject = Campaign
        Trinity.Helper.MainObject = Campaign

        Trinity.Helper.WriteToLogFile("Set Campaign")
        Campaign.Area = TrinitySettings.DefaultArea
        Campaign.AreaLog = TrinitySettings.DefaultAreaLog


        Trinity.Helper.WriteToLogFile("Create channels")
        Campaign.CreateChannels()

        Trinity.Helper.WriteToLogFile("Set Basic Targets")
        Campaign.AllAdults = TrinitySettings.AllAdults
        Campaign.MainTarget.TargetName = Campaign.AllAdults
        Campaign.SecondaryTarget.TargetName = Campaign.AllAdults

        LongName.Clear()
        LongBT.Clear()
        For i = 1 To Campaign.Channels.Count
            LongName.Add(Campaign.Channels(i).Shortname, Campaign.Channels(i).ChannelName)
        Next
        For Each chan As Trinity.cChannel In Campaign.Channels
            For Each BT As Trinity.cBookingType In chan.BookingTypes
                If Not LongBT.ContainsKey(BT.Shortname) Then
                    LongBT.Add(BT.Shortname, BT.Name)
                End If
            Next
        Next

        Trinity.Helper.WriteToLogFile("Read Pricelists")
        For Each TmpChan In Campaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                TmpBT.ReadPricelist()
            Next
        Next

        If TrinitySettings.DefaultContractPath <> "" Then

            Trinity.Helper.WriteToLogFile("Read default contract")
            Try
                Campaign.LoadDefaultContract()
            Catch ex As Exception
                Windows.Forms.MessageBox.Show("Error while loading default contract: " & TrinitySettings.DefaultContractPath & vbCrLf & vbCrLf & "Message:" & vbCrLf & ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            End Try
            Campaign.Contract = Nothing
        End If

        Reset()
        Campaign.AdToox = New Trinity.cAdtoox(TrinitySettings.AdtooxUsername, TrinitySettings.AdtooxPassword)
        cmdLinkCampaign.BackColor = SystemColors.Control

        Me.Cursor = Windows.Forms.Cursors.Default
        Saved = True
        JustStarted = False
    End Sub

    Sub Reset()
        'disables all buttons except setup
        Me.cmdAllocate.Enabled = False
        Me.cmdMonitor.Enabled = False
        Me.cmdSpots.Enabled = False
        Me.cmdLab.Enabled = False
        Me.cmdPivot.Enabled = False
        Me.cmdInfo.Enabled = False
        Me.cmdBudget.Enabled = False
        Me.cmdDelivery.Enabled = False
        Me.cmdNotes.Enabled = False
        Me.cmdBooking.Enabled = False

        AutoSaveName = ""

        'update all labels and information containers
        UpdateInfo()
    End Sub

    Sub PrintMaterialSpecification(ByVal sender As Object, ByVal e As EventArgs)
        frmMtrlSpec.ShowDialog()
    End Sub

    Private Sub EditPricelistToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditPricelistToolStripMenuItem.Click

        Dim frmPrice As New frmPricelist(Campaign.Channels)


        frmPrice.ShowDialog()

    End Sub

    Private Sub RereadSpotsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RereadSpotsToolStripMenuItem.Click
        Saved = False
        frmSpots.Close()
        frmMonitor.Close()
        Campaign.UpdatedTo = 0
        Campaign.ActualSpots = New Trinity.cActualSpots(Campaign)
        Campaign.GetNewActualSpots(True)
        lblStatus.Text = ""
        Campaign.CreateAdedgeSpots()
        Campaign.CalculateSpots(UseFilters:=False)
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                TmpBT.InvalidateActualNetValue()
                Dim Dummy As Single = TmpBT.ActualNetValue
            Next
        Next
    End Sub

    Protected Overloads Sub Update()
        MyBase.Update()
        UpdateInfo()
        For Each TmpForm As System.Windows.Forms.Form In Me.MdiChildren
            TmpForm.Update()
        Next
    End Sub

    Private Sub ReadScheduleFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReadScheduleFileToolStripMenuItem.Click
        Dim Errors As Integer = 0
        If DBReader.isLocal Then
            MsgBox("You are using a Local database and all changes you make will be lost when you connect back to the network.", MsgBoxStyle.Information, "FYI")
        End If
        dlgDialog.FileName = "*.mtf"
        dlgDialog.Filter = "Meta files|*.mtf"
        If dlgDialog.ShowDialog <> Windows.Forms.DialogResult.OK Then Exit Sub
        Using sr As IO.StreamReader = New IO.StreamReader(dlgDialog.FileName)
            Dim line As String
            ' Read and display the lines from the file until the end 
            ' of the file is reached.
            'Dim trans As Odbc.OdbcTransaction = DBConn.BeginTransaction()
            'Dim c As Odbc.OdbcCommand = DBConn.CreateCommand
            'c.Transaction = trans
            Do
                line = sr.ReadLine()
                If Not line Is Nothing Then line.Replace(".", ",")
                'c.CommandText = line

                If Not line Is Nothing Then
                    Try
                        'c.ExecuteNonQuery()
                        DBReader.QUERY(line)
                    Catch ex As Exception
                        Errors += 1
                        Trinity.Helper.WriteToLogFile("EXCEPTION: " & ex.Message)
                        Trinity.Helper.WriteToLogFile("Error on line: " & line)
                    End Try
                End If
            Loop Until line Is Nothing
            'trans.Commit()
            sr.Close()
        End Using
        If Errors = 0 Then
            Windows.Forms.MessageBox.Show("The file was succesfully imported!", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        Else
            Windows.Forms.MessageBox.Show("The file was imported with " & Errors & " errors.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub DefineChannelsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefineChannelsToolStripMenuItem.Click

        frmDefineChannels.ShowDialog()
    End Sub

    Private Sub tmrAutosave_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrAutosave.Tick
        'the timer controls the autosave function
        Dim XMLDoc As New Xml.XmlDocument

        If TrinitySettings.Autosave Then
            Me.Cursor = Windows.Forms.Cursors.WaitCursor
            If AutoSaveName = "" Then
                AutoSaveName = Trinity.Helper.Pathify(My.Application.Info.DirectoryPath) & "autosave.cmp"
            End If

            Try
                XMLDoc.LoadXml(Campaign.SaveCampaign("", True, False, False, True))
                XMLDoc.Save(AutoSaveName)
                Trinity.Helper.WriteToLogFile("Autosaved to " & AutoSaveName)
            Catch
                Trinity.Helper.WriteToLogFile("Could not autosave to " & AutoSaveName)
            End Try
            Me.Cursor = Windows.Forms.Cursors.Default
        End If
    End Sub

    Private Sub EvaluateSpecificsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EvaluateSpecificsToolStripMenuItem.Click
        frmEvaluateSpecifics.MdiParent = Me
        frmEvaluateSpecifics.Show()
    End Sub

    Private Sub CreateInsertionsInMarathonToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateInsertionsInMarathonToolStripMenuItem.Click
        frmMarathon.ShowDialog()
        frmMarathon.Dispose()
    End Sub

    Private Sub MarathonToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles MarathonToolStripMenuItem.DropDownOpening
        If Campaign.MarathonPlanNr <> 0 Then
            If Campaign.MarathonCTC = 0 Then
                CreateInsertionsInMarathonToolStripMenuItem.Enabled = True
                MakeOrdersDefinitiveInMarathonToolStripMenuItem.Enabled = False
                CreateOrdersInMarathonToolStripMenuItem.Enabled = False
                AddOrdersInMarathonToolStripMenuItem.Enabled = True
            Else
                CreateInsertionsInMarathonToolStripMenuItem.Enabled = False
                MakeOrdersDefinitiveInMarathonToolStripMenuItem.Enabled = True
                CreateOrdersInMarathonToolStripMenuItem.Enabled = False
                AddOrdersInMarathonToolStripMenuItem.Enabled = True
            End If
        Else
            CreateInsertionsInMarathonToolStripMenuItem.Enabled = False
            MakeOrdersDefinitiveInMarathonToolStripMenuItem.Enabled = False
            CreateOrdersInMarathonToolStripMenuItem.Enabled = True
            AddOrdersInMarathonToolStripMenuItem.Enabled = True
        End If
        CreateOrdersInMarathonToolStripMenuItem.Enabled = True
    End Sub


    Private Sub MakeOrdersDefinitiveInMarathonToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MakeOrdersDefinitiveInMarathonToolStripMenuItem.Click
        Dim _marathon As New Marathon(TrinitySettings.MarathonCommand)

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        Dim info As DataTable = DBReader.getAllFromProducts(Campaign.ProductID) '

        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                If TmpBT.BookIt Then
                    Try
                        _marathon.MakeOrderDefinitive(info.Rows(0)!MarathonCompany, TmpBT.OrderNumber)
                    Catch ex As Exception
                        Windows.Forms.MessageBox.Show("There was an error while making " & TmpChan.ChannelName & " definitive." & vbCrLf & vbCrLf & "Marathon response: " & ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                    End Try
                End If
            Next
        Next
        Try
            _marathon.MakeOrderDefinitive(info.Rows(0)!MarathonCompany, Campaign.MarathonOtherOrder)
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("There was an error while making the general order definitive." & vbCrLf & vbCrLf & "Marathon response: " & ex.Message, "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        End Try
        Me.Cursor = Windows.Forms.Cursors.Default
        MsgBox("Orders where made definitive.", vbInformation, "T R I N I T Y")
    End Sub

    Private Sub txtName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtName.TextChanged
        Saved = False
    End Sub

    Private Sub cmbFF_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFF.SelectedIndexChanged
        Campaign.FrequencyFocus = cmbFF.SelectedIndex
    End Sub

    Private Sub cmdPivot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPivot.Click

        'frmPivottable.MdiParent = Me
        'frmPivottable.Show()
        frmPivotTableDevEx.MdiParent = Me
        frmPivotTableDevEx.Show()
    End Sub

    Private Sub DownloadLatestVersionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadLatestVersionToolStripMenuItem.Click
        If Windows.Forms.MessageBox.Show("This will restart Trinity.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OKCancel, Windows.Forms.MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
            Try
                Kill(Trinity.Helper.Pathify(My.Application.Info.DirectoryPath) & "version.txt")
            Catch

            End Try
            Try
                Kill(Trinity.Helper.Pathify(My.Application.Info.DirectoryPath) & "version.xml")
            Catch

            End Try
            Shell(Trinity.Helper.Pathify(My.Application.Info.DirectoryPath) & "launchtrinity.exe")
            End
        End If
    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click
        cmdPivot.Enabled = True
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub mnuStatus_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuStatus.DropDownOpening
        Select Case Campaign.Status
            Case "Planning"
                PlanningToolStripMenuItem.Checked = True
            Case "Running"
                RunningToolStripMenuItem.Checked = True
            Case "Finished"
                FinishedToolStripMenuItem.Checked = True
            Case "Cancelled"
                CancelledToolStripMenuItem.Checked = True
        End Select
    End Sub

    Private Sub PlanningToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlanningToolStripMenuItem.Click
        Saved = False
        PlanningToolStripMenuItem.Checked = True
        RunningToolStripMenuItem.Checked = False
        FinishedToolStripMenuItem.Checked = False
        CancelledToolStripMenuItem.Checked = False
        Campaign.Status = "Planning"
    End Sub

    Private Sub RunningToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunningToolStripMenuItem.Click
        Saved = False
        PlanningToolStripMenuItem.Checked = False
        RunningToolStripMenuItem.Checked = True
        FinishedToolStripMenuItem.Checked = False
        CancelledToolStripMenuItem.Checked = False
        Campaign.Status = "Running"
    End Sub

    Private Sub FinishedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinishedToolStripMenuItem.Click
        Saved = False
        PlanningToolStripMenuItem.Checked = False
        RunningToolStripMenuItem.Checked = False
        FinishedToolStripMenuItem.Checked = True
        CancelledToolStripMenuItem.Checked = False
        Campaign.Status = "Finished"
    End Sub

    Private Sub CancelledToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelledToolStripMenuItem.Click
        Saved = False
        PlanningToolStripMenuItem.Checked = False
        RunningToolStripMenuItem.Checked = False
        FinishedToolStripMenuItem.Checked = False
        CancelledToolStripMenuItem.Checked = True
        Campaign.Status = "Cancelled"
    End Sub

    Private Sub cmdHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHistory.Click

    End Sub

    Private Sub ResetMarathonSettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetMarathonSettingsToolStripMenuItem.Click
        Campaign.MarathonOtherOrder = 0
        Campaign.MarathonPlanNr = 0
        Campaign.MarathonCTC = 0
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                TmpBT.OrderNumber = ""
                TmpBT.MarathonNetBudget = 0
            Next
        Next
    End Sub

    Private Sub exitWindow(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Dim rply As Windows.Forms.DialogResult

        If Saved = False Then
            rply = Windows.Forms.MessageBox.Show("Do you want to save before you exit?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question)
        Else
            rply = Windows.Forms.DialogResult.No
        End If
        If rply = Windows.Forms.DialogResult.Cancel Then
            e.Cancel = True
            Exit Sub
        ElseIf rply = Windows.Forms.DialogResult.Yes Then
            ' Save the campaign
            Trinity.Helper.WriteToLogFile("Start of frmMain/cmdSave_Click")

            If TrinitySettings.SaveCampaignsAsFiles Then
                SaveToFile()
            Else
                SaveToDB()
            End If

            ''if the file name is empty (not saved before) it opens a save file dialog and then saves the campaign
            'If Campaign.Filename = "" Then
            '    Dim dlgDialog As New Windows.Forms.SaveFileDialog
            '    dlgDialog.Title = "Save campaign as..."
            '    dlgDialog.FileName = "*.cmp"
            '    dlgDialog.DefaultExt = "*.cmp"
            '    dlgDialog.Filter = "Trinity campaigns|*.cmp"
            '    dlgDialog.InitialDirectory = TrinitySettings.CampaignFiles
            '    If dlgDialog.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
            '    Me.Cursor = Windows.Forms.Cursors.WaitCursor
            '    Campaign.SaveCampaign(dlgDialog.FileName)
            '    DBReader.closeConnection()
            'Else
            '    'if the filename is set the campagn is saved
            '    Me.Cursor = Windows.Forms.Cursors.WaitCursor
            '    Campaign.SaveCampaign(Campaign.Filename)
            '    Me.Cursor = Windows.Forms.Cursors.Default
            '    DBReader.closeConnection()
            'End If
        End If
        DBReader.closeConnection()
        tmrDetectProblems.Enabled = False
        Campaign.UnRegisterAllProblemDetectors()

    End Sub


    Private Sub cmdAddClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddClient.Click
        Saved = False
    End Sub

    Public Sub showRecentCampaignsMenu()
        Dim tmparray(9) As String
        tmparray = TrinitySettings.getLastCampaigns

        mnuRecent.Enabled = False
        For i As Integer = 0 To 9
            If Not tmparray(i) = "" Then
                mnuRecent.Enabled = True
                mnuRecent.DropDownItems("Recent" & i + 1 & "ToolStripMenuItem").Tag = tmparray(i)
                mnuRecent.DropDownItems("Recent" & i + 1 & "ToolStripMenuItem").Text = tmparray(i).Substring(tmparray(i).LastIndexOf("\") + 1)
                'pnlStartHelp.Controls("lblRecent" & i + 1).Text = RecentFilesToolStripMenuItem.DropDownItems("Recent" & i + 1 & "ToolStripMenuItem").Text
                'pnlStartHelp.Controls("lblRecent" & i + 1).Tag = RecentFilesToolStripMenuItem.DropDownItems("Recent" & i + 1 & "ToolStripMenuItem").Tag
            Else
                mnuRecent.DropDownItems("Recent" & i + 1 & "ToolStripMenuItem").Visible = False
                'pnlStartHelp.Controls("lblRecent" & i + 1).Visible = False
            End If
        Next

    End Sub

    Private Sub lblRecent1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblRecent1.Click
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType

        'disables autosave function
        tmrAutosave.Enabled = False

        'hides the recent panel
        pnlStartHelp.Visible = False

        'Changes the cursor to a "loading" symbol
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        'closes all open windows
        Dim j As Integer
        For j = My.Application.OpenForms.Count - 1 To 0 Step -1
            If Not My.Application.OpenForms(j).Name = "frmMain" Then
                My.Application.OpenForms(j).Dispose()
            End If
        Next

        'creates a new Campaign
        Campaign = New Trinity.cKampanj(TrinitySettings.ErrorChecking)
        TrinitySettings.MainObject = Campaign
        Trinity.Helper.MainObject = Campaign


        'resets all buttons and information containers, görs inte detta nedan? knappar sets och update info körs???
        Reset()

        'if the setup dialog for a new campaign is open it will be closed
        If Not frmSetup Is Nothing Then
            frmSetup.Dispose()
        End If

        Campaign.LoadCampaign(lblRecent1.Tag)
        Windows.Forms.Application.DoEvents()

        Campaign.GetNewActualSpots()
        lblStatus.Text = ""

        Campaign.CreateAdedgeSpots()

        Campaign.CalculateSpots()

        'updates the "note" and the window name
        frmMain_Activated(New Object, New EventArgs)


        'Enables all buttons
        Me.cmdAllocate.Enabled = True
        Me.cmdMonitor.Enabled = True
        Me.cmdSpots.Enabled = True
        Me.cmdLab.Enabled = True
        Me.cmdPivot.Enabled = True
        Me.cmdInfo.Enabled = True
        Me.cmdBudget.Enabled = True
        Me.cmdDelivery.Enabled = True
        Me.cmdNotes.Enabled = True
        'resets the cursor to normal
        Me.Cursor = Windows.Forms.Cursors.Default
        For Each TmpChan In Campaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                If (TmpBT.IsSpecific Or TmpBT.IsPremium) AndAlso TmpBT.BookIt Then
                    Me.cmdBooking.Enabled = True
                End If
            Next
        Next

        'updates all labels and combo information
        UpdateInfo()

        'enables autosave function
        tmrAutosave.Enabled = True
        Saved = True
        pnlStartHelp.Visible = False
    End Sub

    Private Sub lblRecent2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblRecent2.Click
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType

        'disables autosave function
        tmrAutosave.Enabled = False

        'hides the recent panel
        pnlStartHelp.Visible = False

        'Changes the cursor to a "loading" symbol
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        'closes all open windows
        Dim j As Integer
        For j = My.Application.OpenForms.Count - 1 To 0 Step -1
            If Not My.Application.OpenForms(j).Name = "frmMain" Then
                My.Application.OpenForms(j).Dispose()
            End If
        Next

        'creates a new Campaign
        Campaign = New Trinity.cKampanj(TrinitySettings.ErrorChecking)
        TrinitySettings.MainObject = Campaign
        Trinity.Helper.MainObject = Campaign


        'resets all buttons and information containers, görs inte detta nedan? knappar sets och update info körs???
        Reset()

        'if the setup dialog for a new campaign is open it will be closed
        If Not frmSetup Is Nothing Then
            frmSetup.Dispose()
        End If

        Campaign.LoadCampaign(lblRecent2.Tag)
        Windows.Forms.Application.DoEvents()

        Campaign.GetNewActualSpots()
        lblStatus.Text = ""

        Campaign.CreateAdedgeSpots()

        Campaign.CalculateSpots()

        'updates the "note" and the window name
        frmMain_Activated(New Object, New EventArgs)


        'Enables all buttons
        Me.cmdAllocate.Enabled = True
        Me.cmdMonitor.Enabled = True
        Me.cmdSpots.Enabled = True
        Me.cmdLab.Enabled = True
        Me.cmdPivot.Enabled = True
        Me.cmdInfo.Enabled = True
        Me.cmdBudget.Enabled = True
        Me.cmdDelivery.Enabled = True
        Me.cmdNotes.Enabled = True
        'resets the cursor to normal
        Me.Cursor = Windows.Forms.Cursors.Default
        For Each TmpChan In Campaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                If (TmpBT.IsSpecific OrElse TmpBT.IsPremium) AndAlso TmpBT.BookIt Then
                    Me.cmdBooking.Enabled = True
                End If
            Next
        Next

        'updates all labels and combo information
        UpdateInfo()

        'enables autosave function
        tmrAutosave.Enabled = True
        Saved = True
        pnlStartHelp.Visible = False
    End Sub

    Private Sub lblRecent3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblRecent3.Click
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType

        'disables autosave function
        tmrAutosave.Enabled = False

        'hides the recent panel
        pnlStartHelp.Visible = False

        'Changes the cursor to a "loading" symbol
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        'closes all open windows
        Dim j As Integer
        For j = My.Application.OpenForms.Count - 1 To 0 Step -1
            If Not My.Application.OpenForms(j).Name = "frmMain" Then
                My.Application.OpenForms(j).Dispose()
            End If
        Next

        'creates a new Campaign
        Campaign = New Trinity.cKampanj(TrinitySettings.ErrorChecking)
        TrinitySettings.MainObject = Campaign
        Trinity.Helper.MainObject = Campaign


        'resets all buttons and information containers, görs inte detta nedan? knappar sets och update info körs???
        Reset()

        'if the setup dialog for a new campaign is open it will be closed
        If Not frmSetup Is Nothing Then
            frmSetup.Dispose()
        End If

        Campaign.LoadCampaign(lblRecent3.Tag)
        Windows.Forms.Application.DoEvents()

        Campaign.GetNewActualSpots()
        lblStatus.Text = ""

        Campaign.CreateAdedgeSpots()

        Campaign.CalculateSpots()

        'updates the "note" and the window name
        frmMain_Activated(New Object, New EventArgs)


        'Enables all buttons
        Me.cmdAllocate.Enabled = True
        Me.cmdMonitor.Enabled = True
        Me.cmdSpots.Enabled = True
        Me.cmdLab.Enabled = True
        Me.cmdPivot.Enabled = True
        Me.cmdInfo.Enabled = True
        Me.cmdBudget.Enabled = True
        Me.cmdDelivery.Enabled = True
        Me.cmdNotes.Enabled = True
        'resets the cursor to normal
        Me.Cursor = Windows.Forms.Cursors.Default
        For Each TmpChan In Campaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                If (TmpBT.IsSpecific OrElse TmpBT.IsPremium) AndAlso TmpBT.BookIt Then
                    Me.cmdBooking.Enabled = True
                End If
            Next
        Next

        'updates all labels and combo information
        UpdateInfo()

        'enables autosave function
        tmrAutosave.Enabled = True
        Saved = True
        pnlStartHelp.Visible = False
    End Sub

    Private Sub lblRecent4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblRecent4.Click
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType

        'disables autosave function
        tmrAutosave.Enabled = False

        'hides the recent panel
        pnlStartHelp.Visible = False

        'Changes the cursor to a "loading" symbol
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        'closes all open windows
        Dim j As Integer
        For j = My.Application.OpenForms.Count - 1 To 0 Step -1
            If Not My.Application.OpenForms(j).Name = "frmMain" Then
                My.Application.OpenForms(j).Dispose()
            End If
        Next

        'creates a new Campaign
        Campaign = New Trinity.cKampanj(TrinitySettings.ErrorChecking)
        TrinitySettings.MainObject = Campaign
        Trinity.Helper.MainObject = Campaign


        'resets all buttons and information containers, görs inte detta nedan? knappar sets och update info körs???
        Reset()

        'if the setup dialog for a new campaign is open it will be closed
        If Not frmSetup Is Nothing Then
            frmSetup.Dispose()
        End If

        Campaign.LoadCampaign(lblRecent4.Tag)
        Windows.Forms.Application.DoEvents()

        Campaign.GetNewActualSpots()
        lblStatus.Text = ""

        Campaign.CreateAdedgeSpots()

        Campaign.CalculateSpots()

        'updates the "note" and the window name
        frmMain_Activated(New Object, New EventArgs)


        'Enables all buttons
        Me.cmdAllocate.Enabled = True
        Me.cmdMonitor.Enabled = True
        Me.cmdSpots.Enabled = True
        Me.cmdLab.Enabled = True
        Me.cmdPivot.Enabled = True
        Me.cmdInfo.Enabled = True
        Me.cmdBudget.Enabled = True
        Me.cmdDelivery.Enabled = True
        Me.cmdNotes.Enabled = True
        'resets the cursor to normal
        Me.Cursor = Windows.Forms.Cursors.Default
        For Each TmpChan In Campaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                If (TmpBT.IsSpecific OrElse TmpBT.IsPremium) AndAlso TmpBT.BookIt Then
                    Me.cmdBooking.Enabled = True
                End If
            Next
        Next

        'updates all labels and combo information
        UpdateInfo()

        'enables autosave function
        tmrAutosave.Enabled = True
        Saved = True
        pnlStartHelp.Visible = False
    End Sub

    Private Sub lblRecent5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblRecent5.Click
        Dim TmpChan As Trinity.cChannel
        Dim TmpBT As Trinity.cBookingType

        'disables autosave function
        tmrAutosave.Enabled = False

        'hides the recent panel
        pnlStartHelp.Visible = False

        'Changes the cursor to a "loading" symbol
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        'closes all open windows
        Dim j As Integer
        For j = My.Application.OpenForms.Count - 1 To 0 Step -1
            If Not My.Application.OpenForms(j).Name = "frmMain" Then
                My.Application.OpenForms(j).Dispose()
            End If
        Next

        'creates a new Campaign
        Campaign = New Trinity.cKampanj(TrinitySettings.ErrorChecking)
        TrinitySettings.MainObject = Campaign
        Trinity.Helper.MainObject = Campaign


        'resets all buttons and information containers, görs inte detta nedan? knappar sets och update info körs???
        Reset()

        'if the setup dialog for a new campaign is open it will be closed
        If Not frmSetup Is Nothing Then
            frmSetup.Dispose()
        End If

        Campaign.LoadCampaign(lblRecent5.Tag)
        Windows.Forms.Application.DoEvents()

        Campaign.GetNewActualSpots()
        lblStatus.Text = ""

        Campaign.CreateAdedgeSpots()

        Campaign.CalculateSpots()

        'updates the "note" and the window name
        frmMain_Activated(New Object, New EventArgs)


        'Enables all buttons
        Me.cmdAllocate.Enabled = True
        Me.cmdMonitor.Enabled = True
        Me.cmdSpots.Enabled = True
        Me.cmdLab.Enabled = True
        Me.cmdPivot.Enabled = True
        Me.cmdInfo.Enabled = True
        Me.cmdBudget.Enabled = True
        Me.cmdDelivery.Enabled = True
        Me.cmdNotes.Enabled = True
        'resets the cursor to normal
        Me.Cursor = Windows.Forms.Cursors.Default
        For Each TmpChan In Campaign.Channels
            For Each TmpBT In TmpChan.BookingTypes
                If (TmpBT.IsSpecific OrElse TmpBT.IsPremium) AndAlso TmpBT.BookIt Then
                    Me.cmdBooking.Enabled = True
                End If
            Next
        Next

        'updates all labels and combo information
        UpdateInfo()

        'enables autosave function
        tmrAutosave.Enabled = True
        Saved = True
        pnlStartHelp.Visible = False
    End Sub


    Private Sub lblRecent_enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblRecent5.MouseEnter, lblRecent4.MouseEnter, lblRecent3.MouseEnter, lblRecent2.MouseEnter, lblRecent1.MouseEnter, lblManual.MouseEnter
        Me.Cursor = Windows.Forms.Cursors.Hand
    End Sub

    Private Sub lblRecent_leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblRecent5.MouseLeave, lblRecent4.MouseLeave, lblRecent3.MouseLeave, lblRecent2.MouseLeave, lblRecent1.MouseLeave, lblManual.MouseLeave
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub RecentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Recent1ToolStripMenuItem.Click, lblRecent1.Click, Recent2ToolStripMenuItem.Click, lblRecent2.Click, Recent3ToolStripMenuItem.Click, lblRecent3.Click, Recent4ToolStripMenuItem.Click, lblRecent4.Click, Recent5ToolStripMenuItem.Click, lblRecent5.Click, Recent6ToolStripMenuItem.Click, Recent7ToolStripMenuItem.Click, Recent8ToolStripMenuItem.Click, Recent9ToolStripMenuItem.Click, Recent10ToolStripMenuItem.Click
        Campaign.ExtendedInfos.RemoveAll()
        OpenCampaign(sender.tag)
        pnlStartHelp.Visible = False
    End Sub

    Private Sub DefaultToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefaultToolStripMenuItem.Click
        If MsgBox("Do you wish to reload all default channels?", MsgBoxStyle.YesNo, "Reload Channels") = MsgBoxResult.No Then
            Exit Sub
        End If

        Dim oldCI As System.Globalization.CultureInfo = Nothing

        Try
            oldCI = System.Threading.Thread.CurrentThread.CurrentCulture
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")

            'reloads all default channels
            Saved = False
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            Campaign.ReloadDeletedChannels()

            LongName.Clear()
            For i As Integer = 1 To Campaign.Channels.Count
                LongName.Add(Campaign.Channels(i).Shortname, Campaign.Channels(i).ChannelName)
            Next

            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
            Me.Cursor = Windows.Forms.Cursors.Default

        Catch
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
            Me.Cursor = Windows.Forms.Cursors.Default
            MsgBox("Error reading channel set", MsgBoxStyle.Critical, "Error")

        End Try

    End Sub

    Private Sub DeleteUnusedChannelsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteUnusedChannelsToolStripMenuItem.Click
        'deletes as bookingtypes where bookit is false and all channels who has no bookingtype

        Campaign.DeleteUnusedChannels()

        LongName.Clear()
        For i As Integer = 1 To Campaign.Channels.Count
            LongName.Add(Campaign.Channels(i).Shortname, Campaign.Channels(i).ChannelName)
        Next

    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'adds the different set of channels
        Dim ini As New Trinity.clsIni
        ini.Filename = TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork) & "/" & Campaign.Area & "/" & "Area.ini"
        Dim i As Integer = 1
        Dim s As String = ini.Text("channelfiles", "channelfile" & 1)
        If MatrixVersion <> MatrixVersionEnum.NotInstalled Then
            mnuMatrix.Visible = True
        End If
        While s <> ""
            Select Case i
                Case Is = 1
                    channelset1.Text = "Load " & s.Substring(0, s.IndexOf("."))
                    channelset1.Tag = s.Substring(0, s.IndexOf("."))
                    channelset1.Visible = True
                Case Is = 2
                    channelset2.Text = "Load " & s.Substring(0, s.IndexOf("."))
                    channelset2.Tag = s.Substring(0, s.IndexOf("."))
                    channelset2.Visible = True
                Case Is = 3
                    channelset3.Text = "Load " & s.Substring(0, s.IndexOf("."))
                    channelset3.Tag = s.Substring(0, s.IndexOf("."))
                    channelset3.Visible = True
                Case Is = 4
                    channelset4.Text = "Load " & s.Substring(0, s.IndexOf("."))
                    channelset4.Tag = s.Substring(0, s.IndexOf("."))
                    channelset4.Visible = True
            End Select
            i += 1
            s = ini.Text("channelfiles", "channelfile" & i)
        End While
        If Not TrinitySettings.SaveCampaignsAsFiles Then
            cmdLinkCampaign.Visible = False
        End If

        Dim xmlDoc As New XmlDocument

        If IO.File.Exists(My.Application.Info.DirectoryPath & "\versions.xml") Then
            Try
                xmlDoc.Load(My.Application.Info.DirectoryPath & "\versions.xml")
                VersionToolStripMenuItem.Text = VersionToolStripMenuItem.Text & " " & xmlDoc.ChildNodes(1).ChildNodes(1).FirstChild.Attributes("Version").Value
                xmlDoc = Nothing
            Catch ex As Exception
                Debug.Print("Could not read versions.xml correctly")
            Finally
                xmlDoc = Nothing
            End Try
        Else
            Debug.Print("versions.xml not found")
            xmlDoc = Nothing
        End If
        Dim catalog As New AggregateCatalog()
        catalog.Catalogs.Add(New AssemblyCatalog(Assembly.GetExecutingAssembly))

        'Below comment were already commented
        'catalog.Catalogs.Add(New AssemblyCatalog(GetType(IPlugin).Assembly))

        ' /JOOS
        '   IO.Directory.Exists does not find the current directory and needs a specified path.
        '   Changed: "IO.Directory.Exists("./Plugins/")" into:
        '   "IO.Directory.Exists(IO.Path.Combine(My.Application.Info.DirectoryPath, "Plugins"))"
        ' 

        If IO.Directory.Exists(IO.Path.Combine(My.Application.Info.DirectoryPath, "Plugins")) Then
            catalog.Catalogs.Add(New DirectoryCatalog("./Plugins/"))
        End If
        'catalog.Catalogs.Add(New TypeCatalog(GetType(IPlugin)))
        Dim container As New CompositionContainer(catalog)
        container.SatisfyImportsOnce(Me)

        If IO.Directory.Exists(IO.Path.Combine(My.Application.Info.DirectoryPath, "Plugins")) AndAlso Plugins.Count <> My.Computer.FileSystem.GetDirectoryInfo(IO.Path.Combine(My.Application.Info.DirectoryPath, "Plugins")).GetFiles("*.dll").Count Then
            Shell(IO.Path.Combine(My.Application.Info.DirectoryPath, "trinity.exe forceplugins"), AppWinStyle.NormalFocus)
            End
        End If

        For Each _plugin As IPlugin In Plugins
            If _plugin.Menu.AddToMenu <> "" Then
                If msMain.Items.ContainsKey(_plugin.Menu.AddToMenu) Then
                    With DirectCast(DirectCast(msMain.Items(_plugin.Menu.AddToMenu), Windows.Forms.ToolStripMenuItem).DropDownItems.Add(_plugin.Menu.Caption), Windows.Forms.ToolStripMenuItem)
                        For Each _item As IPlugin.PluginMenuItem In _plugin.Menu.Items
                            .DropDownItems.Add(_item.Text, _item.Image, _item.OnClickFunction)
                        Next
                    End With
                End If
            Else
                With DirectCast(msMain.Items.Add(_plugin.Menu.Caption), Windows.Forms.ToolStripMenuItem)
                    For Each _item As IPlugin.PluginMenuItem In _plugin.Menu.Items
                        .DropDownItems.Add(_item.Text, _item.Image, _item.OnClickFunction)
                    Next
                End With
            End If
            AddHandler _plugin.SaveDataAvailale, AddressOf PluginSaveDataAvailable
        Next

        If DBReader.GetType Is GetType(Trinity.cDBReaderSQL) Then
            AddHandler DirectCast(DBReader, Trinity.cDBReaderSQL).IncomingMessage, AddressOf IncomingMessage
            DirectCast(DBReader, Trinity.cDBReaderSQL).CheckForNewMessages()
        End If
    End Sub

    <ImportMany(GetType(IPlugin))>
    Property Plugins() As IEnumerable(Of IPlugin)

    Sub PluginSaveDataAvailable(sender As Object, e As EventArgs)
        With DirectCast(sender, IPlugin)
            If Not Campaign.PluginSaveData.ContainsKey(.PluginName) Then
                Campaign.PluginSaveData.Add(.PluginName, .GetSaveData)
            Else
                Campaign.PluginSaveData(.PluginName) = .GetSaveData
            End If
        End With
    End Sub

    Sub IncomingMessage(sender As Object, e As Trinity.cDBReaderSQL.MessageEventArgs)

        QueueMessage(e)
        Me.Invoke(Sub()
                      ShowNextMessage()
                  End Sub)
    End Sub

    Dim _messageQueue As New Queue(Of Trinity.cDBReaderSQL.MessageEventArgs)
    Sub QueueMessage(e As Trinity.cDBReaderSQL.MessageEventArgs)
        _messageQueue.Enqueue(e)
    End Sub

    Sub ShowNextMessage()
        If pnlMessage.Visible OrElse _messageQueue.Count = 0 Then Exit Sub
        pnlMessage.Height = 0
        pnlMessage.Top = pnlMenu.Top - 1
        pnlMessage.BringToFront()
        pnlMessage.Visible = True
        pnlMessage.Tag = Nothing

        Dim _msg As Trinity.cDBReaderSQL.MessageEventArgs = _messageQueue.Dequeue

        lblMsgHeadline.Text = _msg.Headline
        lblMsgMessage.Text = _msg.Ingress
        lblMsgReadMore.Tag = _msg.Body
        lblMsgReadMore.Visible = (_msg.Body <> "")

        Dim _thread As New Threading.Thread(Sub()
                                                While pnlMessage.Height < 100 AndAlso pnlMessage.Tag Is Nothing
                                                    Me.Invoke(Sub()
                                                                  pnlMessage.Height += 1
                                                                  pnlMessage.Top -= 1
                                                                  My.Application.DoEvents()
                                                              End Sub)
                                                    Threading.Thread.Sleep(5)
                                                End While
                                            End Sub)
        _thread.Start()
    End Sub

    Private Sub lblMsgReadMore_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblMsgReadMore.LinkClicked
        frmShowMessage.txtMessage.Text = lblMsgReadMore.Tag
        frmShowMessage.Text = lblMsgHeadline.Text
        frmShowMessage.ShowDialog()
    End Sub

    Private Sub lblHideMessage_Click(sender As System.Object, e As System.EventArgs) Handles lblHideMessage.Click
        pnlMessage.Tag = "ABORT"
        Dim _thread As New Threading.Thread(Sub()
                                                While pnlMessage.Height > 0
                                                    Me.Invoke(Sub()
                                                                  pnlMessage.Height -= 1
                                                                  pnlMessage.Top += 1
                                                                  My.Application.DoEvents()
                                                              End Sub)
                                                    Threading.Thread.Sleep(5)
                                                End While
                                                Me.Invoke(Sub()
                                                              pnlMessage.Visible = False
                                                              ShowNextMessage()
                                                          End Sub)

                                            End Sub)
        _thread.Start()
    End Sub

    Private Sub channelset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles channelset1.Click, channelset2.Click, channelset3.Click, channelset4.Click
        If MsgBox("Do you wish to load/reaload all channels in the " & sender.tag & " group?", MsgBoxStyle.YesNo, "Reload Channels") = MsgBoxResult.No Then
            Exit Sub
        End If

        Dim oldCI As System.Globalization.CultureInfo = Nothing

        Try
            oldCI = System.Threading.Thread.CurrentThread.CurrentCulture
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("sv-SE")

            Saved = False
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            Dim chanName As String = Campaign.Channels(1).ChannelName
            Dim btName As String = Campaign.Channels(1).BookingTypes(1).Name

            Dim strMatch As String = sender.tag & ".xml"
            'reloads the channels and Bookingtypes present
            For Each TmpChan As Trinity.cChannel In Campaign.Channels
                If TmpChan.fileName = strMatch Then
                    Dim tmpChanNew As New Trinity.cChannel(Campaign, Campaign.Channels.RawCollection)
                    tmpChanNew.fileName = strMatch
                    tmpChanNew.ChannelName = TmpChan.ChannelName
                    tmpChanNew.MainObject = Campaign
                    tmpChanNew.ReadDefaultProperties("")
                    tmpChanNew.readDefaultBookingTypes()
                    'add BT that does not exists
                    For Each bt As Trinity.cBookingType In tmpChanNew.BookingTypes
                        If TmpChan.BookingTypes(bt.Name) Is Nothing Then
                            TmpChan.BookingTypes.Add(bt.Name, True)
                            TmpChan.BookingTypes(bt.Name).Shortname = tmpChanNew.BookingTypes(bt.Name).Shortname
                            TmpChan.BookingTypes(bt.Name).IsRBS = tmpChanNew.BookingTypes(bt.Name).IsRBS
                            TmpChan.BookingTypes(bt.Name).IsSpecific = tmpChanNew.BookingTypes(bt.Name).IsSpecific
                            TmpChan.BookingTypes(bt.Name).PrintDayparts = tmpChanNew.BookingTypes(bt.Name).PrintDayparts
                            TmpChan.BookingTypes(bt.Name).PrintBookingCode = tmpChanNew.BookingTypes(bt.Name).PrintBookingCode

                            'if we are in the middle of an campaign we need to add hte weeks to the BT aswell
                            If Campaign.Channels(chanName).BookingTypes(btName).Weeks.Count > 0 Then
                                For Each week As Trinity.cWeek In Campaign.Channels(chanName).BookingTypes(btName).Weeks
                                    TmpChan.BookingTypes(bt.Name).Weeks.Add(week.Name)
                                    TmpChan.BookingTypes(bt.Name).Weeks(week.Name).StartDate = week.StartDate
                                    TmpChan.BookingTypes(bt.Name).Weeks(week.Name).EndDate = week.EndDate
                                    For Each f As Trinity.cFilm In Campaign.Channels(1).BookingTypes(1).Weeks(1).Films
                                        TmpChan.BookingTypes(bt.Name).Weeks(week.Name).Films.Add(f.Name)
                                        TmpChan.BookingTypes(bt.Name).Weeks(week.Name).Films(f.Name).Filmcode = f.Filmcode
                                        TmpChan.BookingTypes(bt.Name).Weeks(week.Name).Films(f.Name).FilmLength = f.FilmLength
                                        TmpChan.BookingTypes(bt.Name).Weeks(week.Name).Films(f.Name).Index = f.Index
                                    Next
                                Next
                            End If

                            TmpChan.BookingTypes(bt.Name).ReadPricelist()
                        End If
                    Next
                End If
            Next


            'reloads all the selected channels


            Dim XMLDoc As Xml.XmlDocument = New Xml.XmlDocument
            Dim XMLChannels As Xml.XmlElement
            Dim XMLTmpNode As Xml.XmlElement
            Dim XMLTmpNode2 As Xml.XmlElement
            Dim XMLBookingTypes As Xml.XmlElement
            Dim TmpChannel As Trinity.cChannel
            Dim TmpBT As Trinity.cBookingType
            Dim tmpStr As String
            Dim found As Boolean = False
            Dim channel As Trinity.cChannel

            XMLDoc.Load(TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork) & Campaign.Area & "\" & sender.tag & ".xml")

            XMLChannels = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("Channels")

            XMLTmpNode = XMLChannels.ChildNodes.Item(0)

            While Not XMLTmpNode Is Nothing
                tmpStr = XMLTmpNode.GetAttribute("Name")
                For Each channel In Campaign.Channels
                    If channel.ChannelName = tmpStr Then
                        found = True
                    End If
                Next
                'if the channel dont exist we add it
                If Not found Then
                    TmpChannel = Campaign.Channels.Add(XMLTmpNode.GetAttribute("Name"), sender.tag & ".xml", Campaign.Area)
                    TmpChannel.fileName = sender.tag + ".xml"
                    XMLBookingTypes = XMLTmpNode.SelectSingleNode("Bookingtypes")
                    If XMLBookingTypes Is Nothing Then
                        XMLBookingTypes = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("Bookingtypes")
                    End If
                    XMLTmpNode2 = XMLBookingTypes.ChildNodes.Item(0)
                    While Not XMLTmpNode2 Is Nothing
                        TmpBT = TmpChannel.BookingTypes.Add(XMLTmpNode2.GetAttribute("Name"), False)
                        TmpBT.Shortname = XMLTmpNode2.GetAttribute("Shortname")
                        TmpBT.IsRBS = XMLTmpNode2.GetAttribute("IsRBS")
                        TmpBT.IsSpecific = XMLTmpNode2.GetAttribute("IsSpecific")
                        TmpBT.Dayparts = Campaign.Dayparts
                        If XMLTmpNode2.GetAttribute("IsPremium") <> "" Then
                            TmpBT.IsPremium = XMLTmpNode2.GetAttribute("IsPremium")
                        End If
                        Try
                            TmpBT.PrintDayparts = XMLTmpNode2.GetAttribute("PrintDayparts")
                            TmpBT.PrintBookingCode = XMLTmpNode2.GetAttribute("PrintBookingCode")
                        Catch
                            TmpBT.PrintDayparts = False
                            TmpBT.PrintBookingCode = False
                        End Try

                        'if we are in the middle of an campaign we need to add hte weeks to the BT aswell
                        If Campaign.Channels(chanName).BookingTypes(btName).Weeks.Count > 0 Then
                            For Each week As Trinity.cWeek In Campaign.Channels(chanName).BookingTypes(btName).Weeks
                                TmpBT.Weeks.Add(week.Name)
                                TmpBT.Weeks(week.Name).StartDate = week.StartDate
                                TmpBT.Weeks(week.Name).EndDate = week.EndDate
                                For Each f As Trinity.cFilm In Campaign.Channels(1).BookingTypes(1).Weeks(1).Films
                                    TmpBT.Weeks(week.Name).Films.Add(f.Name)
                                    TmpBT.Weeks(week.Name).Films(f.Name).Filmcode = f.Filmcode
                                    TmpBT.Weeks(week.Name).Films(f.Name).FilmLength = f.FilmLength
                                    TmpBT.Weeks(week.Name).Films(f.Name).Index = f.Index
                                Next
                            Next
                        End If

                        TmpBT.Pricelist.Targets.Clear()
                        TmpBT.ReadPricelist()
                        XMLTmpNode2 = XMLTmpNode2.NextSibling
                    End While
                    XMLTmpNode2 = XMLTmpNode.GetElementsByTagName("SpotIndex").Item(0).FirstChild
                    While Not XMLTmpNode2 Is Nothing
                        For Each TmpBT In TmpChannel.BookingTypes
                            TmpBT.FilmIndex(XMLTmpNode2.GetAttribute("Length")) = XMLTmpNode2.GetAttribute("Idx")
                        Next TmpBT
                        XMLTmpNode2 = XMLTmpNode2.NextSibling
                    End While
                    TmpChannel.fileName = sender.tag & ".xml"
                End If
                found = False
                XMLTmpNode = XMLTmpNode.NextSibling
            End While

            LongName.Clear()
            For i As Integer = 1 To Campaign.Channels.Count
                LongName.Add(Campaign.Channels(i).Shortname, Campaign.Channels(i).ChannelName)
            Next
            LongBT.Clear()
            For Each chan As Trinity.cChannel In Campaign.Channels
                For Each BT As Trinity.cBookingType In chan.BookingTypes
                    If Not LongBT.ContainsKey(BT.Shortname) Then
                        LongBT.Add(BT.Shortname, BT.Name)
                    End If
                Next
            Next

            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
            Me.Cursor = Windows.Forms.Cursors.Default

        Catch
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
            Me.Cursor = Windows.Forms.Cursors.Default
            MsgBox("Error reading channel set", MsgBoxStyle.Critical, "Error")

        End Try
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Dim MyVersion As String = ""
        If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\Version.xml") Then

            Dim xmlDoc As New XmlDocument
            xmlDoc.Load(My.Application.Info.DirectoryPath & "\Version.xml")

        End If
        MyVersion = Format(Trinity.Helper.CompileTime, "yyyy-MM-dd HH:mm:ss")

        Windows.Forms.MessageBox.Show("T R I N I T Y" & vbCrLf & "Version: " & My.Application.Info.Version.ToString & vbCrLf & "Build: " & MyVersion & vbCrLf & "Connect version: " & Trinity.Helper.GetRegisteredDLLVersion & vbCrLf & vbCrLf & "IP: " & System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName)(0).ToString & vbCrLf & vbCrLf & "For support, mail to:" & vbCrLf & "trinity.support@groupm.com", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)

    End Sub

    'Private Sub HelpToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripButton.Click
    '    'Campaign.newCampaign()



    '    '**********************************************************
    '    '**********************************************************
    '    '**********************************************************
    '    '**********************************************************

    '    'Campaign.newCampaign()
    '    Campaign.Channels = New Trinity.cChannels(Campaign)

    '    'reads channels and pricelists from the database
    '    Dim DBConn As New System.Data.SqlClient.SqlConnection
    '    DBConn.ConnectionString = TrinitySettings.ConnectionString(Trinity.cSettings.SettingsLocationEnum.locNetwork) & "Integrated Security=True;" & ";"
    '    DBConn.Open()

    '    Dim com As New SqlClient.SqlCommand
    '    com.Connection = DBConn


    '    com.CommandText = "SELECT * FROM Channels, Bookingtypes, targets, pricelistrows WHERE channels.channelid = bookingtypes.channelid AND bookingtypes.btid = targets.btid AND Targets.targetid = pricelistrows.targetid ORDER BY channels.channelid, bookingtypes.btid, targets.BTID,  targets.targetid"

    '    Dim rd As SqlClient.SqlDataReader
    '    rd = com.ExecuteReader()

    '    Dim strLastChannelID As String = ""
    '    Dim strLastBookingtypeID As String = ""
    '    Dim strLastTargetID As String = ""

    '    Dim TmpChan As Trinity.cChannel
    '    Dim TmpBT As Trinity.cBookingType
    '    Dim TmpTarget As Trinity.cPricelistTarget
    '    Dim TmpPeriod As Trinity.cPricelistPeriod

    '    While rd.Read()

    '        'if its a new channel we add it
    '        If Not rd.Item("ChannelID") = strLastChannelID Then

    '            strLastChannelID = rd.Item("ChannelID")

    '            'Campaign.Channels.Add(rd.Item("name"), rd.Item("Channelset"), rd.Item("Area"))
    '            TmpChan = Campaign.Channels.Add(rd.Item("channelname"), "", rd.Item("Area"), rd.Item("ChannelID"))
    '            TmpChan.Shortname = rd.Item("channelShortname")
    '            TmpChan.MarathonName = rd.Item("Marathonname")
    '            TmpChan.AdEdgeNames = rd.Item("AdvantEdgename")
    '            TmpChan.MatrixName = rd.Item("Matrixname")

    '            TmpChan.BuyingUniverse = rd.Item("Buyinguniverse")
    '            TmpChan.AgencyCommission = rd.Item("Agencycommission")
    '            TmpChan.ListNumber = rd.Item("SortNumber")

    '            TmpChan.ConnectedChannel = rd.Item("Connectedchannel")
    '            TmpChan.LogoPath = rd.Item("Logopath")
    '            TmpChan.DeliveryAddress = rd.Item("Address")
    '            TmpChan.VHSAddress = rd.Item("VHSAddress")
    '        End If

    '        'if its a new channel we add it
    '        If Not rd.Item("BTID") = strLastBookingtypeID Then

    '            strLastBookingtypeID = rd.Item("BTID")

    '            TmpBT = TmpChan.BookingTypes.Add(rd.Item("btname"), False)
    '            TmpBT.ID = rd.Item("btid")
    '            TmpBT.Shortname = rd.Item("btShortname")

    '            TmpBT.IsRBS = rd.Item("IsRBS")
    '            TmpBT.IsSpecific = rd.Item("IsSpecific")

    '            TmpBT.PrintBookingCode = rd.Item("PrintBookingcode")
    '            TmpBT.PrintDayparts = rd.Item("PrintDaypart")
    '            TmpBT.AverageRating = rd.Item("AverageRating")
    '        End If


    '        'if its a new target we add it
    '        If Not rd.Item("targetID") = strLastTargetID Then
    '            strLastTargetID = rd.Item("targetID")

    '            TmpTarget = TmpBT.Pricelist.Targets.Add(rd.Item("targetname"))

    '            TmpTarget.CalcCPP = rd.Item("CalcCPP")

    '            For i As Integer = 1 To Campaign.DaypartCount - 1
    '                TmpTarget.DefaultDaypart(i - 1) = rd.Item("DefaultDP" & i)
    '            Next

    '            TmpTarget.StandardTarget = rd.Item("Standardtarget")

    '            TmpTarget.Target.NoUniverseSize = True
    '            TmpTarget.Target.Universe(False) = TmpChan.BuyingUniverse
    '            TmpTarget.Target.TargetName = rd.Item("AdvantEdgeTarget")

    '            TmpBT.SetAdedgeTarget(TmpTarget, rd.Item("AdvantEdgeTarget"), rd.Item("Targettype"), rd.Item("Targetgroup"))
    '        End If

    '        'we add the pricerow
    '        TmpPeriod = TmpTarget.PricelistPeriods.Add(rd.Item("name"))
    '        TmpPeriod.FromDate = rd.Item("FromDate")
    '        TmpPeriod.ToDate = rd.Item("ToDate")
    '        TmpPeriod.TargetNat = rd.Item("NatUniverse")
    '        TmpPeriod.TargetUni = rd.Item("Universe")
    '        TmpPeriod.PriceIsCPP = rd.Item("isCPP")

    '        TmpPeriod.Price(TmpPeriod.PriceIsCPP) = rd.Item("PriceDP0")
    '        For i As Integer = 0 To Campaign.DaypartCount - 1
    '            TmpPeriod.Price(TmpPeriod.PriceIsCPP, i) = rd.Item("PriceDP" & i)
    '        Next
    '    End While

    '    rd.Close()

    '    com.CommandText = "SELECT * FROM Spotindex ORDER BY ChannelID"
    '    rd = com.ExecuteReader()

    '    'denna är lite 100% effektiv pga cChannels()
    '    While rd.Read()
    '        If Not Campaign.Channels(rd.Item("ChannelID"), True) Is Nothing Then
    '            For Each TmpBT In Campaign.Channels(rd.Item("ChannelID"), True).BookingTypes
    '                TmpBT.FilmIndex(rd.Item("Length")) = rd.Item("Idx")
    '            Next
    '        End If
    '    End While
    '    rd.Close()

    '    'For i As Integer = 0 To dt.Columns.Count - 1
    '    '    Diagnostics.Debug.WriteLine(dt.Columns(i).ColumnName)
    '    'Next
    '    'Stop

    '    DBConn.Close()
    'End Sub

    'Private Sub readSQL()
    '    Campaign.Channels = New Trinity.cChannels(Campaign)

    '    'reads channels and pricelists from the database
    '    Dim DBConn As New System.Data.SqlClient.SqlConnection
    '    DBConn.ConnectionString = TrinitySettings.ConnectionString(Trinity.cSettings.SettingsLocationEnum.locNetwork) & "Integrated Security=True;" & ";"
    '    DBConn.Open()
    '    Dim DBConn2 As New System.Data.SqlClient.SqlConnection
    '    DBConn2.ConnectionString = TrinitySettings.ConnectionString(Trinity.cSettings.SettingsLocationEnum.locNetwork) & "Integrated Security=True;" & ";"
    '    DBConn2.Open()

    '    Dim com As New SqlClient.SqlCommand
    '    com.Connection = DBConn

    '    Dim comTarget As New SqlClient.SqlCommand
    '    comTarget.Connection = DBConn2

    '    'com.CommandText = "SELECT * FROM Channels, Bookingtypes WHERE channels.channelid = bookingtypes.channelID ORDER BY channels.channelid, bookingtypes.btid"
    '    'comTarget.CommandText = "SELECT channels.channelid, bookingtypes.btid, targets.*, pricelistrows.* FROM channels, bookingtypes, Targets, pricelistrows WHERE channels.channelid = bookingtypes.channelid AND bookingtypes.btid = targets.btid AND Targets.targetid = pricelistrows.targetid ORDER BY channels.channelid, bookingtypes.btid, targets.BTID,  targets.targetid"
    '    com.CommandText = "EXECUTE GetAllChannels"
    '    comTarget.CommandText = "EXECUTE GetAllPricelists"

    '    Dim rd As SqlClient.SqlDataReader
    '    Dim rdTarget As SqlClient.SqlDataReader
    '    rd = com.ExecuteReader()

    '    rdTarget = comTarget.ExecuteReader()

    '    rdTarget.Read() 'steps to the first line

    '    Dim strLastChannelID As String = ""
    '    Dim strLastTargetID As String = ""

    '    Dim TmpChan As Trinity.cChannel
    '    Dim TmpBT As Trinity.cBookingType
    '    Dim TmpTarget As Trinity.cPricelistTarget
    '    Dim TmpPeriod As Trinity.cPricelistPeriod

    '    While rd.Read()

    '        'if its a new channel we add it
    '        If Not rd.Item("ChannelID") = strLastChannelID Then

    '            strLastChannelID = rd.Item("ChannelID")

    '            'Campaign.Channels.Add(rd.Item("name"), rd.Item("Channelset"), rd.Item("Area"))
    '            TmpChan = Campaign.Channels.Add(rd.Item("channelname"), "", rd.Item("Area"), rd.Item("ChannelID"))
    '            TmpChan.Shortname = rd.Item("channelShortname")
    '            TmpChan.MarathonName = rd.Item("Marathonname")
    '            TmpChan.AdEdgeNames = rd.Item("AdvantEdgename")
    '            TmpChan.MatrixName = rd.Item("Matrixname")

    '            TmpChan.BuyingUniverse = rd.Item("Buyinguniverse")
    '            TmpChan.AgencyCommission = rd.Item("Agencycommission")
    '            TmpChan.ListNumber = rd.Item("SortNumber")

    '            TmpChan.ConnectedChannel = rd.Item("Connectedchannel")
    '            TmpChan.LogoPath = rd.Item("Logopath")
    '            TmpChan.DeliveryAddress = rd.Item("Address")
    '            TmpChan.VHSAddress = rd.Item("VHSAddress")
    '        End If

    '        TmpBT = TmpChan.BookingTypes.Add(rd.Item("btname"), False)
    '        TmpBT.ID = rd.Item("btid")
    '        TmpBT.Shortname = rd.Item("btShortname")

    '        TmpBT.IsRBS = rd.Item("IsRBS")
    '        TmpBT.IsSpecific = rd.Item("IsSpecific")

    '        TmpBT.PrintBookingCode = rd.Item("PrintBookingcode")
    '        TmpBT.PrintDayparts = rd.Item("PrintDaypart")
    '        TmpBT.AverageRating = rd.Item("AverageRating")

    '        'if we have a pricelist for the bookigntype we read it else we pass
    '        If rdTarget.Read() Then
    '            While rdTarget.Item("BTID") = TmpBT.ID

    '                'if its a new target we add it
    '                If Not rdTarget.Item("targetID") = strLastTargetID Then
    '                    strLastTargetID = rdTarget.Item("targetID")

    '                    TmpTarget = TmpBT.Pricelist.Targets.Add(rdTarget.Item("targetname"))

    '                    TmpTarget.CalcCPP = rdTarget.Item("CalcCPP")

    '                    For i As Integer = 1 To Campaign.DaypartCount - 1
    '                        TmpTarget.DefaultDaypart(i - 1) = rd.Item("DefaultDP" & i)
    '                    Next

    '                    TmpTarget.StandardTarget = rdTarget.Item("Standardtarget")

    '                    TmpTarget.Target.NoUniverseSize = True
    '                    TmpTarget.Target.Universe(False) = TmpChan.BuyingUniverse
    '                    TmpTarget.Target.TargetName = rdTarget.Item("AdvantEdgeTarget")

    '                    TmpBT.SetAdedgeTarget(TmpTarget, rdTarget.Item("AdvantEdgeTarget"), rdTarget.Item("Targettype"), rdTarget.Item("Targetgroup"))
    '                End If

    '                'we add the pricerow
    '                TmpPeriod = TmpTarget.PricelistPeriods.Add(rdTarget.Item("name"))
    '                TmpPeriod.FromDate = rdTarget.Item("FromDate")
    '                TmpPeriod.ToDate = rdTarget.Item("ToDate")
    '                TmpPeriod.TargetNat = rdTarget.Item("NatUniverse")
    '                TmpPeriod.TargetUni = rdTarget.Item("Universe")
    '                TmpPeriod.PriceIsCPP = rdTarget.Item("isCPP")

    '                TmpPeriod.Price(TmpPeriod.PriceIsCPP) = rdTarget.Item("PriceDP0")
    '                For i As Integer = 0 To Campaign.DaypartCount - 1
    '                    TmpPeriod.Price(TmpPeriod.PriceIsCPP, i) = rdTarget.Item("PriceDP" & i)
    '                Next

    '                'this line steps the recordset one step forward, if we are at the end it quites the while loop
    '                If Not rdTarget.Read Then Exit While
    '            End While
    '        End If
    '    End While

    '    rd.Close()
    '    rdTarget.Close()

    '    com.CommandText = "SELECT * FROM Spotindex ORDER BY ChannelID"
    '    rd = com.ExecuteReader()

    '    'denna är lite 100% effektiv pga cChannels()
    '    While rd.Read()
    '        For Each TmpBT In Campaign.Channels(rd.Item("ChannelID"), True).BookingTypes
    '            TmpBT.FilmIndex(rd.Item("Length")) = rd.Item("Idx")
    '        Next
    '    End While
    '    rd.Close()

    '    DBConn.Close()
    '    DBConn2.Close()
    'End Sub

    'Private Sub readXML()
    '    Campaign.Channels = New Trinity.cChannels(Campaign)

    '    Campaign.CreateChannels()

    '    For Each TmpChan As Trinity.cChannel In Campaign.Channels
    '        For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
    '            TmpBT.ReadPricelist()
    '        Next
    '    Next
    'End Sub

    Private Sub mnuMarathonCampaignSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarathonCampaignSettings.Click
        frmEditMarathon.txtPlanNr.Text = Campaign.MarathonPlanNr
        frmEditMarathon.txtGeneralOrder.Text = Campaign.MarathonOtherOrder
        If frmEditMarathon.ShowDialog = Windows.Forms.DialogResult.OK Then
            Campaign.MarathonPlanNr = frmEditMarathon.txtPlanNr.Text
            Campaign.MarathonOtherOrder = frmEditMarathon.txtGeneralOrder.Text
        End If
    End Sub

    Private Sub mnuMatrix_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuMatrix.DropDownOpening
        Trinity.Helper.WriteToLogFile("Displaying Matrix Menu")
        If Not Matrix Is Nothing Then
            If Matrix.LoggedInUser Is Nothing Then
                If MatrixVersion = MatrixVersionEnum.Matrix30 Then
                    mnuMatrixLogin.Text = "Log in to Matrix 3.0"
                Else
                    mnuMatrixLogin.Text = "Log in to Matrix 4.0"
                End If
                mnuMatrixExport.Enabled = False
            Else
                mnuMatrixLogin.Text = "Log off Matrix"
                mnuMatrixExport.Enabled = True
            End If
        Else
            If MatrixVersion = MatrixVersionEnum.Matrix30 Then
                mnuMatrixLogin.Text = "Log in to Matrix 3.0"
            Else
                mnuMatrixLogin.Text = "Log in to Matrix 4.0"
            End If
        End If

        'If the campaign has a MatrixID then it means it was assigned one when exporting it at some point
        If Campaign.MatrixID IsNot Nothing Then
            mnuMatrixExport.Checked = True
        Else
            mnuMatrixExport.Checked = False
        End If


    End Sub

    'This needs to be in its own sub if Matrix40 is not installed
    Private Sub CreateMatrix40()
        If TrinitySettings.MatrixDatabaseServer = "" Then
            Windows.Forms.MessageBox.Show("Please specify your Matrix database server in Preferences.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        End If
        Dim asm As System.Reflection.Assembly = System.Reflection.Assembly.LoadFile(IO.Path.Combine(My.Application.Info.DirectoryPath, "Matrix40.dll"))
        'asm = System.Reflection.Assembly.LoadFile(Path.Combine(My.Application.Info.DirectoryPath, "Matrix40.dll"))
        Dim t As Type = asm.GetType("Matrix40.cMatrix")
        Matrix = Activator.CreateInstance(t)
        Matrix.SetConnectionType(Matrix40.cMatrix.ConnectionTypeEnum.ctSQL)
        Matrix.SetConnectionString("Data Source=" & TrinitySettings.MatrixDatabaseServer & ";Initial Catalog=" & TrinitySettings.MatrixDatabase & ";UID=matrix_user;PWD=benchmark;")
        Matrix.Initialize()
    End Sub

    Private Sub mnuMatrixReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMatrixReset.Click
        Campaign.MatrixID = Nothing
    End Sub
    Private Sub mnuMatrixLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMatrixLogin.Click
        Trinity.Helper.WriteToLogFile("Attempting to connect to Matrix")
        If Matrix Is Nothing Then
            Trinity.Helper.WriteToLogFile("Matrix is Nothing")
            If MatrixVersion = MatrixVersionEnum.Matrix40 Then
                CreateMatrix40()
            ElseIf MatrixVersion = MatrixVersionEnum.Matrix30 Then
                Matrix = CreateObject("Matrix30.cMatrix")
            End If
            If Matrix Is Nothing Then
                Exit Sub
            End If
            If Matrix.LoggedInUser Is Nothing Then
                frmMatrixLogin.ShowDialog()
            End If
        Else
            Trinity.Helper.WriteToLogFile("Matrix is not Nothing")
            If Matrix.LoggedInUser Is Nothing Then
                frmMatrixLogin.ShowDialog()
            Else
                Matrix.Logoff()
                Matrix = Nothing
                If MatrixVersion = MatrixVersionEnum.Matrix40 Then
                    CreateMatrix40()
                Else
                    Matrix = CreateObject("Matrix30.cMatrix")
                End If
            End If
        End If
    End Sub

    Private Sub mnuMatrixExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMatrixExport.Click
        Dim Export As New frmMatrixExport
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        Windows.Forms.Application.DoEvents()
        tmrAutosave.Enabled = False
        Export.ShowDialog()
        tmrAutosave.Enabled = True
    End Sub

    Private Sub cmdDelivery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelivery.Click
        frmDelivery.MdiParent = Me
        frmDelivery.Show()
    End Sub

    Sub mouse_enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblNewCampaign.MouseEnter, lblRecent1.MouseEnter, lblRecent2.MouseEnter, lblRecent3.MouseEnter, lblRecent4.MouseEnter, lblRecent5.MouseEnter, lblOpenCampaign.MouseEnter
        Me.Cursor = Windows.Forms.Cursors.Hand
    End Sub

    Sub mouse_leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblNewCampaign.MouseLeave, lblRecent1.MouseLeave, lblRecent2.MouseLeave, lblRecent3.MouseLeave, lblRecent4.MouseLeave, lblRecent5.MouseLeave, lblOpenCampaign.MouseLeave
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub cmbTarget_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTarget.SelectedIndexChanged
        grdReach.Invalidate()
    End Sub

    Private Sub MoveCampaignDatesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveCampaignDatesToolStripMenuItem.Click

        'Does not currently move the Lab campaigns forward

        If Not Saved Then
            Dim rply As Windows.Forms.DialogResult

            rply = Windows.Forms.MessageBox.Show("Do you wish to save the campaign before proceeding? (Non saved changes will be lost to this campaign)", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question)
            If rply = MsgBoxResult.Yes Then
                'calls the save function
                cmdSave_Click(New Object, New EventArgs)
            ElseIf rply = MsgBoxResult.Cancel Then
                Exit Sub
            End If
        End If

        'close all open sub windows
        For Each Form As Windows.Forms.Form In Me.MdiChildren
            Form.Close()
            Form.Dispose()
        Next


        'set the date to a new one
        Me.frm = New frmAskForDate()
        frm.dtNormalFrom.Value = Date.FromOADate(Campaign.StartDate)
        frm.chkAdvanced.Checked = True
        If Not frm.ShowDialog() = Windows.Forms.DialogResult.OK Then Exit Sub

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        MoveCampaign(Campaign)

        ' If an error occured, stop execution
        If (frm.Failed) Then Return


        If Campaign.Campaigns IsNot Nothing Then
            For Each Camp As Object In Campaign.Campaigns
                MoveCampaign(Camp.value)
            Next
        End If
        If Not TrinitySettings.SaveCampaignsAsFiles AndAlso Windows.Forms.MessageBox.Show("Do you want the moved campaign to be created as a new campaign?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Campaign.DatabaseID = 0
        End If
    End Sub

    Public Sub MoveCampaign(ByVal labCampaign As Trinity.cKampanj)
        labCampaign.ExtendedInfos.RemoveAll() 'resets extended infos

        'set a new id
        labCampaign.ID = CreateGUID()

        'delete all spots
        labCampaign.ActualSpots = New Trinity.cActualSpots(labCampaign)
        labCampaign.PlannedSpots = New Trinity.cPlannedSpots(labCampaign)
        labCampaign.BookedSpots = New Trinity.cBookedSpots(labCampaign)

        labCampaign.Commentary = ""
        labCampaign.MarathonPlanNr = 0
        labCampaign.MarathonOtherOrder = 0
        labCampaign.MarathonCTC = 0
        For i As Integer = 1 To 10
            labCampaign.ReachGoal(i) = 0
        Next

        labCampaign.Adedge = New ConnectWrapper.Brands


        'read the pricelists
        For Each TmpChan As Trinity.cChannel In labCampaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes

                Dim TmpNewBT As New Trinity.cBookingType(labCampaign)
                TmpNewBT.ParentChannel = TmpBT.ParentChannel
                TmpNewBT.Name = TmpBT.Name
                TmpNewBT.Dayparts = TmpBT.Dayparts
                TmpNewBT.PricelistName = TmpBT.PricelistName
                TmpNewBT.ReadPricelist()

                For Each TmpTarget As Trinity.cPricelistTarget In TmpBT.Pricelist.Targets
                    Dim TmpTarget2 As Trinity.cPricelistTarget = TmpNewBT.Pricelist.Targets(TmpTarget.TargetName)

                    If Not TmpTarget2 Is Nothing Then
                        TmpTarget.Target.NoUniverseSize = True
                        TmpTarget.Target.TargetName = TmpTarget2.Target.TargetName
                        TmpTarget.Target.NoUniverseSize = False
                        TmpTarget.CalcCPP = TmpTarget2.CalcCPP
                        TmpTarget.StandardTarget = TmpTarget2.StandardTarget
                        Select Case TmpTarget.IsEntered
                            Case Trinity.cPricelistTarget.EnteredEnum.eCPP
                                TmpTarget.NetCPP = TmpTarget.NetCPP
                            Case Trinity.cPricelistTarget.EnteredEnum.eCPT
                                TmpTarget.NetCPT = TmpTarget.NetCPT
                            Case Trinity.cPricelistTarget.EnteredEnum.eDiscount
                                TmpTarget.Discount = TmpTarget.Discount
                        End Select
                    End If
                    TmpTarget.PricelistPeriods.Clear()
                    If Not TmpTarget2 Is Nothing Then
                        For Each TmpPeriod As Trinity.cPricelistPeriod In TmpTarget2.PricelistPeriods

                            With TmpTarget.PricelistPeriods.Add(TmpPeriod.Name, TmpPeriod.ID)
                                .FromDate = TmpPeriod.FromDate
                                .ToDate = TmpPeriod.ToDate
                                .PriceIsCPP = TmpPeriod.PriceIsCPP
                                If TmpTarget2.CalcCPP Then
                                    For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                                        .Price(TmpPeriod.PriceIsCPP, i) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, i)
                                    Next
                                Else
                                    .Price(TmpPeriod.PriceIsCPP) = TmpPeriod.Price(TmpPeriod.PriceIsCPP)
                                End If
                                .TargetNat = TmpPeriod.TargetNat
                                .TargetUni = TmpPeriod.TargetUni
                            End With
                        Next
                    End If
                Next
                For Each TmpTarget2 As Trinity.cPricelistTarget In TmpNewBT.Pricelist.Targets
                    Try
                        If Not TmpBT.Pricelist.Targets.Contains(TmpTarget2.TargetName) Then
                            Dim TmpTarget As Trinity.cPricelistTarget = TmpBT.Pricelist.Targets.Add(TmpTarget2.TargetName, TmpBT)
                            If Not TmpTarget2 Is Nothing Then
                                TmpTarget.Target.NoUniverseSize = True
                                TmpTarget.Target.TargetName = TmpTarget2.Target.TargetName
                                TmpTarget.Target.NoUniverseSize = False
                                TmpTarget.CalcCPP = TmpTarget2.CalcCPP
                                TmpTarget.StandardTarget = TmpTarget2.StandardTarget
                                Select Case TmpTarget.IsEntered
                                    Case Trinity.cPricelistTarget.EnteredEnum.eCPP
                                        TmpTarget.NetCPP = TmpTarget.NetCPP
                                    Case Trinity.cPricelistTarget.EnteredEnum.eCPT
                                        TmpTarget.NetCPT = TmpTarget.NetCPT
                                    Case Trinity.cPricelistTarget.EnteredEnum.eDiscount
                                        TmpTarget.Discount = TmpTarget.Discount
                                End Select
                            End If
                            TmpTarget.PricelistPeriods.Clear()
                            If Not TmpTarget2 Is Nothing Then
                                For Each TmpPeriod As Trinity.cPricelistPeriod In TmpTarget2.PricelistPeriods
                                    With TmpTarget.PricelistPeriods.Add(TmpPeriod.Name, TmpPeriod.ID)
                                        .FromDate = TmpPeriod.FromDate
                                        .ToDate = TmpPeriod.ToDate
                                        .PriceIsCPP = TmpPeriod.PriceIsCPP
                                        If TmpTarget2.CalcCPP Then
                                            For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                                                .Price(TmpPeriod.PriceIsCPP, i) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, i)
                                            Next
                                        Else
                                            .Price(TmpPeriod.PriceIsCPP) = TmpPeriod.Price(TmpPeriod.PriceIsCPP)
                                        End If
                                        .TargetNat = TmpPeriod.TargetNat
                                        .TargetUni = TmpPeriod.TargetUni
                                    End With
                                Next
                            End If
                        End If
                    Catch ex As Exception

                    End Try
                Next
                If Not TmpBT.BuyingTarget Is Nothing AndAlso Not TmpBT.BuyingTarget.TargetName Is Nothing AndAlso TmpBT.BuyingTarget.TargetName <> "" Then
                    TmpBT.BuyingTarget.Target.NoUniverseSize = True
                    TmpBT.BuyingTarget.Target.TargetName = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Target.TargetName
                    TmpBT.BuyingTarget.Target.NoUniverseSize = False
                    TmpBT.BuyingTarget.CalcCPP = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).CalcCPP
                    TmpBT.BuyingTarget.StandardTarget = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).StandardTarget
                    Select Case TmpBT.BuyingTarget.IsEntered
                        Case Trinity.cPricelistTarget.EnteredEnum.eCPP
                            TmpBT.BuyingTarget.NetCPP = TmpBT.BuyingTarget.NetCPP
                        Case Trinity.cPricelistTarget.EnteredEnum.eCPT
                            TmpBT.BuyingTarget.NetCPT = TmpBT.BuyingTarget.NetCPT
                        Case Trinity.cPricelistTarget.EnteredEnum.eDiscount
                            TmpBT.BuyingTarget.Discount = TmpBT.BuyingTarget.Discount
                    End Select
                    If Not TmpBT.BuyingTarget Is TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName) Then
                        TmpBT.BuyingTarget.PricelistPeriods.Clear()
                        For Each TmpPeriod As Trinity.cPricelistPeriod In TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).PricelistPeriods
                            With TmpBT.BuyingTarget.PricelistPeriods.Add(TmpPeriod.Name, TmpPeriod.ID)
                                .FromDate = TmpPeriod.FromDate
                                .ToDate = TmpPeriod.ToDate
                                .PriceIsCPP = TmpPeriod.PriceIsCPP
                                If TmpBT.BuyingTarget.CalcCPP Then
                                    For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                                        .Price(TmpPeriod.PriceIsCPP, i) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, i)
                                    Next
                                Else
                                    .Price(TmpPeriod.PriceIsCPP) = TmpPeriod.Price(TmpPeriod.PriceIsCPP)
                                End If
                                .TargetNat = TmpPeriod.TargetNat
                                .TargetUni = TmpPeriod.TargetUni
                            End With
                        Next
                    End If
                End If
            Next
        Next

        'Dim newDate As Date = frm.dtNormalFrom.Value
        'Dim oldDate As Date = Date.FromOADate(labCampaign.Channels(1).BookingTypes(1).Weeks(1).StartDate)
        'Dim intDate As Integer = newDate.ToOADate - oldDate.ToOADate

        For Each c As Trinity.cChannel In labCampaign.Channels
            For Each bt As Trinity.cBookingType In c.BookingTypes
                bt.ConfirmedNetBudget = 0

                bt.OrderNumber = Nothing
                bt.ContractNumber = ""


                ' Flag if something in the iteration has failed
                Dim failFlag As Boolean = False

                ' Store the original values in case we need to revert the changes
                Dim tmpOldValues As New List(Of String)
                ' Create padding for 0-to 1-based array
                tmpOldValues.Add(0)
                For i As Integer = 1 To bt.Weeks.Count Step 1
                    tmpOldValues.Add(bt.Weeks(i).Name)
                Next



                'Stepping backwards keeps the filmindexes intact
                For i As Integer = bt.Weeks.Count To 1 Step -1

                    Dim w As Trinity.cWeek = bt.Weeks(i)

                    ' Test for duplicate name in the collection
                    'Dont make the test if its the same object in both cases
                    failFlag = (bt.Weeks.Contains(frm.grdPeriod.Rows(i - 1).Cells(0).Value, w))


                    If (Not failFlag) Then
                        w.Name = frm.grdPeriod.Rows(i - 1).Cells(0).Value
                        w.StartDate = DirectCast(frm.grdPeriod.Rows(i - 1).Cells(1).Value, Date).ToOADate
                        w.EndDate = DirectCast(frm.grdPeriod.Rows(i - 1).Cells(2).Value, Date).ToOADate
                    End If


                Next

                If (failFlag) Then
                    For a As Integer = bt.Weeks.Count To 1 Step -1
                        bt.Weeks(a).Name = tmpOldValues(a)
                    Next

                    Windows.Forms.MessageBox.Show("Error while moving campaign!" + vbNewLine + "Two or more weeks have the same name. All changes has been reverted!",
                                                  "T R I N I T Y ", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Warning)

                    ' Flag the form for error
                    frm.Failed = True

                    Me.Cursor = Windows.Forms.Cursors.Default
                    Return

                End If

                ' If something has been flag as failed, restore all original values and post a message about the problem

                'If intDate < 0 Then
                '    For Each w As Trinity.cWeek In bt.Weeks
                '        w.StartDate += intDate
                '        w.EndDate += intDate
                '        Try
                '            w.Name = DatePart(DateInterval.WeekOfYear, Date.FromOADate(w.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                '        Catch
                '            w.Name = DatePart(DateInterval.WeekOfYear, Date.FromOADate(w.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & "x"
                '        End Try
                '    Next
                'Else
                '    For i As Integer = bt.Weeks.Count To 1 Step -1
                '        Dim w As Trinity.cWeek = bt.Weeks(i)
                '        If w.StartDate > 999999 Then
                '            ' Stop
                '        End If
                '        w.StartDate += intDate
                '        w.EndDate += intDate
                '        Try
                '            w.Name = DatePart(DateInterval.WeekOfYear, Date.FromOADate(w.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                '        Catch
                '            w.Name = DatePart(DateInterval.WeekOfYear, Date.FromOADate(w.StartDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays) & "x"
                '        End Try
                '    Next
                'End If
            Next
        Next

        'This will set all lab labCampaigns to nothing so that they get created again from the new conditions when Lab is opened
        'labCampaign.labCampaigns = Nothing

        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub



    Private Sub DefineDaypartsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefineDaypartsToolStripMenuItem.Click
        frmDefineDayparts.ShowDialog()
    End Sub

    Private Sub ManualToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManualToolStripMenuItem.Click, lblManual.Click
        'uses the standard browser to open the URL
        System.Diagnostics.Process.Start("http://apps.mecglobal.se/trinity/Trinity_Manual.pdf")
    End Sub

    Private Sub HelpSupportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpSupportToolStripMenuItem.Click

    End Sub

    Private Sub ActivateRemoteHelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActivateRemoteHelpToolStripMenuItem.Click
        Dim OS As OperatingSystem
        OS = System.Environment.OSVersion

        'Major = 5 and Minor = 1 is Windows XP
        If OS.Version.Major = 6 Then 'Windows 7 
            Shell("msra /email trinity", Microsoft.VisualBasic.AppWinStyle.NormalFocus, False)
            Windows.Forms.MessageBox.Show("A new mail has been created for you. Send it to trinity@mecglobal.com and do not close the Remote Assistance window that has opened. " & vbNewLine & vbNewLine &
                                          "You will need to confirm the connection before the Trinity administrators assist you.", "Information")
            Exit Sub
        End If
        If OS.Version.Major <> 5 And OS.Version.Minor <> 1 Then
            Windows.Forms.MessageBox.Show("This feature only works on Windows XP at the moment")
            Exit Sub
        End If

        Dim TmpRA As New cRemoteAssistance
        Try
            TmpRA.CheckAndSetRegistry()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show("Unable to set registry parameters to enable unsolicited remote assistance." & vbCrLf &
            "This application will now exit" & vbCrLf & ex.Message & vbCrLf & ex.StackTrace,
            "Error!", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        End Try
        Try
            If Not TmpRA.RequestAssistance() Then
                Windows.Forms.MessageBox.Show("Unable to create the Remote assistance ticket. Please consult the system admin.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            Else
                Windows.Forms.MessageBox.Show("A Remote assistance request was sent. Please wait for a connection" & vbCrLf & "from one of the system administrators.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message, "ERROR", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuReadSpotcontrol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReadSpotcontrol.Click
        frmSpotControlImport.ShowDialog()
    End Sub

    Private Sub mnuExtranet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExtranet.Click
        frmExtranet.ShowDialog()
    End Sub

    Private Sub CreateFromAdvantEdgeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateFromAdvantEdgeToolStripMenuItem.Click
        Dim frm As New frmCreateCampaign
        frm.ShowDialog()
        Reset()
        lblStatus.Text = ""
    End Sub


#Region "NewVersionThread"

    Private Sub tmrNewVersion_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrNewVersion.Tick
        Dim t As New Trinity.cVersionCheck(Me)
        Dim Thread1 As New System.Threading.Thread(AddressOf t.Run)
        Thread1.Priority = Threading.ThreadPriority.Lowest
        Thread1.IsBackground = True
        Thread1.Start()
    End Sub

    Public Sub EndThread(ByVal IsNewVersion As Boolean)

        If IsNewVersion Then
            Dim baloon As New System.Windows.Forms.NotifyIcon()
            AddHandler baloon.BalloonTipClicked, AddressOf Me.Clicked
            AddHandler baloon.BalloonTipClosed, AddressOf Me.disposeIcon
            Try
                baloon.Icon = New Icon(System.Windows.Forms.Application.StartupPath & "\Trinity.ico")
            Catch
                Dim bit As New Bitmap(20, 20)
                Dim hIcon As IntPtr = bit.GetHicon()
                Dim icn As Icon = Icon.FromHandle(hIcon)

                baloon.Icon = icn
            End Try
            baloon.BalloonTipTitle = "New Trinity Version"
            baloon.BalloonTipText = "Click here to download new version"
            baloon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
            baloon.Visible = True
            baloon.ShowBalloonTip(5000)
        End If

    End Sub

    Private Sub disposeIcon(ByVal sender As Object, ByVal e As System.EventArgs)
        DirectCast(sender, System.Windows.Forms.NotifyIcon).Dispose()
    End Sub

    Public Sub Clicked(ByVal sender As Object, ByVal e As System.EventArgs)
        'we get the latest version
        DirectCast(sender, System.Windows.Forms.NotifyIcon).Dispose()

        If MsgBox("Save campaign?", MsgBoxStyle.YesNo, "Save") = MsgBoxResult.Yes Then
            Campaign.SaveCampaign()
        End If

        If Windows.Forms.MessageBox.Show("This will restart Trinity.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OKCancel, Windows.Forms.MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
            Kill(Trinity.Helper.Pathify(My.Application.Info.DirectoryPath) & "version.txt")
            Shell(Trinity.Helper.Pathify(My.Application.Info.DirectoryPath) & "launchtrinity.exe")
            End
        End If

    End Sub
#End Region

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        frmPivotTableDevEx.MdiParent = Me
        frmPivotTableDevEx.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim oXS As XmlSerializer

        oXS = New XmlSerializer(GetType(Trinity.cActualSpots))
        'oXS = New XmlSerializer(GetType(Trinity.cActualSpot))

        Dim oStmW As StreamWriter

        'Serialize object to XML and write it to XML file
        oStmW = New StreamWriter("c:\" & "actualspots" & ".xml")
        oXS.Serialize(oStmW, Campaign.ActualSpots)
        'oXS.Serialize(oStmW, Campaign.ActualSpots(1))
        oStmW.Close()
    End Sub

    Private Sub PerformanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PerformanceToolStripMenuItem.Click
        Dim tmpPerformanceTest As New cPerformanceTest
    End Sub

    Private Sub EvaluateRBSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EvaluateRBSToolStripMenuItem.Click
        frmEvaluateRBS.MdiParent = Me
        frmEvaluateRBS.Show()
    End Sub

    Private Sub CampaignToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles CampaignToolStripMenuItem.DropDownOpening
        mnuExtranet.Visible = TrinitySettings.ExtranetAvailable
    End Sub

    Private Sub CampaignToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CampaignToolStripMenuItem.Click

    End Sub

    Private Sub mnuMarathonShowInsertions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarathonShowInsertions.Click
        If Not Campaign.MarathonInsertions Is Nothing Then
            frmShowMarathonInsertions.ShowDialog()
        Else
            Windows.Forms.MessageBox.Show("No Marathon insertions yet been created with Trinity", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        End If

    End Sub

    Private Sub UploadChannelSchedulesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadChannelSchedulesToolStripMenuItem.Click

        Dim uploadForm As New ScheduleUploader.frmImport()
        uploadForm.ShowDialog()

        'frmUploadSchedule.Show()
    End Sub

    Private Sub cmdLinkCampaign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLinkCampaign.Click
        frmLinkCampaign.ShowDialog()
    End Sub

    Private Sub ExportExcelGrid()

    End Sub

    Private Sub btnExportGridToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportGridToExcel.Click

        Cursor = Windows.Forms.Cursors.Cross
        AddHandler MouseUp, AddressOf ExportExcelGrid
        'wai()
        'RemoveHandler Mouseup, adressof ExportExcelGrid
        Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub OpenFromDB()
        If Not Campaign.Name Is Nothing Then

            Dim rply As Windows.Forms.DialogResult

            rply = System.Windows.Forms.MessageBox.Show("You are currently working on a campaign. " &
                                                    vbNewLine & "Save it before opening another?", "T R I N I T Y",
                                                    Windows.Forms.MessageBoxButtons.YesNoCancel, Windows.Forms.MessageBoxIcon.Question)

            If rply = Windows.Forms.DialogResult.Yes Then
                Campaign.SaveCampaign("", True, , , , True)
                System.Windows.Forms.MessageBox.Show("Saved!", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            End If
            If rply = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End If

        If frmOpenFromDB.ShowDialog <> Windows.Forms.DialogResult.OK Then
            Me.Cursor = Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'Changes the cursor to a "loading" symbol
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        'closes all open windows
        Dim j As Integer
        For j = My.Application.OpenForms.Count - 1 To 0 Step -1
            If Not My.Application.OpenForms(j).Name = "frmMain" Then
                My.Application.OpenForms(j).Dispose()
            End If
        Next

        'resets all buttons and information containers, görs inte detta nedan? knappar sets och update info körs???
        Reset()

        'if the setup dialog for a new campaign is open it will be closed
        If Not frmSetup Is Nothing Then
            frmSetup.Dispose()
        End If

        AddHandler Campaign.LockChanged, AddressOf LockChanged

        InitializeCampaign()
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub
    Sub LockChanged(ByVal Username As String)
        If Username <> TrinitySettings.UserName Then
            Windows.Forms.MessageBox.Show("The user " & Username & " has unlocked this campaign to edit it." & vbCrLf & "You will no longer be able to save the campaign." & vbCrLf & vbCrLf & "You can, however, use 'Save as' to save it under another name.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub mnuSaveAsToDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSaveAsToDB.Click

        If Campaign.DatabaseID = 0 Then
            Windows.Forms.MessageBox.Show("This campaign has not been saved before", "Information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim Problems As Boolean = False
        Dim ProblemString As String = "The following needs to be corrected before saving to the database:" & vbNewLine & vbNewLine

        If Campaign.Name = "" Then
            ProblemString &= " - The campaign needs to be given a name" & vbNewLine
            Problems = True
        End If

        If DBReader.campaignNameExists(Campaign.Name) Then
            Dim _newName As String = Campaign.Name
            While _newName <> "" AndAlso DBReader.campaignNameExists(_newName)
                _newName = InputBox("A campaign with this name already exists in the database. Please rename it.", "T R I N I T Y", _newName)
            End While
            If _newName = "" Then Exit Sub
        End If

        If Campaign.ClientID = 0 Then
            ProblemString &= " - Client for this campaign has not been set" & vbNewLine
            Problems = True
        End If

        If Campaign.ProductID = 0 Then
            ProblemString &= " - Product for this campaign has not been set" & vbNewLine
            Problems = True
        End If

        If Campaign.PlannerID < 1 Then
            ProblemString &= " - Planner has not been set" & vbNewLine
            Problems = True
        End If

        If Campaign.BuyerID = 0 Then
            ProblemString &= " - Buyer has not been set" & vbNewLine
            Problems = True
        End If

        If Problems Then
            Windows.Forms.MessageBox.Show(ProblemString, "Information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        Else
            Campaign.DatabaseID = 0
            Campaign.SaveCampaign(, True, , , , True)
        End If
    End Sub

    Dim AllProblemsFoundEventHandler As New Trinity.cKampanj.AllProblemsFoundEventHandler(AddressOf AllProblemsFound)
    Private Sub tmrDetectProblems_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDetectProblems.Tick
        If Campaign Is Nothing OrElse Campaign.Channels Is Nothing OrElse Campaign.Channels.Count = 0 Then
            icnProblems.Visible = False
            Exit Sub
        End If
        RemoveHandler Campaign.FoundProblems, AddressOf ProblemsFound
        AddHandler Campaign.FoundProblems, AddressOf ProblemsFound
        RemoveHandler Campaign.AllProblemsFound, AllProblemsFoundEventHandler
        AddHandler Campaign.AllProblemsFound, AllProblemsFoundEventHandler
        tmrDetectProblems.Enabled = False
        Campaign.AsyncDetectAllProblems()
    End Sub

    Private Delegate Sub ProblemsFoundDelegate()
    Sub ProblemsFound()
        If Me.InvokeRequired Then
            Me.Invoke(New ProblemsFoundDelegate(AddressOf ProblemsFound))
        Else

            If Not Campaign.Problems Is Nothing Then
                If Campaign.Problems.Where(Function(p) p.IsVisible).Count > 0 Then
                    icnProblems.Visible = True
                End If
            Else
                icnProblems.Visible = False
            End If
        End If
    End Sub

    Sub AllProblemsFound()
        If Me.InvokeRequired Then
            If IsDisposed Then
                Exit Sub
            End If
            Me.Invoke(New ProblemsFoundDelegate(AddressOf AllProblemsFound))
        Else
            icnProblems.Visible = (Campaign.Problems IsNot Nothing AndAlso Campaign.Problems.Where(Function(p) p.IsVisible).Count > 0)
            If Campaign.Problems IsNot Nothing AndAlso Campaign.Problems.Where(Function(p) p.Severity = Trinity.cProblem.ProblemSeverityEnum.Error).Count > 0 Then
                icnProblems.Image = ilsBig.Images("Error")
            Else
                icnProblems.Image = ilsBig.Images("Warning")
            End If
            tmrDetectProblems.Enabled = True
        End If
    End Sub

    Private Sub icnProblems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles icnProblems.Click
        frmProblems.MdiParent = Me
        frmProblems.Show()
    End Sub

    Private Sub ShowProblemsWindowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowProblemsWindowToolStripMenuItem.Click
        frmProblems.MdiParent = Me
        frmProblems.Show()
    End Sub

    Private Sub icnProblems_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles icnProblems.VisibleChanged
        lblClickMe.Visible = icnProblems.Visible
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SettingsToolStripMenuItem.Click

    End Sub

    Private Sub SettingsToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles SettingsToolStripMenuItem.DropDownOpening
        mnuDownloadDevVersion.DropDownItems.Clear()

        Dim _xml As New Xml.XmlDocument
        Try
            _xml.Load(My.Application.Info.DirectoryPath & "\versions.xml")

            Dim _releases As Xml.XmlElement
            _releases = _xml.SelectSingleNode("//Releases")

            If _releases Is Nothing Then Exit Sub

            For Each _release As Xml.XmlElement In _releases.ChildNodes
                With mnuDownloadDevVersion.DropDownItems.Add(_release.GetAttribute("Name"))
                    .Tag = _release.GetAttribute("EXEName")
                    AddHandler .Click, AddressOf DownloadDevelopmentVersion
                End With
            Next
        Catch ex As Exception
            Debug.Print("Error finding the releases node in versions.xml")
        End Try
        mnuEditPeople.Visible = Not TrinitySettings.SaveCampaignsAsFiles

    End Sub

    Sub DownloadDevelopmentVersion(ByVal sender As System.Windows.Forms.ToolStripItem, ByVal e As EventArgs)
        If Windows.Forms.MessageBox.Show("This will restart Trinity.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OKCancel, Windows.Forms.MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
            Shell(Trinity.Helper.Pathify(My.Application.Info.DirectoryPath) & "launchtrinity.exe -file """ & sender.Tag & """")
            End
        End If

    End Sub

    Private Sub FileToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles FileToolStripMenuItem.DropDownOpening
        If TrinitySettings.SaveCampaignsAsFiles Then
            mnuSaveAsToDB.Visible = False
            mnuSaveAs.Visible = True
            sepRecent.Visible = True
            mnuRecent.Visible = True
        Else
            mnuSaveAsToDB.Visible = True
            mnuSaveAs.Visible = False
            mnuSave.Enabled = Not Campaign.ReadOnly
            mnuSaveAsToDB.Enabled = (Campaign.DatabaseID <> 0)
            sepRecent.Visible = False
            mnuRecent.Visible = False
        End If
    End Sub

    Private Sub ChannelPackagesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChannelPackagesToolStripMenuItem.Click

        frmManageBundles.ShowDialog()

    End Sub

    Sub SaveToDB()
        Dim Problems As Boolean = False
        Dim ProblemString As String = "The following needs to be corrected before saving to the database:" & vbNewLine & vbNewLine

        If Campaign.Name = "" Then
            ProblemString &= " - The campaign needs to be given a name" & vbNewLine
            Problems = True
        End If

        If Campaign.ClientID = 0 Then
            ProblemString &= " - Client for this campaign has not been set" & vbNewLine
            Problems = True
        End If

        If Campaign.ProductID = 0 Then
            ProblemString &= " - Product for this campaign has not been set" & vbNewLine
            Problems = True
        End If

        If Campaign.PlannerID < 1 AndAlso DBReader.getUserID(Campaign.Planner) = 0 Then
            ProblemString &= " - Planner has not been set" & vbNewLine
            Problems = True
        ElseIf Campaign.PlannerID < 1 Then
            Campaign.PlannerID = DBReader.getUserID(Campaign.Planner)
        End If

        If Campaign.BuyerID = 0 AndAlso DBReader.getUserID(Campaign.Buyer) = 0 Then
            ProblemString &= " - Buyer has not been set" & vbNewLine
            Problems = True
        ElseIf Campaign.BuyerID < 1 Then
            Campaign.BuyerID = DBReader.getUserID(Campaign.Buyer)
        End If

        If Problems Then
            Windows.Forms.MessageBox.Show(ProblemString, "Information", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        Else
            Dim enableAutosave As Boolean = TrinitySettings.Autosave
            tmrAutosave.Enabled = False
            Me.Cursor = Windows.Forms.Cursors.WaitCursor
            If Campaign.DatabaseID > 0 Then
                Dim _list As List(Of CampaignEssentials) = DBReader.GetCampaigns(String.Format("SELECT id,name FROM campaigns WHERE id={0}", Campaign.DatabaseID))
                If _list.Count > 0 AndAlso _list(0).name <> Campaign.Name Then
                    If Windows.Forms.MessageBox.Show(String.Format("You have changed the name of your campaign from '{0}' to '{1}'." & vbCrLf & vbCrLf & "Are you sure you want to save this campaign?" & vbCrLf & vbCrLf & "If you want this to be saved as a new campaign you should click 'No' and then choose 'Save as a new campaign' from the file menu.", _list(0).name, Campaign.Name), "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                        Me.Cursor = Windows.Forms.Cursors.Default
                        tmrAutosave.Enabled = enableAutosave
                        Exit Sub
                    End If
                End If
            End If
            Campaign.SaveCampaign(, True, , , , True)
            Me.Cursor = Windows.Forms.Cursors.Default
            tmrAutosave.Enabled = enableAutosave
            Windows.Forms.MessageBox.Show("Campaign was saved!", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If TrinitySettings.SaveCampaignsAsFiles Then
            SaveToFile()
        Else
            'SaveToFile()
            SaveToDB()
        End If
    End Sub

    Private Sub FileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileToolStripMenuItem.Click

    End Sub

    Private Sub OpenFromFile()
        'a open Campaign dialog

        'If Not Campaign.Name Is Nothing Then
        '    If System.Windows.Forms.MessageBox.Show("You are currently working on a campaign. " & _
        '                                            vbNewLine & "Save it before opening another?", "T R I N I T Y", _
        '                                            Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
        '        Campaign.SaveCampaign()
        '        System.Windows.Forms.MessageBox.Show("Saved!", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        '    End If
        'End If

        Campaign.ExtendedInfos.RemoveAll() 'resets extended infos

        'disables autosave function
        tmrAutosave.Enabled = False

        'sets a "open" dialog
        dlgDialog.Title = "Open campaign"
        dlgDialog.FileName = "*.cmp"
        dlgDialog.Filter = "Trinity campaigns|*.cmp|Trinity 3.0 campaigns|*.kmp"
        dlgDialog.FilterIndex = 0
        dlgDialog.CheckFileExists = True
        dlgDialog.InitialDirectory = TrinitySettings.CampaignFiles
        If dlgDialog.ShowDialog <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        OpenCampaign(dlgDialog.FileName)

        JustStarted = False
    End Sub

    Private Sub mnuDownloadDevVersion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDownloadDevVersion.Click

    End Sub



    Private Sub SendErrorReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SendErrorReportToolStripMenuItem.Click
        frmErrorReport.ShowDialog()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim tmpForm As New frmFirstUse
        tmpForm.ShowDialog()
    End Sub

    Private Sub HelpToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles HelpToolStripButton.Click



    End Sub

    Private Sub mnuEditPeople_Click(sender As System.Object, e As System.EventArgs) Handles mnuEditPeople.Click
        frmPeople.ShowDialog()
    End Sub

    Private Sub mnuMatrix_Click(sender As System.Object, e As System.EventArgs) Handles mnuMatrix.Click

    End Sub

    Private Sub InstalledPluginsToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles InstalledPluginsToolStripMenuItem.Click
        frmPlugins.ShowDialog()
    End Sub

    Dim weekCount As Integer = 0
    Private Sub RemoveSpottsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveSpottsToolStripMenuItem.Click
        If TrinitySettings.Developer Then
            Dim removeSpotts As New frmRemoveSpotts()
            removeSpotts.ShowDialog()
        End If
    End Sub
    Sub countWeeks()
        Dim tmpWeekCount As Integer = 0
        For Each tmpC As Trinity.cChannel In Campaign.Channels

        Next
    End Sub

    Private Sub cmdUpdateChannelsPricelists_Click(sender As Object, e As EventArgs) Handles cmdUpdateChannelsPricelists.Click
    End Sub
    Dim _progress As frmProgress
    Private _channels As Trinity.cChannels
    Private Sub updateAllPricelists()
        _channels = Campaign.Channels
        Saved = False
        If Windows.Forms.MessageBox.Show("Your pricelist will be updated from the server. All changes will be lost." & vbCrLf & vbCrLf & "Are you sure you want to continue?", "T R I N I T Y", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then Exit Sub
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        ' Sum the total amount of pricelist changes there are
        Dim totalCount As Integer = 0
        For Each TmpChan As Trinity.cChannel In _channels

            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                totalCount += 1
            Next
        Next

        Me._progress = New frmProgress
        Me._progress.MaxValue = totalCount
        Me._progress.Show()
        Me._progress.BarType = cProgressBarType.SingleBar

        For Each TmpChan As Trinity.cChannel In _channels

            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                Dim TmpNewBT As New Trinity.cBookingType(Campaign)
                TmpNewBT.ParentChannel = TmpBT.ParentChannel
                TmpNewBT.Name = TmpBT.Name
                TmpNewBT.Dayparts = TmpBT.Dayparts
                TmpNewBT.PricelistName = TmpBT.PricelistName

                ' Set max Values
                ' Me._progress.MaxValue = TmpNewBT.GetPricelistCount()
                'AddHandler TmpNewBT.OnPriceListUpdate, AddressOf Me.UpdateProgressBar
                '_progress.OverallMaxValue += 1
                _progress.Progress += 1
                _progress.Status = "Updating pricelist for " & TmpBT.ToString
                TmpNewBT.ReadPricelist()

                For Each TmpTarget As Trinity.cPricelistTarget In TmpBT.Pricelist.Targets
                    Dim TmpTarget2 As Trinity.cPricelistTarget = TmpNewBT.Pricelist.Targets(TmpTarget.TargetName)

                    If Not TmpTarget2 Is Nothing Then
                        TmpTarget.Target.NoUniverseSize = True
                        TmpTarget.Target.TargetName = TmpTarget2.Target.TargetName
                        TmpTarget.Target.TargetGroup = TmpTarget2.Target.TargetGroup
                        TmpTarget.Target.TargetType = TmpTarget2.Target.TargetType
                        TmpTarget.Target.NoUniverseSize = False
                        TmpTarget.CalcCPP = TmpTarget2.CalcCPP
                        TmpTarget.StandardTarget = TmpTarget2.StandardTarget
                        Select Case TmpTarget.IsEntered
                            Case Trinity.cPricelistTarget.EnteredEnum.eCPP
                                TmpTarget.NetCPP = TmpTarget.NetCPP
                            Case Trinity.cPricelistTarget.EnteredEnum.eCPT
                                TmpTarget.NetCPT = TmpTarget.NetCPT
                            Case Trinity.cPricelistTarget.EnteredEnum.eDiscount
                                TmpTarget.Discount = TmpTarget.Discount
                        End Select
                    End If

                    TmpTarget.PricelistPeriods.Clear()
                    TmpTarget.Indexes.Clear()
                    If Not TmpTarget2 Is Nothing Then
                        For Each TmpPeriod As Trinity.cPricelistPeriod In TmpTarget2.PricelistPeriods
                            With TmpTarget.PricelistPeriods.Add(TmpPeriod.Name, TmpPeriod.ID)
                                .FromDate = TmpPeriod.FromDate
                                .ToDate = TmpPeriod.ToDate
                                .PriceIsCPP = TmpPeriod.PriceIsCPP
                                If TmpTarget2.CalcCPP Then
                                    For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                                        .Price(TmpPeriod.PriceIsCPP, i) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, i)
                                    Next
                                Else
                                    .Price(TmpPeriod.PriceIsCPP) = TmpPeriod.Price(TmpPeriod.PriceIsCPP)
                                End If
                                .TargetNat = TmpPeriod.TargetNat
                                .TargetUni = TmpPeriod.TargetUni
                            End With
                        Next
                        For Each TmpIndex As Trinity.cIndex In TmpTarget2.Indexes
                            With TmpTarget.Indexes.Add(TmpIndex.Name)
                                .FromDate = TmpIndex.FromDate
                                .ToDate = TmpIndex.ToDate
                                .ID = TmpIndex.ID
                                .IndexOn = TmpIndex.IndexOn
                                .Index = TmpIndex.Index
                                .UseThis = True
                            End With
                        Next
                    End If
                Next

                For Each TmpTarget2 As Trinity.cPricelistTarget In TmpNewBT.Pricelist.Targets
                    Try
                        If Not TmpBT.Pricelist.Targets.Contains(TmpTarget2.TargetName) Then
                            Dim TmpTarget As Trinity.cPricelistTarget = TmpBT.Pricelist.Targets.Add(TmpTarget2.TargetName, TmpBT)
                            If Not TmpTarget2 Is Nothing Then
                                'TmpTarget.CPP = TmpTarget2.CPP
                                'TmpTarget.UniSize = TmpTarget2.UniSize
                                'TmpTarget.UniSizeNat = TmpTarget2.UniSizeNat
                                TmpTarget.Bookingtype = TmpBT
                                TmpTarget.Target.NoUniverseSize = True
                                TmpTarget.Target.TargetName = TmpTarget2.Target.TargetName
                                TmpTarget.Target.TargetGroup = TmpTarget2.Target.TargetGroup
                                TmpTarget.Target.TargetType = TmpTarget2.Target.TargetType
                                TmpTarget.Target.NoUniverseSize = False
                                TmpTarget.CalcCPP = TmpTarget2.CalcCPP
                                TmpTarget.StandardTarget = TmpTarget2.StandardTarget
                                Select Case TmpTarget.IsEntered
                                    Case Trinity.cPricelistTarget.EnteredEnum.eCPP
                                        TmpTarget.NetCPP = TmpTarget.NetCPP
                                    Case Trinity.cPricelistTarget.EnteredEnum.eCPT
                                        TmpTarget.NetCPT = TmpTarget.NetCPT
                                    Case Trinity.cPricelistTarget.EnteredEnum.eDiscount
                                        TmpTarget.Discount = TmpTarget.Discount
                                End Select
                            End If
                            TmpTarget.PricelistPeriods.Clear()
                            If Not TmpTarget2 Is Nothing Then
                                For Each TmpPeriod As Trinity.cPricelistPeriod In TmpTarget2.PricelistPeriods
                                    With TmpTarget.PricelistPeriods.Add(TmpPeriod.Name, TmpPeriod.ID)

                                        .FromDate = TmpPeriod.FromDate
                                        .ToDate = TmpPeriod.ToDate
                                        .PriceIsCPP = TmpPeriod.PriceIsCPP
                                        If TmpTarget2.CalcCPP Then
                                            For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                                                .Price(TmpPeriod.PriceIsCPP, i) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, i)
                                            Next
                                        Else
                                            .Price(TmpPeriod.PriceIsCPP) = TmpPeriod.Price(TmpPeriod.PriceIsCPP)
                                        End If
                                        .TargetNat = TmpPeriod.TargetNat
                                        .TargetUni = TmpPeriod.TargetUni
                                    End With
                                Next
                                For Each TmpIndex As Trinity.cIndex In TmpTarget2.Indexes
                                    With TmpTarget.Indexes.Add(TmpIndex.Name)
                                        .FromDate = TmpIndex.FromDate
                                        .ToDate = TmpIndex.ToDate
                                        .ID = TmpIndex.ID
                                        .IndexOn = TmpIndex.IndexOn
                                        .Index = TmpIndex.Index
                                        .UseThis = True
                                    End With
                                Next
                            End If
                        End If
                    Catch ex As Exception
                        ' Stop
                    End Try
                Next

                If Not TmpBT.BuyingTarget Is Nothing AndAlso Not TmpBT.BuyingTarget.TargetName Is Nothing AndAlso TmpBT.BuyingTarget.TargetName <> "" Then
                    TmpBT.BuyingTarget.Target.NoUniverseSize = True
                    TmpBT.BuyingTarget.Target.TargetName = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Target.TargetName
                    TmpBT.BuyingTarget.Target.TargetGroup = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Target.TargetGroup
                    TmpBT.BuyingTarget.Target.TargetType = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Target.TargetType
                    TmpBT.BuyingTarget.Target.NoUniverseSize = False
                    TmpBT.BuyingTarget.CalcCPP = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).CalcCPP
                    TmpBT.BuyingTarget.StandardTarget = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).StandardTarget

                    Select Case TmpBT.BuyingTarget.IsEntered
                        Case Trinity.cPricelistTarget.EnteredEnum.eCPP
                            TmpBT.BuyingTarget.NetCPP = TmpBT.BuyingTarget.NetCPP
                        Case Trinity.cPricelistTarget.EnteredEnum.eCPT
                            TmpBT.BuyingTarget.NetCPT = TmpBT.BuyingTarget.NetCPT
                        Case Trinity.cPricelistTarget.EnteredEnum.eDiscount
                            TmpBT.BuyingTarget.Discount = TmpBT.BuyingTarget.Discount
                    End Select
                    If Not TmpBT.BuyingTarget Is TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName) Then
                        TmpBT.BuyingTarget.PricelistPeriods.Clear()
                        For Each TmpPeriod As Trinity.cPricelistPeriod In TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).PricelistPeriods
                            With TmpBT.BuyingTarget.PricelistPeriods.Add(TmpPeriod.Name, TmpPeriod.ID)
                                .FromDate = TmpPeriod.FromDate
                                .ToDate = TmpPeriod.ToDate
                                .PriceIsCPP = TmpPeriod.PriceIsCPP
                                If TmpBT.BuyingTarget.CalcCPP Then
                                    For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                                        .Price(TmpPeriod.PriceIsCPP, i) = TmpPeriod.Price(TmpPeriod.PriceIsCPP, i)
                                    Next
                                Else
                                    .Price(TmpPeriod.PriceIsCPP) = TmpPeriod.Price(TmpPeriod.PriceIsCPP)
                                End If
                                .TargetNat = TmpPeriod.TargetNat
                                .TargetUni = TmpPeriod.TargetUni
                            End With
                        Next
                        Dim _notUsedIndexes As New List(Of Trinity.cIndex)
                        For Each _index As Trinity.cIndex In (From _idx As Trinity.cIndex In TmpBT.BuyingTarget.Indexes Where Not _idx.UseThis Select _idx)
                            _notUsedIndexes.Add(_index)
                        Next
                        TmpBT.BuyingTarget.Indexes.Clear()
                        For Each TmpIndex As Trinity.cIndex In TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Indexes
                            With TmpBT.BuyingTarget.Indexes.Add(TmpIndex.Name)
                                .FromDate = TmpIndex.FromDate
                                .ToDate = TmpIndex.ToDate
                                .ID = TmpIndex.ID
                                .IndexOn = TmpIndex.IndexOn
                                .Index = TmpIndex.Index
                                .UseThis = True
                            End With
                        Next
                        For Each _index As Trinity.cIndex In _notUsedIndexes
                            If TmpBT.BuyingTarget.Indexes(_index.ID) IsNot Nothing Then
                                TmpBT.BuyingTarget.Indexes(_index.ID).UseThis = False
                            End If
                        Next
                    End If
                End If

                'RemoveHandler TmpNewBT.OnPriceListUpdate, AddressOf Me.UpdateProgressBar
                'Me._progress.OverallAddOffset(TmpNewBT.GetPricelistCount())
            Next
        Next
        Me.Cursor = Windows.Forms.Cursors.Default

        Me._progress.Hide()
        Me._progress.Dispose()

    End Sub

    Private Sub UpdateChannelListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateChannelListToolStripMenuItem.Click
        Campaign.Channels.updateAllChannels(True)
        Windows.Forms.MessageBox.Show("Channel list is now updated.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ReloadAllPricelistsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReloadAllPricelistsToolStripMenuItem.Click
        updateAllPricelists()
    End Sub

    Private Sub DoBothToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DoBothToolStripMenuItem.Click
        If Campaign.IsStripped Then
            Campaign.ReloadDeletedChannels()
        Else
            Campaign.Channels.updateAllChannels(True)
        End If
        updateAllPricelists()
    End Sub
End Class

'Public Class clsExcelReport

'    Private Function CreateRecordsetFromDataGrid(ByVal DGV As Windows.Forms.DataGridView) As ADODB.Recordset

'        Dim rs As New ADODB.Recordset

'        'Create columns in ADODB.Recordset

'        Dim FieldAttr As ADODB.FieldAttributeEnum

'        FieldAttr = ADODB.FieldAttributeEnum.adFldIsNullable Or ADODB.FieldAttributeEnum.adFldIsNullable Or ADODB.FieldAttributeEnum.adFldUpdatable

'        For Each iColumn As Windows.Forms.DataGridViewColumn In DGV.Columns

'            'only add Visible columns

'            If iColumn.Visible = True Then

'                Dim FieldType As ADODB.DataTypeEnum

'                'select dataType

'                If iColumn.ValueType Is GetType(Boolean) Then

'                    FieldType = ADODB.DataTypeEnum.adBoolean

'                ElseIf iColumn.ValueType Is GetType(Byte) Then

'                    FieldType = ADODB.DataTypeEnum.adTinyInt

'                ElseIf iColumn.ValueType Is GetType(Int16) Then

'                    FieldType = ADODB.DataTypeEnum.adSmallInt

'                ElseIf iColumn.ValueType Is GetType(Int32) Then

'                    FieldType = ADODB.DataTypeEnum.adInteger

'                ElseIf iColumn.ValueType Is GetType(Int64) Then

'                    FieldType = ADODB.DataTypeEnum.adBigInt

'                ElseIf iColumn.ValueType Is GetType(Single) Then

'                    FieldType = ADODB.DataTypeEnum.adSingle

'                ElseIf iColumn.ValueType Is GetType(Double) Then

'                    FieldType = ADODB.DataTypeEnum.adDouble

'                ElseIf iColumn.ValueType Is GetType(Decimal) Then

'                    FieldType = ADODB.DataTypeEnum.adCurrency

'                ElseIf iColumn.ValueType Is GetType(DateTime) Then

'                    FieldType = ADODB.DataTypeEnum.adDBDate

'                ElseIf iColumn.ValueType Is GetType(Char) Then

'                    FieldType = ADODB.DataTypeEnum.adChar

'                ElseIf iColumn.ValueType Is GetType(String) Then

'                    FieldType = ADODB.DataTypeEnum.adVarWChar

'                End If

'                If FieldType = ADODB.DataTypeEnum.adVarWChar Then

'                    rs.Fields.Append(iColumn.Name, FieldType, 300)

'                Else

'                    rs.Fields.Append(iColumn.Name, FieldType)

'                End If

'                rs.Fields(iColumn.Name).Attributes = FieldAttr

'            End If

'        Next

'        'Opens the ADODB.Recordset

'        rs.Open()

'        'Inserts rows into the recordset

'        For Each iRow As Windows.Forms.DataGridViewRow In DGV.Rows

'            rs.AddNew()

'            For Each iColumn As Windows.Forms.DataGridViewColumn In DGV.Columns

'                'only add values for Visible columns

'                If iColumn.Visible = True Then

'                    If iRow.Cells(iColumn.Name).Value.ToString = "" Then

'                        If (rs(iColumn.Name).Attributes And ADODB.FieldAttributeEnum.adFldIsNullable) <> 0 Then

'                            rs(iColumn.Name).Value = DBNull.Value

'                        End If

'                    Else

'                        rs(iColumn.Name).Value = iRow.Cells(iColumn.Name).Value

'                    End If

'                End If

'            Next

'        Next

'        'Moves to the first record in recordset

'        If Not rs.BOF Then rs.MoveFirst()

'        Return rs

'    End Function

'    Public Sub openExcelReport(ByRef DGV As Windows.Forms.DataGridView, Optional ByVal bolSave As Boolean = False, Optional ByVal bolOpen As Boolean = True)

'        Dim xlApp As New CultureSafeExcel.Application

'        Dim xlBook As CultureSafeExcel.Workbook

'        Dim xlSheet As CultureSafeExcel.Worksheet

'        Dim rs As New ADODB.Recordset

'        Dim xlrow As Integer

'        Dim strColType() As String

'        Try

'            'opening connections to excel

'            xlBook = xlApp.AddWorkbook

'            xlSheet = xlBook.AddSheet

'            If bolOpen = True Then

'                xlApp.Visible = True

'            End If

'            xlrow = 1

'            Try

'                xlSheet.RawSheet.Columns.HorizontalAlignment = 2

'                'formating the output of the report

'                xlSheet.RawSheet.Columns.Font.Name = "Times New Roman"

'                xlSheet.RawSheet.Rows.Item(xlrow).Font.Bold = 1

'                xlSheet.RawSheet.Rows.Item(xlrow).Interior.ColorIndex = 15

'                rs = CreateRecordsetFromDataGrid(DGV)

'                If rs.State = 0 Then rs.Open()

'            Catch

'                GoTo PROC_EXIT

'            End Try

'            ReDim strColType(rs.Fields.Count)

'            For j As Integer = 0 To rs.Fields.Count - 1

'                xlSheet.AllCells.Item(xlrow, j + 1) = rs.Fields.Item(j).Name

'                xlSheet.Cells(xlrow, j + 1).BorderAround(1, ColorIndex:=16, Weight:=2)

'            Next j

'            'This does a simple test to see if the of excel held by the user is 2000 or later

'            'This is needed because "CopyFromRecordset" only works with excel 2000 or later

'            xlrow = 2

'            If Val(Mid(xlApp.Version, 1, InStr(1, xlApp.Version, ".") - 1)) > 8 Then

'                xlSheet.Range("A" & xlrow).CopyFromRecordset(rs)

'            Else

'                Windows.Forms.MessageBox.Show("You must use excel 2000 or above, opperation can not continue", "Excel Report", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error, Windows.Forms.MessageBoxDefaultButton.Button1)

'                GoTo PROC_EXIT

'            End If

'            Dim strSearch As String = ""

'            Dim strTemp As String = ""

'            For j As Integer = 0 To rs.Fields.Count - 1

'                'debug.Print(rs.Fields.Item(j).Name)

'                'debug.Print(rs.Fields.Item(j).Type.ToString) 'finds out the format of the field

'                If rs.Fields.Item(j).Type.ToString = "adBigInt" Then 'these if statements are used to select which format to use

'                    strSearch = "0;[Red]0"

'                ElseIf rs.Fields.Item(j).Type.ToString = "adVarChar" Then

'                    strSearch = "@"

'                ElseIf rs.Fields.Item(j).Type.ToString = "adDouble" Then 'type float/double

'                    strSearch = "0.00_ ;[Red]-0.00"

'                ElseIf rs.Fields.Item(j).Type.ToString = "adCurrency" Then 'money

'                    strTemp = j & " " & strTemp 'used to keep track of currency for sum

'                    strSearch = "€#,##0.00;[Red]-€#,##0.00"

'                ElseIf rs.Fields.Item(j).Type.ToString = "11" Then

'                    strSearch = "@"

'                ElseIf rs.Fields.Item(j).Type.ToString = "adDBTimeStamp" Then 'type date/time

'                    If rs.Fields.Item(j).Name.Contains("Date") = True Then

'                        strSearch = "[$-1809]ddd, dd-mmm-yyyy;@"

'                    Else

'                        strSearch = "h:mm AM/PM"

'                    End If

'                Else

'                    strSearch = "@"

'                End If

'                xlSheet.Columns(j + 1).NumberFormat = strSearch

'            Next j

'            If strTemp.Length > 0 Then

'                Dim arrayStr() As String = Split(strTemp, " ")

'                Dim X As Integer

'                Dim lngLenght As Integer

'                For i As Integer = UBound(arrayStr) To 0 Step -1

'                    'Asc (UCase$(Chr(KeyAscii)))

'                    lngLenght = InStr(1, strTemp, " ")

'                    If lngLenght > 0 Then

'                        X = CInt(Left(strTemp, lngLenght - 1))

'                        lngLenght = Len(strTemp) - lngLenght

'                        strTemp = Right(strTemp, lngLenght)

'                    End If

'                    'debug.Print X

'                    If X >= 26 Then

'                        X = X - 26

'                        arrayStr(i) = "A" & Chr(X + 65)

'                    Else

'                        arrayStr(i) = Chr(X + 65)

'                    End If

'                    'debug.Print arrayStr(i)

'                    xlSheet.AllCells.Item(CInt(rs.RecordCount + 6), arrayStr(i)).Font.Bold = 1

'                    xlSheet.AllCells.Item(CInt(rs.RecordCount + 6), arrayStr(i)) = "=SUM(" & arrayStr(i) & xlrow & ":" & arrayStr(i) & CInt(rs.RecordCount + 4) & ")"

'                Next i

'            End If

'            'some more formatting of the excel sheet. Thsi formats the way the

'            'sheet looks in print preview

'            xlSheet.PageSetup.LeftHeader = "&[Page]" & " of " & "&[Pages]"

'            xlSheet.PageSetup.RightHeader = "&[Date]" & " &[Time]"

'            xlSheet.PageSetup.HeaderMargin = 5

'            xlSheet.PageSetup.BottomMargin = 5

'            xlSheet.PageSetup.LeftMargin = 5

'            xlSheet.PageSetup.RightMargin = 5

'            xlSheet.PageSetup.TopMargin = 25

'            xlSheet.Columns.AutoFit()

'            xlSheet.Rows.AutoFit()

'            xlApp.UserControl = True

'            If bolOpen = False Then

'                xlApp.DisplayAlerts = False

'                xlBook.Close(True, My.Application.Info.DirectoryPath & "\Excel\AVCDDUpdate.xls")

'                xlApp.Application.Quit()

'            ElseIf bolSave = True Then

'                xlApp.DisplayAlerts = False

'                xlBook.SaveAs(My.Application.Info.DirectoryPath & "\Excel\AVCDDUpdate.xls")

'                xlApp.DisplayAlerts = True

'            End If

'        Catch ex As Exception

'            'Call errLogger(ex)

'        End Try

'PROC_EXIT:

'        xlSheet = Nothing

'        xlBook = Nothing

'        xlApp = Nothing

'    End Sub

'End Class
