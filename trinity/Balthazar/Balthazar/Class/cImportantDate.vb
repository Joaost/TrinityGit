Public Class cImportantDate

    Public ID As String = Guid.NewGuid.ToString

    Private _date As Date
    Public Property [Date]() As Date
        Get
            Return _date
        End Get
        Set(ByVal value As Date)
            If value.Hour = 0 Then
                value = value.AddHours(11)
            End If
            _date = value
        End Set
    End Property

    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Private _description As String
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Private _remindMe As Integer
    Public Property RemindMe() As Integer
        Get
            Return _remindMe
        End Get
        Set(ByVal value As Integer)
            _remindMe = value
        End Set
    End Property

    Friend Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim TmpNode As Xml.XmlElement
        TmpNode = XMLDoc.CreateElement("ImportantDate")
        TmpNode.SetAttribute("Date", _date)
        TmpNode.SetAttribute("ID", ID)
        TmpNode.SetAttribute("Name", _name)
        TmpNode.SetAttribute("Description", _description)
        TmpNode.SetAttribute("RemindMe", _remindMe)
        Return TmpNode
    End Function

    Friend Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        ID = Node.GetAttribute("ID")
        _date = Node.GetAttribute("Date")
        _name = Node.GetAttribute("Name")
        _description = Node.GetAttribute("Description")
        _remindMe = Node.GetAttribute("RemindMe")
    End Sub


    Public Sub New()

    End Sub

    Public Sub New(ByVal CreateFromNode As Xml.XmlElement)
        CreateFromXML(CreateFromNode)
    End Sub

End Class
