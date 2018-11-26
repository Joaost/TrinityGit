Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cPricelistTargets
        Implements Collections.IEnumerable

        'local variable to hold collection
        Private mCol As Collection
        Private Main As cKampanj
        Private mvarBookingtype As cBookingType

        Public Sub Clear()
            mCol.Clear()
        End Sub

        Public Function TargetNameList() As List(Of String)
            Return (From tmpTarget As cPricelistTarget In mCol Select tmpTarget.TargetName).ToList
        End Function

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided
            'it will return True of succeded and false if failed

            On Error GoTo On_Error

            Dim XMLBuyTarget As Xml.XmlElement
            Dim XMLTarget As Xml.XmlElement
            Dim XMLPeriod As Xml.XmlElement
            Dim XMLPricelistPeriods As Xml.XmlElement
            Dim XMLTmpNode As Xml.XmlElement
            Dim XMLTmpNode2 As Xml.XmlElement

            For Each TmpPLTarget As Trinity.cPricelistTarget In Me
                XMLBuyTarget = xmlDoc.CreateElement("BuyingTarget")
                TmpPLTarget.GetXML(XMLBuyTarget, errorMessege, xmlDoc)
                colXml.AppendChild(XMLBuyTarget)
            Next TmpPLTarget

            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving PriceListTarget list for " & mvarBookingtype.ToString)
            Return False
        End Function

        Friend Property Bookingtype() As cBookingType
            Get
                Return mvarBookingtype
            End Get
            Set(ByVal value As cBookingType)
                If value Is Nothing Then
                    Throw New Exception(Me.ToString & " booking type set to nothing")
                End If
                Dim TmpTarget As cPricelistTarget

                mvarBookingtype = value
                For Each TmpTarget In mCol
                    TmpTarget.Bookingtype = value
                Next
            End Set
        End Property

        Friend WriteOnly Property MainObject() As cKampanj
            Set(ByVal value As cKampanj)
                Dim TmpTarget As cPricelistTarget

                Main = value
                For Each TmpTarget In mCol
                    TmpTarget.MainObject = value
                Next
            End Set
        End Property

        Public Function Add(ByVal TargetName As String, ByVal BT As Trinity.cBookingType, Optional ByVal Target As cTarget = Nothing, Optional ByVal UniSize As Long = 0, Optional ByVal UniSizeNat As Long = 0, Optional ByVal CalcCPP As Boolean = False) As cPricelistTarget
            'Optional ByVal CPP As Single = 0

            If TargetName.IndexOf(" ") = 1 Then
                TargetName = TargetName.Remove(1, 1)
            End If
            'create a new object
            Dim objNewMember As cPricelistTarget
            'On Error GoTo Add_Error

            objNewMember = New cPricelistTarget(Main, BT)

            objNewMember.CalcCPP = CalcCPP
            'objNewMember.CPP = CPP
            If Not Target Is Nothing Then
                objNewMember.Target = Target
            End If
            objNewMember.TargetName = TargetName
            'objNewMember.UniSize = UniSize
            'objNewMember.UniSizeNat = UniSizeNat
            If BT IsNot Nothing Then
                objNewMember.Bookingtype = BT
            Else
                objNewMember.Bookingtype = mvarBookingtype
            End If

            If Main Is Nothing Then
                objNewMember = objNewMember
            End If

            'set the properties passed into the method
            Try
                mCol.Add(objNewMember, TargetName)
            Catch

            End Try


            'return the object created
            Add = objNewMember
            objNewMember = Nothing


            'On Error GoTo 0
            Exit Function

Add_Error:

            'Err.Raise(Err.Number, "cPriceListTargets: Add", Err.Description)


        End Function

        Default Public Property Item(ByVal Key As String) As cPricelistTarget
            Get
                On Error GoTo Item_Error

                'used when referencing an element in the collection
                'vntIndexKey contains either the Index or Key to the collection,
                'this is why it is declared as a Variant
                'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)

                If mCol.Contains(Key) Then
                    Return mCol(Key)
                Else
                    Dim alternateKey As String = Key.Replace(" ", "")
                    If mCol.Contains(alternateKey) Then
                        Return mCol(alternateKey)
                    Else
                        Return Nothing
                    End If

                End If

                'If Item.CalcCPP Then
                '    Item.CalculateCPP()
                'End If

                On Error GoTo 0
                Exit Property

Item_Error:
                Item = Nothing
            End Get
            Set(ByVal value As cPricelistTarget)
                If Not mCol(Key) Is value Then
                    If mCol(Key).TargetName = value.TargetName Then
                        mCol.Add(value, "<temp>", value.TargetName)
                        mCol.Remove(value.TargetName)
                        mCol.Add(value, value.TargetName, "<temp>")
                        mCol.Remove("<temp>")
                    Else
                        Try
                            mCol.Add(value, value.TargetName, Key)
                        Catch ex As Exception
                            Throw New Exception("That target is already used.")
                        End Try
                    End If
                End If
            End Set
        End Property

        Default Public Property Item(ByVal Index As Integer) As cPricelistTarget
            Get
                On Error GoTo Item_Error

                'used when referencing an element in the collection
                'vntIndexKey contains either the Index or Key to the collection,
                'this is why it is declared as a Variant
                'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
                If Index <= mCol.Count Then
                    Return mCol(Index)
                Else
                    Return Nothing
                End If

                'If Item.CalcCPP Then
                '    Item.CalculateCPP()
                'End If

                On Error GoTo 0
                Exit Property

Item_Error:
                Item = Nothing
            End Get
            Set(ByVal value As cPricelistTarget)
                If Not mCol(Index) Is value Then
                    If mCol(Index).TargetName = value.TargetName Then
                        mCol.Add(value, "<temp>", value.TargetName)
                        mCol.Remove(value.TargetName)
                        mCol.Add(value, value.TargetName, "<temp>")
                        mCol.Remove("<temp>")
                    Else
                        Try
                            mCol.Add(value, value.TargetName, Index)
                            mCol.Remove(Index + 1)
                        Catch ex As Exception
                            Throw New Exception("That target is already used.")
                        End Try
                    End If
                End If
            End Set
        End Property

        Public ReadOnly Property Contains(ByVal vntIndexKey As Object) As Boolean
            Get
                Return mCol.Contains(vntIndexKey)
            End Get
        End Property


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

            If DirectCast(mCol(vntIndexKey), cPricelistTarget).IsUserEditable Then
                mCol.Remove(vntIndexKey)
            Else
                Windows.Forms.MessageBox.Show("Can't remove this target - it is standard.")
            End If
        End Sub

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            GetEnumerator = mCol.GetEnumerator
        End Function

        Public Sub New(ByVal Main As cKampanj)
            mCol = New Collection
            MainObject = Main
        End Sub

        Protected Overrides Sub Finalize()
            mCol = Nothing
            MyBase.Finalize()
        End Sub

    End Class
End Namespace