Imports System.Runtime.InteropServices
Imports System
Imports System.Text
Imports System.Collections
Imports Microsoft.VisualBasic

Namespace Trinity
    Public Class cPlannedSpots
        Implements Collections.IEnumerable

        'local variable to hold collection
        Private mCol As Collection
        Private Main As cKampanj


        Public Function GetXML(ByRef colXml As Xml.XmlElement, ByRef errorMessege As List(Of String), ByVal xmlDoc As Xml.XmlDocument) As Boolean
            'this function saves the collection to the xml provided

            On Error GoTo On_Error

            Dim XMLSpot As Xml.XmlElement

            For Each TmpSpot As Trinity.cPlannedSpot In Me
                XMLSpot = xmlDoc.CreateElement("Spot")
                TmpSpot.GetXML(XMLSpot, errorMessege, xmlDoc)
                colXml.AppendChild(XMLSpot)
            Next

            colXml.SetAttribute("TotalTRP", Me.TotalTRP)
            colXml.SetAttribute("TRPToDeliver", Me.TotalTRPToDeliver)

            Return True
            Exit Function

On_Error:
            errorMessege.Add("Error saving the Planned spots collection")
            Return False
        End Function


        Public Function getDataTable(ByVal TargetName As String) As DataTable
            'This function returns the collection as a datatable

            getDataTable = New DataTable

            'add all columns
            getDataTable.Columns.Add("ID")
            getDataTable.Columns.Add("Program")
            getDataTable.Columns.Add("Prog Before")
            getDataTable.Columns.Add("Prog After")
            getDataTable.Columns.Add("Date")
            getDataTable.Columns.Add("Time")
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
            getDataTable.Columns.Add("Remarks")
            getDataTable.Columns.Add("Added Value")
            getDataTable.Columns.Add("CPP(Chn Est)", GetType(Double))
            getDataTable.Columns.Add("CPP (" & TargetName & ")", GetType(Double))
            getDataTable.Columns.Add("Actual0", GetType(Double))
            getDataTable.Columns.Add("Actual1", GetType(Double))
            getDataTable.Columns.Add("Actual2", GetType(Double))
            getDataTable.Columns.Add("Actual3", GetType(Double))
            getDataTable.Columns.Add("Actual4", GetType(Double))
            getDataTable.Columns.Add("Duration", GetType(Double))
            getDataTable.Columns.Add("Color0")
            getDataTable.Columns.Add("Color1")
            getDataTable.Columns.Add("Color2")
            getDataTable.Columns.Add("Color3")
            getDataTable.Columns.Add("Color4")


            'Add all spots
            Dim row As DataRow
            Dim Days() As String = {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"}
            For Each spot As Trinity.cPlannedSpot In mCol

                row = getDataTable.Rows.Add()
                row.Item("ID") = spot.ID
                row.Item("Date") = Format(Date.FromOADate(spot.AirDate), "Short Date")
                row.Item("Time") = Trinity.Helper.Mam2Tid(spot.MaM)
                row.Item("Channel") = spot.Channel.Shortname
                row.Item("Bookingtype") = spot.Bookingtype.Name
                row.Item("Filmcode") = spot.Filmcode
                row.Item("Week") = DatePart(DateInterval.WeekOfYear, Date.FromOADate(spot.AirDate), FirstDayOfWeek.Monday, FirstWeekOfYear.FirstFourDays)
                row.Item("Weekday") = Days(Weekday(Date.FromOADate(spot.AirDate), vbMonday) - 1)
                If spot.Bookingtype Is Nothing OrElse spot.Bookingtype.Weeks(1).Films(spot.Filmcode) Is Nothing Then
                    row.Item("Film") = "<Unknown>"
                    row.Item("Film dscr") = ""
                Else
                    row.Item("Film") = spot.Bookingtype.Weeks(1).Films(spot.Filmcode).Name
                    row.Item("Film dscr") = spot.Bookingtype.Weeks(1).Films(spot.Filmcode).Description
                End If
                row.Item("Remarks") = spot.Remark
                row.Item("Program") = spot.Programme
                row.Item("Prog Before") = spot.ProgBefore
                row.Item("Prog After") = spot.ProgAfter
                row.Item("Duration") = spot.SpotLength
                row.Item("Gross Price") = Format(spot.PriceGross, "##,##0")
                row.Item("Net Price") = Format(spot.PriceNet, "##,##0")

                If Not spot.AddedValue Is Nothing Then
                    row.Item("Added Value") = spot.AddedValue.Name
                End If

                If spot.ChannelRating > 0 Then
                    If Not spot.MatchedSpot Is Nothing Then
                        row.Item("CPP (" & TargetName & ")") = Format(spot.PriceNet / spot.ChannelRating, "##,##0")
                    Else
                        row.Item("CPP (" & TargetName & ")") = "0"
                    End If
                Else
                    row.Item("CPP (" & TargetName & ")") = "0"
                End If

                If spot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                    If Not spot.MatchedSpot Is Nothing Then
                        row.Item("Gross CPP") = Format(spot.PriceNet / spot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "##,##0")
                    Else
                        row.Item("Gross CPP") = "0"
                    End If
                Else
                    row.Item("Gross CPP") = "0"
                End If

                If spot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) > 0 Then
                    If Not spot.MatchedSpot Is Nothing Then
                        row.Item("CPP(Chn Est)") = Format(spot.PriceNet / spot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "##,##0")
                    Else
                        row.Item("CPP(Chn Est)") = "0"
                    End If
                Else
                    row.Item("CPP(Chn Est)") = "0"
                End If

                row.Item("My Est0") = Format(spot.MyRating, "0.00")
                row.Item("Chan Est0") = Format(spot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget), "0.00")
                row.Item("My Est1") = Format(spot.MyRating * (spot.Bookingtype.IndexSecondTarget / spot.Bookingtype.IndexMainTarget), "0.00")
                row.Item("Chan Est1") = Format(spot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget), "0.00")
                row.Item("My Est2") = Format((spot.MyRating), "0.00")
                row.Item("Chan Est2") = Format(spot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteThirdTarget), "0.00")
                row.Item("My Est3") = Format((spot.MyRating), "0.00")
                row.Item("Chan Est3") = Format(spot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "0.00")
                row.Item("My Est4") = Format((spot.MyRating), "0.00")
                row.Item("Chan Est4") = Format(spot.ChannelRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteAllAdults), "0.00")

                If spot.MatchedSpot Is Nothing Then
                    row.Item("Actual0") = 0
                    row.Item("Actual1") = 0
                    row.Item("Actual2") = 0
                    row.Item("Actual3") = 0
                    row.Item("Actual4") = 0
                    row.Item("Color0") = ""
                    row.Item("Color1") = ""
                    row.Item("Color2") = ""
                    row.Item("Color3") = ""
                    row.Item("Color4") = ""
                Else
                    row.Item("Actual0") = Format(spot.MatchedSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget), "0.0")
                    row.Item("Actual1") = Format(spot.MatchedSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget), "0.0")
                    row.Item("Actual2") = Format(spot.MatchedSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateThirdTarget), "0.0")
                    row.Item("Actual3") = Format(spot.MatchedSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget), "0.0")
                    row.Item("Actual4") = Format(spot.MatchedSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateAllAdults), "0.0")

                    If CSng(Format(spot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget), "0.0")) > CSng(Format(spot.MatchedSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget), "0.0")) Then
                        row.Item("Color0") = "RED"
                    ElseIf CSng(Format(spot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteMainTarget), "0.0")) < CSng(Format(spot.MatchedSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateMainTarget), "0.0")) Then
                        row.Item("Color0") = "GREEN"
                    Else
                        row.Item("Color0") = "BLUE"
                    End If
                    If CSng(Format(spot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget), "0.0")) > CSng(Format(spot.MatchedSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget), "0.0")) Then
                        row.Item("Color1") = "RED"
                    ElseIf CSng(Format(spot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteSecondTarget), "0.0")) < CSng(Format(spot.MatchedSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateSecondTarget), "0.0")) Then
                        row.Item("Color1") = "GREEN"
                    Else
                        row.Item("Color1") = "BLUE"
                    End If
                    If CSng(Format(spot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteThirdTarget), "0.0")) > CSng(Format(spot.MatchedSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateThirdTarget), "0.0")) Then
                        row.Item("Color2") = "RED"
                    ElseIf CSng(Format(spot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteThirdTarget), "0.0")) < CSng(Format(spot.MatchedSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateThirdTarget), "0.0")) Then
                        row.Item("Color2") = "GREEN"
                    Else
                        row.Item("Color2") = "BLUE"
                    End If
                    If CSng(Format(spot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "0.0")) > CSng(Format(spot.MatchedSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget), "0.0")) Then
                        row.Item("Color3") = "RED"
                    ElseIf CSng(Format(spot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget), "0.0")) < CSng(Format(spot.MatchedSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget), "0.0")) Then
                        row.Item("Color3") = "GREEN"
                    Else
                        row.Item("Color3") = "BLUE"
                    End If
                    If CSng(Format(spot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteAllAdults), "0.0")) > CSng(Format(spot.MatchedSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateAllAdults), "0.0")) Then
                        row.Item("Color4") = "RED"
                    ElseIf CSng(Format(spot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteAllAdults), "0.0")) < CSng(Format(spot.MatchedSpot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateAllAdults), "0.0")) Then
                        row.Item("Color4") = "GREEN"
                    Else
                        row.Item("Color4") = "BLUE"
                    End If
                End If
            Next

        End Function

        Friend WriteOnly Property MainObject()
            Set(ByVal value)
                Dim TmpSpot As cPlannedSpot

                Main = value
                For Each TmpSpot In mCol
                    TmpSpot.MainObject = value
                Next
            End Set
        End Property

        Public Function Add(Optional ByVal ID As String = "") As cPlannedSpot
            'create a new object
            Dim objNewMember As cPlannedSpot
            objNewMember = New cPlannedSpot(Main)

            'sets a ID
            If ID = "" Then
                ID = CreateGUID()
            End If

            'set the properties passed into the method
            objNewMember.ID = ID
            mCol.Add(objNewMember, ID)

            'return the object created
            Add = objNewMember
            objNewMember = Nothing

        End Function
        Public sub AddTV4Spot(byval o As Object, byval tmpchan As cChannel, byval bt As cBookingType, Byval fk As String, optional byval ID As String = "")

            Dim objNewMember As cPlannedSpot
            objNewMember = New cPlannedSpot(Main)

            objNewMember.ID = ID
            objNewMember.Channel = o.Channel
            objNewMember.Bookingtype = o.BookingType
            objNewMember.Filmcode = fk
            objNewMember.Week = o.Week
            objNewMember.AirDate = o.AirDate
            objNewMember.MaM = o.MaM
            objNewMember.SpotLength = o.SpotLength
            objNewMember.Programme = o.Programme
            objNewMember.PriceGross = o.PriceGross
            objNewMember.PriceNet = o.PriceNet
            objNewMember.AddedValue = o.AddedValue
            objNewMember.MyRating = o.MyRating
            objNewMember.ChannelRating = o.Estimation
            'objNewMember.ChannelRating = o.ChannelRating

            
            Trinity.Helper.SetFilmForSpot(objNewMember)


            mCol.Add(objNewMember, ID)

        End sub

        Public Function TotalTRP() As Single
            Dim TmpTRP As Single = 0

            For Each TmpSpot As cPlannedSpot In mCol
                TmpTRP += TmpSpot.MyRating
            Next
            Return TmpTRP
        End Function

        Public Function TotalTRPToDeliver() As Single
            Dim TmpTRP As Single = 0

            For Each TmpSpot As cPlannedSpot In mCol
                If TmpSpot.AirDate > Main.UpdatedTo Then
                    TmpTRP += TmpSpot.MyRating
                End If
            Next
            Return TmpTRP
        End Function

        Public Function TotalNetBudget(Optional ByVal ChannelName As String = "", Optional ByVal Bookingtype As String = "") As Single
            Dim TmpBudget As Single = 0

            For Each TmpSpot As cPlannedSpot In mCol
                If (ChannelName = "" OrElse ChannelName = TmpSpot.Channel.ChannelName) AndAlso (Bookingtype = "" OrElse Bookingtype = TmpSpot.Bookingtype.Name) Then
                    TmpBudget += TmpSpot.PriceNet
                End If
            Next
            Return TmpBudget
        End Function

        Public Function TotalGrossBudget() As Single
            Dim TmpBudget As Single = 0

            For Each TmpSpot As cPlannedSpot In mCol
                TmpBudget += TmpSpot.PriceGross
            Next
            Return TmpBudget
        End Function

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As cPlannedSpot
            'used when referencing an element in the collection
            'vntIndexKey contains either the Index or Key to the collection,
            'this is why it is declared as a Variant
            'Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
            Get
                If vntIndexKey.GetType.ToString = "System.String" Then
                    If mCol.Contains(vntIndexKey) Then
                        Item = mCol(vntIndexKey)
                    Else
                        Return Nothing
                    End If
                Else
                    Item = mCol(vntIndexKey)
                End If
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

        Public Sub Remove(ByVal Spot As cPlannedSpot)
            mCol.Remove(Spot.ID)
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

    End Class
End Namespace