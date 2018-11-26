Public Class cDBReaderSQL
    Inherits cDBReader

    Public Shadows Event GetAllBookingsProgress(ByVal p As Integer)

    Private _productsByID As Dictionary(Of Integer, cProduct)
    Private _clientsByID As Dictionary(Of Integer, cClient)

    Public Sub GetProductsAndClients()
        _productsByID = New Dictionary(Of Integer, cProduct)
        _clientsByID = New Dictionary(Of Integer, cClient)

        Dim rd As SqlClient.SqlDataReader
        Dim com As SqlClient.SqlCommand
        Using Connection As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)

            com = New SqlClient.SqlCommand("SELECT * FROM Clients", Connection)
            rd = com.ExecuteReader
            While rd.Read
                Dim TmpClient As New cClient
                TmpClient.ID = rd!id
                TmpClient.Name = rd!Name
                _clientsByID.Add(TmpClient.ID, TmpClient)
            End While
            rd.Close()

            com = New SqlClient.SqlCommand("SELECT * FROM Products ORDER BY Name", Connection)
            rd = com.ExecuteReader
            Dim TmpDT As New DataTable
            TmpDT.Load(rd)
            rd.Close()
            For Each TmpRow As DataRow In TmpDT.Rows
                Dim TmpProduct As New cProduct(Nothing)
                TmpProduct.Name = TmpRow!name
                TmpProduct.Client = GetClientByID(TmpRow!clientid)
                TmpProduct.Client.Products.Add(TmpProduct)
                TmpProduct.ID = TmpRow!ID
                Dim _contacts As New DataTable
                Dim CntRd As SqlClient.SqlDataReader
                com = New SqlClient.SqlCommand("SELECT * FROM contacts WHERE ProductID=" & TmpRow!ID, Connection)
                CntRd = com.ExecuteReader
                _contacts.Load(CntRd)
                CntRd.Close()
                For Each _contact As DataRow In _contacts.Rows
                    If _contact!internal Then
                        Dim TmpContact As cContact = TmpProduct.InternalContacts.Add
                        TmpContact.Name = _contact!name
                        If Not IsDBNull(_contact!PhoneNr) Then TmpContact.PhoneNr = _contact!PhoneNr
                        If Not IsDBNull(_contact!Role) Then TmpContact.Role = _contact!role
                    Else
                        Dim TmpContact As cContact = TmpProduct.ExternalContacts.Add
                        TmpContact.Name = _contact!name
                        If Not IsDBNull(_contact!PhoneNr) Then TmpContact.PhoneNr = _contact!PhoneNr
                        If Not IsDBNull(_contact!Role) Then TmpContact.Role = _contact!role
                    End If
                Next
                _productsByID.Add(TmpProduct.ID, TmpProduct)
            Next
        End Using
    End Sub

    Public Overrides Function AddClient(ByVal Name As String) As cClient
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim SQL As String = "INSERT INTO clients (Name) VALUES ('" & Name & "')"
            Dim com As New SqlClient.SqlCommand(SQL, DBConn)
            com.ExecuteNonQuery()
            Dim ID As Integer
            com.CommandText = "SELECT @@identity"
            ID = com.ExecuteScalar
            DBConn.Close()
            _clientsByID = Nothing
            Return GetClientByID(ID)
        End Using
    End Function

    Public Overrides Sub AddContact(ByVal Name As String, ByVal Role As String, ByVal PhoneNr As String, ByVal ProductID As Integer, ByVal Internal As Boolean)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim Int As Integer = 0
            Dim SQL As String = "INSERT INTO contacts (ProductID,Name,Role,PhoneNr,Internal) VALUES (%pid,'%n','%r','%pn',%i)"
            SQL = SQL.Replace("%pid", ProductID)
            SQL = SQL.Replace("%n", Name)
            SQL = SQL.Replace("%r", Role)
            SQL = SQL.Replace("%pn", PhoneNr)
            If Internal = "True" Then Int = 1 Else Int = 0
            SQL = SQL.Replace("%i", Int)

            Dim com As New SqlClient.SqlCommand(SQL, DBConn)
            com.ExecuteNonQuery()
            DBConn.Close()
        End Using
    End Sub

    Public Overrides Function AddProduct(ByVal Name As String, ByVal ClientID As Integer) As cProduct
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim SQL As String = "INSERT INTO products (Name,ClientID) VALUES ('" & Name & "'," & ClientID & ")"
            Dim com As New SqlClient.SqlCommand(SQL, DBConn)
            com.ExecuteNonQuery()
            Dim ID As Integer
            com.CommandText = "SELECT @@identity"
            ID = com.ExecuteScalar
            DBConn.Close()
            _productsByID = Nothing
            Return GetProductByID(ID)
        End Using
    End Function

    Public Overrides Function AddStaff() As Integer
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim com As New SqlClient.SqlCommand("INSERT INTO Staff (Firstname) VALUES ('')", DBConn)
            com.ExecuteNonQuery()
            com.CommandText = "SELECT @@identity"
            Dim ID As Integer = com.ExecuteScalar
            DBConn.Close()
            _staffList = Nothing
            Return ID
        End Using
    End Function


    Public Overrides Function GetClientByID(ByVal ID As String, Optional ByVal Connection As Data.Common.DbConnection = Nothing) As cClient
        If _clientsByID Is Nothing Then
            Dim rd As SqlClient.SqlDataReader
            Dim CloseConnection As Boolean = False
            If Connection Is Nothing Then
                Connection = Connect(ConnectionMode.cmNetwork)
                CloseConnection = True
            End If
            Dim com As New SqlClient.SqlCommand("SELECT * FROM Clients WHERE ID=" & ID, Connection)
            rd = com.ExecuteReader
            Dim TmpClient As New cClient
            TmpClient.ID = ID
            If rd.Read Then
                TmpClient.Name = rd!Name
                Dim _products As New DataTable
                com = New SqlClient.SqlCommand("SELECT * FROM Products WHERE ClientID=" & ID & " ORDER BY Name", Connection)
                rd.Close()
                rd = com.ExecuteReader
                _products.Load(rd)
                For Each _product As DataRow In _products.Rows
                    Dim TmpProduct As New cProduct(Nothing)
                    TmpProduct.Name = _product!name
                    TmpProduct.ID = _product!id
                    TmpProduct.Client = TmpClient
                    TmpClient.Products.Add(TmpProduct)
                    Dim _contacts As New DataTable
                    com = New SqlClient.SqlCommand("SELECT * FROM contacts WHERE ProductID=" & _product!id, Connection)
                    rd = com.ExecuteReader
                    _contacts.Load(rd)
                    For Each _contact As DataRow In _contacts.Rows
                        If _contact!internal Then
                            Dim TmpContact As cContact = TmpProduct.InternalContacts.Add
                            TmpContact.Name = _contact!name
                            If Not IsDBNull(_contact!PhoneNr) Then TmpContact.PhoneNr = _contact!PhoneNr
                            If Not IsDBNull(_contact!Role) Then TmpContact.Role = _contact!role
                        Else
                            Dim TmpContact As cContact = TmpProduct.ExternalContacts.Add
                            TmpContact.Name = _contact!name
                            If Not IsDBNull(_contact!PhoneNr) Then TmpContact.PhoneNr = _contact!PhoneNr
                            If Not IsDBNull(_contact!Role) Then TmpContact.Role = _contact!role
                        End If
                    Next
                Next
                If CloseConnection Then
                    Connection.Close()
                    Connection.Dispose()
                End If
                Return TmpClient
            Else
                If CloseConnection Then
                    Connection.Close()
                    Connection.Dispose()
                End If
                Return Nothing
            End If
            If CloseConnection Then
                Connection.Close()
                Connection.Dispose()
            End If
        Else
            Return _clientsByID(ID)
        End If
    End Function

    Public Overrides Function GetProductByID(ByVal ID As String, Optional ByVal Connection As Data.Common.DbConnection = Nothing) As cProduct
        If _productsByID Is Nothing Then
            Dim rd As SqlClient.SqlDataReader
            Dim com As SqlClient.SqlCommand
            Dim CloseConnection As Boolean = False
            If Connection Is Nothing Then
                Connection = Connect(ConnectionMode.cmNetwork)
                CloseConnection = True
            End If
            com = New SqlClient.SqlCommand("SELECT * FROM Products WHERE id=" & ID & " ORDER BY Name", Connection)
            rd = com.ExecuteReader

            If rd.Read Then
                Dim TmpProduct As New cProduct(Nothing)
                TmpProduct.Name = rd!name
                TmpProduct.Client = GetClientByID(rd!clientid)
                TmpProduct.ID = ID
                Dim _contacts As New DataTable
                com = New SqlClient.SqlCommand("SELECT * FROM contacts WHERE ProductID=" & ID, Connection)
                rd.Close()
                rd = com.ExecuteReader
                _contacts.Load(rd)
                For Each _contact As DataRow In _contacts.Rows
                    If _contact!internal Then
                        Dim TmpContact As cContact = TmpProduct.InternalContacts.Add
                        TmpContact.Name = _contact!name
                        If Not IsDBNull(_contact!PhoneNr) Then TmpContact.PhoneNr = _contact!PhoneNr
                        If Not IsDBNull(_contact!Role) Then TmpContact.Role = _contact!role
                    Else
                        Dim TmpContact As cContact = TmpProduct.ExternalContacts.Add
                        TmpContact.Name = _contact!name
                        If Not IsDBNull(_contact!PhoneNr) Then TmpContact.PhoneNr = _contact!PhoneNr
                        If Not IsDBNull(_contact!Role) Then TmpContact.Role = _contact!role
                    End If
                Next
                If CloseConnection Then
                    Connection.Close()
                    Connection.Dispose()
                End If
                Return TmpProduct
            Else
                If CloseConnection Then
                    Connection.Close()
                    Connection.Dispose()
                End If
                Return Nothing
            End If
            If CloseConnection Then
                Connection.Close()
                Connection.Dispose()
            End If
        Else
            Return _productsByID(ID)
        End If
    End Function

    Public Overrides Function Clients() As System.Collections.Generic.List(Of cClient)
        Dim _clients As New DataTable
        Dim _list As New List(Of cClient)

        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim rd As SqlClient.SqlDataReader
            Dim com As New SqlClient.SqlCommand("SELECT * FROM Clients ORDER BY Name", DBConn)
            rd = com.ExecuteReader
            _clients.Load(rd)
            For Each _client As DataRow In _clients.Rows
                Dim TmpClient As New cClient
                TmpClient.ID = _client!id
                TmpClient.Name = _client!name
                Dim _products As New DataTable
                com = New SqlClient.SqlCommand("SELECT * FROM Products WHERE ClientID=" & _client!id & " ORDER BY Name", DBConn)
                rd = com.ExecuteReader
                _products.Load(rd)
                For Each _product As DataRow In _products.Rows
                    Dim TmpProduct As New cProduct(Nothing)
                    TmpProduct.Name = _product!name
                    TmpProduct.ID = _product!id
                    TmpProduct.Client = TmpClient
                    TmpClient.Products.Add(TmpProduct)
                    Dim _contacts As New DataTable
                    com = New SqlClient.SqlCommand("SELECT * FROM contacts WHERE ProductID=" & _product!id, DBConn)
                    rd = com.ExecuteReader
                    _contacts.Load(rd)
                    For Each _contact As DataRow In _contacts.Rows
                        If _contact!internal Then
                            Dim TmpContact As cContact = TmpProduct.InternalContacts.Add
                            TmpContact.Name = _contact!name
                            If Not IsDBNull(_contact!PhoneNr) Then TmpContact.PhoneNr = _contact!PhoneNr
                            If Not IsDBNull(_contact!Role) Then TmpContact.Role = _contact!role
                        Else
                            Dim TmpContact As cContact = TmpProduct.ExternalContacts.Add
                            TmpContact.Name = _contact!name
                            If Not IsDBNull(_contact!PhoneNr) Then TmpContact.PhoneNr = _contact!PhoneNr
                            If Not IsDBNull(_contact!Role) Then TmpContact.Role = _contact!role
                        End If
                    Next
                Next
                _list.Add(TmpClient)
            Next
            DBConn.Close()
            Return _list
        End Using
    End Function

    Public Overrides Function Connect(ByVal ConnectionMode As cDBReader.ConnectionMode) As Object
        Dim DBConn As New SqlClient.SqlConnection("Data Source=" & BalthazarSettings.Database & ";Initial Catalog=" & BalthazarSettings.DatabaseDB & ";User ID=balthazar;Password=eventrules;Connection Timeout=30;")
        Try
            DBConn.Open()
            Return DBConn
        Catch ex As SqlClient.SqlException
            Return ex
        End Try
    End Function

    Public Overrides Function Events() As System.Collections.Generic.List(Of cDBReader.EventStruct)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim _events As New DataTable
            Dim _list As New List(Of EventStruct)

            Dim rd As SqlClient.SqlDataReader
            Dim com As New SqlClient.SqlCommand("SELECT ID,Name FROM Events ORDER BY Name", DBConn)
            rd = com.ExecuteReader
            _events.Load(rd)
            For Each _event As DataRow In _events.Rows
                Dim TmpEvent As New EventStruct
                TmpEvent.ID = _event!ID
                TmpEvent.Name = _event!Name
                _list.Add(TmpEvent)
            Next
            DBConn.Close()
            Return _list
        End Using
    End Function

    Public Overrides Function EventTemplates() As System.Collections.Generic.List(Of cDBReader.EventStruct)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim _events As New DataTable
            Dim _list As New List(Of EventStruct)

            Dim rd As SqlClient.SqlDataReader
            Dim com As New SqlClient.SqlCommand("SELECT ID,Name FROM EventTemplates ORDER BY Name", DBConn)
            rd = com.ExecuteReader
            _events.Load(rd)
            For Each _event As DataRow In _events.Rows
                Dim TmpEvent As New EventStruct
                TmpEvent.ID = _event!ID
                TmpEvent.Name = _event!Name
                _list.Add(TmpEvent)
            Next
            DBConn.Close()
            Return _list
        End Using
    End Function

    Public Overrides Function GetAnswersForEvent(ByVal EventID As Integer) As System.Data.DataTable
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim com As New SqlClient.SqlCommand("SELECT Staff.Firstname,Staff.Lastname,QuestionaireAnswers.* FROM Staff,QuestionaireAnswers,Questionaire WHERE Staff.ID=QuestionaireAnswers.StaffID AND QuestionaireAnswers.QuestionaireID=Questionaire.ID AND Answered=1 AND EventID=" & EventID, DBConn)
            Dim rd As SqlClient.SqlDataReader
            Dim dt As New DataTable
            rd = com.ExecuteReader
            dt.Load(rd)
            DBConn.Close()
            Return dt
        End Using
    End Function

    Public Overrides Function GetAssignedStaffForShiftList(ByVal ShiftID As Integer) As System.Collections.Generic.List(Of cStaff)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim _staff As New DataTable
            Dim _list As New List(Of cStaff)
            Dim SQL As String = "SELECT Staff.* FROM eventshiftstaff,staff WHERE eventshiftstaff.staffid=staff.id and eventshiftstaff.shiftid=" & ShiftID
            Dim com As New SqlClient.SqlCommand(SQL, DBConn)
            Dim rd As SqlClient.SqlDataReader

            rd = com.ExecuteReader

            _staff.Load(rd)
            For Each _row As DataRow In _staff.Rows
                Dim TmpStaff As New cStaff
                TmpStaff.DatabaseID = _row!id
                TmpStaff.AccountNo = GetString(_row!AccountNo)
                TmpStaff.Adress1 = GetString(_row!Address1)
                TmpStaff.Adress2 = GetString(_row!Address2)
                TmpStaff.Birthday = _row!Birthday
                TmpStaff.Bank = GetString(_row!bank)
                TmpStaff.ClearingNo = GetString(_row!ClearingNo)
                TmpStaff.Driver = _row!Driver
                TmpStaff.Email = _row!Email
                TmpStaff.Firstname = GetString(_row!FirstName)
                TmpStaff.Gender = GetInt(_row!Gender)
                TmpStaff.HomePhone = GetString(_row!HomePhone)
                TmpStaff.ExternalInfo = GetString(_row!Info)
                TmpStaff.InternalInfo = GetString(_row!internalInfo)
                TmpStaff.LastName = GetString(_row!LastName)
                TmpStaff.MobilePhone = GetString(_row!MobilePhone)
                TmpStaff.WorkPhone = GetString(_row!WorkPhone)
                TmpStaff.ZipArea = GetString(_row!ZipArea)
                TmpStaff.ZipCode = GetString(_row!ZipCode)
                TmpStaff.Username = GetString(_row!Login)
                TmpStaff.Password = GetString(_row!Password)
                _list.Add(TmpStaff)
            Next
            DBConn.Close()
            Return _list
        End Using

    End Function

    Private Function GetString(ByVal Strn As Object) As String
        If IsDBNull(Strn) Then
            Return ""
        Else
            Return Strn
        End If
    End Function

    Private Function GetDate(ByVal [Date] As Object) As Date
        If IsDBNull([Date]) Then
            Return New Date(1900, 1, 1)
        Else
            Return [Date]
        End If
    End Function

    Private Function GetInt(ByVal Val As Object) As Integer
        If IsDBNull(Val) Then
            Return 0
        Else
            Return Val
        End If
    End Function

    Public Overrides Function GetAvailableStaffForShiftList(ByVal ShiftID As Integer) As System.Collections.Generic.List(Of cStaff)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim _staff As New DataTable
            Dim _list As New List(Of cStaff)
            Dim SQL As String = "SELECT Staff.* FROM eventshiftavailablestaff,staff WHERE eventshiftavailablestaff.staffid=staff.id and eventshiftavailablestaff.shiftid=" & ShiftID
            Dim com As New SqlClient.SqlCommand(SQL, DBConn)
            Dim rd As SqlClient.SqlDataReader

            rd = com.ExecuteReader

            _staff.Load(rd)
            For Each _row As DataRow In _staff.Rows
                Dim TmpStaff As New cStaff
                TmpStaff.DatabaseID = _row!id
                TmpStaff.AccountNo = GetString(_row!AccountNo)
                TmpStaff.Adress1 = GetString(_row!Address1)
                TmpStaff.Adress2 = GetString(_row!Address2)
                TmpStaff.Birthday = _row!Birthday
                TmpStaff.Bank = GetString(_row!bank)
                TmpStaff.ClearingNo = GetString(_row!ClearingNo)
                TmpStaff.Driver = Math.Abs(cStaff.DriverEnum.driverB * _row!DriverB) + Math.Abs(cStaff.DriverEnum.driverC * _row!DriverC) + Math.Abs(cStaff.DriverEnum.driverD * _row!DriverD) + Math.Abs(cStaff.DriverEnum.driverE * _row!DriverE)
                TmpStaff.Email = _row!Email
                TmpStaff.Firstname = GetString(_row!FirstName)
                TmpStaff.Gender = GetString(_row!Gender)
                TmpStaff.HomePhone = GetString(_row!HomePhone)
                TmpStaff.ExternalInfo = GetString(_row!Info)
                TmpStaff.InternalInfo = GetString(_row!InternalInfo)
                TmpStaff.LastName = GetString(_row!LastName)
                TmpStaff.MobilePhone = GetString(_row!MobilePhone)
                TmpStaff.WorkPhone = GetString(_row!WorkPhone)
                TmpStaff.ZipArea = GetString(_row!ZipArea)
                TmpStaff.ZipCode = GetString(_row!ZipCode)
                TmpStaff.Username = GetString(_row!Login)
                TmpStaff.Password = GetString(_row!Password)
                _list.Add(TmpStaff)
            Next
            DBConn.Close()
            Return _list
        End Using
    End Function

    Public Overrides Function GetEvent(ByVal EventID As Integer) As cEvent
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim TmpEvent As New cEvent
            Dim com As New SqlClient.SqlCommand("SELECT XML FROM Events WHERE id=" & EventID, DBConn)
            TmpEvent.Load(com.ExecuteScalar)
            TmpEvent.DatabaseID = EventID
            DBConn.Close()
            Return TmpEvent
        End Using
    End Function

    Public Overrides Function GetEventTemplate(ByVal TemplateID As Integer) As cEvent
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim TmpEvent As New cEvent
            Dim com As New SqlClient.SqlCommand("SELECT XML FROM EventTemplates WHERE id=" & TemplateID, DBConn)
            TmpEvent.Load(com.ExecuteScalar)
            TmpEvent.DatabaseID = -1

            For Each TmpCat As cStaffCategory In Database.StaffCategories
                With TmpEvent.Budget.StaffCosts.Add(TmpCat.Name)
                    .Description = TmpCat.Description
                    .CostPerHourCTC = TmpCat.CostPerHourCTC
                    .CostPerHourActual = TmpCat.CostPerHourActual
                End With
            Next
            For Each TmpCost As cHourCost In Database.PlanningCosts
                TmpEvent.Budget.PlanningCosts.Add(TmpCost)
            Next
            For Each TmpCost As cCost In Database.MaterialCosts
                TmpEvent.Budget.MaterialCosts.Add(TmpCost)
            Next
            For Each TmpCost As cCost In Database.LogisticsCosts
                TmpEvent.Budget.LogisticsCosts.Add(TmpCost)
            Next
            DBConn.Close()
            Return TmpEvent
        End Using
    End Function

    Public Overrides Function GetEventStaffLocationRoleList(ByVal EventID As Integer) As System.Data.DataTable
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim com As New SqlClient.SqlCommand("SELECT DISTINCT StaffID,LocationID,RoleID FROM eventavailableshifts,eventshift WHERE eventshift.id=eventavailableshifts.shiftid AND EventID=" & EventID, DBConn)
            Dim rd As SqlClient.SqlDataReader
            Dim dt As New DataTable
            rd = com.ExecuteReader
            dt.Load(rd)
            DBConn.Close()
            Return dt
        End Using
    End Function

    Public Overrides Function RemoveSingleStaff(ByVal StaffID As Integer) As Integer

        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim rd As SqlClient.SqlDataReader
            Dim com As New SqlClient.SqlCommand("DELETE FROM Staff WHERE ID=" & StaffID, DBConn)
            rd = com.ExecuteReader
            DBConn.Close()
        End Using

    End Function

    Public Overrides Function RemoveCampaign(ByVal ThisEvent As cEvent) As Integer

        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim rd As SqlClient.SqlDataReader
            Dim com As New SqlClient.SqlCommand("DELETE FROM Events WHERE ID=" & ThisEvent.DatabaseID, DBConn)
            rd = com.ExecuteReader
            DBConn.Close()
        End Using

    End Function

    Public Overrides Function GetSingleStaff(ByVal StaffID As Integer, Optional ByVal Connection As Data.Common.DbConnection = Nothing) As cStaff
        If _staffList Is Nothing Then
            Dim CloseConnection As Boolean = False
            If Connection Is Nothing Then
                Connection = Connect(ConnectionMode.cmNetwork)
                CloseConnection = True
            End If
            Dim _staff As New DataTable
            Dim rd As SqlClient.SqlDataReader
            Dim com As New SqlClient.SqlCommand("SELECT * FROM Staff WHERE id=" & StaffID, Connection)
            rd = com.ExecuteReader
            If rd.HasRows Then
                _staff.Load(rd)
                Dim TmpStaff As New cStaff
                TmpStaff.DatabaseID = _staff.Rows(0)!id
                TmpStaff.AccountNo = GetString(_staff.Rows(0)!AccountNo)
                TmpStaff.Adress1 = GetString(_staff.Rows(0)!Address1)
                TmpStaff.Adress2 = GetString(_staff.Rows(0)!Address2)
                TmpStaff.Birthday = GetDate(_staff.Rows(0)!Birthday)
                TmpStaff.Bank = GetString(_staff.Rows(0)!bank)
                TmpStaff.ClearingNo = GetString(_staff.Rows(0)!ClearingNo)
                TmpStaff.Driver = Math.Abs(cStaff.DriverEnum.driverB * _staff.Rows(0)!DriverB) + Math.Abs(cStaff.DriverEnum.driverC * _staff.Rows(0)!DriverC) + Math.Abs(cStaff.DriverEnum.driverD * _staff.Rows(0)!DriverD) + Math.Abs(cStaff.DriverEnum.driverE * _staff.Rows(0)!DriverE)
                TmpStaff.Email = GetString(_staff.Rows(0)!Email)
                TmpStaff.Firstname = GetString(_staff.Rows(0)!FirstName)
                TmpStaff.Gender = GetInt(_staff.Rows(0)!Gender)
                TmpStaff.HomePhone = GetString(_staff.Rows(0)!HomePhone)
                TmpStaff.ExternalInfo = GetString(_staff.Rows(0)!Info)
                TmpStaff.InternalInfo = GetString(_staff.Rows(0)!InternalInfo)
                TmpStaff.LastName = GetString(_staff.Rows(0)!LastName)
                TmpStaff.MobilePhone = GetString(_staff.Rows(0)!MobilePhone)
                TmpStaff.WorkPhone = GetString(_staff.Rows(0)!WorkPhone)
                TmpStaff.ZipArea = GetString(_staff.Rows(0)!ZipArea)
                TmpStaff.ZipCode = GetString(_staff.Rows(0)!ZipCode)
                TmpStaff.Username = GetString(_staff.Rows(0)!Login)
                TmpStaff.Password = GetString(_staff.Rows(0)!Password)
                TmpStaff.Type = GetInt(_staff.Rows(0)!Type)
                If Not IsDBNull(_staff.Rows(0)!clientid) AndAlso Not _staff.Rows(0)!clientid = 0 Then
                    TmpStaff.Client = GetClientByID(_staff.Rows(0)!clientID, Connection)
                Else
                    TmpStaff.Client = Nothing
                End If
                If Not IsDBNull(_staff.Rows(0)!Picture) Then
                    Dim TmpStream As New IO.MemoryStream
                    Dim TmpByte() As Byte = _staff.Rows(0)!Picture
                    TmpStream.Write(TmpByte, 0, TmpByte.Length)
                    TmpStaff.Picture = New Bitmap(TmpStream)
                End If
                If CloseConnection Then
                    Connection.Close()
                    Connection.Dispose()
                End If
                Return TmpStaff
            Else
                Return Nothing
            End If
        Else
            If _staffListPrivate.ContainsKey(StaffID) Then
                Return _stafflist(StaffID)
            Else
                Return Nothing
            End If

        End If
    End Function

    Public Property _stafflist()
        Get

            Return _staffListPrivate
        End Get
        Set(ByVal value)
            _staffListPrivate = value
        End Set
    End Property

    Dim _staffListPrivate As Dictionary(Of Integer, cStaff)
    Dim _staffListLastUpdate As Date
    Dim _staffListByClient As New Dictionary(Of Integer, Dictionary(Of Integer, cStaff))
    Dim _staffListByClientLastUpdate As New Dictionary(Of Integer, Date)

    Public Overrides Function GetStaffList(Optional ByVal Type As cStaff.UserTypeEnum = cStaff.UserTypeEnum.Staff, Optional ByVal ClientID As Integer = 0) As System.Collections.Generic.Dictionary(Of Integer, cStaff)
        Dim _staff As New DataTable
        Dim _list As New Dictionary(Of Integer, cStaff)

        Dim rd As SqlClient.SqlDataReader
        Dim com As SqlClient.SqlCommand

        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)

            If ClientID <> 0 Then
                'Get list of staff for a client
                If _staffListByClient.ContainsKey(ClientID) Then
                    com = New SqlClient.SqlCommand("SELECT * FROM Staff WHERE clientid=" & ClientID & " AND TimeSaved>@timesaved ORDER BY LastName", DBConn)
                    com.Parameters.AddWithValue("@timesaved", _staffListByClientLastUpdate(ClientID))
                    _staffListByClientLastUpdate(ClientID) = Now
                    _list = _staffListByClient(ClientID)
                Else
                    com = New SqlClient.SqlCommand("SELECT * FROM Staff WHERE clientid=" & ClientID & " ORDER BY LastName", DBConn)
                    _list = New Dictionary(Of Integer, cStaff)
                    _staffListByClient.Add(ClientID, _list)
                    _staffListByClientLastUpdate.Add(ClientID, Now)
                End If

            Else
                If _staffList Is Nothing Then
                    com = New SqlClient.SqlCommand("SELECT * FROM Staff ORDER BY LastName", DBConn)
                    _list = New Dictionary(Of Integer, cStaff)
                    _staffList = _list
                Else
                    com = New SqlClient.SqlCommand("SELECT * FROM Staff WHERE TimeSaved>@timesaved ORDER BY LastName", DBConn)
                    com.Parameters.AddWithValue("@timesaved", _staffListLastUpdate)
                    _list = _staffList
                End If
                _staffListLastUpdate = Now
            End If
            rd = com.ExecuteReader
            _staff.Load(rd)
            For Each _row As DataRow In _staff.Rows
                Dim TmpStaff As cStaff
                If _list.ContainsKey(_row!id) Then
                    TmpStaff = _list(_row!id)
                Else
                    TmpStaff = New cStaff
                    TmpStaff.DatabaseID = _row!id
                End If
                TmpStaff.AccountNo = GetString(_row!AccountNo)
                TmpStaff.Adress1 = GetString(_row!Address1)
                TmpStaff.Adress2 = GetString(_row!Address2)
                TmpStaff.Birthday = GetDate(_row!Birthday)
                TmpStaff.Bank = GetString(_row!bank)
                TmpStaff.ClearingNo = GetString(_row!ClearingNo)
                TmpStaff.Driver = Math.Abs(cStaff.DriverEnum.driverB * _row!DriverB) + Math.Abs(cStaff.DriverEnum.driverC * _row!DriverC) + Math.Abs(cStaff.DriverEnum.driverD * _row!DriverD) + Math.Abs(cStaff.DriverEnum.driverE * _row!DriverE)
                TmpStaff.Email = GetString(_row!Email)
                TmpStaff.Firstname = GetString(_row!FirstName)
                TmpStaff.Gender = GetInt(_row!Gender)
                TmpStaff.HomePhone = GetString(_row!HomePhone)
                TmpStaff.ExternalInfo = GetString(_row!Info)
                TmpStaff.InternalInfo = GetString(_row!InternalInfo)
                TmpStaff.LastName = GetString(_row!LastName)
                TmpStaff.MobilePhone = GetString(_row!MobilePhone)
                TmpStaff.WorkPhone = GetString(_row!WorkPhone)
                TmpStaff.ZipArea = GetString(_row!ZipArea)
                TmpStaff.ZipCode = GetString(_row!ZipCode)
                TmpStaff.Username = GetString(_row!Login)
                TmpStaff.Password = GetString(_row!Password)
                TmpStaff.Type = GetInt(_row!Type)
                If Not _row!clientid = 0 Then
                    TmpStaff.client = GetClientByID(_row!clientID)
                Else
                    TmpStaff.client = Nothing
                End If
                If Not IsDBNull(_row!Picture) Then
                    Dim TmpStream As New IO.MemoryStream
                    Dim TmpByte() As Byte = _row!Picture
                    TmpStream.Write(TmpByte, 0, TmpByte.Length)
                    TmpStaff.Picture = New Bitmap(TmpStream)
                End If
                TmpStaff.SavedToDB = True
                If Not _list.ContainsKey(TmpStaff.DatabaseID) Then
                    _list.Add(TmpStaff.DatabaseID, TmpStaff)
                End If
            Next
            DBConn.Close()
        End Using
        Dim _typedList As IEnumerable(Of cStaff) = From staff As cStaff In _list.Values Select staff Where staff.Type = Type
        Return _typedList.ToDictionary(Function(s) s.DatabaseID)
    End Function

    Private _loggedInUser As Integer = -1
    Public Overrides Function LoggedInUserID() As Integer
        Return _loggedInUser
    End Function

    Public Overrides Function Login(ByVal username As String, ByVal password As String) As Integer
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim ID As Integer = -1
            Dim com As New SqlClient.SqlCommand("SELECT ID FROM staff WHERE login='%user' AND password='%pass'".Replace("%user", username).Replace("%pass", password), DBConn)
            ID = com.ExecuteScalar
            Return ID
        End Using
    End Function

    Public Overrides Function PlanningCosts() As System.Collections.Generic.List(Of cHourCost)
        Dim _costs As New DataTable
        Dim _list As New List(Of cHourCost)

        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim rd As SqlClient.SqlDataReader
            Dim com As New SqlClient.SqlCommand("SELECT * FROM planningcosts", DBConn)
            rd = com.ExecuteReader
            _costs.Load(rd)
            For Each _cost As DataRow In _costs.Rows
                Dim TmpCost As New cHourCost
                TmpCost.Name = _cost!name
                If Not IsDBNull(_cost!description) Then
                    TmpCost.Description = _cost!description
                Else
                    TmpCost.Description = ""
                End If
                TmpCost.CostPerHourCTC = _cost!costperhour
                _list.Add(TmpCost)
            Next
            DBConn.Close()
            Return _list
        End Using
    End Function

    Public Overrides Function MaterialCosts() As System.Collections.Generic.List(Of cCost)
        Dim _costs As New DataTable
        Dim _list As New List(Of cCost)

        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim rd As SqlClient.SqlDataReader
            Dim com As New SqlClient.SqlCommand("SELECT * FROM materialcosts", DBConn)
            rd = com.ExecuteReader
            _costs.Load(rd)
            For Each _cost As DataRow In _costs.Rows
                Dim TmpCost As New cCost
                TmpCost.Name = _cost!name
                If Not IsDBNull(_cost!description) Then
                    TmpCost.Description = _cost!description
                Else
                    TmpCost.Description = ""
                End If
                TmpCost.CTC = _cost!ctc
                TmpCost.ActualCost = _cost!actualcost
                _list.Add(TmpCost)
            Next
            DBConn.Close()
            Return _list
        End Using
    End Function

    Public Overrides Function LogisticsCosts() As System.Collections.Generic.List(Of cCost)
        Dim _costs As New DataTable
        Dim _list As New List(Of cCost)

        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim rd As SqlClient.SqlDataReader
            Dim com As New SqlClient.SqlCommand("SELECT * FROM logisticscosts", DBConn)
            rd = com.ExecuteReader
            _costs.Load(rd)
            For Each _cost As DataRow In _costs.Rows
                Dim TmpCost As New cCost
                TmpCost.Name = _cost!name
                If Not IsDBNull(_cost!description) Then
                    TmpCost.Description = _cost!description
                Else
                    TmpCost.Description = ""
                End If
                TmpCost.CTC = _cost!ctc
                TmpCost.ActualCost = _cost!actualcost
                _list.Add(TmpCost)
            Next
            DBConn.Close()
            Return _list
        End Using
    End Function


    Public Overrides Sub ResetPassword(ByVal StaffID As Integer)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim TmpPassword As String = ""

            For i As Integer = 1 To 7
                Select Case Int(Rnd() * 2)
                    Case 0
                        TmpPassword &= Chr(Int(Rnd() * 26) + 65)
                    Case 1
                        TmpPassword &= Chr(Int(Rnd() * 26) + 97)
                End Select
                Dim com As New SqlClient.SqlCommand("UPDATE Staff SET Password='" & TmpPassword & "' WHERE ID=" & StaffID, DBConn)
                com.ExecuteNonQuery()

                SendPasswordToUser(StaffID)

            Next
            DBConn.Close()
        End Using
    End Sub

    Public Overrides Sub SaveConfirmedToDB(ByRef Schedule As cStaffSchedule)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            For Each TmpLoc As cStaffScheduleLocation In Schedule.Locations
                For Each TmpRole As cStaffScheduleRole In TmpLoc.Roles
                    For Each TmpDay As cStaffScheduleDay In TmpRole.Days
                        For Each TmpShift As cStaffScheduleShift In TmpDay.Shifts
                            Dim Sql As String = "DELETE FROM eventshiftstaff WHERE ShiftID=" & TmpShift.DatabaseID
                            Dim com As New SqlClient.SqlCommand(Sql, DBConn)
                            com.ExecuteNonQuery()
                            For Each TmpStaff As cStaff In TmpShift.AssignedStaff
                                Sql = "INSERT INTO eventshiftstaff (ShiftID,StaffID) VALUES (" & TmpShift.DatabaseID & "," & TmpStaff.DatabaseID & ")"
                                com.CommandText = Sql
                                com.ExecuteNonQuery()
                            Next
                        Next
                    Next
                Next
            Next
            DBConn.Close()
        End Using
    End Sub

    Public Overrides Function SaveEventToDB(ByVal Name As String, ByVal XML As String, ByVal [Event] As cEvent, ByVal ProductID As Integer, Optional ByVal EventID As Integer = -1) As Integer
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            XML = XML.Replace("'", "''")
            If EventID = -1 Then
                Dim com As New SqlClient.SqlCommand("INSERT INTO Events (Name,Responsible,startdate,enddate,maxbookingsperday,ProductID,XML) VALUES ('" & Name & "','" & BalthazarSettings.UserName & "','" & [Event].StartDate & "','" & [Event].EndDate & "'," & [Event].InStore.MaxBookingsPerDay & "," & ProductID & ",'" & XML & "')", DBConn)
                com.ExecuteNonQuery()
                com.CommandText = "SELECT @@identity"
                Return com.ExecuteScalar
            Else
                Dim com As New SqlClient.SqlCommand("UPDATE Events SET Name='" & Name & "', XML='" & XML & "',Startdate='" & [Event].StartDate & "',Enddate='" & [Event].EndDate & "',MaxBookingsPerDay=" & [Event].InStore.MaxBookingsPerDay & ",ProductID=" & ProductID & " WHERE id=" & EventID, DBConn)
                com.ExecuteScalar()
                Return EventID
            End If
            DBConn.Close()
        End Using
    End Function

    Public Overrides Sub SaveQuestionaireToDB(ByRef Questionaire As cQuestionaire, ByVal EventID As Integer)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Try
                Dim CheckCreateQuestionaires As Boolean = False
                Dim ID As Integer
                Using _com As New SqlClient.SqlCommand()
                    If Questionaire.DatabaseID > -1 Then
                        'Update
                        Dim XMLDoc As New Xml.XmlDocument
                        XMLDoc.AppendChild(Questionaire.CreateXML(XMLDoc))
                        _com.Parameters.AddWithValue("@eid", EventID)
                        _com.Parameters.AddWithValue("@name", Questionaire.Name)
                        _com.Parameters.AddWithValue("@xml", XMLDoc.InnerXml)
                        _com.Parameters.AddWithValue("@id", Questionaire.DatabaseID)
                        _com.CommandText = "UPDATE QuestionaireAnswers SET [xml]=@xml WHERE Answered=0 AND QuestionaireID=@id;UPDATE Questionaire SET EventID=@eid,Name=@name,[xml]=@xml WHERE id=@id;SELECT @id"
                    Else
                        'Insert new
                        Dim XMLDoc As New Xml.XmlDocument
                        XMLDoc.AppendChild(Questionaire.CreateXML(XMLDoc))
                        _com.Parameters.AddWithValue("@eid", EventID)
                        _com.Parameters.AddWithValue("@name", Questionaire.Name)
                        _com.Parameters.AddWithValue("@xml", XMLDoc.InnerXml)
                        _com.Parameters.AddWithValue("@id", Questionaire.DatabaseID)
                        _com.CommandText = "INSERT INTO Questionaire (EventID,Name,[xml]) VALUES (@eid,@name,@xml);SELECT @@identity"
                        CheckCreateQuestionaires = True
                    End If
                    _com.Connection = DBConn
                    ID = _com.ExecuteScalar
                    Questionaire.DatabaseID = ID
                End Using
                If CheckCreateQuestionaires Then
                    For Each TmpBooking As cBooking In GetBookingsForEvent(EventID).Values
                        If TmpBooking.Status = cBooking.BookingStatusEnum.bsConfirmed Then
                            If TmpBooking.Provider IsNot Nothing Then
                                CreateQuestionaires(TmpBooking.DatabaseID, TmpBooking.Provider.DatabaseID)
                            End If
                        End If
                    Next
                End If
            Catch ex As Exception
                DBConn.Close()
                Throw ex
            End Try
            DBConn.Close()
        End Using
    End Sub

    'Public Overrides Sub SaveQuestionaireToDB(ByRef Questionaire As cQuestionaire, ByVal EventID As Integer)
    '    Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
    '        Dim SQL As String
    '        Dim ID As Integer
    '        Dim CheckCreateQuestionaires As Boolean = False
    '        If EventID = -1 Then
    '            Exit Sub
    '        End If
    '        If Questionaire.DatabaseID > -1 Then
    '            'Update
    '            SQL = "UPDATE Questionaire SET EventID=%eid,Name='%name',Instruction='%instr',QuantityText='%qt',InTargetText='%itt',TargetText='%tt',PositiveInTargetText='%pit' WHERE id=%id"
    '        Else
    '            'Insert new
    '            SQL = "INSERT INTO Questionaire (EventID,Name,Instruction,QuantityText,InTargetText,TargetText,PositiveInTargetText) VALUES (%eid,'%name','%instr','%qt','%itt','%tt','%pit')"
    '            CheckCreateQuestionaires = True
    '        End If
    '        SQL = SQL.Replace("%eid", EventID)
    '        SQL = SQL.Replace("%name", Questionaire.Name)
    '        SQL = SQL.Replace("%instr", Questionaire.Instructions)
    '        SQL = SQL.Replace("%qt", Questionaire.QuantityText)
    '        SQL = SQL.Replace("%itt", Questionaire.InTargetText)
    '        SQL = SQL.Replace("%tt", Questionaire.TargetText)
    '        SQL = SQL.Replace("%pit", Questionaire.PositiveText)
    '        SQL = SQL.Replace("%id", Questionaire.DatabaseID)
    '        Dim com As New SqlClient.SqlCommand(SQL, DBConn)
    '        com.ExecuteNonQuery()
    '        If Questionaire.DatabaseID < 0 Then
    '            com.CommandText = "SELECT @@identity"
    '            ID = com.ExecuteScalar
    '        Else
    '            ID = Questionaire.DatabaseID
    '        End If

    '        For Each TmpQuestion As cRatingQuestion In Questionaire.RatingQuestions
    '            If TmpQuestion.DatabaseID > -1 Then
    '                SQL = "UPDATE QuestionaireRating SET QuestionaireID=%qid,[Text]='%text',MeaningOf5Text='%m5',MeaningOf1Text='%m1' WHERE id=%id"
    '            Else
    '                SQL = "INSERT INTO QuestionaireRating (QuestionaireID,[Text],MeaningOf5Text,MeaningOf1Text) VALUES (%qid,'%text','%m5','%m1')"
    '            End If
    '            SQL = SQL.Replace("%qid", ID)
    '            SQL = SQL.Replace("%text", TmpQuestion.Text)
    '            SQL = SQL.Replace("%m5", TmpQuestion.MeaningOf5)
    '            SQL = SQL.Replace("%m1", TmpQuestion.MeaningOf1)
    '            SQL = SQL.Replace("%id", TmpQuestion.DatabaseID)
    '            com.CommandText = SQL
    '            com.ExecuteNonQuery()
    '            If TmpQuestion.DatabaseID = -1 Then
    '                com.CommandText = "SELECT @@identity"
    '                TmpQuestion.DatabaseID = com.ExecuteScalar
    '            End If
    '        Next

    '        For Each TmpQuestion As cCommentQuestion In Questionaire.CommentQuestions
    '            If TmpQuestion.DatabaseID > -1 Then
    '                SQL = "UPDATE QuestionaireComment SET QuestionaireID=%qid,[Text]='%text',MaximumAnswers='%max' WHERE id=%id"
    '            Else
    '                SQL = "INSERT INTO QuestionaireComment (QuestionaireID,[Text],MaximumAnswers) VALUES (%qid,'%text','%max')"
    '            End If
    '            SQL = SQL.Replace("%qid", ID)
    '            SQL = SQL.Replace("%text", TmpQuestion.Text)
    '            SQL = SQL.Replace("%max", TmpQuestion.MaxComments)
    '            SQL = SQL.Replace("%id", TmpQuestion.DatabaseID)
    '            com.CommandText = SQL
    '            com.ExecuteNonQuery()
    '            If TmpQuestion.DatabaseID = -1 Then
    '                com.CommandText = "SELECT @@identity"
    '                TmpQuestion.DatabaseID = com.ExecuteScalar
    '            End If
    '        Next

    '        com.CommandText = "DELETE FROM questionairestaff WHERE QuestionaireID=" & ID
    '        com.ExecuteNonQuery()
    '        For Each TmpStaff As cStaff In Questionaire.SendTo.Values
    '            SQL = "INSERT INTO questionairestaff (QuestionaireID,StaffID) VALUES (%qid,%sid)"
    '            SQL = SQL.Replace("%qid", ID)
    '            SQL = SQL.Replace("%sid", TmpStaff.DatabaseID)
    '            com.CommandText = SQL
    '            com.ExecuteNonQuery()
    '        Next
    '        Questionaire.DatabaseID = ID

    '        If CheckCreateQuestionaires Then
    '            For Each TmpBooking As cBooking In GetBookingsForEvent(EventID).Values
    '                If TmpBooking.Status = cBooking.BookingStatusEnum.bsConfirmed Then
    '                    If TmpBooking.Provider IsNot Nothing Then
    '                        CreateQuestionaires(TmpBooking.DatabaseID, TmpBooking.Provider.DatabaseID)
    '                    End If
    '                End If
    '            Next
    '        End If

    '        DBConn.Close()
    '    End Using
    'End Sub

    Private Sub CreateQuestionaires(ByVal BookingID As Integer, ByVal StaffID As Integer)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "SELECT Questionaire.ID,Questionaire.xml,bookingdates.*,bookings.store,bookings.city FROM Questionaire,Bookings,bookingdates WHERE bookings.id=bookingdates.bookingid and Questionaire.EventID=Bookings.EventID AND Bookings.ID=" & BookingID
                Dim rd As SqlClient.SqlDataReader = com.ExecuteReader
                Dim dt As New DataTable
                dt.Load(rd)
                For Each TmpRow As DataRow In dt.Rows
                    SaveQuestionAnswer(TmpRow!id, False, TmpRow!Date, TmpRow!xml, TmpRow!Store, TmpRow!City, StaffID, BookingID)
                Next
            End Using
        End Using
    End Sub

    Private Function SaveQuestionAnswer(ByVal QID As Integer, ByVal Answered As Boolean, ByVal AtDate As Date, ByVal AnswerXML As String, ByVal Locations As String, ByVal City As String, ByVal StaffID As Integer, ByVal BookingID As Integer, Optional ByVal AnswerID As Integer = -1) As Integer
        Dim SQL As String
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Using com As New SqlClient.SqlCommand("", DBConn)
                If AnswerID = -1 Then
                    SQL = "INSERT INTO QuestionaireAnswers (QuestionaireID,StaffID,BookingID,Answered,[Date],Location,City,[xml]) VALUES (@qid,@sid,@bid,@ans,@date,@loc,@city,@xml)"
                    com.CommandText = SQL
                    com.Parameters.AddWithValue("@qid", QID)
                    com.Parameters.AddWithValue("@sid", StaffID)
                    com.Parameters.AddWithValue("@bid", BookingID)
                    com.Parameters.AddWithValue("@ans", Answered)
                    com.Parameters.AddWithValue("@date", AtDate)
                    com.Parameters.AddWithValue("@loc", Locations)
                    com.Parameters.AddWithValue("@city", City)
                    com.Parameters.AddWithValue("@xml", AnswerXML)
                    com.ExecuteNonQuery()
                    com.CommandText = "SELECT @@identity"
                    Return com.ExecuteScalar
                Else
                    SQL = "UPDATE QuestionaireAnswers SET QuestionaireID=@qid,StaffID=@sid,BookingID=@bid,Answered=@ans,[Date]=@date,Location=@loc,City=@city WHERE id=@aid"
                    com.Parameters.AddWithValue("@qid", QID)
                    com.Parameters.AddWithValue("@sid", StaffID)
                    com.Parameters.AddWithValue("@bid", BookingID)
                    com.Parameters.AddWithValue("@ans", Answered)
                    com.Parameters.AddWithValue("@date", AtDate)
                    com.Parameters.AddWithValue("@loc", Locations)
                    com.Parameters.AddWithValue("@city", City)
                    com.Parameters.AddWithValue("@xml", AnswerXML)
                    com.Parameters.AddWithValue("@aid", AnswerID)
                    com.CommandText = SQL
                    com.ExecuteNonQuery()
                    Return AnswerID
                End If
            End Using
            DBConn.Close()
        End Using
    End Function

    Private Locations As String
    Private Roles As String
    Private Shifts As String

    Public Overrides Sub SaveScheduleToDB(ByRef Schedule As cStaffSchedule)
        Dim SQL As String

        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Locations = ""
            Roles = ""
            Shifts = ""
            For Each TmpLoc As cStaffScheduleLocation In Schedule.Locations
                If TmpLoc.DatabaseID > -1 Then
                    Locations &= TmpLoc.DatabaseID & ","
                    UpdateLocation(TmpLoc)
                Else
                    TmpLoc.DatabaseID = InsertLocation(TmpLoc)
                    Locations &= TmpLoc.DatabaseID & ","
                End If
            Next

            'Deletes Locations no longer in the Event

            Locations = Locations.Substring(0, Locations.Length - 1)
            SQL = "DELETE FROM eventlocations WHERE id NOT IN (" & Locations & ") and eventid=" & Schedule.RootEvent.DatabaseID
            Dim com As New SqlClient.SqlCommand(SQL, DBConn)
            com.ExecuteNonQuery()

            'Deletes Roles no longer in the Event

            Roles = Roles.Substring(0, Roles.Length - 1)
            SQL = "DELETE FROM locationroles WHERE locationid IN (" & Locations & ") AND id NOT IN (" & Roles & ")"
            com.CommandText = SQL
            com.ExecuteNonQuery()

            'Deletes Shifts no longer in the Event

            Shifts = Shifts.Substring(0, Shifts.Length - 1)
            SQL = "DELETE FROM eventshift WHERE id NOT IN (" & Shifts & ") and eventid=" & Schedule.RootEvent.DatabaseID
            com.CommandText = SQL
            com.ExecuteNonQuery()

            SQL = "DELETE FROM eventshiftavailablestaff WHERE shiftid NOT IN (SELECT id FROM eventshift)"
            com.CommandText = SQL
            com.ExecuteNonQuery()
            DBConn.Close()
        End Using
    End Sub

    Private Sub UpdateLocation(ByVal Location As cStaffScheduleLocation)
        Dim SQL As String

        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            SQL = "UPDATE eventlocations SET Name='" & Location.Name & "' WHERE id=" & Location.DatabaseID
            Dim com As New SqlClient.SqlCommand(SQL, DBConn)
            com.ExecuteNonQuery()

            For Each TmpRole As cStaffScheduleRole In Location.Roles
                If TmpRole.DatabaseID > -1 Then
                    Roles &= TmpRole.DatabaseID & ","
                    UpdateRole(TmpRole)
                Else
                    TmpRole.DatabaseID = InsertRole(TmpRole)
                    Roles &= TmpRole.DatabaseID & ","
                End If
            Next
            DBConn.Close()
        End Using
    End Sub

    Sub UpdateRole(ByVal Role As cStaffScheduleRole)
        Dim SQL As String

        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            SQL = "UPDATE locationroles SET Name='" & Role.Name & "',Description='" & Role.Role.Description & "',CategoryID=" & Role.Role.Category.DatabaseID & " WHERE id=" & Role.DatabaseID
            Dim com As New SqlClient.SqlCommand(SQL, DBConn)
            com.ExecuteNonQuery()

            SQL = "DELETE FROM roleavailableforstaff WHERE roleid=" & Role.DatabaseID
            com.CommandText = SQL
            com.ExecuteNonQuery()

            InsertStaffForRole(Role)

            For Each TmpDay As cStaffScheduleDay In Role.Days
                For Each TmpShift As cStaffScheduleShift In TmpDay.Shifts
                    If TmpShift.Shift.Roles(TmpShift.Day.Role.Role.ID).Quantity > 0 Then
                        If TmpShift.DatabaseID > -1 Then
                            Shifts &= TmpShift.DatabaseID & ","
                            UpdateShift(TmpShift)
                        Else
                            TmpShift.DatabaseID = InsertShift(TmpShift)
                            Shifts &= TmpShift.DatabaseID & ","
                        End If
                    End If
                Next
            Next
            DBConn.Close()
        End Using
    End Sub

    Private Sub UpdateShift(ByVal Shift As cStaffScheduleShift)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim Sql As String = "UPDATE eventshift SET eventid=%eid,roleid=%rid,[date]='%date',startmam=%start,endmam=%end WHERE id=" & Shift.DatabaseID
            Sql = Sql.Replace("%eid", Shift.RootEvent.DatabaseID)
            Sql = Sql.Replace("%rid", Shift.Day.Role.DatabaseID)
            Sql = Sql.Replace("%date", Shift.Day.Day.DayDate)
            Sql = Sql.Replace("%start", Helper.Time2Mam(Shift.Shift.StartTime))
            Sql = Sql.Replace("%end", Helper.Time2Mam(Shift.Shift.EndTime))

            Dim com As New SqlClient.SqlCommand(Sql, DBConn)
            com.ExecuteNonQuery()
            DBConn.Close()
        End Using
    End Sub

    Private Function InsertShift(ByVal Shift As cStaffScheduleShift) As Integer
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim Sql As String = "INSERT INTO eventshift (eventid,roleid,[date],startmam,endmam) VALUES (%eid,%rid,'%date',%start,%end)"
            Sql = Sql.Replace("%eid", Shift.RootEvent.DatabaseID)
            Sql = Sql.Replace("%rid", Shift.Day.Role.DatabaseID)
            Sql = Sql.Replace("%date", Shift.Day.Day.DayDate)
            Sql = Sql.Replace("%start", Helper.Time2Mam(Shift.Shift.StartTime))
            Sql = Sql.Replace("%end", Helper.Time2Mam(Shift.Shift.EndTime))

            Dim com As New SqlClient.SqlCommand(Sql, DBConn)
            com.ExecuteNonQuery()

            com.CommandText = "SELECT @@identity"
            Dim TmpID As Integer = com.ExecuteScalar()
            DBConn.Close()
            Return TmpID
        End Using
    End Function

    Private Function InsertRole(ByVal Role As cStaffScheduleRole) As Integer
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim SQL As String = "INSERT INTO locationroles (locationid,name,description) VALUES (%lid,'%name','%dscr')"
            SQL = SQL.Replace("%lid", Role.Location.DatabaseID)
            SQL = SQL.Replace("%name", Role.Name)
            SQL = SQL.Replace("%dscr", Role.Role.Description)
            Dim com As New SqlClient.SqlCommand(SQL, DBConn)
            com.ExecuteNonQuery()

            com.CommandText = "SELECT @@identity"
            Dim TmpID As Integer = com.ExecuteScalar()
            Role.DatabaseID = TmpID

            InsertStaffForRole(Role)

            For Each TmpDay As cStaffScheduleDay In Role.Days
                For Each TmpShift As cStaffScheduleShift In TmpDay.Shifts
                    If TmpShift.DatabaseID > -1 Then
                        Shifts &= TmpShift.DatabaseID & ","
                        UpdateShift(TmpShift)
                    Else
                        TmpShift.DatabaseID = InsertShift(TmpShift)
                        Shifts &= TmpShift.DatabaseID & ","
                    End If
                Next
            Next
            DBConn.Close()
            Return TmpID
        End Using
    End Function

    Private Function InsertLocation(ByVal Location As cStaffScheduleLocation) As Integer
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim SQL As String = "INSERT INTO eventlocations (eventid,name) VALUES (%eid,'%name')"
            SQL = SQL.Replace("%eid", Location.RootEvent.DatabaseID)
            SQL = SQL.Replace("%name", Location.Name)
            Dim com As New SqlClient.SqlCommand(SQL, DBConn)
            com.ExecuteNonQuery()

            com.CommandText = "SELECT @@identity"
            Dim TmpID As Integer = com.ExecuteScalar()
            Location.DatabaseID = TmpID

            For Each TmpRole As cStaffScheduleRole In Location.Roles
                If TmpRole.DatabaseID > -1 Then
                    Roles &= TmpRole.DatabaseID & ","
                    UpdateRole(TmpRole)
                Else
                    TmpRole.DatabaseID = InsertRole(TmpRole)
                    Roles &= TmpRole.DatabaseID & ","
                End If
            Next
            DBConn.Close()
            Return TmpID
        End Using
    End Function

    Private Sub InsertStaffForRole(ByVal Role As cStaffScheduleRole)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim SQL As String
            SQL = "DELETE FROM roleavailableforstaff WHERE roleid=" & Role.DatabaseID
            Dim com As New SqlClient.SqlCommand(SQL, DBConn)
            com.ExecuteNonQuery()

            For Each TmpStaff As cStaff In Role.AvailableForStaff
                SQL = "INSERT INTO roleavailableforstaff (RoleID,StaffID) VALUES (%rid,%sid)"
                SQL = SQL.Replace("%rid", Role.DatabaseID)
                SQL = SQL.Replace("%sid", TmpStaff.DatabaseID)
                com.CommandText = SQL
                com.ExecuteNonQuery()
            Next
            DBConn.Close()
        End Using
    End Sub

    Public Overrides Function SaveStaff(ByVal Staff As cStaff) As Boolean
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Try
                Dim SQL As String
                SQL = "UPDATE Staff SET clientID=%prodid,Type=%type,AccountNo='%acc',Address1='%ad1',Address2='%ad2',Birthday='%bd',Bank='%bnk',ClearingNo='%cno',DriverB=%drvB,DriverC=%drvC,DriverD=%drvD,DriverE=%drvE,Email='%mail',FirstName='%fn',Gender=%gnd,HomePhone='%hp',Info='%inf',InternalInfo='%intinf', LastName='%ln',MobilePhone='%mp',Workphone='%wp',ZipCode='%zip',ZipArea='%city',Login='%login',Password='%pwd',Picture=@pic,TimeSaved=@timesaved WHERE id=%id"
                SQL = SQL.Replace("%acc", Staff.AccountNo)
                SQL = SQL.Replace("%ad1", Staff.Adress1)
                SQL = SQL.Replace("%ad2", Staff.Adress2)
                SQL = SQL.Replace("%bd", Staff.Birthday)
                SQL = SQL.Replace("%bnk", Staff.Bank)
                SQL = SQL.Replace("%cno", Staff.ClearingNo)
                SQL = SQL.Replace("%drvB", Staff.Driver And cStaff.DriverEnum.driverB)
                SQL = SQL.Replace("%drvC", Staff.Driver And cStaff.DriverEnum.driverC)
                SQL = SQL.Replace("%drvD", Staff.Driver And cStaff.DriverEnum.driverD)
                SQL = SQL.Replace("%drvE", Staff.Driver And cStaff.DriverEnum.driverE)
                SQL = SQL.Replace("%mail", Staff.Email)
                SQL = SQL.Replace("%fn", Staff.Firstname)
                SQL = SQL.Replace("%gnd", Staff.Gender)
                SQL = SQL.Replace("%hp", Staff.HomePhone)
                SQL = SQL.Replace("%inf", Staff.ExternalInfo)
                SQL = SQL.Replace("%intinf", Staff.InternalInfo)
                SQL = SQL.Replace("%ln", Staff.LastName)
                SQL = SQL.Replace("%mp", Staff.MobilePhone)
                SQL = SQL.Replace("%wp", Staff.WorkPhone)
                SQL = SQL.Replace("%zip", Staff.ZipCode)
                SQL = SQL.Replace("%city", Staff.ZipArea)
                SQL = SQL.Replace("%id", Staff.DatabaseID)
                SQL = SQL.Replace("%login", Staff.Username)
                SQL = SQL.Replace("%pwd", Staff.Password)
                If Staff.Client IsNot Nothing Then
                    SQL = SQL.Replace("%prodid", Staff.Client.ID)
                Else
                    SQL = SQL.Replace("%prodid", 0)
                End If
                SQL = SQL.Replace("%type", Staff.Type)

                Dim com As New SqlClient.SqlCommand(SQL, DBConn)
                com.Parameters.AddWithValue("@timesaved", Now)
                If Staff.Picture IsNot Nothing Then
                    Dim TmpFN As String = My.Computer.FileSystem.GetTempFileName
                    Staff.Picture.Save(TmpFN)
                    Dim TmpStream As New IO.FileStream(TmpFN, IO.FileMode.Open)
                    Dim TmpByte(TmpStream.Length) As Byte
                    TmpStream.Read(TmpByte, 0, TmpStream.Length)
                    com.Parameters.AddWithValue("@pic", TmpByte)
                    TmpStream.Close()
                    Kill(TmpFN)
                Else
                    Dim TmpPar As New SqlClient.SqlParameter("@pic", SqlDbType.Image)
                    TmpPar.Value = System.DBNull.Value
                    com.Parameters.Add(TmpPar)
                End If
                com.ExecuteNonQuery()
                Staff.SavedToDB = True
                DBConn.Close()
                Return True

            Catch ex As Exception
                DBConn.Close()
                Return False
            End Try
        End Using
    End Function

    Public Overrides Sub SendPasswordToUser(ByVal StaffID As Integer)

    End Sub

    Public Overrides Function StaffCategories() As System.Collections.Generic.List(Of cStaffCategory)
        Dim _categories As New DataTable
        Dim _list As New List(Of cStaffCategory)

        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim rd As SqlClient.SqlDataReader
            Dim com As New SqlClient.SqlCommand("SELECT * FROM staffcategories", DBConn)
            rd = com.ExecuteReader
            _categories.Load(rd)
            For Each _cat As DataRow In _categories.Rows
                Dim TmpCat As New cStaffCategory(Nothing)
                TmpCat.Name = _cat!name
                If Not IsDBNull(_cat!description) Then
                    TmpCat.Description = _cat!description
                Else
                    TmpCat.Description = ""
                End If
                TmpCat.CostPerHourActual = _cat!actualcostperhour
                TmpCat.CostPerHourCTC = _cat!costperhour
                TmpCat.DatabaseID = _cat!ID
                _list.Add(TmpCat)
            Next
            DBConn.Close()
            Return _list
        End Using
    End Function

    Public Overrides Function SumAnswersForEvent(ByVal EventID As Integer) As System.Data.DataRow
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim rd As SqlClient.SqlDataReader
            Dim dt As New DataTable
            Dim TmpDT As New DataTable
            Dim com As New SqlClient.SqlCommand("SELECT * FROM SumAnswersForEvent(" & EventID & ")", DBConn)
            rd = com.ExecuteReader
            dt.Load(rd)
            DBConn.Close()
            Return dt.Rows(0)
        End Using
    End Function

    Public Overrides Function SumRatingAnswersForEvent(ByVal EventID As Integer) As System.Data.DataTable
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim com As New SqlClient.SqlCommand("SELECT Text,Avg(CONVERT(DECIMAL(18,5),Answer)) as AvgRating FROM QuestionaireRatingAnswers,QuestionaireRating,Questionaire WHERE QuestionaireRatingID=QuestionaireRating.ID AND Questionaire.ID=QuestionaireID AND EventID=" & EventID & " GROUP BY Text", DBConn)
            Dim rd As SqlClient.SqlDataReader
            Dim dt As New DataTable
            rd = com.ExecuteReader
            dt.Load(rd)
            DBConn.Close()
            Return dt
        End Using
    End Function

    Public Overrides Function GetSelectedCategoriesForStaff(ByVal StaffID As Integer) As System.Collections.Generic.List(Of Integer)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim com As New SqlClient.SqlCommand("SELECT CategoryID FROM staffcategoriesavailablestaff WHERE StaffID=" & StaffID, DBConn)
            Dim rd As SqlClient.SqlDataReader
            Dim dt As New DataTable
            rd = com.ExecuteReader
            Dim TmpList As New List(Of Integer)
            While rd.Read
                TmpList.Add(rd!CategoryID)
            End While
            DBConn.Close()
            Return TmpList
        End Using
    End Function

    Public Overrides Function GetCVForStaff(ByVal StaffID As Integer) As System.Collections.Generic.List(Of cCVEntry)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim com As New SqlClient.SqlCommand("SELECT events.id as EventID,events.name as EventName,events.Responsible,locationroles.Name as Role,staffcategories.Name as Category,SUM(eventshift.endmam-eventshift.startmam) as minutes FROM Events, eventlocations, locationroles, StaffCategories, eventshift, eventshiftstaff  WHERE eventlocations.eventid = Events.id And locationroles.locationid = eventlocations.id And eventshift.roleid = locationroles.id And eventshift.id = eventshiftstaff.ShiftID And locationroles.CategoryID = StaffCategories.id And eventshiftstaff.StaffID = " & StaffID & " GROUP BY events.ID,events.name,events.Responsible,locationroles.Name,staffcategories.name", DBConn)
            Dim rd As SqlClient.SqlDataReader = com.ExecuteReader
            Dim TmpList As New List(Of cCVEntry)
            While rd.Read
                TmpList.Add(New cCVEntry With {.EventName = GetString(rd!eventname), .EventDatabaseID = GetInt(rd!EventID), .EventRole = GetString(rd!Role), .ResponsiblePerson = GetString(rd!Responsible), .RoleCategory = GetString(rd!Category), .WorkedMinutes = GetInt(rd!minutes)})
            End While
            DBConn.Close()
            Return TmpList
        End Using
    End Function

    Public Overrides Sub SaveInStoreToDB(ByVal InStore As cInStore)


        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim com As New SqlClient.SqlCommand("DELETE FROM eventexcludeddays WHERE eventid=" & InStore.Event.DatabaseID, DBConn)
            com.ExecuteNonQuery()
            For Each TmpDate As Date In InStore.ExcludedDates
                com.CommandText = "INSERT INTO eventexcludeddays (eventid,day) VALUES (@eid,@day)"
                com.Parameters.Clear()
                com.Parameters.AddWithValue("@eid", InStore.Event.DatabaseID)
                com.Parameters.AddWithValue("@day", TmpDate)
                com.ExecuteNonQuery()
            Next

            com.CommandText = "DELETE FROM eventstaff WHERE eventid=" & InStore.Event.DatabaseID
            com.ExecuteNonQuery()
            For Each TmpStaff As cChosenSalesPerson In InStore.ChosenSalespersons
                com.CommandText = "INSERT INTO eventstaff (eventid,staffid,instore_maxdays) VALUES (@eid,@sid,@maxdays)"
                com.Parameters.Clear()
                com.Parameters.AddWithValue("@eid", InStore.Event.DatabaseID)
                com.Parameters.AddWithValue("@sid", TmpStaff.Staff.DatabaseID)
                com.Parameters.AddWithValue("@maxdays", TmpStaff.MaxDays)
                com.ExecuteNonQuery()
            Next
            For Each TmpStaff As cStaff In InStore.ChosenProviders
                com.CommandText = "INSERT INTO eventstaff (eventid,staffid) VALUES (@eid,@sid)"
                com.Parameters.Clear()
                com.Parameters.AddWithValue("@eid", InStore.Event.DatabaseID)
                com.Parameters.AddWithValue("@sid", TmpStaff.DatabaseID)
                com.ExecuteNonQuery()
            Next

            com.CommandText = "DELETE FROM docs WHERE eventid=" & InStore.Event.DatabaseID
            com.ExecuteNonQuery()
            If InStore.DemoInstructions IsNot Nothing Then
                com.CommandText = "INSERT INTO docs (eventid,[document],doctype,name,description,showproviders) VALUES (@eid,@doc,@dt,@name,@desc,1)"
                com.Parameters.Clear()
                com.Parameters.AddWithValue("@eid", InStore.Event.DatabaseID)

                com.Parameters.AddWithValue("@doc", InStore.DemoInstructions.Data)
                com.Parameters.AddWithValue("@dt", InStore.DemoInstructions.DocType)
                com.Parameters.AddWithValue("@name", InStore.DemoInstructions.Name)
                com.Parameters.AddWithValue("@desc", InStore.DemoInstructions.Description.ToString)

                com.ExecuteNonQuery()
            End If
            DBConn.Close()
        End Using
    End Sub

    Private Function GetProductsForBooking(ByVal BookingID As Integer, ByVal Connection As SqlClient.SqlConnection) As List(Of String)
        Dim TmpList As New List(Of String)
        Dim com As New SqlClient.SqlCommand("SELECT bookingproducts.* FROM bookingproducts WHERE bookingid=" & BookingID, Connection)
        Dim rd As SqlClient.SqlDataReader = com.ExecuteReader
        While rd.Read
            TmpList.Add(rd!product)
        End While
        rd.Close()
        Return TmpList
    End Function

    Public Overrides Function GetBookingsForEvent(ByVal EventID As String, Optional ByVal Connection As Data.Common.DbConnection = Nothing) As System.Collections.Generic.Dictionary(Of Integer, cBooking)
        Dim CloseConnection As Boolean = False
        If Connection Is Nothing Then
            Connection = Connect(ConnectionMode.cmNetwork)
            CloseConnection = True
        End If
        Dim TmpList As Dictionary(Of Integer, cBooking) = (From Booking As cBooking In GetAllBookings() Select Booking Where Booking.EventID = EventID).ToDictionary(Function(b) b.DatabaseID)
        Return TmpList
    End Function

    Private Function GetDatesForBooking(ByVal BookingID As Integer, ByVal Connection As SqlClient.SqlConnection) As List(Of cBooking.DateTime)
        Dim com As New SqlClient.SqlCommand("SELECT * FROM bookingdates WHERE bookingid=" & BookingID & " order by [date]", Connection)
        Dim dateRD As SqlClient.SqlDataReader = com.ExecuteReader
        Dim TmpList As New List(Of cBooking.DateTime)
        While dateRD.Read
            Dim TmpDT As New cBooking.DateTime
            TmpDT.Date = dateRD!Date
            TmpDT.Time = dateRD!time
            TmpList.Add(TmpDT)
        End While
        dateRD.Close()
        Return TmpList
    End Function

    Friend Overrides Sub SaveBookingData(ByVal BookingData As System.Collections.Generic.Dictionary(Of Integer, cEvent.BookingDataStruct))
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            For Each kv As KeyValuePair(Of Integer, cEvent.BookingDataStruct) In BookingData
                Dim com As New SqlClient.SqlCommand("UPDATE bookings SET confirmed=@conf,chosenprovider=@prov,chosenprovidername=@provname,rejectioncomment=@rejcom,timesaved=@timesaved WHERE id=@id", DBConn)
                com.Parameters.Clear()
                com.Parameters.AddWithValue("@id", kv.Key)
                com.Parameters.AddWithValue("@conf", kv.Value.Status)
                com.Parameters.AddWithValue("@rejcom", kv.Value.RejectionComment)
                com.Parameters.AddWithValue("@timesaved", Now)
                If kv.Value.Provider Is Nothing Then
                    com.Parameters.AddWithValue("@prov", 0)
                Else
                    com.Parameters.AddWithValue("@prov", kv.Value.Provider.DatabaseID)
                End If
                com.Parameters.AddWithValue("@provname", kv.Value.ProviderName)
                com.ExecuteNonQuery()
            Next
            DBConn.Close()
        End Using
    End Sub

    Public Overrides Function SetBookingIsInvoiced(ByVal BookingID As Integer, ByVal Invoiced As Boolean)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim com As New SqlClient.SqlCommand("UPDATE bookings SET Invoiced='" & Invoiced & "',timesaved=@timesaved WHERE id=" & BookingID, DBConn)
            com.Parameters.AddWithValue("@timesaved", Now)
            If _bookingList IsNot Nothing AndAlso _bookingList.ContainsKey(BookingID) Then
                _bookingList(BookingID).Invoiced = Invoiced
            End If
            com.ExecuteNonQuery()
            DBConn.Close()
        End Using
    End Function

    Public Overrides Function GetRatingAnswersForEvent(ByVal EventID As Integer, ByVal AnswerID As Integer) As System.Data.DataTable
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim com As New SqlClient.SqlCommand("SELECT Text,Answer Rating FROM QuestionaireRatingAnswers,QuestionaireRating,Questionaire WHERE QuestionaireRatingID=QuestionaireRating.ID AND Questionaire.ID=QuestionaireID AND EventID=" & EventID & " AND QuestionaireRatingAnswers.AnswerID=" & AnswerID, DBConn)
            Dim rd As SqlClient.SqlDataReader
            Dim dt As New DataTable
            rd = com.ExecuteReader
            dt.Load(rd)
            DBConn.Close()
            Return dt
        End Using
    End Function

    Public Overrides Function GetCommentAnswersForEvent(ByVal EventID As Integer, Optional ByVal AnswerID As Integer = -1) As System.Collections.Generic.SortedList(Of String, String)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim com As SqlClient.SqlCommand
            If AnswerID > -1 Then
                com = New SqlClient.SqlCommand("SELECT * FROM QuestionaireCommentAnswers WHERE AnswerID=" & AnswerID, DBConn)
            Else
                com = New SqlClient.SqlCommand("SELECT QuestionaireCommentAnswers.* FROM QuestionaireCommentAnswers,QuestionaireComment,Questionaire WHERE QuestionaireCommentAnswers.QuestionaireCommentID=QuestionaireComment.ID AND QuestionaireComment.QuestionaireID=Questionaire.ID AND Questionaire.EventID=" & EventID, DBConn)
            End If
            Dim rd As SqlClient.SqlDataReader
            Dim dt As New DataTable
            rd = com.ExecuteReader
            dt.Load(rd)
            Dim TmpList As New SortedList(Of String, String)
            For Each TmpRow As DataRow In dt.Rows
                For i As Integer = 1 To 10
                    If TmpRow.Item("Text" & i) <> "" Then
                        TmpList.Add(TmpRow.Item("Text" & i), TmpRow.Item("Text" & i))
                    End If
                Next
            Next
            DBConn.Close()
            Return TmpList
        End Using
    End Function

    Dim _bookingList As Dictionary(Of Integer, cBooking)
    Dim _bookingListLastUpdate As Date
    Public Overrides Function GetAllBookings() As System.Collections.Generic.List(Of cBooking)
        Dim TmpList As New Dictionary(Of Integer, cBooking)
        If _bookingList Is Nothing Then
            Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
                Dim com As New SqlClient.SqlCommand("SELECT bookings.*,events.name as EventName,Events.Responsible,Products.ClientID FROM bookings,events,Products WHERE bookings.eventid=events.id and events.productid=products.id", DBConn)
                Dim rd As SqlClient.SqlDataReader = com.ExecuteReader
                Dim dt As New DataTable
                dt.Load(rd)
                rd.Close()
                Dim row As Integer = 1
                For Each TmpRow As DataRow In dt.Rows
                    RaiseEvent GetAllBookingsProgress((row / dt.Rows.Count) * 100)
                    Dim NoData As Boolean = False
                    If MyEvent Is Nothing OrElse MyEvent.BookingData.ContainsKey(TmpRow!id) Then
                        NoData = True
                    End If
                    Dim TmpBooking As New cBooking(TmpRow!id)
                    TmpBooking.Dates = GetDatesForBooking(TmpRow!id, DBConn)
                    TmpBooking.City = TmpRow!city
                    If TmpRow!Providerid > 0 Then
                        If GetSingleStaff(TmpRow!providerid, DBConn) Is Nothing Then
                            Continue For
                        End If
                        TmpBooking.RequestedProvider = GetSingleStaff(TmpRow!providerid, DBConn).Firstname
                    ElseIf TmpRow!providerid = 0 Then
                        TmpBooking.RequestedProvider = "-"
                    Else
                        TmpBooking.RequestedProvider = TmpRow!otherprovider
                    End If
                    If TmpRow!chosenprovider > 0 Then
                        TmpBooking.Provider = GetSingleStaff(TmpRow!chosenprovider, DBConn)
                    Else
                        TmpBooking.Provider = Nothing
                    End If
                    TmpBooking.ProviderName = GetString(TmpRow!chosenprovidername)
                    TmpBooking.Salesperson = GetSingleStaff(TmpRow!staffid, DBConn)
                    TmpBooking.Store = TmpRow!store
                    TmpBooking.Address = TmpRow!address
                    TmpBooking.Contact = TmpRow!Contact
                    TmpBooking.Status = TmpRow!confirmed
                    TmpBooking.PhoneNr = TmpRow!phone
                    TmpBooking.Products = GetProductsForBooking(TmpRow!id, DBConn)
                    TmpBooking.Placement = TmpRow!placement
                    TmpBooking.Comments = TmpRow!comments
                    TmpBooking.RejectionComment = GetString(TmpRow!rejectioncomment)
                    TmpBooking.EventID = TmpRow!eventid
                    TmpBooking.EventName = TmpRow!EventName
                    TmpBooking.Responsible = GetString(TmpRow!Responsible)
                    TmpBooking.Client = GetClientByID(TmpRow!ClientID, DBConn)
                    TmpBooking.ProviderConfirmed = TmpRow!confirmedbyprovider
                    TmpBooking.Invoiced = TmpRow!Invoiced
                    TmpBooking.AnsweredQuestionaireDays = GetAnsweredDaysBooking(TmpRow!ID, DBConn)
                    If Not NoData Then
                        MyEvent.BookingData(TmpRow!id).Status = TmpBooking.Status
                        MyEvent.BookingData(TmpRow!id).Provider = TmpBooking.Provider
                        MyEvent.BookingData(TmpRow!id).ProviderName = TmpBooking.ProviderName
                        MyEvent.BookingData(TmpRow!id).RejectionComment = TmpBooking.RejectionComment
                    End If
                    TmpList.Add(TmpBooking.DatabaseID, TmpBooking)
                    row += 1
                Next
        DBConn.Close()
        _bookingList = TmpList
        _bookingListLastUpdate = Now
            End Using
        Else
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim com As New SqlClient.SqlCommand("SELECT bookings.*,events.name as EventName,Events.Responsible,Products.ClientID FROM bookings,events,Products WHERE bookings.timesaved>@timesaved and bookings.eventid=events.id and events.productid=products.id", DBConn)
            com.Parameters.AddWithValue("@timesaved", _bookingListLastUpdate)
            Dim rd As SqlClient.SqlDataReader = com.ExecuteReader
            Dim dt As New DataTable
            dt.Load(rd)
            rd.Close()
            Dim row As Integer = 1
            For Each TmpRow As DataRow In dt.Rows
                RaiseEvent GetAllBookingsProgress((row / dt.Rows.Count) * 100)
                Dim NoData As Boolean = False
                If MyEvent.BookingData.ContainsKey(TmpRow!id) Then
                    NoData = True
                End If
                Dim TmpBooking As New cBooking(TmpRow!id)
                TmpBooking.Dates = GetDatesForBooking(TmpRow!id, DBConn)
                TmpBooking.City = TmpRow!city
                If TmpRow!Providerid > 0 Then
                    TmpBooking.RequestedProvider = GetSingleStaff(TmpRow!providerid, DBConn).Firstname
                ElseIf TmpRow!providerid = 0 Then
                    TmpBooking.RequestedProvider = "-"
                Else
                    TmpBooking.RequestedProvider = TmpRow!otherprovider
                End If
                If TmpRow!chosenprovider > 0 Then
                    TmpBooking.Provider = GetSingleStaff(TmpRow!chosenprovider, DBConn)
                Else
                    TmpBooking.Provider = Nothing
                End If
                TmpBooking.ProviderName = GetString(TmpRow!chosenprovidername)
                TmpBooking.Salesperson = GetSingleStaff(TmpRow!staffid, DBConn)
                TmpBooking.Store = TmpRow!store
                TmpBooking.Address = TmpRow!address
                TmpBooking.Contact = TmpRow!Contact
                TmpBooking.Status = TmpRow!confirmed
                TmpBooking.PhoneNr = TmpRow!phone
                TmpBooking.Products = GetProductsForBooking(TmpRow!id, DBConn)
                TmpBooking.Placement = TmpRow!placement
                TmpBooking.Comments = TmpRow!comments
                TmpBooking.RejectionComment = GetString(TmpRow!rejectioncomment)
                TmpBooking.EventName = TmpRow!EventName
                TmpBooking.EventID = TmpRow!EventID
                TmpBooking.Responsible = GetString(TmpRow!Responsible)
                TmpBooking.Client = GetClientByID(TmpRow!ClientID, DBConn)
                TmpBooking.ProviderConfirmed = TmpRow!confirmedbyprovider
                TmpBooking.AnsweredQuestionaireDays = GetAnsweredDaysBooking(TmpRow!ID, DBConn)
                If Not NoData Then
                    MyEvent.BookingData(TmpRow!id).Status = TmpBooking.Status
                    MyEvent.BookingData(TmpRow!id).Provider = TmpBooking.Provider
                    MyEvent.BookingData(TmpRow!id).ProviderName = TmpBooking.ProviderName
                    MyEvent.BookingData(TmpRow!id).RejectionComment = TmpBooking.RejectionComment
                End If
                TmpList.Add(TmpBooking.DatabaseID, TmpBooking)
                If _bookingList.ContainsKey(TmpRow!id) Then
                    _bookingList(TmpRow!id) = TmpBooking
                Else
                    _bookingList.Add(TmpBooking.DatabaseID, TmpBooking)
                End If
                row += 1
            Next
            DBConn.Close()
            _bookingListLastUpdate = Now
        End Using
        End If
        Dim BECEventArgs As New IBookingEventConsumer.BookingsUpdatedEventArgs
        BECEventArgs.ListOfAllEntries = _bookingList
        BECEventArgs.ListOfUpdatedEntries = TmpList
        For Each TmpBEC As IBookingEventConsumer In _becList
            TmpBEC.BookingsUpdated(BECEventArgs)
        Next
        Return _bookingList.Values.ToList
    End Function


    'Add a date to be booked 
    Public Overrides Sub AddBookingDate(ByVal BookingID As Integer, ByVal [Date] As Date, ByVal Time As Integer)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "INSERT INTO bookingdates (bookingid,[date],[time]) VALUES (@bid,@date,@time)"
                com.Parameters.Add(New SqlClient.SqlParameter("@bid", BookingID))
                com.Parameters.Add(New SqlClient.SqlParameter("@date", [Date]))
                com.Parameters.Add(New SqlClient.SqlParameter("@time", Time))
                com.ExecuteScalar()
            End Using
            DBConn.Close()
        End Using
    End Sub

    'Add a prodyct to be displayed during a booking
    Public Overrides Sub AddBookingProduct(ByVal BookingID As Integer, ByVal Product As String)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "INSERT INTO bookingproducts (bookingid,product) VALUES (@bid,@prod)"
                com.Parameters.Add(New SqlClient.SqlParameter("@bid", BookingID))
                com.Parameters.Add(New SqlClient.SqlParameter("@prod", Product))
                com.ExecuteScalar()
            End Using
            DBConn.Close()
        End Using
    End Sub

    'Add a colleboration for a booking
    Public Overrides Sub AddCollaboration(ByVal BookingID As Integer, ByVal Company As String, ByVal Product As String, ByVal ShareOfInvoice As Integer, ByVal Reference As String, ByVal Address As String, ByVal PhoneNr As String, ByVal ZipCode As String)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "INSERT INTO bookingcollaborations (bookingid,company,product,shareofinvoice,reference,phonenr,address,zipcode) VALUES (@bid,@com,@prod,@soi,@ref,@phone,@addr,@zip)"
                com.Parameters.Add(New SqlClient.SqlParameter("@bid", BookingID))
                com.Parameters.Add(New SqlClient.SqlParameter("@prod", Product))
                com.Parameters.Add(New SqlClient.SqlParameter("@com", Company))
                com.Parameters.Add(New SqlClient.SqlParameter("@soi", ShareOfInvoice))
                com.Parameters.Add(New SqlClient.SqlParameter("@phone", PhoneNr))
                com.Parameters.Add(New SqlClient.SqlParameter("@addr", Address))
                com.Parameters.Add(New SqlClient.SqlParameter("@zip", ZipCode))
                com.Parameters.Add(New SqlClient.SqlParameter("@ref", Reference))
                com.ExecuteScalar()
            End Using
            DBConn.Close()
        End Using
    End Sub


    Public Overrides Function SaveBooking(ByVal CampaignID As Integer, ByVal StaffID As Integer, ByVal Store As String, ByVal Address As String, ByVal City As String, ByVal Phone As String, ByVal Contact As String, ByVal Placement As String, ByVal Comments As String, ByVal HasKitchen As Boolean, ByVal ProviderID As Integer, Optional ByVal ProviderText As String = "", Optional ByVal BookingID As Integer = 0) As Integer
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Using com As New SqlClient.SqlCommand("", DBConn)
                If BookingID = 0 Then
                    com.CommandText = "INSERT INTO bookings (eventid,staffid,store,address,city,phone,contact,placement,comments,providerid,otherprovider,storehaskitchen,confirmed,timesaved) VALUES (@eid,@staffid,@store,@addr,@city,@phone,@cnt,@plc,@com,@pid,@prov,@kitchen,1,@timesaved);SELECT @@identity"
                Else
                    com.CommandText = "INSERT INTO bookings (eventid,staffid,store,address,city,phone,contact,placement,comments,providerid,otherprovider,storehaskitchen,confirmed,timesaved) VALUES (@eid,@staffid,@store,@addr,@city,@phone,@cnt,@plc,@com,@pid,@prov,@kitchen,1,@timesaved);UPDATE bookings SET chosenprovider=(SELECT chosenprovider FROM bookings WHERE id=@bid) WHERE id=@@identity;UPDATE bookings SET chosenprovidername=(SELECT chosenprovidername FROM bookings WHERE id=@bid) WHERE id=@@identity; UPDATE bookingdates SET bookingid=@@identity WHERE bookingid=@bid;UPDATE bookingproducts SET bookingid=@@identity WHERE bookingid=@bid;UPDATE bookingcollaborations SET bookingid=@@identity WHERE bookingid=@bid;DELETE FROM bookings WHERE id=@bid;SELECT @@identity"
                End If
                com.Parameters.Add(New SqlClient.SqlParameter("@bid", BookingID))
                com.Parameters.Add(New SqlClient.SqlParameter("@eid", CampaignID))
                com.Parameters.Add(New SqlClient.SqlParameter("@store", Store))
                com.Parameters.Add(New SqlClient.SqlParameter("@addr", Address))
                com.Parameters.Add(New SqlClient.SqlParameter("@city", City))
                com.Parameters.Add(New SqlClient.SqlParameter("@phone", Phone))
                com.Parameters.Add(New SqlClient.SqlParameter("@cnt", Contact))
                com.Parameters.Add(New SqlClient.SqlParameter("@plc", Placement))
                com.Parameters.Add(New SqlClient.SqlParameter("@com", Comments))
                com.Parameters.Add(New SqlClient.SqlParameter("@pid", ProviderID))
                com.Parameters.Add(New SqlClient.SqlParameter("@prov", ProviderText))
                com.Parameters.Add(New SqlClient.SqlParameter("@kitchen", HasKitchen))
                com.Parameters.Add(New SqlClient.SqlParameter("@staffid", StaffID))
                com.Parameters.AddWithValue("@timesaved", Now)
                Return com.ExecuteScalar
            End Using
            _bookingList = Nothing
            _bookingListLastUpdate = Nothing
            DBConn.Close()
        End Using
    End Function


    Private Function GetAnsweredDaysBooking(ByVal BookingID As Integer, ByVal Connection As SqlClient.SqlConnection) As Integer
        Dim com As New SqlClient.SqlCommand("SELECT Count(QuestionaireAnswers.ID) FROM QuestionaireAnswers WHERE Answered=1 AND QuestionaireAnswers.BookingID=" & BookingID, Connection)
        Dim Count As Integer = com.ExecuteScalar
        Return Count
    End Function

    Public Overrides Sub SetBookingStatus(ByVal BookingID As Integer, ByVal Status As cBooking.BookingStatusEnum)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim com As New SqlClient.SqlCommand("UPDATE bookings SET confirmed=" & Status & ",timesaved=@timesaved WHERE id=" & BookingID, DBConn)
            com.Parameters.AddWithValue("@timesaved", Now)
            com.ExecuteReader()
            DBConn.Close()
        End Using
    End Sub

    Public Overrides Sub SetRejectionComment(ByVal BookingID As Integer, ByVal Comment As String)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim com As New SqlClient.SqlCommand("UPDATE bookings SET RejectionComment='" & Comment & "',timesaved=@timesaved WHERE id=" & BookingID, DBConn)
            com.Parameters.AddWithValue("@timesaved", Now)
            com.ExecuteReader()
            DBConn.Close()
        End Using
    End Sub

    Public Overrides Sub SetProvider(ByVal BookingID As Integer, ByVal Provider As cStaff, Optional ByVal ProviderName As String = "")
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim ProviderID As Integer
            If Provider Is Nothing Then
                ProviderID = 0
            Else
                ProviderID = Provider.DatabaseID
                ProviderName = Provider.Firstname
            End If
            Dim com As New SqlClient.SqlCommand("UPDATE bookings SET chosenprovider=" & ProviderID & ",chosenprovidername='" & ProviderName & "',timesaved=@timesaved WHERE id=" & BookingID, DBConn)
            com.Parameters.AddWithValue("@timesaved", Now)
            com.ExecuteReader()
            DBConn.Close()
        End Using
    End Sub

    Public Overrides Sub DeleteStaff(ByVal StaffID As Integer)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            'Implementera en koll så att man inte raderar en staff som är inblandad i något
            Dim com As New SqlClient.SqlCommand("SELECT id FROM bookings WHERE StaffID=" & StaffID & " OR providerid=" & StaffID & " OR chosenprovider=" & StaffID, DBConn)
            If com.ExecuteScalar Then
                Throw New Exception("Can not delete Staff or Provider assigned to a booking.")
            End If
            com = New SqlClient.SqlCommand("DELETE FROM staff WHERE id=" & StaffID, DBConn)
            com.ExecuteNonQuery()
            com.CommandText = "DELETE FROM staffcategoriesavailablestaff WHERE staffid=" & StaffID
            com.ExecuteNonQuery()
            com.CommandText = "DELETE FROM roleavailableforstaff WHERE staffid=" & StaffID
            com.ExecuteNonQuery()
            com.CommandText = "DELETE FROM questionairestaff WHERE staffid=" & StaffID
            com.ExecuteNonQuery()
            com.CommandText = "DELETE FROM eventstaff WHERE staffid=" & StaffID
            com.ExecuteNonQuery()
            com.CommandText = "DELETE FROM eventshiftstaff WHERE staffid=" & StaffID
            com.ExecuteNonQuery()
            com.CommandText = "DELETE FROM eventshiftavailablestaff WHERE staffid=" & StaffID
            com.ExecuteNonQuery()
            DBConn.Close()
        End Using
    End Sub

    Private _becList As New List(Of IBookingEventConsumer)
    Public Overrides Sub RegisterBookingEventConsumer(ByVal bec As IBookingEventConsumer)
        If Not _becList.Contains(bec) Then
            _becList.Add(bec)
        End If
    End Sub

    Public Overrides Sub UnRegisterBookingEventConsumer(ByVal bec As IBookingEventConsumer)
        If _becList.Contains(bec) Then
            _becList.Remove(bec)
        End If
    End Sub

    Public Overrides Sub DeleteBooking(ByVal BookingID As Integer)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim com As New SqlClient.SqlCommand("DELETE FROM bookings WHERE id=" & BookingID, DBConn)
            com.ExecuteNonQuery()
            com.CommandText = "DELETE FROM bookingdates WHERE bookingid=" & BookingID
            com.ExecuteNonQuery()
            com.CommandText = "DELETE FROM bookingcollaborations WHERE bookingid=" & BookingID
            com.ExecuteNonQuery()
            _bookingList.Remove(BookingID)
        End Using
    End Sub

    Public Overrides Function GetStores() As System.Collections.Generic.List(Of cStore)
        Dim TmpList As New List(Of cStore)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Using com As New SqlClient.SqlCommand("SELECT * FROM stores", DBConn)
                Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                    While rd.Read
                        Dim TmpStore As New cStore
                        TmpStore.Name = rd!Name
                        TmpStore.DatabaseID = rd!id
                        TmpStore.Address = rd!address
                        TmpStore.City = rd!city
                        TmpStore.PhoneNo = rd!phoneno
                        TmpList.Add(TmpStore)
                    End While
                    rd.Close()
                    DBConn.Close()
                End Using
            End Using
        End Using
        Return TmpList
    End Function

    Public Overrides Sub DeleteStore(ByVal StoreID As Integer)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Using com As New SqlClient.SqlCommand("DELETE FROM stores WHERE id=" & StoreID, DBConn)
                com.ExecuteNonQuery()
            End Using
            DBConn.Close()
        End Using
    End Sub

    Public Overrides Sub SaveStore(ByVal Store As cStore)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Dim SQL As String
            If Store.DatabaseID = 0 Then
                SQL = "INSERT INTO stores (name,address,city,phoneno) VALUES (@name,@addr,@city,@phone)"
            Else
                SQL = "UPDATE stores SET name=@name,address=@addr,city=@city,phoneno=@phone WHERE id=@id"
            End If
            Using com As New SqlClient.SqlCommand(SQL, DBConn)
                com.Parameters.AddWithValue("@id", Store.DatabaseID)
                com.Parameters.AddWithValue("@name", Store.Name)
                com.Parameters.AddWithValue("@addr", Store.Address)
                com.Parameters.AddWithValue("@city", Store.City)
                com.Parameters.AddWithValue("@phone", Store.PhoneNo)
                com.ExecuteNonQuery()
            End Using
            DBConn.Close()
        End Using
    End Sub

    Public Overrides Function GetQuestionaireTemplate(ByVal ID As Integer) As cQuestionaire
        Dim _questionaire As cQuestionaire = Nothing
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Using _com As New SqlClient.SqlCommand("SELECT * FROM questionairetemplate WHERE id=" & ID, DBConn)
                Dim _rd As SqlClient.SqlDataReader
                _rd = _com.ExecuteReader
                If _rd.Read Then
                    _questionaire = New cQuestionaire
                    _questionaire.CreateFromXML(_rd!xml)
                End If
                _rd.Close()
            End Using
            DBConn.Close()
        End Using
        Return _questionaire
    End Function

    Public Overrides Function GetQuestionaireTemplates() As System.Collections.Generic.List(Of cQuestionaire)
        Dim _list As New List(Of cQuestionaire)
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Using _com As New SqlClient.SqlCommand("SELECT * FROM questionairetemplate", DBConn)
                Dim _rd As SqlClient.SqlDataReader
                _rd = _com.ExecuteReader
                While _rd.Read
                    Dim _questionaire As New cQuestionaire
                    Dim _xmlDoc As New Xml.XmlDocument
                    _xmlDoc.LoadXml(_rd!xml)
                    _questionaire.CreateFromXML(_xmlDoc.FirstChild)
                    _list.Add(_questionaire)
                End While
                _rd.Close()
            End Using
            DBConn.Close()
        End Using
        Return _list
    End Function

    Public Overrides Function SaveQuestionaireTemplate(ByVal Name As String, ByVal Questionaire As cQuestionaire) As Integer
        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Using _com As New SqlClient.SqlCommand("INSERT INTO questionairetemplate ([name],[xml]) VALUES (@name,@xml)", DBConn)
                Dim xmlDoc As New Xml.XmlDocument()
                _com.Parameters.AddWithValue("@name", Name)
                _com.Parameters.AddWithValue("@xml", Questionaire.CreateXML(xmlDoc).OuterXml)
                _com.ExecuteNonQuery()
            End Using
            DBConn.Close()
        End Using
    End Function

    Public Overrides Function CampaignSummary(ByVal Questionnaire As Integer) As Collection

        Dim _list As New Collection

        Dim masterlist As String
        Dim xmld As New Xml.XmlDocument

        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)
            Using _com As New SqlClient.SqlCommand("SELECT * FROM QuestionaireAnswers WHERE Answered = 1 AND QuestionaireID = " & Questionnaire, DBConn)
                Dim _rd As SqlClient.SqlDataReader
                _rd = _com.ExecuteReader
                While _rd.Read
                    Dim _questionaire As New cQuestionaire
                    Dim _xmlDoc As New Xml.XmlDocument
                    _xmlDoc.LoadXml(_rd!xml)
                    _questionaire.CreateFromXML(_xmlDoc.FirstChild)
                    _list.Add(_questionaire)
                End While
                _rd.Close()
            End Using
            DBConn.Close()
        End Using
        Return _list
    End Function

    Public Overrides Function GetAllQuestionnaires() As Collection

        Dim _list As New Collection

        Using DBConn As SqlClient.SqlConnection = Connect(ConnectionMode.cmNetwork)

            Using _com As New SqlClient.SqlCommand("SELECT * FROM Questionaire", DBConn)
                Dim _rd As SqlClient.SqlDataReader
                _rd = _com.ExecuteReader
                While _rd.Read
                    _list.Add(_rd!Name, _rd!id)
                End While
                _rd.Close()
            End Using
            DBConn.Close()
        End Using
        Return _list
    End Function

End Class
