Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic
Imports clTrinity.Trinity.ColorExtensions
Imports System.Runtime.CompilerServices

Namespace Trinity
    Public Class cSettings

        Private _cachedVariables As New Dictionary(Of String, Object)

#Region "Private vars"
        'Inifiles
        Private TrinityIni As New clsIni
        Private NetworkIni As New clsIni
        Private Main As Object

        'Variables
        Public Enum SettingsLocationEnum
            locLocal = 0
            locNetwork = 1
        End Enum

        Public Enum SyncEnum
            syncNever = -1
            syncAtTrinityStart = 0
            syncDaily = 1
            syncWeekly = 2
        End Enum

        Private mvarLocalDataPath As String

        Public WriteOnly Property MainObject() As Object
            Set(ByVal value As Object)
                Main = value
            End Set
        End Property
#End Region

#Region "General preferences set by user"

        Public Property PPFirst() As Integer
            Get
                If TrinityIni.Text("General", "PPFirst") = "" Then
                    PPFirst = 1
                Else
                    PPFirst = TrinityIni.Text("General", "PPFirst")
                End If
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Text("General", "PPFirst") = value
            End Set
        End Property

        Public Property PPLast() As Integer
            Get
                If TrinityIni.Text("General", "PPLast") = "" Then
                    PPLast = 1
                Else
                    PPLast = TrinityIni.Text("General", "PPLast")
                End If
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Text("General", "PPLast") = value
            End Set
        End Property

        Public Property DefaultMonitorChart() As Integer
            Get
                If TrinityIni.Data("Preferences", "DefaultMonitorChart") = -1 Then
                    Return 0
                Else
                    Return TrinityIni.Data("Preferences", "DefaultMonitorChart")
                End If
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Text("Preferences", "DefaultMonitorChart") = value
            End Set
        End Property

        Public Property DefaultContractDatabaseID As Integer
            Get
                Return TrinityIni.Data("DefaultContract", "ID")
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Data("DefaultContract", "ID") = value
            End Set
        End Property

        Public Property DefaultContractPath() As String
            Get
                Return TrinityIni.Text("DefaultContract", "Path")
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("DefaultContract", "Path") = value
            End Set
        End Property

        Public Property DefaultContractLoadCosts() As Boolean
            Get
                Return TrinityIni.Data("DefaultContract", "Costs")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("DefaultContract", "Costs") = value
            End Set
        End Property

        Public Property DefaultContractLoadTargets() As Boolean
            Get
                If TrinityIni.Data("DefaultContract", "Targets") = -1 Then
                    Return TrinityIni.Data("DefaultContract", "Prices")
                Else
                    Return TrinityIni.Data("DefaultContract", "Targets")
                End If

            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("DefaultContract", "Targets") = value
            End Set
        End Property

        Public Property DefaultContractLoadCombinations() As Boolean
            Get
                Return TrinityIni.Data("DefaultContract", "Combinations")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("DefaultContract", "Combinations") = value
            End Set
        End Property

        Public Property DefaultContractLoadIndexes() As Boolean
            Get
                Return TrinityIni.Data("DefaultContract", "Indexes")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("DefaultContract", "Indexes") = value
            End Set
        End Property

        Public Property DefaultContractLoadAddedValues() As Boolean
            Get
                Return TrinityIni.Data("DefaultContract", "AddedValues")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("DefaultContract", "AddedValues") = value
            End Set
        End Property

        Public Property Autosave() As Boolean
            Get
                Return TrinityIni.Data("General", "Autosave")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("General", "Autosave") = value
            End Set
        End Property

        Public Property ErrorChecking() As Boolean
            Get
                Return TrinityIni.Data("ErrorChecking", "Enabled")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("ErrorChecking", "Enabled") = value
            End Set
        End Property

        Public Property Debug() As Boolean
            Get
                Return TrinityIni.Data("General", "Debug")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("General", "Debug") = value
            End Set
        End Property

        Public Property BetaUser As Boolean
            Get
                Return Not TrinityIni.Data("General", "BetaUser")
            End Get
            Set(value As Boolean)
                TrinityIni.Data("General", "BetaUser") = Not value
            End Set
        End Property

        Public Property TrustedUser As Boolean
            Get
                Return (TrinityIni.Text("General", "TrustedUser") <> "" AndAlso TrinityIni.Text("General", "TrustedUser") <> "0")
            End Get
            Set(value As Boolean)
                TrinityIni.Text("General", "TrustedUser") = IIf(value, "-1", "0")
            End Set
        End Property
#End Region

#Region "General preferences NOT set by user"

        Public ReadOnly Property DefaultUseThis() As Boolean
            Get
                Return NetworkIni.Data("Contract", "DefaultUseThis")
            End Get
        End Property

        Public ReadOnly Property DefaultSecondUniverse(ByVal Area As String) As String
            Get
                'System.Diagnostics.Debug.Print("DefaultSecondUniverse")
                If Area Is Nothing OrElse Area = "" Then
                    Return ""
                End If
                If Not _cachedVariables.ContainsKey("DefaultSecondUniverse" & Area) Then
                    Dim Ini As New clsIni
                    Ini.Create(DataPath(cSettings.SettingsLocationEnum.locNetwork) & Area & "\Area.ini")
                    _cachedVariables.Add("DefaultSecondUniverse" & Area, Ini.Text("Universe", "Second"))
                    Return _cachedVariables("DefaultSecondUniverse" & Area)
                Else
                    Return _cachedVariables("DefaultSecondUniverse" & Area)
                End If
                'Return _cachedVariables("DefaultSecondUniverse" & Area)
            End Get
        End Property

        Public Property DefaultFilmLibraryDate() As Date
            Get
                Return Date.FromOADate(NetworkIni.Data("General", "DefaultFilmLibraryDate"))
            End Get
            Set(ByVal value As Date)
                NetworkIni.Data("General", "DefaultFilmLibraryDate") = value.ToOADate
            End Set
        End Property

        Public Property DefaultShortDateFormat() As String
            Get
                Return TrinityIni.Text("Preferences", "DefaultShortDateFormat")
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("Preferences", "DefaultShortDateFormat") = value
            End Set
        End Property

        Public Property SyncLocalData() As SyncEnum
            Get
                Return TrinityIni.Data("Preferences", "SyncLocalData")
            End Get
            Set(ByVal value As SyncEnum)
                TrinityIni.Data("Preferences", "SyncLocalData") = value
            End Set
        End Property

        Public ReadOnly Property SaveCampaignsAsFiles As Boolean
            Get
                Return NetworkIni.Data("General", "SaveCampaignsAsFiles")
            End Get
        End Property

        Public Property DefaultDateFormat() As String
            Get
                Return TrinityIni.Text("Preferences", "DefaultDateFormat")
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("Preferences", "DefaultDateFormat") = value
            End Set
        End Property
        Public Property SortTargetsByUser() As Boolean
            Get
                Return TrinityIni.Data("General", "SortTargetsByUser")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("General", "SortTargetsByUser") = value
            End Set
        End Property

        Public ReadOnly Property GeneralMedia() As String
            Get
                Return NetworkIni.Text("Marathon", "GeneralMedia")
            End Get
        End Property

        Public ReadOnly Property UseConfirmedBudget() As Boolean
            Get
                If Not _cachedVariables.ContainsKey("UseConfirmedBudget") Then
                    _cachedVariables.Add("UseConfirmedBudget", NetworkIni.Data("General", "UseConfirmedBudget"))
                End If
                Return _cachedVariables("UseConfirmedBudget")
            End Get
        End Property

        Public ReadOnly Property DefaultCPTDropdown() As String
            Get
                If NetworkIni.Text("Preferences", "DefaultCPTDropdown") = "" Then
                    NetworkIni.Text("Preferences", "DefaultCPTDropdown") = "CPT"
                End If
                DefaultCPTDropdown = NetworkIni.Text("Preferences", "DefaultCPTDropdown")
            End Get
        End Property

        Public ReadOnly Property ShowBought() As Boolean
            Get
                ShowBought = TrinityIni.Data("Settings", "ShowBought")
            End Get
        End Property

        Public ReadOnly Property MatchFilmcode() As Boolean
            Get
                MatchFilmcode = TrinityIni.Data("Settings", "MatchFilmcode")
            End Get
        End Property

        Public Property ShowInfoInWindow() As Boolean
            Get
                ShowInfoInWindow = TrinityIni.Data("Settings", "ShowInfoInWindow")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("Settings", "ShowInfoInWindow") = value
            End Set
        End Property

        Public Property DefaultTimeShift As cKampanj.TimeShiftEnum
            Get
                If NetworkIni.Data("Preferences", "DefaultTimeshift") < 0 Then
                    Return cKampanj.TimeShiftEnum.tsDefault
                End If
                Return NetworkIni.Data("Preferences", "DefaultTimeshift")
            End Get
            Set(value As cKampanj.TimeShiftEnum)
                NetworkIni.Data("Preferences", "DefaultTimeshift") = value
            End Set
        End Property

        Public ReadOnly Property ExcludeCommercialsInSponsorship As Boolean
            Get
                Dim TmpIni As New clsIni

                TmpIni.Create(DataPath(SettingsLocationEnum.locNetwork) & Main.Area & "\Area.ini")

                Return TmpIni.Data("Sponsorship", "ExcludeCommercials")
            End Get
        End Property


        Public Function PrintIndexes() As Boolean
            Dim TmpIni As New clsIni

            TmpIni.Create(DataPath(SettingsLocationEnum.locNetwork) & Main.Area & "\Area.ini")
            Return TmpIni.ReadBoolean("Print", "Rbsindex")

        End Function

        Public Function AllAdults() As String
            Dim TmpIni As New clsIni

            TmpIni.Create(DataPath(SettingsLocationEnum.locNetwork) & Main.Area & "\Area.ini")
            AllAdults = TmpIni.Text("General", "EntirePopulation")

        End Function

        Public Property ChannelContact(ByVal Channel As String) As String
            Get
                ChannelContact = TrinityIni.Text("Contacts", Channel)
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("Contacts", Channel) = value
            End Set
        End Property

        Public ReadOnly Property DefaultDayparts(ByVal Parent As Object) As Trinity.cDayparts
            Get
                System.Diagnostics.Debug.Print("DefaultDayParts")
                Dim _dayparts As New cDayparts(Parent)
                Dim _ini As New clsIni

                _ini.Create(DataPath(SettingsLocationEnum.locNetwork) & Main.Area & "\Area.ini")

                Dim DaypartCount As Integer = _ini.Data("Dayparts", "Count")
                Helper.WriteToLogFile("Set Area: Get old Daypart Definitions")
                Dim _primePresent As Boolean = False
                For i As Integer = 1 To DaypartCount
                    Dim _dp As New cDaypart
                    _dp.Name = _ini.Text("Dayparts", "Daypart" & i)
                    _dp.StartMaM = _ini.Data("Dayparts", "StartTime" & i)
                    _dp.EndMaM = _ini.Data("Dayparts", "EndTime" & i)
                    If _ini.Text("Dayparts", "IsPrime" & i) <> "" Then
                        _primePresent = True
                        _dp.IsPrime = _ini.Data("Dayparts", "IsPrime" & i)
                    Else
                        _primePresent = False
                    End If
                    _dayparts.Add(_dp)
                Next
                If Not _primePresent Then
                    _dayparts.GetDaypartForMam(20 * 60).IsPrime = True
                End If
                Return _dayparts
            End Get
        End Property
#End Region

#Region "Lotus Notes"
        Public ReadOnly Property NotesMailFile() As String
            Get
                Dim TmpIni As New clsIni
                TmpIni.Create(Helper.Pathify(NotesDirectory) & "notes.ini")
                Return TmpIni.Text("Notes", "MailFile")
            End Get
        End Property

        Public ReadOnly Property NotesMailServer() As String
            Get
                Dim TmpIni As New clsIni
                TmpIni.Create(Helper.Pathify(NotesDirectory) & "notes.ini")
                Return TmpIni.Text("Notes", "MailServer")
            End Get
        End Property

        Dim _notesDirectory As String = ""
        Private Function NotesDirectory() As String
            If _notesDirectory = "" Then
                _notesDirectory = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Lotus\\Notes").GetValue("Path")
            End If
            Return _notesDirectory
        End Function
#End Region

#Region "Other"

        Public Property CheckForNewDLL() As Boolean
            Get
                Return TrinityIni.Data("General", "CheckForNewDLL", 0)
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("General", "CheckForNewDLL") = value
            End Set
        End Property

        Public ReadOnly Property getLastCampaigns() As String()
            Get
                Dim tmpArray(9) As String
                tmpArray(0) = TrinityIni.Text("LastCampaigns", "lastcampaign1")
                tmpArray(1) = TrinityIni.Text("LastCampaigns", "lastcampaign2")
                tmpArray(2) = TrinityIni.Text("LastCampaigns", "lastcampaign3")
                tmpArray(3) = TrinityIni.Text("LastCampaigns", "lastcampaign4")
                tmpArray(4) = TrinityIni.Text("LastCampaigns", "lastcampaign5")
                tmpArray(5) = TrinityIni.Text("LastCampaigns", "lastcampaign6")
                tmpArray(6) = TrinityIni.Text("LastCampaigns", "lastcampaign7")
                tmpArray(7) = TrinityIni.Text("LastCampaigns", "lastcampaign8")
                tmpArray(8) = TrinityIni.Text("LastCampaigns", "lastcampaign9")
                tmpArray(9) = TrinityIni.Text("LastCampaigns", "lastcampaign10")
                Return tmpArray
            End Get
        End Property

        Public WriteOnly Property setLastCampaign() As String
            Set(ByVal value As String)
                For i As Integer = 1 To 10
                    If TrinityIni.Text("LastCampaigns", "lastcampaign" & i) = value Then Exit Property
                Next
                TrinityIni.Text("LastCampaigns", "lastcampaign10") = TrinityIni.Text("LastCampaigns", "lastcampaign9")
                TrinityIni.Text("LastCampaigns", "lastcampaign9") = TrinityIni.Text("LastCampaigns", "lastcampaign8")
                TrinityIni.Text("LastCampaigns", "lastcampaign8") = TrinityIni.Text("LastCampaigns", "lastcampaign7")
                TrinityIni.Text("LastCampaigns", "lastcampaign7") = TrinityIni.Text("LastCampaigns", "lastcampaign6")
                TrinityIni.Text("LastCampaigns", "lastcampaign6") = TrinityIni.Text("LastCampaigns", "lastcampaign5")
                TrinityIni.Text("LastCampaigns", "lastcampaign5") = TrinityIni.Text("LastCampaigns", "lastcampaign4")
                TrinityIni.Text("LastCampaigns", "lastcampaign4") = TrinityIni.Text("LastCampaigns", "lastcampaign3")
                TrinityIni.Text("LastCampaigns", "lastcampaign3") = TrinityIni.Text("LastCampaigns", "lastcampaign2")
                TrinityIni.Text("LastCampaigns", "lastcampaign2") = TrinityIni.Text("LastCampaigns", "lastcampaign1")
                TrinityIni.Text("LastCampaigns", "lastcampaign1") = value
            End Set
        End Property

        Public ReadOnly Property getPivotDefaultUnits() As List(Of String)
            Get
                Dim list As New List(Of String)
                For i As Integer = 1 To 100
                    If TrinityIni.Text("PivotDefaultUnits", "unit" & i) = "" Then
                        Exit For
                    End If
                    list.Add(TrinityIni.Text("PivotDefaultUnits", "unit" & i))
                Next
                Return list
            End Get
        End Property

        Public WriteOnly Property setPivotDefaultUnits() As List(Of String)
            Set(ByVal value As List(Of String))
                Dim i As Integer = 1
                For Each s As String In value
                    TrinityIni.Text("PivotDefaultUnits", "unit" & i) = s
                    i += 1
                Next
                TrinityIni.Text("PivotDefaultUnits", "unit" & i) = ""
            End Set
        End Property

        Public ReadOnly Property getPivotDefaultColumns() As List(Of String)
            Get
                Dim list As New List(Of String)
                For i As Integer = 1 To 100
                    If TrinityIni.Text("PivotDefaultColumns", "column" & i) = "" Then
                        Exit For
                    End If
                    list.Add(TrinityIni.Text("PivotDefaultColumns", "column" & i))
                Next
                Return list
            End Get
        End Property

        Public WriteOnly Property setPivotDefaultColumns() As List(Of String)
            Set(ByVal value As List(Of String))
                Dim i As Integer = 1
                For Each s As String In value
                    TrinityIni.Text("PivotDefaultColumns", "column" & i) = s
                    i += 1
                Next
                TrinityIni.Text("PivotDefaultColumns", "column" & i) = ""
            End Set
        End Property

        Public ReadOnly Property getPivotDefaultRows() As List(Of String)
            Get
                Dim list As New List(Of String)
                For i As Integer = 1 To 100
                    If TrinityIni.Text("PivotDefaultRows", "row" & i) = "" Then
                        Exit For
                    End If
                    list.Add(TrinityIni.Text("PivotDefaultRows", "row" & i))
                Next
                Return list
            End Get
        End Property

        Public WriteOnly Property setPivotDefaultRows() As List(Of String)
            Set(ByVal value As List(Of String))
                Dim i As Integer = 1
                For Each s As String In value
                    TrinityIni.Text("PivotDefaultRows", "row" & i) = s
                    i += 1
                Next
                TrinityIni.Text("PivotDefaultRows", "row" & i) = ""
            End Set
        End Property

        Public ReadOnly Property BillingAddresses() As Collection
            Get
                Dim addressCollection As New Collection
                If TrinityIni.Text("Billingaddresses", "address1") <> "" Then addressCollection.Add(TrinityIni.Text("BillingAddresses", "address1"))
                If TrinityIni.Text("Billingaddresses", "address2") <> "" Then addressCollection.Add(TrinityIni.Text("BillingAddresses", "address2"))
                If TrinityIni.Text("Billingaddresses", "address3") <> "" Then addressCollection.Add(TrinityIni.Text("BillingAddresses", "address3"))
                If TrinityIni.Text("Billingaddresses", "address4") <> "" Then addressCollection.Add(TrinityIni.Text("BillingAddresses", "address4"))
                If TrinityIni.Text("Billingaddresses", "address5") <> "" Then addressCollection.Add(TrinityIni.Text("BillingAddresses", "address5"))
                Return addressCollection
            End Get
        End Property

        Public ReadOnly Property OrderPlacerAddresses() As Collection
            Get
                Dim addressCollection As New Collection
                If TrinityIni.Text("OrderPlacerAddresses", "address1") <> "" Then addressCollection.Add(TrinityIni.Text("OrderPlacerAddresses", "address1"))
                If TrinityIni.Text("OrderPlacerAddresses", "address2") <> "" Then addressCollection.Add(TrinityIni.Text("OrderPlacerAddresses", "address2"))
                If TrinityIni.Text("OrderPlacerAddresses", "address3") <> "" Then addressCollection.Add(TrinityIni.Text("OrderPlacerAddresses", "address3"))
                If TrinityIni.Text("OrderPlacerAddresses", "address4") <> "" Then addressCollection.Add(TrinityIni.Text("OrderPlacerAddresses", "address4"))
                If TrinityIni.Text("OrderPlacerAddresses", "address5") <> "" Then addressCollection.Add(TrinityIni.Text("OrderPlacerAddresses", "address5"))
                Return addressCollection
            End Get
        End Property

        Public ReadOnly Property StartMode() As Integer
            Get
                StartMode = TrinityIni.Data("Startup", "StartMode")
            End Get
        End Property

        Public Sub New(ByVal LocalDataPath As String)
            mvarLocalDataPath = LocalDataPath
            Setup()
        End Sub

        Private Sub Setup()
            TrinityIni.Create(mvarLocalDataPath & "\Trinity.ini")
            If My.Computer.FileSystem.FileExists(Helper.Pathify(TrinityIni.Text("Paths", "Datapath")) & "Trinity.ini") Then
                NetworkIni.Create(Helper.Pathify(TrinityIni.Text("Paths", "Datapath")) & "Trinity.ini")
            Else
                NetworkIni.Create(mvarLocalDataPath & "\Data\Trinity.ini")
            End If
        End Sub

        Public Property IncludeCompensations() As Boolean
            Get
                Return TrinityIni.Data("General", "IncludeCompensations")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("General", "IncludeCompensations") = value
            End Set
        End Property

        Public Property RegisteredConnectDLLBuild() As Integer
            Get
                Return TrinityIni.Data("General", "RegisteredConnectDLLBuild")
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Data("General", "RegisteredConnectDLLBuild") = value
            End Set
        End Property
#End Region

#Region "Database"

        Public Property UpdateDBSchema() As Boolean
            Get
                Return TrinityIni.Data("General", "UpdateDBSchema")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("General", "UpdateDBSchema") = value
            End Set
        End Property

        Public Property ScheduleDatabaseConnectionString() As String
            Get
                Return TrinityIni.Text("NetworkDB", "ScheduleDatabaseConnectionString")
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("NetworkDB", "ScheduleDatabaseConnectionString") = value
            End Set
        End Property

        Public Function DataBase(ByVal Where As SettingsLocationEnum) As String
            System.Diagnostics.Debug.Print("Database")
            If Where = SettingsLocationEnum.locNetwork Then
                DataBase = TrinityIni.Text("NetworkDB", "DB")
            Else
                DataBase = TrinityIni.Text("LocalDB", "DB")
            End If
        End Function

        Public Property LocalDataBase() As String
            Get
                LocalDataBase = TrinityIni.Text("LocalDB", "DB")
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("LocalDB", "DB") = value
            End Set
        End Property

        Public Property NetworkDataBase() As String
            Get
                NetworkDataBase = TrinityIni.Text("NetworkDB", "DB")
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("NetworkDB", "DB") = value
            End Set
        End Property

        'Public ReadOnly Property DBUser() As String
        '    Get
        '        DBUser = Helper.Decode(TrinityIni.Text("NetworkDB", "Uid"))
        '    End Get
        'End Property

        'Public ReadOnly Property DBPwd() As String
        '    Get
        '        DBPwd = Helper.Decode(TrinityIni.Text("NetworkDB", "Pwd"))
        '    End Get
        'End Property

        Public ReadOnly Property DBUserCommon() As String
            Get
                DBUserCommon = Helper.Decode(NetworkIni.Text("CommonPricelistDB", "Uid"))
            End Get
        End Property

        Public ReadOnly Property DBPwdCommon() As String
            Get
                DBPwdCommon = Helper.Decode(NetworkIni.Text("CommonPricelistDB", "Pwd"))
            End Get
        End Property

        Public Property ConnectionStringCommon() As String
            Get
                'System.Diagnostics.Debug.Print("ConnectionStringCommon")
                Return NetworkIni.Text("CommonPricelistDB", "Connection")
                'Return TrinityIni.Text("CommonNetworkDB", "Connection")
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("CommonNetworkDB", "Connection") = value
            End Set
        End Property


        Public Property ConnectionStringCommonSpecifics() As String
            Get
                'System.Diagnostics.Debug.Print("ConnectionStringCommon")
                Return NetworkIni.Text("CommonSpecifics", "Connection")
                'Return TrinityIni.Text("CommonNetworkDB", "Connection")
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("CommonSpecifics", "Connection") = value
            End Set
        End Property

        Public Property ConnectionString(ByVal Where As SettingsLocationEnum) As String
            Get
                'System.Diagnostics.Debug.Print("ConnectionString")
                If Where = SettingsLocationEnum.locNetwork Then
                    Return TrinityIni.Text("NetworkDB", "Connection")
                Else
                    Return TrinityIni.Text("LocalDB", "Connection")
                End If
            End Get
            Set(ByVal value As String)
                If Where = SettingsLocationEnum.locNetwork Then
                    TrinityIni.Text("NetworkDB", "Connection") = value
                Else
                    TrinityIni.Text("LocalDB", "Connection") = value
                End If
            End Set
        End Property

        Public ReadOnly Property SharedDatabaseName As String
            Get
                Dim Ini As New clsIni
                Ini.Create(IO.Path.Combine(ActiveDataPath, "database.ini"))
                Return Ini.Text("Shared", "Name")
            End Get
        End Property

        Public ReadOnly Property MessagingServer As String
            Get
                Dim Ini As New clsIni
                Ini.Create(IO.Path.Combine(ActiveDataPath, "database.ini"))
                Return Ini.Text("Messages", "ConnectionString")
            End Get
        End Property

        Public Property LastMessageReceived As Integer
            Get
                Return TrinityIni.Data("Messages", "LastReceived")
            End Get
            Set(value As Integer)
                TrinityIni.Data("Messages", "LastReceived") = value
            End Set
        End Property

#End Region

#Region "Areas"
        Public ReadOnly Property DefaultArea() As String
            Get
                System.Diagnostics.Debug.Print("DefaultArea")
                DefaultArea = NetworkIni.Text("Locale", "Area")
            End Get
        End Property

        Public ReadOnly Property DefaultAreaLog() As String
            Get
                DefaultAreaLog = NetworkIni.Text("Locale", "AreaLog")
            End Get
        End Property

        Public ReadOnly Property AreaCount() As Integer
            Get
                AreaCount = NetworkIni.Data("Locale", "AreaCount")
            End Get
        End Property

        Public ReadOnly Property AreaLog(ByVal Index As Integer) As String
            Get
                AreaLog = NetworkIni.Text("Locale", "AreaLog" & Index)
            End Get
        End Property

        Public ReadOnly Property Area(ByVal Index As Integer) As String
            Get
                System.Diagnostics.Debug.Print("Area")
                If Not _cachedVariables.ContainsKey("Area") Then
                    _cachedVariables.Add("Area", NetworkIni.Text("Locale", "Area" & Index))
                    Return _cachedVariables("Area")
                End If
                Area = NetworkIni.Text("Locale", "Area" & Index)
            End Get
        End Property

        Public ReadOnly Property AreaName(ByVal Index As Integer) As String
            Get
                AreaName = NetworkIni.Text("Locale", "AreaName" & Index)
            End Get
        End Property

        Public ReadOnly Property AreaName(ByVal Area As String) As String
            Get
                Dim TmpIni As New clsIni

                TmpIni.Create(DataPath(SettingsLocationEnum.locNetwork) & Main.Area & "\Area.ini")
                Return TmpIni.Text("General", "Areaname")
            End Get
        End Property
#End Region

#Region "List appearances and booking"


        Public Property PrintOpenDBColumn(ByVal Index As Integer) As String
            Get
                PrintOpenDBColumn = TrinityIni.Text("PrintOpenDBColumn", "Column" & Index)
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("PrintOpenDBColumn", "Column" & Index) = value
            End Set
        End Property
        Public Property PrintOpenDBColumnCount() As Integer
            Get
                PrintOpenDBColumnCount = TrinityIni.Data("PrintOpenDBColumn", "Count")
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Data("PrintOpenDBColumn", "Count") = value
            End Set
        End Property
        Public Property PrintRecentOpenDBColumn(ByVal Index As Integer) As String
            Get
                PrintRecentOpenDBColumn = TrinityIni.Text("PrintRecentOpenDBColumn", "Column" & Index)
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("PrintRecentOpenDBColumn", "Column" & Index) = value
            End Set
        End Property
        Public Property PrintRecentOpenDBColumnCount() As Integer
            Get
                PrintRecentOpenDBColumnCount = TrinityIni.Data("PrintRecentOpenDBColumnCount", "Count")
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Data("PrintRecentOpenDBColumnCount", "Count") = value
            End Set
        End Property

        Public Property TopPercent() As Single
            Get
                If TrinityIni.Text("Booking", "TopPercent") <> "" Then
                    TopPercent = TrinityIni.Text("Booking", "TopPercent")
                Else
                    TopPercent = 0.5
                End If
            End Get
            Set(ByVal value As Single)
                TrinityIni.Text("Booking", "TopPercent") = value
            End Set
        End Property

        Public Property SummaryWidth() As Single
            Get
                If TrinityIni.Text("Booking", "SummaryWidth") = "" Then
                    SummaryWidth = 3315
                Else
                    SummaryWidth = TrinityIni.Text("Booking", "SummaryWidth")
                End If
            End Get
            Set(ByVal value As Single)
                TrinityIni.Text("Booking", "SummaryWidth") = value
            End Set
        End Property

        Public Property BookingColumnCount() As Integer
            Get
                BookingColumnCount = TrinityIni.Data("Columns", "Count")
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Data("Columns", "Count") = value
            End Set
        End Property

        Public Property BookingColumn(ByVal Index As Integer) As String
            Get
                BookingColumn = TrinityIni.Text("Columns", "Column" & Index)
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("Columns", "Column" & Index) = value
            End Set
        End Property

        Public Property BookingColumnWidth(ByVal Index As Integer) As Single
            Get
                BookingColumnWidth = TrinityIni.Data("ColumnWidth", TrinityIni.Text("Columns", "Column" & Index))
            End Get
            Set(ByVal value As Single)
                TrinityIni.Data("ColumnWidth", TrinityIni.Text("Columns", "Column" & Index)) = value
            End Set
        End Property

        'Added by Hannes
        Public Property PrintBookingColumn(ByVal Index As Integer) As String
            Get
                PrintBookingColumn = TrinityIni.Text("PrintBookingColumns", "Column" & Index)
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("PrintBookingColumns", "Column" & Index) = value
            End Set
        End Property

        'Added by Hannes
        Public Property PrintBookingColumnCount() As Integer
            Get
                PrintBookingColumnCount = TrinityIni.Data("PrintBookingColumns", "Count")
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Data("PrintBookingColumns", "Count") = value
            End Set
        End Property

        Public Property SpotlistColumnCount() As Integer
            Get
                SpotlistColumnCount = TrinityIni.Data("SpotlistColumns", "Count")
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Data("SpotlistColumns", "Count") = value
            End Set
        End Property

        Public Property SpotlistColumn(ByVal Index As Integer) As String
            Get
                SpotlistColumn = TrinityIni.Text("SpotlistColumns", "Column" & Index)
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("SpotlistColumns", "Column" & Index) = value
            End Set
        End Property

        Public Property SpotlistColumnWidth(ByVal Index As Integer) As Single
            Get
                SpotlistColumnWidth = TrinityIni.Data("SpotlistColumnWidth", TrinityIni.Text("SpotlistColumns", "Column" & Index))
            End Get
            Set(ByVal value As Single)
                TrinityIni.Data("SpotlistColumnWidth", TrinityIni.Text("SpotlistColumns", "Column" & Index)) = value
            End Set
        End Property

        Public Property ConfirmedColumnCount() As Integer
            Get
                ConfirmedColumnCount = TrinityIni.Data("ConfirmedColumns", "Count")
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Data("ConfirmedColumns", "Count") = value
            End Set
        End Property

        Public Property ConfirmedColumn(ByVal Index As Integer) As String
            Get
                Return TrinityIni.Text("ConfirmedColumns", "Column" & Index)
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("ConfirmedColumns", "Column" & Index) = value
            End Set
        End Property

        Public Property ConfirmedColumnWidth(ByVal Index As Integer) As Single
            Get
                ConfirmedColumnWidth = TrinityIni.Data("ConfirmedColumnWidth", TrinityIni.Text("ConfirmedColumns", "Column" & Index))
            End Get
            Set(ByVal value As Single)
                TrinityIni.Data("ConfirmedColumnWidth", TrinityIni.Text("ConfirmedColumns", "Column" & Index)) = value
            End Set
        End Property

        Public Property ActualColumnCount() As Integer
            Get
                ActualColumnCount = TrinityIni.Data("ActualColumns", "Count")
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Data("ActualColumns", "Count") = value
            End Set
        End Property

        Public Property ActualColumn(ByVal Index As Integer) As String
            Get
                ActualColumn = TrinityIni.Text("ActualColumns", "Column" & Index)
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("ActualColumns", "Column" & Index) = value
            End Set
        End Property

        Public Property ActualColumnWidth(ByVal Index As Integer) As Single
            Get
                ActualColumnWidth = TrinityIni.Data("ActualColumnWidth", TrinityIni.Text("ActualColumns", "Column" & Index))
            End Get
            Set(ByVal value As Single)
                TrinityIni.Data("ActualColumnWidth", TrinityIni.Text("ActualColumns", "Column" & Index)) = value
            End Set
        End Property

        Public Property PrintColumnCount() As Integer
            Get
                PrintColumnCount = TrinityIni.Data("PrintColumns", "Count")
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Data("PrintColumns", "Count") = value
            End Set
        End Property

        Public Property PrintColumn(ByVal Index As Integer) As String
            Get
                PrintColumn = TrinityIni.Text("PrintColumns", "Column" & Index)
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("PrintColumns", "Column" & Index) = value
            End Set
        End Property

        Public Property PrintColumnWidth(ByVal Index As Integer) As Single
            Get
                PrintColumnWidth = TrinityIni.Data("SpotlsitColumnWidth", TrinityIni.Text("PrintColumns", "Column" & Index))
            End Get
            Set(ByVal value As Single)
                TrinityIni.Data("PrintColumnWidth", TrinityIni.Text("PrintColumns", "Column" & Index)) = value
            End Set
        End Property
#End Region

#Region "Appearance preferences"
        Public Property LogoAlignment() As Integer
            Get
                If TrinityIni.Data("Settings", "LogoAlignment") = -1 Then
                    Return 1
                Else
                    Return TrinityIni.Data("Settings", "LogoAlignment")
                End If
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Data("Settings", "LogoAlignment") = value
            End Set
        End Property

        Public Property ColorSchemeCount() As Integer
            Get
                ColorSchemeCount = NetworkIni.Data("ColorSchemes", "Count")
            End Get
            Set(ByVal value As Integer)
                NetworkIni.Data("ColorSchemes", "Count") = value
            End Set
        End Property

        Public Property ColorScheme(ByVal Index) As String
            Get
                ColorScheme = NetworkIni.Text("ColorScheme" & Index, "Name")
            End Get
            Set(ByVal value As String)
                NetworkIni.Text("ColorScheme" & Index, "Name") = value
            End Set
        End Property

        Public Property SchemeFont(ByVal index) As String
            Get
                If NetworkIni.Text("ColorScheme" & index, "Font") = "" Then
                    Return "Segoe UI"
                Else
                    Return NetworkIni.Text("ColorScheme" & index, "Font")
                End If
            End Get
            Set(ByVal value As String)
                NetworkIni.Text("ColorScheme" & index, "Font") = value
            End Set
        End Property

        Public Property Color(ByVal Index As Integer, ByVal Var As String) As Long
            Get
                If Campaign.xmlColorSchemes.Count = 0 Then
                    Color = Val(NetworkIni.Text("ColorScheme" & Index, Var))
                Else
                    Select Case Var
                        Case Is = "Headline"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).HeadlineColor.ToRGB
                        Case Is = "Panel"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).PanelBGColor.ToRGB
                        Case "PanelText"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).PanelFGColor.ToRGB
                        Case "Diagram1" Or "Color1"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc1.ToRGB
                        Case "Diagram2" Or "Color2"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc2.ToRGB
                        Case "Diagram3" Or "Color3"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc3.ToRGB
                        Case "Diagram4" Or "Color4"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc4.ToRGB
                        Case "Diagram5" Or "Color5"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc5.ToRGB
                        Case "Diagram6" Or "Color6"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc6.ToRGB
                        Case "Diagram7" Or "Color7"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc7.ToRGB
                        Case "Diagram8" Or "Color8"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc8.ToRGB
                        Case "Diagram9" Or "Color9"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc9.ToRGB
                        Case "Diagram10" Or "Color10"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc10.ToRGB
                            Case "Diagram11" Or "Color11"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc1.ToRGB
                        Case "Diagram12" Or "Color12"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc2.ToRGB
                        Case "Diagram13" Or "Color13"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc3.ToRGB
                        Case "Diagram14" Or "Color14"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc4.ToRGB
                        Case "Diagram15" Or "Color15"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc5.ToRGB
                        Case "Diagram16" Or "Color16"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc6.ToRGB
                        Case "Diagram17" Or "Color17"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc7.ToRGB
                        Case "Diagram18" Or "Color18"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc8.ToRGB
                        Case "Diagram19" Or "Color19"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc9.ToRGB
                        Case "Diagram20" Or "Color20"
                            Color = DirectCast(Campaign.xmlColorSchemes(Index - 1), Trinity.cColorScheme).pbc10.ToRGB
                        Case Else
                            Color = RGB(0, 0, 0)
                    End Select

                End If
            End Get
            Set(ByVal value As Long)
                NetworkIni.Text("ColorScheme" & Index, Var) = value
            End Set
        End Property
#End Region

#Region "User info"
        Public Property UserName() As String
            Get
                UserName = TrinityIni.Text("ID", "UserName")
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("ID", "UserName") = value
            End Set
        End Property

        Public Property UserCompany() As String
            Get
                UserCompany = TrinityIni.Text("ID", "UserCompany")
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("ID", "UserCompany") = value
            End Set
        End Property

        Public Property UserEmail() As String
            Get
                UserEmail = TrinityIni.Text("ID", "UserEmail")
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("ID", "UserEmail") = value
            End Set
        End Property

        Public Property UserPhoneNr()
            Get
                UserPhoneNr = TrinityIni.Text("ID", "UserPhoneNr")
            End Get
            Set(ByVal value)
                TrinityIni.Text("ID", "UserPhoneNr") = value
            End Set
        End Property

        Public Property UserID()
            Get
                UserID = DBReader.getDatabaseIDFromName(TrinitySettings.UserName) 'TrinityIni.Text("ID", "UserID")
            End Get
            Set(ByVal value)
                TrinityIni.Text("ID", "UserID") = value
            End Set
        End Property
#End Region

#Region "User defined default values"

        Public Property DefaultColorScheme() As Integer
            Get
                Return Val(TrinityIni.Text("Preferences", "ColorScheme"))
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Text("Preferences", "ColorScheme") = value
            End Set
        End Property

        Public Property DefaultLogo() As Integer
            Get
                Return Val(TrinityIni.Text("Preferences", "Logo"))
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Text("Preferences", "Logo") = value
            End Set
        End Property

        Public Property DefaultLogoPlacement() As Integer
            Get
                Return Val(TrinityIni.Text("Preferences", "LogoPlacement"))
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Text("Preferences", "LogoPlacement") = value
            End Set
        End Property

        Public Property DefaultLanguage() As Integer
            Get
                Return Val(TrinityIni.Text("Preferences", "Language"))
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Text("Preferences", "Language") = value
            End Set
        End Property

        Public Property DefaultPrintBundled() As Boolean
            Get
                Return TrinityIni.Data("Preferences", "PrintBundled")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("Preferences", "PrintBundled") = value
            End Set
        End Property

        Public Property DefaultPrintBundledSingle() As Boolean
            Get
                Return TrinityIni.Data("Preferences", "PrintBundledSingle")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("Preferences", "PrintBundledSingle") = value
            End Set
        End Property

        Public Property DefaultPrintCombinedSingle() As Boolean
            Get
                Return TrinityIni.Data("Preferences", "PrintCombinedSingle")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("Preferences", "PrintCombinedSingle") = value
            End Set
        End Property

        Public Property DefaultPrintFilmBudget() As Boolean
            Get
                Return TrinityIni.Data("Preferences", "PrintFilmBudget")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("Preferences", "PrintFilmBudget") = value
            End Set
        End Property

        Public Property DefaultPrintWeekBudget() As Boolean
            Get
                Return TrinityIni.Data("Preferences", "PrintWeekBudget")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("Preferences", "PrintWeekBudget") = value
            End Set
        End Property

        Public Property DefaultPrintCombinations() As Integer
            Get
                Return Math.Abs(TrinityIni.Data("Preferences", "PrintCombinations"))
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Data("Preferences", "PrintCombinations") = value
            End Set
        End Property

        Public Property DefaultInfo(ByVal Var As String) As Boolean
            Get
                Return NetworkIni.Data("Preferences", "Info" & Var)
            End Get
            Set(ByVal value As Boolean)
                NetworkIni.Data("Preferences", "Info" & Var) = value
            End Set
        End Property

        Public Property DefaultColorCodeChannels() As Boolean
            Get
                Return TrinityIni.Data("Preferences", "ColorCode")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("Preferences", "ColorCode") = value
            End Set
        End Property

        Public Property DefaultOneSheetPerChannel() As Boolean
            Get
                Return Not TrinityIni.Data("Preferences", "AllChannelsOnOneSheet")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("Preferences", "AllChannelsOnOneSheet") = Not value
            End Set
        End Property

        Public Property DefaultSumWeeks() As Boolean
            Get
                Return TrinityIni.Data("Preferences", "SumWeeks")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("Preferences", "SumWeeks") = value
            End Set
        End Property

        Public Property DefaultSumDayparts() As Boolean
            Get
                Return TrinityIni.Data("Preferences", "SumDayparts")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("Preferences", "SumDayparts") = value
            End Set
        End Property

        Public Property DefaultColorCodeFilms() As Boolean
            Get
                Return TrinityIni.Data("Preferences", "ColorCodeFilms")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("Preferences", "ColorCodeFilms") = value
            End Set
        End Property


        Public Property DefaultCapitals() As Integer
            Get
                Return Math.Abs(TrinityIni.Data("Preferences", "Capitals"))
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Data("Preferences", "Capitals") = value
            End Set
        End Property

        Public Property DefaultColorCoding() As Integer
            Get
                Return Math.Abs(TrinityIni.Data("Preferences", "Colorcoding"))
            End Get
            Set(ByVal value As Integer)
                TrinityIni.Data("Preferences", "Capitals") = value
            End Set
        End Property

        Public Property DefaultConvertToRealTime() As Boolean
            Get
                Return TrinityIni.Data("Preferences", "ConvertToRealTime")
            End Get
            Set(ByVal value As Boolean)
                TrinityIni.Data("Preferences", "ConvertToRealTime") = value
            End Set
        End Property
#End Region

#Region "Marathon"
        Public Function MarathonCommand() As String
            MarathonCommand = NetworkIni.Text("Marathon", "Command")
        End Function

        Public Function MarathonUseSpotcount() As Boolean
            Return NetworkIni.Data("Marathon", "UseSpotCount")
        End Function

        Public Function MarathonInsertionCode() As String
            Dim code As String = NetworkIni.Text("Marathon", "InsertionCode")
            If code = "" Then
                Return "000"
            Else
                Return code
            End If
        End Function

        Public Property MarathonEnabled() As Boolean
            Get
                Return NetworkIni.Data("Marathon", "Enabled")
            End Get
            Set(ByVal value As Boolean)
                NetworkIni.Data("Marathon", "Enabled") = value
            End Set
        End Property

        Public Function MarathonDiscountCode() As String
            If NetworkIni.Text("Marathon", "DiscountCode") = "" Then
                Return "VO"
            Else
                Return NetworkIni.Text("Marathon", "DiscountCode")
            End If
        End Function

        Public Property MarathonUser() As String
            Get
                MarathonUser = TrinityIni.Text("Marathon", "User")
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("Marathon", "User") = value
            End Set
        End Property

        Public Function MarathonCreateInsertionsPerWeek() As Boolean
            If NetworkIni.Data("Marathon", "InsertionPerWeek") <= 0 Then
                Return False
            End If
            Return True
        End Function

#End Region

#Region "Adtoox"
        Public Property AdtooxUsername() As String
            Get
                AdtooxUsername = TrinityIni.Text("Adtoox", "User")
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("Adtoox", "User") = value
            End Set
        End Property

        Public ReadOnly Property AdtooxEnabled() As Boolean
            Get
                If Not _cachedVariables.ContainsKey("AdTooxEnabled") Then
                    _cachedVariables.Add("AdTooxEnabled", NetworkIni.Data("Adtoox", "Enabled") > 0)
                End If
                Return _cachedVariables("AdTooxEnabled")
            End Get
        End Property

        Public ReadOnly Property Developer() As Boolean
            Get
                Developer = TrinityIni.Data("Developer", "Enabled") > 0
            End Get
        End Property

        Public Property AdtooxPassword() As String
            Get
                AdtooxPassword = Trinity.Helper.Decode(TrinityIni.Text("Adtoox", "Pass"))
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("Adtoox", "Pass") = Trinity.Helper.Encode(value)
            End Set
        End Property
#End Region

#Region "Paths"
        Public Property LocalDataPath() As String
            Get
                System.Diagnostics.Debug.Print("LocalDataPath")
                Return mvarLocalDataPath
            End Get
            Set(ByVal value As String)
                mvarLocalDataPath = value
                Setup()
            End Set
        End Property

        'Path to the Data folder shared across a company
        Private _dataPath As String = ""
        Public Property DataPath(Optional ByVal Where As SettingsLocationEnum = SettingsLocationEnum.locNetwork) As String
            Get
                If _dataPath = "" Then
                    If Where = SettingsLocationEnum.locNetwork Then
                        _dataPath = Helper.Pathify(TrinityIni.Text("Paths", "DataPath"))
                    Else
                        _dataPath = Helper.Pathify(Helper.Pathify(LocalDataPath) & "Data")
                    End If
                    If Not My.Computer.FileSystem.DirectoryExists(_dataPath) Then
                        _dataPath = Helper.Pathify(Helper.Pathify(LocalDataPath) & "Data")
                    End If
                End If
                Return _dataPath
            End Get
            Set(ByVal value As String)
                If Where = SettingsLocationEnum.locNetwork Then
                    TrinityIni.Text("Paths", "DataPath") = value
                    _dataPath = ""
                End If
            End Set
        End Property

        'Path to the Data folder shared across several companies
        Public Property SharedDataPath() As String
            Get
                Return Helper.Pathify(NetworkIni.Text("Paths", "SharedDataPath"))
            End Get
            Set(ByVal value As String)
                NetworkIni.Text("Paths", "SharedDataPath") = value
            End Set
        End Property

        'Return SharedDataPath if it exists, otherwise DataPath
        Public ReadOnly Property ActiveDataPath() As String
            Get
                If Not _cachedVariables.ContainsKey("ActiveDataPath") Then
                    If SharedDataPath = "\" Then
                        _cachedVariables.Add("ActiveDataPath", DataPath(SettingsLocationEnum.locNetwork))
                    Else
                        _cachedVariables.Add("ActiveDataPath", SharedDataPath)
                    End If
                    Return _cachedVariables("ActiveDataPath")
                Else
                    Return _cachedVariables("ActiveDataPath")
                End If
                'If SharedDataPath = "\" Then
                '    Return DataPath(SettingsLocationEnum.locNetwork)
                'Else
                '    Return SharedDataPath
                'End If
            End Get
        End Property

        Public Property CampaignFiles() As String
            Get
                Return TrinityIni.Text("Paths", "CampaignsPath")
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("Paths", "CampaignsPath") = value
            End Set
        End Property

        Public Property ChannelSchedules() As String
            Get
                Return TrinityIni.Text("Paths", "SchedulesPath")
            End Get
            Set(ByVal value As String)
                TrinityIni.Text("Paths", "SchedulesPath") = value
            End Set
        End Property
#End Region

#Region "Universes"
        Public Property Universes() As Integer
            Get
                System.Diagnostics.Debug.Print("Universes")
                If Not _cachedVariables.ContainsKey("UniverseCount") Then
                    _cachedVariables.Add("UniverseCount", NetworkIni.Data("Universes", "Count"))
                    Return _cachedVariables("UniverseCount")
                Else
                    Return _cachedVariables("UniverseCount")
                End If
                'Return NetworkIni.Data("Universes", "Count")
            End Get
            Set(ByVal value As Integer)
                NetworkIni.Data("Universes", "Count") = value
            End Set
        End Property

        Public Property Universe(ByVal index As Integer) As String
            Get
                System.Diagnostics.Debug.Print("Universe")
                Return NetworkIni.Text("Universes", "Uni" & index)
            End Get
            Set(ByVal value As String)
                NetworkIni.Text("Universes", "Uni" & index) = value
            End Set
        End Property

        Public Property UniverseAdedge(ByVal index As Integer) As String
            Get
                System.Diagnostics.Debug.Print("UniverseAdedge")
                Return NetworkIni.Text("Universes", "AdedgeUni" & index)
            End Get
            Set(ByVal value As String)
                NetworkIni.Text("Universes", "AdedgeUni" & index) = value
            End Set
        End Property
#End Region

#Region "Extranet"

        Public ReadOnly Property ExtranetAvailable() As Boolean
            Get
                Return ExtranetDatabaseServer <> ""
            End Get
        End Property

        Public Property ExtranetDatabaseServer() As String
            Get
                Return NetworkIni.Text("Extranet", "Server")
            End Get
            Set(ByVal value As String)
                NetworkIni.Text("Extranet", "Server") = value
            End Set
        End Property

        Public Property ExtranetDatabase() As String
            Get
                Return NetworkIni.Text("Extranet", "DB")
            End Get
            Set(ByVal value As String)
                NetworkIni.Text("Extranet", "DB") = value
            End Set
        End Property

        Public ReadOnly Property ExtranetDBUser() As String
            Get
                Return Helper.Decode(NetworkIni.Text("Extranet", "Uid"))
            End Get
        End Property

        Public ReadOnly Property ExtranetDBPwd() As String
            Get
                Return Helper.Decode(NetworkIni.Text("Extranet", "Pwd"))
            End Get
        End Property
#End Region

#Region "Matrix"
        Property MatrixDatabaseServer() As String
            Get
                Return NetworkIni.Text("Matrix", "Server")
            End Get
            Set(ByVal value As String)
                NetworkIni.Text("Matrix", "Server") = value
            End Set
        End Property

        Property MatrixDatabase() As String
            Get
                Return NetworkIni.Text("Matrix", "DB")
            End Get
            Set(ByVal value As String)
                NetworkIni.Text("Matrix", "DB") = value
            End Set
        End Property
#End Region

#Region "Problems"


        Sub ShowAllProblems()
            _displayProblem = New Dictionary(Of String, Boolean)
        End Sub

        Private _fixProblem As New Dictionary(Of String, Boolean)

        Property FixProblem(ByVal Type As String, ByVal ProblemID As Integer) As Boolean
            Get
                If _fixProblem.ContainsKey(Type & "(" & ProblemID & ")") Then
                    Return _fixProblem(Type & "(" & ProblemID & ")")
                Else
                    Return True
                End If
            End Get
            Set(ByVal value As Boolean)
                If _fixProblem.ContainsKey(Type & "(" & ProblemID & ")") Then
                    _fixProblem(Type & "(" & ProblemID & ")") = value
                Else
                    _fixProblem.Add(Type & "(" & ProblemID & ")", value)
                End If
            End Set
        End Property

        Private _displayProblem As New Dictionary(Of String, Boolean)
        Property DisplayProblem(ByVal Type As String, ByVal ProblemID As Integer) As Boolean
            Get
                If _displayProblem.ContainsKey(Type & "(" & ProblemID & ")") Then
                    Return _displayProblem(Type & "(" & ProblemID & ")")
                Else
                    Return True
                End If
            End Get
            Set(ByVal value As Boolean)
                If _displayProblem.ContainsKey(Type & "(" & ProblemID & ")") Then
                    _displayProblem(Type & "(" & ProblemID & ")") = value
                Else
                    _displayProblem.Add(Type & "(" & ProblemID & ")", value)
                End If
            End Set
        End Property

        Function HiddenProblems() As List(Of String)
            Dim _list As New List(Of String)
            For Each _problem As String In _displayProblem.Keys
                If Not _displayProblem(_problem) Then
                    _list.Add(_problem)
                End If
            Next
            Return _list
        End Function

        Function ProblemDetectionEnabled() As Boolean
            If Not _cachedVariables.ContainsKey("ProblemDetectionEnabled") Then
                _cachedVariables.Add("ProblemDetectionEnabled", NetworkIni.Data("Debug", "ProblemDetectionEnabled"))
                Return _cachedVariables("ProblemDetectionEnabled")
            Else
                Return _cachedVariables("ProblemDetectionEnabled")
            End If
        End Function

#End Region

#Region "ScheduleTemplates"

        Public Function ScheduleTemplateList() As List(Of String)

            Dim _list As New List(Of String)

            For Each _path As String In My.Computer.FileSystem.GetFiles(IO.Path.Combine(ActiveDataPath, "Schedules"), FileIO.SearchOption.SearchAllSubDirectories, "*.xml").OrderByDescending(Function(s) s)
                _list.Add(_path)
            Next
            Return _list
        End Function

#End Region

#Region "MEF"
        Public Function GetNetworkPreference(Category As String, Key As String) As String
            Return NetworkIni.Text(Category, Key)
        End Function

        Public Function GetUserPreference(Category As String, Key As String) As String
            Return TrinityIni.Text(Category, Key)
        End Function

        Public Sub SetUserPreference(Category As String, Key As String, Value As String)
            TrinityIni.Text(Category, Key) = Value
        End Sub

        Public Sub SetNetworkPreference(Category As String, Key As String, Value As String)
            NetworkIni.Text(Category, Key) = Value
        End Sub

        Public Function GetSharedNetworkPreference(Category As String, Key As String) As String
            Dim Ini As New clsIni
            Ini.Create(IO.Path.Combine(ActiveDataPath, "database.ini"))

            Return Ini.Text(Category, Key)
        End Function

#End Region

    End Class
End Namespace