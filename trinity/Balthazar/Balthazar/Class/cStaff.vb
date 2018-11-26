Public Class cStaff

    Public DatabaseID As Integer = -1
    Private _firstName As String
    Public LastName As String
    Public Birthday As Date
    Public Gender As GenderEnum = GenderEnum.Female
    Public Adress1 As String
    Public Adress2 As String
    Public ZipCode As String
    Public ZipArea As String
    Public HomePhone As String
    Public MobilePhone As String
    Public WorkPhone As String
    Public Driver As DriverEnum
    Public Bank As String
    Public ClearingNo As String
    Public AccountNo As String
    Public InternalInfo As String
    Public ExternalInfo As String
    Public Email As String
    Public Username As String
    Public Password As String
    Public Picture As Image
    Public SavedToDB As Boolean = True
    Public Client As cClient
    Public Type As UserTypeEnum = UserTypeEnum.Staff
    Private _dbRowVersion As String

    Public Enum DriverEnum
        driverNone = 0
        driverB = 1
        driverC = 2
        driverD = 4
        driverE = 8
    End Enum

    Public Enum GenderEnum
        Female = 1
        Male = 2
    End Enum

    Enum UserTypeEnum
        Staff = 1
        Salesman = 2
        Provider = 3
        HeadOfSales = 4
    End Enum

    Public Overrides Function ToString() As String
        If Type = UserTypeEnum.Provider Then
            Return Firstname
        Else
            Return LastName & ", " & Firstname
        End If
    End Function

    Public Property Firstname() As String
        Get
            Return _firstName
        End Get
        Set(ByVal value As String)
            _firstName = value
        End Set
    End Property

    Public ReadOnly Property CV() As List(Of cCVEntry)
        Get
            Return Database.GetCVForStaff(DatabaseID)
        End Get
    End Property

    Public ReadOnly Property Age() As Byte
        Get
            If Birthday.ToOADate = 0 Then Return 0
            Return Year(Now) - Year(Birthday) + (Birthday.AddYears(Year(Now) - Year(Birthday)) > Now)
        End Get
    End Property

    Public Property DBRowVersion() As String
        Get

        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Friend Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim XMLNode As Xml.XmlElement = XMLDoc.CreateElement("Staff")

        XMLNode.SetAttribute("DatabaseID", DatabaseID)

        Return XMLNode
    End Function

End Class
