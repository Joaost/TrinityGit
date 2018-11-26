Imports System
Imports System.Xml

Public Class cRole
    Public ID As String
    Public Name As String = ""
    Public Description As String = ""
    Public Quantity As Integer = 0
    Public Category As cStaffCategory
    Public MinAge As Integer = 1
    Public MaxAge As Integer = 99
    Public Gender As GenderEnum = GenderEnum.Both
    Public Driver As DriverEnum
    Public PerDiem As Single = 0

    Private _main As cEvent

    Public Enum DriverEnum
        driverNone = 0
        driverB = 1
        driverC = 2
    End Enum

    Public Enum GenderEnum
        Male = 2
        Female = 1
        Both = 3
    End Enum

    Public Overrides Function ToString() As String
        Return Name
    End Function

    Friend Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim XMLNode As Xml.XmlElement = XMLDoc.CreateElement("Role")
        XMLNode.SetAttribute("ID", ID)
        XMLNode.SetAttribute("Name", Name)
        XMLNode.SetAttribute("Description", Description)
        XMLNode.SetAttribute("Quantity", Quantity)
        XMLNode.SetAttribute("MinAge", MinAge)
        XMLNode.SetAttribute("MaxAge", MaxAge)
        XMLNode.SetAttribute("Gender", Gender)
        XMLNode.SetAttribute("Driver", Driver)
        XMLNode.SetAttribute("PerDiem", PerDiem)
        If Category Is Nothing Then
            XMLNode.SetAttribute("CategoryID", "")
        Else
            XMLNode.SetAttribute("CategoryID", Category.ID)
        End If
        Return XMLNode
    End Function

    Friend Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        ID = Node.GetAttribute("ID")
        Name = Node.GetAttribute("Name")
        Description = Node.GetAttribute("Description")
        Quantity = Node.GetAttribute("Quantity")
        MinAge = Node.GetAttribute("MinAge")
        MaxAge = Node.GetAttribute("MaxAge")
        Gender = Node.GetAttribute("Gender")
        Driver = Node.GetAttribute("Driver")
        PerDiem = Node.GetAttribute("PerDiem")
        If Node.GetAttribute("CategoryID") <> "" Then
            Category = _main.StaffCategories(Node.GetAttribute("CategoryID"))
        End If
    End Sub

    Public Sub New(ByVal Main As cEvent, ByVal CreateFromNode As Xml.XmlElement)
        _main = Main
        CreateFromXML(CreateFromNode)
    End Sub

    Public ReadOnly Property CTCPerHour() As Decimal
        Get
            If Category IsNot Nothing Then
                Return Quantity * Category.CostPerHourCTC
            Else
                Return 0
            End If
        End Get
    End Property

    Public ReadOnly Property ActualCost() As Decimal
        Get
            If Category IsNot Nothing Then
                Return Quantity * Category.CostPerHourActual
            Else
                Return 0
            End If
        End Get
    End Property

    Public Sub New(ByVal Main As cEvent)
        _main = Main
    End Sub
End Class
