Imports System.Xml

Namespace Trinity
    Public Class cPricelistPeriods
        Implements Collections.IEnumerable

        Private mCol As New System.Collections.Specialized.HybridDictionary()
        Private mvarBookingtype As Trinity.cBookingType
        Friend ParentObject As Object

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            Dim XMLPeriod As XmlElement
            For Each TmpPeriod As cPricelistPeriod In Me
                XMLPeriod = xmlDoc.CreateElement("Period")
                TmpPeriod.GetXML(XMLPeriod, errorMessege, xmlDoc)
                colXml.AppendChild(XMLPeriod)
            Next

            Exit Function

On_Error:
            colXml.AppendChild(xmlDoc.CreateComment("ERROR (" & Err.Number & "): " & Err.Description))
            errorMessege.Add("Error saving PriceListPeriod collection")
            Resume Next
        End Function

        Public Function Add(ByVal Name As String, Optional ByVal ID As String = "") As cPricelistPeriod

            Dim TmpPeriod As cPricelistPeriod
            TmpPeriod = New cPricelistPeriod(Campaign, Me)

            'sets ID if there is no one set
            If ID = "" Then ID = CreateGUID()
            TmpPeriod.ID = ID
            TmpPeriod.Name = Name

            mCol.Add(ID, TmpPeriod)

            Add = mCol(ID)
        End Function

        Default Public ReadOnly Property Item(ByVal key As String) As cPricelistPeriod
            Get
                Dim e As Long

                '    used when referencing an element in the collection
                '    vntIndexKey contains either the Index or Key to the collection,
                '    this is why it is declared as a Variant
                '    Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
                On Error GoTo Item_Error

                Item = mCol(key)

                On Error GoTo 0
                Exit Property

Item_Error:
                e = Err.Number

                Err.Raise(e, "cPricelistPeriods", "Unknown Index (" & key & ")")
                Resume

            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal index As Integer) As cPricelistPeriod
            Get
                Dim e As Long

                '    used when referencing an element in the collection
                '    vntIndexKey contains either the Index or Key to the collection,
                '    this is why it is declared as a Variant
                '    Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
                On Error GoTo Item_Error

                Item = mCol.Values(index)

                On Error GoTo 0
                Exit Property

Item_Error:
                e = Err.Number

                Err.Raise(e, "cPricelistPeriods", "Unknown Index (" & index & ")")
                Resume

            End Get
        End Property

        Public Sub New(ByVal Parent As cPricelistTarget)
            mCol = New System.Collections.Specialized.HybridDictionary()
            If Parent IsNot Nothing Then
                ParentObject = Parent
            Else
                Throw New Exception("Tried to set the parent of a cPricelistPeriods to Nothing")
            End If

        End Sub

        Public Sub New(ByVal Parent As cContractTarget)
            mCol = New System.Collections.Specialized.HybridDictionary()
            If Parent IsNot Nothing Then
                ParentObject = Parent
            Else
                Throw New Exception("Tried to set the parent of a cPricelistPeriods to Nothing")
            End If

        End Sub

        Public Function Exists(ByVal Index As String) As Boolean
            Return mCol.Contains(Index)
        End Function

        Public ReadOnly Property Count() As Long
            Get
                'used when retrieving the number of elements in the
                'collection. Syntax: Debug.Print x.Count
                Count = mCol.Count
            End Get
        End Property

        Public Sub Remove(ByVal vntIndexKey As Object)
            'used when removing an element from the collection
            'vntIndexKey contains either the Index or Key, which is why
            'it is declared as a Variant
            'Syntax: x.Remove(xyz)

            mCol.Remove(vntIndexKey)
        End Sub

        Public Sub Clear()
            mCol = New System.Collections.Specialized.HybridDictionary()
        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.Values.GetEnumerator
        End Function
    End Class
End Namespace
