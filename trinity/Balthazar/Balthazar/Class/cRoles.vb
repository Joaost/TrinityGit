Imports System
Imports System.Xml

Public Class cRoles
    Implements IEnumerable

    Private _col As New Collection
    Private _main As cEvent

    Public Function Add(ByVal Name As String, Optional ByVal ID As String = "") As cRole
        Dim _role As New cRole(_main)
        Dim _id As String = Guid.NewGuid.ToString
        If ID <> "" Then
            _id = ID
        End If

        _role.ID = _id
        _role.Name = Name
        _col.Add(_role, _id)

        Return _role
    End Function

    Public Sub Add(ByVal Role As cRole)
        _col.Add(Role, Role.ID)
    End Sub

    Default Public ReadOnly Property Item(ByVal indexKey As Object) As cRole
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

    Friend Function CreateXML(ByVal XMLParentNode As Xml.XmlElement, ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        For Each TmpRole As cRole In _col
            XMLParentNode.AppendChild(TmpRole.CreateXML(XMLDoc))
        Next
        Return XMLParentNode
    End Function

    Friend Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        Dim ChildNode As Xml.XmlElement
        _col.Clear()
        ChildNode = Node.FirstChild
        While Not ChildNode Is Nothing
            Dim TmpRole As cRole = New cRole(_main, ChildNode)
            _col.Add(TmpRole, TmpRole.ID)
            ChildNode = ChildNode.NextSibling
        End While
    End Sub

    Public Sub New(ByVal Main As cEvent)
        _main = Main
    End Sub
End Class
