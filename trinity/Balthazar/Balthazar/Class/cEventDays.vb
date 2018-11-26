Imports System
Imports System.Xml

Public Class cEventDays
    Implements IEnumerable

    Private _col As New SortedList(Of Date, cEventDay)

    Private _main As cEvent

    Public Function Add(ByVal DayDate As Date) As cEventDay
        Dim TmpDay As New cEventDay(_main)

        TmpDay.DayDate = DayDate
        _col.Add(DayDate, TmpDay)
        Return TmpDay

    End Function

    Public Sub CreateFromDates(ByVal StartDate As Date, ByVal EndDate As Date, Optional ByVal KeepDays As Boolean = False)
        If StartDate > EndDate Then EndDate = StartDate
        Dim Keep As New SortedList(Of Date, cEventDay)
        If KeepDays Then
            For Each TmpDay As cEventDay In _col.Values
                If TmpDay.DayDate >= StartDate AndAlso TmpDay.DayDate <= EndDate Then
                    Keep.Add(TmpDay.DayDate, TmpDay)
                End If
            Next
        End If
        _col.Clear()
        For d As Long = StartDate.ToOADate To EndDate.ToOADate
            If Not KeepDays OrElse Not Keep.ContainsKey(Date.FromOADate(d)) Then
                Dim TmpDay As New cEventDay(_main)
                TmpDay.DayDate = Date.FromOADate(d)
                _col.Add(TmpDay.DayDate, TmpDay)
            ElseIf KeepDays Then
                _col.Add(Keep(Date.FromOADate(d)).DayDate, Keep(Date.FromOADate(d)))
            End If
        Next
    End Sub

    Default Public ReadOnly Property Item(ByVal Key As Date) As cEventDay
        Get
            Return _col(Key)
        End Get
    End Property

    Default Public ReadOnly Property Item(ByVal Index As Integer) As cEventDay
        Get
            Return _col.Values(Index)
        End Get
    End Property

    Public Function Count() As Integer
        Return _col.Count
    End Function

    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return _col.Values.GetEnumerator
    End Function

    Public Sub Remove(ByVal ID As String)
        _col.Remove(ID)
    End Sub

    Public Sub Remove(ByVal Index As Integer)
        _col.RemoveAt(Index)
    End Sub

    Public Sub Remove(ByVal Contact As cContact)
        _col.Remove(Contact.ID)
    End Sub

    Public Function CreateXML(ByVal XMLDoc As Xml.XmlDocument) As Xml.XmlElement
        Dim XMLNode As Xml.XmlElement = XMLDoc.CreateElement("Days")
        For Each TmpDay As cEventDay In _col.Values
            XMLNode.AppendChild(TmpDay.CreateXML(XMLDoc))
        Next
        Return XMLNode
    End Function

    Public Sub CreateFromXML(ByVal Node As Xml.XmlElement)
        Dim ChildNode As Xml.XmlElement
        _col.Clear()
        ChildNode = Node.FirstChild
        While Not ChildNode Is Nothing
            Dim TmpDay As cEventDay = New cEventDay(_main, ChildNode)
            _col.Add(TmpDay.DayDate, TmpDay)
            ChildNode = ChildNode.NextSibling
        End While
    End Sub

    Public Sub New(ByVal main As cEvent)
        _main = main
    End Sub
End Class
