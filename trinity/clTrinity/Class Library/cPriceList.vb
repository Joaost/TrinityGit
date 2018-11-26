Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Linq

Namespace Trinity
    Public Class cPricelist

        Private mvarStartDate As Long
        Private mvarEndDate As Long
        Private mvarTargets As cPricelistTargets ' a collection of PricelistTarget
        Private Main As cKampanj
        Private mvarBookingtype As cBookingType 'The booking type for the spot
        Private mvarBuyingUniverse As String


        Public Function GetXML(ByRef colXml As XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            Dim xmlTs As XmlElement
            xmlTs = xmlDoc.CreateElement("Targets")
            Me.Targets.GetXML(xmlTs, errorMessege, xmlDoc)
            colXml.AppendChild(xmlTs)

            colXml.SetAttribute("StartDate", Me.StartDate)
            colXml.SetAttribute("EndDate", Me.EndDate)
            colXml.SetAttribute("BuyingUniverse", Me.BuyingUniverse)

            Exit Function

On_Error:
            colXml.AppendChild(xmlDoc.CreateComment("ERROR (" & Err.Number & "): " & Err.Description))
            errorMessege.Add("Error saving Pricelist for" & Me.mvarBookingtype.ToString)
            Resume Next
        End Function

        Friend WriteOnly Property Bookingtype() As cBookingType
            Set(ByVal value As cBookingType)
                mvarBookingtype = value
            End Set
        End Property

        Friend WriteOnly Property MainObject() As cKampanj
            Set(ByVal value As cKampanj)
                mvarTargets.MainObject = value
                Main = value
            End Set
        End Property

        Public Property StartDate() As Long
            '---------------------------------------------------------------------------------------
            ' Procedure : StartDate
            ' DateTime  : 2003-07-11 11:47
            ' Author    : joho
            ' Purpose   : Returns/sets the date when this pricelist started to be used
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo StartDate_Error
                StartDate = mvarStartDate
                On Error GoTo 0
                Exit Property
StartDate_Error:
                Err.Raise(Err.Number, "cPriceList: StartDate", Err.Description)
            End Get
            Set(ByVal value As Long)
                On Error GoTo StartDate_Error
                mvarStartDate = StartDate
                On Error GoTo 0
                Exit Property
StartDate_Error:
                Err.Raise(Err.Number, "cPriceList: StartDate", Err.Description)
            End Set
        End Property

        Public Property EndDate() As Long
            '---------------------------------------------------------------------------------------
            ' Procedure : EndDate
            ' DateTime  : 2003-07-11 11:47
            ' Author    : joho
            ' Purpose   : Returns/sets the date when this pricelist will stop being used
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo EndDate_Error
                EndDate = mvarEndDate
                On Error GoTo 0
                Exit Property
EndDate_Error:
                Err.Raise(Err.Number, "cPriceList: EndDate", Err.Description)
            End Get
            Set(ByVal value As Long)
                On Error GoTo EndDate_Error
                mvarEndDate = value
                On Error GoTo 0
                Exit Property
EndDate_Error:
                Err.Raise(Err.Number, "cPriceList: EndDate", Err.Description)
            End Set
        End Property

        Public Property Targets() As cPricelistTargets
            '---------------------------------------------------------------------------------------
            ' Procedure : Targets
            ' DateTime  : 2003-07-11 11:52
            ' Author    : joho
            ' Purpose   : Pointer to a collection of cPriceListTarget
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Targets_Error
                Targets = mvarTargets
                On Error GoTo 0
                Exit Property
Targets_Error:
                Err.Raise(Err.Number, "cPriceList: Targets", Err.Description)
            End Get
            Set(ByVal value As cPricelistTargets)
                On Error GoTo Targets_Error
                mvarTargets = value
                On Error GoTo 0
                Exit Property
Targets_Error:
                Err.Raise(Err.Number, "cPriceList: Targets", Err.Description)
            End Set
        End Property

        Public Property BuyingUniverse() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : BuyingUniverse
            ' DateTime  : 2003-08-19 14:11
            ' Author    : joho
            ' Purpose   :
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo BuyingUniverse_Error
                BuyingUniverse = mvarBuyingUniverse
                On Error GoTo 0
                Exit Property
BuyingUniverse_Error:
                Err.Raise(Err.Number, "cPriceList: BuyingUniverse", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo BuyingUniverse_Error
                mvarBuyingUniverse = value
                On Error GoTo 0
                Exit Property
BuyingUniverse_Error:
                Err.Raise(Err.Number, "cPriceList: BuyingUniverse", Err.Description)
            End Set
        End Property



        Public Sub New(ByVal Main As cKampanj)
            mvarTargets = New cPricelistTargets(Main)
            MainObject = Main
        End Sub

        Public Sub Save(ByVal Path As String, ByVal commonDatabase As Boolean, SaveStandardTargets As Boolean)

            If commonDatabase Then
                DBReader.savePricelist(Me)
            Else
                Dim XMLDoc As New XmlDocument
                Dim Node As XmlNode
                Dim XMLPricelist As XmlElement
                Dim TmpTarget As cPricelistTarget
                Dim BTNode As XmlElement
                Dim TargetNode As XmlElement
                Dim PriceRows As XmlElement
                Dim Indexes As XmlElement

                Dim TargetsNode As XmlElement
                Dim IndexNode As XmlElement
                'Dim TmpIndex As Trinity.cIndex
                Dim TmpPeriod As Trinity.cPricelistPeriod
                Dim i As Integer

                If IO.File.Exists(Path) Then
                    XMLDoc.Load(Path)
                    XMLPricelist = XMLDoc.SelectSingleNode("Pricelist")
                    XMLPricelist.RemoveChild(XMLPricelist.SelectSingleNode("Price"))
                Else
                    XMLDoc.PreserveWhitespace = True
                    Node = XMLDoc.CreateProcessingInstruction("xml", "version='1.0'")
                    XMLDoc.AppendChild(Node)

                    Node = XMLDoc.CreateComment("Trinity pricelist. Created by Trinity.")
                    XMLDoc.AppendChild(Node)

                    Node = XMLDoc.CreateComment("Saved by user " & TrinitySettings.UserName & " at " & Now)
                    XMLDoc.AppendChild(Node)

                    XMLPricelist = XMLDoc.CreateElement("Pricelist")
                    XMLDoc.AppendChild(XMLPricelist)
                End If
                

                Dim _targetList As List(Of Trinity.cPricelistTarget) = (From _t As Trinity.cPricelistTarget In mvarBookingtype.Pricelist.Targets Where ((SaveStandardTargets AndAlso _t.StandardTarget) OrElse (Not SaveStandardTargets AndAlso Not _t.StandardTarget)) Select _t).ToList
                Try
                    BTNode = XMLDoc.CreateElement("Price")
                    BTNode.SetAttribute("Name", mvarBookingtype.Name)
                    BTNode.SetAttribute("AverageRating", mvarBookingtype.AverageRating)
                    For i = 0 To mvarBookingtype.Dayparts.Count - 1
                        If mvarBookingtype.Pricelist.Targets.Count > 0 Then
                            BTNode.SetAttribute("DP" & i + 1, mvarBookingtype.DefaultDaypart(i))
                        End If
                    Next
                    TargetsNode = XMLDoc.CreateElement("Targets")
                    For Each TmpTarget In _targetList
                        TargetNode = XMLDoc.CreateElement("Target")
                        TargetNode.SetAttribute("Target", TmpTarget.TargetName)
                        TargetNode.SetAttribute("AdedgeTarget", TmpTarget.Target.TargetName)
                        TargetNode.SetAttribute("TargetType", TmpTarget.Target.TargetType)
                        TargetNode.SetAttribute("TargetGroup", TmpTarget.Target.TargetGroup)
                        TargetNode.SetAttribute("CalcCPP", TmpTarget.CalcCPP)
                        TargetNode.SetAttribute("StandardTarget", TmpTarget.StandardTarget)
                        TargetNode.SetAttribute("MaxRatings", TmpTarget.MaxRatings)
                        'get the Pricelist periods
                        PriceRows = XMLDoc.CreateElement("PriceRows")
                        For Each TmpPeriod In TmpTarget.PricelistPeriods
                            IndexNode = XMLDoc.CreateElement("PriceRow")
                            IndexNode.SetAttribute("Name", TmpPeriod.Name)
                            IndexNode.SetAttribute("Price", TmpPeriod.Price(TmpPeriod.PriceIsCPP))
                            IndexNode.SetAttribute("isCPP", TmpPeriod.PriceIsCPP)
                            For i = 0 To mvarBookingtype.Dayparts.Count - 1
                                IndexNode.SetAttribute("PriceDP" & i, TmpPeriod.Price(TmpPeriod.PriceIsCPP, i))
                            Next
                            IndexNode.SetAttribute("FromDate", TmpPeriod.FromDate)
                            IndexNode.SetAttribute("ToDate", TmpPeriod.ToDate)
                            IndexNode.SetAttribute("TargetNat", TmpPeriod.TargetNat)
                            IndexNode.SetAttribute("TargetUni", TmpPeriod.TargetUni)

                            PriceRows.AppendChild(IndexNode)
                        Next
                        TargetNode.AppendChild(PriceRows)

                        'get the Pricelist periods
                        Indexes = XMLDoc.CreateElement("Indexes")
                        For Each TmpIndex As Trinity.cIndex In TmpTarget.Indexes
                            IndexNode = XMLDoc.CreateElement("Index")
                            IndexNode.SetAttribute("ID", TmpIndex.ID)
                            IndexNode.SetAttribute("Name", TmpIndex.Name)
                            IndexNode.SetAttribute("Index", TmpIndex.Index)
                            IndexNode.SetAttribute("IndexOn", TmpIndex.IndexOn)
                            IndexNode.SetAttribute("SystemGenerated", TmpIndex.SystemGenerated)
                            IndexNode.SetAttribute("FromDate", TmpIndex.FromDate.Date)
                            IndexNode.SetAttribute("ToDate", TmpIndex.ToDate.Date)

                            IndexNode.AppendChild(XMLDoc.CreateElement("Enhancements"))
                            For Each En As Trinity.cEnhancement In TmpIndex.Enhancements
                                Dim tmpEnXML As XmlElement = XMLDoc.CreateElement("Enhancement")
                                tmpEnXML.SetAttribute("ID", En.ID)
                                tmpEnXML.SetAttribute("Name", En.Name)
                                tmpEnXML.SetAttribute("Amount", En.Amount)
                                IndexNode.FirstChild.AppendChild(tmpEnXML)
                            Next

                            Indexes.AppendChild(IndexNode)
                        Next
                        TargetNode.AppendChild(Indexes)

                        TargetsNode.AppendChild(TargetNode)
                    Next
                    BTNode.AppendChild(TargetsNode)
                    XMLPricelist.AppendChild(BTNode)
                Catch ex As Exception
                    Windows.Forms.MessageBox.Show("There was an error saving booking type " & mvarBookingtype.ToString)
                    Exit Sub
                End Try
                If _targetList.Count > 0 Then XMLDoc.Save(Path)
            End If
        End Sub
    End Class
End Namespace