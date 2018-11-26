Imports Windows.Forms

Public Class frmDelivery

    Dim SaveFilter As Hashtable

    Private Sub frmDelivery_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        'For Each TmpTable As Hashtable In GeneralFilter.Table
        '    For Each TmpItem As String In Hash
        '    Next
    End Sub


    Private Sub frmDelivery_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each TmpChan As Trinity.cChannel In Campaign.Channels
            For Each TmpBT As Trinity.cBookingType In TmpChan.BookingTypes
                If TmpBT.BookIt Then
                    Dim NewRow As Integer = grdDelivery.Rows.Add
                    grdDelivery.Rows(NewRow).Tag = TmpBT
                End If
            Next
        Next
        Dim _showWarning As Boolean = False
        grdDelivery.Rows.Add()
        For Each TmpSpot As Trinity.cActualSpot In Campaign.ActualSpots
            TmpSpot.InvalidateTargets()
            If TmpSpot.Bookingtype IsNot Nothing AndAlso TmpSpot.Bookingtype.IsSpecific AndAlso TmpSpot.MatchedSpot Is Nothing Then
                _showWarning = True
            End If
        Next
        If _showWarning Then Windows.Forms.MessageBox.Show("There are unmatched specifics spots among the actuals." & vbCrLf & "These spots will not have accurately calculated values," & vbCrLf & "which will impact the overall value of the campaign", "T R I N I T Y", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub grdDelivery_CellDoubleClick_old(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

        Dim TmpBT As Trinity.cBookingType = grdDelivery.Rows(e.RowIndex).Tag

        'set up a form and clears the grid
        Dim frmDetails As New frmDetailsWide
        frmDetails.grdDetails.Rows.Clear()
        frmDetails.grdDetails.Rows.Add(TmpBT.Dayparts.Count + 3)
        frmDetails.grdDetails.ForeColor = Color.Black

        'set the channel name and booking type to the top of the form
        frmDetails.grdDetails.Rows(0).Cells(0).Value = grdDelivery.Rows(e.RowIndex).Cells(0).Value

        'write the headlines
        frmDetails.grdDetails.Rows(1).Cells(1).Value = "Plan TRP 30'"
        frmDetails.grdDetails.Rows(1).Cells(2).Value = "Actual TRP 30'"
        frmDetails.grdDetails.Rows(1).Cells(3).Value = "Split %"
        frmDetails.grdDetails.Rows(1).Cells(4).Value = "Cost"
        frmDetails.grdDetails.Rows(1).Cells(5).Value = "Value"
        frmDetails.grdDetails.Rows(1).Cells(6).Value = "Diff"

        'go through all confirmed spots and print the desired values into the grid
        For dp As Integer = 0 To TmpBT.Dayparts.Count - 1

            'set the estimation to cell one 
            frmDetails.grdDetails.Rows(2 + dp).Cells(1).Value += DirectCast(grdDelivery.Rows(e.RowIndex).Tag, Trinity.cBookingType).PlannedTRP30(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget) * DirectCast(grdDelivery.Rows(e.RowIndex).Tag, Trinity.cBookingType).Dayparts(dp).Share / 100

            'set the net cost
            frmDetails.grdDetails.Rows(2 + dp).Cells(4).Value += 0

        Next

        'go through all actual spots and print the desired values into the grid
        For Each spot As Trinity.cActualSpot In Campaign.ActualSpots
            If spot.Bookingtype.ToString = grdDelivery.Rows(e.RowIndex).Cells(0).Value Then

                'set the estimation to cell 2
                frmDetails.grdDetails.Rows(2 + TmpBT.Dayparts.GetDaypartIndexForMam(spot.MaM)).Cells(2).Value += spot.Rating30(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget)

                'set the value of the spot
                frmDetails.grdDetails.Rows(2 + TmpBT.Dayparts.GetDaypartIndexForMam(spot.MaM)).Cells(5).Value += spot.ActualNetValue

            End If
        Next

        Dim sumTRP As Double = 0
        For i As Integer = 0 To TmpBT.Dayparts.Count - 1
            sumTRP += frmDetails.grdDetails.Rows(2 + i).Cells(2).Value
        Next

        'go through all dayparts 
        For i As Integer = 0 To TmpBT.Dayparts.Count - 1
            'write the names
            frmDetails.grdDetails.Rows(2 + i).Cells(0).Value = Campaign.Dayparts(i).Name

            'set the daypart split achieved (desired)
            Dim percentedge As Double = (frmDetails.grdDetails.Rows(2 + i).Cells(2).Value / sumTRP) * 100
            frmDetails.grdDetails.Rows(2 + i).Cells(3).Value = Format(percentedge, "N1") & " (" & TmpBT.Dayparts(i).Share & ")"

            'if the costs for the bt is 0 we estimate a cost
            If (frmDetails.grdDetails.Rows(2 + i).Cells(4).Value Is Nothing OrElse frmDetails.grdDetails.Rows(2 + i).Cells(4).Value = 0) And Not TmpBT.Dayparts(i).Share = 0 Then
                frmDetails.grdDetails.Rows(2 + i).Cells(4).Value = TmpBT.ConfirmedNetBudget * (percentedge / 100)
            End If

            'write the diff column
            If frmDetails.grdDetails.Rows(2 + i).Cells(5).Value - frmDetails.grdDetails.Rows(2 + i).Cells(4).Value < 0 Then
                frmDetails.grdDetails.Rows(2 + i).Cells(6).Style.ForeColor = Color.Red
                frmDetails.grdDetails.Rows(2 + i).Cells(6).Style.SelectionForeColor = Color.Red
            ElseIf frmDetails.grdDetails.Rows(2 + i).Cells(5).Value - frmDetails.grdDetails.Rows(2 + i).Cells(4).Value > 0 Then
                frmDetails.grdDetails.Rows(2 + i).Cells(6).Style.ForeColor = Color.Green
                frmDetails.grdDetails.Rows(2 + i).Cells(6).Style.SelectionForeColor = Color.Green
            Else
                frmDetails.grdDetails.Rows(2 + i).Cells(6).Style.ForeColor = Color.Black
                frmDetails.grdDetails.Rows(2 + i).Cells(6).Style.SelectionForeColor = Color.Black
            End If
            frmDetails.grdDetails.Rows(2 + i).Cells(6).Value = Format(frmDetails.grdDetails.Rows(2 + i).Cells(5).Value - frmDetails.grdDetails.Rows(2 + i).Cells(4).Value, "C0")

            'set the formatting for the row manually
            frmDetails.grdDetails.Rows(2 + i).Cells(1).Value = Format(frmDetails.grdDetails.Rows(2 + i).Cells(1).Value, "N1")
            frmDetails.grdDetails.Rows(2 + i).Cells(2).Value = Format(frmDetails.grdDetails.Rows(2 + i).Cells(2).Value, "N1")
            frmDetails.grdDetails.Rows(2 + i).Cells(4).Value = Format(frmDetails.grdDetails.Rows(2 + i).Cells(4).Value, "C0")
            frmDetails.grdDetails.Rows(2 + i).Cells(5).Value = Format(frmDetails.grdDetails.Rows(2 + i).Cells(5).Value, "C0")
        Next

        frmDetails.grdDetails.Size = frmDetails.grdDetails.PreferredSize
        frmDetails.grdDetails.Height += 30
        frmDetails.Width = frmDetails.grdDetails.Width + 40
        frmDetails.Height = frmDetails.grdDetails.Height + 20

        frmDetails.ShowDialog()

    End Sub

    Private Sub ExportDetailsToExcel(ByVal sender As Object, ByVal e As EventArgs)

        Dim Excel As New CultureSafeExcel.Application(False)  'CreateObject("CultureSafeExcel")
        Dim WB As Object

        Excel.ScreenUpdating = False
        Excel.DisplayAlerts = False
        Excel.AutoQuit = False

        WB = Excel.AddWorkbook

        Dim _grd As Windows.Forms.DataGridView = DirectCast(sender, Windows.Forms.Button).FindForm.Controls("grdDetails")

        WB.sheets(1).cells(1, 1).Value = Campaign.Client
        WB.sheets(1).cells(1, 2).Value = MonthName(Date.FromOADate(Campaign.StartDate).Month, True)
        WB.sheets(1).cells(1, 3).Value = Campaign.Name

        ' Changed from the old method that manually tried to change culture to use the inbuilt functionality in the Culture safe excel class
        For Each _row As Windows.Forms.DataGridViewRow In _grd.Rows
            For Each _cell As Windows.Forms.DataGridViewCell In _row.Cells
                With WB.Sheets(1)
                    .Cells(_row.Index + 3, _cell.ColumnIndex + 1).Value = _cell.Value
                End With
            Next
        Next

        'For Each _row As Windows.Forms.DataGridViewRow In _grd.Rows
        '    For Each _cell As Windows.Forms.DataGridViewCell In _row.Cells
        '        With WB.Sheets(1)
        '            If _row.Index > 1 AndAlso _cell.ColumnIndex > 0 AndAlso _cell.Value IsNot Nothing AndAlso Not _cell.Value.ToString.Contains("(") AndAlso Not _cell.Value = "" Then
        '                .Cells(_row.Index + 3, _cell.ColumnIndex + 1).NumberFormat = "0.0"
        '                .Cells(_row.Index + 3, _cell.ColumnIndex + 1).Value = CSng(_cell.Value.ToString.Replace(".", "").Replace(",", ".").Replace(" kr", ""))
        '            Else
        '                .Cells(_row.Index + 3, _cell.ColumnIndex + 1).Value = _cell.Value
        '            End If
        '        End With
        '    Next
        'Next

        Excel.ScreenUpdating = True
        Excel.DisplayAlerts = True
        Excel.Visible = True
    End Sub

    Private Sub grdDelivery_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdDelivery.CellDoubleClick

        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        'set up a form and clears the grid
        Dim frmDetails As New frmDetailsWide
        Dim TmpBT As Trinity.cBookingType = grdDelivery.Rows(e.RowIndex).Tag

        frmDetails.cmdExcel.Visible = True
        AddHandler frmDetails.cmdExcel.Click, AddressOf ExportDetailsToExcel

        frmDetails.grdDetails.Rows.Clear()
        frmDetails.grdDetails.Rows.Add(TmpBT.Dayparts.Count + 3)
        frmDetails.grdDetails.ForeColor = Color.Black

        'set the channel name and booking type to the top of the form
        frmDetails.grdDetails.Rows(0).Cells(0).Value = grdDelivery.Rows(e.RowIndex).Cells(0).Value
        frmDetails.grdDetails.Rows(0).Cells(1).Value = TmpBT.BuyingTarget.TargetName
        'write the headlines
        frmDetails.grdDetails.Rows(1).Cells(1).Value = "Booked TRP 30'"
        frmDetails.grdDetails.Rows(1).Cells(2).Value = "Actual TRP 30'"
        frmDetails.grdDetails.Rows(1).Cells(3).Value = "Diff TRP 30'"
        frmDetails.grdDetails.Rows(1).Cells(4).Value = "Booked CPP 30'"
        frmDetails.grdDetails.Rows(1).Cells(5).Value = "Split %"
        frmDetails.grdDetails.Rows(1).Cells(6).Value = "Cost"
        frmDetails.grdDetails.Rows(1).Cells(7).Value = "Value"
        frmDetails.grdDetails.Rows(1).Cells(8).Value = "Diff"

        If TmpBT IsNot Nothing Then
            For Each TmpSpot As Trinity.cActualSpot In Campaign.ActualSpots
                If TmpSpot.Bookingtype Is TmpBT Then
                    frmDetails.grdDetails.Rows(2 + TmpBT.Dayparts.GetDaypartIndexForMam(TmpSpot.MaM)).Cells(2).Value += TmpSpot.Rating30(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget)
                    frmDetails.grdDetails.Rows(2 + TmpBT.Dayparts.GetDaypartIndexForMam(TmpSpot.MaM)).Cells(7).Value += TmpSpot.ActualNetValue
                End If
            Next
            For dp As Integer = 0 To TmpBT.Dayparts.Count - 1
                frmDetails.grdDetails.Rows(2 + dp).Cells(0).Value = TmpBT.Dayparts(dp).Name
                If TmpBT.IsSpecific Then
                    Dim _dp As Integer = dp
                    Dim _trp = (From _spot As Trinity.cBookedSpot In Campaign.BookedSpots Where _spot.Bookingtype Is TmpBT AndAlso TmpBT.Dayparts.GetDaypartIndexForMam(_spot.MaM) = _dp Select _spot.ChannelEstimate * (_spot.Film.Index / 100)).Sum
                    Dim _budget = (From _spot As Trinity.cBookedSpot In Campaign.BookedSpots Where _spot.Bookingtype Is TmpBT AndAlso TmpBT.Dayparts.GetDaypartIndexForMam(_spot.MaM) = _dp Select _spot.NetPrice).Sum
                    frmDetails.grdDetails.Rows(2 + dp).Cells(1).Value = _trp
                    frmDetails.grdDetails.Rows(2 + dp).Cells(6).Value = _budget
                Else
                    For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                        frmDetails.grdDetails.Rows(2 + dp).Cells(1).Value += TmpWeek.TRPBuyingTarget * (TmpWeek.SpotIndex / 100) * (TmpBT.Dayparts(dp).Share / 100)
                        'frmDetails.grdDetails.Rows(2 + dp).Cells(4).Value += TmpWeek.NetBudget * (TmpbT.Dayparts(dp).Share / 100)
                        If TmpBT.ConfirmedNetBudget = 0 Then
                            'We need to calculate budget according to TRP slit since the daypart split is for TRPs
                            frmDetails.grdDetails.Rows(2 + dp).Cells(6).Value += TmpWeek.TRPBuyingTarget * (TmpWeek.SpotIndex / 100) * (TmpBT.Dayparts(dp).Share / 100) * TmpWeek.NetCPP30(dp)
                        End If
                    Next
                    If TmpBT.ConfirmedNetBudget > 0 Then
                        frmDetails.grdDetails.Rows(2 + dp).Cells(6).Value = TmpBT.ConfirmedNetBudget * (TmpBT.BudgetDaypartSplit(dp) / 100)
                    End If
                End If
                frmDetails.grdDetails.Rows(2 + dp).Cells(4).Value = Format(IIf(frmDetails.grdDetails.Rows(2 + dp).Cells(1).Value = 0, 0, frmDetails.grdDetails.Rows(2 + dp).Cells(6).Value / frmDetails.grdDetails.Rows(2 + dp).Cells(1).Value), "C0")
                frmDetails.grdDetails.Rows(2 + dp).Cells(3).Value = Format(frmDetails.grdDetails.Rows(2 + dp).Cells(2).Value - frmDetails.grdDetails.Rows(2 + dp).Cells(1).Value, "N1")
                If frmDetails.grdDetails.Rows(2 + dp).Cells(3).Value < 0 Then
                    frmDetails.grdDetails.Rows(2 + dp).Cells(3).Style.ForeColor = Color.Red
                Else
                    frmDetails.grdDetails.Rows(2 + dp).Cells(3).Style.ForeColor = Color.Green
                End If
            Next

            Dim sumTRP As Double = 0
            For i As Integer = 0 To TmpBT.Dayparts.Count - 1
                sumTRP += frmDetails.grdDetails.Rows(2 + i).Cells(2).Value
            Next
            For dp As Integer = 0 To TmpBT.Dayparts.Count - 1
                frmDetails.grdDetails.Rows(2 + dp).Cells(8).Value = Format(frmDetails.grdDetails.Rows(2 + dp).Cells(7).Value - frmDetails.grdDetails.Rows(2 + dp).Cells(6).Value, "C0")
                frmDetails.grdDetails.Rows(2 + dp).Cells(1).Value = Format(frmDetails.grdDetails.Rows(2 + dp).Cells(1).Value, "N1")
                frmDetails.grdDetails.Rows(2 + dp).Cells(2).Value = Format(frmDetails.grdDetails.Rows(2 + dp).Cells(2).Value, "N1")
                frmDetails.grdDetails.Rows(2 + dp).Cells(6).Value = Format(frmDetails.grdDetails.Rows(2 + dp).Cells(6).Value, "C0")
                frmDetails.grdDetails.Rows(2 + dp).Cells(7).Value = Format(frmDetails.grdDetails.Rows(2 + dp).Cells(7).Value, "C0")
                If frmDetails.grdDetails.Rows(2 + dp).Cells(8).Value < 0 Then
                    frmDetails.grdDetails.Rows(2 + dp).Cells(8).Style.ForeColor = Color.Red
                Else
                    frmDetails.grdDetails.Rows(2 + dp).Cells(8).Style.ForeColor = Color.Green
                End If
                'set the daypart split achieved (desired)
                Dim percentage As Double

                'Hannes added the extra conditional about the cell value not being an empty string since otherwise having dayparts with no TRPs results in an error
                If sumTRP > 0 And Not frmDetails.grdDetails.Rows(2 + dp).Cells(2).Value = "" Then
                    percentage = (frmDetails.grdDetails.Rows(2 + dp).Cells(2).Value / sumTRP)
                Else
                    percentage = 0
                End If
                frmDetails.grdDetails.Rows(2 + dp).Cells(5).Value = Format(percentage, "P0") & " (" & TmpBT.Dayparts(dp).Share & "%)"
            Next
        Else
            Exit Sub
        End If
        frmDetails.grdDetails.Size = frmDetails.grdDetails.PreferredSize
        frmDetails.grdDetails.Height += 30
        frmDetails.Width = frmDetails.grdDetails.Width + 40
        frmDetails.Height = frmDetails.grdDetails.Height + 20

        frmDetails.ShowDialog()

    End Sub

    Private Sub grdDelivery_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdDelivery.CellFormatting
        If e.ColumnIndex = grdDelivery.Columns("colPlannedTRP").Index Then
            e.Value = Format(e.Value, "N1")
        ElseIf e.ColumnIndex = grdDelivery.Columns("colConfirmedTRP").Index Then
            e.Value = Format(e.Value, "N1")
        ElseIf e.ColumnIndex = grdDelivery.Columns("colCPP").Index Then
            e.Value = Format(e.Value, "C0")
        ElseIf e.ColumnIndex = grdDelivery.Columns("colCPP30").Index Then
            e.Value = Format(e.Value, "C0")
        ElseIf e.ColumnIndex = grdDelivery.Columns("colActualTRP").Index Then
            e.Value = Format(e.Value, "N1")
        ElseIf e.ColumnIndex = grdDelivery.Columns("colCost").Index Then
            e.Value = Format(e.Value, "C0")
        ElseIf e.ColumnIndex = grdDelivery.Columns("colValue").Index Then
            e.Value = Format(e.Value, "C0")
        ElseIf e.ColumnIndex = grdDelivery.Columns("colDiff").Index Then
            If e.Value >= 0 Then
                e.CellStyle.ForeColor = Color.Green
            Else
                e.CellStyle.ForeColor = Color.Red
            End If
            e.Value = Format(e.Value, "C0")
        End If
        If e.RowIndex = grdDelivery.Rows.Count - 1 Then
            e.CellStyle.BackColor = Color.DarkGray
        End If
    End Sub



    Private Sub grdDelivery_CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) Handles grdDelivery.CellValueNeeded
        Dim TmpBT As Trinity.cBookingType = grdDelivery.Rows(e.RowIndex).Tag

        If TmpBT Is Nothing Then
            If e.ColumnIndex = grdDelivery.Columns("colChannel").Index Then
                e.Value = "Total"
            ElseIf e.ColumnIndex = grdDelivery.Columns("colPlannedTRP").Index Then
                For Each TmpRow As Windows.Forms.DataGridViewRow In grdDelivery.Rows
                    If TmpRow.Index <> e.RowIndex Then
                        TmpBT = TmpRow.Tag
                        e.Value += TmpBT.BookedTRP30
                    End If
                Next
            ElseIf e.ColumnIndex = grdDelivery.Columns("colConfirmedTRP").Index Then

            ElseIf e.ColumnIndex = grdDelivery.Columns("colCPP").Index Then

            ElseIf e.ColumnIndex = grdDelivery.Columns("colCPP30").Index Then

            ElseIf e.ColumnIndex = grdDelivery.Columns("colActualTRP").Index Then
                Dim TRP As Decimal = 0
                For Each TmpRow As Windows.Forms.DataGridViewRow In grdDelivery.Rows
                    If TmpRow.Index <> e.RowIndex Then
                        TmpBT = TmpRow.Tag
                        For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                            TRP += TmpWeek.ActualTRPBuying
                        Next
                    End If
                Next
                e.Value = TRP
            ElseIf e.ColumnIndex = grdDelivery.Columns("colCost").Index Then
                grdDelivery.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = 0
                For Each TmpRow As Windows.Forms.DataGridViewRow In grdDelivery.Rows
                    If TmpRow.Index <> e.RowIndex Then
                        TmpBT = TmpRow.Tag
                        If TmpBT.ConfirmedNetBudget > 0 Then
                            grdDelivery.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag += TmpBT.ConfirmedNetBudget
                        Else
                            grdDelivery.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag += TmpBT.PlannedNetBudget
                        End If
                    End If
                Next
                e.Value = grdDelivery.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag
            ElseIf e.ColumnIndex = grdDelivery.Columns("colValue").Index Then
                grdDelivery.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = 0
                For Each TmpRow As Windows.Forms.DataGridViewRow In grdDelivery.Rows
                    If TmpRow.Index <> e.RowIndex Then
                        TmpBT = TmpRow.Tag
                        grdDelivery.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag += TmpBT.ActualNetValue
                    End If
                Next
                e.Value = grdDelivery.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag
            ElseIf e.ColumnIndex = grdDelivery.Columns("colDiff").Index Then
                e.Value = grdDelivery.Rows(e.RowIndex).Cells("colValue").Tag - grdDelivery.Rows(e.RowIndex).Cells("colCost").Tag
            End If
        Else
            If e.ColumnIndex = grdDelivery.Columns("colChannel").Index Then
                e.Value = TmpBT.ToString
            ElseIf e.ColumnIndex = grdDelivery.Columns("colPlannedTRP").Index Then
                e.Value = TmpBT.BookedTRP30
            ElseIf e.ColumnIndex = grdDelivery.Columns("colConfirmedTRP").Index Then
                e.Value = (From _spot As Trinity.cPlannedSpot In Campaign.PlannedSpots Where _spot.Bookingtype Is TmpBT Select _spot.MyRating(Trinity.cPlannedSpot.PlannedTargetEnum.pteBuyingTarget)).Sum
            ElseIf e.ColumnIndex = grdDelivery.Columns("colCPP").Index Then
                Dim _rating = (From _spot As Trinity.cActualSpot In Campaign.ActualSpots Where _spot.Bookingtype Is TmpBT Select _spot.Rating(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget)).Sum
                Dim _cost = IIf(TmpBT.ConfirmedNetBudget > 0, TmpBT.ConfirmedNetBudget, TmpBT.PlannedNetBudget)
                If _rating > 0 Then
                    e.Value = _cost / _rating
                Else
                    e.Value = 0
                End If
            ElseIf e.ColumnIndex = grdDelivery.Columns("colCPP30").Index Then
                Dim _rating = (From _spot As Trinity.cActualSpot In Campaign.ActualSpots Where _spot.Bookingtype Is TmpBT Select _spot.Rating30(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget)).Sum
                Dim _cost = IIf(TmpBT.ConfirmedNetBudget > 0, TmpBT.ConfirmedNetBudget, TmpBT.PlannedNetBudget)
                If _rating > 0 Then
                    e.Value = _cost / _rating
                Else
                    e.Value = 0
                End If
            ElseIf e.ColumnIndex = grdDelivery.Columns("colActualTRP").Index Then
                Dim TRP As Decimal = 0
                For Each TmpWeek As Trinity.cWeek In TmpBT.Weeks
                    TRP += TmpWeek.ActualTRPBuying
                Next
                e.Value = TRP
            ElseIf e.ColumnIndex = grdDelivery.Columns("colCost").Index Then
                If TmpBT.ConfirmedNetBudget <> 0 Then
                    grdDelivery.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = TmpBT.ConfirmedNetBudget
                Else
                    grdDelivery.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = TmpBT.PlannedNetBudget
                End If
                e.Value = grdDelivery.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag
            ElseIf e.ColumnIndex = grdDelivery.Columns("colValue").Index Then
                grdDelivery.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag = TmpBT.ActualNetValue
                e.Value = grdDelivery.Rows(e.RowIndex).Cells(e.ColumnIndex).Tag
            ElseIf e.ColumnIndex = grdDelivery.Columns("colDiff").Index Then
                e.Value = grdDelivery.Rows(e.RowIndex).Cells("colValue").Tag - grdDelivery.Rows(e.RowIndex).Cells("colCost").Tag
            End If
        End If
    End Sub

    ' Writes down the entire delivary to an excel file
    Private Sub cmdExcel_Click(sender As System.Object, e As System.EventArgs) Handles cmdExcel.Click

        Dim grdView As New System.Windows.Forms.DataGridView

        grdView.ForeColor = Color.Black
        For i As Integer = 1 To 9
            grdView.Columns.Add("", "")
        Next

        Dim offsetRow As Integer = 2

        ' Write the header
        grdView.Rows.Add(2)
        grdView.Rows(0).Cells(0).Value = Campaign.Client
        grdView.Rows(0).Cells(1).Value = MonthName(Date.FromOADate(Campaign.StartDate).Month, True)
        grdView.Rows(0).Cells(2).Value = Campaign.Name

        ' Loop trough all the bookingypes used
        For Each tmpChannel As Trinity.cChannel In Campaign.Channels
            For Each tmpBT As Trinity.cBookingType In tmpChannel.BookingTypes
                If (tmpBT.BookIt) Then

                    ' Add new rows to write to
                    grdView.Rows.Add(3 + tmpBT.Dayparts.Count)

                    ' The booking type is booked, write the summary

                    ' Write booking type header
                    grdView.Rows(offsetRow).Cells(0).Value = tmpChannel.ChannelName & " " & tmpBT.Name
                    grdView.Rows(offsetRow).Cells(1).Value = tmpBT.BuyingTarget.TargetName

                    grdView.Rows(offsetRow + 1).Cells(1).Value = "Booked TRP 30'"
                    grdView.Rows(offsetRow + 1).Cells(2).Value = "Actual TRP 30'"
                    grdView.Rows(offsetRow + 1).Cells(3).Value = "Diff TRP 30'"
                    grdView.Rows(offsetRow + 1).Cells(4).Value = "Booked CPP 30'"
                    grdView.Rows(offsetRow + 1).Cells(5).Value = "Split %"
                    grdView.Rows(offsetRow + 1).Cells(6).Value = "Cost"
                    grdView.Rows(offsetRow + 1).Cells(7).Value = "Value"
                    grdView.Rows(offsetRow + 1).Cells(8).Value = "Diff"

                    For Each TmpSpot As Trinity.cActualSpot In Campaign.ActualSpots
                        If TmpSpot.Bookingtype Is tmpBT Then
                            grdView.Rows(offsetRow + 2 + tmpBT.Dayparts.GetDaypartIndexForMam(TmpSpot.MaM)).Cells(2).Value += TmpSpot.Rating30(Trinity.cActualSpot.ActualTargetEnum.ateBuyingTarget)
                            grdView.Rows(offsetRow + 2 + tmpBT.Dayparts.GetDaypartIndexForMam(TmpSpot.MaM)).Cells(7).Value += TmpSpot.ActualNetValue
                        End If
                    Next

                    ' Enter the fields with data
                    For dp As Integer = 0 To tmpBT.Dayparts.Count - 1

                        grdView.Rows(offsetRow + 2 + dp).Cells(0).Value = Campaign.Dayparts(dp).Name

                        If tmpBT.IsSpecific Then
                            Dim _dp As Integer = dp
                            Dim _trp = (From _spot As Trinity.cBookedSpot In Campaign.BookedSpots Where _spot.Bookingtype Is tmpBT AndAlso tmpBT.Dayparts.GetDaypartIndexForMam(_spot.MaM) = _dp Select _spot.ChannelEstimate * (_spot.Film.Index / 100)).Sum
                            Dim _budget = (From _spot As Trinity.cBookedSpot In Campaign.BookedSpots Where _spot.Bookingtype Is tmpBT AndAlso tmpBT.Dayparts.GetDaypartIndexForMam(_spot.MaM) = _dp Select _spot.NetPrice).Sum
                            grdView.Rows(offsetRow + 2 + dp).Cells(1).Value = _trp
                            grdView.Rows(offsetRow + 2 + dp).Cells(6).Value = _budget
                        Else
                            For Each TmpWeek As Trinity.cWeek In tmpBT.Weeks
                                grdView.Rows(offsetRow + 2 + dp).Cells(1).Value += TmpWeek.TRPBuyingTarget * (TmpWeek.SpotIndex / 100) * (tmpBT.Dayparts(dp).Share / 100)
                                'frmDetails.grdDetails.Rows(2 + dp).Cells(4).Value += TmpWeek.NetBudget * (TmpbT.Dayparts(dp).Share / 100)
                                If tmpBT.ConfirmedNetBudget = 0 Then
                                    'We need to calculate budget according to TRP slit since the daypart split is for TRPs
                                    grdView.Rows(offsetRow + 2 + dp).Cells(6).Value += TmpWeek.TRPBuyingTarget * (TmpWeek.SpotIndex / 100) * (tmpBT.Dayparts(dp).Share / 100) * TmpWeek.NetCPP30(dp)
                                End If
                            Next
                            If tmpBT.ConfirmedNetBudget > 0 Then
                                grdView.Rows(offsetRow + 2 + dp).Cells(6).Value = tmpBT.ConfirmedNetBudget * (tmpBT.BudgetDaypartSplit(dp) / 100)
                            End If
                        End If
                        grdView.Rows(offsetRow + 2 + dp).Cells(4).Value = Format(IIf(grdView.Rows(offsetRow + 2 + dp).Cells(1).Value = 0, 0, grdView.Rows(offsetRow + 2 + dp).Cells(6).Value / grdView.Rows(offsetRow + 2 + dp).Cells(1).Value), "C0")
                        grdView.Rows(offsetRow + 2 + dp).Cells(3).Value = Format(grdView.Rows(offsetRow + 2 + dp).Cells(2).Value - grdView.Rows(offsetRow + 2 + dp).Cells(1).Value, "N1")
                        If grdView.Rows(offsetRow + 2 + dp).Cells(3).Value < 0 Then
                            grdView.Rows(offsetRow + 2 + dp).Cells(3).Style.ForeColor = Color.Red
                        Else
                            grdView.Rows(offsetRow + 2 + dp).Cells(3).Style.ForeColor = Color.Green
                        End If
                    Next

                    Dim sumTRP As Double = 0
                    For i As Integer = 0 To tmpBT.Dayparts.Count - 1
                        sumTRP += grdView.Rows(offsetRow + 2 + i).Cells(2).Value
                    Next
                    For dp As Integer = 0 To tmpBT.Dayparts.Count - 1
                        grdView.Rows(offsetRow + 2 + dp).Cells(8).Value = Format(grdView.Rows(offsetRow + 2 + dp).Cells(7).Value - grdView.Rows(offsetRow + 2 + dp).Cells(6).Value, "C0")
                        grdView.Rows(offsetRow + 2 + dp).Cells(1).Value = Format(grdView.Rows(offsetRow + 2 + dp).Cells(1).Value, "N1")
                        grdView.Rows(offsetRow + 2 + dp).Cells(2).Value = Format(grdView.Rows(offsetRow + 2 + dp).Cells(2).Value, "N1")
                        grdView.Rows(offsetRow + 2 + dp).Cells(6).Value = Format(grdView.Rows(offsetRow + 2 + dp).Cells(6).Value, "C0")
                        grdView.Rows(offsetRow + 2 + dp).Cells(7).Value = Format(grdView.Rows(offsetRow + 2 + dp).Cells(7).Value, "C0")
                        If grdView.Rows(offsetRow + 2 + dp).Cells(8).Value < 0 Then
                            grdView.Rows(offsetRow + 2 + dp).Cells(8).Style.ForeColor = Color.Red
                        Else
                            grdView.Rows(offsetRow + 2 + dp).Cells(8).Style.ForeColor = Color.Green
                        End If
                        'set the daypart split achieved (desired)
                        Dim percentage As Double

                        'Hannes added the extra conditional about the cell value not being an empty string since otherwise having dayparts with no TRPs results in an error
                        If sumTRP > 0 And Not grdView.Rows(offsetRow + 2 + dp).Cells(2).Value = "" Then
                            percentage = (grdView.Rows(offsetRow + 2 + dp).Cells(2).Value / sumTRP)
                        Else
                            percentage = 0
                        End If
                        grdView.Rows(offsetRow + 2 + dp).Cells(5).Value = Format(percentage, "P0") & " (" & tmpBT.Dayparts(dp).Share & "%)"
                    Next

                    ' Update the offset
                    offsetRow = grdView.RowCount

                End If
            Next
        Next

        ' Create excel object to print to
        Dim Excel As New CultureSafeExcel.Application(False)  'CreateObject("CultureSafeExcel")
        Dim WB As Object

        Excel.ScreenUpdating = False
        Excel.DisplayAlerts = False
        Excel.AutoQuit = False

        WB = Excel.AddWorkbook

        With (WB.Sheets(1))

            For Each _row As Windows.Forms.DataGridViewRow In grdView.Rows
                For Each _cell As Windows.Forms.DataGridViewCell In _row.Cells
                    .Cells(_row.Index + 1, _cell.ColumnIndex + 1).Value = _cell.Value
                Next
            Next

        End With

        Excel.ScreenUpdating = True
        Excel.DisplayAlerts = True
        Excel.Visible = True

    End Sub
End Class