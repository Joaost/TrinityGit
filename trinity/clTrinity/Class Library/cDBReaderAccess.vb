
Namespace Trinity
    Public Class cDBReaderAccess
        Inherits Trinity.cDBReader

        Public DBConn As New System.Data.Odbc.OdbcConnection

        Delegate Sub GotProgsInOtherChannels(ByVal dt As DataTable)

        Public Overrides Function RecoverCampaignFromBackup(originalID As Integer) As Boolean
            Return Nothing
        End Function
        Public Overrides Function getContractSaveTime(ByVal DatabaseID As Long) As Date
            Return Nothing
        End Function
        Public Overrides Function getContracts() As System.Data.DataTable
            Return Nothing
        End Function
        Public Overrides Function getContract(ByVal ContractID As Long) As System.Xml.XmlElement
            Return Nothing
        End Function
        Public Overrides Function saveContract(Contract As Trinity.cContract, ByVal XML As System.Xml.XmlDocument) As Boolean
            Return Nothing
        End Function
        Public Overrides Function updateCampaignStatus(CampaignID As Long, status As String) As Boolean
            Return Nothing
        End Function
        Public Overrides Function updateCampaignBuyer(CampaignID As Long, status As String) As Boolean
            Return Nothing
        End Function
        Public Overrides Function updateCampaignPlanner(CampaignID As Long, status As String) As Boolean
            Return Nothing
        End Function
        Public Overrides Function setAsDeleted(ByVal CampaignID As Long) As Boolean
            Return Nothing
        End Function
        Public Overrides Function setContractAsDeleted(ByVal ContractID As Long) As Boolean
            Return Nothing
        End Function
        Public Overrides Function campaignNameExists(ByVal name As String) As Boolean
            Return Nothing
        End Function
        Public Overrides Function lockCampaign(ByVal id As Long) As Boolean
            Return Nothing
        End Function

        Public Overrides Function unlockCampaign(ByVal id As Long) As Boolean
            Return Nothing
        End Function

        Public Overrides Function getDatabaseIDFromName(ByVal name As String) As Integer
            Return -1
        End Function

        Public Overrides Function transferPeopleToDB() As Boolean
            Return False
        End Function

        Public Overrides Function getAllPeople() As System.Collections.Generic.List(Of cPerson)
            Return Nothing
        End Function
        Public Overrides Function SaveCampaign(ByVal Camp As cKampanj, ByVal XML As System.Xml.XmlElement) As Boolean
            Return False
        End Function

        Public Overrides Function GetCampaign(ByVal ID As Long, Optional ByVal OpenReadOnly As Boolean = False) As String
            Return Nothing
        End Function

        Public Overrides Function GetCampaigns(Optional ByVal SQLQuery As String = "") As List(Of CampaignEssentials)
            Return Nothing
        End Function
        
        Public Overrides Function GetCampaignsXML(Optional ByVal SQLQuery As String = "") As List(Of CampaignEssentials)
                Return Nothing
        End Function

        Public Overrides Function GetBackUpCampaigns(Optional SQLQuery As String = "") As System.Collections.Generic.List(Of CampaignEssentials)
            Return Nothing
        End Function
        Public Overrides Sub Connect(ByVal startmode As Trinity.cDBReader.ConnectionPlace, Optional ByVal UpdateSchema As Boolean = True)

            Trinity.Helper.WriteToLogFile("Connecting to access database. Startmode = " & startmode)
            If startmode = ConnectionPlace.Network Then
                DataPath = TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork)
                DBConn.ConnectionString = TrinitySettings.ConnectionString(Trinity.cSettings.SettingsLocationEnum.locNetwork) & TrinitySettings.DataBase(Trinity.cSettings.SettingsLocationEnum.locNetwork)
                Try
                    DBConn.Open()
                Catch
                    Connect(ConnectionPlace.Local)
                End Try
            Else
                local = True
                DataPath = TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locLocal)
                DBConn.ConnectionString = TrinitySettings.ConnectionString(Trinity.cSettings.SettingsLocationEnum.locLocal) & TrinitySettings.DataBase(Trinity.cSettings.SettingsLocationEnum.locLocal)
                DBConn.Open()
            End If
            If DBConn.State = ConnectionState.Open AndAlso UpdateSchema Then
                UpdateDBSchema()
            End If
        End Sub

        Public Sub New()
            TrinitySettings = New cSettings(Helper.GetSpecialFolder(Helper.CSIDLEnum.UserProfile) & "Trinity 4.0")
        End Sub

        Public Overrides Function addTvCheckInfo(ByVal dDate As Integer, ByVal MaM As Integer, ByVal SaM As Integer, ByVal channel As String, ByVal program As String, ByVal filmcode As String, ByVal remark As String, ByVal status As String) As Boolean
            Dim com As New Odbc.OdbcCommand("INSERT INTO spotcontrol (date,MaM,SaM,channel,program,filmcode,remark,status) VALUES (" & dDate & "," & MaM & "," & SaM & ",'" & channel & "','" & program & "','" & filmcode & "','" & remark & "','" & status & "')", DBConn)
            com.ExecuteNonQuery()
            com.Dispose()
        End Function

        Public Overrides Function ReadPaths(ByVal User As String) As DataTable
            Dim _paths = New DataTable

            Dim rd As Odbc.OdbcDataReader
            Dim com As New Odbc.OdbcCommand("SELECT paths.* FROM paths,users Where users.Name='" & User & "' AND Paths.UserID=Users.ID", DBConn)
            rd = com.ExecuteReader
            _paths.load(rd)
            rd.Close()
            rd.Dispose()
            com.Dispose()
            Return _paths
        End Function

        Public Overrides Sub ReadUsers()
            _users = New List(Of String)

            Dim rd As Odbc.OdbcDataReader
            Dim com As New Odbc.OdbcCommand("SELECT Name FROM Users ORDER BY Name", DBConn)
            rd = com.ExecuteReader

            While rd.Read
                _users.Add(rd!name)
            End While

        End Sub

        Public Overrides Function getAllClients() As DataTable
            'Sets up the table
            Dim _clients As DataTable = New DataTable
            Dim rd As Odbc.OdbcDataReader
            Dim com As New Odbc.OdbcCommand("SELECT * FROM Clients", DBConn)
            rd = com.ExecuteReader
            _clients.Load(rd)
            rd.Close()
            Return _clients
        End Function

        Public Overrides Function getClient(ByVal ID As Integer) As String
            Dim _client As String
            On Error GoTo 0
            Dim rd As Odbc.OdbcDataReader
            Dim com As New Odbc.OdbcCommand("SELECT * FROM Clients WHERE ID=" + ID.ToString, DBConn)
            rd = com.ExecuteReader

            While rd.Read
                _client = rd!Name
                rd.Close()
                Return _client
                Exit Function
            End While
            rd.Close()
            Return ""

getClient_Error:
            Return ""
        End Function


        Public Overrides Function FindProgramDuring(ByVal MaM As Integer, ByVal DuringDate As Date, ByVal Area As String, Optional ByVal frm As Object = Nothing) As DataTable
            'returns a data table who contains info of program during tha marked spots


            'On Error GoTo FindProgramDuring_Error

            Dim SQLString As String = "select format(events.date,""yyyy-mm-dd"") as [Date],events.time as [Time],events.channel as [Chan],events.name as [Programme] from events where (date=" & DuringDate.ToOADate & ") and ((StartMam<=" & MaM & " and StartMam+Duration>" & MaM & ") or (StartMam>=" & MaM & " and StartMam<Duration+" & MaM & "))"
            SQLString = "SELECT format(events.date,'yyyymmdd') as [Date],events.time as [Time],events.channel as [Chan],events.name as [Programme] from events where (date=" & DuringDate.ToOADate & ") and ((StartMam<=" & MaM & " and StartMam+Duration>" & MaM & ") or (StartMam>=" & MaM & " and StartMam<Duration+" & MaM & "))"
            Dim Command As New Odbc.OdbcCommand(SQLString, DBConn)
            Dim Adapter As New Odbc.OdbcDataAdapter
            Dim Rd As Odbc.OdbcDataReader
            Rd = Command.ExecuteReader
            Adapter.SelectCommand = Command

            Dim Table As New DataTable

            Table.Locale = System.Globalization.CultureInfo.InvariantCulture
            Adapter.FillLoadOption = LoadOption.OverwriteChanges
            Table.Load(Rd)
            If frm Is Nothing Then
                Return Table
            Else
                frm.Invoke(New GotProgsInOtherChannels(AddressOf frm.SetOtherGrid), New Object() {Table})
                Return Table
            End If

            On Error GoTo 0
            Exit Function

            'FindProgramDuring_Error:

            '            'Err.Raise Err.Number, "frmSchedule: FindProgramDuring", Err.Description
            '            MsgBox("Runtime Error '" & Err.Number & "':" & Chr(10) & Chr(10) & Err.Description, vbCritical, "Error")

        End Function

        Public Overrides Function QUERY(ByVal sql) As Object
            Dim com As New Odbc.OdbcCommand(sql, DBConn)
            Dim retVal As Object = com.ExecuteScalar
            com.Dispose()
            Return retVal
        End Function

        Public Overrides Function getAllFromProducts(ByVal productID As Integer) As DataTable
            Dim _products As DataTable = New DataTable
            Dim rd As Odbc.OdbcDataReader
            Dim com As New Odbc.OdbcCommand("SELECT * FROM products Where ID=" + productID.ToString, DBConn)
            rd = com.ExecuteReader
            _products.Load(rd)
            rd.Close()
            Return _products
        End Function

        Public Overrides Function getAllProducts(ByVal clientID As Integer) As DataTable

            Dim _products As DataTable = New DataTable
            Dim rd As Odbc.OdbcDataReader
            Dim com As New Odbc.OdbcCommand("SELECT * FROM products Where ClientID=" + clientID.ToString, DBConn)
            rd = com.ExecuteReader
            _products.Load(rd)
            rd.Close()
            Return _products
        End Function

        Public Overrides Function getAllProductsSync() As DataTable

            Dim _products As DataTable = New DataTable
            Dim rd As Odbc.OdbcDataReader
            Dim com As New Odbc.OdbcCommand("SELECT * FROM products", DBConn)
            rd = com.ExecuteReader
            _products.Load(rd)
            rd.Close()
            Return _products
        End Function

        Public Overrides Function getProduct(ByVal productID As Integer) As String
            Dim _product As String = ""
            On Error GoTo 0
            Dim rd As Odbc.OdbcDataReader
            Dim com As New Odbc.OdbcCommand("SELECT * FROM products Where ID=" + productID.ToString, DBConn)
            rd = com.ExecuteReader

            While rd.Read
                _product = rd!Name
            End While
            rd.Close()
            Return _product

getProduct_Error:
            Return ""
        End Function

        Public Overrides Function getProduct(ByVal Mam As Long, ByVal DuringDate As Date) As DataTable
            Dim _product As New DataTable

            Dim rd As Odbc.OdbcDataReader
            Dim SQLString As String = "SELECT format(events.date,'yyyymmdd') as [Date],events.time as [Time],events.channel as [Chan],events.name as [Programme] from events where (date=" & DuringDate.ToOADate & ") and ((StartMam<=" & Mam & " and StartMam+Duration>" & Mam & ") or (StartMam>=" & Mam & " and StartMam<Duration+" & Mam & "))"
            Dim com As New Odbc.OdbcCommand(SQLString, DBConn)
            rd = com.ExecuteReader

            _product.Load(rd)
            rd.Close()
            Return _product


        End Function

        Public Overrides Function FilmExist(ByVal name As String, ByVal productID As Integer) As Boolean
            Dim _film As Boolean = False
            On Error GoTo 0
            Dim rd As Odbc.OdbcDataReader
            Dim com As New Odbc.OdbcCommand("SELECT * FROM films Where Product=" + productID.ToString + " AND Name='" + name + "'", DBConn)
            rd = com.ExecuteReader
            If rd.HasRows Then
                _film = True
            End If
            rd.Close()
            Return _film

FilmExist_error:
            Return False

        End Function

        Public Overrides Sub DeleteFilm(ByVal name As String, ByVal productID As Integer)
            Dim com As New Odbc.OdbcCommand("DELETE FROM Films WHERE Name ='" & name & "' AND Product=" & productID, DBConn)
            com.ExecuteNonQuery()
        End Sub

        Public Overrides Function ClientExist(ByVal name As String) As Boolean
            Dim _client As Boolean = False
            On Error GoTo 0
            Dim rd As Odbc.OdbcDataReader
            Dim com As New Odbc.OdbcCommand("SELECT * FROM clients Where name='" + name + "'", DBConn)
            rd = com.ExecuteReader
            If rd.HasRows Then
                _client = True
            End If
            rd.Close()
            Return _client

ClientExist_error:
            Return False

        End Function

        Public Overrides Function productExist(ByVal name As String) As Boolean
            Dim _product As Boolean = False
            On Error GoTo 0
            Dim rd As Odbc.OdbcDataReader
            Dim com As New Odbc.OdbcCommand("SELECT * FROM products Where Name='" + name + "'", DBConn)
            rd = com.ExecuteReader
            If rd.HasRows Then
                _product = True
            End If
            rd.Close()
            Return _product

ClientExist_error:
            Return False

        End Function

        Public Overrides Sub addClient(ByVal name As String)
            Dim com As New Odbc.OdbcCommand
            com.Connection = DBConn
            com.CommandText = "INSERT INTO clients(Name) VALUES ('" + name + "')"
            com.ExecuteNonQuery()
        End Sub

        Public Overrides Sub addProduct(ByVal Name As String, ByVal ClientID As String, ByVal MarathonClient As String, ByVal MarathonProduct As String, ByVal MarathonCompany As String, ByVal MarathonContract As String, ByVal AdEdgeBrands As List(Of String), ByVal AdTooxAdvertiserID As Long, ByVal AdTooxDivisionID As Long, ByVal AdTooxBrandID As Long, ByVal AdTooxProductType As String)
            Dim com As New Odbc.OdbcCommand

            Dim TmpAdedgeBrands As String = ""
            For Each TmpString As String In AdEdgeBrands
                TmpAdedgeBrands &= TmpString & "|"
            Next
            TmpAdedgeBrands = TmpAdedgeBrands.Trim("|")
            com.Connection = DBConn
            com.CommandText = "INSERT INTO Products (Name,ClientID,MarathonClient,MarathonProduct,MarathonCompany,MarathonContract,AdedgeBrands,AdtooxAdvertiserID,AdTooxDivisionID,AdTooxBrandID,AdTooxProductType) VALUES ('" & Name & "','" & ClientID & "','" & MarathonClient & "','" & MarathonProduct & "','" & MarathonCompany & "','" & MarathonContract & "','" & TmpAdedgeBrands & "'," & AdTooxAdvertiserID & "," & AdTooxDivisionID & "," & AdTooxBrandID & ",'" & AdTooxProductType & "')"
            com.ExecuteNonQuery()
        End Sub

        Public Overrides Sub updateClient(ByVal name As String, ByVal id As Integer)
            Dim com As New Odbc.OdbcCommand
            com.Connection = DBConn
            com.CommandText = "UPDATE Clients SET Name='" & name & "' WHERE id=" & id.ToString
            com.ExecuteNonQuery()
        End Sub

        Public Overrides Sub updateProduct(ByVal ProductID As String, ByVal Name As String, ByVal ClientID As String, ByVal MarathonClient As String, ByVal MarathonProduct As String, ByVal MarathonCompany As String, ByVal MarathonContract As String, ByVal AdedgeBrands As List(Of String), ByVal AdTooxAdvertiserID As Long, ByVal AdTooxDivisionID As Long, ByVal AdTooxBrandID As Long, ByVal AdTooxProductType As String)
            Dim com As New Odbc.OdbcCommand
            com.Connection = DBConn

            Dim TmpAdedgeBrands As String = ""
            For Each TmpString As String In AdedgeBrands
                TmpAdedgeBrands &= TmpString & "|"
            Next
            TmpAdedgeBrands = TmpAdedgeBrands.Trim("|")

            com.CommandText = "UPDATE Products SET Name='" & Name & "',ClientID=" & ClientID & ",MarathonClient='" & MarathonClient & "',MarathonProduct='" & MarathonProduct & "',MarathonCompany='" & MarathonCompany & "',MarathonContract='" & MarathonContract & "',AdedgeBrands='" & TmpAdedgeBrands & "' WHERE ID=" & ProductID
            com.ExecuteNonQuery()
        End Sub

        Public Overrides Function findFilmOnProduct(ByVal name As String, ByVal d As Date, ByVal productID As Integer) As DataTable
            Dim _film As New DataTable
            Dim rd As Odbc.OdbcDataReader
            Dim com As Odbc.OdbcCommand

            com = New Odbc.OdbcCommand("SELECT DISTINCT Films.Name,Films.Length,Films.Description,Films.Created FROM Films,Products WHERE (Films.Name like '%" & name & "%' or Films.Description like '%" & name & "%') AND Films.Created>=cdate('" & Format(d, "Short date") & "') AND Films.Product=" & productID.ToString, DBConn)
            rd = com.ExecuteReader
            _film.Load(rd)

            Return _film
        End Function

        Public Overrides Function findFilmAndProduct(ByVal name As String, ByVal productID As Integer) As DataTable
            Dim _film As New DataTable
            Dim rd As Odbc.OdbcDataReader
            Dim com As Odbc.OdbcCommand

            com = New Odbc.OdbcCommand("SELECT Films.* FROM Films,Products WHERE Films.Name ='" & name & "' AND Films.Product=" & productID, DBConn)
            rd = com.ExecuteReader
            _film.Load(rd)

            Return _film
        End Function

        Public Overrides Function findFilmAndProduct(ByVal name As String) As DataTable
            Dim _film As New DataTable
            Dim rd As Odbc.OdbcDataReader
            Dim com As Odbc.OdbcCommand

            com = New Odbc.OdbcCommand("SELECT Films.* FROM Films,Products WHERE Films.Name ='" & name & "'", DBConn)
            rd = com.ExecuteReader
            _film.Load(rd)

            Return _film
        End Function

        Public Overrides Function findFilmAndProductClient(ByVal name As String, ByVal clientID As Integer) As DataTable
            Dim _film As New DataTable
            Dim rd As Odbc.OdbcDataReader
            Dim com As Odbc.OdbcCommand

            com = New Odbc.OdbcCommand("SELECT Films.* FROM Films,Products WHERE Films.Name ='" & name & "' AND (Films.Product=Products.ID and Products.ClientID=" & clientID & ")", DBConn)
            rd = com.ExecuteReader
            _film.Load(rd)

            Return _film
        End Function

        Public Overrides Function findFilmOnClient(ByVal name As String, ByVal d As Date, ByVal clientID As Integer) As DataTable
            Dim _film As New DataTable
            Dim rd As Odbc.OdbcDataReader
            Dim com As Odbc.OdbcCommand

            com = New Odbc.OdbcCommand("SELECT DISTINCT Films.Name,Films.Length,Films.Description,Films.Created FROM Films,Products WHERE (Films.Name like '%" & name & "%' or Films.Description like '%" & name & "%') AND Films.Created>=cdate('" & Format(d, "Short date") & "')AND (Films.Product=Products.ID and Products.ClientID=" & clientID & ")", DBConn)
            rd = com.ExecuteReader
            _film.Load(rd)

            Return _film
        End Function

        Public Overrides Function getAllFilms() As DataTable
            Dim _film As New DataTable
            Dim rd As Odbc.OdbcDataReader
            Dim com As Odbc.OdbcCommand

            com = New Odbc.OdbcCommand("SELECT DISTINCT Films.Name,Films.Length,Films.Description,Films.Created FROM Films", DBConn)
            rd = com.ExecuteReader
            _film.Load(rd)

            Return _film
        End Function

        Public Overrides Function getAllPathsSync() As DataTable
            Dim _paths As New DataTable
            Dim rd As Odbc.OdbcDataReader
            Dim com As Odbc.OdbcCommand

            com = New Odbc.OdbcCommand("SELECT * FROM Paths", DBConn)
            rd = com.ExecuteReader
            _paths.Load(rd)
            rd.Close()
            com.Dispose()
            Return _paths
        End Function

        Public Overrides Function getAllUsersSync() As DataTable
            Dim _users As New DataTable
            Dim rd As Odbc.OdbcDataReader
            Dim com As Odbc.OdbcCommand

            com = New Odbc.OdbcCommand("SELECT * FROM Users", DBConn)
            rd = com.ExecuteReader
            _users.Load(rd)
            rd.Close()
            com.Dispose()
            Return _users
        End Function

        Public Overrides Function getAllFilmsSync() As DataTable
            Dim _film As New DataTable
            Dim rd As Odbc.OdbcDataReader
            Dim com As Odbc.OdbcCommand

            com = New Odbc.OdbcCommand("SELECT * FROM Films", DBConn)
            rd = com.ExecuteReader
            _film.Load(rd)

            Return _film
        End Function

        Public Overrides Function addOrUpdateFilm(ByVal name As String, ByVal productID As Integer, ByVal filmCode As String, ByVal channel As String, ByVal description As String, ByVal length As Integer, ByVal index As Decimal) As Boolean
            Dim _film As Boolean = False

            Dim rd As Odbc.OdbcDataReader
            Dim com As Odbc.OdbcCommand
            On Error GoTo 0
            com = New Odbc.OdbcCommand("SELECT * FROM Films WHERE Name='" & name & "' AND Product=" & productID & " AND Channel='" & channel & "'", DBConn)
            rd = com.ExecuteReader

            If rd.HasRows Then
                com = New Odbc.OdbcCommand("UPDATE Films SET Filmcode='" & filmCode & "',Name='" & name & "',Description='" & description & "',Length='" & length & "',Channel='" & channel & "',Product='" & productID & "',Index='" & index & "' WHERE Name='" & name & "' and Channel='" & channel & "'", DBConn)
                com.ExecuteNonQuery()
                _film = True
            Else
                com = New Odbc.OdbcCommand("INSERT INTO Films (Filmcode,Name,Description,Length,Channel,Product,Index) VALUES ('" & filmCode & "','" & name & "','" & description & "','" & length & "','" & channel & "','" & productID & "','" & index & "')", DBConn)
                com.ExecuteNonQuery()
                _film = True
            End If
            rd.Close()

            Return _film
addFilm_Error:
            Return False
        End Function


        Public Overrides Function getAllEvents() As DataTable

            Dim _events As DataTable = New DataTable
            Dim rd As Odbc.OdbcDataReader
            Dim com As New Odbc.OdbcCommand("SELECT * FROM events", DBConn)
            rd = com.ExecuteReader

            _events.Load(rd)
            rd.Close()
            Return _events
        End Function

        Public Overrides Function getAllEvents(ByVal startDate As Long, ByVal endDate As Long) As DataTable

            Dim _events As DataTable = New DataTable
            Dim rd As Odbc.OdbcDataReader
            Dim com As New Odbc.OdbcCommand("SELECT * FROM events WHERE Date>=" + startDate.ToString + " AND Date<=" + endDate.ToString + " ORDER BY Date,Time", DBConn)
            rd = com.ExecuteReader
            _events.Load(rd)
            rd.Close()
            Return _events
        End Function

        Public Overrides Function getAllEventsSync(ByVal dDate As Long) As DataTable

            Dim _events As DataTable = New DataTable
            Dim rd As Odbc.OdbcDataReader
            Dim com As New Odbc.OdbcCommand("SELECT * FROM events WHERE Date>=" + dDate.ToString, DBConn)
            rd = com.ExecuteReader

            _events.Load(rd)
            rd.Close()
            Return _events
        End Function


        Public Overrides Function getEvents(Dates As List(Of KeyValuePair(Of Date, Date)), ByVal TargetChannel As String, Optional Type As Integer = 0) As DataTable

            Dim _events As DataTable = New DataTable
            Dim rd As Odbc.OdbcDataReader

            Dim _periodStrB As New System.Text.StringBuilder("(")
            For Each _date As KeyValuePair(Of Date, Date) In Dates
                _periodStrB.Append("(Date>=" & _date.Key.ToOADate & " AND Date<=" & _date.Value.ToOADate & ") OR")
            Next
            Dim _periodStr As String = _periodStrB.ToString.Substring(0, _periodStrB.Length - 3) & ")"
            Dim com As New Odbc.OdbcCommand("SELECT * FROM events WHERE channel ='" + TargetChannel + "' AND " & _periodStr & " AND Type=" & Type & " ORDER BY Date,Time", DBConn)
            rd = com.ExecuteReader
            _events.Load(rd)

            rd.Close()
            Return _events
        End Function

        'Public Overrides Function getEvents(ByVal SQLstring As String) As DataTable

        '    Dim _events As DataTable = New DataTable
        '    Dim rd As Odbc.OdbcDataReader
        '    Dim com As New Odbc.OdbcCommand(SQLstring, DBConn)
        '    rd = com.ExecuteReader
        '    _events.Load(rd)

        '    rd.Close()
        '    Return _events
        'End Function

        Public Overrides Function checkPwd(ByVal UserID As Integer, ByVal s As String) As Boolean
            Return False
        End Function

        Public Overrides Function updatePwd(ByVal s As String, ByVal UserID As String) As Boolean
            Return False
        End Function

        Public Overrides Function getUserID(ByVal User As String) As Integer
            Dim rd As Odbc.OdbcDataReader
            Dim com As New Odbc.OdbcCommand("SELECT id FROM Users WHERE Name='" & User & "'", DBConn)
            Try
                rd = com.ExecuteReader
                If rd.HasRows Then
                    Return rd!id
                Else
                    Return -1
                End If
            Catch ex As Odbc.OdbcException
                Debug.Print("Error: " & ex.Message)
            End Try

        End Function

        Public Overrides Function getDBType() As Integer
            Return DBTYPE.ACCESS
        End Function

        Public Overrides Sub closeConnection()
            DBConn.Close()
            DBConn.Dispose()
        End Sub

        Public Overrides Function getUsers() As DataTable
            Dim _users = New DataTable

            Dim rd As Odbc.OdbcDataReader
            Dim com As New Odbc.OdbcCommand("SELECT Name FROM Users ORDER BY Name", DBConn)
            rd = com.ExecuteReader
            _users.load(rd)
            rd.Close()
            com.Dispose()
            Return _users
        End Function

        Public Overrides Function getUser(ByVal ID As Integer) As DataRow
            Return Nothing
        End Function

        Public Overrides Function alive() As Boolean
            If DBConn.State = ConnectionState.Open Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Overrides Sub addPath(ByVal UserID As Integer, ByVal path As String)
            Dim com As New Odbc.OdbcCommand
            com.CommandText = "INSERT INTO paths(UserID,path) VALUES ('" + UserID.ToString + "','" + path + "')"
            com.Connection = DBConn
            com.ExecuteNonQuery()
            com.Dispose()
        End Sub

        Public Overrides Function isLocal() As Boolean
            Return local
        End Function

        Public Overrides Sub clearPaths(ByVal userID As Integer)
            Dim com As New Odbc.OdbcCommand
            com.CommandText = "DELETE FROM paths WHERE UserID=" & userID
            com.Connection = DBConn
            com.ExecuteNonQuery()
            com.Dispose()
        End Sub

        Public Overrides Function getTVCheckInfo(ByVal StartDate As Long, ByVal EndDate As Long, ByVal Filmcodes As System.Collections.Generic.List(Of String)) As System.Data.DataTable
            Dim _remarks = New DataTable
            Dim FilmStr As String = ""

            For Each s As String In Filmcodes
                FilmStr &= "filmcode='" & s & "' OR "
            Next
            If FilmStr = "" Then Return _remarks
            FilmStr = Left(FilmStr, FilmStr.Length - 4)
            If Not FilmStr = "" Then
                Dim rd As Odbc.OdbcDataReader
                Dim com As New Odbc.OdbcCommand("SELECT * FROM SpotControl WHERE [date]>='" & Date.FromOADate(StartDate) & "' AND [date]<='" & Date.FromOADate(EndDate) & "' AND (" & FilmStr & ")", DBConn)
                rd = com.ExecuteReader
                _remarks.load(rd)
                rd.Close()
                com.Dispose()
            End If
            Return _remarks
        End Function

        Public Overrides Sub UpdateDBSchema()
            'Makes sure all tables look like they should. Each new column needs to be checked for and added manually.

            'Check for AdedgeBrands column
            'Dim rd As Odbc.OdbcDataReader
            'Dim com As New Odbc.OdbcCommand("SELECT * FROM Products", DBConn)
            'Dim dt As DataTable
            'rd = com.ExecuteReader
            'dt = rd.GetSchemaTable
            'rd.Close()
            'Dim AdedgeBrandsExists As Boolean = False
            'For Each TmpRow As DataRow In dt.Rows
            '    If TmpRow!ColumnName = "AdedgeBrands" Then AdedgeBrandsExists = True
            'Next
            'If Not AdedgeBrandsExists Then
            '    com = New Odbc.OdbcCommand("ALTER TABLE Products ADD AdedgeBrands TEXT(255)", DBConn)
            '    com.ExecuteNonQuery()
            'End If
            CheckColumn("AdedgeBrands", "Products", "TEXT(255)")
            CheckColumn("Comment", "Events", "TEXT(255)")

            CheckColumn("AdedgeBrands", "Products", "nvarchar(500)")
            CheckColumn("Comment", "Events", "nvarchar(500)")
            CheckColumn("AdTooxAdvertiserId", "Products", "long")
            CheckColumn("AdTooxDivisionId", "Products", "long")
            CheckColumn("AdTooxBrandId", "Products", "long")
            CheckColumn("AdTooxProductType", "Products", "text(255)")
            CheckColumn("UseCPP", "Events", "YesNo")
            CheckColumn("Addition", "Events", "long")
            CheckColumn("EstimationTarget", "Events", "text(255)")

            If CheckColumn("Area", "Events", "TEXT(20)") Then
                Dim com As New Odbc.OdbcCommand("UPDATE events SET Area='" & TrinitySettings.DefaultArea & "'", DBConn)
                com.ExecuteNonQuery()
            End If

        End Sub

        Private Function CheckColumn(ByVal Column As String, ByVal Table As String, ByVal Columntype As String) As Boolean
            Dim rd As Odbc.OdbcDataReader
            Dim com As New Odbc.OdbcCommand("SELECT * FROM " & Table, DBConn)
            Dim dt As DataTable
            rd = com.ExecuteReader
            dt = rd.GetSchemaTable
            rd.Close()
            For Each TmpRow As DataRow In dt.Rows
                If TmpRow!ColumnName = Column Then Return False
            Next
            com = New Odbc.OdbcCommand("ALTER TABLE " & Table & " ADD " & Column & " " & Columntype, DBConn)
            com.ExecuteNonQuery()
            Return True
        End Function

        Public Overrides Function saveChannelInfo(ByVal channel As cChannel) As Boolean
            Return False 'not implemented
        End Function

        Public Overrides Function updateChannelInfo(ByRef channel As cChannel) As Boolean
            Return False 'not implemented
        End Function

        Public Overrides Function saveBookingTypeInfo(ByVal BT As cBookingType) As Boolean
            Return False 'not implemented
        End Function

        Public Overrides Function updateBookingTypeInfo(ByRef BT As cBookingType) As Boolean
            Return False 'not implemented
        End Function

        Public Overrides Function saveSpotIndex(ByVal BT As cBookingType) As Boolean
            Return False
        End Function

        Public Overrides Function updateSpotIndex(ByRef BT As cBookingType) As Boolean
            Return False
        End Function

        Public Overrides Function readPricelist(ByRef BT As cBookingType) As Boolean
            Return False
        End Function

        Public Overrides Function savePricelist(ByVal pricelist As cPricelist) As Boolean
            Return False
        End Function

        Public Overrides Function readChannels(ByRef campaign As cKampanj) As Boolean
            Return False
        End Function

        Public Overrides Function checkForNewBookingTypes(ByRef channel As cChannel) As Boolean
            Return False
        End Function

        Public Overrides Function checkForNewChannels(ByRef Campaign As cKampanj) As Boolean
            Return False
        End Function

        Public Overrides Function readBookingTypes(ByRef channel As cChannel) As Boolean
            Return False
        End Function

        Public Overrides Function getAllChannelSets() As System.Data.DataTable
            Return Nothing
        End Function

        Public Overrides Function DetectProblems() As System.Collections.Generic.List(Of cProblem)

        End Function

        Public Overrides Function getEventsInInterval([Date] As Date, Channel As String, FromMaM As Integer, ToMaM As Integer) As System.Data.DataTable
            Using com As New Odbc.OdbcCommand()
                com.Connection = DBConn
                com.CommandText = "SELECT * FROM events WHERE Date = " & [Date].ToOADate & " AND Channel = '" & Channel & "' AND StartMaM > " & FromMaM & " AND StartMaM < " & ToMaM
                Using rd As Odbc.OdbcDataReader = com.ExecuteReader()
                    Using dt As New DataTable
                        Try
                            dt.Load(rd)
                            Return dt
                        Catch ex As Exception
                            Return New DataTable()
                        End Try
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Sub addPeople(People As System.Collections.Generic.List(Of cPerson))

        End Sub

        Public Overrides Sub removePerson(id As Long)

        End Sub

        Public Overrides Function getContractName(id As Integer) As String

        End Function
    End Class

End Namespace