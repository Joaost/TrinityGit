Imports System.Text
Imports System.Data
Imports System.Xml

Namespace Trinity
    Public Class cDBReaderSQL
        Inherits Trinity.cDBReader

        'Public DBConn As New System.Data.SqlClient.SqlConnection
        'Public DBConnCommon As New System.Data.SqlClient.SqlConnection

        Private connectionString As String
        Private _commonConnectionString As String
        Private _commonSpecificsConnectionString As String

        Public Delegate Sub MessageReceivedEventHandler(sender As Object, e As MessageEventArgs)
        Public Event IncomingMessage As MessageReceivedEventHandler

        Public Property _connectionString As String
            Get
                Return connectionString
            End Get
            Set(ByVal value As String)
                connectionString = value
            End Set
        End Property
        Delegate Sub GotProgsInOtherChannels(ByVal dt As DataTable)

        Public Structure CampaignDBLock
            Public IsLocked As Boolean
            Public UserID As Integer
            Public LockDate As Date
        End Structure

        Public Overrides Function GetCampaignsUserAccess(Optional ByVal SQLQuery As String = "") As List(Of CampaignEssentials)
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using Command As New SqlClient.SqlCommand
                    Command.Connection = _conn
                    Using ds As New DataTable
                        Dim Clients As New List(Of Integer)
                        Dim Product As New List(Of Integer)
                        Dim Years As New List(Of Integer)
                        Dim Months As New List(Of Integer)
                        Dim Campaigns As New List(Of String)
                        Dim CampaignList As New List(Of CampaignEssentials)

                        If SQLQuery = "" Then
                            Command.CommandText = "IF IS_SRVROLEMEMBER('mc_access') = 1 PRINT 'DR KOCH'"
                        Else
                            Command.CommandText = SQLQuery
                        End If

                        Try
                            Using rd As SqlClient.SqlDataReader = Command.ExecuteReader
                                ds.Load(rd)
                                rd.Close()
                            End Using
                        Catch ex As SqlClient.SqlException
                            Throw ex
                            'Windows.Forms.MessageBox.Show("Could not open the campaigns. Error message: " & vbNewLine & ex.Message)
                            '_conn.Close()
                            'Return New List(Of clTrinity.CampaignEssentials)
                        End Try

                        Return CampaignList
                    End Using
                    _conn.Close()
                End Using
            End Using
        End Function
        Public Overrides Function RecoverCampaignFromBackup(originalID As Integer) As Boolean
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()

                Using Command As SqlClient.SqlCommand = New SqlClient.SqlCommand

                    Command.Connection = _conn

                    Command.CommandText = "INSERT INTO campaigns (campaignid, name, startdate, enddate, client, product, maintarget, secondtarget, thirdtarget, planner, buyer, XML, lastopened, lastsaved,originallocation, originalfilechangeddate, lockedby, status, seraphxml, deletedon, contractid) SELECT campaignid, name, startdate, enddate, client, product, maintarget, secondtarget, thirdtarget, planner, buyer, XML, lastopened, lastsaved,originallocation, originalfilechangeddate, lockedby, status, seraphxml, deletedon, contractid FROM [backup] WHERE id =" & originalID
                    Try
                        Command.ExecuteNonQuery()
                        _conn.Close()
                        Return True
                    Catch ex As Exception
                        Debug.Print(ex.Message)
                        _conn.Close()
                        Return False
                    End Try
                End Using
            End Using
        End Function

        Public Overrides Function getContractSaveTime(ByVal DatabaseID As Long) As Date
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using Command As SqlClient.SqlCommand = New SqlClient.SqlCommand
                    Command.Connection = _conn
                    Command.CommandText = "SELECT lastsavedon FROM contracts WHERE id=" & DatabaseID
                    Try
                        Return CDate(Command.ExecuteScalar)
                    Catch ex As SqlClient.SqlException
                        Debug.Print(ex.Message)
                        Return DateSerial(1900, 1, 1)
                    End Try
                End Using
                _conn.Close()
            End Using
        End Function

        Public Overrides Function getContracts() As System.Data.DataTable
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using Command As SqlClient.SqlCommand = New SqlClient.SqlCommand
                    Command.Connection = _conn
                    Command.CommandText = "SELECT id, name,startdate,enddate,version,originallocation,deletedon,lastsavedon FROM contracts WHERE deletedon IS NULL ORDER BY name"
                    Try
                        Using rd As SqlClient.SqlDataReader = Command.ExecuteReader
                            Try
                                Using dt As New DataTable
                                    dt.Load(rd)
                                    rd.Close()
                                    _conn.Close()
                                    Return dt
                                End Using
                            Catch ex As Exception
                                Debug.Print(ex.Message)
                                rd.Close()
                            End Try
                        End Using
                    Catch ex As SqlClient.SqlException
                        Debug.Print(ex.Message)
                    End Try
                End Using
                _conn.Close()
                Return New System.Data.DataTable
            End Using
        End Function

        Public Overrides Function getContractName(id As Integer) As String
            Dim _name As String
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using _com As New SqlClient.SqlCommand("SELECT name FROM contracts WHERE id=@id", _conn)
                    _com.Parameters.AddWithValue("@id", id)
                    _name = _com.ExecuteScalar
                End Using
                _conn.Close()
            End Using
            Return _name
        End Function

        Public Overrides Function getContract(ByVal ContractID As Long) As System.Xml.XmlElement
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using Command As SqlClient.SqlCommand = New SqlClient.SqlCommand
                    Command.Connection = _conn
                    Dim XMLDoc As New Xml.XmlDocument


                    Command.CommandText = "SELECT xml FROM contracts WHERE id=" & ContractID

                    Try
                        XMLDoc.LoadXml(Command.ExecuteScalar)
                        Return XMLDoc.DocumentElement
                    Catch ex As SqlClient.SqlException
                        Debug.Print(ex.Message)
                        Return (New Xml.XmlDocument).DocumentElement
                    End Try
                End Using
                _conn.Close()
            End Using
        End Function

        Public Overrides Function saveContract(Contract As Trinity.cContract, ByVal XML As System.Xml.XmlDocument) As Boolean

            If Contract.MainObject.ContractID > 0 Then
                Dim Version As Long
                Using _conn As New SqlClient.SqlConnection(_connectionString)
                    _conn.Open()
                    Using Command As SqlClient.SqlCommand = New SqlClient.SqlCommand
                        Command.Connection = _conn
                        Command.CommandText = "SELECT version FROM contracts WHERE id = " & Contract.MainObject.ContractID
                        Version = Command.ExecuteScalar + 1
                    End Using
                    Using Command As New SqlClient.SqlCommand
                        Command.Connection = _conn
                        Command.CommandText = "UPDATE contracts SET name='" & Contract.Name & "',startdate='" & Contract.FromDate & "',enddate='" & _
                                                Contract.ToDate & "',lastsavedon='" & Contract.SaveDateTime & "',lastsavedby=" & TrinitySettings.UserID & ",version=" & Version & _
                                                ",xml=@xml WHERE id = " & Contract.MainObject.ContractID
                        Command.Parameters.Add("@xml", SqlDbType.Xml)
                        Command.Parameters("@xml").Value = XML.OuterXml.ToString
                        Try
                            Command.ExecuteScalar()
                        Catch ex As SqlClient.SqlException
                            Debug.Print("Error saving the contract: " & ex.Message)
                            Return False
                        End Try
                    End Using
                    _conn.Close()
                End Using
            Else
                Dim Rows As Long
                Using _conn As New SqlClient.SqlConnection(_connectionString)
                    _conn.Open()
                    Using Command As New SqlClient.SqlCommand("SELECT COUNT(id) FROM contracts WHERE name='" & Contract.Name & "'", _conn)
                        Rows = Command.ExecuteScalar
                    End Using
                    If Rows = 0 Then
                        Using Command As New SqlClient.SqlCommand
                            Command.Connection = _conn
                            Command.CommandText = "INSERT INTO contracts (name,startdate,enddate,lastsavedon,lastsavedby,version,originallocation,xml) VALUES('" & Contract.Name & "','" & Contract.FromDate & "','" & _
                                                    Contract.ToDate & "','" & Contract.SaveDateTime & "'," & TrinitySettings.UserID & ",1,'" & Contract.Path & "',@xml);SELECT @@IDENTITY"
                            Command.Parameters.Add("@xml", SqlDbType.Xml)
                            Command.Parameters("@xml").Value = XML.OuterXml.ToString

                            Try
                                Contract.MainObject.ContractID = Command.ExecuteScalar
                                Return True
                            Catch ex As SqlClient.SqlException
                                Debug.Print("Error saving the contract: " & ex.Message)
                                Return False
                            End Try
                        End Using
                    Else
                        Windows.Forms.MessageBox.Show("There is already a contract with the name " & Contract.Name & "." & vbNewLine & _
                                                      "Please enter a different name and save again.", "Change the name", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Exclamation)
                        Return False
                    End If
                    _conn.Close()
                End Using
            End If


        End Function

        Public Overrides Function updateCampaignStatus(CampaignID As Long, tmpStatus As String) As Boolean
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using Command As SqlClient.SqlCommand = New SqlClient.SqlCommand
                    Command.Connection = _conn

                    Command.CommandText = "UPDATE campaigns SET status='" & tmpStatus & "' WHERE id=" & CampaignID

                    Try
                        Command.ExecuteNonQuery()
                        Return True
                    Catch ex As SqlClient.SqlException
                        Debug.Print(ex.Message)
                        Return False
                    End Try
                End Using
                _conn.Close()
            End Using
        End Function
        Public Overrides Function updateCampaignBuyer(CampaignID As Long, tmpBuyerName As String) As Boolean
            Dim tmpBuyerID = getUserID(tmpBuyerName)
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using Command As SqlClient.SqlCommand = New SqlClient.SqlCommand
                    Command.Connection = _conn

                    Command.CommandText = "UPDATE campaigns SET buyer='" & tmpBuyerID & "' WHERE id=" & CampaignID

                    Try
                        Command.ExecuteNonQuery()
                        Return True
                    Catch ex As SqlClient.SqlException
                        Debug.Print(ex.Message)
                        Return False
                    End Try
                End Using
                _conn.Close()
            End Using
        End Function
        Public Overrides Function updateCampaignPlanner(CampaignID As Long, tmpPlanner As String) As Boolean
            Dim tmpPlannerID = getUserID(tmpPlanner)
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using Command As SqlClient.SqlCommand = New SqlClient.SqlCommand
                    Command.Connection = _conn
                    Command.CommandText = "UPDATE campaigns SET planner='" & tmpPlannerID & "' WHERE id=" & CampaignID
                    Try
                        Command.ExecuteNonQuery()
                        Return True
                    Catch ex As SqlClient.SqlException
                        Debug.Print(ex.Message)
                        Return False
                    End Try
                End Using
                _conn.Close()
            End Using
        End Function


        Public Overrides Function setAsDeleted(ByVal CampaignID As Long) As Boolean
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using Command As SqlClient.SqlCommand = New SqlClient.SqlCommand
                    Command.Connection = _conn

                    Command.CommandText = "UPDATE campaigns SET deletedon=GETDATE()" & " WHERE id=" & CampaignID

                    Try
                        Command.ExecuteNonQuery()
                        Return True
                    Catch ex As SqlClient.SqlException
                        Debug.Print(ex.Message)
                        Return False
                    End Try
                End Using
                _conn.Close()
            End Using
        End Function
        Public Overrides Function setContractAsDeleted(ByVal ContractID As Long) As Boolean
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using Command As SqlClient.SqlCommand = New SqlClient.SqlCommand
                    Command.Connection = _conn

                    Command.CommandText = "UPDATE contracts SET deletedon=GETDATE()" & " WHERE id=" & ContractID

                    Try
                        Command.ExecuteNonQuery()
                        Return True
                    Catch ex As SqlClient.SqlException
                        Debug.Print(ex.Message)
                        Return False
                    End Try
                End Using
                _conn.Close()
            End Using
        End Function

        Public Overrides Function campaignNameExists(ByVal name As String) As Boolean
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using Command As SqlClient.SqlCommand = New SqlClient.SqlCommand
                    Command.Connection = _conn
                    Command.CommandText = "SELECT id FROM campaigns WHERE name='" & name & "'"

                    Try
                        Using rd As SqlClient.SqlDataReader = Command.ExecuteReader
                            If rd.HasRows Then
                                rd.Close()
                                _conn.Close()
                                Return True
                            Else
                                _conn.Close()
                                rd.Close()
                                Return False
                            End If
                        End Using
                    Catch ex As SqlClient.SqlException
                        Debug.Print(ex.Message)
                    End Try
                End Using
                _conn.Close()
                Return Nothing
            End Using
        End Function

        Public Function GetCampaignLock(ByVal id As Long) As CampaignDBLock
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Dim _dbLock As New CampaignDBLock With {.IsLocked = False}
                Using Command As New SqlClient.SqlCommand("SELECT lockedby,lastopened FROM campaigns WHERE id=" & id, _conn)
                    Using _rd As SqlClient.SqlDataReader = Command.ExecuteReader
                        If _rd.Read Then
                            _dbLock = New CampaignDBLock With {.LockDate = _rd!lastopened, .UserID = _rd!lockedby, .IsLocked = (Not IsDBNull(_rd!lockedby) AndAlso Not _rd!lockedby.ToString = "" AndAlso Not _rd!lockedby = 0)}
                        End If
                        _rd.Close()
                    End Using
                End Using
                _conn.Close()
                Return _dbLock
            End Using
        End Function

        Sub MonitorLock(ByVal id As Long)
            StopMonitoringLock()
            SqlClient.SqlDependency.Start(_connectionString)
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using _dataSet As New DataSet
                    _dataSet.Clear()
                    ' Make sure the command object does not already have a notification object associated with it.
                    Using _command = New SqlClient.SqlCommand("SELECT lockedBy FROM dbo.campaigns WHERE id=" & id, _conn)
                        _command.Notification = Nothing
                        ' Create and bind the SqlDependency object to the command object.
                        Dim _dependency As New SqlClient.SqlDependency(_command)
                        AddHandler _dependency.OnChange, AddressOf LockChanged

                        Using _adapter As New SqlClient.SqlDataAdapter(_command)
                            _adapter.Fill(_dataSet, "campaigns")
                        End Using
                    End Using
                End Using
                _conn.Close()
            End Using
        End Sub

        Private Sub LockChanged()
            'TODO: This should not point at Campaign
            Campaign._lockChanged()
        End Sub

        Public Overrides Function lockCampaign(ByVal id As Long) As Boolean
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using Command As SqlClient.SqlCommand = New SqlClient.SqlCommand
                    Command.Connection = _conn
                    Command.CommandText = "UPDATE campaigns SET lockedby=" & TrinitySettings.UserID & ",lastopened=GETDATE() WHERE id=" & id
                    Try
                        Command.ExecuteNonQuery()
                        MonitorLock(id)
                        _conn.Close()
                        Return True
                    Catch ex As SqlClient.SqlException
                        Debug.Print(ex.Message)
                        _conn.Close()
                        Return False
                    End Try
                End Using
                _conn.Close()
            End Using
        End Function

        Sub StopMonitoringLock()
            SqlClient.SqlDependency.Stop(_connectionString)
        End Sub

        Public Overrides Function unlockCampaign(ByVal id As Long) As Boolean
            StopMonitoringLock()
            Try
                Using _conn As New SqlClient.SqlConnection(_connectionString)
                    _conn.Open()
                    Using Command As SqlClient.SqlCommand = New SqlClient.SqlCommand
                        Command.Connection = _conn
                        Command.CommandText = "UPDATE campaigns SET lockedby=0 WHERE id=" & id
                        Try
                            Command.ExecuteNonQuery()
                            _conn.Close()
                            Return True
                        Catch ex As SqlClient.SqlException
                            Debug.Print(ex.Message)
                            _conn.Close()
                            Return False
                        End Try
                    End Using
                    _conn.Close()
                End Using
            Catch

            End Try
        End Function

        Public Overrides Function getDatabaseIDFromName(ByVal name As String) As Integer
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using Command As SqlClient.SqlCommand = New SqlClient.SqlCommand
                    Command.Connection = _conn

                    Try
                        Command.CommandText = "SELECT id from people WHERE name = '" & name & "'"
                        Dim _id As Integer = Command.ExecuteScalar
                        _conn.Close()
                        Return _id
                    Catch ex As SqlClient.SqlException
                        Debug.Print(ex.Message)
                        _conn.Close()
                        Return -1
                    End Try
                End Using
                _conn.Close()
            End Using
        End Function

        Public Overrides Function transferPeopleToDB() As Boolean
            Dim PeopleList As New List(Of cPerson)


            Dim PeopleXML As New Xml.XmlDocument
            PeopleXML.Load(Trinity.Helper.Pathify(TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork)) & "people.xml")

            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                For Each Person As Xml.XmlNode In PeopleXML.SelectNodes("data/planners/planner")

                    Dim name As String = "Not set"
                    Dim phone As String = "Not set"
                    Dim email As String = "Not set"

                    If Not Person.Attributes("name") Is Nothing Then name = Person.Attributes("name").Value
                    If Not Person.Attributes("phone") Is Nothing Then phone = Person.Attributes("phone").Value
                    If Not Person.Attributes("email") Is Nothing Then email = Person.Attributes("email").Value


                    Using Command As New SqlClient.SqlCommand
                        Command.Connection = _conn
                        'DBConn.Open()

                        Command.CommandText = "SELECT * from people WHERE name='" & name & "'"
                        Using rd As SqlClient.SqlDataReader = Command.ExecuteReader
                            Try
                                If Not rd.HasRows Then
                                    rd.Close()
                                    Command.Connection = _conn
                                    Command.CommandText = "INSERT into people values('" & name & "','" & phone & "','" & email & "')"
                                    Try
                                        Command.ExecuteNonQuery()
                                    Catch ex As SqlClient.SqlException
                                        Debug.Print(ex.Message)
                                    End Try
                                Else
                                    rd.Close()
                                End If
                            Catch ex As SqlClient.SqlException
                                Debug.Print(ex.Message)
                                _conn.Close()

                                Return False
                            End Try
                        End Using
                    End Using
                Next
                _conn.Close()
            End Using
            Return True
        End Function

        Public Overrides Function getAllPeople() As System.Collections.Generic.List(Of cPerson)
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using Command As New SqlClient.SqlCommand
                    Dim PeopleList As New List(Of cPerson)
                    Command.Connection = _conn
                    Command.CommandText = "SELECT * from people ORDER BY name"
                    Using ds As New DataTable
                        Try
                            Using rd As SqlClient.SqlDataReader = Command.ExecuteReader
                                If rd.HasRows Then
                                    ds.Load(rd)
                                    For Each row As Object In ds.Rows
                                        Dim tmpPerson As New cPerson
                                        tmpPerson.id = row!id
                                        tmpPerson.Name = row!name
                                        If Not IsDBNull(row!active) Or IsNothing(row!active) Then tmpPerson.statusActive = row!active Else tmpPerson.statusActive = True
                                        If Not IsDBNull(row!email) Then tmpPerson.Email = row!email
                                        If Not IsDBNull(row!phone) Then tmpPerson.Phone = row!phone
                                        PeopleList.Add(tmpPerson)
                                    Next

                                End If
                                rd.Close()
                                _conn.Close()
                                Return PeopleList
                            End Using
                        Catch ex As Exception
                            Debug.Print("Error executing SQL SELECT * FROM people, " & ex.Message)
                            _conn.Close()
                            Return New List(Of cPerson)
                        End Try
                    End Using
                End Using
                _conn.Close()
            End Using
        End Function

        Public Overrides Function GetCampaign(ByVal ID As Long, Optional ByVal OpenReadOnly As Boolean = False) As String
            If Not OpenReadOnly Then
                Dim _dbLock As CampaignDBLock = GetCampaignLock(ID)
                If _dbLock.IsLocked AndAlso _dbLock.UserID <> TrinitySettings.UserID Then
                    Dim _username = getUser(_dbLock.UserID)!name
                    Throw New ReadOnlyException("Campaign was locked by '" & _username & "' at " & _dbLock.LockDate)
                End If
            End If
            If ID < 1 Then
                Windows.Forms.MessageBox.Show("Trinity could not find an ID for this campaign. Contact the system administrator.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                Return Nothing
            Else
                Using _conn As New SqlClient.SqlConnection(_connectionString)
                    _conn.Open()
                    Using Command As New SqlClient.SqlCommand
                        Command.Connection = _conn
                        Command.CommandText = "SELECT xml from campaigns WHERE id = '" & ID & "'"
                        Using ds As New DataTable
                            Try
                                Using rd As SqlClient.SqlDataReader = Command.ExecuteReader
                                    ds.Load(rd)
                                    If ds.Rows.Count = 0 Then
                                        _conn.Close()
                                        Return ""
                                    End If

                                    Dim tmpXML As New XmlDocument
                                    tmpXML.LoadXml(ds.Rows(0).Item(0))
                                    If tmpXML.SelectSingleNode("Campaign").Attributes("DatabaseID") Is Nothing Then
                                        DirectCast(tmpXML.SelectSingleNode("Campaign"), XmlElement).SetAttribute("DatabaseID", ID)
                                    Else
                                    End If
                                    If Not OpenReadOnly Then
                                        DBReader.lockCampaign(ID)
                                    End If
                                    _conn.Close()
                                    Return tmpXML.OuterXml.ToString
                                End Using
                            Catch ex As SqlClient.SqlException
                                _conn.Close()
                                Return ""
                            End Try
                            _conn.Close()
                        End Using
                    End Using
                End Using
            End If
        End Function

        Public Overrides Function GetCampaigns(Optional ByVal SQLQuery As String = "") As List(Of CampaignEssentials)
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using Command As New SqlClient.SqlCommand
                    Command.Connection = _conn
                    Using ds As New DataTable
                        Dim Clients As New List(Of Integer)
                        Dim Product As New List(Of Integer)
                        Dim Years As New List(Of Integer)
                        Dim Months As New List(Of Integer)
                        Dim Campaigns As New List(Of String)
                        Dim CampaignList As New List(Of CampaignEssentials)

                        If SQLQuery = "" Then
                            Command.CommandText = "SELECT id,name,startdate,enddate,client,status,product,plannername,buyer,contractid,lastopened,lastsaved,originallocation,originalfilechangeddate,campaignid from campaigns WHERE deletedon < '2001-01-01'"
                        Else
                            Command.CommandText = SQLQuery
                        End If

                        Try
                            Using rd As SqlClient.SqlDataReader = Command.ExecuteReader
                                ds.Load(rd)
                                rd.Close()
                            End Using
                        Catch ex As SqlClient.SqlException
                            Throw ex
                            'Windows.Forms.MessageBox.Show("Could not open the campaigns. Error message: " & vbNewLine & ex.Message)
                            '_conn.Close()
                            'Return New List(Of clTrinity.CampaignEssentials)
                        End Try

                        For Each row As DataRow In ds.Rows
                            Try
                                Dim tmpCmp As New CampaignEssentials
                                If ds.Columns.Contains("id") Then tmpCmp.id = row!id
                                If ds.Columns.Contains("campaignid") Then tmpCmp.campaignid = row!campaignid
                                If ds.Columns.Contains("name") Then tmpCmp.name = row!name
                                If ds.Columns.Contains("startdate") Then tmpCmp.startdate = row!startdate
                                If ds.Columns.Contains("enddate") Then tmpCmp.enddate = row!enddate
                                If ds.Columns.Contains("client") Then tmpCmp.client = row!client
                                If ds.Columns.Contains("product") Then tmpCmp.product = row!product
                                If ds.Columns.Contains("planner") Then
                                    If row!planner.ToString() = "" Then
                                        tmpCmp.planner = ""
                                    Else
                                        tmpCmp.planner = row!planner
                                    End If
                                End If
                                If ds.Columns.Contains("buyer") Then
                                    If row!buyer.ToString() = "" Then
                                        tmpCmp.buyer = ""
                                    Else
                                        tmpCmp.buyer = row!buyer
                                    End If
                                End If
                                If ds.Columns.Contains("lastopened") Then tmpCmp.lastopened = row!lastopened
                                If ds.Columns.Contains("lastsaved") Then tmpCmp.lastsaved = row!lastsaved
                                If ds.Columns.Contains("status") Then tmpCmp.status = row!status
                                If ds.Columns.Contains("lockedby") Then
                                    If row!lockedby.ToString() = "" Then
                                        tmpCmp.lockedby = ""
                                    Else
                                        tmpCmp.lockedby = row!lockedby
                                    End If
                                End If
                                If ds.Columns.Contains("contractid") AndAlso Not IsDBNull(row!contractid) Then tmpCmp.contractid = row!contractid
                                If ds.Columns.Contains("originallocation") AndAlso Not IsDBNull(row!originallocation) Then tmpCmp.originalLocation = row!originallocation
                                If ds.Columns.Contains("originalfilechangeddate") AndAlso Not IsDBNull(row!originalfilechangeddate) Then tmpCmp.originalfilechangeddate = row!originalfilechangeddate
                                CampaignList.Add(tmpCmp)
                            Catch ex As Exception
                                _conn.Close()
                                Windows.Forms.MessageBox.Show(ex.Message)
                                Return New List(Of CampaignEssentials)
                            End Try
                        Next

                        Return CampaignList
                    End Using
                    _conn.Close()
                End Using
            End Using
        End Function
        Public Overrides Function GetCampaignsXML(Optional ByVal SQLQuery As String = "") As List(Of CampaignEssentials)
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using Command As New SqlClient.SqlCommand
                    Command.Connection = _conn
                    Using ds As New DataTable
                        Dim CampaignList As New List(Of CampaignEssentials)

                        If SQLQuery = "" Then
                            'Command.CommandText = "SELECT id,name,startdate,enddate,client,status,product,plannername,buyer,contractid,lastopened,lastsaved,originallocation,originalfilechangeddate,campaignid from campaigns WHERE deletedon < '2001-01-01'"
                            Command.CommandText = "SELECT XML from campaigns WHERE deletedon < '2001-01-01'"
                        Else
                            Command.CommandText = SQLQuery
                        End If

                        Try
                            Using rd As SqlClient.SqlDataReader = Command.ExecuteReader
                                ds.Load(rd)
                                rd.Close()
                            End Using
                        Catch ex As SqlClient.SqlException
                            Throw ex
                            Windows.Forms.MessageBox.Show("Could not open the campaigns. Error message: " & vbNewLine & ex.Message)
                        End Try
                        
                        Dim count As Integer = 0

                        For each row As DataRow In ds.Rows
                            count += count
                        Next

                        Return CampaignList
                    End Using
                    _conn.Close()
                End Using
            End Using
        End Function
        Public Overrides Function GetBackUpCampaigns(Optional SQLQuery As String = "") As System.Collections.Generic.List(Of CampaignEssentials)
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using Command As New SqlClient.SqlCommand
                    Command.Connection = _conn
                    Using ds As New DataTable
                        Dim Clients As New List(Of Integer)
                        Dim Product As New List(Of Integer)
                        Dim Years As New List(Of Integer)
                        Dim Months As New List(Of Integer)
                        Dim Campaigns As New List(Of String)
                        Dim CampaignList As New List(Of CampaignEssentials)

                        If SQLQuery = "" Then
                            Command.CommandText = "SELECT id,name,startdate,enddate,client,status,product,plannername,buyer,contractid,lastopened,lastsaved,originallocation,originalfilechangeddate,campaignid from backup WHERE deletedon < '2001-01-01'"
                        Else
                            Command.CommandText = SQLQuery
                        End If

                        Try
                            Using rd As SqlClient.SqlDataReader = Command.ExecuteReader
                                ds.Load(rd)
                                rd.Close()
                            End Using
                        Catch ex As SqlClient.SqlException
                            Throw ex
                            'Windows.Forms.MessageBox.Show("Could not open the campaigns. Error message: " & vbNewLine & ex.Message)
                            '_conn.Close()
                            'Return New List(Of clTrinity.CampaignEssentials)
                        End Try

                        For Each row As DataRow In ds.Rows
                            Try
                                Dim tmpCmp As New CampaignEssentials
                                If ds.Columns.Contains("id") Then tmpCmp.id = row!id
                                If ds.Columns.Contains("campaignid") Then tmpCmp.campaignid = row!campaignid
                                If ds.Columns.Contains("name") Then tmpCmp.name = row!name
                                If ds.Columns.Contains("startdate") Then tmpCmp.startdate = row!startdate
                                If ds.Columns.Contains("enddate") Then tmpCmp.enddate = row!enddate
                                If ds.Columns.Contains("client") Then tmpCmp.client = row!client
                                If ds.Columns.Contains("product") Then tmpCmp.product = row!product
                                If ds.Columns.Contains("planner") Then
                                    If row!planner.ToString() = "" Then
                                        tmpCmp.planner = ""
                                    Else
                                        tmpCmp.planner = row!planner
                                    End If
                                End If
                                If ds.Columns.Contains("buyer") Then
                                    If row!buyer.ToString() = "" Then
                                        tmpCmp.buyer = ""
                                    Else
                                        tmpCmp.buyer = row!buyer
                                    End If
                                End If
                                If ds.Columns.Contains("lastopened") Then tmpCmp.lastopened = row!lastopened
                                If ds.Columns.Contains("lastsaved") Then tmpCmp.lastsaved = row!lastsaved
                                If ds.Columns.Contains("status") Then tmpCmp.status = row!status
                                If ds.Columns.Contains("lockedby") Then
                                    If row!lockedby.ToString() = "" Then
                                        tmpCmp.lockedby = ""
                                    Else
                                        tmpCmp.lockedby = row!lockedby
                                    End If
                                End If
                                If ds.Columns.Contains("contractid") AndAlso Not IsDBNull(row!contractid) Then tmpCmp.contractid = row!contractid
                                If ds.Columns.Contains("originallocation") AndAlso Not IsDBNull(row!originallocation) Then tmpCmp.originalLocation = row!originallocation
                                If ds.Columns.Contains("originalfilechangeddate") AndAlso Not IsDBNull(row!originalfilechangeddate) Then tmpCmp.originalfilechangeddate = row!originalfilechangeddate
                                CampaignList.Add(tmpCmp)
                            Catch ex As Exception
                                _conn.Close()
                                Windows.Forms.MessageBox.Show(ex.Message)
                                Return New List(Of CampaignEssentials)
                            End Try
                        Next

                        Return CampaignList
                    End Using
                    _conn.Close()
                End Using
            End Using
        End Function


        Public Overrides Function SaveCampaign(ByVal Camp As cKampanj, ByVal XML As System.Xml.XmlElement) As Boolean
            If Camp.ReadOnly AndAlso Camp.DatabaseID > 0 Then
                Throw New ReadOnlyException("Could not save the campaign because it is read only.")
                Exit Function
            End If
            Dim _dbLock As CampaignDBLock = GetCampaignLock(Camp.DatabaseID)
            If _dbLock.IsLocked AndAlso _dbLock.UserID <> TrinitySettings.UserID Then
                Dim _username = getUser(_dbLock.UserID)!name
                Throw New ReadOnlyException("Could not save the campaign because it is locked. Campaign was locked by '" & _username & "' at " & _dbLock.LockDate)
                Exit Function
            End If
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using Command As New SqlClient.SqlCommand
                    Command.Connection = _conn

                    Command.CommandText = "SELECT id from campaigns WHERE id = '" & Camp.DatabaseID & "'"
                    Using dt As New DataTable
                        Using rd As SqlClient.SqlDataReader = Command.ExecuteReader
                            dt.Load(rd)
                            rd.Close()
                        End Using
                        If dt.Rows.Count = 0 Then
                            Command.CommandText = "INSERT INTO campaigns ([campaignid],[name],[startdate],[enddate],[client],[product],[maintarget],[secondtarget],[thirdtarget],[planner],[buyer],[xml],[lastopened],[lastsaved],[originallocation],[originalfilechangeddate],[lockedby],[status],[seraphxml],[deletedon],[contractid]) VALUES('" & Camp.ID & "','" & Camp.Name & "','" & _
                                                    Date.FromOADate(Camp.StartDate) & "','" & Date.FromOADate(Camp.EndDate) & "'," & _
                                                    Camp.ClientID & "," & Camp.ProductID & ",'" & Camp.MainTarget.TargetName & "','" & _
                                                    Camp.SecondaryTarget.TargetName & "','" & Camp.ThirdTarget.TargetName & "'," & _
                                                    Camp.PlannerID & "," & Camp.BuyerID & ",@xml, GETDATE(),GETDATE(),'" & Camp.Filename & "','',0,'" & Camp.Status & "','','2000-01-01'," & Camp.ContractID & ");SELECT @@IDENTITY"
                            Command.Parameters.Add("@xml", SqlDbType.Xml)
                            Command.Parameters("@xml").Value = XML.OuterXml.ToString
                            Try
                                Camp.DatabaseID = Command.ExecuteScalar
                                _conn.Close()
                                Return True
                            Catch ex As SqlClient.SqlException
                                Windows.Forms.MessageBox.Show("Could not save campaign " & Camp.Name & ". Error message: " & vbNewLine & ex.Message)
                            End Try
                        Else
                            BackupCampaign(Camp.DatabaseID)
                            Command.CommandText = "UPDATE campaigns SET campaignid='" & Camp.ID & "',name = '" & Camp.Name & "', startdate='" & Date.FromOADate(Camp.StartDate) & "',enddate='" & Date.FromOADate(Camp.EndDate) & _
                            "',client=" & Camp.ClientID & ",product=" & Camp.ProductID & ",maintarget='" & Camp.MainTarget.TargetName & "',secondtarget='" & Camp.SecondaryTarget.TargetName & "'," & _
                            "thirdtarget='" & Camp.ThirdTarget.TargetName & "',planner=" & Camp.PlannerID & ",buyer=" & Camp.BuyerID & ",xml=@xml,lastsaved=GETDATE(),status='" & Camp.Status & "',deletedon='2000-01-01',contractid=" & Camp.ContractID & _
                            " WHERE " & "id=" & Camp.DatabaseID
                            Command.Parameters.Add("@xml", SqlDbType.Xml)
                            Command.Parameters("@xml").Value = XML.OuterXml.ToString
                            Try
                                Command.ExecuteNonQuery()
                                _conn.Close()
                                Return True
                            Catch ex As SqlClient.SqlException
                                Windows.Forms.MessageBox.Show("Could not save campaign " & Camp.Name & ". Error message: " & vbNewLine & ex.Message)
                            End Try
                        End If
                    End Using
                End Using
                _conn.Close()
            End Using
        End Function

        Private Sub BackupCampaign(id As Integer)
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using _cmd As New SqlClient.SqlCommand("SELECT * FROM campaigns WHERE id=" & id, _conn)
                    Dim _dt As New DataTable
                    Using _rd As SqlClient.SqlDataReader = _cmd.ExecuteReader
                        _dt.Load(_rd)
                        _rd.Close()
                    End Using
                    Dim _cols As New List(Of String)
                    Dim _vals As New List(Of String)
                    For Each _col As DataColumn In _dt.Columns
                        _cols.Add(IIf(_col.ColumnName = "id", "originalid", _col.ColumnName))
                        _vals.Add("@" & _col.ColumnName)
                        _cmd.Parameters.AddWithValue("@" & _col.ColumnName, _dt.Rows(0)(_col.ColumnName))
                    Next
                    _cmd.CommandText = String.Format("INSERT INTO [backup] ({0}) VALUES ({1})", String.Join(",", _cols.ToArray), String.Join(",", _vals.ToArray))
                    Try
                        _cmd.ExecuteNonQuery()
                    Catch ex As Exception

                    End Try
                End Using
                _conn.Close()
            End Using
        End Sub

        Public Overrides Sub Connect(ByVal startmode As Trinity.cDBReader.ConnectionPlace, Optional ByVal UpdateSchema As Boolean = True)

            Trinity.Helper.WriteToLogFile("Connecting to SQL database. Startmode = " & startmode)
            If startmode = ConnectionPlace.Network Then
                DataPath = TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locNetwork)
                Trinity.Helper.WriteToLogFile("Set Connection string:" & TrinitySettings.ConnectionString(Trinity.cSettings.SettingsLocationEnum.locNetwork) & "Uid=***;Pwd=***;")
                Using _conn As New SqlClient.SqlConnection(TrinitySettings.ConnectionString(Trinity.cSettings.SettingsLocationEnum.locNetwork) & "Integrated Security=True;" & ";")

                    Try
                        Trinity.Helper.WriteToLogFile("Connect to database")
                        _conn.Open()
                        _connectionString = TrinitySettings.ConnectionString(Trinity.cSettings.SettingsLocationEnum.locNetwork) & "Integrated Security=True;" & ";"
                        _conn.Close()
                        Trinity.Helper.WriteToLogFile("Connection succeeded")
                    Catch ex As Exception
                        Trinity.Helper.WriteToLogFile("Connection failed: " & ex.Message)
                    End Try
                End Using

                'Create the common general connection
                If TrinitySettings.ConnectionStringCommon() <> "" Then
                    Using _conn As New SqlClient.SqlConnection(TrinitySettings.ConnectionStringCommon() & "Uid=" & TrinitySettings.DBUserCommon & ";Pwd=" & TrinitySettings.DBPwdCommon & ";")
                        Try
                            Trinity.Helper.WriteToLogFile("Connect to common database")
                            _conn.Open()
                            _commonConnectionString = TrinitySettings.ConnectionStringCommon() & "Uid=" & TrinitySettings.DBUserCommon & ";Pwd=" & TrinitySettings.DBPwdCommon & ";"
                            _conn.Close()
                        Catch ex As SqlClient.SqlException
                            Throw New Exception(ex.Message)
                        End Try
                    End Using
                Else
                    _commonConnectionString = _connectionString
                End If

                'Create the common specifics connection
                If TrinitySettings.ConnectionStringCommonSpecifics() <> "" Then
                    Using _conn As New SqlClient.SqlConnection(TrinitySettings.ConnectionStringCommonSpecifics() & "Uid=" & TrinitySettings.DBUserCommon & ";Pwd=" & TrinitySettings.DBPwdCommon & ";")
                        Try
                            Trinity.Helper.WriteToLogFile("Connect to common database")
                            _conn.Open()
                            _commonConnectionString = TrinitySettings.ConnectionStringCommonSpecifics() & "Uid=" & TrinitySettings.DBUserCommon & ";Pwd=" & TrinitySettings.DBPwdCommon & ";"
                            _conn.Close()
                        Catch ex As SqlClient.SqlException
                            Throw New Exception(ex.Message)
                        End Try
                    End Using
                Else
                    _commonSpecificsConnectionString = _connectionString
                End If

            Else
                local = True
                DataPath = TrinitySettings.DataPath(Trinity.cSettings.SettingsLocationEnum.locLocal)
                Exit Sub
            End If
            If UpdateSchema Then
                UpdateDBSchema()
            End If
            If TrinitySettings.MessagingServer <> "" Then ListenForMessages()
        End Sub

        Public Sub New()
            TrinitySettings = New cSettings(Helper.GetSpecialFolder(Helper.CSIDLEnum.UserProfile) & "Trinity 4.0")
            'Campaign.RegisterProblemDetection(Me)
        End Sub

        Public Overrides Function addTVCheckInfo(ByVal dDate As Integer, ByVal MaM As Integer, ByVal SaM As Integer, ByVal channel As String, ByVal program As String, ByVal filmcode As String, ByVal remark As String, ByVal status As String) As Boolean
            Dim sb As New StringBuilder()
            sb.Append("INSERT INTO spotcontrol (date,MaM,SaM,channel,program,filmcode,remark,status) VALUES (")
            sb.Append(dDate)
            sb.Append(",")
            sb.Append(MaM)
            sb.Append(",")
            sb.Append(SaM)
            sb.Append(",'")
            sb.Append(channel)
            sb.Append("','")
            sb.Append(program)
            sb.Append("','")
            sb.Append(filmcode)
            sb.Append("','")
            sb.Append(remark)
            sb.Append("','")
            sb.Append(status)
            sb.Append("')")

            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand(sb.ToString, _conn)
                    com.ExecuteNonQuery()
                    com.Dispose()
                    _conn.Close()
                End Using
            End Using
        End Function

        Public Overrides Function ReadPaths(ByVal User As String) As DataTable
            Dim _paths = New DataTable

            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()

                Using com As New SqlClient.SqlCommand("SELECT paths.* FROM paths,users Where users.Name='" & User & "' AND Paths.UserID=Users.ID", _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        _paths.load(rd)
                        rd.Close()
                    End Using
                End Using
                _conn.Close()
                Return _paths
            End Using
        End Function

        Public Overrides Sub ReadUsers()
            _users = New List(Of String)

            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("SELECT Name FROM Users ORDER BY Name", _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        While rd.Read
                            _users.Add(rd!name)
                        End While
                        rd.Close()
                    End Using
                End Using
                _conn.Close()
            End Using
        End Sub

        Public Overrides Function getAllClients() As DataTable
            'Sets up the table
            Using _clients As DataTable = New DataTable
                Using _conn As New SqlClient.SqlConnection(_connectionString)
                    _conn.Open()
                    Using com As New SqlClient.SqlCommand("SELECT * FROM Clients", _conn)
                        Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                            _clients.Load(rd)
                            rd.Close()
                            _conn.Close()
                            Return _clients
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Function getClient(ByVal ID As Integer) As String
            Dim _client As String

            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("SELECT * FROM Clients WHERE ID=" + ID.ToString, _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        Try
                            If rd.Read Then
                                _client = rd!Name
                                rd.Close()
                                _conn.Close()
                                Return _client
                                Exit Function
                            End If
                            rd.Close()
                            _conn.Close()
                            Return ""
                        Catch
                            rd.Close()
                            _conn.Close()
                            Return ""
                        End Try
                    End Using
                End Using
            End Using
        End Function


        Public Overrides Function FindProgramDuring(ByVal MaM As Integer, ByVal DuringDate As Date, ByVal Area As String, Optional ByVal frm As Object = Nothing) As DataTable
            'returns a data table that contains info of program during the marked slot and optionally calls a delegate to update a grid

            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()

                'Dim SQLString As String = "select format(events.date,""yyyy-mm-dd"") as [Date],events.time as [Time],events.channel as [Chan],events.name as [Programme] from events where (date=" & DuringDate.ToOADate & ") and ((StartMam<=" & MaM & " and StartMam+Duration>" & MaM & ") or (StartMam>=" & MaM & " and StartMam<Duration+" & MaM & "))"
                'SQLString = "SELECT format(events.date,'yyyymmdd') as [Date],events.time as [Time],events.channel as [Chan],events.name as [Programme] from events where (date=" & DuringDate.ToOADate & ") and ((StartMam<=" & MaM & " and StartMam+Duration>" & MaM & ") or (StartMam>=" & MaM & " and StartMam<Duration+" & MaM & "))"
                '*** events.date-2 is to make SQL-server return the same date as VB
                Dim events As String = "events"
                If TrinitySettings.SharedDatabaseName <> "" Then
                    events = TrinitySettings.SharedDatabaseName & "." & events
                End If
                Dim sb As New StringBuilder()
                sb.Append("SELECT convert(varchar,cast(cast(e.date-2 as numeric) as DateTime),112) as [Date],e.time as [Time],e.channel as [Chan],e.name as [Programme] from " & events & " e where (date=")
                sb.Append(DuringDate.ToOADate)
                sb.Append(") and ((StartMam<=")
                sb.Append(MaM)
                sb.Append(" and StartMam+Duration>")
                sb.Append(MaM)
                sb.Append(") or (StartMam>=")
                sb.Append(MaM)
                sb.Append(" and StartMam<Duration+")
                sb.Append(MaM)
                sb.Append(")) and Area='")
                sb.Append(Area)
                sb.Append("'")
                sb.Append("")
                sb.Append("")
                sb.Append("")

                Using Command As New SqlClient.SqlCommand(sb.ToString, _conn)
                    Using Rd As SqlClient.SqlDataReader = Command.ExecuteReader

                        Using Table As New DataTable

                            Table.Locale = System.Globalization.CultureInfo.InvariantCulture

                            Table.Load(Rd)

                            Rd.Close()
                            _conn.Close()

                            If frm Is Nothing Then
                                Return Table
                            Else
                                frm.Invoke(New GotProgsInOtherChannels(AddressOf frm.SetOtherGrid), New Object() {Table})
                                Return Table
                            End If
                        End Using
                    End Using
                End Using
            End Using
            Exit Function
        End Function

        Public Overrides Function QUERY(ByVal sql) As Object
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand(sql, _conn)
                    Try
                        Dim _retVal As Object = com.ExecuteScalar
                        _conn.Close()
                        Return _retVal
                    Catch ex As SqlClient.SqlException
                        _conn.Close()
                        Return False
                    End Try
                End Using
            End Using
        End Function

        Public Overrides Function getAllFromProducts(ByVal productID As Integer) As DataTable
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using _products As DataTable = New DataTable
                    Using com As New SqlClient.SqlCommand("SELECT * FROM products Where ID=" + productID.ToString, _conn)
                        Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                            _products.Load(rd)
                            rd.Close()
                            _conn.Close()
                            Return _products
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Function getAllProducts(ByVal clientID As Integer) As DataTable

            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using _products As DataTable = New DataTable
                    Using com As New SqlClient.SqlCommand("SELECT * FROM products Where ClientID=" + clientID.ToString, _conn)
                        Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                            _products.Load(rd)
                            rd.Close()
                            _conn.Close()
                            Return _products
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Function getAllProductsSync() As DataTable

            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using _products As DataTable = New DataTable
                    Using com As New SqlClient.SqlCommand("SELECT * FROM products", _conn)
                        Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                            _products.Load(rd)
                            rd.Close()
                            _conn.Close()
                            Return _products
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Function getProduct(ByVal productID As Integer) As String
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Dim _product As String = ""
                Using com As New SqlClient.SqlCommand("SELECT * FROM products Where ID=" + productID.ToString, _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        While rd.Read
                            _product = rd!Name
                        End While
                        rd.Close()
                        _conn.Close()
                        Return _product
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Function getProduct(ByVal Mam As Long, ByVal DuringDate As Date) As DataTable
            Dim events As String = "events"
            If TrinitySettings.SharedDatabaseName <> "" Then
                events = TrinitySettings.SharedDatabaseName & "." & events
            End If
            Dim sb As New StringBuilder()
            sb.Append("SELECT format(e.date,'yyyymmdd') as [Date],e.time as [Time],e.channel as [Chan],e.name as [Programme] from " & events & " as e where (date=")
            sb.Append(DuringDate.ToOADate)
            sb.Append(") and ((StartMam<=")
            sb.Append(Mam)
            sb.Append(" and StartMam+Duration>")
            sb.Append(Mam)
            sb.Append(") or (StartMam>=")
            sb.Append(Mam)
            sb.Append(" and StartMam<Duration+")
            sb.Append(Mam)
            sb.Append("))")

            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand(sb.ToString, _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        Using _product As New DataTable
                            _product.Load(rd)
                            rd.Close()
                            _conn.Close()
                            Return _product
                        End Using
                    End Using
                End Using
            End Using

        End Function

        Public Overrides Function FilmExist(ByVal name As String, ByVal productID As Integer) As Boolean
            Dim _film As Boolean = False

            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("SELECT * FROM films Where Product=" + productID.ToString + " AND Name='" + name + "'", _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        If rd.HasRows Then
                            _film = True
                        End If
                        rd.Close()
                        _conn.Close()
                        Return _film
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Sub DeleteFilm(ByVal name As String, ByVal productID As Integer)
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("DELETE FROM Films WHERE Name ='" & name & "' AND 1Product=" & productID, _conn)
                    com.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        Public Overrides Function ClientExist(ByVal name As String) As Boolean
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Dim _client As Boolean = False
                Using com As New SqlClient.SqlCommand("SELECT * FROM clients Where name='" + name + "'", _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        If rd.HasRows Then
                            _client = True
                        End If
                        rd.Close()
                        _conn.Close()
                        Return _client
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Function productExist(ByVal name As String) As Boolean
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Dim _product As Boolean = False
                Using com As New SqlClient.SqlCommand("SELECT * FROM products Where Name='" + name + "'", _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        If rd.HasRows Then
                            _product = True
                        End If
                        rd.Close()
                        _conn.Close()
                        Return _product
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Sub addClient(ByVal name As String)
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand
                    com.Connection = _conn
                    com.CommandText = "INSERT INTO clients(Name) VALUES ('" + name + "')"
                    com.ExecuteNonQuery()
                    _conn.Close()
                End Using
            End Using
        End Sub

        Public Overrides Sub addProduct(ByVal Name As String, ByVal ClientID As String, ByVal MarathonClient As String, ByVal MarathonProduct As String, ByVal MarathonCompany As String, ByVal MarathonContract As String, ByVal AdedgeBrands As List(Of String), ByVal AdTooxAdvertiserID As Long, ByVal AdTooxDivisionID As Long, ByVal AdTooxBrandID As Long, ByVal AdTooxProductType As String)
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand

                    Dim TmpAdedgeBrands As String = ""
                    For Each TmpString As String In AdedgeBrands
                        TmpAdedgeBrands &= TmpString & "|"
                    Next

                    TmpAdedgeBrands = TmpAdedgeBrands.Trim("|")

                    Dim sb As New StringBuilder()
                    sb.Append("INSERT INTO Products (Name,ClientID,MarathonClient,MarathonProduct,MarathonCompany,MarathonContract,AdedgeBrands,AdtooxAdvertiserID,AdTooxDivisionID,AdTooxBrandID,AdTooxProductType) VALUES ('")
                    sb.Append(Name)
                    sb.Append("','")
                    sb.Append(ClientID)
                    sb.Append("','")
                    sb.Append(MarathonClient)
                    sb.Append("','")
                    sb.Append(MarathonProduct)
                    sb.Append("','")
                    sb.Append(MarathonCompany)
                    sb.Append("','")
                    sb.Append(MarathonContract)
                    sb.Append("','")
                    sb.Append(TmpAdedgeBrands)
                    sb.Append("',")
                    sb.Append(AdTooxAdvertiserID)
                    sb.Append(",")
                    sb.Append(AdTooxDivisionID)
                    sb.Append(",")
                    sb.Append(AdTooxBrandID)
                    sb.Append(",'")
                    sb.Append(AdTooxProductType)
                    sb.Append("')")

                    com.Connection = _conn
                    com.CommandText = sb.ToString
                    com.ExecuteNonQuery()
                End Using
                _conn.Close()
            End Using
        End Sub

        Public Overrides Sub updateClient(ByVal name As String, ByVal id As Integer)
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand
                    com.Connection = _conn
                    com.CommandText = "UPDATE Clients SET Name='" & name & "' WHERE id=" & id.ToString
                    com.ExecuteNonQuery()
                End Using
                _conn.Close()
            End Using
        End Sub

        Public Overrides Sub updateProduct(ByVal ProductID As String, ByVal Name As String, ByVal ClientID As String, ByVal MarathonClient As String, ByVal MarathonProduct As String, ByVal MarathonCompany As String, ByVal MarathonContract As String, ByVal AdedgeBrands As List(Of String), ByVal AdTooxAdvertiserID As Long, ByVal AdTooxDivisionID As Long, ByVal AdTooxBrandID As Long, ByVal AdTooxProductType As String)
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand
                    com.Connection = _conn

                    Dim TmpAdedgeBrands As String = ""
                    For Each TmpString As String In AdedgeBrands
                        TmpAdedgeBrands &= TmpString & "|"
                    Next
                    TmpAdedgeBrands = TmpAdedgeBrands.Trim("|")

                    Dim sb As New StringBuilder()
                    sb.Append("UPDATE Products SET Name='")
                    sb.Append(Name)
                    sb.Append("',ClientID=")
                    sb.Append(ClientID)
                    sb.Append(",MarathonClient='")
                    sb.Append(MarathonClient)
                    sb.Append("',MarathonProduct='")
                    sb.Append(MarathonProduct)
                    sb.Append("',MarathonCompany='")
                    sb.Append(MarathonCompany)
                    sb.Append("',MarathonContract='")
                    sb.Append(MarathonContract)
                    sb.Append("',AdedgeBrands='")
                    sb.Append(TmpAdedgeBrands)
                    sb.Append("',AdTooxAdvertiserID=")
                    sb.Append(AdTooxAdvertiserID)
                    sb.Append(",AdtooxDivisionID=")
                    sb.Append(AdTooxDivisionID)
                    sb.Append(",AdTooxBrandID=")
                    sb.Append(AdTooxBrandID)
                    sb.Append(",AdTooxProductType='")
                    sb.Append(AdTooxProductType)
                    sb.Append("' WHERE ID=")
                    sb.Append(ProductID)

                    com.CommandText = sb.ToString
                    com.ExecuteNonQuery()
                End Using
                _conn.Close()
            End Using
        End Sub

        Public Overrides Function findFilmOnProduct(ByVal name As String, ByVal d As Date, ByVal productID As Integer) As DataTable

            Dim dd As String = (Format(d, "Short date"))

            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("SELECT DISTINCT Films.Name,Films.Length,Films.Description,CONVERT(VARCHAR(10), Films.Created, 20) AS Created FROM Films,Products WHERE (Films.Name like '%" & name & "%' or Films.Description like '%" & name & "%') AND Films.Created>='" & dd & "' AND Films.Product=" & productID.ToString, _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        Using _film As New DataTable
                            _film.Load(rd)
                            rd.Close()
                            _conn.Close()
                            Return _film
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Function findFilmAndProduct(ByVal name As String, ByVal productID As Integer) As DataTable

            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As SqlClient.SqlCommand = New SqlClient.SqlCommand("SELECT Films.* FROM Films,Products WHERE Films.Name ='" & name & "' AND Films.Product=" & productID, _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        Using _film As New DataTable
                            _film.Load(rd)
                            rd.Close()
                            _conn.Close()
                            Return _film
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Function findFilmAndProduct(ByVal name As String) As DataTable

            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As SqlClient.SqlCommand = New SqlClient.SqlCommand("SELECT Films.* FROM Films,Products WHERE Films.Name ='" & name & "'", _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        Using _film As New DataTable
                            _film.Load(rd)
                            rd.Close()
                            _conn.Close()
                            Return _film
                        End Using
                    End Using
                End Using
            End Using

        End Function

        Public Overrides Function findFilmAndProductClient(ByVal name As String, ByVal clientID As Integer) As DataTable

            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As SqlClient.SqlCommand = New SqlClient.SqlCommand("SELECT Films.* FROM Films,Products WHERE Films.Name ='" & name & "' AND (Films.Product=Products.ID and Products.ClientID=" & clientID & ")", _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        Using _film As New DataTable
                            _film.Load(rd)
                            rd.Close()
                            _conn.Close()
                            Return _film
                        End Using
                    End Using
                End Using
            End Using

        End Function

        Public Overrides Function findFilmOnClient(ByVal name As String, ByVal d As Date, ByVal clientID As Integer) As DataTable

            Dim dd As String = (Format(d, "Short date"))
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As SqlClient.SqlCommand = New SqlClient.SqlCommand("SELECT DISTINCT Films.Name,Films.Length,Films.Description,CONVERT(VARCHAR(10), Films.Created, 20) AS Created FROM Films,Products WHERE (Films.Name like '%" & name & "%' or Films.Description like '%" & name & "%') AND Films.Created>='" & dd & "'AND (Films.Product=Products.ID and Products.ClientID=" & clientID & ")", _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        Using _film As New DataTable
                            _film.Load(rd)
                            rd.Close()
                            _conn.Close()
                            Return _film
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Function getAllFilms() As DataTable
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("SELECT DISTINCT Films.Name,Films.Length,Films.Description,CONVERT(VARCHAR(10), Films.Created, 20) AS Created FROM Films", _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        Using _film As New DataTable
                            _film.Load(rd)
                            rd.Close()
                            _conn.Close()
                            Return _film
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Function getAllPathsSync() As DataTable
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("SELECT * FROM Paths", _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        Using _paths As New DataTable
                            _paths.Load(rd)
                            rd.Close()
                            _conn.Close()
                            com.Dispose()
                            Return _paths
                        End Using
                    End Using
                End Using
            End Using

        End Function

        Public Overrides Function getAllUsersSync() As DataTable
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("SELECT * FROM Users", _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        Using _users As New DataTable
                            _users.Load(rd)
                            rd.Close()
                            _conn.Close()
                            com.Dispose()
                            Return _users
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Function getAllFilmsSync() As DataTable
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("SELECT * FROM Films", _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        Using _film As New DataTable
                            _film.Load(rd)
                            rd.Close()
                            _conn.Close()
                            Return _film
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Function addOrUpdateFilm(ByVal name As String, ByVal productID As Integer, ByVal filmCode As String, ByVal channel As String, ByVal description As String, ByVal length As Integer, ByVal index As Decimal) As Boolean
            Dim _film As Boolean = False

            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("SELECT * FROM Films WHERE Name='" & name & "' AND Product=" & productID & " AND Channel='" & channel & "'", _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader

                        Dim sb As New StringBuilder()
                        If rd.HasRows Then
                            rd.Close()
                            sb.Append("UPDATE Films SET Filmcode='")
                            sb.Append(filmCode)
                            sb.Append("',Name='")
                            sb.Append(name)
                            sb.Append("',Description='")
                            sb.Append(description)
                            sb.Append("',Length='")
                            sb.Append(length)
                            sb.Append("',Channel='")
                            sb.Append(channel)
                            sb.Append("',Product='")
                            sb.Append(productID)
                            sb.Append("',[Index]=")
                            sb.Append(index.ToString.Replace(",", "."))
                            sb.Append(",Created='")
                            sb.Append(Now)
                            sb.Append("' WHERE Name='")
                            sb.Append(name)
                            sb.Append("' and Channel='")
                            sb.Append("channel")
                            sb.Append("'")
                        Else
                            rd.Close()
                            sb.Append("INSERT INTO Films (Filmcode,Name,Description,Length,Channel,Product,[Index],Created) VALUES ('")
                            sb.Append(filmCode)
                            sb.Append("','")
                            sb.Append(name)
                            sb.Append("','")
                            sb.Append(description)
                            sb.Append("','")
                            sb.Append(length)
                            sb.Append("','")
                            sb.Append(channel)
                            sb.Append("','")
                            sb.Append(productID)
                            sb.Append("',")
                            sb.Append(index.ToString.Replace(",", "."))
                            sb.Append(",'")
                            sb.Append(Now)
                            sb.Append("')")
                        End If

                        com.CommandText = sb.ToString
                        com.ExecuteNonQuery()
                        _film = True

                        rd.Close()
                        _conn.Close()
                        Return _film
                    End Using
                End Using
            End Using

        End Function


        Public Overrides Function getAllEvents() As DataTable
            Dim events As String = "events"
            If TrinitySettings.SharedDatabaseName <> "" Then
                events = TrinitySettings.SharedDatabaseName & "." & events
            End If
            'Use common database if available
            Dim _conStr As String
            If _commonSpecificsConnectionString <> "" Then
                _conStr = _commonSpecificsConnectionString
            Else
                _conStr = _connectionString
            End If
            Using _conn As New SqlClient.SqlConnection(_conStr)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("SELECT * FROM " & events, _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        Using _events As DataTable = New DataTable
                            _events.Load(rd)
                            rd.Close()
                            _conn.Close()
                            Return _events
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Function getAllEvents(ByVal startDate As Long, ByVal endDate As Long) As DataTable
            Dim events As String = "events"
            If TrinitySettings.SharedDatabaseName <> "" Then
                events = TrinitySettings.SharedDatabaseName & "." & events
            End If
            'Use common database if available
            Dim _conStr As String
            If _commonSpecificsConnectionString <> "" Then
                _conStr = _commonSpecificsConnectionString
            Else
                _conStr = _connectionString
            End If
            Using _conn As New SqlClient.SqlConnection(_conStr)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("SELECT * FROM " & events & " WHERE Date >= " + startDate.ToString + " AND Date <= " + endDate.ToString + " ORDER BY Date,Time", _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        Using _events As DataTable = New DataTable
                            _events.Load(rd)
                            rd.Close()
                            _conn.Close()
                            Return _events
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Function getAllEventsSync(ByVal dDate As Long) As DataTable
            Dim events As String = "events"
            If TrinitySettings.SharedDatabaseName <> "" Then
                events = TrinitySettings.SharedDatabaseName & "." & events
            End If
            'Use common database if available
            Dim _conStr As String
            If _commonSpecificsConnectionString <> "" Then
                _conStr = _commonSpecificsConnectionString
            Else
                _conStr = _connectionString
            End If
            Using _conn As New SqlClient.SqlConnection(_conStr)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("SELECT * FROM " & events & " WHERE Date>=" + dDate.ToString, _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        Using _events As DataTable = New DataTable
                            _events.Load(rd)
                            rd.Close()
                            _conn.Close()
                            Return _events
                        End Using
                    End Using
                End Using
            End Using

        End Function


        Public Overrides Function getEvents(Dates As List(Of KeyValuePair(Of Date, Date)), ByVal TargetChannel As String, Optional Type As Integer = 0) As DataTable
            Dim events As String = "events"
            If TrinitySettings.SharedDatabaseName <> "" Then
                events = TrinitySettings.SharedDatabaseName & "." & events
            End If
            'Use common database if available
            Dim _conStr As String
            If _commonSpecificsConnectionString <> "" Then
                _conStr = _commonSpecificsConnectionString
            Else
                _conStr = _connectionString
            End If
            Dim _periodStrB As New System.Text.StringBuilder("(")
            For Each _date As KeyValuePair(Of Date, Date) In Dates
                _periodStrB.Append("(Date>=" & _date.Key.ToOADate & " AND Date<=" & _date.Value.ToOADate & ") OR")
            Next
            Dim _periodStr As String = _periodStrB.ToString.Substring(0, _periodStrB.Length - 3) & ")"
            Using _conn As New SqlClient.SqlConnection(_conStr)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("SELECT * FROM " & events & " WHERE channel ='" + TargetChannel + "' AND " & _periodStr & " AND Type=" & Type & " ORDER BY Date,Time", _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        Using _events As DataTable = New DataTable
                            _events.Load(rd)
                            _conn.Close()
                            rd.Close()
                            Return _events
                        End Using
                    End Using
                End Using
            End Using
        End Function

        'Public Overrides Function getEvents(ByVal SQLstring As String) As DataTable

        '    'Use common database if available
        '    Dim _conStr As String
        '    If _commonSpecificsConnectionString <> "" Then
        '        _conStr = _commonSpecificsConnectionString
        '    Else
        '        _conStr = _connectionString
        '    End If
        '    Using _conn As New SqlClient.SqlConnection(_conStr)
        '        _conn.Open()
        '        Using com As New SqlClient.SqlCommand(SQLstring, _conn)
        '            Using rd As SqlClient.SqlDataReader = com.ExecuteReader
        '                Using _events As DataTable = New DataTable
        '                    _events.Load(rd)
        '                    _conn.Close()
        '                    rd.Close()
        '                    Return _events
        '                End Using
        '            End Using
        '        End Using
        '    End Using
        'End Function

        Public Overrides Function checkPwd(ByVal UserID As Integer, ByVal s As String) As Boolean
            Return False
        End Function

        Public Overrides Function updatePwd(ByVal s As String, ByVal UserID As String) As Boolean
            Return False
        End Function

        Public Overrides Function getUserID(ByVal User As String) As Integer
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                If User = """""" Then User = Nothing
                'Dim rd As SqlClient.SqlDataReader
                Using com As New SqlClient.SqlCommand("SELECT id FROM people WHERE name='" & User & "'", _conn)
                    Try
                        Dim _id As Integer = com.ExecuteScalar
                        _conn.Close()
                        Return _id
                        'rd = com.ExecuteReader
                        'If rd.HasRows Then
                        '    Return rd!id
                        'Else
                        '    Return -1
                        'End If
                    Catch ex As SqlClient.SqlException
                        Debug.Print("Problem with SQL query SELECT id FROM people WHERE name='" & User & "'", ex.Message)
                        _conn.Close()
                        Return -1
                    End Try
                End Using
            End Using
            'If rd.HasRows Then
            '    Return rd!id
            'Else
            '    Return -1
            'End If
        End Function

        Public Overrides Function getDBType() As Integer
            Return DBTYPE.SQL
        End Function

        Public Overrides Sub closeConnection()
            unlockCampaign(Campaign.DatabaseID)
        End Sub

        Public Overrides Function getUsers() As DataTable
            Dim _users = New DataTable

            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("SELECT Name FROM Users ORDER BY Name", _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        _users.load(rd)
                        rd.Close()
                        _conn.Close()
                        Return _users
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Function getUser(ByVal ID As Integer) As DataRow
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("SELECT * FROM people WHERE id=" & ID, _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        Using _dt As New DataTable
                            _dt.Load(rd)
                            rd.Close()
                            _conn.Close()
                            If _dt.Rows.Count = 0 Then
                                Return Nothing
                            End If
                            Return _dt.Rows(0)
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Sub addPath(ByVal UserID As Integer, ByVal path As String)
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand
                    com.CommandText = "INSERT INTO paths(UserID,path) VALUES ('" + UserID.ToString + "','" + path + "')"
                    com.Connection = _conn
                    com.ExecuteNonQuery()
                End Using
                _conn.Close()
            End Using
        End Sub

        Public Overrides Function isLocal() As Boolean
            Return local
        End Function

        Public Overrides Sub clearPaths(ByVal userID As Integer)
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand
                    com.CommandText = "DELETE FROM paths WHERE UserID=" & userID
                    com.Connection = _conn
                    com.ExecuteNonQuery()
                    com.Dispose()
                End Using
                _conn.Close()
            End Using
        End Sub

        Public Overloads Overrides Function getTVCheckInfo(ByVal StartDate As Long, ByVal EndDate As Long, ByVal Filmcodes As System.Collections.Generic.List(Of String)) As System.Data.DataTable
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Try
                    Dim _remarks = New DataTable
                    Dim FilmStr As String = ""

                    For Each s As String In Filmcodes
                        FilmStr &= "filmcode='" & s & "' OR "
                    Next
                    If Not FilmStr = "" Then
                        FilmStr = Left(FilmStr, FilmStr.Length - 4)
                        Using com As New SqlClient.SqlCommand("SELECT * FROM SpotControl WHERE [date]>='" & StartDate & "' AND [date]<='" & EndDate & "' AND (" & FilmStr & ")", _conn)
                            Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                                _remarks.load(rd)
                                rd.Close()
                            End Using
                        End Using
                    End If
                    _conn.Close()
                    Return _remarks
                Catch ex As Exception
                    _conn.Close()
                    Return Nothing
                End Try
            End Using
        End Function
        Public Overrides Sub UpdateDBSchema()
            'Makes sure all tables look like they should. Each new column needs to be checked for and added manually.

            'Check for AdedgeBrands column
            'Dim rd As SqlClient.SqlDataReader
            'Dim com As New SqlClient.SqlCommand("SELECT * FROM Products", DBConn)
            'Dim dt As DataTable
            'rd = com.ExecuteReader
            'dt = rd.GetSchemaTable
            'rd.Close()
            'For Each TmpRow As DataRow In dt.Rows
            '    If TmpRow!ColumnName = "AdedgeBrands" Then Exit Sub
            'Next
            'com = New SqlClient.SqlCommand("ALTER TABLE Products ADD AdedgeBrands nvarchar(500)", DBConn)
            'com.ExecuteNonQuery()
            Trinity.Helper.WriteToLogFile("UpdateDBSchema")
            CheckColumn("AdedgeBrands", "Products", "nvarchar(500)")
            CheckColumn("Comment", "Events", "nvarchar(500)")
            CheckColumn("AdTooxAdvertiserId", "Products", "bigint")
            CheckColumn("AdTooxDivisionId", "Products", "bigint")
            CheckColumn("AdTooxBrandId", "Products", "bigint")
            CheckColumn("AdTooxProductType", "Products", "nvarchar(MAX)")
            CheckColumn("deletedon", "Contracts", "datetime")

            'If CheckColumn("Area", "Events", "nvarchar(20)") Then
            '    Using _conn As New SqlClient.SqlConnection(_connectionString)
            '        _conn.Open()
            '        Using com As New SqlClient.SqlCommand("UPDATE events SET Area='" & TrinitySettings.DefaultArea & "'", _conn)
            '            com.ExecuteNonQuery()
            '        End Using
            '        _conn.Close()
            '    End Using
            'End If

        End Sub

        Private Function CheckColumn(ByVal Column As String, ByVal Table As String, ByVal Columntype As String) As Boolean
            Trinity.Helper.WriteToLogFile("Check Column '" & Column & "' if table '" & Table & " with ColumnType '" & Columntype & "'")
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.Columns where TABLE_NAME = '" & Table.ToLower & "'", _conn)
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader
                        Trinity.Helper.WriteToLogFile("Get schema table")
                        Using dt As DataTable = New DataTable
                            dt.Load(rd)
                            For Each TmpRow As DataRow In dt.Rows
                                If (TmpRow!Column_Name).ToString.ToUpper = Column.ToUpper Then
                                    Trinity.Helper.WriteToLogFile("Column found - Do not add")
                                    rd.Close()
                                    _conn.Close()
                                    Return False
                                End If
                            Next
                        End Using
                        rd.Close()
                    End Using
                End Using
                Trinity.Helper.WriteToLogFile("Column not found - Add to table")
                Trinity.Helper.WriteToLogFile("SQL: ALTER TABLE " & Table & " ADD " & Column & " " & Columntype)
                Using com As New SqlClient.SqlCommand("ALTER TABLE " & Table & " ADD " & Column & " " & Columntype, _conn)
                    Trinity.Helper.WriteToLogFile("Execute")
                    com.ExecuteNonQuery()
                    Trinity.Helper.WriteToLogFile("Executed OK")
                    _conn.Close()
                    Return True
                End Using
            End Using
        End Function

        Public Overrides Function updateChannelInfo(ByRef channel As cChannel) As Boolean
            'Updates the channel with the latest information from the database

            If channel.databaseID = "NOT SAVED" Then Return True
            Using _conn As New SqlClient.SqlConnection(_commonConnectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("SELECT * FROM channels WHERE ChannelID='" & channel.databaseID & "'", _conn)
                    Try
                        Using rd As SqlClient.SqlDataReader = com.ExecuteReader()

                            If rd.Read() Then
                                channel.ChannelName = rd("ChannelName")
                                channel.Shortname = rd("ChannelShortname")
                                channel.MarathonName = rd("Marathonname")
                                channel.AdEdgeNames = rd("AdvantEdgename")
                                channel.MatrixName = rd("Matrixname")
                                channel.AgencyCommission = rd("Agencycommission")
                                channel.ListNumber = rd("sortNumber")
                                channel.ConnectedChannel = rd("Connectedchannel")
                                channel.LogoPath = rd("Logopath")
                                channel.DeliveryAddress = rd("Address")
                                channel.VHSAddress = rd("VHSAddress")
                                channel.fileName = rd("Channelset")
                                channel.Area = rd("Area")

                                rd.Close()
                                _conn.Close()
                                Return True

                            End If
                            rd.Close()
                        End Using
                    Catch ex As Exception

                    End Try
                    _conn.Close()
                    Return False
                End Using
            End Using
        End Function

        Public Overrides Function updateSpotIndex(ByRef BT As cBookingType) As Boolean
            'Updates the bookingtype with the latest spotindex from the database

            Using _conn As New SqlClient.SqlConnection(_commonConnectionString)
                _conn.Open()
                Try
                    Using com As New SqlClient.SqlCommand("SELECT * FROM spotindex WHERE BTID='" & BT.databaseID & "'", _conn)
                        Using rd As SqlClient.SqlDataReader = com.ExecuteReader()

                            If rd.Read() Then
                                BT.FilmIndex(rd("Length")) = rd("Idx")
                            End If

                            rd.Close()
                            _conn.Close()
                            Return True
                        End Using
                    End Using
                Catch ex As Exception

                End Try
                _conn.Close()
                Return False
            End Using
        End Function

        Public Overrides Function saveSpotIndex(ByVal BT As cBookingType) As Boolean
            'Saves the the latest spotindex to the database
            Using _conn As New SqlClient.SqlConnection(_commonConnectionString)
                _conn.Open()
                Try
                    Using com As New SqlClient.SqlCommand("DELETE FROM spotindex WHERE BTID='" & BT.databaseID & "'", _conn)
                        Dim x As Integer = com.ExecuteNonQuery()


                        For i As Integer = 0 To 500
                            If BT.FilmIndex(i) > 0 Then
                                com.CommandText = "insert into spotindex values('" & BT.databaseID & "'," & i & "," & BT.FilmIndex(i).ToString().Replace(",", ".") & ")"
                                x = com.ExecuteNonQuery()
                            End If
                        Next
                        _conn.Close()
                        Return True
                    End Using
                Catch ex As Exception

                End Try
                _conn.Close()
                Return False
            End Using
        End Function

        Public Overrides Function saveChannelInfo(ByVal channel As cChannel) As Boolean
            'Saves the information to the database

            Using _conn As New SqlClient.SqlConnection(_commonConnectionString)
                _conn.Open()
                Try
                    Using com As New SqlClient.SqlCommand
                        com.Connection = _conn

                        'Fix for adding channels from file
                        If (channel.channelSet = 0) Then
                            channel.channelSet = 1

                            If channel.fileName.Contains("TV4") Then
                                channel.channelSet = 3 'sätter dessa om orginal kanalernam eD? ändra i sådana fall
                            End If

                            If channel.fileName.Contains("TV3") Then
                                channel.channelSet = 2
                            End If

                        End If

                        Dim sb As New StringBuilder()

                        If channel.databaseID = "NOT SAVED" Then
                            channel.databaseID = System.Guid.NewGuid().ToString()
                            sb.Append("insert into channels(ChannelID, ChannelName, ChannelShortname, Marathonname, AdvantEdgename, AdTooxname, Matrixname, Buyinguniverse, Agencycommission, sortNumber, Connectedchannel, Logopath, Address, VHSAddress, Channelset, Area) values('")
                            sb.Append(channel.databaseID)
                            sb.Append("','")
                            sb.Append(channel.ChannelName)
                            sb.Append("','")
                            sb.Append(channel.Shortname)
                            sb.Append("','")
                            sb.Append(channel.MarathonName)
                            sb.Append("','")
                            sb.Append(channel.AdEdgeNames)
                            sb.Append("','")
                            sb.Append(channel.AdTooxChannelID)
                            sb.Append("','")
                            sb.Append(channel.MatrixName)
                            sb.Append("','")
                            sb.Append(channel.BuyingUniverse)
                            sb.Append("',")
                            sb.Append(channel.AgencyCommission.ToString().Replace(",", "."))
                            sb.Append(",")
                            sb.Append(channel.ListNumber)
                            sb.Append(",'")
                            sb.Append(channel.ConnectedChannel)
                            sb.Append("','")
                            sb.Append(channel.LogoPath)
                            sb.Append("','")
                            sb.Append(channel.DeliveryAddress)
                            sb.Append("','")
                            sb.Append(channel.VHSAddress)
                            sb.Append("',")
                            sb.Append(channel.channelSet)
                            sb.Append(",'")
                            sb.Append(channel.Area)
                            sb.Append("')")
                        Else
                            sb.Append("update channels set ChannelName ='")
                            sb.Append(channel.ChannelName)
                            sb.Append("', ChannelShortname ='")
                            sb.Append(channel.Shortname)
                            sb.Append("', Marathonname ='")
                            sb.Append(channel.MarathonName)
                            sb.Append("', AdvantEdgename ='")
                            sb.Append(channel.AdEdgeNames)
                            sb.Append("', AdTooxName ='")
                            sb.Append(channel.AdTooxChannelID)
                            sb.Append("', Matrixname ='")
                            sb.Append(channel.MatrixName)
                            sb.Append("', Buyinguniverse ='")
                            sb.Append(channel.BuyingUniverse)
                            sb.Append("', Agencycommission =")
                            sb.Append(channel.AgencyCommission.ToString().Replace(",", "."))
                            sb.Append(", sortNumber =")
                            sb.Append(channel.ListNumber)
                            sb.Append(", Connectedchannel ='")
                            sb.Append(channel.ConnectedChannel)
                            sb.Append("', Logopath ='")
                            sb.Append(channel.LogoPath)
                            sb.Append("', Address ='")
                            sb.Append(channel.DeliveryAddress)
                            sb.Append("', VHSAddress ='")
                            sb.Append(channel.VHSAddress)
                            sb.Append("', Channelset =")
                            sb.Append(channel.channelSet)
                            sb.Append(", Area = '")
                            sb.Append(channel.Area)
                            sb.Append("' where ChannelID ='")
                            sb.Append(channel.databaseID)
                            sb.Append("'")
                        End If

                        com.CommandText = sb.ToString
                        com.ExecuteNonQuery()
                    End Using
                    _conn.Close()
                    Return True
                Catch ex As Exception

                End Try
                _conn.Close()
                Return False
            End Using
        End Function

        Public Overrides Function updateBookingTypeInfo(ByRef BT As cBookingType) As Boolean
            'Updates the channel with the latest information from the database
            Using _conn As New SqlClient.SqlConnection(_commonConnectionString)
                _conn.Open()
                Try

                    Using com As New SqlClient.SqlCommand("SELECT * FROM bookingtypes WHERE ChannelID='" & BT.ParentChannel.databaseID & "' and BTID ='" & BT.databaseID & "'", _conn)
                        Using rd As SqlClient.SqlDataReader = com.ExecuteReader

                            If rd.Read() Then
                                BT.Name = rd("BTName")
                                BT.Shortname = rd("BTShortname")
                                BT.IsRBS = (rd("IsRBS") = 1)
                                BT.IsSpecific = (rd("IsSpecific") = 1)
                                BT.PrintBookingCode = rd("PrintBookingcode")
                                BT.PrintDayparts = rd("PrintDaypart")
                                BT.AverageRating = rd("AverageRating")

                                For index As Integer = 0 To 3
                                    If Not IsDBNull(rd("DefaultDP" & index)) Then
                                        BT.DefaultDaypart(index) = rd("DefaultDP" & index)
                                    End If
                                Next

                                If updateSpotIndex(BT) Then
                                    rd.Close()
                                    _conn.Close()
                                    Return True
                                End If
                            End If
                            rd.Close()
                        End Using
                    End Using

                Catch
                End Try
                _conn.Close()
                Return False
            End Using
        End Function

        Public Overrides Function saveBookingTypeInfo(ByVal BT As cBookingType) As Boolean
            'Saves the information to the database
            Using _conn As New SqlClient.SqlConnection(_commonConnectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand
                    com.Connection = _conn
                    Dim i As Integer

                    Dim sb As New StringBuilder()
                    Try
                        If BT.databaseID = "NOT SAVED" Then
                            BT.databaseID = System.Guid.NewGuid().ToString()
                            sb.Append("INSERT INTO bookingtypes(BTID, channelID, BTName, BTShortname, companyID, IsRBS, IsSpecific, IsPremium, IsCompensation, IsSponsorship, PrintBookingcode, PrintDaypart, AverageRating, EnhancementFactor, DefaultDP0, DefaultDP1, DefaultDP2, DefaultDP3) VALUES('")
                            sb.Append(BT.databaseID)
                            sb.Append("','")
                            sb.Append(BT.ParentChannel.databaseID)
                            sb.Append("','")
                            sb.Append(BT.Name)
                            sb.Append("','")
                            sb.Append(BT.Shortname)
                            sb.Append("',")
                            sb.Append(TrinitySettings.UserCompany)
                            sb.Append(",")
                            sb.Append((BT.IsRBS * -1))
                            sb.Append(",")
                            sb.Append((BT.IsSpecific * -1))
                            sb.Append(",")
                            sb.Append((BT.IsPremium * -1))
                            sb.Append(",")
                            sb.Append((BT.IsCompensation * -1))
                            sb.Append(",")
                            sb.Append((BT.IsSponsorship * -1))
                            sb.Append(",")
                            sb.Append((BT.PrintBookingCode * -1))
                            sb.Append(",")
                            sb.Append((BT.PrintDayparts * -1))
                            sb.Append(",")
                            sb.Append(BT.AverageRating.ToString().Replace(",", "."))
                            sb.Append(",")
                            sb.Append(BT.EnhancementFactor)

                            For i = 0 To BT.Dayparts.Count - 1
                                sb.Append(",")
                                sb.Append(BT.DefaultDaypart(i))
                            Next

                            While i < 4
                                sb.Append(",null")
                                i += 1
                            End While
                            sb.Append(")")
                        Else
                            sb.Append("UPDATE bookingtypes SET BTName ='")
                            sb.Append(BT.Name)
                            sb.Append("', BTShortname ='")
                            sb.Append(BT.Shortname)
                            sb.Append("', IsRBS =")
                            sb.Append((BT.IsRBS * -1))
                            sb.Append(", IsSpecific =")
                            sb.Append((BT.IsSpecific * -1))
                            sb.Append(", IsPremium =")
                            sb.Append((BT.IsPremium * -1))
                            sb.Append(", IsCompensation =")
                            sb.Append((BT.IsCompensation * -1))
                            sb.Append(", IsSponsorship =")
                            sb.Append((BT.IsSponsorship * -1))
                            sb.Append(", PrintBookingcode =")
                            sb.Append((BT.PrintBookingCode * -1))
                            sb.Append(", PrintDaypart =")
                            sb.Append((BT.PrintDayparts * -1))
                            sb.Append(", AverageRating =")
                            sb.Append(BT.AverageRating.ToString().Replace(",", "."))
                            sb.Append(", EnhancementFactor = ")
                            sb.Append(BT.EnhancementFactor)

                            For i = 0 To BT.Dayparts.Count - 1
                                sb.Append(", DefaultDP")
                                sb.Append(i)
                                sb.Append(" =")
                                sb.Append(BT.DefaultDaypart(i))
                            Next

                            While i < 4
                                sb.Append(",DefaultDP")
                                sb.Append(i)
                                sb.Append(" =null")
                                i += 1
                            End While
                            sb.Append(" WHERE BTID='")
                            sb.Append(BT.databaseID)
                            sb.Append("'")
                        End If

                        com.CommandText = sb.ToString()
                        com.ExecuteNonQuery()

                        'Save filmindex
                        If Not saveSpotIndex(BT) Then
                            Return False
                        End If

                    Catch ex As Exception
                        _conn.Close()
                        Return False
                    End Try
                    _conn.Close()
                End Using
            End Using
            Return True
        End Function

        Public Overrides Function readPricelist(ByRef BT As cBookingType) As Boolean


            Using _conn As New SqlClient.SqlConnection(_commonConnectionString)
                _conn.Open()
                Try
                    'Read the pricelist
                    Dim NatUni As New Hashtable()
                    Dim Uni As New Hashtable()
                    Using com As New SqlClient.SqlCommand("SELECT * FROM pricelistrows WHERE BTID='" & BT.databaseID & "'", _conn)
                        Using rd As SqlClient.SqlDataReader = com.ExecuteReader()



                            Dim target As cPricelistTarget
                            While rd.Read()
                                target = BT.Pricelist.Targets.Add(rd("TargetName").ToString.Trim, BT)
                                target.databaseID = rd("TargetID")
                                target.CalcCPP = (rd("calcCPP") = 1)
                                NatUni.Add(target.TargetName, rd("NatUniverse"))
                                Uni.Add(target.TargetName, rd("Universe"))
                                target.StandardTarget = (rd("StandardTarget") = 1)
                                target.Target.NoUniverseSize = True
                                target.Target.Universe() = BT.ParentChannel.BuyingUniverse
                                target.Bookingtype = BT
                                target.Target.TargetName = rd("AdvantEdgeTarget")
                                target.MaxRatings = rd("MaxRatings")
                            End While

                            rd.Close()
                        End Using

                        'Read all pricerows
                        Dim period As cPricelistPeriod
                        For Each target As cPricelistTarget In BT.Pricelist.Targets
                            'Get the pricelist items
                            com.CommandText = "SELECT * FROM Pricelistrows WHERE TargetID='" & target.databaseID & "'"
                            Using rd As SqlClient.SqlDataReader = com.ExecuteReader()

                                While rd.Read()
                                    period = target.PricelistPeriods.Add(rd("Name"))
                                    period.FromDate = rd("FromDate")
                                    period.ToDate = rd("ToDate")
                                    period.PriceIsCPP = (rd("isCPP") = 1)
                                    period.TargetNat = NatUni(target.TargetName)
                                    period.TargetUni = Uni(target.TargetName)

                                    If target.CalcCPP Then
                                        Dim tmpName As String
                                        For i As Integer = 0 To BT.Dayparts.Count - 1
                                            tmpName = "PriceDP" & i
                                            If Not IsDBNull(rd(tmpName)) Then
                                                period.Price(period.PriceIsCPP, i) = rd(tmpName)
                                            End If
                                        Next
                                    Else
                                        period.Price(period.PriceIsCPP) = rd("PriceDP0")
                                    End If
                                End While

                                rd.Close()
                            End Using
                            'Get the Indexes
                            com.CommandText = "SELECT * FROM priceListIndexes WHERE TargetID='" & target.databaseID & "'"
                            Using rd As SqlClient.SqlDataReader = com.ExecuteReader()

                                Dim index As cIndex
                                While rd.Read()
                                    index = target.Indexes.Add(rd("IndexName"), rd("IndexID"))
                                    index.FromDate = rd("FromDate")
                                    index.ToDate = rd("ToDate")
                                    index.IndexOn = rd("IndexOn")
                                    index.Index = rd("Idx")
                                    index.SystemGenerated = rd("SystemGenerated")
                                End While

                                rd.Close()
                            End Using
                        Next
                    End Using
                    _conn.Close()
                    Return True
                Catch

                End Try
                _conn.Close()
                Return False
            End Using
        End Function

        Public Overrides Function checkForNewChannels(ByRef OnCampaign As cKampanj) As Boolean
            Dim channelList As New StringBuilder

            For Each tmpChan As cChannel In OnCampaign.Channels
                channelList.Append("','")
                channelList.Append(tmpChan.databaseID)
            Next
            channelList.Append("')")

            Using _conn As New SqlClient.SqlConnection(_commonConnectionString)
                _conn.Open()
                Using cmd As New SqlClient.SqlCommand()


                    cmd.Connection = _conn
                    cmd.CommandText = "SELECT * FROM channels WHERE channelid not in (" & channelList.ToString().Substring(2)

                    Dim needsBT As New List(Of String)
                    Using rd As SqlClient.SqlDataReader = cmd.ExecuteReader
                        Dim channel As cChannel

                        While rd.Read()
                            channel = OnCampaign.Channels.Add(rd("ChannelName"), rd("ChannelSet"), rd("Area"), rd("ChannelID"))
                            channel.ChannelName = rd("ChannelName")
                            channel.Shortname = rd("ChannelShortname")
                            channel.MarathonName = rd("Marathonname")
                            channel.AdEdgeNames = rd("AdvantEdgename")
                            channel.AdTooxChannelID = rd("AdTooxname")
                            channel.MatrixName = rd("Matrixname")
                            channel.BuyingUniverse = rd("Buyinguniverse")
                            channel.AgencyCommission = rd("Agencycommission")
                            channel.ListNumber = rd("SortNumber")
                            channel.ConnectedChannel = rd("Connectedchannel")
                            channel.LogoPath = rd("Logopath")
                            channel.DeliveryAddress = rd("Address")
                            channel.VHSAddress = rd("VHSAddress")

                            needsBT.Add(channel.ChannelName)
                        End While

                        rd.Close()
                    End Using
                    For Each s As String In needsBT
                        OnCampaign.Channels(s).readDefaultBookingTypes()
                    Next
                End Using
                _conn.Close()
            End Using
            Return True
        End Function

        Public Overrides Function checkForNewBookingTypes(ByRef channel As cChannel) As Boolean
            Dim btList As New StringBuilder

            For Each tmpBT As cBookingType In channel.BookingTypes
                btList.Append("','")
                btList.Append(tmpBT.databaseID)
            Next
            btList.Append("')")

            Using _conn As New SqlClient.SqlConnection(_commonConnectionString)
                _conn.Open()
                Using cmd As New SqlClient.SqlCommand()

                    cmd.Connection = _conn
                    cmd.CommandText = "SELECT * FROM bookingtypes WHERE channelid ='" & channel.databaseID & "' and (companyID = 1 OR companyID = " & TrinitySettings.UserCompany & ") and BTID not in (" & btList.ToString().Substring(2)

                    Using rd As SqlClient.SqlDataReader = cmd.ExecuteReader
                        Dim bt As cBookingType

                        Dim needsSpotIndex As New List(Of String)
                        While rd.Read()
                            bt = channel.BookingTypes.Add(rd("BTName"), False)
                            bt.Shortname = rd("BTShortname")
                            bt.IsRBS = (rd("IsRBS") = 1)
                            bt.IsSpecific = (rd("IsSpecific") = 1)
                            bt.PrintBookingCode = rd("PrintBookingcode")
                            bt.PrintDayparts = rd("PrintDaypart")
                            bt.AverageRating = rd("AverageRating")
                            bt.Dayparts(1).Share = rd("DefaultDP0")
                            bt.Dayparts(2).Share = rd("DefaultDP1")
                            bt.Dayparts(3).Share = rd("DefaultDP2")
                            bt.Dayparts(4).Share = rd("DefaultDP3")

                            needsSpotIndex.Add(bt.Name)
                        End While

                        rd.Close()

                        For Each s As String In needsSpotIndex
                            updateSpotIndex(channel.BookingTypes(s))
                        Next
                    End Using
                End Using
                _conn.Close()
            End Using
            Return True
        End Function

        Public Overrides Function savePricelist(ByVal pricelist As cPricelist) As Boolean

            If pricelist.Targets.Count = 0 Then
                Return True
            End If

            Using _conn As New SqlClient.SqlConnection(_commonConnectionString)
                _conn.Open()
                Using transaction As SqlClient.SqlTransaction = _conn.BeginTransaction()
                    Try
                        Using cmd As New SqlClient.SqlCommand()
                            cmd.Connection = _conn
                            cmd.Transaction = transaction
                            Dim target As cPricelistTarget
                            Dim period As cPricelistPeriod

                            'Clear Targets
                            cmd.CommandText = "DELETE FROM PriceListTargets where BTID='" & pricelist.Targets(1).Bookingtype.databaseID & "'"
                            cmd.ExecuteNonQuery()

                            For Each target In pricelist.Targets
                                'If it is a new target
                                If target.databaseID = "NOT SAVED" Then
                                    target.databaseID = System.Guid.NewGuid().ToString()
                                End If

                                Dim sb As New StringBuilder()
                                sb.Append("INSERT INTO PriceListTargets VALUES('")
                                sb.Append(target.databaseID)
                                sb.Append("','")
                                sb.Append(pricelist.Targets(1).Bookingtype.databaseID)
                                sb.Append("','")
                                sb.Append(target.TargetName)
                                sb.Append("','")
                                sb.Append(target.Target.TargetName)
                                sb.Append("',")
                                sb.Append(target.Target.TargetType)
                                sb.Append(",'")
                                sb.Append((CInt(target.StandardTarget) * -1))
                                sb.Append("',")
                                sb.Append((CInt(target.CalcCPP) * -1))

                                'SaveTarget
                                If target.PricelistPeriods.Count = 0 Then
                                    sb.Append(",0,0,")
                                    sb.Append(target.MaxRatings)
                                Else
                                    sb.Append(",")
                                    sb.Append(target.PricelistPeriods(1).TargetNat)
                                    sb.Append(",")
                                    sb.Append(target.PricelistPeriods(1).TargetUni)
                                    sb.Append(",")
                                    sb.Append(target.MaxRatings)
                                End If
                                sb.Append(")")

                                cmd.CommandText = sb.ToString
                                cmd.ExecuteNonQuery()

                                'Clear Pricelistrows
                                cmd.CommandText = "DELETE FROM priceListItems WHERE TargetID='" & target.databaseID & "'"
                                cmd.ExecuteNonQuery()

                                If target.CalcCPP Then
                                    For Each period In target.PricelistPeriods
                                        Dim sbPeriod As New StringBuilder()
                                        sbPeriod.Append("INSERT INTO priceListItems VALUES('")
                                        sbPeriod.Append(target.databaseID)
                                        sbPeriod.Append("','")
                                        sbPeriod.Append(period.Name)
                                        sbPeriod.Append("','")
                                        sbPeriod.Append(period.FromDate)
                                        sbPeriod.Append("','")
                                        sbPeriod.Append(period.ToDate)
                                        sbPeriod.Append("',")
                                        sbPeriod.Append((CInt(period.PriceIsCPP) * -1))
                                        sbPeriod.Append(",")
                                        sbPeriod.Append(period.Price(period.PriceIsCPP, 0).ToString().Replace(",", "."))
                                        sbPeriod.Append(",")
                                        sbPeriod.Append(period.Price(period.PriceIsCPP, 1).ToString().Replace(",", "."))
                                        sbPeriod.Append(",")
                                        sbPeriod.Append(period.Price(period.PriceIsCPP, 2).ToString().Replace(",", "."))
                                        sbPeriod.Append(",")
                                        sbPeriod.Append(period.Price(period.PriceIsCPP, 3).ToString().Replace(",", "."))
                                        sbPeriod.Append(")")

                                        cmd.CommandText = sbPeriod.ToString.Replace("NaN", "null") 'In case some dayparts dont exits
                                        cmd.ExecuteNonQuery()
                                    Next
                                Else
                                    For Each period In target.PricelistPeriods

                                        Dim sbPeriod As New StringBuilder()
                                        sbPeriod.Append("INSERT INTO priceListItems VALUES('")
                                        sbPeriod.Append(target.databaseID)
                                        sbPeriod.Append("','")
                                        sbPeriod.Append(period.Name)
                                        sbPeriod.Append("','")
                                        sbPeriod.Append(period.FromDate)
                                        sbPeriod.Append("','")
                                        sbPeriod.Append(period.ToDate)
                                        sbPeriod.Append("',")
                                        sbPeriod.Append((CInt(period.PriceIsCPP) * -1))
                                        sbPeriod.Append(",")
                                        sbPeriod.Append(period.Price(period.PriceIsCPP).ToString().Replace(",", "."))
                                        sbPeriod.Append(",0,0,0)")

                                        cmd.CommandText = sbPeriod.ToString.Replace("NaN", "null") 'In case some dayparts dont exits
                                        cmd.ExecuteNonQuery()
                                    Next
                                End If

                                'Clear indexes
                                cmd.CommandText = "DELETE FROM priceListIndexes WHERE TargetID='" & target.databaseID & "'"
                                cmd.ExecuteNonQuery()
                                For Each index As cIndex In target.Indexes
                                    Dim sbIndex As New StringBuilder()
                                    sbIndex.Append("INSERT INTO priceListIndexes VALUES('")
                                    sbIndex.Append(index.ID)
                                    sbIndex.Append("','")
                                    sbIndex.Append(target.databaseID)
                                    sbIndex.Append("','")
                                    sbIndex.Append(index.Name)
                                    sbIndex.Append("',")
                                    sbIndex.Append(index.IndexOn)
                                    sbIndex.Append(",")
                                    sbIndex.Append((index.SystemGenerated * -1))
                                    sbIndex.Append(",'")
                                    sbIndex.Append(index.FromDate)
                                    sbIndex.Append("','")
                                    sbIndex.Append(index.ToDate)
                                    sbIndex.Append("',")
                                    sbIndex.Append(index.Index().ToString().Replace(",", "."))
                                    sbIndex.Append(")")
                                    sbIndex.Append("")
                                    sbIndex.Append("")
                                    sbIndex.Append("")

                                    cmd.CommandText = sbIndex.ToString
                                    cmd.ExecuteNonQuery()
                                Next
                            Next
                        End Using
                        transaction.Commit()
                        _conn.Close()
                        Return True
                    Catch ep As Exception

                        transaction.Rollback()
                        _conn.Close()
                        Return False
                    End Try
                End Using
                _conn.Close()
            End Using
        End Function

        Public Overrides Function readChannels(ByRef OnCampaign As cKampanj) As Boolean
            'Read the pricelist
            Using _conn As New SqlClient.SqlConnection(_commonConnectionString)
                _conn.Open()
                Try
                    Using com As New SqlClient.SqlCommand("SELECT * FROM Channels ORDER BY SortNumber", _conn)
                        Dim rd As SqlClient.SqlDataReader = com.ExecuteReader()

                        'Get all channels
                        Dim channel As cChannel
                        While rd.Read()
                            Try
                                channel = Campaign.Channels.Add(rd("ChannelName"), rd("ChannelSet"), rd("Area"), rd("ChannelID"))
                                channel.ChannelName = rd("ChannelName").ToString.Trim
                                channel.Shortname = rd("ChannelShortname").ToString.Trim
                                channel.MarathonName = rd("Marathonname")
                                channel.AdEdgeNames = rd("AdvantEdgename")
                                If Not IsDBNull(rd("AdTooxname")) Then
                                    channel.AdTooxChannelID = rd("AdTooxname")
                                Else
                                    channel.AdTooxChannelID = 0
                                End If
                                channel.MatrixName = rd("Matrixname")
                                channel.BuyingUniverse = rd("Buyinguniverse")
                                channel.AgencyCommission = rd("Agencycommission")
                                channel.ListNumber = rd("SortNumber")
                                channel.ConnectedChannel = rd("Connectedchannel")
                                channel.LogoPath = rd("Logopath")
                                channel.DeliveryAddress = rd("Address")
                                channel.VHSAddress = rd("VHSAddress")
                            Catch ex As Exception
                                Throw New Exception(ex.Message)
                            End Try
                        End While

                        rd.Close()

                        Return True
                    End Using
                Catch

                End Try
                Return False
            End Using
        End Function

        Public Overrides Function readBookingTypes(ByRef channel As cChannel) As Boolean

            Using _conn As New SqlClient.SqlConnection(_commonConnectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand()
                    Try
                        com.Connection = _conn
                        com.CommandText = "SELECT * FROM BookingTypes, Companies WHERE ChannelID='" & channel.databaseID & "' AND (companies.companyname = '" & TrinitySettings.UserCompany & "' or companies.companyname='All')"
                        Dim rd As SqlClient.SqlDataReader = com.ExecuteReader()

                        Dim BT As cBookingType
                        While rd.Read()
                            BT = channel.BookingTypes.Add(rd("BTName").ToString.Trim())
                            BT.databaseID = rd("BTID")
                            BT.Shortname = rd("BTShortName")
                            BT.IsRBS = (rd("IsRBS") = 1)
                            BT.IsSpecific = (rd("IsSpecific") = 1)
                            BT.IsPremium = (rd("IsPremium") = 1)
                            BT.IsCompensation = (rd("IsCompensation") = 1)
                            BT.IsSponsorship = (rd("IsSponsorship") = 1)
                            BT.PrintBookingCode = (rd("PrintBookingcode") = 1)
                            BT.PrintDayparts = (rd("PrintDayparts") = 1)
                            BT.writeProtected = (rd("IsWriteProtected") = 1) 'common bookingtypes are write protected
                            BT.Dayparts = TrinitySettings.DefaultDayparts(BT)

                            For index As Integer = 0 To 3
                                If Not IsDBNull(rd("DefaultDP" & index)) Then
                                    BT.DefaultDaypart(index) = rd("DefaultDP" & index)
                                End If
                            Next

                            'BT.Dayparts = New cDayparts(BT)
                        End While

                        rd.Close()
                        _conn.Close()
                        Return True
                    Catch ex As SqlClient.SqlException
                        _conn.Close()
                        Return False
                    End Try
                End Using
            End Using
        End Function

        Public Overrides Function getAllChannelSets() As System.Data.DataTable
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand()
                    com.Connection = _conn
                    com.CommandText = "SELECT * FROM channelsets"
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader()

                        Using dt As New DataTable
                            dt.Load(rd)
                            _conn.Close()
                            Return dt
                        End Using
                    End Using
                End Using
            End Using
        End Function


        Public Enum ProblemsEnum
            NoSQLServerConnection = 1
        End Enum

        Public Shadows Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem))

        Public Overrides Function DetectProblems() As System.Collections.Generic.List(Of cProblem)

            Dim _problems As New List(Of cProblem)

            Using tmpConn As SqlClient.SqlConnection = New SqlClient.SqlConnection()
                tmpConn.ConnectionString = _connectionString

                Try
                    tmpConn.Open()
                    tmpConn.Close()
                Catch myerror As SqlClient.SqlException
                    Dim _helpText As New System.Text.StringBuilder

                    _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Can't connect to the database server</p>")
                    _helpText.AppendLine("<p>The connection to " & TrinitySettings.DataBase(cSettings.SettingsLocationEnum.locNetwork) & " can't be established. " & _
                                         "Most likely, there is a problem with the network or the database server at the moment</p>")
                    _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                    _helpText.AppendLine("<p>If this issue does not disappear from the list, restart Trinity. If the issue persists, contact the IT department.</p>")
                    Dim _problem As New cProblem(ProblemsEnum.NoSQLServerConnection, cProblem.ProblemSeverityEnum.Warning, "Can't connect to the database server", "Database/Network", _helpText.ToString, Me)

                    _problems.Add(_problem)
                Finally
                    tmpConn.Dispose()
                End Try
            End Using
            If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
            Return _problems
        End Function

        Protected Overrides Sub Finalize()
            StopMonitoringLock()
            StopListeningForMessages()
            MyBase.Finalize()
        End Sub

        Public Overrides Function alive() As Boolean
            If _connectionString <> "" Then
                Return True
            End If
        End Function

        Public Overrides Function getEventsInInterval([Date] As Date, Channel As String, FromMaM As Integer, ToMaM As Integer) As System.Data.DataTable
            Dim events As String = "events"
            If TrinitySettings.SharedDatabaseName <> "" Then
                events = TrinitySettings.SharedDatabaseName & "." & events
            End If
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand()
                    com.Connection = _conn
                    com.CommandText = "SELECT * FROM " & events & " WHERE Date = " & [Date].ToOADate & " AND Channel = '" & Channel & "' AND StartMaM > " & FromMaM & " AND StartMaM < " & ToMaM
                    Using rd As SqlClient.SqlDataReader = com.ExecuteReader()
                        Using dt As New DataTable
                            Try
                                dt.Load(rd)
                                _conn.Close()
                                Return dt
                            Catch ex As Exception
                                _conn.Close()
                                Return New DataTable()
                            End Try
                        End Using
                    End Using
                End Using
            End Using
        End Function

        Public Overrides Sub addPeople(People As System.Collections.Generic.List(Of cPerson))
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                For Each _p As cPerson In People
                    Using com As New SqlClient.SqlCommand()
                        com.Connection = _conn
                        If _p.id > 0 Then
                            com.CommandText = "UPDATE people SET name=@name,phone=@phone,email=@email,active=@active WHERE id=@id"
                            com.Parameters.AddWithValue("id", _p.id)
                        Else
                            com.CommandText = "INSERT INTO people (name,phone,email,active) VALUES (@name,@phone,@email,@active)"
                        End If
                        com.Parameters.AddWithValue("name", _p.Name)
                        com.Parameters.AddWithValue("phone", _p.Phone)
                        com.Parameters.AddWithValue("email", _p.Email)
                        com.Parameters.AddWithValue("active", _p.statusActive)
                        com.ExecuteNonQuery()
                    End Using
                Next
                _conn.Close()
            End Using
        End Sub

        Public Overrides Sub removePerson(id As Long)
            Using _conn As New SqlClient.SqlConnection(_connectionString)
                _conn.Open()
                Using com As New SqlClient.SqlCommand("DELETE FROM people WHERE id=@id", _conn)
                    com.Parameters.AddWithValue("id", id)
                    com.ExecuteNonQuery()
                End Using
                _conn.Close()
            End Using
        End Sub

        Private Sub ListenForMessages()
            If TrinitySettings.MessagingServer <> "" Then
                Try
                    SqlClient.SqlDependency.Start(TrinitySettings.MessagingServer & "Integrated Security=True;" & ";")
                    Using _conn As New SqlClient.SqlConnection((TrinitySettings.MessagingServer & "Integrated Security=True;" & ";"))
                        _conn.Open()
                        Using _dataSet As New DataSet
                            _dataSet.Clear()
                            ' Make sure the command object does not already have a notification object associated with it.
                            Using _command = New SqlClient.SqlCommand("Select headline, ingress, body FROM dbo.messages WHERE id>" & TrinitySettings.LastMessageReceived, _conn)
                                _command.Notification = Nothing
                                ' Create and bind the SqlDependency object to the command object.
                                Dim _dependency As New SqlClient.SqlDependency(_command)
                                AddHandler _dependency.OnChange, AddressOf MessageReceived

                                Using _adapter As New SqlClient.SqlDataAdapter(_command)
                                    _adapter.Fill(_dataSet, "messages")
                                End Using
                            End Using
                        End Using
                        _conn.Close()
                    End Using
                Catch ex As Exception
                    Trinity.Helper.WriteToLogFile("ERROR: Could not connect to messaging server: " & ex.Message)
                    Trinity.Helper.WriteToLogFile("Connection string: " & TrinitySettings.MessagingServer)
                End Try
            End If
        End Sub

        Sub StopListeningForMessages()
            SqlClient.SqlDependency.Stop(TrinitySettings.MessagingServer & "Integrated Security=True;" & ";")
        End Sub

        Private Sub MessageReceived(sender As Object, e As SqlClient.SqlNotificationEventArgs)
            Using _conn As New SqlClient.SqlConnection(TrinitySettings.MessagingServer & "Integrated Security=True;" & ";")
                _conn.Open()
                Using _command As SqlClient.SqlCommand = New SqlClient.SqlCommand("SELECT id,headline,ingress,body FROM dbo.messages WHERE id>" & TrinitySettings.LastMessageReceived, _conn)
                    Using _rd As SqlClient.SqlDataReader = _command.ExecuteReader
                        Dim _lastId As Integer = TrinitySettings.LastMessageReceived
                        While _rd.Read
                            RaiseEvent IncomingMessage(sender, New MessageEventArgs With {.Body = _rd!body, .Headline = _rd!headline, .Ingress = _rd!ingress})
                            _lastId = _rd!id
                        End While
                        TrinitySettings.LastMessageReceived = _lastId
                        _rd.Close()
                    End Using
                End Using
                _conn.Close()
            End Using
            ListenForMessages()
        End Sub

        Public Sub CheckForNewMessages()
            If TrinitySettings.LastMessageReceived < 1 Then
                If TrinitySettings.MessagingServer <> "" Then
                    Try
                        Using _conn As New SqlClient.SqlConnection(TrinitySettings.MessagingServer & "Integrated Security=True;" & ";")
                            _conn.Open()
                            Try
                                Using _command As SqlClient.SqlCommand = New SqlClient.SqlCommand("SELECT MAX(id) FROM messages", _conn)
                                    TrinitySettings.LastMessageReceived = _command.ExecuteScalar
                                End Using
                            Catch

                            End Try
                            _conn.Close()
                        End Using
                    Catch

                    End Try
                End If
            Else
                MessageReceived(Me, Nothing)
            End If
        End Sub

        Public Class MessageEventArgs
            Inherits EventArgs

            Public Property Headline As String
            Public Property Ingress As String
            Public Property Body As String

        End Class
    End Class
End Namespace
