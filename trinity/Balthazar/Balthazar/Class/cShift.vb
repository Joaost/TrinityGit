Imports System
Imports System.Xml

Public Class cShift
    Public ID As String = Guid.NewGuid.ToString
    Public Name As String
    Public Description As String
    Public Roles As cRoles
    Public DatabaseID As Integer = -1
    Private _startMam As Integer = 9 * 60
    Private _endMam As Integer = 18 * 60
    Private _main As cEvent

    Public ReadOnly Property Length() As Single
        Get
            Return _endMam - _startMam
        End Get
    End Property

    Public Property StartTime() As String
        Get
            Return Helper.Mam2Time(_startMam)
        End Get
        Set(ByVal value As String)
            _startMam = Helper.Time2Mam(value)
        End Set
    End Property

    Public Property EndTime() As String
        Get
            Return Helper.Mam2Time(_endMam)
        End Get
        Set(ByVal value As String)
            _endMam = Helper.Time2Mam(value)
        End Set
    End Property

    Public Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim TmpNode As Xml.XmlElement
        TmpNode = XMLDoc.CreateElement("Shift")
        TmpNode.SetAttribute("ID", ID)
        TmpNode.SetAttribute("Name", Name)
        TmpNode.SetAttribute("Description", Description)
        TmpNode.SetAttribute("StartMaM", _startMam)
        TmpNode.SetAttribute("EndMaM", _endMam)
        TmpNode.SetAttribute("DatabaseID", DatabaseID)
        TmpNode.AppendChild(Roles.CreateXML(XMLDoc.CreateElement("Roles"), XMLDoc))
        Return TmpNode
    End Function

    Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        ID = Node.GetAttribute("ID")
        Name = Node.GetAttribute("Name")
        Description = Node.GetAttribute("Description")
        _startMam = Node.GetAttribute("StartMaM")
        _endMam = Node.GetAttribute("EndMaM")
        DatabaseID = Node.GetAttribute("DatabaseID")
        Roles.CreateFromXML(Node.GetElementsByTagName("Roles").Item(0))
    End Sub

    Public Sub New(ByVal Main As cEvent)
        _main = Main
        Roles = New cRoles(Main)
        CheckAndCreateRoles()
    End Sub

    Friend Sub CheckAndCreateRoles()
        Dim Keep As New Dictionary(Of String, cRole)

        For Each TmpRole As cRole In Roles
            Keep.Add(TmpRole.ID, TmpRole)
        Next
        Roles.Clear()
        For Each TmpRole As cRole In _main.Roles
            If Keep.ContainsKey(TmpRole.ID) Then
                Roles.Add(Keep(TmpRole.ID))
            Else
                Dim NewRole As New cRole(_main)
                NewRole.ID = TmpRole.ID
                NewRole.Name = TmpRole.Name
                NewRole.Description = TmpRole.Description
                NewRole.Category = TmpRole.Category
                Roles.Add(NewRole)
            End If
        Next
    End Sub

    Public ReadOnly Property CTC() As Decimal
        Get
            Dim TmpCost As Decimal = 0
            For Each TmpRole As cRole In Roles
                TmpCost += TmpRole.CTCPerHour * (Length / 60)
            Next
        End Get
    End Property

    Public ReadOnly Property ActualCost() As Decimal
        Get
            Dim TmpCost As Decimal = 0
            For Each TmpRole As cRole In Roles
                TmpCost += TmpRole.ActualCost * (Length / 60)
            Next
        End Get
    End Property

    Public Sub New(ByVal Main As cEvent, ByVal Node As Xml.XmlElement)
        _main = Main
        Roles = New cRoles(Main)
        CreateFromXML(Node)
        CheckAndCreateRoles()
    End Sub

    Friend ReadOnly Property StartMam() As Integer
        Get
            Return _startMam
        End Get
    End Property

    Friend ReadOnly Property EndMam() As Integer
        Get
            Return _endMam
        End Get
    End Property

End Class
