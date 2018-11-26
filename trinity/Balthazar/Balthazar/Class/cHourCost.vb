Imports System
Imports System.Xml

Public Class cHourCost
    Inherits cCost

    Public Hours As Single = 0
    Public CostPerHourCTC As Decimal = 0

    Public Overrides Property CTC() As Decimal
        Get
            Return Hours * CostPerHourCTC
        End Get
        Set(ByVal value As Decimal)

        End Set
    End Property

    Friend Overrides Function CreateXML(ByVal XMLDoc As System.Xml.XmlDocument) As System.Xml.XmlElement
        Dim TmpNode As Xml.XmlElement = MyBase.CreateXML(XMLDoc)
        TmpNode.SetAttribute("Hours", Hours)
        TmpNode.SetAttribute("CostPerHourCTC", CostPerHourCTC)
        Return TmpNode
    End Function

    Friend Overrides Sub CreateFromXML(ByVal Node As System.Xml.XmlElement)
        MyBase.CreateFromXML(Node)
        Hours = Node.GetAttribute("Hours")
        CostPerHourCTC = Node.GetAttribute("CostPerHourCTC")
    End Sub

    Public Sub New(ByVal CreateFromNode As Xml.XmlElement)
        CreateFromXML(CreateFromNode)
    End Sub

    Public Sub New()

    End Sub
End Class
