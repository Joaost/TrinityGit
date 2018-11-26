Imports System
Imports System.Xml

Public Class cLocations
    Implements IEnumerable

    Private _col As New Collection

    Private _main As cEvent

    Public Function Add(Optional ByVal Name As String = "", Optional ByVal FromDate As Date = Nothing, Optional ByVal ToDate As Date = Nothing) As cLocation
        Dim TmpLoc As New cLocation(_main)
        If FromDate.ToOADate > 0 AndAlso ToDate.ToOADate > 0 Then
            TmpLoc.Days.CreateFromDates(FromDate, ToDate)
        End If
        TmpLoc.Name = Name
        _col.Add(TmpLoc, TmpLoc.ID)
        Return TmpLoc
    End Function

    Default Public ReadOnly Property Item(ByVal indexKey As Object) As cLocation
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

    Public Sub Remove(ByVal ID As String)
        _col.Remove(ID)
    End Sub

    Public Sub Remove(ByVal Index As Integer)
        _col.Remove(Index)
    End Sub

    Public Sub Remove(ByVal Contact As cContact)
        _col.Remove(Contact.ID)
    End Sub

    Public Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim XMLNode As Xml.XmlElement = XMLDoc.CreateElement("Locations")
        For Each TmpLoc As cLocation In _col
            XMLNode.AppendChild(TmpLoc.CreateXML(XMLDoc))
        Next
        Return XMLNode
    End Function

    Public Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        Dim ChildNode As Xml.XmlElement
        _col.Clear()
        ChildNode = Node.FirstChild
        While Not ChildNode Is Nothing
            Dim TmpLoc As cLocation = New cLocation(_main, ChildNode)
            _col.Add(TmpLoc, TmpLoc.ID)
            ChildNode = ChildNode.NextSibling
        End While
    End Sub

    Public Sub New(ByVal main As cEvent)
        _main = main
    End Sub
End Class
