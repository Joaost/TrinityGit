Imports System
Imports System.Xml

Public Class cBudget
    Private _name As String

    Public StaffCosts As cStaffCategories
    Public MaterialCosts As New List(Of cCost)
    Public PlanningCosts As New List(Of cHourCost)
    Public LogisticsCosts As New List(Of cCost)

    Private _main As cEvent
    Private _parentColl As Dictionary(Of String, cBudget)

    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            If Not _parentColl Is Nothing Then
                _parentColl.Add(value, Me)
                If _parentColl.ContainsKey(_name) Then
                    _parentColl.Remove(_name)
                End If
            End If
            _name = value
        End Set
    End Property

    Public Sub New(ByVal Main As cEvent, ByVal ParentColl As Dictionary(Of String, cBudget))
        _main = Main
        StaffCosts = New cStaffCategories(_main)
        _parentColl = ParentColl
    End Sub

    Friend Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        _name = Node.GetAttribute("Name")
        StaffCosts.CreateFromXML(Node.GetElementsByTagName("StaffCosts")(0))
        For Each TmpNode As XmlElement In Node.GetElementsByTagName("PlanningCosts")(0).ChildNodes
            Dim TmpCost As New cHourCost(TmpNode)
            PlanningCosts.Add(TmpCost)
        Next
        For Each TmpNode As XmlElement In Node.GetElementsByTagName("MaterialCosts")(0).ChildNodes
            Dim TmpCost As New cCost(TmpNode)
            MaterialCosts.Add(TmpCost)
        Next
        For Each TmpNode As XmlElement In Node.GetElementsByTagName("LogisticsCosts")(0).ChildNodes
            Dim TmpCost As New cCost(TmpNode)
            LogisticsCosts.Add(TmpCost)
        Next

    End Sub

    Friend Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim TmpNode As Xml.XmlElement = XMLDoc.CreateElement("Budget")
        Dim XMLNodes As Xml.XmlElement

        TmpNode.SetAttribute("Name", _name)

        XMLNodes = XMLDoc.CreateElement("StaffCosts")
        XMLNodes = StaffCosts.CreateXML(XMLNodes, XMLDoc)
        TmpNode.AppendChild(XMLNodes)

        XMLNodes = XMLDoc.CreateElement("PlanningCosts")
        For Each TmpCost As cHourCost In PlanningCosts
            XMLNodes.AppendChild(TmpCost.CreateXML(XMLDoc))
        Next
        TmpNode.AppendChild(XMLNodes)

        XMLNodes = XMLDoc.CreateElement("MaterialCosts")
        For Each TmpCost As cCost In MaterialCosts
            XMLNodes.AppendChild(TmpCost.CreateXML(XMLDoc))
        Next
        TmpNode.AppendChild(XMLNodes)

        XMLNodes = XMLDoc.CreateElement("LogisticsCosts")
        For Each TmpCost As cCost In LogisticsCosts
            XMLNodes.AppendChild(TmpCost.CreateXML(XMLDoc))
        Next
        TmpNode.AppendChild(XMLNodes)

        Return TmpNode
    End Function
End Class
