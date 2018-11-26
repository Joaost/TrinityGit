Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic
Imports System.Xml

Namespace Trinity
    Public Class cBookingType
        Implements IDetectsProblems

        '---------------------------------------------------------------------------------------
        ' Module    : cBookingType
        ' DateTime  : 2003-07-10 15:37
        ' Author    : joho
        ' Purpose   : Used to handle each different bookingtype used for each channel
        '---------------------------------------------------------------------------------------



        Private mvarDefaultDaypart(0 To cKampanj.MAX_DAYPARTS) As Byte

        Private ParentColl As Collection
        Private mvarMain As cKampanj
        Private mvarParentChannel As cChannel

        Private mvarName As String = ""
        Private mvarDatabaseID As String = "NOT SAVED"
        'Private strID As String
        Private mvarShortname As String
        Private mvarBuyingTarget As cPricelistTarget
        Private mvarIndexMainTarget As Integer
        Private mvarIndexSecondTarget As Integer
        Private mvarIndexAllAdults As Integer
        Private mvarManualIndexes As Boolean 'Used in frmAllocate when doing natural delivery calculations. If this is true, then it's not done for this BT
        Private mvarIndexMainTargetStatus As IndexStatusEnum = IndexStatusEnum.Unchanged
        Private mvarIndexSecondTargetStatus As IndexStatusEnum = IndexStatusEnum.Unchanged
        Private mvarIndexAllAdultsStatus As IndexStatusEnum = IndexStatusEnum.Unchanged
        Private mvarRatecardCPPIsGross As Boolean = True

        Private mvarDaypartSplit(0 To 5) As Byte
        Private mvarWeeks As cWeeks
        Private mvarBookIt As Boolean
        'Private mvarGrossCPP As Single
        Private mvarAverageRating As Decimal
        Private mvarConfirmedNetBudget As Decimal
        Private mvarMarathonNetBudget As Decimal
        Private mvarBookingtype As Byte
        Private mvarContractNumber As String
        Private mvarOrderNumber As String
        Private mvarPriceList As cPricelist
        Private mvarIsVisible As Boolean
        Private mvarIsCompensation As Boolean
        Private mvarIsRBS As Boolean
        Private mvarIsSpecific As Boolean
        Private mvarIsPremium As Boolean
        Private mvarIsSponsorship As Boolean

        'Properties for TV4 Spotlight

        Private mvarBookingIdSpotlight As String
        Private mvarBookingUrlSpotlight As String
        Private mvarBookingConfirmationVersion As Integer
        Private mvarAgencyRefNo As String
        Private mvarCampaignRefNo As String

        'printing options
        Private mvarPrintDayparts As Boolean
        Private mvarPrintBookingCode As Boolean

        'Private mvarDiscount As Single
        'Private mvarNetCPT As Single
        'Private mvarNetCPP As Single
        'Private mvarIsEntered As EnteredEnum
        Private mvarIndexes As cIndexes
        Private mvarAddedValues As cAddedValues
        Private mvarFilmIndex(500) As Single
        Private mvarCompensations As New cCompensations(Me)

        Private mvarActualSpots As New Collection
        Private mvarVolume As Decimal

        Private mvarTotalTRP As Single = 0
        Private mvarTotalTRPChanged As Boolean = True
        Private mvarComments As String
        Private mvarMaxDiscount As Single = 1
        Private mvarIsLocked As Boolean
        Private mvarIndexDiffersFromChannel As Boolean = False

        Private WithEvents mvarNetValue As New cAggregate

        'Used for standard bookingtypes
        Private mvarWriteProtected As Boolean = False

        Private mvarUserEditable As Boolean = True

        ' TRP Changed event and delegate
        ' Added as of 16/8 // BF
        Public Event TRPChanged(ByVal sender As Object, ByVal e As WeekEventArgs)
        Private Sub _trpChanged(ByVal sender As Object, ByVal e As WeekEventArgs)
            ' Raise an event that this event has been raised to send it further up
            RaiseEvent TRPChanged(sender, e)
        End Sub

        ' Event for udateing pricelist to make sure to see progress in being done
        Public Event OnPriceListUpdate(ByVal sender As Object, ByVal e As EventArgs)

        Public Event FilmChanged(Film As cFilm)
        Public Event WeekChanged(Week As cWeek)
        Public Event BookingtypeChanged(Bookingtype As cBookingType)

        Private Sub _filmChanged(Film As cFilm)
            RaiseEvent FilmChanged(Film)
            RaiseEvent BookingtypeChanged(Me)
        End Sub

        Private Sub _weekChanged(Week As cWeek)
            RaiseEvent WeekChanged(Week)
            RaiseEvent BookingtypeChanged(Me)
        End Sub

        Public Property IsUserEditable As Boolean
            Get
                Return mvarUserEditable
            End Get
            Set(ByVal value As Boolean)
                mvarUserEditable = value
                RaiseEvent BookingtypeChanged(Me)
            End Set
        End Property

        'Enum to determine how the Targetindexes was set
        Public Enum IndexStatusEnum
            Unknown
            Unchanged
            NaturalDelivery
            EnteredByUser
        End Enum

        Private _specificSponsringPrograms As New List(Of String)

        'Variable to hold the replacement targets picked when targets could not be found
        Public PickedTargetsList As New Dictionary(Of String, Object)

        Public ReadOnly Property Main() As cKampanj
            Get
                Return mvarMain
            End Get
        End Property

        Public Property writeProtected() As Boolean
            Get
                Return mvarWriteProtected
            End Get
            Set(ByVal value As Boolean)
                mvarWriteProtected = value
                RaiseEvent BookingtypeChanged(Me)
            End Set
        End Property

        Private _pricelist As String
        Public Property PricelistName() As String
            Get
                Return _pricelist
            End Get
            Set(ByVal value As String)
                _pricelist = value
            End Set
        End Property

        Public Property BookingIdSpotlight() As String
            Get
                Return mvarBookingIdSpotlight
            End Get
            Set(ByVal value As String)
                mvarBookingIdSpotlight = value
            End Set
        End Property
        Public Property BookingUrlSpotlight() As String
            Get
                Return mvarBookingUrlSpotlight
            End Get
            Set(ByVal value As String)
                mvarBookingUrlSpotlight = value
            End Set
        End Property
        Public Property BookingConfirmationVersion() As Integer
            Get
                Return mvarBookingConfirmationVersion
            End Get
            Set(ByVal value As Integer)
                mvarBookingConfirmationVersion = value
            End Set
        End Property
        Public Property BookingAgencyRefNo() As String
            Get
                Return mvarAgencyRefNo
            End Get
            Set(ByVal value As String)
                mvarAgencyRefNo = value
            End Set
        End Property
        Public Property CampRefNo() As String
            Get
                Return mvarCampaignRefNo
            End Get
            Set(ByVal value As String)
                mvarCampaignRefNo = value
            End Set
        End Property
        Public Property IndexDiffersFromChannel() As Boolean
            Get
                Return mvarIndexDiffersFromChannel
            End Get
            Set(ByVal value As Boolean)
                mvarIndexDiffersFromChannel = value
                RaiseEvent BookingtypeChanged(Me)
            End Set
        End Property
        Public Property SpecificSponsringPrograms() As List(Of String)
            Get
                Return _specificSponsringPrograms
            End Get
            Set(ByVal value As List(Of String))
                _specificSponsringPrograms = value
                RaiseEvent BookingtypeChanged(Me)
            End Set
        End Property

        Private _enhancementFactor As Integer = 100
        Public Property EnhancementFactor() As Integer
            Get
                Return _enhancementFactor
            End Get
            Set(ByVal value As Integer)
                _enhancementFactor = value
                RaiseEvent BookingtypeChanged(Me)
            End Set
        End Property

        Public Property RatecardCPPIsGross() As Boolean
            Get
                Return mvarRatecardCPPIsGross
            End Get
            Set(ByVal value As Boolean)
                mvarRatecardCPPIsGross = value
                RaiseEvent BookingtypeChanged(Me)
            End Set
        End Property

        Public Property databaseID() As String
            Get
                On Error GoTo ID_Error
                databaseID = mvarDatabaseID
                On Error GoTo 0
                Exit Property
ID_Error:
                Err.Raise(Err.Number, "cBookingType: databaseID", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo ID_Error
                mvarDatabaseID = value
                RaiseEvent BookingtypeChanged(Me)
                On Error GoTo 0
                Exit Property
ID_Error:
                Err.Raise(Err.Number, "cBookingType: databaseID", Err.Description)
            End Set

        End Property

        'In Denmark, some channels have a maximum discount that can be given regardless of the indexes
        Public Property MaxDiscount() As Single
            Get
                Return mvarMaxDiscount
            End Get
            Set(ByVal value As Single)
                mvarMaxDiscount = value
                RaiseEvent BookingtypeChanged(Me)
            End Set
        End Property



        Public Property DefaultDaypart(ByVal Daypart) As Byte
            'Moved here from cPrivelistTarget

            '---------------------------------------------------------------------------------------
            ' Procedure : DefaultDaypart
            ' DateTime  : 2003-07-11 11:48
            ' Author    : joho
            ' Purpose   : Returns/sets the default share in each daypart
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo DefaultDay_Error

                DefaultDaypart = mvarDefaultDaypart(Daypart)

                On Error GoTo 0
                Exit Property

DefaultDay_Error:

                Err.Raise(Err.Number, "cPriceList: DefaultDay", Err.Description)
            End Get
            Set(ByVal value As Byte)
                On Error GoTo DefaultDay_Error

                mvarDefaultDaypart(Daypart) = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

DefaultDay_Error:

                Err.Raise(Err.Number, "cPriceList: DefaultDay", Err.Description)

            End Set
        End Property

        Public Property Comments() As String
            Get
                Return mvarComments
            End Get
            Set(ByVal value As String)
                mvarComments = value
                RaiseEvent BookingtypeChanged(Me)
            End Set
        End Property

        Public Property ManualIndexes() As Boolean
            Get
                Return mvarManualIndexes
            End Get
            Set(ByVal value As Boolean)
                mvarManualIndexes = value
                RaiseEvent BookingtypeChanged(Me)
            End Set
        End Property

        Public Sub InvalidateCPPs()
            For Each TmpWeek As cWeek In mvarWeeks
                TmpWeek.RecalculateCPP()
            Next
            RaiseEvent BookingtypeChanged(Me)
        End Sub

        ''' <summary>
        ''' Gets the CPP for a specific date calculated by discount, regardless of how it was entered
        ''' </summary>
        ''' <param name="WhatDate">The date.</param>
        ''' <param name="Daypart">The daypart.</param><returns></returns>
        Public Function GetDiscountedCPPForDate(ByVal WhatDate As Date, Optional ByVal Daypart As Integer = -1) As Decimal
            Dim MinCPP As Decimal = GetGrossCPP30ForDate(WhatDate, Daypart) * (1 - MaxDiscount)
            Dim TmpCPP As Decimal = 0
            Dim TmpIdx As Decimal = 0
            'Save all indexes that are to be added after check for Maximum discount
            Dim TmpAfterMaxIndex As Single = 1
            If mvarBuyingTarget IsNot Nothing Then

                TmpCPP = mvarBuyingTarget.GetCPPForDate(WhatDate.ToOADate, Daypart) * (1 - mvarBuyingTarget.Discount)
                For Each TmpIndex As cIndex In Indexes
                    If TmpIndex.UseThis Then 'Is it used
                        If TmpIndex.FromDate <= WhatDate AndAlso TmpIndex.ToDate >= WhatDate Then 'Does it concern the date we are asking about 
                            If TmpIndex.IndexOn = cIndex.IndexOnEnum.eTRP Then 'Is it on TRP?
                                If TmpIndex.Enhancements.Count > 0 Then 'If it about enhancements
                                    For Each TmpEnh As cEnhancement In TmpIndex.Enhancements
                                        If TmpEnh.UseThis Then
                                            TmpIdx += TmpEnh.Amount
                                        End If
                                    Next
                                Else 'If it is NOT about enhancements
                                    If TmpIndex.Index > 0 Then
                                        TmpCPP *= 100 / TmpIndex.Index
                                    End If
                                End If
                            Else 'The index is not on TRPs
                                If (Not mvarBuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP OrElse TmpIndex.IndexOn = cIndex.IndexOnEnum.eNetCPP OrElse TmpIndex.IndexOn = cIndex.IndexOnEnum.eNetCPPAfterMaxDiscount) Then
                                    TmpCPP *= TmpIndex.Index / 100
                                End If
                            End If
                            If TmpIndex.IndexOn = cIndex.IndexOnEnum.eNetCPPAfterMaxDiscount Then
                                TmpAfterMaxIndex *= TmpIndex.Index / 100
                            End If
                        End If
                    End If
                Next

                For Each TmpIndex As cIndex In mvarBuyingTarget.Indexes
                    If TmpIndex.UseThis Then
                        If TmpIndex.FromDate <= WhatDate AndAlso TmpIndex.ToDate >= WhatDate Then
                            If TmpIndex.IndexOn = cIndex.IndexOnEnum.eTRP Then
                                If TmpIndex.Enhancements.Count > 0 Then
                                    For Each TmpEnh As cEnhancement In TmpIndex.Enhancements
                                        If TmpEnh.UseThis Then
                                            TmpIdx += TmpEnh.Amount
                                        End If
                                    Next
                                Else
                                    If TmpIndex.Index > 0 Then
                                        TmpCPP *= 100 / TmpIndex.Index
                                    End If
                                End If
                            Else
                                'TJOHO - lade den här (och även i blocket ovan som läser index från Indexes). Ta bort kommentaren sedan.
                                If (Not mvarBuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP OrElse TmpIndex.IndexOn = cIndex.IndexOnEnum.eNetCPP OrElse TmpIndex.IndexOn = cIndex.IndexOnEnum.eNetCPPAfterMaxDiscount) Then
                                    TmpCPP *= TmpIndex.Index / 100
                                End If
                            End If
                            If TmpIndex.IndexOn = cIndex.IndexOnEnum.eNetCPPAfterMaxDiscount Then
                                TmpAfterMaxIndex *= TmpIndex.Index / 100
                            End If
                        End If
                    End If
                Next

                TmpIdx /= 100
                TmpIdx = TmpIdx / (1 + TmpIdx) / (EnhancementFactor / 100)
                TmpIdx = (1 / (1 - TmpIdx))
            End If
            If TmpIdx = 0 Then TmpIdx = 1
            TmpCPP *= (1 / TmpIdx)
            If TmpCPP < MinCPP Then
                'The CPP is lower than the minimum allowed CPP
                TmpCPP = MinCPP * TmpAfterMaxIndex
            End If
            Return TmpCPP
        End Function

        Public Function GetNetCPP30ForDate(ByVal WhatDate As Date, Optional ByVal Daypart As Integer = -1) As Decimal
            Dim MinCPP As Decimal = GetGrossCPP30ForDate(WhatDate, Daypart) * (1 - MaxDiscount)
            Dim TmpCPP As Decimal = 0
            Dim TmpIdx As Decimal = 0
            'Save all indexes that are to be added after check for Maximum discount
            Dim TmpAfterMaxIndex As Single = 1
            If mvarBuyingTarget IsNot Nothing Then
                Select Case mvarBuyingTarget.IsEntered
                    Case cPricelistTarget.EnteredEnum.eCPP
                        TmpIdx = 0
                        TmpCPP = mvarBuyingTarget.NetCPP
                    Case cPricelistTarget.EnteredEnum.eCPT
                        TmpIdx = 0
                        TmpCPP = (mvarBuyingTarget.NetCPT * mvarBuyingTarget.getUniSizeUni(WhatDate.ToOADate)) / 100
                    Case cPricelistTarget.EnteredEnum.eDiscount
                        TmpCPP = mvarBuyingTarget.GetCPPForDate(WhatDate.ToOADate, Daypart) * (1 - mvarBuyingTarget.Discount)
                    Case cPricelistTarget.EnteredEnum.eEnhancement
                        TmpIdx = mvarBuyingTarget.Enhancement
                        TmpCPP = mvarBuyingTarget.GetCPPForDate(WhatDate.ToOADate, Daypart)
                End Select


                For Each TmpIndex As cIndex In Indexes
                    If TmpIndex.UseThis Then 'Is it used
                        If TmpIndex.FromDate <= WhatDate AndAlso TmpIndex.ToDate >= WhatDate Then 'Does it concern the date we are asking about 
                            If TmpIndex.IndexOn = cIndex.IndexOnEnum.eTRP Then 'Is it on TRP?
                                If TmpIndex.Enhancements.Count > 0 Then 'If it about enhancements
                                    For Each TmpEnh As cEnhancement In TmpIndex.Enhancements
                                        If TmpEnh.UseThis Then
                                            TmpIdx += TmpEnh.Amount
                                        End If
                                    Next
                                Else 'If it is NOT about enhancements
                                    If TmpIndex.Index > 0 Then
                                        TmpCPP *= 100 / TmpIndex.Index
                                    End If
                                End If
                            Else 'The index is not on TRPs
                                If (Not mvarBuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP OrElse TmpIndex.IndexOn = cIndex.IndexOnEnum.eNetCPP OrElse TmpIndex.IndexOn = cIndex.IndexOnEnum.eNetCPPAfterMaxDiscount) Then
                                    TmpCPP *= TmpIndex.Index / 100
                                End If
                            End If
                            If TmpIndex.IndexOn = cIndex.IndexOnEnum.eNetCPPAfterMaxDiscount Then
                                TmpAfterMaxIndex *= TmpIndex.Index / 100
                            End If
                        End If
                    End If
                Next

                For Each TmpIndex As cIndex In mvarBuyingTarget.Indexes
                    If TmpIndex.UseThis Then
                        If TmpIndex.FromDate <= WhatDate AndAlso TmpIndex.ToDate >= WhatDate Then
                            If TmpIndex.IndexOn = cIndex.IndexOnEnum.eTRP Then
                                If TmpIndex.Enhancements.Count > 0 Then
                                    For Each TmpEnh As cEnhancement In TmpIndex.Enhancements
                                        If TmpEnh.UseThis Then
                                            TmpIdx += TmpEnh.Amount
                                        End If
                                    Next
                                Else
                                    If TmpIndex.Index > 0 Then
                                        TmpCPP *= 100 / TmpIndex.Index
                                    End If
                                End If
                            Else
                                'TJOHO - lade den här (och även i blocket ovan som läser index från Indexes). Ta bort kommentaren sedan.
                                If (Not mvarBuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP OrElse TmpIndex.IndexOn = cIndex.IndexOnEnum.eNetCPP OrElse TmpIndex.IndexOn = cIndex.IndexOnEnum.eNetCPPAfterMaxDiscount) Then
                                    TmpCPP *= TmpIndex.Index / 100
                                End If
                            End If
                            If TmpIndex.IndexOn = cIndex.IndexOnEnum.eNetCPPAfterMaxDiscount Then
                                TmpAfterMaxIndex *= TmpIndex.Index / 100
                            End If
                        End If
                    End If
                Next

                TmpIdx /= 100
                TmpIdx = TmpIdx / (1 + TmpIdx) / (EnhancementFactor / 100)
                TmpIdx = (1 / (1 - TmpIdx))
            End If
            If TmpIdx = 0 Then TmpIdx = 1
            TmpCPP *= (1 / TmpIdx)
            If TmpCPP < MinCPP Then
                'The CPP is lower than the minimum allowed CPP
                TmpCPP = MinCPP * TmpAfterMaxIndex
            End If
            Return TmpCPP
        End Function

        Public Function GetGrossCPP30ForDate(ByVal WhatDate As Date, Optional ByVal Daypart As Integer = -1) As Decimal
            Dim TmpCPP As Decimal = 0
            If mvarBuyingTarget IsNot Nothing Then
                TmpCPP = mvarBuyingTarget.GetCPPForDate(WhatDate.ToOADate, Daypart)
                For Each TmpIndex As cIndex In Indexes
                    If TmpIndex.UseThis Then
                        If TmpIndex.FromDate <= WhatDate AndAlso TmpIndex.ToDate >= WhatDate Then
                            If TmpIndex.IndexOn = cIndex.IndexOnEnum.eGrossCPP Then
                                TmpCPP *= TmpIndex.Index / 100
                            End If
                        End If
                    End If
                Next
                If mvarRatecardCPPIsGross Then
                    For Each TmpIndex As cIndex In mvarBuyingTarget.Indexes
                        If TmpIndex.UseThis Then
                            If TmpIndex.FromDate <= WhatDate AndAlso TmpIndex.ToDate >= WhatDate Then
                                If TmpIndex.IndexOn = cIndex.IndexOnEnum.eGrossCPP Then
                                    TmpCPP *= TmpIndex.Index / 100
                                End If
                            End If
                        End If
                    Next
                End If
            End If
            Return TmpCPP
        End Function

        Public Function GetIndexForDate(ByVal WhatDate As Date) As Decimal
            Dim TmpIdx As Decimal = 1
            Dim TmpEnhIdx As Decimal = 0
            If mvarBuyingTarget IsNot Nothing Then
                For Each TmpIndex As cIndex In Indexes
                    If TmpIndex.UseThis Then
                        If TmpIndex.FromDate <= WhatDate AndAlso TmpIndex.ToDate >= WhatDate Then
                            If TmpIndex.IndexOn = cIndex.IndexOnEnum.eTRP Then
                                If TmpIndex.Enhancements.Count > 0 Then
                                    For Each TmpEnh As cEnhancement In TmpIndex.Enhancements
                                        If TmpEnh.UseThis Then
                                            TmpEnhIdx += TmpEnh.Amount
                                        End If
                                    Next
                                Else
                                    TmpIdx *= 100 / TmpIndex.Index
                                End If
                            Else
                                TmpIdx *= TmpIndex.Index / 100
                            End If
                        End If
                    End If
                Next
                For Each TmpIndex As cIndex In mvarBuyingTarget.Indexes
                    If TmpIndex.UseThis Then
                        If TmpIndex.FromDate <= WhatDate AndAlso TmpIndex.ToDate >= WhatDate Then
                            If TmpIndex.IndexOn = cIndex.IndexOnEnum.eTRP Then
                                If TmpIndex.Enhancements.Count > 0 Then
                                    For Each TmpEnh As cEnhancement In TmpIndex.Enhancements
                                        If TmpEnh.UseThis Then
                                            TmpEnhIdx += TmpEnh.Amount
                                        End If
                                    Next
                                Else
                                    TmpIdx *= 100 / TmpIndex.Index
                                End If
                            Else
                                TmpIdx *= TmpIndex.Index / 100
                            End If
                        End If
                    End If
                Next
                TmpEnhIdx /= 100
                TmpEnhIdx = TmpEnhIdx / (1 + TmpEnhIdx) / (EnhancementFactor / 100)
                TmpEnhIdx = (1 / (1 - TmpEnhIdx))
            End If
            If TmpEnhIdx = 0 Then TmpEnhIdx = 1
            TmpIdx *= (1 / TmpEnhIdx)
            Return TmpIdx
        End Function

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            Dim XMLTMP As Xml.XmlElement
            Dim XMLTMP2 As Xml.XmlElement

            'Save weeks
            Dim w As Integer = 0
            Dim XMLWeeks As Xml.XmlElement = xmlDoc.CreateElement("Weeks")
            Me.Weeks.GetXML(XMLWeeks, errorMessege, xmlDoc)
            colXml.AppendChild(XMLWeeks)

            'Save the pricelist
            XMLTMP = xmlDoc.CreateElement("Pricelist")
            Me.Pricelist.GetXML(XMLTMP, errorMessege, xmlDoc)
            colXml.AppendChild(XMLTMP)

            'Save Buyingtarget
            Dim XMLBuyTarget As Xml.XmlElement = xmlDoc.CreateElement("BuyingTarget")
            Me.BuyingTarget.GetXML(XMLBuyTarget, errorMessege, xmlDoc)
            colXml.AppendChild(XMLBuyTarget)

            'Save Indexes
            XMLTMP = xmlDoc.CreateElement("Indexes")
            Me.Indexes.GetXML(XMLTMP, errorMessege, xmlDoc)
            colXml.AppendChild(XMLTMP)

            'saves added values
            XMLTMP = xmlDoc.CreateElement("AddedValues")
            Me.AddedValues.GetXML(XMLTMP, errorMessege, xmlDoc, -1)
            colXml.AppendChild(XMLTMP)

            'save compensations
            XMLTMP = xmlDoc.CreateElement("Compensations")
            Me.Compensations.GetXML(XMLTMP, errorMessege, xmlDoc)
            colXml.AppendChild(XMLTMP)


            'Save the rest of the booking type
            colXml.SetAttribute("Name", Me.Name) ' as String
            colXml.SetAttribute("Shortname", Me.Shortname) ' as String

            colXml.SetAttribute("IndexMainTarget", Me.IndexMainTarget) ' as Integer
            colXml.SetAttribute("IndexSecondTarget", Me.IndexSecondTarget) ' as Integer
            colXml.SetAttribute("IndexAllAdults", Me.IndexAllAdults) ' as Integer
            colXml.SetAttribute("BookIt", Me.BookIt) ' as Boolean
            colXml.SetAttribute("PlannedSpotCount", Me.EstimatedSpotCount)
            colXml.SetAttribute("ConfirmedNetBudget", Me.ConfirmedNetBudget) ' as Currency
            colXml.SetAttribute("Bookingtype", Me.Bookingtype) ' as Byte
            colXml.SetAttribute("ContractNumber", Me.ContractNumber) ' as String
            colXml.SetAttribute("OrderNumber", Me.OrderNumber)
            colXml.SetAttribute("IsRBS", Me.IsRBS) ' as Boolean
            colXml.SetAttribute("IsSpecific", Me.IsSpecific) ' as Boolean
            colXml.SetAttribute("IsPremium", Me.IsPremium) ' as Boolean
            colXml.SetAttribute("IsCompensation", Me.IsCompensation) ' as Boolean
            colXml.SetAttribute("PrintBookingCode", Me.PrintBookingCode) ' as Boolean
            colXml.SetAttribute("PrintDayparts", Me.PrintDayparts) ' as Boolean
            colXml.SetAttribute("Comments", Me.Comments) ' as Boolean

            'get filmindexes
            Dim XMLIndexes As Xml.XmlElement = xmlDoc.CreateElement("SpotIndex")
            For i As Integer = 0 To 500
                If Me.FilmIndex(i) > 0 Then
                    XMLTMP = xmlDoc.CreateElement("Index")
                    XMLTMP.SetAttribute("Length", i)
                    XMLTMP.SetAttribute("Idx", Me.FilmIndex(i))
                    XMLIndexes.AppendChild(XMLTMP)
                End If
            Next
            colXml.AppendChild(XMLIndexes)

            'get the daypartsplit
            XMLTMP = xmlDoc.CreateElement("DaypartSplit")
            For i As Integer = 0 To Dayparts.Count - 1
                XMLTMP2 = xmlDoc.CreateElement(Dayparts(i).Name)
                XMLTMP2.SetAttribute("Share", Me.Dayparts(i).Share) ' as Byte
                XMLTMP.AppendChild(XMLTMP2)
            Next
            colXml.AppendChild(XMLTMP)

            'Get the default daypart split
            XMLTMP = xmlDoc.CreateElement("DaypartSplit")
            For i As Integer = 0 To Me.Dayparts.Count - 1
                XMLTMP2 = xmlDoc.CreateElement(Me.Dayparts(i).Name)
                XMLTMP2.SetAttribute("DefaultSplit", Me.DefaultDaypart(i)) ' as New cPriceList
                XMLTMP.AppendChild(XMLTMP2)
            Next
            colXml.AppendChild(XMLTMP)

            Exit Function

On_Error:
            colXml.AppendChild(xmlDoc.CreateComment("ERROR (" & Err.Number & "): " & Err.Description))
            errorMessege.Add("Error saving Bookingtype " & Me.ToString)
            Resume Next
        End Function

        Public Function IsSeasonal()
            'checks the pricelist for seasonal changes ie two pricerows per year
            Dim hashYear As New Hashtable

            If Me.BuyingTarget.Indexes.Count > 0 Then Return True
            For Each plp As Trinity.cPricelistPeriod In Me.BuyingTarget.PricelistPeriods
                If hashYear(plp.FromDate.Year) Is Nothing Then
                    hashYear.Add(plp.FromDate.Year, 1)
                Else
                    Return True
                End If
            Next
            Return False
        End Function

        Public Sub InvalidateTotalTRP()
            mvarTotalTRPChanged = True
        End Sub

        Public ReadOnly Property ActualNetValue() As Single
            Get
                Return mvarNetValue.Value
            End Get
        End Property

        Public Sub InvalidateActualNetValue()
            mvarNetValue.Invalidate()
        End Sub

        Private _combination As cCombination
        Public Property Combination() As cCombination
            Get
                Return _combination
            End Get
            Set(ByVal value As cCombination)
                _combination = value
            End Set
        End Property


        Public ReadOnly Property ShowMe() As Boolean
            'this function is used for displaying cCombinations in one row (=false) or as normal (=True)
            'Default is to show
            Get
                If IsCompensation AndAlso (Combination Is Nothing OrElse Not Combination.IsOnlyCompensation) Then Return True
                Return Combination Is Nothing OrElse Not Combination.ShowAsOne
            End Get
        End Property

        Public Property IsLocked() As Boolean
            'this function is used for locking the BT in Allocate
            'Default is not locked
            Get
                If (IsPremium AndAlso IsSpecific) OrElse IsCompensation Then
                    Return True
                Else
                    Return mvarIsLocked
                End If
            End Get
            Set(ByVal value As Boolean)
                mvarIsLocked = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return ParentChannel.ChannelName & " " & mvarName
        End Function

        Public Function DistinctString() As String
            Return ParentChannel.ChannelName & "|" & mvarName
        End Function

        'Only used when the bookingtype is part of a contract
        Public Property NegotiatedVolume() As Decimal
            Get
                Return mvarVolume
            End Get
            Set(ByVal value As Decimal)
                mvarVolume = value
                RaiseEvent BookingtypeChanged(Me)
            End Set
        End Property

        Public Property PrintBookingCode() As Boolean
            Get
                Return mvarPrintBookingCode
            End Get
            Set(ByVal value As Boolean)
                mvarPrintBookingCode = value
                RaiseEvent BookingtypeChanged(Me)
            End Set
        End Property

        Public Property PrintDayparts() As Boolean
            Get
                Return mvarPrintDayparts
            End Get
            Set(ByVal value As Boolean)
                mvarPrintDayparts = value
                RaiseEvent BookingtypeChanged(Me)
            End Set
        End Property

        Public Property Compensations() As cCompensations
            Get
                Return mvarCompensations
            End Get
            Set(ByVal value As cCompensations)
                mvarCompensations = value
                RaiseEvent BookingtypeChanged(Me)
            End Set
        End Property

        Public Property ParentChannel() As cChannel
            '---------------------------------------------------------------------------------------
            ' Procedure : ParentChannel
            ' DateTime  : 2003-08-15 16:18
            ' Author    : joho
            ' Purpose   :
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo ParentChannel_Error

                ParentChannel = mvarParentChannel

                On Error GoTo 0
                Exit Property

ParentChannel_Error:

                Err.Raise(Err.Number, "cBookingType: ParentChannel", Err.Description)
            End Get
            Set(ByVal value As cChannel)

                Try
                    mvarParentChannel = value

                    SetPricelistName()
                Catch ex As Exception

                    Throw New Exception("Error in cBookingType: ParentChannel" & vbNewLine & vbNewLine & ex.Message, ex)

                End Try
            End Set
        End Property

        Private Sub SetPricelistName()
            If ParentChannel Is Nothing OrElse String.IsNullOrEmpty(Name) OrElse Not String.IsNullOrEmpty(PricelistName) Then Exit Sub
            Dim _chanXML As Xml.Linq.XDocument = Xml.Linq.XDocument.Load(IO.Path.Combine(IO.Path.Combine(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork), Main.Area), ParentChannel.fileName))

            Try
                If _chanXML.<Data>.<Channels>...<Channel>.First(Function(c) c.@Name = ParentChannel.ChannelName) IsNot Nothing AndAlso _chanXML.<Data>.<Channels>...<Channel>.First(Function(c) c.@Name = ParentChannel.ChannelName).<Bookingtypes>...<Bookingtype>.First(Function(b) b.@Name = Name) IsNot Nothing Then
                    PricelistName = _chanXML.<Data>.<Channels>...<Channel>.First(Function(c) c.@Name = ParentChannel.ChannelName).<Bookingtypes>...<Bookingtype>.First(Function(b) b.@Name = Name).@Pricelist
                End If
            Catch
                Exit Sub
            End Try
        End Sub


        Friend Property MainObject() As cKampanj
            Get
                Return mvarMain
            End Get
            Set(ByVal value As cKampanj)
                mvarMain = value
                mvarPriceList.MainObject = value
                mvarPriceList.Bookingtype = Me
                RaiseEvent BookingtypeChanged(Me)
            End Set
        End Property

        Friend WriteOnly Property ParentCollection()
            '---------------------------------------------------------------------------------------
            ' Procedure : ParentCollection
            ' DateTime  : 2003-07-07 13:18
            ' Author    : joho
            ' Purpose   : Sets the Collection of wich this channel is a member. This is used
            '             when a new Name is set. See that property for further explanation
            '---------------------------------------------------------------------------------------
            '
            Set(ByVal value)
                ParentColl = value
                RaiseEvent BookingtypeChanged(Me)
            End Set
        End Property

        '        Public Property DaypartSplit(ByVal Index) As Byte
        '            Get
        '                On Error GoTo DaypartSplit_Error

        '                Return mvarDaypartSplit(Index)

        '                On Error GoTo 0
        '                Exit Property

        'DaypartSplit_Error:

        '                Err.Raise(Err.Number, "cBookingType: DaypartSplit", Err.Description)
        '            End Get
        '            Set(ByVal value As Byte)
        '                'Dim TmpCPP As Decimal

        '                On Error GoTo DaypartSplit_Error

        '                mvarDaypartSplit(Index) = value

        '                'If BuyingTarget.CalcCPP Then
        '                '    BuyingTarget.CalculateCPP()
        '                'End If
        '                'TmpCPP = BuyingTarget.CPP
        '                'mvarGrossCPP = TmpCPP
        '                On Error GoTo 0
        '                Exit Property

        'DaypartSplit_Error:

        '                Err.Raise(Err.Number, "cBookingType: DaypartSplit", Err.Description)

        '            End Set
        '        End Property

        Public Function PlannedNetBudget()
            '---------------------------------------------------------------------------------------
            ' Procedure : PlannedNetBudget
            ' DateTime  : 2003-07-03 12:03
            ' Author    : joho
            ' Purpose   : Calculates the planned net budget for the bookingtype based on CPP
            '             and booked TRPs
            '---------------------------------------------------------------------------------------
            '

            On Error GoTo PlannedNetBudget_Error

            If Not IsCompensation Then
                Dim TmpWeek As cWeek

                PlannedNetBudget = 0
                For Each TmpWeek In mvarWeeks
                    PlannedNetBudget = PlannedNetBudget + TmpWeek.NetBudget
                Next
            Else
                Return 0
            End If
            On Error GoTo 0
            Exit Function

PlannedNetBudget_Error:

            Err.Raise(Err.Number, "cBookingType: PlannedNetBudget", Err.Description)

        End Function

        Public Function ActualTRP30(ByVal Target As Trinity.cActualSpot.ActualTargetEnum, Optional ByVal Week As String = "All") As Decimal

            'Dim TRP As Decimal = 0
            'For Each TmpSpot As Trinity.cActualSpot In mvarActualSpots
            '    TRP += TmpSpot.Rating30(Target)
            'Next
            'Return TRP

            If Week = "All" Then
                Return (From p As cActualSpot In mvarActualSpots Select p.Rating30(Target)).Sum
            Else
                Return (From p As cActualSpot In mvarActualSpots Where p.Week IsNot Nothing AndAlso p.Week.Name = Week Select p.Rating30(Target)).Sum
            End If

        End Function

        Public Function ActualTRP(ByVal Target As Trinity.cActualSpot.ActualTargetEnum, Optional ByVal week As String = "All") As Decimal


            'Dim TRP As Decimal = 0
            'For Each TmpSpot As Trinity.cActualSpot In mvarActualSpots
            '    TRP += TmpSpot.Rating(Target)
            'Next
            'Return TRP

            If week = "All" Then
                Return (From p As cActualSpot In mvarActualSpots Select p.Rating(Target)).Sum
            Else
                Return (From p As cActualSpot In mvarActualSpots Where p.Week IsNot Nothing AndAlso p.Week.Name = week Select p.Rating(Target)).Sum
            End If

        End Function

        Public Function PlannedTRP30() As Decimal
            Dim TRP As Decimal = 0
            For Each TmpWeek As cWeek In mvarWeeks
                If TmpWeek.SpotIndex > 0 Then
                    TRP += TmpWeek.TRP / (TmpWeek.SpotIndex / 100)
                End If
            Next

            Dim Planned As Decimal = (From tmpWeek As Trinity.cWeek In mvarWeeks Where tmpWeek.SpotIndex > 0 Select tmpWeek.TRPBuyingTarget / (tmpWeek.SpotIndex / 100)).Sum

            Return TRP
        End Function

        '        Public Function ActualNetValue()
        '            '---------------------------------------------------------------------------------------
        '            ' Procedure : ActualNetBudget
        '            ' DateTime  : 2003-07-03 12:06
        '            ' Author    : joho
        '            ' Purpose   : Calculates the actual net budget based on CPP and delivered TRPs
        '            '---------------------------------------------------------------------------------------
        '            '

        '            On Error GoTo ActualNetBudget_Error

        '            Dim TmpBudget As Decimal = 0

        '            For Each TmpSpot As cActualSpot In mvarActualSpots
        '                'If TmpSpot.MatchedSpot IsNot Nothing AndAlso TmpSpot.MatchedSpot.PriceNet > 0 Then
        '                ' TmpBudget += TmpSpot.MatchedSpot.PriceNet
        '                ' Else
        '                If TmpSpot.Bookingtype Is Me Then
        '                    TmpBudget += TmpSpot.ActualNetValue
        '                End If
        '                'End If
        '            Next
        '            Return TmpBudget

        '            On Error GoTo 0
        '            Exit Function

        'ActualNetBudget_Error:

        '            Err.Raise(Err.Number, "cBookingType: ActualNetBudget", Err.Description)

        '        End Function

        Public ReadOnly Property ActualSpots() As Collection
            Get
                Return mvarActualSpots
            End Get
            'Set(ByVal value As Collection)
            '    mvarActualSpots = value
            '    RaiseEvent BookingtypeChanged(Me)
            'End Set
        End Property

        Public Function ActualGrossBudget()
            '---------------------------------------------------------------------------------------
            ' Procedure : ActualGrossBudget
            ' DateTime  : 2003-07-03 12:06
            ' Author    : joho
            ' Purpose   : Calculates the actual Gross budget based on CPP and delivered TRPs
            '---------------------------------------------------------------------------------------
            '

            On Error GoTo ActualGrossBudget_Error

            Dim TmpBudget As Decimal = 0

            For Each TmpSpot As cActualSpot In Main.ActualSpots
                If TmpSpot.Bookingtype Is Me Then
                    If TmpSpot.MatchedSpot IsNot Nothing AndAlso TmpSpot.MatchedSpot.PriceGross > 0 Then
                        TmpBudget += TmpSpot.MatchedSpot.PriceGross
                    Else
                        TmpBudget += TmpSpot.Rating(cActualSpot.ActualTargetEnum.ateBuyingTarget) * TmpSpot.Week.GrossCPP
                    End If
                End If
            Next
            Return TmpBudget

            On Error GoTo 0
            Exit Function

ActualGrossBudget_Error:

            Err.Raise(Err.Number, "cBookingType: ActualGrossBudget", Err.Description)

        End Function

        'Public Property ID() As String
        '    Get
        '        Return strID
        '    End Get
        '    Set(ByVal value As String)
        '        strID = value
        '    End Set
        'End Property

        Public Property Name() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : Name
            ' DateTime  : 2003-07-10 15:31
            ' Author    : joho
            ' Purpose   : Returns/sets the name of the booking type
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Name_Error

                Name = mvarName

                On Error GoTo 0
                Exit Property

Name_Error:

                Err.Raise(Err.Number, "cBookingType: Name", Err.Description)
            End Get
            Set(ByVal value As String)
                Dim TmpBookingType As cBookingType

                On Error GoTo Name_Error

                If value <> mvarName Then
                    If Not ParentColl Is Nothing Then
                        If ParentColl.Contains(value) Then
                            Throw New System.Exception("Bookingtype already exists.")
                        Else
                            If ParentColl.Contains(mvarName) Then
                                TmpBookingType = ParentColl(mvarName)
                                ParentColl.Remove(mvarName)
                                If Not TmpBookingType Is Nothing Then
                                    ParentColl.Add(TmpBookingType, value)
                                End If
                            End If
                        End If
                    End If
                End If
                mvarName = value
                SetPricelistName()

                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

Name_Error:

                Err.Raise(Err.Number, "cBookingType: Name", Err.Description)

            End Set
        End Property

        Public Property Shortname() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : Shortname
            ' DateTime  : 2003-07-10 15:31
            ' Author    : joho
            ' Purpose   : Returns/sets the abbrevation for the booking type
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Shortname_Error

                Shortname = mvarShortname

                On Error GoTo 0
                Exit Property

Shortname_Error:

                Err.Raise(Err.Number, "cBookingType: Shortname", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo Shortname_Error

                mvarShortname = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

Shortname_Error:

                Err.Raise(Err.Number, "cBookingType: Shortname", Err.Description)

            End Set
        End Property

        Public Property BuyingTarget() As cPricelistTarget
            '---------------------------------------------------------------------------------------
            ' Procedure : BuyingTarget
            ' DateTime  : 2003-07-10 15:31
            ' Author    : joho
            ' Purpose   : Pointer to the cPriceListTarget object containing the BuyingTarget
            '             for the BuyingType
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo BuyingTarget_Error

                Dim SaveUniSize As Boolean

                If Not mvarBuyingTarget Is Nothing Then
                    If mvarBuyingTarget.Target Is Nothing Then mvarBuyingTarget.Target = New cTarget(Main)
                    SaveUniSize = mvarBuyingTarget.Target.NoUniverseSize
                    mvarBuyingTarget.Target.NoUniverseSize = True
                    mvarBuyingTarget.Target.Universe = mvarParentChannel.BuyingUniverse
                    mvarBuyingTarget.Target.NoUniverseSize = SaveUniSize
                    mvarBuyingTarget.Bookingtype = Me
                Else
                    mvarBuyingTarget = New cPricelistTarget(Main, Me)
                End If
                BuyingTarget = mvarBuyingTarget

                On Error GoTo 0
                Exit Property

BuyingTarget_Error:

                Err.Raise(Err.Number, "cBookingType: BuyingTarget", Err.Description)
            End Get
            Set(ByVal value As cPricelistTarget)
                On Error GoTo BuyingTarget_Error

                Dim TmpWeek As cWeek
                Dim TmpIndex As cIndex
                Dim i As Integer

                If value Is Nothing Then
                    'Windows.Forms.MessageBox.Show("The booking type " & Me.ToString & " tried to set its buying target to nothing. This will cause problems, contact the system administrators.")
                    'Exit Property
                End If
                mvarBuyingTarget = value

                'If Not mvarBuyingTarget Is Nothing Then
                '    mvarGrossCPP = mvarBuyingTarget.CPP
                'Else
                '    mvarGrossCPP = 0
                'End If
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

BuyingTarget_Error:

                Err.Raise(Err.Number, "cBookingType: BuyingTarget", Err.Description)


            End Set
        End Property

        Public Property IndexMainTarget() As Integer
            '---------------------------------------------------------------------------------------
            ' Procedure : IndexMainTarget
            ' DateTime  : 2003-07-10 15:31
            ' Author    : joho
            ' Purpose   : Returns/sets the _expected_ index between the buying target and
            '             the Main target
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IndexMainTarget_Error

                IndexMainTarget = mvarIndexMainTarget

                On Error GoTo 0
                Exit Property

IndexMainTarget_Error:

                Err.Raise(Err.Number, "cBookingType: IndexMainTarget", Err.Description)
            End Get
            Set(ByVal value As Integer)
                On Error GoTo IndexMainTarget_Error

                mvarIndexMainTarget = value
                InvalidateTotalTRP()
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

IndexMainTarget_Error:

                Err.Raise(Err.Number, "cBookingType: IndexMainTarget", Err.Description)

            End Set
        End Property

        Public Property IndexSecondTarget() As Integer
            '---------------------------------------------------------------------------------------
            ' Procedure : IndexSecondTarget
            ' DateTime  : 2003-07-10 15:31
            ' Author    : joho
            ' Purpose   : Returns/sets the _expected_ index between the buying target and
            '             the Second target
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IndexSecondTarget_Error

                IndexSecondTarget = mvarIndexSecondTarget

                On Error GoTo 0
                Exit Property

IndexSecondTarget_Error:

                Err.Raise(Err.Number, "cBookingType: IndexSecondTarget", Err.Description)

            End Get
            Set(ByVal value As Integer)
                On Error GoTo IndexSecondTarget_Error

                mvarIndexSecondTarget = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

IndexSecondTarget_Error:

                Err.Raise(Err.Number, "cBookingType: IndexSecondTarget", Err.Description)

            End Set
        End Property

        Public Property IndexAllAdults() As Integer
            '---------------------------------------------------------------------------------------
            ' Procedure : IndexAllAdults
            ' DateTime  : 2003-07-10 15:32
            ' Author    : joho
            ' Purpose   : Returns/sets the _expected_ index between the buying target and the
            '             entire TV-population
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IndexAllAdults_Error

                IndexAllAdults = mvarIndexAllAdults

                On Error GoTo 0
                Exit Property

IndexAllAdults_Error:

                Err.Raise(Err.Number, "cBookingType: IndexAllAdults", Err.Description)
            End Get
            Set(ByVal value As Integer)
                On Error GoTo IndexAllAdults_Error

                mvarIndexAllAdults = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

IndexAllAdults_Error:

                Err.Raise(Err.Number, "cBookingType: IndexAllAdults", Err.Description)

            End Set
        End Property

        Public Property IndexMainTargetStatus() As IndexStatusEnum
            '---------------------------------------------------------------------------------------
            ' Procedure : IndexMainTargetStatus
            ' DateTime  : 2003-07-10 15:31
            ' Author    : joho
            ' Purpose   : Returns/sets how the property IndexMainTarget was set
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IndexMainTarget_Error

                Return mvarIndexMainTargetStatus

                On Error GoTo 0
                Exit Property

IndexMainTarget_Error:

                Err.Raise(Err.Number, "cBookingType: IndexMainTarget", Err.Description)
            End Get
            Set(ByVal value As IndexStatusEnum)
                On Error GoTo IndexMainTarget_Error

                mvarIndexMainTargetStatus = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

IndexMainTarget_Error:

                Err.Raise(Err.Number, "cBookingType: IndexMainTarget", Err.Description)

            End Set
        End Property

        Public Property IndexSecondTargetStatus() As IndexStatusEnum
            '---------------------------------------------------------------------------------------
            ' Procedure : IndexSecondTarget
            ' DateTime  : 2003-07-10 15:31
            ' Author    : joho
            ' Purpose   : Returns/sets how the property IndexSecondTarget was set
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IndexSecondTarget_Error

                Return mvarIndexSecondTargetStatus

                On Error GoTo 0
                Exit Property

IndexSecondTarget_Error:

                Err.Raise(Err.Number, "cBookingType: IndexSecondTarget", Err.Description)

            End Get
            Set(ByVal value As IndexStatusEnum)
                On Error GoTo IndexSecondTarget_Error

                mvarIndexSecondTargetStatus = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

IndexSecondTarget_Error:

                Err.Raise(Err.Number, "cBookingType: IndexSecondTarget", Err.Description)

            End Set
        End Property

        Public Property IndexAllAdultsStatus() As IndexStatusEnum
            '---------------------------------------------------------------------------------------
            ' Procedure : IndexAllAdults
            ' DateTime  : 2003-07-10 15:32
            ' Author    : joho
            ' Purpose   : Returns/sets how the property IndexAllAdults was set
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IndexAllAdults_Error

                Return mvarIndexAllAdultsStatus

                On Error GoTo 0
                Exit Property

IndexAllAdults_Error:

                Err.Raise(Err.Number, "cBookingType: IndexAllAdults", Err.Description)
            End Get
            Set(ByVal value As IndexStatusEnum)
                On Error GoTo IndexAllAdults_Error

                mvarIndexAllAdultsStatus = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

IndexAllAdults_Error:

                Err.Raise(Err.Number, "cBookingType: IndexAllAdults", Err.Description)

            End Set
        End Property

        Public ReadOnly Property Weeks() As cWeeks
            '---------------------------------------------------------------------------------------
            ' Procedure : Weeks
            ' DateTime  : 2003-07-10 15:32
            ' Author    : joho
            ' Purpose   : Pointer to the collection of cWeek objects containing data for each
            '             week
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Weeks_Error



                Weeks = mvarWeeks

                On Error GoTo 0
                Exit Property

Weeks_Error:

                Err.Raise(Err.Number, "cBookingType: Weeks", Err.Description)
            End Get
            '            Set(ByVal value As cWeeks)
            '                On Error GoTo Weeks_Error

            '                mvarWeeks = value
            '                mvarWeeks.Bookingtype = Me
            '                mvarWeeks.MainObject = Main

            '                On Error GoTo 0
            '                Exit Property

            'Weeks_Error:

            '                Err.Raise(Err.Number, "cBookingType: Weeks", Err.Description)

            '            End Set
        End Property

        Public Property BookIt() As Boolean
            '---------------------------------------------------------------------------------------
            ' Procedure : BookIt
            ' DateTime  : 2003-07-10 15:32
            ' Author    : joho
            ' Purpose   : Returns/sets wether the BookingType is to be used. No bookingtype
            '             should be Removed, instead BookIt should be set to False.
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo BookIt_Error

                BookIt = mvarBookIt

                On Error GoTo 0
                Exit Property

BookIt_Error:

                Err.Raise(Err.Number, "cBookingType: BookIt", Err.Description)
            End Get
            Set(ByVal value As Boolean)
                On Error GoTo BookIt_Error

                ' If there are spots attached to this booking type, throw an exception
                If (value = False AndAlso Me.SpotsExist()) Then
                    Throw New Exception("Trying to unbook a bookingtype that has spots attached to it")
                End If
                mvarBookIt = value

                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

BookIt_Error:

                Err.Raise(Err.Number, "cBookingType: BookIt", Err.Description)


            End Set
        End Property

        '        Public Property GrossCPP() As Single
        '            '---------------------------------------------------------------------------------------
        '            ' Procedure : GrossCPP
        '            ' DateTime  : 2003-07-10 15:32
        '            ' Author    : joho
        '            ' Purpose   : Returns/sets the Gross-CPP for this Buying type. GrossCPP is
        '            '             automatically updated when a new BuyingTarget is set if the optional
        '            '             parameter KeepGrossCPP is not set to False.
        '            '---------------------------------------------------------------------------------------
        '            '
        '            Get
        '                On Error GoTo GrossCPP_Error

        '                GrossCPP = mvarGrossCPP

        '                On Error GoTo 0
        '                Exit Property

        'GrossCPP_Error:

        '                Err.Raise(Err.Number, "cBookingType: GrossCPP", Err.Description)
        '            End Get
        '            Set(ByVal value As Single)
        '                On Error GoTo GrossCPP_Error

        '                mvarGrossCPP = value

        '                On Error GoTo 0
        '                Exit Property

        'GrossCPP_Error:

        '                Err.Raise(Err.Number, "cBookingType: GrossCPP", Err.Description)

        '            End Set
        '        End Property

        Public Property AverageRating() As Decimal
            '---------------------------------------------------------------------------------------
            ' Procedure : AverageRating
            ' DateTime  : 2003-07-10 15:32
            ' Author    : joho
            ' Purpose   : Returns/sets the _expected_ TRP per spot for this Booking Type.
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo AverageRating_Error

                AverageRating = mvarAverageRating

                On Error GoTo 0
                Exit Property

AverageRating_Error:

                Err.Raise(Err.Number, "cBookingType: AverageRating", Err.Description)
            End Get
            Set(ByVal value As Decimal)
                On Error GoTo AverageRating_Error

                If value < 0.01 Then value = 0
                mvarAverageRating = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

AverageRating_Error:

                Err.Raise(Err.Number, "cBookingType: AverageRating", Err.Description)

            End Set
        End Property

        Public Property ConfirmedNetBudget() As Decimal
            '---------------------------------------------------------------------------------------
            ' Procedure : ConfirmedNetBudget
            ' DateTime  : 2003-07-10 15:32
            ' Author    : joho
            ' Purpose   : Returns/sets the Net Budget Confirmed by the channel in their
            '             booking confirmation.
            '---------------------------------------------------------------------------------------
            '
            Get
                Dim TmpSpot As cPlannedSpot
                Dim TmpBudget As Decimal
                On Error GoTo ConfirmedNetBudget_Error

                If mvarIsCompensation Then
                    Return 0
                ElseIf TrinitySettings.UseConfirmedBudget Then
                    For Each TmpSpot In Main.PlannedSpots
                        If TmpSpot.Bookingtype Is Me Then
                            TmpBudget = TmpBudget + TmpSpot.PriceNet
                        End If
                    Next
                    If mvarConfirmedNetBudget > 0 Then
                        ConfirmedNetBudget = mvarConfirmedNetBudget
                    Else
                        ConfirmedNetBudget = TmpBudget
                    End If
                Else
                    Return PlannedNetBudget()
                End If

                On Error GoTo 0
                Exit Property

ConfirmedNetBudget_Error:

                Err.Raise(Err.Number, "cBookingType: ConfirmedNetBudget", Err.Description)
            End Get
            Set(ByVal value As Decimal)
                On Error GoTo ConfirmedNetBudget_Error

                mvarConfirmedNetBudget = value
                mvarMarathonNetBudget = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

ConfirmedNetBudget_Error:

                Err.Raise(Err.Number, "cBookingType: ConfirmedNetBudget", Err.Description)

            End Set
        End Property

        Public Property ConfirmedGrossBudget() As Decimal
            Get
                Dim TmpSpot As cPlannedSpot
                Dim TmpBudget As Decimal


                If mvarIsCompensation Then
                    Return 0
                ElseIf TrinitySettings.UseConfirmedBudget Then
                    For Each TmpSpot In Main.PlannedSpots
                        If TmpSpot.Bookingtype Is Me Then
                            TmpBudget = TmpBudget + TmpSpot.PriceGross
                        End If
                    Next
                    If TmpBudget = 0 Then
                        If Format(PlannedNetBudget, "0") > 0 AndAlso Format(PlannedGrossBudget, "0") > 0 Then
                            ConfirmedGrossBudget = mvarConfirmedNetBudget / (Format(PlannedNetBudget, "0") / Format(PlannedGrossBudget, "0"))
                        Else
                            ConfirmedGrossBudget = 0
                        End If
                    Else
                        ConfirmedGrossBudget = TmpBudget
                    End If
                Else
                    Return PlannedGrossBudget
                End If
            End Get
            Set(ByVal value As Decimal)

                If ConfirmedGrossBudget > 0 Then mvarConfirmedNetBudget = (value / ConfirmedGrossBudget) * mvarConfirmedNetBudget
                RaiseEvent BookingtypeChanged(Me)

            End Set





        End Property

        Public Property Bookingtype() As Byte
            '---------------------------------------------------------------------------------------
            ' Procedure : BookingType
            ' DateTime  : 2003-07-10 15:32
            ' Author    : joho
            ' Purpose   : Returns/set the index number indicating what Bookingtype this
            '             should be counted as:
            '
            '               0  - RBS
            '               1  - Specific
            '               2  - Last minute
            '               3> - User specified
            '---------------------------------------------------------------------------------------
            Get
                On Error GoTo BookingType_Error

                Bookingtype = mvarBookingtype

                On Error GoTo 0
                Exit Property

BookingType_Error:

                Err.Raise(Err.Number, "cBookingType: BookingType", Err.Description)

            End Get
            Set(ByVal value As Byte)
                On Error GoTo BookingType_Error

                mvarBookingtype = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

BookingType_Error:

                Err.Raise(Err.Number, "cBookingType: BookingType", Err.Description)

            End Set
        End Property

        Public Property ContractNumber() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : ContractNumber
            ' DateTime  : 2003-07-10 15:32
            ' Author    : joho
            ' Purpose   : Returns/sets the ContractNumber according to the booking confirmation
            '             from the channel.
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo ContractNumber_Error

                ContractNumber = mvarContractNumber

                On Error GoTo 0
                Exit Property

ContractNumber_Error:

                Err.Raise(Err.Number, "cBookingType: ContractNumber", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo ContractNumber_Error

                mvarContractNumber = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

ContractNumber_Error:

                Err.Raise(Err.Number, "cBookingType: ContractNumber", Err.Description)

            End Set
        End Property
        
        Public Property OrderNumber() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : OrderNumber
            ' DateTime  : 2003-07-10 15:32
            ' Author    : joho
            ' Purpose   : Returns/sets the OrderNumber according to Marathon
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo OrderNumber_Error

                OrderNumber = mvarOrderNumber

                On Error GoTo 0
                Exit Property

OrderNumber_Error:

                Err.Raise(Err.Number, "cBookingType: OrderNumber", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo OrderNumber_Error

                mvarOrderNumber = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

OrderNumber_Error:

                Err.Raise(Err.Number, "cBookingType: OrderNumber", Err.Description)

            End Set
        End Property

        Public ReadOnly Property Pricelist() As cPricelist
            '---------------------------------------------------------------------------------------
            ' Procedure : PriceList
            ' DateTime  : 2003-07-10 15:32
            ' Author    : joho
            ' Purpose   : Pointer to the cPricelist object containing the Pricelist. The
            '             pricelist is read when the Booking type is created.
            '             See Class.Initialize
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo PriceList_Error

                'Set mvarPriceList.MainObject = Main


                mvarPriceList.Bookingtype = Me
                Pricelist = mvarPriceList



                On Error GoTo 0
                Exit Property

PriceList_Error:

                Err.Raise(Err.Number, "cBookingType: PriceList", Err.Description)
            End Get
            '            Set(ByVal value As cPricelist)
            '                On Error GoTo PriceList_Error

            '                mvarPriceList = value


            '                On Error GoTo 0
            '                Exit Property

            'PriceList_Error:

            '                Err.Raise(Err.Number, "cBookingType: PriceList", Err.Description)

            '            End Set
        End Property

        Public Property IsVisible() As Boolean
            '---------------------------------------------------------------------------------------
            ' Procedure : IsVisible
            ' DateTime  : 2003-07-10 15:33
            ' Author    : joho
            ' Purpose   : Returns/sets wether this BookingType should be visible in the charts
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IsVisible_Error

                IsVisible = mvarIsVisible

                On Error GoTo 0
                Exit Property

IsVisible_Error:

                Err.Raise(Err.Number, "cBookingType: IsVisible", Err.Description)
            End Get
            Set(ByVal value As Boolean)
                On Error GoTo IsVisible_Error

                mvarIsVisible = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

IsVisible_Error:

                Err.Raise(Err.Number, "cBookingType: IsVisible", Err.Description)

            End Set
        End Property

        Public Property IsCompensation() As Boolean
            '---------------------------------------------------------------------------------------
            ' Procedure : IsCompensation
            ' DateTime  : 2003-07-10 15:33
            ' Author    : joho
            ' Purpose   : Returns/sets wether this booking tyoe should be regarded as a Compensation
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IsCompensation_Error

                Return mvarIsCompensation

                On Error GoTo 0
                Exit Property

IsCompensation_Error:

                Err.Raise(Err.Number, "cBookingType: IsCompensation", Err.Description)
            End Get
            Set(ByVal value As Boolean)
                On Error GoTo IsCompensation_Error

                mvarIsCompensation = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

IsCompensation_Error:

                Err.Raise(Err.Number, "cBookingType: IsCompensation", Err.Description)


            End Set
        End Property

        Public Property IsRBS() As Boolean
            '---------------------------------------------------------------------------------------
            ' Procedure : IsRBS
            ' DateTime  : 2003-07-10 15:33
            ' Author    : joho
            ' Purpose   : Returns/sets wether this booking tyoe should be regarded as a RBS
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IsRBS_Error

                IsRBS = mvarIsRBS

                On Error GoTo 0
                Exit Property

IsRBS_Error:

                Err.Raise(Err.Number, "cBookingType: IsRBS", Err.Description)
            End Get
            Set(ByVal value As Boolean)
                On Error GoTo IsRBS_Error

                mvarIsRBS = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

IsRBS_Error:

                Err.Raise(Err.Number, "cBookingType: IsRBS", Err.Description)


            End Set
        End Property

        Public Property IsPremium() As Boolean
            '---------------------------------------------------------------------------------------
            ' Procedure : IsPremium
            ' DateTime  : 2003-07-10 15:33
            ' Author    : joho
            ' Purpose   : Returns/sets wether this BookingType should be regarded as a Premium
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IsPremium_Error

                IsPremium = mvarIsPremium

                On Error GoTo 0
                Exit Property

IsPremium_Error:

                Err.Raise(Err.Number, "cBookingType: IsPremium", Err.Description)
            End Get
            Set(ByVal value As Boolean)
                On Error GoTo IsPremium_Error

                mvarIsPremium = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

IsPremium_Error:

                Err.Raise(Err.Number, "cBookingType: IsPremium", Err.Description)

            End Set
        End Property

        Public Property IsSpecific() As Boolean
            '---------------------------------------------------------------------------------------
            ' Procedure : IsSpecific
            ' DateTime  : 2003-07-10 15:33
            ' Author    : joho
            ' Purpose   : Returns/sets wether this BookingType should be regarded as a specific
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IsSpecific_Error

                IsSpecific = mvarIsSpecific

                On Error GoTo 0
                Exit Property

IsSpecific_Error:

                Err.Raise(Err.Number, "cBookingType: IsSpecific", Err.Description)
            End Get
            Set(ByVal value As Boolean)
                On Error GoTo IsSpecific_Error

                mvarIsSpecific = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

IsSpecific_Error:

                Err.Raise(Err.Number, "cBookingType: IsSpecific", Err.Description)

            End Set
        End Property

        Public Property IsSponsorship() As Boolean
            '---------------------------------------------------------------------------------------
            ' Procedure : IsSponsorship
            ' DateTime  : 2003-07-10 15:33
            ' Author    : joho
            ' Purpose   : Returns/sets wether this BookingType should be regarded as a Sponsorship. Must be used in conjunction with RBS or Specific
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IsSponsorship_Error

                IsSponsorship = mvarIsSponsorship

                On Error GoTo 0
                Exit Property

IsSponsorship_Error:

                Err.Raise(Err.Number, "cBookingType: IsSponsorship", Err.Description)
            End Get
            Set(ByVal value As Boolean)
                On Error GoTo IsSponsorship_Error

                mvarIsSponsorship = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

IsSponsorship_Error:

                Err.Raise(Err.Number, "cBookingType: IsSponsorship", Err.Description)

            End Set
        End Property

        Public Sub SetAdedgeTarget(ByVal TmpTarget As Trinity.cPricelistTarget, ByVal TargetName As String, ByVal TargetType As String, ByVal TargetGroup As String)

            If TargetName = "" Then
                If TargetType <> "" AndAlso TargetType > 0 Then
                    TmpTarget.Target.TargetType = TargetType
                    'Main.InternalAdedge.clearTargetSelection()
                    On Error Resume Next
                    Main.InternalAdedge.setTargetUserDefined(TargetGroup, TargetName, True)
                    If Err.Number <> 0 Then
                        frmFindTarget.ShowDialog("The target '" & TargetGroup & "' does not exist. Please choose replacement.")
                        TmpTarget.Target.TargetGroup = frmFindTarget.tvwTargets.SelectedNode.Tag
                        TmpTarget.Target.TargetName = frmFindTarget.tvwTargets.SelectedNode.Text
                    Else
                        TmpTarget.Target.TargetGroup = TargetGroup
                        TmpTarget.Target.TargetName = TargetGroup
                    End If
                End If
                TmpTarget.Target.TargetName = TargetName
            Else
                TmpTarget.Target.TargetName = TargetName
            End If
        End Sub

        Sub ReadDefaultDayparts()
            Dim XMLDaypartDoc As New Xml.XmlDocument
            Dim XMLSharedDaypartDoc As New Xml.XmlDocument
            Dim XMLDayparts As Xml.XmlElement
            Dim XMLSharedDayparts As Xml.XmlElement

            If Dayparts.Count > 0 Then Exit Sub
            If IO.File.Exists(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Main.Area & "\Dayparts.xml") Then
                XMLDaypartDoc.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Main.Area & "\Dayparts.xml")
            End If
            If IO.File.Exists(TrinitySettings.ActiveDataPath & Main.Area & "\Dayparts.xml") Then
                XMLSharedDaypartDoc.Load(TrinitySettings.ActiveDataPath & Main.Area & "\Dayparts.xml")
            Else
                XMLSharedDaypartDoc = XMLDaypartDoc
            End If
            XMLDayparts = XMLDaypartDoc.GetElementsByTagName("Dayparts")(0)
            XMLSharedDayparts = XMLSharedDaypartDoc.GetElementsByTagName("Dayparts")(0)

            If XMLDayparts IsNot Nothing OrElse XMLSharedDayparts IsNot Nothing Then
                If XMLSharedDayparts IsNot Nothing AndAlso XMLSharedDayparts.SelectNodes("Channel[@Name='" & ParentChannel.ChannelName & "']/Bookingtype[@Name='" & Name & "']/*") IsNot Nothing AndAlso XMLSharedDayparts.SelectNodes("Channel[@Name='" & ParentChannel.ChannelName & "']/Bookingtype[@Name='" & Name & "']/*").Count > 0 Then
                    For Each XMLDaypart As Xml.XmlElement In XMLSharedDayparts.SelectNodes("Channel[@Name='" & ParentChannel.ChannelName & "']/Bookingtype[@Name='" & Name & "']/*")
                        Dim _dp As New cDaypart
                        _dp.Name = XMLDaypart.GetAttribute("Name")
                        _dp.StartMaM = XMLDaypart.GetAttribute("StartMaM")
                        _dp.EndMaM = XMLDaypart.GetAttribute("EndMaM")
                        _dp.IsPrime = XMLDaypart.GetAttribute("IsPrime")
                        Dayparts.Add(_dp)
                    Next
                ElseIf XMLSharedDayparts IsNot Nothing AndAlso XMLSharedDayparts.SelectNodes("Channel[@Name='" & ParentChannel.ChannelName & "']/Default/*") IsNot Nothing AndAlso XMLSharedDayparts.SelectNodes("Channel[@Name='" & ParentChannel.ChannelName & "']/Default/*").Count > 0 Then
                    For Each XMLDaypart As Xml.XmlElement In XMLSharedDayparts.SelectNodes("Channel[@Name='" & ParentChannel.ChannelName & "']/Default/*")
                        Dim _dp As New cDaypart
                        _dp.Name = XMLDaypart.GetAttribute("Name")
                        _dp.StartMaM = XMLDaypart.GetAttribute("StartMaM")
                        _dp.EndMaM = XMLDaypart.GetAttribute("EndMaM")
                        _dp.IsPrime = XMLDaypart.GetAttribute("IsPrime")
                        Dayparts.Add(_dp)
                    Next
                ElseIf XMLSharedDayparts IsNot Nothing AndAlso XMLSharedDayparts.SelectNodes("Default/*") IsNot Nothing AndAlso XMLSharedDayparts.SelectNodes("Default/*").Count > 0 Then
                    For Each XMLDaypart As Xml.XmlElement In XMLSharedDayparts.SelectNodes("Default/*")
                        Dim _dp As New cDaypart
                        _dp.Name = XMLDaypart.GetAttribute("Name")
                        _dp.StartMaM = XMLDaypart.GetAttribute("StartMaM")
                        _dp.EndMaM = XMLDaypart.GetAttribute("EndMaM")
                        _dp.IsPrime = XMLDaypart.GetAttribute("IsPrime")
                        Dayparts.Add(_dp)
                    Next
                ElseIf XMLDayparts IsNot Nothing AndAlso XMLDayparts.SelectNodes("Channel[@Name='" & ParentChannel.ChannelName & "']/Bookingtype[@Name='" & Name & "']/*") IsNot Nothing AndAlso XMLDayparts.SelectNodes("Channel[@Name='" & ParentChannel.ChannelName & "']/Bookingtype[@Name='" & Name & "']/*").Count > 0 Then
                    For Each XMLDaypart As Xml.XmlElement In XMLDayparts.SelectNodes("Channel[@Name='" & ParentChannel.ChannelName & "']/Bookingtype[@Name='" & Name & "']/*")
                        Dim _dp As New cDaypart
                        _dp.Name = XMLDaypart.GetAttribute("Name")
                        _dp.StartMaM = XMLDaypart.GetAttribute("StartMaM")
                        _dp.EndMaM = XMLDaypart.GetAttribute("EndMaM")
                        _dp.IsPrime = XMLDaypart.GetAttribute("IsPrime")
                        Dayparts.Add(_dp)
                    Next
                ElseIf XMLDayparts IsNot Nothing AndAlso XMLDayparts.SelectNodes("Channel[@Name='" & ParentChannel.ChannelName & "']/Default/*") IsNot Nothing AndAlso XMLDayparts.SelectNodes("Channel[@Name='" & ParentChannel.ChannelName & "']/Default/*").Count > 0 Then
                    For Each XMLDaypart As Xml.XmlElement In XMLDayparts.SelectNodes("Channel[@Name='" & ParentChannel.ChannelName & "']/Default/*")
                        Dim _dp As New cDaypart
                        _dp.Name = XMLDaypart.GetAttribute("Name")
                        _dp.StartMaM = XMLDaypart.GetAttribute("StartMaM")
                        _dp.EndMaM = XMLDaypart.GetAttribute("EndMaM")
                        _dp.IsPrime = XMLDaypart.GetAttribute("IsPrime")
                        Dayparts.Add(_dp)
                    Next
                ElseIf XMLDayparts IsNot Nothing AndAlso XMLDayparts.SelectNodes("Default/*") IsNot Nothing AndAlso XMLDayparts.SelectNodes("Default/*").Count > 0 Then
                    For Each XMLDaypart As Xml.XmlElement In XMLDayparts.SelectNodes("Default/*")
                        Dim _dp As New cDaypart
                        _dp.Name = XMLDaypart.GetAttribute("Name")
                        _dp.StartMaM = XMLDaypart.GetAttribute("StartMaM")
                        _dp.EndMaM = XMLDaypart.GetAttribute("EndMaM")
                        _dp.IsPrime = XMLDaypart.GetAttribute("IsPrime")
                        Dayparts.Add(_dp)
                    Next
                End If
            End If
            If Dayparts.Count = 0 Then
                Dayparts = TrinitySettings.DefaultDayparts(Me)
            End If
        End Sub

        ' Get the amount of total count of activities in this XML document. Made to be used with ease with a progress bar
        Private Function GetPriceListCount(XMLBTPrice As XmlElement)

            If XMLBTPrice Is Nothing Then Return 0

            Return XMLBTPrice.SelectNodes("Targets/Target/PriceRows/PriceRow").Count + XMLBTPrice.SelectNodes("Targets/Target/Indexes/Index").Count

        End Function

        ' Read the pricelist from an xml file
        Private Sub ReadPricelist(XMLBTPrice As XmlElement, XMLDayparts As XmlElement, ByVal Target As String, ByVal isPriceCheck As Boolean, IsStandardTarget As Boolean)
            Dim XMLTmpNode As Xml.XmlElement
            Dim XMLTmpNode2 As Xml.XmlElement
            Dim TmpTarget As cPricelistTarget
            Dim TmpTargetCPP As cPricelistPeriod
            Dim TmpIndex As cIndex
            Dim i As Integer

            Dim _progress As Integer = 0
            Dim _count As Integer = 0

            If XMLBTPrice Is Nothing Then Exit Sub

            'Added when DefaultDayparts was moved from cPricelisttarget to cBookingtype
            If Pricelist.Targets.Count = 0 Then
                If XMLBTPrice.GetAttribute("DP1") <> "" Then
                    For i = 1 To Dayparts.Count
                        If Not Byte.TryParse(XMLBTPrice.GetAttribute("DP" & i), Me.DefaultDaypart(i - 1)) Then
                            Me.DefaultDaypart(i - 1) = 0
                        End If
                    Next
                End If
                mvarAverageRating = XMLBTPrice.GetAttribute("AverageRating")
            End If

            If XMLDayparts IsNot Nothing Then
                Dayparts.Clear()
                For Each XMLDaypart As Xml.XmlElement In XMLDayparts
                    Dim _dp As New cDaypart
                    _dp.Name = XMLDaypart.GetAttribute("Name")
                    _dp.StartMaM = XMLDaypart.GetAttribute("StartMaM")
                    _dp.EndMaM = XMLDaypart.GetAttribute("EndMaM")
                    _dp.IsPrime = XMLDaypart.GetAttribute("IsPrime")
                    Dayparts.Add(_dp)
                Next
            End If

            If XMLBTPrice.GetAttribute("IsUserEditable") <> "" Then mvarUserEditable = XMLBTPrice.GetAttribute("IsUserEditable")

            XMLTmpNode = XMLBTPrice.FirstChild.FirstChild

            'Price is the node name in the XML before the intruduction of Indexes in the pricelists.
            'When Indexes where intruduced we renamned the node to Target
            If XMLTmpNode IsNot Nothing AndAlso XMLTmpNode.Name = "Price" Then
                _count = XMLTmpNode.ParentNode.ChildNodes.Count
                While Not XMLTmpNode Is Nothing
                    If (Target = "" OrElse XMLTmpNode.GetAttribute("Target") = Target) AndAlso Pricelist.Targets(XMLTmpNode.GetAttribute("Target")) Is Nothing Then
                        TmpTarget = mvarPriceList.Targets.Add(XMLTmpNode.GetAttribute("Target"), Me)
                        TmpTarget.CalcCPP = XMLTmpNode.GetAttribute("CalcCPP")
                        TmpTarget.StandardTarget = IsStandardTarget
                        TmpTarget.Target.NoUniverseSize = True
                        TmpTarget.Target.Universe(isPriceCheck) = mvarParentChannel.BuyingUniverse
                        TmpTarget.Bookingtype = Me
                        If XMLTmpNode.GetAttribute("AdEdgeTarget") Is Nothing OrElse XMLTmpNode.GetAttribute("AdEdgeTarget") = "" Then
                            If XMLTmpNode.GetAttribute("TargetType") <> "" AndAlso XMLTmpNode.GetAttribute("TargetType") > 0 Then
                                TmpTarget.Target.TargetType = XMLTmpNode.GetAttribute("TargetType")
                                'Main.InternalAdedge.clearTargetSelection()
                                On Error Resume Next
                                Main.InternalAdedge.setTargetUserDefined(XMLTmpNode.GetAttribute("TargetGroup"), XMLTmpNode.GetAttribute("AdedgeTarget"), True)
                                If Err.Number <> 0 Then
                                    frmFindTarget.ShowDialog("The target '" & XMLTmpNode.GetAttribute("AdedgeTarget") & "' does not exist. Please choose replacement.")
                                    TmpTarget.Target.TargetGroup = frmFindTarget.tvwTargets.SelectedNode.Tag
                                    TmpTarget.Target.TargetName = frmFindTarget.tvwTargets.SelectedNode.Text
                                Else
                                    TmpTarget.Target.TargetGroup = XMLTmpNode.GetAttribute("TargetGroup")
                                    TmpTarget.Target.TargetName = XMLTmpNode.GetAttribute("AdedgeTarget")
                                End If
                            End If
                            TmpTarget.Target.TargetName = XMLTmpNode.GetAttribute("AdedgeTarget")
                        Else
                            TmpTarget.Target.TargetName = XMLTmpNode.GetAttribute("AdEdgeTarget")
                        End If
                        On Error GoTo 0

                        'Old pricelists are saved with indexes not CPP.
                        ' If the pricelist are saved with CPP we need to convert it

                        XMLTmpNode2 = XMLTmpNode.FirstChild
                        'some old pricelists has no index row
                        If XMLTmpNode2 Is Nothing Then
                            TmpTargetCPP = TmpTarget.PricelistPeriods.Add("All Year")
                            TmpTargetCPP.FromDate = New Date(2007, 1, 1)
                            TmpTargetCPP.ToDate = New Date(2007, 12, 31)
                            TmpTargetCPP.TargetNat = XMLTmpNode.GetAttribute("TargetNat")
                            TmpTargetCPP.TargetUni = XMLTmpNode.GetAttribute("TargetUni")
                            TmpTargetCPP.Price(True) = XMLTmpNode.GetAttribute("CPP")

                            For i = 0 To Dayparts.Count - 1
                                Dim cpp As String = XMLTmpNode.GetAttribute("CPP_DP" & i + 1)
                                If cpp = "" Then cpp = "0"
                                TmpTargetCPP.Price(True, i) = cpp
                            Next
                        End If

                        While Not XMLTmpNode2 Is Nothing
                            'we have two types of pricelists, one with indexes (OLD) and one with actual CPP (NEW ones)
                            If XMLTmpNode2.Name = "Index" Then
                                TmpTargetCPP = TmpTarget.PricelistPeriods.Add(XMLTmpNode2.GetAttribute("Name"))
                                TmpTargetCPP.FromDate = XMLTmpNode2.GetAttribute("FromDate")
                                TmpTargetCPP.ToDate = XMLTmpNode2.GetAttribute("ToDate")
                                TmpTargetCPP.TargetNat = XMLTmpNode.GetAttribute("TargetNat")
                                TmpTargetCPP.TargetUni = XMLTmpNode.GetAttribute("TargetUni")
                                TmpTargetCPP.Price(True) = XMLTmpNode2.GetAttribute("Index") * XMLTmpNode.GetAttribute("CPP") / 100

                                For i = 0 To Dayparts.Count - 1
                                    Dim index As String = XMLTmpNode2.GetAttribute("IndexDP" & i)
                                    Dim cpp As String = XMLTmpNode.GetAttribute("CPP_DP" & i + 1)
                                    If index = "" Then index = "0"
                                    If cpp = "" Then cpp = "0"

                                    TmpTargetCPP.Price(True, i) = index * cpp / 100
                                Next
                            Else
                                TmpTargetCPP = TmpTarget.PricelistPeriods.Add(XMLTmpNode2.GetAttribute("Name"))
                                TmpTargetCPP.FromDate = XMLTmpNode2.GetAttribute("FromDate")
                                TmpTargetCPP.ToDate = XMLTmpNode2.GetAttribute("ToDate")
                                TmpTargetCPP.TargetNat = XMLTmpNode2.GetAttribute("TargetNat")
                                TmpTargetCPP.TargetUni = XMLTmpNode2.GetAttribute("TargetUni")
                                TmpTargetCPP.PriceIsCPP = XMLTmpNode2.GetAttribute("isCPP")
                                TmpTargetCPP.Price(XMLTmpNode2.GetAttribute("isCPP")) = XMLTmpNode2.GetAttribute("Price")
                                For i = 0 To Dayparts.Count - 1
                                    If XMLTmpNode2.GetAttribute("PriceDP" & i) <> "" Then
                                        TmpTargetCPP.Price(XMLTmpNode2.GetAttribute("isCPP"), i) = XMLTmpNode2.GetAttribute("PriceDP" & i)
                                    End If
                                Next
                            End If
                            XMLTmpNode2 = XMLTmpNode2.NextSibling
                        End While
                    End If
                    XMLTmpNode = XMLTmpNode.NextSibling

                    _progress += 1
                    RaiseEvent OnPriceListUpdate(Me, New Trinity.PriceListEventArgs(_progress, TmpTarget, Me))

                End While
            Else 'we have a Target node meaning we might have indexes in the pricelist
                If XMLTmpNode IsNot Nothing Then _count = XMLTmpNode.ParentNode.ChildNodes.Count
                While Not XMLTmpNode Is Nothing
                    If (Target = "" OrElse XMLTmpNode.GetAttribute("Target") = Target) AndAlso Pricelist.Targets(XMLTmpNode.GetAttribute("Target")) Is Nothing Then
                        TmpTarget = mvarPriceList.Targets.Add(XMLTmpNode.GetAttribute("Target"), Me)
                        If XMLTmpNode.GetAttribute("IsUserEditable") <> "" Then TmpTarget.IsUserEditable = XMLTmpNode.GetAttribute("IsUserEditable")
                        TmpTarget.CalcCPP = XMLTmpNode.GetAttribute("CalcCPP")
                        TmpTarget.StandardTarget = IsStandardTarget
                        TmpTarget.Target.NoUniverseSize = True
                        TmpTarget.Target.Universe(isPriceCheck) = mvarParentChannel.BuyingUniverse
                        If XMLTmpNode.GetAttribute("MaxRatings") <> "" Then
                            TmpTarget.MaxRatings = CSng(XMLTmpNode.GetAttribute("MaxRatings"))
                        Else
                            TmpTarget.MaxRatings = 0
                        End If

                        TmpTarget.Bookingtype = Me
                        If XMLTmpNode.GetAttribute("AdEdgeTarget") Is Nothing OrElse XMLTmpNode.GetAttribute("AdEdgeTarget") = "" Then
                            If XMLTmpNode.GetAttribute("TargetType") <> "" AndAlso XMLTmpNode.GetAttribute("TargetType") > 0 Then
                                TmpTarget.Target.TargetType = XMLTmpNode.GetAttribute("TargetType")
                                On Error Resume Next
                                Main.InternalAdedge.setTargetUserDefined(XMLTmpNode.GetAttribute("TargetGroup"), XMLTmpNode.GetAttribute("AdedgeTarget"), True)
                                If Err.Number <> 0 Then
                                    If Not PickedTargetsList.ContainsKey(XMLTmpNode.GetAttribute("AdedgeTarget")) Then
                                        frmFindTarget.ShowDialog("The target '" & XMLTmpNode.GetAttribute("AdedgeTarget") & "' does not exist. Please choose replacement.")
                                        TmpTarget.Target.TargetGroup = frmFindTarget.tvwTargets.SelectedNode.Tag
                                        TmpTarget.Target.TargetName = frmFindTarget.tvwTargets.SelectedNode.Text
                                        Dim kv As New KeyValuePair(Of String, String)
                                        PickedTargetsList.Add(XMLTmpNode.GetAttribute("AdedgeTarget"), New With {.Group = TmpTarget.Target.TargetGroup, .Name = TmpTarget.Target.TargetName})
                                    Else
                                        TmpTarget.Target.TargetGroup = PickedTargetsList(XMLTmpNode.GetAttribute("AdedgeTarget")).Group
                                        TmpTarget.Target.TargetName = PickedTargetsList(XMLTmpNode.GetAttribute("AdedgeTarget")).Name
                                    End If
                                Else
                                    TmpTarget.Target.TargetGroup = XMLTmpNode.GetAttribute("TargetGroup")
                                    TmpTarget.Target.TargetName = XMLTmpNode.GetAttribute("AdedgeTarget")
                                End If
                            End If
                            TmpTarget.Target.TargetName = XMLTmpNode.GetAttribute("AdedgeTarget")
                        Else
                            TmpTarget.Target.TargetName = XMLTmpNode.GetAttribute("AdEdgeTarget")
                        End If
                        On Error GoTo 0

                        'Read pricerows
                        XMLTmpNode2 = XMLTmpNode.FirstChild.FirstChild
                        While Not XMLTmpNode2 Is Nothing
                            TmpTargetCPP = TmpTarget.PricelistPeriods.Add(XMLTmpNode2.GetAttribute("Name"))
                            TmpTargetCPP.FromDate = XMLTmpNode2.GetAttribute("FromDate")
                            TmpTargetCPP.ToDate = XMLTmpNode2.GetAttribute("ToDate")
                            TmpTargetCPP.TargetNat = XMLTmpNode2.GetAttribute("TargetNat")
                            TmpTargetCPP.TargetUni = XMLTmpNode2.GetAttribute("TargetUni")
                            TmpTargetCPP.PriceIsCPP = XMLTmpNode2.GetAttribute("isCPP")
                            TmpTargetCPP.Price(XMLTmpNode2.GetAttribute("isCPP")) = XMLTmpNode2.GetAttribute("Price")
                            For i = 0 To Dayparts.Count - 1
                                'If Me.Name = "Specifics" And Me.ParentChannel.ChannelName = "TV2" Then
                                'TmpTargetCPP.Price(XMLTmpNode2.GetAttribute("isCPP"), i) = 1.25 * XMLTmpNode2.GetAttribute("PriceDP" & i)
                                'Else
                                If XMLTmpNode2.GetAttribute("PriceDP" & i) = "" Then
                                    TmpTargetCPP.Price(XMLTmpNode2.GetAttribute("isCPP"), i) = 0
                                Else
                                    TmpTargetCPP.Price(XMLTmpNode2.GetAttribute("isCPP"), i) = XMLTmpNode2.GetAttribute("PriceDP" & i)
                                End If
                            Next

                            XMLTmpNode2 = XMLTmpNode2.NextSibling
                        End While

                        'read Indexes
                        XMLTmpNode2 = XMLTmpNode.LastChild.FirstChild
                        While Not XMLTmpNode2 Is Nothing
                            Dim _useThis As Boolean = True
                            If TmpTarget.Indexes.Exists(XMLTmpNode2.GetAttribute("ID")) Then
                                _useThis = TmpTarget.Indexes(XMLTmpNode2.GetAttribute("ID")).UseThis
                            End If
                            TmpTarget.Indexes.Add(XMLTmpNode2.GetAttribute("Name"), XMLTmpNode2.GetAttribute("ID"))
                            TmpTarget.Indexes(XMLTmpNode2.GetAttribute("ID")).FromDate = XMLTmpNode2.GetAttribute("FromDate")
                            TmpTarget.Indexes(XMLTmpNode2.GetAttribute("ID")).ToDate = XMLTmpNode2.GetAttribute("ToDate")
                            TmpTarget.Indexes(XMLTmpNode2.GetAttribute("ID")).Index = XMLTmpNode2.GetAttribute("Index")
                            TmpTarget.Indexes(XMLTmpNode2.GetAttribute("ID")).IndexOn = XMLTmpNode2.GetAttribute("IndexOn")
                            TmpTarget.Indexes(XMLTmpNode2.GetAttribute("ID")).SystemGenerated = XMLTmpNode2.GetAttribute("SystemGenerated")
                            TmpTarget.Indexes(XMLTmpNode2.GetAttribute("ID")).UseThis = _useThis
                            If Not XMLTmpNode2.FirstChild Is Nothing Then
                                Dim EnXML As XmlElement = XMLTmpNode2.FirstChild.FirstChild

                                While Not EnXML Is Nothing
                                    With TmpTarget.Indexes(XMLTmpNode2.GetAttribute("ID")).Enhancements.Add
                                        .ID = EnXML.GetAttribute("ID")
                                        .Name = EnXML.GetAttribute("Name")
                                        .Amount = EnXML.GetAttribute("Amount")
                                    End With

                                    EnXML = EnXML.NextSibling
                                End While
                            End If

                            'get next index
                            _progress += 1
                            RaiseEvent OnPriceListUpdate(Me, New Trinity.PriceListEventArgs(_progress, TmpTarget, Me))
                            XMLTmpNode2 = XMLTmpNode2.NextSibling
                        End While
                    End If
                    XMLTmpNode = XMLTmpNode.NextSibling
                End While
            End If
        End Sub

        Public Function GetPricelistCount(Optional ByVal Area As String = "")
            Dim XMLBTPrice As Xml.XmlElement
            Dim XMLDoc As New Xml.XmlDocument
            If (TrinitySettings.ConnectionStringCommon <> "") Then
                Return 0
            Else

                If Area = "" Then
                    Area = Main.Area
                End If

                'First read prislists from the common path
                If _pricelist <> "" AndAlso My.Computer.FileSystem.FileExists(TrinitySettings.ActiveDataPath & Main.Area & "\Pricelists\" + _pricelist & ".xml") Then
                    XMLDoc.Load(TrinitySettings.ActiveDataPath & Main.Area & "\Pricelists\" + _pricelist & ".xml")
                    XMLBTPrice = XMLDoc.GetElementsByTagName("Pricelist").Item(0).SelectSingleNode("Price")
                    Return GetPricelistCount(XMLBTPrice)
                End If
                'Add targets found in the network path for this company
                If My.Computer.FileSystem.FileExists(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Main.Area & "\Pricelists\" + _pricelist & ".xml") Then

                    XMLDoc.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Main.Area & "\Pricelists\" + _pricelist & ".xml")
                    XMLBTPrice = XMLDoc.GetElementsByTagName("Pricelist").Item(0).SelectSingleNode("Price[@Name='" & mvarName & "']")

                    Return GetPricelistCount(XMLBTPrice)
                End If
                If My.Computer.FileSystem.FileExists(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Main.Area & "\Pricelists\" + mvarParentChannel.ChannelName & ".xml") Then
                    XMLDoc.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Main.Area & "\Pricelists\" + mvarParentChannel.ChannelName & ".xml")
                    XMLBTPrice = XMLDoc.GetElementsByTagName("Pricelist").Item(0).SelectSingleNode("Price[@Name='" & mvarName & "']")

                    Return GetPricelistCount(XMLBTPrice)
                End If
            End If
            Helper.WriteToLogFile("Found " & mvarPriceList.Targets.Count & " targets")

            Return 0
        End Function

        Public Sub ReadPricelist(Optional ByVal Area As String = "", Optional ByVal Target As String = "", Optional ByVal isPriceCheck As Boolean = False)
            'isPriceCheck is True when we are just loading prices to compare with the current loaded one

            'the optional target are used when comparing the current pricelists in the campaign with the saved ones
            'that way we dont have to go though the entire pricelist

            '(TrinitySettings.ConnectionStringCommon <> "")
            Dim XMLBTPrice As Xml.XmlElement
            Dim XMLDoc As New Xml.XmlDocument

            If (TrinitySettings.ConnectionStringCommon <> "") Then
                DBReader.readPricelist(Me)
                Helper.WriteToLogFile("Done reading pricelist form database")
            Else

                If Area = "" Then
                    Area = Main.Area
                End If

                mvarPriceList.Targets.Clear()

                'First read prislists from the common path
                If _pricelist <> "" AndAlso My.Computer.FileSystem.FileExists(TrinitySettings.ActiveDataPath & Main.Area & "\Pricelists\" + _pricelist & ".xml") Then
                    Helper.WriteToLogFile("Reading pricelist from " & TrinitySettings.ActiveDataPath & Main.Area & "\Pricelists\" + _pricelist & ".xml")
                    XMLDoc.Load(TrinitySettings.ActiveDataPath & Main.Area & "\Pricelists\" + _pricelist & ".xml")
                    XMLBTPrice = XMLDoc.GetElementsByTagName("Pricelist").Item(0).SelectSingleNode("Price")
                    ReadPricelist(XMLBTPrice, XMLDoc.GetElementsByTagName("Pricelist").Item(0).SelectSingleNode("Dayparts"), Target, isPriceCheck, True)
                End If
                'Add targets found in the network path for this company
                If My.Computer.FileSystem.FileExists(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Main.Area & "\Pricelists\" + _pricelist & ".xml") Then
                    Helper.WriteToLogFile("Reading pricelist from " & TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Main.Area & "\Pricelists\" + _pricelist & ".xml")
                    XMLDoc.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Main.Area & "\Pricelists\" + _pricelist & ".xml")
                    XMLBTPrice = XMLDoc.GetElementsByTagName("Pricelist").Item(0).SelectSingleNode("Price[@Name='" & mvarName & "']")
                    ReadPricelist(XMLBTPrice, XMLDoc.GetElementsByTagName("Pricelist").Item(0).SelectSingleNode("Dayparts"), Target, isPriceCheck, False)
                End If
                If My.Computer.FileSystem.FileExists(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Main.Area & "\Pricelists\" + mvarParentChannel.ChannelName & ".xml") Then
                    Helper.WriteToLogFile("Reading pricelist from " & TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Main.Area & "\Pricelists\" + mvarParentChannel.ChannelName & ".xml")
                    XMLDoc.Load(TrinitySettings.DataPath(cSettings.SettingsLocationEnum.locNetwork) & Main.Area & "\Pricelists\" + mvarParentChannel.ChannelName & ".xml")
                    XMLBTPrice = XMLDoc.GetElementsByTagName("Pricelist").Item(0).SelectSingleNode("Price[@Name='" & mvarName & "']")
                    ReadPricelist(XMLBTPrice, XMLDoc.GetElementsByTagName("Pricelist").Item(0).SelectSingleNode("Dayparts"), Target, isPriceCheck, False)
                End If
            End If
            Helper.WriteToLogFile("Found " & mvarPriceList.Targets.Count & " targets")
        End Sub


        Public Function TotalTRP(Optional ByVal IncludeCompensation As Boolean = True) As Single

            'If mvarTotalTRPChanged Then
            Dim TmpWeek As cWeek
            Dim TmpTRP As Single = 0

            For Each TmpWeek In mvarWeeks
                TmpTRP = TmpTRP + TmpWeek.TRP
            Next
            If IncludeCompensation Then
                For Each TmpComp As Trinity.cCompensation In mvarCompensations
                    TmpTRP += TmpComp.TRPMainTarget
                Next
            End If
            mvarTotalTRP = TmpTRP
            mvarTotalTRPChanged = False
            'End If

            Return mvarTotalTRP
        End Function

        Public Function TotalTRPBuyingTarget() As Single

            Dim TmpWeek As cWeek
            Dim _trp As Single

            For Each TmpWeek In mvarWeeks
                _trp += TmpWeek.TRPBuyingTarget
            Next
            Return _trp
        End Function

        Public Property EstimatedSpotCount() As Integer
            '---------------------------------------------------------------------------------------
            ' Procedure : EstimatedSpotCount
            ' DateTime  : 2003-08-21 15:00
            ' Author    : joho
            ' Purpose   : Returns/Sets the estimated number of spots for this BookingType
            '             When set, the new AverageRating is calculated
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo EstimatedSpotCount_Error

                '-1.#IND value on mvarAverageRating makes true if not like this
                If AverageRating < 0 Or AverageRating > 0 Then
                    EstimatedSpotCount = TotalTRPBuyingTarget() / mvarAverageRating
                Else
                    EstimatedSpotCount = 0
                End If

                On Error GoTo 0
                Exit Property

EstimatedSpotCount_Error:

                Err.Raise(Err.Number, "cBookingType: EstimatedSpotCount", Err.Description)
            End Get
            Set(ByVal value As Integer)
                On Error GoTo EstimatedSpotCount_Error

                If value <> 0 Then
                    AverageRating = TotalTRPBuyingTarget() / value
                Else
                    AverageRating = 0
                End If
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

EstimatedSpotCount_Error:

                Err.Raise(Err.Number, "cBookingType: EstimatedSpotCount", Err.Description)


            End Set
        End Property

        Public ReadOnly Property ConfirmedSpotCount() As Integer
            '---------------------------------------------------------------------------------------
            ' Procedure : ConfirmedSpotCount
            ' DateTime  : 2003-08-21 15:00
            ' Author    : joho
            ' Purpose   : Returns the number of spots that has been confirmed for this BookingType
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo ConfirmedSpotCount_Error

                Dim TmpSpot As cPlannedSpot

                For Each TmpSpot In Main.PlannedSpots

                    If TmpSpot.Bookingtype Is Me Then
                        ConfirmedSpotCount = ConfirmedSpotCount + 1
                    End If

                Next
                On Error GoTo 0
                Exit Property

ConfirmedSpotCount_Error:

                Err.Raise(Err.Number, "cBookingType: ConfirmedSpotCount", Err.Description)
            End Get

        End Property


        Public ReadOnly Property PlannedSpotCount() As Integer
            '---------------------------------------------------------------------------------------
            ' Procedure : ConfirmedSpotCount
            ' DateTime  : 2003-08-21 15:00
            ' Author    : joho
            ' Purpose   : Returns the number of spots that has been confirmed for this BookingType
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo PlannedSpotCount_Error

                Dim TmpSpot As cBookedSpot

                For Each TmpSpot In Main.BookedSpots

                    If TmpSpot.Bookingtype Is Me Then
                        PlannedSpotCount = PlannedSpotCount + 1
                    End If

                Next
                On Error GoTo 0
                Exit Property

PlannedSpotCount_Error:

                Err.Raise(Err.Number, "cBookingType: PlannedSpotCount", Err.Description)
            End Get

        End Property

        Public ReadOnly Property PlannedGrossBudget()
            Get
                If Not IsCompensation Then
                    Dim TmpWeek As cWeek
                    Dim GB As Decimal

                    GB = 0
                    For Each TmpWeek In mvarWeeks
                        GB = GB + TmpWeek.GrossBudget
                    Next
                    Return GB
                Else
                    Return 0
                End If
            End Get
        End Property

        ''' <summary>
        ''' Returns Booked TRP. For specifics it returns TRP from BookedSpots and for RBS it returns allocated ratings in Buying target
        ''' </summary><returns></returns>
        Public Function BookedTRP30() As Single
            If IsSpecific Then
                Dim _bookedTRP = (From _spot As Trinity.cBookedSpot In mvarMain.BookedSpots Where _spot.Bookingtype Is Me Select _spot.ChannelEstimate * (_spot.Film.Index / 100)).Sum
                If _bookedTRP > 0 Then
                    Return _bookedTRP
                Else
                    Return PlannedTRP30(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
                End If
            Else
                Return PlannedTRP30(cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)
            End If
        End Function

        Public Function PlannedTRP30(ByVal Target As cPlannedSpot.PlannedTargetEnum) As Single

            Dim TmpWeek As cWeek
            Dim TRPSum As Single

            If Target = cPlannedSpot.PlannedTargetEnum.pteMainTarget Then

                For Each TmpWeek In mvarWeeks
                    TRPSum = TRPSum + TmpWeek.TRP * (TmpWeek.SpotIndex / 100)
                Next
                PlannedTRP30 = TRPSum

            ElseIf Target = cPlannedSpot.PlannedTargetEnum.pteBuyingTarget Then

                For Each TmpWeek In mvarWeeks
                    TRPSum = TRPSum + TmpWeek.TRPBuyingTarget * (TmpWeek.SpotIndex / 100)
                Next
                PlannedTRP30 = TRPSum

            End If

        End Function

        Public Function PlannedCPP30() As Decimal
            Return PlannedNetBudget() / PlannedTRP30()
        End Function

        Public Function PlannedTRP(ByVal Target As cPlannedSpot.PlannedTargetEnum) As Single

            Dim TmpWeek As cWeek
            Dim TRPSum As Single

            If Target = cPlannedSpot.PlannedTargetEnum.pteMainTarget Then

                For Each TmpWeek In mvarWeeks
                    TRPSum = TRPSum + TmpWeek.TRP
                Next
                Return TRPSum

            ElseIf Target = cPlannedSpot.PlannedTargetEnum.pteBuyingTarget Then

                For Each TmpWeek In mvarWeeks
                    TRPSum = TRPSum + TmpWeek.TRPBuyingTarget
                Next
                Return TRPSum

            End If

        End Function

        Public Function SpotIndex() As Single

            Dim TmpWeek As cWeek
            Dim TmpFilm As cFilm
            Dim TRPs() As Single
            Dim TRPSum As Single
            Dim x As Integer
            Dim TmpIndex As Single

            ReDim TRPs(mvarWeeks(1).Films.Count)

            For Each TmpWeek In mvarWeeks
                x = 1
                For Each TmpFilm In TmpWeek.Films
                    TRPs(x) = TRPs(x) + TmpWeek.TRP * (TmpFilm.Share / 100)
                    TRPSum = TRPSum + TmpWeek.TRP * (TmpFilm.Share / 100)
                    x = x + 1
                Next
            Next
            TmpIndex = 0
            For x = 1 To mvarWeeks(1).Films.Count
                If TRPSum > 0 Then
                    TmpIndex = TmpIndex + (TRPs(x) / TRPSum) * mvarWeeks(1).Films(x).Index
                Else
                    TmpIndex = 0
                End If
            Next
            SpotIndex = TmpIndex / 100
        End Function

        Public ReadOnly Property Indexes() As cIndexes
            Get
                Indexes = mvarIndexes
            End Get
        End Property

        Public Property AddedValues() As cAddedValues
            Get
                mvarAddedValues.Bookingtype = Me
                AddedValues = mvarAddedValues
            End Get
            Set(ByVal value As cAddedValues)
                mvarAddedValues = value
                RaiseEvent BookingtypeChanged(Me)
            End Set
        End Property

        Public Property FilmIndex(ByVal Length As Integer) As Single
            Get
                Try
                    FilmIndex = mvarFilmIndex(Length)
                Catch

                End Try
            End Get
            Set(ByVal value As Single)
                mvarFilmIndex(Length) = value
                RaiseEvent BookingtypeChanged(Me)
            End Set
        End Property

        Public Function GetWeek(ByVal d As Date) As cWeek
            Dim TmpWeek As cWeek

            GetWeek = Nothing
            For Each TmpWeek In mvarWeeks
                If TmpWeek.StartDate <= Int(d.ToOADate) Then
                    If TmpWeek.EndDate >= Int(d.ToOADate) Then
                        GetWeek = TmpWeek
                        Exit For
                    End If
                End If
            Next
        End Function

        Public Property MarathonNetBudget() As Decimal
            '---------------------------------------------------------------------------------------
            ' Procedure : MarathonNetBudget
            ' DateTime  : 2003-07-10 15:32
            ' Author    : joho
            ' Purpose   : Returns/sets the Net Budget Confirmed by the channel in their
            '             booking confirmation.
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo MarathonNetBudget_Error

                MarathonNetBudget = mvarMarathonNetBudget

                On Error GoTo 0
                Exit Property

MarathonNetBudget_Error:

                Err.Raise(Err.Number, "cBookingType: MarathonNetBudget", Err.Description)
            End Get
            Set(ByVal value As Decimal)
                On Error GoTo MarathonNetBudget_Error

                mvarMarathonNetBudget = value
                RaiseEvent BookingtypeChanged(Me)

                On Error GoTo 0
                Exit Property

MarathonNetBudget_Error:

                Err.Raise(Err.Number, "cBookingType: MarathonNetBudget", Err.Description)

            End Set
        End Property

        Public Sub New(ByVal Main As cKampanj)
            mvarIndexAllAdults = 100
            mvarIndexMainTarget = 100
            mvarIndexSecondTarget = 100
            mvarIsVisible = True
            mvarPriceList = New cPricelist(Main)
            mvarBuyingTarget = New cPricelistTarget(Main, Me)
            mvarBuyingTarget.Bookingtype = Me
            If Main Is Nothing Then
                Throw New Exception("Empty campaign passed when creating new campaign.")
            End If
            MainObject = Main
            mvarWeeks = New cWeeks(Main)
            mvarWeeks.Bookingtype = Me
            mvarIndexes = New cIndexes(Main, Me)
            Main.RegisterProblemDetection(Me)
            mvarAddedValues = New cAddedValues(Main)

            AddHandler mvarWeeks.TRPChanged, AddressOf _trpChanged
            AddHandler mvarWeeks.WeekChanged, AddressOf _weekChanged
            AddHandler mvarWeeks.FilmChanged, AddressOf _filmChanged

        End Sub

        Protected Overrides Sub Finalize()
            mvarBuyingTarget = Nothing

            mvarWeeks = Nothing
            mvarPriceList = Nothing
            mvarParentChannel = Nothing
            mvarIndexes = Nothing
            MyBase.Finalize()
        End Sub

        Private Sub mvarNetValue_UpdateRequested(ByRef e As AggregateEventArgs, ByVal sender As Object) Handles mvarNetValue.UpdateRequested
            Dim TmpBudget As Decimal = 0

            For Each TmpSpot As cActualSpot In mvarActualSpots
                'If TmpSpot.MatchedSpot IsNot Nothing AndAlso TmpSpot.MatchedSpot.PriceNet > 0 Then
                ' TmpBudget += TmpSpot.MatchedSpot.PriceNet
                ' Else
                If TmpSpot.Bookingtype Is Me Then
                    TmpBudget += TmpSpot.ActualNetValue
                End If
                'End If
            Next
            e.Value = TmpBudget

        End Sub

        Private _dayparts As New cDayparts(Me)
        Public Property Dayparts() As cDayparts
            Get
                Return _dayparts
            End Get
            Set(ByVal value As cDayparts)
                _dayparts = value
            End Set
        End Property

        Private _mainDaypart As New Dictionary(Of Byte, Decimal)
        Public Function MainDaypartSplit(ByVal Daypart As Byte) As Single
            If _mainDaypart.ContainsKey(Daypart) Then
                Return _mainDaypart(Daypart)
            Else
                Dim _mainDP As cDaypart = Main.Dayparts(Daypart)
                _mainDaypart.Add(Daypart, 0)
                For Each _dp As cDaypart In _dayparts
                    If Not (_dp.StartMaM > _mainDP.EndMaM OrElse _dp.EndMaM < _mainDP.StartMaM) Then
                        Dim _percentPerMinute As Decimal = _dp.Share / (_dp.EndMaM - _dp.StartMaM)
                        Dim _overlappingMinutes As Integer = (_dp.EndMaM - _dp.StartMaM)
                        If _dp.StartMaM < _mainDP.StartMaM Then
                            _overlappingMinutes -= _mainDP.StartMaM - _dp.StartMaM
                        End If
                        If _dp.EndMaM > _mainDP.EndMaM Then
                            _overlappingMinutes -= _dp.EndMaM - _mainDP.EndMaM
                        End If
                        _mainDaypart(Daypart) += _overlappingMinutes * _percentPerMinute
                    ElseIf (_dp.EndMaM > 24 * 60) Then
                        Dim _startMam As Long = 0
                        Dim _endMam As Long = _dp.EndMaM - 24 * 60
                        If Not (_startMam > _mainDP.EndMaM OrElse _endMam < _mainDP.StartMaM) Then
                            Dim _percentPerMinute As Decimal = _dp.Share / (_endMam - _startMam)
                            Dim _overlappingMinutes As Integer = (_endMam - _startMam)
                            If _startMam < _mainDP.StartMaM Then
                                _overlappingMinutes -= _mainDP.StartMaM - _startMam
                            End If
                            If _endMam > _mainDP.EndMaM Then
                                _overlappingMinutes -= _endMam - _mainDP.EndMaM
                            End If
                            _mainDaypart(Daypart) += _overlappingMinutes * _percentPerMinute
                        End If
                    ElseIf (_mainDP.EndMaM > 24 * 60) Then
                        Dim _startMam As Long = 0
                        Dim _endMam As Long = _mainDP.EndMaM - 24 * 60
                        If Not (_dp.StartMaM > _endMam OrElse _dp.EndMaM < _startMam) Then
                            Dim _percentPerMinute As Decimal = _dp.Share / (_dp.EndMaM - _dp.StartMaM)
                            Dim _overlappingMinutes As Integer = (_dp.EndMaM - _dp.StartMaM)
                            If _dp.StartMaM < _startMam Then
                                _overlappingMinutes -= _startMam - _dp.StartMaM
                            End If
                            If _dp.EndMaM > _endMam Then
                                _overlappingMinutes -= _dp.EndMaM - _endMam
                            End If
                            _mainDaypart(Daypart) += _overlappingMinutes * _percentPerMinute
                        End If
                    End If
                Next
                Return _mainDaypart(Daypart)
            End If
        End Function

        Public Sub InvalidateMainDaypartSplit()
            _mainDaypart = New Dictionary(Of Byte, Decimal)
        End Sub

        Public Function updateBookingTypeInfoFromDatabase() As Boolean
            Return DBReader.updateBookingTypeInfo(Me)
        End Function

        Public Function saveBookingTypeToDatabase() As Boolean
            Dim returnValue As Boolean = False

            returnValue = DBReader.saveBookingTypeInfo(Me)

            Return returnValue
        End Function

        Public Enum BookingTypeProblems
            DaypartSumNot100 = 1
            NoBuyingTarget = 2
            MarathonNetDiffersFromConfirmedNet = 3
            DayNotCovered = 4
            IndexMainTargetWrong = 5
            IndexSecondTargetWrong = 6
            IndexAllAdultsWrong = 7
        End Enum

        Public Function DetectProblems() As List(Of cProblem) Implements IDetectsProblems.DetectProblems
            If Not Me.BookIt Then Return New List(Of cProblem)
            Dim _problems As New List(Of cProblem)


            If Me.IndexSecondTarget <= 0 Or Me.IndexSecondTarget > 300 Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Index toward second target is " & Me.IndexSecondTarget & "</p>")
                _helpText.AppendLine("<p>The second target index for " & Me.BuyingTarget.TargetName & " is " & Me.IndexSecondTarget & ". This may be an error.</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Run Natural Delivery or enter a proper index in Allocate -> Indices</p>")
                Dim _problem As New cProblem(BookingTypeProblems.IndexSecondTargetWrong, cProblem.ProblemSeverityEnum.Warning, "Index toward second target is " & Me.IndexSecondTarget, Me.ToString, _helpText.ToString, Me)

                _problems.Add(_problem)
            End If

            If Me.IndexMainTarget <= 0 Or Me.IndexMainTarget > 300 Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Index toward main target is " & Me.IndexMainTarget & "</p>")
                _helpText.AppendLine("<p>The main target index for " & Me.BuyingTarget.TargetName & " is " & Me.IndexMainTarget & ". This may be an error.</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Run Natural Delivery or enter a proper index in Allocate -> Indices</p>")
                Dim _problem As New cProblem(BookingTypeProblems.IndexMainTargetWrong, cProblem.ProblemSeverityEnum.Warning, "Index toward main target is " & Me.IndexMainTarget, Me.ToString, _helpText.ToString, Me)

                _problems.Add(_problem)
            End If

            If Me.IndexAllAdults <= 0 Or Me.IndexAllAdults > 300 Then
                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Index toward All Adults is " & Me.IndexAllAdults & "</p>")
                _helpText.AppendLine("<p>The all adults index for " & Me.BuyingTarget.TargetName & " is " & Me.IndexAllAdults & ". This may be an error.</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Run Natural Delivery or enter a proper index in Allocate -> Indices</p>")
                Dim _problem As New cProblem(BookingTypeProblems.IndexAllAdultsWrong, cProblem.ProblemSeverityEnum.Warning, "Index toward all adults is " & Me.IndexAllAdults, Me.ToString, _helpText.ToString, Me)

                _problems.Add(_problem)
            End If

            'Detect daypart definition problems
            Dim minutesCovered As Long = Dayparts.Count - 1
            For Each _dp As cDaypart In Dayparts
                minutesCovered += _dp.EndMaM - _dp.StartMaM
            Next
            If minutesCovered <> 1439 Then


                Dim _helpText As New Text.StringBuilder

                _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Daypart does not cover an entire day</p>")
                _helpText.AppendLine("<p>The daypart definition for booking type " & Me.ToString & " is incorrect. Either the entire 24 hours of a day is not " & _
                                     "covered, or more than this is covered</p>")
                _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                _helpText.AppendLine("<p>Solve the problem by editing the daypart definition in Settings -> Define Dayparts</p>")
                Dim _problem As New cProblem(BookingTypeProblems.DayNotCovered, cProblem.ProblemSeverityEnum.Warning, "Daypart does not cover an entire day", Me.ToString, _helpText.ToString, Me)

                _problems.Add(_problem)



            End If
            'Detect daypartsplit problems
            Dim Sum As Single = 0
            For Each _dp As cDaypart In Dayparts
                Sum += _dp.Share
            Next
            If Math.Round(Sum) <> 100 Then
                Dim HelpText As New Text.StringBuilder

                HelpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Daypart split does not sum up to 100%</p>")
                HelpText.AppendLine("<p>The daypart split for '" & Name & "' does not sum up to 100%. All bookingtypes need to have a daypart split summing up to 100% or the following functions may not function correctly:</p>")
                HelpText.AppendLine("<ul><li>Calculating natural deliveries</li><li>Estimating reach and frequency</li></ul>")
                HelpText.AppendLine("<p>If you do not intend to do this then you can disregard this warning.</p>")
                HelpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                HelpText.AppendLine("<p>Open the 'Setup'-window, click the 'Channels'-tab and set appropriate daypartsplits in the top-most grid. If you do not know the daypart split, you can have Trinity calculate it for you by clicking the calculator icon.</p>")


                Dim _problem As New cProblem(BookingTypeProblems.DaypartSumNot100, cProblem.ProblemSeverityEnum.Warning, "Daypart split does not sum up to 100%", ToString, HelpText.ToString, Me)
                _problems.Add(_problem)
            End If
            If BuyingTarget Is Nothing Then
                Dim HelpText As New Text.StringBuilder

                HelpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Bookingtype does not have a buying target</p>")
                HelpText.AppendLine("<p>The bookingtype '" & ToString() & "' does not have a buying target set</p>")
                HelpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                HelpText.AppendLine("<p>Open the 'Setup'-window, click the 'Channels'-tab and set a buying target for '" & ToString() & "'</p>")

                Dim _problem As New cProblem(BookingTypeProblems.NoBuyingTarget, cProblem.ProblemSeverityEnum.Error, "Bookingtype does not have a buying target", ToString, HelpText.ToString, Me)
                _problems.Add(_problem)
            End If
            If TrinitySettings.MarathonEnabled AndAlso MarathonNetBudget > 0 AndAlso MarathonNetBudget <> ConfirmedNetBudget Then
                Dim HelpText As New Text.StringBuilder

                HelpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>The confirmed budget does not match the budget sent to Marathon</p>")
                HelpText.AppendLine("<p>The bookingtype '" & ToString() & "' has a confirmed budget of " & ConfirmedNetBudget & " but the budget sent to Marathon was " & MarathonNetBudget & ".</p>")
                HelpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                HelpText.AppendLine("<p><ul><li>If the Marathon budget is correct, open the 'Budget'-window and correct the Confirmed budget for '" & ToString() & "'</li>")
                HelpText.AppendLine("<li>If the Confirmed budget is correct, make sure to correct the budget in Marathon either by sending additional debit-/credit invoices or on the next campaign</li></ul></p>")

                Dim _problem As New cProblem(BookingTypeProblems.NoBuyingTarget, cProblem.ProblemSeverityEnum.Warning, "The confirmed budget does not match the budget sent to Marathon", ToString, HelpText.ToString, Me)
                _problems.Add(_problem)
            End If

            If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
            Return _problems
        End Function

        Function BudgetDaypartSplit(Daypart As Integer) As Single
            If BuyingTarget Is Nothing OrElse Not BuyingTarget.CalcCPP Then
                Return Dayparts(Daypart).Share
            Else
                Dim _sum As Single = 0
                Dim _totSum As Single = 0
                For Each _week As cWeek In Weeks
                    _sum += _week.TRPBuyingTarget * (_week.SpotIndex / 100) * (_week.Bookingtype.Dayparts(Daypart).Share / 100) * _week.GrossCPP30(Daypart) * (1 - _week.Discount)
                    _totSum += _week.NetBudget
                Next
                Return (_sum / _totSum) * 100
            End If
        End Function

        Public Event ProblemsFound(problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound


        ' Returns true if there are any spots attached to this 
        Public Function SpotsExist() As Boolean

            ' Get the amount of spots that exists on this Channel and bookingtype
            Dim SpotAmountBooked As Integer = 0
            For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots

                If (TmpSpot.Bookingtype Is Me) Then
                    SpotAmountBooked += 1
                End If
            Next

            ' Get the amount of spots that exists on this Channel and bookingtype
            Dim SpotAmountActual As Integer = 0
            For Each TmpSpot As Trinity.cActualSpot In Campaign.ActualSpots

                If (TmpSpot.Bookingtype Is Me) Then
                    SpotAmountActual += 1
                End If
            Next

            ' Get the amount of spots that exists on this Channel and bookingtype
            Dim SpotAmountPlanned As Integer = 0
            For Each TmpSpot As Trinity.cPlannedSpot In Campaign.PlannedSpots

                If (TmpSpot.Bookingtype Is Me) Then
                    SpotAmountPlanned += 1
                End If
            Next


            If SpotAmountActual > 0 Or SpotAmountBooked > 0 Or SpotAmountPlanned > 0 Then
                Return True
            End If

            Return False

        End Function

        ' Remove all the spots that are attached to this bookingtype, public so other function can call it
        Public Sub RemoveAllSpots()

            ' List to gather all the spots to be removed as they cannot be removed during iteration over the collection
            Dim bookedSpotList As New List(Of Trinity.cBookedSpot)
            Dim actualSpotList As New List(Of Trinity.cActualSpot)
            Dim plannedSpotList As New List(Of Trinity.cPlannedSpot)

            'Interate over all the spots and remove them
            For Each TmpSpot As Trinity.cBookedSpot In Campaign.BookedSpots

                If (TmpSpot.Bookingtype Is Me) Then
                    bookedSpotList.Add(TmpSpot)
                End If
            Next

            ' Get the amount of spots that exists on this Channel and bookingtype
            Dim SpotAmountActual As Integer = 0
            For Each TmpSpot As Trinity.cActualSpot In Campaign.ActualSpots

                If (TmpSpot.Bookingtype Is Me) Then
                    actualSpotList.Add(TmpSpot)
                End If
            Next

            ' Get the amount of spots that exists on this Channel and bookingtype
            Dim SpotAmountPlanned As Integer = 0
            For Each TmpSpot As Trinity.cPlannedSpot In Campaign.PlannedSpots

                If (TmpSpot.Bookingtype Is Me) Then
                    plannedSpotList.Add(TmpSpot)
                End If
            Next

            ' Loop trough all items that are flagged for deletion
            For Each tmpSpot As Trinity.cBookedSpot In bookedSpotList
                Campaign.BookedSpots.Remove(tmpSpot.ID)
            Next

            ' Loop trough all items that are flagged for deletion
            For Each tmpSpot As Trinity.cActualSpot In actualSpotList
                Campaign.ActualSpots.Remove(tmpSpot.ID)
            Next

            ' Loop trough all items that are flagged for deletion
            For Each tmpSpot As Trinity.cPlannedSpot In plannedSpotList
                Campaign.PlannedSpots.Remove(tmpSpot.ID)
            Next

            ' Update the gridviews that are dependent on this data
            frmSpots.UpdateActual(False, True)
            frmSpots.UpdateConfirmed(False, True)

            ' Recreate the advantage list
            Campaign.CreateAdedgeSpots()

        End Sub

    End Class
End Namespace