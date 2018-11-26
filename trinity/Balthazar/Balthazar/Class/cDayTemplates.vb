Imports System
Imports System.Xml

Public Class cDayTemplates
    Implements IEnumerable

    Private _col As New Collection
    Private _main As cEvent

    Public Function Add() As cDayTemplate
        Dim TmpDay As New cDayTemplate(_main)

        _col.Add(TmpDay, TmpDay.ID)

        Return TmpDay

    End Function

    Default Public ReadOnly Property Item(ByVal Key As String) As cDayTemplate
        Get
            Return _col(Key)
        End Get
    End Property

    Default Public ReadOnly Property Item(ByVal Index As Integer) As cDayTemplate
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
        Dim XMLNode As Xml.XmlElement = XMLDoc.CreateElement("DayTemplates")
        For Each TmpDay As cDayTemplate In _col
            XMLNode.AppendChild(TmpDay.CreateXML(XMLDoc))
        Next
        Return XMLNode
    End Function

    Public Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        _col.Clear()
        Dim XmlNode As Xml.XmlElement = Node.FirstChild
        While Not XmlNode Is Nothing
            Dim TmpDay As New cDayTemplate(_main, XmlNode)
            _col.Add(TmpDay, TmpDay.ID)
            XmlNode = XmlNode.NextSibling
        End While
    End Sub

    Public Sub New(ByVal main As cEvent)
        _main = main
    End Sub
End Class
