Imports System
Imports System.Xml

Public Class cStaffCategories
    Implements IEnumerable

    Private _col As New Collection
    Private _main As cEvent

    Public Function Add(ByVal Name As String, Optional ByVal ID As String = "") As cStaffCategory
        Dim _category As New cStaffCategory(_main)
        Dim _id As String = Guid.NewGuid.ToString
        If ID <> "" Then
            _id = ID
        End If

        _category.ID = _id
        _category.Name = Name
        _col.Add(_category, _id)

        Return _category
    End Function

    Public Sub Add(ByVal Category As cStaffCategory)
        _col.Add(Category, Category.ID)
    End Sub

    Default Public ReadOnly Property Item(ByVal indexKey As Object) As cStaffCategory
        Get
            Return _col(indexKey)
        End Get
    End Property

    Public Function Count() As Integer
        Return _col.Count
    End Function

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return _col.GetEnumerator
    End Function

    Public Sub Clear()
        _col.Clear()
    End Sub

    Public Sub Remove(ByVal Key As String)
        _col.Remove(Key)
    End Sub

    Public Sub Remove(ByVal Index As Integer)
        _col.Remove(Index)
    End Sub

    Public Function Contains(ByVal Key As String)
        If Key Is Nothing Then Return False
        Return _col.Contains(Key)
    End Function

    Public Sub New(ByVal Main As cEvent)
        _main = Main
    End Sub

    Friend Function CreateXML(ByVal XMLParentNode As Xml.XmlElement, ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        For Each TmpCat As cStaffCategory In _col
            Dim XMLNode As Xml.XmlElement = XMLDoc.CreateElement("Category")
            XMLNode.SetAttribute("ID", TmpCat.ID)
            XMLNode.SetAttribute("Name", TmpCat.Name)
            XMLNode.SetAttribute("Description", TmpCat.Description)
            XMLNode.SetAttribute("Quantity", TmpCat.Quantity)
            XMLNode.SetAttribute("HoursPerDay", TmpCat.HoursPerDay)
            XMLNode.SetAttribute("Days", TmpCat.Days)
            XMLNode.SetAttribute("CostPerHourActual", TmpCat.CostPerHourActual)
            XMLNode.SetAttribute("CostPerHourCTC", TmpCat.CostPerHourCTC)
            XMLParentNode.AppendChild(XMLNode)
        Next
        Return XMLParentNode
    End Function

    Friend Sub CreateFromXML(ByVal XMLNode As Xml.XmlElement)
        Dim ChildNode As Xml.XmlElement
        _col.Clear()
        ChildNode = XMLNode.FirstChild
        While Not ChildNode Is Nothing
            Dim TmpCat As New cStaffCategory(_main)
            TmpCat.ID = ChildNode.GetAttribute("ID")
            TmpCat.Name = ChildNode.GetAttribute("Name")
            TmpCat.Description = ChildNode.GetAttribute("Description")
            TmpCat.Quantity = ChildNode.GetAttribute("Quantity")
            TmpCat.HoursPerDay = ChildNode.GetAttribute("HoursPerDay")
            TmpCat.Days = ChildNode.GetAttribute("Days")
            TmpCat.CostPerHourActual = ChildNode.GetAttribute("CostPerHourActual")
            TmpCat.CostPerHourCTC = ChildNode.GetAttribute("CostPerHourCTC")
            _col.Add(TmpCat, TmpCat.ID)
            ChildNode = ChildNode.NextSibling
        End While
    End Sub

End Class
