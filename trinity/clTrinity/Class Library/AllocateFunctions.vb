Imports System.Windows.Forms

Namespace Trinity
    Public Class AllocateFunctions

        Shared Sub CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs, OnCampaign As cKampanj)
            If e.RowIndex < 0 Then Exit Sub
            If e.ColumnIndex < 0 Then Exit Sub

            If e.Button = Windows.Forms.MouseButtons.Right Then
                Dim mnu As New System.Windows.Forms.ContextMenuStrip

                Dim item As System.Windows.Forms.ToolStripItem = mnu.Items.Add("Copy to clipboard", Nothing, AddressOf DiscountsToClipBoard)
                item.Tag = New With {.Grid = sender, .Campaign = OnCampaign}
                item.Name = "CopyDiscounts"

                mnu.Show(sender, New System.Drawing.Point(DirectCast(sender, DataGridView).GetColumnDisplayRectangle(e.ColumnIndex, False).Left + e.X, DirectCast(sender, DataGridView).GetRowDisplayRectangle(e.RowIndex, False).Top + e.Y - 15))
            End If
        End Sub

        Private Shared Sub DiscountsToClipBoard(ByVal sender As Object, ByVal e As EventArgs)

            Dim SB As New System.Text.StringBuilder
            Dim CellFormat As String
            Dim grid As DataGridView = sender.tag.Grid

            SB.Append("Copied from Discounts in the campaign " & sender.tag.Campaign.Name & vbNewLine)

            SB.Append(vbTab & vbTab)

            For Each Cell As Windows.Forms.DataGridViewCell In grid.Rows(0).Cells
                SB.Append(Cell.OwningColumn.HeaderText & vbTab)
            Next

            SB.Append(vbNewLine)

            Dim Counter As Integer = 0

            For Each Row As Windows.Forms.DataGridViewRow In grid.Rows
                If Row.Tag.GetType.FullName = "clTrinity.Trinity.cBookingType" Then
                    SB.Append(DirectCast(Row.Tag, Trinity.cBookingType).ParentChannel.Shortname & " " & DirectCast(Row.Tag, Trinity.cBookingType).Name & vbTab)
                Else
                    SB.Append(DirectCast(Row.Tag, Trinity.cCombination).Name & " " & DirectCast(Row.Tag, Trinity.cCombination).Name & vbTab)
                End If
                Select Case Counter
                    Case 0
                        SB.Append("Eff. Discount" & vbTab)
                        CellFormat = "P1"
                    Case 1
                        SB.Append("Net CPP 30" & vbTab)
                        CellFormat = "N1"
                    Case 2
                        SB.Append("Actual CPP" & vbTab)
                        CellFormat = "N1"
                    Case 3
                        SB.Append("Index" & vbTab)
                        CellFormat = "N1"
                    Case Else
                        CellFormat = ""
                End Select
                For Each Cell As Windows.Forms.DataGridViewCell In Row.Cells
                    If Not Cell.Value Is Nothing AndAlso Cell.Value.ToString <> "-" AndAlso Cell.Value.ToString <> "NaN" Then
                        SB.Append(Format(Cell.Value, CellFormat).Replace(" ", "") & vbTab)
                    ElseIf Cell.Value IsNot Nothing Then
                        SB.Append("-" & vbTab)
                    End If

                Next
                SB.Append(vbNewLine)

                Counter += 1
                If Counter = 4 Then Counter = 0

            Next

            Windows.Forms.Clipboard.SetDataObject(SB.ToString(), True)

        End Sub

        Public Enum EstimationPeriodEnum
            LastWeeks
            LastYear
            CustomPeriod
        End Enum

        Public Shared Sub CalculateNaturalDelivery(ByVal grid As DataGridView, ByVal EstimationPeriod As EstimationPeriodEnum, OnCampaign As cKampanj, ByVal CustomPeriod As String)
            Saved = False
            Dim TmpAdedge As New ConnectWrapper.Breaks
            Dim TmpBT As Trinity.cBookingType
            Dim r As Integer
            Dim i As Integer
            Dim t As Integer
            Dim b As Long
            Dim Indx As Double
            Dim TRPDaypart(,)
            Dim BreakCount As Long
            Dim TmpDay As Date

            If Not OnCampaign.RootCampaign Is Nothing Then Exit Sub

            frmMain.Cursor = Windows.Forms.Cursors.WaitCursor
            TmpAdedge.setArea(OnCampaign.Area)
            If EstimationPeriod = EstimationPeriodEnum.LastWeeks Then
                Dim DateDiff As Long
                Dim PeriodStr As String = ""
                TmpDay = Date.FromOADate(OnCampaign.EndDate)
                While TmpDay.ToOADate >= TmpAdedge.getDataRangeTo(Connect.eDataType.mSpot)
                    TmpDay = TmpDay.AddDays(-1)
                End While
                DateDiff = OnCampaign.EndDate - TmpDay.ToOADate

                For Each TmpWeek As Trinity.cWeek In OnCampaign.Channels(1).BookingTypes(1).Weeks
                    PeriodStr &= Format(Date.FromOADate(TmpWeek.StartDate - DateDiff), "ddMMyy") & "-" & Format(Date.FromOADate(TmpWeek.EndDate - DateDiff), "ddMMyy") & ","
                Next
                TmpAdedge.setPeriod(PeriodStr)

            ElseIf EstimationPeriod = EstimationPeriodEnum.LastYear Then
                Dim StartDate As Date
                Dim EndDate As Date
                TmpDay = Date.FromOADate(OnCampaign.StartDate).AddYears(-1)
                While Not Weekday(TmpDay, FirstDayOfWeek.Monday) = Weekday(Date.FromOADate(OnCampaign.StartDate), FirstDayOfWeek.Monday)
                    TmpDay = TmpDay.AddDays(1)
                End While
                StartDate = TmpDay
                TmpDay = Date.FromOADate(OnCampaign.EndDate).AddYears(-1)
                While Not Weekday(TmpDay, FirstDayOfWeek.Monday) = Weekday(Date.FromOADate(OnCampaign.EndDate), FirstDayOfWeek.Monday)
                    TmpDay = TmpDay.AddDays(1)
                End While
                EndDate = TmpDay
                TmpAdedge.setPeriod(Format(StartDate, "ddMMyy") & "-" & Format(EndDate, "ddMMyy"))
            Else
                TmpAdedge.setPeriod(CustomPeriod)
            End If
            frmProgress.Status = "Calculating index..."
            frmProgress.Show()
            frmProgress.Progress = 0
            For r = 0 To grid.Rows.Count - 1

                If Not grid.Rows(r).Tag.Manualindexes Then
                    ReDim TRPDaypart(0 To 5, 0 To 5)
                    frmProgress.Progress = (r / (grid.Rows.Count)) * 100
                    If grid.Rows(r).Tag.GetType.FullName = "clTrinity.Trinity.cCombination" Then
                        Dim TmpC As Trinity.cCombination = grid.Rows(r).Tag
                        Dim sum As Single = 0

                        Dim SumIndexMain As Double = 0
                        Dim SumIndexSecond As Double = 0
                        Dim SumIndexAllAdults As Double = 0

                        For Each TmpCC As Trinity.cCombinationChannel In TmpC.Relations

                            TmpBT = TmpCC.Bookingtype
                            TmpAdedge.setChannelsArea(TmpBT.ParentChannel.AdEdgeNames, OnCampaign.Area)
                            TmpAdedge.clearTargetSelection()
                            Trinity.Helper.AddTarget(TmpAdedge, TmpBT.BuyingTarget.Target, False)
                            Trinity.Helper.AddTarget(TmpAdedge, OnCampaign.MainTarget, False)
                            Trinity.Helper.AddTarget(TmpAdedge, OnCampaign.SecondaryTarget, False)
                            Trinity.Helper.AddTarget(TmpAdedge, OnCampaign.ThirdTarget, False)
                            TmpAdedge.setTargetMnemonic(OnCampaign.AllAdults, False)
                            If TmpAdedge.getTargetCount < 5 Then
                                frmMain.Cursor = Windows.Forms.Cursors.Default
                                Windows.Forms.MessageBox.Show("One or more targets in this campaign is not correctly defined. Could not calculate Natural delivery.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                                Exit Sub
                            End If
                            'TmpAdedge.setTargetMnemonic(TmpBT.BuyingTarget.Target.TargetName & "," & OnCampaign.MainTarget.TargetName & "," & OnCampaign.SecondaryTarget.TargetName & "," & OnCampaign.ThirdTarget.TargetName & "," & OnCampaign.AllAdults)
                            'TmpAdedge.setUniverseUserDefined(OnCampaign.UniStr)
                            Trinity.Helper.AddTimeShift(TmpAdedge)
                            TmpAdedge.clearList()
                            BreakCount = TmpAdedge.Run
                            For dp As Integer = 0 To TmpBT.Dayparts.Count - 1
                                For t = 1 To 5
                                    TRPDaypart(dp, t) = 0
                                Next
                            Next
                            For b = 0 To BreakCount - 1
                                For t = 1 To 5
                                    Dim u As Integer
                                    u = OnCampaign.TimeShift
                                    TRPDaypart(TmpBT.Dayparts.GetDaypartIndexForMam(TmpAdedge.getAttrib(Connect.eAttribs.aFromTime, b) \ 60), t) += TmpAdedge.getUnit(Connect.eUnits.uTRP, b, , u, t - 1)
                                Next
                            Next
                            Indx = 0
                            Dim Share As Single = 0
                            For i = 0 To TmpBT.Dayparts.Count - 1
                                If TRPDaypart(i, 1) = 0 Then
                                    Indx += (TmpBT.Dayparts(i).Share / 100) * 1
                                Else
                                    Indx += (TmpBT.Dayparts(i).Share / 100) * (TRPDaypart(i, 2) / TRPDaypart(i, 1))
                                End If
                                Share += TmpBT.Dayparts(i).Share
                            Next
                            If Math.Round(Share) <> 100 Then
                                frmProgress.Hide()
                                Windows.Forms.MessageBox.Show(TmpBT.ToString & " does not have a daypartsplit that sums to 100." & vbCrLf & vbCrLf & "Natural delivery will not be accurate.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                frmProgress.Show()
                            End If

                            SumIndexMain += Indx * (TmpCC.Percent)

                            'grdIndex.Rows(r).Cells(0).Value = Format(Indx * 100, "N0")
                            Indx = 0
                            For i = 0 To TmpBT.Dayparts.Count - 1
                                If TRPDaypart(i, 1) = 0 Then
                                    Indx += (TmpBT.Dayparts(i).Share / 100) * 1
                                Else
                                    Indx += (TmpBT.Dayparts(i).Share / 100) * (TRPDaypart(i, 3) / TRPDaypart(i, 1))
                                End If
                            Next
                            SumIndexSecond += Indx * (TmpCC.Percent)
                            ' grdIndex.Rows(r).Cells(1).Value = Format(Indx * 100, "0")
                            Indx = 0
                            For i = 0 To TmpBT.Dayparts.Count - 1
                                If TRPDaypart(i, 1) = 0 Then
                                    Indx += (TmpBT.Dayparts(i).Share / 100) * 1
                                Else
                                    Indx += (TmpBT.Dayparts(i).Share / 100) * (TRPDaypart(i, 5) / TRPDaypart(i, 1))
                                End If
                            Next
                            SumIndexAllAdults += Indx * (TmpCC.Percent)
                        Next

                        TmpC.IndexMainTarget = SumIndexMain
                        grid.Rows(r).Cells(0).Value = Format(SumIndexMain * 100, "N0")
                        TmpC.IndexSecondTarget = SumIndexSecond
                        grid.Rows(r).Cells(1).Value = Format(SumIndexSecond * 100, "N0")
                        TmpC.IndexAllAdults = SumIndexAllAdults
                        grid.Rows(r).Cells(2).Value = Format(SumIndexAllAdults * 100, "N0")

                        TmpC.IndexMainTargetStatus = Trinity.cBookingType.IndexStatusEnum.NaturalDelivery
                        TmpC.IndexSecondTargetStatus = Trinity.cBookingType.IndexStatusEnum.NaturalDelivery
                        TmpC.IndexAllAdultsStatus = Trinity.cBookingType.IndexStatusEnum.NaturalDelivery
                        'grdIndex.Rows(r).Cells(2).Value = Format(sum * 100, "0")
                        ' grdIndex.Rows(r).Cells(2).Value = Format(Indx * 100, "0")
                        'TmpC.Relations(1).Bookingtype.IndexMainTargetStatus = Trinity.cBookingType.IndexStatusEnum.NaturalDelivery
                        'TmpC.Relations(1).Bookingtype.IndexSecondTargetStatus = Trinity.cBookingType.IndexStatusEnum.NaturalDelivery
                        'TmpC.Relations(1).Bookingtype.IndexAllAdultsStatus = Trinity.cBookingType.IndexStatusEnum.NaturalDelivery

                    Else
                        TmpBT = grid.Rows(r).Tag

                        'If the tag of this row informs us that the booking type has manually set indexes, then go to the next booking type
                        If TmpBT.ManualIndexes Then
                            Continue For
                        End If

                        TmpAdedge.setChannelsArea(TmpBT.ParentChannel.AdEdgeNames, OnCampaign.Area)
                        TmpAdedge.clearTargetSelection()
                        Trinity.Helper.AddTarget(TmpAdedge, TmpBT.BuyingTarget.Target, False)
                        Trinity.Helper.AddTarget(TmpAdedge, OnCampaign.MainTarget, False)
                        Trinity.Helper.AddTarget(TmpAdedge, OnCampaign.SecondaryTarget, False)
                        Trinity.Helper.AddTarget(TmpAdedge, OnCampaign.ThirdTarget, False)
                        TmpAdedge.setTargetMnemonic(OnCampaign.AllAdults, False)
                        If TmpAdedge.getTargetCount < 5 Then
                            frmMain.Cursor = Windows.Forms.Cursors.Default
                            Windows.Forms.MessageBox.Show("One or more targets in this campaign is not correctly defined. Could not calculate Natural delivery.", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
                            Exit Sub
                        End If
                        'TmpAdedge.setTargetMnemonic(TmpBT.BuyingTarget.Target.TargetName & "," & OnCampaign.MainTarget.TargetName & "," & OnCampaign.SecondaryTarget.TargetName & "," & OnCampaign.ThirdTarget.TargetName & "," & OnCampaign.AllAdults)
                        'TmpAdedge.setUniverseUserDefined(OnCampaign.UniStr)
                        Trinity.Helper.AddTimeShift(TmpAdedge)
                        TmpAdedge.clearList()
                        BreakCount = TmpAdedge.Run
                        For b = 0 To BreakCount - 1
                            For t = 1 To 5
                                Dim u As Integer
                                u = OnCampaign.TimeShift
                                TRPDaypart(TmpBT.Dayparts.GetDaypartIndexForMam(TmpAdedge.getAttrib(Connect.eAttribs.aFromTime, b) \ 60), t) += TmpAdedge.getUnit(Connect.eUnits.uTRP, b, , u, t - 1)
                            Next
                        Next
                        Indx = 0
                        Dim Share As Single = 0
                        For i = 0 To TmpBT.Dayparts.Count - 1
                            If TRPDaypart(i, 1) = 0 Then
                                Indx += (TmpBT.Dayparts(i).Share / 100) * 1
                            Else
                                Indx += (TmpBT.Dayparts(i).Share / 100) * (TRPDaypart(i, 2) / TRPDaypart(i, 1))
                            End If
                            Share += TmpBT.Dayparts(i).Share
                        Next
                        If Math.Round(Share) <> 100 Then
                            frmProgress.Hide()
                            Windows.Forms.MessageBox.Show(TmpBT.ToString & " does not have a daypartsplit that sums to 100." & vbCrLf & vbCrLf & "Natural delivery will not be accurate.", "T R I N I T Y", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            frmProgress.Show()
                        End If

                        grid.Rows(r).Cells(0).Value = Format(Indx * 100, "N0")
                        Indx = 0
                        For i = 0 To TmpBT.Dayparts.Count - 1
                            If TRPDaypart(i, 1) = 0 OrElse TRPDaypart(i, 1) < 0 Then
                                Indx += (TmpBT.Dayparts(i).Share / 100) * 1
                            Else
                                Indx += (TmpBT.Dayparts(i).Share / 100) * (TRPDaypart(i, 3) / TRPDaypart(i, 1))
                            End If
                        Next
                        grid.Rows(r).Cells(1).Value = Format(Indx * 100, "0")
                        Indx = 0
                        For i = 0 To TmpBT.Dayparts.Count - 1
                            If TRPDaypart(i, 1) = 0 OrElse TRPDaypart(i, 1) < 0 Then
                                Indx += (TmpBT.Dayparts(i).Share / 100) * 1
                            Else
                                Indx += (TmpBT.Dayparts(i).Share / 100) * (TRPDaypart(i, 5) / TRPDaypart(i, 1))
                            End If
                        Next
                        grid.Rows(r).Cells(2).Value = Format(Indx * 100, "0")
                        TmpBT.IndexMainTargetStatus = Trinity.cBookingType.IndexStatusEnum.NaturalDelivery
                        TmpBT.IndexSecondTargetStatus = Trinity.cBookingType.IndexStatusEnum.NaturalDelivery
                        TmpBT.IndexAllAdultsStatus = Trinity.cBookingType.IndexStatusEnum.NaturalDelivery
                    End If
                End If
            Next

            frmProgress.Hide()
            frmMain.Cursor = Windows.Forms.Cursors.Default
            grid.Invalidate()
        End Sub

        Shared Function ApplyDiscountCellFormat(ByVal Style As DataGridViewCellStyle, ByVal RowNumber As Integer, ByVal ColumnNumber As Integer) As DataGridViewCellStyle
            Select Case RowNumber Mod 4
                Case Is = 0
                    Style.Format = "P1"
                    Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
                    Style.ForeColor = Drawing.Color.Blue
                Case Is = 1
                    Style.Format = "N1"
                    Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
                    Style.ForeColor = Drawing.Color.Blue
                Case Is = 2
                    Style.Format = "N1"
                    Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
                    Style.ForeColor = Drawing.Color.Blue
                Case Is = 3
                    Style.Format = "N0"
                    Style.Alignment = Windows.Forms.DataGridViewContentAlignment.MiddleCenter
                    Style.ForeColor = Drawing.Color.Blue
            End Select
            Return Style
        End Function

        Shared Function GetDiscountCellValue(ByVal BookingType As cBookingType, ByVal RowNumber As Integer, ByVal ColumnName As String) As Object
            If ColumnName = "Total" Then
                Dim trps As Double = 0
                Dim netbudget As Double = 0
                Select Case RowNumber Mod 4
                    Case Is = 0

                        Dim discount As Double = 0
                        For Each week As Trinity.cWeek In BookingType.Weeks
                            trps += week.TRPBuyingTarget
                        Next

                        For Each week As Trinity.cWeek In BookingType.Weeks
                            discount += week.Discount(True) * (week.TRPBuyingTarget / trps)
                        Next
                        Return discount

                    Case Is = 1

                        Dim _trp30 As Single = 0
                        Dim _netBudget As Single = 0

                        For Each tmpWeek As Trinity.cWeek In BookingType.Weeks
                            _trp30 += tmpWeek.TRPBuyingTarget * (tmpWeek.SpotIndex / 100)
                            _netBudget += tmpWeek.NetBudget
                        Next
                        Return _netBudget / _trp30

                    Case Is = 2
                        For Each week As Trinity.cWeek In BookingType.Weeks
                            netbudget += week.NetBudget
                            trps += week.TRP
                        Next
                        Return netbudget / trps

                    Case Is = 3

                        Return "-"
                End Select
            Else

                Select Case RowNumber Mod 4
                    'all first rows of the channels, the discount
                    Case Is = 0

                        Dim TmpWeek As Trinity.cWeek = BookingType.Weeks(ColumnName)

                        Return TmpWeek.Discount(True)

                    Case Is = 1 'all second rows of the channels, the NetCPP30

                        Dim TmpWeek As Trinity.cWeek = BookingType.Weeks(ColumnName)

                        Return TmpWeek.NetCPP30()

                    Case Is = 2 'all third rows of the channels, the actual CPP buying target

                        Dim TmpWeek As Trinity.cWeek = BookingType.Weeks(ColumnName)
                        Return TmpWeek.NetCPP / (BookingType.IndexMainTarget / 100)

                    Case Is = 3 'all fourth rows of the channels, the index

                        Dim TmpWeek As Trinity.cWeek = BookingType.Weeks(ColumnName)

                        Return TmpWeek.Index() * 100

                End Select
            End If
            Return "-"
        End Function

        Shared Function GetDiscountCellValue(ByVal Combination As cCombination, ByVal RowNumber As Integer, ByVal ColumnName As String) As Object
            If ColumnName = "Total" Then
                Dim trps As Double = 0
                Dim netbudget As Double = 0
                Select Case RowNumber Mod 4
                    Case Is = 0
                        Dim discount As Double = 0
                        Dim count As Integer = 0

                        For Each tmpCC As Trinity.cCombinationChannel In Combination.Relations
                            For Each week As Trinity.cWeek In tmpCC.Bookingtype.Weeks
                                trps += week.TRPBuyingTarget
                            Next
                        Next

                        For Each tmpCC As Trinity.cCombinationChannel In Combination.Relations
                            For Each week As Trinity.cWeek In tmpCC.Bookingtype.Weeks
                                discount += week.Discount(True) * (week.TRPBuyingTarget / trps)
                            Next
                        Next
                        Return discount

                    Case Is = 1
                        Dim _trp30 As Single = 0
                        Dim _netBudget As Single = 0

                        Dim netCPP30 As Double = 0
                        Dim count As Integer = 0

                        For Each tmpCC As Trinity.cCombinationChannel In Combination.Relations
                            For Each tmpweek As Trinity.cWeek In tmpCC.Bookingtype.Weeks
                                _trp30 += tmpweek.TRPBuyingTarget * (tmpweek.SpotIndex / 100)
                                _netBudget += tmpweek.NetBudget
                            Next
                            'count += 1
                            'netCPP30 += (tmpCC.Bookingtype.Weeks(grdDiscounts.Columns(e.ColumnIndex).HeaderText).NetCPP30(True) * tmpCC.Percent)
                            '_trp30 += tmpCC.Bookingtype.Weeks(grdDiscounts.Columns(e.ColumnIndex).HeaderText).TRPBuyingTarget / (tmpCC.Bookingtype.Weeks(grdDiscounts.Columns(e.ColumnIndex).HeaderText).SpotIndex / 100)
                            '_netBudget += tmpCC.Bookingtype.Weeks(grdDiscounts.Columns(e.ColumnIndex).HeaderText).NetBudget
                        Next
                        'netCPP30 = _netBudget / _trp30
                        Return _netBudget / _trp30

                    Case Is = 2
                        For Each tmpCC As Trinity.cCombinationChannel In Combination.Relations
                            For Each week As Trinity.cWeek In tmpCC.Bookingtype.Weeks
                                netbudget += week.NetBudget
                                trps += week.TRP
                            Next
                        Next
                        If trps > 0 Then
                            Return netbudget / trps
                        Else
                            Return 0
                        End If

                    Case Is = 3


                End Select
            Else

                Select Case RowNumber Mod 4
                    'all first rows of the channels, the discount
                    Case Is = 0
                        Dim discount As Double = 0
                        Dim count As Integer = 0
                        For Each tmpCC As Trinity.cCombinationChannel In Combination.Relations
                            count += 1
                            discount += (tmpCC.Bookingtype.Weeks(ColumnName).Discount(True) * tmpCC.Percent)
                        Next
                        Return discount

                    Case Is = 1 'all second rows of the channels, the NetCPP30
                        Dim netCPP30 As Double = 0
                        Dim count As Integer = 0
                        For Each tmpCC As Trinity.cCombinationChannel In Combination.Relations
                            count += 1
                            netCPP30 += (tmpCC.Bookingtype.Weeks(ColumnName).NetCPP30(True) * tmpCC.Percent)
                        Next

                        Return netCPP30

                    Case Is = 2 'all third rows of the channels, the actual CPP buying target

                        Dim TmpBudget As Single = 0
                        Dim TmpTRP As Single = 0
                        For Each tmpCC As Trinity.cCombinationChannel In Combination.Relations
                            TmpBudget += tmpCC.Bookingtype.Weeks(ColumnName).NetBudget
                            TmpTRP += tmpCC.Bookingtype.Weeks(ColumnName).TRP
                        Next
                        If TmpTRP > 0 Then
                            Return TmpBudget / TmpTRP
                        Else
                            Return "-"
                        End If

                    Case Is = 3 'all fourth rows of the channels, the index
                        Dim index As Double = 0
                        Dim count As Integer = 0
                        For Each tmpCC As Trinity.cCombinationChannel In Combination.Relations
                            count += 1
                            index += ((tmpCC.Bookingtype.Weeks(ColumnName).Index * 100) * tmpCC.Percent)
                        Next

                        Return index

                End Select
            End If
            Return ""
        End Function

        Shared Sub ShowDiscountWindow(ByVal Combination As cCombination, ByVal WeekName As String)
            Dim TmpBT As Trinity.cBookingType
            Dim TmpIndex As Trinity.cIndex
            Dim TmpWeek As Trinity.cWeek
            Dim Idx As Single
            Dim r As Integer

            'start of printing one row combo detials

            Dim tmpC As Trinity.cCombination = Combination

            Dim frmDetails As New frmDetails
            'clears the grid,
            frmDetails.MaximumSize = New Size(frmDetails.Width, Screen.GetBounds(frmDetails).Height * 0.8)
            frmDetails.grdDetails.Rows.Clear()
            frmDetails.grdDetails.Rows.Add(1000)
            frmDetails.grdDetails.ForeColor = Color.Black
            frmDetails.StartPosition = FormStartPosition.CenterScreen

            For Each tmpCC As Trinity.cCombinationChannel In tmpC.Relations
                TmpBT = tmpCC.Bookingtype
                TmpWeek = tmpCC.Bookingtype.Weeks(WeekName)

                frmDetails.grdDetails.Rows(r).Cells(0).Value = tmpCC.Bookingtype.ToString

                r += 1

                'Calculate total index from Pricelist
                frmDetails.grdDetails.Rows(r).Cells(0).Value = "Base CPP 30"""

                Idx = 1
                For Each TmpIndex In TmpBT.Indexes
                    If TmpIndex.UseThis Then
                        If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
                            If (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate) Or (TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate And TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then
                                Idx = Idx * (TmpIndex.Index / 100)
                            End If
                        End If
                    End If
                Next

                ''Print out ratecard CPP
                'If TmpWeek.SpotIndex(True) > 0 Then
                '    Idx *= TmpWeek.SpotIndex(True) / 100
                'End If
                Dim TmpCPP As Single = 0
                For d As Long = TmpWeek.StartDate To TmpWeek.EndDate
                    TmpCPP += TmpBT.BuyingTarget.GetCPPForDate(d)
                Next
                TmpCPP /= (TmpWeek.EndDate - TmpWeek.StartDate + 1)
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpCPP, "C0")

                'Print out ratecard indexes
                If TmpBT.RatecardCPPIsGross Then
                    r += 1
                    For Each TmpIndex In TmpBT.BuyingTarget.Indexes
                        If TmpIndex.UseThis AndAlso TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
                            If (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate) Or (TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate And TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then

                                frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
                                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
                                r = r + 1
                            End If
                        End If
                    Next
                End If

                'Print out user created indexes on Gross CPP
                r += 1
                For Each TmpIndex In TmpBT.Indexes
                    If TmpIndex.UseThis Then
                        If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
                            If (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate) Or (TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate And TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then

                                frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
                                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
                                r = r + 1
                            End If
                        End If
                    End If
                Next
                frmDetails.grdDetails.Rows(r).Cells(1).Value = "Spot index"
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.SpotIndex(True), "N1")
                r += 1
                frmDetails.grdDetails.Rows(r).Cells(2).Value = "------------"
                r = r + 1
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.GrossCPP, "C0")
                r = r + 2

                'Print out the Discount, Net CPT or Net CPP depending on wich was enterd by the user
                If TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eDiscount Then
                    frmDetails.grdDetails.Rows(r).Cells(0).Value = "Discount"
                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.BuyingTarget.Discount, "P1")
                ElseIf TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eCPT Then
                    frmDetails.grdDetails.Rows(r).Cells(0).Value = "Net CPT"
                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.BuyingTarget.NetCPT, "N1")
                    r = r + 1
                    frmDetails.grdDetails.Rows(r).Cells(0).Value = "Universe"
                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.BuyingTarget.getUniSizeUni(TmpWeek.StartDate), "N0")
                ElseIf TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eCPP Then
                    frmDetails.grdDetails.Rows(r).Cells(0).Value = "Net CPP"
                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.BuyingTarget.NetCPP, "N1")
                ElseIf TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eEnhancement Then
                    frmDetails.grdDetails.Rows(r).Cells(0).Value = "Enhancement"
                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.BuyingTarget.Enhancement, "N1")
                End If
                r = r + 1
                If TmpBT.EnhancementFactor <> 100 Then
                    frmDetails.grdDetails.Rows(r).Cells(0).Value = "Enh.Factor"
                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.EnhancementFactor, "N1")
                    r += 1
                End If

                For Each TmpIndex In TmpBT.BuyingTarget.Indexes
                    If TmpIndex.UseThis Then
                        If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP Then
                            If TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate Then
                                If Not (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then
                                    'The index is only partly used on this week. Calculate how many days and display it.
                                    Dim _weekLength As Integer = TmpWeek.EndDate - TmpWeek.StartDate + 1
                                    Dim _indexLength As Integer = Math.Abs(TmpWeek.StartDate - TmpIndex.FromDate.ToOADate) * Math.Abs(CInt(TmpIndex.FromDate.ToOADate > TmpWeek.StartDate))
                                    _indexLength += Math.Abs(TmpWeek.EndDate - TmpIndex.ToDate.ToOADate) * Math.Abs(CInt(TmpIndex.ToDate.ToOADate < TmpWeek.EndDate))
                                    frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name & " (" & _weekLength - _indexLength & " / " & _weekLength & " days)"
                                Else
                                    'The index is for the entire period
                                    frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
                                End If
                                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
                                r = r + 1
                            End If
                        End If
                    End If
                Next
                'Print out indexes that is on Net CPP or TRP
                For Each TmpIndex In TmpBT.Indexes
                    If TmpIndex.UseThis Then
                        If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP Then
                            If (TmpIndex.FromDate.ToOADate >= TmpWeek.StartDate And TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate) Or (TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate <= TmpWeek.EndDate) Then
                                frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
                                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
                                r = r + 1
                            ElseIf TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate Then
                                frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
                                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
                                r = r + 1
                            End If
                        End If
                    End If
                Next
                If TmpWeek.Discount >= TmpBT.MaxDiscount Then
                    frmDetails.grdDetails.Rows(r).Cells(1).Value = "Max Discount"
                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.MaxDiscount, "P0")
                    r += 1
                End If
                For Each TmpIndex In TmpBT.Indexes
                    If TmpIndex.UseThis Then
                        If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPPAfterMaxDiscount Then
                            If (TmpIndex.FromDate.ToOADate >= TmpWeek.StartDate And TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate) Or (TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate <= TmpWeek.EndDate) Then
                                frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
                                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
                                r = r + 1
                            ElseIf TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate Then
                                frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
                                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
                                r = r + 1
                            End If
                        End If
                    End If
                Next
                For Each TmpIndex In TmpBT.Indexes
                    If TmpIndex.UseThis Then
                        If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eTRP Then
                            If (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate) Or (TmpIndex.FromDate.ToOADate >= TmpWeek.StartDate And TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate) Then

                                frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
                                If TmpIndex.Enhancements.Count = 0 Then
                                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1") & " (TRP)"
                                Else
                                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Enhancements.FactoredIndex, "N1") & " (TRP)"
                                    For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
                                        If TmpEnh.UseThis Then
                                            r += 1
                                            frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpEnh.Name
                                            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpEnh.Amount, "N1")
                                            frmDetails.grdDetails.Rows(r).DefaultCellStyle.ForeColor = Color.LightGray
                                        End If
                                    Next
                                End If
                                r = r + 1
                            End If
                        End If
                    End If
                Next
                frmDetails.grdDetails.Rows(r).Cells(2).Value = "------------"
                r = r + 1

                'Print out final 30 sec Net CPP
                frmDetails.grdDetails.Rows(r).Cells(0).Value = "Net CPP 30"""
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.NetCPP30(), "C1")

                r = r + 2
                frmDetails.grdDetails.Rows(r).Cells(1).Value = "Spot index"
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.SpotIndex, "N1")

                r = r + 2
                frmDetails.grdDetails.Rows(r).Cells(0).Value = "Net CPP"
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.NetCPP, "C1")

                r += 1
                frmDetails.grdDetails.Rows(r).Cells(0).Value = "------------"
                frmDetails.grdDetails.Rows(r).Cells(2).Value = "------------"

                r += 2

            Next

            While frmDetails.grdDetails.Rows.Count > r + 1
                frmDetails.grdDetails.Rows.Remove(frmDetails.grdDetails.Rows(frmDetails.grdDetails.Rows.Count - 1))
            End While

            frmDetails.ShowDialog()

        End Sub

        Shared Sub ShowDiscountWindow(ByVal Bookingtype As cBookingType, ByVal WeekName As String)
            'start of printing normal details
            Dim TmpBT As cBookingType = Bookingtype
            Dim TmpWeek As cWeek = Bookingtype.Weeks(WeekName)

            Dim frmDetails As New frmDetails
            'clears the grid
            frmDetails.grdDetails.Rows.Clear()
            frmDetails.MaximumSize = New Size(frmDetails.Width, Screen.GetBounds(frmDetails).Height * 0.8)
            frmDetails.grdDetails.Rows.Add(100)
            frmDetails.grdDetails.ForeColor = Color.Black
            frmDetails.cmdExcel.Visible = True
            frmDetails.Tag = New With {Key .Week = TmpWeek, .Bookingtype = TmpBT}
            frmDetails.StartPosition = FormStartPosition.CenterScreen

            Dim e As New ExportEventArgs With {.Bookingtype = Bookingtype, .Week = TmpWeek}
            frmDetails.cmdExcel.Tag = e
            AddHandler frmDetails.cmdExcel.Click, AddressOf cmdExcel_Click

            'grdDiscounts.Tag = frmDetails

            ''Calculate total index from Pricelist
            frmDetails.grdDetails.Rows(0).Cells(0).Value = "Base CPP 30"""
            Dim r As Integer = 0
            Dim Idx As Single
            Dim TmpIndex As cIndex

            Idx = 1
            For Each TmpIndex In TmpBT.Indexes
                If TmpIndex.UseThis Then
                    If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
                        If (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate) Or (TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate And TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then
                            Idx = Idx * (TmpIndex.Index / 100)
                        End If
                    End If
                End If
            Next
            ''Print out ratecard CPP
            'If TmpWeek.SpotIndex(True) > 0 Then
            '    Idx *= TmpWeek.SpotIndex(True) / 100
            'End If
            Dim TmpCPP As Single = 0
            For d As Long = TmpWeek.StartDate To TmpWeek.EndDate
                TmpCPP += TmpBT.BuyingTarget.GetCPPForDate(d)
            Next
            TmpCPP /= (TmpWeek.EndDate - TmpWeek.StartDate + 1)
            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpCPP, "C0")

            'Print out ratecard indexes
            r += 1
            If TmpBT.RatecardCPPIsGross Then
                For Each TmpIndex In TmpBT.BuyingTarget.Indexes
                    If TmpIndex.UseThis AndAlso TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
                        If TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate Then
                            If Not (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then
                                'The index is only partly used on this week. Calculate how many days and display it.
                                Dim _weekLength As Integer = TmpWeek.EndDate - TmpWeek.StartDate + 1
                                Dim _indexLength As Integer = Math.Abs(TmpWeek.StartDate - TmpIndex.FromDate.ToOADate) * Math.Abs(CInt(TmpIndex.FromDate.ToOADate > TmpWeek.StartDate))
                                _indexLength += Math.Abs(TmpWeek.EndDate - TmpIndex.ToDate.ToOADate) * Math.Abs(CInt(TmpIndex.ToDate.ToOADate < TmpWeek.EndDate))
                                frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name & " (" & _weekLength - _indexLength & " / " & _weekLength & " days)"
                            Else
                                'The index is for the entire period
                                frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
                            End If
                            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
                            r = r + 1
                        End If
                    End If
                Next
            End If

            'Print out user created indexes on Gross CPP
            For Each TmpIndex In TmpBT.Indexes
                If TmpIndex.UseThis Then
                    If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
                        If TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate Then
                            If Not (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then
                                'The index is only partly used on this week. Calculate how many days and display it.
                                Dim _weekLength As Integer = TmpWeek.EndDate - TmpWeek.StartDate + 1
                                Dim _indexLength As Integer = Math.Abs(TmpWeek.StartDate - TmpIndex.FromDate.ToOADate) * Math.Abs(CInt(TmpIndex.FromDate.ToOADate > TmpWeek.StartDate))
                                _indexLength += Math.Abs(TmpWeek.EndDate - TmpIndex.ToDate.ToOADate) * Math.Abs(CInt(TmpIndex.ToDate.ToOADate < TmpWeek.EndDate))
                                frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name & " (" & _weekLength - _indexLength & " / " & _weekLength & " days)"
                            Else
                                'The index is for the entire period
                                frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
                            End If
                            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
                            r = r + 1
                        End If
                    End If
                End If
            Next
            frmDetails.grdDetails.Rows(r).Cells(1).Value = "Spot index"
            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.SpotIndex(True), "N1")
            r += 1
            If TmpWeek.AddedValueIndexGross <> 1 Then
                frmDetails.grdDetails.Rows(r).Cells(1).Value = "Added values"
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.AddedValueIndexGross * 100, "N1")
                r += 1
            End If
            frmDetails.grdDetails.Rows(r).Cells(2).Value = "------------"
            r = r + 1
            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.GrossCPP, "C0")
            r = r + 2

            'Print out the Dicount, Net CPT or Net CPP depending on wich was enterd by the user
            If TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eDiscount Then
                frmDetails.grdDetails.Rows(r).Cells(0).Value = "Discount"
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.BuyingTarget.Discount, "P1")
            ElseIf TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eCPT Then
                frmDetails.grdDetails.Rows(r).Cells(0).Value = "Net CPT"
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.BuyingTarget.NetCPT, "N1")
                r = r + 1
                frmDetails.grdDetails.Rows(r).Cells(0).Value = "Universe"
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.BuyingTarget.getUniSizeUni(TmpWeek.StartDate), "N0")
            ElseIf TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eCPP Then
                frmDetails.grdDetails.Rows(r).Cells(0).Value = "Net CPP"
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.BuyingTarget.NetCPP, "N1")
            ElseIf TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eEnhancement Then
                frmDetails.grdDetails.Rows(r).Cells(0).Value = "Enhancement"
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.BuyingTarget.Enhancement, "N1")
            End If
            r += 1

            For Each TmpIndex In TmpBT.BuyingTarget.Indexes
                If TmpIndex.UseThis Then
                    If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP Then
                        If TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate Then
                            If Not (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then
                                'The index is only partly used on this week. Calculate how many days and display it.
                                Dim _weekLength As Integer = TmpWeek.EndDate - TmpWeek.StartDate + 1
                                Dim _indexLength As Integer = Math.Abs(TmpWeek.StartDate - TmpIndex.FromDate.ToOADate) * Math.Abs(CInt(TmpIndex.FromDate.ToOADate > TmpWeek.StartDate))
                                _indexLength += Math.Abs(TmpWeek.EndDate - TmpIndex.ToDate.ToOADate) * Math.Abs(CInt(TmpIndex.ToDate.ToOADate < TmpWeek.EndDate))
                                frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name & " (" & _weekLength - _indexLength & " / " & _weekLength & " days)"
                            Else
                                'The index is for the entire period
                                frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
                            End If
                            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
                            r = r + 1
                        End If
                    End If
                End If
            Next
            'Print out indexes that is on Net CPP or TRP
            For Each TmpIndex In TmpBT.Indexes
                If TmpIndex.UseThis Then
                    If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP Then
                        If TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate Then
                            If Not (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then
                                'The index is only partly used on this week. Calculate how many days and display it.
                                Dim _weekLength As Integer = TmpWeek.EndDate - TmpWeek.StartDate + 1
                                Dim _indexLength As Integer = Math.Abs(TmpWeek.StartDate - TmpIndex.FromDate.ToOADate) * Math.Abs(CInt(TmpIndex.FromDate.ToOADate > TmpWeek.StartDate))
                                _indexLength += Math.Abs(TmpWeek.EndDate - TmpIndex.ToDate.ToOADate) * Math.Abs(CInt(TmpIndex.ToDate.ToOADate < TmpWeek.EndDate))
                                frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name & " (" & _weekLength - _indexLength & " / " & _weekLength & " days)"
                            Else
                                'The index is for the entire period
                                frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
                            End If
                            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
                            r = r + 1
                        End If
                    End If
                End If
            Next
            If TmpWeek.Discount >= TmpBT.MaxDiscount Then
                frmDetails.grdDetails.Rows(r).Cells(0).Value = "Max Discount"
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.MaxDiscount, "P0")
                frmDetails.grdDetails.Rows(r).Cells(0).Style.ForeColor = Color.Red
                frmDetails.grdDetails.Rows(r).Cells(2).Style.ForeColor = Color.Red
                r += 1
            End If
            For Each TmpIndex In TmpBT.Indexes
                If TmpIndex.UseThis Then
                    If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPPAfterMaxDiscount Then
                        If TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate Then
                            If Not (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then
                                'The index is only partly used on this week. Calculate how many days and display it.
                                Dim _weekLength As Integer = TmpWeek.EndDate - TmpWeek.StartDate + 1
                                Dim _indexLength As Integer = Math.Abs(TmpWeek.StartDate - TmpIndex.FromDate.ToOADate) * Math.Abs(CInt(TmpIndex.FromDate.ToOADate > TmpWeek.StartDate))
                                _indexLength += Math.Abs(TmpWeek.EndDate - TmpIndex.ToDate.ToOADate) * Math.Abs(CInt(TmpIndex.ToDate.ToOADate < TmpWeek.EndDate))
                                frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name & " (" & _weekLength - _indexLength & " / " & _weekLength & " days)"
                            Else
                                'The index is for the entire period
                                frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
                            End If
                            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1")
                            r = r + 1
                        End If
                    End If
                End If
            Next
            For Each TmpIndex In TmpBT.Indexes
                If TmpIndex.UseThis Then
                    If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eTRP Then
                        If TmpIndex.Enhancements.Count = 0 Then
                            If TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate Then
                                If Not (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then
                                    'The index is only partly used on this week. Calculate how many days and display it.
                                    Dim _weekLength As Integer = TmpWeek.EndDate - TmpWeek.StartDate + 1
                                    Dim _indexLength As Integer = Math.Abs(TmpWeek.StartDate - TmpIndex.FromDate.ToOADate) * Math.Abs(CInt(TmpIndex.FromDate.ToOADate > TmpWeek.StartDate))
                                    _indexLength += Math.Abs(TmpWeek.EndDate - TmpIndex.ToDate.ToOADate) * Math.Abs(CInt(TmpIndex.ToDate.ToOADate < TmpWeek.EndDate))
                                    frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name & " (" & _weekLength - _indexLength & " / " & _weekLength & " days)"
                                Else
                                    'The index is for the entire period
                                    frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
                                End If
                                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Index, "N1") & " (TRP)"
                                r = r + 1
                            End If
                        Else
                            If TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate Then
                                frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpIndex.Name
                                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpIndex.Enhancements.FactoredIndex, "N1")
                                r = r + 1
                                For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
                                    If TmpEnh.UseThis Then
                                        frmDetails.grdDetails.Rows(r).Cells(1).Value = TmpEnh.Name
                                        frmDetails.grdDetails.Rows(r).Cells(2).Value = TmpEnh.Amount
                                        frmDetails.grdDetails.Rows(r).DefaultCellStyle.ForeColor = Color.Gray
                                        r = r + 1
                                    End If
                                Next
                                If TmpBT.EnhancementFactor <> 100 Then
                                    frmDetails.grdDetails.Rows(r).Cells(1).Value = "Enh. Factor"
                                    frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpBT.EnhancementFactor, "N1")
                                    frmDetails.grdDetails.Rows(r).DefaultCellStyle.ForeColor = Color.Gray
                                    r += 1
                                End If
                            End If
                        End If
                    End If
                End If
            Next
            If TmpWeek.AddedValueIndexNet <> 1 Then
                frmDetails.grdDetails.Rows(r).Cells(1).Value = "Added values"
                frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.AddedValueIndexNet * 100, "N1")
                r += 1
            End If
            r = r + 1
            frmDetails.grdDetails.Rows(r).Cells(2).Value = "------------"
            'Print out final 30 sec Net CPP
            frmDetails.grdDetails.Rows(r).Cells(0).Value = "Net CPP 30"""
            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.NetCPP30(), "C1")
            r = r + 2

            frmDetails.grdDetails.Rows(r).Cells(1).Value = "Spot index"
            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.SpotIndex, "N1")
            r += 2

            frmDetails.grdDetails.Rows(r).Cells(0).Value = "Net CPP"
            frmDetails.grdDetails.Rows(r).Cells(2).Value = Format(TmpWeek.NetCPP, "C1")

            While frmDetails.grdDetails.Rows.Count > r + 1
                frmDetails.grdDetails.Rows.Remove(frmDetails.grdDetails.Rows(frmDetails.grdDetails.Rows.Count - 1))
            End While
            frmDetails.grdDetails.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.DisplayedCells)

            frmDetails.ShowDialog()

            'end of printing normal details
        End Sub

        Private Shared Sub cmdExcel_Click(ByVal sender As Object, ByVal e As EventArgs)
            ExportDetailsToExcel(sender, sender.tag)
        End Sub

        Private Shared Sub ExportDetailsToExcel(ByVal sender As Object, ByVal e As ExportEventArgs)

            Dim Excel As New CultureSafeExcel.Application(False)  'CreateObject("CultureSafeExcel")
            Dim WB As CultureSafeExcel.Workbook

            Excel.ScreenUpdating = False
            Excel.DisplayAlerts = False

            WB = Excel.AddWorkbook

            Dim TmpWeek As Trinity.cWeek = e.Week
            Dim TmpBT As Trinity.cBookingType = e.Bookingtype
            Dim r As Integer = 1

            With WB.Sheets(1)                
                .cells(1, 1).value = "Gross CPP 30"""
                Dim TmpCPP As Single = 0
                For d As Long = TmpWeek.StartDate To TmpWeek.EndDate
                    TmpCPP += TmpBT.BuyingTarget.GetCPPForDate(d)
                Next
                TmpCPP /= (TmpWeek.EndDate - TmpWeek.StartDate + 1)
                .cells(1, 4).Value = TmpCPP

                'Print out ratecard indexes
                r += 1
                Dim _startList As Integer = r
                For Each TmpIndex As Trinity.cIndex In TmpBT.BuyingTarget.Indexes
                    If TmpIndex.UseThis Then
                        If TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate Then
                            If Not (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then
                                'The index is only partly used on this week. Calculate how many days and display it.
                                Dim _weekLength As Integer = TmpWeek.EndDate - TmpWeek.StartDate + 1
                                Dim _indexLength As Integer = Math.Abs(TmpWeek.StartDate - TmpIndex.FromDate.ToOADate) * Math.Abs(CInt(TmpIndex.FromDate.ToOADate > TmpWeek.StartDate))
                                _indexLength += Math.Abs(TmpWeek.EndDate - TmpIndex.ToDate.ToOADate) * Math.Abs(CInt(TmpIndex.ToDate.ToOADate < TmpWeek.EndDate))
                                .cells(r, 2).Value = TmpIndex.Name & " (" & _weekLength - _indexLength & " / " & _weekLength & " days)"
                                .cells(r, 3).Value = "=" & TmpIndex.Index / 100 & "*(" & _weekLength - _indexLength & "/" & _weekLength & ")"
                            Else
                                'The index is for the entire period
                                .cells(r, 2).Value = TmpIndex.Name
                                .cells(r, 4).Value = TmpIndex.Index / 100
                            End If
                            r = r + 1
                        End If
                    End If
                Next
                If _startList < r Then
                    .cells(r - 1, 4).value = "=SUM(" & .cells(_startList, 3).address & ":" & .cells(r - 1, 3).address & ")"
                End If
                _startList = r
                For Each TmpIndex As Trinity.cIndex In TmpBT.Indexes
                    If TmpIndex.UseThis Then
                        If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eGrossCPP Then
                            If TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate Then
                                If Not (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then
                                    'The index is only partly used on this week. Calculate how many days and display it.
                                    Dim _weekLength As Integer = TmpWeek.EndDate - TmpWeek.StartDate + 1
                                    Dim _indexLength As Integer = Math.Abs(TmpWeek.StartDate - TmpIndex.FromDate.ToOADate) * Math.Abs(CInt(TmpIndex.FromDate.ToOADate > TmpWeek.StartDate))
                                    _indexLength += Math.Abs(TmpWeek.EndDate - TmpIndex.ToDate.ToOADate) * Math.Abs(CInt(TmpIndex.ToDate.ToOADate < TmpWeek.EndDate))
                                    .cells(r, 2).Value = TmpIndex.Name & " (" & _weekLength - _indexLength & " / " & _weekLength & " days)"
                                    .cells(r, 3).Value = "=" & TmpIndex.Index / 100 & "*(" & _weekLength - _indexLength & "/" & _weekLength & ")"
                                Else
                                    'The index is for the entire period
                                    .cells(r, 2).Value = TmpIndex.Name
                                    .cells(r, 4).Value = TmpIndex.Index / 100
                                End If
                                r = r + 1
                            End If
                        End If
                    End If
                Next
                If _startList < r Then
                    .cells(r - 1, 4).value = "=SUM(" & .cells(_startList, 3).address & ":" & .cells(r - 1, 3).address & ")"
                End If
                Dim GrossCPP30Formula As String = "PRODUCT(" & .cells(1, 4).address & ":" & .cells(r - 1, 4).address & ")"

                .cells(r, 2).Value = "Spot index"
                .cells(r, 4).Value = TmpWeek.SpotIndex(True) / 100
                r += 1
                If TmpWeek.AddedValueIndexGross <> 1 Then
                    .cells(r, 2).Value = "Added values"
                    .cells(r, 4).Value = TmpWeek.AddedValueIndexGross
                    r += 1
                End If
                With .Cells(r, 4).Borders(8)
                    .LineStyle = -4115
                    .Weight = -4138
                    .ColorIndex = -4105
                End With
                Dim NetCPPFormula As String = ""
                .Cells(r, 4).Formula = "=PRODUCT(" & .Cells(1, 4).Address & ":" & .Cells(r - 1, 4).Address & ")"
                r += 2
                'Print out the Dicount, Net CPT or Net CPP depending on wich was enterd by the user
                If TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eDiscount Then
                    .Cells(r, 1).Value = "Discount"
                    .Cells(r, 4).Value = TmpBT.BuyingTarget.Discount
                    NetCPPFormula = GrossCPP30Formula & "*(1-" & .Cells(r, 4).Address & ")"
                ElseIf TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eCPT Then
                    .Cells(r, 1).Value = "Net CPT"
                    .Cells(r, 4).Value = TmpBT.BuyingTarget.NetCPT
                    r += 1
                    .Cells(r, 1).Value = "Universe"
                    .Cells(r, 4).Value = Format(TmpBT.BuyingTarget.getUniSizeUni(TmpWeek.StartDate), "N0")
                    NetCPPFormula = "(" & .Cells(r - 1, 4).Address & "*" & .Cells(r, 4).Address & "/100)"
                ElseIf TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eCPP Then
                    .Cells(r, 1).Value = "Net CPP"
                    .Cells(r, 4).Value = TmpBT.BuyingTarget.NetCPP
                    NetCPPFormula = .Cells(r, 4).Address
                ElseIf TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eEnhancement Then
                    .Cells(r, 1).Value = "Enhancement"
                    .Cells(r, 4).Value = TmpBT.BuyingTarget.Enhancement
                    NetCPPFormula = GrossCPP30Formula
                End If
                Dim MainUnitRow As Integer = r
                Dim IndexList As New List(Of String)
                Dim EnhancementList As New List(Of String)
                r += 1

                'Print out indexes that is on Net CPP or TRP
                For Each TmpIndex As Trinity.cIndex In TmpBT.Indexes
                    If TmpIndex.UseThis Then
                        If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eTRP Then
                            If TmpIndex.Enhancements.Count = 0 Then
                                If TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate Then
                                    If Not (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then
                                        'The index is only partly used on this week. Calculate how many days and display it.
                                        Dim _weekLength As Integer = TmpWeek.EndDate - TmpWeek.StartDate + 1
                                        Dim _indexLength As Integer = Math.Abs(TmpWeek.StartDate - TmpIndex.FromDate.ToOADate) * Math.Abs(CInt(TmpIndex.FromDate.ToOADate > TmpWeek.StartDate))
                                        _indexLength += Math.Abs(TmpWeek.EndDate - TmpIndex.ToDate.ToOADate) * Math.Abs(CInt(TmpIndex.ToDate.ToOADate < TmpWeek.EndDate))
                                        .Cells(r, 2).Value = TmpIndex.Name & " (" & _weekLength - _indexLength & " / " & _weekLength & " days)"
                                    Else
                                        'The index is for the entire period
                                        .Cells(r, 2).Value = TmpIndex.Name
                                    End If
                                    .Cells(r, 4).Value = Format(TmpIndex.Index, "N1") & " (TRP)"
                                    r = r + 1
                                End If
                            Else
                                Dim StartRow As Integer = r
                                If TmpBT.BuyingTarget.IsEntered = Trinity.cPricelistTarget.EnteredEnum.eEnhancement Then
                                    StartRow -= 1
                                End If
                                If (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate And TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate) Or (TmpIndex.FromDate.ToOADate >= TmpWeek.StartDate And TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate) Then
                                    For Each TmpEnh As Trinity.cEnhancement In TmpIndex.Enhancements
                                        If TmpEnh.UseThis Then
                                            .Cells(r, 2).Value = TmpEnh.Name
                                            .Cells(r, 4).Value = TmpEnh.Amount / 100
                                            .Rows(r).Font.color = RGB(127, 127, 127)
                                            r = r + 1
                                        End If
                                    Next
                                    Dim EndRow As Integer = r - 1
                                    If TmpBT.EnhancementFactor <> 100 Then
                                        .Cells(r, 2).Value = "Enh. Factor"
                                        .Cells(r, 4).Value = TmpBT.EnhancementFactor / 100
                                        .Rows(r).Font.color = RGB(127, 127, 127)
                                        r += 1
                                    End If
                                    .Cells(r, 2).Value = TmpIndex.Name
                                    If TmpBT.EnhancementFactor <> 100 Then
                                        .Cells(r, 4).Formula = "=1/(1-(SUM(C" & StartRow & ":C" & EndRow & ") / (1 + SUM(C" & StartRow & ":C" & EndRow & ")) / C" & EndRow + 1 & "))"
                                    Else
                                        .Cells(r, 4).Formula = "=1+SUM(C" & StartRow & ":C" & EndRow & ")"
                                    End If
                                    If TmpIndex.Enhancements.Count > 0 Then
                                        EnhancementList.Add("SUM(C" & StartRow & ":C" & EndRow & ")")
                                    Else
                                        IndexList.Add("(1/" & .Cells(r, 4).Address() & ")")
                                    End If
                                    '.cells(r, 4).Value = Format(TmpIndex.Enhancements.FactoredIndex, "N1")
                                    r = r + 1
                                End If
                            End If
                        End If
                    End If
                Next

                For Each TmpIndex As Trinity.cIndex In TmpBT.Indexes
                    If TmpIndex.UseThis Then
                        If TmpIndex.IndexOn = Trinity.cIndex.IndexOnEnum.eNetCPP Then
                            If TmpIndex.FromDate.ToOADate <= TmpWeek.EndDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.StartDate Then
                                If Not (TmpIndex.FromDate.ToOADate <= TmpWeek.StartDate AndAlso TmpIndex.ToDate.ToOADate >= TmpWeek.EndDate) Then
                                    'The index is only partly used on this week. Calculate how many days and display it.
                                    Dim _weekLength As Integer = TmpWeek.EndDate - TmpWeek.StartDate + 1
                                    Dim _indexLength As Integer = Math.Abs(TmpWeek.StartDate - TmpIndex.FromDate.ToOADate) * Math.Abs(CInt(TmpIndex.FromDate.ToOADate > TmpWeek.StartDate))
                                    _indexLength += Math.Abs(TmpWeek.EndDate - TmpIndex.ToDate.ToOADate) * Math.Abs(CInt(TmpIndex.ToDate.ToOADate < TmpWeek.EndDate))
                                    .Cells(r, 2).Value = TmpIndex.Name & " (" & _weekLength - _indexLength & " / " & _weekLength & " days)"
                                Else
                                    'The index is for the entire period
                                    .Cells(r, 2).Value = TmpIndex.Name
                                End If
                                .Cells(r, 4).Value = TmpIndex.Index / 100
                                IndexList.Add(.Cells(r, 4).Address())
                                r += 1
                            End If
                        End If
                    End If
                Next
                If TmpWeek.AddedValueIndexNet <> 1 Then
                    .Cells(r, 2).Value = "Added values"
                    .Cells(r, 4).Value = TmpWeek.AddedValueIndexNet
                    IndexList.Add(.Cells(r, 4).Address())
                    r += 1
                End If
                With .Cells(r, 4).Borders(8)
                    .LineStyle = -4115
                    .Weight = -4138
                    .ColorIndex = -4105
                End With
                Dim FormulaString As String = ""
                If IndexList.Count > 0 Then
                    FormulaString = "("
                    For Each TmpStr As String In IndexList
                        FormulaString &= TmpStr & "*"
                    Next
                    FormulaString = FormulaString.Trim("*") & ")"
                End If
                If EnhancementList.Count > 0 Then
                    Dim EnhString As String = ""
                    For Each _enh As String In EnhancementList
                        EnhString &= _enh & "+"
                    Next
                    EnhString = EnhString.Trim("+")
                    If TmpBT.EnhancementFactor <> 100 Then
                        FormulaString &= "*(1-(" & EnhString & ") / (1 + " & EnhString & ")/" & TmpBT.EnhancementFactor / 100 & ")"
                    Else
                        FormulaString &= "*(1/(1+" & EnhString & "))"
                    End If
                    FormulaString = FormulaString.Trim("*")
                End If
                .Cells(r, 1).Value = "Net CPP 30"""
                Dim NetCPP30Row As Integer = r
                If FormulaString <> "" Then
                    .cells(r, 4).Formula = "=" & NetCPPFormula & "*" & FormulaString
                Else
                    .cells(r, 4).Formula = "=" & NetCPPFormula
                End If
                r += 2
                .Cells(r, 2).Value = "Spot index"
                .Cells(r, 4).Value = TmpWeek.SpotIndex / 100
                r += 2
                .Cells(r, 1).Value = "Net CPP"
                .Cells(r, 4).Value = "=PRODUCT(D" & NetCPP30Row & ":D" & r - 1 & ")"
            End With
            Excel.ScreenUpdating = True
            Excel.DisplayAlerts = True
            Excel.Visible = True
        End Sub

        Private Class ExportEventArgs
            Inherits EventArgs

            Private _week As cWeek
            Public Property Week() As cWeek
                Get
                    Return _week
                End Get
                Set(ByVal value As cWeek)
                    _week = value
                End Set
            End Property

            Private _bookingType As cBookingType
            Public Property Bookingtype() As cBookingType
                Get
                    Return _bookingType
                End Get
                Set(ByVal value As cBookingType)
                    _bookingType = value
                End Set
            End Property

        End Class


    End Class


End Namespace