Imports System
Imports System.Windows
Imports System.Windows.Forms
Imports System.Text

Namespace Trinity
    Public MustInherit Class cDBReader
        Implements IDetectsProblems

        Public DataPath As String
        Friend _paths As List(Of String)
        Friend _users As List(Of String)
        Public local As Boolean = False

        Public Enum DBTYPE
            ACCESS = 0
            SQLce = 1
            SQL = 2
        End Enum

        Public Enum ConnectionPlace
            Network = 0
            Local = 1
        End Enum

        Public MustOverride Function isLocal() As Boolean

        Public MustOverride Sub Connect(ByVal startmode As Trinity.cDBReader.ConnectionPlace, Optional ByVal UpdateSchema As Boolean = True)

        Public Sub New()
            TrinitySettings = New cSettings(Helper.GetSpecialFolder(Helper.CSIDLEnum.UserProfile) & "Trinity 4.0")
        End Sub

        Public MustOverride Function ReadPaths(ByVal User As String) As DataTable

        Public MustOverride Sub ReadUsers()

        Public ReadOnly Property Users() As List(Of String)
            Get
                Return _users
            End Get
        End Property

        Public ReadOnly Property Paths() As List(Of String)
            Get
                Return _paths
            End Get
        End Property


        Public MustOverride Function getContractSaveTime(ByVal DatabaseID As Long) As DateTime

        Public MustOverride Function getContracts(Optional ByVal searchForRestrictionLock = False) As DataTable

        Public MustOverride Function getContract(ByVal ContractID As Long) As Xml.XmlElement

        Public MustOverride Function getContractAsDatatable(ByVal ContractID As Long) As System.Data.DataTable

        Public MustOverride Function saveContract(Contract As Trinity.cContract, ByVal XML As Xml.XmlDocument) As Boolean

        Public MustOverride Function setAsDeleted(ByVal CampaignID As Long) As Boolean

        Public MustOverride Function updateCampaignStatus(ByVal CampaignID As Long, ByVal status As String) As Boolean

        Public MustOverride Function updateCampaignBuyer(ByVal CampaignID As Long, ByVal status As String) As Boolean

        Public MustOverride Function updateCampaignPlanner(ByVal CampaignID As Long, ByVal status As String) As Boolean

        Public MustOverride Function setContractAsDeleted(ByVal ContractID As Long) As Boolean

        Public MustOverride Function campaignNameExists(ByVal name As String) As Boolean

        Public MustOverride Function unlockCampaign(ByVal id As Long) As Boolean

        Public MustOverride Function lockCampaign(ByVal id As Long) As Boolean

        Public MustOverride Function getDatabaseIDFromName(ByVal name As String) As Integer

        Public MustOverride Function transferPeopleToDB() As Boolean

        Public MustOverride Function getAllPeople() As List(Of cPerson)

        Public MustOverride Sub addPeople(People As List(Of cPerson))

        Public MustOverride Sub removePerson(id As Long)

        Public MustOverride Function GetCampaign(ByVal ID As Long, Optional ByVal OpenReadOnly As Boolean = False) As String

        Public MustOverride Function GetCampaigns(Optional ByVal SQLQuery As String = "") As List(Of CampaignEssentials)

        Public MustOverride Function GetCampaignsUserAccess(Optional ByVal SQLQuery As String = "", Optional ByVal relation As String = "") As Integer

        Public MustOverride Function GetCampaignsXML(Optional ByVal SQLQuery As String = "") As List(Of CampaignEssentials)

        Public MustOverride Function GetBackUpCampaigns(Optional ByVal SQLQuery As String = "") As List(Of CampaignEssentials)

        Public MustOverride Function SaveCampaign(ByVal Camp As Trinity.cKampanj, ByVal XML As Xml.XmlElement) As Boolean

        Public MustOverride Function getAllClients(Optional ByVal sqlSearchForSpecificClientID As String = "", Optional ByVal campaignClientID As Integer = 0) As DataTable

        Public MustOverride Function getClient(ByVal ID As Integer) As String

        Public MustOverride Function FindProgramDuring(ByVal MaM As Integer, ByVal DuringDate As Date, ByVal Area As String, Optional ByVal frm As Object = Nothing) As DataTable

        Public MustOverride Function QUERY(ByVal sql) As Object

        Public MustOverride Function getAllFromProducts(ByVal productID As Integer) As DataTable

        Public MustOverride Function getAllProducts(ByVal clientID As Integer) As DataTable

        Public MustOverride Function getProduct(ByVal productID As Integer) As String

        Public MustOverride Function getProduct(ByVal Mam As Long, ByVal DuringDate As Date) As DataTable

        Public MustOverride Function FilmExist(ByVal name As String, ByVal productID As Integer) As Boolean

        Public MustOverride Sub DeleteFilm(ByVal name As String, ByVal productID As Integer)

        Public MustOverride Function ClientExist(ByVal name As String) As Boolean

        Public MustOverride Function productExist(ByVal name As String) As Boolean

        Public MustOverride Sub addClient(ByVal newClient As Client)

        Public MustOverride Sub addProduct(ByVal Name As String, ByVal ClientID As String, ByVal MarathonClient As String, ByVal MarathonProduct As String, ByVal MarathonCompany As String, ByVal MarathonContract As String, ByVal AdEdgeBrands As List(Of String), ByVal AdTooxAdvertiserID As Long, ByVal AdTooxDivisionID As Long, ByVal AdTooxBrandID As Long, ByVal AdTooxProductType As String)

        Public MustOverride Sub updateClient(ByVal name As String, ByVal id As Integer, Optional ByVal restricted As Integer = 0)

        Public MustOverride Sub deleteClient(ByVal id As Integer)

        Public MustOverride Sub updateProduct(ByVal ProductID As String, ByVal Name As String, ByVal ClientID As String, ByVal MarathonClient As String, ByVal MarathonProduct As String, ByVal MarathonCompany As String, ByVal MarathonContract As String, ByVal AdEdgeBrands As List(Of String), ByVal AdTooxAdvertiserID As Long, ByVal AdTooxDivisionID As Long, ByVal AdTooxBrandID As Long, ByVal AdTooxProductType As String)

        Public MustOverride Function findFilmOnProduct(ByVal name As String, ByVal d As Date, ByVal productID As Integer) As DataTable

        Public MustOverride Function findFilmAndProduct(ByVal name As String, ByVal productID As Integer) As DataTable

        Public MustOverride Function findFilmAndProduct(ByVal name As String) As DataTable

        Public MustOverride Function findFilmAndProductClient(ByVal name As String, ByVal clientID As Integer) As DataTable

        Public MustOverride Function findFilmOnClient(ByVal name As String, ByVal d As Date, ByVal clientID As Integer) As DataTable

        Public MustOverride Function getAllFilms() As DataTable

        Public MustOverride Function addOrUpdateFilm(ByVal name As String, ByVal productID As Integer, ByVal filmCode As String, ByVal channel As String, ByVal description As String, ByVal length As Integer, ByVal index As Decimal) As Boolean

        Public MustOverride Function getAllEvents() As DataTable

        Public MustOverride Function getAllEvents(ByVal startDate As Long, ByVal endDate As Long) As DataTable

        Public MustOverride Function getEvents(Dates As List(Of KeyValuePair(Of Date, Date)), ByVal TargetChannel As String, Optional Type As Integer = 0) As DataTable

        Public MustOverride Function getEventsInInterval([Date] As Date, Channel As String, FromMaM As Integer, ToMaM As Integer) As DataTable

        Public MustOverride Function checkPwd(ByVal UserID As Integer, ByVal s As String) As Boolean

        Public MustOverride Function updatePwd(ByVal s As String, ByVal UserID As String) As Boolean

        Public MustOverride Function getUserID(ByVal s As String) As Integer

        Public MustOverride Function getDBType() As Integer

        Public MustOverride Sub closeConnection()

        Public MustOverride Function getUser(ByVal ID As Integer) As DataRow

        Public MustOverride Function getUsers() As DataTable

        Public MustOverride Sub clearPaths(ByVal userID As Integer)

        Public MustOverride Sub addPath(ByVal userID As Integer, ByVal path As String)

        Public MustOverride Function alive() As Boolean

        Public MustOverride Function getAllProductsSync() As DataTable

        Public MustOverride Function getAllPathsSync() As DataTable

        Public MustOverride Function getAllUsersSync() As DataTable

        Public MustOverride Function getAllFilmsSync() As DataTable

        Public MustOverride Function getAllEventsSync(ByVal dDate As Long) As DataTable

        Public MustOverride Function addTVCheckInfo(ByVal dDate As Integer, ByVal MaM As Integer, ByVal SaM As Integer, ByVal channel As String, ByVal program As String, ByVal filmcode As String, ByVal remark As String, ByVal status As String) As Boolean

        Public MustOverride Function getTVCheckInfo(ByVal StartDate As Long, ByVal EndDate As Long, ByVal Filmcodes As List(Of String)) As DataTable

        Public MustOverride Sub UpdateDBSchema()

        Public MustOverride Function getAllChannelSets() As DataTable

        Public MustOverride Function getContractName(id As Integer) As String

        Public MustOverride Function RecoverCampaignFromBackup(ByVal originalID As Integer) As Boolean

        Public MustOverride Function saveChannelInfo(ByVal channel As cChannel) As Boolean
        Public MustOverride Function updateChannelInfo(ByRef channel As cChannel) As Boolean
        Public MustOverride Function checkForNewChannels(ByRef Campaign As cKampanj) As Boolean
        Public MustOverride Function saveSpotIndex(ByVal BT As cBookingType) As Boolean
        Public MustOverride Function updateSpotIndex(ByRef BT As cBookingType) As Boolean
        Public MustOverride Function saveBookingTypeInfo(ByVal BT As cBookingType) As Boolean
        Public MustOverride Function updateBookingTypeInfo(ByRef BT As cBookingType) As Boolean
        Public MustOverride Function checkForNewBookingTypes(ByRef channel As cChannel) As Boolean
        Public MustOverride Function readPricelist(ByRef BT As cBookingType) As Boolean
        Public MustOverride Function savePricelist(ByVal pricelist As cPricelist) As Boolean
        Public MustOverride Function readChannels(ByRef campaign As cKampanj) As Boolean
        Public MustOverride Function readBookingTypes(ByRef channel As cChannel) As Boolean

        Public MustOverride Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound
    End Class
End Namespace



