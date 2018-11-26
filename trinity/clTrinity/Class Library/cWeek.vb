Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cWeek
        Implements IDetectsProblems

        'the class cWeek represents several days, not nessesary a "real" week but usually

        Private mvarFilms As cFilms 'A collection of films to be used in the campaign
        Private mvarTRPBuyingTarget As Single
        Private mvarNetBudget As Decimal
        Private mvarStartDate As Long 'where the "week" starts (not nessesary a monday)
        Private mvarEndDate As Long 'where the "week" ends
        Private mvarName As String = ""
        Private mvarControlSaved As Boolean
        Private mvarControlSent As Boolean
        Private mvarControlConfirmed As Boolean
        Private mvarControlSentToClient As Boolean
        Private mvarControlInvoiced As Boolean
        Private mvarIsVisible As Boolean
        Private mvarActualTRPBuying As Decimal

        ' The variable below is set to true when TRP is regarded as the fixed value.
        ' When this is the case, all CPP changes influence the Net budget.
        ' If TRPControl is set to false all CPP changes influences the TRPs

        Public TRPControl As Boolean

        Private Main As cKampanj
        Private mvarBookingtype As cBookingType 'The booking type used
        Private ParentColl As Collection

        Private mvarIsLocked As Boolean

        Public Enum ProblemsEnum
            TooManyDays = 1
        End Enum

        ' Event to raise whenever the trp has been changed
        Public Event TRPChanged(ByVal sender As Object, ByVal e As WeekEventArgs)

        Public Event FilmChanged(Film As cFilm)
        Public Event WeekChanged(Week As cWeek)

        Private Sub _filmChanged(Film As cFilm)
            RaiseEvent FilmChanged(Film)
            RaiseEvent WeekChanged(Me)
        End Sub

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument, ByVal WeekNum As Integer) As Boolean
            'this function saves the collection to the xml provided
            'it will return True of succeded and false if failed

            On Error GoTo On_Error

            'Added values
            Dim XMLAV As Xml.XmlElement = xmlDoc.CreateElement("AddedValues")
            mvarBookingtype.AddedValues.GetXML(XMLAV, errorMessege, xmlDoc, WeekNum)
            colXml.AppendChild(XMLAV)

            'Save Films
            Dim XMLFilms As Xml.XmlElement = xmlDoc.CreateElement("Films")
            Me.Films.GetXML(XMLFilms, errorMessege, xmlDoc)
            colXml.AppendChild(XMLFilms)


            colXml = xmlDoc.CreateElement("Week") 'as String
            colXml.SetAttribute("Name", Me.Name)
            colXml.SetAttribute("TRPControl", Me.TRPControl)
            colXml.SetAttribute("TRP", Me.TRP) 'as Single
            colXml.SetAttribute("TRPBuyingTarget", Me.TRPBuyingTarget) 'as Single
            colXml.SetAttribute("TRPAllAdults", Me.TRPAllAdults) 'as Single
            colXml.SetAttribute("GrossBudget", Me.GrossBudget)
            colXml.SetAttribute("NetBudget", Me.NetBudget) 'as Currency
            colXml.SetAttribute("Discount", Me.Discount(True)) 'as Single
            colXml.SetAttribute("StartDate", Me.StartDate) 'as Long
            colXml.SetAttribute("EndDate", Me.EndDate) 'as Long
            colXml.SetAttribute("NetCPP30", Me.NetCPP30)


            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving week" & Me.Name)
            Return False
        End Function

        Public Property IsLocked() As Boolean
            'this function is used for locking the BT in Allocate
            'Default is not locked
            Get
                Return mvarIsLocked
            End Get
            Set(ByVal value As Boolean)
                mvarIsLocked = value
                RaiseEvent WeekChanged(Me)
            End Set
        End Property

        Private _recalculateActualTRPBuying As Boolean = True
        Sub InvalidateActualTRPBuying()
            _recalculateActualTRPBuying = True
        End Sub

        Friend ReadOnly Property ActualTRPBuying() As Decimal
            Get
                If _recalculateActualTRPBuying Then
                    mvarActualTRPBuying = 0
                    For Each _spot As cActualSpot In Main.ActualSpots
                        If _spot.Week Is Me Then
                            mvarActualTRPBuying += _spot.Rating30(cActualSpot.ActualTargetEnum.ateBuyingTarget)
                        End If
                    Next
                    _recalculateActualTRPBuying = False
                    RaiseEvent WeekChanged(Me)
                End If
                Return mvarActualTRPBuying
            End Get
            'Set(ByVal value As Decimal)
            '    mvarActualTRPBuying = value
            '    RaiseEvent WeekChanged(Me)
            'End Set
        End Property

        Friend WriteOnly Property ParentCollection()
            '---------------------------------------------------------------------------------------
            ' Procedure : ParentCollection
            ' DateTime  : 2003-07-07 13:18
            ' Author    : joho
            ' Purpose   : Sets the Collection of wich this week is a member. This is used
            '             when a new week Name is set. See that property for further explanation
            '---------------------------------------------------------------------------------------
            '
            Set(ByVal value)
                ParentColl = value
            End Set
        End Property

        Friend Property Bookingtype() As Object
            Get
                Return mvarBookingtype
            End Get
            Set(ByVal value As Object)
                mvarBookingtype = value
                mvarFilms.Bookingtype = value
                RaiseEvent WeekChanged(Me)
            End Set
        End Property

        Friend WriteOnly Property MainObject()
            Set(ByVal value)
                Main = value
                mvarFilms.MainObject = value
                RaiseEvent WeekChanged(Me)
            End Set
        End Property

        Public ReadOnly Property NetCPP(Optional ByVal dp As Integer = -1) As Decimal     'Optional ByVal Effective As Boolean = False) As Decimal
            '---------------------------------------------------------------------------------------
            ' Procedure : NetCPP
            ' DateTime  : 2003-07-12 11:39
            ' Author    : joho
            ' Purpose   : Returns/sets the actual Net CPP for this week

            '---------------------------------------------------------------------------------------
            '
            Get
                Return NetCPP30(dp) * (SpotIndex() / 100)
            End Get
        End Property

        Public ReadOnly Property GrossCPP() As Decimal
            '---------------------------------------------------------------------------------------
            ' Procedure : NetCPP
            ' DateTime  : 2003-07-12 11:39
            ' Author    : joho
            ' Purpose   : Returns/sets the actual Net CPP for this week

            '---------------------------------------------------------------------------------------
            '
            Get
                Dim SpotIdx = (SpotIndex(True) / 100)
                If SpotIdx = 0 Then SpotIdx = 1
                Return GrossCPP30 * SpotIdx
            End Get

            'old function replaced 2007-05-31 by Kurki
            'Get
            '    Dim TmpCPP As Decimal = 0
            '    Dim SpotIdx = (SpotIndex(True) / 100)
            '    Dim TmpDebug As Boolean = False
            '    If SpotIdx = 0 Then SpotIdx = 1
            '    For i As Integer = mvarStartDate To mvarEndDate
            '        For dp As Integer = 0 To Main.DaypartCount - 1
            '            Dim TmpIdx As Decimal
            '            If mvarBookingtype.BuyingTarget.CalcCPP Then
            '                TmpIdx = ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, dp) * (mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, dp))) / 10000)
            '                TmpCPP += mvarBookingtype.BuyingTarget.CPPDaypart(dp) * TmpIdx * SpotIdx * AddedValueIndexGross() * (mvarBookingtype.DaypartSplit(dp) / 100)
            '            Else
            '                TmpIdx = ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, -1) * (mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, -1))) / 10000)
            '                TmpCPP += mvarBookingtype.BuyingTarget.CPP * TmpIdx * SpotIdx * AddedValueIndexGross() * (mvarBookingtype.DaypartSplit(dp) / 100)
            '            End If
            '            If TmpDebug Then
            '                Debug.Print(TmpCPP)
            '            End If
            '        Next
            '        TmpDebug = False
            '    Next
            '    Return TmpCPP / (mvarEndDate - mvarStartDate + 1)
            'End Get
        End Property

        Public ReadOnly Property GrossCPP30(Optional Daypart As Integer = -1) As Decimal
            '---------------------------------------------------------------------------------------
            ' Procedure : NetCPP
            ' DateTime  : 2003-07-12 11:39
            ' Author    : joho
            ' Purpose   : Returns/sets the actual Net CPP for this week

            '---------------------------------------------------------------------------------------
            '
            Get
                Dim TmpCPP As Decimal = 0
                For i As Integer = mvarStartDate To mvarEndDate
                    'Dim TmpIdx As Decimal
                    'If mvarBookingtype.BuyingTarget.CalcCPP Then
                    '    For dp As Integer = 0 To Main.DaypartCount - 1
                    '        TmpIdx = ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, dp)) / 100)
                    '        TmpCPP += mvarBookingtype.BuyingTarget.GetCPPForDate(i, dp) * TmpIdx * (mvarBookingtype.DaypartSplit(dp) / 100)
                    '    Next
                    'Else
                    '    TmpIdx = ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, -1)) / 100)
                    '    TmpCPP += mvarBookingtype.BuyingTarget.GetCPPForDate(i) * TmpIdx
                    'End If
                    '****CHANGED BY JOHO 21/10 - 2008. SHOULD BE THE SAME BUT MORE CORRECT AND STRINGENT
                    TmpCPP += mvarBookingtype.GetGrossCPP30ForDate(Date.FromOADate(i), Daypart) * AddedValueIndexGross()
                Next
                Return TmpCPP / (mvarEndDate - mvarStartDate + 1)
            End Get

        End Property

        'Public ReadOnly Property NetCPT30(Optional ByVal Effective As Boolean = False) As Decimal
        '    '---------------------------------------------------------------------------------------
        '    ' Procedure : NetCPP
        '    ' DateTime  : 2003-07-12 11:39
        '    ' Author    : joho
        '    ' Purpose   : Returns/sets the actual Net CPP for this week
        '    ' Arguments : Effective = If set to true then indexes added by the user is taken into acount, if set to false
        '    '             the function returns the base NetCPP
        '    '---------------------------------------------------------------------------------------
        '    '
        '    Get
        '        If mvarBookingtype.BuyingTarget.getUniSizeUni(mvarStartDate) = 0 Then
        '            Return 0
        '        End If
        '        Dim TmpCPP As Decimal = 0
        '        For i As Integer = mvarStartDate To mvarEndDate
        '            Dim TmpIdx As Decimal
        '            If mvarBookingtype.BuyingTarget.CalcCPP Then
        '                For dp As Integer = 0 To Main.DaypartCount - 1
        '                    TmpIdx = 1
        '                    If Effective Then
        '                        TmpIdx = ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP, dp)) / 100)
        '                        TmpIdx *= 1 / ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eTRP, dp)) / 100)
        '                    End If
        '                    If Not mvarBookingtype.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
        '                        TmpIdx *= ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, dp)) / 100)
        '                    End If
        '                    TmpCPP += mvarBookingtype.BuyingTarget.GetCPPForDate(i, dp) * (1 - mvarBookingtype.BuyingTarget.Discount) * TmpIdx * AddedValueIndexNet() * (mvarBookingtype.DaypartSplit(dp) / 100)
        '                Next
        '            Else
        '                TmpIdx = 1
        '                If Effective Then
        '                    TmpIdx *= ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP, -1)) / 100)
        '                    TmpIdx *= 1 / ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eTRP, -1)) / 100)
        '                End If
        '                If Not mvarBookingtype.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
        '                    TmpIdx *= ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, -1) / 100))
        '                End If
        '                TmpCPP += mvarBookingtype.BuyingTarget.NetCPP * TmpIdx * AddedValueIndexNet()
        '            End If
        '        Next
        '        Return ((TmpCPP / (mvarEndDate - mvarStartDate + 1)) / mvarBookingtype.BuyingTarget.getUniSizeUni(mvarStartDate) * 100)
        '    End Get
        'End Property

        '*** BEFORE GROSSINDEX REMOVAL
        '        Public ReadOnly Property NetCPP() As Single
        '            '---------------------------------------------------------------------------------------
        '            ' Procedure : NetCPP
        '            ' DateTime  : 2003-07-12 11:39
        '            ' Author    : joho
        '            ' Purpose   : Returns/sets the actual Net CPP for this week
        '            '---------------------------------------------------------------------------------------
        '            '
        '            Get
        '                On Error GoTo NetCPP_Error

        '                If mvarBookingtype.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
        '                    Return mvarBookingtype.BuyingTarget.NetCPP * Index(True) * (SpotIndex() / 100) * AddedValueIndexNet()
        '                Else
        '                    Return mvarBookingtype.BuyingTarget.NetCPP * Index(True) * (SpotIndex() / 100) * GrossIndex() * AddedValueIndexNet()
        '                End If

        '                On Error GoTo 0
        '                Exit Property

        'NetCPP_Error:

        '                Err.Raise(Err.Number, "cWeek: NetCPP", Err.Description)
        '            End Get

        '        End Property

        '        Public ReadOnly Property NetCPP30(Optional ByVal Effective As Boolean = False) As Decimal
        Public ReadOnly Property NetCPP30(Optional ByVal Daypart As Integer = -1) As Decimal
            '---------------------------------------------------------------------------------------
            ' Procedure : NetCPP
            ' DateTime  : 2003-07-12 11:39
            ' Author    : joho
            ' Purpose   : Returns/sets the actual Net CPP for this week

            '---------------------------------------------------------------------------------------
            '
            Get
                Dim TmpCPP As Decimal = 0
                For i As Integer = mvarStartDate To mvarEndDate
                    'If Daypart = -1 Or Not mvarBookingtype.BuyingTarget.CalcCPP Then
                    '    Dim TmpIdx As Decimal
                    '    If mvarBookingtype.BuyingTarget.CalcCPP Then
                    '        For dp As Integer = 0 To Main.DaypartCount - 1
                    '            TmpIdx = ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP, dp)) / 100)
                    '            'If Effective Then
                    '            TmpIdx *= 1 / ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eTRP, dp)) / 100)
                    '            'End If
                    '            If Not mvarBookingtype.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
                    '                TmpIdx *= (mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, dp) / 100)
                    '            End If
                    '            TmpCPP += mvarBookingtype.BuyingTarget.GetNetCPP30ForDate(i, dp) * TmpIdx * AddedValueIndexNet() * (mvarBookingtype.DaypartSplit(dp) / 100)
                    '        Next
                    '    Else
                    '        TmpIdx = ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP, -1)) / 100)
                    '        'If Effective Then
                    '        TmpIdx *= 1 / ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eTRP, -1)) / 100)
                    '        'End If
                    '        If Not mvarBookingtype.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
                    '            TmpIdx *= ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, -1) / 100))
                    '        End If
                    '        TmpCPP += mvarBookingtype.BuyingTarget.GetNetCPP30ForDate(i, -1) * TmpIdx * AddedValueIndexNet()
                    '    End If
                    'Else
                    '    Dim TmpIdx As Decimal
                    '    TmpIdx = ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP, Daypart)) / 100)
                    '    'If Effective Then
                    '    TmpIdx *= 1 / ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eTRP, Daypart)) / 100)
                    '    'End If
                    '    If Not mvarBookingtype.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
                    '        TmpIdx *= ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, Daypart) / 100))
                    '    End If
                    '    TmpCPP += mvarBookingtype.BuyingTarget.GetNetCPP30ForDate(i, Daypart) * TmpIdx * AddedValueIndexNet()
                    'End If
                    TmpCPP += mvarBookingtype.GetNetCPP30ForDate(Date.FromOADate(i), Daypart) * AddedValueIndexNet()
                Next
                Return TmpCPP / (mvarEndDate - mvarStartDate + 1)


                'edit made 2007-05-31 by Kurki
                'Dim TmpCPP As Decimal = 0
                'For i As Integer = mvarStartDate To mvarEndDate
                '    If Daypart = -1 Or Not mvarBookingtype.BuyingTarget.CalcCPP Then
                '        For dp As Integer = 0 To Main.DaypartCount - 1
                '            Dim TmpIdx As Decimal
                '            If mvarBookingtype.BuyingTarget.CalcCPP Then
                '                TmpIdx = ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP, dp) * (mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP, dp))) / 10000)
                '                'If Effective Then
                '                TmpIdx *= 1 / ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eTRP, dp) * (mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eTRP, dp))) / 10000)
                '                'End If
                '                If Not mvarBookingtype.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
                '                    TmpIdx *= ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, dp) * mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, dp)) / 10000)
                '                End If
                '            Else
                '                TmpIdx = ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP, -1) * (mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP, -1))) / 10000)
                '                'If Effective Then
                '                TmpIdx *= 1 / ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eTRP, -1) * (mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eTRP, -1))) / 10000)
                '                'End If
                '                If Not mvarBookingtype.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
                '                    TmpIdx *= ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, -1) * mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, -1)) / 10000)
                '                End If
                '            End If
                '            If mvarBookingtype.BuyingTarget.CalcCPP Then
                '                TmpCPP += mvarBookingtype.BuyingTarget.CPPDaypart(dp) * (1 - mvarBookingtype.BuyingTarget.Discount) * TmpIdx * AddedValueIndexNet() * (mvarBookingtype.DaypartSplit(dp) / 100)
                '            Else
                '                TmpCPP += mvarBookingtype.BuyingTarget.NetCPP * TmpIdx * AddedValueIndexNet() * (mvarBookingtype.DaypartSplit(dp) / 100)
                '            End If
                '        Next
                '    Else
                '        Dim TmpIdx As Decimal
                '        TmpIdx = ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP, Daypart) * (mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP, Daypart))) / 10000)
                '        'If Effective Then
                '        TmpIdx *= 1 / ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eTRP, Daypart) * (mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eTRP, Daypart))) / 10000)
                '        'End If
                '        If Not mvarBookingtype.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
                '            TmpIdx *= ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, Daypart) * mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, Daypart)) / 10000)
                '        End If
                '        TmpCPP += mvarBookingtype.BuyingTarget.CPPDaypart(Daypart) * (1 - mvarBookingtype.BuyingTarget.Discount) * TmpIdx * AddedValueIndexNet()
                '    End If

                '    'If mvarBookingtype.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
                '    '    Return mvarBookingtype.BuyingTarget.NetCPP * Index(True) * (SpotIndex() / 100) * AddedValueIndexNet()
                '    'Else

                '    'End If
                'Next
                'Return TmpCPP / (mvarEndDate - mvarStartDate + 1)
            End Get
        End Property


        '*** BEFORE GROSSINDEX REMOVAL
        '        Public ReadOnly Property NetCPP30(Optional ByVal Effective As Boolean = False) As Single
        '            Get
        '                On Error GoTo NetCPP30_Error

        '                If mvarBookingtype.BuyingTarget.IsEntered = cPricelistTarget.EnteredEnum.eCPP Then
        '                    Return (mvarBookingtype.BuyingTarget.NetCPP * Index(Effective) * AddedValueIndexNet())
        '                Else
        '                    Return (mvarBookingtype.BuyingTarget.NetCPP * Index(Effective) * GrossIndex() * AddedValueIndexNet())
        '                End If

        '                On Error GoTo 0
        '                Exit Property

        'NetCPP30_Error:

        '                Err.Raise(Err.Number, "cWeek: NetCPP30", Err.Description)
        '            End Get
        '        End Property

        Public ReadOnly Property Films() As cFilms
            '---------------------------------------------------------------------------------------
            ' Procedure : Films
            ' DateTime  : 2003-07-12 11:27
            ' Author    : joho
            ' Purpose   : Pointer to a collection of cFilm, containing each spot to be used
            '             in the campaign
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Films_Error

                Films = mvarFilms

                On Error GoTo 0
                Exit Property

Films_Error:

                Err.Raise(Err.Number, "cWeek: Films", Err.Description)
            End Get
            '            Set(ByVal value As cFilms)
            '                On Error GoTo Films_Error

            '                mvarFilms = value
            '                RaiseEvent WeekChanged(Me)

            '                On Error GoTo 0
            '                Exit Property

            'Films_Error:

            '                Err.Raise(Err.Number, "cWeek: Films", Err.Description)

            '            End Set
        End Property

        Public Property TRP() As Single
            '---------------------------------------------------------------------------------------
            ' Procedure : TRP
            ' DateTime  : 2003-07-12 11:28
            ' Author    : joho
            ' Purpose   : Returns/sets the amount of TRPs for this week in the actual target
            '---------------------------------------------------------------------------------------
            '
            Get
                'TODO: Handle errors here
                'On Error GoTo TRP_Error

                If Not (mvarBookingtype.IsPremium AndAlso mvarBookingtype.IsSpecific) Then
                    Return mvarTRPBuyingTarget * mvarBookingtype.BuyingTarget.UniIndex(mvarStartDate, True) * (mvarBookingtype.IndexMainTarget / 100)
                Else
                    Dim TmpTRP As Single = 0

                    Dim test As Single = (From spot As cBookedSpot In Main.BookedSpots Where spot.week Is Me Select spot.MyEstimate).Sum

                    For Each TmpSpot As cBookedSpot In Main.BookedSpots
                        If TmpSpot.week Is Me Then
                            TmpTRP += TmpSpot.MyEstimate
                        End If
                    Next
                    Return TmpTRP
                End If

                On Error GoTo 0
                Exit Property

TRP_Error:

                Err.Raise(Err.Number, "cWeek: TRP", Err.Description)
            End Get
            Set(ByVal value As Single)

                On Error GoTo TRP_Error

                If mvarBookingtype.BuyingTarget.UniIndex(mvarStartDate, True) <> 0 Then '* (mvarBookingtype.IndexMainTarget / 100)
                    If (mvarBookingtype.IndexMainTarget / 100) > 0 And mvarBookingtype.BuyingTarget.UniIndex(mvarStartDate, True) > 0 Or IsError(value) Then
                        mvarTRPBuyingTarget = value / mvarBookingtype.BuyingTarget.UniIndex(mvarStartDate, True) / (mvarBookingtype.IndexMainTarget / 100)
                    Else
                        mvarTRPBuyingTarget = 0
                    End If
                Else
                    mvarTRPBuyingTarget = 0
                End If
                'TRPControl = True
                If Helper.IsIndefinite(mvarTRPBuyingTarget) Then mvarTRPBuyingTarget = 0
                mvarBookingtype.InvalidateTotalTRP()
                RaiseEvent WeekChanged(Me)

                ' Raise an event that the TRP has been changed
                RaiseEvent TRPChanged(Me, New WeekEventArgs(Me))

                On Error GoTo 0
                Exit Property

TRP_Error:

                Err.Raise(Err.Number, "cWeek: TRP", Err.Description)


            End Set
        End Property

        Public ReadOnly Property TRP30 As Single
            Get
                Return TRP * (SpotIndex() / 100)
            End Get
        End Property

        Public ReadOnly Property TRPBuyingTarget30 As Single
            Get
                Return TRPBuyingTarget * (SpotIndex() / 100)
            End Get
        End Property

        Public ReadOnly Property Impressions000 As Single
            Get
                Return (TRP / 100) * Main.MainTarget.UniSize
            End Get
        End Property

        Public ReadOnly Property ImpressionsBuyingTarget000 As Single
            Get
                Return (TRPBuyingTarget / 100) * mvarBookingtype.BuyingTarget.getUniSizeNat(Main.StartDate)
            End Get
        End Property

        Public Property NetBudget() As Decimal
            '---------------------------------------------------------------------------------------
            ' Procedure : NetBudget
            ' DateTime  : 2003-07-12 11:28
            ' Author    : joho
            ' Purpose   : Returns/sets the Net budget for this week
            '---------------------------------------------------------------------------------------
            '
            Get
                'TODO: Handle errors
                'On Error GoTo NetBudget_Error

                If Not (mvarBookingtype.IsPremium AndAlso mvarBookingtype.IsSpecific) Then
                    RecalculateCPP()
                    NetBudget = mvarNetBudget
                Else
                    Dim Budget As Decimal = 0

                    Dim test As Decimal = (From spot As cBookedSpot In Main.BookedSpots Where spot.week Is Me Select spot.NetPrice * spot.AddedValueIndex).Sum

                    For Each TmpSpot As cBookedSpot In Main.BookedSpots
                        If TmpSpot.week Is Me Then
                            Budget += TmpSpot.NetPrice * TmpSpot.AddedValueIndex
                        End If
                    Next
                    Return Budget
                End If
                On Error GoTo 0
                Exit Property

NetBudget_Error:

                Err.Raise(Err.Number, "cWeek: NetBudget", Err.Description)
            End Get
            Set(ByVal value As Decimal)
                On Error GoTo NetBudget_Error

                mvarNetBudget = value
                If NetCPP = 0 Then
                    TRPBuyingTarget = 0
                Else 'If Not Main.Loading Then
                    TRPBuyingTarget = value / NetCPP
                End If
                'TRPControl = False
                mvarBookingtype.InvalidateTotalTRP()
                RaiseEvent WeekChanged(Me)

                On Error GoTo 0
                Exit Property

NetBudget_Error:

                Err.Raise(Err.Number, "cWeek: NetBudget", Err.Description)

            End Set
        End Property

        Public ReadOnly Property Discount(Optional ByVal Effective As Boolean = False) As Single
            '---------------------------------------------------------------------------------------
            ' Procedure : Discount
            ' DateTime  : 2003-07-12 11:28
            ' Author    : joho
            ' Purpose   : Returns the actual discount for this week
            '---------------------------------------------------------------------------------------
            '
            Get
                Dim NetCPP As Single
                Dim Gross As Single

                On Error GoTo Discount_Error
                Gross = GrossCPP30
                NetCPP = NetCPP30()
                If Effective Then
                    Gross *= (SpotIndex(True) / 100) * AddedValueIndexGross()
                    NetCPP *= (SpotIndex() / 100)
                End If
                If Gross <> 0 Then
                    Discount = 1 - (NetCPP / Gross)
                Else
                    Discount = 1
                End If

                On Error GoTo 0
                Exit Property

Discount_Error:

                Err.Raise(Err.Number, "cWeek: Discount", Err.Description)
            End Get

        End Property

        Public Property StartDate() As Long
            '---------------------------------------------------------------------------------------
            ' Procedure : StartDate
            ' DateTime  : 2003-07-12 11:28
            ' Author    : joho
            ' Purpose   : Returns/sets the date when this week is set to start
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo StartDate_Error

                StartDate = mvarStartDate

                On Error GoTo 0
                Exit Property

StartDate_Error:

                Err.Raise(Err.Number, "cWeek: StartDate", Err.Description)
            End Get
            Set(ByVal value As Long)
                Dim i As Integer
                Dim Added As Boolean

                On Error GoTo StartDate_Error

                mvarStartDate = Math.Floor(value)
                On Error Resume Next
                ParentColl.Remove(mvarName)
                On Error GoTo StartDate_Error
                Added = False
                For i = 1 To ParentColl.Count
                    If ParentColl(i).StartDate >= mvarStartDate Then
                        ParentColl.Add(Me, mvarName, i)
                        Added = True
                        Exit For
                    End If
                Next
                If Not Added Then
                    ParentColl.Add(Me, mvarName)
                End If
                RaiseEvent WeekChanged(Me)

                On Error GoTo 0
                Exit Property

StartDate_Error:

                Err.Raise(Err.Number, "cWeek: StartDate", Err.Description)

            End Set
        End Property

        Public Property EndDate() As Long
            '---------------------------------------------------------------------------------------
            ' Procedure : EndDate
            ' DateTime  : 2003-07-12 11:28
            ' Author    : joho
            ' Purpose   : Returns/sets the date when this week is set to end
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo EndDate_Error

                EndDate = mvarEndDate

                On Error GoTo 0
                Exit Property

EndDate_Error:

                Err.Raise(Err.Number, "cWeek: EndDate", Err.Description)
            End Get
            Set(ByVal value As Long)
                On Error GoTo EndDate_Error

                mvarEndDate = Math.Floor(value)
                RaiseEvent WeekChanged(Me)

                On Error GoTo 0
                Exit Property

EndDate_Error:

                Err.Raise(Err.Number, "cWeek: EndDate", Err.Description)


            End Set
        End Property

        Public Property Name() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : Name
            ' DateTime  : 2003-07-12 11:28
            ' Author    : joho
            ' Purpose   : Returns/sets the name of this week
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Name_Error

                Name = mvarName

                On Error GoTo 0
                Exit Property

Name_Error:

                Err.Raise(Err.Number, "cWeek: Name", Err.Description)
            End Get
            Set(ByVal value As String)
                Dim Ini As New clsIni
                Dim Added As Boolean
                Dim i As Integer

                On Error GoTo Name_Error

                '    If Name <> mvarName And mvarName <> "" Then
                '
                '        Set TmpWeek = ParentColl(mvarName)
                '        ParentColl.Remove mvarName
                '        ParentColl.Add TmpWeek, Name
                '
                '    End If
                If ParentColl.Contains(mvarName) Then
                    ParentColl.Remove(mvarName)
                End If
                mvarName = Trim(value)
                Added = False
                For i = 1 To ParentColl.Count
                    If ParentColl(i).StartDate >= mvarStartDate Then
                        ParentColl.Add(Me, mvarName, i)
                        Added = True
                        Exit For
                    End If
                Next
                If Not Added Then
                    ParentColl.Add(Me, mvarName)
                End If
                Ini = Nothing
                RaiseEvent WeekChanged(Me)
                On Error GoTo 0
                Exit Property

Name_Error:

                Err.Raise(Err.Number, "cWeek: Name", Err.Description)

            End Set
        End Property

        Public Property IsVisible() As Boolean
            '---------------------------------------------------------------------------------------
            ' Procedure : IsVisible
            ' DateTime  : 2003-07-12 11:30
            ' Author    : joho
            ' Purpose   : Returns/sets wether this week will be shown in the charts
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo IsVisible_Error

                IsVisible = mvarIsVisible

                On Error GoTo 0
                Exit Property

IsVisible_Error:

                Err.Raise(Err.Number, "cWeek: IsVisible", Err.Description)
            End Get
            Set(ByVal value As Boolean)
                On Error GoTo IsVisible_Error

                mvarIsVisible = value
                RaiseEvent WeekChanged(Me)

                On Error GoTo 0
                Exit Property

IsVisible_Error:

                Err.Raise(Err.Number, "cWeek: IsVisible", Err.Description)

            End Set
        End Property

        Public Function SpotIndex(Optional ByVal Gross As Boolean = False) As Single
            '---------------------------------------------------------------------------------------
            ' Procedure : SpotIndex
            ' DateTime  : 2003-07-12 12:04
            ' Author    : joho
            ' Purpose   : Returns the spotindex calculated from all the films
            '---------------------------------------------------------------------------------------
            '

            Dim TmpFilm As cFilm

            On Error GoTo SpotIndex_Error

            SpotIndex = 0
            If mvarFilms.Count = 0 Then
                SpotIndex = 100
            Else
                For Each TmpFilm In mvarFilms
                    If Gross And Main.FilmindexAsDiscount Then
                        SpotIndex += (TmpFilm.Share(cFilm.FilmShareEnum.fseTRP) / 100) * (TmpFilm.GrossIndex)
                    Else
                        SpotIndex += (TmpFilm.Share(cFilm.FilmShareEnum.fseTRP) / 100) * (TmpFilm.Index)
                    End If
                Next
            End If

            On Error GoTo 0
            Exit Function

SpotIndex_Error:

            Err.Raise(Err.Number, "cWeek: SpotIndex", Err.Description)


        End Function

        Public Property TRPBuyingTarget() As Single
            '---------------------------------------------------------------------------------------
            ' Procedure : TRPBuyingTarget
            ' DateTime  : 2003-07-13 17:20
            ' Author    : joho
            ' Purpose   : Returns/sets the amount of TRPs for this week in the buying target
            '---------------------------------------------------------------------------------------
            '
            Get
                'TODO: Handle error
                'On Error GoTo TRPBuyingTarget_Error

                If Not (mvarBookingtype.IsPremium AndAlso mvarBookingtype.IsSpecific) Then
                    TRPBuyingTarget = mvarTRPBuyingTarget
                Else
                    Dim TmpTRP As Single = 0
                    For Each TmpSpot As cBookedSpot In Main.BookedSpots
                        If TmpSpot.week Is Me Then
                            TmpTRP += TmpSpot.MyEstimateBuyTarget
                        End If
                    Next
                    Dim test As Single = (From spot As cBookedSpot In Main.BookedSpots Where spot.week Is Me Select spot.MyEstimateBuyTarget).Sum
                    Return TmpTRP
                End If

                On Error GoTo 0
                Exit Property

TRPBuyingTarget_Error:

                Err.Raise(Err.Number, "cWeek: TRPBuyingTarget", Err.Description)
            End Get
            Set(ByVal value As Single)
                On Error GoTo TRPBuyingTarget_Error

                mvarTRPBuyingTarget = value
                mvarBookingtype.InvalidateTotalTRP()
                RaiseEvent WeekChanged(Me)

                'TRPControl = True
                On Error GoTo 0
                Exit Property

TRPBuyingTarget_Error:

                Err.Raise(Err.Number, "cWeek: TRPBuyingTarget", Err.Description)

            End Set
        End Property

        Public Property TRPAllAdults() As Single
            '---------------------------------------------------------------------------------------
            ' Procedure : TRPAllAdults
            ' DateTime  : 2003-07-15 11:43
            ' Author    : joho
            ' Purpose   : Returns/sets the amount of TRPs for this week in All adults.
            '             The name 3Plus is kept for backward compatibility
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo TRPAllAdults_Error

                Return TRPBuyingTarget * mvarBookingtype.BuyingTarget.UniIndex(mvarStartDate, True) * (mvarBookingtype.IndexAllAdults / 100)

                On Error GoTo 0
                Exit Property

TRPAllAdults_Error:

                Err.Raise(Err.Number, "cWeek: TRPAllAdults", Err.Description)
            End Get
            Set(ByVal value As Single)
                On Error GoTo TRPAllAdults_Error

                If mvarBookingtype.BuyingTarget.UniIndex(mvarStartDate) <> 0 Then
                    mvarTRPBuyingTarget = value / mvarBookingtype.BuyingTarget.UniIndex(mvarStartDate, True) / (mvarBookingtype.IndexAllAdults / 100)
                Else
                    mvarTRPBuyingTarget = 0
                End If
                mvarBookingtype.InvalidateTotalTRP()
                RaiseEvent WeekChanged(Me)
                'TRPControl = True

                On Error GoTo 0
                Exit Property

TRPAllAdults_Error:

                Err.Raise(Err.Number, "cWeek: TRPAllAdults", Err.Description)


            End Set
        End Property

        'Changed to public 28/12 KK *****************************************
        Public Sub RecalculateCPP()
            If Helper.IsIndefinite(mvarTRPBuyingTarget) Then mvarTRPBuyingTarget = 0
            If TRPControl Then

                mvarNetBudget = mvarTRPBuyingTarget * NetCPP

            Else

                If NetCPP > 0 Then
                    mvarTRPBuyingTarget = mvarNetBudget / NetCPP
                Else
                    mvarTRPBuyingTarget = 0
                End If

            End If
            If Helper.IsIndefinite(mvarNetBudget) Then mvarNetBudget = 0
            RaiseEvent WeekChanged(Me)
        End Sub

        Public Property GrossBudget() As Decimal
            Get
                GrossBudget = GrossCPP * mvarTRPBuyingTarget
            End Get

            Set(value As Decimal)
                mvarTRPBuyingTarget = value / GrossCPP

                NetBudget = NetCPP * TRPBuyingTarget
            End Set

        End Property

        Public ReadOnly Property GrossIndex_remove() As Single
            '---------------------------------------------------------------------------------------
            ' Procedure : GrossIndex
            ' DateTime  : 2003-09-22 13:50
            ' Author    : joho
            ' Purpose   : Returns the Index to be used to calculate Gross CPPs
            '---------------------------------------------------------------------------------------
            '
            Get
                Dim Idx As Single
                Dim i As Integer
                On Error GoTo GrossIndex_Error

                Idx = 0
                For i = mvarStartDate To mvarEndDate
                    If mvarBookingtype.BuyingTarget.CalcCPP Then
                        For dp As Integer = 0 To mvarBookingtype.Dayparts.Count - 1
                            Idx += ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, dp)) / (mvarEndDate - mvarStartDate + 1)) * (mvarBookingtype.Dayparts(dp).Share / 100)
                        Next
                    Else
                        Idx += ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, -1)) / (mvarEndDate - mvarStartDate + 1))
                    End If
                    'Original before introduction of Daypart indexes
                    'Idx = Idx + ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP) * mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP)) / (mvarEndDate - mvarStartDate + 1))
                Next
                Return Format(Idx / 10000, "0.0000000")

                On Error GoTo 0
                Exit Property

GrossIndex_Error:

                Err.Raise(Err.Number, "cWeek: GrossIndex", Err.Description)
            End Get

        End Property

        Public ReadOnly Property ExtraTRP() As Single
            Get
                On Error GoTo ExtraTRP_Error
                Dim TmpIndex As cIndex
                Dim Idx As Single
                Dim i As Integer

                '    If TRPControl Then
                '        ExtraTRP = (TRPBuyingTarget - (TRPBuyingTarget * (SeasonIndex / 100)))
                '    Else
                '        ExtraTRP = (NetBudget / NetCPP) - ((NetBudget / NetCPP) * (SeasonIndex / 100))
                '    End If

                Idx = 0
                For i = mvarStartDate To mvarEndDate
                    Idx = Idx + ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eTRP))) / (mvarEndDate - mvarStartDate + 1)
                Next

                ExtraTRP = Format(mvarTRPBuyingTarget * (Idx / 10000) - mvarTRPBuyingTarget, "0.0000")

                On Error GoTo 0
                Exit Property

ExtraTRP_Error:

                Err.Raise(Err.Number, "cWeek: ExtraTRP", Err.Description)
            End Get
        End Property

        Public Function Index() As Decimal
            Dim Idx As Decimal = 0
            For i As Integer = mvarStartDate To mvarEndDate
                Idx += mvarBookingtype.GetIndexForDate(Date.FromOADate(i)) / (mvarEndDate - mvarStartDate + 1)
            Next
            Return Idx
        End Function

        '*** BEFORE GROSSINDEX REMOVAL
        '        Public Function Index(Optional ByVal Effective As Boolean = False, Optional ByVal IncludeGross As Boolean = False) As Single
        '            Dim Idx As Single
        '            Dim i As Long
        '            Dim ExtraIndex As Single
        '            Dim GrossIndex As Single = 0

        '            On Error GoTo GrossIndex_Error

        '            Idx = 0
        '            For i = mvarStartDate To mvarEndDate
        '                For dp As Integer = 0 To Main.DaypartCount - 1
        '                    Idx += ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP, dp) * (mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP, dp))) / (mvarEndDate - mvarStartDate + 1)) * (mvarBookingtype.DaypartSplit(dp) / 100)
        '                    If Effective Then
        '                        ExtraIndex += ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eTRP, dp) * (mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eTRP, dp))) / (mvarEndDate - mvarStartDate + 1)) * (mvarBookingtype.DaypartSplit(dp) / 100)
        '                    End If
        '                    If IncludeGross Then
        '                        GrossIndex += ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eGrossCPP, dp)) / (mvarEndDate - mvarStartDate + 1)) * (mvarBookingtype.DaypartSplit(dp) / 100)
        '                    End If
        '                Next
        '                ' Old statement before introduction of Daypart index
        '                ' Idx += ((mvarBookingtype.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP) * (mvarBookingtype.BuyingTarget.Indexes.GetIndexForDate(Date.FromOADate(i), cIndex.IndexOnEnum.eNetCPP))) / (mvarEndDate - mvarStartDate + 1))
        '            Next
        '            If IncludeGross Then
        '                Idx = Idx * (GrossIndex / 100)
        '            End If
        '            If Effective Then
        '                If ExtraIndex <> 0 Then
        '                    Index = Format((Idx / 10000) * (10000 / ExtraIndex), "0.000000")
        '                Else
        '                    Index = Format((Idx / 10000), "0.000000")
        '                End If
        '            Else
        '                Index = Format((Idx / 10000), "0.000000")
        '            End If
        '            If IncludeGross Then

        '            End If
        '            If Index = 0 Then
        '                Index = 1
        '            End If
        '            On Error GoTo 0
        '            Exit Function

        'GrossIndex_Error:

        '            Err.Raise(Err.Number, "cWeek: GrossIndex", Err.Description)

        '        End Function

        Public Function AddedValueIndexNet()
            Dim TmpAV As cAddedValue
            Dim i As Integer
            Dim TmpWeek As cWeek
            Dim TmpIndex As Single

            i = 1
            For Each TmpWeek In mvarBookingtype.Weeks
                If TmpWeek Is Me Then
                    Exit For
                End If
                i = i + 1
            Next
            TmpIndex = 1


            For Each TmpAV In mvarBookingtype.AddedValues
                If TmpAV.UseThis Then
                    If Main.MultiplyAddedValues Then
                        TmpIndex *= (100 + (TmpAV.IndexNet - 100) * (TmpAV.Amount(i) / 100)) / 100
                    Else
                        TmpIndex += ((TmpAV.IndexNet - 100) * (TmpAV.Amount(i) / 100)) / 100
                    End If
                End If
            Next
            AddedValueIndexNet = TmpIndex
        End Function

        Public Function AddedValueIndexGross()
            Dim TmpAV As cAddedValue
            Dim i As Integer
            Dim TmpWeek As cWeek
            Dim TmpIndex As Single

            i = 1
            For Each TmpWeek In mvarBookingtype.Weeks
                If TmpWeek Is Me Then
                    Exit For
                End If
                i = i + 1
            Next
            TmpIndex = 1
            For Each TmpAV In mvarBookingtype.AddedValues
                If TmpAV.UseThis Then
                    If Main.MultiplyAddedValues Then
                        TmpIndex *= (100 + (TmpAV.IndexGross - 100) * (TmpAV.Amount(i) / 100)) / 100
                    Else
                        TmpIndex += ((TmpAV.IndexGross - 100) * (TmpAV.Amount(i) / 100)) / 100
                    End If
                End If
            Next
            AddedValueIndexGross = TmpIndex
        End Function

        Public Sub New(ByVal MainObject As cKampanj)
            mvarIsVisible = True
            Main = MainObject
            mvarFilms = New cFilms(Main, Me)
            AddHandler mvarFilms.FilmChanged, AddressOf _filmChanged
            Main.RegisterProblemDetection(Me)
        End Sub

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems

            Try
                If Not Me.mvarBookingtype.BookIt Then Return New List(Of cProblem)

                Dim _problems As New List(Of cProblem)



                'Number of days in the Week
                Dim DaysInWeek As Integer = DateDiff(DateInterval.Day, Date.FromOADate(Me.StartDate), Date.FromOADate(Me.EndDate))
                If DaysInWeek > 7 Then

                    Dim _helpText As New Text.StringBuilder

                    _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Week has more than 7 days</p>")
                    _helpText.AppendLine("<p>The week named" & Me.Name & DaysInWeek & " days</p>")
                    _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
                    _helpText.AppendLine("<p>You can fix this by going into Setup, clicking the Period button, and adjusting the time span for this week.</p>")

                    Dim _problem As New cProblem(ProblemsEnum.TooManyDays, cProblem.ProblemSeverityEnum.Warning, "A week is longer than 7 days", "Week " & Me.Name, _helpText.ToString, Me)
                    _problems.Add(_problem)

                End If

                If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
                Return _problems
            Catch ex As Exception
                Return New List(Of cProblem)
            End Try

        End Function

        Function NetValue() As Decimal
            Return (From _s As cActualSpot In Main.ActualSpots Where _s.Week Is Me Select _s.ActualNetValue()).Sum
        End Function

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound
    End Class
End Namespace