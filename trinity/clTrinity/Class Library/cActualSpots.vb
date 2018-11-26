Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cActualSpots
        Implements IDetectsProblems

        'a collection of aired spots in the campaign
        Implements System.Collections.IEnumerable

        'local variable to hold collection
        ' Uses an object as its key
        Private mCol As Dictionary(Of String, cActualSpot)
        Private mvarLastSpot As cActualSpot
        Private mvarMainObject As cKampanj

        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            Dim XMLSpot As Xml.XmlElement

            For Each TmpSpot As Trinity.cActualSpot In Me
                XMLSpot = xmlDoc.CreateElement("Spot")
                TmpSpot.GetXML(XMLSpot, errorMessege, xmlDoc)
                colXml.AppendChild(XMLSpot)
            Next

            colXml.SetAttribute("TotalTRP", Me.TotalTRP)

            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving the Actual spots collection")
            Return False
        End Function

        Public Sub InvalidateTargets()
            For Each _spot As cActualSpot In mCol.Values
                _spot.InvalidateTargets()
            Next
        End Sub

        Public Function getDataTable(ByVal TargetName As String) As DataTable
            'This function returns the collection as a datatable

            getDataTable = New DataTable

            'add all columns
            getDataTable.Columns.Add("ID")
            getDataTable.Columns.Add("MatchedSpotID")
            getDataTable.Columns.Add("Program")
            getDataTable.Columns.Add("Prog Before")
            getDataTable.Columns.Add("Prog After")
            getDataTable.Columns.Add("Date")
            getDataTable.Columns.Add("Time")
            getDataTable.Columns.Add("Actual0", GetType(Double))
            getDataTable.Columns.Add("Actual1", GetType(Double))
            getDataTable.Columns.Add("Actual2", GetType(Double))
            getDataTable.Columns.Add("Actual3", GetType(Double))
            getDataTable.Columns.Add("Actual4", GetType(Double))
            getDataTable.Columns.Add("My Est0", GetType(Double))
            getDataTable.Columns.Add("Chan Est0", GetType(Double))
            getDataTable.Columns.Add("My Est1", GetType(Double))
            getDataTable.Columns.Add("Chan Est1", GetType(Double))
            getDataTable.Columns.Add("My Est2", GetType(Double))
            getDataTable.Columns.Add("Chan Est2", GetType(Double))
            getDataTable.Columns.Add("My Est3", GetType(Double))
            getDataTable.Columns.Add("Chan Est3", GetType(Double))
            getDataTable.Columns.Add("My Est4", GetType(Double))
            getDataTable.Columns.Add("Chan Est4", GetType(Double))
            getDataTable.Columns.Add("Channel")
            getDataTable.Columns.Add("Bookingtype")
            getDataTable.Columns.Add("Filmcode")
            getDataTable.Columns.Add("Week")
            getDataTable.Columns.Add("Weekday")
            getDataTable.Columns.Add("Gross Price", GetType(Double))
            getDataTable.Columns.Add("Net Price", GetType(Double))
            getDataTable.Columns.Add("Film")
            getDataTable.Columns.Add("Film dscr")
            getDataTable.Columns.Add("Gross CPP", GetType(Double))
            getDataTable.Columns.Add("Quality")
            getDataTable.Columns.Add("Remarks")
            getDataTable.Columns.Add("Notes")
            getDataTable.Columns.Add("Added Value")
            getDataTable.Columns.Add("PIB")
            getDataTable.Columns.Add("Net value", GetType(Double))
            getDataTable.Columns.Add("SC")
            getDataTable.Columns.Add("CPP(Chn Est)", GetType(Double))
            getDataTable.Columns.Add("CPP (" & TargetName & ")", GetType(Double))
            getDataTable.Columns.Add("Color0")
            getDataTable.Columns.Add("Color1")
            getDataTable.Columns.Add("Color2")
            getDataTable.Columns.Add("Color3")
            getDataTable.Columns.Add("Color4")

            'Add all spots
            Dim row As DataRow
            Dim Days() As String = {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"}
            For Each spot As Trinity.cActualSpot In mCol.Values
                row = getDataTable.Rows.Add()
                row.Item("ID") = spot.ID
                row.Item("Date") = Format(Date.FromOADate(spot.AirDate), "Short Date")
                row.Item("Time") = Trinity.Helper.Mam2Tid(spot.MaM)
                row.Item("Channel") = spot.Channel.Shortname
                row.Item("Bookingtype") = spot.Bookingtype.Name
                row.Item("Filmcode") = spot.Filmcode
                row.Item("Week") = DatePart(DateInterval.WeekOfYear, Date.FromOADate(spot.AirDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                row.Item("Weekday") = Days(Weekday(Date.FromOADate(spot.AirDate), vbMonday) - 1)
                If Not spot.Bookingtype.Weeks(1).Films(spot.Filmcode) Is Nothing Then
                    row.Item("Film") = spot.Bookingtype.Weeks(1).Films(spot.Filmcode).Name
                    row.Item("Film dscr") = spot.Bookingtype.Weeks(1).Films(spot.Filmcode).Description
                End If
                row.Item("Remarks") = spot.Remark
                row.Item("PIB") = spot.PosInBreak & " / " & spot.SpotsInBreak
                row.Item("Net value") = Format(spot.ActualNetValue, "##,##0")

                If spot.Rating > 0 Then
                    If Not spot.MatchedSpot Is Nothing Then
                        row.Item("CPP (" & TargetName & ")") = Format(spot.MatchedSpot.PriceNet / spot.Rating, "##,##0")
                    Else
                        row.Item("CPP (" & TargetName & ")") = "0"
                    End If
                Else
                    row.Item("CPP (" & TargetName & ")") = "0"
                End If

                If spot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                    If Not spot.MatchedSpot Is Nothing Then
                        row.Item("Gross CPP") = Format(spot.MatchedSpot.PriceNet / spot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "##,##0")
                    Else
                        row.Item("Gross CPP") = "0"
                    End If
                Else
                    row.Item("Gross CPP") = "0"
                End If

                If spot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                    If Not spot.MatchedSpot Is Nothing Then
                        row.Item("CPP(Chn Est)") = Format(spot.MatchedSpot.PriceNet / spot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "##,##0")
                    Else
                        row.Item("CPP(Chn Est)") = "0"
                    End If
                Else
                    row.Item("CPP(Chn Est)") = "0"
                End If

                If spot.SpotControlRemark = "" Then
                    row.Item("Program") = spot.Programme
                    row.Item("SC") = ""
                Else
                    row.Item("Program") = spot.Programme & "!"
                    row.Item("SC") = "!"
                End If
                row.Item("Prog Before") = spot.ProgBefore
                row.Item("Prog After") = spot.ProgAfter

                If spot.MatchedSpot Is Nothing Then
                    row.Item("Gross Price") = "0"
                    row.Item("Net Price") = "0"
                    row.Item("My Est0") = "0"
                    row.Item("Chan Est0") = "0"
                    row.Item("My Est1") = "0"
                    row.Item("Chan Est1") = "0"
                    row.Item("My Est2") = "0"
                    row.Item("Chan Est2") = "0"
                    row.Item("My Est3") = "0"
                    row.Item("Chan Est3") = "0"
                    row.Item("My Est4") = "0"
                    row.Item("Chan Est4") = "0"
                    row.Item("Color0") = ""
                    row.Item("Color1") = ""
                    row.Item("Color2") = ""
                    row.Item("Color3") = ""
                    row.Item("Color4") = ""
                Else
                    row.Item("MatchedSpotID") = spot.MatchedSpot.ID
                    row.Item("Gross Price") = Format(spot.MatchedSpot.PriceGross, "##,##0")
                    row.Item("Net Price") = Format(spot.MatchedSpot.PriceNet, "##,##0")
                    row.Item("My Est0") = Format(spot.MatchedSpot.MyRating, "0.0")
                    row.Item("Chan Est0") = Format(spot.MatchedSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget), "0.0")
                    row.Item("My Est1") = Format(spot.MatchedSpot.MyRating * (spot.Bookingtype.IndexSecondTarget / spot.Bookingtype.IndexMainTarget), "0.0")
                    row.Item("Chan Est1") = Format(spot.MatchedSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget), "0.0")
                    row.Item("My Est2") = Format(spot.MatchedSpot.MyRating, "0.0")
                    row.Item("Chan Est2") = Format(spot.MatchedSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteThirdTarget), "0.0")
                    row.Item("My Est3") = Format(spot.MatchedSpot.MyRating, "0.0")
                    row.Item("Chan Est3") = Format(spot.MatchedSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "0.0")
                    row.Item("My Est4") = Format(spot.MatchedSpot.MyRating, "0.0")
                    row.Item("Chan Est4") = Format(spot.MatchedSpot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteAllAdults), "0.0")

                    If CSng(Format(spot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget), "0.0")) < CSng(Format(spot.MatchedSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget), "0.0")) Then
                        row.Item("Color0") = "RED"
                    ElseIf CSng(Format(spot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget), "0.0")) > CSng(Format(spot.MatchedSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget), "0.0")) Then
                        row.Item("Color0") = "GREEN"
                    Else
                        row.Item("Color0") = "BLUE"
                    End If
                    If CSng(Format(spot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget), "0.0")) < CSng(Format(spot.MatchedSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget), "0.0")) Then
                        row.Item("Color1") = "RED"
                    ElseIf CSng(Format(spot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget), "0.0")) > CSng(Format(spot.MatchedSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget), "0.0")) Then
                        row.Item("Color1") = "GREEN"
                    Else
                        row.Item("Color1") = "BLUE"
                    End If
                    If CSng(Format(spot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteThirdTarget), "0.0")) < CSng(Format(spot.MatchedSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateThirdTarget), "0.0")) Then
                        row.Item("Color2") = "RED"
                    ElseIf CSng(Format(spot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteThirdTarget), "0.0")) > CSng(Format(spot.MatchedSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateThirdTarget), "0.0")) Then
                        row.Item("Color2") = "GREEN"
                    Else
                        row.Item("Color2") = "BLUE"
                    End If
                    If CSng(Format(spot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "0.0")) < CSng(Format(spot.MatchedSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget), "0.0")) Then
                        row.Item("Color3") = "RED"
                    ElseIf CSng(Format(spot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "0.0")) > CSng(Format(spot.MatchedSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget), "0.0")) Then
                        row.Item("Color3") = "GREEN"
                    Else
                        row.Item("Color3") = "BLUE"
                    End If
                    If CSng(Format(spot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteAllAdults), "0.0")) < CSng(Format(spot.MatchedSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateAllAdults), "0.0")) Then
                        row.Item("Color4") = "RED"
                    ElseIf CSng(Format(spot.Rating(Trinity.cPlannedSpot.PlannedTargetEnum.pteAllAdults), "0.0")) > CSng(Format(spot.MatchedSpot.MyRating(Trinity.cActualSpot.ActualTargetEnum.ateAllAdults), "0.0")) Then
                        row.Item("Color4") = "GREEN"
                    Else
                        row.Item("Color4") = "BLUE"
                    End If
                End If

                row.Item("Actual0") = Format(spot.Rating, "0.0")
                row.Item("Actual1") = Format(spot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget), "0.0")
                row.Item("Actual2") = Format(spot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateThirdTarget), "0.0")
                row.Item("Actual3") = Format(spot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget), "0.0")
                row.Item("Actual4") = Format(spot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateAllAdults), "0.0")
            Next

        End Function

        Public Function Add(ByVal AirDate As Date, ByVal MaM As Integer, Optional ByVal Filmcode As String = "", Optional ByVal Channel As cChannel = Nothing, Optional ByVal Programme As String = "", Optional ByVal SpotLength As Byte = 30, Optional ByVal Advertiser As String = "", Optional ByVal Product As String = "", Optional ByVal Index As Integer = 100, Optional ByVal PosInBreak As Byte = 0, Optional ByVal SpotsInBreak As Byte = 0, Optional ByVal BreakType As Byte = 0, Optional ByVal AdedgeChannel As String = "", Optional ByVal SpotType As Byte = 0, Optional ByVal Sort As Boolean = False) As cActualSpot
            'create a new object

            Dim objNewMember As New cActualSpot(mvarMainObject)
            Dim i As Integer
            Dim BeforeSpot As cActualSpot

            If Not Channel Is Nothing Then
                objNewMember.Channel = Channel
            End If

            If AdedgeChannel <> "" Then
                objNewMember.AdedgeChannel = AdedgeChannel
            End If

            objNewMember.AirDate = AirDate.ToOADate
            objNewMember.MaM = MaM
            objNewMember.Programme = Programme
            objNewMember.Advertiser = Advertiser
            objNewMember.Product = Product
            objNewMember.Filmcode = Filmcode
            'objNewMember.Rating = Rating
            objNewMember.Index = Index
            objNewMember.PosInBreak = PosInBreak
            objNewMember.SpotsInBreak = SpotsInBreak
            objNewMember.SpotLength = SpotLength
            objNewMember.Deactivated = False
            objNewMember.SpotType = SpotType
            objNewMember.BreakType = BreakType
            objNewMember.ID = CreateGUID()
            objNewMember.MainObject = mvarMainObject

            'set the properties passed into the method

            BeforeSpot = Nothing
            If Sort Then
                For i = 1 To mCol.Count
                    If objNewMember.AirDate < mCol(i).AirDate Then
                        BeforeSpot = mCol(i)
                    ElseIf objNewMember.AirDate = mCol(i).AirDate Then
                        If objNewMember.MaM < mCol(i).MaM Then
                            BeforeSpot = mCol(i)
                            Exit For
                        End If
                    End If
                Next
            End If
            If Not BeforeSpot Is Nothing Then
                mCol.Add(objNewMember.ID, objNewMember)
                'mCol.Add(objNewMember, objNewMember.ID, BeforeSpot.ID)
            Else
                'mCol.Add(objNewMember, objNewMember.ID)
            End If

            ' Add the object to the dictionary
            mCol.Add(objNewMember.ID, objNewMember)

            'return the object created
            Add = objNewMember

            mvarMainObject.AddActualspotToTargetChanged(objNewMember)
            objNewMember = Nothing


        End Function

        Default Public ReadOnly Property Item(ByVal vntIndexKey As String) As cActualSpot
            'used when referencing an element in the collection
            'vntIndexKey contains either the Index or Key to the collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Get
                Item = mCol(vntIndexKey)
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Integer) As cActualSpot
            'used when referencing an element in the collection
            'vntIndexKey contains either the Index or Key to the collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Get
                ' Substracts one as the old collection was one-based and the dictionary collection is zero-based
                Item = mCol.Values.ToList(vntIndexKey - 1)
            End Get
        End Property

        Public ReadOnly Property Count() As Long
            'used when retrieving the number of elements in the
            'collection. Syntax: Debug.Print x.Count
            Get
                Count = mCol.Count
            End Get
        End Property

        Public Sub Remove(ByVal vntIndexKey As Integer)
            'used when removing an element from the collection
            'vntIndexKey contains either the Index or Key, which is why
            'it is declared as a Variant
            'Syntax: x.Remove(xyz)

            Dim tmpKeyList As String() = mCol.Keys.ToArray()
            Dim tmpKey As String = tmpKeyList(vntIndexKey - 1)

            DirectCast(mCol(tmpKey), cActualSpot).Bookingtype.ActualSpots.Remove(DirectCast(mCol(tmpKey), cActualSpot).ID)
            mvarMainObject.UnRegisterProblemDetection(mCol(tmpKey))
            Dim tmp As String = tmpKey
            mCol.Remove(tmpKey)

        End Sub

        Public Sub Remove(ByVal vntIndexKey As String)
            'used when removing an element from the collection
            'vntIndexKey contains either the Index or Key, which is why
            'it is declared as a Variant
            'Syntax: x.Remove(xyz)

            DirectCast(mCol(vntIndexKey), cActualSpot).Bookingtype.ActualSpots.Remove(DirectCast(mCol(vntIndexKey), cActualSpot).ID)
            mvarMainObject.UnRegisterProblemDetection(mCol(vntIndexKey))
            Dim tmp As String = vntIndexKey
            mCol.Remove(vntIndexKey)

        End Sub

        Public ReadOnly Property LastSpot() As cActualSpot
            Get
                LastSpot = mvarLastSpot
            End Get
        End Property

        Friend WriteOnly Property MainObject() As cKampanj
            Set(ByVal value As cKampanj)
                mvarMainObject = value
            End Set
        End Property

        Friend ReadOnly Property Collection() As Dictionary(Of String, cActualSpot)
            Get
                Collection = mCol
            End Get
        End Property

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            GetEnumerator = mCol.Values.GetEnumerator()
        End Function

        Public Sub New(ByVal Main As cKampanj)
            mCol = New Dictionary(Of String, cActualSpot)()
            mvarMainObject = Main
            Main.RegisterProblemDetection(Me)
            If Not Main.Channels Is Nothing Then
                For Each TmpChan As Trinity.cChannel In Main.Channels
                    For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                        TmpBT.ActualSpots.Clear()
                    Next
                Next
            End If
        End Sub

        Protected Overrides Sub Finalize()
            mCol = Nothing
            MyBase.Finalize()
        End Sub

        Public Function TotalTRP(Optional ByVal ratingtype As cKampanj.TRPTypeEnum = cKampanj.TRPTypeEnum.TRPMain) As Single

            'Dim TmpTRP As Single = 0

            'For Each TmpSpot As cActualSpot In mCol
            '    TmpTRP += TmpSpot.Rating
            'Next

            'Return TmpTRP


            Return (From spot As cActualSpot In mCol.Values Select spot.Rating).Sum

        End Function

        Public Function DetectProblems() As System.Collections.Generic.List(Of cProblem) Implements IDetectsProblems.DetectProblems

            Dim _problems As New List(Of cProblem)

            If _problems.Count > 0 Then RaiseEvent ProblemsFound(_problems)
            Return _problems

        End Function

        Public Event ProblemsFound(ByVal problems As System.Collections.Generic.List(Of cProblem)) Implements IDetectsProblems.ProblemsFound

    End Class
End Namespace