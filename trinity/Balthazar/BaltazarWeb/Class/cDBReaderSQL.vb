Public Class cDBReaderSQL
    Inherits cDBReader

    Private _activeUser As Integer = -1

#Region "General"

    'Return a connection to the database
    Public Overrides Function Connect() As Object
        Dim DBConn As New SqlClient.SqlConnection("Data Source=" & Settings.DatabaseServer & ";Initial Catalog=" & Settings.DatabaseTable & ";User ID=balthazar;Password=eventrules")
        DBConn.Open()
        Return DBConn
    End Function

    'Attempt to login user. If it succeeds, return the ID of the logged in user, otherwise return 0 for wrong password, -1 for not activated user and -2 for unknown user
    Public Overrides Function Login(ByVal User As String, ByVal Password As String) As Integer
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                _info = Nothing
                com.CommandText = "SELECT ID,VerificationCode,Password From Staff WHERE login='" & User & "'"
                Dim TmpRd As SqlClient.SqlDataReader = com.ExecuteReader
                If TmpRd.Read Then
                    If Not IsDBNull(TmpRd("Password")) AndAlso TmpRd("Password") <> Password Then
                        'Wrong password
                        Return 0
                    End If
                    'Check if there is an activation code present. If so, the user account has not been activated
                    If IsDBNull(TmpRd("VerificationCode")) OrElse TmpRd("VerificationCode") = "" Then
                        _activeUser = TmpRd("ID")
                        TmpRd.Close()
                        com.CommandText = "UPDATE Staff SET lastlogin='" & Now & "' WHERE id=" & _activeUser
                        com.ExecuteNonQuery()
                        DBConn.Close()
                        Return _activeUser
                    Else
                        'Not activated
                        Return -1
                    End If
                Else
                    'No such user
                    Return -2
                End If
            End Using
            DBConn.Close()
        End Using
    End Function

    'Get the connection state of the connection
    Public Overrides ReadOnly Property ConnectionState() As System.Data.ConnectionState
        Get
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    Return DBConn.State
                End Using
                DBConn.Close()
            End Using
        End Get
    End Property

    'Get a list of Questionaires available to the logged in user
    Public Overrides ReadOnly Property Questionaires(Optional ByVal IncludeAnswers As Boolean = False) As ICollection
        Get
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    If _activeUser = -1 Then Return Nothing
                    Dim dt As New DataTable
                    Dim rd As SqlClient.SqlDataReader

                    dt.Columns.Add(New DataColumn("ID", GetType(Integer)))
                    dt.Columns.Add(New DataColumn("Label", GetType(String)))

                    'Return different data depending on the type of user logged in
                    Select Case GetLoggedInUserInfo.Type
                        Case cUserInfo.UserTypeEnum.Staff
                            If Not IncludeAnswers Then
                                com.CommandText = "SELECT Questionaire.ID,Questionaire.Name as Label FROM Questionaire,questionairestaff WHERE Questionaire.ID=questionairestaff.QuestionaireID and questionairestaff.StaffID=" & _activeUser
                            Else
                                dt.Columns.Add(New DataColumn("AnswerID", GetType(String)))
                                dt.Columns.Add(New DataColumn("AnswerDate", GetType(Date)))
                                com.CommandText = "SELECT Questionaire.ID,Questionaire.Name as Label,QuestionaireAnswers.ID as AnswerID,QuestionaireAnswers.Date as AnswerDate FROM Questionaire,questionairestaff,QuestionaireAnswers WHERE Questionaire.ID=questionairestaff.QuestionaireID and QuestionaireAnswers.QuestionaireID=Questionaire.ID and QuestionaireAnswers.StaffID=questionairestaff.staffID and questionairestaff.StaffID=" & _activeUser
                            End If
                        Case cUserInfo.UserTypeEnum.Provider
                            com.CommandText = "SELECT DISTINCT Questionaire.ID,Questionaire.Name as Label,QuestionaireAnswers.ID as AnswerID,QuestionaireAnswers.Date as AnswerDate,QuestionaireAnswers.Location,QuestionaireAnswers.City,QuestionaireAnswers.Answered FROM Questionaire,Bookings,QuestionaireAnswers WHERE questionaire.id=questionaireanswers.questionaireid AND Questionaire.EventID=Bookings.EventID AND confirmedbyprovider=1 AND questionaireanswers.staffid=" & _activeUser & " ORDER BY date"
                        Case cUserInfo.UserTypeEnum.Salesman
                            com.CommandText = "SELECT DISTINCT Questionaire.ID,Questionaire.Name as Label,QuestionaireAnswers.ID as AnswerID,QuestionaireAnswers.Date as AnswerDate,QuestionaireAnswers.Location,QuestionaireAnswers.City FROM Questionaire,Bookings,QuestionaireAnswers WHERE questionaire.id=questionaireanswers.questionaireid AND Questionaire.EventID=Bookings.EventID AND confirmedbyprovider=1 AND QuestionaireAnswers.Answered=1 AND bookings.staffid=" & _activeUser & " ORDER BY date"
                        Case cUserInfo.UserTypeEnum.HeadOfSales
                            If IncludeAnswers Then
                                com.CommandText = "SELECT DISTINCT Questionaire.ID,Questionaire.Name as Label,QuestionaireAnswers.ID as AnswerID,QuestionaireAnswers.Date as AnswerDate,QuestionaireAnswers.Location,QuestionaireAnswers.City FROM Questionaire,Bookings,QuestionaireAnswers,staff WHERE questionaire.id=questionaireanswers.questionaireid AND Questionaire.EventID=Bookings.EventID AND confirmedbyprovider=1 AND QuestionaireAnswers.Answered=1 AND bookings.staffid=staff.id and staff.clientid=" & GetLoggedInUserInfo.clientID & " ORDER BY date"
                            Else
                                com.CommandText = "SELECT DISTINCT Questionaire.ID,Questionaire.Name as Label FROM Questionaire,events WHERE Questionaire.eventid=events.id and events.clientid=" & GetLoggedInUserInfo.clientID
                            End If
                    End Select
                    rd = com.ExecuteReader
                    dt.Load(rd)

                    Dim dv As New DataView(dt)
                    Return dv
                End Using
                DBConn.Close()
            End Using
        End Get
    End Property

    'Get a single Questionaire by its ID
    'Public Overrides ReadOnly Property GetQuestionaire(ByVal ID As Integer) As System.Data.DataRow
    '    Get
    '        Using DBConn As SqlClient.SqlConnection = Connect()
    '            Using com As New SqlClient.SqlCommand("", DBConn)
    '                Dim rd As SqlClient.SqlDataReader
    '                Dim dt As New DataTable
    '                'Return different data depening on the logged in user
    '                Select Case GetLoggedInUserInfo.Type
    '                    Case cUserInfo.UserTypeEnum.Staff
    '                        com.CommandText = "SELECT Questionaire.*,Clients.Name as [Client],products.name as [Product] FROM Questionaire,Products,Clients,events,questionairestaff WHERE clients.id=products.clientid and products.id=events.productid and events.id=Questionaire.eventid and questionairestaff.questionaireid=questionaire.id and questionairestaff.staffid=" & _activeUser & " and Questionaire.id=" & ID
    '                    Case cUserInfo.UserTypeEnum.Provider
    '                        com.CommandText = "SELECT DISTINCT Questionaire.*,Clients.Name as [Client],products.name as [Product],bookings.chosenprovidername FROM Questionaire,Products,Clients,events,bookings WHERE bookings.eventid=questionaire.eventid AND clients.id=products.clientid and products.id=events.productid and events.id=Questionaire.eventid and bookings.confirmedbyprovider=1 and bookings.chosenprovider=" & _activeUser & " and Questionaire.id=" & ID
    '                    Case cUserInfo.UserTypeEnum.Salesman
    '                        com.CommandText = "SELECT DISTINCT Questionaire.*,Clients.Name as [Client],products.name as [Product],bookings.chosenprovidername FROM Questionaire,Products,Clients,events,bookings WHERE bookings.eventid=questionaire.eventid AND clients.id=products.clientid and products.id=events.productid and events.id=Questionaire.eventid and bookings.confirmedbyprovider=1 and bookings.staffid=" & _activeUser & " and Questionaire.id=" & ID
    '                    Case cUserInfo.UserTypeEnum.HeadOfSales
    '                        com.CommandText = "SELECT DISTINCT Questionaire.*,Clients.Name as [Client],products.name as [Product],bookings.chosenprovidername FROM Questionaire,Products,Clients,events,bookings,staff WHERE bookings.eventid=questionaire.eventid AND clients.id=products.clientid and products.id=events.productid and events.id=Questionaire.eventid and bookings.confirmedbyprovider=1 and bookings.staffid=staff.id and staff.productid=" & GetLoggedInUserInfo.ProductID & " and Questionaire.id=" & ID
    '                End Select
    '                rd = com.ExecuteReader
    '                dt.Load(rd)
    '                If dt.Rows.Count = 0 Then Return Nothing
    '                Return dt.Rows(0)
    '            End Using
    '            DBConn.Close()
    '        End Using
    '    End Get
    'End Property

    Public Overrides ReadOnly Property GetQuestionaire(ByVal ID As Integer) As System.Data.DataRow
        Get
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    Dim rd As SqlClient.SqlDataReader
                    Dim dt As New DataTable
                    'Return different data depening on the logged in user
                    Select Case GetLoggedInUserInfo.Type
                        Case cUserInfo.UserTypeEnum.Staff
                            com.CommandText = "SELECT Questionaire.*,Clients.Name as [Client],products.name as [Product] FROM Questionaire,Products,Clients,events,questionairestaff WHERE clients.id=products.clientid and products.id=events.productid and events.id=Questionaire.eventid and questionairestaff.questionaireid=questionaire.id and questionairestaff.staffid=" & _activeUser & " and Questionaire.id=" & ID
                        Case cUserInfo.UserTypeEnum.Provider
                            com.CommandText = "SELECT Questionaire.*, other.* FROM (SELECT DISTINCT Questionaire.id as qid,Clients.Name as [Client],products.name as [Product],bookings.chosenprovidername FROM Questionaire,Products,Clients,events,bookings WHERE bookings.eventid=questionaire.eventid AND clients.id=products.clientid and products.id=events.productid and events.id=Questionaire.eventid and bookings.confirmedbyprovider=1 and bookings.chosenprovider=" & _activeUser & " and Questionaire.id=" & ID & ") as other,Questionaire WHERE Questionaire.id=other.qid"
                        Case cUserInfo.UserTypeEnum.Salesman
                            com.CommandText = "SELECT Questionaire.*, other.* FROM (SELECT DISTINCT Questionaire.id as qid,Clients.Name as [Client],products.name as [Product],bookings.chosenprovidername FROM Questionaire,Products,Clients,events,bookings WHERE bookings.eventid=questionaire.eventid AND clients.id=products.clientid and products.id=events.productid and events.id=Questionaire.eventid and bookings.confirmedbyprovider=1 and bookings.staffid=" & _activeUser & " and Questionaire.id=" & ID & ") as other,Questionaire WHERE Questionaire.id=other.qid"
                        Case cUserInfo.UserTypeEnum.HeadOfSales
                            com.CommandText = "SELECT Questionaire.*, other.* FROM (SELECT DISTINCT Questionaire.id as qid,Clients.Name as [Client],products.name as [Product],bookings.chosenprovidername FROM Questionaire,Products,Clients,events,bookings,staff WHERE bookings.eventid=questionaire.eventid AND clients.id=products.clientid and products.id=events.productid and events.id=Questionaire.eventid and bookings.confirmedbyprovider=1 and bookings.staffid=staff.id and staff.clientid=" & GetLoggedInUserInfo.clientID & " and Questionaire.id=" & ID & ") as other,Questionaire WHERE Questionaire.id=other.qid"
                    End Select
                    rd = com.ExecuteReader
                    dt.Load(rd)
                    If dt.Rows.Count = 0 Then Return Nothing
                    Return dt.Rows(0)
                End Using
                DBConn.Close()
            End Using
        End Get
    End Property

    'Get a list of question to be answered with a rating, for a certain Questionaire (QID=Questionaire ID)
    Public Overrides ReadOnly Property GetRatingQuestions(ByVal QID As Integer) As ICollection
        Get
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    If _activeUser = -1 Then Return Nothing
                    Dim dt As New DataTable
                    Dim rd As SqlClient.SqlDataReader

                    com.CommandText = "SELECT * FROM QuestionaireRating WHERE QuestionaireID=" & QID

                    rd = com.ExecuteReader
                    dt.Load(rd)

                    Dim dv As New DataView(dt)
                    Return dv
                End Using
                DBConn.Close()
            End Using
        End Get
    End Property

    'Get a list of questions to be answered with free text, for a certain Questionaire (QID=Questionaire ID)
    Public Overrides ReadOnly Property GetCommentQuestions(ByVal QID As Integer) As System.Collections.ICollection
        Get
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    If _activeUser = -1 Then Return Nothing
                    Dim dt As New DataTable
                    Dim rd As SqlClient.SqlDataReader

                    com.CommandText = "SELECT * FROM QuestionaireComment WHERE QuestionaireID=" & QID

                    rd = com.ExecuteReader
                    dt.Load(rd)

                    Dim dv As New DataView(dt)
                    Return dv
                End Using
                DBConn.Close()
            End Using
        End Get
    End Property

    Public Overloads Overrides Function SaveQuestionAnswer(ByVal QID As Integer, ByVal Answered As Boolean, ByVal AtDate As Date, ByVal AnswerXML As String, ByVal Locations As String, ByVal City As String, Optional ByVal AnswerID As Integer = -1, Optional ByVal BookingID As Integer = -1) As Integer
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                If AnswerID = -1 Then
                    com.CommandText = "INSERT INTO QuestionaireAnswers(QuestionaireID,BookingID,Answered,[Date],Location,City,[XML]) VALUES (@qid,@bid,@answered,@date,@loc,@city,@xml);SELECT @@identity"
                    com.Parameters.AddWithValue("@qid", QID)
                    com.Parameters.AddWithValue("@answered", Answered)
                    com.Parameters.AddWithValue("@xml", AnswerXML)
                    com.Parameters.AddWithValue("@date", QID)
                    com.Parameters.AddWithValue("@loc", Locations)
                    com.Parameters.AddWithValue("@city", City)
                    com.Parameters.AddWithValue("@bid", BookingID)
                    Return com.ExecuteScalar
                Else
                    com.CommandText = "UPDATE QuestionaireAnswers SET QuestionaireID=@qid,Answered=@answered,[XML]=@xml,[Date]=@date,Location=@loc,City=@city WHERE id=@aid"
                    com.Parameters.AddWithValue("@aid", AnswerID)
                    com.Parameters.AddWithValue("@qid", QID)
                    com.Parameters.AddWithValue("@answered", Answered)
                    com.Parameters.AddWithValue("@xml", AnswerXML)
                    com.Parameters.AddWithValue("@date", QID)
                    com.Parameters.AddWithValue("@loc", Locations)
                    com.Parameters.AddWithValue("@city", City)
                    Return com.ExecuteScalar
                End If
            End Using
        End Using
    End Function

    ''Write the answers for a questionaire
    'Public Overrides Function SaveQuestionAnswer(ByVal QID As Integer, ByVal Answered As Boolean, ByVal AnsweredBy As String, ByVal AnseredByPhoneNr As String, ByVal AtDate As Date, ByVal StartTime As Integer, ByVal EndTime As Integer, ByVal Locations As String, ByVal City As String, ByVal Quantity As Integer, ByVal InTarget As Integer, ByVal Target As Integer, ByVal Positive As Integer, ByVal Comments As String, Optional ByVal AnswerID As Integer = -1, Optional ByVal BookingID As Integer = -1) As Integer
    '    Dim SQL As String
    '    Using DBConn As SqlClient.SqlConnection = Connect()
    '        Using com As New SqlClient.SqlCommand("", DBConn)
    '            If AnswerID = -1 Then
    '                'This Questionaire has not been answered before and a new db entry should be created
    '                SQL = "INSERT INTO QuestionaireAnswers (QuestionaireID,StaffID,BookingID,Answered,[Date],StartTime,EndTime,Location,City,Quantity,InTarget,Target,Positive,Comments,NameOfPerson,CellPhoneOfPerson) VALUES (%qid,%sid,%bid,'%ans','%date',%st,%et,'%loc','%city',%qty,%it,%trg,%pos,'%com','%nop','%cop')"
    '                SQL = SQL.Replace("%qid", QID)
    '                SQL = SQL.Replace("%sid", _activeUser)
    '                SQL = SQL.Replace("%bid", BookingID)
    '                SQL = SQL.Replace("%ans", Answered)
    '                SQL = SQL.Replace("%date", AtDate)
    '                SQL = SQL.Replace("%st", StartTime)
    '                SQL = SQL.Replace("%et", EndTime)
    '                SQL = SQL.Replace("%loc", Locations)
    '                SQL = SQL.Replace("%city", City)
    '                SQL = SQL.Replace("%qty", Quantity)
    '                SQL = SQL.Replace("%it", InTarget)
    '                SQL = SQL.Replace("%trg", Target)
    '                SQL = SQL.Replace("%pos", Positive)
    '                SQL = SQL.Replace("%com", Comments)
    '                SQL = SQL.Replace("%nop", AnsweredBy)
    '                SQL = SQL.Replace("%cop", AnseredByPhoneNr)

    '                com.CommandText = SQL
    '                com.ExecuteNonQuery()
    '                com.CommandText = "SELECT @@identity"
    '                AnswerID = com.ExecuteScalar
    '            Else
    '                'This Questionaire has been answered before and the answers needs to be updated
    '                SQL = "UPDATE QuestionaireAnswers SET QuestionaireID=%qid,StaffID=%sid,Answered=%ans,[Date]='%date',StartTime=%st,EndTime=%et,Location='%loc',City='%city',Quantity=%qty,InTarget=%it,Target=%trg,Positive=%pos,Comments='%com',NameOfPerson='%nop',CellPhoneOfPerson='%cop' WHERE id=" & AnswerID
    '                SQL = SQL.Replace("%qid", QID)
    '                SQL = SQL.Replace("%sid", _activeUser)
    '                SQL = SQL.Replace("%ans", CInt(Answered))
    '                SQL = SQL.Replace("%date", Format(AtDate, "Short date"))
    '                SQL = SQL.Replace("%st", StartTime)
    '                SQL = SQL.Replace("%et", EndTime)
    '                SQL = SQL.Replace("%loc", Locations)
    '                SQL = SQL.Replace("%city", City)
    '                SQL = SQL.Replace("%qty", Quantity)
    '                SQL = SQL.Replace("%it", InTarget)
    '                SQL = SQL.Replace("%trg", Target)
    '                SQL = SQL.Replace("%pos", Positive)
    '                SQL = SQL.Replace("%com", Comments)
    '                SQL = SQL.Replace("%nop", AnsweredBy)
    '                SQL = SQL.Replace("%cop", AnseredByPhoneNr)
    '                com.CommandText = SQL
    '                com.ExecuteNonQuery()
    '            End If
    '        End Using
    '        Using com As New SqlClient.SqlCommand("SELECT bookingid FROM QuestionaireAnswers WHERE id=" & AnswerID, DBConn)
    '            Dim rd As SqlClient.SqlDataReader
    '            rd = com.ExecuteReader
    '            While rd.Read
    '                TouchBooking(rd!bookingid)
    '            End While
    '            rd.Close()
    '        End Using
    '        DBConn.Close()
    '        Return AnswerID
    '    End Using
    'End Function

    'Save answers for one rating question
    Public Overrides Sub SaveRatingQuestionAnswer(ByVal AnswerID As Integer, ByVal QuestionaireRatingID As Integer, ByVal Value As Integer, Optional ByVal RatingQuestionAnswerID As Integer = -1)
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                If RatingQuestionAnswerID = -1 Then
                    'This Question has not been answered before and a new db entry should be created
                    Dim SQL As String = "INSERT INTO QuestionaireRatingAnswers (AnswerID,QuestionaireRatingID,StaffID,Answer) VALUES (%aid,%qrid,%sid,%val)"
                    SQL = SQL.Replace("%aid", AnswerID)
                    SQL = SQL.Replace("%sid", _activeUser)
                    SQL = SQL.Replace("%qrid", QuestionaireRatingID)
                    SQL = SQL.Replace("%val", Value)
                    com.CommandText = SQL
                    com.ExecuteNonQuery()
                Else
                    'This Question has been answered before and the answer needs to be updated
                    Dim SQL As String = "UPDATE QuestionaireRatingAnswers SET Answer=%val WHERE id=%id"
                    SQL = SQL.Replace("%id", RatingQuestionAnswerID)
                    SQL = SQL.Replace("%val", Value)
                    com.CommandText = SQL
                    com.ExecuteNonQuery()
                End If
                com.CommandText = "SELECT bookingid FROM QuestionaireAnswers WHERE id=" & AnswerID
                Dim rd As SqlClient.SqlDataReader
                rd = com.ExecuteReader
                While rd.Read
                    TouchBooking(rd!bookingid)
                End While
                rd.Close()
            End Using
            DBConn.Close()
        End Using
    End Sub

    Private Sub TouchBooking(ByVal BookingID As Integer)
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("UPDATE bookings SET timesaved=@timesaved WHERE id=" & BookingID, DBConn)
                com.Parameters.AddWithValue("@timesaved", Now)
                com.ExecuteNonQuery()
            End Using
            DBConn.Close()
        End Using
    End Sub

    Private Sub TouchBookingsForEvent(ByVal EventID As Integer)
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("UPDATE bookings SET timesaved=@timesaved WHERE eventid=" & EventID, DBConn)
                com.Parameters.AddWithValue("@timesaved", Now)
                com.ExecuteNonQuery()
            End Using
            DBConn.Close()
        End Using
    End Sub

    'Save answers for one question with free text answers
    Public Overrides Sub SaveCommentQuestionAnswer(ByVal AnswerID As Integer, ByVal QuestionaireRatingID As Integer, ByVal Answer() As String, Optional ByVal RatingQuestionAnswerID As Integer = -1)
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                If RatingQuestionAnswerID = -1 Then
                    'This Question has not been answered before and a new db entry should be created
                    Dim SQL As String = "INSERT INTO QuestionaireCommentAnswers (AnswerID,QuestionaireCommentID,StaffID,Text1,Text2,Text3,Text4,Text5,Text6,Text7,Text8,Text9,Text10) VALUES (%aid,%qrid,%sid"
                    SQL = SQL.Replace("%aid", AnswerID)
                    SQL = SQL.Replace("%sid", _activeUser)
                    SQL = SQL.Replace("%qrid", QuestionaireRatingID)
                    For i As Integer = 0 To 9
                        SQL &= ",'" & Answer(i) & "'"
                    Next
                    SQL &= ")"
                    com.CommandText = SQL
                    com.ExecuteNonQuery()
                Else
                    'This Question has been answered before and the answer needs to be updated
                    Dim SQL As String = "UPDATE QuestionaireCommentAnswers SET "
                    For i As Integer = 0 To 9
                        SQL &= "Text" & i + 1 & "='" & Answer(i) & "',"
                    Next
                    SQL = SQL.Substring(0, SQL.Length - 1)
                    SQL &= " WHERE ID=" & RatingQuestionAnswerID
                    com.CommandText = SQL
                    com.ExecuteNonQuery()
                End If
                com.CommandText = "SELECT bookingid FROM QuestionaireAnswers WHERE id=" & AnswerID
                Dim rd As SqlClient.SqlDataReader
                rd = com.ExecuteReader
                While rd.Read
                    TouchBooking(rd!bookingid)
                End While
                rd.Close()
            End Using
            DBConn.Close()
        End Using
    End Sub

    'Get the answers for a question with free text answers
    Public Overrides ReadOnly Property GetCommentQuestionAnswers(ByVal AnswerID As Integer) As System.Data.DataTable
        Get
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    Dim rd As SqlClient.SqlDataReader
                    Dim dt As New DataTable
                    com.CommandText = "SELECT * FROM QuestionaireCommentAnswers WHERE AnswerID=" & AnswerID
                    rd = com.ExecuteReader
                    dt.Load(rd)
                    Return dt
                End Using
                DBConn.Close()
            End Using
        End Get
    End Property

    'Get a single answer for a questionaire by its ID
    Public Overrides ReadOnly Property GetQuestionaireAnswer(ByVal AnswerID As Integer) As System.Data.DataRow
        Get
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    Dim rd As SqlClient.SqlDataReader
                    Dim dt As New DataTable
                    com.CommandText = "SELECT QuestionaireAnswers.*,bookings.*,(staff.Firstname+' '+staff.lastname) as [salesman] FROM QuestionaireAnswers,bookings,staff WHERE bookings.id=QuestionaireAnswers.bookingid and staff.id=bookings.staffid and QuestionaireAnswers.id=" & AnswerID
                    rd = com.ExecuteReader
                    dt.Load(rd)
                    If dt.Rows.Count = 0 Then Return Nothing
                    Return dt.Rows(0)
                End Using
                DBConn.Close()
            End Using
        End Get
    End Property

    'Get the answers for all rating questions by the answer id
    Public Overrides ReadOnly Property GetRatingQuestionAnswers(ByVal AnswerID As Integer) As System.Data.DataTable
        Get
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    Dim rd As SqlClient.SqlDataReader
                    Dim dt As New DataTable
                    com.CommandText = "SELECT * FROM QuestionaireRatingAnswers WHERE AnswerID=" & AnswerID
                    rd = com.ExecuteReader
                    dt.Load(rd)
                    Return dt
                End Using
                DBConn.Close()
            End Using
        End Get
    End Property

    'Remove the answers for a questionaire from the db
    Public Overrides Sub DeleteQuestionAnswer(ByVal AnswerID As Integer)
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "DELETE FROM QuestionaireAnswers WHERE id=" & AnswerID
                com.ExecuteNonQuery()

                com.CommandText = "DELETE FROM QuestionaireRatingAnswers WHERE AnswerID=" & AnswerID
                com.ExecuteNonQuery()

                com.CommandText = "DELETE FROM QuestionaireCommentAnswers WHERE AnswerID=" & AnswerID
                com.ExecuteNonQuery()
            End Using
            DBConn.Close()
        End Using
    End Sub

    'Get info for the logged in user
    Public Overrides ReadOnly Property GetUserInfo() As System.Data.DataRow
        Get
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    Dim rd As SqlClient.SqlDataReader
                    Dim dt As New DataTable
                    com.CommandText = "SELECT * FROM staff WHERE id=" & _activeUser
                    rd = com.ExecuteReader
                    dt.Load(rd)
                    Return dt.Rows(0)
                End Using
                DBConn.Close()
            End Using
        End Get
    End Property

    'Save info for the logged in user
    Public Overrides Sub SaveUserInfo(ByVal Firstname As String, ByVal Lastname As String, ByVal Birthday As Date, ByVal Gender As Byte, ByVal DriverB As Byte, ByVal DriverC As Byte, ByVal DriverD As Byte, ByVal DriverE As Byte, ByVal Email As String, ByVal Address1 As String, ByVal Address2 As String, ByVal ZipCode As String, ByVal ZipArea As String, ByVal HomePhone As String, ByVal WorkPhone As String, ByVal MobilePhone As String, ByVal Bank As String, ByVal ClearingNo As String, ByVal AccountNo As String, ByVal Info As String)
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                Dim SQL As String
                SQL = "UPDATE Staff SET AccountNo='%acc',Address1='%ad1',Address2='%ad2',Birthday='%bd',Bank='%bnk',ClearingNo='%cno',DriverB=%drvB,DriverC=%drvC,DriverD=%drvD,DriverE=%drvE,Email='%mail',FirstName='%fn',Gender=%gnd,HomePhone='%hp', LastName='%ln',MobilePhone='%mp',Workphone='%wp',ZipCode='%zip',ZipArea='%city',Info='%inf',TimeSaved=@timesaved WHERE id=%id"
                SQL = SQL.Replace("%acc", AccountNo)
                SQL = SQL.Replace("%ad1", Address1)
                SQL = SQL.Replace("%ad2", Address2)
                SQL = SQL.Replace("%bd", Birthday)
                SQL = SQL.Replace("%bnk", Bank)
                SQL = SQL.Replace("%cno", ClearingNo)
                SQL = SQL.Replace("%drvB", DriverB)
                SQL = SQL.Replace("%drvC", DriverC)
                SQL = SQL.Replace("%drvD", DriverD)
                SQL = SQL.Replace("%drvE", DriverE)
                SQL = SQL.Replace("%mail", Email)
                SQL = SQL.Replace("%fn", Firstname)
                SQL = SQL.Replace("%gnd", Gender)
                SQL = SQL.Replace("%hp", HomePhone)
                SQL = SQL.Replace("%ln", Lastname)
                SQL = SQL.Replace("%mp", MobilePhone)
                SQL = SQL.Replace("%wp", WorkPhone)
                SQL = SQL.Replace("%zip", ZipCode)
                SQL = SQL.Replace("%city", ZipArea)
                SQL = SQL.Replace("%inf", Info)
                SQL = SQL.Replace("%id", _activeUser)

                com.CommandText = SQL
                com.Parameters.AddWithValue("@timesaved", Now)
                com.ExecuteNonQuery()
            End Using
            DBConn.Close()
        End Using

    End Sub

    'Get user info for the logged in user
    Dim _info As cUserInfo
    Public Overrides Function GetLoggedInUserInfo() As cUserInfo
        If _info Is Nothing Then
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    Dim rd As SqlClient.SqlDataReader
                    com.CommandText = "SELECT FirstName,LastName,MobilePhone,Type,ClientID,CanCreateBookings FROM Staff WHERE id=" & _activeUser
                    rd = com.ExecuteReader
                    If rd.Read Then
                        _info = New cUserInfo() With {.ID = _activeUser, .Type = rd!Type, .FirstName = GetDBString(rd!FirstName), .LastName = GetDBString(rd!LastName), .MobilePhone = GetDBString(rd!MobilePhone), .ClientID = rd!ClientID, .CanCreateBookings = rd!CanCreateBookings}
                    Else
                        Return Nothing
                    End If
                End Using
                DBConn.Close()
            End Using
        End If
        Return _info
    End Function

    'Make sure a string is always returned, even if the db value is DBNull
    Private Function GetDBString(ByVal s As Object) As String
        If IsDBNull(s) Then Return ""
        Return s
    End Function
#End Region

#Region "Events"
    'Get a list of all events where the logged in user is supposed to work
    Public Overrides ReadOnly Property CurrentJobs() As ICollection
        Get
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    If _activeUser = -1 Then Return Nothing
                    Dim dt As New DataTable
                    Dim rd As SqlClient.SqlDataReader

                    com.CommandText = "SELECT DISTINCT events.* FROM events,eventlocations,locationroles,roleavailableforstaff WHERE roleavailableforstaff.roleid=locationroles.id and locationroles.locationid=eventlocations.id and eventlocations.eventid=events.id and roleavailableforstaff.staffid=" & _activeUser
                    rd = com.ExecuteReader
                    dt.Load(rd)

                    Dim dv As New DataView(dt)
                    Return dv
                End Using
                DBConn.Close()
            End Using
        End Get
    End Property
    'Get work shifts that has been confirmed for the logged in user on a certain event
    Public Overrides ReadOnly Property GetConfirmedShifts(ByVal EventID As Integer) As System.Collections.ICollection
        Get
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    If _activeUser = -1 Then Return Nothing
                    Dim dt As New DataTable
                    Dim rd As SqlClient.SqlDataReader

                    com.CommandText = "SELECT eventshift.date,eventshift.startmam,eventshift.endmam,locationroles.name as role,eventlocations.name as location FROM eventshift,eventshiftstaff,locationroles,eventlocations WHERE eventshift.eventid=" & EventID & " and eventshiftstaff.shiftid=eventshift.id and eventshiftstaff.staffid=" & _activeUser & " AND locationroles.id=eventshift.roleid and eventlocations.eventid=eventshift.eventid and eventlocations.id=locationroles.locationid"
                    rd = com.ExecuteReader
                    dt.Load(rd)

                    Dim dv As New DataView(dt)
                    Return dv
                End Using
                DBConn.Close()
            End Using
        End Get
    End Property

    'Get all available work shifts for the logged in user on a certain event
    Public Overrides ReadOnly Property GetAvailableShifts(ByVal EventID As Integer) As System.Collections.ICollection
        Get
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    If _activeUser = -1 Then Return Nothing
                    Dim dt As New DataTable
                    Dim rd As SqlClient.SqlDataReader

                    com.CommandText = "SELECT es.*,eventlocations.name as location,locationroles.name as role, (SELECT -1 FROM eventshiftavailablestaff WHERE staffid=ras.staffid AND shiftid=es.id) as Checked FROM eventshift as es,roleavailableforstaff as ras,eventlocations,locationroles WHERE  ras.roleid=es.roleid and staffid=" & _activeUser & " and eventlocations.eventid=es.eventid and locationroles.locationid=eventlocations.id and es.roleid=locationroles.id and es.eventid=" & EventID & " ORDER BY es.[date],locationroles.name,es.startmam,es.endmam,eventlocations.name"
                    rd = com.ExecuteReader
                    dt.Load(rd)

                    Dim dv As New DataView(dt)
                    Return dv
                End Using
                DBConn.Close()
            End Using
        End Get
    End Property

    'Get a list of all events where there are jobs available for the logged in user
    Public Overrides ReadOnly Property GetAvailableJobs() As System.Collections.ICollection
        Get
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    If _activeUser = -1 Then Return Nothing
                    Select Case GetLoggedInUserInfo.Type
                        Case cUserInfo.UserTypeEnum.Staff
                            Dim dt As New DataTable
                            Dim rd As SqlClient.SqlDataReader

                            com.CommandText = "SELECT DISTINCT events.id,events.name FROM events,eventshift,roleavailableforstaff WHERE events.id=eventshift.eventid and eventshift.roleid=roleavailableforstaff.roleid AND StaffID=" & _activeUser
                            rd = com.ExecuteReader
                            dt.Load(rd)

                            Dim dv As New DataView(dt)
                            Return dv
                        Case cUserInfo.UserTypeEnum.Salesman
                            Dim dt As New DataTable
                            Dim rd As SqlClient.SqlDataReader

                            com.CommandText = "SELECT DISTINCT events.id,events.name,(SELECT MaxDays-BookedDays FROM DaysForSalesman(eventstaff.eventid,eventstaff.staffid)) as daysleft FROM events,eventstaff WHERE events.id=eventstaff.eventid and eventstaff.staffid=" & _activeUser
                            rd = com.ExecuteReader
                            dt.Load(rd)

                            Dim dv As New DataView(dt)
                            Return dv
                    End Select
                End Using
                DBConn.Close()
            End Using
        End Get
    End Property

    'Update wether the logged in user can or can not work a certain shift
    Public Overrides Sub UpdateAvailableShift(ByVal ShiftID As Integer, ByVal Checked As Boolean)
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "DELETE FROM eventshiftavailablestaff WHERE ShiftID=" & ShiftID & " AND StaffID=" & _activeUser
                com.ExecuteNonQuery()
                If Checked Then
                    com.CommandText = "INSERT INTO eventshiftavailablestaff (ShiftID,StaffID) VALUES (" & ShiftID & "," & _activeUser & ")"
                    com.ExecuteNonQuery()
                End If
            End Using
            DBConn.Close()
        End Using
    End Sub

    'Check if a username already exists in the database
    Public Overrides Function UsernameExists(ByVal Username As String) As Boolean
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "SELECT ID From Staff WHERE login='" & Username & "'"
                Dim TmpExists As Boolean = com.ExecuteReader.HasRows
                DBConn.Close()
                Return TmpExists
            End Using
            DBConn.Close()
        End Using
    End Function

    'Verify a newly created account
    Public Overrides Function VerifyAccount(ByVal Username As String, ByVal VerificationCode As String, ByVal Password As String) As Integer
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "SELECT login,VerificationCode From Staff WHERE login='" & Username & "'"
                Dim TmpRd As SqlClient.SqlDataReader = com.ExecuteReader
                TmpRd.Read()
                Dim TmpReturn As Integer
                If TmpRd("VerificationCode") = "" Then
                    TmpReturn = -1
                ElseIf TmpRd("VerificationCode") = VerificationCode Then
                    TmpReturn = 1
                Else
                    TmpReturn = 0
                End If
                If TmpReturn = 1 Then
                    TmpRd.Close()
                    com.CommandText = "UPDATE Staff SET VerificationCode='',Password='" & Password & "' WHERE login='" & Username & "' AND VerificationCode='" & VerificationCode & "'"
                    com.ExecuteNonQuery()
                End If
                DBConn.Close()
                Return TmpReturn
            End Using
            DBConn.Close()
        End Using
    End Function

    'Create a new user and set a VerificationCode
    Public Overrides Function CreateUser(ByVal Username As String, ByVal Email As String) As String
        Try
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    Dim TmpCode As String = Helper.CreateVerificationCode
                    com.CommandText = "INSERT INTO staff (login,Email,VerificationCode) VALUES ('" & Username & "','" & Email & "','" & TmpCode & "')"
                    com.ExecuteNonQuery()
                    Return TmpCode
                End Using
                DBConn.Close()
            End Using
        Catch ex As Exception
            Return ""
        End Try
    End Function

    'Send an email with the password to a user
    Public Overrides Sub RecoverPassword(ByVal Username As String)
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                Dim TmpCode As String = Helper.CreateVerificationCode
                com.CommandText = "SELECT Email,Password FROM Staff WHERE Login='" & Username & "'"
                Dim TmpRd As SqlClient.SqlDataReader = com.ExecuteReader
                If TmpRd.Read Then
                    SendMail(TmpRd("Email"), "", "Ditt lösenord", "Hej,<br /><br />Ditt lösenord till MEC Access är " & TmpRd("Password") & "<br /><br />Mvh<br /><br />MEC Access")
                End If
            End Using
            DBConn.Close()
        End Using
    End Sub

    'Change the passowrd for the logged in user
    Public Overrides Sub ChangePassword(ByVal NewPassword As String)
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "UPDATE staff SET Password='" & NewPassword & "' WHERE ID=" & _activeUser
                com.ExecuteNonQuery()
            End Using
            DBConn.Close()
        End Using
    End Sub

    'Get a staff image from the database
    Public Overrides Function GetImage() As Byte()
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "SELECT Picture FROM Staff WHERE ID=" & _activeUser
                Dim TmpImage As Object = com.ExecuteScalar
                If Not IsDBNull(TmpImage) Then
                    Return TmpImage
                Else
                    Dim NoPicture As New Drawing.Bitmap(100, 150)
                    Using gfx As Drawing.Graphics = Drawing.Graphics.FromImage(NoPicture)
                        Using fnt = New Drawing.Font("Arial", 12, Drawing.FontStyle.Bold)
                            gfx.Clear(Drawing.Color.White)
                            gfx.DrawString("No Picture", fnt, Drawing.Brushes.DarkGray, NoPicture.Width / 2 - gfx.MeasureString("No Picture", fnt).Width / 2, NoPicture.Height / 2 - gfx.MeasureString("No Picture", fnt).Height / 2)
                        End Using
                    End Using
                    Dim TmpStream As New IO.MemoryStream
                    NoPicture.Save(TmpStream, Drawing.Imaging.ImageFormat.Jpeg)
                    Dim TmpByte(TmpStream.Length) As Byte
                    TmpStream.Read(TmpByte, 0, TmpStream.Length)
                    Return TmpByte
                End If
            End Using
            DBConn.Close()
        End Using
    End Function

    'Save a staff image to the database
    Public Overrides Function UploadImage(ByVal ByteArray() As Byte) As Boolean
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "UPDATE Staff SET Picture=@pic WHERE ID=" & _activeUser
                com.Parameters.AddWithValue("@pic", ByteArray)
                Try
                    com.ExecuteNonQuery()
                Catch
                    Return False
                End Try
                Return True
            End Using
            DBConn.Close()
        End Using
    End Function

    'Get a list of available staff categories
    Public Overrides Function GetCategories() As System.Collections.ICollection
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                Dim rd As SqlClient.SqlDataReader
                Dim dt As New DataTable
                com.CommandText = "SELECT * FROM staffcategories"
                rd = com.ExecuteReader
                dt.Load(rd)
                Dim dv As New DataView(dt)
                Return dv
            End Using
            DBConn.Close()
        End Using
    End Function

    'Get what categories the logged in user has stated he/she could work as
    Public Overrides Function GetSelectedCategoriesForLoggedInUser() As System.Collections.ICollection
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                Dim rd As SqlClient.SqlDataReader
                Dim dt As New DataTable
                com.CommandText = "SELECT * FROM staffcategoriesavailablestaff WHERE StaffID=" & _activeUser
                rd = com.ExecuteReader
                dt.Load(rd)
                Dim dv As New DataView(dt)
                Return dv
            End Using
            DBConn.Close()
        End Using
    End Function

    'Save the chosen categories for the logged in user
    Public Overrides Sub SaveSelectedCategoriesForLoggedInUser(ByVal Categories As System.Collections.Generic.List(Of Integer))
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "DELETE FROM staffcategoriesavailablestaff WHERE StaffID=" & _activeUser
                com.ExecuteNonQuery()
                For Each TmpID As Integer In Categories
                    com.CommandText = "INSERT INTO staffcategoriesavailablestaff (StaffID,CategoryID) VALUES (" & _activeUser & "," & TmpID & ")"
                Next
                com.ExecuteNonQuery()
            End Using
            DBConn.Close()
        End Using
    End Sub
#End Region

#Region "InStore"
    'Check if a day is available for booking
    Public Overrides Function DayAvailable(ByVal Day As Date, ByVal CampaignID As Integer) As Boolean
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                Dim rd As SqlClient.SqlDataReader
                com.CommandText = "SELECT events.id as event_id,convert(DATETIME,FLOOR(CONVERT(FLOAT,@date))) FROM events,eventstaff WHERE ((SELECT count(bookingdates.id) FROM bookingdates,bookings WHERE bookings.eventid=events.id and bookingdates.bookingid=bookings.id and date=@date)<events.maxbookingsperday OR events.maxbookingsperday=0) AND round(convert(decimal(18,5),events.startdate),0,1)<=convert(decimal(18,5),@date) and round(convert(decimal(18,5),events.enddate),0,1)>=convert(decimal(18,5),@date) and events.id=@campid and eventstaff.staffid=@staffid AND convert(DATETIME,FLOOR(CONVERT(FLOAT,@date))) NOT IN (SELECT convert(DATETIME,FLOOR(CONVERT(FLOAT,[day]))) FROM eventexcludeddays WHERE eventid=@campid)"
                com.Parameters.Add(New SqlClient.SqlParameter("@date", Day))
                com.Parameters.Add(New SqlClient.SqlParameter("@campid", CampaignID))
                com.Parameters.Add(New SqlClient.SqlParameter("@staffid", _activeUser))
                rd = com.ExecuteReader
                If rd.Read Then
                    rd.Close()
                    Return True
                Else
                    rd.Close()
                    Return False
                End If
            End Using
            DBConn.Close()
        End Using
    End Function

    'Get a list of providers (swedish "Demobolag") available for a certain campaign
    Public Overrides Function GetProviders(ByVal CampaignID As Integer) As System.Collections.ICollection
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                Dim rd As SqlClient.SqlDataReader
                If GetLoggedInUserInfo.Type = cUserInfo.UserTypeEnum.HeadOfSales Then
                    com.CommandText = "SELECT Staff.ID,Staff.FirstName FROM Staff,eventstaff,events,products WHERE events.id=eventstaff.eventid and Staff.Type=3 AND eventstaff.staffid=Staff.id and events.productid=products.id and products.clientid=" & GetLoggedInUserInfo.clientID & " AND eventstaff.eventid=" & CampaignID
                    'com.CommandText = "SELECT Staff.ID,Staff.FirstName FROM Staff,eventstaff,events,products WHERE events.id=eventstaff.eventid and Staff.Type=3 AND eventstaff.staffid=Staff.id and events.productid=products.id and products.clientid=" & GetLoggedInUserInfo.clientID & " AND eventstaff.eventid=" & CampaignID
                Else
                    com.CommandText = "SELECT Staff.ID,Staff.FirstName FROM Staff,eventstaff WHERE Staff.Type=3 AND eventstaff.staffid=" & _activeUser & " AND eventstaff.eventid=" & CampaignID
                End If
                rd = com.ExecuteReader
                Dim TmpDT As DataTable = New DataTable
                TmpDT.Load(rd)
                If TmpDT.Rows.Count = 0 Then
                    Return Nothing
                Else
                    Dim TmpDV As New DataView(TmpDT)
                    Return TmpDV
                End If
            End Using
            DBConn.Close()
        End Using
    End Function

    'Add a date to be booked 
    Public Overrides Sub AddBookingDate(ByVal BookingID As Integer, ByVal [Date] As Date, ByVal Time As Integer)
        Using DBConn As SqlClient.SqlConnection = Connect()
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
        Using DBConn As SqlClient.SqlConnection = Connect()
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
        Using DBConn As SqlClient.SqlConnection = Connect()
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

    'Save data for a booking
    Public Overrides Function SaveBooking(ByVal CampaignID As Integer, ByVal Store As String, ByVal Address As String, ByVal City As String, ByVal Phone As String, ByVal Contact As String, ByVal Placement As String, ByVal Comments As String, ByVal HasKitchen As Boolean, ByVal ProviderID As Integer, Optional ByVal ProviderText As String = "", Optional ByVal BookingID As Integer = 0) As Integer
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                If BookingID = 0 Then
                    com.CommandText = "INSERT INTO bookings (eventid,staffid,store,address,city,phone,contact,placement,comments,providerid,otherprovider,storehaskitchen,confirmed,timesaved) VALUES (@eid,@staffid,@store,@addr,@city,@phone,@cnt,@plc,@com,@pid,@prov,@kitchen,0,@timesaved);SELECT @@identity"
                Else
                    com.CommandText = "INSERT INTO bookings (eventid,staffid,store,address,city,phone,contact,placement,comments,providerid,otherprovider,storehaskitchen,confirmed,timesaved) VALUES (@eid,@staffid,@store,@addr,@city,@phone,@cnt,@plc,@com,@pid,@prov,@kitchen,0,@timesaved);UPDATE bookings SET chosenprovider=(SELECT chosenprovider FROM bookings WHERE id=@bid) WHERE id=@@identity;UPDATE bookings SET chosenprovidername=(SELECT chosenprovidername FROM bookings WHERE id=@bid) WHERE id=@@identity; UPDATE bookingdates SET bookingid=@@identity WHERE bookingid=@bid;UPDATE bookingproducts SET bookingid=@@identity WHERE bookingid=@bid;UPDATE bookingcollaborations SET bookingid=@@identity WHERE bookingid=@bid;DELETE FROM bookings WHERE id=@bid;SELECT @@identity"
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
                com.Parameters.Add(New SqlClient.SqlParameter("@staffid", _activeUser))
                com.Parameters.AddWithValue("@timesaved", Now)
                Return com.ExecuteScalar
            End Using
            DBConn.Close()
        End Using
    End Function

    'Get a list of all bookings for the logged in user, depending on Status
    Public Overrides Function GetBookings(Optional ByVal Status As BookingStatusEnum = BookingStatusEnum.bsPending) As System.Collections.ICollection
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                If Status = BookingStatusEnum.bsPending Then
                    com.CommandText = "SELECT bookings.id,bookings.eventid,events.name,bookings.store,bookings.city,convert(varchar(MAX),MIN(bookingdates.date),112)+'-'+convert(varchar(MAX),MAX(bookingdates.date),112) as dates,chosenproviderstaffname FROM bookings,bookingdates,events WHERE confirmed=0 and eventid=events.id and bookings.id=bookingdates.bookingid and staffid=" & _activeUser & " GROUP BY bookings.id,events.name,eventid,store,city,chosenproviderstaffname order by dates"
                ElseIf Status = BookingStatusEnum.bsConfirmed Then
                    Select Case GetLoggedInUserInfo.Type
                        Case cUserInfo.UserTypeEnum.HeadOfSales
                            com.CommandText = "SELECT bookings.id,bookings.eventid,events.name,bookings.store,bookings.city,convert(varchar(MAX),MIN(bookingdates.date),112)+'-'+convert(varchar(MAX),MAX(bookingdates.date),112) as dates,chosenproviderstaffname,(staff.firstname+' '+staff.lastname) as salesman FROM bookings,bookingdates,events,staff WHERE confirmed=1 and eventid=events.id and bookings.id=bookingdates.bookingid and staffid=staff.id and staff.ClientID=" & GetLoggedInUserInfo.clientID & " GROUP BY bookings.id,events.name,eventid,store,city,chosenproviderstaffname,firstname,lastname order by dates"
                        Case Else
                            com.CommandText = "SELECT bookings.id,bookings.eventid,events.name,bookings.store,bookings.city,convert(varchar(MAX),MIN(bookingdates.date),112)+'-'+convert(varchar(MAX),MAX(bookingdates.date),112) as dates,chosenproviderstaffname,'' as salesman FROM bookings,bookingdates,events WHERE confirmed=1 and eventid=events.id and bookings.id=bookingdates.bookingid and staffid=" & _activeUser & " GROUP BY bookings.id,events.name,eventid,store,city,chosenproviderstaffname order by dates"
                    End Select
                ElseIf Status = BookingStatusEnum.bsRejected Then
                    com.CommandText = "SELECT bookings.id,bookings.eventid,events.name,bookings.store,bookings.city,convert(varchar(MAX),MIN(bookingdates.date),112)+'-'+convert(varchar(MAX),MAX(bookingdates.date),112) as dates,rejectioncomment,chosenproviderstaffname FROM bookings,bookingdates,events WHERE confirmed=-1 and eventid=events.id and bookings.id=bookingdates.bookingid and staffid=" & _activeUser & " GROUP BY bookings.id,events.name,eventid,store,city,rejectioncomment,chosenproviderstaffname order by dates"
                End If
                Dim rd As SqlClient.SqlDataReader
                rd = com.ExecuteReader
                Dim dt As New DataTable
                dt.Load(rd)
                Dim dv As New DataView(dt)
                Return dv
            End Using
            DBConn.Close()
        End Using
    End Function

    'Get a single booking from db, by its booking id
    Public Overrides Function GetBooking(ByVal BookingID As Integer) As System.Data.DataRow
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "SELECT bookings.*,events.clientid,staff.id as staffid,(staff.firstname+' '+staff.lastname) as salesman,staff.mobilephone,(SELECT Clients.Name FROM Clients,Products,events WHERE clients.id=products.clientid and products.id=events.productid and bookings.eventid=events.id) as Client FROM bookings,staff,events WHERE bookings.staffid=staff.id AND bookings.eventid=events.id AND bookings.id=" & BookingID
                com.CommandText = "SELECT bookings.*,events.id,staff.id as staffid,(staff.firstname+' '+staff.lastname) as salesman,staff.mobilephone,(SELECT Clients.Name FROM Clients,Products,events WHERE clients.id=products.clientid and products.id=events.productid and bookings.eventid=events.id) as Client FROM bookings,staff,events WHERE bookings.staffid=staff.id AND bookings.eventid=events.id AND bookings.id=" & BookingID
                'com.CommandText = "SELECT bookings.*,events.clientid,staff.id as staffid,(staff.firstname+' '+staff.lastname) as salesman,staff.mobilephone,(SELECT Clients.Name FROM Clients,Products,events WHERE clients.id=products.clientid and products.id=events.productid and bookings.eventid=events.id) as Client FROM bookings,staff,events WHERE bookings.staffid=staff.id AND bookings.eventid=events.id AND bookings.id=" & BookingID
                com.CommandText = "SELECT bookings.*,events.productid,staff.id as staffid,(staff.firstname+' '+staff.lastname) as salesman,staff.mobilephone,(SELECT Clients.Name FROM Clients,Products,events WHERE clients.id=products.clientid and products.id=events.productid and bookings.eventid=events.id) as Client FROM bookings,staff,events WHERE bookings.staffid=staff.id AND bookings.eventid=events.id AND bookings.id=" & BookingID
                Dim rd As SqlClient.SqlDataReader
                rd = com.ExecuteReader
                Dim dt As New DataTable
                dt.Load(rd)
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0)
                Else
                    Return Nothing
                End If
            End Using
            DBConn.Close()
        End Using
    End Function

    'Remove all dates from a booking
    Public Overrides Sub ClearBookingDates(ByVal BookingID As Integer)
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "DELETE FROM bookingdates WHERE bookingid=" & BookingID
                com.ExecuteNonQuery()
            End Using
            DBConn.Close()
        End Using
    End Sub

    'Remove all products from a booking
    Public Overrides Sub ClearBookingProducts(ByVal BookingID As Integer)
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "DELETE FROM bookingproducts WHERE bookingid=" & BookingID
                com.ExecuteNonQuery()
            End Using
            DBConn.Close()
        End Using
    End Sub

    'Remove all collaborations from a booking
    Public Overrides Sub ClearCollaborations(ByVal BookingID As Integer)
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "DELETE FROM bookingcollaborations WHERE bookingid=" & BookingID
                com.ExecuteNonQuery()
            End Using
            DBConn.Close()
        End Using
    End Sub

    'Get a list of dates booked for a certain booking
    Public Overrides Function GetBookingDates(ByVal BookingID As Integer) As DataTable
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                Dim TmpDT As New DataTable
                TmpDT.Columns.Add("ID", GetType(Integer))
                TmpDT.Columns.Add("Date", GetType(Date))
                TmpDT.Columns.Add("Time", GetType(Integer))
                Dim Key() As DataColumn = {TmpDT.Columns("ID")}
                TmpDT.PrimaryKey = Key

                com.CommandText = "SELECT id,[date],time FROM bookingdates WHERE bookingid=" & BookingID
                Dim rd As SqlClient.SqlDataReader = com.ExecuteReader
                Dim dt As New DataTable
                dt.Load(rd)
                For Each TmpRow As DataRow In dt.Rows
                    TmpDT.Rows.Add(TmpDT.Rows.Count + 1, TmpRow!date, TmpRow!time)
                Next
                If TmpDT.Rows.Count > 0 Then
                    Return TmpDT
                Else
                    Return Nothing
                End If
            End Using
            DBConn.Close()
        End Using
    End Function

    'Get a list of Collaborations for a certain booking
    Public Overrides Function GetBookingCollaborations(ByVal BookingID As Integer) As System.Data.DataTable
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "SELECT * FROM bookingcollaborations WHERE bookingid=" & BookingID
                Dim rd As SqlClient.SqlDataReader = com.ExecuteReader
                Dim dt As New DataTable
                dt.Load(rd)
                Return dt
            End Using
            DBConn.Close()
        End Using
    End Function

    'Get a list of products for a certain booking
    Public Overrides Function GetBookingProducts(ByVal BookingID As Integer) As System.Data.DataTable
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "SELECT * FROM bookingproducts WHERE bookingid=" & BookingID
                Dim rd As SqlClient.SqlDataReader = com.ExecuteReader
                Dim dt As New DataTable
                dt.Load(rd)
                Return dt
            End Using
            DBConn.Close()
        End Using
    End Function

    'Get a list of running bookings for the logged in user
    Public Overrides ReadOnly Property CurrentBookings() As DataTable
        Get
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    com.CommandText = "SELECT (SELECT clients.name FROM clients,products WHERE products.clientid=clients.id and products.id=events.productid) as Client,(staff.firstname+' '+staff.lastname) as salesman,bookings.id,bookings.eventid,events.name,bookings.store,bookings.city,convert(varchar(MAX),MIN(bookingdates.date),112)+'-'+convert(varchar(MAX),MAX(bookingdates.date),112) as dates,chosenproviderstaffname FROM bookings,bookingdates,events,staff WHERE bookingdates.date > GETDATE() and staff.id=bookings.staffid AND confirmedbyprovider=1 and eventid=events.id and bookings.id=bookingdates.bookingid and confirmed=1 and chosenprovider=" & _activeUser & " GROUP BY bookingdates.date,bookings.id,events.name,eventid,store,city,firstname,lastname,events.productid,chosenproviderstaffname"
                    'com.CommandText = "SELECT (SELECT clients.name FROM clients,products WHERE products.clientid=clients.id and products.id=events.productid) as Client,(staff.firstname+' '+staff.lastname) as salesman,bookings.id,bookings.eventid,events.name,bookings.store,bookings.city,convert(varchar(MAX),MIN(bookingdates.date),112)+'-'+convert(varchar(MAX),MAX(bookingdates.date),112) as dates,chosenproviderstaffname FROM bookings,bookingdates,events,staff WHERE staff.id=bookings.staffid AND confirmedbyprovider=1 and eventid=events.id and bookings.id=bookingdates.bookingid and confirmed=1 and chosenprovider=" & _activeUser & " ORDER BY events.date" 'GROUP BY bookings.id,events.name,eventid,store,city,firstname,lastname,events.productid,chosenproviderstaffname"
                    'com.CommandText = "SELECT (SELECT clients.name FROM clients,products WHERE products.clientid=clients.id and products.id=events.productid) as Client,(staff.firstname+' '+staff.lastname) as salesman,bookings.id,bookings.eventid,events.name,bookings.store,bookings.city,convert(varchar(MAX),MIN(bookingdates.date),112)+'-'+convert(varchar(MAX),MAX(bookingdates.date),112) as dates,chosenproviderstaffname FROM bookings,bookingdates,events,staff WHERE staff.id=bookings.staffid AND confirmedbyprovider=1 and eventid=events.id and bookings.id=bookingdates.bookingid and confirmed=1 and chosenprovider=" & _activeUser & " GROUP BY bookingdates.date,bookings.id,events.name,eventid,store,city,firstname,lastname,events.productid,chosenproviderstaffname"
                    Dim rd As SqlClient.SqlDataReader = com.ExecuteReader
                    Dim dt As New DataTable
                    dt.Load(rd)
                    Return dt
                End Using
                DBConn.Close()
            End Using
        End Get
    End Property

    'Get a list of bookings available to the logged in user
    Public Overrides ReadOnly Property GetAvailableBookings() As DataTable
        Get
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    com.CommandText = "SELECT (SELECT clients.name FROM clients,products WHERE products.clientid=clients.id and products.id=events.productid) as Client,(staff.firstname+' '+staff.lastname) as salesman,bookings.id,bookings.eventid,events.name,bookings.store,bookings.city,convert(varchar(MAX),MIN(bookingdates.date),112)+'-'+convert(varchar(MAX),MAX(bookingdates.date),112) as dates FROM bookings,bookingdates,events,staff WHERE staff.id=bookings.staffid AND confirmedbyprovider=0 and eventid=events.id and bookings.id=bookingdates.bookingid and confirmed=1 and chosenprovider=" & _activeUser & " GROUP BY bookingdates.date,bookings.id,events.name,eventid,store,city,firstname,lastname,events.productid"
                    Dim rd As SqlClient.SqlDataReader = com.ExecuteReader
                    Dim dt As New DataTable
                    dt.Load(rd)
                    Return dt
                End Using
                DBConn.Close()
            End Using
        End Get
    End Property

    'Set the status of a booking and set what person will work from the provider
    Public Overrides Sub SetBookingStatus(ByVal BookingID As Integer, ByVal Accept As Boolean, ByVal StaffName As String)
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                If Accept Then
                    com.CommandText = "UPDATE bookings SET confirmedbyprovider=1,timesaved=@timesaved,chosenproviderstaffname='" & StaffName & "' WHERE id=" & BookingID & " AND chosenprovider=" & _activeUser
                    CreateQuestionaires(BookingID)
                Else
                    com.CommandText = "UPDATE bookings SET confirmedbyprovider=-1,timesaved=@timesaved,chosenproviderstaffname='" & StaffName & "' WHERE id=" & BookingID & " AND chosenprovider=" & _activeUser
                End If
                com.Parameters.AddWithValue("@timesaved", Now)
                com.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    'Create Questionaires for a booking
    Private Sub CreateQuestionaires(ByVal BookingID As Integer)
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "SELECT Questionaire.ID,Questionaire.xml,bookingdates.*,bookings.store,bookings.city FROM Questionaire,Bookings,bookingdates WHERE bookings.id=bookingdates.bookingid and Questionaire.EventID=Bookings.EventID AND Bookings.ID=" & BookingID
                Dim rd As SqlClient.SqlDataReader = com.ExecuteReader
                Dim dt As New DataTable
                dt.Load(rd)
                For Each TmpRow As DataRow In dt.Rows
                    SaveQuestionAnswer(TmpRow!id, False, TmpRow!Date, TmpRow!xml, TmpRow!Store, TmpRow!City, , BookingID)
                Next
            End Using
        End Using
    End Sub

    'Get booked days for a given salesman on a given event
    Public Overrides Function GetDaysForSalesman(ByVal EventID As Integer, ByVal StaffID As Integer) As System.Data.DataRow
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "SELECT * FROM DaysForSalesman(" & EventID & "," & StaffID & " )"
                Dim rd As SqlClient.SqlDataReader = com.ExecuteReader
                Dim dt As New DataTable
                dt.Load(rd)
                DBConn.Close()
                Return dt.Rows(0)
            End Using
        End Using
    End Function

    'Get all info on a given event
    Public Overrides Function GetEvent(ByVal EventID As Integer) As System.Data.DataRow
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "SELECT events.*,products.name as product FROM events,products WHERE events.productid=products.id and events.id=" & EventID
                Dim rd As SqlClient.SqlDataReader = com.ExecuteReader
                Dim dt As New DataTable
                dt.Load(rd)
                DBConn.Close()
                Return dt.Rows(0)
            End Using
        End Using
    End Function

    'Get a document from the db
    Public Overrides Function GetDocument(ByVal DocumentID As Integer) As System.Data.DataRow
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "SELECT * FROM Docs WHERE id=" & DocumentID
                Dim rd As SqlClient.SqlDataReader = com.ExecuteReader
                Dim dt As New DataTable
                dt.Load(rd)
                DBConn.Close()
                Return dt.Rows(0)
            End Using
        End Using
    End Function

    'Get all documents attached to an event from the db
    Public Overrides Function GetDocuments(ByVal EventID As Integer) As System.Data.DataTable
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("", DBConn)
                com.CommandText = "SELECT * FROM Docs WHERE EventID=" & EventID
                Dim rd As SqlClient.SqlDataReader = com.ExecuteReader
                Dim dt As New DataTable
                dt.Load(rd)
                DBConn.Close()
                Return dt
            End Using
        End Using
    End Function

    'Get summary of a questionaire for a given event
    Public Overrides Function GetQuestionaireSummary(ByVal SummaryID As Integer) As XmlDocument
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("SELECT [xml] FROM QuestionaireSummary WHERE id=" & SummaryID, DBConn)
                Dim rd As SqlClient.SqlDataReader
                Dim _xml As New XmlDocument
                rd = com.ExecuteReader
                If rd.Read Then
                    _xml.LoadXml(rd!xml)
                End If
                rd.Close()
                DBConn.Close()
                Return _xml
            End Using
        End Using
    End Function
#End Region

    Public Overrides Function GetStores() As DataTable
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("SELECT * FROM stores", DBConn)
                Dim rd As SqlClient.SqlDataReader
                Dim dt As New DataTable
                Dim TmpDT As New DataTable
                rd = com.ExecuteReader
                dt.Load(rd)
                DBConn.Close()
                Return dt
            End Using
        End Using
    End Function


    Public Overrides ReadOnly Property QuestionaireSummaries() As System.Collections.ICollection
        Get
            Using DBConn As SqlClient.SqlConnection = Connect()
                Using com As New SqlClient.SqlCommand("", DBConn)
                    If _activeUser = -1 Then Return Nothing
                    Dim dt As New DataTable
                    Dim rd As SqlClient.SqlDataReader

                    Select Case GetLoggedInUserInfo.Type
                        Case cUserInfo.UserTypeEnum.HeadOfSales
                            com.CommandText = "SELECT DISTINCT QuestionaireSummary.id,Questionairesummary.[name],QuestionaireSummary.QuestionaireID FROM QuestionaireSummary,Questionaire,bookings,staff,staff as mystaff WHERE Questionaire.eventid=bookings.eventid and QuestionaireSummary.questionaireid=questionaire.id and staff.id=bookings.staffid and staff.clientid=mystaff.clientid and mystaff.id=" & _activeUser
                        Case cUserInfo.UserTypeEnum.Salesman
                            com.CommandText = "SELECT DISTINCT QuestionaireSummary.id,Questionairesummary.[name],QuestionaireSummary.QuestionaireID FROM QuestionaireSummary,Questionaire,bookings WHERE Questionaire.eventid=bookings.eventid and QuestionaireSummary.questionaireid=questionaire.id and bookings.staffid=" & _activeUser
                        Case Else
                            Return Nothing
                    End Select
                    rd = com.ExecuteReader
                    dt.Load(rd)

                    Dim dv As New DataView(dt)
                    Return dv
                End Using
                DBConn.Close()
            End Using
        End Get
    End Property

    Public Overrides Function GetQuestionaireAnswers(ByVal QuestionaireID As Integer) As System.Collections.Generic.List(Of System.Xml.XmlDocument)
        Using DBConn As SqlClient.SqlConnection = Connect()
            Using com As New SqlClient.SqlCommand("SELECT [xml] FROM QuestionaireAnswers WHERE Answered=1 and QuestionaireId=" & QuestionaireID, DBConn)
                Dim _list As New List(Of XmlDocument)
                Dim rd As SqlClient.SqlDataReader
                rd = com.ExecuteReader
                While rd.Read
                    Dim _xml As New XmlDocument
                    _xml.LoadXml(rd!xml)
                    _list.Add(_xml)
                End While
                rd.Close()
                DBConn.Close()
                Return _list
            End Using
        End Using
    End Function
End Class
