Public MustInherit Class cDBReader

    Public Structure DB_Entry
        Public ID As Integer
        Public Label As String
    End Structure

    Public Enum BookingStatusEnum
        bsRejected = -1
        bsPending = 0
        bsConfirmed = 1
    End Enum

    Public Sub SendMail(ByVal ToAddress As String, ByVal FromAddress As String, ByVal Subject As String, ByVal Body As String, Optional ByVal AttachmentPath As String = "")
        Throw (New Exception("För tillfället kan du inte maila via formuläret." & vbCrLf & vbCrLf & "Vänligen ring enligt ovan."))
    End Sub

    Public MustOverride Function Connect() As Object

    Public MustOverride Function Login(ByVal User As String, ByVal Password As String) As Integer

    Public MustOverride ReadOnly Property ConnectionState() As System.Data.ConnectionState

    Public MustOverride ReadOnly Property CurrentJobs() As ICollection

    Public MustOverride ReadOnly Property Questionaires(Optional ByVal IncludeAnswers As Boolean = False) As ICollection

    Public MustOverride ReadOnly Property QuestionaireSummaries() As ICollection

    Public MustOverride ReadOnly Property GetQuestionaire(ByVal ID As Integer) As DataRow

    Public MustOverride ReadOnly Property GetQuestionaireAnswer(ByVal AnswerID As Integer) As DataRow

    Public MustOverride ReadOnly Property GetRatingQuestions(ByVal QID As Integer) As ICollection

    Public MustOverride ReadOnly Property GetRatingQuestionAnswers(ByVal AnswerID As Integer) As DataTable

    Public MustOverride ReadOnly Property GetCommentQuestions(ByVal QID As Integer) As ICollection

    Public MustOverride ReadOnly Property GetCommentQuestionAnswers(ByVal AnswerID As Integer) As DataTable

    Public MustOverride Function SaveQuestionAnswer(ByVal QID As Integer, ByVal Answered As Boolean, ByVal AtDate As Date, ByVal AnswerXML As String, ByVal Locations As String, ByVal City As String, Optional ByVal AnswerID As Integer = -1, Optional ByVal BookingID As Integer = -1) As Integer

    'Public MustOverride Function SaveQuestionAnswer(ByVal QID As Integer, ByVal Answered As Boolean, ByVal AnsweredBy As String, ByVal AnseredByPhoneNr As String, ByVal AtDate As Date, ByVal StartTime As Integer, ByVal EndTime As Integer, ByVal Locations As String, ByVal City As String, ByVal Quantity As Integer, ByVal InTarget As Integer, ByVal Target As Integer, ByVal Positive As Integer, ByVal Comments As String, Optional ByVal AnswerID As Integer = -1, Optional ByVal BookingID As Integer = -1) As Integer

    Public MustOverride Sub SaveRatingQuestionAnswer(ByVal AnswerID As Integer, ByVal QuestionaireRatingID As Integer, ByVal Value As Integer, Optional ByVal RatingQuestionAnswerID As Integer = -1)

    Public MustOverride Sub SaveCommentQuestionAnswer(ByVal AnswerID As Integer, ByVal QuestionaireRatingID As Integer, ByVal Answer() As String, Optional ByVal RatingQuestionAnswerID As Integer = -1)

    Public MustOverride Sub DeleteQuestionAnswer(ByVal AnswerID As Integer)

    Public MustOverride ReadOnly Property GetUserInfo() As DataRow

    Public MustOverride Sub SaveUserInfo(ByVal Firstname As String, ByVal Secondname As String, ByVal Birthday As Date, ByVal Gender As Byte, ByVal DriverB As Byte, ByVal DriverC As Byte, ByVal DriverD As Byte, ByVal DriverE As Byte, ByVal Email As String, ByVal Address1 As String, ByVal Address2 As String, ByVal ZipCode As String, ByVal ZipArea As String, ByVal HomePhone As String, ByVal WorkPhone As String, ByVal MobilePhone As String, ByVal Bank As String, ByVal ClearingNo As String, ByVal AccountNo As String, ByVal Info As String)

    Public MustOverride ReadOnly Property GetConfirmedShifts(ByVal EventID As Integer) As ICollection

    Public MustOverride ReadOnly Property GetAvailableShifts(ByVal EventID As Integer) As ICollection

    Public MustOverride ReadOnly Property GetAvailableJobs() As ICollection

    Public MustOverride Sub UpdateAvailableShift(ByVal ShiftID As Integer, ByVal Checked As Boolean)

    Public MustOverride Function UsernameExists(ByVal Username As String) As Boolean

    Public MustOverride Function VerifyAccount(ByVal Username As String, ByVal VerificationCode As String, ByVal Password As String) As Integer

    Public MustOverride Function CreateUser(ByVal Username As String, ByVal Email As String) As String

    Public MustOverride Sub RecoverPassword(ByVal Username As String)

    Public MustOverride Sub ChangePassword(ByVal NewPassword As String)

    Public MustOverride Function GetImage() As Byte()

    Public MustOverride Function UploadImage(ByVal ByteArray() As Byte) As Boolean

    Public MustOverride Function GetCategories() As ICollection

    Public MustOverride Function GetSelectedCategoriesForLoggedInUser() As ICollection

    Public MustOverride Sub SaveSelectedCategoriesForLoggedInUser(ByVal Categories As List(Of Integer))

    Public MustOverride Function GetLoggedInUserInfo() As cUserInfo

    Public MustOverride Function DayAvailable(ByVal Day As Date, ByVal CampaignID As Integer) As Boolean

    Public MustOverride Function GetProviders(ByVal CampaignID As Integer) As ICollection

    Public MustOverride Function SaveBooking(ByVal CampaignID As Integer, ByVal Store As String, ByVal Address As String, ByVal City As String, ByVal Phone As String, ByVal Contact As String, ByVal Placement As String, ByVal Comments As String, ByVal HasKitchen As Boolean, ByVal ProviderID As Integer, Optional ByVal ProviderText As String = "", Optional ByVal BookingID As Integer = 0) As Integer

    Public MustOverride Sub AddBookingDate(ByVal BookingID As Integer, ByVal [Date] As Date, ByVal Time As Integer)

    Public MustOverride Sub AddBookingProduct(ByVal BookingID As Integer, ByVal Product As String)

    Public MustOverride Sub AddCollaboration(ByVal BookingID As Integer, ByVal Company As String, ByVal Product As String, ByVal ShareOfInvoice As Integer, ByVal Reference As String, ByVal Address As String, ByVal PhoneNr As String, ByVal ZipCode As String)

    Public MustOverride Function GetBookings(Optional ByVal Status As BookingStatusEnum = BookingStatusEnum.bsPending) As ICollection

    Public MustOverride Function GetBooking(ByVal BookingID As Integer) As DataRow

    Public MustOverride Function GetBookingDates(ByVal BookingID As Integer) As DataTable

    Public MustOverride Function GetBookingProducts(ByVal BookingID As Integer) As DataTable

    Public MustOverride Function GetBookingCollaborations(ByVal BookingID As Integer) As DataTable

    Public MustOverride Sub ClearBookingDates(ByVal BookingID As Integer)

    Public MustOverride Sub ClearBookingProducts(ByVal BookingID As Integer)

    Public MustOverride Sub ClearCollaborations(ByVal BookingID As Integer)

    Public MustOverride ReadOnly Property GetAvailableBookings() As DataTable

    Public MustOverride ReadOnly Property CurrentBookings() As DataTable

    Public MustOverride Sub SetBookingStatus(ByVal BookingID As Integer, ByVal Accept As Boolean, ByVal StaffName As String)

    Public MustOverride Function GetDaysForSalesman(ByVal EventID As Integer, ByVal StaffID As Integer) As DataRow

    Public MustOverride Function GetEvent(ByVal EventID As Integer) As DataRow

    Public MustOverride Function GetDocument(ByVal DocumentID As Integer) As DataRow

    Public MustOverride Function GetDocuments(ByVal EventID As Integer) As DataTable

    Public MustOverride Function GetQuestionaireSummary(ByVal SummaryID As Integer) As XmlDocument

    Public MustOverride Function GetQuestionaireAnswers(ByVal QuestionaireID As Integer) As List(Of XmlDocument)

    Public MustOverride Function GetStores() As DataTable

End Class
