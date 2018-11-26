Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cAddedValues
        Implements Collections.IEnumerable

        Private mCol As New Collection
        Private Parent As cBookingType
        Private Main As cKampanj

        Public WriteOnly Property Bookingtype()
            Set(ByVal value)
                Parent = value
            End Set
        End Property

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument, ByVal WeekIndexNumber As Integer) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            Dim XMLAV As Xml.XmlElement

            'if = -1 we have a bookingtype AV, if not -1 we have a week AV
            If WeekIndexNumber = -1 Then
                For Each TmpAV As Trinity.cAddedValue In Me
                    XMLAV = xmlDoc.CreateElement("AddedValue")
                    XMLAV.SetAttribute("ID", TmpAV.ID)
                    XMLAV.SetAttribute("Name", TmpAV.Name)
                    XMLAV.SetAttribute("IndexGross", TmpAV.IndexGross)
                    XMLAV.SetAttribute("IndexNet", TmpAV.IndexNet)
                    XMLAV.SetAttribute("ShowIn", TmpAV.ShowIn)
                    XMLAV.SetAttribute("UseThis", TmpAV.UseThis)
                    colXml.AppendChild(XMLAV)
                Next TmpAV
            Else
                For Each TmpAV As Trinity.cAddedValue In Me
                    XMLAV = xmlDoc.CreateElement("AddedValue")
                    XMLAV.SetAttribute("ID", TmpAV.ID)
                    XMLAV.SetAttribute("Amount", TmpAV.Amount(WeekIndexNumber))
                    XMLAV.SetAttribute("UseThis", TmpAV.UseThis)
                    colXml.AppendChild(XMLAV)
                Next
            End If


            Return True
            Exit Function

On_Error:
            If WeekIndexNumber = -1 Then
                errorMessege.Add("Error saving AddedValues for bookingtype " & Parent.ToString)
            Else
                errorMessege.Add("Error saving AddedValues for week index " & WeekIndexNumber)
            End If

            Return False
        End Function

        Public Function Add(ByVal Name As String, Optional ByVal ID As String = "") As cAddedValue

            Dim TmpAV As New cAddedValue(Main, mCol)

            If ID = "" Then ID = CreateGUID()
            TmpAV.ID = ID
            TmpAV.Name = Name
            TmpAV.Bookingtype = Parent

            mCol.Add(TmpAV, ID)
            Add = mCol(ID)

        End Function

        Default Public ReadOnly Property Item(ByVal Index As Integer) As cAddedValue
            Get
                '    used when referencing an element in the collection
                '    vntIndexKey contains either the Index or Key to the collection,
                '    this is why it is declared as a Variant
                '    Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
                On Error GoTo Item_Error


                Return mCol(Index)

                On Error GoTo 0
                Exit Property

Item_Error:
                Dim e As Long
                e = Err.Number

                Err.Raise(e, "cAddedvalues", "Unknown Index (" & Index & ")")
            End Get
        End Property

        Public Function FindByName(Name As String) As cAddedValue
            Dim _list As IEnumerable(Of cAddedValue) = (From _av As cAddedValue In mCol Where _av.Name = Name Select _av)
            If _list.Count > 0 Then
                Return _list.First
            End If
            Return Nothing
        End Function

        Default Public ReadOnly Property Item(ByVal key As String) As cAddedValue
            Get
                '    used when referencing an element in the collection
                '    vntIndexKey contains either the Index or Key to the collection,
                '    this is why it is declared as a Variant
                '    Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
                On Error GoTo Item_Error


                If mCol.Contains(key) Then
                    Return mCol(key)
                Else
                    Return Nothing
                End If

                On Error GoTo 0
                Exit Property

Item_Error:
                Dim e As Long
                e = Err.Number

                Err.Raise(e, "cAddedvalues", "Unknown Index (" & key & ")")
            End Get
        End Property

        Public ReadOnly Property Count() As Long
            'used when retrieving the number of elements in the
            'collection. Syntax: Debug.Print x.Count
            Get
                Count = mCol.Count
            End Get
        End Property

        Public Sub Remove(ByVal vntIndexKey As Object)
            'used when removing an element from the collection
            'vntIndexKey contains either the Index or Key, which is why
            'it is declared as a Variant
            'Syntax: x.Remove(xyz)

            Try
                mCol.Remove(vntIndexKey)
            Catch
                Debug.Print("Tried to remove an added value with a key that does not exist.")
            End Try
        End Sub

        Public Sub Clear()
            mCol = New Collection
        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            GetEnumerator = mCol.GetEnumerator
        End Function

        Public Sub New(ByVal MainObject As cKampanj)
            mCol = New Collection
            Main = MainObject
        End Sub

        Protected Overrides Sub Finalize()
            mCol = Nothing
            MyBase.Finalize()
        End Sub

        Public Function Contains(ByVal ID As String) As Boolean
            Return mCol.Contains(ID)
        End Function

    End Class
End Namespace
