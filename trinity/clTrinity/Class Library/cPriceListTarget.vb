Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cPricelistTarget
        Implements IDetectsProblems

        Private mvarTargetName As String
        Private WithEvents mvarTarget As cTarget
        Private mvarDatabaseID As String = "NOT SAVED"
        'Private mvarUniSizeNat As Long
        'Private mvarUniSize As Long
        'Private mvarCPPDaypart(0 To 5) As Integer
        'Private mvarCPP As Single
        Private mvarCalcCPP As Boolean
        Private Main As cKampanj
        Private mvarBookingtype As cBookingType
        Private mvarStandardTarget As Boolean
        'Private mvarIndexes As cIndexes
        Private mvarPricelistPeriods As cPricelistPeriods
        Private mvarIsEntered As EnteredEnum
        Private mvarNetCPT As Single
        Private mvarDiscount As Single
        Private mvarNetCPP As Single
        Private mvarEnhancement As Single
        Private mvarMaxRatings As Single
        Private mvarDefaultDaypart(0 To cKampanj.MAX_DAYPARTS) As Byte

        Private mvarIndexes As cIndexes

        Private mvarUserEditable As Boolean = True

        Public Property DefaultDayPart(ByVal i As Integer) As Byte
            Get
                Return mvarDefaultDaypart(i)
            End Get
            Set(ByVal value As Byte)
                mvarDefaultDaypart(i) = value
            End Set
        End Property
        Public Property IsUserEditable As Boolean
            Get
                Return mvarUserEditable
            End Get
            Set(ByVal value As Boolean)
                mvarUserEditable = value
            End Set
        End Property

        Public Enum EnteredEnum
            eDiscount = 0
            eCPT = 1
            eCPP = 2
            eEnhancement = 3
        End Enum

        Public Property databaseID() As String
            Get
                On Error GoTo ID_Error
                databaseID = mvarDatabaseID
                On Error GoTo 0
                Exit Property
ID_Error:
                Err.Raise(Err.Number, "cPriceListTarget: databaseID", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo ID_Error
                mvarDatabaseID = value
                On Error GoTo 0
                Exit Property
ID_Error:
                Err.Raise(Err.Number, "cPriceListTarget: databaseID", Err.Description)
            End Set

        End Property

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            Dim XMLTarget As Xml.XmlElement
            Dim XMLPeriod As Xml.XmlElement
            Dim XMLPricelistPeriods As Xml.XmlElement
            Dim XMLTmpNode As Xml.XmlElement
            Dim XMLTmpNode2 As Xml.XmlElement

            XMLPricelistPeriods = xmlDoc.CreateElement("PricelistPeriods")
            PricelistPeriods.GetXML(XMLPricelistPeriods, errorMessege, xmlDoc)
            colXml.AppendChild(XMLPricelistPeriods)


            colXml = xmlDoc.CreateElement("BuyingTarget")
            colXml.SetAttribute("Name", Me.TargetName)
            colXml.SetAttribute("CalcCPP", Me.CalcCPP) ' as New cPriceListTarget
            colXml.SetAttribute("Discount", Me.Discount) ' as Single
            colXml.SetAttribute("NetCPT", Me.NetCPT) ' as Single
            colXml.SetAttribute("NetCPP", Me.NetCPP) ' as Single
            colXml.SetAttribute("IsEntered", Me.IsEntered) ' as Single

            'XMLTmpNode = xmlDoc.CreateElement("DaypartSplit")
            'For i As Integer = 0 To mvarBookingtype.Dayparts.Count - 1
            '    XMLTmpNode2 = xmlDoc.CreateElement(mvarBookingtype.Dayparts(i).Name)
            '    XMLTmpNode2.SetAttribute("DefaultSplit", Me.DefaultDaypart(i)) ' as New cPriceList
            '    XMLTmpNode.AppendChild(XMLTmpNode2)
            'Next
            'colXml.AppendChild(XMLTmpNode)

            XMLTarget = xmlDoc.CreateElement("Target")
            Me.Target.GetXML(XMLTarget, errorMessege, xmlDoc)
            colXml.AppendChild(XMLTarget)


            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving PriceListTarget for " & mvarBookingtype.ToString)
            Return True
        End Function

        Private Sub TargetChanged() Handles mvarTarget.wasAltered
            'go thorugh all pricelist periods
            For Each plp As cPricelistPeriod In mvarPricelistPeriods
                'invoke the delegate so Actual spots in that period will update its ratings
                plp.TargetWasChanged()
            Next
        End Sub

        Public Sub AddActualspotToWatch(ByVal spot As cActualSpot)
            For Each tmpPeriod As cPricelistPeriod In mvarPricelistPeriods
                If tmpPeriod.FromDate <= Date.FromOADate(spot.AirDate) Then
                    If tmpPeriod.ToDate >= Date.FromOADate(spot.AirDate) Then
                        tmpPeriod.AddActualspotToWatch(spot)
                        Exit For
                    End If
                End If
            Next
        End Sub

        Public Function GetCPPForDate(ByVal WhatDate As Long, Optional ByVal Daypart As Integer = -1) As Decimal
            'gets the CPP for a specific date

            If mvarPricelistPeriods.Count = 0 Then Return 0

            Try

                Dim relevantPeriods() As cPricelistPeriod = (From tmpPeriod As cPricelistPeriod In mvarPricelistPeriods Select tmpPeriod Where _
                                                            tmpPeriod.FromDate <= Date.FromOADate(WhatDate) And _
                                                            tmpPeriod.ToDate >= Date.FromOADate(WhatDate)).ToArray

                If relevantPeriods.Count = 0 Then Return 0

                If Daypart = -1 Then
                    If mvarCalcCPP Then
                        Dim sumDP As Decimal = 0
                        For i As Integer = 0 To mvarBookingtype.Dayparts.Count - 1
                            sumDP += (relevantPeriods(0).Price(True, i) * (Me.Bookingtype.Dayparts(i).Share / 100))
                        Next
                        Return sumDP
                    Else
                        Return relevantPeriods(0).Price(True)
                    End If
                Else
                    Return relevantPeriods(0).Price(True, Daypart)
                End If


                'For Each tmpPeriod As cPricelistPeriod In mvarPricelistPeriods
                '    rptPricelistPeriod = tmpPeriod
                '    If tmpPeriod.FromDate <= Date.FromOADate(WhatDate) Then
                '        If tmpPeriod.ToDate >= Date.FromOADate(WhatDate) Then
                '            If Daypart = -1 Then
                '                If mvarCalcCPP Then
                '                    Dim sumDp As Decimal = 0
                '                    For i As Integer = 0 To mvarBookingtype.Dayparts.Count - 1
                '                        sumDp += (tmpPeriod.Price(True, i) * (Me.Bookingtype.Dayparts(i).Share / 100))
                '                    Next
                '                    Return sumDp
                '                Else
                '                    Return tmpPeriod.Price(True)
                '                End If
                '            Else
                '                Return tmpPeriod.Price(True, Daypart)
                '            End If

                '            Exit Function
                '        End If
                '    End If
                'Next
                Return 0
            Catch ex As Exception

                Debug.Print("Could not get a CPP for" & vbNewLine & _
                                              mvarBookingtype.ToString & vbNewLine & _
                                              Date.FromOADate(WhatDate).ToLongDateString & vbNewLine & _
                                              mvarTargetName & vbNewLine & _
                                              "Daypart " & Daypart & vbNewLine & _
                                              "There may be some information missing in the price list.")
                Return 0
            End Try

            'Dim sql As New StringBuilder
            'sql.Append("SELECT Channels.ChannelName, Bookingtypes.BTName, PriceListTargets.TargetName, PriceListItems.Name AS Expr1, PriceListItems.Name, ")
            'sql.Append("PriceListItems.FromDate, PriceListItems.ToDate, PriceListItems.IsCPP, PriceListItems.PriceDP0, PriceListItems.PriceDP1, ")
            'sql.Append("PriceListItems.PriceDP2, PriceListItems.PriceDP3 ")
            'sql.Append("FROM Channels INNER JOIN")
            'sql.Append("Bookingtypes ON Channels.ChannelID = Bookingtypes.ChannelId INNER JOIN ")
            'sql.Append("PriceListTargets ON Bookingtypes.BTID = PriceListTargets.BTID INNER JOIN ")
            'sql.Append("PriceListItems ON PriceListTargets.TargetID = PriceListItems.TargetID ")
            'sql.Append("WHERE (Channels.ChannelName = '" & Me.Bookingtype.ParentChannel.ChannelName & "') AND (Bookingtypes.BTName = '" & Me.Bookingtype.Name & "') AND ")
            'sql.Append("(PriceListItems.FromDate >= '" & Format(WhatDate, "yyyy-MM-dd") & "') AND (PriceListItems.ToDate <= '" & Format(WhatDate, "yyyy-MM-dd") & "') AND (PriceListTargets.TargetName = '" & Me.TargetName & "')")


        End Function

        ''' <summary>
        ''' Determines whether a specific date has a CPP.
        ''' </summary>
        ''' <param name="WhatDate">The what date.</param>
        ''' <param name="Daypart">The daypart.</param>
        ''' <returns>
        ''' <c>true</c> if this date has a price; otherwise, <c>false</c>.
        ''' </returns>
        Public Function hasCPPForDate(ByVal WhatDate As Long, Optional ByVal Daypart As Integer = -1) As Boolean
            For Each tmpPeriod As cPricelistPeriod In mvarPricelistPeriods
                If tmpPeriod.FromDate.AddHours(-tmpPeriod.FromDate.Hour) <= Date.FromOADate(WhatDate) Then
                    If tmpPeriod.ToDate.AddHours(-tmpPeriod.ToDate.Hour) >= Date.FromOADate(WhatDate) Then
                        If Daypart = -1 Then
                            If mvarCalcCPP Then
                                Dim sumDp As Integer = 0
                                For i As Integer = 0 To mvarBookingtype.Dayparts.Count - 1
                                    sumDp += tmpPeriod.Price(True, i)
                                Next
                                If sumDp > 0 Then
                                    Return True
                                End If
                            Else
                                If tmpPeriod.Price(True) > 0 Then
                                    Return True
                                End If
                            End If
                        Else
                            If tmpPeriod.Price(True, Daypart) > 0 Then
                                Return True
                            End If
                        End If
                        Exit Function
                    End If
                End If
            Next
            Return False
        End Function

        Public ReadOnly Property getUniSizeNat(ByVal d As Long) As Single
            Get
                If d = 0 Then
                    If mvarPricelistPeriods.Count = 0 Then Return 0
                    If mvarBookingtype Is Nothing OrElse mvarBookingtype.Weeks.Count = 0 Then
                        Return 0
                    End If
                    Return getUniSizeNat(mvarBookingtype.Weeks(1).StartDate)
                    Exit Property
                End If
                For Each p As cPricelistPeriod In mvarPricelistPeriods
                    If p.FromDate <= Date.FromOADate(d) AndAlso Date.FromOADate(d) <= p.ToDate Then
                        Return p.TargetNat
                        Exit Property
                    End If
                Next
                Return 0
            End Get
        End Property

        Public ReadOnly Property getUniSizeUni(ByVal d As Long) As Single
            Get
                If d = 0 Then
                    If mvarPricelistPeriods.Count = 0 Then Return 0
                    If mvarBookingtype Is Nothing OrElse mvarBookingtype.Weeks.Count = 0 Then
                        Return 0
                    End If
                    If mvarBookingtype.Weeks(1).StartDate = 0 Then
                        Return 0
                    End If
                    Return getUniSizeUni(mvarBookingtype.Weeks(1).StartDate)
                    Exit Property
                End If
                For Each p As cPricelistPeriod In mvarPricelistPeriods
                    If p.FromDate <= Date.FromOADate(d) AndAlso Date.FromOADate(d) <= p.ToDate Then
                        Return p.TargetUni
                        Exit Property
                    End If
                Next
                Return 0
            End Get
        End Property

        Public Property MaxRatings() As Single
            Get
                Return mvarMaxRatings
            End Get
            Set(ByVal value As Single)
                mvarMaxRatings = value
            End Set
        End Property

        Public Property PricelistPeriods() As cPricelistPeriods
            Get
                PricelistPeriods = mvarPricelistPeriods
            End Get
            Set(ByVal value As cPricelistPeriods)
                mvarPricelistPeriods = value
            End Set
        End Property

        Friend Property Bookingtype() As cBookingType
            Get
                If mvarBookingtype Is Nothing Then
                    Return Nothing
                Else
                    Return mvarBookingtype
                End If

            End Get
            Set(ByVal value As cBookingType)
                'If value Is Nothing Then Stop
                If mvarBookingtype Is Nothing Then
                    'this should only be executed when the Pricelist target is created
                    mvarIndexes = New cIndexes(Main, Me)
                    'Debug.Print("Target " & Me.TargetName & " had its booking type set to nothing")
                End If
                mvarBookingtype = value
            End Set
        End Property

        Friend WriteOnly Property MainObject() As cKampanj
            Set(ByVal value As cKampanj)
                Main = value
            End Set
        End Property

        'Function to determine if two targets are the same
        Public Function CheckTarget(ByVal Target As cPricelistTarget) As Boolean
            If Me.getUniSizeUni(0) <> Target.getUniSizeUni(0) OrElse Me.getUniSizeNat(0) <> Target.getUniSizeNat(0) Then
                Return False
            End If
            If mvarTarget.TargetName <> Target.Target.TargetName Then
                Return False
            End If
            If mvarCalcCPP AndAlso Target.CalcCPP Then
                If Not Target.CalcCPP Then Return False
                For i As Integer = 0 To mvarBookingtype.Dayparts.Count - 1
                    For z As Integer = 1 To mvarPricelistPeriods.Count
                        If Not (Target.GetCPPForDate(z, i) = GetCPPForDate(z, i)) Then
                            Return False
                        End If
                    Next

                    'If mvarCPPDaypart(i) <> Target.CPPDaypart(i) Then
                    '    Return False
                    'End If
                Next
            Else
                Return False

                'If mvarCPP <> Target.CPP Then Return False
            End If

            Return True
        End Function

        'Function to determine if two targets are the same
        Public Function CheckTargetWReason(ByVal Target As cPricelistTarget) As String
            CheckTargetWReason = ""
            If Me.getUniSizeUni(0) <> Target.getUniSizeUni(0) OrElse Me.getUniSizeNat(0) <> Target.getUniSizeNat(0) Then
                CheckTargetWReason = "Universe sizes has changed"
            End If
            If mvarTarget.TargetName <> Target.Target.TargetName Then
                If CheckTargetWReason = "" Then
                    CheckTargetWReason = "Target name has changed"
                Else
                    CheckTargetWReason = CheckTargetWReason & ", Target name has changed"
                End If
            End If
            If mvarCalcCPP Then
                If Not Target.CalcCPP Then Return False
                For i As Integer = 0 To mvarBookingtype.Dayparts.Count - 1
                    For z As Integer = 1 To mvarPricelistPeriods.Count
                        If Not (GetCPPForDate(z, i) = Target.GetCPPForDate(z, i)) Then
                            'check the daypart prices
                            If CheckTargetWReason = "" Then
                                CheckTargetWReason = "Prices for " & Target.mvarTarget.TargetName & " " & mvarBookingtype.Dayparts(i).Name & " is not updated"
                            Else
                                CheckTargetWReason = CheckTargetWReason & ", Prices for " & Target.mvarTarget.TargetName & " " & Main.Dayparts(i).Name & " is not updated"
                            End If
                        End If

                    Next
                    'If mvarCPPDaypart(i) <> Target.CPPDaypart(i) Then
                    '    'check the daypart prices
                    '    If CheckTargetWReason = "" Then
                    '        CheckTargetWReason = "Prices for " & Target.mvarTarget.TargetName & " " & Campaign.DaypartName(i) & " is not updated"
                    '    Else
                    '        CheckTargetWReason = CheckTargetWReason & ", Prices for " & Target.mvarTarget.TargetName & " " & Campaign.DaypartName(i) & " is not updated"
                    '    End If
                    'End If
                Next
            Else
                If Target.CalcCPP Then
                    'the target is now calc CPP
                    If CheckTargetWReason = "" Then
                        CheckTargetWReason = Target.mvarTarget.TargetName & " now uses calculated CPP"
                    Else
                        CheckTargetWReason = CheckTargetWReason & ", " & Target.mvarTarget.TargetName & " now uses calculated CPP"
                    End If
                End If
                For z As Integer = 1 To mvarPricelistPeriods.Count
                    If Not (GetCPPForDate(z) = Target.GetCPPForDate(z)) Then
                        'the targets CPP price dont match
                        If CheckTargetWReason = "" Then
                            CheckTargetWReason = "Prices for " & Target.mvarTarget.TargetName & " is not updated"
                        Else
                            CheckTargetWReason = CheckTargetWReason & ", Prices for " & Target.mvarTarget.TargetName & " is not updated"
                        End If
                    End If
                Next
                'If mvarCPP <> Target.CPP Then
                '    'the targets CPP price dont match
                '    If CheckTargetWReason = "" Then
                '        CheckTargetWReason = "Prices for " & Target.mvarTarget.TargetName & " is not updated"
                '    Else
                '        CheckTargetWReason = CheckTargetWReason & ", Prices for " & Target.mvarTarget.TargetName & " is not updated"
                '    End If
                'End If
            End If

        End Function

        Public Property TargetName() As String
            '---------------------------------------------------------------------------------------
            ' Procedure : TargetName
            ' DateTime  : 2003-07-11 12:00
            ' Author    : joho
            ' Purpose   : Returns/sets the name of the target
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo TargetName_Error

                TargetName = mvarTargetName

                On Error GoTo 0
                Exit Property

TargetName_Error:

                Err.Raise(Err.Number, "cPriceListTarget: TargetName", Err.Description)
            End Get
            Set(ByVal value As String)
                On Error GoTo TargetName_Error

                mvarTargetName = value

                On Error GoTo 0
                Exit Property

TargetName_Error:

                Err.Raise(Err.Number, "cPriceListTarget: TargetName", Err.Description)


            End Set
        End Property

        Public Property Target() As cTarget
            '---------------------------------------------------------------------------------------
            ' Procedure : Target
            ' DateTime  : 2003-07-11 12:00
            ' Author    : joho
            ' Purpose   : Pointer to a cTarget object that holds the actual Target
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo Target_Error
                If mvarTarget Is Nothing Then
                    mvarTarget = New cTarget(Main)
                End If
                Target = mvarTarget

                On Error GoTo 0
                Exit Property

Target_Error:

                Err.Raise(Err.Number, "cPriceListTarget: Target", Err.Description)
            End Get
            Set(ByVal value As cTarget)

                If value Is Nothing Then
                    Windows.Forms.MessageBox.Show("Tried to set the target of the buying target to nothing")
                    Exit Property
                End If
                On Error GoTo Target_Error

                mvarTarget = value

                On Error GoTo 0
                Exit Property

Target_Error:

                Err.Raise(Err.Number, "cPriceListTarget: Target", Err.Description)

            End Set
        End Property

        '        Public Property UniSizeNat() As Long
        '            '---------------------------------------------------------------------------------------
        '            ' Procedure : UniSizeNat
        '            ' DateTime  : 2003-07-11 12:00
        '            ' Author    : joho
        '            ' Purpose   : Returns/sets the National Universe Size for the buying target
        '            '             according to the channel pricelist
        '            '---------------------------------------------------------------------------------------
        '            '
        '            Get
        '                On Error GoTo UniSizeNat_Error

        '                UniSizeNat = mvarUniSizeNat

        '                On Error GoTo 0
        '                Exit Property

        'UniSizeNat_Error:

        '                Err.Raise(Err.Number, "cPriceListTarget: UniSizeNat", Err.Description)
        '            End Get
        '            Set(ByVal value As Long)
        '                On Error GoTo UniSizeNat_Error

        '                mvarUniSizeNat = value

        '                On Error GoTo 0
        '                Exit Property

        'UniSizeNat_Error:

        '                Err.Raise(Err.Number, "cPriceListTarget: UniSizeNat", Err.Description)

        '            End Set
        '        End Property

        '        Public Property UniSize() As Long
        '            '---------------------------------------------------------------------------------------
        '            ' Procedure : UniSize
        '            ' DateTime  : 2003-07-11 12:00
        '            ' Author    : joho
        '            ' Purpose   : Returns/sets the universe size in the buying universe according to
        '            '             the channel pricelist
        '            '---------------------------------------------------------------------------------------
        '            '
        '            Get
        '                On Error GoTo UniSize_Error

        '                UniSize = mvarUniSize

        '                On Error GoTo 0
        '                Exit Property

        'UniSize_Error:

        '                Err.Raise(Err.Number, "cPriceListTarget: UniSize", Err.Description)
        '            End Get
        '            Set(ByVal value As Long)

        '                On Error GoTo UniSize_Error

        '                mvarUniSize = value

        '                On Error GoTo 0
        '                Exit Property

        'UniSize_Error:

        '                Err.Raise(Err.Number, "cPriceListTarget: UniSize", Err.Description)

        '            End Set
        '        End Property

        '        Public Property CPPDaypart(ByVal Daypart) As Integer
        '            '---------------------------------------------------------------------------------------
        '            ' Procedure : CPPDaypart
        '            ' DateTime  : 2003-07-11 12:01
        '            ' Author    : joho
        '            ' Purpose   : Returns/sets the CPP during each daypart
        '            '---------------------------------------------------------------------------------------
        '            '
        '            Get
        '                On Error GoTo CPP_Error

        '                CPPDaypart = mvarCPPDaypart(Daypart)

        '                On Error GoTo 0
        '                Exit Property

        'CPP_Error:

        '                Err.Raise(Err.Number, "cPriceListTarget: CPP_DT", Err.Description)
        '            End Get
        '            Set(ByVal value As Integer)
        '                On Error GoTo CPP_Error

        '                mvarCPPDaypart(Daypart) = value

        '                If mvarCalcCPP Then
        '                    CalculateCPP()
        '                End If

        '                On Error GoTo 0
        '                Exit Property

        'CPP_Error:

        '                Err.Raise(Err.Number, "cPriceListTarget: CPP_DT", Err.Description)

        '            End Set
        '        End Property

        '        Public Property CPP() As Single
        '            '---------------------------------------------------------------------------------------
        '            ' Procedure : CPP
        '            ' DateTime  : 2003-07-11 12:01
        '            ' Author    : joho
        '            ' Purpose   : Returns/sets the CPP during the entire day
        '            '---------------------------------------------------------------------------------------
        '            '

        '            Get
        '                On Error GoTo CPP_Error

        '                If mvarCPP = 0 And mvarCalcCPP Then
        '                    CalculateCPP()
        '                End If

        '                Return mvarCPP

        '                On Error GoTo 0
        '                Exit Property

        'CPP_Error:

        '                Err.Raise(Err.Number, "cPriceListTarget: CPP", Err.Description)
        '            End Get
        '            Set(ByVal value As Single)
        '                On Error GoTo CPP_Error

        '                mvarCPP = value

        '                On Error GoTo 0
        '                Exit Property

        'CPP_Error:

        '                Err.Raise(Err.Number, "cPriceListTarget: CPP", Err.Description)

        '            End Set
        '        End Property


        Public Property CalcCPP() As Boolean
            '---------------------------------------------------------------------------------------
            ' Procedure : CalcCPP
            ' DateTime  : 2003-07-11 12:02
            ' Author    : joho
            ' Purpose   : Returns/sets wether the CPP should be calculated from the individual
            '             daypart-CPPs
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo CalcCPP_Error

                CalcCPP = mvarCalcCPP

                On Error GoTo 0
                Exit Property

CalcCPP_Error:

                Err.Raise(Err.Number, "cPriceListTarget: CalcCPP", Err.Description)
            End Get
            Set(ByVal value As Boolean)

                On Error GoTo CalcCPP_Error

                mvarCalcCPP = value

                On Error GoTo 0
                Exit Property

CalcCPP_Error:

                Err.Raise(Err.Number, "cPriceListTarget: CalcCPP", Err.Description)

            End Set
        End Property

        'Friend Sub CalculateCPP()

        '    Dim x As Integer
        '    Dim TmpCPP As Decimal

        '    For x = 0 To Main.DaypartCount - 1
        '        TmpCPP = TmpCPP + (mvarBookingtype.DaypartSplit(x) / 100) * mvarCPPDaypart(x)
        '    Next

        '    mvarCPP = TmpCPP

        'End Sub

        Public Function UniIndex(ByVal d As Long, Optional ByVal CompareWithMain As Boolean = False) As Single
            '---------------------------------------------------------------------------------------
            ' Procedure : UniIndex
            ' DateTime  : 2003-07-09 13:15
            ' Author    : joho
            ' Purpose   : Macro to return the index between the actual universe and the
            '             national
            '             CompareWithMain: Decides whether the return value should be the
            '                              index between the channel universe and the
            '                              national universe (False) or between channel
            '                              universe and the chosen universe for the main
            '                              target (True).
            '---------------------------------------------------------------------------------------
            '

            On Error GoTo UniIndex_Error
            If CompareWithMain Then
                If Main.MainTarget.Universe = mvarBookingtype.ParentChannel.BuyingUniverse OrElse (Main.MainTarget.TargetName = mvarBookingtype.BuyingTarget.Target.TargetName AndAlso Main.MainTarget.UniSize = mvarBookingtype.BuyingTarget.Target.UniSize) Then
                    UniIndex = 1
                Else
                    If getUniSizeNat(d) > 0 Then
                        UniIndex = getUniSizeUni(d) / getUniSizeNat(d)
                    Else
                        UniIndex = 1
                    End If
                End If
            Else
                If getUniSizeNat(d) > 0 Then
                    UniIndex = getUniSizeUni(d) / getUniSizeNat(d)
                Else
                    UniIndex = 1
                End If
            End If
            If UniIndex = 0 Then
                UniIndex = 1
            End If
            On Error GoTo 0
            Exit Function

UniIndex_Error:

            Err.Raise(Err.Number, "cTarget: UniIndex", Err.Description)


        End Function

        Public Property StandardTarget() As Boolean
            '---------------------------------------------------------------------------------------
            ' Procedure : StandardTarget
            ' DateTime  : 2003-10-15 13:58
            ' Author    : joho
            ' Purpose   : If set to true it indicates that the target is in the
            '             standard pricelist of the channel.
            '---------------------------------------------------------------------------------------
            '
            Get
                On Error GoTo StandardTarget_Error

                StandardTarget = mvarStandardTarget

                On Error GoTo 0
                Exit Property

StandardTarget_Error:

                Err.Raise(Err.Number, "cPriceListTarget: StandardTarget", Err.Description)
            End Get
            Set(ByVal value As Boolean)
                On Error GoTo StandardTarget_Error

                mvarStandardTarget = value

                On Error GoTo 0
                Exit Property

StandardTarget_Error:

                Err.Raise(Err.Number, "cPriceListTarget: StandardTarget", Err.Description)

            End Set
        End Property

        'Public Property Indexes() As cIndexes
        '    Get
        '        Indexes = mvarIndexes
        '    End Get
        '    Set(ByVal value As cIndexes)
        '        mvarIndexes = value
        '    End Set
        'End Property

        Public Property Enhancement() As Single
            Get
                If mvarIsEntered = EnteredEnum.eEnhancement Then
                    Return mvarEnhancement
                Else
                    Return (1 / (1 - Discount)) * 100 - 100
                End If
            End Get
            Set(ByVal value As Single)
                mvarEnhancement = value
                If CalcCPP Then
                    Dim sumCPP As Integer = 0
                    If mvarBookingtype.BuyingTarget.CalcCPP Then
                        For z As Integer = 0 To mvarBookingtype.Dayparts.Count - 1
                            sumCPP += mvarBookingtype.BuyingTarget.GetCPPForDate(Main.StartDate, z) * mvarBookingtype.Dayparts(z).Share / 100
                        Next
                    Else
                        sumCPP = mvarBookingtype.BuyingTarget.GetCPPForDate(Main.StartDate, -1)
                    End If
                    mvarNetCPP = sumCPP * (1 / (1 + value / 100))
                Else
                    mvarNetCPP = mvarBookingtype.BuyingTarget.GetCPPForDate(Main.StartDate) * (1 / (1 + value / 100))
                End If
            End Set
        End Property

        Public Property Discount(Optional ExcludeIndexes As Boolean = False) As Single
            Get
                If mvarIsEntered = EnteredEnum.eDiscount Then
                    Discount = mvarDiscount
                Else
                    Dim sumCPP As Integer = 0
                    If mvarBookingtype.BuyingTarget.CalcCPP Then
                        For z As Integer = 0 To mvarBookingtype.Dayparts.Count - 1
                            sumCPP += mvarBookingtype.BuyingTarget.GetCPPForDate(Main.StartDate, z) * mvarBookingtype.Dayparts(z).Share / 100
                        Next
                    Else
                        sumCPP = mvarBookingtype.BuyingTarget.GetCPPForDate(Main.StartDate, -1)
                    End If
                    If sumCPP = 0 Then
                        Discount = 1
                    Else
                        Discount = 1 - (mvarNetCPP / sumCPP)
                    End If
                    'If mvarBookingtype.BuyingTarget.PricelistPeriods.GetCPPForDate(Campaign.StartDate) <> 0 Then
                    '    Discount = 1 - (mvarNetCPP / mvarBookingtype.BuyingTarget.PricelistPeriods.GetCPPForDate(Campaign.StartDate))
                    'Else
                    '    Discount = 1
                    'End If
                End If
            End Get
            Set(ByVal value As Single)
                mvarDiscount = value
                If CalcCPP Then
                    Dim sumCPP As Integer = 0
                    If Bookingtype.BuyingTarget.CalcCPP Then
                        For z As Integer = 0 To mvarBookingtype.Dayparts.Count - 1
                            sumCPP += Bookingtype.BuyingTarget.GetCPPForDate(Main.StartDate, z) * Bookingtype.Dayparts(z).Share / 100
                        Next
                    Else
                        sumCPP = Bookingtype.BuyingTarget.GetCPPForDate(Main.StartDate, -1)
                    End If
                    mvarNetCPP = sumCPP * (1 - value)
                Else
                    mvarNetCPP = Bookingtype.BuyingTarget.GetCPPForDate(Main.StartDate) * (1 - value)
                End If
                If Bookingtype.BuyingTarget.getUniSizeUni(0) <> 0 Then
                    mvarNetCPT = (mvarNetCPP / Bookingtype.BuyingTarget.getUniSizeUni(0)) * 100
                Else
                    mvarNetCPT = 0
                End If
                Bookingtype.InvalidateTotalTRP()
            End Set
        End Property

        'Public Function GetNetCPP30ForDate(ByVal d As Long, ByVal daypart As Integer) As Single
        '    Select Case IsEntered
        '        Case EnteredEnum.eCPT
        '            Return (mvarNetCPT * getUniSizeUni(d)) / 100
        '        Case EnteredEnum.eDiscount
        '            Return GetCPPForDate(d, daypart) * (1 - mvarDiscount)
        '        Case EnteredEnum.eCPP
        '            Return mvarNetCPP
        '    End Select
        'End Function

        Public Property NetCPT() As Single
            Get
                NetCPT = mvarNetCPT
            End Get
            Set(ByVal value As Single)
                mvarNetCPT = value
                mvarNetCPP = (value * getUniSizeUni(0)) / 100

                If CalcCPP Then
                    Dim sumCPP As Integer = 0
                    For z As Integer = 0 To mvarBookingtype.Dayparts.Count - 1
                        sumCPP += mvarBookingtype.BuyingTarget.GetCPPForDate(Main.StartDate, z) * mvarBookingtype.Dayparts(z).Share / 100
                    Next
                    If sumCPP = 0 Then
                        mvarDiscount = 1
                    Else
                        mvarDiscount = 1 - (mvarNetCPP / sumCPP)
                    End If

                Else
                    If mvarBookingtype.BuyingTarget.GetCPPForDate(Main.StartDate) = 0 Then
                        mvarDiscount = 1
                    Else
                        mvarDiscount = 1 - (mvarNetCPP / mvarBookingtype.BuyingTarget.GetCPPForDate(Main.StartDate))
                    End If
                End If
                mvarBookingtype.InvalidateTotalTRP()
            End Set
        End Property

        '*** BEFORE GROSSINDEX REMOVAL
        'Public Property NetCPP() As Single
        '    Get
        '        NetCPP = mvarNetCPP
        '    End Get
        '    Set(ByVal value As Single)
        '        Dim TmpIndex As Single

        '        If mvarBookingtype.Weeks.Count > 0 Then
        '            TmpIndex = mvarBookingtype.Weeks(1).GrossIndex
        '        Else
        '            TmpIndex = 1
        '        End If
        '        mvarNetCPP = value
        '        If mvarBookingtype.GrossCPP <> 0 Then
        '            mvarDiscount = 1 - (value / mvarBookingtype.GrossCPP) * TmpIndex
        '        Else
        '            mvarDiscount = 1
        '        End If
        '        If mvarBookingtype.BuyingTarget.UniSize <> 0 Then
        '            mvarNetCPT = (mvarNetCPP / mvarBookingtype.BuyingTarget.UniSize) * 100
        '        Else
        '            mvarNetCPT = 0
        '        End If
        '    End Set
        'End Property

        Public Property Indexes() As cIndexes
            Get
                Indexes = mvarIndexes
            End Get
            Set(ByVal value As cIndexes)
                mvarIndexes = value
            End Set
        End Property

        Public Property NetCPP() As Single
            Get
                Return mvarNetCPP
            End Get
            Set(ByVal value As Single)
                If mvarBookingtype.Weeks.Count > 0 Then
                    mvarNetCPP = value
                    If mvarBookingtype.Weeks(1).GrossCPP <> 0 Then
                        mvarDiscount = 1 - (value / mvarBookingtype.Weeks(1).GrossCPP)
                    Else
                        mvarDiscount = 1
                    End If
                    If mvarBookingtype.BuyingTarget.getUniSizeUni(0) <> 0 Then
                        mvarNetCPT = (mvarNetCPP / mvarBookingtype.BuyingTarget.getUniSizeUni(0)) * 100
                    Else
                        mvarNetCPT = 0
                    End If

                Else
                    mvarDiscount = 1
                    mvarNetCPT = 0
                    mvarNetCPP = value
                End If

                mvarBookingtype.InvalidateTotalTRP()
            End Set
        End Property

        Public Property IsEntered() As EnteredEnum
            Get
                IsEntered = mvarIsEntered
            End Get
            Set(ByVal value As EnteredEnum)
                mvarIsEntered = value
            End Set
        End Property

        Public Sub New(ByVal Main As cKampanj, ByVal BT As cBookingType)
            mvarStandardTarget = False
            mvarIsEntered = 0
            mvarNetCPT = 0
            mvarTarget = New cTarget(Main)
            MainObject = Main
            'mvarIndexes = New cIndexes(Main, Me)
            mvarPricelistPeriods = New cPricelistPeriods(Me)
            If BT IsNot Nothing Then
                Bookingtype = BT
            Else
                Throw New Exception("Tried to set the booking type parent of target " & Me.TargetName & " to Nothing")
            End If
            Main.RegisterProblemDetection(Me)
        End Sub

        Protected Overrides Sub Finalize()

            mvarTarget = Nothing

            'mvarIndexes = Nothing
            MyBase.Finalize()
        End Sub

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems

            Dim _problems As New List(Of cProblem)

            'If mvarUniSize <> Me.GetUniSizes Then

            '    Dim _helpText As New Text.StringBuilder

            '    _helpText.AppendLine("<p style='font-weight: bold; font-size: 14px'>Universe size is 0</p>")
            '    _helpText.AppendLine("<p>The target " & Me.TargetName & " in the booking type " & "</p>")
            '    _helpText.AppendLine("<p style='font-weight: bold'>Solution:</p>")
            '    _helpText.AppendLine("<p>You can fix this by going into Edit Pricelist and clicking the button Get Universe from AdvantEdge on pricerows with this target.</p>")

            '    Dim _problem As New cProblem(ProblemsEnum.UniverseIsZero, cProblem.ProblemSeverityEnum.Error, "Universe is 0 or less", Me.TargetName, _helpText.ToString, Me)
            '    _problems.Add(_problem)

            'End If

            If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
            Return _problems

        End Function

        Public Overrides Function ToString() As String
            Return mvarTargetName
        End Function

        Public ReadOnly Property Longname() As String
            Get
                Return Me.Bookingtype.ToString & " - " & Me.TargetName
            End Get
        End Property

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound
    End Class
End Namespace