Imports System
Imports System.Xml

Public Class cCost
    Private _ctc As Decimal
    Private _actualCost As Decimal
    Private _description As String
    Private _name As String

    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Public Overridable Property CTC() As Decimal
        Get
            Return _ctc
        End Get
        Set(ByVal value As Decimal)
            _ctc = value
        End Set
    End Property

    Public Overridable Property ActualCost() As Decimal
        Get
            Return _actualCost
        End Get
        Set(ByVal value As Decimal)
            _actualCost = value
        End Set
    End Property

    Public ReadOnly Property Profit() As Decimal
        Get
            Return CTC - ActualCost
        End Get
    End Property

    Friend Overridable Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim TmpNode As Xml.XmlElement = XMLDoc.CreateElement("Cost")
        TmpNode.SetAttribute("Name", Name)
        TmpNode.SetAttribute("Description", Description)
        TmpNode.SetAttribute("CTC", CTC)
        TmpNode.SetAttribute("ActualCost", ActualCost)
        Return TmpNode
    End Function

    Friend Overridable Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        Name = Node.GetAttribute("Name")
        Description = Node.GetAttribute("Description")
        ActualCost = Node.GetAttribute("ActualCost")
        If Node.GetAttribute("CTC") <> "" Then _ctc = Node.GetAttribute("CTC")
    End Sub

    Public Sub New(ByVal CreateFromNode As Xml.XmlElement)
        CreateFromXML(CreateFromNode)
    End Sub

    Public Sub New()

    End Sub
End Class
