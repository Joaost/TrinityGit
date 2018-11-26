Imports System
Imports System.Xml

Public Class cContacts
    Implements IEnumerable

    Private _col As New Collection
    Private _roles As cRoles
    Public DefaultContact As cContact = Nothing
    Private _main As cEvent

    Public Function Add() As cContact
        Dim _contact As New cContact
        Dim _id As String = Guid.NewGuid.ToString

        _contact.ID = _id
        _col.Add(_contact, _id)

        Return _contact
    End Function

    Default Public ReadOnly Property Item(ByVal indexKey As Object) As cContact
        Get
            Return _col(indexKey)
        End Get
    End Property

    Default Public ReadOnly Property Item(ByVal Index As Integer) As cContact
        Get
            Return _col(Index)
        End Get
    End Property

    Public Function Count() As Integer
        Return _col.Count
    End Function

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return _col.GetEnumerator
    End Function

    Public ReadOnly Property Roles() As cRoles
        Get
            Return _roles
        End Get
    End Property

    Public Sub Remove(ByVal ID As String)
        _col.Remove(ID)
    End Sub

    Public Sub Remove(ByVal Index As Integer)
        _col.Remove(Index)
    End Sub

    Public Sub Remove(ByVal Contact As cContact)
        _col.Remove(Contact.ID)
    End Sub

    Friend Function CreateXML(ByVal XMLParentNode As Xml.XmlElement, ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        For Each TmpContact As cContact In _col
            Dim XMLNode As Xml.XmlElement = XMLDoc.CreateElement("Contact")
            XMLNode.SetAttribute("ID", TmpContact.ID)
            XMLNode.SetAttribute("Name", TmpContact.Name)
            XMLNode.SetAttribute("Role", TmpContact.Role)
            XMLNode.SetAttribute("PhoneNr", TmpContact.PhoneNr)
            XMLParentNode.AppendChild(XMLNode)
        Next
        Return XMLParentNode
    End Function

    Friend Sub CreateFromXML(ByVal XMLNode As Xml.XmlElement)
        Dim ChildNode As Xml.XmlElement
        _col.Clear()
        ChildNode = XMLNode.FirstChild
        While Not ChildNode Is Nothing
            Dim TmpContact As New cContact
            TmpContact.ID = ChildNode.GetAttribute("ID")
            TmpContact.Name = ChildNode.GetAttribute("Name")
            TmpContact.PhoneNr = ChildNode.GetAttribute("PhoneNr")
            TmpContact.Role = ChildNode.GetAttribute("Role")
            _col.Add(TmpContact, TmpContact.ID)
            ChildNode = ChildNode.NextSibling
        End While
    End Sub

    Public Sub New(ByVal Main As cEvent)
        _main = Main
        _roles = New cRoles(Main)
    End Sub
End Class
