Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic
Imports System.Xml

Namespace Trinity
    Public Class cContract

        Const VERSION As Integer = 1

        Public Costs As cCosts 'a collection of cost
        Public Name As String
        Private mvarChannels As ccontractChannels 'collection of channels
        Private _fromDate As Date
        Private _toDate As Date
        'Private campaign As cKampanj 'the campaign campaign
        Private mvarCombinations As cCombinations ' a collection of Combinations
        Private mvarPath As String
        Private mvarSaveDateTime As Date
        Private mvarIncludedSets() As String
        Private mvarMain As cKampanj

        Private mvarChannelPackage As String

        Public Event ApplyingContract(sender As Object, e As ContractEventArgs)

        Public Property ChannelPackage As String
            Get
                Return mvarChannelPackage
            End Get
            Set(ByVal value As String)
                mvarChannelPackage = value
            End Set
        End Property

        Private mvarDescription As String

        Public Property FromDate() As Date
            Get
                Return _fromDate
            End Get
            Set(ByVal value As Date)
                _fromDate = value
            End Set
        End Property

        Public ReadOnly Property SaveDateTime() As Date
            Get
                Return mvarSaveDateTime
            End Get
        End Property

        Sub ApplyToCampaign()
            Dim TmpBT As Trinity.cBookingType
            Dim _p As Integer = 0
            Dim _count As Integer = (From _chan As Trinity.cContractChannel In Channels From _bt As Trinity.cContractBookingtype In _chan.BookingTypes(_chan.ActiveContractLevel) Select _bt).Count
            For Each TmpC As Trinity.cContractChannel In Channels
                If MainObject.Channels(TmpC.ChannelName) IsNot Nothing Then
                    MainObject.Channels(TmpC.ChannelName).AgencyCommission = TmpC.Agencycommission
                End If
                For Each TmpCBT As Trinity.cContractBookingtype In TmpC.BookingTypes(TmpC.ActiveContractLevel)
                    _p += 1
                    RaiseEvent ApplyingContract(Me, New ContractEventArgs() With {.Bookingtype = TmpCBT, .Progress = (_p / _count) * 100})
                    If MainObject.Channels(TmpC.ChannelName) IsNot Nothing Then
                        TmpBT = MainObject.Channels(TmpC.ChannelName).BookingTypes(TmpCBT.Name)
                    Else
                        Exit For
                    End If

                    If TmpBT Is Nothing Then
                        Dim CampaignFilms As New List(Of Trinity.cFilm)

                        If MainObject.Channels(1).BookingTypes(1).Weeks.Count > 0 AndAlso MainObject.Channels(1).BookingTypes(1).Weeks(1).Films.Count > 0 Then
                            For Each f As Trinity.cFilm In MainObject.Channels(1).BookingTypes(1).Weeks(1).Films
                                CampaignFilms.Add(f)
                            Next
                        End If

                        'if it is a BT added in the cotnract we add it
                        If TmpCBT.IsContractBookingtype Then
                            TmpBT = MainObject.Channels(TmpC.ChannelName).BookingTypes.Add(TmpCBT.Name)
                            TmpBT.Shortname = TmpCBT.ShortName
                            TmpBT.PrintDayparts = TmpCBT.PrintDayparts
                            TmpBT.PrintBookingCode = TmpCBT.PrintBookingCode
                            TmpBT.IsRBS = TmpCBT.IsRBS
                            TmpBT.IsSpecific = TmpCBT.IsSpecific
                            TmpBT.MaxDiscount = TmpCBT.MaxDiscount

                            If MainObject.Channels(1).BookingTypes(1).Weeks.Count = 0 Then
                                For Each w As Trinity.cWeek In MainObject.Channels(2).BookingTypes(1).Weeks
                                    With TmpBT.Weeks.Add(w.Name)
                                        .Bookingtype = TmpBT
                                        .EndDate = w.EndDate
                                        .StartDate = w.StartDate
                                    End With
                                Next
                            Else
                                For Each w As Trinity.cWeek In MainObject.Channels(1).BookingTypes(1).Weeks
                                    With TmpBT.Weeks.Add(w.Name)
                                        .Bookingtype = TmpBT
                                        .EndDate = w.EndDate
                                        .StartDate = w.StartDate
                                        For Each film As Trinity.cFilm In CampaignFilms
                                            .Films.Add(film.FilmString)
                                            .Films(film.FilmString).Filmcode = film.Filmcode
                                            .Films(film.FilmString).Name = film.Name
                                        Next
                                    End With
                                Next
                            End If
                        Else
                            'if the bookingtype is missing we ask the user about it
                            'OPTIONS AVAILABLE
                            'Skip reading in the bookingtype
                            'Rename the bookingtype (add all the options to another BT since it has been renamed)

                            'create a list of available bookingtypes
                            Dim list As New List(Of String)
                            Dim found As Boolean = False
                            For Each TmpBT In MainObject.Channels(TmpC.ChannelName).BookingTypes
                                For Each TmpCBT2 As Trinity.cContractBookingtype In TmpC.BookingTypes(1)
                                    If TmpBT.Name = TmpCBT2.Name Then
                                        found = True
                                    End If
                                Next
                                If Not found Then
                                    list.Add(TmpBT.Name)
                                End If
                            Next
                            If list.Count = 0 Then
                                GoTo End_BookingType 'we skip this BT
                            Else
                                Dim intResult As Integer
                                With New OptionsPicker(list)
                                    .Text = "Bookingtype error"
                                    .lbl.Text = TmpCBT.ToString & " does not exist in your campaign."
                                    .ShowDialog()
                                    intResult = .DialogResult
                                End With

                                If intResult = Windows.Forms.DialogResult.No Then
                                    GoTo End_BookingType 'we skip this BT
                                Else
                                    TmpBT = MainObject.Channels(TmpC.ChannelName).BookingTypes(list(intResult - 1))
                                End If
                            End If
                        End If
                    End If

                    If TmpCBT.IsContractBookingtype Then
                        TmpBT.Shortname = TmpCBT.ShortName
                        TmpBT.IsRBS = TmpCBT.IsRBS
                        TmpBT.IsSpecific = TmpCBT.IsSpecific
                        TmpBT.PrintDayparts = TmpCBT.PrintDayparts
                        TmpBT.PrintBookingCode = TmpCBT.PrintBookingCode
                    End If
                    TmpBT.RatecardCPPIsGross = TmpCBT.RatecardCPPIsGross
                    TmpBT.MaxDiscount = TmpCBT.MaxDiscount
                    TmpBT.ParentChannel.AgencyCommission = TmpCBT.ParentChannel.Agencycommission
                    For i As Integer = 1 To 500
                        TmpBT.FilmIndex(i) = TmpCBT.FilmIndex(i)
                    Next
                    
                    TmpBT.Indexes.Clear()
                    If TmpCBT.Indexes.Count = 0 Then
                        'For Each TmpIndex As Trinity.cIndex In TmpBT.Indexes
                        'TmpBT.Indexes.Remove(TmpIndex.ID)
                        'Next
                    Else
                        For Each TmpIndex As Trinity.cIndex In TmpCBT.Indexes
                            Dim UseThis As Boolean = TrinitySettings.DefaultUseThis
                            Dim SaveEnh As New Dictionary(Of String, Trinity.cEnhancement)

                            If Not TmpBT.Indexes(TmpIndex.ID) Is Nothing Then
                                UseThis = TmpBT.Indexes(TmpIndex.ID).UseThis
                                For Each TmpEnh As Trinity.cEnhancement In TmpBT.Indexes(TmpIndex.ID).Enhancements
                                    SaveEnh.Add(TmpEnh.ID, TmpEnh)
                                Next
                                TmpBT.Indexes.Remove(TmpIndex.ID)
                            End If

                            'If TmpBT.Indexes.Count > TmpCBT.Indexes.Count 

                            'End If
                            'TmpBT.Indexes.Remove(TmpIndex.ID)

                            'Add all the indexes from the contract into the campaign again.. not desirable 
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
                                        .UseThis = TrinitySettings.DefaultUseThis
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
                    End If
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
                        If TmpCTarget.IsContractTarget Then
                        'If TmpCTarget.IsContractTarget OrElse (TmpBT.Pricelist.Targets.Count > 0 and TmpBT.Pricelist.Targets(TmpCTarget.TargetName) Is Nothing) Then
                            If TmpBT.Pricelist.Targets.Contains(TmpCTarget.TargetName) Then
                                TmpBT.Pricelist.Targets.Remove(TmpCTarget.TargetName)
                            End If
                            TmpTarget = TmpBT.Pricelist.Targets.Add(TmpCTarget.TargetName, TmpBT)

                            TmpTarget.CalcCPP = TmpCTarget.CalcCPP
                            TmpTarget.Target.TargetType = TmpCTarget.TargetType
                            TmpTarget.Target.TargetName = TmpCTarget.AdEdgeTargetName
                            TmpTarget.Bookingtype = MainObject.Channels(TmpC.ChannelName).BookingTypes(TmpCBT.Name)    '(TmpCTarget.Bookingtype.Name)
                            For Each TmpPeriod As Trinity.cPricelistPeriod In TmpCTarget.PricelistPeriods
                                Dim period As Trinity.cPricelistPeriod = TmpTarget.PricelistPeriods.Add(TmpPeriod.Name)
                                period.FromDate = TmpPeriod.FromDate
                                period.ToDate = TmpPeriod.ToDate
                                period.PriceIsCPP = TmpPeriod.PriceIsCPP
                                period.TargetNat = TmpPeriod.TargetNat
                                period.TargetUni = TmpPeriod.TargetUni
                                For j As Integer = 0 To TmpBT.Dayparts.Count - 1
                                    period.Price(j) = TmpPeriod.Price(j)
                                Next
                            Next
                        Else
                            TmpTarget = TmpBT.Pricelist.Targets(TmpCTarget.TargetName)
                        End If
                        If TmpTarget IsNot Nothing Then
                            Dim TmpCPP As Single = TmpBT.BuyingTarget.NetCPP
                            Dim Added As Boolean = False
                            Dim _buyingTargetSet As Boolean = False
                            For Each TmpIndex As Trinity.cIndex In TmpCTarget.Indexes
                                Dim UseThis As Boolean = False
                                Dim SaveEnh As New Dictionary(Of String, Trinity.cEnhancement)
                                If Not TmpTarget.Indexes(TmpIndex.ID) Is Nothing Then
                                    UseThis = TmpTarget.Indexes(TmpIndex.ID).UseThis
                                    For Each TmpEnh As Trinity.cEnhancement In TmpTarget.Indexes(TmpIndex.ID).Enhancements
                                        SaveEnh.Add(TmpEnh.ID, TmpEnh)
                                    Next
                                    TmpTarget.Indexes.Remove(TmpIndex.ID)
                                End If
                                If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eFixedCPP Then UseThis = True
                                'Add all the indexes from the contract into the campaign again.. not desirable 
                                With TmpTarget.Indexes.Add(TmpIndex.Name, TmpIndex.ID)
                                    .FromDate = TmpIndex.FromDate
                                    .ToDate = TmpIndex.ToDate
                                    .IndexOn = TmpIndex.IndexOn
                                    .Index = TmpIndex.Index
                                    .FixedCPP = TmpIndex.FixedCPP
                                    .SystemGenerated = False

                                    If TmpBT.BuyingTarget.TargetName = TmpTarget.TargetName Then
                                        If .IndexOn = Trinity.cIndex.IndexOnEnum.eFixedCPP Then
                                            If (.FromDate.ToOADate <= Campaign.EndDate AndAlso .ToDate.ToOADate >= Campaign.StartDate) Then
                                                If Not Added Then
                                                    TmpCPP = .FixedCPP
                                                    TmpCTarget.EnteredValue = TmpCPP
                                                    TmpBT.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP
                                                    Added = True
                                                End If
                                                If TmpBT.BuyingTarget.Indexes(.ID) Is Nothing Then
                                                    With TmpBT.BuyingTarget.Indexes.Add(.Name, .ID)
                                                        .FromDate = TmpIndex.FromDate
                                                        .ToDate = TmpIndex.ToDate
                                                        .IndexOn = TmpIndex.IndexOn
                                                        .Index = TmpIndex.Index
                                                        .FixedCPP = TmpIndex.FixedCPP
                                                        .SystemGenerated = False
                                                    End With
                                                End If
                                                TmpBT.BuyingTarget.Indexes(.ID).IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP
                                                TmpBT.BuyingTarget.Indexes(.ID).Index = (.FixedCPP / TmpCPP) * 100
                                                TmpBT.BuyingTarget.Indexes(.ID).UseThis = True

                                                .IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP
                                                .Index = (.FixedCPP / TmpCPP) * 100
                                                .UseThis = True
                                                _buyingTargetSet = True
                                            End If
                                        End If
                                    End If

                                    For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
                                        If Not TmpBT.Indexes(TmpIndex.ID) Is Nothing AndAlso Not TmpBT.Indexes(TmpIndex.ID).Enhancements(TmpEnh.ID) Is Nothing Then
                                            TmpBT.Indexes(TmpIndex.ID).Enhancements.Remove(TmpEnh.ID)
                                        End If
                                        With .Enhancements.Add
                                            .Amount = TmpEnh.Amount
                                            .ID = TmpEnh.ID
                                            .Name = TmpEnh.Name
                                            .UseThis = TrinitySettings.DefaultUseThis
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

                            'copy the daypart split
                            For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                                TmpTarget.DefaultDayPart(i) = TmpCTarget.DefaultDaypart(i)
                                'TmpTarget.DefaultDaypart(i) = TmpCTarget.DefaultDaypart(i)
                            Next

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
                            If Not _buyingTargetSet Then
                                If TmpTarget.TargetName = TmpBT.BuyingTarget.TargetName Then
                                    TmpBT.BuyingTarget.IsEntered = TmpTarget.IsEntered
                                    Select Case TmpTarget.IsEntered
                                        Case Is = Trinity.cContractTarget.EnteredEnum.eCPP
                                            TmpBT.BuyingTarget.NetCPP = TmpTarget.NetCPP
                                        Case Is = Trinity.cContractTarget.EnteredEnum.eCPT
                                            TmpBT.BuyingTarget.NetCPT = TmpTarget.NetCPT
                                        Case Is = Trinity.cContractTarget.EnteredEnum.eDiscount
                                            TmpBT.BuyingTarget.Discount = TmpTarget.Discount
                                    End Select
                                End If
                            End If
                        End If
                    Next

                    'set the buyingtarget again so it will be updated
                    If Not TmpBT.BuyingTarget.TargetName Is Nothing AndAlso TmpBT.BuyingTarget.TargetName <> "" Then
                        Select Case TmpBT.BuyingTarget.IsEntered
                            Case Trinity.cPricelistTarget.EnteredEnum.eCPP
                                TmpBT.BuyingTarget.NetCPP = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).NetCPP
                            Case Trinity.cPricelistTarget.EnteredEnum.eCPT
                                TmpBT.BuyingTarget.NetCPT = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).NetCPT
                            Case Trinity.cPricelistTarget.EnteredEnum.eDiscount
                                TmpBT.BuyingTarget.Discount = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName).Discount
                        End Select
                        'TmpBT.BuyingTarget = TmpBT.Pricelist.Targets(TmpBT.BuyingTarget.TargetName)
                    End If
End_BookingType:
                Next
            Next

            'For Each TmpChan In MainObject.Channels
            '    For Each TmpBT In TmpChan.BookingTypes
            '        If Not MainObject.Contract.Channels(TmpChan.ChannelName) Is Nothing AndAlso Not MainObject.Contract.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name) Is Nothing Then
            '            If MainObject.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Pricelist.Targets.Count > 0 Then
            '                For Each TmpAV As Trinity.cAddedValue In MainObject.Contract.Channels(TmpChan.ChannelName).BookingTypes(MainObject.Contract.Channels(TmpChan.ChannelName).ActiveContractLevel)(TmpBT.Name).AddedValues
            '                    If Not TmpBT.AddedValues(TmpAV.ID) Is Nothing Then
            '                        TmpBT.AddedValues.Remove(TmpAV.ID)
            '                    End If
            '                    With TmpBT.AddedValues.Add(TmpAV.Name, TmpAV.ID)
            '                        .IndexGross = TmpAV.IndexGross
            '                        .IndexNet = TmpAV.IndexNet
            '                    End With
            '                Next
            '                TmpBT.Pricelist = MainObject.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Pricelist
            '                TmpBT.BookIt = False
            '            End If
            '            TmpBT.Indexes = MainObject.Contract.Channels(TmpChan.ChannelName).BookingTypes(MainObject.Contract.Channels(TmpChan.ChannelName).ActiveContractLevel)(TmpBT.Name).Indexes
            '        End If
            '    Next
            'Next
            'update all labels and other information boxes about the new changes
            If MainObject.Costs.Count = 0 OrElse MainObject.Channels(1).BookingTypes(1).Weeks.Count = 0 Then
                MainObject.Costs = MainObject.Contract.Costs
            End If

            If Combinations.Count > 0 Then                
                For Each TmpCombo As Trinity.cCombination In Combinations
                    If Not MainObject.Combinations.ContainsName(TmpCombo.Name) Then
                        'Check that none of the channels in the combination already exists in a combination. If they do - skip this combo.
                        Dim _skipIt As Boolean = False
                        For Each _cc As cCombinationChannel In TmpCombo.Relations
                            If MainObject.Channels(_cc.ChannelName) IsNot Nothing AndAlso MainObject.Channels(_cc.ChannelName).BookingTypes(_cc.BookingTypeName) IsNot Nothing AndAlso MainObject.Channels(_cc.ChannelName).BookingTypes(_cc.BookingTypeName).Combination IsNot Nothing Then
                                _skipIt = True
                                Exit For
                            End If
                        Next
                        If Not _skipIt Then
                            With MainObject.Combinations.Add
                                .CombinationOn = TmpCombo.CombinationOn
                                .Name = TmpCombo.Name
                                .ID = TmpCombo.ID
                                .ShowAsOne = TmpCombo.ShowAsOne
                                For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                                    If MainObject.Channels(TmpCC.ChannelName) IsNot Nothing AndAlso MainObject.Channels(TmpCC.ChannelName).BookingTypes(TmpCC.BookingTypeName) IsNot Nothing Then
                                        TmpCC.Bookingtype = MainObject.Channels(TmpCC.ChannelName).BookingTypes(TmpCC.BookingTypeName)
                                        .Relations.Add(MainObject.Channels(TmpCC.ChannelName).BookingTypes(TmpCC.BookingTypeName), TmpCC.Relation)
                                        'Deprecated: ShowMe now derives from ShowAsOne. See Bookingtype.ShowMe
                                        '
                                        'If .ShowAsOne Then
                                        '    TmpCC.Bookingtype.ShowMe = False
                                        'Else
                                        '    TmpCC.Bookingtype.ShowMe = True
                                        'End If
                                    End If
                                Next
                            End With
                        End If
                    End If
                Next
            End If
        End Sub

        Public Property Description() As String
            Get
                On Error GoTo Commentary_Error
                Description = mvarDescription
                On Error GoTo 0
                Exit Property
Commentary_Error:
                Err.Raise(Err.Number, "cDescription: Description", Err.Description)
            End Get
            Set(ByVal Value As String)
                On Error GoTo Commentary_Error
                mvarDescription = Value
                On Error GoTo 0
                Exit Property
Commentary_Error:
                Err.Raise(Err.Number, "cContract: Description", Err.Description)
            End Set
        End Property

        Public Property Path() As String
            Get
                Return mvarPath
            End Get
            Set(ByVal value As String)
                mvarPath = value
            End Set
        End Property

        Public Property IncludedSets() As String()
            Get
                Return mvarIncludedSets
            End Get
            Set(ByVal value As String())
                mvarIncludedSets = value
            End Set
        End Property

        'Public WriteOnly Property MainObject() As cKampanj
        '    Set(ByVal value As cKampanj)
        '        campaign = value
        '    End Set
        'End Property

        Public Property Combinations() As cCombinations
            Get
                Return mvarCombinations
            End Get
            Set(ByVal value As cCombinations)
                mvarCombinations = value
            End Set
        End Property

        ReadOnly Property MainObject As cKampanj
            Get
                Return mvarMain
            End Get
        End Property

        Private Sub CreateChannels()
            Dim Cchan As Trinity.cContractChannel

            'if we have and old contract loaded we need to get the default film index from the files
            If mvarMain.Contract Is Nothing Then
                For Each TmpChan As Trinity.cChannel In mvarMain.Channels
                    Cchan = Me.Channels.Add(TmpChan.ChannelName, TmpChan.fileName)
                    Cchan.ListNumber = TmpChan.ListNumber
                    Cchan.Shortname = TmpChan.Shortname
                    Cchan.Agencycommission = TmpChan.AgencyCommission
                    Cchan.AddLevel() 'this adds all BTs aswell
                    Cchan.ActiveContractLevel = 1

                    Dim TmpBT As Trinity.cBookingType = TmpChan.BookingTypes(1)
                    For i As Integer = 1 To 500
                        Cchan.StandardFilmIndex(i) = mvarMain.Channels.DefaultFilmIndex(i)
                        For Each _bt As cContractBookingtype In Cchan.BookingTypes(1)
                            _bt.FilmIndex(i) = Cchan.StandardFilmIndex(i)
                        Next
                    Next
                Next
            Else
                For Each TmpChan As Trinity.cChannel In mvarMain.Channels
                    Cchan = Me.Channels.Add(TmpChan.ChannelName, TmpChan.fileName)
                    Cchan.ListNumber = TmpChan.ListNumber
                    Cchan.Shortname = TmpChan.Shortname
                    Cchan.AddLevel() 'this adds all BTs aswell
                    Cchan.ActiveContractLevel = 1

                    'get the default film index from the channels.xml file
                    Dim xmlDoc As New Xml.XmlDocument
                    xmlDoc.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & mvarMain.Area & "\" & TmpChan.fileName)

                    For Each XmlElement As XmlElement In xmlDoc.SelectNodes("//DefaultSpotIndex")(1).ChildNodes
                        Cchan.StandardFilmIndex(XmlElement.GetAttribute("Length")) = XmlElement.GetAttribute("Idx")
                    Next
                Next
            End If


            'denna skapar prislistorna etc, även levels ochj jiddrar

            'saves all channels from the XML files to a cChannels object

            'Dim XMLDoc As New Xml.XmlDocument
            'Dim XMLChannels As Xml.XmlElement
            'Dim XMLTmpNode As Xml.XmlElement
            'Dim XMLTmpNode2 As Xml.XmlElement
            'Dim XMLBookingTypes As Xml.XmlElement
            'Dim TmpChannel As cContractChannel
            'Dim TmpBT As cBookingType

            ''gets the path where the XML files are located
            'XMLDoc.Load(TrinitySettings.DataPath & Campaign.Area & "\Channels.xml")

            ''gets all the channels and booking types into a XML element
            'XMLChannels = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("Channels")

            ''selects the first channel tp the temp object
            'XMLTmpNode = XMLChannels.ChildNodes.Item(0)

            ''creates a new channel collection
            'Channels = New cChannels(Campaign)
            'Channels.MainObject = Campaign

            ''as long as there are a object in the temp variable
            'While Not XMLTmpNode Is Nothing
            '    'adds the channel to the collection
            '    TmpChannel = mvarChannels.Add(XMLTmpNode.GetAttribute("Name"), "", Campaign.Area)
            '    'selects the first booking type
            '    XMLBookingTypes = XMLTmpNode.SelectSingleNode("Bookingtypes")
            '    If XMLBookingTypes Is Nothing Then
            '        XMLBookingTypes = XMLDoc.GetElementsByTagName("Data").Item(0).SelectSingleNode("Bookingtypes")
            '    End If
            '    XMLTmpNode2 = XMLBookingTypes.ChildNodes.Item(0)

            '    'this loop goes through all available booking types for the selected channel
            '    While Not XMLTmpNode2 Is Nothing
            '        TmpBT = TmpChannel.BookingTypes.Add(XMLTmpNode2.GetAttribute("Name"), False)
            '        TmpBT.Shortname = XMLTmpNode2.GetAttribute("Shortname")
            '        TmpBT.IsRBS = XMLTmpNode2.GetAttribute("IsRBS")
            '        TmpBT.IsSpecific = XMLTmpNode2.GetAttribute("IsSpecific")
            '        XMLTmpNode2 = XMLTmpNode2.NextSibling
            '    End While
            '    'selects the first spot index
            '    XMLTmpNode2 = XMLTmpNode.GetElementsByTagName("SpotIndex").Item(0).FirstChild
            '    'the loop gets all availavle spot indexes
            '    While Not XMLTmpNode2 Is Nothing
            '        For Each TmpBT In TmpChannel.BookingTypes
            '            TmpBT.FilmIndex(XMLTmpNode2.GetAttribute("Length")) = XMLTmpNode2.GetAttribute("Idx")
            '        Next
            '        XMLTmpNode2 = XMLTmpNode2.NextSibling
            '    End While
            '    XMLTmpNode = XMLTmpNode.NextSibling
            'End While
            'now all the channels are set in the channels variable


        End Sub

        Public Shared Function GetDateTime(ByVal Path As String) As Date
            Dim XMLDoc As New Xml.XmlDocument
            Dim XMLContract As Xml.XmlElement

            XMLDoc.Load(Path)

            XMLContract = XMLDoc.GetElementsByTagName("Contract").Item(0)
            If XMLContract.GetAttribute("SaveDateTime") = "" Then
                If XMLContract.GetAttribute("LASTEDIT") <> "" Then
                    XMLContract.SetAttribute("SaveDateTime", XMLContract.GetAttribute("LASTEDIT"))
                Else
                    XMLContract.SetAttribute("SaveDateTime", Now)
                End If
                XMLDoc.Save(Path)
            End If
            Return XMLContract.GetAttribute("SaveDateTime")
        End Function

        Function Save(ByVal Path As String, Optional ByVal DoNotSaveToFile As Boolean = False, Optional ByVal SaveToDB As Boolean = False)
            'the function saves the contract to a XML file

            On Error Resume Next

            Dim i As Integer

            If Not Path = "" Then
                'this only appears if you save the contract FILE, not the contract in the campaign
                'Added this so if the contract file is updates by mistake it will show

                'If MsgBox("Please note:" & vbNewLine & _
                '"If you reached this dialog by any other way that pushing the save button in the CONTRACT WINDOW" & vbNewLine & _
                '" then please do not save and also contact the system administrators." & vbNewLine & vbNewLine & _
                '"Do you really wish to save your contract?", MsgBoxStyle.YesNo, "Important information") = MsgBoxResult.No Then
                '    Return "FALSE"
                'End If

                mvarPath = Path
            End If

            On Error GoTo 0

            Dim strLoadedChannelssets As String = ""
            'we check if there is a subset of channels loaded into the campaign
            Dim list As New List(Of String)
            For Each TmpChan As Trinity.cChannel In mvarMain.Channels
                If Not list.Contains(TmpChan.fileName) Then
                    list.Add(TmpChan.fileName)
                End If
            Next

            For i = 0 To list.Count - 1
                strLoadedChannelssets = strLoadedChannelssets & "," & list(i)
            Next

            Helper.WriteToLogFile("Init XML")
            Dim XMLDoc As New Xml.XmlDocument
            Dim XMLContract As Xml.XmlElement
            Dim XMLChannel As Xml.XmlElement
            Dim XMLBT As Xml.XmlElement
            Dim XMLIndexes As Xml.XmlElement
            Dim XMLIndex As Xml.XmlElement
            Dim TmpNode As Xml.XmlElement
            Dim XMLTargets As Xml.XmlElement
            Dim XMLTarget As Xml.XmlElement
            Dim Node

            Dim TmpCost As cCost
            Dim TmpChannel As cContractChannel
            Dim TmpBT As cContractBookingtype
            Dim TmpIndex As cIndex
            Dim TmpAV As cAddedValue
            Dim TmpTarget As cContractTarget



            Helper.WriteToLogFile("Start creating document")
            XMLDoc.PreserveWhitespace = True
            Node = XMLDoc.CreateProcessingInstruction("xml", "version='1.0'")
            XMLDoc.AppendChild(Node)

            Node = XMLDoc.CreateComment("Trinity contract.")
            XMLDoc.AppendChild(Node)

            XMLContract = XMLDoc.CreateElement("Contract")
            XMLDoc.AppendChild(XMLContract)
            XMLContract.SetAttribute("Version", VERSION)
            XMLContract.SetAttribute("Name", Name)
            XMLContract.SetAttribute("ChannelSets", strLoadedChannelssets.Substring(1))
            XMLContract.SetAttribute("Channelpackage", ChannelPackage)

            If Path = "" Then
                XMLContract.SetAttribute("Path", mvarPath)
            Else
                XMLContract.SetAttribute("Path", Path)
            End If
            If DoNotSaveToFile AndAlso Not SaveToDB Then
                XMLContract.SetAttribute("SaveDateTime", mvarSaveDateTime)
            Else
                XMLContract.SetAttribute("SaveDateTime", Now)
                mvarSaveDateTime = Now
            End If
            XMLContract.SetAttribute("From", FromDate)
            XMLContract.SetAttribute("To", ToDate)
            'add the user info
            If Not Path = "" Then
                XMLContract.SetAttribute("LASTEDIT", Date.Now.ToString)
                XMLContract.SetAttribute("PERSONEDITING", TrinitySettings.UserName)
                XMLContract.SetAttribute("PERSONEDITINGMail", TrinitySettings.UserEmail)
                XMLContract.SetAttribute("PERSONEDITINGPhone", TrinitySettings.UserPhoneNr)
            End If

            TmpNode = XMLDoc.CreateElement("Description")
            TmpNode.InnerText = Description
            XMLContract.AppendChild(TmpNode)

            Node = XMLDoc.CreateElement("Costs")
            For Each TmpCost In Costs
                TmpNode = XMLDoc.CreateElement("Node")
                TmpNode.SetAttribute("Name", TmpCost.CostName)
                TmpNode.SetAttribute("ID", TmpCost.ID)
                TmpNode.SetAttribute("Amount", TmpCost.Amount)
                TmpNode.SetAttribute("CostOn", TmpCost.CountCostOn)
                TmpNode.SetAttribute("CostType", TmpCost.CostType)
                TmpNode.SetAttribute("MarathonID", TmpCost.MarathonID)
                Node.appendChild(TmpNode)
            Next
            XMLContract.AppendChild(Node)

            Dim XMLCombos As Xml.XmlElement
            Dim XMLCombo As Xml.XmlElement
            Dim XMLComboChannel As Xml.XmlElement

            XMLCombos = XMLDoc.CreateElement("Combinations")
            For Each TmpCombo As Trinity.cCombination In mvarCombinations
                XMLCombo = XMLDoc.CreateElement("Combo")
                XMLCombo.SetAttribute("ID", TmpCombo.ID)
                XMLCombo.SetAttribute("Name", TmpCombo.Name)
                XMLCombo.SetAttribute("CombinationOn", TmpCombo.CombinationOn)
                XMLCombo.SetAttribute("ShowAsOne", TmpCombo.ShowAsOne)
                For Each TmpCC As Trinity.cCombinationChannel In TmpCombo.Relations
                    XMLComboChannel = XMLDoc.CreateElement("Channel")
                    XMLComboChannel.SetAttribute("ID", TmpCC.ID)
                    XMLComboChannel.SetAttribute("Chan", TmpCC.ChannelName)
                    XMLComboChannel.SetAttribute("BT", TmpCC.BookingTypeName)
                    XMLComboChannel.SetAttribute("Relation", TmpCC.Relation)
                    XMLCombo.AppendChild(XMLComboChannel)
                Next
                XMLCombos.AppendChild(XMLCombo)
            Next
            XMLContract.AppendChild(XMLCombos)

            Node = XMLDoc.CreateElement("Channels")
            For Each TmpChannel In Channels
                Dim Chan As XmlElement = XMLDoc.CreateElement("Channel")
                Chan.SetAttribute("Channel", TmpChannel.ChannelName)
                Chan.SetAttribute("ActiveLevel", TmpChannel.ActiveContractLevel)
                Chan.SetAttribute("Commission", TmpChannel.Agencycommission)

                Dim LevelsNode As XmlElement = XMLDoc.CreateElement("Levels")
                For Level As Integer = 1 To TmpChannel.LevelCount
                    Dim LevelNode As XmlElement = XMLDoc.CreateElement("Level")
                    LevelNode.SetAttribute("Level", Level)
                    For Each TmpBT In TmpChannel.BookingTypes(Level)
                        'we only save the used ones
                        If TmpBT.Indexes.Count = 0 AndAlso TmpBT.AddedValues.Count = 0 AndAlso TmpBT.ContractTargets.Count = 0 Then GoTo End_BT

                        TmpNode = XMLDoc.CreateElement("BookingType")
                        TmpNode.SetAttribute("Name", TmpBT.Name)
                        TmpNode.SetAttribute("ShortName", TmpBT.ShortName)
                        TmpNode.SetAttribute("IsRBS", TmpBT.IsRBS)
                        TmpNode.SetAttribute("IsSpecific", TmpBT.IsSpecific)
                        TmpNode.SetAttribute("IsPremium", TmpBT.IsPremium)
                        TmpNode.SetAttribute("PrintDayparts", TmpBT.PrintDayparts)
                        TmpNode.SetAttribute("PrintBookingCode", TmpBT.PrintBookingCode)
                        TmpNode.SetAttribute("IsConractBookingtype", TmpBT.IsContractBookingtype)
                        TmpNode.SetAttribute("RatecardIsGross", TmpBT.RatecardCPPIsGross)
                        TmpNode.SetAttribute("MaxDiscount", TmpBT.MaxDiscount)

                        TmpNode.SetAttribute("NegotiatedVolume", TmpBT.NegotiatedVolume)
                        TmpNode.SetAttribute("IsActive", TmpBT.Active)
                        TmpNode.SetAttribute("FromDate", TmpBT.ActiveFromDate)
                        XMLIndexes = XMLDoc.CreateElement("Indexes")
                        For Each TmpIndex In TmpBT.Indexes
                            XMLIndex = XMLDoc.CreateElement("Index")
                            XMLIndex.SetAttribute("ID", TmpIndex.ID)
                            XMLIndex.SetAttribute("Name", TmpIndex.Name)

                            If TmpIndex.Enhancements.Count = 0 Then
                                XMLIndex.SetAttribute("Index", TmpIndex.Index(0))
                            Else
                                Dim XMLEnhancements As Xml.XmlElement
                                XMLEnhancements = XMLDoc.CreateElement("Enhancements")
                                For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
                                    Dim XMLEnh As Xml.XmlElement = XMLDoc.CreateElement("Enhancement")
                                    XMLEnh.SetAttribute("ID", TmpEnh.ID)
                                    XMLEnh.SetAttribute("Name", TmpEnh.Name)
                                    XMLEnh.SetAttribute("Amount", TmpEnh.Amount)
                                    XMLEnhancements.AppendChild(XMLEnh)
                                Next
                                'XMLEnhancements.SetAttribute("SpecificFactor", TmpIndex.Enhancements.SpecificFactor)
                                XMLIndex.AppendChild(XMLEnhancements)
                            End If

                            XMLIndex.SetAttribute("Index", TmpIndex.Index(0))
                            XMLIndex.SetAttribute("IndexOn", TmpIndex.IndexOn)
                            XMLIndex.SetAttribute("SystemGenerated", TmpIndex.SystemGenerated)
                            XMLIndex.SetAttribute("FromDate", TmpIndex.FromDate)
                            XMLIndex.SetAttribute("ToDate", TmpIndex.ToDate)
                            XMLIndexes.AppendChild(XMLIndex)
                        Next
                        TmpNode.AppendChild(XMLIndexes)

                        XMLIndexes = XMLDoc.CreateElement("AddedValues")
                        For Each TmpAV In TmpBT.AddedValues
                            XMLIndex = XMLDoc.CreateElement("AddedValue")
                            XMLIndex.SetAttribute("ID", TmpAV.ID)
                            XMLIndex.SetAttribute("Name", TmpAV.Name)
                            XMLIndex.SetAttribute("IndexGross", TmpAV.IndexGross)
                            XMLIndex.SetAttribute("IndexNet", TmpAV.IndexNet)
                            XMLIndex.SetAttribute("ShowIn", TmpAV.ShowIn)
                            XMLIndexes.AppendChild(XMLIndex)
                        Next
                        TmpNode.AppendChild(XMLIndexes)
                        XMLIndexes = XMLDoc.CreateElement("SpotIndex")
                        For i = 0 To 500
                            If TmpBT.FilmIndex(i) > 0 Then
                                XMLIndex = XMLDoc.CreateElement("Index")
                                XMLIndex.SetAttribute("Length", i)
                                XMLIndex.SetAttribute("Idx", TmpBT.FilmIndex(i))
                                XMLIndexes.AppendChild(XMLIndex)
                            End If
                        Next
                        TmpNode.AppendChild(XMLIndexes)

                        XMLTargets = XMLDoc.CreateElement("Targets")
                        For Each TmpTarget In TmpBT.ContractTargets
                            XMLTarget = XMLDoc.CreateElement("Target")
                            XMLTarget.SetAttribute("Name", TmpTarget.TargetName)
                            XMLTarget.SetAttribute("Target", TmpTarget.AdEdgeTargetName)
                            XMLTarget.SetAttribute("CalcCPP", TmpTarget.CalcCPP)
                            XMLTarget.SetAttribute("IsEntered", TmpTarget.IsEntered)
                            XMLTarget.SetAttribute("Value", TmpTarget.EnteredValue)
                            XMLTarget.SetAttribute("IsConractTarget", TmpTarget.IsContractTarget)

                            If mvarMain.Channels(TmpChannel.ChannelName) IsNot Nothing AndAlso mvarMain.Channels(TmpChannel.ChannelName).BookingTypes(TmpBT.Name) IsNot Nothing Then
                                For i = 0 To mvarMain.Channels(TmpChannel.ChannelName).BookingTypes(TmpBT.Name).Dayparts.Count - 1
                                    XMLTarget.SetAttribute("DP" & i, TmpTarget.DefaultDaypart(i))
                                Next
                            Else
                                For i = 0 To mvarMain.Dayparts.Count - 1
                                    XMLTarget.SetAttribute("DP" & i, TmpTarget.DefaultDaypart(i))
                                Next
                            End If
                            XMLIndexes = XMLDoc.CreateElement("PricelistPeriods")
                            For Each TmpPeriod As cPricelistPeriod In TmpTarget.PricelistPeriods
                                XMLIndex = XMLDoc.CreateElement("Period")
                                XMLIndex.SetAttribute("ID", TmpPeriod.ID)
                                XMLIndex.SetAttribute("Name", TmpPeriod.Name)
                                XMLIndex.SetAttribute("isCPP", TmpPeriod.PriceIsCPP)
                                XMLIndex.SetAttribute("Price", TmpPeriod.Price(TmpPeriod.PriceIsCPP))
                                For i = 0 To mvarMain.Channels(TmpChannel.ChannelName).BookingTypes(TmpBT.Name).Dayparts.Count - 1
                                    XMLIndex.SetAttribute("PriceDP" & i, TmpPeriod.Price(TmpPeriod.PriceIsCPP, i))
                                Next
                                XMLIndex.SetAttribute("UniSize", TmpPeriod.TargetUni)
                                XMLIndex.SetAttribute("UniSizeNat", TmpPeriod.TargetNat)
                                XMLIndex.SetAttribute("FromDate", TmpPeriod.FromDate)
                                XMLIndex.SetAttribute("ToDate", TmpPeriod.ToDate)
                                XMLIndexes.AppendChild(XMLIndex)
                            Next
                            XMLTarget.AppendChild(XMLIndexes)
                            XMLIndexes = XMLDoc.CreateElement("Indexes")
                            For Each TmpIndex In TmpTarget.Indexes
                                XMLIndex = XMLDoc.CreateElement("Index")
                                XMLIndex.SetAttribute("ID", TmpIndex.ID)
                                XMLIndex.SetAttribute("Name", TmpIndex.Name)

                                If TmpIndex.Enhancements.Count = 0 Then
                                    XMLIndex.SetAttribute("Index", TmpIndex.Index(0))
                                Else
                                    Dim XMLEnhancements As Xml.XmlElement
                                    XMLEnhancements = XMLDoc.CreateElement("Enhancements")
                                    For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
                                        Dim XMLEnh As Xml.XmlElement = XMLDoc.CreateElement("Enhancement")
                                        XMLEnh.SetAttribute("ID", TmpEnh.ID)
                                        XMLEnh.SetAttribute("Name", TmpEnh.Name)
                                        XMLEnh.SetAttribute("Amount", TmpEnh.Amount)
                                        XMLEnhancements.AppendChild(XMLEnh)
                                    Next
                                    'XMLEnhancements.SetAttribute("SpecificFactor", TmpIndex.Enhancements.SpecificFactor)
                                    XMLIndex.AppendChild(XMLEnhancements)
                                End If

                                XMLIndex.SetAttribute("Index", TmpIndex.Index(0))
                                XMLIndex.SetAttribute("FixedCPP", TmpIndex.FixedCPP)
                                XMLIndex.SetAttribute("IndexOn", TmpIndex.IndexOn)
                                XMLIndex.SetAttribute("SystemGenerated", TmpIndex.SystemGenerated)
                                XMLIndex.SetAttribute("FromDate", TmpIndex.FromDate)
                                XMLIndex.SetAttribute("ToDate", TmpIndex.ToDate)
                                XMLIndexes.AppendChild(XMLIndex)
                            Next
                            XMLTarget.AppendChild(XMLIndexes)
                            XMLTargets.AppendChild(XMLTarget)
                        Next
                        TmpNode.AppendChild(XMLTargets)
                        LevelNode.AppendChild(TmpNode)
End_BT:
                    Next
                    LevelsNode.AppendChild(LevelNode)
                Next
                Chan.AppendChild(LevelsNode)
                Node.appendChild(Chan)
            Next
            XMLContract.AppendChild(Node)

            If SaveToDB Then
                DBReader.saveContract(Me, XMLDoc)
            ElseIf Not DoNotSaveToFile Then
                XMLDoc.Save(Path)
            End If
            Save = XMLDoc.OuterXml

        End Function

        Sub LoadOld(ByVal Path As String, Optional ByVal LoadXML As Boolean = False, Optional ByVal XML As String = "")
            'loads a contract from a XML file
            Dim XMLDoc As New Xml.XmlDocument
            Dim XMLContract As Xml.XmlElement
            Dim XMLChannel As Xml.XmlElement
            Dim XMLIndex As Xml.XmlElement
            Dim TmpNode As Xml.XmlElement

            Dim Chan As String
            Dim BT As String
            Dim i As Integer

            If LoadXML Then
                XMLDoc.LoadXml(XML)
            Else
                XMLDoc.Load(Path)
            End If

            XMLContract = XMLDoc.GetElementsByTagName("Contract").Item(0)

            Name = XMLContract.GetAttribute("Name")
            FromDate = XMLContract.GetAttribute("From")
            ToDate = XMLContract.GetAttribute("To")
            If Not XMLContract.GetAttribute("SaveDateTime") = "" Then
                mvarSaveDateTime = XMLContract.GetAttribute("SaveDateTime")
                mvarPath = XMLContract.GetAttribute("Path")
            End If
            TmpNode = XMLContract.GetElementsByTagName("Costs").Item(0).ChildNodes.Item(0)
            While Not TmpNode Is Nothing
                If TmpNode.GetAttribute("CostOn") = "" Then
                    Costs.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("CostType"), TmpNode.GetAttribute("Amount"), Nothing, TmpNode.GetAttribute("MarathonID"))
                Else
                    Costs.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("CostType"), TmpNode.GetAttribute("Amount"), TmpNode.GetAttribute("CostOn"), TmpNode.GetAttribute("MarathonID"))
                End If
                TmpNode = TmpNode.NextSibling
            End While

            Dim XMLCombo As Xml.XmlElement
            Dim XMLComboChannel As Xml.XmlElement

            If Not XMLContract.GetElementsByTagName("Combinations").Item(0) Is Nothing Then
                XMLCombo = XMLContract.GetElementsByTagName("Combinations").Item(0).FirstChild
                While Not XMLCombo Is Nothing
                    With mvarMain.Contract.Combinations.Add
                        .ID = XMLCombo.GetAttribute("ID")
                        .Name = XMLCombo.GetAttribute("Name")
                        .CombinationOn = XMLCombo.GetAttribute("CombinationOn")
                        If XMLCombo.GetAttribute("ShowAsOne") <> "" Then .ShowAsOne = XMLCombo.GetAttribute("ShowAsOne")
                        If Not XMLCombo.GetAttribute("ShowAsOne") = "" Then
                            .ShowAsOne = XMLCombo.GetAttribute("ShowAsOne")
                        End If
                        XMLComboChannel = XMLCombo.FirstChild
                        While Not XMLComboChannel Is Nothing
                            .Relations.Add(mvarMain.Channels(XMLComboChannel.GetAttribute("Chan")).BookingTypes(XMLComboChannel.GetAttribute("BT")), XMLComboChannel.GetAttribute("Relation"))
                            XMLComboChannel = XMLComboChannel.NextSibling
                        End While
                    End With
                    XMLCombo = XMLCombo.NextSibling
                End While
            End If

            XMLChannel = XMLDoc.GetElementsByTagName("Channels").Item(0).ChildNodes.Item(0)
            While Not XMLChannel Is Nothing
                If Not XMLChannel.GetAttribute("Name") Is Nothing AndAlso XMLChannel.GetAttribute("Name") <> "" Then
                    Chan = Left(XMLChannel.GetAttribute("Name"), InStr(XMLChannel.GetAttribute("Name"), " ") - 1)
                    BT = Mid(XMLChannel.GetAttribute("Name"), InStr(XMLChannel.GetAttribute("Name"), " ") + 1)
                Else
                    Chan = XMLChannel.GetAttribute("Chan")
                    BT = XMLChannel.GetAttribute("BT")
                End If

                'add the channel if it doesnt exists
                If Channels(Chan) Is Nothing AndAlso mvarMain.Channels(Chan) IsNot Nothing Then
                    Dim TmpChan As Trinity.cContractChannel = Channels.Add(Chan, "")
                    TmpChan.AddLevel()
                    TmpChan.ActiveContractLevel = 1
                End If

                If Not Channels(Chan) Is Nothing AndAlso Not Channels(Chan).BookingTypes(1)(BT) Is Nothing Then

                    Channels(Chan).BookingTypes(1)(BT).Active = True
                    Channels(Chan).BookingTypes(1)(BT).ActiveFromDate = Me.FromDate

                    If Not XMLChannel.GetAttribute("NegotiatedVolume") Is Nothing AndAlso Not XMLChannel.GetAttribute("NegotiatedVolume") = "" Then
                        Channels(Chan).BookingTypes(1)(BT).NegotiatedVolume = XMLChannel.GetAttribute("NegotiatedVolume")
                    End If
                    TmpNode = XMLChannel.GetElementsByTagName("Indexes").Item(0).FirstChild
                    While Not TmpNode Is Nothing
                        Channels(Chan).BookingTypes(1)(BT).Indexes.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("ID"))
                        Channels(Chan).BookingTypes(1)(BT).Indexes(TmpNode.GetAttribute("ID")).Name = TmpNode.GetAttribute("Name")
                        Channels(Chan).BookingTypes(1)(BT).Indexes(TmpNode.GetAttribute("ID")).FromDate = TmpNode.GetAttribute("FromDate")
                        Channels(Chan).BookingTypes(1)(BT).Indexes(TmpNode.GetAttribute("ID")).ToDate = TmpNode.GetAttribute("ToDate")
                        If TmpNode.GetAttribute("IndexDP0") = "" Then
                            Channels(Chan).BookingTypes(1)(BT).Indexes(TmpNode.GetAttribute("ID")).Index = TmpNode.GetAttribute("Value")
                        Else
                            For i = 0 To mvarMain.Channels(Chan).BookingTypes(BT).Dayparts.Count - 1
                                Channels(Chan).BookingTypes(1)(BT).Indexes(TmpNode.GetAttribute("ID")).Index(i) = TmpNode.GetAttribute("IndexDP" & i)
                            Next
                        End If
                        Channels(Chan).BookingTypes(1)(BT).Indexes(TmpNode.GetAttribute("ID")).IndexOn = TmpNode.GetAttribute("IndexOn")
                        Channels(Chan).BookingTypes(1)(BT).Indexes(TmpNode.GetAttribute("ID")).SystemGenerated = TmpNode.GetAttribute("SystemGenerated")
                        TmpNode = TmpNode.NextSibling
                    End While
                    TmpNode = XMLChannel.GetElementsByTagName("AddedValues").Item(0).FirstChild
                    While Not TmpNode Is Nothing
                        Channels(Chan).BookingTypes(1)(BT).AddedValues.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("ID"))
                        Channels(Chan).BookingTypes(1)(BT).AddedValues(TmpNode.GetAttribute("ID")).IndexGross = TmpNode.GetAttribute("IndexGross")
                        Channels(Chan).BookingTypes(1)(BT).AddedValues(TmpNode.GetAttribute("ID")).IndexNet = TmpNode.GetAttribute("IndexNet")
                        TmpNode = TmpNode.NextSibling
                    End While
                    TmpNode = XMLChannel.GetElementsByTagName("SpotIndex").Item(0).FirstChild
                    While Not TmpNode Is Nothing
                        Channels(Chan).BookingTypes(1)(BT).FilmIndex(TmpNode.GetAttribute("Length")) = TmpNode.GetAttribute("Idx")
                        TmpNode = TmpNode.NextSibling
                    End While
                    TmpNode = XMLChannel.GetElementsByTagName("Targets").Item(0).FirstChild
                    While Not TmpNode Is Nothing
                        Channels(Chan).BookingTypes(1)(BT).ContractTargets.Add(TmpNode.GetAttribute("Name"))
                        Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).AdEdgeTargetName = TmpNode.GetAttribute("Target")
                        Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).CalcCPP = TmpNode.GetAttribute("CalcCPP")
                        Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).IsEntered = TmpNode.GetAttribute("IsEntered")
                        For i = 0 To mvarMain.Channels(Chan).BookingTypes(BT).Dayparts.Count - 1
                            If TmpNode.GetAttribute("DP" & i) <> "" Then
                                Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).DefaultDaypart(i) = TmpNode.GetAttribute("DP" & i)
                                'Channels(Chan).BookingTypes(1)(BT).DaypartSplit(i) = TmpNode.GetAttribute("DP" & i)
                            End If
                        Next


                        'only get the prices if there is a contract target
                        If Not mvarMain.Channels(Chan).BookingTypes(BT).Pricelist.Targets.Contains(TmpNode.GetAttribute("Name")) Then
                            'the old version of pricelists
                            If TmpNode.GetElementsByTagName("Indexes").Count > 0 Then
                                If TmpNode.HasChildNodes Then
                                    XMLIndex = TmpNode.GetElementsByTagName("Indexes").Item(0).FirstChild
                                    While Not XMLIndex Is Nothing
                                        If Not mvarMain.Channels(Chan).BookingTypes(BT).Pricelist.Targets.Contains(XMLIndex.GetAttribute("ID")) Then
                                            Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).PricelistPeriods.Add(XMLIndex.GetAttribute("Name"), XMLIndex.GetAttribute("ID"))
                                            Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).PricelistPeriods(XMLIndex.GetAttribute("ID")).FromDate = XMLIndex.GetAttribute("FromDate")
                                            Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).PricelistPeriods(XMLIndex.GetAttribute("ID")).ToDate = XMLIndex.GetAttribute("ToDate")
                                            Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).PricelistPeriods(XMLIndex.GetAttribute("ID")).TargetNat = TmpNode.GetAttribute("UniSizeNat")
                                            Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).PricelistPeriods(XMLIndex.GetAttribute("ID")).TargetUni = TmpNode.GetAttribute("UniSize")

                                            If Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).CalcCPP Then
                                                For i = 0 To mvarMain.Channels(Chan).BookingTypes(BT).Dayparts.Count - 1
                                                    Dim cpp As String = TmpNode.GetAttribute("CPP")
                                                    Dim index As String = TmpNode.GetAttribute("IndexDP" & i)
                                                    If cpp = "" Then cpp = "0"
                                                    If index = "" Then index = "0"
                                                    Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).PricelistPeriods(XMLIndex.GetAttribute("ID")).Price(True, i) = index * cpp / 100
                                                Next
                                            Else
                                                Dim cpp As String = TmpNode.GetAttribute("CPP")
                                                Dim index As String = XMLIndex.GetAttribute("IndexDP0")
                                                If cpp = "" Then cpp = "0"
                                                If index = "" Then index = "0"
                                                Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).PricelistPeriods(XMLIndex.GetAttribute("ID")).Price(True) = cpp * index / 100
                                            End If
                                        End If
                                        XMLIndex = XMLIndex.NextSibling
                                    End While
                                End If
                            End If

                            If TmpNode.GetElementsByTagName("PricelistPeriods").Count > 0 Then
                                XMLIndex = TmpNode.GetElementsByTagName("PricelistPeriods").Item(0).FirstChild
                                While Not XMLIndex Is Nothing
                                    Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).PricelistPeriods.Add(XMLIndex.GetAttribute("Name"), XMLIndex.GetAttribute("ID"))
                                    Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).PricelistPeriods(XMLIndex.GetAttribute("ID")).FromDate = XMLIndex.GetAttribute("FromDate")
                                    Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).PricelistPeriods(XMLIndex.GetAttribute("ID")).ToDate = XMLIndex.GetAttribute("ToDate")
                                    Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).PricelistPeriods(XMLIndex.GetAttribute("ID")).TargetNat = XMLIndex.GetAttribute("UniSizeNat")
                                    Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).PricelistPeriods(XMLIndex.GetAttribute("ID")).TargetUni = XMLIndex.GetAttribute("UniSize")
                                    Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).PricelistPeriods(XMLIndex.GetAttribute("ID")).PriceIsCPP = XMLIndex.GetAttribute("isCPP")

                                    'enables the contractp ricelist editor.
                                    Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).IsContractTarget = True
                                    If Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).CalcCPP Then
                                        For i = 0 To mvarMain.Channels(Chan).BookingTypes(BT).Dayparts.Count - 1
                                            Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).PricelistPeriods(XMLIndex.GetAttribute("ID")).Price(XMLIndex.GetAttribute("isCPP"), i) = XMLIndex.GetAttribute("PriceDP" & i)
                                        Next
                                    Else
                                        If XMLIndex.GetAttribute("Price") = "" Then
                                            Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).PricelistPeriods(XMLIndex.GetAttribute("ID")).Price(XMLIndex.GetAttribute("isCPP")) = 0
                                        Else
                                            Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).PricelistPeriods(XMLIndex.GetAttribute("ID")).Price(XMLIndex.GetAttribute("isCPP")) = XMLIndex.GetAttribute("Price")
                                        End If
                                    End If
                                    XMLIndex = XMLIndex.NextSibling
                                End While
                            End If
                        End If

                        If Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
                            Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).EnteredValue = TmpNode.GetAttribute("Value")
                        ElseIf Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).IsEntered = cPricelistTarget.EnteredEnum.eCPT Then
                            Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).EnteredValue = TmpNode.GetAttribute("Value")
                        Else
                            Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).EnteredValue = TmpNode.GetAttribute("Value")
                        End If
                        'Channels(Chan).BookingTypes(1)(BT).ContractTargets(TmpNode.GetAttribute("Name")).Target.NoUniverseSize = False
                        TmpNode = TmpNode.NextSibling
                    End While
                End If
                XMLChannel = XMLChannel.NextSibling
            End While
            If mvarPath = "" Then
                mvarPath = Path
            End If
        End Sub

        Sub Load(ByVal Path As String, Optional ByVal LoadXML As Boolean = False, Optional ByVal XML As String = "")
            'loads a contract from a XML file
            Dim XMLDoc As New Xml.XmlDocument
            Dim XMLContract As Xml.XmlElement
            Dim XMLChannel As Xml.XmlElement
            Dim XMLChannels As Xml.XmlElement
            Dim XMLLevels As Xml.XmlElement
            Dim XMLLevel As Xml.XmlElement
            Dim XMLBT As Xml.XmlElement
            Dim XMLIndex As Xml.XmlElement
            Dim TmpNode As Xml.XmlElement

            Dim i As Integer


            If LoadXML Then
                XMLDoc.LoadXml(XML)
            Else
                XMLDoc.Load(Path)
            End If


            Channels = New Trinity.cContractChannels(Campaign)

            XMLContract = XMLDoc.GetElementsByTagName("Contract").Item(0)

            'we check is all used chennel sub sets is loaded
            '(we find a channel from the correct sub set)            
            Dim strChannelSets As String = XMLContract.GetAttribute("ChannelSets")

            If strChannelSets = "" Then
                LoadOld(Path, LoadXML, XML)
                Exit Sub
            End If

            Dim list() As String = strChannelSets.Split(",")
            Dim found As Boolean
            For i = 0 To list.Length - 1
                found = False
                For Each TmpChan As Trinity.cChannel In mvarMain.Channels
                    If TmpChan.fileName = list(i) Then
                        found = True
                        Exit For
                    End If
                Next
                If Not found Then
                    MsgBox("The Channel set " & list(i).Substring(0, list(i).Length - 4) & " needs to be loaded into the campaign if you want to use this contract.", MsgBoxStyle.Information, "Channelset Missing")
                    Exit Sub
                End If
            Next

            Name = XMLContract.GetAttribute("Name")
            FromDate = XMLContract.GetAttribute("From")
            ToDate = XMLContract.GetAttribute("To")

            If Not XMLContract.GetAttribute("SaveDateTime") = "" Then
                mvarSaveDateTime = XMLContract.GetAttribute("SaveDateTime")
                mvarPath = XMLContract.GetAttribute("Path")
            End If

            If Not XMLContract.GetElementsByTagName("Description").Count = 0 Then
                TmpNode = XMLContract.GetElementsByTagName("Description").Item(0)
                Description = TmpNode.InnerText
            End If

            TmpNode = XMLContract.GetElementsByTagName("Costs").Item(0).ChildNodes.Item(0)
            While Not TmpNode Is Nothing
                If TmpNode.GetAttribute("CostOn") = "" Then
                    Costs.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("CostType"), TmpNode.GetAttribute("Amount"), Nothing, TmpNode.GetAttribute("MarathonID"))
                Else
                    Costs.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("CostType"), TmpNode.GetAttribute("Amount"), TmpNode.GetAttribute("CostOn"), TmpNode.GetAttribute("MarathonID"))
                End If
                TmpNode = TmpNode.NextSibling
            End While

            Dim XMLCombo As Xml.XmlElement
            Dim XMLComboChannel As Xml.XmlElement

            XMLChannels = XMLDoc.GetElementsByTagName("Channels").Item(0)
            XMLChannel = XMLChannels.FirstChild

            While Not XMLChannel Is Nothing
                Dim TmpChan As Trinity.cContractChannel

                TmpChan = Channels.Add(XMLChannel.GetAttribute("Channel"), "")

                Try
                    TmpChan.Agencycommission = CSng(XMLChannel.GetAttribute("Commission"))
                Catch ex As Exception
                    If mvarMain.Channels(TmpChan.ChannelName) Is Nothing Then
                        TmpChan.Agencycommission = 0
                    Else
                        TmpChan.Agencycommission = mvarMain.Channels(TmpChan.ChannelName).AgencyCommission
                    End If
                End Try

                XMLLevels = XMLChannel.FirstChild
                XMLLevel = XMLLevels.FirstChild
                While Not XMLLevel Is Nothing
                    Dim Level As Integer = XMLLevel.GetAttribute("Level")

                    'adds a level
                    TmpChan.AddEmptyLevel()

                    XMLBT = XMLLevel.FirstChild
                    While Not XMLBT Is Nothing
                        Dim TmpBT As Trinity.cContractBookingtype = TmpChan.BookingTypes(Level).Add(XMLBT.GetAttribute("Name"))
                        TmpBT.ParentChannel = TmpChan
                        If XMLBT.GetAttribute("RatecardIsGross") IsNot Nothing AndAlso XMLBT.GetAttribute("RatecardIsGross") <> "" Then
                            TmpBT.RatecardCPPIsGross = XMLBT.GetAttribute("RatecardIsGross")
                        End If
                        If XMLBT.GetAttribute("MaxDiscount") IsNot Nothing AndAlso XMLBT.GetAttribute("MaxDiscount") <> "" Then
                            TmpBT.MaxDiscount = XMLBT.GetAttribute("MaxDiscount")
                        End If
                        TmpBT.NegotiatedVolume = XMLBT.GetAttribute("NegotiatedVolume")

                        If XMLBT.GetAttribute("IsConractBookingtype") = "True" Then
                            TmpBT.ShortName = XMLBT.GetAttribute("ShortName")
                            TmpBT.IsRBS = XMLBT.GetAttribute("IsRBS")
                            TmpBT.IsSpecific = XMLBT.GetAttribute("IsSpecific")
                            TmpBT.IsPremium = XMLBT.GetAttribute("IsPremium")
                            TmpBT.PrintDayparts = XMLBT.GetAttribute("PrintDayparts")
                            TmpBT.PrintBookingCode = XMLBT.GetAttribute("PrintBookingCode")
                            TmpBT.IsContractBookingtype = True
                        Else
                            TmpBT.IsContractBookingtype = False
                            If XMLBT.GetAttribute("IsConractBookingtype") = "" Then
                                'a fix since we added these options after contracts where initially saved
                                '
                                'We copy the information from the channels into the contract channels
                                If Not mvarMain.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name) Is Nothing Then
                                    TmpBT.ShortName = mvarMain.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Shortname
                                    TmpBT.IsRBS = mvarMain.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).IsRBS
                                    TmpBT.IsSpecific = mvarMain.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).IsSpecific
                                    TmpBT.PrintDayparts = mvarMain.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).PrintDayparts
                                    TmpBT.PrintBookingCode = mvarMain.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).PrintBookingCode
                                End If
                            Else
                                TmpBT.ShortName = XMLBT.GetAttribute("ShortName")
                                TmpBT.IsRBS = XMLBT.GetAttribute("IsRBS")
                                TmpBT.IsSpecific = XMLBT.GetAttribute("IsSpecific")
                                If XMLBT.GetAttribute("IsPremium") = "" Then
                                    TmpBT.IsPremium = False
                                Else
                                    TmpBT.IsPremium = True
                                End If

                                TmpBT.PrintDayparts = XMLBT.GetAttribute("PrintDayparts")
                                TmpBT.PrintBookingCode = XMLBT.GetAttribute("PrintBookingCode")
                            End If
                        End If

                        TmpNode = XMLBT.GetElementsByTagName("Indexes").Item(0).FirstChild
                        TmpBT.Indexes = New Trinity.cIndexes(Campaign, TmpBT)
                        While Not TmpNode Is Nothing
                            TmpBT.Indexes.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("ID"))
                            TmpBT.Indexes(TmpNode.GetAttribute("ID")).Name = TmpNode.GetAttribute("Name")
                            TmpBT.Indexes(TmpNode.GetAttribute("ID")).FromDate = TmpNode.GetAttribute("FromDate")
                            TmpBT.Indexes(TmpNode.GetAttribute("ID")).ToDate = TmpNode.GetAttribute("ToDate")

                            If TmpNode.GetElementsByTagName("Enhancements").Count > 0 Then

                                Dim TmpENode As Xml.XmlElement = TmpNode.GetElementsByTagName("Enhancements").Item(0)
                                'TmpBT.Indexes(TmpNode.GetAttribute("ID")).Enhancements.SpecificFactor = TmpENode.GetAttribute("SpecificFactor")
                                TmpENode = TmpENode.FirstChild
                                While Not TmpENode Is Nothing
                                    With TmpBT.Indexes(TmpNode.GetAttribute("ID")).Enhancements.Add()
                                        .ID = TmpENode.GetAttribute("ID")
                                        .Name = TmpENode.GetAttribute("Name")
                                        .Amount = TmpENode.GetAttribute("Amount")
                                    End With
                                    TmpENode = TmpENode.NextSibling
                                End While
                            Else
                                TmpBT.Indexes(TmpNode.GetAttribute("ID")).Index = TmpNode.GetAttribute("Index")
                            End If

                            TmpBT.Indexes(TmpNode.GetAttribute("ID")).IndexOn = TmpNode.GetAttribute("IndexOn")
                            TmpBT.Indexes(TmpNode.GetAttribute("ID")).SystemGenerated = TmpNode.GetAttribute("SystemGenerated")
                            TmpNode = TmpNode.NextSibling
                        End While
                        TmpNode = XMLBT.GetElementsByTagName("AddedValues").Item(0).FirstChild
                        While Not TmpNode Is Nothing
                            TmpBT.AddedValues.Add(TmpNode.GetAttribute("Name"), TmpNode.GetAttribute("ID"))
                            TmpBT.AddedValues(TmpNode.GetAttribute("ID")).IndexGross = TmpNode.GetAttribute("IndexGross")
                            TmpBT.AddedValues(TmpNode.GetAttribute("ID")).IndexNet = TmpNode.GetAttribute("IndexNet")
                            If Not TmpNode.GetAttribute("ShowIn") = "" Then
                                If TmpNode.GetAttribute("ShowIn") = 0 Then
                                    TmpBT.AddedValues(TmpNode.GetAttribute("ID")).ShowIn = cAddedValue.ShowInEnum.siBoth
                                ElseIf TmpNode.GetAttribute("ShowIn") = 1 Then
                                    TmpBT.AddedValues(TmpNode.GetAttribute("ID")).ShowIn = cAddedValue.ShowInEnum.siAllocate
                                ElseIf TmpNode.GetAttribute("ShowIn") = 2 Then
                                    TmpBT.AddedValues(TmpNode.GetAttribute("ID")).ShowIn = cAddedValue.ShowInEnum.siBooking
                                End If
                            End If

                            TmpNode = TmpNode.NextSibling
                        End While

                        For indexCounter As Integer = 0 To 500
                            If mvarMain.Channels.DefaultFilmIndex(indexCounter) > 0 Then
                                TmpBT.FilmIndex(indexCounter) = mvarMain.Channels.DefaultFilmIndex(indexCounter)
                            End If
                        Next
                        TmpNode = XMLBT.GetElementsByTagName("SpotIndex").Item(0).FirstChild
                        While Not TmpNode Is Nothing
                            TmpBT.FilmIndex(TmpNode.GetAttribute("Length")) = TmpNode.GetAttribute("Idx")
                            TmpNode = TmpNode.NextSibling
                        End While
                        TmpNode = XMLBT.GetElementsByTagName("Targets").Item(0).FirstChild
                        While Not TmpNode Is Nothing
                            Dim TmpTarget As Trinity.cContractTarget = TmpBT.ContractTargets.Add(TmpNode.GetAttribute("Name"))
                            TmpTarget.CalcCPP = TmpNode.GetAttribute("CalcCPP")
                            TmpTarget.IsEntered = TmpNode.GetAttribute("IsEntered")
                            TmpTarget.IsContractTarget = TmpNode.GetAttribute("IsConractTarget")

                            If InStr(TmpNode.GetAttribute("Target"), "A") > 0 Then
                                TmpTarget.AdEdgeTargetName = TmpNode.GetAttribute("Target").Replace("A", "").Replace(" ", "").Replace(" ", "")
                            Else
                                TmpTarget.AdEdgeTargetName = TmpNode.GetAttribute("Target")
                            End If
                            If mvarMain.Channels(TmpChan.ChannelName) IsNot Nothing AndAlso mvarMain.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name) IsNot Nothing Then
                                For i = 0 To mvarMain.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Dayparts.Count - 1
                                    If TmpNode.GetAttribute("DP" & i) <> "" Then
                                        TmpTarget.DefaultDaypart(i) = TmpNode.GetAttribute("DP" & i)
                                    End If
                                Next
                            Else
                                For i = 0 To mvarMain.Dayparts.Count - 1
                                    If TmpNode.GetAttribute("DP" & i) <> "" Then
                                        TmpTarget.DefaultDaypart(i) = TmpNode.GetAttribute("DP" & i)
                                    End If
                                Next
                            End If

                            If TmpNode.GetElementsByTagName("PricelistPeriods").Count > 0 Then
                                XMLIndex = TmpNode.GetElementsByTagName("PricelistPeriods").Item(0).FirstChild
                                While Not XMLIndex Is Nothing
                                    Dim TmpPeriod As Trinity.cPricelistPeriod = TmpTarget.PricelistPeriods.Add(XMLIndex.GetAttribute("Name"), XMLIndex.GetAttribute("ID"))
                                    TmpPeriod.FromDate = XMLIndex.GetAttribute("FromDate")
                                    TmpPeriod.ToDate = XMLIndex.GetAttribute("ToDate")
                                    TmpPeriod.TargetNat = XMLIndex.GetAttribute("UniSizeNat")
                                    TmpPeriod.TargetUni = XMLIndex.GetAttribute("UniSize")
                                    TmpPeriod.PriceIsCPP = XMLIndex.GetAttribute("isCPP")
                                    If TmpTarget.CalcCPP Then
                                        For i = 0 To mvarMain.Channels(TmpChan.ChannelName).BookingTypes(TmpBT.Name).Dayparts.Count - 1
                                            TmpPeriod.Price(XMLIndex.GetAttribute("isCPP"), i) = XMLIndex.GetAttribute("PriceDP" & i)
                                        Next
                                    Else
                                        If XMLIndex.GetAttribute("Price") = "" Then
                                            TmpPeriod.Price(XMLIndex.GetAttribute("isCPP")) = 0
                                        Else
                                            TmpPeriod.Price(XMLIndex.GetAttribute("isCPP")) = XMLIndex.GetAttribute("Price")
                                        End If
                                    End If
                                    XMLIndex = XMLIndex.NextSibling
                                End While
                            End If
                            TmpTarget.Indexes = New Trinity.cIndexes(Campaign, TmpTarget)
                            If TmpNode.GetElementsByTagName("Indexes") IsNot Nothing AndAlso TmpNode.GetElementsByTagName("Indexes").Count > 0 Then
                                Dim TmpIndexNode As Xml.XmlElement = TmpNode.GetElementsByTagName("Indexes").Item(0).FirstChild
                                While Not TmpIndexNode Is Nothing
                                    TmpTarget.Indexes.Add(TmpIndexNode.GetAttribute("Name"), TmpIndexNode.GetAttribute("ID"))
                                    TmpTarget.Indexes(TmpIndexNode.GetAttribute("ID")).Name = TmpIndexNode.GetAttribute("Name")
                                    TmpTarget.Indexes(TmpIndexNode.GetAttribute("ID")).FromDate = TmpIndexNode.GetAttribute("FromDate")
                                    TmpTarget.Indexes(TmpIndexNode.GetAttribute("ID")).ToDate = TmpIndexNode.GetAttribute("ToDate")

                                    If TmpIndexNode.GetElementsByTagName("Enhancements").Count > 0 Then

                                        Dim TmpENode As Xml.XmlElement = TmpIndexNode.GetElementsByTagName("Enhancements").Item(0)
                                        'TmpBT.Indexes(TmpNode.GetAttribute("ID")).Enhancements.SpecificFactor = TmpENode.GetAttribute("SpecificFactor")
                                        TmpENode = TmpENode.FirstChild
                                        While Not TmpENode Is Nothing
                                            With TmpTarget.Indexes(TmpIndexNode.GetAttribute("ID")).Enhancements.Add()
                                                .ID = TmpENode.GetAttribute("ID")
                                                .Name = TmpENode.GetAttribute("Name")
                                                .Amount = TmpENode.GetAttribute("Amount")
                                            End With
                                            TmpENode = TmpENode.NextSibling
                                        End While
                                    Else
                                        TmpTarget.Indexes(TmpIndexNode.GetAttribute("ID")).Index = TmpIndexNode.GetAttribute("Index")
                                    End If
                                    If TmpIndexNode.GetAttribute("FixedCPP") <> "" Then
                                        TmpTarget.Indexes(TmpIndexNode.GetAttribute("ID")).FixedCPP = TmpIndexNode.GetAttribute("FixedCPP")
                                    End If

                                    TmpTarget.Indexes(TmpIndexNode.GetAttribute("ID")).IndexOn = TmpIndexNode.GetAttribute("IndexOn")
                                    TmpTarget.Indexes(TmpIndexNode.GetAttribute("ID")).SystemGenerated = TmpIndexNode.GetAttribute("SystemGenerated")
                                    TmpIndexNode = TmpIndexNode.NextSibling
                                End While
                            End If
                            TmpTarget.EnteredValue = TmpNode.GetAttribute("Value")

                            TmpNode = TmpNode.NextSibling
                        End While

                        TmpBT.Active = XMLBT.GetAttribute("IsActive")
                        If Not XMLBT.GetAttribute("FromDate") = "" Then
                            TmpBT.ActiveFromDate = XMLBT.GetAttribute("FromDate")
                        End If

                        XMLBT = XMLBT.NextSibling
                    End While

                    XMLLevel = XMLLevel.NextSibling
                End While

                TmpChan.ActiveContractLevel = XMLChannel.GetAttribute("ActiveLevel")

                XMLChannel = XMLChannel.NextSibling
            End While
            If Not XMLContract.GetElementsByTagName("Combinations").Item(0) Is Nothing Then
                XMLCombo = XMLContract.GetElementsByTagName("Combinations").Item(0).FirstChild
                While Not XMLCombo Is Nothing
                    With mvarMain.Contract.Combinations.Add
                        .ID = XMLCombo.GetAttribute("ID")
                        .Name = XMLCombo.GetAttribute("Name")
                        .CombinationOn = XMLCombo.GetAttribute("CombinationOn")
                        If XMLCombo.GetAttribute("ShowAsOne") <> "" Then .ShowAsOne = XMLCombo.GetAttribute("ShowAsOne")
                        If Not XMLCombo.GetAttribute("ShowAsOne") = "" Then
                            .ShowAsOne = XMLCombo.GetAttribute("ShowAsOne")
                        End If
                        XMLComboChannel = XMLCombo.FirstChild
                        While Not XMLComboChannel Is Nothing
                            If mvarMain.Channels(XMLComboChannel.GetAttribute("Chan")) Isnot nothing
                                If Channels(XMLComboChannel.GetAttribute("Chan")) IsNot Nothing AndAlso Channels(XMLComboChannel.GetAttribute("Chan")).BookingTypes(1)(XMLComboChannel.GetAttribute("BT")) Is Nothing Then
                                    If mvarMain.Channels(XMLComboChannel.GetAttribute("Chan")).BookingTypes(XMLComboChannel.GetAttribute("BT")) IsNot Nothing Then
                                        Dim TmpBT As Trinity.cBookingType = mvarMain.Channels(XMLComboChannel.GetAttribute("Chan")).BookingTypes(XMLComboChannel.GetAttribute("BT"))
                                        For i = 1 To mvarMain.Contract.Channels(XMLComboChannel.GetAttribute("Chan")).LevelCount
                                            With mvarMain.Contract.Channels(XMLComboChannel.GetAttribute("Chan")).BookingTypes(i).Add(XMLComboChannel.GetAttribute("BT"))
                                                .ShortName = TmpBT.Shortname
                                                .IsRBS = TmpBT.IsRBS
                                                .IsSpecific = TmpBT.IsSpecific
                                                .PrintDayparts = TmpBT.PrintDayparts
                                                .PrintBookingCode = TmpBT.PrintBookingCode
                                                .ParentChannel = Channels(XMLComboChannel.GetAttribute("Chan"))
                                                .Indexes = New Trinity.cIndexes(Campaign, mvarMain.Contract.Channels(XMLComboChannel.GetAttribute("Chan")).BookingTypes(i)(XMLComboChannel.GetAttribute("BT")))
                                                .MaxDiscount = TmpBT.MaxDiscount
                                            End With
                                        Next
                                    End If
                                End If
                            End If
                            If Not Channels(XMLComboChannel.GetAttribute("Chan")) Is Nothing AndAlso Channels(XMLComboChannel.GetAttribute("Chan")).BookingTypes(1)(XMLComboChannel.GetAttribute("BT")) IsNot Nothing Then
                                .Relations.Add(Channels(XMLComboChannel.GetAttribute("Chan")).BookingTypes(1)(XMLComboChannel.GetAttribute("BT")), XMLComboChannel.GetAttribute("Relation"))
                                'ElseIf mvarMain.Channels(XMLComboChannel.GetAttribute("Chan")) Is Nothing Then
                                '    Windows.Forms.MessageBox.Show("Channel " & XMLComboChannel.GetAttribute("Chan") & " was not added to combination " & XMLCombo.GetAttribute("Name") & vbNewLine & _
                                '                                  vbNewLine & "This channel does not exist in the campaign. To add it, press Update in Campaign -> Define Channels and then Update in Edit Pricelists.")
                                'Else
                                '    Windows.Forms.MessageBox.Show("Channel " & XMLComboChannel.GetAttribute("Chan") & " " & XMLComboChannel.GetAttribute("BT") & " was not added to combination " & XMLCombo.GetAttribute("Name") & vbNewLine & _
                                '                                  vbNewLine & "This bookingtype does not exist in the campaign. To add it, press Update in Campaign -> Define Channels and then Update in Edit Pricelists.")
                            End If
                            XMLComboChannel = XMLComboChannel.NextSibling
                        End While
                    End With
                    XMLCombo = XMLCombo.NextSibling
                End While
            End If
            If mvarPath = "" Then
                mvarPath = Path
            End If
        End Sub

        Public Property Channels() As cContractChannels
            Get
                Channels = mvarChannels
            End Get
            Set(ByVal value As cContractChannels)
                mvarChannels = value
            End Set
        End Property

        Public Sub New(ByVal MainObject As cKampanj, Optional ByVal createNew As Boolean = False)
            mvarMain = MainObject
            Combinations = New cCombinations(mvarMain, False)
            Costs = New cCosts(MainObject)
            Channels = New cContractChannels(mvarMain)
            FromDate = Now
            ToDate = Now
            If createNew Then
                CreateChannels()
            End If
            'CreateChannels()
        End Sub
        Public Property ToDate() As Date
            Get
                Return _toDate
            End Get
            Set(ByVal value As Date)
                _toDate = value
            End Set
        End Property

        Public Class ContractEventArgs
            Inherits EventArgs

            Public Property Progress As Single
            Public Property Bookingtype As cContractBookingtype

        End Class

    End Class
End Namespace