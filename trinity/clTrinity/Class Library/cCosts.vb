Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cCosts
        Implements Collections.IEnumerable
        'cCosts is a container class for Costs

        'local variable to hold collection
        Private mCol As Collection
        Private Main As cKampanj

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            Dim xmlCosts As Xml.XmlElement
            For Each TmpCost As Trinity.cCost In Me
                xmlCosts = xmlDoc.CreateElement("Node")
                TmpCost.GetXML(xmlCosts, errorMessege, xmlDoc)
                colXml.AppendChild(xmlCosts)
            Next

            Exit Function

On_Error:
            colXml.AppendChild(xmlDoc.CreateComment("ERROR (" & Err.Number & "): " & Err.Description))
            errorMessege.Add("Error saving Costs collection")
            Resume Next

        End Function

        Public Function Add(ByVal Name As String, ByVal CostType As cCost.CostTypeEnum, ByVal Amount As Single, ByVal CountCostOn As Object, ByVal MarathonID As String) As cCost
            'create a new object
            Dim objNewMember As cCost
            Dim e As Long
            Dim ID As String

            objNewMember = New cCost(Main)

            ID = CreateGUID()

            'set the properties passed into the method
            objNewMember.ID = ID
            objNewMember.CostName = Name
            objNewMember.Amount = Amount
            objNewMember.CostType = CostType
            objNewMember.CountCostOn = CountCostOn

            objNewMember.MarathonID = MarathonID

            On Error Resume Next
            mCol.Add(objNewMember, ID)
            e = Err.Number
            On Error GoTo 0


            'return the object created
            If e = 0 Then
                Add = objNewMember
            Else
                Add = mCol(ID)
            End If
            objNewMember = Nothing


        End Function

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cCost
            Get
                Dim e As Long

                '    used when referencing an element in the collection
                '    vntIndexKey contains either the Index or Key to the collection,
                '    this is why it is declared as a Variant
                '    Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
                On Error GoTo Item_Error

                Item = mCol(vntIndexKey)

                On Error GoTo 0
                Exit Property

Item_Error:
                e = Err.Number

                Err.Raise(e, "cCosts", "Unknown Cost (" & vntIndexKey & ")")
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


            mCol.Remove(vntIndexKey)
        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return mCol.GetEnumerator
        End Function

        Public Sub New(ByVal MainObject As cKampanj)
            mCol = New Collection
            Main = MainObject
        End Sub

        Protected Overrides Sub Finalize()
            mCol = Nothing
            MyBase.Finalize()
        End Sub
    End Class
End Namespace
