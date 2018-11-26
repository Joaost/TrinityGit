Public MustInherit Class cDBReader

    Public Enum ConnectionMode
        cmNotConnected = -1
        cmNetwork = 0
        cmLocal = 1
    End Enum

    Public Structure EventStruct
        Public ID As Integer
        Public Name As String

        Public Overrides Function ToString() As String
            Return Name
        End Function

    End Structure

    Public MustOverride Function Connect(ByVal ConnectionMode As ConnectionMode) As Object

    Public MustOverride Function Clients() As List(Of cClient)

    Public MustOverride Function StaffCategories() As List(Of cStaffCategory)

    Public MustOverride Function MaterialCosts() As List(Of cCost)

    Public MustOverride Function LogisticsCosts() As List(Of cCost)

    Public MustOverride Function PlanningCosts() As List(Of cHourCost)

    Public MustOverride Function GetClientByID(ByVal ID As String, Optional ByVal Connection As Data.Common.DbConnection = Nothing) As cClient

    Public MustOverride Function GetProductByID(ByVal ID As String, Optional ByVal Connection As Data.Common.DbConnection = Nothing) As cProduct

    Public MustOverride Function GetStaffList(Optional ByVal Type As cStaff.UserTypeEnum = cStaff.UserTypeEnum.Staff, Optional ByVal ProductID As Integer = 0) As Dictionary(Of Integer, cStaff)

    Public MustOverride Function GetSingleStaff(ByVal StaffID As Integer, Optional ByVal Connection As Data.Common.DbConnection = Nothing) As cStaff

    Public MustOverride Function RemoveSingleStaff(ByVal StaffID As Integer) As Integer

    Public MustOverride Function RemoveCampaign(ByVal Campaign As cEvent) As Integer

    Public MustOverride Function SaveStaff(ByVal Staff As cStaff) As Boolean

    Public MustOverride Sub ResetPassword(ByVal StaffID As Integer)

    Public MustOverride Sub SendPasswordToUser(ByVal StaffID As Integer)

    Public MustOverride Function Events() As List(Of EventStruct)

    Public MustOverride Function EventTemplates() As List(Of EventStruct)

    Public MustOverride Function GetEvent(ByVal EventID As Integer) As cEvent

    Public MustOverride Function GetEventTemplate(ByVal TemplateID As Integer) As cEvent

    Public MustOverride Function SaveEventToDB(ByVal Name As String, ByVal XML As String, ByVal [Event] As cEvent, ByVal ProductID As Integer, Optional ByVal EventID As Integer = -1) As Integer

    Public MustOverride Sub SaveQuestionaireToDB(ByRef Questionaire As cQuestionaire, ByVal EventID As Integer)

    Public MustOverride Function Login(ByVal username As String, ByVal password As String) As Integer

    Public MustOverride Function LoggedInUserID() As Integer


    Public MustOverride Function CampaignSummary(ByVal Questionnaire As Integer) As Collection

    Public MustOverride Function GetAllQuestionnaires() As Collection


    Public MustOverride Function SumAnswersForEvent(ByVal EventID As Integer) As DataRow

    Public MustOverride Function SumRatingAnswersForEvent(ByVal EventID As Integer) As DataTable

    Public MustOverride Function GetRatingAnswersForEvent(ByVal EventID As Integer, ByVal AnswerID As Integer) As DataTable

    Public MustOverride Function GetAnswersForEvent(ByVal EventID As Integer) As DataTable

    Public MustOverride Function AddStaff() As Integer

    Public MustOverride Function GetEventStaffLocationRoleList(ByVal EventID As Integer) As DataTable

    Public MustOverride Sub SaveScheduleToDB(ByRef Schedule As cStaffSchedule)

    Public MustOverride Function GetAvailableStaffForShiftList(ByVal ShiftID As Integer) As List(Of cStaff)

    Public MustOverride Function GetAssignedStaffForShiftList(ByVal ShiftID As Integer) As List(Of cStaff)

    Public MustOverride Sub SaveConfirmedToDB(ByRef Schedule As cStaffSchedule)

    Public MustOverride Function AddClient(ByVal Name As String) As cClient

    Public MustOverride Function AddProduct(ByVal Name As String, ByVal ClientID As Integer) As cProduct

    Public MustOverride Sub AddContact(ByVal Name As String, ByVal Role As String, ByVal PhoneNr As String, ByVal ProductID As Integer, ByVal Internal As Boolean)

    Public MustOverride Function GetSelectedCategoriesForStaff(ByVal StaffID As Integer) As List(Of Integer)

    Public MustOverride Function GetCVForStaff(ByVal StaffID As Integer) As List(Of cCVEntry)

    Public MustOverride Sub SaveInStoreToDB(ByVal InStore As cInStore)

    Public MustOverride Function GetBookingsForEvent(ByVal EventID As String, Optional ByVal Connection As Data.Common.DbConnection = Nothing) As Dictionary(Of Integer, cBooking)

    Public MustOverride Function SaveBooking(ByVal CampaignID As Integer, ByVal StaffID As Integer, ByVal Store As String, ByVal Address As String, ByVal City As String, ByVal Phone As String, ByVal Contact As String, ByVal Placement As String, ByVal Comments As String, ByVal HasKitchen As Boolean, ByVal ProviderID As Integer, Optional ByVal ProviderText As String = "", Optional ByVal BookingID As Integer = 0) As Integer

    Public MustOverride Sub AddBookingDate(ByVal BookingID As Integer, ByVal [Date] As Date, ByVal Time As Integer)

    Public MustOverride Sub AddBookingProduct(ByVal BookingID As Integer, ByVal Product As String)

    Public MustOverride Sub AddCollaboration(ByVal BookingID As Integer, ByVal Company As String, ByVal Product As String, ByVal ShareOfInvoice As Integer, ByVal Reference As String, ByVal Address As String, ByVal PhoneNr As String, ByVal ZipCode As String)

    Public MustOverride Function GetAllBookings() As List(Of cBooking)

    Friend MustOverride Sub SaveBookingData(ByVal BookingData As Dictionary(Of Integer, cEvent.BookingDataStruct))

    Public MustOverride Function SetBookingIsInvoiced(ByVal BookingID As Integer, ByVal Invoiced As Boolean)

    Public MustOverride Function GetCommentAnswersForEvent(ByVal EventID As Integer, Optional ByVal AnswerID As Integer = -1) As SortedList(Of String, String)

    Public MustOverride Sub SetBookingStatus(ByVal BookingID As Integer, ByVal Status As cBooking.BookingStatusEnum)

    Public MustOverride Sub SetRejectionComment(ByVal BookingID As Integer, ByVal Comment As String)

    Public MustOverride Sub SetProvider(ByVal BookingID As Integer, ByVal Provider As cStaff, Optional ByVal ProviderName As String = "")

    Public MustOverride Sub DeleteStaff(ByVal StaffID As Integer)

    Public MustOverride Sub DeleteBooking(ByVal BookingID As Integer)

    Public MustOverride Sub RegisterBookingEventConsumer(ByVal bec As IBookingEventConsumer)

    Public MustOverride Sub UnRegisterBookingEventConsumer(ByVal bec As IBookingEventConsumer)

    Public MustOverride Function GetStores() As List(Of cStore)

    Public MustOverride Sub DeleteStore(ByVal StoreID As Integer)

    Public MustOverride Sub SaveStore(ByVal Store As cStore)

    Public MustOverride Function SaveQuestionaireTemplate(ByVal Name As String, ByVal Questionaire As cQuestionaire) As Integer

    Public MustOverride Function GetQuestionaireTemplate(ByVal ID As Integer) As cQuestionaire

    Public MustOverride Function GetQuestionaireTemplates() As List(Of cQuestionaire)

End Class
