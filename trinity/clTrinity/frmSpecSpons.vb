Public Class frmSpecSpons

    Private _bookingtype As Trinity.cBookingType
    Public Property BookingType() As Trinity.cBookingType
        Get
            Return _bookingtype
        End Get
        Set(ByVal value As Trinity.cBookingType)
            _bookingtype = value
        End Set
    End Property


    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        cmdFind.Enabled = False
        Dim TmpAdedge As New ConnectWrapper.Programmes
        TmpAdedge.setArea(Campaign.Area)
        TmpAdedge.setPeriod(Format(dtFrom.Value, "ddMMyy") & "-" & Format(dtTo.Value, "ddMMyy"))
        TmpAdedge.setTargetMnemonic("3+", True)
        TmpAdedge.setChannels(_bookingtype.ParentChannel.AdEdgeNames)
        Trinity.Helper.AddTimeShift(TmpAdedge)
        Dim ProgCount As Integer = TmpAdedge.Run()

        Dim TmpList As New SortedList(Of String, String)
        For s As Integer = 0 To ProgCount - 1
            Dim ProgName As String = TmpAdedge.getAttrib(Connect.eAttribs.aProgTitle, s)
            If Not TmpList.ContainsKey(ProgName) Then
                TmpList.Add(ProgName, ProgName)
            End If
        Next

        lstAvailable.Items.Clear()
        For Each TmpString As String In TmpList.Values
            lstAvailable.Items.Add(TmpString)
        Next
        Me.Cursor = Windows.Forms.Cursors.Default
        cmdFind.Enabled = True
    End Sub

    Private Sub cmdPick_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPick.Click
        For Each item As String In lstAvailable.SelectedItems
            lstChosen.Items.Add(item)
        Next
    End Sub

    Private Sub cmdUnPick_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUnPick.Click
        While lstChosen.SelectedItems.Count > 0
            lstChosen.Items.Remove(lstChosen.SelectedItems(0))
        End While
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        _bookingtype.SpecificSponsringPrograms.Clear()
        For Each item As String In lstChosen.Items
            _bookingtype.SpecificSponsringPrograms.Add(item)
        Next

        If chkGetTRP.Checked Then

            For Each TmpWeek As Trinity.cWeek In _bookingtype.Weeks
                TmpWeek.TRP = 0
            Next

            Dim TmpAdedge As New ConnectWrapper.Brands
            Dim DateDiff As Long
            TmpAdedge.setArea(Campaign.Area)
            Trinity.Helper.AddTarget(TmpAdedge, Campaign.MainTarget, False)
            Trinity.Helper.AddTarget(TmpAdedge, _bookingtype.BuyingTarget.Target, False)

            If cmbPeriod.SelectedIndex = 0 Then
                Dim TmpDate As Long = Campaign.EndDate
                Dim PeriodStr As String = ""

                While TmpDate >= TmpAdedge.getDataRangeTo(Connect.eDataType.mSpot)
                    TmpDate = TmpDate - 1
                End While
                DateDiff = Campaign.EndDate - TmpDate

                For Each TmpWeek As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                    PeriodStr &= Format(Date.FromOADate(TmpWeek.StartDate - DateDiff), "ddMMyy") & "-" & Format(Date.FromOADate(TmpWeek.EndDate - DateDiff), "ddMMyy") & ","
                Next

                TmpAdedge.setPeriod(PeriodStr)
            ElseIf cmbPeriod.SelectedIndex = 1 Then
                Dim TmpDate As Long = Date.FromOADate(Campaign.EndDate).AddYears(-1).ToOADate
                Dim PeriodStr As String = ""

                While Weekday(Date.FromOADate(TmpDate), FirstDayOfWeek.Monday) <> Weekday(Date.FromOADate(Campaign.EndDate), FirstDayOfWeek.Monday)
                    TmpDate = TmpDate + 1
                End While
                DateDiff = Campaign.EndDate - TmpDate

                For Each TmpWeek As Trinity.cWeek In Campaign.Channels(1).BookingTypes(1).Weeks
                    PeriodStr &= Format(Date.FromOADate(TmpWeek.StartDate - DateDiff), "ddMMyy") & "-" & Format(Date.FromOADate(TmpWeek.EndDate - DateDiff), "ddMMyy") & ","
                Next
                TmpAdedge.setPeriod(PeriodStr)

            ElseIf cmbPeriod.SelectedIndex = 2 Then
                TmpAdedge.setPeriod(Format(dtFrom.Value, "ddMMyy") & "-" & Format(dtTo.Value, "ddMMyy"))
                DateDiff = Campaign.EndDate - dtTo.Value.ToOADate
            End If
            TmpAdedge.setChannels(_bookingtype.ParentChannel.AdEdgeNames)
            TmpAdedge.setBrandType("SPONSOR")

            Dim Spotcount As Long = TmpAdedge.Run

            Dim PickedSpots As New List(Of Long)
            Dim TotTRPMain As Single = 0
            Dim TotTRPBuying As Single = 0
            For s As Long = 0 To Spotcount - 1
                If _bookingtype.SpecificSponsringPrograms.Contains(TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, s)) Then
                    Dim TmpList As New List(Of Long)
                    Dim SpotNr As Long = s
                    Dim ThisProgBefore As String = TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, SpotNr)
                    Dim ThisProgAfter As String = TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, SpotNr)
                    Dim ThisProd As String = TmpAdedge.getAttrib(Connect.eAttribs.aBrandProduct, SpotNr)
                    Dim ThisProg As String
                    If SpotNr = Spotcount - 1 OrElse ThisProd <> TmpAdedge.getAttrib(Connect.eAttribs.aBrandProduct, SpotNr + 1) Then
                        'Not end break
                        ThisProg = ThisProgBefore
                    Else
                        ThisProg = ThisProgAfter
                    End If
                    While SpotNr > 0 AndAlso (TmpAdedge.getAttrib(Connect.eAttribs.aBrandProduct, SpotNr) = ThisProd AndAlso (TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, SpotNr) = ThisProg OrElse TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, SpotNr) = ThisProg))
                        SpotNr -= 1
                    End While
                    SpotNr += 1
                    If _bookingtype.ParentChannel.UseBillboards AndAlso SpotNr < Spotcount Then TmpList.Add(SpotNr)
                    SpotNr += 1
                    While SpotNr < Spotcount AndAlso (TmpAdedge.getAttrib(Connect.eAttribs.aBrandProduct, SpotNr) = ThisProd AndAlso (TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, SpotNr) = ThisProg OrElse TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, SpotNr) = ThisProg))
                        If _bookingtype.ParentChannel.UseBreakBumpers AndAlso (TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, SpotNr) = ThisProg AndAlso TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, SpotNr) = ThisProg) Then TmpList.Add(SpotNr)
                        If _bookingtype.ParentChannel.UseBillboards AndAlso (TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgBefore, SpotNr) = ThisProg AndAlso TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, SpotNr) <> ThisProg) Then TmpList.Add(SpotNr)
                        SpotNr += 1
                    End While

                    If TmpList.Count > 0 AndAlso _bookingtype.SpecificSponsringPrograms.Contains(TmpAdedge.getAttrib(Connect.eAttribs.aBrandProgAfter, TmpList(0))) Then
                        For Each TmpR As Long In TmpList
                            If Not PickedSpots.Contains(TmpR) Then
                                If GetWeek(Date.FromOADate(TmpAdedge.getAttrib(Connect.eAttribs.aDate, TmpR) + DateDiff)) IsNot Nothing Then
                                    TotTRPMain += TmpAdedge.getUnit(Connect.eUnits.uTRP, TmpR)
                                    TotTRPBuying += TmpAdedge.getUnit(Connect.eUnits.uTRP, TmpR, , , 1)
                                    GetWeek(Date.FromOADate(TmpAdedge.getAttrib(Connect.eAttribs.aDate, TmpR) + DateDiff)).TRPBuyingTarget += TmpAdedge.getUnit(Connect.eUnits.uTRP, TmpR, , , 1)
                                    GetWeek(Date.FromOADate(TmpAdedge.getAttrib(Connect.eAttribs.aDate, TmpR) + DateDiff)).TRPControl = True
                                End If
                                PickedSpots.Add(TmpR)
                            End If
                        Next
                    End If
                End If
            Next
            If TotTRPBuying > 0 Then
                _bookingtype.IndexMainTarget = (TotTRPMain / TotTRPBuying) * 100
            Else : _bookingtype.IndexMainTarget = 0
            End If

        End If
        Me.Close()
    End Sub

    Private Function GetWeek(ByVal [date] As Date) As Trinity.cWeek
        For Each TmpWeek As Trinity.cWeek In _bookingtype.Weeks
            If [date].ToOADate >= TmpWeek.StartDate AndAlso [date].ToOADate <= TmpWeek.EndDate Then
                Return TmpWeek
            End If
        Next
        Return Nothing
    End Function

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub frmSpecSpons_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lstChosen.Items.Clear()
        For Each TmpString As String In _bookingtype.SpecificSponsringPrograms
            lstChosen.Items.Add(TmpString)
        Next
        cmbPeriod.SelectedIndex = 0
    End Sub
End Class